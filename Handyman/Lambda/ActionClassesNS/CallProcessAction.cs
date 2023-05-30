using System;
using Serilog;
using Dapper;
using System.Data.SqlClient;

using ActionInterfaceNS;
using AuditDAONS;
using CommonQueryUtilNS;
using UtilityMethodsNS;
using ExceptionNS;
using ResourceAccessNS;

[ActionExecution(ActionName = CallProcessAction.CALLPROCESS)]
public class CallProcessAction : IActionExecution
{
    public const string CALLPROCESS = "CallProcess";
    private ActionExecutionAudit actionExecutionAudit;
    private ILogger log;
    private CallProcess callprocess;

    public CallProcessAction(ActionExecutionAudit actionExecutionAudit, ILogger log, object callprocess)
    {
        this.callprocess = (CallProcess)callprocess;
        this.actionExecutionAudit = actionExecutionAudit;
        this.log = log;
    }
    public void execute()
    {
        Log.Information("<-------Call Process Action for {CallProcessName} has been started------->", callprocess.name);
        string fileRelativePath = callprocess.source;
        string targetProcess = callprocess.target;
        string dbSrc = callprocess.datasource;
        string sql = callprocess.value.Replace("\"", "");
        Log.Information(" id#{ActionId}, name#{CallProcessName}, calledProcess#{TargetProcess}, calledFile#{FileRelativePath}, db=#{DbSrc}",
            actionExecutionAudit.ActionId, callprocess.name, targetProcess, fileRelativePath, dbSrc);

        var runContext = new List<LContext>();
        var context = actionExecutionAudit.context;
        using (var connection = (SqlConnection)ResourceAccess.GetSqlConnection(dbSrc))
        {

            try
            {
                connection.Open();
                using (var transaction = connection.BeginTransaction())
                {
                    var formattedQueries = CommonQueryUtil.GetFormattedQuery(sql);
                    foreach (var sqlToExecute in formattedQueries)
                    {
                        Log.Information("Execution query sql#{SqlToExecute} on db=#{DbSrc}", sqlToExecute, dbSrc);
                        var results = connection.Query(sqlToExecute, transaction: transaction);
                        foreach (var row in results)
                        {
                            var stringObjectMap = (IDictionary<string, object>)row;
                            foreach (var kvp in stringObjectMap)
                            {
                                context[kvp.Key] = kvp.Value?.ToString();
                                Log.Debug("Value {Value} has been added for {Key}", kvp.Value, kvp.Key);
                            }
                            var lContext = new LContext()
                            {
                                InheritedContext = new Dictionary<string, string>(context),
                                LambdaName = actionExecutionAudit.LambdaName,
                                ParentActionId = actionExecutionAudit.ActionId,
                                ParentActionName = actionExecutionAudit.ActionName,
                                RelativePath = fileRelativePath,
                                ProcessLoadType = HRequestResolver.LoadType.FILE.ToString(),
                                PipelineName = callprocess.target,
                                ParentPipelineId = actionExecutionAudit.PipelineId,
                                ParentPipelineName = actionExecutionAudit.PipelineName,
                                RootPipelineId = actionExecutionAudit.RootPipelineId
                            };
                            runContext.Add(lContext);
                        }
                    }

                    transaction.Commit();
                }
            }
            catch (Exception e)
            {
                Log.Error(e, "The Exception occurred");
                throw new HandymanException("Failed to execute", e);
            }
        }

        try
        {
            var counter = new CountdownEvent(runContext.Count);

            int forkBatchSize = callprocess.forkBatchSize != null ? int.Parse(callprocess.forkBatchSize) : 0;
            if (forkBatchSize != 0)
            {
                var executor = new ConcurrentExclusiveSchedulerPair(TaskScheduler.Default, forkBatchSize).ExclusiveScheduler;

                foreach (var lContext in runContext)
                {
                    var lambdaCallable = new LambdaCallable(lContext, counter);
                    var task = Task.Factory.StartNew(lambdaCallable.Call, CancellationToken.None, TaskCreationOptions.None, executor);
                }

                counter.Wait();
            }
            else
            {
                foreach (var lContext in runContext)
                {
                    var lambdaCallable = new LambdaCallable(lContext, counter);
                    try
                    {
                        PipelineExecutionAudit start = lambdaCallable.Call();
                        foreach (var kvp in start.context)
                        {
                            context[kvp.Key] = kvp.Value;
                        }
                    }
                    catch (Exception e)
                    {
                        log.Error($"Failed process {lContext}", e);
                    }
                }
            }
        }
        catch (Exception e)
        {
            log.Error("The Exception occurred ", e);
            throw new HandymanException("Failed to execute", e);
        }

    }
    public bool executeIf()
    {
        return callprocess.condition;
    }
}

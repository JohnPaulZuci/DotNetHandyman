using System;
using Serilog;
using Dapper;

using ActionInterfaceNS;
using AuditDAONS;
using ResourceAccessNS;
using CommonQueryUtilNS;
using ExceptionNS;

[ActionExecution(ActionName = AssignAction.ASSIGN)]
public class AssignAction : IActionExecution
{
    public const string ASSIGN = "Assign";
    private ActionExecutionAudit actionExecutionAudit;
    private ILogger log;
    private Assign assign;

    public AssignAction(ActionExecutionAudit actionExecutionAudit, ILogger log, object assign)
    {
        this.assign = (Assign)assign;
        this.actionExecutionAudit = actionExecutionAudit;
        this.log = log;
    }
    public void execute()
    {
        log.Information("<-------Assign Action for {0} has been started------->", assign.name);
        string dbSrc = assign.source;
        log.Information(" input variables id: {0}, name: {1}, source-database: {2}", 
			actionExecutionAudit.ActionId, assign.name, dbSrc);
        log.Debug("Sql input post parameter ingestion \n {0}", assign.value);
        Dictionary<string, string> context = actionExecutionAudit.context;

        try
        {
            using (var dbConnection = ResourceAccess.GetSqlConnection(dbSrc))
            {
                dbConnection.Open();

                var formattedQueries = CommonQueryUtil.GetFormattedQuery(assign.value);
                foreach (var sqlToExecute in formattedQueries)
                {
                    log.Information("Execution query sql# {0} on db= {1}", sqlToExecute, dbSrc);
                    var queryResult = dbConnection.QueryFirstOrDefault(sqlToExecute);
                    {
                        foreach (var keyValue in queryResult)
                        {
                            string key = assign.name == string.Empty ? keyValue.Key : $"{assign.name}.{keyValue.Key}";
                            string value = Convert.ToString(keyValue.Value);
                            context[key] = value;
                            log.Debug("Value {0} has been added for {1}", value, key);
                        }
                    }
                }
            }

            log.Information($"<-------Assign Action for {assign.name} has been Completed------->");
        }
        catch (Exception e)
        {
            log.Error("The Exception occurred ", e);
            throw new HandymanException("Failed to execute", e);
        }
    }

    public bool executeIf() { return true; }
}

using System;
using Serilog;
using Dapper;

using ActionInterfaceNS;
using AuditDAONS;
using ResourceAccessNS;
using CommonQueryUtilNS;
using ExceptionNS;

[ActionExecution(ActionName = TransformAction.TRANSFORM)]
public class TransformAction : IActionExecution
{
    public const string TRANSFORM = "Transform";
    private ActionExecutionAudit actionExecutionAudit;
    private ILogger log;
    private Transform transform;

    public TransformAction(ActionExecutionAudit actionExecutionAudit, ILogger log, object transform)
    {
        this.transform = (Transform)transform;
        this.actionExecutionAudit = actionExecutionAudit;
        this.log = log;
    }
    public void execute()
    {

        log.Information("<-------Transform Action for {TransformName} has been started------->", transform.name);
        string dbSrc = transform.on;
        log.Information("Transform action input variables id: {ActionId}, name: {TransformName}, " +
            "source-database: {Database}", actionExecutionAudit.ActionId, transform.name, dbSrc);
        log.Debug("Sql input post parameter ingestion \n {Value}", transform._value);

        using (var connection = ResourceAccess.GetSqlConnection(dbSrc))
        {
            connection.Open();
            var transaction = connection.BeginTransaction();

            try
            {
                foreach (string givenQuery in transform._value)
                {
                    var sqlList = transform.format ? CommonQueryUtil.GetFormattedQuery(givenQuery) : new List<string> { givenQuery };

                    foreach (var sqlToExecute in sqlList)
                    {
                        log.Information("Transform with id:{ActionId}, executing script {Script}", actionExecutionAudit.ActionId, givenQuery);
                        var statementId = BitConverter.ToInt64(Guid.NewGuid().ToByteArray(), 0);

                        try
                        {
                            var rowCount = connection.Execute(sqlToExecute, transaction: transaction);
                            //////var warnings = ExceptionUtil.CompleteSQLWarning(((SqlConnection)connection).Warnings);
                            log.Debug("{Script}.count {RowCount}", sqlToExecute, rowCount);
                            /////log.Debug("{Script}.warnings {Warnings}", sqlToExecute, warnings);
                            log.Information("Transform id# {StatementId}, executed script {Script}, rows returned {RowCount}", statementId, sqlToExecute, rowCount);
                        }
                        catch (Exception ex) when (ex.Message != string.Empty)
                        {
                            log.Error("Stopping execution, General Error executing sql for {Script} with for {Exception}", sqlToExecute, ex);
                            ///log.Information("{Script}.exception {Exception}", sqlToExecute, ExceptionUtil.ToString(ex));
                            throw new HandymanException("Process failed", ex);
                        }
                        catch (Exception ex)
                        {
                            log.Error("Continuing to execute, even though SQL Error executing sql for {Script}", sqlToExecute, ex);
                            //// log.Information("{Script}.exception {Exception}", sqlToExecute, ExceptionUtil.ToString(ex));
                            throw new HandymanException("Process failed", ex);
                        }
                        // catch (Exception ex)
                        // {
                        //     log.Error("Stopping execution, General Error executing sql for {Script} with for {Exception}", sqlToExecute, ex);
                        //     log.Information("{Script}.exception {Exception}", sqlToExecute, ExceptionUtil.ToString(ex));
                        //     throw new HandymanException("Process failed", ex);
                        // }
                    }

                    transaction.Commit();
                    log.Debug("Completed Transform id#{ActionId}, name#{TransformName}, dbSrc#{Database}, sqlList#{SqlList}",
                    actionExecutionAudit.ActionId, transform.name, dbSrc, sqlList);
                }

                log.Information("<-------Transform Action for {TransformName} has been completed------->", transform.name);
            }
            catch (Exception ex)
            {
                log.Error("Stopping execution, Fetching connection failed", ex);
                //// log.Information("connection.exception {Exception}", ExceptionUtil.ToString(ex));
                throw new HandymanException("Process failed", ex);
            }
            finally
            {
                transaction.Dispose();
                connection.Close();
            }
        }
        Console.WriteLine("Valueeeee" + transform._value + "#####cond####" + transform.condition +
      "#####name####" + transform.name + "#####on####" + transform.on);
    }
    public bool executeIf() { return true; }
}

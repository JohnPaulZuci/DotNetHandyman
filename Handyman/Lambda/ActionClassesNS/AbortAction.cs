using System;
using Serilog;
using ActionInterfaceNS;
using AuditDAONS;
using ExceptionNS;

[ActionExecution(ActionName = AbortAction.ABORT)]
public class AbortAction : IActionExecution
{
    public const string ABORT = "Abort";
    private ActionExecutionAudit actionExecutionAudit;
    private ILogger log;
    private Abort abort;

    public AbortAction(ActionExecutionAudit actionExecutionAudit, ILogger log, object abort)
    {
        this.abort = (Abort)abort;
        this.actionExecutionAudit = actionExecutionAudit;
        this.log = log;
    }
    public void execute()
    {
        Log.Information($"Abort action id#{actionExecutionAudit.ActionId}, name#{abort.name}, calledProcess#{actionExecutionAudit.ActionName}, message#{abort.value}");
        throw new AbortException(abort.value);
    }
    public bool executeIf()
    {
        return abort.condition;
    }
}

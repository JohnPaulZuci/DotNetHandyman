using System;
using Serilog;
using ActionInterfaceNS;
using AuditDAONS;

[ActionExecution(ActionName = TqaFilterAction.TQAFILTER)]
public class TqaFilterAction : IActionExecution {
public const string TQAFILTER= "TqaFilter";
 private ActionExecutionAudit actionExecutionAudit; 
 private ILogger log; 
 private TqaFilter tqafilter; 

public TqaFilterAction (ActionExecutionAudit actionExecutionAudit, ILogger log, object tqafilter){
	this.tqafilter = (TqaFilter) tqafilter ;
	this.actionExecutionAudit = actionExecutionAudit; 
	this.log = log; 
}
public void execute() { }
public bool executeIf() {return true;}
}

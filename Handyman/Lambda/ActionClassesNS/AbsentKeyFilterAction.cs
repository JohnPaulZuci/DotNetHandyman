using System;
using Serilog;
using ActionInterfaceNS;
using AuditDAONS;

[ActionExecution(ActionName = AbsentKeyFilterAction.ABSENTKEYFILTER)]
public class AbsentKeyFilterAction : IActionExecution {
public const string ABSENTKEYFILTER= "AbsentKeyFilter";
 private ActionExecutionAudit actionExecutionAudit; 
 private ILogger log; 
 private AbsentKeyFilter absentkeyfilter; 

public AbsentKeyFilterAction (ActionExecutionAudit actionExecutionAudit, ILogger log, object absentkeyfilter){
	this.absentkeyfilter = (AbsentKeyFilter) absentkeyfilter ;
	this.actionExecutionAudit = actionExecutionAudit; 
	this.log = log; 
}
public void execute() { }
public bool executeIf() {return true;}
}

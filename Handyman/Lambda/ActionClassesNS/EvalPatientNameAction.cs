using System;
using Serilog;
using ActionInterfaceNS;
using AuditDAONS;

[ActionExecution(ActionName = EvalPatientNameAction.EVALPATIENTNAME)]
public class EvalPatientNameAction : IActionExecution {
public const string EVALPATIENTNAME= "EvalPatientName";
 private ActionExecutionAudit actionExecutionAudit; 
 private ILogger log; 
 private EvalPatientName evalpatientname; 

public EvalPatientNameAction (ActionExecutionAudit actionExecutionAudit, ILogger log, object evalpatientname){
	this.evalpatientname = (EvalPatientName) evalpatientname ;
	this.actionExecutionAudit = actionExecutionAudit; 
	this.log = log; 
}
public void execute() { }
public bool executeIf() {return true;}
}

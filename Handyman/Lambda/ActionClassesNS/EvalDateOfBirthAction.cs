using System;
using Serilog;
using ActionInterfaceNS;
using AuditDAONS;

[ActionExecution(ActionName = EvalDateOfBirthAction.EVALDATEOFBIRTH)]
public class EvalDateOfBirthAction : IActionExecution {
public const string EVALDATEOFBIRTH= "EvalDateOfBirth";
 private ActionExecutionAudit actionExecutionAudit; 
 private ILogger log; 
 private EvalDateOfBirth evaldateofbirth; 

public EvalDateOfBirthAction (ActionExecutionAudit actionExecutionAudit, ILogger log, object evaldateofbirth){
	this.evaldateofbirth = (EvalDateOfBirth) evaldateofbirth ;
	this.actionExecutionAudit = actionExecutionAudit; 
	this.log = log; 
}
public void execute() { }
public bool executeIf() {return true;}
}

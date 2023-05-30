using System;
using Serilog;
using ActionInterfaceNS;
using AuditDAONS;

[ActionExecution(ActionName = CreateTARAction.CREATETAR)]
public class CreateTARAction : IActionExecution {
public const string CREATETAR= "CreateTAR";
 private ActionExecutionAudit actionExecutionAudit; 
 private ILogger log; 
 private CreateTAR createtar; 

public CreateTARAction (ActionExecutionAudit actionExecutionAudit, ILogger log, object createtar){
	this.createtar = (CreateTAR) createtar ;
	this.actionExecutionAudit = actionExecutionAudit; 
	this.log = log; 
}
public void execute() { }
public bool executeIf() {return true;}
}

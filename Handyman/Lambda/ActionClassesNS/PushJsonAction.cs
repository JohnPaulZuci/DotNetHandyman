using System;
using Serilog;
using ActionInterfaceNS;
using AuditDAONS;

[ActionExecution(ActionName = PushJsonAction.PUSHJSON)]
public class PushJsonAction : IActionExecution {
public const string PUSHJSON= "PushJson";
 private ActionExecutionAudit actionExecutionAudit; 
 private ILogger log; 
 private PushJson pushjson; 

public PushJsonAction (ActionExecutionAudit actionExecutionAudit, ILogger log, object pushjson){
	this.pushjson = (PushJson) pushjson ;
	this.actionExecutionAudit = actionExecutionAudit; 
	this.log = log; 
}
public void execute() { }
public bool executeIf() {return true;}
}

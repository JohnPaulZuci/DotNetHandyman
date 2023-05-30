using System;
using Serilog;
using ActionInterfaceNS;
using AuditDAONS;

[ActionExecution(ActionName = PushLocalToMlAction.PUSHLOCALTOML)]
public class PushLocalToMlAction : IActionExecution {
public const string PUSHLOCALTOML= "PushLocalToMl";
 private ActionExecutionAudit actionExecutionAudit; 
 private ILogger log; 
 private PushLocalToMl pushlocaltoml; 

public PushLocalToMlAction (ActionExecutionAudit actionExecutionAudit, ILogger log, object pushlocaltoml){
	this.pushlocaltoml = (PushLocalToMl) pushlocaltoml ;
	this.actionExecutionAudit = actionExecutionAudit; 
	this.log = log; 
}
public void execute() { }
public bool executeIf() {return true;}
}

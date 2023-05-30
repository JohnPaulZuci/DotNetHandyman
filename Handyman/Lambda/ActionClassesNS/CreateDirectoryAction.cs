using System;
using Serilog;
using ActionInterfaceNS;
using AuditDAONS;

[ActionExecution(ActionName = CreateDirectoryAction.CREATEDIRECTORY)]
public class CreateDirectoryAction : IActionExecution {
public const string CREATEDIRECTORY= "CreateDirectory";
 private ActionExecutionAudit actionExecutionAudit; 
 private ILogger log; 
 private CreateDirectory createdirectory; 

public CreateDirectoryAction (ActionExecutionAudit actionExecutionAudit, ILogger log, object createdirectory){
	this.createdirectory = (CreateDirectory) createdirectory ;
	this.actionExecutionAudit = actionExecutionAudit; 
	this.log = log; 
}
public void execute() { }
public bool executeIf() {return true;}
}

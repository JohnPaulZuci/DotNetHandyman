using System;
using Serilog;
using ActionInterfaceNS;
using AuditDAONS;

[ActionExecution(ActionName = CreateFileAction.CREATEFILE)]
public class CreateFileAction : IActionExecution {
public const string CREATEFILE= "CreateFile";
 private ActionExecutionAudit actionExecutionAudit; 
 private ILogger log; 
 private CreateFile createfile; 

public CreateFileAction (ActionExecutionAudit actionExecutionAudit, ILogger log, object createfile){
	this.createfile = (CreateFile) createfile ;
	this.actionExecutionAudit = actionExecutionAudit; 
	this.log = log; 
}
public void execute() { }
public bool executeIf() {return true;}
}

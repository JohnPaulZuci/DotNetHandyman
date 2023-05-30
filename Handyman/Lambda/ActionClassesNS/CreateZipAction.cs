using System;
using Serilog;
using ActionInterfaceNS;
using AuditDAONS;

[ActionExecution(ActionName = CreateZipAction.CREATEZIP)]
public class CreateZipAction : IActionExecution {
public const string CREATEZIP= "CreateZip";
 private ActionExecutionAudit actionExecutionAudit; 
 private ILogger log; 
 private CreateZip createzip; 

public CreateZipAction (ActionExecutionAudit actionExecutionAudit, ILogger log, object createzip){
	this.createzip = (CreateZip) createzip ;
	this.actionExecutionAudit = actionExecutionAudit; 
	this.log = log; 
}
public void execute() { }
public bool executeIf() {return true;}
}

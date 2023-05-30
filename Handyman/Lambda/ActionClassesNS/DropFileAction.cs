using System;
using Serilog;
using ActionInterfaceNS;
using AuditDAONS;

[ActionExecution(ActionName = DropFileAction.DROPFILE)]
public class DropFileAction : IActionExecution {
public const string DROPFILE= "DropFile";
 private ActionExecutionAudit actionExecutionAudit; 
 private ILogger log; 
 private DropFile dropfile; 

public DropFileAction (ActionExecutionAudit actionExecutionAudit, ILogger log, object dropfile){
	this.dropfile = (DropFile) dropfile ;
	this.actionExecutionAudit = actionExecutionAudit; 
	this.log = log; 
}
public void execute() { }
public bool executeIf() {return true;}
}

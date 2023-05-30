using System;
using Serilog;
using ActionInterfaceNS;
using AuditDAONS;

[ActionExecution(ActionName = DeleteFileDirectoryAction.DELETEFILEDIRECTORY)]
public class DeleteFileDirectoryAction : IActionExecution {
public const string DELETEFILEDIRECTORY= "DeleteFileDirectory";
 private ActionExecutionAudit actionExecutionAudit; 
 private ILogger log; 
 private DeleteFileDirectory deletefiledirectory; 

public DeleteFileDirectoryAction (ActionExecutionAudit actionExecutionAudit, ILogger log, object deletefiledirectory){
	this.deletefiledirectory = (DeleteFileDirectory) deletefiledirectory ;
	this.actionExecutionAudit = actionExecutionAudit; 
	this.log = log; 
}
public void execute() { }
public bool executeIf() {return true;}
}

using System;
using Serilog;
using ActionInterfaceNS;
using AuditDAONS;

[ActionExecution(ActionName = FileMergerAction.FILEMERGER)]
public class FileMergerAction : IActionExecution {
public const string FILEMERGER= "FileMerger";
 private ActionExecutionAudit actionExecutionAudit; 
 private ILogger log; 
 private FileMerger filemerger; 

public FileMergerAction (ActionExecutionAudit actionExecutionAudit, ILogger log, object filemerger){
	this.filemerger = (FileMerger) filemerger ;
	this.actionExecutionAudit = actionExecutionAudit; 
	this.log = log; 
}
public void execute() { }
public bool executeIf() {return true;}
}

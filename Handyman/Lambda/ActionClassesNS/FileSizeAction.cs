using System;
using Serilog;
using ActionInterfaceNS;
using AuditDAONS;

[ActionExecution(ActionName = FileSizeAction.FILESIZE)]
public class FileSizeAction : IActionExecution {
public const string FILESIZE= "FileSize";
 private ActionExecutionAudit actionExecutionAudit; 
 private ILogger log; 
 private FileSize filesize; 

public FileSizeAction (ActionExecutionAudit actionExecutionAudit, ILogger log, object filesize){
	this.filesize = (FileSize) filesize ;
	this.actionExecutionAudit = actionExecutionAudit; 
	this.log = log; 
}
public void execute() { }
public bool executeIf() {return true;}
}

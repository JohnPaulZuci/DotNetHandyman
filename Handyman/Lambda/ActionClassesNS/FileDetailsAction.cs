using System;
using Serilog;
using ActionInterfaceNS;
using AuditDAONS;

[ActionExecution(ActionName = FileDetailsAction.FILEDETAILS)]
public class FileDetailsAction : IActionExecution {
public const string FILEDETAILS= "FileDetails";
 private ActionExecutionAudit actionExecutionAudit; 
 private ILogger log; 
 private FileDetails filedetails; 

public FileDetailsAction (ActionExecutionAudit actionExecutionAudit, ILogger log, object filedetails){
	this.filedetails = (FileDetails) filedetails ;
	this.actionExecutionAudit = actionExecutionAudit; 
	this.log = log; 
}
public void execute() { }
public bool executeIf() {return true;}
}

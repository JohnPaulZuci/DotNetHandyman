using System;
using Serilog;
using ActionInterfaceNS;
using AuditDAONS;

[ActionExecution(ActionName = FtpsUploadAction.FTPSUPLOAD)]
public class FtpsUploadAction : IActionExecution {
public const string FTPSUPLOAD= "FtpsUpload";
 private ActionExecutionAudit actionExecutionAudit; 
 private ILogger log; 
 private FtpsUpload ftpsupload; 

public FtpsUploadAction (ActionExecutionAudit actionExecutionAudit, ILogger log, object ftpsupload){
	this.ftpsupload = (FtpsUpload) ftpsupload ;
	this.actionExecutionAudit = actionExecutionAudit; 
	this.log = log; 
}
public void execute() { }
public bool executeIf() {return true;}
}

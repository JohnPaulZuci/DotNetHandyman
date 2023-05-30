using System;
using Serilog;
using ActionInterfaceNS;
using AuditDAONS;

[ActionExecution(ActionName = FtpsDownloadAction.FTPSDOWNLOAD)]
public class FtpsDownloadAction : IActionExecution {
public const string FTPSDOWNLOAD= "FtpsDownload";
 private ActionExecutionAudit actionExecutionAudit; 
 private ILogger log; 
 private FtpsDownload ftpsdownload; 

public FtpsDownloadAction (ActionExecutionAudit actionExecutionAudit, ILogger log, object ftpsdownload){
	this.ftpsdownload = (FtpsDownload) ftpsdownload ;
	this.actionExecutionAudit = actionExecutionAudit; 
	this.log = log; 
}
public void execute() { }
public bool executeIf() {return true;}
}

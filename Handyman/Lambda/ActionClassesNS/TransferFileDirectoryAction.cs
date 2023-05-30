using System;
using Serilog;
using ActionInterfaceNS;
using AuditDAONS;

[ActionExecution(ActionName = TransferFileDirectoryAction.TRANSFERFILEDIRECTORY)]
public class TransferFileDirectoryAction : IActionExecution {
public const string TRANSFERFILEDIRECTORY= "TransferFileDirectory";
 private ActionExecutionAudit actionExecutionAudit; 
 private ILogger log; 
 private TransferFileDirectory transferfiledirectory; 

public TransferFileDirectoryAction (ActionExecutionAudit actionExecutionAudit, ILogger log, object transferfiledirectory){
	this.transferfiledirectory = (TransferFileDirectory) transferfiledirectory ;
	this.actionExecutionAudit = actionExecutionAudit; 
	this.log = log; 
}
public void execute() { }
public bool executeIf() {return true;}
}

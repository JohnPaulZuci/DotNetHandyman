using System;
using Serilog;
using ActionInterfaceNS;
using AuditDAONS;

[ActionExecution(ActionName = ChecksumAction.CHECKSUM)]
public class ChecksumAction : IActionExecution {
public const string CHECKSUM= "Checksum";
 private ActionExecutionAudit actionExecutionAudit; 
 private ILogger log; 
 private Checksum checksum; 

public ChecksumAction (ActionExecutionAudit actionExecutionAudit, ILogger log, object checksum){
	this.checksum = (Checksum) checksum ;
	this.actionExecutionAudit = actionExecutionAudit; 
	this.log = log; 
}
public void execute() { }
public bool executeIf() {return true;}
}

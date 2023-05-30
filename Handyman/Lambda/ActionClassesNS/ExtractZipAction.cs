using System;
using Serilog;
using ActionInterfaceNS;
using AuditDAONS;

[ActionExecution(ActionName = ExtractZipAction.EXTRACTZIP)]
public class ExtractZipAction : IActionExecution {
public const string EXTRACTZIP= "ExtractZip";
 private ActionExecutionAudit actionExecutionAudit; 
 private ILogger log; 
 private ExtractZip extractzip; 

public ExtractZipAction (ActionExecutionAudit actionExecutionAudit, ILogger log, object extractzip){
	this.extractzip = (ExtractZip) extractzip ;
	this.actionExecutionAudit = actionExecutionAudit; 
	this.log = log; 
}
public void execute() { }
public bool executeIf() {return true;}
}

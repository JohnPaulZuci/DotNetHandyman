using System;
using Serilog;
using ActionInterfaceNS;
using AuditDAONS;

[ActionExecution(ActionName = ExtractTARAction.EXTRACTTAR)]
public class ExtractTARAction : IActionExecution {
public const string EXTRACTTAR= "ExtractTAR";
 private ActionExecutionAudit actionExecutionAudit; 
 private ILogger log; 
 private ExtractTAR extracttar; 

public ExtractTARAction (ActionExecutionAudit actionExecutionAudit, ILogger log, object extracttar){
	this.extracttar = (ExtractTAR) extracttar ;
	this.actionExecutionAudit = actionExecutionAudit; 
	this.log = log; 
}
public void execute() { }
public bool executeIf() {return true;}
}

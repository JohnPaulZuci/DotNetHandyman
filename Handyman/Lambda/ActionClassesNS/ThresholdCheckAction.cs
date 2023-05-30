using System;
using Serilog;
using ActionInterfaceNS;
using AuditDAONS;

[ActionExecution(ActionName = ThresholdCheckAction.THRESHOLDCHECK)]
public class ThresholdCheckAction : IActionExecution {
public const string THRESHOLDCHECK= "ThresholdCheck";
 private ActionExecutionAudit actionExecutionAudit; 
 private ILogger log; 
 private ThresholdCheck thresholdcheck; 

public ThresholdCheckAction (ActionExecutionAudit actionExecutionAudit, ILogger log, object thresholdcheck){
	this.thresholdcheck = (ThresholdCheck) thresholdcheck ;
	this.actionExecutionAudit = actionExecutionAudit; 
	this.log = log; 
}
public void execute() { }
public bool executeIf() {return true;}
}

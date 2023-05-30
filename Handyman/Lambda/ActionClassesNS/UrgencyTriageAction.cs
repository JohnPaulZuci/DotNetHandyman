using System;
using Serilog;
using ActionInterfaceNS;
using AuditDAONS;

[ActionExecution(ActionName = UrgencyTriageAction.URGENCYTRIAGE)]
public class UrgencyTriageAction : IActionExecution {
public const string URGENCYTRIAGE= "UrgencyTriage";
 private ActionExecutionAudit actionExecutionAudit; 
 private ILogger log; 
 private UrgencyTriage urgencytriage; 

public UrgencyTriageAction (ActionExecutionAudit actionExecutionAudit, ILogger log, object urgencytriage){
	this.urgencytriage = (UrgencyTriage) urgencytriage ;
	this.actionExecutionAudit = actionExecutionAudit; 
	this.log = log; 
}
public void execute() { }
public bool executeIf() {return true;}
}

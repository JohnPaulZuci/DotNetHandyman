using System;
using Serilog;
using ActionInterfaceNS;
using AuditDAONS;

[ActionExecution(ActionName = TriageAttributionAction.TRIAGEATTRIBUTION)]
public class TriageAttributionAction : IActionExecution {
public const string TRIAGEATTRIBUTION= "TriageAttribution";
 private ActionExecutionAudit actionExecutionAudit; 
 private ILogger log; 
 private TriageAttribution triageattribution; 

public TriageAttributionAction (ActionExecutionAudit actionExecutionAudit, ILogger log, object triageattribution){
	this.triageattribution = (TriageAttribution) triageattribution ;
	this.actionExecutionAudit = actionExecutionAudit; 
	this.log = log; 
}
public void execute() { }
public bool executeIf() {return true;}
}

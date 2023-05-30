using System;
using Serilog;
using ActionInterfaceNS;
using AuditDAONS;

[ActionExecution(ActionName = DocnetAttributionAction.DOCNETATTRIBUTION)]
public class DocnetAttributionAction : IActionExecution {
public const string DOCNETATTRIBUTION= "DocnetAttribution";
 private ActionExecutionAudit actionExecutionAudit; 
 private ILogger log; 
 private DocnetAttribution docnetattribution; 

public DocnetAttributionAction (ActionExecutionAudit actionExecutionAudit, ILogger log, object docnetattribution){
	this.docnetattribution = (DocnetAttribution) docnetattribution ;
	this.actionExecutionAudit = actionExecutionAudit; 
	this.log = log; 
}
public void execute() { }
public bool executeIf() {return true;}
}

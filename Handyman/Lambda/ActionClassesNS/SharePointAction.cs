using System;
using Serilog;
using ActionInterfaceNS;
using AuditDAONS;

[ActionExecution(ActionName = SharePointAction.SHAREPOINT)]
public class SharePointAction : IActionExecution {
public const string SHAREPOINT= "SharePoint";
 private ActionExecutionAudit actionExecutionAudit; 
 private ILogger log; 
 private SharePoint sharepoint; 

public SharePointAction (ActionExecutionAudit actionExecutionAudit, ILogger log, object sharepoint){
	this.sharepoint = (SharePoint) sharepoint ;
	this.actionExecutionAudit = actionExecutionAudit; 
	this.log = log; 
}
public void execute() { }
public bool executeIf() {return true;}
}

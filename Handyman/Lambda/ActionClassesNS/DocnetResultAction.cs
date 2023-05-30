using System;
using Serilog;
using ActionInterfaceNS;
using AuditDAONS;

[ActionExecution(ActionName = DocnetResultAction.DOCNETRESULT)]
public class DocnetResultAction : IActionExecution {
public const string DOCNETRESULT= "DocnetResult";
 private ActionExecutionAudit actionExecutionAudit; 
 private ILogger log; 
 private DocnetResult docnetresult; 

public DocnetResultAction (ActionExecutionAudit actionExecutionAudit, ILogger log, object docnetresult){
	this.docnetresult = (DocnetResult) docnetresult ;
	this.actionExecutionAudit = actionExecutionAudit; 
	this.log = log; 
}
public void execute() { }
public bool executeIf() {return true;}
}

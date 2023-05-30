using System;
using Serilog;
using ActionInterfaceNS;
using AuditDAONS;

[ActionExecution(ActionName = TextFilterAction.TEXTFILTER)]
public class TextFilterAction : IActionExecution {
public const string TEXTFILTER= "TextFilter";
 private ActionExecutionAudit actionExecutionAudit; 
 private ILogger log; 
 private TextFilter textfilter; 

public TextFilterAction (ActionExecutionAudit actionExecutionAudit, ILogger log, object textfilter){
	this.textfilter = (TextFilter) textfilter ;
	this.actionExecutionAudit = actionExecutionAudit; 
	this.log = log; 
}
public void execute() { }
public bool executeIf() {return true;}
}

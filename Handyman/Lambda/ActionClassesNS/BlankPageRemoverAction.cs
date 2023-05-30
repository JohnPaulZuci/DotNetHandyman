using System;
using Serilog;
using ActionInterfaceNS;
using AuditDAONS;

[ActionExecution(ActionName = BlankPageRemoverAction.BLANKPAGEREMOVER)]
public class BlankPageRemoverAction : IActionExecution {
public const string BLANKPAGEREMOVER= "BlankPageRemover";
 private ActionExecutionAudit actionExecutionAudit; 
 private ILogger log; 
 private BlankPageRemover blankpageremover; 

public BlankPageRemoverAction (ActionExecutionAudit actionExecutionAudit, ILogger log, object blankpageremover){
	this.blankpageremover = (BlankPageRemover) blankpageremover ;
	this.actionExecutionAudit = actionExecutionAudit; 
	this.log = log; 
}
public void execute() { }
public bool executeIf() {return true;}
}

using System;
using Serilog;
using ActionInterfaceNS;
using AuditDAONS;

[ActionExecution(ActionName = PaperItemizationAction.PAPERITEMIZATION)]
public class PaperItemizationAction : IActionExecution {
public const string PAPERITEMIZATION= "PaperItemization";
 private ActionExecutionAudit actionExecutionAudit; 
 private ILogger log; 
 private PaperItemization paperitemization; 

public PaperItemizationAction (ActionExecutionAudit actionExecutionAudit, ILogger log, object paperitemization){
	this.paperitemization = (PaperItemization) paperitemization ;
	this.actionExecutionAudit = actionExecutionAudit; 
	this.log = log; 
}
public void execute() { }
public bool executeIf() {return true;}
}

using System;
using Serilog;
using ActionInterfaceNS;
using AuditDAONS;

[ActionExecution(ActionName = SorGroupDetailsAction.SORGROUPDETAILS)]
public class SorGroupDetailsAction : IActionExecution {
public const string SORGROUPDETAILS= "SorGroupDetails";
 private ActionExecutionAudit actionExecutionAudit; 
 private ILogger log; 
 private SorGroupDetails sorgroupdetails; 

public SorGroupDetailsAction (ActionExecutionAudit actionExecutionAudit, ILogger log, object sorgroupdetails){
	this.sorgroupdetails = (SorGroupDetails) sorgroupdetails ;
	this.actionExecutionAudit = actionExecutionAudit; 
	this.log = log; 
}
public void execute() { }
public bool executeIf() {return true;}
}

using System;
using Serilog;
using ActionInterfaceNS;
using AuditDAONS;

[ActionExecution(ActionName = SorFilterAction.SORFILTER)]
public class SorFilterAction : IActionExecution {
public const string SORFILTER= "SorFilter";
 private ActionExecutionAudit actionExecutionAudit; 
 private ILogger log; 
 private SorFilter sorfilter; 

public SorFilterAction (ActionExecutionAudit actionExecutionAudit, ILogger log, object sorfilter){
	this.sorfilter = (SorFilter) sorfilter ;
	this.actionExecutionAudit = actionExecutionAudit; 
	this.log = log; 
}
public void execute() { }
public bool executeIf() {return true;}
}

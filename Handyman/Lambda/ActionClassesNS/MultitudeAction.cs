using System;
using Serilog;
using ActionInterfaceNS;
using AuditDAONS;

[ActionExecution(ActionName = MultitudeAction.MULTITUDE)]
public class MultitudeAction : IActionExecution {
public const string MULTITUDE= "Multitude";
 private ActionExecutionAudit actionExecutionAudit; 
 private ILogger log; 
 private Multitude multitude; 

public MultitudeAction (ActionExecutionAudit actionExecutionAudit, ILogger log, object multitude){
	this.multitude = (Multitude) multitude ;
	this.actionExecutionAudit = actionExecutionAudit; 
	this.log = log; 
}
public void execute() { }
public bool executeIf() {return true;}
}

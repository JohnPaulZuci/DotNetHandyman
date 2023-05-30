using System;
using Serilog;
using ActionInterfaceNS;
using AuditDAONS;

[ActionExecution(ActionName = ProducerAction.PRODUCER)]
public class ProducerAction : IActionExecution {
public const string PRODUCER= "Producer";
 private ActionExecutionAudit actionExecutionAudit; 
 private ILogger log; 
 private Producer producer; 

public ProducerAction (ActionExecutionAudit actionExecutionAudit, ILogger log, object producer){
	this.producer = (Producer) producer ;
	this.actionExecutionAudit = actionExecutionAudit; 
	this.log = log; 
}
public void execute() { }
public bool executeIf() {return true;}
}

using System;
using Serilog;
using ActionInterfaceNS;
using AuditDAONS;

[ActionExecution(ActionName = ProducerConsumerModelAction.PRODUCERCONSUMERMODEL)]
public class ProducerConsumerModelAction : IActionExecution {
public const string PRODUCERCONSUMERMODEL= "ProducerConsumerModel";
 private ActionExecutionAudit actionExecutionAudit; 
 private ILogger log; 
 private ProducerConsumerModel producerconsumermodel; 

public ProducerConsumerModelAction (ActionExecutionAudit actionExecutionAudit, ILogger log, object producerconsumermodel){
	this.producerconsumermodel = (ProducerConsumerModel) producerconsumermodel ;
	this.actionExecutionAudit = actionExecutionAudit; 
	this.log = log; 
}
public void execute() { }
public bool executeIf() {return true;}
}

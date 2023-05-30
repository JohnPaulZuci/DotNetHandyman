using System;
using Serilog;
using ActionInterfaceNS;
using AuditDAONS;

[ActionExecution(ActionName = ConsumerAction.CONSUMER)]
public class ConsumerAction : IActionExecution {
public const string CONSUMER= "Consumer";
 private ActionExecutionAudit actionExecutionAudit; 
 private ILogger log; 
 private Consumer consumer; 

public ConsumerAction (ActionExecutionAudit actionExecutionAudit, ILogger log, object consumer){
	this.consumer = (Consumer) consumer ;
	this.actionExecutionAudit = actionExecutionAudit; 
	this.log = log; 
}
public void execute() { }
public bool executeIf() {return true;}
}

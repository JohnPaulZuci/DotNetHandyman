using System;
using Serilog;
using ActionInterfaceNS;
using AuditDAONS;

[ActionExecution(ActionName = ZeroShotClassifierAction.ZEROSHOTCLASSIFIER)]
public class ZeroShotClassifierAction : IActionExecution {
public const string ZEROSHOTCLASSIFIER= "ZeroShotClassifier";
 private ActionExecutionAudit actionExecutionAudit; 
 private ILogger log; 
 private ZeroShotClassifier zeroshotclassifier; 

public ZeroShotClassifierAction (ActionExecutionAudit actionExecutionAudit, ILogger log, object zeroshotclassifier){
	this.zeroshotclassifier = (ZeroShotClassifier) zeroshotclassifier ;
	this.actionExecutionAudit = actionExecutionAudit; 
	this.log = log; 
}
public void execute() { }
public bool executeIf() {return true;}
}

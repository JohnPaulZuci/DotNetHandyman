using System;
using Serilog;
using ActionInterfaceNS;
using AuditDAONS;

[ActionExecution(ActionName = DogLegAction.DOGLEG)]
public class DogLegAction : IActionExecution {
public const string DOGLEG= "DogLeg";
 private ActionExecutionAudit actionExecutionAudit; 
 private ILogger log; 
 private DogLeg dogleg; 

public DogLegAction (ActionExecutionAudit actionExecutionAudit, ILogger log, object dogleg){
	this.dogleg = (DogLeg) dogleg ;
	this.actionExecutionAudit = actionExecutionAudit; 
	this.log = log; 
}
public void execute() { }
public bool executeIf() {return true;}
}

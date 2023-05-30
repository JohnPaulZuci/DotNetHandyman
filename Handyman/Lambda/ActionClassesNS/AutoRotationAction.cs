using System;
using Serilog;
using ActionInterfaceNS;
using AuditDAONS;

[ActionExecution(ActionName = AutoRotationAction.AUTOROTATION)]
public class AutoRotationAction : IActionExecution {
public const string AUTOROTATION= "AutoRotation";
 private ActionExecutionAudit actionExecutionAudit; 
 private ILogger log; 
 private AutoRotation autorotation; 

public AutoRotationAction (ActionExecutionAudit actionExecutionAudit, ILogger log, object autorotation){
	this.autorotation = (AutoRotation) autorotation ;
	this.actionExecutionAudit = actionExecutionAudit; 
	this.log = log; 
}
public void execute() { 
	
}
public bool executeIf() {return true;}
}

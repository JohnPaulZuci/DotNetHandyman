using System;
using Serilog;
using ActionInterfaceNS;
using AuditDAONS;

[ActionExecution(ActionName = EntityFilterAction.ENTITYFILTER)]
public class EntityFilterAction : IActionExecution {
public const string ENTITYFILTER= "EntityFilter";
 private ActionExecutionAudit actionExecutionAudit; 
 private ILogger log; 
 private EntityFilter entityfilter; 

public EntityFilterAction (ActionExecutionAudit actionExecutionAudit, ILogger log, object entityfilter){
	this.entityfilter = (EntityFilter) entityfilter ;
	this.actionExecutionAudit = actionExecutionAudit; 
	this.log = log; 
}
public void execute() { }
public bool executeIf() {return true;}
}

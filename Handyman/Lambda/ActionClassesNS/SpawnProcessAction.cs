using System;
using Serilog;
using ActionInterfaceNS;
using AuditDAONS;

[ActionExecution(ActionName = SpawnProcessAction.SPAWNPROCESS)]
public class SpawnProcessAction : IActionExecution {
public const string SPAWNPROCESS= "SpawnProcess";
 private ActionExecutionAudit actionExecutionAudit; 
 private ILogger log; 
 private SpawnProcess spawnprocess; 

public SpawnProcessAction (ActionExecutionAudit actionExecutionAudit, ILogger log, object spawnprocess){
	this.spawnprocess = (SpawnProcess) spawnprocess ;
	this.actionExecutionAudit = actionExecutionAudit; 
	this.log = log; 
}
public void execute() { }
public bool executeIf() {return true;}
}

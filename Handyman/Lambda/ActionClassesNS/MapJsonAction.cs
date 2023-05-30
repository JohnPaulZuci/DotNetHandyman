using System;
using Serilog;
using ActionInterfaceNS;
using AuditDAONS;

[ActionExecution(ActionName = MapJsonAction.MAPJSON)]
public class MapJsonAction : IActionExecution {
public const string MAPJSON= "MapJson";
 private ActionExecutionAudit actionExecutionAudit; 
 private ILogger log; 
 private MapJson mapjson; 

public MapJsonAction (ActionExecutionAudit actionExecutionAudit, ILogger log, object mapjson){
	this.mapjson = (MapJson) mapjson ;
	this.actionExecutionAudit = actionExecutionAudit; 
	this.log = log; 
}
public void execute() { }
public bool executeIf() {return true;}
}

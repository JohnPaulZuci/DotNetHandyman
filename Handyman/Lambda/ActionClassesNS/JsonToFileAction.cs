using System;
using Serilog;
using ActionInterfaceNS;
using AuditDAONS;

[ActionExecution(ActionName = JsonToFileAction.JSONTOFILE)]
public class JsonToFileAction : IActionExecution {
public const string JSONTOFILE= "JsonToFile";
 private ActionExecutionAudit actionExecutionAudit; 
 private ILogger log; 
 private JsonToFile jsontofile; 

public JsonToFileAction (ActionExecutionAudit actionExecutionAudit, ILogger log, object jsontofile){
	this.jsontofile = (JsonToFile) jsontofile ;
	this.actionExecutionAudit = actionExecutionAudit; 
	this.log = log; 
}
public void execute() { }
public bool executeIf() {return true;}
}

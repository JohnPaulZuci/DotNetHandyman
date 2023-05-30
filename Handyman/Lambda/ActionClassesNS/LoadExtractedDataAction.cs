using System;
using Serilog;
using ActionInterfaceNS;
using AuditDAONS;

[ActionExecution(ActionName = LoadExtractedDataAction.LOADEXTRACTEDDATA)]
public class LoadExtractedDataAction : IActionExecution {
public const string LOADEXTRACTEDDATA= "LoadExtractedData";
 private ActionExecutionAudit actionExecutionAudit; 
 private ILogger log; 
 private LoadExtractedData loadextracteddata; 

public LoadExtractedDataAction (ActionExecutionAudit actionExecutionAudit, ILogger log, object loadextracteddata){
	this.loadextracteddata = (LoadExtractedData) loadextracteddata ;
	this.actionExecutionAudit = actionExecutionAudit; 
	this.log = log; 
}
public void execute() { }
public bool executeIf() {return true;}
}

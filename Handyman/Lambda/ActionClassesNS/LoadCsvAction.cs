using System;
using Serilog;
using ActionInterfaceNS;
using AuditDAONS;

[ActionExecution(ActionName = LoadCsvAction.LOADCSV)]
public class LoadCsvAction : IActionExecution {
public const string LOADCSV= "LoadCsv";
 private ActionExecutionAudit actionExecutionAudit; 
 private ILogger log; 
 private LoadCsv loadcsv; 

public LoadCsvAction (ActionExecutionAudit actionExecutionAudit, ILogger log, object loadcsv){
	this.loadcsv = (LoadCsv) loadcsv ;
	this.actionExecutionAudit = actionExecutionAudit; 
	this.log = log; 
}
public void execute() { }
public bool executeIf() {return true;}
}

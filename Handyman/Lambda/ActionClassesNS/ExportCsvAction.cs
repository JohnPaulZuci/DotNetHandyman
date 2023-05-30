using System;
using Serilog;
using ActionInterfaceNS;
using AuditDAONS;

[ActionExecution(ActionName = ExportCsvAction.EXPORTCSV)]
public class ExportCsvAction : IActionExecution {
public const string EXPORTCSV= "ExportCsv";
 private ActionExecutionAudit actionExecutionAudit; 
 private ILogger log; 
 private ExportCsv exportcsv; 

public ExportCsvAction (ActionExecutionAudit actionExecutionAudit, ILogger log, object exportcsv){
	this.exportcsv = (ExportCsv) exportcsv ;
	this.actionExecutionAudit = actionExecutionAudit; 
	this.log = log; 
}
public void execute() { }
public bool executeIf() {return true;}
}

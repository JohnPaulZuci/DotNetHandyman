using System;
using Serilog;
using ActionInterfaceNS;
using AuditDAONS;

[ActionExecution(ActionName = ImportCsvToDBAction.IMPORTCSVTODB)]
public class ImportCsvToDBAction : IActionExecution {
public const string IMPORTCSVTODB= "ImportCsvToDB";
 private ActionExecutionAudit actionExecutionAudit; 
 private ILogger log; 
 private ImportCsvToDB importcsvtodb; 

public ImportCsvToDBAction (ActionExecutionAudit actionExecutionAudit, ILogger log, object importcsvtodb){
	this.importcsvtodb = (ImportCsvToDB) importcsvtodb ;
	this.actionExecutionAudit = actionExecutionAudit; 
	this.log = log; 
}
public void execute() { }
public bool executeIf() {return true;}
}

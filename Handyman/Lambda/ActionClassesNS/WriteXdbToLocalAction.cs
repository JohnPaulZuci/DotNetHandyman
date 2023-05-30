using System;
using Serilog;
using ActionInterfaceNS;
using AuditDAONS;

[ActionExecution(ActionName = WriteXdbToLocalAction.WRITEXDBTOLOCAL)]
public class WriteXdbToLocalAction : IActionExecution {
public const string WRITEXDBTOLOCAL= "WriteXdbToLocal";
 private ActionExecutionAudit actionExecutionAudit; 
 private ILogger log; 
 private WriteXdbToLocal writexdbtolocal; 

public WriteXdbToLocalAction (ActionExecutionAudit actionExecutionAudit, ILogger log, object writexdbtolocal){
	this.writexdbtolocal = (WriteXdbToLocal) writexdbtolocal ;
	this.actionExecutionAudit = actionExecutionAudit; 
	this.log = log; 
}
public void execute() { }
public bool executeIf() {return true;}
}

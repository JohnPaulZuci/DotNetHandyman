using System;
using Serilog;
using ActionInterfaceNS;
using AuditDAONS;

[ActionExecution(ActionName = UpdateLogFileAction.UPDATELOGFILE)]
public class UpdateLogFileAction : IActionExecution {
public const string UPDATELOGFILE= "UpdateLogFile";
 private ActionExecutionAudit actionExecutionAudit; 
 private ILogger log; 
 private UpdateLogFile updatelogfile; 

public UpdateLogFileAction (ActionExecutionAudit actionExecutionAudit, ILogger log, object updatelogfile){
	this.updatelogfile = (UpdateLogFile) updatelogfile ;
	this.actionExecutionAudit = actionExecutionAudit; 
	this.log = log; 
}
public void execute() { }
public bool executeIf() {return true;}
}

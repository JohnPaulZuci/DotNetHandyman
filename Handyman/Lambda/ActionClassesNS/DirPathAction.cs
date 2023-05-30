using System;
using Serilog;
using ActionInterfaceNS;
using AuditDAONS;

[ActionExecution(ActionName = DirPathAction.DIRPATH)]
public class DirPathAction : IActionExecution {
public const string DIRPATH= "DirPath";
 private ActionExecutionAudit actionExecutionAudit; 
 private ILogger log; 
 private DirPath dirpath; 

public DirPathAction (ActionExecutionAudit actionExecutionAudit, ILogger log, object dirpath){
	this.dirpath = (DirPath) dirpath ;
	this.actionExecutionAudit = actionExecutionAudit; 
	this.log = log; 
}
public void execute() { }
public bool executeIf() {return true;}
}

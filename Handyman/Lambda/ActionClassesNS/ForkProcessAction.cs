using System;
using Serilog;
using ActionInterfaceNS;
using AuditDAONS;

[ActionExecution(ActionName = ForkProcessAction.FORKPROCESS)]
public class ForkProcessAction : IActionExecution {
public const string FORKPROCESS= "ForkProcess";
 private ActionExecutionAudit actionExecutionAudit; 
 private ILogger log; 
 private ForkProcess forkprocess; 

public ForkProcessAction (ActionExecutionAudit actionExecutionAudit, ILogger log, object forkprocess){
	this.forkprocess = (ForkProcess) forkprocess ;
	this.actionExecutionAudit = actionExecutionAudit; 
	this.log = log; 
}
public void execute() { }
public bool executeIf() {return true;}
}

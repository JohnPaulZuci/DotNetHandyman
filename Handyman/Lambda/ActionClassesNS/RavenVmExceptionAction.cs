using System;
using Serilog;
using ActionInterfaceNS;
using AuditDAONS;

[ActionExecution(ActionName = RavenVmExceptionAction.RAVENVMEXCEPTION)]
public class RavenVmExceptionAction : IActionExecution {
public const string RAVENVMEXCEPTION= "RavenVmException";
 private ActionExecutionAudit actionExecutionAudit; 
 private ILogger log; 
 private RavenVmException ravenvmexception; 

public RavenVmExceptionAction (ActionExecutionAudit actionExecutionAudit, ILogger log, object ravenvmexception){
	this.ravenvmexception = (RavenVmException) ravenvmexception ;
	this.actionExecutionAudit = actionExecutionAudit; 
	this.log = log; 
}
public void execute() { }
public bool executeIf() {return true;}
}

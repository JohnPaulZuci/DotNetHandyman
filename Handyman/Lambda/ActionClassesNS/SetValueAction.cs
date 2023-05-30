using System;
using Serilog;
using ActionInterfaceNS;
using AuditDAONS;

[ActionExecution(ActionName = SetValueAction.SETVALUE)]
public class SetValueAction : IActionExecution {
public const string SETVALUE= "SetValue";
 private ActionExecutionAudit actionExecutionAudit; 
 private ILogger log; 
 private SetValue setvalue; 

public SetValueAction (ActionExecutionAudit actionExecutionAudit, ILogger log, object setvalue){
	this.setvalue = (SetValue) setvalue ;
	this.actionExecutionAudit = actionExecutionAudit; 
	this.log = log; 
}
public void execute() { }
public bool executeIf() {return true;}
}

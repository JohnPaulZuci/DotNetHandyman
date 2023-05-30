using System;
using Serilog;
using ActionInterfaceNS;
using AuditDAONS;

[ActionExecution(ActionName = EvalMemberIdAction.EVALMEMBERID)]
public class EvalMemberIdAction : IActionExecution {
public const string EVALMEMBERID= "EvalMemberId";
 private ActionExecutionAudit actionExecutionAudit; 
 private ILogger log; 
 private EvalMemberId evalmemberid; 

public EvalMemberIdAction (ActionExecutionAudit actionExecutionAudit, ILogger log, object evalmemberid){
	this.evalmemberid = (EvalMemberId) evalmemberid ;
	this.actionExecutionAudit = actionExecutionAudit; 
	this.log = log; 
}
public void execute() { }
public bool executeIf() {return true;}
}

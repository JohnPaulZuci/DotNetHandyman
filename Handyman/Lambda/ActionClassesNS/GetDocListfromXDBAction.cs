using System;
using Serilog;
using ActionInterfaceNS;
using AuditDAONS;

[ActionExecution(ActionName = GetDocListfromXDBAction.GETDOCLISTFROMXDB)]
public class GetDocListfromXDBAction : IActionExecution {
public const string GETDOCLISTFROMXDB= "GetDocListfromXDB";
 private ActionExecutionAudit actionExecutionAudit; 
 private ILogger log; 
 private GetDocListfromXDB getdoclistfromxdb; 

public GetDocListfromXDBAction (ActionExecutionAudit actionExecutionAudit, ILogger log, object getdoclistfromxdb){
	this.getdoclistfromxdb = (GetDocListfromXDB) getdoclistfromxdb ;
	this.actionExecutionAudit = actionExecutionAudit; 
	this.log = log; 
}
public void execute() { }
public bool executeIf() {return true;}
}

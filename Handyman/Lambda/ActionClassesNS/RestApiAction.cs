using System;
using Serilog;
using ActionInterfaceNS;
using AuditDAONS;

[ActionExecution(ActionName = RestApiAction.RESTAPI)]
public class RestApiAction : IActionExecution {
public const string RESTAPI= "RestApi";
 private ActionExecutionAudit actionExecutionAudit; 
 private ILogger log; 
 private RestApi restapi; 

public RestApiAction (ActionExecutionAudit actionExecutionAudit, ILogger log, object restapi){
	this.restapi = (RestApi) restapi ;
	this.actionExecutionAudit = actionExecutionAudit; 
	this.log = log; 
}
public void execute() { }
public bool executeIf() {return true;}
}

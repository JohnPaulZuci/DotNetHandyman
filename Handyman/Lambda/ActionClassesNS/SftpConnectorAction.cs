using System;
using Serilog;
using ActionInterfaceNS;
using AuditDAONS;

[ActionExecution(ActionName = SftpConnectorAction.SFTPCONNECTOR)]
public class SftpConnectorAction : IActionExecution {
public const string SFTPCONNECTOR= "SftpConnector";
 private ActionExecutionAudit actionExecutionAudit; 
 private ILogger log; 
 private SftpConnector sftpconnector; 

public SftpConnectorAction (ActionExecutionAudit actionExecutionAudit, ILogger log, object sftpconnector){
	this.sftpconnector = (SftpConnector) sftpconnector ;
	this.actionExecutionAudit = actionExecutionAudit; 
	this.log = log; 
}
public void execute() { }
public bool executeIf() {return true;}
}

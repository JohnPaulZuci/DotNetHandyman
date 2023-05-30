using System;
using Serilog;
using ActionInterfaceNS;
using AuditDAONS;

[ActionExecution(ActionName = QrAttributionAction.QRATTRIBUTION)]
public class QrAttributionAction : IActionExecution {
public const string QRATTRIBUTION= "QrAttribution";
 private ActionExecutionAudit actionExecutionAudit; 
 private ILogger log; 
 private QrAttribution qrattribution; 

public QrAttributionAction (ActionExecutionAudit actionExecutionAudit, ILogger log, object qrattribution){
	this.qrattribution = (QrAttribution) qrattribution ;
	this.actionExecutionAudit = actionExecutionAudit; 
	this.log = log; 
}
public void execute() { }
public bool executeIf() {return true;}
}

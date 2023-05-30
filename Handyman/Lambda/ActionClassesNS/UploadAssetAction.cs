using System;
using Serilog;
using ActionInterfaceNS;
using AuditDAONS;

[ActionExecution(ActionName = UploadAssetAction.UPLOADASSET)]
public class UploadAssetAction : IActionExecution {
public const string UPLOADASSET= "UploadAsset";
 private ActionExecutionAudit actionExecutionAudit; 
 private ILogger log; 
 private UploadAsset uploadasset; 

public UploadAssetAction (ActionExecutionAudit actionExecutionAudit, ILogger log, object uploadasset){
	this.uploadasset = (UploadAsset) uploadasset ;
	this.actionExecutionAudit = actionExecutionAudit; 
	this.log = log; 
}
public void execute() { }
public bool executeIf() {return true;}
}

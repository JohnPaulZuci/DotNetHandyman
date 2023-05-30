using System;
using Serilog;
using ActionInterfaceNS;
using AuditDAONS;

[ActionExecution(ActionName = DownloadAssetAction.DOWNLOADASSET)]
public class DownloadAssetAction : IActionExecution {
public const string DOWNLOADASSET= "DownloadAsset";
 private ActionExecutionAudit actionExecutionAudit; 
 private ILogger log; 
 private DownloadAsset downloadasset; 

public DownloadAssetAction (ActionExecutionAudit actionExecutionAudit, ILogger log, object downloadasset){
	this.downloadasset = (DownloadAsset) downloadasset ;
	this.actionExecutionAudit = actionExecutionAudit; 
	this.log = log; 
}
public void execute() { }
public bool executeIf() {return true;}
}

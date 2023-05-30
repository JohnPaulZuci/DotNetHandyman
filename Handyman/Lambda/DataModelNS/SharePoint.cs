using System;
using ActionInterfaceNS;
[ActionContext(ActionName = "SharePoint")] 
public class SharePoint : IActionContext {
public string name { get; set; }
public string shpClientId { get; set; }
public string shpTenantId { get; set; }
public string shpClientSecret { get; set; }
public string orgName { get; set; }
public string actionType { get; set; }
public string siteUrl { get; set; }
public string sourceRelativePath { get; set; }
public string fileName { get; set; }
public string targetRelativePath { get; set; }
public string value { get; set; }
public bool condition { get; set; }
public string children { get; set; }
public string exception { get; set; }
public string invokingState { get; set; }
}

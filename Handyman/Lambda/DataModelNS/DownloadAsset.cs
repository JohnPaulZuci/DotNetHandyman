using System;
using ActionInterfaceNS;
[ActionContext(ActionName = "DownloadAsset")] 
public class DownloadAsset : IActionContext {
public string name { get; set; }
public string url { get; set; }
public string location { get; set; }
public bool condition { get; set; }
public string children { get; set; }
public string exception { get; set; }
public string invokingState { get; set; }
}

using System;
using ActionInterfaceNS;
[ActionContext(ActionName = "UploadAsset")] 
public class UploadAsset : IActionContext {
public string name { get; set; }
public string filePath { get; set; }
public string templateId { get; set; }
public string token { get; set; }
public bool condition { get; set; }
public string children { get; set; }
public string exception { get; set; }
public string invokingState { get; set; }
}

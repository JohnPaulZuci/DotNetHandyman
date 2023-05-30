using System;
using ActionInterfaceNS;
[ActionContext(ActionName = "AutoRotation")] 
public class AutoRotation : IActionContext {
public string name { get; set; }
public string filePath { get; set; }
public string outputDir { get; set; }
public bool condition { get; set; }
public string children { get; set; }
public string exception { get; set; }
public string invokingState { get; set; }
}

using System;
using ActionInterfaceNS;
[ActionContext(ActionName = "TextFilter")] 
public class TextFilter : IActionContext {
public string name { get; set; }
public string filteringKeys { get; set; }
public string inputFilePath { get; set; }
public bool condition { get; set; }
public string children { get; set; }
public string exception { get; set; }
public string invokingState { get; set; }
}

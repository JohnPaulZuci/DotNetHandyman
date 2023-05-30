using System;
using ActionInterfaceNS;
[ActionContext(ActionName = "SorFilter")] 
public class SorFilter : IActionContext {
public string name { get; set; }
public string filePath { get; set; }
public string inticsReferenceId { get; set; }
public string searchValue { get; set; }
public bool condition { get; set; }
public string children { get; set; }
public string exception { get; set; }
public string invokingState { get; set; }
}

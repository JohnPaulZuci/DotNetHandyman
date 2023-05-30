using System;
using ActionInterfaceNS;
[ActionContext(ActionName = "ExportCsv")] 
public class ExportCsv : IActionContext {
public string name { get; set; }
public string source { get; set; }
public string executionSource { get; set; }
public string stmt { get; set; }
public string targetLocation { get; set; }
public bool condition { get; set; }
public string writeThreadCount { get; set; }
public string children { get; set; }
public string exception { get; set; }
public string invokingState { get; set; }
}

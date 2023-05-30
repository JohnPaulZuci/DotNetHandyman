using System;
using ActionInterfaceNS;
[ActionContext(ActionName = "LoadCsv")] 
public class LoadCsv : IActionContext {
public string name { get; set; }
public string pid { get; set; }
public string source { get; set; }
public string to { get; set; }
public string delim { get; set; }
public string limit { get; set; }
public string value { get; set; }
public bool condition { get; set; }
public string children { get; set; }
public string exception { get; set; }
public string invokingState { get; set; }
}

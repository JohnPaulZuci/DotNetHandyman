using System;
using ActionInterfaceNS;
[ActionContext(ActionName = "SpawnProcess")] 
public class SpawnProcess : IActionContext {
public string name { get; set; }
public string target { get; set; }
public string source { get; set; }
public bool condition { get; set; }
public string children { get; set; }
public string exception { get; set; }
public string invokingState { get; set; }
}

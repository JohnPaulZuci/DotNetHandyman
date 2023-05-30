using System;
using ActionInterfaceNS;
[ActionContext(ActionName = "Multitude")] 
public class Multitude : IActionContext {
public string name { get; set; }
public string on { get; set; }
public string _action { get; set; }
public string _actions { get; set; }
public bool condition { get; set; }
public string writeThreadCount { get; set; }
public string children { get; set; }
public string exception { get; set; }
public string invokingState { get; set; }
}

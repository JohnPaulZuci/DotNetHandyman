using System;
using ActionInterfaceNS;
[ActionContext(ActionName = "Producer")] 
public class Producer : IActionContext {
public string name { get; set; }
public string push { get; set; }
public ConfigNS.SpwResourceConfig source { get; set; }
public string stmt { get; set; }
public string _action { get; set; }
public string _actions { get; set; }
public bool condition { get; set; }
public string threadCount { get; set; }
public string children { get; set; }
public string exception { get; set; }
public string invokingState { get; set; }
}

using System;
using ActionInterfaceNS;
[ActionContext(ActionName = "Consumer")] 
public class Consumer : IActionContext {
public string name { get; set; }
public string events { get; set; }
public ConfigNS.SpwResourceConfig source { get; set; }
public string pop { get; set; }
public string limit { get; set; }
public string _action { get; set; }
public string _actions { get; set; }
public bool condition { get; set; }
public string threadCount { get; set; }
public bool standalone { get; set; }
public string children { get; set; }
public string exception { get; set; }
public string invokingState { get; set; }
}

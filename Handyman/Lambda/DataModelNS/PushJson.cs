using System;
using ActionInterfaceNS;
[ActionContext(ActionName = "PushJson")] 
public class PushJson : IActionContext {
public string name { get; set; }
public string key { get; set; }
public Newtonsoft.Json.Linq.JToken value { get; set; }
public bool condition { get; set; }
public string children { get; set; }
public string exception { get; set; }
public string invokingState { get; set; }
}

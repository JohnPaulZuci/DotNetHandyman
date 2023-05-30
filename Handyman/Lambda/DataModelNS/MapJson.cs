using System;
using ActionInterfaceNS;
[ActionContext(ActionName = "MapJson")] 
public class MapJson : IActionContext {
public string name { get; set; }
public string value { get; set; }
public bool condition { get; set; }
public string children { get; set; }
public string exception { get; set; }
public string invokingState { get; set; }
}

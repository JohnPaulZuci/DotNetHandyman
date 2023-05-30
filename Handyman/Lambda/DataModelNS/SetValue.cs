using System;
using ActionInterfaceNS;
[ActionContext(ActionName = "SetValue")] 
public class SetValue : IActionContext {
public string name { get; set; }
public string contextKey { get; set; }
public string contextValue { get; set; }
public bool condition { get; set; }
public string children { get; set; }
public string exception { get; set; }
public string invokingState { get; set; }
}

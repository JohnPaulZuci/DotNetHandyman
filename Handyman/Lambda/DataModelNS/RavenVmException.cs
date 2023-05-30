using System;
using ActionInterfaceNS;
[ActionContext(ActionName = "RavenVmException")] 
public class RavenVmException : IActionContext {
public string name { get; set; }
public string message { get; set; }
public bool condition { get; set; }
public string children { get; set; }
public string exception { get; set; }
public string invokingState { get; set; }
}

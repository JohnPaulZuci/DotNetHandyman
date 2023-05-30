using System;
using ActionInterfaceNS;
[ActionContext(ActionName = "Transform")] 
public class Transform : IActionContext {
public string name { get; set; }
public string on { get; set; }
public string _STRING { get; set; }
public IList<string> _value { get; set; }
public bool condition { get; set; }
public bool format { get; set; }
public string children { get; set; }
public string exception { get; set; }
public string invokingState { get; set; }
}

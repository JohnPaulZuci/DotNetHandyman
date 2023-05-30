using System;
using ActionInterfaceNS;
[ActionContext(ActionName = "SorGroupDetails")] 
public class SorGroupDetails : IActionContext {
public string name { get; set; }
public string on { get; set; }
public string keyfields { get; set; }
public string searchfields { get; set; }
public string groupbyfields { get; set; }
public string targettable { get; set; }
public string value { get; set; }
public bool condition { get; set; }
public string children { get; set; }
public string exception { get; set; }
public string invokingState { get; set; }
}

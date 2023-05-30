using System;
using ActionInterfaceNS;
[ActionContext(ActionName = "CreateFile")] 
public class CreateFile : IActionContext {
public string name { get; set; }
public string location { get; set; }
public string fileName { get; set; }
public string extension { get; set; }
public string value { get; set; }
public bool condition { get; set; }
public string children { get; set; }
public string exception { get; set; }
public string invokingState { get; set; }
}

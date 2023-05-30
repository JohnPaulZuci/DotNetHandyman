using System;
using ActionInterfaceNS;
[ActionContext(ActionName = "CreateTAR")] 
public class CreateTAR : IActionContext {
public string name { get; set; }
public string source { get; set; }
public string destination { get; set; }
public string extension { get; set; }
public bool condition { get; set; }
public string children { get; set; }
public string exception { get; set; }
public string invokingState { get; set; }
}

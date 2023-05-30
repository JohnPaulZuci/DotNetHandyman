using System;
using ActionInterfaceNS;
[ActionContext(ActionName = "DogLeg")] 
public class DogLeg : IActionContext {
public string name { get; set; }
public string inheritContext { get; set; }
public string _startProcess { get; set; }
public string _processList { get; set; }
public bool condition { get; set; }
public string children { get; set; }
public string exception { get; set; }
public string invokingState { get; set; }
}

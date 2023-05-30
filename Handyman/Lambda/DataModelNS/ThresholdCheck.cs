using System;
using ActionInterfaceNS;
[ActionContext(ActionName = "ThresholdCheck")] 
public class ThresholdCheck : IActionContext {
public string name { get; set; }
public string threshold { get; set; }
public string input { get; set; }
public bool condition { get; set; }
public string children { get; set; }
public string exception { get; set; }
public string invokingState { get; set; }
}

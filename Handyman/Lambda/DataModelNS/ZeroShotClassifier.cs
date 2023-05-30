using System;
using ActionInterfaceNS;
[ActionContext(ActionName = "ZeroShotClassifier")] 
public class ZeroShotClassifier : IActionContext {
public string name { get; set; }
public string candidateLabels { get; set; }
public string content { get; set; }
public bool condition { get; set; }
public string children { get; set; }
public string exception { get; set; }
public string invokingState { get; set; }
}

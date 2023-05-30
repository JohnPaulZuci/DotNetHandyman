using System;
using ActionInterfaceNS;
[ActionContext(ActionName = "EvalDateOfBirth")] 
public class EvalDateOfBirth : IActionContext {
public string name { get; set; }
public string dob { get; set; }
public string wordCountLimit { get; set; }
public string charCountLimit { get; set; }
public string wordCountThreshold { get; set; }
public string charCountThreshold { get; set; }
public string comparableYear { get; set; }
public string dateFormats { get; set; }
public string validatorThreshold { get; set; }
public bool condition { get; set; }
public string children { get; set; }
public string exception { get; set; }
public string invokingState { get; set; }
}

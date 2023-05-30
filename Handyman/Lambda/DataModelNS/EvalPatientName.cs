using System;
using ActionInterfaceNS;
[ActionContext(ActionName = "EvalPatientName")] 
public class EvalPatientName : IActionContext {
public string name { get; set; }
public string patientName { get; set; }
public string wordCountLimit { get; set; }
public string charCountLimit { get; set; }
public string nerCoproApi { get; set; }
public string wordCountThreshold { get; set; }
public string charCountThreshold { get; set; }
public string nerApiThreshold { get; set; }
public bool condition { get; set; }
public string children { get; set; }
public string exception { get; set; }
public string invokingState { get; set; }
}

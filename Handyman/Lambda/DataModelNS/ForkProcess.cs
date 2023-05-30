using System;
using ActionInterfaceNS;
[ActionContext(ActionName = "ForkProcess")] 
public class ForkProcess : IActionContext {
public string name { get; set; }
public string target { get; set; }
public string source { get; set; }
public string datasource { get; set; }
public string value { get; set; }
public string forkBatchSize { get; set; }
public bool condition { get; set; }
public string children { get; set; }
public string exception { get; set; }
public string invokingState { get; set; }
}

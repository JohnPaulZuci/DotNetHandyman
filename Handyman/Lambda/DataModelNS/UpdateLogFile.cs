using System;
using ActionInterfaceNS;
[ActionContext(ActionName = "UpdateLogFile")] 
public class UpdateLogFile : IActionContext {
public string name { get; set; }
public string source { get; set; }
public string logfile { get; set; }
public string value { get; set; }
public bool condition { get; set; }
public string writeThreadCount { get; set; }
public string fetchBatchSize { get; set; }
public string writeBatchSize { get; set; }
public string children { get; set; }
public string exception { get; set; }
public string invokingState { get; set; }
}

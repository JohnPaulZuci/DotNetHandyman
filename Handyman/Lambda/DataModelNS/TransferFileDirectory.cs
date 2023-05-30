using System;
using ActionInterfaceNS;
[ActionContext(ActionName = "TransferFileDirectory")] 
public class TransferFileDirectory : IActionContext {
public string name { get; set; }
public string source { get; set; }
public string to { get; set; }
public string operation { get; set; }
public bool condition { get; set; }
public string children { get; set; }
public string exception { get; set; }
public string invokingState { get; set; }
}

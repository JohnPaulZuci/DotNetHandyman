using System;
using ActionInterfaceNS;
[ActionContext(ActionName = "CreateDirectory")] 
public class CreateDirectory : IActionContext {
public string name { get; set; }
public string _STRING { get; set; }
public IList<string> _directoryPath { get; set; }
public bool condition { get; set; }
public string children { get; set; }
public string exception { get; set; }
public string invokingState { get; set; }
}

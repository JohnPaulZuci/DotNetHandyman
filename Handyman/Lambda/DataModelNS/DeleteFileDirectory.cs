using System;
using ActionInterfaceNS;
[ActionContext(ActionName = "DeleteFileDirectory")] 
public class DeleteFileDirectory : IActionContext {
public string name { get; set; }
public string _STRING { get; set; }
public IList<string> _path { get; set; }
public bool condition { get; set; }
public string children { get; set; }
public string exception { get; set; }
public string invokingState { get; set; }
}

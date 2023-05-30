using System;
using ActionInterfaceNS;
[ActionContext(ActionName = "FileDetails")] 
public class FileDetails : IActionContext {
public string name { get; set; }
public string dirpath { get; set; }
public string group_id { get; set; }
public string inbound_id { get; set; }
public string resourceConn { get; set; }
public bool condition { get; set; }
public string children { get; set; }
public string exception { get; set; }
public string invokingState { get; set; }
}

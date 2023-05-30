using System;
using ActionInterfaceNS;
[ActionContext(ActionName = "JsonToFile")] 
public class JsonToFile : IActionContext {
public string name { get; set; }
public string filePath { get; set; }
public string resourceConn { get; set; }
public string jsonSql { get; set; }
public bool condition { get; set; }
public string children { get; set; }
public string exception { get; set; }
public string invokingState { get; set; }
}

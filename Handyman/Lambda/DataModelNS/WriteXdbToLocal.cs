using System;
using ActionInterfaceNS;
[ActionContext(ActionName = "WriteXdbToLocal")] 
public class WriteXdbToLocal : IActionContext {
public string name { get; set; }
public string source { get; set; }
public string postgres { get; set; }
public string path { get; set; }
public string rootpath { get; set; }
public string sourceschema { get; set; }
public string sourcetable { get; set; }
public string destinationschema { get; set; }
public string destinationtable { get; set; }
public string sessionTimeout { get; set; }
public string batchSize { get; set; }
public string threadCount { get; set; }
public string value { get; set; }
public bool condition { get; set; }
public string children { get; set; }
public string exception { get; set; }
public string invokingState { get; set; }
}

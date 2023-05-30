using System;
using ActionInterfaceNS;
[ActionContext(ActionName = "GetDocListfromXDB")] 
public class GetDocListfromXDB : IActionContext {
public string name { get; set; }
public string source { get; set; }
public string rootpath { get; set; }
public string schema { get; set; }
public string table { get; set; }
public string status { get; set; }
public string processId { get; set; }
public string postgres { get; set; }
public string batchSize { get; set; }
public string value { get; set; }
public bool condition { get; set; }
public string children { get; set; }
public string exception { get; set; }
public string invokingState { get; set; }
}

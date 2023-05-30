using System;
using ActionInterfaceNS;
[ActionContext(ActionName = "DocnetResult")] 
public class DocnetResult : IActionContext {
public string name { get; set; }
public string resourceConn { get; set; }
public string coproResultSqlQuery { get; set; }
public string weightageSqlQuery { get; set; }
public bool condition { get; set; }
public string children { get; set; }
public string exception { get; set; }
public string invokingState { get; set; }
}

using System;
using ActionInterfaceNS;
[ActionContext(ActionName = "RestApi")] 
public class RestApi : IActionContext {
public string name { get; set; }
public string source { get; set; }
public string url { get; set; }
public string method { get; set; }
public Newtonsoft.Json.Linq.JToken headers { get; set; }
public Newtonsoft.Json.Linq.JToken param { get; set; }
public string bodyType { get; set; }
public string _restPart { get; set; }
public string _value { get; set; }
public bool condition { get; set; }
public string children { get; set; }
public string exception { get; set; }
public string invokingState { get; set; }
}

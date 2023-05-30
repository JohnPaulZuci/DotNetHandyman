using System;
using ActionInterfaceNS;
[ActionContext(ActionName = "ProducerConsumerModel")] 
public class ProducerConsumerModel : IActionContext {
public string name { get; set; }
public ConfigNS.SpwResourceConfig source { get; set; }
public string _producer { get; set; }
public string _produce { get; set; }
public string produceThreadCount { get; set; }
public string _consumer { get; set; }
public string _consume { get; set; }
public string consumeThreadCount { get; set; }
public bool condition { get; set; }
public string children { get; set; }
public string exception { get; set; }
public string invokingState { get; set; }
}

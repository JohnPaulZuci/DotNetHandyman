using System;
using ActionInterfaceNS;
[ActionContext(ActionName = "EntityFilter")] 
public class EntityFilter : IActionContext {
public string name { get; set; }
public string docId { get; set; }
public string paperNo { get; set; }
public string groupId { get; set; }
public string resourceConn { get; set; }
public string entityKeysToFilter { get; set; }
public string mandatoryKeysToFilter { get; set; }
public string inputFilePath { get; set; }
public bool condition { get; set; }
public string children { get; set; }
public string exception { get; set; }
public string invokingState { get; set; }
}

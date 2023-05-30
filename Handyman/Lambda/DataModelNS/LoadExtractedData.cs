using System;
using ActionInterfaceNS;
[ActionContext(ActionName = "LoadExtractedData")] 
public class LoadExtractedData : IActionContext {
public string name { get; set; }
public string filePath { get; set; }
public string paperNo { get; set; }
public string inticsReferenceId { get; set; }
public string batchId { get; set; }
public string targetDir { get; set; }
public bool condition { get; set; }
public string children { get; set; }
public string exception { get; set; }
public string invokingState { get; set; }
}

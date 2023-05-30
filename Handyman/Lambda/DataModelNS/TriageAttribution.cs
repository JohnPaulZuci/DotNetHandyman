using System;
using ActionInterfaceNS;
[ActionContext(ActionName = "TriageAttribution")] 
public class TriageAttribution : IActionContext {
public string name { get; set; }
public string outputDir { get; set; }
public string labelledClassifierModelFilePath { get; set; }
public string handwrittenClassifierModelFilePath { get; set; }
public string checkboxClassifierModelFilePath { get; set; }
public string synonyms { get; set; }
public string labelledClassifierLabels { get; set; }
public string viltCocoLabels { get; set; }
public string viltConfigLabel { get; set; }
public string isViltCocoOverride { get; set; }
public string viltCocoThreshold { get; set; }
public string vggImageWidth { get; set; }
public string vggImageHeight { get; set; }
public string triageAttributionResponseName { get; set; }
public string inputFilePath { get; set; }
public bool condition { get; set; }
public string children { get; set; }
public string exception { get; set; }
public string invokingState { get; set; }
}

using System;
using ActionInterfaceNS;
[ActionContext(ActionName = "UrgencyTriage")] 
public class UrgencyTriage : IActionContext {
public string name { get; set; }
public string inputFilePath { get; set; }
public string binaryClassifierModelFilePath { get; set; }
public string multiClassifierModelFilePath { get; set; }
public string checkboxClassifierModelFilePath { get; set; }
public string synonyms { get; set; }
public string binaryClassifierLabels { get; set; }
public string multiClassifierLabels { get; set; }
public string checkboxClassifierLabels { get; set; }
public string outputDir { get; set; }
public string binaryImageWidth { get; set; }
public string binaryImageHeight { get; set; }
public string multiImageWidth { get; set; }
public string multiImageHeight { get; set; }
public string checkboxImageWidth { get; set; }
public string checkboxImageHeight { get; set; }
public bool condition { get; set; }
public string children { get; set; }
public string exception { get; set; }
public string invokingState { get; set; }
}

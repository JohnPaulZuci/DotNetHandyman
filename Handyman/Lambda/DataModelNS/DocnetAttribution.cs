using System;
using ActionInterfaceNS;
[ActionContext(ActionName = "DocnetAttribution")] 
public class DocnetAttribution : IActionContext {
public string name { get; set; }
public string outputDir { get; set; }
public string docnetAttributionAsResponse { get; set; }
public string inputFilePath { get; set; }
public string resourceConn { get; set; }
public string attributeQuestionSql { get; set; }
public bool condition { get; set; }
public string children { get; set; }
public string exception { get; set; }
public string invokingState { get; set; }
}

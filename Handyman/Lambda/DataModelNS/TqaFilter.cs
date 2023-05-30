using System;
using ActionInterfaceNS;
[ActionContext(ActionName = "TqaFilter")] 
public class TqaFilter : IActionContext {
public string name { get; set; }
public string outputDir { get; set; }
public string truthExtractorUrl { get; set; }
public string maxDoctrDiff { get; set; }
public string maxQuestionDiff { get; set; }
public string resourceConn { get; set; }
public string synonymSqlQuery { get; set; }
public string inputFilePathSqlQuery { get; set; }
public bool condition { get; set; }
public string threadCount { get; set; }
public string fetchBatchSize { get; set; }
public string writeBatchSize { get; set; }
public string children { get; set; }
public string exception { get; set; }
public string invokingState { get; set; }
}

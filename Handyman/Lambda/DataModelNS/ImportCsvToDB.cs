using System;
using ActionInterfaceNS;
[ActionContext(ActionName = "ImportCsvToDB")] 
public class ImportCsvToDB : IActionContext {
public string name { get; set; }
public ConfigNS.SpwResourceConfig target { get; set; }
public string tableName { get; set; }
public string _STRING { get; set; }
public IList<string> _value { get; set; }
public bool condition { get; set; }
public string writeThreadCount { get; set; }
public string batchSize { get; set; }
public string children { get; set; }
public string exception { get; set; }
public string invokingState { get; set; }
}

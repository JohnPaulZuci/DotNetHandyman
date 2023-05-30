using System;
using ActionInterfaceNS;
[ActionContext(ActionName = "SftpConnector")] 
public class SftpConnector : IActionContext {
public string name { get; set; }
public string host { get; set; }
public string port { get; set; }
public string userName { get; set; }
public string password { get; set; }
public string sessionTimeOut { get; set; }
public string channelTimeOut { get; set; }
public string sourceFile { get; set; }
public string destDir { get; set; }
public bool condition { get; set; }
public string children { get; set; }
public string exception { get; set; }
public string invokingState { get; set; }
}

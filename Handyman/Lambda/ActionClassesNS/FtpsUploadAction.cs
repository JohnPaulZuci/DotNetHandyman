using System;
using System.Net;
using Serilog;
using FluentFTP;


using ActionInterfaceNS;
using AuditDAONS;

[ActionExecution(ActionName = FtpsUploadAction.FTPSUPLOAD)]
public class FtpsUploadAction : IActionExecution
{
    public const string FTPSUPLOAD = "FtpsUpload";
    private ActionExecutionAudit actionExecutionAudit;
    private ILogger log;
    private FtpsUpload ftpsupload;

    public FtpsUploadAction(ActionExecutionAudit actionExecutionAudit, ILogger log, object ftpsupload)
    {
        this.ftpsupload = (FtpsUpload)ftpsupload;
        this.actionExecutionAudit = actionExecutionAudit;
        this.log = log;
    }
    public void execute()
    {
        try
        {
            int sessionTimeout = int.Parse(ftpsupload.sessionTimeOut);
            string destDir = ftpsupload.destDir;
            string remoteFile = ftpsupload.sourceFile;
            string check = ftpsupload.uploadCheck;

            UploadFile(remoteFile, destDir);

        }
        catch (WebException ex)
        {
            log.Error($"FTP upload error: {ex.Message}");
        }
    }

    public async void UploadFile(string localFilePath, string remoteFilePath)
    {
        await UploadFileAsync(localFilePath, remoteFilePath);
        log.Information("File upload completed.");
    }

    public async Task UploadFileAsync(string localFilePath, string remoteFilePath)
    {
        try
        {
            // Create a FTP request object
            string ftpUrl = "ftp://" + ftpsupload.host;
            using (FtpClient client = new FtpClient(ftpUrl, ftpsupload.userName, ftpsupload.password))
            {
                client.Connect();

                if (client.IsConnected)
                {
                    client.Config.ConnectTimeout = Convert.ToInt32(ftpsupload.sessionTimeOut);
                    var source = ftpsupload.sourceFile;
                    var destDir = ftpsupload.destDir;
                    if (File.Exists(source))
                    {
                        // Transfer a single file
                        client.UploadFile(source, destDir);
                        log.Information("File uploaded successfully.");
                    }
                    else if (Directory.Exists(source))
                    {
                        // Transfer a folder (including subdirectories and files)
                        client.UploadDirectory(source, destDir, FtpFolderSyncMode.Update);
                        log.Information("Folder uploaded successfully.");
                    }
                    else
                    {
                        log.Information("Source path does not exist.");
                    }
                }
                else
                {
                    log.Information("Failed to connect to the FTP server.");
                }
            }
        }

        catch (Exception ex)
        {
            log.Error($"FTP upload error: {ex.Message}");
        }
    }
    public bool executeIf()
    {
        return ftpsupload.condition;
    }
}

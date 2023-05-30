using AccessConfigNS;

public class LContext
{
    public long? RootPipelineId { get; set; }
    //Load Enum
    public string ProcessLoadType { get; set; }
    // Config name
    public string PipelineName { get; set; }
    // Auto set
    private string lambdaName;
    // Start lambda Engine by path file
    public string? RelativePath { get; set; }
    public Dictionary<string, string> InheritedContext = new Dictionary<string, string>();

    public long? ParentPipelineId { get; set; }
    public string? ParentPipelineName { get; set; }

    public long ParentActionId { get; set; }
    public string? ParentActionName { get; set; }

    public string? Payload { get; set; }

    public long UserId { get; set; }

    public string LambdaName
    {
        get
        {
            return lambdaName ?? ConfigAccess.GetLambdaName(this.PipelineName);
        }
        set
        {
            lambdaName = value;
        }
    }

    public static explicit operator LContext(string v)
    {
        throw new NotImplementedException();
    }
}
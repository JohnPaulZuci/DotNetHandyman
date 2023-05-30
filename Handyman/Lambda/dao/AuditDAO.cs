using DoaActionsNS;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Serilog;
using Serilog.Events;
using ActorOperationNS;

namespace AuditDAONS
{

    //TODO add extra methods
    public class ActionExecutionAudit : AbstractAudit, IAction
    {
        [JsonIgnore]
        private readonly Queue<LogEvent> eventQueue = new Queue<LogEvent>();

        [JsonProperty("actionId")]
        public long ActionId { get; set; } = BitConverter.ToInt64(Guid.NewGuid().ToByteArray(), 0);
        [JsonProperty("actionName")]
        public string ActionName { get; set; }

        [JsonProperty("macroName")]
        public string MacroName { get; set; }

        [JsonProperty("pipelineId")]
        public long PipelineId { get; set; }

        [JsonProperty("input")]
        public JToken Input { get; set; }

        [JsonProperty("log")]
        public string Log { get; set; }

        [JsonProperty("executionGroupId")]
        public int ExecutionGroupId { get; set; }

        [JsonProperty("threadName")]
        public string ThreadName { get; set; }

        [JsonIgnore]
        public string InputNode => Input?.ToString();

        public void UpdateExecutionStatusId(ExecutionStatus executionStatusId)
        {
            this.ExecutionStatusId = (int)executionStatusId;
            ActionExecutionStatusAudit actionExecutionStatusAudit = new ActionExecutionStatusAudit()
            {
                ActionId = this.ActionId,
                RootPipelineId = this.RootPipelineId,
                PipelineId = this.PipelineId,
                ExecutionStatusId = (int)executionStatusId
            };
            HandymanActorSystemAccess.Insert(actionExecutionStatusAudit);
            PipelineExecutorNS.LambdaEngine.GetLogger(this).Information
            ($"Action audit has been inserted with ActionId {this.ActionId} as {ExecutionStatusId}");
        }

    }

    public class ActionExecutionStatusAudit : Auditable
    {
        public ActionExecutionStatusAudit()
        {
            Id = BitConverter.ToInt64(Guid.NewGuid().ToByteArray(), 0);
        }

        public long Id { get; set; }
        public long ActionId { get; set; }
        public long PipelineId { get; set; }
        public int ExecutionStatusId { get; set; }
        public long RootPipelineId { get; set; }
    }


    public enum ExecutionGroup
    {
        TRY = 1,
        CATCH = 2,
        FINALLY = 3,
        ACTION = 4
    }

    public enum ExecutionStatus
    {
        STARTED = 1,
        RUNNING = 2,
        STOPPED = 3,
        FAILED = 4,
        COMPLETED = 5,
        STAGED = 6,
        WARNING = 7
    }

    public class PipelineExecutionAudit : AbstractAudit, IPipeline
    {

        public long PipelineId { get; set; } = BitConverter.ToInt64(Guid.NewGuid().ToByteArray(), 0);

        public string ModeOfExecution { get; set; }
        public string HostName { get; set; }
        public string ThreadName { get; set; }
        public string PipelineLoadType { get; set; }
        public string FileContent { get; set; } = string.Empty;
        public string RequestBody { get; set; } = string.Empty;
        public string RelativePath { get; set; } = string.Empty;

        public void UpdateExecutionStatusId(ExecutionStatus executionStatusId)
        {
            this.ExecutionStatusId = (int)executionStatusId;
            var pipelineStatusAudit = new PipelineExecutionStatusAudit()
            {
                PipelineId = PipelineId,
                ExecutionStatusId = (int)executionStatusId,
                RootPipelineId = RootPipelineId
            };
            HandymanActorSystemAccess.Insert(pipelineStatusAudit);
            Log.Information($"Pipeline audit has been inserted with PipelineId {PipelineName} as {executionStatusId}"); // TODO change this
        }

    }

    public class PipelineExecutionStatusAudit : Auditable
    {
        public long Id { get; set; } = BitConverter.ToInt64(Guid.NewGuid().ToByteArray(), 0);
        public long PipelineId { get; set; }
        public int ExecutionStatusId { get; set; }
        public long RootPipelineId { get; set; }
    }

    public class StatementExecutionAudit : Auditable
    {
        [JsonProperty("statementId")]

        public long StatementId { get; set; } = BitConverter.ToInt64(Guid.NewGuid().ToByteArray(), 0);


        [JsonProperty("actionId")]
        public long ActionId { get; set; }

        [JsonProperty("statementContent")]
        public string StatementContent { get; set; }

        [JsonProperty("rootPipelineId")]
        public long RootPipelineId { get; set; }

        [JsonProperty("rowsWritten")]
        public int RowsWritten { get; set; }

        [JsonProperty("rowsRead")]
        public int RowsRead { get; set; }

        [JsonProperty("rowsProcessed")]
        public int RowsProcessed { get; set; }

        [JsonProperty("timeTaken")]
        public double TimeTaken { get; set; }
    }



}
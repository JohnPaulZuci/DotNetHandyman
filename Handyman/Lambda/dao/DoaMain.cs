using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Newtonsoft.Json.Converters;

namespace DoaActionsNS
{
    public abstract class Auditable
    {
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public DateTime? LastModifiedDate { get; set; }
        public long CreatedBy { get; set; } = -1L;
        public long LastModifiedBy { get; set; } = -1L;
    }

    public abstract class AbstractAudit : Auditable
    {
        protected static readonly JsonSerializerSettings JSON_SETTINGS = new JsonSerializerSettings
        {
            ContractResolver = new CamelCasePropertyNamesContractResolver(),
            Converters = { new StringEnumConverter() },
            DateTimeZoneHandling = DateTimeZoneHandling.Utc
        };

        public static readonly JsonSerializer JSON_SERIALIZER = JsonSerializer.Create(JSON_SETTINGS);

        static AbstractAudit()
        {
            JsonConvert.DefaultSettings = () => JSON_SETTINGS;
        }

        public string? LambdaName { get; set; }  //TODO not null value check
        public string? PipelineName { get; set; } //TODO not null value check
        public string ProcessName { get; set; } //TODO not null value check
        public long RootPipelineId { get; set; }

        public long ParentPipelineId { get; set; } = -1L;
        public string ParentPipelineName { get; set; } = string.Empty;
        public long ParentActionId { get; set; } = -1L;
        public string ParentActionName { get; set; } = string.Empty;

        public int ExecutionStatusId { get; set; }

        public long ProcessId { get; set; }


        public Dictionary<string, string> context = new Dictionary<string, string>();
        [JsonIgnore]
        public string ContextNode { get; set; } = string.Empty;

        public void SetPipelineName(string pipelineName)
        {
            this.PipelineName = pipelineName;
        }

        public void SetContext(Dictionary<string, string> context)
        {
            this.context = context;
            this.ContextNode = context != null ? JsonConvert.SerializeObject(context, JSON_SETTINGS) : null;
        }
    }

    public interface IPipeline
    {
        public long PipelineId { get; }

        public long ParentPipelineId { get; }

        public long ParentActionId { get; }

        public string PipelineName { get; }

        public string LambdaName { get; }
    }

    // This PipelineExtensions methods are defined with the this keyword followed by 
    // the interface type, allowing them to be invoked on instances of any class that 
    // implements the IPipeline interface.
    public static class PipelineExtensions
    {
        public static string CurrentTime(this IPipeline pipeline)
        {
            return DateTime.Now.ToString();
        }

        public static string CurrentDate(this IPipeline pipeline)
        {
            return DateTime.Now.Date.ToString();
        }
    }

    public interface IAction : IPipeline
    {
        long ActionId { get; }

        string ActionName { get; }
    }

}
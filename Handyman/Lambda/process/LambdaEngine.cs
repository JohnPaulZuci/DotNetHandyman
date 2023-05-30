using Serilog;
using System.Net;
using System.Diagnostics;
using Antlr4.Runtime.Tree;

using AuditDAONS;
using AccessConfigNS;
using ExceptionNS;
using UtilityMethodsNS;
using DoaActionsNS;
using LambdaParserNS;
using ActionInterfaceNS;
using System.Reflection;
using Newtonsoft.Json;
using ActorOperationNS;

namespace PipelineExecutorNS
{

    public class LambdaEngine
    {
        private static readonly HandymanRepo REPO = new HandymanRepoImpl();
        private const string RAVEN_VM = "RavenVM";
        private const string THROW_EXCEPTION = "throw_exception";

        public static PipelineExecutionAudit Start(LContext lContext)
        {
            Log.Information("the given request => {LContext}", lContext);
            string hostName;
            try
            {
                hostName = Dns.GetHostName();
                Log.Information("Execution Engine Starts in host {HostName}", hostName);
            }
            catch (Exception e)
            {
                throw new HandymanException("hostName not found ", e);
            }

            PipelineExecutionAudit pipelineExecutionAudit = new PipelineExecutionAudit
            {
                HostName = hostName,
                ModeOfExecution = RAVEN_VM,
                ThreadName = Thread.CurrentThread.Name
            };

            if (lContext.RootPipelineId == null)
            {
                lContext.RootPipelineId = BitConverter.ToInt64(Guid.NewGuid().ToByteArray(), 0);
                Log.Information("LContext rootID => {RootPipelineId}", lContext.RootPipelineId);
                pipelineExecutionAudit.PipelineId = lContext.RootPipelineId ?? 0;
            }

            pipelineExecutionAudit.RootPipelineId = lContext.RootPipelineId ?? 0;
            pipelineExecutionAudit.ProcessId = Process.GetCurrentProcess().Id;

            HandymanActorSystemAccess.Insert(pipelineExecutionAudit);
            pipelineExecutionAudit.UpdateExecutionStatusId(ExecutionStatus.STAGED);
            Log.Information("Started building the pipeline context");

            try
            {
                pipelineExecutionAudit.UpdateExecutionStatusId(ExecutionStatus.STARTED);

                Dictionary<string, string> context = GetEContext(lContext.PipelineName);
                Log.Information("Raven Context has been initialized");

                string processFile = GetProcessFile(lContext, context);
                context.Add("parent-pipeline-id", lContext.ParentPipelineId.ToString());
                context.Add("pipeline-id", pipelineExecutionAudit.PipelineId.ToString());
                context.Add("process-id", pipelineExecutionAudit.PipelineId.ToString());
                context.Add("root-ref-id", pipelineExecutionAudit.PipelineId.ToString());
                foreach (var entry in lContext.InheritedContext)
                {
                    context[entry.Key] = entry.Value;
                }
                Log.Information("Raven context has been populated with inheritedContext");

                var ravenParserContext = GetRavenParserContext(processFile, lContext.LambdaName, context);

                ToPipeline(lContext, pipelineExecutionAudit);

                var relativePath = context.GetValueOrDefault(HRequestResolver.LoadType.FILE.ToString());

                pipelineExecutionAudit.ProcessName = (ravenParserContext.ProcessName);
                pipelineExecutionAudit.SetContext(context);
                Log.Information("Initial Pipeline context has been build successfully");

                try
                {
                    Log.Information("Pipeline execution has been started");
                    pipelineExecutionAudit.UpdateExecutionStatusId(ExecutionStatus.RUNNING);
                    Run(pipelineExecutionAudit, ravenParserContext.TryContext, context, ExecutionGroup.TRY);
                    Log.Information("Pipeline execution has been completed successfully");
                }
                catch (Exception e)
                {
                    Run(pipelineExecutionAudit, ravenParserContext.CatchContext, context, ExecutionGroup.CATCH);
                    pipelineExecutionAudit.UpdateExecutionStatusId(ExecutionStatus.FAILED);
                    Log.Error("try section");
                }

            }
            catch (Exception e)
            {
                Log.Error("Exception section", e);
            }
            return pipelineExecutionAudit;
        }

        private static void Run(PipelineExecutionAudit pipelineExecutionAudit,
        IList<RavenParser.ActionContext> tryContext, Dictionary<string, string> context,
        ExecutionGroup executionGroup)
        {
            foreach (var actionContext in tryContext)
            {
                var action = new ActionExecutionAudit
                {
                    ExecutionGroupId = (int)executionGroup,
                    PipelineId = pipelineExecutionAudit.PipelineId
                };

                action.context = context;
                action.PipelineId = pipelineExecutionAudit.PipelineId;

                ToAction(action, pipelineExecutionAudit);
                Log.Information("");
                Log.Information("Action context has been set successfully for action {0}", action.ActionId);
                DoAction(action, actionContext);
                Log.Information("Action context has been executed successfully for action {0}", action.ActionId);
            }

        }

        private static void DoAction(ActionExecutionAudit actionExecutionAudit,
            RavenParser.ActionContext actionContext)
        {
            HandymanActorSystemAccess.Insert(actionExecutionAudit);
            actionExecutionAudit.UpdateExecutionStatusId(ExecutionStatus.STAGED);
            HandymanActorSystemAccess.Update(actionExecutionAudit);
            Log.Information("\n");
            try
            {
                Log.Information("Action execution has been started");
                Log.Information("Given context {0}", actionExecutionAudit.context);
                IActionExecution execution = Load(actionContext, actionExecutionAudit);
                Execute(execution, actionExecutionAudit);
                Log.Information("Execution has been completed successfully");
            }
            catch (Exception e)
            {
                Log.Error(e, "Exception {0}", actionExecutionAudit.ActionName);
                actionExecutionAudit.UpdateExecutionStatusId(ExecutionStatus.FAILED);
                throw new HandymanException("Failed to convert", e);
            }
        }

        private static void Execute(IActionExecution execution, ActionExecutionAudit actionExecutionAudit)
        {
            ILogger logger = GetLogger(actionExecutionAudit);
            string actionName = actionExecutionAudit.ActionName;

            try
            {
                actionExecutionAudit.ThreadName = Thread.CurrentThread.Name;
                actionExecutionAudit.UpdateExecutionStatusId(ExecutionStatus.RUNNING);
                logger.Information("Execution class {0} loaded", actionName);

                if (execution.executeIf())
                {
                    logger.Information("Execution class {0} condition passed", actionName);
                    execution.execute();
                    logger.Information("Execution class {0} executed", actionName);
                }
                else
                {
                    logger.Information("Execution class {0} condition failed", actionName);
                }

                actionExecutionAudit.UpdateExecutionStatusId(ExecutionStatus.COMPLETED);
            }
            catch (Exception e)
            {
                logger.Error("Error at Execution " + actionName, e);
                actionExecutionAudit.UpdateExecutionStatusId(ExecutionStatus.FAILED);
                throw new HandymanException("Execution failed for " + actionName, e);
            }
        }

        private static IActionExecution Load(RavenParser.ActionContext actionContext, ActionExecutionAudit actionExecutionAudit)
        {
            IParseTree child = actionContext.GetChild(0);
            if (child != null)
            {
                string macroName = child.GetType().Name.Replace(ProcessExecutor.CONTEXT, "");
                actionExecutionAudit.MacroName = macroName;
                ILogger logger = GetLogger(actionExecutionAudit);
                logger.Information("Execution class {MacroName} staged", macroName);

                if (ProcessExecutor.ACTION_CONTEXT_MAP.ContainsKey(macroName) && ProcessExecutor.ACTION_EXECUTION_MAP.ContainsKey(macroName))
                {
                    try
                    {
                        actionExecutionAudit.UpdateExecutionStatusId(ExecutionStatus.STARTED);
                        return Initialization(child, actionExecutionAudit);
                    }
                    catch (Exception e)
                    {
                        logger.Error(e, "Error at Initialization {MacroName}");
                        // Handle the exception as needed
                    }
                }
                else
                {
                    logger.Error("Error Not found action {MacroName}");
                    // Handle the error as needed
                }
            }
            throw new HandymanException("Unknown ActionContext");
        }

        private static IActionExecution Initialization(IParseTree child, ActionExecutionAudit actionExecutionAudit)
        {
            ILogger logger = GetLogger(actionExecutionAudit);
            string macroName = actionExecutionAudit.MacroName;

            Type actionContextType = ProcessExecutor.ACTION_CONTEXT_MAP[macroName];
            IActionContext actionContext = (IActionContext)Activator.CreateInstance(actionContextType);
            logger.Debug("actionContext Execution class {0} instance created", macroName);
            CommandProxy.SetTarget(actionContext, child, actionExecutionAudit.context);
            actionExecutionAudit.ActionName = actionContext.name;
            logger.Debug("actionContext Execution context {0}", actionContext);
            actionExecutionAudit.Input = JsonConvert.SerializeObject(actionContext);
            HandymanActorSystemAccess.Update(actionExecutionAudit);

            Type actionExecutionType = ProcessExecutor.ACTION_EXECUTION_MAP[macroName];
            ConstructorInfo constructor = actionExecutionType.GetConstructor(new[] { typeof(ActionExecutionAudit), typeof(ILogger), typeof(object) });
            IActionExecution actionExecution = (IActionExecution)constructor.Invoke(new object[] { actionExecutionAudit, logger, actionContext });

            return actionExecution;
        }

        private static void ToAction(ActionExecutionAudit actionExecutionAudit,
            AbstractAudit abstractAudit)
        {
            actionExecutionAudit.ProcessId = Process.GetCurrentProcess().Id;
            actionExecutionAudit.CreatedBy = abstractAudit.CreatedBy;
            actionExecutionAudit.PipelineName = abstractAudit.PipelineName;
            actionExecutionAudit.LambdaName = abstractAudit.LambdaName;
            actionExecutionAudit.ParentActionId = abstractAudit.ParentActionId;
            actionExecutionAudit.ParentActionName = abstractAudit.ParentActionName;
            actionExecutionAudit.ParentPipelineId = abstractAudit.ParentPipelineId;
            actionExecutionAudit.ParentPipelineName = abstractAudit.ParentPipelineName;
            actionExecutionAudit.RootPipelineId = abstractAudit.RootPipelineId;
        }

        private static void ToPipeline(LContext lContext, PipelineExecutionAudit pipelineExecutionAudit)
        {
            pipelineExecutionAudit.RelativePath = lContext.RelativePath;
            pipelineExecutionAudit.PipelineLoadType = lContext.ProcessLoadType;
            pipelineExecutionAudit.LambdaName = lContext.LambdaName;
            pipelineExecutionAudit.PipelineName = lContext.PipelineName;
            pipelineExecutionAudit.ParentPipelineId = lContext.ParentPipelineId.GetValueOrDefault();
            pipelineExecutionAudit.ParentPipelineName = lContext.ParentPipelineName;
            pipelineExecutionAudit.ParentActionId = lContext.ParentActionId;
            pipelineExecutionAudit.ParentActionName = lContext.ParentActionName;
            pipelineExecutionAudit.RequestBody = lContext.Payload;
            pipelineExecutionAudit.CreatedBy = lContext.UserId;
        }

        private static RavenParserContext GetRavenParserContext(string processFile, string lambdaName, Dictionary<string, string> context)
        {
            RavenParser.ProcessContext ravenParser = LambdaParser.DoParse(processFile, context);
            RavenParserContext rp = new RavenParserContext();
            rp.ProcessName = CommandProxy.GetString(ravenParser.name, context);
            rp.TryContext = ravenParser.tryBlock._actions;
            rp.CatchContext = ravenParser.catchBlock._actions;
            rp.FinallyContext = ravenParser.finallyBlock._actions;
            return rp;
        }

        private static string GetProcessFile(LContext lContext, Dictionary<string, string> context)
        {
            string lambdaName = lContext.LambdaName;
            string relativePath = lContext.RelativePath;
            Log.Information("Started initializing raven context ");
            string processLoadType = lContext.ProcessLoadType;
            string processFile;
            if (lContext.ParentPipelineId != null)
            {
                processFile = GetProcessFile(HRequestResolver.LoadType.FILE.ToString(), lambdaName, context, relativePath);
            }
            else if ((HRequestResolver.LoadType)Enum.Parse(typeof(HRequestResolver.LoadType), lContext.ProcessLoadType) == HRequestResolver.LoadType.REST)
            {
                processFile = lContext.Payload;
            }
            else
            {
                processFile = GetProcessFile(processLoadType, lambdaName, context, null);
            }
            return processFile;
        }

        private static string GetProcessFile(string processLoadType, string lambdaName,
            Dictionary<string, string> context, string? relativePath)
        {
            string processFile = relativePath != null ? HRequestResolver.ReadFile(relativePath, "") :
                        HRequestResolver.DoResolve(lambdaName, processLoadType, context);
            if (processFile == null)
            {
                throw new HandymanException("Content configuration for process " + lambdaName + " is missing, check spw_process_config or spw_instance_config");
            }
            if (processFile.Length == 0)
            {
                throw new HandymanException("Content configuration for process " + lambdaName + " is missing, check spw_process_config or spw_instance_config");
            }
            return processFile;
        }

        private static Dictionary<string, string> GetEContext(string pipelineName)
        {
            return new Dictionary<string, string>(ConfigAccess.GetAllConfig(pipelineName));
        }

        public static ILogger GetLogger(ActionExecutionAudit actionExecutionAudit)
        {
            return SerilogConfig.Log ?? createLogger();
        }

        private static ILogger createLogger()
        {
            ILogger Log = new LoggerConfiguration()
            .MinimumLevel.Debug()
            .WriteTo.File("./log_lamda.txt", rollingInterval: RollingInterval.Day)
            .CreateLogger();
            return Log;

        }
    }


}
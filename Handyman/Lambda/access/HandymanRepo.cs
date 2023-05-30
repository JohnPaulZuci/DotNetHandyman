using AuditDAONS;
using ConfigNS;

public interface HandymanRepo
    {
        static string SYS_PACKAGE = "SYS_PACKAGE";

        Dictionary<string, string> GetAllConfig(string pipelineName);

        Dictionary<string, string> GetCommonConfig();

        SpwResourceConfig GetResourceConfig(string name);

        HashSet<string> GetPackageAction();

        // SPW Instance

        void Insert(SpwInstanceConfig spwInstanceConfig);

        void Update(SpwInstanceConfig spwInstanceConfig);

        List<SpwInstanceConfig> FindAllInstances();

        List<SpwInstanceConfig> FindAllByInstanceVariable(string variable);

        List<SpwInstanceConfig> FindAllByInstance(string instance);

        SpwInstanceConfig? FindOneInstance(string instance, string variable);

        // SPW Process

        void Insert(SpwProcessConfig spwProcessConfig);

        void Update(SpwProcessConfig spwProcessConfig);

        List<SpwProcessConfig> FindAllProcesses();

        List<SpwProcessConfig> FindAllByProcess(string process);

        SpwProcessConfig? FindOneProcess(string process, string variable);

        // SPW Common

        void Insert(SpwCommonConfig spwCommonConfig);

        void Update(SpwCommonConfig spwCommonConfig);

        List<SpwCommonConfig> FindAllCommonConfigs();

        SpwCommonConfig? FindOneCommonConfig(string variable);

        // SPW Resource

        void Insert(SpwResourceConfig spwResourceConfig);

        void Update(SpwResourceConfig spwResourceConfig);

        List<SpwResourceConfig> FindAllResourceConfigs();

        SpwResourceConfig? FindOneResourceConfig(string configName);

        // Audit

        void InsertPipeline(PipelineExecutionAudit audit);

        void InsertAction(ActionExecutionAudit audit);

        void InsertStatement(StatementExecutionAudit audit);

        void Save(PipelineExecutionStatusAudit audit);

        void Save(ActionExecutionStatusAudit audit);

        void Update(PipelineExecutionAudit audit);

        void Update(ActionExecutionAudit audit);

        List<PipelineExecutionAudit> FindAllPipelinesByRootPipelineId(long rootPipelineId);

        PipelineExecutionAudit? FindPipeline(long pipelineId);

        List<ActionExecutionAudit> FindActions(long pipelineId);

        List<ActionExecutionAudit> FindAllActionsByRootPipelineId(long rootPipelineId);

        List<PipelineExecutionAudit> FindAllPipelinesByParentActionId(long parentActionId);

        List<PipelineExecutionAudit> FindAllPipelines();

        List<PipelineExecutionAudit> FindAllByPipelineName(string pipelineName);

        List<PipelineExecutionAudit> FindAllProcessName(string processName);

    }
using System.Data;
using System.Data.SqlClient;
using Dapper;
using AuditDAONS;
using UtilsNS;

namespace RepoConfigNS
{

    public interface AbstractAccess
    {
        protected string GetLambdaName(string pipelineName)
        {
            if (pipelineName != null && pipelineName.Contains("#"))
            {
                return pipelineName.Substring(0, pipelineName.LastIndexOf("#"));
            }
            return pipelineName;
        }
    }

    public interface IPipelineExecutionAuditRepo
    {
        string COLUMNS { get; } // Define the column names here

        void Insert(PipelineExecutionAudit actionExecutionAudit);
        void Update(PipelineExecutionAudit actionExecutionAudit);
        List<PipelineExecutionAudit> FindAllPipelines();
        List<PipelineExecutionAudit> FindAllPipelinesByRootPipelineId(long rootPipelineId);
        List<PipelineExecutionAudit> FindAllByPipelineName(string pipelineName);
        List<PipelineExecutionAudit> FindAllPipelinesByParentActionId(long parentActionId);
        PipelineExecutionAudit FindOneByPipelineId(long pipelineId);
        List<PipelineExecutionAudit> FindAllByProcessName(string processName);
    }

    public class PipelineExecutionAuditRepo : IPipelineExecutionAuditRepo
    {
        private readonly SqlConnection connection;

        public PipelineExecutionAuditRepo(SqlConnection connection)
        {
            this.connection = connection;
        }

        public string COLUMNS { get; } = "pipeline_id, created_by, created_date, last_modified_by, last_modified_date, context_node, execution_status_id, lambda_name, parent_action_id, parent_action_name, parent_pipeline_id, parent_pipeline_name, pipeline_name, file_content, host_name, mode_of_execution, pipeline_load_type, relative_path, request_body, thread_name, process_name, root_pipeline_id, process_id";

        public void Insert(PipelineExecutionAudit actionExecutionAudit)
        {
            // Construct the SQL query string
            string query = $"INSERT INTO {DoaConstant.AUDIT_SCHEMA_NAME}.{DoaConstant.PEA_TABLE_NAME} ({COLUMNS}) " +
                "VALUES (@pipelineId, @createdBy, @createdDate, @lastModifiedBy, @lastModifiedDate, @contextNode, " +
                "@executionStatusId, @lambdaName, @parentActionId, @parentActionName, @parentPipelineId, @parentPipelineName, " +
                "@pipelineName, @fileContent, @hostName, @modeOfExecution, @pipelineLoadType, @relativePath, @requestBody, " +
                "@threadName, @processName, @rootPipelineId, @processId)";

            // Execute the query using Dapper
            connection.Execute(query, actionExecutionAudit);
        }

        public void Update(PipelineExecutionAudit actionExecutionAudit)
        {
            // Construct the SQL query string
            string query = $"UPDATE {DoaConstant.AUDIT_SCHEMA_NAME}.{DoaConstant.PEA_TABLE_NAME} " +
                "SET created_by = @createdBy, created_date = @createdDate, last_modified_by = @lastModifiedBy, " +
                "last_modified_date = @lastModifiedDate, context_node = @contextNode, execution_status_id = @executionStatusId, " +
                "lambda_name = @lambdaName, parent_action_id = @parentActionId, parent_action_name = @parentActionName, " +
                "parent_pipeline_id = @parentPipelineId, parent_pipeline_name = @parentPipelineName, pipeline_name = @pipelineName, " +
                "file_content = @fileContent, host_name = @hostName, mode_of_execution = @modeOfExecution, " +
                "pipeline_load_type = @pipelineLoadType, relative_path = @relativePath, request_body = @requestBody, " +
                "process_name = @processName, root_pipeline_id = @rootPipelineId, process_id = @processId " +
                "WHERE pipeline_id = @pipelineId";

            // Execute the query using Dapper
            connection.Execute(query, actionExecutionAudit);
        }

        public List<PipelineExecutionAudit> FindAllPipelines()
        {
            // Construct the SQL query string
            string query = $"SELECT {COLUMNS} FROM {DoaConstant.AUDIT_SCHEMA_NAME}.{DoaConstant.PEA_TABLE_NAME}";

            // Execute the query using Dapper and return the result
            return connection.Query<PipelineExecutionAudit>(query).ToList();
        }

        public List<PipelineExecutionAudit> FindAllPipelinesByRootPipelineId(long rootPipelineId)
        {
            // Construct the SQL query string
            string query = $"SELECT {COLUMNS} FROM {DoaConstant.AUDIT_SCHEMA_NAME}.{DoaConstant.PEA_TABLE_NAME} " +
                "WHERE root_pipeline_id = @rootPipelineId";

            // Execute the query using Dapper and return the result
            return connection.Query<PipelineExecutionAudit>(query, new { rootPipelineId }).ToList();
        }

        public List<PipelineExecutionAudit> FindAllByPipelineName(string pipelineName)
        {
            // Construct the SQL query string
            string query = $"SELECT {COLUMNS} FROM {DoaConstant.AUDIT_SCHEMA_NAME}.{DoaConstant.PEA_TABLE_NAME} " +
                "WHERE pipeline_name = @pipelineName";

            // Execute the query using Dapper and return the result
            return connection.Query<PipelineExecutionAudit>(query, new { pipelineName }).ToList();
        }

        public List<PipelineExecutionAudit> FindAllPipelinesByParentActionId(long parentActionId)
        {
            // Construct the SQL query string
            string query = $"SELECT {COLUMNS} FROM {DoaConstant.AUDIT_SCHEMA_NAME}.{DoaConstant.PEA_TABLE_NAME} " +
                "WHERE parent_action_id = @parentActionId";

            // Execute the query using Dapper and return the result
            return connection.Query<PipelineExecutionAudit>(query, new { parentActionId }).ToList();
        }

        public PipelineExecutionAudit FindOneByPipelineId(long pipelineId)
        {
            // Construct the SQL query string
            string query = $"SELECT {COLUMNS} FROM {DoaConstant.AUDIT_SCHEMA_NAME}.{DoaConstant.PEA_TABLE_NAME} " +
                "WHERE pipeline_id = @pipelineId";

            // Execute the query using Dapper and return the result
            return connection.QueryFirstOrDefault<PipelineExecutionAudit>(query, new { pipelineId });
        }

        public List<PipelineExecutionAudit> FindAllByProcessName(string processName)
        {
            // Construct the SQL query string
            string query = $"SELECT DISTINCT {COLUMNS} FROM {DoaConstant.AUDIT_SCHEMA_NAME}.{DoaConstant.PEA_TABLE_NAME} " +
                "WHERE process_name = @processName";

            // Execute the query using Dapper and return the result
            return connection.Query<PipelineExecutionAudit>(query, new { processName }).ToList();
        }
    }
    public interface IActionExecutionAuditRepo
    {
        string COLUMNS { get; }

        void Insert(ActionExecutionAudit actionExecutionAudit);

        void Update(ActionExecutionAudit actionExecutionAudit);

        List<ActionExecutionAudit> FindAllActions();

        List<ActionExecutionAudit> FindAllActionsByRootPipelineId(long rootPipelineId);

        List<ActionExecutionAudit> FindAllActionsByPipelineId(long pipelineId);
    }

    public class ActionExecutionAuditRepo : IActionExecutionAuditRepo
    {
        private readonly IDbConnection connection;

        public ActionExecutionAuditRepo(IDbConnection connection)
        {
            this.connection = connection;
        }

        public string COLUMNS { get; } = " action_id, created_by, created_date, last_modified_by, last_modified_date, context_node, execution_status_id, lambda_name, parent_action_id, parent_action_name, parent_pipeline_id, parent_pipeline_name, pipeline_name, action_name, execution_group_id, input_node, log, pipeline_id, root_pipeline_id ";

        public void Insert(ActionExecutionAudit actionExecutionAudit)
        {
            connection.Execute(
                @"INSERT INTO " + DoaConstant.AUDIT_SCHEMA_NAME + "." + DoaConstant.AEA_TABLE_NAME + " (" + COLUMNS + ") VALUES (@ActionId, @CreatedBy, @CreatedDate, @LastModifiedBy, @LastModifiedDate, @ContextNode, @ExecutionStatusId, @LambdaName, @ParentActionId, @ParentActionName, @ParentPipelineId, @ParentPipelineName, @PipelineName, @ActionName, @ExecutionGroupId, @InputNode, @Log, @PipelineId, @RootPipelineId)",
                actionExecutionAudit
            );
        }

        public void Update(ActionExecutionAudit actionExecutionAudit)
        {
            connection.Execute(
                @"UPDATE " + DoaConstant.AUDIT_SCHEMA_NAME + "." + DoaConstant.AEA_TABLE_NAME + " SET created_by = @CreatedBy, created_date = @CreatedDate, last_modified_by = @LastModifiedBy, last_modified_date = @LastModifiedDate, context_node = @ContextNode, execution_status_id = @ExecutionStatusId, lambda_name = @LambdaName, parent_action_id = @ParentActionId, parent_action_name = @ParentActionName, parent_pipeline_id = @ParentPipelineId, parent_pipeline_name = @ParentPipelineName, pipeline_name = @PipelineName, action_name = @ActionName, execution_group_id = @ExecutionGroupId, input_node = @InputNode, log = @Log, pipeline_id = @PipelineId, thread_name = @ThreadName, root_pipeline_id = @RootPipelineId WHERE action_id = @ActionId",
                actionExecutionAudit
            );
        }

        public List<ActionExecutionAudit> FindAllActions()
        {
            return connection.Query<ActionExecutionAudit>(
                @"SELECT " + COLUMNS + " FROM " + DoaConstant.AUDIT_SCHEMA_NAME + "." + DoaConstant.AEA_TABLE_NAME
            ).ToList();
        }

        public List<ActionExecutionAudit> FindAllActionsByRootPipelineId(long rootPipelineId)
        {
            return connection.Query<ActionExecutionAudit>(
                @"SELECT " + COLUMNS + " FROM " + DoaConstant.AUDIT_SCHEMA_NAME + "." + DoaConstant.AEA_TABLE_NAME + " WHERE root_pipeline_id = @RootPipelineId",
                new { RootPipelineId = rootPipelineId }
            ).ToList();
        }

        public List<ActionExecutionAudit> FindAllActionsByPipelineId(long pipelineId)
        {
            return connection.Query<ActionExecutionAudit>(
                @"SELECT " + COLUMNS + " FROM " + DoaConstant.AUDIT_SCHEMA_NAME + "." + DoaConstant.AEA_TABLE_NAME + " WHERE pipeline_id = @PipelineId",
                new { PipelineId = pipelineId }
            ).ToList();
        }
    }

}
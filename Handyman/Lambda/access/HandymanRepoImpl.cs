using System.Data;
using System.Data.SqlClient;
using AuditDAONS;
using ConfigNS;
using Dapper;
using RepoConfigNS;
using UtilityMethodsNS;
using UtilsNS;

public partial class HandymanRepoImpl : HandymanRepo, AbstractAccess
    {
        public static readonly string DOT = ".";
        protected static readonly string CONFIG_URL = "raven.db.url";
        private static readonly string CONFIG_PASSWORD = "raven.db.password";
        private static readonly string CONFIG_USER = "raven.db.user";
        private static readonly string CONFIG_DBNAME = "raven.db.databasename";
        private static readonly IDbConnection dbConnection;
        static string connectionString;

        static HandymanRepoImpl()
        {

            //TODO update the connection string variable [Postgres]
            //connectionString = $"Server=localhost;Database=zio_flatirons;User Id=postgres;Password=password;";
            //var dbConnection = new NpgsqlConnection(connectionString);

            connectionString = $"Data Source={PropertyHandler.Get(CONFIG_URL)};Initial Catalog={PropertyHandler.Get(CONFIG_DBNAME)};User ID={PropertyHandler.Get(CONFIG_USER)};Password={PropertyHandler.Get(CONFIG_PASSWORD)}";
            dbConnection = new SqlConnection(connectionString);
            dbConnection.Open();
        }

        public Dictionary<string, string> GetAllConfig(string pipelineName)
        {
            Dictionary<string, string> common = GetCommonConfig();

            List<SpwProcessConfig> process = FindAllByProcess(pipelineName);
            var processDictionary = new Dictionary<string, string>();
            foreach (var item in process)
            {
                processDictionary[item.Variable] = item.Value;
            }

            List<SpwInstanceConfig> instance = FindAllByInstance(pipelineName);
            var instanceDictionary = new Dictionary<string, string>();
            foreach (var item in instance)
            {
                instanceDictionary[item.Variable] = item.Value;
            }

            Dictionary<string, string> allConfig = common.Concat(processDictionary).Concat(instanceDictionary)
                                           .ToDictionary(pair => pair.Key, pair => pair.Value);
            return allConfig;
        }

        public List<SpwInstanceConfig> FindAllByInstance(string pipelineName)
        {
            List<SpwInstanceConfig> result = dbConnection.Query<SpwInstanceConfig>(
               "SELECT * FROM config.spw_instance_config WHERE process = @pipelineName",
               new { pipelineName }
           ).ToList();
            return result ?? new List<SpwInstanceConfig>();
        }

        public List<SpwProcessConfig> FindAllByProcess(string lambdaName = "")
        {
            List<SpwProcessConfig> result = dbConnection.Query<SpwProcessConfig>(
               "SELECT * FROM config.spw_process_config  WHERE process = @lambdaName",
               new { lambdaName }
           ).ToList();
            return result ?? new List<SpwProcessConfig>();
        }
        public Dictionary<string, string> GetCommonConfig()
        {
            var sql = "SELECT variable, value FROM config.spw_common_config";
            var result = new Dictionary<string, string>();

            using (SqlCommand command = new SqlCommand(sql, (SqlConnection)dbConnection))
            {
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        result[reader.GetString(0)] = reader.GetString(1);
                    }
                }
            }
            return result;
        }

        public SpwResourceConfig GetResourceConfig(string config_name)
        {
            var sql = "SELECT * FROM config.spw_resource_config WHERE config_name = @config_name";
            SpwResourceConfig result = dbConnection.QueryFirst<SpwResourceConfig>(
                sql,
                new { config_name }
                );
            return result ?? new SpwResourceConfig();
        }

        public HashSet<string> GetPackageAction()
        {
            var sql = $"SELECT DISTINCT value FROM config.spw_process_config where process='{HandymanRepo.SYS_PACKAGE}' AND active='true'";
            var command = new SqlCommand(sql, (SqlConnection)dbConnection);
            var result = new HashSet<string>();
            using (var reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    result.Add(reader.GetString(0));
                }
            }

            return result;

        }

        public void Insert(SpwInstanceConfig spwInstanceConfig)
        {
            var sql = @"
                INSERT INTO config.spw_instance_config 
                (instance, variable, value, updated_at) 
                VALUES (@instance, @variable, @value, @updatedAt)";

            var command = new SqlCommand(sql, (SqlConnection)dbConnection);
            command.Parameters.AddWithValue("@instance", spwInstanceConfig.Instance);
            command.Parameters.AddWithValue("@variable", spwInstanceConfig.Variable);
            command.Parameters.AddWithValue("@value", spwInstanceConfig.Value);
            command.Parameters.AddWithValue("@updatedAt", DateTime.UtcNow);
            //dbConnection.Open();
            command.ExecuteNonQuery();
        }

        public void Update(SpwInstanceConfig spwInstanceConfig)
        {
            var sql = @"
             UPDATE config.instance_config SET value = @value WHERE instance = @instance AND 
             variable = @variable";

            var command = new SqlCommand(sql, (SqlConnection)dbConnection);
            command.Parameters.AddWithValue("@instance", spwInstanceConfig.Instance);
            command.Parameters.AddWithValue("@variable", spwInstanceConfig.Variable);
            command.Parameters.AddWithValue("@value", spwInstanceConfig.Value);
            command.ExecuteNonQuery();
        }

        List<SpwInstanceConfig> HandymanRepo.FindAllInstances()
        {
            throw new NotImplementedException();
        }

        List<SpwInstanceConfig> HandymanRepo.FindAllByInstanceVariable(string variable)
        {
            throw new NotImplementedException();
        }

        List<SpwInstanceConfig> HandymanRepo.FindAllByInstance(string instance)
        {
            throw new NotImplementedException();
        }

        SpwInstanceConfig? HandymanRepo.FindOneInstance(string instance, string variable)
        {
            throw new NotImplementedException();
        }

        //Process
        void Insert(SpwProcessConfig spwProcessConfig)
        {
            throw new NotImplementedException();
        }

        void HandymanRepo.Update(SpwProcessConfig spwProcessConfig)
        {
            throw new NotImplementedException();
        }

        List<SpwProcessConfig> HandymanRepo.FindAllProcesses()
        {
            throw new NotImplementedException();
        }


        SpwProcessConfig? HandymanRepo.FindOneProcess(string process, string variable)
        {
            throw new NotImplementedException();
        }

        //Common config
        void HandymanRepo.Insert(SpwCommonConfig spwCommonConfig)
        {
            throw new NotImplementedException();
        }

        void HandymanRepo.Update(SpwCommonConfig spwCommonConfig)
        {
            throw new NotImplementedException();
        }

        List<SpwCommonConfig> HandymanRepo.FindAllCommonConfigs()
        {
            throw new NotImplementedException();
        }

        SpwCommonConfig? HandymanRepo.FindOneCommonConfig(string variable)
        {
            throw new NotImplementedException();
        }

        //Resource config
        void HandymanRepo.Insert(SpwResourceConfig spwResourceConfig)
        {
            throw new NotImplementedException();
        }

        void HandymanRepo.Update(SpwResourceConfig spwResourceConfig)
        {
            throw new NotImplementedException();
        }

        List<SpwResourceConfig> FindAllResourceConfigs()
        {
            throw new NotImplementedException();
        }

        public SpwResourceConfig? FindOneResourceConfig(string configName)
        {
            throw new NotImplementedException();
        }


        List<SpwResourceConfig> HandymanRepo.FindAllResourceConfigs()
        {
            throw new NotImplementedException();
        }

        //Audit 
        public void InsertPipeline(PipelineExecutionAudit audit)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                audit.LastModifiedDate = DateTime.Now;
                IPipelineExecutionAuditRepo repo = new PipelineExecutionAuditRepo(connection);
                repo.Insert(audit);
            }
        }

        public void InsertAction(ActionExecutionAudit audit)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                audit.LastModifiedDate = DateTime.Now;
                IActionExecutionAuditRepo repo = new ActionExecutionAuditRepo(connection);
                repo.Insert(audit);
            }
        }

        public void InsertStatement(StatementExecutionAudit audit)
        {
            throw new NotImplementedException();
        }

        // Save 
        public void Save(PipelineExecutionStatusAudit audit)
        {
            audit.LastModifiedDate = DateTime.Now;
            using (var connection = new SqlConnection(connectionString))
            {
                connection.Execute(@"INSERT INTO " + DoaConstant.AUDIT_SCHEMA_NAME + "." + DoaConstant.PESA_TABLE_NAME + " (id, created_by, created_date, last_modified_by, last_modified_date, execution_status_id, pipeline_id, root_pipeline_id) VALUES (@Id, @CreatedBy, @CreatedDate, @LastModifiedBy, @LastModifiedDate, @ExecutionStatusId, @PipelineId, @RootPipelineId)",
                    audit);
            }
        }

        public void Save(ActionExecutionStatusAudit audit)
        {
            audit.LastModifiedDate = DateTime.Now;
            using (var connection = new SqlConnection(connectionString))
            {
                connection.Execute(
                    @"INSERT INTO " + DoaConstant.AUDIT_SCHEMA_NAME + "." + DoaConstant.AESA_TABLE_NAME + " (id, created_by, created_date, last_modified_by, last_modified_date, action_id, execution_status_id, pipeline_id, root_pipeline_id) " +
                    "VALUES (@Id, @CreatedBy, @CreatedDate, @LastModifiedBy, @LastModifiedDate, @ActionId, @ExecutionStatusId, @PipelineId, @RootPipelineId)",
                    audit
                );
            }
        }

        //Update
        public void Update(PipelineExecutionAudit audit)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                audit.LastModifiedDate = DateTime.Now;
                IPipelineExecutionAuditRepo repo = new PipelineExecutionAuditRepo(connection);
                repo.Update(audit);
            }
        }

        public void Update(ActionExecutionAudit audit)
        {
             using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                audit.LastModifiedDate = DateTime.Now;
                IActionExecutionAuditRepo repo = new ActionExecutionAuditRepo(connection);
                repo.Update(audit);
            }
            
        }

        //Query in the Audit
        List<PipelineExecutionAudit> HandymanRepo.FindAllPipelinesByRootPipelineId(long rootPipelineId)
        {
            throw new NotImplementedException();
        }

        PipelineExecutionAudit? HandymanRepo.FindPipeline(long pipelineId)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                var columns = "pipeline_id, created_by, created_date, last_modified_by, last_modified_date, context_node, execution_status_id, lambda_name, parent_action_id, parent_action_name, parent_pipeline_id, parent_pipeline_name, pipeline_name, file_content, host_name, mode_of_execution, pipeline_load_type, relative_path, request_body, thread_name, process_name, root_pipeline_id, process_id";
                var repo = connection.QuerySingleOrDefault<PipelineExecutionAudit>(
                    @"SELECT " + columns + " " +
                    "FROM " + DoaConstant.AUDIT_SCHEMA_NAME + "." + DoaConstant.PEA_TABLE_NAME + " " +
                    "WHERE pipeline_id = @PipelineId",
                    new { PipelineId = pipelineId }
                );

                return repo;
            }
        }

        List<ActionExecutionAudit> HandymanRepo.FindActions(long pipelineId)
        {
            throw new NotImplementedException();
        }

        List<ActionExecutionAudit> HandymanRepo.FindAllActionsByRootPipelineId(long rootPipelineId)
        {
            throw new NotImplementedException();
        }

        List<PipelineExecutionAudit> HandymanRepo.FindAllPipelinesByParentActionId(long parentActionId)
        {
            throw new NotImplementedException();
        }

        List<PipelineExecutionAudit> HandymanRepo.FindAllPipelines()
        {
            throw new NotImplementedException();
        }

        List<PipelineExecutionAudit> HandymanRepo.FindAllByPipelineName(string pipelineName)
        {
            throw new NotImplementedException();
        }

        List<PipelineExecutionAudit> HandymanRepo.FindAllProcessName(string processName)
        {
            throw new NotImplementedException();
        }

        void HandymanRepo.Insert(SpwProcessConfig spwProcessConfig)
        {
            throw new NotImplementedException();
        }

    }
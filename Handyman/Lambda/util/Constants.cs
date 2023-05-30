namespace UtilsNS
{
    public static class DoaConstant
    {
        public const string AUDIT_SCHEMA_NAME = "audit";
        public const string CONFIG_SCHEMA_NAME = "config";

        public const string SEA_TABLE_NAME = "statement_execution_audit";
        public const string PESA_TABLE_NAME = "pipeline_execution_status_audit";
        public const string AESA_TABLE_NAME = "action_execution_status_audit";
        public const string PEA_TABLE_NAME = "pipeline_execution_audit";
        public const string AEA_TABLE_NAME = "action_execution_audit";
        public const string SCC_TABLE_NAME = "spw_common_config";
        public const string SIC_TABLE_NAME = "spw_instance_config";
        public const string SPC_TABLE_NAME = "spw_process_config";
        public const string SRC_TABLE_NAME = "spw_resource_config";
    }

    public static class Constants
    {
        public const string WRITE_SIZE = "write_size";
        public const string DEFAULT_WRITE_SIZE = "5000";
        public const string READ_SIZE = "read_size";
        public const string DEFAULT_READ_SIZE = "5000";
        public const string WRITER_THREAD = "writer_count";
        public const string DEFAULT_WRITER_COUNT = "3";
        public const string STRING_DATATYPE = "varchar";
        public const string STRING_ENCLOSER = "'";
        public const string FIELD_SEPARATOR = ",";
        public const string INSERT_STMT_VALUE_START = "(";
        public const string INSERT_STMT_VALUE_END = ")";
        public const string SEMICOLON = ";";
        public const string OPEN_PARENTHESES = "(";
        public const string CLOSE_PARENTHESES = ")";
        public const string SPACE = " ";

        public const string BODY_TYPE_NONE = "NONE";
        public const string BODY_TYPE_JSON = "JSON";
        public const string BODY_TYPE_FORM = "FORM";
        public const string PART_TYPE_ARRAY = "ARRAY";
        public const string PART_TYPE_TEXT = "TEXT";
        public const string PART_TYPE_FILE = "FILE";
    }

}
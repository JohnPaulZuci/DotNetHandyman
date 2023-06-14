using System.Data;
using System.Data.SqlClient;
using MySql.Data.MySqlClient;
using Npgsql;
using IBM.Data.DB2.Core;

using DoaActionsNS;
using System.Data.Common;

namespace ConfigNS
{
    public class SpwCommonConfig : Auditable
    {
        public long Id { get; set; }
        public string Variable { get; set; } = null!;
        public string Value { get; set; } = null!;
        public bool Active { get; set; }
        public int Version { get; set; }
    }

    public class SpwInstanceConfig : Auditable
    {
        public long Id { get; set; }
        public string Instance { get; set; } = null!;
        public string Process { get; set; } = null!;
        public string Variable { get; set; } = null!;
        public string Value { get; set; } = null!;
        public bool Active { get; set; }
        public int Version { get; set; }
    }


    public class SpwProcessConfig : Auditable
    {
        public long Id { get; set; }
        public string Process { get; set; } = null!;
        public string Variable { get; set; } = null!;
        public string Value { get; set; } = null!;
        public bool Active { get; set; }
        public int Version { get; set; }

    }



    public class SpwResourceConfig : Auditable
    {
        public long Id { get; set; }

        public string Config_Name { get; set; } = null!;
        public string Driver_Class { get; set; } = null!;
        public string User_Name { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string Resource_Url { get; set; } = null!;
        public string Host { get; set; } = null!;
        public string Port { get; set; } = null!;
        public string Database_Name { get; set; } = null!;

        public bool Active { get; set; }
        public int Version { get; set; }

        public IDbConnection GetConnection()
        {
            return CreateConnection();
        }

        public enum DatabaseType
        {
            MySql,
            PostgreSQL,
            MSSQL,
            DB2
        }
        public IDbConnection CreateConnection()
        {
            IDbConnection connection;
            DatabaseType databaseType = DatabaseType.PostgreSQL;

            if (Driver_Class.IndexOf("mysql", StringComparison.OrdinalIgnoreCase) >= 0)
            {
                databaseType = DatabaseType.MySql;
            }
            else if ((Driver_Class.IndexOf("sqlserver", StringComparison.OrdinalIgnoreCase) >= 0) || 
                (Driver_Class.IndexOf("mssql", StringComparison.OrdinalIgnoreCase) >= 0))
            {
                databaseType = DatabaseType.MSSQL;
            }
            else if (Driver_Class.IndexOf("db2", StringComparison.OrdinalIgnoreCase) >= 0) 
            {
                databaseType = DatabaseType.DB2;
            }

            switch (databaseType)
            {
                case DatabaseType.PostgreSQL:
                    string connectionString = $"Server={Host};Database={Database_Name};User Id={User_Name};Password={Password};";
                    connection = new NpgsqlConnection(connectionString);
                    break;

                case DatabaseType.MySql:
                    connectionString = $"Server={Host};Database={Database_Name};Uid={User_Name};Pwd={Password}";
                    connection = new MySqlConnection(connectionString);
                    break;

                case DatabaseType.MSSQL:
                    connectionString = $"Data Source={Host};Initial Catalog={Database_Name};User ID={User_Name};Password={Password}";
                    connection = new SqlConnection(connectionString);
                    break;
                case DatabaseType.DB2:
                    connectionString = $"Server={Host};Database={Database_Name};UID={User_Name};PWD={Password};";
                    connection = new DB2Connection(connectionString);
                    break;

                default:
                    throw new ArgumentException("Invalid database type.");
            }

            return connection;
        }
    }

}
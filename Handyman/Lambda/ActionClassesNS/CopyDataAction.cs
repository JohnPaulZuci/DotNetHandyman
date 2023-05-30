using System;
using Serilog;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Concurrent;

using ActionInterfaceNS;
using AuditDAONS;
using ExceptionNS;
using AccessConfigNS;
using UtilsNS;
using ResourceAccessNS;

[ActionExecution(ActionName = CopyDataAction.COPYDATA)]
public class CopyDataAction : IActionExecution
{
    public const string COPYDATA = "CopyData";
    private ActionExecutionAudit actionExecutionAudit;
    private ILogger log;
    private CopyData copydata;

    // Test code

    private static string sourceConnectionString = string.Empty;// = "Data Source=InstanceA;Initial Catalog=DatabaseA;User ID=Username;Password=Password;";
    private static string targetConnectionString = string.Empty;// = "Data Source=InstanceB;Initial Catalog=DatabaseB;User ID=Username;Password=Password;";


    private static int fetchSize = 1000; // Number of rows to fetch at a time
    private static int batchSize = 100; // Number of rows to include in each batch insert
    private static int maxDegreeOfParallelism = 5; // Maximum number of concurrent insert operations
    private CountdownEvent countdownEvent;

    public CopyDataAction(ActionExecutionAudit actionExecutionAudit, ILogger log, object copydata)
    {
        this.copydata = (CopyData)copydata;
        this.actionExecutionAudit = actionExecutionAudit;
        this.log = log;
    }
    public void execute()
    {

        // Retrieving the global config map for default value
        var configMap = ConfigAccess.GetCommonConfig();
        var pipelineId = actionExecutionAudit.PipelineId;
        var name = copydata.name;
        var source = string.IsNullOrWhiteSpace(copydata.source) ? throw new HandymanException("source data source cannot be empty for copydata for " + name) : copydata.source.Trim();
        var target = string.IsNullOrWhiteSpace(copydata.to) ? throw new HandymanException("target data source cannot be empty for copydata for " + name) : copydata.to.Trim();
        fetchSize = int.TryParse(copydata.fetchBatchSize?.Trim(), out var fetchSizeValue) && fetchSizeValue > 0
           ? fetchSizeValue
           : int.Parse(configMap.GetValueOrDefault(Constants.READ_SIZE, Constants.DEFAULT_READ_SIZE).Trim());
        var writeSize = int.TryParse(copydata.writeBatchSize?.Trim(), out var writeSizeValue) && writeSizeValue > 0
            ? writeSizeValue
            : int.Parse(configMap.GetValueOrDefault(Constants.WRITE_SIZE, Constants.DEFAULT_WRITE_SIZE).Trim());
        var upperThreadCount = int.TryParse(copydata.writeThreadCount?.Trim(), out var upperThreadCountValue) && upperThreadCountValue > 0
            ? upperThreadCountValue
            : int.Parse(configMap.GetValueOrDefault(Constants.WRITER_THREAD, Constants.DEFAULT_WRITER_COUNT).Trim());
        var lowerThreadCount = 1;
        // Retrieving the insert into SQL statement
        var insertStatement = string.IsNullOrWhiteSpace(copydata.value) ? throw new HandymanException("INSERT INTO SELECT .... cannot be empty for copydata for " + name)
            : copydata.value.Trim().Replace("\"", "");

        sourceConnectionString = source;
        targetConnectionString = target;
        batchSize = writeSize;
        TransferData();

    }

    public void TransferData()
    {
        var rowCount = GetRowCount(); // Get total number of rows in the table
        var offset = 0;

        // Initialize countdown event
        countdownEvent = new CountdownEvent((rowCount / fetchSize) + 1);

        // Fetch and process data in batches
        while (offset < rowCount)
        {
            var dataTable = RetrieveData(offset, fetchSize); // Fetch data in batches

            // Convert data into batches of insert statements
            var batches = ConvertToInsertBatches(dataTable);

            // Insert data into target database in parallel
            Parallel.ForEach(batches, new ParallelOptions { MaxDegreeOfParallelism = maxDegreeOfParallelism }, batch =>
            {
                InsertBatchData(batch);
                countdownEvent.Signal();
            });

            offset += fetchSize;
        }

        // Wait for all insert operations to complete
        countdownEvent.Wait();
        countdownEvent.Dispose();

        Console.WriteLine("Data transfer completed.");
    }

    private int GetRowCount()
    {
        using (var connection = (SqlConnection)ResourceAccess.GetSqlConnection(sourceConnectionString))
        {
            connection.Open();

            var command = new SqlCommand("SELECT COUNT(*) FROM movedata.movedata_src", connection);
            command.CommandType = CommandType.Text;

            var rowCount = Convert.ToInt32(command.ExecuteScalar());

            return rowCount;
        }
    }

    private DataTable RetrieveData(int offset, int fetchSize)
    {
        using (var connection = (SqlConnection)ResourceAccess.GetSqlConnection(sourceConnectionString))
        {
            connection.Open();

            var command = new SqlCommand($"{copydata.value} OFFSET {offset} ROWS FETCH NEXT {fetchSize} ROWS ONLY", connection);
            command.CommandType = CommandType.Text;

            var adapter = new SqlDataAdapter(command);
            var dataTable = new DataTable();
            adapter.Fill(dataTable);

            return dataTable;
        }
    }

    private ConcurrentQueue<string> ConvertToInsertBatches(DataTable dataTable)
    {
        var batches = new ConcurrentQueue<string>();
        var columnNames = dataTable.Columns.Cast<DataColumn>().Select(column => column.ColumnName);

        for (var i = 0; i < dataTable.Rows.Count; i += batchSize)
        {
            var batchRows = dataTable.AsEnumerable().Skip(i).Take(batchSize);
            var insertStatements = batchRows.Select(row =>
                $"INSERT INTO movedata.movedata_dest ({string.Join(", ", columnNames)}) VALUES ({string.Join(", ", row.ItemArray.Select(value => $"'{value}'"))})");

            batches.Enqueue(string.Join(";\n", insertStatements));
        }

        return batches;
    }

    private void InsertBatchData(string batch)
    {
        using (var connection = (SqlConnection)ResourceAccess.GetSqlConnection(targetConnectionString))
        {
            connection.Open();

            var command = new SqlCommand(batch, connection);
            command.CommandType = CommandType.Text;
            command.ExecuteNonQuery();
        }
    }



    public bool executeIf()
    {
        return copydata.condition;
    }
}

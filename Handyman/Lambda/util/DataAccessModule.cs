namespace DataAccessNS
{


    // public interface SpwProcessConfigRepo
    // {
    //     static string COLUMNS = " id, process, variable, value, active, created_by, created_date, last_modified_by, last_modified_date, version ";

    //     [Sql("INSERT INTO " + DoaConstant.CONFIG_SCHEMA_NAME + "." + DoaConstant.SPC_TABLE_NAME + " (id, process, variable, value, active, created_by, created_date, last_modified_by, last_modified_date, version) " +
    //             "VALUES (@id, @process, @variable, @value,@active,@createdBy, @createdDate, @lastModifiedBy, @lastModifiedDate, @version)")]
    //     Task InsertAsync(SpwProcessConfig spwProcessConfig);

    //     [Sql("UPDATE " + DoaConstant.CONFIG_SCHEMA_NAME + "." + DoaConstant.SPC_TABLE_NAME + " SET active=@active, created_by= @createdBy , created_date= @createdDate," +
    //             " last_modified_by= @lastModifiedBy, last_modified_date= @lastModifiedDate ,  process= @process , variable= @variable , value= @value WHERE id = @id ")]
    //     Task UpdateAsync(SpwProcessConfig spwProcessConfig);

    //     [Sql("SELECT " + COLUMNS + " FROM  " + DoaConstant.CONFIG_SCHEMA_NAME + "." + DoaConstant.SPC_TABLE_NAME + " WHERE active=true")]
    //     Task<IEnumerable<SpwProcessConfig>> FindAllAsync();
    // }

}
--- Data

INSERT INTO config.spw_common_config (id, variable, value, active, created_by, created_date, last_modified_by, last_modified_date, [version])
VALUES
    (1, 'xdbmetadata', 'xdbmetadata', 1, 1, GETDATE(), 1, GETDATE(), 1),
    (2, 'xdbfileprocess', 'xdbfileprocess', 1, 1, GETDATE(), 1, GETDATE(), 1),
    (3, 'marklogicdatapush', 'marklogicdatapush', 1, 1, GETDATE(), 1, GETDATE(), 1),
    (4, 'reconciliation', 'reconciliation', 1, 1, GETDATE(), 1, GETDATE(), 1),
    (5, 'cleanupdata', 'cleanupdata', 1, 1, GETDATE(), 1, GETDATE(), 1);

INSERT INTO config.spw_process_config (id, process, variable, value, active, created_by, created_date, last_modified_by, last_modified_date, [version])
VALUES
    (1, 'SYS_PACKAGE', 'raven.java.lib', 'in.handyman.raven.lib', 1, 1, GETDATE(), 1, GETDATE(), 1),
    (2, 'root.test_pipeline', 'filepath', '<Project-Path>/resources/lmdfiles/root.test_pipeline.lmd', 1, 1, GETDATE(), 1, GETDATE(), 1);

   

INSERT INTO config.spw_resource_config (id, config_name, resource_url, active, user_name, [password], driver_class, host, port, database_name, created_by, created_date, last_modified_by, last_modified_date, [version])
VALUES
    (1, 'db_conn', 'jdbc:postgresql://localhost:5432/zio_flatirons', 1, 'postgres', 'password', 'org.postgresql.Driver', 'localhost', '5432', 'zio_flatirons', 1, GETDATE(), 1, GETDATE(), 1),
    (2, 'db_conn_mssql', 'jdbc:mssql://localhost:1433/zio_csharp', 1, 'sa', 'password@123', 'com.microsoft.sqlserver', 'localhost', '1433', 'zio_csharp', 1, GETDATE(), 1, GETDATE(), 1),
    (5, 'db_conn_m8', 'jdbc:mysql://localhost:3307/newdb', 1, 'root', 'password', 'com.mysql.cj.jdbc.Driver', 'localhost', '3307', 'newdb', 1, GETDATE(), 1, GETDATE(), 1),
    (4, 'db_conn_m5', 'jdbc:mysql://localhost:3306/olddb', 0, 'root', 'password', 'com.mysql.jdbc.Driver', 'localhost', '3306', 'olddb', 1, GETDATE(), 1, GETDATE(), 1);
  

CREATE DATABASE zio_csharp;
use zio_csharp;

CREATE SCHEMA config;
CREATE SCHEMA audit;
CREATE SCHEMA movedata;

CREATE TABLE config.spw_common_config
(
    id BIGINT NOT NULL,
    variable VARCHAR(150) NOT NULL,
    value TEXT NOT NULL,
    active BIT NOT NULL,
    created_by BIGINT NOT NULL,
    created_date DATETIME2 NOT NULL,
    last_modified_by BIGINT NOT NULL,
    last_modified_date DATETIME2 NOT NULL,
    [version] INT NOT NULL,
    CONSTRAINT spw_common_config_pkey PRIMARY KEY (id)
);

CREATE TABLE config.spw_instance_config (
    id INT PRIMARY KEY,
    [instance] VARCHAR(150) NOT NULL,
    [process] VARCHAR(150) NOT NULL,
    variable VARCHAR(150) NOT NULL,
    value TEXT NOT NULL,
    active BIT NOT NULL,
    created_by INT NOT NULL,
    created_date DATETIME NOT NULL,
    last_modified_by INT NOT NULL,
    last_modified_date DATETIME NOT NULL,
    [version] INT NOT NULL
);


CREATE TABLE config.spw_process_config (
    id INT PRIMARY KEY,
    [process] VARCHAR(150) NOT NULL,
    variable VARCHAR(150) NOT NULL,
    value VARCHAR(500) NOT NULL,
    active BIT NOT NULL,
    created_by INT NOT NULL,
    created_date DATETIME NOT NULL,
    last_modified_by INT NOT NULL,
    last_modified_date DATETIME NOT NULL,
    [version] INT NOT NULL
);



CREATE TABLE config.spw_resource_config (
    id INT PRIMARY KEY,
    config_name VARCHAR(255) NOT NULL,
    resource_url TEXT NOT NULL,
    active BIT NOT NULL,
    user_name VARCHAR(255) NOT NULL,
    [password] VARCHAR(255) NOT NULL,
    driver_class VARCHAR(255) NOT NULL,
    host VARCHAR(255) NOT NULL,
    port VARCHAR(255) NOT NULL,
    database_name VARCHAR(255) NOT NULL,
    created_by INT NOT NULL,
    created_date DATETIME NOT NULL,
    last_modified_by INT NOT NULL,
    last_modified_date DATETIME NOT NULL,
    [version] INT NOT NULL
);


CREATE TABLE movedata.movedata_pipeline_audit (
    movedata_pipeline_audit_id int IDENTITY(1,1) NOT NULL,
    process_id bigint NOT NULL,
    status varchar(30) NOT NULL,
    message text NULL,
    created_on datetime2 NULL DEFAULT GETDATE()
);

   
   
CREATE TABLE audit.action_execution_audit (
    action_id BIGINT NOT NULL,
    created_by BIGINT NULL,
    created_date DATETIME NULL,
    last_modified_by BIGINT NULL,
    last_modified_date DATETIME NULL,
    context_node NVARCHAR(MAX) NULL,
    execution_status_id INT NULL,
    lambda_name VARCHAR(MAX) NULL,
    parent_action_id BIGINT NULL,
    parent_action_name VARCHAR(MAX) NULL,
    parent_pipeline_id BIGINT NULL,
    parent_pipeline_name VARCHAR(MAX) NULL,
    pipeline_name VARCHAR(MAX) NULL,
    action_name VARCHAR(MAX) NULL,
    execution_group_id INT NULL,
    input_node NVARCHAR(MAX) NULL,
    log NVARCHAR(MAX) NULL,
    pipeline_id BIGINT NULL,
    thread_name VARCHAR(MAX) NULL,
    root_pipeline_id BIGINT NULL,
    CONSTRAINT action_execution_audit_pkey PRIMARY KEY (action_id)
);

-- Drop table
-- DROP TABLE audit.action_execution_status_audit;

CREATE TABLE audit.action_execution_status_audit (
    id BIGINT NOT NULL,
    created_by BIGINT NOT NULL,
    created_date DATETIME NOT NULL,
    last_modified_by BIGINT NOT NULL,
    last_modified_date DATETIME NOT NULL,
    action_id BIGINT NOT NULL,
    execution_status_id INT NOT NULL,
    pipeline_id BIGINT NOT NULL,
    root_pipeline_id BIGINT NOT NULL,
    CONSTRAINT action_execution_status_audit_pkey PRIMARY KEY (id)
);

CREATE TABLE audit.execution_group (
    id INT NOT NULL,
    value VARCHAR(MAX) NOT NULL
);

CREATE TABLE audit.execution_status (
    id INT NOT NULL,
    value VARCHAR(MAX) NOT NULL,
    CONSTRAINT execution_status_pkey PRIMARY KEY (id)
);

-- Drop table
-- DROP TABLE audit.pipeline_execution_audit;

CREATE TABLE audit.pipeline_execution_audit (
    pipeline_id BIGINT NOT NULL,
    created_by BIGINT NULL,
    created_date DATETIME NULL,
    last_modified_by BIGINT NULL,
    last_modified_date DATETIME NULL,
    context_node NVARCHAR(MAX) NULL,
    execution_status_id INT NULL,
    lambda_name VARCHAR(MAX) NULL,
    parent_action_id BIGINT NULL,
    parent_action_name VARCHAR(MAX) NULL,
    parent_pipeline_id BIGINT NULL,
    parent_pipeline_name VARCHAR(MAX) NULL,
    pipeline_name VARCHAR(MAX) NULL,
    file_content NVARCHAR(MAX) NULL,
    host_name VARCHAR(MAX) NULL,
    mode_of_execution VARCHAR(MAX) NULL,
    pipeline_load_type VARCHAR(MAX) NULL,
    relative_path VARCHAR(MAX) NULL,
    request_body NVARCHAR(MAX) NULL,
    thread_name VARCHAR(MAX) NULL,
    process_name VARCHAR(MAX) NULL,
    root_pipeline_id BIGINT NULL,
    process_id BIGINT NULL,
    CONSTRAINT pipeline_execution_audit_pkey PRIMARY KEY (pipeline_id)
);

-- Drop table
-- DROP TABLE audit.pipeline_execution_status_audit;

CREATE TABLE audit.pipeline_execution_status_audit (
    id BIGINT NOT NULL,
    created_by BIGINT NULL,
    created_date DATETIME NULL,
    last_modified_by BIGINT NULL,
    last_modified_date DATETIME NULL,
    execution_status_id INT NULL,
    pipeline_id BIGINT NULL,
    root_pipeline_id BIGINT NULL,
    CONSTRAINT pipeline_execution_status_audit_pkey PRIMARY KEY (id)
);


-- Drop table
-- DROP TABLE audit.statement_execution_audit;

CREATE TABLE audit.statement_execution_audit (
    statement_id BIGINT NOT NULL,
    created_by BIGINT NULL,
    created_date DATETIME NULL,
    last_modified_by BIGINT NULL,
    last_modified_date DATETIME NULL,
    action_id BIGINT NULL,
    rows_processed INT NULL,
    rows_read INT NULL,
    rows_written INT NULL,
    statement_content NVARCHAR(MAX) NULL,
    time_taken FLOAT NULL,
    root_pipeline_id BIGINT NULL,
    CONSTRAINT statement_execution_audit_pkey PRIMARY KEY (statement_id)
);

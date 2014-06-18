DECLARE @Statements VARCHAR(8000);

CREATE TABLE #Statements (Id int identity(1,1) NOT NULL, [STATEMENT] nvarchar(max))

-- this code creates the drop FK statements
INSERT INTO #Statements ([Statement])
SELECT
'ALTER TABLE ' + QUOTENAME(OBJECT_SCHEMA_NAME(fk.parent_object_id))
   + '.' + QUOTENAME(OBJECT_NAME(fk.parent_object_id))
   + ' DROP CONSTRAINT ' + QUOTENAME(fk.name) + ';'
FROM sys.foreign_keys AS fk
INNER JOIN sys.tables AS t
ON fk.referenced_object_id = t.[object_id]
WHERE t.name LIKE 'aspnet[_]%';

-- this code creates the trancate table statements
INSERT INTO #Statements ([Statement])
SELECT 'TRUNCATE TABLE ' + QUOTENAME(name) + ';'
FROM sys.tables 
WHERE name LIKE 'aspnet[_]%';

-- this is creating the SQL to run
SELECT @Statements = COALESCE(@Statements + '
', '') + STATEMENT 
FROM #Statements;

-- removing the temp tables
DROP TABLE #Statements;

-- run the statements
EXEC(@Statements);
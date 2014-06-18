DECLARE @Statements VARCHAR(8000);

-- this code creates the drop FK statements
SELECT
'ALTER TABLE ' + QUOTENAME(OBJECT_SCHEMA_NAME(fk.parent_object_id))
   + '.' + QUOTENAME(OBJECT_NAME(fk.parent_object_id))
   + ' DROP CONSTRAINT ' + QUOTENAME(fk.name) + ';' AS STATEMENT
INTO #DROPFKSTATEMENTS
FROM sys.foreign_keys AS fk
INNER JOIN sys.tables AS t
ON fk.referenced_object_id = t.[object_id]
WHERE t.name LIKE 'aspnet[_]%';

-- this code creates the trancate table statements
SELECT 'TRUNCATE TABLE ' + QUOTENAME(name) + ';' AS STATEMENT
INTO #TRUNCATESTATEMENTS
FROM sys.tables 
WHERE name LIKE 'aspnet[_]%';

-- this is creating the SQL to run
SELECT @Statements = COALESCE(@Statements + '
', '') + STATEMENT 
FROM #DROPFKSTATEMENTS;
SELECT @Statements = COALESCE(@Statements + '
', '') + STATEMENT 
FROM #TRUNCATESTATEMENTS;

-- removing the temp tables
DROP TABLE #TRUNCATESTATEMENTS;
DROP TABLE #DROPFKSTATEMENTS;

-- run the statements
EXEC(@Statements);
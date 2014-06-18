DECLARE @Statements VARCHAR(8000);

CREATE TABLE #Statements (Id int identity(1,1) NOT NULL, [STATEMENT] nvarchar(max))

-- this code creates the drop Procedures statements
INSERT INTO #Statements ([Statement])
SELECT
'DROP PROCEDURE [' +  s.name + '].[' + p.name + '];'
FROM sys.procedures p
JOIN sys.schemas s on p.schema_id = s.schema_id
WHERE p.NAME LIKE '%aspnet_%';

-- this code creates the drop views statements
INSERT INTO #Statements ([Statement])
SELECT
'DROP VIEW [' +  s.name + '].[' + v.name + '];'
FROM sys.views v
JOIN sys.schemas s on v.schema_id = s.schema_id
WHERE v.NAME LIKE '%vw_aspnet_%';

-- this code creates the drop views statements
INSERT INTO #Statements ([Statement])
SELECT
'DROP TABLE [' +  s.name + '].[' + t.name + '];'
FROM sys.tables t
JOIN sys.schemas s on t.schema_id = s.schema_id
WHERE t.NAME LIKE '%aspnet_%';

-- this code creates the drop views statements
INSERT INTO #Statements ([Statement])
SELECT
'DROP ROLE [' + r.name + '];'
FROM sys.database_principals r
WHERE r.Type = 'R' AND r.name LIKE '%aspnet_%'

SELECT @Statements = COALESCE(@Statements + '
', '') + STATEMENT 
FROM #Statements;

-- removing the temp tables
DROP TABLE #Statements;

-- run the statements
EXEC(@Statements);
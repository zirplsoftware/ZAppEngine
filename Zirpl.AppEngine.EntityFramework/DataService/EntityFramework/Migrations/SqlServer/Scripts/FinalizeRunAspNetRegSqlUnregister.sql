DECLARE @Statements VARCHAR(8000);

-- this code creates the drop Procedures statements
SELECT
'DROP PROCEDURE [' +  s.name + '].[' + p.name + '];' AS STATEMENT
INTO #DROPPROCEDURESSTATEMENTS
FROM sys.procedures p
JOIN sys.schemas s on p.schema_id = s.schema_id
WHERE p.NAME LIKE '%aspnet_%';

-- this code creates the drop views statements
SELECT
'DROP VIEW [' +  s.name + '].[' + v.name + '];' AS STATEMENT
INTO #DROPVIEWSSTATEMENTS
FROM sys.views v
JOIN sys.schemas s on v.schema_id = s.schema_id
WHERE v.NAME LIKE '%vw_aspnet_%';

-- this code creates the drop views statements
SELECT
'DROP TABLE [' +  s.name + '].[' + t.name + '];' AS STATEMENT
INTO #DROPTABLESTATEMENTS
FROM sys.tables t
JOIN sys.schemas s on t.schema_id = s.schema_id
WHERE t.NAME LIKE '%aspnet_%';

-- this code creates the drop views statements
SELECT
'DROP ROLE [' + r.name + '];' AS STATEMENT
INTO #DROPROLESTATEMENTS
FROM sys.database_principals r
WHERE r.Type = 'R' AND r.name LIKE '%aspnet_%'

SELECT @Statements = COALESCE(@Statements + '
', '') + STATEMENT 
FROM #DROPPROCEDURESSTATEMENTS;
SELECT @Statements = COALESCE(@Statements + '
', '') + STATEMENT 
FROM #DROPVIEWSSTATEMENTS;
SELECT @Statements = COALESCE(@Statements + '
', '') + STATEMENT 
FROM #DROPTABLESTATEMENTS;
SELECT @Statements = COALESCE(@Statements + '
', '') + STATEMENT 
FROM #DROPROLESTATEMENTS;

-- removing the temp tables
DROP TABLE #DROPPROCEDURESSTATEMENTS;
DROP TABLE #DROPVIEWSSTATEMENTS;
DROP TABLE #DROPTABLESTATEMENTS;
DROP TABLE #DROPROLESTATEMENTS;

-- run the statements
EXEC(@Statements);
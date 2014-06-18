-- We would use the code below, but SQL Azure does not support this
--CREATE SEQUENCE dbo.[PromoCodeSequence] AS bigint START WITH 362852159 INCREMENT BY 3 NO CYCLE

-- So instead we will use this code:
CREATE PROCEDURE dbo.usp_GetCurrent{0}
as
begin
	SELECT CAST(IDENT_CURRENT(TABLE_NAME) AS bigint)
	FROM INFORMATION_SCHEMA.TABLES
	WHERE OBJECTPROPERTY(OBJECT_ID(TABLE_NAME), 'TableHasIdentity') = 1
	AND TABLE_TYPE = 'BASE TABLE'
	AND TABLE_NAME = '{0}'
end;
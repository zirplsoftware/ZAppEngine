-- We would use the code below, but SQL Azure does not support this
--CREATE SEQUENCE dbo.[PromoCodeSequence] AS bigint START WITH 362852159 INCREMENT BY 3 NO CYCLE

-- So instead we will use this code:
CREATE PROCEDURE dbo.usp_GetNext{0}
as
begin
      declare @NewSeqValue int
      set NOCOUNT ON
      insert into {0}(TempValue) values (1)
     
      set @NewSeqValue = scope_identity()
     
      delete from {0} WITH (READPAST)
SELECT CAST(@NewSeqValue AS bigint)
end;
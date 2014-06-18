-- We would use the code below, but SQL Azure does not support this
--CREATE SEQUENCE dbo.[PromoCodeSequence] AS bigint START WITH 362852159 INCREMENT BY 3 NO CYCLE

-- So instead we will use this code:
CREATE TABLE {0} (
      Id bigint identity({1},{2}) primary key,
      TempValue bit
);
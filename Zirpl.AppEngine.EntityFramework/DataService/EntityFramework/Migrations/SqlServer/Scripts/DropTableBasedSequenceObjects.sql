-- We would use the code below, but SQL Azure does not support this
--DROP SEQUENCE dbo.[PromoCodeSequence]

-- So instead we will use this code:
DROP TABLE [dbo].[{0}];
DROP PROCEDURE [dbo].[usp_GetNext{0}];
DROP PROCEDURE [dbo].[usp_GetCurrent{0}];
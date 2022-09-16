CREATE PROCEDURE [dbo].[Account_Update]
	@Id BIGINT,
	@Status TINYINT

AS
BEGIN

UPDATE dbo.Account

SET [Status]=@Status
	
WHERE 
Id=@Id

END

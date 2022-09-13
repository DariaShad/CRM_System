CREATE PROCEDURE [dbo].[Account_Update]
	@Id BIGINT,
	@Status TINYINT,
	@IsDeleted BIT 

AS
BEGIN

UPDATE dbo.Account
SET [Status]=@Status, IsDeleted = @IsDeleted
	
WHERE 
Id=@Id

END

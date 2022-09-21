CREATE PROCEDURE [dbo].[Account_Restore]
	@Id int

AS
BEGIN

UPDATE dbo.Account
SET IsDeleted = 0
WHERE Id=@Id

END

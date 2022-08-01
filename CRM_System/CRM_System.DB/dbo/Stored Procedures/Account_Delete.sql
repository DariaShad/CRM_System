CREATE PROCEDURE [dbo].[Account_Delete]
	@Id int

AS
BEGIN

UPDATE dbo.Account
SET IsDeleted = 1
WHERE Id=@Id

END

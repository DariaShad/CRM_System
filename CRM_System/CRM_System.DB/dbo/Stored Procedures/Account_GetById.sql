CREATE PROCEDURE [dbo].[Account_GetById]
	@Id int

AS
BEGIN

	SELECT Id, Currency, [Status], LeadId
	FROM dbo.Account
	WHERE Id=@Id

	END

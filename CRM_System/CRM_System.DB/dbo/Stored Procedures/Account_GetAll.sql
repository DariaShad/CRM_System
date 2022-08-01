CREATE PROCEDURE [dbo].[Account_GetAll]
	
AS
BEGIN
	SELECT Id, Currency, [Status], LeadId
	FROM dbo.Account
	WHERE (IsDeleted=0)

END

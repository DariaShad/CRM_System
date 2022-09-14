CREATE PROCEDURE [dbo].[Account_GetById]
	@Id int

AS
BEGIN

	SELECT Id, TradingCurrency, [Status], LeadId, IsDeleted
	FROM dbo.Account
	WHERE Id=@Id

	END

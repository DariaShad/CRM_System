CREATE PROCEDURE [dbo].[Account_GetAll]
	
AS
BEGIN
	SELECT Id, TradingCurrency, [Status], LeadId
	FROM dbo.Account
	WHERE (IsDeleted=0)

END

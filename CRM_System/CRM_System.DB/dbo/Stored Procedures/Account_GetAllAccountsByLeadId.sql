CREATE PROCEDURE [dbo].[Account_GetAllAccountsByLeadId]
@LeadId int
	AS
BEGIN
	SELECT Id, TradingCurrency, [Status], LeadId, IsDeleted
	FROM dbo.Account
	WHERE (IsDeleted=0) AND LeadId=@LeadId

END

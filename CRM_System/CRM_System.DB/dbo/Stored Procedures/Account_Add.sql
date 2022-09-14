CREATE PROCEDURE [dbo].[Account_Add]
	@TradingCurrency TINYINT,
	@Status TINYINT,
	@LeadId INT
AS
BEGIN
INSERT INTO dbo.Account(
	TradingCurrency,
	[Status],
	LeadId) 
	VALUES(
	@TradingCurrency,
	@Status,
	@LeadId)

	SELECT @@IDENTITY
END

CREATE PROCEDURE [dbo].[Account_Add]
	@Currency TINYINT,
	@Status TINYINT,
	@LeadId INT
AS
BEGIN
INSERT INTO dbo.Account(
	Currency,
	[Status],
	LeadId) 
	VALUES(
	@Currency,
	@Status,
	@LeadId)

	SELECT @@IDENTITY
END

CREATE PROCEDURE [dbo].[Account_Add]
	@Currency NVARCHAR(10),
	@Status NVARCHAR(10),
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

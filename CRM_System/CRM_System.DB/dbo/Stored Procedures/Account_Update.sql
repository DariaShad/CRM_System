CREATE PROCEDURE [dbo].[Account_Update]
	@Id BIGINT,
	@Currency TINYINT,
	@Status TINYINT,
	@LeadId INT

AS
BEGIN

UPDATE dbo.Account
SET [Status]=@Status
	
WHERE 
Id=@Id

END

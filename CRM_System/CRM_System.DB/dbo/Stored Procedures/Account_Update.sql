CREATE PROCEDURE [dbo].[Account_Update]
	@Id int,
	@Currency NVARCHAR(10),
	@Status NVARCHAR(10),
	@LeadId INT

AS
BEGIN

UPDATE dbo.Account
SET Currency=@Currency,
	[Status]=@Status,
	LeadId=@LeadId

WHERE 
Id=@Id

END

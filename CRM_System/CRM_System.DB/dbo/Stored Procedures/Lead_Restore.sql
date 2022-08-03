CREATE PROCEDURE [dbo].[Lead_Restore]
	@Id int

AS
BEGIN

UPDATE dbo.[Lead]
SET IsDeleted = 0
WHERE Id = @Id

END
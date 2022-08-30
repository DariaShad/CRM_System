CREATE PROCEDURE [dbo].[Lead_Delete]
	@Id int,
	@IsDeleted bit

AS
BEGIN

UPDATE dbo.[Lead]
SET IsDeleted = @IsDeleted
WHERE Id = @Id

END

CREATE PROCEDURE [dbo].[Lead_UpdateRole]

	@Id int,
	@Role tinyint

AS
BEGIN

UPDATE dbo.[Lead]
SET 
[Role] = @Role
WHERE Id = @Id

END

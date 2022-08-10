CREATE PROCEDURE [dbo].[Admin_Add]
	@Email nvarchar(50),
	@Password nvarchar(255),
	@Role tinyint

AS
BEGIN
INSERT INTO dbo.[Admin](
	Email,
	[Password],
	[Role])
VALUES(
	@Email,
	@Password,
	@Role)

SELECT @@IDENTITY
END


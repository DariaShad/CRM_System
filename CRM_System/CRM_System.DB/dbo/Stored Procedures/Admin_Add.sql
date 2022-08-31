CREATE PROCEDURE [dbo].[Admin_Add]
	@Email nvarchar(50),
	@Password nvarchar(255)

AS
BEGIN
INSERT INTO dbo.[Admin](
	Email,
	[Password])
VALUES(
	@Email,
	@Password)

SELECT @@IDENTITY
END


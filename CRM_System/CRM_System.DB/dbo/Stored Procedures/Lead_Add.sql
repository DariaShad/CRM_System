CREATE PROCEDURE [dbo].[Lead_Add]
	@FirstName nvarchar(50),
	@LastName nvarchar(50),
	@Patronymic nvarchar(50),
	@Birthday date,
	@Email nvarchar(50),
	@Phone nvarchar(25),
	@Passport nvarchar(20),
	@Address nvarchar(100),
	@Role nvarchar(20)
AS
BEGIN
INSERT INTO dbo.[Lead](
	FirstName,
	LastName,
	Patronymic,
	Birthday,
	Email,
	Phone,
	Passport,
	[Address],
	[Role])
VALUES(
	@FirstName,
	@LastName,
	@Patronymic,
	@Birthday,
	@Email,
	@Phone,
	@Passport,
	@Address,
	@Role)

SELECT @@IDENTITY
END

CREATE PROCEDURE [dbo].[Lead_Add]
	@FirstName nvarchar(50),
	@LastName nvarchar(50),
	@Patronymic nvarchar(50),
	@Birthday datetime,
	@Email nvarchar(50),
	@Phone nvarchar(15),
	@Passport nvarchar(150),
	@City tinyint,
	@Address nvarchar(60),
	@Role tinyint,
	@Password nvarchar(255),
	@RegistarionDate datetime
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
	City,
	[Address],
	[Role],
	[Password],
	RegistrationDate)
VALUES(
	@FirstName,
	@LastName,
	@Patronymic,
	@Birthday,
	@Email,
	@Phone,
	@Passport,
	@City,
	@Address,
	@Role,
	@Password,
	GETDATE())

SELECT @@IDENTITY
END

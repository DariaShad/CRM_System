CREATE PROCEDURE [dbo].[Lead_Update]
	@Id int,
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

UPDATE dbo.[Lead]
SET FirstName = @FirstName,
	LastName = @LastName,
	Patronymic = @Patronymic,
	Birthday = @Birthday,
	Email = @Email,
	Phone = @Phone,
	Passport = @Passport,
	[Address] = @Address,
	[Role] = @Role
WHERE Id = @Id

END
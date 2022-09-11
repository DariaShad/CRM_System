CREATE PROCEDURE [dbo].[Lead_Update]
	@Id int,
	@FirstName nvarchar(50),
	@LastName nvarchar(50),
	@Patronymic nvarchar(50),
	@Birthday date,
	@Phone nvarchar(15),
	@City tinyint,
	@Address nvarchar(60)

AS
BEGIN

UPDATE dbo.[Lead]
SET FirstName = @FirstName,
	LastName = @LastName,
	Patronymic = @Patronymic,
	Birthday = @Birthday,
	Phone = @Phone,
	City = @City,
	[Address] = @Address

WHERE Id = @Id

END
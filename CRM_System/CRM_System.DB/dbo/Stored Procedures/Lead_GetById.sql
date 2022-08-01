CREATE PROCEDURE [dbo].[Lead_GetById]
	@Id int

AS
BEGIN

	SELECT Id, FirstName, LastName, Patronymic, Birthday, Email, Phone,
	Passport, City, [Address], [Role], RegistrationDate
	FROM dbo.[Lead]
	WHERE Id = @Id AND IsDeleted = 0

END
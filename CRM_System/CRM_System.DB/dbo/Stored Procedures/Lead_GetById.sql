CREATE PROCEDURE [dbo].[Lead_GetById]
	@Id int

AS
BEGIN

	SELECT Id, FirstName, LastName, Patronymic, Birthday, Email, Phone,
	Passport, [Address], [Role]
	FROM dbo.[Lead]
	WHERE Id = @Id

END
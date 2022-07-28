CREATE PROCEDURE [dbo].[Lead_GetAll]

AS
BEGIN

	SELECT Id, FirstName, LastName, Patronymic, Birthday, Email, Phone,
	Passport, [Address], [Role]
	FROM dbo.[Lead]
	WHERE (IsDeleted = 0)

END

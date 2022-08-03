CREATE PROCEDURE [dbo].[Lead_GetLeadByEmail]
 @Email nvarchar(50)

as
begin

	select Id, FirstName, LastName, Patronymic, Birthday, Phone,
	Passport, City, [Address], [Role], RegistrationDate
	from dbo.[Lead]
	where @Email = Email and IsDeleted = 0

end
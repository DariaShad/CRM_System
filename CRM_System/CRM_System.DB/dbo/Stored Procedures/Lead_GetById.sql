CREATE PROCEDURE [dbo].[Lead_GetById]
	@Id int

AS
BEGIN

	SELECT 
		L.Id, 
		L.FirstName, 
		L.LastName, 
		L.Patronymic, 
		L.Birthday, 
		L.Email, 
		L.Phone,
		L.Passport, 
		L.City, 
		L.[Address], 
		L.[Role], 
		L.RegistrationDate,
		L.IsDeleted,
		A.Id,
		A.TradingCurrency,
		A.[Status]
	FROM dbo.[Lead] as L
	
	LEFT JOIN dbo.Account as A on (A.LeadId = L.Id)

	WHERE L.Id = @Id
END
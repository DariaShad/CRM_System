CREATE PROCEDURE [dbo].[Lead_GetAllInfoByLeadId]
 @Id int

as
Begin

select 
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
		A.Id, 
		A.TradingCurrency, 
		A.[Status] from [dbo].[Lead] as L
	left join [dbo].[Account] as A on L.Id = A.LeadId

	where L.Id = @Id and L.IsDeleted = 0 
end
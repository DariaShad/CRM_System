CREATE PROCEDURE [dbo].[Lead_GetLeadByEmail]
 @Email nvarchar(50)

as
begin

	select 
		Id, 
		FirstName, 
		LastName, 
		[Password]
	from dbo.[Lead]
	where Email = @Email and IsDeleted = 0

end
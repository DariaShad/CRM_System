CREATE PROCEDURE [dbo].[Admin_GetAdminByEmail]
 @Email nvarchar(50)

as
begin

	select 
		Id, 
		Email,
		[Password]
	from dbo.[Admin]
	where Email = @Email and IsDeleted = 0

end

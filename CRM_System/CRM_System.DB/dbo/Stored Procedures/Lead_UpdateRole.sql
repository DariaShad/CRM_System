CREATE PROCEDURE [dbo].[Lead_UpdateRole]
 @ids intTable READONLY
AS
BEGIN
    
     UPDATE [dbo].[Lead]
	 SET [Role] = 2 
     where [Id] in (select Id from @ids)
     Update [dbo].[Lead]
	 SET [Role] = 1 
     where [Id] not in (select Id from @ids)
END

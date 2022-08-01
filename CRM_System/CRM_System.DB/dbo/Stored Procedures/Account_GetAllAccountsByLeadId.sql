﻿CREATE PROCEDURE [dbo].[Account_GetAllAccountsByLeadId]
@LeadId int
	AS
BEGIN
	SELECT Id, Currency, [Status], LeadId
	FROM dbo.Account
	WHERE (IsDeleted=0) AND LeadId=@LeadId

END

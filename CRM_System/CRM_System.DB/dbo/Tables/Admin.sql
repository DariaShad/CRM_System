﻿CREATE TABLE [dbo].[Admin]
(
	[Id] INT NOT NULL PRIMARY KEY,
	[Email]     NVARCHAR (50) UNIQUE NOT NULL,
	[Password] NVARCHAR(255) NOT NULL,
	[Role] TINYINT NOT NULL,
	[IsDeleted] BIT DEFAULT 0 NOT NULL
)

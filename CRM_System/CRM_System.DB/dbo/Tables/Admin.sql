CREATE TABLE [dbo].[Admin]
(
	[Id] BIGINT IDENTITY NOT NULL PRIMARY KEY,
	[Email]     NVARCHAR (50) UNIQUE NOT NULL,
	[Password] NVARCHAR(255) NOT NULL,
	[IsDeleted] BIT DEFAULT 0 NOT NULL
)

CREATE TABLE [dbo].[Lead]
(
	[Id] INT IDENTITY (1, 1) NOT NULL PRIMARY KEY,
	[FirstName] NVARCHAR (50) NOT NULL,
    [LastName]  NVARCHAR (50) NOT NULL,
    [Patronymic]  NVARCHAR (50) NOT NULL,
    [Birthday] DATE NOT NULL,
    [Email]     NVARCHAR (50) NOT NULL,
    [Phone]     NVARCHAR (25) NOT NULL,
    [Passport] NVARCHAR(20) NOT NULL,
    [Address] NVARCHAR(100) NOT NULL,
    [Role] NVARCHAR(20) NOT NULL,
    [IsDeleted] BIT DEFAULT 0 NOT NULL
)

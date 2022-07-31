CREATE TABLE [dbo].[Lead]
(
	[Id] INT IDENTITY (1, 1) NOT NULL PRIMARY KEY,
	[FirstName] NVARCHAR (50) NOT NULL,
    [LastName]  NVARCHAR (50) NOT NULL,
    [Patronymic]  NVARCHAR (50) NOT NULL,
    [Birthday] DATE NOT NULL,
    [Email]     NVARCHAR (50) UNIQUE NOT NULL,
    [Phone]     NVARCHAR (15) NOT NULL,
    [Passport] NVARCHAR(150) NOT NULL,
    [City] TINYINT NOT NULL,
    [Address] NVARCHAR(60) NOT NULL,
    [Role] TINYINT NOT NULL,
    [RegistrationDate] DATE NOT NULL,
    [IsDeleted] BIT DEFAULT 0 NOT NULL
)

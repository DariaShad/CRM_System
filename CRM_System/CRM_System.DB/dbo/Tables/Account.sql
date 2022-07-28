CREATE TABLE [dbo].[Account]
(
	[Id] INT IDENTITY (1, 1) NOT NULL PRIMARY KEY,
	[Currency] NVARCHAR(10) NOT NULL,
	[Status] NVARCHAR(10) NOT NULL,
	[LeadId] INT NOT NULL,
	[IsDeleted] BIT DEFAULT 0 NOT NULL,
	FOREIGN KEY ([LeadId]) REFERENCES [dbo].[Lead] ([Id])
);

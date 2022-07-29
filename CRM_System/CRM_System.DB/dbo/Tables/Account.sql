CREATE TABLE [dbo].[Account]
(
	[Id] BIGINT IDENTITY (1, 1) NOT NULL PRIMARY KEY,
	[Currency] TINYINT NOT NULL,
	[Status] TINYINT NOT NULL,
	[LeadId] INT NOT NULL,
	[IsDeleted] BIT DEFAULT 0 NOT NULL,
	FOREIGN KEY ([LeadId]) REFERENCES [dbo].[Lead] ([Id])
);

CREATE TABLE [dbo].[Account]
(
	[Id] BIGINT IDENTITY (1, 1) NOT NULL PRIMARY KEY,
	[Currency] TINYINT NOT NULL,
	[Status] TINYINT NOT NULL,
	[LeadId] INT NOT NULL,
	[IsDeleted] BIT DEFAULT 0 NOT NULL,
	FOREIGN KEY ([LeadId]) REFERENCES [dbo].[Lead] ([Id])
);
GO
CREATE NONCLUSTERED INDEX [NonClusteredIndex-20220829-171313] ON [dbo].[Account]
(
	[LeadId] ASC
)
INCLUDE([Id],[Currency],[Status]) ON [PRIMARY]
GO

CREATE TABLE [dbo].[Homework]
(
	[ID]			INT IDENTITY (1,1)	NOT NULL,
	[Title]			NVARCHAR(64)		NOT NULL,
	[DueDate]		Date				NOT NULL,
	[DueTime]		Time				NOT NULL,
	[HWPriority]	INT					NOT NULL,
	[Department]	NVARCHAR(64)		NOT NULL,
	[Course]		INT					NOT NULL,
	[Notes]			NVARCHAR(128),
	
	CONSTRAINT [PK_dbo.Homework] PRIMARY KEY CLUSTERED([ID] ASC)
)


GO
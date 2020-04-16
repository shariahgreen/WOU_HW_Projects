﻿CREATE TABLE [dbo].[Coaches]
(
	[ID]		INT IDENTITY (1,1)	NOT NULL,
	[CoachName]	NVARCHAR(128)		NOT NULL,
	CONSTRAINT [PK_dbo.Coaches] PRIMARY KEY CLUSTERED ([ID] ASC)
);

CREATE TABLE [dbo].[Teams]
(
	[ID]		INT IDENTITY (1,1)	NOT NULL,
	[TeamName]	NVARCHAR(128)		NOT NULL,
	[CoachID]	INT				NOT NULL,
	CONSTRAINT [PK_dbo.Teams] PRIMARY KEY CLUSTERED ([ID] ASC),
	CONSTRAINT [FK_dbo.Teams.Coach_ID] FOREIGN KEY ([CoachID]) REFERENCES [dbo].[Coaches] ([ID]) ON DELETE CASCADE
);

CREATE TABLE [dbo].[Meets]
(
	[ID]		INT IDENTITY (1,1)		NOT NULL,
	[MeetLocation]	NVARCHAR(128)		NOT NULL,
	[MeetDate]		DateTime			NOT NULL,
	CONSTRAINT [PK_dbo.Meets] PRIMARY KEY CLUSTERED ([ID] ASC)
);

CREATE TABLE [dbo].[Categories]
(
	[ID]			INT IDENTITY (1,1)		NOT NULL,
	[CategoryName]	NVARCHAR(50)			NOT NULL,
	CONSTRAINT [PK_dbo.Categories] PRIMARY KEY CLUSTERED ([ID] ASC)
);

CREATE TABLE  [dbo].[Races]
(
	[ID]			INT IDENTITY (1,1)		NOT NULL,
	[RaceName]		NVARCHAR(128)			NOT NULL,
	CONSTRAINT [PK_dbo.Races] PRIMARY KEY CLUSTERED ([ID] ASC),
);

CREATE TABLE [dbo].[Athletes]
(
	[ID]			INT IDENTITY (1,1)		NOT NULL,
	[AthleteName]	NVARCHAR(128)			NOT NULL,
	[TeamID]		INT						NOT NULL,
	[CategoryID]	INT						NOT NULL,
	CONSTRAINT [PK_dbo.Athletes] PRIMARY KEY CLUSTERED ([ID] ASC),
	CONSTRAINT [FK_dbo.Athletes.Team_ID] FOREIGN KEY ([TeamID]) REFERENCES [dbo].[Teams] ([ID]) ON DELETE CASCADE,
	CONSTRAINT [FK_dbo.Athletes.Category_ID] FOREIGN KEY ([CategoryID]) REFERENCES [dbo].[Categories] ([ID]) ON DELETE CASCADE
);

CREATE TABLE [dbo].[Results]
(
	[ID]		INT IDENTITY (1,1)		NOT NULL,
	[RaceID]	INT						NOT NULL,
	[MeetID]	INT						NOT NULL,
	[RaceTime]	Real					NOT NULL,
	[AthleteID]	INT						NOT NULL,
	CONSTRAINT [PK_dbo.Results] PRIMARY KEY CLUSTERED ([ID] ASC),
	CONSTRAINT [FK_dbo.Results.Race_ID] FOREIGN KEY ([RaceID]) REFERENCES [dbo].[Races] ([ID]) ON DELETE CASCADE,
	CONSTRAINT [FK_dbo.Results.Meet_ID] FOREIGN KEY ([MeetID]) REFERENCES [dbo].[Meets] ([ID]) ON DELETE CASCADE,
	CONSTRAINT [FK_dbo.Results.Athlete_ID] FOREIGN KEY ([AthleteID]) REFERENCES [dbo].[Athletes] ([ID]) ON DELETE CASCADE
);
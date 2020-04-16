CREATE TABLE [dbo].[AllData]
(
	[Location] NVARCHAR(50),
	[MeetDate] DATETIME,
	[Team] NVARCHAR(50),
	[Coach] NVARCHAR(50),
	[Event] NVARCHAR(20),
	[Gender] NVARCHAR(20),
	[Athlete] NVARCHAR(50),
	[RaceTime] REAL
);

BULK INSERT [dbo].[AllData]
	FROM 'C:\TEMP\racetimes.csv'
	WITH
	(
		FIELDTERMINATOR = ',',
		ROWTERMINATOR = '\n',
		FIRSTROW = 2
	);

SELECT * FROM [dbo].[AllData];

INSERT INTO [dbo].[Categories] ([CategoryName])
	SELECT DISTINCT Gender from [dbo].[AllData];

INSERT INTO [dbo].[Coaches] ([CoachName])
	SELECT DISTINCT Coach from [dbo].[AllData];

INSERT INTO [dbo].[Teams]
	(TeamName,CoachID)
	SELECT DISTINCT ad.Team,cs.ID
		FROM dbo.AllData as ad, dbo.Coaches as cs
		WHERE ad.Coach = cs.CoachName;

INSERT INTO [dbo].[Races]
	(RaceName)
	SELECT DISTINCT Event from [dbo].[AllData];

INSERT INTO [dbo].[Meets]
	(MeetLocation, MeetDate)
	SELECT DISTINCT ad.Location, ad.MeetDate
		FROM dbo.AllData as ad;

INSERT INTO [dbo].[Athletes]
	(AthleteName, TeamID, CategoryID)
	SELECT DISTINCT ad.Athlete, tm.ID, ct.ID
	FROM dbo.AllData as ad, dbo.Teams as tm, dbo.Categories as ct
	WHERE ad.Team = tm.TeamName and ad.Gender = ct.CategoryName;

INSERT INTO [dbo].[Results]
	(RaceID, MeetID, AthleteID, RaceTime)
	SELECT DISTINCT rs.ID, mt.ID, al.ID, ad.RaceTime
	FROM [dbo].[AllData] as ad, [dbo].[Races] as rs, [dbo].[Meets] as mt, [dbo].[Athletes] as al
	WHERE ad.Location = mt.MeetLocation AND ad.Event = rs.RaceName AND ad.Athlete = al.AthleteName;


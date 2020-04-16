ALTER TABLE [dbo].[Athletes] DROP CONSTRAINT [FK_dbo.Athletes.Team_ID];
ALTER TABLE [dbo].[Races] DROP CONSTRAINT [FK_dbo.Races.Category_ID];
ALTER TABLE [dbo].[Teams] DROP CONSTRAINT [FK_dbo.Teams.Coach_ID];
ALTER TABLE [dbo].[Results] DROP CONSTRAINT [FK_dbo.Results.Athlete_ID];
ALTER TABLE [dbo].[Results] DROP CONSTRAINT [FK_dbo.Results.Meet_ID];
ALTER TABLE [dbo].[Results] DROP CONSTRAINT [FK_dbo.Results.Race_ID];

DROP TABLE [dbo].[Coaches];
DROP TABLE [dbo].[Teams];
DROP TABLE [dbo].[Meets];
DROP TABLE [dbo].[Races];
DROP TABLE [dbo].[Categories];
DROP TABLE [dbo].[Results];
DROP TABLE [dbo].[Athletes];
DROP TABLE [dbo].[AllData];
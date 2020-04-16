ALTER TABLE [dbo].[Events] DROP CONSTRAINT [FK_dbo.Events.Coordinator];
ALTER TABLE [dbo].[RSVPs] DROP CONSTRAINT [FK_dbo.RSVPs.Event];
ALTER TABLE [dbo].[RSVPs] DROP CONSTRAINT [FK_dbo.RSVPs.Person];

DROP TABLE [dbo].[Events];
DROP TABLE [dbo].[Persons];
DROP TABLE [dbo].[RSVPs];
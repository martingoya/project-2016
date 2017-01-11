
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 01/10/2017 22:52:27
-- Generated from EDMX file: C:\Users\marti\Source\Repos\project-2016\Model\Models\Model.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [DB_108573_ohlalaph];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[FK_Event_Image]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Event] DROP CONSTRAINT [FK_Event_Image];
GO
IF OBJECT_ID(N'[dbo].[FK_Event_TypeEvent]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Event] DROP CONSTRAINT [FK_Event_TypeEvent];
GO
IF OBJECT_ID(N'[dbo].[FK_Service_TypeService]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Service] DROP CONSTRAINT [FK_Service_TypeService];
GO
IF OBJECT_ID(N'[dbo].[FK_EventImage_Event]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[EventImage] DROP CONSTRAINT [FK_EventImage_Event];
GO
IF OBJECT_ID(N'[dbo].[FK_EventImage_Image]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[EventImage] DROP CONSTRAINT [FK_EventImage_Image];
GO
IF OBJECT_ID(N'[dbo].[FK_EventService_Event]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[EventService] DROP CONSTRAINT [FK_EventService_Event];
GO
IF OBJECT_ID(N'[dbo].[FK_EventService_Service]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[EventService] DROP CONSTRAINT [FK_EventService_Service];
GO
IF OBJECT_ID(N'[dbo].[FK_EventVideo_Event]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[EventVideo] DROP CONSTRAINT [FK_EventVideo_Event];
GO
IF OBJECT_ID(N'[dbo].[FK_EventVideo_Video]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[EventVideo] DROP CONSTRAINT [FK_EventVideo_Video];
GO
IF OBJECT_ID(N'[dbo].[FK_EventCoverVideo]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Event] DROP CONSTRAINT [FK_EventCoverVideo];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[Event]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Event];
GO
IF OBJECT_ID(N'[dbo].[Image]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Image];
GO
IF OBJECT_ID(N'[dbo].[Service]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Service];
GO
IF OBJECT_ID(N'[dbo].[TypeEvent]', 'U') IS NOT NULL
    DROP TABLE [dbo].[TypeEvent];
GO
IF OBJECT_ID(N'[dbo].[TypeService]', 'U') IS NOT NULL
    DROP TABLE [dbo].[TypeService];
GO
IF OBJECT_ID(N'[dbo].[Video]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Video];
GO
IF OBJECT_ID(N'[dbo].[EventImage]', 'U') IS NOT NULL
    DROP TABLE [dbo].[EventImage];
GO
IF OBJECT_ID(N'[dbo].[EventService]', 'U') IS NOT NULL
    DROP TABLE [dbo].[EventService];
GO
IF OBJECT_ID(N'[dbo].[EventVideo]', 'U') IS NOT NULL
    DROP TABLE [dbo].[EventVideo];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'Event'
CREATE TABLE [dbo].[Event] (
    [ID] int IDENTITY(1,1) NOT NULL,
    [Title] varchar(50)  NOT NULL,
    [TypeEventID] int  NOT NULL,
    [Date] datetime  NOT NULL,
    [Text] varchar(2048)  NULL,
    [CoverImageID] int  NOT NULL,
    [Controller] varchar(50)  NOT NULL,
    [Action] varchar(50)  NOT NULL,
    [CoverVideoID] nvarchar(max)  NOT NULL,
    [CoverVideo_ID] int  NULL
);
GO

-- Creating table 'Image'
CREATE TABLE [dbo].[Image] (
    [ID] int IDENTITY(1,1) NOT NULL,
    [Title] varchar(50)  NULL,
    [ImagePath] varchar(255)  NOT NULL
);
GO

-- Creating table 'Service'
CREATE TABLE [dbo].[Service] (
    [ID] int IDENTITY(1,1) NOT NULL,
    [Name] varchar(50)  NOT NULL,
    [TypeService] int  NOT NULL,
    [ServiceLink] varchar(255)  NULL
);
GO

-- Creating table 'TypeEvent'
CREATE TABLE [dbo].[TypeEvent] (
    [ID] int IDENTITY(1,1) NOT NULL,
    [Name] varchar(50)  NOT NULL
);
GO

-- Creating table 'TypeService'
CREATE TABLE [dbo].[TypeService] (
    [ID] int IDENTITY(1,1) NOT NULL,
    [Name] varchar(50)  NOT NULL
);
GO

-- Creating table 'Video'
CREATE TABLE [dbo].[Video] (
    [ID] int IDENTITY(1,1) NOT NULL,
    [Title] nvarchar(max)  NULL,
    [VideoLink] nvarchar(512)  NOT NULL
);
GO

-- Creating table 'EventImage'
CREATE TABLE [dbo].[EventImage] (
    [Events_ID] int  NOT NULL,
    [Images_ID] int  NOT NULL
);
GO

-- Creating table 'EventService'
CREATE TABLE [dbo].[EventService] (
    [Event_ID] int  NOT NULL,
    [Service_ID] int  NOT NULL
);
GO

-- Creating table 'EventVideo'
CREATE TABLE [dbo].[EventVideo] (
    [Events_ID] int  NOT NULL,
    [Videos_ID] int  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [ID] in table 'Event'
ALTER TABLE [dbo].[Event]
ADD CONSTRAINT [PK_Event]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ID] in table 'Image'
ALTER TABLE [dbo].[Image]
ADD CONSTRAINT [PK_Image]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ID] in table 'Service'
ALTER TABLE [dbo].[Service]
ADD CONSTRAINT [PK_Service]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ID] in table 'TypeEvent'
ALTER TABLE [dbo].[TypeEvent]
ADD CONSTRAINT [PK_TypeEvent]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ID] in table 'TypeService'
ALTER TABLE [dbo].[TypeService]
ADD CONSTRAINT [PK_TypeService]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ID] in table 'Video'
ALTER TABLE [dbo].[Video]
ADD CONSTRAINT [PK_Video]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [Events_ID], [Images_ID] in table 'EventImage'
ALTER TABLE [dbo].[EventImage]
ADD CONSTRAINT [PK_EventImage]
    PRIMARY KEY CLUSTERED ([Events_ID], [Images_ID] ASC);
GO

-- Creating primary key on [Event_ID], [Service_ID] in table 'EventService'
ALTER TABLE [dbo].[EventService]
ADD CONSTRAINT [PK_EventService]
    PRIMARY KEY CLUSTERED ([Event_ID], [Service_ID] ASC);
GO

-- Creating primary key on [Events_ID], [Videos_ID] in table 'EventVideo'
ALTER TABLE [dbo].[EventVideo]
ADD CONSTRAINT [PK_EventVideo]
    PRIMARY KEY CLUSTERED ([Events_ID], [Videos_ID] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [CoverImageID] in table 'Event'
ALTER TABLE [dbo].[Event]
ADD CONSTRAINT [FK_Event_Image]
    FOREIGN KEY ([CoverImageID])
    REFERENCES [dbo].[Image]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Event_Image'
CREATE INDEX [IX_FK_Event_Image]
ON [dbo].[Event]
    ([CoverImageID]);
GO

-- Creating foreign key on [TypeEventID] in table 'Event'
ALTER TABLE [dbo].[Event]
ADD CONSTRAINT [FK_Event_TypeEvent]
    FOREIGN KEY ([TypeEventID])
    REFERENCES [dbo].[TypeEvent]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Event_TypeEvent'
CREATE INDEX [IX_FK_Event_TypeEvent]
ON [dbo].[Event]
    ([TypeEventID]);
GO

-- Creating foreign key on [TypeService] in table 'Service'
ALTER TABLE [dbo].[Service]
ADD CONSTRAINT [FK_Service_TypeService]
    FOREIGN KEY ([TypeService])
    REFERENCES [dbo].[TypeService]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Service_TypeService'
CREATE INDEX [IX_FK_Service_TypeService]
ON [dbo].[Service]
    ([TypeService]);
GO

-- Creating foreign key on [Events_ID] in table 'EventImage'
ALTER TABLE [dbo].[EventImage]
ADD CONSTRAINT [FK_EventImage_Event]
    FOREIGN KEY ([Events_ID])
    REFERENCES [dbo].[Event]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating foreign key on [Images_ID] in table 'EventImage'
ALTER TABLE [dbo].[EventImage]
ADD CONSTRAINT [FK_EventImage_Image]
    FOREIGN KEY ([Images_ID])
    REFERENCES [dbo].[Image]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_EventImage_Image'
CREATE INDEX [IX_FK_EventImage_Image]
ON [dbo].[EventImage]
    ([Images_ID]);
GO

-- Creating foreign key on [Event_ID] in table 'EventService'
ALTER TABLE [dbo].[EventService]
ADD CONSTRAINT [FK_EventService_Event]
    FOREIGN KEY ([Event_ID])
    REFERENCES [dbo].[Event]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating foreign key on [Service_ID] in table 'EventService'
ALTER TABLE [dbo].[EventService]
ADD CONSTRAINT [FK_EventService_Service]
    FOREIGN KEY ([Service_ID])
    REFERENCES [dbo].[Service]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_EventService_Service'
CREATE INDEX [IX_FK_EventService_Service]
ON [dbo].[EventService]
    ([Service_ID]);
GO

-- Creating foreign key on [Events_ID] in table 'EventVideo'
ALTER TABLE [dbo].[EventVideo]
ADD CONSTRAINT [FK_EventVideo_Event]
    FOREIGN KEY ([Events_ID])
    REFERENCES [dbo].[Event]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating foreign key on [Videos_ID] in table 'EventVideo'
ALTER TABLE [dbo].[EventVideo]
ADD CONSTRAINT [FK_EventVideo_Video]
    FOREIGN KEY ([Videos_ID])
    REFERENCES [dbo].[Video]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_EventVideo_Video'
CREATE INDEX [IX_FK_EventVideo_Video]
ON [dbo].[EventVideo]
    ([Videos_ID]);
GO

-- Creating foreign key on [CoverVideo_ID] in table 'Event'
ALTER TABLE [dbo].[Event]
ADD CONSTRAINT [FK_EventCoverVideo]
    FOREIGN KEY ([CoverVideo_ID])
    REFERENCES [dbo].[Video]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_EventCoverVideo'
CREATE INDEX [IX_FK_EventCoverVideo]
ON [dbo].[Event]
    ([CoverVideo_ID]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------
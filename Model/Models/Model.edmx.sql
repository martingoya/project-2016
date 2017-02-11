
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 02/11/2017 18:16:52
-- Generated from EDMX file: C:\Users\Notebook\Source\Repos\project-2016\Model\Models\Model.edmx
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
IF OBJECT_ID(N'[dbo].[FK_EventImage_Event]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[EventImage] DROP CONSTRAINT [FK_EventImage_Event];
GO
IF OBJECT_ID(N'[dbo].[FK_EventImage_Image]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[EventImage] DROP CONSTRAINT [FK_EventImage_Image];
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
IF OBJECT_ID(N'[dbo].[TypeEvent]', 'U') IS NOT NULL
    DROP TABLE [dbo].[TypeEvent];
GO
IF OBJECT_ID(N'[dbo].[EventImage]', 'U') IS NOT NULL
    DROP TABLE [dbo].[EventImage];
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
    [Introduction] varchar(512)  NULL,
    [Path] varchar(128)  NOT NULL
);
GO

-- Creating table 'Image'
CREATE TABLE [dbo].[Image] (
    [ID] int IDENTITY(1,1) NOT NULL,
    [Title] varchar(50)  NULL,
    [ImagePath] varchar(255)  NOT NULL
);
GO

-- Creating table 'TypeEvent'
CREATE TABLE [dbo].[TypeEvent] (
    [ID] int IDENTITY(1,1) NOT NULL,
    [Name] varchar(50)  NOT NULL
);
GO

-- Creating table 'EventImage'
CREATE TABLE [dbo].[EventImage] (
    [Events_ID] int  NOT NULL,
    [Images_ID] int  NOT NULL
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

-- Creating primary key on [ID] in table 'TypeEvent'
ALTER TABLE [dbo].[TypeEvent]
ADD CONSTRAINT [PK_TypeEvent]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [Events_ID], [Images_ID] in table 'EventImage'
ALTER TABLE [dbo].[EventImage]
ADD CONSTRAINT [PK_EventImage]
    PRIMARY KEY CLUSTERED ([Events_ID], [Images_ID] ASC);
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

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------
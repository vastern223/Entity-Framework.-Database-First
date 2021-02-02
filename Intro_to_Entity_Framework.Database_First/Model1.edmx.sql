
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 02/02/2021 19:27:15
-- Generated from EDMX file: C:\Users\Kolotyuk\source\repos\Intro_to_Entity_Framework.Database_First\Intro_to_Entity_Framework.Database_First\Model1.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [Rent_Sql];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------


-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------


-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'RoomsInfo'
CREATE TABLE [dbo].[RoomsInfo] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Area] int  NOT NULL,
    [AccommodationAddress] nvarchar(50)  NOT NULL,
    [City] nvarchar(50)  NOT NULL,
    [StartRent] datetime  NULL,
    [EndRent] datetime  NULL,
    [UsersInfoId] int  NULL
);
GO

-- Creating table 'UsersInfo'
CREATE TABLE [dbo].[UsersInfo] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(50)  NOT NULL,
    [Password] nvarchar(max)  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [Id] in table 'RoomsInfo'
ALTER TABLE [dbo].[RoomsInfo]
ADD CONSTRAINT [PK_RoomsInfo]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'UsersInfo'
ALTER TABLE [dbo].[UsersInfo]
ADD CONSTRAINT [PK_UsersInfo]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [UsersInfoId] in table 'RoomsInfo'
ALTER TABLE [dbo].[RoomsInfo]
ADD CONSTRAINT [FK_RoomsInfoUsersInfo]
    FOREIGN KEY ([UsersInfoId])
    REFERENCES [dbo].[UsersInfo]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_RoomsInfoUsersInfo'
CREATE INDEX [IX_FK_RoomsInfoUsersInfo]
ON [dbo].[RoomsInfo]
    ([UsersInfoId]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------
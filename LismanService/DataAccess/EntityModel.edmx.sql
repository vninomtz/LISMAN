
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 11/10/2019 20:49:41
-- Generated from EDMX file: C:\Users\Vik-t\Documents\Software Engineering\5to Semestre\Tecnologías para la Construcción\Proyecto\LISMAN\LismanService\DataAccess\EntityModel.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [Lisman];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[FK_AccountRecord]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[RecordSet] DROP CONSTRAINT [FK_AccountRecord];
GO
IF OBJECT_ID(N'[dbo].[FK_AccountPlayer]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[PlayerSet] DROP CONSTRAINT [FK_AccountPlayer];
GO
IF OBJECT_ID(N'[dbo].[FK_AccountGameMembers]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[GameSet] DROP CONSTRAINT [FK_AccountGameMembers];
GO
IF OBJECT_ID(N'[dbo].[FK_GameAccount]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[AccountSet] DROP CONSTRAINT [FK_GameAccount];
GO
IF OBJECT_ID(N'[dbo].[FK_GameChat]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[GameSet] DROP CONSTRAINT [FK_GameChat];
GO
IF OBJECT_ID(N'[dbo].[FK_ChatMessage]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[MessageSet] DROP CONSTRAINT [FK_ChatMessage];
GO
IF OBJECT_ID(N'[dbo].[FK_AccountMessage]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[MessageSet] DROP CONSTRAINT [FK_AccountMessage];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[PlayerSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[PlayerSet];
GO
IF OBJECT_ID(N'[dbo].[AccountSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[AccountSet];
GO
IF OBJECT_ID(N'[dbo].[RecordSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[RecordSet];
GO
IF OBJECT_ID(N'[dbo].[GameSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[GameSet];
GO
IF OBJECT_ID(N'[dbo].[ChatSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[ChatSet];
GO
IF OBJECT_ID(N'[dbo].[MessageSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[MessageSet];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'PlayerSet'
CREATE TABLE [dbo].[PlayerSet] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [First_name] nvarchar(max)  NOT NULL,
    [Last_name] nvarchar(max)  NOT NULL,
    [Email] nvarchar(max)  NOT NULL,
    [Account_Id] int  NOT NULL
);
GO

-- Creating table 'AccountSet'
CREATE TABLE [dbo].[AccountSet] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [User] nvarchar(max)  NOT NULL,
    [Password] nvarchar(max)  NOT NULL,
    [Key_confirmation] nvarchar(max)  NULL,
    [Registration_date] nvarchar(max)  NOT NULL,
    [GameAccount_Account_Id] int  NULL
);
GO

-- Creating table 'RecordSet'
CREATE TABLE [dbo].[RecordSet] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Mult_best_score] int  NULL,
    [Story_best_score] int  NULL,
    [Mult_games_played] int  NULL,
    [Mult_games_won] int  NULL,
    [Account_Id] int  NOT NULL
);
GO

-- Creating table 'GameSet'
CREATE TABLE [dbo].[GameSet] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Creation_date] datetime  NOT NULL,
    [Last_update] datetime  NULL,
    [Status] bit  NOT NULL,
    [Game_over] datetime  NULL,
    [GameCreator_Id] int  NOT NULL,
    [Chat_Id] int  NOT NULL
);
GO

-- Creating table 'ChatSet'
CREATE TABLE [dbo].[ChatSet] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Creation_date] datetime  NOT NULL
);
GO

-- Creating table 'MessageSet'
CREATE TABLE [dbo].[MessageSet] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Text] nvarchar(max)  NOT NULL,
    [Creation_date] datetime  NOT NULL,
    [Chat_Id] int  NOT NULL,
    [Account_Id] int  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [Id] in table 'PlayerSet'
ALTER TABLE [dbo].[PlayerSet]
ADD CONSTRAINT [PK_PlayerSet]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'AccountSet'
ALTER TABLE [dbo].[AccountSet]
ADD CONSTRAINT [PK_AccountSet]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'RecordSet'
ALTER TABLE [dbo].[RecordSet]
ADD CONSTRAINT [PK_RecordSet]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'GameSet'
ALTER TABLE [dbo].[GameSet]
ADD CONSTRAINT [PK_GameSet]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'ChatSet'
ALTER TABLE [dbo].[ChatSet]
ADD CONSTRAINT [PK_ChatSet]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'MessageSet'
ALTER TABLE [dbo].[MessageSet]
ADD CONSTRAINT [PK_MessageSet]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [Account_Id] in table 'RecordSet'
ALTER TABLE [dbo].[RecordSet]
ADD CONSTRAINT [FK_AccountRecord]
    FOREIGN KEY ([Account_Id])
    REFERENCES [dbo].[AccountSet]
        ([Id])
    ON DELETE CASCADE ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_AccountRecord'
CREATE INDEX [IX_FK_AccountRecord]
ON [dbo].[RecordSet]
    ([Account_Id]);
GO

-- Creating foreign key on [Account_Id] in table 'PlayerSet'
ALTER TABLE [dbo].[PlayerSet]
ADD CONSTRAINT [FK_AccountPlayer]
    FOREIGN KEY ([Account_Id])
    REFERENCES [dbo].[AccountSet]
        ([Id])
    ON DELETE CASCADE ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_AccountPlayer'
CREATE INDEX [IX_FK_AccountPlayer]
ON [dbo].[PlayerSet]
    ([Account_Id]);
GO

-- Creating foreign key on [GameCreator_Id] in table 'GameSet'
ALTER TABLE [dbo].[GameSet]
ADD CONSTRAINT [FK_AccountGameMembers]
    FOREIGN KEY ([GameCreator_Id])
    REFERENCES [dbo].[AccountSet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_AccountGameMembers'
CREATE INDEX [IX_FK_AccountGameMembers]
ON [dbo].[GameSet]
    ([GameCreator_Id]);
GO

-- Creating foreign key on [GameAccount_Account_Id] in table 'AccountSet'
ALTER TABLE [dbo].[AccountSet]
ADD CONSTRAINT [FK_GameAccount]
    FOREIGN KEY ([GameAccount_Account_Id])
    REFERENCES [dbo].[GameSet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_GameAccount'
CREATE INDEX [IX_FK_GameAccount]
ON [dbo].[AccountSet]
    ([GameAccount_Account_Id]);
GO

-- Creating foreign key on [Chat_Id] in table 'GameSet'
ALTER TABLE [dbo].[GameSet]
ADD CONSTRAINT [FK_GameChat]
    FOREIGN KEY ([Chat_Id])
    REFERENCES [dbo].[ChatSet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_GameChat'
CREATE INDEX [IX_FK_GameChat]
ON [dbo].[GameSet]
    ([Chat_Id]);
GO

-- Creating foreign key on [Chat_Id] in table 'MessageSet'
ALTER TABLE [dbo].[MessageSet]
ADD CONSTRAINT [FK_ChatMessage]
    FOREIGN KEY ([Chat_Id])
    REFERENCES [dbo].[ChatSet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_ChatMessage'
CREATE INDEX [IX_FK_ChatMessage]
ON [dbo].[MessageSet]
    ([Chat_Id]);
GO

-- Creating foreign key on [Account_Id] in table 'MessageSet'
ALTER TABLE [dbo].[MessageSet]
ADD CONSTRAINT [FK_AccountMessage]
    FOREIGN KEY ([Account_Id])
    REFERENCES [dbo].[AccountSet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_AccountMessage'
CREATE INDEX [IX_FK_AccountMessage]
ON [dbo].[MessageSet]
    ([Account_Id]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------
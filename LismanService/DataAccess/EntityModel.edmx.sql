
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 10/08/2019 15:42:43
-- Generated from EDMX file: C:\Users\Vik-t\Documents\CarreraUV\5to Semestre\Tecnologías para la Construcción\Proyecto\LISMAN\LismanService\DataAccess\EntityModel.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [LISMAN];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[FK_CuentaUsuario]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[CuentaSet] DROP CONSTRAINT [FK_CuentaUsuario];
GO
IF OBJECT_ID(N'[dbo].[FK_CuentaHistorial]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[CuentaSet] DROP CONSTRAINT [FK_CuentaHistorial];
GO
IF OBJECT_ID(N'[dbo].[FK_CuentaPartida_Cuenta]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[CuentaPartida] DROP CONSTRAINT [FK_CuentaPartida_Cuenta];
GO
IF OBJECT_ID(N'[dbo].[FK_CuentaPartida_Partida]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[CuentaPartida] DROP CONSTRAINT [FK_CuentaPartida_Partida];
GO
IF OBJECT_ID(N'[dbo].[FK_ChatPartida]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[ChatSet] DROP CONSTRAINT [FK_ChatPartida];
GO
IF OBJECT_ID(N'[dbo].[FK_ChatMensaje]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[MensajeSet] DROP CONSTRAINT [FK_ChatMensaje];
GO
IF OBJECT_ID(N'[dbo].[FK_CuentaMensaje]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[MensajeSet] DROP CONSTRAINT [FK_CuentaMensaje];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[JugadorSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[JugadorSet];
GO
IF OBJECT_ID(N'[dbo].[CuentaSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[CuentaSet];
GO
IF OBJECT_ID(N'[dbo].[HistorialSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[HistorialSet];
GO
IF OBJECT_ID(N'[dbo].[PartidaSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[PartidaSet];
GO
IF OBJECT_ID(N'[dbo].[ChatSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[ChatSet];
GO
IF OBJECT_ID(N'[dbo].[MensajeSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[MensajeSet];
GO
IF OBJECT_ID(N'[dbo].[CuentaPartida]', 'U') IS NOT NULL
    DROP TABLE [dbo].[CuentaPartida];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'PlayerSet'
CREATE TABLE [dbo].[PlayerSet] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [First_name] nvarchar(max)  NOT NULL,
    [Last_name] nvarchar(max)  NOT NULL,
    [Email] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'AccountSet'
CREATE TABLE [dbo].[AccountSet] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [User] nvarchar(max)  NOT NULL,
    [Password] nvarchar(max)  NOT NULL,
    [Key_confirmation] nvarchar(max)  NULL,
    [Registration_date] nvarchar(max)  NOT NULL,
    [Player_Id] int  NOT NULL,
    [Record_Id] int  NOT NULL
);
GO

-- Creating table 'RecordSet'
CREATE TABLE [dbo].[RecordSet] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Mult_best_score] int  NULL,
    [Story_best_score] int  NULL,
    [Mult_games_played] int  NULL,
    [Mult_games_won] int  NULL
);
GO

-- Creating table 'GameSet'
CREATE TABLE [dbo].[GameSet] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Creation_date] datetime  NOT NULL
);
GO

-- Creating table 'ChatSet'
CREATE TABLE [dbo].[ChatSet] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Creation_date] datetime  NOT NULL,
    [Game_Id] int  NOT NULL
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

-- Creating table 'CuentaPartida'
CREATE TABLE [dbo].[CuentaPartida] (
    [Account_Id] int  NOT NULL,
    [CuentaPartida_Cuenta_Id] int  NOT NULL
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

-- Creating primary key on [Account_Id], [CuentaPartida_Cuenta_Id] in table 'CuentaPartida'
ALTER TABLE [dbo].[CuentaPartida]
ADD CONSTRAINT [PK_CuentaPartida]
    PRIMARY KEY CLUSTERED ([Account_Id], [CuentaPartida_Cuenta_Id] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [Player_Id] in table 'AccountSet'
ALTER TABLE [dbo].[AccountSet]
ADD CONSTRAINT [FK_CuentaUsuario]
    FOREIGN KEY ([Player_Id])
    REFERENCES [dbo].[PlayerSet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_CuentaUsuario'
CREATE INDEX [IX_FK_CuentaUsuario]
ON [dbo].[AccountSet]
    ([Player_Id]);
GO

-- Creating foreign key on [Record_Id] in table 'AccountSet'
ALTER TABLE [dbo].[AccountSet]
ADD CONSTRAINT [FK_CuentaHistorial]
    FOREIGN KEY ([Record_Id])
    REFERENCES [dbo].[RecordSet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_CuentaHistorial'
CREATE INDEX [IX_FK_CuentaHistorial]
ON [dbo].[AccountSet]
    ([Record_Id]);
GO

-- Creating foreign key on [Account_Id] in table 'CuentaPartida'
ALTER TABLE [dbo].[CuentaPartida]
ADD CONSTRAINT [FK_CuentaPartida_Cuenta]
    FOREIGN KEY ([Account_Id])
    REFERENCES [dbo].[AccountSet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating foreign key on [CuentaPartida_Cuenta_Id] in table 'CuentaPartida'
ALTER TABLE [dbo].[CuentaPartida]
ADD CONSTRAINT [FK_CuentaPartida_Partida]
    FOREIGN KEY ([CuentaPartida_Cuenta_Id])
    REFERENCES [dbo].[GameSet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_CuentaPartida_Partida'
CREATE INDEX [IX_FK_CuentaPartida_Partida]
ON [dbo].[CuentaPartida]
    ([CuentaPartida_Cuenta_Id]);
GO

-- Creating foreign key on [Game_Id] in table 'ChatSet'
ALTER TABLE [dbo].[ChatSet]
ADD CONSTRAINT [FK_ChatPartida]
    FOREIGN KEY ([Game_Id])
    REFERENCES [dbo].[GameSet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_ChatPartida'
CREATE INDEX [IX_FK_ChatPartida]
ON [dbo].[ChatSet]
    ([Game_Id]);
GO

-- Creating foreign key on [Chat_Id] in table 'MessageSet'
ALTER TABLE [dbo].[MessageSet]
ADD CONSTRAINT [FK_ChatMensaje]
    FOREIGN KEY ([Chat_Id])
    REFERENCES [dbo].[ChatSet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_ChatMensaje'
CREATE INDEX [IX_FK_ChatMensaje]
ON [dbo].[MessageSet]
    ([Chat_Id]);
GO

-- Creating foreign key on [Account_Id] in table 'MessageSet'
ALTER TABLE [dbo].[MessageSet]
ADD CONSTRAINT [FK_CuentaMensaje]
    FOREIGN KEY ([Account_Id])
    REFERENCES [dbo].[AccountSet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_CuentaMensaje'
CREATE INDEX [IX_FK_CuentaMensaje]
ON [dbo].[MessageSet]
    ([Account_Id]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------
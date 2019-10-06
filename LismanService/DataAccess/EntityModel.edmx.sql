
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 10/06/2019 13:04:02
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

-- Creating table 'JugadorSet'
CREATE TABLE [dbo].[JugadorSet] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Nombre] nvarchar(max)  NOT NULL,
    [Apellido] nvarchar(max)  NOT NULL,
    [Email] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'CuentaSet'
CREATE TABLE [dbo].[CuentaSet] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Usuario] nvarchar(max)  NOT NULL,
    [Contrasenia] nvarchar(max)  NOT NULL,
    [key_confirmation] nvarchar(max)  NULL,
    [fecha_registro] nvarchar(max)  NOT NULL,
    [Jugador_Id] int  NOT NULL,
    [Historial_Id] int  NOT NULL
);
GO

-- Creating table 'HistorialSet'
CREATE TABLE [dbo].[HistorialSet] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Multijugador_PuntajeMaximo] int  NULL,
    [Historia_PuntajeMaximo] int  NULL,
    [Mult_PartidasJugadas] int  NULL,
    [Mult_PartidasGanadas] int  NULL
);
GO

-- Creating table 'PartidaSet'
CREATE TABLE [dbo].[PartidaSet] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Fecha_creacion] datetime  NOT NULL
);
GO

-- Creating table 'ChatSet'
CREATE TABLE [dbo].[ChatSet] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Fecha_Creacion] datetime  NOT NULL,
    [Partida_Id] int  NOT NULL
);
GO

-- Creating table 'MensajeSet'
CREATE TABLE [dbo].[MensajeSet] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Texto] nvarchar(max)  NOT NULL,
    [Fecha_creacion] datetime  NOT NULL,
    [MAC] nvarchar(max)  NULL,
    [Chat_Id] int  NOT NULL,
    [Cuenta_Id] int  NOT NULL
);
GO

-- Creating table 'CuentaPartida'
CREATE TABLE [dbo].[CuentaPartida] (
    [Cuenta_Id] int  NOT NULL,
    [Partida_Id] int  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [Id] in table 'JugadorSet'
ALTER TABLE [dbo].[JugadorSet]
ADD CONSTRAINT [PK_JugadorSet]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'CuentaSet'
ALTER TABLE [dbo].[CuentaSet]
ADD CONSTRAINT [PK_CuentaSet]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'HistorialSet'
ALTER TABLE [dbo].[HistorialSet]
ADD CONSTRAINT [PK_HistorialSet]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'PartidaSet'
ALTER TABLE [dbo].[PartidaSet]
ADD CONSTRAINT [PK_PartidaSet]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'ChatSet'
ALTER TABLE [dbo].[ChatSet]
ADD CONSTRAINT [PK_ChatSet]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'MensajeSet'
ALTER TABLE [dbo].[MensajeSet]
ADD CONSTRAINT [PK_MensajeSet]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Cuenta_Id], [Partida_Id] in table 'CuentaPartida'
ALTER TABLE [dbo].[CuentaPartida]
ADD CONSTRAINT [PK_CuentaPartida]
    PRIMARY KEY CLUSTERED ([Cuenta_Id], [Partida_Id] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [Jugador_Id] in table 'CuentaSet'
ALTER TABLE [dbo].[CuentaSet]
ADD CONSTRAINT [FK_CuentaUsuario]
    FOREIGN KEY ([Jugador_Id])
    REFERENCES [dbo].[JugadorSet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_CuentaUsuario'
CREATE INDEX [IX_FK_CuentaUsuario]
ON [dbo].[CuentaSet]
    ([Jugador_Id]);
GO

-- Creating foreign key on [Historial_Id] in table 'CuentaSet'
ALTER TABLE [dbo].[CuentaSet]
ADD CONSTRAINT [FK_CuentaHistorial]
    FOREIGN KEY ([Historial_Id])
    REFERENCES [dbo].[HistorialSet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_CuentaHistorial'
CREATE INDEX [IX_FK_CuentaHistorial]
ON [dbo].[CuentaSet]
    ([Historial_Id]);
GO

-- Creating foreign key on [Cuenta_Id] in table 'CuentaPartida'
ALTER TABLE [dbo].[CuentaPartida]
ADD CONSTRAINT [FK_CuentaPartida_Cuenta]
    FOREIGN KEY ([Cuenta_Id])
    REFERENCES [dbo].[CuentaSet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating foreign key on [Partida_Id] in table 'CuentaPartida'
ALTER TABLE [dbo].[CuentaPartida]
ADD CONSTRAINT [FK_CuentaPartida_Partida]
    FOREIGN KEY ([Partida_Id])
    REFERENCES [dbo].[PartidaSet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_CuentaPartida_Partida'
CREATE INDEX [IX_FK_CuentaPartida_Partida]
ON [dbo].[CuentaPartida]
    ([Partida_Id]);
GO

-- Creating foreign key on [Partida_Id] in table 'ChatSet'
ALTER TABLE [dbo].[ChatSet]
ADD CONSTRAINT [FK_ChatPartida]
    FOREIGN KEY ([Partida_Id])
    REFERENCES [dbo].[PartidaSet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_ChatPartida'
CREATE INDEX [IX_FK_ChatPartida]
ON [dbo].[ChatSet]
    ([Partida_Id]);
GO

-- Creating foreign key on [Chat_Id] in table 'MensajeSet'
ALTER TABLE [dbo].[MensajeSet]
ADD CONSTRAINT [FK_ChatMensaje]
    FOREIGN KEY ([Chat_Id])
    REFERENCES [dbo].[ChatSet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_ChatMensaje'
CREATE INDEX [IX_FK_ChatMensaje]
ON [dbo].[MensajeSet]
    ([Chat_Id]);
GO

-- Creating foreign key on [Cuenta_Id] in table 'MensajeSet'
ALTER TABLE [dbo].[MensajeSet]
ADD CONSTRAINT [FK_CuentaMensaje]
    FOREIGN KEY ([Cuenta_Id])
    REFERENCES [dbo].[CuentaSet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_CuentaMensaje'
CREATE INDEX [IX_FK_CuentaMensaje]
ON [dbo].[MensajeSet]
    ([Cuenta_Id]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------
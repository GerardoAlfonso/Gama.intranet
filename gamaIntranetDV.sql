CREATE DATABASE GamaIntranet
GO
USE GamaIntranet

CREATE TABLE Usuario(
    [Id][INT] IDENTITY (1,1) NOT NULL,
    [Name][VARCHAR](200) NULL,
    [Password][VARCHAR](MAX) NULL,
    [Status][INT] NULL,
    [LoginAttempts][INT] NULL,
    [Role][INT] NULL,
    [Token][VARCHAR](MAX) NULL,
    [ShouldChangePassword][BIT] NOT NULL,
    [LastAccess][DATETIME] NULL,
    [LastAttempDate][DATETIME] NULL,
    [UserCreation][INT] NULL,
    [CreatedAt][DATETIME] NULL,
    [UpdatedAt][DATETIME] NULL
)


CREATE TABLE Roles(
    [Id][INT] IDENTITY (1,1) NOT NULL,
    [Description][VARCHAR](255) NOT NULL,
    [CreatedAt][DATETIME] NULL,
    [UpdatedAt][DATETIME] NULL

)
CREATE TABLE Logs(
    [Id][INT] IDENTITY(1,1) NOT NULL,
    [Page][VARCHAR](255) NULL,
    [Description][VARCHAR](MAX) NULL,
    --[IP][VARCHAR](25) NULL,
    [UserCreation][INT] NULL,
    [CreatedAt][DATETIME] NULL,
    [UpdatedAt][DATETIME] NULL
)

CREATE TABLE Folders(
    [Id][INT] IDENTITY (1,1) NOT NULL,
    [Name][VARCHAR](255) NULL
)

CREATE TABLE UsuariosPermisosFolders(
    [Id][INT] IDENTITY (1,1) NOT NULL,
    [IdUsuario][INT] NOT NULL,
    [IdFolder][INT] NOT NULL,
    [Read][BIT] NULL,
    [Write][BIT] NULL
)

CREATE TABLE ParametrosGenerales(
    [Id][INT] IDENTITY (1,1) NOT NULL,
    [Name][VARCHAR](255) NULL,
    [Value][VARCHAR](MAX) NULL,
    [UserCreation][INT] NULL,
    [CreatedAt][DATETIME] NULL,
    [UpdatedAt][DATETIME] NULL
)

select * from usuario

insert into Usuario ([Name],[Password], [Status], [Role], [LoginAttempts], [ShouldChangePassword])
VALUES ('Gerardo', 'b221d9dbb083a7f33428d7c2a3c3198ae925614d70210e28716ccaa7cd4ddb79', 1, 1, 0, 0)
insert into Usuario ([Name],[Password], [Status], [Role], [LoginAttempts], [ShouldChangePassword])
VALUES ('Alfonso', 'ASDF', 1, 2, 0, 0)

-- truncate table usuario

update usuario 
set [Status] = 1
where id = 2



--[Permissions]
use master
go
drop database GamaIntranet
go



USE MASTER
GO

IF NOT EXISTS (
    SELECT [name]
    FROM sys.databases
    WHERE [name] = N'BingeBuddy'
)

CREATE DATABASE BingeBuddy
GO

USE BingeBuddy
GO


DROP TABLE IF EXISTS UserShow;
DROP TABLE IF EXISTS UserProfile;
DROP TABLE IF EXISTS Show;
DROP TABLE IF EXISTS Platform;
DROP TABLE IF EXISTS Category;

CREATE TABLE [Show] (
  [Id] int PRIMARY KEY IDENTITY(1, 1),
  [Title] nvarchar(255) NOT NULL,
  [CoverImage] nvarchar(1000),
  [Cancelled] bit,
  [Approved] bit
)
GO

CREATE TABLE [UserProfile] (
  [Id] int PRIMARY KEY IDENTITY(1, 1),
  [FirebaseUserId] nvarchar(255) NOT NULL,
  [UserName] nvarchar(255) NOT NULL,
  [Name] nvarchar(255) NOT NULL,
  [Email] nvarchar(255) NOT NULL,
  [Admin] bit
)
GO

CREATE TABLE [Category] (
  [Id] int PRIMARY KEY IDENTITY(1, 1),
  [Name] nvarchar(255) NOT NULL
)
GO

CREATE TABLE [Platform] (
  [Id] int PRIMARY KEY IDENTITY(1, 1),
  [Name] nvarchar(255) NOT NULL
)
GO

CREATE TABLE [UserShow] (
  [Id] int PRIMARY KEY IDENTITY(1, 1),
  [ShowId] int NOT NULL,
  [UserId] int NOT NULL,
  [LastWatchedSeason] int,
  [LastWatchedEpisode] int,
  [LastReleasedSeason] int,
  [LastReleasedEpisode] int,
  [DateUpdated] datetime,
  [Note] nvarchar(1000),
  [PlatformId] int,
  [CategoryId] int
)
GO

ALTER TABLE [UserShow] ADD FOREIGN KEY ([ShowId]) REFERENCES [Show] ([Id]) ON DELETE CASCADE
GO

ALTER TABLE [UserShow] ADD FOREIGN KEY ([UserId]) REFERENCES [UserProfile] ([Id]) ON DELETE CASCADE
GO

ALTER TABLE [UserShow] ADD FOREIGN KEY ([PlatformId]) REFERENCES [Platform] ([Id])
GO

ALTER TABLE [UserShow] ADD FOREIGN KEY ([CategoryId]) REFERENCES [Category] ([Id])
GO

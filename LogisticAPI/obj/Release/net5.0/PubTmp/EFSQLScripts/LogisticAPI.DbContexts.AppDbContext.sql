IF OBJECT_ID(N'[__EFMigrationsHistory]') IS NULL
BEGIN
    CREATE TABLE [__EFMigrationsHistory] (
        [MigrationId] nvarchar(150) NOT NULL,
        [ProductVersion] nvarchar(32) NOT NULL,
        CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY ([MigrationId])
    );
END;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20211111145255_InitialCreate')
BEGIN
    CREATE TABLE [Countries] (
        [Id] int NOT NULL IDENTITY,
        [CountryCode] nvarchar(max) NULL,
        [CountryName] nvarchar(max) NULL,
        CONSTRAINT [PK_Countries] PRIMARY KEY ([Id])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20211111145255_InitialCreate')
BEGIN
    CREATE TABLE [CountryConnections] (
        [Id] int NOT NULL IDENTITY,
        [CountryAId] int NOT NULL,
        [CountryBId] int NOT NULL,
        [CostOfRoad] int NOT NULL,
        CONSTRAINT [PK_CountryConnections] PRIMARY KEY ([Id])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20211111145255_InitialCreate')
BEGIN
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'CountryCode', N'CountryName') AND [object_id] = OBJECT_ID(N'[Countries]'))
        SET IDENTITY_INSERT [Countries] ON;
    EXEC(N'INSERT INTO [Countries] ([Id], [CountryCode], [CountryName])
    VALUES (1, N''CAN'', N''Canada''),
    (2, N''USA'', N''United States of America''),
    (3, N''MEX'', N''Mexico''),
    (4, N''BLZ'', N''Belize''),
    (5, N''GTM'', N''Guatemala''),
    (6, N''SLV'', N''Salvador''),
    (7, N''HND'', N''Honduras''),
    (8, N''NIC'', N''Nicaragua''),
    (9, N''CRI'', N''Costa Rica''),
    (10, N''PAN'', N''Panama'')');
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'CountryCode', N'CountryName') AND [object_id] = OBJECT_ID(N'[Countries]'))
        SET IDENTITY_INSERT [Countries] OFF;
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20211111145255_InitialCreate')
BEGIN
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'CostOfRoad', N'CountryAId', N'CountryBId') AND [object_id] = OBJECT_ID(N'[CountryConnections]'))
        SET IDENTITY_INSERT [CountryConnections] ON;
    EXEC(N'INSERT INTO [CountryConnections] ([Id], [CostOfRoad], [CountryAId], [CountryBId])
    VALUES (10, 1, 7, 8),
    (9, 1, 7, 8),
    (8, 1, 6, 7),
    (7, 1, 5, 7),
    (6, 1, 5, 6),
    (1, 1, 1, 2),
    (4, 1, 3, 5),
    (3, 1, 3, 4),
    (2, 1, 2, 3),
    (11, 1, 8, 9),
    (5, 1, 4, 5),
    (12, 1, 9, 10)');
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'CostOfRoad', N'CountryAId', N'CountryBId') AND [object_id] = OBJECT_ID(N'[CountryConnections]'))
        SET IDENTITY_INSERT [CountryConnections] OFF;
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20211111145255_InitialCreate')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20211111145255_InitialCreate', N'5.0.12');
END;
GO

COMMIT;
GO


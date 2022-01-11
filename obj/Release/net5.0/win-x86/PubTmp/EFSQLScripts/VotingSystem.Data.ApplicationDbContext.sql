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

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220110140110_AddUser')
BEGIN
    CREATE TABLE [Users] (
        [Id] int NOT NULL IDENTITY,
        [Username] nvarchar(max) NULL,
        [PasswordSalt] varbinary(max) NULL,
        [PasswordHash] varbinary(max) NULL,
        [Role] nvarchar(max) NOT NULL DEFAULT N'User',
        CONSTRAINT [PK_Users] PRIMARY KEY ([Id])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220110140110_AddUser')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20220110140110_AddUser', N'5.0.0');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220111101254_Article')
BEGIN
    CREATE TABLE [Article] (
        [Id] int NOT NULL IDENTITY,
        [Header] nvarchar(max) NULL,
        [Body] nvarchar(max) NULL,
        [UserId] int NULL,
        CONSTRAINT [PK_Article] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_Article_Users_UserId] FOREIGN KEY ([UserId]) REFERENCES [Users] ([Id]) ON DELETE NO ACTION
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220111101254_Article')
BEGIN
    CREATE INDEX [IX_Article_UserId] ON [Article] ([UserId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220111101254_Article')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20220111101254_Article', N'5.0.0');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220111110533_ArticleToDb')
BEGIN
    ALTER TABLE [Article] DROP CONSTRAINT [FK_Article_Users_UserId];
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220111110533_ArticleToDb')
BEGIN
    ALTER TABLE [Article] DROP CONSTRAINT [PK_Article];
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220111110533_ArticleToDb')
BEGIN
    EXEC sp_rename N'[Article]', N'Articles';
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220111110533_ArticleToDb')
BEGIN
    EXEC sp_rename N'[Articles].[IX_Article_UserId]', N'IX_Articles_UserId', N'INDEX';
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220111110533_ArticleToDb')
BEGIN
    DECLARE @var0 sysname;
    SELECT @var0 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Users]') AND [c].[name] = N'Role');
    IF @var0 IS NOT NULL EXEC(N'ALTER TABLE [Users] DROP CONSTRAINT [' + @var0 + '];');
    ALTER TABLE [Users] ADD DEFAULT N'Member' FOR [Role];
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220111110533_ArticleToDb')
BEGIN
    DROP INDEX [IX_Articles_UserId] ON [Articles];
    DECLARE @var1 sysname;
    SELECT @var1 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Articles]') AND [c].[name] = N'UserId');
    IF @var1 IS NOT NULL EXEC(N'ALTER TABLE [Articles] DROP CONSTRAINT [' + @var1 + '];');
    ALTER TABLE [Articles] ALTER COLUMN [UserId] int NOT NULL;
    ALTER TABLE [Articles] ADD DEFAULT 0 FOR [UserId];
    CREATE INDEX [IX_Articles_UserId] ON [Articles] ([UserId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220111110533_ArticleToDb')
BEGIN
    ALTER TABLE [Articles] ADD [Dislikes] int NOT NULL DEFAULT 0;
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220111110533_ArticleToDb')
BEGIN
    ALTER TABLE [Articles] ADD [Likes] int NOT NULL DEFAULT 0;
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220111110533_ArticleToDb')
BEGIN
    ALTER TABLE [Articles] ADD CONSTRAINT [PK_Articles] PRIMARY KEY ([Id]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220111110533_ArticleToDb')
BEGIN
    ALTER TABLE [Articles] ADD CONSTRAINT [FK_Articles_Users_UserId] FOREIGN KEY ([UserId]) REFERENCES [Users] ([Id]) ON DELETE CASCADE;
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220111110533_ArticleToDb')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20220111110533_ArticleToDb', N'5.0.0');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220111152221_UpdateDb')
BEGIN
    DECLARE @var2 sysname;
    SELECT @var2 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Articles]') AND [c].[name] = N'Dislikes');
    IF @var2 IS NOT NULL EXEC(N'ALTER TABLE [Articles] DROP CONSTRAINT [' + @var2 + '];');
    ALTER TABLE [Articles] DROP COLUMN [Dislikes];
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220111152221_UpdateDb')
BEGIN
    DECLARE @var3 sysname;
    SELECT @var3 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Articles]') AND [c].[name] = N'Likes');
    IF @var3 IS NOT NULL EXEC(N'ALTER TABLE [Articles] DROP CONSTRAINT [' + @var3 + '];');
    ALTER TABLE [Articles] DROP COLUMN [Likes];
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220111152221_UpdateDb')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20220111152221_UpdateDb', N'5.0.0');
END;
GO

COMMIT;
GO


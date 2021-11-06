CREATE TABLE [dbo].[command] (
    [id]      INT           IDENTITY (50000001, 1) NOT NULL,
    [code]    CHAR (4)      NOT NULL,
    [command] VARCHAR (MAX) NOT NULL,
    [key]     VARCHAR (MAX) NOT NULL,
    CONSTRAINT [PK_command_1] PRIMARY KEY CLUSTERED ([id] ASC),
    CONSTRAINT [IX_command] UNIQUE NONCLUSTERED ([code] ASC)
);


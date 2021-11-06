CREATE TABLE [dbo].[command_name] (
    [id]            INT           IDENTITY (50100001, 1) NOT NULL,
    [command_code]  CHAR (3)      NOT NULL,
    [language_code] CHAR (2)      NOT NULL,
    [name]          VARCHAR (MAX) NOT NULL,
    CONSTRAINT [PK_command_name] PRIMARY KEY CLUSTERED ([id] ASC),
    CONSTRAINT [FK_command_name_language] FOREIGN KEY ([language_code]) REFERENCES [dbo].[language] ([code]),
    CONSTRAINT [IX_command_name] UNIQUE NONCLUSTERED ([command_code] ASC, [language_code] ASC)
);


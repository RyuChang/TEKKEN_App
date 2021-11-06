CREATE TABLE [dbo].[character_name] (
    [id]             INT            IDENTITY (10100001, 1) NOT NULL,
    [character_code] INT            NOT NULL,
    [language_code]  CHAR (2)       NOT NULL,
    [name]           NVARCHAR (MAX) NOT NULL,
    [fullName]       VARCHAR (MAX)  NOT NULL,
    CONSTRAINT [PK_character_name] PRIMARY KEY CLUSTERED ([id] ASC),
    CONSTRAINT [FK_character_name_language] FOREIGN KEY ([language_code]) REFERENCES [dbo].[language] ([code]),
    CONSTRAINT [IX_character_name] UNIQUE NONCLUSTERED ([character_code] ASC, [language_code] ASC)
);


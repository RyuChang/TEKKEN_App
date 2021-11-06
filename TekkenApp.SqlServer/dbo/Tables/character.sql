CREATE TABLE [dbo].[character] (
    [id]          INT           IDENTITY (10000001, 1) NOT NULL,
    [code]        TINYINT       NOT NULL,
    [code_name]   CHAR (3)      NOT NULL,
    [season]      TINYINT       NOT NULL,
    [description] VARCHAR (MAX) NOT NULL,
    CONSTRAINT [PK_character_1] PRIMARY KEY CLUSTERED ([id] ASC),
    CONSTRAINT [FK_character_character] FOREIGN KEY ([id]) REFERENCES [dbo].[character] ([id]),
    CONSTRAINT [IX_character] UNIQUE NONCLUSTERED ([code] ASC),
    CONSTRAINT [IX_character_code_Unique] UNIQUE NONCLUSTERED ([code_name] ASC)
);


CREATE TABLE [dbo].[moveSubType] (
    [id]             INT           IDENTITY (70000001, 1) NOT NULL,
    [code]           INT           NOT NULL,
    [character_code] TINYINT       NOT NULL,
    [number]         TINYINT       NOT NULL,
    [description]    VARCHAR (MAX) NOT NULL,
    CONSTRAINT [PK_moveSubType_id] PRIMARY KEY NONCLUSTERED ([id] ASC),
    CONSTRAINT [FK_moveSubType_moveSubType] FOREIGN KEY ([character_code]) REFERENCES [dbo].[character] ([code]),
    CONSTRAINT [FK_moveSubType_moveSubType1] FOREIGN KEY ([id]) REFERENCES [dbo].[moveSubType] ([id]),
    CONSTRAINT [IX_moveSubType_character_code_number] UNIQUE NONCLUSTERED ([character_code] ASC, [number] ASC),
    CONSTRAINT [IX_moveSubType_code] UNIQUE NONCLUSTERED ([code] ASC)
);


GO
CREATE UNIQUE CLUSTERED INDEX [IX_moveSubType]
    ON [dbo].[moveSubType]([character_code] ASC, [code] ASC);


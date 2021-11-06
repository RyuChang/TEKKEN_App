CREATE TABLE [dbo].[moveText] (
    [id]             INT           IDENTITY (100000001, 1) NOT NULL,
    [code]           INT           NOT NULL,
    [character_code] TINYINT       NOT NULL,
    [number]         TINYINT       NOT NULL,
    [description]    VARCHAR (MAX) NOT NULL,
    CONSTRAINT [PK_moveText_id] PRIMARY KEY CLUSTERED ([id] ASC),
    CONSTRAINT [FK_moveText_character] FOREIGN KEY ([character_code]) REFERENCES [dbo].[character] ([code]) ON UPDATE CASCADE,
    CONSTRAINT [IX_moveText_character_code_number] UNIQUE NONCLUSTERED ([character_code] ASC, [number] ASC),
    CONSTRAINT [IX_moveText_code] UNIQUE NONCLUSTERED ([code] ASC)
);


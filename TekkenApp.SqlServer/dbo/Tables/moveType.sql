CREATE TABLE [dbo].[moveType] (
    [id]          INT           IDENTITY (60000001, 1) NOT NULL,
    [code]        INT           NOT NULL,
    [number]      TINYINT       NOT NULL,
    [description] VARCHAR (MAX) NOT NULL,
    CONSTRAINT [PK_moveType_id] PRIMARY KEY CLUSTERED ([id] ASC),
    CONSTRAINT [FK_moveType_moveType] FOREIGN KEY ([id]) REFERENCES [dbo].[moveType] ([id]),
    CONSTRAINT [IX_moveType_1] UNIQUE NONCLUSTERED ([code] ASC, [number] ASC),
    CONSTRAINT [IX_moveType_code_unique] UNIQUE NONCLUSTERED ([code] ASC)
);


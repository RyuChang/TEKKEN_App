CREATE TABLE [dbo].[StateGroup] (
    [id]          INT           IDENTITY (80000001, 1) NOT NULL,
    [code]        INT           NOT NULL,
    [description] VARCHAR (MAX) NOT NULL,
    [number]      TINYINT       NOT NULL,
    CONSTRAINT [PK_StateGroup] PRIMARY KEY CLUSTERED ([id] ASC),
    CONSTRAINT [IX_StateGroup] UNIQUE NONCLUSTERED ([code] ASC),
    CONSTRAINT [IX_StateGroup_1] UNIQUE NONCLUSTERED ([number] ASC)
);


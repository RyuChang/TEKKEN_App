CREATE TABLE [dbo].[hitType] (
    [id]          INT           IDENTITY (110000001, 1) NOT NULL,
    [code]        INT           NOT NULL,
    [description] VARCHAR (MAX) NOT NULL,
    [number]      TINYINT       NOT NULL,
    CONSTRAINT [PK_HitType] PRIMARY KEY CLUSTERED ([id] ASC),
    CONSTRAINT [IX_hitType] UNIQUE NONCLUSTERED ([code] ASC),
    CONSTRAINT [IX_hitType_1] UNIQUE NONCLUSTERED ([number] ASC)
);


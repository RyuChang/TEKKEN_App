CREATE TABLE [dbo].[language] (
    [id]     INT           IDENTITY (20000001, 1) NOT NULL,
    [code]   CHAR (2)      NOT NULL,
    [name]   VARCHAR (MAX) NOT NULL,
    [number] TINYINT       NOT NULL,
    CONSTRAINT [PK_language] PRIMARY KEY CLUSTERED ([id] ASC),
    CONSTRAINT [IX_language] UNIQUE NONCLUSTERED ([code] ASC)
);


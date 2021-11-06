CREATE TABLE [dbo].[Move] (
    [id]             INT            IDENTITY (40000001, 1) NOT NULL,
    [code]           INT            NOT NULL,
    [character_code] TINYINT        NOT NULL,
    [number]         SMALLINT       NOT NULL,
    [description]    VARCHAR (MAX)  NOT NULL,
    [version]        DECIMAL (4, 2) NULL,
    CONSTRAINT [PK_Move_1] PRIMARY KEY CLUSTERED ([id] ASC),
    CONSTRAINT [FK_Move_character] FOREIGN KEY ([character_code]) REFERENCES [dbo].[character] ([code]),
    CONSTRAINT [FK_Move_version] FOREIGN KEY ([version]) REFERENCES [dbo].[tekkenVersion] ([version]) ON UPDATE CASCADE,
    CONSTRAINT [IX_Move] UNIQUE NONCLUSTERED ([character_code] ASC, [number] ASC),
    CONSTRAINT [IX_Move_1] UNIQUE NONCLUSTERED ([code] ASC)
);


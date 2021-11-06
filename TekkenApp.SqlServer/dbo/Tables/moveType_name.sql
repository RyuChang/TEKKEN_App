CREATE TABLE [dbo].[moveType_name] (
    [id]            INT           IDENTITY (60100001, 1) NOT NULL,
    [moveType_code] INT           NOT NULL,
    [language_code] CHAR (2)      NOT NULL,
    [name]          VARCHAR (MAX) NOT NULL,
    [Checked]       BIT           NOT NULL,
    CONSTRAINT [PK_moveType_name] PRIMARY KEY CLUSTERED ([id] ASC),
    CONSTRAINT [FK_moveType_name_language] FOREIGN KEY ([language_code]) REFERENCES [dbo].[language] ([code]),
    CONSTRAINT [FK_moveType_name_moveType] FOREIGN KEY ([moveType_code]) REFERENCES [dbo].[moveType] ([code]) ON UPDATE CASCADE,
    CONSTRAINT [IX_moveType_name] UNIQUE NONCLUSTERED ([language_code] ASC, [moveType_code] ASC)
);


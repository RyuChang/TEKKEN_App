CREATE TABLE [dbo].[moveSubType_name] (
    [id]               INT           IDENTITY (70100001, 1) NOT NULL,
    [moveSubType_code] INT           NOT NULL,
    [language_code]    CHAR (2)      NOT NULL,
    [name]             VARCHAR (MAX) NOT NULL,
    [Checked]          BIT           CONSTRAINT [DF_moveSubType_name_Checked] DEFAULT ((0)) NOT NULL,
    CONSTRAINT [PK_moveSubType_name_id] PRIMARY KEY CLUSTERED ([id] ASC),
    CONSTRAINT [FK_moveSubType_name_language] FOREIGN KEY ([language_code]) REFERENCES [dbo].[language] ([code]) ON UPDATE CASCADE,
    CONSTRAINT [FK_moveSubType_name_moveSubType_name] FOREIGN KEY ([moveSubType_code]) REFERENCES [dbo].[moveSubType] ([code]) ON UPDATE CASCADE,
    CONSTRAINT [IX_moveSubType_name] UNIQUE NONCLUSTERED ([moveSubType_code] ASC, [language_code] ASC)
);


CREATE TABLE [dbo].[moveText_name] (
    [id]            INT           IDENTITY (100100001, 1) NOT NULL,
    [moveText_code] INT           NOT NULL,
    [language_code] CHAR (2)      NOT NULL,
    [name]          VARCHAR (MAX) NOT NULL,
    [Checked]       BIT           CONSTRAINT [DF_moveText_name_Checked] DEFAULT ((0)) NOT NULL,
    CONSTRAINT [PK_moveText_name_id] PRIMARY KEY CLUSTERED ([id] ASC),
    CONSTRAINT [FK_moveText_name_language1] FOREIGN KEY ([language_code]) REFERENCES [dbo].[language] ([code]),
    CONSTRAINT [FK_moveText_name_moveText] FOREIGN KEY ([moveText_code]) REFERENCES [dbo].[moveText] ([code]),
    CONSTRAINT [IX_moveText_name] UNIQUE NONCLUSTERED ([moveText_code] ASC, [language_code] ASC)
);


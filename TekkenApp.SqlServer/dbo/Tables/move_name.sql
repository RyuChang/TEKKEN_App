CREATE TABLE [dbo].[move_name] (
    [id]            INT           IDENTITY (40100001, 1) NOT NULL,
    [move_code]     INT           NOT NULL,
    [language_code] CHAR (2)      NOT NULL,
    [name]          VARCHAR (MAX) NOT NULL,
    [Checked]       BIT           NULL,
    CONSTRAINT [PK_move_name] PRIMARY KEY CLUSTERED ([id] ASC),
    CONSTRAINT [FK_move_name_language] FOREIGN KEY ([language_code]) REFERENCES [dbo].[language] ([code]),
    CONSTRAINT [FK_move_name_Move] FOREIGN KEY ([move_code]) REFERENCES [dbo].[Move] ([code]) ON DELETE CASCADE ON UPDATE CASCADE,
    CONSTRAINT [IX_move_name] UNIQUE NONCLUSTERED ([move_code] ASC, [language_code] ASC)
);


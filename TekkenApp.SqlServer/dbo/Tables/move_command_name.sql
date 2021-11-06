CREATE TABLE [dbo].[move_command_name] (
    [id]                INT           IDENTITY (41000001, 1) NOT NULL,
    [Move_Command_code] INT           NOT NULL,
    [language_code]     CHAR (2)      NOT NULL,
    [name]              VARCHAR (MAX) NOT NULL,
    [checked]           BIT           NOT NULL,
    CONSTRAINT [PK_move_command_1] PRIMARY KEY CLUSTERED ([id] ASC),
    CONSTRAINT [FK_move_command_language] FOREIGN KEY ([language_code]) REFERENCES [dbo].[language] ([code]),
    CONSTRAINT [FK_move_command_Move] FOREIGN KEY ([Move_Command_code]) REFERENCES [dbo].[Move] ([code]) ON DELETE CASCADE ON UPDATE CASCADE,
    CONSTRAINT [IX_move_command] UNIQUE NONCLUSTERED ([language_code] ASC, [Move_Command_code] ASC)
);


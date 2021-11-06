CREATE TABLE [dbo].[Move_command] (
    [id]        INT           IDENTITY (41000001, 1) NOT NULL,
    [move_code] INT           NOT NULL,
    [command]   VARCHAR (MAX) NOT NULL,
    CONSTRAINT [PK_Move_command] PRIMARY KEY CLUSTERED ([id] ASC),
    CONSTRAINT [FK_Move_command_Move1] FOREIGN KEY ([move_code]) REFERENCES [dbo].[Move] ([code]) ON DELETE CASCADE ON UPDATE CASCADE,
    CONSTRAINT [IX_Move_command_1] UNIQUE NONCLUSTERED ([move_code] ASC)
);


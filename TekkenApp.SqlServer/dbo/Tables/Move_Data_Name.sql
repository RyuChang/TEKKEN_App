CREATE TABLE [dbo].[Move_Data_Name] (
    [id]               INT            IDENTITY (42100001, 1) NOT NULL,
    [Move_Data_Code]   INT            NOT NULL,
    [moveType_name]    NVARCHAR (MAX) NULL,
    [moveSubType_name] NVARCHAR (MAX) NULL,
    [StartType_name]   NVARCHAR (MAX) NULL,
    [Guardtype_name]   NVARCHAR (MAX) NULL,
    [hitType_name]     NVARCHAR (MAX) NULL,
    [counterType_name] NVARCHAR (MAX) NULL,
    [note_name]        NVARCHAR (MAX) NULL,
    [language_code]    CHAR (2)       NOT NULL,
    [checked]          BIT            NOT NULL,
    CONSTRAINT [PK_Move_Data_Name] PRIMARY KEY CLUSTERED ([id] ASC),
    CONSTRAINT [FK_Move_Data_Name_move_data] FOREIGN KEY ([Move_Data_Code]) REFERENCES [dbo].[move_data] ([Move_Code]) ON DELETE CASCADE ON UPDATE CASCADE,
    CONSTRAINT [IX_Move_Data_Name] UNIQUE NONCLUSTERED ([Move_Data_Code] ASC, [language_code] ASC)
);


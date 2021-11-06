CREATE TABLE [dbo].[move_data] (
    [id]                   INT            IDENTITY (42000001, 1) NOT NULL,
    [Move_Code]            INT            NOT NULL,
    [moveType_code]        INT            NULL,
    [moveSubType_code]     INT            NULL,
    [hitCount]             TINYINT        NOT NULL,
    [hitLevel]             VARCHAR (MAX)  NOT NULL,
    [damage]               SMALLINT       NOT NULL,
    [startFrame]           SMALLINT       NOT NULL,
    [startFrame_Display]   VARCHAR (MAX)  NULL,
    [startType_code]       INT            NULL,
    [guardFrame]           SMALLINT       NOT NULL,
    [guardFrame_Display]   VARCHAR (MAX)  NULL,
    [guardType_code]       INT            NOT NULL,
    [hitFrame]             SMALLINT       NOT NULL,
    [hitFrame_Display]     VARCHAR (MAX)  NULL,
    [hitType_code]         INT            NOT NULL,
    [counterFrame]         SMALLINT       NOT NULL,
    [counterFrame_Display] VARCHAR (MAX)  NULL,
    [counterType_code]     INT            NOT NULL,
    [breakThrow]           VARCHAR (MAX)  NULL,
    [afterBreak]           VARCHAR (MAX)  NULL,
    [homing]               BIT            NOT NULL,
    [powerCrush]           BIT            NOT NULL,
    [technicallyCrouching] BIT            NOT NULL,
    [technicallyJumping]   BIT            NOT NULL,
    [tailSpin]             BIT            NOT NULL,
    [wallSplat]            BIT            NOT NULL,
    [note]                 VARCHAR (MAX)  NULL,
    [version]              DECIMAL (4, 2) NULL,
    CONSTRAINT [PK_move_data] PRIMARY KEY CLUSTERED ([id] ASC),
    CONSTRAINT [FK_move_data_hitType] FOREIGN KEY ([guardType_code]) REFERENCES [dbo].[hitType] ([code]),
    CONSTRAINT [FK_move_data_hitType1] FOREIGN KEY ([startType_code]) REFERENCES [dbo].[hitType] ([code]),
    CONSTRAINT [FK_move_data_hitType2] FOREIGN KEY ([hitType_code]) REFERENCES [dbo].[hitType] ([code]),
    CONSTRAINT [FK_move_data_hitType3] FOREIGN KEY ([counterType_code]) REFERENCES [dbo].[hitType] ([code]),
    CONSTRAINT [FK_move_data_Move] FOREIGN KEY ([Move_Code]) REFERENCES [dbo].[Move] ([code]) ON DELETE CASCADE ON UPDATE CASCADE,
    CONSTRAINT [FK_move_data_moveSubType] FOREIGN KEY ([moveSubType_code]) REFERENCES [dbo].[moveSubType] ([code]),
    CONSTRAINT [FK_move_data_moveType] FOREIGN KEY ([moveType_code]) REFERENCES [dbo].[moveType] ([code])
);


GO
CREATE UNIQUE NONCLUSTERED INDEX [IX_move_data]
    ON [dbo].[move_data]([Move_Code] ASC);


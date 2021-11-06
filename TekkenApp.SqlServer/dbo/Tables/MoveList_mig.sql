CREATE TABLE [dbo].[MoveList_mig] (
    [character_code] CHAR (3)      NOT NULL,
    [code]           INT           IDENTITY (1, 1) NOT NULL,
    [country]        CHAR (10)     NOT NULL,
    [moveType]       CHAR (20)     NOT NULL,
    [name]           VARCHAR (MAX) NULL,
    [command]        VARCHAR (MAX) NULL,
    [start]          VARCHAR (MAX) NULL,
    [guard]          VARCHAR (MAX) NULL,
    [hit]            VARCHAR (MAX) NULL,
    [counter]        VARCHAR (MAX) NULL,
    [hitLevel]       VARCHAR (MAX) NULL,
    [dmg]            VARCHAR (MAX) NULL,
    [breakThrow]     VARCHAR (MAX) NULL,
    [afterBreak]     VARCHAR (MAX) NULL,
    [note]           VARCHAR (MAX) NULL
);


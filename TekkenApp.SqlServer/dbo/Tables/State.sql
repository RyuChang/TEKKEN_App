CREATE TABLE [dbo].[State] (
    [id]              INT           IDENTITY (90000001, 1) NOT NULL,
    [code]            INT           NOT NULL,
    [number]          TINYINT       NOT NULL,
    [StateGroup_code] INT           NOT NULL,
    [description]     VARCHAR (MAX) NOT NULL,
    CONSTRAINT [PK_State] PRIMARY KEY CLUSTERED ([id] ASC),
    CONSTRAINT [FK_State_StateGroup] FOREIGN KEY ([StateGroup_code]) REFERENCES [dbo].[StateGroup] ([code]) ON UPDATE CASCADE,
    CONSTRAINT [IX_State] UNIQUE NONCLUSTERED ([code] ASC),
    CONSTRAINT [IX_State_1] UNIQUE NONCLUSTERED ([StateGroup_code] ASC, [number] ASC)
);


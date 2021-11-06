CREATE TABLE [dbo].[StateGroup_name] (
    [id]              INT           IDENTITY (80100001, 1) NOT NULL,
    [StateGroup_code] INT           NOT NULL,
    [language_code]   CHAR (2)      NOT NULL,
    [name]            VARCHAR (MAX) NOT NULL,
    [Checked]         BIT           NOT NULL,
    CONSTRAINT [PK_StateGroup_name] PRIMARY KEY CLUSTERED ([id] ASC),
    CONSTRAINT [FK_StateGroup_name_language] FOREIGN KEY ([language_code]) REFERENCES [dbo].[language] ([code]),
    CONSTRAINT [FK_StateGroup_name_StateGroup] FOREIGN KEY ([StateGroup_code]) REFERENCES [dbo].[StateGroup] ([code]) ON UPDATE CASCADE,
    CONSTRAINT [IX_StateGroup_name] UNIQUE NONCLUSTERED ([StateGroup_code] ASC, [language_code] ASC)
);


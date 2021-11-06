CREATE TABLE [dbo].[State_name] (
    [id]            INT           IDENTITY (90100001, 1) NOT NULL,
    [state_code]    INT           NOT NULL,
    [language_code] CHAR (2)      NOT NULL,
    [name]          VARCHAR (MAX) NOT NULL,
    [checked]       BIT           NOT NULL,
    CONSTRAINT [PK_state_name] PRIMARY KEY CLUSTERED ([id] ASC),
    CONSTRAINT [FK_State_name_language] FOREIGN KEY ([language_code]) REFERENCES [dbo].[language] ([code]),
    CONSTRAINT [FK_State_name_State] FOREIGN KEY ([state_code]) REFERENCES [dbo].[State] ([code]) ON UPDATE CASCADE,
    CONSTRAINT [IX_State_name] UNIQUE NONCLUSTERED ([state_code] ASC, [language_code] ASC)
);


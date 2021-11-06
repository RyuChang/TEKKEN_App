CREATE TABLE [dbo].[hitType_name] (
    [id]            INT           IDENTITY (110100001, 1) NOT NULL,
    [hitType_code]  INT           NOT NULL,
    [language_code] CHAR (2)      NOT NULL,
    [name]          VARCHAR (MAX) NOT NULL,
    [checked]       BIT           NOT NULL,
    CONSTRAINT [PK_hitType_name] PRIMARY KEY CLUSTERED ([id] ASC),
    CONSTRAINT [FK_hitType_name_hitType] FOREIGN KEY ([hitType_code]) REFERENCES [dbo].[hitType] ([code]) ON DELETE CASCADE ON UPDATE CASCADE
);


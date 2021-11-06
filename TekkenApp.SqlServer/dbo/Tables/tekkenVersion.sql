CREATE TABLE [dbo].[tekkenVersion] (
    [ID]         INT            NULL,
    [version]    DECIMAL (4, 2) NOT NULL,
    [season]     TINYINT        NOT NULL,
    [updateDate] DATE           NOT NULL,
    CONSTRAINT [PK_version] PRIMARY KEY CLUSTERED ([version] ASC)
);


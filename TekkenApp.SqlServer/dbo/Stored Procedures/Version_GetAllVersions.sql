
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[Version_GetAllVersions]
AS
    BEGIN
        SET NOCOUNT ON;
        SELECT [Version], 
               [Season], 
               [UpdateDate]
        FROM [TEKKEN].[dbo].[TekkenVersion]
        ORDER BY Version DESC;
    END;
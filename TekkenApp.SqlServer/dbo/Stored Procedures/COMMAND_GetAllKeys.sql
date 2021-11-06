

-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[COMMAND_GetAllKeys]
AS
     SET NOCOUNT ON;
    BEGIN
        SET NOCOUNT ON;
        SELECT [key], 
               TRIM(code) AS CODE
        FROM   [TEKKEN].[dbo].[command]
        ORDER BY [KEY];
    END;

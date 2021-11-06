
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================

CREATE PROCEDURE [dbo].[Move_RecentDetail_del] @character_code TINYINT
AS
    BEGIN
        SET NOCOUNT ON;

        IF EXISTS
        (
            SELECT 1
            FROM   [TEKKEN].[dbo].[Move]
            WHERE  character_code = @character_code
        )
            BEGIN
                SELECT [character_code], 
                       MAX([number]) + 1 AS number, 
                       MAX([code]) + 1 AS code
                FROM   [TEKKEN].[dbo].[Move]
                WHERE  character_code = @character_code
                GROUP BY character_code;
            END;
            ELSE
            BEGIN
                SELECT(1 * 1000) + 1 AS code, 
                      1 AS number
                FROM  character
                WHERE CODE = @character_code;
            END;
    END;
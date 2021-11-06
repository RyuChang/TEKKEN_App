
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[Move_GetDetailById] @id            INT, 
                                           @language_code CHAR(2)
AS
     SET NOCOUNT ON;
    BEGIN
        SELECT move.[id] AS ID, 
               [character_code], 
               [number], 
               [moveType_code], 
               [moveSubType_code], 
               [code], 
               [Description], 
               [command], 
               [version]
        FROM   [TEKKEN].[dbo].[Move]
               LEFT OUTER JOIN [move_name] name ON move.code = NAME.move_code
        WHERE  move.id = @id
               AND NAME.language_code = @language_code
        ORDER BY number;
    END;

	
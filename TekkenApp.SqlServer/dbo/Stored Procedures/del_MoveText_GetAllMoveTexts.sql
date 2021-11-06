

-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================

CREATE PROCEDURE [dbo].[del_MoveText_GetAllMoveTexts] @language_code  CHAR(2), 
                                                 @character_code TINYINT = NULL
AS
    BEGIN
        SET NOCOUNT ON;
        BEGIN
            SET NOCOUNT ON;
            SELECT moveText.id, 
                   character_code, 
                   number, 
                   code, 
                   description, 
                   name
            FROM   [TEKKEN].[dbo].moveText AS moveText
                   LEFT OUTER JOIN [moveText_name] AS name ON moveText.code = NAME.moveText_code
            WHERE  NAME.language_code = @language_code
                   AND (@character_code IS NULL
                        OR character_code = @character_code)
            ORDER BY number;
        END;
    END;



-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================

CREATE PROCEDURE [dbo].[MoveSubType_MoveSubTypesSelectItems] @language_code  CHAR(2), 
                                                             @character_code TINYINT = NULL
AS
    BEGIN
        SET NOCOUNT ON;
        BEGIN
            SELECT name, 
					code
            FROM   [TEKKEN].[dbo].moveSubType AS moveSubType
                   LEFT OUTER JOIN [moveSubType_name] AS name ON moveSubType.code = NAME.moveSubType_code
            WHERE  NAME.language_code = @language_code
                   AND (@character_code IS NULL
                        OR character_code = @character_code)
            ORDER BY number;
        END;
    END;

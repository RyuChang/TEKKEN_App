


-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[MoveText_GetMoveText_ByCode] @code          INT, 
                                                     @language_code CHAR(2) = 'en'
AS
     SET NOCOUNT ON;
    BEGIN
        SELECT [name]
        FROM   [TEKKEN].[dbo].moveText moveText
               LEFT OUTER JOIN [moveText_name] name ON moveText.code = NAME.moveText_code
                                                       AND (NAME.language_code = @language_code)
        WHERE  moveText.code = @code;
    END;

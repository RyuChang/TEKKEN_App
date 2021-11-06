


-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[Move_GetAllMoveText_NamesBycode] @code          INT, 
                                                         @language_code CHAR(2)
AS
     SET NOCOUNT ON;
    BEGIN
        SELECT name
        FROM   [TEKKEN].[dbo].[MoveText]
               LEFT OUTER JOIN [moveText_name] name ON moveText.code = NAME.moveText_code
        WHERE  moveText.code = @code
               AND NAME.language_code = @language_code;
    END;

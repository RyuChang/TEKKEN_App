

-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[Move_GetAllMove_NamesBycode] @code          INT, 
                                         @language_code CHAR(2)
AS
     SET NOCOUNT ON;
    BEGIN
        SELECT name
        FROM   [TEKKEN].[dbo].[Move]
               LEFT OUTER JOIN [move_name] name ON move.code = NAME.move_code
        WHERE  move.code = @code
               AND NAME.language_code = @language_code;
    END;

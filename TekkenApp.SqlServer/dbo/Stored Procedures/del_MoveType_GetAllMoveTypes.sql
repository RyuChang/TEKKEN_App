
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[del_MoveType_GetAllMoveTypes] @language_code CHAR(2)
AS
     SET NOCOUNT ON;
    BEGIN
        SET NOCOUNT ON;
        SELECT moveType.id, 
               code, 
			   description,
               [name],
			   number
        FROM [TEKKEN].[dbo].moveType moveType
             LEFT OUTER JOIN [moveType_name] name ON moveType.code = NAME.moveType_code
        WHERE NAME.language_code = @language_code
        ORDER BY moveType.id;
    END;
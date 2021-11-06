



-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[MoveCommand_GetMoveAllNamesByCode] @CODE INT = 0
AS
     SET NOCOUNT ON;
    BEGIN
        SELECT language.code, 
               move_command_name.[name]
        FROM   LANGUAGE
               LEFT OUTER JOIN move_command_name ON move_command_name.Move_Command_code = @CODE
                                                    AND move_command_name.language_code = LANGUAGE.code
        ORDER BY LANGUAGE.number;
    END;

	

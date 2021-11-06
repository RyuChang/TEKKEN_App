


-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[del_MoveCommand_GetAllMoveCommands] @id INT
AS
     SET NOCOUNT ON;
     DECLARE @code INT;
    BEGIN

        SELECT @code = CODE
        FROM   Move
        WHERE  ID = @id;



        SELECT move_command_name.Id Id, 
               move.code, 
               language.code language_code, 
               move_name.name move_name, 
               move.command, 
               move_command_name.name name
        FROM
        (
            SELECT DISTINCT 
                   MOVE_CODE, 
                   NAME
            FROM   MOVE_NAME
        ) MOVE_NAME
        CROSS JOIN language
        RIGHT OUTER JOIN move ON move.code = move_name.move_code
        LEFT OUTER JOIN move_command_name ON move.code = move_command_name.Move_Command_code
                                             AND move_command_name.language_code = language.code
        WHERE (@code = 0
                    OR move.code = @code)
        ORDER BY move.code, 
                 language.number;
    END;

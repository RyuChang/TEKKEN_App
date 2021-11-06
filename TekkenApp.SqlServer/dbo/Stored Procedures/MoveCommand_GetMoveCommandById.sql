



-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[MoveCommand_GetMoveCommandById] @id INT
AS
     SET NOCOUNT ON;


     SELECT COMMAND.id, 
            MOVE.code, 
            MOVE.character_code, 
            MOVE.number, 
            MOVE.description, 
            command.command COMMAND, 
            LANGUAGE.code language_code, 
            NAME.name
     FROM   MOVE MOVE
            CROSS JOIN
     (
         SELECT code, 
                number
         FROM   LANGUAGE
     ) LANGUAGE
            LEFT OUTER JOIN Move_command COMMAND ON COMMAND.move_code = MOVE.code
            LEFT OUTER JOIN move_command_name NAME ON NAME.language_code = language.code
                                                      AND COMMAND.move_code = NAME.Move_Command_code
     WHERE  MOVE.ID = @id
     ORDER BY LANGUAGE.number;
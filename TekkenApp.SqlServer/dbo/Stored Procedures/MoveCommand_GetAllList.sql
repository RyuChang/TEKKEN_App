



-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[MoveCommand_GetAllList] @character_code TINYINT
AS
     SET NOCOUNT ON;

    BEGIN

        SELECT MOVE.id, 
               MOVE.code, 
               MOVE.character_code, 
               MOVE.number, 
               MOVE.description, 
               command.command COMMAND
        FROM
        (
            SELECT ID, 
                   CODE, 
                   character_code, 
                   number, 
                   description
            FROM   MOVE
        ) MOVE
        LEFT OUTER JOIN move_command COMMAND ON COMMAND.Move_Code = Move.code
        WHERE  character_code = @character_code
        ORDER BY move.code;
    END;

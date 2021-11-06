

-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[Move_GetAllMoves] @language_code  CHAR(2), 
                                         @character_code TINYINT = 0
AS
     SET NOCOUNT ON;
    BEGIN
        SELECT move.id AS id, 
               move_name.name AS name, 
               move.character_code AS character_code, 
               character_name.name AS Character_Name, 
               number, 
               moveType_name.name AS MoveType_Name, 
               moveSubType_name.name AS MoveSubType_Name, 
               Description, 
               code, 
               command, 
               move_command_name.name, 
               version
        FROM   Move Move
               LEFT OUTER JOIN move_name ON move.code = move_name.move_code
                                            AND move_name.language_code = @language_code
               LEFT OUTER JOIN character_name ON Move.character_code = character_name.character_code
                                                 AND character_name.language_code = @language_code
               LEFT OUTER JOIN moveType_name ON MOVE.moveType_code = moveType_name.moveType_code
                                                AND moveType_name.language_code = @language_code
               LEFT OUTER JOIN moveSubType_name ON MOVE.moveSubType_code = moveSubType_name.moveSubType_code
                                                   AND moveSubType_name.language_code = @language_code
               LEFT OUTER JOIN move_command_name ON MOVE.code = move_command_name.Move_Command_code
                                                    AND moveSubType_name.language_code = @language_code
        WHERE  (@character_code = 0
                OR move.character_code = @character_code)
        ORDER BY CODE;
    END;

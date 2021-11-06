

-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[MoveList_GetAllMoveLists] @language_code  CHAR(2), 
                                                  @character_code TINYINT
AS
     SET NOCOUNT ON;
    BEGIN
        SELECT move.[id], 
               [code], 
               [number], 
               move_name.[name], 
               move_command_name.name command, 
               moveType_name, 
               moveSubType_name, 
               StartType_name, 
               Guardtype_name, 
               hitType_name, 
               counterType_name note_name, 
			   hitCount,
			   hitLevel,	
			   damage,
			   startFrame,
			   secondFrame,
			   startType_code,
			   guardFrame,
			   guardType_code,
			   hitFrame,
			   hitType_code,
			   counterFrame,
			   counterType_code,
			   breakThrow,
			   afterBreak,
			   homing,
			   powerCrush,
			   technicallyCrouching,
			   technicallyJumping,
			   tailSpin,
			   wallSplat,
               move.[version]
        FROM   [TEKKEN].[dbo].[Move]
               LEFT OUTER JOIN move_name ON move.code = move_name.move_code
                                            AND move_name.language_code = @language_code
               LEFT OUTER JOIN move_command_name ON MOVE.code = move_command_name.Move_Command_code
                                                    AND move_command_name.language_code = @language_code
				LEFT OUTER JOIN move_data ON MOVE.code = move_data.Move_Code
               LEFT OUTER JOIN Move_Data_Name ON MOVE.code = Move_Data_Name.Move_Data_Code
                                                 AND Move_Data_Name.language_code = @language_code
        WHERE  move.character_code = @character_code
        ORDER BY NUMBER;


    END;

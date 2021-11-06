


-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[DumpData_GetAllDumps] @character_code TINYINT = 0
AS
     SET NOCOUNT ON;
    BEGIN
        SELECT TOP (1000) [id], 
                          [character_code], 
                          [number], 
                          [description], 
                          [name], 
                          [moveType_code], 
                          [moveSubType_code], 
                          [command], 
                          [hitCount], 
                          [hitLevel], 
                          [damage], 
                          [damageList], 
                          [startFrame], 
                          [secondFrame], 
                          [guardFrame], 
                          [hitFrame], 
                          [counterFrame], 
                          [guardType_code], 
                          [hitType_code], 
                          [counterType_code], 
                          [breakThrow], 
                          [afterBreak], 
                          [note], 
                          [homing], 
                          [powerCrush], 
                          [technicallyCrouching], 
                          [technicallyJumping], 
                          [tailSpin], 
                          [wallSplat]
        FROM              [TEKKEN].[dbo].[DumpData] data
        WHERE             (@character_code = 0
                           OR data.character_code = @character_code)
        ORDER BY [number];
    END;

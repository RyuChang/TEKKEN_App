



-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[MoveData_GetAllList] @character_code TINYINT
AS
     SET NOCOUNT ON;

     SELECT MOVE.id, 
            MOVE.code, 
            MOVE.character_code, 
            MOVE.number, 
            MOVE.description, 
            DATA.startType_code, 
            DATA.guardType_code, 
            DATA.hitType_code, 
            DATA.counterType_code, 
            ISNULL(DATA.id, 0) AS DATA_ID, 
            ISNULL(DATA.hitCount, '') AS HITCOUNT, 
            ISNULL(DATA.hitLevel, '') AS hitLevel, 
            ISNULL(DATA.Damage, 0) AS Damage, 
            ISNULL(DATA.startFrame, 0) AS startFrame, 
			ISNULL(DATA.startFrame_display, '') AS startFrame_display, 
            ISNULL(DATA.guardFrame, 0) AS guardFrame, 
			ISNULL(DATA.guardFrame_display, 0) AS guardFrame_display, 
            ISNULL(DATA.hitFrame, 0) AS hitFrame, 
			ISNULL(DATA.hitFrame_display, 0) AS hitFrame_display, 
            ISNULL(DATA.counterFrame, 0) AS counterFrame, 
			ISNULL(DATA.counterFrame_display, 0) AS counterFrame_display, 
            ISNULL(breakThrow, '') AS breakThrow, 
            ISNULL(afterBreak, '') AS afterBreak,
			homing,
			powerCrush,
			technicallyCrouching,
			technicallyJumping,
			tailSpin,
			wallSplat

     FROM
     (
         SELECT ID, 
                CODE, 
                character_code, 
                number, 
                description
         FROM   MOVE
     ) MOVE
     LEFT OUTER JOIN move_data DATA ON DATA.Move_Code = MOVE.code
     WHERE  character_code = @character_code
     ORDER BY MOVE.CODE;



-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[MoveData_GetMoveDataById] @id INT
AS
     SET NOCOUNT ON;

     SELECT ISNULL(DATA.id, '') AS ID, 
            DESCRIPTION, 
            MOVE.code AS CODE, 
            MOVE_CODE AS MOVE_CODE, 
            moveType_code AS MoveType_Code, 
            moveSubType_code AS MoveSubType_Code, 
            ISNULL(DATA.hitCount, '') AS HITCOUNT, 
            ISNULL(DATA.hitLevel, '') AS hitLevel, 
            ISNULL(DATA.Damage, 0) AS Damage, 
            ISNULL(DATA.startFrame, 0) AS startFrame, 
			ISNULL(DATA.startFrame_display, '') AS startFrame_display, 
            DATA.startType_code AS startType_code, 
            ISNULL(DATA.guardFrame, 0) AS guardFrame, 
			ISNULL(DATA.guardFrame_display, 0) AS guardFrame_display, 
            DATA.guardType_code AS guardType_code, 
            ISNULL(DATA.hitFrame, 0) AS hitFrame, 
			ISNULL(DATA.hitFrame_display, 0) AS hitFrame_display, 
            DATA.hitType_code AS hitType_code, 
            ISNULL(DATA.counterFrame, 0) AS counterFrame, 
			ISNULL(DATA.counterFrame_display, 0) AS counterFrame_display, 
            DATA.counterType_code AS counterType_code, 
            ISNULL(breakThrow, '') AS breakThrow, 
            ISNULL(afterBreak, '') AS afterBreak, 
            homing, 
            powerCrush, 
            technicallyCrouching, 
            technicallyJumping, 
            tailSpin, 
            wallSplat
     FROM   MOVE
            LEFT OUTER JOIN move_data DATA ON DATA.Move_Code = MOVE.code
     WHERE  Move.id = @id
     ORDER BY MOVE.CODE;
	 
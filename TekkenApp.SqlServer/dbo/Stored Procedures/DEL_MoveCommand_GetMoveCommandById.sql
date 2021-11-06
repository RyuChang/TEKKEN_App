



-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[DEL_MoveCommand_GetMoveCommandById] @id INT
AS
     SET NOCOUNT ON;

     SELECT ISNULL(COMMAND.id, '') AS ID, 
            DESCRIPTION, 
            MOVE.code AS CODE, 
            MOVE_CODE AS MOVE_CODE, 
			character_code,
            COMMAND
     FROM   MOVE
            LEFT OUTER JOIN Move_command COMMAND ON COMMAND.Move_Code = MOVE.code
     WHERE  Move.id = @id;

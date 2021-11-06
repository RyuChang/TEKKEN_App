

-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[del_MoveType_UpdateMoveType] @id          INT, 
                                                @code        INT, 
                                                @number      TINYINT, 
                                                @description VARCHAR(MAX)
AS
     UPDATE moveType
       SET  
           code = @code, 
           number = @number, 
           description = @description
     WHERE  ID = @ID;
    
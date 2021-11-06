
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[del_MoveSubtype_UpdateMoveSubType] @id             INT, 
                                                      @character_code TINYINT, 
                                                      @number         TINYINT, 
                                                      @description    VARCHAR(MAX)
AS
     DECLARE @err INT, @code INT;

     SET @err = 0;

     EXEC @code = [dbo].[GetCode] 
          @character_code = @character_code, 
          @tableName = N'movesubtype', 
          @number = @number;


     UPDATE moveSubType
       SET  
           code = @code, 
           number = @number, 
           description = @description
     WHERE  ID = @id;
  

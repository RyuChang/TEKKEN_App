

-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================

CREATE PROCEDURE [dbo].[Move_UpdateMove] @id               INT, 
                                        @character_code   TINYINT, 
                                        @number           TINYINT, 
                                        @moveType_code    INT, 
                                        @moveSubType_code INT, 
                                        @description      VARCHAR(MAX), 
                                        @command          VARCHAR(MAX), 
                                        @version          DECIMAL(4, 2)
AS
     DECLARE @err INT, @code INT;

     SET @err = 0;

     -- GetCode
     EXEC @code = [dbo].[GetCode] 
          @character_code = @character_code, 
          @tableName = N'move', 
          @number = @number;

     BEGIN TRAN;

     UPDATE Move
       SET  
           code = @code, 
           number = @number, 
           moveType_code = @moveType_code, 
           moveSubType_code = @moveSubType_code, 
           description = @description, 
           command = @command, 
           version = @version
     WHERE  ID = @id;

     COMMIT TRAN;


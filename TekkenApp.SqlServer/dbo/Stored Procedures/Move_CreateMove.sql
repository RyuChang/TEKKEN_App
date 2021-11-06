
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================

CREATE PROCEDURE [dbo].[Move_CreateMove] @character_code   TINYINT, 
                                        @number           TINYINT, 
                                        @moveType_code    INT, 
                                        @moveSubType_code INT, 
                                        @description      VARCHAR(MAX), 
                                        @command          VARCHAR(MAX), 
                                        @version          DECIMAL(4, 2), 
                                        @moveCode         INT OUTPUT
AS
     DECLARE @err INT, @code INT;

     SET @err = 0;

     -- GetCode
     EXEC @code = [dbo].[GetCode] 
          @character_code = @character_code, 
          @tableName = N'move', 
          @number = @number;

     BEGIN TRAN;

     INSERT INTO Move
     (code, 
      character_code, 
      number, 
      [moveType_code], 
      [moveSubType_code], 
      [description], 
      [command], 
      [version]
     )
     VALUES
     (@code, 
      @character_code, 
      @number, 
      @moveType_code, 
      @moveSubType_code, 
      @description, 
      @command, 
      @version
     );



     SET @err = @@ERROR;
     IF @err <> 0
         BEGIN
             ROLLBACK TRAN;
             RETURN;
         END;

     DECLARE @return_value INT, @cnt INT, @i INT, @language_code CHAR(2);

     SELECT @cnt = COUNT(*)
     FROM   language;

     SET @i = 0;
     WHILE(@i < @cnt)
         BEGIN
             SET @i = @i + 1;
             SELECT @language_code = CODE
             FROM   language
             WHERE  number = @i;
			 
/*
             EXEC @return_value = [dbo].[Move_CreateMove_name] 
                  @move_code = @code, 
                  @language_code = @language_code, 
                  @name = @description;
			*/

             EXEC @return_value = [dbo].[TranslateName_CreateTranslateName] 
                  @tableName = 'Move', 
                  @code = @code, 
                  @name = @description, 
                  @language_code = @language_code;

             SET @err = @@ERROR;
             IF @err <> 0
                 BEGIN
                     ROLLBACK TRAN;
                     RETURN;
                 END;
         END;
     COMMIT TRAN;
     SET @moveCode = @code;
     RETURN @moveCode;



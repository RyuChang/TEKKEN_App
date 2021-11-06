
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================

CREATE PROCEDURE [dbo].[Base_Create] @tableName       NVARCHAR(MAX), 
                                    @character_code  TINYINT       = 0, 
                                    @stateGroup_code INT           = 0, 
                                    @number          TINYINT, 
                                    @description     NVARCHAR(MAX)
AS
     DECLARE @err INT, @code INT;

     SET @err = 0;

     EXEC @code = [dbo].[GetCreateCode] 
          @character_code = @character_code, 
          @stateGroup_code = @stateGroup_code, 
          @tableName = @tableName, 
          @number = @number;

     BEGIN TRAN;

    BEGIN
        IF(@stateGroup_code > 0)
            BEGIN
                EXEC [Base_Create_StateGroup] 
                     @tableName = @tableName, 
                     @code = @code, 
                     @stateGroup_code = @stateGroup_code, 
                     @number = @number, 
                     @description = @description;
            END;
            ELSE
            IF(@character_code = 0)
                BEGIN
                    EXEC [Base_Create_None] 
                         @tableName = @tableName, 
                         @code = @code, 
                         @number = @number, 
                         @description = @description;

                END;
                ELSE
                IF(@character_code > 0)
                    BEGIN
                        SELECT @code, 
                               @character_code, 
                               @number, 
                               @description;

                        EXEC [Base_Create_Character] 
                             @tableName = @tableName, 
                             @code = @code, 
                             @character_code = @character_code, 
                             @number = @number, 
                             @description = @description;
                        SELECT 3;
                    END;

    END;




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
             EXEC @return_value = [dbo].[TranslateName_CreateTranslateName] 
                  @code = @code, 
                  @tableName = @tableName, 
                  @language_code = @language_code, 
                  @name = @description;

             SET @err = @@ERROR;
             IF @err <> 0
                 BEGIN
                     ROLLBACK TRAN;
                     RETURN;
                 END;
         END;


     COMMIT TRAN;
	         
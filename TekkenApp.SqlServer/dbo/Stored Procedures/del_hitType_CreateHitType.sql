

-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[del_hitType_CreateHitType] @number      TINYINT, 
                                              @description VARCHAR(MAX)
AS
     DECLARE @err INT, @code INT;
     SET @err = 0;

     -- GetCode
     EXEC @code = [dbo].[GetCode] 
          @tableName = N'hitType', 
          @number = @number;

     BEGIN TRAN;

     INSERT INTO hitType
     (code, 
      number, 
      description
     )
     VALUES
     (@code, 
      @number, 
      @description
     );

     SET @err = @@ERROR;
     IF @err <> 0
         BEGIN
             ROLLBACK TRAN;
             RETURN;
         END;

     DECLARE @return_value INT, @cnt INT, @i INT, @language_code CHAR(2);

     SELECT @cnt = COUNT(1)
     FROM   language;

     SET @i = 0;
     WHILE(@i < @cnt)
         BEGIN
             SET @i = @i + 1;
             SELECT @language_code = CODE
             FROM   language
             WHERE  number = @i;

             EXEC @return_value = [dbo].[TranslateName_CreateTranslateName] 
                  @tableName = 'hitType', 
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

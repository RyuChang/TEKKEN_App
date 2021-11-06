
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[del_State_CreateState] @number          TINYINT, 
                                          @StateGroup_code INT, 
                                          @description     VARCHAR(MAX)
AS
     DECLARE @err INT, @code INT;
     SET @err = 0;

     -- GetCode
     EXEC @code = [dbo].[GetCodeByGroup] 
          @tableName = N'State', 
          @number = @number, 
          @StateGroup_code = @StateGroup_code;


     BEGIN TRAN;
     INSERT INTO State
     (code, 
      number, 
      StateGroup_code, 
      description
     )
     VALUES
     (@code, 
      @number, 
      @StateGroup_code, 
      @description
     );
     SET @err = @@ERROR;
     IF @err <> 0
         BEGIN
             ROLLBACK TRAN;
             RETURN;
         END;
     DECLARE @return_value INT;
     DECLARE @cnt INT;
     DECLARE @i INT;
     DECLARE @language_code CHAR(2);
     SELECT @cnt = COUNT(*)
     FROM   language;
     SET @i = 0;
     WHILE(@i < @cnt)
         BEGIN
             SET @i = @i + 1;
             SELECT @language_code = CODE
             FROM   language
             WHERE  number = @i;
             EXEC @return_value = [dbo].[State_CreateState_name] 
                  @State_code = @code, 
                  @language_code = @language_code, 
                  @name = @description;
             SET @err = @@ERROR;
             IF @err <> 0
                 BEGIN
                     ROLLBACK TRAN;
                     RETURN;
                 END;

                     --	 print(@err);
         END;
     COMMIT TRAN;

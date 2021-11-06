
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[del_StateGroup_UpdateStateGroup] @id          INT, 
                                                    @number      TINYINT, 
                                                    @description VARCHAR(MAX)
AS
     DECLARE @err INT, @code INT;
     SET @err = 0;

     -- GetCode
     EXEC @code = [dbo].[GetCode] 
          @tableName = N'StateGroup', 
          @number = @number;

     BEGIN TRAN;

     UPDATE StateGroup
       SET  
           code = @code, 
           number = @number, 
           description = @description
     WHERE  id = @id;

     COMMIT TRAN;
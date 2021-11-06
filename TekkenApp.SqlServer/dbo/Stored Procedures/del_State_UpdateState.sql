
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[del_State_UpdateState] @id          INT, 
                                          @number      TINYINT, 
                                          @description VARCHAR(MAX)
AS
     DECLARE @err INT;
     SET @err = 0;
     BEGIN TRAN;
     UPDATE State
       SET number = @number, 
           description = @description
     WHERE ID = @ID;
     SET @err = @@ERROR;
     IF @err <> 0
         BEGIN
             ROLLBACK;
             RETURN;
         END;
     DECLARE @return_value INT;
     SET @err = @@ERROR;
     IF @err <> 0
         BEGIN
             ROLLBACK TRAN;
             RETURN;
         END;
     COMMIT;

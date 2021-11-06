


-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================

CREATE PROCEDURE [dbo].[MoveCommand_Merge] @Id      INT, 
                                          @code    INT, 
                                          @command VARCHAR(MAX)
AS
     SET NOCOUNT ON;
     DECLARE @err INT;

     SET @err = 0;
     BEGIN TRAN;


     SET @err = @@ERROR;
     IF @err <> 0
         BEGIN
             ROLLBACK TRAN;
             RETURN;
         END;
     --SELECT @Move_Code;

    BEGIN
        MERGE MOVE_COMMAND AS MOVE_COMMAND
        USING
        (
            SELECT @code
        ) AS COMMAND(MOVE_CODE)
        ON MOVE_COMMAND.MOVE_CODE = COMMAND.MOVE_CODE
            WHEN MATCHED
            THEN UPDATE SET 
                            Move_Code = @code, 
                            command = @command
            WHEN NOT MATCHED
            THEN
              INSERT(Move_Code, 
                     Command)
              VALUES
        (@code, 
         @command
        );



        SET @err = @@ERROR;
        IF @err <> 0
            BEGIN
                ROLLBACK TRAN;
                RETURN;
            END;
    END;
     COMMIT TRAN;


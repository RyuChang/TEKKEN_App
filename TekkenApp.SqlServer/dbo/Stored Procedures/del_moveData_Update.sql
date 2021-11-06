


-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================

CREATE PROCEDURE [dbo].[del_moveData_Update] @Move_Code        INT, 
                                         @hitCount         TINYINT, 
                                         @hitLevel         NVARCHAR(MAX), 
                                         @damage           SMALLINT, 
                                         @startFrame       SMALLINT, 
                                         @secondFrame      SMALLINT, 
                                         @startType_code   INT, 
                                         @guardFrame       SMALLINT, 
                                         @guardType_code   INT, 
                                         @hitFrame         SMALLINT, 
                                         @hitType_code     INT, 
                                         @counterFrame     SMALLINT, 
                                         @counterType_code INT, 
                                         @breakThrow       NVARCHAR(MAX) = '', 
                                         @afterBreak       NVARCHAR(MAX) = '', 
                                         @version          DECIMAL       = NULL
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

     DECLARE @return_value INT, @cnt INT, @i INT, @language_code CHAR(2);

    BEGIN
        UPDATE move_data
        SET  
         hitCount = @hitCount, 
         hitLevel=@hitLevel, 
         damage=@damage, 
         startFrame=@startFrame, 
         secondFrame=@secondFrame, 
         startType_code=@startType_code, 
         guardFrame=@guardFrame, 
         guardType_code=@guardType_code, 
         hitFrame=@hitFrame, 
         hitType_code=@hitType_code, 
         counterFrame=@counterFrame,  
         counterType_code=@counterType_code, 
         breakThrow=@breakThrow, 
         afterBreak=@afterBreak, 
         version=@version
        
        
		WHERE MOVE_CODE=@Move_Code;

        SET @err = @@ERROR;
        IF @err <> 0
            BEGIN
                ROLLBACK TRAN;
                RETURN;
            END;
    END;
     COMMIT TRAN;
	         

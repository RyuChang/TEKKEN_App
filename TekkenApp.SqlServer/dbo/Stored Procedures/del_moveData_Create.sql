

-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================

CREATE PROCEDURE [dbo].[del_moveData_Create] @Move_Code        INT, 
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

     DECLARE @return_value INT, @cnt INT,  @language_code CHAR(2);

    BEGIN
        INSERT INTO move_data
        (Move_Code, 
         hitCount, 
         hitLevel, 
         damage, 
         startFrame, 
         secondFrame, 
         startType_code, 
         guardFrame, 
         guardType_code, 
         hitFrame, 
         hitType_code, 
         counterFrame, 
         counterType_code, 
         breakThrow, 
         afterBreak, 
         version
        )
        VALUES
        (@Move_Code, 
         @hitCount, 
         @hitLevel, 
         @damage, 
         @startFrame, 
         @secondFrame, 
         @startType_code, 
         @guardFrame, 
         @guardType_code, 
         @hitFrame, 
         @hitType_code, 
         @counterFrame, 
         @counterType_code, 
         @breakThrow, 
         @afterBreak, 
         @version
        );

        SET @err = @@ERROR;
        IF @err <> 0
            BEGIN
                ROLLBACK TRAN;
                RETURN;
            END;
    END;
     COMMIT TRAN;
	         




-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================

CREATE PROCEDURE [dbo].[moveData_Merge] @Move_Code            INT, 
                                       @moveType_code        INT, 
                                       @moveSubType_code     INT, 
                                       @hitCount             TINYINT, 
                                       @hitLevel             NVARCHAR(MAX), 
                                       @damage               SMALLINT, 
                                       @startFrame           SMALLINT, 
									   @startFrame_display   NVARCHAR(MAX), 
                                       @startType_code       INT, 
                                       @guardFrame           SMALLINT, 
									   @guardFrame_display   NVARCHAR(MAX), 
                                       @guardType_code       INT, 
                                       @hitFrame             SMALLINT, 
									   @hitFrame_display   NVARCHAR(MAX), 
                                       @hitType_code         INT, 
                                       @counterFrame         SMALLINT, 
									   @counterFrame_display   NVARCHAR(MAX), 
                                       @counterType_code     INT, 
                                       @breakThrow           NVARCHAR(MAX) = '', 
                                       @afterBreak           NVARCHAR(MAX) = '', 
                                       @homing               BIT, 
                                       @powerCrush           BIT, 
                                       @technicallyCrouching BIT, 
                                       @technicallyJumping   BIT, 
                                       @tailSpin             BIT, 
                                       @wallSplat            BIT, 
                                       @version              DECIMAL       = NULL
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
        MERGE MOVE_DATA AS MOVE_DATA
        USING
        (
            SELECT @Move_Code
        ) AS DATA(MOVE_CODE)
        ON MOVE_DATA.MOVE_CODE = DATA.MOVE_CODE
            WHEN MATCHED
            THEN UPDATE SET 
                            moveType_code = @moveType_code, 
                            moveSubType_code = @moveSubType_code, 
                            hitCount = @hitCount, 
                            hitLevel = @hitLevel, 
                            damage = @damage, 
                            startFrame = @startFrame, 
							startFrame_display=@startFrame_display,
                            --secondFrame = @secondFrame, 
                            --startType_code = @startType_code, 
                            guardFrame = @guardFrame, 
							guardFrame_display=@guardFrame_display,
                            guardType_code = @guardType_code, 
                            hitFrame = @hitFrame, 
							hitFrame_display=@hitFrame_display,
                            hitType_code = @hitType_code, 
                            counterFrame = @counterFrame, 
							counterFrame_display=@counterFrame_display,
                            counterType_code = @counterType_code, 
                            breakThrow = @breakThrow, 
                            afterBreak = @afterBreak, 
                            homing = @homing, 
                            powerCrush = @powerCrush, 
                            technicallyCrouching = @technicallyCrouching, 
                            technicallyJumping = @technicallyJumping, 
                            tailSpin = @tailSpin, 
                            wallSplat = @wallSplat, 
                            version = @version
            WHEN NOT MATCHED
            THEN
              INSERT(Move_Code, 
                     moveType_code, 
                     moveSubType_code, 
                     hitCount, 
                     hitLevel, 
                     damage, 
                     startFrame, 
					 startFrame_display,
                     --secondFrame, 
                     --startType_code, 
                     guardFrame, 
					 guardFrame_display,
                     guardType_code, 
                     hitFrame, 
					 hitFrame_display,
                     hitType_code, 
                     counterFrame, 
					 counterFrame_display,
                     counterType_code, 
                     breakThrow, 
                     afterBreak, 
                     homing, 
                     powerCrush, 
                     technicallyCrouching, 
                     technicallyJumping, 
                     tailSpin, 
                     wallSplat, 
                     version)
              VALUES
        (@Move_Code, 
         @moveType_code, 
         @moveSubType_code, 
         @hitCount, 
         @hitLevel, 
         @damage, 
         @startFrame, 
		 @startFrame_display,
         --@secondFrame, 
         --@startType_code, 
         @guardFrame, 
		 @guardFrame_display,
         @guardType_code, 
         @hitFrame, 
		 @hitFrame_display,
         @hitType_code, 
         @counterFrame, 
		 @counterFrame_display,
         @counterType_code, 
         @breakThrow, 
         @afterBreak, 
         @homing, 
         @powerCrush, 
         @technicallyCrouching, 
         @technicallyJumping, 
         @tailSpin, 
         @wallSplat, 
         @version
        );


        DECLARE @return_value INT, @cnt INT, @i INT, @language_code2 CHAR(2);
        SELECT @cnt = COUNT(*)
        FROM   language;

        SET @i = 0;
        WHILE(@i < @cnt)
            BEGIN
                SET @i = @i + 1;

                SELECT @language_code2 = CODE
                FROM   language
                WHERE  number = @i;

                EXEC @return_value = [dbo].[MoveData_MergeTranslateName] 
                     @code = @Move_Code, 
                     @language_code = @language_code2;

                SET @err = @@ERROR;
                IF @err <> 0
                    BEGIN
                        ROLLBACK TRAN;
                        RETURN;
                    END;
            END;



        SET @err = @@ERROR;
        IF @err <> 0
            BEGIN
                ROLLBACK TRAN;
                RETURN;
            END;
    END;
     COMMIT TRAN;

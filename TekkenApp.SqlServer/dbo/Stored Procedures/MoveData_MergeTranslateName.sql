


-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[MoveData_MergeTranslateName] @code          INT, 
                                                    @language_code CHAR(2)
AS
     DECLARE @err INT;

     SET @err = 0;
     SET NOCOUNT ON;

     BEGIN TRAN;


     MERGE INTO Move_Data_Name NAME
     USING
     (
         SELECT MOVE_CODE, 
                [moveType_code], 
                [moveSubType_code], 
                [startType_code], 
                [guardType_code], 
                [hitType_code], 
                [counterType_code]
         FROM   MOVE_DATA
         WHERE  MOVE_CODE = @code
     ) DATA
     ON NAME.MOVE_DATA_CODE = DATA.MOVE_CODE
        AND NAME.LANGUAGE_CODE = @language_code
         WHEN MATCHED
         THEN UPDATE SET 
                         STARTTYPE_NAME = dbo.GetHitType_NameByCode([startType_code], @language_code)
         WHEN NOT MATCHED
         THEN
           INSERT(Move_Data_Code, 
                  [moveType_name], 
                  [moveSubType_name], 
                  [StartType_name], 
                  [Guardtype_name], 
                  [hitType_name], 
                  [counterType_name], 
                  language_code, 
                  checked)
           VALUES
     (@code, 
      dbo.GetMoveType_NameByCode([moveType_code], @language_code), 
      dbo.GetMoveSubType_NameByCode([moveSubType_code], @language_code), 
      dbo.GetHitType_NameByCode([startType_code], @language_code), 
      dbo.GetHitType_NameByCode([guardType_code], @language_code), 
      dbo.GetHitType_NameByCode([hitType_code], @language_code), 
      dbo.GetHitType_NameByCode([counterType_code], @language_code), 
      @language_code, 
      0
     );

	 /*
     IF EXISTS
     (
         SELECT 1
         FROM   Move_Data_Name
         WHERE  Move_Data_Code = @code
                AND language_code = @language_code
     )
         BEGIN
             SELECT 111;
             UPDATE move_data_name
               SET  
                   Move_Data_Code = @code, 
                   StartType_name = dbo.GetHitType_NameByCode([startType_code], @language_code), 
                   Guardtype_name = dbo.GetHitType_NameByCode([guardType_code], @language_code), 
                   hitType_name = dbo.GetHitType_NameByCode([hitType_code], @language_code), 
                   counterType_name = dbo.GetHitType_NameByCode([counterType_code], @language_code), 
                   checked = 0
             FROM   move_data_name
             WHERE  Move_Data_Code = @code AND language_code = @language_code;

         END;
         ELSE
         BEGIN
             SELECT 222;
             INSERT INTO move_data_name
                    SELECT @code, 
                           dbo.GetHitType_NameByCode([startType_code], @language_code), 
                           dbo.GetHitType_NameByCode([guardType_code], @language_code), 
                           dbo.GetHitType_NameByCode([hitType_code], @language_code), 
                           dbo.GetHitType_NameByCode([counterType_code], @language_code), 
                           @language_code, 
                           0
                    FROM   Move_Data
                    WHERE  [Move_Code] = @code;

         END;


		 */



     COMMIT TRAN;
	         
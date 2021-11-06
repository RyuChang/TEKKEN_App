

-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[del_MoveType_GetLastDetail_del]
AS
     SET NOCOUNT ON;
     DECLARE @characterNumber INT;
     DECLARE @maxNumber INT;
     DECLARE @maxCode INT;


     IF EXISTS
     (
         SELECT 1
         FROM   [TEKKEN].[dbo].[moveType]
     )
         BEGIN
             SELECT @maxNumber = MAX([number]) + 1, 
                    @maxCode = MAX([code]) + 1
             FROM   [TEKKEN].[dbo].[moveType];
         END;
         ELSE
         BEGIN
             SET @maxNumber = 1;
             EXEC @maxCode = [dbo].[GetCode] 
                  @tableName = N'moveType',
				  @number=@maxNumber;
         END;
     SELECT @maxCode code, 
            @maxNumber number;

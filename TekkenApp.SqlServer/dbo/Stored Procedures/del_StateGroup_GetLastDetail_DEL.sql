

-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[del_StateGroup_GetLastDetail_DEL]
AS
     DECLARE @maxNumber INT;
     DECLARE @maxCode INT;
     SET NOCOUNT ON;
     IF EXISTS
     (
         SELECT 1
         FROM [TEKKEN].[dbo].[StateGroup]
     )
         BEGIN
             SELECT @maxNumber = MAX([number]) + 1, 
                    @maxCode = MAX([code]) + 1
             FROM [TEKKEN].[dbo].[StateGroup];
         END;
         ELSE
         BEGIN
             SET @maxNumber = 1;
             SET @maxCode = @maxNumber;
         END;
     SELECT @maxCode code, 
            @maxNumber number;

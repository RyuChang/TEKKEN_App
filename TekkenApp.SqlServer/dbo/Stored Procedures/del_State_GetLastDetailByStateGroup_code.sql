
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[del_State_GetLastDetailByStateGroup_code] @stateGroup_code INT
AS
     DECLARE @maxNumber INT;
     DECLARE @maxCode INT;
     SET NOCOUNT ON;
     IF EXISTS
     (
         SELECT 1
         FROM   [TEKKEN].[dbo].[State]
         WHERE  StateGroup_code = @stateGroup_code
     )
         BEGIN
             SELECT @maxNumber = MAX([number]) + 1
             FROM   [TEKKEN].[dbo].[State]
             WHERE  StateGroup_code = @stateGroup_code;
         END;
         ELSE
         BEGIN
             SET @maxNumber = 1;
         END;
     IF EXISTS
     (
         SELECT 1
         FROM   [TEKKEN].[dbo].[State]
     )
         BEGIN
             SELECT @maxCode = MAX([code]) + 1
             FROM   [TEKKEN].[dbo].[State];
         END;
         ELSE
         BEGIN
             SET @maxNumber = 1;
             SET @maxCode = 1;
         END;

     SELECT @stateGroup_code stateGroup_code, 
            @maxCode code, 
            @maxNumber number;
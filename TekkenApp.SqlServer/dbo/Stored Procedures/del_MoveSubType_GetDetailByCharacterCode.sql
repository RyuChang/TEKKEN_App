
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[del_MoveSubType_GetDetailByCharacterCode] @character_code CHAR(3) = NULL
AS
     SET NOCOUNT ON;
    BEGIN
        SELECT TOP (1) id, 
                       code, 
                       character_code, 
                       number, 
                       description
        FROM [TEKKEN].[dbo].[moveSubType]
        WHERE(@character_code IS NULL
              OR character_code = @character_code);
    END;
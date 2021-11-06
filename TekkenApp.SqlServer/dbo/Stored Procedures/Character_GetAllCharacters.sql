
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[Character_GetAllCharacters] @language_code CHAR(2)
AS
    BEGIN
        SET NOCOUNT ON;
        SELECT character.ID, 
               CODE, 
               SEASON, 
               NAME, 
               fullName
        FROM [TEKKEN].[dbo].character character
             LEFT OUTER JOIN [character_name] name ON character.code = NAME.character_code
        WHERE NAME.language_code = @language_code
        ORDER BY character.ID;
    END;
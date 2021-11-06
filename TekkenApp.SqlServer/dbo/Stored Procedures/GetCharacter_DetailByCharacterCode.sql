
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================

CREATE PROCEDURE [dbo].[GetCharacter_DetailByCharacterCode] 
				@character_code tinyint, @language_code char(2)
AS
BEGIN
	SET NOCOUNT ON;
	BEGIN
		SELECT character.id, CODE, SEASON, NAME, fullName
		FROM [TEKKEN].[dbo].character AS character
			 LEFT OUTER JOIN
			 [character_name] AS name
			 ON character.code = NAME.character_code
		WHERE character_code = @character_code AND 
			  NAME.language_code = @language_code
		ORDER BY character.id;
	END;
END;
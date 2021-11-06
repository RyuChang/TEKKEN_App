

-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================

CREATE PROCEDURE [dbo].[del_MoveSubType_GetAllMoveSubTypes] 
				@language_code char(2), @character_code tinyint= NULL
AS
BEGIN
	SET NOCOUNT ON;
	BEGIN
		SELECT moveSubType.id, character_code, number, code, description, name
		FROM [TEKKEN].[dbo].moveSubType AS moveSubType
			 LEFT OUTER JOIN
			 [moveSubType_name] AS name
			 ON moveSubType.code = NAME.moveSubType_code
		WHERE NAME.language_code = @language_code AND 
			  ( @character_code IS NULL OR 
				character_code = @character_code
			  )
		ORDER BY number;
	END;
END;

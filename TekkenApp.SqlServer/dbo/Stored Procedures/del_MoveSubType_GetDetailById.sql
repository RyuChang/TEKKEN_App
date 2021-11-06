
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[del_MoveSubType_GetDetailById] @id            INT, 
                                               @language_code CHAR(2)
AS
     SET NOCOUNT ON;
     SELECT moveSubType.id, 
            code, 
            character_code, 
            number, 
            name.name name, 
            description
     FROM [TEKKEN].[dbo].[moveSubType] moveSubType
          LEFT OUTER JOIN [TEKKEN].[dbo].[moveSubType_name] name ON moveSubType.code = name.moveSubType_code
                                                                    AND (name.language_code = @language_code)
     WHERE moveSubType.id = @id;

    
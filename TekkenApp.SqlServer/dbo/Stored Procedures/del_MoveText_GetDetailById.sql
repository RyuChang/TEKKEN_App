

-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[del_MoveText_GetDetailById] @id            INT, 
                                                @language_code CHAR(2)
AS
     SET NOCOUNT ON;
     SELECT moveText.id, 
            code, 
            character_code, 
            number, 
            name.name name, 
            description
     FROM   [TEKKEN].[dbo].[moveText] [moveText]
            LEFT OUTER JOIN [TEKKEN].[dbo].[moveText_name] name ON [moveText].code = name.moveText_code
                                                                   AND (name.language_code = @language_code)
     WHERE  moveText.id = @id;



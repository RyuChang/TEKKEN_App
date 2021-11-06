
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[del_MoveType_GetDetailById] @id            INT, 
                                            @language_code CHAR(2)
AS
     SET NOCOUNT ON;
     SELECT moveType.id, 
            code, 
            number, 
            name.name name, 
            description
     FROM [TEKKEN].[dbo].[moveType] moveType
          LEFT OUTER JOIN [TEKKEN].[dbo].[moveType_name] name ON moveType.code = name.moveType_code
                                                                 AND (name.language_code = @language_code)
     WHERE moveType.id = @id;
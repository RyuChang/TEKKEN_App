

-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[TranslateName_GetTranslateNameById] @tableName    VARCHAR(MAX), 
                                                           @subTableName VARCHAR(MAX) = 'NONE', 
                                                           @id           INT
AS
     SET NOCOUNT ON;
     DECLARE @sql NVARCHAR(MAX);

     IF(@subTableName = 'NONE')
         BEGIN
             SET @subTableName = @tableName;
         END;
         ELSE
         BEGIN
             SET @subTableName = @subTableName;
         END;

     SET @sql = 'SELECT ''' + @subTableName + ''' AS tableName,
						name.id, 
						code, 
						description, 
						name.language_code language_code,
						name.name name,
						checked
				 FROM [TEKKEN].[dbo].[' + @tableName + '] data
					  INNER JOIN [TEKKEN].[dbo].[' + @subTableName + '_name] name
					  ON data.code = name.' + @subTableName + '_code       
     WHERE name.id=' + CAST(@id AS CHAR);
     EXEC (@sql);

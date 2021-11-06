


-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[TranslateName_GetBaseModelById] @tableName    VARCHAR(MAX), 
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

     SET @sql = 'SELECT id, 
						code, 
						number,
						description
				 FROM [TEKKEN].[dbo].[' + @subTableName + '] data
			     WHERE id=' + CAST(@id AS CHAR);
     EXEC (@sql);

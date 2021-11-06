
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[Base_GetNewNumber] @tableName       VARCHAR(MAX), 
                                          @character_code  TINYINT      = NULL, 
                                          @stateGroup_code INT          = NULL
AS
     SET NOCOUNT ON;

     DECLARE @sql VARCHAR(MAX), @sql_exists VARCHAR(MAX), @sql_column VARCHAR(MAX), @sql_Clause VARCHAR(MAX), @sql_stateGroup VARCHAR(MAX), @rowCount INT, @maxNumber TINYINT;

     SET @sql_column = '';
     SET @sql_Clause = '';
	 
     IF(@character_code != 0)
         BEGIN
             SET @sql_column+='''' + CAST(@character_code AS NVARCHAR(MAX)) + ''' AS character_code, ';
             SET @sql_Clause+=' AND  character_code = ' + CAST(@character_code AS NVARCHAR(2));
         END;

     IF(@stateGroup_code IS NOT NULL)
         BEGIN
             SET @sql_column+='''' + CAST(@stateGroup_code AS NVARCHAR(MAX)) + ''' AS stateGroup_code, ';
             SET @sql_Clause+=' AND  stateGroup_code = ' + CAST(@stateGroup_code AS NVARCHAR(8));
         END;
		 
		 
	
     SET @sql_exists = 'DECLARE @rowCount INT;SELECT @rowCount = 1 FROM [TEKKEN].[dbo].[' + @tableName + '] WHERE 1=1 ' + @sql_Clause;
	 --PRINT(@sql_exists);
     EXEC (@sql_exists);
     
	 
     IF @@ROWCOUNT > 0
         BEGIN
             SET @sql = 'SELECT ' + @sql_column + ' 
						MAX([number]) + 1 number
					  FROM   [TEKKEN].[dbo].[' + @tableName + '] WHERE 1=1 ' + @sql_Clause;

             EXEC (@sql);
         END;
         ELSE
         BEGIN
             SET @sql = 'SELECT ' + @sql_column + ' 1 number';
             EXEC (@sql);
         END;

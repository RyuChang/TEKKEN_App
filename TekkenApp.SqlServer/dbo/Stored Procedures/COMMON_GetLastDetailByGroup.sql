

-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[COMMON_GetLastDetailByGroup] @tableName       VARCHAR(MAX), 
                                                    @stateGroup_code INT          = NULL
AS
     SET NOCOUNT ON;
     DECLARE @sql_exists VARCHAR(MAX), @sql_stateGroup VARCHAR(MAX), @sql_stateGroupClause VARCHAR(MAX), @sql VARCHAR(MAX), @rowCount INT, @maxNumber TINYINT;

     SET @sql_stateGroup = '';
     SET @sql_stateGroupClause = '';

     IF(@stateGroup_code IS NOT NULL)
         BEGIN
             SET @sql_stateGroup+='''' + CAST(@stateGroup_code AS NVARCHAR(8)) + ''' character_code, ';
             SET @sql_stateGroupClause+='WHERE  stateGroup_code = ' + CAST(@stateGroup_code AS NVARCHAR(8)) + ' GROUP BY stateGroup_code';
         END;


     SET @sql_exists = '
	 DECLARE @rowCount int
	 SELECT TOP(1) @rowCount=1
						FROM [TEKKEN].[dbo].[' + @tableName + '] ' + @sql_stateGroupClause;

     EXEC (@sql_exists);


     SET @rowCount = @@ROWCOUNT;
     IF @rowCount > 0

         BEGIN
             SET @sql = 'SELECT ' + @sql_stateGroup + ' 
						MAX([number]) + 1 number
					  FROM   [TEKKEN].[dbo].[' + @tableName + '] ' + @sql_stateGroupClause;
             EXEC (@sql);
         END;
         ELSE
         BEGIN
             SET @sql = 'SELECT ' + @sql_stateGroup + ' 1 number';
             EXEC (@sql);
         END;
		 
		

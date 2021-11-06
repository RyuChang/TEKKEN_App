



-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[Base_GetSelectList] @tableName     NVARCHAR(MAX), 
                                           @subTableName  VARCHAR(MAX)  = 'NONE', 
                                           @language_code CHAR(2)       = '  '
AS
     SET NOCOUNT ON;

     DECLARE @sql NVARCHAR(MAX);
     DECLARE @ParmDefinition NVARCHAR(MAX);


     IF(@subTableName = 'NONE')
         BEGIN
             SET @subTableName = @tableName;
         END;
         ELSE
         BEGIN
             SET @subTableName = @subTableName;
         END;



     SET @sql = N' SELECT CODE, NAME
            FROM   [TEKKEN].[dbo].[' + @tableName + '] data
					  LEFT OUTER JOIN [TEKKEN].[dbo].[' + @subTableName + '_name] name
					   ON data.code = name.' + @subTableName + '_code         
            WHERE name.language_code= @language_code
			ORDER BY number';

     SET @ParmDefinition = '@language_code			CHAR(2)';

     EXEC SP_EXECUTESQL 
          @sql, 
          @ParmDefinition, 
          @language_code = @language_code;
	

    

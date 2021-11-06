
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[TranslateName_GetTranslateNameByCode] @tableName     VARCHAR(MAX), 
                                                             @subTableName  VARCHAR(MAX) = 'NONE', 
                                                             @code          INT          = 0, 
                                                             @language_code CHAR(2)      = '  '
AS
     SET NOCOUNT ON;
     DECLARE @sql NVARCHAR(MAX);
	 
     --DECLARE @subTableName NVARCHAR(MAX)=@tableName;

     IF(@subTableName = 'NONE')
         BEGIN
             SET @subTableName = @tableName;
         END;
         ELSE
         BEGIN
             SET @subTableName = @subTableName;
         END;

     SET @sql = 'SELECT ' + CAST(@code AS CHAR) + ' AS Code,
						ISNULL(name.id,'''') Id, 
						LANGUAGE.code Language_Code, 
						ISNULL(name.name,'''') Name,
						ISNULL(checked,0) checked
				 FROM (SELECT code,number FROM LANGUAGE) LANGUAGE
				 	  LEFT OUTER JOIN [TEKKEN].[dbo].[' + @subTableName + '_name] name
					  ON (name.language_code = LANGUAGE.code) AND name.' + @subTableName + '_code = ' + CAST(@code AS CHAR) + '
					  --WHERE ( 0 =' + CAST(@code AS CHAR) + 'OR DATA.code=' + CAST(@code AS CHAR) + ') AND (''' + @language_code + '''=''  '' OR LANGUAGE.CODE=''' + @language_code + ''')
	 ORDER BY CODE, LANGUAGE.number';
	 
     --SELECT @sql;
     EXEC (@sql);


 /*
     SET @sql = 'SELECT ISNULL(name.id,'''')Id, 
						data.code Code, 
					    LANGUAGE.code Language_Code, 
						description Description, 
					    ISNULL(name.name,'''') Name,
						ISNULL(checked,0) checked
				 FROM (SELECT code,number FROM LANGUAGE) LANGUAGE
				 CROSS JOIN [TEKKEN].[dbo].[' + @tableName + '] data
					  LEFT OUTER JOIN [TEKKEN].[dbo].[' + @subTableName + '_name] name
					  ON data.code = name.' + @subTableName + '_code         AND (name.language_code = LANGUAGE.code)
     WHERE ( 0 =' + CAST(@code AS CHAR) + 'OR DATA.code=' + CAST(@code AS CHAR) + ') AND (''' + @language_code + '''=''  '' OR LANGUAGE.CODE=''' + @language_code + ''')
	 ORDER BY CODE, LANGUAGE.number';
	 */
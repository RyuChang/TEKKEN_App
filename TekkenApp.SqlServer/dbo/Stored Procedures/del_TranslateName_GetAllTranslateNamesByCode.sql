



-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[del_TranslateName_GetAllTranslateNamesByCode] @tableName VARCHAR(MAX), 
                                                                 @code      INT          = 0
AS
     SET NOCOUNT ON;
     DECLARE @sql NVARCHAR(MAX);
     DECLARE @nameTable VARCHAR(MAX)= '[TEKKEN].[dbo].[' + @tableName + '_name]';
     DECLARE @codeColumn VARCHAR(MAX)= @tableName + '_code';

    BEGIN
        SET @sql = 'SELECT nameTable.id id, 
						   language.code Language_Code, 
						   nameTable.[name] [Name],
						   checked
					FROM   LANGUAGE
					LEFT OUTER JOIN ' + @nameTable + ' nameTable 
						ON  nameTable.' + @codeColumn + ' = ' + CAST(@code AS CHAR) + ' AND nameTable.language_code = LANGUAGE.code
					ORDER BY LANGUAGE.number';

        --select @sql
        EXEC (@sql);

    END;


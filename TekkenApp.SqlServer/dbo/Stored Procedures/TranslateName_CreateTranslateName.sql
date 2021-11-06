

-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[TranslateName_CreateTranslateName] @tableName     VARCHAR(MAX), 
                                                          @code          INT, 
                                                          @name          VARCHAR(MAX), 
                                                          @language_code CHAR(2)
AS
     SET NOCOUNT ON;
     DECLARE @nameTable VARCHAR(MAX)= '[TEKKEN].[dbo].[' + @tableName + '_name] ';
     DECLARE @codeColumn VARCHAR(MAX)= @tableName + '_code';
     DECLARE @sql NVARCHAR(MAX);

     SET @sql = 'INSERT INTO ' + @nameTable + '
							(' + @codeColumn + ' , 
							  language_code, 
							  name, 
							  Checked
							 )
							 VALUES
							 ('+CAST(@code AS char)+' , 
							  '''+@language_code+''', 
							  '''+@name+''', 
							  0
							 )';

     EXEC (@sql);
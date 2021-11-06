
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[TranslateName_UpdateTranslateName] @tableName VARCHAR(MAX), 
                                                          @id        INT, 
                                                          @name      VARCHAR(MAX) = ''
AS
     SET NOCOUNT ON;
     DECLARE @nameTable VARCHAR(MAX)= '[TEKKEN].[dbo].[' + @tableName + '_name]';
     --DECLARE @codeColumn VARCHAR(MAX)= @tableName + '_code';
     DECLARE @sql NVARCHAR(MAX);
     SET @sql = 'UPDATE ' + @nameTable + ' SET name = ''' + @name + ''' ,
										       Checked = 1
				 WHERE ID = ' + CAST(@id AS CHAR);

     EXEC (@sql);
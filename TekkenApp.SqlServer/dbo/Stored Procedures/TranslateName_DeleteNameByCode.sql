

-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[TranslateName_DeleteNameByCode] @tableName VARCHAR(MAX), 
                                                       @code      INT
AS
     SET NOCOUNT ON;
     DECLARE @nameTable VARCHAR(MAX)= '[TEKKEN].[dbo].[' + @tableName + '_name]';

     DECLARE @sql NVARCHAR(MAX);
     DECLARE @ParmDefinition NVARCHAR(MAX);

     SET @sql = 'DELETE FROM ' + @nameTable + ' WHERE ' + @tableName + '_code = @code';
     SET @ParmDefinition = '@code			INT';

     EXEC sp_executesql 
          @sql, 
          @ParmDefinition, 
          @code = @code;

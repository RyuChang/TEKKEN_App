

-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[Base_GetDetailById] @tableName     NVARCHAR(MAX), 
                                           @id            INT, 
                                           @language_code NCHAR(2), 
                                           @BaseType      NVARCHAR(MAX)
AS
     SET NOCOUNT ON;

     DECLARE @sql NVARCHAR(MAX);
     DECLARE @ParmDefinition NVARCHAR(MAX);
     DECLARE @sql_column NVARCHAR(MAX)= '';

     IF(@BaseType = 'Character_Code' OR @BaseType = 'StateGroup_Code')
         BEGIN
             SET @sql_column+=@BaseType + ',';
         END;

     SET @ParmDefinition = '@id					INT,
						    @language_code		NVARCHAR(MAX)';

     SET @sql = N'SELECT data.id AS ID, 
						 code, 
						 ' + @sql_column + '
						 number, 
						 name.name AS NAME, 
						 description
				   FROM [TEKKEN].[dbo].[' + @tableName + '] data
						 LEFT OUTER JOIN [TEKKEN].[dbo].[' + @tableName + '_name] name ON data.code = name.' + @tableName + '_code
																				  AND (name.language_code = @language_code)
																				  WHERE  data.id = @id;';

     EXEC SP_EXECUTESQL 
          @sql, 
          @ParmDefinition, 
          @language_code = @language_code, 
          @id = @id;
	

    

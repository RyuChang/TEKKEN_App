




-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[Base_GetSelectListByCharacter] @tableName      NVARCHAR(MAX), 
                                                      @character_code INT           = 0, 
                                                      @language_code  CHAR(2)       = '  '
AS
     SET NOCOUNT ON;

     DECLARE @sql NVARCHAR(MAX);
     DECLARE @ParmDefinition NVARCHAR(MAX);

     SET @sql = N' SELECT CODE, NAME
            FROM   [TEKKEN].[dbo].[' + @tableName + '] data
					  LEFT OUTER JOIN [TEKKEN].[dbo].[' + @tableName + '_name] name
					   ON data.code = name.' + @tableName + '_code         
            WHERE name.language_code= @language_code AND (character_code = 0 OR character_code = @character_code)
			ORDER BY number';

     SET @ParmDefinition = '@language_code			CHAR(2),
							@character_code         INT';

     EXEC SP_EXECUTESQL 
          @sql, 
          @ParmDefinition, 
          @language_code = @language_code, 
          @character_code = @character_code;
    

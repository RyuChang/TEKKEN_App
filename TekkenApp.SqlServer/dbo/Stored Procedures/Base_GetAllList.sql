


-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[Base_GetAllList] @tableName NVARCHAR(MAX), 
                                        @code      INT           = 0, 
                                        @BaseType  NVARCHAR(MAX)
AS
     SET NOCOUNT ON;

     DECLARE @sql NVARCHAR(MAX);
     DECLARE @ParmDefinition NVARCHAR(MAX)= '@code    INT';

     DECLARE @sql_Clause NVARCHAR(MAX)= 'WHERE @code = 0';


     IF(@BaseType = 'Character_Code')
         BEGIN
             SET @sql_Clause = ' WHERE  (@code = 0 OR character_code = @code)';

         END;
     IF(@BaseType = 'StateGroup_Code')
         BEGIN
             SET @sql_Clause = ' WHERE  (@code = 0 OR StateGroup_code = @code)';
         END;



     SET @sql = N' SELECT *
            FROM   [TEKKEN].[dbo].[' + @tableName + '] 
            ' + @sql_Clause + '
            ORDER BY number';

     EXEC SP_EXECUTESQL 
          @sql, 
          @ParmDefinition, 
          @code = @code;
	

    

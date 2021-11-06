


-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[GetCodeById] @tableName NVARCHAR(MAX), 
                                    @id        INT 
AS
     SET NOCOUNT ON;
     DECLARE @sql NVARCHAR(MAX);
     DECLARE @ParmDefinition NVARCHAR(MAX);
     DECLARE @code      INT 


    BEGIN

        SET @sql = N'
				SELECT @code = code
				  FROM ' + @tableName + '
				 WHERE id = @id';
        
        SET @ParmDefinition = N'@id			INT,
							    @code		INT OUTPUT';

        EXEC sp_executesql 
             @sql, 
             @ParmDefinition, 
             @id = @id, 
             @code = @code OUTPUT;

			 return @code;

    END;


-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================

CREATE PROCEDURE [dbo].[Base_Create_None] @tableName   NVARCHAR(MAX), 
                                            @code        INT, 
                                            @number      TINYINT, 
                                            @description VARCHAR(MAX)
AS
     DECLARE @sql NVARCHAR(MAX);
     DECLARE @ParmDefinition NVARCHAR(MAX);

     SET @sql = N'INSERT INTO ' + @tableName + '
							 (code, 
							  number, 
							  description
							 )
							 VALUES
							 (@code, 
							  @number, 
							  @description
							 );';

     SET @ParmDefinition = '@code		INT,
						    @number		TINYINT,
						    @description NVARCHAR(MAX)';

     EXEC SP_EXECUTESQL 
          @sql, 
          @ParmDefinition, 
          @code = @code, 
          @number = @number, 
          @description = @description;
	         
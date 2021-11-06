
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================

CREATE PROCEDURE [dbo].[Base_Create_Character] @tableName      NVARCHAR(MAX), 
												 @code        INT, 
                                                 @character_code TINYINT, 
                                                 @number         TINYINT, 
                                                 @description    NVARCHAR(MAX)
AS
     DECLARE @err INT;

     SET @err = 0;
	 
     DECLARE @sql NVARCHAR(MAX);
     DECLARE @ParmDefinition NVARCHAR(MAX);
	 
     SET @ParmDefinition = '@code		INT,
						    @number		TINYINT,
						    @description NVARCHAR(MAX)';


     SET @sql = N'INSERT INTO ' + @tableName + '
							 (code, 
							  character_code, 
							  number, 
							  description
							 )
							 VALUES
							 (@code, 
							  @character_code, 
							  @number, 
							  @description
							 );';

     SET @ParmDefinition = '@code		INT,
							@character_code INT,
						    @number		TINYINT,
						    @description NVARCHAR(MAX)';


     EXEC SP_EXECUTESQL 
          @sql, 
          @ParmDefinition, 
          @code = @code, 
          @character_code = @character_code, 
          @number = @number, 
          @description = @description;
	         
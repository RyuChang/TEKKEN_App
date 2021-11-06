
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[Base_Update] @tableName       NVARCHAR(MAX), 
                                    @id              INT, 
                                    @character_code  TINYINT       = 0, 
                                    @stategroup_code INT       = 0, 
                                    @number          TINYINT, 
                                    @description     NVARCHAR(MAX)
AS
     SET NOCOUNT ON;
     DECLARE @sql NVARCHAR(MAX);
     DECLARE @ParmDefinition NVARCHAR(MAX);
     DECLARE @err INT, @code INT;

     SET @err = 0;

     EXEC @code = [dbo].[GetCreateCode] 
          @character_code = @character_code, 
		  @stategroup_code = @stategroup_code, 
          @tableName = @tableName, 
          @number = @number;



     SET @sql = N'UPDATE ' + @tableName + '
			        SET code = @code, 
						number = @number, 
						description = @description
				  WHERE ID = @id';

     SET @ParmDefinition = '@id			INT,
						   @code		INT,
						   @number		TINYINT,
						   @description NVARCHAR(MAX)';



     EXEC sp_executesql 
          @sql, 
          @ParmDefinition, 
          @code = @code, 
          @number = @number, 
          @description = @description, 
          @id = @id;
  
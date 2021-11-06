

-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================

CREATE PROCEDURE [dbo].[Base_Create_StateGroup] @tableName       NVARCHAR(MAX), 
                                                @code            INT, 
                                                @stateGroup_code INT, 
                                                @number          TINYINT, 
                                                @description     VARCHAR(MAX)
AS
     DECLARE @err INT;

     SET @err = 0;

     DECLARE @sql NVARCHAR(MAX);
     DECLARE @ParmDefinition NVARCHAR(MAX);

     SET @sql = N'INSERT INTO ' + @tableName + '
							 (code, 
							  stateGroup_code, 
							  number, 
							  description
							 )
							 VALUES
							 (@code, 
							  @stateGroup_code, 
							  @number, 
							  @description
							 );';

     SET @ParmDefinition = '@code				INT,
							@stateGroup_code	INT,
						    @number				TINYINT,
						    @description		NVARCHAR(MAX)';


     EXEC SP_EXECUTESQL 
          @sql, 
          @ParmDefinition, 
          @code = @code, 
          @stateGroup_code = @stateGroup_code, 
          @number = @number, 
          @description = @description;
	         

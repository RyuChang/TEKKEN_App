

-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[TranslateName_Merge] @tableName     NVARCHAR(MAX), 
                                            @subTableName  NVARCHAR(MAX)  = 'NONE', 
                                            @code          INT, 
                                            @language_code CHAR(2), 
                                            @name          NVARCHAR(MAX)
AS
     SET NOCOUNT ON;
     DECLARE @targetTable NVARCHAR(MAX);

     IF(@subTableName = 'NONE')
         BEGIN
             SET @targetTable = @tableName;
         END;
         ELSE
         BEGIN
             SET @targetTable = @subTableName;
         END;

     DECLARE @codeColumn NVARCHAR(MAX)= @targetTable + '_code';
     DECLARE @nameTable NVARCHAR(MAX)= '[TEKKEN].[dbo].[' + @targetTable + '_name]';
     DECLARE @sql NVARCHAR(MAX);

     DECLARE @err INT;

     SET @err = 0;
     BEGIN TRAN;


     SET @err = @@ERROR;
     IF @err <> 0
         BEGIN
             ROLLBACK TRAN;
             RETURN;
         END;
     --SELECT @Move_Code;


    BEGIN
        SET @sql = 'MERGE ' + @nameTable + ' AS NAME
					USING
					(
						SELECT @code, @language_code
					) AS DATA(CODE,LANGUAGE_CODE)
					ON NAME.' + @codeColumn + ' = DATA.CODE AND 
					   NAME.LANGUAGE_CODE = DATA.LANGUAGE_CODE
						WHEN MATCHED
						THEN UPDATE SET ' + @codeColumn + '=@code, NAME=''' + @name + ''' 
						WHEN NOT MATCHED
						THEN
						INSERT 			(' + @codeColumn + ' , 
											language_code, 
											name, 
											Checked
											)
											VALUES
											(@code , 
											@language_code, 
											@name, 
											0
											);';
        DECLARE @ParmDefinition NVARCHAR(MAX);
        SET @ParmDefinition = '@code				INT,
								@language_code		NVARCHAR(MAX),
								@name				NVARCHAR(MAX)';

        SELECT @ParmDefinition;

        EXEC sp_executesql 
             @sql, 
             @ParmDefinition, 
             @code = @code, 
             @language_code = @language_code, 
             @name = @name;


        SET @err = @@ERROR;
        IF @err <> 0
            BEGIN
                ROLLBACK TRAN;
                RETURN;
            END;
    END;
     COMMIT TRAN;

	 
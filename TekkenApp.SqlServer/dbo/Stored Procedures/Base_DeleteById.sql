

-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[Base_DeleteById] @tableName NVARCHAR(MAX), 
                                        @id        INT
AS
     SET NOCOUNT ON;
     DECLARE @sql NVARCHAR(MAX), @code INT, @err INT;

     EXEC @code = [dbo].[GetCodeById] 
          @tableName = @tableName, 
          @id = @id;


     BEGIN TRAN;
     EXEC @code = [dbo].[TranslateName_DeleteNameByCode] 
          @tableName = @tableName, 
          @code = @code;

     SET @err = @@ERROR;
     IF @err <> 0
         BEGIN
             ROLLBACK TRAN;
             RETURN;
         END;
     DECLARE @ParmDefinition NVARCHAR(MAX);

     SET @sql = N'DELETE FROM [TEKKEN].[dbo].[' + @tableName + '] 
					    WHERE id=@id';

     SET @ParmDefinition = '@id			INT';

     EXEC sp_executesql 
          @sql, 
          @ParmDefinition, 
          @id = @id;

     SET @err = @@ERROR;
     IF @err <> 0
         BEGIN
             ROLLBACK TRAN;
             RETURN;
         END;
     COMMIT;


		

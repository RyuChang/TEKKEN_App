

-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================

CREATE PROCEDURE [dbo].[GetTableCode] @tableName VARCHAR(MAX)
AS
     SET NOCOUNT ON;
     DECLARE @tableCode INT;

    BEGIN
        SELECT @tableCode = CODE
        FROM   tableCode
        WHERE  tableName = @tableName;

        RETURN @tableCode;
    END;

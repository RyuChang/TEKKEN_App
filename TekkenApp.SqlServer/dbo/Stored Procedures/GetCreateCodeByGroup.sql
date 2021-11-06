

-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================

CREATE PROCEDURE [dbo].[GetCreateCodeByGroup] @tableName       VARCHAR(MAX), 
                                       @stateGroup_Code INT, 
                                       @number          TINYINT
AS
     SET NOCOUNT ON;
     DECLARE @tableCode INT;
     DECLARE @Code INT;

    BEGIN
        SELECT @tableCode = CODE
        FROM   tableCode
        WHERE  tableName = @tableName;

        SET @Code = @tableCode + ((@stateGroup_Code - 80000000)*1000) + @number;
        RETURN @Code;
    END;


-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================

CREATE PROCEDURE [dbo].[GetCreateCode] @tableName       VARCHAR(MAX), 
                                      @character_code  TINYINT      = 0, 
                                      @stateGroup_code INT          = 0, 
                                      @number          TINYINT
AS
     SET NOCOUNT ON;
     DECLARE @tableCode INT;
     DECLARE @Code INT;

    BEGIN
        SELECT @tableCode = CODE
        FROM   tableCode
        WHERE  tableName = @tableName;

        IF(@stateGroup_code > 0)
            BEGIN
                SET @stateGroup_code = ((@stateGroup_code - 80000000) * 1000);
            END;


        SET @Code = @tableCode + (@character_code * 1000) + @stateGroup_code + @number;
        RETURN @Code;
    END;
	
	                 
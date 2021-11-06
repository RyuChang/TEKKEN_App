
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================

CREATE PROCEDURE [dbo].[del_MoveSubType_GetLastDetailByCharacterCode] @character_code TINYINT
AS
     SET NOCOUNT ON;
    BEGIN
        DECLARE @characterNumber INT;
        DECLARE @maxNumber INT;
        DECLARE @maxCode INT;

        IF EXISTS
        (
            SELECT 1
            FROM   [TEKKEN].[dbo].[moveSubType]
            WHERE  character_code = @character_code
        )

            BEGIN
                SELECT @maxNumber = MAX([number]) + 1, 
                       @maxCode = MAX([code]) + 1
                FROM   [TEKKEN].[dbo].[moveSubType]
                WHERE  character_code = @character_code;
            END;
            ELSE
            BEGIN
                SET @maxNumber = 1;
                EXEC @maxCode = [dbo].[GetCode] 
                     @tableName = N'moveSubType', 
                     @character_code = @character_code,
					 @number=@maxNumber;
            END;

        SELECT @character_code AS character_code, 
               @maxCode AS code, 
               @maxNumber AS number;
    END;
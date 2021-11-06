


-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[Move_GetCodeByNumber] @character_code INT, 
                                                 @number         TINYINT
AS
     SET NOCOUNT ON;
    BEGIN
        SELECT CODE
        FROM   Move
        WHERE  character_code = @character_code
               AND number = @number;
    END;

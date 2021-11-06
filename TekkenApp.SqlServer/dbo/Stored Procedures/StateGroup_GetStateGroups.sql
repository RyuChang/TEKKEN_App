
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[StateGroup_GetStateGroups] @language_code   CHAR(2)
AS
     SET NOCOUNT ON;
    BEGIN
        SET NOCOUNT ON;
        SELECT StateGroup.id, 
               CODE, 
               description, 
               [name], 
               number
        FROM   [TEKKEN].[dbo].StateGroup StateGroup
               LEFT OUTER JOIN [StateGroup_name] name ON StateGroup.code = NAME.StateGroup_code
        WHERE  NAME.language_code = @language_code
        ORDER BY number;
    END;

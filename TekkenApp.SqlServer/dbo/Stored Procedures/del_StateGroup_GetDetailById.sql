
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[del_StateGroup_GetDetailById] @id INT, 
                                                              @language_code   CHAR(2)
AS
     SET NOCOUNT ON;
    BEGIN
        SELECT *
        FROM   StateGroup
        WHERE  id = @id;
    END;

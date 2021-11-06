
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[Version_RemoveVersion] @Version DECIMAL(4, 2)
AS
    BEGIN
        DELETE FROM [tekkenVersion]
        WHERE version = @Version;
    END;

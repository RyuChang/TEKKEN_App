-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[Version_Detail] @Version    DECIMAL(4, 2)
AS
     SET NOCOUNT ON;
    BEGIN
        SELECT *
        FROM tekkenVersion
        WHERE version = @version;
    END;
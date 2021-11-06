-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[Version_AddVersion] @Version    DECIMAL(4, 2), 
                                                 @Season     TINYINT, 
                                                 @UpdateDate DATE
AS
    BEGIN
        UPDATE [tekkenVersion]
          SET 
              version = @Version, 
              season = @Season, 
              updateDate = @UpdateDate
        WHERE version = @Version;
    END;
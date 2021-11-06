



-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[del_HitType_GetAllHitTypes] @language_code CHAR(2)
AS
     SET NOCOUNT ON;
    BEGIN

        SELECT hitType_name.id, 
               hittype.code, 
               hittype.description, 
               HITTYPE_NAME.language_code, 
               hitType_name.name, 
               hittype.number
        FROM   hitType
               LEFT OUTER JOIN HITTYPE_NAME ON hitType.code = hitType_name.hitType_code
               LEFT OUTER JOIN language ON hitType_name.language_code = language.code
        WHERE  (language_code = @language_code)
        ORDER BY HITTYPE.code, 
                 language.number;

    END;
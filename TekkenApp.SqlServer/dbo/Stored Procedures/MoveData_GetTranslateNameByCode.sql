



-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[MoveData_GetTranslateNameByCode] @code INT
AS
     SET NOCOUNT ON;

     SELECT language.code Language_Code, 
            ISNULL(Name.StartType_name, '') AS StartType_name, 
            ISNULL(Name.Guardtype_name, '') AS Guardtype_name, 
            ISNULL(Name.hitType_name, '') AS hitType_name, 
            ISNULL(name.counterType_name, '') AS counterType_name
     FROM
     (
         SELECT code, 
                number
         FROM   LANGUAGE
     ) LANGUAGE
     LEFT OUTER JOIN Move_Data_Name NAME ON name.language_code = language.code
                                            AND name.Move_Data_Code = @code
     ORDER BY LANGUAGE.number;





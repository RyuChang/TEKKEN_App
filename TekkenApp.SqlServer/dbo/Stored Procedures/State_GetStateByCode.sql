

-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[State_GetStateByCode] @code          INT, 
                                             @language_code CHAR(2) = 'en'
AS
     SET NOCOUNT ON;
    BEGIN
        SELECT [name]
        FROM   [TEKKEN].[dbo].State State
               LEFT OUTER JOIN [State_name] name ON State.code = NAME.State_code
                                                    AND (NAME.language_code = @language_code)
        WHERE  state.code = @code;
    END;

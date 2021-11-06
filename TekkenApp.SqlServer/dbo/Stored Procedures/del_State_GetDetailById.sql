
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[del_State_GetDetailById] @id            INT, 
                                            @language_code CHAR(2)
AS
     SET NOCOUNT ON;
    BEGIN
        SELECT State.id, 
               CODE, 
               number, 
               StateGroup_code, 
               dbo.GetStateGroup_nameByCode(StateGroup_code, @language_code) StateGroup_name, 
               description, 
               [name]
        FROM   [TEKKEN].[dbo].State State
               LEFT OUTER JOIN [State_name] name ON State.code = NAME.State_code
                                                    AND (NAME.language_code = @language_code)
        WHERE  state.id = @id
        ORDER BY number;
    END;
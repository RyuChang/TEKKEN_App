
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[del_State_GetAllState] @language_code   CHAR(2), 
                                          @stateGroup_code INT     = NULL
AS
     SET NOCOUNT ON;
    BEGIN
        SET NOCOUNT ON;
        SELECT state.ID, 
               CODE, 
               state.StateGroup_code stateGroup_code, 
               description, 
               name.[name], 
               group_name.name StateGroup_name, 
               number
        FROM   [TEKKEN].[dbo].State State
               LEFT OUTER JOIN [State_name] name ON State.code = NAME.State_code
               LEFT OUTER JOIN [StateGroup_name] group_name ON STATE.StateGroup_code = group_name.StateGroup_code
                                                               AND group_name.language_code = @language_code
        WHERE  NAME.language_code = @language_code
               AND (@stateGroup_code = 0
                    OR state.StateGroup_code = @stateGroup_code)
        ORDER BY CODE;
    END;
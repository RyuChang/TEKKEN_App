
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[Character_RemoveCharacter] @Id INT
AS
    BEGIN
        SET NOCOUNT ON;
        DELETE FROM [TEKKEN].[dbo].[character]
        WHERE id = @id;
    END;

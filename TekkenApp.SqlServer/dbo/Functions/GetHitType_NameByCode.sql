
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date, ,>
-- Description:	<Description, ,>
-- =============================================
CREATE FUNCTION [dbo].[GetHitType_NameByCode](@code          INT, 
                                             @language_code CHAR(2))
RETURNS VARCHAR(MAX)
AS
     BEGIN
         DECLARE @result VARCHAR(MAX);

         SELECT @result = [name]
         FROM   hitType_name
         WHERE  hitType_code = @code
                AND language_code = @language_code;

         RETURN(@result);
     END;

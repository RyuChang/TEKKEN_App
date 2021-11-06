-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date, ,>
-- Description:	<Description, ,>
-- =============================================
CREATE FUNCTION [dbo].[GetStateGroup_nameByCode](@code          INT, 
                                                @language_code CHAR(2))
RETURNS VARCHAR(MAX)
AS
     BEGIN
         -- Declare the return variable here
         DECLARE @stateGroup_name VARCHAR(MAX);

         -- Add the T-SQL statements to compute the return value here
         SELECT @stateGroup_name = [name]
         FROM   StateGroup_name
         WHERE  StateGroup_code = @code
                AND language_code = @language_code;

         -- Return the result of the function
         RETURN(@stateGroup_name);
     END;

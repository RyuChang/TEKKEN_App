-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE TEST1
	-- Add the parameters for the stored procedure here
	
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
		SELECT NAME, NUM, VER
		FROM (SELECT NAME,NUM,VER,RANK()OVER(PARTITION BY NAME ORDER BY VER DESC) AS GROUP1
		FROM TEST
		WHERE VER<=3) AS A 
		WHERE GROUP1=1
END

-- ================================================
-- Template generated from Template Explorer using:
-- Create Procedure (New Menu).SQL
--
-- Use the Specify Values for Template Parameters 
-- command (Ctrl-Shift-M) to fill in the parameter 
-- values below.
--
-- This block of comments will not be included in
-- the definition of the procedure.
-- ================================================
CREATE PROCEDURE [dbo].[next_number]
	-- Add the parameters for the stored procedure here
	@TableName nvarChar(30),
	@NewVal bigint OUTPUT
AS
BEGIN TRANSACTION
	DECLARE @strQuery nvarChar(300)

	--Advance key value of table by 1--
	SET @strQuery = 'UPDATE ' + @TableName + ' SET NextAvailableNumber  = NextAvailableNumber + 1;'
	EXEC(@strQuery)
	
	--retrieve new key value--
	SET @strQuery = 'SELECT NextAvailableNumber FROM ' + @TableName + ';'
	
	--create temporary table to store results of above query--
	CREATE TABLE #Data (var int)
	
	--place results of above query into new table--
	INSERT #Data exec (@strQuery)
	--populate reference(output) argument with results from query--
	SELECT @NewVal = var from #Data
	--no longer need temporary table--
	DROP TABLE #Data
	COMMIT
RETURN

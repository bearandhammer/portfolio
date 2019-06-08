-- Extract and run as a raw script or encapsulate in a stored procedure (I'm wrapping this up as an SP for completeness)
IF (OBJECT_ID('dbo.GetColumnLastSeedValues', 'P') IS NOT NULL)
BEGIN

	DROP PROCEDURE dbo.GetColumnLastSeedValues;

END;
GO

CREATE PROCEDURE dbo.GetColumnLastSeedValues
AS
BEGIN

	-- SET NOCOUNT ON added to prevent extra result sets from interfering with SELECT statements.
	SET NOCOUNT ON;

	-- A first stab at some operational monitoring. Grab all of the columns that support 'seeding' and, based on type, look at the last seed value recorded against
	-- the maximum supported value (could expand this, use it for reporting, etc.)
	SELECT
	st.[name] AS [TableName]
	, ic.[Name] AS [SeedColumnName]
	, stype.system_type_id AS [DataTypeId]
	, stype.[name] AS [DataTypeName]
	, 
	CASE
		WHEN stype.system_type_id = 56
		THEN '2147483647'
		WHEN stype.system_type_id = 127
		THEN '9223372036854775807'
		ELSE 'Unknown'
	END AS [MaximumAllowedPositiveValue]
	, COALESCE(CONVERT(NVARCHAR(20), ic.last_value), 'Last Value Unset') AS [LastSeedValue]
	, 
	CASE
		WHEN COALESCE(CONVERT(NVARCHAR(20), ic.last_value), '') = ''
		THEN 'Cannot Calculate'
		WHEN stype.system_type_id = 56
		THEN CONVERT(NVARCHAR(20), (2147483647 - CAST(ic.last_value AS INT)))
		WHEN stype.system_type_id = 127
		THEN CONVERT(NVARCHAR(20), (9223372036854775807 - CAST(ic.last_value AS BIGINT)))
		ELSE 'Unknown'
	END AS [CurrentDifferenceBetweenSeedAndMax]
	FROM sys.identity_columns ic
		INNER JOIN sys.tables st ON ic.object_id = st.object_id 
		INNER JOIN sys.types stype ON ic.system_type_id = stype.system_type_id
	ORDER by st.[Name];

	-- Return successful execution status by default
	RETURN 0;

END;
GO

-- Example usage
EXEC dbo.GetColumnLastSeedValues;
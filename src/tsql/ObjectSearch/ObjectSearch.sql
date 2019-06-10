-- Extract and run as a raw script or encapsulate in a stored procedure (I'm wrapping this up as an SP for completeness)
IF (OBJECT_ID('dbo.ObjectSearch', 'P') IS NOT NULL)
BEGIN

	DROP PROCEDURE dbo.ObjectSearch;

END;
GO

CREATE PROCEDURE dbo.ObjectSearch
(
	@SEARCH_TERM			NVARCHAR(MAX)
)
AS
BEGIN

	-- SET NOCOUNT ON added to prevent extra result sets from interfering with SELECT statements.
	SET NOCOUNT ON;
 
	--1) Validation - Ensure that a valid Search Term has been supplied (cannot be NULL or an empty string)
	IF (COALESCE(@SEARCH_TERM, '') = '')
	BEGIN
      
		  RAISERROR('@SEARCH_TERM cannot be an empty string or NULL. Please specify a valid value (procedure: ObjectSearch).', 16, 1);
		  RETURN;       --Input invalid, simply return
 
	END;
 
	--Enable a 'fuzzy' match
	SET
	@SEARCH_TERM = '%' + @SEARCH_TERM + '%';
 
	--CTE that is used to 'bundle' up all of the various objects being search (for the given Search Term)
	WITH COMBINED_OBJECTS
	(
		  OBJ_ID
		  , OBJ_NAME
		  , OBJ_TYPE
		  , OBJ_DEFINITION
	)
	AS
	(
		  --Search Procedures, Functions, Triggers and Views for the Search Term (in the actual definition of the object)
		  SELECT
		  sm.object_id AS [OBJ_ID]
		  , so.name AS [OBJ_NAME]
		  , so.type_desc AS [OBJ_TYPE]
		  , sm.definition AS [OBJ_DEFINITION]
		  FROM sys.sql_modules sm
				 INNER JOIN sys.objects so ON sm.object_id = so.object_id
		  WHERE sm.definition LIKE @SEARCH_TERM
		  UNION ALL
		  --Search for the Search Term in the name of an index
		  SELECT
		  si.object_id AS [OBJ_ID]
		  , si.name AS [OBJ_NAME]
		  , 'INDEX - ' + si.type_desc COLLATE DATABASE_DEFAULT AS [OBJ_TYPE]                                                                 --Negate collation issues with concatenation                                                       
		  , NULL AS [OBJ_DEFINITION]
		  FROM sys.indexes si
		  WHERE si.name LIKE @SEARCH_TERM
		  UNION ALL
		  --Search for the Search Term in the physical column names that comprise an index definition
		  SELECT
		  si.object_id AS [OBJ_ID]
		  , sc.name + ' (' + si.name COLLATE DATABASE_DEFAULT + ' - ' + si.type_desc COLLATE DATABASE_DEFAULT + ')' AS [OBJ_NAME]            --Negate collation issues with concatenation
		  , 'INDEX_COLUMN' AS [OBJ_TYPE]
		  , NULL AS [OBJ_DEFINITION]
		  FROM sys.indexes si
				 INNER JOIN sys.index_columns sic ON
				 (
					   si.object_id = sic.object_id
					   AND si.index_id = sic.index_id
				 )
				 INNER JOIN sys.columns sc ON
				 (
					   sic.object_id = sc.object_id
					   AND sic.column_id = sc.column_id
				 )
		  WHERE sc.name LIKE @SEARCH_TERM
	)
	--Return the results to the caller (can be expanded as needed)
	SELECT
	co.OBJ_ID
	, co.OBJ_NAME
	, co.OBJ_TYPE
	, co.OBJ_DEFINITION
	FROM COMBINED_OBJECTS co
	ORDER BY co.OBJ_TYPE, co.OBJ_NAME;      --Do a little bit of ordering to make the results easier to digest

	-- Return successful execution status by default
	RETURN 0;

END;
GO

-- Example usage
EXEC dbo.ObjectSearch
@SEARCH_TERM = 'Perk';
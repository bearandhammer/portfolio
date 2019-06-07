IF (OBJECT_ID('[dbo].[GetCachedExecutionPlanDetails]', 'P') IS NOT NULL)
BEGIN

	DROP PROCEDURE [dbo].[GetCachedExecutionPlanDetails];

END;
GO

CREATE PROCEDURE dbo.GetCachedExecutionPlanDetails
AS
BEGIN

	-- SET NOCOUNT ON added to prevent extra result sets from interfering with SELECT statements.
	SET NOCOUNT ON;

	SELECT
	COALESCE(OBJECT_NAME(st.objectid, st.[dbid]), '[No Object]') AS [ObjectName]
	, cp.objtype AS [ObjectType]
	, cp.usecounts AS [TimesExecuted]
	, DB_NAME(qp.[dbid]) AS [DatabaseName]
	, cp.size_in_bytes AS [SizeInBytes]
	, cp.cacheobjtype AS [CacheObjectType]
	, st.[text] AS [PhysicalQuery]
	, qp.query_plan AS [QueryPlanXml]
	, qs.last_execution_time AS [LastRan]
	, qs.last_elapsed_time AS [LastRunElapsedTime]
	FROM sys.dm_exec_cached_plans cp
		CROSS APPLY sys.dm_exec_query_plan(cp.plan_handle) qp
		CROSS APPLY sys.dm_exec_sql_text(cp.plan_handle) st
		-- Extra join added for optional execution stats (like the last 'run' time and execution times)
		LEFT OUTER JOIN sys.dm_exec_query_stats qs ON cp.plan_handle = qs.plan_handle
	-- Omit this is you want results for all databases contained on an instance
	WHERE DB_NAME(qp.[dbid]) = 'JsonForTestDatabase'
	-- Place the most 'used' first within the results set
	ORDER BY cp.usecounts DESC;

	-- Return successful execution status by default
	RETURN 0;

END;
GO

-- Example usage
EXEC dbo.GetCachedExecutionPlanDetails;
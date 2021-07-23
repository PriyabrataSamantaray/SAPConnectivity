	---- create dummy view if it doesnt exist
	if not exists (select * from sysobjects where id = object_id('dbo.vwFiscalWeeks'))
	begin
		exec ('CREATE view dbo.vwFiscalWeeks as select 0 as x')
	end
	GO
	
	ALTER VIEW vwFiscalWeeks
	
	AS

	SELECT 
		FiscalYear,
		FiscalWeek,
		MIN(FiscalWkBegin) AS FiscalWkBegin,
		MAX(FiscalWkEnd) AS FiscalWkEnd,
		CAST(MIN(FiscalPeriod) AS TINYINT) AS FiscalPeriod,
		MIN(FiscalQuarter) AS FiscalQuarter,
		CONVERT(CHAR(8),MAX(FiscalWkEnd),112) AS eDate
		
	FROM
		dbo.vwFiscalCalBase		
		
	GROUP BY
		FiscalYear,
		FiscalWeek
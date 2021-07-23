	---- create dummy view if it doesnt exist
	if not exists (select * from sysobjects where id = object_id('dbo.vwFiscalPeriods'))
	begin
		exec ('CREATE view dbo.vwFiscalPeriods as select 0 as x')
	end
	GO
	
	ALTER VIEW vwFiscalPeriods
	
	AS
	
	SELECT 
		B.*, 
		W.PeriodWeeks,
		Y.BeginDate AS FiscalYrBegin,
		Y.EndDate AS FiscalYrEnd,
		Q.BeginDate AS FiscalQtrBegin,
		Q.EndDate AS FiscalQtrEnd
		
		FROM
	
	(	-- BASE QUERY INFO
		SELECT 
			FiscalYear,
			FiscalPeriod,
			MIN(FiscalPerBegin) AS FiscalPerBegin,
			MAX(FiscalPerEnd) AS FiscalPerEnd,
			MIN(FiscalQuarter) AS FiscalQuarter,
			MONTH(DATEADD(d, 15 ,MIN(FiscalPerBegin))) AS CalendarMonth,
			COUNT(CalDate) AS PeriodDays
			
		FROM
			dbo.vwFiscalCalBase 
					
		GROUP BY
			FiscalYear,
			FiscalPeriod
			
	) AS B
	
	LEFT JOIN	-- COUNT WEEKS IN THE PERIOD
	( 
		SELECT
			COUNT(FiscalWeek) AS PeriodWeeks,
			FiscalYear,
			FiscalPeriod

		FROM
			dbo.vwFiscalWeeks
			
		GROUP BY
			FiscalYear,
			FiscalPeriod
			
	) AS W
		ON ((B.FiscalYear = W.FiscalYear) AND(B.FiscalPeriod = W.FiscalPeriod))
		
	LEFT JOIN dbo.VWFiscalYears AS Y	-- GET YEAR BEGINNING AND ENDING DATES
		ON B.FiscalYear = Y.FiscalYear
		
	LEFT JOIN dbo.vwFiscalQuarters AS Q
		ON ((B.FiscalYear = Q.FiscalYear) AND(B.FiscalQuarter = Q.FiscalQuarter)) 
		
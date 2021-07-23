	---- create dummy view if it doesnt exist
	if not exists (select * from sysobjects where id = object_id('dbo.vwFiscalQuarters'))
	begin
		exec ('CREATE view dbo.vwFiscalQuarters as select 0 as x')
	end
	GO
	
	ALTER VIEW vwFiscalQuarters
	
	AS

	SELECT 
		FiscalYear,
		FiscalQuarter,
		MIN(CalDate) AS BeginDate,
		MAX(CalDate) AS EndDate,
		'FY' + RIGHT(FiscalYear , 2) + '-Q' + CAST(FiscalQuarter AS CHAR(1)) AS QuarterDesc,
		CONVERT(CHAR(8),MIN(CalDate), 112) AS BDate,
		CONVERT(CHAR(8),MAX(CalDate), 112) AS EDate,
		
		CASE
			WHEN FiscalQuarter = 1 THEN 'FY' + RIGHT(FiscalYear, 2)
			ELSE 'FY' + RIGHT(FiscalYear, 2) + ' Forecast ' +
				CASE
					WHEN FiscalQuarter = 2 THEN 'I'
					WHEN FiscalQuarter = 3 THEN 'II'
					WHEN FiscalQuarter = 4 THEN 'III'
				END
		END AS QRAMPlan
			
	FROM 
		vwFiscalCalBase
  
	GROUP BY
		FiscalYear,
		FiscalQuarter
		
	GO

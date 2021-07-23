	---- create dummy view if it doesnt exist
	if not exists (select * from sysobjects where id = object_id('dbo.vwFiscalYears'))
	begin
		exec ('CREATE view vwFiscalYears as select 0 as x')
	end
	GO
	
	ALTER VIEW vwFiscalYears
	
	AS

	SELECT 
		FiscalYear,
		MIN(CalDate) AS BeginDate,
		MAX(CalDate) AS EndDate
	
	FROM 
		vwFiscalCalBase
  
	GROUP BY
		FiscalYear
		
	GO


	
	---- create dummy view if it doesnt exist
	if not exists (select * from sysobjects where id = object_id('dbo.vwFiscalDays'))
	begin
		exec ('CREATE view vwFiscalDays as select 0 as x')
	end
	GO

	ALTER VIEW vwFiscalDays

	AS

	SELECT 
		CalDate,
		FiscalID,
		CAST(FiscalWeek AS int) AS FiscalWeek,
		CAST(FiscalPeriod AS int) AS FiscalPeriod,
		FiscalQuarter,
		FiscalYear
      
	FROM 
		dbo.vwFiscalCalBase
		
	GO



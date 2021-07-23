---- create dummy view if it doesnt exist
	if not exists (select * from sysobjects where id = object_id('dbo.vwFiscalCalBase'))
	begin
		exec ('CREATE view vwFiscalCalBase as select 0 as x')
	end
	GO
	
	ALTER VIEW vwFiscalCalBase
	
	AS
	
	Select
		CALENDAR_DAY AS CalDate,
		RIGHT(FISCAL_WEEK_NUMBER, 2) AS FiscalWeek,
		RIGHT(FISCAL_MONTH_NUMBER, 2) AS FiscalPeriod,
		FISCAL_QUARTER_NUMBER AS FiscalQuarter,
		FISCAL_YEAR_NUMBER AS FiscalYear,
		FISCAL_WEEK_START_DATE AS FiscalWkBegin,
		FISCAL_WEEK_END_DATE AS FiscalWkEnd,
		FISCAL_MONTH_START_DATE AS FiscalPerBegin,
		FISCAL_MONTH_END_DATE AS FiscalPerEnd,
		CALENDAR_MONTH_NUMBER AS CalendarMonth,
		
		CALENDAR_QUARTER_NUMBER AS CalendarQuarter,
		CALENDAR_YEAR_NUMBER AS CalendarYear,
		
		FISCAL_MONTH_NUMBER + RIGHT('0' + CAST(DAY(CALENDAR_DAY) AS varchar(2)), 2)  AS FiscalID


	FROM 
		[DCA-DB-230\MSBI_DM].[AGS_DATAMART].[dbo].[SM_DATE_DIM]
		
	WHERE
		[CALENDAR_DAY] IS NOT NULL


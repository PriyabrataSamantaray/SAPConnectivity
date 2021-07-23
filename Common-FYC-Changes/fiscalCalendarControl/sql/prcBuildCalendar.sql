USE [commonDB]
GO

/****** Object:  StoredProcedure [dbo].[prcBuildCalendar]    Script Date: 8/14/2014 10:14:00 AM ******/
DROP PROCEDURE [dbo].[prcBuildCalendar]
GO

/****** Object:  StoredProcedure [dbo].[prcBuildCalendar]    Script Date: 8/14/2014 10:14:00 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[prcBuildCalendar]

AS


if (select count(*) from t_cal where CALDATE >GETDATE()) = 0
	return



delete t_FiscalDays where CalDate > GETDATE()


insert t_FiscalDays
select * from t_cal where CALDATE >GETDATE()





-- Uses dbo.t_FiscalDays to build remaining tables in the calendar









	-- *********************************************************
	-- Build t_FiscalYears
	-- *********************************************************
	TRUNCATE TABLE dbo.t_FiscalYears
	
	INSERT INTO 
		dbo.t_FiscalYears
	(
		FiscalYear,
		BeginDate,
		EndDate
	)
		SELECT
			FiscalYear,
			MIN(CalDate) AS BeginDate,
			MAX(CalDate) AS EndDate
		FROM
			dbo.t_FiscalDays
		GROUP BY
			FiscalYear
		ORDER BY
			FiscalYear

			
	-- *********************************************************
	-- Build t_FiscalQuarters
	-- *********************************************************
	TRUNCATE TABLE dbo.t_FiscalQuarters
	
	INSERT INTO
		dbo.t_FiscalQuarters
	(
		FiscalYear,
		FiscalQuarter,
		BeginDate,
		EndDate,
		QuarterDesc,
		BDate,
		EDate,
		WorkingDays,
		QRAMPlan
	)
	
	SELECT
		FiscalYear,
		FiscalQuarter,
		MIN(CalDate) AS BeginDate,
		MAX(CalDate) AS EndDate,
		
		'FY' + RIGHT(FiscalYear,2) + '-Q' 
			+ CAST(FiscalQuarter AS char(1)) AS QuarterDesc,
			
		CAST(YEAR(MIN(CalDate)) AS char(4))
			+ RIGHT('0' + CAST(MONTH(MIN(CalDate)) AS varchar(2)), 2)
			+ RIGHT('0' + CAST(DAY(MIN(CalDate)) AS varchar(2)), 2) AS BDate,

		CAST(YEAR(MAX(CalDate)) AS char(4))
			+ RIGHT('0' + CAST(MONTH(MAX(CalDate)) AS varchar(2)), 2)
			+ RIGHT('0' + CAST(DAY(MAX(CalDate)) AS varchar(2)), 2) AS EDate,
		
		SUM(CAST(WorkingDay AS tinyint)) AS WorkingDays,
		
		'FY' + RIGHT(FiscalYear,2) + 
			CASE 
				WHEN FiscalQuarter = 2 THEN ' Forecast I'  
				WHEN FiscalQuarter = 3 THEN ' Forecast II'  
				WHEN FiscalQuarter = 4 THEN ' Forecast III' 
				ELSE ''
			END AS 'QRAM Plan'

	FROM
		dbo.t_FiscalDays	
	GROUP BY
		FiscalYear,
		FiscalQuarter,
		
		'FY' + RIGHT(FiscalYear,2) + '-Q' 
			+ CAST(FiscalQuarter AS char(1))

	ORDER BY	
		FiscalYear,
		FiscalQuarter		

		
	-- *********************************************************
	-- Build t_FiscalWeeks
	-- *********************************************************
	TRUNCATE TABLE dbo.t_FiscalWeeks
	
	INSERT INTO 
		dbo.t_FiscalWeeks
	(
		FiscalYear,
		FiscalQuarter,
		FiscalPeriod,
		FiscalWeek,
		FiscalWkBegin,
		FiscalWkEnd,
		eDate,
		WorkingDays
	)
	
	SELECT
		FiscalYear,
		FiscalQuarter,
		FiscalPeriod,
		FiscalWeek,
		MIN(CalDate) AS WeekBegin,
		MAX(CalDate) AS WeekEnd,
		CAST(YEAR(MAX(CalDate)) AS char(4))
			+ RIGHT('0' + CAST(MONTH(MAX(CalDate)) AS varchar(2)), 2)
			+ RIGHT('0' + CAST(DAY(MAX(CalDate)) AS varchar(2)), 2) AS EDate,
		SUM(CAST(WorkingDay AS tinyint)) AS WorkingDays
	FROM
		dbo.t_FiscalDays
	GROUP BY
		FiscalYear,
		FiscalWeek,
		FiscalPeriod,
		FiscalQuarter
		
	ORDER BY
		FiscalYear,
		FiscalQuarter,
		FiscalPeriod,
		FiscalWeek


	-- *********************************************************
	-- Build t_FiscalPeriods
	-- *********************************************************
	TRUNCATE TABLE dbo.t_FiscalPeriods
	
	INSERT INTO 
		dbo.t_FiscalPeriods
	(
		FiscalYear,
		FiscalQuarter,
		FiscalPeriod,
		FiscalPerBegin,
		FiscalPerEnd,
		FiscalQtrBegin,
		FiscalQtrEnd,
		FiscalYrBegin,
		FiscalYrEnd,
		CalendarMonth,
		PeriodWeeks,
		PeriodMfgDays,
		PeriodDays
	)
	
	SELECT
		D.FiscalYear,	
		D.FiscalQuarter,
		D.FiscalPeriod,
		MIN(CalDate) AS FiscalPerBegin,
		MAX(CalDate) AS FiscalPerEnd,
		Q.BeginDate AS FiscalQtrBegin,
		Q.EndDate AS FiscalQtrEnd,
		Y.BeginDate AS FiscalYrBegin,
		Y.EndDate AS FiscalYrEnd,
		MONTH(DATEADD(D,15,MIN(CalDate))) AS CalendarMonth,
		W.NoOfWeeks,
		SUM(CAST(WorkingDay AS tinyint))AS PerMfgDays,
		COUNT(D.CalDate) AS PeriodDays
	FROM
		dbo.t_FiscalDays AS D
		
	LEFT JOIN 
		dbo.t_FiscalQuarters AS Q
			ON (D.FiscalYear = Q.FiscalYear)
			AND (D.FiscalQuarter = Q.FiscalQuarter)
			
	LEFT JOIN
		dbo.t_FiscalYears AS Y
			ON Q.FiscalYear = Y.FiscalYear
			
	LEFT JOIN
		(
		SELECT
			FiscalYear,
			FiscalPeriod,
			COUNT(FiscalWeek) AS NoOfWeeks
		FROM
			dbo.t_FiscalWeeks
		GROUP BY
			FiscalYear,
			FiscalPeriod
		)
			AS W
				ON (D.FiscalYear = W.FiscalYear)
				AND (D.FiscalPeriod = W.FiscalPeriod)
			
	GROUP BY
		D.FiscalYear,	
		D.FiscalQuarter,
		D.FiscalPeriod,
		Q.BeginDate,
		Q.EndDate,
		Y.BeginDate,
		Y.EndDate,
		W.NoOfWeeks
		
	ORDER BY
		D.FiscalYear,	
		D.FiscalQuarter,
		D.FiscalPeriod;

GO



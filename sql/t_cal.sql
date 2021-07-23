IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'dbo.t_cal') AND type in (N'U'))
	DROP TABLE dbo.t_cal
GO
/*
Transient table used for populating t_fiscalDays.
All column headers are in capital to support the SAP RFC

*/

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE dbo.t_cal(
	CALDATE smalldatetime NOT NULL,
	FISCALID int NOT NULL,
	FISCALWEEK tinyint NOT NULL,
	FISCALPERIOD tinyint NOT NULL,
	FISCALQUARTER tinyint NOT NULL,
	FISCALYEAR smallint NOT NULL,
	WORKINGDAY bit NOT NULL
) 



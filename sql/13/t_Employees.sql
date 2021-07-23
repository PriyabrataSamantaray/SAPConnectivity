set nocount on
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'dbo.Employees') AND type in (N'U'))
	DROP TABLE dbo.Employees
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE dbo.Employees(
	Badge nvarchar(15) NOT NULL,
	DEPARTMENT nvarchar(10) NULL,
	FIRSTNAME nvarchar(20) NULL,
	LASTNAME nvarchar(20) NULL,
	MI nvarchar(10) NULL,
	Shift nvarchar(12) NULL,
	Person_Type nvarchar(30) NULL,
	EE_Group nvarchar(20) NULL,
	Sch_Hours smallint NULL,
	FTEs smallint NULL,
	Title nvarchar(40) NULL,
	Hire_Date datetime NULL,
	EMAIL nvarchar(30) NULL,
	Standard_ID nvarchar(20) NULL,
	Supv_Badge nvarchar(15) NULL,
	Supv_Name nvarchar(40) NULL,
	Supv_EMAIL nvarchar(30) NULL,
	Functional_Area nvarchar(30) NULL,
	Ext int NULL,
	WorkPhone nvarchar(20) NULL,
	active bit NOT NULL,
	termWeek nvarchar(8) NULL,
	varian_badge nvarchar(15) NULL,
	nickName nvarchar(60) NULL,
	payRate_type nvarchar(1) NULL,
	jobFamily1 nvarchar(75) NULL,
	jobFamily2 nvarchar(75) NULL,
	jobFamily3 nvarchar(75) NULL,
	contWorkerType nvarchar(50) NULL,
	cwJobCode nvarchar(6) NULL
 CONSTRAINT PK_Employees PRIMARY KEY CLUSTERED 
(
	Badge ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) 
) 
GO





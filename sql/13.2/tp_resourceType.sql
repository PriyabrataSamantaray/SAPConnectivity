USE [commonDB]
GO
DROP PROCEDURE [dbo].[sp_LoadPersonnelData]
GO
/****** Object:  UserDefinedTableType [dbo].[resourceType]    Script Date: 9/12/2018 7:32:24 AM ******/
DROP TYPE [dbo].[resourceType]
GO

/****** Object:  UserDefinedTableType [dbo].[resourceType]    Script Date: 9/12/2018 7:32:24 AM ******/
CREATE TYPE [dbo].[resourceType] AS TABLE(
	[employeeId] [nvarchar](15) NULL,
	[firstName] [nvarchar](20) NULL,
	[lastName] [nvarchar](20) NULL,
	[department] [nvarchar](10) NULL,
	[shift] [nvarchar](12) NULL,
	[status] [nvarchar](10) NULL,
	[ee_group] [nvarchar](20) NULL,
	[sch_hours] [int] NULL,
	[title] [nvarchar](40) NULL,
	[hireDate] [datetime] NULL,
	[email] [nvarchar](30) NULL,
	[supervisorId] [nvarchar](15) NULL,
	[phoneNumber] [nvarchar](20) NULL,
	[PREFERRED_NAME] [nvarchar](60) NULL,
	[PAY_RATE_TYPE] [nvarchar](1) NULL,
	[JOB_FAMILY_LEVEL1] [nvarchar](75) NULL,
	[JOB_FAMILY_LEVEL2] [nvarchar](75) NULL,
	[JOB_FAMILY_LEVEL3] [nvarchar](75) NULL,
	[CONTINGENT_WORKER_TYPE] [nvarchar](50) NULL,
	[CW_JOB_CODE] [nvarchar](6) NULL,
	[Cost_Center_Name] [nvarchar](75) NULL,
	[employee_type] [nvarchar](75) NULL,
	[location] [nvarchar](75) NULL
)
GO



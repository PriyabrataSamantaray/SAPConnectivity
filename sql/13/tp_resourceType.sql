set nocount on

IF  EXISTS (SELECT * FROM sys.types WHERE is_table_type = 1 AND name ='resourceType')
  BEGIN
	IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_LoadPersonnelData]') AND type in (N'P', N'PC'))
		DROP PROCEDURE [dbo].[sp_LoadPersonnelData]
	DROP TYPE [dbo].[resourceType]
END
GO

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
    [PAY_RATE_TYPE] [nvarchar] (1) NULL,
    [JOB_FAMILY_LEVEL1] [nvarchar] (75) NULL,
    [JOB_FAMILY_LEVEL2] [nvarchar] (75) NULL,
    [JOB_FAMILY_LEVEL3] [nvarchar] (75) NULL,
    [CONTINGENT_WORKER_TYPE] [nvarchar] (50) NULL,
    [CW_JOB_CODE] [nvarchar] (6) NULL


)
GO


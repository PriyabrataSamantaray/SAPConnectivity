
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_LoadPersonnelData]') AND type in (N'P', N'PC'))
	DROP PROCEDURE [dbo].[sp_LoadPersonnelData]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO
 




CREATE PROCEDURE [dbo].[sp_LoadPersonnelData]

	@data resourceType READONLY,	-- 1=Create temporary table | 2=Populate table and merge data
	@state int  -- state 1 = done truncate staging table and continue, 0 = file being sent in pieces plz continue
AS
BEGIN
	   declare @nowWeek as nvarchar(8)


		insert tempEmployees
		select * from @data

		if (@state = 1)
		begin

		/*
		Assumptions:
			Workday will pass all Active and leave resources daily
			If Workday messes up the data we can roll back the PRD data by "update resources set person_type = 'active' where term week is current week
			If Workday passes inactive resources that will be a problem because they wont get marked in-active in projectresourcemgmt DB... commonddb must only have the active roster
				we could send out email notes if commondb employee table has < x values or sends inactive resources to B2B
		*/




			if(select count(*) from tempEmployees) < 10000 --check for sufficient amount of data
				return

			truncate table Employees


			SELECT @nowWeek = cast(FiscalYear as nvarchar(4)) + '-' +
			cast(FiscalWeek as nvarchar(2)) FROM t_FiscalWeeks WHERE GETDATE() BETWEEN FiscalWkBegin AND FiscalWkEnd + '23:59:59'


			insert Employees (Badge,  FIRSTNAME, LastName, department, shift, active, ee_group, Sch_Hours, Title, Hire_Date, 
			Email,  supv_Badge, workphone, Person_Type, ftes, nickName, payRate_type, jobFamily1, jobFamily2, jobFamily3, contWorkerType, cwJobCode )
			select employeeId, firstName, lastName, department, shift, (case when status = 'A' then 1 else 0 end), case when ee_group = 'CW' then 'Contractor' else 'Employee' end, 
			case when sch_hours > 40 then 40 else sch_hours end, title, hireDate, email, supervisorId, phoneNumber, 
			(case when status = 'A' then 'Active' when status = 'L' then 'Leave' else 'inactive' end),1, PREFERRED_NAME, PAY_RATE_TYPE, JOB_FAMILY_LEVEL1,
			JOB_FAMILY_LEVEL2, JOB_FAMILY_LEVEL3, CONTINGENT_WORKER_TYPE, CW_JOB_CODE
			from tempemployees where employeeId not in (select badge from Employees)					
		



			/*
			Populate the Varian/PSE employee table
			*/

			update projectresourcemgmt..Employees set department =b.department, FIRSTNAME = b.firstname, LastName = b.lastName,shift =b.shift,
			ee_group = b.EE_Group, Sch_Hours = case when b.sch_hours > 40 then 40 else b.sch_hours end, Title = b.title, Hire_Date = b.Hire_Date, Email = b.email, 
			supv_Badge = b.supv_Badge,  active = b.active, Person_Type = b.Person_Type,
			 WorkPhone = b.WorkPhone, termWeek = b.termWeek, nickName = b.nickname, payRate_type = b.payRate_type, jobFamily1 = b.jobFamily1,
			jobFamily2 = b.jobFamily2, jobFamily3 = b.jobFamily3, contWorkerType = b.contWorkerType, cwJobCode = b.cwJobCode
			from projectresourcemgmt..Employees a join employees b on a.Badge = b.Badge

			--delete certain roles for PSE 

			delete projectresourcemgmt..EmployeeRoleDetails where functionalArea = 'PSE' and Badge in 
			(
			select a.badge
			from Employees a join projectresourcemgmt..DepartmentList b on a.DEPARTMENT = b.Department join projectresourcemgmt..EmployeeRoleDetails c on a.Badge = c.Badge and b.dept_type = c.functionalArea 
			join projectresourcemgmt..roles d on c.Role = d.ID and c.functionalArea = d.dept_type
			where b.dept_type = 'PSE' and a.Person_Type = 'Active' and isnull(b.subType, '') != '' and not  d.RoleCategory in('Admin', 'Not_a_Resource')
			and b.subType != d.RoleCategory
			)

			insert projectresourcemgmt..Employees (Badge,  FIRSTNAME, LastName, department, shift, active, ee_group, Sch_Hours, Title, Hire_Date, 
			Email,  supv_Badge, workphone, Person_Type, ftes, nickName, payRate_type, jobFamily1, jobFamily2, jobFamily3, contWorkerType, cwJobCode )
			select badge, firstName, lastName, department, shift, active, EE_Group, 
			Sch_Hours, title, Hire_Date, email, supv_Badge, workphone, Person_Type,ftes, nickName, payRate_type, jobFamily1,
			jobFamily2, jobFamily3, contWorkerType, cwJobCode
			from employees where badge not in (select badge from projectresourcemgmt..Employees) 
			and not cwJobCode in ('CNT003', 'CNT006', 'CNT008', 'CNT009')

			--fix for 40 hours for PSE
			update projectresourcemgmt..employees set Sch_Hours = 40 where DEPARTMENT in (select DEPARTMENT from projectresourcemgmt..DepartmentList where dept_type = 'PSE')

			--sets resources as inactive for PSE/Varian
			update projectresourcemgmt..Employees set active = 0, Person_Type = 'inactive', termWeek =  @nowWeek 
			where Person_Type != 'inactive' and badge not in (select badge from Employees where Person_Type != 'inactive') --all on leave or active resources can change to inactive

			/*
			Populate the GFP employee table
			*/

			/*
			Only 2 contractor types will be allowed into GFP
			*/
			update gfpPRM..Employees set department =b.department, FIRSTNAME = b.firstname, LastName = b.lastName,shift =b.shift,
			ee_group = b.EE_Group, Sch_Hours = case when b.sch_hours > 40 then 40 else b.sch_hours end, Title = b.title, Hire_Date = b.Hire_Date, Email = b.email, 
			supv_Badge = b.supv_Badge,  active = b.active, Person_Type = b.Person_Type,
			 WorkPhone = b.WorkPhone, termWeek = b.termWeek, nickName = b.nickname, payRate_type = b.payRate_type, jobFamily1 = b.jobFamily1,
			jobFamily2 = b.jobFamily2, jobFamily3 = b.jobFamily3, contWorkerType = b.contWorkerType, cwJobCode = b.cwJobCode
			from gfpPRM..Employees a join employees b on a.Badge = b.Badge

			insert gfpPRM..Employees (Badge,  FIRSTNAME, LastName, department, shift, active, ee_group, Sch_Hours, Title, Hire_Date, 
			Email,  supv_Badge, workphone, Person_Type, ftes, nickName, payRate_type, jobFamily1, jobFamily2, jobFamily3, contWorkerType, cwJobCode )
			select badge, firstName, lastName, department, shift, active, EE_Group, 
			Sch_Hours, title, Hire_Date, email, supv_Badge, workphone, Person_Type,ftes, nickName, payRate_type, jobFamily1,
			jobFamily2, jobFamily3, contWorkerType, cwJobCode
			from employees where badge not in (select badge from gfpPRM..Employees) 
			and case when contWorkerType = 'Contractor-Outside Services' then 'ok Contractor' when contWorkerType = 'Contractor-Professional Svcs' then 'ok Contractor'  else  jobFamily3 end  != 'Contractors'

			--sets resources as inactive for GFP
			update gfpPRM..Employees set active = 0, Person_Type = 'inactive', termWeek =  @nowWeek 
			where Person_Type != 'inactive' and badge not in (select badge from Employees where Person_Type != 'inactive') --all on leave or active resources can change to inactive


			--backup staging table and clean out working table
			IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'dbo.employee_log') AND type in (N'U'))
				drop table employee_log

			select * into employee_log from tempEmployees --backup of actual data sent from B2B

			truncate table tempemployees --this must be done in state1


		end

			
			
END






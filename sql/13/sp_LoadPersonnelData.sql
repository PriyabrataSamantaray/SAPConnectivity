IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_LoadPersonnelData]') AND type in (N'P', N'PC'))
	DROP PROCEDURE [dbo].[sp_LoadPersonnelData]
GO


CREATE PROCEDURE [dbo].[sp_LoadPersonnelData]

	@data resourceType READONLY,	-- 1=Create temporary table | 2=Populate table and merge data
	@state int  -- state 1 = done truncate staging table and continue, 0 = file being sent in pieces plz continue
AS
BEGIN
	   declare @nowWeek as nvarchar(8)


	   IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'dbo.tempemployees') AND type in (N'U'))
			truncate table tempemployees

		insert tempEmployees
		select * from @data

		if (@state = 1)
		begin


			SELECT @nowWeek = cast(FiscalYear as nvarchar(4)) + '-' +
			cast(FiscalWeek as nvarchar(2)) FROM t_FiscalWeeks WHERE GETDATE() BETWEEN FiscalWkBegin AND FiscalWkEnd + '23:59:59'

			
			update Employees set department =b.department, FIRSTNAME = b.firstname, LastName = b.lastName,shift =b.shift,
			ee_group = case when b.ee_group ='CW' then 'Contractor' else 'Employee' end, Sch_Hours = case when b.sch_hours > 40 then 40 else b.sch_hours end, Title = b.title, Hire_Date = b.hireDate, Email = b.email, 
			supv_Badge = b.supervisorId,  active = (case when b.status = 'A' then 1 else 0 end), Person_Type = (case when b.status = 'A' then 'Active' when b.status = 'L' then 'Leave' else 'inactive' end),
			 WorkPhone = b.phoneNumber, termWeek = case when b.status = 'A' then null else @nowWeek end, nickName = b.PREFERRED_NAME, payRate_type = b.PAY_RATE_TYPE, jobFamily1 = b.JOB_FAMILY_LEVEL1,
			jobFamily2 = b.JOB_FAMILY_LEVEL2, jobFamily3 = b.JOB_FAMILY_LEVEL3, contWorkerType = b.CONTINGENT_WORKER_TYPE, cwJobCode = b.CW_JOB_CODE
			from Employees a join tempemployees b on a.Badge = b.employeeId

			insert Employees (Badge,  FIRSTNAME, LastName, department, shift, active, ee_group, Sch_Hours, Title, Hire_Date, 
			Email,  supv_Badge, workphone, Person_Type, ftes, nickName, payRate_type, jobFamily1, jobFamily2, jobFamily3, contWorkerType, cwJobCode )
			select employeeId, firstName, lastName, department, shift, (case when status = 'A' then 1 else 0 end), case when ee_group = 'CW' then 'Contractor' else 'Employee' end, 
			case when sch_hours > 40 then 40 else sch_hours end, title, hireDate, email, supervisorId, phoneNumber, 'Active',1, PREFERRED_NAME, PAY_RATE_TYPE, JOB_FAMILY_LEVEL1,
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



			/*
			Populate the GFP employee table
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

			/*
			Only 2 contractor types will be allowed into GFP
			*/




		end

			
			
END






GO



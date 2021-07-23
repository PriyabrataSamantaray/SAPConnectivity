IF OBJECT_ID('v_overlappingCostCenters', 'V') IS  NULL
    BEGIN
        EXECUTE ('CREATE view v_overlappingCostCenters AS SELECT 1 as col ');
    END

GO

Alter VIEW v_overlappingCostCenters
AS
	select a.department, a.departmentname, case when b.dept_type = 'Engineering' then 'Varian' else 'PSE' end as area, case when c.DEPARTMENT is null then 0 else 1 end as hasRole
	from  gfpPRM..departmentlist a join projectresourcemgmt..departmentList b on a.department = b.department left join
	(select distinct a.department from projectresourcemgmt..Employees a join projectresourcemgmt..employeeRoleDetails b on a.badge = b.badge where a.Person_Type = 'Active') c on a.Department = c.DEPARTMENT
	where not b.dept_type = 'core' and a.active = 1 and b.active = 1

GO



IF OBJECT_ID('v_adGroups', 'V') IS  NULL
    BEGIN
        EXECUTE ('CREATE view v_adGroups AS SELECT 1 as ''x'' ');
    END

GO

Alter VIEW v_adGroups
AS
	

	select adName, privilege, functionalArea from projectresourcemgmt..adGroups where not functionalArea = 'All'
	union
	select a.adname, 'Admin', b.area from projectresourcemgmt..adGroups a, 
	(select distinct functionalarea as area from projectresourcemgmt..adGroups where not functionalArea = 'All') b
	where a.functionalARea = 'All'
	union
	select adName, privilege, functionalArea from gfpPRM..adGroups where privilege = 'Admin' and functionalArea = 'GFP'

GO
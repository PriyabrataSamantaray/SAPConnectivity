
IF OBJECT_ID('v_gfpCostCenters', 'V') IS  NULL
    BEGIN
        EXECUTE ('CREATE view v_gfpCostCenters AS SELECT 1 as ''x'' ');
    END

GO

Alter VIEW v_gfpCostCenters
AS
	

	select Department from gfpPRM..DepartmentList

GO
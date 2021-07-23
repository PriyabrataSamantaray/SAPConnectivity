IF OBJECT_ID('v_gfpBUUsers', 'V') IS  NULL
    BEGIN
        EXECUTE ('CREATE view v_gfpBUUsers AS SELECT 1 as ''x'' ');
    END

GO

Alter VIEW v_gfpBUUsers
AS
	
	SELECT buOps,financeController,buGm FROM gfpPRM..[EmployeeBuLevelMapping]

GO
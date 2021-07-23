
IF OBJECT_ID('v_pseSecurity', 'V') IS  NULL
    BEGIN
        EXECUTE ('CREATE view v_pseSecurity AS SELECT 1 as ''x'' ');
    END

GO

Alter VIEW v_pseSecurity
AS
	
	select employeeID from ProjectResourceMgmt..v_pseSecurity

GO
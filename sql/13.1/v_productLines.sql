

IF OBJECT_ID('v_productLines', 'V') IS  NULL
    BEGIN
        EXECUTE ('CREATE view v_productLines AS SELECT 1 as ''x''');
    END

GO

/****** Object:  View [dbo].[v_productLines]    Script Date: 7/9/2018 1:12:03 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO



ALTER VIEW [dbo].[v_productLines]
AS
 select a.AMAT_PRODUCT, a.BUSS_UNIT,  c.LINE_G1_DESC, d.LINE_G2_DESC, b.PROD_DESC as G3, 
 case when b.INACTIVE_PROD = 'X' then 0 else 1 end as status -- 0 is inactive and 1 is active
 from [SAP_MDG_ZAF0207_Staging] a 
 join [SAP_MDG_ZAF0204_Staging] b on a.AMAT_PRODUCT= b.AMAT_PRODUCT
 join [SAP_MDG_ZAF0205_Staging] c on a.PROD_LINE_G1 = c.PROD_LINE_G1
 join [SAP_MDG_ZAF0206_Staging] d on a.PROD_LINE_G2 = d.PROD_LINE_G2
-- where a.VALID_TO = '9999-12-31'
GO



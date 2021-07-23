IF OBJECT_ID('v_productLines', 'V') IS NOT NULL
	DROP VIEW v_productLines
GO

CREATE VIEW [dbo].[v_productLines]
AS
 select a.AMAT_PRODUCT, a.BUSS_UNIT,  c.LINE_G1_DESC, d.LINE_G2_DESC, b.PROD_DESC as G3
 from [SAP_MDG_ZAF0207_Staging] a join [SAP_MDG_ZAF0204_Staging] b on a.AMAT_PRODUCT= b.AMAT_PRODUCT
 join [SAP_MDG_ZAF0205_Staging] c on a.PROD_LINE_G1 = c.PROD_LINE_G1
 join [SAP_MDG_ZAF0206_Staging] d on a.PROD_LINE_G2 = d.PROD_LINE_G2
 where a.VALID_TO = '9999-12-31'
GO



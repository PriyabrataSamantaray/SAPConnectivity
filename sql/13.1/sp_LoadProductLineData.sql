
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_LoadProductLineData]') AND type in (N'P', N'PC'))
	DROP PROCEDURE [dbo].[sp_LoadProductLineData]
GO
/****** Object:  StoredProcedure [dbo].[sp_LoadProductLineData]    Script Date: 7/9/2018 1:22:16 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO
 

CREATE PROCEDURE [dbo].[sp_LoadProductLineData]

AS
BEGIN

		/* One Time
		
		update projectresourcemgmt..buToKPU
		set productID = AMAT_product 
		from v_productLines where v_productLines.BUSS_UNIT = projectresourcemgmt..buToKPU.bu and
		v_productLines.LINE_G1_DESC = projectresourcemgmt..buToKPU.kpu and v_productLines.LINE_G2_DESC = projectresourcemgmt..buToKPU.G2
		and v_productLines.G3 = projectresourcemgmt..buToKPU.G3

		*/

		if (select count(*) from v_productLines) = 0
			return


	   update projectresourcemgmt..buToKPU
	   set projectresourcemgmt..buToKPU.bu =  case when v_productLines.BUSS_UNIT = 'DCVD' then projectresourcemgmt..buToKPU.bu when v_productLines.AMAT_PRODUCT = '2290' then 'CMP'
	   when v_productLines.AMAT_PRODUCT = '2184' then 'MDP' when v_productLines.AMAT_PRODUCT = '2183' then 'Etch' else v_productLines.BUSS_UNIT end, 
	   projectresourcemgmt..buToKPU.kpu = v_productLines.LINE_G1_DESC,
	   projectresourcemgmt..buToKPU.G2 = v_productLines.LINE_G2_DESC, projectresourcemgmt..buToKPU.G3 = v_productLines.G3,
	   projectresourcemgmt..buToKPU.Status = v_productLines.status
	   from v_productLines where v_productLines.AMAT_PRODUCT = projectresourcemgmt..buToKPU.productId


	   insert projectresourcemgmt..buToKPU (functionalArea, bu, kpu, G2, G3, Status, ShowOnPSE, productId)
	   select 'PSE', case when BUSS_UNIT = 'DCVD' then 'DSM' when AMAT_PRODUCT = '2290' then 'CMP' when AMAT_PRODUCT = '2184' then 'MDP' when AMAT_PRODUCT = '2183' then 'Etch'  else v_productLines.BUSS_UNIT end,
	   LINE_G1_DESC, LINE_G2_DESC, G3, Status,0, AMAT_PRODUCT
	   from v_productLines where AMAT_PRODUCT not in (select isnull(productId,'') from projectresourcemgmt..buToKPU)
	   and not G3 like '% ONLY'



			
			
END







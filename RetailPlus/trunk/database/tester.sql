
/**************************************************************

	procProcessGLA
	Lemuel E. Aceron
	october 22, 2013
	
	Desc: This will process all the GLA from infogenesis

	CALL procProcessGLA();
	
**************************************************************/
delimiter GO
DROP PROCEDURE IF EXISTS procProcessGLA
GO
	
create procedure procProcessGLA(
	IN strBatchID	VARCHAR(60)
)
BEGIN

	DECLARE intINFOGENESISGroupID INT(4) DEFAULT 1;

	-- update the discount using the latest discounts
	INSERT INTO tblDiscount(DiscountID, DiscountCode, DiscountType, DiscountPrice, InPercent)
	SELECT CONCAT(999,Dsc_Number), Dsc_Name, Dsc_Name, 0, 1 FROM tblgla_d_dsc_def
	WHERE CONCAT(999,Dsc_Number) NOT IN (SELECT DiscountID FROM tblDiscount) AND BatchID = strBatchID
	GROUP BY CONCAT(999,Dsc_Number), Dsc_Name, Dsc_Name;

	UPDATE tblDiscount dsc 
	INNER JOIN tblgla_d_dsc_def def ON dsc.DiscountID = CONCAT(999,def.Dsc_Number) AND BatchID = strBatchID SET
		DiscountCode = Dsc_Name,
		DiscountType = Dsc_Name
	WHERE DiscountCode <> Dsc_Name;

	/**** Step 1: pdate the infogenesis users ***/

	SELECT GroupID INTO intINFOGENESISGroupID FROM sysAccessGroups WHERE GroupName = 'INFOGENESIS';

	INSERT INTO sysAccessUsers(UID, UserName, Password, DateCreated)
	SELECT CONCAT(999,Emp_Number), CONCAT('gen_',Emp_Number) UserName, First_Name, NOW() FROM tblgla_d_emp_def
	WHERE CONCAT(999,Emp_Number) NOT IN (SELECT UID FROM sysAccessUsers) AND BatchID = strBatchID
	GROUP BY CONCAT(999,Emp_Number), CONCAT('gen_',Emp_Number), First_Name, NOW()
	ORDER BY Emp_Number;

	INSERT INTO sysAccessUserDetails(UID, Name, EmailAddress, GroupID, CountryID, PageSize)
	SELECT usrs.UID, CONCAT(emp.First_Name,' ',emp.Last_Name, ' ',usrs.UID) Name, '' EmailAddress, intINFOGENESISGroupID GroupID, 1, 10 PageSize
	FROM tblgla_d_emp_def emp 
	INNER JOIN sysAccessUsers usrs ON CONCAT(999,Emp_Number) = usrs.UID
	WHERE UID NOT IN (SELECT UID FROM SysAccessUserDetails) AND BatchID = strBatchID
	GROUP BY usrs.UID, CONCAT(emp.First_Name,' ',emp.Last_Name, ' ',usrs.UID)
	ORDER BY UID;

	-- update the branch
	INSERT INTO tblBranch(BranchID, BranchCode, BranchName, DBIP, DBPort)
	SELECT CONCAT(999,Rvc_Number) BranchID, Rvc_Name BranchCode, Rvc_Name BranchName, '127.0.0.1' DBIP, 3306 DBPort
	FROM tblgla_d_location_def 
	WHERE Rvc_Number > 0 AND CONCAT(999,Rvc_Number)  NOT IN (SELECT BranchID FROM tblBranch) AND BatchID = strBatchID
	GROUP BY CONCAT(999,Rvc_Number), Rvc_Name
	ORDER BY Rvc_Number;

	UPDATE tblBranch brnch
	INNER JOIN tblgla_d_location_def loc ON brnch.BranchID = CONCAT(999,loc.Rvc_Number) AND BatchID = strBatchID SET
		brnch.BranchCode = Rvc_Name,
		brnch.BranchName = Rvc_Name
	WHERE BranchCode <> Rvc_Name;

	-- update the products

	INSERT INTO tblProductGroup(ProductGroupID, ProductGroupCode, ProductGroupName, BaseUnitID, IncludeInSubtotalDiscount, SequenceNo)
	SELECT CONCAT(999,Sales_Itemizer_Number), Sales_Itemizer_Name, Sales_Itemizer_Name, 1, 1, Sales_Itemizer_Number
	FROM tblgla_d_mi_def
	WHERE CONCAT(999,Sales_Itemizer_Number) NOT IN (SELECT ProductGroupID FROM tblProductGroup) AND BatchID = strBatchID
	GROUP BY CONCAT(999,Sales_Itemizer_Number), Sales_Itemizer_Name 
	ORDER BY Sales_Itemizer_Number;

	UPDATE tblProductGroup grp
	INNER JOIN (
		SELECT CONCAT(999,Sales_Itemizer_Number) Sales_Itemizer_Number, Sales_Itemizer_Name
		FROM tblgla_d_mi_def
		WHERE BatchID = strBatchID
		GROUP BY Sales_Itemizer_Number, Sales_Itemizer_Name 
	) gla_grp ON grp.ProductGroupID = gla_grp.Sales_Itemizer_Number	SET
		grp.ProductGroupCode = gla_grp.Sales_Itemizer_Name,
		grp.ProductGroupName = gla_grp.Sales_Itemizer_Name
	WHERE grp.ProductGroupCode <> gla_grp.Sales_Itemizer_Name;

	INSERT INTO tblProductSubGroup(ProductGroupID, ProductSubGroupID, ProductSubGroupCode, ProductSubGroupName, BaseUnitID, IncludeInSubtotalDiscount, SequenceNo)
	SELECT CONCAT(999,Sales_Itemizer_Number), CONCAT(999,Sales_Itemizer_Number), Sales_Itemizer_Name, Sales_Itemizer_Name, 1, 1, Sales_Itemizer_Number
	FROM tblgla_d_mi_def
	WHERE CONCAT(999,Sales_Itemizer_Number) NOT IN (SELECT ProductSubGroupID FROM tblProductSubGroup) AND BatchID = strBatchID
	GROUP BY CONCAT(999,Sales_Itemizer_Number), Sales_Itemizer_Name 
	ORDER BY Sales_Itemizer_Number;

	UPDATE tblProductSubGroup sgrp
	INNER JOIN (
		SELECT CONCAT(999,Sales_Itemizer_Number) Sales_Itemizer_Number, Sales_Itemizer_Name
		FROM tblgla_d_mi_def
		WHERE BatchID = strBatchID
		GROUP BY Sales_Itemizer_Number, Sales_Itemizer_Name 
	) gla_grp ON sgrp.ProductSubGroupID = gla_grp.Sales_Itemizer_Number	SET
		sgrp.ProductSubGroupCode = gla_grp.Sales_Itemizer_Name,
		sgrp.ProductSubGroupName = gla_grp.Sales_Itemizer_Name
	WHERE sgrp.ProductSubGroupCode <> gla_grp.Sales_Itemizer_Name;

	-- append 999 to identify that it is Infogenesis products
	INSERT INTO tblProducts(ProductID, ProductCode, ProductDesc, ProductSubGroupID, BaseUnitID, DateCreated, IncludeInSubTotalDiscount
	 	,SupplierID ,IsItemSold, Active, SequenceNo)
	SELECT CONCAT(999,Mi_Number), LEFT(Mi_Name, 30), Mi_Name, CONCAT(999,Sales_Itemizer_Number), 1 BaseUnitID, NOW(), 1 IncludeInSubTotalDiscount
		,2 SupplierID, 1 IsItemSold, 1 Active, Def_Seq SequenceNo
	FROM tblgla_d_mi_def
	WHERE CONCAT(999,Mi_Number) NOT IN (SELECT ProductID FROM tblProducts) AND BatchID = strBatchID
	GROUP BY CONCAT(999,Mi_Number), LEFT(Mi_Name, 30), Mi_Name, Sales_Itemizer_Name
	ORDER BY Mi_Number;

	UPDATE tblProducts prd
	INNER JOIN (
		SELECT CONCAT(999,Mi_Number) ProductID, LEFT(Mi_Name, 30) ProductCode, Mi_Name ProductDesc, CONCAT(999,Sales_Itemizer_Number) ProductSubGroupID
		FROM tblgla_d_mi_def
		WHERE BatchID = strBatchID
		GROUP BY CONCAT(999,Mi_Number), LEFT(Mi_Name, 30), Mi_Name
	) gla_prd ON prd.ProductID = gla_prd.ProductID SET
		prd.ProductCode = gla_prd.ProductCode,
		prd.ProductDesc = gla_prd.ProductDesc,
		prd.ProductSubGroupID = gla_prd.ProductSubGroupID
	WHERE prd.ProductCode <> gla_prd.ProductCode;

	INSERT INTO tblProductPackage(PackageID, ProductID, UnitID, Price, PurchasePrice, Quantity, VAT, EVAT, LocalTax, WSPrice, BarCode1)
	SELECT ProductID, ProductID, BaseUnitID, 0, 0, 1 Quantity, 12, 0, 0, 0, ProductCode
	FROM tblProducts WHERE ProductID NOT IN (SELECT ProductID FROM tblProductPackage);

	-- update charge types
	INSERT INTO tblChargeType(ChargeTypeID, ChargeTypeCode, ChargeType, ChargeAmount, InPercent)
	SELECT CONCAT(999,Svc_Number), Svc_Name, Svc_Name, 0, 1 FROM tblgla_d_svc_def
	WHERE CONCAT(999,Svc_Number) NOT IN (SELECT ChargeTypeID FROM tblChargeType) AND BatchID = strBatchID
	GROUP BY CONCAT(999,Svc_Number), Svc_Name;

	UPDATE tblChargeType svc	
	INNER JOIN tblgla_d_svc_def def ON svc.ChargeTypeID = CONCAT(999,def.Svc_Number) AND BatchID = strBatchID SET
		ChargeTypeCode = Svc_Name,
		ChargeType = Svc_Name
	WHERE ChargeTypeCode <> Svc_Name;

	-- update the contacts 
	INSERT INTO tblContacts(ContactCode, ContactName, ContactGroupID
		,ModeOfTerms, Terms, Address, BusinessName, TelephoneNo, Remarks
		,Debit, Credit, CreditLimit, IsCreditAllowed, DateCreated
		,Deleted, DepartmentID, PositionID, isLock
	)
	SELECT RIGHT(dsc.Ref_Info_1, 6) ContactCode, RIGHT(dsc.Ref_Info_1, 6) ContactName, 21 ContactGroupID
		,0 ModeOfTerms, 0 Terms, '' Address, dsc.Ref_Info_1 BusinessName, '' TelephoneNo, '' Remarks
		,0 Debit, 0 Credit, 0 CreditLimit, 0 IsCreditAllowed, NOW() DateCreated
		,0 Deleted, 1 DepartmentID, 1 PositionID, 0 isLock
 	FROM tblgla_f_dtl_chk_dsc dsc
	WHERE BatchID = strBatchID
		AND IFNULL(RIGHT(dsc.Ref_Info_1, 6),'') <> ''
		AND RIGHT(dsc.Ref_Info_1, 6) NOT IN (SELECT ContactCode FROM tblContacts)
	GROUP BY RIGHT(dsc.Ref_Info_1, 6), dsc.Ref_Info_1
	;

	-- update the transactions
	DELETE FROM tblTransactionItems WHERE IFNULL(Datasource,'') = strBatchID;
	DELETE FROM tblTransactionItems WHERE TransactionID IN (SELECT TransactionID FROM tblTransactions WHERE IFNULL(Datasource,'') = strBatchID);
	DELETE FROM tblTransactions WHERE IFNULL(Datasource,'') = strBatchID;

	INSERT INTO tblTransactions(TransactionID, TransactionNo, CustomerID , CustomerName, CashierID, CashierName, TerminalNo, TransactionDate
		,DateSuspended, DateResumed, TransactionStatus, SubTotal, Discount 
		,TransDiscount, TransDiscountType, VAT, VatableAmount, EVAT, EVATableAmount, LocalTax
		,AmountPaid, CashPayment, ChequePayment, CreditCardPayment, CreditPayment, BalanceAmount, ChangeAmount
		,DateClosed, PaymentType, DiscountCode, DebitPayment, ItemsDiscount, Charge, ChargeAmount, ChargeCode, ChargeRemarks, WaiterID, WaiterName
		,Packed, OrderType, AgentID, AgentName, CreatedByID, CreatedByName
		,AgentDepartmentName, AgentPositionName, ReleaserID, ReleaserName, ReleasedDate
		,RewardPointsPayment, RewardConvertedPayment, PaxNo, CreditChargeAmount, BranchID, BranchCode, TransactionType, isConsignment
		,Datasource)
	SELECT chk_headers_seq_number TransactionID, chk_num TransactionNo
		,IFNULL(cntct.ContactID,1) CustomerID ,IFNULL(cntct.ContactName, 'RetailPlus Default Customer') CustomerName -- ,RIGHT(dsc.Ref_Info_1, 6) ContactCode
		,CONCAT(999,hdr.fk_emp_def) CashierID,  IFNULL(usr.Name, '') CashierName, hdr.fk_location_def TerminalNo, hdr.Chk_Open_Date_Time TransactionDate
		,'1900-01-01' DateSuspended ,'1900-01-01' DateResumed 
		,case when dsc.Status_Flag = 'DSC_RTN' THEN 5
			  when dsc.Status_Flag = 'DSC_VOID' THEN 3
			  else 1
		 end TransactionStatus 
		,(hdr.Sub_Ttl + Tax_Ttl + -hdr.Dsc_Ttl) SubTotal ,-hdr.Dsc_Ttl Discount 
		,0 TransDiscount, IFNULL(dsc.fk_dsc_def,0) TransDiscountType
		,Tax_Ttl VAT ,hdr.Sub_Ttl VatableAmount, 0 EVAT,0 EvatableAmount, 0 LocalTax
		,hdr.Pymnt_Ttl AmountPaid ,hdr.Pymnt_Ttl CashPayment, 0 ChequePayment, 0 CreditCardPayment, 0, 0
		,(hdr.Pymnt_Ttl -((hdr.Sub_Ttl + Tax_Ttl + -hdr.Dsc_Ttl) - -hdr.Dsc_Ttl + (hdr.Auto_Svc_Ttl + hdr.Other_Svc_Ttl) )) ChangeAmount
		,hdr.Chk_Closed_Date_Time DateClosed ,0 PaymentType ,dis.DiscountCode ,0 DebitPayment ,0 ItemsDiscount
		,(hdr.Auto_Svc_Ttl + hdr.Other_Svc_Ttl) Charge ,(hdr.Auto_Svc_Ttl + hdr.Other_Svc_Ttl) ChargeAmount
		,MAX(IFNULL(chrg.ChargeTypeCode, '')) ChargeCode, '' ChargeRemarks, 2 WaiterID, 'RetailPlus Default' WaiterName
		,0 Packed, 0 OrderType, 1 AgentID, 'RetailPlus Agent ?' AgentName, CONCAT(999,hdr.fk_emp_def) CreatedByID, IFNULL(usr.Name, '') CreatedByName
		,'System Default Department' AgentDepartmentName, 'System Default Position' AgentPositionName, 0 ReleaserID, '' ReleaserName, hdr.fk_business_date ReleasedDate
		,0 RewardPointsPayment,0 RewardConvertedPayment, 0 PaxNo, 0 CreditChargeAmount
		,CONCAT(999,hdr.fk_location_def) BranchID, brnch.BranchCode
		,case when dsc.Status_Flag = 'DSC_RTN' THEN 1 else 0 end TransactionType, 0 isConsignment
		,hdr.BatchID
		--  + Auto_Svc_Ttl + Other_Svc_Ttl
	FROM tblgla_f_dtl_chk_headers hdr
	INNER JOIN tblgla_f_dtl_chk_dsc dsc ON dsc.fk_chk_headers = hdr.chk_headers_seq_number
	LEFT OUTER JOIN tblgla_f_dtl_chk_svc svc ON svc.fk_chk_headers = hdr.chk_headers_seq_number
	LEFT OUTER JOIN tblContacts cntct ON RIGHT(dsc.Ref_Info_1, 6) = cntct.ContactCode
	LEFT OUTER JOIN sysAccessUserDetails usr ON CONCAT(999,hdr.fk_emp_def) = usr.UID
	LEFT OUTER JOIN tblDiscount dis ON CONCAT(999,dsc.fk_dsc_def) = dis.DiscountID
	LEFT OUTER JOIN tblChargeType chrg ON CONCAT(999,svc.fk_svc_def) = chrg.ChargeTypeID
	LEFT OUTER JOIN tblBranch brnch ON CONCAT(999,hdr.fk_location_def) = brnch.BranchID
	WHERE hdr.BatchID = strBatchID 
		AND IFNULL(hdr.chk_headers_seq_number,0) NOT IN (SELECT TransactionID FROM tblTransactions)
		AND IFNULL(RIGHT(dsc.Ref_Info_1, 6),'') <> ''
	GROUP BY chk_headers_seq_number, chk_num
		,IFNULL(cntct.ContactID,1) ,IFNULL(cntct.ContactName, 'RetailPlus Default Customer') -- ,RIGHT(dsc.Ref_Info_1, 6) ContactCode
		,CONCAT(999,hdr.fk_emp_def),IFNULL(usr.Name, ''), hdr.fk_location_def, hdr.Chk_Open_Date_Time
		,case when dsc.Status_Flag = 'DSC_RTN' THEN 5
			  when dsc.Status_Flag = 'DSC_VOID' THEN 3
			  else 1
		 end 
		,(hdr.Sub_Ttl + -hdr.Dsc_Ttl) ,-hdr.Dsc_Ttl 
		,IFNULL(dsc.fk_dsc_def,0)
		,Tax_Ttl ,hdr.Sub_Ttl
		,hdr.Pymnt_Ttl ,hdr.Pymnt_Ttl
		,hdr.Chk_Closed_Date_Time ,dis.DiscountCode
		,(hdr.Auto_Svc_Ttl + hdr.Other_Svc_Ttl) ,(hdr.Auto_Svc_Ttl + hdr.Other_Svc_Ttl)
		,CONCAT(999,hdr.fk_emp_def), IFNULL(usr.Name, ''), hdr.fk_business_date
		,CONCAT(999,hdr.fk_location_def), brnch.BranchCode
		,case when dsc.Status_Flag = 'DSC_RTN' THEN 1 else 0 end
		,hdr.BatchID;
	

	-- insert to transaciton items table
	INSERT INTO tblTransactionItems(TransactionID, ProductID, ProductCode, BarCode, Description
		,ProductUnitID, ProductUnitCode, Quantity, Price, SellingPrice, Discount, ItemDiscount, ItemDiscountType
		,Amount, VAT, vatableAmount, EVAT, EVATableAmount, LocalTax, VariationsMatrixID, MatrixDescription, ProductGroup, ProductSubGroup
		,TransactionItemStatus, DiscountCode, DiscountRemarks, ProductPackageID, MatrixPackageID, PackageQuantity
		,PromoQuantity, PromoValue, PromoType, PromoApplied, PurchasePrice, PurchaseAmount
		,IncludeInSubtotalDiscount, OrderSlipPrinter, orderslipprinted, PercentageCommision, Commision, PaxNo, TransactionDiscount, Datasource
	)
	SELECT trx.TransactionID, CONCAT(999,det.fk_mi_def) ProductID, prd.ProductCode, pkg.Barcode1, prd.ProductDesc Description
		,pkg.UnitID ProductUnitID, unt.UnitCode ProductUnitCode
		,det.Item_Count Quantity, (det.Item_Total / (case when det.Item_Count = 0 then 1 else det.Item_Count end)) Price, 0 SellingPrice, 0 Discount, 0 ItemDiscount, 0 ItemDiscountType
		,det.Item_Total Amount, 12 VAT, (det.Item_Total / 1.12) VatableAmount, 0 EVAT, 0 EVATableAmount, 0 LocalTax
		,0 VariationsMatrixID, '' MatrixDescription, grp.ProductGroupCode ProductGroup, sgrp.ProductSubGroupCode ProductSubGroup
		,case when Status_Flag = 'MI_VOID' then 1
			  when Status_Flag = 'MI_RTN' then 4
			  else 0
		 end TransactionItemStatus
		,'' DiscountCode, '' DiscountRemarks, pkg.PackageID ProductPackageID, 0 MatrixPackageID, 1 PackageQuantity
		,0 PromoQuantity, 0 PromoValue, 0 PromoType, 0 PromoApplied, 0 PurchasePrice, 0 PurchaseAmount
		,prd.IncludeInSubtotalDiscount, grp.OrderSlipPrinter, 0 orderslipprinted, 0 PercentageCommision, 0 Commision, 1 PaxNo, 0 TransactionDiscount
		,det.BatchID
	FROM tblTransactions trx
	INNER JOIN tblgla_f_dtl_chk_mi det ON det.fk_chk_headers = trx.TransactionID
	INNER JOIN tblProducts prd ON CONCAT(999,det.fk_mi_def) = prd.ProductID 
	INNER JOIN tblProductPackage pkg ON pkg.ProductID = prd.ProductID AND pkg.UnitID = prd.BaseUnitID AND pkg.Quantity = 1
	INNER JOIN tblUnit unt ON pkg.UnitID = unt.UnitID
	INNER JOIN tblProductSubGroup sgrp ON sgrp.ProductSubGroupID = prd.ProductSubGroupID
	INNER JOIN tblProductGroup grp ON grp.ProductGroupID = sgrp.ProductGroupID
	WHERE det.BatchID = strBatchID
	GROUP BY trx.TransactionID, CONCAT(999,det.fk_mi_def), prd.ProductCode, pkg.Barcode1, prd.ProductDesc
		,pkg.UnitID, unt.UnitCode
		,det.Item_Count, (det.Item_Total / det.Item_Count)
		,det.Item_Total, (det.Item_Total / 1.12)
		, grp.ProductGroupCode, sgrp.ProductSubGroupCode
		,case when Status_Flag = 'MI_VOID' then 1
			  when Status_Flag = 'MI_RTN' then 4
			  else 0
		 end
		,prd.IncludeInSubtotalDiscount, grp.OrderSlipPrinter
	ORDER BY det.fk_chk_headers, det.Dtl_Num;

END;
GO
delimiter ;

 -- CALL procProcessGLA('3030104130105');


 -- select * from tblTransactionItems  limit 50;

 -- select * from tblgla_f_dtl_chk_mi limit 5;
 -- select * from tbltransactionitems limit 5;

-- select * from tblproductgroup;
-- select * from tblproductsubgroup;
	

/****

	select COUNT(*) d_emp_def from tblgla_d_emp_def where batchid = '3030101130102';
	select COUNT(*) d_location_def from tblgla_d_location_def where batchid = '3030101130102';
	select COUNT(*) f_dtl_chk_headers from tblgla_f_dtl_chk_headers where batchid = '3030101130102';
	select COUNT(*) f_dtl_chk_mi from tblgla_f_dtl_chk_mi where batchid = '3030101130102';

select * from tblcontacts limit 5;
	select * from tblcontactaddon limit 5;

select * from tblgla_d_mi_def limit 5;

select Mi_Number, Mi_Name, Sales_Itemizer_Number, Sales_Itemizer_Name 
	,Family_Group_Number, Family_Group_Name, Major_Group_Number, Major_Group_Name
	,Mi_Class_Number ,Mi_Class_Name
from tblgla_d_mi_def 
GROUP by Mi_Number, Mi_Name, Sales_Itemizer_Number, Sales_Itemizer_Name 
	,Family_Group_Number, Family_Group_Name, Major_Group_Number, Major_Group_Name
	,Mi_Class_Number ,Mi_Class_Name
limit 5;
***/
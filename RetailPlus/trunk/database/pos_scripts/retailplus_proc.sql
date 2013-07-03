delimiter GO
DROP PROCEDURE IF EXISTS procGenerateSalesPerItem
GO

create procedure procGenerateSalesPerItem(
	IN strSessionID varchar(30),
	IN strTransactionNo varchar(30),
	IN strCustomerName varchar(100),
	IN strCashierName varchar(100),
	IN strTerminalNo varchar(30),
	IN dteStartTransactionDate DateTime,
	IN dteEndTransactionDate DateTime
	)
BEGIN
	DECLARE intOpenTransactionStatus, intValidTransactionItemStatus, intReturnTransactionItemStatus, intRefundransactionItemStatus INTEGER DEFAULT 0;
	

	SET intOpenTransactionStatus = 0; 
	SET intValidTransactionItemStatus = 0;
	SET intReturnTransactionItemStatus = 3;
	SET intRefundransactionItemStatus = 4;
	
	SET strTransactionNo = IF(NOT ISNULL(strTransactionNo), CONCAT('%',strTransactionNo,'%'), '%%');
	SET strCustomerName = IF(NOT ISNULL(strCustomerName), CONCAT('%',strCustomerName,'%'), '%%');
	SET strCashierName = IF(NOT ISNULL(strCashierName), CONCAT('%',strCashierName,'%'), '%%');
	SET strTerminalNo = IF(NOT ISNULL(strTerminalNo), CONCAT('%',strTerminalNo,'%'), '%%');
	SET dteStartTransactionDate = IF(NOT ISNULL(dteStartTransactionDate), dteStartTransactionDate, '0001-01-01');
	SET dteEndTransactionDate = IF(NOT ISNULL(dteEndTransactionDate), dteEndTransactionDate, now());
	
	INSERT INTO tblSalesPerItem (SessionID, ProductGroup, ProductCode, ProductUnitCode, Quantity, Amount, PurchaseAmount)
	SELECT strSessionID, ProductGroup,
		ProductCode, ProductUnitCode,
		SUM(IF(TransactionItemStatus=3,-a.Quantity,a.Quantity)) 'Quantity', SUM(IF(TransactionItemStatus=3,-a.Amount,a.amount)) Amount, 
		SUM(IF(TransactionItemStatus=3,-PurchaseAmount,IF(TransactionItemStatus=4,-PurchaseAmount,PurchaseAmount))) PurchaseAmount
	FROM tblTransactionItems a 
	INNER JOIN tblTransactions b ON a.TransactionID = b.TransactionID
	WHERE 1=1 
		AND TransactionStatus <> intOpenTransactionStatus 
		AND (TransactionItemStatus = intValidTransactionItemStatus or
			TransactionItemStatus = intReturnTransactionItemStatus or
			TransactionItemStatus = intRefundransactionItemStatus)
		AND TransactionNo LIKE strTransactionNo
		AND CustomerName LIKE strCustomerName
		AND CashierName LIKE strCashierName
		AND TerminalNo LIKE strTerminalNo
		AND DATE_FORMAT(TransactionDate, '%Y-%m-%d %H:%i') >= DATE_FORMAT(dteStartTransactionDate, '%Y-%m-%d %H:%i')
		AND DATE_FORMAT(TransactionDate, '%Y-%m-%d %H:%i') <= DATE_FORMAT(dteEndTransactionDate, '%Y-%m-%d %H:%i')
	GROUP BY IF(MatrixDescription <> NULL, MatrixDescription, ProductCode);

END;
GO
delimiter ;

/********************************************
	procTerminalReportUpdateTransactionSales
********************************************/

delimiter GO
DROP PROCEDURE IF EXISTS procTerminalReportUpdateTransactionSales
GO

create procedure procTerminalReportUpdateTransactionSales(IN intBranchID int(4), IN strTerminalNo varchar(10), 
														IN decGrossSales decimal(10,2),
														IN decTotalDiscount decimal(10,2),
														IN decTotalCharge decimal(10,2),
														IN decDailySales decimal(10,2),
														IN decQuantitySold decimal(10,2),
														IN decGroupSales decimal(10,2),
														IN decOldGrandTotal decimal(10,2),
														IN decNewGrandTotal decimal(10,2),
														IN decVATableAmount decimal(10,2),
														IN decNonVaTableAmount decimal(10,2),
														IN decVAT decimal(10,2),
														IN decEVATableAmount decimal(10,2),
														IN decNonEVaTableAmount decimal(10,2),
														IN decEVAT decimal(10,2),
														IN decLocalTax decimal(10,2),
														IN decCashSales decimal(10,2),
														IN decChequeSales decimal(10,2),
														IN decCreditCardSales decimal(10,2),
														IN decCreditSales decimal(10,2),
														IN decCreditPayment decimal(10,2),
														IN decDebitPayment decimal(10,2),
														IN decRewardPointsPayment decimal(10,2),
														IN decRewardConvertedPayment decimal(10,2),
														IN decCashInDrawer decimal(10,2),
														IN decVoidSales decimal(10,2),
														IN decRefundSales decimal(10,2),
														IN decItemsDiscount decimal(10,2),
														IN decSubTotalDiscount decimal(10,2),
														IN intNoOfCashTransactions int(10),
														IN intNoOfChequeTransactions int(10),
														IN intNoOfCreditCardTransactions int(10),
														IN intNoOfCreditTransactions int(10),
														IN intNoOfCombinationPaymentTransactions int(10),
														IN intNoOfCreditPaymentTransactions int(10),
														IN intNoOfDebitPaymentTransactions int(10),
														IN intNoOfClosedTransactions int(10),
														IN intNoOfRefundTransactions int(10),
														IN intNoOfVoidTransactions int(10),
														IN intNoOfRewardPointsPayment int(10),
														IN intNoOfTotalTransactions int(10),
														IN intNoOfDiscountedTransactions int(10),
														IN decNegativeAdjustments decimal(10),
														IN intNoOfNegativeAdjustmentTransactions  int(10),
														IN decPromotionalItems decimal(10),
														IN decCreditSalesTax decimal(10))
BEGIN

	UPDATE tblTerminalReport SET 
					GrossSales							=  GrossSales							+  decGrossSales, 
					TotalDiscount						=  TotalDiscount						+  decTotalDiscount, 
					TotalCharge							=  TotalCharge							+  decTotalCharge, 
					DailySales							=  DailySales							+  decDailySales, 
					QuantitySold						=  QuantitySold							+  decQuantitySold, 
					GroupSales							=  GroupSales							+  decGroupSales, 
					OldGrandTotal						=  OldGrandTotal						+  decOldGrandTotal, 
					NewGrandTotal						=  NewGrandTotal						+  decNewGrandTotal, 
					VATableAmount						=  VATableAmount						+  decVATableAmount, 
					NonVaTableAmount					=  NonVaTableAmount						+  decNonVaTableAmount, 
					VAT									=  VAT									+  decVAT, 
					EVATableAmount						=  EVATableAmount						+  decEVATableAmount, 
					NonEVaTableAmount					=  NonEVaTableAmount					+  decNonEVaTableAmount, 
					EVAT								=  EVAT									+  decEVAT, 
					LocalTax							=  LocalTax								+  decLocalTax, 
					CashSales							=  CashSales							+  decCashSales, 
					ChequeSales							=  ChequeSales							+  decChequeSales, 
					CreditCardSales						=  CreditCardSales						+  decCreditCardSales, 
					CreditSales							=  CreditSales							+  decCreditSales, 
					CreditPayment						=  CreditPayment						+  decCreditPayment, 
					DebitPayment						=  DebitPayment						    +  decDebitPayment, 
					RewardPointsPayment					=  RewardPointsPayment					+  decRewardPointsPayment, 
					RewardConvertedPayment				=  RewardConvertedPayment			    +  decRewardConvertedPayment, 
					CashInDrawer						=  CashInDrawer							+  decCashInDrawer, 
					VoidSales							=  VoidSales							+  decVoidSales, 
					RefundSales							=  RefundSales							+  decRefundSales, 
					ItemsDiscount						=  ItemsDiscount						+  decItemsDiscount, 
					SubTotalDiscount					=  SubTotalDiscount						+  decSubTotalDiscount, 
					NoOfCashTransactions				=  NoOfCashTransactions					+  intNoOfCashTransactions, 
					NoOfChequeTransactions				=  NoOfChequeTransactions				+  intNoOfChequeTransactions, 
					NoOfCreditCardTransactions			=  NoOfCreditCardTransactions			+  intNoOfCreditCardTransactions, 
					NoOfCreditTransactions				=  NoOfCreditTransactions				+  intNoOfCreditTransactions, 
					NoOfCombinationPaymentTransactions	=  NoOfCombinationPaymentTransactions	+  intNoOfCombinationPaymentTransactions, 
					NoOfCreditPaymentTransactions		=  NoOfCreditPaymentTransactions		+  intNoOfCreditPaymentTransactions, 
					NoOfDebitPaymentTransactions		=  NoOfDebitPaymentTransactions			+  intNoOfDebitPaymentTransactions, 
					NoOfClosedTransactions				=  NoOfClosedTransactions				+  intNoOfClosedTransactions, 
					NoOfRefundTransactions				=  NoOfRefundTransactions				+  intNoOfRefundTransactions, 
					NoOfVoidTransactions				=  NoOfVoidTransactions					+  intNoOfVoidTransactions, 
					NoOfRewardPointsPayment				=  NoOfRewardPointsPayment				+  intNoOfRewardPointsPayment, 
					NoOfTotalTransactions				=  NoOfTotalTransactions				+  intNoOfTotalTransactions,
					NoOfDiscountedTransactions			=  NoOfDiscountedTransactions			+  intNoOfDiscountedTransactions,
					NegativeAdjustments					=  NegativeAdjustments					+  decNegativeAdjustments,
					NoOfNegativeAdjustmentTransactions	=  NoOfNegativeAdjustmentTransactions	+  intNoOfNegativeAdjustmentTransactions,
					PromotionalItems					=  PromotionalItems						+  decPromotionalItems,
					CreditSalesTax						=  CreditSalesTax						+  decCreditSalesTax
	WHERE BranchID = intBranchID AND TerminalNo = strTerminalNo;
	
	
END;
GO
delimiter ;

/********************************************
	procTerminalReportIncrementBatchCounter
********************************************/

delimiter GO
DROP PROCEDURE IF EXISTS procTerminalReportIncrementBatchCounter
GO

create procedure procTerminalReportIncrementBatchCounter(IN strTerminalNo varchar(10))
BEGIN

	UPDATE tblTerminalReport SET 
				BatchCounter	=  BatchCounter	+  1
	WHERE TerminalNo = strTerminalNo;
	
	
END;
GO
delimiter ;

/********************************************
	procCashierReportUpdateTransactionSales
********************************************/

delimiter GO
DROP PROCEDURE IF EXISTS procCashierReportUpdateTransactionSales
GO

create procedure procCashierReportUpdateTransactionSales(IN intBranchID INT(4), IN strTerminalNo varchar(10), IN lngCashierID int(10),
														IN decGrossSales decimal(10,2),
														IN decTotalDiscount decimal(10,2),
														IN decTotalCharge decimal(10,2),
														IN decDailySales decimal(10,2),
														IN decQuantitySold decimal(10,2),
														IN decGroupSales decimal(10,2),
														IN decVAT decimal(10,2),
														IN decLocalTax decimal(10,2),
														IN decCashSales decimal(10,2),
														IN decChequeSales decimal(10,2),
														IN decCreditCardSales decimal(10,2),
														IN decCreditSales decimal(10,2),
														IN decCreditPayment decimal(10,2),
														IN decDebitPayment decimal(10,2),
														IN decRewardPointsPayment decimal(10,2),
														IN decRewardConvertedPayment decimal(10,2),
														IN decCashInDrawer decimal(10,2),
														IN decVoidSales decimal(10,2),
														IN decRefundSales decimal(10,2),
														IN decItemsDiscount decimal(10,2),
														IN decSubTotalDiscount decimal(10,2),
														IN intNoOfCashTransactions int(10),
														IN intNoOfChequeTransactions int(10),
														IN intNoOfCreditCardTransactions int(10),
														IN intNoOfCreditTransactions int(10),
														IN intNoOfCombinationPaymentTransactions int(10),
														IN intNoOfCreditPaymentTransactions int(10),
														IN intNoOfDebitPaymentTransactions int(10),
														IN intNoOfClosedTransactions int(10),
														IN intNoOfRefundTransactions int(10),
														IN intNoOfVoidTransactions int(10),
														IN intNoOfRewardPointsPayment int(10),
														IN intNoOfTotalTransactions int(10),
														IN intNoOfDiscountedTransactions int(10),
														IN decNegativeAdjustments decimal(10),
														IN intNoOfNegativeAdjustmentTransactions  int(10),
														IN decPromotionalItems decimal(10),
														IN decCreditSalesTax decimal(10))
BEGIN
	UPDATE tblCashierReport SET 
		GrossSales								=  GrossSales							+  decGrossSales, 
		TotalDiscount							=  TotalDiscount						+  decTotalDiscount, 
		TotalCharge								=  TotalCharge							+  decTotalCharge, 
		DailySales								=  DailySales							+  decDailySales, 
		QuantitySold							=  QuantitySold							+  decQuantitySold, 
		GroupSales								=  GroupSales							+  decGroupSales, 
		VAT										=  VAT									+  decVAT, 
		LocalTax								=  LocalTax								+  decLocalTax, 
		CashSales								=  CashSales							+  decCashSales, 
		ChequeSales								=  ChequeSales							+  decChequeSales, 
		CreditCardSales							=  CreditCardSales						+  decCreditCardSales, 
		CreditSales								=  CreditSales							+  decCreditSales, 
		CreditPayment							=  CreditPayment						+  decCreditPayment, 
		DebitPayment							=  DebitPayment						   	+  decDebitPayment, 
		RewardPointsPayment						=  RewardPointsPayment					+  decRewardPointsPayment, 
		RewardConvertedPayment					=  RewardConvertedPayment				+  decRewardConvertedPayment, 
		CashInDrawer							=  CashInDrawer							+  decCashInDrawer, 
		VoidSales								=  VoidSales							+  decVoidSales, 
		RefundSales								=  RefundSales							+  decRefundSales, 
		ItemsDiscount							=  ItemsDiscount						+  decItemsDiscount, 
		SubTotalDiscount						=  SubTotalDiscount						+  decSubTotalDiscount, 
		NoOfCashTransactions					=  NoOfCashTransactions					+  intNoOfCashTransactions, 
		NoOfChequeTransactions					=  NoOfChequeTransactions				+  intNoOfChequeTransactions, 
		NoOfCreditCardTransactions				=  NoOfCreditCardTransactions			+  intNoOfCreditCardTransactions, 
		NoOfCreditTransactions					=  NoOfCreditTransactions				+  intNoOfCreditTransactions, 
		NoOfCombinationPaymentTransactions		=  NoOfCombinationPaymentTransactions	+  intNoOfCombinationPaymentTransactions, 
		NoOfCreditPaymentTransactions			=  NoOfCreditPaymentTransactions		+  intNoOfCreditPaymentTransactions, 
		NoOfDebitPaymentTransactions			=  NoOfDebitPaymentTransactions			+  intNoOfDebitPaymentTransactions, 
		NoOfClosedTransactions					=  NoOfClosedTransactions				+  intNoOfClosedTransactions, 
		NoOfRefundTransactions					=  NoOfRefundTransactions				+  intNoOfRefundTransactions, 			
		NoOfVoidTransactions					=  NoOfVoidTransactions					+  intNoOfVoidTransactions, 
		NoOfRewardPointsPayment					=  NoOfRewardPointsPayment				+  intNoOfRewardPointsPayment,
		NoOfTotalTransactions					=  NoOfTotalTransactions				+  intNoOfTotalTransactions,
		NoOfDiscountedTransactions				=  NoOfDiscountedTransactions			+  intNoOfDiscountedTransactions,
		NegativeAdjustments						=  NegativeAdjustments					+  decNegativeAdjustments,
		NoOfNegativeAdjustmentTransactions		=  NoOfNegativeAdjustmentTransactions	+  intNoOfNegativeAdjustmentTransactions,
		PromotionalItems						=  PromotionalItems						+  decPromotionalItems,
		CreditSalesTax							=  CreditSalesTax						+  decCreditSalesTax
	WHERE BranchID = intBranchID AND TerminalNo = strTerminalNo AND CashierID = lngCashierID;
	
	
END;
GO
delimiter ;

/**************************************************************

	procSyncQuantityProductHistory
	Lemuel E. Aceron
	March 14, 2009

	CALL procSyncQuantityProductHistory();

**************************************************************/
delimiter GO
DROP PROCEDURE IF EXISTS procSyncQuantityProductHistory
GO

create procedure procSyncQuantityProductHistory()
BEGIN
	DECLARE strSessionID varchar(30);
	set strSessionID = '1';
	
	DROP TABLE IF EXISTS tblProductHistoryAll;
	
	CREATE TABLE tblProductHistoryAll (
		`SessionID` VARCHAR(30) NOT NULL,
		`HistoryID` BIGINT NOT NULL DEFAULT 0,
		`ProductID` BIGINT NOT NULL DEFAULT 0,
		`MatrixID` BIGINT NOT NULL DEFAULT 0,
		`MatrixDescription` VARCHAR(100) NOT NULL,
		`Quantity` DECIMAL(18,2) NOT NULL DEFAULT 0,
		`UnitCode` VARCHAR(100) NOT NULL,
		`Remarks` VARCHAR(100) NOT NULL,
		`TransactionDate` DateTime NOT NULL,
		`TransactionNo` VARCHAR(100) NOT NULL,
		INDEX `IX_tblProductHistory`(`SessionID`),
		INDEX `IX_tblProductHistory1`(`MatrixDescription`)
	);

	INSERT INTO tblProductHistoryAll
	SELECT strSessionID, StockItemID, a.ProductID, a.VariationMatrixID, 
		IFNULL(c.Description, b.ProductCode) 'MatrixDescription',
		CASE StockDirection
			WHEN 0 THEN a.Quantity
			WHEN 1 THEN -a.Quantity
		END AS Quantity,
		d.UnitCode,
		CONCAT(e.Description, ':' , a.Remarks) AS Remarks,
		a.StockDate AS TransactionDate,
		TransactionNo
	FROM (((tblStockItems a
	INNER JOIN tblStock f ON a.StockID = f.StockID
	LEFT OUTER JOIN tblProducts b ON a.ProductID = b.ProductID)
	LEFT OUTER JOIN tblProductBaseVariationsMatrix c ON a.VariationMatrixID = c.MatrixID)
	LEFT OUTER JOIN tblUnit d ON a.ProductUnitID = d.UnitID)
	LEFT OUTER JOIN tblStockType e ON a.StockTypeID = e.StockTypeID;
	
	SELECT 'Inserting StockItems';
	
	INSERT INTO tblProductHistoryAll
	SELECT strSessionID, TransactionItemsID, a.ProductID, a.VariationsMatrixID, 
		IFNULL(NULLIF(MatrixDescription,''), ProductCode) AS MatrixDescription,
		CASE TransactionItemStatus
			WHEN 0 THEN -Quantity
			WHEN 1 THEN 0
			WHEN 2 THEN 0
			WHEN 3 THEN Quantity
			WHEN 4 THEN -Quantity
		END AS Quantity,
		ProductUnitCode as UnitCode,
		CASE TransactionItemStatus
			WHEN 0 THEN 'Sold'
			WHEN 1 THEN 'Void'
			WHEN 2 THEN 'Trash'
			WHEN 3 THEN 'Return'
			WHEN 4 THEN 'Refund'
		END AS Remarks,
		b.TransactionDate, b.TransactionNo
	FROM  tblTransactionItems a INNER JOIN tblTransactions b ON a.TransactionID = b.TransactionID;

	/***************************************Added July 10, 2009*****************************************************/
	INSERT INTO tblProductHistoryAll
	SELECT strSessionID, a.POID, a.ProductID, a.VariationMatrixID, 
		IFNULL(a.MatrixDescription, a.ProductCode) 'MatrixDescription',
		Quantity,
		a.ProductUnitCode as UnitCode,
		'Purchase Order' AS Remarks,
		b.PODate AS TransactionDate,
		b.PONo AS TransactionNo
	FROM tblPOItems a
	INNER JOIN tblPO b ON a.POID = b.POID
	WHERE b.Status = 1;
		
	/***************************************Added July 13, 2009*****************************************************/
	INSERT INTO tblProductHistoryAll
	SELECT strSessionID, InvAdjustmentID, a.ProductID, a.VariationMatrixID, 
		IFNULL(a.MatrixDescription, a.ProductCode) 'MatrixDescription',
		(QuantityNow - QuantityBefore) AS Quantity,
		a.UnitCode,
		'Inventory Adjustment' AS Remarks,
		InvAdjustmentDate AS TransactionDate,
		CONCAT('Adjusted By :' , b.Name) AS TransactionNo
	FROM tblInvAdjustment a
	INNER JOIN sysAccessUserDetails b ON a.UID = b.UID;
		
	/***************************************Added July 20, 2009*****************************************************/
	INSERT INTO tblProductHistoryAll
	SELECT strSessionID, a.TransferInID, a.ProductID, a.VariationMatrixID, 
		IFNULL(a.MatrixDescription, a.ProductCode) 'MatrixDescription',
		Quantity,
		a.ProductUnitCode as UnitCode,
		'Transfer In' AS Remarks,
		b.TransferInDate AS TransactionDate,
		b.TransferInNo AS TransactionNo
	FROM tblTransferInItems a
	INNER JOIN tblTransferIn b ON a.TransferInID = b.TransferInID
	WHERE b.Status = 1;
		
	/***************************************Added July 20, 2009*****************************************************/
	INSERT INTO tblProductHistoryAll
	SELECT strSessionID, a.TransferOutID, a.ProductID, a.VariationMatrixID, 
		IFNULL(a.MatrixDescription, a.ProductCode) 'MatrixDescription',
		-Quantity,
		a.ProductUnitCode as UnitCode,
		'Transfer Out' AS Remarks,
		b.TransferOutDate AS TransactionDate,
		b.TransferOutNo AS TransactionNo
	FROM tblTransferOutItems a
	INNER JOIN tblTransferOut b ON a.TransferOutID = b.TransferOutID
	WHERE b.Status = 1;
	
	UPDATE tblProductBaseVariationsMatrix SET Quantity = (SELECT SUM(Quantity) FROM tblProductHistoryAll WHERE tblProductHistoryAll.MatrixID = tblProductBaseVariationsMatrix.MatrixID);
		
	UPDATE tblProducts SET Quantity = (SELECT SUM(Quantity) FROM tblProductHistoryAll WHERE tblProductHistoryAll.ProductID = tblProducts.ProductID);

	SELECT N'DONE level1 Synching';
	
	
END;
GO
delimiter ;

/**************************************************************

	procSyncQuantityProductHistoryAdjust
	Lemuel E. Aceron
	July 20, 2009

	CALL procSyncQuantityProductHistoryAdjust();

	Description: make an automatic adjustments for products Not in the current Variations List;
**************************************************************/
delimiter GO
DROP PROCEDURE IF EXISTS procSyncQuantityProductHistoryAdjust
GO

create procedure procSyncQuantityProductHistoryAdjust()
BEGIN	
	DECLARE lngProductID BIGINT DEFAULT 0;
	DECLARE decProductQuantity, decMatrixQuantity, decMinThreshold, decMaxThreshold DECIMAL(18,3);
	DECLARE strProductCode, strUnitCode VARCHAR(30);
	DECLARE strDescription VARCHAR(150);
	DECLARE intUnitID INT DEFAULT 0;
	DECLARE done INT DEFAULT 0;
	DECLARE curItems CURSOR FOR SELECT ProductID, Quantity, ProductCode, ProductDesc, a.BaseUnitID, UnitCode, MinThreshold, MaxThreshold FROM tblProducts a INNER JOIN tblUnit b ON a.BaseUnitID = b.UnitID
									WHERE Quantity <> (SELECT SUM(Quantity) FROM tblProductBaseVariationsMatrix b WHERE b.Deleted = 0 AND a.ProductID = b.ProductID);
	DECLARE CONTINUE HANDLER FOR NOT FOUND SET done = 1;
	
	OPEN curItems;
	REPEAT
		FETCH curItems INTO lngProductID, decProductQuantity, strProductCode, strDescription, intUnitID, strUnitCode, decMinThreshold, decMaxThreshold;
		
		IF NOT done THEN
			SET decMatrixQuantity = (SELECT SUM(Quantity) FROM tblProductBaseVariationsMatrix WHERE Deleted = 0 AND ProductID = lngProductID);
			
			-- CALL procInvAdjustmentInsert (1, now(), lngProductID, strProductCode, strDescription, 0, 0, intUnitID, strUnitCode, 
			--								decProductQuantity, decMatrixQuantity, decMinThreshold, decMinThreshold, decMaxThreshold, decMaxThreshold, 'System added during auto-sync.');
			
		END IF;
	UNTIL done END REPEAT;
	CLOSE curItems;
	
	SELECT 'DONE level2 Synching';
	
END;
GO
delimiter ;

/**************************************************************

	--UPDATE tblProducts SET Quantity = (SELECT SUM(Quantity) FROM tblProductBaseVariationsMatrix WHERE tblProductBaseVariationsMatrix.ProductID = tblProducts.ProductID);

	procGenerateProductHistory
	Lemuel E. Aceron
	2009-03-14 : created procedure
	2009-07-10 : included Purchase Orders
	2009-07-13 : included inventory adjustment in product history.
	2009-07-20 : included transferin in product history.
	2009-07-20 : included transferout in product history.
	
	CALL procGenerateProductHistory('1', null, null, 2 );
**************************************************************/
delimiter GO
DROP PROCEDURE IF EXISTS procGenerateProductHistory
GO

create procedure procGenerateProductHistory(
	IN strSessionID varchar(30),
	IN dteStartTransactionDate DateTime,
	IN dteEndTransactionDate DateTime,
	IN lngProductID BIGINT
	)
BEGIN
	
	SET dteStartTransactionDate = IF(NOT ISNULL(dteStartTransactionDate), dteStartTransactionDate, '0001-01-01');
	SET dteEndTransactionDate = IF(NOT ISNULL(dteEndTransactionDate), dteEndTransactionDate, now());
	
	INSERT INTO tblProductHistory
	SELECT strSessionID, StockItemID, a.ProductID, a.VariationMatrixID, 
		IFNULL(c.Description, b.ProductCode) 'MatrixDescription',
		CASE StockDirection
			WHEN 0 THEN a.Quantity
			WHEN 1 THEN -a.Quantity
		END AS Quantity,
		d.UnitCode,
		CONCAT(e.Description, ':' , a.Remarks) AS Remarks,
		a.StockDate AS TransactionDate,
		TransactionNo
	FROM (((tblStockItems a
	INNER JOIN tblStock f ON a.StockID = f.StockID
	LEFT OUTER JOIN tblProducts b ON a.ProductID = b.ProductID)
	LEFT OUTER JOIN tblProductBaseVariationsMatrix c ON a.VariationMatrixID = c.MatrixID)
	LEFT OUTER JOIN tblUnit d ON a.ProductUnitID = d.UnitID)
	LEFT OUTER JOIN tblStockType e ON a.StockTypeID = e.StockTypeID
	WHERE a.ProductID = lngProductID
		AND DATE_FORMAT(a.StockDate, '%Y-%m-%d %H:%i') >= DATE_FORMAT(dteStartTransactionDate, '%Y-%m-%d %H:%i')
		AND DATE_FORMAT(a.StockDate, '%Y-%m-%d %H:%i') <= DATE_FORMAT(dteEndTransactionDate, '%Y-%m-%d %H:%i');

	INSERT INTO tblProductHistory
	SELECT strSessionID, TransactionItemsID, a.ProductID, a.VariationsMatrixID, 
		IFNULL(NULLIF(MatrixDescription,''), ProductCode) AS MatrixDescription,
		CASE TransactionItemStatus
			WHEN 0 THEN -Quantity
			WHEN 1 THEN 0
			WHEN 2 THEN 0
			WHEN 3 THEN Quantity
			WHEN 4 THEN -Quantity
		END AS Quantity,
		ProductUnitCode as UnitCode,
		CASE TransactionItemStatus
			WHEN 0 THEN CONCAT('Sold @ ',a.Price, '. Price: ',a.PurchasePrice,' /',a.ProductUnitCode, ' to ', CustomerName)
			WHEN 1 THEN 'Void'
			WHEN 2 THEN 'Trash'
			WHEN 3 THEN 'Return'
			WHEN 4 THEN 'Refund'
		END AS Remarks,
		b.TransactionDate, b.TransactionNo
	FROM  tblTransactionItems a INNER JOIN tblTransactions b ON a.TransactionID = b.TransactionID
	WHERE a.ProductID = lngProductID 
		AND DATE_FORMAT(b.TransactionDate, '%Y-%m-%d %H:%i') >= DATE_FORMAT(dteStartTransactionDate, '%Y-%m-%d %H:%i')
		AND DATE_FORMAT(b.TransactionDate, '%Y-%m-%d %H:%i') <= DATE_FORMAT(dteEndTransactionDate, '%Y-%m-%d %H:%i');

	/***************************************Added July 10, 2009*****************************************************/
	INSERT INTO tblProductHistory
	SELECT strSessionID, a.POID, a.ProductID, a.VariationMatrixID, 
		IFNULL(a.MatrixDescription, a.ProductCode) 'MatrixDescription',
		Quantity,
		a.ProductUnitCode as UnitCode,
		CONCAT('Purchase Order from ',SupplierCode) AS Remarks,
		b.PODate AS TransactionDate,
		b.PONo AS TransactionNo
	FROM tblPOItems a
	INNER JOIN tblPO b ON a.POID = b.POID
	WHERE a.ProductID = lngProductID
		AND DATE_FORMAT(b.PODate, '%Y-%m-%d %H:%i') >= DATE_FORMAT(dteStartTransactionDate, '%Y-%m-%d %H:%i')
		AND DATE_FORMAT(b.PODate, '%Y-%m-%d %H:%i') <= DATE_FORMAT(dteEndTransactionDate, '%Y-%m-%d %H:%i')
		AND b.Status = 1;
		
	/***************************************Added July 13, 2009*****************************************************/
	INSERT INTO tblProductHistory
	SELECT strSessionID, InvAdjustmentID, a.ProductID, a.VariationMatrixID, 
		IFNULL(a.MatrixDescription, a.ProductCode) 'MatrixDescription',
		(QuantityNow - QuantityBefore) AS Quantity,
		a.UnitCode,
		CONCAT('Inventory Adjustment : ' , Remarks, ' from ', QuantityBefore, ' to ', QuantityNow ) Remarks,
		InvAdjustmentDate AS TransactionDate,
		CONCAT('Adjusted By :' , b.Name) AS TransactionNo
	FROM tblInvAdjustment a
	INNER JOIN sysAccessUserDetails b ON a.UID = b.UID
	WHERE a.ProductID = lngProductID
		AND DATE_FORMAT(a.InvAdjustmentDate, '%Y-%m-%d %H:%i') >= DATE_FORMAT(dteStartTransactionDate, '%Y-%m-%d %H:%i')
		AND DATE_FORMAT(a.InvAdjustmentDate, '%Y-%m-%d %H:%i') <= DATE_FORMAT(dteEndTransactionDate, '%Y-%m-%d %H:%i');
		
	/***************************************Added July 20, 2009*****************************************************/
	INSERT INTO tblProductHistory
	SELECT strSessionID, a.TransferInID, a.ProductID, a.VariationMatrixID, 
		IFNULL(a.MatrixDescription, a.ProductCode) 'MatrixDescription',
		Quantity,
		a.ProductUnitCode as UnitCode,
		CONCAT('Transfer In from ',SupplierCode) AS Remarks,
		b.TransferInDate AS TransactionDate,
		b.TransferInNo AS TransactionNo
	FROM tblTransferInItems a
	INNER JOIN tblTransferIn b ON a.TransferInID = b.TransferInID
	WHERE a.ProductID = lngProductID
		AND DATE_FORMAT(b.TransferInDate, '%Y-%m-%d %H:%i') >= DATE_FORMAT(dteStartTransactionDate, '%Y-%m-%d %H:%i')
		AND DATE_FORMAT(b.TransferInDate, '%Y-%m-%d %H:%i') <= DATE_FORMAT(dteEndTransactionDate, '%Y-%m-%d %H:%i')
		AND b.Status = 1;
		
	/***************************************Added July 20, 2009*****************************************************/
	INSERT INTO tblProductHistory
	SELECT strSessionID, a.TransferOutID, a.ProductID, a.VariationMatrixID, 
		IFNULL(a.MatrixDescription, a.ProductCode) 'MatrixDescription',
		-Quantity,
		a.ProductUnitCode as UnitCode,
		CONCAT('Transfer out to ',SupplierCode) AS Remarks,
		b.TransferOutDate AS TransactionDate,
		b.TransferOutNo AS TransactionNo
	FROM tblTransferOutItems a
	INNER JOIN tblTransferOut b ON a.TransferOutID = b.TransferOutID
	WHERE a.ProductID = lngProductID
		AND DATE_FORMAT(b.TransferOutDate, '%Y-%m-%d %H:%i') >= DATE_FORMAT(dteStartTransactionDate, '%Y-%m-%d %H:%i')
		AND DATE_FORMAT(b.TransferOutDate, '%Y-%m-%d %H:%i') <= DATE_FORMAT(dteEndTransactionDate, '%Y-%m-%d %H:%i')
		AND b.Status = 1;
		
	

END;
GO
delimiter ;

/*********************************
	procTerminalVersionUpdate
	Lemuel E. Aceron
	
	April 22, 2009 - create this procedure
*********************************/
DROP PROCEDURE IF EXISTS procTerminalVersionUpdate;
delimiter GO

create procedure procTerminalVersionUpdate(IN intBranchID INT(4), IN strTerminalNo varchar(10), IN strVersion varchar(25))
BEGIN
	
	UPDATE tblTerminal SET
		FEVersion = strVersion
	WHERE BranchID = intBranchID AND TerminalNo = strTerminalNo;
	
END;
GO
delimiter ;

/*********************************
	procUpdateTerminalReportBatchCounter
	Lemuel E. Aceron
	CALL procUpdateTerminalReportBatchCounter();
	
	April 19, 2009 - create this procedure
*********************************/
delimiter GO
DROP PROCEDURE IF EXISTS procUpdateTerminalReportBatchCounter
GO

create procedure procUpdateTerminalReportBatchCounter(IN pvtTerminalNo varchar(10), IN pvtDateLastInitialized DATETIME)
BEGIN
	
	UPDATE tblTerminalReportHistory SET BatchCounter = BatchCounter + 1 WHERE TerminalNo = pvtTerminalNo AND DateLastInitialized = pvtDateLastInitialized;
		
END;
GO
delimiter ;
 
/*********************************
	procTransactionOrderTypeUpdate
	Lemuel E. Aceron
	
	May 1, 2009 - create this procedure
	
*********************************/
DROP PROCEDURE IF EXISTS procTransactionOrderTypeUpdate;
delimiter GO

create procedure procTransactionOrderTypeUpdate(IN lngTransactionID bigint(20), IN intOrderType smallint)
BEGIN

	UPDATE tblTransactions SET OrderType = intOrderType WHERE TransactionID = lngTransactionID;
	
END;
GO
delimiter ;

/*********************************
	procTransactionWaiterUpdate
	Lemuel E. Aceron
	May 1, 2009

	SET @SQL := CONCAT('UPDATE tblTransactions',strMonth,' SET WaiterID = ',lngWaiterID,', WaiterName = ''',strWaiterName,''' WHERE TransactionID = ',lngTransactionID,';');
	
	PREPARE stmt FROM @SQL;
	EXECUTE stmt;
	DEALLOCATE PREPARE stmt;
	
*********************************/
DROP PROCEDURE IF EXISTS procTransactionWaiterUpdate;
delimiter GO

create procedure procTransactionWaiterUpdate(IN lngTransactionID bigint(20), IN lngWaiterID BIGINT(20), IN strWaiterName varchar(100))
BEGIN
	
	UPDATE tblTransactions SET WaiterID = lngWaiterID, WaiterName = strWaiterName WHERE TransactionID = lngTransactionID;

END;
GO
delimiter ;

/*********************************
	procTransactionContactUpdate
	Lemuel E. Aceron
	May 1, 2009
*********************************/
DROP PROCEDURE IF EXISTS procTransactionContactUpdate;
delimiter GO

create procedure procTransactionContactUpdate(IN lngTransactionID bigint(20), IN lngCustomerID BIGINT(20), IN strCustomerName varchar(100))
BEGIN
	
	UPDATE tblTransactions SET CustomerID = lngCustomerID, CustomerName = strCustomerName WHERE TransactionID = lngTransactionID;

END;
GO
delimiter ;

/*********************************
	procTransactionTerminalNoUpdate
	Lemuel E. Aceron
	May 1, 2009
*********************************/
DROP PROCEDURE IF EXISTS procTransactionTerminalNoUpdate;
delimiter GO

create procedure procTransactionTerminalNoUpdate(IN lngTransactionID bigint(20), IN strTerminalNo varchar(30))
BEGIN
	
	UPDATE tblTransactions SET TerminalNo = strTerminalNo WHERE TransactionID = lngTransactionID;

END;
GO
delimiter ;


/*********************************
	procGenerateDiscountByTerminalNo
	Lemuel E. Aceron
	May 5, 2009
	CALL procGenerateDiscountByTerminalNo('1', '01', '00000000000001, '00000000000006' );
*********************************/
delimiter GO
DROP PROCEDURE IF EXISTS procGenerateDiscountByTerminalNo
GO

create procedure procGenerateDiscountByTerminalNo(
	IN strSessionID varchar(30),
	IN strTerminalNo varchar(30),
	IN strTransactionNoFrom varchar(30),
	IN strTransactionNoTo varchar(30)
	)
BEGIN
	
	INSERT INTO tblDiscountHistory
	SELECT strSessionID, strTerminalNo,
			DiscountCode, COUNT(DiscountCode) AS DiscountCount, SUM(Discount) AS Discount
	FROM  tblTransactions WHERE TerminalNo = strTerminalNo
		AND TransactionNo >= strTransactionNoFrom AND TransactionNo < strTransactionNoTo
	GROUP BY DiscountCode;
	
END;
GO
delimiter ;

/*********************************
	procDeleteDiscountHistory
	Lemuel E. Aceron
	CALL procDeleteDiscountHistory('1');
	
	May 5, 2009 - create this procedure

*********************************/
delimiter GO
DROP PROCEDURE IF EXISTS procDeleteDiscountHistory
GO

create procedure procDeleteDiscountHistory(
	IN strSessionID varchar(30) 
	)
BEGIN
	
	DELETE FROM tblDiscountHistory WHERE SessionID = strSessionID;
	
END;
GO
delimiter ;

/*********************************
	procGenerateDiscountByTerminalNoByCashier
	Lemuel E. Aceron
	CALL procGenerateDiscountByTerminalNoByCashier('1', '01', 1, '00000000000001, '00000000000006' );
	
	May 5, 2009 - create this procedure
	
*********************************/
delimiter GO
DROP PROCEDURE IF EXISTS procGenerateDiscountByTerminalNoByCashier
GO

create procedure procGenerateDiscountByTerminalNoByCashier(
	IN strSessionID varchar(30),
	IN strTerminalNo varchar(30),
	IN lngCashierID bigint(20),
	IN strTransactionNoFrom varchar(30),
	IN strTransactionNoTo varchar(30)
	)
BEGIN
	
	INSERT INTO tblDiscountHistory
	SELECT strSessionID, strTerminalNo,
			DiscountCode, COUNT(DiscountCode) AS DiscountCount, SUM(Discount) AS Discount
	FROM  tblTransactions WHERE TerminalNo = strTerminalNo AND CashierID = lngCashierID
		AND TransactionNo >= strTransactionNoFrom AND TransactionNo < strTransactionNoTo
	GROUP BY DiscountCode;
	
END;
GO
delimiter ;


/*********************************
	procTerminalReportInitializeZRead
	Lemuel E. Aceron
	
	May 5, 2009 - insert the ff items in tblTerminalReportHistory
					NoOfDiscountedTransactions, NegativeAdjustments, NoOfNegativeAdjustmentTransactions,
					PromotionalItems, CreditSalesTax, BatchCounter
					
	May 5, 2009 - insert the ff items in tblTerminalReportHistory
					NoOfDiscountedTransactions, NegativeAdjustments, NoOfNegativeAdjustmentTransactions,
					PromotionalItems, CreditSalesTax

*********************************/
delimiter GO
DROP PROCEDURE IF EXISTS procTerminalReportInitializeZRead
GO

create procedure procTerminalReportInitializeZRead(IN intBranchID int(4), IN strTerminalNo varchar(10), IN dteDateLastInitialized DateTime, IN strInitializedBy varchar(150))
BEGIN
	DECLARE decTrustFund DECIMAL(18,3) DEFAULT 0;
	
	SET decTrustFund = (SELECT TRUSTFUND FROM tblTerminal WHERE BranchID = intBranchID AND TerminalNo = strTerminalNo);
	
	INSERT INTO tblTerminalReportHistory (
					BranchID, TerminalID, TerminalNo, BeginningTransactionNo, EndingTransactionNo, ZReadCount, 
					XReadCount, GrossSales, TotalDiscount, TotalCharge, DailySales, 
					QuantitySold, GroupSales, OldGrandTotal, NewGrandTotal, VATableAmount, 
					NonVATableAmount, VAT, EVATableAmount, NonEVATableAmount, EVAT, LocalTax, CashSales, 
					ChequeSales, CreditCardSales, CreditSales, CreditPayment, DebitPayment, RewardPointsPayment, RewardConvertedPayment, CashInDrawer, 
					TotalDisburse, CashDisburse, ChequeDisburse, CreditCardDisburse, 
					TotalWithhold, CashWithhold, ChequeWithhold, CreditCardWithhold, 
					TotalPaidOut, CashPaidOut, ChequePaidOut, CreditCardPaidOut, 
					TotalDeposit, CashDeposit, ChequeDeposit, CreditCardDeposit, DebitDeposit,
					BeginningBalance, VoidSales, RefundSales, ItemsDiscount, SubtotalDiscount, 
					NoOfCashTransactions, NoOfChequeTransactions, NoOfCreditCardTransactions, 
					NoOfCreditTransactions, NoOfCombinationPaymentTransactions, 
					NoOfCreditPaymentTransactions, NoOfDebitPaymentTransactions, 
					NoOfClosedTransactions, NoOfRefundTransactions, 
					NoOfVoidTransactions, NoOfRewardPointsPayment, NoOfTotalTransactions, 
					DateLastInitialized, TrustFund, 
					NoOfDiscountedTransactions, NegativeAdjustments, NoOfNegativeAdjustmentTransactions,
					PromotionalItems, CreditSalesTax, BatchCounter, InitializedBy) 
				(SELECT 
					BranchID, TerminalID, TerminalNo, BeginningTransactionNo, EndingTransactionNo, ZReadCount, 
					XReadCount, GrossSales, TotalDiscount, TotalCharge, DailySales, 
					QuantitySold, GroupSales, OldGrandTotal, NewGrandTotal, VATableAmount, 
					NonVATableAmount, VAT, EVATableAmount, NonEVATableAmount, EVAT, LocalTax, CashSales, 
					ChequeSales, CreditCardSales, CreditSales, CreditPayment, DebitPayment, RewardPointsPayment, RewardConvertedPayment, CashInDrawer, 
					TotalDisburse, CashDisburse, ChequeDisburse, CreditCardDisburse, 
					TotalWithhold, CashWithhold, ChequeWithhold, CreditCardWithhold, 
					TotalPaidOut, CashPaidOut, ChequePaidOut, CreditCardPaidOut, 
					TotalDeposit, CashDeposit, ChequeDeposit, CreditCardDeposit, DebitDeposit,
					BeginningBalance, VoidSales, RefundSales, ItemsDiscount, SubtotalDiscount, 
					NoOfCashTransactions, NoOfChequeTransactions, NoOfCreditCardTransactions, 
					NoOfCreditTransactions, NoOfCombinationPaymentTransactions, 
					NoOfCreditPaymentTransactions, NoOfDebitPaymentTransactions, 
					NoOfClosedTransactions, NoOfRefundTransactions, 
					NoOfVoidTransactions, NoOfRewardPointsPayment, NoOfTotalTransactions, 
					DateLastInitialized, decTrustFund,
					NoOfDiscountedTransactions, NegativeAdjustments, NoOfNegativeAdjustmentTransactions,
					PromotionalItems, CreditSalesTax, BatchCounter, strInitializedBy FROM tblTerminalReport WHERE BranchID = intBranchID AND TerminalNo = strTerminalNo);
					
	UPDATE tblTerminalReport SET OldGrandTotal =  NewGrandTotal WHERE TerminalNo = strTerminalNo;
	
	UPDATE tblTerminalReport SET 
					BeginningTransactionNo				=  EndingTransactionNo, 
					GrossSales							=  0, 
					TotalDiscount						=  0, 
					TotalCharge							=  0, 
					DailySales							=  0, 
					QuantitySold						=  0, 
					GroupSales							=  0, 
					VATableAmount						=  0, 
					NonVaTableAmount					=  0, 
					VAT									=  0, 
					EVATableAmount						=  0, 
					NonEVaTableAmount					=  0, 
					EVAT								=  0, 
					LocalTax							=  0, 
					CashSales							=  0, 
					ChequeSales							=  0, 
					CreditCardSales						=  0, 
					CreditSales							=  0, 
					CreditPayment						=  0, 
					DebitPayment						=  0, 
					RewardPointsPayment					=  0,
					RewardConvertedPayment				=  0,
					CashInDrawer						=  0, 
					TotalDisburse						=  0, 
					CashDisburse						=  0, 
					ChequeDisburse						=  0, 
					CreditCardDisburse					=  0, 
					TotalWithhold						=  0, 
					CashWithhold						=  0, 
					ChequeWithhold						=  0, 
					CreditCardWithhold					=  0, 
					TotalPaidOut						=  0, 
					CashPaidOut							=  0,
					ChequePaidOut						=  0,
					CreditCardPaidOut					=  0,
					TotalDeposit						=  0, 
					CashDeposit							=  0, 
					ChequeDeposit						=  0, 
					CreditCardDeposit					=  0, 
					DebitDeposit						=  0, 
					BeginningBalance					=  0, 
					VoidSales							=  0, 
					RefundSales							=  0, 
					ItemsDiscount						=  0, 
					SubTotalDiscount					=  0, 
					NoOfCashTransactions				=  0, 
					NoOfChequeTransactions				=  0, 
					NoOfCreditCardTransactions			=  0, 
					NoOfCreditTransactions				=  0, 
					NoOfCombinationPaymentTransactions	=  0, 
					NoOfCreditPaymentTransactions		=  0, 
					NoOfDebitPaymentTransactions		=  0, 
					NoOfClosedTransactions				=  0, 
					NoOfRefundTransactions				=  0, 
					NoOfVoidTransactions				=  0, 
					NoOfRewardPointsPayment				=  0,
					NoOfTotalTransactions				=  0, 
					NoOfDiscountedTransactions			=  0,
					NegativeAdjustments					=  0,
					NoOfNegativeAdjustmentTransactions	=  0,
					PromotionalItems					=  0,
					CreditSalesTax						=  0,
					BatchCounter						=  1,
					DateLastInitialized					=  dteDateLastInitialized
			WHERE BranchID = intBranchID AND TerminalNo = strTerminalNo;
			
	
	INSERT INTO tblCashierReportHistory (
					CashierID, BranchID, TerminalID, TerminalNo, GrossSales, 
					TotalDiscount, TotalCharge, DailySales, 
					QuantitySold, GroupSales, VAT, LocalTax, 
					CashSales, ChequeSales, CreditCardSales, CreditSales, 
					CreditPayment, DebitPayment, RewardPointsPayment, RewardConvertedPayment, CashInDrawer, 
					TotalDisburse, CashDisburse, ChequeDisburse, CreditCardDisburse, 
					TotalWithhold, CashWithhold, ChequeWithhold, CreditCardWithhold, 
					TotalPaidOut, CashPaidOut, ChequePaidOut, CreditCardPaidOut, 
					TotalDeposit, CashDeposit, ChequeDeposit, CreditCardDeposit, DebitDeposit, 
					BeginningBalance, VoidSales, RefundSales, 
					ItemsDiscount, SubtotalDiscount, NoOfCashTransactions, NoOfChequeTransactions, 
					NoOfCreditCardTransactions, NoOfCreditTransactions, 
					NoOfCombinationPaymentTransactions, 
					NoOfCreditPaymentTransactions, NoOfDebitPaymentTransactions, 
					NoOfClosedTransactions, NoOfRefundTransactions, 
					NoOfVoidTransactions, NoOfRewardPointsPayment, NoOfTotalTransactions, 
					CashCount, LastLoginDate,
					NoOfDiscountedTransactions, NegativeAdjustments, NoOfNegativeAdjustmentTransactions,
					PromotionalItems, CreditSalesTax )
				(SELECT 
					CashierID, BranchID, TerminalID, TerminalNo, GrossSales, 
					TotalDiscount, TotalCharge, DailySales, 
					QuantitySold, GroupSales, VAT, LocalTax, 
					CashSales, ChequeSales, CreditCardSales, CreditSales, 
					CreditPayment, DebitPayment, RewardPointsPayment, RewardConvertedPayment, CashInDrawer, 
					TotalDisburse, CashDisburse, ChequeDisburse, CreditCardDisburse, 
					TotalWithhold, CashWithhold, ChequeWithhold, CreditCardWithhold, 
					TotalPaidOut, CashPaidOut, ChequePaidOut, CreditCardPaidOut, 
					TotalDeposit, CashDeposit, ChequeDeposit, CreditCardDeposit, DebitDeposit,
					BeginningBalance, VoidSales, RefundSales, 
					ItemsDiscount, SubtotalDiscount, NoOfCashTransactions, NoOfChequeTransactions, 
					NoOfCreditCardTransactions, NoOfCreditTransactions, 
					NoOfCombinationPaymentTransactions, 
					NoOfCreditPaymentTransactions, NoOfDebitPaymentTransactions, 
					NoOfClosedTransactions, NoOfRefundTransactions, 
					NoOfVoidTransactions, NoOfRewardPointsPayment, NoOfTotalTransactions, 
					CashCount, LastLoginDate,
					NoOfDiscountedTransactions, NegativeAdjustments, NoOfNegativeAdjustmentTransactions,
					PromotionalItems, CreditSalesTax FROM tblCashierReport WHERE BranchID = intBranchID AND TerminalNo = strTerminalNo);
					
	UPDATE tblCashierReport SET 
			GrossSales							=  0, 
			TotalDiscount						=  0, 
			TotalCharge							=  0, 
			DailySales							=  0, 
			QuantitySold						=  0, 
			GroupSales							=  0, 
			VAT									=  0, 
			LocalTax							=  0, 
			CashSales							=  0, 
			ChequeSales							=  0, 
			CreditCardSales						=  0, 
			CreditSales							=  0, 
			CreditPayment						=  0, 
			DebitPayment						=  0, 
			RewardPointsPayment					=  0,
			RewardConvertedPayment				=  0,
			CashInDrawer						=  0, 
			TotalDisburse						=  0, 
			CashDisburse						=  0, 
			ChequeDisburse						=  0, 
			CreditCardDisburse					=  0, 
			TotalWithhold						=  0, 
			CashWithhold						=  0, 
			ChequeWithhold						=  0, 
			CreditCardWithhold					=  0, 
			TotalPaidOut						=  0, 
			CashPaidOut							=  0,
			ChequePaidOut						=  0,
			CreditCardPaidOut					=  0,
			TotalDeposit						=  0, 
			CashDeposit							=  0, 
			ChequeDeposit						=  0, 
			CreditCardDeposit					=  0, 
			DebitDeposit						=  0, 
			BeginningBalance					=  0, 
			VoidSales							=  0, 
			RefundSales							=  0, 
			ItemsDiscount						=  0, 
			SubTotalDiscount					=  0, 
			NoOfCashTransactions				=  0, 
			NoOfChequeTransactions				=  0, 
			NoOfCreditCardTransactions			=  0, 
			NoOfCreditTransactions				=  0, 
			NoOfCombinationPaymentTransactions	=  0, 
			NoOfCreditPaymentTransactions		=  0, 
			NoOfDebitPaymentTransactions		=  0, 
			NoOfClosedTransactions				=  0, 
			NoOfRefundTransactions				=  0, 
			NoOfVoidTransactions				=  0, 
			NoOfRewardPointsPayment				=  0,
			NoOfTotalTransactions				=  0, 
			NoOfDiscountedTransactions			=  0,
			NegativeAdjustments					=  0,
			NoOfNegativeAdjustmentTransactions	=  0,
			PromotionalItems					=  0,
			CreditSalesTax						=  0,
			CashCount							=  0 
	WHERE BranchID = intBranchID AND TerminalNo = strTerminalNo;
	
	
END;
GO
delimiter ;

/********************************************
	procTerminalUpdate
	Lemuel E. Aceron
	
	May 06, 2009 : - Created this procedure
	Oct 17, 2011 : - Added ShowCustomerSelection, RewardPointsMinimum,
					 RewardPointsEvery, RewardPoints
					
********************************************/

delimiter GO
DROP PROCEDURE IF EXISTS procTerminalUpdate
GO

create procedure procTerminalUpdate(
	IN intBranchID INT(4),
	IN lngTerminalID BIGINT(20), 
	IN bolIsPrinterAutoCutter TINYINT(1),
	IN bolAutoPrint TINYINT(1),
	IN bolIsVATInclusive TINYINT(1),
	IN strPrinterName VARCHAR(20),
	IN strTurretName VARCHAR(20),
	IN strCashDrawerName VARCHAR(20),
	IN bolItemVoidConfirmation TINYINT(1),
	IN bolEnableEVAT TINYINT(1),
	IN intMaxReceiptWidth INT(10),
	IN strFORM_Behavior VARCHAR(20),
	IN strMarqueeMessage VARCHAR(255),
	IN strMachineSerialNo VARCHAR(20),
	IN strAccreditationNo VARCHAR(20),
	IN decVAT DECIMAL(18,3),
	IN decEVAT DECIMAL(18,3),
	IN decLocalTax DECIMAL(18,3),
	IN bolShowItemMoreThanZeroQty TINYINT(1),
	IN bolShowOnlyPackedTransactions TINYINT(1),
	IN bolShowOneTerminalSuspendedTransactions TINYINT(1),
	IN intTerminalReceiptType TINYINT(1),
	IN strSalesInvoicePrinterName VARCHAR(30),
	IN bolCashCountBeforeReport TINYINT(1),
	IN bolPreviewTerminalReport TINYINT(1),
	IN bolIsPrinterDotMatrix TINYINT(1),
	IN bolIsChargeEditable TINYINT(1),
	IN bolIsDiscountEditable TINYINT(1),
	IN bolCheckCutOffTime TINYINT(1),
	IN strStartCutOffTime VARCHAR(5),
	IN strEndCutOffTime VARCHAR(5),
	IN bolWithRestaurantFeatures TINYINT(1),
	IN strSeniorCitizenDiscountCode varchar(5),
	IN bolIsTouchScreen TINYINT(1),
	IN bolWillContinueSelectionVariation TINYINT(1),
	IN bolWillContinueSelectionProduct TINYINT(1),
	IN bolWillPrintGrandTotal TINYINT(1),
	IN bolReservedAndCommit TINYINT(1),
	IN bolShowCustomerSelection TINYINT(1)
	)
BEGIN

	UPDATE tblTerminal SET 
				BranchID				= intBranchID,
				IsPrinterAutoCutter		= bolIsPrinterAutoCutter,
				AutoPrint				= bolAutoPrint,
				IsVATInclusive			= bolIsVATInclusive,
				PrinterName				= strPrinterName,
				TurretName				= strTurretName,
				CashDrawerName			= strCashDrawerName,
				ItemVoidConfirmation	= bolItemVoidConfirmation,
				EnableEVAT				= bolEnableEVAT,
				MaxReceiptWidth			= intMaxReceiptWidth,
				FORM_Behavior			= strFORM_Behavior,
				MarqueeMessage			= strMarqueeMessage,
				MachineSerialNo			= strMachineSerialNo,
				AccreditationNo			= strAccreditationNo,
				VAT						= decVAT,
				EVAT					= decEVAT,
				LocalTax				= decLocalTax,
				ShowItemMoreThanZeroQty = bolShowItemMoreThanZeroQty,
				ShowOnlyPackedTransactions = bolShowOnlyPackedTransactions,
				ShowOneTerminalSuspendedTransactions = bolShowOneTerminalSuspendedTransactions,
				TerminalReceiptType		= intTerminalReceiptType,
				SalesInvoicePrinterName = strSalesInvoicePrinterName,
				CashCountBeforeReport	= bolCashCountBeforeReport,
				PreviewTerminalReport	= bolPreviewTerminalReport,
				IsPrinterDotMatrix		= bolIsPrinterDotMatrix,
				IsChargeEditable		= bolIsChargeEditable,
				IsDiscountEditable		= bolIsDiscountEditable,
				CheckCutOffTime			= bolCheckCutOffTime,
				StartCutOffTime			= strStartCutOffTime,
				EndCutOffTime			= strEndCutOffTime,
				WithRestaurantFeatures	= bolWithRestaurantFeatures,
				SeniorCitizenDiscountCode = strSeniorCitizenDiscountCode,
				IsTouchScreen			= bolIsTouchScreen,
				WillContinueSelectionVariation	= bolWillContinueSelectionVariation,
				WillContinueSelectionProduct	= bolWillContinueSelectionProduct,
				WillPrintGrandTotal		= bolWillPrintGrandTotal,
				ReservedAndCommit		= bolReservedAndCommit,
				ShowCustomerSelection	= bolShowCustomerSelection
	WHERE TerminalID = lngTerminalID;
	
	
END;
GO
delimiter ;


/********************************************
	procTerminalUpdateRewardPointSystem
	Lemuel E. Aceron
	
	Nov 4, 2011 : - Created this procedure
					
********************************************/

delimiter GO
DROP PROCEDURE IF EXISTS procTerminalUpdateRewardPointSystem
GO

create procedure procTerminalUpdateRewardPointSystem(
	IN bolEnableRewardPoints TINYINT(1),
	IN bolRoundDownRewardPoints TINYINT(1),
	IN decRewardPointsMinimum DECIMAL(18,3),
	IN decRewardPointsEvery DECIMAL(18,3),
	IN decRewardPoints DECIMAL(18,3),
	IN bolEnableRewardPointsAsPayment DECIMAL(18,3),
	IN decRewardPointsMaxPercentageForPayment DECIMAL(5,2),
	IN decRewardPointsPaymentValue DECIMAL(18,3),
	IN decRewardPointsPaymentCashEquivalent DECIMAL(18,3)
	)
BEGIN

	UPDATE tblTerminal SET 
		EnableRewardPoints		= bolEnableRewardPoints,
		RoundDownRewardPoints	= bolRoundDownRewardPoints,
		RewardPointsMinimum		= decRewardPointsMinimum,
		RewardPointsEvery		= decRewardPointsEvery,
		RewardPoints			= decRewardPoints,
		EnableRewardPointsAsPayment			= bolEnableRewardPointsAsPayment,
		RewardPointsMaxPercentageForPayment	= decRewardPointsMaxPercentageForPayment,
		RewardPointsPaymentValue			= decRewardPointsPaymentValue,
		RewardPointsPaymentCashEquivalent	= decRewardPointsPaymentCashEquivalent;
		
	
END;
GO
delimiter ;

/**************************************************************
	procGenerateSalesPerItemByGroup
	Lemuel E. Aceron
	CALL procGenerateSalesPerItemByGroup('1', null, null, null, null, null, null, null);
	
	May 15, 2009 - as requested by Malou of Baguio
**************************************************************/
delimiter GO
DROP PROCEDURE IF EXISTS procGenerateSalesPerItemByGroup
GO

create procedure procGenerateSalesPerItemByGroup(
	IN strSessionID varchar(30),
	IN strProductGroup varchar(20),
	IN strTransactionNo varchar(30),
	IN strCustomerName varchar(100),
	IN strCashierName varchar(100),
	IN strTerminalNo varchar(30),
	IN dteStartTransactionDate DateTime,
	IN dteEndTransactionDate DateTime
	)
BEGIN
	DECLARE intOpenTransactionStatus, intValidTransactionItemStatus, intReturnTransactionItemStatus, intRefundransactionItemStatus INTEGER DEFAULT 0;
	
	SET intOpenTransactionStatus = 0; 
	SET intValidTransactionItemStatus = 0;
	SET intReturnTransactionItemStatus = 3;
	SET intRefundransactionItemStatus = 4;
	
	SET strTransactionNo = IF(NOT ISNULL(strTransactionNo), CONCAT('%',strTransactionNo,'%'), '%%');
	SET strCustomerName = IF(NOT ISNULL(strCustomerName), CONCAT('%',strCustomerName,'%'), '%%');
	SET strCashierName = IF(NOT ISNULL(strCashierName), CONCAT('%',strCashierName,'%'), '%%');
	SET strTerminalNo = IF(NOT ISNULL(strTerminalNo), CONCAT('%',strTerminalNo,'%'), '%%');
	SET strProductGroup = IF(NOT ISNULL(strProductGroup), CONCAT('%',strProductGroup,'%'), '%%');
	SET dteStartTransactionDate = IF(NOT ISNULL(dteStartTransactionDate), dteStartTransactionDate, '0001-01-01');
	SET dteEndTransactionDate = IF(NOT ISNULL(dteEndTransactionDate), dteEndTransactionDate, now());
	
	INSERT INTO tblSalesPerItem (SessionID, ProductGroup, ProductCode, ProductUnitCode, Quantity, Amount, PurchaseAmount)
	SELECT strSessionID, ProductGroup,
		ProductCode, ProductUnitCode,
		SUM(IF(TransactionItemStatus=3,-a.Quantity,a.Quantity)) 'Quantity', SUM(IF(TransactionItemStatus=3,-a.Amount,a.amount)) Amount, 
		SUM(IF(TransactionItemStatus=3,-PurchaseAmount,IF(TransactionItemStatus=4,-PurchaseAmount,PurchaseAmount))) PurchaseAmount
	FROM tblTransactionItems a 
	INNER JOIN tblTransactions b ON a.TransactionID = b.TransactionID
	WHERE 1=1 
		AND TransactionStatus <> intOpenTransactionStatus 
		AND (TransactionItemStatus = intValidTransactionItemStatus or
			TransactionItemStatus = intReturnTransactionItemStatus or
			TransactionItemStatus = intRefundransactionItemStatus)
		AND TransactionNo LIKE strTransactionNo
		AND CustomerName LIKE strCustomerName
		AND CashierName LIKE strCashierName
		AND TerminalNo LIKE strTerminalNo
		AND ProductGroup LIKE strProductGroup
		AND DATE_FORMAT(TransactionDate, '%Y-%m-%d %H:%i') >= DATE_FORMAT(dteStartTransactionDate, '%Y-%m-%d %H:%i')
		AND DATE_FORMAT(TransactionDate, '%Y-%m-%d %H:%i') <= DATE_FORMAT(dteEndTransactionDate, '%Y-%m-%d %H:%i')
	GROUP BY IF(MatrixDescription <> NULL, MatrixDescription, ProductCode);

END;
GO
delimiter ;

/*********************************
	procUpdateTerminalReportMallForwarderFileName
	Lemuel E. Aceron
	CALL procUpdateTerminalReportMallForwarderFileName
	
	May 21, 2009 - create this procedure
*********************************/
delimiter GO
DROP PROCEDURE IF EXISTS procUpdateTerminalReportMallForwarderFileName
GO

create procedure procUpdateTerminalReportMallForwarderFileName(
	IN pvtTerminalNo varchar(10), 
	IN pvtDateLastInitialized DATETIME,
	IN pvtMallFilename varchar(30)
)
BEGIN
	
	UPDATE tblTerminalReportHistory SET 
		MallFilename = pvtMallFilename
	WHERE TerminalNo = pvtTerminalNo AND DateLastInitialized = pvtDateLastInitialized;
		
END;
GO
delimiter ;

/*********************************
	procUpdateTerminalReportIsMallFileUploadComplete
	Lemuel E. Aceron
	CALL procUpdateTerminalReportIsMallFileUploadComplete
	
	May 21, 2009 - create this procedure
*********************************/
delimiter GO
DROP PROCEDURE IF EXISTS procUpdateTerminalReportIsMallFileUploadComplete
GO

create procedure procUpdateTerminalReportIsMallFileUploadComplete(
	IN pvtTerminalNo varchar(10), 
	IN pvtDateLastInitialized DATETIME,
	IN pvtIsMallFileUploadComplete TINYINT(1)
)
BEGIN
	
	UPDATE tblTerminalReportHistory SET 
		IsMallFileUploadComplete = pvtIsMallFileUploadComplete
	WHERE TerminalNo = pvtTerminalNo AND DateLastInitialized = pvtDateLastInitialized;
		
END;
GO
delimiter ;

/*********************************
	procBEVersionUpdate
	Lemuel E. Aceron
	
	May 26, 2009 - create this procedure
*********************************/
DROP PROCEDURE IF EXISTS procBEVersionUpdate;
delimiter GO

create procedure procBEVersionUpdate(IN strVersion varchar(25))
BEGIN
	
	UPDATE tblTerminal SET BEVersion = strVersion;
	
END;
GO
delimiter ;

/*********************************
	procProductPackageSave
	Lemuel E. Aceron
	May 30, 2013
	

	30May2013 Combine procProductPackageInsert and procProductPackageUpdate
	CALL procProductPackageSave(0, 4355, 26719, 5, 69, 66.3, 65, 1, 12, 0, 0, 'test7', NULL, NULL);
*********************************/

delimiter GO
DROP PROCEDURE IF EXISTS procProductPackageInsert
GO

delimiter GO
DROP PROCEDURE IF EXISTS procProductPackageUpdate
GO

delimiter GO
DROP PROCEDURE IF EXISTS procProductPackageSave
GO

create procedure procProductPackageSave(
	IN pvtPackageID BIGINT(20),
	IN pvtProductID BIGINT(20),
	IN pvtMatrixID BIGINT(20),
	IN pvtUnitID INT(10),
	IN pvtPurchasePrice DECIMAL(18,3),
	IN pvtSellingPrice DECIMAL(18,3),
	IN pvtWSPrice DECIMAL(18,3),
	IN pvtQuantity DECIMAL(18,3), 
	IN pvtVAT DECIMAL(18,3), 
	IN pvtEVAT DECIMAL(18,3), 
	IN pvtLocalTax DECIMAL(18,3),
	IN pvtBarCode1 VARCHAR(30),
	IN pvtBarCode2 VARCHAR(30),
	IN pvtBarCode3 VARCHAR(30))
BEGIN
	IF pvtPackageID = 0 THEN
		SET pvtPackageID = IFNULL((SELECT PackageID FROM tblProductPackage WHERE ProductID = pvtProductID AND MatrixID = pvtMatrixID AND Quantity = 1),0);
	END IF;

	IF pvtPackageID = 0 THEN
		INSERT INTO tblProductPackage(
			ProductID, MatrixID, UnitID, PurchasePrice, Price, WSPrice, Quantity,
			VAT, EVAT, LocalTax, BarCode1, BarCode2, BarCode3)
		VALUES(
			pvtProductID, pvtMatrixID, pvtUnitID, pvtPurchasePrice, pvtSellingPrice, pvtWSPrice, pvtQuantity,
			pvtVAT, pvtEVAT, pvtLocalTax, pvtBarCode1, pvtBarCode2, pvtBarCode3);
	ELSE
		UPDATE tblProductPackage SET
			UnitID			=	pvtUnitID,
			PurchasePrice	=	pvtPurchasePrice,
			Price			=	pvtSellingPrice,
			WSPrice			=	pvtWSPrice,
			Quantity		=	pvtQuantity,
			VAT				=	pvtVAT,
			EVAT			=	pvtEVAT,
			LocalTax		=	pvtLocalTax,
			BarCode1		=	pvtBarCode1,
			BarCode2		=	pvtBarCode2,
			BarCode3		=	pvtBarCode3
		WHERE PackageID		=	pvtPackageID;
	END IF;

	UPDATE tblProductPackage SET 
		BarCode4 = REPLACE(CONCAT(IFNULL(BarCode1,''), Quantity, ProductID, MatrixID),'.','')
	WHERE MatrixID = 0 AND PackageID=pvtPackageID;

	IF pvtQuantity = 1 THEN
		UPDATE tblProductPackage prd 
		INNER JOIN tblProductPackage mtrx ON prd.ProductID = mtrx.ProductID AND mtrx.MatrixID <> 0 SET 
			mtrx.BarCode4 = REPLACE(CONCAT(IFNULL(prd.BarCode1,''), mtrx.Quantity, prd.ProductID, mtrx.MatrixID),'.','')
		WHERE prd.ProductID	=pvtProductID;
	END IF;
	
END;
GO
delimiter ;


/*********************************
	procProductPackagePriceHistoryInsert
	Lemuel E. Aceron
	CALL procProductPackagePriceHistoryInsert();
	
	June 6, 2009 - create this procedure
*********************************/
delimiter GO
DROP PROCEDURE IF EXISTS procProductPackagePriceHistoryInsert
GO

create procedure procProductPackagePriceHistoryInsert(
	IN pvtUID BIGINT(20),
	IN pvtPackageID BIGINT(20),
	IN pvtChangeDate DATETIME,
	IN pvtPurchasePriceNow DECIMAL(18,3), 
	IN pvtSellingPriceNow DECIMAL(18,3),
	IN pvtVATNow DECIMAL(18,3), 
	IN pvtEVATNow DECIMAL(18,3), 
	IN pvtLocalTaxNow DECIMAL(18,3),
	IN pvtRemarks VARCHAR(150))
BEGIN

	INSERT INTO tblProductPackagePriceHistory(UID, PackageID, ChangeDate, 
		PurchasePriceBefore, PurchasePriceNow, SellingPriceBefore, SellingPriceNow, 
		VATBefore, VATNow, EVATBefore, EVATNow, LocalTaxBefore, LocalTaxNow, Remarks)
	SELECT pvtUID, pvtPackageID, pvtChangeDate, 
		PurchasePrice, IF(pvtPurchasePriceNow=-1,PurchasePrice,pvtPurchasePriceNow), 
		Price, IF(pvtSellingPriceNow=-1,Price,pvtSellingPriceNow), 
		VAT, IF(pvtVATNow=-1,VAT,pvtVATNow), EVAT, IF(pvtEVATNow=-1,EVAT,pvtEVATNow), 
		LocalTax, IF(pvtLocalTaxNow=-1,LocalTax,pvtLocalTaxNow), pvtRemarks 
	FROM tblProductPackage pkg
	WHERE PackageID = pvtPackageID;
		
END;
GO
delimiter ;


/*********************************
	procMatrixPackagePriceHistoryInsert
	Lemuel E. Aceron
	CALL procMatrixPackagePriceHistoryInsert();
	
	June 6, 2009 - create this procedure
*********************************/
delimiter GO
DROP PROCEDURE IF EXISTS procMatrixPackagePriceHistoryInsert
GO

create procedure procMatrixPackagePriceHistoryInsert(
	IN pvtUID BIGINT(20),
	IN pvtPackageID BIGINT(20),
	IN pvtChangeDate DATETIME,
	IN pvtPurchasePriceNow DECIMAL(18,3), 
	IN pvtSellingPriceNow DECIMAL(18,3),
	IN pvtVATNow DECIMAL(18,3), 
	IN pvtEVATNow DECIMAL(18,3), 
	IN pvtLocalTaxNow DECIMAL(18,3),
	IN pvtRemarks VARCHAR(150))
BEGIN

	INSERT INTO tblMatrixPackagePriceHistory(UID, PackageID, ChangeDate, 
		PurchasePriceBefore, PurchasePriceNow, SellingPriceBefore, SellingPriceNow, 
		VATBefore, VATNow, EVATBefore, EVATNow, LocalTaxBefore, LocalTaxNow, Remarks)
						   SELECT pvtUID, pvtPackageID, pvtChangeDate, 
		PurchasePrice, IF(pvtPurchasePriceNow=-1,PurchasePrice,pvtPurchasePriceNow), 
		Price, IF(pvtSellingPriceNow=-1,Price,pvtSellingPriceNow), 
		VAT, IF(pvtVATNow=-1,VAT,pvtVATNow), EVAT, IF(pvtEVATNow=-1,EVAT,pvtEVATNow), 
		LocalTax, IF(pvtLocalTaxNow=-1,LocalTax,pvtLocalTaxNow), pvtRemarks FROM tblMatrixPackage WHERE PackageID = pvtPackageID;
		
END;
GO
delimiter ;

/*********************************
	procZeroOutProductQuantity
	Lemuel E. Aceron
	CALL procZeroOutProductQuantity();
	
	July 2 2009 - create this procedure
*********************************/
delimiter GO
DROP PROCEDURE IF EXISTS procZeroOutProductQuantity
GO

create procedure procZeroOutProductQuantity()
BEGIN

	UPDATE tblProducts SET Quantity = 0;

	UPDATE tblProductBasevariationsMatrix SET Quantity = 0;

	UPDATE tblBranchInventory SET Quantity = 0;

	UPDATE tblBranchInventoryMatrix SET Quantity = 0;

END;
GO
delimiter ;

/*********************************
	procZeroOutProductQuantityAndDropVariations
	Lemuel E. Aceron
	CALL procZeroOutProductQuantityAndDropVariations();
	
	July 2 2009 - create this procedure
*********************************/
delimiter GO
DROP PROCEDURE IF EXISTS procZeroOutProductQuantityAndDropVariations
GO

create procedure procZeroOutProductQuantityAndDropVariations()
BEGIN

	TRUNCATE TABLE tblProductInventory;

	TRUNCATE TABLE tblBranchInventoryMatrix;
	TRUNCATE TABLE tblProductVariationsMatrix;
	
	TRUNCATE TABLE tblProductBaseVariationsMatrix;
	
	
END;
GO
delimiter ;

/*********************************
	procProductVariationCountUpdate
	Lemuel E. Aceron
	CALL procProductVariationCountUpdate
	
	July 8 2009 - create this procedure
*********************************/
delimiter GO
DROP PROCEDURE IF EXISTS procProductVariationCountUpdate
GO

create procedure procProductVariationCountUpdate(IN lngProductID BIGINT)
BEGIN
	
	UPDATE tblProducts SET VariationCount = (SELECT COUNT(MatrixID) FROM tblProductBaseVariationsMatrix z WHERE z.Deleted = 0 AND tblProducts.ProductID = z.ProductID) WHERE ProductID = lngProductID ;

END;
GO
delimiter ;

/*********************************
	procProductSynchronizeQuantity
	Lemuel E. Aceron
	CALL procProductSynchronizeQuantity(11);
	
	Oct 01, 2009 : Lemu
	Create this procedure
	
	Jul 26, 2011 : Lemu
	Added Insert to product movement history
	Added Insert to inventory adjustment
	
*********************************/
delimiter GO
DROP PROCEDURE IF EXISTS procProductSynchronizeQuantity
GO

create procedure procProductSynchronizeQuantity(
	IN lngProductID BIGINT
)
BEGIN
	DECLARE strTransactionNo VARCHAR(30) DEFAULT '';
	DECLARE lngMatrixVariationCount BIGINT DEFAULT 0;
	DECLARE strProductCode VARCHAR(30) DEFAULT '';
	DECLARE strProductDesc VARCHAR(50) DEFAULT '';
	DECLARE intUnitID INT DEFAULT 0;
	DECLARE strUnitCode VARCHAR(5) DEFAULT '';
	DECLARE decProductQuantity, decProductActualQuantity, decMinThreshold, decMaxThreshold DECIMAL(18,3) DEFAULT 0;
	DECLARE decMatrixTotalQuantity DECIMAL(18,3) DEFAULT 0;
	DECLARE lngMatrixID BIGINT DEFAULT 0;
	DECLARE strRemarks VARCHAR(100);
	DECLARE intBranchID INT(4) DEFAULT 1;
	
	-- STEP 1: check if there is an existing variation

	-- STEP 1.a: Set the value of lngMatrixVariationCount
	SELECT COUNT(MatrixID) INTO lngMatrixVariationCount FROM tblProductBaseVariationsMatrix WHERE Deleted = 0 AND ProductID = lngProductID;

	IF (ISNULL(lngMatrixVariationCount)) THEN SET lngMatrixVariationCount = 0; END IF; 
	
	-- STEP 1.b: compare if there is a count
	IF (lngMatrixVariationCount <> 0) THEN 

		-- STEP 2: get the total Quantity of all Matrix
		SET decMatrixTotalQuantity = 0;
		SELECT SUM(Quantity) INTO decMatrixTotalQuantity FROM tblProductBaseVariationsMatrix WHERE Deleted = 0 AND ProductID = lngProductID;
	
		-- STEP 3: Set the value of strProductCode, strProductDesc, decProductQuantity, decProductActualQuantity, intUnitID, strUnitCode, decMinThreshold, decMaxThreshold
		SELECT ProductCode, ProductDesc, Quantity, ActualQuantity, BaseUnitID, UnitCode, MinThreshold, MaxThreshold INTO 
				strProductCode, strProductDesc, decProductQuantity, decProductActualQuantity, intUnitID, strUnitCode, decMinThreshold, decMaxThreshold
		FROM tblProducts a INNER JOIN tblUnit b ON a.BaseUnitID = b.UnitID WHERE ProductID = lngProductID;
			
		-- STEP 4: IF Matrix Total Quantity is not equal to Product Quantity
		--		   auto adjust the product quantity based on total of quantities of all variations
		IF (decMatrixTotalQuantity <> decProductQuantity) THEN
			
			-- set the value of stRemarks, see the administrator for the list of constant remarks
			SET strRemarks = 'SYSTEM AUTO ADJUSTMENT OF PRODUCT QTY FROM SUM OF MATRIX QTY AS BASIS';
			
			-- STEP 4.a: Insert to product movement history
			CALL procProductMovementInsert(lngProductID, strProductCode, strProductDesc, 0, '', 
											decProductQuantity, decMatrixTotalQuantity - decProductQuantity, decMatrixTotalQuantity, 0, 
											strUnitCode, strRemarks, now(), strTransactionNo, 'SYSTEM', intBranchID, intBranchID, 0);
			
			-- STEP 4.b: Insert to inventory adjustment
			CALL procInvAdjustmentInsert(1, now(), lngProductID, strProductCode, strProductDesc, 0,
														'', intUnitID, strUnitCode, decProductQuantity, decMatrixTotalQuantity, 
														decMinThreshold, decMinThreshold, decMaxThreshold, decMaxThreshold, CONCAT(strRemarks, ' ', strTransactionNo));
			
			-- STEP 4.c: Do the actual adjustment
			UPDATE tblProducts a SET 
				Quantity			= (SELECT IFNULL(SUM(Quantity), 0) from tblProductBaseVariationsMatrix b where b.Deleted = 0 AND a.productID = b.ProductID) 
			WHERE ProductID = lngProductID;
		
		END IF;
		
		-- STEP 5: Update the Actual Quantity
		UPDATE tblProducts a SET 
			ActualQuantity		= (SELECT IFNULL(SUM(ActualQuantity), 0) from tblProductBaseVariationsMatrix b where b.Deleted = 0 AND a.productID = b.ProductID) 
		WHERE ProductID = lngProductID;
	
	END IF;
	
	
END;
GO
delimiter ;

/*********************************
	procProductBaseVariationsMatrixDelete
	Lemuel E. Aceron
	CALL procProductBaseVariationsMatrixDelete();
	
	Jul 02 2009 : Lemu
	- Create this procedure
	
	Jul 26, 2011 : Lemu
	- Added Synchronize the Product Quantity	
*********************************/
delimiter GO
DROP PROCEDURE IF EXISTS procProductBaseVariationsMatrixDelete
GO

create procedure procProductBaseVariationsMatrixDelete(
	IN pvtIDs varchar(100)
)
BEGIN
	DECLARE lngProductID BIGINT DEFAULT 0;
	DECLARE done INT DEFAULT 0;
	DECLARE curProductIDs CURSOR FOR SELECT DISTINCT(ProductID) FROM tblProductBaseVariationsMatrix WHERE Deleted = 0 AND MatrixID IN (pvtIDs);
	DECLARE CONTINUE HANDLER FOR NOT FOUND SET done = 1;
		
	DELETE FROM tblMatrixPackage WHERE MatrixID IN (pvtIDs);

	DELETE FROM tblProductVariationsMatrix WHERE MatrixID IN (pvtIDs);
	
	OPEN curProductIDs;
	
	UPDATE tblProductBaseVariationsMatrix SET Deleted = 1 WHERE MatrixID IN (pvtIDs);
	
	REPEAT
		FETCH curProductIDs INTO lngProductID;
		
		IF NOT done THEN
			
			-- Synchronize the Product Quantity	
			CALL procProductSynchronizeQuantity(lngProductID);
			
			-- Update the variation count in table Product
			CALL procProductVariationCountUpdate(lngProductID);
			
		END IF;
		
	UNTIL done END REPEAT;
	CLOSE curProductIDs;
			
END;
GO
delimiter ;


/*********************************
	procProductBaseVariationsMatrixDeleteByID
	Lemuel E. Aceron
	CALL procProductBaseVariationsMatrixDeleteByID
	
	Jul 02, 2009 : Lemu
	- create this procedure
	Jul 26, 2011 : Lemu
	- Added Synchronize the Product Quantity	
	May 23, 2013 : Lemu
	- Point to tblProductInventory
*********************************/
delimiter GO
DROP PROCEDURE IF EXISTS procProductBaseVariationsMatrixDeleteByID
GO

create procedure procProductBaseVariationsMatrixDeleteByID(
	IN pvtID BIGINT
)
BEGIN
	
	DELETE FROM tblProductPackage WHERE MatrixID = pvtID;
	
	DELETE FROM tblProductVariationsMatrix WHERE MatrixID = pvtID;
	
	DELETE FROM tblProductInventory WHERE MatrixID = pvtID;

	DELETE FROM tblProductBaseVariationsMatrix WHERE MatrixID = pvtID;

	-- UPDATE tblProductBaseVariationsMatrix SET Deleted = 1 WHERE MatrixID = pvtID;
	
END;
GO
delimiter ;

/**************************************************************
	procGenerateProductPrices
	Lemuel E. Aceron
	CALL procGenerateProductPrices('1', null, null);
	
**************************************************************/
delimiter GO
DROP PROCEDURE IF EXISTS procGenerateProductPrices
GO

create procedure procGenerateProductPrices(
	IN strSessionID varchar(30),
	IN strProductGroupName varchar(30),
	IN strProductSubGroupName varchar(30)
	)
BEGIN

	/*****************************
	**	tblProductPrices
	*****************************/
	DROP TABLE IF EXISTS tblProductPrices;
	CREATE TABLE tblProductPrices (
		`SessionID` VARCHAR(30) NOT NULL,
		`ProductID` BIGINT(20) UNSIGNED NOT NULL DEFAULT 0,
		`ProductCode` VARCHAR(30) NOT NULL,
		`ProductDescription` VARCHAR(30) NOT NULL,
		`MatrixDescription` VARCHAR(100) NOT NULL,
		`ProductGroupID` BIGINT(20) UNSIGNED NOT NULL DEFAULT 0,
		`ProductGroupName` VARCHAR(30) NOT NULL,
		`ProductSubGroupID` BIGINT(20) UNSIGNED NOT NULL DEFAULT 0,
		`ProductSubGroupName` VARCHAR(30) NOT NULL,
		`Quantity` DECIMAL(18,3),
		`UnitCode` VARCHAR(10) NOT NULL,
		`UnitName` VARCHAR(30) NOT NULL,
		`PurchasePrice` DECIMAL(18,3),
		`Price` DECIMAL(18,3),
		`VAT` DECIMAL(18,3),
		`EVAT` DECIMAL(18,3),
		`LocalTax` DECIMAL(18,3),
	INDEX `IX_tblProductPrices`(`SessionID`),
	INDEX `IX1_tblProductPrices`(`ProductID`),
	INDEX `IX2_tblProductPrices`(`ProductGroupID`),
	INDEX `IX3_tblProductPrices`(`ProductSubGroupID`)
	);

	/*** Select the package prices for matrix ***/
	INSERT INTO tblProductPrices
	SELECT 
		strSessionID,
		b.ProductID, 
		d.ProductCode, 
		d.ProductDesc AS ProductDescription, 
		b.Description AS MatrixDescription,
		f.ProductGroupID, 
		f.ProductGroupName,
		e.ProductSubGroupID,
		e.ProductSubGroupName,
		a.Quantity,
		c.UnitCode, 
		c.UnitName, 
		a.PurchasePrice, 
		a.Price,
		a.VAT, 
		a.EVAT, 
		a.LocalTax 
	FROM tblMatrixPackage a 
		INNER JOIN tblProductBaseVariationsMatrix b ON a.MatrixID = b.MatrixID 
		INNER JOIN tblUnit c ON a.UnitID = c.UnitID  
		INNER JOIN tblProducts d ON b.ProductID = d. ProductID
		INNER JOIN tblProductSubGroup e ON d.ProductSubGroupID = e.ProductSubGroupID
		INNER JOIN tblProductGroup f ON e.ProductGroupID = f.ProductGroupID
	WHERE d.Deleted = 0;
	
	/*** Select the packages for products without variations ***/
	INSERT INTO tblProductPrices
	SELECT 
		strSessionID,
		b.ProductID, 
		b.ProductCode, 
		b.ProductDesc AS ProductDescription, 
		null AS MatrixDescription,
		e.ProductGroupID, 
		e.ProductGroupName,
		d.ProductSubGroupID,
		d.ProductSubGroupName,
		a.Quantity,
		c.UnitCode, 
		c.UnitName, 
		a.PurchasePrice, 
		a.Price,
		a.VAT, 
		a.EVAT, 
		a.LocalTax 
	FROM tblProductPackage a 
		INNER JOIN tblProducts b ON a.ProductID = b.ProductID
		INNER JOIN tblUnit c ON a.UnitID = c.UnitID  
		INNER JOIN tblProductSubGroup d ON b.ProductSubGroupID = d.ProductSubGroupID
		INNER JOIN tblProductGroup e ON d.ProductGroupID = e.ProductGroupID
	WHERE b.ProductID NOT IN (SELECT DISTINCT(ProductID) FROM tblProductBaseVariationsMatrix WHERE Deleted = 0);
	
END;
GO
delimiter ;


/**************************************************************
	AccessuserSynchronizeAccessRights
	Lemuel E. Aceron
	CALL AccessuserSynchronizeAccessRights(1);
	
**************************************************************/
delimiter GO
DROP PROCEDURE IF EXISTS AccessuserSynchronizeAccessRights
GO

create procedure AccessuserSynchronizeAccessRights(IN pvtUID BIGINT)
BEGIN
	
	-- delete all current access of User
	DELETE FROM sysAccessRights WHERE UID = pvtUID;

	-- insert all the access of user based on his/her group
	INSERT INTO sysAccessRights (UID, TranTypeID, AllowRead, AllowWrite)
	SELECT pvtUID, TranTypeID, AllowRead, AllowWrite FROM sysAccessGroupRights WHERE GroupID = (SELECT GroupID FROM sysAccessUserDetails WHERE UID = pvtUID);
	
END;
GO
delimiter ;


/**************************************************************
	AccessuserSynchronizeAccessRightsFromGroup
	Lemuel E. Aceron
	CALL AccessuserSynchronizeAccessRightsFromGroup(1,1);
	
**************************************************************/
delimiter GO
DROP PROCEDURE IF EXISTS AccessuserSynchronizeAccessRightsFromGroup
GO

create procedure AccessuserSynchronizeAccessRightsFromGroup(IN pvtUID BIGINT, IN pvtGroupID INT)
BEGIN
	
	DELETE FROM sysAccessRights WHERE UID = pvtUID;
	
	-- delete all current access of User
	DELETE FROM sysAccessRights WHERE UID = pvtUID;

	-- insert all the access of user based on his/her group
	INSERT INTO sysAccessRights (UID, TranTypeID, AllowRead, AllowWrite)
	SELECT pvtUID, TranTypeID, AllowRead, AllowWrite FROM sysAccessGroupRights WHERE GroupID = pvtGroupID;
	
END;
GO
delimiter ;


/*********************************
	procCashPaymentInsert
	Lemuel E. Aceron
	CALL procCashPaymentInsert();
	
	Sep 01, 2009 : Lemu
	- create this procedure
*********************************/
delimiter GO
DROP PROCEDURE IF EXISTS procCashPaymentInsert
GO

create procedure procCashPaymentInsert(
	IN pvtTransactionID BIGINT(20),
	IN pvtTransactionNo VARCHAR(30),
	IN pvtAmount DECIMAL(18,3), 
	IN pvtRemarks VARCHAR(255))
BEGIN

	INSERT INTO tblCashPayment(TransactionID, TransactionNo, Amount, Remarks)
				VALUES (pvtTransactionID, pvtTransactionNo, pvtAmount, pvtRemarks);
		
END;
GO
delimiter ;


/*********************************
	procChequePaymentInsert
	Lemuel E. Aceron
	CALL procChequePaymentInsert();
	
	Sep 01, 2009 : Lemu
	- create this procedure
*********************************/
delimiter GO
DROP PROCEDURE IF EXISTS procChequePaymentInsert
GO

create procedure procChequePaymentInsert(
	IN pvtTransactionID BIGINT(20),
	IN pvtTransactionNo VARCHAR(30),
	IN pvtChequeNo VARCHAR(30),
	IN pvtAmount DECIMAL(18,3), 
	IN pvtValidityDate DATETIME,
	IN pvtRemarks VARCHAR(255))
BEGIN

	INSERT INTO tblChequePayment(TransactionID, TransactionNo, ChequeNo, Amount, ValidityDate, Remarks)
				VALUES (pvtTransactionID, pvtTransactionNo, pvtChequeNo, pvtAmount, pvtValidityDate, pvtRemarks);
		
END;
GO
delimiter ;


/*********************************
	procCreditCardPaymentInsert
	Lemuel E. Aceron
	CALL procCreditCardPaymentInsert();
	
	Sep 01, 2009 : Lemu
	- create this procedure
*********************************/
delimiter GO
DROP PROCEDURE IF EXISTS procCreditCardPaymentInsert
GO

create procedure procCreditCardPaymentInsert(
	IN pvtTransactionID BIGINT(20),
	IN pvtTransactionNo VARCHAR(30),
	IN pvtAmount DECIMAL(18,3), 
	IN pvtCardTypeID INT,
	IN pvtCardTypeCode VARCHAR(30),
	IN pvtCardTypeName VARCHAR(30),
	IN pvtCardNo VARCHAR(30),
	in pvtCardHolder VARCHAR(150),
	IN pvtValidityDates VARCHAR(14),
	IN pvtRemarks VARCHAR(255))
BEGIN

	INSERT INTO tblCreditCardPayment(TransactionID, TransactionNo, Amount, CardTypeID, CardTypeCode, CardTypeName, CardNo, CardHolder, ValidityDates, Remarks)
				VALUES (pvtTransactionID, pvtTransactionNo, pvtAmount, pvtCardTypeID, pvtCardTypeCode, pvtCardTypeName, pvtCardNo, pvtCardHolder, pvtValidityDates, pvtRemarks);
		
END;
GO
delimiter ;

/*********************************
	procCreditPaymentInsert
	Lemuel E. Aceron
	CALL procCreditPaymentInsert();
	
	[09/01/2009] - create this procedure
	[04/05/2012] - include additional fields in saving to get the values of creditcard info

*********************************/
delimiter GO
DROP PROCEDURE IF EXISTS procCreditPaymentInsert
GO

create procedure procCreditPaymentInsert(
	IN pvtTransactionID BIGINT(20),
	IN pvtCustomerID BIGINT(20),
	IN pvtGuarantorID BIGINT(20),
	IN pvtCreditType TINYINT(1),
	IN pvtCreditExpiryDate DATETIME,
	IN pvtCurrentCredit DECIMAL(18,3),
	IN pvtAmount DECIMAL(18,3),
	IN pvtTerminalNo VARCHAR(10),
	IN pvtTransactionDate DATETIME,
	IN pvtTransactionNo VARCHAR(30),
	IN pvtCashierName VARCHAR(150),
	IN pvtRemarks VARCHAR(255))
BEGIN

	
	INSERT INTO tblCreditPayment(TransactionID, ContactID, GuarantorID, CreditType, 
								CreditBefore, Amount, CreditAfter, CreditExpiryDate, 
								CreditReason, CreditDate, 
								TerminalNo, CashierName, TransactionNo)
					VALUES (pvtTransactionID, pvtCustomerID, pvtGuarantorID, pvtCreditType,  
								pvtCurrentCredit, pvtAmount, pvtCurrentCredit + pvtAmount, pvtCreditExpiryDate, 
								pvtRemarks, pvtTransactionDate, 
								pvtTerminalNo, pvtCashierName, pvtTransactionNo);
END;
GO
delimiter ;

/*********************************
	procDebitPaymentInsert
	Lemuel E. Aceron
	CALL procDebitPaymentInsert();
	
	September 01, 2009 - create this procedure
*********************************/
delimiter GO
DROP PROCEDURE IF EXISTS procDebitPaymentInsert
GO

create procedure procDebitPaymentInsert(
	IN pvtTransactionID BIGINT(20),
	IN pvtTransactionNo VARCHAR(30),
	IN pvtAmount DECIMAL(18,3),
	IN pvtContactID BIGINT,
	IN pvtRemarks VARCHAR(255))
BEGIN

	INSERT INTO tblDebitPayment(TransactionID, TransactionNo, Amount, ContactID, Remarks)
				VALUES (pvtTransactionID, pvtTransactionNo, pvtAmount, pvtContactID, pvtRemarks);
		
END;
GO
delimiter ;

/*********************************
	procContactAddCredit
	Lemuel E. Aceron
	CALL procContactAddCredit();
	
	[09/01/2009] - create this procedure
	[04/03/2012] - include saving of credit as totalpuchase in creditcardinfo
*********************************/
delimiter GO
DROP PROCEDURE IF EXISTS procContactAddCredit
GO

create procedure procContactAddCredit(
	IN pvtContactID BIGINT(20),
	IN pvtCredit DECIMAL(18,3))
BEGIN

	UPDATE tblContacts SET Credit =	Credit + pvtCredit WHERE ContactID = pvtContactID;
	
	UPDATE tblContactCreditCardInfo SET TotalPurchases = TotalPurchases + pvtCredit WHERE CustomerID = pvtContactID;
		
END;
GO
delimiter ;

/*********************************
	procContactAddDebit
	Lemuel E. Aceron
	CALL procContactAddDebit();
	
	[09/01/2009] - create this procedure
*********************************/
delimiter GO
DROP PROCEDURE IF EXISTS procContactAddDebit
GO

create procedure procContactAddDebit(
	IN pvtContactID BIGINT(20),
	IN pvtDebit DECIMAL(18,3))
BEGIN

	UPDATE tblContacts SET Debit =	Debit + pvtDebit WHERE ContactID = pvtContactID;
		
END;
GO
delimiter ;

/*********************************
	procContactSubtractCredit
	Lemuel E. Aceron
	CALL procContactSubtractCredit();
	
	[09/01/2009] - create this procedure
*********************************/
delimiter GO
DROP PROCEDURE IF EXISTS procContactSubtractCredit
GO

create procedure procContactSubtractCredit(
	IN pvtContactID BIGINT(20),
	IN pvtCredit DECIMAL(18,3))
BEGIN

	UPDATE tblContacts SET Credit =	Credit - pvtCredit WHERE ContactID = pvtContactID;
		
END;
GO
delimiter ;

/*********************************
	procContactSubtractDebit
	Lemuel E. Aceron
	CALL procContactSubtractDebit();
	
	September 01, 2009 - create this procedure
*********************************/
delimiter GO
DROP PROCEDURE IF EXISTS procContactSubtractDebit
GO

create procedure procContactSubtractDebit(
	IN pvtContactID BIGINT(20),
	IN pvtDebit DECIMAL(18,3))
BEGIN

	UPDATE tblContacts SET Debit =	Debit - pvtDebit WHERE ContactID = pvtContactID;
		
END;
GO
delimiter ;


/*********************************
	procCreditPaymentUpdateCredit
	Lemuel E. Aceron
	CALL procCreditPaymentUpdateCredit();
	
	[09/01/2009] - create this procedure
*********************************/
delimiter GO
DROP PROCEDURE IF EXISTS procCreditPaymentUpdateCredit
GO

create procedure procCreditPaymentUpdateCredit(
	IN pvtTransactionID BIGINT(20),
	IN pvtTransactionNo VARCHAR(30),
	IN pvtAmount DECIMAL(18,3),
	IN pvtRemarks VARCHAR(255))
BEGIN

	UPDATE tblCreditPayment SET 
		AmountPaid = AmountPaid + pvtAmount,
		AmountPaidCuttOffMonth = AmountPaidCuttOffMonth + pvtAmount,
		Remarks = CONCAT(Remarks,';',pvtRemarks)
	WHERE TransactionID = pvtTransactionID 
		AND TransactionNo = pvtTransactionNo;
		
END;
GO
delimiter ;

/*********************************
	procDebitPaymentUpdateDebit
	Lemuel E. Aceron
	CALL procDebitPaymentUpdateDebit();
	
	[09/01/2009] - create this procedure
*********************************/
delimiter GO
DROP PROCEDURE IF EXISTS procDebitPaymentUpdateDebit
GO

create procedure procDebitPaymentUpdateDebit(
	IN pvtTransactionID BIGINT(20),
	IN pvtTransactionNo VARCHAR(30),
	IN pvtAmount DECIMAL(18,3),
	IN pvtRemarks VARCHAR(255))
BEGIN

	UPDATE tblDebitPayment SET 
		AmountPaid = AmountPaid + pvtAmount,
		Remarks = CONCAT(Remarks,';',pvtRemarks)
	WHERE TransactionID = pvtTransactionID 
		AND TransactionNo = pvtTransactionNo;
		
END;
GO
delimiter ;

/*********************************
	procCreditPaymentSyncTransactionNo
	Lemuel E. Aceron
	CALL procCreditPaymentSyncTransactionNo();
	
	[09/02/2009] - create this procedure
	Update Credit Payment TransactionNo with the Correct TransactionNo
	THIS IS ONLY APPLICABLE IF DB Version is lower than v.2.0.0.8
	
*********************************/
delimiter GO
DROP PROCEDURE IF EXISTS procCreditPaymentSyncTransactionNo
GO

create procedure procCreditPaymentSyncTransactionNo()
BEGIN

	UPDATE tblCreditPayment a, (select TransactionNo, TransactionID, CustomerID, CreditPayment from tbltransactions01) AS b SET 
		a.TransactionNo = b.TransactionNo
	where a.TransactionID = b.TransactionID AND a.ContactID = b.CustomerID AND a.Amount = b.CreditPayment;

	UPDATE tblCreditPayment a, (select TransactionNo, TransactionID, CustomerID, CreditPayment from tbltransactions02) AS b SET 
		a.TransactionNo = b.TransactionNo
	where a.TransactionID = b.TransactionID AND a.ContactID = b.CustomerID AND a.Amount = b.CreditPayment;

	UPDATE tblCreditPayment a, (select TransactionNo, TransactionID, CustomerID, CreditPayment from tbltransactions03) AS b SET 
		a.TransactionNo = b.TransactionNo
	where a.TransactionID = b.TransactionID AND a.ContactID = b.CustomerID AND a.Amount = b.CreditPayment;

	UPDATE tblCreditPayment a, (select TransactionNo, TransactionID, CustomerID, CreditPayment from tbltransactions04) AS b SET 
		a.TransactionNo = b.TransactionNo
	where a.TransactionID = b.TransactionID AND a.ContactID = b.CustomerID AND a.Amount = b.CreditPayment;

	UPDATE tblCreditPayment a, (select TransactionNo, TransactionID, CustomerID, CreditPayment from tbltransactions05) AS b SET 
		a.TransactionNo = b.TransactionNo
	where a.TransactionID = b.TransactionID AND a.ContactID = b.CustomerID AND a.Amount = b.CreditPayment;

	UPDATE tblCreditPayment a, (select TransactionNo, TransactionID, CustomerID, CreditPayment from tbltransactions06) AS b SET 
		a.TransactionNo = b.TransactionNo
	where a.TransactionID = b.TransactionID AND a.ContactID = b.CustomerID AND a.Amount = b.CreditPayment;

	UPDATE tblCreditPayment a, (select TransactionNo, TransactionID, CustomerID, CreditPayment from tbltransactions07) AS b SET 
		a.TransactionNo = b.TransactionNo
	where a.TransactionID = b.TransactionID AND a.ContactID = b.CustomerID AND a.Amount = b.CreditPayment;

	UPDATE tblCreditPayment a, (select TransactionNo, TransactionID, CustomerID, CreditPayment from tbltransactions08) AS b SET 
		a.TransactionNo = b.TransactionNo
	where a.TransactionID = b.TransactionID AND a.ContactID = b.CustomerID AND a.Amount = b.CreditPayment;

	UPDATE tblCreditPayment a, (select TransactionNo, TransactionID, CustomerID, CreditPayment from tbltransactions09) AS b SET 
		a.TransactionNo = b.TransactionNo
	where a.TransactionID = b.TransactionID AND a.ContactID = b.CustomerID AND a.Amount = b.CreditPayment;

	UPDATE tblCreditPayment a, (select TransactionNo, TransactionID, CustomerID, CreditPayment from tbltransactions10) AS b SET 
		a.TransactionNo = b.TransactionNo
	where a.TransactionID = b.TransactionID AND a.ContactID = b.CustomerID AND a.Amount = b.CreditPayment;

	UPDATE tblCreditPayment a, (select TransactionNo, TransactionID, CustomerID, CreditPayment from tbltransactions11) AS b SET 
		a.TransactionNo = b.TransactionNo
	where a.TransactionID = b.TransactionID AND a.ContactID = b.CustomerID AND a.Amount = b.CreditPayment;

	UPDATE tblCreditPayment a, (select TransactionNo, TransactionID, CustomerID, CreditPayment from tbltransactions12) AS b SET 
		a.TransactionNo = b.TransactionNo
	where a.TransactionID = b.TransactionID AND a.ContactID = b.CustomerID AND a.Amount = b.CreditPayment;
		
END;
GO
delimiter ;

/**************************************************************
	procGenerateSalesPerItemWithZeroSales
	Lemuel E. Aceron
	
	CALL procGenerateSalesPerItemWithZeroSales('1', null, null, null, null, null, null);
	
	[09/07/2009] created this procedure

**************************************************************/
delimiter GO
DROP PROCEDURE IF EXISTS procGenerateSalesPerItemWithZeroSales
GO

create procedure procGenerateSalesPerItemWithZeroSales(
	IN strSessionID varchar(30),
	IN strTransactionNo varchar(30),
	IN strCustomerName varchar(100),
	IN strCashierName varchar(100),
	IN strTerminalNo varchar(30),
	IN dteStartTransactionDate DateTime,
	IN dteEndTransactionDate DateTime
	)
BEGIN

	SET strTransactionNo = IF(NOT ISNULL(strTransactionNo), CONCAT('%',strTransactionNo,'%'), '%%');
	SET strCustomerName = IF(NOT ISNULL(strCustomerName), CONCAT('%',strCustomerName,'%'), '%%');
	SET strCashierName = IF(NOT ISNULL(strCashierName), CONCAT('%',strCashierName,'%'), '%%');
	SET strTerminalNo = IF(NOT ISNULL(strTerminalNo), CONCAT('%',strTerminalNo,'%'), '%%');
	SET dteStartTransactionDate = IF(NOT ISNULL(dteStartTransactionDate), dteStartTransactionDate, '0001-01-01');
	SET dteEndTransactionDate = IF(NOT ISNULL(dteEndTransactionDate), dteEndTransactionDate, now());
	
	CALL procGenerateSalesPerItem(strSessionID, strTransactionNo, strCustomerName, strCashierName, strTerminalNo, dteStartTransactionDate, dteEndTransactionDate);
	
	INSERT INTO tblSalesPerItem (SessionID, ProductGroup, ProductCode, ProductUnitCode, Quantity, Amount, PurchaseAmount)
	SELECT strSessionID,
		ProductGroupCode,
		a.ProductCode 'ProductCode',
		UnitCode 'ProductUnitCode',
		0 'Quantity', 0 Amount, 
		0 PurchaseAmount
	FROM tblProducts a 
		INNER JOIN tblProductSubGroup b ON b.ProductSubGroupID = a.ProductSubGroupID
		INNER JOIN tblProductGroup c ON c.ProductGroupID = b.ProductGroupID
		INNER JOIN tblUnit d ON d.UnitID = a.BaseUnitID
	WHERE ProductCode NOT IN (SELECT ProductCode FROM tblSalesPerItem WHERE ProductCode NOT LIKE '%-%');
	
	-- exclude the ProductCode with '-' coz it's sure that's is with sales [ProductCode NOT LIKE '%-%']
	
END;
GO
delimiter ;


/*********************************
	procMatrixPackageUpdatePurchasing
	Lemuel E. Aceron
	CALL procMatrixPackageUpdatePurchasing();
	
	[10/25/2009] - create this procedure

*********************************/
delimiter GO
DROP PROCEDURE IF EXISTS procMatrixPackageUpdatePurchasing
GO

create procedure procMatrixPackageUpdatePurchasing(
	IN pvtMatrixID BIGINT(20),
	IN pvtUnitID INT,
	IN pvtQuantity DECIMAL(18,3),
	IN pvtPurchasePrice DECIMAL(18,3))
BEGIN

	UPDATE tblMatrixPackage SET
		PurchasePrice	=	pvtPurchasePrice
	WHERE MatrixID		=	pvtMatrixID
		AND UnitID		=	pvtUnitID
		AND Quantity	=	pvtQuantity;
							
		
END;
GO
delimiter ;

/*********************************
	procProductTagActiveInactive
	Lemuel E. Aceron
	CALL procProductTagActiveInactive
	
	[10/28/2009] - create this procedure

*********************************/
delimiter GO
DROP PROCEDURE IF EXISTS procProductTagActiveInactive
GO

create procedure procProductTagActiveInactive(IN lngProductID BIGINT, IN intStatus TINYINT(1))
BEGIN
	
	UPDATE tblProducts SET Active = intStatus WHERE ProductID = lngProductID ;

END;
GO
delimiter ;

/*********************************
	procSyncContactCredit
	Lemuel E. Aceron
	CALL procSyncContactCredit();
	
	[12/18/2009] - create this procedure

*********************************/
delimiter GO
DROP PROCEDURE IF EXISTS procSyncContactCredit
GO

create procedure procSyncContactCredit()
BEGIN
	
	UPDATE tblContacts, (SELECT ContactID, SUM(Amount) - SUM(AmountPaid) Credit 
						 FROM tblCreditPayment GROUP BY ContactID 
						) tblCreditPayment
	SET 
		tblContacts.Credit = tblCreditPayment.Credit
	WHERE tblContacts.ContactID = tblCreditPayment.ContactID;

END;
GO
delimiter ;

/*********************************
	procMatrixPackageUpdateSelling
	Lemuel E. Aceron
	CALL procMatrixPackageUpdateSelling();
	
	[01/01/2010] - create this procedure

*********************************/
delimiter GO
DROP PROCEDURE IF EXISTS procMatrixPackageUpdateSelling
GO

create procedure procMatrixPackageUpdateSelling(
	IN pvtMatrixID BIGINT(20),
	IN pvtUnitID INT,
	IN pvtQuantity DECIMAL(18,3),
	IN pvtPrice DECIMAL(18,3))
BEGIN

	UPDATE tblMatrixPackage SET
		Price	=	pvtPrice
	WHERE MatrixID		=	pvtMatrixID
		AND UnitID		=	pvtUnitID
		AND Quantity	=	pvtQuantity;
							
		
END;
GO
delimiter ;

/*********************************
	procMatrixPackageUpdateSellingWSPrice
	Lemuel E. Aceron
	CALL procMatrixPackageUpdateSellingWSPrice();
	
	[01/01/2010] - create this procedure
*********************************/
delimiter GO
DROP PROCEDURE IF EXISTS procMatrixPackageUpdateSellingWSPrice
GO

create procedure procMatrixPackageUpdateSellingWSPrice(
	IN pvtMatrixID BIGINT(20),
	IN pvtUnitID INT,
	IN pvtQuantity DECIMAL(18,3),
	IN pvtWSPrice DECIMAL(18,3))
BEGIN

	UPDATE tblMatrixPackage SET
		WSPrice	=	pvtWSPrice
	WHERE MatrixID		=	pvtMatrixID
		AND UnitID		=	pvtUnitID
		AND Quantity	=	pvtQuantity;
							
		
END;
GO
delimiter ;

/*********************************
	procMatrixPackageUpdateSellingUsingProductID
	Lemuel E. Aceron
	CALL procMatrixPackageUpdateSellingUsingProductID();
	
	[01/01/2010] - create this procedure

*********************************/
delimiter GO
DROP PROCEDURE IF EXISTS procMatrixPackageUpdateSellingUsingProductID
GO

create procedure procMatrixPackageUpdateSellingUsingProductID(
	IN pvtProductID BIGINT(20),
	IN pvtUnitID INT,
	IN pvtQuantity DECIMAL(18,3),
	IN pvtPrice DECIMAL(18,3))
BEGIN

	UPDATE tblMatrixPackage SET
		Price	=	pvtPrice
	WHERE MatrixID IN (SELECT MatrixID FROM tblProductBaseVariationsMatrix WHERE Deleted = 0 AND ProductID = pvtProductID)
		AND UnitID		=	pvtUnitID
		AND Quantity	=	pvtQuantity;
							
		
END;
GO
delimiter ;

/*********************************
	procMatrixPackageUpdateSellingUsingProductIDWSPrice
	Lemuel E. Aceron
	CALL procMatrixPackageUpdateSellingUsingProductIDWSPrice();
	
	[01/13/2010] - create this procedure

*********************************/
delimiter GO
DROP PROCEDURE IF EXISTS procMatrixPackageUpdateSellingUsingProductIDWSPrice
GO

create procedure procMatrixPackageUpdateSellingUsingProductIDWSPrice(
	IN pvtProductID BIGINT(20),
	IN pvtUnitID INT,
	IN pvtQuantity DECIMAL(18,3),
	IN pvtPrice DECIMAL(18,3))
BEGIN

	UPDATE tblMatrixPackage SET
		WSPrice	=	pvtWSPrice
	WHERE MatrixID IN (SELECT MatrixID FROM tblProductBaseVariationsMatrix WHERE Deleted = 0 AND ProductID = pvtProductID)
		AND UnitID		=	pvtUnitID
		AND Quantity	=	pvtQuantity;
							
		
END;
GO
delimiter ;

/*********************************
	procTransactionAgentUpdate
	Lemuel E. Aceron
	
	[02/26/2010] - created this procedure

	[10/08/2010] - Added AgentPositionName and AgentDepartmentName

*********************************/
DROP PROCEDURE IF EXISTS procTransactionAgentUpdate;
delimiter GO

create procedure procTransactionAgentUpdate(IN lngTransactionID bigint(20), IN lngAgentID BIGINT(20), IN strAgentName varchar(100), IN strAgentPositionName varchar(30), IN strAgentDepartmentName varchar(30))
BEGIN
	
	UPDATE tblTransactions SET AgentID = lngAgentID, AgentName = strAgentName, AgentPositionName = strAgentPositionName, AgentDepartmentName = strAgentDepartmentName WHERE TransactionID = lngTransactionID;

END;
GO
delimiter ;


/*********************************
	procProductCommisionUpdate
	Lemuel E. Aceron
	CALL procProductCommisionUpdate
	
	March 1, 2010 - create this procedure
*********************************/
delimiter GO
DROP PROCEDURE IF EXISTS procProductCommisionUpdate
GO

create procedure procProductCommisionUpdate(IN lngProductID BIGINT, IN decPercentageCommision DECIMAL(3,2))
BEGIN
	
	UPDATE tblProducts SET PercentageCommision = decPercentageCommision WHERE ProductID = lngProductID ;

END;
GO
delimiter ;

/**************************************************************
	procGenerateAgentsCommision
	Lemuel E. Aceron
	CALL procGenerateAgentsCommision('1', 1, null, null);
	
**************************************************************/
delimiter GO
DROP PROCEDURE IF EXISTS procGenerateAgentsCommision
GO

create procedure procGenerateAgentsCommision(
	IN strSessionID varchar(30),
	IN lngAgentID BIGINT(2),
	IN dteStartTransactionDate DateTime,
	IN dteEndTransactionDate DateTime
	)
BEGIN
	DECLARE intOpenTransactionStatus, intValidTransactionItemStatus, intReturnTransactionItemStatus, intRefundransactionItemStatus INTEGER DEFAULT 0;
	
	SET intOpenTransactionStatus = 0; 
	SET intValidTransactionItemStatus = 0;
	SET intReturnTransactionItemStatus = 3;
	SET intRefundransactionItemStatus = 4;
	
	SET dteStartTransactionDate = IF(NOT ISNULL(dteStartTransactionDate), dteStartTransactionDate, '0001-01-01');
	SET dteEndTransactionDate = IF(NOT ISNULL(dteEndTransactionDate), dteEndTransactionDate, now());
	
	INSERT INTO tblAgentsCommision (SessionID, TransactionNo, TransactionDate, Description, Quantity, Amount, PercentageCommision, Commision, DepartmentName, PositionName)
	SELECT strSessionID, TransactionNo,
		TransactionDate, IF(MatrixDescription <> NULL, MatrixDescription, Description) 'Description',
		SUM(IF(TransactionItemStatus=3,-a.Quantity,a.Quantity)) 'Quantity', SUM(IF(TransactionItemStatus=3,-a.Amount,a.amount)) Amount, 
		PercentageCommision, SUM(Commision) 'Commision', AgentDepartmentName, AgentPositionName
	FROM tblTransactionItems a 
	INNER JOIN tblTransactions b ON a.TransactionID = b.TransactionID
	WHERE 1=1 
		AND TransactionStatus <> intOpenTransactionStatus 
		AND (TransactionItemStatus = intValidTransactionItemStatus or
			TransactionItemStatus = intReturnTransactionItemStatus or
			TransactionItemStatus = intRefundransactionItemStatus)
		AND AgentID = lngAgentID AND PercentageCommision <> 0 AND Commision <> 0
		AND DATE_FORMAT(TransactionDate, '%Y-%m-%d %H:%i') >= DATE_FORMAT(dteStartTransactionDate, '%Y-%m-%d %H:%i')
		AND DATE_FORMAT(TransactionDate, '%Y-%m-%d %H:%i') <= DATE_FORMAT(dteEndTransactionDate, '%Y-%m-%d %H:%i')
	GROUP BY IF(MatrixDescription <> NULL, MatrixDescription, Description), PercentageCommision, AgentDepartmentName, AgentPositionName;
	
END;
GO
delimiter ;

/**************************************************************
	procGenerateAllAgentsCommision
	Lemuel E. Aceron
	CALL procGenerateAllAgentsCommision('1', null, null);
	
**************************************************************/
delimiter GO
DROP PROCEDURE IF EXISTS procGenerateAllAgentsCommision
GO

create procedure procGenerateAllAgentsCommision(
	IN strSessionID varchar(30),
	IN dteStartTransactionDate DateTime,
	IN dteEndTransactionDate DateTime
	)
BEGIN
	DECLARE intOpenTransactionStatus, intValidTransactionItemStatus, intReturnTransactionItemStatus, intRefundransactionItemStatus INTEGER DEFAULT 0;
	
	SET intOpenTransactionStatus = 0; 
	SET intValidTransactionItemStatus = 0;
	SET intReturnTransactionItemStatus = 3;
	SET intRefundransactionItemStatus = 4;
	
	SET dteStartTransactionDate = IF(NOT ISNULL(dteStartTransactionDate), dteStartTransactionDate, '0001-01-01');
	SET dteEndTransactionDate = IF(NOT ISNULL(dteEndTransactionDate), dteEndTransactionDate, now());
	
	INSERT INTO tblAgentsCommision (SessionID, TransactionNo, TransactionDate, Description, Quantity, Amount, PercentageCommision, Commision, AgentID, AgentName, DepartmentName, PositionName)
	SELECT strSessionID, TransactionNo,
		TransactionDate, IF(MatrixDescription <> NULL, MatrixDescription, Description) 'Description',
		SUM(IF(TransactionItemStatus=3,-a.Quantity,a.Quantity)) 'Quantity', SUM(IF(TransactionItemStatus=3,-a.Amount,a.amount)) Amount, 
		PercentageCommision, SUM(Commision) 'Commision', AgentID, AgentName, AgentDepartmentName, AgentPositionName
	FROM tblTransactionItems a 
	INNER JOIN tblTransactions b ON a.TransactionID = b.TransactionID
	WHERE 1=1 
		AND TransactionStatus <> intOpenTransactionStatus 
		AND (TransactionItemStatus = intValidTransactionItemStatus or
			TransactionItemStatus = intReturnTransactionItemStatus or
			TransactionItemStatus = intRefundransactionItemStatus)
		AND PercentageCommision <> 0 AND Commision <> 0
		AND DATE_FORMAT(TransactionDate, '%Y-%m-%d %H:%i') >= DATE_FORMAT(dteStartTransactionDate, '%Y-%m-%d %H:%i')
		AND DATE_FORMAT(TransactionDate, '%Y-%m-%d %H:%i') <= DATE_FORMAT(dteEndTransactionDate, '%Y-%m-%d %H:%i')
	GROUP BY IF(MatrixDescription <> NULL, MatrixDescription, Description), PercentageCommision, AgentID, AgentName, AgentDepartmentName, AgentPositionName;

END;
GO
delimiter ;

/*********************************
	procStockTagActiveInactive
	Lemuel E. Aceron
	CALL procStockTagActiveInactive
	
	March 10,2010 - create this procedure

*********************************/
delimiter GO
DROP PROCEDURE IF EXISTS procStockTagActiveInactive
GO

create procedure procStockTagActiveInactive(IN lngStockID BIGINT, IN intStatus TINYINT(1))
BEGIN
	
	UPDATE tblStock SET Active = intStatus WHERE StockID = lngStockID;

END;
GO
delimiter ;

/*********************************
	procCheckTerminalLastDateInitialized
	Lemuel E. Aceron
	CALL procCheckTerminalLastDateInitialized
	
	This can be use to get the last initialization of zread 
	and previous initialization of zread.
	
	[03/10/2010] - create this procedure

*********************************/
delimiter GO
DROP PROCEDURE IF EXISTS procCheckTerminalLastDateInitialized
GO

create procedure procCheckTerminalLastDateInitialized()
BEGIN
	
	SELECT DateLastInitialized 'DateLastInitialized' FROM tblTerminalReport;
	
	SELECT DateLastInitialized 'PreviousDateLastInitialized' FROM tblTerminalReportHistory ORDER BY DateLastInitialized DESC LIMIT 1;

END;
GO
delimiter ;

/*********************************
	procFixItemsPurchaseAmount
	Lemuel E. Aceron
	CALL procFixItemsPurchaseAmount();
	
	This can be use to fix the item purchase amount if purchase amount is not consistent 
	with purchaseprice * quantity.
	
	April 8,2010 - create this procedure
*********************************/
delimiter GO
DROP PROCEDURE IF EXISTS procFixItemsPurchaseAmount
GO

create procedure procFixItemsPurchaseAmount()
BEGIN
	
	UPDATE tblTransactionItems SET PurchaseAmount = Purchaseprice * Quantity;

	UPDATE tblTransactionItems SET PurchaseAmount = PurchaseAmount * -1 WHERE PurchaseAmount < 0;
	
END;
GO
delimiter ;


/*********************************
	procPositionInsert
	Lemuel E. Aceron
	CALL procPositionInsert();
	
	September 21, 2010 - create this procedure
*********************************/
delimiter GO
DROP PROCEDURE IF EXISTS procPositionInsert
GO

create procedure procPositionInsert(
	IN pvtPositionCode VARCHAR(30),
	IN pvtPositionName VARCHAR(30))
BEGIN

	INSERT INTO tblPositions(PositionCode, PositionName)
	VALUES (pvtPositionCode, pvtPositionName);
		
END;
GO
delimiter ;


/*********************************
	procPositionUpdate
	Lemuel E. Aceron
	CALL procPositionUpdate();
	
	September 21, 2010 - create this procedure
*********************************/
delimiter GO
DROP PROCEDURE IF EXISTS procPositionUpdate
GO

create procedure procPositionUpdate(
	IN pvtPositionID INT(10),
	IN pvtPositionCode VARCHAR(30),
	IN pvtPositionName VARCHAR(30))
BEGIN

	UPDATE tblPositions SET 
		PositionCode	= pvtPositionCode, 
		PositionName	= pvtPositionName
	WHERE PositionID	= pvtPositionID;
		
END;
GO
delimiter ;

/*********************************
	procDepartmentInsert
	Lemuel E. Aceron
	CALL procDepartmentInsert();
	
	September 21, 2010 - create this procedure
*********************************/
delimiter GO
DROP PROCEDURE IF EXISTS procDepartmentInsert
GO

create procedure procDepartmentInsert(
	IN pvtDepartmentCode VARCHAR(30),
	IN pvtDepartmentName VARCHAR(30))
BEGIN

	INSERT INTO tblDepartments(DepartmentCode, DepartmentName)
	VALUES (pvtDepartmentCode, pvtDepartmentName);
		
END;
GO
delimiter ;


/*********************************
	procDepartmentUpdate
	Lemuel E. Aceron
	CALL procDepartmentUpdate();
	
	September 21, 2010 - create this procedure
*********************************/
delimiter GO
DROP PROCEDURE IF EXISTS procDepartmentUpdate
GO

create procedure procDepartmentUpdate(
	IN pvtDepartmentID INT(10),
	IN pvtDepartmentCode VARCHAR(30),
	IN pvtDepartmentName VARCHAR(30))
BEGIN

	UPDATE tblDepartments SET 
		DepartmentCode	= pvtDepartmentCode, 
		DepartmentName	= pvtDepartmentName
	WHERE DepartmentID	= pvtDepartmentID;
		
END;
GO
delimiter ;

/*********************************
	procTransactionRelease
	Lemuel E. Aceron
	May 3, 2011
	For releasing of closed transaction.
*********************************/

DROP PROCEDURE IF EXISTS procTransactionRelease;
delimiter GO

create procedure procTransactionRelease(IN lngTransactionID BIGINT(20), 
										IN intMonth SMALLINT(2) UNSIGNED ZEROFILL, 
										IN intTransactionStatus SMALLINT,
										IN lngReleasedByID BIGINT(20),
										IN strReleasedByName VARCHAR(100))
BEGIN
	SET @SQL = CONCAT('UPDATE tblTransactions', intMonth,' SET ');
	SET @SQL = CONCAT(@SQL,'	TransactionStatus=', intTransactionStatus,', ');
	SET @SQL = CONCAT(@SQL,'	ReleaserID=', lngReleasedByID,', ');
	SET @SQL = CONCAT(@SQL,'	ReleaserName=''', strReleasedByName,''', ');
	SET @SQL = CONCAT(@SQL,'	ReleasedDate=NOW() ');
	SET @SQL = CONCAT(@SQL,'WHERE TransactionID=',lngTransactionID,'; ');
		
	PREPARE strCmd FROM @SQL;
	EXECUTE strCmd;
	DEALLOCATE PREPARE strCmd;
	
	
END;
GO
delimiter ;

/*********************************
call procTransactionRelease(1,1,9,1,'Administrator');
*********************************/

/**************************************************************

	procProductUpdateActualQuantity
	Lemuel E. Aceron
	March 14, 2009

	CALL procProductUpdateActualQuantity();

**************************************************************/
delimiter GO
DROP PROCEDURE IF EXISTS procProductUpdateActualQuantity
GO

create procedure procProductUpdateActualQuantity(
						IN intBranchID INT(4),
						IN lngProductID bigint,
						IN decQuantity numeric)
BEGIN
	IF (lngProductID = 0) THEN
		UPDATE tblBranchInventory SET ActualQuantity = decQuantity AND BranchID = intBranchID;
	ELSE
		UPDATE tblBranchInventory SET ActualQuantity = decQuantity WHERE ProductID = lngProductID AND BranchID = intBranchID;
	END IF;
	
	IF (lngProductID = 0) THEN
		UPDATE tblProducts SET ActualQuantity = (SELECT SUM(ActualQuantity) FROM tblBranchInventory z WHERE tblProducts.ProductID = z.ProductID);
	ELSE
		UPDATE tblProducts SET ActualQuantity = (SELECT SUM(ActualQuantity) FROM tblBranchInventory z WHERE tblProducts.ProductID = z.ProductID) WHERE ProductID = lngProductID;
	END IF;
END;
GO
delimiter ;

/**************************************************************

	procCloseInventory
	Lemuel E. Aceron
	March 14, 2009

	CALL procCloseInventory(1, '2011-07-26', '00001', 1, 'RetailPlus', true);

**************************************************************/
delimiter GO
DROP PROCEDURE IF EXISTS procCloseInventory
GO

create procedure procCloseInventory(IN intBranchID INT(4),
									IN lngUID bigint, 
									IN dteClosingDate datetime,
									IN strReferenceNo varchar(30),
									IN lngContactID bigint,
									IN strContactCode varchar(150),
									IN bolUseVariationAsReference TINYINT(1))
BEGIN
	
	DECLARE lngProductID, lngMatrixID BIGINT DEFAULT 0;
	DECLARE decProductQuantity, decProductActualQuantity, decMatrixTotalQuantity DECIMAL(18,3) DEFAULT 0;
	DECLARE decMinThreshold, decMaxThreshold, decPurchasePrice DECIMAL(18,3) DEFAULT 0;
	DECLARE strProductCode VARCHAR(30) DEFAULT '';
	DECLARE strDescription VARCHAR(50) DEFAULT '';
	DECLARE strMatrixDescription VARCHAR(255) DEFAULT '';
	DECLARE dtePostingDateFrom, dtePostingDateTo DATETIME;
	DECLARE strRemarks VARCHAR(100) DEFAULT '';
	DECLARE intUnitID INT DEFAULT 0;
	DECLARE strUnitCode VARCHAR(5) DEFAULT '';
	DECLARE done INT DEFAULT 0;
	DECLARE lngCtr, lngCount bigint DEFAULT 0;
	DECLARE curItems CURSOR FOR SELECT ProductID, Quantity, ActualQuantity, ProductCode, ProductDesc, a.BaseUnitID, UnitCode, a.MinThreshold, a.MaxThreshold, PurchasePrice 
								FROM tblProducts a INNER JOIN tblUnit b ON a.BaseUnitID = b.UnitID
								WHERE Quantity <> ActualQuantity ; 
	DECLARE CONTINUE HANDLER FOR NOT FOUND SET done = 1;
	
	SELECT COUNT(*) INTO lngCount FROM tblProducts a INNER JOIN tblUnit b ON a.BaseUnitID = b.UnitID WHERE Quantity <> ActualQuantity;
	
	--	get the posting dates
	SELECT PostingDateFrom, PostingDateTo INTO dtePostingDateFrom, dtePostingDateTo FROM tblERPConfig;
	
	INSERT INTO tblInventory (BranchID, PostingDateFrom, PostingDateTo, PostingDate, 
									ReferenceNo, ContactID, ContactCode, 
									ProductID, ProductCode, VariationMatrixID, MatrixDescription, 
									ClosingQuantity, ClosingActualQuantity, ClosingVAT, ClosingCost, PurchasePrice)  
									SELECT intBranchID, dtePostingDateFrom, dtePostingDateTo, dteClosingDate,
										strReferenceNo, lngContactID, strContactCode, 
										ProductID, ProductCode, 0, '',
										Quantity, ActualQuantity, 
										PurchasePrice * ActualQuantity * 0.12, 
										PurchasePrice * ActualQuantity, PurchasePrice
									FROM tblProducts a INNER JOIN tblUnit b ON a.BaseUnitID = b.UnitID 
										WHERE a.Deleted = 0 AND Active = 1 AND Quantity = ActualQuantity;
									
	OPEN curItems;
	curItems: LOOP
		SET lngCtr = lngCtr + 1; 
		IF (lngCtr > lngCount) THEN LEAVE curItems; END IF;
		
		FETCH curItems INTO lngProductID, decProductQuantity, decProductActualQuantity, strProductCode, strDescription, intUnitID, strUnitCode, decMinThreshold, decMaxThreshold, decPurchasePrice;
		-- For testing: SELECT lngProductID, decProductQuantity, decProductActualQuantity, strProductCode, strDescription, intUnitID, strUnitCode, decMinThreshold, decMaxThreshold, decPurchasePrice;
		
		-- STEP 1: get the last Matrix to be udpated
		SET lngMatrixID = 0; SET strMatrixDescription = '';
		
		SELECT MatrixID, Description INTO lngMatrixID, strMatrixDescription FROM tblProductBaseVariationsMatrix WHERE Deleted = 0 AND ProductID = lngProductID ORDER BY MatrixID DESC LIMIT 1;
		
		IF (ISNULL(lngMatrixID)) THEN SET lngMatrixID = 0; END IF; 
		IF (ISNULL(strMatrixDescription)) THEN SET strMatrixDescription = ''; END IF;
		
		-- STEP 2: get the total Quantity of all Matrix
		SET decMatrixTotalQuantity = 0;
		IF (lngMatrixID <> 0) THEN SELECT SUM(Quantity) INTO decMatrixTotalQuantity FROM tblProductBaseVariationsMatrix WHERE Deleted = 0 AND ProductID = lngProductID; END IF;
		
		-- STEP 3: Overwrite the PurchasePrice from tblProductBaseVariationsMatrix if with Matrix
		IF (lngMatrixID <> 0) THEN SELECT AVG(PurchasePrice) INTO decPurchasePrice FROM tblProductBaseVariationsMatrix WHERE Deleted = 0 AND ProductID = lngProductID; END IF;
		
		-- For testing: SELECT lngProductID, decProductQuantity, decProductActualQuantity, strProductCode, strDescription, intUnitID, strUnitCode, decMinThreshold, decMaxThreshold, decPurchasePrice;
		
		-- STEP 4: Insert to tblInventory
		INSERT INTO tblInventory (intBranchID, PostingDateFrom, PostingDateTo, PostingDate, 
									ReferenceNo, ContactID, ContactCode, 
									ProductID, ProductCode, VariationMatrixID, MatrixDescription, 
									ClosingQuantity, ClosingActualQuantity, ClosingVAT, ClosingCost, PurchasePrice) VALUES (
									dtePostingDateFrom, dtePostingDateTo, dteClosingDate,
									strReferenceNo, lngContactID, strContactCode, 
									lngProductID, strProductCode, 0, '',
									decProductQuantity, decProductActualQuantity, 
									decPurchasePrice * decProductActualQuantity * 0.12, 
									decPurchasePrice * decProductActualQuantity, decPurchasePrice);
		
		-- STEP 6: IF Matrix Total Quantity is not equal to Product Quantity
		--		   auto adjust the last Matrix (using matrixid) quantity of the correct quantity
		IF (bolUseVariationAsReference = 0) THEN
			IF (decMatrixTotalQuantity <> decProductActualQuantity) THEN
				UPDATE tblProductBaseVariationsMatrix SET Quantity = decProductActualQuantity - decMatrixTotalQuantity + Quantity WHERE MatrixID = lngMatrixID ;
			END IF;
		ELSE
			UPDATE tblProductBaseVariationsMatrix SET Quantity = ActualQuantity WHERE ProductID = lngProductID;
		END IF;
		
		-- STEP 7: set the value of stRemarks, see the administrator for the list of constant remarks
		SET strRemarks = 'SYSTEM AUTO ADJUSTMENT OF PRODUCT QTY FROM INVENTORY CLOSING-ACTUAL PRODUCT QTY AS BASIS';
			
		-- STEP 8: Insert to product movement history
		CALL procProductMovementInsert(lngProductID, strProductCode, strDescription, lngMatrixID, strMatrixDescription, 
										decProductQuantity, decProductActualQuantity -decProductQuantity, decProductActualQuantity, decProductActualQuantity, 
										strUnitCode, strRemarks, now(), strReferenceNo, 'SYSTEM', intBranchID, intBranchID, 0);
		
		-- STEP 9: Insert to inventory adjustment
		CALL procInvAdjustmentInsert(lngUID, dteClosingDate, lngProductID, strProductCode, strDescription, lngMatrixID,
												strMatrixDescription, intUnitID, strUnitCode, decProductQuantity, decProductActualQuantity, 
												decMinThreshold, decMinThreshold, decMaxThreshold, decMaxThreshold, CONCAT(strRemarks, ' ', strReferenceNo));
		
		-- STEP 10: auto adjust the quantity based on actual quantity
		UPDATE tblProducts SET Quantity = decProductActualQuantity WHERE ProductID = lngProductID;
		
		SET lngProductID = 0; SET strProductCode = ''; 
		SET lngMatrixID = 0; SET strMatrixDescription = '';
		SET decPurchasePrice = 0; SET decProductQuantity = 0; SET decProductActualQuantity = 0;
			
	END LOOP curItems;
	CLOSE curItems;
	
	UPDATE tblProducts SET QuantityIN = 0;
	UPDATE tblProducts SET QuantityOUT = 0;
	
	UPDATE tblProductBaseVariationsMatrix SET QuantityIN = 0;
	UPDATE tblProductBaseVariationsMatrix SET QuantityOUT = 0;
	
	
	IF (bolUseVariationAsReference = 0) THEN
		CALL procSyncProductVariationFromQuantityAllItem();
	END IF;
END;
GO
delimiter ;


/**************************************************************

	procSyncProductVariationFromQuantity
	Lemuel E. Aceron
	March 14, 2009

	CALL procSyncProductVariationFromQuantityPerItem(9, 1);

	Jul 26, 2011 : Lemu
	- Added Insert to product movement history
	- Added Insert to inventory adjustment
	
**************************************************************/
delimiter GO
DROP PROCEDURE IF EXISTS procSyncProductVariationFromQuantityPerItem
GO

create procedure procSyncProductVariationFromQuantityPerItem(IN lngProductID BIGINT, IN intBranchID INT)
BEGIN
	DECLARE strTransactionNo VARCHAR(30) DEFAULT '';
	DECLARE lngMatrixID BIGINT DEFAULT 0;
	DECLARE strProductCode VARCHAR(30) DEFAULT '';
	DECLARE strProductDesc VARCHAR(50) DEFAULT '';
	DECLARE strMatrixDescription VARCHAR(255) DEFAULT '';
	DECLARE intUnitID INT DEFAULT 0;
	DECLARE strUnitCode VARCHAR(5) DEFAULT '';
	DECLARE decProductQuantity, decProductActualQuantity, decMinThreshold, decMaxThreshold DECIMAL(18,3) DEFAULT 0;
	DECLARE decMatrixQuantity, decMatrixTotalQuantity DECIMAL(18,3) DEFAULT 0;
	DECLARE strRemarks VARCHAR(100) DEFAULT '';
	
	-- STEP 1: get the last Matrix to be udpated
	SET lngMatrixID = 0; SET strMatrixDescription = '';
	
	-- Set the value of strMatrixDescription, decMatrixQuantity
	/********** update main product **********/
	SELECT MatrixID, Description, Quantity INTO lngMatrixID, strMatrixDescription, decMatrixQuantity FROM tblProductBaseVariationsMatrix WHERE Deleted = 0 AND ProductID = lngProductID ORDER BY MatrixID DESC LIMIT 1;
	/********** end update main product **********/
	
	SELECT a.MatrixID, Description, b.Quantity INTO lngMatrixID, strMatrixDescription, decMatrixQuantity FROM tblProductBaseVariationsMatrix a INNER JOIN tblBranchInventoryMatrix b ON a.MatrixID = b.MatrixID AND a.ProductID = b.ProductID WHERE a.Deleted = 0 AND a.ProductID = lngProductID AND BranchID = intBranchID ORDER BY MatrixID DESC LIMIT 1;
	
	IF (ISNULL(lngMatrixID)) THEN SET lngMatrixID = 0; END IF; 
	IF (ISNULL(strMatrixDescription)) THEN SET strMatrixDescription = ''; END IF;
	
	SET decMatrixTotalQuantity = 0;
	IF (lngMatrixID <> 0) THEN
			
		/********** update main product **********/
		-- STEP 2.a: get the total Quantity of all Matrix
		SELECT SUM(Quantity) INTO decMatrixTotalQuantity FROM tblProductBaseVariationsMatrix WHERE Deleted = 0 AND ProductID = lngProductID; 
		
		-- STEP 2.b: get the Quantity of product
		--			 Set the value of strProductCode, strProductDesc, decProductQuantity, strUnitCode
		SELECT ProductCode, ProductDesc, Quantity, ActualQuantity, BaseUnitID, UnitCode, MinThreshold, MaxThreshold INTO 
				strProductCode, strProductDesc, decProductQuantity, decProductActualQuantity, intUnitID, strUnitCode, decMinThreshold, decMaxThreshold
		FROM tblProducts a INNER JOIN tblUnit b ON a.BaseUnitID = b.UnitID WHERE ProductID = lngProductID;
		
		/********** end update main product **********/
		
		-- STEP 2.a: get the total Quantity of all Matrix
		SELECT SUM(b.Quantity) INTO decMatrixTotalQuantity FROM tblProductBaseVariationsMatrix a INNER JOIN tblBranchInventoryMatrix b ON a.MatrixID = b.MatrixID AND a.ProductID = b.ProductID AND a.ProductID = lngProductID AND BranchID = intBranchID; 
		
		-- STEP 2.b: get the Quantity of product
		--			 Set the value of strProductCode, strProductDesc, decProductQuantity, strUnitCode
		SELECT ProductCode, ProductDesc, c.Quantity, c.ActualQuantity, BaseUnitID, UnitCode, MinThreshold, MaxThreshold INTO 
				strProductCode, strProductDesc, decProductQuantity, decProductActualQuantity, intUnitID, strUnitCode, decMinThreshold, decMaxThreshold
		FROM tblProducts a INNER JOIN tblUnit b ON a.BaseUnitID = b.UnitID 
		INNER JOIN tblBranchInventory c ON a.ProductID = c.ProductID WHERE a.ProductID = lngProductID AND BranchID = intBranchID;
		
		
		-- STEP 3: IF Matrix Total Quantity is not equal to Product Quantity
		--		   auto adjust the last Matrix (using matrixid) quantity of the correct quantity
		IF (decMatrixTotalQuantity <> decProductQuantity) THEN
			
			-- STEP 3.a: set values for tblProductMovement history
			-- Set the value of strTransactionNo
			SET strTransactionNo = (SELECT CONCAT('SAADJ-', EndingTransactionNo) AS strTransactionNo FROM tblTerminalReport LIMIT 1);
			
			-- set the value of stRemarks, see the administrator for the list of constant remarks
			SET strRemarks = 'SYSTEM AUTO ADJUSTMENT OF MATRIX QTY FROM PRODUCT QTY AS BASIS';
				
			-- STEP 3.b: Insert to product movement history
			CALL procProductMovementInsert(lngProductID, strProductCode, strProductDesc, lngMatrixID, strMatrixDescription, 
											decProductQuantity, 0, decProductQuantity, decProductQuantity - decMatrixTotalQuantity + decMatrixQuantity, 
											strUnitCode, strRemarks, now(), strTransactionNo, 'SYSTEM', intBranchID, intBranchID, 0);
			
			-- STEP 3.c: Insert to inventory adjustment
			CALL procInvAdjustmentInsert(1, now(), lngProductID, strProductCode, strProductDesc, lngMatrixID,
														strMatrixDescription, intUnitID, strUnitCode, decProductQuantity, decProductQuantity, 
														decMinThreshold, decMinThreshold, decMaxThreshold, decMaxThreshold, CONCAT(strRemarks, ' ', strTransactionNo));
														
			-- STEP 3.c: Do the actual adjustment
			/********** update main product **********/
			UPDATE tblProductBaseVariationsMatrix SET Quantity = decProductQuantity - decMatrixTotalQuantity + Quantity WHERE MatrixID = lngMatrixID ;
			/********** end update main product **********/
			
			UPDATE tblBranchInventoryMatrix SET Quantity = decProductQuantity - decMatrixTotalQuantity + Quantity WHERE MatrixID = lngMatrixID AND BranchID = intBranchID;

		END IF;
	END IF;
END;
GO
delimiter ; 

/**************************************************************

	procSyncProductVariationFromQuantityAllItem
	Lemuel E. Aceron
	March 14, 2009

	CALL procSyncProductVariationFromQuantityAllItem(1);

	Mar 14, 2011 : Lemu
	- create this procedure
	
**************************************************************/
delimiter GO
DROP PROCEDURE IF EXISTS procSyncProductVariationFromQuantityAllItem
GO

create procedure procSyncProductVariationFromQuantityAllItem(IN intBranchID INT(4))
BEGIN
	DECLARE lngProductID BIGINT DEFAULT 0;
	DECLARE done INT DEFAULT 0;
	DECLARE lngCtr, lngCount BIGINT DEFAULT 0;
	DECLARE curItems CURSOR FOR SELECT ProductID FROM tblProducts a
									WHERE Quantity <> (SELECT SUM(Quantity) FROM tblProductBaseVariationsMatrix b WHERE b.Deleted = 0 AND a.ProductID = b.ProductID) ; 
	DECLARE CONTINUE HANDLER FOR NOT FOUND SET done = 1;
	
	SELECT COUNT(*) INTO lngCount FROM tblProducts a INNER JOIN tblUnit b ON a.BaseUnitID = b.UnitID WHERE Quantity <> (SELECT SUM(Quantity) FROM tblProductBaseVariationsMatrix b WHERE b.Deleted = 0 AND a.ProductID = b.ProductID) ; 
	
	OPEN curItems;
	curItems: LOOP
		SET lngCtr = lngCtr + 1; 
		IF (lngCtr > lngCount) THEN LEAVE curItems; END IF;
		
		-- Fetch the ProductID to be processed 
		FETCH curItems INTO lngProductID;
		
		-- Process the actual sync of product
		CALL procSyncProductVariationFromQuantityPerItem(lngProductID, intBranchID);
		
		-- reset the ProductID to be processed
		SET lngProductID = 0;
			
	END LOOP curItems;
	CLOSE curItems;
	
END;
GO
delimiter ;


/********************************************
	procProductMovementInsert
	
	CALL procProductMovementInsert(9, 'test', 'test desc', 0, 'test matrix desc', 100, 30, 130, 100, 'PC', 'remarks', '2011-07-26 00:00:00', 'PO-MPC20110000010858', 'Lemuel', 1, 1, 0);
	
	Jul 26, 2011 : Lemu
	- create this procedure
	
	Oct 28, 2011 : Lemu
	- include BranchIDFrom, BranchIDTo to include inventory per branch.
	
********************************************/
delimiter GO
DROP PROCEDURE IF EXISTS procProductMovementInsert
GO

create procedure procProductMovementInsert(
	IN lngProductID BIGINT,
	IN strProductCode VARCHAR(30),
	IN strProductDesc VARCHAR(50),
	IN lngMatrixID BIGINT,
	IN strMatrixDescription VARCHAR(100),
	IN decQuantityFrom DECIMAL(18,2),
	IN decQuantity DECIMAL(18,2),
	IN decQuantityTo DECIMAL(18,2),
	IN decMatrixQuantity DECIMAL(18,2),
	IN strUnitCode VARCHAR(5),
	IN strRemarks VARCHAR(100),
	IN dteTransactionDate DateTime,
	IN strTransactionNo VARCHAR(100),
	IN strCreatedBy VARCHAR(100),
	IN intBranchIDFrom INT(4),
	IN intBranchIDTo INT(4),
	IN intQuantityMovementType INT(1)
	)
BEGIN

	INSERT INTO tblProductMovement (ProductID,
									ProductCode,
									ProductDescription,
									MatrixID,
									MatrixDescription,
									QuantityFrom,
									Quantity,
									QuantityTo,
									MatrixQuantity,
									UnitCode,
									Remarks,
									TransactionDate,
									TransactionNo,
									CreatedBy,
									BranchIDFrom,
									BranchIDTo,
									QuantityMovementType)
							VALUES( lngProductID,
									strProductCode,
									strProductDesc,
									lngMatrixID,
									strMatrixDescription,
									decQuantityFrom,
									decQuantity,									
									decQuantityTo,
									decMatrixQuantity,
									strUnitCode,
									strRemarks,
									dteTransactionDate,
									strTransactionNo,
									strCreatedBy,
									intBranchIDFrom,
									intBranchIDTo,
									intQuantityMovementType);
									
END;
GO
delimiter ;

/********************************************
	procProductAddQuantity
	
	CALL procProductAddQuantity(2, 2715, 2581, 10, 'purchase', '2011-07-26 00:00:00', 'PO-MPC20110000010858', 'Lemuel');
	
	Jul 26, 2011 : Lemu
	- create this procedure
	
	Oct 28, 2011 : Lemu
	- include inventory per branch
	
********************************************/
delimiter GO
DROP PROCEDURE IF EXISTS procProductAddQuantity
GO

create procedure procProductAddQuantity(
	IN intBranchID INT(4),
	IN lngProductID BIGINT,
	IN lngMatrixID BIGINT,
	IN decQuantity DECIMAL(18,2),
	IN strRemarks VARCHAR(100),
	IN dteTransactionDate DateTime,
	IN strTransactionNo VARCHAR(100),
	IN strCreatedBy VARCHAR(100)
	)
BEGIN
	DECLARE strProductCode VARCHAR(30) DEFAULT '';
	DECLARE strProductDesc VARCHAR(50) DEFAULT '';
	DECLARE strMatrixDescription VARCHAR(255) DEFAULT '';
	DECLARE strUnitCode VARCHAR(5) DEFAULT '';
	DECLARE decProductQuantity DECIMAL(18,3) DEFAULT 0;
	DECLARE decQuantityDiff DECIMAL(18,3) DEFAULT 0;

	/*********** add to main ***********/	
	-- Set the value of strProductCode, strProductDesc, decProductQuantity, strUnitCode
	SELECT ProductCode, ProductDesc, UnitCode, IFNULL(Description,'')
		INTO strProductCode, strProductDesc, strUnitCode, strMatrixDescription
	FROM tblProducts a 
	INNER JOIN tblUnit b ON a.BaseUnitID = b.UnitID 
	LEFT OUTER JOIN tblProductBaseVariationsMatrix mtrx ON mtrx.ProductID = a.ProductID AND mtrx.MatrixID = lngMatrixID
	WHERE a.Deleted = 0 AND a.ProductID = lngProductID AND IFNULL(mtrx.MatrixID,0) = lngMatrixID;
	
	SELECT IFNULL(SUM(Quantity),0) INTO decProductQuantity
	FROM tblProductInventory inv
	WHERE inv.BranchID = intBranchID AND inv.ProductID = lngProductID;
	
	SET decQuantityDiff = decQuantity - decProductQuantity;

	-- Insert to product movement history
	CALL procProductMovementInsert(lngProductID, strProductCode, strProductDesc, lngMatrixID, strMatrixDescription, 
									decProductQuantity, decQuantity, decProductQuantity + decQuantity, 0, 
									strUnitCode, strRemarks, dteTransactionDate, strTransactionNo, strCreatedBy, intBranchID, intBranchID, 0);
	
	IF EXISTS(SELECT Quantity FROM tblProductInventory WHERE ProductID = lngProductID AND MatrixID = lngMatrixID AND BranchID = intBranchID) THEN 
		IF decQuantity >= 0 THEN
			UPDATE tblProductInventory SET
				Quantity	= decQuantity + Quantity,
				QuantityIN  = decQuantity + QuantityIN
			WHERE ProductID = lngProductID AND MatrixID = lngMatrixID AND BranchID = intBranchID;
		ELSE
			UPDATE tblProductInventory SET
				Quantity	= decQuantity + Quantity,
				QuantityOut = decQuantity + QuantityOut
			WHERE ProductID = lngProductID AND MatrixID = lngMatrixID AND BranchID = intBranchID;
		END IF;
	ELSE
		IF decQuantity >= 0 THEN
			INSERT INTO tblProductInventory(BranchID, ProductID, MatrixID, Quantity, QuantityIN, QuantityOut)
			VALUES(intBranchID, lngProductID, lngMatrixID, decQuantity, decQuantity, 0);
		ELSE
			INSERT INTO tblProductInventory(BranchID, ProductID, MatrixID, Quantity, QuantityIN, QuantityOut)
			VALUES(intBranchID, lngProductID, lngMatrixID, decQuantity, 0, decQuantity);
		END IF;
	END IF;
									
	/*********** end add to main ***********/	
	
	-- Tag product as Active if quantity > 0
	IF (SELECT SUM(Quantity) FROM tblProductInventory WHERE ProductID = lngProductID) > 0 THEN
		CALL procProductTagActiveInactive(lngProductID, 1);
	END IF;

	-- Process sync of product that are sold without matrix but with existing matrix now
	-- CALL procSyncProductVariationFromQuantityPerItem(lngProductID, intBranchID);
END;
GO
delimiter ;


/********************************************
	procProductSubtractQuantity
	
	CALL procProductSubtractQuantity(2, 2715, 484, 3, 'SALES', '2011-07-26 00:00:00', 'PO-MPC20110000010858', 'Lemuel');
	
	Jul 26, 2011 : Lemu
	- create this procedure
	
	Oct 28, 2011 : Lemu
	- include inventory per branch
	
********************************************/
delimiter GO
DROP PROCEDURE IF EXISTS procProductSubtractQuantity
GO

create procedure procProductSubtractQuantity(
	IN intBranchID INT(4),
	IN lngProductID BIGINT,
	IN lngMatrixID BIGINT,
	IN decQuantity DECIMAL(18,2),
	IN strRemarks VARCHAR(100),
	IN dteTransactionDate DateTime,
	IN strTransactionNo VARCHAR(100),
	IN strCreatedBy VARCHAR(100)
	)
BEGIN
	DECLARE strProductCode VARCHAR(30) DEFAULT '';
	DECLARE strProductDesc VARCHAR(50) DEFAULT '';
	DECLARE strMatrixDescription VARCHAR(255) DEFAULT '';
	DECLARE strUnitCode VARCHAR(5) DEFAULT '';
	DECLARE decProductQuantity DECIMAL(18,3) DEFAULT 0;
	DECLARE decQuantityDiff DECIMAL(18,3) DEFAULT 0;

	/*********** subtract from main ***********/

	-- Set the value of strProductCode, strProductDesc, decProductQuantity, strUnitCode
	SELECT ProductCode, ProductDesc, UnitCode, IFNULL(Description,'')
		INTO strProductCode, strProductDesc, strUnitCode, strMatrixDescription
	FROM tblProducts a 
	INNER JOIN tblUnit b ON a.BaseUnitID = b.UnitID 
	LEFT OUTER JOIN tblProductBaseVariationsMatrix mtrx ON mtrx.ProductID = a.ProductID AND mtrx.MatrixID = lngMatrixID
	WHERE a.Deleted = 0 AND a.ProductID = lngProductID AND IFNULL(mtrx.MatrixID,0) = lngMatrixID;
	
	SELECT IFNULL(SUM(Quantity),0) INTO decProductQuantity
	FROM tblProductInventory inv
	WHERE inv.BranchID = intBranchID AND inv.ProductID = lngProductID;
	
	-- Insert to product movement history
	CALL procProductMovementInsert(lngProductID, strProductCode, strProductDesc, lngMatrixID, strMatrixDescription, 
									decProductQuantity, -1 * decQuantity, decProductQuantity - decQuantity, 0, 
									strUnitCode, strRemarks, dteTransactionDate, strTransactionNo, strCreatedBy, intBranchID, intBranchID, 0);
	
	-- Subtract the quantity from Product table
	UPDATE tblProductInventory SET 
		Quantity	= Quantity - decQuantity, QuantityOut	= QuantityOut + decQuantity
	WHERE MatrixID	= lngMatrixID 
		AND ProductID = lngProductID
		AND BranchID = intBranchID;
		
	/*********** end subtract from main ***********/
	
	-- Tag product as InActive if quantity <= 0
	IF (SELECT ShowItemMoreThanZeroQty FROM tblTerminal WHERE TerminalID = 1) = 1 THEN
		IF (SELECT SUM(Quantity) FROM tblProductInventory WHERE ProductID = lngProductID) = 0 THEN
			CALL procProductTagActiveInactive(lngProductID, 0);
		END IF;
	END IF;
	
	-- Process sync of product that are returned without matrix but with existing matrix now
	-- CALL procSyncProductVariationFromQuantityPerItem(lngProductID, intBranchID);
									
END;
GO
delimiter ;


/**************************************************************

	procProductMovementSelect

	Jul 26, 2011 : Lemu
	- create this procedure

	CALL procProductMovementSelect(3924, '', '');
	
**************************************************************/
delimiter GO
DROP PROCEDURE IF EXISTS procProductMovementSelect
GO

create procedure procProductMovementSelect(
									IN lngProductID BIGINT, 
									IN dteStartTransactionDate DATETIME,
									in dteEndTransactionDate DATETIME)
BEGIN
	SET @SQL := '';
	
	SET @SQL := 'SELECT
						ProductID,
						ProductCode, 
						ProductDescription,
						MatrixID,
						MatrixDescription, 
						QuantityFrom,
						Quantity,
						QuantityTo,
						matrixQuantity,
						UnitCode,
						Remarks,
						TransactionDate,
						TransactionNo,
						CreatedBy
					FROM tblProductMovement
					WHERE QuantityMovementType = 0 ';
	
	IF (lngProductID <> 0) THEN
		SET @SQL = CONCAT(@SQL,'AND ProductID = ', lngProductID,' ');
	END IF;
	
	
	IF (DATE_FORMAT(dteStartTransactionDate, '%Y%m%d')  <> DATE_FORMAT('1900-01-01', '%Y-%m-%d')) THEN
		SET @SQL = CONCAT(@SQL,'AND TransactionDate >= ''', dteStartTransactionDate,''' ');
	END IF;
	
	IF (DATE_FORMAT(dteEndTransactionDate, '%Y%m%d')  <> DATE_FORMAT('1900-01-01', '%Y-%m-%d')) THEN
		SET @SQL = CONCAT(@SQL,'AND TransactionDate <= ''', dteEndTransactionDate,''' ');
	END IF;
	
	SET @SQL = CONCAT(@SQL,'ORDER BY TransactionDate DESC, QuantityTo ASC ');

	PREPARE stmt FROM @SQL;
	EXECUTE stmt;
	DEALLOCATE PREPARE stmt;
	
END;
GO
delimiter ;


/**************************************************************

	procProductUpdateRIDByPO

	Aug 26, 2011 : Lemu
	- create this procedure

	CALL procProductUpdateRIDByPO(10);
	
**************************************************************/
delimiter GO
DROP PROCEDURE IF EXISTS procProductUpdateRIDByPO
GO

create procedure procProductUpdateRIDByPO(IN lngPOID BIGINT)
BEGIN
	DECLARE lngProductID, lngRID BIGINT DEFAULT 0;
	DECLARE done INT DEFAULT 0;
	DECLARE lngCtr, lngCount BIGINT DEFAULT 0;
	DECLARE curItems CURSOR FOR SELECT ProductID, RID FROM tblPOItems WHERE POID = lngPOID; 
	DECLARE CONTINUE HANDLER FOR NOT FOUND SET done = 1;
	
	SELECT COUNT(ProductID) INTO lngCount FROM tblPOItems WHERE POID = lngPOID; 
	
	OPEN curItems;
	curItems: LOOP
		SET lngCtr = lngCtr + 1; 
		IF (lngCtr > lngCount) THEN LEAVE curItems; END IF;
		
		-- Fetch the ProductID, lngRID to be processed 
		FETCH curItems INTO lngProductID, lngRID;
		
		-- Process the actual update of product RID
		CALL procProductUpdateRID(lngProductID, lngRID);
		
		-- reset the ProductID, lngRID to be processed
		SET lngProductID = 0; SET lngRID = 0;
			
	END LOOP curItems;
	CLOSE curItems;
	
END;
GO
delimiter ;

/**************************************************************

	procProductUpdateRID

	Aug 26, 2011 : Lemu
	- create this procedure

	CALL procProductUpdateRID(3924, 0);
	
**************************************************************/
delimiter GO
DROP PROCEDURE IF EXISTS procProductUpdateRID
GO

create procedure procProductUpdateRID(
									IN lngProductID BIGINT, 
									IN lngRID BIGINT)
BEGIN
	-- Update the RID to Products table
	UPDATE tblProducts SET 
		RID	= lngRID 
	WHERE ProductID	= lngProductID;
	
END;
GO
delimiter ;


/**************************************************************

	procUpdateProductReorderOverStockPerProduct

	Aug 26, 2011 : Lemu
	- create this procedure

	CALL procUpdateProductReorderOverStockPerProduct(3139, '2011-09-28 00:00:00', '2011-09-28 23:59:59');
	
**************************************************************/
delimiter GO
DROP PROCEDURE IF EXISTS procUpdateProductReorderOverStockPerProduct
GO

create procedure procUpdateProductReorderOverStockPerProduct(IN lngProductID BIGINT, IN dteStartDate DATETIME, IN dteEndDate DATETIME)
BEGIN
	DECLARE lngCtr, lngCount BIGINT DEFAULT 0;
	DECLARE strSessionID VARCHAR(15);
	
	SELECT DATEDIFF(dteEndDate,dteStartDate) + 1, rand()  INTO lngCount, strSessionID;
	SET lngCtr = 0; 
	REPEAT 
		SET lngCtr = lngCtr + 1; 
		INSERT INTO tblCountingRef(SessionID, Counter, ReferenceDate) VALUES(strSessionID, lngCtr, DATE_ADD(dteStartDate, INTERVAL lngCtr-1 DAY));
		UNTIL lngCtr = lngCount 
	END REPEAT;
	
	CALL procUpdateProductReorderOverStockPerProductID(lngProductID, strSessionID, dteStartDate, dteEndDate);
	
	DELETE FROM tblCountingRef WHERE SessionID = strSessionID;
	
END;
GO
delimiter ;

/**************************************************************

	procUpdateProductReorderOverStockPerProductID

	Aug 26, 2011 : Lemu
	- create this procedure

	CALL procUpdateProductReorderOverStockPerProductID(3011, 1, '2011-09-25 00:00:00', '2011-09-27 23:59:59');
	
**************************************************************/
delimiter GO
DROP PROCEDURE IF EXISTS procUpdateProductReorderOverStockPerProductID
GO

create procedure procUpdateProductReorderOverStockPerProductID(IN lngProductID BIGINT, IN strSessionID VARCHAR(15), IN dteStartDate DATETIME, IN dteEndDate DATETIME)
BEGIN
	DECLARE lngRID BIGINT DEFAULT 0;
	DECLARE intAvgCounter INT DEFAULT 0;
	DECLARE lngCtr, lngCount BIGINT DEFAULT 0;
	DECLARE decQuantity, decTotalQuantity, decAverageSales, decTotalAverageSales, decIDC DECIMAL(18,3) DEFAULT 0;
	DECLARE intValidTransactionItemStatus INTEGER DEFAULT 0;
	DECLARE intOrderSlipItemStatus INTEGER DEFAULT 5;
	
	SELECT Quantity, RID INTO decQuantity, lngRID FROM tblProducts WHERE ProductID = lngProductID;
	
	SET intValidTransactionItemStatus = 0; SET intOrderSlipItemStatus = 5;
	SET intAvgCounter = 0; SET decTotalAverageSales = 0;
	
	-- SELECT * FROM tblCountingRef;
	-- SELECT lngProductID, decAverageSales, decQuantity, decIDC, lngRID,  (decTotalQuantity - decQuantity) AS ReorderQty, decTotalAverageSales, intAvgCounter;
	
	-- Get the average sales
	SELECT AVG(Quantity) INTO decAverageSales FROM 
			( SELECT ReferenceDate, IFNULL(AVG(Quantity), 0) AS Quantity FROM tblCountingRef a
									LEFT JOIN tblTransactions b ON a.ReferenceDate = CAST(b.TransactionDate AS DATE)
									LEFT JOIN tblTransactionItems c ON b.TransactionID = c.TransactionID
													AND (TransactionItemStatus = intValidTransactionItemStatus OR TransactionItemStatus = intOrderSlipItemStatus)
													AND ProductID = lngProductID
													AND TransactionDate BETWEEN dteStartDate AND dteEndDate
									WHERE SessionID = strSessionID GROUP BY ReferenceDate) AS tblTransactionItems;
	SET intAvgCounter = intAvgCounter + 1; SET decTotalAverageSales = decTotalAverageSales + decAverageSales;
	
	SET decAverageSales = decTotalAverageSales / intAvgCounter;
	
	-- Get the Inventory Days Covered of the existing Quantity
	-- SET decIDC = decQuantity / decAverageSales;
	IF decQuantity = 0 THEN
		SET decIDC = 0;
	ELSEIF decAverageSales = 0 THEN
		SET decIDC = decQuantity;
	ELSE
		SET decIDC = decQuantity / decAverageSales;
	END IF;
	  
	-- Get the daily average sales will be used as RIDMinThreshold
	IF decIDC <> 0 THEN
		IF (lngRID > decIDC) THEN
			SET decTotalQuantity = (lngRID * cdbl(decIDC)) - cdbl(decQuantity);
		ELSE
			SET decTotalQuantity = 0;
		END IF; 
	ELSE
		SET decTotalQuantity = 0;
	END IF; 
		
	-- IF (decIDC > lngRID) THEN 
	-- 	SET decTotalQuantity = decQuantity;
	-- ELSE
	-- 	SET decTotalQuantity = round(lngRID - decIDC) * decAverageSales;
	-- END IF; 
	
	-- For checking purposes uncomment this
	-- SELECT * FROM tblCountingRef;
	-- SELECT lngProductID, decAverageSales, decQuantity, decIDC, lngRID,  (decTotalQuantity - decQuantity) AS ReorderQty, decTotalAverageSales, intAvgCounter;
	
	-- Set the RIDMinThreshold and RIDMaxThreshold
	UPDATE tblProducts SET RIDMinThreshold = round(IFNULL(decAverageSales, 0), 2), RIDMaxThreshold = round(IFNULL(decTotalQuantity, 0), 2) WHERE ProductID = lngProductID;
	UPDATE tblProductBaseVariationsMatrix SET RIDMinThreshold = round(IFNULL(decAverageSales, 0), 2), RIDMaxThreshold = round(IFNULL(decTotalQuantity, 0), 2) WHERE ProductID = lngProductID;
	
END;
GO
delimiter ;

/**************************************************************

	procUpdateProductReorderOverStockPerSupplierPerRID

	Aug 26, 2011 : Lemu
	- create this procedure

	CALL procUpdateProductReorderOverStockPerSupplierPerRID(1019, 10, '2011-09-10', '2011-09-16');
	
**************************************************************/
delimiter GO
DROP PROCEDURE IF EXISTS procUpdateProductReorderOverStockPerSupplierPerRID
GO

create procedure procUpdateProductReorderOverStockPerSupplierPerRID(IN lngSupplierID BIGINT, IN lngRID BIGINT, IN dteStartDate DATETIME, IN dteEndDate DATETIME)
BEGIN
	DECLARE lngProductID BIGINT DEFAULT 0;
	DECLARE lngCtr, lngCount BIGINT DEFAULT 0;
	DECLARE strSessionID VARCHAR(15);
	DECLARE curItems CURSOR FOR SELECT ProductID FROM tblProducts WHERE SupplierID = lngSupplierID OR ProductID IN (SELECT DISTINCT(ProductID) FROM tblProductBaseVariationsMatrix WHERE Deleted = 0 AND SupplierID = lngSupplierID) ; 
	
	SELECT DATEDIFF(dteEndDate,dteStartDate) + 1, rand()  INTO lngCount, strSessionID;
	SET lngCtr = 0; 
	REPEAT 
		SET lngCtr = lngCtr + 1; 
		INSERT INTO tblCountingRef(SessionID, Counter, ReferenceDate) VALUES(strSessionID, lngCtr, DATE_ADD(dteStartDate, INTERVAL lngCtr-1 DAY));
		UNTIL lngCtr = lngCount 
	END REPEAT;
	
	SET lngCtr = 0; 
	SELECT COUNT(ProductID) INTO lngCount FROM tblProducts WHERE SupplierID = lngSupplierID OR ProductID IN (SELECT DISTINCT(ProductID) FROM tblProductBaseVariationsMatrix WHERE Deleted = 0 AND SupplierID = lngSupplierID) ;
	OPEN curItems;
	curItems: LOOP
		SET lngCtr = lngCtr + 1;
		IF (lngCtr > lngCount) THEN LEAVE curItems; END IF;
		
		-- Fetch the ProductID to be processed 
		FETCH curItems INTO lngProductID;
		
		-- Process the ProductID
		CALL procUpdateProductReorderOverStockPerProductID(lngProductID, strSessionID, dteStartDate, dteEndDate);
		
		-- reset the ProductID to be processed
		SET lngProductID = 0;
		
	END LOOP curItems;
	CLOSE curItems;
	DELETE FROM tblCountingRef WHERE SessionID = strSessionID;
	
END;
GO
delimiter ;

/**************************************************************

	procUpdateProductReorderOverStockPerSupplier

	Sep 14, 2011 : Lemu
	- create this procedure

	CALL procUpdateProductReorderOverStockPerSupplier(1019, '2011-09-01', '2011-09-06');
	
**************************************************************/
delimiter GO
DROP PROCEDURE IF EXISTS procUpdateProductReorderOverStockPerSupplier
GO

create procedure procUpdateProductReorderOverStockPerSupplier(IN lngSupplierID BIGINT, IN dteStartDate DATETIME, IN dteEndDate DATETIME)
BEGIN
	DECLARE lngProductID BIGINT DEFAULT 0;
	DECLARE lngCtr, lngCount BIGINT DEFAULT 0;
	DECLARE strSessionID VARCHAR(15);
	DECLARE curItems CURSOR FOR SELECT ProductID FROM tblProducts WHERE SupplierID = lngSupplierID OR ProductID IN (SELECT DISTINCT(ProductID) FROM tblProductBaseVariationsMatrix WHERE Deleted = 0 AND SupplierID = lngSupplierID) ; 
	
	SELECT DATEDIFF(dteEndDate,dteStartDate) + 1, rand()  INTO lngCount, strSessionID;
	SET lngCtr = 0; 
	REPEAT 
		SET lngCtr = lngCtr + 1; 
		INSERT INTO tblCountingRef(SessionID, Counter, ReferenceDate) VALUES(strSessionID, lngCtr, DATE_ADD(dteStartDate, INTERVAL lngCtr-1 DAY));
		UNTIL lngCtr = lngCount 
	END REPEAT;
	
	SET lngCtr = 0;
	SELECT COUNT(ProductID) INTO lngCount FROM tblProducts WHERE SupplierID = lngSupplierID OR ProductID IN (SELECT DISTINCT(ProductID) FROM tblProductBaseVariationsMatrix WHERE Deleted = 0 AND SupplierID = lngSupplierID) ;
	OPEN curItems;
	curItems: LOOP
		SET lngCtr = lngCtr + 1;
		IF (lngCtr > lngCount) THEN LEAVE curItems; END IF;
		
		-- Fetch the ProductID to be processed 
		FETCH curItems INTO lngProductID;
		
		-- Process the ProductID
		CALL procUpdateProductReorderOverStockPerProductID(lngProductID, strSessionID, dteStartDate, dteEndDate);
		
		-- reset the ProductID to be processed
		SET lngProductID = 0;
		
	END LOOP curItems;
	CLOSE curItems;
	DELETE FROM tblCountingRef WHERE SessionID = strSessionID;
	
END;
GO
delimiter ;

/**************************************************************

	procUpdateProductReorderOverStockPerSupplierPerGroup

	Sep 14, 2011 : Lemu
	- create this procedure

	CALL procUpdateProductReorderOverStockPerSupplierPerGroup(1019, 1, '2011-09-01', '2011-09-06');
	
**************************************************************/
delimiter GO
DROP PROCEDURE IF EXISTS procUpdateProductReorderOverStockPerSupplierPerGroup
GO

create procedure procUpdateProductReorderOverStockPerSupplierPerGroup(IN lngSupplierID BIGINT, IN lngGroupID BIGINT, IN dteStartDate DATETIME, IN dteEndDate DATETIME)
BEGIN
	DECLARE lngProductID BIGINT DEFAULT 0;
	DECLARE lngCtr, lngCount BIGINT DEFAULT 0;
	DECLARE strSessionID VARCHAR(15);
	DECLARE curItems CURSOR FOR SELECT ProductID FROM tblProducts WHERE SupplierID = lngSupplierID OR ProductID IN (SELECT DISTINCT(ProductID) FROM tblProductBaseVariationsMatrix WHERE Deleted = 0 AND SupplierID = lngSupplierID) 
																		AND ProductSubGroupID IN (SELECT ProductSubGroupID FROM tblProductSubGroup WHERE ProductGroupID = lngGroupID); 
	
	SELECT DATEDIFF(dteEndDate,dteStartDate) + 1, rand()  INTO lngCount, strSessionID;
	SET lngCtr = 0; 
	REPEAT 
		SET lngCtr = lngCtr + 1; 
		INSERT INTO tblCountingRef(SessionID, Counter, ReferenceDate) VALUES(strSessionID, lngCtr, DATE_ADD(dteStartDate, INTERVAL lngCtr-1 DAY));
		UNTIL lngCtr = lngCount 
	END REPEAT;
	
	SET lngCtr = 0;
	SELECT COUNT(ProductID) INTO lngCount FROM tblProducts WHERE SupplierID = lngSupplierID OR ProductID IN (SELECT DISTINCT(ProductID) FROM tblProductBaseVariationsMatrix WHERE Deleted = 0 AND SupplierID = lngSupplierID) 
																		AND ProductSubGroupID IN (SELECT ProductSubGroupID FROM tblProductSubGroup WHERE ProductGroupID = lngGroupID);
	OPEN curItems;
	curItems: LOOP
		SET lngCtr = lngCtr + 1;
		IF (lngCtr > lngCount) THEN LEAVE curItems; END IF;
		
		-- Fetch the ProductID to be processed 
		FETCH curItems INTO lngProductID;
		
		-- Process the ProductID
		CALL procUpdateProductReorderOverStockPerProductID(lngProductID, strSessionID, dteStartDate, dteEndDate);
		
		-- reset the ProductID to be processed
		SET lngProductID = 0;
		
	END LOOP curItems;
	CLOSE curItems;
	DELETE FROM tblCountingRef WHERE SessionID = strSessionID;
	
END;
GO
delimiter ;


/**************************************************************

	procUpdateProductReorderOverStockPerSupplierPerSubGroup

	Sep 14, 2011 : Lemu
	- create this procedure

	CALL procUpdateProductReorderOverStockPerSupplierPerSubGroup(1019, 1, '2011-09-01', '2011-09-06');
	
**************************************************************/
delimiter GO
DROP PROCEDURE IF EXISTS procUpdateProductReorderOverStockPerSupplierPerSubGroup
GO

create procedure procUpdateProductReorderOverStockPerSupplierPerSubGroup(IN lngSupplierID BIGINT, IN lngSubGroupID BIGINT, IN dteStartDate DATETIME, IN dteEndDate DATETIME)
BEGIN
	DECLARE lngProductID BIGINT DEFAULT 0;
	DECLARE lngCtr, lngCount BIGINT DEFAULT 0;
	DECLARE strSessionID VARCHAR(15);
	DECLARE curItems CURSOR FOR SELECT ProductID FROM tblProducts WHERE SupplierID = lngSupplierID OR ProductID IN (SELECT DISTINCT(ProductID) FROM tblProductBaseVariationsMatrix WHERE Deleted = 0 AND SupplierID = lngSupplierID) 
																		AND ProductSubGroupID = lngSubGroupID;
	
	SELECT DATEDIFF(dteEndDate,dteStartDate) + 1, rand()  INTO lngCount, strSessionID;
	SET lngCtr = 0; 
	REPEAT 
		SET lngCtr = lngCtr + 1; 
		INSERT INTO tblCountingRef(SessionID, Counter, ReferenceDate) VALUES(strSessionID, lngCtr, DATE_ADD(dteStartDate, INTERVAL lngCtr-1 DAY));
		UNTIL lngCtr = lngCount 
	END REPEAT;
	
	SET lngCtr = 0;
	SELECT COUNT(ProductID) INTO lngCount FROM tblProducts WHERE SupplierID = lngSupplierID OR ProductID IN (SELECT DISTINCT(ProductID) FROM tblProductBaseVariationsMatrix WHERE Deleted = 0 AND SupplierID = lngSupplierID) 
																		AND ProductSubGroupID = lngSubGroupID;																		
	OPEN curItems;
	curItems: LOOP
		SET lngCtr = lngCtr + 1;
		IF (lngCtr > lngCount) THEN LEAVE curItems; END IF;
		
		-- Fetch the ProductID to be processed 
		FETCH curItems INTO lngProductID;
		
		-- Process the ProductID
		CALL procUpdateProductReorderOverStockPerProductID(lngProductID, strSessionID, dteStartDate, dteEndDate);
		
		-- reset the ProductID to be processed
		SET lngProductID = 0;
		
	END LOOP curItems;
	CLOSE curItems;
	DELETE FROM tblCountingRef WHERE SessionID = strSessionID;
	
END;
GO
delimiter ;

/**************************************************************

	procUpdateProductReorderOverStockPerGroup

	Sep 14, 2011 : Lemu
	- create this procedure

	CALL procUpdateProductReorderOverStockPerGroup(1, '2011-09-01', '2011-09-06');
	
**************************************************************/
delimiter GO
DROP PROCEDURE IF EXISTS procUpdateProductReorderOverStockPerGroup
GO

create procedure procUpdateProductReorderOverStockPerGroup(IN lngGroupID BIGINT, IN dteStartDate DATETIME, IN dteEndDate DATETIME)
BEGIN
	DECLARE lngProductID BIGINT DEFAULT 0;
	DECLARE lngCtr, lngCount BIGINT DEFAULT 0;
	DECLARE strSessionID varchar(15);
	DECLARE curItems CURSOR FOR SELECT ProductID FROM tblProducts WHERE ProductSubGroupID IN (SELECT ProductSubGroupID FROM tblProductSubGroup WHERE ProductGroupID = lngGroupID); 
	
	SELECT DATEDIFF(dteEndDate,dteStartDate) + 1, rand()  INTO lngCount, strSessionID;
	SET lngCtr = 0; 
	REPEAT 
		SET lngCtr = lngCtr + 1; 
		INSERT INTO tblCountingRef(SessionID, Counter, ReferenceDate) VALUES(strSessionID, lngCtr, DATE_ADD(dteStartDate, INTERVAL lngCtr-1 DAY));
		UNTIL lngCtr = lngCount 
	END REPEAT;
	
	SET lngCtr = 0;
	SELECT COUNT(ProductID) INTO lngCount FROM tblProducts tblProducts WHERE ProductSubGroupID IN (SELECT ProductSubGroupID FROM tblProductSubGroup WHERE ProductGroupID = lngGroupID);
	OPEN curItems;
	curItems: LOOP
		SET lngCtr = lngCtr + 1;
		IF (lngCtr > lngCount) THEN LEAVE curItems; END IF;
		
		-- Fetch the ProductID to be processed 
		FETCH curItems INTO lngProductID;
		
		-- Process the ProductID
		CALL procUpdateProductReorderOverStockPerProductID(lngProductID, strSessionID, dteStartDate, dteEndDate);
		
		-- reset the ProductID to be processed
		SET lngProductID = 0;
		
	END LOOP curItems;
	CLOSE curItems;
	DELETE FROM tblCountingRef WHERE SessionID = strSessionID;
	
END;
GO
delimiter ;


/**************************************************************

	procUpdateProductReorderOverStockPerSubGroup

	Sep 14, 2011 : Lemu
	- create this procedure

	CALL procUpdateProductReorderOverStockPerSubGroup(1, '2011-09-01', '2011-09-06');
	
**************************************************************/
delimiter GO
DROP PROCEDURE IF EXISTS procUpdateProductReorderOverStockPerSubGroup
GO

create procedure procUpdateProductReorderOverStockPerSubGroup(IN lngSubGroupID BIGINT, IN dteStartDate DATETIME, IN dteEndDate DATETIME)
BEGIN
	DECLARE lngProductID BIGINT DEFAULT 0;
	DECLARE lngCtr, lngCount BIGINT DEFAULT 0;
	DECLARE strSessionID varchar(15);
	DECLARE curItems CURSOR FOR SELECT ProductID FROM tblProducts WHERE ProductSubGroupID = lngSubGroupID; 
	
	SELECT DATEDIFF(dteEndDate,dteStartDate) + 1, rand()  INTO lngCount, strSessionID;
	SET lngCtr = 0; 
	REPEAT 
		SET lngCtr = lngCtr + 1; 
		INSERT INTO tblCountingRef(SessionID, Counter, ReferenceDate) VALUES(strSessionID, lngCtr, DATE_ADD(dteStartDate, INTERVAL lngCtr-1 DAY));
		UNTIL lngCtr = lngCount 
	END REPEAT;
	
	SET lngCtr = 0;
	SELECT COUNT(ProductID) INTO lngCount FROM tblProducts tblProducts WHERE ProductSubGroupID = lngSubGroupID; 
	OPEN curItems;
	curItems: LOOP
		SET lngCtr = lngCtr + 1;
		IF (lngCtr > lngCount) THEN LEAVE curItems; END IF;
		
		-- Fetch the ProductID to be processed 
		FETCH curItems INTO lngProductID;
		
		-- Process the ProductID
		CALL procUpdateProductReorderOverStockPerProductID(lngProductID, strSessionID, dteStartDate, dteEndDate);
		
		-- reset the ProductID to be processed
		SET lngProductID = 0;
		
	END LOOP curItems;
	CLOSE curItems;
	DELETE FROM tblCountingRef WHERE SessionID = strSessionID;
	
END;
GO
delimiter ;


/**************************************************************

	procUpdateProductReorderOverStock

	Sep 14, 2011 : Lemu
	- create this procedure

	CALL procUpdateProductReorderOverStock('2011-09-01', '2011-09-06');
	
**************************************************************/
delimiter GO
DROP PROCEDURE IF EXISTS procUpdateProductReorderOverStock
GO

create procedure procUpdateProductReorderOverStock(IN dteStartDate DATETIME, IN dteEndDate DATETIME)
BEGIN
	DECLARE lngProductID BIGINT DEFAULT 0;
	DECLARE lngCtr, lngCount BIGINT DEFAULT 0;
	DECLARE strSessionID VARCHAR(15);
	DECLARE curItems CURSOR FOR SELECT ProductID FROM tblProducts; 
	
	SELECT DATEDIFF(dteEndDate,dteStartDate) + 1, rand()  INTO lngCount, strSessionID;
	SET lngCtr = 0; 
	REPEAT 
		SET lngCtr = lngCtr + 1; 
		INSERT INTO tblCountingRef(SessionID, Counter, ReferenceDate) VALUES(strSessionID, lngCtr, DATE_ADD(dteStartDate, INTERVAL lngCtr-1 DAY));
		UNTIL lngCtr = lngCount 
	END REPEAT;
	
	SET lngCtr = 0;
	SELECT COUNT(ProductID) INTO lngCount FROM tblProducts;
	OPEN curItems;
	curItems: LOOP
		SET lngCtr = lngCtr + 1;
		IF (lngCtr > lngCount) THEN LEAVE curItems; END IF;
		
		-- Fetch the ProductID to be processed 
		FETCH curItems INTO lngProductID;
		
		-- Process the ProductID
		CALL procUpdateProductReorderOverStockPerProductID(lngProductID, strSessionID, dteStartDate, dteEndDate);
		
		-- reset the ProductID to be processed
		SET lngProductID = 0;
		
	END LOOP curItems;
	CLOSE curItems;
	
END;
GO
delimiter ;


/**************************************************************
	procContactRewardsAddPoint
	Lemuel E. Aceron
	CALL procContactRewardsAddPoint();
	
	September 14, 2011 - create this procedure
**************************************************************/
delimiter GO
DROP PROCEDURE IF EXISTS procContactRewardsAddPoint
GO

create procedure procContactRewardsAddPoint(
	IN pvtContactID BIGINT(20),
	IN pvtRewardPoint DECIMAL(18,3))
BEGIN

	UPDATE tblContactRewards SET RewardPoints =	RewardPoints + pvtRewardPoint WHERE CustomerID = pvtContactID;
		
END;
GO
delimiter ;


/**************************************************************
	procContactRewardsDeductPoint
	Lemuel E. Aceron
	CALL procContactRewardsDeductPoint();
	
	September 14, 2011 - create this procedure
**************************************************************/
delimiter GO
DROP PROCEDURE IF EXISTS procContactRewardsDeductPoint
GO

create procedure procContactRewardsDeductPoint(
	IN pvtContactID BIGINT(20),
	IN pvtRewardPoint DECIMAL(18,3))
BEGIN

	UPDATE tblContactRewards SET RewardPoints =	RewardPoints - pvtRewardPoint WHERE CustomerID = pvtContactID;
	
	UPDATE tblContactRewards SET RedeemedPoints =	RedeemedPoints + pvtRewardPoint WHERE CustomerID = pvtContactID;
		
END;
GO
delimiter ;

/**************************************************************
	procContactRewardsAddPurchase
	Lemuel E. Aceron
	CALL procContactRewardsAddPurchase();
	
	September 14, 2011 - create this procedure
**************************************************************/
delimiter GO
DROP PROCEDURE IF EXISTS procContactRewardsAddPurchase
GO

create procedure procContactRewardsAddPurchase(
	IN pvtContactID BIGINT(20),
	IN pvtAmount DECIMAL(18,3))
BEGIN

	UPDATE tblContactRewards SET TotalPurchases =	TotalPurchases + pvtAmount WHERE CustomerID = pvtContactID;
		
END;
GO
delimiter ;

/**************************************************************
	procContactRewardModify
	Lemuel E. Aceron
	CALL procContactRewardModify(2885);
	
**************************************************************/
delimiter GO
DROP PROCEDURE IF EXISTS procContactRewardModify
GO

create procedure procContactRewardModify(IN lngCustomerID BIGINT, 
										IN strRewardCardNo VARCHAR(15), 
										IN intRewardActive TINYINT(1),
										IN decPoints DECIMAL(18,3),
										IN dteRewardAwardDate DATETIME,
										IN intRewardCardStatus TINYINT(1),
										IN dteExpiryDate DATE,
										IN dteBirthDate DATE)
BEGIN
	
	IF (NOT EXISTS(SELECT RewardCardNo FROM tblContactRewards WHERE RewardCardNo = strRewardCardNo)) THEN
		IF (NOT EXISTS(SELECT RewardCardNo FROM tblContactRewards WHERE CustomerID = lngCustomerID)) THEN
			INSERT INTO tblContactRewards(CustomerID, RewardCardNo, RewardActive, RewardPoints, RewardAwardDate, RewardCardStatus, ExpiryDate, BirthDate) 
								  VALUES(lngCustomerID, strRewardCardNo, intRewardActive, decPoints, dteRewardAwardDate, intRewardCardStatus, dteExpiryDate, dteBirthDate);
		ELSE
			UPDATE tblContactRewards SET
				RewardCardNo = strRewardCardNo,
				RewardActive = intRewardActive,
				RewardAwardDate = dteRewardAwardDate,
				RewardCardStatus = intRewardCardStatus,
				ExpiryDate = dteExpiryDate,
				BirthDate = dteBirthDate
			WHERE
				CustomerID = lngCustomerID;
		END IF;
	ELSE
		UPDATE tblContactRewards SET
			RewardCardNo = strRewardCardNo,
			RewardActive = intRewardActive,
			RewardAwardDate = dteRewardAwardDate,
			RewardCardStatus = intRewardCardStatus,
			ExpiryDate = dteExpiryDate,
			BirthDate = dteBirthDate
		WHERE
			RewardCardNo = strRewardCardNo;
	END IF;
	
	/*******************************
		CALL procContactRewardModify(1, '100000001', 1, 0, '2011-09-01');
	*******************************/
END;
GO
delimiter ;

/**************************************************************
	procContactRewardExpire
	Lemuel E. Aceron
	CALL procContactRewardExpire();
	
**************************************************************/
delimiter GO
DROP PROCEDURE IF EXISTS procContactRewardExpire
GO

create procedure procContactRewardExpire()
BEGIN
	DECLARE intRewardActive TINYINT(1) DEFAULT 0;
	DECLARE intRewardCardStatusExpired TINYINT(1) DEFAULT 2;
	
	INSERT INTO tblContactRewardsMovement (
		CustomerID, RewardDate, RewardPointsBefore, RewardPointsAdjustment, RewardPointsAfter,
		RewardExpiryDate, RewardReason, TerminalNo, CashierName, TransactionNo)
	SELECT CustomerID, RewardAwardDate, RewardPoints, 0, RewardPoints, 
		ExpiryDate, 'SYSTEM AUTO EXPIRE', '01', 'SYSTEM', DATE_FORMAT(NOW(), '%Y%m%d%H%i') 
		FROM tblContactRewards
		WHERE DATE_FORMAT(ExpiryDate, '%Y-%m-%d') < DATE_FORMAT(NOW(), '%Y-%m-%d')
			AND DATE_FORMAT(ExpiryDate, '%Y-%m-%d') <> DATE_FORMAT('1900-01-01', '%Y-%m-%d')
			AND RewardCardStatus <> intRewardCardStatusExpired
			AND RewardActive <> intRewardActive;
	
	UPDATE tblContactRewards SET 
			RewardActive = intRewardActive,
			RewardCardStatus = intRewardCardStatusExpired
		WHERE DATE_FORMAT(ExpiryDate, '%Y-%m-%d') < DATE_FORMAT(NOW(), '%Y-%m-%d')
			AND DATE_FORMAT(ExpiryDate, '%Y-%m-%d') <> DATE_FORMAT('1900-01-01', '%Y-%m-%d')
			AND RewardCardStatus <> intRewardCardStatusExpired
			AND RewardActive <> intRewardActive;
	
	/*******************************
		CALL procContactRewardExpire();
	*******************************/
END;
GO
delimiter ;

/**************************************************************
	procContactRewardsMovementInsert
	Lemuel E. Aceron
	CALL procContactRewardsMovementInsert();
	
	September 14, 2011 - create this procedure
**************************************************************/
delimiter GO
DROP PROCEDURE IF EXISTS procContactRewardsMovementInsert
GO

create procedure procContactRewardsMovementInsert(
	IN lngCustomerID BIGINT(20),
	IN dteRewardDate DATETIME,
	IN decRewardPointsBefore DECIMAL(18,3),
	IN decRewardPointsAdjustment DECIMAL(18,3),
	IN decRewardPointsAfter DECIMAL(18,3),
	IN dteRewardExpiryDate DATE,
	IN strRewardReason VARCHAR(150),
	IN strTerminalNo VARCHAR(10),
	IN strCashierName VARCHAR(150),
	IN strTransactionNo VARCHAR(15))
BEGIN

	INSERT INTO tblContactRewardsMovement (
		CustomerID, RewardDate, RewardPointsBefore, RewardPointsAdjustment, RewardPointsAfter,
		RewardExpiryDate, RewardReason, TerminalNo, CashierName, TransactionNo
	)VALUES(
		lngCustomerID, dteRewardDate, decRewardPointsBefore, decRewardPointsAdjustment, decRewardPointsAfter,
		dteRewardExpiryDate, strRewardReason, strTerminalNo, strCashierName, strTransactionNo
	);
	
	
END;
GO
delimiter ;


/**************************************************************

	procProductUpdateRewardPoints
	Lemuel E. Aceron
	March 14, 2009

	CALL procProductUpdateRewardPoints(0,0,0,2);

**************************************************************/
delimiter GO
DROP PROCEDURE IF EXISTS procProductUpdateRewardPoints
GO

create procedure procProductUpdateRewardPoints(
						IN lngProductGroupID BIGINT,
						IN lngProductSubGroupID BIGINT,
						IN lngProductID BIGINT,
						IN decRewardPoints NUMERIC)
BEGIN
	IF (lngProductID > 0) THEN
		UPDATE tblProducts SET RewardPoints = decRewardPoints WHERE ProductID = lngProductID;
	ELSEIF (lngProductSubGroupID > 0) THEN
		UPDATE tblProducts SET RewardPoints = decRewardPoints WHERE ProductSubGroupID = lngProductSubGroupID;
	ELSEIF (lngProductGroupID > 0) THEN
		UPDATE tblProducts SET RewardPoints = decRewardPoints WHERE ProductSubGroupID IN (SELECT DISTINCT ProductSubGroupID FROM tblProductSubGroup WHERE ProductGroupID = lngProductGroupID);
	ELSE
		UPDATE tblProducts SET RewardPoints = decRewardPoints;
	END IF;
	
END;
GO
delimiter ;

/**************************************************************

	procProductBaseVariationUpdateActualQuantity
	Lemuel E. Aceron
	Oct 24, 2011

	CALL procProductBaseVariationUpdateActualQuantity();

**************************************************************/
delimiter GO
DROP PROCEDURE IF EXISTS procProductBaseVariationUpdateActualQuantity
GO

create procedure procProductBaseVariationUpdateActualQuantity(
						IN lngProductID bigint,
						IN lngMatrixID bigint,
						IN decQuantity numeric)
BEGIN
	IF (lngMatrixID != 0) THEN
		UPDATE tblProductBaseVariationsMatrix SET ActualQuantity = decQuantity WHERE MatrixID = lngMatrixID;
	END IF;
	
	UPDATE tblProducts SET 
		ActualQuantity = (SELECT IFNULL(SUM(ActualQuantity),0) FROM tblProductBaseVariationsMatrix a WHERE a.Deleted = 0 AND a.ProductID = tblProducts.ProductID) 
	WHERE ProductID = lngProductID;
	
END;
GO
delimiter ;

/**************************************************************
	procProductBranchInventoryInsert
	Lemuel E. Aceron
	CALL procProductBranchInventoryInsert(1);
	
	Oct 6, 2009 - create this procedure
**************************************************************/
delimiter GO
DROP PROCEDURE IF EXISTS procProductBranchInventoryInsert
GO

create procedure procProductBranchInventoryInsert(IN lngProductID BIGINT)
BEGIN
	DECLARE lngCtr, lngCount bigint DEFAULT 0;
	DECLARE intBranchID INT(4) DEFAULT 0;
	DECLARE curBranches CURSOR FOR SELECT BranchID FROM tblBranch; 
	
	SELECT COUNT(*) INTO lngCount FROM tblBranch; 
	
	OPEN curBranches;
	curBranches: LOOP
		SET lngCtr = lngCtr + 1; 
		IF (lngCtr > lngCount) THEN LEAVE curBranches; END IF;
	
		FETCH curBranches INTO intBranchID;
		
		IF NOT EXISTS(SELECT ProductID FROM tblBranchInventory WHERE ProductID = lngProductID AND BranchID = intBranchID) THEN
			INSERT INTO tblBranchInventory(BranchID, ProductID)VALUES (intBranchID, lngProductID);
		END IF;

		INSERT INTO tblBranchInventoryMatrix(BranchID, ProductID, MatrixID, Quantity, QuantityIn)
		SELECT intBranchID, lngProductID, MatrixID, Quantity, QuantityIn FROM tblProductBaseVariationsMatrix 
										 WHERE ProductID = lngProductID 
												AND MatrixID NOT IN (SELECT DISTINCT MatrixID FROM tblBranchInventoryMatrix WHERE ProductID = lngProductID AND BranchID = intBranchID);
		
		SET intBranchID = 0;
	END LOOP curBranches;
	CLOSE curBranches;
END;
GO
delimiter ;

/**************************************************************
	procSyncProductVariationFromQuantityPerItemAllBranch
	Lemuel E. Aceron
	CALL procSyncProductVariationFromQuantityPerItemAllBranch(1);
	
	Oct 28, 2011 - create this procedure
**************************************************************/
delimiter GO
DROP PROCEDURE IF EXISTS procSyncProductVariationFromQuantityPerItemAllBranch
GO

create procedure procSyncProductVariationFromQuantityPerItemAllBranch(IN lngProductID BIGINT)
BEGIN
	DECLARE lngCtr, lngCount bigint DEFAULT 0;
	DECLARE intBranchID INT(4) DEFAULT 0;
	DECLARE curBranches CURSOR FOR SELECT BranchID FROM tblBranch; 
	
	SELECT COUNT(*) INTO lngCount FROM tblBranch; 
	
	OPEN curBranches;
	curBranches: LOOP
		SET lngCtr = lngCtr + 1; 
		IF (lngCtr > lngCount) THEN LEAVE curBranches; END IF;
	
		FETCH curBranches INTO intBranchID;
		
		-- Put the correct quantity of the newly created variation based on Product Quantity
		CALL procSyncProductVariationFromQuantityPerItem(lngProductID, intBranchID);
		
		SET intBranchID = 0;
	END LOOP curBranches;
	CLOSE curBranches;
END;
GO
delimiter ;

/**************************************************************
	procProductBranchInventoryMatrixInsert
	Lemuel E. Aceron
	CALL procProductBranchInventoryMatrixInsert(1);
	
	Oct 29, 2011 : Lemu - create this procedure
**************************************************************/
delimiter GO
DROP PROCEDURE IF EXISTS procProductBranchInventoryMatrixInsert
GO

create procedure procProductBranchInventoryMatrixInsert(IN lngProductID BIGINT, IN lngMatrixID BIGINT)
BEGIN
	DECLARE lngCtr, lngCount bigint DEFAULT 0;
	DECLARE intBranchID INT(4) DEFAULT 0;
	DECLARE curBranches CURSOR FOR SELECT BranchID FROM tblBranch WHERE BranchID <> 1; 
	
	SELECT COUNT(*) INTO lngCount FROM tblBranch WHERE BranchID <> 1; 
	
	OPEN curBranches;
	curBranches: LOOP
		SET lngCtr = lngCtr + 1; 
		IF (lngCtr > lngCount) THEN LEAVE curBranches; END IF;
	
		FETCH curBranches INTO intBranchID;
		
		IF NOT EXISTS(SELECT ProductID FROM tblBranchInventoryMatrix WHERE ProductID = lngProductID AND MatrixID = lngMatrixID AND BranchID = intBranchID) THEN
			INSERT INTO tblBranchInventory(BranchID, ProductID, MatrixID)VALUES (intBranchID, lngProductID, MatrixID);
		END IF;
		
		SET intBranchID = 0;
	END LOOP curBranches;
	CLOSE curBranches;
END;
GO
delimiter ;

/**************************************************************

	procProductBranchInventoryMatrixCopyAllItems
	Lemuel E. Aceron
	CALL procProductBranchInventoryMatrixCopyAllItems(1);
	
	Oct 29, 2011 : Lemu - create this procedure

**************************************************************/
delimiter GO
DROP PROCEDURE IF EXISTS procProductBranchInventoryMatrixCopyAllItems
GO

create procedure procProductBranchInventoryMatrixCopyAllItems()
BEGIN
	DECLARE lngCtr, lngCount bigint DEFAULT 0;
	DECLARE lngProductID, lngMatrixID BIGINT DEFAULT 0;
	DECLARE curItems CURSOR FOR SELECT ProductID, MatrixID FROM tblProductBaseVariationsMatrix WHERE Deleted = 0; 
	
	SELECT COUNT(MatrixID) INTO lngCount FROM tblProductBaseVariationsMatrix WHERE Deleted = 0; 
	
	OPEN curItems;
	curItems: LOOP
		SET lngCtr = lngCtr + 1; 
		IF (lngCtr > lngCount) THEN LEAVE curItems; END IF;
	
		FETCH curItems INTO lngProductID, lngMatrixID;
		
		CALL procProductBranchInventoryMatrixInsert(lngProductID, lngMatrixID);
		
		SET lngProductID = 0;
	END LOOP curItems;
	CLOSE curItems;
END;
GO
delimiter ;

/**************************************************************

	procProductBranchInventoryCopyAllItems
	Lemuel E. Aceron
	CALL procProductBranchInventoryCopyAllItems(1);
	
	Oct 29, 2011 : Lemu - create this procedure

**************************************************************/
delimiter GO
DROP PROCEDURE IF EXISTS procProductBranchInventoryCopyAllItems
GO

create procedure procProductBranchInventoryCopyAllItems()
BEGIN
	DECLARE lngCtr, lngCount bigint DEFAULT 0;
	DECLARE lngProductID BIGINT DEFAULT 0;
	DECLARE curItems CURSOR FOR SELECT ProductID FROM tblProducts WHERE Deleted = 0; 
	
	SELECT COUNT(ProductID) INTO lngCount FROM tblProducts WHERE Deleted = 0; 
	
	OPEN curItems;
	curItems: LOOP
		SET lngCtr = lngCtr + 1; 
		IF (lngCtr > lngCount) THEN LEAVE curItems; END IF;
	
		FETCH curItems INTO lngProductID;
		
		CALL procProductBranchInventoryInsert(lngProductID);
		
		SET lngProductID = 0;
	END LOOP curItems;
	CLOSE curItems;
END;
GO
delimiter ;

-- CALL procProductBranchInventoryCopyAllItems();

/**************************************************************
	procContactCreditModify
	Lemuel E. Aceron
	CALL procContactCreditModify(5, 5, 0, 'Egay', '2011-11-01 01:00:00', 0, '2012-11-01 01:00:00', 1, 1000);
	
**************************************************************/
delimiter GO
DROP PROCEDURE IF EXISTS procContactCreditModify
GO

create procedure procContactCreditModify(IN lngCustomerID BIGINT, 
										IN lngGuarantorID BIGINT, 
										IN intCreditType TINYINT(1),
										IN strCreditCardNo VARCHAR(15), 
										IN dteCreditAwardDate DATETIME,
										IN intCreditCardStatus TINYINT(1),
										IN dteExpiryDate DATE,
										IN intCreditActive TINYINT(1),
										IN decCreditLimit DECIMAL(18,3))
BEGIN
	
	UPDATE tblContacts SET
		IsCreditAllowed = intCreditActive,
		CreditLimit = decCreditLimit
	WHERE ContactID = lngCustomerID;
	
	IF (NOT EXISTS(SELECT CreditCardNo FROM tblContactCreditCardInfo WHERE CreditCardNo = strCreditCardNo)) THEN
		IF (NOT EXISTS(SELECT CreditCardNo FROM tblContactCreditCardInfo WHERE CustomerID = lngCustomerID)) THEN
			INSERT INTO tblContactCreditCardInfo(CustomerID, GuarantorID, CreditType, CreditCardNo, CreditAwardDate, CreditCardStatus, ExpiryDate) 
								  VALUES(lngCustomerID, lngGuarantorID, intCreditType, strCreditCardNo, dteCreditAwardDate, intCreditCardStatus, dteExpiryDate);
		ELSE
			UPDATE tblContactCreditCardInfo SET
				CreditCardNo = strCreditCardNo,
				CreditAwardDate = dteCreditAwardDate,
				CreditCardStatus = intCreditCardStatus,
				ExpiryDate = dteExpiryDate
			WHERE
				CustomerID = lngCustomerID;
		END IF;
	ELSE
		UPDATE tblContactCreditCardInfo SET
			CreditCardNo = strCreditCardNo,
			CreditAwardDate = dteCreditAwardDate,
			CreditCardStatus = intCreditCardStatus,
			ExpiryDate = dteExpiryDate
		WHERE
			CreditCardNo = strCreditCardNo;
	END IF;
	
	/*******************************
		CALL procContactCreditModify(1, '100000001', 1, 0, '2011-09-01');
	*******************************/
END;
GO
delimiter ;

/**************************************************************
	procContactCreditsAddPurchase
	Lemuel E. Aceron
	CALL procContactCreditsAddPurchase();
	
	September 14, 2011 - create this procedure
**************************************************************/
delimiter GO
DROP PROCEDURE IF EXISTS procContactCreditsAddPurchase
GO

create procedure procContactCreditsAddPurchase(
	IN pvtContactID BIGINT(20),
	IN pvtAmount DECIMAL(18,3))
BEGIN

	UPDATE tblContactCreditCardInfo SET TotalPurchases =	TotalPurchases + pvtAmount WHERE CustomerID = pvtContactID;
		
END;
GO
delimiter ;

/**************************************************************
	procContactCreditCardExpire
	Lemuel E. Aceron
	CALL procContactCreditCardExpire();
	
**************************************************************/
delimiter GO
DROP PROCEDURE IF EXISTS procContactCreditCardExpire
GO

create procedure procContactCreditCardExpire()
BEGIN
	DECLARE intCreditInActive TINYINT(1) DEFAULT 0;
	DECLARE intCreditCardStatusExpired TINYINT(1) DEFAULT 2;
	
	UPDATE tblContacts SET
		IsCreditAllowed = intCreditInActive
	WHERE ContactID IN (SELECT DISTINCT(CustomerID) FROM tblContactCreditCardInfo 
													WHERE DATE_FORMAT(ExpiryDate, '%Y-%m-%d') < DATE_FORMAT(NOW(), '%Y-%m-%d')
													AND DATE_FORMAT(ExpiryDate, '%Y-%m-%d') <> DATE_FORMAT('1900-01-01', '%Y-%m-%d')
													AND CreditCardStatus <> intCreditCardStatusExpired);
	
	UPDATE tblContactCreditCardInfo SET 
			CreditCardStatus = intCreditCardStatusExpired
		WHERE DATE_FORMAT(ExpiryDate, '%Y-%m-%d') < DATE_FORMAT(NOW(), '%Y-%m-%d')
			AND DATE_FORMAT(ExpiryDate, '%Y-%m-%d') <> DATE_FORMAT('1900-01-01', '%Y-%m-%d')
			AND CreditCardStatus <> intCreditCardStatusExpired;
	
	/*******************************
		CALL procContactCreditCardExpire();
	*******************************/
END;
GO
delimiter ;

/**************************************************************
	fnProductQuantityConvert
	[3/19/2012] get the converted string equivalent of the quantity.
	Lemuel E. Aceron
	CALL fnProductQuantityConvert();
	
**************************************************************/
delimiter GO
DROP FUNCTION IF EXISTS fnProductQuantityConvert
GO

create function fnProductQuantityConvert(
	lngProductID BIGINT,
	decProductQuantity DECIMAL(18,3),
	intProductUnitID INT
	) RETURNS VARCHAR(200) DETERMINISTIC
BEGIN
	DECLARE strRetValue VARCHAR(200) DEFAULT '';
	DECLARE lngCtr, lngCount BIGINT DEFAULT 0;
	DECLARE intBaseUnitID, intBottomUnitID, intComparedUnitID INT DEFAULT 0;
	DECLARE decBaseUnitValue, decBottomUnitValue, decConvertedWholeQty, decConvertedRemainderQty DECIMAL(18,3) DEFAULT 0;
	DECLARE strBaseUnitCode, strBottomUnitCode VARCHAR(5);
	DECLARE curItems CURSOR FOR SELECT PUM.BaseUnitID, PUM.BaseUnitValue, PUM.BottomUnitValue, PUM.BottomUnitID, BottU.UnitCode
								FROM tblProductUnitMatrix PUM LEFT JOIN tblUnit BottU ON PUM.BottomUnitID = BottU.UnitID 
								WHERE ProductID = lngProductID ORDER BY MatrixID ASC;
	
	SELECT COUNT(*) INTO lngCount FROM tblProductUnitMatrix PUM LEFT JOIN tblUnit BottU ON PUM.BottomUnitID = BottU.UnitID WHERE ProductID = lngProductID;
	
	IF (intProductUnitID = 0) THEN
		SELECT P.BaseUnitID, U.UnitCode INTO intComparedUnitID, strBaseUnitCode FROM tblProducts P INNER JOIN tblUnit U ON P.BaseUnitID = U.UnitID WHERE ProductID = lngProductID;
	ELSE
		SET intComparedUnitID = intProductUnitID;
		SELECT UnitCode INTO strBaseUnitCode FROM tblUnit WHERE UnitID = intProductUnitID;
	END IF;
	
	IF (lngCount > 0 AND decProductQuantity <> 0) THEN
		OPEN curItems;
		curItems: LOOP
			SET lngCtr = lngCtr + 1; 
			IF (lngCtr > lngCount) THEN LEAVE curItems; END IF;
			
			FETCH curItems INTO intBaseUnitID, decBaseUnitValue, decBottomUnitValue, intBottomUnitID, strBottomUnitCode;
			
			IF (intComparedUnitID = intBaseUnitID) THEN
				SET decConvertedRemainderQty = decProductQuantity MOD decBaseUnitValue;
				IF (decConvertedRemainderQty <> 0) THEN
					SET strRetValue = CONCAT(decConvertedRemainderQty, ' ', strBaseUnitCode, '; ', strRetValue);
					-- SET decProductQuantity = decProductQuantity - decConvertedRemainderQty;
				END IF;
				
				SET decProductQuantity = (decProductQuantity DIV decBaseUnitValue) * decBottomUnitValue;
				IF (decProductQuantity <> 0) THEN
					IF (lngCtr = lngCount) THEN
						SET strRetValue = CONCAT(decProductQuantity,' ', strBottomUnitCode,'; ', strRetValue);
					END IF;
				END IF;
				
				SET intComparedUnitID = intBottomUnitID;
				SET strBaseUnitCode = strBottomUnitCode;
			END IF;
			
			SET intBaseUnitID = 0;
			SET decBaseUnitValue = 0;
			SET decBottomUnitValue = 0;
			SET intBottomUnitID = 0;
			SET strBottomUnitCode = '';
		END LOOP curItems;
		CLOSE curItems;
		
		SET strRetValue = TRIM(strRetValue);
		SET strRetValue = LEFT(strRetValue, LENGTH(strRetValue) - 1);
	ELSE
		SET strRetValue = CONCAT(decProductQuantity,' ', strBaseUnitCode);
	END IF;
	
	RETURN strRetValue;
	/*******************************
		CALL fnProductQuantityConvert();
	*******************************/
END;
GO
delimiter ;

/**************************************************************
	procProductQuantityConvert
	Lemuel E. Aceron
	CALL procGetRewardPointsReport(0, null, null);
	
**************************************************************/
delimiter GO
DROP PROCEDURE IF EXISTS procGetRewardPointsReport
GO

create procedure procGetRewardPointsReport(
	lngCustomerID BIGINT,
	dteTransactionDateFrom DATETIME,
	dteTransactionDateTo DATETIME
	) 
BEGIN
	
	SET @SQL = CONCAT('SELECT BranchID 
								,TerminalNo 
								,CashierName
								,CustomerID
								,CustomerName
								,DATE_FORMAT(TransactionDate, ''%Y-%m-%d'') TransactionDate
								,COUNT(TransactionNo) TransactionCount
								,SUM(RewardPointsPayment) AS RewardPointsPayment
								,SUM(RewardConvertedPayment) AS RewardConvertedPayment
							FROM tblTransactions
							WHERE CustomerID <> 1 AND TransactionStatus = 1 ');

	IF (lngCustomerID <> 0) THEN
		SET @SQL = CONCAT(@SQL, 'AND CustomerID = ',lngCustomerID,' ');
	END IF;

	IF (NOT ISNULL(dteTransactionDateFrom) AND DATE_FORMAT(dteTransactionDateFrom, '%Y-%m-%d') <> DATE_FORMAT('1900-01-01', '%Y-%m-%d')) THEN
		SET @SQL = CONCAT(@SQL, 'AND DATE_FORMAT(TransactionDate, ''%Y-%m-%d'') >= DATE_FORMAT(''',dteTransactionDateFrom,''', ''%Y-%m-%d'') ');
	END IF;
	
	IF (NOT ISNULL(dteTransactionDateTo) AND DATE_FORMAT(dteTransactionDateTo, '%Y-%m-%d') <> DATE_FORMAT('1900-01-01', '%Y-%m-%d')) THEN
		SET @SQL = CONCAT(@SQL, 'AND DATE_FORMAT(TransactionDate, ''%Y-%m-%d'') <= DATE_FORMAT(''',dteTransactionDateTo,''', ''%Y-%m-%d'') ');
	END IF;

	SET @SQL = CONCAT(@SQL, '
							GROUP BY BranchID
								,TerminalNo
								,CashierName
								,CustomerID
								,CustomerName
								,DATE_FORMAT(TransactionDate, ''%Y-%m-%d'')
							ORDER BY BranchID
								,TerminalNo
								,CashierName
								,CustomerName
								,DATE_FORMAT(TransactionDate, ''%Y-%m-%d'')  ');

	PREPARE strCmd FROM @SQL;
	EXECUTE strCmd;
	DEALLOCATE PREPARE strCmd;
	/*******************************
		CALL procGetRewardPointsReport();
	*******************************/
END;
GO
delimiter ;

/**************************************************************

	procProductIsExist

	Jul 26, 2011 : Lemu
	- create this procedure

	CALL procProductIsExist(4, 'ADVNTGE CARD - REPLACEMENT FEE');
	
**************************************************************/
delimiter GO
DROP PROCEDURE IF EXISTS procProductIsExist
GO

create procedure procProductIsExist(
									IN lngProductID BIGINT, 
									IN strBarCode VARCHAR(30))
BEGIN
	SET @SQL := '';
	
	SET strBarCode = REPLACE(strBarCode, '''', '''''');

	SET @SQL := 'SELECT
						Count(1) ProductCount
					FROM tblProductPackage ';
	
	IF (lngProductID <> 0) THEN
		SET @SQL = CONCAT(@SQL,' WHERE ProductID <> ', lngProductID,' AND ''',strBarCode,''' IN  (BarCode1, BarCode2, BarCode3) ');
	ELSEIF (lngProductID = 0) THEN
		SET @SQL = CONCAT(@SQL,' WHERE ''',strBarCode,''' IN  (BarCode1, BarCode2, BarCode3) ');
	END IF;
	
	PREPARE stmt FROM @SQL;
	EXECUTE stmt;
	DEALLOCATE PREPARE stmt;
	
END;
GO
delimiter ;

delimiter GO
DROP PROCEDURE IF EXISTS procSetupCalendarDate
GO

delimiter GO
CREATE PROCEDURE procSetupCalendarDate(IN strYear VARCHAR(4))
BEGIN
	DECLARE lngCtr, lngCount BIGINT DEFAULT 0;
	DELETE FROM tblCalDate WHERE YEAR(CalDate) = strYear;
	REPEAT 
		insert into tblCalDate(CalDate)values(DATE_ADD(DATE_FORMAT(CONCAT(strYear,'-01-01'), '%Y-%m-%d'), INTERVAL lngCtr DAY));
		SET lngCtr = lngCtr + 1; 
		UNTIL lngCtr = 366
	END REPEAT;
END;
GO
delimiter ;

CALL procSetupCalendarDate('2012');
CALL procSetupCalendarDate('2013');




/**************************************************************
	procGenerateProductHistoryToProductMovement
	Lemuel E. Aceron
	CALL procGenerateProductHistoryToProductMovement();
	
	Mar 6, 2013 - Save all previous history of products to tblProductMovement
**************************************************************/

ALTER TABLE tblProductMovement MODIFY MatrixDescription VARCHAR(100);
ALTER TABLE tblProductMovement MODIFY Remarks VARCHAR(150);

delimiter GO
DROP PROCEDURE IF EXISTS procGenerateProductHistoryToProductMovement
GO

create procedure procGenerateProductHistoryToProductMovement()
BEGIN
	DECLARE dteStartTransactionDate DateTime;
	DECLARE dteEndTransactionDate DateTime;

	SET dteStartTransactionDate = '0001-01-01';
	SET dteEndTransactionDate = IF(NOT ISNULL(dteEndTransactionDate), dteEndTransactionDate, (SELECT DATE_ADD(MIN(transactiondate), INTERVAL -1 MINUTE) FROM tblProductMovement));
	SET dteEndTransactionDate = IF(NOT ISNULL(dteEndTransactionDate), dteEndTransactionDate, dteStartTransactionDate);

	SELECT MIN(transactiondate) AS 'tblProductMovement END Date', 
			dteEndTransactionDate AS EndTransactionDateToProcess 
	FROM tblProductMovement;

	INSERT INTO tblProductMovement (ProductID, ProductCode, ProductDescription, MatrixID, MatrixDescription, QuantityFrom, Quantity, QuantityTo, MatrixQuantity, 
									UnitCode, Remarks, TransactionDate, TransactionNo, CreatedBy, BranchIDFrom, BranchIDTo, QuantityMovementType)
	SELECT a.ProductID, b.ProductCode, COALESCE(c.Description,''), a.VariationMatrixID, IFNULL(c.Description, b.ProductCode) 'MatrixDescription', 0, CASE StockDirection
																																						WHEN 0 THEN a.Quantity
																																						WHEN 1 THEN -a.Quantity
																																					END AS Quantity, 0, 0,
									d.UnitCode, a.Remarks, a.StockDate, TransactionNo, 'SYSTEM AUTO-G', BranchID, BranchID, 1 'QuantityMovementType'
	FROM (((tblStockItems a
		INNER JOIN tblStock f ON a.StockID = f.StockID
		LEFT OUTER JOIN tblProducts b ON a.ProductID = b.ProductID)
		LEFT OUTER JOIN tblProductBaseVariationsMatrix c ON a.VariationMatrixID = c.MatrixID)
		LEFT OUTER JOIN tblUnit d ON a.ProductUnitID = d.UnitID)
		LEFT OUTER JOIN tblStockType e ON a.StockTypeID = e.StockTypeID
	WHERE DATE_FORMAT(a.StockDate, '%Y-%m-%d %H:%i') >= DATE_FORMAT(dteStartTransactionDate, '%Y-%m-%d %H:%i')
		AND DATE_FORMAT(a.StockDate, '%Y-%m-%d %H:%i') <= DATE_FORMAT(dteEndTransactionDate, '%Y-%m-%d %H:%i');

	
	INSERT INTO tblProductMovement (ProductID, ProductCode, ProductDescription, MatrixID, MatrixDescription, QuantityFrom, Quantity, QuantityTo, MatrixQuantity, 
									UnitCode, Remarks, TransactionDate, TransactionNo, CreatedBy, BranchIDFrom, BranchIDTo, QuantityMovementType)
	SELECT ProductID, ProductCode, COALESCE(Description,''), VariationsMatrixID, MatrixDescription, 0, CASE TransactionItemStatus
																											WHEN 0 THEN -Quantity
																											WHEN 1 THEN 0
																											WHEN 2 THEN 0
																											WHEN 3 THEN Quantity
																											WHEN 4 THEN -Quantity
																										END AS Quantity, 0 'QuantityTo', 0 'MatrixQuantity',  
									ProductUnitCode, CASE TransactionItemStatus
														WHEN 0 THEN CONCAT('Sold @ ',a.Price, '. Price: ',a.PurchasePrice,' /',a.ProductUnitCode, ' to ', CustomerName)
														WHEN 1 THEN 'Void'
														WHEN 2 THEN 'Trash'
														WHEN 3 THEN 'Return'
														WHEN 4 THEN 'Refund'
													END AS Remarks, TransactionDate, TransactionNo,
									CashierName, BranchID, BranchID, 1 'QuantityMovementType'
	FROM tblTransactionItems a 
	INNER JOIN tblTransactions b ON a.TransactionID = b.TransactionID
	WHERE DATE_FORMAT(TransactionDate, '%Y-%m-%d %H:%i') >= DATE_FORMAT(dteStartTransactionDate, '%Y-%m-%d %H:%i')
		AND DATE_FORMAT(TransactionDate, '%Y-%m-%d %H:%i') <= DATE_FORMAT(dteEndTransactionDate, '%Y-%m-%d %H:%i');

	/***************************************Added July 10, 2009*****************************************************/
	INSERT INTO tblProductMovement (ProductID, ProductCode, ProductDescription, MatrixID, MatrixDescription, QuantityFrom, Quantity, QuantityTo, MatrixQuantity, 
									UnitCode, Remarks, TransactionDate, TransactionNo, CreatedBy, BranchIDFrom, BranchIDTo, QuantityMovementType)
	SELECT a.ProductID, a.ProductCode, COALESCE(a.Description,''), a.VariationMatrixID, a.MatrixDescription, 0, Quantity, 0, 0 'MatrixQuantity',
									a.ProductUnitCode as UnitCode, CONCAT('Purchase Order from ',SupplierCode) AS Remarks, b.PODate AS TransactionDate, b.PONo AS TransactionNo,
									PurchaserName, BranchID, BranchID, 1 'QuantityMovementType'
	FROM tblPOItems a
	INNER JOIN tblPO b ON a.POID = b.POID
	WHERE DATE_FORMAT(b.PODate, '%Y-%m-%d %H:%i') >= DATE_FORMAT(dteStartTransactionDate, '%Y-%m-%d %H:%i')
		AND DATE_FORMAT(b.PODate, '%Y-%m-%d %H:%i') <= DATE_FORMAT(dteEndTransactionDate, '%Y-%m-%d %H:%i')
		AND b.Status = 1;

	/***************************************Added July 13, 2009*****************************************************/
	INSERT INTO tblProductMovement (ProductID, ProductCode, ProductDescription, MatrixID, MatrixDescription, QuantityFrom, Quantity, QuantityTo, MatrixQuantity, 
									UnitCode, Remarks, TransactionDate, TransactionNo, CreatedBy, BranchIDFrom, BranchIDTo, QuantityMovementType)
	SELECT a.ProductID, ProductCode, COALESCE(Description,''), a.VariationMatrixID, MatrixDescription, QuantityBefore, (QuantityNow - QuantityBefore) AS Quantity, QuantityNow, 0 'MatrixQuantity',
									a.UnitCode, CONCAT('Inventory Adjustment : ' , Remarks, ' from ', QuantityBefore, ' to ', QuantityNow ) Remarks, InvAdjustmentDate AS TransactionDate,
									CONCAT('InvAdjID:' , a.InvAdjustmentID) AS TransactionNo, b.Name, 1, 1, 1 'QuantityMovementType'
	FROM tblInvAdjustment a
	INNER JOIN sysAccessUserDetails b ON a.UID = b.UID
	WHERE DATE_FORMAT(a.InvAdjustmentDate, '%Y-%m-%d %H:%i') >= DATE_FORMAT(dteStartTransactionDate, '%Y-%m-%d %H:%i')
		AND DATE_FORMAT(a.InvAdjustmentDate, '%Y-%m-%d %H:%i') <= DATE_FORMAT(dteEndTransactionDate, '%Y-%m-%d %H:%i');

	/***************************************Added July 20, 2009*****************************************************/
	INSERT INTO tblProductMovement (ProductID, ProductCode, ProductDescription, MatrixID, MatrixDescription, QuantityFrom, Quantity, QuantityTo, MatrixQuantity, 
									UnitCode, Remarks, TransactionDate, TransactionNo, CreatedBy, BranchIDFrom, BranchIDTo, QuantityMovementType)
	SELECT a.ProductID, a.ProductCode, COALESCE(a.Description,''), a.VariationMatrixID, a.MatrixDescription, 0, Quantity, 0, 0 'MatrixQuantity',
									a.ProductUnitCode as UnitCode, CONCAT('Transfer In from ',SupplierCode) AS Remarks, b.TransferInDate AS TransactionDate, b.TransferInNo AS TransactionNo,
									TransferrerName, BranchID, BranchID, 1 'QuantityMovementType'
	FROM tblTransferInItems a
	INNER JOIN tblTransferIn b ON a.TransferInID = b.TransferInID
	WHERE DATE_FORMAT(b.TransferInDate, '%Y-%m-%d %H:%i') >= DATE_FORMAT(dteStartTransactionDate, '%Y-%m-%d %H:%i')
		AND DATE_FORMAT(b.TransferInDate, '%Y-%m-%d %H:%i') <= DATE_FORMAT(dteEndTransactionDate, '%Y-%m-%d %H:%i')
		AND b.Status = 1;

	/***************************************Added July 20, 2009*****************************************************/
	INSERT INTO tblProductMovement (ProductID, ProductCode, ProductDescription, MatrixID, MatrixDescription, QuantityFrom, Quantity, QuantityTo, MatrixQuantity, 
									UnitCode, Remarks, TransactionDate, TransactionNo, CreatedBy, BranchIDFrom, BranchIDTo, QuantityMovementType)
	SELECT a.ProductID, a.ProductCode, COALESCE(a.Description,''), a.VariationMatrixID, a.MatrixDescription, 0, Quantity, 0, 0 'MatrixQuantity',
									a.ProductUnitCode as UnitCode, CONCAT('Transfer out to ',SupplierCode) AS Remarks, b.TransferOutDate AS TransactionDate, b.TransferOutNo AS TransactionNo,
									TransferrerName, BranchID, BranchID, 1 'QuantityMovementType'
	FROM tblTransferOutItems a
	INNER JOIN tblTransferOut b ON a.TransferOutID = b.TransferOutID
	WHERE DATE_FORMAT(b.TransferOutDate, '%Y-%m-%d %H:%i') >= DATE_FORMAT(dteStartTransactionDate, '%Y-%m-%d %H:%i')
		AND DATE_FORMAT(b.TransferOutDate, '%Y-%m-%d %H:%i') <= DATE_FORMAT(dteEndTransactionDate, '%Y-%m-%d %H:%i')
		AND b.Status = 1;

END;
GO
delimiter ;



/**************************************************************

	procProductZeroOutActualQuantityBySupplier
	Lemuel E. Aceron
	May 4, 2013 - Add updating of products by supplier

	CALL procProductZeroOutActualQuantityBySupplier();

**************************************************************/
delimiter GO
DROP PROCEDURE IF EXISTS procProductZeroOutActualQuantityBySupplier
GO

create procedure procProductZeroOutActualQuantityBySupplier(
						IN intBranchID INT(4),
						IN lngSupplierID bigint)
BEGIN
	
	UPDATE tblProductInventory SET 
		ActualQuantity = 0 
	WHERE BranchID = intBranchID 
		AND (ProductID IN (SELECT ProductID FROM tblProducts WHERE SupplierID = lngSupplierID)
		OR ProductID IN (SELECT DISTINCT(ProductID) FROM tblProductBaseVariationsMatrix WHERE SupplierID = lngSupplierID));
END;
GO
delimiter ;

/**************************************************************

	procLockUnlockProduct
	Lemuel E. Aceron
	March 14, 2009

	CALL LockUnlockProductForSellingBySupplier();

	May 4, 2013 - Add updating of products by supplier
**************************************************************/
delimiter GO
DROP PROCEDURE IF EXISTS LockUnlockProductForSellingBySupplier
GO

create procedure LockUnlockProductForSellingBySupplier(
						IN intBranchID INT(4),
						IN lngSupplierID bigint,
						IN bolisLock TINYINT(1))
BEGIN

	UPDATE tblProductInventory SET 
		isLock = bolisLock
	WHERE BranchID = intBranchID 
		AND (ProductID IN (SELECT ProductID FROM tblProducts WHERE SupplierID = lngSupplierID)
		OR ProductID IN (SELECT DISTINCT(ProductID) FROM tblProductBaseVariationsMatrix WHERE SupplierID = lngSupplierID));
	
	UPDATE tblContacts SET
		isLock = bolisLock
	WHERE ContactID = lngSupplierID;
END;
GO
delimiter ;


/**************************************************************

	procProductInventorySelect
	Lemuel E. Aceron
	March 22, 2013
	
	Desc: This will get the all product package list

	CALL procProductInventorySelect(0, 0,'D10 W 500ML/EM','D10 W 500ML/EM',0,2,0,0,null,null);
	
**************************************************************/
delimiter GO
DROP PROCEDURE IF EXISTS procProductInventorySelect
GO

create procedure procProductInventorySelect(
			 IN BranchID bigint,
			 IN ProductID bigint,
			 IN BarCode varchar(30),
			 IN ProductCode varchar(30),
			 IN ProductGroupID bigint,
			 IN ProductSubGroupID bigint,
			 IN SupplierID bigint,
			 IN ShowActiveAndInactive INT(1),
			 IN isQuantityGreaterThanZERO TINYINT(1),
			 IN lngLimit int,
			 IN SortField varchar(60),
			 IN SortOrder varchar(4))
BEGIN
	SET BarCode = REPLACE(BarCode, '''', '''''');
	SET ProductCode = REPLACE(ProductCode, '''', '''''');

	SET @SQL = CONCAT('	SELECT 
							 prd.ProductID
							,pkg.PackageID
							,IFNULL(pkg.BarCode1,pkg.BarCode4) BarCode
							,pkg.BarCode1
							,pkg.BarCode2
							,pkg.BarCode3
							,pkg.BarCode4
							,prd.ProductCode
							,prd.ProductDesc
							
							,prdsg.ProductGroupID
							,prdg.ProductGroupCode
							,prdg.ProductGroupName
							,prdg.OrderSlipPrinter

							,prd.ProductSubGroupID
							,prdsg.ProductSubGroupCode
							,prdsg.ProductSubGroupName

							,prd.BaseUnitID
							,unt.UnitCode BaseUnitCode
							,unt.UnitName BaseUnitName
							,prd.BaseUnitID UnitID
							,unt.UnitCode
							,unt.UnitName

							,prd.DateCreated
							,prd.Active
							,prd.Deleted

							,prd.SupplierID
							,supp.ContactCode SupplierCode
							,supp.ContactName SupplierName

							,prd.IsItemSold
							,pkg.Price
							,pkg.WSPrice
							,pkg.PurchasePrice
							,prd.PercentageCommision
							,prd.IncludeInSubtotalDiscount
							,pkg.VAT
							,pkg.EVAT
							,pkg.LocalTax
							,prd.RewardPoints

							,IFNULL(inv.Quantity,0) Quantity
							,IF(ISNULL(inv.Quantity),0, fnProductQuantityConvert(prd.ProductID, inv.Quantity, prd.BaseUnitID))  ConvertedQuantity
							,IFNULL(inv.QuantityIN,0) QuantityIN
							,IFNULL(inv.QuantityOUT,0) QuantityOUT
							,IFNULL(inv.ActualQuantity,0) ActualQuantity

							,prd.WillPrintProductComposition

							,IFNULL(mtrx.MinThreshold, prd.MinThreshold) MinThreshold
							,IFNULL(mtrx.MaxThreshold, prd.MaxThreshold) MaxThreshold
							,prd.RID

							,IFNULL(mtrx.MaxThreshold, prd.MaxThreshold) - IFNULL(inv.Quantity,0) ReorderQty
							,prd.RIDMinThreshold
							,prd.RIDMaxThreshold
							,prd.RIDMaxThreshold -  IFNULL(inv.Quantity,0) AS RIDReorderQty

							,prd.ChartOfAccountIDPurchase
							,prd.ChartOfAccountIDSold
							,prd.ChartOfAccountIDInventory
							,prd.ChartOfAccountIDTaxPurchase
							,prd.ChartOfAccountIDTaxSold

							,IFNULL(mtrx.MatrixID,0) MatrixID
							,CONCAT(prd.ProductDesc, '':'' , mtrx.Description) AS VariationDesc
							,mtrx.Description AS MatrixDescription
						FROM tblProducts prd 
						INNER JOIN tblProductSubGroup prdsg ON prdsg.ProductSubGroupID = prd.ProductSubGroupID
						INNER JOIN tblProductGroup prdg ON prdg.ProductGroupID = prdsg.ProductGroupID
						INNER JOIN tblUnit unt ON prd.BaseUnitID = unt.UnitID
						INNER JOIN tblContacts supp ON supp.ContactID = prd.SupplierID
						INNER JOIN tblProductPackage pkg ON prd.productID = pkg.ProductID 
														AND prd.BaseUnitID = pkg.UnitID
														AND pkg.Quantity = 1 
						LEFT OUTER JOIN tblProductBaseVariationsMatrix mtrx ON mtrx.ProductID = prd.ProductID AND pkg.MatrixID = mtrx.MatrixID
						LEFT OUTER JOIN (
							SELECT ProductID, MatrixID, SUM(Quantity) Quantity, SUM(QuantityIn) QuantityIn, SUM(QuantityOut) QuantityOut, SUM(ActualQuantity) ActualQuantity FROM tblProductInventory WHERE ',IF(BranchID=0,'1=1',Concat('BranchID=',BranchID)),' GROUP BY ProductID, MatrixID
						) inv ON inv.ProductID = prd.ProductID AND inv.MatrixID = IFNULL(mtrx.MatrixID,0)
						WHERE prd.deleted = 0 AND IFNULL(mtrx.deleted, 0) = 0 ');
	
	IF ProductID <> 0 THEN
		SET @SQL = CONCAT(@SQL, 'AND prd.ProductID = ',ProductID,' ');
	END IF;

	IF IFNULL(ProductCode,'') <> '' AND IFNULL(BarCode,'') <> '' THEN
		SET @SQL = CONCAT(@SQL, 'AND (pkg.BarCode1 LIKE ''%',BarCode,'%'' ');
		SET @SQL = CONCAT(@SQL, '  OR pkg.BarCode2 LIKE ''%',BarCode,'%'' ');
		SET @SQL = CONCAT(@SQL, '  OR pkg.BarCode3 LIKE ''%',BarCode,'%'' ');
		SET @SQL = CONCAT(@SQL, '  OR pkg.BarCode4 LIKE ''%',BarCode,'%'' ');
		SET @SQL = CONCAT(@SQL, '  OR prd.ProductCode LIKE ''%',BarCode,'%'') ');
	ELSEIF IFNULL(ProductCode,'') = '' AND IFNULL(BarCode,'') <> '' THEN
		SET @SQL = CONCAT(@SQL, 'AND (pkg.BarCode1 LIKE ''%',BarCode,'%'' ');
		SET @SQL = CONCAT(@SQL, '  OR pkg.BarCode2 LIKE ''%',BarCode,'%'' ');
		SET @SQL = CONCAT(@SQL, '  OR pkg.BarCode3 LIKE ''%',BarCode,'%'' ');
		SET @SQL = CONCAT(@SQL, '  OR pkg.BarCode4 LIKE ''%',BarCode,'%'') ');
	ELSEIF IFNULL(ProductCode,'') <> '' AND IFNULL(BarCode,'') = '' THEN
		SET @SQL = CONCAT(@SQL, 'AND prd.ProductCode LIKE ''%',ProductCode,'%'' ');
	END IF;

	IF ProductGroupID <> 0 THEN
		SET @SQL = CONCAT(@SQL, 'AND prdg.ProductGroupID = ',ProductGroupID,' ');
	END IF;

	IF ProductSubGroupID <> 0 THEN
		SET @SQL = CONCAT(@SQL, 'AND prdsg.ProductSubGroupID = ',ProductSubGroupID,' ');
	END IF;

	IF SupplierID <> 0 THEN
		SET @SQL = CONCAT(@SQL, 'AND prd.SupplierID = ',SupplierID,' ');
	END IF;

	IF ShowActiveAndInactive = 0 OR ShowActiveAndInactive = 1  THEN
		SET @SQL = CONCAT(@SQL, 'AND prd.Active = ',ShowActiveAndInactive,' ');
	END IF;

	IF isQuantityGreaterThanZERO <> 0 THEN
		SET @SQL = CONCAT(@SQL, 'AND IFNULL(inv.Quantity,0) > 0 ');
	END IF;

	SET @SQL = CONCAT(@SQL, 'ORDER BY ',IF(IFNULL(SortField,'')='','MatrixDescription',SortField),' ',IFNULL(SortOrder,'ASC'),' ');

	SET @SQL = CONCAT(@SQL, IF(lngLimit=0,'',CONCAT('LIMIT ',lngLimit,' ')));

	PREPARE cmd FROM @SQL;
	EXECUTE cmd;
	DEALLOCATE PREPARE cmd;

END;
GO
delimiter ;

/**************************************************************

	procProductSelect
	Lemuel E. Aceron
	March 22, 2013
	
	Desc: This will get the main product list

	CALL procProductSelect(0, 'TEST3','TEST3',0,2,0,0,null,null);
	
**************************************************************/
delimiter GO
DROP PROCEDURE IF EXISTS procProductSelect
GO

create procedure procProductSelect(
			 IN BranchID int,
			 IN BarCode varchar(30),
			 IN ProductCode varchar(30),
			 IN SupplierID bigint,
			 IN ShowActiveAndInactive INT(1),
			 IN isQuantityGreaterThanZERO TINYINT(1),
			 IN lngLimit int,
			 IN SortField varchar(60),
			 IN SortOrder varchar(4))
BEGIN
	SET BarCode = REPLACE(BarCode, '''', '''''');
	SET ProductCode = REPLACE(ProductCode, '''', '''''');

	SET @SQL = CONCAT('	SELECT 
							 prd.ProductID
							,IFNULL(pkg.BarCode1,pkg.BarCode4) BarCode
							,pkg.BarCode1
							,pkg.BarCode2
							,pkg.BarCode3
							,pkg.BarCode4
							,prd.ProductCode
							,prd.ProductDesc
							
							,prd.ProductSubGroupID
							,prdsg.ProductSubGroupCode
							,prdsg.ProductSubGroupName
							,prdsg.ProductGroupID
							,prdg.ProductGroupCode
							,prdg.ProductGroupName
							,prd.BaseUnitID
							,unt.UnitCode BaseUnitCode
							,unt.UnitName BaseUnitName
							,prd.DateCreated
							,prd.Active

							,prd.SupplierID
							,supp.ContactCode SupplierCode
							,supp.ContactName SupplierName

							,pkg.Price
							,pkg.WSPrice
							,pkg.PurchasePrice
							,prd.PercentageCommision
							,prd.IncludeInSubtotalDiscount
							,pkg.VAT
							,pkg.EVAT
							,pkg.LocalTax
							,prd.RewardPoints

							,IFNULL(inv.Quantity,0) Quantity
							,IFNULL(inv.ActualQuantity,0) ActualQuantity
							,IF(ISNULL(inv.Quantity),0, fnProductQuantityConvert(prd.ProductID, inv.Quantity, prd.BaseUnitID))  ConvertedQuantity

						FROM tblProducts prd
						INNER JOIN tblProductSubGroup prdsg ON prdsg.ProductSubGroupID = prd.ProductSubGroupID
						INNER JOIN tblProductGroup prdg ON prdg.ProductGroupID = prdsg.ProductGroupID
						INNER JOIN tblUnit unt ON prd.BaseUnitID = unt.UnitID
						INNER JOIN tblProductPackage pkg ON prd.productID = pkg.ProductID 
														AND prd.BaseUnitID = pkg.UnitID
														AND pkg.Quantity = 1
						INNER JOIN tblContacts supp ON supp.ContactID = prd.SupplierID
						LEFT OUTER JOIN (
							SELECT ProductID, SUM(Quantity) Quantity, SUM(ActualQuantity) ActualQuantity FROM tblProductInventory WHERE ',IF(BranchID=0,'1=1',Concat('BranchID=',BranchID)),' GROUP BY ProductID
						) inv ON inv.ProductID = prd.ProductID 
						WHERE prd.deleted = 0 ');

	IF IFNULL(ProductCode,'') <> '' AND IFNULL(BarCode,'') <> '' THEN
		SET @SQL = CONCAT(@SQL, 'AND (pkg.BarCode1 LIKE ''%',BarCode,'%'' ');
		SET @SQL = CONCAT(@SQL, '  OR pkg.BarCode2 LIKE ''%',BarCode,'%'' ');
		SET @SQL = CONCAT(@SQL, '  OR pkg.BarCode3 LIKE ''%',BarCode,'%'' ');
		SET @SQL = CONCAT(@SQL, '  OR pkg.BarCode4 LIKE ''%',BarCode,'%'' ');
		SET @SQL = CONCAT(@SQL, '  OR prd.ProductDesc LIKE ''%',ProductCode,'%'' ');
		SET @SQL = CONCAT(@SQL, '  OR prd.ProductCode LIKE ''%',ProductCode,'%'') ');
	ELSEIF IFNULL(ProductCode,'') = '' AND IFNULL(BarCode,'') <> '' THEN
		SET @SQL = CONCAT(@SQL, 'AND (pkg.BarCode1 LIKE ''%',BarCode,'%'' ');
		SET @SQL = CONCAT(@SQL, '  OR pkg.BarCode2 LIKE ''%',BarCode,'%'' ');
		SET @SQL = CONCAT(@SQL, '  OR pkg.BarCode3 LIKE ''%',BarCode,'%'' ');
		SET @SQL = CONCAT(@SQL, '  OR pkg.BarCode4 LIKE ''%',BarCode,'%'') ');
	ELSEIF IFNULL(ProductCode,'') <> '' AND IFNULL(BarCode,'') = '' THEN
		SET @SQL = CONCAT(@SQL, 'AND prd.ProductCode LIKE ''%',ProductCode,'%'' ');
	END IF;

	IF SupplierID <> 0 THEN
		SET @SQL = CONCAT(@SQL, 'AND prd.SupplierID = ',SupplierID,' ');
	END IF;

	IF ShowActiveAndInactive = 0 OR ShowActiveAndInactive = 1  THEN
		SET @SQL = CONCAT(@SQL, 'AND prd.Active = ',ShowActiveAndInactive,' ');
	END IF;

	IF isQuantityGreaterThanZERO <> 0 THEN
		SET @SQL = CONCAT(@SQL, 'AND IFNULL(inv.Quantity,0) > 0 ');
	END IF;

	SET @SQL = CONCAT(@SQL, 'ORDER BY ',IF(IFNULL(SortField,'')='','prd.ProductCode',SortField),' ',IFNULL(SortOrder,'ASC'),' ');

	SET @SQL = CONCAT(@SQL, IF(lngLimit=0,'',CONCAT('LIMIT ',lngLimit,' ')));
	
	PREPARE cmd FROM @SQL;
	EXECUTE cmd;
	DEALLOCATE PREPARE cmd;

END;
GO
delimiter ;

/**************************************************************

	procProductCodeSelect
	Lemuel E. Aceron
	March 22, 2013
	
	Desc: This will get the main product list

	CALL procProductCodeSelect(0,'D10 W 500ML','D10 W 500ML',0,2,0,0,null,null);
	
**************************************************************/
delimiter GO
DROP PROCEDURE IF EXISTS procProductCodeSelect
GO

create procedure procProductCodeSelect(
			 IN BranchID int,
			 IN BarCode varchar(30),
			 IN ProductCode varchar(30),
			 IN SupplierID bigint,
			 IN ShowActiveAndInactive INT(1),
			 IN isQuantityGreaterThanZERO TINYINT(1),
			 IN lngLimit int,
			 IN SortField varchar(60),
			 IN SortOrder varchar(4))
BEGIN
	SET BarCode = REPLACE(BarCode, '''', '''''');
	SET ProductCode = REPLACE(ProductCode, '''', '''''');

	SET @SQL = CONCAT('	SELECT 
							 prd.ProductID
							,prd.ProductCode
						FROM tblProducts prd
						INNER JOIN tblProductSubGroup prdsg ON prdsg.ProductSubGroupID = prd.ProductSubGroupID
						INNER JOIN tblProductGroup prdg ON prdg.ProductGroupID = prdsg.ProductGroupID
						INNER JOIN tblUnit unt ON prd.BaseUnitID = unt.UnitID
						INNER JOIN tblProductPackage pkg ON prd.productID = pkg.ProductID 
														AND prd.BaseUnitID = pkg.UnitID
														AND pkg.Quantity = 1
						INNER JOIN tblContacts supp ON supp.ContactID = prd.SupplierID
						LEFT OUTER JOIN (
							SELECT ProductID, SUM(Quantity) Quantity, SUM(ActualQuantity) ActualQuantity FROM tblProductInventory WHERE ',IF(BranchID=0,'1=1',Concat('BranchID=',BranchID)),' GROUP BY ProductID
						) inv ON inv.ProductID = prd.ProductID 
						WHERE prd.deleted = 0 ');

	IF IFNULL(ProductCode,'') <> '' AND IFNULL(BarCode,'') <> '' THEN
		SET @SQL = CONCAT(@SQL, 'AND (pkg.BarCode1 LIKE ''%',BarCode,'%'' ');
		SET @SQL = CONCAT(@SQL, '  OR pkg.BarCode2 LIKE ''%',BarCode,'%'' ');
		SET @SQL = CONCAT(@SQL, '  OR pkg.BarCode3 LIKE ''%',BarCode,'%'' ');
		SET @SQL = CONCAT(@SQL, '  OR pkg.BarCode4 LIKE ''%',BarCode,'%'' ');
		SET @SQL = CONCAT(@SQL, '  OR prd.ProductCode LIKE ''%',BarCode,'%'') ');
	ELSEIF IFNULL(ProductCode,'') = '' AND IFNULL(BarCode,'') <> '' THEN
		SET @SQL = CONCAT(@SQL, 'AND (pkg.BarCode1 LIKE ''%',BarCode,'%'' ');
		SET @SQL = CONCAT(@SQL, '  OR pkg.BarCode2 LIKE ''%',BarCode,'%'' ');
		SET @SQL = CONCAT(@SQL, '  OR pkg.BarCode3 LIKE ''%',BarCode,'%'' ');
		SET @SQL = CONCAT(@SQL, '  OR pkg.BarCode4 LIKE ''%',BarCode,'%'') ');
	ELSEIF IFNULL(ProductCode,'') <> '' AND IFNULL(BarCode,'') = '' THEN
		SET @SQL = CONCAT(@SQL, 'AND prd.ProductCode LIKE ''%',ProductCode,'%'' ');
	END IF;

	IF SupplierID <> 0 THEN
		SET @SQL = CONCAT(@SQL, 'AND prd.SupplierID = ',SupplierID,' ');
	END IF;

	IF ShowActiveAndInactive = 0 OR ShowActiveAndInactive = 1  THEN
		SET @SQL = CONCAT(@SQL, 'AND prd.Active = ',ShowActiveAndInactive,' ');
	END IF;

	IF isQuantityGreaterThanZERO <> 0 THEN
		SET @SQL = CONCAT(@SQL, 'AND IFNULL(inv.Quantity,0) > 0 ');
	END IF;

	SET @SQL = CONCAT(@SQL, 'GROUP BY prd.ProductID, prd.ProductCode ');
	SET @SQL = CONCAT(@SQL, 'ORDER BY ',IF(IFNULL(SortField,'')='','prd.ProductCode',SortField),' ',IFNULL(SortOrder,'ASC'),' ');

	SET @SQL = CONCAT(@SQL, IF(lngLimit=0,'',CONCAT('LIMIT ',lngLimit,' ')));
	
	PREPARE cmd FROM @SQL;
	EXECUTE cmd;
	DEALLOCATE PREPARE cmd;

END;
GO
delimiter ;

/**************************************************************

	procProductMainDetails
	Lemuel E. Aceron
	March 22, 2013
	
	Desc: This will get the main product list

	CALL procProductMainDetails(0, 3057, '');
	
**************************************************************/
delimiter GO
DROP PROCEDURE IF EXISTS procProductMainDetails
GO

create procedure procProductMainDetails(
			 IN BranchID int,
			 IN ProductID bigint,
			 IN MatrixID bigint,
			 IN BarCode varchar(60))
BEGIN
	SET BarCode = REPLACE(BarCode, '''', '''''');
	
	SET @SQL = CONCAT('	SELECT 
							 prd.ProductID
							,pkg.PackageID
							,IFNULL(pkg.BarCode1,pkg.BarCode4) BarCode
							,pkg.BarCode1
							,pkg.BarCode2
							,pkg.BarCode3
							,pkg.BarCode4
							,prd.ProductCode
							,prd.ProductDesc
							
							,prdsg.ProductGroupID
							,prdg.ProductGroupCode
							,prdg.ProductGroupName
							,prdg.OrderSlipPrinter

							,prd.ProductSubGroupID
							,prdsg.ProductSubGroupCode
							,prdsg.ProductSubGroupName

							,prd.BaseUnitID
							,unt.UnitCode BaseUnitCode
							,unt.UnitName BaseUnitName
							,prd.BaseUnitID UnitID
							,unt.UnitCode
							,unt.UnitName

							,prd.DateCreated
							,prd.Active
							,prd.Deleted

							,prd.SupplierID
							,supp.ContactCode SupplierCode
							,supp.ContactName SupplierName

							,prd.IsItemSold
							,pkg.Price
							,pkg.WSPrice
							,pkg.PurchasePrice
							,prd.PercentageCommision
							,prd.IncludeInSubtotalDiscount
							,pkg.VAT
							,pkg.EVAT
							,pkg.LocalTax
							,prd.RewardPoints

							,IFNULL(inv.Quantity,0) Quantity
							,IF(ISNULL(inv.Quantity),0, fnProductQuantityConvert(prd.ProductID, inv.Quantity, prd.BaseUnitID))  ConvertedQuantity
							,IFNULL(inv.QuantityIN,0) QuantityIN
							,IFNULL(inv.QuantityOUT,0) QuantityOUT
							,IFNULL(inv.ActualQuantity,0) ActualQuantity

							,prd.WillPrintProductComposition

							,prd.MinThreshold
							,prd.MaxThreshold
							,prd.RID

							,prd.MaxThreshold - IFNULL(inv.Quantity,0) ReorderQty
							,prd.RIDMinThreshold
							,prd.RIDMaxThreshold
							,prd.RIDMaxThreshold -  IFNULL(inv.Quantity,0) AS RIDReorderQty

							,prd.ChartOfAccountIDPurchase
							,prd.ChartOfAccountIDSold
							,prd.ChartOfAccountIDInventory
							,prd.ChartOfAccountIDTaxPurchase
							,prd.ChartOfAccountIDTaxSold

							,IFNULL(mtrx.MatrixID,0) MatrixID
							,IFNULL(CONCAT(prd.ProductDesc, '':'' , mtrx.Description),'''') AS VariationDesc
							,IFNULL(mtrx.Description,'''') MatrixDescription
						FROM tblProducts prd
						INNER JOIN tblProductSubGroup prdsg ON prdsg.ProductSubGroupID = prd.ProductSubGroupID
						INNER JOIN tblProductGroup prdg ON prdg.ProductGroupID = prdsg.ProductGroupID
						INNER JOIN tblUnit unt ON prd.BaseUnitID = unt.UnitID
						INNER JOIN tblProductPackage pkg ON prd.productID = pkg.ProductID 
														AND prd.BaseUnitID = pkg.UnitID
														AND pkg.Quantity = 1
						INNER JOIN tblContacts supp ON supp.ContactID = prd.SupplierID
						LEFT OUTER JOIN tblProductBaseVariationsMatrix mtrx ON prd.productID = mtrx.ProductID AND pkg.MatrixID =  mtrx.MatrixID
						LEFT OUTER JOIN (
							SELECT ProductID, SUM(Quantity) Quantity, SUM(QuantityIn) QuantityIn, SUM(QuantityOut) QuantityOut, SUM(ActualQuantity) ActualQuantity FROM tblProductInventory WHERE ',IF(BranchID=0,'1=1',Concat('BranchID=',BranchID)),' GROUP BY ProductID
						) inv ON inv.ProductID = prd.ProductID 
						WHERE prd.deleted = 0 ');

	IF ProductID <> 0 THEN
		SET @SQL = CONCAT(@SQL, 'AND prd.ProductID = ',ProductID,' ');
	ELSEIF IFNULL(BarCode,'') <> '' THEN
		SET @SQL = CONCAT(@SQL, 'AND (pkg.BarCode1 = ''',BarCode,''' ');
		SET @SQL = CONCAT(@SQL, '  OR pkg.BarCode2 = ''',BarCode,''' ');
		SET @SQL = CONCAT(@SQL, '  OR pkg.BarCode3 = ''',BarCode,''' ');
		SET @SQL = CONCAT(@SQL, '  OR pkg.BarCode4 = ''',BarCode,''') ');
	END IF;
	SET @SQL = CONCAT(@SQL, 'AND pkg.MatrixID = ',MatrixID,' ');

	SET @SQL = CONCAT(@SQL, 'LIMIT 1 ');

	PREPARE cmd FROM @SQL;
	EXECUTE cmd;
	DEALLOCATE PREPARE cmd;

END;
GO
delimiter ;


/**************************************************************

	procProductVaritionMatrixSelect
	Lemuel E. Aceron
	March 22, 2013
	
	Desc: This will get the main product list

	CALL procProductVaritionMatrixSelect(0, 4355, '', '', '', 0, 2, 0, 10, null, null);
	
**************************************************************/

delimiter GO
DROP PROCEDURE IF EXISTS procProductVaritionMatrixSelect
GO

create procedure procProductVaritionMatrixSelect(
			 IN BranchID bigint,
			 IN ProductID bigint,
			 IN BarCode varchar(30),
			 IN ProductCode varchar(30),
			 IN MatrixDescription varchar(60),
			 IN SupplierID bigint,
			 IN ShowActiveAndInactive INT(1),
			 IN isQuantityGreaterThanZERO TINYINT(1),
			 IN lngLimit int,
			 IN SortField varchar(60),
			 IN SortOrder varchar(4))
BEGIN
	SET BarCode = REPLACE(BarCode, '''', '''''');
	SET ProductCode = REPLACE(ProductCode, '''', '''''');
	SET MatrixDescription = REPLACE(MatrixDescription, '''', '''''');

	SET @SQL = CONCAT('	SELECT 
							 prd.ProductID
							,pkg.PackageID
							,IFNULL(pkg.BarCode1,pkg.BarCode4) BarCode
							,pkg.BarCode1
							,pkg.BarCode2
							,pkg.BarCode3
							,pkg.BarCode4
							,prd.ProductCode
							,prd.ProductDesc
							
							,prdsg.ProductGroupID
							,prdg.ProductGroupCode
							,prdg.ProductGroupName
							,prdg.OrderSlipPrinter

							,prd.ProductSubGroupID
							,prdsg.ProductSubGroupCode
							,prdsg.ProductSubGroupName

							,prd.BaseUnitID
							,unt.UnitCode BaseUnitCode
							,unt.UnitName BaseUnitName
							,prd.BaseUnitID UnitID
							,unt.UnitCode
							,unt.UnitName

							,prd.DateCreated
							,prd.Active
							,prd.Deleted

							,prd.SupplierID
							,supp.ContactCode SupplierCode
							,supp.ContactName SupplierName

							,prd.IsItemSold
							,pkg.Price
							,pkg.WSPrice
							,pkg.PurchasePrice
							,prd.PercentageCommision
							,prd.IncludeInSubtotalDiscount
							,pkg.VAT
							,pkg.EVAT
							,pkg.LocalTax
							,prd.RewardPoints

							,IFNULL(inv.Quantity,0) Quantity
							,IF(ISNULL(inv.Quantity),0, fnProductQuantityConvert(prd.ProductID, inv.Quantity, prd.BaseUnitID))  ConvertedQuantity
							,IFNULL(inv.QuantityIN,0) QuantityIN
							,IFNULL(inv.QuantityOUT,0) QuantityOUT
							,IFNULL(inv.ActualQuantity,0) ActualQuantity

							,prd.WillPrintProductComposition

							,prd.MinThreshold
							,prd.MaxThreshold
							,prd.RID

							,prd.MaxThreshold - IFNULL(inv.Quantity,0) ReorderQty
							,prd.RIDMinThreshold
							,prd.RIDMaxThreshold
							,prd.RIDMaxThreshold -  IFNULL(inv.Quantity,0) AS RIDReorderQty

							,prd.ChartOfAccountIDPurchase
							,prd.ChartOfAccountIDSold
							,prd.ChartOfAccountIDInventory
							,prd.ChartOfAccountIDTaxPurchase
							,prd.ChartOfAccountIDTaxSold

							,mtrx.MatrixID
							,CONCAT(prd.ProductDesc, '':'' , mtrx.Description) AS VariationDesc
							,mtrx.Description AS MatrixDescription
						FROM tblProductBaseVariationsMatrix mtrx 
						INNER JOIN tblProducts prd ON mtrx.ProductID = prd.ProductID
						INNER JOIN tblProductSubGroup prdsg ON prdsg.ProductSubGroupID = prd.ProductSubGroupID
						INNER JOIN tblProductGroup prdg ON prdg.ProductGroupID = prdsg.ProductGroupID
						INNER JOIN tblUnit unt ON prd.BaseUnitID = unt.UnitID
						INNER JOIN tblProductPackage pkg ON prd.productID = pkg.ProductID 
														AND prd.BaseUnitID = pkg.UnitID
														AND pkg.Quantity = 1 AND pkg.MatrixID = mtrx.MatrixID
						INNER JOIN tblContacts supp ON supp.ContactID = prd.SupplierID
						LEFT OUTER JOIN (
							SELECT ProductID, MatrixID, SUM(Quantity) Quantity, SUM(QuantityIn) QuantityIn, SUM(QuantityOut) QuantityOut, SUM(ActualQuantity) ActualQuantity FROM tblProductInventory WHERE ',IF(BranchID=0,'1=1',Concat('BranchID=',BranchID)),' GROUP BY ProductID, MatrixID
						) inv ON inv.ProductID = prd.ProductID AND inv.MatrixID = mtrx.MatrixID
						WHERE mtrx.deleted = 0 ');
	
	IF ProductID <> 0 THEN
		SET @SQL = CONCAT(@SQL, 'AND prd.ProductID = ',ProductID,' ');
	END IF;

	IF IFNULL(ProductCode,'') <> '' AND IFNULL(BarCode,'') <> '' THEN
		SET @SQL = CONCAT(@SQL, 'AND (pkg.BarCode2 LIKE ''%',BarCode,'%'' ');
		SET @SQL = CONCAT(@SQL, '  OR pkg.BarCode3 LIKE ''%',BarCode,'%'' ');
		SET @SQL = CONCAT(@SQL, '  OR pkg.BarCode4 LIKE ''%',BarCode,'%'' ');
		SET @SQL = CONCAT(@SQL, '  OR prd.ProductCode LIKE ''%',BarCode,'%'') ');
	ELSEIF IFNULL(ProductCode,'') = '' AND IFNULL(BarCode,'') <> '' THEN
		SET @SQL = CONCAT(@SQL, 'AND (pkg.BarCode2 LIKE ''%',BarCode,'%'' ');
		SET @SQL = CONCAT(@SQL, '  OR pkg.BarCode3 LIKE ''%',BarCode,'%'' ');
		SET @SQL = CONCAT(@SQL, '  OR pkg.BarCode4 LIKE ''%',BarCode,'%'') ');
	ELSEIF IFNULL(ProductCode,'') <> '' AND IFNULL(BarCode,'') = '' THEN
		SET @SQL = CONCAT(@SQL, 'AND prd.ProductCode LIKE ''%',ProductCode,'%'' ');
	END IF;

	IF IFNULL(MatrixDescription,'') <> '' THEN
		SET @SQL = CONCAT(@SQL, 'AND IFNULL(mtrx.Description,'''') LIKE ''%',MatrixDescription,'%'' ');
	END IF;

	IF SupplierID <> 0 THEN
		SET @SQL = CONCAT(@SQL, 'AND prd.SupplierID = ',SupplierID,' ');
	END IF;

	IF ShowActiveAndInactive = 0 OR ShowActiveAndInactive = 1  THEN
		SET @SQL = CONCAT(@SQL, 'AND prd.Active = ',ShowActiveAndInactive,' ');
	END IF;

	IF isQuantityGreaterThanZERO <> 0 THEN
		SET @SQL = CONCAT(@SQL, 'AND IFNULL(inv.Quantity,0) > 0 ');
	END IF;

	SET @SQL = CONCAT(@SQL, 'ORDER BY ',IF(IFNULL(SortField,'')='','MatrixDescription',SortField),' ',IFNULL(SortOrder,'ASC'),' ');

	SET @SQL = CONCAT(@SQL, IF(lngLimit=0,'',CONCAT('LIMIT ',lngLimit,' ')));

	PREPARE cmd FROM @SQL;
	EXECUTE cmd;
	DEALLOCATE PREPARE cmd;

END;
GO
delimiter ;


/**************************************************************

	procProductVaritionMatrixDetails
	Lemuel E. Aceron
	March 22, 2013
	
	Desc: This will get the product and matrix packages

	CALL procProductPackageSelect(0, 0, 22269, '', '');
	
**************************************************************/
delimiter GO
DROP PROCEDURE IF EXISTS procProductPackageSelect
GO

create procedure procProductPackageSelect(
			 IN ProductID bigint,
			 IN BarCode varchar(30),
			 IN ProductGroupName varchar(30),
			 IN ProductSubGroupName varchar(30),
			 IN lngLimit int,
			 IN SortField varchar(60),
			 IN SortOrder varchar(4))
BEGIN
	SET BarCode = REPLACE(BarCode, '''', '''''');
	SET ProductGroupName = REPLACE(ProductGroupName, '''', '''''');
	SET ProductSubGroupName = REPLACE(ProductSubGroupName, '''', '''''');

	SET @SQL = '		SELECT 
							 prd.ProductID
							,pkg.PackageID
							,IFNULL(pkg.BarCode1,pkg.BarCode4) BarCode
							,pkg.BarCode1
							,pkg.BarCode2
							,pkg.BarCode3
							,pkg.BarCode4
							,IFNULL(mtrx.Description,prd.ProductCode) ProductDesc
							,IF(IFNULL(mtrx.Description,'''') <> '''', CONCAT(prd.ProductDesc, '':'' , mtrx.Description), prd.ProductDesc) Description

							,pkg.UnitID
							,unt.UnitCode
							,unt.UnitName

							,pkg.Price
							,pkg.WSPrice
							,pkg.PurchasePrice
							,pkg.Quantity
							,pkg.VAT
							,pkg.EVAT
							,pkg.LocalTax
							,IFNULL(mtrx.MatrixID,0) MatrixID

						FROM tblProductPackage pkg
						INNER JOIN tblProducts prd ON pkg.ProductID = prd.ProductID
						INNER JOIN tblProductSubGroup prdsg ON prdsg.ProductSubGroupID = prd.ProductSubGroupID
						INNER JOIN tblProductGroup prdg ON prdg.ProductGroupID = prdsg.ProductGroupID
						INNER JOIN tblUnit unt ON pkg.UnitID = unt.UnitID
						LEFT OUTER JOIN tblProductBaseVariationsMatrix mtrx ON pkg.MatrixID = mtrx.MatrixID AND mtrx.ProductID = prd.ProductID
						WHERE prd.deleted = 0 ';
	
	IF ProductID <> 0 THEN
		SET @SQL = CONCAT(@SQL, 'AND prd.ProductID = ',ProductID,' ');
	END IF;

	IF IFNULL(BarCode,'') <> '' THEN
		SET @SQL = CONCAT(@SQL, 'AND (pkg.BarCode2 LIKE ''%',BarCode,'%'' ');
		SET @SQL = CONCAT(@SQL, '  OR pkg.BarCode3 LIKE ''%',BarCode,'%'' ');
		SET @SQL = CONCAT(@SQL, '  OR pkg.BarCode4 LIKE ''%',BarCode,'%'') ');
	END IF;

	IF IFNULL(ProductGroupName,'') <> '' THEN
		SET @SQL = CONCAT(@SQL, 'AND prdg.ProductGroupName LIKE ''%',BarCode,'%'' ');
	END IF;

	IF IFNULL(ProductSubGroupName,'') <> '' THEN
		SET @SQL = CONCAT(@SQL, 'AND prdsg.ProductSubGroupName LIKE ''%',BarCode,'%'' ');
	END IF;

	SET @SQL = CONCAT(@SQL, 'ORDER BY ',IF(IFNULL(SortField,'')='','PackageID',SortField),' ',IFNULL(SortOrder,'ASC'),' ');

	SET @SQL = CONCAT(@SQL, IF(lngLimit=0,'',CONCAT('LIMIT ',lngLimit,' ')));

	PREPARE cmd FROM @SQL;
	EXECUTE cmd;
	DEALLOCATE PREPARE cmd;

END;
GO
delimiter ;

/**************************************************************

	procProductPriceHistorySelect
	Lemuel E. Aceron
	March 22, 2013
	
	Desc: This will get the main product list

	CALL procProductPriceHistorySelect(null, null, 3057, null, null);
	
**************************************************************/
delimiter GO
DROP PROCEDURE IF EXISTS procProductPriceHistorySelect
GO

create procedure procProductPriceHistorySelect(
			 IN StartChangeDate datetime,
			 IN EndChangeDate datetime,
			 IN ProductID bigint,
			 IN SortField varchar(60),
			 IN SortOrder varchar(4))
BEGIN
	SET @SQL = CONCAT('	SELECT 
							 hst.PackageID
							,pkg.ProductID
							,pkg.MatrixID
							,prd.ProductCode
							,IF(ISNULL(mtrx.Description), prd.ProductDesc, CONCAT(prd.ProductDesc, '':'' , mtrx.Description)) AS Description 
							,pkg.UnitID
							,unt.UnitCode
							,unt.UnitName
							,hst.ChangeDate
							,pkg.Quantity
							,hst.PurchasePriceBefore
							,hst.PurchasePriceNow
							,hst.SellingPriceBefore
							,hst.SellingPriceNow
							,hst.VATBefore
							,hst.VATNow
							,hst.EVATBefore
							,hst.EVATNow
							,hst.LocalTaxBefore
							,hst.LocalTaxNow
							,hst.Remarks
							,usr.name
						FROM tblProductPackagePriceHistory hst
						INNER JOIN tblProductPackage pkg ON hst.PackageID = pkg.PackageID
						INNER JOIN tblProducts prd ON prd.ProductID = pkg.ProductID
						INNER JOIN tblUnit unt ON pkg.UnitID = unt.UnitID
						INNER JOIN sysAccessUserDetails usr ON usr.UID = hst.UID
						LEFT OUTER JOIN tblProductBaseVariationsMatrix mtrx ON pkg.MatrixID = mtrx.MatrixID AND pkg.ProductID = mtrx.ProductID 
						WHERE 1=1 ');

	IF IFNULL(StartChangeDate,'') <> '' THEN
		SET @SQL = CONCAT(@SQL, 'AND hst.ChangeDate >= ''',StartChangeDate,''' ');
	END IF;

	IF IFNULL(EndChangeDate,'') <> '' THEN
		SET @SQL = CONCAT(@SQL, 'AND hst.ChangeDate <= ''',EndChangeDate,''' ');
	END IF;

	IF ProductID <> 0 THEN
		SET @SQL = CONCAT(@SQL, 'AND prd.ProductID = ',ProductID,' ');
	END IF;

	SET @SQL = CONCAT(@SQL, 'ORDER BY ',IF(IFNULL(SortField,'')='','hst.ChangeDate',SortField),' ',IFNULL(SortOrder,'DESC'),' ');

	PREPARE cmd FROM @SQL;
	EXECUTE cmd;
	DEALLOCATE PREPARE cmd;

END;
GO
delimiter ;

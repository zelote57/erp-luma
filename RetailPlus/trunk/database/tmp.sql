
/**************************************************************

	procUpdateProductReorderOverStockPerProductIDPerBranch

	02Jun2015 : Lemu
	- create this procedure

	CALL procUpdateProductReorderOverStockPerProductIDPerBranch(1, 1497, 1, '2015-05-01 00:00:00', '2015-05-27 23:59:59');
	
**************************************************************/
delimiter GO
DROP PROCEDURE IF EXISTS procUpdateProductReorderOverStockPerProductIDPerBranch
GO

create procedure procUpdateProductReorderOverStockPerProductIDPerBranch(IN intBranchID INT(4), IN intProductID BIGINT, IN strSessionID VARCHAR(15), IN dteStartDate DATETIME, IN dteEndDate DATETIME)
BEGIN
	
	DECLARE intMaxCounter INT DEFAULT 0;
	DECLARE lngCtr, lngCount BIGINT DEFAULT 0;

	DECLARE intRID BIGINT DEFAULT 0;						-- the Required Inventory Days
	DECLARE decQuantity DECIMAL(18,3) DEFAULT 0;			-- the Current Quantity of item
	DECLARE decTotalQuantity DECIMAL(18,3) DEFAULT 0;		-- the Maximum Sales which will be use as the MaxThreshold
	DECLARE decAverageSales DECIMAL(18,3) DEFAULT 0;		-- the Average Sales which will be use as the MinThreshold
	DECLARE decIDC DECIMAL(18,3) DEFAULT 0;					-- the Inventory Days Covered of the existing Quantity
	DECLARE decTotalAverageSales DECIMAL(18,3) DEFAULT 0;	-- temporary holder for Average Sales
	
	DECLARE intValidTrxItemStatus, intReturnTrxItemStatus, intRefundTrxItemStatus, intDemoTrxItemStatus, intOutOfStockTrxItemStatus, intOrderSlipItemStatus INTEGER DEFAULT 0;
	DECLARE intOpenTransactionStatus INTEGER DEFAULT 0;
	DECLARE intLeadTimeToDeliver INTEGER DEFAULT 0;

	-- DECLARE strSessionID VARCHAR(15) DEFAULT '';

	SET intOpenTransactionStatus = 0; 
	SET intValidTrxItemStatus = 0;
	SET intReturnTrxItemStatus = 3;
	SET intRefundTrxItemStatus = 4;
	SET intDemoTrxItemStatus = 7;
	SET intOutOfStockTrxItemStatus = 8;

	-- set a random session id for producing a countingref
	-- SET strSessionID = rand();

	-- product a counting reference for a date
	-- this will be use as the ReferenceDate for sales coz sometimes in there is no sales in a certain date
	CALL procCreateCountingRefSession(strSessionID, dteStartDate, dteEndDate);
	
	IF (intBranchID = 0) THEN
		SELECT IFNULL(SUM(inv.Quantity),0), IFNULL(MAX(inv. RIDBranch),14) INTO decQuantity, intRID 
		FROM tblProductInventory inv WHERE ProductID = intProductID;
	ELSE
		SELECT IFNULL(inv.Quantity,0), IFNULL(inv.RIDBranch,14) INTO decQuantity, intRID 
		FROM tblProductInventory inv WHERE BranchID = intBranchID AND ProductID = intProductID;
	END IF;

	SET intValidTrxItemStatus = 0; SET intOrderSlipItemStatus = 5;
	SET intMaxCounter = 0; SET decTotalAverageSales = 0;
	
	-- put the maximum counter to be use in determining the average sales for covered period
	SELECT MAX(Counter) INTO intMaxCounter FROM tblCountingRef WHERE SessionID = strSessionID;

	-- put the lead time to deliver to be use in determining the MinThreshold
	SELECT IFNULL(LeadTimeToDeliver,0) INTO intLeadTimeToDeliver
	FROM tblProducts prd 
	INNER JOIN tblContacts cntct ON prd.SupplierID =  cntct.ContactID
	WHERE prd.ProductID = intProductID;

	SELECT * FROM tblCountingRef;
	SELECT intProductID, decAverageSales, decQuantity, decIDC, intRID,  (decTotalQuantity - decQuantity) AS ReorderQty, decTotalAverageSales, intMaxCounter, intLeadTimeToDeliver;
	
	

	-- get the total sales for the specific dates
	SELECT SUM(IF(TransactionItemStatus=intReturnTrxItemStatus,-items.Quantity,items.Quantity)) AS TotalAverageSales INTO decTotalAverageSales
	FROM tblTransactionItems items
	INNER JOIN tblTransactions trx ON trx.TransactionID = items.TransactionID
	WHERE TransactionStatus <> intOpenTransactionStatus 
		AND TransactionItemStatus IN (intValidTrxItemStatus, intReturnTrxItemStatus, intRefundTrxItemStatus, intDemoTrxItemStatus, intOutOfStockTrxItemStatus)
		AND TransactionDate BETWEEN dteStartDate AND dteEndDate;

	-- get the Daily Average Sales by dividing the totalsales / totalperiod
	-- this will also be the Min sales per day or Min sales in 1 day
	SET decAverageSales = decTotalAverageSales / intMaxCounter;
	
	-- Get the Inventory Days Covered of the existing Quantity
	
	IF decQuantity = 0 THEN								-- >Use this to catch an error when divedend (decQuantity) = 0
		SET decIDC = 0;
	ELSEIF decAverageSales = 0 THEN						-- >Use this to catch an error when divisor (decAverageSales) = 0
		SET decIDC = decQuantity;
	ELSE												-- >SET decIDC = decQuantity / decAverageSales;
		SET decIDC = decQuantity / decAverageSales;			
	END IF;
	  
	-- Get the daily average sales will be used as RIDMinThreshold
	-- need to study this further
	-- doesn't make sense. need explanation from mindoro
	-- IF decIDC <> 0 THEN
	-- 	IF (intRID > decIDC) THEN
	-- 		SET decTotalQuantity = (intRID * abs(decIDC)) - (decQuantity);
	-- 	ELSE
	-- 		SET decTotalQuantity = 0;
	-- 	END IF; 
	-- ELSE
	-- 	SET decTotalQuantity = 0;
	-- END IF; 
		
	 IF (decIDC > intRID) THEN 
	 	SET decTotalQuantity = decQuantity;
	 ELSE
	 	SET decTotalQuantity = round(intRID - decIDC) * decAverageSales;
	 END IF; 
		
	-- For checking purposes uncomment this
	-- SELECT * FROM tblCountingRef;
	SELECT intProductID, decAverageSales, decQuantity, decIDC, intRID, decTotalQuantity, (decTotalQuantity - decQuantity) AS ReorderQty, decTotalAverageSales, intMaxCounter;
	
	-- Set the RIDBranchMinThreshold and RIDBranchMaxThreshold
	UPDATE tblProductInventory SET RIDBranchMinThreshold = round(IFNULL(decAverageSales, 0), 2) * intLeadTimeToDeliver, 
								   RIDBranchMaxThreshold = round(IFNULL(decTotalQuantity, 0), 2) + (round(IFNULL(decAverageSales, 0), 2) * intLeadTimeToDeliver)
	WHERE BranchID = intBranchID AND ProductID = intProductID;
	
	SELECT RIDBranchMinThreshold, RIDBranchMaxThreshold FROM tblProductInventory WHERE productID = intProductID;

	DELETE FROM tblCountingRef;

END;
GO
delimiter ;
-- =============================================
-- Script Template
-- =============================================

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


/********************************************
	procSaveParkingRate
	
	CALL procSaveParkingRate(0, 5448, 'Monday', '00:00', '23:59', 60, 10, 360, 100, 'Lemuel');
	CALL procSaveParkingRate(0, 5448, 'Tuesday', '00:00', '23:59', 60, 10, 360, 100, 'Lemuel');
	CALL procSaveParkingRate(0, 5448, 'Wednesday', '00:00', '23:59', 60, 10, 360, 100, 'Lemuel');
	CALL procSaveParkingRate(0, 5448, 'Thursday', '00:00', '23:59', 60, 10, 360, 100, 'Lemuel');
	CALL procSaveParkingRate(0, 5448, 'Friday', '00:00', '23:59', 60, 10, 360, 100, 'Lemuel');
	CALL procSaveParkingRate(0, 5448, 'Saturday', '00:00', '23:59', 60, 10, 360, 100, 'Lemuel');
	CALL procSaveParkingRate(0, 5448, 'Sunday', '00:00', '23:59', 60, 10, 360, 100, 'Lemuel');

	Jul 21, 2013 : Lem
	- create this procedure
	
	Aug 17, 2014 : Lem
	- replaced with procSaveParkingRate
********************************************/
delimiter GO
DROP PROCEDURE IF EXISTS procSaveParkingRate
GO
delimiter ;


create procedure procSaveParkingRate(
	IN lngParkingRateID BIGINT,
	IN lngProductID BIGINT,
	IN strDayOfWeek VARCHAR(9),
	IN strStartTime VARCHAR(5),
	IN strEndtime VARCHAR(5),
	IN intNoOfUnitPerMin INT,
	IN decPerUnitPrice DECIMAL(18,3),
	IN intMinimumStayInMin INT,
	IN decMinimumStayPrice DECIMAL(18,3),
	IN strCreatedByName VARCHAR(100)
	)
BEGIN
	
	IF EXISTS(SELECT ProductID FROM tblParkingRates WHERE ParkingRateID = lngParkingRateID) THEN 
		UPDATE tblParkingRates SET
			DayOfWeek			= strDayOfWeek,
			StartTime			= strStartTime,
			EndTime				= strEndTime,
			NoofUnitPerMin		= intNoOfUnitPerMin,
			PerUnitPrice		= decPerUnitPrice,
			MinimumStayInMin	= intMinimumStayInMin,
			MinimumStayPrice	= decMinimumStayPrice,
			LastUpdatedByName	= strCreatedByName,
			LastUpdatedOn		= NOW()
		WHERE ParkingRateID		= lngParkingRateID;
	ELSE
		INSERT INTO tblParkingRates(ProductID, DayOfWeek, StartTime, EndTime, NoOfUnitperMin, PerUnitPrice, MinimumStayInMin, MinimumStayPrice, CreatedByName, CreatedOn)
			VALUES(lngProductID, strDayOfWeek, strStartTime, strEndTime, intNoOfUnitperMin, decPerUnitPrice, intMinimumStayInMin, decMinimumStayPrice, strCreatedByName, NOW());
	END IF;
				
END;
GO
delimiter ;


/*********************************
	procCashPaymentInsert
	Lemuel E. Aceron
	CALL procCashPaymentInsert();
	
	Sep 01, 2009 - create this procedure
	Aug 29, 2014 - replaced with procCashPaymentSave
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



/*********************************
	procTerminalReportInitializeZRead
	Lemuel E. Aceron
	
	May 5, 2009 - insert the ff items in tblTerminalReportHistory
					NoOfDiscountedTransactions, NegativeAdjustments, NoOfNegativeAdjustmentTransactions,
					PromotionalItems, CreditSalesTax, BatchCounter
					
	May 5, 2009 - insert the ff items in tblTerminalReportHistory
					NoOfDiscountedTransactions, NegativeAdjustments, NoOfNegativeAdjustmentTransactions,
					PromotionalItems, CreditSalesTax

	CALL procTerminalReportInitializeZRead(1, '01', now(),'Lem', 0);
*********************************/
delimiter GO
DROP PROCEDURE IF EXISTS procTerminalReportInitializeZRead
GO

create procedure procTerminalReportInitializeZRead(
	IN intBranchID int(4), 
	IN strTerminalNo varchar(10), 
	IN dteDateLastInitialized DateTime, 
	IN strInitializedBy varchar(150),
	IN intWithOutTF tinyint(1)
)
BEGIN
	DECLARE decTrustFund DECIMAL(18,3) DEFAULT 0;

	-- use to remove the TF in the newgrandtotal
	-- use to add the TF in the new oldgrandtotal
	DECLARE decVirtualTrustFund DECIMAL(18,3) DEFAULT 0;

	DECLARE decOldGrandTotal DECIMAL(18,3) DEFAULT 0;
	DECLARE decNewGrandTotal DECIMAL(18,3) DEFAULT 0;

	DECLARE strEndingTransactionNo VARCHAR(30);
	DECLARE strEndingORNo VARCHAR(30);

	SET decOldGrandTotal = (SELECT OldGrandTotal FROM tblTerminalReport WHERE BranchID = intBranchID AND TerminalNo = strTerminalNo);

	IF (intWithOutTF = 1 OR intWithOutTF = -1) THEN
		SET decTrustFund = 0;
		SET decVirtualTrustFund = (SELECT TRUSTFUND FROM tblTerminal WHERE BranchID = intBranchID AND TerminalNo = strTerminalNo);
		SET decNewGrandTotal = (SELECT OldGrandTotal + (GrossSales * (100-decVirtualTrustFund)/100) FROM tblTerminalReport WHERE BranchID = intBranchID AND TerminalNo = strTerminalNo);
	ELSE
		SET decTrustFund = (SELECT TRUSTFUND FROM tblTerminal WHERE BranchID = intBranchID AND TerminalNo = strTerminalNo);
		SET decVirtualTrustFund = 0;

		SET decOldGrandTotal = (SELECT OldGrandTotal FROM tblTerminalReport WHERE BranchID = intBranchID AND TerminalNo = strTerminalNo);
		SET decNewGrandTotal = (SELECT NewGrandTotal FROM tblTerminalReport WHERE BranchID = intBranchID AND TerminalNo = strTerminalNo);
		
	END IF;

	SET strEndingTransactionNo = (SELECT EndingTransactionNo FROM tblTerminalReport WHERE BranchID = intBranchID AND TerminalNo = strTerminalNo);
	SET strEndingORNo = (SELECT EndingORNo FROM tblTerminalReport WHERE BranchID = intBranchID AND TerminalNo = strTerminalNo);

	INSERT INTO tblTerminalReportHistory (
					BranchID, TerminalID, TerminalNo, BeginningTransactionNo, EndingTransactionNo, BeginningORNo, EndingORNo, 
					ZReadCount, XReadCount, NetSales, GrossSales, TotalDiscount, SNRDiscount, PWDDiscount, OtherDiscount, TotalCharge, DailySales, 
					ItemSold, QuantitySold, GroupSales, OldGrandTotal, NewGrandTotal, ActualOldGrandTotal, ActualNewGrandTotal, 
					VATExempt, NonVATableAmount, VATableAmount, VAT, EVATableAmount, NonEVATableAmount, EVAT, LocalTax, CashSales, 
					ChequeSales, CreditCardSales, CreditSales, CreditPayment, CreditPaymentCash, CreditPaymentCheque,
					CreditPaymentCreditCard, CreditPaymentDebit, DebitPayment, RewardPointsPayment, RewardConvertedPayment, CashInDrawer, 
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
					PromotionalItems, CreditSalesTax, BatchCounter, 
					NoOfReprintedTransaction, TotalReprintedTransaction, InitializedBy, IsProcessed) 
				(SELECT 
					BranchID, TerminalID, TerminalNo, BeginningTransactionNo, EndingTransactionNo, BeginningORNo, EndingORNo, 
					ZReadCount, XReadCount, NetSales, GrossSales, TotalDiscount, SNRDiscount, PWDDiscount, OtherDiscount, TotalCharge, DailySales, 
					ItemSold, QuantitySold, GroupSales, decOldGrandTotal, decNewGrandTotal, ActualOldGrandTotal, ActualNewGrandTotal, 
					VATExempt, NonVATableAmount, VATableAmount, VAT, EVATableAmount, NonEVATableAmount, EVAT, LocalTax, CashSales, 
					ChequeSales, CreditCardSales, CreditSales, CreditPayment, CreditPaymentCash, CreditPaymentCheque,
					CreditPaymentCreditCard, CreditPaymentDebit, DebitPayment, RewardPointsPayment, RewardConvertedPayment, CashInDrawer, 
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
					PromotionalItems, CreditSalesTax, BatchCounter, 
					NoOfReprintedTransaction, TotalReprintedTransaction, strInitializedBy, IsProcessed FROM tblTerminalReport WHERE BranchID = intBranchID AND TerminalNo = strTerminalNo);
					
	UPDATE tblTerminalReport SET 
		OldGrandTotal =  decNewGrandTotal / ((100 - decVirtualTrustFund) / 100),
		NewGrandTotal =  decNewGrandTotal / ((100 - decVirtualTrustFund) / 100),
		ActualOldGrandTotal = ActualNewGrandTotal
	WHERE BranchID = intBranchID AND TerminalNo = strTerminalNo;
	
	UPDATE tblTerminalReport SET 
					BeginningTransactionNo				=  EndingTransactionNo, 
					BeginningORNo						=  EndingORNo, 
					GrossSales							=  0, 
					TotalDiscount						=  0, 
					SNRDiscount							=  0, 
					PWDDiscount							=  0, 
					OtherDiscount						=  0,
					TotalCharge							=  0, 
					DailySales							=  0, 
					ItemSold							=  0, 
					QuantitySold						=  0, 
					NetSales							=  0, 
					GroupSales							=  0, 
					VATExempt   						=  0, 
					NonVATableAmount					=  0, 
					VATableAmount						=  0, 
					VAT									=  0, 
					EVATableAmount						=  0, 
					NonEVATableAmount					=  0, 
					EVAT								=  0, 
					LocalTax							=  0, 
					CashSales							=  0, 
					ChequeSales							=  0, 
					CreditCardSales						=  0, 
					CreditSales							=  0, 
					CreditPayment						=  0, 
					CreditPaymentCash					=  0, 
					CreditPaymentCheque					=  0, 
					CreditPaymentCreditCard				=  0, 
					CreditPaymentDebit					=  0, 
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
					NoOfReprintedTransaction			=  0,
					TotalReprintedTransaction			=  0, 
					IsProcessed							=  0,
					DateLastInitialized					=  dteDateLastInitialized
			WHERE BranchID = intBranchID AND TerminalNo = strTerminalNo;
			
	
	INSERT INTO tblCashierReportHistory (
					CashierID, BranchID, TerminalID, TerminalNo, BeginningTransactionNo, BeginningORNo, 
					EndingTransactionNo, EndingORNo, NetSales, GrossSales, 
					TotalDiscount, SNRDiscount, PWDDiscount, OtherDiscount, TotalCharge, DailySales, 
					ItemSold, QuantitySold, GroupSales, VATExempt, NonVATableAmount, VATableAmount, VAT, EVATableAmount, NonEVATableAmount, EVAT, LocalTax, 
					CashSales, ChequeSales, CreditCardSales, CreditSales, 
					CreditPayment, CreditPaymentCash, CreditPaymentCheque, CreditPaymentCreditCard, 
					CreditPaymentDebit, DebitPayment, RewardPointsPayment, RewardConvertedPayment, CashInDrawer, 
					TotalDisburse, CashDisburse, ChequeDisburse, CreditCardDisburse, 
					TotalWithhold, CashWithhold, ChequeWithhold, CreditCardWithhold, 
					TotalPaidOut, CashPaidOut, ChequePaidOut, CreditCardPaidOut, 
					TotalDeposit, CashDeposit, ChequeDeposit, CreditCardDeposit, DebitDeposit, 
					BeginningBalance, VoidSales, RefundSales, ItemsDiscount, SubtotalDiscount, 
					NoOfCashTransactions, NoOfChequeTransactions, 
					NoOfCreditCardTransactions, NoOfCreditTransactions, 
					NoOfCombinationPaymentTransactions, 
					NoOfCreditPaymentTransactions, NoOfDebitPaymentTransactions, 
					NoOfClosedTransactions, NoOfRefundTransactions, 
					NoOfVoidTransactions, NoOfRewardPointsPayment, NoOfTotalTransactions, 
					CashCount, LastLoginDate,
					NoOfDiscountedTransactions, NegativeAdjustments, NoOfNegativeAdjustmentTransactions,
					PromotionalItems, CreditSalesTax )
				(SELECT 
					CashierID, BranchID, TerminalID, TerminalNo, BeginningTransactionNo, BeginningORNo, 
					strEndingTransactionNo, strEndingORNo, NetSales, GrossSales, 
					TotalDiscount, SNRDiscount, PWDDiscount, OtherDiscount, TotalCharge, DailySales, 
					ItemSold, QuantitySold, GroupSales, VATExempt, NonVATableAmount, VATableAmount, VAT, EVATableAmount, NonEVATableAmount, EVAT, LocalTax, 
					CashSales, ChequeSales, CreditCardSales, CreditSales, 
					CreditPayment, CreditPaymentCash, CreditPaymentCheque, CreditPaymentCreditCard, 
					CreditPaymentDebit, DebitPayment, RewardPointsPayment, RewardConvertedPayment, CashInDrawer, 
					TotalDisburse, CashDisburse, ChequeDisburse, CreditCardDisburse, 
					TotalWithhold, CashWithhold, ChequeWithhold, CreditCardWithhold, 
					TotalPaidOut, CashPaidOut, ChequePaidOut, CreditCardPaidOut, 
					TotalDeposit, CashDeposit, ChequeDeposit, CreditCardDeposit, DebitDeposit,
					BeginningBalance, VoidSales, RefundSales, ItemsDiscount, SubtotalDiscount, 
					NoOfCashTransactions, NoOfChequeTransactions, 
					NoOfCreditCardTransactions, NoOfCreditTransactions, 
					NoOfCombinationPaymentTransactions, 
					NoOfCreditPaymentTransactions, NoOfDebitPaymentTransactions, 
					NoOfClosedTransactions, NoOfRefundTransactions, 
					NoOfVoidTransactions, NoOfRewardPointsPayment, NoOfTotalTransactions, 
					CashCount, LastLoginDate,
					NoOfDiscountedTransactions, NegativeAdjustments, NoOfNegativeAdjustmentTransactions,
					PromotionalItems, CreditSalesTax FROM tblCashierReport WHERE BranchID = intBranchID AND TerminalNo = strTerminalNo);
	
	-- Just delete the cashier's this will be recreated anyway.
	-- also the syncid does update and the cashiers logs will be cleaned up
	DELETE FROM tblCashierReport WHERE BranchID = intBranchID AND TerminalNo = strTerminalNo;
	
END;
GO
delimiter ;
/**************************************************************
	procProcessCreditBills
	Lemuel E. Aceron
	CALL procProcessCreditBills(1);
	08-Oct-2014	Create this procedure
	
	Sample:
		PurchaseStart:	2012-12-10
		PurchaseEnd:	2013-01-09
		CreditCutOff:	2012-12-31
		BillingDate:	2012-01-10

	
	
**************************************************************/
delimiter GO
DROP PROCEDURE IF EXISTS procProcessCreditBills
GO

create procedure procProcessCreditBills(
	IN boDebug TINYINT(1)	
	 ) 
BEGIN
	DECLARE lngCreditBillID BIGINT DEFAULT 0;
	
	DECLARE dteBillingDate, dteCreditPurcStartDateToProcess, dteCreditPurcEndDateToProcess DATE;
	DECLARE dteCreditCutOffDate, dteStartCreditCutOffDate, dteEndCreditCutOffDate, dteCreditPaymentDueDate DATE;
	DECLARE dtePrev2BillingDate, dtePrevBillingDate, dteNextBillingDate DATE;

	DECLARE bolCreditUseLastDayCutOffDate TINYINT default 1;
	DECLARE decCreditFinanceCharge, decCreditMinimumPercentageDue, decCreditMinimumAmountDue, decCreditLatePenaltyCharge DECIMAL(10,3) DEFAULT 0;
	
	DECLARE dteCurrDate datetime default now();
	DECLARE lngCreatedByID BIGINT default 1;
	DECLARE strCreatedByName varchar(100) default 'SysCreditBiller';
	DECLARE intCardtypeID INT(10) default 0;

	DECLARE strCardTypeCode VARCHAR(100);

	-- get the card type to process
	SET strCardTypeCode = (SELECT ConfigValue FROM sysCreditConfig WHERE ConfigName = 'IndividualCardTypeCode');

	IF (boDebug = 1) THEN
		-- source C:\RetailPlus\RetailPlus\database\credit\credit_proc.sql
		UPDATE tblCardTypes SET CreditPurcStartDateToProcess	= '2014-09-10' WHERE CardTypeCode = 'HP CREDIT CARD';
		UPDATE tblCardTypes SET CreditPurcEndDateToProcess		= '2014-10-09' WHERE CardTypeCode = 'HP CREDIT CARD';
		UPDATE tblCardTypes SET CreditCutOffDate				= '2014-09-30' WHERE CardTypeCode = 'HP CREDIT CARD';
		UPDATE tblCardTypes SET BillingDate						= '2014-10-10' WHERE CardTypeCode = 'HP CREDIT CARD';
		UPDATE tblCardTypes SET CreditUseLastDayCutOffDate		= 1 WHERE CardTypeCode = 'HP CREDIT CARD';

		Truncate TABLE tblCreditBills;
		Truncate TABLE tblCreditBillHeader;
		Truncate TABLE tblCreditBillDetail;

		UPDATE tblContactCreditCardInfo SET LastBillingDate = '2014-09-10';

		DELETE FROM tblTransactions WHERE (TerminalNo = '00' AND BranchID = 1) or TransactionStatus = 7;
		DELETE FROM tblTransactionItems WHERE ProductCode = 'CREDIT PAYMENT' OR ProductCode = 'CCI LATE PAYMENT CHARGE' OR ProductCode = 'CCI FINANCE CHARGE';
		DELETE FROM tblCreditPayment WHERE CreditPaymentID >= 3;
		UPDATE tblCreditPayment  SET AmountPaid = 0;
		
		CALL procSyncContactCredit();
		-- do not put this here it will throw a recursive
		-- CALL procProcessCreditBills(1);
	END IF;

	

	-- get the variable charges
	SELECT CardtypeID, 
		   CreditFinanceCharge/100, CreditMinimumPercentageDue/100, CreditMinimumAmountDue, CreditLatePenaltyCharge/100,
		   CreditPurcStartDateToProcess, CreditPurcEndDateToProcess, CreditCutOffDate, BillingDate, CreditUseLastDayCutOffDate
	INTO   intCardtypeID, 
		   decCreditFinanceCharge, decCreditMinimumPercentageDue, decCreditMinimumAmountDue, decCreditLatePenaltyCharge,
		   dteCreditPurcStartDateToProcess, dteCreditPurcEndDateToProcess, dteCreditCutOffDate, dteBillingDate, bolCreditUseLastDayCutOffDate
	FROM tblCardTypes WHERE CardTypeCode = strCardTypeCode;
	
	-- check the variable charges
	IF (boDebug = 1) THEN
		SELECT intCardtypeID, decCreditFinanceCharge, decCreditMinimumPercentageDue, decCreditMinimumAmountDue, decCreditLatePenaltyCharge,
			   dteCreditPurcStartDateToProcess, dteCreditPurcEndDateToProcess, dteCreditCutOffDate, dteBillingDate, bolCreditUseLastDayCutOffDate;
	END IF;

	-- IF dteBillingDate < DATE_ADD(now(), INTERVAL 1 MONTH) THEN
	IF dteBillingDate < now() THEN
		SET dtePrev2BillingDate = (SELECT DATE_ADD(dteBillingDate, INTERVAL -2 MONTH));
		SET dtePrevBillingDate = (SELECT DATE_ADD(dteBillingDate, INTERVAL -1 MONTH));
		SET dteNextBillingDate = (SELECT DATE_ADD(dteBillingDate, INTERVAL 1 MONTH));

		SET dteStartCreditCutOffDate = (SELECT DATE_FORMAT(DATE_ADD(dteCreditCutOffDate, INTERVAL -1 MONTH) ,'%Y-%m-01'));
		SET dteEndCreditCutOffDate = (SELECT DATE_ADD(dteCreditCutOffDate, INTERVAL -1 MONTH));
		SET dteCreditPaymentDueDate = (SELECT DATE_ADD(dteCreditCutOffDate, INTERVAL 1 MONTH));

		-- IF DAY(dteCreditCutOffDate) >= 28 then
		IF bolCreditUseLastDayCutOffDate = 1 OR ISNULL(bolCreditUseLastDayCutOffDate) THEN
			SET dteCreditCutOffDate = (SELECT LAST_DAY(dteCreditCutOffDate));
			SET dteStartCreditCutOffDate = (SELECT DATE_FORMAT(DATE_ADD(dteCreditCutOffDate, INTERVAL -1 MONTH) ,'%Y-%m-01'));
			SET dteEndCreditCutOffDate = (SELECT DATE_ADD(dteCreditCutOffDate, INTERVAL -1 MONTH));
			SET dteCreditPaymentDueDate = (SELECT LAST_DAY(dteCreditPaymentDueDate));
		END IF;

		-- check 
		IF (boDebug = 1) THEN
			SELECT dtePrev2BillingDate, dtePrevBillingDate, dteBillingDate, dteNextBillingDate,
				   dteCreditCutOffDate, dteStartCreditCutOffDate, dteEndCreditCutOffDate, dteCreditPaymentDueDate;
		END IF;

		-- put the creditbill for this rundate / billingdate
		IF NOT EXISTS(SELECT CreditBillID FROM tblCreditBills WHERE BillingDate = dteBillingDate) THEN
			INSERT INTO tblCreditBills(  CreditPurcStartDateToProcess ,CreditPurcEndDateToProcess ,BillingDate ,CreditCutOffDate, CreditPaymentDueDate
										,CreditFinanceCharge ,CreditMinimumPercentageDue ,CreditMinimumAmountDue
										,CreditLatePenaltyCharge ,CreatedOn ,CreatedByID ,CreatedByName)
			SELECT						 dteCreditPurcStartDateToProcess ,dteCreditPurcEndDateToProcess ,dteBillingDate ,dteCreditCutOffDate, dteCreditPaymentDueDate
										,decCreditFinanceCharge ,decCreditMinimumPercentageDue ,decCreditMinimumAmountDue
										,decCreditLatePenaltyCharge ,dteCurrDate , lngCreatedByID, strCreatedByName;
		END IF;

		/** Put the credit paramateres to be process *****/
		
		SELECT CreditBillID 
		INTO  lngCreditBillID 
		FROM tblCreditBills WHERE BillingDate = dteBillingDate;

		/** end-Put the credit paramateres to be process *****/

		-- check
		IF (boDebug = 1) THEN
			SELECT lngCreditBillID, a.* FROM tblCreditBills a WHERE BillingDate = dteBillingDate;
		END IF;

		/** Insert the Contacts that have credits as header *****/
		INSERT INTO tblCreditBillHeader(CreditBillID ,ContactID ,CreditLimit ,RunningCreditAmt
										,BillingDate ,CreatedOn ,CreatedByID ,CreatedByName)
		SELECT lngCreditBillID, CUS.ContactID ,CUS.CreditLimit ,CUS.Credit RunningCreditAmt
										,dteBillingDate ,dteCurrDate ,lngCreatedByID ,strCreatedByName
		FROM tblContacts CUS
			INNER JOIN tblContactCreditCardInfo CCI ON CUS.ContactID = CCI.CustomerID
			INNER JOIN tblCardTypes CTY ON CCI.CreditCardTypeID = CTY.CardTypeID 
		WHERE CCI.LastBillingDate <= dteBillingDate
			AND CUS.ContactID NOT IN (SELECT ContactID FROM tblCreditBillHeader WHERE CreditBillID = lngCreditBillID)
			AND CTY.CardTypeID = intCardtypeID;
		/** end-Insert the Contacts that have credits as header *****/
	
		-- check
		IF (boDebug = 1) THEN
			SELECT * FROM tblCreditBillHeader WHERE RunningCreditAmt > 0 LIMIT 10;
		END IF;

		/** Put the credit details for each contacts *****/
		-- Insert the Credit Details
		INSERT INTO tblCreditBillDetail(CreditBillHeaderID ,TransactionDate ,Description ,Amount ,TransactionTypeID ,TransactionRefID, TerminalNoRefID, BranchIDRefID)
		SELECT CreditBillHeaderID ,CreditDate ,CONCAT('Credit: Trn#:' ,CONVERT(TransactionNo, UNSIGNED INTEGER),' @ Ter#:',TerminalNo) 'Description'
				,SUM(Amount) CurrMonthCreditAmt ,1 tblCreditPaymentTransactionTypeID ,CRP.TransactionID TransactionRefID, CRP.TerminalNo, CRP.BranchID
		FROM tblCreditBillHeader CBH
			INNER JOIN tblCreditPayment CRP on CRP.ContactID = CBH.ContactID
		WHERE CBH.CreditBillID = lngCreditBillID 
			AND CONVERT(CreditDate, DATE) BETWEEN dteCreditPurcStartDateToProcess AND dteCreditPurcEndDateToProcess
			AND CRP.TerminalNo <> '00'	-- do not include the previous late charges
			AND CRP.TransactionID NOT IN (SELECT TransactionRefID FROM tblCreditBillDetail 
																	 WHERE TransactionTypeID = 1
																		AND CONVERT(CreditDate, DATE) BETWEEN dteCreditPurcStartDateToProcess AND dteCreditPurcEndDateToProcess)
		GROUP BY CreditBillHeaderID ,TransactionNo ,CreditDate;

		-- Insert the Payment Details
		INSERT INTO tblCreditBillDetail(CreditBillHeaderID ,TransactionDate ,Description ,Amount ,TransactionTypeID ,TransactionRefID, TerminalNoRefID, BranchIDRefID)
		SELECT CreditBillHeaderID, TransactionDate, Description, CurrMonthCreditAmt, tblCreditPaymentTransactionTypeID, TransactionRefID, TerminalNo, BranchID
		FROM
			(
				SELECT CreditBillHeaderID, TransactionDate
						,CONCAT('Payment: Trn#:' ,CONVERT(TransactionNo, UNSIGNED INTEGER),' @ Ter#:',TerminalNo) 'Description'
						,-SUM(Subtotal) CurrMonthCreditAmt ,2 tblCreditPaymentTransactionTypeID ,Trx.TransactionID TransactionRefID, Trx.TerminalNo, Trx.BranchID
				FROM tblTransactions Trx 
					INNER JOIN tblTransactionItems TrxD ON Trx.TransactionID = TrxD.TransactionID
					INNER JOIN tblCreditBillHeader CBH ON CBH.ContactID = Trx.CustomerID AND CBH.CreditBillID = lngCreditBillID
				WHERE TrxD.ProductCode = 'CREDIT PAYMENT'
					AND TransactionStatus = 7 -- creditPaymentStatus
					-- change to be the same as the puchstartdate
					-- AND CONVERT(Trx.TransactionDate, DATE) BETWEEN dteStartCreditCutOffDate AND dteEndCreditCutOffDate
					AND CONVERT(Trx.TransactionDate, DATE) BETWEEN dteCreditPurcStartDateToProcess AND dteCreditPurcEndDateToProcess
					AND Trx.TransactionID NOT IN (SELECT TransactionRefID FROM tblCreditBillDetail 
																			 WHERE TransactionTypeID = 2
																				AND CONVERT(TransactionDate, DATE) BETWEEN dteCreditPurcStartDateToProcess AND dteCreditPurcEndDateToProcess)
				GROUP BY TransactionNo
			) Payments WHERE CreditBillHeaderID <> 0;

			-- check
		IF (boDebug = 1) THEN
			SELECT concat('getting tblCreditBillDetail from ', dteCreditPurcStartDateToProcess, ' to ', dteCreditPurcEndDateToProcess) AS Step ;

			SELECT CRP.* FROM tblCreditBillHeader CBH
				INNER JOIN tblCreditPayment CRP on CRP.ContactID = CBH.ContactID limit 10;
			
			SELECT * FROM tblCreditBillDetail;
		END IF;

		/** end-Put the credit details for each contacts *****/

		/** Update the header with the details *****/
		UPDATE tblCreditBillHeader AS CBH 
		LEFT OUTER JOIN	
		( 
			SELECT CreditBillHeaderID ,SUM(Amount) CurrMonthCreditAmt 
			FROM tblCreditBillDetail  WHERE TransactionTypeID = 1 -- AND Description NOT LIKE '%Ter#:00' -- remove the previous 
			GROUP BY CreditBillHeaderID
		)	AS CBDcmca ON CBH.CreditBillHeaderID = CBDcmca.CreditBillHeaderID	-- <-update the CurrMonthCreditAmt
		LEFT OUTER JOIN 
		(  
			SELECT CreditBillHeaderID ,SUM(Amount) CurrMonthAmountPaid
			FROM tblCreditBillDetail  WHERE TransactionTypeID = 2
			GROUP BY CreditBillHeaderID
		)	AS CBDcmap ON CBH.CreditBillHeaderID = CBDcmap.CreditBillHeaderID	-- <-update the CurrMonthAmountPaid
		LEFT OUTER JOIN 
		(  
			SELECT ContactID ,CurrMonthAmountPaid ,CurrentDueAmount ,MinimumAmountDue ,Prev1MoCurrentDueAmount ,Prev2MoCurrentDueAmount ,TotalBillCharges ,CurrMonthCreditAmt
			FROM tblCreditBillHeader 
			WHERE BillingDate = dtePrevBillingDate
		)	AS CBHP ON CBH.ContactID = CBHP.ContactID							-- <-update the PrevMonthMinAmtDue
		SET  CBH.CurrMonthCreditAmt = IFNULL(CBDcmca.CurrMonthCreditAmt,0)
			,CBH.CurrMonthAmountPaid = IFNULL(CBDcmap.CurrMonthAmountPaid,0)
			,CBH.Prev1MoCurrentDueAmount = IFNULL(CBHP.TotalBillCharges,0) + IFNULL(CBHP.CurrMonthCreditAmt,0)
			,CBH.Prev1MoMinimumAmountDue = IFNULL(CBHP.MinimumAmountDue,0)
			,CBH.Prev1MoCurrMonthAmountPaid = IFNULL(CBHP.CurrMonthAmountPaid,0)
			,CBH.Prev2MoCurrentDueAmount = IFNULL(CBHP.Prev2MoCurrentDueAmount,0) + IFNULL(CBHP.Prev1MoCurrentDueAmount,0) + IFNULL(CBHP.CurrMonthAmountPaid,0)
		WHERE CBH.CreditBillID = lngCreditBillID;

		-- check
		IF (boDebug = 1) THEN
			SELECT 'done updating the header with the details ' AS Step ;

			SELECT * FROM tblCreditBillHeader WHERE CreditBillHeaderID IN (SELECT CreditBillHeaderID FROM tblCreditBillDetail) LIMIT 10;
		END IF;

		/***********INSERT THE CCI LATE PAYMENT CHARGE**************/
		-- update the TransactionRefID to be use for saving to tblTransactions
		INSERT INTO tblTransactions (	 TransactionNo, BranchID, BranchCode, CustomerID, CustomerName
										,AgentID, AgentName, CreatedByID, CreatedByName, CashierID, CashierName
										,TerminalNo, TransactionDate, TransactionStatus
										,WaiterID, WaiterName ,AgentPositionName, AgentDepartmentName
										,SubTotal ,AmountPaid ,CreditPayment ,PaymentType ,DateClosed, Datasource)
		SELECT LPAD(TransactionNo + rownum,14,'0'), 1, 'Main', CBH.ContactID ,CBH.ContactName 
						,1 ,'RetailPlus Agent' ,1 ,'Credit Bill' ,1 ,'Credit Biller-LPC'
						,'00' ,dteBillingDate ,1
						,2 ,'System Credit Bill' ,'Credit Bill Position' ,'Credit Bill Dept.'
						,CBH.LatePaymentChargeAmt ,CBH.LatePaymentChargeAmt ,CBH.LatePaymentChargeAmt ,5 ,dteBillingDate, CONCAT('CreditBillerLPC:', lngCreditBillID)
		FROM (
			SELECT @rownum:=@rownum+1 AS rownum, CBH.*
			FROM (
				SELECT CBH.ContactID ,CON.ContactName ,((Prev2MoCurrentDueAmount + Prev1MoCurrentDueAmount) * decCreditLatePenaltyCharge) LatePaymentChargeAmt
				FROM tblCreditBillHeader CBH 
				INNER JOIN tblContacts CON ON CBH.ContactID = CON.ContactID
				WHERE CBH.CreditBillID = lngCreditBillID AND (Prev2MoCurrentDueAmount + Prev1MoCurrentDueAmount) > 0 AND CurrMonthAmountPaid = 0
		) CBH, (SELECT @rownum:=0) r) CBH, (SELECT MAX(TransactionNo) TransactionNo  FROM tblTransactions) r;

		-- insert the late payment Charges FOR bills that did not pay for the 1 month.
		INSERT INTO tblCreditBillDetail(CreditBillHeaderID ,TransactionDate ,Description ,Amount ,TransactionTypeID ,TransactionRefID, TerminalNoRefID, BranchIDRefID)
		SELECT CreditBillHeaderID ,dteBillingDate 
				,CONCAT('Late Payment Charge  : (', Prev2MoCurrentDueAmount + Prev1MoCurrentDueAmount,' * ',decCreditLatePenaltyCharge,')') 'Description'
				,((Prev2MoCurrentDueAmount + Prev1MoCurrentDueAmount) * decCreditLatePenaltyCharge) CurrMonthCreditAmt 
				,3 tblCreditPaymentTransactionTypeID ,TRX.TransactionID TransactionRefID, Trx.TerminalNo, Trx.BranchID
		FROM tblCreditBillHeader CBH
		INNER JOIN tblTransactions TRX ON CBH.ContactID = TRX.CustomerID 
													   AND CONVERT(TRX.TransactionDate, DATE) = dteBillingDate
													   AND TRX.CashierName = 'Credit Biller-LPC'
													   AND TRX.Datasource = CONCAT('CreditBillerLPC:', lngCreditBillID)
		WHERE CBH.CreditBillID = lngCreditBillID AND (Prev2MoCurrentDueAmount + Prev1MoCurrentDueAmount) > 0 AND CurrMonthAmountPaid = 0;

		-- insert the transactionitems
		INSERT INTO tblTransactionItems(TransactionID, ProductID, ProductCode, BarCode, Description, 
                                ProductUnitID, ProductUnitCode, Quantity, Price, SellingPrice, 
                                Discount, ItemDiscount, ItemDiscountType, Amount, VAT, VatableAmount, EVAT, LocalTax, 
                                VariationsMatrixID, MatrixDescription, ProductGroup, ProductSubGroup, TransactionItemStatus,
                                DiscountCode,DiscountRemarks,ProductPackageID,MatrixPackageID, PackageQuantity,PromoQuantity,
                                PromoValue,PromoInPercent,PromoType,PromoApplied,PurchasePrice,PurchaseAmount,
                                IncludeInSubtotalDiscount,OrderSlipPrinter,OrderSlipPrinted,PercentageCommision, Commision, DataSource)
		SELECT TRX.TransactionID , prd.ProductID, prd.ProductCode, prd.BarCode1, prd.ProductDesc
									,prd.UnitID, 'PC', 1, 0, (Prev2MoCurrentDueAmount + Prev1MoCurrentDueAmount) * decCreditLatePenaltyCharge
									,0, 0, 0, (Prev2MoCurrentDueAmount + Prev1MoCurrentDueAmount) * decCreditLatePenaltyCharge AS Amount, 0, 0, 0, 0
									,0, '', 'System Default', 'System Default', 0
									,'', '', prd.PackageID, 0, 1, 0
									,0, 0, 0, 0, 0, 0
									,0, 0, 0, 0, 0, CONCAT('CreditBillerLPC:', lngCreditBillID)
						FROM tblCreditBillHeader CBH
						INNER JOIN tblContacts CON ON CBH.ContactID = CON.ContactID
						INNER JOIN tblContactCreditCardInfo CCI ON CON.ContactID = CCI.CustomerID
						INNER JOIN tblTransactions TRX ON CBH.ContactID = TRX.CustomerID 
													   AND CONVERT(TRX.TransactionDate, DATE) = dteBillingDate
													   AND TRX.CashierName = 'Credit Biller-LPC'
													   AND TRX.Datasource = CONCAT('CreditBillerLPC:', lngCreditBillID)
						LEFT OUTER JOIN (
							SELECT prd.ProductID, prd.ProductCode, pkg.BarCode1, prd.ProductDesc,
								   pkg.UnitID, pkg.PackageID
							FROM tblProducts prd 
							INNER JOIN tblProductPackage pkg ON prd.productID = pkg.ProductID  AND pkg.Quantity = 1
							WHERE ProductCode = 'CCI LATE PAYMENT CHARGE'
						) prd ON 1=1
						WHERE CBH.CreditBillID = lngCreditBillID 
							AND (Prev2MoCurrentDueAmount + Prev1MoCurrentDueAmount) > 0 AND CurrMonthAmountPaid = 0;
		IF (boDebug = 1) THEN
			SELECT 'Transaction Items saved...';
			SELECT * FROM tblTransactionItems WHERE ProductCode = 'CCI LATE PAYMENT CHARGE' AND DataSource = CONCAT('CreditBillerLPC:', lngCreditBillID);
		END IF;

		-- insert the payment as creditpayment 
		INSERT INTO tblCreditPayment(TransactionID, ContactID, CreditCardTypeID, 
									CreditBefore, Amount, CreditAfter,  
									CreditReason, CreditDate, 
									TerminalNo, BranchID, CashierName, TransactionNo)
						SELECT TRX.TransactionID ,CBH.ContactID , CCI.CreditCardTypeID
									,CON.Credit
									,(Prev2MoCurrentDueAmount + Prev1MoCurrentDueAmount) * decCreditLatePenaltyCharge
									,CON.Credit + ((Prev2MoCurrentDueAmount + Prev1MoCurrentDueAmount) * decCreditLatePenaltyCharge)
									,CONCAT('Late Payment Charge  : (', Prev2MoCurrentDueAmount + Prev1MoCurrentDueAmount,' * ',decCreditLatePenaltyCharge,')')
									,dteBillingDate
									,'00', 1, 'Credit Biller-LPC', TRX.TransactionNo
						FROM tblCreditBillHeader CBH
						INNER JOIN tblContacts CON ON CBH.ContactID = CON.ContactID
						INNER JOIN tblContactCreditCardInfo CCI ON CON.ContactID = CCI.CustomerID
						INNER JOIN tblTransactions TRX ON CBH.ContactID = TRX.CustomerID 
													   AND CONVERT(TRX.TransactionDate, DATE) = dteBillingDate
													   AND TRX.CashierName = 'Credit Biller-LPC'
													   AND TRX.Datasource = CONCAT('CreditBillerLPC:', lngCreditBillID)
						WHERE CBH.CreditBillID = lngCreditBillID AND (Prev2MoCurrentDueAmount + Prev1MoCurrentDueAmount) > 0 AND CurrMonthAmountPaid = 0;
		/***********END - INSERT THE CCI LATE PAYMENT CHARGE**************/

		/****Update the tblCreditBillHeader to update the current credit of contact to get the before and after during saving in creditpayment*******/
		-- update the TotalBillCharges, MinimumAmountDue, CurrentDueAmount
		UPDATE tblCreditBillHeader AS CBH 
		LEFT OUTER JOIN 
		(  
			SELECT CreditBillHeaderID ,SUM(Amount) TotalBillCharges
			FROM tblCreditBillDetail  WHERE TransactionTypeID = 3 OR TransactionTypeID = 4
			GROUP BY CreditBillHeaderID
		)	AS CBDtbc ON CBH.CreditBillHeaderID = CBDtbc.CreditBillHeaderID	-- <-update the TotalBillCharges
		SET  CBH.TotalBillCharges = IFNULL(CBDtbc.TotalBillCharges,0)
			,CBH.MinimumAmountDue = IF((CBH.Prev2MoCurrentDueAmount + CBH.Prev1MoCurrentDueAmount + CBH.CurrMonthAmountPaid + IFNULL(CBDtbc.TotalBillCharges,0) + CBH.CurrMonthCreditAmt) * decCreditMinimumPercentageDue < decCreditMinimumAmountDue, (CBH.Prev2MoCurrentDueAmount + CBH.Prev1MoCurrentDueAmount + CBH.CurrMonthAmountPaid + IFNULL(CBDtbc.TotalBillCharges,0) + CBH.CurrMonthCreditAmt) ,(CBH.Prev2MoCurrentDueAmount + CBH.Prev1MoCurrentDueAmount + CBH.CurrMonthAmountPaid + IFNULL(CBDtbc.TotalBillCharges,0) + CBH.CurrMonthCreditAmt) * decCreditMinimumPercentageDue)
			,CBH.CurrentDueAmount = CBH.Prev2MoCurrentDueAmount + CBH.Prev1MoCurrentDueAmount + CBH.CurrMonthAmountPaid + IFNULL(CBDtbc.TotalBillCharges,0) + CBH.CurrMonthCreditAmt
		WHERE CBH.CreditBillID = lngCreditBillID;

		-- update the credit of contact
		UPDATE tblContacts AS CON
		INNER JOIN
		(
			SELECT ContactID ,CurrentDueAmount FROM tblCreditBillHeader WHERE CreditBillID = lngCreditBillID
		) CBH ON CBH.ContactID = CON.ContactID
		SET CON.Credit = CurrentDueAmount;
		/****end-Update the tblCreditBillHeader to update the current credit of contact to get the before and after during saving in creditpayment*******/

		/***********INSERT THE CCI FINANCE CHARGE**************/
		-- update the TransactionRefID to be use for saving to tblTransactions
		INSERT INTO tblTransactions (	 TransactionNo, BranchID, BranchCode, CustomerID, CustomerName
										,AgentID, AgentName, CreatedByID, CreatedByName, CashierID, CashierName
										,TerminalNo, TransactionDate, TransactionStatus
										,WaiterID, WaiterName ,AgentPositionName, AgentDepartmentName
										,SubTotal ,AmountPaid ,CreditPayment ,PaymentType ,DateClosed, DataSource)
		SELECT LPAD(TransactionNo + rownum,14,'0'), 1, 'Main', CBH.ContactID ,CBH.ContactName 
						,1 ,'RetailPlus Agent' ,1 ,'Credit Bill' ,1 ,'Credit Biller-FC'
						,'00' ,dteBillingDate ,1
						,2 ,'System Credit Bill' ,'Credit Bill Position' ,'Credit Bill Dept.'
						,CBH.FinanceCharge ,CBH.FinanceCharge ,CBH.FinanceCharge ,5 ,dteBillingDate, CONCAT('CreditBillerFC:', lngCreditBillID)
		FROM (
			SELECT @rownum:=@rownum+1 AS rownum, CBH.*
			FROM (
				SELECT CBH.ContactID ,CON.ContactName ,((Prev2MoCurrentDueAmount + Prev1MoCurrentDueAmount) * decCreditFinanceCharge) FinanceCharge
				FROM tblCreditBillHeader CBH
				INNER JOIN tblContacts CON ON CBH.ContactID = CON.ContactID
				WHERE CBH.CreditBillID = lngCreditBillID AND (Prev2MoCurrentDueAmount + Prev1MoCurrentDueAmount) > 0
		) CBH, (SELECT @rownum:=0) r) CBH, (SELECT MAX(TransactionNo) TransactionNo  FROM tblTransactions) r;

		-- insert the finance Charges FOR bills that are not paid for 30days and above.
		INSERT INTO tblCreditBillDetail(CreditBillHeaderID ,TransactionDate ,Description ,Amount ,TransactionTypeID ,TransactionRefID, TerminalNoRefID, BranchIDRefID)
		SELECT CreditBillHeaderID ,dteBillingDate 
				,CONCAT('Finance Charge 30days: (', Prev2MoCurrentDueAmount + Prev1MoCurrentDueAmount,' * ',decCreditFinanceCharge,')') 'Description'
				,((Prev2MoCurrentDueAmount + Prev1MoCurrentDueAmount) * decCreditFinanceCharge) CurrMonthCreditAmt 
				,4 tblCreditPaymentTransactionTypeID ,TRX.TransactionID TransactionRefID, Trx.TerminalNo, Trx.BranchID
		FROM tblCreditBillHeader CBH
		INNER JOIN tblTransactions TRX ON CBH.ContactID = TRX.CustomerID 
													   AND CONVERT(TRX.TransactionDate, DATE) = dteBillingDate
													   AND TRX.CashierName = 'Credit Biller-FC'
													   AND TRX.Datasource = CONCAT('CreditBillerFC:', lngCreditBillID)
		WHERE CBH.CreditBillID = lngCreditBillID AND (Prev2MoCurrentDueAmount + Prev1MoCurrentDueAmount) > 0;

		-- insert the transactionitems
		INSERT INTO tblTransactionItems(TransactionID, ProductID, ProductCode, BarCode, Description, 
                                ProductUnitID, ProductUnitCode, Quantity, Price, SellingPrice, 
                                Discount, ItemDiscount, ItemDiscountType, Amount, VAT, VatableAmount, EVAT, LocalTax, 
                                VariationsMatrixID, MatrixDescription, ProductGroup, ProductSubGroup, TransactionItemStatus,
                                DiscountCode,DiscountRemarks,ProductPackageID,MatrixPackageID, PackageQuantity,PromoQuantity,
                                PromoValue,PromoInPercent,PromoType,PromoApplied,PurchasePrice,PurchaseAmount,
                                IncludeInSubtotalDiscount,OrderSlipPrinter,OrderSlipPrinted,PercentageCommision, Commision, DataSource)
		SELECT TRX.TransactionID , prd.ProductID, prd.ProductCode, prd.BarCode1, prd.ProductDesc
									,prd.UnitID, 'PC', 1, 0, (Prev2MoCurrentDueAmount + Prev1MoCurrentDueAmount) * decCreditFinanceCharge
									,0, 0, 0, (Prev2MoCurrentDueAmount + Prev1MoCurrentDueAmount) * decCreditFinanceCharge AS Amount, 0, 0, 0, 0
									,0, '', 'System Default', 'System Default', 0
									,'', '', prd.PackageID, 0, 1, 0
									,0, 0, 0, 0, 0, 0
									,0, 0, 0, 0, 0, CONCAT('CreditBillerFC:', lngCreditBillID)
						FROM tblCreditBillHeader CBH
						INNER JOIN tblContacts CON ON CBH.ContactID = CON.ContactID
						INNER JOIN tblContactCreditCardInfo CCI ON CON.ContactID = CCI.CustomerID
						INNER JOIN tblTransactions TRX ON CBH.ContactID = TRX.CustomerID 
													   AND CONVERT(TRX.TransactionDate, DATE) = dteBillingDate
													   AND TRX.CashierName = 'Credit Biller-FC'
													   AND TRX.Datasource = CONCAT('CreditBillerFC:', lngCreditBillID)
						LEFT OUTER JOIN (
							SELECT prd.ProductID, prd.ProductCode, pkg.BarCode1, prd.ProductDesc,
								   pkg.UnitID, pkg.PackageID
							FROM tblProducts prd 
							INNER JOIN tblProductPackage pkg ON prd.productID = pkg.ProductID  AND pkg.Quantity = 1
							WHERE ProductCode = 'CCI FINANCE CHARGE'
						) prd ON 1=1
						WHERE CBH.CreditBillID = lngCreditBillID AND (Prev2MoCurrentDueAmount + Prev1MoCurrentDueAmount) > 0 AND CurrMonthAmountPaid = 0;

		IF (boDebug = 1) THEN
			SELECT 'Transaction Items saved...';
			SELECT * FROM tblTransactionItems WHERE ProductCode = 'CCI FINANCE CHARGE' AND DataSource = CONCAT('CreditBillerFC:', lngCreditBillID);
		END IF;

		INSERT INTO tblCreditPayment(TransactionID, ContactID, CreditCardTypeID, 
									CreditBefore, Amount, CreditAfter, 
									CreditReason, CreditDate, 
									TerminalNo, BranchID, CashierName, TransactionNo)
						SELECT TransactionID ,CBH.ContactID , CCI.CreditCardTypeID
									,CON.Credit 
									,(Prev2MoCurrentDueAmount + Prev1MoCurrentDueAmount) * decCreditFinanceCharge
									,CON.Credit + ((Prev2MoCurrentDueAmount + Prev1MoCurrentDueAmount) * decCreditFinanceCharge)
									,CONCAT('Finance Charge 30days: (', Prev2MoCurrentDueAmount + Prev1MoCurrentDueAmount,' * ',decCreditFinanceCharge,')')
									,dteBillingDate
									,'00', 1, 'Credit Biller-FC', TransactionNo 
						FROM tblCreditBillHeader CBH
						INNER JOIN tblContacts CON ON CBH.ContactID = CON.ContactID
						INNER JOIN tblContactCreditCardInfo CCI ON CON.ContactID = CCI.CustomerID
						INNER JOIN tblTransactions TRX ON CBH.ContactID = TRX.CustomerID 
													   AND CONVERT(TRX.TransactionDate, DATE) = dteBillingDate
													   AND TRX.CashierName = 'Credit Biller-FC'
													   AND TRX.Datasource = CONCAT('CreditBillerFC:', lngCreditBillID)
						WHERE CBH.CreditBillID = lngCreditBillID AND (Prev2MoCurrentDueAmount + Prev1MoCurrentDueAmount) > 0;
		/***********END - INSERT THE CCI FINANCE CHARGE**************/

		UPDATE tblCreditPayment SET SyncID = CreditPaymentID WHERE SyncID = 0;

		-- insert the 60Days Charges
		/****Update the tblCreditBillHeader to update the current credit of contact to get the before and after during saving in creditpayment*******/
		-- update the TotalBillCharges, MinimumAmountDue, CurrentDueAmount
		UPDATE tblCreditBillHeader AS CBH 
		LEFT OUTER JOIN 
		(  
			SELECT CreditBillHeaderID ,SUM(Amount) TotalBillCharges
			FROM tblCreditBillDetail  WHERE TransactionTypeID = 3 OR TransactionTypeID = 4
			GROUP BY CreditBillHeaderID
		)	AS CBDtbc ON CBH.CreditBillHeaderID = CBDtbc.CreditBillHeaderID	-- <-update the TotalBillCharges
		SET  CBH.TotalBillCharges = IFNULL(CBDtbc.TotalBillCharges,0)
			,CBH.MinimumAmountDue = IF((CBH.Prev2MoCurrentDueAmount + CBH.Prev1MoCurrentDueAmount + CBH.CurrMonthAmountPaid + IFNULL(CBDtbc.TotalBillCharges,0) + CBH.CurrMonthCreditAmt) * decCreditMinimumPercentageDue < decCreditMinimumAmountDue, (CBH.Prev2MoCurrentDueAmount + CBH.Prev1MoCurrentDueAmount + CBH.CurrMonthAmountPaid + IFNULL(CBDtbc.TotalBillCharges,0) + CBH.CurrMonthCreditAmt) ,(CBH.Prev2MoCurrentDueAmount + CBH.Prev1MoCurrentDueAmount + CBH.CurrMonthAmountPaid + IFNULL(CBDtbc.TotalBillCharges,0) + CBH.CurrMonthCreditAmt) * decCreditMinimumPercentageDue)
			,CBH.CurrentDueAmount = CBH.Prev2MoCurrentDueAmount + CBH.Prev1MoCurrentDueAmount + CBH.CurrMonthAmountPaid + IFNULL(CBDtbc.TotalBillCharges,0) + CBH.CurrMonthCreditAmt
		WHERE CBH.CreditBillID = lngCreditBillID;

		-- update the credit of contact
		UPDATE tblContacts AS CON
		INNER JOIN
		(
			SELECT ContactID ,CurrentDueAmount FROM tblCreditBillHeader WHERE CreditBillID = lngCreditBillID
		) CBH ON CBH.ContactID = CON.ContactID
		SET CON.Credit = CurrentDueAmount;
		/****end-Update the tblCreditBillHeader to update the current credit of contact to get the before and after during saving in creditpayment*******/

		-- update the LastBillingDate of contacts
		UPDATE tblContactCreditCardInfo AS CCI
		INNER JOIN
		(
			SELECT ContactID FROM tblCreditBillHeader WHERE CreditBillID = lngCreditBillID
		) CBH ON CBH.ContactID = CCI.CustomerID
		SET CCI.LastBillingDate = dteBillingDate;

	END IF;

	IF (boDebug = 1) THEN
		SELECT * FROM tblContacts WHERE Credit > 0;
	END IF;

	-- re-sync to include the unposted credit's
	CALL procSyncContactCredit();

END;
GO
delimiter ;




/**************************************************************
	procProductQuantityConvert
	Lemuel E. Aceron
	CALL procProcessCreditBillsClose();
	01-Feb-2013	Create this procedure
				Separate this procedure from existing procProcessCreditBills
	
**************************************************************/
delimiter GO
DROP PROCEDURE IF EXISTS procProcessCreditBillsClose
GO

create procedure procProcessCreditBillsClose(
	 ) 
BEGIN
	
	DECLARE bolCreditUseLastDayCutOffDate TINYINT default 1;	
	DECLARE dteCreditCutOffDate, dteNextCreditCutOffDate, dteBillingDate DATE;
	DECLARE strCardTypeCode VARCHAR(100);

	-- get the card type to process
	SET strCardTypeCode = (SELECT ConfigValue FROM sysCreditConfig WHERE ConfigName = 'IndividualCardTypeCode');

	SELECT CreditCutOffDate, CreditUseLastDayCutOffDate, BillingDate
	INTO   dteCreditCutOffDate, bolCreditUseLastDayCutOffDate, dteBillingDate
	FROM tblCardTypes WHERE CardTypeCode = strCardTypeCode;

	IF dteBillingDate < now() THEN

		SET dteNextCreditCutOffDate = (SELECT DATE_ADD(dteCreditCutOffDate, INTERVAL 1 MONTH));
	
		SET dteCreditCutOffDate = DATE_ADD(dteCreditCutOffDate, INTERVAL 1 MONTH);
		IF bolCreditUseLastDayCutOffDate = 1 OR ISNULL(bolCreditUseLastDayCutOffDate) THEN
			SET dteCreditCutOffDate = (SELECT LAST_DAY(dteCreditCutOffDate));
			SET dteNextCreditCutOffDate = (SELECT LAST_DAY(dteNextCreditCutOffDate));
		END IF;

		/** end-Update the header with the details *****/
		UPDATE tblCardTypes SET
			CreditPurcStartDateToProcess = DATE_ADD(CreditPurcStartDateToProcess, INTERVAL 1 MONTH),
			CreditPurcEndDateToProcess = DATE_ADD(CreditPurcEndDateToProcess, INTERVAL 1 MONTH),
			CreditCutOffDate = dteCreditCutOffDate,
			BillingDate = DATE_ADD(BillingDate, INTERVAL 1 MONTH)
		WHERE CardTypeCode = strCardTypeCode;
	END IF;
END;
GO
delimiter ;

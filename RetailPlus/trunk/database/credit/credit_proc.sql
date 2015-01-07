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

	call procProcessCreditBills(0, 'HP CREDIT CARD');
	
**************************************************************/
delimiter GO
DROP PROCEDURE IF EXISTS procProcessCreditBills
GO

create procedure procProcessCreditBills(
	IN boDebug TINYINT(1),
	IN strCardTypeCode VARCHAR(30)
	 ) 
BEGIN
	DECLARE lngCreditBillID BIGINT DEFAULT 0;
	
	DECLARE dteBillingDate, dteCreditPurcStartDateToProcess, dteCreditPurcEndDateToProcess DATE;
	DECLARE dteCreditCutOffDate, dteStartCreditCutOffDate, dteEndCreditCutOffDate, dteCreditPaymentDueDate DATE;
	DECLARE dtePrev2BillingDate, dtePrevBillingDate, dteNextBillingDate DATE;

	DECLARE bolCreditUseLastDayCutOffDate TINYINT default 1;
	DECLARE decCreditFinanceCharge, decCreditMinimumPercentageDue, decCreditMinimumAmountDue, decCreditLatePenaltyCharge DECIMAL(10,3) DEFAULT 0;
	DECLARE decCreditFinanceCharge15th, decCreditMinimumPercentageDue15th, decCreditMinimumAmountDue15th, decCreditLatePenaltyCharge15th DECIMAL(10,3) DEFAULT 0;

	DECLARE dteCurrDate datetime default now();
	DECLARE lngCreatedByID BIGINT default 1;
	DECLARE strCreatedByName varchar(100) default 'SysCWoutGBiller';
	DECLARE intCardTypeID INT(10) DEFAULT 0;
	DECLARE intCreditCardType, intWithGuarantor TINYINT(1) DEFAULT 0;

	-- set the no of unpaid months where the minimumamountdue will be the currentamountdue
	DECLARE intNoOfMonthsUnPaid INT(1) DEFAULT 2; 
	
	-- get the variable charges
	SELECT CardTypeID, CreditCardType, WithGuarantor, CreditUseLastDayCutOffDate,
			CreditFinanceCharge/100, CreditMinimumPercentageDue/100, CreditMinimumAmountDue, CreditLatePenaltyCharge/100,
			CreditFinanceCharge15th/100, CreditMinimumPercentageDue15th/100, CreditMinimumAmountDue15th, CreditLatePenaltyCharge15th/100,
			CreditPurcStartDateToProcess, CreditPurcEndDateToProcess, CreditCutOffDate, BillingDate
	INTO   intCardTypeID, intCreditCardType, intWithGuarantor, bolCreditUseLastDayCutOffDate,
			decCreditFinanceCharge, decCreditMinimumPercentageDue, decCreditMinimumAmountDue, decCreditLatePenaltyCharge,
			decCreditFinanceCharge15th, decCreditMinimumPercentageDue15th, decCreditMinimumAmountDue15th, decCreditLatePenaltyCharge15th,
			dteCreditPurcStartDateToProcess, dteCreditPurcEndDateToProcess, dteCreditCutOffDate, dteBillingDate
	FROM tblCardTypes WHERE CardTypeCode = strCardTypeCode;

	IF intCardTypeID <> 0 THEN
		IF (boDebug = 1) THEN
			-- source C:\RetailPlus\RetailPlus\database\credit\credit_proc.sql
			UPDATE tblCardTypes SET CreditUseLastDayCutOffDate		= 1 WHERE CreditCardType=1 AND WithGuarantor=0;
			
			UPDATE tblCardTypes SET CreditPurcStartDateToProcess	= '2014-11-10' WHERE CreditCardType=1 AND WithGuarantor=0;
			UPDATE tblCardTypes SET CreditPurcEndDateToProcess		= '2014-12-09' WHERE CreditCardType=1 AND WithGuarantor=0;
			UPDATE tblCardTypes SET CreditCutOffDate				= '2014-11-30' WHERE CreditCardType=1 AND WithGuarantor=0;
			UPDATE tblCardTypes SET BillingDate						= '2014-12-10' WHERE CreditCardType=1 AND WithGuarantor=0;
			UPDATE tblContactCreditCardInfo SET LastBillingDate		= '2014-11-10' WHERE GuarantorID = 0;

			CALL procProcessCreditBills(0, 'HP CREDIT CARD');
			CALL procProcessCreditBillsClose('HP CREDIT CARD');
			
			UPDATE tblCardTypes SET BillingDate						= '2015-01-10' WHERE CreditCardType=1 AND WithGuarantor=0;
			UPDATE tblContactCreditCardInfo SET LastBillingDate		= '2014-12-10' WHERE GuarantorID = 0;

			UPDATE tblcreditbillheader set isbillprinted = 0 where creditbillid = 48;
			DELETE FROM tblCreditBillDetail WHERE CreditBillHeaderID IN (SELECT CreditBillHeaderID FROM tblCreditBillHeader WHERE CreditBillID IN (SELECT CreditBillID FROM tblCreditBills WHERE CreditBillID=48 AND WithGuarantor=0 AND CreatedOn BETWEEN '2014-12-10' AND '2014-12-24'));
			DELETE FROM tblCreditBillHeader WHERE CreditBillID IN (SELECT CreditBillID FROM tblCreditBills WHERE CreditBillID=48 AND WithGuarantor=0 AND CreatedOn BETWEEN '2014-12-10' AND '2014-12-24');
			DELETE FROM tblCreditBills WHERE CreditBillID =48 AND WithGuarantor=0 AND CreatedOn BETWEEN '2014-12-10' AND '2014-12-24';
			DELETE FROM tblCreditPayment WHERE CashierName = 'SysCWoutGBiller-LPC' OR CashierName = 'SysCWoutGBiller-FC' AND CreditDate BETWEEN '2014-12-10' AND '2014-12-24';
			DELETE FROM tblTransactions WHERE Transactiondate BETWEEN '2014-12-10' AND '2014-12-24' AND TransactionID IN (SELECT DISTINCT TransactionID FROM tblTransactionItems WHERE ProductCode = 'CCI LATE PAYMENT CHARGE' OR ProductCode = 'CCI FINANCE CHARGE');
			DELETE FROM tblTransactionItems WHERE ProductCode = 'CCI LATE PAYMENT CHARGE' OR ProductCode = 'CCI FINANCE CHARGE' AND CreatedOn BETWEEN '2014-12-10' AND '2014-12-24';

		END IF;

		-- check the variable charges
		IF (boDebug = 1) THEN
			SELECT intCardTypeID, decCreditFinanceCharge, decCreditMinimumPercentageDue, decCreditMinimumAmountDue, decCreditLatePenaltyCharge,
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
			IF NOT EXISTS(SELECT CreditBillID FROM tblCreditBills WHERE BillingDate = dteBillingDate AND CreditCardTypeID = intCardTypeID) THEN
				INSERT INTO tblCreditBills(  CreditPurcStartDateToProcess ,CreditPurcEndDateToProcess ,BillingDate ,CreditCutOffDate, CreditPaymentDueDate
											,CreditFinanceCharge ,CreditMinimumPercentageDue ,CreditMinimumAmountDue ,CreditLatePenaltyCharge 
											,CreditFinanceCharge15th ,CreditMinimumPercentageDue15th ,CreditMinimumAmountDue15th ,CreditLatePenaltyCharge15th 
											,CreditCardTypeID ,CardTypeCode ,CreditCardType, WithGuarantor ,CreditUseLastDayCutOffDate
											,CreatedOn ,CreatedByID ,CreatedByName)
				SELECT						 dteCreditPurcStartDateToProcess ,dteCreditPurcEndDateToProcess ,dteBillingDate ,dteCreditCutOffDate, dteCreditPaymentDueDate
											,decCreditFinanceCharge ,decCreditMinimumPercentageDue ,decCreditMinimumAmountDue ,decCreditLatePenaltyCharge
											,decCreditFinanceCharge15th ,decCreditMinimumPercentageDue15th ,decCreditMinimumAmountDue15th ,decCreditLatePenaltyCharge15th
											,intCardTypeID ,strCardTypeCode ,intCreditCardType, intWithGuarantor ,bolCreditUseLastDayCutOffDate
											,dteCurrDate , lngCreatedByID, strCreatedByName;
			END IF;

			/** Put the credit paramateres to be process *****/
		
			SELECT CreditBillID 
			INTO  lngCreditBillID 
			FROM tblCreditBills WHERE BillingDate = dteBillingDate AND CreditCardTypeID = intCardTypeID;

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
				AND CUS.ContactID NOT IN (SELECT DISTINCT ContactID FROM tblCreditBillHeader WHERE CreditBillID = lngCreditBillID)
				AND CTY.CardTypeID = intCardTypeID;
			/** end-Insert the Contacts that have credits as header *****/
	
			-- check
			IF (boDebug = 1) THEN
				SELECT * FROM tblCreditBillHeader WHERE RunningCreditAmt > 0 LIMIT 10;
			END IF;

			/** Put the credit details for each contacts *****/
			-- Insert the Credit Details
			INSERT INTO tblCreditBillDetail(CreditBillHeaderID ,TransactionDate ,Description ,Amount ,TransactionTypeID ,TransactionRefID, TerminalNoRefID, BranchIDRefID)
			SELECT CreditBillHeaderID ,CreditDate ,CONCAT('Credit: Trn#:' ,CONVERT(TransactionNo, UNSIGNED INTEGER),' @ Ter#:',TerminalNo) 'Description'
					,(Amount) CurrMonthCreditAmt ,1 tblCreditPaymentTransactionTypeID ,CRP.TransactionID TransactionRefID, CRP.TerminalNo, CRP.BranchID
			FROM tblCreditBillHeader CBH
				INNER JOIN tblCreditPayment CRP on CRP.ContactID = CBH.ContactID
			WHERE CBH.CreditBillID = lngCreditBillID 
				AND CONVERT(CreditDate, DATE) BETWEEN dteCreditPurcStartDateToProcess AND dteCreditPurcEndDateToProcess
				AND CRP.TerminalNo <> '00'	-- do not include the previous late charges
				-- AND CRP.TransactionID NOT IN (SELECT DISTINCT TransactionRefID FROM tblCreditBillDetail 
				--														 WHERE TransactionTypeID = 1
				--															AND CONVERT(CreditDate, DATE) BETWEEN dteCreditPurcStartDateToProcess AND dteCreditPurcEndDateToProcess)
			GROUP BY CreditBillHeaderID ,TransactionNo ,CreditDate;

			-- update the purchase sumamry after rupdating
			UPDATE tblCreditBillHeader AS CBH 
			LEFT OUTER JOIN	
			( 
				SELECT CreditBillHeaderID ,SUM(Amount) CurrMonthCreditAmt 
				FROM tblCreditBillDetail  WHERE TransactionTypeID = 1 -- Purchase tblCreditPaymentTransactionTypeID
				GROUP BY CreditBillHeaderID
			)	AS CBDcmca ON CBH.CreditBillHeaderID = CBDcmca.CreditBillHeaderID	-- <-update the CurrentPurchaseAmt
			SET  CBH.CurrentPurchaseAmt = IFNULL(CBDcmca.CurrMonthCreditAmt,0)
			WHERE CBH.CreditBillID = lngCreditBillID;

			-- Insert the Payment Details
			INSERT INTO tblCreditBillDetail(CreditBillHeaderID ,TransactionDate ,Description ,Amount ,TransactionTypeID ,TransactionRefID, TerminalNoRefID, BranchIDRefID)
			SELECT CreditBillHeaderID, TransactionDate, Description, CurrMonthCreditAmt, tblCreditPaymentTransactionTypeID, TransactionRefID, TerminalNo, BranchID
			FROM
				(
					SELECT CreditBillHeaderID, TransactionDate
							,CONCAT('Payment: Trn#:' ,CONVERT(TransactionNo, UNSIGNED INTEGER),' @ Ter#:',TerminalNo) 'Description'
							,-(Subtotal) CurrMonthCreditAmt ,2 tblCreditPaymentTransactionTypeID ,Trx.TransactionID TransactionRefID, Trx.TerminalNo, Trx.BranchID
					FROM tblTransactions Trx 
						INNER JOIN tblTransactionItems TrxD ON Trx.TransactionID = TrxD.TransactionID
						INNER JOIN tblCreditBillHeader CBH ON CBH.ContactID = Trx.CustomerID AND CBH.CreditBillID = lngCreditBillID
					WHERE TrxD.ProductCode = 'CREDIT PAYMENT'
						AND TransactionStatus = 7 -- creditPaymentStatus
						-- change to be the same as the puchstartdate
						-- AND CONVERT(Trx.TransactionDate, DATE) BETWEEN dteStartCreditCutOffDate AND dteEndCreditCutOffDate
						AND CONVERT(Trx.TransactionDate, DATE) BETWEEN dteCreditPurcStartDateToProcess AND dteCreditPurcEndDateToProcess
						-- AND Trx.TransactionID NOT IN (SELECT DISTINCT TransactionRefID FROM tblCreditBillDetail 
						--														 WHERE TransactionTypeID = 2
						--															AND CONVERT(TransactionDate, DATE) BETWEEN dteCreditPurcStartDateToProcess AND dteCreditPurcEndDateToProcess)
					-- GROUP BY TransactionNo
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
							,1 ,'RetailPlus Agent' ,1 ,'Credit Bill' ,1 ,CONCAT(strCreatedByName,'-LPC')
							,'00' ,dteBillingDate ,1
							,2 ,'System Credit Bill' ,'Credit Bill Position' ,'Credit Bill Dept.'
							,CBH.LatePaymentChargeAmt ,CBH.LatePaymentChargeAmt ,CBH.LatePaymentChargeAmt ,5 ,dteBillingDate, CONCAT('CreditBillerLPC:', lngCreditBillID)
			FROM (
				SELECT @rownum:=@rownum+1 AS rownum, CBH.*
				FROM (
					SELECT CBH.ContactID ,CON.ContactName ,((Prev2MoCurrentDueAmount + Prev1MoCurrentDueAmount) * decCreditFinanceCharge) LatePaymentChargeAmt
					FROM tblCreditBillHeader CBH 
					INNER JOIN tblContacts CON ON CBH.ContactID = CON.ContactID
					WHERE CBH.CreditBillID = lngCreditBillID AND (Prev2MoCurrentDueAmount + Prev1MoCurrentDueAmount) > 0 AND CurrMonthAmountPaid = 0
			) CBH, (SELECT @rownum:=0) r) CBH, (SELECT MAX(TransactionNo) TransactionNo  FROM tblTransactions) r;

			-- insert the late payment Charges FOR bills that did not pay for the 1 month.
			INSERT INTO tblCreditBillDetail(CreditBillHeaderID ,TransactionDate ,Description ,Amount ,TransactionTypeID ,TransactionRefID, TerminalNoRefID, BranchIDRefID)
			SELECT CreditBillHeaderID ,dteBillingDate 
					,CONCAT('Late Payment Charge  : (', FORMAT(Prev2MoCurrentDueAmount + Prev1MoCurrentDueAmount,2),' * ',decCreditFinanceCharge,')') 'Description'
					,((Prev2MoCurrentDueAmount + Prev1MoCurrentDueAmount) * decCreditFinanceCharge) CurrMonthCreditAmt 
					,3 tblCreditPaymentTransactionTypeID ,TRX.TransactionID TransactionRefID, Trx.TerminalNo, Trx.BranchID
			FROM tblCreditBillHeader CBH
			INNER JOIN tblTransactions TRX ON CBH.ContactID = TRX.CustomerID 
														   AND CONVERT(TRX.TransactionDate, DATE) = dteBillingDate
														   AND TRX.CashierName = CONCAT(strCreatedByName,'-LPC')
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
										,prd.UnitID, 'PC', 1, (Prev2MoCurrentDueAmount + Prev1MoCurrentDueAmount) * decCreditFinanceCharge AS Price, (Prev2MoCurrentDueAmount + Prev1MoCurrentDueAmount) * decCreditFinanceCharge AS SellingPrice
										,0, 0, 0, (Prev2MoCurrentDueAmount + Prev1MoCurrentDueAmount) * decCreditFinanceCharge AS Amount, 0, 0, 0, 0
										,0, '', 'System Default', 'System Default', 0
										,'', '', prd.PackageID, 0, 1, 0
										,0, 0, 0, 0, 0, 0
										,0, 0, 0, 0, 0, CONCAT('CreditBillerLPC:', lngCreditBillID)
							FROM tblCreditBillHeader CBH
							INNER JOIN tblContacts CON ON CBH.ContactID = CON.ContactID
							INNER JOIN tblContactCreditCardInfo CCI ON CON.ContactID = CCI.CustomerID
							INNER JOIN tblTransactions TRX ON CBH.ContactID = TRX.CustomerID 
														   AND CONVERT(TRX.TransactionDate, DATE) = dteBillingDate
														   AND TRX.CashierName = CONCAT(strCreatedByName,'-LPC')
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
										CreditReasonID, 
										CreditReason, CreditDate, 
										TerminalNo, BranchID, CashierName, TransactionNo)
							SELECT TRX.TransactionID ,CBH.ContactID , CCI.CreditCardTypeID
										,CON.Credit
										,(Prev2MoCurrentDueAmount + Prev1MoCurrentDueAmount) * decCreditFinanceCharge
										,CON.Credit + ((Prev2MoCurrentDueAmount + Prev1MoCurrentDueAmount) * decCreditFinanceCharge)
										,1 CreditReasonID
										,CONCAT('Late Payment Charge  : (', FORMAT(Prev2MoCurrentDueAmount + Prev1MoCurrentDueAmount,2),' * ',decCreditFinanceCharge,')') AS CreditReason
										,dteBillingDate
										,'00', 1, CONCAT(strCreatedByName,'-LPC'), TRX.TransactionNo
							FROM tblCreditBillHeader CBH
							INNER JOIN tblContacts CON ON CBH.ContactID = CON.ContactID
							INNER JOIN tblContactCreditCardInfo CCI ON CON.ContactID = CCI.CustomerID
							INNER JOIN tblTransactions TRX ON CBH.ContactID = TRX.CustomerID 
														   AND CONVERT(TRX.TransactionDate, DATE) = dteBillingDate
														   AND TRX.CashierName = CONCAT(strCreatedByName,'-LPC')
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
				-- ,CBH.MinimumAmountDue = IF((CBH.Prev2MoCurrentDueAmount + CBH.Prev1MoCurrentDueAmount + CBH.CurrMonthAmountPaid + IFNULL(CBDtbc.TotalBillCharges,0) + CBH.CurrMonthCreditAmt) * decCreditMinimumPercentageDue < decCreditMinimumAmountDue), decCreditMinimumAmountDue ,(CBH.Prev2MoCurrentDueAmount + CBH.Prev1MoCurrentDueAmount + CBH.CurrMonthAmountPaid + IFNULL(CBDtbc.TotalBillCharges,0) + CBH.CurrMonthCreditAmt) * decCreditMinimumPercentageDue)
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
							,1 ,'RetailPlus Agent' ,1 ,'Credit Bill' ,1 ,CONCAT(strCreatedByName,'-FC')
							,'00' ,dteBillingDate ,1
							,2 ,'System Credit Bill' ,'Credit Bill Position' ,'Credit Bill Dept.'
							,CBH.FinanceCharge ,CBH.FinanceCharge ,CBH.FinanceCharge ,5 ,dteBillingDate, CONCAT('CreditBillerFC:', lngCreditBillID)
			FROM (
				SELECT @rownum:=@rownum+1 AS rownum, CBH.*
				FROM (
					SELECT CBH.ContactID ,CON.ContactName 
						,CASE WHEN (Prev2MoCurrentDueAmount + Prev1MoCurrentDueAmount + CurrMonthAmountPaid) > Prev1MoMinimumAmountDue THEN
								Prev1MoMinimumAmountDue * decCreditLatePenaltyCharge
							  ELSE (Prev2MoCurrentDueAmount + Prev1MoCurrentDueAmount + CurrMonthAmountPaid) * decCreditLatePenaltyCharge 
						 END AS FinanceCharge
					FROM tblCreditBillHeader CBH
					INNER JOIN tblContacts CON ON CBH.ContactID = CON.ContactID
					WHERE CBH.CreditBillID = lngCreditBillID AND (Prev2MoCurrentDueAmount + Prev1MoCurrentDueAmount + CurrMonthAmountPaid) > 0
			) CBH, (SELECT @rownum:=0) r) CBH, (SELECT MAX(TransactionNo) TransactionNo  FROM tblTransactions) r;

			-- insert the finance Charges FOR bills that are not paid for 30days and above.
			INSERT INTO tblCreditBillDetail(CreditBillHeaderID ,TransactionDate ,Description ,Amount ,TransactionTypeID ,TransactionRefID, TerminalNoRefID, BranchIDRefID)
			SELECT CreditBillHeaderID ,dteBillingDate 
					,CASE WHEN (Prev2MoCurrentDueAmount + Prev1MoCurrentDueAmount + CurrMonthAmountPaid) > Prev1MoMinimumAmountDue THEN
							CONCAT('Finance Charge 30days: (', FORMAT(Prev1MoMinimumAmountDue,2),' * ',decCreditLatePenaltyCharge,')') 
						  ELSE 
							CONCAT('Finance Charge 30days: (', FORMAT((Prev2MoCurrentDueAmount + Prev1MoCurrentDueAmount + CurrMonthAmountPaid),2),' * ',decCreditLatePenaltyCharge,')') 
					 END 'Description'
					,CASE WHEN (Prev2MoCurrentDueAmount + Prev1MoCurrentDueAmount + CurrMonthAmountPaid) > Prev1MoMinimumAmountDue THEN
							Prev1MoMinimumAmountDue * decCreditLatePenaltyCharge
						  ELSE (Prev2MoCurrentDueAmount + Prev1MoCurrentDueAmount + CurrMonthAmountPaid) * decCreditLatePenaltyCharge 
					 END AS CurrMonthCreditAmt 
					,4 tblCreditPaymentTransactionTypeID ,TRX.TransactionID TransactionRefID, Trx.TerminalNo, Trx.BranchID
			FROM tblCreditBillHeader CBH
			INNER JOIN tblTransactions TRX ON CBH.ContactID = TRX.CustomerID 
														   AND CONVERT(TRX.TransactionDate, DATE) = dteBillingDate
														   AND TRX.CashierName = CONCAT(strCreatedByName,'-FC')
														   AND TRX.Datasource = CONCAT('CreditBillerFC:', lngCreditBillID)
			WHERE CBH.CreditBillID = lngCreditBillID AND ROUND(Prev2MoCurrentDueAmount + Prev1MoCurrentDueAmount + CurrMonthAmountPaid,0) > 0;

			-- insert the transactionitems
			INSERT INTO tblTransactionItems(TransactionID, ProductID, ProductCode, BarCode, Description, 
									ProductUnitID, ProductUnitCode, Quantity, Price, SellingPrice, 
									Discount, ItemDiscount, ItemDiscountType, Amount, VAT, VatableAmount, EVAT, LocalTax, 
									VariationsMatrixID, MatrixDescription, ProductGroup, ProductSubGroup, TransactionItemStatus,
									DiscountCode,DiscountRemarks,ProductPackageID,MatrixPackageID, PackageQuantity,PromoQuantity,
									PromoValue,PromoInPercent,PromoType,PromoApplied,PurchasePrice,PurchaseAmount,
									IncludeInSubtotalDiscount,OrderSlipPrinter,OrderSlipPrinted,PercentageCommision, Commision, DataSource)
			SELECT TRX.TransactionID , prd.ProductID, prd.ProductCode, prd.BarCode1, prd.ProductDesc
										,prd.UnitID, 'PC', 1
										,CASE WHEN (Prev2MoCurrentDueAmount + Prev1MoCurrentDueAmount + CurrMonthAmountPaid) > Prev1MoMinimumAmountDue THEN
												Prev1MoMinimumAmountDue * decCreditLatePenaltyCharge
											  ELSE (Prev2MoCurrentDueAmount + Prev1MoCurrentDueAmount + CurrMonthAmountPaid) * decCreditLatePenaltyCharge 
										 END AS Price
										,CASE WHEN (Prev2MoCurrentDueAmount + Prev1MoCurrentDueAmount + CurrMonthAmountPaid) > Prev1MoMinimumAmountDue THEN
												Prev1MoMinimumAmountDue * decCreditLatePenaltyCharge
											  ELSE (Prev2MoCurrentDueAmount + Prev1MoCurrentDueAmount + CurrMonthAmountPaid) * decCreditLatePenaltyCharge 
										 END AS SellingPrice
										,0, 0, 0
										,CASE WHEN (Prev2MoCurrentDueAmount + Prev1MoCurrentDueAmount + CurrMonthAmountPaid) > Prev1MoMinimumAmountDue THEN
												Prev1MoMinimumAmountDue * decCreditLatePenaltyCharge
											  ELSE (Prev2MoCurrentDueAmount + Prev1MoCurrentDueAmount + CurrMonthAmountPaid) * decCreditLatePenaltyCharge 
										 END AS Amount, 0, 0, 0, 0
										,0, '', 'System Default', 'System Default', 0
										,'', '', prd.PackageID, 0, 1, 0
										,0, 0, 0, 0, 0, 0
										,0, 0, 0, 0, 0, CONCAT('CreditBillerFC:', lngCreditBillID)
							FROM tblCreditBillHeader CBH
							INNER JOIN tblContacts CON ON CBH.ContactID = CON.ContactID
							INNER JOIN tblContactCreditCardInfo CCI ON CON.ContactID = CCI.CustomerID
							INNER JOIN tblTransactions TRX ON CBH.ContactID = TRX.CustomerID 
														   AND CONVERT(TRX.TransactionDate, DATE) = dteBillingDate
														   AND TRX.CashierName = CONCAT(strCreatedByName,'-FC')
														   AND TRX.Datasource = CONCAT('CreditBillerFC:', lngCreditBillID)
							LEFT OUTER JOIN (
								SELECT prd.ProductID, prd.ProductCode, pkg.BarCode1, prd.ProductDesc,
									   pkg.UnitID, pkg.PackageID
								FROM tblProducts prd 
								INNER JOIN tblProductPackage pkg ON prd.productID = pkg.ProductID  AND pkg.Quantity = 1
								WHERE ProductCode = 'CCI FINANCE CHARGE'
							) prd ON 1=1
							WHERE CBH.CreditBillID = lngCreditBillID AND (Prev2MoCurrentDueAmount + Prev1MoCurrentDueAmount + CurrMonthAmountPaid) > 0;

			IF (boDebug = 1) THEN
				SELECT 'Transaction Items saved...';
				SELECT * FROM tblTransactionItems WHERE ProductCode = 'CCI FINANCE CHARGE' AND DataSource = CONCAT('CreditBillerFC:', lngCreditBillID);
			END IF;

			INSERT INTO tblCreditPayment(TransactionID, ContactID, CreditCardTypeID, 
										CreditBefore, Amount, CreditAfter, 
										CreditReasonID, CreditReason, CreditDate, 
										TerminalNo, BranchID, CashierName, TransactionNo)
							SELECT TransactionID ,CBH.ContactID , CCI.CreditCardTypeID
										,CON.Credit 
										,(Prev2MoCurrentDueAmount + Prev1MoCurrentDueAmount) * decCreditLatePenaltyCharge
										,CON.Credit + ((Prev2MoCurrentDueAmount + Prev1MoCurrentDueAmount) * decCreditLatePenaltyCharge)
										,2 CreditReasonID
										,CASE WHEN (Prev2MoCurrentDueAmount + Prev1MoCurrentDueAmount + CurrMonthAmountPaid) > Prev1MoMinimumAmountDue THEN
												CONCAT('Finance Charge 30days: (', FORMAT(Prev1MoMinimumAmountDue,2),' * ',decCreditLatePenaltyCharge,')') 
											  ELSE 
												CONCAT('Finance Charge 30days: (', FORMAT((Prev2MoCurrentDueAmount + Prev1MoCurrentDueAmount + CurrMonthAmountPaid),2),' * ',decCreditLatePenaltyCharge,')')
										 END AS CreditReason
										,dteBillingDate
										,'00', 1, CONCAT(strCreatedByName,'-FC'), TransactionNo 
							FROM tblCreditBillHeader CBH
							INNER JOIN tblContacts CON ON CBH.ContactID = CON.ContactID
							INNER JOIN tblContactCreditCardInfo CCI ON CON.ContactID = CCI.CustomerID
							INNER JOIN tblTransactions TRX ON CBH.ContactID = TRX.CustomerID 
														   AND CONVERT(TRX.TransactionDate, DATE) = dteBillingDate
														   AND TRX.CashierName = CONCAT(strCreatedByName,'-FC')
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
				-- ,CBH.MinimumAmountDue = IF((CBH.Prev2MoCurrentDueAmount + CBH.Prev1MoCurrentDueAmount + CBH.CurrMonthAmountPaid + IFNULL(CBDtbc.TotalBillCharges,0) + CBH.CurrMonthCreditAmt) * decCreditMinimumPercentageDue < decCreditMinimumAmountDue, decCreditMinimumAmountDue ,(CBH.Prev2MoCurrentDueAmount + CBH.Prev1MoCurrentDueAmount + CBH.CurrMonthAmountPaid + IFNULL(CBDtbc.TotalBillCharges,0) + CBH.CurrMonthCreditAmt) * decCreditMinimumPercentageDue)
				,CBH.CurrentDueAmount = CBH.Prev2MoCurrentDueAmount + CBH.Prev1MoCurrentDueAmount + CBH.CurrMonthAmountPaid + IFNULL(CBDtbc.TotalBillCharges,0) + CBH.CurrMonthCreditAmt
			WHERE CBH.CreditBillID = lngCreditBillID;

			-- update the minimum amountdue
			UPDATE tblCreditBillHeader 
			SET  MinimumAmountDue = CASE 
										WHEN CurrentDueAmount = 0 THEN 0
										WHEN CurrentDueAmount < decCreditMinimumAmountDue THEN CurrentDueAmount
										WHEN CurrentDueAmount * decCreditMinimumPercentageDue <= decCreditMinimumAmountDue THEN decCreditMinimumAmountDue
										ELSE CurrentDueAmount * decCreditMinimumPercentageDue
									END
			WHERE CreditBillID = lngCreditBillID;

			-- override for the minimum amount due for those that did not pay for 2months
			-- as per Egay Oct 19, 2014 set the NoOfMonthsUnPaid
			UPDATE tblCreditBillHeader 
			SET MinimumAmountDue = CurrentDueAmount
			WHERE CreditBillID = lngCreditBillID AND ContactID IN (
																	SELECT ContactID 
																	FROM (
																		SELECT ContactID, SUM(CurrMonthAmountPaid) CurrMonthAmountPaid, COUNT(*) NoOfMonthsUnPaid
																		FROM tblCreditBillHeader CBH 
																		WHERE BillingDate < dteBillingdate AND BillingDate >= DATE_ADD(dteBillingdate, INTERVAL -(intNoOfMonthsUnPaid) MONTH)
																		GROUP BY ContactID
																	) CBH WHERE CurrMonthAmountPaid = 0 AND NoOfMonthsUnPaid >= intNoOfMonthsUnPaid
																  );

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
			SET CCI.Last2BillingDate = CCI.LastBillingDate, CCI.LastBillingDate = dteBillingDate;

		END IF;

		IF (boDebug = 1) THEN
			SELECT * FROM tblContacts WHERE Credit > 0 AND ContactID IN (SELECT CustomerID FROM tblContactCreditCardInfo WHERE CreditCradTypeID = intCardTypeID);
		END IF;

		-- re-sync to include the unposted credit's
		CALL procSyncContactCredit();
	END IF;
END;
GO
delimiter ;




/**************************************************************
	procProcessCreditBillsClose
	Lemuel E. Aceron
	CALL procProcessCreditBillsClose('HP CREDIT CARD');
	01-Feb-2013	Create this procedure
				Separate this procedure from existing procProcessCreditBills
	
**************************************************************/
delimiter GO
DROP PROCEDURE IF EXISTS procProcessCreditBillsClose
GO

create procedure procProcessCreditBillsClose(
	IN strCardTypeCode VARCHAR(30)
	 ) 
BEGIN
	DECLARE bolCreditUseLastDayCutOffDate TINYINT default 1;	
	DECLARE dteCreditCutOffDate, dteNextCreditCutOffDate, dteBillingDate DATE;
	DECLARE intCardTypeID INT(10) DEFAULT 0;

	SELECT CardTypeID, CreditCutOffDate, CreditUseLastDayCutOffDate, BillingDate
		INTO   intCardTypeID, dteCreditCutOffDate, bolCreditUseLastDayCutOffDate, dteBillingDate
		FROM tblCardTypes WHERE CardTypeCode = strCardTypeCode;

	IF intCardTypeID <> 0 THEN
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
	END IF;
END;
GO
delimiter ;


/**************************************************************
	procProcessCreditBillsWG
	Lemuel E. Aceron
	CALL procProcessCreditBillsWG(0, 'CN EMP SUPER CARD');
	CALL procProcessCreditBillsWGClose('CN EMP SUPER CARD');
	08-Oct-2014	Create this procedure
	
	Sample:
		PurchaseStart:	2012-12-10
		PurchaseEnd:	2013-01-09
		CreditCutOff:	2012-12-31
		BillingDate:	2012-01-10

	
	
**************************************************************/
delimiter GO
DROP PROCEDURE IF EXISTS procProcessCreditBillsWG
GO

create procedure procProcessCreditBillsWG(
	IN boDebug TINYINT(1),
	IN strCardTypeCode VARCHAR(30)
	 ) 
BEGIN
	DECLARE lngCreditBillID BIGINT DEFAULT 0;
	
	-- dteLastCreditPurcStartDateToProcess: use to get the last 15days purchase date
	DECLARE dteBillingDate, dteCreditPurcStartDateToProcess, dteCreditPurcEndDateToProcess, dteLastCreditPurcStartDateToProcess DATE;
	DECLARE dtePaymentStartDateToProcess, dtePaymentEndDateToProcess DATE;
	DECLARE dteCreditCutOffDate, dteCreditPaymentDueDate DATE;
	DECLARE dtePrevBillingDate, dteNextBillingDate DATE;

	DECLARE bolCreditUseLastDayCutOffDate TINYINT default 1;
	DECLARE decCreditFinanceCharge, decCreditMinimumPercentageDue, decCreditMinimumAmountDue, decCreditLatePenaltyCharge DECIMAL(10,3) DEFAULT 0;
	DECLARE decCreditFinanceCharge15th, decCreditMinimumPercentageDue15th, decCreditMinimumAmountDue15th, decCreditLatePenaltyCharge15th DECIMAL(10,3) DEFAULT 0;
	DECLARE decCreditLatePenaltyChargeToUse DECIMAL(10,3) DEFAULT 0;

	DECLARE dteCurrDate datetime default now();
	DECLARE lngCreatedByID BIGINT default 1;
	DECLARE strCreatedByName varchar(100) default 'SysCWithGBiller';
	DECLARE intCardTypeID INT(10) DEFAULT 0;
	DECLARE intCreditCardType, intWithGuarantor TINYINT(1) DEFAULT 0;

	-- set the no of unpaid months where the minimumamountdue will be the currentamountdue
	DECLARE intNoOfMonthsUnPaid INT(1) DEFAULT 2; 
	
	-- get the card type to process
	-- CreditCardType 1 = Internal 
	-- WithGuarantor  1 = With Guarantor
	
	-- get the variable charges
	SELECT CardTypeID, CreditCardType, WithGuarantor, CreditUseLastDayCutOffDate,
			CreditFinanceCharge/100, CreditMinimumPercentageDue/100, CreditMinimumAmountDue, CreditLatePenaltyCharge/100,
			CreditFinanceCharge15th/100, CreditMinimumPercentageDue15th/100, CreditMinimumAmountDue15th, CreditLatePenaltyCharge15th/100,
			CreditPurcStartDateToProcess, CreditPurcEndDateToProcess, CreditCutOffDate, BillingDate
	INTO   intCardTypeID, intCreditCardType, intWithGuarantor, bolCreditUseLastDayCutOffDate,
			decCreditFinanceCharge, decCreditMinimumPercentageDue, decCreditMinimumAmountDue, decCreditLatePenaltyCharge,
			decCreditFinanceCharge15th, decCreditMinimumPercentageDue15th, decCreditMinimumAmountDue15th, decCreditLatePenaltyCharge15th,
			dteCreditPurcStartDateToProcess, dteCreditPurcEndDateToProcess, dteCreditCutOffDate, dteBillingDate
	FROM tblCardTypes WHERE CardTypeCode = strCardTypeCode;

	IF intCardTypeID <> 0 THEN
		IF (boDebug = 1) THEN
			-- source C:\RetailPlus\RetailPlus\database\tmp3.sql
			UPDATE tblCardTypes SET CreditUseLastDayCutOffDate		= 1 WHERE CreditCardType=1 AND WithGuarantor=1;

			UPDATE tblContactCreditCardInfo SET LastBillingDate		= '2014-12-20' WHERE GuarantorID <> 0;
			UPDATE tblCardTypes SET CreditPurcStartDateToProcess	= '2014-10-20' WHERE CreditCardType=1 AND WithGuarantor=1;
			UPDATE tblCardTypes SET CreditPurcEndDateToProcess		= '2014-12-19' WHERE CreditCardType=1 AND WithGuarantor=1;
			UPDATE tblCardTypes SET CreditCutOffDate				= '2014-12-31' WHERE CreditCardType=1 AND WithGuarantor=1;
			UPDATE tblCardTypes SET BillingDate						= '2014-12-20' WHERE CreditCardType=1 AND WithGuarantor=1;
			
			DELETE FROM tblCreditBillDetail WHERE CreditBillHeaderID IN (SELECT CreditBillHeaderID FROM tblCreditBillHeader WHERE CreditBillID IN (SELECT CreditBillID FROM tblCreditBills WHERE CreditBillID >=34));
			DELETE FROM tblCreditBillHeader WHERE CreditBillID IN (SELECT CreditBillID FROM tblCreditBills WHERE CreditBillID >=34);
			DELETE FROM tblCreditBills WHERE CreditBillID >=34;
			DELETE FROM tblCreditPayment WHERE CashierName = 'SysCWithGBiller-LPC' OR CashierName = 'SysCWithGBiller-FC' AND CreditDate >= '2014-12-20';
			DELETE FROM tblTransactionItems WHERE ProductCode = 'GCI LATE PAYMENT CHARGE' OR ProductCode = 'GCI FINANCE CHARGE' AND TransactionID IN (SELECT TransactionID FROM tblTransactions WHERE TerminalNo = '00' AND CreatedByName = 'Credit Bill' AND TransactionDate >= '2014-12-20');
			DELETE FROM tblTransactions WHERE TerminalNo = '00' AND CreatedByName = 'Credit Bill' AND TransactionDate >= '2014-12-20';

			-- UPDATE tblContactCreditCardInfo SET LastBillingDate = ''
			CALL procSyncContactCredit();

			-- DELETE FROM tblTransactions WHERE (TerminalNo = '00' AND BranchID = 1) or TransactionStatus = 7;
			-- DELETE FROM tblTransactionItems WHERE ProductCode = 'CREDIT PAYMENT' OR ProductCode = 'GCI LATE PAYMENT CHARGE' OR ProductCode = 'GCI FINANCE CHARGE';
			-- UPDATE tblCreditPayment  SET AmountPaid = 0;
		
			-- do not put this here it will throw a recursive
			-- CALL procProcessCreditBillsWG(1);
		END IF;
		
		-- check the variable charges
		IF (boDebug = 1) THEN
			SELECT intCardTypeID, decCreditFinanceCharge, decCreditMinimumPercentageDue, decCreditMinimumAmountDue, decCreditLatePenaltyCharge,
				   dteCreditPurcStartDateToProcess, dteCreditPurcEndDateToProcess, dteCreditCutOffDate, dteBillingDate, bolCreditUseLastDayCutOffDate;
		END IF;
		
		-- IF dteBillingDate < DATE_ADD(now(), INTERVAL 1 MONTH) THEN
		IF dteBillingDate < now() THEN
			SET dtePrevBillingDate = DATE_ADD(dteBillingDate, INTERVAL -13 DAY);
			IF (DAY(dtePrevBillingDate) <= 15) THEN
				SET dtePrevBillingDate = (SELECT CONCAT(YEAR(dtePrevBillingDate), '-', MONTH(dtePrevBillingDate), '-', '06'));
			ELSE
				SET dtePrevBillingDate = (SELECT CONCAT(YEAR(dtePrevBillingDate), '-', MONTH(dtePrevBillingDate), '-', '20'));
			END IF;

			SET dteNextBillingDate = DATE_ADD(dteBillingDate, INTERVAL 13 DAY);
			IF (DAY(dteNextBillingDate) <= 15) THEN
				SET dteNextBillingDate = (SELECT CONCAT(YEAR(dteNextBillingDate), '-', MONTH(dteNextBillingDate), '-', '06'));
			ELSE
				SET dteNextBillingDate = (SELECT CONCAT(YEAR(dteNextBillingDate), '-', MONTH(dteNextBillingDate), '-', '20'));
			END IF;

			SET dteCreditPaymentDueDate = dteCreditCutOffDate;

			IF (DAY(dteCreditPurcEndDateToProcess) < 19) THEN
				IF (MONTH(dteCreditPurcEndDateToProcess) = 1) THEN
					SET dtePaymentStartDateToProcess = (SELECT CONCAT(YEAR(dteCreditPurcEndDateToProcess)-1, '-', '12', '-', '20'));
				ELSE
					SET dtePaymentStartDateToProcess = (SELECT CONCAT(YEAR(dteCreditPurcEndDateToProcess), '-', MONTH(dteCreditPurcEndDateToProcess)-1, '-', '20'));
				END IF;
			ELSE
				SET dtePaymentStartDateToProcess = (SELECT CONCAT(YEAR(dteCreditPurcEndDateToProcess), '-', MONTH(dteCreditPurcEndDateToProcess), '-', '06'));
			END IF;
			SET dtePaymentEndDateToProcess = dteCreditPurcEndDateToProcess;

			-- check 
			IF (boDebug = 1) THEN
				SELECT dtePrevBillingDate, dteBillingDate, dteNextBillingDate,
					   dteCreditCutOffDate, dteCreditPaymentDueDate, dtePaymentStartDateToProcess, dtePaymentEndDateToProcess ;
			END IF;

			-- put the creditbill for this rundate / billingdate
			IF NOT EXISTS(SELECT CreditBillID FROM tblCreditBills WHERE BillingDate = dteBillingDate AND CreditCardTypeID = intCardTypeID) THEN
				INSERT INTO tblCreditBills(  CreditPurcStartDateToProcess ,CreditPurcEndDateToProcess ,BillingDate ,CreditCutOffDate, CreditPaymentDueDate
											,CreditFinanceCharge ,CreditMinimumPercentageDue ,CreditMinimumAmountDue ,CreditLatePenaltyCharge 
											,CreditFinanceCharge15th ,CreditMinimumPercentageDue15th ,CreditMinimumAmountDue15th ,CreditLatePenaltyCharge15th 
											,CreditCardTypeID ,CardTypeCode ,CreditCardType, WithGuarantor ,CreditUseLastDayCutOffDate
											,CreatedOn ,CreatedByID ,CreatedByName)
				SELECT						 dteCreditPurcStartDateToProcess ,dteCreditPurcEndDateToProcess ,dteBillingDate ,dteCreditCutOffDate, dteCreditPaymentDueDate
											,decCreditFinanceCharge ,decCreditMinimumPercentageDue ,decCreditMinimumAmountDue ,decCreditLatePenaltyCharge
											,decCreditFinanceCharge15th ,decCreditMinimumPercentageDue15th ,decCreditMinimumAmountDue15th ,decCreditLatePenaltyCharge15th
											,intCardTypeID ,strCardTypeCode ,intCreditCardType, intWithGuarantor ,bolCreditUseLastDayCutOffDate
											,dteCurrDate , lngCreatedByID, strCreatedByName;

			END IF;

			/** Put the credit paramateres to be process *****/
		
			SELECT CreditBillID 
			INTO  lngCreditBillID 
			FROM tblCreditBills WHERE BillingDate = dteBillingDate AND CreditCardTypeID = intCardTypeID;

			/** end-Put the credit paramateres to be process *****/

			-- check
			IF (boDebug = 1) THEN
				SELECT lngCreditBillID, a.* FROM tblCreditBills a WHERE BillingDate = dteBillingDate;
			END IF;

			/** Insert the Contacts that have credits as header *****/
			INSERT INTO tblCreditBillHeader(CreditBillID ,GuarantorID ,ContactID ,CreditLimit ,RunningCreditAmt
											,BillingDate ,CreatedOn ,CreatedByID ,CreatedByName)
			SELECT lngCreditBillID, CCI.GuarantorID ,CUS.ContactID ,CUS.CreditLimit ,0 RunningCreditAmt -- RunningCreditAmt is set to zero, update this after processing
											,dteBillingDate ,dteCurrDate ,lngCreatedByID ,strCreatedByName
			FROM tblContacts CUS
				INNER JOIN tblContactCreditCardInfo CCI ON CUS.ContactID = CCI.CustomerID
				-- INNER JOIN tblCardTypes CTY ON CCI.CreditCardTypeID = CTY.CardTypeID 
			WHERE CCI.LastBillingDate <= dteBillingDate
				AND CUS.ContactID NOT IN (SELECT DISTINCT ContactID FROM tblCreditBillHeader WHERE CreditBillID = lngCreditBillID)
				AND CCI.CreditCardTypeID = intCardTypeID;
			/** end-Insert the Contacts that have credits as header *****/
	
			-- check
			IF (boDebug = 1) THEN
				SELECT * FROM tblCreditBillHeader WHERE RunningCreditAmt > 0 LIMIT 10;
			END IF;

			/** Put the credit details for each contacts *****/
			-- Insert the Credit Details
			INSERT INTO tblCreditBillDetail(CreditBillHeaderID ,TransactionDate ,Description ,Amount ,TransactionTypeID ,TransactionRefID, TerminalNoRefID, BranchIDRefID)
			SELECT CreditBillHeaderID ,CreditDate ,CONCAT('Credit: Trn#:' ,CONVERT(TransactionNo, UNSIGNED INTEGER),' @ Ter#:',TerminalNo) 'Description'
					,(Amount) CurrMonthCreditAmt ,1 tblCreditPaymentTransactionTypeID ,CRP.TransactionID TransactionRefID, CRP.TerminalNo, CRP.BranchID
			FROM tblCreditBillHeader CBH
				INNER JOIN tblCreditPayment CRP on CRP.ContactID = CBH.ContactID
			WHERE CBH.CreditBillID = lngCreditBillID 
				AND CONVERT(CreditDate, DATE) BETWEEN dteCreditPurcStartDateToProcess AND dteCreditPurcEndDateToProcess
				AND CRP.TerminalNo <> '00'	-- do not include the previous late charges
				-- AND CRP.TransactionID NOT IN (SELECT TransactionRefID FROM tblCreditBillDetail 
				-- 														 WHERE TransactionTypeID = 1
				-- 															AND CONVERT(CreditDate, DATE) BETWEEN dteCreditPurcStartDateToProcess AND dteCreditPurcEndDateToProcess)
			GROUP BY CreditBillHeaderID ,TransactionNo ,CreditDate;

			-- Insert the Payment Details
			INSERT INTO tblCreditBillDetail(CreditBillHeaderID ,TransactionDate ,Description ,Amount ,TransactionTypeID ,TransactionRefID, TerminalNoRefID, BranchIDRefID)
			SELECT CreditBillHeaderID, TransactionDate, Description, CurrMonthCreditAmt, tblCreditPaymentTransactionTypeID, TransactionRefID, TerminalNo, BranchID
			FROM
				(
					SELECT CreditBillHeaderID, TransactionDate
							,CONCAT('Payment: Trn#:' ,CONVERT(TransactionNo, UNSIGNED INTEGER),' @ Ter#:',TerminalNo) 'Description'
							,-(trx.Subtotal) CurrMonthCreditAmt ,2 tblCreditPaymentTransactionTypeID ,Trx.TransactionID TransactionRefID, Trx.TerminalNo, Trx.BranchID
					FROM tblTransactions Trx 
						INNER JOIN tblTransactionItems TrxD ON Trx.TransactionID = TrxD.TransactionID
						INNER JOIN tblCreditBillHeader CBH ON CBH.ContactID = Trx.CustomerID AND CBH.CreditBillID = lngCreditBillID
					WHERE TrxD.ProductCode = 'CREDIT PAYMENT'
						AND TransactionStatus = 7 -- creditPaymentStatus
						-- change to be the same as the puchstartdate
						AND CONVERT(Trx.TransactionDate, DATE) BETWEEN dtePaymentStartDateToProcess AND dtePaymentEndDateToProcess
					-- GROUP BY TransactionNo
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
				SELECT ContactID ,CurrMonthAmountPaid ,CurrentDueAmount ,MinimumAmountDue ,Prev1MoCurrentDueAmount ,TotalBillCharges ,CurrMonthCreditAmt ,EndingBalance
				FROM tblCreditBillHeader 
				WHERE BillingDate = dtePrevBillingDate
			)	AS CBHP ON CBH.ContactID = CBHP.ContactID							-- <-update the PrevMonthMinAmtDue
			SET  CBH.CurrMonthCreditAmt = ROUND(IFNULL(CBDcmca.CurrMonthCreditAmt,0) / 4,2)  -- <-divide this into 4months coz it's payable in 4months
				-- ,CBH.CurrentPurchaseAmt = IFNULL(CBDcmca.CurrMonthCreditAmt,0)
				,CBH.CurrMonthAmountPaid = IFNULL(CBDcmap.CurrMonthAmountPaid,0)
				,CBH.Prev1MoCurrentDueAmount = IFNULL(CBHP.CurrentDueAmount,0)
				,CBH.Prev1MoMinimumAmountDue = IFNULL(CBHP.MinimumAmountDue,0)
				,CBH.Prev1MoCurrMonthAmountPaid = IFNULL(CBHP.CurrMonthAmountPaid,0)
				,CBH.BeginningBalance = IFNULL(CBHP.EndingBalance,0)
			WHERE CBH.CreditBillID = lngCreditBillID;

			/** Update the last purchase amounts *****/
			SET dteLastCreditPurcStartDateToProcess = dtePaymentStartDateToProcess;

			UPDATE tblCreditBillHeader AS CBH 
			LEFT OUTER JOIN	
			( 
				SELECT CreditBillHeaderID, SUM(Amount) CurrMonthCreditAmt
				FROM tblCreditBillHeader CBH
					INNER JOIN tblCreditPayment CRP on CRP.ContactID = CBH.ContactID
				WHERE CBH.CreditBillID = lngCreditBillID 
					AND CONVERT(CreditDate, DATE) BETWEEN dtePaymentStartDateToProcess AND dtePaymentEndDateToProcess
					AND CRP.TerminalNo <> '00'	-- do not include the previous late charges
					-- AND CRP.TransactionID NOT IN (SELECT TransactionRefID FROM tblCreditBillDetail 
					-- 														 WHERE TransactionTypeID = 1
					-- 															AND CONVERT(CreditDate, DATE) BETWEEN dteCreditPurcStartDateToProcess AND dteCreditPurcEndDateToProcess)
				GROUP BY CreditBillHeaderID
			) AS CBDcmca ON CBH.CreditBillHeaderID = CBDcmca.CreditBillHeaderID
			SET  CBH.CurrentPurchaseAmt = IFNULL(CBDcmca.CurrMonthCreditAmt,0)
			WHERE CBH.CreditBillID = lngCreditBillID;

			CALL procsysAuditInsert(NOW(), strCreatedByName, 'CreditBiller:WG', 'localhost', CONCAT('Update purchase amount of ', lngCreditBillID, ' to ', dteLastCreditPurcStartDateToProcess,'.'));

			-- check
			IF (boDebug = 1) THEN
				SELECT 'done updating the header with the details ' AS Step ;

				SELECT * FROM tblCreditBillHeader WHERE CreditBillHeaderID IN (SELECT CreditBillHeaderID FROM tblCreditBillDetail) LIMIT 10;
			END IF;


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
				,CBH.CurrentDueAmount = CBH.Prev1MoCurrentDueAmount + CBH.CurrMonthAmountPaid + IFNULL(CBDtbc.TotalBillCharges,0) + CBH.CurrMonthCreditAmt
			WHERE CBH.CreditBillID = lngCreditBillID;

			-- update the credit of contact
			UPDATE tblContacts AS CON
			INNER JOIN
			(
				SELECT ContactID ,CurrentDueAmount FROM tblCreditBillHeader WHERE CreditBillID = lngCreditBillID
			) CBH ON CBH.ContactID = CON.ContactID
			SET CON.Credit = CurrentDueAmount;
			/****end-Update the tblCreditBillHeader to update the current credit of contact to get the before and after during saving in creditpayment*******/

			-- set the decCreditLatePenaltyChargeToUse for computation
			IF (DAY(dteCreditPaymentDueDate) = 15) THEN
				SET decCreditLatePenaltyChargeToUse = decCreditLatePenaltyCharge;	-- if Due is on 15th, get the 30th Penalty
			ELSE
				SET decCreditLatePenaltyChargeToUse = decCreditLatePenaltyCharge15th;	-- if Due is on 30th, get the 15th Penalty
			END IF;

			/***********INSERT THE GCI LATE PAYMENT CHARGE**************/
			-- update the TransactionRefID to be use for saving to tblTransactions
			INSERT INTO tblTransactions (	 TransactionNo, BranchID, BranchCode, CustomerID, CustomerName
											,AgentID, AgentName, CreatedByID, CreatedByName, CashierID, CashierName
											,TerminalNo, TransactionDate, TransactionStatus
											,WaiterID, WaiterName ,AgentPositionName, AgentDepartmentName
											,SubTotal ,AmountPaid ,CreditPayment ,PaymentType ,DateClosed, DataSource)
			SELECT LPAD(TransactionNo + rownum,14,'0'), 1, 'Main', CBH.ContactID ,CBH.ContactName 
							,1 ,'RetailPlus Agent' ,1 ,'Credit Bill' ,1 ,CONCAT(strCreatedByName,'-FC')
							,'00' ,dteBillingDate ,1
							,2 ,'System Credit Bill' ,'Credit Bill Position' ,'Credit Bill Dept.'
							,CBH.FinanceCharge ,CBH.FinanceCharge ,CBH.FinanceCharge ,5 ,dteBillingDate, CONCAT('CreditBillerFC:', lngCreditBillID)
			FROM (
				SELECT @rownum:=@rownum+1 AS rownum, CBH.*
				FROM (
					SELECT CBH.ContactID ,CON.ContactName ,((Prev1MoCurrentDueAmount + CurrMonthAmountPaid) * decCreditLatePenaltyChargeToUse) FinanceCharge
					FROM tblCreditBillHeader CBH
					INNER JOIN tblContacts CON ON CBH.ContactID = CON.ContactID
					WHERE CBH.CreditBillID = lngCreditBillID AND (Prev1MoCurrentDueAmount + CurrMonthAmountPaid) > 0
			) CBH, (SELECT @rownum:=0) r) CBH, (SELECT MAX(TransactionNo) TransactionNo  FROM tblTransactions) r;

			-- insert the Late Penalty Charges FOR bills that are not paid for 15days.
			INSERT INTO tblCreditBillDetail(CreditBillHeaderID ,TransactionDate ,Description ,Amount ,TransactionTypeID ,TransactionRefID, TerminalNoRefID, BranchIDRefID)
			SELECT CreditBillHeaderID ,dteBillingDate 
					,CONCAT('Late Penalty Charge 15days: (', FORMAT(Prev1MoCurrentDueAmount,2),' * ',decCreditLatePenaltyChargeToUse,')') 'Description'
					,((Prev1MoCurrentDueAmount + CurrMonthAmountPaid) * decCreditLatePenaltyChargeToUse) CurrMonthCreditAmt 
					,4 tblCreditPaymentTransactionTypeID ,TRX.TransactionID TransactionRefID, Trx.TerminalNo, Trx.BranchID
			FROM tblCreditBillHeader CBH
			INNER JOIN tblTransactions TRX ON CBH.ContactID = TRX.CustomerID 
														   AND CONVERT(TRX.TransactionDate, DATE) = dteBillingDate
														   AND TRX.CashierName = CONCAT(strCreatedByName,'-FC')
														   AND TRX.Datasource = CONCAT('CreditBillerFC:', lngCreditBillID)
			WHERE CBH.CreditBillID = lngCreditBillID AND (Prev1MoCurrentDueAmount + CurrMonthAmountPaid) > 0;

			-- insert the transactionitems
			INSERT INTO tblTransactionItems(TransactionID, ProductID, ProductCode, BarCode, Description, 
									ProductUnitID, ProductUnitCode, Quantity, Price, SellingPrice, 
									Discount, ItemDiscount, ItemDiscountType, Amount, VAT, VatableAmount, EVAT, LocalTax, 
									VariationsMatrixID, MatrixDescription, ProductGroup, ProductSubGroup, TransactionItemStatus,
									DiscountCode,DiscountRemarks,ProductPackageID,MatrixPackageID, PackageQuantity,PromoQuantity,
									PromoValue,PromoInPercent,PromoType,PromoApplied,PurchasePrice,PurchaseAmount,
									IncludeInSubtotalDiscount,OrderSlipPrinter,OrderSlipPrinted,PercentageCommision, Commision, DataSource)
			SELECT TRX.TransactionID , prd.ProductID, prd.ProductCode, prd.BarCode1, prd.ProductDesc
										,prd.UnitID, 'PC', 1, (Prev2MoCurrentDueAmount + Prev1MoCurrentDueAmount) * decCreditLatePenaltyChargeToUse AS Price, (Prev2MoCurrentDueAmount + Prev1MoCurrentDueAmount) * decCreditLatePenaltyChargeToUse AS SellingPrice
										,0, 0, 0, (Prev2MoCurrentDueAmount + Prev1MoCurrentDueAmount) * decCreditLatePenaltyChargeToUse AS Amount, 0, 0, 0, 0
										,0, '', 'System Default', 'System Default', 0
										,'', '', prd.PackageID, 0, 1, 0
										,0, 0, 0, 0, 0, 0
										,0, 0, 0, 0, 0, CONCAT('CreditBillerFC:', lngCreditBillID)
							FROM tblCreditBillHeader CBH
							INNER JOIN tblContacts CON ON CBH.ContactID = CON.ContactID
							INNER JOIN tblContactCreditCardInfo CCI ON CON.ContactID = CCI.CustomerID
							INNER JOIN tblTransactions TRX ON CBH.ContactID = TRX.CustomerID 
														   AND CONVERT(TRX.TransactionDate, DATE) = dteBillingDate
														   AND TRX.CashierName = CONCAT(strCreatedByName,'-FC')
														   AND TRX.Datasource = CONCAT('CreditBillerFC:', lngCreditBillID)
							LEFT OUTER JOIN (
								SELECT prd.ProductID, prd.ProductCode, pkg.BarCode1, prd.ProductDesc,
									   pkg.UnitID, pkg.PackageID
								FROM tblProducts prd 
								INNER JOIN tblProductPackage pkg ON prd.productID = pkg.ProductID  AND pkg.Quantity = 1
								WHERE ProductCode = 'GCI LATE PAYMENT CHARGE'
							) prd ON 1=1
							WHERE CBH.CreditBillID = lngCreditBillID AND (Prev1MoCurrentDueAmount + CurrMonthAmountPaid) > 0;

			IF (boDebug = 1) THEN
				SELECT 'Transaction Items saved...';
				SELECT * FROM tblTransactionItems WHERE ProductCode = 'GCI LATE PAYMENT CHARGE' AND DataSource = CONCAT('CreditBillerFC:', lngCreditBillID);
			END IF;

			INSERT INTO tblCreditPayment(TransactionID, ContactID, CreditCardTypeID, 
										CreditBefore, Amount, CreditAfter, 
										CreditReasonID, CreditReason, CreditDate, 
										TerminalNo, BranchID, CashierName, TransactionNo)
							SELECT TransactionID ,CBH.ContactID , CCI.CreditCardTypeID
										,CON.Credit 
										,(Prev1MoCurrentDueAmount + CurrMonthAmountPaid) * decCreditLatePenaltyChargeToUse
										,CON.Credit + ((Prev1MoCurrentDueAmount + CurrMonthAmountPaid) * decCreditLatePenaltyChargeToUse)
										,5 CreditReasonID, CONCAT('Late Penalty 15days: (', FORMAT(Prev1MoCurrentDueAmount + CurrMonthAmountPaid,2),' * ',decCreditLatePenaltyChargeToUse,')') AS CreditReason
										,dteBillingDate
										,'00', 1, CONCAT(strCreatedByName,'-FC'), TransactionNo 
							FROM tblCreditBillHeader CBH
							INNER JOIN tblContacts CON ON CBH.ContactID = CON.ContactID
							INNER JOIN tblContactCreditCardInfo CCI ON CON.ContactID = CCI.CustomerID
							INNER JOIN tblTransactions TRX ON CBH.ContactID = TRX.CustomerID 
														   AND CONVERT(TRX.TransactionDate, DATE) = dteBillingDate
														   AND TRX.CashierName = CONCAT(strCreatedByName,'-FC')
														   AND TRX.Datasource = CONCAT('CreditBillerFC:', lngCreditBillID)
							WHERE CBH.CreditBillID = lngCreditBillID AND (Prev2MoCurrentDueAmount + Prev1MoCurrentDueAmount) > 0;
			/***********END - INSERT THE GCI LATE PAYMENT CHARGE**************/

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
				,CBH.CurrentDueAmount = CBH.Prev1MoCurrentDueAmount + CBH.CurrMonthAmountPaid + IFNULL(CBDtbc.TotalBillCharges,0) + CBH.CurrMonthCreditAmt
				,CBH.EndingBalance = CBH.BeginningBalance + CBH.CurrMonthAmountPaid + IFNULL(CBDtbc.TotalBillCharges,0) + CurrentPurchaseAmt 
			WHERE CBH.CreditBillID = lngCreditBillID;

			-- update the minimum amountdue
			UPDATE tblCreditBillHeader 
			SET  MinimumAmountDue = CurrentDueAmount
				,RunningCreditAmt = Prev1MoCurrentDueAmount + CurrMonthAmountPaid
			WHERE CreditBillID = lngCreditBillID;

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
			SET CCI.Last2BillingDate = CCI.LastBillingDate, CCI.LastBillingDate = dteBillingDate;

		END IF;

		IF (boDebug = 1) THEN
			SELECT * FROM tblContacts WHERE Credit > 0 AND ContactID IN (SELECT CustomerID FROM tblContactCreditCardInfo WHERE CreditCradTypeID = intCardTypeID);
		END IF;
		
		-- re-sync to include the unposted credit's
		CALL procSyncContactCredit();
		
	END IF;
END;
GO
delimiter ;

/**************************************************************
	procProcessCreditBillsWGClose
	Lemuel E. Aceron
	CALL procProcessCreditBillsWGClose();
	24-Oct-2014	Create this procedure
				Separate this procedure from existing procProcessCreditBillsWG
	
**************************************************************/
delimiter GO
DROP PROCEDURE IF EXISTS procProcessCreditBillsWGClose
GO

create procedure procProcessCreditBillsWGClose(
	IN strCardTypeCode VARCHAR(30)
	 ) 
BEGIN
	
	DECLARE bolCreditUseLastDayCutOffDate TINYINT DEFAULT 1;	
	DECLARE dteCreditCutOffDate, dteBillingDate, dteCreditPurcStartDateToProcess, dteCreditPurcEndDateToProcess DATE;
	DECLARE intCardTypeID INT(10) DEFAULT 0;

	SELECT CardTypeID, CreditUseLastDayCutOffDate, CreditCutOffDate, BillingDate, CreditPurcStartDateToProcess, CreditPurcEndDateToProcess
	INTO   intCardTypeID, bolCreditUseLastDayCutOffDate, dteCreditCutOffDate, dteBillingDate, dteCreditPurcStartDateToProcess, dteCreditPurcEndDateToProcess
	FROM tblCardTypes WHERE CardTypeCode = strCardTypeCode;

	IF intCardTypeID <> 0 THEN
		IF dteBillingDate < now() THEN
		
			-- set the parameters to next rundate
			-- Note the interval is not 15Days instead it's only 13Days to accomodate the February

			-- IF (bolCreditUseLastDayCutOffDate = 1 OR ISNULL(bolCreditUseLastDayCutOffDate)) AND DAY(dteCreditCutOffDate) >= 28 THEN
			-- 	SET dteCreditCutOffDate = (SELECT LAST_DAY(dteCreditCutOffDate));
			-- END IF;
			SET dteCreditCutOffDate = DATE_ADD(dteCreditCutOffDate, INTERVAL 13 DAY);
			IF (DAY(dteCreditCutOffDate) <= 15) THEN
				SET dteCreditCutOffDate = (SELECT CONCAT(YEAR(dteCreditCutOffDate), '-', MONTH(dteCreditCutOffDate), '-', '15'));
			ELSE
				SET dteCreditCutOffDate = (SELECT LAST_DAY(dteCreditCutOffDate));
			END IF;

			SET dteBillingDate = DATE_ADD(dteBillingDate, INTERVAL 13 DAY);
			IF (DAY(dteBillingDate) <= 15) THEN
				SET dteBillingDate = (SELECT CONCAT(YEAR(dteBillingDate), '-', MONTH(dteBillingDate), '-', '06'));
			ELSE
				SET dteBillingDate = (SELECT CONCAT(YEAR(dteBillingDate), '-', MONTH(dteBillingDate), '-', '20'));
			END IF;

			SET dteCreditPurcStartDateToProcess = DATE_ADD(dteCreditPurcStartDateToProcess, INTERVAL 13 DAY);
			IF (DAY(dteCreditPurcStartDateToProcess) <= 15) THEN
				SET dteCreditPurcStartDateToProcess = (SELECT CONCAT(YEAR(dteCreditPurcStartDateToProcess), '-', MONTH(dteCreditPurcStartDateToProcess), '-', '06'));
			ELSE
				SET dteCreditPurcStartDateToProcess = (SELECT CONCAT(YEAR(dteCreditPurcStartDateToProcess), '-', MONTH(dteCreditPurcStartDateToProcess), '-', '20'));
			END IF;

			SET dteCreditPurcEndDateToProcess = DATE_ADD(dteCreditPurcEndDateToProcess, INTERVAL 13 DAY);
			IF (DAY(dteCreditPurcEndDateToProcess) <= 15) THEN
				SET dteCreditPurcEndDateToProcess = (SELECT CONCAT(YEAR(dteCreditPurcEndDateToProcess), '-', MONTH(dteCreditPurcEndDateToProcess), '-', '05'));
			ELSE
				SET dteCreditPurcEndDateToProcess = (SELECT CONCAT(YEAR(dteCreditPurcEndDateToProcess), '-', MONTH(dteCreditPurcEndDateToProcess), '-', '19'));
			END IF;

			/** end-Update the header with the details *****/
			UPDATE tblCardTypes SET
				CreditPurcStartDateToProcess = dteCreditPurcStartDateToProcess,
				CreditPurcEndDateToProcess = dteCreditPurcEndDateToProcess,
				CreditCutOffDate = dteCreditCutOffDate,
				BillingDate = dteBillingDate
			WHERE CardTypeCode = strCardTypeCode;
		END IF;
	END IF;
END;
GO
delimiter ;


/**************************************************************

	procContactCreditPaymentSelectDetailed

	Sep 15, 2014 : Lemu
	- create this procedure

	Descrition: 
		1. get transaction details for resuming transaction
		2. get transactions list for reports
		3. get list of suspended transactions

	CALL procContactCreditPaymentSelectDetailed(0, '', 0, 2, 8, '2014-12-11', '2014-12-12', null, null, '', '', '', 'ASC', 10);

**************************************************************/
delimiter GO
DROP PROCEDURE IF EXISTS procContactCreditPaymentSelectDetailed
GO

create procedure procContactCreditPaymentSelectDetailed(
									IN intBranchID BIGINT, 
									IN strTerminalNo VARCHAR(20),
									IN intContactID BIGINT(20),
									IN intCreditType INT(2),
									IN intCreditCardTypeID INT(10),
									IN dtePaymentDateFrom DATETIME,
									IN dtePaymentDateTo DATETIME,
									IN strCreditorLastNameFrom VARCHAR(200), 
									IN strCreditorLastNameTo VARCHAR(200), 
									IN strGuaLastNameFrom VARCHAR(200), 
									IN strGuaLastNameTo VARCHAR(200), 
									IN strSortField VARCHAR(200), 
									IN strSortOption VARCHAR(4), 
									IN intLimit INT(10))
BEGIN
	SET @SQL := '';
	
		SET @SQL := 'SELECT CPC.BranchID, CPC.TerminalNo, MAX(CPC.SyncID) SyncID, MAX(CPC.CreditPaymentCashID) CreditPaymentCashID, CPC.TransactionID, CPC.TransactionNo, MAX(CPC.CreatedOn) TransactionDate,
							SUM(CASE CreditReasonID WHEN 1 THEN CPC.Amount WHEN 5 THEN CPC.Amount ELSE 0 END) AS LatePenaltyAmount,
							SUM(CASE CreditReasonID WHEN 2 THEN CPC.Amount ELSE 0 END) AS FinanceChargeAmount,
							SUM(CASE CreditReasonID WHEN 0 THEN CPC.Amount ELSE 0 END) AS PrincipalAmount,
							MAX(CPC.Remarks) Remarks, MAX(CPC.TransactionNo) TransactionNo, MAX(CPC.CreatedOn) CreatedOn, MAX(CPC.LastModified) LastModified,
							''Cash'' AS PaymentSource,
							MAX(CPC.CPRefBranchID) CPRefBranchID, MAX(CPC.CPRefTerminalNo) CPRefTerminalNo, MAX(CP.TransactionNo) AS CPRefTransactionNo,
							MAX(CP.CreditReasonID) CreditReasonID, MAX(CP.CreditReason) CreditReason,
							cci.CreditCardNo, cntct.ContactName, SUM(CPC.Amount) Amount,
							IFNULL(gci.CreditCardNo, '''') GuarantorCreditCardNo, 
							IFNULL(gua.ContactCode, '''') GuarantorCode, 
							IFNULL(gua.ContactName,'''') GuarantorName
						FROM tblCreditPaymentCash CPC 
						INNER JOIN tblCreditPayment CP ON CPC.CreditPaymentID = CP.CreditPaymentID 
							AND CPC.CPRefBranchID = CP.BranchID AND CPC.CPRefTerminalNo = CP.TerminalNo
						INNER JOIN tblContactCreditCardInfo cci ON cci.CustomerID = CP.ContactID
						INNER JOIN tblContacts cntct ON cntct.ContactID = CP.ContactID
						INNER JOIN tblCardTypes ctype ON cci.CreditCardTypeID = ctype.CardTypeID
						LEFT OUTER JOIN tblContactCreditCardInfo gci ON gci.CustomerID = cci.GuarantorID
						LEFT OUTER JOIN tblContacts gua ON gua.ContactID = cci.GuarantorID
						WHERE 1=1 ';

	IF (intBranchID <> 0) THEN
		SET @SQL = CONCAT(@SQL,'AND CPC.BranchID = ', intBranchID,' ');
	END IF;
	
	IF (strTerminalNo <> '') THEN
		SET @SQL = CONCAT(@SQL,'AND CPC.TerminalNo = ''', strTerminalNo,''' ');
	END IF;

	IF (intContactID <> 0) THEN
		SET @SQL = CONCAT(@SQL,'AND CP.ContactID = ', intContactID,' ');
	END IF;

	IF (intCreditType = 0) THEN -- group
		SET @SQL = CONCAT(@SQL,'AND ctype.Withguarantor = 1 ');
	ELSEIF (intCreditType = 1) THEN -- individual
		SET @SQL = CONCAT(@SQL,'AND ctype.Withguarantor = 0 ');
	END IF;

	IF (intCreditCardTypeID <> 0) THEN
		SET @SQL = CONCAT(@SQL,'AND cci.CreditCardTypeID = ', intCreditCardTypeID,' ');
	END IF;

	IF (DATE_FORMAT(dtePaymentDateFrom, '%Y-%m-%d')  <> DATE_FORMAT('1900-01-01', '%Y-%m-%d')) THEN
		SET @SQL = CONCAT(@SQL,'AND CPC.CreatedOn >= ''', dtePaymentDateFrom,''' ');
	END IF;

	IF (DATE_FORMAT(dtePaymentDateTo, '%Y-%m-%d')  <> DATE_FORMAT('1900-01-01', '%Y-%m-%d')) THEN
		SET @SQL = CONCAT(@SQL,'AND CPC.CreatedOn <= ''', dtePaymentDateTo,''' ');
	END IF;

	IF (strCreditorLastNameFrom <> '' AND strCreditorLastNameTo <> '') THEN
		SET @SQL = CONCAT(@SQL,'AND CP.ContactID IN (SELECT ContactID FROM tblContactAddOn WHERE LastName >= ''', strCreditorLastNameFrom,''' AND LastName <= ''', strCreditorLastNameTo,''') ');
	ELSEIF (strCreditorLastNameFrom <> '') THEN
		SET @SQL = CONCAT(@SQL,'AND CP.ContactID IN (SELECT ContactID FROM tblContactAddOn WHERE LastName LIKE ''', strCreditorLastNameFrom,'%'') ');
	ELSEIF (strCreditorLastNameTo <> '') THEN
		SET @SQL = CONCAT(@SQL,'AND CP.ContactID IN (SELECT ContactID FROM tblContactAddOn WHERE LastName LIKE ''', strCreditorLastNameTo,'%'') ');
	END IF;

	IF (strGuaLastNameFrom <> '' AND strGuaLastNameTo <> '') THEN
		SET @SQL = CONCAT(@SQL,'AND cci.GuarantorID IN (SELECT ContactID FROM tblContactAddOn WHERE LastName >= ''', strGuaLastNameFrom,''' AND LastName <= ''', strGuaLastNameTo,''') ');
	ELSEIF (strGuaLastNameFrom <> '') THEN
		SET @SQL = CONCAT(@SQL,'AND cci.GuarantorID IN (SELECT ContactID FROM tblContactAddOn WHERE LastName LIKE ''', strGuaLastNameFrom,'%'') ');
	ELSEIF (strGuaLastNameTo <> '') THEN
		SET @SQL = CONCAT(@SQL,'AND cci.GuarantorID IN (SELECT ContactID FROM tblContactAddOn WHERE LastName LIKE ''', strGuaLastNameTo,'%'') ');
	END IF;

	SET @SQL = CONCAT(@SQL,'GROUP BY CPC.BranchID, CPC.TerminalNo, CPC.TransactionID, CPC.TransactionNo, 
							cci.CreditCardNo, cntct.ContactName, 
							IFNULL(gci.CreditCardNo, ''''), 
							IFNULL(gua.ContactCode, ''''), 
							IFNULL(gua.ContactName,'''') ');

	IF (strSortField = 'CPC.CreatedOn') THEN
		SET @SQL = CONCAT(@SQL,'ORDER BY ', strSortField, ' ');
	ELSEIF (strSortField <> '') THEN
		SET @SQL = CONCAT(@SQL,'ORDER BY ', strSortField, ', CPC.CreatedOn ');
	ELSE
		SET @SQL = CONCAT(@SQL,'ORDER BY CPC.CreatedOn ');
	END IF;

	IF (strSortOption <> '') THEN SET @SQL = CONCAT(@SQL,strSortOption,' '); END IF;
	IF (intLimit <> 0) THEN SET @SQL = CONCAT(@SQL,'LIMIT ', intLimit,' '); END IF;
	
	PREPARE stmt FROM @SQL;
	EXECUTE stmt;
	DEALLOCATE PREPARE stmt;

	
END;
GO
delimiter ;

/**************************************************************

	procContactCreditPaymentSelect

	Sep 15, 2014 : Lemu
	- create this procedure

	Descrition: 
		1. get transaction details for resuming transaction
		2. get transactions list for reports
		3. get list of suspended transactions

	CALL procContactCreditPaymentSelect(0, '', 0, 2, 0, '1900-01-01', '1900-01-01', '', '', '', '', '', 'ASC', 10);

**************************************************************/
delimiter GO
DROP PROCEDURE IF EXISTS procContactCreditPaymentSelect
GO

create procedure procContactCreditPaymentSelect(
									IN intBranchID BIGINT, 
									IN strTerminalNo VARCHAR(20),
									IN intContactID BIGINT(20),
									IN intCreditType INT(2),
									IN intCreditCardTypeID INT(10),
									IN dtePaymentDateFrom DATETIME,
									IN dtePaymentDateTo DATETIME,
									IN strCreditorLastNameFrom VARCHAR(200), 
									IN strCreditorLastNameTo VARCHAR(200), 
									IN strGuaLastNameFrom VARCHAR(200), 
									IN strGuaLastNameTo VARCHAR(200), 
									IN strSortField VARCHAR(200), 
									IN strSortOption VARCHAR(4), 
									IN intLimit INT(10))
BEGIN
	SET @SQL := '';
	
		SET @SQL := 'SELECT trx.BranchID, trx.TerminalNo, trx.SyncID, 0 CreditPaymentCashID, trx.TransactionID, trx.TransactionNo, trx.CreatedOn TransactionDate,
							0 LatePenaltyAmount,
							0 FinanceChargeAmount,
							SubTotal PrincipalAmount,
							'''' Remarks, trx.CreatedOn, trx.LastModified,
							''Cash'' AS PaymentSource,
							trx.BranchID AS CPRefBranchID, trx.TerminalNo AS CPRefTerminalNo, trx.TransactionNo AS CPRefTransactionNo,
							0 CreditReasonID, CONCAT(''Payment @ Ter#:'', trx.TerminalNo,'' Br#:'',trx.BranchID) CreditReason,
							cci.CreditCardNo, cntct.ContactName, trx.SubTotal Amount,
							IFNULL(gci.CreditCardNo, '''') GuarantorCreditCardNo, 
							IFNULL(gua.ContactCode, '''') GuarantorCode, 
							IFNULL(gua.ContactName,'''') GuarantorName
						FROM tblTransactions trx
						INNER JOIN tblContactCreditCardInfo cci ON cci.CustomerID = trx.CustomerID
						INNER JOIN tblContacts cntct ON cntct.ContactID =  trx.CustomerID
						LEFT OUTER JOIN tblContactCreditCardInfo gci ON gci.CustomerID = cci.GuarantorID
						LEFT OUTER JOIN tblContacts gua ON gua.ContactID = cci.GuarantorID
						WHERE TransactionStatus = 7 ';

	IF (intBranchID <> 0) THEN
		SET @SQL = CONCAT(@SQL,'AND trx.BranchID = ', intBranchID,' ');
	END IF;
	
	IF (strTerminalNo <> '') THEN
		SET @SQL = CONCAT(@SQL,'AND trx.TerminalNo = ''', strTerminalNo,''' ');
	END IF;

	IF (intContactID <> 0) THEN
		SET @SQL = CONCAT(@SQL,'AND trx.CustomerID = ', intContactID,' ');
	END IF;

	IF (intCreditType = 0) THEN -- group
		SET @SQL = CONCAT(@SQL,'AND cci.GuarantorID <> 0 ');
	ELSEIF (intCreditType = 1) THEN -- individual
		SET @SQL = CONCAT(@SQL,'AND cci.GuarantorID = 0 ');
	END IF;

	IF (intCreditCardTypeID <> 0) THEN
		SET @SQL = CONCAT(@SQL,'AND cci.CreditCardTypeID = ', intCreditCardTypeID,' ');
	END IF;

	IF (DATE_FORMAT(dtePaymentDateFrom, '%Y-%m-%d')  <> DATE_FORMAT('1900-01-01', '%Y-%m-%d')) THEN
		SET @SQL = CONCAT(@SQL,'AND trx.CreatedOn >= ''', dtePaymentDateFrom,''' ');
	END IF;

	IF (DATE_FORMAT(dtePaymentDateTo, '%Y-%m-%d')  <> DATE_FORMAT('1900-01-01', '%Y-%m-%d')) THEN
		SET @SQL = CONCAT(@SQL,'AND trx.CreatedOn <= ''', dtePaymentDateTo,''' ');
	END IF;

	IF (strCreditorLastNameFrom <> '' AND strCreditorLastNameTo <> '') THEN
		SET @SQL = CONCAT(@SQL,'AND trx.CustomerID IN (SELECT ContactID FROM tblContactAddOn WHERE LastName >= ''', strCreditorLastNameFrom,''' AND LastName <= ''', strCreditorLastNameTo,''') ');
	ELSEIF (strCreditorLastNameFrom <> '') THEN
		SET @SQL = CONCAT(@SQL,'AND trx.CustomerID IN (SELECT ContactID FROM tblContactAddOn WHERE LastName LIKE ''', strCreditorLastNameFrom,'%'') ');
	ELSEIF (strCreditorLastNameTo <> '') THEN
		SET @SQL = CONCAT(@SQL,'AND trx.CustomerID IN (SELECT ContactID FROM tblContactAddOn WHERE LastName LIKE ''', strCreditorLastNameTo,'%'') ');
	END IF;

	IF (strGuaLastNameFrom <> '' AND strGuaLastNameTo <> '') THEN
		SET @SQL = CONCAT(@SQL,'AND cci.GuarantorID IN (SELECT ContactID FROM tblContactAddOn WHERE LastName >= ''', strGuaLastNameFrom,''' AND LastName <= ''', strGuaLastNameTo,''') ');
	ELSEIF (strGuaLastNameFrom <> '') THEN
		SET @SQL = CONCAT(@SQL,'AND cci.GuarantorID IN (SELECT ContactID FROM tblContactAddOn WHERE LastName LIKE ''', strGuaLastNameFrom,'%'') ');
	ELSEIF (strGuaLastNameTo <> '') THEN
		SET @SQL = CONCAT(@SQL,'AND cci.GuarantorID IN (SELECT ContactID FROM tblContactAddOn WHERE LastName LIKE ''', strGuaLastNameTo,'%'') ');
	END IF;

	IF (strSortField = 'trx.CreatedOn') THEN
		SET @SQL = CONCAT(@SQL,'ORDER BY ', strSortField, ' ');
	ELSEIF (strSortField <> '') THEN
		SET @SQL = CONCAT(@SQL,'ORDER BY ', strSortField, ', trx.CreatedOn ');
	ELSE
		SET @SQL = CONCAT(@SQL,'ORDER BY trx.CreatedOn ');
	END IF;

	IF (strSortOption <> '') THEN SET @SQL = CONCAT(@SQL,strSortOption,' '); END IF;
	IF (intLimit <> 0) THEN SET @SQL = CONCAT(@SQL,'LIMIT ', intLimit,' '); END IF;
	
	PREPARE stmt FROM @SQL;
	EXECUTE stmt;
	DEALLOCATE PREPARE stmt;

	
END;
GO
delimiter ;

/***
 SELECT CPC.BranchID, CPC.TerminalNo, MAX(CPC.SyncID) SyncID, MAX(CPC.CreditPaymentCashID) CreditPaymentCashID, CPC.TransactionID, CPC.TransactionNo, CPC.CreatedOn TransactionDate,
                                                       0 LatePenaltyAmount,
                                                       0 FinanceChargeAmount,
                                                       SUM(CPC.Amount) PrincipalAmount,
                                                       CPC.Remarks, CPC.TransactionNo, CPC.CreatedOn, CPC.LastModified,
                                                       'Cash' AS PaymentSource,
                                                       CPC.CPRefBranchID, CPC.CPRefTerminalNo, MAX(CP.TransactionNo) AS CPRefTransactionNo,
                                                       MAX(CP.CreditReasonID) CreditReasonID, CONCAT('Payment') CreditReason,
                                                       cci.CreditCardNo, cntct.ContactName, SUM(CPC.Amount) Amount,
                                                       IFNULL(gci.CreditCardNo, '') GuarantorCreditCardNo, IFNULL(gua.ContactName,'') GuarantorName
                                               FROM tblCreditPaymentCash CPC
                                               INNER JOIN tblCreditPayment CP ON CPC.CreditPaymentID = CP.CreditPaymentID
                                                       AND CPC.CPRefBranchID = CP.BranchID AND CPC.CPRefTerminalNo = CP.TerminalNo
                                               INNER JOIN tblContactCreditCardInfo cci ON cci.CustomerID = CP.ContactID
                                               INNER JOIN tblContacts cntct ON cntct.ContactID = CP.ContactID
                                               LEFT OUTER JOIN tblContactCreditCardInfo gci ON gci.CustomerID = cci.GuarantorID
                                               LEFT OUTER JOIN tblContacts gua ON gua.ContactID = cci.GuarantorID
                                               WHERE 1=1 
											   GROUP BY
												CPC.BranchID, CPC.TerminalNo, CPC.TransactionID, CPC.TransactionNo, CPC.CreatedOn,
                                                       CPC.Remarks, CPC.TransactionNo, CPC.CreatedOn, CPC.LastModified,
                                                       CPC.CPRefBranchID, CPC.CPRefTerminalNo, 
                                                       cci.CreditCardNo, cntct.ContactName,
                                                       IFNULL(gci.CreditCardNo, ''), IFNULL(gua.ContactName,'')
											   ORDER BY CPC.CreatedOn ASC LIMIT 10
***/

/**************************************************************

	procContactCreditPurchaseSelect

	Nov 02, 2014 : Lemu
	- create this procedure

	Descrition: 
		1. get transaction details for resuming transaction
		2. get transactions list for reports
		3. get list of suspended transactions

	CALL procContactCreditPurchaseSelect(0, '', 0, 0, 1, '1900-01-01', '1900-01-01', '', '', '', '', '', 'ASC', 10);

**************************************************************/
delimiter GO
DROP PROCEDURE IF EXISTS procContactCreditPurchaseSelect
GO

create procedure procContactCreditPurchaseSelect(
									IN intBranchID BIGINT, 
									IN strTerminalNo VARCHAR(20),
									IN intContactID BIGINT(20),
									IN intCreditType INT(2),
									IN intCreditCardTypeID INT(10),
									IN dtePurchaseDateFrom DATETIME,
									IN dtePurchaseDateTo DATETIME,
									IN strCreditorLastNameFrom VARCHAR(200), 
									IN strCreditorLastNameTo VARCHAR(200), 
									IN strGuaLastNameFrom VARCHAR(200), 
									IN strGuaLastNameTo VARCHAR(200), 
									IN strSortField VARCHAR(200), 
									IN strSortOption VARCHAR(4), 
									IN intLimit INT(10))
BEGIN
	SET @SQL := '';
	
		SET @SQL := 'SELECT CP.BranchID, CP.TerminalNo, CP.SyncID, CP.TransactionID, CP.TransactionNo, CP.CreditDate TransactionDate,
							CASE CreditReasonID WHEN 1 THEN CP.Amount WHEN 5 THEN CP.Amount ELSE 0 END LatePenaltyAmount,
							CASE CreditReasonID WHEN 2 THEN CP.Amount ELSE 0 END FinanceChargeAmount,
							CASE CreditReasonID WHEN 0 THEN CP.Amount ELSE 0 END PrincipalAmount,
							CP.Remarks, CP.CreatedOn, CP.LastModified,
							CP.CreditReasonID, CP.CreditReason,
							cci.CreditCardNo, cntct.ContactName,  CP.Amount,
							IFNULL(gci.CreditCardNo, '''') GuarantorCreditCardNo, 
							IFNULL(gua.ContactCode, '''') GuarantorCode, 
							IFNULL(gua.ContactName,'''') GuarantorName
						FROM tblCreditPayment CP
						INNER JOIN tblContactCreditCardInfo cci ON cci.CustomerID = CP.ContactID
						INNER JOIN tblContacts cntct ON cntct.ContactID = CP.ContactID
						LEFT OUTER JOIN tblContactCreditCardInfo gci ON gci.CustomerID = cci.GuarantorID
						LEFT OUTER JOIN tblContacts gua ON gua.ContactID = cci.GuarantorID
						WHERE 1=1 ';

	IF (intBranchID <> 0) THEN
		SET @SQL = CONCAT(@SQL,'AND CP.BranchID = ', intBranchID,' ');
	END IF;
	
	IF (strTerminalNo <> '') THEN
		SET @SQL = CONCAT(@SQL,'AND CP.TerminalNo = ''', strTerminalNo,''' ');
	END IF;

	IF (intContactID <> 0) THEN
		SET @SQL = CONCAT(@SQL,'AND CP.ContactID = ', intContactID,' ');
	END IF;

	IF (intCreditType = 0) THEN -- group
		SET @SQL = CONCAT(@SQL,'AND cci.GuarantorID <> 0 ');
	ELSEIF (intCreditType = 1) THEN -- individual
		SET @SQL = CONCAT(@SQL,'AND cci.GuarantorID = 0 ');
	END IF;

	IF (intCreditCardTypeID <> 0) THEN
		SET @SQL = CONCAT(@SQL,'AND CP.CreditCardTypeID = ', intCreditCardTypeID,' ');
	END IF;

	IF (DATE_FORMAT(dtePurchaseDateFrom, '%Y-%m-%d')  <> DATE_FORMAT('1900-01-01', '%Y-%m-%d')) THEN
		SET @SQL = CONCAT(@SQL,'AND CP.CreditDate >= ''', dtePurchaseDateFrom,''' ');
	END IF;

	IF (DATE_FORMAT(dtePurchaseDateTo, '%Y-%m-%d')  <> DATE_FORMAT('1900-01-01', '%Y-%m-%d')) THEN
		SET @SQL = CONCAT(@SQL,'AND CP.CreditDate <= ''', dtePurchaseDateTo,''' ');
	END IF;

	IF (strCreditorLastNameFrom <> '' AND strCreditorLastNameTo <> '') THEN
		SET @SQL = CONCAT(@SQL,'AND CP.ContactID IN (SELECT ContactID FROM tblContactAddOn WHERE LastName >= ''', strCreditorLastNameFrom,''' AND LastName <= ''', strCreditorLastNameTo,''') ');
	ELSEIF (strCreditorLastNameFrom <> '') THEN
		SET @SQL = CONCAT(@SQL,'AND CP.ContactID IN (SELECT ContactID FROM tblContactAddOn WHERE LastName LIKE ''', strCreditorLastNameFrom,'%'') ');
	ELSEIF (strCreditorLastNameTo <> '') THEN
		SET @SQL = CONCAT(@SQL,'AND CP.ContactID IN (SELECT ContactID FROM tblContactAddOn WHERE LastName LIKE ''', strCreditorLastNameTo,'%'') ');
	END IF;

	IF (strGuaLastNameFrom <> '' AND strGuaLastNameTo <> '') THEN
		SET @SQL = CONCAT(@SQL,'AND cci.GuarantorID IN (SELECT ContactID FROM tblContactAddOn WHERE LastName >= ''', strGuaLastNameFrom,''' AND LastName <= ''', strGuaLastNameTo,''') ');
	ELSEIF (strGuaLastNameFrom <> '') THEN
		SET @SQL = CONCAT(@SQL,'AND cci.GuarantorID IN (SELECT ContactID FROM tblContactAddOn WHERE LastName LIKE ''', strGuaLastNameFrom,'%'') ');
	ELSEIF (strGuaLastNameTo <> '') THEN
		SET @SQL = CONCAT(@SQL,'AND cci.GuarantorID IN (SELECT ContactID FROM tblContactAddOn WHERE LastName LIKE ''', strGuaLastNameTo,'%'') ');
	END IF;

	IF (strSortField = 'CP.CreditDate') THEN
		SET @SQL = CONCAT(@SQL,'ORDER BY ', strSortField, ' ');
	ELSEIF (strSortField <> '') THEN
		SET @SQL = CONCAT(@SQL,'ORDER BY ', strSortField, ', CP.CreditDate ');
	ELSE
		SET @SQL = CONCAT(@SQL,'ORDER BY CP.CreditDate ');
	END IF;

	IF (strSortOption <> '') THEN SET @SQL = CONCAT(@SQL,strSortOption,' '); END IF;
	IF (intLimit <> 0) THEN SET @SQL = CONCAT(@SQL,'LIMIT ', intLimit,' '); END IF;

	PREPARE stmt FROM @SQL;
	EXECUTE stmt;
	DEALLOCATE PREPARE stmt;

	
END;
GO
delimiter ;



/*********************************
	procContactChangeGuarantor
	Lemuel E. Aceron
	CALL procContactChangeGuarantor();
	
	October 26, 2014 - create this procedure
*********************************/
delimiter GO
DROP PROCEDURE IF EXISTS procContactChangeGuarantor
GO

create procedure procContactChangeGuarantor(
	IN intContactID BIGINT(20),
	IN intGuarantorID BIGINT(20),
	IN strUpdatedBy varchar(150))
BEGIN

	DECLARE intOldGuarantorID BIGINT(20) DEFAULT 0;

	SET intOldGuarantorID = (SELECT GuarantorID FROM tblContactCreditCardInfo WHERE CustomerID = intContactID LIMIT 1);

	-- update the contact with the new guarantor
	UPDATE tblContactCreditCardInfo SET GuarantorID = intGuarantorID WHERE CustomerID = intContactID AND GuarantorID = intOldGuarantorID;
	
	CALL procsysAuditInsert(NOW(), strUpdatedBy, 'CONTACT GUARANTOR', 'localhost', CONCAT('GurantorID of customer: ',intContactID,' was overwritten from ',intOldGuarantorID,' to ',intGuarantorID,' due to backend update.'));

END;
GO
delimiter ;


/*********************************
	procContactUpdateRemarks
	Lemuel E. Aceron
	CALL procContactUpdateRemarks();
	
	October 26, 2014 - create this procedure
*********************************/
delimiter GO
DROP PROCEDURE IF EXISTS procContactUpdateRemarks
GO

create procedure procContactUpdateRemarks(
	IN intContactID BIGINT(20),
	IN strRemarks VARCHAR(150),
	IN strUpdatedBy varchar(150))
BEGIN

	-- update the contact with the new guarantor
	UPDATE tblContacts SET Remarks = strRemarks WHERE ContactID = intContactID;
	
	-- CALL procsysAuditInsert(NOW(), strUpdatedBy, 'CONTACT GUARANTOR', 'localhost', CONCAT('GurantorID of customer: ',intContactID,' was overwritten from ',intOldGuarantorID,' to ',intGuarantorID,' due to backend update.'));

END;
GO
delimiter ;

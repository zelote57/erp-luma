/**************************************************************
	procProductQuantityConvert
	Lemuel E. Aceron
	CALL procProcessCreditBills();
	08-Jul-2012	Create this procedure
	
	Sample:
		PurchaseStart:	Dec 10, 2012
		PurchaseEnd:	Jan 9, 2013
		CreditCutOff:	Dec 31, 2012
		BillingDate:	Jan 10, 2012

**************************************************************/
delimiter GO
DROP PROCEDURE IF EXISTS procProcessCreditBills
GO

create procedure procProcessCreditBills(
	 ) 
BEGIN
	DECLARE lngCreditBillID BIGINT DEFAULT 0;
	DECLARE dteCreditPurcStartDateToProcess DATE;
	DECLARE dteCreditPurcEndDateToProcess DATE;
	DECLARE dteBillingDate DATE;
	DECLARE dteCreditCutOffDate DATE;
	DECLARE dteStartCreditCutOffDate DATE;
	DECLARE dteEndCreditCutOffDate DATE;
	DECLARE decCreditFinanceCharge DECIMAL(10,3) DEFAULT 0;
	DECLARE decCreditMinimumPercentageDue DECIMAL(10,3) DEFAULT 0;
	DECLARE decCreditMinimumAmountDue DECIMAL(10,3) DEFAULT 0;
	DECLARE decCreditLatePenaltyCharge DECIMAL(10,3) DEFAULT 0;

	DECLARE dtePrev2BillingDate DATE;
	DECLARE dtePrevBillingDate DATE;
	DECLARE dteNextBillingDate DATE;

	DECLARE bolCreditUseLastDayCutOffDate TINYINT default 1;

	declare dteCurrDate datetime default now();
	declare lngCreatedByID bigint default 1;
	declare strCreatedByName varchar(100) default 'Sys Credit Bill';

	SET dteBillingDate = (SELECT ConfigValue FROM sysCreditConfig WHERE ConfigName = 'BillingDate');
	IF dteBillingDate < now() THEN
		SET dtePrev2BillingDate = (SELECT DATE_ADD(dteBillingDate, INTERVAL -2 MONTH));
		SET dtePrevBillingDate = (SELECT DATE_ADD(dteBillingDate, INTERVAL -1 MONTH));
		SET dteNextBillingDate = (SELECT DATE_ADD(dteBillingDate, INTERVAL 1 MONTH));

		SET bolCreditUseLastDayCutOffDate = (SELECT ConfigValue FROM sysCreditConfig WHERE ConfigName = 'CreditUseLastDayCutOffDate');
		SET dteCreditCutOffDate = (SELECT ConfigValue FROM sysCreditConfig WHERE ConfigName = 'CreditCutOffDate');
		SET dteStartCreditCutOffDate = (SELECT DATE_FORMAT(DATE_ADD(dteCreditCutOffDate, INTERVAL -1 MONTH) ,'%Y-%m-01'));
		SET dteEndCreditCutOffDate = (SELECT DATE_ADD(dteCreditCutOffDate, INTERVAL -1 MONTH));

		-- IF DAY(dteCreditCutOffDate) >= 28 then
		IF bolCreditUseLastDayCutOffDate = 1 OR ISNULL(bolCreditUseLastDayCutOffDate) THEN
			SET dteCreditCutOffDate = (SELECT LAST_DAY(dteCreditCutOffDate));
			SET dteStartCreditCutOffDate = (SELECT DATE_FORMAT(DATE_ADD(dteCreditCutOffDate, INTERVAL -1 MONTH) ,'%Y-%m-01'));
			SET dteEndCreditCutOffDate = (SELECT DATE_ADD(dteCreditCutOffDate, INTERVAL -1 MONTH));
		END IF;

		IF NOT EXISTS(SELECT CreditBillID FROM tblCreditBills WHERE BillingDate = dteBillingDate) THEN
			INSERT INTO tblCreditBills(  CreditPurcStartDateToProcess ,CreditPurcEndDateToProcess ,BillingDate ,CreditCutOffDate
										,CreditFinanceCharge ,CreditMinimumPercentageDue ,CreditMinimumAmountDue
										,CreditLatePenaltyCharge ,CreatedOn ,CreatedByID ,CreatedByName)
			SELECT						 CreditPurcStartDateToProcess ,CreditPurcEndDateToProcess ,dteBillingDate ,dteCreditCutOffDate
										,CreditFinanceCharge ,CreditMinimumPercentageDue ,CreditMinimumAmountDue
										,CreditLatePenaltyCharge ,dteCurrDate ,lngCreatedByID,strCreatedByName
			FROM (
					SELECT	 PSDP.ConfigValue CreditPurcStartDateToProcess
							,PEDP.ConfigValue CreditPurcEndDateToProcess
							,COD.ConfigValue BillingDate
							,CCD.ConfigValue CreditCutOffDate
							,FC.ConfigValue CreditFinanceCharge
							,MPD.ConfigValue CreditMinimumPercentageDue
							,MAD.ConfigValue CreditMinimumAmountDue
							,LPC.ConfigValue CreditLatePenaltyCharge
					FROM sysCreditConfig COD
						LEFT OUTER JOIN sysCreditConfig PSDP ON 1=1 AND PSDP.ConfigName = 'CreditPurcStartDateToProcess'
						LEFT OUTER JOIN sysCreditConfig PEDP ON 1=1 AND PEDP.ConfigName = 'CreditPurcEndDateToProcess'
						LEFT OUTER JOIN sysCreditConfig CCD ON 1=1 AND PEDP.ConfigName = 'CreditCutOffDate'
						LEFT OUTER JOIN sysCreditConfig FC ON 1=1 AND FC.ConfigName = 'CreditFinanceCharge'
						LEFT OUTER JOIN sysCreditConfig MPD ON 1=1 AND MPD.ConfigName = 'CreditMinimumPercentageDue'
						LEFT OUTER JOIN sysCreditConfig MAD ON 1=1 AND MAD.ConfigName = 'CreditMinimumAmountDue'
						LEFT OUTER JOIN sysCreditConfig LPC ON 1=1 AND LPC.ConfigName = 'CreditLatePenaltyCharge'
					WHERE COD.ConfigName = 'BillingDate'
				) sysCreditConfig;
		END IF;

		/** Put the credit paramateres to be process *****/
		SELECT CreditBillID ,CreditPurcStartDateToProcess 
				,CreditPurcEndDateToProcess ,BillingDate ,CreditCutOffDate ,CreditFinanceCharge ,CreditMinimumPercentageDue 
				,CreditMinimumAmountDue ,CreditLatePenaltyCharge 
				INTO  lngCreditBillID ,dteCreditPurcStartDateToProcess 
				,dteCreditPurcEndDateToProcess ,dteBillingDate ,dteCreditCutOffDate ,decCreditFinanceCharge ,decCreditMinimumPercentageDue 
				,decCreditMinimumAmountDue ,decCreditLatePenaltyCharge 
		FROM tblCreditBills WHERE BillingDate = dteBillingDate;
		/** end-Put the credit paramateres to be process *****/

		/** Insert the Contacts that have credits as header *****/
		INSERT INTO tblCreditBillHeader(CreditBillID ,ContactID ,CreditLimit ,RunningCreditAmt
										,BillingDate ,CreatedOn ,CreatedByID ,CreatedByName)
		SELECT lngCreditBillID, CUS.ContactID ,CUS.CreditLimit ,CUS.Credit RunningCreditAmt
										,dteBillingDate ,dteCurrDate ,lngCreatedByID ,strCreatedByName
		FROM tblContacts CUS
			INNER JOIN tblContactCreditCardInfo CCI ON CUS.ContactID = CCI.CustomerID
		WHERE CUS.IsCreditAllowed = 1 AND CCI.LastBillingDate <= dteBillingDate
			AND CUS.ContactID NOT IN (SELECT ContactID FROM tblCreditBillHeader WHERE CreditBillID = lngCreditBillID);
		/** end-Insert the Contacts that have credits as header *****/
	


		/** Put the credit details for each contacts *****/
		-- Insert the Credit Details
		INSERT INTO tblCreditBillDetail(CreditBillHeaderID ,TransactionDate ,Description ,Amount ,TransactionTypeID ,TransactionRefID)
		SELECT CreditBillHeaderID ,CreditDate ,CONCAT('Credit: Trn#:' ,CONVERT(TransactionNo, UNSIGNED INTEGER),' @ Ter#:',TerminalNo) 'Description'
				,SUM(Amount) CurrMonthCreditAmt ,1 tblCreditPaymentTransactionTypeID ,TransactionID TransactionRefID
		FROM tblCreditBillHeader CBH
		INNER JOIN tblCreditPayment CRP on CRP.ContactID = CBH.ContactID
		WHERE CBH.CreditBillID = lngCreditBillID 
			AND CONVERT(CreditDate, DATE) BETWEEN dteCreditPurcStartDateToProcess AND dteCreditPurcEndDateToProcess
			AND CRP.TransactionID NOT IN (SELECT TransactionRefID FROM tblCreditBillDetail 
																	 WHERE TransactionTypeID = 1
																		AND CONVERT(CreditDate, DATE) BETWEEN dteCreditPurcStartDateToProcess AND dteCreditPurcEndDateToProcess)
		GROUP BY CreditBillHeaderID ,TransactionNo ,CreditDate;



		-- Insert the Payment Details
		INSERT INTO tblCreditBillDetail(CreditBillHeaderID ,TransactionDate ,Description ,Amount ,TransactionTypeID ,TransactionRefID)
		SELECT * 
		FROM
			(
				SELECT CreditBillHeaderID, TransactionDate
						,CONCAT('Payment: Trn#:' ,CONVERT(TransactionNo, UNSIGNED INTEGER),' @ Ter#:',TerminalNo) 'Description'
						,-SUM(Subtotal) CurrMonthCreditAmt ,2 tblCreditPaymentTransactionTypeID ,Trx.TransactionID TransactionRefID
				FROM tblTransactions Trx 
					INNER JOIN tblTransactionItems TrxD ON Trx.TransactionID = TrxD.TransactionID
					INNER JOIN tblCreditBillHeader CBH ON CBH.ContactID = Trx.CustomerID AND CBH.CreditBillID = lngCreditBillID
				WHERE TrxD.ProductID = 1 
					AND TransactionStatus = 1
					AND CONVERT(Trx.TransactionDate, DATE) BETWEEN dteStartCreditCutOffDate AND dteEndCreditCutOffDate
					AND Trx.TransactionID NOT IN (SELECT TransactionRefID FROM tblCreditBillDetail 
																			 WHERE TransactionTypeID = 2
																				AND CONVERT(TransactionDate, DATE) BETWEEN dteCreditPurcStartDateToProcess AND dteCreditPurcEndDateToProcess)
				GROUP BY TransactionNo
			) Payments WHERE CreditBillHeaderID <> 0;



		/** end-Put the credit details for each contacts *****/


		/** Update the header with the details *****/
		UPDATE tblCreditBillHeader AS CBH 
		LEFT OUTER JOIN	
		( 
			SELECT CreditBillHeaderID ,SUM(Amount) CurrMonthCreditAmt 
			FROM tblCreditBillDetail  WHERE TransactionTypeID = 1 
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


		-- insert the late payment Charges FOR bills that did not pay for the 1 month.
		INSERT INTO tblCreditBillDetail(CreditBillHeaderID ,TransactionDate ,Description ,Amount ,TransactionTypeID ,TransactionRefID)
		SELECT CreditBillHeaderID ,dteBillingDate 
				,CONCAT('Late Payment Charge  : (', Prev2MoCurrentDueAmount + Prev1MoCurrentDueAmount,' * ',decCreditLatePenaltyCharge,')') 'Description'
				,((Prev2MoCurrentDueAmount + Prev1MoCurrentDueAmount) * decCreditLatePenaltyCharge) CurrMonthCreditAmt 
				,3 tblCreditPaymentTransactionTypeID ,0 TransactionRefID
		FROM tblCreditBillHeader CBH
		WHERE CBH.CreditBillID = lngCreditBillID
			AND (Prev2MoCurrentDueAmount + Prev1MoCurrentDueAmount) > 0 AND CurrMonthAmountPaid = 0;


		-- update the credit of contact
		UPDATE tblContacts AS CON
		INNER JOIN
		(
			SELECT ContactID ,CurrentDueAmount FROM tblCreditBillHeader WHERE CreditBillID = lngCreditBillID
		) CBH ON CBH.ContactID = CON.ContactID
		SET CON.Credit = CurrentDueAmount;

		-- update the TransactionRefID to be use for saving to tblTransactions
		INSERT INTO tblTransactions (	 TransactionNo, BranchID, BranchCode, CustomerID, CustomerName
										,AgentID, AgentName, CreatedByID, CreatedByName, CashierID, CashierName
										,TerminalNo, TransactionDate, TransactionStatus
										,WaiterID, WaiterName ,AgentPositionName, AgentDepartmentName
										,SubTotal ,AmountPaid ,CreditPayment ,PaymentType ,DateClosed)
		SELECT LPAD(TransactionNo + rownum,14,'0'), 1, 'Main', CBH.ContactID ,CBH.ContactName 
						,1 ,'RetailPlus Agent' ,1 ,'Credit Bill' ,1 ,'Credit Biller-LPC'
						,'00' ,dteBillingDate ,1
						,2 ,'System Credit Bill' ,'Credit Bill Position' ,'Credit Bill Dept.'
						,CBH.LatePaymentChargeAmt ,CBH.LatePaymentChargeAmt ,CBH.LatePaymentChargeAmt ,5 ,dteBillingDate
		FROM (
			SELECT @rownum:=@rownum+1 AS rownum, CBH.*
			FROM (
				SELECT CBH.ContactID ,CON.ContactName ,((Prev2MoCurrentDueAmount + Prev1MoCurrentDueAmount) * decCreditLatePenaltyCharge) LatePaymentChargeAmt
				FROM tblCreditBillHeader CBH 
				INNER JOIN tblContacts CON ON CBH.ContactID = CON.ContactID
				WHERE CBH.CreditBillID = lngCreditBillID 
					AND (Prev2MoCurrentDueAmount + Prev1MoCurrentDueAmount) > 0 AND CurrMonthAmountPaid = 0
		) CBH, (SELECT @rownum:=0) r) CBH, (SELECT MAX(TransactionNo) TransactionNo  FROM tblTransactions) r;
		INSERT INTO tblCreditPayment(TransactionID, ContactID, GuarantorID, CreditType, 
									CreditBefore, Amount, CreditAfter, CreditExpiryDate, 
									CreditReason, CreditDate, 
									TerminalNo, CashierName, TransactionNo)
						SELECT TRX.TransactionID ,CBH.ContactID ,CCI.GuarantorID ,CCI.CreditType
									,CON.Credit - ((Prev2MoCurrentDueAmount + Prev1MoCurrentDueAmount) * decCreditLatePenaltyCharge)
									,(Prev2MoCurrentDueAmount + Prev1MoCurrentDueAmount) * decCreditLatePenaltyCharge
									,CON.Credit
									,CCI.ExpiryDate
									,CONCAT('Late Payment Charge  : (', Prev2MoCurrentDueAmount + Prev1MoCurrentDueAmount,' * ',decCreditLatePenaltyCharge,')')
									,dteBillingDate
									,'00', 'Credit Biller-LPC', TRX.TransactionNo
						FROM tblCreditBillHeader CBH
						INNER JOIN tblContacts CON ON CBH.ContactID = CON.ContactID
						INNER JOIN tblContactCreditCardInfo CCI ON CON.ContactID = CCI.CustomerID
						INNER JOIN tblTransactions TRX ON CBH.ContactID = TRX.CustomerID 
													   AND CONVERT(TRX.TransactionDate, DATE) = dteBillingDate
													   AND TRX.CashierName = 'Credit Biller-LPC'
						WHERE CBH.CreditBillID = lngCreditBillID 
							AND (Prev2MoCurrentDueAmount + Prev1MoCurrentDueAmount) > 0 AND CurrMonthAmountPaid = 0;
		/****
		UPDATE tblCreditBillDetail CBD
		INNER JOIN
		(
			SELECT TransactionID tbl CBH
			INNER JOIN tblCreditPayment CRP on CRP.ContactID = CBH.ContactID
		) 
		SET CBD.TransactionRefID = 
		****/

		-- insert the 30Days Charges
		INSERT INTO tblCreditBillDetail(CreditBillHeaderID ,TransactionDate ,Description ,Amount ,TransactionTypeID ,TransactionRefID)
		SELECT CreditBillHeaderID ,dteBillingDate 
				,CONCAT('Finance Charge 30days: (', Prev2MoCurrentDueAmount + Prev1MoCurrentDueAmount,' * ',decCreditFinanceCharge,')') 'Description'
				,((Prev2MoCurrentDueAmount + Prev1MoCurrentDueAmount) * decCreditFinanceCharge) CurrMonthCreditAmt 
				,4 tblCreditPaymentTransactionTypeID ,0 TransactionRefID
		FROM tblCreditBillHeader CBH
		WHERE CBH.CreditBillID = lngCreditBillID
			AND (Prev2MoCurrentDueAmount + Prev1MoCurrentDueAmount) > 0;


		INSERT INTO tblTransactions (	 TransactionNo, BranchID, BranchCode, CustomerID, CustomerName
										,AgentID, AgentName, CreatedByID, CreatedByName, CashierID, CashierName
										,TerminalNo, TransactionDate, TransactionStatus
										,WaiterID, WaiterName ,AgentPositionName, AgentDepartmentName
										,SubTotal ,AmountPaid ,CreditPayment ,PaymentType ,DateClosed)
		SELECT LPAD(TransactionNo + rownum,14,'0'), 1, 'Main', CBH.ContactID ,CBH.ContactName 
						,1 ,'RetailPlus Agent' ,1 ,'Credit Bill' ,1 ,'Credit Biller-FC'
						,'00' ,dteBillingDate ,1
						,2 ,'System Credit Bill' ,'Credit Bill Position' ,'Credit Bill Dept.'
						,CBH.FinanceCharge ,CBH.FinanceCharge ,CBH.FinanceCharge ,5 ,dteBillingDate
		FROM (
			SELECT @rownum:=@rownum+1 AS rownum, CBH.*
			FROM (
				SELECT CBH.ContactID ,CON.ContactName ,((Prev2MoCurrentDueAmount + Prev1MoCurrentDueAmount) * decCreditFinanceCharge) FinanceCharge
				FROM tblCreditBillHeader CBH
				INNER JOIN tblContacts CON ON CBH.ContactID = CON.ContactID
				WHERE CBH.CreditBillID = lngCreditBillID 
					AND (Prev2MoCurrentDueAmount + Prev1MoCurrentDueAmount) > 0
		) CBH, (SELECT @rownum:=0) r) CBH, (SELECT MAX(TransactionNo) TransactionNo  FROM tblTransactions) r;
		INSERT INTO tblCreditPayment(TransactionID, ContactID, GuarantorID, CreditType, 
									CreditBefore, Amount, CreditAfter, CreditExpiryDate, 
									CreditReason, CreditDate, 
									TerminalNo, CashierName, TransactionNo)
						SELECT TransactionID ,CBH.ContactID ,CCI.GuarantorID ,CCI.CreditType
									,CON.Credit - ((Prev2MoCurrentDueAmount + Prev1MoCurrentDueAmount) * decCreditFinanceCharge)
									,(Prev2MoCurrentDueAmount + Prev1MoCurrentDueAmount) * decCreditFinanceCharge
									,CON.Credit
									,CCI.ExpiryDate
									,CONCAT('Finance Charge 30days: (', Prev2MoCurrentDueAmount + Prev1MoCurrentDueAmount,' * ',decCreditFinanceCharge,')')
									,dteBillingDate
									,'00', 'Credit Biller-FC', TransactionNo 
						FROM tblCreditBillHeader CBH
						INNER JOIN tblContacts CON ON CBH.ContactID = CON.ContactID
						INNER JOIN tblContactCreditCardInfo CCI ON CON.ContactID = CCI.CustomerID
						INNER JOIN tblTransactions TRX ON CBH.ContactID = TRX.CustomerID 
													   AND CONVERT(TRX.TransactionDate, DATE) = dteBillingDate
													   AND TRX.CashierName = 'Credit Biller-FC'
						WHERE CBH.CreditBillID = lngCreditBillID 
							AND (Prev2MoCurrentDueAmount + Prev1MoCurrentDueAmount) > 0;

		-- insert the 60Days Charges

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

		-- update the LastBillingDate of contacts
		UPDATE tblContactCreditCardInfo AS CCI
		INNER JOIN
		(
			SELECT ContactID FROM tblCreditBillHeader WHERE CreditBillID = lngCreditBillID
		) CBH ON CBH.ContactID = CCI.CustomerID
		SET CCI.LastBillingDate = dteBillingDate;
	END IF;

	-- UPDATE tblCreditBillheader SET IsBillPrinted = 1 WHERE CurrentDueAmount = 0;
	/*******************************
		
		truncate table tbltransactions;
		truncate tbltransactionitems;
		truncate table tblcreditpayment;
		UPDATE tblContacts SET Credit = 0;
		UPDATE tblContactCreditCardInfo SET LastBillingDate = '0001-01-01';
		source C:\RetailPlus\RetailPlus\database\credit.sql

		CALL procProcessCreditBills();

		SELECT * FROM tblCreditBillHeader;
		SELECT * FROM tblCreditBillDetail;

	*******************************/
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
	DECLARE dteCreditCutOffDate DATE;
	DECLARE dteNextCreditCutOffDate DATE;

	SET bolCreditUseLastDayCutOffDate = (SELECT ConfigValue FROM sysCreditConfig WHERE ConfigName = 'CreditUseLastDayCutOffDate');
	SET dteCreditCutOffDate = (SELECT ConfigValue FROM sysCreditConfig WHERE ConfigName = 'CreditCutOffDate');
	SET dteNextCreditCutOffDate = (SELECT DATE_ADD(dteCreditCutOffDate, INTERVAL 1 MONTH));
	

	-- IF DAY(dteCreditCutOffDate) >= 28 then
	IF bolCreditUseLastDayCutOffDate = 1 OR ISNULL(bolCreditUseLastDayCutOffDate) THEN
		SET dteCreditCutOffDate = (SELECT LAST_DAY(dteCreditCutOffDate));
		SET dteNextCreditCutOffDate = (SELECT LAST_DAY(dteNextCreditCutOffDate));
	END IF;

	/** end-Update the header with the details *****/
	-- IF dteCurrDate >= dteCreditCutOffDate THEN
		UPDATE sysCreditConfig SET ConfigValue = DATE_ADD(ConfigValue, INTERVAL 1 MONTH) WHERE ConfigName = 'CreditPurcStartDateToProcess';
		UPDATE sysCreditConfig SET ConfigValue = DATE_ADD(ConfigValue, INTERVAL 1 MONTH) WHERE ConfigName = 'CreditPurcEndDateToProcess';
		UPDATE sysCreditConfig SET ConfigValue = dteNextCreditCutOffDate WHERE ConfigName = 'CreditCutOffDate';
		UPDATE sysCreditConfig SET ConfigValue = DATE_ADD(ConfigValue, INTERVAL 1 MONTH) WHERE ConfigName = 'BillingDate';
	-- END IF;


END;
GO
delimiter ;

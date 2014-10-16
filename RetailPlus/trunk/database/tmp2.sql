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
				WHERE TrxD.ProductID = (SELECT ProductID FROM tblProducts WHERE ProductCode = 'CREDIT PAYMENT' LIMIT 1)
					AND TransactionStatus = 1
					AND CONVERT(Trx.TransactionDate, DATE) BETWEEN dteStartCreditCutOffDate AND dteEndCreditCutOffDate
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

		-- check
		IF (boDebug = 1) THEN
			SELECT 'done updating the header with the details ' AS Step ;

			SELECT * FROM tblCreditBillHeader WHERE CreditBillHeaderID IN (SELECT CreditBillHeaderID FROM tblCreditBillDetail) LIMIT 10;
		END IF;

		-- insert the late payment Charges FOR bills that did not pay for the 1 month.
		INSERT INTO tblCreditBillDetail(CreditBillHeaderID ,TransactionDate ,Description ,Amount ,TransactionTypeID ,TransactionRefID)
		SELECT CreditBillHeaderID ,dteBillingDate 
				,CONCAT('Late Payment Charge  : (', Prev2MoCurrentDueAmount + Prev1MoCurrentDueAmount,' * ',decCreditLatePenaltyCharge,')') 'Description'
				,((Prev2MoCurrentDueAmount + Prev1MoCurrentDueAmount) * decCreditLatePenaltyCharge) CurrMonthCreditAmt 
				,3 tblCreditPaymentTransactionTypeID ,0 TransactionRefID
		FROM tblCreditBillHeader CBH
		WHERE CBH.CreditBillID = lngCreditBillID
			AND (Prev2MoCurrentDueAmount + Prev1MoCurrentDueAmount) > 0 AND CurrMonthAmountPaid = 0;

		IF (boDebug = 1) THEN
			SELECT 'done inserting the late payment charges for bills that did not pay for the 1 month' AS Step ;

			SELECT * FROM tblCreditBillDetail WHERE Description LIKE 'Late Payment Charge %' LIMIT 10;

			SELECT CreditBillHeaderID ,dteBillingDate 
					,CONCAT('Late Payment Charge  : (', Prev2MoCurrentDueAmount + Prev1MoCurrentDueAmount,' * ',decCreditLatePenaltyCharge,')') 'Description'
					,((Prev2MoCurrentDueAmount + Prev1MoCurrentDueAmount) * decCreditLatePenaltyCharge) CurrMonthCreditAmt 
					,3 tblCreditPaymentTransactionTypeID ,0 TransactionRefID
			FROM tblCreditBillHeader CBH
			WHERE CBH.CreditBillID = lngCreditBillID
				AND (Prev2MoCurrentDueAmount + Prev1MoCurrentDueAmount) > 0 AND CurrMonthAmountPaid = 0;
		END IF;

		-- update the credit of contact
		UPDATE tblContacts AS CON
		INNER JOIN
		(
			SELECT ContactID ,CurrentDueAmount, CurrMonthCreditAmt FROM tblCreditBillHeader WHERE CreditBillID = lngCreditBillID
		) CBH ON CBH.ContactID = CON.ContactID
		SET CON.Credit = CurrentDueAmount + CurrMonthCreditAmt;

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
		INSERT INTO tblCreditPayment(TransactionID, ContactID, CreditCardTypeID, 
									CreditBefore, Amount, CreditAfter,  
									CreditReason, CreditDate, 
									TerminalNo, CashierName, TransactionNo)
						SELECT TRX.TransactionID ,CBH.ContactID , CCI.CreditCardTypeID
									,CON.Credit - ((Prev2MoCurrentDueAmount + Prev1MoCurrentDueAmount) * decCreditLatePenaltyCharge)
									,(Prev2MoCurrentDueAmount + Prev1MoCurrentDueAmount) * decCreditLatePenaltyCharge
									,CON.Credit
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

		SELECT EndingTransactionNo, LPAD(EndingTransactionNo+1, LENGTH(BeginningTransactionNo), '0')  NewEndingTransactionNo 
		-- INTO EndingTransactionNo, NewEndingTransactionNo
		FROM tblTerminalReport WHERE BranchID = 1 AND TerminalNo = '01';
		
		SELECT EndingTransactionNo, NewEndingTransactionNo;

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

		
		INSERT INTO tblCreditPayment(TransactionID, ContactID, CreditCardTypeID, 
									CreditBefore, Amount, CreditAfter, 
									CreditReason, CreditDate, 
									TerminalNo, CashierName, TransactionNo)
						SELECT TransactionID ,CBH.ContactID , CCI.CreditCardTypeID
									,CON.Credit - ((Prev2MoCurrentDueAmount + Prev1MoCurrentDueAmount) * decCreditFinanceCharge)
									,(Prev2MoCurrentDueAmount + Prev1MoCurrentDueAmount) * decCreditFinanceCharge
									,CON.Credit
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
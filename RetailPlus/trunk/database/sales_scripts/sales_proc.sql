 /*************************************************************************************************************

procSOSynchronizeAmount
Lemuel E. Aceron

SELECT decSubTotalDiscountableAmount, decAmount, decDiscount;
SELECT Subtotal, discount, TotalItemDiscount, vat , VATAbleAmount, evat , eVATAbleAmount, localtax from tblpo where SOID = lngSOID;
SELECT decSubTotalDiscountableAmount, decTotalItemDiscount, decSODiscount, decSODiscountApplied, intSODiscountType, decTotalVAT, decTotalVATableAmount;

*************************************************************************************************************/

DROP PROCEDURE IF EXISTS procSOSynchronizeAmount;
delimiter GO

create procedure procSOSynchronizeAmount(IN lngSOID bigint(20))
BEGIN
	DECLARE decTotalItemAmount, decTotalItemDiscount, decTotalVAT, decTotalVATableAmount, decTotalEVAT, decTotalEVATableAmount, decTotalLocalTax  DECIMAL(10,2) DEFAULT 0;
	DECLARE decAmount, decDiscount DECIMAL(10,2) DEFAULT 0;
	DECLARE intSODiscountType INT DEFAULT 0;
	DECLARE decSubTotalDiscountableAmount, decSODiscount, decSODiscountApplied DECIMAL(10,2) DEFAULT 0;
	
	DECLARE done INT DEFAULT 0;
	DECLARE curItems CURSOR FOR SELECT Amount, Discount FROM tblSOItems WHERE SOID = lngSOID;
	DECLARE CONTINUE HANDLER FOR NOT FOUND SET done = 1;
	
	SET decTotalItemAmount = (SELECT SUM(Amount) FROM tblSOItems WHERE SOID = lngSOID);
	SET decTotalItemDiscount = (SELECT SUM(Discount) FROM tblSOItems WHERE SOID = lngSOID AND Discount <> 0);
	
	SET decSODiscountApplied = (SELECT DiscountApplied FROM tblSO WHERE SOID = lngSOID);
	set decTotalItemDiscount = 0;
	
	OPEN curItems;
	REPEAT
		FETCH curItems INTO decAmount, decDiscount;
		
		IF NOT done THEN
			IF decDiscount=0 THEN
				SET decSubTotalDiscountableAmount = (SELECT decSubTotalDiscountableAmount + decAmount);
			ELSE
				SET decTotalItemDiscount = (SELECT decTotalItemDiscount + decDiscount);
			END IF;
			
		END IF;
	UNTIL done END REPEAT;
	CLOSE curItems;
	
	SET decSODiscountApplied = (SELECT DiscountApplied FROM tblSO WHERE SOID = lngSOID);
	SET intSODiscountType = (SELECT DiscountType FROM tblSO WHERE SOID = lngSOID);

	IF intSODiscountType = 1 and decTotalItemAmount >= decSODiscountApplied THEN
		SET decSODiscount = (SELECT decSODiscountApplied);
	ELSEIF intSODiscountType = 2 THEN
		SET decSODiscount = (SELECT (decSubTotalDiscountableAmount * (decSODiscountApplied / 100)));
	END IF;
	
	SET decTotalVATableAmount = (SELECT (decTotalItemAmount - decSODiscount) / (1+(SELECT VAT/100 FROM tblTerminal WHERE TerminalID = 1))) ;
	SET decTotalVAT = (SELECT decTotalVATableAmount * (SELECT VAT/100 FROM tblTerminal WHERE TerminalID = 1));
	SET decTotalEVATableAmount = (SELECT (decTotalItemAmount - decSODiscount) / (1+(SELECT VAT/100 FROM tblTerminal WHERE TerminalID = 1))) ;
	SET decTotalEVAT = (SELECT decTotalEVATableAmount * (SELECT EVAT/100 FROM tblTerminal WHERE TerminalID = 1));
	SET decTotalLocalTax = (SELECT decTotalVATableAmount * (SELECT LocalTax/100 FROM tblTerminal WHERE TerminalID = 1));
	
	UPDATE tblSO SET
		SubTotal = decTotalItemAmount - decSODiscount + Freight - Deposit,
		Discount = decSODiscount,
		TotalItemDiscount = decTotalItemDiscount,
		VAT = decTotalVAT,
		VATAbleAmount = decTotalVATableAmount,
		EVAT = decTotalEVAT,
		EVATAbleAmount = decTotalEVATableAmount,
		LocalTax = decTotalLocalTax,
		UnpaidAmount = decTotalItemAmount - decSODiscount + Freight - Deposit
	WHERE SOID = lngSOID;
	
END;
GO
delimiter ;

 /*************************************************************************************************************

procSOUpdateFreight
Lemuel E. Aceron

*************************************************************************************************************/

DROP PROCEDURE IF EXISTS procSOUpdateFreight;
delimiter GO

create procedure procSOUpdateFreight(IN lngSOID bigint(20), IN decFreight DECIMAL(10,2))
BEGIN
	
	UPDATE tblSO SET
		Freight	= decFreight 
	WHERE SOID	= lngSOID;
	
END;
GO
delimiter ;

/*************************************************************************************************************

procSOCreditMemoSynchronizeAmount
Lemuel E. Aceron

*************************************************************************************************************/
delimiter GO
DROP PROCEDURE IF EXISTS procSOCreditMemoSynchronizeAmount
GO

create procedure procSOCreditMemoSynchronizeAmount(IN lngCreditMemoID bigint(20))
BEGIN
	DECLARE decTotalItemAmount, decTotalItemDiscount, decTotalVAT, decTotalVATableAmount, decTotalEVAT, decTotalEVATableAmount, decTotalLocalTax  DECIMAL(10,2) DEFAULT 0;
	DECLARE decAmount, decDiscount DECIMAL(10,2) DEFAULT 0;
	DECLARE intSOCreditMemoDiscountType INT DEFAULT 0;
	DECLARE decSubTotalDiscountableAmount, decSOCreditMemoDiscount, decSOCreditMemoDiscountApplied DECIMAL(10,2) DEFAULT 0;
	
	DECLARE done INT DEFAULT 0;
	DECLARE curItems CURSOR FOR SELECT Amount, Discount FROM tblSOCreditMemoItems WHERE CreditMemoID = lngCreditMemoID;
	DECLARE CONTINUE HANDLER FOR NOT FOUND SET done = 1;
	
	SET decTotalItemAmount = (SELECT SUM(Amount) FROM tblSOCreditMemoItems WHERE CreditMemoID = lngCreditMemoID);
	SET decTotalItemDiscount = (SELECT SUM(Discount) FROM tblSOCreditMemoItems WHERE CreditMemoID = lngCreditMemoID AND Discount <> 0);
	
	SET decSOCreditMemoDiscountApplied = (SELECT DiscountApplied FROM tblSOCreditMemo WHERE CreditMemoID = lngCreditMemoID);
	set decTotalItemDiscount = 0;
	
	OPEN curItems;
	REPEAT
		FETCH curItems INTO decAmount, decDiscount;
		
		IF NOT done THEN
			IF decDiscount=0 THEN
				SET decSubTotalDiscountableAmount = (SELECT decSubTotalDiscountableAmount + decAmount);
			ELSE
				SET decTotalItemDiscount = (SELECT decTotalItemDiscount + decDiscount);
			END IF;
			
		END IF;
	UNTIL done END REPEAT;
	CLOSE curItems;
	
	SET decSOCreditMemoDiscountApplied = (SELECT DiscountApplied FROM tblSOCreditMemo WHERE CreditMemoID = lngCreditMemoID);
	SET intSOCreditMemoDiscountType = (SELECT DiscountType FROM tblSOCreditMemo WHERE CreditMemoID = lngCreditMemoID);

	IF intSOCreditMemoDiscountType = 1 and decTotalItemAmount >= decSOCreditMemoDiscountApplied THEN
		SET decSOCreditMemoDiscount = (SELECT decSOCreditMemoDiscountApplied);
	ELSEIF intSOCreditMemoDiscountType = 2 THEN
		SET decSOCreditMemoDiscount = (SELECT (decSubTotalDiscountableAmount * (decSOCreditMemoDiscountApplied / 100)));
	END IF;
	
	SET decTotalVATableAmount = (SELECT (decTotalItemAmount - decSOCreditMemoDiscount) / (1+(SELECT VAT/100 FROM tblTerminal WHERE TerminalID = 1))) ;
	SET decTotalVAT = (SELECT decTotalVATableAmount * (SELECT VAT/100 FROM tblTerminal WHERE TerminalID = 1));
	SET decTotalEVATableAmount = (SELECT (decTotalItemAmount - decSOCreditMemoDiscount) / (1+(SELECT VAT/100 FROM tblTerminal WHERE TerminalID = 1))) ;
	SET decTotalEVAT = (SELECT decTotalEVATableAmount * (SELECT EVAT/100 FROM tblTerminal WHERE TerminalID = 1));
	SET decTotalLocalTax = (SELECT decTotalVATableAmount * (SELECT LocalTax/100 FROM tblTerminal WHERE TerminalID = 1));
	
	UPDATE tblSOCreditMemo SET
		SubTotal = decTotalItemAmount - decSOCreditMemoDiscount + Freight - Deposit,
		Discount = decSOCreditMemoDiscount,
		TotalItemDiscount = decTotalItemDiscount,
		VAT = decTotalVAT,
		VATAbleAmount = decTotalVATableAmount,
		EVAT = decTotalEVAT,
		EVATAbleAmount = decTotalEVATableAmount,
		LocalTax = decTotalLocalTax,
		UnpaidAmount = decTotalItemAmount - decSOCreditMemoDiscount + Freight - Deposit
	WHERE CreditMemoID = lngCreditMemoID;
	
END;
GO
delimiter ;
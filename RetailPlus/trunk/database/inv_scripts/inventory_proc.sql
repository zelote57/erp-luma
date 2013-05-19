/*************************************************************************************************************

procPOSynchronizeAmount
Lemuel E. Aceron

SELECT decSubTotalDiscountableAmount;
SELECT Subtotal, discount, TotalItemDiscount, vat , VATAbleAmount, evat , eVATAbleAmount, localtax from tblpo where poid = lngpoid;
SELECT decSubTotalDiscountableAmount, decTotalItemDiscount, decPODiscount, decPODiscountApplied, intPODiscountType, decTotalVAT, decTotalVATableAmount;

*************************************************************************************************************/

DROP PROCEDURE IF EXISTS procPOSynchronizeAmount;
delimiter GO

create procedure procPOSynchronizeAmount(IN lngPOID bigint(20))
BEGIN
	DECLARE decTotalItemAmount, decTotalItemDiscount, decTotalVAT, decTotalVATableAmount, decTotalEVAT, decTotalEVATableAmount, decTotalLocalTax  DECIMAL(18,3) DEFAULT 0;
	DECLARE intPODiscountType, intPODiscount2Type, intPODiscount3Type, intIsVatInclusive INT DEFAULT 0;
	DECLARE decSubTotalDiscountableAmount, decSubTotalDiscountable2Amount, decSubTotalDiscountable3Amount DECIMAL(18,3) DEFAULT 0;
	DECLARE decPODiscount, decPODiscount2, decPODiscount3 DECIMAL(18,3) DEFAULT 0;
	DECLARE decPODiscountApplied, decPODiscount2Applied, decPODiscount3Applied DECIMAL(18,3) DEFAULT 0;
	
	SELECT SUM(Amount), SUM(Discount) INTO decTotalItemAmount, decTotalItemDiscount FROM tblPOItems WHERE POID = lngPOID;
	-- SET decTotalItemAmount = (SELECT SUM(Amount) FROM tblPOItems WHERE POID = lngPOID);
	-- SET decTotalItemAmount = (SELECT IFNULL(SUM(Discount),0) FROM tblPOItems WHERE POID = lngPOID AND Discount <> 0);
	SET decSubTotalDiscountableAmount = decTotalItemAmount;
	
	SELECT IsVatInclusive, DiscountApplied, Discount2Applied, Discount3Applied, DiscountType, Discount2Type, Discount3Type 
	INTO intIsVatInclusive, decPODiscountApplied, decPODiscount2Applied, decPODiscount3Applied, intPODiscountType, intPODiscount2Type, intPODiscount3Type
	FROM tblPO WHERE POID = lngPOID;
	-- SET intIsVatInclusive = (SELECT IsVatInclusive FROM tblPO WHERE POID = lngPOID);
	-- SET decPODiscountApplied  = (SELECT DiscountApplied FROM tblPO WHERE POID = lngPOID);
	-- SET decPODiscount2Applied = (SELECT Discount2Applied FROM tblPO WHERE POID = lngPOID); 
	-- SET decPODiscount3Applied = (SELECT Discount3Applied FROM tblPO WHERE POID = lngPOID);
	-- SET intPODiscountType	  = (SELECT DiscountType FROM tblPO WHERE POID = lngPOID);
	-- SET intPODiscount2Type	  = (SELECT Discount2Type FROM tblPO WHERE POID = lngPOID);
	-- SET intPODiscount3Type	  = (SELECT Discount3Type FROM tblPO WHERE POID = lngPOID);
	
	IF intPODiscountType = 1 and decSubTotalDiscountableAmount >= decPODiscountApplied THEN
		SET decPODiscount = (SELECT decPODiscountApplied);
	ELSEIF intPODiscountType = 2 THEN
		SET decPODiscount = (SELECT (decSubTotalDiscountableAmount * (decPODiscountApplied / 100)));
	END IF;
	
	SET decSubTotalDiscountableAmount = decSubTotalDiscountableAmount - decPODiscount;
	IF intPODiscount2Type = 1 and decSubTotalDiscountableAmount >= decPODiscount2Applied THEN
		SET decPODiscount2 = (SELECT decPODiscount2Applied);
	ELSEIF intPODiscount2Type = 2 THEN
		SET decPODiscount2 = (SELECT (decSubTotalDiscountableAmount * (decPODiscount2Applied / 100)));
	END IF;
	
	SET decSubTotalDiscountableAmount = decSubTotalDiscountableAmount - decPODiscount2;
	IF intPODiscount3Type = 1 and decSubTotalDiscountableAmount >= decPODiscount3Applied THEN
		SET decPODiscount3 = (SELECT decPODiscount3Applied);
	ELSEIF intPODiscount3Type = 2 THEN
		SET decPODiscount3 = (SELECT (decSubTotalDiscountableAmount * (decPODiscount3Applied / 100)));
	END IF;
	SET decSubTotalDiscountableAmount = decSubTotalDiscountableAmount - decPODiscount3;
	
	IF (intIsVatInclusive = 0) THEN
		SET decSubTotalDiscountableAmount = decSubTotalDiscountableAmount * (1+(SELECT VAT/100 FROM tblTerminal WHERE TerminalID = 1));
	END IF;
	
	SET decTotalVATableAmount = (SELECT decSubTotalDiscountableAmount / (1+(SELECT VAT/100 FROM tblTerminal WHERE TerminalID = 1))) ;
	SET decTotalVAT = (SELECT decTotalVATableAmount * (SELECT VAT/100 FROM tblTerminal WHERE TerminalID = 1));
	SET decTotalEVATableAmount = (SELECT decSubTotalDiscountableAmount / (1+(SELECT VAT/100 FROM tblTerminal WHERE TerminalID = 1))) ;
	SET decTotalEVAT = (SELECT decTotalEVATableAmount * (SELECT EVAT/100 FROM tblTerminal WHERE TerminalID = 1));
	SET decTotalLocalTax = (SELECT decTotalVATableAmount * (SELECT LocalTax/100 FROM tblTerminal WHERE TerminalID = 1));
	
	UPDATE tblPO SET
		SubTotal = decTotalItemAmount - decPODiscount - decPODiscount2 - decPODiscount3 + Freight - Deposit,
		Discount = decPODiscount,
		Discount2 = decPODiscount2,
		Discount3 = decPODiscount3,
		TotalItemDiscount = decTotalItemDiscount,
		VAT = decTotalVAT,
		VATAbleAmount = decTotalVATableAmount,
		EVAT = decTotalEVAT,
		EVATAbleAmount = decTotalEVATableAmount,
		LocalTax = decTotalLocalTax,
		UnpaidAmount = decTotalItemAmount - decPODiscount - decPODiscount2 - decPODiscount3 + Freight - Deposit
	WHERE POID = lngPOID;
	
END;
GO
delimiter ;

/*************************************************************************************************************

procPODebitMemoSynchronizeAmount
Lemuel E. Aceron

SELECT decSubTotalDiscountableAmount;
SELECT Subtotal, discount, TotalItemDiscount, vat , VATAbleAmount, evat , eVATAbleAmount, localtax from tblpo where poid = lngpoid;
SELECT decSubTotalDiscountableAmount, decTotalItemDiscount, decPODebitMemoDiscount, decPODebitMemoDiscountApplied, intPODebitMemoDiscountType, decTotalVAT, decTotalVATableAmount;

*************************************************************************************************************/

DROP PROCEDURE IF EXISTS procPODebitMemoSynchronizeAmount;
delimiter GO

create procedure procPODebitMemoSynchronizeAmount(IN lngDebitMemoID bigint(20))
BEGIN
	DECLARE decTotalItemAmount, decTotalItemDiscount, decTotalVAT, decTotalVATableAmount, decTotalEVAT, decTotalEVATableAmount, decTotalLocalTax  DECIMAL(18,3) DEFAULT 0;
	DECLARE intPODebitMemoDiscountType, intPODebitMemoDiscount2Type, intPODebitMemoDiscount3Type, intIsVatInclusive INT DEFAULT 0;
	DECLARE decSubTotalDiscountableAmount, decSubTotalDiscountable2Amount, decSubTotalDiscountable3Amount DECIMAL(18,3) DEFAULT 0;
	DECLARE decPODebitMemoDiscount, decPODebitMemoDiscount2, decPODebitMemoDiscount3 DECIMAL(18,3) DEFAULT 0;
	DECLARE decPODebitMemoDiscountApplied, decPODebitMemoDiscount2Applied, decPODebitMemoDiscount3Applied DECIMAL(18,3) DEFAULT 0;
	
	SELECT SUM(Amount), SUM(Discount) INTO decTotalItemAmount, decTotalItemDiscount FROM tblPODebitMemoItems WHERE DebitMemoID = lngDebitMemoID;
	SET decSubTotalDiscountableAmount = decTotalItemAmount;
	
	SELECT IsVatInclusive, DiscountApplied, Discount2Applied, Discount3Applied, DiscountType, Discount2Type, Discount3Type 
	INTO intIsVatInclusive, decPODebitMemoDiscountApplied, decPODebitMemoDiscount2Applied, decPODebitMemoDiscount3Applied, intPODebitMemoDiscountType, intPODebitMemoDiscount2Type, intPODebitMemoDiscount3Type
	FROM tblPODebitMemo WHERE DebitMemoID = lngDebitMemoID;
	
	IF intPODebitMemoDiscountType = 1 and decSubTotalDiscountableAmount >= decPODebitMemoDiscountApplied THEN
		SET decPODebitMemoDiscount = (SELECT decPODebitMemoDiscountApplied);
	ELSEIF intPODebitMemoDiscountType = 2 THEN
		SET decPODebitMemoDiscount = (SELECT (decSubTotalDiscountableAmount * (decPODebitMemoDiscountApplied / 100)));
	END IF;
	
	SET decSubTotalDiscountableAmount = decSubTotalDiscountableAmount - decPODebitMemoDiscount;
	IF intPODebitMemoDiscount2Type = 1 and decSubTotalDiscountableAmount >= decPODebitMemoDiscount2Applied THEN
		SET decPODebitMemoDiscount2 = (SELECT decPODebitMemoDiscount2Applied);
	ELSEIF intPODebitMemoDiscount2Type = 2 THEN
		SET decPODebitMemoDiscount2 = (SELECT (decSubTotalDiscountableAmount * (decPODebitMemoDiscount2Applied / 100)));
	END IF;
	
	SET decSubTotalDiscountableAmount = decSubTotalDiscountableAmount - decPODebitMemoDiscount2;
	IF intPODebitMemoDiscount3Type = 1 and decSubTotalDiscountableAmount >= decPODebitMemoDiscount3Applied THEN
		SET decPODebitMemoDiscount3 = (SELECT decPODebitMemoDiscount3Applied);
	ELSEIF intPODebitMemoDiscount3Type = 2 THEN
		SET decPODebitMemoDiscount3 = (SELECT (decSubTotalDiscountableAmount * (decPODebitMemoDiscount3Applied / 100)));
	END IF;
	SET decSubTotalDiscountableAmount = decSubTotalDiscountableAmount - decPODebitMemoDiscount3;
	
	IF (intIsVatInclusive = 0) THEN
		SET decSubTotalDiscountableAmount = decSubTotalDiscountableAmount * (1+(SELECT VAT/100 FROM tblTerminal WHERE TerminalID = 1));
	END IF;
	
	SET decTotalVATableAmount = (SELECT decSubTotalDiscountableAmount / (1+(SELECT VAT/100 FROM tblTerminal WHERE TerminalID = 1))) ;
	SET decTotalVAT = (SELECT decTotalVATableAmount * (SELECT VAT/100 FROM tblTerminal WHERE TerminalID = 1));
	SET decTotalEVATableAmount = (SELECT decSubTotalDiscountableAmount / (1+(SELECT VAT/100 FROM tblTerminal WHERE TerminalID = 1))) ;
	SET decTotalEVAT = (SELECT decTotalEVATableAmount * (SELECT EVAT/100 FROM tblTerminal WHERE TerminalID = 1));
	SET decTotalLocalTax = (SELECT decTotalVATableAmount * (SELECT LocalTax/100 FROM tblTerminal WHERE TerminalID = 1));
	
	UPDATE tblPODebitMemo SET
		SubTotal = decTotalItemAmount - decPODebitMemoDiscount - decPODebitMemoDiscount2 - decPODebitMemoDiscount3 + Freight - Deposit,
		Discount = decPODebitMemoDiscount,
		Discount2 = decPODebitMemoDiscount2,
		Discount3 = decPODebitMemoDiscount3,
		TotalItemDiscount = decTotalItemDiscount,
		VAT = decTotalVAT,
		VATAbleAmount = decTotalVATableAmount,
		EVAT = decTotalEVAT,
		EVATAbleAmount = decTotalEVATableAmount,
		LocalTax = decTotalLocalTax,
		UnpaidAmount = decTotalItemAmount - decPODebitMemoDiscount - decPODebitMemoDiscount2 - decPODebitMemoDiscount3 + Freight - Deposit
	WHERE DebitMemoID = lngDebitMemoID;
	
END;
GO
delimiter ;

/*******************************************************************

procTransferInSynchronizeAmount
Lemuel E. Aceron
April 29, 2009

********************************************************************/

DROP PROCEDURE IF EXISTS procTransferInSynchronizeAmount;
delimiter GO

create procedure procTransferInSynchronizeAmount(IN lngTransferInID bigint(20))
BEGIN
	DECLARE decTotalItemAmount, decTotalItemDiscount, decTotalVAT, decTotalVATableAmount, decTotalEVAT, decTotalEVATableAmount, decTotalLocalTax  DECIMAL(18,3) DEFAULT 0;
	DECLARE intTransferInDiscountType, intTransferInDiscount2Type, intTransferInDiscount3Type, intIsVatInclusive INT DEFAULT 0;
	DECLARE decSubTotalDiscountableAmount, decSubTotalDiscountable2Amount, decSubTotalDiscountable3Amount DECIMAL(18,3) DEFAULT 0;
	DECLARE decTransferInDiscount, decTransferInDiscount2, decTransferInDiscount3 DECIMAL(18,3) DEFAULT 0;
	DECLARE decTransferInDiscountApplied, decTransferInDiscount2Applied, decTransferInDiscount3Applied DECIMAL(18,3) DEFAULT 0;
	
	SELECT SUM(Amount), SUM(Discount) INTO decTotalItemAmount, decTotalItemDiscount FROM tblTransferInItems WHERE TransferInID = lngTransferInID;
	-- SET decTotalItemAmount = (SELECT SUM(Amount) FROM tblTransferInItems WHERE TransferInID = lngTransferInID);
	-- SET decTotalItemDiscount = (SELECT IFNULL(SUM(Discount),0) FROM tblTransferInItems WHERE TransferInID = lngTransferInID AND Discount <> 0);
	SET decSubTotalDiscountableAmount = decTotalItemAmount;
	
	SELECT IsVatInclusive, DiscountApplied, Discount2Applied, Discount3Applied, DiscountType, Discount2Type, Discount3Type 
	INTO intIsVatInclusive, decTransferInDiscountApplied, decTransferInDiscount2Applied, decTransferInDiscount3Applied, intTransferInDiscountType, intTransferInDiscount2Type, intTransferInDiscount3Type
	FROM tblTransferIn WHERE TransferInID = lngTransferInID;
	-- SET intIsVatInclusive = (SELECT IsVatInclusive FROM tblTransferIn WHERE TransferInID = lngTransferInID);
	-- SET decTransferInDiscountApplied  = (SELECT DiscountApplied FROM tblTransferIn WHERE TransferInID = lngTransferInID);
	-- SET decTransferInDiscount2Applied = (SELECT Discount2Applied FROM tblTransferIn WHERE TransferInID = lngTransferInID); 
	-- SET decTransferInDiscount3Applied = (SELECT Discount3Applied FROM tblTransferIn WHERE TransferInID = lngTransferInID);
	-- SET intTransferInDiscountType	  = (SELECT DiscountType FROM tblTransferIn WHERE TransferInID = lngTransferInID);
	-- SET intTransferInDiscount2Type	  = (SELECT Discount2Type FROM tblTransferIn WHERE TransferInID = lngTransferInID);
	-- SET intTransferInDiscount3Type	  = (SELECT Discount3Type FROM tblTransferIn WHERE TransferInID = lngTransferInID);
	
	IF intTransferInDiscountType = 1 and decSubTotalDiscountableAmount >= decTransferInDiscountApplied THEN
		SET decTransferInDiscount = (SELECT decTransferInDiscountApplied);
	ELSEIF intTransferInDiscountType = 2 THEN
		SET decTransferInDiscount = (SELECT (decSubTotalDiscountableAmount * (decTransferInDiscountApplied / 100)));
	END IF;
	
	SET decSubTotalDiscountableAmount = decSubTotalDiscountableAmount - decTransferInDiscount;
	IF intTransferInDiscount2Type = 1 and decSubTotalDiscountableAmount >= decTransferInDiscount2Applied THEN
		SET decTransferInDiscount2 = (SELECT decTransferInDiscount2Applied);
	ELSEIF intTransferInDiscount2Type = 2 THEN
		SET decTransferInDiscount2 = (SELECT (decSubTotalDiscountableAmount * (decTransferInDiscount2Applied / 100)));
	END IF;
	
	SET decSubTotalDiscountableAmount = decSubTotalDiscountableAmount - decTransferInDiscount2;
	IF intTransferInDiscount3Type = 1 and decSubTotalDiscountableAmount >= decTransferInDiscount3Applied THEN
		SET decTransferInDiscount3 = (SELECT decTransferInDiscount3Applied);
	ELSEIF intTransferInDiscount3Type = 2 THEN
		SET decTransferInDiscount3 = (SELECT (decSubTotalDiscountableAmount * (decTransferInDiscount3Applied / 100)));
	END IF;
	SET decSubTotalDiscountableAmount = decSubTotalDiscountableAmount - decTransferInDiscount3;
	
	IF (intIsVatInclusive = 0) THEN
		SET decSubTotalDiscountableAmount = decSubTotalDiscountableAmount * (1+(SELECT VAT/100 FROM tblTerminal WHERE TerminalID = 1));
	END IF;
	
	SET decTotalVATableAmount = (SELECT decSubTotalDiscountableAmount / (1+(SELECT VAT/100 FROM tblTerminal WHERE TerminalID = 1))) ;
	SET decTotalVAT = (SELECT decTotalVATableAmount * (SELECT VAT/100 FROM tblTerminal WHERE TerminalID = 1));
	SET decTotalEVATableAmount = (SELECT decSubTotalDiscountableAmount / (1+(SELECT VAT/100 FROM tblTerminal WHERE TerminalID = 1))) ;
	SET decTotalEVAT = (SELECT decTotalEVATableAmount * (SELECT EVAT/100 FROM tblTerminal WHERE TerminalID = 1));
	SET decTotalLocalTax = (SELECT decTotalVATableAmount * (SELECT LocalTax/100 FROM tblTerminal WHERE TerminalID = 1));
	
	UPDATE tblTransferIn SET
		SubTotal = decTotalItemAmount - decTransferInDiscount - decTransferInDiscount2 - decTransferInDiscount3 + Freight - Deposit,
		Discount = decTransferInDiscount,
		Discount2 = decTransferInDiscount2,
		Discount3 = decTransferInDiscount3,
		TotalItemDiscount = decTotalItemDiscount,
		VAT = decTotalVAT,
		VATAbleAmount = decTotalVATableAmount,
		EVAT = decTotalEVAT,
		EVATAbleAmount = decTotalEVATableAmount,
		LocalTax = decTotalLocalTax,
		UnpaidAmount = decTotalItemAmount - decTransferInDiscount - decTransferInDiscount2 - decTransferInDiscount3 + Freight - Deposit
	WHERE TransferInID = lngTransferInID;
	
END;
GO
delimiter ;

/*******************************************************************

procTransferOutSynchronizeAmount
Lemuel E. Aceron
April 29, 2009

********************************************************************/

DROP PROCEDURE IF EXISTS procTransferOutSynchronizeAmount;
delimiter GO

create procedure procTransferOutSynchronizeAmount(IN lngTransferOutID bigint(20))
BEGIN
	DECLARE decTotalItemAmount, decTotalItemDiscount, decTotalVAT, decTotalVATableAmount, decTotalEVAT, decTotalEVATableAmount, decTotalLocalTax  DECIMAL(18,3) DEFAULT 0;
	DECLARE intTransferOutDiscountType, intTransferOutDiscount2Type, intTransferOutDiscount3Type, intIsVatInclusive INT DEFAULT 0;
	DECLARE decSubTotalDiscountableAmount, decSubTotalDiscountable2Amount, decSubTotalDiscountable3Amount DECIMAL(18,3) DEFAULT 0;
	DECLARE decTransferOutDiscount, decTransferOutDiscount2, decTransferOutDiscount3 DECIMAL(18,3) DEFAULT 0;
	DECLARE decTransferOutDiscountApplied, decTransferOutDiscount2Applied, decTransferOutDiscount3Applied DECIMAL(18,3) DEFAULT 0;
	
	SELECT SUM(Amount), SUM(Discount) INTO decTotalItemAmount, decTotalItemDiscount FROM tblTransferOutItems WHERE TransferOutID = lngTransferOutID;
	SET decSubTotalDiscountableAmount = decTotalItemAmount;
	
	SELECT IsVatInclusive, DiscountApplied, Discount2Applied, Discount3Applied, DiscountType, Discount2Type, Discount3Type 
	INTO intIsVatInclusive, decTransferOutDiscountApplied, decTransferOutDiscount2Applied, decTransferOutDiscount3Applied, intTransferOutDiscountType, intTransferOutDiscount2Type, intTransferOutDiscount3Type
	FROM tblTransferOut WHERE TransferOutID = lngTransferOutID;
	
	IF intTransferOutDiscountType = 1 and decSubTotalDiscountableAmount >= decTransferOutDiscountApplied THEN
		SET decTransferOutDiscount = (SELECT decTransferOutDiscountApplied);
	ELSEIF intTransferOutDiscountType = 2 THEN
		SET decTransferOutDiscount = (SELECT (decSubTotalDiscountableAmount * (decTransferOutDiscountApplied / 100)));
	END IF;
	
	SET decSubTotalDiscountableAmount = decSubTotalDiscountableAmount - decTransferOutDiscount;
	IF intTransferOutDiscount2Type = 1 and decSubTotalDiscountableAmount >= decTransferOutDiscount2Applied THEN
		SET decTransferOutDiscount2 = (SELECT decTransferOutDiscount2Applied);
	ELSEIF intTransferOutDiscount2Type = 2 THEN
		SET decTransferOutDiscount2 = (SELECT (decSubTotalDiscountableAmount * (decTransferOutDiscount2Applied / 100)));
	END IF;
	
	SET decSubTotalDiscountableAmount = decSubTotalDiscountableAmount - decTransferOutDiscount2;
	IF intTransferOutDiscount3Type = 1 and decSubTotalDiscountableAmount >= decTransferOutDiscount3Applied THEN
		SET decTransferOutDiscount3 = (SELECT decTransferOutDiscount3Applied);
	ELSEIF intTransferOutDiscount3Type = 2 THEN
		SET decTransferOutDiscount3 = (SELECT (decSubTotalDiscountableAmount * (decTransferOutDiscount3Applied / 100)));
	END IF;
	SET decSubTotalDiscountableAmount = decSubTotalDiscountableAmount - decTransferOutDiscount3;
	
	IF (intIsVatInclusive = 0) THEN
		SET decSubTotalDiscountableAmount = decSubTotalDiscountableAmount * (1+(SELECT VAT/100 FROM tblTerminal WHERE TerminalID = 1));
	END IF;
	
	SET decTotalVATableAmount = (SELECT decSubTotalDiscountableAmount / (1+(SELECT VAT/100 FROM tblTerminal WHERE TerminalID = 1))) ;
	SET decTotalVAT = (SELECT decTotalVATableAmount * (SELECT VAT/100 FROM tblTerminal WHERE TerminalID = 1));
	SET decTotalEVATableAmount = (SELECT decSubTotalDiscountableAmount / (1+(SELECT VAT/100 FROM tblTerminal WHERE TerminalID = 1))) ;
	SET decTotalEVAT = (SELECT decTotalEVATableAmount * (SELECT EVAT/100 FROM tblTerminal WHERE TerminalID = 1));
	SET decTotalLocalTax = (SELECT decTotalVATableAmount * (SELECT LocalTax/100 FROM tblTerminal WHERE TerminalID = 1));
	
	UPDATE tblTransferOut SET
		SubTotal = decTotalItemAmount - decTransferOutDiscount - decTransferOutDiscount2 - decTransferOutDiscount3 + Freight - Deposit,
		Discount = decTransferOutDiscount,
		Discount2 = decTransferOutDiscount2,
		Discount3 = decTransferOutDiscount3,
		TotalItemDiscount = decTotalItemDiscount,
		VAT = decTotalVAT,
		VATAbleAmount = decTotalVATableAmount,
		EVAT = decTotalEVAT,
		EVATAbleAmount = decTotalEVATableAmount,
		LocalTax = decTotalLocalTax,
		UnpaidAmount = decTotalItemAmount - decTransferOutDiscount - decTransferOutDiscount2 - decTransferOutDiscount3 + Freight - Deposit
	WHERE TransferOutID = lngTransferOutID;
	
END;
GO
delimiter ;

/*******************************************************************

procInvAdjustmentSynchronizeAmount
Lemuel E. Aceron
June 28, 2009

********************************************************************/

DROP PROCEDURE IF EXISTS procInvAdjustmentSynchronizeAmount;
delimiter GO

create procedure procInvAdjustmentSynchronizeAmount(IN lngInvAdjustmentID bigint(20))
BEGIN
	DECLARE decTotalItemAmount, decTotalItemDiscount, decTotalVAT, decTotalVATableAmount, decTotalEVAT, decTotalEVATableAmount, decTotalLocalTax  DECIMAL(18,3) DEFAULT 0;
	DECLARE decAmount, decDiscount DECIMAL(18,3) DEFAULT 0;
	DECLARE intInvAdjustmentDiscountType INT DEFAULT 0;
	DECLARE decSubTotalDiscountableAmount, decInvAdjustmentDiscount, decInvAdjustmentDiscountApplied DECIMAL(18,3) DEFAULT 0;
	
	DECLARE done INT DEFAULT 0;
	DECLARE curItems CURSOR FOR SELECT Amount, Discount FROM tblInvAdjustmentItems WHERE InvAdjustmentID = lngInvAdjustmentID;
	DECLARE CONTINUE HANDLER FOR NOT FOUND SET done = 1;
	
	SET decTotalItemAmount = (SELECT SUM(Amount) FROM tblInvAdjustmentItems WHERE InvAdjustmentID = lngInvAdjustmentID);
	SET decTotalItemDiscount = (SELECT SUM(Discount) FROM tblInvAdjustmentItems WHERE InvAdjustmentID = lngInvAdjustmentID AND Discount <> 0);
	
	SET decInvAdjustmentDiscountApplied = (SELECT DiscountApplied FROM tblInvAdjustment WHERE InvAdjustmentID = lngInvAdjustmentID);
	set decTotalItemDiscount = 0;
	
	OPEN curItems;
	REPEAT
		FETCH curItems INTO decAmount, decDiscount;
		
		IF NOT done THEN
			IF decDiscount<>0 THEN
				SET decTotalItemDiscount = (SELECT decTotalItemDiscount + decDiscount);
			END IF;
			SET decSubTotalDiscountableAmount = (SELECT decSubTotalDiscountableAmount + decAmount);
		END IF;
	UNTIL done END REPEAT;
	CLOSE curItems;
	
	SET decInvAdjustmentDiscountApplied = (SELECT DiscountApplied FROM tblInvAdjustment WHERE InvAdjustmentID = lngInvAdjustmentID);
	SET intInvAdjustmentDiscountType = (SELECT DiscountType FROM tblInvAdjustment WHERE InvAdjustmentID = lngInvAdjustmentID);

	IF intInvAdjustmentDiscountType = 1 and decTotalItemAmount >= decInvAdjustmentDiscountApplied THEN
		SET decInvAdjustmentDiscount = (SELECT decInvAdjustmentDiscountApplied);
	ELSEIF intInvAdjustmentDiscountType = 2 THEN
		SET decInvAdjustmentDiscount = (SELECT (decSubTotalDiscountableAmount * (decInvAdjustmentDiscountApplied / 100)));
	END IF;
	
	SET decTotalVATableAmount = (SELECT (decTotalItemAmount - decInvAdjustmentDiscount) / (1+(SELECT VAT/100 FROM tblTerminal WHERE TerminalID = 1))) ;
	SET decTotalVAT = (SELECT decTotalVATableAmount * (SELECT VAT/100 FROM tblTerminal WHERE TerminalID = 1));
	SET decTotalEVATableAmount = (SELECT (decTotalItemAmount - decInvAdjustmentDiscount) / (1+(SELECT VAT/100 FROM tblTerminal WHERE TerminalID = 1))) ;
	SET decTotalEVAT = (SELECT decTotalEVATableAmount * (SELECT EVAT/100 FROM tblTerminal WHERE TerminalID = 1));
	SET decTotalLocalTax = (SELECT decTotalVATableAmount * (SELECT LocalTax/100 FROM tblTerminal WHERE TerminalID = 1));
	
	UPDATE tblInvAdjustment SET
		SubTotal = decTotalItemAmount - decInvAdjustmentDiscount + Freight - Deposit,
		Discount = decInvAdjustmentDiscount,
		TotalItemDiscount = decTotalItemDiscount,
		VAT = decTotalVAT,
		VATAbleAmount = decTotalVATableAmount,
		EVAT = decTotalEVAT,
		EVATAbleAmount = decTotalEVATableAmount,
		LocalTax = decTotalLocalTax,
		UnpaidAmount = decTotalItemAmount - decInvAdjustmentDiscount + Freight - Deposit
	WHERE InvAdjustmentID = lngInvAdjustmentID;
	
END;
GO
delimiter ;

/*********************************
	procInvAdjustmentInsert
	Lemuel E. Aceron
	CALL procInvAdjustmentInsert();
	
	July 6, 2009 - create this procedure
*********************************/
delimiter GO
DROP PROCEDURE IF EXISTS procInvAdjustmentInsert
GO

create procedure procInvAdjustmentInsert(
	IN pvtUID BIGINT(20),
	IN pvtInvAdjustmentDate DATETIME,
	IN pvtProductID BIGINT(20),
	IN pvtProductCode VARCHAR(30),
	IN pvtDescription VARCHAR(100),
	IN pvtVariationMatrixID BIGINT(20),
	IN pvtMatrixDescription VARCHAR(150),
	IN pvtUnitID BIGINT(20),
	IN pvtUnitCode VARCHAR(30),
	IN pvtQuantityBefore DECIMAL(18,3), 
	IN pvtQuantityNow DECIMAL(18,3),
	IN pvtMinThresholdBefore DECIMAL(18,3), 
	IN pvtMinThresholdNow DECIMAL(18,3), 
	IN pvtMaxThresholdBefore DECIMAL(18,3), 
	IN pvtMaxThresholdNow DECIMAL(18,3),
	IN pvtRemarks VARCHAR(100))
BEGIN

	INSERT INTO tblInvAdjustment(UID, InvAdjustmentDate, ProductID, ProductCode, Description, 
							VariationMatrixID, MatrixDescription, UnitID, UnitCode, 
							QuantityBefore, QuantityNow, MinThresholdBefore, MinThresholdNow, MaxThresholdBefore, MaxThresholdNow, Remarks)VALUES
								(pvtUID, pvtInvAdjustmentDate, pvtProductID, pvtProductCode, pvtDescription,
							pvtVariationMatrixID, pvtMatrixDescription, pvtUnitID, pvtUnitCode, 
							pvtQuantityBefore, pvtQuantityNow, pvtMinThresholdBefore, pvtMinThresholdNow, pvtMaxThresholdBefore, pvtMaxThresholdNow, pvtRemarks);
		
END;
GO
delimiter ;

/*********************************
	procProductUpdateInvDetails
	Lemuel E. Aceron
	CALL procProductUpdateInvDetails();
	
	July 9, 2009 - create this procedure
*********************************/
delimiter GO
DROP PROCEDURE IF EXISTS procProductUpdateInvDetails
GO

create procedure procProductUpdateInvDetails(
	IN pvtBranchID INT(4),
	IN pvtProductID BIGINT(20),
	IN pvtMatrixID BIGINT(20),
	IN pvtQuantityNow DECIMAL(18,3), 
	IN pvtMinThresholdNow DECIMAL(18,3), 
	IN pvtMaxThresholdNow DECIMAL(18,3),
	IN strRemarks VARCHAR(100),
	IN dteTransactionDate DateTime,
	IN strTransactionNo VARCHAR(100),
	IN pvtAdjustedBy VARCHAR(120))
BEGIN
	
	DECLARE strProductCode VARCHAR(30) DEFAULT '';
	DECLARE strProductDesc VARCHAR(50) DEFAULT '';
	DECLARE strUnitCode VARCHAR(5) DEFAULT '';
	DECLARE decProductQuantity, decMatrixQuantity DECIMAL(18,3) DEFAULT 0;

	-- Set the value of strProductCode, strProductDesc, decProductQuantity, strUnitCode
	SELECT ProductCode, ProductDesc, UnitCode, SUM(Quantity) INTO strProductCode, strProductDesc, strUnitCode, decProductQuantity
	FROM tblProducts a 
		INNER JOIN tblUnit b ON a.BaseUnitID = b.UnitID 
		INNER JOIN tblProductInventory c ON a.ProductID = c.ProductID
	WHERE ProductID = pvtProductID AND MatrixID = pvtMatrixID
	GROUP BY ProductCode, ProductDesc, UnitCode;
	
	-- Insert to product movement history
	CALL procProductMovementInsert(pvtProductID, strProductCode, strProductDesc, 0, '', 
									decProductQuantity, pvtQuantityNow - decProductQuantity, pvtQuantityNow, 0, 
									strUnitCode, strRemarks, dteTransactionDate, strTransactionNo, pvtAdjustedBy, pvtBranchID, pvtBranchID, 0);
	
	UPDATE tblProductInventory SET
		Quantity	= pvtQuantityNow,
		QuantityIN  = pvtQuantityNow +  QuantityOut
	WHERE ProductID = pvtProductID AND MatrixID = pvtMatrixID AND BranchID = pvtBranchID;
			
	UPDATE tblProducts SET
		MinThreshold= pvtMinThresholdNow,
		MaxThreshold= pvtmaxThresholdNow
	WHERE ProductID = pvtproductID;

	UPDATE tblProducts SET
		MinThreshold= pvtMinThresholdNow,
		MaxThreshold= pvtmaxThresholdNow
	WHERE ProductID = pvtproductID;

END;
GO
delimiter ;


/*************************************************************************************************************

procBranchTransferSynchronizeAmount
Lemuel E. Aceron

SELECT decSubTotalDiscountableAmount, decAmount, decDiscount;
SELECT Subtotal, discount, TotalItemDiscount, vat , VATAbleAmount, evat , eVATAbleAmount, localtax from tblpo where poid = lngpoid;
SELECT decSubTotalDiscountableAmount, decTotalItemDiscount, decBranchTransferDiscount, decBranchTransferDiscountApplied, intBranchTransferDiscountType, decTotalVAT, decTotalVATableAmount;

*************************************************************************************************************/

DROP PROCEDURE IF EXISTS procBranchTransferSynchronizeAmount;
delimiter GO

create procedure procBranchTransferSynchronizeAmount(IN lngBranchTransferID bigint(20))
BEGIN
	DECLARE decTotalItemAmount, decTotalItemDiscount, decTotalVAT, decTotalVATableAmount, decTotalEVAT, decTotalEVATableAmount, decTotalLocalTax  DECIMAL(18,3) DEFAULT 0;
	DECLARE decAmount, decDiscount DECIMAL(18,3) DEFAULT 0;
	DECLARE intBranchTransferDiscountType INT DEFAULT 0;
	DECLARE decSubTotalDiscountableAmount, decBranchTransferDiscount, decBranchTransferDiscountApplied DECIMAL(18,3) DEFAULT 0;
	
	DECLARE done INT DEFAULT 0;
	DECLARE curItems CURSOR FOR SELECT Amount, Discount FROM tblBranchTransferItems WHERE BranchTransferID = lngBranchTransferID;
	DECLARE CONTINUE HANDLER FOR NOT FOUND SET done = 1;
	
	SET decTotalItemAmount = (SELECT SUM(Amount) FROM tblBranchTransferItems WHERE BranchTransferID = lngBranchTransferID);
	SET decTotalItemDiscount = (SELECT SUM(Discount) FROM tblBranchTransferItems WHERE BranchTransferID = lngBranchTransferID AND Discount <> 0);
	
	SET decBranchTransferDiscountApplied = (SELECT DiscountApplied FROM tblBranchTransfer WHERE BranchTransferID = lngBranchTransferID);
	set decTotalItemDiscount = 0;
	
	OPEN curItems;
	REPEAT
		FETCH curItems INTO decAmount, decDiscount;
		
		IF NOT done THEN
			IF decDiscount<>0 THEN
				SET decTotalItemDiscount = (SELECT decTotalItemDiscount + decDiscount);
			END IF;
			SET decSubTotalDiscountableAmount = (SELECT decSubTotalDiscountableAmount + decAmount);
		END IF;
	UNTIL done END REPEAT;
	CLOSE curItems;
	
	SET decBranchTransferDiscountApplied = (SELECT DiscountApplied FROM tblBranchTransfer WHERE BranchTransferID = lngBranchTransferID);
	SET intBranchTransferDiscountType = (SELECT DiscountType FROM tblBranchTransfer WHERE BranchTransferID = lngBranchTransferID);

	IF intBranchTransferDiscountType = 1 and decTotalItemAmount >= decBranchTransferDiscountApplied THEN
		SET decBranchTransferDiscount = (SELECT decBranchTransferDiscountApplied);
	ELSEIF intBranchTransferDiscountType = 2 THEN
		SET decBranchTransferDiscount = (SELECT (decSubTotalDiscountableAmount * (decBranchTransferDiscountApplied / 100)));
	END IF;
	
	SET decTotalVATableAmount = (SELECT (decTotalItemAmount - decBranchTransferDiscount) / (1+(SELECT VAT/100 FROM tblTerminal WHERE TerminalID = 1))) ;
	SET decTotalVAT = (SELECT decTotalVATableAmount * (SELECT VAT/100 FROM tblTerminal WHERE TerminalID = 1));
	SET decTotalEVATableAmount = (SELECT (decTotalItemAmount - decBranchTransferDiscount) / (1+(SELECT VAT/100 FROM tblTerminal WHERE TerminalID = 1))) ;
	SET decTotalEVAT = (SELECT decTotalEVATableAmount * (SELECT EVAT/100 FROM tblTerminal WHERE TerminalID = 1));
	SET decTotalLocalTax = (SELECT decTotalVATableAmount * (SELECT LocalTax/100 FROM tblTerminal WHERE TerminalID = 1));
	
	UPDATE tblBranchTransfer SET
		SubTotal = decTotalItemAmount - decBranchTransferDiscount + Freight - Deposit,
		Discount = decBranchTransferDiscount,
		TotalItemDiscount = decTotalItemDiscount,
		VAT = decTotalVAT,
		VATAbleAmount = decTotalVATableAmount,
		EVAT = decTotalEVAT,
		EVATAbleAmount = decTotalEVATableAmount,
		LocalTax = decTotalLocalTax,
		UnpaidAmount = decTotalItemAmount - decBranchTransferDiscount + Freight - Deposit
	WHERE BranchTransferID = lngBranchTransferID;
	
END;
GO
delimiter ;


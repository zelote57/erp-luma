/***

procPOSynchronizeAmount
Lemuel E. Aceron

SELECT decSubTotalDiscountableAmount, decAmount, decDiscount;
SELECT Subtotal, discount, TotalItemDiscount, vat , VATAbleAmount, evat , eVATAbleAmount, localtax from tblpo where poid = lngpoid;
SELECT decSubTotalDiscountableAmount, decTotalItemDiscount, decPODiscount, decPODiscountApplied, intPODiscountType, decTotalVAT, decTotalVATableAmount;

****/

delimiter GO
DROP PROCEDURE IF EXISTS procPOSynchronizeAmount
GO

create procedure procPOSynchronizeAmount(IN lngPOID bigint(20))
BEGIN
	/*******************************   
	Just set the value of
		@ProductID to check if Qty is 
		correct base don history
	*******************************/
	DECLARE decTotalItemAmount, decTotalItemDiscount, decTotalVAT, decTotalVATableAmount, decTotalEVAT, decTotalEVATableAmount, decTotalLocalTax  DECIMAL(10,2) DEFAULT 0;
	DECLARE decAmount, decDiscount DECIMAL(10,2) DEFAULT 0;
	DECLARE intPODiscountType INT DEFAULT 0;
	DECLARE decSubTotalDiscountableAmount, decPODiscount, decPODiscountApplied DECIMAL(10,2) DEFAULT 0;
	
	DECLARE done INT DEFAULT 0;
	DECLARE curItems CURSOR FOR SELECT Amount, Discount FROM tblPOItems WHERE POID = lngPOID;
	DECLARE CONTINUE HANDLER FOR NOT FOUND SET done = 1;
	
	SET decTotalItemAmount = (SELECT SUM(Amount) FROM tblPOItems WHERE POID = lngPOID);
	SET decTotalItemDiscount = (SELECT SUM(Discount) FROM tblPOItems WHERE POID = lngPOID AND Discount <> 0);
	
	SET decPODiscountApplied = (SELECT DiscountApplied FROM tblPO WHERE POID = lngPOID);
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
	
	SET decPODiscountApplied = (SELECT DiscountApplied FROM tblPO WHERE POID = lngPOID);
	SET intPODiscountType = (SELECT DiscountType FROM tblPO WHERE POID = lngPOID);

	IF intPODiscountType = 1 and decTotalItemAmount >= decPODiscountApplied THEN
		SET decPODiscount = (SELECT decPODiscountApplied);
	ELSEIF intPODiscountType = 2 THEN
		SET decPODiscount = (SELECT (decSubTotalDiscountableAmount * (decPODiscountApplied / 100)));
	END IF;
	
	SET decTotalVATableAmount = (SELECT (decTotalItemAmount - decPODiscount) / (1+(SELECT VAT/100 FROM tblTerminal WHERE TerminalID = 1))) ;
	SET decTotalVAT = (SELECT decTotalVATableAmount * (SELECT VAT/100 FROM tblTerminal WHERE TerminalID = 1));
	SET decTotalEVATableAmount = (SELECT (decTotalItemAmount - decPODiscount) / (1+(SELECT VAT/100 FROM tblTerminal WHERE TerminalID = 1))) ;
	SET decTotalEVAT = (SELECT decTotalEVATableAmount * (SELECT EVAT/100 FROM tblTerminal WHERE TerminalID = 1));
	SET decTotalLocalTax = (SELECT decTotalVATableAmount * (SELECT LocalTax/100 FROM tblTerminal WHERE TerminalID = 1));
	
	UPDATE tblPO SET
		SubTotal = decTotalItemAmount - decPODiscount + Freight - Deposit,
		Discount = decPODiscount,
		TotalItemDiscount = decTotalItemDiscount,
		VAT = decTotalVAT,
		VATAbleAmount = decTotalVATableAmount,
		EVAT = decTotalEVAT,
		EVATAbleAmount = decTotalEVATableAmount,
		LocalTax = decTotalLocalTax
	WHERE POID = lngPOID;
		
	
END;
GO

delimiter ;
 
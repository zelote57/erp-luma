ALTER TABLE tblterminal MODIFY `Status` INT (1) NOT NULL DEFAULT 0;
ALTER TABLE tblterminal MODIFY `AutoPrint` INT (1) NOT NULL DEFAULT 0;
ALTER TABLE tblterminal MODIFY `TerminalReceiptType` INT (1) NOT NULL DEFAULT 0;
ALTER TABLE tblterminal MODIFY `ProductSearchType` INT (1) NOT NULL DEFAULT 0;


ALTER TABLE tblterminal MODIFY `IsPrinterAutoCutter` TINYINT (1) NOT NULL DEFAULT 0;
ALTER TABLE tblterminal MODIFY `EnableEVAT` TINYINT (1) NOT NULL DEFAULT 0;
ALTER TABLE tblterminal MODIFY `ItemVoidConfirmation` TINYINT (1) NOT NULL DEFAULT 0;
ALTER TABLE tblterminal MODIFY `ShowCustomerSelection` TINYINT (1) NOT NULL DEFAULT 0;
ALTER TABLE tblTerminal MODIFY `ShowItemMoreThanZeroQty` TINYINT (1) NOT NULL DEFAULT 0;
ALTER TABLE tblterminal MODIFY `RoundDownRewardPoints` TINYINT (1) NOT NULL DEFAULT 0;
ALTER TABLE tblterminal MODIFY `EnableRewardPoints` TINYINT (1) NOT NULL DEFAULT 0;
ALTER TABLE tblterminal MODIFY `IsFineDining` TINYINT (1) NOT NULL DEFAULT 0;
ALTER TABLE tblterminal MODIFY `AutoGenerateRewardCardNo` TINYINT (1) NOT NULL DEFAULT 0;
ALTER TABLE tblterminal MODIFY `EnableRewardPointsAsPayment` TINYINT (1) NOT NULL DEFAULT 0;
ALTER TABLE tblterminal MODIFY `WillPrintGrandTotal` TINYINT (1) NOT NULL DEFAULT 0;

ALTER TABLE tblterminal MODIFY `IsVATInclusive` TINYINT (1) NOT NULL DEFAULT 0;

ALTER TABLE tblterminal MODIFY `OrderSlipPrinter` INT (1) NOT NULL DEFAULT 0;
ALTER TABLE tblProductGroup MODIFY `OrderSlipPrinter` INT (1) NOT NULL DEFAULT 0;

ALTER TABLE tblTransactionItems MODIFY `OrderSlipPrinter` INT (1) NOT NULL DEFAULT 0;
ALTER TABLE tblTransactionItems MODIFY `ItemDiscountType` INT (1) NOT NULL DEFAULT 0;
ALTER TABLE tblTransactionItems MODIFY `TransactionItemStatus` INT (1) NOT NULL DEFAULT 0;
ALTER TABLE tblTransactionItems MODIFY `PromoType` INT (1) NOT NULL DEFAULT 0;

ALTER TABLE tblContactRewards MODIFY `RewardCardStatus` INT (1) NOT NULL DEFAULT 0;
ALTER TABLE tblContactCreditCardInfo MODIFY `CreditType` INT (1) NOT NULL DEFAULT 0;
ALTER TABLE tblContactCreditCardInfo MODIFY `CreditCardStatus` INT (1) NOT NULL DEFAULT 0;




/*********************************  v_1.0.0.4.sql START  *******************************************************/


UPDATE tblERPConfig SET DBVersion = 'v_1.0.0.4';

ALTER TABLE tblMatrixPackagePriceHistory MODIFY COLUMN Remarks VARCHAR(150);

ALTER TABLE tblPOItems ADD `OldSellingPrice` DECIMAL(18,2) NOT NULL DEFAULT 0;

ALTER TABLE tblTransferOutItems ADD `SellingPrice` DECIMAL(18,2) NOT NULL DEFAULT 0;
ALTER TABLE tblTransferOutItems ADD `SellingVAT` DECIMAL(18,2) NOT NULL DEFAULT 0;
ALTER TABLE tblTransferOutItems ADD `SellingEVAT` DECIMAL(18,2) NOT NULL DEFAULT 0;
ALTER TABLE tblTransferOutItems ADD `SellingLocalTax` DECIMAL(18,2) NOT NULL DEFAULT 0;
ALTER TABLE tblTransferOutItems ADD `OldSellingPrice` DECIMAL(18,2) NOT NULL DEFAULT 0;

ALTER TABLE tblTransferInItems ADD `SellingPrice` DECIMAL(18,2) NOT NULL DEFAULT 0;
ALTER TABLE tblTransferInItems ADD `SellingVAT` DECIMAL(18,2) NOT NULL DEFAULT 0;
ALTER TABLE tblTransferInItems ADD `SellingEVAT` DECIMAL(18,2) NOT NULL DEFAULT 0;
ALTER TABLE tblTransferInItems ADD `SellingLocalTax` DECIMAL(18,2) NOT NULL DEFAULT 0;
ALTER TABLE tblTransferInItems ADD `OldSellingPrice` DECIMAL(18,2) NOT NULL DEFAULT 0;

/*********************************  v_1.0.0.4.sql END  *******************************************************/
   
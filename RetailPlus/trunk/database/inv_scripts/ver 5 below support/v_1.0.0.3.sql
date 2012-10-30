/*********************************  v_1.0.0.3.sql START  *******************************************************/


UPDATE tblERPConfig SET DBVersion = 'v_1.0.0.3';

ALTER TABLE tblPOItems ADD `SellingPrice` DECIMAL(18,2) NOT NULL DEFAULT 0;
ALTER TABLE tblPOItems ADD `SellingVAT` DECIMAL(18,2) NOT NULL DEFAULT 0;
ALTER TABLE tblPOItems ADD `SellingEVAT` DECIMAL(18,2) NOT NULL DEFAULT 0;
ALTER TABLE tblPOItems ADD `SellingLocalTax` DECIMAL(18,2) NOT NULL DEFAULT 0;

/*********************************  v_1.0.0.3.sql END  *******************************************************/
  
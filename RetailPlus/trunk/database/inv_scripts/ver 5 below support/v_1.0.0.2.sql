/*********************************  v_1.0.0.2.sql START  *******************************************************/


UPDATE tblERPConfig SET DBVersion = 'v_1.0.0.2';

ALTER TABLE tblPODebitMemoItems ADD `PrevUnitCost` DECIMAL(18,2) NOT NULL DEFAULT 0;


/*********************************  v_1.0.0.2.sql END  *******************************************************/
 
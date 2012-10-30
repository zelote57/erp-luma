 /*********************************  v_2.0.0.81.sql START  *****************************************************/

UPDATE tblTerminal SET DBVersion = 'v_2.0.0.81';

ALTER TABLE tblSalesPerItem ADD `ProductGroup` VARCHAR(100) NOT NULL;
ALTER TABLE tblSalesPerItem ADD `ProductUnitCode` VARCHAR(30) NOT NULL;

/*********************************  v_2.0.0.81.sql END  *******************************************************/  
 /*********************************  v_2.0.0.7.sql START  *****************************************************/

UPDATE tblTerminal SET DBVersion = 'v_2.0.0.7';


ALTER TABLE tblProductPackagePriceHistory ADD Remarks VARCHAR(75) NOT NULL DEFAULT 'Change Price Module';
ALTER TABLE tblMatrixPackagePriceHistory ADD Remarks VARCHAR(75) NOT NULL DEFAULT 'Change Price Module';

UPDATE tblProductPackagePriceHistory SET Remarks = 'Change Price Module';
UPDATE tblMatrixPackagePriceHistory SET Remarks = 'Change Price Module';
        
/*********************************  v_2.0.0.7.sql END  *******************************************************/ 
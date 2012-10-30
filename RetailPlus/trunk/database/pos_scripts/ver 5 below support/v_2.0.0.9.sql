 /*********************************  v_2.0.0.9.sql START  *****************************************************/

UPDATE tblTerminal SET DBVersion = 'v_2.0.0.9';

ALTER TABLE tblProducts DROP INDEX `IX_tblProducts`;
ALTER TABLE tblProducts DROP INDEX `PK_tblProducts`;

ALTER TABLE tblProducts ADD INDEX `IX_tblProducts` (`ProductID`, `ProductCode`,`ProductDesc`);
ALTER TABLE tblProducts ADD UNIQUE INDEX `PK_tblProducts` (`ProductCode`,`ProductDesc`);

ALTER TABLE tblProducts ADD `Active` TINYINT(1) NOT NULL DEFAULT 1;

/*********************************  v_2.0.0.9.sql END  *******************************************************/   
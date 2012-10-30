/*********************************  v_2.0.1.5.sql START  *******************************************************/

ALTER TABLE tblAgentsCommision ADD `AgentID` BIGINT(20) NOT NULL DEFAULT 0;
ALTER TABLE tblAgentsCommision ADD `AgentName` VARCHAR(100);

/*****************************
**	Added on September 21, 2010 for Agent Commision Access
**	Lemuel E. Aceron
*****************************/
INSERT INTO sysAccessTypes (TypeID, TypeName) VALUES (134, 'Agents Commision Report');
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (1, 134, 1, 1);
INSERT INTO sysAccessRights (UID, TranTypeID, AllowRead, AllowWrite) VALUES (1, 134, 1, 1);
UPDATE sysAccessTypes SET SequenceNo = 5, Category = '11: Backend - Sales Reports' WHERE TypeID = 134;

ALTER TABLE tblTerminal ADD `WillPrintGrandTotal` TINYINT(1) UNSIGNED NOT NULL DEFAULT 1;

INSERT INTO sysAccessTypes (TypeID, TypeName) VALUES (135, 'Position');
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (1, 135, 1, 1);
INSERT INTO sysAccessRights (UID, TranTypeID, AllowRead, AllowWrite) VALUES (1, 135, 1, 1);
UPDATE sysAccessTypes SET SequenceNo = 9, Category = '04: Backend - MasterFiles' WHERE TypeID = 135;

INSERT INTO sysAccessTypes (TypeID, TypeName) VALUES (136, 'Department');
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (1, 136, 1, 1);
INSERT INTO sysAccessRights (UID, TranTypeID, AllowRead, AllowWrite) VALUES (1, 136, 1, 1);
UPDATE sysAccessTypes SET SequenceNo = 10, Category = '04: Backend - MasterFiles' WHERE TypeID = 136;

INSERT INTO sysAccessTypes (TypeID, TypeName) VALUES (137, 'Agents Sales Report');
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (1, 137, 1, 1);
INSERT INTO sysAccessRights (UID, TranTypeID, AllowRead, AllowWrite) VALUES (1, 137, 1, 1);
UPDATE sysAccessTypes SET SequenceNo = 4, Category = '11: Backend - Sales Reports' WHERE TypeID = 137;

/*****************************
**	tblPositions
*****************************/
DROP TABLE IF EXISTS tblPositions;
CREATE TABLE tblPositions (
`PositionID` INT(10) UNSIGNED NOT NULL AUTO_INCREMENT,
`PositionCode` VARCHAR(30) NOT NULL,
`PositionName` VARCHAR(30) NOT NULL,
PRIMARY KEY (`PositionID`),
INDEX `IX_tblPositions`(`PositionID`, `PositionCode`, `PositionName`),
UNIQUE `PK_tblPositions`(`PositionCode`),
INDEX `IX1_tblPositions`(`PositionID`),
INDEX `IX2_tblPositions`(`PositionCode`),
INDEX `IX3_tblPositions`(`PositionName`)
);

INSERT INTO tblPositions VALUES(1, 'System Default Position', 'System Default Position');



/*****************************
**	tblDepartments
*****************************/
DROP TABLE IF EXISTS tblDepartments;
CREATE TABLE tblDepartments (
`DepartmentID` INT(10) UNSIGNED NOT NULL AUTO_INCREMENT,
`DepartmentCode` VARCHAR(30) NOT NULL,
`DepartmentName` VARCHAR(30) NOT NULL,
PRIMARY KEY (`DepartmentID`),
INDEX `IX_tblDepartments`(`DepartmentID`, `DepartmentCode`, `DepartmentName`),
UNIQUE `PK_tblDepartments`(`DepartmentCode`),
INDEX `IX1_tblDepartments`(`DepartmentID`),
INDEX `IX2_tblDepartments`(`DepartmentCode`),
INDEX `IX3_tblDepartments`(`DepartmentName`)
);

INSERT INTO tblDepartments VALUES(1, 'System Default Department', 'System Default Department');

ALTER TABLE tblContacts ADD `DepartmentID` INT(10) UNSIGNED NOT NULL DEFAULT 1;
ALTER TABLE tblContacts ADD `PositionID` INT(10) UNSIGNED NOT NULL DEFAULT 1;

ALTER TABLE tblAgentsCommision ADD `DepartmentName` VARCHAR(30) NOT NULL;
ALTER TABLE tblAgentsCommision ADD `PositionName` VARCHAR(30) NOT NULL;

ALTER TABLE tblTransactions01 ADD `AgentDepartmentName` VARCHAR(30) NOT NULL DEFAULT 'System Default Department';
ALTER TABLE tblTransactions01 ADD `AgentPositionName` VARCHAR(30) NOT NULL DEFAULT 'System Default Position';
ALTER TABLE tblTransactions02 ADD `AgentDepartmentName` VARCHAR(30) NOT NULL DEFAULT 'System Default Department';
ALTER TABLE tblTransactions02 ADD `AgentPositionName` VARCHAR(30) NOT NULL DEFAULT 'System Default Position';
ALTER TABLE tblTransactions03 ADD `AgentDepartmentName` VARCHAR(30) NOT NULL DEFAULT 'System Default Department';
ALTER TABLE tblTransactions03 ADD `AgentPositionName` VARCHAR(30) NOT NULL DEFAULT 'System Default Position';
ALTER TABLE tblTransactions04 ADD `AgentDepartmentName` VARCHAR(30) NOT NULL DEFAULT 'System Default Department';
ALTER TABLE tblTransactions04 ADD `AgentPositionName` VARCHAR(30) NOT NULL DEFAULT 'System Default Position';
ALTER TABLE tblTransactions05 ADD `AgentDepartmentName` VARCHAR(30) NOT NULL DEFAULT 'System Default Department';
ALTER TABLE tblTransactions05 ADD `AgentPositionName` VARCHAR(30) NOT NULL DEFAULT 'System Default Position';
ALTER TABLE tblTransactions06 ADD `AgentDepartmentName` VARCHAR(30) NOT NULL DEFAULT 'System Default Department';
ALTER TABLE tblTransactions06 ADD `AgentPositionName` VARCHAR(30) NOT NULL DEFAULT 'System Default Position';
ALTER TABLE tblTransactions07 ADD `AgentDepartmentName` VARCHAR(30) NOT NULL DEFAULT 'System Default Department';
ALTER TABLE tblTransactions07 ADD `AgentPositionName` VARCHAR(30) NOT NULL DEFAULT 'System Default Position';
ALTER TABLE tblTransactions08 ADD `AgentDepartmentName` VARCHAR(30) NOT NULL DEFAULT 'System Default Department';
ALTER TABLE tblTransactions08 ADD `AgentPositionName` VARCHAR(30) NOT NULL DEFAULT 'System Default Position';
ALTER TABLE tblTransactions09 ADD `AgentDepartmentName` VARCHAR(30) NOT NULL DEFAULT 'System Default Department';
ALTER TABLE tblTransactions09 ADD `AgentPositionName` VARCHAR(30) NOT NULL DEFAULT 'System Default Position';
ALTER TABLE tblTransactions10 ADD `AgentDepartmentName` VARCHAR(30) NOT NULL DEFAULT 'System Default Department';
ALTER TABLE tblTransactions10 ADD `AgentPositionName` VARCHAR(30) NOT NULL DEFAULT 'System Default Position';
ALTER TABLE tblTransactions11 ADD `AgentDepartmentName` VARCHAR(30) NOT NULL DEFAULT 'System Default Department';
ALTER TABLE tblTransactions11 ADD `AgentPositionName` VARCHAR(30) NOT NULL DEFAULT 'System Default Position';
ALTER TABLE tblTransactions12 ADD `AgentDepartmentName` VARCHAR(30) NOT NULL DEFAULT 'System Default Department';
ALTER TABLE tblTransactions12 ADD `AgentPositionName` VARCHAR(30) NOT NULL DEFAULT 'System Default Position';

UPDATE tblTransactions01 SET AgentDepartmentName = 'System Default Department';
UPDATE tblTransactions01 SET AgentPositionName = 'System Default Position';
UPDATE tblTransactions02 SET AgentDepartmentName = 'System Default Department';
UPDATE tblTransactions02 SET AgentPositionName = 'System Default Position';
UPDATE tblTransactions03 SET AgentDepartmentName = 'System Default Department';
UPDATE tblTransactions03 SET AgentPositionName = 'System Default Position';
UPDATE tblTransactions04 SET AgentDepartmentName = 'System Default Department';
UPDATE tblTransactions04 SET AgentPositionName = 'System Default Position';
UPDATE tblTransactions05 SET AgentDepartmentName = 'System Default Department';
UPDATE tblTransactions05 SET AgentPositionName = 'System Default Position';
UPDATE tblTransactions06 SET AgentDepartmentName = 'System Default Department';
UPDATE tblTransactions06 SET AgentPositionName = 'System Default Position';
UPDATE tblTransactions07 SET AgentDepartmentName = 'System Default Department';
UPDATE tblTransactions07 SET AgentPositionName = 'System Default Position';
UPDATE tblTransactions08 SET AgentDepartmentName = 'System Default Department';
UPDATE tblTransactions08 SET AgentPositionName = 'System Default Position';
UPDATE tblTransactions09 SET AgentDepartmentName = 'System Default Department';
UPDATE tblTransactions09 SET AgentPositionName = 'System Default Position';
UPDATE tblTransactions10 SET AgentDepartmentName = 'System Default Department';
UPDATE tblTransactions10 SET AgentPositionName = 'System Default Position';
UPDATE tblTransactions11 SET AgentDepartmentName = 'System Default Department';
UPDATE tblTransactions11 SET AgentPositionName = 'System Default Position';
UPDATE tblTransactions12 SET AgentDepartmentName = 'System Default Department';
UPDATE tblTransactions12 SET AgentPositionName = 'System Default Position';


/*********************************  v_2.0.1.5.sql END  *******************************************************/
    
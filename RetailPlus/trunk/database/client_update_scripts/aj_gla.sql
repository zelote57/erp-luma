﻿-- =============================================
-- Script Template
-- =============================================

TRUNCATE TABLE tblContractGroup;
INSERT INTO tblContactGroup (ContactGroupID, ContactGroupCode, ContactGroupName, ContactGroupCategory) VALUES (1, 'CUS', 'Default Customer Group', 1);
INSERT INTO tblContactGroup (ContactGroupID, ContactGroupCode, ContactGroupName, ContactGroupCategory) VALUES (2, 'SUP', 'Default Supplier Group', 2);



TRUNCATE TABLE tblPositions;

INSERT INTO tblPositions VALUES(1, 'System Default Position', 'System Default Position');
INSERT INTO tblPositions(PositionCode, PositionName) VALUES ('GEN MGR',    'GEN MGR'  );
INSERT INTO tblPositions(PositionCode, PositionName) VALUES ('LEGAL SERVICES MANAG',    'LEGAL SERVICES MANAG'  );
INSERT INTO tblPositions(PositionCode, PositionName) VALUES ('CHAIRMAN',    'CHAIRMAN'  );
INSERT INTO tblPositions(PositionCode, PositionName) VALUES ('FISCAL',    'FISCAL'  );
INSERT INTO tblPositions(PositionCode, PositionName) VALUES ('CEO',    'CEO'  );
INSERT INTO tblPositions(PositionCode, PositionName) VALUES ('VP',    'VP'  );
INSERT INTO tblPositions(PositionCode, PositionName) VALUES ('PROPRIETRESS',    'PROPRIETRESS'  );
INSERT INTO tblPositions(PositionCode, PositionName) VALUES ('PRES',    'PRES'  );
INSERT INTO tblPositions(PositionCode, PositionName) VALUES ('ENGR',    'ENGR'  );
INSERT INTO tblPositions(PositionCode, PositionName) VALUES ('NURSE',    'NURSE'  );
INSERT INTO tblPositions(PositionCode, PositionName) VALUES ('BUSINESSMAN',    'BUSINESSMAN'  );
INSERT INTO tblPositions(PositionCode, PositionName) VALUES ('GM',    'GM'  );
INSERT INTO tblPositions(PositionCode, PositionName) VALUES ('1ST VP',    '1ST VP'  );
INSERT INTO tblPositions(PositionCode, PositionName) VALUES ('SAVP',    'SAVP'  );
INSERT INTO tblPositions(PositionCode, PositionName) VALUES ('PRESIDENT',    'PRESIDENT'  );
INSERT INTO tblPositions(PositionCode, PositionName) VALUES ('OWNER',    'OWNER'  );
INSERT INTO tblPositions(PositionCode, PositionName) VALUES ('MANAGING DIR',    'MANAGING DIR'  );
INSERT INTO tblPositions(PositionCode, PositionName) VALUES ('FVP',    'FVP'  );
INSERT INTO tblPositions(PositionCode, PositionName) VALUES ('DEPT HEAD',    'DEPT HEAD'  );
INSERT INTO tblPositions(PositionCode, PositionName) VALUES ('REGIONAL CHIEF',    'REGIONAL CHIEF'  );
INSERT INTO tblPositions(PositionCode, PositionName) VALUES ('MANAGER',    'MANAGER'  );
INSERT INTO tblPositions(PositionCode, PositionName) VALUES ('CHARIMAN',    'CHARIMAN'  );
INSERT INTO tblPositions(PositionCode, PositionName) VALUES ('LAWYER',    'LAWYER'  );
INSERT INTO tblPositions(PositionCode, PositionName) VALUES ('TREASURY',    'TREASURY'  );
INSERT INTO tblPositions(PositionCode, PositionName) VALUES ('OP MGR',    'OP MGR'  );
INSERT INTO tblPositions(PositionCode, PositionName) VALUES ('AGD MGR',    'AGD MGR'  );
INSERT INTO tblPositions(PositionCode, PositionName) VALUES ('CLUSTER SALES',    'CLUSTER SALES'  );
INSERT INTO tblPositions(PositionCode, PositionName) VALUES ('TREASURY HEAD',    'TREASURY HEAD'  );
INSERT INTO tblPositions(PositionCode, PositionName) VALUES ('DIRECTOR',    'DIRECTOR'  );
INSERT INTO tblPositions(PositionCode, PositionName) VALUES ('VICE CHAIR',    'VICE CHAIR'  );
INSERT INTO tblPositions(PositionCode, PositionName) VALUES ('SR MGR',    'SR MGR'  );
INSERT INTO tblPositions(PositionCode, PositionName) VALUES ('SR ASSOC VP',    'SR ASSOC VP'  );
INSERT INTO tblPositions(PositionCode, PositionName) VALUES ('INT ADMIN',    'INT ADMIN'  );
INSERT INTO tblPositions(PositionCode, PositionName) VALUES ('FSVP',    'FSVP'  );
INSERT INTO tblPositions(PositionCode, PositionName) VALUES ('EXEC OFFICER',    'EXEC OFFICER'  );
INSERT INTO tblPositions(PositionCode, PositionName) VALUES ('AVP',    'AVP'  );
INSERT INTO tblPositions(PositionCode, PositionName) VALUES ('ACCT MGR',    'ACCT MGR'  );
INSERT INTO tblPositions(PositionCode, PositionName) VALUES ('MGR',    'MGR'  );
INSERT INTO tblPositions(PositionCode, PositionName) VALUES ('TREASURER',    'TREASURER'  );
INSERT INTO tblPositions(PositionCode, PositionName) VALUES ('TECH ASSOC',    'TECH ASSOC'  );
INSERT INTO tblPositions(PositionCode, PositionName) VALUES ('CHIEF OF STAFF',    'CHIEF OF STAFF'  );
INSERT INTO tblPositions(PositionCode, PositionName) VALUES ('ASSET MGMT',    'ASSET MGMT'  );
INSERT INTO tblPositions(PositionCode, PositionName) VALUES ('JR ASST MGR',    'JR ASST MGR'  );
INSERT INTO tblPositions(PositionCode, PositionName) VALUES ('VP MKTG',    'VP MKTG'  );
INSERT INTO tblPositions(PositionCode, PositionName) VALUES ('BOARD OF DIR',    'BOARD OF DIR'  );
INSERT INTO tblPositions(PositionCode, PositionName) VALUES ('CATEGORY MGR',    'CATEGORY MGR'  );
INSERT INTO tblPositions(PositionCode, PositionName) VALUES ('IT DEPT',    'IT DEPT'  );
INSERT INTO tblPositions(PositionCode, PositionName) VALUES ('MED DIR',    'MED DIR'  );
INSERT INTO tblPositions(PositionCode, PositionName) VALUES ('MD',    'MD'  );
INSERT INTO tblPositions(PositionCode, PositionName) VALUES ('ASSOC DIR',    'ASSOC DIR'  );
INSERT INTO tblPositions(PositionCode, PositionName) VALUES ('GOVERNOR',    'GOVERNOR'  );
INSERT INTO tblPositions(PositionCode, PositionName) VALUES ('ADMIN ASST',    'ADMIN ASST'  );
INSERT INTO tblPositions(PositionCode, PositionName) VALUES ('SALES',    'SALES'  );
INSERT INTO tblPositions(PositionCode, PositionName) VALUES ('COUNTRY MGR',    'COUNTRY MGR'  );
INSERT INTO tblPositions(PositionCode, PositionName) VALUES ('CHIEF FINANCIAL',    'CHIEF FINANCIAL'  );
INSERT INTO tblPositions(PositionCode, PositionName) VALUES ('CORP PLANNING',    'CORP PLANNING'  );
INSERT INTO tblPositions(PositionCode, PositionName) VALUES ('AUDIT',    'AUDIT'  );
INSERT INTO tblPositions(PositionCode, PositionName) VALUES ('IT',    'IT'  );
INSERT INTO tblPositions(PositionCode, PositionName) VALUES ('BUS DEVT DIR',    'BUS DEVT DIR'  );
INSERT INTO tblPositions(PositionCode, PositionName) VALUES ('EP',    'EP'  );
INSERT INTO tblPositions(PositionCode, PositionName) VALUES ('ADMIN MGR',    'ADMIN MGR'  );
INSERT INTO tblPositions(PositionCode, PositionName) VALUES ('BUDGET DIR',    'BUDGET DIR'  );
INSERT INTO tblPositions(PositionCode, PositionName) VALUES ('CHAIRMAN/CEO',    'CHAIRMAN/CEO'  );
INSERT INTO tblPositions(PositionCode, PositionName) VALUES ('1ST VP SECURITY DEPT',    '1ST VP SECURITY DEPT'  );
INSERT INTO tblPositions(PositionCode, PositionName) VALUES ('EVP',    'EVP'  );
INSERT INTO tblPositions(PositionCode, PositionName) VALUES ('ADMIN',    'ADMIN'  );
INSERT INTO tblPositions(PositionCode, PositionName) VALUES ('BRANCH HEAD',    'BRANCH HEAD'  );
INSERT INTO tblPositions(PositionCode, PositionName) VALUES ('BUSINESSWOMAN',    'BUSINESSWOMAN'  );
INSERT INTO tblPositions(PositionCode, PositionName) VALUES ('CEO/PRES',    'CEO/PRES'  );
INSERT INTO tblPositions(PositionCode, PositionName) VALUES ('CFC',    'CFC'  );
INSERT INTO tblPositions(PositionCode, PositionName) VALUES ('EXEC DIR',    'EXEC DIR'  );
INSERT INTO tblPositions(PositionCode, PositionName) VALUES ('DERMATOLOGIST',    'DERMATOLOGIST'  );
INSERT INTO tblPositions(PositionCode, PositionName) VALUES ('BUREAU CHIEF',    'BUREAU CHIEF'  );
INSERT INTO tblPositions(PositionCode, PositionName) VALUES ('FINANCIAL ADVISOR',    'FINANCIAL ADVISOR'  );
INSERT INTO tblPositions(PositionCode, PositionName) VALUES ('DIRECTRESS',    'DIRECTRESS'  );
INSERT INTO tblPositions(PositionCode, PositionName) VALUES ('PARTNER',    'PARTNER'  );
INSERT INTO tblPositions(PositionCode, PositionName) VALUES ('SVP',    'SVP'  );
INSERT INTO tblPositions(PositionCode, PositionName) VALUES ('MGMT SPECIALIST',    'MGMT SPECIALIST'  );
INSERT INTO tblPositions(PositionCode, PositionName) VALUES ('VP TREASURER',    'VP TREASURER'  );
INSERT INTO tblPositions(PositionCode, PositionName) VALUES ('FINANCE MGR',    'FINANCE MGR'  );
INSERT INTO tblPositions(PositionCode, PositionName) VALUES ('HEAD ADMIN',    'HEAD ADMIN'  );
INSERT INTO tblPositions(PositionCode, PositionName) VALUES ('PRES/CEO',    'PRES/CEO'  );
INSERT INTO tblPositions(PositionCode, PositionName) VALUES ('CLG DEPT',    'CLG DEPT'  );
INSERT INTO tblPositions(PositionCode, PositionName) VALUES ('KEY ACCT MGR',    'KEY ACCT MGR'  );
INSERT INTO tblPositions(PositionCode, PositionName) VALUES ('RESEARCH SPECIALIST',    'RESEARCH SPECIALIST'  );
INSERT INTO tblPositions(PositionCode, PositionName) VALUES ('VP/GM',    'VP/GM'  );
INSERT INTO tblPositions(PositionCode, PositionName) VALUES ('HRD MGR',    'HRD MGR'  );
INSERT INTO tblPositions(PositionCode, PositionName) VALUES ('DIR GEN',    'DIR GEN'  );
INSERT INTO tblPositions(PositionCode, PositionName) VALUES ('REGIONAL HEAD',    'REGIONAL HEAD'  );
INSERT INTO tblPositions(PositionCode, PositionName) VALUES ('PROFESSOR',    'PROFESSOR'  );
INSERT INTO tblPositions(PositionCode, PositionName) VALUES ('SR VP',    'SR VP'  );
INSERT INTO tblPositions(PositionCode, PositionName) VALUES ('HEAD OPERATIONS',    'HEAD OPERATIONS'  );
INSERT INTO tblPositions(PositionCode, PositionName) VALUES ('TEACHER',    'TEACHER'  );
INSERT INTO tblPositions(PositionCode, PositionName) VALUES ('CORP TREASURER',    'CORP TREASURER'  );
INSERT INTO tblPositions(PositionCode, PositionName) VALUES ('LOAN/DISCOUNT DEPT',    'LOAN/DISCOUNT DEPT'  );
INSERT INTO tblPositions(PositionCode, PositionName) VALUES ('MAYOR',    'MAYOR'  );
INSERT INTO tblPositions(PositionCode, PositionName) VALUES ('LEGAL COUNSEL',    'LEGAL COUNSEL'  );
INSERT INTO tblPositions(PositionCode, PositionName) VALUES ('HEAD',    'HEAD'  );
INSERT INTO tblPositions(PositionCode, PositionName) VALUES ('RECORDS OFFICER',    'RECORDS OFFICER'  );
INSERT INTO tblPositions(PositionCode, PositionName) VALUES ('ACCTG DEPT',    'ACCTG DEPT'  );
INSERT INTO tblPositions(PositionCode, PositionName) VALUES ('QMR',    'QMR'  );
INSERT INTO tblPositions(PositionCode, PositionName) VALUES ('OFFICER',    'OFFICER'  );
INSERT INTO tblPositions(PositionCode, PositionName) VALUES ('CONSULTANT',    'CONSULTANT'  );
INSERT INTO tblPositions(PositionCode, PositionName) VALUES ('FACULTY MEMB',    'FACULTY MEMB'  );
INSERT INTO tblPositions(PositionCode, PositionName) VALUES ('SALES AND MKTG',    'SALES AND MKTG'  );
INSERT INTO tblPositions(PositionCode, PositionName) VALUES ('BRANCH HEAD MGR',    'BRANCH HEAD MGR'  );
INSERT INTO tblPositions(PositionCode, PositionName) VALUES ('CONTRACT ASSISTANT',    'CONTRACT ASSISTANT'  );
INSERT INTO tblPositions(PositionCode, PositionName) VALUES ('DOCTOR',    'DOCTOR'  );
INSERT INTO tblPositions(PositionCode, PositionName) VALUES ('CPA',    'CPA'  );
INSERT INTO tblPositions(PositionCode, PositionName) VALUES ('UNIT MGR',    'UNIT MGR'  );
INSERT INTO tblPositions(PositionCode, PositionName) VALUES ('REGIONAL DIRECTOR',    'REGIONAL DIRECTOR'  );
INSERT INTO tblPositions(PositionCode, PositionName) VALUES ('AVP MARKETING SALES',    'AVP MARKETING SALES'  );
INSERT INTO tblPositions(PositionCode, PositionName) VALUES ('PRES &CEO',    'PRES &CEO'  );
INSERT INTO tblPositions(PositionCode, PositionName) VALUES ('CORP COMMUNICATION SPECIALIST',    'CORP COMMUNICATION SPECIALIST'  );
INSERT INTO tblPositions(PositionCode, PositionName) VALUES ('CORPORATE LAWYER',    'CORPORATE LAWYER'  );
INSERT INTO tblPositions(PositionCode, PositionName) VALUES ('PROMOTIONS HEAD',    'PROMOTIONS HEAD'  );
INSERT INTO tblPositions(PositionCode, PositionName) VALUES ('CEO & MANAGING DIRECTOR',    'CEO & MANAGING DIRECTOR'  );
INSERT INTO tblPositions(PositionCode, PositionName) VALUES ('FINANCE MANAGER',    'FINANCE MANAGER'  );
INSERT INTO tblPositions(PositionCode, PositionName) VALUES ('EVP/ TREASURY HEADS',    'EVP/ TREASURY HEADS'  );
INSERT INTO tblPositions(PositionCode, PositionName) VALUES ('DEPUTY AMBASSADOR',    'DEPUTY AMBASSADOR'  );
INSERT INTO tblPositions(PositionCode, PositionName) VALUES ('TRAINING & QUALITY SPECIALIST',    'TRAINING & QUALITY SPECIALIST'  );
INSERT INTO tblPositions(PositionCode, PositionName) VALUES ('MANAGING DIRECTOR',    'MANAGING DIRECTOR'  );
INSERT INTO tblPositions(PositionCode, PositionName) VALUES ('VP UNDERWRITTING',    'VP UNDERWRITTING'  );
INSERT INTO tblPositions(PositionCode, PositionName) VALUES ('SEC',    'SEC'  );
INSERT INTO tblPositions(PositionCode, PositionName) VALUES ('BRANCH MANAGER',    'BRANCH MANAGER'  );
INSERT INTO tblPositions(PositionCode, PositionName) VALUES ('ASSOCIATE DIRECTOR',    'ASSOCIATE DIRECTOR'  );
INSERT INTO tblPositions(PositionCode, PositionName) VALUES ('MARKETING DIRECTOR',    'MARKETING DIRECTOR'  );
INSERT INTO tblPositions(PositionCode, PositionName) VALUES ('BRANCH MGR',    'BRANCH MGR'  );
INSERT INTO tblPositions(PositionCode, PositionName) VALUES ('AGENCY MANAGER',    'AGENCY MANAGER'  );
INSERT INTO tblPositions(PositionCode, PositionName) VALUES ('GROUP HEAD',    'GROUP HEAD'  );
INSERT INTO tblPositions(PositionCode, PositionName) VALUES ('LEGAL DEPT',    'LEGAL DEPT'  );
INSERT INTO tblPositions(PositionCode, PositionName) VALUES ('BM',    'BM'  );


-- Salutations
DELETE FROM sysConfig WHERE Category = 'Salutation';
TRUNCATE TABLE tblSalutations;
INSERT INTO tblSalutations (SalutationCode, SalutationName)VALUES('ARCH', 'ARCH');
INSERT INTO tblSalutations (SalutationCode, SalutationName)VALUES('ATTY', 'ATTY');
INSERT INTO tblSalutations (SalutationCode, SalutationName)VALUES('ATY', 'ATY');
INSERT INTO tblSalutations (SalutationCode, SalutationName)VALUES('CAPT', 'CAPT');
INSERT INTO tblSalutations (SalutationCode, SalutationName)VALUES('COL', 'COL');
INSERT INTO tblSalutations (SalutationCode, SalutationName)VALUES('CONG.', 'CONG.');
INSERT INTO tblSalutations (SalutationCode, SalutationName)VALUES('DEAN', 'DEAN');
INSERT INTO tblSalutations (SalutationCode, SalutationName)VALUES('DIR', 'DIR');
INSERT INTO tblSalutations (SalutationCode, SalutationName)VALUES('DR', 'DR');
INSERT INTO tblSalutations (SalutationCode, SalutationName)VALUES('DRMORA', 'DRMORA');
INSERT INTO tblSalutations (SalutationCode, SalutationName)VALUES('ENGR', 'ENGR');
INSERT INTO tblSalutations (SalutationCode, SalutationName)VALUES('ENGR.', 'ENGR.');
INSERT INTO tblSalutations (SalutationCode, SalutationName)VALUES('FR', 'FR');
INSERT INTO tblSalutations (SalutationCode, SalutationName)VALUES('GOV', 'GOV');
INSERT INTO tblSalutations (SalutationCode, SalutationName)VALUES('JUDGE', 'JUDGE');
INSERT INTO tblSalutations (SalutationCode, SalutationName)VALUES('JUSTICE', 'JUSTICE');
INSERT INTO tblSalutations (SalutationCode, SalutationName)VALUES('MAR', 'MAR');
INSERT INTO tblSalutations (SalutationCode, SalutationName)VALUES('MAYOR', 'MAYOR');
INSERT INTO tblSalutations (SalutationCode, SalutationName)VALUES('MR', 'MR');
INSERT INTO tblSalutations (SalutationCode, SalutationName)VALUES('MRS', 'MRS');
INSERT INTO tblSalutations (SalutationCode, SalutationName)VALUES('MS', 'MS');
INSERT INTO tblSalutations (SalutationCode, SalutationName)VALUES('MSGR', 'MSGR');

DELETE FROM sysAccessRights WHERE TranTypeID IN (SELECT TypeID FROM sysAccessTypes WHERE Category = '05: Backend - MasterFiles - Product');
DELETE FROM sysAccessGroupRights WHERE TranTypeID IN (SELECT TypeID FROM sysAccessTypes WHERE Category = '05: Backend - MasterFiles - Product');
DELETE FROM sysAccessTypes WHERE Category = '05: Backend - MasterFiles - Product';

DELETE FROM sysAccessRights WHERE TranTypeID = 145; DELETE FROM sysAccessGroupRights WHERE TranTypeID = 145;
DELETE FROM sysAccessTypes WHERE TypeID = 145;
--  INSERT INTO sysAccessTypes (TypeID, TypeName) VALUES (145, 'Credit Card Issuance');


DELETE FROM sysAccessRights WHERE TranTypeID = 146; DELETE FROM sysAccessGroupRights WHERE TranTypeID = 146;
DELETE FROM sysAccessTypes WHERE TypeID = 146;
--  INSERT INTO sysAccessTypes (TypeID, TypeName) VALUES (146, 'Credit Card Replacement');


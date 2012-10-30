/******************************************************************************
**		File: access.sql
**		Name: inventory access
**		Desc: inventory access
**
**		Auth: Lemuel E. Aceron
**		Date: 
*******************************************************************************
**		Change History
*******************************************************************************
**		Date:		Author:				Description:
**		--------		--------				-------------------------------------------
**    
*******************************************************************************/ 

/*****************************
**	Added on September 1, 2007 for ERP
**	Lemuel E. Aceron
*****************************/
INSERT INTO sysAccessTypes (TypeID, TypeName) VALUES (93, 'PurchasesAndPayablesMenu');
INSERT INTO sysAccessTypes (TypeID, TypeName) VALUES (94, 'Purchase Orders');
INSERT INTO sysAccessTypes (TypeID, TypeName) VALUES (95, 'GRN');
INSERT INTO sysAccessTypes (TypeID, TypeName) VALUES (96, 'Accounts Payable');
INSERT INTO sysAccessTypes (TypeID, TypeName) VALUES (97, 'PostingDates');
INSERT INTO sysAccessTypes (TypeID, TypeName) VALUES (98, 'PurchaseAnalysis');
INSERT INTO sysAccessTypes (TypeID, TypeName) VALUES (105, 'Purchase Returns');
INSERT INTO sysAccessTypes (TypeID, TypeName) VALUES (106, 'Purchase Debit Memo');

UPDATE sysAccessTypes SET SequenceNo = 1, Category = '06: Backend - Purchase And Payables' WHERE TypeID = 94;
UPDATE sysAccessTypes SET SequenceNo = 2, Category = '06: Backend - Purchase And Payables' WHERE TypeID = 105;
UPDATE sysAccessTypes SET SequenceNo = 3, Category = '06: Backend - Purchase And Payables' WHERE TypeID = 106;
/*****************************
**	Added on Feb 17, 2008 for ERP
**	Lemuel E. Aceron
*****************************/
INSERT INTO sysAccessTypes (TypeID, TypeName) VALUES (112, 'Transfer In');
INSERT INTO sysAccessTypes (TypeID, TypeName) VALUES (113, 'Transfer Out');
/*!40000 ALTER TABLE `sysAccessTypes` ENABLE KEYS */; 

UPDATE sysAccessTypes SET SequenceNo = 6, Category = '08: Backend - Inventory' WHERE TypeID = 112;
UPDATE sysAccessTypes SET SequenceNo = 7, Category = '08: Backend - Inventory' WHERE TypeID = 113;

/*****************************
**	Added on Feb 21, 2008 for ERP
**	Lemuel E. Aceron
*****************************/
INSERT INTO sysAccessTypes (TypeID, TypeName) VALUES (114, 'InvAdjustment');
INSERT INTO sysAccessTypes (TypeID, TypeName) VALUES (115, 'CloseInventory');
/*!40000 ALTER TABLE `sysAccessTypes` ENABLE KEYS */; 

UPDATE sysAccessTypes SET SequenceNo = 8, Category = '08: Backend - Inventory' WHERE TypeID = 114;
UPDATE sysAccessTypes SET SequenceNo = 9, Category = '08: Backend - Inventory' WHERE TypeID = 115;

/*****************************
**	Added on Feb 26, 2008 for Branch Inventory Count Sync
**	Lemuel E. Aceron
*****************************/
INSERT INTO sysAccessTypes (TypeID, TypeName) VALUES (116, 'Export Inventory Count');
INSERT INTO sysAccessTypes (TypeID, TypeName) VALUES (117, 'Import Inventory Count');
INSERT INTO sysAccessTypes (TypeID, TypeName) VALUES (118, 'Synchronize Inventory Count');
/*!40000 ALTER TABLE `sysAccessTypes` ENABLE KEYS */; 

UPDATE sysAccessTypes SET SequenceNo = 10, Category = '08: Backend - Inventory' WHERE TypeID = 116;
UPDATE sysAccessTypes SET SequenceNo = 11, Category = '08: Backend - Inventory' WHERE TypeID = 117;
UPDATE sysAccessTypes SET SequenceNo = 12, Category = '08: Backend - Inventory' WHERE TypeID = 118;

INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (1, 93, 1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (1, 94, 1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (1, 95, 1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (1, 96, 1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (1, 97, 1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (1, 98, 1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (1, 105, 1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (1, 106, 1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (1, 112, 1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (1, 113, 1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (1, 114, 1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (1, 115, 1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (1, 116, 1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (1, 117, 1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (1, 118, 1, 1);


/*****************************
**	Managers Default Access
*****************************/
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (2, 93, 1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (2, 94, 1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (2, 95, 1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (2, 96, 1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (2, 97, 1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (2, 98, 1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (2, 105, 1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (2, 106, 1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (2, 112, 1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (2, 113, 1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (2, 114, 1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (2, 115, 1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (2, 116, 1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (2, 117, 1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (2, 118, 1, 1);

/*****************************
**	Supervisors Default Access
*****************************/
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (3, 1,  1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (3, 2,  1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (3, 3,  1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (3, 4,  1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (3, 5,  1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (3, 6,  1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (3, 7,  1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (3, 8,  1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (3, 9,  1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (3, 10, 1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (3, 11, 1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (3, 12, 1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (3, 13, 1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (3, 14, 1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (3, 15, 1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (3, 16, 1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (3, 17, 1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (3, 18, 1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (3, 19, 1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (3, 20, 1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (3, 21, 1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (3, 22, 1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (3, 23, 1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (3, 24, 1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (3, 25, 1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (3, 26, 1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (3, 27, 1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (3, 28, 1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (3, 29, 1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (3, 30, 1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (3, 31, 1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (3, 32, 1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (3, 33, 1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (3, 34, 1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (3, 35, 1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (3, 36, 1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (3, 37, 1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (3, 38, 1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (3, 39, 1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (3, 40, 1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (3, 41, 1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (3, 42, 1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (3, 43, 1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (3, 44, 1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (3, 45, 1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (3, 46, 1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (3, 47, 1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (3, 48, 1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (3, 49, 1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (3, 50, 1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (3, 51, 1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (3, 52, 1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (3, 53, 1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (3, 54, 1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (3, 55, 1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (3, 56, 1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (3, 57, 1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (3, 58, 1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (3, 59, 1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (3, 60, 1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (3, 61, 1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (3, 62, 1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (3, 63, 1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (3, 64, 1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (3, 65, 1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (3, 66, 1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (3, 67, 1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (3, 68, 1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (3, 69, 1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (3, 70, 1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (3, 71, 1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (3, 72, 1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (3, 73, 1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (3, 74, 1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (3, 75, 1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (3, 76, 1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (3, 77, 1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (3, 78, 1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (3, 79, 1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (3, 80, 1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (3, 81, 1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (3, 82, 1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (3, 83, 1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (3, 84, 1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (3, 85, 1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (3, 86, 1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (3, 87, 1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (3, 88, 1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (3, 89, 1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (3, 90, 1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (3, 91, 1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (3, 92, 1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (3, 93, 1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (3, 94, 1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (3, 95, 1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (3, 96, 1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (3, 97, 1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (3, 98, 1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (3, 105, 1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (3, 106, 1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (3, 112, 1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (3, 113, 1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (3, 114, 1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (3, 115, 1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (3, 116, 1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (3, 117, 1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (3, 118, 1, 1);


/*****************************
**	Cashiers Default Access
*****************************/
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (4, 93, 1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (4, 94, 1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (4, 95, 1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (4, 96, 1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (4, 97, 1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (4, 98, 1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (4, 105, 1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (4, 106, 1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (4, 112, 1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (4, 113, 1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (4, 114, 1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (4, 115, 1, 1);



INSERT INTO sysAccessRights (UID, TranTypeID, AllowRead, AllowWrite) VALUES (1, 93, 1, 1);
INSERT INTO sysAccessRights (UID, TranTypeID, AllowRead, AllowWrite) VALUES (1, 94, 1, 1);
INSERT INTO sysAccessRights (UID, TranTypeID, AllowRead, AllowWrite) VALUES (1, 95, 1, 1);
INSERT INTO sysAccessRights (UID, TranTypeID, AllowRead, AllowWrite) VALUES (1, 96, 1, 1);
INSERT INTO sysAccessRights (UID, TranTypeID, AllowRead, AllowWrite) VALUES (1, 97, 1, 1);
INSERT INTO sysAccessRights (UID, TranTypeID, AllowRead, AllowWrite) VALUES (1, 98, 1, 1);
INSERT INTO sysAccessRights (UID, TranTypeID, AllowRead, AllowWrite) VALUES (1, 105, 1, 1);
INSERT INTO sysAccessRights (UID, TranTypeID, AllowRead, AllowWrite) VALUES (1, 106, 1, 1);
INSERT INTO sysAccessRights (UID, TranTypeID, AllowRead, AllowWrite) VALUES (1, 112, 1, 1);
INSERT INTO sysAccessRights (UID, TranTypeID, AllowRead, AllowWrite) VALUES (1, 113, 1, 1);
INSERT INTO sysAccessRights (UID, TranTypeID, AllowRead, AllowWrite) VALUES (1, 114, 1, 1);
INSERT INTO sysAccessRights (UID, TranTypeID, AllowRead, AllowWrite) VALUES (1, 115, 1, 1);
INSERT INTO sysAccessRights (UID, TranTypeID, AllowRead, AllowWrite) VALUES (1, 116, 1, 1);
INSERT INTO sysAccessRights (UID, TranTypeID, AllowRead, AllowWrite) VALUES (1, 117, 1, 1);
INSERT INTO sysAccessRights (UID, TranTypeID, AllowRead, AllowWrite) VALUES (1, 118, 1, 1);


/*****************************
**	Added on July 28, 2008 for Bank - payment
**	Lemuel E. Aceron
*****************************/
INSERT INTO sysAccessTypes (TypeID, TypeName) VALUES (119, 'Banks');
UPDATE sysAccessTypes SET SequenceNo = 13, Category = '08: Backend - Inventory' WHERE TypeID = 119;

INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (1, 119, 1, 1);

INSERT INTO sysAccessRights (UID, TranTypeID, AllowRead, AllowWrite) VALUES (1, 119, 1, 1);

/*****************************
**	Added on November 28, 2008 for packing terminal
**	Lemuel E. Aceron
*****************************/
INSERT INTO sysAccessTypes (TypeID, TypeName) VALUES (120, 'PackUnpackTransaction');

UPDATE sysAccessTypes SET SequenceNo = 14, Category = '08: Backend - Inventory' WHERE TypeID = 120;

INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (1, 120, 1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (2, 120, 1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (3, 120, 1, 1);

INSERT INTO sysAccessRights (UID, TranTypeID, AllowRead, AllowWrite) VALUES (1, 120, 1, 1);
INSERT INTO sysAccessRights (UID, TranTypeID, AllowRead, AllowWrite) VALUES (2, 120, 1, 1);


/*****************************
**	Added on May 21, 2010 for Synchronizing Products
**	Lemuel E. Aceron
*****************************/
INSERT INTO sysAccessTypes (TypeID, TypeName) VALUES (133, '');

UPDATE sysAccessTypes SET SequenceNo = 14, Category = '05: Backend - MasterFiles - Product' WHERE TypeID = 133;
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (1, 133, 1, 1);
INSERT INTO sysAccessRights (UID, TranTypeID, AllowRead, AllowWrite) VALUES (1, 133, 1, 1);


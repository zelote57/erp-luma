
INSERT INTO sysAccessTypes (TypeID, TypeName) VALUES (122, 'ItemSetupFinancial');
INSERT INTO sysAccessTypes (TypeID, TypeName) VALUES (123, 'APLinkConfig');

INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (1, 122, 1, 1);
INSERT INTO sysAccessGroupRights (GroupID, TranTypeID, AllowRead, AllowWrite) VALUES (1, 123, 1, 1);
INSERT INTO sysAccessRights (UID, TranTypeID, AllowRead, AllowWrite) VALUES (1, 122, 1, 1);
INSERT INTO sysAccessRights (UID, TranTypeID, AllowRead, AllowWrite) VALUES (1, 123, 1, 1);
 
ALTER TABLE tblProducts ADD ChartOfAccountIDPurchase INT(4) UNSIGNED NOT NULL DEFAULT 0;
ALTER TABLE tblProducts ADD ChartOfAccountIDTaxPurchase INT(4) UNSIGNED NOT NULL DEFAULT 0;
ALTER TABLE tblProducts ADD ChartOfAccountIDSold INT(4) UNSIGNED NOT NULL DEFAULT 0;
ALTER TABLE tblProducts ADD ChartOfAccountIDTaxSold INT(4) UNSIGNED NOT NULL DEFAULT 0;
ALTER TABLE tblProducts ADD ChartOfAccountIDInventory INT(4) UNSIGNED NOT NULL DEFAULT 0;

ALTER TABLE tblProductGroup ADD ChartOfAccountIDPurchase INT(4) UNSIGNED NOT NULL DEFAULT 0;
ALTER TABLE tblProductGroup ADD ChartOfAccountIDTaxPurchase INT(4) UNSIGNED NOT NULL DEFAULT 0;
ALTER TABLE tblProductGroup ADD ChartOfAccountIDSold INT(4) UNSIGNED NOT NULL DEFAULT 0;
ALTER TABLE tblProductGroup ADD ChartOfAccountIDTaxSold INT(4) UNSIGNED NOT NULL DEFAULT 0;
ALTER TABLE tblProductGroup ADD ChartOfAccountIDInventory INT(4) UNSIGNED NOT NULL DEFAULT 0; 

ALTER TABLE tblProductSubGroup ADD ChartOfAccountIDPurchase INT(4) UNSIGNED NOT NULL DEFAULT 0;
ALTER TABLE tblProductSubGroup ADD ChartOfAccountIDTaxPurchase INT(4) UNSIGNED NOT NULL DEFAULT 0;
ALTER TABLE tblProductSubGroup ADD ChartOfAccountIDSold INT(4) UNSIGNED NOT NULL DEFAULT 0;
ALTER TABLE tblProductSubGroup ADD ChartOfAccountIDTaxSold INT(4) UNSIGNED NOT NULL DEFAULT 0;
ALTER TABLE tblProductSubGroup ADD ChartOfAccountIDInventory INT(4) UNSIGNED NOT NULL DEFAULT 0; 
 
ALTER TABLE tblERPConfig add ChartOfAccountIDAPTracking INT(4) UNSIGNED NOT NULL DEFAULT 0;
ALTER TABLE tblERPConfig add ChartOfAccountIDAPBills INT(4) UNSIGNED NOT NULL DEFAULT 0;
ALTER TABLE tblERPConfig add ChartOfAccountIDAPFreight INT(4) UNSIGNED NOT NULL DEFAULT 0;
ALTER TABLE tblERPConfig add ChartOfAccountIDAPVDeposit INT(4) UNSIGNED NOT NULL DEFAULT 0;
ALTER TABLE tblERPConfig add ChartOfAccountIDAPContra INT(4) UNSIGNED NOT NULL DEFAULT 0;
ALTER TABLE tblERPConfig add ChartOfAccountIDAPLatePayment INT(4) UNSIGNED NOT NULL DEFAULT 0;
 
 /**************************************************************
** February 8, 2009
** Lemuel E. Aceron
**
** 1.For accounting entries
**	 
**
**************************************************************/
ALTER TABLE tblPO ADD ChartOfAccountIDAPTracking INT(4) UNSIGNED NOT NULL DEFAULT 0;
ALTER TABLE tblPO ADD ChartOfAccountIDAPBills INT(4) UNSIGNED NOT NULL DEFAULT 0;
ALTER TABLE tblPO ADD ChartOfAccountIDAPFreight INT(4) UNSIGNED NOT NULL DEFAULT 0;
ALTER TABLE tblPO ADD ChartOfAccountIDAPVDeposit INT(4) UNSIGNED NOT NULL DEFAULT 0;
ALTER TABLE tblPO ADD ChartOfAccountIDAPContra INT(4) UNSIGNED NOT NULL DEFAULT 0;
ALTER TABLE tblPO ADD ChartOfAccountIDAPLatePayment INT(4) UNSIGNED NOT NULL DEFAULT 0;

ALTER TABLE tblPOItems ADD ChartOfAccountIDPurchase INT(4) UNSIGNED NOT NULL DEFAULT 0;
ALTER TABLE tblPOItems ADD ChartOfAccountIDTaxPurchase INT(4) UNSIGNED NOT NULL DEFAULT 0;
ALTER TABLE tblPOItems ADD ChartOfAccountIDInventory INT(4) UNSIGNED NOT NULL DEFAULT 0;


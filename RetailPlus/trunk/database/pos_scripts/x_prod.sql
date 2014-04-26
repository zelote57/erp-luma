SET FOREIGN_KEY_CHECKS = 0;


DELETE FROM sysAccessRights WHERE UID > 3;

DELETE FROM sysAccessuserDetails WHERE UID > 3;

DELETE FROM tblCashierLogs;
DELETE FROM tblPaidOut;
DELETE FROM tblDisburse;
DELETE FROM tblWithHold;
DELETE FROM tblDeposit;

DELETE FROM tblTransactionItems;
DELETE FROM tblTransactions;

DELETE FROM sysAccessUsers WHERE UID > 3;



TRUNCATE TABLE tblproducthistory;
TRUNCATE TABLE tblproductmovement;
TRUNCATE TABLE tblproductpackagepricehistory;
TRUNCATE TABLE tblproductpurchasepricehistory;


TRUNCATE TABLE tblProductInventory;
TRUNCATE TABLE tblProductBasevariationsMatrix;
TRUNCATE TABLE tblProductVariationsMatrix;

TRUNCATE TABLE tblproductvariations;

TRUNCATE TABLE tblproductcomposition;
TRUNCATE TABLE tblproductunitmatrix;
TRUNCATE TABLE tblproductpackage;
TRUNCATE TABLE tblproductprices;
TRUNCATE TABLE tblproducts;

TRUNCATE TABLE tblproductsubgroupbasevariationsmatrix;
TRUNCATE TABLE tblproductsubgroupcharges;
TRUNCATE TABLE tblproductsubgroupunitmatrix;
TRUNCATE TABLE tblproductsubgroupvariations;
TRUNCATE TABLE tblproductsubgroupvariationsmatrix;
TRUNCATE TABLE tblproductsubgroup;

TRUNCATE TABLE tblproductgroupbasevariationsmatrix;
TRUNCATE TABLE tblproductgroupcharges;
TRUNCATE TABLE tblproductgroupunitmatrix;
TRUNCATE TABLE tblproductgroupvariations;
TRUNCATE TABLE tblproductgroupvariationsmatrix;
TRUNCATE TABLE tblproductgroup;


TRUNCATE TABLE tblcontactcreditcardinfo;
TRUNCATE TABLE tblcontactgroup;
TRUNCATE TABLE tblcontactrewards;
TRUNCATE TABLE tblcontactrewardsmovement;
TRUNCATE TABLE tblContacts;





TRUNCATE TABLE tblso;
TRUNCATE TABLE tblsocreditmemo;
TRUNCATE TABLE tblsocreditmemoitems;
TRUNCATE TABLE tblsoitems;
TRUNCATE TABLE tblstock;
TRUNCATE TABLE tblstockitems;
TRUNCATE TABLE tblstocktype;


SET FOREIGN_KEY_CHECKS = 1;
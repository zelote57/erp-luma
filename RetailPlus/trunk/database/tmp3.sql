



/**************************************************************

	procSyncProductSelect

	Aug 20, 2015 : create this procedure

	Descrition: 
		1. get products information that will be use to sync
		2. not all columns are required to be sync

	CALL procSyncProductSelect('2015-06-29 00:00:00', '2015-06-29 19:00:00');
	
**************************************************************/
delimiter GO
DROP PROCEDURE IF EXISTS procSyncProductSelect
GO

create procedure procSyncProductSelect(IN dteLastModifiedFrom DATETIME, IN dteLastModifiedTo DATETIME)
BEGIN
	
	SELECT ProductID, ProductCode, ProductDesc, ProductSubGroupID, BaseUnitID, DateCreated, Deleted, 
						IncludeInSubtotalDiscount, IsItemSold, WillPrintProductComposition, Active, PercentageCommision,
						RID, RewardPoints, SequenceNo, IsCreditChargeExcluded, LastModified
	FROM tblProducts
	WHERE LastModified >= dteLastModifiedFrom AND LastModified <= dteLastModifiedTo;
	
END;
GO
delimiter ;
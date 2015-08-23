



SELECT ProductID, ProductCode, ProductDesc, ProductSubGroupID, BaseUnitID, DateCreated, Deleted, IncludeInSubtotalDiscount, 
		MinThreshold, MaxThreshold, SupplierID,
		IsItemSold, WillPrintProductComposition, Active, PercentageCommision,
		RID, RIDMinThreshold, RIDMaxThreshold, RewardPoints, SequenceNo,
		CreatedOn, LastModified, IsCreditChargeExcluded
FROM tblProducts 
WHERE LastModified >= '2014-12-31 17:26:47'
ORDER BY LastModified DESC
LIMIT 10;


select *
FROM tblProducts 
WHERE LastModified >= '2014-12-31 17:26:47'
ORDER BY LastModified DESC
LIMIT 10;

select distinct LastModified 
FROM tblProducts 
ORDER BY LastModified DESC
LIMIT 10;
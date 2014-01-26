



select transactionno, transactiondate, CustomerName from tbltransactions
where transactionno in (75762,	75758,	74045,	75760,	75793,	75769,	75773,	75767,	75775,	75777,	74681,	74340,	74358,	74348,	74341,	75797,	75779,	74365,	75785,	75799,	75802,	75805,	75787,	75789,	75791,	75795,	75771,	75764)



UPDATE tblproducts SET deleted = 1 where ProductSubGroupID in (select ProductSubGroupID from tblproductsubgroup where productgroupid = 3 );

select * from tblproductgroup where productgroupname like '%others%';
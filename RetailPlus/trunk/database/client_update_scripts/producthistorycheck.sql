
/*****
Note:	you should run the producthistoryprocedure.sql
		before you can use this procedure.
******/

-- Create the stored procedures to use
source producthistoryprocedure.sql;
--D:\retailplus\RetailPlus\Database\scripts\producthistoryprocedure.sql

-- Set the delimiter back to semi-colon
delimiter ;

-- Check all products and save logs to tblQtySyncToHistory by callng the procedure ProductQtySyncToHistory()
call ProductQtySyncToHistory();

-- Get the records of affected products for update
select * from tblQtySyncToHistory where productqty <> historyqty;


update tblProducts set quantity = (select historyqty from tblQtySyncToHistory 
										where tblQtySyncToHistory.productid = tblProducts.productid 
										and productqty <> historyqty) ;


select * from tblproducts where productcode like '%nestle%';

call ProductHistorySummary(450)
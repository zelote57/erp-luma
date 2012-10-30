 /***************************************************
 Description:
 
 	For synchronizing quantity of all products 
 		to the total quantity of all matrix.
 	
 	Example: 
 		Product: Shoes		Qty: 100
 		
 		Matrix:
 				Red-Shoes	Qty: 25
 				Brown-Shoes	Qty: 75

	If the Matrix total is not sync, it will sync.
 ***************************************************/
 
 update tblproducts set quantity = ifnull((select sum(quantity) from tblProductBaseVariationsMatrix where tblproducts.productid = tblProductBaseVariationsMatrix.productid),quantity);

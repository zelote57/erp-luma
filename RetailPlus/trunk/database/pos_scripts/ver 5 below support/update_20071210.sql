 /*****************************
**	Added on December 10, 2007
**	Lemuel E. Aceron
**	Parameterized the display of items in Front-End if items with Quantity 
**	to display only Items with more than zero quantity
**		0 - means false [display all items]
**		1 - means true  [display only more than zero items]
*****************************/ 
alter table tblTerminal add `ShowItemMoreThanZeroQty` TINYINT (1) NOT NULL DEFAULT 0;
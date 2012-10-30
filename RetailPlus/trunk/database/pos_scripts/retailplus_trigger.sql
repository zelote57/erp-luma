

/********************************************
	trgr_tblProducts_Update
********************************************/
delimiter GO
DROP TRIGGER IF EXISTS trgr_tblProducts_Update
GO

CREATE TRIGGER trgr_tblProducts_Update BEFORE UPDATE ON tblProducts
FOR EACH ROW 
BEGIN
	IF (NEW.ProductID == NEW.ProductID) THEN
		SET NEW.LastUpdateDate = CURRENT_TIMESTAMP ; -- WHERE NEW.ProductID = NEW.ProductID;
	END IF;
END;
GO

delimiter ;

update tblproducts set LastUpdateDate = '2001-01-01';
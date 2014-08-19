-- GLA
-- October 10, 2013 
/*****************************
**	tblgla_d_dsc_def
*****************************/
DROP TABLE IF EXISTS tblgla_d_dsc_def;
CREATE TABLE tblgla_d_dsc_def (
  `Dsc_Number` bigint(20) NOT NULL DEFAULT 0,
  `Dsc_Name` varchar(60) DEFAULT NULL,
  `Is_HotelMark_Promo` tinyint(1) NOT NULL DEFAULT 0,
  `DateCreated` datetime NOT NULL,
  `CreatedBy` varchar(120) DEFAULT NULL,
  `Filename` varchar(120) DEFAULT NULL,
  `BatchID` varchar(30) DEFAULT NULL,
  KEY `IX_tblgla_d_dsc_def` (`Dsc_Number`),
  KEY `IX1_tblgla_d_dsc_def` (`BatchID`)
);


-- GLA
-- October 10, 2013 
/*****************************
**	tblgla_d_emp_def
*****************************/
DROP TABLE IF EXISTS tblgla_d_emp_def;
CREATE TABLE tblgla_d_emp_def (
	`Emp_Number` BIGINT NOT NULL DEFAULT 0,
	`First_Name` VARCHAR(60),
	`Last_Name` VARCHAR(60),
	`Class_Number` BIGINT NOT NULL DEFAULT 0,
	`Class_Name` VARCHAR(60),
	`DateCreated` DATETIME NOT NULL ,
	`CreatedBy` VARCHAR(120) NULL,
	`Filename` VARCHAR(120) NULL,
	`BatchID` VARCHAR(30) NULL,
INDEX `IX_tblgla_d_emp_def`(`Emp_Number`),
INDEX `IX1_tblgla_d_emp_def`(`BatchID`)
);


-- GLA
-- October 10, 2013 
/*****************************
**	tblgla_d_location_def
*****************************/
DROP TABLE IF EXISTS tblgla_d_location_def;
CREATE TABLE tblgla_d_location_def (
	`Rvc_Number` BIGINT NOT NULL DEFAULT 0,
	`Rvc_Name` VARCHAR(60),
	`Sales_Itemizer1_Name` VARCHAR(256),
	`Sales_Itemizer2_Name` VARCHAR(256),
	`Sales_Itemizer3_Name` VARCHAR(256),
	`Sales_Itemizer4_Name` VARCHAR(256),
	`Sales_Itemizer5_Name` VARCHAR(256),
	`Sales_Itemizer6_Name` VARCHAR(256),
	`Sales_Itemizer7_Name` VARCHAR(256),
	`Sales_Itemizer8_Name` VARCHAR(256),
	`Sales_Itemizer9_Name` VARCHAR(256),
	`Sales_Itemizer10_Name` VARCHAR(256),
	`Sales_Itemizer11_Name` VARCHAR(256),
	`Sales_Itemizer12_Name` VARCHAR(256),
	`Sales_Itemizer13_Name` VARCHAR(256),
	`Sales_Itemizer14_Name` VARCHAR(256),
	`Sales_Itemizer15_Name` VARCHAR(256),
	`Sales_Itemizer16_Name` VARCHAR(256),
	`DateCreated` DATETIME NOT NULL ,
	`CreatedBy` VARCHAR(120) NULL,
	`Filename` VARCHAR(120) NULL,
	`BatchID` VARCHAR(30) NULL,
INDEX `IX_tblgla_d_location_def`(`Rvc_Number`),
INDEX `IX1_tblgla_d_location_def`(`BatchID`)
);



-- GLA
-- October 10, 2013 
/*****************************
**	tblgla_d_mi_def
*****************************/
DROP TABLE IF EXISTS tblgla_d_mi_def;
CREATE TABLE tblgla_d_mi_def (
	`Rvc_Number` BIGINT NOT NULL DEFAULT 0,
	`Mi_Number` BIGINT NOT NULL DEFAULT 0,
	`Def_Seq` INT NOT NULL DEFAULT 0,
	`Mi_Name` VARCHAR(60),
	`Sales_Itemizer_Number` BIGINT NOT NULL DEFAULT 0,
	`Sales_Itemizer_Name` VARCHAR(60),
	`Family_Group_Number` BIGINT NOT NULL DEFAULT 0,
	`Family_Group_Name` VARCHAR(60),
	`Major_Group_Number` BIGINT NOT NULL DEFAULT 0,
	`Major_Group_Name` VARCHAR(60),
	`Mi_Class_Number` BIGINT NOT NULL DEFAULT 0,
	`Mi_Class_Name` VARCHAR(60),
	`DateCreated` DATETIME NOT NULL ,
	`CreatedBy` VARCHAR(120) NULL,
	`Filename` VARCHAR(120) NULL,
	`BatchID` VARCHAR(30) NULL,
INDEX `IX_tblgla_d_mi_def`(`Rvc_Number`),
INDEX `IX1_tblgla_d_mi_def`(`BatchID`)
);

-- GLA
-- October 10, 2013 
/*****************************
**	tblgla_d_svc_def
*****************************/
DROP TABLE IF EXISTS tblgla_d_svc_def;
CREATE TABLE tblgla_d_svc_def (
	`Svc_Number` BIGINT NOT NULL DEFAULT 0,
	`Svc_Name` VARCHAR(60),
	`DateCreated` DATETIME NOT NULL ,
	`CreatedBy` VARCHAR(120) NULL,
	`Filename` VARCHAR(120) NULL,
	`BatchID` VARCHAR(30) NULL,
INDEX `IX_tblgla_d_svc_def`(`Svc_Number`),
INDEX `IX1_tblgla_d_svc_def`(`BatchID`)
);


-- GLA
-- October 10, 2013 
/*****************************
**	tblgla_d_tmd_def
*****************************/
DROP TABLE IF EXISTS tblgla_d_tmd_def;
CREATE TABLE tblgla_d_tmd_def (
	`Tmd_Number` BIGINT NOT NULL DEFAULT 0,
	`Tmd_Name` VARCHAR(60),
	`DateCreated` DATETIME NOT NULL ,
	`CreatedBy` VARCHAR(120) NULL,
	`Filename` VARCHAR(120) NULL,
	`BatchID` VARCHAR(30) NULL,
INDEX `IX_tblgla_d_tmd_def`(`Tmd_Number`),
INDEX `IX1_tblgla_d_tmd_def`(`BatchID`)
);



-- GLA
-- October 10, 2013 
/*****************************
**	tblgla_f_dtl_chk_dsc
*****************************/
DROP TABLE IF EXISTS tblgla_f_dtl_chk_dsc;
CREATE TABLE tblgla_f_dtl_chk_dsc (
	`fk_business_date` DATETIME NULL,
	`fk_location_def` INT NOT NULL DEFAULT 0,
	`fk_emp_def` BIGINT NOT NULL DEFAULT 0,
	`fk_chk_headers` BIGINT NOT NULL DEFAULT 0,
	`fk_dsc_def` INT NOT NULL DEFAULT 0,
	`fk_auth_emp_def` INT NOT NULL DEFAULT 0,
	`Transaction_Date_Time` DATETIME,
	`status_flag` VARCHAR(8),
	`Round_Num` INT NOT NULL DEFAULT 0,
	`Dtl_Num` INT NOT NULL DEFAULT 0,
	`Dsc_Count` INT NOT NULL DEFAULT 0,	
	`Dsc_Total` DECIMAL(18,3) NOT NULL DEFAULT 0,
	`Ref_Info_1` VARCHAR(60),
	`Is_HotelMark_Promo` TINYINT(1) NOT NULL DEFAULT 0,
	`ContactCode` VARCHAR(30),
	`DateCreated` DATETIME NOT NULL ,
	`CreatedBy` VARCHAR(120) NULL,
	`Filename` VARCHAR(120) NULL,
	`BatchID` VARCHAR(30) NULL,
INDEX `IX_tblgla_f_dtl_chk_dsc`(`fk_chk_headers`),
INDEX `IX1_tblgla_f_dtl_chk_dsc`(`fk_dsc_def`)
);

-- GLA
-- October 10, 2013 
/*****************************
**	tblgla_f_dtl_chk_headers
*****************************/
DROP TABLE IF EXISTS tblgla_f_dtl_chk_headers;
CREATE TABLE tblgla_f_dtl_chk_headers (
	`fk_business_date` DATETIME NULL,
	`fk_location_def` INT NOT NULL DEFAULT 0,
	`fk_emp_def` BIGINT NOT NULL DEFAULT 0,
	`status_flag` VARCHAR(8),
	`chk_headers_seq_number` BIGINT NOT NULL DEFAULT 0,
	`chk_num` INT NOT NULL DEFAULT 0,
	`chk_id` VARCHAR(60) NULL,
	`ot_number` INT NOT NULL DEFAULT 0,
	`Ot_Name` VARCHAR(60) NULL,
	`Tbl_Number` INT NOT NULL DEFAULT 0,
	`Chk_Open_Date_Time` DATETIME,
	`Chk_Closed_Date_Time` DATETIME,
	`Uws_Number` INT NOT NULL DEFAULT 0,
	`Is_HotelMark_Promo` TINYINT(1) NOT NULL DEFAULT 0,
	`Sub_Ttl` DECIMAL(18,3) NOT NULL DEFAULT 0,
	`Tax_Ttl` DECIMAL(18,3) NOT NULL DEFAULT 0,
	`Auto_Svc_Ttl` DECIMAL(18,3) NOT NULL DEFAULT 0,
	`Other_Svc_Ttl` DECIMAL(18,3) NOT NULL DEFAULT 0,
	`Dsc_Ttl` DECIMAL(18,3) NOT NULL DEFAULT 0,
	`Pymnt_Ttl` DECIMAL(18,3) NOT NULL DEFAULT 0,
	`Chk_Prntd_Cnt` INT NOT NULL DEFAULT 0,
	`Cov_Cnt` INT NOT NULL DEFAULT 0,
	`Num_Dtl` INT NOT NULL DEFAULT 0,
	`Itemizer1` DECIMAL(18,3) NOT NULL DEFAULT 0,
	`Itemizer2` DECIMAL(18,3) NOT NULL DEFAULT 0,
	`Itemizer3` DECIMAL(18,3) NOT NULL DEFAULT 0,
	`Itemizer4` DECIMAL(18,3) NOT NULL DEFAULT 0,
	`Itemizer5` DECIMAL(18,3) NOT NULL DEFAULT 0,
	`Itemizer6` DECIMAL(18,3) NOT NULL DEFAULT 0,
	`Itemizer7` DECIMAL(18,3) NOT NULL DEFAULT 0,
	`Itemizer8` DECIMAL(18,3) NOT NULL DEFAULT 0,
	`Itemizer9` DECIMAL(18,3) NOT NULL DEFAULT 0,
	`Itemizer10` DECIMAL(18,3) NOT NULL DEFAULT 0,
	`Itemizer11` DECIMAL(18,3) NOT NULL DEFAULT 0,
	`Itemizer12` DECIMAL(18,3) NOT NULL DEFAULT 0,
	`Itemizer13` DECIMAL(18,3) NOT NULL DEFAULT 0,
	`Itemizer14` DECIMAL(18,3) NOT NULL DEFAULT 0,
	`Itemizer15` DECIMAL(18,3) NOT NULL DEFAULT 0,
	`Itemizer16` DECIMAL(18,3) NOT NULL DEFAULT 0,
	`Tip_ttl` DECIMAL(18,3) NOT NULL DEFAULT 0,
	`DateCreated` DATETIME NOT NULL ,
	`CreatedBy` VARCHAR(120) NULL,
	`Filename` VARCHAR(120) NULL,
	`BatchID` VARCHAR(30) NULL,
INDEX `IX_tblgla_f_dtl_chk_headers`(`chk_headers_seq_number`),
INDEX `IX1_tblgla_f_dtl_chk_headers`(`chk_num`)
);



-- GLA
-- October 10, 2013 
/*****************************
**	tblgla_f_dtl_chk_mi
*****************************/
DROP TABLE IF EXISTS tblgla_f_dtl_chk_mi;
CREATE TABLE tblgla_f_dtl_chk_mi (
	`fk_business_date` DATETIME NULL,
	`fk_location_def` INT NOT NULL DEFAULT 0,
	`fk_emp_def` BIGINT NOT NULL DEFAULT 0,
	`fk_chk_headers` BIGINT NOT NULL DEFAULT 0,
	`fk_mi_def` INT NOT NULL DEFAULT 0,
	`Def_Seq` INT NOT NULL DEFAULT 0,
	`fk_auth_emp_def` INT NOT NULL DEFAULT 0,
	`Transaction_Date_Time` DATETIME,
	`status_flag` VARCHAR(8),
	`Round_Num` INT NOT NULL DEFAULT 0,
	`Dtl_Num` INT NOT NULL DEFAULT 0,
	`Dsc_Count` INT NOT NULL DEFAULT 0,	
	`Item_Count` INT NOT NULL DEFAULT 0,
	`Item_Total` DECIMAL(18,3) NOT NULL DEFAULT 0,
	`Ref_Info_1` VARCHAR(60),
	`DateCreated` DATETIME NOT NULL ,
	`CreatedBy` VARCHAR(120) NULL,
	`Filename` VARCHAR(120) NULL,
	`BatchID` VARCHAR(30) NULL,
INDEX `IX_tblgla_f_dtl_chk_mi`(`fk_chk_headers`),
INDEX `IX1_tblgla_f_dtl_chk_mi`(`BatchID`)
);




-- GLA
-- October 10, 2013 
/*****************************
**	tblgla_f_dtl_chk_svc
*****************************/
DROP TABLE IF EXISTS tblgla_f_dtl_chk_svc;
CREATE TABLE tblgla_f_dtl_chk_svc (
	`fk_business_date` DATETIME NULL,
	`fk_location_def` INT NOT NULL DEFAULT 0,
	`fk_emp_def` BIGINT NOT NULL DEFAULT 0,
	`fk_chk_headers` BIGINT NOT NULL DEFAULT 0,
	`fk_svc_def` INT NOT NULL DEFAULT 0,
	`fk_auth_emp_def` INT NOT NULL DEFAULT 0,
	`Transaction_Date_Time` DATETIME,
	`status_flag` VARCHAR(8),
	`Round_Num` INT NOT NULL DEFAULT 0,
	`Dtl_Num` INT NOT NULL DEFAULT 0,
	`Dsc_Count` INT NOT NULL DEFAULT 0,	
	`Svc_Count` INT NOT NULL DEFAULT 0,
	`Svc_Total` DECIMAL(18,3) NOT NULL DEFAULT 0,
	`Ref_Info_1` VARCHAR(60),
	`DateCreated` DATETIME NOT NULL ,
	`CreatedBy` VARCHAR(120) NULL,
	`Filename` VARCHAR(120) NULL,
	`BatchID` VARCHAR(30) NULL,
INDEX `IX_tblgla_f_dtl_chk_svc`(`fk_chk_headers`),
INDEX `IX1_tblgla_f_dtl_chk_svc`(`BatchID`)
);




-- GLA
-- October 10, 2013 
/*****************************
**	tblgla_f_dtl_chk_tmd
*****************************/
DROP TABLE IF EXISTS tblgla_f_dtl_chk_tmd;
CREATE TABLE tblgla_f_dtl_chk_tmd (
	`fk_business_date` DATETIME NULL,
	`fk_location_def` INT NOT NULL DEFAULT 0,
	`fk_emp_def` BIGINT NOT NULL DEFAULT 0,
	`fk_chk_headers` BIGINT NOT NULL DEFAULT 0,
	`fk_tmd_def` INT NOT NULL DEFAULT 0,
	`fk_auth_emp_def` INT NOT NULL DEFAULT 0,
	`Transaction_Date_Time` DATETIME,
	`status_flag` VARCHAR(8),
	`Round_Num` INT NOT NULL DEFAULT 0,
	`Dtl_Num` INT NOT NULL DEFAULT 0,
	`Dsc_Count` INT NOT NULL DEFAULT 0,	
	`Tender_Count` INT NOT NULL DEFAULT 0,
	`Tender_Total` DECIMAL(18,3) NOT NULL DEFAULT 0,
	`Chgd_Tip_Ttl` DECIMAL(18,3) NOT NULL DEFAULT 0,	-- Tip_amt
	`Ref_Info_1` VARCHAR(60),
	`DateCreated` DATETIME NOT NULL ,
	`CreatedBy` VARCHAR(120) NULL,
	`Filename` VARCHAR(120) NULL,
	`BatchID` VARCHAR(30) NULL,
INDEX `IX_tblgla_f_dtl_chk_tmd`(`fk_chk_headers`),
INDEX `IX1_tblgla_f_dtl_chk_tmd`(`BatchID`)
);







/*****************************
**	tblgla_order_tender
*****************************/
DROP TABLE IF EXISTS tblgla_order_tender;
CREATE TABLE tblgla_order_tender (
	`identifier` VARCHAR(50) NULL,
	`order_hdr_id` BIGINT NOT NULL DEFAULT 0 COMMENT 'Reference: tblgla_f_dtl_chk_headers.chk_headers_seq_number',
	`tender_seq` BIGINT NOT NULL DEFAULT 0,
	`tender_id` BIGINT NOT NULL DEFAULT 0,
	`tender_amt` DECIMAL(18,3) NOT NULL DEFAULT 0,
	`prorata_sales_amt_gross` DECIMAL(18,3) NOT NULL DEFAULT 0,
	`prorata_discount_amt` DECIMAL(18,3) NOT NULL DEFAULT 0,
	`prorata_tax_amt` DECIMAL(18,3) NOT NULL DEFAULT 0,
	`prorata_grat_amt` DECIMAL(18,3) NOT NULL DEFAULT 0,
	`prorata_svc_chg_amt` DECIMAL(18,3) NOT NULL DEFAULT 0,
	`tip_amt` DECIMAL(18,3) NOT NULL DEFAULT 0,
	`breakage_amt` DECIMAL(18,3) NOT NULL DEFAULT 0,
	`received_curr_amt` DECIMAL(18,3) NOT NULL DEFAULT 0,
	`curr_decimal_places` INT NOT NULL DEFAULT 0 COMMENT 'Number of decimal places required to print on report for this foregin currency',
	`exchange_rate` INT NOT NULL DEFAULT 0,
	`change_amt` DECIMAL(18,3) NOT NULL DEFAULT 0,
	`change_tender_id` INT NOT NULL DEFAULT 0,
	`tax_removed_code` INT NOT NULL DEFAULT 0,
	`tender_type_id` INT NOT NULL DEFAULT 0,
	`subtender_id` INT NOT NULL DEFAULT 0,
	`auth_acct_no` VARCHAR(250) NULL,
	`post_acct_no` VARCHAR(250) NULL,
	`customer_name` VARCHAR(250) NULL COMMENT 'Customer name for Credit Card, room, PMSI, GA, Comp, Coupon=null',
	`adtnl_info` VARCHAR(400) NULL COMMENT 'Additional input from the terminal',
	`subtender_qty` INT NOT NULL DEFAULT 0,
	`charges_to_date_amt` DECIMAL(18,3) NOT NULL DEFAULT 0,
	`remaining_balance_amt` DECIMAL(18,3) NOT NULL DEFAULT 0,
	`PMS_post_flag` TINYINT(1) NOT NULL DEFAULT 0,
	`sales_tippable_flag` TINYINT(1) NOT NULL DEFAULT 0,
	`post_system1_flag` TINYINT(1) NOT NULL DEFAULT 0,
	`post_system2_flag` TINYINT(1) NOT NULL DEFAULT 0,
	`post_system3_flag` TINYINT(1) NOT NULL DEFAULT 0,
	`post_system4_flag` TINYINT(1) NOT NULL DEFAULT 0,
	`post_system5_flag` TINYINT(1) NOT NULL DEFAULT 0,
	`post_system6_flag` TINYINT(1) NOT NULL DEFAULT 0,
	`post_system7_flag` TINYINT(1) NOT NULL DEFAULT 0,
	`DateCreated` DATETIME NOT NULL ,
	`CreatedBy` VARCHAR(120) NULL,
	`Filename` VARCHAR(120) NULL,
	`BatchID` VARCHAR(30) NULL,
INDEX `IX_tblgla_order_tender`(`order_hdr_id`),
INDEX `IX1_tblgla_order_tender`(`customer_name`)
);


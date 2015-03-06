using System;
using System.Security.Permissions;
using MySql.Data.MySqlClient;

namespace AceSoft.RetailPlus.Data
{

	[StrongNameIdentityPermissionAttribute(SecurityAction.LinkDemand,
		 PublicKey = "002400000480000094000000060200000024000" +
		 "052534131000400000100010053D785642F9F960B43157E0380" +
		 "F393BEE53E8DFAFBF441366C1B6F8B48D9DDF0D527B1F3B21EA" +
		 "E85D2FDB664CE81EB8A87DBE4C589D6F4202FE2B7D4B978BB69" +
		 "684874612CB9B8DB7A0339400A9C4E68277884B07817363D242" +
		 "E3696F9FACDBEA831810AE6DC9EDCA91A7B5DA12FE7BF65D113" +
		 "FF52834EAFB5A7A1FDFD5851A3")]
	public struct ProductPackageDetails
	{
		public Int64 PackageID;
		public Int64 ProductID;
        public Int64 MatrixID;
		public Int32 UnitID;
		public string UnitCode;
		public string UnitName;
        public decimal Price;
        public decimal Price1;
        public decimal Price2;
        public decimal Price3;
        public decimal Price4;
        public decimal Price5;
        public decimal WSPrice;
        public decimal PurchasePrice;
		public decimal Quantity;
		public decimal VAT;
		public decimal EVAT;
		public decimal LocalTax;
        public string BarCode1;
        public string BarCode2;
        public string BarCode3;
        public string BarCode4;
        public string ProductDesc;

        public DateTime CreatedOn;
        public DateTime LastModified;
	}

    public struct ProductPackageColumns
    {
        public bool PackageID;
        public bool ProductID;
        public bool MatrixID;
        public bool UnitID;
        public bool UnitCode;
        public bool UnitName;
        public bool Price;
        public bool Price1;
        public bool Price2;
        public bool Price3;
        public bool Price4;
        public bool Price5;
        public bool WSPrice;
        public bool PurchasePrice;
        public bool Quantity;
        public bool VAT;
        public bool EVAT;
        public bool LocalTax;
        public bool BarCode1;
        public bool BarCode2;
        public bool BarCode3;
        public bool ProductDesc;
    }

    public struct ProductPackageColumnNames
    {
        public const string PackageID = "PackageID";
        public const string ProductID = "ProductID";
        public const string UnitID = "UnitID";
        public const string UnitCode = "UnitCode";
        public const string UnitName = "UnitName";
        public const string PurchasePrice = "PurchasePrice";
        public const string Price = "Price";
        public const string Price1 = "Price1";
        public const string Price2 = "Price2";
        public const string Price3 = "Price3";
        public const string Price4 = "Price4";
        public const string Price5 = "Price5";
        public const string WSPrice = "WSPrice";
        public const string Quantity = "Quantity";
        public const string VAT = "VAT";
        public const string EVAT = "EVAT";
        public const string LocalTax = "LocalTax";
        public const string BarCode1 = "BarCode1";
        public const string BarCode2 = "BarCode2";
        public const string BarCode3 = "BarCode3";
        public const string ProductDesc = "ProductDesc";
        public const string MatrixID = "MatrixID";
    }

	[StrongNameIdentityPermissionAttribute(SecurityAction.LinkDemand,
		 PublicKey = "002400000480000094000000060200000024000" +
		 "052534131000400000100010053D785642F9F960B43157E0380" +
		 "F393BEE53E8DFAFBF441366C1B6F8B48D9DDF0D527B1F3B21EA" +
		 "E85D2FDB664CE81EB8A87DBE4C589D6F4202FE2B7D4B978BB69" +
		 "684874612CB9B8DB7A0339400A9C4E68277884B07817363D242" +
		 "E3696F9FACDBEA831810AE6DC9EDCA91A7B5DA12FE7BF65D113" +
		 "FF52834EAFB5A7A1FDFD5851A3")]
	public class ProductPackage : POSConnection
    {
		#region Constructors and Destructors

		public ProductPackage()
            : base(null, null)
        {
        }

        public ProductPackage(MySqlConnection Connection, MySqlTransaction Transaction) 
            : base(Connection, Transaction)
		{

		}

		#endregion
		
		#region Insert and Update

		public void Insert(ProductPackageDetails Details)
		{
			try  
			{
                Save(Details, 1, DateTime.Now, "");
			}
			catch (Exception ex)
			{
				throw base.ThrowException(ex);
			}	
		}
        public void Update(ProductPackageDetails Details, long pvtUID, DateTime pvtChangeDate, string pvtHistoryRemarks)
		{
			try  
			{
                // Update ProductPackagePriceHistory first to get the history
                ProductPackagePriceHistoryDetails clsProductPackagePriceHistoryDetails = new ProductPackagePriceHistoryDetails();
                clsProductPackagePriceHistoryDetails.UID = pvtUID;
                clsProductPackagePriceHistoryDetails.PackageID = Details.PackageID;
                clsProductPackagePriceHistoryDetails.ChangeDate = pvtChangeDate;
                clsProductPackagePriceHistoryDetails.PurchasePrice = Details.PurchasePrice;
                clsProductPackagePriceHistoryDetails.Price = Details.Price;
                clsProductPackagePriceHistoryDetails.Price1 = Details.Price1;
                clsProductPackagePriceHistoryDetails.Price2 = Details.Price2;
                clsProductPackagePriceHistoryDetails.Price3 = Details.Price3;
                clsProductPackagePriceHistoryDetails.Price4 = Details.Price4;
                clsProductPackagePriceHistoryDetails.Price5 = Details.Price5;
                clsProductPackagePriceHistoryDetails.VAT = Details.VAT;
                clsProductPackagePriceHistoryDetails.EVAT = Details.EVAT;
                clsProductPackagePriceHistoryDetails.LocalTax = Details.LocalTax;
                clsProductPackagePriceHistoryDetails.Remarks = pvtHistoryRemarks;

                ProductPackagePriceHistory clsProductPackagePriceHistory = new ProductPackagePriceHistory(base.Connection, base.Transaction);
                clsProductPackagePriceHistory.Insert(clsProductPackagePriceHistoryDetails);

                Save(Details, pvtUID, pvtChangeDate, pvtHistoryRemarks);
			}

			catch (Exception ex)
			{
				throw base.ThrowException(ex);
			}	
		}

        private void Save(ProductPackageDetails Details, long pvtUID, DateTime pvtChangeDate, string pvtHistoryRemarks)
        {
            try
            {
                string SQL = "CALL procProductPackageSave(@PackageID, @ProductID, @MatrixID, @UnitID, @PurchasePrice, " +
                                                         "@Price, @Price1, @Price2, @Price3, @Price4, @Price5, " +
                                                         "@WSPrice, @Quantity, @VAT, @EVAT, @LocalTax, @BarCode1, @BarCode2, @BarCode3);";

                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandText = SQL;

                cmd.Parameters.AddWithValue("@PackageID", Details.PackageID);
                cmd.Parameters.AddWithValue("@ProductID", Details.ProductID);
                cmd.Parameters.AddWithValue("@MatrixID", Details.MatrixID);
                cmd.Parameters.AddWithValue("@UnitID", Details.UnitID);
                cmd.Parameters.AddWithValue("@PurchasePrice", Details.PurchasePrice);
                cmd.Parameters.AddWithValue("@Price", Details.Price);
                cmd.Parameters.AddWithValue("@Price1", Details.Price1);
                cmd.Parameters.AddWithValue("@Price2", Details.Price2);
                cmd.Parameters.AddWithValue("@Price3", Details.Price3);
                cmd.Parameters.AddWithValue("@Price4", Details.Price4);
                cmd.Parameters.AddWithValue("@Price5", Details.Price5);
                cmd.Parameters.AddWithValue("@WSPrice", Details.WSPrice);
                cmd.Parameters.AddWithValue("@Quantity", Details.Quantity);
                cmd.Parameters.AddWithValue("@VAT", Details.VAT);
                cmd.Parameters.AddWithValue("@EVAT", Details.EVAT);
                cmd.Parameters.AddWithValue("@LocalTax", Details.LocalTax);
                cmd.Parameters.AddWithValue("@BarCode1", Details.BarCode1);
                cmd.Parameters.AddWithValue("@BarCode2", Details.BarCode2);
                cmd.Parameters.AddWithValue("@BarCode3", Details.BarCode3);

                base.ExecuteNonQuery(cmd);
            }

            catch (Exception ex)
            {
                throw base.ThrowException(ex);
            }
        }

        public void UpdateByProductIDUnitIDAndQuantity(ProductPackageDetails Details, long pvtUID, DateTime pvtChangeDate, string pvtHistoryRemarks)
		{
			try  
			{
                string SQL = "SELECT PackageID FROM tblProductPackage " +
                                "WHERE MatrixID = @MatrixID " +
                                    "AND ProductID = @ProductID " +
                                    "AND UnitID		=	@UnitID " +
                                    "AND Quantity	=	@Quantity;";
	 			
				MySqlCommand cmd = new MySqlCommand();
				cmd.CommandType = System.Data.CommandType.Text;
				cmd.CommandText = SQL;
				
                cmd.Parameters.AddWithValue("@ProductID", Details.ProductID);
                cmd.Parameters.AddWithValue("@MatrixID", Details.MatrixID);
                cmd.Parameters.AddWithValue("@UnitID", Details.UnitID);
                cmd.Parameters.AddWithValue("@Quantity", Details.Quantity);

                System.Data.DataTable dt = new System.Data.DataTable("tblProductPackage");
                base.MySqlDataAdapterFill(cmd, dt);

                foreach(System.Data.DataRow dr in dt.Rows)
                { Details.PackageID = Int64.Parse(dr["PackageID"].ToString()); }

                Update(Details, pvtUID, pvtChangeDate, pvtHistoryRemarks);
            }

			catch (Exception ex)
			{
				throw base.ThrowException(ex);
			}	
		}

        /// <summary>
        /// Update all product packages with equal productID. variations will be affected.
        /// </summary>
        /// <param name="ProductID"></param>
        /// <param name="UnitID"></param>
        /// <param name="Quantity"></param>
        /// <param name="PurchasePrice"></param>
		public void UpdatePurchasing(long ProductID, long MatrixID, int UnitID, decimal Quantity, decimal PurchasePrice)
		{
			try  
			{
				string SQL="UPDATE tblProductPackage SET " +
                                "PurchasePrice	=   @PurchasePrice, " +
                                "WSPrice	    =   @PurchasePrice * (1 + ((SELECT WSPriceMarkUp FROM tblTerminal LIMIT 1) / 100)) " +
							"WHERE ProductID	=	@ProductID " +
                                "AND MatrixID	=	@MatrixID " +
							    "AND UnitID		=	@UnitID " +
							    "AND Quantity	=	@Quantity;";
				  
				MySqlCommand cmd = new MySqlCommand();
				cmd.CommandType = System.Data.CommandType.Text;
				cmd.CommandText = SQL;

                cmd.Parameters.AddWithValue("@ProductID", ProductID);
                cmd.Parameters.AddWithValue("@MatrixID", MatrixID);
                cmd.Parameters.AddWithValue("@UnitID", UnitID);
                cmd.Parameters.AddWithValue("@PurchasePrice", PurchasePrice);
                cmd.Parameters.AddWithValue("@Quantity", Quantity);

				base.ExecuteNonQuery(cmd);
			}

			catch (Exception ex)
			{
				throw base.ThrowException(ex);
			}	
		}

        /// <summary>
        /// If Price1..Price5 value is -1, Price1..Price5 will not change.
        /// </summary>
        /// <param name="ProductID"></param>
        /// <param name="MatrixID"></param>
        /// <param name="UnitID"></param>
        /// <param name="Quantity"></param>
        /// <param name="SellingPrice"></param>
        /// <param name="Price1"></param>
        /// <param name="Price2"></param>
        /// <param name="Price3"></param>
        /// <param name="Price4"></param>
        /// <param name="Price5"></param>
        public void UpdateSelling(long ProductID, long MatrixID, int UnitID, decimal Quantity, decimal SellingPrice, decimal Price1, decimal Price2, decimal Price3, decimal Price4, decimal Price5)
		{
			try  
			{
				string SQL="UPDATE tblProductPackage SET " +
								"Price			=	@Price, " +
                                "Price1			=	IF(@Price1=-1, Price1, @Price1), " +
                                "Price2			=	IF(@Price2=-1, Price2, @Price2), " +
                                "Price3			=	IF(@Price3=-1, Price3, @Price3), " +
                                "Price4			=	IF(@Price4=-1, Price4, @Price4), " +
                                "Price5			=	IF(@Price5=-1, Price5, @Price5) " +
							"WHERE ProductID	=	@ProductID " +
                                "AND MatrixID	=	@MatrixID " +
							    "AND UnitID		=	@UnitID " +
							    "AND Quantity	=	@Quantity;";
				  
				MySqlCommand cmd = new MySqlCommand();
				cmd.CommandType = System.Data.CommandType.Text;
				cmd.CommandText = SQL;

                cmd.Parameters.AddWithValue("@ProductID", ProductID);
                cmd.Parameters.AddWithValue("@MatrixID", MatrixID);
                cmd.Parameters.AddWithValue("@UnitID", UnitID);
                cmd.Parameters.AddWithValue("@Price", SellingPrice);
                cmd.Parameters.AddWithValue("@Price1", Price1);
                cmd.Parameters.AddWithValue("@Price2", Price2);
                cmd.Parameters.AddWithValue("@Price3", Price3);
                cmd.Parameters.AddWithValue("@Price4", Price4);
                cmd.Parameters.AddWithValue("@Price5", Price5);
                cmd.Parameters.AddWithValue("@Quantity", Quantity);
				
				base.ExecuteNonQuery(cmd);
			}

			catch (Exception ex)
			{
				throw base.ThrowException(ex);
			}	
		}
        public void UpdateSellingWSPrice(long ProductID, long MatrixID, int UnitID, decimal Quantity, decimal WholeSalePrice)
        {
            try
            {
                string SQL = "UPDATE tblProductPackage SET " +
                                "WSPrice			=	@WSPrice " +
                            "WHERE ProductID	=	@ProductID " +
                                "AND MatrixID	=	@MatrixID " +
                                "AND UnitID		=	@UnitID " +
                                "AND Quantity	=	@Quantity;";

                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandText = SQL;

                cmd.Parameters.AddWithValue("@ProductID", ProductID);
                cmd.Parameters.AddWithValue("@MatrixID", MatrixID);
                cmd.Parameters.AddWithValue("@UnitID", UnitID);
                cmd.Parameters.AddWithValue("@WSPrice", WholeSalePrice);
                cmd.Parameters.AddWithValue("@Quantity", Quantity);

                base.ExecuteNonQuery(cmd);
            }

            catch (Exception ex)
            {
                throw base.ThrowException(ex);
            }
        }

        // Dec 10, 2011 : Obsolete, change to ChangeTax
        //public void ChangeVAT(decimal OldVAT, decimal NewVAT)
        //{
        //    try 
        //    {
        //        string SQL =	"UPDATE tblProductPackage SET " +
        //                            "VAT		= @NewVAT " +
        //                        "WHERE VAT		= @OldVAT;";
				  
        //        
	 			
        //        MySqlCommand cmd = new MySqlCommand();
        //        
        //        
        //        cmd.CommandType = System.Data.CommandType.Text;
        //        cmd.CommandText = SQL;
				
        //        MySqlParameter prmNewVAT = new MySqlParameter("@NewVAT",MySqlDbType.Decimal);			
        //        prmNewVAT.Value = NewVAT;
        //        cmd.Parameters.Add(prmNewVAT);

        //        MySqlParameter prmOldVAT = new MySqlParameter("@OldVAT",MySqlDbType.Decimal);			
        //        prmOldVAT.Value = OldVAT;
        //        cmd.Parameters.Add(prmOldVAT);

        //        base.ExecuteNonQuery(cmd);
        //    }

        //    catch (Exception ex)
        //    {
        //        
        //        
        //        {
        //            
        //            
        //            
        //            
        //        }

        //        throw base.ThrowException(ex);
        //    }	
        //}

        // Dec 10, 2011 : Obsolete, change to ChangeTax
        //public void ChangeEVAT(decimal OldEVAT, decimal NewEVAT)
        //{
        //    try  
        //    {
        //        string SQL="UPDATE tblProductPackage SET " +
        //                        "EVAT		=	@NewEVAT " +
        //                    "WHERE EVAT		=	@OldEVAT;";
				  
        //        
	 			
        //        MySqlCommand cmd = new MySqlCommand();
        //        
        //        
        //        cmd.CommandType = System.Data.CommandType.Text;
        //        cmd.CommandText = SQL;
				
        //        MySqlParameter prmNewEVAT = new MySqlParameter("@NewEVAT",MySqlDbType.Decimal);			
        //        prmNewEVAT.Value = NewEVAT;
        //        cmd.Parameters.Add(prmNewEVAT);

        //        MySqlParameter prmOldEVAT = new MySqlParameter("@OldEVAT",MySqlDbType.Decimal);			
        //        prmOldEVAT.Value = OldEVAT;
        //        cmd.Parameters.Add(prmOldEVAT);

        //        base.ExecuteNonQuery(cmd);

        //    }

        //    catch (Exception ex)
        //    {
        //        
        //        
        //        {
        //            
        //            
        //            
        //            
        //        }

        //        throw base.ThrowException(ex);
        //    }	
        //}

        // Dec 10, 2011 : Obsolete, change to ChangeTax
        //public void ChangeLocalTax(decimal OldLocalTax, decimal NewLocalTax)
        //{
        //    try 
        //    {
        //        string SQL =	"UPDATE tblProductPackage SET " +
        //                            "LocalTax		= @NewLocalTax " +
        //                        "WHERE LocalTax		= @OldLocalTax;";
				  
        //        
	 			
        //        MySqlCommand cmd = new MySqlCommand();
        //        
        //        
        //        cmd.CommandType = System.Data.CommandType.Text;
        //        cmd.CommandText = SQL;
				
        //        MySqlParameter prmNewLocalTax = new MySqlParameter("@NewLocalTax",MySqlDbType.Decimal);			
        //        prmNewLocalTax.Value = NewLocalTax;
        //        cmd.Parameters.Add(prmNewLocalTax);

        //        MySqlParameter prmOldLocalTax = new MySqlParameter("@OldLocalTax",MySqlDbType.Decimal);			
        //        prmOldLocalTax.Value = OldLocalTax;
        //        cmd.Parameters.Add(prmOldLocalTax);

        //        base.ExecuteNonQuery(cmd);
        //    }

        //    catch (Exception ex)
        //    {
        //        
        //        
        //        {
        //            
        //            
        //            
        //            
        //        }

        //        throw base.ThrowException(ex);
        //    }	
        //}

        public void ChangeTax(long ProductGroupID, long ProductSubGroupID, long ProductID, decimal NewVAT, decimal NewEVAT, decimal NewLocalTax, string CreatedBy)
        {
            try
            {
                string SQL = "CALL procProductUpdateVAT(@ProductGroupID, @ProductSubGroupID, @ProductID, @NewVAT, @NewEVAT, @NewLocalTax, @CreatedBy);";

                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandText = SQL;

                cmd.Parameters.AddWithValue("@ProductGroupID", ProductGroupID);
                cmd.Parameters.AddWithValue("@ProductSubGroupID", ProductSubGroupID);
                cmd.Parameters.AddWithValue("@ProductID", ProductID);
                cmd.Parameters.AddWithValue("@NewVAT", NewVAT);
                cmd.Parameters.AddWithValue("@NewEVAT", NewEVAT);
                cmd.Parameters.AddWithValue("@NewLocalTax", NewLocalTax);
                cmd.Parameters.AddWithValue("@CreatedBy", CreatedBy);

                base.ExecuteNonQuery(cmd);
            }
            catch (Exception ex)
            {
                throw base.ThrowException(ex);
            }
        }

        public Int32 Save(ProductPackageDetails Details)
        {
            try
            {
                string SQL = "CALL procSaveProductPackage(@PackageID, @ProductID, @MatrixID, @UnitID, " +
                                                    "@Price, @Price1, @Price2, @Price3, @Price4, @Price5, @PurchasePrice," +
                                                    "@Quantity, @VAT, @EVAT, @LocalTax, @WSPrice, @Barcode1, " +
                                                    "@Barcode2, @Barcode3, @BarCode4, @CreatedOn, @LastModified);";

                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandText = SQL;

                cmd.Parameters.AddWithValue("PackageID", Details.PackageID);
                cmd.Parameters.AddWithValue("ProductID", Details.ProductID);
                cmd.Parameters.AddWithValue("MatrixID", Details.MatrixID);
                cmd.Parameters.AddWithValue("UnitID", Details.UnitID);
                cmd.Parameters.AddWithValue("Price", Details.Price);
                cmd.Parameters.AddWithValue("Price1", Details.Price1);
                cmd.Parameters.AddWithValue("Price2", Details.Price2);
                cmd.Parameters.AddWithValue("Price3", Details.Price3);
                cmd.Parameters.AddWithValue("Price4", Details.Price4);
                cmd.Parameters.AddWithValue("Price5", Details.Price5);
                cmd.Parameters.AddWithValue("PurchasePrice", Details.PurchasePrice);
                cmd.Parameters.AddWithValue("Quantity", Details.Quantity);
                cmd.Parameters.AddWithValue("VAT", Details.VAT);
                cmd.Parameters.AddWithValue("EVAT", Details.EVAT);
                cmd.Parameters.AddWithValue("LocalTax", Details.LocalTax);
                cmd.Parameters.AddWithValue("WSPrice", Details.WSPrice);
                cmd.Parameters.AddWithValue("BarCode1", Details.BarCode1);
                cmd.Parameters.AddWithValue("BarCode2", Details.BarCode2);
                cmd.Parameters.AddWithValue("BarCode3", Details.BarCode3);
                cmd.Parameters.AddWithValue("BarCode4", Details.BarCode4);
                cmd.Parameters.AddWithValue("CreatedOn", Details.CreatedOn == DateTime.MinValue ? Constants.C_DATE_MIN_VALUE : Details.CreatedOn);
                cmd.Parameters.AddWithValue("LastModified", Details.LastModified == DateTime.MinValue ? Constants.C_DATE_MIN_VALUE : Details.LastModified);

                return base.ExecuteNonQuery(cmd);
            }

            catch (Exception ex)
            {
                throw base.ThrowException(ex);
            }
        }

		#endregion

		#region Delete

		public bool Delete(string IDs)
		{
			try 
			{
				string SQL=	"DELETE FROM tblProductPackage WHERE PackageID IN (" + IDs + ");";
	 			
				MySqlCommand cmd = new MySqlCommand();
				cmd.CommandType = System.Data.CommandType.Text;
				cmd.CommandText = SQL;

				base.ExecuteNonQuery(cmd);

				return true;
			}

			catch (Exception ex)
			{
				throw base.ThrowException(ex);
			}	
		}

		
		#endregion

        private string SQLSelect()
        {
            string stSQL = "SELECT " +
                                "PackageID, " +
                                "a.ProductID, " +
                                "ProductDesc, " +
                                "a.UnitID, " +
                                "c.UnitCode, " +
                                "c.UnitName, " +
                                "a.Price, " +
                                "a.Price1, " +
                                "a.Price2, " +
                                "a.Price3, " +
                                "a.Price4, " +
                                "a.Price5, " +
                                "a.WSPrice, " +
                                "a.PurchasePrice, " +
                                "a.Quantity, " +
                                "a.VAT, " +
                                "a.EVAT, " +
                                "a.LocalTax, " +
                                "a.BarCode1, " +
                                "a.BarCode2, " +
                                "a.BarCode3 " +
                            "FROM tblProductPackage a " +
                            "INNER JOIN tblProducts b ON a.MatrixID = 0 AND a.ProductID = b.ProductID " +
                            "INNER JOIN tblUnit c ON a.UnitID = c.UnitID ";

            return stSQL;
        }

        private string SQLSelect(ProductPackageColumns clsProductPackageColumns)
        {
            string stSQL = "SELECT ";

            if (clsProductPackageColumns.ProductID) stSQL += "tblProductPackage.ProductID, ";
            if (clsProductPackageColumns.MatrixID) stSQL += "tblProductPackage.MatrixID, ";
            if (clsProductPackageColumns.UnitID) stSQL += "tblProductPackage.UnitID, ";
            if (clsProductPackageColumns.UnitCode) stSQL += "tblUnit.UnitCode, ";
            if (clsProductPackageColumns.UnitName) stSQL += "tblUnit.UnitName, ";
            if (clsProductPackageColumns.Price) stSQL += "tblProductPackage.Price, ";
            if (clsProductPackageColumns.Price) stSQL += "tblProductPackage.Price1, ";
            if (clsProductPackageColumns.Price) stSQL += "tblProductPackage.Price2, ";
            if (clsProductPackageColumns.Price) stSQL += "tblProductPackage.Price3, ";
            if (clsProductPackageColumns.Price) stSQL += "tblProductPackage.Price4, ";
            if (clsProductPackageColumns.Price) stSQL += "tblProductPackage.Price5, ";
            if (clsProductPackageColumns.WSPrice) stSQL += "tblProductPackage.WSPrice, ";
            if (clsProductPackageColumns.PurchasePrice) stSQL += "tblProductPackage.PurchasePrice, ";
            if (clsProductPackageColumns.Quantity) stSQL += "tblProductPackage.Quantity, ";
            if (clsProductPackageColumns.VAT) stSQL += "tblProductPackage.VAT, ";
            if (clsProductPackageColumns.EVAT) stSQL += "tblProductPackage.EVAT, ";
            if (clsProductPackageColumns.LocalTax) stSQL += "tblProductPackage.LocalTax, ";
            if (clsProductPackageColumns.BarCode1) stSQL += "tblProductPackage.BarCode1, ";
            if (clsProductPackageColumns.BarCode2) stSQL += "tblProductPackage.BarCode2, ";
            if (clsProductPackageColumns.BarCode3) stSQL += "tblProductPackage.BarCode3, ";
            if (clsProductPackageColumns.ProductDesc) stSQL += "tblProducts.ProductDesc, ";

            stSQL += "tblProductPackage.PackageID ";
            stSQL += "FROM tblProductPackage ";

            if (clsProductPackageColumns.ProductDesc)
                stSQL += "INNER JOIN tblProducts ON tblProductPackage.ProductID = tblProducts.ProductID ";

            if (clsProductPackageColumns.UnitCode || clsProductPackageColumns.UnitName)
                stSQL += "INNER JOIN tblUnit ON tblProductPackage.UnitID = tblUnit.UnitID ";

            return stSQL;
        }

		#region Details

        public ProductPackageColumns getAllPackageColumns()
        {
            ProductPackageColumns clsProductPackageColumns = new ProductPackageColumns();
            clsProductPackageColumns.PackageID = true;
            clsProductPackageColumns.ProductID = true;
            clsProductPackageColumns.MatrixID = true;
            clsProductPackageColumns.UnitID = true;
            clsProductPackageColumns.UnitCode = true;
            clsProductPackageColumns.UnitName = true;
            clsProductPackageColumns.PurchasePrice = true;
            clsProductPackageColumns.Price = true;
            clsProductPackageColumns.Price1 = true;
            clsProductPackageColumns.Price2 = true;
            clsProductPackageColumns.Price3 = true;
            clsProductPackageColumns.Price4 = true;
            clsProductPackageColumns.Price5 = true;
            clsProductPackageColumns.WSPrice = true;
            clsProductPackageColumns.Quantity = true;
            clsProductPackageColumns.VAT = true;
            clsProductPackageColumns.EVAT = true;
            clsProductPackageColumns.LocalTax = true;
            clsProductPackageColumns.BarCode1 = true;
            clsProductPackageColumns.BarCode2 = true;
            clsProductPackageColumns.BarCode3 = true;
            clsProductPackageColumns.ProductDesc = true;

            return clsProductPackageColumns;
        }
        public long GetPackageID(long ProductID, int UnitID)
        {
            try
            {
                string SQL = "SELECT PackageID FROM tblProductPackage WHERE MatrixID = 0 AND ProductID = @ProductID AND UnitID = @UnitID AND Quantity = 1 LIMIT 1;";

                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandText = SQL;

                cmd.Parameters.AddWithValue("@ProductID", ProductID);
                cmd.Parameters.AddWithValue("@UnitID", UnitID);

                System.Data.DataTable dt = new System.Data.DataTable("tblProductPackage");
                base.MySqlDataAdapterFill(cmd, dt); 

                long lngRetValue = 0;

                foreach(System.Data.DataRow dr in dt.Rows)
                {
                    lngRetValue = Int64.Parse(dr["PackageID"].ToString());
                    break;
                }

                return lngRetValue;
            }

            catch (Exception ex)
            {
                throw base.ThrowException(ex);
            }
        }
		public ProductPackageDetails Details(Int64 PackageID)
		{
			try
			{
                ProductPackageColumns clsProductPackageColumns = getAllPackageColumns();
                clsProductPackageColumns.ProductDesc = false;

                string SQL = SQLSelect(clsProductPackageColumns) + " ";
                SQL += "WHERE tblProductPackage.PackageID = @PackageID ";

                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandText = SQL;

                MySqlParameter prmPackageID = new MySqlParameter("@PackageID",MySqlDbType.Int64);
                prmPackageID.Value = PackageID;
                cmd.Parameters.Add(prmPackageID);

                System.Data.DataTable dt = new System.Data.DataTable("tblProductPackage");
                base.MySqlDataAdapterFill(cmd, dt);
                

                ProductPackageDetails clsDetails = setDetails(dt);

                return clsDetails;
			}

			catch (Exception ex)
			{
				throw base.ThrowException(ex);
			}	
		}
		public ProductPackageDetails DetailsByProductID(Int64 ProductID, Int64 MatrixID = 0)
		{
			try
			{
                ProductPackageColumns clsProductPackageColumns = getAllPackageColumns();
                clsProductPackageColumns.ProductDesc = false;

                string SQL = SQLSelect(clsProductPackageColumns) + " ";
                SQL += "WHERE tblProductPackage.ProductID = @ProductID AND MatrixID = @MatrixID ";
                SQL += "ORDER BY tblProductPackage.PackageID ASC LIMIT 1;";

                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandText = SQL;

                MySqlParameter prmProductID = new MySqlParameter("@ProductID", MySqlDbType.Int64);
                prmProductID.Value = ProductID;
                cmd.Parameters.Add(prmProductID);

                MySqlParameter prmMatrixID = new MySqlParameter("@MatrixID", MySqlDbType.Int64);
                prmMatrixID.Value = MatrixID;
                cmd.Parameters.Add(prmMatrixID);

                System.Data.DataTable dt = new System.Data.DataTable("tblProductPackage");
                base.MySqlDataAdapterFill(cmd, dt);

                ProductPackageDetails clsDetails = setDetails(dt);

                return clsDetails;
			}

			catch (Exception ex)
			{
				throw base.ThrowException(ex);
			}	
		}
        public ProductPackageDetails DetailsByProductIDAndUnitID(Int64 ProductID, long UnitID)
        {
            try
            {
                ProductPackageColumns clsProductPackageColumns = getAllPackageColumns();
                clsProductPackageColumns.ProductDesc = false;

                string SQL = SQLSelect(clsProductPackageColumns) + " ";
                SQL += "WHERE tblProductPackage.ProductID = @ProductID ";
                SQL += "    AND tblProductPackage.UnitID = @UnitID ";
                SQL += "LIMIT 1;";

                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandText = SQL;

                MySqlParameter prmProductID = new MySqlParameter("@ProductID",MySqlDbType.Int64);
                prmProductID.Value = ProductID;
                cmd.Parameters.Add(prmProductID);

                MySqlParameter prmUnitID = new MySqlParameter("@UnitID",MySqlDbType.Int32);
                prmUnitID.Value = UnitID;
                cmd.Parameters.Add(prmUnitID);

                System.Data.DataTable dt = new System.Data.DataTable("tblProductPackage");
                base.MySqlDataAdapterFill(cmd, dt);
                
                ProductPackageDetails clsDetails = setDetails(dt);

                return clsDetails;
            }

            catch (Exception ex)
            {
                throw base.ThrowException(ex);
            }
        }
        public ProductPackageDetails DetailsByProductIDAndBarCode(Int64 ProductID, string BarCode)
        {
            try
            {
                ProductPackageColumns clsProductPackageColumns = getAllPackageColumns();
                clsProductPackageColumns.ProductDesc = false;

                string SQL = SQLSelect(clsProductPackageColumns) + " ";
                SQL += "WHERE tblProductPackage.ProductID = @ProductID ";
                SQL += "    AND (tblProductPackage.Barcode1 LIKE @BarCode OR tblProductPackage.Barcode2 LIKE @BarCode OR tblProductPackage.Barcode3 LIKE @BarCode) ";
                SQL += "LIMIT 1;";

                

                MySqlCommand cmd = new MySqlCommand();
                
                
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandText = SQL;

                MySqlParameter prmProductID = new MySqlParameter("@ProductID",MySqlDbType.Int64);
                prmProductID.Value = ProductID;
                cmd.Parameters.Add(prmProductID);

                MySqlParameter prmBarCode = new MySqlParameter("@BarCode",MySqlDbType.String);
                prmBarCode.Value = BarCode;
                cmd.Parameters.Add(prmBarCode);

                System.Data.DataTable dt = new System.Data.DataTable("tblProductPackage");
                base.MySqlDataAdapterFill(cmd, dt);
                

                ProductPackageDetails clsDetails = setDetails(dt);

                return clsDetails;
            }

            catch (Exception ex)
            {
                throw base.ThrowException(ex);
            }
        }
        public ProductPackageDetails DetailsByBarCode(string BarCode, bool ShowItemMoreThanZeroQty = false)
        {
            try
            {
                ProductPackageColumns clsProductPackageColumns = getAllPackageColumns();
                clsProductPackageColumns.ProductDesc = false;

                string SQL = SQLSelect(clsProductPackageColumns) + " ";
                SQL += "WHERE tblProductPackage.Barcode1 LIKE @BarCode OR tblProductPackage.Barcode2 LIKE @BarCode OR tblProductPackage.Barcode3 LIKE @BarCode OR tblProductPackage.Barcode4 LIKE @BarCode ";
                if (ShowItemMoreThanZeroQty)
                {
                    SQL += "AND ProductID = (SELECT ProductID FROM tblProductInventory inv WHERE inv.ProductID = tblProductPackage.ProductID AND inv.MatrixID = tblProductPackage.MatrixID ORDER BY Quantity DESC LIMIT 1)";
                }
                SQL += "ORDER BY LIMIT 1;";

                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandText = SQL;

                MySqlParameter prmBarCode = new MySqlParameter("@BarCode",MySqlDbType.String);
                prmBarCode.Value = BarCode;
                cmd.Parameters.Add(prmBarCode);

                System.Data.DataTable dt = new System.Data.DataTable("tblProductPackage");
                base.MySqlDataAdapterFill(cmd, dt);

                ProductPackageDetails clsDetails = setDetails(dt);

                return clsDetails;
            }

            catch (Exception ex)
            {
                throw base.ThrowException(ex);
            }
        }
        //private ProductPackageDetails setDetails(MySqlDataReader myReader)
        //{
        //    ProductPackageDetails clsDetails = new ProductPackageDetails(); ;

        //    while (myReader.Read())
        //    {
        //        clsDetails.PackageID = Convert.ToInt64(myReader[ProductPackageColumnNames.PackageID].ToString());
        //        clsDetails.ProductID = Convert.ToInt64(myReader[ProductPackageColumnNames.ProductID].ToString());
        //        clsDetails.UnitID = Convert.ToInt32(myReader[ProductPackageColumnNames.UnitID].ToString());
        //        clsDetails.UnitCode = "" + myReader[ProductPackageColumnNames.UnitCode].ToString();
        //        clsDetails.UnitName = "" + myReader[ProductPackageColumnNames.UnitName].ToString();
        //        clsDetails.Price = Convert.ToDecimal(myReader[ProductPackageColumnNames.Price].ToString());
        //        clsDetails.Price1 = Convert.ToDecimal(myReader[ProductPackageColumnNames.Price1].ToString());
        //        clsDetails.Price2 = Convert.ToDecimal(myReader[ProductPackageColumnNames.Price2].ToString());
        //        clsDetails.Price3 = Convert.ToDecimal(myReader[ProductPackageColumnNames.Price3].ToString());
        //        clsDetails.Price4 = Convert.ToDecimal(myReader[ProductPackageColumnNames.Price4].ToString());
        //        clsDetails.Price5 = Convert.ToDecimal(myReader[ProductPackageColumnNames.Price5].ToString());
        //        clsDetails.Quantity = Convert.ToDecimal(myReader[ProductPackageColumnNames.Quantity].ToString());
        //        clsDetails.VAT = Convert.ToDecimal(myReader[ProductPackageColumnNames.VAT].ToString());
        //        clsDetails.EVAT = Convert.ToDecimal(myReader[ProductPackageColumnNames.EVAT].ToString());
        //        clsDetails.LocalTax = Convert.ToDecimal(myReader[ProductPackageColumnNames.LocalTax].ToString());
        //        clsDetails.BarCode1 = "" + myReader[ProductPackageColumnNames.BarCode1].ToString();
        //        clsDetails.BarCode2 = "" + myReader[ProductPackageColumnNames.BarCode2].ToString();
        //        clsDetails.BarCode3 = "" + myReader[ProductPackageColumnNames.BarCode3].ToString();
        //    }

        //    myReader.Close();

        //    return clsDetails;
        //}

        private ProductPackageDetails setDetails(System.Data.DataTable dt)
        {
            ProductPackageDetails clsDetails = new ProductPackageDetails(); ;

            foreach(System.Data.DataRow dr in dt.Rows)
            {
                clsDetails.PackageID = Convert.ToInt64(dr[ProductPackageColumnNames.PackageID].ToString());
                clsDetails.ProductID = Convert.ToInt64(dr[ProductPackageColumnNames.ProductID].ToString());
                clsDetails.MatrixID = Convert.ToInt64(dr[ProductPackageColumnNames.MatrixID].ToString());
                clsDetails.UnitID = Convert.ToInt32(dr[ProductPackageColumnNames.UnitID].ToString());
                clsDetails.UnitCode = "" + dr[ProductPackageColumnNames.UnitCode].ToString();
                clsDetails.UnitName = "" + dr[ProductPackageColumnNames.UnitName].ToString();
                clsDetails.PurchasePrice = Convert.ToDecimal(dr[ProductPackageColumnNames.PurchasePrice].ToString());
                clsDetails.Price = Convert.ToDecimal(dr[ProductPackageColumnNames.Price].ToString());
                clsDetails.Price1 = Convert.ToDecimal(dr[ProductPackageColumnNames.Price1].ToString());
                clsDetails.Price2 = Convert.ToDecimal(dr[ProductPackageColumnNames.Price2].ToString());
                clsDetails.Price3 = Convert.ToDecimal(dr[ProductPackageColumnNames.Price3].ToString());
                clsDetails.Price4 = Convert.ToDecimal(dr[ProductPackageColumnNames.Price4].ToString());
                clsDetails.Price5 = Convert.ToDecimal(dr[ProductPackageColumnNames.Price5].ToString());
                clsDetails.WSPrice = Convert.ToDecimal(dr[ProductPackageColumnNames.WSPrice].ToString());
                
                clsDetails.Quantity = Convert.ToDecimal(dr[ProductPackageColumnNames.Quantity].ToString());
                clsDetails.VAT = Convert.ToDecimal(dr[ProductPackageColumnNames.VAT].ToString());
                clsDetails.EVAT = Convert.ToDecimal(dr[ProductPackageColumnNames.EVAT].ToString());
                clsDetails.LocalTax = Convert.ToDecimal(dr[ProductPackageColumnNames.LocalTax].ToString());
                clsDetails.BarCode1 = "" + dr[ProductPackageColumnNames.BarCode1].ToString();
                clsDetails.BarCode2 = "" + dr[ProductPackageColumnNames.BarCode2].ToString();
                clsDetails.BarCode3 = "" + dr[ProductPackageColumnNames.BarCode3].ToString();
            }

            return clsDetails;
        }

		#endregion

		#region Streams

        //public MySqlDataReader List(string SortField, SortOption SortOrder, string ProductIDs)
        //{
        //    try
        //    {
        //        string SQL = "SELECT " +
        //                        "a.PackageID, " +
        //                        "a.ProductID, " +
        //                        "ProductDesc, " +
        //                        "a.UnitID, " +
        //                        "c.UnitCode, " +
        //                        "c.UnitName, " +
        //                        "a.Price, " +
        //                        "a.Price1, " +
        //                        "a.Price2, " +
        //                        "a.Price3, " +
        //                        "a.Price4, " +
        //                        "a.Price5, " +
        //                        "a.Quantity, " +
        //                        "a.VAT, " +
        //                        "a.EVAT, " +
        //                        "a.LocalTax " +
        //                    "FROM tblProductPackage a " +
        //                    "INNER JOIN tblProducts b ON a.ProductID = b.ProductID " +
        //                    "INNER JOIN tblUnit c ON a.UnitID = c.UnitID " +
        //                    "INNER JOIN tblProductSubGroup d ON b.ProductSubGroupID = d.ProductSubGroupID " +
        //                    "INNER JOIN tblProductGroup e ON d.ProductGroupID = e.ProductGroupID " +	
        //                    "WHERE 1=1 ";

        //        if (ProductIDs != "" && ProductIDs != null)
        //            SQL += "AND a.ProductID in (" + ProductIDs + ") ";

        //        SQL += "ORDER BY " + SortField; 

        //        if (SortOrder == SortOption.Ascending)
        //            SQL += " ASC";
        //        else
        //            SQL += " DESC";

        //        MySqlCommand cmd = new MySqlCommand();
        //        cmd.CommandType = System.Data.CommandType.Text;
        //        cmd.CommandText = SQL;
				
        //        return base.ExecuteReader(cmd);			
        //    }
        //    catch (Exception ex)
        //    {
        //        throw base.ThrowException(ex);
        //    }	
        //}

        public System.Data.DataTable ListAsDataTable(Int64 ProductID, string BarCode = "", string ProductGroupName = "", string ProductSubGroupName = "", int Limit = 0, string SortField = "", SortOption SortOrder = SortOption.Ascending)
        {
            try
            {
                string SQL = "CALL procProductPackageSelect(@ProductID, @BarCode, @ProductGroupName, @ProductSubGroupName, @lngLimit, @SortField, @SortOrder)";

                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandText = SQL;

                cmd.Parameters.AddWithValue("@ProductID", ProductID);
                cmd.Parameters.AddWithValue("@BarCode", BarCode);
                cmd.Parameters.AddWithValue("@ProductGroupName", ProductGroupName);
                cmd.Parameters.AddWithValue("@ProductSubGroupName", ProductSubGroupName);
                cmd.Parameters.AddWithValue("@lngLimit", Limit);
                cmd.Parameters.AddWithValue("@SortField", SortField);
                switch (SortOrder)
                {
                    case SortOption.Ascending:
                        cmd.Parameters.AddWithValue("@SortOrder", "ASC");
                        break;
                    case SortOption.Desscending:
                        cmd.Parameters.AddWithValue("@SortOrder", "DESC");
                        break;
                } 

                string strDataTableName = "tbl" + this.GetType().FullName.Split(new Char[] { '.' })[this.GetType().FullName.Split(new Char[] { '.' }).Length - 1]; System.Data.DataTable dt = new System.Data.DataTable(strDataTableName);
                base.MySqlDataAdapterFill(cmd, dt);

                return dt;	

            }
            catch (Exception ex)
            {
                throw base.ThrowException(ex);
            }
        }

        public System.Data.DataTable ListAsDataTable(ProductPackageColumns clsProductPackageColumns, ProductPackageColumns SearchColumns, string SearchKey, long SequenceNoStart, System.Data.SqlClient.SortOrder SequenceSortOrder, int Limit, string SortField, System.Data.SqlClient.SortOrder SortOrder)
        {
            try
            {
                string SQL = SQLSelect(clsProductPackageColumns) + "WHERE 1=1 ";

                if (SequenceNoStart != 0)
                {
                    if (SequenceSortOrder == System.Data.SqlClient.SortOrder.Descending)
                        SQL += "AND PackageID < " + SequenceNoStart.ToString() + " ";
                    else
                        SQL += "AND PackageID > " + SequenceNoStart.ToString() + " ";
                }

                if (SearchKey != string.Empty)
                {
                    string SQLSearch = string.Empty;

                    if (SearchColumns.BarCode1)
                    { if (SQLSearch == string.Empty) SQLSearch += "Barcode1 LIKE @SearchKey "; else SQLSearch += "OR Barcode1 LIKE @SearchKey "; }

                    if (SearchColumns.BarCode2)
                    { if (SQLSearch == string.Empty) SQLSearch += "Barcode2 LIKE @SearchKey "; else SQLSearch += "OR Barcode2 LIKE @SearchKey "; }

                    if (SearchColumns.BarCode3)
                    { if (SQLSearch == string.Empty) SQLSearch += "Barcode3 LIKE @SearchKey "; else SQLSearch += "OR Barcode3 LIKE @SearchKey "; }

                    if (SQLSearch != string.Empty) SQL += "AND (" + SQLSearch + ") ";

                    if (SearchColumns.ProductID)
                    { SQL += "AND tblProductPackage.ProductID = " + SearchKey + " "; }
                }

                if (SortField != string.Empty && SortField != null)
                {
                    SQL += "ORDER BY " + SortField + " ";

                    if (SortOrder != System.Data.SqlClient.SortOrder.Descending) SQL += "ASC ";
                    else SQL += "DESC ";
                }

                if (Limit != 0)
                    SQL += "LIMIT " + Limit + " ";

                

                MySqlCommand cmd = new MySqlCommand();
                
                
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandText = SQL;

                MySqlParameter prmSearchKey = new MySqlParameter("@SearchKey",MySqlDbType.String);
                if (SearchKey.StartsWith("%") == true)
                    prmSearchKey.Value = SearchKey + "%";
                else
                    prmSearchKey.Value = "%" + SearchKey + "%";

                cmd.Parameters.Add(prmSearchKey);

                System.Data.DataTable dt = new System.Data.DataTable("tblProductPackage");
                base.MySqlDataAdapterFill(cmd, dt);
                

                return dt;
            }
            catch (Exception ex)
            {
                
                
                {
                    
                    
                    
                    
                }

                throw base.ThrowException(ex);
            }
        }

		#endregion

		#region Public Modifiers

		public int CountPackage(long ProductID)
		{
			try
			{
                string SQL = "SELECT Count(ProductID) FROM tblProductPackage WHERE MatrixID = 0 AND ProductID = @ProductID "; 

				MySqlCommand cmd = new MySqlCommand();
				cmd.CommandType = System.Data.CommandType.Text;
				cmd.CommandText = SQL;

				MySqlParameter prmProductID = new MySqlParameter("@ProductID",MySqlDbType.Int64);			
				prmProductID.Value = ProductID;
				cmd.Parameters.Add(prmProductID);

                System.Data.DataTable dt = new System.Data.DataTable("tblProductPackage");
                base.MySqlDataAdapterFill(cmd, dt);
                

                int iRetValue = 0;
                foreach (System.Data.DataRow dr in dt.Rows)
                {
                    iRetValue = int.Parse(dr[0].ToString());
                }

				return iRetValue;
			}
			catch (Exception ex)
			{
				throw base.ThrowException(ex);
			}
		}

		#endregion
	}
}


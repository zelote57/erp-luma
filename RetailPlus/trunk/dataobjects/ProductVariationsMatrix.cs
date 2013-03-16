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
	public struct ProductVariationsMatrixDetails
	{
		public long MatrixID;
		public long ProductID;
		public long VariationID;
		public string VariationType;
		public string Description;
	}

	[StrongNameIdentityPermissionAttribute(SecurityAction.LinkDemand,
		 PublicKey = "002400000480000094000000060200000024000" +
		 "052534131000400000100010053D785642F9F960B43157E0380" +
		 "F393BEE53E8DFAFBF441366C1B6F8B48D9DDF0D527B1F3B21EA" +
		 "E85D2FDB664CE81EB8A87DBE4C589D6F4202FE2B7D4B978BB69" +
		 "684874612CB9B8DB7A0339400A9C4E68277884B07817363D242" +
		 "E3696F9FACDBEA831810AE6DC9EDCA91A7B5DA12FE7BF65D113" +
		 "FF52834EAFB5A7A1FDFD5851A3")]
	public struct ProductBaseMatrixDetails
	{
		public long MatrixID;
		public long ProductID;
		public string Description;
		public Int32 UnitID;
		public string UnitCode;
		public string UnitName;
		public decimal Price;
        public decimal WSPrice;
		public decimal PurchasePrice;
		public bool IncludeInSubtotalDiscount;
		public decimal VAT;
		public decimal EVAT;
		public decimal LocalTax;
		public decimal Quantity;
        public decimal ActualQuantity;
		public decimal MinThreshold;
		public decimal MaxThreshold;
        public decimal RIDMinThreshold;
        public decimal RIDMaxThreshold;
        public long UpdatedBy;
        public DateTime UpdatedOn;
        public decimal QuantityIN;
        public decimal QuantityOUT;
        public string CreatedBy;
	}

	[StrongNameIdentityPermissionAttribute(SecurityAction.LinkDemand,
		 PublicKey = "002400000480000094000000060200000024000" +
		 "052534131000400000100010053D785642F9F960B43157E0380" +
		 "F393BEE53E8DFAFBF441366C1B6F8B48D9DDF0D527B1F3B21EA" +
		 "E85D2FDB664CE81EB8A87DBE4C589D6F4202FE2B7D4B978BB69" +
		 "684874612CB9B8DB7A0339400A9C4E68277884B07817363D242" +
		 "E3696F9FACDBEA831810AE6DC9EDCA91A7B5DA12FE7BF65D113" +
		 "FF52834EAFB5A7A1FDFD5851A3")]
	public class ProductVariationsMatrix : POSConnection
	{
		#region Constructors and Destructors

		public ProductVariationsMatrix()
            : base(null, null)
        {
        }

        public ProductVariationsMatrix(MySqlConnection Connection, MySqlTransaction Transaction) 
            : base(Connection, Transaction)
		{

		}

		#endregion
		
		#region tblProductBaseVariationsMatrix Insert and Update

        /// <summary>
        /// Aug 1, 2011 : Lemu
        /// Include clsProduct.AddQuantity
        /// </summary>
        /// <param name="Details"></param>
        /// <returns></returns>
		public Int64 InsertBaseVariation(ProductBaseMatrixDetails Details)
		{
			try 
			{
				string SQL = "INSERT INTO tblProductBaseVariationsMatrix (" +
								"ProductID, " +
								"Description, " +
								"UnitID, " +
								"Price, " +
                                "WSPrice, " +
								"PurchasePrice, " +
								"IncludeInSubtotalDiscount, " +
								"VAT, " +
								"EVAT, " +
								"LocalTax, " +
								"Quantity, " +
								"MinThreshold, " +
								"MaxThreshold " + 
							")VALUES(" +
								"@ProductID, " +
								"@Description, " +
								"@UnitID, " +
								"@Price, " +
                                "@WSPrice, " +
								"@PurchasePrice, " +
								"@IncludeInSubtotalDiscount, " +
								"@VAT, " +
								"@EVAT, " +
								"@LocalTax, " +
								"@Quantity, " +
								"@MinThreshold, " +
								"@MaxThreshold " +
							");";
				  
				MySqlCommand cmd = new MySqlCommand();
				cmd.CommandType = System.Data.CommandType.Text;
				cmd.CommandText = SQL;

                cmd.Parameters.AddWithValue("@ProductID", Details.ProductID);
                cmd.Parameters.AddWithValue("@Description", Details.Description);
                cmd.Parameters.AddWithValue("@UnitID", Details.UnitID);
                cmd.Parameters.AddWithValue("@Price", Details.Price);
                cmd.Parameters.AddWithValue("@WSPrice", Details.WSPrice);
                cmd.Parameters.AddWithValue("@PurchasePrice", Details.PurchasePrice);
                cmd.Parameters.AddWithValue("@IncludeInSubtotalDiscount", Details.IncludeInSubtotalDiscount);
                cmd.Parameters.AddWithValue("@VAT", Details.VAT);
                cmd.Parameters.AddWithValue("@EVAT", Details.EVAT);
                cmd.Parameters.AddWithValue("@LocalTax", Details.LocalTax);
                cmd.Parameters.AddWithValue("@Quantity", 0); // Jul 26, 2011 : Lemu - change the Details.Quantity to zero, transfer adding of quantity for this variation in clsProduct.AddQuantity
                cmd.Parameters.AddWithValue("@MinThreshold", Details.MinThreshold);
                cmd.Parameters.AddWithValue("@MaxThreshold", Details.MaxThreshold);

				base.ExecuteNonQuery(cmd);

				SQL = "SELECT LAST_INSERT_ID();";
				
				cmd.Parameters.Clear(); 
				cmd.CommandText = SQL;

                System.Data.DataTable dt = new System.Data.DataTable("ProdVarMatrix");
                base.MySqlDataAdapterFill(cmd, dt);

                Int64 iID = 0;
                foreach (System.Data.DataRow dr in dt.Rows)
                {
                    iID = Int64.Parse(dr[0].ToString());
                }

				MatrixPackageDetails clsMatrixPackageDetails = new MatrixPackageDetails();
				clsMatrixPackageDetails.MatrixID = iID;
				clsMatrixPackageDetails.UnitID = Details.UnitID;
				clsMatrixPackageDetails.Price = Details.Price;
                clsMatrixPackageDetails.WSPrice = Details.WSPrice;
				clsMatrixPackageDetails.PurchasePrice = Details.PurchasePrice;
				clsMatrixPackageDetails.Quantity = 1;
				clsMatrixPackageDetails.VAT = Details.VAT;
				clsMatrixPackageDetails.EVAT = Details.EVAT;
				clsMatrixPackageDetails.LocalTax = Details.LocalTax;

				MatrixPackage clsMatrixPackage = new MatrixPackage(base.Connection, base.Transaction);
				clsMatrixPackage.Insert(clsMatrixPackageDetails);

                // Oct 28, 2011 : Lemu
                // Added to cater branch inventory
                SQL = "CALL procProductBranchInventoryInsert(@lngProductID);";

                cmd.Parameters.Clear();
                cmd.CommandText = SQL;
                cmd.Parameters.AddWithValue("@lngProductID", Details.ProductID);
                base.ExecuteNonQuery(cmd);

                // Oct 28, 2011 : change to procSyncProductVariationFromQuantityPerItem(lngProductID, intBranchID);
                // ProductUnit clsProductUnit = new ProductUnit(base.Connection, base.Transaction);
                // decimal Quantity = clsProductUnit.GetBaseUnitValue(Details.ProductID, Details.UnitID, Details.Quantity);
				// Product clsProduct = new Product(base.Connection, base.Transaction);
                // clsProduct.AddQuantity(Constants.BRANCH_ID_MAIN, Details.ProductID, iID, Details.Quantity, Product.getPRODUCT_INVENTORY_MOVEMENT_VALUE(PRODUCT_INVENTORY_MOVEMENT.ADD_PRODUCT_VARIATION_CREATION), DateTime.Now, "SYS-ADDVAR" + DateTime.Now.ToString("yyyyMMddHHmmss"), Details.CreatedBy);
                SQL = "CALL procSyncProductVariationFromQuantityPerItemAllBranch(@lngProductID);";

                cmd.Parameters.Clear();
                cmd.CommandText = SQL;
                cmd.Parameters.AddWithValue("@lngProductID", Details.ProductID);
                base.ExecuteNonQuery(cmd);          

                // Added August 2, 2009 to monitor if product still has/have variations
                Products clsProduct = new Products(base.Connection, base.Transaction);
                clsProduct.UpdateVariationCount(Details.ProductID);

				return iID;
			}

			catch (Exception ex)
			{
				throw base.ThrowException(ex);
			}	
		}
        public void InsertBaseVariationEasy(long pvtProductID, string pvtDescription)
        {
            try
            {
                

                string SQL = "CALL procBaseVariationEasyInsert(@ProductID, @Description);";

                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandText = SQL;

                cmd.Parameters.AddWithValue("@ProductID", pvtProductID);
                cmd.Parameters.AddWithValue("@Description", pvtDescription);

                base.ExecuteNonQuery(cmd);
            }

            catch (Exception ex)
            {
                throw base.ThrowException(ex);
            }
        }

		public bool UpdateVariationDesc(ProductBaseMatrixDetails Details)
		{
			try 
			{
				string SQL = "UPDATE tblProductBaseVariationsMatrix SET " +
								"Description = @Description " +
							"WHERE ProductID = @ProductID AND MatrixID = @MatrixID;";
				  
				MySqlCommand cmd = new MySqlCommand();
				cmd.CommandType = System.Data.CommandType.Text;
				cmd.CommandText = SQL;
				
				MySqlParameter prmProductID = new MySqlParameter("@ProductID",MySqlDbType.Int32);			
				prmProductID.Value = Details.ProductID;
				cmd.Parameters.Add(prmProductID);

				MySqlParameter prmMatrixID = new MySqlParameter("@MatrixID",MySqlDbType.Int32);			
				prmMatrixID.Value = Details.MatrixID;
				cmd.Parameters.Add(prmMatrixID);

				MySqlParameter prmDescription = new MySqlParameter("@Description",MySqlDbType.String);			
				prmDescription.Value = Details.Description;
				cmd.Parameters.Add(prmDescription);

				base.ExecuteNonQuery(cmd);
				
				return true;
			}

			catch (Exception ex)
			{
				throw base.ThrowException(ex);
			}	
		}

		public bool UpdateBaseVariation(ProductBaseMatrixDetails Details)
		{
			try 
			{
				string SQL = "UPDATE tblProductBaseVariationsMatrix SET " +
								"Description		= @Description, " +
								"UnitID				= @UnitID, " +
								"Price				= @Price, " +
                                "WSPrice			= @WSPrice, " +
								"PurchasePrice		= @PurchasePrice, " +
								"IncludeInSubtotalDiscount	=	@IncludeInSubtotalDiscount, " +
								"VAT				= @VAT, " +
								"EVAT				= @EVAT, " +
								"LocalTax			= @LocalTax, " +
								"Quantity			= @Quantity, " +
								"MinThreshold		= @MinThreshold, " +
								"MaxThreshold		= @MaxThreshold " +
							"WHERE ProductID = @ProductID AND MatrixID = @MatrixID;";
				  
				MySqlCommand cmd = new MySqlCommand();
				cmd.CommandType = System.Data.CommandType.Text;
				cmd.CommandText = SQL;
				
				cmd.Parameters.AddWithValue("@ProductID", Details.ProductID);
				cmd.Parameters.AddWithValue("@MatrixID", Details.MatrixID);
				cmd.Parameters.AddWithValue("@Description", Details.Description);
				cmd.Parameters.AddWithValue("@UnitID", Details.UnitID);
				cmd.Parameters.AddWithValue("@Price", Details.Price);
                cmd.Parameters.AddWithValue("@WSPrice", Details.WSPrice);
				cmd.Parameters.AddWithValue("@PurchasePrice", Details.PurchasePrice);
				cmd.Parameters.AddWithValue("@IncludeInSubtotalDiscount", Details.IncludeInSubtotalDiscount);
				cmd.Parameters.AddWithValue("@VAT", Details.VAT);
				cmd.Parameters.AddWithValue("@EVAT", Details.EVAT);
				cmd.Parameters.AddWithValue("@LocalTax", Details.LocalTax);
				cmd.Parameters.AddWithValue("@Quantity", Details.Quantity);
				cmd.Parameters.AddWithValue("@MinThreshold", Details.MinThreshold);
				cmd.Parameters.AddWithValue("@MaxThreshold", Details.MaxThreshold);

				base.ExecuteNonQuery(cmd);
				
				MatrixPackageDetails clsMatrixPackageDetails = new MatrixPackageDetails();
				clsMatrixPackageDetails.MatrixID = Details.MatrixID;
				clsMatrixPackageDetails.UnitID = Details.UnitID;
				clsMatrixPackageDetails.Price = Details.Price;
                clsMatrixPackageDetails.WSPrice = Details.WSPrice;
				clsMatrixPackageDetails.PurchasePrice = Details.PurchasePrice;
				clsMatrixPackageDetails.Quantity = 1;
				clsMatrixPackageDetails.VAT = Details.VAT;
				clsMatrixPackageDetails.EVAT = Details.EVAT;
				clsMatrixPackageDetails.LocalTax = Details.LocalTax;

				MatrixPackage clsMatrixPackage = new MatrixPackage(base.Connection, base.Transaction);
				clsMatrixPackage.UpdateByMatrixIDUnitIDAndQuantity(clsMatrixPackageDetails, Details.UpdatedBy, Details.UpdatedOn, "Edit matrix module."); 

				return true;
			}

			catch (Exception ex)
			{
				throw base.ThrowException(ex);
			}	
		}
        public void UpdatePurchasing(long MatrixID, long SupplierID, int UnitID, decimal PurchasePrice)
        {
            try
            {
                string SQL = "UPDATE tblProductBaseVariationsMatrix SET " +
                                    "PurchasePrice	= @PurchasePrice, " +
                                    "SupplierID		= @SupplierID " +
                            "WHERE MatrixID = @MatrixID;";

                

                MySqlCommand cmd = new MySqlCommand();
                
                
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandText = SQL;

                cmd.Parameters.AddWithValue("@PurchasePrice", PurchasePrice);
                cmd.Parameters.AddWithValue("@SupplierID", SupplierID);
                cmd.Parameters.AddWithValue("@MatrixID", MatrixID); 
                //cmd.Parameters.AddWithValue("@ProductID", ProductID);

                base.ExecuteNonQuery(cmd);

                MatrixPackage clsMatrixPackage = new MatrixPackage(base.Connection, base.Transaction);
                clsMatrixPackage.UpdatePurchasing(MatrixID, UnitID, 1, PurchasePrice);

            }

            catch (Exception ex)
            {
                
                
                {
                    
                    
                    
                    
                }

                throw base.ThrowException(ex);
            }
        }
        public void UpdateSelling(long MatrixID, long SupplierID, int UnitID, decimal Price)
        {
            try
            {
                string SQL = "UPDATE tblProductBaseVariationsMatrix SET " +
                                    "Price	= @Price " +
                            "WHERE MatrixID = @MatrixID;";

                

                MySqlCommand cmd = new MySqlCommand();
                
                
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandText = SQL;

                cmd.Parameters.AddWithValue("@Price", Price);
                cmd.Parameters.AddWithValue("@MatrixID", MatrixID);

                base.ExecuteNonQuery(cmd);

                MatrixPackage clsMatrixPackage = new MatrixPackage(base.Connection, base.Transaction);
                clsMatrixPackage.UpdateSelling(MatrixID, UnitID, 1, Price);

            }

            catch (Exception ex)
            {
                
                
                {
                    
                    
                    
                    
                }

                throw base.ThrowException(ex);
            }
        }
        public void UpdateSellingWSPrice(long MatrixID, long SupplierID, int UnitID, decimal WholesalePrice)
        {
            try
            {
                string SQL = "UPDATE tblProductBaseVariationsMatrix SET " +
                                    "WSPrice	= @WSPrice " +
                            "WHERE MatrixID = @MatrixID;";

                

                MySqlCommand cmd = new MySqlCommand();
                
                
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandText = SQL;

                cmd.Parameters.AddWithValue("@WSPrice", WholesalePrice);
                cmd.Parameters.AddWithValue("@MatrixID", MatrixID);

                base.ExecuteNonQuery(cmd);

                MatrixPackage clsMatrixPackage = new MatrixPackage(base.Connection, base.Transaction);
                clsMatrixPackage.UpdateSellingWSPrice(MatrixID, UnitID, 1, WholesalePrice);

            }

            catch (Exception ex)
            {
                
                
                {
                    
                    
                    
                    
                }

                throw base.ThrowException(ex);
            }
        }
        public void UpdateSellingWithSameQuantityAndUnit(long ProductID, long SupplierID, int UnitID, decimal Price)
        {
            try
            {
                string SQL = "UPDATE tblProductBaseVariationsMatrix SET " +
                                    "Price	= @Price " +
                            "WHERE ProductID = @ProductID " +
                                "AND UnitID = @UnitID ";

                

                MySqlCommand cmd = new MySqlCommand();
                
                
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandText = SQL;

                cmd.Parameters.AddWithValue("@Price", Price);
                cmd.Parameters.AddWithValue("@ProductID", ProductID);
                cmd.Parameters.AddWithValue("@UnitID", UnitID);

                base.ExecuteNonQuery(cmd);

                MatrixPackage clsMatrixPackage = new MatrixPackage(base.Connection, base.Transaction);
                clsMatrixPackage.UpdateSellingWithSameQuantityAndUnit(ProductID, UnitID, 1, Price);

            }

            catch (Exception ex)
            {
                
                
                {
                    
                    
                    
                    
                }

                throw base.ThrowException(ex);
            }
        }
        public void UpdateSellingWithSameQuantityAndUnitWSPrice(long ProductID, long SupplierID, int UnitID, decimal WholePrice)
        {
            try
            {
                string SQL = "UPDATE tblProductBaseVariationsMatrix SET " +
                                    "WSPrice	= @WSPrice " +
                            "WHERE ProductID = @ProductID " +
                                "AND UnitID = @UnitID ";

                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandText = SQL;

                cmd.Parameters.AddWithValue("@WSPrice", WholePrice);
                cmd.Parameters.AddWithValue("@ProductID", ProductID);
                cmd.Parameters.AddWithValue("@UnitID", UnitID);

                base.ExecuteNonQuery(cmd);

                MatrixPackage clsMatrixPackage = new MatrixPackage(base.Connection, base.Transaction);
                clsMatrixPackage.UpdateSellingWithSameQuantityAndUnitWSPrice(ProductID, UnitID, 1, WholePrice);
            }

            catch (Exception ex)
            {
                throw base.ThrowException(ex);
            }
        }
        public void UpdateInvDetails(int BranchID, long ProductID, long MatrixID, decimal QuantityNow, decimal MinThresholdNow, decimal MaxThresholdNow, string Remarks, DateTime TransactionDate, string TransactionNo, string AdjustedBy)
        {
            try
            {
                string SQL = "CALL procProductBaseVariationUpdateInvDetails(@BranchID, @ProductID, @MatrixID, @QuantityNow, @MinThresholdNow, @MaxThresholdNow, @strRemarks, @dteTransactionDate, @strTransactionNo, @AdjustedBy);";

                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandText = SQL;

                cmd.Parameters.AddWithValue("@BranchID", BranchID);
                cmd.Parameters.AddWithValue("@ProductID", ProductID);
                cmd.Parameters.AddWithValue("@MatrixID", MatrixID);
                cmd.Parameters.AddWithValue("@QuantityNow", QuantityNow);
                cmd.Parameters.AddWithValue("@MinThresholdNow", MinThresholdNow);
                cmd.Parameters.AddWithValue("@MaxThresholdNow", MaxThresholdNow);
                cmd.Parameters.AddWithValue("@strRemarks", Remarks);
                cmd.Parameters.AddWithValue("@dteTransactionDate", TransactionDate);
                cmd.Parameters.AddWithValue("@strTransactionNo", TransactionNo);
                cmd.Parameters.AddWithValue("@AdjustedBy", AdjustedBy);

                base.ExecuteNonQuery(cmd);
            }

            catch (Exception ex)
            {
                throw base.ThrowException(ex);
            }
        }

        public void UpdateByPackage(Int64 MatrixID, Int32 UnitID, decimal Price, decimal WSPrice, decimal PurchasePrice, decimal VAT, decimal EVAT, decimal LocalTax)
		{
			try 
			{
				string SQL =	"UPDATE tblProductBaseVariationsMatrix SET " +
					                "Price				= @Price, " +
                                    "WSPrice			= @WSPrice, " +
					                "PurchasePrice		= @PurchasePrice, " +
					                "VAT				= @VAT, " +
					                "EVAT				= @EVAT, " +
					                "LocalTax			= @LocalTax " +
					            "WHERE MatrixID			= @MatrixID " +
					                "AND UnitID				= @UnitID ";
				  
				
	 			
				MySqlCommand cmd = new MySqlCommand();
				
				
				cmd.CommandType = System.Data.CommandType.Text;
				cmd.CommandText = SQL;
				
				MySqlParameter prmPrice = new MySqlParameter("@Price",MySqlDbType.Decimal);			
				prmPrice.Value = Price;
				cmd.Parameters.Add(prmPrice);

                cmd.Parameters.AddWithValue("@WSPrice", WSPrice);

				MySqlParameter prmPurchasePrice = new MySqlParameter("@PurchasePrice",MySqlDbType.Decimal);			
				prmPurchasePrice.Value = PurchasePrice;
				cmd.Parameters.Add(prmPurchasePrice);

				MySqlParameter prmVAT = new MySqlParameter("@VAT",MySqlDbType.Decimal);			
				prmVAT.Value = VAT;
				cmd.Parameters.Add(prmVAT);

				MySqlParameter prmEVAT = new MySqlParameter("@EVAT",MySqlDbType.Decimal);			
				prmEVAT.Value = EVAT;
				cmd.Parameters.Add(prmEVAT);

				MySqlParameter prmLocalTax = new MySqlParameter("@LocalTax",MySqlDbType.Decimal);			
				prmLocalTax.Value = LocalTax;
				cmd.Parameters.Add(prmLocalTax);

				MySqlParameter prmMatrixID = new MySqlParameter("@MatrixID",MySqlDbType.Int64);			
				prmMatrixID.Value = MatrixID;
				cmd.Parameters.Add(prmMatrixID);

				MySqlParameter prmUnitID = new MySqlParameter("@UnitID",MySqlDbType.Int32);			
				prmUnitID.Value = UnitID;
				cmd.Parameters.Add(prmUnitID);

				base.ExecuteNonQuery(cmd);
			}

			catch (Exception ex)
			{
				throw base.ThrowException(ex);
			}	
		}

		public bool IsFirstBaseVariation(long ProductID)
		{
			try
			{
				string SQL =	"SELECT count(ProductID) 'basecount' FROM tblProductBaseVariationsMatrix " +
																	   "WHERE ProductID = @ProductID;"; 

				MySqlCommand cmd = new MySqlCommand();
				cmd.CommandType = System.Data.CommandType.Text;
				cmd.CommandText = SQL;
				
				MySqlParameter prmProductID = new MySqlParameter("@ProductID",MySqlDbType.Int64);	
				prmProductID.Value = ProductID;
				cmd.Parameters.Add(prmProductID);

				bool boRetValue = true;

                string strDataTableName = "tbl" + this.GetType().FullName.Split(new Char[] { '.' })[this.GetType().FullName.Split(new Char[] { '.' }).Length - 1]; System.Data.DataTable dt = new System.Data.DataTable(strDataTableName);
                base.MySqlDataAdapterFill(cmd, dt);

                foreach (System.Data.DataRow dr in dt.Rows)
                {
                    if (Int32.Parse(dr[0].ToString()) > 0)
                        boRetValue = false;
                }

				return boRetValue;
			}
										
			catch (Exception ex)
			{
				throw base.ThrowException(ex);
			}	
		}

		public void SynchronizeQuantity(long pvtProductID)
		{
			try 
			{
                string SQL = "CALL procProductSynchronizeQuantity(@ProductID);";

                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandText = SQL;

                cmd.Parameters.AddWithValue("@ProductID", pvtProductID);

                base.ExecuteNonQuery(cmd);
			}

			catch (Exception ex)
			{
				throw base.ThrowException(ex);
			}	
		}

        /// <summary>
        /// Lemu - 06-20-2011
        /// </summary>
        /// <param name="ProductID">Put zero(0) if you want to update all products</param>
        /// <param name="Quantity"></param>
        /// <returns></returns>
        public bool UpdateActualQuantity(long lngProductID, long lngMatrixID, decimal decQuantity)
        {
            bool boRetValue = false;
            try
            {
                string SQL = "CALL procProductBaseVariationUpdateActualQuantity(@lngProductID, @lngMatrixID, @decQuantity);";

                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandText = SQL;

                cmd.Parameters.AddWithValue("@lngProductID", lngProductID);
                cmd.Parameters.AddWithValue("@lngMatrixID", lngMatrixID);
                cmd.Parameters.AddWithValue("@decQuantity", decQuantity);

                if (base.ExecuteNonQuery(cmd) > 0) boRetValue = true;
            }

            catch (Exception ex)
            {
                throw base.ThrowException(ex);
            }

            return boRetValue;
        }

		#endregion

		#region tblProductBaseVariationsMatrix Details

		public ProductBaseMatrixDetails BaseDetails(long MatrixID, long ProductID)
		{
			try
			{
				string SQL=	SQLSelect() + "AND a.MatrixID = @MatrixID AND a.ProductID = @ProductID;"; 
	 			
				MySqlCommand cmd = new MySqlCommand();
				cmd.CommandType = System.Data.CommandType.Text;
				cmd.CommandText = SQL;

				MySqlParameter prmMatrixID = new MySqlParameter("@MatrixID",MySqlDbType.Int64);			
				prmMatrixID.Value = MatrixID;
				cmd.Parameters.Add(prmMatrixID);

				MySqlParameter prmProductID = new MySqlParameter("@ProductID",MySqlDbType.Int64);			
				prmProductID.Value = ProductID;
				cmd.Parameters.Add(prmProductID);

                string strDataTableName = "tbl" + this.GetType().FullName.Split(new Char[] { '.' })[this.GetType().FullName.Split(new Char[] { '.' }).Length - 1]; System.Data.DataTable dt = new System.Data.DataTable(strDataTableName);
                base.MySqlDataAdapterFill(cmd, dt);

				ProductBaseMatrixDetails Details = getBaseDetails(dt);

				return Details;
			}

			catch (Exception ex)
			{
				throw base.ThrowException(ex);
			}	
		}
		public ProductBaseMatrixDetails BaseDetails(long ProductID)
		{
			try
			{
				string SQL=		SQLSelect() + "AND a.ProductID = @ProductID;"; 

				
	 			
				MySqlCommand cmd = new MySqlCommand();
				
				
				cmd.CommandType = System.Data.CommandType.Text;
				cmd.CommandText = SQL;

				MySqlParameter prmProductID = new MySqlParameter("@ProductID",MySqlDbType.Int64);			
				prmProductID.Value = ProductID;
				cmd.Parameters.Add(prmProductID);

                string strDataTableName = "tbl" + this.GetType().FullName.Split(new Char[] { '.' })[this.GetType().FullName.Split(new Char[] { '.' }).Length - 1]; System.Data.DataTable dt = new System.Data.DataTable(strDataTableName);
                base.MySqlDataAdapterFill(cmd, dt);

				ProductBaseMatrixDetails Details = getBaseDetails(dt);

				return Details;
			}

			catch (Exception ex)
			{
				throw base.ThrowException(ex);
			}	
		}
		public ProductBaseMatrixDetails BaseDetails(string Description, long ProductID)
		{
			try
			{
				string SQL=	SQLSelect() + "AND a.Description = @Description AND a.ProductID = @ProductID;"; 

				
	 			
				MySqlCommand cmd = new MySqlCommand();
				
				
				cmd.CommandType = System.Data.CommandType.Text;
				cmd.CommandText = SQL;

				MySqlParameter prmDescription = new MySqlParameter("@Description",MySqlDbType.String);
				prmDescription.Value = Description;
				cmd.Parameters.Add(prmDescription);

				MySqlParameter prmProductID = new MySqlParameter("@ProductID",MySqlDbType.Int64);			
				prmProductID.Value = ProductID;
				cmd.Parameters.Add(prmProductID);

                string strDataTableName = "tbl" + this.GetType().FullName.Split(new Char[] { '.' })[this.GetType().FullName.Split(new Char[] { '.' }).Length - 1]; System.Data.DataTable dt = new System.Data.DataTable(strDataTableName);
                base.MySqlDataAdapterFill(cmd, dt);

                ProductBaseMatrixDetails Details = getBaseDetails(dt);

				return Details;
			}

			catch (Exception ex)
			{
				throw base.ThrowException(ex);
			}	
		}
		public ProductBaseMatrixDetails BaseDetailsByMatrixID(long MatrixID)
		{
			try
			{
				string SQL=		SQLSelect() + "AND a.MatrixID = @MatrixID;"; 

				
	 			
				MySqlCommand cmd = new MySqlCommand();
				
				
				cmd.CommandType = System.Data.CommandType.Text;
				cmd.CommandText = SQL;

				MySqlParameter prmMatrixID = new MySqlParameter("@MatrixID",MySqlDbType.Int64);			
				prmMatrixID.Value = MatrixID;
				cmd.Parameters.Add(prmMatrixID);

                string strDataTableName = "tbl" + this.GetType().FullName.Split(new Char[] { '.' })[this.GetType().FullName.Split(new Char[] { '.' }).Length - 1]; System.Data.DataTable dt = new System.Data.DataTable(strDataTableName);
                base.MySqlDataAdapterFill(cmd, dt);

                ProductBaseMatrixDetails Details = getBaseDetails(dt);

				return Details;
			}

			catch (Exception ex)
			{
				throw base.ThrowException(ex);
			}	
		}
        //private ProductBaseMatrixDetails getBaseDetails(MySqlDataReader myReader)
        //{
        //    ProductBaseMatrixDetails Details = new ProductBaseMatrixDetails();
        //    while (myReader.Read())
        //    {
        //        Details.MatrixID = myReader.GetInt64("MatrixID");
        //        Details.ProductID = myReader.GetInt64("ProductID");
        //        Details.Description = "" + myReader["Description"].ToString();
        //        Details.UnitID = myReader.GetInt32("UnitID");
        //        Details.UnitCode = "" + myReader["UnitCode"].ToString();
        //        Details.UnitName = "" + myReader["UnitName"].ToString();
        //        Details.Price = myReader.GetDecimal("Price");
        //        Details.WSPrice = myReader.GetDecimal("WSPrice");
        //        Details.PurchasePrice = myReader.GetDecimal("PurchasePrice");
        //        Details.IncludeInSubtotalDiscount = myReader.GetBoolean("IncludeInSubtotalDiscount");
        //        Details.VAT = myReader.GetDecimal("VAT");
        //        Details.EVAT = myReader.GetDecimal("EVAT");
        //        Details.LocalTax = myReader.GetDecimal("LocalTax");
        //        Details.Quantity = myReader.GetDecimal("Quantity");
        //        Details.ActualQuantity = myReader.GetDecimal("ActualQuantity");
        //        Details.MinThreshold = myReader.GetDecimal("MinThreshold");
        //        Details.MaxThreshold = myReader.GetDecimal("MaxThreshold");
        //        Details.RIDMinThreshold = myReader.GetDecimal("RIDMinThreshold");
        //        Details.RIDMaxThreshold = myReader.GetDecimal("RIDMaxThreshold");
        //    }

        //    myReader.Close();
        //    return Details;
        //}

        private ProductBaseMatrixDetails getBaseDetails(System.Data.DataTable dt)
        {
            ProductBaseMatrixDetails Details = new ProductBaseMatrixDetails();
            foreach (System.Data.DataRow dr in dt.Rows)
            {
                Details.MatrixID = Int64.Parse(dr["MatrixID"].ToString());
                Details.ProductID = Int64.Parse(dr["ProductID"].ToString());
                Details.Description = "" + dr["Description"].ToString();
                Details.UnitID = Int32.Parse(dr["UnitID"].ToString());
                Details.UnitCode = "" + dr["UnitCode"].ToString();
                Details.UnitName = "" + dr["UnitName"].ToString();
                Details.Price = decimal.Parse(dr["Price"].ToString());
                Details.WSPrice = decimal.Parse(dr["WSPrice"].ToString());
                Details.PurchasePrice = decimal.Parse(dr["PurchasePrice"].ToString());
                Details.IncludeInSubtotalDiscount = Boolean.Parse(dr["IncludeInSubtotalDiscount"].ToString());
                Details.VAT = decimal.Parse(dr["VAT"].ToString());
                Details.EVAT = decimal.Parse(dr["EVAT"].ToString());
                Details.LocalTax = decimal.Parse(dr["LocalTax"].ToString());
                Details.Quantity = decimal.Parse(dr["Quantity"].ToString());
                Details.ActualQuantity = decimal.Parse(dr["ActualQuantity"].ToString());
                Details.MinThreshold = decimal.Parse(dr["MinThreshold"].ToString());
                Details.MaxThreshold = decimal.Parse(dr["MaxThreshold"].ToString());
                Details.RIDMinThreshold = decimal.Parse(dr["RIDMinThreshold"].ToString());
                Details.RIDMaxThreshold = decimal.Parse(dr["RIDMaxThreshold"].ToString());
            }

            return Details;
        }

		#endregion

		#region tblProductBaseVariationsMatrix Streams

        private string SQLSelect()
        {
            string stSQL = "SELECT a.MatriXID, " +
                                    "a.ProductID, " +
                                    "a.Description, " +
                                    "CONCAT(ProductDesc, ':' , a.Description) AS VariationDesc, " +
                                    "a.Description AS VariationDescOnly, " +
                                    "b.ProductDesc, " +
                                    "a.UnitID, " +
                                    "c.UnitCode, " +
                                    "c.UnitName, " +
                                    "a.Price, " +
                                    "a.WSPrice, " +
                                    "a.PurchasePrice, " +
                                    "a.IncludeInSubtotalDiscount, " +
                                    "a.VAT, " +
                                    "a.EVAT, " +
                                    "a.LocalTax, " +
                                    "a.Quantity, " +
                                    "a.ActualQuantity, " +
                                    "a.MinThreshold, " +
                                    "a.MaxThreshold, " +
                                    "a.QuantityIN, " +
                                    "a.QuantityOUT, " +
                                    "a.RIDMinThreshold, " +
                                    "a.RIDMaxThreshold, " +
                                    "a.SupplierID, " +
                                    "f.ContactName AS SupplierName " +
                                "FROM tblProductBaseVariationsMatrix a " +
                                    "INNER JOIN tblProducts b ON a.ProductID = b.ProductID " +
                                    "INNER JOIN tblUnit c ON a.UnitID = c.UnitID " +
                                    "INNER JOIN tblProductSubGroup d ON b.ProductSubGroupID = d.ProductSubGroupID " +
                                    "INNER JOIN tblProductGroup e ON d.ProductGroupID = e.ProductGroupID " +
                                    "INNER JOIN tblContacts f ON a.SupplierID = f.ContactID " +
                                    "LEFT OUTER JOIN tblproductvariationsmatrix g on a.MatrixID = g.MatrixID " +
                                "WHERE a.Deleted = 0 ";

            return stSQL;
        }

        private string SQLSelect(int BranchID)
        {
            string stSQL = "SELECT a.MatriXID, " +
                                    "a.ProductID, " +
                                    "a.Description, " +
                                    "CONCAT(ProductDesc, ':' , a.Description) AS VariationDesc, " +
                                    "a.Description AS VariationDescOnly, " +
                                    "b.ProductDesc, " +
                                    "a.UnitID, " +
                                    "c.UnitCode, " +
                                    "c.UnitName, " +
                                    "a.Price, " +
                                    "a.WSPrice, " +
                                    "a.PurchasePrice, " +
                                    "a.IncludeInSubtotalDiscount, " +
                                    "a.VAT, " +
                                    "a.EVAT, " +
                                    "a.LocalTax, " +
                                    "z.Quantity, " +
                                    "z.ActualQuantity, " +
                                    "a.MinThreshold, " +
                                    "a.MaxThreshold, " +
                                    "z.QuantityIN, " +
                                    "z.QuantityOUT, " +
                                    "a.RIDMinThreshold, " +
                                    "a.RIDMaxThreshold, " +
                                    "a.SupplierID, " +
                                    "f.ContactName AS SupplierName " +
                                "FROM tblBranchInventoryMatrix z " +
                                    "INNER JOIN tblProductBaseVariationsMatrix a ON a.MatrixID = z.MatrixID " +
                                    "INNER JOIN tblProducts b ON a.ProductID = b.ProductID " +
                                    "INNER JOIN tblUnit c ON a.UnitID = c.UnitID " +
                                    "INNER JOIN tblProductSubGroup d ON b.ProductSubGroupID = d.ProductSubGroupID " +
                                    "INNER JOIN tblProductGroup e ON d.ProductGroupID = e.ProductGroupID " +
                                    "INNER JOIN tblContacts f ON a.SupplierID = f.ContactID " +
                                    "LEFT OUTER JOIN tblproductvariationsmatrix g on a.MatrixID = g.MatrixID " +
                                "WHERE a.Deleted = 0 AND z.BranchID = " + BranchID + " ";

            return stSQL;
        }

		public MySqlDataReader List(string SortField, SortOption SortOrder)
		{
			try
			{
				string SQL =	"SELECT a.MatriXID, " +
									"a.ProductID, " +
									"Description, " +
									"CONCAT(ProductDesc, ':' , Description) AS VariationDesc, " +
									"ProductDesc " +
								"FROM tblProductBaseVariationsMatrix a INNER JOIN " +
								    "tblProducts b ON a.ProductID = b.ProductID " +
								"WHERE 1=1 ORDER BY " + SortField;

				if (SortOrder == SortOption.Ascending)
					SQL += " ASC";
				else
					SQL += " DESC";

				

				MySqlCommand cmd = new MySqlCommand();
				
				
				cmd.CommandType = System.Data.CommandType.Text;
				cmd.CommandText = SQL;
				
				
				
				return base.ExecuteReader(cmd);			
			}
			catch (Exception ex)
			{
				
				
				{
					
					
					
					
				}

				throw base.ThrowException(ex);
			}	
		}

		public MySqlDataReader BaseList(string SortField, SortOption SortOrder)
		{
			try
			{
				string SQL = SQLSelect() + "ORDER BY " + SortField;

				if (SortOrder == SortOption.Ascending)
					SQL += " ASC";
				else
					SQL += " DESC";

				

				MySqlCommand cmd = new MySqlCommand();
				
				
				cmd.CommandType = System.Data.CommandType.Text;
				cmd.CommandText = SQL;
				
				
				
				return base.ExecuteReader(cmd);			
			}
			catch (Exception ex)
			{
				
				
				{
					
					
					
					
				}

				throw base.ThrowException(ex);
			}	
		}

		public MySqlDataReader BaseList(string SortField, SortOption SortOrder, string ProductIDs)
		{
			try
			{
				string SQL = SQLSelect();
				
				if (ProductIDs != "" && ProductIDs != null)
					SQL += "AND a.ProductID in (" + ProductIDs + ") ";
			
				SQL += "ORDER BY " + SortField;

				if (SortOrder == SortOption.Ascending)
					SQL += " ASC";
				else
					SQL += " DESC";

				

				MySqlCommand cmd = new MySqlCommand();
				
				
				cmd.CommandType = System.Data.CommandType.Text;
				cmd.CommandText = SQL;
				
				
				
				return base.ExecuteReader(cmd);
			}
			catch (Exception ex)
			{
				
				
				{
					
					
					
					
				}

				throw base.ThrowException(ex);
			}	
		}

		public MySqlDataReader BaseList(string SortField, SortOption SortOrder, string ProductGroupName, string ProductSubGroupName)
		{
			try
			{
				string SQL =	SQLSelect();
				
				if (ProductGroupName != "" && ProductGroupName != null)
					SQL += "AND ProductGroupName = '" + ProductGroupName + "' ";
			
				if (ProductSubGroupName != "" && ProductSubGroupName != null)
					SQL += "AND ProductSubGroupName = '" + ProductSubGroupName + "' ";

				SQL += "ORDER BY " + SortField;

				if (SortOrder == SortOption.Ascending)
					SQL += " ASC";
				else
					SQL += " DESC";

				

				MySqlCommand cmd = new MySqlCommand();
				
				
				cmd.CommandType = System.Data.CommandType.Text;
				cmd.CommandText = SQL;
				
				
				
				return base.ExecuteReader(cmd);			
			}
			catch (Exception ex)
			{
				
				
				{
					
					
					
					
				}

				throw base.ThrowException(ex);
			}	
		}

		public MySqlDataReader BaseList(long ProductID, string SortField, SortOption SortOrder)
		{
			try
			{
				string SQL =	SQLSelect() + "AND a.ProductID = @ProductID " +
								"ORDER BY " + SortField;

				if (SortOrder == SortOption.Ascending)
					SQL += " ASC";
				else
					SQL += " DESC";

				

				MySqlCommand cmd = new MySqlCommand();
				
				
				cmd.CommandType = System.Data.CommandType.Text;
				cmd.CommandText = SQL;
				
				MySqlParameter prmProductID = new MySqlParameter("@ProductID",MySqlDbType.Int32);			
				prmProductID.Value = ProductID;
				cmd.Parameters.Add(prmProductID);

				
				
				return base.ExecuteReader(cmd);			
			}
			catch (Exception ex)
			{
				
				
				{
					
					
					
					
				}

				throw base.ThrowException(ex);
			}	
		}

        public System.Data.DataTable BaseListAsDataTable(long ProductID=0, string SortField="Description", SortOption SortOrder=SortOption.Ascending)
        {
            try
            {
                MySqlCommand cmd = new MySqlCommand();
                string SQL = SQLSelect();

                if (ProductID != 0)
                {
                    SQL += "AND a.ProductID = @ProductID ";
                    cmd.Parameters.AddWithValue("@ProductID", ProductID);
                }

                SQL += "ORDER BY " + SortField;
                if (SortOrder == SortOption.Ascending)
                    SQL += " ASC";
                else
                    SQL += " DESC";

                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandText = SQL;

                string strDataTableName = "tbl" + this.GetType().FullName.Split(new Char[] { '.' })[this.GetType().FullName.Split(new Char[] { '.' }).Length - 1]; System.Data.DataTable dt = new System.Data.DataTable(strDataTableName);
                base.MySqlDataAdapterFill(cmd, dt);

                return dt;
            }
            catch (Exception ex)
            {
                throw base.ThrowException(ex);
            }
        }

        //public System.Data.DataTable BaseListAsDataTable(int BranchID, long ProductID, string SortField, SortOption SortOrder)
        //{
        //    try
        //    {
        //        string SQL = string.Empty;
        //        if (BranchID == Constants.BRANCH_ID_MAIN)
        //            SQL = SQLSelect();
        //        else
        //            SQL = SQLSelect(BranchID);

        //        SQL += "AND a.ProductID = @ProductID ";

        //        if (SortField == string.Empty) SortField = "a.MatrixID";
        //            SQL += "ORDER BY " + SortField;

        //        if (SortOrder == SortOption.Ascending)
        //            SQL += " ASC";
        //        else
        //            SQL += " DESC";

                
        //        MySqlCommand cmd = new MySqlCommand();
                
                
        //        cmd.CommandType = System.Data.CommandType.Text;
        //        cmd.CommandText = SQL;

        //        cmd.Parameters.AddWithValue("@ProductID", ProductID);

        //        string strDataTableName = "tbl" + this.GetType().FullName.Split(new Char[] { '.' })[this.GetType().FullName.Split(new Char[] { '.' }).Length - 1]; System.Data.DataTable dt = new System.Data.DataTable(strDataTableName);
        //        base.MySqlDataAdapterFill(cmd, dt);

        //        return dt;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw base.ThrowException(ex);
        //    }
        //}

        public System.Data.DataTable BaseListAsDataPerBranch(int BranchID, long ProductID, string SortField, SortOption SortOrder)
        {
            try
            {
                string SQL = SQLSelect(BranchID);

                SQL += "AND a.ProductID = @ProductID ";

                if (SortField == string.Empty) SortField = "a.MatrixID";
                SQL += "ORDER BY " + SortField;

                if (SortOrder == SortOption.Ascending)
                    SQL += " ASC";
                else
                    SQL += " DESC";


                MySqlCommand cmd = new MySqlCommand();


                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandText = SQL;

                cmd.Parameters.AddWithValue("@ProductID", ProductID);

                string strDataTableName = "tbl" + this.GetType().FullName.Split(new Char[] { '.' })[this.GetType().FullName.Split(new Char[] { '.' }).Length - 1]; System.Data.DataTable dt = new System.Data.DataTable(strDataTableName);
                base.MySqlDataAdapterFill(cmd, dt);

                return dt;
            }
            catch (Exception ex)
            {
                throw base.ThrowException(ex);
            }
        }

		public MySqlDataReader Search(Int64 ProductID, string SearchKey, string SortField, SortOption SortOrder)
		{
			try
			{
				string SQL = SQLSelect() + "AND a.ProductID = @ProductID " +
                            "AND (a.Description LIKE @SearchKey " +
							"OR UnitName LIKE @SearchKey) " +
							"ORDER BY " + SortField;

				if (SortOrder == SortOption.Ascending)
					SQL += " ASC";
				else
					SQL += " DESC";

				MySqlCommand cmd = new MySqlCommand();
				cmd.CommandType = System.Data.CommandType.Text;
				cmd.CommandText = SQL;
				
				MySqlParameter prmProductID = new MySqlParameter("@ProductID",MySqlDbType.Int64);			
				prmProductID.Value = ProductID;
				cmd.Parameters.Add(prmProductID);

				MySqlParameter prmSearchKey = new MySqlParameter("@SearchKey",MySqlDbType.String);			
				prmSearchKey.Value = "%" + SearchKey + "%";
				cmd.Parameters.Add(prmSearchKey);

				return base.ExecuteReader(cmd);
			}
			catch (Exception ex)
			{
				throw base.ThrowException(ex);
			}	
		}

        public System.Data.DataTable SearchAsDataTable(Int64 ProductID, string SearchKey, string SortField, SortOption SortOrder)
        {
            try
            {
                string SQL = SQLSelect() + "AND a.ProductID = @ProductID " +
                            "AND (a.Description LIKE @SearchKey " +
                            "OR UnitName LIKE @SearchKey) " +
                            "ORDER BY " + SortField;

                if (SortOrder == SortOption.Ascending)
                    SQL += " ASC";
                else
                    SQL += " DESC";

                
                MySqlCommand cmd = new MySqlCommand();
                
                
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandText = SQL;

                cmd.Parameters.AddWithValue("@ProductID", ProductID);
                cmd.Parameters.AddWithValue("@SearchKey", "%" + SearchKey + "%");

                string strDataTableName = "tbl" + this.GetType().FullName.Split(new Char[] { '.' })[this.GetType().FullName.Split(new Char[] { '.' }).Length - 1]; System.Data.DataTable dt = new System.Data.DataTable(strDataTableName);
                base.MySqlDataAdapterFill(cmd, dt);

                return dt;
            }
            catch (Exception ex)
            {
                throw base.ThrowException(ex);
            }
        }

		public MySqlDataReader Search(Int64 ProductID, string SearchKey, string SortField, SortOption SortOrder, Int32 Limit, bool isQuantityGreaterThanZERO)
		{
			try
			{
				string SQL = SQLSelect() + "AND a.ProductID = @ProductID " +
								"AND (a.Description LIKE @SearchKey " +
								"OR UnitName LIKE @SearchKey) ";
				if (isQuantityGreaterThanZERO)
					SQL += "AND a.Quantity > 0 ";

				SQL += "ORDER BY " + SortField; 

				if (SortOrder == SortOption.Ascending)
					SQL += " ASC ";
				else
					SQL += " DESC ";

				if (Limit != 0)
					SQL += "LIMIT " + Limit;

				

				MySqlCommand cmd = new MySqlCommand();
				
				
				cmd.CommandType = System.Data.CommandType.Text;
				cmd.CommandText = SQL;
				
				MySqlParameter prmProductID = new MySqlParameter("@ProductID",MySqlDbType.Int32);			
				prmProductID.Value = ProductID;
				cmd.Parameters.Add(prmProductID);

				MySqlParameter prmSearchKey = new MySqlParameter("@SearchKey",MySqlDbType.String);			
				prmSearchKey.Value = SearchKey + "%";
				cmd.Parameters.Add(prmSearchKey);

				
				
				return base.ExecuteReader(cmd);
			}
			catch (Exception ex)
			{
				
				
				{
					
					
					
					
				}

				throw base.ThrowException(ex);
			}	
		}
        
		#endregion

		#region tblProductBaseVariationsMatrix Streams for Report 

		public System.Data.DataTable InventoryReport(string SortField, SortOption SortOrder)
		{
			try
			{
                //string SQL =	"SELECT MatriXID, " +
                //                    "a.ProductID, " +
                //                    "Description, " +
                //                    "CONCAT(ProductDesc, ':' , Description) AS VariationDesc, " +
                //                    "ProductDesc, " + 
                //                    "c.UnitName, " + 
                //                    "a.Price, " +
                //                    "a.WSPrice, " +
                //                    "a.Quantity, " +
                //                    "a.MinThreshold, " +
                //                    "a.MaxThreshold, " +
                //                    "a.PurchasePrice, " +
                //                    "e.ContactName AS SupplierName, " +
                //                    "a.QuantityIN, " +
                //                    "a.QuantityOUT " +
                //                "FROM tblProductBaseVariationsMatrix a " +
                //                    "INNER JOIN tblProducts b ON a.ProductID = b.ProductID " +
                //                    "INNER JOIN tblUnit c ON a.UnitID = c.UnitID " +	
                //                    "INNER JOIN tblContacts e ON a.SupplierID = e.ContactID " +
                //                "WHERE 1=1 ORDER BY " + SortField;

                string SQL = SQLSelect() + "ORDER BY " + SortField;
				if (SortOrder == SortOption.Ascending)
					SQL += " ASC";
				else
					SQL += " DESC";

				
				System.Data.DataTable dt = new System.Data.DataTable("ProductVariations");
				base.MySqlDataAdapterFill(SQL, dt);
				

				return dt;	
			}
			catch (Exception ex)
			{
				
				
				{
					
					
					
					
				}

				throw base.ThrowException(ex);
			}	
		}
		public System.Data.DataTable InventoryReport(DateTime ExpiryDate, string SortField, SortOption SortOrder)
		{
			try
			{
                string SQL = SQLSelect();

				if (ExpiryDate != DateTime.MinValue)
				{
					SQL += "AND VariationID = " + CONSTANT_VARIATIONS.EXPIRATION.ToString("d") + " "; 
					SQL += "AND DATE_FORMAT(g.Description, '%Y-%m-%d %H:%i') <= DATE_FORMAT('" + ExpiryDate.ToString("yyyy-MM-dd HH:mm:ss") + "', '%Y-%m-%d %H:%i') ";
				}

				SQL += "ORDER BY " + SortField;

				if (SortOrder == SortOption.Ascending)
					SQL += " ASC";
				else
					SQL += " DESC";

				
				System.Data.DataTable dt = new System.Data.DataTable("ProductVariations");
				base.MySqlDataAdapterFill(SQL, dt);
				

				return dt;	
			}
			catch (Exception ex)
			{
				
				
				{
					
					
					
					
				}

				throw base.ThrowException(ex);
			}	
		}
        public System.Data.DataTable InventoryReport(string ProductGroupName, string ProductSubGroupName, string ProductCode)
        {
            try
            {
                string SQL = SQLSelect();

                if (ProductGroupName != "" && ProductGroupName != null)
                {
                    SQL += "AND ProductGroupName = '" + ProductGroupName + "' ";
                }
                if (ProductSubGroupName != "" && ProductSubGroupName != null)
                {
                    SQL += "AND ProductSubGroupName = '" + ProductSubGroupName + "' ";
                }

                if (ProductCode != "" && ProductCode != null)
                {
                    string stSQL = "";
                    foreach (string stProductCode in ProductCode.Split(';'))
                    {
                        stSQL += "OR ProductCode LIKE '%" + stProductCode + "%' ";
                    }
                    SQL += "AND (" + stSQL.Remove(0, 2) + ")";
                }
                SQL += "ORDER BY a.Quantity ASC";

                
                System.Data.DataTable dt = new System.Data.DataTable("ProductVariations");
                base.MySqlDataAdapterFill(SQL, dt);
                

                return dt;
            }
            catch (Exception ex)
            {
                
                
                {
                    
                    
                    
                    
                }

                throw base.ThrowException(ex);
            }
        }

        private string SQLSelectForReorder()
        {
            string SQL = SQLSelect() + "AND a.Quantity <= a.MinThreshold ";
            return SQL;
        }
		public System.Data.DataTable ForReorder(string SortField, SortOption SortOrder)
		{
			try
			{
				string SQL = SQLSelectForReorder() + "ORDER BY " + SortField;

				if (SortOrder == SortOption.Ascending)
					SQL += " ASC";
				else
					SQL += " DESC";

				
				System.Data.DataTable dt = new System.Data.DataTable("ProductVariations");
				base.MySqlDataAdapterFill(SQL, dt);
				

				return dt;	
			}
			catch (Exception ex)
			{
				
				
				{
					
					
					
					
				}

				throw base.ThrowException(ex);
			}	
		}
		public System.Data.DataTable ForReorder(long ProductID, long SupplierID)
		{
			try
			{
                string SQL = SQLSelectForReorder();

				SQL += "AND a.ProductID = " + ProductID + " ";
                SQL += "AND a.SupplierID = " + SupplierID + " ";

				
				System.Data.DataTable dt = new System.Data.DataTable("ProductVariations");
				base.MySqlDataAdapterFill(SQL, dt);
				

				return dt;	
			}
			catch (Exception ex)
			{
				
				
				{
					
					
					
					
				}

				throw base.ThrowException(ex);
			}	
		}
		public System.Data.DataTable OverStock(string SortField, SortOption SortOrder)
		{
			try
			{
                string SQL = SQLSelect() + "AND a.Quantity >= a.MaxThreshold ORDER BY " + SortField;

				if (SortOrder == SortOption.Ascending)
					SQL += " ASC";
				else
					SQL += " DESC";

				
				System.Data.DataTable dt = new System.Data.DataTable("ProductVariations");
				base.MySqlDataAdapterFill(SQL, dt);
				

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



		#region ProductVariationsMatrixList Insert and Update

		public bool InsertVariation(ProductVariationsMatrixDetails Details)
		{
			try 
			{
				string SQL = "INSERT INTO tblProductVariationsMatrix (MatrixID, VariationID, Description) VALUES (@MatrixID, @VariationID, @Description);";
				  
				
	 			
				MySqlCommand cmd = new MySqlCommand();
				
				
				cmd.CommandType = System.Data.CommandType.Text;
				cmd.CommandText = SQL;
				
				MySqlParameter prmMatrixID = new MySqlParameter("@MatrixID",MySqlDbType.Int32);			
				prmMatrixID.Value = Details.MatrixID;
				cmd.Parameters.Add(prmMatrixID);

				MySqlParameter prmVariationID = new MySqlParameter("@VariationID",MySqlDbType.Int32);			
				prmVariationID.Value = Details.VariationID;
				cmd.Parameters.Add(prmVariationID);

				MySqlParameter prmDescription = new MySqlParameter("@Description",MySqlDbType.String);			
				prmDescription.Value = Details.Description;
				cmd.Parameters.Add(prmDescription);

				base.ExecuteNonQuery(cmd);
				
				return true;
			}

			catch (Exception ex)
			{
				
				
				{
					
					
					
					
				}

				throw base.ThrowException(ex);
			}	
		}

		public void Update(ProductVariationsMatrixDetails Details)
		{
			try 
			{
				string SQL=	"UPDATE tblProductVariationsMatrix SET " + 
					"Description = @Description " +  
					"WHERE MatrixID = @MatrixID " + 
					"AND VariationID = @VariationID;";
				  
				
	 			
				MySqlCommand cmd = new MySqlCommand();
				
				
				cmd.CommandType = System.Data.CommandType.Text;
				cmd.CommandText = SQL;
				
				MySqlParameter prmDescription = new MySqlParameter("@Description",MySqlDbType.String);			
				prmDescription.Value = Details.Description;
				cmd.Parameters.Add(prmDescription);

				MySqlParameter prmMatrixID = new MySqlParameter("@MatrixID",MySqlDbType.Int32);			
				prmMatrixID.Value = Details.MatrixID;
				cmd.Parameters.Add(prmMatrixID);

				MySqlParameter prmVariationID = new MySqlParameter("@VariationID",MySqlDbType.Int32);			
				prmVariationID.Value = Details.VariationID;
				cmd.Parameters.Add(prmVariationID);

				base.ExecuteNonQuery(cmd);
			}

			catch (Exception ex)
			{
				
				
				{
					
					
					
					
				}
				throw base.ThrowException(ex);
			}	
		}

		
		#endregion

		#region ProductVariationsMatrixList Details

		public ProductVariationsMatrixDetails Details(long MatrixID, long VariationID)
		{
			try
			{
				string SQL=	"SELECT MatrixID, VariationID, Description FROM tblProductVariationsMatrix " +
					"WHERE MatrixID = @MatrixID AND VariationID = @VariationID;";
				  
				
	 			
				MySqlCommand cmd = new MySqlCommand();
				
				
				cmd.CommandType = System.Data.CommandType.Text;
				cmd.CommandText = SQL;

				MySqlParameter prmMatrixID = new MySqlParameter("@MatrixID",MySqlDbType.Int32);
				prmMatrixID.Value = MatrixID;
				cmd.Parameters.Add(prmMatrixID);

				MySqlParameter prmVariationID = new MySqlParameter("@VariationID",MySqlDbType.Int32);
				prmVariationID.Value = VariationID;
				cmd.Parameters.Add(prmVariationID);

				MySqlDataReader myReader = base.ExecuteReader(cmd, System.Data.CommandBehavior.SingleResult);
				
				ProductVariationsMatrixDetails Details = new ProductVariationsMatrixDetails();

				while (myReader.Read()) 
				{
					Details.MatrixID = MatrixID;
					Details.VariationID = VariationID;
					Details.Description = "" + myReader["Description"].ToString();
				}

				myReader.Close();

				return Details;
			}

			catch (Exception ex)
			{
				
				
				{
					
					
					
					
				}

				throw base.ThrowException(ex);
			}	
		}


		#endregion

		#region ProductVariationsMatrixList Streams

		public MySqlDataReader ProductVariationsMatrixList(long MatriXID, string SortField, SortOption SortOrder)
		{
			try
			{
				string SQL =	"SELECT MatriXID, " +
									"a.VariationID, " +
									"Description, " +
									"b.VariationCode, " +
									"b.VariationType " +
								"FROM tblProductVariationsMatrix a INNER JOIN " +
								"tblVariations b ON a.VariationID = b.VariationID " +
								"WHERE 1=1 " +
								"AND MatriXID = @MatriXID " +
								"ORDER BY " + SortField;

				if (SortOrder == SortOption.Ascending)
					SQL += " ASC";
				else
					SQL += " DESC";

				

				MySqlCommand cmd = new MySqlCommand();
				
				
				cmd.CommandType = System.Data.CommandType.Text;
				cmd.CommandText = SQL;
				
				MySqlParameter prmMatriXID = new MySqlParameter("@MatriXID",MySqlDbType.Int64);			
				prmMatriXID.Value = MatriXID;
				cmd.Parameters.Add(prmMatriXID);

				
				
				return base.ExecuteReader(cmd);			
			}
			catch (Exception ex)
			{
				
				
				{
					
					
					
					
				}

				throw base.ThrowException(ex);
			}	
		}
        public System.Data.DataTable ProductVariationsMatrixListAsDataTable(long MatriXID, string SortField, System.Data.SqlClient.SortOrder SortOrder)
        {
            try
            {
                string SQL = "SELECT MatriXID, " +
                                    "a.VariationID, " +
                                    "Description, " +
                                    "b.VariationCode, " +
                                    "b.VariationType " +
                                "FROM tblProductVariationsMatrix a INNER JOIN " +
                                "tblVariations b ON a.VariationID = b.VariationID " +
                                "WHERE 1=1 " +
                                "AND MatriXID = @MatriXID ";

                if (SortField != string.Empty && SortField != null)
                {
                    SQL += "ORDER BY " + SortField + " ";

                    if (SortOrder != System.Data.SqlClient.SortOrder.Descending) SQL += "ASC ";
                    else SQL += "DESC ";
                }

                

                MySqlCommand cmd = new MySqlCommand();
                
                
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandText = SQL;
                cmd.Parameters.AddWithValue("@MatriXID", MatriXID);

                System.Data.DataTable dt = new System.Data.DataTable("tblVariations");
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

		#region isExist

		public bool isExist(long MatrixID, long VariationID)
		{
			try 
			{
				string SQL = "SELECT COUNT(*) FROM tblProductVariationsMatrix " +
					"WHERE MatrixID = @MatrixID AND VariationID = @VariationID;";
				  
				
	 			
				MySqlCommand cmd = new MySqlCommand();
				
				
				cmd.CommandType = System.Data.CommandType.Text;
				cmd.CommandText = SQL;
				
				MySqlParameter prmMatrixID = new MySqlParameter("@MatrixID",MySqlDbType.Int64);			
				prmMatrixID.Value = MatrixID;
				cmd.Parameters.Add(prmMatrixID);

				MySqlParameter prmVariationID = new MySqlParameter("@VariationID",MySqlDbType.Int32);			
				prmVariationID.Value = VariationID;
				cmd.Parameters.Add(prmVariationID);

				MySqlDataReader myReader = base.ExecuteReader(cmd, System.Data.CommandBehavior.SingleResult);
				
				bool boRetValue = false;

				while (myReader.Read())
				{
					boRetValue = true;
				}

				return boRetValue;
			}

			catch (Exception ex)
			{
				
				
				{
					
					
					
					
				}

				throw base.ThrowException(ex);
			}	
		}


		#endregion


		#region Change VAT, EVAT and Localtax

        //// Dec 10, 2011 : Obsolete, change to ChangeTax
        //public void ChangeVAT(decimal OldVAT, decimal NewVAT)
        //{
        //    try 
        //    {
        //        string SQL =	"UPDATE tblProductBaseVariationsMatrix SET " +
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
        //// Dec 10, 2011 : Obsolete, change to ChangeTax
        //public void ChangeEVAT(decimal OldEVAT, decimal NewEVAT)
        //{
        //    try 
        //    {
        //        string SQL =	"UPDATE tblProductBaseVariationsMatrix SET " +
        //                            "EVAT		= @NewEVAT " +
        //                        "WHERE EVAT		= @OldEVAT;";
				  
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
        //// Dec 10, 2011 : Obsolete, change to ChangeTax
        //public void ChangeLocalTax(decimal OldLocalTax, decimal NewLocalTax)
        //{
        //    try 
        //    {
        //        string SQL =	"UPDATE tblProductBaseVariationsMatrix SET " +
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

        public void ChangeTax(long ProductGroupID, long ProductSubGroupID, long ProductID, decimal NewVAT, decimal NewEVAT, decimal NewLocalTax)
        {
            try
            {
                string SQL = "UPDATE tblProductBaseVariationsMatrix SET " +
                                    "VAT		= @NewVAT, " +
                                    "EVAT		= @NewEVAT, " +
                                    "LocalTax	= @NewLocalTax ";

                if (ProductID != 0) SQL += "WHERE ProductID = @ProductID;";
                else if (ProductSubGroupID != 0) SQL += "WHERE ProductID IN (SELECT DISTINCT(ProductID) FROM tblProducts WHERE ProductSubGroupID = @ProductSubGroupID);";
                else if (ProductGroupID != 0) SQL += "WHERE ProductID IN (SELECT DISTINCT(ProductID) FROM tblProducts WHERE ProductSubGroupID IN (SELECT DISTINCT(ProductSubGroupID) FROM tblProductSubGroup WHERE ProductGroupID = @ProductGroupID));";

                

                MySqlCommand cmd = new MySqlCommand();
                
                
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandText = SQL;

                MySqlParameter prmNewVAT = new MySqlParameter("@NewVAT",MySqlDbType.Decimal);
                prmNewVAT.Value = NewVAT;
                cmd.Parameters.Add(prmNewVAT);

                MySqlParameter prmNewEVAT = new MySqlParameter("@NewEVAT",MySqlDbType.Decimal);
                prmNewEVAT.Value = NewEVAT;
                cmd.Parameters.Add(prmNewEVAT);

                MySqlParameter prmNewLocalTax = new MySqlParameter("@NewLocalTax",MySqlDbType.Decimal);
                prmNewLocalTax.Value = NewLocalTax;
                cmd.Parameters.Add(prmNewLocalTax);

                if (ProductID != 0)
                {
                    MySqlParameter prmProductID = new MySqlParameter("@ProductID",MySqlDbType.Int64);
                    prmProductID.Value = ProductID;
                    cmd.Parameters.Add(prmProductID);
                }
                else if (ProductSubGroupID != 0)
                {
                    MySqlParameter prmProductSubGroupID = new MySqlParameter("@ProductSubGroupID",MySqlDbType.Int64);
                    prmProductSubGroupID.Value = ProductSubGroupID;
                    cmd.Parameters.Add(prmProductSubGroupID);
                }
                else if (ProductGroupID != 0)
                {
                    MySqlParameter prmProductGroupID = new MySqlParameter("@ProductGroupID",MySqlDbType.Int64);
                    prmProductGroupID.Value = ProductGroupID;
                    cmd.Parameters.Add(prmProductGroupID);
                }

                base.ExecuteNonQuery(cmd);

                MatrixPackage clsMatrixPackage = new MatrixPackage(base.Connection, base.Transaction);
                clsMatrixPackage.ChangeTax(ProductGroupID, ProductSubGroupID, ProductID, NewVAT, NewEVAT, NewLocalTax);
            }

            catch (Exception ex)
            {
                throw base.ThrowException(ex);
            }
        }

		#endregion

		#region Add and Subtract Quantity

        /// <summary>
        /// Depreciated, use "public void Products.AddQuantity(long ProductID, long MatrixID, decimal Quantity, string Remarks, DateTime TransactionDate, string TransactionNo)" instead
        /// </summary>
        /// <param name="MatrixID"></param>
        /// <param name="Quantity"></param>
		public void AddQuantity(Int64 MatrixID, decimal Quantity)
		{
			try 
			{
				string SQL =	"UPDATE tblProductBaseVariationsMatrix SET " +
									"Quantity			= Quantity + @Quantity, " +
                                    "QuantityIN			= QuantityIN + @Quantity " +
								"WHERE MatrixID		= @MatrixID;";
				  
				
	 			
				MySqlCommand cmd = new MySqlCommand();
				
				
				cmd.CommandType = System.Data.CommandType.Text;
				cmd.CommandText = SQL;
				
				MySqlParameter prmQuantity = new MySqlParameter("@Quantity",MySqlDbType.Decimal);			
				prmQuantity.Value = Quantity;
				cmd.Parameters.Add(prmQuantity);

				MySqlParameter prmMatrixID = new MySqlParameter("@MatrixID",MySqlDbType.Int64);			
				prmMatrixID.Value = MatrixID;
				cmd.Parameters.Add(prmMatrixID);

				base.ExecuteNonQuery(cmd);
			}

			catch (Exception ex)
			{
				
				
				{
					
					
					
					
				}

				throw base.ThrowException(ex);
			}	
		}

        /// <summary>
        /// Depreciated, use "public void Products.SubtractQuantity(long ProductID, long MatrixID, decimal Quantity, string Remarks, DateTime TransactionDate, string TransactionNo)" instead
        /// </summary>
        /// <param name="ProductID"></param>
        /// <param name="Quantity"></param>
		public void SubtractQuantity(Int64 MatrixID, decimal Quantity)
		{
			try 
			{
				string SQL =	"UPDATE tblProductBaseVariationsMatrix SET " +
									"Quantity		= Quantity - @Quantity, " +
                                    "QuantityOUT	= QuantityOUT + @Quantity " +
								"WHERE MatrixID		= @MatrixID;";
							  
				
	 			
				MySqlCommand cmd = new MySqlCommand();
				
				
				cmd.CommandType = System.Data.CommandType.Text;
				cmd.CommandText = SQL;
				
				MySqlParameter prmQuantity = new MySqlParameter("@Quantity",MySqlDbType.Decimal);			
				prmQuantity.Value = Quantity;
				cmd.Parameters.Add(prmQuantity);

				MySqlParameter prmMatrixID = new MySqlParameter("@MatrixID",MySqlDbType.Int64);			
				prmMatrixID.Value = MatrixID;
				cmd.Parameters.Add(prmMatrixID);

				base.ExecuteNonQuery(cmd);
			}

			catch (Exception ex)
			{
				
				
				{
					
					
					
					
				}

				throw base.ThrowException(ex);
			}	
		}


		#endregion

		#region Delete

		public bool Delete(string IDs)
		{
			try 
			{
				

                string SQL = "CALL procProductBaseVariationsMatrixDelete(@IDs);";

				MySqlCommand cmd = new MySqlCommand();
				
				
				cmd.CommandType = System.Data.CommandType.Text;
				cmd.CommandText = SQL;
                
                cmd.Parameters.AddWithValue("@IDs", IDs);
				
                base.ExecuteNonQuery(cmd);

				return true;
			}

			catch (Exception ex)
			{
				
				
				{
					
					
					
					
				}

				throw base.ThrowException(ex);
			}	
		}

        public bool Delete(long ID)
        {
            try
            {
                

                string SQL = "CALL procProductBaseVariationsMatrixDeleteByID(@ID);";

                MySqlCommand cmd = new MySqlCommand();
                
                
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandText = SQL;

                cmd.Parameters.AddWithValue("@ID", ID);

                base.ExecuteNonQuery(cmd);

                return true;
            }

            catch (Exception ex)
            {
                
                
                {
                    
                    
                    
                    
                }

                throw base.ThrowException(ex);
            }
        }

		#endregion

		#region CountVariations
		public int CountVariations(long ProductID)
		{
			try
			{
				string SQL = "SELECT Count(MatrixID) FROM tblProductBaseVariationsMatrix WHERE ProductID = @ProductID "; 

				MySqlCommand cmd = new MySqlCommand();
				cmd.CommandType = System.Data.CommandType.Text;
				cmd.CommandText = SQL;

				MySqlParameter prmProductID = new MySqlParameter("@ProductID",MySqlDbType.Int64);			
				prmProductID.Value = ProductID;
				cmd.Parameters.Add(prmProductID);

                MySqlDataReader myReader = base.ExecuteReader(cmd, System.Data.CommandBehavior.SingleResult);

                int iRetValue = 0;

                while (myReader.Read())
                {
				    iRetValue = myReader.GetInt32(0);
                }
				myReader.Close();

				return iRetValue;
			}
			catch (Exception ex)
			{
				
				
				{
					
					
					
					
				}

				throw base.ThrowException(ex);
			}
		}


		#endregion

		#region IsVariationExists

		public bool IsVariationExists(long MatrixID, long VariationID)
		{
			try 
			{
				bool boRetValue = false;
					
				string SQL=	"SELECT * FROM tblProductVariationsMatrix " + 
					"WHERE MatrixID = @MatrixID " + 
					"AND VariationID = @VariationID;";
 
				
	 			
				MySqlCommand cmd = new MySqlCommand();
				
				
				cmd.CommandType = System.Data.CommandType.Text;
				cmd.CommandText = SQL;

				MySqlParameter prmMatrixID = new MySqlParameter("@MatrixID",MySqlDbType.Int32);			
				prmMatrixID.Value = MatrixID;
				cmd.Parameters.Add(prmMatrixID);

				MySqlParameter prmVariationID = new MySqlParameter("@VariationID",MySqlDbType.Int32);			
				prmVariationID.Value = VariationID;
				cmd.Parameters.Add(prmVariationID);

				MySqlDataReader myReader = base.ExecuteReader(cmd, System.Data.CommandBehavior.SingleResult);
				
				while (myReader.Read()) 
				{
					boRetValue = true;
				}

				myReader.Close();

				return boRetValue;
			}

			catch (Exception ex)
			{
				
				
				{
					
					
					
					
				}

				throw base.ThrowException(ex);
			}	
		}


		#endregion
	}
}


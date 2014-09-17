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
	public struct ProductSubGroupDetails
	{
		public Int64 ProductSubGroupID;
		public Int64 ProductGroupID;
		public string ProductSubGroupCode;
		public string ProductSubGroupName;
		public Int64 BaseUnitID;
		public string BaseUnitName;
		public decimal Price;
		public decimal PurchasePrice;
		public bool IncludeInSubtotalDiscount;
		public decimal VAT;
		public decimal EVAT;
		public decimal LocalTax;
        public int ChartOfAccountIDPurchase;
        public int ChartOfAccountIDSold;
        public int ChartOfAccountIDInventory;
        public int ChartOfAccountIDTaxPurchase;
        public int ChartOfAccountIDTaxSold;
        public int SequenceNo;

        public ProductSubGroupChartOfAccountDetails ProductSubGroupChartOfAccountDetails;

        public DateTime CreatedOn;
        public DateTime LastModified;
	}

    [StrongNameIdentityPermissionAttribute(SecurityAction.LinkDemand,
         PublicKey = "002400000480000094000000060200000024000" +
         "052534131000400000100010053D785642F9F960B43157E0380" +
         "F393BEE53E8DFAFBF441366C1B6F8B48D9DDF0D527B1F3B21EA" +
         "E85D2FDB664CE81EB8A87DBE4C589D6F4202FE2B7D4B978BB69" +
         "684874612CB9B8DB7A0339400A9C4E68277884B07817363D242" +
         "E3696F9FACDBEA831810AE6DC9EDCA91A7B5DA12FE7BF65D113" +
         "FF52834EAFB5A7A1FDFD5851A3")]
    public struct ProductSubGroupChartOfAccountDetails
    {
        public Int32 ChartOfAccountIDTransferIn;
        public Int32 ChartOfAccountIDTaxTransferIn;
        public Int32 ChartOfAccountIDTransferOut;
        public Int32 ChartOfAccountIDTaxTransferOut;
        public Int32 ChartOfAccountIDInvAdjustment;
        public Int32 ChartOfAccountIDTaxInvAdjustment;
    }

    /// <summary>
    /// Use for selecting the required columns for select.
    /// Column value should be equal to TRUE if will be included in the select statement
    /// </summary>
    public struct ProductSubGroupColumns
    {
        public bool ProductSubGroupID;
        public bool ProductGroupID;
        public bool ProductGroupCode;
        public bool ProductGroupName;
        public bool ProductSubGroupCode;
        public bool ProductSubGroupName;
        public bool BaseUnitID;
        public bool BaseUnitName;
        public bool Price;
        public bool PurchasePrice;
        public bool IncludeInSubtotalDiscount;
        public bool VAT;
        public bool EVAT;
        public bool LocalTax;
        public bool ChartOfAccountIDPurchase;
        public bool ChartOfAccountIDSold;
        public bool ChartOfAccountIDInventory;
        public bool ChartOfAccountIDTaxPurchase;
        public bool ChartOfAccountIDTaxSold;
        public bool SequenceNo;
    }

    public struct ProductSubGroupColumnNames
    {
        public const string ProductSubGroupID = "ProductSubGroupID";
        public const string ProductGroupID = "ProductGroupID";
        public const string ProductGroupCode = "ProductGroupCode";
        public const string ProductGroupName = "ProductGroupName";
        public const string ProductSubGroupCode = "ProductSubGroupCode";
        public const string ProductSubGroupName = "ProductSubGroupName";
        public const string BaseUnitID = "BaseUnitID";
        public const string BaseUnitName = "BaseUnitName";
        public const string Price = "Price";
        public const string PurchasePrice = "PurchasePrice";
        public const string IncludeInSubtotalDiscount = "IncludeInSubtotalDiscount";
        public const string VAT = "VAT";
        public const string EVAT = "EVAT";
        public const string LocalTax = "LocalTax";
        public const string ChartOfAccountIDPurchase = "ChartOfAccountIDPurchase";
        public const string ChartOfAccountIDSold = "ChartOfAccountIDSold";
        public const string ChartOfAccountIDInventory = "ChartOfAccountIDInventory";
        public const string ChartOfAccountIDTaxPurchase = "ChartOfAccountIDTaxPurchase";
        public const string ChartOfAccountIDTaxSold = "ChartOfAccountIDTaxSold";
        public const string SequenceNo = "SequenceNo";
    }

	[StrongNameIdentityPermissionAttribute(SecurityAction.LinkDemand,
		 PublicKey = "002400000480000094000000060200000024000" +
		 "052534131000400000100010053D785642F9F960B43157E0380" +
		 "F393BEE53E8DFAFBF441366C1B6F8B48D9DDF0D527B1F3B21EA" +
		 "E85D2FDB664CE81EB8A87DBE4C589D6F4202FE2B7D4B978BB69" +
		 "684874612CB9B8DB7A0339400A9C4E68277884B07817363D242" +
		 "E3696F9FACDBEA831810AE6DC9EDCA91A7B5DA12FE7BF65D113" +
		 "FF52834EAFB5A7A1FDFD5851A3")]
	public class ProductSubGroup : POSConnection
	{
        public const long DEFAULT_SUB_GROUP_ID = 1;

        #region Constructors and Destructors

		public ProductSubGroup()
            : base(null, null)
        {
        }

        public ProductSubGroup(MySqlConnection Connection, MySqlTransaction Transaction) 
            : base(Connection, Transaction)
		{

		}

		#endregion
			
		#region Insert and Update

		public Int64 Insert(ProductSubGroupDetails Details)
		{
			try 
			{
				string SQL = "INSERT INTO tblProductSubGroup (" +
								"ProductSubGroupCode, " +
								"ProductGroupID, " +
								"ProductSubGroupName, " +
								"BaseUnitID, " +
								"Price, " +
								"PurchasePrice, " +
								"IncludeInSubtotalDiscount, " +
								"VAT, " +
								"EVAT, " +
								"LocalTax" +
							") VALUES (" +
								"@ProductSubGroupCode, " +
								"@ProductGroupID, " +
								"@ProductSubGroupName, " +
								"@BaseUnitID, " +
								"@Price, " +
								"@PurchasePrice, " +
								"@IncludeInSubtotalDiscount, " +
								"@VAT, " +
								"@EVAT, " +
								"@LocalTax);";
				  
				MySqlCommand cmd = new MySqlCommand();
				cmd.CommandType = System.Data.CommandType.Text;
				cmd.CommandText = SQL;

                cmd.Parameters.AddWithValue("ProductGroupID", Details.ProductGroupID);
                cmd.Parameters.AddWithValue("ProductSubGroupCode", Details.ProductSubGroupCode);
                cmd.Parameters.AddWithValue("ProductSubGroupName", Details.ProductSubGroupName);
                cmd.Parameters.AddWithValue("BaseUnitID", Details.BaseUnitID);
                cmd.Parameters.AddWithValue("Price", Details.Price);
                cmd.Parameters.AddWithValue("PurchasePrice", Details.PurchasePrice);
                cmd.Parameters.AddWithValue("IncludeInSubtotalDiscount", Details.IncludeInSubtotalDiscount);
                cmd.Parameters.AddWithValue("VAT", Details.VAT);
                cmd.Parameters.AddWithValue("EVAT", Details.EVAT);
                cmd.Parameters.AddWithValue("LocalTax", Details.LocalTax);
                
				base.ExecuteNonQuery(cmd);

				SQL = "SELECT LAST_INSERT_ID();";
				
				cmd.Parameters.Clear(); 
				cmd.CommandText = SQL;

                System.Data.DataTable dt = new System.Data.DataTable("LAST_INSERT_ID");
                base.MySqlDataAdapterFill(cmd, dt);
                

                Int64 iID = 0;
                foreach (System.Data.DataRow dr in dt.Rows)
                {
                    iID = Int64.Parse(dr[0].ToString());
                }

				return iID;
			}

			catch (Exception ex)
			{
				throw base.ThrowException(ex);
			}	
		}

		public void Update(ProductSubGroupDetails Details)
		{
			try 
			{
				string SQL = "UPDATE tblProductSubGroup SET " + 
								"ProductGroupID			= @ProductGroupID, " +
								"ProductSubGroupCode	= @ProductSubGroupCode, " +  
								"ProductSubGroupName	= @ProductSubGroupName, " +  
								"BaseUnitID				= @BaseUnitID, " +  
								"Price					= @Price, " +
								"PurchasePrice			= @PurchasePrice, " +
								"IncludeInSubtotalDiscount	=	@IncludeInSubtotalDiscount, " +
								"VAT					= @VAT, " +
								"EVAT					= @EVAT, " +
								"LocalTax				= @LocalTax " +
							"WHERE ProductSubGroupID = @ProductSubGroupID;";
				  
				
	 			
				MySqlCommand cmd = new MySqlCommand();
				
				
				cmd.CommandType = System.Data.CommandType.Text;
				cmd.CommandText = SQL;

				MySqlParameter prmProductGroupID = new MySqlParameter("@ProductGroupID",MySqlDbType.Int16);			
				prmProductGroupID.Value = Details.ProductGroupID;
				cmd.Parameters.Add(prmProductGroupID);
				
				MySqlParameter prmProductSubGroupCode = new MySqlParameter("@ProductSubGroupCode",MySqlDbType.String);			
				prmProductSubGroupCode.Value = Details.ProductSubGroupCode;
				cmd.Parameters.Add(prmProductSubGroupCode);

				MySqlParameter prmProductSubGroupName = new MySqlParameter("@ProductSubGroupName",MySqlDbType.String);			
				prmProductSubGroupName.Value = Details.ProductSubGroupName;
				cmd.Parameters.Add(prmProductSubGroupName);

				MySqlParameter prmBaseUnitID = new MySqlParameter("@BaseUnitID",MySqlDbType.Int16);			
				prmBaseUnitID.Value = Details.BaseUnitID;
				cmd.Parameters.Add(prmBaseUnitID);

				MySqlParameter prmPrice = new MySqlParameter("@Price",MySqlDbType.Decimal);			
				prmPrice.Value = Details.Price;
				cmd.Parameters.Add(prmPrice);

				MySqlParameter prmPurchasePrice = new MySqlParameter("@PurchasePrice",MySqlDbType.Decimal);			
				prmPurchasePrice.Value = Details.PurchasePrice;
				cmd.Parameters.Add(prmPurchasePrice);

				MySqlParameter prmIncludeInSubtotalDiscount = new MySqlParameter("@IncludeInSubtotalDiscount",MySqlDbType.Int16);			
				prmIncludeInSubtotalDiscount.Value = Details.IncludeInSubtotalDiscount;
				cmd.Parameters.Add(prmIncludeInSubtotalDiscount);

				MySqlParameter prmVAT = new MySqlParameter("@VAT",MySqlDbType.Decimal);			
				prmVAT.Value = Details.VAT;
				cmd.Parameters.Add(prmVAT);

				MySqlParameter prmEVAT = new MySqlParameter("@EVAT",MySqlDbType.Decimal);			
				prmEVAT.Value = Details.EVAT;
				cmd.Parameters.Add(prmEVAT);

				MySqlParameter prmLocalTax = new MySqlParameter("@LocalTax",MySqlDbType.Decimal);			
				prmLocalTax.Value = Details.LocalTax;
				cmd.Parameters.Add(prmLocalTax);

				MySqlParameter prmProductSubGroupID = new MySqlParameter("@ProductSubGroupID",MySqlDbType.Int16);			
				prmProductSubGroupID.Value = Details.ProductSubGroupID;
				cmd.Parameters.Add(prmProductSubGroupID);

				base.ExecuteNonQuery(cmd);
			}

			catch (Exception ex)
			{
				throw base.ThrowException(ex);
			}	
		}
        //public void ChangeTax(long ProductGroupID, long ProductSubGroupID, decimal NewVAT, decimal NewEVAT, decimal NewLocalTax, string CreatedBy)
        //{
        //    try
        //    {
        //        Products clsProduct = new Products(base.Connection, base.Transaction);
        //        clsProduct.ChangeTax(ProductGroupID, ProductSubGroupID, 0, NewVAT, NewEVAT, NewLocalTax, CreatedBy);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw base.ThrowException(ex);
        //    }
        //}

        public void UpdateFinancialInformation(int ChartOfAccountIDPurchase, int ChartOfAccountIDSold, int ChartOfAccountIDInventory, int ChartOfAccountIDTaxPurchase, int ChartOfAccountIDTaxSold)
        {
            try
            {
                string SQL = "UPDATE tblProductSubGroup SET " +
                                    "ChartOfAccountIDPurchase	= @ChartOfAccountIDPurchase, " +
                                    "ChartOfAccountIDSold		= @ChartOfAccountIDSold, " +
                                    "ChartOfAccountIDInventory	= @ChartOfAccountIDInventory, " +
                                    "ChartOfAccountIDTaxPurchase = @ChartOfAccountIDTaxPurchase, " +
                                    "ChartOfAccountIDTaxSold        = @ChartOfAccountIDTaxSold, " +
                                    "ChartOfAccountIDTransferIn	    = @ChartOfAccountIDPurchase, " +
                                    "ChartOfAccountIDTaxTransferIn  = @ChartOfAccountIDTaxPurchase, " +
                                    "ChartOfAccountIDTransferOut	= @ChartOfAccountIDSold, " +
                                    "ChartOfAccountIDTaxTransferOut = @ChartOfAccountIDTaxSold;";

                

                MySqlCommand cmd = new MySqlCommand();
                
                
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandText = SQL;

                MySqlParameter prmChartOfAccountIDPurchase = new MySqlParameter("@ChartOfAccountIDPurchase",MySqlDbType.Int32);
                prmChartOfAccountIDPurchase.Value = ChartOfAccountIDPurchase;
                cmd.Parameters.Add(prmChartOfAccountIDPurchase);

                MySqlParameter prmChartOfAccountIDSold = new MySqlParameter("@ChartOfAccountIDSold",MySqlDbType.Int32);
                prmChartOfAccountIDSold.Value = ChartOfAccountIDSold;
                cmd.Parameters.Add(prmChartOfAccountIDSold);

                MySqlParameter prmChartOfAccountIDInventory = new MySqlParameter("@ChartOfAccountIDInventory",MySqlDbType.Int32);
                prmChartOfAccountIDInventory.Value = ChartOfAccountIDInventory;
                cmd.Parameters.Add(prmChartOfAccountIDInventory);

                MySqlParameter prmChartOfAccountIDTaxPurchase = new MySqlParameter("@ChartOfAccountIDTaxPurchase",MySqlDbType.Int32);
                prmChartOfAccountIDTaxPurchase.Value = ChartOfAccountIDTaxPurchase;
                cmd.Parameters.Add(prmChartOfAccountIDTaxPurchase);

                MySqlParameter prmChartOfAccountIDTaxSold = new MySqlParameter("@ChartOfAccountIDTaxSold",MySqlDbType.Int32);
                prmChartOfAccountIDTaxSold.Value = ChartOfAccountIDTaxSold;
                cmd.Parameters.Add(prmChartOfAccountIDTaxSold);

                base.ExecuteNonQuery(cmd);
            }

            catch (Exception ex)
            {
                throw base.ThrowException(ex);
            }
        }

        public void UpdateFinancialInformation(long ProductSubGroupID, int ChartOfAccountIDPurchase, int ChartOfAccountIDSold, int ChartOfAccountIDInventory, int ChartOfAccountIDTaxPurchase, int ChartOfAccountIDTaxSold)
        {
            try
            {
                string SQL = "UPDATE tblProductSubGroup SET " +
                                    "ChartOfAccountIDPurchase	= @ChartOfAccountIDPurchase, " +
                                    "ChartOfAccountIDSold		= @ChartOfAccountIDSold, " +
                                    "ChartOfAccountIDInventory	= @ChartOfAccountIDInventory, " +
                                    "ChartOfAccountIDTaxPurchase = @ChartOfAccountIDTaxPurchase, " +
                                    "ChartOfAccountIDTaxSold        = @ChartOfAccountIDTaxSold, " +
                                    "ChartOfAccountIDTransferIn	    = @ChartOfAccountIDPurchase, " +
                                    "ChartOfAccountIDTaxTransferIn  = @ChartOfAccountIDTaxPurchase, " +
                                    "ChartOfAccountIDTransferOut	= @ChartOfAccountIDSold, " +
                                    "ChartOfAccountIDTaxTransferOut = @ChartOfAccountIDTaxSold " +
                                "WHERE ProductSubGroupID	= @ProductSubGroupID; ";

                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandText = SQL;

                MySqlParameter prmChartOfAccountIDPurchase = new MySqlParameter("@ChartOfAccountIDPurchase",MySqlDbType.Int32);
                prmChartOfAccountIDPurchase.Value = ChartOfAccountIDPurchase;
                cmd.Parameters.Add(prmChartOfAccountIDPurchase);

                MySqlParameter prmChartOfAccountIDSold = new MySqlParameter("@ChartOfAccountIDSold",MySqlDbType.Int32);
                prmChartOfAccountIDSold.Value = ChartOfAccountIDSold;
                cmd.Parameters.Add(prmChartOfAccountIDSold);

                MySqlParameter prmChartOfAccountIDInventory = new MySqlParameter("@ChartOfAccountIDInventory",MySqlDbType.Int32);
                prmChartOfAccountIDInventory.Value = ChartOfAccountIDInventory;
                cmd.Parameters.Add(prmChartOfAccountIDInventory);

                MySqlParameter prmChartOfAccountIDTaxPurchase = new MySqlParameter("@ChartOfAccountIDTaxPurchase",MySqlDbType.Int32);
                prmChartOfAccountIDTaxPurchase.Value = ChartOfAccountIDTaxPurchase;
                cmd.Parameters.Add(prmChartOfAccountIDTaxPurchase);

                MySqlParameter prmChartOfAccountIDTaxSold = new MySqlParameter("@ChartOfAccountIDTaxSold",MySqlDbType.Int32);
                prmChartOfAccountIDTaxSold.Value = ChartOfAccountIDTaxSold;
                cmd.Parameters.Add(prmChartOfAccountIDTaxSold);

                MySqlParameter prmProductSubGroupID = new MySqlParameter("@ProductSubGroupID",MySqlDbType.Int64);
                prmProductSubGroupID.Value = ProductSubGroupID;
                cmd.Parameters.Add(prmProductSubGroupID);

                base.ExecuteNonQuery(cmd);
            }

            catch (Exception ex)
            {
                throw base.ThrowException(ex);
            }
        }

        public void UpdateFinancialInformationByGroup(long ProductGroupID, int ChartOfAccountIDPurchase, int ChartOfAccountIDSold, int ChartOfAccountIDInventory, int ChartOfAccountIDTaxPurchase, int ChartOfAccountIDTaxSold)
        {
            try
            {
                string SQL = "UPDATE tblProductSubGroup SET " +
                                    "ChartOfAccountIDPurchase	= @ChartOfAccountIDPurchase, " +
                                    "ChartOfAccountIDSold		= @ChartOfAccountIDSold, " +
                                    "ChartOfAccountIDInventory	= @ChartOfAccountIDInventory, " +
                                    "ChartOfAccountIDTaxPurchase = @ChartOfAccountIDTaxPurchase, " +
                                    "ChartOfAccountIDTaxSold    = @ChartOfAccountIDTaxSold " +
                                "WHERE ProductGroupID	= @ProductGroupID; ";

                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandText = SQL;

                MySqlParameter prmChartOfAccountIDPurchase = new MySqlParameter("@ChartOfAccountIDPurchase",MySqlDbType.Int32);
                prmChartOfAccountIDPurchase.Value = ChartOfAccountIDPurchase;
                cmd.Parameters.Add(prmChartOfAccountIDPurchase);

                MySqlParameter prmChartOfAccountIDSold = new MySqlParameter("@ChartOfAccountIDSold",MySqlDbType.Int32);
                prmChartOfAccountIDSold.Value = ChartOfAccountIDSold;
                cmd.Parameters.Add(prmChartOfAccountIDSold);

                MySqlParameter prmChartOfAccountIDInventory = new MySqlParameter("@ChartOfAccountIDInventory",MySqlDbType.Int32);
                prmChartOfAccountIDInventory.Value = ChartOfAccountIDInventory;
                cmd.Parameters.Add(prmChartOfAccountIDInventory);

                MySqlParameter prmChartOfAccountIDTaxPurchase = new MySqlParameter("@ChartOfAccountIDTaxPurchase",MySqlDbType.Int32);
                prmChartOfAccountIDTaxPurchase.Value = ChartOfAccountIDTaxPurchase;
                cmd.Parameters.Add(prmChartOfAccountIDTaxPurchase);

                MySqlParameter prmChartOfAccountIDTaxSold = new MySqlParameter("@ChartOfAccountIDTaxSold",MySqlDbType.Int32);
                prmChartOfAccountIDTaxSold.Value = ChartOfAccountIDTaxSold;
                cmd.Parameters.Add(prmChartOfAccountIDTaxSold);

                MySqlParameter prmProductGroupID = new MySqlParameter("@ProductGroupID",MySqlDbType.Int64);
                prmProductGroupID.Value = ProductGroupID;
                cmd.Parameters.Add(prmProductGroupID);

                base.ExecuteNonQuery(cmd);
            }

            catch (Exception ex)
            {
                
                
                {
                    
                    
                    
                    
                }

                throw base.ThrowException(ex);
            }
        }

        public Int32 Save(ProductSubGroupDetails Details)
        {
            try
            {
                string SQL = "CALL procSaveProductSubGroup(@ProductSubGroupID, @ProductGroupID, @ProductSubGroupCode, @ProductSubGroupName, @BaseUnitID," +
                                        "@Price, @PurchasePrice, @IncludeInSubtotalDiscount, @VAT, @EVAT," +
                                        "@LocalTax, @ChartOfAccountIDPurchase, @ChartOfAccountIDTaxPurchase," +
                                        "@ChartOfAccountIDSold, @ChartOfAccountIDTaxSold, @ChartOfAccountIDInventory," +
                                        "@SequenceNo, @ChartOfAccountIDTransferIn, @ChartOfAccountIDTaxTransferIn," +
                                        "@ChartOfAccountIDTransferOut, @ChartOfAccountIDTaxTransferOut," +
                                        "@ChartOfAccountIDInvAdjustment, @ChartOfAccountIDTaxInvAdjustment," +
                                        "@CreatedOn, @LastModified);";

                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandText = SQL;

                cmd.Parameters.AddWithValue("ProductSubGroupID", Details.ProductSubGroupID);
                cmd.Parameters.AddWithValue("ProductGroupID", Details.ProductGroupID);
                cmd.Parameters.AddWithValue("ProductSubGroupCode", Details.ProductSubGroupCode);
                cmd.Parameters.AddWithValue("ProductSubGroupName", Details.ProductSubGroupName);
                cmd.Parameters.AddWithValue("BaseUnitID", Details.BaseUnitID);
                cmd.Parameters.AddWithValue("Price", Details.Price);
                cmd.Parameters.AddWithValue("PurchasePrice", Details.PurchasePrice);
                cmd.Parameters.AddWithValue("IncludeInSubtotalDiscount", Details.IncludeInSubtotalDiscount);
                cmd.Parameters.AddWithValue("VAT", Details.VAT);
                cmd.Parameters.AddWithValue("EVAT", Details.EVAT);
                cmd.Parameters.AddWithValue("LocalTax", Details.LocalTax);
                cmd.Parameters.AddWithValue("ChartOfAccountIDPurchase", Details.ChartOfAccountIDPurchase);
                cmd.Parameters.AddWithValue("ChartOfAccountIDTaxPurchase", Details.ChartOfAccountIDTaxPurchase);
                cmd.Parameters.AddWithValue("ChartOfAccountIDSold", Details.ChartOfAccountIDSold);
                cmd.Parameters.AddWithValue("ChartOfAccountIDTaxSold", Details.ChartOfAccountIDTaxSold);
                cmd.Parameters.AddWithValue("ChartOfAccountIDInventory", Details.ChartOfAccountIDInventory);
                cmd.Parameters.AddWithValue("SequenceNo", Details.SequenceNo);
                cmd.Parameters.AddWithValue("ChartOfAccountIDTransferIn", Details.ProductSubGroupChartOfAccountDetails.ChartOfAccountIDTransferIn);
                cmd.Parameters.AddWithValue("ChartOfAccountIDTaxTransferIn", Details.ProductSubGroupChartOfAccountDetails.ChartOfAccountIDTaxTransferIn);
                cmd.Parameters.AddWithValue("ChartOfAccountIDTransferOut", Details.ProductSubGroupChartOfAccountDetails.ChartOfAccountIDTransferOut);
                cmd.Parameters.AddWithValue("ChartOfAccountIDTaxTransferOut", Details.ProductSubGroupChartOfAccountDetails.ChartOfAccountIDTaxTransferOut);
                cmd.Parameters.AddWithValue("ChartOfAccountIDInvAdjustment", Details.ProductSubGroupChartOfAccountDetails.ChartOfAccountIDInvAdjustment);
                cmd.Parameters.AddWithValue("ChartOfAccountIDTaxInvAdjustment", Details.ProductSubGroupChartOfAccountDetails.ChartOfAccountIDTaxInvAdjustment);
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
				

				MySqlCommand cmd;;
				string SQL;

				SQL=	"DELETE FROM tblProductSubGroupUnitMatrix WHERE SubGroupID IN (" + IDs + ");";
				cmd = new MySqlCommand(); 
				
				
				cmd.CommandType = System.Data.CommandType.Text;
				cmd.CommandText = SQL;
				base.ExecuteNonQuery(cmd);

				SQL=	"DELETE FROM tblProductSubGroupVariationsMatrix WHERE MatrixID IN (SELECT MatrixID FROM tblProductSubGroupBaseVariationsMatrix WHERE SubGroupID IN (" + IDs + "));";
				cmd = new MySqlCommand(); 
				
				
				cmd.CommandType = System.Data.CommandType.Text;
				cmd.CommandText = SQL;
				base.ExecuteNonQuery(cmd);

				SQL=	"DELETE FROM tblProductSubGroupBaseVariationsMatrix WHERE SubGroupID IN (" + IDs + ");";
				cmd = new MySqlCommand(); 
				
				
				cmd.CommandType = System.Data.CommandType.Text;
				cmd.CommandText = SQL;
				base.ExecuteNonQuery(cmd);

				SQL=	"DELETE FROM tblProductSubGroupVariations WHERE SubGroupID IN (" + IDs + ");";
				cmd = new MySqlCommand(); 
				
				
				cmd.CommandType = System.Data.CommandType.Text;
				cmd.CommandText = SQL;
				base.ExecuteNonQuery(cmd);

				SQL=	"DELETE FROM tblProductSubGroup WHERE ProductSubGroupID IN (" + IDs + ");";
				cmd = new MySqlCommand();
				
				
				cmd.CommandType = System.Data.CommandType.Text;
				cmd.CommandText = SQL;
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

        private string SQLSelect()
        {
            string stSQL = "SELECT " +
								"ProductSubGroupID, " +
                                "ProductGroupCode, " +
                                "ProductGroupName, " +
								"a.ProductGroupID, " +
								"ProductSubGroupCode, " +
								"ProductSubGroupName, " +
								"a.BaseUnitID, " +
								"UnitName 'BaseUnitName', " +
								"a.Price, " +
								"a.PurchasePrice, " +
								"a.IncludeInSubtotalDiscount, " +
								"a.VAT, " +
								"a.EVAT, " +
								"a.LocalTax, " +
                                "a.ChartOfAccountIDPurchase, " +
                                "a.ChartOfAccountIDSold, " +
                                "a.ChartOfAccountIDInventory, " +
                                "a.ChartOfAccountIDTaxPurchase, " +
                                "a.ChartOfAccountIDTaxSold " +
							"FROM tblProductSubGroup a " +
							"INNER JOIN tblProductGroup b ON a.ProductGroupID = b.ProductGroupID " +
							"INNER JOIN tblUnit c ON a.BaseUnitID = c.UnitID ";
                                
            return stSQL;
        }

        private string SQLSelect(ProductSubGroupColumns clsProductSubGroupColumns)
        {
            string stSQL = "SELECT ";

            if (clsProductSubGroupColumns.ProductGroupID) stSQL += "tblProductGroup.ProductGroupID, ";
            if (clsProductSubGroupColumns.ProductGroupCode) stSQL += "tblProductGroup.ProductGroupCode, ";
            if (clsProductSubGroupColumns.ProductGroupName) stSQL += "tblProductGroup.ProductGroupName, ";
            if (clsProductSubGroupColumns.ProductSubGroupCode) stSQL += "tblProductSubGroup.ProductSubGroupCode, ";
            if (clsProductSubGroupColumns.ProductSubGroupName) stSQL += "tblProductSubGroup.ProductSubGroupName, ";
            if (clsProductSubGroupColumns.BaseUnitID) stSQL += "tblProductSubGroup.BaseUnitID, ";
            if (clsProductSubGroupColumns.BaseUnitName) stSQL += "tblUnit.UnitName 'BaseUnitName', ";
            if (clsProductSubGroupColumns.Price) stSQL += "tblProductSubGroup.Price, ";
            if (clsProductSubGroupColumns.PurchasePrice) stSQL += "tblProductSubGroup.PurchasePrice, ";
            if (clsProductSubGroupColumns.IncludeInSubtotalDiscount) stSQL += "tblProductSubGroup.IncludeInSubtotalDiscount, ";
            if (clsProductSubGroupColumns.VAT) stSQL += "tblProductSubGroup.VAT, ";
            if (clsProductSubGroupColumns.EVAT) stSQL += "tblProductSubGroup.EVAT, ";
            if (clsProductSubGroupColumns.LocalTax) stSQL += "tblProductSubGroup.LocalTax, ";
            if (clsProductSubGroupColumns.ChartOfAccountIDPurchase) stSQL += "tblProductSubGroup.ChartOfAccountIDPurchase, ";
            if (clsProductSubGroupColumns.ChartOfAccountIDSold) stSQL += "tblProductSubGroup.ChartOfAccountIDSold, ";
            if (clsProductSubGroupColumns.ChartOfAccountIDInventory) stSQL += "tblProductSubGroup.ChartOfAccountIDInventory, ";
            if (clsProductSubGroupColumns.ChartOfAccountIDTaxPurchase) stSQL += "tblProductSubGroup.ChartOfAccountIDTaxPurchase, ";
            if (clsProductSubGroupColumns.ChartOfAccountIDTaxSold) stSQL += "tblProductSubGroup.ChartOfAccountIDTaxSold, ";
            if (clsProductSubGroupColumns.SequenceNo) stSQL += "tblProductSubGroup.SequenceNo, ";

            stSQL += "tblProductSubGroup.ProductSubGroupID ";
            stSQL += "FROM tblProductSubGroup ";

            if (clsProductSubGroupColumns.ProductGroupCode || clsProductSubGroupColumns.ProductGroupName)
                stSQL += "INNER JOIN tblProductGroup ON tblProductSubGroup.ProductGroupID = tblProductGroup.ProductGroupID ";

            if (clsProductSubGroupColumns.BaseUnitName)
                stSQL += "INNER JOIN tblUnit ON tblProductSubGroup.BaseUnitID = tblUnit.UnitID ";

            return stSQL;
        }

		#region Details

		public ProductSubGroupDetails Details(Int64 ProductSubGroupID)
		{
			try
			{
				string SQL = SQLSelect() + "WHERE ProductSubGroupID = @ProductSubGroupID;";

				
	 			
				MySqlCommand cmd = new MySqlCommand();
				
				
				cmd.CommandType = System.Data.CommandType.Text;
				cmd.CommandText = SQL;

				MySqlParameter prmProductSubGroupID = new MySqlParameter("@ProductSubGroupID",MySqlDbType.Int16);
				prmProductSubGroupID.Value = ProductSubGroupID;
				cmd.Parameters.Add(prmProductSubGroupID);

                System.Data.DataTable dt = new System.Data.DataTable("tblProductSubGroup");
                base.MySqlDataAdapterFill(cmd, dt);
                
				
				ProductSubGroupDetails Details = new ProductSubGroupDetails();

				foreach(System.Data.DataRow dr in dt.Rows)
				{
					Details.ProductSubGroupID = Int64.Parse(dr["ProductSubGroupID"].ToString());
					Details.ProductGroupID = Int64.Parse(dr["ProductGroupID"].ToString());
					Details.ProductSubGroupCode = "" + dr["ProductSubGroupCode"].ToString();
					Details.ProductSubGroupName = "" + dr["ProductSubGroupName"].ToString();
					Details.BaseUnitID = Int32.Parse(dr["BaseUnitID"].ToString());
					Details.BaseUnitName = "" + dr["BaseUnitName"].ToString();
					Details.Price = decimal.Parse(dr["Price"].ToString());
					Details.PurchasePrice = decimal.Parse(dr["PurchasePrice"].ToString());
					Details.IncludeInSubtotalDiscount = bool.Parse(dr["IncludeInSubtotalDiscount"].ToString());
					Details.VAT = decimal.Parse(dr["VAT"].ToString());
					Details.EVAT = decimal.Parse(dr["EVAT"].ToString());
					Details.LocalTax = decimal.Parse(dr["LocalTax"].ToString());
                    /*** Added for Financial Information  ***/
                    /*** March 07, 2009 ***/
                    Details.ChartOfAccountIDPurchase = Int32.Parse(dr["ChartOfAccountIDPurchase"].ToString());
                    Details.ChartOfAccountIDSold = Int32.Parse(dr["ChartOfAccountIDSold"].ToString());
                    Details.ChartOfAccountIDInventory = Int32.Parse(dr["ChartOfAccountIDInventory"].ToString());
                    Details.ChartOfAccountIDTaxPurchase = Int32.Parse(dr["ChartOfAccountIDTaxPurchase"].ToString());
                    Details.ChartOfAccountIDTaxSold = Int32.Parse(dr["ChartOfAccountIDTaxSold"].ToString());
				}

				return Details;
			}

			catch (Exception ex)
			{
				
				
				{
					
					
					
					
				}

				throw base.ThrowException(ex);
			}	
		}

		public ProductSubGroupDetails Details(string ProductSubGroupCode)
		{
			try
			{
				string SQL = SQLSelect() + "WHERE ProductSubGroupCode = @ProductSubGroupCode;";

				
	 			
				MySqlCommand cmd = new MySqlCommand();
				
				
				cmd.CommandType = System.Data.CommandType.Text;
				cmd.CommandText = SQL;

				MySqlParameter prmProductSubGroupCode = new MySqlParameter("@ProductSubGroupCode",MySqlDbType.String);
				prmProductSubGroupCode.Value = ProductSubGroupCode;
				cmd.Parameters.Add(prmProductSubGroupCode);

                System.Data.DataTable dt = new System.Data.DataTable("tblProductSubGroup");
                base.MySqlDataAdapterFill(cmd, dt);
                
				
				ProductSubGroupDetails Details = new ProductSubGroupDetails();

                foreach (System.Data.DataRow dr in dt.Rows)
                {
                    Details.ProductSubGroupID = Int64.Parse(dr["ProductSubGroupID"].ToString());
                    Details.ProductGroupID = Int64.Parse(dr["ProductGroupID"].ToString());
                    Details.ProductSubGroupCode = "" + dr["ProductSubGroupCode"].ToString();
                    Details.ProductSubGroupName = "" + dr["ProductSubGroupName"].ToString();
                    Details.BaseUnitID = Int32.Parse(dr["BaseUnitID"].ToString());
                    Details.BaseUnitName = "" + dr["BaseUnitName"].ToString();
                    Details.Price = decimal.Parse(dr["Price"].ToString());
                    Details.PurchasePrice = decimal.Parse(dr["PurchasePrice"].ToString());
                    Details.IncludeInSubtotalDiscount = bool.Parse(dr["IncludeInSubtotalDiscount"].ToString());
                    Details.VAT = decimal.Parse(dr["VAT"].ToString());
                    Details.EVAT = decimal.Parse(dr["EVAT"].ToString());
                    Details.LocalTax = decimal.Parse(dr["LocalTax"].ToString());
                    /*** Added for Financial Information  ***/
                    /*** March 07, 2009 ***/
                    Details.ChartOfAccountIDPurchase = Int32.Parse(dr["ChartOfAccountIDPurchase"].ToString());
                    Details.ChartOfAccountIDSold = Int32.Parse(dr["ChartOfAccountIDSold"].ToString());
                    Details.ChartOfAccountIDInventory = Int32.Parse(dr["ChartOfAccountIDInventory"].ToString());
                    Details.ChartOfAccountIDTaxPurchase = Int32.Parse(dr["ChartOfAccountIDTaxPurchase"].ToString());
                    Details.ChartOfAccountIDTaxSold = Int32.Parse(dr["ChartOfAccountIDTaxSold"].ToString());
                }

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

		#region Streams


        public System.Data.DataTable ListAsDataTable(ProductSubGroupColumns clsProductSubGroupColumns, ProductSubGroupDetails clsSearchKeys, long SequenceNoStart=0, System.Data.SqlClient.SortOrder SequenceSortOrder = System.Data.SqlClient.SortOrder.Ascending, int Limit = 100, string SortField = ProductSubGroupColumnNames.ProductSubGroupName, System.Data.SqlClient.SortOrder SortOrder = System.Data.SqlClient.SortOrder.Ascending)
        {
            try
            {
                MySqlCommand cmd = new MySqlCommand();

                string SQL = SQLSelect(clsProductSubGroupColumns) + "WHERE 1=1 ";

                if (SequenceNoStart != 0)
                {
                    if (SequenceSortOrder == System.Data.SqlClient.SortOrder.Descending)
                        SQL += "AND SequenceNo < " + SequenceNoStart.ToString() + " ";
                    else
                        SQL += "AND SequenceNo > " + SequenceNoStart.ToString() + " ";
                }

                if (clsSearchKeys.ProductGroupID != 0)
                {
                    SQL += "AND tblProductSubGroup.ProductGroupID = @ProductGroupID ";
                    MySqlParameter prmProductGroupID = new MySqlParameter("@ProductGroupID",MySqlDbType.Int64);
                    prmProductGroupID.Value = clsSearchKeys.ProductGroupID;
                    cmd.Parameters.Add(prmProductGroupID);
                }
                if (clsSearchKeys.ProductSubGroupID != 0)
                {
                    SQL += "AND tblProductSubGroup.ProductSubGroupID = @ProductSubGroupID ";
                    MySqlParameter prmProductSubGroupID = new MySqlParameter("@ProductSubGroupID",MySqlDbType.Int64);
                    prmProductSubGroupID.Value = clsSearchKeys.ProductSubGroupID;
                    cmd.Parameters.Add(prmProductSubGroupID);
                }
                if (clsSearchKeys.ProductSubGroupCode != string.Empty && clsSearchKeys.ProductSubGroupCode != null)
                {
                    SQL += "AND tblProductSubGroup.ProductSubGroupCode LIKE @ProductSubGroupCode ";
                    MySqlParameter prmProductSubGroupCode = new MySqlParameter("@ProductSubGroupCode",MySqlDbType.String);
                    prmProductSubGroupCode.Value = clsSearchKeys.ProductSubGroupCode + "%";
                    cmd.Parameters.Add(prmProductSubGroupCode);
                }
                if (clsSearchKeys.ProductSubGroupName != string.Empty && clsSearchKeys.ProductSubGroupName != null)
                {
                    SQL += "AND tblProductSubGroup.ProductSubGroupName LIKE @ProductSubGroupName ";
                    MySqlParameter prmProductSubGroupName = new MySqlParameter("@ProductSubGroupName",MySqlDbType.String);
                    prmProductSubGroupName.Value = clsSearchKeys.ProductSubGroupName + "%";
                    cmd.Parameters.Add(prmProductSubGroupName);
                }

                if (SortField != string.Empty && SortField != null)
                {
                    SQL += "ORDER BY " + SortField + " ";

                    if (SortOrder != System.Data.SqlClient.SortOrder.Descending) SQL += "ASC ";
                    else SQL += "DESC ";
                }

                if (Limit != 0)
                    SQL += "LIMIT " + Limit + " ";

                
                
                
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandText = SQL;

                System.Data.DataTable dt = new System.Data.DataTable("tblProductSubGroups");
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
        public System.Data.DataTable ListAsDataTable(ProductSubGroupColumns clsProductSubGroupColumns, ProductSubGroupColumns SearchColumns, string SearchKey, long SequenceNoStart, System.Data.SqlClient.SortOrder SequenceSortOrder, int Limit, string SortField, System.Data.SqlClient.SortOrder SortOrder)
        {
            try
            {
                string SQL = SQLSelect(clsProductSubGroupColumns) + "WHERE 1=1 ";
                
                if (SequenceNoStart != 0)
                {
                    if (SequenceSortOrder == System.Data.SqlClient.SortOrder.Descending)
                        SQL += "AND SequenceNo < " + SequenceNoStart.ToString() + " ";
                    else
                        SQL += "AND SequenceNo > " + SequenceNoStart.ToString() + " ";
                }

                if (SearchColumns.ProductGroupID)
                    SQL += "AND tblProductSubGroup.ProductGroupID = " + SearchKey + " ";

                if (SearchColumns.ProductSubGroupID)
                    SQL += "AND tblProductSubGroup.ProductSubGroupID = " + SearchKey + " ";

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

                System.Data.DataTable dt = new System.Data.DataTable("tblProductSubGroups");
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
        //public System.Data.DataTable ListAsDataTable(string SortField, SortOption SortOrder)
        //{
        //    try
        //    {
        //        string SQL = SQLSelect() + "WHERE 1=1 ORDER BY " + SortField;


        //        if (SortOrder == SortOption.Ascending)
        //            SQL += " ASC";
        //        else
        //            SQL += " DESC";

        //        

        //        MySqlCommand cmd = new MySqlCommand();
        //        
        //        
        //        cmd.CommandType = System.Data.CommandType.Text;
        //        cmd.CommandText = SQL;

        //        System.Data.DataTable dt = new System.Data.DataTable("tblProductSubGroups");
        //        base.MySqlDataAdapterFill(cmd, dt);
        //        

        //        return dt;
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
        //public System.Data.DataTable ListAsDataTable(long ProductGroupID, string SortField, SortOption SortOrder)
        //{
        //    try
        //    {
        //        string SQL = SQLSelect() + "WHERE 1=1 AND a.ProductGroupID = @ProductGroupID ORDER BY " + SortField;


        //        if (SortOrder == SortOption.Ascending)
        //            SQL += " ASC";
        //        else
        //            SQL += " DESC";

        //        

        //        MySqlCommand cmd = new MySqlCommand();
        //        
        //        
        //        cmd.CommandType = System.Data.CommandType.Text;
        //        cmd.CommandText = SQL;

        //        cmd.Parameters.AddWithValue("@ProductGroupID", ProductGroupID);

        //        System.Data.DataTable dt = new System.Data.DataTable("tblProductSubGroups");
        //        base.MySqlDataAdapterFill(cmd, dt);
        //        

        //        return dt;
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
        //public System.Data.DataTable ListByNameAsDataTable(string SortField, SortOption SortOrder)
        //{
        //    try
        //    {
        //        string SQL = "SELECT DISTINCT " +
        //                        "ProductSubGroupName " +
        //                    "FROM tblProductSubGroup " +
        //                    "ORDER BY " + SortField;

        //        if (SortOrder == SortOption.Ascending)
        //            SQL += " ASC";
        //        else
        //            SQL += " DESC";

        //        

        //        MySqlCommand cmd = new MySqlCommand();
        //        
        //        
        //        cmd.CommandType = System.Data.CommandType.Text;
        //        cmd.CommandText = SQL;

        //        System.Data.DataTable dt = new System.Data.DataTable("tblProductSubGroups");
        //        base.MySqlDataAdapterFill(cmd, dt);
        //        

        //        return dt;
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
        //public System.Data.DataTable ListByNameAsDataTable(string GroupName, string SortField, SortOption SortOrder)
        //{
        //    try
        //    {
        //        string SQL = "SELECT DISTINCT " +
        //                        "ProductSubGroupName " +
        //                    "FROM tblProductSubGroup a INNER JOIN tblProductGroup b ON a.ProductGroupID = b.ProductGroupID " +
        //                    "WHERE ProductGroupName LIKE @GroupName ORDER BY " + SortField;

        //        if (SortOrder == SortOption.Ascending)
        //            SQL += " ASC";
        //        else
        //            SQL += " DESC";

        //        

        //        MySqlCommand cmd = new MySqlCommand();
        //        
        //        
        //        cmd.CommandType = System.Data.CommandType.Text;
        //        cmd.CommandText = SQL;

        //        cmd.Parameters.AddWithValue("@GroupName", "%" + GroupName + "%");

        //        System.Data.DataTable dt = new System.Data.DataTable("tblProductSubGroups");
        //        base.MySqlDataAdapterFill(cmd, dt);
        //        

        //        return dt;
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
        //public System.Data.DataTable SearchDataTable(long ProductGroupID, string SearchKey, string SortField, SortOption SortOrder)
        //{
        //    try
        //    {
        //        string SQL = SQLSelect() + "WHERE 1=1 " +
        //                                        "AND (ProductSubGroupName LIKE @SearchKey" +
        //                                        "OR ProductSubGroupCode LIKE @SearchKey) ";

        //        if (ProductGroupID != Constants.ZERO)
        //            SQL += "AND a.ProductGroupID = " + ProductGroupID + " ";

        //        SQL += "ORDER BY " + SortField;
        //        if (SortOrder == SortOption.Ascending)
        //            SQL += " ASC ";
        //        else
        //            SQL += " DESC ";

        //        

        //        MySqlCommand cmd = new MySqlCommand();
        //        
        //        
        //        cmd.CommandType = System.Data.CommandType.Text;
        //        cmd.CommandText = SQL;
        //        cmd.Parameters.AddWithValue("@SearchKey", "%" + SearchKey + "%");

        //        System.Data.DataTable dt = new System.Data.DataTable("tblProductSubGroups");
        //        base.MySqlDataAdapterFill(cmd, dt);
        //        

        //        return dt;
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

        //public MySqlDataReader List(string SortField, SortOption SortOrder)
        //{
        //    try
        //    {
        //        string SQL = "SELECT " +
        //                        "ProductSubGroupID, " +
        //                        "a.ProductGroupID, " +
        //                        "b.ProductGroupCode, " +
        //                        "ProductSubGroupCode, " +
        //                        "ProductSubGroupName, " +
        //                        "a.BaseUnitID, " +
        //                        "UnitName 'BaseUnitName', " +
        //                        "a.Price, " +
        //                        "a.PurchasePrice, " +
        //                        "a.IncludeInSubtotalDiscount, " +
        //                        "a.VAT, " +
        //                        "a.EVAT, " +
        //                        "a.LocalTax " +
        //                    "FROM tblProductSubGroup a INNER JOIN " +
        //                    "tblProductGroup b ON a.ProductGroupID = b.ProductGroupID INNER JOIN " +
        //                    "tblUnit c ON a.BaseUnitID = c.UnitID " +
        //                    "WHERE 1=1 ORDER BY " + SortField;

        //        if (SortOrder == SortOption.Ascending)
        //            SQL += " ASC";
        //        else
        //            SQL += " DESC";

        //        

        //        MySqlCommand cmd = new MySqlCommand();
        //        
        //        
        //        cmd.CommandType = System.Data.CommandType.Text;
        //        cmd.CommandText = SQL;

        //        

        //        return base.ExecuteReader(cmd);
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
        //public MySqlDataReader List(Int64 GroupID, string SortField, SortOption SortOrder)
        //{
        //    try
        //    {
        //        string SQL = "SELECT " +
        //                        "ProductSubGroupID, " +
        //                        "a.ProductGroupID, " +
        //                        "b.ProductGroupCode, " +
        //                        "ProductSubGroupCode, " +
        //                        "ProductSubGroupName, " +
        //                        "a.BaseUnitID, " +
        //                        "UnitName 'BaseUnitName', " +
        //                        "a.Price, " +
        //                        "a.PurchasePrice, " +
        //                        "a.IncludeInSubtotalDiscount, " +
        //                        "a.VAT, " +
        //                        "a.EVAT, " +
        //                        "a.LocalTax " +
        //                    "FROM tblProductSubGroup a INNER JOIN " +
        //                    "tblProductGroup b ON a.ProductGroupID = b.ProductGroupID INNER JOIN " +
        //                    "tblUnit c ON a.BaseUnitID = c.UnitID " +
        //                    "WHERE 1=1 AND a.ProductGroupID = " + GroupID + " ORDER BY " + SortField;

        //        if (SortOrder == SortOption.Ascending)
        //            SQL += " ASC";
        //        else
        //            SQL += " DESC";

        //        

        //        MySqlCommand cmd = new MySqlCommand();
        //        
        //        
        //        cmd.CommandType = System.Data.CommandType.Text;
        //        cmd.CommandText = SQL;

        //        

        //        return base.ExecuteReader(cmd);
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
        //public MySqlDataReader ListByName(string SortField, SortOption SortOrder)
        //{
        //    try
        //    {
        //        string SQL = "SELECT DISTINCT " +
        //                        "ProductSubGroupName " +
        //                    "FROM tblProductSubGroup " +
        //                    "WHERE 1=1 ORDER BY " + SortField;

        //        if (SortOrder == SortOption.Ascending)
        //            SQL += " ASC";
        //        else
        //            SQL += " DESC";

        //        

        //        MySqlCommand cmd = new MySqlCommand();
        //        
        //        
        //        cmd.CommandType = System.Data.CommandType.Text;
        //        cmd.CommandText = SQL;
				
        //        
				
        //        return base.ExecuteReader(cmd);			
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
        //public MySqlDataReader Search(string SearchKey, string SortField, SortOption SortOrder)
        //{
        //    try
        //    {
        //        string SQL = "SELECT " +
        //                        "ProductSubGroupID, " +
        //                        "a.ProductGroupID, " +
        //                        "b.ProductGroupCode, " +
        //                        "a.ProductSubGroupCode, " +
        //                        "a.ProductSubGroupName, " +
        //                        "a.BaseUnitID, " +
        //                        "c.UnitName 'BaseUnitName', " +
        //                        "a.Price, " +
        //                        "a.PurchasePrice, " +
        //                        "a.IncludeInSubtotalDiscount, " +
        //                        "a.VAT, " +
        //                        "a.EVAT, " +
        //                        "a.LocalTax " +
        //                    "FROM tblProductSubGroup a INNER JOIN " +
        //                    "tblProductGroup b ON a.ProductGroupID = b.ProductGroupID INNER JOIN " +
        //                    "tblUnit c ON a.BaseUnitID = c.UnitID " +
        //                    "WHERE ProductSubGroupName LIKE @SearchKey" +
        //                    "OR ProductSubGroupCode LIKE @SearchKey " +
        //                    "ORDER BY " + SortField;

        //        if (SortOrder == SortOption.Ascending)
        //            SQL += " ASC";
        //        else
        //            SQL += " DESC";

        //        

        //        MySqlCommand cmd = new MySqlCommand();
        //        
        //        
        //        cmd.CommandType = System.Data.CommandType.Text;
        //        cmd.CommandText = SQL;
				
        //        MySqlParameter prmSearchKey = new MySqlParameter("@SearchKey",MySqlDbType.String);
        //        prmSearchKey.Value = "%" + SearchKey + "%";
        //        cmd.Parameters.Add(prmSearchKey);

        //        
				
        //        return base.ExecuteReader(cmd);			
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

		#endregion

		#region Inheritance

		public void InheritGroupVariations(Int64 ProductGroupID, Int64 ProductSubGroupID)
		{
			try 
			{
				string SQL	= "INSERT INTO tblProductSubGroupVariations (SubGroupID, VariationID) " + 
					"SELECT @SubGroupID, VariationID FROM tblProductGroupVariations " + 
					"WHERE GroupID = @GroupID;";

				
	 			
				MySqlCommand cmd = new MySqlCommand();
				
				
				cmd.CommandType = System.Data.CommandType.Text;
				cmd.CommandText = SQL;

				MySqlParameter prmSubGroupID = new MySqlParameter("@SubGroupID",MySqlDbType.Int64);			
				prmSubGroupID.Value = ProductSubGroupID;
				cmd.Parameters.Add(prmSubGroupID);

				MySqlParameter prmGroupID = new MySqlParameter("@GroupID",MySqlDbType.Int64);			
				prmGroupID.Value = ProductGroupID;
				cmd.Parameters.Add(prmGroupID);

				base.ExecuteNonQuery(cmd);
			}

			catch (Exception ex)
			{
				
				
				{
					
					
					
					
				}

				throw base.ThrowException(ex);
			}	
		}
		public void InheritGroupVariationsMatrix(Int64 ProductGroupID, Int64 ProductSubGroupID)
		{
			try 
			{	
				

				ProductSubGroupBaseVariationsMatrixDetails clsSubGroupBaseDetails;
				ProductGroupVariationsMatrix clsProductGroupVariationsMatrix = new ProductGroupVariationsMatrix(base.Connection, base.Transaction);

				ProductSubGroupVariationsMatrix clsProductSubGroupVariationsMatrix = new ProductSubGroupVariationsMatrix(base.Connection, base.Transaction);
				ProductSubGroupVariationsMatrixDetails  clsProductSubGroupVariationsMatrixDetails = new ProductSubGroupVariationsMatrixDetails();

				MySqlDataReader clsProductGroupVariationsMatrixList;
				MySqlDataReader clsProductGroupBaseVariationsMatrixList = clsProductGroupVariationsMatrix.BaseList(ProductGroupID,"MatrixID",SortOption.Ascending);

				Int64 GroupBaseMatrixID = 0;
				while (clsProductGroupBaseVariationsMatrixList.Read())
				{
					clsSubGroupBaseDetails = new ProductSubGroupBaseVariationsMatrixDetails();

					clsSubGroupBaseDetails.SubGroupID = ProductSubGroupID;
					clsSubGroupBaseDetails.Description = "" + clsProductGroupBaseVariationsMatrixList["Description"].ToString();
					clsSubGroupBaseDetails.UnitID = Convert.ToInt32(clsProductGroupBaseVariationsMatrixList["UnitID"]);
					clsSubGroupBaseDetails.Price =  Convert.ToDecimal(clsProductGroupBaseVariationsMatrixList["Price"]);
					clsSubGroupBaseDetails.PurchasePrice =  Convert.ToDecimal(clsProductGroupBaseVariationsMatrixList["PurchasePrice"]);
					clsSubGroupBaseDetails.IncludeInSubtotalDiscount =  Convert.ToBoolean(clsProductGroupBaseVariationsMatrixList["IncludeInSubtotalDiscount"]);
					clsSubGroupBaseDetails.VAT =  Convert.ToDecimal(clsProductGroupBaseVariationsMatrixList["VAT"]);
					clsSubGroupBaseDetails.EVAT =  Convert.ToDecimal(clsProductGroupBaseVariationsMatrixList["EVAT"]);
					clsSubGroupBaseDetails.LocalTax =  Convert.ToDecimal(clsProductGroupBaseVariationsMatrixList["LocalTax"]);

					clsSubGroupBaseDetails.MatrixID = clsProductSubGroupVariationsMatrix.InsertBaseVariation(clsSubGroupBaseDetails);

					GroupBaseMatrixID = clsProductGroupBaseVariationsMatrixList.GetInt64(0);
					clsProductGroupVariationsMatrix = new ProductGroupVariationsMatrix(base.Connection, base.Transaction);
					clsProductGroupVariationsMatrixList =  clsProductGroupVariationsMatrix.List(GroupBaseMatrixID);

					while (clsProductGroupVariationsMatrixList.Read())
					{
						clsProductSubGroupVariationsMatrixDetails = new ProductSubGroupVariationsMatrixDetails();
						clsProductSubGroupVariationsMatrixDetails.MatrixID = clsSubGroupBaseDetails.MatrixID;
						clsProductSubGroupVariationsMatrixDetails.SubGroupID = ProductSubGroupID;
						clsProductSubGroupVariationsMatrixDetails.VariationID = Convert.ToInt32(clsProductGroupVariationsMatrixList["VariationID"]);
						clsProductSubGroupVariationsMatrixDetails.Description = "" + clsProductGroupVariationsMatrixList["Description"].ToString();
						clsProductSubGroupVariationsMatrix.InsertVariation(clsProductSubGroupVariationsMatrixDetails);
					}
					clsProductGroupVariationsMatrixList.Close(); 

				}
				clsProductGroupBaseVariationsMatrixList.Close();

			}
				
			catch (Exception ex)
			{
				
				
				{
					
					
					
					
				}

				throw base.ThrowException(ex);
			}	
		}
		public void InheritGroupUnitMatrix(Int64 ProductGroupID, Int64 ProductSubGroupID)
		{
			try 
			{	
				

				ProductGroupUnitsMatrix clsProductGroupUnitsMatrix = new ProductGroupUnitsMatrix(base.Connection, base.Transaction);

				ProductSubGroupUnitsMatrix clsUnitMatrix = new ProductSubGroupUnitsMatrix(base.Connection, base.Transaction);
				ProductSubGroupUnitsMatrixDetails clsProductSubGroupUnitsMatrixDetails = new ProductSubGroupUnitsMatrixDetails();

				MySqlDataReader clsProductGroupUnitsMatrixList = clsProductGroupUnitsMatrix.List(ProductGroupID,"MatrixID",SortOption.Ascending);
				
				while (clsProductGroupUnitsMatrixList.Read())
				{
					clsProductSubGroupUnitsMatrixDetails.SubGroupID = Convert.ToInt64(ProductSubGroupID);
					clsProductSubGroupUnitsMatrixDetails.BaseUnitID = Convert.ToInt32(clsProductGroupUnitsMatrixList["BaseUnitID"]);
					clsProductSubGroupUnitsMatrixDetails.BaseUnitValue = Convert.ToDecimal(clsProductGroupUnitsMatrixList["BaseUnitValue"]);
					clsProductSubGroupUnitsMatrixDetails.BottomUnitID = Convert.ToInt32(clsProductGroupUnitsMatrixList["BottomUnitID"]);
					clsProductSubGroupUnitsMatrixDetails.BottomUnitValue = Convert.ToDecimal(clsProductGroupUnitsMatrixList["BottomUnitValue"]);
					clsUnitMatrix.Insert(clsProductSubGroupUnitsMatrixDetails);

				}
				clsProductGroupUnitsMatrixList.Close();
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


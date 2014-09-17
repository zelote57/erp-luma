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
	public struct ProductGroupDetails
	{
		public Int64 ProductGroupID;
		public string ProductGroupCode;
		public string ProductGroupName;
        public UnitDetails UnitDetails;
		public decimal Price;
		public decimal PurchasePrice;
		public bool IncludeInSubtotalDiscount;
		public decimal VAT;
		public decimal EVAT;
		public decimal LocalTax;
        public OrderSlipPrinter OrderSlipPrinter;
        public Int32 ChartOfAccountIDPurchase;
        public Int32 ChartOfAccountIDSold;
        public Int32 ChartOfAccountIDInventory;
        public Int32 ChartOfAccountIDTaxPurchase;
        public Int32 ChartOfAccountIDTaxSold;
        public Int32 SequenceNo;
        public bool isLock;

        public ProductGroupChartOfAccountDetails ProductGroupChartOfAccountDetails;

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
    public struct ProductGroupChartOfAccountDetails
    {
        public Int32 ChartOfAccountIDTransferIn;
        public Int32 ChartOfAccountIDTaxTransferIn;
        public Int32 ChartOfAccountIDTransferOut;
        public Int32 ChartOfAccountIDTaxTransferOut;
        public Int32 ChartOfAccountIDInvAdjustment;
        public Int32 ChartOfAccountIDTaxInvAdjustment;
    }

	[StrongNameIdentityPermissionAttribute(SecurityAction.LinkDemand,
		 PublicKey = "002400000480000094000000060200000024000" +
		 "052534131000400000100010053D785642F9F960B43157E0380" +
		 "F393BEE53E8DFAFBF441366C1B6F8B48D9DDF0D527B1F3B21EA" +
		 "E85D2FDB664CE81EB8A87DBE4C589D6F4202FE2B7D4B978BB69" +
		 "684874612CB9B8DB7A0339400A9C4E68277884B07817363D242" +
		 "E3696F9FACDBEA831810AE6DC9EDCA91A7B5DA12FE7BF65D113" +
		 "FF52834EAFB5A7A1FDFD5851A3")]
	public class ProductGroup : POSConnection
    {
        public const long DEFAULT_GROUP_ID = 1;

		#region Constructors and Destructors

		public ProductGroup()
            : base(null, null)
        {
        }

        public ProductGroup(MySqlConnection Connection, MySqlTransaction Transaction) 
            : base(Connection, Transaction)
		{

		}

		#endregion
		
		#region Insert and Update

		public Int64 Insert(ProductGroupDetails Details)
		{
			try 
			{
                Save(Details);

                return Int64.Parse(base.getLAST_INSERT_ID(this));
			}
			catch (Exception ex)
			{
				throw base.ThrowException(ex);
			}	
		}
		public void Update(ProductGroupDetails Details)
		{
			try 
			{
                Save(Details);
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
        //        string SQL =	"UPDATE tblProductGroup SET " +
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

        //        ProductGroupVariationsMatrix clsProductGroupVariationsMatrix = new ProductGroupVariationsMatrix(base.Connection, base.Transaction);
        //        clsProductGroupVariationsMatrix.ChangeVAT(OldVAT, NewVAT);
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
        //        string SQL =	"UPDATE tblProductGroup SET " +
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

        //        ProductGroupVariationsMatrix clsProductGroupVariationsMatrix = new ProductGroupVariationsMatrix(base.Connection, base.Transaction);
        //        clsProductGroupVariationsMatrix.ChangeEVAT(OldEVAT, NewEVAT);

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
        //        string SQL =	"UPDATE tblProductGroup SET " +
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

        //        ProductGroupVariationsMatrix clsProductGroupVariationsMatrix = new ProductGroupVariationsMatrix(base.Connection, base.Transaction);
        //        clsProductGroupVariationsMatrix.ChangeLocalTax(OldLocalTax, NewLocalTax);
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

        //public void ChangeTax(long ProductGroupID, decimal NewVAT, decimal NewEVAT, decimal NewLocalTax)
        //{
        //    try
        //    {
                
        //        ProductGroupVariationsMatrix clsProductGroupVariationsMatrix = new ProductGroupVariationsMatrix(base.Connection, base.Transaction);
        //        clsProductGroupVariationsMatrix.ChangeTax(ProductGroupID, NewVAT, NewEVAT, NewLocalTax);

        //        ProductSubGroup clsProductSubGroup = new ProductSubGroup(base.Connection, base.Transaction);
        //        clsProductSubGroup.ChangeTax(ProductGroupID, 0, NewVAT, NewEVAT, NewLocalTax);
        //    }

        //    catch (Exception ex)
        //    {
                
                
        //        {
                    
                    
                    
                    
        //        }

        //        throw base.ThrowException(ex);
        //    }
        //}

        public void UpdateFinancialInformation(Int32 ChartOfAccountIDPurchase, Int32 ChartOfAccountIDSold, Int32 ChartOfAccountIDInventory, Int32 ChartOfAccountIDTaxPurchase, Int32 ChartOfAccountIDTaxSold)
        {
            try
            {
                string SQL = "UPDATE tblProductGroup SET " +
                                    "ChartOfAccountIDPurchase	    = @ChartOfAccountIDPurchase, " +
                                    "ChartOfAccountIDSold		    = @ChartOfAccountIDSold, " +
                                    "ChartOfAccountIDInventory	    = @ChartOfAccountIDInventory, " +
                                    "ChartOfAccountIDTaxPurchase    = @ChartOfAccountIDTaxPurchase, " +
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
        public void UpdateFinancialInformation(long ProductGroupID, Int32 ChartOfAccountIDPurchase, Int32 ChartOfAccountIDSold, Int32 ChartOfAccountIDInventory, Int32 ChartOfAccountIDTaxPurchase, Int32 ChartOfAccountIDTaxSold)
        {
            try
            {
                string SQL = "UPDATE tblProductGroup SET " +
                                    "ChartOfAccountIDPurchase	= @ChartOfAccountIDPurchase, " +
                                    "ChartOfAccountIDSold		= @ChartOfAccountIDSold, " +
                                    "ChartOfAccountIDInventory	= @ChartOfAccountIDInventory, " +
                                    "ChartOfAccountIDTaxPurchase = @ChartOfAccountIDTaxPurchase, " +
                                    "ChartOfAccountIDTaxSold        = @ChartOfAccountIDTaxSold, " +
                                    "ChartOfAccountIDTransferIn	    = @ChartOfAccountIDPurchase, " +
                                    "ChartOfAccountIDTaxTransferIn  = @ChartOfAccountIDTaxPurchase, " +
                                    "ChartOfAccountIDTransferOut	= @ChartOfAccountIDSold, " +
                                    "ChartOfAccountIDTaxTransferOut = @ChartOfAccountIDTaxSold " +
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

        public Int32 Save(ProductGroupDetails Details)
        {
            try
            {
                string SQL = "CALL procSaveProductGroup(@ProductGroupID, @ProductGroupCode, @ProductGroupName, @BaseUnitID," +
                                        "@Price, @PurchasePrice, @IncludeInSubtotalDiscount, @VAT, @EVAT," +
                                        "@LocalTax, @OrderSlipPrinter, @ChartOfAccountIDPurchase, @ChartOfAccountIDTaxPurchase," +
                                        "@ChartOfAccountIDSold, @ChartOfAccountIDTaxSold, @ChartOfAccountIDInventory," +
                                        "@SequenceNo, @isLock, @ChartOfAccountIDTransferIn, @ChartOfAccountIDTaxTransferIn," +
                                        "@ChartOfAccountIDTransferOut, @ChartOfAccountIDTaxTransferOut," +
                                        "@ChartOfAccountIDInvAdjustment, @ChartOfAccountIDTaxInvAdjustment," +
                                        "@CreatedOn, @LastModified);";

                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandText = SQL;

                cmd.Parameters.AddWithValue("ProductGroupID", Details.ProductGroupID);
                cmd.Parameters.AddWithValue("ProductGroupCode", Details.ProductGroupCode);
                cmd.Parameters.AddWithValue("ProductGroupName", Details.ProductGroupName);
                cmd.Parameters.AddWithValue("BaseUnitID", Details.UnitDetails.UnitID);
                cmd.Parameters.AddWithValue("Price", Details.Price);
                cmd.Parameters.AddWithValue("PurchasePrice", Details.PurchasePrice);
                cmd.Parameters.AddWithValue("IncludeInSubtotalDiscount", Details.IncludeInSubtotalDiscount);
                cmd.Parameters.AddWithValue("VAT", Details.VAT);
                cmd.Parameters.AddWithValue("EVAT", Details.EVAT);
                cmd.Parameters.AddWithValue("LocalTax", Details.LocalTax);
                cmd.Parameters.AddWithValue("OrderSlipPrinter", Details.OrderSlipPrinter);
                cmd.Parameters.AddWithValue("ChartOfAccountIDPurchase", Details.ChartOfAccountIDPurchase);
                cmd.Parameters.AddWithValue("ChartOfAccountIDTaxPurchase", Details.ChartOfAccountIDTaxPurchase);
                cmd.Parameters.AddWithValue("ChartOfAccountIDSold", Details.ChartOfAccountIDSold);
                cmd.Parameters.AddWithValue("ChartOfAccountIDTaxSold", Details.ChartOfAccountIDTaxSold);
                cmd.Parameters.AddWithValue("ChartOfAccountIDInventory", Details.ChartOfAccountIDInventory);
                cmd.Parameters.AddWithValue("SequenceNo", Details.SequenceNo);
                cmd.Parameters.AddWithValue("isLock", Details.isLock);
                cmd.Parameters.AddWithValue("ChartOfAccountIDTransferIn", Details.ProductGroupChartOfAccountDetails.ChartOfAccountIDTransferIn);
                cmd.Parameters.AddWithValue("ChartOfAccountIDTaxTransferIn", Details.ProductGroupChartOfAccountDetails.ChartOfAccountIDTaxTransferIn);
                cmd.Parameters.AddWithValue("ChartOfAccountIDTransferOut", Details.ProductGroupChartOfAccountDetails.ChartOfAccountIDTransferOut);
                cmd.Parameters.AddWithValue("ChartOfAccountIDTaxTransferOut", Details.ProductGroupChartOfAccountDetails.ChartOfAccountIDTaxTransferOut);
                cmd.Parameters.AddWithValue("ChartOfAccountIDInvAdjustment", Details.ProductGroupChartOfAccountDetails.ChartOfAccountIDInvAdjustment);
                cmd.Parameters.AddWithValue("ChartOfAccountIDTaxInvAdjustment", Details.ProductGroupChartOfAccountDetails.ChartOfAccountIDTaxInvAdjustment);
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

				SQL=	"DELETE FROM tblProductGroupUnitMatrix WHERE GroupID IN (" + IDs + ");";
				cmd = new MySqlCommand(); 
				
				cmd.CommandType = System.Data.CommandType.Text;
				cmd.CommandText = SQL;
				base.ExecuteNonQuery(cmd);
				
				SQL=	"DELETE FROM tblProductGroupVariations WHERE GroupID IN (" + IDs + ");";
				cmd = new MySqlCommand(); 
				
				cmd.CommandType = System.Data.CommandType.Text;
				cmd.CommandText = SQL;
				base.ExecuteNonQuery(cmd);

				SQL=	"DELETE FROM tblProductGroupVariationsMatrix WHERE MatrixID IN (SELECT MatrixID FROM tblProductGroupBaseVariationsMatrix WHERE GroupID IN (" + IDs + "));";
				cmd = new MySqlCommand(); 
				
				cmd.CommandType = System.Data.CommandType.Text;
				cmd.CommandText = SQL;
				base.ExecuteNonQuery(cmd);

				SQL=	"DELETE FROM tblProductGroupBaseVariationsMatrix WHERE GroupID IN (" + IDs + ");";
				cmd = new MySqlCommand(); 
				
				cmd.CommandType = System.Data.CommandType.Text;
				cmd.CommandText = SQL;
				base.ExecuteNonQuery(cmd);

				SQL=	"DELETE FROM tblProductGroup WHERE ProductGroupID IN (" + IDs + ");";
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
                                "a.ProductGroupID, " +
                                "a.ProductGroupCode, " +
                                "a.ProductGroupName, " +
                                "a.BaseUnitID, " +
                                "b.UnitName 'BaseUnitName', " +
                                "a.Price, " +
                                "a.PurchasePrice, " +
                                "a.IncludeInSubtotalDiscount, " +
                                "a.VAT, " +
                                "a.EVAT, " +
                                "a.LocalTax, " +
                                "OrderSlipPrinter, " +
                                "a.ChartOfAccountIDPurchase, " +
                                "a.ChartOfAccountIDSold, " +
                                "a.ChartOfAccountIDInventory, " +
                                "a.ChartOfAccountIDTaxPurchase, " +
                                "a.ChartOfAccountIDTaxSold, " +
                                "a.isLock " +
                            "FROM tblProductGroup a " +
                            "INNER JOIN tblUnit b ON a.BaseUnitID = b.UnitID ";
            return stSQL;
        }

        private string SQLSelectSimple()
        {
            string stSQL = "SELECT " +
                                "a.ProductGroupID, " +
                                "a.ProductGroupCode, " +
                                "a.ProductGroupName, " +
                                "a.SequenceNo " +
                            "FROM tblProductGroup a ";
            return stSQL;
        }

		#region Details

		public ProductGroupDetails Details(Int64 ProductGroupID)
		{
			try
			{
                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandType = System.Data.CommandType.Text;

				string SQL=	SQLSelect() + " WHERE ProductGroupID = @ProductGroupID;";
				  
                cmd.Parameters.AddWithValue("@ProductGroupID", ProductGroupID);

                cmd.CommandText = SQL;
                string strDataTableName = "tbl" + this.GetType().FullName.Split(new Char[] { '.' })[this.GetType().FullName.Split(new Char[] { '.' }).Length - 1]; System.Data.DataTable dt = new System.Data.DataTable(strDataTableName);
                base.MySqlDataAdapterFill(cmd, dt);
				
				ProductGroupDetails Details = new ProductGroupDetails();
                foreach (System.Data.DataRow dr in dt.Rows)
                {
                    Details.ProductGroupID = Int64.Parse(dr["ProductGroupID"].ToString());
                    Details.ProductGroupCode = "" + dr["ProductGroupCode"].ToString();
                    Details.ProductGroupName = "" + dr["ProductGroupName"].ToString();
                    Details.Price = decimal.Parse(dr["Price"].ToString());
                    Details.PurchasePrice = decimal.Parse(dr["PurchasePrice"].ToString());
                    Details.IncludeInSubtotalDiscount = bool.Parse(dr["IncludeInSubtotalDiscount"].ToString());
                    Details.VAT = decimal.Parse(dr["VAT"].ToString());
                    Details.EVAT = decimal.Parse(dr["EVAT"].ToString());
                    Details.LocalTax = decimal.Parse(dr["LocalTax"].ToString());
                    Details.OrderSlipPrinter = (OrderSlipPrinter)Enum.Parse(typeof(OrderSlipPrinter), dr["OrderSlipPrinter"].ToString());
                    /*** Added for Financial Information  ***/
                    /*** March 07, 2009 ***/
                    Details.ChartOfAccountIDPurchase = Int32.Parse(dr["ChartOfAccountIDPurchase"].ToString());
                    Details.ChartOfAccountIDSold = Int32.Parse(dr["ChartOfAccountIDSold"].ToString());
                    Details.ChartOfAccountIDInventory = Int32.Parse(dr["ChartOfAccountIDInventory"].ToString());
                    Details.ChartOfAccountIDTaxPurchase = Int32.Parse(dr["ChartOfAccountIDTaxPurchase"].ToString());
                    Details.ChartOfAccountIDTaxSold = Int32.Parse(dr["ChartOfAccountIDTaxSold"].ToString());

                    Details.isLock = bool.Parse(dr["isLock"].ToString());
                    Details.UnitDetails = new Unit(base.Connection, base.Transaction).Details(Int32.Parse(dr["BaseUnitID"].ToString()));
                }

				return Details;
			}

			catch (Exception ex)
			{
				throw base.ThrowException(ex);
			}	
		}

		public ProductGroupDetails Details(string ProductGroupCode)
		{
			try
			{
                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandType = System.Data.CommandType.Text;
				
				string SQL=	SQLSelect() + "WHERE ProductGroupCode = @ProductGroupCode;";

                cmd.Parameters.AddWithValue("@ProductGroupCode", ProductGroupCode);

                cmd.CommandText = SQL;
                string strDataTableName = "tbl" + this.GetType().FullName.Split(new Char[] { '.' })[this.GetType().FullName.Split(new Char[] { '.' }).Length - 1]; System.Data.DataTable dt = new System.Data.DataTable(strDataTableName);
                base.MySqlDataAdapterFill(cmd, dt);

                ProductGroupDetails Details = new ProductGroupDetails();
                foreach (System.Data.DataRow dr in dt.Rows)
                {
                    Details.ProductGroupID = Int64.Parse(dr["ProductGroupID"].ToString());
                    Details.ProductGroupCode = "" + dr["ProductGroupCode"].ToString();
                    Details.ProductGroupName = "" + dr["ProductGroupName"].ToString();
                    Details.Price = decimal.Parse(dr["Price"].ToString());
                    Details.PurchasePrice = decimal.Parse(dr["PurchasePrice"].ToString());
                    Details.IncludeInSubtotalDiscount = bool.Parse(dr["IncludeInSubtotalDiscount"].ToString());
                    Details.VAT = decimal.Parse(dr["VAT"].ToString());
                    Details.EVAT = decimal.Parse(dr["EVAT"].ToString());
                    Details.LocalTax = decimal.Parse(dr["LocalTax"].ToString());
                    Details.OrderSlipPrinter = (OrderSlipPrinter)Enum.Parse(typeof(OrderSlipPrinter), dr["OrderSlipPrinter"].ToString());
                    /*** Added for Financial Information  ***/
                    /*** March 07, 2009 ***/
                    Details.ChartOfAccountIDPurchase = Int32.Parse(dr["ChartOfAccountIDPurchase"].ToString());
                    Details.ChartOfAccountIDSold = Int32.Parse(dr["ChartOfAccountIDSold"].ToString());
                    Details.ChartOfAccountIDInventory = Int32.Parse(dr["ChartOfAccountIDInventory"].ToString());
                    Details.ChartOfAccountIDTaxPurchase = Int32.Parse(dr["ChartOfAccountIDTaxPurchase"].ToString());
                    Details.ChartOfAccountIDTaxSold = Int32.Parse(dr["ChartOfAccountIDTaxSold"].ToString());

                    Details.isLock = bool.Parse(dr["isLock"].ToString());
                    Details.UnitDetails = new Unit(base.Connection, base.Transaction).Details(Int32.Parse(dr["BaseUnitID"].ToString()));
                }

                return Details;
			}

			catch (Exception ex)
			{
				throw base.ThrowException(ex);
			}	
		}

		
		#endregion

		#region Streams

        public System.Data.DataTable ListAsDataTableSimple(long SequenceNoStart, System.Data.SqlClient.SortOrder SequenceSortOrder, string SortField = "SequenceNo", SortOption SortOrder = SortOption.Ascending)
        {
            try
            {
                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandType = System.Data.CommandType.Text;

                string SQL = SQLSelectSimple() + "WHERE 1=1 ";

                if (SequenceNoStart != 0)
                {
                    if (SequenceSortOrder == System.Data.SqlClient.SortOrder.Descending)
                        SQL += "AND SequenceNo <= " + SequenceNoStart.ToString() + " ";
                    else
                        SQL += "AND SequenceNo >= " + SequenceNoStart.ToString() + " ";
                }

                SQL += "ORDER BY " + SortField + " ";
                SQL += SortOrder == SortOption.Ascending ? "ASC " : "DESC ";

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
        public System.Data.DataTable ListAsDataTable(string SearchKey = "", string SortField = "ProductGroupCode", SortOption SortOrder = SortOption.Ascending, Int32 Limit = 0)
        {
            try
            {
                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandType = System.Data.CommandType.Text;

                string SQL = SQLSelect() + " ";

                if (!string.IsNullOrEmpty(SearchKey))
                {
                    SQL += "WHERE (ProductGroupCode LIKE @SearchKey or ProductGroupName LIKE @SearchKey) ";
                    cmd.Parameters.AddWithValue("@SearchKey", "%" + SearchKey + "%");
                }

                SQL += "ORDER BY " + SortField + " ";
                SQL += SortOrder == SortOption.Ascending ? "ASC " : "DESC ";
                SQL += Limit == 0 ? "" : " LIMIT " + Limit.ToString();

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

		#endregion
	}
}


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
		public Int32 BaseUnitID;
		public string BaseUnitName;
		public decimal Price;
		public decimal PurchasePrice;
		public bool IncludeInSubtotalDiscount;
		public decimal VAT;
		public decimal EVAT;
		public decimal LocalTax;
        public OrderSlipPrinter OrderSlipPrinter;
        public int ChartOfAccountIDPurchase;
        public int ChartOfAccountIDSold;
        public int ChartOfAccountIDInventory;
        public int ChartOfAccountIDTaxPurchase;
        public int ChartOfAccountIDTaxSold;

        public bool isLock;
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
				string SQL = "INSERT INTO tblProductGroup (" +
								"ProductGroupCode, " +
								"ProductGroupName, " +
								"BaseUnitID, " +
								"Price, " +
								"PurchasePrice, " +
								"IncludeInSubtotalDiscount, " +
								"VAT, " +
								"EVAT, " +
								"LocalTax," +
                                "OrderSlipPrinter" +
							") VALUES (" +
								"@ProductGroupCode, " +
								"@ProductGroupName, " +
								"@BaseUnitID, " +
								"@Price, " +
								"@PurchasePrice, " +
								"@IncludeInSubtotalDiscount, " +
								"@VAT, " +
								"@EVAT, " +
								"@LocalTax," +
                                "@OrderSlipPrinter);";
				  
				
	 			
				MySqlCommand cmd = new MySqlCommand();
				
				
				cmd.CommandType = System.Data.CommandType.Text;
				cmd.CommandText = SQL;
				
				MySqlParameter prmProductGroupCode = new MySqlParameter("@ProductGroupCode",MySqlDbType.String);			
				prmProductGroupCode.Value = Details.ProductGroupCode;
				cmd.Parameters.Add(prmProductGroupCode);

				MySqlParameter prmProductGroupName = new MySqlParameter("@ProductGroupName",MySqlDbType.String);			
				prmProductGroupName.Value = Details.ProductGroupName;
				cmd.Parameters.Add(prmProductGroupName);

				MySqlParameter prmBaseUnitID = new MySqlParameter("@BaseUnitID",MySqlDbType.Int32);			
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

                MySqlParameter prmOrderSlipPrinter = new MySqlParameter("@OrderSlipPrinter",MySqlDbType.Int16);
                prmOrderSlipPrinter.Value = (int) Details.OrderSlipPrinter;
                cmd.Parameters.Add(prmOrderSlipPrinter);
                
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
				
				
				{
					
					
					
					
				}

				throw base.ThrowException(ex);
			}	
		}
		public void Update(ProductGroupDetails Details)
		{
			try 
			{
				string SQL=	"UPDATE tblProductGroup SET " + 
								"ProductGroupCode		= @ProductGroupCode, " + 
								"ProductGroupName		= @ProductGroupName, " +  
								"BaseUnitID				= @BaseUnitID, " +
								"Price					= @Price, " +
								"PurchasePrice			= @PurchasePrice, " +
								"IncludeInSubtotalDiscount	=	@IncludeInSubtotalDiscount, " +
								"VAT					= @VAT, " +
								"EVAT					= @EVAT, " +
								"LocalTax				= @LocalTax, " +
                                "OrderSlipPrinter       = @OrderSlipPrinter " +
							"WHERE ProductGroupID = @ProductGroupID;";
				  
				
	 			
				MySqlCommand cmd = new MySqlCommand();
				
				
				cmd.CommandType = System.Data.CommandType.Text;
				cmd.CommandText = SQL;
				
				MySqlParameter prmProductGroupCode = new MySqlParameter("@ProductGroupCode",MySqlDbType.String);			
				prmProductGroupCode.Value = Details.ProductGroupCode;
				cmd.Parameters.Add(prmProductGroupCode);

				MySqlParameter prmProductGroupName = new MySqlParameter("@ProductGroupName",MySqlDbType.String);			
				prmProductGroupName.Value = Details.ProductGroupName;
				cmd.Parameters.Add(prmProductGroupName);

				MySqlParameter prmBaseUnitID = new MySqlParameter("@BaseUnitID",MySqlDbType.Int32);			
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

                MySqlParameter prmOrderSlipPrinter = new MySqlParameter("@OrderSlipPrinter",MySqlDbType.Int16);
                prmOrderSlipPrinter.Value = (int) Details.OrderSlipPrinter;
                cmd.Parameters.Add(prmOrderSlipPrinter);
                
				MySqlParameter prmProductGroupID = new MySqlParameter("@ProductGroupID",MySqlDbType.Int64);						
				prmProductGroupID.Value = Details.ProductGroupID;
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

        public void ChangeTax(long ProductGroupID, decimal NewVAT, decimal NewEVAT, decimal NewLocalTax)
        {
            try
            {
                string SQL = "UPDATE tblProductGroup SET " +
                                    "VAT		= @NewVAT, " +
                                    "EVAT		= @NewEVAT, " +
                                    "LocalTax	= @NewLocalTax ";
                if (ProductGroupID !=0) SQL += "WHERE ProductGroupID		= @ProductGroupID;";

                

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

                if (ProductGroupID != 0)
                {
                    MySqlParameter prmProductGroupID = new MySqlParameter("@ProductGroupID",MySqlDbType.Int64);
                    prmProductGroupID.Value = ProductGroupID;
                    cmd.Parameters.Add(prmProductGroupID);
                }

                base.ExecuteNonQuery(cmd);

                ProductGroupVariationsMatrix clsProductGroupVariationsMatrix = new ProductGroupVariationsMatrix(base.Connection, base.Transaction);
                clsProductGroupVariationsMatrix.ChangeTax(ProductGroupID, NewVAT, NewEVAT, NewLocalTax);

                ProductSubGroup clsProductSubGroup = new ProductSubGroup(base.Connection, base.Transaction);
                clsProductSubGroup.ChangeTax(ProductGroupID, 0, NewVAT, NewEVAT, NewLocalTax);
            }

            catch (Exception ex)
            {
                
                
                {
                    
                    
                    
                    
                }

                throw base.ThrowException(ex);
            }
        }

        public void UpdateFinancialInformation(int ChartOfAccountIDPurchase, int ChartOfAccountIDSold, int ChartOfAccountIDInventory, int ChartOfAccountIDTaxPurchase, int ChartOfAccountIDTaxSold)
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
        public void UpdateFinancialInformation(long ProductGroupID, int ChartOfAccountIDPurchase, int ChartOfAccountIDSold, int ChartOfAccountIDInventory, int ChartOfAccountIDTaxPurchase, int ChartOfAccountIDTaxSold)
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
				string SQL=	SQLSelect() + "WHERE ProductGroupID = @ProductGroupID;";
				  
				MySqlCommand cmd = new MySqlCommand();
				cmd.CommandType = System.Data.CommandType.Text;
				cmd.CommandText = SQL;

				MySqlParameter prmProductGroupID = new MySqlParameter("@ProductGroupID",MySqlDbType.Int16);
				prmProductGroupID.Value = ProductGroupID;
				cmd.Parameters.Add(prmProductGroupID);

                System.Data.DataTable dt = new System.Data.DataTable("tblProductGroup");
                base.MySqlDataAdapterFill(cmd, dt);
                
				
				ProductGroupDetails Details = new ProductGroupDetails();

                foreach (System.Data.DataRow dr in dt.Rows)
                {
                    Details.ProductGroupID = Int64.Parse(dr["ProductGroupID"].ToString());
                    Details.ProductGroupCode = "" + dr["ProductGroupCode"].ToString();
                    Details.ProductGroupName = "" + dr["ProductGroupName"].ToString();
                    Details.BaseUnitID = Int32.Parse(dr["BaseUnitID"].ToString());
                    Details.BaseUnitName = "" + dr["BaseUnitName"].ToString();
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

                    Details.isLock = Convert.ToBoolean (Int16.Parse(dr["isLock"].ToString()));
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
				string SQL=	SQLSelect() + "WHERE ProductGroupCode = @ProductGroupCode;";
				  
				
	 			
				MySqlCommand cmd = new MySqlCommand();
				
				
				cmd.CommandType = System.Data.CommandType.Text;
				cmd.CommandText = SQL;

				MySqlParameter prmProductGroupCode = new MySqlParameter("@ProductGroupCode",MySqlDbType.String);
				prmProductGroupCode.Value = ProductGroupCode;
				cmd.Parameters.Add(prmProductGroupCode);

				System.Data.DataTable dt = new System.Data.DataTable("tblProductGroup");
                base.MySqlDataAdapterFill(cmd, dt);
                

                ProductGroupDetails Details = new ProductGroupDetails();

                foreach (System.Data.DataRow dr in dt.Rows)
				{
					Details.ProductGroupID = Int64.Parse(dr["ProductGroupID"].ToString());
					Details.ProductGroupCode = "" + dr["ProductGroupCode"].ToString();
					Details.ProductGroupName = "" + dr["ProductGroupName"].ToString();
					Details.BaseUnitID = Int32.Parse(dr["BaseUnitID"].ToString());
					Details.BaseUnitName = "" + dr["BaseUnitName"].ToString();
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

		public MySqlDataReader List(string SortField, SortOption SortOrder)
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
		public MySqlDataReader Search(string SearchKey, string SortField, SortOption SortOrder)
		{
			try
			{
				string SQL =SQLSelect() + "WHERE 1=1 " +
							            "AND (ProductGroupCode LIKE @SearchKey " +
							            "OR ProductGroupName LIKE @SearchKey " +
							            "OR UnitName LIKE @SearchKey) " +
							            "ORDER BY " + SortField;

				if (SortOrder == SortOption.Ascending)
					SQL += " ASC";
				else
					SQL += " DESC";

				

				MySqlCommand cmd = new MySqlCommand();
				
				
				cmd.CommandType = System.Data.CommandType.Text;
				cmd.CommandText = SQL;
				
				MySqlParameter prmSearchKey = new MySqlParameter("@SearchKey",MySqlDbType.String);
				prmSearchKey.Value = "%" + SearchKey + "%";
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
        public System.Data.DataTable SearchDataTable(string SearchKey, string SortField = "ProductGroupName", SortOption SortOrder = SortOption.Ascending, int Limit=Constants.C_DEFAULT_LIMIT_OF_RECORD_TO_SHOW)
        {
            try
            {
                string SQL = SQLSelect() + "WHERE 1=1 " +
                                        "AND (ProductGroupCode LIKE @SearchKey " +
                                        "OR ProductGroupName LIKE @SearchKey) ";

                if (SortField == string.Empty) SortField = "ProductGroupCode";
                SQL += "ORDER BY " + SortField;

                if (SortOrder == SortOption.Ascending)
                    SQL += " ASC ";
                else
                    SQL += " DESC ";

                if (Limit != 0)
                    SQL += "LIMIT " + Limit + " ";

                

                MySqlCommand cmd = new MySqlCommand();
                
                
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandText = SQL;

                MySqlParameter prmSearchKey = new MySqlParameter("@SearchKey",MySqlDbType.String);
                prmSearchKey.Value = "%" + SearchKey + "%";
                cmd.Parameters.Add(prmSearchKey);

                System.Data.DataTable dt = new System.Data.DataTable("tblProductGroup");
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

        public System.Data.DataTable DataTable(string SortField, SortOption SortOrder)
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

                System.Data.DataTable dt = new System.Data.DataTable("tblProductGroup");
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
        public System.Data.DataTable DataTableSimple(long SequenceNoStart, System.Data.SqlClient.SortOrder SequenceSortOrder, string SortField, SortOption SortOrder)
        {
            try
            {
                string SQL = SQLSelectSimple() + "WHERE 1=1 ";

                if (SequenceNoStart != 0)
                {
                    if (SequenceSortOrder == System.Data.SqlClient.SortOrder.Descending)
                        SQL += "AND SequenceNo <= " + SequenceNoStart.ToString() + " ";
                    else
                        SQL += "AND SequenceNo >= " + SequenceNoStart.ToString() + " ";
                }

                if (SortField == string.Empty) SortField = "SequenceNo";
                SQL += "ORDER BY " + SortField + " ";

                if (SortOrder == SortOption.Ascending)
                    SQL += "ASC ";
                else
                    SQL += "DESC ";

                

                MySqlCommand cmd = new MySqlCommand();
                
                
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandText = SQL;

                System.Data.DataTable dt = new System.Data.DataTable("tblProductGroup");
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
        public System.Data.DataTable ListAsDataTable(string Searchkey = "", string SortField = "ProductGroupCode", SortOption SortOrder = SortOption.Ascending)
        {
            try
            {
                MySqlCommand cmd = new MySqlCommand();

                string SQL = SQLSelect();
                if (!string.IsNullOrEmpty(Searchkey)) {
                    SQL+= "WHERE ProductGroupCode LIKE @SearchKey ";

                    cmd.Parameters.AddWithValue("@Searchkey", Searchkey);
                }
                
                SQL+= "ORDER BY " + SortField;

                if (SortOrder == SortOption.Ascending)
                    SQL += " ASC";
                else
                    SQL += " DESC";

                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandText = SQL;

                System.Data.DataTable dt = new System.Data.DataTable("tblProductGroup");
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


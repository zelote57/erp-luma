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
	#region Struct

	public struct ProductCompositionDetails
	{
		public Int64 CompositionID;
        public Int64 MainProductID;
		public Int64 ProductID;
		public string ProductCode;
		public string ProductDesc;
		public Int64 VariationMatrixID;
		public string MatrixDescription;
		public decimal Quantity;
		public Int32 UnitID;
		public string UnitCode;
		public string UnitName;

        public DateTime CreatedOn;
        public DateTime LastModified;
	}

	
	#endregion

	[StrongNameIdentityPermissionAttribute(SecurityAction.LinkDemand,
		 PublicKey = "002400000480000094000000060200000024000" +
		 "052534131000400000100010053D785642F9F960B43157E0380" +
		 "F393BEE53E8DFAFBF441366C1B6F8B48D9DDF0D527B1F3B21EA" +
		 "E85D2FDB664CE81EB8A87DBE4C589D6F4202FE2B7D4B978BB69" +
		 "684874612CB9B8DB7A0339400A9C4E68277884B07817363D242" +
		 "E3696F9FACDBEA831810AE6DC9EDCA91A7B5DA12FE7BF65D113" +
		 "FF52834EAFB5A7A1FDFD5851A3")]
	public class ProductComposition : POSConnection
    {
		#region Constructors and Destructors

		public ProductComposition()
            : base(null, null)
        {
        }

        public ProductComposition(MySqlConnection Connection, MySqlTransaction Transaction) 
            : base(Connection, Transaction)
		{

		}

		#endregion

		#region Insert and Update

		public Int64 Insert(ProductCompositionDetails Details)
		{
			try  
			{
                Save(Details);

                return Int32.Parse(base.getLAST_INSERT_ID(this));
			}
			catch (Exception ex)
			{
				throw base.ThrowException(ex);
			}	
		}

		public void Update(ProductCompositionDetails Details)
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

        public Int32 Save(ProductCompositionDetails Details)
        {
            try
            {
                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandType = System.Data.CommandType.Text;
                
                string SQL = "CALL procSaveProductComposition(@CompositionID, @MainProductID, @ProductID, @VariationMatrixID, @UnitID, @Quantity, @CreatedOn, @LastModified);";

                cmd.Parameters.AddWithValue("CompositionID", Details.CompositionID);
                cmd.Parameters.AddWithValue("MainProductID", Details.MainProductID);
                cmd.Parameters.AddWithValue("ProductID", Details.ProductID);
                cmd.Parameters.AddWithValue("VariationMatrixID", Details.VariationMatrixID);
                cmd.Parameters.AddWithValue("UnitID", Details.UnitID);
                cmd.Parameters.AddWithValue("Quantity", Details.Quantity);
                cmd.Parameters.AddWithValue("CreatedOn", Details.CreatedOn == DateTime.MinValue ? Constants.C_DATE_MIN_VALUE : Details.CreatedOn);
                cmd.Parameters.AddWithValue("LastModified", Details.LastModified == DateTime.MinValue ? Constants.C_DATE_MIN_VALUE : Details.LastModified);

                cmd.CommandText = SQL;
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
				MySqlCommand cmd = new MySqlCommand();
                cmd.CommandType = System.Data.CommandType.Text;

                string SQL = "DELETE FROM tblProductComposition WHERE CompositionID IN (" + IDs + ");";
				
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

		#region Details

		public ProductCompositionDetails Details(Int64 CompositionID)
		{
			try
			{
                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandType = System.Data.CommandType.Text;

				string SQL = SQLSelect() + "WHERE a.CompositionID = @CompositionID;";
				  
                cmd.Parameters.AddWithValue("@CompositionID", CompositionID);

                cmd.CommandText = SQL;
                string strDataTableName = "tbl" + this.GetType().FullName.Split(new Char[] { '.' })[this.GetType().FullName.Split(new Char[] { '.' }).Length - 1]; System.Data.DataTable dt = new System.Data.DataTable(strDataTableName);
                base.MySqlDataAdapterFill(cmd, dt);

				ProductCompositionDetails Details = new ProductCompositionDetails();
                foreach (System.Data.DataRow dr in dt.Rows)
				{
					Details.CompositionID = Int64.Parse(dr["CompositionID"].ToString());
                    Details.MainProductID = Int64.Parse(dr["MainProductID"].ToString());
                    Details.ProductID = Int64.Parse(dr["ProductID"].ToString());
					Details.ProductCode = dr["ProductCode"].ToString();
					Details.ProductDesc = dr["ProductDesc"].ToString();
                    Details.VariationMatrixID = Int64.Parse(dr["VariationMatrixID"].ToString());
					Details.MatrixDescription = dr["MatrixDescription"].ToString();
					Details.Quantity = Decimal.Parse(dr["Quantity"].ToString());
					Details.UnitID = Int32.Parse(dr["UnitID"].ToString());
					Details.UnitCode = dr["UnitCode"].ToString();
					Details.UnitName = dr["UnitName"].ToString();
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

		private string SQLSelect()
		{
            string SQL = "SELECT " +
                                "CompositionID, " +
                                "MainProductID, " +
                                "a.ProductID, " +
                                "ProductCode, " +
                                "ProductDesc, " +
                                "a.VariationMatrixID, " +
                                "c.Description as MatrixDescription, " +
                                "a.Quantity, " +
                                "a.UnitID, " +
                                "d.UnitCode, " +
                                "d.UnitName, " +
                                "f.OrderSlipPrinter " +
                            "FROM tblProductComposition a " +
                            "INNER JOIN tblProducts b ON a.ProductID = b.ProductID " +
                            "LEFT OUTER JOIN tblProductBaseVariationsMatrix c ON a.VariationMatrixID = c.MatrixID " +
                            "INNER JOIN tblUnit d ON a.UnitID = d.UnitID " +
                            "INNER JOIN tblProductSubGroup e ON b.ProductSubGroupID = e.ProductSubGroupID " +
                            "INNER JOIN tblProductGroup f ON e.ProductGroupID = f.ProductGroupID ";
			return SQL;
		}

		public System.Data.DataTable ListAsDataTable(Int64 MainProductID, string SortField = "CompositionID", SortOption SortOrder = SortOption.Ascending, Int32 limit = 0)
		{
			try
			{
                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandType = System.Data.CommandType.Text;

                string SQL = SQLSelect() + "WHERE MainProductID = @MainProductID ";

                cmd.Parameters.AddWithValue("@MainProductID", MainProductID);

                SQL += "ORDER BY " + SortField + " ";
                SQL += SortOrder == SortOption.Ascending ? "ASC " : "DESC ";
                SQL += limit == 0 ? "" : "LIMIT " + limit.ToString() + " ";

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

        // returns True if a composition exist
        // returns False if a composition does not exist
        public bool GeneratePLUReport(Int32 BranchID, string TerminalNo, long MainProductID, string MainProductCode, decimal Quantity)
		{
			try
			{
                
                bool boRetValue = false;

				System.Data.DataTable dtCompositionList = ListAsDataTable(MainProductID, "CompositionID", SortOption.Ascending);

				Data.PLUReport clsPLUReport = new Data.PLUReport(base.Connection, base.Transaction);
				PLUReportDetails clsPLUReportDetails;

				foreach(System.Data.DataRow dr in dtCompositionList.Rows)
				{
					long lProductID = Convert.ToInt64(dr["ProductID"]);
                    string stProductCode = dr["ProductCode"].ToString();
					decimal decQuantity = Convert.ToDecimal(Convert.ToDecimal(dr["Quantity"]) * Quantity);
                    OrderSlipPrinter locOrderSlipPrinter = (OrderSlipPrinter)Enum.Parse(typeof(OrderSlipPrinter), dr["OrderSlipPrinter"].ToString());

                    clsPLUReportDetails = new PLUReportDetails();
                    clsPLUReportDetails.BranchDetails = new BranchDetails
                    {
                        BranchID = BranchID
                    };
                    clsPLUReportDetails.TerminalNo = TerminalNo;
                    clsPLUReportDetails.ProductID = lProductID;
                    clsPLUReportDetails.ProductCode = MainProductCode + "-" + stProductCode;
                    clsPLUReportDetails.Quantity = decQuantity;
                    clsPLUReportDetails.Amount = 0;
                    clsPLUReportDetails.OrderSlipPrinter = locOrderSlipPrinter;

                    clsPLUReport.Insert(clsPLUReportDetails);

                    GeneratePLUReport(BranchID, TerminalNo, lProductID, stProductCode, decQuantity);

                    boRetValue = true;
				}

                return boRetValue;
			}
			catch (Exception ex)
			{
				throw base.ThrowException(ex);
			}	
		}
	}
}


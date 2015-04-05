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

	public struct ProductPurchasePriceHistoryDetails
	{
        public Int64 ProductPurchasePriceHistoryID;
		public Int64 ProductID;
        public ProductDetails ProductDetails;
        public Int64 MatrixID;
        public Int64 SupplierID;
        public ContactDetails SupplierDetails;
        public decimal PurchasePrice;
        public DateTime PurchaseDate;
        public string Remarks;
        public string PurchaserName;
        public DateTime DateCreated;
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
	public class ProductPurchasePriceHistory : POSConnection
    {
		#region Constructors and Destructors

		public ProductPurchasePriceHistory()
            : base(null, null)
        {
        }

        public ProductPurchasePriceHistory(MySqlConnection Connection, MySqlTransaction Transaction) 
            : base(Connection, Transaction)
		{

		}

		#endregion

        #region Insert and Update

        public void AddToList(ProductPurchasePriceHistoryDetails Details)
        {
            try
            {
                System.Data.DataTable dt = ListAsDataTable(Details.ProductID, DateTime.Now.AddMonths(-6), "PurchasePrice", SortOption.Desscending);
                if (dt.Rows.Count < DataConstants.MAX_PURCHASE_PRICE_SUPPLIER)
                {
                    //insert new purchase price if price levels are lower than max
                    Insert(Details);
                }
                else
                {
                    long lngCtr = 0;
                    //update purchase price
                    foreach (System.Data.DataRow dr in dt.Rows)
                    {
                        decimal decPurchasePrice = decimal.Parse(dr["PurchasePrice"].ToString());
                        long lngProductPurchasePriceHistoryID = long.Parse(dr["ProductPurchasePriceHistoryID"].ToString());
                        Details.ProductPurchasePriceHistoryID = lngProductPurchasePriceHistoryID;

                        lngCtr += 1;
                        if (decPurchasePrice >= Details.PurchasePrice )
                            break;
                        else if(dt.Rows.Count == lngCtr && decPurchasePrice == 0)
                            break;
                    }
                    //if it comes here then update the most oldest with the latest
                    Update(Details);
                }
            }
            catch (Exception ex)
            {
                throw base.ThrowException(ex);
            }
        }

		private Int64 Insert(ProductPurchasePriceHistoryDetails Details)
		{
			try  
			{
                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandType = System.Data.CommandType.Text;

				string SQL =	"INSERT INTO tblProductPurchasePriceHistory (" +
									"ProductID, " + 
									"MatrixID, " + 
									"SupplierID, " +  
									"PurchasePrice, " + 
									"PurchaseDate, " + 
                                    "Remarks," +
                                    "PurchaserName," +
                                    "DateCreated" +
								") VALUES (" +
                                    "@ProductID, " +
                                    "@MatrixID, " +
                                    "@SupplierID, " +
                                    "@PurchasePrice, " + 
									"@PurchaseDate, " +
                                    "@Remarks," +
                                    "@PurchaserName," +
                                    "now());"; 
	 			
				cmd.Parameters.AddWithValue("@ProductID",Details.ProductID);
                cmd.Parameters.AddWithValue("@MatrixID", Details.MatrixID);
                cmd.Parameters.AddWithValue("@SupplierID", Details.SupplierID);
                cmd.Parameters.AddWithValue("@PurchasePrice", Details.PurchasePrice);
                cmd.Parameters.AddWithValue("@PurchaseDate", Details.PurchaseDate.ToString("yyyy-MM-dd HH:mm:ss"));
                cmd.Parameters.AddWithValue("@Remarks", Details.Remarks);
                cmd.Parameters.AddWithValue("@PurchaserName", Details.PurchaserName);

                cmd.CommandText = SQL;
				base.ExecuteNonQuery(cmd);

                return Int64.Parse(base.getLAST_INSERT_ID(this));
			}
			catch (Exception ex)
			{
				throw base.ThrowException(ex);
			}	
		}

        private void Update(ProductPurchasePriceHistoryDetails Details)
		{
			try 
			{
                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandType = System.Data.CommandType.Text;
				
				string SQL =	"UPDATE tblProductPurchasePriceHistory SET " +
									"SupplierID     = @SupplierID, " + 
									"PurchasePrice  = @PurchasePrice, " +  
									"PurchaseDate	= @PurchaseDate, " + 
									"Remarks        = @Remarks, " +
                                    "PurchaserName       = @PurchaserName " +
								"WHERE ProductPurchasePriceHistoryID	= @ProductPurchasePriceHistoryID;";
				  
                cmd.Parameters.AddWithValue("@ProductPurchasePriceHistoryID", Details.ProductPurchasePriceHistoryID);
                cmd.Parameters.AddWithValue("@SupplierID", Details.SupplierID);
                cmd.Parameters.AddWithValue("@PurchasePrice", Details.PurchasePrice);
                cmd.Parameters.AddWithValue("@PurchaseDate", Details.PurchaseDate.ToString("yyyy-MM-dd HH:mm:ss"));
                cmd.Parameters.AddWithValue("@Remarks", Details.Remarks);
                cmd.Parameters.AddWithValue("@PurchaserName", Details.PurchaserName);

                cmd.CommandText = SQL;
				base.ExecuteNonQuery(cmd);

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

				string SQL=	"DELETE FROM tblProductPurchasePriceHistory WHERE ProductPurchasePriceHistoryID IN (" + IDs + ");";

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
                                    "a.ProductPurchasePriceHistoryID, " +
                                    "a.ProductID, " +
                                    "a.MatrixID, " +
                                    "a.SupplierID, " +
                                    "c.ContactCode AS 'SupplierCode', " +
                                    "c.ContactName AS 'SupplierName', " +
                                    "a.PurchasePrice, " +
                                    "a.PurchaseDate, " +
                                    "a.Remarks, " +
                                    "a.PurchaserName " +
                                "FROM tblProductPurchasePriceHistory a " +
                                "INNER JOIN tblProducts b ON a.ProductID = b.ProductID " +
                                "INNER JOIN tblContacts c ON a.SupplierID = c.ContactID ";
            return stSQL;
        }

		#region Details

		public ProductPurchasePriceHistoryDetails Details(Int64 ProductPurchasePriceHistoryID)
		{
			try
			{
                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandType = System.Data.CommandType.Text;
				
                string SQL = SQLSelect() + "WHERE a.ProductPurchasePriceHistoryID = @ProductPurchasePriceHistoryID;";

                cmd.Parameters.AddWithValue("@ProductPurchasePriceHistoryID", ProductPurchasePriceHistoryID);

                cmd.CommandText = SQL;
                string strDataTableName = "tbl" + this.GetType().FullName.Split(new Char[] { '.' })[this.GetType().FullName.Split(new Char[] { '.' }).Length - 1]; System.Data.DataTable dt = new System.Data.DataTable(strDataTableName);
                base.MySqlDataAdapterFill(cmd, dt);

                return SetDetails(dt);
			}
			catch (Exception ex)
			{
				throw base.ThrowException(ex);
			}	
		}

		public ProductPurchasePriceHistoryDetails DetailsByProductID(Int64 ProductID)
		{
			try
			{
                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandType = System.Data.CommandType.Text;

                string SQL = SQLSelect() + "WHERE a.ProductID = @ProductID;";
				  
				cmd.Parameters.AddWithValue("@ProductID", ProductID);

                cmd.CommandText = SQL;
                string strDataTableName = "tbl" + this.GetType().FullName.Split(new Char[] { '.' })[this.GetType().FullName.Split(new Char[] { '.' }).Length - 1]; System.Data.DataTable dt = new System.Data.DataTable(strDataTableName);
                base.MySqlDataAdapterFill(cmd, dt);

                return SetDetails(dt);
			}
			catch (Exception ex)
			{
				throw base.ThrowException(ex);
			}	
		}

        private ProductPurchasePriceHistoryDetails SetDetails(System.Data.DataTable dt, Int32 BranchID = 0)
        {
            try
            {
                ProductPurchasePriceHistoryDetails Details = new ProductPurchasePriceHistoryDetails();
                Details.ProductPurchasePriceHistoryID = 0;

                foreach (System.Data.DataRow dr in dt.Rows)
                {
                    Details.ProductPurchasePriceHistoryID = Int64.Parse(dr["ProductPurchasePriceHistoryID"].ToString());
                    Details.MatrixID = Int64.Parse(dr["MatrixID"].ToString());
                    Details.SupplierID = Int64.Parse(dr["SupplierID"].ToString());
                    Details.PurchasePrice = Decimal.Parse(dr["PurchasePrice"].ToString());
                    Details.PurchaseDate = DateTime.Parse(dr["PurchaseDate"].ToString());
                    Details.Remarks = "" + dr["Remarks"].ToString();

                    Products clsProduct = new Products(base.Connection, base.Transaction);
                    Details.ProductDetails = clsProduct.Details1(BranchID, Details.ProductID);

                    Contacts clsContact = new Contacts(base.Connection, base.Transaction);
                    Details.SupplierDetails = clsContact.Details(Details.SupplierID);
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

		public System.Data.DataTable ListAsDataTable(Int64 ProductID = 0, DateTime? PurchaseDateFrom = null, string SortField = "PurchasePrice", SortOption SortOrder = SortOption.Ascending, Int32 limit = 0)
		{
			try
			{
                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandType = System.Data.CommandType.Text;
                
                string SQL = SQLSelect() + "WHERE 1=1 ";

                if (ProductID != 0)
                {
                    SQL += "AND a.ProductID = @ProductID ";
                    cmd.Parameters.AddWithValue("@ProductID", ProductID);
                }

                if (PurchaseDateFrom.GetValueOrDefault() != DateTime.MinValue)
                {
                    SQL += "AND a.PurchaseDate >= @PurchaseDateFrom ";
                    cmd.Parameters.AddWithValue("@PurchaseDateFrom", PurchaseDateFrom.GetValueOrDefault() == DateTime.MinValue ? Constants.C_DATE_MIN_VALUE : PurchaseDateFrom);
                }

                SQL += "ORDER BY " + (!string.IsNullOrEmpty(SortField) ? SortField : "PurchasePrice") + " ";
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

    }
}


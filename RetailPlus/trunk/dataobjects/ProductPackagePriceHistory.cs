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
	public struct ProductPackagePriceHistoryDetails
	{
        public Int64 UID;
		public Int64 PackageID;
        public DateTime ChangeDate;
		public decimal Price;
        public decimal WSPrice;
		public decimal PurchasePrice;
		public decimal Quantity;
		public decimal VAT;
		public decimal EVAT;
		public decimal LocalTax;
        public string Remarks;
	}

	[StrongNameIdentityPermissionAttribute(SecurityAction.LinkDemand,
		 PublicKey = "002400000480000094000000060200000024000" +
		 "052534131000400000100010053D785642F9F960B43157E0380" +
		 "F393BEE53E8DFAFBF441366C1B6F8B48D9DDF0D527B1F3B21EA" +
		 "E85D2FDB664CE81EB8A87DBE4C589D6F4202FE2B7D4B978BB69" +
		 "684874612CB9B8DB7A0339400A9C4E68277884B07817363D242" +
		 "E3696F9FACDBEA831810AE6DC9EDCA91A7B5DA12FE7BF65D113" +
		 "FF52834EAFB5A7A1FDFD5851A3")]
	public class ProductPackagePriceHistory : POSConnection
    {
		#region Constructors and Destructors

		public ProductPackagePriceHistory()
            : base(null, null)
        {
        }

        public ProductPackagePriceHistory(MySqlConnection Connection, MySqlTransaction Transaction) 
            : base(Connection, Transaction)
		{

		}

		#endregion
		
		#region Insert and Update

		public void Insert(ProductPackagePriceHistoryDetails Details)
		{
			try  
			{
                string SQL = "CALL procProductPackagePriceHistoryInsert(@UID, @PackageID, @ChangeDate, @PurchasePriceNow, @SellingPriceNow, @VAT, @EVAT, @LocalTax, @Remarks);";

                

                MySqlCommand cmd = new MySqlCommand();
                
                
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandText = SQL;

                cmd.Parameters.AddWithValue("@UID", Details.UID);
                cmd.Parameters.AddWithValue("@PackageID", Details.PackageID);
                cmd.Parameters.AddWithValue("@ChangeDate", Details.ChangeDate.ToString("yyyy-MM-dd HH:mm:ss"));
                cmd.Parameters.AddWithValue("@PurchasePriceNow", Details.PurchasePrice);
                cmd.Parameters.AddWithValue("@SellingPriceNow", Details.Price);
                cmd.Parameters.AddWithValue("@VAT", Details.VAT);
                cmd.Parameters.AddWithValue("@EVAT", Details.EVAT);
                cmd.Parameters.AddWithValue("@LocalTax", Details.LocalTax);
                cmd.Parameters.AddWithValue("@Remarks", Details.Remarks);

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
				string SQL=	"DELETE FROM tblProductPackagePriceHistory WHERE PackageID IN (" + IDs + ");";
				  
				
	 			
				MySqlCommand cmd = new MySqlCommand();
				
				
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
                                "a.PackageID, " +
                                "b.ProductID, " +
                                "ProductCode, " +
                                "ProductDesc AS Description, " +
                                "b.UnitID, " +
                                "d.UnitCode, " +
                                "d.UnitName, " +
                                "a.ChangeDate, " +
                                "b.Quantity, " +
                                "a.PurchasePriceBefore, " +
                                "a.PurchasePriceNow, " +
                                "a.SellingPriceBefore, " +
                                "a.SellingPriceNow, " +
                                "a.VATBefore, " +
                                "a.VATNow, " +
                                "a.EVATBefore, " +
                                "a.EVATNow, " +
                                "a.LocalTaxBefore, " +
                                "a.LocalTaxNow, " +
                                "a.Remarks, " +
                                "e.Name " +
                            "FROM tblProductPackagePriceHistory a " +
                            "INNER JOIN tblProductPackage b ON a.PackageID = b.PackageID " +
                            "INNER JOIN tblProducts c ON b.ProductID = c.ProductID " +
                            "INNER JOIN tblUnit d ON b.UnitID = d.UnitID " +
                            "INNER JOIN sysAccessUserDetails e ON a.UID = e.UID ";

            return stSQL;
        }

		#region Streams

        //public System.Data.DataTable List(DateTime StartChangeDate, DateTime EndChangeDate, string SortField, SortOption SortOrder)
        //{
        //    try
        //    {
        //        string SQL = SQLSelect() + "WHERE a.ChangeDate >= @StartChangeDate " +
        //                                    "AND a.ChangeDate <= @EndChangeDate " +
        //                                    "ORDER BY " + SortField;

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

        //        cmd.Parameters.AddWithValue("@StartChangeDate", StartChangeDate.ToString("yyyy-MM-dd HH:mm:ss"));
        //        cmd.Parameters.AddWithValue("@EndChangeDate", EndChangeDate.ToString("yyyy-MM-dd HH:mm:ss"));

        //        string strDataTableName = "tbl" + this.GetType().FullName.Split(new Char[] { '.' })[this.GetType().FullName.Split(new Char[] { '.' }).Length - 1]; System.Data.DataTable dt = new System.Data.DataTable(strDataTableName);
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
        //public System.Data.DataTable List(long ProductID, string SortField, SortOption SortOrder)
        //{
        //    try
        //    {
        //        string SQL = SQLSelect() + "WHERE b.ProductID = @ProductID ORDER BY " + SortField;

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

        //        cmd.Parameters.AddWithValue("@ProductID", ProductID);

        //        string strDataTableName = "tbl" + this.GetType().FullName.Split(new Char[] { '.' })[this.GetType().FullName.Split(new Char[] { '.' }).Length - 1]; System.Data.DataTable dt = new System.Data.DataTable(strDataTableName);
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
        public System.Data.DataTable List(DateTime StartChangeDate, DateTime EndChangeDate, long ProductID = 0, string SortField = "ChangeDate", SortOption SortOrder = SortOption.Ascending)
        {
            try
            {
                MySqlCommand cmd = new MySqlCommand();

                string SQL = SQLSelect() + "WHERE 1=1 ";

                if (StartChangeDate == DateTime.MinValue)
                {
                    SQL += "AND a.ChangeDate >= @StartChangeDate ";
                    cmd.Parameters.AddWithValue("@StartChangeDate", StartChangeDate.ToString("yyyy-MM-dd HH:mm:ss"));
                }
                if (EndChangeDate == DateTime.MinValue)
                {
                    SQL += "AND a.ChangeDate <= @EndChangeDate ";
                    cmd.Parameters.AddWithValue("@EndChangeDate", EndChangeDate.ToString("yyyy-MM-dd HH:mm:ss"));
                }
                if (ProductID != 0) 
                { 
                    SQL+= "AND b.ProductID = @ProductID ";
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

		#endregion

	}
}


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
                string SQL = "CALL procProductPackagePriceHistoryInsert(@UID, @PackageID, @ChangeDate, @PurchasePriceNow, @SellingPriceNow, " +
                                                                       "@Price1Now, @Price2Now, @Price3Now, @Price4Now, @Price5Now, " +
                                                                       "@VAT, @EVAT, @LocalTax, @Remarks);";

                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandText = SQL;

                cmd.Parameters.AddWithValue("@UID", Details.UID);
                cmd.Parameters.AddWithValue("@PackageID", Details.PackageID);
                cmd.Parameters.AddWithValue("@ChangeDate", Details.ChangeDate.ToString("yyyy-MM-dd HH:mm:ss"));
                cmd.Parameters.AddWithValue("@PurchasePriceNow", Details.PurchasePrice);
                cmd.Parameters.AddWithValue("@SellingPriceNow", Details.Price);
                cmd.Parameters.AddWithValue("@Price1Now", Details.Price1);
                cmd.Parameters.AddWithValue("@Price2Now", Details.Price2);
                cmd.Parameters.AddWithValue("@Price3Now", Details.Price3);
                cmd.Parameters.AddWithValue("@Price4Now", Details.Price4);
                cmd.Parameters.AddWithValue("@Price5Now", Details.Price5);
                cmd.Parameters.AddWithValue("@VAT", Details.VAT);
                cmd.Parameters.AddWithValue("@EVAT", Details.EVAT);
                cmd.Parameters.AddWithValue("@LocalTax", Details.LocalTax);
                cmd.Parameters.AddWithValue("@Remarks", Details.Remarks);

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
				string SQL=	"DELETE FROM tblProductPackagePriceHistory WHERE PackageID IN (" + IDs + ");";
				
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

		#region Streams

        public System.Data.DataTable List(DateTime StartChangeDate, DateTime EndChangeDate, long ProductID = 0, string SortField = "ChangeDate", SortOption SortOrder = SortOption.Ascending)
        {
            try
            {
                string SQL = "CALL procProductPriceHistorySelect(@StartChangeDate, @EndChangeDate, @ProductID, @SortField, @SortOrder)";

                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandText = SQL;

                cmd.Parameters.AddWithValue("@StartChangeDate", StartChangeDate.ToString("yyyy-MM-dd HH:mm:ss"));
                cmd.Parameters.AddWithValue("@EndChangeDate", EndChangeDate.ToString("yyyy-MM-dd HH:mm:ss"));
                cmd.Parameters.AddWithValue("@ProductID", ProductID);
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

		#endregion

	}
}


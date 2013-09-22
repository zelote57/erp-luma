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
    public class ProductInventories : POSConnection
    {
        #region Constructors and Destructors

		public ProductInventories()
            : base(null, null)
        {
        }

        public ProductInventories(MySqlConnection Connection, MySqlTransaction Transaction) 
            : base(Connection, Transaction)
		{

		}
		
		#endregion


        public System.Data.DataTable ListAsDataTable(int BranchID = 0, long ProductID = 0, long MatrixID =0, string BarCode = "", string ProductCode = "", Int64 ProductGroupID = 0, Int64 ProductSubGroupID = 0, Int64 SupplierID = 0, ProductListFilterType clsProductListFilterType = ProductListFilterType.ShowActiveAndInactive,
                bool isQuantityGreaterThanZERO = false, Int32 Limit = 0, Int32 isSummary = 1, string ExpirationDate = Constants.C_DATE_MIN_VALUE_STRING, string SortField = "", SortOption SortOrder = SortOption.Ascending)
        {
            DateTime dteExpiration = Constants.C_DATE_MIN_VALUE;

            DateTime.TryParse(ExpirationDate, out dteExpiration);

            string SQL = "CALL procProductInventorySelect(@BranchID, @ProductID, @MatrixID, @BarCode, @ProductCode, @ProductGroupID, @ProductSubGroupID, @SupplierID, @ShowActiveAndInactive, @isQuantityGreaterThanZERO, @lngLimit, @isSummary, @dteExpiration, @SortField, @SortOrder)";

            MySqlCommand cmd = new MySqlCommand();
            cmd.CommandType = System.Data.CommandType.Text;
            cmd.CommandText = SQL;

            cmd.Parameters.AddWithValue("@BranchID", BranchID);
            cmd.Parameters.AddWithValue("@ProductID", ProductID);
            cmd.Parameters.AddWithValue("@MatrixID", MatrixID);
            cmd.Parameters.AddWithValue("@BarCode", BarCode);
            cmd.Parameters.AddWithValue("@ProductCode", ProductCode);
            cmd.Parameters.AddWithValue("@ProductGroupID", ProductGroupID);
            cmd.Parameters.AddWithValue("@ProductSubGroupID", ProductSubGroupID);
            cmd.Parameters.AddWithValue("@SupplierID", SupplierID);
            cmd.Parameters.AddWithValue("@ShowActiveAndInactive", clsProductListFilterType.ToString("d"));
            cmd.Parameters.AddWithValue("@isQuantityGreaterThanZERO", isQuantityGreaterThanZERO);
            cmd.Parameters.AddWithValue("@lngLimit", Limit);
            cmd.Parameters.AddWithValue("@isSummary", isSummary);
            cmd.Parameters.AddWithValue("@dteExpiration", dteExpiration.ToString("yyyy-MM-dd"));
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


        /// <summary>
        /// Lemu - 06-20-2011
        /// </summary>
        /// <param name="ProductID">Put zero(0) if you want to update all products</param>
        /// <param name="Quantity"></param>
        /// <returns></returns>
        public bool UpdateActualQuantity(int BranchID, long lngProductID, long lngMatrixID, decimal decQuantity)
        {
            bool boRetValue = false;
            try
            {
                string SQL = "CALL procProductUpdateActualQuantity(@BranchID, @lngProductID, @lngMatrixID, @decQuantity);";

                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandText = SQL;

                cmd.Parameters.AddWithValue("@BranchID", BranchID);
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

        public void CloseInventoryByProductGroup(int intBranchID, long lngCloseByUserID, DateTime dteClosingDate, string strReferenceNo, long lngProductGroupID, string strProductGroupName)
        {
            try
            {
                string SQL = "CALL procCloseInventoryByProductGroup(@intBranchID, @lngUID, @dteClosingDate, @strReferenceNo, @lngProductGroupID, @strProductGroupName);";

                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandText = SQL;

                cmd.Parameters.AddWithValue("@intBranchID", intBranchID);
                cmd.Parameters.AddWithValue("@lngUID", lngCloseByUserID);
                cmd.Parameters.AddWithValue("@dteClosingDate", dteClosingDate);
                cmd.Parameters.AddWithValue("@strReferenceNo", strReferenceNo);
                cmd.Parameters.AddWithValue("@lngProductGroupID", lngProductGroupID);
                cmd.Parameters.AddWithValue("@strProductGroupName", strProductGroupName);

                base.ExecuteNonQuery(cmd);

            }

            catch (Exception ex)
            {
                throw base.ThrowException(ex);
            }
        }

        public void CloseInventoryBySupplier(int intBranchID, long lngCloseByUserID, DateTime dteClosingDate, string strReferenceNo, long lngSupplierID, string strContactCode)
        {
            try
            {
                string SQL = "CALL procCloseInventory(@intBranchID, @lngUID, @dteClosingDate, @strReferenceNo, @lngContactID, @strContactCode);";

                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandText = SQL;

                cmd.Parameters.AddWithValue("@intBranchID", intBranchID);
                cmd.Parameters.AddWithValue("@lngUID", lngCloseByUserID);
                cmd.Parameters.AddWithValue("@dteClosingDate", dteClosingDate);
                cmd.Parameters.AddWithValue("@strReferenceNo", strReferenceNo);
                cmd.Parameters.AddWithValue("@lngContactID", lngSupplierID);
                cmd.Parameters.AddWithValue("@strContactCode", strContactCode);

                base.ExecuteNonQuery(cmd);

            }

            catch (Exception ex)
            {
                throw base.ThrowException(ex);
            }
        }
    }
}

﻿using System;
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


        public System.Data.DataTable ListAsDataTable(int BranchID = 0, long ProductID = 0, string BarCode = "", string ProductCode = "", Int64 ProductGroupID = 0, Int64 ProductSubGroupID = 0, Int64 SupplierID = 0, ProductListFilterType clsProductListFilterType = ProductListFilterType.ShowActiveAndInactive,
                bool isQuantityGreaterThanZERO = false, Int32 Limit = 0, string SortField = "", SortOption SortOrder = SortOption.Ascending)
        {
            string SQL = "CALL procProductInventorySelect(@BranchID, @ProductID, @BarCode, @ProductCode, @ProductGroupID, @ProductSubGroupID, @SupplierID, @ShowActiveAndInactive, @isQuantityGreaterThanZERO, @lngLimit, @SortField, @SortOrder)";

            MySqlCommand cmd = new MySqlCommand();
            cmd.CommandType = System.Data.CommandType.Text;
            cmd.CommandText = SQL;

            cmd.Parameters.AddWithValue("@BranchID", BranchID);
            cmd.Parameters.AddWithValue("@ProductID", ProductID);
            cmd.Parameters.AddWithValue("@BarCode", BarCode);
            cmd.Parameters.AddWithValue("@ProductCode", ProductCode);
            cmd.Parameters.AddWithValue("@ProductGroupID", ProductGroupID);
            cmd.Parameters.AddWithValue("@ProductSubGroupID", ProductSubGroupID);
            cmd.Parameters.AddWithValue("@SupplierID", SupplierID);
            cmd.Parameters.AddWithValue("@ShowActiveAndInactive", clsProductListFilterType.ToString("d"));
            cmd.Parameters.AddWithValue("@isQuantityGreaterThanZERO", isQuantityGreaterThanZERO);
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


    }
}
using System;
using System.Security.Permissions;
using MySql.Data.MySqlClient;
using System.Xml;

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
    public class DBSync : POSConnection
    {
		#region Constructors and Destructors

		public DBSync()
            : base(null, null)
        {
        }

        public DBSync(MySqlConnection Connection, MySqlTransaction Transaction) 
            : base(Connection, Transaction)
		{

		}

		#endregion

        #region Streams

        public System.Data.DataTable ListAsDataTable(string TableName, DateTime StartSyncDateTime, DateTime EndSyncDateTime)
        {
            try
            {
                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                string SQL = "procTableSelectAll";

                cmd.Parameters.AddWithValue("@strTableName", TableName);
                cmd.Parameters.AddWithValue("@dteStartSyncDateTime", StartSyncDateTime);
                cmd.Parameters.AddWithValue("@dteEndSyncDateTime", EndSyncDateTime);

                cmd.CommandText = SQL;
                string strDataTableName = TableName; System.Data.DataTable dt = new System.Data.DataTable(strDataTableName);
                base.MySqlDataAdapterFill(cmd, dt);

                return dt;
            }
            catch (Exception ex)
            {
                throw base.ThrowException(ex);
            }
        }

        #endregion

        public static ProductDetails setSyncProductDetails(XmlReader xmlReader)
        {
            ProductDetails clsProductDetails = new ProductDetails();
            
            clsProductDetails.ProductID = Int64.Parse(xmlReader.GetAttribute("ProductID").ToString());
            clsProductDetails.ProductCode = xmlReader.GetAttribute("ProductCode").ToString();
            clsProductDetails.ProductDesc = xmlReader.GetAttribute("ProductDesc").ToString();
            clsProductDetails.ProductSubGroupID = Int64.Parse(xmlReader.GetAttribute("ProductSubGroupID").ToString());
            clsProductDetails.BaseUnitID = Int32.Parse(xmlReader.GetAttribute("BaseUnitID").ToString());
            clsProductDetails.DateCreated = DateTime.TryParse(xmlReader.GetAttribute("DateCreated").ToString(), out clsProductDetails.DateCreated) ? clsProductDetails.DateCreated : DateTime.Now;
            clsProductDetails.Deleted = bool.Parse(xmlReader.GetAttribute("Deleted").ToString());
            clsProductDetails.IncludeInSubtotalDiscount = bool.Parse(xmlReader.GetAttribute("IncludeInSubtotalDiscount").ToString());
            clsProductDetails.WillPrintProductComposition = bool.Parse(xmlReader.GetAttribute("WillPrintProductComposition").ToString());
            clsProductDetails.Active = bool.Parse(xmlReader.GetAttribute("Active").ToString());
            clsProductDetails.PercentageCommision = decimal.Parse(xmlReader.GetAttribute("PercentageCommision").ToString());
            clsProductDetails.RID = Int64.Parse(xmlReader.GetAttribute("RID").ToString());
            clsProductDetails.RewardPoints = decimal.Parse(xmlReader.GetAttribute("RewardPoints").ToString());
            clsProductDetails.SequenceNo = Int64.Parse(xmlReader.GetAttribute("SequenceNo").ToString());
            clsProductDetails.IsCreditChargeExcluded = bool.Parse(xmlReader.GetAttribute("IsCreditChargeExcluded").ToString());
            clsProductDetails.LastModified = DateTime.TryParse(xmlReader.GetAttribute("LastModified").ToString(), out clsProductDetails.LastModified) ? clsProductDetails.LastModified : DateTime.Now;

            clsProductDetails.MinThreshold = decimal.Parse(xmlReader.GetAttribute("MinThreshold").ToString());
            clsProductDetails.MaxThreshold = decimal.Parse(xmlReader.GetAttribute("MaxThreshold").ToString());
            clsProductDetails.SupplierID = Constants.C_RETAILPLUS_SUPPLIERID;       // 20Aug2015 : This is only for insert, do not overwrite during update sync

            return clsProductDetails;
        }

    }
}


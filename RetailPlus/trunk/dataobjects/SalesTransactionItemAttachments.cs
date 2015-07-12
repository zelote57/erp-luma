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
    public struct SalesTransactionItemAttachmentDetails
	{
        public Int64 TransactionItemAttachmentsID;
        public Int64 TransactionItemsID;
        public Int64 TransactionID;

        public string OrigFileName;
        public string FileName;
        public bool Deleted;
        public string UploadedByName;
        public string DeletedByName;
        public string LastUpdatedByName;

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
	public class SalesTransactionItemAttachments : POSConnection
	{
		#region Constructors and Destructors

		public SalesTransactionItemAttachments()
            : base(null, null)
        {
        }

        public SalesTransactionItemAttachments(MySqlConnection Connection, MySqlTransaction Transaction) 
            : base(Connection, Transaction)
		{

		}

		#endregion

		#region Insert and Update

		public Int32 Insert(SalesTransactionItemAttachmentDetails Details)
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

		public void Update(SalesTransactionItemAttachmentDetails Details)
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

        public Int32 Save(SalesTransactionItemAttachmentDetails Details)
        {
            try
            {
                string SQL = "CALL procSaveTransactionItemAttachments(@TransactionItemAttachmentsID, @TransactionItemsID, @TransactionID, @OrigFileName, @FileName, @UploadedByName, @LastUpdatedByName, @CreatedOn, @LastModified);";

                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandText = SQL;

                cmd.Parameters.AddWithValue("TransactionItemAttachmentsID", Details.TransactionItemAttachmentsID);
                cmd.Parameters.AddWithValue("TransactionItemsID", Details.TransactionItemsID);
                cmd.Parameters.AddWithValue("TransactionID", Details.TransactionID);
                cmd.Parameters.AddWithValue("OrigFileName", Details.OrigFileName);
                cmd.Parameters.AddWithValue("FileName", Details.FileName);
                cmd.Parameters.AddWithValue("UploadedByName", Details.UploadedByName);
                cmd.Parameters.AddWithValue("LastUpdatedByName", Details.LastUpdatedByName);
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
                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandType = System.Data.CommandType.Text;

				string SQL=	"UPDATE tblTransactionItemAttachments SET deleted = 1 WHERE TransactionItemAttachmentsID IN (" + IDs + ");";
				
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

		public SalesTransactionItemAttachmentDetails Details(Int32 TransactionItemAttachmentsID)
		{
			try
			{
                SalesTransactionItemAttachmentDetails clsSearchKey = new SalesTransactionItemAttachmentDetails()
                { TransactionItemAttachmentsID = TransactionItemAttachmentsID };

                System.Data.DataTable dt = ListAsDataTable(clsSearchKey);

                SalesTransactionItemAttachmentDetails Details = new SalesTransactionItemAttachmentDetails();
                foreach (System.Data.DataRow dr in dt.Rows)
                {
                    Details.TransactionItemAttachmentsID = Int64.Parse(dr["TransactionItemAttachmentsID"].ToString());
                    Details.TransactionItemsID = Int64.Parse(dr["TransactionItemsID"].ToString());
                    Details.TransactionID = Int64.Parse(dr["TransactionID"].ToString());

                    Details.OrigFileName = dr["OrigFileName"].ToString();
                    Details.FileName = dr["FileName"].ToString();
                    Details.Deleted = bool.Parse(dr["Deleted"].ToString());
                    Details.UploadedByName = dr["UploadedByName"].ToString();
                    Details.DeletedByName = dr["DeletedByName"].ToString();
                    Details.LastUpdatedByName = dr["LastUpdatedByName"].ToString();

                    Details.CreatedOn = DateTime.Parse(dr["CreatedOn"].ToString());
                    Details.LastModified = DateTime.Parse(dr["LastModified"].ToString());
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

        public System.Data.DataTable ListAsDataTable(SalesTransactionItemAttachmentDetails clsSearchKey = new SalesTransactionItemAttachmentDetails(), string SortField = "TransactionItemAttachmentsID", SortOption SortOrder = SortOption.Ascending)
		{
            MySqlCommand cmd = new MySqlCommand();
            cmd.CommandType = System.Data.CommandType.Text;

            string SQL = "SELECT TransactionItemAttachmentsID, TransactionItemsID, TransactionID, " +
                         "OrigFileName, FileName, Deleted, UploadedByName, DeletedByName, LastUpdatedByName, CreatedOn, LastModified " +
                         "FROM tblTransactionItemAttachments WHERE Deleted = 0 ";

            if (clsSearchKey.TransactionItemAttachmentsID != 0)
            {
                SQL += "AND TransactionItemAttachmentsID = @TransactionItemAttachmentsID ";
                cmd.Parameters.AddWithValue("@TransactionItemAttachmentsID", clsSearchKey.TransactionItemAttachmentsID);
            }

            if (clsSearchKey.TransactionItemsID != 0)
            {
                SQL += "AND TransactionItemsID = @TransactionItemsID ";
                cmd.Parameters.AddWithValue("@TransactionItemsID", clsSearchKey.TransactionItemsID);
            }

            if (clsSearchKey.TransactionID != 0)
            {
                SQL += "AND TransactionID = @TransactionID ";
                cmd.Parameters.AddWithValue("@TransactionID", clsSearchKey.TransactionID);
            }

            SQL += "ORDER BY " + (!string.IsNullOrEmpty(SortField) ? SortField : "TransactionItemAttachmentsID") + " ";
            SQL += SortOrder == SortOption.Ascending ? "ASC " : "DESC ";
            
            cmd.CommandText = SQL;
            string strDataTableName = "tbl" + this.GetType().FullName.Split(new Char[] { '.' })[this.GetType().FullName.Split(new Char[] { '.' }).Length - 1]; System.Data.DataTable dt = new System.Data.DataTable(strDataTableName);
            base.MySqlDataAdapterFill(cmd, dt);

            return dt;
		}

		#endregion
	}
}


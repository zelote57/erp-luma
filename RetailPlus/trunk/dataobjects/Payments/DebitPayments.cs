using System;
using System.Collections;
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
	public struct DebitPaymentDetails
	{
        public BranchDetails BranchDetails;
        public string TerminalNo;
        public Int64 TransactionID;

        public Int64 SyncID;
        public Int64 DebitPaymentID;
		public ContactDetails CustomerDetails;

        public string TransactionNo;
		public decimal Amount;
		public string Remarks;
        public bool IsRefund;

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
	public class DebitPayments : POSConnection
    {
		#region Constructors and Destructors

		public DebitPayments()
            : base(null, null)
        {
        }

        public DebitPayments(MySqlConnection Connection, MySqlTransaction Transaction) 
            : base(Connection, Transaction)
		{

		}

		#endregion

		#region Insert and Update

		public Int64 Insert(DebitPaymentDetails Details)
		{
			try  
			{
                Contacts clsContact = new Contacts(base.Connection, base.Transaction);
                clsContact.SubtractDebit(Details.CustomerDetails.ContactID, Details.Amount);

                Save(Details);

                return Int64.Parse(base.getLAST_INSERT_ID(this));
			}
			catch (Exception ex)
			{
				throw base.ThrowException(ex);
			}	
		}

        public Int32 Save(DebitPaymentDetails Details)
        {
            try
            {
                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandType = System.Data.CommandType.Text;

                string SQL = "CALL procSaveDebitPayment(@BranchID, @TerminalNo, @SyncID, @DebitPaymentID, @TransactionID, @ContactID, @Amount, @Remarks, @TransactionNo, @CreatedOn, @LastModified);";

                cmd.Parameters.AddWithValue("BranchID", Details.BranchDetails.BranchID);
                cmd.Parameters.AddWithValue("TerminalNo", Details.TerminalNo);
                cmd.Parameters.AddWithValue("SyncID", Details.SyncID);
                cmd.Parameters.AddWithValue("DebitPaymentID", Details.DebitPaymentID);
                cmd.Parameters.AddWithValue("TransactionID", Details.TransactionID);
                cmd.Parameters.AddWithValue("ContactID", Details.CustomerDetails.ContactID);
                cmd.Parameters.AddWithValue("Amount", Details.Amount);
                cmd.Parameters.AddWithValue("Remarks", Details.Remarks);
                cmd.Parameters.AddWithValue("TransactionNo", Details.TransactionNo);
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

                string SQL = "DELETE FROM tblDebitPayment WHERE DebitPaymentID IN (" + IDs + ");";
	 			
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
            string stSQL = "SELECT BranchID, TerminalNo, SyncID, DebitPaymentID, TransactionID, ContactID, Amount, Remarks, TransactionNo, CreatedOn, LastModified FROM tblDebitPayment ";

            return stSQL;
        }

		#region Details

        /// <summary>
        /// Get the cash payment details of a certain transaction.
        /// </summary>
        /// <param name="BranchID"></param>
        /// <param name="TerminalNo"></param>
        /// <param name="SyncID">Same also as the TransactionID</param>
        /// <returns></returns>
        public DebitPaymentDetails[] Details(Int32 BranchID, string TerminalNo, Int64 TransactionID)
        {
            try
            {
                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandType = System.Data.CommandType.Text;

                string SQL = SQLSelect() + "WHERE BranchID = @BranchID AND TerminalNo = @TerminalNo AND TransactionID = @TransactionID;";

                cmd.Parameters.AddWithValue("BranchID", BranchID);
                cmd.Parameters.AddWithValue("TerminalNo", TerminalNo);
                cmd.Parameters.AddWithValue("TransactionID", TransactionID);

                cmd.CommandText = SQL;
                string strDataTableName = "tbl" + this.GetType().FullName.Split(new Char[] { '.' })[this.GetType().FullName.Split(new Char[] { '.' }).Length - 1]; System.Data.DataTable dt = new System.Data.DataTable(strDataTableName);
                base.MySqlDataAdapterFill(cmd, dt);

                ArrayList items = new ArrayList();
                foreach (System.Data.DataRow dr in dt.Rows)
                {
                    items.Add(setDetails(dr));
                }

                DebitPaymentDetails[] arrCashDetails = new DebitPaymentDetails[0];

                if (items != null)
                {
                    arrCashDetails = new DebitPaymentDetails[items.Count];
                    items.CopyTo(arrCashDetails);
                }

                return arrCashDetails;
            }

            catch (Exception ex)
            {
                throw base.ThrowException(ex);
            }
        }

        public DebitPaymentDetails setDetails(System.Data.DataRow dr)
        {
            Data.DebitPaymentDetails Details = new Data.DebitPaymentDetails();

            Details.BranchDetails = new Branch(base.Connection, base.Transaction).Details(Int32.Parse(dr["BranchID"].ToString()));
            Details.TerminalNo = dr["TerminalNo"].ToString();
            Details.SyncID = Int64.Parse(dr["SyncID"].ToString());
            Details.DebitPaymentID = Int64.Parse(dr["DebitPaymentID"].ToString());
            Details.TransactionID = Int64.Parse(dr["TransactionID"].ToString());
            Details.Amount = decimal.Parse(dr["Amount"].ToString());
            Details.Remarks = dr["Remarks"].ToString();
            Details.TransactionNo = dr["TransactionNo"].ToString();

            Details.CreatedOn = DateTime.Parse(dr["CreatedOn"].ToString());
            Details.LastModified = DateTime.Parse(dr["LastModified"].ToString());

            Details.CustomerDetails = new Customer(this.Connection, this.Transaction).Details(Int64.Parse(dr["ContactID"].ToString()));

            return Details;
        }

		#endregion
		
		#region Streams


		#endregion
	}
}


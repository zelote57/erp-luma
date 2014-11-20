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
	public struct CreditPaymentChequeDetails
	{
        public BranchDetails BranchDetails;
        public string TerminalNo;
        public Int64 SyncID;
        public Int64 CreditPaymentChequeID;
        public Int64 CreditPaymentID;
        public Int32 CPRefBranchID;
        public string CPRefTerminalNo;
        public Int64 TransactionID;
        public string TransactionNo;
        public string ChequeNo;
		public decimal Amount;
        public DateTime ValidityDate;
		public string Remarks;

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
	public class CreditPaymentCheque : POSConnection
    {
		#region Constructors and Destructors

		public CreditPaymentCheque()
            : base(null, null)
        {
        }

        public CreditPaymentCheque(MySqlConnection Connection, MySqlTransaction Transaction) 
            : base(Connection, Transaction)
		{

		}

		#endregion

		#region Insert and Update

		public Int64 Insert(CreditPaymentChequeDetails Details)
		{
			try  
			{
                Save(Details);

                return Int64.Parse(base.getLAST_INSERT_ID(this));
			}
			catch (Exception ex)
			{
				throw base.ThrowException(ex);
			}	
		}

        public Int32 Save(CreditPaymentChequeDetails Details)
        {
            try
            {
                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandType = System.Data.CommandType.Text;

                string SQL = "CALL procSaveCreditPaymentCheque(@BranchID, @TerminalNo, @SyncID, @CreditPaymentChequeID, @CreditPaymentID, @CPRefBranchID, @CPRefTerminalNo, @TransactionID, @ChequeNo, @Amount, @ValidityDate, @Remarks, @TransactionNo, @CreatedOn, @LastModified);";

                cmd.Parameters.AddWithValue("BranchID", Details.BranchDetails.BranchID);
                cmd.Parameters.AddWithValue("TerminalNo", Details.TerminalNo);
                cmd.Parameters.AddWithValue("SyncID", Details.SyncID);
                cmd.Parameters.AddWithValue("CreditPaymentChequeID", Details.CreditPaymentChequeID);
                cmd.Parameters.AddWithValue("CreditPaymentID", Details.CreditPaymentID);
                cmd.Parameters.AddWithValue("CPRefBranchID", Details.CPRefBranchID);
                cmd.Parameters.AddWithValue("CPRefTerminalNo", Details.CPRefTerminalNo);
                cmd.Parameters.AddWithValue("TransactionID", Details.TransactionID);
                cmd.Parameters.AddWithValue("ChequeNo", Details.ChequeNo);
                cmd.Parameters.AddWithValue("Amount", Details.Amount);
                cmd.Parameters.AddWithValue("ValidityDate", Details.ValidityDate);
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

                string SQL = "DELETE FROM tblCreditPaymentCheque WHERE PaymentID IN (" + IDs + ");";
	 			
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
            string stSQL = "SELECT BranchID, TerminalNo,SyncID, CreditPaymentChequeID, CreditPaymentID, CPRefBranchID, CPRefTerminalNo, TransactionID, ChequeNo, Amount, ValidityDate, Remarks, TransactionNo, CreatedOn, LastModified " +
                            "FROM tblCreditPaymentCheque ";

            return stSQL;
        }

		#region Details

        /// <summary>
        /// Get the Cheque Payment details of a certain Transaction.
        /// </summary>
        /// <param name="BranchID"></param>
        /// <param name="TerminalNo"></param>
        /// <param name="SyncID">sabme as the TransactionID</param>
        /// <returns></returns>
        public CreditPaymentChequeDetails[] Details(Int32 BranchID, string TerminalNo, Int64 TransactionID)
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

                CreditPaymentChequeDetails[] arrCashDetails = new CreditPaymentChequeDetails[0];

                if (items != null)
                {
                    arrCashDetails = new CreditPaymentChequeDetails[items.Count];
                    items.CopyTo(arrCashDetails);
                }

                return arrCashDetails;
            }

            catch (Exception ex)
            {
                throw base.ThrowException(ex);
            }
        }

        public CreditPaymentChequeDetails setDetails(System.Data.DataRow dr)
        {
            Data.CreditPaymentChequeDetails Details = new Data.CreditPaymentChequeDetails();

            Details.BranchDetails = new Branch(base.Connection, base.Transaction).Details(Int32.Parse(dr["BranchID"].ToString()));
            Details.TerminalNo = dr["TerminalNo"].ToString();
            Details.SyncID = Int64.Parse(dr["SyncID"].ToString());
            Details.CreditPaymentChequeID = Int64.Parse(dr["CreditPaymentChequeID"].ToString());
            Details.TransactionID = Int64.Parse(dr["TransactionID"].ToString());
            Details.Amount = decimal.Parse(dr["Amount"].ToString());
            Details.Remarks = dr["Remarks"].ToString();
            Details.TransactionNo = dr["TransactionNo"].ToString();

            Details.CreatedOn = DateTime.Parse(dr["CreatedOn"].ToString());
            Details.LastModified = DateTime.Parse(dr["LastModified"].ToString());

            return Details;
        }

		#endregion
		
		#region Streams


		#endregion
	}
}


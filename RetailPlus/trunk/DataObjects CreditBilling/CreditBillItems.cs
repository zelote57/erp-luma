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
	public struct CreditBillItemDetails
	{
		public Int64 CreditBillDetailID;
        public Int64 CreditBillHeaderID;
        public DateTime TransactionDate;
        public string Description;
        public decimal Amount;
        public int TransactionTypeID;
        public Int64 TransactionRefID;
        public string TerminalNoRefID;
        public Int32 BranchIDRefID;
	}

	[StrongNameIdentityPermissionAttribute(SecurityAction.LinkDemand,
		 PublicKey = "002400000480000094000000060200000024000" +
		 "052534131000400000100010053D785642F9F960B43157E0380" +
		 "F393BEE53E8DFAFBF441366C1B6F8B48D9DDF0D527B1F3B21EA" +
		 "E85D2FDB664CE81EB8A87DBE4C589D6F4202FE2B7D4B978BB69" +
		 "684874612CB9B8DB7A0339400A9C4E68277884B07817363D242" +
		 "E3696F9FACDBEA831810AE6DC9EDCA91A7B5DA12FE7BF65D113" +
		 "FF52834EAFB5A7A1FDFD5851A3")]
	public class CreditBillItems : POSConnection
    {
		#region Constructors and Destructors

		public CreditBillItems()
            : base(null, null)
        {
        }

        public CreditBillItems(MySqlConnection Connection, MySqlTransaction Transaction) 
            : base(Connection, Transaction)
		{

		}

		#endregion


        private string SQLSelect()
        {
            string stSQL = "SELECT " +
					            "CreditBillDetailID, CreditBillHeaderID, TransactionDate, Description, Amount, " +
                                "TransactionTypeID, TransactionRefID, TerminalNoRefID, BranchIDRefID " +
					        "FROM tblCreditBillDetails ";

            return stSQL;
        }

        public Int64 Insert(CreditBillItemDetails Details)
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
        public Int32 Save(CreditBillItemDetails Details)
        {
            try
            {
                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandType = System.Data.CommandType.Text;

                string SQL = "INSERT INTO tblCreditBillDetailss(CreditBillHeaderID, TransactionDate, Description, Amount, " +
                                "TransactionTypeID, TransactionRefID, TerminalNoRefID, BranchIDRefID)VALUES(" +
                                "@CreditBillHeaderID, @TransactionDate, @Description, @Amount, " +
                                "@TransactionTypeID, @TransactionRefID, @TerminalNoRefID, @BranchIDRefID)";

                cmd.Parameters.AddWithValue("CreditBillHeaderID", Details.CreditBillHeaderID);
                cmd.Parameters.AddWithValue("TransactionDate", Details.TransactionDate);
                cmd.Parameters.AddWithValue("Description", Details.Description);
                cmd.Parameters.AddWithValue("Amount", Details.Amount);
                cmd.Parameters.AddWithValue("TransactionTypeID", Details.TransactionTypeID);
                cmd.Parameters.AddWithValue("TransactionRefID", Details.TransactionRefID);
                cmd.Parameters.AddWithValue("TerminalNoRefID", Details.TerminalNoRefID);
                cmd.Parameters.AddWithValue("BranchIDRefID", Details.BranchIDRefID);

                cmd.CommandText = SQL;
                return base.ExecuteNonQuery(cmd);
            }
            catch (Exception ex)
            {
                throw base.ThrowException(ex);
            }
        }

		#region Details

		public CreditBillItemDetails Details(Int64 CreditBillItemID)
		{
			try
			{
                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandType = System.Data.CommandType.Text;

                string SQL = SQLSelect() + "WHERE CreditBillItemID = @CreditBillItemID;";

                cmd.Parameters.AddWithValue("@CreditBillItemID", CreditBillItemID);

                cmd.CommandText = SQL;
                string strDataTableName = "tbl" + this.GetType().FullName.Split(new Char[] { '.' })[this.GetType().FullName.Split(new Char[] { '.' }).Length - 1]; System.Data.DataTable dt = new System.Data.DataTable(strDataTableName);
                base.MySqlDataAdapterFill(cmd, dt);

                CreditBillItemDetails Details = setDetails(dt);

                return Details;
			}
			catch (Exception ex)
			{
				throw base.ThrowException(ex);
			}	
		}

        public CreditBillItemDetails[] Details(Int64 ContactID, Int64 CreditBillHeaderID)
        {
            try
            {
                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandType = System.Data.CommandType.Text;

                string SQL = SQLSelect() + "WHERE AND ContactID = @ContactID AND CreditBillHeaderID = @CreditBillHeaderID ";

                cmd.Parameters.AddWithValue("@ContactID", ContactID);
                cmd.Parameters.AddWithValue("@CreditBillHeaderID", CreditBillHeaderID);

                cmd.CommandText = SQL;
                string strDataTableName = "tbl" + this.GetType().FullName.Split(new Char[] { '.' })[this.GetType().FullName.Split(new Char[] { '.' }).Length - 1]; System.Data.DataTable dt = new System.Data.DataTable(strDataTableName);
                base.MySqlDataAdapterFill(cmd, dt);

                CreditBillItemDetails[] Details = setArrayDetails(dt);

                return Details;
            }
            catch (Exception ex)
            {
                throw base.ThrowException(ex);
            }
        }

        public CreditBillItemDetails setDetails(System.Data.DataTable dt)
        {
            CreditBillItemDetails Details = new CreditBillItemDetails();
            foreach (System.Data.DataRow dr in dt.Rows)
            {
                Details.CreditBillDetailID = Int64.Parse(dr["CreditBillDetailID"].ToString());
                Details.CreditBillHeaderID = Int64.Parse(dr["CreditBillHeaderID"].ToString());
                Details.TransactionDate = DateTime.Parse(dr["TransactionDate"].ToString());
                Details.Description = dr["Description"].ToString();
                Details.Amount = decimal.Parse(dr["Amount"].ToString());
                Details.TransactionTypeID = Int32.Parse(dr["TransactionTypeID"].ToString());
                Details.TransactionRefID = Int64.Parse(dr["TransactionRefID"].ToString());
                Details.TerminalNoRefID = dr["TerminalNoRefID"].ToString();
                Details.BranchIDRefID = Int32.Parse(dr["BranchIDRefID"].ToString());
            }
            return Details;
        }

        public CreditBillItemDetails[] setArrayDetails(System.Data.DataTable dt)
        {
            System.Collections.ArrayList arrItems = new System.Collections.ArrayList();
            CreditBillItemDetails Details = new CreditBillItemDetails();
            foreach (System.Data.DataRow dr in dt.Rows)
            {
                Details = new CreditBillItemDetails();
                Details.CreditBillDetailID = Int64.Parse(dr["CreditBillDetailID"].ToString());
                Details.CreditBillHeaderID = Int64.Parse(dr["CreditBillHeaderID"].ToString());
                Details.TransactionDate = DateTime.Parse(dr["TransactionDate"].ToString());
                Details.Description = dr["Description"].ToString();
                Details.Amount = decimal.Parse(dr["Amount"].ToString());
                Details.TransactionTypeID = Int32.Parse(dr["TransactionTypeID"].ToString());
                Details.TransactionRefID = Int64.Parse(dr["TransactionRefID"].ToString());
                Details.TerminalNoRefID = dr["TerminalNoRefID"].ToString();
                Details.BranchIDRefID = Int32.Parse(dr["BranchIDRefID"].ToString());

                arrItems.Add(Details);
            }

            CreditBillItemDetails[] retDetails = new Data.CreditBillItemDetails[0];
            if (arrItems.Count > 0)
            {
                retDetails = new Data.CreditBillItemDetails[arrItems.Count];
                arrItems.CopyTo(retDetails);
            }

            return retDetails;
        }

		#endregion

	}
}


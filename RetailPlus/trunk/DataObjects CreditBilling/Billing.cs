using System;
using System.Security.Permissions;
using MySql.Data.MySqlClient;
using System.Collections;
using System.Collections.Generic;
using System.Data;

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

    #region BillingDetails

    public struct BillingDetails
    {
        public long CreditBillHeaderID;
        public long ContactID;
        public decimal CrediLimit;
        public decimal RunningCreditAmt;
        public decimal CurrMonthCreditAmt;
        public decimal CurrMonthAmountPaid;
        public decimal TotalBillCharges;
        public decimal CurrentDueAmount;
        public decimal MinimumAmountDue;

        public decimal Prev1MoCurrentDueAmount;
        public decimal Prev1MoMinimumAmountDue;
        public decimal Prev1MoCurrMonthAmountPaid;
        public decimal Prev2MoCurrentDueAmount;

        public DateTime CreditCutOffDate;
        public DateTime BillingDate;

        public Data.ContactDetails CustomerDetails;
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
    public class Billing : POSConnection
    {
        #region Constructors and Destructors

		public Billing()
            : base(null, null)
        {
        }

        public Billing(MySqlConnection Connection, MySqlTransaction Transaction) 
            : base(Connection, Transaction)
		{

		}

		#endregion

        #region Insert and Update

        public void SetBillinAsPrinted(long ContactID, DateTime BillingDate, string BillingFile)
        {
            try
            {
                string SQL = "UPDATE tblCreditBillHeader SET " +
                                "IsBillPrinted = 1, BillingFile = @BillingFile " +
                            "WHERE ContactID = @ContactID AND BillingDate = @BillingDate;";

                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandText = SQL;

                MySqlParameter prmBillingFile = new MySqlParameter("@BillingFile", MySqlDbType.String);
                prmBillingFile.Value = BillingFile;
                cmd.Parameters.Add(prmBillingFile);

                MySqlParameter prmContactID = new MySqlParameter("@ContactID", MySqlDbType.Int64);
                prmContactID.Value = ContactID;
                cmd.Parameters.Add(prmContactID);

                MySqlParameter prmBillingDate = new MySqlParameter("@BillingDate", MySqlDbType.Date);
                prmBillingDate.Value = BillingDate.ToString("yyyy-MM-dd");
                cmd.Parameters.Add(prmBillingDate);

                base.ExecuteNonQuery(cmd);
            }

            catch (Exception ex)
            {
                throw ex;
            }
        }
       
        #endregion

        #region Delete

        
        #endregion

        private string SQLSelect()
        {
            string stSQL = "SELECT CreditBillHeaderID ,CBH.CreditBillID ,ContactID ,CreditLimit ,RunningCreditAmt ,CurrMonthCreditAmt ,CurrMonthAmountPaid ,TotalBillCharges ,CurrentDueAmount ,MinimumAmountDue ,Prev1MoCurrentDueAmount ,Prev1MoMinimumAmountDue ,Prev1MoCurrMonthAmountPaid ,Prev2MoCurrentDueAmount ,CBL.BillingDate ,CreditCutOffDate " +
                            "FROM tblCreditBillHeader CBH " +
                            "INNER JOIN tblCreditBills CBL ON CBH.CreditBillID = CBL.CreditBillID " +
                            "WHERE IsBillPrinted = 0 AND CBL.BillingDate = (SELECT MAX(BillingDate) FROM tblCreditBills) ";

            return stSQL;
        }

        #region Details

        public BillingDetails Details(long CustomerID)
        {
            try
            {
                string SQL = SQLSelect() + "WHERE tblContacts.ContactID = @CustomerID;";

                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandText = SQL;

                cmd.Parameters.AddWithValue("@CustomerID", CustomerID);

                MySqlDataReader myReader = base.ExecuteReader(cmd, System.Data.CommandBehavior.SingleResult);

                BillingDetails Details = new BillingDetails();

                while (myReader.Read())
                {
                    Details.ContactID = myReader.GetInt64("ContactID");
                    Details.CrediLimit = myReader.GetDecimal("CreditLimit");
                    Details.RunningCreditAmt = myReader.GetDecimal("RunningCreditAmt");
                    Details.CurrMonthCreditAmt = myReader.GetDecimal("CurrMonthCreditAmt");
                    Details.CurrMonthAmountPaid = myReader.GetDecimal("CurrMonthAmountPaid");
                    Details.TotalBillCharges = myReader.GetDecimal("TotalBillCharges");
                    Details.CurrentDueAmount = myReader.GetDecimal("CurrentDueAmount");
                    Details.MinimumAmountDue = myReader.GetDecimal("MinimumAmountDue");

                    Details.Prev1MoCurrentDueAmount = myReader.GetDecimal("Prev1MoCurrentDueAmount");
                    Details.Prev1MoMinimumAmountDue = myReader.GetDecimal("Prev1MoMinimumAmountDue");
                    Details.Prev1MoCurrMonthAmountPaid = myReader.GetDecimal("Prev1MoCurrMonthAmountPaid");
                    Details.Prev2MoCurrentDueAmount = myReader.GetDecimal("Prev2MoCurrentDueAmount");

                    Details.CreditCutOffDate = myReader.GetDateTime("CreditCutOffDate");
                    Details.BillingDate = myReader.GetDateTime("BillingDate");

                    Customer clsCustomer = new Customer(base.Connection, base.Transaction);
                    Details.CustomerDetails = clsCustomer.Details(Details.ContactID);
                }

                myReader.Close();

                return Details;
            }

            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DateTime getCreditPurcEndDateToProcess()
        {
            try
            {
                string SQL = "SELECT ConfigValue FROM sysCreditConfig WHERE ConfigName = 'CreditPurcEndDateToProcess'";

                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandText = SQL;

                System.Data.DataTable dt = new System.Data.DataTable("Billing");
                base.MySqlDataAdapterFill(cmd, dt);

                DateTime dteRetValue = DateTime.MaxValue;

                foreach(System.Data.DataRow dr in dt.Rows)
                {
                    dteRetValue = DateTime.Parse(dr["ConfigValue"].ToString());
                }

                return dteRetValue;
            }

            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DateTime getBillingDate()
        {
            try
            {
                string SQL = "SELECT ConfigValue FROM sysCreditConfig WHERE ConfigName = 'BillingDate'";

                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandText = SQL;

                System.Data.DataTable dt = new System.Data.DataTable("Billing");
                base.MySqlDataAdapterFill(cmd, dt);

                DateTime dteRetValue = DateTime.MinValue;

                foreach (System.Data.DataRow dr in dt.Rows)
                {
                    dteRetValue = DateTime.Parse(dr["ConfigValue"].ToString());
                }

                return dteRetValue;
            }

            catch (Exception ex)
            {
                throw ex;
            }
        }

        private BillingDetails setDetails(System.Data.DataRow dr)
        {
            try
            {
                BillingDetails Details = new BillingDetails();

                Details.CreditBillHeaderID = Convert.ToInt64(dr["CreditBillHeaderID"]);
                Details.ContactID = Convert.ToInt64(dr["ContactID"]);
                Details.CrediLimit = Convert.ToDecimal(dr["CreditLimit"]);
                Details.RunningCreditAmt = Convert.ToDecimal(dr["RunningCreditAmt"]);
                Details.CurrMonthCreditAmt = Convert.ToDecimal(dr["CurrMonthCreditAmt"]);
                Details.CurrMonthAmountPaid = Convert.ToDecimal(dr["CurrMonthAmountPaid"]);
                Details.TotalBillCharges = Convert.ToDecimal(dr["TotalBillCharges"]);
                Details.CurrentDueAmount = Convert.ToDecimal(dr["CurrentDueAmount"]);
                Details.MinimumAmountDue = Convert.ToDecimal(dr["MinimumAmountDue"]);

                Details.Prev1MoCurrentDueAmount = Convert.ToDecimal(dr["Prev1MoCurrentDueAmount"]);
                Details.Prev1MoMinimumAmountDue = Convert.ToDecimal(dr["Prev1MoMinimumAmountDue"]);
                Details.Prev1MoCurrMonthAmountPaid = Convert.ToDecimal(dr["Prev1MoCurrMonthAmountPaid"]);
                Details.Prev2MoCurrentDueAmount = Convert.ToDecimal(dr["Prev2MoCurrentDueAmount"]);

                Details.CreditCutOffDate = Convert.ToDateTime(dr["CreditCutOffDate"]);
                Details.BillingDate = Convert.ToDateTime(dr["BillingDate"]);

                Customer clsCustomer = new Customer(base.Connection, base.Transaction);
                Details.CustomerDetails = clsCustomer.Details(Details.ContactID);

                return Details;
            }

            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #region Streams

        public System.Data.DataTable ListAsDataTable(Int64 ContactID = 0, string SortField = "ContactName", System.Data.SqlClient.SortOrder SortOrder = System.Data.SqlClient.SortOrder.Ascending)
        {
            string SQL = SQLSelect();
            
            if (ContactID !=0 ) 
                SQL += "WHERE tblContacts.ContactID = @ContactID ";

            SQL += "ORDER BY " + SortField;

            if (SortOrder == System.Data.SqlClient.SortOrder.Ascending)
                SQL += " ASC";
            else
                SQL += " DESC";

            MySqlCommand cmd = new MySqlCommand();
            cmd.CommandType = System.Data.CommandType.Text;
            cmd.CommandText = SQL;

            System.Data.DataTable dt = new System.Data.DataTable("CreditBillHeaders");
            base.MySqlDataAdapterFill(cmd, dt);

            return dt;
        }

        public System.Data.DataTable ListDetailsAsDataTable(Int64 CreditBillHeaderID = 0, string SortField = "TransactionDate", System.Data.SqlClient.SortOrder SortOrder = System.Data.SqlClient.SortOrder.Ascending)
        {
            string SQL = "SELECT CreditBillHeaderID ,TransactionDate ,Description ,Amount FROM tblCreditBillDetail ";

            if (CreditBillHeaderID != 0)
                SQL += "WHERE CreditBillHeaderID = @CreditBillHeaderID ";

            SQL += "ORDER BY " + SortField;

            if (SortOrder == System.Data.SqlClient.SortOrder.Ascending)
                SQL += " ASC";
            else
                SQL += " DESC";

            MySqlCommand cmd = new MySqlCommand();
            cmd.CommandType = System.Data.CommandType.Text;
            cmd.CommandText = SQL;

            cmd.Parameters.AddWithValue("@CreditBillHeaderID", CreditBillHeaderID);

            System.Data.DataTable dt = new System.Data.DataTable("CreditBillDetails");
            base.MySqlDataAdapterFill(cmd, dt);

            return dt;
        }

        public System.Data.DataTable ListBillingDateAsDataTable(Int64 CustomerID, string SortField = "BillingDate", System.Data.SqlClient.SortOrder SortOrder = System.Data.SqlClient.SortOrder.Descending)
        {
            string SQL = "SELECT DISTINCT DATE_FORMAT(BillingDate, '%Y-%m-%d') BillingDate, BillingFile FROM tblCreditBillHeader ";

            if (CustomerID != 0)
                SQL += "WHERE ContactID = @CustomerID ";

            SQL += "ORDER BY " + SortField;

            if (SortOrder == System.Data.SqlClient.SortOrder.Ascending)
                SQL += " ASC";
            else
                SQL += " DESC";

            MySqlCommand cmd = new MySqlCommand();
            cmd.CommandType = System.Data.CommandType.Text;
            cmd.CommandText = SQL;

            cmd.Parameters.AddWithValue("@CustomerID", CustomerID);

            System.Data.DataTable dt = new System.Data.DataTable("tblBillings");
            base.MySqlDataAdapterFill(cmd, dt);

            return dt;
        }

        public List<BillingDetails> List(Int64 ContactID = 0, string SortField = "ContactID", System.Data.SqlClient.SortOrder SortOrder = System.Data.SqlClient.SortOrder.Ascending)
        {
            string SQL = SQLSelect();

            if (ContactID != 0)
                SQL += "AND ContactID = @ContactID ";

            SQL += "ORDER BY " + SortField;

            if (SortOrder == System.Data.SqlClient.SortOrder.Ascending)
                SQL += " ASC";
            else
                SQL += " DESC";

            MySqlCommand cmd = new MySqlCommand();
            cmd.CommandType = System.Data.CommandType.Text;
            cmd.CommandText = SQL;

            System.Data.DataTable dt = new System.Data.DataTable("Billings");
            base.MySqlDataAdapterFill(cmd, dt);

            List<BillingDetails> lstRetValue = new List<BillingDetails>();
            foreach (DataRow dr in dt.Rows)
            {
                lstRetValue.Add(setDetails(dr));
            }
            
            return lstRetValue;
        }

        #endregion

        public void ProcessCurrentBill()
        {
            try
            {
                string SQL = "CALL procProcessCreditBills();";

                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandText = SQL;

                base.ExecuteNonQuery(cmd);

            }

            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void CloseCurrentBill()
        {
            try
            {
                string SQL = "CALL procProcessCreditBillsClose();";

                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandText = SQL;

                base.ExecuteNonQuery(cmd);

            }

            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}

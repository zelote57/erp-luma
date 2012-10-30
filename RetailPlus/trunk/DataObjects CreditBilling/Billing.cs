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
    public class Billing
    {
        MySqlConnection mConnection;
        MySqlTransaction mTransaction;
        bool IsInTransaction = false;
        bool TransactionFailed = false;

        public MySqlConnection Connection
        {
            get { return mConnection; }
        }

        public MySqlTransaction Transaction
        {
            get { return mTransaction; }
        }


        #region Constructors and Destructors

        public Billing()
        {

        }

        public Billing(MySqlConnection Connection, MySqlTransaction Transaction)
        {
            mConnection = Connection;
            mTransaction = Transaction;

        }

        public void CommitAndDispose()
        {
            if (!TransactionFailed)
            {
                if (IsInTransaction)
                {
                    mTransaction.Commit();
                    mTransaction.Dispose();
                    mConnection.Close();
                    mConnection.Dispose();
                }
            }
        }

        public MySqlConnection GetConnection()
        {
            if (mConnection == null)
            {
                mConnection = new MySqlConnection(AceSoft.RetailPlus.DBConnection.ConnectionString());
                mConnection.Open();

                mTransaction = (MySqlTransaction)mConnection.BeginTransaction();
            }

            IsInTransaction = true;
            return mConnection;
        }


        #endregion

        #region Insert and Update

       
        #endregion

        #region Delete

        
        #endregion

        private string SQLSelect()
        {
            string stSQL = "SELECT CreditBillHeaderID ,CBH.CreditBillID ,ContactID ,CreditLimit ,RunningCreditAmt ,CurrMonthCreditAmt ,CurrMonthAmountPaid ,TotalBillCharges ,CurrentDueAmount ,MinimumAmountDue ,Prev1MoCurrentDueAmount ,Prev1MoMinimumAmountDue ,Prev1MoCurrMonthAmountPaid ,Prev2MoCurrentDueAmount ,CreditCutOffDate BillingDate ,CreditCutOffDate " +
                            "FROM tblCreditBillHeader CBH " +
                            "INNER JOIN tblCreditBills CBL ON CBH.CreditBillID = CBL.CreditBillID " +
                            "WHERE IsBillPrinted = 0 AND CreditCutOffDate = (SELECT MAX(CreditCutOffDate) FROM tblCreditBills) ";

            return stSQL;
        }

        #region Details

        public BillingDetails Details(long CustomerID)
        {
            try
            {
                string SQL = SQLSelect() + "WHERE tblContacts.ContactID = @CustomerID;";

                MySqlConnection cn = GetConnection();

                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = cn;
                cmd.Transaction = mTransaction;
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandText = SQL;

                cmd.Parameters.AddWithValue("@CustomerID", CustomerID);

                MySqlDataReader myReader = (MySqlDataReader)cmd.ExecuteReader(System.Data.CommandBehavior.SingleResult);

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

                    Details.BillingDate = myReader.GetDateTime("BillingDate");

                    Customer clsCustomer = new Customer(mConnection, mTransaction);
                    Details.CustomerDetails = clsCustomer.Details(Details.ContactID);
                }

                myReader.Close();

                return Details;
            }

            catch (Exception ex)
            {
                TransactionFailed = true;
                if (IsInTransaction)
                {
                    mTransaction.Rollback();
                    mTransaction.Dispose();
                    mConnection.Close();
                    mConnection.Dispose();
                }

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

                Details.BillingDate = Convert.ToDateTime(dr["BillingDate"]);

                Customer clsCustomer = new Customer(mConnection, mTransaction);
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

            MySqlConnection cn = GetConnection();

            MySqlCommand cmd = new MySqlCommand();
            cmd.Connection = cn;
            cmd.Transaction = mTransaction;
            cmd.CommandType = System.Data.CommandType.Text;
            cmd.CommandText = SQL;

            System.Data.DataTable dt = new System.Data.DataTable("CreditBillHeader");
            MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
            adapter.Fill(dt);

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

            MySqlConnection cn = GetConnection();

            MySqlCommand cmd = new MySqlCommand();
            cmd.Connection = cn;
            cmd.Transaction = mTransaction;
            cmd.CommandType = System.Data.CommandType.Text;
            cmd.CommandText = SQL;

            cmd.Parameters.AddWithValue("@CreditBillHeaderID", CreditBillHeaderID);

            System.Data.DataTable dt = new System.Data.DataTable("CreditBillDetail");
            MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
            adapter.Fill(dt);

            return dt;
        }

        public List<BillingDetails> List(Int64 ContactID = 0, string SortField = "ContactID", System.Data.SqlClient.SortOrder SortOrder = System.Data.SqlClient.SortOrder.Ascending)
        {
            string SQL = SQLSelect();

            if (ContactID != 0)
                SQL += "WHERE ContactID = @ContactID ";

            SQL += "ORDER BY " + SortField;

            if (SortOrder == System.Data.SqlClient.SortOrder.Ascending)
                SQL += " ASC";
            else
                SQL += " DESC";

            MySqlConnection cn = GetConnection();

            MySqlCommand cmd = new MySqlCommand();
            cmd.Connection = cn;
            cmd.Transaction = mTransaction;
            cmd.CommandType = System.Data.CommandType.Text;
            cmd.CommandText = SQL;

            System.Data.DataTable dt = new System.Data.DataTable("Billing");
            MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
            adapter.Fill(dt);

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
            // Added August 2, 2009 to monitor if product still has/have variations
            try
            {
                string SQL = "CALL procProcessCreditBills();";

                MySqlConnection cn = GetConnection();

                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = cn;
                cmd.Transaction = mTransaction;
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandText = SQL;

                cmd.ExecuteNonQuery();

            }

            catch (Exception ex)
            {
                TransactionFailed = true;
                if (IsInTransaction)
                {
                    mTransaction.Rollback();
                    mTransaction.Dispose();
                    mConnection.Close();
                    mConnection.Dispose();
                }

                throw ex;
            }
        }

    }
}

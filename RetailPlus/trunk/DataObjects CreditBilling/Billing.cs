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
        public DateTime CreditPaymentDueDate;
        public DateTime BillingDate;
        public DateTime CreditPurcStartDateToProcess;
        public DateTime CreditPurcEndDateToProcess;
        public string BillingFile;
        
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

        public Int32 SetBillingAsPrinted(long ContactID, DateTime BillingDate, string BillingFile)
        {
            try
            {
                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandType = System.Data.CommandType.Text;

                string SQL = "UPDATE tblCreditBillHeader SET " +
                                "IsBillPrinted = 1, BillingFile = @BillingFile " +
                            "WHERE ContactID = @ContactID AND BillingDate = @BillingDate;";

                
                cmd.Parameters.AddWithValue("BillingFile", BillingFile);
                cmd.Parameters.AddWithValue("ContactID", ContactID);
                cmd.Parameters.AddWithValue("BillingDate", BillingDate.ToString("yyyy-MM-dd"));

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

        
        #endregion

        private string SQLSelect()
        {
            string stSQL = "SELECT CreditBillHeaderID ,CBH.CreditBillID ,ContactID ,CreditLimit ,RunningCreditAmt ,CurrMonthCreditAmt ,CurrMonthAmountPaid ,TotalBillCharges ,CurrentDueAmount ,MinimumAmountDue " +
                                  ",Prev1MoCurrentDueAmount ,Prev1MoMinimumAmountDue ,Prev1MoCurrMonthAmountPaid ,Prev2MoCurrentDueAmount ,CBL.BillingDate ,CreditCutOffDate ,CreditPaymentDueDate " +
                                  ",CreditPurcStartDateToProcess ,CreditPurcEndDateToProcess, BillingFile " +
                            "FROM tblCreditBillHeader CBH " +
                            "INNER JOIN tblCreditBills CBL ON CBH.CreditBillID = CBL.CreditBillID ";

            return stSQL;
        }

        #region Details

        public BillingDetails Details(long CustomerID)
        {
            try
            {
                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandType = System.Data.CommandType.Text;

                string SQL = SQLSelect();
                SQL += "WHERE IsBillPrinted = 0 AND CBL.BillingDate = (SELECT MAX(BillingDate) FROM tblCreditBills) ";
                SQL += "AND tblContacts.ContactID = @CustomerID;";

                if (CustomerID != 0)
                {
                    SQL += "AND tblContacts.ContactID = @ContactID ";
                    cmd.Parameters.AddWithValue("ContactID", CustomerID);
                }

                cmd.CommandText = SQL;
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
                    Details.CreditPaymentDueDate = myReader.GetDateTime("CreditPaymentDueDate");
                    Details.BillingDate = myReader.GetDateTime("BillingDate");
                    Details.CreditPurcStartDateToProcess = myReader.GetDateTime("CreditPurcStartDateToProcess");
                    Details.CreditPurcEndDateToProcess = myReader.GetDateTime("CreditPurcEndDateToProcess");
                    Details.BillingFile = myReader.GetString("BillingFile");

                    Customer clsCustomer = new Customer(base.Connection, base.Transaction);
                    Details.CustomerDetails = clsCustomer.Details(Details.ContactID);
                }

                myReader.Close();

                return Details;
            }
            catch (Exception ex)
            {
                throw base.ThrowException(ex);
            }
        }

        public DateTime getCreditPurcEndDateToProcess()
        {
            try
            {
                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandType = System.Data.CommandType.Text;

                string SQL = "SELECT CreditPurcEndDateToProcess FROM tblCardTypes WHERE CardTypeCode = 'HP Card'";

                cmd.CommandText = SQL;
                string strDataTableName = "tbl" + this.GetType().FullName.Split(new Char[] { '.' })[this.GetType().FullName.Split(new Char[] { '.' }).Length - 1]; System.Data.DataTable dt = new System.Data.DataTable(strDataTableName);
                base.MySqlDataAdapterFill(cmd, dt);

                DateTime dteRetValue = DateTime.MaxValue;
                foreach(System.Data.DataRow dr in dt.Rows)
                {
                    dteRetValue = DateTime.Parse(dr["CreditPurcEndDateToProcess"].ToString());
                }

                return dteRetValue;
            }
            catch (Exception ex)
            {
                throw base.ThrowException(ex);
            }
        }

        public DateTime getBillingDate()
        {
            try
            {
                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandType = System.Data.CommandType.Text;

                string SQL = "SELECT BillingDate FROM tblCardTypes WHERE CardTypeCode = 'HP Card'";

                cmd.CommandText = SQL;
                string strDataTableName = "tbl" + this.GetType().FullName.Split(new Char[] { '.' })[this.GetType().FullName.Split(new Char[] { '.' }).Length - 1]; System.Data.DataTable dt = new System.Data.DataTable(strDataTableName);
                base.MySqlDataAdapterFill(cmd, dt);

                DateTime dteRetValue = Constants.C_DATE_MIN_VALUE;
                foreach (System.Data.DataRow dr in dt.Rows)
                {
                    dteRetValue = DateTime.Parse(dr["BillingDate"].ToString());
                }

                return dteRetValue;
            }
            catch (Exception ex)
            {
                throw base.ThrowException(ex);
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

                Details.CreditCutOffDate = DateTime.Parse(dr["CreditCutOffDate"].ToString());
                Details.CreditPaymentDueDate = DateTime.Parse(dr["CreditPaymentDueDate"].ToString());
                Details.BillingDate = DateTime.Parse(dr["BillingDate"].ToString());

                Details.CreditPurcStartDateToProcess = DateTime.Parse(dr["CreditPurcStartDateToProcess"].ToString());
                Details.CreditPurcEndDateToProcess = DateTime.Parse(dr["CreditPurcEndDateToProcess"].ToString());

                // need an override to eliminate reporting issue
                // '0001-01-01' is not accepted by Crystal
                Details.CreditPurcStartDateToProcess = Details.CreditPurcStartDateToProcess == DateTime.MinValue ? Constants.C_DATE_MIN_VALUE : Details.CreditPurcStartDateToProcess;
                Details.CreditPurcEndDateToProcess = Details.CreditPurcEndDateToProcess == DateTime.MinValue ? Constants.C_DATE_MIN_VALUE : Details.CreditPurcEndDateToProcess;

                Details.BillingFile = dr["BillingFile"].ToString();

                Customer clsCustomer = new Customer(base.Connection, base.Transaction);
                Details.CustomerDetails = clsCustomer.Details(Details.ContactID);

                return Details;
            }
            catch (Exception ex)
            {
                throw base.ThrowException(ex);
            }
        }

        #endregion

        #region Streams

        public System.Data.DataTable ListAsDataTable(Int64 ContactID = 0, DateTime? BillingDate = null, string SortField = "CreditBillHeaderID", System.Data.SqlClient.SortOrder SortOrder = System.Data.SqlClient.SortOrder.Ascending, Int32 limit = 0)
        {
            try
            {
                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandType = System.Data.CommandType.Text;

                string SQL = SQLSelect() + "";
                SQL += "WHERE IsBillPrinted = 0 ";

                if (ContactID != 0)
                {
                    SQL += "AND tblContacts.ContactID = @ContactID ";
                    cmd.Parameters.AddWithValue("ContactID", ContactID);
                }
                if (BillingDate.GetValueOrDefault(Constants.C_DATE_MIN_VALUE) == Constants.C_DATE_MIN_VALUE)
                {
                    SQL += "AND CBL.BillingDate = (SELECT MAX(BillingDate) FROM tblCreditBills) ";
                }
                else
                {
                    SQL += "AND CBL.BillingDate = @BillingDate ";
                    cmd.Parameters.AddWithValue("BillingDate", BillingDate.GetValueOrDefault(Constants.C_DATE_MIN_VALUE));
                }

                SQL += "ORDER BY " + (!string.IsNullOrEmpty(SortField) ? SortField : "CreditBillHeaderID") + " ";
                SQL += SortOrder == System.Data.SqlClient.SortOrder.Ascending ? "ASC " : "DESC ";
                SQL += limit == 0 ? "" : "LIMIT " + limit.ToString() + " ";

                cmd.CommandText = SQL;
                string strDataTableName = "tbl" + this.GetType().FullName.Split(new Char[] { '.' })[this.GetType().FullName.Split(new Char[] { '.' }).Length - 1]; System.Data.DataTable dt = new System.Data.DataTable(strDataTableName);
                base.MySqlDataAdapterFill(cmd, dt);

                return dt;
            }
            catch (Exception ex)
            {
                throw base.ThrowException(ex);
            }
        }

        public System.Data.DataTable ListDetailsAsDataTable(Int64 CreditBillHeaderID = 0, string SortField = "TransactionDate", System.Data.SqlClient.SortOrder SortOrder = System.Data.SqlClient.SortOrder.Ascending, Int32 limit = 0)
        {
            try
            {
                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandType = System.Data.CommandType.Text;

                string SQL = "SELECT CreditBillHeaderID ,TransactionDate ,Description ,Amount FROM tblCreditBillDetail WHERE 1=1 ";

                if (CreditBillHeaderID != 0)
                {
                    SQL += "AND CreditBillHeaderID = @CreditBillHeaderID ";
                    cmd.Parameters.AddWithValue("CreditBillHeaderID", CreditBillHeaderID);
                }

                SQL += "ORDER BY " + (!string.IsNullOrEmpty(SortField) ? SortField : "TransactionDate") + " ";
                SQL += SortOrder == System.Data.SqlClient.SortOrder.Ascending ? "ASC " : "DESC ";
                SQL += limit == 0 ? "" : "LIMIT " + limit.ToString() + " ";

                cmd.CommandText = SQL;
                string strDataTableName = "tbl" + this.GetType().FullName.Split(new Char[] { '.' })[this.GetType().FullName.Split(new Char[] { '.' }).Length - 1]; System.Data.DataTable dt = new System.Data.DataTable(strDataTableName);
                base.MySqlDataAdapterFill(cmd, dt);

                return dt;
            }
            catch (Exception ex)
            {
                throw base.ThrowException(ex);
            }
        }

        public System.Data.DataTable ListBillingDateAsDataTable(Int64 CustomerID, DateTime? BillingDate = null, string SortField = "BillingDate", System.Data.SqlClient.SortOrder SortOrder = System.Data.SqlClient.SortOrder.Descending, Int32 limit = 0)
        {
            try
            {
                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandType = System.Data.CommandType.Text;

                string SQL = "SELECT DISTINCT DATE_FORMAT(BillingDate, '%Y-%m-%d') BillingDate, BillingFile FROM tblCreditBillHeader WHERE 1=1 ";

                if (CustomerID != 0)
                {
                    SQL += "AND ContactID = @CustomerID ";
                    cmd.Parameters.AddWithValue("CustomerID", CustomerID);
                }
                if (BillingDate.GetValueOrDefault(Constants.C_DATE_MIN_VALUE) != Constants.C_DATE_MIN_VALUE)
                {
                    SQL += "AND BillingDate = @BillingDate ";
                    cmd.Parameters.AddWithValue("BillingDate", BillingDate);
                }

                SQL += "ORDER BY " + (!string.IsNullOrEmpty(SortField) ? SortField : "BillingDate") + " ";
                SQL += SortOrder == System.Data.SqlClient.SortOrder.Ascending ? "ASC " : "DESC ";
                SQL += limit == 0 ? "" : "LIMIT " + limit.ToString() + " ";

                cmd.CommandText = SQL;
                string strDataTableName = "tbl" + this.GetType().FullName.Split(new Char[] { '.' })[this.GetType().FullName.Split(new Char[] { '.' }).Length - 1]; System.Data.DataTable dt = new System.Data.DataTable(strDataTableName);
                base.MySqlDataAdapterFill(cmd, dt);

                return dt;
            }
            catch (Exception ex)
            {
                throw base.ThrowException(ex);
            }
        }

        public List<BillingDetails> List(Int64 ContactID = 0, DateTime? BillingDate = null, string SortField = "ContactID", System.Data.SqlClient.SortOrder SortOrder = System.Data.SqlClient.SortOrder.Ascending, Int32 limit = 0)
        {
            try
            {
                System.Data.DataTable dt = ListAsDataTable(ContactID, BillingDate, SortField, SortOrder, limit);

                List<BillingDetails> lstRetValue = new List<BillingDetails>();
                foreach (DataRow dr in dt.Rows)
                {
                    lstRetValue.Add(setDetails(dr));
                }
            
                return lstRetValue;
            }
            catch (Exception ex)
            {
                throw base.ThrowException(ex);
            }
        }

        #endregion

        public void ProcessCurrentBill()
        {
            try
            {
                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandType = System.Data.CommandType.Text;

                string SQL = "CALL procProcessCreditBills(0);";

                cmd.CommandText = SQL;
                base.ExecuteNonQuery(cmd);
            }
            catch (Exception ex)
            {
                throw base.ThrowException(ex);
            }
        }

        public Int32 CloseCurrentBill()
        {
            try
            {
                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandType = System.Data.CommandType.Text;

                string SQL = "CALL procProcessCreditBillsClose();";

                cmd.CommandText = SQL;
                return base.ExecuteNonQuery(cmd);
            }
            catch (Exception ex)
            {
                throw base.ThrowException(ex);
            }
        }
    }
}

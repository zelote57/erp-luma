//using System;
//using System.Security.Permissions;
//using MySql.Data.MySqlClient;
//using System.Collections;
//using System.Collections.Generic;
//using System.Data;

//namespace AceSoft.RetailPlus.Data
//{

//    [StrongNameIdentityPermissionAttribute(SecurityAction.LinkDemand,
//         PublicKey = "002400000480000094000000060200000024000" +
//         "052534131000400000100010053D785642F9F960B43157E0380" +
//         "F393BEE53E8DFAFBF441366C1B6F8B48D9DDF0D527B1F3B21EA" +
//         "E85D2FDB664CE81EB8A87DBE4C589D6F4202FE2B7D4B978BB69" +
//         "684874612CB9B8DB7A0339400A9C4E68277884B07817363D242" +
//         "E3696F9FACDBEA831810AE6DC9EDCA91A7B5DA12FE7BF65D113" +
//         "FF52834EAFB5A7A1FDFD5851A3")]

//    #region CreditPaymentDetails

//    public struct CreditPaymentDetails
//    {
//        public Int64 SyncID;
//        public Int64 CreditPaymentID;
        
//        public string TerminalNo;
//        public Int32 BranchID;
//        public BranchDetails BranchDetails;
//        public long TransactionID;
//        public long ContactID;

//        public string TransactionNo;
//        public decimal Amount;
//        public decimal AmountPaid;

//        public string Remarks;
//        public string CreditReason;
//        public string CashierName;
//        public DateTime CreditDate;

//        public decimal AmountPaidCuttOffMonth;

//        public Int32 CreditCardTypeID; // reference for 
//        public Int64 CreditCardPaymentID; // reference for InHouseCreditCard Payment
//    }

//    #endregion


//    [StrongNameIdentityPermissionAttribute(SecurityAction.LinkDemand,
//         PublicKey = "002400000480000094000000060200000024000" +
//         "052534131000400000100010053D785642F9F960B43157E0380" +
//         "F393BEE53E8DFAFBF441366C1B6F8B48D9DDF0D527B1F3B21EA" +
//         "E85D2FDB664CE81EB8A87DBE4C589D6F4202FE2B7D4B978BB69" +
//         "684874612CB9B8DB7A0339400A9C4E68277884B07817363D242" +
//         "E3696F9FACDBEA831810AE6DC9EDCA91A7B5DA12FE7BF65D113" +
//         "FF52834EAFB5A7A1FDFD5851A3")]
//    public class CreditPayment : POSConnection
//    {
//        #region Constructors and Destructors

//        public CreditPayment()
//            : base(null, null)
//        {
//        }

//        public CreditPayment(MySqlConnection Connection, MySqlTransaction Transaction) 
//            : base(Connection, Transaction)
//        {

//        }

//        #endregion

//        #region Insert and Update

//        public Int32 SetCreditPaymentAsPrinted(long ContactID, DateTime CreditPaymentDate, string CreditPaymentFile)
//        {
//            try
//            {
//                MySqlCommand cmd = new MySqlCommand();
//                cmd.CommandType = System.Data.CommandType.Text;

//                string SQL = "UPDATE tblCreditBillHeader SET " +
//                                "IsBillPrinted = 1, CreditPaymentFile = @CreditPaymentFile " +
//                            "WHERE ContactID = @ContactID AND CreditPaymentDate = @CreditPaymentDate;";

                
//                cmd.Parameters.AddWithValue("CreditPaymentFile", CreditPaymentFile);
//                cmd.Parameters.AddWithValue("ContactID", ContactID);
//                cmd.Parameters.AddWithValue("CreditPaymentDate", CreditPaymentDate.ToString("yyyy-MM-dd"));

//                cmd.CommandText = SQL;
//                return base.ExecuteNonQuery(cmd);
//            }
//            catch (Exception ex)
//            {
//                throw base.ThrowException(ex);
//            }
//        }
       
//        #endregion

//        #region Delete

        
//        #endregion

//        private string SQLSelect()
//        {
//            string stSQL = "SELECT CreditBillHeaderID ,CBH.CreditBillID ,ContactID ,CreditLimit ,RunningCreditAmt ,CurrMonthCreditAmt ,CurrMonthAmountPaid ,TotalBillCharges ,CurrentDueAmount ,MinimumAmountDue " +
//                                  ",Prev1MoCurrentDueAmount ,Prev1MoMinimumAmountDue ,Prev1MoCurrMonthAmountPaid ,Prev2MoCurrentDueAmount ,CBL.CreditPaymentDate ,CreditCutOffDate ,CreditPaymentDueDate " +
//                                  ",CreditPurcStartDateToProcess ,CreditPurcEndDateToProcess, CreditPaymentFile " +
//                            "FROM tblCreditBillHeader CBH " +
//                            "INNER JOIN tblCreditBills CBL ON CBH.CreditBillID = CBL.CreditBillID ";

//            return stSQL;
//        }

//        #region Details

//        public CreditPaymentDetails Details(long CustomerID)
//        {
//            try
//            {
//                MySqlCommand cmd = new MySqlCommand();
//                cmd.CommandType = System.Data.CommandType.Text;

//                string SQL = SQLSelect();
//                SQL += "WHERE IsBillPrinted = 0 AND CBL.CreditPaymentDate = (SELECT MAX(CreditPaymentDate) FROM tblCreditBills) ";
//                SQL += "AND tblContacts.ContactID = @CustomerID;";

//                if (CustomerID != 0)
//                {
//                    SQL += "AND tblContacts.ContactID = @ContactID ";
//                    cmd.Parameters.AddWithValue("ContactID", CustomerID);
//                }

//                cmd.CommandText = SQL;
//                string strDataTableName = "tbl" + this.GetType().FullName.Split(new Char[] { '.' })[this.GetType().FullName.Split(new Char[] { '.' }).Length - 1]; System.Data.DataTable dt = new System.Data.DataTable(strDataTableName);
//                base.MySqlDataAdapterFill(cmd, dt);

//                CreditPaymentDetails Details = new CreditPaymentDetails();
//                foreach (System.Data.DataRow dr in dt.Rows)
//                {
//                    Details.ContactID = Int64.Parse(dr["ContactID"].ToString());
//                    Details.CrediLimit = decimal.Parse(dr["CreditLimit"].ToString());
//                    Details.RunningCreditAmt = decimal.Parse(dr["RunningCreditAmt"].ToString());
//                    Details.CurrMonthCreditAmt = decimal.Parse(dr["CurrMonthCreditAmt"].ToString());
//                    Details.CurrMonthAmountPaid = decimal.Parse(dr["CurrMonthAmountPaid"].ToString());
//                    Details.TotalBillCharges = decimal.Parse(dr["TotalBillCharges"].ToString());
//                    Details.CurrentDueAmount = decimal.Parse(dr["CurrentDueAmount"].ToString());
//                    Details.MinimumAmountDue = decimal.Parse(dr["MinimumAmountDue"].ToString());

//                    Details.Prev1MoCurrentDueAmount = decimal.Parse(dr["Prev1MoCurrentDueAmount"].ToString());
//                    Details.Prev1MoMinimumAmountDue = decimal.Parse(dr["Prev1MoMinimumAmountDue"].ToString());
//                    Details.Prev1MoCurrMonthAmountPaid = decimal.Parse(dr["Prev1MoCurrMonthAmountPaid"].ToString());
//                    Details.Prev2MoCurrentDueAmount = decimal.Parse(dr["Prev2MoCurrentDueAmount"].ToString());

//                    Details.CreditCutOffDate = DateTime.Parse(dr["CreditCutOffDate"].ToString());
//                    Details.CreditPaymentDueDate = DateTime.Parse(dr["CreditPaymentDueDate"].ToString());
//                    Details.CreditPaymentDate = DateTime.Parse(dr["CreditPaymentDate"].ToString());
//                    Details.CreditPurcStartDateToProcess = DateTime.Parse(dr["CreditPurcStartDateToProcess"].ToString());
//                    Details.CreditPurcEndDateToProcess = DateTime.Parse(dr["CreditPurcEndDateToProcess"].ToString());
//                    Details.CreditPaymentFile = dr["CreditPaymentFile"].ToString();

//                    Customer clsCustomer = new Customer(base.Connection, base.Transaction);
//                    Details.CustomerDetails = clsCustomer.Details(Details.ContactID);
//                }

//                return Details;
//            }
//            catch (Exception ex)
//            {
//                throw base.ThrowException(ex);
//            }
//        }

//        public DateTime getCreditPurcEndDateToProcess()
//        {
//            try
//            {
//                MySqlCommand cmd = new MySqlCommand();
//                cmd.CommandType = System.Data.CommandType.Text;

//                string SQL = "SELECT CreditPurcEndDateToProcess FROM tblCardTypes WHERE CardTypeCode = (SELECT ConfigValue FROM sysCreditConfig WHERE ConfigName = 'IndividualCardTypeCode') ";

//                cmd.CommandText = SQL;
//                string strDataTableName = "tbl" + this.GetType().FullName.Split(new Char[] { '.' })[this.GetType().FullName.Split(new Char[] { '.' }).Length - 1]; System.Data.DataTable dt = new System.Data.DataTable(strDataTableName);
//                base.MySqlDataAdapterFill(cmd, dt);

//                DateTime dteRetValue = DateTime.MaxValue;
//                foreach(System.Data.DataRow dr in dt.Rows)
//                {
//                    dteRetValue = DateTime.Parse(dr["CreditPurcEndDateToProcess"].ToString());
//                }

//                return dteRetValue;
//            }
//            catch (Exception ex)
//            {
//                throw base.ThrowException(ex);
//            }
//        }

//        public DateTime getCreditPaymentDate()
//        {
//            try
//            {
//                MySqlCommand cmd = new MySqlCommand();
//                cmd.CommandType = System.Data.CommandType.Text;

//                string SQL = "SELECT CreditPaymentDate FROM tblCardTypes WHERE CardTypeCode = (SELECT ConfigValue FROM sysCreditConfig WHERE ConfigName = 'IndividualCardTypeCode') ";

//                cmd.CommandText = SQL;
//                string strDataTableName = "tbl" + this.GetType().FullName.Split(new Char[] { '.' })[this.GetType().FullName.Split(new Char[] { '.' }).Length - 1]; System.Data.DataTable dt = new System.Data.DataTable(strDataTableName);
//                base.MySqlDataAdapterFill(cmd, dt);

//                DateTime dteRetValue = Constants.C_DATE_MIN_VALUE;
//                foreach (System.Data.DataRow dr in dt.Rows)
//                {
//                    dteRetValue = DateTime.Parse(dr["CreditPaymentDate"].ToString());
//                }

//                return dteRetValue;
//            }
//            catch (Exception ex)
//            {
//                throw base.ThrowException(ex);
//            }
//        }

//        private CreditPaymentDetails setDetails(System.Data.DataRow dr)
//        {
//            try
//            {
//                CreditPaymentDetails Details = new CreditPaymentDetails();

//                Details.CreditBillHeaderID = Convert.ToInt64(dr["CreditBillHeaderID"]);
//                Details.ContactID = Convert.ToInt64(dr["ContactID"]);
//                Details.CrediLimit = Convert.ToDecimal(dr["CreditLimit"]);
//                Details.RunningCreditAmt = Convert.ToDecimal(dr["RunningCreditAmt"]);
//                Details.CurrMonthCreditAmt = Convert.ToDecimal(dr["CurrMonthCreditAmt"]);
//                Details.CurrMonthAmountPaid = Convert.ToDecimal(dr["CurrMonthAmountPaid"]);
//                Details.TotalBillCharges = Convert.ToDecimal(dr["TotalBillCharges"]);
//                Details.CurrentDueAmount = Convert.ToDecimal(dr["CurrentDueAmount"]);
//                Details.MinimumAmountDue = Convert.ToDecimal(dr["MinimumAmountDue"]);

//                Details.Prev1MoCurrentDueAmount = Convert.ToDecimal(dr["Prev1MoCurrentDueAmount"]);
//                Details.Prev1MoMinimumAmountDue = Convert.ToDecimal(dr["Prev1MoMinimumAmountDue"]);
//                Details.Prev1MoCurrMonthAmountPaid = Convert.ToDecimal(dr["Prev1MoCurrMonthAmountPaid"]);
//                Details.Prev2MoCurrentDueAmount = Convert.ToDecimal(dr["Prev2MoCurrentDueAmount"]);

//                Details.CreditCutOffDate = DateTime.Parse(dr["CreditCutOffDate"].ToString());
//                Details.CreditPaymentDueDate = DateTime.Parse(dr["CreditPaymentDueDate"].ToString());
//                Details.CreditPaymentDate = DateTime.Parse(dr["CreditPaymentDate"].ToString());

//                Details.CreditPurcStartDateToProcess = DateTime.Parse(dr["CreditPurcStartDateToProcess"].ToString());
//                Details.CreditPurcEndDateToProcess = DateTime.Parse(dr["CreditPurcEndDateToProcess"].ToString());

//                // need an override to eliminate reporting issue
//                // '0001-01-01' is not accepted by Crystal
//                Details.CreditPurcStartDateToProcess = Details.CreditPurcStartDateToProcess == DateTime.MinValue ? Constants.C_DATE_MIN_VALUE : Details.CreditPurcStartDateToProcess;
//                Details.CreditPurcEndDateToProcess = Details.CreditPurcEndDateToProcess == DateTime.MinValue ? Constants.C_DATE_MIN_VALUE : Details.CreditPurcEndDateToProcess;

//                Details.CreditPaymentFile = dr["CreditPaymentFile"].ToString();

//                Customer clsCustomer = new Customer(base.Connection, base.Transaction);
//                Details.CustomerDetails = clsCustomer.Details(Details.ContactID);

//                return Details;
//            }
//            catch (Exception ex)
//            {
//                throw base.ThrowException(ex);
//            }
//        }

//        #endregion

//        #region Streams

//        public System.Data.DataTable ListAsDataTable(Int64 ContactID = 0, DateTime? CreditPaymentDate = null, string SortField = "CreditBillHeaderID", System.Data.SqlClient.SortOrder SortOrder = System.Data.SqlClient.SortOrder.Ascending, Int32 limit = 0)
//        {
//            try
//            {
//                MySqlCommand cmd = new MySqlCommand();
//                cmd.CommandType = System.Data.CommandType.Text;

//                string SQL = SQLSelect() + "";
//                SQL += "WHERE IsBillPrinted = 0 ";

//                if (ContactID != 0)
//                {
//                    SQL += "AND tblContacts.ContactID = @ContactID ";
//                    cmd.Parameters.AddWithValue("ContactID", ContactID);
//                }
//                if (CreditPaymentDate.GetValueOrDefault(Constants.C_DATE_MIN_VALUE) == Constants.C_DATE_MIN_VALUE)
//                {
//                    SQL += "AND CBL.CreditPaymentDate = (SELECT MAX(CreditPaymentDate) FROM tblCreditBills) ";
//                }
//                else
//                {
//                    SQL += "AND CBL.CreditPaymentDate = @CreditPaymentDate ";
//                    cmd.Parameters.AddWithValue("CreditPaymentDate", CreditPaymentDate.GetValueOrDefault(Constants.C_DATE_MIN_VALUE));
//                }

//                SQL += "ORDER BY " + (!string.IsNullOrEmpty(SortField) ? SortField : "CreditBillHeaderID") + " ";
//                SQL += SortOrder == System.Data.SqlClient.SortOrder.Ascending ? "ASC " : "DESC ";
//                SQL += limit == 0 ? "" : "LIMIT " + limit.ToString() + " ";

//                cmd.CommandText = SQL;
//                string strDataTableName = "tbl" + this.GetType().FullName.Split(new Char[] { '.' })[this.GetType().FullName.Split(new Char[] { '.' }).Length - 1]; System.Data.DataTable dt = new System.Data.DataTable(strDataTableName);
//                base.MySqlDataAdapterFill(cmd, dt);

//                return dt;
//            }
//            catch (Exception ex)
//            {
//                throw base.ThrowException(ex);
//            }
//        }

//        public System.Data.DataTable ListDetailsAsDataTable(Int64 CreditBillHeaderID = 0, string SortField = "TransactionDate", System.Data.SqlClient.SortOrder SortOrder = System.Data.SqlClient.SortOrder.Ascending, Int32 limit = 0)
//        {
//            try
//            {
//                MySqlCommand cmd = new MySqlCommand();
//                cmd.CommandType = System.Data.CommandType.Text;

//                string SQL = "SELECT CreditBillHeaderID ,TransactionDate ,Description ,Amount FROM tblCreditBillDetail WHERE 1=1 ";

//                if (CreditBillHeaderID != 0)
//                {
//                    SQL += "AND CreditBillHeaderID = @CreditBillHeaderID ";
//                    cmd.Parameters.AddWithValue("CreditBillHeaderID", CreditBillHeaderID);
//                }

//                SQL += "ORDER BY " + (!string.IsNullOrEmpty(SortField) ? SortField : "TransactionDate") + " ";
//                SQL += SortOrder == System.Data.SqlClient.SortOrder.Ascending ? "ASC " : "DESC ";
//                SQL += limit == 0 ? "" : "LIMIT " + limit.ToString() + " ";

//                cmd.CommandText = SQL;
//                string strDataTableName = "tbl" + this.GetType().FullName.Split(new Char[] { '.' })[this.GetType().FullName.Split(new Char[] { '.' }).Length - 1]; System.Data.DataTable dt = new System.Data.DataTable(strDataTableName);
//                base.MySqlDataAdapterFill(cmd, dt);

//                return dt;
//            }
//            catch (Exception ex)
//            {
//                throw base.ThrowException(ex);
//            }
//        }

//        public System.Data.DataTable ListCreditPaymentDateAsDataTable(Int64 CustomerID, DateTime? CreditPaymentDate = null, string SortField = "CreditPaymentDate", System.Data.SqlClient.SortOrder SortOrder = System.Data.SqlClient.SortOrder.Descending, Int32 limit = 0)
//        {
//            try
//            {
//                MySqlCommand cmd = new MySqlCommand();
//                cmd.CommandType = System.Data.CommandType.Text;

//                string SQL = "SELECT DISTINCT DATE_FORMAT(CreditPaymentDate, '%Y-%m-%d') CreditPaymentDate, CreditPaymentFile FROM tblCreditBillHeader WHERE 1=1 ";

//                if (CustomerID != 0)
//                {
//                    SQL += "AND ContactID = @CustomerID ";
//                    cmd.Parameters.AddWithValue("CustomerID", CustomerID);
//                }
//                if (CreditPaymentDate.GetValueOrDefault(Constants.C_DATE_MIN_VALUE) != Constants.C_DATE_MIN_VALUE)
//                {
//                    SQL += "AND CreditPaymentDate = @CreditPaymentDate ";
//                    cmd.Parameters.AddWithValue("CreditPaymentDate", CreditPaymentDate);
//                }

//                SQL += "ORDER BY " + (!string.IsNullOrEmpty(SortField) ? SortField : "CreditPaymentDate") + " ";
//                SQL += SortOrder == System.Data.SqlClient.SortOrder.Ascending ? "ASC " : "DESC ";
//                SQL += limit == 0 ? "" : "LIMIT " + limit.ToString() + " ";

//                cmd.CommandText = SQL;
//                string strDataTableName = "tbl" + this.GetType().FullName.Split(new Char[] { '.' })[this.GetType().FullName.Split(new Char[] { '.' }).Length - 1]; System.Data.DataTable dt = new System.Data.DataTable(strDataTableName);
//                base.MySqlDataAdapterFill(cmd, dt);

//                return dt;
//            }
//            catch (Exception ex)
//            {
//                throw base.ThrowException(ex);
//            }
//        }

//        public List<CreditPaymentDetails> List(Int64 ContactID = 0, DateTime? CreditPaymentDate = null, string SortField = "ContactID", System.Data.SqlClient.SortOrder SortOrder = System.Data.SqlClient.SortOrder.Ascending, Int32 limit = 0)
//        {
//            try
//            {
//                System.Data.DataTable dt = ListAsDataTable(ContactID, CreditPaymentDate, SortField, SortOrder, limit);

//                List<CreditPaymentDetails> lstRetValue = new List<CreditPaymentDetails>();
//                foreach (DataRow dr in dt.Rows)
//                {
//                    lstRetValue.Add(setDetails(dr));
//                }
            
//                return lstRetValue;
//            }
//            catch (Exception ex)
//            {
//                throw base.ThrowException(ex);
//            }
//        }

//        #endregion

//        public void ProcessCurrentBill()
//        {
//            try
//            {
//                MySqlCommand cmd = new MySqlCommand();
//                cmd.CommandType = System.Data.CommandType.Text;

//                string SQL = "CALL procProcessCreditBills(0);";

//                cmd.CommandText = SQL;
//                base.ExecuteNonQuery(cmd);
//            }
//            catch (Exception ex)
//            {
//                throw base.ThrowException(ex);
//            }
//        }

//        public Int32 CloseCurrentBill()
//        {
//            try
//            {
//                MySqlCommand cmd = new MySqlCommand();
//                cmd.CommandType = System.Data.CommandType.Text;

//                string SQL = "CALL procProcessCreditBillsClose();";

//                cmd.CommandText = SQL;
//                return base.ExecuteNonQuery(cmd);
//            }
//            catch (Exception ex)
//            {
//                throw base.ThrowException(ex);
//            }
//        }
//    }
//}

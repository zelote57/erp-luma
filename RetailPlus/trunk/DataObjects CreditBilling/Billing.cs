﻿using System;
using System.Security.Permissions;
using MySql.Data.MySqlClient;
using System.Collections;
using System.Collections.Generic;
using System.Data;

namespace AceSoft.RetailPlus.Data
{

    #region BillingDetails

    [StrongNameIdentityPermissionAttribute(SecurityAction.LinkDemand,
         PublicKey = "002400000480000094000000060200000024000" +
         "052534131000400000100010053D785642F9F960B43157E0380" +
         "F393BEE53E8DFAFBF441366C1B6F8B48D9DDF0D527B1F3B21EA" +
         "E85D2FDB664CE81EB8A87DBE4C589D6F4202FE2B7D4B978BB69" +
         "684874612CB9B8DB7A0339400A9C4E68277884B07817363D242" +
         "E3696F9FACDBEA831810AE6DC9EDCA91A7B5DA12FE7BF65D113" +
         "FF52834EAFB5A7A1FDFD5851A3")]
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

        public decimal CurrentPurchaseAmt;
        public decimal BeginningBalance;
        public decimal EndingBalance;

        public DateTime CreditCutOffDate;
        public DateTime CreditPaymentDueDate;
        public DateTime BillingDate;
        public DateTime CreditPurcStartDateToProcess;
        public DateTime CreditPurcEndDateToProcess;
        public string BillingFile;
        public bool isBillPrinted;
        
        public Data.ContactDetails CustomerDetails;
        public Data.CardTypeDetails CardTypeDetails;
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

        public Int32 SetBillingAsPrinted(CreditType CreditType, Int64 ContactIDorGuarantorID, DateTime BillingDate, string BillingFile)
        {
            try
            {
                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandType = System.Data.CommandType.Text;

                string SQL = "UPDATE tblCreditBillHeader SET " +
                                "IsBillPrinted = 1, BillingFile = @BillingFile " +
                            "WHERE ContactID = @ContactID AND BillingDate = @BillingDate;";

                // do an override if group
                if (CreditType == RetailPlus.CreditType.Group)
                    SQL = "UPDATE tblCreditBillHeader SET " +
                                "IsBillPrinted = 1, BillingFile = @BillingFile " +
                            "WHERE GuarantorID = @ContactID AND BillingDate = @BillingDate;";
                
                cmd.Parameters.AddWithValue("BillingFile", BillingFile);
                cmd.Parameters.AddWithValue("ContactID", ContactIDorGuarantorID);
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
            string stSQL = "SELECT CreditBillHeaderID ,CBH.CreditBillID ,CBH.GuarantorID ,IFNULL(GUA.ContactCode,'''') AS GuarantorCode ,IFNULL(GUA.ContactName,'''') AS GuarantorName " +
                                  ",CBH.ContactID ,CBH.CreditLimit ,CBH.RunningCreditAmt ,CBH.CurrMonthCreditAmt ,CBH.CurrMonthAmountPaid ,CBH.TotalBillCharges ,CBH.CurrentDueAmount ,CBH.MinimumAmountDue " +
                                  ",CBH.Prev1MoCurrentDueAmount ,CBH.Prev1MoMinimumAmountDue ,CBH.Prev1MoCurrMonthAmountPaid ,CBH.Prev2MoCurrentDueAmount ,CBH.CurrentPurchaseAmt ,CBH.BeginningBalance ,CBH.EndingBalance " +
                                  ",CreditPaymentDueDate " +
                                  ",BillingFile, IsBillPrinted " +
                                  ",CUS.ContactName ,CUS.CreditLimit ,CCI.CreditCardNo " +
                                  ",CBL.CreditCardTypeID, CBL.CreditFinanceCharge " +
                                  ",CBL.CreditLatePenaltyCharge " +
                                    ",CBL.CreditMinimumAmountDue " +
                                    ",CBL.CreditMinimumPercentageDue " +
                                    ",CBL.CreditFinanceCharge15th " +
                                    ",CBL.CreditLatePenaltyCharge15th " +
                                    ",CBL.CreditMinimumAmountDue15th " +
                                    ",CBL.CreditMinimumPercentageDue15th " +
                                    ",CBL.CreditPurcStartDateToProcess " +
                                    ",CBL.CreditPurcEndDateToProcess" +
                                    ",CBL.CreditCutOffDate" +
                                    ",CBL.CreditCardType " +
                                    ",CBL.WithGuarantor " +
                                    ",CBL.CreditUseLastDayCutOffDate, CBL.BillingDate " +
                                    ",CTY.CardTypeCode ,CTY.CardTypeName " +
                            "FROM tblCreditBillHeader CBH " +
                            "INNER JOIN tblCreditBills CBL ON CBH.CreditBillID = CBL.CreditBillID " +
                            "INNER JOIN tblCardTypes CTY ON CTY.CardTypeID = CBL.CreditCardTypeID " +
                            "INNER JOIN tblContacts CUS ON CUS.ContactID = CBH.ContactID " +
                            "INNER JOIN tblContactCreditCardInfo CCI ON CUS.ContactID = CCI.CustomerID " +
                            "LEFT OUTER JOIN tblContacts GUA ON GUA.ContactID = CBH.GuarantorID ";

            return stSQL;
        }

        private string SQLSelectCreditCardTypes()
        {
            string stSQL = "SELECT DISTINCT CBL.CreditCardTypeID, CBL.CreditFinanceCharge " +
                                    ",CBL.CreditLatePenaltyCharge " +
                                    ",CBL.CreditMinimumAmountDue " +
                                    ",CBL.CreditMinimumPercentageDue " +
                                    ",CBL.CreditFinanceCharge15th " +
                                    ",CBL.CreditLatePenaltyCharge15th " +
                                    ",CBL.CreditMinimumAmountDue15th " +
                                    ",CBL.CreditMinimumPercentageDue15th " +
                                    ",CBL.CreditPurcStartDateToProcess " +
                                    ",CBL.CreditPurcEndDateToProcess " +
                                    ",CBL.CreditCutOffDate " +
                                    ",CBL.CreditCardType " +
                                    ",CBL.WithGuarantor " +
                                    ",CBL.CreditUseLastDayCutOffDate, CBL.BillingDate  " +
                                    ",CTY.CardTypeCode ,CTY.CardTypeName " +
                            "FROM tblCreditBills CBL " +
                            "INNER JOIN tblCreditBillHeader CBH ON CBH.CreditBillID = CBL.CreditBillID " +
                            "INNER JOIN tblCardTypes CTY ON CTY.CardTypeID = CBL.CreditCardTypeID ";

            return stSQL;
        }

        #region Details

        public BillingDetails Details(Int64 CustomerID, DateTime LastBillingDate, bool CheckIsBillPrinted = false, bool IsBillPrinted = false)
        {
            try
            {
                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandType = System.Data.CommandType.Text;

                string SQL = SQLSelect();
                SQL += "WHERE 1=1 ";

                SQL += CheckIsBillPrinted ? (IsBillPrinted ? "AND IsBillPrinted = 1 " : "AND IsBillPrinted = 0 ") : "";

                if (LastBillingDate == Constants.C_DATE_MIN_VALUE)
                {
                    SQL += "AND CBL.BillingDate = (SELECT MAX(BillingDate) FROM tblCreditBills) ";
                }
                else
                {
                    SQL += "AND CBL.BillingDate = @BillingDate ";
                    cmd.Parameters.AddWithValue("BillingDate", LastBillingDate);
                }
                if (CustomerID != 0)
                {
                    SQL += "AND CUS.ContactID = @ContactID ";
                    cmd.Parameters.AddWithValue("ContactID", CustomerID);
                }

                cmd.CommandText = SQL;
                string strDataTableName = "tbl" + this.GetType().FullName.Split(new Char[] { '.' })[this.GetType().FullName.Split(new Char[] { '.' }).Length - 1]; System.Data.DataTable dt = new System.Data.DataTable(strDataTableName);
                base.MySqlDataAdapterFill(cmd, dt);

                BillingDetails Details = new BillingDetails();
                foreach (System.Data.DataRow dr in dt.Rows)
                {
                    Details.CreditBillHeaderID = Int64.Parse(dr["CreditBillHeaderID"].ToString());
                    Details.ContactID = Int64.Parse(dr["ContactID"].ToString());
                    Details.CrediLimit = decimal.Parse(dr["CreditLimit"].ToString());
                    Details.RunningCreditAmt = decimal.Parse(dr["RunningCreditAmt"].ToString());
                    Details.CurrMonthCreditAmt = decimal.Parse(dr["CurrMonthCreditAmt"].ToString());
                    Details.CurrMonthAmountPaid = decimal.Parse(dr["CurrMonthAmountPaid"].ToString());
                    Details.TotalBillCharges = decimal.Parse(dr["TotalBillCharges"].ToString());
                    Details.CurrentDueAmount = decimal.Parse(dr["CurrentDueAmount"].ToString());
                    Details.MinimumAmountDue = decimal.Parse(dr["MinimumAmountDue"].ToString());

                    Details.Prev1MoCurrentDueAmount = decimal.Parse(dr["Prev1MoCurrentDueAmount"].ToString());
                    Details.Prev1MoMinimumAmountDue = decimal.Parse(dr["Prev1MoMinimumAmountDue"].ToString());
                    Details.Prev1MoCurrMonthAmountPaid = decimal.Parse(dr["Prev1MoCurrMonthAmountPaid"].ToString());
                    Details.Prev2MoCurrentDueAmount = decimal.Parse(dr["Prev2MoCurrentDueAmount"].ToString());
                    Details.CurrentPurchaseAmt = decimal.Parse(dr["CurrentPurchaseAmt"].ToString());
                    Details.BeginningBalance = decimal.Parse(dr["BeginningBalance"].ToString());
                    Details.EndingBalance = decimal.Parse(dr["EndingBalance"].ToString());

                    Details.CreditCutOffDate = DateTime.Parse(dr["CreditCutOffDate"].ToString());
                    Details.CreditPaymentDueDate = DateTime.Parse(dr["CreditPaymentDueDate"].ToString());
                    Details.BillingDate = DateTime.Parse(dr["BillingDate"].ToString());
                    Details.CreditPurcStartDateToProcess = DateTime.Parse(dr["CreditPurcStartDateToProcess"].ToString());
                    Details.CreditPurcEndDateToProcess = DateTime.Parse(dr["CreditPurcEndDateToProcess"].ToString());
                    Details.BillingFile = dr["BillingFile"].ToString();
                    Details.isBillPrinted = bool.Parse(dr["isBillPrinted"].ToString());

                    Details.CardTypeDetails = new Data.CardTypeDetails()
                    {
                        CardTypeID = Int16.Parse(dr["CreditCardTypeID"].ToString()),
                        CardTypeCode = dr["CardTypeCode"].ToString(),
                        CardTypeName = dr["CardTypeName"].ToString(),
                        CreditFinanceCharge = decimal.Parse(dr["CreditFinanceCharge"].ToString()),
                        CreditLatePenaltyCharge = decimal.Parse(dr["CreditLatePenaltyCharge"].ToString()),
                        CreditMinimumAmountDue = decimal.Parse(dr["CreditMinimumAmountDue"].ToString()),
                        CreditMinimumPercentageDue = decimal.Parse(dr["CreditMinimumPercentageDue"].ToString()),
                        CreditFinanceCharge15th = decimal.Parse(dr["CreditFinanceCharge15th"].ToString()),
                        CreditLatePenaltyCharge15th = decimal.Parse(dr["CreditLatePenaltyCharge15th"].ToString()),
                        CreditMinimumAmountDue15th = decimal.Parse(dr["CreditMinimumAmountDue15th"].ToString()),
                        CreditMinimumPercentageDue15th = decimal.Parse(dr["CreditMinimumPercentageDue15th"].ToString()),
                        CreditPurcStartDateToProcess = DateTime.Parse(dr["CreditPurcStartDateToProcess"].ToString()),
                        CreditPurcEndDateToProcess = DateTime.Parse(dr["CreditPurcEndDateToProcess"].ToString()),
                        CreditCutOffDate = DateTime.Parse(dr["CreditCutOffDate"].ToString()),
                        CreditCardType = (CreditCardTypes)Enum.Parse(typeof(CreditCardTypes), dr["CreditCardType"].ToString()),
                        WithGuarantor = bool.Parse(dr["WithGuarantor"].ToString()),
                        BillingDate = DateTime.Parse(dr["BillingDate"].ToString())
                    };


                    Customer clsCustomer = new Customer(base.Connection, base.Transaction);
                    Details.CustomerDetails = clsCustomer.Details(Details.ContactID);
                }

                return Details;
            }
            catch (Exception ex)
            {
                throw base.ThrowException(ex);
            }
        }

        //public DateTime getCreditPurcEndDateToProcess(CreditType CreditType)
        //{
        //    try
        //    {
        //        MySqlCommand cmd = new MySqlCommand();
        //        cmd.CommandType = System.Data.CommandType.Text;

        //        string SQL = "SELECT CreditPurcEndDateToProcess FROM tblCardTypes WHERE CardTypeCode = (SELECT ConfigValue FROM sysCreditConfig WHERE ConfigName = 'IndividualCardTypeCode') ";

        //        // do an override
        //        if (CreditType == RetailPlus.CreditType.Group)
        //            SQL = "SELECT CreditPurcEndDateToProcess FROM tblCardTypes WHERE CardTypeCode = (SELECT ConfigValue FROM sysCreditConfig WHERE ConfigName = 'GroupCardTypeCode') ";

        //        cmd.CommandText = SQL;
        //        string strDataTableName = "tbl" + this.GetType().FullName.Split(new Char[] { '.' })[this.GetType().FullName.Split(new Char[] { '.' }).Length - 1]; System.Data.DataTable dt = new System.Data.DataTable(strDataTableName);
        //        base.MySqlDataAdapterFill(cmd, dt);

        //        DateTime dteRetValue = DateTime.MaxValue;
        //        foreach(System.Data.DataRow dr in dt.Rows)
        //        {
        //            dteRetValue = DateTime.Parse(dr["CreditPurcEndDateToProcess"].ToString());
        //        }

        //        return dteRetValue;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw base.ThrowException(ex);
        //    }
        //}

        //public DateTime getBillingDate(CreditType CreditType)
        //{
        //    try
        //    {
        //        MySqlCommand cmd = new MySqlCommand();
        //        cmd.CommandType = System.Data.CommandType.Text;

        //        string SQL = "SELECT BillingDate FROM tblCardTypes WHERE CardTypeCode = (SELECT ConfigValue FROM sysCreditConfig WHERE ConfigName = 'IndividualCardTypeCode') ";

        //        // do an override if group
        //        if (CreditType == RetailPlus.CreditType.Group)
        //            SQL = "SELECT BillingDate FROM tblCardTypes WHERE CardTypeCode = (SELECT ConfigValue FROM sysCreditConfig WHERE ConfigName = 'GroupCardTypeCode') ";

        //        cmd.CommandText = SQL;
        //        string strDataTableName = "tbl" + this.GetType().FullName.Split(new Char[] { '.' })[this.GetType().FullName.Split(new Char[] { '.' }).Length - 1]; System.Data.DataTable dt = new System.Data.DataTable(strDataTableName);
        //        base.MySqlDataAdapterFill(cmd, dt);

        //        DateTime dteRetValue = Constants.C_DATE_MIN_VALUE;
        //        foreach (System.Data.DataRow dr in dt.Rows)
        //        {
        //            dteRetValue = DateTime.Parse(dr["BillingDate"].ToString());
        //        }

        //        return dteRetValue;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw base.ThrowException(ex);
        //    }
        //}

        //public CardTypeDetails getCreditCardTypeInfo(CreditType CreditType)
        //{
        //    try
        //    {
        //        MySqlCommand cmd = new MySqlCommand();
        //        cmd.CommandType = System.Data.CommandType.Text;

        //        string SQL = "SELECT ConfigValue FROM sysCreditConfig WHERE ConfigName = 'IndividualCardTypeCode' ";

        //        // do an override if group
        //        if (CreditType == RetailPlus.CreditType.Group)
        //            SQL = "SELECT ConfigValue FROM sysCreditConfig WHERE ConfigName = 'GroupCardTypeCode' ";

        //        cmd.CommandText = SQL;
        //        string strDataTableName = "tbl" + this.GetType().FullName.Split(new Char[] { '.' })[this.GetType().FullName.Split(new Char[] { '.' }).Length - 1]; System.Data.DataTable dt = new System.Data.DataTable(strDataTableName);
        //        base.MySqlDataAdapterFill(cmd, dt);

        //        string strCardTypeCode = string.Empty;
        //        foreach (System.Data.DataRow dr in dt.Rows)
        //        {
        //            strCardTypeCode = dr["ConfigValue"].ToString();
        //            return new CardType(base.Connection, base.Transaction).Details(strCardTypeCode);
        //        }

        //        return new CardTypeDetails();
        //    }
        //    catch (Exception ex)
        //    {
        //        throw base.ThrowException(ex);
        //    }
        //}

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
                Details.CurrentPurchaseAmt = decimal.Parse(dr["CurrentPurchaseAmt"].ToString());
                Details.BeginningBalance = decimal.Parse(dr["BeginningBalance"].ToString());
                Details.EndingBalance = decimal.Parse(dr["EndingBalance"].ToString());

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
                Details.isBillPrinted = bool.Parse(dr["isBillPrinted"].ToString());

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

        public System.Data.DataTable ListAsDataTable(Int64 GuarantorID = 0, Int64 ContactID = 0, Int16 CreditCardTypeID = 0, CreditType CreditType = CreditType.Both, DateTime? BillingDate = null, bool CheckIsBillPrinted = false, bool IsBillPrinted = false, string SortField = "CreditBillHeaderID", System.Data.SqlClient.SortOrder SortOrder = System.Data.SqlClient.SortOrder.Ascending, Int32 limit = 0)
        {
            try
            {
                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandType = System.Data.CommandType.Text;

                string SQL = SQLSelect() + "";
                SQL += "WHERE 1=1 ";

                if (CheckIsBillPrinted)
                {
                    SQL += "AND IsBillPrinted = @IsBillPrinted ";
                    cmd.Parameters.AddWithValue("IsBillPrinted", IsBillPrinted ? 1 : 0);
                }

                if (GuarantorID != 0)
                {
                    SQL += "AND CBH.GuarantorID = @GuarantorID ";
                    cmd.Parameters.AddWithValue("GuarantorID", GuarantorID);
                }
                if (ContactID != 0)
                {
                    SQL += "AND CBH.ContactID = @ContactID ";
                    cmd.Parameters.AddWithValue("ContactID", ContactID);
                }
                if (CreditType == CreditType.Group)
                { SQL += "AND CBH.GuarantorID <> 0 "; }
                else if (CreditType == CreditType.Individual)
                { SQL += "AND CBH.GuarantorID = 0 "; }

                if (CreditCardTypeID != 0)
                {
                    SQL += "AND CBL.CreditCardTypeID = @CreditCardTypeID ";
                    cmd.Parameters.AddWithValue("CreditCardTypeID", CreditCardTypeID);
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

        public System.Data.DataTable ListBillingDateAsDataTable(CreditType CreditType, Int64 CustomerID = 0, DateTime? BillingDate = null, Int16 CreditCardTypeID = 0, string SortField = "BillingDate", System.Data.SqlClient.SortOrder SortOrder = System.Data.SqlClient.SortOrder.Descending, Int32 limit = 0)
        {
            try
            {
                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandType = System.Data.CommandType.Text;

                string SQL = "SELECT DISTINCT DATE_FORMAT(CBH.BillingDate, '%Y-%m-%d') BillingDate, CBH.BillingFile FROM tblCreditBillHeader CBH " +
                             "INNER JOIN tblCreditBills CB ON CB.CreditBillID = CBH.CreditBillID WHERE 1=1 ";

                if (CreditType == CreditType.Individual)
                { SQL += "AND CB.WithGuarantor = 0 "; }
                if (CreditType == CreditType.Group)
                { SQL += "AND CB.WithGuarantor <> 0 "; }

                if (CustomerID != 0)
                {
                    SQL += "AND CBH.ContactID = @CustomerID ";
                    cmd.Parameters.AddWithValue("CustomerID", CustomerID);
                }
                if (BillingDate.GetValueOrDefault(Constants.C_DATE_MIN_VALUE) != Constants.C_DATE_MIN_VALUE)
                {
                    SQL += "AND CBH.BillingDate = @BillingDate ";
                    cmd.Parameters.AddWithValue("BillingDate", BillingDate);
                }
                if (CreditCardTypeID != 0)
                {
                    SQL += "AND CB.CreditCardTypeID = @CreditCardTypeID ";
                    cmd.Parameters.AddWithValue("CreditCardTypeID", CreditCardTypeID);
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

        public List<BillingDetails> List(Int64 GuarantorID = 0, Int64 ContactID = 0, Int16 CreditCardTypeID = 0, CreditType CreditType = CreditType.Both, DateTime? BillingDate = null, bool CheckIsBillPrinted = false, bool IsBillPrinted = false, string SortField = "ContactID", System.Data.SqlClient.SortOrder SortOrder = System.Data.SqlClient.SortOrder.Ascending, Int32 limit = 0)
        {
            try
            {
                System.Data.DataTable dt = ListAsDataTable(GuarantorID, ContactID, CreditCardTypeID, CreditType, BillingDate, CheckIsBillPrinted, IsBillPrinted, SortField, SortOrder, limit);

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

        public List<CardTypeDetails> ListUnPrintedCreditCardTypes(CreditType CreditType = CreditType.Both, string SortField = "CardTypeCode", System.Data.SqlClient.SortOrder SortOrder = System.Data.SqlClient.SortOrder.Ascending, Int32 limit = 0)
        {
            try
            {
                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandType = System.Data.CommandType.Text;

                string SQL = SQLSelectCreditCardTypes() + "";
                SQL += "WHERE CBH.IsBillPrinted = 0 ";

                if (CreditType == CreditType.Group)
                { SQL += "AND CBH.GuarantorID <> 0 "; }
                else if (CreditType == CreditType.Individual)
                { SQL += "AND CBH.GuarantorID = 0 "; }

                SQL += "ORDER BY " + (!string.IsNullOrEmpty(SortField) ? SortField : "CardTypeCode") + " ";
                SQL += SortOrder == System.Data.SqlClient.SortOrder.Ascending ? "ASC " : "DESC ";
                SQL += limit == 0 ? "" : "LIMIT " + limit.ToString() + " ";

                cmd.CommandText = SQL;
                string strDataTableName = "tbl" + this.GetType().FullName.Split(new Char[] { '.' })[this.GetType().FullName.Split(new Char[] { '.' }).Length - 1]; System.Data.DataTable dt = new System.Data.DataTable(strDataTableName);
                base.MySqlDataAdapterFill(cmd, dt);

                List<CardTypeDetails> lstRetValue = new List<CardTypeDetails>();
                foreach (DataRow dr in dt.Rows)
                {
                    lstRetValue.Add(new Data.CardTypeDetails() {
                        CardTypeID = Int16.Parse(dr["CreditCardTypeID"].ToString()),
                        CardTypeCode = dr["CardTypeCode"].ToString(),
                        CardTypeName = dr["CardTypeName"].ToString(),
                        CreditFinanceCharge = decimal.Parse(dr["CreditFinanceCharge"].ToString()),
                        CreditLatePenaltyCharge = decimal.Parse(dr["CreditLatePenaltyCharge"].ToString()),
                        CreditMinimumAmountDue = decimal.Parse(dr["CreditMinimumAmountDue"].ToString()),
                        CreditMinimumPercentageDue = decimal.Parse(dr["CreditMinimumPercentageDue"].ToString()),
                        CreditFinanceCharge15th = decimal.Parse(dr["CreditFinanceCharge15th"].ToString()),
                        CreditLatePenaltyCharge15th = decimal.Parse(dr["CreditLatePenaltyCharge15th"].ToString()),
                        CreditMinimumAmountDue15th = decimal.Parse(dr["CreditMinimumAmountDue15th"].ToString()),
                        CreditMinimumPercentageDue15th = decimal.Parse(dr["CreditMinimumPercentageDue15th"].ToString()),
                        CreditPurcStartDateToProcess = DateTime.Parse(dr["CreditPurcStartDateToProcess"].ToString()),
                        CreditPurcEndDateToProcess = DateTime.Parse(dr["CreditPurcEndDateToProcess"].ToString()),
                        CreditCutOffDate = DateTime.Parse(dr["CreditCutOffDate"].ToString()),
                        CreditCardType = (CreditCardTypes)Enum.Parse(typeof(CreditCardTypes), dr["CreditCardType"].ToString()),
                        WithGuarantor = bool.Parse(dr["WithGuarantor"].ToString()),
                        BillingDate = DateTime.Parse(dr["BillingDate"].ToString())
                    });
                }

                return lstRetValue;
            }
            catch (Exception ex)
            {
                throw base.ThrowException(ex);
            }
        }

        #endregion

        public void ProcessCurrentBill(CreditType CreditType, string CardTypeCode)
        {
            try
            {
                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandType = System.Data.CommandType.Text;

                string SQL = "CALL procProcessCreditBills(0, @CardTypeCode);";

                if (CreditType == RetailPlus.CreditType.Group)
                    SQL = "CALL procProcessCreditBillsWG(0, @CardTypeCode);";

                cmd.Parameters.AddWithValue("CardTypeCode", CardTypeCode);

                // overried if group
                cmd.CommandText = SQL;
                base.ExecuteNonQuery(cmd);
            }
            catch (Exception ex)
            {
                throw base.ThrowException(ex);
            }
        }

        public Int32 CloseCurrentBill(CreditType CreditType, string CardTypeCode)
        {
            try
            {
                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandType = System.Data.CommandType.Text;

                string SQL = "CALL procProcessCreditBillsClose(@CardTypeCode);";

                // overried if group
                if (CreditType == RetailPlus.CreditType.Group)
                    SQL = "CALL procProcessCreditBillsWGClose(@CardTypeCode);";

                cmd.Parameters.AddWithValue("CardTypeCode", CardTypeCode);

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

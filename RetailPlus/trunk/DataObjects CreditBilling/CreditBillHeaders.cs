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
	public struct CreditBillHeaderDetails
	{
		public Int64 CreditBillHeaderID;
        public Int64 CreditBillID;
        public Int64 ContactID;
        public Int64 GuarantorID;
        public decimal CreditLimit;
        public decimal RunningCreditAmt;
        public decimal CurrMonthCreditAmt;
        public decimal CurrMonthAmountPaid;
        public DateTime BillingDate;
        public string BillingFile;
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
        public DateTime CreatedOn;
        public Int64 CreatedByID;
        public string CreatedByName;
        public bool IsBillPrinted;
	}

	[StrongNameIdentityPermissionAttribute(SecurityAction.LinkDemand,
		 PublicKey = "002400000480000094000000060200000024000" +
		 "052534131000400000100010053D785642F9F960B43157E0380" +
		 "F393BEE53E8DFAFBF441366C1B6F8B48D9DDF0D527B1F3B21EA" +
		 "E85D2FDB664CE81EB8A87DBE4C589D6F4202FE2B7D4B978BB69" +
		 "684874612CB9B8DB7A0339400A9C4E68277884B07817363D242" +
		 "E3696F9FACDBEA831810AE6DC9EDCA91A7B5DA12FE7BF65D113" +
		 "FF52834EAFB5A7A1FDFD5851A3")]
	public class CreditBillHeaders : POSConnection
    {
		#region Constructors and Destructors

		public CreditBillHeaders()
            : base(null, null)
        {
        }

        public CreditBillHeaders(MySqlConnection Connection, MySqlTransaction Transaction) 
            : base(Connection, Transaction)
		{

		}

		#endregion


        private string SQLSelect()
        {
            string stSQL = "SELECT " +
					            "CreditBillHeaderID, CreditBillID, ContactID, GuarantorID, CreditLimit, RunningCreditAmt, CurrMonthCreditAmt " +
                                ",CurrMonthAmountPaid, BillingDate, BillingFile, TotalBillCharges, CurrentDueAmount, MinimumAmountDue " +
                                ",Prev1MoCurrentDueAmount, Prev1MoMinimumAmountDue, Prev1MoCurrMonthAmountPaid, Prev2MoCurrentDueAmount " +
                                ",CurrentPurchaseAmt, BeginningBalance, EndingBalance, CreatedOn, CreatedByID, CreatedByName, IsBillPrinted " +
					        "FROM tblCreditBillHeader ";

            return stSQL;
        }

        public Int64 Insert(CreditBillHeaderDetails Details)
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
        public Int32 Save(CreditBillHeaderDetails Details)
        {
            try
            {
                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandType = System.Data.CommandType.Text;

                string SQL = "INSERT INTO tblCreditBillHeaders(CreditBillID, ContactID, GuarantorID, CreditLimit, RunningCreditAmt, CurrMonthCreditAmt " +
                                ",CurrMonthAmountPaid, BillingDate, BillingFile, TotalBillCharges, CurrentDueAmount, MinimumAmountDue " +
                                ",Prev1MoCurrentDueAmount, Prev1MoMinimumAmountDue, Prev1MoCurrMonthAmountPaid, Prev2MoCurrentDueAmount " +
                                ",CurrentPurchaseAmt, BeginningBalance, EndingBalance, CreatedOn, CreatedByID, CreatedByName, IsBillPrinted )VALUES(";
                SQL += "@CreditBillID, @ContactID, @GuarantorID, @CreditLimit, @RunningCreditAmt, @CurrMonthCreditAmt " +
                                ",@CurrMonthAmountPaid, @BillingDate, @BillingFile, @TotalBillCharges, @CurrentDueAmount, @MinimumAmountDue " +
                                ",@Prev1MoCurrentDueAmount, @Prev1MoMinimumAmountDue, @Prev1MoCurrMonthAmountPaid, @Prev2MoCurrentDueAmount " +
                                ",@CurrentPurchaseAmt, @BeginningBalance, @EndingBalance, @CreatedOn, @CreatedByID, @CreatedByName, @IsBillPrinted )";

                cmd.Parameters.AddWithValue("CreditBillID", Details.CreditBillID);
                cmd.Parameters.AddWithValue("ContactID", Details.ContactID);
                cmd.Parameters.AddWithValue("GuarantorID", Details.GuarantorID);
                cmd.Parameters.AddWithValue("CreditLimit", Details.CreditLimit);
                cmd.Parameters.AddWithValue("RunningCreditAmt", Details.RunningCreditAmt);
                cmd.Parameters.AddWithValue("CurrMonthCreditAmt", Details.CurrMonthCreditAmt);
                cmd.Parameters.AddWithValue("CurrMonthAmountPaid", Details.CurrMonthAmountPaid);
                cmd.Parameters.AddWithValue("BillingDate", Details.BillingDate);
                cmd.Parameters.AddWithValue("BillingFile", Details.BillingFile);
                cmd.Parameters.AddWithValue("TotalBillCharges", Details.TotalBillCharges);
                cmd.Parameters.AddWithValue("CreditMinimumPercentageDue15th", Details.CurrentDueAmount);
                cmd.Parameters.AddWithValue("MinimumAmountDue", Details.MinimumAmountDue);
                cmd.Parameters.AddWithValue("Prev1MoCurrentDueAmount", Details.Prev1MoCurrentDueAmount);
                cmd.Parameters.AddWithValue("Prev1MoMinimumAmountDue", Details.Prev1MoMinimumAmountDue);
                cmd.Parameters.AddWithValue("Prev1MoCurrMonthAmountPaid", Details.Prev1MoCurrMonthAmountPaid);
                cmd.Parameters.AddWithValue("Prev2MoCurrentDueAmount", Details.Prev2MoCurrentDueAmount);
                cmd.Parameters.AddWithValue("CurrentPurchaseAmt", Details.CurrentPurchaseAmt);
                cmd.Parameters.AddWithValue("BeginningBalance", Details.BeginningBalance);
                cmd.Parameters.AddWithValue("EndingBalance", Details.EndingBalance);
                cmd.Parameters.AddWithValue("CreatedOn", Details.CreatedOn == DateTime.MinValue ? Constants.C_DATE_MIN_VALUE : Details.CreatedOn);
                cmd.Parameters.AddWithValue("CreatedByID", Details.CreatedByID);
                cmd.Parameters.AddWithValue("CreatedByName", Details.CreatedByName);
                cmd.Parameters.AddWithValue("IsBillPrinted", Details.IsBillPrinted);

                cmd.CommandText = SQL;
                return base.ExecuteNonQuery(cmd);
            }
            catch (Exception ex)
            {
                throw base.ThrowException(ex);
            }
        }

        public Int32 OverWriteBillingNoG(CreditBillHeaderDetails Details)
        {
            try
            {
                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandType = System.Data.CommandType.Text;

                string SQL = "UPDATE tblCreditBillHeader SET " +
                             "      Prev2MoCurrentDueAmount = @Prev2MoCurrentDueAmount, " +
                             "      Prev1MoCurrentDueAmount = @Prev1MoCurrentDueAmount, " +
                             "      CurrMonthAmountPaid     = @CurrMonthAmountPaid, " +
                             "      TotalBillCharges        = @TotalBillCharges, " +
                             "      CurrentPurchaseAmt      = @CurrentPurchaseAmt, " +
                             "      MinimumAmountDue        = @MinimumAmountDue, " +
                             "      CurrentDueAmount        = @CurrentDueAmount " +
                             "WHERE CreditBillHeaderID=@CreditBillHeaderID AND BillingDate=@BillingDate;";

                cmd.Parameters.AddWithValue("Prev2MoCurrentDueAmount", Details.Prev2MoCurrentDueAmount);
                cmd.Parameters.AddWithValue("Prev1MoCurrentDueAmount", Details.Prev1MoCurrentDueAmount);
                cmd.Parameters.AddWithValue("CurrMonthAmountPaid", Details.CurrMonthAmountPaid);
                cmd.Parameters.AddWithValue("TotalBillCharges", Details.TotalBillCharges);
                cmd.Parameters.AddWithValue("CurrentPurchaseAmt", Details.CurrentPurchaseAmt);
                cmd.Parameters.AddWithValue("MinimumAmountDue", Details.MinimumAmountDue);
                cmd.Parameters.AddWithValue("CurrentDueAmount", Details.CurrentDueAmount);
                cmd.Parameters.AddWithValue("CreditBillHeaderID", Details.CreditBillHeaderID);
                cmd.Parameters.AddWithValue("BillingDate", Details.BillingDate);

                cmd.CommandText = SQL;
                return base.ExecuteNonQuery(cmd);
            }
            catch (Exception ex)
            {
                throw base.ThrowException(ex);
            }
        }

        public Int32 OverWriteBilling(CreditBillHeaderDetails Details)
        {
            try
            {
                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandType = System.Data.CommandType.Text;

                string SQL = "UPDATE tblCreditBillHeader SET " +
                             "      BeginningBalance        = @BeginningBalance, " +
                             "      Prev1MoCurrentDueAmount = @Prev1MoCurrentDueAmount, " +
                             "      CurrMonthAmountPaid     = @CurrMonthAmountPaid, " +
                             "      RunningCreditAmt        = @RunningCreditAmt, " +
                             "      TotalBillCharges        = @TotalBillCharges, " +
                             "      CurrentPurchaseAmt      = @CurrentPurchaseAmt, " +
                             "      CurrMonthCreditAmt      = @CurrMonthCreditAmt, " +
                             "      CurrentDueAmount        = @CurrentDueAmount, " +
                             "      EndingBalance           = @EndingBalance " +
                             "WHERE CreditBillHeaderID=@CreditBillHeaderID AND BillingDate=@BillingDate;";

                cmd.Parameters.AddWithValue("BeginningBalance", Details.BeginningBalance);
                cmd.Parameters.AddWithValue("Prev1MoCurrentDueAmount", Details.Prev1MoCurrentDueAmount);
                cmd.Parameters.AddWithValue("CurrMonthAmountPaid", Details.CurrMonthAmountPaid);
                cmd.Parameters.AddWithValue("RunningCreditAmt", Details.RunningCreditAmt);
                cmd.Parameters.AddWithValue("TotalBillCharges", Details.TotalBillCharges);
                cmd.Parameters.AddWithValue("CurrentPurchaseAmt", Details.CurrentPurchaseAmt);
                cmd.Parameters.AddWithValue("CurrMonthCreditAmt", Details.CurrMonthCreditAmt);
                cmd.Parameters.AddWithValue("CurrentDueAmount", Details.CurrentDueAmount);
                cmd.Parameters.AddWithValue("EndingBalance", Details.EndingBalance);
                cmd.Parameters.AddWithValue("CreditBillHeaderID", Details.CreditBillHeaderID);
                cmd.Parameters.AddWithValue("BillingDate", Details.BillingDate);

                cmd.CommandText = SQL;
                return base.ExecuteNonQuery(cmd);
            }
            catch (Exception ex)
            {
                throw base.ThrowException(ex);
            }
        }

        public bool setIsBillPrintedNoG(Int64 ContactID, DateTime BillingDate, bool isBillPrinted)
        {
            try
            {
                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandType = System.Data.CommandType.Text;

                string SQL = "UPDATE tblCreditBillHeader SET isBillPrinted = @isBillPrinted " +
                             "WHERE ContactID=@ContactID AND BillingDate=@BillingDate;";

                cmd.Parameters.AddWithValue("isBillPrinted", isBillPrinted);
                cmd.Parameters.AddWithValue("ContactID", ContactID);
                cmd.Parameters.AddWithValue("BillingDate", BillingDate);

                cmd.CommandText = SQL;
                base.ExecuteNonQuery(cmd);

                return true;
            }
            catch (Exception ex)
            {
                throw base.ThrowException(ex);
            }
        }

        public bool setIsBillPrinted(Int64 GuarantorID, DateTime BillingDate, bool isBillPrinted)
        {
            try
            {
                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandType = System.Data.CommandType.Text;

                string SQL = "UPDATE tblCreditBillHeader SET isBillPrinted = @isBillPrinted " +
                             "WHERE GuarantorID=@GuarantorID AND BillingDate=@BillingDate;";

                cmd.Parameters.AddWithValue("isBillPrinted", isBillPrinted);
                cmd.Parameters.AddWithValue("GuarantorID", GuarantorID);
                cmd.Parameters.AddWithValue("BillingDate", BillingDate);

                cmd.CommandText = SQL;
                base.ExecuteNonQuery(cmd);

                return true;
            }
            catch (Exception ex)
            {
                throw base.ThrowException(ex);
            }
        }

		#region Details

		public CreditBillHeaderDetails Details(Int64 CreditBillHeaderID)
		{
			try
			{
                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandType = System.Data.CommandType.Text;

                string SQL = SQLSelect() + "WHERE CreditBillHeaderID = @CreditBillHeaderID;";

                cmd.Parameters.AddWithValue("@CreditBillHeaderID", CreditBillHeaderID);

                cmd.CommandText = SQL;
                string strDataTableName = "tbl" + this.GetType().FullName.Split(new Char[] { '.' })[this.GetType().FullName.Split(new Char[] { '.' }).Length - 1]; System.Data.DataTable dt = new System.Data.DataTable(strDataTableName);
                base.MySqlDataAdapterFill(cmd, dt);

                CreditBillHeaderDetails Details = setDetails(dt);

                return Details;
			}
			catch (Exception ex)
			{
				throw base.ThrowException(ex);
			}	
		}

        public CreditBillHeaderDetails Details(Int64 ContactID, DateTime BillingDate)
        {
            try
            {
                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandType = System.Data.CommandType.Text;

                string SQL = SQLSelect() + "WHERE AND ContactID = @ContactID AND BillingDate = @BillingDate LIMIT 1 ";

                cmd.Parameters.AddWithValue("@ContactID", ContactID);
                cmd.Parameters.AddWithValue("@BillingDate", BillingDate);

                cmd.CommandText = SQL;
                string strDataTableName = "tbl" + this.GetType().FullName.Split(new Char[] { '.' })[this.GetType().FullName.Split(new Char[] { '.' }).Length - 1]; System.Data.DataTable dt = new System.Data.DataTable(strDataTableName);
                base.MySqlDataAdapterFill(cmd, dt);

                CreditBillHeaderDetails Details = setDetails(dt);

                return Details;
            }
            catch (Exception ex)
            {
                throw base.ThrowException(ex);
            }
        }

        public CreditBillHeaderDetails setDetails(System.Data.DataTable dt)
        {
            CreditBillHeaderDetails Details = new CreditBillHeaderDetails();
            foreach (System.Data.DataRow dr in dt.Rows)
            {
                Details.CreditBillHeaderID = Int64.Parse(dr["CreditBillHeaderID"].ToString());
                Details.CreditBillID = Int64.Parse(dr["CreditBillID"].ToString());
                Details.ContactID = Int64.Parse(dr["ContactID"].ToString());
                Details.GuarantorID = Int64.Parse(dr["GuarantorID"].ToString());
                Details.CreditLimit = decimal.Parse(dr["CreditLimit"].ToString());
                Details.RunningCreditAmt = decimal.Parse(dr["RunningCreditAmt"].ToString());
                Details.CurrMonthCreditAmt = decimal.Parse(dr["CurrMonthCreditAmt"].ToString());
                Details.CurrMonthAmountPaid = decimal.Parse(dr["CurrMonthAmountPaid"].ToString());
                Details.BillingDate = DateTime.Parse(dr["BillingDate"].ToString());
                Details.BillingFile = dr["BillingFile"].ToString();
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
                Details.CreatedOn = DateTime.Parse(dr["CreatedOn"].ToString());
                Details.CreatedByID = Int64.Parse(dr["CreatedByID"].ToString());
                Details.CreatedByName = dr["CreatedByName"].ToString();
                Details.IsBillPrinted = bool.Parse(dr["IsBillPrinted"].ToString());
            }
            return Details;
        }

		#endregion

	}
}


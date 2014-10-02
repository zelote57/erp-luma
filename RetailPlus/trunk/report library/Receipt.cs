using System;
using System.Security.Permissions;
using MySql.Data.MySqlClient;

/******************************************************************************
	**		Auth: Lemuel E. Aceron
	**		Date: Feb 13, 2008
	***************************************************************************
	**		Change History
	***************************************************************************
	**		Date:			Author:				Description:
	**		--------		--------			-------------------------------
	**      
	***************************************************************************/

namespace AceSoft.RetailPlus.Reports
{

	[StrongNameIdentityPermissionAttribute(SecurityAction.LinkDemand,
		 PublicKey = "002400000480000094000000060200000024000" +
		 "052534131000400000100010053D785642F9F960B43157E0380" +
		 "F393BEE53E8DFAFBF441366C1B6F8B48D9DDF0D527B1F3B21EA" +
		 "E85D2FDB664CE81EB8A87DBE4C589D6F4202FE2B7D4B978BB69" +
		 "684874612CB9B8DB7A0339400A9C4E68277884B07817363D242" +
		 "E3696F9FACDBEA831810AE6DC9EDCA91A7B5DA12FE7BF65D113" +
		 "FF52834EAFB5A7A1FDFD5851A3")]
	public struct ReceiptDetails
	{
        public Int32 ReceiptID;
		public string Module;
		public string Text;
		public string Value;
		public ReportFormatOrientation Orientation;

        public DateTime CreatedOn;
        public DateTime LastModified;
	}

    public enum TerminalReportType
    {
        ZRead,
        XRead,
        CashiersTerminalReport,
        TerminalReport
    }

    public struct ReceiptFieldFormats
    {
        public static string CompanyCode = CompanyDetails.CompanyCode;
        public static string CompanyName = CompanyDetails.CompanyName;
        public static string Address1 = CompanyDetails.Address1 + " " + CompanyDetails.Address2;
        public static string Address2 = CompanyDetails.City + ", " + CompanyDetails.State + " " + CompanyDetails.Country + " " + CompanyDetails.Zip;
        public static string OfficePhone = CompanyDetails.OfficePhone;
        public static string DirectPhone = CompanyDetails.DirectPhone;
        public static string FaxPhone = CompanyDetails.FaxPhone;
        public static string MobilePhone = CompanyDetails.MobilePhone;
        public static string EmailAddress = CompanyDetails.EmailAddress;
        public static string WebSite = CompanyDetails.WebSite;
        public static string TIN = "TIN: " + CompanyDetails.TIN;
        public static string Blank = "{Blank}";
        public static string Spacer = "{NewLine}";
        public static string Terminator = "-/-";
        public static string WelcomeNote1 = System.Configuration.ConfigurationManager.AppSettings["WelcomeNote1"].ToString();
        public static string WelcomeNote2 = System.Configuration.ConfigurationManager.AppSettings["WelcomeNote2"].ToString();
        public static string WelcomeNote3 = System.Configuration.ConfigurationManager.AppSettings["WelcomeNote3"].ToString();
        public static string WelcomeNote4 = System.Configuration.ConfigurationManager.AppSettings["WelcomeNote4"].ToString();
        public static string WelcomeNote5 = System.Configuration.ConfigurationManager.AppSettings["WelcomeNote5"].ToString();
        public static string ClosingNote1 = System.Configuration.ConfigurationManager.AppSettings["ClosingNote1"].ToString();
        public static string ClosingNote2 = System.Configuration.ConfigurationManager.AppSettings["ClosingNote2"].ToString();
        public static string ClosingNote3 = System.Configuration.ConfigurationManager.AppSettings["ClosingNote3"].ToString();
        public static string ClosingNote4 = System.Configuration.ConfigurationManager.AppSettings["ClosingNote4"].ToString();
        public static string ClosingNote5 = System.Configuration.ConfigurationManager.AppSettings["ClosingNote5"].ToString();
        public static string CheckCounter = "{Check Counter}";
        public static string InvoiceNo = "{InvoiceNo}";
        public static string ORNo = "{ORNo}";
        public static string DateNow = "{DateNow}";
        public static string TransactionDate = "{TransactionDate}";
        public static string Cashier = "{Cashier}";
        public static string TerminalNo = "{TerminalNo}";
        public static string MachineSerialNo = "{MachineSerialNo}";
        public static string AccreditationNo = "{AccreditationNo}";

        // 13Feb08 added the following due to tblreceipt
        public static string Spacer2 = "----------------------------------------";
        public static string SubTotal = "{SUBTOTAL}";
        public static string OtherCharges = "{OTH CHARGE}";
        public static string CreditChargeAmount = "{IN-HOUSE CREDIT CHARGE}";
        public static string Discount = "{DISCOUNT}";
        public static string AmountDue = "{AMOUNT DUE}";
        public static string AmountTender = "{AMOUNT TENDER}";
        public static string Change = "{CHANGE}";
        public static string VATExempt = "{VAT Exempt}";
        public static string VATZeroRated = "{VAT Zero-Rated}";
        public static string NonVATableAmount = "{NON-VAT AMT}";
        public static string VATableAmount = "{VATABLE AMT}";
        public static string VAT = "{VAT}";
        public static string TotalItemSold = "{TTL ITEM SOLD}";
        public static string TotalQtySold = "{TTL QTY SOLD}";
        public static string CustomerName = "{Customer Name}";
        public static string WaiterName = "{Waiter Name}";
        public static string BaggerName = "{Bagger Name}";
        // 02May09 added for restaurants
        public static string OrderType = "{Order Type}";
        public static string CheckOutBillFooter = "{CheckOut Bill Footer}";
        // 12May09 added for TGP
        public static string DiscountCode = "{DiscountCode}";
        public static string DiscountRemarks = "{DiscountRemarks}";
        public static string ChargeCode = "{ChargeCode}";
        public static string ChargeRemarks = "{ChargeRemarks}";
        // 25Oct11 added for RewardPoints
        public static string RewardCardNo = "{RewardCardNo}";
        public static string RewardPreviousPoints = "{RewardPreviousPoints}";
        public static string RewardEarnedPoints = "{RewardEarnedPoints}";
        public static string RewardCurrentPoints = "{RewardCurrentPoints}";
        // 05Nov11 added for Permit Nos.
        public static string RewardsPermitNo = "{RewardsPermitNo}";
        public static string InHouseIndividualCreditPermitNo = "{InHouseIndividualCreditPermitNo}";
        public static string InHouseGroupCreditPermitNo = "{InHouseGroupCreditPermitNo}";

    }
	[StrongNameIdentityPermissionAttribute(SecurityAction.LinkDemand,
		 PublicKey = "002400000480000094000000060200000024000" +
		 "052534131000400000100010053D785642F9F960B43157E0380" +
		 "F393BEE53E8DFAFBF441366C1B6F8B48D9DDF0D527B1F3B21EA" +
		 "E85D2FDB664CE81EB8A87DBE4C589D6F4202FE2B7D4B978BB69" +
		 "684874612CB9B8DB7A0339400A9C4E68277884B07817363D242" +
		 "E3696F9FACDBEA831810AE6DC9EDCA91A7B5DA12FE7BF65D113" +
		 "FF52834EAFB5A7A1FDFD5851A3")]
	public class Receipt : POSConnection
	{

		#region Constructors and Destructors

		public Receipt()
            : base(null, null)
        {
        }

        public Receipt(MySqlConnection Connection, MySqlTransaction Transaction) 
            : base(Connection, Transaction)
		{

		}

		#endregion

		#region Insert and Update

		public void Update(ReceiptDetails Details)
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

        public Int32 Save(ReceiptDetails Details)
        {
            try
            {
                string SQL = "CALL procSaveReceipt(@ReceiptID, @Module, @Text, @Value, @Orientation, @CreatedOn, @LastModified);";

                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandText = SQL;

                cmd.Parameters.AddWithValue("ReceiptID", Details.ReceiptID);
                cmd.Parameters.AddWithValue("Module", Details.Module);
                cmd.Parameters.AddWithValue("Text", Details.Text);
                cmd.Parameters.AddWithValue("Value", Details.Value);
                cmd.Parameters.AddWithValue("Orientation", Details.Orientation.ToString("d"));
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

        private string SQLSelect()
        {
            string stSQL = "SELECT Module, Text, Value, Orientation FROM tblReceipt ";

            return stSQL;
        }

		#region Details

		public ReceiptDetails Details(string Module)
		{
			try
			{
                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandType = System.Data.CommandType.Text;

				string SQL=	SQLSelect() + "WHERE Module = @Module;"; 
				  
                cmd.Parameters.AddWithValue("Module", Module);

                cmd.CommandText = SQL;
                string strDataTableName = "tbl" + this.GetType().FullName.Split(new Char[] { '.' })[this.GetType().FullName.Split(new Char[] { '.' }).Length - 1]; System.Data.DataTable dt = new System.Data.DataTable(strDataTableName);
                base.MySqlDataAdapterFill(cmd, dt);
                
                ReceiptDetails Details = new ReceiptDetails();
				foreach (System.Data.DataRow dr in dt.Rows)
				{
					Details.Module = "" + dr["Module"].ToString();
					Details.Text = "" + dr["Text"].ToString();
					Details.Value = "" + dr["Value"].ToString();
					Details.Orientation = (ReportFormatOrientation) Enum.Parse(typeof(ReportFormatOrientation), dr["Orientation"].ToString());
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

        public System.Data.DataTable ListAsDataTable(ReceiptDetails ReceiptDetails, string SortField = "ReceiptID", SortOption SortOrder = SortOption.Ascending, Int32 limit = 0)
        {
            try
            {
                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandType = System.Data.CommandType.Text;

                string SQL = SQLSelect() + "WHERE 1=1 ";

                if (ReceiptDetails.ReceiptID != 0)
                {
                    SQL += "AND ReceiptID = @ReceiptID) ";
                    cmd.Parameters.AddWithValue("@ReceiptID", ReceiptDetails.ReceiptID);
                }
                if (!string.IsNullOrEmpty(ReceiptDetails.Module))
                {
                    SQL += "AND Module = @Module) ";
                    cmd.Parameters.AddWithValue("@Module", ReceiptDetails.Module);
                }
                if (!string.IsNullOrEmpty(ReceiptDetails.Value))
                {
                    SQL += "AND Value = @Value) ";
                    cmd.Parameters.AddWithValue("@Value", ReceiptDetails.Value);
                }
                

                SQL += "ORDER BY " + SortField + " ";
                SQL += SortOrder == SortOption.Ascending ? "ASC " : "DESC ";
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


		#endregion
	}
}
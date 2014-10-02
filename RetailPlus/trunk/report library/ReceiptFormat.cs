//using System;
//using System.Security.Permissions;
//using MySql.Data.MySqlClient;

///******************************************************************************
//    **		Auth: Lemuel E. Aceron
//    **		Date: March 29, 2005
//    ***************************************************************************
//    **		Change History
//    ***************************************************************************
//    **		Date:			Author:				Description:
//    **		--------		--------			-------------------------------
//    **      
//    ***************************************************************************/

//namespace AceSoft.RetailPlus.Reports
//{

//    public enum TerminalReportType
//    {
//        ZRead,
//        XRead,
//        CashiersTerminalReport,
//        TerminalReport
//    }

	
//    [StrongNameIdentityPermissionAttribute(SecurityAction.LinkDemand,
//         PublicKey = "002400000480000094000000060200000024000" +
//         "052534131000400000100010053D785642F9F960B43157E0380" +
//         "F393BEE53E8DFAFBF441366C1B6F8B48D9DDF0D527B1F3B21EA" +
//         "E85D2FDB664CE81EB8A87DBE4C589D6F4202FE2B7D4B978BB69" +
//         "684874612CB9B8DB7A0339400A9C4E68277884B07817363D242" +
//         "E3696F9FACDBEA831810AE6DC9EDCA91A7B5DA12FE7BF65D113" +
//         "FF52834EAFB5A7A1FDFD5851A3")]
//    public struct ReceiptFormatDetails
//    {
//        public int ReportHeaderSpacer;
//        public string ReportHeader1;
//        public string ReportHeader2;
//        public string ReportHeader3;
//        public string ReportHeader4;
//        public string PageHeader1;
//        public string PageHeader2;
//        public string PageHeader3;
//        public string PageFooter1;
//        public string PageFooter2;
//        public string PageFooter3;
//        public string ReportFooter1;
//        public string ReportFooter2;
//        public string ReportFooter3;
//        public int ReportFooterSpacer;
//    }

	
//    [StrongNameIdentityPermissionAttribute(SecurityAction.LinkDemand,
//         PublicKey = "002400000480000094000000060200000024000" +
//         "052534131000400000100010053D785642F9F960B43157E0380" +
//         "F393BEE53E8DFAFBF441366C1B6F8B48D9DDF0D527B1F3B21EA" +
//         "E85D2FDB664CE81EB8A87DBE4C589D6F4202FE2B7D4B978BB69" +
//         "684874612CB9B8DB7A0339400A9C4E68277884B07817363D242" +
//         "E3696F9FACDBEA831810AE6DC9EDCA91A7B5DA12FE7BF65D113" +
//         "FF52834EAFB5A7A1FDFD5851A3")]
//    public struct ReceiptFieldFormats
//    {
//        public static string CompanyCode			=	CompanyDetails.CompanyCode;
//        public static string CompanyName			=	CompanyDetails.CompanyName;
//        public static string Address1				=	CompanyDetails.Address1 + " " + CompanyDetails.Address2;
//        public static string Address2				=	CompanyDetails.City + ", " + CompanyDetails.State + " " + CompanyDetails.Country + " " + CompanyDetails.Zip;
//        public static string OfficePhone			=	CompanyDetails.OfficePhone;
//        public static string DirectPhone			=	CompanyDetails.DirectPhone;
//        public static string FaxPhone				=	CompanyDetails.FaxPhone;
//        public static string MobilePhone			=	CompanyDetails.MobilePhone;
//        public static string EmailAddress			=	CompanyDetails.EmailAddress;
//        public static string WebSite				=	CompanyDetails.WebSite;
//        public static string TIN					=	"TIN: " + CompanyDetails.TIN;
//        public static string Blank					=	"{Blank}";
//        public static string Spacer					=	"{NewLine}";
//        public static string Terminator				=	"-/-";
//        public static string WelcomeNote1			=	System.Configuration.ConfigurationManager.AppSettings["WelcomeNote1"].ToString();
//        public static string WelcomeNote2           =   System.Configuration.ConfigurationManager.AppSettings["WelcomeNote2"].ToString();
//        public static string WelcomeNote3           =   System.Configuration.ConfigurationManager.AppSettings["WelcomeNote3"].ToString();
//        public static string WelcomeNote4           =   System.Configuration.ConfigurationManager.AppSettings["WelcomeNote4"].ToString();
//        public static string WelcomeNote5           =   System.Configuration.ConfigurationManager.AppSettings["WelcomeNote5"].ToString();
//        public static string ClosingNote1           =   System.Configuration.ConfigurationManager.AppSettings["ClosingNote1"].ToString();
//        public static string ClosingNote2           =   System.Configuration.ConfigurationManager.AppSettings["ClosingNote2"].ToString();
//        public static string ClosingNote3           =   System.Configuration.ConfigurationManager.AppSettings["ClosingNote3"].ToString();
//        public static string ClosingNote4           =   System.Configuration.ConfigurationManager.AppSettings["ClosingNote4"].ToString();
//        public static string ClosingNote5           =   System.Configuration.ConfigurationManager.AppSettings["ClosingNote5"].ToString();
//        public static string InvoiceNo				=	"{InvoiceNo}";
//        public static string ORNo                   =   "{ORNo}";
//        public static string DateNow				=	"{DateNow}";
//        public static string TransactionDate		=	"{TransactionDate}";
//        public static string Cashier				=	"{Cashier}";
//        public static string TerminalNo				=	"{TerminalNo}";
//        public static string MachineSerialNo		=	"{MachineSerialNo}";
//        public static string AccreditationNo		=	"{AccreditationNo}";
		
//        // 13Feb08 added the following due to tblreceipt
//        public static string Spacer2				=	"----------------------------------------";
//        public static string SubTotal				=	"{SUBTOTAL}";
//        public static string OtherCharges			=	"{OTH CHARGE}";
//        public static string CreditChargeAmount     =   "{IN-HOUSE CREDIT CHARGE}";
//        public static string Discount				=	"{DISCOUNT}";
//        public static string AmountDue				=	"{AMOUNT DUE}";
//        public static string AmountTender			=	"{AMOUNT TENDER}";
//        public static string Change					=	"{CHANGE}";
//        public static string VATExempt = "{VAT Exempt}";
//        public static string NonVATableAmount = "{VAT Zero-Rated}";
//        public static string VATableAmount = "{VATABLE AMT}";
//        public static string VAT					=	"{VAT}";
//        public static string TotalItemSold			=	"{TTL ITEM SOLD}";
//        public static string TotalQtySold			=	"{TTL QTY SOLD}";
//        public static string CustomerName			=	"{Customer Name}";
//        public static string WaiterName				=	"{Waiter Name}";
//        public static string BaggerName				=	"{Bagger Name}";
//        // 02May09 added for restaurants
//        public static string OrderType              =   "{Order Type}";
//        public static string CheckOutBillFooter     =   "{CheckOut Bill Footer}";
//        // 12May09 added for TGP
//        public static string DiscountCode           =   "{DiscountCode}";
//        public static string DiscountRemarks        =   "{DiscountRemarks}";
//        public static string ChargeCode             =   "{ChargeCode}";
//        public static string ChargeRemarks          =   "{ChargeRemarks}";
//        // 25Oct11 added for RewardPoints
//        public static string RewardCardNo           =   "{RewardCardNo}";
//        public static string RewardPreviousPoints   =   "{RewardPreviousPoints}";
//        public static string RewardEarnedPoints     =   "{RewardEarnedPoints}";
//        public static string RewardCurrentPoints    =   "{RewardCurrentPoints}";
//        // 05Nov11 added for Permit Nos.
//        public static string RewardsPermitNo = "{RewardsPermitNo}";
//        public static string InHouseIndividualCreditPermitNo = "{InHouseIndividualCreditPermitNo}";
//        public static string InHouseGroupCreditPermitNo = "{InHouseGroupCreditPermitNo}";
		
//    }

	
//    [StrongNameIdentityPermissionAttribute(SecurityAction.LinkDemand,
//         PublicKey = "002400000480000094000000060200000024000" +
//         "052534131000400000100010053D785642F9F960B43157E0380" +
//         "F393BEE53E8DFAFBF441366C1B6F8B48D9DDF0D527B1F3B21EA" +
//         "E85D2FDB664CE81EB8A87DBE4C589D6F4202FE2B7D4B978BB69" +
//         "684874612CB9B8DB7A0339400A9C4E68277884B07817363D242" +
//         "E3696F9FACDBEA831810AE6DC9EDCA91A7B5DA12FE7BF65D113" +
//         "FF52834EAFB5A7A1FDFD5851A3")]
//    public class ReceiptFormat : POSConnection
//    {
//        #region Constructors and Destructors

//        public ReceiptFormat()
//            : base(null, null)
//        {
//        }

//        public ReceiptFormat(MySqlConnection Connection, MySqlTransaction Transaction) 
//            : base(Connection, Transaction)
//        {

//        }

//        #endregion

//        #region Insert and Update

//        public void Update(ReceiptFormatDetails Details)
//        {
//            try 
//            {
//                string SQL=	"UPDATE tblReceiptFormat SET " +
//                                "ReportHeaderSpacer	=	@ReportHeaderSpacer," +
//                                "ReportHeader1		=	@ReportHeader1," +
//                                "ReportHeader2		=	@ReportHeader2," +
//                                "ReportHeader3		=	@ReportHeader3," +
//                                "ReportHeader4		=	@ReportHeader4," +
//                                "PageHeader1		=	@PageHeader1," +
//                                "PageHeader2		=	@PageHeader2," +
//                                "PageHeader3		=	@PageHeader3," +
//                                "PageFooter1		=	@PageFooter1," +
//                                "PageFooter2		=	@PageFooter2," +
//                                "PageFooter3		=	@PageFooter3," +
//                                "ReportFooter1		=	@ReportFooter1," +
//                                "ReportFooter2		=	@ReportFooter2," +
//                                "ReportFooter3		=	@ReportFooter3," +
//                                "ReportFooterSpacer	=	@ReportFooterSpacer;";
				  
				
	 			
//                MySqlCommand cmd = new MySqlCommand();
				
				
//                cmd.CommandType = System.Data.CommandType.Text;
//                cmd.CommandText = SQL;

//                MySqlParameter prmReportHeaderSpacer = new MySqlParameter("@ReportHeaderSpacer", MySqlDbType.Int32);
//                prmReportHeaderSpacer.Value = Details.ReportHeaderSpacer;
//                cmd.Parameters.Add(prmReportHeaderSpacer);

//                MySqlParameter prmReportHeader1 = new MySqlParameter("@ReportHeader1", MySqlDbType.String);	
//                prmReportHeader1.Value = Details.ReportHeader1;
//                cmd.Parameters.Add(prmReportHeader1);

//                MySqlParameter prmReportHeader2 = new MySqlParameter("@ReportHeader2", MySqlDbType.String);	
//                prmReportHeader2.Value = Details.ReportHeader2;
//                cmd.Parameters.Add(prmReportHeader2);

//                MySqlParameter prmReportHeader3 = new MySqlParameter("@ReportHeader3", MySqlDbType.String);	
//                prmReportHeader3.Value = Details.ReportHeader3;
//                cmd.Parameters.Add(prmReportHeader3);

//                MySqlParameter prmReportHeader4 = new MySqlParameter("@ReportHeader4", MySqlDbType.String);	
//                prmReportHeader4.Value = Details.ReportHeader4;
//                cmd.Parameters.Add(prmReportHeader4);

//                MySqlParameter prmPageHeader1 = new MySqlParameter("@PageHeader1", MySqlDbType.String);	
//                prmPageHeader1.Value = Details.PageHeader1;
//                cmd.Parameters.Add(prmPageHeader1);

//                MySqlParameter prmPageHeader2 = new MySqlParameter("@PageHeader2", MySqlDbType.String);	
//                prmPageHeader2.Value = Details.PageHeader2;
//                cmd.Parameters.Add(prmPageHeader2);

//                MySqlParameter prmPageHeader3 = new MySqlParameter("@PageHeader3", MySqlDbType.String);	
//                prmPageHeader3.Value = Details.PageHeader3;
//                cmd.Parameters.Add(prmPageHeader3);

//                MySqlParameter prmPageFooter1 = new MySqlParameter("@PageFooter1", MySqlDbType.String);	
//                prmPageFooter1.Value = Details.PageFooter1;
//                cmd.Parameters.Add(prmPageFooter1);

//                MySqlParameter prmPageFooter2 = new MySqlParameter("@PageFooter2", MySqlDbType.String);	
//                prmPageFooter2.Value = Details.PageFooter2;
//                cmd.Parameters.Add(prmPageFooter2);

//                MySqlParameter prmPageFooter3 = new MySqlParameter("@PageFooter3", MySqlDbType.String);	
//                prmPageFooter3.Value = Details.PageFooter3;
//                cmd.Parameters.Add(prmPageFooter3);

//                MySqlParameter prmReportFooter1 = new MySqlParameter("@ReportFooter1", MySqlDbType.String);	
//                prmReportFooter1.Value = Details.ReportFooter1;
//                cmd.Parameters.Add(prmReportFooter1);

//                MySqlParameter prmReportFooter2 = new MySqlParameter("@ReportFooter2", MySqlDbType.String);	
//                prmReportFooter2.Value = Details.ReportFooter2;
//                cmd.Parameters.Add(prmReportFooter2);

//                MySqlParameter prmReportFooter3 = new MySqlParameter("@ReportFooter3", MySqlDbType.String);	
//                prmReportFooter3.Value = Details.ReportFooter3;
//                cmd.Parameters.Add(prmReportFooter3);

//                MySqlParameter prmReportFooterSpacer = new MySqlParameter("@ReportFooterSpacer", MySqlDbType.Int32);	
//                prmReportFooterSpacer.Value = Details.ReportFooterSpacer;
//                cmd.Parameters.Add(prmReportFooterSpacer);

//                base.ExecuteNonQuery(cmd);
//            }

//            catch (Exception ex)
//            {
//                throw base.ThrowException(ex);
//            }	
//        }


//        #endregion

//        #region Details

//        public ReceiptFormatDetails Details()
//        {
//            try
//            {
//                string SQL=	"SELECT " +
//                                "ReportHeaderSpacer," +
//                                "ReportHeader1," +
//                                "ReportHeader2," +
//                                "ReportHeader3," +
//                                "ReportHeader4," +
//                                "PageHeader1," +
//                                "PageHeader2," +
//                                "PageHeader3," +
//                                "PageFooter1," +
//                                "PageFooter2," +
//                                "PageFooter3," +
//                                "ReportFooter1," +
//                                "ReportFooter2," +
//                                "ReportFooter3, " +
//                                "ReportFooterSpacer " +
//                            "FROM tblReceiptFormat;"; 
				  
//                MySqlCommand cmd = new MySqlCommand();
//                cmd.CommandType = System.Data.CommandType.Text;
//                cmd.CommandText = SQL;

//                ReceiptFormatDetails Details = new ReceiptFormatDetails();

//                MySqlDataReader myReader = base.ExecuteReader(cmd, System.Data.CommandBehavior.SingleResult);
//                while (myReader.Read()) 
//                {
//                    Details.ReportHeaderSpacer = myReader.GetInt32("ReportHeaderSpacer");
//                    Details.ReportHeader1 = myReader.GetString("ReportHeader1");
//                    Details.ReportHeader2 = myReader.GetString("ReportHeader2");
//                    Details.ReportHeader3 = myReader.GetString("ReportHeader3");
//                    Details.ReportHeader4 = myReader.GetString("ReportHeader4");
//                    Details.PageHeader1 = myReader.GetString("PageHeader1");
//                    Details.PageHeader2 = myReader.GetString("PageHeader2");
//                    Details.PageHeader3 = myReader.GetString("PageHeader3");
//                    Details.PageFooter1 = myReader.GetString("PageFooter1");
//                    Details.PageFooter2 = myReader.GetString("PageFooter2");
//                    Details.PageFooter3 = myReader.GetString("PageFooter3");
//                    Details.ReportFooter1 = myReader.GetString("ReportFooter1");
//                    Details.ReportFooter2 = myReader.GetString("ReportFooter2");
//                    Details.ReportFooter3 = myReader.GetString("ReportFooter3");
//                    Details.ReportFooterSpacer = myReader.GetInt32("ReportFooterSpacer");
//                }

//                myReader.Close();

//                return Details;
//            }

//            catch (Exception ex)
//            {
//                throw base.ThrowException(ex);
//            }	
//        }


//        #endregion

//        #region Streams

//        public MySqlDataReader List()
//        {
//            try
//            {
//                string SQL=	"SELECT " +
//                                "ReportHeaderSpacer," +
//                                "ReportHeader1," +
//                                "ReportHeader2," +
//                                "ReportHeader3," +
//                                "ReportHeader4," +
//                                "PageHeader1," +
//                                "PageHeader2," +
//                                "PageHeader3," +
//                                "PageFooter1," +
//                                "PageFooter2," +
//                                "PageFooter3," +
//                                "ReportFooter1," +
//                                "ReportFooter2," +
//                                "ReportFooter3, " +
//                                "ReportFooterSpacer " +
//                            "FROM tblReceiptFormat;"; 
				  
//                MySqlCommand cmd = new MySqlCommand();
//                cmd.CommandType = System.Data.CommandType.Text;
//                cmd.CommandText = SQL;

//                return base.ExecuteReader(cmd);
//            }

//            catch (Exception ex)
//            {
//                throw base.ThrowException(ex);
//            }	
//        }


//        #endregion
//    }
//}


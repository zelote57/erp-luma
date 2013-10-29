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
	public struct TerminalDetails
	{
        public int BranchID;
        public BranchDetails BranchDetails;
		public int TerminalID;
		public string TerminalNo;
		public string TerminalCode;
		public string TerminalName;
		public TerminalStatus Status;
		public DateTime DateCreated;
		public bool IsPrinterAutoCutter;
		public Int16 MaxReceiptWidth;
		public Int16 TransactionNoLength;
		public PrintingPreference AutoPrint;
		public bool IsVATInclusive;
		public string PrinterName;
		public string TurretName;
		public string CashDrawerName;
		public string MachineSerialNo;
		public string AccreditationNo;
		public bool ItemVoidConfirmation;
		public bool EnableEVAT;
		public string FORM_Behavior;
		public string MarqueeMessage;
		public decimal TrustFund;
		public decimal VAT;
		public decimal EVAT;
		public decimal LocalTax;
		public bool ShowItemMoreThanZeroQty;
        public bool ShowOnlyPackedTransactions;
        public bool ShowOneTerminalSuspendedTransactions;
        
        public TerminalReceiptType ReceiptType;
        public string SalesInvoicePrinterName;
        public bool CashCountBeforeReport;
        public bool PreviewTerminalReport;

        public bool IsPrinterDotMatrix;
        public bool IsChargeEditable;
        public bool IsDiscountEditable;
        public bool CheckCutOffTime;
        public string StartCutOffTime;
        public string EndCutOffTime;
        public bool WithRestaurantFeatures;
        public string SeniorCitizenDiscountCode;

        public bool IsTouchScreen;

        public bool WillContinueSelectionVariation;
        public bool WillContinueSelectionProduct;

        public decimal WSPriceMarkUp;
        public decimal RETPriceMarkUp;

        public bool WillPrintGrandTotal;
        public bool ReservedAndCommit;

        public string DBVersion;

        public bool ShowCustomerSelection;
        public bool AutoGenerateRewardCardNo;
        public RewardPointsDetails RewardPointsDetails;

        public string InHouseIndividualCreditPermitNo;
        public string InHouseGroupCreditPermitNo;

        public bool IsFineDIning;
        public ChargeTypeDetails PersonalChargeType;
        public ChargeTypeDetails GroupChargeType;

        // [03/18/2012] Include to know how the search will be done.
        public ProductSearchType ProductSearchType;

        // [04/29/2013] Include to know whether to print agreement or not
        public bool IncludeCreditChargeAgreement;

        // [06/30/2013] Include to know whether the terminal is parking or not
        public bool IsParkingTerminal;

        public bool WillPrintChargeSlip;

        // [04/29/2013] Include to know whether to print agreement or not
        public bool IncludeTermsAndConditions;
	}

    public struct RewardPointsDetails
    {
        public bool EnableRewardPoints;
        public bool RoundDownRewardPoints;
        public decimal RewardPointsMinimum;
        public decimal RewardPointsEvery;
        public decimal RewardPoints;
        public bool EnableRewardPointsAsPayment;
        public decimal RewardPointsMaxPercentageForPayment;
        public decimal RewardPointsPaymentValue;
        public decimal RewardPointsPaymentCashEquivalent;

        public string RewardsPermitNo;
    }

	[StrongNameIdentityPermissionAttribute(SecurityAction.LinkDemand,
		 PublicKey = "002400000480000094000000060200000024000" +
		 "052534131000400000100010053D785642F9F960B43157E0380" +
		 "F393BEE53E8DFAFBF441366C1B6F8B48D9DDF0D527B1F3B21EA" +
		 "E85D2FDB664CE81EB8A87DBE4C589D6F4202FE2B7D4B978BB69" +
		 "684874612CB9B8DB7A0339400A9C4E68277884B07817363D242" +
		 "E3696F9FACDBEA831810AE6DC9EDCA91A7B5DA12FE7BF65D113" +
		 "FF52834EAFB5A7A1FDFD5851A3")]
	public class Terminal : POSConnection
	{
		public const int DEFAULT_TERMINAL_NO_ID	= 1;

		#region Constructors & Destructors

        public Terminal()
            : base(null, null)
        {
        }

        public Terminal(MySqlConnection Connection, MySqlTransaction Transaction) 
            : base(Connection, Transaction)
		{

		}

		#endregion

		#region Insert & Update

		public Int16 Insert(TerminalDetails Details)
		{
			try
			{
				string SQL="INSERT INTO tblTerminal(" +
                                "BranchID, " +
                                "TerminalNo, " +
								"TerminalCode, " +
								"TerminalName, " +
								"Status, " +
								"DateCreated, " +
								"MachineSerialNo, " +
								"AccreditationNo " +
							")VALUES(" +
                                "@BranchID, " +
								"@TerminalNo, " +
								"@TerminalCode, " +
								"@TerminalName, " +
								"@Status, " +
								"NOW(), " +
								"@MachineSerialNo, " +
								"@AccreditationNo" +
							");";
				
				
	 			
				MySqlCommand cmd = new MySqlCommand();

				cmd.CommandType = System.Data.CommandType.Text;
				cmd.CommandText = SQL;

                MySqlParameter prmBranchID = new MySqlParameter("@BranchID",MySqlDbType.Int32);
                prmBranchID.Value = Details.BranchID;
                cmd.Parameters.Add(prmBranchID);

				MySqlParameter prmTerminalNo = new MySqlParameter("@TerminalNo",MySqlDbType.String);			
				prmTerminalNo.Value = Details.TerminalNo;
				cmd.Parameters.Add(prmTerminalNo);

				MySqlParameter prmTerminalCode = new MySqlParameter("@TerminalCode",MySqlDbType.String);			
				prmTerminalCode.Value = Details.TerminalCode;
				cmd.Parameters.Add(prmTerminalCode);

				MySqlParameter prmTerminalName = new MySqlParameter("@TerminalName",MySqlDbType.String);			
				prmTerminalName.Value = Details.TerminalName;
				cmd.Parameters.Add(prmTerminalName);
				
				MySqlParameter prmStatus = new MySqlParameter("@Status",MySqlDbType.Int16);
				prmStatus.Value = Details.Status.ToString("d");
				cmd.Parameters.Add(prmStatus);

				MySqlParameter prmDateCreated = new MySqlParameter("@DateCreated",MySqlDbType.DateTime);
				prmDateCreated.Value = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
				cmd.Parameters.Add(prmDateCreated);

				MySqlParameter prmMachineSerialNo = new MySqlParameter("@MachineSerialNo",MySqlDbType.String);			
				prmMachineSerialNo.Value = Details.MachineSerialNo;
				cmd.Parameters.Add(prmMachineSerialNo);

				MySqlParameter prmAccreditationNo = new MySqlParameter("@AccreditationNo",MySqlDbType.String);			
				prmAccreditationNo.Value = Details.AccreditationNo;
				cmd.Parameters.Add(prmAccreditationNo);

				base.ExecuteNonQuery(cmd);

				SQL = "SELECT LAST_INSERT_ID();";
				
				cmd.Parameters.Clear(); 
				cmd.CommandText = SQL;

                System.Data.DataTable dt = new System.Data.DataTable("LAST_INSERT_ID");
                base.MySqlDataAdapterFill(cmd, dt);
                

                Int16 iID = 0;
                foreach (System.Data.DataRow dr in dt.Rows)
                {
                    iID = Int16.Parse(dr[0].ToString());
                }

                TerminalReport clsTerminalReport = new TerminalReport(base.Connection, base.Transaction);
				clsTerminalReport.Insert(Details.BranchID, iID, CompanyDetails.TerminalNo);

				return iID;
			}

			catch (Exception ex)
			{
				throw base.ThrowException(ex);
			}	
		}

		public void Update(TerminalDetails Details)
		{
			try 
			{
                string SQL = "CALL procTerminalUpdate(@BranchID, @TerminalID, " +
                                                    "@IsPrinterAutoCutter, " +
                                                    "@AutoPrint, " +
                                                    "@IsVATInclusive, " +
                                                    "@PrinterName, " +
                                                    "@TurretName, " +
                                                    "@CashDrawerName, " +
                                                    "@ItemVoidConfirmation, " +
                                                    "@EnableEVAT, " +
                                                    "@MaxReceiptWidth, " +
                                                    "@FORM_Behavior, " +
                                                    "@MarqueeMessage, " +
                                                    "@MachineSerialNo, " +
                                                    "@AccreditationNo, " +
                                                    "@VAT, " +
                                                    "@EVAT, " +
                                                    "@LocalTax, " +
                                                    "@ShowItemMoreThanZeroQty, " +
                                                    "@ShowOnlyPackedTransactions, " +
                                                    "@ShowOneTerminalSuspendedTransactions, " +
                                                    "@ReceiptType, " +
                                                    "@SalesInvoicePrinterName, " +
                                                    "@CashCountBeforeReport, " +
                                                    "@PreviewTerminalReport, " +
                                                    "@IsPrinterDotMatrix, " +
                                                    "@IsChargeEditable, " +
                                                    "@IsDiscountEditable, " +
                                                    "@CheckCutOffTime, " +
                                                    "@StartCutOffTime, " +
                                                    "@EndCutOffTime, " +
                                                    "@WithRestaurantFeatures, " +
                                                    "@SeniorCitizenDiscountCode, " +
                                                    "@IsTouchScreen," +
                                                    "@WillContinueSelectionVariation," +
                                                    "@WillContinueSelectionProduct," +
                                                    "@WillPrintGrandTotal," +
                                                    "@ReservedAndCommit," +
                                                    "@ShowCustomerSelection);";
				  
				MySqlCommand cmd = new MySqlCommand();
				cmd.CommandType = System.Data.CommandType.Text;
				cmd.CommandText = SQL;

                cmd.Parameters.AddWithValue("@BranchID", Details.BranchID);
                cmd.Parameters.AddWithValue("@TerminalID", Details.TerminalID);
                cmd.Parameters.AddWithValue("@IsPrinterAutoCutter", Details.IsPrinterAutoCutter);
                cmd.Parameters.AddWithValue("@AutoPrint", Details.AutoPrint.ToString("d"));
                cmd.Parameters.AddWithValue("@IsVATInclusive", Details.IsVATInclusive);
                cmd.Parameters.AddWithValue("@PrinterName", Details.PrinterName);
                cmd.Parameters.AddWithValue("@TurretName", Details.TurretName);
                cmd.Parameters.AddWithValue("@CashDrawerName", Details.CashDrawerName);
                cmd.Parameters.AddWithValue("@MaxReceiptWidth", Details.MaxReceiptWidth);
                cmd.Parameters.AddWithValue("@ItemVoidConfirmation", Details.ItemVoidConfirmation);
				cmd.Parameters.AddWithValue("@EnableEVAT", Details.EnableEVAT);
                cmd.Parameters.AddWithValue("@FORM_Behavior", Details.FORM_Behavior);
                cmd.Parameters.AddWithValue("@MarqueeMessage", Details.MarqueeMessage);
                cmd.Parameters.AddWithValue("@MachineSerialNo", Details.MachineSerialNo);
                cmd.Parameters.AddWithValue("@AccreditationNo", Details.AccreditationNo);
                cmd.Parameters.AddWithValue("@VAT", Details.VAT);
                cmd.Parameters.AddWithValue("@EVAT", Details.EVAT);
                cmd.Parameters.AddWithValue("@LocalTax", Details.LocalTax);
                cmd.Parameters.AddWithValue("@ShowItemMoreThanZeroQty", Details.ShowItemMoreThanZeroQty);
                cmd.Parameters.AddWithValue("@ShowOnlyPackedTransactions", Details.ShowOnlyPackedTransactions);
                cmd.Parameters.AddWithValue("@ShowOneTerminalSuspendedTransactions", Details.ShowOneTerminalSuspendedTransactions);
                cmd.Parameters.AddWithValue("@ReceiptType", Details.ReceiptType.ToString("d"));
                cmd.Parameters.AddWithValue("@SalesInvoicePrinterName", Details.SalesInvoicePrinterName);
                cmd.Parameters.AddWithValue("@CashCountBeforeReport", Details.CashCountBeforeReport);
                cmd.Parameters.AddWithValue("@PreviewTerminalReport", Details.PreviewTerminalReport);
                cmd.Parameters.AddWithValue("@IsPrinterDotMatrix", Details.IsPrinterDotMatrix);
                cmd.Parameters.AddWithValue("@IsChargeEditable", Details.IsChargeEditable);
                cmd.Parameters.AddWithValue("@IsDiscountEditable", Details.IsDiscountEditable);
                cmd.Parameters.AddWithValue("@CheckCutOffTime", Details.CheckCutOffTime);
                cmd.Parameters.AddWithValue("@StartCutOffTime", Details.StartCutOffTime);
                cmd.Parameters.AddWithValue("@EndCutOffTime", Details.EndCutOffTime);
                cmd.Parameters.AddWithValue("@WithRestaurantFeatures", Details.WithRestaurantFeatures);
                cmd.Parameters.AddWithValue("@SeniorCitizenDiscountCode", Details.SeniorCitizenDiscountCode);
                cmd.Parameters.AddWithValue("@IsTouchScreen", Details.IsTouchScreen);
                cmd.Parameters.AddWithValue("@WillContinueSelectionVariation", Details.WillContinueSelectionVariation);
                cmd.Parameters.AddWithValue("@WillContinueSelectionProduct", Details.WillContinueSelectionProduct);
                cmd.Parameters.AddWithValue("@WillPrintGrandTotal", Details.WillPrintGrandTotal);
                cmd.Parameters.AddWithValue("@ReservedAndCommit", Details.ReservedAndCommit);
                cmd.Parameters.AddWithValue("@ShowCustomerSelection", Details.ShowCustomerSelection);

                base.ExecuteNonQuery(cmd);
			}

			catch (Exception ex)
			{
				throw base.ThrowException(ex);
			}	
		}

        public void UpdateRewardPointSystem(RewardPointsDetails Details)
        {
            try
            {
                string SQL = "CALL procTerminalUpdateRewardPointSystem(@EnableRewardPoints, " +
                                                    "@RoundDownRewardPoints, " +
                                                    "@RewardPointsMinimum," +
                                                    "@RewardPointsEvery," +
                                                    "@RewardPoints," +
                                                    "@EnableRewardPointsAsPayment," +
                                                    "@RewardPointsMaxPercentageForPayment," +
                                                    "@RewardPointsPaymentValue," +
                                                    "@RewardPointsPaymentCashEquivalent);";

                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandText = SQL;

                cmd.Parameters.AddWithValue("@EnableRewardPoints", Convert.ToInt16(Details.EnableRewardPoints));
                cmd.Parameters.AddWithValue("@RoundDownRewardPoints", Convert.ToInt16(Details.RoundDownRewardPoints));
                cmd.Parameters.AddWithValue("@RewardPointsMinimum", Details.RewardPointsMinimum);
                cmd.Parameters.AddWithValue("@RewardPointsEvery", Details.RewardPointsEvery);
                cmd.Parameters.AddWithValue("@RewardPoints", Details.RewardPoints);
                cmd.Parameters.AddWithValue("@EnableRewardPointsAsPayment", Convert.ToInt16(Details.EnableRewardPointsAsPayment));
                cmd.Parameters.AddWithValue("@RewardPointsMaxPercentageForPayment", Details.RewardPointsMaxPercentageForPayment);
                cmd.Parameters.AddWithValue("@RewardPointsPaymentValue", Details.RewardPointsPaymentValue);
                cmd.Parameters.AddWithValue("@RewardPointsPaymentCashEquivalent", Details.RewardPointsPaymentCashEquivalent);

                base.ExecuteNonQuery(cmd);
            }

            catch (Exception ex)
            {
                throw base.ThrowException(ex);
            }
        }

        public void UpdateIsCashCountInitialized(int BranchID, string TerminalNo, long CashierID, bool IsCashCountInitialized)
        {
            try
            {
                string SQL = "UPDATE tblCashierReport SET " +
                                "IsCashCountInitialized	= @IsCashCountInitialized " +
                            "WHERE BranchID = @BranchID AND TerminalNo			= @TerminalNo " +
                                "AND CashierID          = @CashierID;";

                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandText = SQL;

                MySqlParameter prmBranchID = new MySqlParameter("@BranchID",MySqlDbType.Int32);
                prmBranchID.Value = BranchID;
                cmd.Parameters.Add(prmBranchID);

                MySqlParameter prmTerminalNo = new MySqlParameter("@TerminalNo",MySqlDbType.String);
                prmTerminalNo.Value = TerminalNo;
                cmd.Parameters.Add(prmTerminalNo);

                MySqlParameter prmCashierID = new MySqlParameter("@CashierID",MySqlDbType.Int64);
                prmCashierID.Value = CashierID;
                cmd.Parameters.Add(prmCashierID);

                MySqlParameter prmIsCashCountInitialized = new MySqlParameter("@IsCashCountInitialized",MySqlDbType.Int16);
                prmIsCashCountInitialized.Value = Convert.ToInt16(IsCashCountInitialized);
                cmd.Parameters.Add(prmIsCashCountInitialized);

                base.ExecuteNonQuery(cmd);
            }

            catch (Exception ex)
            {
                throw base.ThrowException(ex);
            }
        }

        public void UpdateFEVersion(int BranchID, string TerminalNo, string FEVersion)
        {
            try
            {
                string SQL = "CALL procTerminalVersionUpdate(@BranchID, @TerminalNo, @Version);";

                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandText = SQL;

                cmd.Parameters.AddWithValue("@BranchID", BranchID);
                cmd.Parameters.AddWithValue("@TerminalNo", TerminalNo);
                cmd.Parameters.AddWithValue("@Version", FEVersion);

                base.ExecuteNonQuery(cmd);
            }

            catch (Exception ex)
            {
                throw base.ThrowException(ex);
            }
        }

        public void UpdateBEVersion(string BEVersion)
        {
            try
            {
                string SQL = "CALL procBEVersionUpdate(@Version);";

                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandText = SQL;
                cmd.Parameters.AddWithValue("@Version", BEVersion);

                base.ExecuteNonQuery(cmd);
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
				string SQL=	"DELETE FROM tblTerminal WHERE TerminalID IN (" + IDs + ");";
				  
				MySqlCommand cmd = new MySqlCommand();
				cmd.CommandType = System.Data.CommandType.Text;
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
			string SQL = "SELECT " +
                            "BranchID, " +
							"TerminalID, " +
							"TerminalNo, " +
							"TerminalCode, " +
							"TerminalName, " +
							"Status, " +
							"DateCreated, " +
							"IsPrinterAutoCutter, " +
							"MaxReceiptWidth, " +
							"TransactionNoLength, " +
							"AutoPrint, " +
							"IsVATInclusive, " +
							"PrinterName, " +
							"TurretName, " +
							"CashDrawerName, " +
							"MachineSerialNo, " +
							"AccreditationNo, " +
							"ItemVoidConfirmation, " +
							"EnableEVAT, " +
							"FORM_Behavior, " +
							"MarqueeMessage, " +
							"TrustFund, " +
							"VAT, " +
							"EVAT, " +
							"LocalTax, " +
							"ShowItemMoreThanZeroQty, " +
                            "ShowOnlyPackedTransactions, " +
                            "ShowOneTerminalSuspendedTransactions, " +
                            "TerminalReceiptType, " +
                            "SalesInvoicePrinterName, " +
                            "CashCountBeforeReport, " +
                            "PreviewTerminalReport, " +
                            "IsPrinterDotmatrix, " +
                            "IsChargeEditable, " +
                            "IsDiscountEditable, " +
                            "CheckCutOffTime, " +
                            "StartCutOffTime, " +
                            "EndCutOffTime, " +
                            "WithRestaurantFeatures, " +
                            "IsFineDIning, " +
                            "PersonalChargeTypeID, " +
                            "GroupChargeTypeID, " +
                            "SeniorCitizenDiscountCode, " +
                            "IsTouchScreen, " +
                            "WillContinueSelectionVariation, " +
                            "WillContinueSelectionProduct, " +
                            "WSPriceMarkUp, " +
                            "RETPriceMarkUp, " +
                            "WillPrintGrandTotal, " +
                            "ReservedAndCommit, " +
                            "ShowCustomerSelection, " +
                            "AutoGenerateRewardCardNo, " +
                            "EnableRewardPoints, " +
                            "RoundDownRewardPoints, " +
                            "RewardPointsMinimum, " +
                            "RewardPointsEvery, " +
                            "RewardPoints, " +
                            "EnableRewardPointsAsPayment, " +
                            "RewardPointsMaxPercentageForPayment, " +
                            "RewardPointsPaymentValue, " +
                            "RewardPointsPaymentCashEquivalent, " +
                            "RewardsPermitNo, " +
                            "InHouseIndividualCreditPermitNo, " +
                            "InHouseGroupCreditPermitNo, " +
                            "DBVersion, " +
                            "ProductSearchType, " +
                            "IncludeCreditChargeAgreement, " +
                            "IncludeTermsAndConditions, " +
                            "IsParkingTerminal, " +
                            "WillPrintChargeSlip " +
						"FROM tblTerminal ";

			return SQL;
		}

		#region Details

        public RewardPointsDetails RewardPointsDetails()
        {
            try
            {
                string SQL = "SELECT " +
                                "EnableRewardPoints, " +
                                "RoundDownRewardPoints, " +
                                "RewardPointsMinimum, " +
                                "RewardPointsEvery, " +
                                "RewardPoints, " +
                                "EnableRewardPointsAsPayment, " +
                                "RewardPointsMaxPercentageForPayment, " +
                                "RewardPointsPaymentValue, " +
                                "RewardPointsPaymentCashEquivalent, " +
                                "RewardsPermitNo " +
						    "FROM tblTerminal LIMIT 1 ";

                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandText = SQL;

                MySqlDataReader myReader = base.ExecuteReader(cmd, System.Data.CommandBehavior.SingleResult);

                RewardPointsDetails clsRewardPointsDetails = new RewardPointsDetails();
                while (myReader.Read())
                {
                    clsRewardPointsDetails.EnableRewardPoints = myReader.GetBoolean("EnableRewardPoints");
                    clsRewardPointsDetails.RoundDownRewardPoints = myReader.GetBoolean("RoundDownRewardPoints");
                    clsRewardPointsDetails.RewardPointsMinimum = myReader.GetDecimal("RewardPointsMinimum");
                    clsRewardPointsDetails.RewardPointsEvery = myReader.GetDecimal("RewardPointsEvery");
                    clsRewardPointsDetails.RewardPoints = myReader.GetDecimal("RewardPoints");
                    clsRewardPointsDetails.EnableRewardPointsAsPayment = myReader.GetBoolean("EnableRewardPointsAsPayment");
                    clsRewardPointsDetails.RewardPointsMaxPercentageForPayment = myReader.GetDecimal("RewardPointsMaxPercentageForPayment");
                    clsRewardPointsDetails.RewardPointsPaymentValue = myReader.GetDecimal("RewardPointsPaymentValue");
                    clsRewardPointsDetails.RewardPointsPaymentCashEquivalent = myReader.GetDecimal("RewardPointsPaymentCashEquivalent");
                    clsRewardPointsDetails.RewardsPermitNo = "" + myReader["RewardsPermitNo"].ToString();
                }
                myReader.Close();

                return clsRewardPointsDetails;
            }

            catch (Exception ex)
            {
                throw base.ThrowException(ex);
            }
        }

		public TerminalDetails Details(Int32 TerminalID)
		{
			try
			{
				string SQL=	SQLSelect() + "WHERE TerminalID = @TerminalID;";
				  
				MySqlCommand cmd = new MySqlCommand();
				cmd.CommandType = System.Data.CommandType.Text;
				cmd.CommandText = SQL;

				MySqlParameter prmTerminalID = new MySqlParameter("@TerminalID",MySqlDbType.Int16);
				prmTerminalID.Value = TerminalID;
				cmd.Parameters.Add(prmTerminalID);

                MySqlDataReader myReader = base.ExecuteReader(cmd, System.Data.CommandBehavior.SingleResult);

                TerminalDetails Details = SetDetails(myReader);
                myReader.Close();

				return Details;
			}

			catch (Exception ex)
			{
				throw base.ThrowException(ex);
			}	
		}

		public TerminalDetails Details(int BranchID, string TerminalNo)
		{
			try
			{
                string SQL = SQLSelect() + "WHERE BranchID = @BranchID AND TerminalNo = @TerminalNo;";
				  
				MySqlCommand cmd = new MySqlCommand();
				cmd.CommandType = System.Data.CommandType.Text;
				cmd.CommandText = SQL;

                MySqlParameter prmBranchID = new MySqlParameter("@BranchID",MySqlDbType.Int32);
                prmBranchID.Value = BranchID;
                cmd.Parameters.Add(prmBranchID);

				MySqlParameter prmTerminalNo = new MySqlParameter("@TerminalNo",MySqlDbType.String);
				prmTerminalNo.Value = TerminalNo;
				cmd.Parameters.Add(prmTerminalNo);

				MySqlDataReader myReader = base.ExecuteReader(cmd, System.Data.CommandBehavior.SingleResult);

                TerminalDetails Details = SetDetails(myReader);
                myReader.Close();

				return Details;
			}

			catch (Exception ex)
			{
				throw base.ThrowException(ex);
			}	
		}

        private TerminalDetails SetDetails(MySqlDataReader myReader)
        {
            int iPersonalChargeTypeID = 0;
            int iGroupChargeTypeID = 0;

            TerminalDetails Details = new TerminalDetails();

            while (myReader.Read())
            {
                Details.BranchID = myReader.GetInt32("BranchID");

                Details.TerminalID = myReader.GetInt32("TerminalID");
                Details.TerminalNo = "" + myReader["TerminalNo"].ToString();
                Details.TerminalCode = "" + myReader["TerminalCode"].ToString();
                Details.TerminalName = "" + myReader["TerminalName"].ToString();
                Details.Status = (TerminalStatus)Enum.Parse(typeof(TerminalStatus), myReader.GetString("Status"));
                Details.DateCreated = myReader.GetDateTime("DateCreated");
                Details.IsPrinterAutoCutter = myReader.GetBoolean("IsPrinterAutoCutter");
                Details.MaxReceiptWidth = myReader.GetInt16("MaxReceiptWidth");
                Details.TransactionNoLength = myReader.GetInt16("TransactionNoLength");
                Details.AutoPrint = (PrintingPreference)Enum.Parse(typeof(PrintingPreference), myReader.GetString("AutoPrint"));
                Details.IsVATInclusive = myReader.GetBoolean("IsVATInclusive");
                Details.PrinterName = "" + myReader["PrinterName"].ToString();
                Details.TurretName = "" + myReader["TurretName"].ToString();
                Details.CashDrawerName = "" + myReader["CashDrawerName"].ToString();
                Details.MachineSerialNo = "" + myReader["MachineSerialNo"].ToString();
                Details.AccreditationNo = "" + myReader["AccreditationNo"].ToString();
                Details.ItemVoidConfirmation = myReader.GetBoolean("ItemVoidConfirmation");
                Details.EnableEVAT = myReader.GetBoolean("EnableEVAT");
                Details.FORM_Behavior = "" + myReader["FORM_Behavior"].ToString();
                Details.MarqueeMessage = "" + myReader["MarqueeMessage"].ToString();
                Details.TrustFund = myReader.GetDecimal("TrustFund");
                Details.VAT = myReader.GetDecimal("VAT");
                Details.EVAT = myReader.GetDecimal("EVAT");
                Details.LocalTax = myReader.GetDecimal("LocalTax");
                Details.ShowItemMoreThanZeroQty = myReader.GetBoolean("ShowItemMoreThanZeroQty");
                Details.ShowOnlyPackedTransactions = myReader.GetBoolean("ShowOnlyPackedTransactions");
                Details.ShowOneTerminalSuspendedTransactions = myReader.GetBoolean("ShowOneTerminalSuspendedTransactions");
                Details.ReceiptType = (TerminalReceiptType)Enum.Parse(typeof(TerminalReceiptType), myReader.GetString("TerminalReceiptType"));
                Details.SalesInvoicePrinterName = "" + myReader["SalesInvoicePrinterName"].ToString();
                Details.CashCountBeforeReport = myReader.GetBoolean("CashCountBeforeReport");
                Details.PreviewTerminalReport = myReader.GetBoolean("PreviewTerminalReport");
                
                // Added May 5, 2009
                Details.IsPrinterDotMatrix = myReader.GetBoolean("IsPrinterDotMatrix");
                Details.IsChargeEditable = myReader.GetBoolean("IsChargeEditable");
                Details.IsDiscountEditable = myReader.GetBoolean("IsDiscountEditable");
                Details.CheckCutOffTime = myReader.GetBoolean("CheckCutOffTime");
                Details.StartCutOffTime = "" + myReader["StartCutOffTime"].ToString();
                Details.EndCutOffTime = "" + myReader["EndCutOffTime"].ToString();
                Details.WithRestaurantFeatures = myReader.GetBoolean("WithRestaurantFeatures");

                // Added Nov 8, 2011
                Details.IsFineDIning = myReader.GetBoolean("IsFineDIning");

                // -- end
                
                Details.SeniorCitizenDiscountCode = "" + myReader["SeniorCitizenDiscountCode"].ToString();

                // Added May 21, 2009
                Details.IsTouchScreen = myReader.GetBoolean("IsTouchScreen");

                // Added June 1, 2010
                Details.WillContinueSelectionVariation = myReader.GetBoolean("WillContinueSelectionVariation");
                Details.WillContinueSelectionProduct = myReader.GetBoolean("WillContinueSelectionProduct");

                // Added July 9, 2010
                Details.WSPriceMarkUp = myReader.GetDecimal("WSPriceMarkUp");
                Details.RETPriceMarkUp = myReader.GetDecimal("RETPriceMarkUp");

                // Added Sep 21, 2010
                Details.WillPrintGrandTotal = myReader.GetBoolean("WillPrintGrandTotal");

                // Added Apr 12, 2011
                Details.ReservedAndCommit = myReader.GetBoolean("ReservedAndCommit");       
         
                // Added Sep 10, 2011
                Details.DBVersion = "" + myReader["DBVersion"].ToString();

                // Added Oct 17, 2011
                Details.ShowCustomerSelection = myReader.GetBoolean("ShowCustomerSelection");
                Details.AutoGenerateRewardCardNo = myReader.GetBoolean("AutoGenerateRewardCardNo");

                // Added Oct 17, 2011 
                RewardPointsDetails clsRewardPointsDetails = new RewardPointsDetails();
                clsRewardPointsDetails.EnableRewardPoints = myReader.GetBoolean("EnableRewardPoints");
                clsRewardPointsDetails.RoundDownRewardPoints = myReader.GetBoolean("RoundDownRewardPoints");
                clsRewardPointsDetails.RewardPointsMinimum = myReader.GetDecimal("RewardPointsMinimum");
                clsRewardPointsDetails.RewardPointsEvery = myReader.GetDecimal("RewardPointsEvery");
                clsRewardPointsDetails.RewardPoints = myReader.GetDecimal("RewardPoints");
                // Added Nov 4, 2011 :  For use as payment
                clsRewardPointsDetails.EnableRewardPointsAsPayment = myReader.GetBoolean("EnableRewardPointsAsPayment");
                clsRewardPointsDetails.RewardPointsMaxPercentageForPayment = myReader.GetDecimal("RewardPointsMaxPercentageForPayment");
                clsRewardPointsDetails.RewardPointsPaymentValue = myReader.GetDecimal("RewardPointsPaymentValue");
                clsRewardPointsDetails.RewardPointsPaymentCashEquivalent = myReader.GetDecimal("RewardPointsPaymentCashEquivalent");
                clsRewardPointsDetails.RewardsPermitNo = "" + myReader["RewardsPermitNo"].ToString();

                Details.RewardPointsDetails = clsRewardPointsDetails;

                Details.InHouseIndividualCreditPermitNo = "" + myReader["InHouseIndividualCreditPermitNo"].ToString();
                Details.InHouseGroupCreditPermitNo = "" + myReader["InHouseGroupCreditPermitNo"].ToString();

                iPersonalChargeTypeID = myReader.GetInt32("PersonalChargeTypeID");
                iGroupChargeTypeID = myReader.GetInt32("GroupChargeTypeID");

                // Added Mar 18, 2012
                Details.ProductSearchType = (ProductSearchType)Enum.Parse(typeof(ProductSearchType), myReader.GetString("ProductSearchType"));
                
                // Added Apr 29, 2013
                Details.IncludeCreditChargeAgreement = myReader.GetBoolean("IncludeCreditChargeAgreement");

                // Added Jun 30, 2013
                Details.IsParkingTerminal = myReader.GetBoolean("IsParkingTerminal");

                Details.WillPrintChargeSlip = myReader.GetBoolean("WillPrintChargeSlip");

                // Added Oct 20, 2013
                Details.IncludeTermsAndConditions = myReader.GetBoolean("IncludeTermsAndConditions");

            }
            myReader.Close();

            Branch clsBranch = new Branch(base.Connection, base.Transaction);
            Details.BranchDetails = clsBranch.Details(Convert.ToInt16(Details.BranchID));

            ChargeType clsChargeType = new ChargeType(base.Connection, base.Transaction);
            if (iPersonalChargeTypeID != 0)
            { Details.PersonalChargeType = clsChargeType.Details(iPersonalChargeTypeID); }

            if (iGroupChargeTypeID != 0)
            { Details.GroupChargeType = clsChargeType.Details(iGroupChargeTypeID); }

            return Details;
        }

        public string getTerminalKey(string HDSerialNo)
        {
            try
            {
                string SQL = "SELECT RegistrationKey FROM sysTerminalkey WHERE HDSerialNo = @HDSerialNo;";

                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandText = SQL;

                MySqlParameter prmHDSerialNo = new MySqlParameter("@HDSerialNo",MySqlDbType.String);
                prmHDSerialNo.Value = HDSerialNo;
                cmd.Parameters.Add(prmHDSerialNo);

                MySqlDataReader myReader = base.ExecuteReader(cmd, System.Data.CommandBehavior.SingleResult);

                string strRetvalue = string.Empty;

                while (myReader.Read())
                {
                    strRetvalue = "" + myReader["RegistrationKey"].ToString();
                }
                myReader.Close();

                return strRetvalue;
            }

            catch (Exception ex)
            {
                throw base.ThrowException(ex);
            }
        }

		#endregion
	
		#region Streams

        public MySqlDataReader List(string SearchKey = "", string SortField = "", SortOption SortOrder = SortOption.Ascending)
		{
			try
			{
                MySqlCommand cmd = new MySqlCommand();

                string SQL = SQLSelect();

                if (SearchKey != string.Empty && SearchKey != "")
                {
                    SQL += "WHERE (TerminalNo LIKE @SearchKey " +
                            "OR TerminalCode LIKE @SearchKey " +
                            "OR TerminalName LIKE @SearchKey) ";

                    cmd.Parameters.AddWithValue("@SearchKey", "%" + SearchKey + "%");
                }
                if (SortField != string.Empty && SortField != "")
                {
                    SQL += "ORDER BY " + SortField;

                    if (SortOrder == SortOption.Ascending)
                        SQL += " ASC";
                    else
                        SQL += " DESC";
                }

                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandText = SQL;
				
				MySqlDataReader myReader = base.ExecuteReader(cmd);

                return base.ExecuteReader(cmd);
			}
			catch (Exception ex)
			{
				throw base.ThrowException(ex);
			}	
		}
        public System.Data.DataTable ListAsDataTable(string SearchKey = "", string SortField = "", SortOption SortOrder = SortOption.Ascending)
		{
			try
			{
                MySqlCommand cmd = new MySqlCommand();

                string SQL = SQLSelect();

                if (SearchKey != string.Empty && SearchKey != "")
                {
                    SQL += "WHERE (TerminalNo LIKE @SearchKey " +
                            "OR TerminalCode LIKE @SearchKey " +
                            "OR TerminalName LIKE @SearchKey) ";

                    cmd.Parameters.AddWithValue("@SearchKey", "%" + SearchKey + "%");
                }
                if (SortField != string.Empty && SortField != "")
                {
                    SQL += "ORDER BY " + SortField;

                    if (SortOrder == SortOption.Ascending)
                        SQL += " ASC";
                    else
                        SQL += " DESC";
                }
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandText = SQL;

                System.Data.DataTable dt = new System.Data.DataTable("tblTerminal");
                base.MySqlDataAdapterFill(cmd, dt);

                return dt;	
			}
			catch (Exception ex)
			{
				throw base.ThrowException(ex);
			}	
		}

		#endregion

        #region Validate: IsExist & IsZReadInitialized, IsCashCountInitialized

        public bool IsExist(int BranchID, string TerminalNo, out TerminalDetails OutDetails)
		{
			try
			{
				bool boRetValue = false;

				string SQL=	SQLSelect() + "WHERE BranchID = @BranchID AND TerminalNo = @TerminalNo;";
	 			
				MySqlCommand cmd = new MySqlCommand();
				cmd.CommandType = System.Data.CommandType.Text;
				cmd.CommandText = SQL;

                MySqlParameter prmBranchID = new MySqlParameter("@BranchID",MySqlDbType.Int32);
                prmBranchID.Value = BranchID;
                cmd.Parameters.Add(prmBranchID);

				MySqlParameter prmTerminalNo = new MySqlParameter("@TerminalNo",MySqlDbType.String);
				prmTerminalNo.Value = TerminalNo;
				cmd.Parameters.Add(prmTerminalNo);

				MySqlDataReader myReader = base.ExecuteReader(cmd, System.Data.CommandBehavior.SingleResult);
				
				OutDetails = SetDetails(myReader);
                myReader.Close();

                if (OutDetails.TerminalID != 0) boRetValue = true;

				return boRetValue;
			}

			catch (Exception ex)
			{
				throw base.ThrowException(ex);
			}	
		}

        public bool IsCashCountInitialized(int BranchID, string TerminalNo, long CashierID)
        {
            try
            {
                bool boRetValue = false;

                string SQL = "SELECT IsCashCountInitialized FROM tblCashierReport " +
                               "WHERE BranchID = @BranchID AND TerminalNo = @TerminalNo AND CashierID = @CashierID;";

                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandText = SQL;

                MySqlParameter prmBranchID = new MySqlParameter("@BranchID",MySqlDbType.Int32);
                prmBranchID.Value = BranchID;
                cmd.Parameters.Add(prmBranchID);

                MySqlParameter prmTerminalNo = new MySqlParameter("@TerminalNo",MySqlDbType.String);
                prmTerminalNo.Value = TerminalNo;
                cmd.Parameters.Add(prmTerminalNo);

                MySqlParameter prmCashierID = new MySqlParameter("@CashierID",MySqlDbType.Int64);
                prmCashierID.Value = CashierID;
                cmd.Parameters.Add(prmCashierID);

                MySqlDataReader myReader = base.ExecuteReader(cmd, System.Data.CommandBehavior.SingleResult);

                while (myReader.Read())
                {
                    boRetValue = myReader.GetBoolean("IsCashCountInitialized");
                }

                myReader.Close();

                return boRetValue;
            }

            catch (Exception ex)
            {
                throw base.ThrowException(ex);
            }
        }

		#endregion
	}
}


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
				
				MySqlDataReader myReader = base.ExecuteReader(cmd, System.Data.CommandBehavior.SingleResult);
				
				Int16 iID = 0;

				while (myReader.Read()) 
				{
					iID = myReader.GetInt16("TerminalID");
				}

				myReader.Close();

                TerminalReport clsTerminalReport = new TerminalReport(base.Connection, base.Transaction);
				clsTerminalReport.Insert(Details.BranchID, iID, CompanyDetails.TerminalNo);

				return iID;
			}

			catch (Exception ex)
			{
				throw ex;
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

                MySqlParameter prmBranchID = new MySqlParameter("@BranchID",MySqlDbType.Int32);
                prmBranchID.Value = Details.BranchID;
                cmd.Parameters.Add(prmBranchID);

                MySqlParameter prmTerminalID = new MySqlParameter("@TerminalID",MySqlDbType.Int32);
                prmTerminalID.Value = Details.TerminalID;
                cmd.Parameters.Add(prmTerminalID);

                MySqlParameter prmIsPrinterAutoCutter = new MySqlParameter("@IsPrinterAutoCutter",MySqlDbType.Int16);			
                prmIsPrinterAutoCutter.Value = Convert.ToInt16(Details.IsPrinterAutoCutter);
				cmd.Parameters.Add(prmIsPrinterAutoCutter);

				MySqlParameter prmAutoPrint = new MySqlParameter("@AutoPrint",MySqlDbType.Int16);	
				prmAutoPrint.Value = Details.AutoPrint.ToString("d");
				cmd.Parameters.Add(prmAutoPrint);

                MySqlParameter prmIsVATInclusive = new MySqlParameter("@IsVATInclusive",MySqlDbType.Int16);	
				prmIsVATInclusive.Value = Convert.ToInt16(Details.IsVATInclusive);
				cmd.Parameters.Add(prmIsVATInclusive);

				MySqlParameter prmPrinterName = new MySqlParameter("@PrinterName",MySqlDbType.String);			
				prmPrinterName.Value = Details.PrinterName;
				cmd.Parameters.Add(prmPrinterName);

				MySqlParameter prmTurretName = new MySqlParameter("@TurretName",MySqlDbType.String);			
				prmTurretName.Value = Details.TurretName;
				cmd.Parameters.Add(prmTurretName);

				MySqlParameter prmCashDrawerName = new MySqlParameter("@CashDrawerName",MySqlDbType.String);			
				prmCashDrawerName.Value = Details.CashDrawerName;
				cmd.Parameters.Add(prmCashDrawerName);

				MySqlParameter prmMaxReceiptWidth = new MySqlParameter("@MaxReceiptWidth",MySqlDbType.Int32);			
				prmMaxReceiptWidth.Value = Details.MaxReceiptWidth;
				cmd.Parameters.Add(prmMaxReceiptWidth);
				
				MySqlParameter prmItemVoidConfirmation= new MySqlParameter("@ItemVoidConfirmation",MySqlDbType.Int16);
                prmItemVoidConfirmation.Value = Convert.ToInt16(Details.ItemVoidConfirmation);
				cmd.Parameters.Add(prmItemVoidConfirmation);

				MySqlParameter prmEnableEVAT = new MySqlParameter("@EnableEVAT",MySqlDbType.String);			
                prmEnableEVAT.Value = Convert.ToInt16(Details.EnableEVAT);
				cmd.Parameters.Add(prmEnableEVAT);

				MySqlParameter prmFORM_Behavior = new MySqlParameter("@FORM_Behavior",MySqlDbType.Int16);
				prmFORM_Behavior.Value = Details.FORM_Behavior;
				cmd.Parameters.Add(prmFORM_Behavior);

				MySqlParameter prmMarqueeMessage = new MySqlParameter("@MarqueeMessage",MySqlDbType.String);
				prmMarqueeMessage.Value = Details.MarqueeMessage;
				cmd.Parameters.Add(prmMarqueeMessage);

				MySqlParameter prmMachineSerialNo = new MySqlParameter("@MachineSerialNo",MySqlDbType.String);
				prmMachineSerialNo.Value = Details.MachineSerialNo;
				cmd.Parameters.Add(prmMachineSerialNo);

				MySqlParameter prmAccreditationNo = new MySqlParameter("@AccreditationNo",MySqlDbType.String);
				prmAccreditationNo.Value = Details.AccreditationNo;
				cmd.Parameters.Add(prmAccreditationNo);

                MySqlParameter prmVAT = new MySqlParameter("@VAT", System.Data.DbType.Decimal);
                prmVAT.Value = Details.VAT;
                cmd.Parameters.Add(prmVAT);

                MySqlParameter prmEVAT = new MySqlParameter("@EVAT", System.Data.DbType.Decimal);
                prmEVAT.Value = Details.EVAT;
                cmd.Parameters.Add(prmEVAT);

                MySqlParameter prmLocalTax = new MySqlParameter("@LocalTax", System.Data.DbType.Decimal);
                prmLocalTax.Value = Details.LocalTax;
                cmd.Parameters.Add(prmLocalTax);

                MySqlParameter prmShowItemMoreThanZeroQty = new MySqlParameter("@ShowItemMoreThanZeroQty",MySqlDbType.Int16);
                prmShowItemMoreThanZeroQty.Value = Convert.ToInt16(Details.ShowItemMoreThanZeroQty);
                cmd.Parameters.Add(prmShowItemMoreThanZeroQty);

                MySqlParameter prmShowOnlyPackedTransactions = new MySqlParameter("@ShowOnlyPackedTransactions",MySqlDbType.Int16);
                prmShowOnlyPackedTransactions.Value = Convert.ToInt16(Details.ShowOnlyPackedTransactions);
                cmd.Parameters.Add(prmShowOnlyPackedTransactions);

                MySqlParameter prmShowOneTerminalSuspendedTransactions = new MySqlParameter("@ShowOneTerminalSuspendedTransactions",MySqlDbType.Int16);
                prmShowOneTerminalSuspendedTransactions.Value = Convert.ToInt16(Details.ShowOneTerminalSuspendedTransactions);
                cmd.Parameters.Add(prmShowOneTerminalSuspendedTransactions);

                MySqlParameter prmReceiptType = new MySqlParameter("@ReceiptType",MySqlDbType.Int16);
                prmReceiptType.Value = Convert.ToInt16(Details.ReceiptType.ToString("d"));
                cmd.Parameters.Add(prmReceiptType);

                MySqlParameter prmSalesInvoicePrinterName = new MySqlParameter("@SalesInvoicePrinterName",MySqlDbType.String);
                prmSalesInvoicePrinterName.Value = Details.SalesInvoicePrinterName;
                cmd.Parameters.Add(prmSalesInvoicePrinterName);

                MySqlParameter prmCashCountBeforeReport = new MySqlParameter("@CashCountBeforeReport",MySqlDbType.Int16);
                prmCashCountBeforeReport.Value = Convert.ToInt16(Details.CashCountBeforeReport);
                cmd.Parameters.Add(prmCashCountBeforeReport);

                MySqlParameter prmPreviewTerminalReport = new MySqlParameter("@PreviewTerminalReport",MySqlDbType.Int16);
                prmPreviewTerminalReport.Value = Convert.ToInt16(Details.PreviewTerminalReport);
                cmd.Parameters.Add(prmPreviewTerminalReport);

                MySqlParameter prmIsPrinterDotMatrix = new MySqlParameter("@IsPrinterDotMatrix",MySqlDbType.Int16);
                prmIsPrinterDotMatrix.Value = Convert.ToInt16(Details.IsPrinterDotMatrix);
                cmd.Parameters.Add(prmIsPrinterDotMatrix);

                MySqlParameter prmIsChargeEditable = new MySqlParameter("@IsChargeEditable",MySqlDbType.Int16);
                prmIsChargeEditable.Value = Convert.ToInt16(Details.IsChargeEditable);
                cmd.Parameters.Add(prmIsChargeEditable);

                MySqlParameter prmIsDiscountEditable = new MySqlParameter("@IsDiscountEditable",MySqlDbType.Int16);
                prmIsDiscountEditable.Value = Convert.ToInt16(Details.IsDiscountEditable);
                cmd.Parameters.Add(prmIsDiscountEditable);

                MySqlParameter prmCheckCutOffTime = new MySqlParameter("@CheckCutOffTime",MySqlDbType.Int16);
                prmCheckCutOffTime.Value = Convert.ToInt16(Details.CheckCutOffTime);
                cmd.Parameters.Add(prmCheckCutOffTime);

                MySqlParameter prmStartCutOffTime = new MySqlParameter("@StartCutOffTime",MySqlDbType.String);
                prmStartCutOffTime.Value = Details.StartCutOffTime;
                cmd.Parameters.Add(prmStartCutOffTime);

                MySqlParameter prmEndCutOffTime = new MySqlParameter("@EndCutOffTime",MySqlDbType.String);
                prmEndCutOffTime.Value = Details.EndCutOffTime;
                cmd.Parameters.Add(prmEndCutOffTime);

                MySqlParameter prmWithRestaurantFeatures = new MySqlParameter("@WithRestaurantFeatures",MySqlDbType.Int16);
                prmWithRestaurantFeatures.Value = Convert.ToInt16(Details.WithRestaurantFeatures);
                cmd.Parameters.Add(prmWithRestaurantFeatures);

                cmd.Parameters.AddWithValue("@SeniorCitizenDiscountCode", Details.SeniorCitizenDiscountCode);

                cmd.Parameters.AddWithValue("@IsTouchScreen", Convert.ToInt16(Details.IsTouchScreen));
                cmd.Parameters.AddWithValue("@WillContinueSelectionVariation", Convert.ToInt16(Details.WillContinueSelectionVariation));
                cmd.Parameters.AddWithValue("@WillContinueSelectionProduct", Convert.ToInt16(Details.WillContinueSelectionProduct));
                cmd.Parameters.AddWithValue("@WillPrintGrandTotal", Convert.ToInt16(Details.WillPrintGrandTotal));
                cmd.Parameters.AddWithValue("@ReservedAndCommit", Convert.ToInt16(Details.ReservedAndCommit));
                cmd.Parameters.AddWithValue("@ShowCustomerSelection", Convert.ToInt16(Details.ShowCustomerSelection));

                base.ExecuteNonQuery(cmd);
			}

			catch (Exception ex)
			{
				throw ex;
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
                throw ex;
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
                throw ex;
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
                throw ex;
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
                throw ex;
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
				throw ex;
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
                            "ProductSearchType " +
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
                throw ex;
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
				throw ex;
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
				throw ex;
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
                throw ex;
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

                return myReader;
			}
			catch (Exception ex)
			{
				throw ex;
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
				throw ex;
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
				throw ex;
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
                throw ex;
            }
        }

		#endregion
	}
}


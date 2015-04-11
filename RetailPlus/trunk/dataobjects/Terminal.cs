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
        public OrderSlipPrinter OrderSlipPrinter;

        public bool IsPrinterDotMatrix;
        public bool IsChargeEditable;
        public bool IsDiscountEditable;
        public bool CheckCutOffTime;
        public string StartCutOffTime;
        public string EndCutOffTime;
        public bool WithRestaurantFeatures;
        public string SeniorCitizenDiscountCode;
        public string PWDDiscountCode;

        public bool IsTouchScreen;

        public bool WillContinueSelectionVariation;
        public bool WillContinueSelectionProduct;

        public decimal WSPriceMarkUp;
        public decimal RETPriceMarkUp;

        public bool WillPrintGrandTotal;
        public bool ReservedAndCommit;

        public string DBVersion;
        public string FEVersion;
        public string BEVersion;

        public bool ShowCustomerSelection;
        public bool AutoGenerateRewardCardNo;
        public RewardPointsDetails RewardPointsDetails;

        //public string InHouseIndividualCreditPermitNo;
        //public string InHouseGroupCreditPermitNo;

        public bool IsFineDining;
        public ChargeTypeDetails PersonalChargeType;
        public ChargeTypeDetails GroupChargeType;

        // [03/18/2012] Include to know how the search will be done.
        public ProductSearchType ProductSearchType;

        // [04/29/2013] Include to know whether to print agreement or not
        public bool IncludeCreditChargeAgreement;

        // [06/30/2013] Include to know whether the terminal is parking or not
        public bool IsParkingTerminal;

        public bool WillPrintChargeSlip;
        public bool WillPrintVoidItem;

        // [04/29/2013] Include to know whether to print agreement or not
        public bool IncludeTermsAndConditions;

        // [09/24/2014] Default Transaction Charge Code when a transaction is created.
        // primarily use for FineDining / Restaurant
        public string DefaultTransactionChargeCode;
        public string DineInChargeCode;
        public string TakeOutChargeCode;
        public string DeliveryChargeCode;

        // 23Mar2015 : This is used to determine which BranchID & TerminalNo to use when creating an ORNo
        public Int32 ORSeriesBranchID;
        public string ORSeriesTerminalNo;

        public DateTime CreatedOn;
        public DateTime LastModified;
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

        /// <summary>
        /// Sep 4, 2014 Remove coz this is redundant
        /// </summary>
        /// <param name="Details"></param>
        /// <returns></returns>
        //public Int16 Insert(TerminalDetails Details)
        //{
        //    try
        //    {
        //        string SQL="INSERT INTO tblTerminal(" +
        //                        "BranchID, " +
        //                        "TerminalNo, " +
        //                        "TerminalCode, " +
        //                        "TerminalName, " +
        //                        "Status, " +
        //                        "DateCreated, " +
        //                        "MachineSerialNo, " +
        //                        "AccreditationNo " +
        //                    ")VALUES(" +
        //                        "@BranchID, " +
        //                        "@TerminalNo, " +
        //                        "@TerminalCode, " +
        //                        "@TerminalName, " +
        //                        "@Status, " +
        //                        "NOW(), " +
        //                        "@MachineSerialNo, " +
        //                        "@AccreditationNo" +
        //                    ");";
				
	 			
        //        MySqlCommand cmd = new MySqlCommand();
        //        cmd.CommandType = System.Data.CommandType.Text;
        //        cmd.CommandText = SQL;

        //        MySqlParameter prmBranchID = new MySqlParameter("@BranchID",MySqlDbType.Int32);
        //        prmBranchID.Value = Details.BranchID;
        //        cmd.Parameters.Add(prmBranchID);

        //        MySqlParameter prmTerminalNo = new MySqlParameter("@TerminalNo",MySqlDbType.String);			
        //        prmTerminalNo.Value = Details.TerminalNo;
        //        cmd.Parameters.Add(prmTerminalNo);

        //        MySqlParameter prmTerminalCode = new MySqlParameter("@TerminalCode",MySqlDbType.String);			
        //        prmTerminalCode.Value = Details.TerminalCode;
        //        cmd.Parameters.Add(prmTerminalCode);

        //        MySqlParameter prmTerminalName = new MySqlParameter("@TerminalName",MySqlDbType.String);			
        //        prmTerminalName.Value = Details.TerminalName;
        //        cmd.Parameters.Add(prmTerminalName);
				
        //        MySqlParameter prmStatus = new MySqlParameter("@Status",MySqlDbType.Int16);
        //        prmStatus.Value = Details.Status.ToString("d");
        //        cmd.Parameters.Add(prmStatus);

        //        MySqlParameter prmDateCreated = new MySqlParameter("@DateCreated",MySqlDbType.DateTime);
        //        prmDateCreated.Value = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
        //        cmd.Parameters.Add(prmDateCreated);

        //        MySqlParameter prmMachineSerialNo = new MySqlParameter("@MachineSerialNo",MySqlDbType.String);			
        //        prmMachineSerialNo.Value = Details.MachineSerialNo;
        //        cmd.Parameters.Add(prmMachineSerialNo);

        //        MySqlParameter prmAccreditationNo = new MySqlParameter("@AccreditationNo",MySqlDbType.String);			
        //        prmAccreditationNo.Value = Details.AccreditationNo;
        //        cmd.Parameters.Add(prmAccreditationNo);

        //        base.ExecuteNonQuery(cmd);

        //        SQL = "SELECT LAST_INSERT_ID();";
				
        //        cmd.Parameters.Clear(); 
        //        cmd.CommandText = SQL;

        //        System.Data.DataTable dt = new System.Data.DataTable("LAST_INSERT_ID");
        //        base.MySqlDataAdapterFill(cmd, dt);
                

        //        Int16 iID = 0;
        //        foreach (System.Data.DataRow dr in dt.Rows)
        //        {
        //            iID = Int16.Parse(dr[0].ToString());
        //        }

        //        TerminalReport clsTerminalReport = new TerminalReport(base.Connection, base.Transaction);
        //        clsTerminalReport.Insert(Details.BranchID, iID, CompanyDetails.TerminalNo);

        //        return iID;
        //    }

        //    catch (Exception ex)
        //    {
        //        throw base.ThrowException(ex);
        //    }	
        //}

		public void Update(TerminalDetails Details)
		{
			try 
			{
                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandType = System.Data.CommandType.Text;

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
                                                    "@PWDDiscountCode, " +
                                                    "@GroupChargeType, " +
                                                    "@PersonalChargeType, " +
                                                    "@DefaultTransactionChargeCode, " +
                                                    "@DineInChargeCode, " +
                                                    "@TakeOutChargeCode, " +
                                                    "@DeliveryChargeCode, " +
                                                    "@IsTouchScreen," +
                                                    "@WillContinueSelectionVariation," +
                                                    "@WillContinueSelectionProduct," +
                                                    "@WillPrintGrandTotal," +
                                                    "@ReservedAndCommit," +
                                                    "@ShowCustomerSelection);";
				  
				cmd.Parameters.AddWithValue("BranchID", Details.BranchID);
                cmd.Parameters.AddWithValue("TerminalID", Details.TerminalID);
                cmd.Parameters.AddWithValue("IsPrinterAutoCutter", Details.IsPrinterAutoCutter);
                cmd.Parameters.AddWithValue("AutoPrint", Details.AutoPrint.ToString("d"));
                cmd.Parameters.AddWithValue("IsVATInclusive", Details.IsVATInclusive);
                cmd.Parameters.AddWithValue("PrinterName", Details.PrinterName);
                cmd.Parameters.AddWithValue("TurretName", Details.TurretName);
                cmd.Parameters.AddWithValue("CashDrawerName", Details.CashDrawerName);
                cmd.Parameters.AddWithValue("MaxReceiptWidth", Details.MaxReceiptWidth);
                cmd.Parameters.AddWithValue("ItemVoidConfirmation", Details.ItemVoidConfirmation);
				cmd.Parameters.AddWithValue("EnableEVAT", Details.EnableEVAT);
                cmd.Parameters.AddWithValue("FORM_Behavior", Details.FORM_Behavior);
                cmd.Parameters.AddWithValue("MarqueeMessage", Details.MarqueeMessage);
                cmd.Parameters.AddWithValue("MachineSerialNo", Details.MachineSerialNo);
                cmd.Parameters.AddWithValue("AccreditationNo", Details.AccreditationNo);
                cmd.Parameters.AddWithValue("VAT", Details.VAT);
                cmd.Parameters.AddWithValue("EVAT", Details.EVAT);
                cmd.Parameters.AddWithValue("LocalTax", Details.LocalTax);
                cmd.Parameters.AddWithValue("ShowItemMoreThanZeroQty", Details.ShowItemMoreThanZeroQty);
                cmd.Parameters.AddWithValue("ShowOnlyPackedTransactions", Details.ShowOnlyPackedTransactions);
                cmd.Parameters.AddWithValue("ShowOneTerminalSuspendedTransactions", Details.ShowOneTerminalSuspendedTransactions);
                cmd.Parameters.AddWithValue("ReceiptType", Details.ReceiptType.ToString("d"));
                cmd.Parameters.AddWithValue("SalesInvoicePrinterName", Details.SalesInvoicePrinterName);
                cmd.Parameters.AddWithValue("CashCountBeforeReport", Details.CashCountBeforeReport);
                cmd.Parameters.AddWithValue("PreviewTerminalReport", Details.PreviewTerminalReport);
                cmd.Parameters.AddWithValue("IsPrinterDotMatrix", Details.IsPrinterDotMatrix);
                cmd.Parameters.AddWithValue("IsChargeEditable", Details.IsChargeEditable);
                cmd.Parameters.AddWithValue("IsDiscountEditable", Details.IsDiscountEditable);
                cmd.Parameters.AddWithValue("CheckCutOffTime", Details.CheckCutOffTime);
                cmd.Parameters.AddWithValue("StartCutOffTime", Details.StartCutOffTime);
                cmd.Parameters.AddWithValue("EndCutOffTime", Details.EndCutOffTime);
                cmd.Parameters.AddWithValue("WithRestaurantFeatures", Details.WithRestaurantFeatures);
                cmd.Parameters.AddWithValue("SeniorCitizenDiscountCode", Details.SeniorCitizenDiscountCode);
                cmd.Parameters.AddWithValue("PWDDiscountCode", Details.PWDDiscountCode);
                cmd.Parameters.AddWithValue("GroupChargeType", Details.GroupChargeType.ChargeTypeID);
                cmd.Parameters.AddWithValue("PersonalChargeType", Details.PersonalChargeType.ChargeTypeID);
                cmd.Parameters.AddWithValue("DefaultTransactionChargeCode", Details.DefaultTransactionChargeCode);
                cmd.Parameters.AddWithValue("DineInChargeCode", Details.DineInChargeCode);
                cmd.Parameters.AddWithValue("TakeOutChargeCode", Details.TakeOutChargeCode);
                cmd.Parameters.AddWithValue("DeliveryChargeCode", Details.DeliveryChargeCode);
                cmd.Parameters.AddWithValue("IsTouchScreen", Details.IsTouchScreen);
                cmd.Parameters.AddWithValue("WillContinueSelectionVariation", Details.WillContinueSelectionVariation);
                cmd.Parameters.AddWithValue("WillContinueSelectionProduct", Details.WillContinueSelectionProduct);
                cmd.Parameters.AddWithValue("WillPrintGrandTotal", Details.WillPrintGrandTotal);
                cmd.Parameters.AddWithValue("ReservedAndCommit", Details.ReservedAndCommit);
                cmd.Parameters.AddWithValue("ShowCustomerSelection", Details.ShowCustomerSelection);

                cmd.CommandText = SQL;
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

        public void UpdateIsCashCountInitialized(Int32 BranchID, string TerminalNo, Int64 CashierID = 0, bool IsCashCountInitialized = false)
        {
            try
            {
                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandType = System.Data.CommandType.Text;

                string SQL = "UPDATE tblCashierReport SET " +
                                "IsCashCountInitialized	= @IsCashCountInitialized " +
                            "WHERE BranchID = @BranchID AND TerminalNo = @TerminalNo ";

                if (CashierID != 0)
                {
                    SQL += "AND CashierID = @CashierID ";
                    cmd.Parameters.AddWithValue("CashierID", CashierID);
                }

                cmd.Parameters.AddWithValue("BranchID", BranchID);
                cmd.Parameters.AddWithValue("TerminalNo", TerminalNo);
                cmd.Parameters.AddWithValue("IsCashCountInitialized", IsCashCountInitialized);

                cmd.CommandText = SQL;
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

        public Int32 Save(TerminalDetails Details)
        {
            try
            {
                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandType = System.Data.CommandType.Text;

                string SQL = "CALL procSaveTerminal(@TerminalID, @TerminalNo, @TerminalCode, @TerminalName, @Status, @DateCreated, " +
                                "@IsPrinterAutoCutter, @MaxReceiptWidth, @TransactionNoLength, @AutoPrint, @PrinterName, " +
                                "@TurretName, @CashDrawerName, @MachineSerialNo, @AccreditationNo, @ItemVoidConfirmation, " +
                                "@EnableEVAT, @FORM_Behavior, @MarqueeMessage, @TrustFund, @IsVATInclusive, @VAT, @EVAT, " +
                                "@LocalTax, @ShowItemMoreThanZeroQty, @ShowOneTerminalSuspendedTransactions, " +
                                "@ShowOnlyPackedTransactions, @TerminalReceiptType, @SalesInvoicePrinterName, " +
                                "@CashCountBeforeReport, @PreviewTerminalReport, @OrderSlipPrinter, @DBVersion, @FEVersion, " +
                                "@BEVersion, @IsPrinterDotmatrix, @IsChargeEditable, @IsDiscountEditable, @CheckCutOffTime, " +
                                "@StartCutOffTime, @EndCutOffTime, @WithRestaurantFeatures, @SeniorCitizenDiscountCode, " +
                                "@IsTouchScreen, @WillContinueSelectionVariation, @WillContinueSelectionProduct, " +
                                "@RETPriceMarkUp, @WSPriceMarkUp, @WillPrintGrandTotal, @ReservedAndCommit, " +
                                "@ShowCustomerSelection, @EnableRewardPoints, @RewardPointsMinimum, @RewardPointsEvery, " +
                                "@RewardPoints, @RoundDownRewardPoints, @AutoGenerateRewardCardNo,  " +
                                "@EnableRewardPointsAsPayment, @RewardPointsMaxPercentageForPayment, " +
                                "@RewardPointsPaymentValue, @RewardPointsPaymentCashEquivalent, @RewardsPermitNo, " +
                                "@IsFineDining, " +
                                //"@InHouseIndividualCreditPermitNo, @InHouseGroupCreditPermitNo, @IsFineDining, " +
                                "@PersonalChargeTypeID, @GroupChargeTypeID, @BranchID, @ProductSearchType, " +
                                "@IncludeCreditChargeAgreement, @IsParkingTerminal, @WillPrintChargeSlip, @WillPrintVoidItem, " +
                                "@IncludeTermsAndConditions, @PWDDiscountCode, @DefaultTransactionChargeCode, @DineInChargeCode, " +
                                "@TakeOutChargeCode, @DeliveryChargeCode, @CreatedOn, @LastModified);";

                cmd.Parameters.AddWithValue("TerminalID", Details.TerminalID);
                cmd.Parameters.AddWithValue("TerminalNo", Details.TerminalNo);
                cmd.Parameters.AddWithValue("TerminalCode", Details.TerminalCode);
                cmd.Parameters.AddWithValue("TerminalName", Details.TerminalName);
                cmd.Parameters.AddWithValue("Status", Details.Status);
                cmd.Parameters.AddWithValue("DateCreated", Details.DateCreated);
                cmd.Parameters.AddWithValue("IsPrinterAutoCutter", Details.IsPrinterAutoCutter);
                cmd.Parameters.AddWithValue("MaxReceiptWidth", Details.MaxReceiptWidth);
                cmd.Parameters.AddWithValue("TransactionNoLength", Details.TransactionNoLength);
                cmd.Parameters.AddWithValue("AutoPrint", Details.AutoPrint);
                cmd.Parameters.AddWithValue("PrinterName", Details.PrinterName);
                cmd.Parameters.AddWithValue("TurretName", Details.TurretName);
                cmd.Parameters.AddWithValue("CashDrawerName", Details.CashDrawerName);
                cmd.Parameters.AddWithValue("MachineSerialNo", Details.MachineSerialNo);
                cmd.Parameters.AddWithValue("AccreditationNo", Details.AccreditationNo);
                cmd.Parameters.AddWithValue("ItemVoidConfirmation", Details.ItemVoidConfirmation);
                cmd.Parameters.AddWithValue("EnableEVAT", Details.EnableEVAT);
                cmd.Parameters.AddWithValue("FORM_Behavior", Details.FORM_Behavior);
                cmd.Parameters.AddWithValue("MarqueeMessage", Details.MarqueeMessage);
                cmd.Parameters.AddWithValue("TrustFund", Details.TrustFund);
                cmd.Parameters.AddWithValue("IsVATInclusive", Details.IsVATInclusive);
                cmd.Parameters.AddWithValue("VAT", Details.VAT);
                cmd.Parameters.AddWithValue("EVAT", Details.EVAT);
                cmd.Parameters.AddWithValue("LocalTax", Details.LocalTax);
                cmd.Parameters.AddWithValue("ShowItemMoreThanZeroQty", Details.ShowItemMoreThanZeroQty);
                cmd.Parameters.AddWithValue("ShowOneTerminalSuspendedTransactions", Details.ShowOneTerminalSuspendedTransactions);
                cmd.Parameters.AddWithValue("ShowOnlyPackedTransactions", Details.ShowOnlyPackedTransactions);
                cmd.Parameters.AddWithValue("TerminalReceiptType", Details.ReceiptType.ToString("d"));
                cmd.Parameters.AddWithValue("SalesInvoicePrinterName", Details.SalesInvoicePrinterName);
                cmd.Parameters.AddWithValue("CashCountBeforeReport", Details.CashCountBeforeReport);
                cmd.Parameters.AddWithValue("PreviewTerminalReport", Details.PreviewTerminalReport);
                cmd.Parameters.AddWithValue("OrderSlipPrinter", Details.OrderSlipPrinter.ToString("d"));
                cmd.Parameters.AddWithValue("DBVersion", Details.DBVersion);
                cmd.Parameters.AddWithValue("FEVersion", Details.FEVersion);
                cmd.Parameters.AddWithValue("BEVersion", Details.BEVersion);
                cmd.Parameters.AddWithValue("IsPrinterDotMatrix", Details.IsPrinterDotMatrix);
                cmd.Parameters.AddWithValue("IsChargeEditable", Details.IsChargeEditable);
                cmd.Parameters.AddWithValue("IsDiscountEditable", Details.IsDiscountEditable);
                cmd.Parameters.AddWithValue("CheckCutOffTime", Details.CheckCutOffTime);
                cmd.Parameters.AddWithValue("StartCutOffTime", Details.StartCutOffTime);
                cmd.Parameters.AddWithValue("EndCutOffTime", Details.EndCutOffTime);
                cmd.Parameters.AddWithValue("WithRestaurantFeatures", Details.WithRestaurantFeatures);
                cmd.Parameters.AddWithValue("SeniorCitizenDiscountCode", Details.SeniorCitizenDiscountCode);
                cmd.Parameters.AddWithValue("IsTouchScreen", Details.IsTouchScreen);
                cmd.Parameters.AddWithValue("WillContinueSelectionVariation", Details.WillContinueSelectionVariation);
                cmd.Parameters.AddWithValue("WillContinueSelectionProduct", Details.WillContinueSelectionProduct);
                cmd.Parameters.AddWithValue("RETPriceMarkUp", Details.RETPriceMarkUp);
                cmd.Parameters.AddWithValue("WSPriceMarkUp", Details.WSPriceMarkUp);
                cmd.Parameters.AddWithValue("WillPrintGrandTotal", Details.WillPrintGrandTotal);
                cmd.Parameters.AddWithValue("ReservedAndCommit", Details.ReservedAndCommit);
                cmd.Parameters.AddWithValue("ShowCustomerSelection", Details.ShowCustomerSelection);
                cmd.Parameters.AddWithValue("EnableRewardPoints", Details.RewardPointsDetails.EnableRewardPoints);
                cmd.Parameters.AddWithValue("RewardPointsMinimum", Details.RewardPointsDetails.RewardPointsMinimum);
                cmd.Parameters.AddWithValue("RewardPointsEvery", Details.RewardPointsDetails.RewardPointsEvery);
                cmd.Parameters.AddWithValue("RewardPoints", Details.RewardPointsDetails.RewardPoints);
                cmd.Parameters.AddWithValue("RoundDownRewardPoints", Details.RewardPointsDetails.RoundDownRewardPoints);
                cmd.Parameters.AddWithValue("AutoGenerateRewardCardNo", Details.AutoGenerateRewardCardNo);
                cmd.Parameters.AddWithValue("EnableRewardPointsAsPayment", Details.RewardPointsDetails.EnableRewardPointsAsPayment);
                cmd.Parameters.AddWithValue("RewardPointsMaxPercentageForPayment", Details.RewardPointsDetails.RewardPointsMaxPercentageForPayment);
                cmd.Parameters.AddWithValue("RewardPointsPaymentValue", Details.RewardPointsDetails.RewardPointsPaymentValue);
                cmd.Parameters.AddWithValue("RewardPointsPaymentCashEquivalent", Details.RewardPointsDetails.RewardPointsPaymentCashEquivalent);
                cmd.Parameters.AddWithValue("RewardsPermitNo", Details.RewardPointsDetails.RewardsPermitNo);
                //cmd.Parameters.AddWithValue("InHouseIndividualCreditPermitNo", Details.InHouseIndividualCreditPermitNo);
                //cmd.Parameters.AddWithValue("InHouseGroupCreditPermitNo", Details.InHouseGroupCreditPermitNo);
                cmd.Parameters.AddWithValue("IsFineDining", Details.IsFineDining);
                cmd.Parameters.AddWithValue("PersonalChargeTypeID", Details.PersonalChargeType.ChargeTypeID);
                cmd.Parameters.AddWithValue("GroupChargeTypeID", Details.GroupChargeType.ChargeTypeID);
                cmd.Parameters.AddWithValue("BranchID", Details.BranchID);
                cmd.Parameters.AddWithValue("ProductSearchType", Details.ProductSearchType);
                cmd.Parameters.AddWithValue("IncludeCreditChargeAgreement", Details.IncludeCreditChargeAgreement);
                cmd.Parameters.AddWithValue("IsParkingTerminal", Details.IsParkingTerminal);
                cmd.Parameters.AddWithValue("WillPrintChargeSlip", Details.WillPrintChargeSlip);
                cmd.Parameters.AddWithValue("WillPrintVoidItem", Details.WillPrintVoidItem);
                cmd.Parameters.AddWithValue("IncludeTermsAndConditions", Details.IncludeTermsAndConditions);
                cmd.Parameters.AddWithValue("PWDDiscountCode", Details.PWDDiscountCode);
                cmd.Parameters.AddWithValue("DefaultTransactionChargeCode", Details.DefaultTransactionChargeCode);
                cmd.Parameters.AddWithValue("DineInChargeCode", Details.DineInChargeCode);
                cmd.Parameters.AddWithValue("TakeOutChargeCode", Details.TakeOutChargeCode);
                cmd.Parameters.AddWithValue("DeliveryChargeCode", Details.DeliveryChargeCode);
                cmd.Parameters.AddWithValue("CreatedOn", Details.CreatedOn == DateTime.MinValue ? Constants.C_DATE_MIN_VALUE : Details.CreatedOn);
                cmd.Parameters.AddWithValue("LastModified", Details.LastModified == DateTime.MinValue ? Constants.C_DATE_MIN_VALUE : Details.LastModified);

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
                            "PWDDiscountCode, " +
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
                            //"InHouseIndividualCreditPermitNo, " +
                            //"InHouseGroupCreditPermitNo, " +
                            "DBVersion, " +
                            "ProductSearchType, " +
                            "IncludeCreditChargeAgreement, " +
                            "IncludeTermsAndConditions, " +
                            "IsParkingTerminal, " +
                            "WillPrintChargeSlip, " +
                            "WillPrintVoidItem, " +
                            "DefaultTransactionChargeCode, " +
                            "DineInChargeCode, " +
                            "TakeOutChargeCode, " +
                            "DeliveryChargeCode, " +
                            "ORSeriesBranchID, " +
                            "ORSeriesTerminalNo " +
						"FROM tblTerminal ";

			return SQL;
		}

		#region Details

        public RewardPointsDetails RewardPointsDetails()
        {
            try
            {
                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandType = System.Data.CommandType.Text;

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

                cmd.CommandText = SQL;
                string strDataTableName = "tbl" + this.GetType().FullName.Split(new Char[] { '.' })[this.GetType().FullName.Split(new Char[] { '.' }).Length - 1]; System.Data.DataTable dt = new System.Data.DataTable(strDataTableName);
                base.MySqlDataAdapterFill(cmd, dt);

                RewardPointsDetails clsRewardPointsDetails = new RewardPointsDetails();
                foreach(System.Data.DataRow dr in dt.Rows)
                {
                    clsRewardPointsDetails.EnableRewardPoints = bool.Parse(dr["EnableRewardPoints"].ToString());
                    clsRewardPointsDetails.RoundDownRewardPoints = bool.Parse(dr["RoundDownRewardPoints"].ToString());
                    clsRewardPointsDetails.RewardPointsMinimum = decimal.Parse(dr["RewardPointsMinimum"].ToString());
                    clsRewardPointsDetails.RewardPointsEvery = decimal.Parse(dr["RewardPointsEvery"].ToString());
                    clsRewardPointsDetails.RewardPoints = decimal.Parse(dr["RewardPoints"].ToString());
                    clsRewardPointsDetails.EnableRewardPointsAsPayment = bool.Parse(dr["EnableRewardPointsAsPayment"].ToString());
                    clsRewardPointsDetails.RewardPointsMaxPercentageForPayment = decimal.Parse(dr["RewardPointsMaxPercentageForPayment"].ToString());
                    clsRewardPointsDetails.RewardPointsPaymentValue = decimal.Parse(dr["RewardPointsPaymentValue"].ToString());
                    clsRewardPointsDetails.RewardPointsPaymentCashEquivalent = decimal.Parse(dr["RewardPointsPaymentCashEquivalent"].ToString());
                    clsRewardPointsDetails.RewardsPermitNo = dr["RewardsPermitNo"].ToString();
                }

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
                // no need to put the BranchID coz it's already the TerminalID
				MySqlCommand cmd = new MySqlCommand();
				cmd.CommandType = System.Data.CommandType.Text;

                string SQL = SQLSelect() + "WHERE TerminalID = @TerminalID;";

                cmd.Parameters.AddWithValue("@TerminalID", TerminalID);

                cmd.CommandText = SQL;
                string strDataTableName = "tbl" + this.GetType().FullName.Split(new Char[] { '.' })[this.GetType().FullName.Split(new Char[] { '.' }).Length - 1]; System.Data.DataTable dt = new System.Data.DataTable(strDataTableName);
                base.MySqlDataAdapterFill(cmd, dt);

                TerminalDetails Details = SetDetails(dt);

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
                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandType = System.Data.CommandType.Text;
				
                string SQL = SQLSelect() + "WHERE BranchID = @BranchID AND TerminalNo = @TerminalNo;";
				  
                cmd.Parameters.AddWithValue("@BranchID", BranchID);
                cmd.Parameters.AddWithValue("@TerminalNo", TerminalNo);

                cmd.CommandText = SQL;
                string strDataTableName = "tbl" + this.GetType().FullName.Split(new Char[] { '.' })[this.GetType().FullName.Split(new Char[] { '.' }).Length - 1]; System.Data.DataTable dt = new System.Data.DataTable(strDataTableName);
                base.MySqlDataAdapterFill(cmd, dt);

                TerminalDetails Details = SetDetails(dt);

				return Details;
			}
			catch (Exception ex)
			{
				throw base.ThrowException(ex);
			}	
		}

        public TerminalDetails Details(string TerminalNo)
        {
            try
            {
                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandType = System.Data.CommandType.Text;

                string SQL = SQLSelect() + "WHERE TerminalNo = @TerminalNo;";

                cmd.Parameters.AddWithValue("@TerminalNo", TerminalNo);

                cmd.CommandText = SQL;
                string strDataTableName = "tbl" + this.GetType().FullName.Split(new Char[] { '.' })[this.GetType().FullName.Split(new Char[] { '.' }).Length - 1]; System.Data.DataTable dt = new System.Data.DataTable(strDataTableName);
                base.MySqlDataAdapterFill(cmd, dt);

                TerminalDetails Details = SetDetails(dt);

                return Details;
            }
            catch (Exception ex)
            {
                throw base.ThrowException(ex);
            }
        }
        private TerminalDetails SetDetails(System.Data.DataTable dt)
        {
            int iPersonalChargeTypeID = 0;
            int iGroupChargeTypeID = 0;

            TerminalDetails Details = new TerminalDetails();

            foreach(System.Data.DataRow dr in dt.Rows)
            {
                Details.BranchID = Int32.Parse(dr["BranchID"].ToString());
                Details.TerminalID = Int32.Parse(dr["TerminalID"].ToString());
                Details.TerminalNo = dr["TerminalNo"].ToString();
                Details.TerminalCode = dr["TerminalCode"].ToString();
                Details.TerminalName = dr["TerminalName"].ToString();
                Details.Status = (TerminalStatus)Enum.Parse(typeof(TerminalStatus), dr["Status"].ToString());
                Details.DateCreated = DateTime.Parse(dr["DateCreated"].ToString());
                Details.IsPrinterAutoCutter = bool.Parse(dr["IsPrinterAutoCutter"].ToString());
                Details.MaxReceiptWidth = Int16.Parse(dr["MaxReceiptWidth"].ToString());
                Details.TransactionNoLength = Int16.Parse(dr["TransactionNoLength"].ToString());
                Details.AutoPrint = (PrintingPreference)Enum.Parse(typeof(PrintingPreference), dr["AutoPrint"].ToString());
                Details.IsVATInclusive = bool.Parse(dr["IsVATInclusive"].ToString());
                Details.PrinterName = dr["PrinterName"].ToString();
                Details.TurretName = dr["TurretName"].ToString();
                Details.CashDrawerName = dr["CashDrawerName"].ToString();
                Details.MachineSerialNo = dr["MachineSerialNo"].ToString();
                Details.AccreditationNo = dr["AccreditationNo"].ToString();
                Details.ItemVoidConfirmation = bool.Parse(dr["ItemVoidConfirmation"].ToString());
                Details.EnableEVAT = bool.Parse(dr["EnableEVAT"].ToString());
                Details.FORM_Behavior = dr["FORM_Behavior"].ToString();
                Details.MarqueeMessage = dr["MarqueeMessage"].ToString();
                Details.TrustFund = decimal.Parse(dr["TrustFund"].ToString());
                Details.VAT = decimal.Parse(dr["VAT"].ToString());
                Details.EVAT = decimal.Parse(dr["EVAT"].ToString());
                Details.LocalTax = decimal.Parse(dr["LocalTax"].ToString());
                Details.ShowItemMoreThanZeroQty = bool.Parse(dr["ShowItemMoreThanZeroQty"].ToString());
                Details.ShowOnlyPackedTransactions = bool.Parse(dr["ShowOnlyPackedTransactions"].ToString());
                Details.ShowOneTerminalSuspendedTransactions = bool.Parse(dr["ShowOneTerminalSuspendedTransactions"].ToString());
                Details.ReceiptType = (TerminalReceiptType)Enum.Parse(typeof(TerminalReceiptType), dr["TerminalReceiptType"].ToString());
                Details.SalesInvoicePrinterName = dr["SalesInvoicePrinterName"].ToString();
                Details.CashCountBeforeReport = bool.Parse(dr["CashCountBeforeReport"].ToString());
                Details.PreviewTerminalReport = bool.Parse(dr["PreviewTerminalReport"].ToString());
                
                // Added May 5, 2009
                Details.IsPrinterDotMatrix = bool.Parse(dr["IsPrinterDotMatrix"].ToString());
                Details.IsChargeEditable = bool.Parse(dr["IsChargeEditable"].ToString());
                Details.IsDiscountEditable = bool.Parse(dr["IsDiscountEditable"].ToString());
                Details.CheckCutOffTime = bool.Parse(dr["CheckCutOffTime"].ToString());
                Details.StartCutOffTime = dr["StartCutOffTime"].ToString();
                Details.EndCutOffTime = dr["EndCutOffTime"].ToString();
                Details.WithRestaurantFeatures = bool.Parse(dr["WithRestaurantFeatures"].ToString());

                // Added Nov 8, 2011
                Details.IsFineDining = bool.Parse(dr["IsFineDIning"].ToString());

                // -- end
                
                Details.SeniorCitizenDiscountCode = dr["SeniorCitizenDiscountCode"].ToString();
                Details.PWDDiscountCode = dr["PWDDiscountCode"].ToString();

                // Added May 21, 2009
                Details.IsTouchScreen = bool.Parse(dr["IsTouchScreen"].ToString());

                // Added June 1, 2010
                Details.WillContinueSelectionVariation = bool.Parse(dr["WillContinueSelectionVariation"].ToString());
                Details.WillContinueSelectionProduct = bool.Parse(dr["WillContinueSelectionProduct"].ToString());

                // Added July 9, 2010
                Details.WSPriceMarkUp = decimal.Parse(dr["WSPriceMarkUp"].ToString());
                Details.RETPriceMarkUp = decimal.Parse(dr["RETPriceMarkUp"].ToString());

                // Added Sep 21, 2010
                Details.WillPrintGrandTotal = bool.Parse(dr["WillPrintGrandTotal"].ToString());

                // Added Apr 12, 2011
                Details.ReservedAndCommit = bool.Parse(dr["ReservedAndCommit"].ToString());       
         
                // Added Sep 10, 2011
                Details.DBVersion = dr["DBVersion"].ToString();

                // Added Oct 17, 2011
                Details.ShowCustomerSelection = bool.Parse(dr["ShowCustomerSelection"].ToString());
                Details.AutoGenerateRewardCardNo = bool.Parse(dr["AutoGenerateRewardCardNo"].ToString());

                // Added Oct 17, 2011 
                RewardPointsDetails clsRewardPointsDetails = new RewardPointsDetails();
                clsRewardPointsDetails.EnableRewardPoints = bool.Parse(dr["EnableRewardPoints"].ToString());
                clsRewardPointsDetails.RoundDownRewardPoints = bool.Parse(dr["RoundDownRewardPoints"].ToString());
                clsRewardPointsDetails.RewardPointsMinimum = decimal.Parse(dr["RewardPointsMinimum"].ToString());
                clsRewardPointsDetails.RewardPointsEvery = decimal.Parse(dr["RewardPointsEvery"].ToString());
                clsRewardPointsDetails.RewardPoints = decimal.Parse(dr["RewardPoints"].ToString());
                // Added Nov 4, 2011 :  For use as payment
                clsRewardPointsDetails.EnableRewardPointsAsPayment = bool.Parse(dr["EnableRewardPointsAsPayment"].ToString());
                clsRewardPointsDetails.RewardPointsMaxPercentageForPayment = decimal.Parse(dr["RewardPointsMaxPercentageForPayment"].ToString());
                clsRewardPointsDetails.RewardPointsPaymentValue = decimal.Parse(dr["RewardPointsPaymentValue"].ToString());
                clsRewardPointsDetails.RewardPointsPaymentCashEquivalent = decimal.Parse(dr["RewardPointsPaymentCashEquivalent"].ToString());
                clsRewardPointsDetails.RewardsPermitNo = dr["RewardsPermitNo"].ToString();

                Details.RewardPointsDetails = clsRewardPointsDetails;

                //Details.InHouseIndividualCreditPermitNo = dr["InHouseIndividualCreditPermitNo"].ToString();
                //Details.InHouseGroupCreditPermitNo = dr["InHouseGroupCreditPermitNo"].ToString();

                iPersonalChargeTypeID = Int32.Parse(dr["PersonalChargeTypeID"].ToString());
                iGroupChargeTypeID = Int32.Parse(dr["GroupChargeTypeID"].ToString());

                // Added Mar 18, 2012
                Details.ProductSearchType = (ProductSearchType)Enum.Parse(typeof(ProductSearchType), dr["ProductSearchType"].ToString());
                
                // Added Apr 29, 2013
                Details.IncludeCreditChargeAgreement = bool.Parse(dr["IncludeCreditChargeAgreement"].ToString());

                // Added Jun 30, 2013
                Details.IsParkingTerminal = bool.Parse(dr["IsParkingTerminal"].ToString());

                Details.WillPrintChargeSlip = bool.Parse(dr["WillPrintChargeSlip"].ToString());
                Details.WillPrintVoidItem = bool.Parse(dr["WillPrintVoidItem"].ToString());

                // Added Oct 20, 2013
                Details.IncludeTermsAndConditions = bool.Parse(dr["IncludeTermsAndConditions"].ToString());

                Details.DefaultTransactionChargeCode = dr["DefaultTransactionChargeCode"].ToString();
                Details.DineInChargeCode = dr["DineInChargeCode"].ToString();
                Details.TakeOutChargeCode = dr["TakeOutChargeCode"].ToString();
                Details.DeliveryChargeCode = dr["DeliveryChargeCode"].ToString();
                Details.ORSeriesBranchID = Int32.Parse(dr["ORSeriesBranchID"].ToString());
                Details.ORSeriesTerminalNo = dr["ORSeriesTerminalNo"].ToString();
            }

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
                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandType = System.Data.CommandType.Text;
                
                string SQL = "SELECT RegistrationKey FROM sysTerminalkey WHERE HDSerialNo = @HDSerialNo;";

                cmd.Parameters.AddWithValue("@HDSerialNo", HDSerialNo);

                cmd.CommandText = SQL;
                string strDataTableName = "tbl" + this.GetType().FullName.Split(new Char[] { '.' })[this.GetType().FullName.Split(new Char[] { '.' }).Length - 1]; System.Data.DataTable dt = new System.Data.DataTable(strDataTableName);
                base.MySqlDataAdapterFill(cmd, dt);

                string strRetvalue = string.Empty;

                foreach(System.Data.DataRow dr in dt.Rows)
                {
                    strRetvalue = "" + dr["RegistrationKey"].ToString();
                }

                return strRetvalue;
            }

            catch (Exception ex)
            {
                throw base.ThrowException(ex);
            }
        }

        public Int64 getLastLoggedCashierID(Int32 BranchID, string TerminalNo)
        {
            try
            {
                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandType = System.Data.CommandType.Text;

                string SQL = "SELECT CashierID FROM tblCashierReport WHERE BranchID = @BranchID AND TerminalNo = @TerminalNo " +
                             "ORDER BY LastLoginDate DESC LIMIT 1;";

                cmd.Parameters.AddWithValue("@BranchID", BranchID);
                cmd.Parameters.AddWithValue("@TerminalNo", TerminalNo);

                cmd.CommandText = SQL;
                string strDataTableName = "tbl" + this.GetType().FullName.Split(new Char[] { '.' })[this.GetType().FullName.Split(new Char[] { '.' }).Length - 1]; System.Data.DataTable dt = new System.Data.DataTable(strDataTableName);
                base.MySqlDataAdapterFill(cmd, dt);

                Int64 iRetvalue = 0;
                foreach (System.Data.DataRow dr in dt.Rows)
                {
                    iRetvalue = Int64.Parse(dr["CashierID"].ToString());
                }

                return iRetvalue;
            }
            catch (Exception ex)
            {
                throw base.ThrowException(ex);
            }
        }

		#endregion
	
		#region Streams

        public System.Data.DataTable ListAsDataTable(string SearchKey = "", string SortField = "TerminalNo", SortOption SortOrder = SortOption.Ascending, Int32 limit = 0)
		{
			try
			{
                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandType = System.Data.CommandType.Text;

                string SQL = SQLSelect();

                if (!string.IsNullOrEmpty(SearchKey))
                {
                    SQL += "WHERE (TerminalNo LIKE @SearchKey " +
                            "OR TerminalCode LIKE @SearchKey " +
                            "OR TerminalName LIKE @SearchKey) ";

                    cmd.Parameters.AddWithValue("@SearchKey", "%" + SearchKey + "%");
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

        public System.Data.DataTable ListORSeries(string ORSeriesTerminalNo, string SortField = "TerminalNo", SortOption SortOrder = SortOption.Ascending, Int32 limit = 0)
        {
            try
            {
                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandType = System.Data.CommandType.Text;

                string SQL = SQLSelect();

                SQL += "WHERE ORSeriesTerminalNo = @ORSeriesTerminalNo ";

                cmd.Parameters.AddWithValue("@ORSeriesTerminalNo", ORSeriesTerminalNo);

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

        #region Validate: IsExist & IsZReadInitialized, IsCashCountInitialized

        public bool IsExist(Int32 BranchID, string TerminalNo, out TerminalDetails OutDetails)
		{
			try
			{
                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandType = System.Data.CommandType.Text;

				bool boRetValue = false;

				string SQL=	SQLSelect() + "WHERE BranchID = @BranchID AND TerminalNo = @TerminalNo;";

                cmd.Parameters.AddWithValue("@BranchID", BranchID);
                cmd.Parameters.AddWithValue("@TerminalNo", TerminalNo);

                cmd.CommandText = SQL;
                string strDataTableName = "tbl" + this.GetType().FullName.Split(new Char[] { '.' })[this.GetType().FullName.Split(new Char[] { '.' }).Length - 1]; System.Data.DataTable dt = new System.Data.DataTable(strDataTableName);
                base.MySqlDataAdapterFill(cmd, dt);

                OutDetails = SetDetails(dt);

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
                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandType = System.Data.CommandType.Text;

                bool boRetValue = false;

                string SQL = "SELECT IsCashCountInitialized FROM tblCashierReport " +
                               "WHERE BranchID = @BranchID AND TerminalNo = @TerminalNo AND CashierID = @CashierID;";

                cmd.Parameters.AddWithValue("@BranchID", BranchID);
                cmd.Parameters.AddWithValue("@TerminalNo", TerminalNo);
                cmd.Parameters.AddWithValue("@CashierID", CashierID);

                cmd.CommandText = SQL;
                string strDataTableName = "tbl" + this.GetType().FullName.Split(new Char[] { '.' })[this.GetType().FullName.Split(new Char[] { '.' }).Length - 1]; System.Data.DataTable dt = new System.Data.DataTable(strDataTableName);
                base.MySqlDataAdapterFill(cmd, dt);

                foreach(System.Data.DataRow dr in dt.Rows)
                {
                    boRetValue = bool.Parse(dr["IsCashCountInitialized"].ToString());
                }

                return boRetValue;
            }
            catch (Exception ex)
            {
                throw base.ThrowException(ex);
            }
        }

        public Int32 BranchCount(string TerminalNo)
        {
            try
            {
                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandType = System.Data.CommandType.Text;

                Int32 iRetValue = 0;

                string SQL = "SELECT COUNT(BranchID) BranchCount FROM tblTerminal " +
                               "WHERE TerminalNo = @TerminalNo;";

                cmd.Parameters.AddWithValue("@TerminalNo", TerminalNo);

                cmd.CommandText = SQL;
                string strDataTableName = "tbl" + this.GetType().FullName.Split(new Char[] { '.' })[this.GetType().FullName.Split(new Char[] { '.' }).Length - 1]; System.Data.DataTable dt = new System.Data.DataTable(strDataTableName);
                base.MySqlDataAdapterFill(cmd, dt);

                foreach (System.Data.DataRow dr in dt.Rows)
                {
                    iRetValue = Int32.Parse(dr["BranchCount"].ToString());
                }

                return iRetValue;
            }
            catch (Exception ex)
            {
                throw base.ThrowException(ex);
            }
        }

		#endregion
	}
}


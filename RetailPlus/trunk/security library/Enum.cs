using System;
using System.Security.Permissions;

/******************************************************************************
	**		Auth: Lemuel E. Aceron
	**		Date: March 29, 2005
	***************************************************************************
	**		Change History
	***************************************************************************
	**		Date:			Author:				Description:
	**		--------		--------			-------------------------------
	**      26-03-2006			Lemuel				Add ApplyItemDiscount		=	78,
	**												Add ApplyTransDiscount		=	79,
	**												Add EnterCreditPayment		=	80,
	**												Add PrintTransactionHeader	=	81
	***************************************************************************/

namespace AceSoft.RetailPlus
{

	public enum AccessTypes
	{
        None                    =   0,
		LoginBE					=	1,
		Home					=	2,
		MasterFilesMenu			=	3,
		CardType				=	4,
		ChargeType				=	5,
		Variations				=	6,
		UnitMeasurement			=	7,
		ProductGroups			=	8,
		ProductGroupVariations	=	9,
		ProductSubGroups		=	10,
		ProductSubGroupVariations	=	11,
		Products				=	12,
		ProductVariations		=	13,
		ProductPackage			=   14,
		ProductVariationsPackage	=	15,
		Discounts				=	16,
		Promos					=	17,
		Contacts				=	18,
		ContactGroups			=	19,
		InventoryMenu			=	20,
		InventoryList			=	21,
		StockTypes				=	22,
		StockTransactions		=	23,
		ReportsMenu				=	24,
		ProductsListReport		=	25,
		InventoryReport			=	26,
		ReorderReport			=	27,
		OverStockReport			=	28,
		PricesReport			=	29,
		SalesTransactionReport	=	30,
		ContactsReport			=	31,
		CustomerCreditReport	=	32,
		CustomersWithCreditReport	=	33,
		MostSalableItemsReport	=	34,
		LeastSalableItemsReport	=	35,
		DiscountsReport			=	36,
		PrintReceiptsReport		=	37,
		ReturnedItemsReport		=	38,
		VoidedItemsReport		=	39,
		RefundedItemsReport		=	40,
		SalesReports			=	41,
		AdministrationFilesMenu	=	42,
		CompanyInfo				=	43,
		AccessGroups			=	44,
		AccessUsers				=	45,
		AccessRights			=	46,
		ReportFormat			=	47,
		Terminal				=	48,
		/******************************
		 * This are for FE requirements
		 ******************************/
		LoginFE					=	49,
		InitializeXRead			=	50,
		InitializeZRead			=	51,
		CreateTransaction		=	52,
		CloseTransaction		=	53,
		SuspendTransaction		=	54,
		ResumeTransaction		=	55,
		VoidTransaction			=	56,
		RefundTransaction		=	57,
		ReprintTransaction		=	58,
		Withhold				=	59,
		Disburse				=	60,
		PaidOut					=	61,
		Denomination			=	62,
		CashCount				=	63,
		CreditPayment			=	64,
		OpenDrawer				=	65,
		VoidItem				=	66,
		ChangePrice				=	67,
		ChangeQuantity			=	68,
		PrintXRead				=	69,
		PrintZRead				=	70,
		PrintTerminalReport		=	71,
		PrintCashierReport		=	72,
		PrintHourlyReport		=	73,
		PrintGroupReport		=	74,
		PrintPLUReport			=	75,
		PrintElectronicJournal	=	76,
		LogoutFE				=	77,
		ApplyItemDiscount		=	78,
		ApplyTransDiscount		=	79,
		EnterCreditPayment		=	80,
		PrintTransactionHeader	=	81,
		/******************************
		 * Mall  Accreditation Info.
		 ******************************/
		AyalaInfo				=	82,
		LockTerminal			=	83,
		UnlockTerminal			=	84,
		EnterFloat				=	85,
		LoginLogoutReport		=	86,
		ReturnItem				=	87,
		/******************************
		 * Added: Mar 8, 2007 Lemuel E. Aceron
		 ******************************/
		Branch					=	88,
		BranchTransfer			=	89,
		BranchUpload			=	90,
		Deposit					=	91,
		/******************************
		 * Product Composition
		 * Added: Nov 23, 2007 Lemuel E. Aceron
		 ******************************/
		ProductComposition		=	92,
		/******************************
		 * Purchases for ERP
		 * Added: July 20, 2007 Lemuel E. Aceron
		 ******************************/
		PurchasesAndPayablesMenu=	93,
		PurchaseOrders			=	94,
		GRN						=	95,
		AccountsPayable			=	96,
		PostingDates			=	97,
		PurchaseAnalysis		=	98,
		AccountSummary			=	99,
		AccountCategory			=	100,
		ChartOfAccounts			=	101,
		PaymentJournals			=	102,
		GeneralLedgerMenu		=	103,
		//SalesReceivableMenu		=	104,
		PurchaseReturns			=	105,
		PurchaseDebitMemo		=	106,
		/******************************
		 * Sales for ERP
		 * Added: October 9, 2007 Lemuel E. Aceron
		 ******************************/
		SalesOrders				=	107,
		SalesJournals			=	108,
		SalesReturns			=	109,
		SalesCreditMemos		=	110,
		SalesAndReceivablesMenu	=	111,
		/******************************
		 * Sales for ERP
		 * Added: February 17, 2008 Lemuel E. Aceron
		 ******************************/
		TransferIn				=	112,
		TransferOut				=	113,
		/******************************
		 * Sales for ERP
		 * Added: February 21, 2008 Lemuel E. Aceron
		 ******************************/
		InvAdjustment			=	114,
		CloseInventory			=	115,
        /******************************
		 * Export and Import Inventory Count
		 * Added: February 26, 2008 Lemuel E. Aceron
		 ******************************/
		ExportInventoryCount	=	116,
		ImportInventoryCount	=	117,
        SynchronizeInventoryCount = 118,
        Banks                   =   119,
        /******************************
		 * For packing terminal
		 * Added: November 28, 2008 Lemuel E. Aceron
		 ******************************/
        PackUnpackTransaction   = 120,
        /******************************
		 * Total Stock Report
		 * Added: December 26, 2008 Lemuel E. Aceron
		 ******************************/
        TotalStockReport        = 121,
        /******************************
		 * Tax Code Setup
		 * Added: January 11, 2009 Lemuel E. Aceron
		 ******************************/
        ItemSetupFinancial      = 122,
        APLinkConfig            = 123,
        /******************************
		 * Restaurant Reports
		 * Added: February 7, 2009 Lemuel E. Aceron
		 ******************************/
        ReprintZRead            = 124,
        PLUReportPerOrderSlipPrinter    = 125,
        /******************************
        * Mall Forwarder
        * Added: April 18, 2009 Lemuel E. Aceron
        ******************************/
        MallForwarder           = 126,
        /******************************
        * ChangeProductPrices
        * Added: June 7, 2009 Lemuel E. Aceron
        ******************************/
        ChangeProductPrices     = 127,
        /******************************
        * For AccountingSystem
        * Added: March 17,2010 Lemuel E. Aceron
        *******************************/
        BankDeposits            = 128,
        WriteCheques            = 129,
        FundTransfers           = 130,
        ReconcileAccounts       = 131,
        /******************************
        * For AccountingSystem
        * Added: April 10,2010 Lemuel E. Aceron
        *******************************/
        GeneralJournals			= 132,
        /******************************
        * For AccountingSystem
        * Added: May 21,2010 Lemuel E. Aceron
        *******************************/
        SynchronizeBranchProducts     = 133,
        /******************************
        * For Agents Commision
        * Added: September 21, 2010 Lemuel E. Aceron
        *******************************/
        AgentCommisionReport    = 134,
        Position = 135,
        Department = 136,
        AgentSalesReport = 137,
        /******************************
        * For Releasing of Items
        * Added: April 7, 2011 Lemuel E. Aceron
        *******************************/
        ReleaseItems = 138,
        /******************************
        * For Reward Points
        * Added: Sep 14, 2011 Lemuel E. Aceron
        *******************************/
        InventoryAnalyst = 139,
        RewardPointsSetup = 140,
        RewardCardIssuance = 141,
        RewardCardChange = 142,
        RewardPointsReedemption = 143,
        RewardItemsSetup = 144,
        /******************************
        * For Credit Cards
        * Added: Nov 2, 2011 Lemuel E. Aceron
        *******************************/
        CreditCardIssuance = 145,
        CreditCardChange = 146,
        /******************************
       * For Reward Membership information
       * Added: Aug 8, 2013 Lemuel E. Aceron
       *******************************/
        CustomerManagement = 147,
        /******************************
       * For Additional reporting of salestransaction
       * Added: Feb 19, 2014 Lemuel E. Aceron
       *******************************/
        SummarizedDailySales = 148,
        SummarizedDailySalesWithTF = 149,
        PaidOutDisburseROC = 150,
        ManagementReports = 151,
        AnalyticsReports = 152,

        InternalCreditCardSetup = 153,
        CreditorsWithoutGuarantor = 154,
        CreditorsWithoutGuarantorPurchases = 155,
        CreditorsWithoutGuarantorPayments = 156,
        CreditorsWithoutGuarantorReserve1 = 157,
        CreditorsWithoutGuarantorReserve2 = 158,
        CreditorsWithoutGuarantorReserve3 = 159,
        CreditorsLedgerSummary = 160,
        CreditorsWithGuarantor = 161,
        CreditorsWithGuarantorPurchases = 162,
        CreditorsWithGuarantorPayments = 163,
        GuarantorsLedgerSummary = 164,
        CreditorsWithGuarantorReserve2 = 165,
        CreditorsWithGuarantorReserve3 = 166,
        CreditorsWithGuarantorReserve4 = 167,
        CreditCardRenewal = 168,
        /******************************
      * For Additional reporting of salestransaction
      * Added: Feb 19, 2014 Lemuel E. Aceron
      *******************************/
        CreditPaymentReversal = 169,
        CreditAmountDueAdjustment = 170,

        ChangeOrderType = 171,

        ChangeOSPrinter = 172,
        PrintShlevesTagPrice = 173,

        ZeroOutBranchInventory = 174,
        SummarizedDailySalesWithTFDetailed = 175,
        PrintCheckOutBill = 176,
        ResumeSuspendedOpenTransaction = 177,

        // 13Mar2015 : use only for manager's MPC
        ChangePriceLevel = 178
    }

    /// <summary>
    /// [04/12/201]
    ///     These are default system created access groups
    ///     that should not be renamed or altered even though it is not applicable.
    /// </summary>
    public enum AccessGroupTypes
    {
        All,
        Administrators,
        Cashiers,
        Waiters,
        Bagger
    }
}

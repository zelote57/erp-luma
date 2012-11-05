using System;

namespace AceSoft.RetailPlus
{
    public enum ExportTypes
    {
        PDF     = 0,
        Word    = 1,
        Excel   = 2
    }

	public enum TransactionStatus
	{
		Open				=	0,
		Closed				=	1,
		Suspended			=	2,
		Void				=	3,
		Reprinted			=	4,
		Refund				=	5,
		NotYetApplied		=	6,
		CreditPayment		=	7,
        DebitPayment        =   8,
        Released            =   9,
        OrderSlip           =   10
	}
	
	public enum TransactionItemStatus
	{
		Valid				=	0,
		Void				=	1,
		Trash				=	2,
		Return				=	3,
		Refund				=	4,
        OrderSlip           =   5
	}

	public enum TerminalStatus
	{
		New					=	0,
		XRead				=	1,
		ZRead				=	2
	}

	public enum PaymentTypes
	{
		Cash				=	0,
		Cheque				=	1,
		CreditCard			=	2,
		Combination			=	3,
		NotYetAssigned		=	4,
		Credit				=	5,
		Debit				=	6,
        RewardPoints        = 7
	}

	public enum StockDirections
	{
		Increment			=	0,
		Decrement			=	1
	}

	public enum PromoTypes
	{
		NotApplicable				=	0,
		ValueOffAfterQtyReached		=	1,
		PercentOffAfterQtyReached	=	2
	}

	public enum DiscountTypes
	{
		NotApplicable	=	0,
		FixedValue		=	1,
		Percentage		=	2
	}

	public enum ChargeTypes
	{
		NotApplicable	=	0,
		FixedValue		=	1,
		Percentage		=	2
	}

	public enum RegistrationType
	{
		Error			=	0,
		DEMO_Unexpired	=	1,
		DEMO_Expired	=	2,
		Registered		=	3
	}

	public enum CONSTANT_VARIATIONS
	{	
		EXPIRATION		=	1,
		SIZE			=	2,
		COLOR			=	3,
		LENGTH			=	4,
		WIDTH		=	5
	}

	public enum StockStatus
	{
		Open			=	0,
		Posted			=	1,
		Cancelled		=	2
	}

	public enum POStatus
	{
		Open			=	0,
		Posted			=	1,
		Cancelled		=	2
	}

	public enum POItemStatus
	{
		Valid			=	0,
		Posted			=	1,
		Cancelled		=	2
	}

    public enum POItemReceivedStatus
    {
        NotYetReceived = 0,
        Received = 1
    }

    public enum DebitMemoItemReceivedStatus
    {
        NotYetReceived = 0,
        Received = 1
    }

    public enum TransferInItemReceivedStatus
    {
        NotYetReceived = 0,
        Received = 1
    }

    public enum TransferOutItemReceivedStatus
    {
        NotYetReceived = 0,
        Received = 1
    }

    public enum BranchTransferStatus
    {
        Open = 0,
        Posted = 1,
        Cancelled = 2
    }

    public enum BranchTransferItemStatus
    {
        Valid = 0,
        Posted = 1,
        Cancelled = 2
    }

	public enum AccountPaymentsStatus
	{
		Open			=	0,
		Posted			=	1,
		Cancelled		=	2
	}

    public enum AccountGJournalsStatus
    {
        Open = 0,
        Posted = 1,
        Cancelled = 2
    }

	public enum POReturnStatus
	{
		Open			=	0,
		Posted			=	1,
		Cancelled		=	2
	}

	public enum POReturnItemStatus
	{
		Valid			=	0,
		Posted			=	1,
		Cancelled		=	2
	}

	public enum DebitMemoStatus
	{
		Open			=	0,
		Posted			=	1,
		Cancelled		=	2
	}

	public enum DebitMemoItemStatus
	{
		Valid			=	0,
		Posted			=	1,
		Cancelled		=	2
	}

	public enum SOStatus
	{
		Open			=	0,
		Posted			=	1,
		Cancelled		=	2
	}

	public enum SOItemStatus
	{
		Valid			=	0,
		Posted			=	1,
		Cancelled		=	2
	}

	public enum SOReturnStatus
	{
		Open			=	0,
		Posted			=	1,
		Cancelled		=	2
	}

	public enum SOReturnItemStatus
	{
		Valid			=	0,
		Posted			=	1,
		Cancelled		=	2
	}

	public enum CreditMemoStatus
	{
		Open			=	0,
		Posted			=	1,
		Cancelled		=	2
	}

	public enum CreditMemoItemStatus
	{
		Valid			=	0,
		Posted			=	1,
		Cancelled		=	2
	}

	public enum TransferInStatus
	{
		Open			=	0,
		Posted			=	1,
		Cancelled		=	2
	}

	public enum TransferInItemStatus
	{
		Valid			=	0,
		Posted			=	1,
		Cancelled		=	2
	}

	public enum TransferOutStatus
	{
		Open			=	0,
		Posted			=	1,
		Cancelled		=	2
	}

	public enum TransferOutItemStatus
	{
		Valid			=	0,
		Posted			=	1,
		Cancelled		=	2
	}

	public enum PrintingPreference
	{
		Normal			=	0,
		Auto			=	1,
		AskFirst		=	2,
        Disabled        =   3
	}

    public enum AccountClassificationType
    {
        BalanceSheet = 0,
        IncomeStatement = 1
    }

    public enum POPaymentStatus
    { 
        Unpaid = 0,
        ForProcessing = 1,
        Partially = 2,
        FullyPaid = 3
    }

    public enum SOPaymentStatus
    {
        Unpaid = 0,
        ForProcessing = 1,
        Partially = 2,
        FullyPaid = 3
    }

    public enum TransferInPaymentStatus
    {
        Unpaid = 0,
        ForProcessing = 1,
        Partially = 2,
        FullyPaid = 3
    }

    public enum TransferOutPaymentStatus
    {
        Unpaid = 0,
        ForProcessing = 1,
        Partially = 2,
        FullyPaid = 3
    }

    public enum InvAdjustmentPaymentStatus
    {
        Unpaid = 0,
        ForProcessing = 1,
        Partially = 2,
        FullyPaid = 3
    }

    public enum BranchTransferPaymentStatus
    {
        Unpaid = 0,
        ForProcessing = 1,
        Partially = 2,
        FullyPaid = 3
    }

    public enum ModeOfTerms
    {
        Days = 0,
        Months = 1,
        Years = 2
    }

    public enum TerminalReceiptType
    { 
        Default = 0,
        SalesInvoice = 1,
        DeliveryReceipt = 2,
        SalesInvoiceAndDR = 3,
        SalesInvoiceOrDR = 5,
        SalesInvoiceForLX300Printer = 4,
        SalesInvoiceForLX300PlusPrinter = 6,
        SalesInvoiceForLX300PlusAmazon = 7
    }

    public enum OrderSlipPrinter
    {
        RetailPlusOSPrinter1 = 0,
        RetailPlusOSPrinter2 = 1,
        RetailPlusOSPrinter3 = 2,
        RetailPlusOSPrinter4 = 3,
        RetailPlusOSPrinter5 = 4
    }

    public enum OrderTypes
    { 
        DineIn = 0,
        TakeOut = 1,
        Delivery = 2
    }

    public enum SaleperItemFilterType
    {
        ShowBothPositiveAndNegative = 0,
        ShowPositiveOnly = 1,
        ShowNegativeOnly = 2
    }

    public enum ProductListFilterType
    {
        ShowActiveAndInactive = 0,
        ShowActiveOnly = 1,
        ShowInactiveOnly = 2
    }

    public enum TransactionListFilterType
    {
        ShowActiveAndInactive = 0,
        ShowActiveOnly = 1,
        ShowInactiveOnly = 2
    }

    public enum BankDepositStatus
    {
        Open = 0,
        Posted = 1,
        Cancelled = 2
    }

    public enum BankDepositType
    {
        FinancialAccount = 0,
        Vendor = 1,
        Customer = 2
    }

    public enum RewardCardStatus
    {
        New = 0,
        Lost = 1,
        Expired = 2,
        Replaced_Lost = 3,
        Replaced_Expired = 4,
        ReNew = 5,
        Reactivated_Lost = 6,
        Manual = 7
    }

    public enum CreditCardStatus
    {
        New = 0,
        Lost = 1,
        Expired = 2,
        Replaced_Lost = 3,
        Replaced_Expired = 4,
        ReNew = 5,
        Reactivated_Lost = 6,
        Manual = 7
    }

    public enum CreditType
    {
        Individual = 0,
        Group = 1
    }

    public enum ChargeSlipType
    {
        Original,
        Guarantor
    }

    public enum ProductSearchType
    {
        BarcodeOnly,
        BarcodeProductCode,
        BarcodeProductCodeProductDesc,
        ProductCode,
        ProductDesc,
        ProductCodeProductDesc
    }

}
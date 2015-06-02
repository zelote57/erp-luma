using System;
using System.Security.Permissions;
using MySql.Data.MySqlClient;
using AceSoft.RetailPlus;

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
	#region Struct

	public struct SalesTransactionDetails
	{
		public Int64 TransactionID;
        public TransactionTypes TransactionType;
		public string TransactionNo;
        public string ORNo;
		public int BranchID;
		public string BranchCode;
        public Int64 RewardsCustomerID;
        public string RewardsCustomerName;
        public ContactDetails CustomerDetails;
		public Int64 CustomerID;
		public string CustomerName;
        public string CustomerGroupName;
		public Int64 CashierID;
		public string CashierName;
		public string TerminalNo;
		public DateTime TransactionDate;
		public DateTime DateSuspended;
		public DateTime DateResumed;
		public TransactionStatus TransactionStatus;
        public decimal GrossSales;
		public decimal SubTotal;
        public decimal NetSales;
		public decimal Discount;
        public decimal SNRDiscount;
        public decimal PWDDiscount;
        public decimal OtherDiscount;
		public decimal TransDiscount;
		public DiscountTypes TransDiscountType;
		public decimal VAT;
		public decimal VATableAmount;
        public decimal ZeroRatedSales;
        public decimal NonVATableAmount;
        public decimal VATExempt;
		public decimal EVAT;
		public decimal EVATableAmount;
        public decimal NonEVATableAmount;
		public decimal LocalTax;
		public decimal AmountPaid;
		public decimal CashPayment;
		public decimal ChequePayment;
		public decimal CreditCardPayment;
		public decimal CreditPayment;
		public decimal DebitPayment;
		public decimal BalanceAmount;
		public decimal ChangeAmount;
		public DateTime DateClosed;
		public PaymentTypes PaymentType;
		public bool isExist;
		public string DiscountCode;
		public string DiscountRemarks;
		public SalesTransactionItemDetails[] TransactionItems;
		public Int64 WaiterID;
		public string WaiterName;
		public decimal ItemsDiscount;
        public decimal SNRItemsDiscount;
        public decimal PWDItemsDiscount;
        public decimal OtherItemsDiscount;
		public decimal Charge;
		public decimal ChargeAmount;
		public string ChargeCode;
		public string ChargeRemarks;
        public ChargeTypes ChargeType;
		public decimal CreditChargeAmount;

		/**
		 * Added on Feb 15, 2008 
		 * Hold all variables in FE
		 * */
		public decimal ItemSold;
		public decimal QuantitySold;
		public decimal DiscountableAmount;
		public decimal AmountDue;
		/**
		 * Added on May 1, 2009
		 * as per request by OX Family
		 * */
		public OrderTypes OrderType;
		/**
		 * Added on Feb 26, 2010
		 * as per request by Lemuel for Agents Commision
		 * */
		public Int64 AgentID;
		public string AgentName;
		/**
		 * Added on April 26, 2010
		 * as per request by Edison to track the creator of transaction
		 * */
		public Int64 CreatedByID;
		public string CreatedByName;

		public string AgentPositionName;
		public string AgentDepartmentName;
		/**
		 * Added on Oct 25, 2011
		 * for reward system information
		 * */
		public bool RewardCardActive;
		public string RewardCardNo;
		public DateTime RewardCardExpiry;
		public decimal RewardPreviousPoints;
		public decimal RewardEarnedPoints;
		public decimal RewardCurrentPoints;
		/**
		 * Nov 4, 2011
		 * for reward points payment
		 * */
		public decimal RewardPointsPayment;
		public decimal RewardConvertedPayment;

		public int PaxNo;

        public ModeOfTerms ModeOfTerms;
        public Int32 Terms;
        public Int64 CRNo;

		public DateTime TransactionDateFrom;
		public DateTime TransactionDateTo;

        public bool isZeroRated;
        public bool isConsignment;
        public string isConsignmentSearch;

        public string DataSource;
        public decimal TrustFund;

        //this will only be use during the on-time computation
        public Data.PaymentDetails PaymentDetails;
	}

	public struct SalesTransactionsColumns
	{
		public bool TransactionID;
        public bool TransactionType;
		public bool TransactionNo;
        public bool ORNo;
		public bool BranchID;
		public bool BranchCode;
        public bool RewardsCustomerID;
        public bool RewardsCustomerName;
		public bool CustomerID;
		public bool CustomerName;
        public bool CustomerGroupName;
		public bool CashierID;
		public bool CashierName;
		public bool TerminalNo;
		public bool TransactionDate;
		public bool DateSuspended;
		public bool DateResumed;
		public bool TransactionStatus;
		public bool TransactionStatusName;
        public bool GrossSales;
		public bool SubTotal;
        public bool NetSales;
		public bool Discount;
		public bool TransDiscount;
		public bool TransDiscountType;
		public bool VAT;
		public bool VATableAmount;
        public bool ZeroRatedSales;
        public bool NonVATableAmount;
        public bool VATExempt;
		public bool EVAT;
		public bool EVATableAmount;
        public bool NonEVATableAmount;
		public bool LocalTax;
		public bool AmountPaid;
		public bool CashPayment;
		public bool ChequePayment;
		public bool CreditCardPayment;
		public bool CreditPayment;
		public bool DebitPayment;
		public bool BalanceAmount;
		public bool ChangeAmount;
		public bool DateClosed;
		public bool PaymentType;
		public bool isExist;
		public bool DiscountCode;
		public bool DiscountRemarks;
		public bool TransactionItems;
		public bool WaiterID;
		public bool WaiterName;
		public bool ItemsDiscount;
		public bool Charge;
		public bool ChargeAmount;
		public bool ChargeCode;
		public bool ChargeRemarks;
		public bool CreditChargeAmount;

		/**
		 * Added on Feb 15, 2008 
		 * Hold all variables in FE
		 * */
		public bool ItemSold;
		public bool QuantitySold;
		public bool DiscountableAmount;
		public bool AmountDue;
		/**
		 * Added on May 1, 2009
		 * as per request by OX Family
		 * */
		public bool OrderType;
		/**
		 * Added on Feb 26, 2010
		 * as per request by Lemuel for Agents Commision
		 * */
		public bool AgentID;
		public bool AgentName;
		/**
		 * Added on April 26, 2010
		 * as per request by Edison to track the creator of transaction
		 * */
		public bool CreatedByID;
		public bool CreatedByName;

		public bool AgentPositionName;
		public bool AgentDepartmentName;
		/**
		 * Added on Oct 25, 2011
		 * for reward system information
		 * */
		public bool RewardCardActive;
		public bool RewardCardNo;
		public bool RewardCardExpiry;
		public bool RewardPreviousPoints;
		public bool RewardEarnedPoints;
		public bool RewardCurrentPoints;
		/**
		 * Nov 4, 2011
		 * for reward points payment
		 * */
		public bool RewardPointsPayment;
		public bool RewardConvertedPayment;

		public bool PaxNo;
        public bool ModeOfterms;
        public bool Terms;
        public bool CRNo;

        public bool isConsignment;
        public bool isZeroRated;
	}

	public struct SalesTransactionsColumnNames
	{
		public const string TransactionID = "TransactionID";
        public const string TransactionType = "TransactionType";
		public const string TransactionNo = "TransactionNo";
        public const string ORNo = "ORNo";
		public const string BranchID = "BranchID";
		public const string BranchCode = "BranchCode";
        public const string RewardsCustomerID = "RewardsCustomerID";
        public const string RewardsCustomerName = "RewardsCustomerName";
		public const string CustomerID = "CustomerID";
		public const string CustomerName = "CustomerName";
        public const string CustomerGroupName = "CustomerGroupName";
		public const string CashierID = "CashierID";
		public const string CashierName = "CashierName";
		public const string TerminalNo = "TerminalNo";
		public const string TransactionDate = "TransactionDate";
		public const string DateSuspended = "DateSuspended";
		public const string DateResumed = "DateResumed";
		public const string TransactionStatus = "TransactionStatus";
		public const string TransactionStatusName = "TransactionStatusName";
        public const string GrossSales = "GrossSales";
		public const string SubTotal = "SubTotal";
        public const string NetSales = "NetSales";
		public const string Discount = "Discount";
        public const string SNRDiscount = "SNRDiscount";
        public const string PWDDiscount = "PWDDiscount";
        public const string OtherDiscount = "OtherDiscount";
		public const string TransDiscount = "TransDiscount";
		public const string TransDiscountType = "TransDiscountType";
		public const string VAT = "VAT";
		public const string VATableAmount = "VATableAmount";
        public const string ZeroRatedSales = "ZeroRatedSales";
        public const string NonVATableAmount = "NonVATableAmount";
        public const string VATExempt = "VATExempt";
		public const string EVAT = "EVAT";
		public const string EVATableAmount = "EVATableAmount";
        public const string NonEVATableAmount = "NonEVATableAmount";
		public const string LocalTax = "LocalTax";
		public const string AmountPaid = "AmountPaid";
		public const string CashPayment = "CashPayment";
		public const string ChequePayment = "ChequePayment";
		public const string CreditCardPayment = "CreditCardPayment";
		public const string CreditPayment = "CreditPayment";
		public const string DebitPayment = "DebitPayment";
		public const string BalanceAmount = "BalanceAmount";
		public const string ChangeAmount = "ChangeAmount";
		public const string DateClosed = "DateClosed";
		public const string PaymentType = "PaymentType";
		public const string isExist = "isExist";
		public const string DiscountCode = "DiscountCode";
		public const string DiscountRemarks = "DiscountRemarks";
		public const string TransactionItems = "TransactionItems";
		public const string WaiterID = "WaiterID";
		public const string WaiterName = "WaiterName";
		public const string ItemsDiscount = "ItemsDiscount";
        public const string SNRItemsDiscount = "SNRItemsDiscount";
        public const string PWDItemsDiscount = "PWDItemsDiscount";
        public const string OtherItemsDiscount = "OtherItemsDiscount";
		public const string Charge = "Charge";
		public const string ChargeAmount = "ChargeAmount";
		public const string ChargeCode = "ChargeCode";
		public const string ChargeRemarks = "ChargeRemarks";
		public const string CreditChargeAmount = "CreditChargeAmount";

		/**
		 * Added on Feb 15, 2008 
		 * Hold all variables in FE
		 * */
		public const string ItemSold = "ItemSold";
		public const string QuantitySold = "QuantitySold";
		public const string DiscountableAmount = "DiscountableAmount";
		public const string AmountDue = "AmountDue";
        /**
		 * Added on May 1, 2009
		 * as per request by OX Family
		 * */
		public const string OrderType = "OrderType";
		/**
		 * Added on Feb 26, 2010
		 * as per request by Lemuel for Agents Commision
		 * */
		public const string AgentID = "AgentID";
		public const string AgentName = "AgentName";
		/**
		 * Added on April 26, 2010
		 * as per request by Edison to track the creator of transaction
		 * */
		public const string CreatedByID = "CreatedByID";
		public const string CreatedByName = "CreatedByName";

		public const string AgentPositionName = "AgentPositionName";
		public const string AgentDepartmentName = "AgentDepartmentName";
		/**
		 * Added on Oct 25, 2011
		 * for reward system information
		 * */
		public const string RewardCardActive = "RewardCardActive";
		public const string RewardCardNo = "RewardCardNo";
		public const string RewardCardExpiry = "RewardCardExpiry";
		public const string RewardPreviousPoints = "RewardPreviousPoints";
		public const string RewardEarnedPoints = "RewardEarnedPoints";
		public const string RewardCurrentPoints = "RewardCurrentPoints";
		/**
		 * Nov 4, 2011
		 * for reward points payment
		 * */
		public const string RewardPointsPayment = "RewardPointsPayment";
		public const string RewardConvertedPayment = "RewardConvertedPayment";

		public const string PaxNo = "PaxNo";
        public const string ModeOfTerms = "ModeOfTerms";
        public const string Terms = "Terms";
        public const string CRNo = "CRNo";

        public const string isConsignment = "isConsignment";
        public const string isZeroRated = "isZeroRated";

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
    public class SalesTransactions : POSConnection
	{
		#region Constructors and Destructors

		public SalesTransactions()
            : base(null, null)
        {
        }

        public SalesTransactions(MySqlConnection Connection, MySqlTransaction Transaction) 
            : base(Connection, Transaction)
		{

		}

		#endregion

		private string SQLSelect(SalesTransactionsColumns clsSalesTransactionsColumns)
		{
			string stSQL = "SELECT ";

            if (clsSalesTransactionsColumns.TransactionNo) stSQL += "tblTransactions." + SalesTransactionsColumnNames.TransactionNo + ", ";
            if (clsSalesTransactionsColumns.TransactionNo) stSQL += "tblTransactions." + SalesTransactionsColumnNames.ORNo + ", ";
            if (clsSalesTransactionsColumns.BranchID) stSQL += "tblTransactions." + SalesTransactionsColumnNames.BranchID + ", ";
			if (clsSalesTransactionsColumns.BranchCode) stSQL += "" + SalesTransactionsColumnNames.BranchCode + ", ";
			if (clsSalesTransactionsColumns.PaxNo) stSQL += "" + SalesTransactionsColumnNames.PaxNo + ", ";
            if (clsSalesTransactionsColumns.CustomerID) stSQL += "" + SalesTransactionsColumnNames.ModeOfTerms + ", ";
            if (clsSalesTransactionsColumns.CustomerID) stSQL += "" + SalesTransactionsColumnNames.Terms + ", ";
            if (clsSalesTransactionsColumns.CustomerID) stSQL += "" + SalesTransactionsColumnNames.CRNo + ", ";
            if (clsSalesTransactionsColumns.CustomerID) stSQL += "" + SalesTransactionsColumnNames.RewardsCustomerID + ", ";
            if (clsSalesTransactionsColumns.CustomerID) stSQL += "" + SalesTransactionsColumnNames.RewardsCustomerName + ", ";
            if (clsSalesTransactionsColumns.CustomerID) stSQL += "" + SalesTransactionsColumnNames.CustomerID + ", ";
			if (clsSalesTransactionsColumns.CustomerName) stSQL += "" + SalesTransactionsColumnNames.CustomerName + ", ";
            if (clsSalesTransactionsColumns.CustomerGroupName) stSQL += "" + SalesTransactionsColumnNames.CustomerGroupName + ", ";
			if (clsSalesTransactionsColumns.AgentID) stSQL += "" + SalesTransactionsColumnNames.AgentID + ", ";
			if (clsSalesTransactionsColumns.AgentName) stSQL += "" + SalesTransactionsColumnNames.AgentName + ", ";
			if (clsSalesTransactionsColumns.CreatedByID) stSQL += "" + SalesTransactionsColumnNames.CreatedByID + ", ";
			if (clsSalesTransactionsColumns.CreatedByName) stSQL += "" + SalesTransactionsColumnNames.CreatedByName + ", ";
			if (clsSalesTransactionsColumns.CashierID) stSQL += "" + SalesTransactionsColumnNames.CashierID + ", ";
			if (clsSalesTransactionsColumns.CashierName) stSQL += "" + SalesTransactionsColumnNames.CashierName + ", ";
            if (clsSalesTransactionsColumns.TerminalNo) stSQL += "tblTransactions." + SalesTransactionsColumnNames.TerminalNo + ", ";
			if (clsSalesTransactionsColumns.TransactionDate) stSQL += "" + SalesTransactionsColumnNames.TransactionDate + ", ";
			if (clsSalesTransactionsColumns.DateSuspended) stSQL += "" + SalesTransactionsColumnNames.DateSuspended + ", ";
			if (clsSalesTransactionsColumns.DateResumed) stSQL += "" + SalesTransactionsColumnNames.DateResumed + ", ";
			if (clsSalesTransactionsColumns.TransactionStatus) stSQL += "" + SalesTransactionsColumnNames.TransactionStatus + ", ";
            if (clsSalesTransactionsColumns.TransactionStatus) stSQL += "" + "CASE TransactionStatus " +
																			"WHEN 0 THEN 'Open' " +
																			"WHEN 1 THEN 'Closed' " +
																			"WHEN 2 THEN 'Suspended' " +
																			"WHEN 3 THEN 'Void' " +
																			"WHEN 4 THEN 'Reprinted' " +
																			"WHEN 5 THEN 'Refund' " +
																			"WHEN 6 THEN 'NotYetApplied' " +
																			"WHEN 7 THEN 'NotYetApplied' " +
																			"WHEN 8 THEN 'DebitPayment' " +
																			"WHEN 9 THEN 'Released' " +
																			"WHEN 10 THEN 'OrderSlip' " +
																		"END 'TransactionStatusName'" + ", ";
            if (clsSalesTransactionsColumns.SubTotal) stSQL += "" + SalesTransactionsColumnNames.GrossSales + ", ";
			if (clsSalesTransactionsColumns.SubTotal) stSQL += "" + SalesTransactionsColumnNames.SubTotal + ", ";
            if (clsSalesTransactionsColumns.SubTotal) stSQL += "" + SalesTransactionsColumnNames.NetSales + ", ";
			if (clsSalesTransactionsColumns.ItemsDiscount) stSQL += "" + SalesTransactionsColumnNames.ItemsDiscount + ", ";
            if (clsSalesTransactionsColumns.ItemsDiscount) stSQL += "" + SalesTransactionsColumnNames.SNRItemsDiscount + ", ";
            if (clsSalesTransactionsColumns.ItemsDiscount) stSQL += "" + SalesTransactionsColumnNames.PWDItemsDiscount + ", ";
            if (clsSalesTransactionsColumns.ItemsDiscount) stSQL += "" + SalesTransactionsColumnNames.OtherItemsDiscount + ", ";
			if (clsSalesTransactionsColumns.Discount) stSQL += "" + SalesTransactionsColumnNames.Discount + ", ";
            if (clsSalesTransactionsColumns.Discount) stSQL += "" + SalesTransactionsColumnNames.SNRDiscount + ", ";
            if (clsSalesTransactionsColumns.Discount) stSQL += "" + SalesTransactionsColumnNames.PWDDiscount + ", ";
            if (clsSalesTransactionsColumns.Discount) stSQL += "" + SalesTransactionsColumnNames.OtherDiscount + ", ";
			if (clsSalesTransactionsColumns.DiscountCode) stSQL += "" + SalesTransactionsColumnNames.DiscountCode + ", ";
			if (clsSalesTransactionsColumns.DiscountRemarks) stSQL += "" + SalesTransactionsColumnNames.DiscountRemarks + ", ";
			if (clsSalesTransactionsColumns.TransDiscount) stSQL += "" + SalesTransactionsColumnNames.TransDiscount + ", ";
			if (clsSalesTransactionsColumns.TransDiscountType) stSQL += "" + SalesTransactionsColumnNames.TransDiscountType + ", ";
			if (clsSalesTransactionsColumns.VAT) stSQL += "" + SalesTransactionsColumnNames.VAT + ", ";
			if (clsSalesTransactionsColumns.VAT) stSQL += "" + SalesTransactionsColumnNames.VATableAmount + ", ";
            if (clsSalesTransactionsColumns.VAT) stSQL += "" + SalesTransactionsColumnNames.ZeroRatedSales + ", ";
            if (clsSalesTransactionsColumns.VAT) stSQL += "" + SalesTransactionsColumnNames.NonVATableAmount + ", ";
            if (clsSalesTransactionsColumns.VAT) stSQL += "" + SalesTransactionsColumnNames.VATExempt + ", ";
			if (clsSalesTransactionsColumns.EVAT) stSQL += "" + SalesTransactionsColumnNames.EVAT + ", ";
			if (clsSalesTransactionsColumns.EVATableAmount) stSQL += "" + SalesTransactionsColumnNames.EVATableAmount + ", ";
            if (clsSalesTransactionsColumns.NonEVATableAmount) stSQL += "" + SalesTransactionsColumnNames.NonEVATableAmount + ", ";
			if (clsSalesTransactionsColumns.LocalTax) stSQL += "" + SalesTransactionsColumnNames.LocalTax + ", ";
			if (clsSalesTransactionsColumns.AmountPaid) stSQL += "" + SalesTransactionsColumnNames.AmountPaid + ", ";
			if (clsSalesTransactionsColumns.CashPayment) stSQL += "" + SalesTransactionsColumnNames.CashPayment + ", ";
			if (clsSalesTransactionsColumns.ChequePayment) stSQL += "" + SalesTransactionsColumnNames.ChequePayment + ", ";
			if (clsSalesTransactionsColumns.CreditCardPayment) stSQL += "" + SalesTransactionsColumnNames.CreditCardPayment + ", ";
            stSQL += "IF(isConsignment=0,CreditPayment,0) 'CreditPayment', ";
            stSQL += "IF(isConsignment<>0,CreditPayment,0) 'ConsignmentPayment', ";
			if (clsSalesTransactionsColumns.DebitPayment) stSQL += "" + SalesTransactionsColumnNames.DebitPayment + ", ";
			if (clsSalesTransactionsColumns.RewardPointsPayment) stSQL += "" + SalesTransactionsColumnNames.RewardPointsPayment + ", ";
			if (clsSalesTransactionsColumns.RewardConvertedPayment) stSQL += "" + SalesTransactionsColumnNames.RewardConvertedPayment + ", ";
			if (clsSalesTransactionsColumns.BalanceAmount) stSQL += "" + SalesTransactionsColumnNames.BalanceAmount + ", ";
			if (clsSalesTransactionsColumns.ChangeAmount) stSQL += "" + SalesTransactionsColumnNames.ChangeAmount + ", ";
			if (clsSalesTransactionsColumns.DateClosed) stSQL += "" + SalesTransactionsColumnNames.DateClosed + ", ";
			if (clsSalesTransactionsColumns.PaymentType) stSQL += "" + SalesTransactionsColumnNames.PaymentType + ", ";
			if (clsSalesTransactionsColumns.WaiterID) stSQL += "" + SalesTransactionsColumnNames.WaiterID + ", ";
			if (clsSalesTransactionsColumns.WaiterName) stSQL += "" + SalesTransactionsColumnNames.WaiterName + ", ";
			if (clsSalesTransactionsColumns.Charge) stSQL += "" + SalesTransactionsColumnNames.Charge + ", ";
			if (clsSalesTransactionsColumns.ChargeAmount) stSQL += "" + SalesTransactionsColumnNames.ChargeAmount + ", ";
			if (clsSalesTransactionsColumns.ChargeCode) stSQL += "" + SalesTransactionsColumnNames.ChargeCode + ", ";
			if (clsSalesTransactionsColumns.ChargeRemarks) stSQL += "" + SalesTransactionsColumnNames.ChargeRemarks + ", ";
			if (clsSalesTransactionsColumns.CreditChargeAmount) stSQL += "" + SalesTransactionsColumnNames.CreditChargeAmount + ", ";
			if (clsSalesTransactionsColumns.OrderType) stSQL += "" + SalesTransactionsColumnNames.OrderType + ", ";
			if (clsSalesTransactionsColumns.AgentPositionName) stSQL += "" + SalesTransactionsColumnNames.AgentPositionName + ", ";
			if (clsSalesTransactionsColumns.AgentDepartmentName) stSQL += "" + SalesTransactionsColumnNames.AgentDepartmentName + ", ";

            stSQL += "TransactionType, tblTransactions.TransactionID, isConsignment, isZeroRated, " +
                     "IFNULL(ChequeNo,'') AS ChequeNo, IFNULL(ValidityDate,'" + DateTime.MinValue.ToString("yyyy-MM-dd") + "') AS ValidityDate " +
                     "FROM tblTransactions " +
                     "LEFT OUTER JOIN tblChequePayment chque ON tblTransactions.TransactionID = chque.TransactionID ";

			return stSQL;
		}

		#region Details

        public static SalesTransactionDetails SetDetails(System.Data.DataTable dt)
        {
            SalesTransactionDetails Details = new SalesTransactionDetails();

            foreach (System.Data.DataRow dr in dt.Rows)
            {
                Details.TransactionID = Int64.Parse(dr["TransactionID"].ToString());
                Details.TransactionType = (TransactionTypes)Enum.Parse(typeof(TransactionTypes), dr["TransactionType"].ToString());
                Details.TransactionNo = "" + dr["TransactionNo"].ToString();
                Details.ORNo = "" + dr["ORNo"].ToString();
                Details.BranchID = Int32.Parse(dr["BranchID"].ToString());
                Details.BranchCode = "" + dr["BranchCode"].ToString();
                Details.PaxNo = Int32.Parse(dr["PaxNo"].ToString());
                Details.ModeOfTerms = (ModeOfTerms)Enum.Parse(typeof(ModeOfTerms), dr["ModeOfTerms"].ToString());
                Details.Terms = Int32.Parse(dr["Terms"].ToString());
                Details.CRNo = Int64.Parse(dr["CRNo"].ToString());
                Details.RewardsCustomerID = Int32.Parse(dr["RewardsCustomerID"].ToString());
                Details.RewardsCustomerName = dr["RewardsCustomerName"].ToString();
                Details.CustomerID = Int32.Parse(dr["CustomerID"].ToString());
                Details.CustomerName = "" + dr["CustomerName"].ToString();
                Details.CustomerGroupName = "" + dr["CustomerGroupName"].ToString();
                Details.AgentID = Int32.Parse(dr["AgentID"].ToString());
                Details.AgentName = "" + dr["AgentName"].ToString();
                Details.CreatedByID = Int64.Parse(dr["CreatedByID"].ToString());
                Details.CreatedByName = "" + dr["CreatedByName"].ToString();
                Details.CashierID = Int64.Parse(dr["CashierID"].ToString());
                Details.CashierName = "" + dr["CashierName"].ToString();
                Details.TerminalNo = "" + dr["TerminalNo"].ToString();
                Details.TransactionDate = DateTime.Parse(dr["TransactionDate"].ToString());
                Details.DateSuspended = DateTime.Parse(dr["DateSuspended"].ToString());
                Details.DateResumed = DateTime.Parse(dr["DateResumed"].ToString());
                Details.TransactionStatus = (TransactionStatus)Enum.Parse(typeof(TransactionStatus), dr["TransactionStatus"].ToString());
                Details.GrossSales = decimal.Parse(dr["GrossSales"].ToString());
                Details.SubTotal = decimal.Parse(dr["SubTotal"].ToString());
                Details.NetSales = decimal.Parse(dr["NetSales"].ToString());
                Details.ItemsDiscount = decimal.Parse(dr["ItemsDiscount"].ToString());
                Details.SNRItemsDiscount = decimal.Parse(dr["SNRItemsDiscount"].ToString());
                Details.PWDItemsDiscount = decimal.Parse(dr["PWDItemsDiscount"].ToString());
                Details.OtherItemsDiscount = decimal.Parse(dr["OtherItemsDiscount"].ToString());
                Details.Discount = decimal.Parse(dr["Discount"].ToString());

                // Sep 14, 2014 Separate the discounts for VAT computation
                Details.SNRDiscount = decimal.Parse(dr["SNRDiscount"].ToString());
                Details.PWDDiscount = decimal.Parse(dr["PWDDiscount"].ToString());
                Details.OtherDiscount = decimal.Parse(dr["OtherDiscount"].ToString());
                // Aug 6, 2011 : Include in loading DiscountCode
                Details.DiscountCode = "" + dr["DiscountCode"].ToString();
                // Aug 6, 2011 : Include in loading DiscountRemarks
                Details.DiscountRemarks = "" + dr["DiscountRemarks"].ToString();
                Details.TransDiscount = decimal.Parse(dr["TransDiscount"].ToString());
                Details.TransDiscountType = (DiscountTypes)Enum.Parse(typeof(DiscountTypes), dr["TransDiscountType"].ToString());
                Details.VAT = decimal.Parse(dr["VAT"].ToString());
                Details.VATableAmount = decimal.Parse(dr["VATableAmount"].ToString());
                Details.ZeroRatedSales = decimal.Parse(dr["ZeroRatedSales"].ToString());
                Details.NonVATableAmount = decimal.Parse(dr["NonVATableAmount"].ToString());
                Details.VATExempt = decimal.Parse(dr["VATExempt"].ToString());
                Details.EVAT = decimal.Parse(dr["EVAT"].ToString());
                Details.EVATableAmount = decimal.Parse(dr["EVATableAmount"].ToString());
                Details.NonEVATableAmount = decimal.Parse(dr["NonEVATableAmount"].ToString());
                Details.LocalTax = decimal.Parse(dr["LocalTax"].ToString());
                Details.AmountPaid = decimal.Parse(dr["AmountPaid"].ToString());
                Details.CashPayment = decimal.Parse(dr["CashPayment"].ToString());
                Details.ChequePayment = decimal.Parse(dr["ChequePayment"].ToString());
                Details.CreditCardPayment = decimal.Parse(dr["CreditCardPayment"].ToString());
                Details.CreditPayment = decimal.Parse(dr["CreditPayment"].ToString());
                Details.DebitPayment = decimal.Parse(dr["DebitPayment"].ToString());
                Details.RewardPointsPayment = decimal.Parse(dr["RewardPointsPayment"].ToString());
                Details.RewardConvertedPayment = decimal.Parse(dr["RewardConvertedPayment"].ToString());
                Details.BalanceAmount = decimal.Parse(dr["BalanceAmount"].ToString());
                Details.ChangeAmount = decimal.Parse(dr["ChangeAmount"].ToString());
                Details.DateClosed = DateTime.Parse(dr["DateClosed"].ToString());
                Details.PaymentType = (PaymentTypes)Enum.Parse(typeof(PaymentTypes), dr["PaymentType"].ToString());
                Details.WaiterID = Int64.Parse(dr["WaiterID"].ToString());
                Details.WaiterName = "" + dr["WaiterName"].ToString();
                Details.Charge = decimal.Parse(dr["Charge"].ToString());
                Details.ChargeAmount = decimal.Parse(dr["ChargeAmount"].ToString());
                Details.ChargeCode = "" + dr["ChargeCode"].ToString();
                Details.ChargeRemarks = "" + dr["ChargeRemarks"].ToString();
                Details.ChargeType = (ChargeTypes)Enum.Parse(typeof(ChargeTypes), dr["ChargeType"].ToString());
                Details.CreditChargeAmount = decimal.Parse(dr["CreditChargeAmount"].ToString());
                Details.OrderType = (OrderTypes)Enum.Parse(typeof(OrderTypes), dr["OrderType"].ToString());
                Details.AgentPositionName = "" + dr["AgentPositionName"].ToString();
                Details.AgentDepartmentName = "" + dr["AgentDepartmentName"].ToString();
                Details.isExist = true;
                Details.isConsignment = Convert.ToBoolean(dr["isConsignment"]);
                Details.isZeroRated = Convert.ToBoolean(dr["isZeroRated"]);

                Details.ItemSold = decimal.Parse(dr["ItemSold"].ToString());
                Details.QuantitySold = decimal.Parse(dr["QuantitySold"].ToString());

                Details.TrustFund = decimal.Parse(dr["TrustFund"].ToString());

                break;
            }
            
            return Details;
        }

        public SalesTransactionDetails Details(string TransactionNo, string TerminalNo, Int32 BranchID)
        {
            try
            {
                System.Data.DataTable dt = ListAsDataTable(BranchID, TerminalNo, TransactionNo: TransactionNo );

                SalesTransactionDetails Details = SalesTransactions.SetDetails(dt);

                Details.PaymentDetails = new Payment(base.Connection, base.Transaction).Details(BranchID, TerminalNo, Details.TransactionID);

                return Details;
            }
            catch (Exception ex)
            {
                throw base.ThrowException(ex);
            }
        }

        #region Deleted
        #endregion

        public string getSuspendedTransactionNo(long CustomerID, string TerminalNo, int BranchID)
		{

			try
			{
                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandType = System.Data.CommandType.Text;

				string SQL = "SELECT DISTINCT(TransactionNo) FROM  tblTransactions " +
                             "WHERE BranchID = @BranchID AND TerminalNo = @TerminalNo AND CustomerID = @CustomerID AND (TransactionStatus = @TransactionStatusSuspended OR TransactionStatus = @TransactionStatusSuspendedOpen) LIMIT 1;";

                cmd.Parameters.AddWithValue("@BranchID", BranchID);
                cmd.Parameters.AddWithValue("@TerminalNo", TerminalNo);
                cmd.Parameters.AddWithValue("@CustomerID", CustomerID);
                cmd.Parameters.AddWithValue("@TransactionStatusSuspended", TransactionStatus.Suspended.ToString("d"));
                cmd.Parameters.AddWithValue("@TransactionStatusSuspendedOpen", TransactionStatus.SuspendedOpen.ToString("d"));

                cmd.CommandText = SQL;
                string strDataTableName = "tbl" + this.GetType().FullName.Split(new Char[] { '.' })[this.GetType().FullName.Split(new Char[] { '.' }).Length - 1]; System.Data.DataTable dt = new System.Data.DataTable(strDataTableName);
                base.MySqlDataAdapterFill(cmd, dt);

                string stRetValue = string.Empty;
                foreach (System.Data.DataRow dr in dt.Rows)
                {
                    stRetValue = dr["TransactionNo"].ToString();
                }

				return stRetValue;
			}
			catch (Exception ex)
			{
				throw base.ThrowException(ex);
			}
		}

        public long getTransactionIDByNo(string TransactionNo, string TerminalNo, int BranchID)
        {

            try
            {
                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandType = System.Data.CommandType.Text;

                string SQL = "SELECT DISTINCT(TransactionID) TransactionID FROM  tblTransactions " +
                            "WHERE TransactionNo = @TransactionNo AND TerminalNo = @TerminalNo AND BranchID = @BranchID LIMIT 1;";

                cmd.Parameters.AddWithValue("@TransactionNo", TransactionNo);
                cmd.Parameters.AddWithValue("@TerminalNo", TerminalNo);
                cmd.Parameters.AddWithValue("@BranchID", BranchID);

                cmd.CommandText = SQL;
                string strDataTableName = "tbl" + this.GetType().FullName.Split(new Char[] { '.' })[this.GetType().FullName.Split(new Char[] { '.' }).Length - 1]; System.Data.DataTable dt = new System.Data.DataTable(strDataTableName);
                base.MySqlDataAdapterFill(cmd, dt);

                Int64 iID = 0;
                foreach (System.Data.DataRow dr in dt.Rows)
                {
                    iID = Int64.Parse(dr[0].ToString());
                }

                return iID;
            }
            catch (Exception ex)
            {
                throw base.ThrowException(ex);
            }
        }

		#endregion

		#region Insert and Update

		public Int64 Insert(SalesTransactionDetails Details)
		{
			//	April 30, 2007 : Added "ChargeCode, ChargeRemarks" 
			try
			{
                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandType = System.Data.CommandType.Text;

				string SQL = "INSERT INTO tblTransactions (" +
                                "TransactionType, " +
								"TransactionNo, " +
								"BranchID, " +
								"BranchCode, " +
                                "RewardsCustomerID, " +
                                "RewardsCustomerName, " +
								"CustomerID, " +
								"CustomerName, " +
                                "CustomerGroupName, " +
                                "ModeOfTerms, " +
                                "Terms, " +
								"AgentID, " +
								"AgentName, " +
								"CreatedByID, " +
								"CreatedByName, " +
								"CashierID, " +
								"CashierName, " +
								"TerminalNo, " +
								"TransactionDate, " +
                                "DateSuspended, " +
								"TransactionStatus," +
								"DiscountCode, " +
								"DiscountRemarks, " +
								"WaiterID, " +
								"WaiterName," +
								"ChargeCode, ChargeRemarks,OrderType, " +
								"AgentPositionName, AgentDepartmentName,DataSource, " +
                                "ContactCheckInDate," +
                                "CreatedOn, LastModified " +
							")VALUES(" +
                                "@TransactionType, " +
								"@TransactionNo, " +
								"@BranchID, " +
								"@BranchCode, " +
								"@RewardsCustomerID, " +
                                "@RewardsCustomerName, " +
                                "@CustomerID, " +
								"@CustomerName, " +
                                "@CustomerGroupName, " +
                                "@ModeOfTerms, " +
                                "@Terms, " +
								"@AgentID, " +
								"@AgentName, " +
								"@CreatedByID, " +
								"@CreatedByName, " +
								"@CashierID, " +
								"@CashierName, " +
								"@TerminalNo, " +
								"@TransactionDate, " +
								"@DateSuspended, " +
								"@TransactionStatus," +
								"@DiscCode, " +
								"@DiscRemarks, " +
								"@WaiterID, " +
								"@WaiterName," +
								"@ChargeCode, @ChargeRemarks,@OrderType," +
                                "@AgentPositionName, @AgentDepartmentName, @DataSource," +
                                "@ContactCheckInDate, NOW(), NOW());";

				cmd.Parameters.AddWithValue("TransactionType", Details.TransactionType.ToString("d"));
				cmd.Parameters.AddWithValue("TransactionNo", Details.TransactionNo);
				cmd.Parameters.AddWithValue("BranchID", Details.BranchID);
				cmd.Parameters.AddWithValue("BranchCode", Details.BranchCode);
				cmd.Parameters.AddWithValue("CustomerID", Details.CustomerID);
                cmd.Parameters.AddWithValue("RewardsCustomerID", Details.RewardsCustomerID);
                cmd.Parameters.AddWithValue("RewardsCustomerName", Details.RewardsCustomerName);
				cmd.Parameters.AddWithValue("CustomerName", Details.CustomerName);
                cmd.Parameters.AddWithValue("CustomerGroupName", Details.CustomerGroupName);
                cmd.Parameters.AddWithValue("ModeOfTerms", Details.CustomerDetails.ModeOfTerms.ToString("d"));
                cmd.Parameters.AddWithValue("Terms", Details.CustomerDetails.Terms);
				cmd.Parameters.AddWithValue("AgentID", Details.AgentID);
				cmd.Parameters.AddWithValue("AgentName", Details.AgentName);
				cmd.Parameters.AddWithValue("CreatedByID", Details.CreatedByID);
				cmd.Parameters.AddWithValue("CreatedByName", Details.CreatedByName);
				cmd.Parameters.AddWithValue("CashierID", Details.CashierID);
				cmd.Parameters.AddWithValue("CashierName", Details.CashierName);
				cmd.Parameters.AddWithValue("TerminalNo", Details.TerminalNo);
				cmd.Parameters.AddWithValue("TransactionDate", Details.TransactionDate.ToString("yyyy-MM-dd HH:mm:ss"));
				cmd.Parameters.AddWithValue("DateSuspended", Details.DateSuspended.ToString("yyyy-MM-dd HH:mm:ss"));
				cmd.Parameters.AddWithValue("TransactionStatus", Details.TransactionStatus.ToString("d"));
				cmd.Parameters.AddWithValue("DiscCode", Details.DiscountCode);
				if (Details.DiscountRemarks == null) Details.DiscountRemarks = ""; cmd.Parameters.AddWithValue("DiscRemarks", Details.DiscountRemarks);
				cmd.Parameters.AddWithValue("WaiterID", Details.WaiterID);
				cmd.Parameters.AddWithValue("WaiterName", Details.WaiterName);
				cmd.Parameters.AddWithValue("ChargeCode", Details.ChargeCode);
				if (Details.ChargeRemarks == null) Details.ChargeRemarks = ""; cmd.Parameters.AddWithValue("ChargeRemarks", Details.ChargeRemarks);
				cmd.Parameters.AddWithValue("OrderType", Details.OrderType.ToString("d"));
				cmd.Parameters.AddWithValue("AgentPositionName", Details.AgentPositionName);
				cmd.Parameters.AddWithValue("AgentDepartmentName", Details.AgentDepartmentName);
                cmd.Parameters.AddWithValue("DataSource", Details.DataSource);
                cmd.Parameters.AddWithValue("ContactCheckInDate", Details.CustomerDetails.LastCheckInDate);

                cmd.CommandText = SQL;
				base.ExecuteNonQuery(cmd);

                Int64 intID = Int64.Parse(base.getLAST_INSERT_ID(this));

                SQL = "UPDATE tblTransactions SET SyncID = TransactionID WHERE TransactionID = @TransactionID AND SyncID = 0;";

                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("TransactionID", intID);

                cmd.CommandText = SQL;
                base.ExecuteNonQuery(cmd);

                return intID;
			}
			catch (Exception ex)
			{
				throw base.ThrowException(ex);
			}
		}
        public void UpdateRewardsContactUpdate(Int32 BranchID, string TerminalNo, Int64 TransactionID, Int64 RewardsCustomerID, string RewardsCustomerName)
        {
            try
            {
                string SQL = "CALL procTransactionRewardsContactUpdate(@BranchID, @TerminalNo, @TransactionID, @RewardsCustomerID, @RewardsCustomerName);";

                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandText = SQL;

                cmd.Parameters.AddWithValue("@BranchID", BranchID);
                cmd.Parameters.AddWithValue("@TerminalNo", TerminalNo);
                cmd.Parameters.AddWithValue("@TransactionID", TransactionID);
                cmd.Parameters.AddWithValue("@RewardsCustomerID", RewardsCustomerID);
                cmd.Parameters.AddWithValue("@RewardsCustomerName", RewardsCustomerName);

                base.ExecuteNonQuery(cmd);
            }
            catch (Exception ex)
            {
                throw base.ThrowException(ex);
            }
        }
		public void UpdateContact(Int64 TransactionID, DateTime TransactionDate, ContactDetails details)
		{
			try
			{
                string SQL = "CALL procTransactionContactUpdate(@TransactionID, @CustomerID, @CustomerName, @CustomerGroupName, @ModeOfTerms, @Terms);";

				MySqlCommand cmd = new MySqlCommand();
				cmd.CommandType = System.Data.CommandType.Text;
				cmd.CommandText = SQL;

				cmd.Parameters.AddWithValue("@TransactionID", TransactionID);
				cmd.Parameters.AddWithValue("@CustomerID", details.ContactID);
				cmd.Parameters.AddWithValue("@CustomerName", details.ContactName);
                cmd.Parameters.AddWithValue("@CustomerGroupName", details.ContactGroupName);
                cmd.Parameters.AddWithValue("@ModeOfTerms", details.ModeOfTerms.ToString("d"));
                cmd.Parameters.AddWithValue("@Terms", details.Terms);

				base.ExecuteNonQuery(cmd);
			}
			catch (Exception ex)
			{
				throw base.ThrowException(ex);
			}
		}
		public void UpdateWaiter(Int64 TransactionID, DateTime TransactionDate, long WaiterID, string WaiterName)
		{
			try
			{
				string SQL = "CALL procTransactionWaiterUpdate(@TransactionID, @WaiterID, @WaiterName);";

				MySqlCommand cmd = new MySqlCommand();
				cmd.CommandType = System.Data.CommandType.Text;
				cmd.CommandText = SQL;

				cmd.Parameters.AddWithValue("@TransactionID", TransactionID);
				cmd.Parameters.AddWithValue("@WaiterID", WaiterID);
				cmd.Parameters.AddWithValue("@WaiterName", WaiterName);

				base.ExecuteNonQuery(cmd);
			}
			catch (Exception ex)
			{
				throw base.ThrowException(ex);
			}
		}
		public void UpdateOrderType(Int64 TransactionID, DateTime TransactionDate, OrderTypes pvtOrderType)
		{
			try
			{
				string SQL = "CALL procTransactionOrderTypeUpdate(@TransactionID, @OrderType);";

				MySqlCommand cmd = new MySqlCommand();
				cmd.CommandType = System.Data.CommandType.Text;
				cmd.CommandText = SQL;

				cmd.Parameters.AddWithValue("@TransactionID", TransactionID);
				cmd.Parameters.AddWithValue("@OrderType", pvtOrderType.ToString("d"));

				base.ExecuteNonQuery(cmd);
			}
			catch (Exception ex)
			{
				throw base.ThrowException(ex);
			}
		}
		public void UpdateAgent(Int64 TransactionID, DateTime TransactionDate, ContactDetails details)
		{
			try
			{
				string SQL = "CALL procTransactionAgentUpdate(@TransactionID, @AgentID, @AgentName, @AgentPositionName, @AgentDepartmentName);";

				MySqlCommand cmd = new MySqlCommand();
				cmd.CommandType = System.Data.CommandType.Text;
				cmd.CommandText = SQL;

				cmd.Parameters.AddWithValue("@TransactionID", TransactionID);
				cmd.Parameters.AddWithValue("@AgentID", details.ContactID);
				cmd.Parameters.AddWithValue("@AgentName", details.ContactName);
				cmd.Parameters.AddWithValue("@AgentPositionName", details.PositionName);
				cmd.Parameters.AddWithValue("@AgentDepartmentName", details.DepartmentName);

				base.ExecuteNonQuery(cmd);
			}
			catch (Exception ex)
			{
				throw base.ThrowException(ex);
			}
		}

        public void UpdateisConsignment(Int64 TransactionID, bool isConsignment)
        {
            try
            {
                string SQL = "CALL procTransactionIsConsignmentUpdate(@TransactionID, @isConsignment);";

                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandText = SQL;

                cmd.Parameters.AddWithValue("@TransactionID", TransactionID);
                cmd.Parameters.AddWithValue("@isConsignment", isConsignment);

                base.ExecuteNonQuery(cmd);
            }
            catch (Exception ex)
            {
                throw base.ThrowException(ex);
            }
        }

        public void UpdateisZeroRated(Int64 TransactionID, bool isZeroRated)
        {
            try
            {
                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandType = System.Data.CommandType.Text;

                string SQL = "CALL procTransactionisZeroRatedUpdate(@TransactionID, @isZeroRated);";

                cmd.Parameters.AddWithValue("@TransactionID", TransactionID);
                cmd.Parameters.AddWithValue("@isZeroRated", isZeroRated);

                cmd.CommandText = SQL;
                base.ExecuteNonQuery(cmd);
            }
            catch (Exception ex)
            {
                throw base.ThrowException(ex);
            }
        }

        public void UpdateTransactionToSuspendedOpen(Int64 TransactionID)
        {
            try
            {
                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandType = System.Data.CommandType.Text;

                string SQL = "CALL procTransactionSuspendedOpen(@TransactionID);";

                cmd.Parameters.AddWithValue("@TransactionID", TransactionID);

                cmd.CommandText = SQL;
                base.ExecuteNonQuery(cmd);
            }
            catch (Exception ex)
            {
                throw base.ThrowException(ex);
            }
        }

		#endregion

		#region Streams

		public System.Data.DataTable List(SalesTransactionsColumns clsSalesTransactionsColumns, SalesTransactionDetails clsSearchKeys, System.Data.SqlClient.SortOrder SequenceSortOrder, int Limit, string SortField, System.Data.SqlClient.SortOrder SortOrder)
		{
			try
			{
				MySqlCommand cmd = new MySqlCommand();

				string SQL = SQLSelect(clsSalesTransactionsColumns) + "WHERE 1=1 ";

				if (clsSearchKeys.BranchID != 0)
				{
					SQL += "AND tblTransactions.BranchID = @BranchID ";
					MySqlParameter prmBranchID = new MySqlParameter("@BranchID",MySqlDbType.Int32);
					prmBranchID.Value = clsSearchKeys.BranchID;
					cmd.Parameters.Add(prmBranchID);
				}
				if (clsSearchKeys.BranchCode != string.Empty && clsSearchKeys.BranchCode != null)
				{
					SQL += "AND tblTransactions.BranchCode = @BranchCode ";
					MySqlParameter prmBranchCode = new MySqlParameter("@BranchCode",MySqlDbType.String);
					prmBranchCode.Value = clsSearchKeys.BranchCode;
					cmd.Parameters.Add(prmBranchCode);
				}
				if (clsSearchKeys.TransactionID != 0)
				{
					SQL += "AND tblTransactions.TransactionID = @TransactionID ";
					MySqlParameter prmTransactionID = new MySqlParameter("@TransactionID",MySqlDbType.Int64);
					prmTransactionID.Value = clsSearchKeys.TransactionID;
					cmd.Parameters.Add(prmTransactionID);
				}
                if (!string.IsNullOrEmpty(clsSearchKeys.TransactionNo))
				{
					SQL += "AND tblTransactions.TransactionNo = @TransactionNo ";
					MySqlParameter prmTransactionNo = new MySqlParameter("@TransactionNo",MySqlDbType.String);
					prmTransactionNo.Value = clsSearchKeys.TransactionNo;
					cmd.Parameters.Add(prmTransactionNo);
				}
                if (!string.IsNullOrEmpty(clsSearchKeys.CustomerName))
				{
					SQL += "AND tblTransactions.CustomerName LIKE @CustomerName ";
                    cmd.Parameters.AddWithValue("@CustomerName", "%" + clsSearchKeys.CustomerName + "%");
				}
                if (!string.IsNullOrEmpty(clsSearchKeys.CustomerGroupName))
                {
                    SQL += "AND tblTransactions.CustomerGroupName LIKE @CustomerGroupName ";
                    cmd.Parameters.AddWithValue("@CustomerGroupName", "%" + clsSearchKeys.CustomerGroupName + "%");
                }
                if (!string.IsNullOrEmpty(clsSearchKeys.CashierName))
				{
					SQL += "AND tblTransactions.CashierName LIKE @CashierName ";
                    cmd.Parameters.AddWithValue("@CashierName", "%" + clsSearchKeys.CashierName + "%");
				}
                if (!string.IsNullOrEmpty(clsSearchKeys.AgentName))
                {
                    SQL += "AND tblTransactions.AgentName LIKE @AgentName ";
                    cmd.Parameters.AddWithValue("@AgentName", "%" + clsSearchKeys.AgentName + "%");
                }
				if (clsSearchKeys.TerminalNo != string.Empty && clsSearchKeys.TerminalNo != null)
				{
					SQL += "AND tblTransactions.TerminalNo = @TerminalNo ";
					MySqlParameter prmTerminalNo = new MySqlParameter("@TerminalNo",MySqlDbType.String);
					prmTerminalNo.Value = clsSearchKeys.TerminalNo;
					cmd.Parameters.Add(prmTerminalNo);
				}

				if (clsSearchKeys.TransactionDateFrom != DateTime.MinValue)
				{
					SQL += "AND DATE_FORMAT(TransactionDate, '%Y-%m-%d %H:%i') >= DATE_FORMAT(@TransactionDateFrom, '%Y-%m-%d %H:%i') ";
					MySqlParameter prmTransactionDateFrom = new MySqlParameter("@TransactionDateFrom",MySqlDbType.DateTime);
					prmTransactionDateFrom.Value = clsSearchKeys.TransactionDateFrom.ToString("yyyy-MM-dd HH:mm:ss");
					cmd.Parameters.Add(prmTransactionDateFrom);
				}
				if (clsSearchKeys.TransactionDateTo != DateTime.MinValue)
				{
					SQL += "AND DATE_FORMAT(TransactionDate, '%Y-%m-%d %H:%i') <= DATE_FORMAT(@TransactionDateTo, '%Y-%m-%d %H:%i') ";
					MySqlParameter prmTransactionDateTo = new MySqlParameter("@TransactionDateTo",MySqlDbType.DateTime);
					prmTransactionDateTo.Value = clsSearchKeys.TransactionDateTo.ToString("yyyy-MM-dd HH:mm:ss");
					cmd.Parameters.Add(prmTransactionDateTo);
				}
				if (clsSearchKeys.TransactionStatus != TransactionStatus.NotYetApplied)
				{
					SQL += "AND TransactionStatus = @TransactionStatus ";
					MySqlParameter prmTransactionStatus = new MySqlParameter("@TransactionStatus",MySqlDbType.Int16);
					prmTransactionStatus.Value = clsSearchKeys.TransactionStatus.ToString("d");
					cmd.Parameters.Add(prmTransactionStatus);
				}
				if (clsSearchKeys.PaymentType != PaymentTypes.NotYetAssigned)
				{
					SQL += "AND PaymentType = @PaymentType ";
					MySqlParameter prmPaymentType = new MySqlParameter("@PaymentType",MySqlDbType.Int16);
					prmPaymentType.Value = clsSearchKeys.PaymentType.ToString("d");
					cmd.Parameters.Add(prmPaymentType);
				}
                if (clsSearchKeys.isConsignmentSearch != "-1")
                {
                    SQL += "AND isConsignment = @isConsignment ";
                    cmd.Parameters.AddWithValue("@isConsignment", clsSearchKeys.isConsignment ? 1 : 0);
                }

				if (SortField != string.Empty && SortField != null)
				{
					SQL += "ORDER BY " + SortField + " ";

					if (SortOrder != System.Data.SqlClient.SortOrder.Descending)
						SQL += "ASC ";
					else
						SQL += "DESC ";
				}

				if (Limit != 0)
					SQL += "LIMIT " + Limit + " ";

				cmd.CommandType = System.Data.CommandType.Text;
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

        public System.Data.DataTable ListForPaymentDataTable(Int64 ContactID, string SortField = "TransactionNo", System.Data.SqlClient.SortOrder SortOrder = System.Data.SqlClient.SortOrder.Ascending, Int32 limit = 0, DateTime? TransactionDateFrom = null, DateTime? TransactionDateTo = null, bool boShowConsignment = true, bool boShowAROnly = true)
		{
			try
            {
                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandType = System.Data.CommandType.Text;

				string SQL = "SELECT " +
									   "a.TerminalNo, " +
                                       "a.BranchID, " +
                                       "b.CreditPaymentID, " +
                                       "a.TransactionID, " +
									   "a.TransactionNo, " +
									   "a.PaxNo, " +
                                       "a.ModeOfTerms, a.Terms, " +
                                       "CASE ModeOfTerms " +
                                       "    WHEN 0 THEN 'Days' " +
                                       "    WHEN 1 THEN 'Months' " +
                                       "    WHEN 2 THEN 'Years' " +
                                       "END ModeOfTermsCode, " +
                                       "CASE ModeOfTerms " +
                                       "    WHEN 0 THEN DATE_ADD(a.TransactionDate, INTERVAL a.Terms DAY) " +
                                       "    WHEN 1 THEN DATE_ADD(a.TransactionDate, INTERVAL a.Terms MONTH) " +
                                       "    WHEN 2 THEN DATE_ADD(a.TransactionDate, INTERVAL a.Terms YEAR) " +
                                       "END AgingDate, " +
                                       "CASE ModeOfTerms " +
                                       "    WHEN 0 THEN CONCAT(DATEDIFF(NOW(), a.TransactionDate), 'Days') " +
                                       "    WHEN 1 THEN CONCAT(DATEDIFF(NOW(), a.TransactionDate)/30, 'Months') " +
                                       "    WHEN 2 THEN CONCAT(DATEDIFF(NOW(), a.TransactionDate)/365, 'Years') " +
                                       "END AgeTerms, " +
                                       "a.CustomerID, " +
									   "a.CustomerName, " +
									   "a.TransactionDate, " +
                                       "a.GrossSales, " +
                                       "a.SubTotal + a.CreditChargeAmount AS SubTotal, " +
                                       "a.NetSales, " +
									   "a.ItemsDiscount, " +
                                       "a.SNRItemsDiscount, " +
                                       "a.PWDItemsDiscount, " +
                                       "a.OtherItemsDiscount, " +
									   "a.Discount, " +
                                       "a.SNRDiscount, " +
                                       "a.PWDDiscount, " +
                                       "a.OtherDiscount, " +
									   "a.AmountPaid - b.Amount 'AmountPaid', " +
									   "b.Amount 'Credit', " +
									   "b.AmountPaid 'CreditPaid', " +
									   "b.Amount - b.AmountPaid 'Balance', " +
                                       "b.CreditReason, b.CreditReasonID " +
								   "FROM  tblTransactions a " +
								   "INNER JOIN tblCreditPayment b ON a.BranchID = b.BranchID AND a.TerminalNo = b.TerminalNo AND a.TransactionNo = b.TransactionNo " +
                                   "WHERE b.Amount - b.AmountPaid <> 0 AND a.CustomerID = @ContactID " +
								       "AND b.ContactID = @ContactID " +
								       "AND b.Amount <> b.AmountPaid ";

                if (TransactionDateFrom.GetValueOrDefault() != DateTime.MinValue)
                {
                    SQL += "AND a.TransactionDate >= @TransactionDateFrom ";
                    cmd.Parameters.AddWithValue("@TransactionDateFrom", TransactionDateFrom.GetValueOrDefault() == DateTime.MinValue ? Constants.C_DATE_MIN_VALUE : TransactionDateFrom);
                }
                if (TransactionDateTo.GetValueOrDefault() != DateTime.MinValue)
                {
                    SQL += "AND a.TransactionDate <= @TransactionDateTo ";
                    cmd.Parameters.AddWithValue("@TransactionDateTo", TransactionDateTo.GetValueOrDefault() == DateTime.MinValue ? Constants.C_DATE_MIN_VALUE : TransactionDateTo);
                }
                if (!boShowConsignment)
                {
                    SQL += "AND a.isConsignment = @ShowConsignment ";
                    cmd.Parameters.AddWithValue("@ShowConsignment", false);
                }
                if (!boShowAROnly)
                {
                    SQL += "AND a.isConsignment = @boShowAR ";
                    cmd.Parameters.AddWithValue("@boShowAR", true);
                }

				// Added Jan 18, 2009
				// ORDER BY TransactionNo ASC
				// SO that FIFO during payment will be applied
				// FIFO - first in first out
                SQL = "SELECT TerminalNo, BranchID, CreditPaymentID, TransactionID, " +
							"TransactionNo, " +
							"PaxNo, " +
							"CustomerID, " +
							"CustomerName, " +
							"TransactionDate, " +
                            "CreditReason, CreditReasonID, " +
                            "ModeOfTerms, Terms, ModeOfTermsCode, " +
                            "AgingDate, AgeTerms, " +
							"GrossSales, " +
                            "SubTotal, " +
                            "NetSales, " +
							"ItemsDiscount, " +
                            "SNRItemsDiscount, " +
                            "PWDItemsDiscount, " +
                            "OtherItemsDiscount, " +
							"Discount, " +
                            "SNRDiscount, " +
                            "PWDDiscount, " +
                            "OtherDiscount, " +
							"AmountPaid, " +
							"Credit, " +
							"CreditPaid, " +
							"Balance " +
						"FROM (" + SQL + ") AS tblCreditPayment ";

                SQL += "ORDER BY " + (!string.IsNullOrEmpty(SortField) ? SortField : "TransactionNo") + " ";
                SQL += SortOrder == System.Data.SqlClient.SortOrder.Ascending ? "ASC " : "DESC ";
                SQL += limit == 0 ? "" : "LIMIT " + limit.ToString() + " ";

                cmd.Parameters.AddWithValue("@ContactID", ContactID);

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

        public System.Data.DataTable ListSuspendedDataTable(Int32 BranchID, string TerminalNo, Int64 CashierID = 0, bool isPacked = false, string SortField = "TransactionDate", SortOption SortOrder = SortOption.Ascending, Int32 limit = 0)
        {
            try
            {
                return ListAsDataTable(BranchID, TerminalNo, CashierID: CashierID, isPacked: isPacked, ShowSuspendedOnly: true, SortField: SortField, SortOption: SortOrder, limit: limit);
                
                //MySqlCommand cmd = new MySqlCommand();
                //cmd.CommandType = System.Data.CommandType.Text;

                //SalesTransactionsColumns clsSalesTransactionsColumns = new SalesTransactionsColumns();
                //clsSalesTransactionsColumns.TransactionNo = true;
                //clsSalesTransactionsColumns.CustomerName = true;
                //clsSalesTransactionsColumns.DateSuspended = true;

                //string SQL = SQLSelect(clsSalesTransactionsColumns) + "WHERE tblTransactions.TransactionStatus = @TransactionStatus ";
                //cmd.Parameters.AddWithValue("@TransactionStatus", TransactionStatus.Suspended.ToString("d"));

                //if (BranchID != 0)
                //{
                //    SQL += "AND tblTransactions.BranchID = @BranchID ";
                //    cmd.Parameters.AddWithValue("@BranchID", BranchID);
                //}

                //if (TerminalNo != string.Empty && TerminalNo != null)
                //{
                //    SQL += "AND tblTransactions.TerminalNo = @TerminalNo ";
                //    cmd.Parameters.AddWithValue("@TerminalNo", TerminalNo);
                //}

                //if (CashierID != 0)
                //{
                //    SQL += "AND tblTransactions.CashierID = @CashierID ";
                //    cmd.Parameters.AddWithValue("@CashierID", CashierID);
                //}
                //SQL += !isPacked ? "" : "AND Packed = 1 ";

                //SQL += "ORDER BY " + SortField + " ";
                //SQL += SortOrder == SortOption.Ascending ? "ASC " : "DESC ";
                //SQL += limit == 0 ? "" : "LIMIT " + limit.ToString() + " ";

                //cmd.CommandText = SQL;
                //string strDataTableName = "tbl" + this.GetType().FullName.Split(new Char[] { '.' })[this.GetType().FullName.Split(new Char[] { '.' }).Length - 1]; System.Data.DataTable dt = new System.Data.DataTable(strDataTableName);
                //base.MySqlDataAdapterFill(cmd, dt);

                //return dt;

            }
            catch (Exception ex)
            {
                throw base.ThrowException(ex);
            }
        }

        // deleted Sep 14, 2014
        //public System.Data.DataTable ListSuspendedDataTable(Int32 BranchID, string TerminalNo, Int64 CashierID = 0, bool isPacked = false, string SortField = "TransactionDate", SortOption SortOrder = SortOption.Ascending, Int32 limit = 0)
        //{
        //    try
        //    {
        //        MySqlCommand cmd = new MySqlCommand();
        //        cmd.CommandType = System.Data.CommandType.Text;

        //        SalesTransactionsColumns clsSalesTransactionsColumns = new SalesTransactionsColumns();
        //        clsSalesTransactionsColumns.TransactionNo = true;
        //        clsSalesTransactionsColumns.CustomerName = true;
        //        clsSalesTransactionsColumns.DateSuspended = true;

        //        string SQL = SQLSelect(clsSalesTransactionsColumns) + "WHERE tblTransactions.TransactionStatus = @TransactionStatus ";
        //        cmd.Parameters.AddWithValue("@TransactionStatus", TransactionStatus.Suspended.ToString("d"));

        //        if (BranchID != 0)
        //        {
        //            SQL += "AND tblTransactions.BranchID = @BranchID ";
        //            cmd.Parameters.AddWithValue("@BranchID", BranchID);
        //        }

        //        if (TerminalNo != string.Empty && TerminalNo != null)
        //        {
        //            SQL += "AND tblTransactions.TerminalNo = @TerminalNo ";
        //            cmd.Parameters.AddWithValue("@TerminalNo", TerminalNo);
        //        }

        //        if (CashierID != 0)
        //        {
        //            SQL += "AND tblTransactions.CashierID = @CashierID ";
        //            cmd.Parameters.AddWithValue("@CashierID", CashierID);
        //        }
        //        SQL += !isPacked ? "" : "AND Packed = 1 ";

        //        SQL += "ORDER BY " + SortField + " ";
        //        SQL += SortOrder == SortOption.Ascending ? "ASC " : "DESC ";
        //        SQL += limit == 0 ? "" : "LIMIT " + limit.ToString() + " ";

        //        cmd.CommandText = SQL;
        //        string strDataTableName = "tbl" + this.GetType().FullName.Split(new Char[] { '.' })[this.GetType().FullName.Split(new Char[] { '.' }).Length - 1]; System.Data.DataTable dt = new System.Data.DataTable(strDataTableName);
        //        base.MySqlDataAdapterFill(cmd, dt);

        //        return dt;

        //    }
        //    catch (Exception ex)
        //    {
        //        throw base.ThrowException(ex);
        //    }
        //}

        public decimal SeniorCitizenDiscounts(Int32 BranchID, string TerminalNo, string TransactionNoFrom, string TransactionNoTo, out long DiscountCount)
		{
			try
			{
                DiscountCount = 0;
                decimal DiscountAmount = 0;
                getDiscountByTerminal("SNR", BranchID, TerminalNo, 0, TransactionNoFrom, TransactionNoTo, out DiscountCount, out DiscountAmount);

                return DiscountAmount;
			}
			catch (Exception ex)
			{
				throw base.ThrowException(ex);
			}
		}
        public decimal PersonWithDisabilityDiscounts(Int32 BranchID, string TerminalNo, string TransactionNoFrom, string TransactionNoTo, out long DiscountCount)
        {
            try
            {
                DiscountCount = 0;
                decimal DiscountAmount = 0;
                getDiscountByTerminal("PWD", BranchID, TerminalNo, 0, TransactionNoFrom, TransactionNoTo, out DiscountCount, out DiscountAmount);

                return DiscountAmount;
            }
            catch (Exception ex)
            {
                throw base.ThrowException(ex);
            }
        }
        public System.Data.DataTable Discounts(Int32 BranchID, string TerminalNo, string TransactionNoFrom, string TransactionNoTo, long CashierID = 0)
		{
			try
			{
                Int64 DiscountCount = 0;
                decimal DiscountAmount = 0;
                return getDiscountByTerminal(string.Empty, BranchID, TerminalNo, CashierID, TransactionNoFrom, TransactionNoTo, out DiscountCount, out DiscountAmount);
			}
			catch (Exception ex)
			{
				throw base.ThrowException(ex);
			}
		}

        /// <summary>
        /// Filter Discount Type are:
        ///    string.empty = no filter
        ///    PWD = PersonWithDisability
        ///    SNR = SeniorCitizen
        /// </summary>
        /// <param name="DiscountType"></param>
        /// <param name="BranchID"></param>
        /// <param name="TerminalNo"></param>
        /// <param name="CashierID"></param>
        /// <param name="TransactionNoFrom"></param>
        /// <param name="TransactionNoTo"></param>
        /// <param name="DiscountCount"></param>
        /// <returns></returns>
        private System.Data.DataTable getDiscountByTerminal(string FilterDiscountType, Int32 BranchID, string TerminalNo, long CashierID, string TransactionNoFrom, string TransactionNoTo, out long DiscountCount, out decimal DiscountAmount)
        {
            try
            {
                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandType = System.Data.CommandType.Text;

                string SQL = "CALL procGenerateDiscountByTerminalNo(@SessionID, @BranchID, @TerminalNo, @CashierID, @TransactionNoFrom, @TransactionNoTo);";

                Int32 intSessionID = new Random().Next(1234567, 99999999);
                cmd.Parameters.AddWithValue("SessionID", intSessionID);
                cmd.Parameters.AddWithValue("BranchID", BranchID);
                cmd.Parameters.AddWithValue("TerminalNo", TerminalNo);
                cmd.Parameters.AddWithValue("CashierID", CashierID);
                cmd.Parameters.AddWithValue("TransactionNoFrom", TransactionNoFrom);
                cmd.Parameters.AddWithValue("TransactionNoTo", TransactionNoTo);

                cmd.CommandText = SQL;
                base.ExecuteNonQuery(cmd);

                SQL = "SELECT DiscountCode, DiscountCount, Discount FROM tblDiscountHistory WHERE SessionID = @SessionID ";
                switch (FilterDiscountType)
                {
                    case "PWD":
                        SQL += "AND DiscountCode = (SELECT PWDDiscountCode FROM tblTerminal WHERE BranchID = @BranchID AND TerminalNo = @TerminalNo) ";
                        break;
                    case "SNR":
                        SQL += "AND DiscountCode = (SELECT SeniorCitizenDiscountCode FROM tblTerminal WHERE BranchID = @BranchID AND TerminalNo = @TerminalNo) ";
                        break;
                }
                SQL += "ORDER BY DiscountCode;";

                cmd.CommandText = SQL;
                string strDataTableName = "tbl" + this.GetType().FullName.Split(new Char[] { '.' })[this.GetType().FullName.Split(new Char[] { '.' }).Length - 1]; System.Data.DataTable dt = new System.Data.DataTable(strDataTableName);
                base.MySqlDataAdapterFill(cmd, dt);

                // Assign the discountamount and count for out values specifically needed for SNR and PWD
                DiscountAmount = 0;
                DiscountCount = 0;
                foreach(System.Data.DataRow dr in dt.Rows)
                {
                    DiscountCount += Int64.Parse(dr["DiscountCount"].ToString());
                    DiscountAmount += decimal.Parse(dr["Discount"].ToString());
                }

                SQL = "CALL procDeleteDiscountHistory(@SessionID);";

                cmd.CommandText = SQL;
                base.ExecuteNonQuery(cmd);

                return dt;
            }
            catch (Exception ex)
            {
                throw base.ThrowException(ex);
            }
        }

        public System.Data.DataTable ListAsDataTable(Int32 BranchID, string TerminalNo = "", Int64 TransactionID = 0, string TransactionNo = "", DateTime? TransactionDateFrom = null, DateTime? TransactionDateTo = null,
                                                    TransactionStatus TransactionStatus = TransactionStatus.NotYetApplied, PaymentTypes PaymentType = PaymentTypes.NotYetAssigned, bool isConsignment = false, bool isPacked = false,
                                                    string CustomerName = "", string CustomerGroupName = "", Int64 CashierID = 0, string CashierName = "", string AgentName = "",
                                                    bool WithTF = false, bool ShowSuspendedOnly = false, string SortField = "", SortOption SortOption = SortOption.Ascending, Int32 limit = 0)
        {
            try
            {
                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandType = System.Data.CommandType.Text;

                string SQL = "CALL procTransactionsSelect(@BranchID, @TerminalNo, @TransactionID, @TransactionNo, @TransactionDateFrom, " +
                                                         "@TransactionDateTo, @TransactionStatus, @PaymentType, @isConsignment, @isPacked, @CustomerName, " +
                                                         "@CustomerGroupName, @CashierID, @CashierName, @AgentName, @WithTF, @ShowSuspended, " +
                                                         "@SortField, @SortOption, @Limit);";

                cmd.Parameters.AddWithValue("BranchID", BranchID);
                cmd.Parameters.AddWithValue("TerminalNo", TerminalNo);
                cmd.Parameters.AddWithValue("TransactionID", TransactionID);
                cmd.Parameters.AddWithValue("TransactionNo", TransactionNo);
                cmd.Parameters.AddWithValue("TransactionDateFrom", TransactionDateFrom.GetValueOrDefault() == DateTime.MinValue ? Constants.C_DATE_MIN_VALUE : TransactionDateFrom);
                cmd.Parameters.AddWithValue("TransactionDateTo", TransactionDateTo.GetValueOrDefault() == DateTime.MinValue ? Constants.C_DATE_MIN_VALUE : TransactionDateTo);
                cmd.Parameters.AddWithValue("TransactionStatus", TransactionStatus.ToString("d"));
                cmd.Parameters.AddWithValue("PaymentType", PaymentType.ToString("d"));
                cmd.Parameters.AddWithValue("isConsignment", isConsignment);
                cmd.Parameters.AddWithValue("isPacked", isPacked);
                cmd.Parameters.AddWithValue("CustomerName", CustomerName);
                cmd.Parameters.AddWithValue("CustomerGroupName", CustomerGroupName);
                cmd.Parameters.AddWithValue("CashierID", CashierID);
                cmd.Parameters.AddWithValue("CashierName", CashierName);
                cmd.Parameters.AddWithValue("AgentName", AgentName);
                cmd.Parameters.AddWithValue("WithTF", WithTF);
                cmd.Parameters.AddWithValue("ShowSuspended", ShowSuspendedOnly);
                cmd.Parameters.AddWithValue("SortField", SortField);
                cmd.Parameters.AddWithValue("SortOption", SortOption==SortOption.Ascending ? "ASC" : "DESC");
                cmd.Parameters.AddWithValue("Limit", limit);

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

        public System.Data.DataTable ListAsDataTable(SalesTransactionDetails clsSearchKey, bool WithTF = false, string SortField = "", SortOption SortOption = SortOption.Ascending, Int32 limit = 0)
        {
            try
            {
                return ListAsDataTable(clsSearchKey.BranchID, clsSearchKey.TerminalNo, clsSearchKey.TransactionID, clsSearchKey.TransactionNo, clsSearchKey.TransactionDateFrom, clsSearchKey.TransactionDateTo, clsSearchKey.TransactionStatus, clsSearchKey.PaymentType, clsSearchKey.isConsignment, false, clsSearchKey.CustomerName, clsSearchKey.CustomerGroupName, clsSearchKey.CashierID, clsSearchKey.CashierName, clsSearchKey.AgentName, WithTF, false, SortField, SortOption, limit); ;
            }
            catch (Exception ex)
            {
                throw base.ThrowException(ex);
            }
        }

		
        #endregion

		#region Public Modifiers

        public void DeleteByDataSource(string DataSource)
        {
            try
            {
                string SQL = "CALL procTransactionDeleteByDataSource(@DataSource);";

                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandText = SQL;

                cmd.Parameters.AddWithValue("@DataSource", DataSource);

                base.ExecuteNonQuery(cmd);
            }

            catch (Exception ex)
            {
                throw base.ThrowException(ex);
            }
        }

        public void UpdateCRNo(Int32 BranchID, string TerminalNo, Int64 TransactionID, Int64 CRNo)
        {
            try
            {
                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandType = System.Data.CommandType.Text;

                string SQL = "UPDATE tblTransactions SET " +
                                "CRNo =	@CRNo " +
                            "WHERE BranchID = @BranchID AND TerminalNo = @TerminalNo " +
                                "AND TransactionID	=	@TransactionID ";

                cmd.Parameters.AddWithValue("CRNo", CRNo);
                cmd.Parameters.AddWithValue("BranchID", BranchID);
                cmd.Parameters.AddWithValue("TerminalNo", TerminalNo);
                cmd.Parameters.AddWithValue("TransactionID", TransactionID);

                cmd.CommandText = SQL;
                base.ExecuteNonQuery(cmd);
            }
            catch (Exception ex)
            {
                throw base.ThrowException(ex);
            }
        }

		public void UpdateCreditChargeAmount(Int32 BranchID, string TerminalNo, Int64 TransactionID, decimal CreditChargeAmount)
		{
			try
			{
                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandType = System.Data.CommandType.Text;

                string SQL = "UPDATE tblTransactions SET " +
                                "SubTotal			=	SubTotal + @CreditChargeAmount, " +
                                "CreditChargeAmount =	@CreditChargeAmount " +
                            "WHERE BranchID = @BranchID AND TerminalNo = @TerminalNo " +
                                "AND TransactionID	=	@TransactionID ";

                cmd.Parameters.AddWithValue("CreditChargeAmount", CreditChargeAmount);
                cmd.Parameters.AddWithValue("BranchID", BranchID);
                cmd.Parameters.AddWithValue("TerminalNo", TerminalNo);
                cmd.Parameters.AddWithValue("TransactionID", TransactionID);

                cmd.CommandText = SQL;
				base.ExecuteNonQuery(cmd);
			}
			catch (Exception ex)
			{
				throw base.ThrowException(ex);
			}
		}
        public void UpdateSubTotal(Int64 TransactionID, decimal ItemSold, decimal QuantitySold, decimal GrossSales, decimal SubTotal, decimal NetSales, decimal ItemsDiscount, decimal SNRItemsDiscount, decimal PWDItemsDiscount, decimal OtherItemsDiscount, decimal Discount, decimal SNRDiscount, decimal PWDDiscount, decimal OtherDiscount, decimal TransDiscount, DiscountTypes TransDiscountType, decimal VAT, decimal VATableAmount, decimal ZeroRatedSales, decimal NonVATableAmount, decimal VATExempt, decimal EVAT, decimal EVATableAmount, decimal NonEVATableAmount, decimal LocalTax, string DiscountCode, string DiscountRemarks, decimal Charge, decimal ChargeAmount, string ChargeCode, string ChargeRemarks, ChargeTypes ChargeType)
		{
            // Sep 4, 2014 : Added VATableAmount, ZeroRatedSales, NonVATableAmount, VATExempt as per requirement of BIR
            try
            {
                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandType = System.Data.CommandType.Text;

			    string SQL = "UPDATE tblTransactions SET " +
							    "TransactionStatus	=	@TransactionStatus, " +
                                "ItemSold		    =	@ItemSold, " +
                                "QuantitySold	    =	@QuantitySold, " +
                                "GrossSales		    =	@GrossSales, " +
                                "SubTotal			=	@SubTotal, " +
                                "NetSales			=	@NetSales, " +
							    "ItemsDiscount		=	@ItemsDiscount, " +
                                "SNRItemsDiscount	=	@SNRItemsDiscount, " +
                                "PWDItemsDiscount	=	@PWDItemsDiscount, " +
                                "OtherItemsDiscount =	@OtherItemsDiscount, " +
							    "Discount			=	@Discount, " +
                                "SNRDiscount		=	@SNRDiscount, " +
                                "PWDDiscount		=	@PWDDiscount, " +
                                "OtherDiscount		=	@OtherDiscount, " +
							    "TransDiscount		=	@TransDiscount, " +
							    "TransDiscountType	=	@TransDiscType, " +
							    "VAT				=	@VAT, " +
							    "VATableAmount		=	@VATableAmount, " +
                                "ZeroRatedSales     =   @ZeroRatedSales, " +
                                "NonVATableAmount	=	@NonVATableAmount, " +
                                "VATExempt		    =	@VATExempt, " +
							    "EVAT				=	@EVAT, " +
							    "EVATableAmount		=	@EVATableAmount, " +
                                "NonEVATableAmount	=	@NonEVATableAmount, " +
							    "LocalTax			=	@LocalTax, " +
							    "DateClosed			=	NOW(), " +
							    "DiscountCode		=	@DiscCode,  " +
							    "DiscountRemarks	=	@DiscRemarks, " +
							    "Charge				=	@Charge, " +
							    "ChargeAmount		=	@ChargeAmount, " +
							    "ChargeCode			=	@ChargeCode, " +
							    "ChargeRemarks		=	@ChargeRemarks, " +
                                "ChargeType		    =	@ChargeType, " +
                                "LastModified		=	NOW() " +
						    "WHERE TransactionID	=	@TransactionID;";


                cmd.Parameters.AddWithValue("@ItemSold", ItemSold);
                cmd.Parameters.AddWithValue("@QuantitySold", QuantitySold);
                cmd.Parameters.AddWithValue("@GrossSales", GrossSales);
                cmd.Parameters.AddWithValue("@SubTotal", SubTotal);
                cmd.Parameters.AddWithValue("@NetSales", NetSales);
                cmd.Parameters.AddWithValue("@ItemsDiscount", ItemsDiscount);
                cmd.Parameters.AddWithValue("@SNRItemsDiscount", SNRItemsDiscount);
                cmd.Parameters.AddWithValue("@PWDItemsDiscount", PWDItemsDiscount);
                cmd.Parameters.AddWithValue("@OtherItemsDiscount", OtherItemsDiscount);
                cmd.Parameters.AddWithValue("@Discount", Discount);
                cmd.Parameters.AddWithValue("@SNRDiscount", SNRDiscount);
                cmd.Parameters.AddWithValue("@PWDDiscount", PWDDiscount);
                cmd.Parameters.AddWithValue("@OtherDiscount", OtherDiscount);
                cmd.Parameters.AddWithValue("@TransDiscount", TransDiscount);
                cmd.Parameters.AddWithValue("@TransDiscType", TransDiscountType.ToString("d"));
                cmd.Parameters.AddWithValue("@VAT", VAT);
                cmd.Parameters.AddWithValue("@VATableAmount", VATableAmount);
                cmd.Parameters.AddWithValue("@ZeroRatedSales", ZeroRatedSales);
                cmd.Parameters.AddWithValue("@NonVATableAmount", NonVATableAmount);
                cmd.Parameters.AddWithValue("@VATExempt", VATExempt);
                cmd.Parameters.AddWithValue("@EVAT", EVAT);
                cmd.Parameters.AddWithValue("@EVATableAmount", EVATableAmount);
                cmd.Parameters.AddWithValue("@NonEVATableAmount", NonEVATableAmount);
                cmd.Parameters.AddWithValue("@LocalTax", LocalTax);
                cmd.Parameters.AddWithValue("@TransactionStatus", TransactionStatus.SuspendedOpen.ToString("d"));
                cmd.Parameters.AddWithValue("@DiscCode", DiscountCode);
                cmd.Parameters.AddWithValue("@DiscRemarks", DiscountRemarks);
                cmd.Parameters.AddWithValue("@Charge", Charge);
                cmd.Parameters.AddWithValue("@ChargeAmount", ChargeAmount);
                cmd.Parameters.AddWithValue("@ChargeCode", ChargeCode);
                cmd.Parameters.AddWithValue("@ChargeRemarks", ChargeRemarks);
                cmd.Parameters.AddWithValue("@ChargeType", ChargeType);
                cmd.Parameters.AddWithValue("@TransactionID", TransactionID);

                cmd.CommandText = SQL;
                base.ExecuteNonQuery(cmd);
            }
            catch (Exception ex)
            {
                throw base.ThrowException(ex);
            }
		}
		public void Suspend(Int64 TransactionID, decimal GrossSales, decimal SubTotal, decimal NetSales)
		{
			try
			{
                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandType = System.Data.CommandType.Text;
				
				string SQL = "UPDATE tblTransactions SET " +
								"TransactionStatus	=	@TransactionStatus, " +
                                "GrossSales		    =	@GrossSales, " +
								"SubTotal			=	@SubTotal, " +
                                "NetSales			=	@NetSales, " +
								"DateSuspended		=	NOW(), " +
                                "LastModified	    =	NOW() " +
							"WHERE TransactionID		=	@TransactionID;";

                cmd.Parameters.AddWithValue("@GrossSales", GrossSales);
                cmd.Parameters.AddWithValue("@SubTotal", SubTotal);
                cmd.Parameters.AddWithValue("@NetSales", NetSales);
                cmd.Parameters.AddWithValue("@TransactionStatus", TransactionStatus.Suspended.ToString("d"));
                cmd.Parameters.AddWithValue("@TransactionID", TransactionID);

                cmd.CommandText = SQL;
				base.ExecuteNonQuery(cmd);
			}
			catch (Exception ex)
			{
				throw base.ThrowException(ex);
			}
		}
		public void Suspend(Int64 TransactionID, decimal GrossSales, decimal SubTotal, decimal NetSales, ContactDetails details)
		{
			try
			{
                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandType = System.Data.CommandType.Text;

				string SQL = "UPDATE tblTransactions SET " +
								"CustomerID			=	@CustomerID," +
								"CustomerName		=	@CustomerName, " +
                                "CustomerGroupName	=	@CustomerGroupName, " +
								"TransactionStatus	=	@TransactionStatus, " +
                                "GrossSales		    =	@GrossSales, " +
                                "SubTotal			=	@SubTotal, " +
                                "NetSales			=	@NetSales, " +
								"DateSuspended		=	NOW(), " +
                                "LastModified	    =	NOW() " +
							"WHERE TransactionID		=	@TransactionID;";

                cmd.Parameters.AddWithValue("@CustomerID", details.ContactID);
                cmd.Parameters.AddWithValue("@CustomerName", details.ContactName);
                cmd.Parameters.AddWithValue("@CustomerGroupName", details.ContactGroupName);
                cmd.Parameters.AddWithValue("@GrossSales", GrossSales);
                cmd.Parameters.AddWithValue("@SubTotal", SubTotal);
                cmd.Parameters.AddWithValue("@NetSales", NetSales);
                cmd.Parameters.AddWithValue("@TransactionStatus", TransactionStatus.Suspended.ToString("d"));
                cmd.Parameters.AddWithValue("@TransactionID", TransactionID);

                cmd.CommandText = SQL;
				base.ExecuteNonQuery(cmd);
			}
			catch (Exception ex)
			{
				throw base.ThrowException(ex);
			}
		}
		public void Resume(Int64 TransactionID)
		{
			try
			{
                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandType = System.Data.CommandType.Text;

				string SQL = "UPDATE tblTransactions SET " +
								"TransactionStatus	=	@TransactionStatus, " +
								"DateResumed        =   NOW(), " +
                                "LastModified	    =	NOW() " +
							"WHERE TransactionID = @TransactionID;";

                cmd.Parameters.AddWithValue("@TransactionStatus", TransactionStatus.SuspendedOpen.ToString("d"));
				cmd.Parameters.AddWithValue("@TransactionID", TransactionID);

                cmd.CommandText = SQL;
				base.ExecuteNonQuery(cmd);
			}

			catch (Exception ex)
			{
				throw base.ThrowException(ex);
			}
		}
        public void Close(Int64 TransactionID, string TerminalNo, string ORNo, decimal ItemSold, decimal QuantitySold, decimal GrossSales, decimal SubTotal, decimal NetSales, decimal ItemsDiscount, decimal SNRItemsDiscount, decimal PWDItemsDiscount, decimal OtherItemsDiscount, decimal Discount, decimal SNRDiscount, decimal PWDDiscount, decimal OtherDiscount, decimal TransDiscount, DiscountTypes TransDiscountType, decimal VAT, decimal VATableAmount, decimal ZeroRatedSales, decimal NonVATableAmount, decimal VATExempt, decimal EVAT, decimal EVATableAmount, decimal NonEVATableAmount, decimal LocalTax, decimal AmountPaid, decimal CashPayment, decimal ChequePayment, decimal CreditCardPayment, decimal CreditPayment, decimal DebitPayment, decimal RewardPointsPayment, decimal RewardConvertedPayment, decimal BalanceAmount, decimal ChangeAmount, PaymentTypes PaymentType, string DiscountCode, string DiscountRemarks, decimal Charge, decimal ChargeAmount, string ChargeCode, string ChargeRemarks, Int64 CashierID, string CashierName, TransactionStatus TransactionStatus = TransactionStatus.Closed)
		{
            // Sep4, 2014 : Added VATableAmount, ZeroRatedSales, NonVATableAmount, VATExempt as per requirement of BIR
			try
			{
                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandType = System.Data.CommandType.Text;

				string SQL = "UPDATE tblTransactions SET " +
                                "TerminalNo	        =	@TerminalNo, " +
                                "ORNo	            =	@ORNo, " +
								"TransactionStatus	=	@TransactionStatus, " +
                                "ItemSold		    =	@ItemSold, " +
                                "QuantitySold	    =	@QuantitySold, " +
                                "GrossSales		    =	@GrossSales, " +
                                "SubTotal			=	@SubTotal, " +
                                "NetSales			=	@NetSales, " +
								"ItemsDiscount		=	@ItemsDiscount, " +
                                "SNRItemsDiscount	=	@SNRItemsDiscount, " +
                                "PWDItemsDiscount	=	@PWDItemsDiscount, " +
                                "OtherItemsDiscount	=	@OtherItemsDiscount, " +
								"Discount			=	@Discount, " +
                                "SNRDiscount		=	@SNRDiscount, " +
                                "PWDDiscount		=	@PWDDiscount, " +
                                "OtherDiscount		=	@OtherDiscount, " +
								"TransDiscount		=	@TransDiscount, " +
								"TransDiscountType	=	@TransDiscType, " +
								"VAT				=	@VAT, " +
								"VATableAmount		=	@VATableAmount, " +
                                "ZeroRatedSales     =   @ZeroRatedSales, " +
                                "NonVATableAmount	=	@NonVATableAmount, " +
                                "VATExempt		    =	@VATExempt, " +
								"EVAT				=	@EVAT, " +
								"EVATableAmount		=	@EVATableAmount, " +
                                "NonEVATableAmount	=	@NonEVATableAmount, " +
								"LocalTax			=	@LocalTax, " +
								"AmountPaid			=	@AmountPaid, " +
								"CashPayment		=	@CashPayment, " +
								"ChequePayment		=	@ChequePayment, " +
								"CreditCardPayment	=	@CreditCardPayment, " +
								"CreditPayment		=	@CreditPayment, " +
								"DebitPayment		=	@DebitPayment, " +

								"RewardPointsPayment    = @RewardPointsPayment, " +
								"RewardConvertedPayment = @RewardConvertedPayment, " +

								"BalanceAmount		=	@BalanceAmount, " +
								"ChangeAmount		=	@ChangeAmount, " +
								"DateClosed			=	NOW(), " +
								"PaymentType		=	@PaymentType, " +
								"DiscountCode		=	@DiscCode,  " +
								"DiscountRemarks	=	@DiscRemarks, " +
								"Charge				=	@Charge, " +
								"ChargeAmount		=	@ChargeAmount, " +
								"ChargeCode			=	@ChargeCode, " +
								"ChargeRemarks		=	@ChargeRemarks, " +
								"CashierID			=	@CashierID, " +
								"CashierName		=	@CashierName, " +
                                "LastModified		=	NOW() " +
					"WHERE TransactionID	=	@TransactionID;";

                cmd.Parameters.AddWithValue("TerminalNo", TerminalNo);
                cmd.Parameters.AddWithValue("ORNo", ORNo);
                cmd.Parameters.AddWithValue("TransactionStatus", TransactionStatus.ToString("d"));
                cmd.Parameters.AddWithValue("ItemSold", ItemSold);
                cmd.Parameters.AddWithValue("QuantitySold", QuantitySold);
                cmd.Parameters.AddWithValue("GrossSales", GrossSales);
                cmd.Parameters.AddWithValue("SubTotal", SubTotal);
                cmd.Parameters.AddWithValue("NetSales", NetSales);
                cmd.Parameters.AddWithValue("ItemsDiscount", ItemsDiscount);
                cmd.Parameters.AddWithValue("SNRItemsDiscount", SNRItemsDiscount);
                cmd.Parameters.AddWithValue("PWDItemsDiscount", PWDItemsDiscount);
                cmd.Parameters.AddWithValue("OtherItemsDiscount", OtherItemsDiscount);
                cmd.Parameters.AddWithValue("Discount", Discount);
                cmd.Parameters.AddWithValue("SNRDiscount", SNRDiscount);
                cmd.Parameters.AddWithValue("PWDDiscount", PWDDiscount);
                cmd.Parameters.AddWithValue("OtherDiscount", OtherDiscount);
                cmd.Parameters.AddWithValue("TransDiscount", TransDiscount);
                cmd.Parameters.AddWithValue("TransDiscType", TransDiscountType.ToString("d"));
                cmd.Parameters.AddWithValue("VAT", VAT);
                cmd.Parameters.AddWithValue("VATableAmount", VATableAmount);
                cmd.Parameters.AddWithValue("ZeroRatedSales", ZeroRatedSales);
                cmd.Parameters.AddWithValue("NonVATableAmount", NonVATableAmount);
                cmd.Parameters.AddWithValue("VATExempt", VATExempt);
                cmd.Parameters.AddWithValue("EVAT", EVAT);
                cmd.Parameters.AddWithValue("EVATableAmount", EVATableAmount);
                cmd.Parameters.AddWithValue("NonEVATableAmount", NonEVATableAmount);
                cmd.Parameters.AddWithValue("LocalTax", LocalTax);
                cmd.Parameters.AddWithValue("AmountPaid", AmountPaid);
                cmd.Parameters.AddWithValue("CashPayment", CashPayment);
                cmd.Parameters.AddWithValue("ChequePayment", ChequePayment);
                cmd.Parameters.AddWithValue("CreditCardPayment", CreditCardPayment);
                cmd.Parameters.AddWithValue("CreditPayment", CreditPayment);
                cmd.Parameters.AddWithValue("DebitPayment", DebitPayment);
                cmd.Parameters.AddWithValue("RewardPointsPayment", RewardPointsPayment);
                cmd.Parameters.AddWithValue("RewardConvertedPayment", RewardConvertedPayment);
                cmd.Parameters.AddWithValue("BalanceAmount", BalanceAmount);
                cmd.Parameters.AddWithValue("ChangeAmount", ChangeAmount);
                cmd.Parameters.AddWithValue("PaymentType", PaymentType.ToString("d"));
                cmd.Parameters.AddWithValue("DiscCode", DiscountCode);
                cmd.Parameters.AddWithValue("DiscRemarks", DiscountRemarks);
                cmd.Parameters.AddWithValue("Charge", Charge);
                cmd.Parameters.AddWithValue("ChargeAmount", ChargeAmount);
                cmd.Parameters.AddWithValue("ChargeCode", ChargeCode);
                cmd.Parameters.AddWithValue("ChargeRemarks", ChargeRemarks);
                cmd.Parameters.AddWithValue("CashierID", CashierID);
                cmd.Parameters.AddWithValue("CashierName", CashierName);
                cmd.Parameters.AddWithValue("TransactionID", TransactionID);

                cmd.CommandText = SQL;
				base.ExecuteNonQuery(cmd);
			}
			catch (Exception ex)
			{
				throw base.ThrowException(ex);
			}
		}
		public void CloseAsOrderSlip(Int64 TransactionID)
		{
			try
			{
				string SQL = "UPDATE tblTransactions SET " +
								"TransactionStatus  =   @TransactionStatus, " +
                                "LastModified		=	NOW() " +
							"WHERE TransactionID    = @TransactionID;";

				MySqlCommand cmd = new MySqlCommand();
				cmd.CommandType = System.Data.CommandType.Text;
				cmd.CommandText = SQL;

				MySqlParameter prmTransactionID = new MySqlParameter("@TransactionID",MySqlDbType.Int64);
				prmTransactionID.Value = TransactionID;
				cmd.Parameters.Add(prmTransactionID);

				MySqlParameter prmTransactionStatus = new MySqlParameter("@TransactionStatus",MySqlDbType.Int16);
				prmTransactionStatus.Value = TransactionStatus.OrderSlip.ToString("d");
				cmd.Parameters.Add(prmTransactionStatus);

				base.ExecuteNonQuery(cmd);

				SalesTransactionItems clsSalesTransactionItems = new SalesTransactionItems(base.Connection, base.Transaction);
				clsSalesTransactionItems.CloseAsOrderSlip(TransactionID);

			}
			catch (Exception ex)
			{
				throw base.ThrowException(ex);
			}
		}
        public void Void(Int64 TransactionID, decimal ItemSold, decimal QuantitySold, decimal GrossSales, decimal SubTotal, decimal NetSales, decimal ItemsDiscount, decimal SNRItemsDiscount, decimal PWDItemsDiscount, decimal OtherItemsDiscount, decimal Discount, decimal SNRDiscount, decimal PWDDiscount, decimal OtherDiscount, decimal TransDiscount, DiscountTypes TransDiscountType, decimal VAT, decimal VATableAmount, decimal ZeroRatedSales, decimal NonVATableAmount, decimal VATExempt, decimal EVAT, decimal EVATableAmount, decimal NonEVATableAmount, decimal LocalTax, decimal Charge, Int64 CashierID, string CashierName)
		{
			try
			{
                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandType = System.Data.CommandType.Text;

				string SQL = "UPDATE tblTransactions SET " +
								"TransactionStatus	=	@TransactionStatus, " +
                                "ItemSold		    =	@ItemSold, " +
                                "QuantitySold	    =	@QuantitySold, " +
                                "GrossSales		    =	@GrossSales, " +
								"SubTotal			=	@SubTotal, " +
                                "NetSales			=	@NetSales, " +
								"ItemsDiscount		=	@ItemsDiscount, " +
                                "SNRItemsDiscount	=	@SNRItemsDiscount, " +
                                "PWDItemsDiscount	=	@PWDItemsDiscount, " +
                                "OtherItemsDiscount	=	@OtherItemsDiscount, " +
								"Discount			=	@Discount, " +
                                "SNRDiscount		=	@SNRDiscount, " +
                                "PWDDiscount		=	@PWDDiscount, " +
                                "OtherDiscount		=	@OtherDiscount, " +
								"VAT				=	@VAT, " +
								"VATableAmount		=	@VATableAmount, " +
                                "ZeroRatedSales     =   @ZeroRatedSales, " +
                                "NonVATableAmount	=	@NonVATableAmount, " +
                                "VATExempt  		=	@VATExempt, " +
								"EVAT				=	@EVAT, " +
								"EVATableAmount		=	@EVATableAmount, " +
                                "NonEVATableAmount	=	@NonEVATableAmount, " +
								"LocalTax			=	@LocalTax, " +
								"DateClosed			=	NOW(), " +
								"Charge				=	@Charge, " +
								"CashierID			=	@CashierID, " +
								"CashierName		=	@CashierName, " +
                                "LastModified		=	NOW() " +
							"WHERE TransactionID	=	@TransactionID;";

                cmd.Parameters.AddWithValue("TransactionStatus", TransactionStatus.Void.ToString("d"));
                cmd.Parameters.AddWithValue("ItemSold", ItemSold);
                cmd.Parameters.AddWithValue("QuantitySold", QuantitySold);
                cmd.Parameters.AddWithValue("GrossSales", GrossSales);
                cmd.Parameters.AddWithValue("SubTotal", SubTotal);
                cmd.Parameters.AddWithValue("NetSales", NetSales);
                cmd.Parameters.AddWithValue("ItemsDiscount", ItemsDiscount);
                cmd.Parameters.AddWithValue("SNRItemsDiscount", SNRItemsDiscount);
                cmd.Parameters.AddWithValue("PWDItemsDiscount", PWDItemsDiscount);
                cmd.Parameters.AddWithValue("OtherItemsDiscount", OtherItemsDiscount);
                cmd.Parameters.AddWithValue("Discount", Discount);
                cmd.Parameters.AddWithValue("SNRDiscount", SNRDiscount);
                cmd.Parameters.AddWithValue("PWDDiscount", PWDDiscount);
                cmd.Parameters.AddWithValue("OtherDiscount", OtherDiscount);
                cmd.Parameters.AddWithValue("VAT", VAT);
                cmd.Parameters.AddWithValue("VATableAmount", VATableAmount);
                cmd.Parameters.AddWithValue("ZeroRatedSales", ZeroRatedSales);
                cmd.Parameters.AddWithValue("NonVATableAmount", NonVATableAmount);
                cmd.Parameters.AddWithValue("VATExempt", VATExempt);
                cmd.Parameters.AddWithValue("EVAT", EVAT);
                cmd.Parameters.AddWithValue("EVATableAmount", EVATableAmount);
                cmd.Parameters.AddWithValue("NonEVATableAmount", NonEVATableAmount);
                cmd.Parameters.AddWithValue("LocalTax", LocalTax);
                cmd.Parameters.AddWithValue("Charge", Charge);
                cmd.Parameters.AddWithValue("CashierID", CashierID);
                cmd.Parameters.AddWithValue("CashierName", CashierName);
                cmd.Parameters.AddWithValue("TransactionID", TransactionID);

                cmd.CommandText = SQL;
				base.ExecuteNonQuery(cmd);

				SalesTransactionItems clsSalesTransactionItems = new SalesTransactionItems(base.Connection, base.Transaction);
				clsSalesTransactionItems.VoidByTransaction(TransactionID);
			}
			catch (Exception ex)
			{
				throw base.ThrowException(ex);
			}
		}
        public void Refund(TransactionStatus RefundTransactionStatus, Int64 TransactionID, string ORNo, decimal ItemSold, decimal QuantitySold, decimal GrossSales, decimal SubTotal, decimal NetSales, decimal ItemsDiscount, decimal SNRItemsDiscount, decimal PWDItemsDiscount, decimal OtherItemsDiscount, decimal Discount, decimal SNRDiscount, decimal PWDDiscount, decimal OtherDiscount, decimal TransDiscount, DiscountTypes TransDiscountType, decimal VAT, decimal VATableAmount, decimal ZeroRatedSales, decimal NonVATableAmount, decimal VATExempt, decimal EVAT, decimal EVATableAmount, decimal NonEVATableAmount, decimal LocalTax, decimal AmountPaid, decimal CashPayment, decimal ChequePayment, decimal CreditCardPayment, decimal CreditPayment, decimal DebitPayment, decimal RewardPointsPayment, decimal RewardConvertedPayment, decimal BalanceAmount, decimal ChangeAmount, PaymentTypes PaymentType, string DiscountCode, string DiscountRemarks, decimal Charge, decimal ChargeAmount, string ChargeCode, string ChargeRemarks, Int64 CashierID, string CashierName)
		{
			try
			{
                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandType = System.Data.CommandType.Text;

				string SQL = "UPDATE tblTransactions SET " +
                                "ORNo	            =	@ORNo, " +
								"TransactionStatus	=	@TransactionStatus, " +
                                "ItemSold		    =	@ItemSold, " +
                                "QuantitySold	    =	@QuantitySold, " +
                                "GrossSales		    =	@GrossSales, " +
                                "SubTotal			=	@SubTotal, " +
                                "NetSales			=	@NetSales, " +
								"ItemsDiscount		=	@ItemsDiscount, " +
                                "SNRItemsDiscount	=	@SNRItemsDiscount, " +
                                "PWDItemsDiscount	=	@PWDItemsDiscount, " +
                                "OtherItemsDiscount	=	@OtherItemsDiscount, " +
								"Discount			=	@Discount, " +
                                "SNRDiscount		=	@SNRDiscount, " +
                                "PWDDiscount		=	@PWDDiscount, " +
                                "OtherDiscount		=	@OtherDiscount, " +
								"TransDiscount		=	@TransDiscount, " +
								"TransDiscountType	=	@TransDiscType, " +
								"VAT				=	@VAT, " +
								"VATableAmount		=	@VATableAmount, " +
                                "ZeroRatedSales     =   @ZeroRatedSales, " +
                                "NonVATableAmount   =	@NonVATableAmount, " +
                                "VATExempt  		=	@VATExempt, " +
								"EVAT				=	@EVAT, " +
								"EVATableAmount		=	@EVATableAmount, " +
                                "NonEVATableAmount	=	@NonEVATableAmount, " +
								"LocalTax			=	@LocalTax, " +
								"AmountPaid			=	@AmountPaid, " +
								"CashPayment		=	@CashPayment, " +
								"ChequePayment		=	@ChequePayment, " +
								"CreditCardPayment	=	@CreditCardPayment, " +
								"CreditPayment		=	@CreditPayment, " +
								"DebitPayment		=	@DebitPayment, " +
								"RewardPointsPayment		=	@RewardPointsPayment, " +
								"RewardConvertedPayment		=	@RewardConvertedPayment, " +
								"BalanceAmount		=	@BalanceAmount, " +
								"ChangeAmount		=	@ChangeAmount, " +
								"DateClosed			=	NOW(), " +
								"PaymentType		=	@PaymentType, " +
								"DiscountCode		=	@DiscCode,  " +
								"DiscountRemarks	=	@DiscRemarks, " +
								"Charge				=	@Charge, " +
								"ChargeAmount		=	@ChargeAmount, " +
								"ChargeCode			=	@ChargeCode, " +
								"ChargeRemarks		=	@ChargeRemarks, " +
								"CashierID			=	@CashierID, " +
								"CashierName		=	@CashierName, " +
                                "LastModified		=	NOW() " +
							"WHERE TransactionID	=	@TransactionID;";

                cmd.Parameters.AddWithValue("ORNo", ORNo);
                cmd.Parameters.AddWithValue("TransactionStatus", RefundTransactionStatus.ToString("d"));
                cmd.Parameters.AddWithValue("ItemSold", ItemSold);
                cmd.Parameters.AddWithValue("QuantitySold", QuantitySold);
                cmd.Parameters.AddWithValue("GrossSales", GrossSales);
                cmd.Parameters.AddWithValue("SubTotal", SubTotal);
                cmd.Parameters.AddWithValue("NetSales", NetSales);
                cmd.Parameters.AddWithValue("ItemsDiscount", ItemsDiscount);
                cmd.Parameters.AddWithValue("SNRItemsDiscount", SNRItemsDiscount);
                cmd.Parameters.AddWithValue("PWDItemsDiscount", PWDItemsDiscount);
                cmd.Parameters.AddWithValue("OtherItemsDiscount", OtherItemsDiscount);
                cmd.Parameters.AddWithValue("Discount", Discount);
                cmd.Parameters.AddWithValue("SNRDiscount", SNRDiscount);
                cmd.Parameters.AddWithValue("PWDDiscount", PWDDiscount);
                cmd.Parameters.AddWithValue("OtherDiscount", OtherDiscount);
                cmd.Parameters.AddWithValue("TransDiscount", TransDiscount);
                cmd.Parameters.AddWithValue("TransDiscType", TransDiscountType.ToString("d"));
                cmd.Parameters.AddWithValue("VAT", VAT);
                cmd.Parameters.AddWithValue("VATableAmount", VATableAmount);
                cmd.Parameters.AddWithValue("ZeroRatedSales", ZeroRatedSales);
                cmd.Parameters.AddWithValue("NonVATableAmount", NonVATableAmount);
                cmd.Parameters.AddWithValue("VATExempt", VATExempt);
                cmd.Parameters.AddWithValue("EVAT", EVAT);
                cmd.Parameters.AddWithValue("EVATableAmount", EVATableAmount);
                cmd.Parameters.AddWithValue("NonEVATableAmount", NonEVATableAmount);
                cmd.Parameters.AddWithValue("LocalTax", LocalTax);
                cmd.Parameters.AddWithValue("AmountPaid", AmountPaid);
                cmd.Parameters.AddWithValue("CashPayment", CashPayment);
                cmd.Parameters.AddWithValue("ChequePayment", ChequePayment);
                cmd.Parameters.AddWithValue("CreditCardPayment", CreditCardPayment);
                cmd.Parameters.AddWithValue("CreditPayment", CreditPayment);
                cmd.Parameters.AddWithValue("DebitPayment", DebitPayment);
                cmd.Parameters.AddWithValue("RewardPointsPayment", RewardPointsPayment);
                cmd.Parameters.AddWithValue("RewardConvertedPayment", RewardConvertedPayment);
                cmd.Parameters.AddWithValue("BalanceAmount", BalanceAmount);
                cmd.Parameters.AddWithValue("ChangeAmount", ChangeAmount);
                cmd.Parameters.AddWithValue("PaymentType", PaymentType.ToString("d"));
                cmd.Parameters.AddWithValue("DiscCode", string.IsNullOrEmpty(DiscountCode) ? "" : DiscountCode);
                cmd.Parameters.AddWithValue("DiscRemarks", DiscountRemarks);
                cmd.Parameters.AddWithValue("Charge", Charge);
                cmd.Parameters.AddWithValue("ChargeAmount", ChargeAmount);
                cmd.Parameters.AddWithValue("ChargeCode", ChargeCode);
                cmd.Parameters.AddWithValue("ChargeRemarks", string.IsNullOrEmpty(ChargeRemarks) ? "" : ChargeRemarks);
                cmd.Parameters.AddWithValue("CashierID", CashierID);
                cmd.Parameters.AddWithValue("CashierName", CashierName);
                cmd.Parameters.AddWithValue("TransactionID", TransactionID);

                cmd.CommandText = SQL;
				base.ExecuteNonQuery(cmd);

				SalesTransactionItems clsSalesTransactionItems = new SalesTransactionItems(base.Connection, base.Transaction);
				clsSalesTransactionItems.RefundByTransaction(TransactionID);
			}

			catch (Exception ex)
			{
				throw base.ThrowException(ex);
			}
		}
		public void Pack(Int64 TransactionID)
		{
			try
			{
				string SQL = "UPDATE tblTransactions SET " +
								"Packed	=	1 " +
							"WHERE TransactionID = @TransactionID;";

				MySqlCommand cmd = new MySqlCommand();
				cmd.CommandType = System.Data.CommandType.Text;
				cmd.CommandText = SQL;

				MySqlParameter prmTransactionID = new MySqlParameter("@TransactionID",MySqlDbType.Int64);
				prmTransactionID.Value = TransactionID;
				cmd.Parameters.Add(prmTransactionID);

				base.ExecuteNonQuery(cmd);
			}

			catch (Exception ex)
			{
				throw base.ThrowException(ex);
			}
		}
		public void UnPack(Int64 TransactionID)
		{
			try
			{
				string SQL = "UPDATE tblTransactions SET " +
								"Packed	=	0 " +
							"WHERE TransactionID = @TransactionID;";

				MySqlCommand cmd = new MySqlCommand();
				cmd.CommandType = System.Data.CommandType.Text;
				cmd.CommandText = SQL;

				MySqlParameter prmTransactionID = new MySqlParameter("@TransactionID",MySqlDbType.Int64);
				prmTransactionID.Value = TransactionID;
				cmd.Parameters.Add(prmTransactionID);

				base.ExecuteNonQuery(cmd);
			}

			catch (Exception ex)
			{
				throw base.ThrowException(ex);
			}
		}
		public void Release(Int64 TransactionID, long ReleaserID, string ReleaserName)
		{
			try
			{
				string SQL = "UPDATE tblTransactions SET " +
								"TransactionStatus  = @TransactionStatus, " +
								"ReleaserID         = @ReleaserID, " +
								"ReleaserName       = @ReleaserName, " +
								"ReleasedDate       = NOW() " +
							"WHERE TransactionID    = @TransactionID;";

				MySqlCommand cmd = new MySqlCommand();
				cmd.CommandType = System.Data.CommandType.Text;
				cmd.CommandText = SQL;

				MySqlParameter prmTransactionID = new MySqlParameter("@TransactionID",MySqlDbType.Int64);
				prmTransactionID.Value = TransactionID;
				cmd.Parameters.Add(prmTransactionID);

				MySqlParameter prmTransactionStatus = new MySqlParameter("@TransactionStatus",MySqlDbType.Int16);
				prmTransactionStatus.Value = TransactionStatus.Released.ToString("d");
				cmd.Parameters.Add(prmTransactionStatus);

				MySqlParameter prmReleaserID = new MySqlParameter("@ReleaserID",MySqlDbType.Int64);
				prmReleaserID.Value = ReleaserID;
				cmd.Parameters.Add(prmReleaserID);

				MySqlParameter prmReleaserName = new MySqlParameter("@ReleaserName",MySqlDbType.String);
				prmReleaserName.Value = ReleaserName;
				cmd.Parameters.Add(prmReleaserName);

				base.ExecuteNonQuery(cmd);
			}
			catch (Exception ex)
			{
				throw base.ThrowException(ex);
			}
		}

        public void UpdateTerminalNo(Int64 TransactionID, string TerminalNo)
        {
            try
            {
                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandType = System.Data.CommandType.Text;
                
                string SQL = "CALL procTransactionTerminalNoUpdate(@TransactionID, @TerminalNo);";

                cmd.Parameters.AddWithValue("@TransactionID", TransactionID);
                cmd.Parameters.AddWithValue("@TerminalNo", TerminalNo);

                cmd.CommandText = SQL;
                base.ExecuteNonQuery(cmd);
            }
            catch (Exception ex)
            {
                throw base.ThrowException(ex);
            }
        }

        /// <summary>
        /// This will overwrite the DateClosed. Applicable for uploaded transactions like GLA.
        /// </summary>
        /// <param name="TransactionID"></param>
        /// <param name="DateClosed"></param>
        public void UpdateDateClosed(Int64 TransactionID, DateTime DateClosed)
        {
            try
            {
                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandType = System.Data.CommandType.Text;

                string SQL = "CALL procTransactionDateClosedUpdate(@TransactionID, @DateClosed);";

                cmd.Parameters.AddWithValue("TransactionID", TransactionID);
                cmd.Parameters.AddWithValue("DateClosed", DateClosed.ToString("yyyy-MM-dd HH;mm:ss"));

                cmd.CommandText = SQL;
                base.ExecuteNonQuery(cmd);
            }

            catch (Exception ex)
            {
                throw base.ThrowException(ex);
            }
        }
        
		public void UpdatePaxNo(Int64 TransactionID, int PaxNo)
		{
			try
			{
				string SQL = "UPDATE tblTransactions SET " +
								"PaxNo	= @PaxNo " +
							"WHERE TransactionID = @TransactionID;";

				MySqlCommand cmd = new MySqlCommand();
				cmd.CommandType = System.Data.CommandType.Text;
				cmd.CommandText = SQL;

				MySqlParameter prmTransactionID = new MySqlParameter("@TransactionID",MySqlDbType.Int64);
				prmTransactionID.Value = TransactionID;
				cmd.Parameters.Add(prmTransactionID);

				MySqlParameter prmPaxNo = new MySqlParameter("@PaxNo",MySqlDbType.Int32);
				prmPaxNo.Value = PaxNo;
				cmd.Parameters.Add(prmPaxNo);

				base.ExecuteNonQuery(cmd);
			}

			catch (Exception ex)
			{
				throw base.ThrowException(ex);
			}
		}

		public Int64 AddItem(SalesTransactionDetails SalesTransactionDetails, SalesTransactionItemDetails SalesTransItemDetails)
		{
			try
			{
                UpdateSubTotal(SalesTransactionDetails.TransactionID, SalesTransactionDetails.ItemSold, SalesTransactionDetails.QuantitySold, SalesTransactionDetails.GrossSales, SalesTransactionDetails.SubTotal, SalesTransactionDetails.NetSales, SalesTransactionDetails.ItemsDiscount, SalesTransactionDetails.SNRItemsDiscount, SalesTransactionDetails.PWDItemsDiscount, SalesTransactionDetails.OtherItemsDiscount, SalesTransactionDetails.Discount, SalesTransactionDetails.SNRDiscount, SalesTransactionDetails.PWDDiscount, SalesTransactionDetails.OtherDiscount, SalesTransactionDetails.TransDiscount, SalesTransactionDetails.TransDiscountType, SalesTransactionDetails.VAT, SalesTransactionDetails.VATableAmount, SalesTransactionDetails.ZeroRatedSales, SalesTransactionDetails.NonVATableAmount, SalesTransactionDetails.VATExempt, SalesTransactionDetails.EVAT, SalesTransactionDetails.EVATableAmount, SalesTransactionDetails.NonEVATableAmount, SalesTransactionDetails.LocalTax, SalesTransactionDetails.DiscountCode, SalesTransactionDetails.DiscountRemarks, SalesTransactionDetails.Charge, SalesTransactionDetails.ChargeAmount, SalesTransactionDetails.ChargeCode, SalesTransactionDetails.ChargeRemarks, SalesTransactionDetails.ChargeType);

				SalesTransactionItems clsSalesTransactionItems = new SalesTransactionItems(base.Connection, base.Transaction);
				Int64 TransactionItemID = clsSalesTransactionItems.Insert(SalesTransItemDetails);

				return TransactionItemID;
			}

			catch (Exception ex)
			{
				throw base.ThrowException(ex);
			}
		}

        public Int64 AddItem(Int64 TransactionID, decimal ItemSold, decimal QuantitySold, decimal GrossSales, decimal SubTotal, decimal NetSales, decimal ItemsDiscount, decimal SNRItemsDiscount, decimal PWDItemsDiscount, decimal OtherItemsDiscount, decimal Discount, decimal SNRDiscount, decimal PWDDiscount, decimal OtherDiscount, decimal TransDiscount, DiscountTypes TransDiscountType, decimal VAT, decimal VATableAmount, decimal ZeroRatedSales, decimal NonVATableAmount, decimal VATExempt, decimal EVAT, decimal EVATableAmount, decimal NonEVATableAmount, decimal LocalTax, string DiscountCode, string DiscountRemarks, decimal Charge, decimal ChargeAmount, string ChargeCode, string ChargeRemarks, ChargeTypes ChargeType, SalesTransactionItemDetails SalesTransItemDetails)
		{
			try
			{
                UpdateSubTotal(TransactionID, ItemSold, QuantitySold, GrossSales, SubTotal, NetSales, ItemsDiscount, SNRItemsDiscount, PWDItemsDiscount, OtherItemsDiscount, Discount, SNRDiscount, PWDDiscount, OtherDiscount, TransDiscount, TransDiscountType, VAT, VATableAmount, ZeroRatedSales, NonVATableAmount, VATExempt, EVAT, EVATableAmount, NonEVATableAmount, LocalTax, DiscountCode, DiscountRemarks, Charge, ChargeAmount, ChargeCode, ChargeRemarks, ChargeType);

				SalesTransactionItems clsSalesTransactionItems = new SalesTransactionItems(base.Connection, base.Transaction);
				Int64 TransactionItemID = clsSalesTransactionItems.Insert(SalesTransItemDetails);

				return TransactionItemID;
			}
			catch (Exception ex)
			{
				throw base.ThrowException(ex);
			}
		}

		public TransactionStatus Status(string TransactionNo, string TerminalNo, Int32 BranchID=0)
		{
			try
			{
				TransactionStatus status = TransactionStatus.NotYetApplied;

                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandType = System.Data.CommandType.Text;

				string SQL = "SELECT TransactionStatus FROM  tblTransactions " +
                    "WHERE CAST(TransactionNo AS UNSIGNED INT) = CAST(@TransactionNo AS UNSIGNED INT) AND TerminalNo = @TerminalNo;";

                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@TransactionNo", TransactionNo);
                cmd.Parameters.AddWithValue("@TerminalNo", TerminalNo);

                cmd.CommandText = SQL;
                string strDataTableName = "tbl" + this.GetType().FullName.Split(new Char[] { '.' })[this.GetType().FullName.Split(new Char[] { '.' }).Length - 1]; System.Data.DataTable dt = new System.Data.DataTable(strDataTableName);
                base.MySqlDataAdapterFill(cmd, dt);
                foreach (System.Data.DataRow dr in dt.Rows)
                {
                    status = (TransactionStatus)Enum.Parse(typeof(TransactionStatus), dr["TransactionStatus"].ToString());
                    break;
                }
				return status;
			}
			catch (Exception ex)
			{
				throw base.ThrowException(ex);
			}
		}


		public Int32 CountSuspended(string TerminalNo, Int64 CashierID, Int32 BranchID)
		{
			try
			{
				MySqlCommand cmd = new MySqlCommand();
                cmd.CommandType = System.Data.CommandType.Text;

				string SQL = "SELECT " +
								"COUNT(TransactionID) " +
							"FROM  tblTransactions " +
                                "WHERE (TransactionStatus = @Suspended OR TransactionStatus = @SuspendedOpen) " +
								"AND TerminalNo = @TerminalNo ";
				if (CashierID != 0)
				{
					SQL += "AND CashierID = @CashierID ";
                    cmd.Parameters.AddWithValue("@CashierID", CashierID);
				}
				if (BranchID != 0)
				{
					SQL += "AND BranchID = @BranchID ";
                    cmd.Parameters.AddWithValue("@BranchID", BranchID);
				}

				SQL = "SELECT (" + SQL + ") AS TranCount";

                cmd.Parameters.AddWithValue("@Suspended", TransactionStatus.Suspended.ToString("d"));
                cmd.Parameters.AddWithValue("@SuspendedOpen", TransactionStatus.SuspendedOpen.ToString("d"));
                cmd.Parameters.AddWithValue("@TerminalNo", TerminalNo);

                cmd.CommandText = SQL;
                string strDataTableName = "tbl" + this.GetType().FullName.Split(new Char[] { '.' })[this.GetType().FullName.Split(new Char[] { '.' }).Length - 1]; System.Data.DataTable dt = new System.Data.DataTable(strDataTableName);
                base.MySqlDataAdapterFill(cmd, dt);

                Int32 iCtr = 0;
                foreach (System.Data.DataRow dr in dt.Rows)
                {
                    iCtr = Int32.Parse(dr["TranCount"].ToString());
                }

				return iCtr;
			}
			catch (Exception ex)
			{
				throw base.ThrowException(ex);
			}
		}

		public bool HasPendingTransaction(Int64 CashierID, Int32 BranchID, string TerminalNo, out string TransactionNo)
		{
			try
			{
                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandType = System.Data.CommandType.Text;

				string SQL = "SELECT TransactionNo FROM  tblTransactions " +
								"WHERE TransactionStatus = @TransactionStatus " +
								    "AND CashierID = @CashierID " +
								    "AND TerminalNo = @TerminalNo " +
                                    "AND BranchID = @BranchID " +
								"LIMIT 1";
                // "ORDER BY TransactionDate ASC LIMIT 1";

                cmd.Parameters.AddWithValue("@TransactionStatus", TransactionStatus.Open.ToString("d"));
                cmd.Parameters.AddWithValue("@CashierID", CashierID);
                cmd.Parameters.AddWithValue("@BranchID", BranchID);
                cmd.Parameters.AddWithValue("@TerminalNo", TerminalNo);

                cmd.CommandText = SQL;
                string strDataTableName = "tbl" + this.GetType().FullName.Split(new Char[] { '.' })[this.GetType().FullName.Split(new Char[] { '.' }).Length - 1]; System.Data.DataTable dt = new System.Data.DataTable(strDataTableName);
                base.MySqlDataAdapterFill(cmd, dt);

                TransactionNo = null;
                bool boRetVaue = false;
                foreach (System.Data.DataRow dr in dt.Rows)
                {
                    boRetVaue = true;
                    TransactionNo = "" + dr["TransactionNo"].ToString();
                }

				return boRetVaue;
			}
			catch (Exception ex)
			{
				throw base.ThrowException(ex);
			}
		}


        public string CreateTransactionNo(Int32 BranchID, string TerminalNo)
        {
            try
            {
                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandType = System.Data.CommandType.Text;

                string SQL = "SELECT " +
                                "EndingTransactionNo " +
                            "FROM tblTerminalReport " +
                            "WHERE TerminalNo = @TerminalNo AND BranchID = @BranchID;";
                
                cmd.Parameters.AddWithValue("BranchID", BranchID);
                cmd.Parameters.AddWithValue("TerminalNo", TerminalNo);

                cmd.CommandText = SQL;
                string strDataTableName = "tbl" + this.GetType().FullName.Split(new Char[] { '.' })[this.GetType().FullName.Split(new Char[] { '.' }).Length - 1]; System.Data.DataTable dt = new System.Data.DataTable(strDataTableName);
                base.MySqlDataAdapterFill(cmd, dt);
                
                string stRetValue = String.Empty;
                Int32 iLen = 15;
                foreach (System.Data.DataRow dr in dt.Rows)
                {
                    if (!string.IsNullOrEmpty(dr["EndingTransactionNo"].ToString()))
                    {
                        stRetValue = dr["EndingTransactionNo"].ToString();
                        iLen = stRetValue.Length;
                        stRetValue = stRetValue.PadLeft(iLen, '0');
                    }
                }

                if (string.IsNullOrEmpty(stRetValue))
                    throw new Exception("Invalid Ending Transaction No");

                string EndingTransactionNo = Convert.ToString(Convert.ToInt64(stRetValue) + 1);
                EndingTransactionNo = EndingTransactionNo.PadLeft(iLen, '0');

                SQL = "UPDATE tblTerminalReport SET EndingTransactionNo = @EndingTransactionNo " +
                      "WHERE TerminalNo = @TerminalNo AND BranchID = @BranchID;";
                
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("BranchID", BranchID);
                cmd.Parameters.AddWithValue("TerminalNo", TerminalNo);
                cmd.Parameters.AddWithValue("EndingTransactionNo", EndingTransactionNo);

                cmd.CommandText = SQL;
                base.ExecuteNonQuery(cmd);
                
                return stRetValue;
            }
            catch (Exception ex)
            {
                throw base.ThrowException(ex);
            }
        }

        public string CreateORNo(Int32 BranchID, string TerminalNo)
        {
            try
            {
                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandType = System.Data.CommandType.Text;

                string SQL = "SELECT " +
                                "EndingORNo " +
                            "FROM tblTerminalReport " +
                            "WHERE TerminalNo = @TerminalNo AND BranchID = @BranchID;";

                cmd.Parameters.AddWithValue("BranchID", BranchID);
                cmd.Parameters.AddWithValue("TerminalNo", TerminalNo);

                cmd.CommandText = SQL;
                string strDataTableName = "tbl" + this.GetType().FullName.Split(new Char[] { '.' })[this.GetType().FullName.Split(new Char[] { '.' }).Length - 1]; System.Data.DataTable dt = new System.Data.DataTable(strDataTableName);
                base.MySqlDataAdapterFill(cmd, dt);

                string stRetValue = String.Empty;
                Int32 iLen = 15;
                foreach (System.Data.DataRow dr in dt.Rows)
                {
                    if (!string.IsNullOrEmpty(dr["EndingORNo"].ToString()))
                    {
                        stRetValue = dr["EndingORNo"].ToString();
                        iLen = stRetValue.Length;
                        stRetValue = stRetValue.PadLeft(iLen, '0');
                    }
                }

                if (string.IsNullOrEmpty(stRetValue))
                    throw new Exception("Invalid Ending OR No");

                string EndingORNo = Convert.ToString(Convert.ToInt64(stRetValue) + 1);
                EndingORNo = EndingORNo.PadLeft(iLen, '0');

                // 23Mar2015 : Do an override in checking the ORNo for server based Terminals
                SQL = "UPDATE tblTerminalReport SET EndingORNo = @EndingORNo " +
                                            "WHERE ORSeriesTerminalNo = @TerminalNo AND ORSeriesBranchID = @BranchID;";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("TerminalNo", TerminalNo);
                cmd.Parameters.AddWithValue("BranchID", BranchID);
                cmd.Parameters.AddWithValue("EndingORNo", EndingORNo);

                cmd.CommandText = SQL;
                base.ExecuteNonQuery(cmd);

                return stRetValue;
            }
            catch (Exception ex)
            {
                throw base.ThrowException(ex);
            }
        }

		#endregion

		#region Transaction Items Method

		public void ReturnItem(Int64 TransactionItemID)
		{
			SalesTransactionItems clsSalesTransactionItems = new SalesTransactionItems(base.Connection, base.Transaction);
			clsSalesTransactionItems.Return(TransactionItemID);
		}
		
        public void VoidItem(Int64 TransactionItemID, DateTime TransactionDate)
		{
			SalesTransactionItems clsSalesTransactionItems = new SalesTransactionItems(base.Connection, base.Transaction);
			clsSalesTransactionItems.Void(TransactionItemID);
		}

        public void setItemAsDemo(Int64 TransactionItemID, DateTime TransactionDate)
        {
            SalesTransactionItems clsSalesTransactionItems = new SalesTransactionItems(base.Connection, base.Transaction);
            clsSalesTransactionItems.setItemAsDemo(TransactionItemID);
        }

        public void UpdateItem(Int64 TransactionID, decimal ItemSold, decimal QuantitySold, decimal GrossSales, decimal SubTotal, decimal NetSales, decimal ItemsDiscount, decimal SNRItemsDiscount, decimal PWDItemsDiscount, decimal OtherItemsDiscount, decimal Discount, decimal SNRDiscount, decimal PWDDiscount, decimal OtherDiscount, decimal TransDiscount, DiscountTypes TransDiscountType, decimal VAT, decimal VATableAmount, decimal ZeroRatedSales, decimal NonVATableAmount, decimal VATExempt, decimal EVAT, decimal EVATableAmount, decimal NonEVATableAmount, decimal LocalTax, string DiscountCode, string DiscountRemarks, decimal Charge, decimal ChargeAmount, string ChargeCode, string ChargeRemarks, ChargeTypes ChargeType, SalesTransactionItemDetails SalesTransItemDetails)
		{
            UpdateSubTotal(TransactionID, ItemSold, QuantitySold, GrossSales, SubTotal, NetSales, ItemsDiscount, SNRItemsDiscount, PWDItemsDiscount, OtherItemsDiscount, Discount, SNRDiscount, PWDDiscount, OtherDiscount, TransDiscount, TransDiscountType, VAT, VATableAmount, ZeroRatedSales, NonVATableAmount, VATExempt, EVAT, EVATableAmount, NonEVATableAmount, LocalTax, DiscountCode, DiscountRemarks, Charge, ChargeAmount, ChargeCode, ChargeRemarks, ChargeType);

            SalesTransactionItems clsSalesTransactionItems = new SalesTransactionItems(base.Connection, base.Transaction);
			clsSalesTransactionItems.Update(SalesTransItemDetails);
		}
		public void TrashItem(Int64 TransactionItemID)
		{
			SalesTransactionItems clsSalesTransactionItems = new SalesTransactionItems(base.Connection, base.Transaction);
			clsSalesTransactionItems.Trash(TransactionItemID);
		}


		#endregion

		#region Reports Method

		public System.Data.DataTable Cash_Cheque_CreditCard_Credit_Sales(SalesTransactionsColumns clsSalesTransactionsColumns, SalesTransactionDetails clsSearchKeys, System.Data.SqlClient.SortOrder SequenceSortOrder, int Limit, string SortField, System.Data.SqlClient.SortOrder SortOrder)
		{
			try
			{
				MySqlCommand cmd = new MySqlCommand();
                cmd.CommandType = System.Data.CommandType.Text;

				string SQL = SQLSelect(clsSalesTransactionsColumns) + "WHERE TransactionStatus <> 0 ";

				if (clsSearchKeys.BranchID != 0)
				{
					SQL += "AND tblTransactions.BranchID = @BranchID ";
					cmd.Parameters.AddWithValue("@BranchID", clsSearchKeys.BranchID);
				}
				if (!string.IsNullOrEmpty(clsSearchKeys.BranchCode))
				{
					SQL += "AND tblTransactions.BranchCode = @BranchCode ";
                    cmd.Parameters.AddWithValue("@BranchCode", clsSearchKeys.BranchCode);
				}
				if (clsSearchKeys.TransactionID != 0)
				{
					SQL += "AND tblTransactions.TransactionID = @TransactionID ";
                    cmd.Parameters.AddWithValue("@TransactionID", clsSearchKeys.TransactionID);
				}
                if (!string.IsNullOrEmpty(clsSearchKeys.TransactionNo))
				{
					SQL += "AND tblTransactions.TransactionNo = @TransactionNo ";
                    cmd.Parameters.AddWithValue("@TransactionNo", clsSearchKeys.TransactionNo);
				}
                if (!string.IsNullOrEmpty(clsSearchKeys.CustomerName))
				{
					SQL += "AND tblTransactions.CustomerName = @CustomerName ";
                    cmd.Parameters.AddWithValue("@CustomerName", clsSearchKeys.CustomerName);
				}
                if (!string.IsNullOrEmpty(clsSearchKeys.CashierName))
				{
					SQL += "AND tblTransactions.CashierName = @CashierName ";
                    cmd.Parameters.AddWithValue("@CashierName", clsSearchKeys.CashierName);
				}
                if (!string.IsNullOrEmpty(clsSearchKeys.TerminalNo))
				{
					SQL += "AND tblTransactions.TerminalNo = @TerminalNo ";
                    cmd.Parameters.AddWithValue("@TerminalNo", clsSearchKeys.TerminalNo);
				}

				if (clsSearchKeys.TransactionDateFrom != DateTime.MinValue)
				{
					SQL += "AND DATE_FORMAT(TransactionDate, '%Y-%m-%d %H:%i') >= DATE_FORMAT(@TransactionDateFrom, '%Y-%m-%d %H:%i') ";
                    cmd.Parameters.AddWithValue("@TransactionDateFrom", clsSearchKeys.TransactionDateFrom);
				}
				if (clsSearchKeys.TransactionDateTo != DateTime.MinValue)
				{
					SQL += "AND DATE_FORMAT(TransactionDate, '%Y-%m-%d %H:%i') <= DATE_FORMAT(@TransactionDateTo, '%Y-%m-%d %H:%i') ";
                    cmd.Parameters.AddWithValue("@TransactionDateTo", clsSearchKeys.TransactionDateTo);
				}
				if (clsSearchKeys.TransactionStatus != TransactionStatus.NotYetApplied)
				{
					SQL += "AND TransactionStatus = @TransactionStatus ";
                    cmd.Parameters.AddWithValue("@TransactionStatus", clsSearchKeys.TransactionStatus.ToString("d"));
				}
				if (clsSearchKeys.PaymentType != PaymentTypes.NotYetAssigned)
				{
					SQL += "AND PaymentType = @PaymentType ";
                    cmd.Parameters.AddWithValue("@PaymentType", clsSearchKeys.PaymentType.ToString("d"));
				}

				if (SortField != string.Empty && SortField != null)
				{
					SQL += "ORDER BY " + SortField + " ";

					if (SortOrder != System.Data.SqlClient.SortOrder.Descending)
						SQL += "ASC ";
					else
						SQL += "DESC ";
				}

				if (Limit != 0)
					SQL += "LIMIT " + Limit + " ";

				
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

        public System.Data.DataTable SalesPerDay(DateTime StartTransactionDate, DateTime EndTransactionDate, bool WithTrustFund = false)
        {
            try
            {
                MySqlCommand cmd = new MySqlCommand();
                string SQL = string.Empty;
                if (WithTrustFund)
                    #region WithTrustFund
                    SQL = "SELECT " +
                                "DateLastInitialized, " +
                                "day(DateLastInitialized) AS DATE, " +
                                "DailySales - (DailySales * TrustFund/100) AS DailySales, " +
                                "0 AS PromoWApproval, " +
                                "0 AS PromoWOApproval, " +
                                "0 AS GC, " +
                                "TotalDiscount - (TotalDiscount * TrustFund/100) AS TotalDiscount, " +
                                "LocalTax - (LocalTax * TrustFund/100) AS LocalTax, " +
                                "TotalCharge - (TotalCharge * TrustFund/100) AS TotalCharge, " +
                                "GrossSales - (GrossSales * TrustFund/100) AS GrossSales, " +
                                "VAT - (VAT * TrustFund/100) AS VAT " +
                            "FROM tblTerminalReportHistory " +
                            "WHERE DateLastInitialized >= @StartDate " +
                            "AND DateLastInitialized <= @EndDate " +
                            "ORDER BY DateLastInitialized;";
                    #endregion
                else
                    #region Without TrustFund
                    SQL = "SELECT " +
                                "DateLastInitialized, " +
                                "day(DateLastInitialized) AS DATE, " +
                                "DailySales, " +
                                "0 AS PromoWApproval, " +
                                "0 AS PromoWOApproval, " +
                                "0 AS GC, " +
                                "TotalDiscount, " +
                                "LocalTax, " +
                                "TotalCharge, " +
                                "GrossSales, " +
                                "VAT " +
                            "FROM tblTerminalReportHistory " +
                            "WHERE DateLastInitialized >= @StartDate " +
                            "AND DateLastInitialized <= @EndDate " +
                            "ORDER BY DateLastInitialized;";
                    #endregion

                MySqlParameter prmStartTransactionDate = new MySqlParameter("@StartDate", MySqlDbType.DateTime);
                prmStartTransactionDate.Value = StartTransactionDate.ToString("yyyy-MM-dd HH:mm:ss");
                cmd.Parameters.Add(prmStartTransactionDate);

                MySqlParameter prmEndTransactionDate = new MySqlParameter("@EndDate", MySqlDbType.DateTime);
                prmEndTransactionDate.Value = EndTransactionDate.ToString("yyyy-MM-dd HH:mm:ss");
                cmd.Parameters.Add(prmEndTransactionDate);

                cmd.CommandType = System.Data.CommandType.Text;
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

		public System.Data.DataTable SalesPerHour(string BeginningTransactionNo, string EndingTransactionNo, DateTime? StartDateTimeOfTransaction = null, DateTime? UptoDateTimeOfTransaction = null, int BranchID = 0, string TerminalNo = Constants.ALL)
		{
			try
			{
				TerminalReport clsTerminalReport = new TerminalReport(base.Connection, base.Transaction);
                System.Data.DataTable dt = clsTerminalReport.HourlyReport(BeginningTransactionNo, EndingTransactionNo, StartDateTimeOfTransaction, UptoDateTimeOfTransaction, BranchID, TerminalNo);

				return dt;
			}
			catch (Exception ex)
			{
				throw base.ThrowException(ex);
			}
		}

        public System.Data.DataTable SalesPerCreditCard(Int32 BranchID, string TerminalNo, Int64 CashierID, DateTime StartTransactionDate, DateTime EndTransactionDate)
        {
            try
            {
                TerminalReport clsTerminalReport = new TerminalReport(base.Connection, base.Transaction);
                System.Data.DataTable dt = clsTerminalReport.CreditCardReport(BranchID, TerminalNo, CashierID, StartTransactionDate, EndTransactionDate);

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
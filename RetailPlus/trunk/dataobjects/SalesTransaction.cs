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
		public string TransactionNo;
		public int BranchID;
		public string BranchCode;
		public Int64 CustomerID;
		public string CustomerName;
		public Int64 CashierID;
		public string CashierName;
		public string TerminalNo;
		public DateTime TransactionDate;
		public DateTime DateSuspended;
		public DateTime DateResumed;
		public TransactionStatus TransactionStatus;
		public decimal SubTotal;
		public decimal Discount;
		public decimal TransDiscount;
		public DiscountTypes TransDiscountType;
		public decimal VAT;
		public decimal VatableAmount;
		public decimal EVAT;
		public decimal EVatableAmount;
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
		public decimal Charge;
		public decimal ChargeAmount;
		public string ChargeCode;
		public string ChargeRemarks;
		public decimal CreditChargeAmount;

		/**
		 * Added on Feb 15, 2008 
		 * Hold all variables in FE
		 * */
		public decimal TotalItemSold;
		public decimal TotalQuantitySold;
		public decimal DiscountableAmount;
		public decimal NonVATableAmount;
		public decimal NonEVATableAmount;
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

		public DateTime TransactionDateFrom;
		public DateTime TransactionDateTo;
	}

	public struct SalesTransactionsColumns
	{
		public bool TransactionID;
		public bool TransactionNo;
		public bool BranchID;
		public bool BranchCode;
		public bool CustomerID;
		public bool CustomerName;
		public bool CashierID;
		public bool CashierName;
		public bool TerminalNo;
		public bool TransactionDate;
		public bool DateSuspended;
		public bool DateResumed;
		public bool TransactionStatus;
		public bool TransactionStatusName;
		public bool SubTotal;
		public bool Discount;
		public bool TransDiscount;
		public bool TransDiscountType;
		public bool VAT;
		public bool VatableAmount;
		public bool EVAT;
		public bool EVatableAmount;
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
		public bool TotalItemSold;
		public bool TotalQuantitySold;
		public bool DiscountableAmount;
		public bool NonVATableAmount;
		public bool NonEVATableAmount;
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

	}

	public struct SalesTransactionsColumnNames
	{
		public const string TransactionID = "TransactionID";
		public const string TransactionNo = "TransactionNo";
		public const string BranchID = "BranchID";
		public const string BranchCode = "BranchCode";
		public const string CustomerID = "CustomerID";
		public const string CustomerName = "CustomerName";
		public const string CashierID = "CashierID";
		public const string CashierName = "CashierName";
		public const string TerminalNo = "TerminalNo";
		public const string TransactionDate = "TransactionDate";
		public const string DateSuspended = "DateSuspended";
		public const string DateResumed = "DateResumed";
		public const string TransactionStatus = "TransactionStatus";
		public const string TransactionStatusName = "TransactionStatusName";
		public const string SubTotal = "SubTotal";
		public const string Discount = "Discount";
		public const string TransDiscount = "TransDiscount";
		public const string TransDiscountType = "TransDiscountType";
		public const string VAT = "VAT";
		public const string VatableAmount = "VatableAmount";
		public const string EVAT = "EVAT";
		public const string EVatableAmount = "EVatableAmount";
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
		public const string Charge = "Charge";
		public const string ChargeAmount = "ChargeAmount";
		public const string ChargeCode = "ChargeCode";
		public const string ChargeRemarks = "ChargeRemarks";
		public const string CreditChargeAmount = "CreditChargeAmount";

		/**
		 * Added on Feb 15, 2008 
		 * Hold all variables in FE
		 * */
		public const string TotalItemSold = "TotalItemSold";
		public const string TotalQuantitySold = "TotalQuantitySold";
		public const string DiscountableAmount = "DiscountableAmount";
		public const string NonVATableAmount = "NonVATableAmount";
		public const string NonEVATableAmount = "NonEVATableAmount";
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

		private string SQLSelect()
		{
			//	Apr 30, 2007 : Added "Charge, ChargeAmount, ChargeCode, ChargeRemarks "
			//	Feb 07, 2008 : Added "WaiterID, WaiterName "

			string stSQL = "SELECT " +
								"TransactionID, " +
								"TransactionNo, " +
								"BranchID, " +
								"BranchCode, " +
								"PaxNo, " +
								"CustomerID, " +
								"CustomerName, " +
								"AgentID, " +
								"AgentName, " +
								"CreatedByID, " +
								"CreatedByName, " +
								"CashierID, " +
								"CashierName, " +
								"TerminalNo, " +
								"TransactionDate, " +
								"DateSuspended, " +
								"DateResumed, " +
								"TransactionStatus, " +
								"SubTotal, " +
								"ItemsDiscount, " +
								"Discount, " +
								"DiscountCode, " +
								"DiscountRemarks, " +
								"TransDiscount, " +
								"TransDiscountType, " +
								"VAT, " +
								"VatableAmount, " +
								"EVAT, " +
								"EVatableAmount, " +
								"LocalTax, " +
								"AmountPaid, " +
								"CashPayment, " +
								"ChequePayment, " +
								"CreditCardPayment, " +
								"CreditPayment, " +
								"DebitPayment, " +
								"RewardPointsPayment, " +
								"RewardConvertedPayment, " +
								"BalanceAmount, " +
								"ChangeAmount, " +
								"DateClosed, " +
								"PaymentType, " +
								"WaiterID, " +
								"WaiterName, " +
								"Charge, ChargeAmount, ChargeCode, ChargeRemarks, " +
								"CreditChargeAmount, " +
								"OrderType, " +
								"AgentPositionName, AgentDepartmentName FROM tblTransactions ";

			return stSQL;
		}

		private string SQLSelect(SalesTransactionsColumns clsSalesTransactionsColumns)
		{
			string stSQL = "SELECT ";

			if (clsSalesTransactionsColumns.TransactionNo) stSQL += "" + SalesTransactionsColumnNames.TransactionNo + ", ";
			if (clsSalesTransactionsColumns.BranchID) stSQL += "" + SalesTransactionsColumnNames.BranchID + ", ";
			if (clsSalesTransactionsColumns.BranchCode) stSQL += "" + SalesTransactionsColumnNames.BranchCode + ", ";
			if (clsSalesTransactionsColumns.PaxNo) stSQL += "" + SalesTransactionsColumnNames.PaxNo + ", ";
			if (clsSalesTransactionsColumns.CustomerID) stSQL += "" + SalesTransactionsColumnNames.CustomerID + ", ";
			if (clsSalesTransactionsColumns.CustomerName) stSQL += "" + SalesTransactionsColumnNames.CustomerName + ", ";
			if (clsSalesTransactionsColumns.AgentID) stSQL += "" + SalesTransactionsColumnNames.AgentID + ", ";
			if (clsSalesTransactionsColumns.AgentName) stSQL += "" + SalesTransactionsColumnNames.AgentName + ", ";
			if (clsSalesTransactionsColumns.CreatedByID) stSQL += "" + SalesTransactionsColumnNames.CreatedByID + ", ";
			if (clsSalesTransactionsColumns.CreatedByName) stSQL += "" + SalesTransactionsColumnNames.CreatedByName + ", ";
			if (clsSalesTransactionsColumns.CashierID) stSQL += "" + SalesTransactionsColumnNames.CashierID + ", ";
			if (clsSalesTransactionsColumns.CashierName) stSQL += "" + SalesTransactionsColumnNames.CashierName + ", ";
			if (clsSalesTransactionsColumns.TerminalNo) stSQL += "" + SalesTransactionsColumnNames.TerminalNo + ", ";
			if (clsSalesTransactionsColumns.TransactionDate) stSQL += "" + SalesTransactionsColumnNames.TransactionDate + ", ";
			if (clsSalesTransactionsColumns.DateSuspended) stSQL += "" + SalesTransactionsColumnNames.DateSuspended + ", ";
			if (clsSalesTransactionsColumns.DateResumed) stSQL += "" + SalesTransactionsColumnNames.DateResumed + ", ";
			if (clsSalesTransactionsColumns.TransactionStatus) stSQL += "" + SalesTransactionsColumnNames.TransactionStatus + ", ";
			if (clsSalesTransactionsColumns.TransactionStatusName) stSQL += "" + "CASE TransactionStatus " +
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
			if (clsSalesTransactionsColumns.SubTotal) stSQL += "" + SalesTransactionsColumnNames.SubTotal + ", ";
			if (clsSalesTransactionsColumns.ItemsDiscount) stSQL += "" + SalesTransactionsColumnNames.ItemsDiscount + ", ";
			if (clsSalesTransactionsColumns.Discount) stSQL += "" + SalesTransactionsColumnNames.Discount + ", ";
			if (clsSalesTransactionsColumns.DiscountCode) stSQL += "" + SalesTransactionsColumnNames.DiscountCode + ", ";
			if (clsSalesTransactionsColumns.DiscountRemarks) stSQL += "" + SalesTransactionsColumnNames.DiscountRemarks + ", ";
			if (clsSalesTransactionsColumns.TransDiscount) stSQL += "" + SalesTransactionsColumnNames.TransDiscount + ", ";
			if (clsSalesTransactionsColumns.TransDiscountType) stSQL += "" + SalesTransactionsColumnNames.TransDiscountType + ", ";
			if (clsSalesTransactionsColumns.VAT) stSQL += "" + SalesTransactionsColumnNames.VAT + ", ";
			if (clsSalesTransactionsColumns.VatableAmount) stSQL += "" + SalesTransactionsColumnNames.VatableAmount + ", ";
			if (clsSalesTransactionsColumns.EVAT) stSQL += "" + SalesTransactionsColumnNames.EVAT + ", ";
			if (clsSalesTransactionsColumns.EVatableAmount) stSQL += "" + SalesTransactionsColumnNames.EVatableAmount + ", ";
			if (clsSalesTransactionsColumns.LocalTax) stSQL += "" + SalesTransactionsColumnNames.LocalTax + ", ";
			if (clsSalesTransactionsColumns.AmountPaid) stSQL += "" + SalesTransactionsColumnNames.AmountPaid + ", ";
			if (clsSalesTransactionsColumns.CashPayment) stSQL += "" + SalesTransactionsColumnNames.CashPayment + ", ";
			if (clsSalesTransactionsColumns.ChequePayment) stSQL += "" + SalesTransactionsColumnNames.ChequePayment + ", ";
			if (clsSalesTransactionsColumns.CreditCardPayment) stSQL += "" + SalesTransactionsColumnNames.CreditCardPayment + ", ";
			if (clsSalesTransactionsColumns.CreditPayment) stSQL += "" + SalesTransactionsColumnNames.CreditPayment + ", ";
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

			stSQL += "TransactionID FROM tblTransactions ";

			return stSQL;
		}

		#region Details

		public SalesTransactionDetails Details(string TransactionNo, string TerminalNo, int BranchID)
		{

			try
			{
				SalesTransactionDetails Details = new SalesTransactionDetails();

				string SQL = SQLSelect() + "WHERE TransactionNo = @TransactionNo AND TerminalNo = @TerminalNo AND BranchID = @BranchID;";

				MySqlCommand cmd = new MySqlCommand();
				cmd.CommandType = System.Data.CommandType.Text;
				cmd.CommandText = SQL;

				MySqlParameter prmTransactionNo = new MySqlParameter("@TransactionNo",MySqlDbType.String);
				prmTransactionNo.Value = TransactionNo;
				cmd.Parameters.Add(prmTransactionNo);

				MySqlParameter prmTerminalNo = new MySqlParameter("@TerminalNo",MySqlDbType.String);
				prmTerminalNo.Value = TerminalNo;
				cmd.Parameters.Add(prmTerminalNo);

				MySqlParameter prmBranchID = new MySqlParameter("@BranchID",MySqlDbType.Int32);
				prmBranchID.Value = BranchID;
				cmd.Parameters.Add(prmBranchID);

				MySqlDataReader myReader = base.ExecuteReader(cmd, System.Data.CommandBehavior.SingleResult);

				while (myReader.Read())
				{
					Details.TransactionID = myReader.GetInt64("TransactionID");
					Details.TransactionNo = "" + myReader["TransactionNo"].ToString();
					Details.BranchID = myReader.GetInt32("BranchID");
					Details.BranchCode = "" + myReader["BranchCode"].ToString();
					Details.PaxNo = myReader.GetInt32("PaxNo");
					Details.CustomerID = myReader.GetInt32("CustomerID");
					Details.CustomerName = "" + myReader["CustomerName"].ToString();
					Details.AgentID = myReader.GetInt32("AgentID");
					Details.AgentName = "" + myReader["AgentName"].ToString();
					Details.CreatedByID = myReader.GetInt64("CreatedByID");
					Details.CreatedByName = "" + myReader["CreatedByName"].ToString();
					Details.CashierID = myReader.GetInt64("CashierID");
					Details.CashierName = "" + myReader["CashierName"].ToString();
					Details.TerminalNo = "" + myReader["TerminalNo"].ToString();
					Details.TransactionDate = myReader.GetDateTime("TransactionDate");
					Details.DateSuspended = myReader.GetDateTime("DateSuspended");
					Details.DateResumed = myReader.GetDateTime("DateResumed");
					Details.TransactionStatus = (TransactionStatus)Enum.Parse(typeof(TransactionStatus), myReader.GetString("TransactionStatus"));
					Details.SubTotal = myReader.GetDecimal("SubTotal");
					Details.ItemsDiscount = myReader.GetDecimal("ItemsDiscount");
					Details.Discount = myReader.GetDecimal("Discount");
					// Aug 6, 2011 : Include in loading DiscountCode
					Details.DiscountCode = "" + myReader["DiscountCode"].ToString();
					// Aug 6, 2011 : Include in loading DiscountRemarks
					Details.DiscountRemarks = "" + myReader["DiscountRemarks"].ToString();
					Details.TransDiscount = myReader.GetDecimal("TransDiscount");
					Details.TransDiscountType = (DiscountTypes)Enum.Parse(typeof(DiscountTypes), myReader.GetString("TransDiscountType"));
					Details.VAT = myReader.GetDecimal("VAT");
					Details.VatableAmount = myReader.GetDecimal("VatableAmount");
					Details.EVAT = myReader.GetDecimal("EVAT");
					Details.EVatableAmount = myReader.GetDecimal("EVatableAmount");
					Details.LocalTax = myReader.GetDecimal("LocalTax");
					Details.AmountPaid = myReader.GetDecimal("AmountPaid");
					Details.CashPayment = myReader.GetDecimal("CashPayment");
					Details.ChequePayment = myReader.GetDecimal("ChequePayment");
					Details.CreditCardPayment = myReader.GetDecimal("CreditCardPayment");
					Details.CreditPayment = myReader.GetDecimal("CreditPayment");
					Details.DebitPayment = myReader.GetDecimal("DebitPayment");
					Details.RewardPointsPayment = myReader.GetDecimal("RewardPointsPayment");
					Details.RewardConvertedPayment = myReader.GetDecimal("RewardConvertedPayment");
					Details.BalanceAmount = myReader.GetDecimal("BalanceAmount");
					Details.ChangeAmount = myReader.GetDecimal("ChangeAmount");
					Details.DateClosed = myReader.GetDateTime("DateClosed");
					Details.PaymentType = (PaymentTypes)Enum.Parse(typeof(PaymentTypes), myReader.GetString("PaymentType"));
					Details.WaiterID = myReader.GetInt64("WaiterID");
					Details.WaiterName = "" + myReader["WaiterName"].ToString();
					Details.Charge = myReader.GetDecimal("Charge");
					Details.ChargeAmount = myReader.GetDecimal("ChargeAmount");
					Details.ChargeCode = "" + myReader["ChargeCode"].ToString();
					Details.ChargeRemarks = "" + myReader["ChargeRemarks"].ToString();
					Details.CreditChargeAmount = myReader.GetDecimal("CreditChargeAmount");
                    Details.OrderType = (OrderTypes)Enum.Parse(typeof(OrderTypes), myReader.GetString("OrderType"));
					Details.AgentPositionName = "" + myReader["AgentPositionName"].ToString();
					Details.AgentDepartmentName = "" + myReader["AgentDepartmentName"].ToString();
					Details.isExist = true;
				}
				myReader.Close();

				return Details;
			}

			catch (Exception ex)
			{
				throw ex;
			}
		}

		public string getSuspendedTransactionNo(long CustomerID, string TerminalNo, int BranchID)
		{

			try
			{
				string stRetValue = string.Empty;

				string SQL = "SELECT DISTINCT(TransactionNo) FROM  tblTransactions " +
							"WHERE CustomerID = @CustomerID AND TerminalNo = @TerminalNo AND TransactionStatus = @TransactionStatus AND BranchID = @BranchID LIMIT 1;";

				MySqlCommand cmd = new MySqlCommand();
				cmd.CommandType = System.Data.CommandType.Text;
				cmd.CommandText = SQL;

				MySqlParameter prmCustomerID = new MySqlParameter("@CustomerID",MySqlDbType.Int64);
				prmCustomerID.Value = CustomerID;
				cmd.Parameters.Add(prmCustomerID);

				MySqlParameter prmTerminalNo = new MySqlParameter("@TerminalNo",MySqlDbType.String);
				prmTerminalNo.Value = TerminalNo;
				cmd.Parameters.Add(prmTerminalNo);

				MySqlParameter prmTransStatus = new MySqlParameter("@TransactionStatus",MySqlDbType.Int16);
				prmTransStatus.Value = TransactionStatus.Suspended.ToString("d");
				cmd.Parameters.Add(prmTransStatus);

				MySqlParameter prmBranchID = new MySqlParameter("@BranchID",MySqlDbType.Int32);
				prmBranchID.Value = BranchID;
				cmd.Parameters.Add(prmBranchID);

				MySqlDataReader myReader = base.ExecuteReader(cmd, System.Data.CommandBehavior.SingleResult);

				while (myReader.Read())
				{
					stRetValue = "" + myReader["TransactionNo"].ToString();
				}
				myReader.Close();

				return stRetValue;
			}

			catch (Exception ex)
			{
				throw ex;
			}
		}

		#endregion

		#region Insert and Update

		public Int64 Insert(SalesTransactionDetails Details)
		{
			//	April 30, 2007 : Added "ChargeCode, ChargeRemarks" 
			try
			{
				string SQL = "INSERT INTO tblTransactions (" +
								"TransactionNo, " +
								"BranchID, " +
								"BranchCode, " + 
								"CustomerID, " +
								"CustomerName, " +
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
								"AgentPositionName, AgentDepartmentName" +
							")VALUES(" +
								"@TransactionNo, " +
								"@BranchID, " +
								"@BranchCode, " +
								"@CustomerID, " +
								"@CustomerName, " +
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
								"@AgentPositionName, @AgentDepartmentName" +
								");";

				MySqlCommand cmd = new MySqlCommand();
				cmd.CommandType = System.Data.CommandType.Text;
				cmd.CommandText = SQL;

				cmd.Parameters.AddWithValue("@TransactionNo", Details.TransactionNo);
				cmd.Parameters.AddWithValue("@BranchID", Details.BranchID);
				cmd.Parameters.AddWithValue("@BranchCode", Details.BranchCode);
				cmd.Parameters.AddWithValue("@CustomerID", Details.CustomerID);
				cmd.Parameters.AddWithValue("@CustomerName", Details.CustomerName);
				cmd.Parameters.AddWithValue("@AgentID", Details.AgentID);
				cmd.Parameters.AddWithValue("@AgentName", Details.AgentName);
				cmd.Parameters.AddWithValue("@CreatedByID", Details.CreatedByID);
				cmd.Parameters.AddWithValue("@CreatedByName", Details.CreatedByName);
				cmd.Parameters.AddWithValue("@CashierID", Details.CashierID);
				cmd.Parameters.AddWithValue("@CashierName", Details.CashierName);
				cmd.Parameters.AddWithValue("@TerminalNo", Details.TerminalNo);
				cmd.Parameters.AddWithValue("@TransactionDate", Details.TransactionDate.ToString("yyyy-MM-dd HH:mm:ss"));
				cmd.Parameters.AddWithValue("@DateSuspended", Details.DateSuspended.ToString("yyyy-MM-dd HH:mm:ss"));
				cmd.Parameters.AddWithValue("@TransactionStatus", Details.TransactionStatus.ToString("d"));
				cmd.Parameters.AddWithValue("@DiscCode", Details.DiscountCode);
				if (Details.DiscountRemarks == null) Details.DiscountRemarks = ""; cmd.Parameters.AddWithValue("@DiscRemarks", Details.DiscountRemarks);
				cmd.Parameters.AddWithValue("@WaiterID", Details.WaiterID);
				cmd.Parameters.AddWithValue("@WaiterName", Details.WaiterName);
				cmd.Parameters.AddWithValue("@ChargeCode", Details.ChargeCode);
				if (Details.ChargeRemarks == null) Details.ChargeRemarks = ""; cmd.Parameters.AddWithValue("@ChargeRemarks", Details.ChargeRemarks);
				cmd.Parameters.AddWithValue("@OrderType", Details.OrderType.ToString("d"));
				cmd.Parameters.AddWithValue("@AgentPositionName", Details.AgentPositionName);
				cmd.Parameters.AddWithValue("@AgentDepartmentName", Details.AgentDepartmentName);

				base.ExecuteNonQuery(cmd);

				SQL = "SELECT LAST_INSERT_ID();";

				cmd.Parameters.Clear();
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
				throw ex;
			}
		}
		public void UpdateContact(Int64 TransactionID, DateTime TransactionDate, ContactDetails details)
		{
			try
			{
				string SQL = "CALL procTransactionContactUpdate(@TransactionID, @CustomerID, @CustomerName);";

				MySqlCommand cmd = new MySqlCommand();
				cmd.CommandType = System.Data.CommandType.Text;
				cmd.CommandText = SQL;

				cmd.Parameters.AddWithValue("@TransactionID", TransactionID);
				cmd.Parameters.AddWithValue("@CustomerID", details.ContactID);
				cmd.Parameters.AddWithValue("@CustomerName", details.ContactName);

				base.ExecuteNonQuery(cmd);
			}
			catch (Exception ex)
			{
				throw ex;
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
				throw ex;
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
				throw ex;
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
				throw ex;
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
				if (clsSearchKeys.TransactionNo != string.Empty && clsSearchKeys.TransactionNo != null)
				{
					SQL += "AND tblTransactions.TransactionNo = @TransactionNo ";
					MySqlParameter prmTransactionNo = new MySqlParameter("@TransactionNo",MySqlDbType.String);
					prmTransactionNo.Value = clsSearchKeys.TransactionNo;
					cmd.Parameters.Add(prmTransactionNo);
				}

				if (clsSearchKeys.CustomerName != string.Empty && clsSearchKeys.CustomerName != null)
				{
					SQL += "AND tblTransactions.CustomerName = @CustomerName ";
					MySqlParameter prmCustomerName = new MySqlParameter("@CustomerName",MySqlDbType.String);
					prmCustomerName.Value = clsSearchKeys.CustomerName;
					cmd.Parameters.Add(prmCustomerName);
				}
				if (clsSearchKeys.CashierName != string.Empty && clsSearchKeys.CashierName != null)
				{
					SQL += "AND tblTransactions.CashierName = @CashierName ";
					MySqlParameter prmCashierName = new MySqlParameter("@CashierName",MySqlDbType.String);
					prmCashierName.Value = clsSearchKeys.CashierName;
					cmd.Parameters.Add(prmCashierName);
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
				throw ex;
			}
		}

		public System.Data.DataTable ListForPaymentDataTable(Int64 ContactID)
		{
			try
			{
				string SQL = "SELECT " +
									   "a.TransactionID, " +
									   "a.TransactionNo, " +
									   "a.PaxNo, " +
									   "a.CustomerID, " +
									   "a.CustomerName, " +
									   "a.TransactionDate, " +
									   "a.SubTotal, " +
									   "a.ItemsDiscount, " +
									   "a.Discount, " +
									   "a.AmountPaid - b.Amount 'AmountPaid', " +
									   "b.Amount 'Credit', " +
									   "b.AmountPaid 'CreditPaid', " +
									   "b.Amount - b.AmountPaid 'Balance' " +
								   "FROM  tblTransactions a " +
								   "INNER JOIN tblCreditPayment b ON a.TransactionNo = b.TransactionNo " +
								   "WHERE 1=1 " +
								   "AND CustomerID = @ContactID " +
								   "AND ContactID = @ContactID " +
								   "AND b.Amount > b.AmountPaid";

				// Added Jan 18, 2009
				// ORDER BY TransactionNo ASC
				// SO that FIFO during payment will be applied
				// FIFO - first in first out
				SQL = "SELECT TransactionID, " +
							"TransactionNo, " +
							"PaxNo, " +
							"CustomerID, " +
							"CustomerName, " +
							"TransactionDate, " +
							"SubTotal, " +
							"ItemsDiscount, " +
							"Discount, " +
							"AmountPaid, " +
							"Credit, " +
							"CreditPaid, " +
							"Balance " +
						"FROM (" + SQL + ") AS tblCreditPayment ORDER BY TransactionNo ASC ";

				MySqlCommand cmd = new MySqlCommand();
				cmd.CommandType = System.Data.CommandType.Text;
				cmd.CommandText = SQL;

				MySqlParameter prmContactID = new MySqlParameter("@ContactID",MySqlDbType.Int64);
				prmContactID.Value = ContactID;
				cmd.Parameters.Add(prmContactID);

				System.Data.DataTable dt = new System.Data.DataTable("tblForPayment");
				base.MySqlDataAdapterFill(cmd, dt);
				
				return dt;
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}

		public System.Data.DataTable ListSuspendedDataTable(int BranchID, string TerminalNo, Int64 CashierID, string SortField, SortOption SortOrder, bool isPacked)
		{
			try
			{
				MySqlCommand cmd = new MySqlCommand();

				SalesTransactionsColumns clsSalesTransactionsColumns = new SalesTransactionsColumns();
				clsSalesTransactionsColumns.TransactionNo = true;
				clsSalesTransactionsColumns.CustomerName = true;
				clsSalesTransactionsColumns.DateSuspended = true;

				string SQL = SQLSelect(clsSalesTransactionsColumns) + "WHERE TransactionStatus = @TransactionStatus ";

				if (BranchID != 0)
				{
					SQL += "AND BranchID = @BranchID ";
					MySqlParameter prmBranchID = new MySqlParameter("@BranchID",MySqlDbType.Int32);
					prmBranchID.Value = BranchID;
					cmd.Parameters.Add(prmBranchID);
				}

				if (TerminalNo != string.Empty && TerminalNo != null)
				{
					SQL += "AND TerminalNo = @TerminalNo ";
					MySqlParameter prmTerminalNo = new MySqlParameter("@TerminalNo",MySqlDbType.String);
					prmTerminalNo.Value = TerminalNo;
					cmd.Parameters.Add(prmTerminalNo);
				}

				if (CashierID != 0)
				{
					SQL += "AND CashierID = @CashierID ";
					MySqlParameter prmCashierID = new MySqlParameter("@CashierID",MySqlDbType.Int64);
					prmCashierID.Value = CashierID;
					cmd.Parameters.Add(prmCashierID);
				}

				if (isPacked)
				{
					SQL += "AND Packed = 1 ";
				}

				if (SortField != string.Empty && SortField != null)
				{
					SQL += "ORDER BY " + SortField + " ";

					if (SortOrder != SortOption.Desscending)
						SQL += "ASC ";
					else
						SQL += "DESC ";
				}
				
				cmd.CommandType = System.Data.CommandType.Text;
				cmd.CommandText = SQL;

				MySqlParameter prmTransStatus = new MySqlParameter("@TransactionStatus",MySqlDbType.Int16);
				prmTransStatus.Value = TransactionStatus.Suspended.ToString("d");
				cmd.Parameters.Add(prmTransStatus);

				System.Data.DataTable dt = new System.Data.DataTable("tblSuspendedTransactions");
				base.MySqlDataAdapterFill(cmd, dt);
				
				return dt;

			}
			catch (Exception ex)
			{
				throw ex;
			}
		}

		public decimal SeniorCitizenDiscounts(string TerminalNo, string TransactionNoFrom, string TransactionNoTo, out long DiscountCount)
		{
			try
			{
				string SQL = "CALL procGenerateDiscountByTerminalNo(@SessionID, @TerminalNo, @TransactionNoFrom, @TransactionNoTo);";

				MySqlCommand cmd = new MySqlCommand();
				cmd.CommandType = System.Data.CommandType.Text;
				cmd.CommandText = SQL;

				Random clsRandom = new Random();
				MySqlParameter prmSessionID = new MySqlParameter("@SessionID",MySqlDbType.String);
				prmSessionID.Value = clsRandom.Next(1234567, 99999999);
				cmd.Parameters.Add(prmSessionID);

				MySqlParameter prmTerminalNo = new MySqlParameter("@TerminalNo",MySqlDbType.String);
				prmTerminalNo.Value = TerminalNo;
				cmd.Parameters.Add(prmTerminalNo);

				MySqlParameter prmTransactionNoFrom = new MySqlParameter("@TransactionNoFrom",MySqlDbType.String);
				prmTransactionNoFrom.Value = TransactionNoFrom;
				cmd.Parameters.Add(prmTransactionNoFrom);

				MySqlParameter prmTransactionNoTo = new MySqlParameter("@TransactionNoTo",MySqlDbType.String);
				prmTransactionNoTo.Value = TransactionNoTo;
				cmd.Parameters.Add(prmTransactionNoTo);

				base.ExecuteNonQuery(cmd);

				SQL = "SELECT " +
						"DiscountCount, " +
						"Discount " +
					"FROM tblDiscountHistory " +
					"WHERE SessionID = @SessionID " +
						"AND DiscountCode = (SELECT SeniorCitizenDiscountCode FROM tblTerminal WHERE TerminalNo = @TerminalNo) " +
					"ORDER BY DiscountCode;";

				cmd.CommandText = SQL;
                cmd.Parameters.Clear();
				cmd.Parameters.Add(prmSessionID);
				cmd.Parameters.Add(prmTerminalNo);

				decimal decRetValue = 0;
				DiscountCount = 0;

				MySqlDataReader myReader = base.ExecuteReader(cmd, System.Data.CommandBehavior.SingleResult);
				while (myReader.Read())
				{
					DiscountCount = myReader.GetInt64("DiscountCount");
					decRetValue = myReader.GetDecimal("Discount");
				}
				myReader.Close();

				SQL = "CALL procDeleteDiscountHistory(@SessionID);";

				cmd.CommandText = SQL;
                cmd.Parameters.Clear();
				cmd.Parameters.Add(prmSessionID);

				return decRetValue;
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}

		public System.Data.DataTable Discounts(string TerminalNo, string TransactionNoFrom, string TransactionNoTo)
		{
			try
			{
				string SQL = "CALL procGenerateDiscountByTerminalNo(@SessionID, @TerminalNo, @TransactionNoFrom, @TransactionNoTo);";

				MySqlCommand cmd = new MySqlCommand();
				cmd.CommandType = System.Data.CommandType.Text;
				cmd.CommandText = SQL;

				Random clsRandom = new Random();
				MySqlParameter prmSessionID = new MySqlParameter("@SessionID",MySqlDbType.String);
				prmSessionID.Value = clsRandom.Next(1234567, 99999999);
				cmd.Parameters.Add(prmSessionID);

				MySqlParameter prmTerminalNo = new MySqlParameter("@TerminalNo",MySqlDbType.String);
				prmTerminalNo.Value = TerminalNo;
				cmd.Parameters.Add(prmTerminalNo);

				MySqlParameter prmTransactionNoFrom = new MySqlParameter("@TransactionNoFrom",MySqlDbType.String);
				prmTransactionNoFrom.Value = TransactionNoFrom;
				cmd.Parameters.Add(prmTransactionNoFrom);

				MySqlParameter prmTransactionNoTo = new MySqlParameter("@TransactionNoTo",MySqlDbType.String);
				prmTransactionNoTo.Value = TransactionNoTo;
				cmd.Parameters.Add(prmTransactionNoTo);

				base.ExecuteNonQuery(cmd);

				SQL = "SELECT " +
						"DiscountCode," +
						"Discount " +
					"FROM tblDiscountHistory " +
					"WHERE SessionID = @SessionID " +
					"ORDER BY DiscountCode;";

				cmd.CommandText = SQL;
                cmd.Parameters.Clear();
				cmd.Parameters.Add(prmSessionID);

				System.Data.DataTable dt = new System.Data.DataTable("tblDiscounts");
				base.MySqlDataAdapterFill(cmd, dt);

				SQL = "CALL procDeleteDiscountHistory(@SessionID);";

				cmd.CommandText = SQL;
                cmd.Parameters.Clear();
				cmd.Parameters.Add(prmSessionID);

				return dt;
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}

		public System.Data.DataTable Discounts(string TerminalNo, string TransactionNoFrom, string TransactionNoTo, long CashierID)
		{
			try
			{
				string SQL = "CALL procGenerateDiscountByTerminalNoByCashier(@SessionID, @TerminalNo, @CashierID, @TransactionNoFrom, @TransactionNoTo);";

				MySqlCommand cmd = new MySqlCommand();
				cmd.CommandType = System.Data.CommandType.Text;
				cmd.CommandText = SQL;

				Random clsRandom = new Random();
				MySqlParameter prmSessionID = new MySqlParameter("@SessionID",MySqlDbType.String);
				prmSessionID.Value = clsRandom.Next(1234567, 99999999);
				cmd.Parameters.Add(prmSessionID);

				MySqlParameter prmTerminalNo = new MySqlParameter("@TerminalNo",MySqlDbType.String);
				prmTerminalNo.Value = TerminalNo;
				cmd.Parameters.Add(prmTerminalNo);

				MySqlParameter prmCashierID = new MySqlParameter("@CashierID",MySqlDbType.Int64);
				prmCashierID.Value = CashierID;
				cmd.Parameters.Add(prmCashierID);

				MySqlParameter prmTransactionNoFrom = new MySqlParameter("@TransactionNoFrom",MySqlDbType.String);
				prmTransactionNoFrom.Value = TransactionNoFrom;
				cmd.Parameters.Add(prmTransactionNoFrom);

				MySqlParameter prmTransactionNoTo = new MySqlParameter("@TransactionNoTo",MySqlDbType.String);
				prmTransactionNoTo.Value = TransactionNoTo;
				cmd.Parameters.Add(prmTransactionNoTo);

				base.ExecuteNonQuery(cmd);

				SQL = "SELECT " +
						"DiscountCode," +
						"Discount " +
					"FROM tblDiscountHistory " +
					"WHERE SessionID = @SessionID " +
					"ORDER BY DiscountCode;";

				cmd.CommandText = SQL;
                cmd.Parameters.Clear();
				cmd.Parameters.Add(prmSessionID);

				System.Data.DataTable dt = new System.Data.DataTable("tblDiscounts");
				base.MySqlDataAdapterFill(cmd, dt);

				SQL = "CALL procDeleteDiscountHistory(@SessionID);";

				cmd.CommandText = SQL;
                cmd.Parameters.Clear();
				cmd.Parameters.Add(prmSessionID);

				return dt;
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}


		#endregion

		#region Public Modifiers

		public void UpdateCreditChargeAmount(Int64 TransactionID, decimal CreditChargeAmount)
		{
			try
			{
				string SQL = "UPDATE tblTransactions SET " +
								"SubTotal			=	@SubTotal + @CreditChargeAmount, " +
								"CreditChargeAmount =	@CreditChargeAmount " +
							"WHERE TransactionID	=	@TransactionID;";

				MySqlCommand cmd = new MySqlCommand();
				cmd.CommandType = System.Data.CommandType.Text;
				cmd.CommandText = SQL;

				MySqlParameter prmCreditChargeAmount = new MySqlParameter("@CreditChargeAmount",MySqlDbType.Decimal);
				prmCreditChargeAmount.Value = CreditChargeAmount;
				cmd.Parameters.Add(prmCreditChargeAmount);

				MySqlParameter prmTransactionID = new MySqlParameter("@TransactionID",MySqlDbType.Int64);
				prmTransactionID.Value = TransactionID;
				cmd.Parameters.Add(prmTransactionID);

				base.ExecuteNonQuery(cmd);
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}
		public void UpdateSubTotal(Int64 TransactionID, decimal SubTotal, decimal ItemsDiscount, decimal Discount, decimal TransDiscount, DiscountTypes TransDiscountType, decimal VAT, decimal VatableAmount, decimal EVAT, decimal EVatableAmount, decimal LocalTax, string DiscountCode, string DiscountRemarks, decimal Charge, decimal ChargeAmount, string ChargeCode, string ChargeRemarks)
		{
			string SQL = "UPDATE tblTransactions SET " +
							"TransactionStatus	=	@TransactionStatus, " +
							"SubTotal			=	@SubTotal, " +
							"ItemsDiscount		=	@ItemsDiscount, " +
							"Discount			=	@Discount, " +
							"TransDiscount		=	@TransDiscount, " +
							"TransDiscountType	=	@TransDiscType, " +
							"VAT				=	@VAT, " +
							"VatableAmount		=	@VatableAmount, " +
							"EVAT				=	@EVAT, " +
							"EVatableAmount		=	@EVatableAmount, " +
							"LocalTax			=	@LocalTax, " +
							"DateClosed			=	NOW(), " +
							"DiscountCode		=	@DiscCode,  " +
							"DiscountRemarks	=	@DiscRemarks, " +
							"Charge				=	@Charge, " +
							"ChargeAmount		=	@ChargeAmount, " +
							"ChargeCode			=	@ChargeCode, " +
							"ChargeRemarks		=	@ChargeRemarks " +
						"WHERE TransactionID	=	@TransactionID;";

			MySqlCommand cmd = new MySqlCommand();
			cmd.CommandType = System.Data.CommandType.Text;
			cmd.CommandText = SQL;

			MySqlParameter prmSubTotal = new MySqlParameter("@SubTotal",MySqlDbType.Decimal);
			prmSubTotal.Value = SubTotal;
			cmd.Parameters.Add(prmSubTotal);

			MySqlParameter prmItemsDiscount = new MySqlParameter("@ItemsDiscount",MySqlDbType.Decimal);
			prmItemsDiscount.Value = ItemsDiscount;
			cmd.Parameters.Add(prmItemsDiscount);

			MySqlParameter prmDiscount = new MySqlParameter("@Discount",MySqlDbType.Decimal);
			prmDiscount.Value = Discount;
			cmd.Parameters.Add(prmDiscount);

			MySqlParameter prmDiscountApplied = new MySqlParameter("@TransDiscount",MySqlDbType.Decimal);
			prmDiscountApplied.Value = TransDiscount;
			cmd.Parameters.Add(prmDiscountApplied);

			MySqlParameter prmDiscountType = new MySqlParameter("@TransDiscType",MySqlDbType.Int16);
			prmDiscountType.Value = Convert.ToInt16(TransDiscountType.ToString("d"));
			cmd.Parameters.Add(prmDiscountType);

			MySqlParameter prmVAT = new MySqlParameter("@VAT",MySqlDbType.Decimal);
			prmVAT.Value = VAT;
			cmd.Parameters.Add(prmVAT);

			MySqlParameter prmVatableAmount = new MySqlParameter("@VatableAmount",MySqlDbType.Decimal);
			prmVatableAmount.Value = VatableAmount;
			cmd.Parameters.Add(prmVatableAmount);

			MySqlParameter prmEVAT = new MySqlParameter("@EVAT",MySqlDbType.Decimal);
			prmEVAT.Value = EVAT;
			cmd.Parameters.Add(prmEVAT);

			MySqlParameter prmEVatableAmount = new MySqlParameter("@EVatableAmount",MySqlDbType.Decimal);
			prmEVatableAmount.Value = EVatableAmount;
			cmd.Parameters.Add(prmEVatableAmount);

			MySqlParameter prmLocalTax = new MySqlParameter("@LocalTax",MySqlDbType.Decimal);
			prmLocalTax.Value = LocalTax;
			cmd.Parameters.Add(prmLocalTax);

			MySqlParameter prmTransStatus = new MySqlParameter("@TransactionStatus",MySqlDbType.Int16);
			prmTransStatus.Value = TransactionStatus.Open.ToString("d");
			cmd.Parameters.Add(prmTransStatus);

			MySqlParameter prmDiscountCode = new MySqlParameter("@DiscCode",MySqlDbType.String);
			if (DiscountCode == null) DiscountCode = "";
			prmDiscountCode.Value = DiscountCode;
			cmd.Parameters.Add(prmDiscountCode);

			MySqlParameter prmDiscountRemarks = new MySqlParameter("@DiscRemarks",MySqlDbType.String);
			if (DiscountRemarks == null) DiscountRemarks = "";
			prmDiscountRemarks.Value = DiscountRemarks;
			cmd.Parameters.Add(prmDiscountRemarks);

			MySqlParameter prmCharge = new MySqlParameter("@Charge",MySqlDbType.Decimal);
			prmCharge.Value = Charge;
			cmd.Parameters.Add(prmCharge);

			MySqlParameter prmChargeAmount = new MySqlParameter("@ChargeAmount",MySqlDbType.Decimal);
			prmChargeAmount.Value = ChargeAmount;
			cmd.Parameters.Add(prmChargeAmount);

			MySqlParameter prmChargeCode = new MySqlParameter("@ChargeCode",MySqlDbType.String);
			if (ChargeCode == null) ChargeCode = "";
			prmChargeCode.Value = ChargeCode;
			cmd.Parameters.Add(prmChargeCode);

			MySqlParameter prmChargeRemarks = new MySqlParameter("@ChargeRemarks",MySqlDbType.String);
			if (ChargeRemarks == null) ChargeRemarks = "";
			prmChargeRemarks.Value = ChargeRemarks;
			cmd.Parameters.Add(prmChargeRemarks);

			MySqlParameter prmTransactionID = new MySqlParameter("@TransactionID",MySqlDbType.Int64);
			prmTransactionID.Value = TransactionID;
			cmd.Parameters.Add(prmTransactionID);

            base.ExecuteNonQuery(cmd);
		}
		public void Suspend(Int64 TransactionID, Decimal SubTotal)
		{
			try
			{
				string SQL = "UPDATE tblTransactions SET " +
								"TransactionStatus	=	@TransactionStatus, " +
								"SubTotal			=	@SubTotal, " +
								"DateSuspended		=	NOW() " +
							"WHERE TransactionID		=	@TransactionID;";

				MySqlCommand cmd = new MySqlCommand();
				cmd.CommandType = System.Data.CommandType.Text;
				cmd.CommandText = SQL;

				MySqlParameter prmSubTotal = new MySqlParameter("@SubTotal",MySqlDbType.Decimal);
				prmSubTotal.Value = SubTotal;
				cmd.Parameters.Add(prmSubTotal);

				MySqlParameter prmTransStatus = new MySqlParameter("@TransactionStatus",MySqlDbType.Int16);
				prmTransStatus.Value = TransactionStatus.Suspended.ToString("d");
				cmd.Parameters.Add(prmTransStatus);

				MySqlParameter prmTransactionID = new MySqlParameter("@TransactionID",MySqlDbType.Int64);
				prmTransactionID.Value = TransactionID;
				cmd.Parameters.Add(prmTransactionID);

				base.ExecuteNonQuery(cmd);
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}
		public void Suspend(Int64 TransactionID, Decimal SubTotal, ContactDetails details)
		{
			try
			{
				string SQL = "UPDATE tblTransactions SET " +
								"CustomerID			=	@CustomerID," +
								"CustomerName		=	@CustomerName, " +
								"TransactionStatus	=	@TransactionStatus, " +
								"SubTotal			=	@SubTotal, " +
								"DateSuspended		=	NOW() " +
							"WHERE TransactionID		=	@TransactionID;";

				MySqlCommand cmd = new MySqlCommand();
				cmd.CommandType = System.Data.CommandType.Text;
				cmd.CommandText = SQL;

				MySqlParameter prmCustomerID = new MySqlParameter("@CustomerID",MySqlDbType.Int64);
				prmCustomerID.Value = details.ContactID;
				cmd.Parameters.Add(prmCustomerID);

				MySqlParameter prmCustomerName = new MySqlParameter("@CustomerName",MySqlDbType.String);
				prmCustomerName.Value = details.ContactName;
				cmd.Parameters.Add(prmCustomerName);

				MySqlParameter prmSubTotal = new MySqlParameter("@SubTotal",MySqlDbType.Decimal);
				prmSubTotal.Value = SubTotal;
				cmd.Parameters.Add(prmSubTotal);

				MySqlParameter prmTransStatus = new MySqlParameter("@TransactionStatus",MySqlDbType.Int16);
				prmTransStatus.Value = TransactionStatus.Suspended.ToString("d");
				cmd.Parameters.Add(prmTransStatus);

				MySqlParameter prmTransactionID = new MySqlParameter("@TransactionID",MySqlDbType.Int64);
				prmTransactionID.Value = TransactionID;
				cmd.Parameters.Add(prmTransactionID);

				base.ExecuteNonQuery(cmd);
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}
		public void Resume(Int64 TransactionID)
		{
			try
			{
				string SQL = "UPDATE tblTransactions SET " +
								"TransactionStatus	=	@TransactionStatus, " +
								"DateResumed = NOW() " +
							"WHERE TransactionID = @TransactionID;";

				MySqlCommand cmd = new MySqlCommand();
				cmd.CommandType = System.Data.CommandType.Text;
				cmd.CommandText = SQL;

				MySqlParameter prmTransStatus = new MySqlParameter("@TransactionStatus",MySqlDbType.Int16);
				prmTransStatus.Value = TransactionStatus.Open.ToString("d");
				cmd.Parameters.Add(prmTransStatus);

				MySqlParameter prmTransactionID = new MySqlParameter("@TransactionID",MySqlDbType.Int64);
				prmTransactionID.Value = TransactionID;
				cmd.Parameters.Add(prmTransactionID);

				base.ExecuteNonQuery(cmd);
			}

			catch (Exception ex)
			{
				throw ex;
			}
		}
		public void Close(Int64 TransactionID, decimal SubTotal, decimal ItemsDiscount, decimal Discount, decimal TransDiscount, DiscountTypes TransDiscountType, decimal VAT, decimal VatableAmount, decimal EVAT, decimal EVatableAmount, decimal LocalTax, decimal AmountPaid, decimal CashPayment, decimal ChequePayment, decimal CreditCardPayment, decimal CreditPayment, decimal DebitPayment, decimal RewardPointsPayment, decimal RewardConvertedPayment, decimal BalanceAmount, decimal ChangeAmount, PaymentTypes PaymentType, string DiscountCode, string DiscountRemarks, decimal Charge, decimal ChargeAmount, string ChargeCode, string ChargeRemarks, Int64 CashierID, string CashierName)
		{
			try
			{
				string SQL = "UPDATE tblTransactions SET " +
								"TransactionStatus	=	@TransactionStatus, " +
								"SubTotal			=	@SubTotal, " +
								"ItemsDiscount		=	@ItemsDiscount, " +
								"Discount			=	@Discount, " +
								"TransDiscount		=	@TransDiscount, " +
								"TransDiscountType	=	@TransDiscType, " +
								"VAT				=	@VAT, " +
								"VatableAmount		=	@VatableAmount, " +
								"EVAT				=	@EVAT, " +
								"EVatableAmount		=	@EVatableAmount, " +
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
								"CashierName		=	@CashierName " +
					"WHERE TransactionID	=	@TransactionID;";

				MySqlCommand cmd = new MySqlCommand();
				cmd.CommandType = System.Data.CommandType.Text;
				cmd.CommandText = SQL;

				MySqlParameter prmTransactionStatus = new MySqlParameter("@TransactionStatus",MySqlDbType.Int16);
				prmTransactionStatus.Value = TransactionStatus.Closed.ToString("d");
				cmd.Parameters.Add(prmTransactionStatus);

				MySqlParameter prmSubTotal = new MySqlParameter("@SubTotal",MySqlDbType.Decimal);
				prmSubTotal.Value = SubTotal;
				cmd.Parameters.Add(prmSubTotal);

				MySqlParameter prmItemsDiscount = new MySqlParameter("@ItemsDiscount",MySqlDbType.Decimal);
				prmItemsDiscount.Value = ItemsDiscount;
				cmd.Parameters.Add(prmItemsDiscount);

				MySqlParameter prmDiscount = new MySqlParameter("@Discount",MySqlDbType.Decimal);
				prmDiscount.Value = Discount;
				cmd.Parameters.Add(prmDiscount);

				MySqlParameter prmDiscountApplied = new MySqlParameter("@TransDiscount",MySqlDbType.Decimal);
				prmDiscountApplied.Value = TransDiscount;
				cmd.Parameters.Add(prmDiscountApplied);

				MySqlParameter prmDiscountType = new MySqlParameter("@TransDiscType",MySqlDbType.Int16);
				prmDiscountType.Value = Convert.ToInt16(TransDiscountType.ToString("d"));
				cmd.Parameters.Add(prmDiscountType);

				MySqlParameter prmVAT = new MySqlParameter("@VAT",MySqlDbType.Decimal);
				prmVAT.Value = VAT;
				cmd.Parameters.Add(prmVAT);

				MySqlParameter prmVatableAmount = new MySqlParameter("@VatableAmount",MySqlDbType.Decimal);
				prmVatableAmount.Value = VatableAmount;
				cmd.Parameters.Add(prmVatableAmount);

				MySqlParameter prmEVAT = new MySqlParameter("@EVAT",MySqlDbType.Decimal);
				prmEVAT.Value = EVAT;
				cmd.Parameters.Add(prmEVAT);

				MySqlParameter prmEVatableAmount = new MySqlParameter("@EVatableAmount",MySqlDbType.Decimal);
				prmEVatableAmount.Value = EVatableAmount;
				cmd.Parameters.Add(prmEVatableAmount);

				MySqlParameter prmLocalTax = new MySqlParameter("@LocalTax",MySqlDbType.Decimal);
				prmLocalTax.Value = LocalTax;
				cmd.Parameters.Add(prmLocalTax);

				MySqlParameter prmAmountPaid = new MySqlParameter("@AmountPaid",MySqlDbType.Decimal);
				prmAmountPaid.Value = AmountPaid;
				cmd.Parameters.Add(prmAmountPaid);

				MySqlParameter prmCashPayment = new MySqlParameter("@CashPayment",MySqlDbType.Decimal);
				prmCashPayment.Value = CashPayment;
				cmd.Parameters.Add(prmCashPayment);

				MySqlParameter prmChequePayment = new MySqlParameter("@ChequePayment",MySqlDbType.Decimal);
				prmChequePayment.Value = ChequePayment;
				cmd.Parameters.Add(prmChequePayment);

				MySqlParameter prmCreditCardPayment = new MySqlParameter("@CreditCardPayment",MySqlDbType.Decimal);
				prmCreditCardPayment.Value = CreditCardPayment;
				cmd.Parameters.Add(prmCreditCardPayment);

				MySqlParameter prmCreditPayment = new MySqlParameter("@CreditPayment",MySqlDbType.Decimal);
				prmCreditPayment.Value = CreditPayment;
				cmd.Parameters.Add(prmCreditPayment);

				MySqlParameter prmDebitPayment = new MySqlParameter("@DebitPayment",MySqlDbType.Decimal);
				prmDebitPayment.Value = DebitPayment;
				cmd.Parameters.Add(prmDebitPayment);

				MySqlParameter prmRewardPointsPayment = new MySqlParameter("@RewardPointsPayment",MySqlDbType.Decimal);
				prmRewardPointsPayment.Value = RewardPointsPayment;
				cmd.Parameters.Add(prmRewardPointsPayment);

				MySqlParameter prmRewardConvertedPayment = new MySqlParameter("@RewardConvertedPayment",MySqlDbType.Decimal);
				prmRewardConvertedPayment.Value = RewardConvertedPayment;
				cmd.Parameters.Add(prmRewardConvertedPayment);

				MySqlParameter prmBalanceAmount = new MySqlParameter("@BalanceAmount",MySqlDbType.Decimal);
				prmBalanceAmount.Value = BalanceAmount;
				cmd.Parameters.Add(prmBalanceAmount);

				MySqlParameter prmChangeAmount = new MySqlParameter("@ChangeAmount",MySqlDbType.Decimal);
				prmChangeAmount.Value = ChangeAmount;
				cmd.Parameters.Add(prmChangeAmount);

				MySqlParameter prmPaymentType = new MySqlParameter("@PaymentType",MySqlDbType.Int16);
				prmPaymentType.Value = PaymentType.ToString("d");
				cmd.Parameters.Add(prmPaymentType);

				MySqlParameter prmDiscountCode = new MySqlParameter("@DiscCode",MySqlDbType.String);
				if (DiscountCode == null) DiscountCode = "";
				prmDiscountCode.Value = DiscountCode;
				cmd.Parameters.Add(prmDiscountCode);

				MySqlParameter prmDiscountRemarks = new MySqlParameter("@DiscRemarks",MySqlDbType.String);
				if (DiscountRemarks == null) DiscountRemarks = "";
				prmDiscountRemarks.Value = DiscountRemarks;
				cmd.Parameters.Add(prmDiscountRemarks);

				MySqlParameter prmCharge = new MySqlParameter("@Charge",MySqlDbType.Decimal);
				prmCharge.Value = Charge;
				cmd.Parameters.Add(prmCharge);

				MySqlParameter prmChargeAmount = new MySqlParameter("@ChargeAmount",MySqlDbType.Decimal);
				prmChargeAmount.Value = ChargeAmount;
				cmd.Parameters.Add(prmChargeAmount);

				MySqlParameter prmChargeCode = new MySqlParameter("@ChargeCode",MySqlDbType.String);
				if (ChargeCode == null) ChargeCode = "";
				prmChargeCode.Value = ChargeCode;
				cmd.Parameters.Add(prmChargeCode);

				MySqlParameter prmChargeRemarks = new MySqlParameter("@ChargeRemarks",MySqlDbType.String);
				if (ChargeRemarks == null) ChargeRemarks = "";
				prmChargeRemarks.Value = ChargeRemarks;
				cmd.Parameters.Add(prmChargeRemarks);

				MySqlParameter prmCashierID = new MySqlParameter("@CashierID",MySqlDbType.Int64);
				prmCashierID.Value = CashierID;
				cmd.Parameters.Add(prmCashierID);

				MySqlParameter prmCashierName = new MySqlParameter("@CashierName",MySqlDbType.String);
				prmCashierName.Value = CashierName;
				cmd.Parameters.Add(prmCashierName);

				MySqlParameter prmTransactionID = new MySqlParameter("@TransactionID",MySqlDbType.Int64);
				prmTransactionID.Value = TransactionID;
				cmd.Parameters.Add(prmTransactionID);

				base.ExecuteNonQuery(cmd);
			}

			catch (Exception ex)
			{
				throw ex;
			}
		}
		public void CloseAsOrderSlip(Int64 TransactionID)
		{
			try
			{
				string SQL = "UPDATE tblTransactions SET " +
								"TransactionStatus  = @TransactionStatus " +
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
				throw ex;
			}
		}
		public void Void(Int64 TransactionID, decimal SubTotal, decimal ItemsDiscount, decimal Discount, decimal TransDiscount, DiscountTypes TransDiscountType, decimal VAT, decimal VatableAmount, decimal EVAT, decimal EVatableAmount, decimal LocalTax, decimal Charge, Int64 CashierID, string CashierName)
		{
			try
			{
				string SQL = "UPDATE tblTransactions SET " +
								"TransactionStatus	=	@TransactionStatus, " +
								"SubTotal			=	@SubTotal, " +
								"ItemsDiscount		=	@ItemsDiscount, " +
								"Discount			=	@Discount, " +
								"VAT				=	@VAT, " +
								"VatableAmount		=	@VatableAmount, " +
								"EVAT				=	@EVAT, " +
								"EVatableAmount		=	@EVatableAmount, " +
								"LocalTax			=	@LocalTax, " +
								"DateClosed			=	NOW(), " +
								"Charge				=	@Charge, " +
								"CashierID			=	@CashierID, " +
								"CashierName		=	@CashierName " +
							"WHERE TransactionID	=	@TransactionID;";

				MySqlCommand cmd = new MySqlCommand();
				cmd.CommandType = System.Data.CommandType.Text;
				cmd.CommandText = SQL;

				MySqlParameter prmTransactionStatus = new MySqlParameter("@TransactionStatus",MySqlDbType.Int16);
				prmTransactionStatus.Value = TransactionStatus.Void.ToString("d");
				cmd.Parameters.Add(prmTransactionStatus);

				MySqlParameter prmSubTotal = new MySqlParameter("@SubTotal",MySqlDbType.Decimal);
				prmSubTotal.Value = SubTotal;
				cmd.Parameters.Add(prmSubTotal);

				MySqlParameter prmItemsDiscount = new MySqlParameter("@ItemsDiscount",MySqlDbType.Decimal);
				prmItemsDiscount.Value = ItemsDiscount;
				cmd.Parameters.Add(prmItemsDiscount);

				MySqlParameter prmDiscount = new MySqlParameter("@Discount",MySqlDbType.Decimal);
				prmDiscount.Value = Discount;
				cmd.Parameters.Add(prmDiscount);

				MySqlParameter prmVAT = new MySqlParameter("@VAT",MySqlDbType.Decimal);
				prmVAT.Value = VAT;
				cmd.Parameters.Add(prmVAT);

				MySqlParameter prmVatableAmount = new MySqlParameter("@VatableAmount",MySqlDbType.Decimal);
				prmVatableAmount.Value = VatableAmount;
				cmd.Parameters.Add(prmVatableAmount);

				MySqlParameter prmEVAT = new MySqlParameter("@EVAT",MySqlDbType.Decimal);
				prmEVAT.Value = EVAT;
				cmd.Parameters.Add(prmEVAT);

				MySqlParameter prmEVatableAmount = new MySqlParameter("@EVatableAmount",MySqlDbType.Decimal);
				prmEVatableAmount.Value = EVatableAmount;
				cmd.Parameters.Add(prmEVatableAmount);

				MySqlParameter prmLocalTax = new MySqlParameter("@LocalTax",MySqlDbType.Decimal);
				prmLocalTax.Value = LocalTax;
				cmd.Parameters.Add(prmLocalTax);

				MySqlParameter prmCharge = new MySqlParameter("@Charge",MySqlDbType.Decimal);
				prmCharge.Value = Charge;
				cmd.Parameters.Add(prmCharge);

				MySqlParameter prmCashierID = new MySqlParameter("@CashierID",MySqlDbType.Int64);
				prmCashierID.Value = CashierID;
				cmd.Parameters.Add(prmCashierID);

				MySqlParameter prmCashierName = new MySqlParameter("@CashierName",MySqlDbType.String);
				prmCashierName.Value = CashierName;
				cmd.Parameters.Add(prmCashierName);

				MySqlParameter prmTransactionID = new MySqlParameter("@TransactionID",MySqlDbType.Int64);
				prmTransactionID.Value = TransactionID;
				cmd.Parameters.Add(prmTransactionID);

				base.ExecuteNonQuery(cmd);

				SalesTransactionItems clsSalesTransactionItems = new SalesTransactionItems(base.Connection, base.Transaction);
				clsSalesTransactionItems.VoidByTransaction(TransactionID);
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}
		public void Refund(Int64 TransactionID, decimal SubTotal, decimal ItemsDiscount, decimal Discount, decimal TransDiscount, DiscountTypes TransDiscountType, decimal VAT, decimal VatableAmount, decimal EVAT, decimal EVatableAmount, decimal LocalTax, decimal AmountPaid, decimal CashPayment, decimal ChequePayment, decimal CreditCardPayment, decimal CreditPayment, decimal DebitPayment, decimal RewardPointsPayment, decimal RewardConvertedPayment, decimal BalanceAmount, decimal ChangeAmount, PaymentTypes PaymentType, string DiscountCode, string DiscountRemarks, decimal Charge, decimal ChargeAmount, string ChargeCode, string ChargeRemarks, Int64 CashierID, string CashierName)
		{
			try
			{
				string SQL = "UPDATE tblTransactions SET " +
								"TransactionStatus	=	@TransactionStatus, " +
								"SubTotal			=	@SubTotal, " +
								"ItemsDiscount		=	@ItemsDiscount, " +
								"Discount			=	@Discount, " +
								"TransDiscount		=	@TransDiscount, " +
								"TransDiscountType	=	@TransDiscType, " +
								"VAT				=	@VAT, " +
								"VatableAmount		=	@VatableAmount, " +
								"EVAT				=	@EVAT, " +
								"EVatableAmount		=	@EVatableAmount, " +
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
								"CashierName		=	@CashierName " +
							"WHERE TransactionID	=	@TransactionID;";

				MySqlCommand cmd = new MySqlCommand();
				cmd.CommandType = System.Data.CommandType.Text;
				cmd.CommandText = SQL;

				MySqlParameter prmTransactionStatus = new MySqlParameter("@TransactionStatus",MySqlDbType.Int16);
				prmTransactionStatus.Value = TransactionStatus.Refund.ToString("d");
				cmd.Parameters.Add(prmTransactionStatus);

				MySqlParameter prmSubTotal = new MySqlParameter("@SubTotal",MySqlDbType.Decimal);
				prmSubTotal.Value = SubTotal;
				cmd.Parameters.Add(prmSubTotal);

				MySqlParameter prmItemsDiscount = new MySqlParameter("@ItemsDiscount",MySqlDbType.Decimal);
				prmItemsDiscount.Value = ItemsDiscount;
				cmd.Parameters.Add(prmItemsDiscount);

				MySqlParameter prmDiscount = new MySqlParameter("@Discount",MySqlDbType.Decimal);
				prmDiscount.Value = Discount;
				cmd.Parameters.Add(prmDiscount);

				MySqlParameter prmDiscountApplied = new MySqlParameter("@TransDiscount",MySqlDbType.Decimal);
				prmDiscountApplied.Value = TransDiscount;
				cmd.Parameters.Add(prmDiscountApplied);

				MySqlParameter prmDiscountType = new MySqlParameter("@TransDiscType",MySqlDbType.Int16);
				prmDiscountType.Value = Convert.ToInt16(TransDiscountType.ToString("d"));
				cmd.Parameters.Add(prmDiscountType);

				MySqlParameter prmVAT = new MySqlParameter("@VAT",MySqlDbType.Decimal);
				prmVAT.Value = VAT;
				cmd.Parameters.Add(prmVAT);

				MySqlParameter prmVatableAmount = new MySqlParameter("@VatableAmount",MySqlDbType.Decimal);
				prmVatableAmount.Value = VatableAmount;
				cmd.Parameters.Add(prmVatableAmount);

				MySqlParameter prmEVAT = new MySqlParameter("@EVAT",MySqlDbType.Decimal);
				prmEVAT.Value = EVAT;
				cmd.Parameters.Add(prmEVAT);

				MySqlParameter prmEVatableAmount = new MySqlParameter("@EVatableAmount",MySqlDbType.Decimal);
				prmEVatableAmount.Value = EVatableAmount;
				cmd.Parameters.Add(prmEVatableAmount);

				MySqlParameter prmLocalTax = new MySqlParameter("@LocalTax",MySqlDbType.Decimal);
				prmLocalTax.Value = LocalTax;
				cmd.Parameters.Add(prmLocalTax);

				MySqlParameter prmAmountPaid = new MySqlParameter("@AmountPaid",MySqlDbType.Decimal);
				prmAmountPaid.Value = AmountPaid;
				cmd.Parameters.Add(prmAmountPaid);

				MySqlParameter prmCashPayment = new MySqlParameter("@CashPayment",MySqlDbType.Decimal);
				prmCashPayment.Value = CashPayment;
				cmd.Parameters.Add(prmCashPayment);

				MySqlParameter prmChequePayment = new MySqlParameter("@ChequePayment",MySqlDbType.Decimal);
				prmChequePayment.Value = ChequePayment;
				cmd.Parameters.Add(prmChequePayment);

				MySqlParameter prmCreditCardPayment = new MySqlParameter("@CreditCardPayment",MySqlDbType.Decimal);
				prmCreditCardPayment.Value = CreditCardPayment;
				cmd.Parameters.Add(prmCreditCardPayment);

				MySqlParameter prmCreditPayment = new MySqlParameter("@CreditPayment",MySqlDbType.Decimal);
				prmCreditPayment.Value = CreditPayment;
				cmd.Parameters.Add(prmCreditPayment);

				MySqlParameter prmDebitPayment = new MySqlParameter("@DebitPayment",MySqlDbType.Decimal);
				prmDebitPayment.Value = DebitPayment;
				cmd.Parameters.Add(prmDebitPayment);

				MySqlParameter prmRewardPointsPayment = new MySqlParameter("@RewardPointsPayment",MySqlDbType.Decimal);
				prmRewardPointsPayment.Value = RewardPointsPayment;
				cmd.Parameters.Add(prmRewardPointsPayment);

				MySqlParameter prmRewardConvertedPayment = new MySqlParameter("@RewardConvertedPayment",MySqlDbType.Decimal);
				prmRewardConvertedPayment.Value = RewardConvertedPayment;
				cmd.Parameters.Add(prmRewardConvertedPayment);

				MySqlParameter prmBalanceAmount = new MySqlParameter("@BalanceAmount",MySqlDbType.Decimal);
				prmBalanceAmount.Value = BalanceAmount;
				cmd.Parameters.Add(prmBalanceAmount);

				MySqlParameter prmChangeAmount = new MySqlParameter("@ChangeAmount",MySqlDbType.Decimal);
				prmChangeAmount.Value = ChangeAmount;
				cmd.Parameters.Add(prmChangeAmount);

				MySqlParameter prmPaymentType = new MySqlParameter("@PaymentType",MySqlDbType.Int16);
				prmPaymentType.Value = PaymentType.ToString("d");
				cmd.Parameters.Add(prmPaymentType);

				MySqlParameter prmDiscountCode = new MySqlParameter("@DiscCode",MySqlDbType.String);
				if (DiscountCode == null) DiscountCode = "";
				prmDiscountCode.Value = DiscountCode;
				cmd.Parameters.Add(prmDiscountCode);

				MySqlParameter prmDiscountRemarks = new MySqlParameter("@DiscRemarks",MySqlDbType.String);
				if (DiscountRemarks == null) DiscountRemarks = "";
				prmDiscountRemarks.Value = DiscountRemarks;
				cmd.Parameters.Add(prmDiscountRemarks);

				MySqlParameter prmCharge = new MySqlParameter("@Charge",MySqlDbType.Decimal);
				prmCharge.Value = Charge;
				cmd.Parameters.Add(prmCharge);

				MySqlParameter prmChargeAmount = new MySqlParameter("@ChargeAmount",MySqlDbType.Decimal);
				prmChargeAmount.Value = ChargeAmount;
				cmd.Parameters.Add(prmChargeAmount);

				MySqlParameter prmChargeCode = new MySqlParameter("@ChargeCode",MySqlDbType.String);
				if (ChargeCode == null) ChargeCode = "";
				prmChargeCode.Value = ChargeCode;
				cmd.Parameters.Add(prmChargeCode);

				MySqlParameter prmChargeRemarks = new MySqlParameter("@ChargeRemarks",MySqlDbType.String);
				if (ChargeRemarks == null) ChargeRemarks = "";
				prmChargeRemarks.Value = ChargeRemarks;
				cmd.Parameters.Add(prmChargeRemarks);

				MySqlParameter prmCashierID = new MySqlParameter("@CashierID",MySqlDbType.Int64);
				prmCashierID.Value = CashierID;
				cmd.Parameters.Add(prmCashierID);

				MySqlParameter prmCashierName = new MySqlParameter("@CashierName",MySqlDbType.String);
				prmCashierName.Value = CashierName;
				cmd.Parameters.Add(prmCashierName);

				MySqlParameter prmTransactionID = new MySqlParameter("@TransactionID",MySqlDbType.Int64);
				prmTransactionID.Value = TransactionID;
				cmd.Parameters.Add(prmTransactionID);

				base.ExecuteNonQuery(cmd);

				SalesTransactionItems clsSalesTransactionItems = new SalesTransactionItems(base.Connection, base.Transaction);
				clsSalesTransactionItems.RefundByTransaction(TransactionID);
			}

			catch (Exception ex)
			{
				throw ex;
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
				throw ex;
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
				throw ex;
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
				throw ex;
			}
		}

		public void UpdateTerminalNo(Int64 TransactionID, string TerminalNo)
		{
			try
			{
				string SQL = "CALL procTransactionTerminalNoUpdate(@TransactionID, @TerminalNo);";

				MySqlCommand cmd = new MySqlCommand();
				cmd.CommandType = System.Data.CommandType.Text;
				cmd.CommandText = SQL;

				MySqlParameter prmTransactionID = new MySqlParameter("@TransactionID",MySqlDbType.Int64);
				prmTransactionID.Value = TransactionID;
				cmd.Parameters.Add(prmTransactionID);

				MySqlParameter prmTerminalNo = new MySqlParameter("@TerminalNo",MySqlDbType.String);
				prmTerminalNo.Value = TerminalNo;
				cmd.Parameters.Add(prmTerminalNo);

				base.ExecuteNonQuery(cmd);
			}

			catch (Exception ex)
			{
				throw ex;
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
				throw ex;
			}
		}

		public Int64 AddItem(SalesTransactionItemDetails SalesTransItemDetails)
		{
			try
			{
				SalesTransactionItems clsSalesTransactionItems = new SalesTransactionItems(base.Connection, base.Transaction);
				Int64 TransactionItemID = clsSalesTransactionItems.Insert(SalesTransItemDetails);

				return TransactionItemID;
			}

			catch (Exception ex)
			{
				throw ex;
			}
		}

		public Int64 AddItem(Int64 TransactionID, decimal SubTotal, decimal ItemsDiscount, decimal Discount, decimal TransDiscount, DiscountTypes TransDiscountType, decimal VAT, decimal VatableAmount, decimal EVAT, decimal EVatableAmount, decimal LocalTax, string DiscountCode, string DiscountRemarks, decimal Charge, decimal ChargeAmount, string ChargeCode, string ChargeRemarks, SalesTransactionItemDetails SalesTransItemDetails)
		{
			try
			{
				UpdateSubTotal(TransactionID, SubTotal, ItemsDiscount, Discount, TransDiscount, TransDiscountType, VAT, VatableAmount, EVAT, EVatableAmount, LocalTax, DiscountCode, DiscountRemarks, Charge, ChargeAmount, ChargeCode, ChargeRemarks);

				SalesTransactionItems clsSalesTransactionItems = new SalesTransactionItems(base.Connection, base.Transaction);
				Int64 TransactionItemID = clsSalesTransactionItems.Insert(SalesTransItemDetails);

				return TransactionItemID;
			}

			catch (Exception ex)
			{
				throw ex;
			}
		}

		public TransactionStatus Status(string TransactionNo)
		{
			try
			{
				TransactionStatus status = TransactionStatus.NotYetApplied;

				string SQL = "SELECT TransactionStatus FROM  tblTransactions " +
					"WHERE CAST(TransactionNo AS UNSIGNED INT) = CAST(@TransactionNo AS UNSIGNED INT) ";

				MySqlCommand cmd = new MySqlCommand();
				cmd.CommandType = System.Data.CommandType.Text;
				cmd.CommandText = SQL;

				MySqlParameter prmTransactionNo = new MySqlParameter("@TransactionNo",MySqlDbType.String);
				prmTransactionNo.Value = TransactionNo;
				cmd.Parameters.Add(prmTransactionNo);

				MySqlDataReader myReader = base.ExecuteReader(cmd, System.Data.CommandBehavior.SingleResult);

				while (myReader.Read())
				{
                    status = (TransactionStatus)Enum.Parse(typeof(TransactionStatus), myReader.GetString("TransactionStatus"));
				}
				myReader.Close();
				return status;
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}


		public Int32 CountSuspended(string TerminalNo, long CashierID, int BranchID)
		{
			try
			{
				MySqlCommand cmd = new MySqlCommand();

				string SQL = "SELECT " +
								"COUNT(TransactionID) " +
							"FROM  tblTransactions " +
								"WHERE TransactionStatus = @TransactionStatus " +
								"AND TerminalNo = @TerminalNo ";
				if (CashierID != 0)
				{
					SQL += "AND CashierID = @CashierID ";
					MySqlParameter prmCashierID = new MySqlParameter("@CashierID",MySqlDbType.Int64);
					prmCashierID.Value = CashierID;
					cmd.Parameters.Add(prmCashierID);
				}
				if (BranchID != 0)
				{
					SQL += "AND BranchID = @BranchID ";
					MySqlParameter prmBranchID = new MySqlParameter("@BranchID",MySqlDbType.Int64);
					prmBranchID.Value = BranchID;
					cmd.Parameters.Add(prmBranchID);
				}


				SQL = "SELECT (" + SQL + ") AS TranCount";
				
				cmd.CommandType = System.Data.CommandType.Text;
				cmd.CommandText = SQL;

				MySqlParameter prmTransStatus = new MySqlParameter("@TransactionStatus",MySqlDbType.Int16);
				prmTransStatus.Value = TransactionStatus.Suspended.ToString("d");
				cmd.Parameters.Add(prmTransStatus);

				MySqlParameter prmTerminalNo = new MySqlParameter("@TerminalNo",MySqlDbType.String);
				prmTerminalNo.Value = TerminalNo;
				cmd.Parameters.Add(prmTerminalNo);

				MySqlDataReader myReader = base.ExecuteReader(cmd, System.Data.CommandBehavior.SingleResult);

				Int32 iCtr = 0;

				while (myReader.Read())
				{
					iCtr = myReader.GetInt32("TranCount");
				}
                myReader.Close();

				return iCtr;
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}

		public bool HasPendingTransaction(Int64 CashierID, string TerminalNo, out string TransactionNo)
		{
			try
			{
				string SQL = "SELECT " +
								"TransactionNo " +
							"FROM  tblTransactions " +
								"WHERE TransactionStatus = @TransactionStatus " +
								"AND CashierID = @CashierID " +
								"AND TerminalNo = @TerminalNo " +
								"ORDER BY TransactionDate ASC LIMIT 1";

				MySqlCommand cmd = new MySqlCommand();
				cmd.CommandType = System.Data.CommandType.Text;
				cmd.CommandText = SQL;

				MySqlParameter prmTransStatus = new MySqlParameter("@TransactionStatus",MySqlDbType.Int16);
				prmTransStatus.Value = TransactionStatus.Open.ToString("d");
				cmd.Parameters.Add(prmTransStatus);

				MySqlParameter prmCashierID = new MySqlParameter("@CashierID",MySqlDbType.Int64);
				prmCashierID.Value = CashierID;
				cmd.Parameters.Add(prmCashierID);

				MySqlParameter prmTerminalNo = new MySqlParameter("@TerminalNo",MySqlDbType.String);
				prmTerminalNo.Value = TerminalNo;
				cmd.Parameters.Add(prmTerminalNo);

				MySqlDataReader myReader = base.ExecuteReader(cmd, System.Data.CommandBehavior.SingleResult);

				TransactionNo = null;
				bool boRetVaue = false;

				while (myReader.Read())
				{
					boRetVaue = true;
					TransactionNo = "" + myReader["TransactionNo"].ToString();
				}
                myReader.Close();

				return boRetVaue;
			}
			catch (Exception ex)
			{
				throw ex;
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
		public void UpdateItem(Int64 TransactionID, decimal SubTotal, decimal ItemsDiscount, decimal Discount, decimal TransDiscount, DiscountTypes TransDiscountType, decimal VAT, decimal VatableAmount, decimal EVAT, decimal EVatableAmount, decimal LocalTax, string DiscountCode, string DiscountRemarks, decimal Charge, decimal ChargeAmount, string ChargeCode, string ChargeRemarks, SalesTransactionItemDetails SalesTransItemDetails)
		{
			UpdateSubTotal(TransactionID, SubTotal, ItemsDiscount, Discount, TransDiscount, TransDiscountType, VAT, VatableAmount, EVAT, EVatableAmount, LocalTax, DiscountCode, DiscountRemarks, Charge, ChargeAmount, ChargeCode, ChargeRemarks);

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

				string SQL = SQLSelect(clsSalesTransactionsColumns) + "WHERE TransactionStatus <> 0 ";

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
				if (clsSearchKeys.TransactionNo != string.Empty && clsSearchKeys.TransactionNo != null)
				{
					SQL += "AND tblTransactions.TransactionNo = @TransactionNo ";
					MySqlParameter prmTransactionNo = new MySqlParameter("@TransactionNo",MySqlDbType.String);
					prmTransactionNo.Value = clsSearchKeys.TransactionNo;
					cmd.Parameters.Add(prmTransactionNo);
				}

				if (clsSearchKeys.CustomerName != string.Empty && clsSearchKeys.CustomerName != null)
				{
					SQL += "AND tblTransactions.CustomerName = @CustomerName ";
					MySqlParameter prmCustomerName = new MySqlParameter("@CustomerName",MySqlDbType.String);
					prmCustomerName.Value = clsSearchKeys.CustomerName;
					cmd.Parameters.Add(prmCustomerName);
				}
				if (clsSearchKeys.CashierName != string.Empty && clsSearchKeys.CashierName != null)
				{
					SQL += "AND tblTransactions.CashierName = @CashierName ";
					MySqlParameter prmCashierName = new MySqlParameter("@CashierName",MySqlDbType.String);
					prmCashierName.Value = clsSearchKeys.CashierName;
					cmd.Parameters.Add(prmCashierName);
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
				throw ex;
			}
		}

        public MySqlDataReader TerminalReport(string CashierName, string TerminalNo)
		{
			try
			{
				MySqlCommand cmd = new MySqlCommand();
				cmd.CommandType = System.Data.CommandType.Text;

				string TableName = "tblTransactions";
				string SQL = "";

				for (int i = 1; i < 13; i++)
				{
					TableName = "tblTransactions" + i.ToString("0#");
					SQL += "UNION (SELECT " +
										"TransactionID, " +
										"TransactionNo, " +
										"PaxNo, " +
										"CustomerID, " +
										"CustomerName, " +
										"AgentID, " +
										"AgentName, " +
										"CashierID, " +
										"CashierName, " +
										"TerminalNo, " +
										"TransactionDate, " +
										"DateSuspended, " +
										"DateResumed, " +
										"TransactionStatus, " +
										"SubTotal, " +
										"ItemsDiscount, " +
										"Discount, " +
										"TransDiscount, " +
										"TransDiscountType, " +
										"VAT, " +
										"VatableAmount, " +
										"EVAT, " +
										"EVatableAmount, " +
										"LocalTax, " +
										"AmountPaid, " +
										"CashPayment, " +
										"ChequePayment, " +
										"CreditCardPayment, " +
										"CreditPayment, " +
										"DebitPayment, " +
										"RewardPointsPayment, " +
										"RewardConvertedPayment, " +
										"BalanceAmount, " +
										"ChangeAmount, " +
										"DateClosed, " +
										"PaymentType, " +
										"DiscountCode, " +
										"DiscountRemarks, " +
										"WaiterID, " +
										"WaiterName, " +
										"Charge, ChargeAmount, ChargeCode, ChargeRemarks, " +
										"AgentPositionName, AgentDepartmentName " +
									"FROM  " + TableName + " " +
									"WHERE 1=1 ";
					if (TerminalNo != null && TerminalNo != "")
					{
						SQL += "AND TerminalNo = TerminalNo ";
						MySqlParameter prmTerminalNo = new MySqlParameter("@TerminalNo",MySqlDbType.String);
						prmTerminalNo.Value = TerminalNo;
						cmd.Parameters.Add(prmTerminalNo);
					}
					if (CashierName != null && CashierName != "")
					{
						SQL += "AND CashierName LIKE @CashierName ";
						MySqlParameter prmCashierName = new MySqlParameter("@CashierName",MySqlDbType.String);
						prmCashierName.Value = CashierName;
						cmd.Parameters.Add(prmCashierName);
					}
					SQL += "AND (TransactionStatus = @TransactionStatusClosed " +
						"OR TransactionStatus = @TransactionStatusVoid " +
						"OR TransactionStatus = @TransactionStatusReprinted " +
						"OR TransactionStatus = @TransactionStatusRefund " +
						"OR TransactionStatus = @TransactionStatusCreditPayment) " +
						"AND TransactionDate >= (SELECT DateLastInitialized FROM tblTerminalReport " +
						"WHERE TerminalNo = @TerminalNo) ";
					SQL += ") ";
				}

				SQL = SQL.Remove(0, 6);
				SQL += "ORDER BY TransactionID DESC ";

				MySqlParameter prmTransactionStatusClosed = new MySqlParameter("@TransactionStatusClosed",MySqlDbType.Int16);
				prmTransactionStatusClosed.Value = (Int16)TransactionStatus.Closed;
				cmd.Parameters.Add(prmTransactionStatusClosed);

				MySqlParameter prmTransactionStatusVoid = new MySqlParameter("@TransactionStatusVoid",MySqlDbType.Int16);
				prmTransactionStatusVoid.Value = (Int16)TransactionStatus.Void;
				cmd.Parameters.Add(prmTransactionStatusVoid);

				MySqlParameter prmTransactionStatusReprinted = new MySqlParameter("@TransactionStatusReprinted",MySqlDbType.Int16);
				prmTransactionStatusReprinted.Value = (Int16)TransactionStatus.Reprinted;
				cmd.Parameters.Add(prmTransactionStatusReprinted);

				MySqlParameter prmTransactionStatusRefund = new MySqlParameter("@TransactionStatusRefund",MySqlDbType.Int16);
				prmTransactionStatusRefund.Value = (Int16)TransactionStatus.Refund;
				cmd.Parameters.Add(prmTransactionStatusRefund);

				MySqlParameter prmTransactionStatusCreditPayment = new MySqlParameter("@TransactionStatusCreditPayment",MySqlDbType.Int16);
				prmTransactionStatusCreditPayment.Value = (Int16)TransactionStatus.CreditPayment;
				cmd.Parameters.Add(prmTransactionStatusCreditPayment);

				cmd.CommandText = SQL;

				return base.ExecuteReader(cmd);
			}
			catch (Exception ex)
			{
				throw ex;
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

				MySqlParameter prmStartTransactionDate = new MySqlParameter("@StartDate",MySqlDbType.DateTime);
				prmStartTransactionDate.Value = StartTransactionDate.ToString("yyyy-MM-dd HH:mm:ss");
				cmd.Parameters.Add(prmStartTransactionDate);

				MySqlParameter prmEndTransactionDate = new MySqlParameter("@EndDate",MySqlDbType.DateTime);
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
				throw ex;
			}
		}

		public System.Data.DataTable SalesPerHour(DateTime? StartDateTimeOfTransaction = null, DateTime? UptoDateTimeOfTransaction = null, int BranchID = 0, string TerminalNo = Constants.ALL)
		{
			try
			{
				TerminalReport clsTerminalReport = new TerminalReport(base.Connection, base.Transaction);
				System.Data.DataTable dt = clsTerminalReport.HourlyReport(StartDateTimeOfTransaction, UptoDateTimeOfTransaction, BranchID, TerminalNo);

				return dt;
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}

		#endregion
	}
}
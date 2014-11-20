using System;
using System.Collections;
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
    public class SplitPaymentDetails
    {
        public Int32 PaxNo;
        public decimal AmountDue;
        public decimal AmountPaid;
        public decimal CashPayment;
        public decimal ChequePayment;
        public decimal CreditCardPayment;
        public decimal CreditPayment;
        public decimal DebitPayment;
        public decimal CreditChargeAmount;

        public decimal BalanceAmount;
        public decimal ChangeAmount;
        public PaymentTypes PaymentType;
        public ArrayList arrCashPaymentDetails;
        public ArrayList arrChequePaymentDetails;
        public ArrayList arrCreditCardPaymentDetails;
        public ArrayList arrCreditPaymentDetails;
        public ArrayList arrDebitPaymentDetails;
        public decimal RewardPointsPayment;
        public decimal RewardConvertedPayment;
        public Data.ContactDetails clsCreditorDetails;

    }

	[StrongNameIdentityPermissionAttribute(SecurityAction.LinkDemand,
		 PublicKey = "002400000480000094000000060200000024000" +
		 "052534131000400000100010053D785642F9F960B43157E0380" +
		 "F393BEE53E8DFAFBF441366C1B6F8B48D9DDF0D527B1F3B21EA" +
		 "E85D2FDB664CE81EB8A87DBE4C589D6F4202FE2B7D4B978BB69" +
		 "684874612CB9B8DB7A0339400A9C4E68277884B07817363D242" +
		 "E3696F9FACDBEA831810AE6DC9EDCA91A7B5DA12FE7BF65D113" +
		 "FF52834EAFB5A7A1FDFD5851A3")]
	public class SplitPayment : POSConnection
    {
		#region Constructors and Destructors

		public SplitPayment()
            : base(null, null)
        {
        }

        public SplitPayment(MySqlConnection Connection, MySqlTransaction Transaction) 
            : base(Connection, Transaction)
		{

		}

		#endregion

	}
}


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
	public struct PaymentDetails
	{
        public Int32 BranchID;
        public string TerminalNo;
		public Int64 TransactionID;
		public CashPaymentDetails[] arrCashPaymentDetails;
		public ChequePaymentDetails[] arrChequePaymentDetails;
		public CreditCardPaymentDetails[] arrCreditCardPaymentDetails;
		public CreditPaymentDetails[] arrCreditPaymentDetails;
		public DebitPaymentDetails[] arrDebitPaymentDetails;
	}

	[StrongNameIdentityPermissionAttribute(SecurityAction.LinkDemand,
		 PublicKey = "002400000480000094000000060200000024000" +
		 "052534131000400000100010053D785642F9F960B43157E0380" +
		 "F393BEE53E8DFAFBF441366C1B6F8B48D9DDF0D527B1F3B21EA" +
		 "E85D2FDB664CE81EB8A87DBE4C589D6F4202FE2B7D4B978BB69" +
		 "684874612CB9B8DB7A0339400A9C4E68277884B07817363D242" +
		 "E3696F9FACDBEA831810AE6DC9EDCA91A7B5DA12FE7BF65D113" +
		 "FF52834EAFB5A7A1FDFD5851A3")]
	public class Payment : POSConnection
    {
		#region Constructors and Destructors

		public Payment()
            : base(null, null)
        {
        }

        public Payment(MySqlConnection Connection, MySqlTransaction Transaction) 
            : base(Connection, Transaction)
		{

		}

		#endregion

		#region Insert and Update

		public void Insert(PaymentDetails Details)
		{
			try  
			{
				foreach (CashPaymentDetails cashpaymentdet in Details.arrCashPaymentDetails)
				{
                    new CashPayments(base.Connection, base.Transaction).Insert(cashpaymentdet);
				}
				foreach (ChequePaymentDetails chequepaymentdet in Details.arrChequePaymentDetails)
				{
                    new ChequePayments(base.Connection, base.Transaction).Insert(chequepaymentdet);
				}
				foreach (CreditCardPaymentDetails cardpaymentdet in Details.arrCreditCardPaymentDetails)
				{
                    new CreditCardPayments(base.Connection, base.Transaction).Insert(cardpaymentdet);
				}
				foreach (CreditPaymentDetails creditpaymentdet in Details.arrCreditPaymentDetails)
				{
                    new CreditPayments(base.Connection, base.Transaction).Insert(creditpaymentdet); 
				}
				foreach (DebitPaymentDetails debitpaymentdet in Details.arrDebitPaymentDetails)
				{
                    new DebitPayments(base.Connection, base.Transaction).Insert(debitpaymentdet); 
				}
			}
			catch (Exception ex)
			{
				throw base.ThrowException(ex);
			}	
		}

        //public void InsertDebitPayment(DebitPaymentDetails Details)
        //{
        //    try
        //    {
        //        string SQL = "CALL procDebitPaymentInsert(@BranchID, @TerminalNo, @TransactionID, @TransactionNo, @Amount, @ContactID, @Remarks);";

        //        MySqlCommand cmd = new MySqlCommand();
        //        cmd.CommandType = System.Data.CommandType.Text;
        //        cmd.CommandText = SQL;

        //        cmd.Parameters.AddWithValue("@BranchID", Details.BranchDetails.BranchID);
        //        cmd.Parameters.AddWithValue("@TerminalNo", Details.TerminalNo);
        //        cmd.Parameters.AddWithValue("@TransactionID", Details.TransactionID);
        //        cmd.Parameters.AddWithValue("@TransactionNo", Details.TransactionNo);
        //        cmd.Parameters.AddWithValue("@Amount", Details.Amount);
        //        cmd.Parameters.AddWithValue("@ContactID", Details.CustomerDetails.ContactID);
        //        cmd.Parameters.AddWithValue("@Remarks", Details.Remarks);

        //        base.ExecuteNonQuery(cmd);

        //        Contacts clsContact = new Contacts(base.Connection, base.Transaction);
        //        clsContact.SubtractDebit(Details.CustomerDetails.ContactID, Details.Amount);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw base.ThrowException(ex);
        //    }
        //}

        /// <summary>
        /// Update CreditPayment, when paying credits.
        /// Use CreditPaymentID as reference.
        /// </summary>
        /// <param name="BranchID"></param>
        /// <param name="TerminalNo"></param>
        /// <param name="ContactID"></param>
        /// <param name="CreditPaymentID"></param>
        /// <param name="AmountPaid"></param>
        /// <param name="Remarks"></param>
        /// <param name="ActivateSuspendedAccount"></param>
        public void UpdateCredit(Int32 BranchID, string TerminalNo, Int64 ContactID, long CreditPaymentID, decimal AmountPaid, string Remarks, bool ActivateSuspendedAccount = true)
		{
			try 
			{
                string SQL = "CALL procCreditPaymentUpdateCredit(@BranchID, @TerminalNo, @CreditPaymentID, @Amount, @Remarks);";
				  
				MySqlCommand cmd = new MySqlCommand();
				cmd.CommandType = System.Data.CommandType.Text;
				cmd.CommandText = SQL;

                cmd.Parameters.AddWithValue("@BranchID", BranchID);
                cmd.Parameters.AddWithValue("@TerminalNo", TerminalNo);
                cmd.Parameters.AddWithValue("@CreditPaymentID", CreditPaymentID);
                cmd.Parameters.AddWithValue("@Amount", AmountPaid);
                cmd.Parameters.AddWithValue("@Remarks", Remarks);

				base.ExecuteNonQuery(cmd);

                Contacts clsContact = new Contacts(base.Connection, base.Transaction);
                clsContact.SubtractCredit(ContactID, AmountPaid, ActivateSuspendedAccount);
			}
			catch (Exception ex)
			{
				throw base.ThrowException(ex);
			}	
		}
        //public void UpdateDebit(Int32 BranchID, string TerminalNo, Int64 ContactID, Int64 CreditPaymentID, decimal AmountPaid, string Remarks)
        //{
        //    try 
        //    {
        //        string SQL = "CALL procDebitPaymentUpdateDebit(@BranchID, @TerminalNo, @CreditPaymentID, @Amount, @Remarks);";

        //        MySqlCommand cmd = new MySqlCommand();
        //        cmd.CommandType = System.Data.CommandType.Text;
        //        cmd.CommandText = SQL;

        //        cmd.Parameters.AddWithValue("@BranchID", BranchID);
        //        cmd.Parameters.AddWithValue("@TerminalNo", TerminalNo);
        //        cmd.Parameters.AddWithValue("@CreditPaymentID", CreditPaymentID);
        //        cmd.Parameters.AddWithValue("@Amount", AmountPaid);
        //        cmd.Parameters.AddWithValue("@Remarks", Remarks);

        //        base.ExecuteNonQuery(cmd);

        //        Contacts clsContact = new Contacts(base.Connection, base.Transaction);
        //        clsContact.SubtractDebit(ContactID, AmountPaid);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw base.ThrowException(ex);
        //    }	
        //}
		
		#endregion

		#region Delete

		public bool Delete(string IDs)
		{
			try 
			{
				string SQL=	"DELETE FROM tblPayment WHERE PaymentID IN (" + IDs + ");";
	 			
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

		#region Details

		public PaymentDetails Details(Int32 BranchID, string TerminalNo, Int64 TransactionID)
		{
			try
			{
				PaymentDetails Details = new PaymentDetails();
				Details.TransactionID = TransactionID;
                Details.arrCashPaymentDetails = new CashPayments(base.Connection, base.Transaction).Details(BranchID, TerminalNo, TransactionID);
                Details.arrChequePaymentDetails = new ChequePayments(base.Connection, base.Transaction).Details(BranchID, TerminalNo, TransactionID);
                Details.arrCreditCardPaymentDetails = new CreditCardPayments(base.Connection, base.Transaction).Details(BranchID, TerminalNo, TransactionID);
                Details.arrCreditPaymentDetails = new CreditPayments(base.Connection, base.Transaction).Details(BranchID, TerminalNo, TransactionID);
                Details.arrDebitPaymentDetails = new DebitPayments(base.Connection, base.Transaction).Details(BranchID, TerminalNo, TransactionID);

				return Details;
			}

			catch (Exception ex)
			{
				throw base.ThrowException(ex);
			}	
		}

		#endregion
		
		#region Streams


		#endregion
	}
}


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
    public struct PaymentCreditDetails
	{
        public Int32 BranchID;
        public string TerminalNo;
        public Int64 TransactionID;
        public CreditPaymentCashDetails[] arrCreditPaymentCashDetails;
        public CreditPaymentChequeDetails[] arrCreditPaymentChequeDetails;
	}

	[StrongNameIdentityPermissionAttribute(SecurityAction.LinkDemand,
		 PublicKey = "002400000480000094000000060200000024000" +
		 "052534131000400000100010053D785642F9F960B43157E0380" +
		 "F393BEE53E8DFAFBF441366C1B6F8B48D9DDF0D527B1F3B21EA" +
		 "E85D2FDB664CE81EB8A87DBE4C589D6F4202FE2B7D4B978BB69" +
		 "684874612CB9B8DB7A0339400A9C4E68277884B07817363D242" +
		 "E3696F9FACDBEA831810AE6DC9EDCA91A7B5DA12FE7BF65D113" +
		 "FF52834EAFB5A7A1FDFD5851A3")]
	public class PaymentCredits : POSConnection
    {
		#region Constructors and Destructors

		public PaymentCredits()
            : base(null, null)
        {
        }

        public PaymentCredits(MySqlConnection Connection, MySqlTransaction Transaction) 
            : base(Connection, Transaction)
		{

		}

		#endregion

		#region Insert and Update

		public void Insert(PaymentCreditDetails Details)
		{
			try  
			{
                foreach (CreditPaymentCashDetails cashCreditPaymentdet in Details.arrCreditPaymentCashDetails)
				{
                    new CreditPaymentCash(base.Connection, base.Transaction).Insert(cashCreditPaymentdet);
				}
                foreach (CreditPaymentChequeDetails chequeCreditPaymentdet in Details.arrCreditPaymentChequeDetails)
				{
                    new CreditPaymentCheque(base.Connection, base.Transaction).Insert(chequeCreditPaymentdet);
				}
			}
			catch (Exception ex)
			{
				throw base.ThrowException(ex);
			}	
		}

        public void UpdateCredit(long pvtlngContactID, long pvtlngTransactionID, string pvtstrTransactionNo, decimal pvtdecAmountPaid, string pvtstrRemarks)
		{
			try 
			{
                string SQL = "CALL procCreditCreditPaymentUpdateCredit(@TransactionID, @TransactionNo, @Amount, @Remarks);";
				  
				MySqlCommand cmd = new MySqlCommand();
				cmd.CommandType = System.Data.CommandType.Text;
				cmd.CommandText = SQL;
				
                cmd.Parameters.AddWithValue("@TransactionID", pvtlngTransactionID);
                cmd.Parameters.AddWithValue("@TransactionNo", pvtstrTransactionNo);
                cmd.Parameters.AddWithValue("@Amount", pvtdecAmountPaid);
                cmd.Parameters.AddWithValue("@Remarks", pvtstrRemarks);

				base.ExecuteNonQuery(cmd);

                Contacts clsContact = new Contacts(base.Connection, base.Transaction);
                clsContact.SubtractCredit(pvtlngContactID, pvtdecAmountPaid);
			}
			catch (Exception ex)
			{
				throw base.ThrowException(ex);
			}	
		}
        public void UpdateDebit(long pvtlngContactID, long pvtlngTransactionID, string pvtstrTransactionNo, decimal pvtdecAmountPaid, string pvtstrRemarks)
		{
			try 
			{
                string SQL = "CALL procDebitCreditPaymentUpdateDebit(@TransactionID, @TransactionNo, @Amount, @Remarks);";

                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandText = SQL;

                cmd.Parameters.AddWithValue("@TransactionID", pvtlngTransactionID);
                cmd.Parameters.AddWithValue("@TransactionNo", pvtstrTransactionNo);
                cmd.Parameters.AddWithValue("@Amount", pvtdecAmountPaid);

				base.ExecuteNonQuery(cmd);

				Contacts clsContact = new Contacts(base.Connection, base.Transaction);
                clsContact.SubtractDebit(pvtlngContactID, pvtdecAmountPaid);
			}

			catch (Exception ex)
			{
				throw base.ThrowException(ex);
			}	
		}
		
		#endregion

		#region Delete



		#endregion

		#region Details

        public PaymentCreditDetails Details(Int32 BranchID, string TerminalNo, Int64 TransactionID)
		{
			try
			{
                PaymentCreditDetails Details = new PaymentCreditDetails();
                Details.BranchID = BranchID;
                Details.TerminalNo = TerminalNo;
                Details.TransactionID = TransactionID;
                Details.arrCreditPaymentCashDetails = new CreditPaymentCash(base.Connection, base.Transaction).Details(BranchID, TerminalNo, TransactionID);
                Details.arrCreditPaymentChequeDetails = new CreditPaymentCheque(base.Connection, base.Transaction).Details(BranchID, TerminalNo, TransactionID);

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


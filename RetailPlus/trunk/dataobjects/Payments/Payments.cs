using System;
using System.Collections;
using System.Security.Permissions;
using MySql.Data.MySqlClient;

namespace AceSoft.RetailPlus.Data
{
	public struct DebitPaymentDetails
	{
		public Int64 TransactionID;
        public bool IsRefund;
        public string TransactionNo;
		public decimal Amount;
        public ContactDetails CustomerDetails;
		public string Remarks;
	}

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
                    InsertDebitPayment(debitpaymentdet);
				}
			}

			catch (Exception ex)
			{
				throw base.ThrowException(ex);
			}	
		}

        public void InsertDebitPayment(DebitPaymentDetails Details)
        {
            try
            {
                string SQL = "CALL procDebitPaymentInsert(@TransactionID, @TransactionNo, @Amount, @ContactID, @Remarks);";

                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandText = SQL;

                cmd.Parameters.AddWithValue("@TransactionID", Details.TransactionID);
                cmd.Parameters.AddWithValue("@TransactionNo", Details.TransactionNo);
                cmd.Parameters.AddWithValue("@Amount", Details.Amount);
                cmd.Parameters.AddWithValue("@ContactID", Details.CustomerDetails.ContactID);
                cmd.Parameters.AddWithValue("@Remarks", Details.Remarks);

                base.ExecuteNonQuery(cmd);

                Contacts clsContact = new Contacts(base.Connection, base.Transaction);
                clsContact.SubtractDebit(Details.CustomerDetails.ContactID, Details.Amount);

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
                string SQL = "CALL procCreditPaymentUpdateCredit(@TransactionID, @TransactionNo, @Amount, @Remarks);";
				  
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
                string SQL = "CALL procDebitPaymentUpdateDebit(@TransactionID, @TransactionNo, @Amount, @Remarks);";

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
                Details.arrDebitPaymentDetails = arrDebitPaymentDetails(TransactionID); 

				return Details;
			}

			catch (Exception ex)
			{
				throw base.ThrowException(ex);
			}	
		}

		public DebitPaymentDetails[] arrDebitPaymentDetails(Int64 TransactionID)
		{
			try
			{
				string SQL = "SELECT " +
								"TransactionID, " +
								"Amount, " +
								"ContactID, " +
								"Remarks " +
							"FROM tblDebitPayment " +
							"WHERE TransactionID = @TransactionID;";
				  
				MySqlCommand cmd = new MySqlCommand();
				cmd.CommandType = System.Data.CommandType.Text;
				cmd.CommandText = SQL;

				MySqlParameter prmTransactionID = new MySqlParameter("@TransactionID",MySqlDbType.Int64);			
				prmTransactionID.Value = TransactionID;
				cmd.Parameters.Add(prmTransactionID);

				MySqlDataReader myReader = base.ExecuteReader(cmd, System.Data.CommandBehavior.SingleResult);
				
				ArrayList items = new ArrayList();
                Customer clsCustomer = new Customer(base.Connection, base.Transaction);

				while (myReader.Read()) 
				{
					Data.DebitPaymentDetails Details = new Data.DebitPaymentDetails();

					Details.TransactionID = myReader.GetInt64("TransactionID");
					
					Details.Amount = myReader.GetDecimal("Amount");
                    Details.CustomerDetails = clsCustomer.Details(myReader.GetInt64("ContactID"));
					Details.Remarks = "" + myReader["Remarks"].ToString();

					items.Add(Details);
				}

				myReader.Close();

				DebitPaymentDetails[] arrDebitDetails = new DebitPaymentDetails[0];

				if (items != null)
				{
					arrDebitDetails = new DebitPaymentDetails[items.Count];
					items.CopyTo(arrDebitDetails);
				}

				return arrDebitDetails;
			}

			catch (Exception ex)
			{
				throw base.ThrowException(ex);
			}	
		}

		
		#endregion
		
		#region Streams

        //public MySqlDataReader List(Int64 TransactionID, string SortField, SortOption SortOrder)
        //{
        //    try
        //    {
        //        string SQL = "SELECT " +
        //            "PaymentID, " +
        //            "TransactionID, " +
        //            "SubTotal, " +
        //            "Discount, " +
        //            "AmountPaid, " +
        //            "Balance, " +
        //            "Change, " +
        //            "DatePaid " +
        //            "FROM tblPayment " +
        //            "WHERE 1=1 AND TransactionID = @TransactionID ORDER BY " + SortField; 

        //        if (SortOrder == SortOption.Ascending)
        //            SQL += " ASC";
        //        else
        //            SQL += " DESC";

        //        MySqlCommand cmd = new MySqlCommand();
        //        cmd.CommandType = System.Data.CommandType.Text;
        //        cmd.CommandText = SQL;
				
        //        MySqlParameter prmTransactionID = new MySqlParameter("@TransactionID",MySqlDbType.Decimal);			
        //        prmTransactionID.Value = TransactionID;
        //        cmd.Parameters.Add(prmTransactionID);

				
				
        //        return base.ExecuteReader(cmd);			
        //    }
        //    catch (Exception ex)
        //    {
        //        throw base.ThrowException(ex);
        //    }	
        //}


		#endregion
	}
}


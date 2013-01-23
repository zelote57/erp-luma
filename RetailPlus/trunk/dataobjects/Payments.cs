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
	public struct CashPaymentDetails
	{
		public Int64 TransactionID;
        public string TransactionNo;
		public decimal Amount;
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
	public struct ChequePaymentDetails
	{
		public Int64 TransactionID;
        public string TransactionNo;
		public string ChequeNo;
		public decimal Amount;
		public DateTime ValidityDate;
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
	public struct CreditCardPaymentDetails
	{
		public Int64 TransactionID;
        public string TransactionNo;
		public decimal Amount;
		public Int16 CardTypeID;
		public string CardTypeCode;
		public string CardTypeName;
		public string CardNo;
		public string CardHolder;
		public string ValidityDates;
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
	public struct CreditPaymentDetails
	{
		public Int64 TransactionID;
        public string TerminalNo;
        public string TransactionNo;
        public DateTime TransactionDate;
        public string CashierName;
		public decimal Amount;
        public ContactDetails CustomerDetails;
		public string Remarks;
	}

	public struct DebitPaymentDetails
	{
		public Int64 TransactionID;
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
		public CreditCardPaymentDetails[] arrCardPaymentDetails;
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
                    InsertCashPayment(cashpaymentdet);
				}
				foreach (ChequePaymentDetails chequepaymentdet in Details.arrChequePaymentDetails)
				{
                    InsertChequePayment(chequepaymentdet);
				}
				foreach (CreditCardPaymentDetails cardpaymentdet in Details.arrCardPaymentDetails)
				{
                    InsertCreditCardPayment(cardpaymentdet);
				}
				foreach (CreditPaymentDetails creditpaymentdet in Details.arrCreditPaymentDetails)
				{
                    InsertCreditPayment(creditpaymentdet);
				}
				foreach (DebitPaymentDetails debitpaymentdet in Details.arrDebitPaymentDetails)
				{
                    InsertDebitPayment(debitpaymentdet);
				}
			}

			catch (Exception ex)
			{
				
				
				{
					
					
					
					
				}

				throw ex;
			}	
		}

		public void InsertCashPayment(CashPaymentDetails Details)
		{
			try  
			{
                string SQL = "CALL procCashPaymentInsert(@TransactionID, @TransactionNo, @Amount, @Remarks);";
				  
				
	 			
				MySqlCommand cmd = new MySqlCommand();
				
				
				cmd.CommandType = System.Data.CommandType.Text;
				cmd.CommandText = SQL;
				
                cmd.Parameters.AddWithValue("@TransactionID", Details.TransactionID);
                cmd.Parameters.AddWithValue("@TransactionNo", Details.TransactionNo);
                cmd.Parameters.AddWithValue("@Amount", Details.Amount);
                cmd.Parameters.AddWithValue("@Remarks", Details.Remarks);

				base.ExecuteNonQuery(cmd);
			}

			catch (Exception ex)
			{
				
				
				{
					
					
					
					
				}

				throw ex;
			}	
		}
		public void InsertChequePayment(ChequePaymentDetails Details)
		{
			try  
			{
                string SQL = "CALL procChequePaymentInsert(@TransactionID, @TransactionNo, @ChequeNo, @Amount, @ValidityDate, @Remarks);";
				  
				
	 			
				MySqlCommand cmd = new MySqlCommand();
				
				
				cmd.CommandType = System.Data.CommandType.Text;
				cmd.CommandText = SQL;
				
                cmd.Parameters.AddWithValue("@TransactionID", Details.TransactionID);
                cmd.Parameters.AddWithValue("@TransactionNo", Details.TransactionNo);
				cmd.Parameters.AddWithValue("@ChequeNo", Details.ChequeNo);
                cmd.Parameters.AddWithValue("@Amount", Details.Amount);
                cmd.Parameters.AddWithValue("@ValidityDate", Details.ValidityDate.ToString("yyyy-MM-dd HH:mm:ss"));
                cmd.Parameters.AddWithValue("@Remarks", Details.Remarks);

				base.ExecuteNonQuery(cmd);
			}

			catch (Exception ex)
			{
				
				
				{
					
					
					
					
				}

				throw ex;
			}	
		}
		public void InsertCreditCardPayment(CreditCardPaymentDetails Details)
		{
			try  
			{
                string SQL = "CALL procCreditCardPaymentInsert(@TransactionID, @TransactionNo, @Amount, @CardTypeID, @CardTypeCode, @CardTypeName, @CardNo, @CardHolder," +
                                                                "@ValidityDates, @Remarks);";

                

                MySqlCommand cmd = new MySqlCommand();
                
                
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandText = SQL;

                cmd.Parameters.AddWithValue("@TransactionID", Details.TransactionID);
                cmd.Parameters.AddWithValue("@TransactionNo", Details.TransactionNo);
                cmd.Parameters.AddWithValue("@Amount", Details.Amount);
                cmd.Parameters.AddWithValue("@CardTypeID", Details.CardTypeID);
                cmd.Parameters.AddWithValue("@CardTypeCode", Details.CardTypeCode);
                cmd.Parameters.AddWithValue("@CardTypeName", Details.CardTypeName);
                cmd.Parameters.AddWithValue("@CardNo", Details.CardNo);
                cmd.Parameters.AddWithValue("@CardHolder", Details.CardHolder);
                cmd.Parameters.AddWithValue("@ValidityDates", Details.ValidityDates);
                cmd.Parameters.AddWithValue("@Remarks", Details.Remarks);

				base.ExecuteNonQuery(cmd);
			}

			catch (Exception ex)
			{
				
				
				{
					
					
					
					
				}

				throw ex;
			}	
		}
		public void InsertCreditPayment(CreditPaymentDetails Details)
		{
			try  
			{
                //string SQL = "CALL procCreditPaymentInsert(@TransactionID, @TransactionNo, @Amount, @ContactID, @Remarks);";

                //
	 			
                //MySqlCommand cmd = new MySqlCommand();
                //
                //
                //cmd.CommandType = System.Data.CommandType.Text;
                //cmd.CommandText = SQL;

                //cmd.Parameters.AddWithValue("@TransactionID", Details.TransactionID);
                //cmd.Parameters.AddWithValue("@TransactionNo", Details.TransactionNo);
                //cmd.Parameters.AddWithValue("@Amount", Details.Amount);
                //cmd.Parameters.AddWithValue("@ContactID", Details.CustomerDetails.ContactID);
                //cmd.Parameters.AddWithValue("@Remarks", Details.Remarks);

                //base.ExecuteNonQuery(cmd);


                // [04/03/2012] Added creditcard information as requested by HP
                string SQL = "CALL procCreditPaymentInsert(@TransactionID, @pvtCustomerID, @pvtGuarantorID, @pvtCreditType, @pvtCreditExpiryDate, @pvtCurrentCredit, @pvtAmount, @pvtTerminalNo, @pvtTransactionDate, @pvtTransactionNo, @pvtCashierName, @pvtRemarks);";

                

                MySqlCommand cmd = new MySqlCommand();
                cmd = new MySqlCommand();
                
                
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandText = SQL;

                cmd.Parameters.AddWithValue("@TransactionID", Details.TransactionID);
                cmd.Parameters.AddWithValue("@pvtCustomerID", Details.CustomerDetails.ContactID);
                cmd.Parameters.AddWithValue("@pvtGuarantorID", Details.CustomerDetails.CreditDetails.GuarantorID);
                cmd.Parameters.AddWithValue("@pvtCreditType", Details.CustomerDetails.CreditDetails.CreditType.ToString("D"));
                cmd.Parameters.AddWithValue("@pvtCreditExpiryDate", Details.CustomerDetails.CreditDetails.ExpiryDate.ToString("yyyy-MM-dd HH:mm:ss"));
                cmd.Parameters.AddWithValue("@pvtCurrentCredit", Details.CustomerDetails.Credit);
                cmd.Parameters.AddWithValue("@pvtAmount", Details.Amount);
                cmd.Parameters.AddWithValue("@pvtTerminalNo", Details.TerminalNo);
                cmd.Parameters.AddWithValue("@pvtTransactionDate", Details.TransactionDate.ToString("yyyy-MM-dd HH:mm:ss"));
                cmd.Parameters.AddWithValue("@pvtTransactionNo", Details.TransactionNo);
                cmd.Parameters.AddWithValue("@pvtCashierName", Details.CashierName);
                cmd.Parameters.AddWithValue("@pvtRemarks", Details.Remarks);

                base.ExecuteNonQuery(cmd);

                //add credit to masterfile
				Contacts clsContact = new Contacts(base.Connection, base.Transaction);
				clsContact.AddCredit(Details.CustomerDetails.ContactID, Details.Amount);

			}

			catch (Exception ex)
			{
				
				
				{
					
					
					
					
				}

				throw ex;
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
                
                
                {
                    
                    
                    
                    
                }

                throw ex;
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
				
				
				{
					
					
					
					
				}

				throw ex;
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
				
				
				{
					
					
					
					
				}

				throw ex;
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
				
				
				{
					
					
					
					
				}

				throw ex;
			}	
		}


		#endregion

		#region Details

		public PaymentDetails Details(Int64 TransactionID)
		{
			try
			{
				PaymentDetails Details = new PaymentDetails();
				Details.TransactionID = TransactionID;
				Details.arrCashPaymentDetails	= arrCashPaymentDetails(TransactionID);
				Details.arrChequePaymentDetails = arrChequePaymentDetails(TransactionID);
				Details.arrCardPaymentDetails	= arrCreditCardPaymentDetails(TransactionID);
				Details.arrCreditPaymentDetails	= arrCreditPaymentDetails(TransactionID);

				return Details;
			}

			catch (Exception ex)
			{
				
				
				{
					
					
					
					
				}

				throw ex;
			}	
		}
		public CashPaymentDetails[] arrCashPaymentDetails(Int64 TransactionID)
		{
			try
			{
				string SQL = "SELECT " +
								"TransactionID, " +
								"Amount, " +
								"Remarks " +
							"FROM tblCashPayment " +
							"WHERE TransactionID = @TransactionID;";
				  
				
	 			
				MySqlCommand cmd = new MySqlCommand();
				
				
				cmd.CommandType = System.Data.CommandType.Text;
				cmd.CommandText = SQL;

				MySqlParameter prmTransactionID = new MySqlParameter("@TransactionID",MySqlDbType.Int64);			
				prmTransactionID.Value = TransactionID;
				cmd.Parameters.Add(prmTransactionID);

				MySqlDataReader myReader = base.ExecuteReader(cmd, System.Data.CommandBehavior.SingleResult);
				
				ArrayList items = new ArrayList();

				while (myReader.Read()) 
				{
					Data.CashPaymentDetails Details = new Data.CashPaymentDetails();

					Details.TransactionID = myReader.GetInt64("TransactionID");
					Details.Amount = myReader.GetDecimal("Amount");
					Details.Remarks = "" + myReader["Remarks"].ToString();

					items.Add(Details);
				}

				myReader.Close();

				CashPaymentDetails[] arrCashDetails = new CashPaymentDetails[0];

				if (items != null)
				{
					arrCashDetails = new CashPaymentDetails[items.Count];
					items.CopyTo(arrCashDetails);
				}

				return arrCashDetails;
			}

			catch (Exception ex)
			{
				
				
				{
					
					
					
					
				}

				throw ex;
			}	
		}
		public ChequePaymentDetails[] arrChequePaymentDetails(Int64 TransactionID)
		{
			try
			{
				string SQL = "SELECT " +
								"TransactionID, " +
								"ChequeNo, " +
								"Amount, " +
								"ValidityDate, " +
								"Remarks " +
							"FROM tblChequePayment " +
							"WHERE TransactionID = @TransactionID;";
				  
				
	 			
				MySqlCommand cmd = new MySqlCommand();
				
				
				cmd.CommandType = System.Data.CommandType.Text;
				cmd.CommandText = SQL;

				MySqlParameter prmTransactionID = new MySqlParameter("@TransactionID",MySqlDbType.Int64);			
				prmTransactionID.Value = TransactionID;
				cmd.Parameters.Add(prmTransactionID);

				MySqlDataReader myReader = base.ExecuteReader(cmd, System.Data.CommandBehavior.SingleResult);
				
				ArrayList items = new ArrayList();;

				while (myReader.Read()) 
				{
					Data.ChequePaymentDetails Details = new Data.ChequePaymentDetails();

					Details.TransactionID = myReader.GetInt64("TransactionID");
					Details.ChequeNo = "" + myReader["ChequeNo"].ToString();
					Details.Amount = myReader.GetDecimal("Amount");
					Details.ValidityDate = myReader.GetDateTime("ValidityDate");
					Details.Remarks = "" + myReader["Remarks"].ToString();

					items.Add(Details);
				}

				myReader.Close();

				ChequePaymentDetails[] arrChequeDetails = new ChequePaymentDetails[0];

				if (items != null)
				{
					arrChequeDetails = new ChequePaymentDetails[items.Count];
					items.CopyTo(arrChequeDetails);
				}

				return arrChequeDetails;
			}

			catch (Exception ex)
			{
				
				
				{
					
					
					
					
				}

				throw ex;
			}	
		}
		public CreditCardPaymentDetails[] arrCreditCardPaymentDetails(Int64 TransactionID)
		{
			try
			{
				string SQL = "SELECT " +
								"TransactionID, " +
								"Amount, " +
								"CardTypeID, " +
								"CardTypeCode, " +
								"CardTypeName, " +
								"CardNo, " +
								"CardHolder, " +
								"ValidityDates, " +
								"Remarks " +
							"FROM tblCreditCardPayment " +
							"WHERE TransactionID = @TransactionID;";
				  
				
	 			
				MySqlCommand cmd = new MySqlCommand();
				
				
				cmd.CommandType = System.Data.CommandType.Text;
				cmd.CommandText = SQL;

				MySqlParameter prmTransactionID = new MySqlParameter("@TransactionID",MySqlDbType.Int64);			
				prmTransactionID.Value = TransactionID;
				cmd.Parameters.Add(prmTransactionID);

				MySqlDataReader myReader = base.ExecuteReader(cmd, System.Data.CommandBehavior.SingleResult);
				
				ArrayList items = new ArrayList();;

				while (myReader.Read()) 
				{
					Data.CreditCardPaymentDetails Details = new Data.CreditCardPaymentDetails();

					Details.TransactionID = myReader.GetInt64("TransactionID");
					
					Details.Amount = myReader.GetDecimal("Amount");
					Details.CardTypeID = myReader.GetInt16("CardTypeID");
					Details.CardTypeCode = "" + myReader["CardTypeCode"].ToString();
					Details.CardTypeName = "" + myReader["CardTypeName"].ToString();
					Details.CardNo = "" + myReader["CardNo"].ToString();
					Details.CardHolder = "" + myReader["CardHolder"].ToString();
					Details.ValidityDates = "" + myReader["ValidityDates"].ToString();
					Details.Remarks = "" + myReader["Remarks"].ToString();

					items.Add(Details);
				}

				myReader.Close();

				CreditCardPaymentDetails[] arrCardDetails = new CreditCardPaymentDetails[0];

				if (items != null)
				{
					arrCardDetails = new CreditCardPaymentDetails[items.Count];
					items.CopyTo(arrCardDetails);
				}

				return arrCardDetails;
			}

			catch (Exception ex)
			{
				
				
				{
					
					
					
					
				}

				throw ex;
			}	
		}
		public CreditPaymentDetails[] arrCreditPaymentDetails(Int64 TransactionID)
		{
			try
			{
				string SQL = "SELECT " +
								"TransactionID, " +
								"Amount, " +
								"ContactID, " +
								"Remarks " +
							"FROM tblCreditPayment " +
							"WHERE TransactionID = @TransactionID;";
				  
				
	 			
				MySqlCommand cmd = new MySqlCommand();
				
				
				cmd.CommandType = System.Data.CommandType.Text;
				cmd.CommandText = SQL;

				MySqlParameter prmTransactionID = new MySqlParameter("@TransactionID",MySqlDbType.Int64);			
				prmTransactionID.Value = TransactionID;
				cmd.Parameters.Add(prmTransactionID);

				MySqlDataReader myReader = base.ExecuteReader(cmd, System.Data.CommandBehavior.SingleResult);
				
				ArrayList items = new ArrayList();;
                
                Customer clsCustomer = new Customer(base.Connection, base.Transaction);

				while (myReader.Read()) 
				{
					Data.CreditPaymentDetails Details = new Data.CreditPaymentDetails();

					Details.TransactionID = myReader.GetInt64("TransactionID");
					Details.Amount = myReader.GetDecimal("Amount");
                    Details.CustomerDetails = clsCustomer.Details(myReader.GetInt64("ContactID"));
					Details.Remarks = "" + myReader["Remarks"].ToString();

					items.Add(Details);
				}
				myReader.Close();

				CreditPaymentDetails[] arrCreditDetails = new CreditPaymentDetails[0];

				if (items != null)
				{
					arrCreditDetails = new CreditPaymentDetails[items.Count];
					items.CopyTo(arrCreditDetails);
				}

				return arrCreditDetails;
			}

			catch (Exception ex)
			{
				
				
				{
					
					
					
					
				}

				throw ex;
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
				throw ex;
			}	
		}

		
		#endregion
		
		#region Streams

		public MySqlDataReader List(Int64 TransactionID, string SortField, SortOption SortOrder)
		{
			try
			{
				string SQL = "SELECT " +
					"PaymentID, " +
					"TransactionID, " +
					"SubTotal, " +
					"Discount, " +
					"AmountPaid, " +
					"Balance, " +
					"Change, " +
					"DatePaid " +
					"FROM tblPayment " +
					"WHERE 1=1 AND TransactionID = @TransactionID ORDER BY " + SortField; 

				if (SortOrder == SortOption.Ascending)
					SQL += " ASC";
				else
					SQL += " DESC";

				

				MySqlCommand cmd = new MySqlCommand();
				
				
				cmd.CommandType = System.Data.CommandType.Text;
				cmd.CommandText = SQL;
				
				MySqlParameter prmTransactionID = new MySqlParameter("@TransactionID",MySqlDbType.Decimal);			
				prmTransactionID.Value = TransactionID;
				cmd.Parameters.Add(prmTransactionID);

				
				
				return base.ExecuteReader(cmd);			
			}
			catch (Exception ex)
			{
				
				
				{
					
					
					
					
				}

				throw ex;
			}	
		}


		#endregion
	}
}


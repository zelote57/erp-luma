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
	public struct BasePaymentsDetails
	{
		public Int64 BasePaymentsID;
		public Int64 TransactionID;
		public decimal SubTotal;
		public decimal Discount;
		public decimal AmountPaid;
		public decimal Balance;
		public decimal Change;
		public DateTime DatePaid;
	}

	[StrongNameIdentityPermissionAttribute(SecurityAction.LinkDemand,
		 PublicKey = "002400000480000094000000060200000024000" +
		 "052534131000400000100010053D785642F9F960B43157E0380" +
		 "F393BEE53E8DFAFBF441366C1B6F8B48D9DDF0D527B1F3B21EA" +
		 "E85D2FDB664CE81EB8A87DBE4C589D6F4202FE2B7D4B978BB69" +
		 "684874612CB9B8DB7A0339400A9C4E68277884B07817363D242" +
		 "E3696F9FACDBEA831810AE6DC9EDCA91A7B5DA12FE7BF65D113" +
		 "FF52834EAFB5A7A1FDFD5851A3")]
	public class BasePayments : POSConnection
    {
		#region Constructors and Destructors

		public BasePayments()
            : base(null, null)
        {
        }

        public BasePayments(MySqlConnection Connection, MySqlTransaction Transaction) 
            : base(Connection, Transaction)
		{

		}

		#endregion
		
		#region Insert and Update

		public Int64 Insert(BasePaymentsDetails Details)
		{
			try  
			{
				string SQL="INSERT INTO tblBasePayments (" +
					"TransactionID, SubTotal, Discount, AmountPaid," +
					"Balance, Change, DatePaid) VALUES (" +
					"@TransactionID, @SubTotal, @Discount, @AmountPaid," +
					"@Balance, @Change, @DatePaid);";
				  
				
	 			
				MySqlCommand cmd = new MySqlCommand();
				
				
				cmd.CommandType = System.Data.CommandType.Text;
				cmd.CommandText = SQL;
				
				MySqlParameter prmTransactionID = new MySqlParameter("@TransactionID",MySqlDbType.Int64);			
				prmTransactionID.Value = Details.TransactionID;
				cmd.Parameters.Add(prmTransactionID);

				MySqlParameter prmSubTotal = new MySqlParameter("@SubTotal",MySqlDbType.Decimal);			
				prmSubTotal.Value = Details.SubTotal;
				cmd.Parameters.Add(prmSubTotal);
				
				MySqlParameter prmDiscount = new MySqlParameter("@Discount",MySqlDbType.Decimal);
				prmDiscount.Value = Details.Discount;
				cmd.Parameters.Add(prmDiscount);

				MySqlParameter prmAmountPaid = new MySqlParameter("@AmountPaid",MySqlDbType.Decimal);			
				prmAmountPaid.Value = Details.AmountPaid;
				cmd.Parameters.Add(prmAmountPaid);

				MySqlParameter prmBalance = new MySqlParameter("@Balance",MySqlDbType.Decimal);			
				prmBalance.Value = Details.Balance;
				cmd.Parameters.Add(prmBalance);

				MySqlParameter prmChange = new MySqlParameter("@Change",MySqlDbType.Decimal);			
				prmChange.Value = Details.Change;
				cmd.Parameters.Add(prmChange);

				MySqlParameter prmDatePaid = new MySqlParameter("@DatePaid",MySqlDbType.DateTime);
				prmDatePaid.Value = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
				cmd.Parameters.Add(prmDatePaid);

				base.ExecuteNonQuery(cmd);

				SQL = "SELECT LAST_INSERT_ID();";
				
				cmd.Parameters.Clear(); 
				cmd.CommandText = SQL;
				
				MySqlDataReader myReader = base.ExecuteReader(cmd, System.Data.CommandBehavior.SingleResult);
				
				Int64 iID = 0;

				while (myReader.Read()) 
				{
					iID = myReader.GetInt64(0);
				}

				myReader.Close();

				return iID;
			}

			catch (Exception ex)
			{
				
				
				{
					
					
					
					
				}

				throw ex;
			}	
		}

		public void Update(BasePaymentsDetails Details)
		{
			try 
			{
				string SQL=	"UPDATE tblBasePayments SET " + 
					"TransactionID	=	@TransactionID, " +
					"SubTotal		=	@SubTotal, "+
					"Discount		=	@Discount, "+
					"AmountPaid		=	@AmountPaid," +
					"Balance		=	@Balance, " +
					"Change			=	@Change, " + 
					"DatePaid		=	@DatePaid " +
					"WHERE BasePaymentsID = @BasePaymentsID;";
				  
				
	 			
				MySqlCommand cmd = new MySqlCommand();
				
				
				cmd.CommandType = System.Data.CommandType.Text;
				cmd.CommandText = SQL;
				
				MySqlParameter prmTransactionID = new MySqlParameter("@TransactionID",MySqlDbType.Int64);			
				prmTransactionID.Value = Details.TransactionID;
				cmd.Parameters.Add(prmTransactionID);

				MySqlParameter prmSubTotal = new MySqlParameter("@SubTotal",MySqlDbType.Decimal);			
				prmSubTotal.Value = Details.SubTotal;
				cmd.Parameters.Add(prmSubTotal);
				
				MySqlParameter prmDiscount = new MySqlParameter("@Discount",MySqlDbType.Decimal);			
				prmDiscount.Value = Details.Discount;
				cmd.Parameters.Add(prmDiscount);

				MySqlParameter prmAmountPaid = new MySqlParameter("@AmountPaid",MySqlDbType.Decimal);			
				prmAmountPaid.Value = Details.AmountPaid;
				cmd.Parameters.Add(prmAmountPaid);

				MySqlParameter prmBalance = new MySqlParameter("@Balance",MySqlDbType.Decimal);			
				prmBalance.Value = Details.Balance;
				cmd.Parameters.Add(prmBalance);

				MySqlParameter prmChange = new MySqlParameter("@Change",MySqlDbType.Decimal);			
				prmChange.Value = Details.Change;
				cmd.Parameters.Add(prmChange);

				MySqlParameter prmDatePaid = new MySqlParameter("@DatePaid",MySqlDbType.DateTime);
				prmDatePaid.Value = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
				cmd.Parameters.Add(prmDatePaid);

				MySqlParameter prmBasePaymentsID = new MySqlParameter("@BasePaymentsID",System.Data.DbType.Int64);			
				prmBasePaymentsID.Value = Details.BasePaymentsID;
				cmd.Parameters.Add(prmBasePaymentsID);

				base.ExecuteNonQuery(cmd);
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
				string SQL=	"DELETE FROM tblBasePayments WHERE BasePaymentsID IN (" + IDs + ");";
				  
				
	 			
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

		public BasePaymentsDetails Details(Int64 BasePaymentsID)
		{
			try
			{
				string SQL = "SELECT " +
							"BasePaymentsID, " +
							"TransactionID, " +
							"SubTotal, " +
							"Discount, " +
							"AmountPaid, " +
							"Balance, " +
							"Change, " +
							"DatePaid " +
					"FROM tblBasePayments " +
					"WHERE BasePaymentsID = @BasePaymentsID;";
				  
				
	 			
				MySqlCommand cmd = new MySqlCommand();
				
				
				cmd.CommandType = System.Data.CommandType.Text;
				cmd.CommandText = SQL;

				MySqlParameter prmBasePaymentsID = new MySqlParameter("@BasePaymentsID",MySqlDbType.Int16);
				prmBasePaymentsID.Value = BasePaymentsID;
				cmd.Parameters.Add(prmBasePaymentsID);

				MySqlDataReader myReader = base.ExecuteReader(cmd, System.Data.CommandBehavior.SingleResult);
				
				BasePaymentsDetails Details = new BasePaymentsDetails();

				while (myReader.Read()) 
				{
					Details.BasePaymentsID = BasePaymentsID;
					Details.TransactionID = myReader.GetInt64("TransactionID");
					Details.SubTotal = myReader.GetDecimal("SubTotal");
					Details.Discount = myReader.GetDecimal("Discount");
					Details.AmountPaid = myReader.GetDecimal("AmountPaid");
					Details.Balance = myReader.GetDecimal("Balance");
					Details.Change = myReader.GetDecimal("Change");
					Details.DatePaid = myReader.GetDateTime("DatePaid");
				}

				myReader.Close();

				return Details;
			}

			catch (Exception ex)
			{
				
				
				{
					
					
					
					
				}

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
					"BasePaymentsID, " +
					"TransactionID, " +
					"SubTotal, " +
					"Discount, " +
					"AmountPaid, " +
					"Balance, " +
					"Change, " +
					"DatePaid " +
					"FROM tblBasePayments " +
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


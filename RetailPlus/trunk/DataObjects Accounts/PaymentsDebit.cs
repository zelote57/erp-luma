
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
	public struct PaymentsDebitDetails
	{
		public long PaymentDebitID;
		public long PaymentID;
		public int ChartOfAccountID;
		public decimal Amount;
	}

	[StrongNameIdentityPermissionAttribute(SecurityAction.LinkDemand,
		 PublicKey = "002400000480000094000000060200000024000" +
		 "052534131000400000100010053D785642F9F960B43157E0380" +
		 "F393BEE53E8DFAFBF441366C1B6F8B48D9DDF0D527B1F3B21EA" +
		 "E85D2FDB664CE81EB8A87DBE4C589D6F4202FE2B7D4B978BB69" +
		 "684874612CB9B8DB7A0339400A9C4E68277884B07817363D242" +
		 "E3696F9FACDBEA831810AE6DC9EDCA91A7B5DA12FE7BF65D113" +
		 "FF52834EAFB5A7A1FDFD5851A3")]
	public class PaymentsDebit : POSConnection
	{
		#region Constructors and Destructors

		public PaymentsDebit()
            : base(null, null)
        {
        }

        public PaymentsDebit(MySqlConnection Connection, MySqlTransaction Transaction) 
            : base(Connection, Transaction)
		{

		}

		#endregion

		#region Insert and Update

		public long Insert(PaymentsDebitDetails Details)
		{
			try 
			{
				string SQL = "INSERT INTO tblPaymentDebit (" +
								"PaymentID, " +
								"ChartOfAccountID, " +
								"Amount" +
							") VALUES (" +
								"@PaymentID, " +
								"@ChartOfAccountID, " +
								"@Amount" +
							");";
	 			
				MySqlCommand cmd = new MySqlCommand();
				cmd.CommandType = System.Data.CommandType.Text;
				cmd.CommandText = SQL;

				MySqlParameter prmPaymentID = new MySqlParameter("@PaymentID",MySqlDbType.Int64);			
				prmPaymentID.Value = Details.PaymentID;
				cmd.Parameters.Add(prmPaymentID);

				MySqlParameter prmChartOfAccountID = new MySqlParameter("@ChartOfAccountID",MySqlDbType.Int64);			
				prmChartOfAccountID.Value = Details.ChartOfAccountID;
				cmd.Parameters.Add(prmChartOfAccountID);

				MySqlParameter prmAmount = new MySqlParameter("@Amount",MySqlDbType.Decimal);			
				prmAmount.Value = Details.Amount;
				cmd.Parameters.Add(prmAmount);
     
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
				throw base.ThrowException(ex);
			}	
		}

		public void Update(PaymentsDebitDetails Details)
		{
			try 
			{
				string SQL = "UPDATE tblPaymentDebit SET " + 
								"PaymentID			=	@PaymentID, " +
								"ChartOfAccountID	=	@ChartOfAccountID, " +
								"Amount				=	@Amount " +
							"WHERE PaymentDebitID	=	@PaymentDebitID;";
				  
				MySqlCommand cmd = new MySqlCommand();
				cmd.CommandType = System.Data.CommandType.Text;
				cmd.CommandText = SQL;

				MySqlParameter prmPaymentID = new MySqlParameter("@PaymentID",MySqlDbType.Int64);			
				prmPaymentID.Value = Details.PaymentID;
				cmd.Parameters.Add(prmPaymentID);

				MySqlParameter prmChartOfAccountID = new MySqlParameter("@ChartOfAccountID",MySqlDbType.Int64);			
				prmChartOfAccountID.Value = Details.ChartOfAccountID;
				cmd.Parameters.Add(prmChartOfAccountID);

				MySqlParameter prmAmount = new MySqlParameter("@Amount",MySqlDbType.Decimal);			
				prmAmount.Value = Details.Amount;
				cmd.Parameters.Add(prmAmount);

				MySqlParameter prmPaymentDebitID = new MySqlParameter("@PaymentDebitID",MySqlDbType.Int64);				
				prmPaymentDebitID.Value = Details.PaymentDebitID;
				cmd.Parameters.Add(prmPaymentDebitID);

				base.ExecuteNonQuery(cmd);
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
				string SQL=	"DELETE FROM tblPaymentDebit WHERE PaymentDebitID IN (" + IDs + ");";
				  
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

		public PaymentsDebitDetails Details(long PaymentDebitID)
		{
			try
			{
				string SQL =	"SELECT " +
									"PaymentDebitID, " +
									"PaymentID, " +
									"a.ChartOfAccountID, " +
									"ChartOfAccountCode, " +
									"ChartOfAccountName, " +
									"Amount " +
								"FROM tblPaymentDebit a INNER JOIN tblChartOfAccount b " +
									"ON a.ChartOfAccountID = b.ChartOfAccountID " +
								"WHERE PaymentDebitID = @PaymentDebitID;";
				  
				MySqlCommand cmd = new MySqlCommand();
				cmd.CommandType = System.Data.CommandType.Text;
				cmd.CommandText = SQL;

				MySqlParameter prmPaymentDebitID = new MySqlParameter("@PaymentDebitID",MySqlDbType.Int64);			
				prmPaymentDebitID.Value = PaymentDebitID;
				cmd.Parameters.Add(prmPaymentDebitID);

                string strDataTableName = "tbl" + this.GetType().FullName.Split(new Char[] { '.' })[this.GetType().FullName.Split(new Char[] { '.' }).Length - 1]; System.Data.DataTable dt = new System.Data.DataTable(strDataTableName);
                base.MySqlDataAdapterFill(cmd, dt);

                PaymentsDebitDetails Details = new PaymentsDebitDetails();

                foreach (System.Data.DataRow dr in dt.Rows)
                {
                    Details.PaymentDebitID = PaymentDebitID;
                    Details.PaymentID = Int64.Parse(dr["PaymentID"].ToString());
                    Details.ChartOfAccountID = Int32.Parse(dr["ChartOfAccountID"].ToString());
                    Details.Amount = decimal.Parse(dr["Amount"].ToString());
                }

				return Details;
			}

			catch (Exception ex)
			{
				throw base.ThrowException(ex);
			}	
		}


		#endregion

		#region Streams

		public MySqlDataReader List(string SortField, SortOption SortOrder)
		{
			try
			{
				string SQL ="SELECT " +
								"PaymentDebitID, " +
								"PaymentID, " +
								"a.ChartOfAccountID, " +
								"ChartOfAccountCode, " +
								"ChartOfAccountName, " +
								"Amount " +
							"FROM tblPaymentDebit a INNER JOIN tblChartOfAccount b " +
							"ON a.ChartOfAccountID = b.ChartOfAccountID " +
							"ORDER BY " + SortField;

				if (SortOrder == SortOption.Ascending)
					SQL += " ASC";
				else
					SQL += " DESC";

				MySqlCommand cmd = new MySqlCommand();
				cmd.CommandType = System.Data.CommandType.Text;
				cmd.CommandText = SQL;
				
				MySqlDataReader myReader = base.ExecuteReader(cmd);
				
				return myReader;			
			}
			catch (Exception ex)
			{
				throw base.ThrowException(ex);
			}	
		}
		
		public MySqlDataReader List(long PaymentID, string SortField, SortOption SortOrder)
		{
			try
			{
				string SQL ="SELECT " +
								"PaymentDebitID, " +
								"PaymentID, " +
								"a.ChartOfAccountID, " +
								"ChartOfAccountCode, " +
								"ChartOfAccountName, " +
								"Amount " +
							"FROM tblPaymentDebit a INNER JOIN tblChartOfAccount b " +
							"ON a.ChartOfAccountID = b.ChartOfAccountID " +
							"WHERE PaymentID = @PaymentID " +
							"ORDER BY " + SortField;

				if (SortOrder == SortOption.Ascending)
					SQL += " ASC";
				else
					SQL += " DESC";

				MySqlCommand cmd = new MySqlCommand();
				cmd.CommandType = System.Data.CommandType.Text;
				cmd.CommandText = SQL;
				
				MySqlParameter prmPaymentID = new MySqlParameter("@PaymentID",MySqlDbType.Int64);			
				prmPaymentID.Value = PaymentID;
				cmd.Parameters.Add(prmPaymentID);

				MySqlDataReader myReader = base.ExecuteReader(cmd);
				
				return myReader;			
			}
			catch (Exception ex)
			{
				throw base.ThrowException(ex);
			}	
		}
		
		public MySqlDataReader Search(string SearchKey, string SortField, SortOption SortOrder)
		{
			try
			{
				string SQL ="SELECT " +
								"PaymentDebitID, " +
								"PaymentID, " +
								"a.ChartOfAccountID, " +
								"ChartOfAccountCode, " +
								"ChartOfAccountName, " +
								"Amount " +
							"FROM tblPaymentDebit a INNER JOIN tblChartOfAccount b " +
							"ON a.ChartOfAccountID = b.ChartOfAccountID " +
							"WHERE PaymentID LIKE @SearchKey " +
								"or ChartOfAccountCode LIKE @SearchKey " +
								"or ChartOfAccountName LIKE @SearchKey " +
							"ORDER BY " + SortField;

				if (SortOrder == SortOption.Ascending)
					SQL += " ASC";
				else
					SQL += " DESC";

				MySqlCommand cmd = new MySqlCommand();
				cmd.CommandType = System.Data.CommandType.Text;
				cmd.CommandText = SQL;
				
				MySqlParameter prmSearchKey = new MySqlParameter("@SearchKey",MySqlDbType.String);
				prmSearchKey.Value = "%" + SearchKey +"%";
				cmd.Parameters.Add(prmSearchKey);

				MySqlDataReader myReader = base.ExecuteReader(cmd);
				
				return myReader;			
			}
			catch (Exception ex)
			{
				throw base.ThrowException(ex);
			}	
		}		

		public MySqlDataReader Search(long PaymentID, string SearchKey, string SortField, SortOption SortOrder)
		{
			try
			{
				string SQL ="SELECT " +
								"PaymentDebitID, " +
								"PaymentID, " +
								"a.ChartOfAccountID, " +
								"ChartOfAccountCode, " +
								"ChartOfAccountName, " +
								"Amount " +
							"FROM tblPaymentDebit a INNER JOIN tblChartOfAccount b " +
								"ON a.ChartOfAccountID = b.ChartOfAccountID " +
							"WHERE PaymentID = @PaymentID " +
								"AND (ChartOfAccountCode LIKE @SearchKey " +
								"or ChartOfAccountName LIKE @SearchKey) " +
							"ORDER BY " + SortField;

				if (SortOrder == SortOption.Ascending)
					SQL += " ASC";
				else
					SQL += " DESC";

				MySqlCommand cmd = new MySqlCommand();
				cmd.CommandType = System.Data.CommandType.Text;
				cmd.CommandText = SQL;
				
				MySqlParameter prmPaymentID = new MySqlParameter("@PaymentID",MySqlDbType.Int64);			
				prmPaymentID.Value = PaymentID;
				cmd.Parameters.Add(prmPaymentID);

				MySqlParameter prmSearchKey = new MySqlParameter("@SearchKey",MySqlDbType.String);
				prmSearchKey.Value = "%" + SearchKey +"%";
				cmd.Parameters.Add(prmSearchKey);

				MySqlDataReader myReader = base.ExecuteReader(cmd);
				
				return myReader;			
			}
			catch (Exception ex)
			{
				throw base.ThrowException(ex);
			}	
		}		

		#endregion

		public void Post(long PaymentID)
		{
			try 
			{
				GetConnection();
				ChartOfAccount clsChartOfAccount = new ChartOfAccount(Connection, Transaction);

				MySqlDataReader myReader = List(PaymentID, "PaymentDebitID",SortOption.Ascending);
				while (myReader.Read())
				{
					int ChartOfAccountID = myReader.GetInt32("ChartOfAccountID");
					decimal Amount = myReader.GetDecimal("Amount");

					clsChartOfAccount.UpdateDebit(ChartOfAccountID, Amount);
				}
				myReader.Close();

			}

			catch (Exception ex)
			{
				throw base.ThrowException(ex);
			}	
		}
	}
}


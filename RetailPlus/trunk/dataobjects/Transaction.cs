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
	public class Transaction
	{
		MySqlConnection mConnection;
		MySqlTransaction mTransaction;
		bool IsInTransaction = false;
		bool TransactionFailed = false;

		public MySqlConnection SQLConnection
		{
			get { return mConnection;	}
		}

		public MySqlTransaction SQLTransaction
		{
			get { return mTransaction;	}
		}


		#region Constructors & Destructors

		public Transaction()
		{
			
		}

		public Transaction(MySqlConnection Connection, MySqlTransaction Transaction)
		{
			mConnection = Connection;
			mTransaction = Transaction;
			
		}

		public void CommitAndDispose() 
		{
			if (!TransactionFailed)
			{
				if (IsInTransaction)
				{
					mTransaction.Commit();
					mTransaction.Dispose(); 
					mConnection.Close();
					mConnection.Dispose();
				}
			}
		}

		public MySqlConnection GetConnection()
		{
			if (mConnection==null)
			{
				mConnection = new MySqlConnection(AceSoft.RetailPlus.DBConnection.ConnectionString());	
				mConnection.Open(); 
				
				mTransaction = (MySqlTransaction) mConnection.BeginTransaction();
				IsInTransaction = true;
			}

			return mConnection;
		} 


		#endregion

		#region Public Modifiers

		public string LastTransactionNo()
		{
			try
			{
				string SQL=	"SELECT " +
								"EndingTransactionNo " +
							"FROM tblTerminalReport " +
							"WHERE 1 = 1 " +
							"AND TerminalNo = @TerminalNo;";
				  
				MySqlConnection cn = GetConnection();
	 			
				MySqlCommand cmd = new MySqlCommand();
				cmd.Connection = cn;
				cmd.Transaction = mTransaction;
				cmd.CommandType = System.Data.CommandType.Text;
				cmd.CommandText = SQL;

				MySqlParameter prmTerminalNo = new MySqlParameter("@TerminalNo",MySqlDbType.String);
				prmTerminalNo.Value = CompanyDetails.TerminalNo;
				cmd.Parameters.Add(prmTerminalNo);

				MySqlDataReader myReader = (MySqlDataReader) cmd.ExecuteReader(System.Data.CommandBehavior.SingleResult);
				
				string stRetValue = String.Empty;
				Int32 iLen = 15;

				while (myReader.Read()) 
				{
					if (myReader.GetString(0) != null && myReader.GetString(0) != "")
					{
						stRetValue = "" + myReader["EndingTransactionNo"].ToString();
						iLen = stRetValue.Length;
						stRetValue = stRetValue.PadLeft(iLen, '0');
					}
				}

				myReader.Close();

				if (stRetValue == String.Empty)
					throw new NullReferenceException();

				string EndingTransactionNo = Convert.ToString(Convert.ToInt64(stRetValue) + 1);
				EndingTransactionNo = EndingTransactionNo.PadLeft(iLen, '0');

				SQL = "UPDATE tblTerminalReport SET EndingTransactionNo = @EndingTransactionNo " +
						"WHERE TerminalNo = @TerminalNo;";
                cmd.CommandText = SQL;
                cmd.Parameters.Clear();

				MySqlParameter prmEndingTransactionNo = new MySqlParameter("@EndingTransactionNo",MySqlDbType.String);
				prmEndingTransactionNo.Value = EndingTransactionNo;
				cmd.Parameters.Add(prmEndingTransactionNo);

				cmd.Parameters.Add(prmTerminalNo);

				cmd.ExecuteNonQuery();

				return stRetValue;
			}

			catch (Exception ex)
			{
				TransactionFailed = true;
				if (IsInTransaction)
				{
					mTransaction.Rollback();
					mTransaction.Dispose(); 
					mConnection.Close();
					mConnection.Dispose();
				}

				throw ex;
			}	
		}

		#endregion
	}
}


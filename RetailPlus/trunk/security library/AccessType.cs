using System;
using System.Data;
using System.Security.Permissions;
using MySql.Data.MySqlClient;

/******************************************************************************
	**		Auth: Lemuel E. Aceron
	**		Date: March 29, 2005
	***************************************************************************
	**		Change History
	***************************************************************************
	**		Date:			Author:				Description:
	**		--------		--------			-------------------------------
	**      
	***************************************************************************/

namespace AceSoft.RetailPlus.Security
{
	

	[StrongNameIdentityPermissionAttribute(SecurityAction.LinkDemand,
		 PublicKey = "002400000480000094000000060200000024000" +
		 "052534131000400000100010053D785642F9F960B43157E0380" +
		 "F393BEE53E8DFAFBF441366C1B6F8B48D9DDF0D527B1F3B21EA" +
		 "E85D2FDB664CE81EB8A87DBE4C589D6F4202FE2B7D4B978BB69" +
		 "684874612CB9B8DB7A0339400A9C4E68277884B07817363D242" +
		 "E3696F9FACDBEA831810AE6DC9EDCA91A7B5DA12FE7BF65D113" +
		 "FF52834EAFB5A7A1FDFD5851A3")]
	public struct AccessTypeDetails
	{
		public int TypeID;
		public string TypeName;
		public string Remarks;
		public bool Enabled;
		public int SequenceNo;
		public string Category;
	}

	
	[StrongNameIdentityPermissionAttribute(SecurityAction.LinkDemand,
		 PublicKey = "002400000480000094000000060200000024000" +
		 "052534131000400000100010053D785642F9F960B43157E0380" +
		 "F393BEE53E8DFAFBF441366C1B6F8B48D9DDF0D527B1F3B21EA" +
		 "E85D2FDB664CE81EB8A87DBE4C589D6F4202FE2B7D4B978BB69" +
		 "684874612CB9B8DB7A0339400A9C4E68277884B07817363D242" +
		 "E3696F9FACDBEA831810AE6DC9EDCA91A7B5DA12FE7BF65D113" +
		 "FF52834EAFB5A7A1FDFD5851A3")]
	public class AccessType
	{
		MySqlConnection mConnection;
		MySqlTransaction mTransaction;
		bool IsInTransaction = false;
		bool TransactionFailed = false;

		public MySqlConnection Connection
		{
			get { return mConnection;	}
		}

		public MySqlTransaction Transaction
		{
			get { return mTransaction;	}
		}


		#region Constructors and Destructors

		public AccessType()
		{
			
		}

		public AccessType(MySqlConnection Connection, MySqlTransaction Transaction)
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


		#endregion

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

		#region Streams

		private string SQLSelect()
		{
            string stSQL = "SELECT " +
                                "TypeID, " +
                                "TypeName, " +
                                "Remarks, " +
                                "Enabled, " +
                                "SequenceNo, " +
                                "Category " +
                            "FROM sysAccessTypes ";
			return stSQL;
		}

        public System.Data.DataTable DataList(string SortField, SortOption SortOrder)
        {
            if (SortField == string.Empty || SortField == null) SortField = "Category, SequenceNo";

            string SQL = SQLSelect() + "ORDER BY " + SortField;

            if (SortOrder == SortOption.Ascending)
                SQL += " ASC;";
            else
                SQL += " DESC;";

            MySqlConnection cn = GetConnection();

            MySqlCommand cmd = new MySqlCommand();
            cmd.Connection = cn;
            cmd.Transaction = mTransaction;
            cmd.CommandType = System.Data.CommandType.Text;
            cmd.CommandText = SQL;

            System.Data.DataTable dt = new System.Data.DataTable("sysAccessTypes");
            MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
            adapter.Fill(dt);

            return dt;

        }
        public System.Data.DataTable DataList(string Category, string SortField, SortOption SortOrder)
        {
            if (SortField == string.Empty || SortField == null) SortField = "Category, SequenceNo";

            string SQL = SQLSelect() + "WHERE Category = @Category ORDER BY " + SortField;

            if (SortOrder == SortOption.Ascending)
                SQL += " ASC;";
            else
                SQL += " DESC;";

            MySqlConnection cn = GetConnection();

            MySqlCommand cmd = new MySqlCommand();
            cmd.Connection = cn;
            cmd.Transaction = mTransaction;
            cmd.CommandType = System.Data.CommandType.Text;
            cmd.CommandText = SQL;

            cmd.Parameters.AddWithValue("@Category", Category);

            System.Data.DataTable dt = new System.Data.DataTable("sysAccessTypes");
            MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
            adapter.Fill(dt);

            return dt;

        }
        public System.Data.DataTable Categories(string SortField, SortOption SortOrder)
        {
            try
            {
                if (SortField == string.Empty || SortField == null) SortField = "Category, SequenceNo";

                string SQL = "SELECT DISTINCT(Category) FROM sysAccessTypes ORDER BY " + SortField;

                if (SortOrder == SortOption.Ascending)
                    SQL += " ASC;";
                else
                    SQL += " DESC;";

                MySqlConnection cn = GetConnection();

                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = cn;
                cmd.Transaction = mTransaction;
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandText = SQL;

                System.Data.DataTable dt = new System.Data.DataTable("tblAccessTypes");
                MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
                adapter.Fill(dt);

                return dt;
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


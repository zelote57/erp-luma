
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System;
using System.Data;
using System.Security.Permissions;
using MySql.Data.MySqlClient;

namespace AceSoft.RetailPlus.Data
{
    /// <summary>
    /// This is an inherited class from contact
    /// </summary>
    public class Customer : Contact
    {
        MySqlConnection mConnection;
        MySqlTransaction mTransaction;
        bool IsInTransaction = false;
        bool TransactionFailed = false;

        public new MySqlConnection Connection
        {
            get { return mConnection; }
        }

        public new MySqlTransaction Transaction
        {
            get { return mTransaction; }
        }

		#region Constructors and Destructors

		public Customer()
		{
			
		}

        public Customer(MySqlConnection Connection, MySqlTransaction Transaction)
		{
			mConnection = Connection;
			mTransaction = Transaction;
		}

        new public void CommitAndDispose()
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

        new public MySqlConnection GetConnection()
        {
            if (mConnection == null)
            {
                mConnection = new MySqlConnection(AceSoft.RetailPlus.DBConnection.ConnectionString());
                mConnection.Open();

                mTransaction = (MySqlTransaction)mConnection.BeginTransaction();
                IsInTransaction = true;
            }

            return mConnection;
        } 

		#endregion

        #region DataTables

        public DataTable getRewardPoints(long CustomerID, DateTime TransactionDateFrom, DateTime TransactionDateTo)
        {
            try
            {
                MySqlConnection cn = GetConnection();

                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = cn;
                cmd.Transaction = mTransaction;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = "CALL procGetRewardPointsReport(@lngCustomerID, @dteTransactionDateFrom, @dteTransactionDateTo)";

                MySqlParameter prmCustomerID = new MySqlParameter("@lngCustomerID",MySqlDbType.Int64);
                prmCustomerID.Value = CustomerID;
                cmd.Parameters.Add(prmCustomerID);

                MySqlParameter prmdteTransactionDateFrom = new MySqlParameter("@dteTransactionDateFrom",MySqlDbType.DateTime);
                prmdteTransactionDateFrom.Value = TransactionDateFrom.ToString("yyyy-MM-dd");
                cmd.Parameters.Add(prmdteTransactionDateFrom);

                MySqlParameter prmTransactionDateTo = new MySqlParameter("@dteTransactionDateTo",MySqlDbType.DateTime);
                prmTransactionDateTo.Value = TransactionDateTo.ToString("yyyy-MM-dd");
                cmd.Parameters.Add(prmTransactionDateTo);

                System.Data.DataTable dt = new System.Data.DataTable("tblContacts");
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

        public new MySqlDataReader Suppliers(string SearchKey, Int32 Limit, string SortField, SortOption SortOrder)
        {
            return null;
        }

        public new DataTable Suppliers(ContactColumns clsContactColumns, long SequenceNoStart, System.Data.SqlClient.SortOrder SequenceSortOrder, ContactColumns SearchColumns, string SearchKey, Int32 Limit, bool HasCreditOnly, string SortField, System.Data.SqlClient.SortOrder SortOrder)
        {
            return null;
        }
        public new DataTable ListAsDataTable(string SortField, SortOption SortOrder)
        {
            return null;
        }
        public new DataTable SearchAsDataTable(string SearchKey, string SortField, SortOption SortOrder)
        {
            return null;
        }
        public new DataTable SuppliersAsDataTable(string SearchKey = "", Int32 Limit = 0, string SortField = "ContactCode", SortOption SortOrder = SortOption.Ascending)
        {
            return null;
        }
        public new DataTable AgentsAsDataTable(string SearchKey, Int32 Limit, string SortField, SortOption SortOrder)
        {
            return null;
        }

        #endregion
    }
}

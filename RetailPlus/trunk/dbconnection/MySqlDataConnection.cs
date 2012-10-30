using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MySql.Data.MySqlClient;

namespace AceSoft.RetailPlus
{
    public abstract class POSConnection
    {
        MySqlConnection mConnection;
        MySqlTransaction mTransaction;
        bool IsInTransaction = false;
        bool TransactionFailed = false;

        public MySqlConnection Connection
        {
            get { return mConnection; }
            set { mConnection = value; }
        }

        public MySqlTransaction Transaction
        {
            get { return mTransaction; }
        }

        public POSConnection()
        {
        }

        public POSConnection(MySqlConnection Connection, MySqlTransaction Transaction)
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
            if (mConnection == null)
            {
                mConnection = new MySqlConnection(AceSoft.RetailPlus.DBConnection.ConnectionString());
                mConnection.Open();

                mTransaction = (MySqlTransaction)mConnection.BeginTransaction();
                IsInTransaction = true;
            }

            return mConnection;
        }

        public MySqlDataReader ExecuteReader(MySqlCommand cmd, System.Data.CommandBehavior behaviour = System.Data.CommandBehavior.Default)
        {
            try
            {
                MySqlConnection cn = GetConnection();
                cmd.Connection = cn;

                MySqlDataReader myReader = (MySqlDataReader)cmd.ExecuteReader(System.Data.CommandBehavior.SingleResult);

                return myReader;
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

        public int ExecuteNonQuery(MySqlCommand cmd)
        {
            try
            {
                MySqlConnection cn = GetConnection();
                cmd.Connection = cn;

                return cmd.ExecuteNonQuery();

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

        public int MySqlDataAdapterFill(MySqlCommand cmd, System.Data.DataTable dt)
        {
            try
            {
                MySqlConnection cn = GetConnection();
                cmd.Connection = cn;

                MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
                return adapter.Fill(dt);
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
    }
}

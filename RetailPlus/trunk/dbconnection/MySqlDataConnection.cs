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
        bool IsNewInstance = false;

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

            GetConnection();
        }

        //public POSConnection(out MySqlConnection Connection, out MySqlTransaction Transaction)
        //{

        //    mConnection = Connection;
        //    mTransaction = Transaction;

        //    GetConnection();
        //}

        public void CommitAndDispose()
        {
            if (!TransactionFailed)
            {
                if (IsInTransaction && IsNewInstance)
                {
                    mTransaction.Commit();
                    mTransaction.Dispose();
                    mConnection.Close();
                    mConnection.Dispose();
                }
            }
        }

        public MySqlConnection GetConnection(string ServerIP = "", string DBPort = "", string DBName = "")
        {
            if (mConnection == null || mConnection.State != System.Data.ConnectionState.Open)
            {
                mConnection = new MySqlConnection(AceSoft.RetailPlus.DBConnection.ConnectionString(ServerIP, DBPort, DBName));
                mConnection.Open();

                mTransaction = (MySqlTransaction)mConnection.BeginTransaction();
                IsInTransaction = true;
                IsNewInstance = true;
            }

            return mConnection;
        }

        public MySqlDataReader ExecuteReader(MySqlCommand cmd, System.Data.CommandBehavior behaviour = System.Data.CommandBehavior.Default)
        {
            try
            {
                cmd.Connection = GetConnection();

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

        public int ExecuteNonQuery(string SQL)
        {
            try
            {
                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = GetConnection();
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandText = SQL;

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
        public int ExecuteNonQuery(MySqlCommand cmd)
        {
            try
            {
                cmd.Connection = GetConnection();

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
                cmd.Connection = GetConnection();

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

        public int MySqlDataAdapterFill(string SQL, System.Data.DataTable dt)
        {
            try
            {
                MySqlDataAdapter adapter = new MySqlDataAdapter(SQL, GetConnection());
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

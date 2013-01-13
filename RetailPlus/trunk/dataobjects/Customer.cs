
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
    public class Customer : Contacts
    {
        public Customer()
            : base(null, null)
        {
        }

        public Customer(MySqlConnection Connection, MySqlTransaction Transaction) 
            : base(Connection, Transaction)
		{

		}

        #region DataTables

        public DataTable getRewardPoints(long CustomerID, DateTime TransactionDateFrom, DateTime TransactionDateTo)
        {
            try
            {
                MySqlCommand cmd = new MySqlCommand();
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
                base.MySqlDataAdapterFill(cmd, dt);
                

                return dt;
            }
            catch (Exception ex)
            {
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

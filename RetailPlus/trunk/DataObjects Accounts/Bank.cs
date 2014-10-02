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
	public struct BankDetails
	{
		public Int32 BankID;
		public string BankCode;
		public string BankName;
        public string ChequeCode;
        public string ChequeCounter;

        public DateTime CreatedOn;
        public DateTime LastModified;
	}

	[StrongNameIdentityPermissionAttribute(SecurityAction.LinkDemand,
		 PublicKey = "002400000480000094000000060200000024000" +
		 "052534131000400000100010053D785642F9F960B43157E0380" +
		 "F393BEE53E8DFAFBF441366C1B6F8B48D9DDF0D527B1F3B21EA" +
		 "E85D2FDB664CE81EB8A87DBE4C589D6F4202FE2B7D4B978BB69" +
		 "684874612CB9B8DB7A0339400A9C4E68277884B07817363D242" +
		 "E3696F9FACDBEA831810AE6DC9EDCA91A7B5DA12FE7BF65D113" +
		 "FF52834EAFB5A7A1FDFD5851A3")]
	public class Banks : POSConnection
	{
		#region Bank and Destructors

        public Banks()
            : base(null, null)
        {
        }

        public Banks(MySqlConnection Connection, MySqlTransaction Transaction) 
            : base(Connection, Transaction)
		{

		}

		#endregion

		#region Insert and Update

		public Int32 Insert(BankDetails Details)
		{
			try 
			{
                Save(Details);

                return Int32.Parse(base.getLAST_INSERT_ID(this));
			}
			catch (Exception ex)
			{
				throw base.ThrowException(ex);
			}	
		}
		public void Update(BankDetails Details)
		{
			try 
			{
                Save(Details);
			}

			catch (Exception ex)
			{
				throw base.ThrowException(ex);
			}	
		}
        public void UpdateChequeCounter(Int32 BankID, string ChequeCounter)
        {
            try
            {
                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandType = System.Data.CommandType.Text;
                
                string SQL = "UPDATE tblBank SET " +
                                "ChequeCounter  = @ChequeCounter " +
                            "WHERE BankID = @BankID;";

                cmd.Parameters.AddWithValue("ChequeCounter", ChequeCounter);
                cmd.Parameters.AddWithValue("BankID", BankID);

                cmd.CommandText = SQL;
                base.ExecuteNonQuery(cmd);
            }

            catch (Exception ex)
            {
                throw base.ThrowException(ex);
            }
        }
        public void UpdateChequeCounter(string BankCode, string ChequeCounter)
        {
            try
            {
                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandType = System.Data.CommandType.Text;

                string SQL = "UPDATE tblBank SET " +
                                "ChequeCounter  = @ChequeCounter " +
                            "WHERE BankCode	    = @BankCode ";

                cmd.Parameters.AddWithValue("BankCode", BankCode);
                cmd.Parameters.AddWithValue("ChequeCounter", ChequeCounter);

                cmd.CommandText = SQL;
                base.ExecuteNonQuery(cmd);
            }

            catch (Exception ex)
            {
                throw base.ThrowException(ex);
            }
        }

        public Int32 Save(BankDetails Details)
        {
            try
            {
                string SQL = "CALL procSaveBank(@BankID, @BankCode, @BankName, @ChequeCode, @ChequeCounter, @CreatedOn, @LastModified);";

                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandText = SQL;

                cmd.Parameters.AddWithValue("BankID", Details.BankID);
                cmd.Parameters.AddWithValue("BankCode", Details.BankCode);
                cmd.Parameters.AddWithValue("BankName", Details.BankName);
                cmd.Parameters.AddWithValue("ChequeCode", Details.ChequeCode);
                cmd.Parameters.AddWithValue("ChequeCounter", Details.ChequeCounter);
                cmd.Parameters.AddWithValue("CreatedOn", Details.CreatedOn == DateTime.MinValue ? Constants.C_DATE_MIN_VALUE : Details.CreatedOn);
                cmd.Parameters.AddWithValue("LastModified", Details.LastModified == DateTime.MinValue ? Constants.C_DATE_MIN_VALUE : Details.LastModified);

                return base.ExecuteNonQuery(cmd);
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
				string SQL=	"DELETE FROM tblBank WHERE BankID IN (" + IDs + ");";
				  
				
	 			
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

				throw base.ThrowException(ex);
			}	
		}


		#endregion

        private string SQLSelect()
        {
            string stSQL = "SELECT " +
                                "BankID, " +
                                "BankCode, " +
                                "BankName, " +
                                "ChequeCode, " +
                                "ChequeCounter " +
                            "FROM tblBank ";
            return stSQL;
        }

		#region Details

		public BankDetails Details(Int32 BankID)
		{
			try
			{
                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandType = System.Data.CommandType.Text;

				string SQL =	SQLSelect() + "WHERE BankID = @BankID;";
				  
				cmd.Parameters.AddWithValue("@BankID", BankID);

                cmd.CommandText = SQL;
                string strDataTableName = "tbl" + this.GetType().FullName.Split(new Char[] { '.' })[this.GetType().FullName.Split(new Char[] { '.' }).Length - 1]; System.Data.DataTable dt = new System.Data.DataTable(strDataTableName);
                base.MySqlDataAdapterFill(cmd, dt);
				
				BankDetails Details = new BankDetails();
                foreach (System.Data.DataRow dr in dt.Rows)
                {
                    Details.BankID = BankID;
                    Details.BankCode = dr["BankCode"].ToString();
                    Details.BankName = dr["BankName"].ToString();
                    Details.ChequeCode = dr["ChequeCode"].ToString();
                    Details.ChequeCounter = dr["ChequeCounter"].ToString();
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

        public System.Data.DataTable ListAsDataTable(string SearchKey = "", string SortField = "BankCode", SortOption SortOrder = SortOption.Ascending)
        {
            try
            {
                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandType = System.Data.CommandType.Text;

                string SQL = SQLSelect() + " ";

                if (!string.IsNullOrEmpty(SearchKey))
                {
                    SQL += "WHERE (BankCode LIKE @SearchKey or BankName LIKE @SearchKey or ChequeCode LIKE @SearchKey) ";
                    cmd.Parameters.AddWithValue("@SearchKey", SearchKey);
                }

                SQL += "ORDER BY " + SortField + " ";
                SQL += SortOrder == SortOption.Ascending ? "ASC" : "DESC";

                cmd.CommandText = SQL;
                string strDataTableName = "tbl" + this.GetType().FullName.Split(new Char[] { '.' })[this.GetType().FullName.Split(new Char[] { '.' }).Length - 1]; System.Data.DataTable dt = new System.Data.DataTable(strDataTableName);
                base.MySqlDataAdapterFill(cmd, dt);

                return dt;
            }
            catch (Exception ex)
            {
                throw base.ThrowException(ex);
            }
        }

		#endregion

        #region Public Modifiers
       
        public string getChequeNo(Int32 BankID = 1)
        {
            try
            {
                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandType = System.Data.CommandType.Text;

                string SQL = "SELECT ChequeCounter FROM tblBank WHERE BankID = @BankID ";

                cmd.Parameters.AddWithValue("@BankID", BankID);

                cmd.CommandText = SQL;
                string strDataTableName = "tbl" + this.GetType().FullName.Split(new Char[] { '.' })[this.GetType().FullName.Split(new Char[] { '.' }).Length - 1]; System.Data.DataTable dt = new System.Data.DataTable(strDataTableName);
                base.MySqlDataAdapterFill(cmd, dt);

                string stRetValue = "";
                foreach (System.Data.DataRow dr in dt.Rows)
                {
                    Int64 lChequeCounter = Int64.Parse(dr["ChequeCounter"].ToString()) + 1;
                    stRetValue = dr["ChequeCounter"].ToString();
                    stRetValue = lChequeCounter.ToString().PadLeft(stRetValue.Length, '0');
                }
                
                return stRetValue;
            }

            catch (Exception ex)
            {
                throw base.ThrowException(ex);
            }
        }
        public string getChequeNo(string BankCode)
        {
            try
            {
                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandType = System.Data.CommandType.Text;

                string SQL = "SELECT ChequeCounter FROM tblBank WHERE BankID = @BankCode ";

                cmd.Parameters.AddWithValue("@BankCode", BankCode);

                cmd.CommandText = SQL;
                string strDataTableName = "tbl" + this.GetType().FullName.Split(new Char[] { '.' })[this.GetType().FullName.Split(new Char[] { '.' }).Length - 1]; System.Data.DataTable dt = new System.Data.DataTable(strDataTableName);
                base.MySqlDataAdapterFill(cmd, dt);

                string stRetValue = "";
                foreach (System.Data.DataRow dr in dt.Rows)
                {
                    Int64 lChequeCounter = Int64.Parse(dr["ChequeCounter"].ToString()) + 1;
                    stRetValue = dr["ChequeCounter"].ToString();
                    stRetValue = lChequeCounter.ToString().PadLeft(stRetValue.Length, '0');
                }

                return stRetValue;
            }

            catch (Exception ex)
            {
                throw base.ThrowException(ex);
            }
        }
       
        public string getChequeNo(out string ChequeCode, Int32 BankID = 1)
        {
            try
            {
                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandType = System.Data.CommandType.Text;
                
                string SQL = "SELECT ChequeCode, ChequeCounter FROM tblBank WHERE BankID = @BankID ";

                cmd.Parameters.AddWithValue("@BankID", BankID);

                cmd.CommandText = SQL;
                string strDataTableName = "tbl" + this.GetType().FullName.Split(new Char[] { '.' })[this.GetType().FullName.Split(new Char[] { '.' }).Length - 1]; System.Data.DataTable dt = new System.Data.DataTable(strDataTableName);
                base.MySqlDataAdapterFill(cmd, dt);

                ChequeCode = "";
                string stRetValue = "";
                foreach (System.Data.DataRow dr in dt.Rows)
                {
                    ChequeCode = dr["ChequeCode"].ToString();
                    long lChequeCounter = Int64.Parse(dr["ChequeCounter"].ToString()) + 1;
                    stRetValue = dr["ChequeCounter"].ToString();
                    stRetValue = lChequeCounter.ToString().PadLeft(stRetValue.Length, '0');
                }

                return stRetValue;
            }

            catch (Exception ex)
            {
                throw base.ThrowException(ex);
            }
        }
        public string getChequeNo(string BankCode, out string ChequeCode)
        {
            try
            {
                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandType = System.Data.CommandType.Text;

                string SQL = "SELECT ChequeCode, ChequeCounter FROM tblBank WHERE BankID = @BankCode ";

                cmd.Parameters.AddWithValue("@BankCode", BankCode);

                cmd.CommandText = SQL;
                string strDataTableName = "tbl" + this.GetType().FullName.Split(new Char[] { '.' })[this.GetType().FullName.Split(new Char[] { '.' }).Length - 1]; System.Data.DataTable dt = new System.Data.DataTable(strDataTableName);
                base.MySqlDataAdapterFill(cmd, dt);

                ChequeCode = "";
                string stRetValue = "";
                foreach (System.Data.DataRow dr in dt.Rows)
                {
                    ChequeCode = dr["ChequeCode"].ToString();
                    long lChequeCounter = Int64.Parse(dr["ChequeCounter"].ToString()) + 1;
                    stRetValue = dr["ChequeCounter"].ToString();
                    stRetValue = lChequeCounter.ToString().PadLeft(stRetValue.Length, '0');
                }

                return stRetValue;
            }

            catch (Exception ex)
            {
                throw base.ThrowException(ex);
            }
        }

        #endregion
    }
}


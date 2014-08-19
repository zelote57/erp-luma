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
	public struct ChargeTypeDetails
	{
		public Int32	ChargeTypeID;
		public string	ChargeTypeCode;
		public string	ChargeType;
		public decimal	ChargeAmount;
		public bool		InPercent;

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
	public class ChargeType : POSConnection
    {
		#region Constructors and Destructors

		public ChargeType()
            : base(null, null)
        {
        }

        public ChargeType(MySqlConnection Connection, MySqlTransaction Transaction) 
            : base(Connection, Transaction)
		{

		}

		#endregion

		#region Insert and Update

		public Int32 Insert(ChargeTypeDetails Details)
		{
			try 
			{
				string SQL = "INSERT INTO tblChargeType (ChargeTypeCode, ChargeType, ChargeAmount, InPercent) " +
					"VALUES (@ChargeTypeCode, @ChargeType, @ChargeAmount, @InPercent) ;";
				  
				MySqlCommand cmd = new MySqlCommand();
				cmd.CommandType = System.Data.CommandType.Text;
				cmd.CommandText = SQL;

                cmd.Parameters.AddWithValue("@ChargeTypeCode", Details.ChargeTypeCode);
                cmd.Parameters.AddWithValue("@ChargeType", Details.ChargeType);
                cmd.Parameters.AddWithValue("@ChargeAmount", Details.ChargeAmount);
                cmd.Parameters.AddWithValue("@InPercent", Details.InPercent);

				base.ExecuteNonQuery(cmd);

				SQL = "SELECT LAST_INSERT_ID();";
				
				cmd.Parameters.Clear(); 
				cmd.CommandText = SQL;
				
				MySqlDataReader myReader = base.ExecuteReader(cmd, System.Data.CommandBehavior.SingleResult);
				
				Int32 iID = 0;

				while (myReader.Read()) 
				{
					iID = myReader.GetInt32(0);
				}

				myReader.Close();

				return iID;
			}

			catch (Exception ex)
			{
				throw base.ThrowException(ex);
			}	
		}

		public void Update(ChargeTypeDetails Details)
		{
			try 
			{
				string SQL=	"UPDATE tblChargeType SET " + 
					"ChargeTypeCode	= @ChargeTypeCode, " +  
					"ChargeType = @ChargeType, " +  
					"ChargeAmount = @ChargeAmount, " +  
					"InPercent = @InPercent " +  
					"WHERE ChargeTypeID = " + Details.ChargeTypeID + ";";

				MySqlCommand cmd = new MySqlCommand();
				cmd.CommandType = System.Data.CommandType.Text;
				cmd.CommandText = SQL;

                cmd.Parameters.AddWithValue("@ChargeTypeCode", Details.ChargeTypeCode);
                cmd.Parameters.AddWithValue("@ChargeType", Details.ChargeType);
                cmd.Parameters.AddWithValue("@ChargeAmount", Details.ChargeAmount);
                cmd.Parameters.AddWithValue("@InPercent", Details.InPercent);
                cmd.Parameters.AddWithValue("@ChargeTypeID", Details.ChargeTypeID);

				base.ExecuteNonQuery(cmd);
			}

			catch (Exception ex)
			{
				throw base.ThrowException(ex);
			}	
		}

        public Int32 Save(ChargeTypeDetails Details)
        {
            try
            {
                string SQL = "CALL procSaveChargeType(@ChargeTypeID, @ChargeTypeCode, @ChargeType, @ChargeAmount, @InPercent, @CreatedOn, @LastModified);";

                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandText = SQL;

                cmd.Parameters.AddWithValue("ChargeTypeID", Details.ChargeTypeID);
                cmd.Parameters.AddWithValue("ChargeTypeCode", Details.ChargeTypeCode);
                cmd.Parameters.AddWithValue("ChargeType", Details.ChargeType);
                cmd.Parameters.AddWithValue("ChargeAmount", Details.ChargeAmount);
                cmd.Parameters.AddWithValue("InPercent", Details.InPercent);
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
				string SQL=	"DELETE FROM tblChargeType WHERE ChargeTypeID IN (" + IDs + ");";
				  
				
	 			
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
					        "ChargeTypeID, " +
					        "ChargeTypeCode, " +
					        "ChargeType, " +
					        "ChargeAmount, " + 
					        "InPercent, " +
                            "CreatedOn, " +
                            "LastModified " +
					       "FROM tblChargeType ";
            return stSQL;
        }

		#region Details

		public ChargeTypeDetails Details(Int32 ChargeTypeID)
		{
			try
			{
				string SQL=	SQLSelect() + "WHERE ChargeTypeID = @ChargeTypeID;";
	 			
				MySqlCommand cmd = new MySqlCommand();
				cmd.CommandType = System.Data.CommandType.Text;
				cmd.CommandText = SQL;

                cmd.Parameters.AddWithValue("@ChargeTypeID", ChargeTypeID);

                string strDataTableName = "tbl" + this.GetType().FullName.Split(new Char[] { '.' })[this.GetType().FullName.Split(new Char[] { '.' }).Length - 1]; System.Data.DataTable dt = new System.Data.DataTable(strDataTableName);
                base.MySqlDataAdapterFill(cmd, dt);

                ChargeTypeDetails Details = new ChargeTypeDetails();
                foreach (System.Data.DataRow dr in dt.Rows)
                {
                    Details.ChargeTypeID = Int32.Parse(dr["ChargeTypeID"].ToString());
                    Details.ChargeTypeCode = dr["ChargeTypeCode"].ToString();
                    Details.ChargeType = dr["ChargeType"].ToString();
                    Details.ChargeAmount = decimal.Parse(dr["ChargeAmount"].ToString());
                    Details.InPercent = bool.Parse(dr["InPercent"].ToString());

                    Details.CreatedOn = DateTime.Parse(dr["CreatedOn"].ToString());
                    Details.LastModified = DateTime.Parse(dr["LastModified"].ToString());
                }

				return Details;
			}

			catch (Exception ex)
			{
				throw base.ThrowException(ex);
			}	
		}
        public ChargeTypeDetails Details(string ChargeTypeCode)
        {
            try
            {
                string SQL = SQLSelect() + "WHERE ChargeTypeCode = @ChargeTypeCode;";

                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandText = SQL;  

                cmd.Parameters.AddWithValue("@ChargeTypeCode", ChargeTypeCode);

                string strDataTableName = "tbl" + this.GetType().FullName.Split(new Char[] { '.' })[this.GetType().FullName.Split(new Char[] { '.' }).Length - 1]; System.Data.DataTable dt = new System.Data.DataTable(strDataTableName);
                base.MySqlDataAdapterFill(cmd, dt);

                ChargeTypeDetails Details = new ChargeTypeDetails();
                foreach (System.Data.DataRow dr in dt.Rows)
                {
                    Details.ChargeTypeID = Int32.Parse(dr["ChargeTypeID"].ToString());
                    Details.ChargeTypeCode = dr["ChargeTypeCode"].ToString();
                    Details.ChargeType = dr["ChargeType"].ToString();
                    Details.ChargeAmount = decimal.Parse(dr["ChargeAmount"].ToString());
                    Details.InPercent = bool.Parse(dr["InPercent"].ToString());

                    Details.CreatedOn = DateTime.Parse(dr["CreatedOn"].ToString());
                    Details.LastModified = DateTime.Parse(dr["LastModified"].ToString());
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

		public System.Data.DataTable DataList(string SortField, SortOption SortOrder)
		{
            string SQL = SQLSelect() + "WHERE 1=1 ORDER BY " + SortField;

            if (SortOrder == SortOption.Ascending)
                SQL += " ASC";
            else
                SQL += " DESC";

            MySqlCommand cmd = new MySqlCommand();
            cmd.CommandType = System.Data.CommandType.Text;
            cmd.CommandText = SQL;

            string strDataTableName = "tbl" + this.GetType().FullName.Split(new Char[] { '.' })[this.GetType().FullName.Split(new Char[] { '.' }).Length - 1]; System.Data.DataTable dt = new System.Data.DataTable(strDataTableName);
            base.MySqlDataAdapterFill(cmd, dt);

			return dt;
		}
		
		public MySqlDataReader List(string SortField, SortOption SortOrder)
		{
			try
			{
				string SQL = SQLSelect() + "WHERE 1=1 ORDER BY " + SortField;

				if (SortOrder == SortOption.Ascending)
					SQL += " ASC";
				else
					SQL += " DESC";

				MySqlCommand cmd = new MySqlCommand();
				cmd.CommandType = System.Data.CommandType.Text;
				cmd.CommandText = SQL;
				
				return base.ExecuteReader(cmd);			
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
				string SQL = SQLSelect() + "WHERE 1=1 " +
					            "AND (ChargeTypeCode LIKE @SearchKey " + 
					            "OR ChargeType LIKE @SearchKey) " +
					            "ORDER BY " + SortField;

				if (SortOrder == SortOption.Ascending)
					SQL += " ASC";
				else
					SQL += " DESC";

				MySqlCommand cmd = new MySqlCommand();
				cmd.CommandType = System.Data.CommandType.Text;
				cmd.CommandText = SQL;
				
				MySqlParameter prmSearchKey = new MySqlParameter("@SearchKey",MySqlDbType.String);
				prmSearchKey.Value = "%" + SearchKey + "%";
				cmd.Parameters.Add(prmSearchKey);

				return base.ExecuteReader(cmd);			
			}
			catch (Exception ex)
			{
				throw base.ThrowException(ex);
			}	
		}
		

		#endregion
	}
}


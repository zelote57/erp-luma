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
	public struct CharacteristicsDetails
	{
		public int CharacteristicsID;
		public string CharacteristicsCode;
		public string CharacteristicsName;
	}

	[StrongNameIdentityPermissionAttribute(SecurityAction.LinkDemand,
		 PublicKey = "002400000480000094000000060200000024000" +
		 "052534131000400000100010053D785642F9F960B43157E0380" +
		 "F393BEE53E8DFAFBF441366C1B6F8B48D9DDF0D527B1F3B21EA" +
		 "E85D2FDB664CE81EB8A87DBE4C589D6F4202FE2B7D4B978BB69" +
		 "684874612CB9B8DB7A0339400A9C4E68277884B07817363D242" +
		 "E3696F9FACDBEA831810AE6DC9EDCA91A7B5DA12FE7BF65D113" +
		 "FF52834EAFB5A7A1FDFD5851A3")]
	public class Characteristics : POSConnection
    {
		#region Constructors and Destructors

		public Characteristics()
            : base(null, null)
        {
        }

        public Characteristics(MySqlConnection Connection, MySqlTransaction Transaction) 
            : base(Connection, Transaction)
		{

		}

		#endregion

		#region Insert and Update

		public Int32 Insert(CharacteristicsDetails Details)
		{
			try 
			{
                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandType = System.Data.CommandType.Text;

				string SQL = "INSERT INTO tblCharacteristics (CharacteristicsCode, CharacteristicsName) VALUES (@CharacteristicsCode, @CharacteristicsName);";
				  
				cmd.Parameters.AddWithValue("@CharacteristicsCode", Details.CharacteristicsCode);
                cmd.Parameters.AddWithValue("@CharacteristicsName", Details.CharacteristicsName);

                cmd.CommandText = SQL;
				base.ExecuteNonQuery(cmd);

                return Int32.Parse(base.getLAST_INSERT_ID(this));
			}
			catch (Exception ex)
			{
				throw base.ThrowException(ex);
			}	
		}

		public void Update(CharacteristicsDetails Details)
		{
			try 
			{
                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandType = System.Data.CommandType.Text;

				string SQL=	"UPDATE tblCharacteristics SET " + 
							"CharacteristicsCode = @CharacteristicsCode, " +  
							"CharacteristicsName = @CharacteristicsName " +  
							"WHERE CharacteristicsID = @CharacteristicsID;";

                cmd.Parameters.AddWithValue("@CharacteristicsCode", Details.CharacteristicsCode);
                cmd.Parameters.AddWithValue("@CharacteristicsName", Details.CharacteristicsName);
                cmd.Parameters.AddWithValue("@CharacteristicsID", Details.CharacteristicsID);

                cmd.CommandText = SQL;
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
				string SQL=	"DELETE FROM tblCharacteristics WHERE CharacteristicsID IN (" + IDs + ");";
				  
				
	 			
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

		public CharacteristicsDetails Details(Int32 CharacteristicsID)
		{
			try
			{
                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandType = System.Data.CommandType.Text;

				string SQL=	"SELECT " +
								"CharacteristicsID, " +
								"CharacteristicsCode, " +
								"CharacteristicsName " +
							"FROM tblCharacteristics " +
							"WHERE CharacteristicsID = @CharacteristicsID;";

                cmd.Parameters.AddWithValue("@CharacteristicsID", CharacteristicsID);

                cmd.CommandText = SQL;
                string strDataTableName = "tbl" + this.GetType().FullName.Split(new Char[] { '.' })[this.GetType().FullName.Split(new Char[] { '.' }).Length - 1]; System.Data.DataTable dt = new System.Data.DataTable(strDataTableName);
                base.MySqlDataAdapterFill(cmd, dt);

                CharacteristicsDetails Details = new CharacteristicsDetails();
                foreach (System.Data.DataRow dr in dt.Rows)
                {
                    Details.CharacteristicsID = Int32.Parse(dr["CharacteristicsID"].ToString());
                    Details.CharacteristicsCode = dr["CharacteristicsCode"].ToString();
                    Details.CharacteristicsName = dr["CharacteristicsName"].ToString();
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

        public System.Data.DataTable DataList(string SearchKey = null, string SortField = "CharacteristicsCode", SortOption SortOrder = SortOption.Ascending, Int32 limit = 0)
		{
            try
            {
                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandType = System.Data.CommandType.Text;

                string SQL = "SELECT * FROM tblCharacteristics ";

                if (!string.IsNullOrEmpty(SearchKey))
                {
                    SQL += "CharacteristicsName LIKE @SearchKey";
                    cmd.Parameters.AddWithValue("@SearchKey", SearchKey);
                }

                SQL += "ORDER BY " + (!string.IsNullOrEmpty(SortField) ? SortField : "BranchCode") + " ";
                SQL += SortOrder == SortOption.Ascending ? "ASC " : "DESC ";
                SQL += limit == 0 ? "" : "LIMIT " + limit.ToString() + " ";

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
	}
}


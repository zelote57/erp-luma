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
				string SQL = "INSERT INTO tblCharacteristics (CharacteristicsCode, CharacteristicsName) VALUES (@CharacteristicsCode, @CharacteristicsName);";
				  
				
	 			
				MySqlCommand cmd = new MySqlCommand();
				
				
				cmd.CommandType = System.Data.CommandType.Text;
				cmd.CommandText = SQL;
				
				MySqlParameter prmCharacteristicsCode = new MySqlParameter("@CharacteristicsCode",MySqlDbType.String);			
				prmCharacteristicsCode.Value = Details.CharacteristicsCode;
				cmd.Parameters.Add(prmCharacteristicsCode);

				MySqlParameter prmCharacteristicsName = new MySqlParameter("@CharacteristicsName",MySqlDbType.String);			
				prmCharacteristicsName.Value = Details.CharacteristicsName;
				cmd.Parameters.Add(prmCharacteristicsName);

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

		public void Update(CharacteristicsDetails Details)
		{
			try 
			{
				string SQL=	"UPDATE tblCharacteristics SET " + 
							"CharacteristicsCode = @CharacteristicsCode, " +  
							"CharacteristicsName = @CharacteristicsName " +  
							"WHERE CharacteristicsID = @CharacteristicsID;";
				  
				
	 			
				MySqlCommand cmd = new MySqlCommand();
				
				
				cmd.CommandType = System.Data.CommandType.Text;
				cmd.CommandText = SQL;
				
				MySqlParameter prmCharacteristicsCode = new MySqlParameter("@CharacteristicsCode",MySqlDbType.String);			
				prmCharacteristicsCode.Value = Details.CharacteristicsCode;
				cmd.Parameters.Add(prmCharacteristicsCode);

				MySqlParameter prmCharacteristicsName = new MySqlParameter("@CharacteristicsName",MySqlDbType.String);			
				prmCharacteristicsName.Value = Details.CharacteristicsName;
				cmd.Parameters.Add(prmCharacteristicsName);

				MySqlParameter prmCharacteristicsID = new MySqlParameter("@CharacteristicsID",MySqlDbType.Int16);			
				prmCharacteristicsID.Value = Details.CharacteristicsID;
				cmd.Parameters.Add(prmCharacteristicsID);

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
				string SQL=	"SELECT " +
								"CharacteristicsID, " +
								"CharacteristicsCode, " +
								"CharacteristicsName " +
							"FROM tblCharacteristics " +
							"WHERE CharacteristicsID = @CharacteristicsID;";
				  
				
	 			
				MySqlCommand cmd = new MySqlCommand();
				
				
				cmd.CommandType = System.Data.CommandType.Text;
				cmd.CommandText = SQL;

				MySqlParameter prmCharacteristicsID = new MySqlParameter("@CharacteristicsID",MySqlDbType.Int16);
				prmCharacteristicsID.Value = CharacteristicsID;
				cmd.Parameters.Add(prmCharacteristicsID);

				MySqlDataReader myReader = base.ExecuteReader(cmd, System.Data.CommandBehavior.SingleResult);
				
				CharacteristicsDetails Details = new CharacteristicsDetails();

				while (myReader.Read()) 
				{
					Details.CharacteristicsID = CharacteristicsID;
					Details.CharacteristicsCode = myReader.GetString(1);
					Details.CharacteristicsName = myReader.GetString(2);
				}

				myReader.Close();

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
				string SQL = "SELECT * FROM tblCharacteristics ORDER BY " + SortField;

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

		public System.Data.DataTable DataList(string SortField, SortOption SortOrder)
		{
			MySqlDataReader myReader = List(SortField,SortOption.Ascending);
			
			System.Data.DataTable dt = new System.Data.DataTable("tblCharacteristics");

			dt.Columns.Add("CharacteristicsID");
			dt.Columns.Add("CharacteristicsCode");
			dt.Columns.Add("CharacteristicsName");
				
			while (myReader.Read())
			{
				System.Data.DataRow dr = dt.NewRow();

				dr["CharacteristicsID"] = myReader.GetInt16(0);
				dr["CharacteristicsCode"] = myReader.GetString(1);
				dr["CharacteristicsName"] = myReader.GetString(2);
					
				dt.Rows.Add(dr);
			}
			
			myReader.Close();

			return dt;
		}

		public MySqlDataReader Search(string SearchKey, string SortField, SortOption SortOrder)
		{
			try
			{
				string SQL ="SELECT " +
								"CharacteristicsID, " +
								"CharacteristicsCode, " +
								"CharacteristicsName " +
							"FROM tblCharacteristics " +
							"WHERE CharacteristicsName LIKE @SearchKey " +
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
		
		public System.Data.DataTable DataSearch(string SearchKey, string SortField, SortOption SortOrder)
		{
			MySqlDataReader myReader = Search(SearchKey,SortField,SortOption.Ascending);
			
			System.Data.DataTable dt = new System.Data.DataTable("tblCharacteristics");

			dt.Columns.Add("CharacteristicsID");
			dt.Columns.Add("CharacteristicsCode");
			dt.Columns.Add("CharacteristicsName");
				
			while (myReader.Read())
			{
				System.Data.DataRow dr = dt.NewRow();

				dr["CharacteristicsID"] = myReader.GetInt16(0);
				dr["CharacteristicsCode"] = myReader.GetString(1);
				dr["CharacteristicsName"] = myReader.GetString(2);
					
				dt.Rows.Add(dr);
			}
			
			myReader.Close();

			return dt;
		}

		#endregion
	}
}


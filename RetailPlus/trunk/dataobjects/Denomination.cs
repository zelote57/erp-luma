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
	public struct DenominationDetails
	{
		public Int32 DenominationID;
		public string DenominationCode;
		public decimal DenominationValue;
		public string ImagePath;
	}

	[StrongNameIdentityPermissionAttribute(SecurityAction.LinkDemand,
		 PublicKey = "002400000480000094000000060200000024000" +
		 "052534131000400000100010053D785642F9F960B43157E0380" +
		 "F393BEE53E8DFAFBF441366C1B6F8B48D9DDF0D527B1F3B21EA" +
		 "E85D2FDB664CE81EB8A87DBE4C589D6F4202FE2B7D4B978BB69" +
		 "684874612CB9B8DB7A0339400A9C4E68277884B07817363D242" +
		 "E3696F9FACDBEA831810AE6DC9EDCA91A7B5DA12FE7BF65D113" +
		 "FF52834EAFB5A7A1FDFD5851A3")]
	public class Denominations : POSConnection
	{
		#region Constructors and Destructors

		public Denominations()
            : base(null, null)
        {
        }

        public Denominations(MySqlConnection Connection, MySqlTransaction Transaction) 
            : base(Connection, Transaction)
		{

		}

		#endregion

		#region Insert and Update

		public Int32 Insert(DenominationDetails Details)
		{
			try 
			{
				string SQL = "INSERT INTO tblDenomination (DenominationCode, DenominationValue, ImagePath) VALUES (@DenominationCode, @DenominationValue, @ImagePath);";
				  
				
	 			
				MySqlCommand cmd = new MySqlCommand();
				
				
				cmd.CommandType = System.Data.CommandType.Text;
				cmd.CommandText = SQL;
				
				MySqlParameter prmDenominationCode = new MySqlParameter("@DenominationCode",MySqlDbType.String);			
				prmDenominationCode.Value = Details.DenominationCode;
				cmd.Parameters.Add(prmDenominationCode);

				MySqlParameter prmDenominationValue = new MySqlParameter("@DenominationValue",MySqlDbType.Decimal);			
				prmDenominationValue.Value = Details.DenominationValue;
				cmd.Parameters.Add(prmDenominationValue);

				MySqlParameter prmImagePath = new MySqlParameter("@ImagePath",MySqlDbType.String);			
				prmImagePath.Value = Details.ImagePath;
				cmd.Parameters.Add(prmImagePath);

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
				
				
					

				
				
				

				throw ex;
			}	
		}

		public void Update(DenominationDetails Details)
		{
			try 
			{
				string SQL=	"UPDATE tblDenomination SET " + 
								"DenominationCode = @DenominationCode, " +  
								"ImagePath = @ImagePath " +  
							"WHERE DenominationID = @DenominationID;";
				  
				
	 			
				MySqlCommand cmd = new MySqlCommand();
				
				
				cmd.CommandType = System.Data.CommandType.Text;
				cmd.CommandText = SQL;
				
				MySqlParameter prmDenominationCode = new MySqlParameter("@DenominationCode",MySqlDbType.String);			
				prmDenominationCode.Value = Details.DenominationCode;
				cmd.Parameters.Add(prmDenominationCode);

				MySqlParameter prmImagePath = new MySqlParameter("@ImagePath",MySqlDbType.String);			
				prmImagePath.Value = Details.ImagePath;
				cmd.Parameters.Add(prmImagePath);

				MySqlParameter prmDenominationID = new MySqlParameter("@DenominationID",MySqlDbType.Int16);			
				prmDenominationID.Value = Details.DenominationID;
				cmd.Parameters.Add(prmDenominationID);

				base.ExecuteNonQuery(cmd);
			}

			catch (Exception ex)
			{
				
				
					

				
				
				

				throw ex;
			}	
		}


		#endregion

		#region Delete

		public bool Delete(string IDs)
		{
			try 
			{
				string SQL=	"DELETE FROM tblDenomination WHERE DenominationID IN (" + IDs + ");";
				  
				
	 			
				MySqlCommand cmd = new MySqlCommand();
				
				
				cmd.CommandType = System.Data.CommandType.Text;
				cmd.CommandText = SQL;

				base.ExecuteNonQuery(cmd);

				return true;
			}

			catch (Exception ex)
			{
				
				
					

				
				
				

				throw ex;
			}	
		}


		#endregion

		#region Details

		public DenominationDetails Details(Int32 DenominationID)
		{
			try
			{
				string SQL=	"SELECT " +
								"DenominationID, " +
								"DenominationCode, " +
								"ImagePath " +
							"FROM tblDenomination " +
							"WHERE DenominationID = @DenominationID;";
				  
				
	 			
				MySqlCommand cmd = new MySqlCommand();
				
				
				cmd.CommandType = System.Data.CommandType.Text;
				cmd.CommandText = SQL;

				MySqlParameter prmDenominationID = new MySqlParameter("@DenominationID",MySqlDbType.Int16);
				prmDenominationID.Value = DenominationID;
				cmd.Parameters.Add(prmDenominationID);

				MySqlDataReader myReader = base.ExecuteReader(cmd, System.Data.CommandBehavior.SingleResult);
				
				DenominationDetails Details = new DenominationDetails();

				while (myReader.Read()) 
				{
					Details.DenominationID = myReader.GetInt32("DenominationID");
					Details.DenominationCode = "" + myReader["DenominationCode"].ToString();
					Details.ImagePath = "" + myReader["ImagePath"].ToString();
				}

				myReader.Close();

				return Details;
			}

			catch (Exception ex)
			{
				
				
					

				
				
				

				throw ex;
			}	
		}


		#endregion

		#region Streams

		public MySqlDataReader List(string SortField, SortOption SortOrder)
		{
			try
			{
				string SQL = "SELECT DenominationID, " +
					"DenominationCode," +
					"DenominationValue, " +
					"ImagePath " +
					"FROM tblDenomination ORDER BY " + SortField;

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
				
				
					

				
				
				

				throw ex;
			}	
		}

		public System.Data.DataTable ListForCashCount(string SortField, SortOption SortOrder)
		{
			try
			{
				MySqlDataReader myReader = List(SortField,SortOption.Ascending);

                string strDataTableName = "tbl" + this.GetType().FullName.Split(new Char[] { '.' })[this.GetType().FullName.Split(new Char[] { '.' }).Length - 1];
                System.Data.DataTable dt = new System.Data.DataTable(strDataTableName);

				dt.Columns.Add("DenominationID");
				dt.Columns.Add("DenominationCode");
				dt.Columns.Add("DenominationValue");
				dt.Columns.Add("ImagePath");
				dt.Columns.Add("DenominationCount");
				dt.Columns.Add("DenominationAmount");
				
				while (myReader.Read())
				{
					System.Data.DataRow dr = dt.NewRow();

					dr["DenominationID"] = myReader.GetInt32("DenominationID");
					dr["DenominationCode"] = "" + myReader["DenominationCode"].ToString();
					dr["DenominationValue"] = myReader.GetDecimal("DenominationValue").ToString("#,##0.#0");
					dr["ImagePath"] = "" + myReader["ImagePath"].ToString();
					dr["DenominationCount"] = 0;
					dr["DenominationAmount"] = 0.00;
					
					dt.Rows.Add(dr);
				}
			
				myReader.Close();

				return dt;
			}
			catch (Exception ex)
			{
				throw ex;
			}	
		}

		public System.Data.DataTable DataList(string SortField, SortOption SortOrder)
		{
			MySqlDataReader myReader = List(SortField,SortOption.Ascending);
			
			System.Data.DataTable dt = new System.Data.DataTable("tblDenomination");

			dt.Columns.Add("DenominationID");
			dt.Columns.Add("DenominationCode");
			dt.Columns.Add("DenominationValue");
			dt.Columns.Add("ImagePath");
				
			while (myReader.Read())
			{
				System.Data.DataRow dr = dt.NewRow();

				dr["DenominationID"] = myReader.GetInt32("DenominationID");
				dr["DenominationCode"] = "" + myReader["DenominationCode"].ToString();
				dr["DenominationValue"] = myReader.GetDecimal("DenominationValue");
				dr["ImagePath"] = "" + myReader["ImagePath"].ToString();
					
				dt.Rows.Add(dr);
			}
			
			myReader.Close();

			return dt;
		}

		public MySqlDataReader Search(string SearchKey, string SortField, SortOption SortOrder)
		{
			try
			{
				string SQL ="SELECT DenominationID, " +
								"DenominationCode, " +
								"ImagePath " +
							"FROM tblDenomination " +
							"WHERE ImagePath LIKE '%" + SearchKey + "%' " +
							"ORDER BY " + SortField;

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
				
				
					

				
				
				

				throw ex;
			}	
		}
		
		public System.Data.DataTable DataSearch(string SearchKey, string SortField, SortOption SortOrder)
		{
			MySqlDataReader myReader = Search(SearchKey,SortField,SortOption.Ascending);
			
			System.Data.DataTable dt = new System.Data.DataTable("tblDenomination");

			dt.Columns.Add("DenominationID");
			dt.Columns.Add("DenominationCode");
			dt.Columns.Add("ImagePath");
				
			while (myReader.Read())
			{
				System.Data.DataRow dr = dt.NewRow();

				dr["DenominationID"] = myReader.GetInt32("DenominationID");
				dr["DenominationCode"] = "" + myReader["DenominationCode"].ToString();
				dr["ImagePath"] = "" + myReader["ImagePath"].ToString();
					
				dt.Rows.Add(dr);
			}
			
			myReader.Close();

			return dt;
		}

		
		#endregion
	}
}


using System;
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
	public struct AccessRightsDetails
	{
		public Int64 UID;
		public Int16 TranTypeID;
		public bool Read;
		public bool Write;
	}

	[StrongNameIdentityPermissionAttribute(SecurityAction.LinkDemand,
		 PublicKey = "002400000480000094000000060200000024000" +
		 "052534131000400000100010053D785642F9F960B43157E0380" +
		 "F393BEE53E8DFAFBF441366C1B6F8B48D9DDF0D527B1F3B21EA" +
		 "E85D2FDB664CE81EB8A87DBE4C589D6F4202FE2B7D4B978BB69" +
		 "684874612CB9B8DB7A0339400A9C4E68277884B07817363D242" +
		 "E3696F9FACDBEA831810AE6DC9EDCA91A7B5DA12FE7BF65D113" +
		 "FF52834EAFB5A7A1FDFD5851A3")]
	public struct AllowedRights
	{
		public bool Read;
		public bool Write;
	}

	[StrongNameIdentityPermissionAttribute(SecurityAction.LinkDemand,
		 PublicKey = "002400000480000094000000060200000024000" +
		 "052534131000400000100010053D785642F9F960B43157E0380" +
		 "F393BEE53E8DFAFBF441366C1B6F8B48D9DDF0D527B1F3B21EA" +
		 "E85D2FDB664CE81EB8A87DBE4C589D6F4202FE2B7D4B978BB69" +
		 "684874612CB9B8DB7A0339400A9C4E68277884B07817363D242" +
		 "E3696F9FACDBEA831810AE6DC9EDCA91A7B5DA12FE7BF65D113" +
		 "FF52834EAFB5A7A1FDFD5851A3")]
	public class AccessRights : POSConnection
	{
		#region Contructors and Destructors
		
		public AccessRights()
            : base(null, null)
        {
        }

        public AccessRights(MySqlConnection Connection, MySqlTransaction Transaction) 
            : base(Connection, Transaction)
		{

		}

		#endregion

		#region DataList

		private MySqlDataReader ListAccessTypes(string SortField, SortOption SortOrder)
		{
			try
			{
				string SQL =	"SELECT		TypeID, TypeName, Remarks " +
								"FROM		sysAccessTypes " + 
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

		public System.Data.DataTable DataList(Int64 UID, string SortField, SortOption SortOrder)
		{
			MySqlDataReader myReader = ListAccessTypes(SortField,SortOption.Ascending);
			
			System.Data.DataTable dt = new System.Data.DataTable("sysAccessRights");

			dt.Columns.Add("TypeID");
			dt.Columns.Add("TypeName");
			dt.Columns.Add("Remarks");
			dt.Columns.Add("Read");
			dt.Columns.Add("Write");

			while (myReader.Read())
			{
				System.Data.DataRow dr = dt.NewRow();

				dr["TypeID"] = myReader.GetInt16("TypeID");
				dr["TypeName"] = "" + myReader["TypeName"].ToString();
				dr["Remarks"] = "" + myReader["Remarks"].ToString();
								
				AllowedRights rights = GetReadWrite(UID,Convert.ToInt16(dr["TypeID"]));
  
				dr["Read"] = rights.Read;
				dr["Write"] = rights.Write;
					
				dt.Rows.Add(dr);
			}
			
			myReader.Close();

			return dt;
		}
        public System.Data.DataTable DataList(string Category, long UID, string SortField, SortOption SortOrder)
        {
            AccessType clsAccessType = new AccessType(base.Connection, base.Transaction);
            System.Data.DataTable dtAccessType = clsAccessType.DataList(Category, "Category, SequenceNo", SortOption.Ascending);

            System.Data.DataTable dt = new System.Data.DataTable("sysAccessRights");

            dt.Columns.Add("TypeID");
            dt.Columns.Add("TypeName");
            dt.Columns.Add("Remarks");
            dt.Columns.Add("Enabled");
            dt.Columns.Add("SequenceNo");
            dt.Columns.Add("Read");
            dt.Columns.Add("Write");

            foreach (System.Data.DataRow dr in dtAccessType.Rows)
            {
                System.Data.DataRow drNew = dt.NewRow();

                drNew["TypeID"] = Convert.ToInt16(dr["TypeID"]);
                drNew["TypeName"] = "" + dr["TypeName"].ToString();
                drNew["Remarks"] = "" + dr["Remarks"].ToString();
                drNew["Enabled"] = "" + dr["Enabled"].ToString();
                drNew["SequenceNo"] = "" + dr["SequenceNo"].ToString();

                AllowedRights rights = GetReadWrite(UID, Convert.ToInt16(drNew["TypeID"]));

                drNew["Read"] = rights.Read;
                drNew["Write"] = rights.Write;

                dt.Rows.Add(drNew);
            }
            return dt;
        }

		public AllowedRights GetReadWrite(Int64 UID,int TranTypeID)
		{
			try
			{
				string SQL ="SELECT IFNULL(AllowRead,0) as 'Read', IFNULL(AllowWrite,0) as 'Write' " +
                                "FROM sysAccessRights a INNER JOIN sysAccessTypes b ON a.TranTypeID = b.TypeID " +
							"WHERE UID = @UID AND TranTypeID = @TranTypeID AND Enabled=1 ";

				MySqlCommand cmd = new MySqlCommand();
				cmd.CommandType = System.Data.CommandType.Text;
				cmd.CommandText = SQL;
				
                cmd.Parameters.AddWithValue("@UID", UID);
				cmd.Parameters.AddWithValue("@TranTypeID", TranTypeID);

				MySqlDataReader myReader = base.ExecuteReader(cmd, System.Data.CommandBehavior.SingleResult);
				
				AllowedRights rights = new AllowedRights();

				while (myReader.Read())
				{
					rights.Read = myReader.GetBoolean("Read");
					rights.Write  = myReader.GetBoolean("Write");
				}
			
				myReader.Close();

				return rights;

			}
			catch (Exception ex)
			{
				throw ex;
			}		
		}

		#endregion

		#region Update

		private bool IsExisting(Int64 UID, int TranTypeID)
		{
			try
			{
				string SQL ="SELECT COUNT(*) FROM sysAccessRights " +
							"WHERE UID = @UID AND TranTypeID = @TranTypeID";

				MySqlCommand cmd = new MySqlCommand();
				cmd.CommandType = System.Data.CommandType.Text;
				cmd.CommandText = SQL;

				MySqlParameter prmUID = new MySqlParameter("@UID",System.Data.DbType.Int64);			
				prmUID.Value = UID;
				cmd.Parameters.Add(prmUID);

				MySqlParameter prmTranTypeID = new MySqlParameter("@TranTypeID",MySqlDbType.Int16);			
				prmTranTypeID.Value = TranTypeID;
				cmd.Parameters.Add(prmTranTypeID);

				MySqlDataReader myReader = base.ExecuteReader(cmd, System.Data.CommandBehavior.SingleResult);
				
				bool existing = false;
				
				while (myReader.Read())
				{
					if (myReader.GetInt32(0)>0)
						existing = true;
				}
			
				myReader.Close();

				return existing;
			}
			catch (Exception ex)
			{
				throw ex;
			}	
		}

		public void Modify(AccessRightsDetails Details)
		{
			try 
			{
				string SQL = string.Empty;

				if (IsExisting(Details.UID,Details.TranTypeID))
				{
					Update(Details); 
				}
				else
				{
					Insert(Details);
				}
 
			}

			catch (Exception ex)
			{
				throw ex;
			}	
		}

		public void Insert(AccessRightsDetails Details)
		{
			try 
			{
				string SQL = string.Empty;

				SQL	=	"INSERT INTO sysAccessRights " +
						"(UID, TranTypeID, AllowRead, AllowWrite) " +
						"VALUES (@UID, @TranTypeID, @Read, @Write)";
	 			
				MySqlCommand cmd = new MySqlCommand();
				cmd.CommandType = System.Data.CommandType.Text;
				cmd.CommandText = SQL;
				
				MySqlParameter prmUID = new MySqlParameter("@UID",System.Data.DbType.Int64);			
				prmUID.Value = Details.UID;
				cmd.Parameters.Add(prmUID);

				MySqlParameter prmTranTypeID = new MySqlParameter("@TranTypeID",MySqlDbType.Int16);			
				prmTranTypeID.Value = Details.TranTypeID;
				cmd.Parameters.Add(prmTranTypeID);

				MySqlParameter prmRead = new MySqlParameter("@Read",MySqlDbType.String);			
				if (Details.Read)
					prmRead.Value = "1";
				else
					prmRead.Value = "0";
				cmd.Parameters.Add(prmRead);

				MySqlParameter prmWrite = new MySqlParameter("@Write",MySqlDbType.String);			
				if (Details.Write)
					prmWrite.Value = "1";
				else
					prmWrite.Value = "0";
				cmd.Parameters.Add(prmWrite);

				base.ExecuteNonQuery(cmd);
			}

			catch (Exception ex)
			{
				throw ex;
			}	
		}

		private void Update(AccessRightsDetails Details)
		{
			try 
			{
				string SQL = string.Empty;

				SQL	=	"UPDATE sysAccessRights SET " +
						    "AllowRead	 = @Read, " +
						    "AllowWrite = @Write " +
						"WHERE 1=1 " +
						    "AND UID = @UID " +
						    "AND TranTypeID = @TranTypeID";
				
				MySqlCommand cmd = new MySqlCommand();
				cmd.CommandType = System.Data.CommandType.Text;
				cmd.CommandText = SQL;
				
				MySqlParameter prmRead = new MySqlParameter("@Read",MySqlDbType.String);			
				if (Details.Read)
					prmRead.Value = "1";
				else
					prmRead.Value = "0";
				cmd.Parameters.Add(prmRead);

				MySqlParameter prmWrite = new MySqlParameter("@Write",MySqlDbType.String);			
				if (Details.Write)
					prmWrite.Value = "1";
				else
					prmWrite.Value = "0";
				cmd.Parameters.Add(prmWrite);

				MySqlParameter prmUID = new MySqlParameter("@UID",System.Data.DbType.Int64);			
				prmUID.Value = Details.UID;
				cmd.Parameters.Add(prmUID);

				MySqlParameter prmTranTypeID = new MySqlParameter("@TranTypeID",MySqlDbType.Int16);			
				prmTranTypeID.Value = Details.TranTypeID;
				cmd.Parameters.Add(prmTranTypeID);

				base.ExecuteNonQuery(cmd);
			}

			catch (Exception ex)
			{
				throw ex;
			}	
		}

		#endregion

		#region Details

		public AccessRightsDetails Details(Int64 UID, Int16 TranTypeID)
		{
			try
			{
				AllowedRights rights = new AllowedRights();				
				rights = GetReadWrite(UID,TranTypeID);
 			
				AccessRightsDetails Details = new AccessRightsDetails();
	
				Details.UID = UID;
				Details.TranTypeID = TranTypeID;
				Details.Read = rights.Read;
				Details.Write = rights.Write; 

				return Details;
			}

			catch (Exception ex)
			{
				throw ex;
			}	
		}

		#endregion
	}
}


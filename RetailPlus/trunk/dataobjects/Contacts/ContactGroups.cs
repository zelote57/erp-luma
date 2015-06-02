using System;
using System.Security.Permissions;
using MySql.Data.MySqlClient;

namespace AceSoft.RetailPlus.Data
{
	public enum ContactGroupCategory
	{
		CUSTOMER = 1,
		SUPPLIER = 2,
		BOTH = 3,
        AGENT = 4,
        TABLES = 5
	}

	[StrongNameIdentityPermissionAttribute(SecurityAction.LinkDemand,
		 PublicKey = "002400000480000094000000060200000024000" +
		 "052534131000400000100010053D785642F9F960B43157E0380" +
		 "F393BEE53E8DFAFBF441366C1B6F8B48D9DDF0D527B1F3B21EA" +
		 "E85D2FDB664CE81EB8A87DBE4C589D6F4202FE2B7D4B978BB69" +
		 "684874612CB9B8DB7A0339400A9C4E68277884B07817363D242" +
		 "E3696F9FACDBEA831810AE6DC9EDCA91A7B5DA12FE7BF65D113" +
		 "FF52834EAFB5A7A1FDFD5851A3")]
	public struct ContactGroupDetails
	{
		public Int32 ContactGroupID;
		public string ContactGroupCode;
		public string ContactGroupName;
		public ContactGroupCategory ContactGroupCategory;

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
	public class ContactGroups : POSConnection
    {
		#region Constructors and Destructors

		public ContactGroups()
            : base(null, null)
        {
        }

        public ContactGroups(MySqlConnection Connection, MySqlTransaction Transaction) 
            : base(Connection, Transaction)
		{

		}

		#endregion

		#region Insert and Update

		public Int32 Insert(ContactGroupDetails Details)
		{
			try 
			{
				string SQL = "INSERT INTO tblContactGroup (ContactGroupCode, ContactGroupName, ContactGroupCategory) VALUES (@ContactGroupCode, @ContactGroupName, @ContactGroupCategory);";
				  
				MySqlCommand cmd = new MySqlCommand();
				cmd.CommandType = System.Data.CommandType.Text;
				cmd.CommandText = SQL;
				
				MySqlParameter prmContactGroupCode = new MySqlParameter("@ContactGroupCode",MySqlDbType.String);			
				prmContactGroupCode.Value = Details.ContactGroupCode;
				cmd.Parameters.Add(prmContactGroupCode);

				MySqlParameter prmContactGroupName = new MySqlParameter("@ContactGroupName",MySqlDbType.String);			
				prmContactGroupName.Value = Details.ContactGroupName;
				cmd.Parameters.Add(prmContactGroupName);
     
				MySqlParameter prmContactGroupCategory = new MySqlParameter("@ContactGroupCategory",MySqlDbType.Int16);			
				prmContactGroupCategory.Value = Details.ContactGroupCategory.ToString("d");
				cmd.Parameters.Add(prmContactGroupCategory);
				
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
				
				
				{
					
					
					
					
				}

				throw base.ThrowException(ex);
			}	
		}

		public void Update(ContactGroupDetails Details)
		{
			try 
			{
				string SQL = "UPDATE tblContactGroup SET " + 
					"ContactGroupCode		= @ContactGroupCode, " +
					"ContactGroupName		= @ContactGroupName, " +
					"ContactGroupCategory	= @ContactGroupCategory " +
					"WHERE ContactGroupID = @ContactGroupID;";
				  
				
	 			
				MySqlCommand cmd = new MySqlCommand();
				
				
				cmd.CommandType = System.Data.CommandType.Text;
				cmd.CommandText = SQL;

				MySqlParameter prmContactGroupCode = new MySqlParameter("@ContactGroupCode",MySqlDbType.String);			
				prmContactGroupCode.Value = Details.ContactGroupCode;
				cmd.Parameters.Add(prmContactGroupCode);		

				MySqlParameter prmContactGroupName = new MySqlParameter("@ContactGroupName",MySqlDbType.String);			
				prmContactGroupName.Value = Details.ContactGroupName;
				cmd.Parameters.Add(prmContactGroupName);

				MySqlParameter prmContactGroupID = new MySqlParameter("@ContactGroupID",MySqlDbType.Int16);			
				prmContactGroupID.Value = Details.ContactGroupID;
				cmd.Parameters.Add(prmContactGroupID);

				MySqlParameter prmContactGroupCategory = new MySqlParameter("@ContactGroupCategory",MySqlDbType.Int16);			
				prmContactGroupCategory.Value = Details.ContactGroupCategory.ToString("d");
				cmd.Parameters.Add(prmContactGroupCategory);

				base.ExecuteNonQuery(cmd);
			}

			catch (Exception ex)
			{
				
				
				{
					
					
					
					
				}

				throw base.ThrowException(ex);
			}	
		}

        public Int32 Save(ContactGroupDetails Details)
        {
            try
            {
                string SQL = "CALL procSaveContactGroup(@ContactGroupID, @ContactGroupCode, @ContactGroupName, @ContactGroupCategory, @CreatedOn, @LastModified);";

                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandText = SQL;

                cmd.Parameters.AddWithValue("ContactGroupID", Details.ContactGroupID);
                cmd.Parameters.AddWithValue("ContactGroupCode", Details.ContactGroupCode);
                cmd.Parameters.AddWithValue("ContactGroupName", Details.ContactGroupName);
                cmd.Parameters.AddWithValue("ContactGroupCategory", Details.ContactGroupCategory.ToString("d"));
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
				string SQL=	"DELETE FROM tblContactGroup WHERE ContactGroupID IN (" + IDs + ");";
				  
				
	 			
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
            string stSQL ="SELECT " +
					        "ContactGroupID, " +
					        "ContactGroupCode, " +
					        "ContactGroupName, " +
					        "ContactGroupCategory " +
                        "FROM tblContactGroup "; ;

            return stSQL;
        }

		#region Details

		public ContactGroupDetails Details(Int32 ContactGroupID)
		{
			try
			{
                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandType = System.Data.CommandType.Text;

                string SQL = SQLSelect() + "WHERE ContactGroupID = @ContactGroupID;";

                cmd.Parameters.AddWithValue("@ContactGroupID", ContactGroupID);

                cmd.CommandText = SQL;
                string strDataTableName = "tbl" + this.GetType().FullName.Split(new Char[] { '.' })[this.GetType().FullName.Split(new Char[] { '.' }).Length - 1]; System.Data.DataTable dt = new System.Data.DataTable(strDataTableName);
                base.MySqlDataAdapterFill(cmd, dt);

                ContactGroupDetails Details = setDetails(dt);

				return Details;
			}
			catch (Exception ex)
			{
				throw base.ThrowException(ex);
			}	
		}
        public ContactGroupDetails Details(string ContactGroupCode)
        {
            try
            {
                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandType = System.Data.CommandType.Text;

                string SQL = SQLSelect() + "WHERE ContactGroupCode = @ContactGroupCode;";

                cmd.Parameters.AddWithValue("@ContactGroupCode", ContactGroupCode);

                cmd.CommandText = SQL;
                string strDataTableName = "tbl" + this.GetType().FullName.Split(new Char[] { '.' })[this.GetType().FullName.Split(new Char[] { '.' }).Length - 1]; System.Data.DataTable dt = new System.Data.DataTable(strDataTableName);
                base.MySqlDataAdapterFill(cmd, dt);

                ContactGroupDetails Details = setDetails(dt);

                return Details;
            }
            catch (Exception ex)
            {
                throw base.ThrowException(ex);
            }
        }
        public ContactGroupDetails DetailsByName(string ContactGroupName)
        {
            try
            {
                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandType = System.Data.CommandType.Text;

                string SQL = SQLSelect() + "WHERE ContactGroupName = @ContactGroupName;";

                cmd.Parameters.AddWithValue("@ContactGroupName", ContactGroupName);

                cmd.CommandText = SQL;
                string strDataTableName = "tbl" + this.GetType().FullName.Split(new Char[] { '.' })[this.GetType().FullName.Split(new Char[] { '.' }).Length - 1]; System.Data.DataTable dt = new System.Data.DataTable(strDataTableName);
                base.MySqlDataAdapterFill(cmd, dt);

                ContactGroupDetails Details = setDetails(dt);
                
                return Details;
            }
            catch (Exception ex)
            {
                throw base.ThrowException(ex);
            }
        }

        private ContactGroupDetails setDetails(System.Data.DataTable dt)
        {
            ContactGroupDetails Details = new ContactGroupDetails();
            foreach (System.Data.DataRow dr in dt.Rows)
            {
                Details.ContactGroupID = Int32.Parse(dr["ContactGroupID"].ToString());
                Details.ContactGroupCode = "" + dr["ContactGroupCode"].ToString();
                Details.ContactGroupName = "" + dr["ContactGroupName"].ToString();
                Details.ContactGroupCategory = (ContactGroupCategory)Enum.Parse(typeof(ContactGroupCategory), dr["ContactGroupCategory"].ToString());
            }

            return Details;
        }


		#endregion

		#region Streams

		public MySqlDataReader List(string SortField, SortOption SortOrder)
		{
			try
			{
				string SQL = SQLSelect() + "ORDER BY " + SortField;

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
				
				
				{
					
					
					
					
				}

				throw base.ThrowException(ex);
			}	
		}
		public MySqlDataReader List(ContactGroupCategory Category, string SortField, SortOption SortOrder)
		{
			try
			{
				string SQL = SQLSelect() + "WHERE (ContactGroupCategory = @Category OR ContactGroupCategory = 3) " +
					                        "ORDER BY " + SortField;

				if (SortOrder == SortOption.Ascending)
					SQL += " ASC";
				else
					SQL += " DESC";

				

				MySqlCommand cmd = new MySqlCommand();
				
				
				cmd.CommandType = System.Data.CommandType.Text;
				cmd.CommandText = SQL;

                cmd.Parameters.AddWithValue("@Category", Category.ToString("d"));

				
				
				return base.ExecuteReader(cmd);			
			}
			catch (Exception ex)
			{
				
				
				{
					
					
					
					
				}

				throw base.ThrowException(ex);
			}	
		}
		public MySqlDataReader Search(string SearchKey, string SortField, SortOption SortOrder)
		{
			try
			{
				string SQL = SQLSelect() + "WHERE ContactGroupName LIKE @SearchKey ORDER BY " + SortField;

				if (SortOrder == SortOption.Ascending)
					SQL += " ASC";
				else
					SQL += " DESC";

				

				MySqlCommand cmd = new MySqlCommand();
				
				
				cmd.CommandType = System.Data.CommandType.Text;
				cmd.CommandText = SQL;
                cmd.Parameters.AddWithValue("@SearchKey", "%" + SearchKey + "%");

				
				
				return base.ExecuteReader(cmd);			
			}
			catch (Exception ex)
			{
				
				
				{
					
					
					
					
				}

				throw base.ThrowException(ex);
			}	
		}		

		#endregion

        #region DataTables

        public System.Data.DataTable ListAsDataTable(ContactGroupCategory Category = ContactGroupCategory.BOTH, string SearchKey = null, string SortField = "ContactGroupName", SortOption SortOrder = SortOption.Ascending, Int32 limit = 0)
        {
            MySqlCommand cmd = new MySqlCommand();
            cmd.CommandType = System.Data.CommandType.Text;

            string SQL = SQLSelect() + "WHERE 1=1 ";

            if (SearchKey != "")
            {
                SQL += "AND (ContactGroupCode LIKE @SearchKey  OR ContactGroupName LIKE @SearchKey) ";
                cmd.Parameters.AddWithValue("@SearchKey", "%" + SearchKey + "%");
            }
            if (Category != ContactGroupCategory.BOTH)
            {
                SQL += "AND (ContactGroupCategory = @Category OR ContactGroupCategory = 3) ";
                cmd.Parameters.AddWithValue("@Category", Category.ToString("d"));
            }

            SQL += "ORDER BY " + (!string.IsNullOrEmpty(SortField) ? SortField : "ContactGroupCode") + " ";
            SQL += SortOrder == SortOption.Ascending ? "ASC " : "DESC ";
            SQL += limit == 0 ? "" : "LIMIT " + limit.ToString() + " ";

            cmd.CommandText = SQL;
            string strDataTableName = "tbl" + this.GetType().FullName.Split(new Char[] { '.' })[this.GetType().FullName.Split(new Char[] { '.' }).Length - 1]; System.Data.DataTable dt = new System.Data.DataTable(strDataTableName);
            base.MySqlDataAdapterFill(cmd, dt);            

            return dt;
        }

        #endregion
	}
}


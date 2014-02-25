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
	public struct BranchDetails
	{
		public Int32 BranchID;
		public string BranchCode;
		public string BranchName;
		public string DBIP;
		public string DBPort;
		public string Address;
		public string Remarks;
	}

	[StrongNameIdentityPermissionAttribute(SecurityAction.LinkDemand,
		 PublicKey = "002400000480000094000000060200000024000" +
		 "052534131000400000100010053D785642F9F960B43157E0380" +
		 "F393BEE53E8DFAFBF441366C1B6F8B48D9DDF0D527B1F3B21EA" +
		 "E85D2FDB664CE81EB8A87DBE4C589D6F4202FE2B7D4B978BB69" +
		 "684874612CB9B8DB7A0339400A9C4E68277884B07817363D242" +
		 "E3696F9FACDBEA831810AE6DC9EDCA91A7B5DA12FE7BF65D113" +
		 "FF52834EAFB5A7A1FDFD5851A3")]
	public class Branch : POSConnection
    {
		#region Constructors and Destructors

		public Branch()
            : base(null, null)
        {
        }

        public Branch(MySqlConnection Connection, MySqlTransaction Transaction) 
            : base(Connection, Transaction)
		{

		}

		#endregion

		#region Insert and Update

		public Int32 Insert(BranchDetails Details)
		{
			try 
			{
				string SQL = "INSERT INTO tblBranch (BranchCode, BranchName, DBIP, DBPort, Address, Remarks) " +
					"VALUES (@BranchCode, @BranchName, @DBIP, @DBPort, @Address, @Remarks);";
				  
				MySqlCommand cmd = new MySqlCommand();
				cmd.CommandType = System.Data.CommandType.Text;
				cmd.CommandText = SQL;

                cmd.Parameters.AddWithValue("@BranchCode", Details.BranchCode);
                cmd.Parameters.AddWithValue("@BranchName", Details.BranchName);
                cmd.Parameters.AddWithValue("@DBIP", Details.DBIP);
                cmd.Parameters.AddWithValue("@DBPort", Details.DBPort);
                cmd.Parameters.AddWithValue("@Address", Details.Address);
                cmd.Parameters.AddWithValue("@Remarks", Details.Remarks);

				base.ExecuteNonQuery(cmd);

				SQL = "SELECT LAST_INSERT_ID();";
				
				cmd.Parameters.Clear(); 
				cmd.CommandText = SQL;

                System.Data.DataTable dt = new System.Data.DataTable("LAST_INSERT_ID");
                base.MySqlDataAdapterFill(cmd, dt);

                Int32 iID = 0;
                foreach (System.Data.DataRow dr in dt.Rows)
                {
                    iID = Int32.Parse(dr[0].ToString());
                }

				return iID;
			}

			catch (Exception ex)
			{
				throw base.ThrowException(ex);
			}	
		}

		public void Update(BranchDetails Details)
		{
			try 
			{
				string SQL=	"UPDATE tblBranch SET " + 
								"BranchCode = @BranchCode, " +  
								"BranchName = @BranchName, " +  
								"DBIP = @DBIP, " +  
								"DBPort = @DBPort, " +  
								"Address = @Address, " +  
								"Remarks = @Remarks " +  
							"WHERE BranchID = @BranchID;";
	 			
				MySqlCommand cmd = new MySqlCommand();
				cmd.CommandType = System.Data.CommandType.Text;
				cmd.CommandText = SQL;
				
                cmd.Parameters.AddWithValue("@BranchID", Details.BranchID);
                cmd.Parameters.AddWithValue("@BranchCode", Details.BranchCode);
                cmd.Parameters.AddWithValue("@BranchName", Details.BranchName);
                cmd.Parameters.AddWithValue("@DBIP", Details.DBIP);
                cmd.Parameters.AddWithValue("@DBPort", Details.DBPort);
                cmd.Parameters.AddWithValue("@Address", Details.Address);
                cmd.Parameters.AddWithValue("@Remarks", Details.Remarks);

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
				string SQL=	"DELETE FROM tblBranch WHERE BranchID IN (" + IDs + ");";
				  
				
	 			
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
					            "BranchID, " +
					            "BranchCode, " +
					            "BranchName, " + 
					            "DBIP, " +
					            "DBPort, " +
					            "Address, " +
					            "Remarks " +
					        "FROM tblBranch ";

            return stSQL;
        }

		#region Details

		public BranchDetails Details(Int32 BranchID)
		{
			try
			{
				string SQL = "SELECT " +
								"BranchID, " +
								"BranchCode, " +
								"BranchName, " + 
								"DBIP, " +
								"DBPort, " +
								"Address, " +
								"Remarks " +
							"FROM tblBranch " +
							"WHERE BranchID = @BranchID;";
				  
				MySqlCommand cmd = new MySqlCommand();
				cmd.CommandType = System.Data.CommandType.Text;
				cmd.CommandText = SQL;

                cmd.Parameters.AddWithValue("@BranchID", BranchID);

				MySqlDataReader myReader = base.ExecuteReader(cmd, System.Data.CommandBehavior.SingleResult);
				BranchDetails Details = new BranchDetails();

				while (myReader.Read()) 
				{
                    Details.BranchID = myReader.GetInt32("BranchID");
                    Details.BranchCode = "" + myReader["BranchCode"].ToString();
                    Details.BranchName = "" + myReader["BranchName"].ToString();
                    Details.DBIP = "" + myReader["DBIP"].ToString();
                    Details.DBPort = "" + myReader["DBPort"].ToString();
                    Details.Address = "" + myReader["Address"].ToString();
                    Details.Remarks = "" + myReader["Remarks"].ToString();
				}
				myReader.Close();

				return Details;
			}

			catch (Exception ex)
			{
				throw base.ThrowException(ex);
			}	
		}
        public BranchDetails Details(string BranchCode)
        {
            try
            {
                string SQL = "SELECT " +
                                "BranchID, " +
                                "BranchCode, " +
                                "BranchName, " +
                                "DBIP, " +
                                "DBPort, " +
                                "Address, " +
                                "Remarks " +
                            "FROM tblBranch " +
                            "WHERE BranchCode = @BranchCode;";

                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandText = SQL;

                cmd.Parameters.AddWithValue("@BranchCode", BranchCode);

                MySqlDataReader myReader = base.ExecuteReader(cmd, System.Data.CommandBehavior.SingleResult);

                BranchDetails Details = new BranchDetails();

                while (myReader.Read())
                {
                    Details.BranchID = myReader.GetInt32("BranchID");
                    Details.BranchCode = "" + myReader["BranchCode"].ToString();
                    Details.BranchName = "" + myReader["BranchName"].ToString();
                    Details.DBIP = "" + myReader["DBIP"].ToString();
                    Details.DBPort = "" + myReader["DBPort"].ToString();
                    Details.Address = "" + myReader["Address"].ToString();
                    Details.Remarks = "" + myReader["Remarks"].ToString();
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

        public System.Data.DataTable ListAsDataTable(string SortField = "BranchCode", SortOption SortOrder=SortOption.Ascending)
        {
            string SQL = SQLSelect() + "ORDER BY " + SortField;

            if (SortOrder == SortOption.Ascending)
                SQL += " ASC";
            else
                SQL += " DESC";

            MySqlCommand cmd = new MySqlCommand();
            cmd.CommandType = System.Data.CommandType.Text;
            cmd.CommandText = SQL;

            System.Data.DataTable dt = new System.Data.DataTable("tblBranch");
            base.MySqlDataAdapterFill(cmd, dt);

            return dt;
        }
        public System.Data.DataTable SearchAsDataTable(string SearchKey, string SortField, SortOption SortOrder)
        {
            string SQL = SQLSelect() + "WHERE (BranchCode LIKE @SearchKey or BranchName LIKE @SearchKey) ORDER BY " + SortField;

            if (SortOrder == SortOption.Ascending)
                SQL += " ASC";
            else
                SQL += " DESC";

            MySqlCommand cmd = new MySqlCommand();
            cmd.CommandType = System.Data.CommandType.Text;
            cmd.CommandText = SQL;

            cmd.Parameters.AddWithValue("@SearchKey", SearchKey);

            System.Data.DataTable dt = new System.Data.DataTable("tblBranch");
            base.MySqlDataAdapterFill(cmd, dt);

            return dt;
        }

		#endregion
	}
}


using System;
using System.Security.Permissions;
using MySql.Data.MySqlClient;
using System.Collections;

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
	public struct AuditTrailDetails
	{
		public DateTime ActivityDate;
		public string User;
		public string Activity;
		public string IPAddress;
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
	public class AuditTrail : POSConnection
	{
		
		#region Constructors and Destructors

		public AuditTrail()
            : base(null, null)
        {
        }

        public AuditTrail(MySqlConnection Connection, MySqlTransaction Transaction) 
            : base(Connection, Transaction)
		{

		}

		#endregion

		#region Insert and Update

		public void Insert(AuditTrailDetails Details)
		{
			try 
			{

				string SQL="INSERT INTO sysAuditTrail ( ActivityDate, User, Activity, IPAddress, Remarks ) " +
							"VALUES ( @ActivityDate, @User, @Activity, @IPAddress, @Remarks );";

				
	 			
				MySqlCommand cmd = new MySqlCommand();
				
				
				cmd.CommandType = System.Data.CommandType.Text;
				cmd.CommandText = SQL;

                cmd.Parameters.AddWithValue("@ActivityDate", Details.ActivityDate);
                cmd.Parameters.AddWithValue("@User", Details.User);
                cmd.Parameters.AddWithValue("@Activity", Details.Activity);
                cmd.Parameters.AddWithValue("@IPAddress", Details.IPAddress);
                cmd.Parameters.AddWithValue("@Remarks", Details.Remarks);

                //cmd.Parameters.AddWithValue("@ActivityDate", Details.ActivityDate.ToString("yyyy-MM-dd HH:mm:ss"));

                base.ExecuteNonQuery(cmd);
			}

			catch (Exception ex)
			{
				throw base.ThrowException(ex);
			}	
		}

		
		#endregion

        private string SQLSelect()
        {
            string stSQL = "SELECT " +
                                "ActivityDate, " +
                                "User, " +
                                "Activity, " +
                                "IPAddress, " +
                                "Remarks " +
                                "FROM sysAuditTrail ";

            return stSQL;
        }

		#region Streams
		
		public AuditTrailDetails[] DetailedList(string SortField, SortOption SortOrder)
		{
			try
			{
				MySqlDataReader myReader = List(SortField, SortOrder);
 				ArrayList arrDetails = new ArrayList();

				while (myReader.Read())
				{
					AuditTrailDetails Details = new AuditTrailDetails();
					
					// Map parameter fields
					// example: 
					//Details.IDENTITY			=	myReader.GetInt64("IDENTITY");
					

					arrDetails.Add(Details);
				}
				myReader.Close();
			
				AuditTrailDetails[] arrList = new AuditTrailDetails[0];
				if (arrDetails != null)
				{
					arrList = new AuditTrailDetails[arrDetails.Count];
					arrDetails.CopyTo(arrList);
				}

				return arrList;		
			}
			catch (Exception ex)
			{
				throw base.ThrowException(ex);
			}	
		}

		public MySqlDataReader List(string SortField, SortOption SortOrder)
		{
			try
			{
				string SQL = "SELECT ActivityDate, User, Activity, IPAddress, Remarks FROM sysAuditTrail ORDER BY " + SortField;

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
				string SQL =SQLSelect() + "WHERE User LIKE @SearchKey " +
							"OR Activity LIKE @SearchKey " +
							"OR IPAddress LIKE @SearchKey " +
							"OR Remarks LIKE @SearchKey " +
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

        public MySqlDataReader AdvanceSearch(DateTime ActivityDateFrom, DateTime ActivityDateTo, string User, AccessTypes Activity, string Remarks, int limit, string SortField, SortOption SortOrder)
		{
			try
			{
				string SQL = SQLSelect() + "WHERE 1=1 ";

				if (ActivityDateFrom != DateTime.MinValue)
					SQL += "AND ActivityDate >= @ActivityDateFrom ";
				if (ActivityDateTo != DateTime.MinValue)
                    SQL += "AND ActivityDate >= @ActivityDateTo ";
				if (User != null || User != string.Empty)
                    SQL += "AND User = @User ";
                if (Activity != AccessTypes.None)
                    SQL += "AND Activity = @Activity ";
                if (Remarks != string.Empty)
                    SQL += "AND Remarks LIKE @Remarks ";

				SQL += "ORDER BY " + SortField;

				if (SortOrder == SortOption.Ascending)
					SQL += " ASC ";
				else
					SQL += " DESC ";

                if (limit != 0) SQL += " LIMIT " + limit.ToString() + " ";

				MySqlCommand cmd = new MySqlCommand();
				cmd.CommandType = System.Data.CommandType.Text;
				cmd.CommandText = SQL;

                cmd.Parameters.AddWithValue("@ActivityDateFrom", ActivityDateFrom.ToString("yyyy-MM-dd HH:mm"));
                cmd.Parameters.AddWithValue("@ActivityDateTo", ActivityDateTo.ToString("yyyy-MM-dd HH:mm"));
                cmd.Parameters.AddWithValue("@User", User);
                cmd.Parameters.AddWithValue("@Activity", Activity);
                cmd.Parameters.AddWithValue("@Remarks", Remarks);

				return base.ExecuteReader(cmd);			
			}
			catch (Exception ex)
			{
				throw base.ThrowException(ex);
			}	
		}

        public AuditTrailDetails[] DetailedList(DateTime ActivityDateFrom, DateTime ActivityDateTo, string User, AccessTypes Activity,  string Remarks, int limit, string SortField, SortOption SortOrder)
        {
            try
            {
                MySqlDataReader myReader = AdvanceSearch(ActivityDateFrom, ActivityDateTo, User, Activity, Remarks, limit, SortField, SortOrder);
                ArrayList arrDetails = new ArrayList();

                while (myReader.Read())
                {
                    AuditTrailDetails Details = new AuditTrailDetails();

                    Details.ActivityDate = myReader.GetDateTime("ActivityDate");
                    Details.User = "" + myReader["User"].ToString();
                    Details.Activity = "" + myReader["Activity"].ToString();
                    Details.IPAddress = "" + myReader["IPAddress"].ToString();
                    Details.Remarks = "" + myReader["Remarks"].ToString();

                    arrDetails.Add(Details);
                }
                myReader.Close();

                AuditTrailDetails[] arrList = new AuditTrailDetails[0];
                if (arrDetails != null)
                {
                    arrList = new AuditTrailDetails[arrDetails.Count];
                    arrDetails.CopyTo(arrList);
                }

                return arrList;
            }
            catch (Exception ex)
            {
                throw base.ThrowException(ex);
            }	
        }			

		#endregion
	}
}

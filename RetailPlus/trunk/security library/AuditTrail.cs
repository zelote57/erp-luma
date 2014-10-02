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

        public Int32 BranchID;
        public string TerminalNo;

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

				string SQL="INSERT INTO sysAuditTrail (BranchID, TerminalNo, ActivityDate, User, Activity, IPAddress, Remarks ) " +
                            "VALUES (@BranchID, @TerminalNo, @ActivityDate, @User, @Activity, @IPAddress, @Remarks );";

				MySqlCommand cmd = new MySqlCommand();
				cmd.CommandType = System.Data.CommandType.Text;
				cmd.CommandText = SQL;

                cmd.Parameters.AddWithValue("@BranchID", Details.BranchID);
                cmd.Parameters.AddWithValue("@TerminalNo", Details.TerminalNo);
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
                                "BranchID, " +
                                "TerminalNo, " +
                                "ActivityDate, " +
                                "User, " +
                                "Activity, " +
                                "IPAddress, " +
                                "Remarks, " +
                                "CreatedOn, " +
                                "LastModified " +
                                "FROM sysAuditTrail ";

            return stSQL;
        }

		#region Streams

        public AuditTrailDetails[] DetailedList(string SortField, SortOption SortOrder)
        {
            try
            {
                System.Data.DataTable dt = ListAsDataTable(SortField: SortField, SortOrder: SortOrder);
                ArrayList arrDetails = new ArrayList();
                AuditTrailDetails clsDetails = new AuditTrailDetails();

                foreach (System.Data.DataRow dr in dt.Rows)
                {
                    clsDetails = new AuditTrailDetails();
                    clsDetails.BranchID = Int32.Parse(dr["BranchID"].ToString());
                    clsDetails.TerminalNo = dr["TerminalNo"].ToString();
                    clsDetails.ActivityDate = DateTime.Parse(dr["ActivityDate"].ToString());
                    clsDetails.User = dr["User"].ToString();
                    clsDetails.Activity = dr["Activity"].ToString();
                    clsDetails.IPAddress = dr["IPAddress"].ToString();
                    clsDetails.Remarks = dr["Remarks"].ToString();
                    clsDetails.CreatedOn = DateTime.Parse(dr["CreatedOn"].ToString());
                    clsDetails.LastModified = DateTime.Parse(dr["LastModified"].ToString());
                    arrDetails.Add(clsDetails);
                }

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

        public System.Data.DataTable ListAsDataTable(Int32 BranchID = 0, string TerminalNo = "", string SearchKey = "", int limit = 0, string SortField = "ActivityDate", SortOption SortOrder = SortOption.Ascending)
        {
            MySqlCommand cmd = new MySqlCommand();
            cmd.CommandType = System.Data.CommandType.Text;

            string SQL = SQLSelect() + "WHERE 1=1 ";

            if (BranchID != 0)
            {
                SQL += "AND BranchID = @BranchID ";
                cmd.Parameters.AddWithValue("@BranchID", BranchID);
            }
            if (!string.IsNullOrEmpty(TerminalNo))
            {
                SQL += "AND TerminalNo = @TerminalNo ";
                cmd.Parameters.AddWithValue("@TerminalNo", TerminalNo);
            }
            if (!string.IsNullOrEmpty(SearchKey))
            {
                SQL += "AND TerminalNo = @SearchKey ";
                cmd.Parameters.AddWithValue("@SearchKey", SearchKey);
            }

            SQL += "ORDER BY " + SortField + " ";
            SQL += SortOrder == SortOption.Ascending ? "ASC " : "DESC ";
            SQL += limit == 0 ? "" : "LIMIT " + limit.ToString() + " ";

            cmd.CommandText = SQL;
            string strDataTableName = "tbl" + this.GetType().FullName.Split(new Char[] { '.' })[this.GetType().FullName.Split(new Char[] { '.' }).Length - 1]; System.Data.DataTable dt = new System.Data.DataTable(strDataTableName);
            base.MySqlDataAdapterFill(cmd, dt);

            return dt;
        }

        public System.Data.DataTable AdvanceSearch(DateTime ActivityDateFrom, DateTime ActivityDateTo, string User, AccessTypes Activity, string Remarks, int limit = 0, string SortField = "ActivityDate", SortOption SortOrder = SortOption.Ascending)
		{
			try
			{
                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandType = System.Data.CommandType.Text;

				string SQL = SQLSelect() + "WHERE 1=1 ";

                if (ActivityDateFrom != DateTime.MinValue)
                {
                    SQL += "AND ActivityDate >= @ActivityDateFrom ";
                    cmd.Parameters.AddWithValue("@ActivityDateFrom", ActivityDateFrom.ToString("yyyy-MM-dd HH:mm"));
                }
                if (ActivityDateTo != DateTime.MinValue)
                {
                    SQL += "AND ActivityDate >= @ActivityDateTo ";
                    cmd.Parameters.AddWithValue("@ActivityDateTo", ActivityDateTo.ToString("yyyy-MM-dd HH:mm"));
                }
                if (!string.IsNullOrEmpty(User))
                {
                    SQL += "AND User = @User ";
                    cmd.Parameters.AddWithValue("@User", User);
                    
                }
                if (Activity != AccessTypes.None)
                {
                    SQL += "AND Activity = @Activity ";
                    cmd.Parameters.AddWithValue("@Activity", Activity);
                    
                }
                if (!string.IsNullOrEmpty(Remarks))
                {
                    SQL += "AND Remarks LIKE @Remarks ";
                    cmd.Parameters.AddWithValue("@Remarks", Remarks);
                }

                SQL += "ORDER BY " + SortField + " ";
                SQL += SortOrder == SortOption.Ascending ? "ASC " : "DESC ";
                SQL += limit == 0 ? "" : " LIMIT " + limit.ToString() + " ";

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

        public AuditTrailDetails[] DetailedList(DateTime ActivityDateFrom, DateTime ActivityDateTo, string User, AccessTypes Activity, string Remarks, int limit = 0, string SortField = "ActivityDate", SortOption SortOrder = SortOption.Ascending)
        {
            try
            {
                System.Data.DataTable dt = AdvanceSearch(ActivityDateFrom, ActivityDateTo, User, Activity, Remarks, limit, SortField, SortOrder);
                ArrayList arrDetails = new ArrayList();
                AuditTrailDetails clsDetails = new AuditTrailDetails();

                foreach(System.Data.DataRow dr in dt.Rows)
                {
                    clsDetails = new AuditTrailDetails();
                    clsDetails.ActivityDate = DateTime.Parse(dr["ActivityDate"].ToString());
                    clsDetails.User = dr["User"].ToString();
                    clsDetails.Activity = dr["Activity"].ToString();
                    clsDetails.IPAddress = dr["IPAddress"].ToString();
                    clsDetails.Remarks = dr["Remarks"].ToString();
                    clsDetails.CreatedOn = DateTime.Parse(dr["CreatedOn"].ToString());
                    clsDetails.LastModified = DateTime.Parse(dr["LastModified"].ToString());
                    arrDetails.Add(clsDetails);
                }

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

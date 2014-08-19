using System;
using System.Security.Permissions;
using MySql.Data.MySqlClient;

namespace AceSoft.RetailPlus.Data
{

	public struct ParkingRateDetails
	{
        public Int64 ParkingRateID;
        public Int64 ProductID;
		public string DayOfWeek;
        public string StartTime;
        public string EndTime;
        public Int32 NoOfUnitperMin;
        public decimal PerUnitPrice;
        public Int32 MinimumStayInMin;
        public decimal MinimumStayPrice;
        public string CreatedByName;
        public string LastUpdatedByName;

        public DateTime CreatedOn;
        public DateTime LastModified;
	}

	public class ParkingRates : POSConnection
    {
        public const string DEFAULT_ALL_ParkingRates = "All ParkingRates";

		#region Constructors and Destructors

		public ParkingRates()
            : base(null, null)
        {
        }

        public ParkingRates(MySqlConnection Connection, MySqlTransaction Transaction) 
            : base(Connection, Transaction)
		{

		}

		#endregion

		#region Insert and Update

		public Int16 Insert(ParkingRateDetails Details)
		{
			try 
			{
                Details.CreatedByName = Details.ParkingRateID == 0 ? Details.CreatedByName : Details.LastUpdatedByName;

                Save(Details);

                string SQL = "SELECT LAST_INSERT_ID();";

                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandText = SQL;

                System.Data.DataTable dt = new System.Data.DataTable("LAST_INSERT_ID");
                base.MySqlDataAdapterFill(cmd, dt);
                

                Int16 iID = 0;
                foreach (System.Data.DataRow dr in dt.Rows)
                {
                    iID = Int16.Parse(dr[0].ToString());
                }

				return iID;
			}

			catch (Exception ex)
			{
				throw base.ThrowException(ex);
			}	
		}

        public Int32 Save(ParkingRateDetails Details)
        {
            try
            {
                string SQL = "CALL procSaveParkingRates(@ParkingRateID, @ProductID, @DayOfWeek, @StartTime, @Endtime, @NoOfUnitPerMin, @PerUnitPrice, @MinimumStayInMin, " +
                                        "@MinimumStayPrice, @CreatedByName, @LastUpdatedByName, @CreatedOn, @LastModified);";

                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandText = SQL;

                cmd.Parameters.AddWithValue("ParkingRateID", Details.ParkingRateID);
                cmd.Parameters.AddWithValue("ProductID", Details.ProductID);
                cmd.Parameters.AddWithValue("DayOfWeek", Details.DayOfWeek);
                cmd.Parameters.AddWithValue("StartTime", Details.StartTime);
                cmd.Parameters.AddWithValue("EndTime", Details.EndTime);
                cmd.Parameters.AddWithValue("NoOfUnitperMin", Details.NoOfUnitperMin);
                cmd.Parameters.AddWithValue("PerUnitPrice", Details.PerUnitPrice);
                cmd.Parameters.AddWithValue("MinimumStayInMin", Details.MinimumStayInMin);
                cmd.Parameters.AddWithValue("MinimumStayPrice", Details.MinimumStayPrice);
                cmd.Parameters.AddWithValue("CreatedByName", Details.CreatedByName);
                cmd.Parameters.AddWithValue("LastUpdatedByName", Details.LastUpdatedByName);
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
				string SQL=	"DELETE FROM tblParkingRates WHERE ParkingRateID IN (" + IDs + ");";
	 			
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

		public ParkingRateDetails Details(Int64 ParkingRateID)
		{
			try
			{
                ParkingRateDetails Details = setDetails(ListAsDataTable(ParkingRateID, 0, null));

                return Details;
			}

			catch (Exception ex)
			{
				throw base.ThrowException(ex);
			}	
		}

        public ParkingRateDetails Details(Int64 ProductID, string DayOfWeek)
        {
            try
            {
                ParkingRateDetails Details = setDetails(ListAsDataTable(0, ProductID, DayOfWeek));

                return Details;
            }

            catch (Exception ex)
            {
                throw base.ThrowException(ex);
            }
        }

        private ParkingRateDetails setDetails(System.Data.DataTable dt)
        {
            ParkingRateDetails Details = new ParkingRateDetails();

            foreach (System.Data.DataRow dr in dt.Rows)
            {
                Details.ParkingRateID = Int64.Parse(dr["ParkingRateID"].ToString());
                Details.ProductID = Int64.Parse(dr["ProductID"].ToString());
                Details.DayOfWeek = "" + dr["DayOfWeek"].ToString();
                Details.StartTime = "" + dr["StartTime"].ToString();
                Details.EndTime = "" + dr["EndTime"].ToString();
                Details.NoOfUnitperMin = Int32.Parse(dr["NoOfUnitperMin"].ToString());
                Details.PerUnitPrice = decimal.Parse(dr["PerUnitPrice"].ToString());
                Details.MinimumStayInMin = Int32.Parse(dr["MinimumStayInMin"].ToString());
                Details.MinimumStayPrice = decimal.Parse(dr["MinimumStayPrice"].ToString());
                Details.CreatedByName = "" + dr["CreatedByName"].ToString();
                Details.LastUpdatedByName = "" + dr["LastUpdatedByName"].ToString();
                Details.CreatedOn = DateTime.Parse(dr["CreatedOn"].ToString());
                Details.LastModified = DateTime.Parse(dr["LastModified"].ToString());
            }

            return Details;
        }
		#endregion

		#region Streams

        public System.Data.DataTable ListAsDataTable(Int64 ParkingRateID, long ProductID, string DayOfWeek, string StartTime = "", string EndTime = "", string SortField = "DayOfWeek", SortOption SortOrder = SortOption.Ascending)
        {
            try
            {
                string SQL = "CALL procParkingRateSelect(@ParkingRateID, @ProductID, @DayOfWeek, @StartTime, @EndTime, @SortField, @SortOrder)";

                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandText = SQL;

                cmd.Parameters.AddWithValue("@ParkingRateID", ParkingRateID);
                cmd.Parameters.AddWithValue("@ProductID", ProductID);
                cmd.Parameters.AddWithValue("@DayOfWeek", DayOfWeek);
                cmd.Parameters.AddWithValue("@StartTime", StartTime);
                cmd.Parameters.AddWithValue("@EndTime", EndTime);
                cmd.Parameters.AddWithValue("@SortField", SortField);
                cmd.Parameters.AddWithValue("@SortOrder", SortOrder == SortOption.Ascending ? "ASC" : "DESC");

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


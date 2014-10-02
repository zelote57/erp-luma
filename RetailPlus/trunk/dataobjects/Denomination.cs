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
                Save(Details);

                return Int32.Parse(base.getLAST_INSERT_ID(this));
			}
			catch (Exception ex)
			{
				throw base.ThrowException(ex);
			}	
		}

		public void Update(DenominationDetails Details)
		{
			try 
			{
                Save(Details);
			}
			catch (Exception ex)
			{
				throw base.ThrowException(ex);
			}	
		}

        public Int32 Save(DenominationDetails Details)
        {
            try
            {
                string SQL = "CALL procSaveDenomination(@DenominationID, @DenominationCode, @DenominationValue, @ImagePath, @CreatedOn, @LastModified);";

                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandText = SQL;

                cmd.Parameters.AddWithValue("DenominationID", Details.DenominationID);
                cmd.Parameters.AddWithValue("DenominationCode", Details.DenominationCode);
                cmd.Parameters.AddWithValue("DenominationValue", Details.DenominationValue);
                cmd.Parameters.AddWithValue("ImagePath", Details.ImagePath);
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
				string SQL=	"DELETE FROM tblDenomination WHERE DenominationID IN (" + IDs + ");";
				  
				
	 			
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

        private string SQLSelect()
        {
            string stSQL = "SELECT DenominationID, DenominationCode, DenominationValue, ImagePath, 0 AS DenominationCount, 0.00 AS DenominationAmount, CreatedOn, LastModified " +
                            "FROM tblDenomination ";

            return stSQL;
        }

		#region Details

		public DenominationDetails Details(Int32 DenominationID)
		{
			try
			{
                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandType = System.Data.CommandType.Text;

                string SQL = SQLSelect() + "WHERE DenominationID = @DenominationID ";

                cmd.Parameters.AddWithValue("@DenominationID", DenominationID);

                cmd.CommandText = SQL;
                string strDataTableName = "tbl" + this.GetType().FullName.Split(new Char[] { '.' })[this.GetType().FullName.Split(new Char[] { '.' }).Length - 1]; System.Data.DataTable dt = new System.Data.DataTable(strDataTableName);
                base.MySqlDataAdapterFill(cmd, dt);

				DenominationDetails Details = new DenominationDetails();
                foreach (System.Data.DataRow dr in dt.Rows)
				{
					Details.DenominationID = Int32.Parse(dr["DenominationID"].ToString());
					Details.DenominationCode = dr["DenominationCode"].ToString();
                    Details.DenominationValue = decimal.Parse(dr["DenominationCode"].ToString());
					Details.ImagePath = dr["ImagePath"].ToString();
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

        public System.Data.DataTable ListAsDataTable(DenominationDetails clsSearchKey, string SortField = "DenominationValue", SortOption SortOrder = SortOption.Desscending, Int32 limit = 0)
        {
            MySqlCommand cmd = new MySqlCommand();
            cmd.CommandType = System.Data.CommandType.Text;

            string SQL = SQLSelect() + "WHERE 1= 1 ";

            if (clsSearchKey.DenominationID !=0 )
            {
                SQL += "AND DenominationID = @DenominationID ";
                cmd.Parameters.AddWithValue("@DenominationID", clsSearchKey.DenominationID);
            }
            if (!string.IsNullOrEmpty(clsSearchKey.DenominationCode))
            {
                SQL += "AND DenominationCode = @DenominationCode ";
                cmd.Parameters.AddWithValue("@DenominationCode", clsSearchKey.DenominationCode);
            }

            SQL += "ORDER BY " + SortField + " ";
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


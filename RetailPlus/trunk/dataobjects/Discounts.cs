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
	public struct DiscountDetails
	{
		public Int32 DiscountID;
		public string DiscountCode;
		public string DiscountType;
		public decimal DiscountPrice;
		public bool InPercent;

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
	public class Discount : POSConnection
    {
		#region Constructors and Destructors

		public Discount()
            : base(null, null)
        {
        }

        public Discount(MySqlConnection Connection, MySqlTransaction Transaction) 
            : base(Connection, Transaction)
		{

		}

		#endregion

		#region Insert and Update

		public Int32 Insert(DiscountDetails Details)
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

		public void Update(DiscountDetails Details)
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

        public Int32 Save(DiscountDetails Details)
        {
            try
            {
                string SQL = "CALL procSaveDiscount(@DiscountID, @DiscountCode, @DiscountType, @DiscountPrice, @InPercent, @CreatedOn, @LastModified);";

                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandText = SQL;

                cmd.Parameters.AddWithValue("DiscountID", Details.DiscountID);
                cmd.Parameters.AddWithValue("DiscountCode", Details.DiscountCode);
                cmd.Parameters.AddWithValue("DiscountType", Details.DiscountType);
                cmd.Parameters.AddWithValue("DiscountPrice", Details.DiscountPrice);
                cmd.Parameters.AddWithValue("InPercent", Details.InPercent);
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
				string SQL=	"DELETE FROM tblDiscount WHERE DiscountID IN (" + IDs + ");";
				  
				
	 			
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
                                "DiscountID, " +
                                "DiscountCode, " +
                                "DiscountType, " +
                                "DiscountPrice, " +
                                "InPercent " +
                            "FROM tblDiscount ";

            return stSQL;
        }

		#region Details

		public DiscountDetails Details(Int32 DiscountID)
		{
			try
			{
                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandType = System.Data.CommandType.Text;
				
				string SQL=SQLSelect() + "WHERE DiscountID = @DiscountID;";

                cmd.Parameters.AddWithValue("@DiscountID", DiscountID);

                cmd.CommandText = SQL;
                string strDataTableName = "tbl" + this.GetType().FullName.Split(new Char[] { '.' })[this.GetType().FullName.Split(new Char[] { '.' }).Length - 1]; System.Data.DataTable dt = new System.Data.DataTable(strDataTableName);
                base.MySqlDataAdapterFill(cmd, dt);

                DiscountDetails Details = new DiscountDetails();
                foreach (System.Data.DataRow dr in dt.Rows)
                {
                    Details.DiscountID = Int32.Parse(dr["DiscountID"].ToString());
                    Details.DiscountCode = "" + dr["DiscountCode"].ToString();
                    Details.DiscountType = "" + dr["DiscountType"].ToString();
                    Details.DiscountPrice = Decimal.Parse(dr["DiscountPrice"].ToString());
                    Details.InPercent = bool.Parse(dr["InPercent"].ToString());
                }

				return Details;
			}
			catch (Exception ex)
			{
				throw base.ThrowException(ex);
			}	
		}

		public DiscountDetails Details(string DiscountCode)
		{
			try
            {
                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandType = System.Data.CommandType.Text;

				string SQL=	SQLSelect() + "WHERE DiscountCode = @DiscountCode;";
				  
                cmd.Parameters.AddWithValue("@DiscountCode", DiscountCode);

                cmd.CommandText = SQL;
                string strDataTableName = "tbl" + this.GetType().FullName.Split(new Char[] { '.' })[this.GetType().FullName.Split(new Char[] { '.' }).Length - 1]; System.Data.DataTable dt = new System.Data.DataTable(strDataTableName);
                base.MySqlDataAdapterFill(cmd, dt);

                DiscountDetails Details = new DiscountDetails();
                foreach (System.Data.DataRow dr in dt.Rows)
                {
                    Details.DiscountID = Int32.Parse(dr["DiscountID"].ToString());
                    Details.DiscountCode = "" + dr["DiscountCode"].ToString();
                    Details.DiscountType = "" + dr["DiscountType"].ToString();
                    Details.DiscountPrice = Decimal.Parse(dr["DiscountPrice"].ToString());
                    Details.InPercent = bool.Parse(dr["InPercent"].ToString());
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

        public System.Data.DataTable ListAsDataTable(string SearchKey = null, string SortField = "DiscountCode", SortOption SortOrder = SortOption.Ascending, Int32 limit = 0)
		{
            try
            {
                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandType = System.Data.CommandType.Text;

                string SQL = SQLSelect() + " ";

                if (!string.IsNullOrEmpty(SearchKey))
                {
                    SQL += "WHERE DiscountType LIKE @SearchKey ";
                    cmd.Parameters.AddWithValue("@SearchKey", "%" + SearchKey + "%");
                }

                SQL += "ORDER BY " + (!string.IsNullOrEmpty(SortField) ? SortField : "DiscountCode") + " ";
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
		
        //public MySqlDataReader List(string SortField, SortOption SortOrder)
        //{
        //    try
        //    {
        //        string SQL =SQLSelect() + "ORDER BY " + SortField;

        //        if (SortOrder == SortOption.Ascending)
        //            SQL += " ASC";
        //        else
        //            SQL += " DESC";

				

        //        MySqlCommand cmd = new MySqlCommand();
				
				
        //        cmd.CommandType = System.Data.CommandType.Text;
        //        cmd.CommandText = SQL;
				
				
				
        //        return base.ExecuteReader(cmd);			
        //    }
        //    catch (Exception ex)
        //    {
				
				
        //        {
					
					
					
					
        //        }

        //        throw base.ThrowException(ex);
        //    }	
        //}
		
        //public MySqlDataReader Search(string SearchKey, string SortField, SortOption SortOrder)
        //{
        //    try
        //    {
        //        string SQL = SQLSelect() + "WHERE DiscountType LIKE @SearchKey ORDER BY " + SortField;

        //        if (SortOrder == SortOption.Ascending)
        //            SQL += " ASC";
        //        else
        //            SQL += " DESC";

				

        //        MySqlCommand cmd = new MySqlCommand();
				
				
        //        cmd.CommandType = System.Data.CommandType.Text;
        //        cmd.CommandText = SQL;
				
        //        MySqlParameter prmSearchKey = new MySqlParameter("@SearchKey",MySqlDbType.String);
        //        prmSearchKey.Value = "%" + SearchKey + "%";
        //        cmd.Parameters.Add(prmSearchKey);

				
				
        //        return base.ExecuteReader(cmd);			
        //    }
        //    catch (Exception ex)
        //    {
				
				
        //        {
					
					
					
					
        //        }

        //        throw base.ThrowException(ex);
        //    }	
        //}		

		
		#endregion
	}
}


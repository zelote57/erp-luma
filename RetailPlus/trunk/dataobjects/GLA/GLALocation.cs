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
	public struct GLALocationDetails
	{
        public long Rvc_Number;
        public string Rvc_Name;
        public string Sales_Itemizer1_Name;
        public string Sales_Itemizer2_Name;
        public string Sales_Itemizer3_Name;
        public string Sales_Itemizer4_Name;
        public string Sales_Itemizer5_Name;
        public string Sales_Itemizer6_Name;
        public string Sales_Itemizer7_Name;
        public string Sales_Itemizer8_Name;
        public string Sales_Itemizer9_Name;
        public string Sales_Itemizer10_Name;
        public string Sales_Itemizer11_Name;
        public string Sales_Itemizer12_Name;
        public string Sales_Itemizer13_Name;
        public string Sales_Itemizer14_Name;
        public string Sales_Itemizer15_Name;
        public string Sales_Itemizer16_Name;
        public DateTime DateCreated;
        public string CreatedBy;
        public string Filename;
        public string BatchID;
	}

	[StrongNameIdentityPermissionAttribute(SecurityAction.LinkDemand,
		 PublicKey = "002400000480000094000000060200000024000" +
		 "052534131000400000100010053D785642F9F960B43157E0380" +
		 "F393BEE53E8DFAFBF441366C1B6F8B48D9DDF0D527B1F3B21EA" +
		 "E85D2FDB664CE81EB8A87DBE4C589D6F4202FE2B7D4B978BB69" +
		 "684874612CB9B8DB7A0339400A9C4E68277884B07817363D242" +
		 "E3696F9FACDBEA831810AE6DC9EDCA91A7B5DA12FE7BF65D113" +
		 "FF52834EAFB5A7A1FDFD5851A3")]
	public class GLALocation : POSConnection
	{
		
		#region Constructors and Destructors

		public GLALocation()
            : base(null, null)
        {
        }

        public GLALocation(MySqlConnection Connection, MySqlTransaction Discount) 
            : base(Connection, Discount)
		{

		}

		#endregion

		#region Insert and Update

        public Int64 Insert(GLALocationDetails Details)
		{
			try  
			{
                string SQL = "INSERT INTO tblgla_d_location_def (" +
                                    "Rvc_Number," +
                                    "Rvc_Name," +
                                    "Sales_Itemizer1_Name," +
                                    "Sales_Itemizer2_Name," +
                                    "Sales_Itemizer3_Name," +
                                    "Sales_Itemizer4_Name," +
                                    "Sales_Itemizer5_Name," +
                                    "Sales_Itemizer6_Name," +
                                    "Sales_Itemizer7_Name," +
                                    "Sales_Itemizer8_Name," +
                                    "Sales_Itemizer9_Name," +
                                    "Sales_Itemizer10_Name," +
                                    "Sales_Itemizer11_Name," +
                                    "Sales_Itemizer12_Name," +
                                    "Sales_Itemizer13_Name," +
                                    "Sales_Itemizer14_Name," +
                                    "Sales_Itemizer15_Name," +
                                    "Sales_Itemizer16_Name," +
                                    "DateCreated," +
                                    "CreatedBy," +
                                    "Filename," +
                                    "BatchID" +
								") VALUES (" +
                                    "@Rvc_Number," +
                                    "@Rvc_Name," +
                                    "@Sales_Itemizer1_Name," +
                                    "@Sales_Itemizer2_Name," +
                                    "@Sales_Itemizer3_Name," +
                                    "@Sales_Itemizer4_Name," +
                                    "@Sales_Itemizer5_Name," +
                                    "@Sales_Itemizer6_Name," +
                                    "@Sales_Itemizer7_Name," +
                                    "@Sales_Itemizer8_Name," +
                                    "@Sales_Itemizer9_Name," +
                                    "@Sales_Itemizer10_Name," +
                                    "@Sales_Itemizer11_Name," +
                                    "@Sales_Itemizer12_Name," +
                                    "@Sales_Itemizer13_Name," +
                                    "@Sales_Itemizer14_Name," +
                                    "@Sales_Itemizer15_Name," +
                                    "@Sales_Itemizer16_Name," +
                                    "@DateCreated," +
                                    "@CreatedBy," +
                                    "@Filename," +
                                    "@BatchID);";
	 			
				MySqlCommand cmd = new MySqlCommand();
				cmd.CommandType = System.Data.CommandType.Text;
				cmd.CommandText = SQL;

                cmd.Parameters.AddWithValue("@Rvc_Number", Details.Rvc_Number);
                cmd.Parameters.AddWithValue("@Rvc_Name", Details.Rvc_Name);
                cmd.Parameters.AddWithValue("@Sales_Itemizer1_Name", Details.Sales_Itemizer1_Name);
                cmd.Parameters.AddWithValue("@Sales_Itemizer2_Name", Details.Sales_Itemizer2_Name);
                cmd.Parameters.AddWithValue("@Sales_Itemizer3_Name", Details.Sales_Itemizer3_Name);
                cmd.Parameters.AddWithValue("@Sales_Itemizer4_Name", Details.Sales_Itemizer4_Name);
                cmd.Parameters.AddWithValue("@Sales_Itemizer5_Name", Details.Sales_Itemizer5_Name);
                cmd.Parameters.AddWithValue("@Sales_Itemizer6_Name", Details.Sales_Itemizer6_Name);
                cmd.Parameters.AddWithValue("@Sales_Itemizer7_Name", Details.Sales_Itemizer7_Name);
                cmd.Parameters.AddWithValue("@Sales_Itemizer8_Name", Details.Sales_Itemizer8_Name);
                cmd.Parameters.AddWithValue("@Sales_Itemizer9_Name", Details.Sales_Itemizer9_Name);
                cmd.Parameters.AddWithValue("@Sales_Itemizer10_Name", Details.Sales_Itemizer10_Name);
                cmd.Parameters.AddWithValue("@Sales_Itemizer11_Name", Details.Sales_Itemizer11_Name);
                cmd.Parameters.AddWithValue("@Sales_Itemizer12_Name", Details.Sales_Itemizer12_Name);
                cmd.Parameters.AddWithValue("@Sales_Itemizer13_Name", Details.Sales_Itemizer13_Name);
                cmd.Parameters.AddWithValue("@Sales_Itemizer14_Name", Details.Sales_Itemizer14_Name);
                cmd.Parameters.AddWithValue("@Sales_Itemizer15_Name", Details.Sales_Itemizer15_Name);
                cmd.Parameters.AddWithValue("@Sales_Itemizer16_Name", Details.Sales_Itemizer16_Name);
                cmd.Parameters.AddWithValue("@DateCreated", Details.DateCreated);
                cmd.Parameters.AddWithValue("@CreatedBy", Details.CreatedBy);
                cmd.Parameters.AddWithValue("@Filename", Details.Filename);
                cmd.Parameters.AddWithValue("@BatchID", Details.BatchID);

				base.ExecuteNonQuery(cmd);

				SQL = "SELECT LAST_INSERT_ID();";
				
				cmd.Parameters.Clear(); 
				cmd.CommandText = SQL;

                System.Data.DataTable dt = new System.Data.DataTable("LAST_INSERT_ID");
                base.MySqlDataAdapterFill(cmd, dt);
                
                Int64 iID = 0;
                foreach (System.Data.DataRow dr in dt.Rows)
                {
                    iID = Int64.Parse(dr[0].ToString());
                }

				return iID;
			}

			catch (Exception ex)
			{
				throw base.ThrowException(ex);
			}	
		}

		#endregion

		#region Delete

		public bool Delete(string BatchID)
		{
			try 
			{

                string SQL = "DELETE FROM tblgla_d_location_def WHERE BatchID  = '" + BatchID + "';";

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


		#endregion

		#region Streams


		#endregion

    }
}


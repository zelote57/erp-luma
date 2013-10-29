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
	public struct TransactionGLADetails
	{
        public DateTime fk_business_date;
        public int fk_location_def;
        public int fk_emp_def;
        public string status_flag;
        public long chk_headers_seq_number;
        public int chk_num;
        public string chk_id;
        public int ot_number;
		public DateTime StockDate;
        public string Ot_Name;
        public int Tbl_Number;
        public DateTime Chk_Open_Date_Time;
        public DateTime Chk_Closed_Date_Time;
        public int Uws_Number;
        public bool Is_HotelMark_Promo;
        public decimal Sub_Ttl;
        public decimal Tax_Ttl;
        public decimal Auto_Svc_Ttl;
        public decimal Other_Svc_Ttl;
        public decimal Dsc_Ttl;
        public decimal Pymnt_Ttl;
        public int Chk_Prntd_Cnt;
        public int Cov_Cnt;
        public int Num_Dtl;
        public decimal Itemizer1;
        public decimal Itemizer2;
        public decimal Itemizer3;
        public decimal Itemizer4;
        public decimal Itemizer5;
        public decimal Itemizer6;
        public decimal Itemizer7;
        public decimal Itemizer8;
        public decimal Itemizer9;
        public decimal Itemizer10;
        public decimal Itemizer11;
        public decimal Itemizer12;
        public decimal Itemizer13;
        public decimal Itemizer14;
        public decimal Itemizer15;
        public decimal Itemizer16;
        public decimal Tip_ttl;
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
	public class TransactionGLA : POSConnection
	{
		
		#region Constructors and Destructors

		public TransactionGLA()
            : base(null, null)
        {
        }

        public TransactionGLA(MySqlConnection Connection, MySqlTransaction Transaction) 
            : base(Connection, Transaction)
		{

		}

		#endregion

		#region Insert and Update

        public Int64 Insert(TransactionGLADetails Details)
		{
			try  
			{
                string SQL = "INSERT INTO tblgla_f_dtl_chk_headers (" +
                                    "fk_business_date," +
                                    "fk_location_def," +
                                    "fk_emp_def," +
                                    "status_flag," +
                                    "chk_headers_seq_number," +
                                    "chk_num," +
                                    "chk_id," +
                                    "ot_number," +
                                    "Ot_Name," +
                                    "Tbl_Number," +
                                    "Chk_Open_Date_Time," +
                                    "Chk_Closed_Date_Time," +
                                    "Uws_Number," +
                                    "Is_HotelMark_Promo," +
                                    "Sub_Ttl," +
                                    "Tax_Ttl," +
                                    "Auto_Svc_Ttl," +
                                    "Other_Svc_Ttl," +
                                    "Dsc_Ttl," +
                                    "Pymnt_Ttl," +
                                    "Chk_Prntd_Cnt," +
                                    "Cov_Cnt," +
                                    "Num_Dtl," +
                                    "Itemizer1," +
                                    "Itemizer2," +
                                    "Itemizer3," +
                                    "Itemizer4," +
                                    "Itemizer5," +
                                    "Itemizer6," +
                                    "Itemizer7," +
                                    "Itemizer8," +
                                    "Itemizer9," +
                                    "Itemizer10," +
                                    "Itemizer11," +
                                    "Itemizer12," +
                                    "Itemizer13," +
                                    "Itemizer14," +
                                    "Itemizer15," +
                                    "Itemizer16," +
                                    "Tip_ttl," +
                                    "DateCreated," +
                                    "CreatedBy," +
                                    "Filename," +
                                    "BatchID" +
								") VALUES (" +
                                    "@fk_business_date," +
                                    "@fk_location_def," +
                                    "@fk_emp_def," +
                                    "@status_flag," +
                                    "@chk_headers_seq_number," +
                                    "@chk_num," +
                                    "@chk_id," +
                                    "@ot_number," +
                                    "@Ot_Name," +
                                    "@Tbl_Number," +
                                    "@Chk_Open_Date_Time," +
                                    "@Chk_Closed_Date_Time," +
                                    "@Uws_Number," +
                                    "@Is_HotelMark_Promo," +
                                    "@Sub_Ttl," +
                                    "@Tax_Ttl," +
                                    "@Auto_Svc_Ttl," +
                                    "@Other_Svc_Ttl," +
                                    "@Dsc_Ttl," +
                                    "@Pymnt_Ttl," +
                                    "@Chk_Prntd_Cnt," +
                                    "@Cov_Cnt," +
                                    "@Num_Dtl," +
                                    "@Itemizer1," +
                                    "@Itemizer2," +
                                    "@Itemizer3," +
                                    "@Itemizer4," +
                                    "@Itemizer5," +
                                    "@Itemizer6," +
                                    "@Itemizer7," +
                                    "@Itemizer8," +
                                    "@Itemizer9," +
                                    "@Itemizer10," +
                                    "@Itemizer11," +
                                    "@Itemizer12," +
                                    "@Itemizer13," +
                                    "@Itemizer14," +
                                    "@Itemizer15," +
                                    "@Itemizer16," +
                                    "@Tip_ttl," +
                                    "@DateCreated," +
                                    "@CreatedBy," +
                                    "@Filename," +
                                    "@BatchID);";
	 			
				MySqlCommand cmd = new MySqlCommand();
				cmd.CommandType = System.Data.CommandType.Text;
				cmd.CommandText = SQL;

                cmd.Parameters.AddWithValue("@fk_business_date", Details.fk_business_date.ToString("yyyy-MM-dd HH:mm:ss"));
                cmd.Parameters.AddWithValue("@fk_location_def", Details.fk_location_def);
                cmd.Parameters.AddWithValue("@fk_emp_def", Details.fk_emp_def);
                cmd.Parameters.AddWithValue("@status_flag", Details.status_flag);
                cmd.Parameters.AddWithValue("@chk_headers_seq_number", Details.chk_headers_seq_number);
                cmd.Parameters.AddWithValue("@chk_num", Details.chk_num);
                cmd.Parameters.AddWithValue("@chk_id", Details.chk_id);
                cmd.Parameters.AddWithValue("@ot_number", Details.ot_number);
                cmd.Parameters.AddWithValue("@Ot_Name", Details.Ot_Name);
                cmd.Parameters.AddWithValue("@Tbl_Number", Details.Tbl_Number);
                cmd.Parameters.AddWithValue("@Chk_Open_Date_Time", Details.Chk_Open_Date_Time.ToString("yyyy-MM-dd HH:mm:ss"));
                cmd.Parameters.AddWithValue("@Chk_Closed_Date_Time", Details.Chk_Closed_Date_Time.ToString("yyyy-MM-dd HH:mm:ss"));
                cmd.Parameters.AddWithValue("@Uws_Number", Details.Uws_Number);
                cmd.Parameters.AddWithValue("@Is_HotelMark_Promo", Details.Is_HotelMark_Promo);
                cmd.Parameters.AddWithValue("@Sub_Ttl", Details.Sub_Ttl);
                cmd.Parameters.AddWithValue("@Tax_Ttl", Details.Tax_Ttl);
                cmd.Parameters.AddWithValue("@Auto_Svc_Ttl", Details.Auto_Svc_Ttl);
                cmd.Parameters.AddWithValue("@Other_Svc_Ttl", Details.Other_Svc_Ttl);
                cmd.Parameters.AddWithValue("@Dsc_Ttl", Details.Dsc_Ttl);
                cmd.Parameters.AddWithValue("@Pymnt_Ttl", Details.Pymnt_Ttl);
                cmd.Parameters.AddWithValue("@Chk_Prntd_Cnt", Details.Chk_Prntd_Cnt);
                cmd.Parameters.AddWithValue("@Cov_Cnt", Details.Cov_Cnt);
                cmd.Parameters.AddWithValue("@Num_Dtl", Details.Num_Dtl);
                cmd.Parameters.AddWithValue("@Itemizer1", Details.Itemizer1);
                cmd.Parameters.AddWithValue("@Itemizer2", Details.Itemizer2);
                cmd.Parameters.AddWithValue("@Itemizer3", Details.Itemizer3);
                cmd.Parameters.AddWithValue("@Itemizer4", Details.Itemizer4);
                cmd.Parameters.AddWithValue("@Itemizer5", Details.Itemizer5);
                cmd.Parameters.AddWithValue("@Itemizer6", Details.Itemizer6);
                cmd.Parameters.AddWithValue("@Itemizer7", Details.Itemizer7);
                cmd.Parameters.AddWithValue("@Itemizer8", Details.Itemizer8);
                cmd.Parameters.AddWithValue("@Itemizer9", Details.Itemizer9);
                cmd.Parameters.AddWithValue("@Itemizer10", Details.Itemizer10);
                cmd.Parameters.AddWithValue("@Itemizer11", Details.Itemizer11);
                cmd.Parameters.AddWithValue("@Itemizer12", Details.Itemizer12);
                cmd.Parameters.AddWithValue("@Itemizer13", Details.Itemizer13);
                cmd.Parameters.AddWithValue("@Itemizer14", Details.Itemizer14);
                cmd.Parameters.AddWithValue("@Itemizer15", Details.Itemizer15);
                cmd.Parameters.AddWithValue("@Itemizer16", Details.Itemizer16);
                cmd.Parameters.AddWithValue("@Tip_ttl", Details.Tip_ttl);
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

		public bool Delete(string IDs)
		{
			try 
			{

                string SQL = "DELETE FROM tblgla_f_dtl_chk_headers WHERE TransactionDate (" + IDs + ");";

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


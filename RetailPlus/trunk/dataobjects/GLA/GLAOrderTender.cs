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
	public struct GLAOrderTenderDetails
	{
        public string identifier;
        public long order_hdr_id;
        public long tender_seq;
        public long tender_id;
        public decimal tender_amt;
        public decimal prorata_sales_amt_gross;
        public decimal prorata_discount_amt;
        public decimal prorata_tax_amt;
        public decimal prorata_grat_amt;
        public decimal prorata_svc_chg_amt;
        public decimal tip_amt;
        public decimal breakage_amt;
        public decimal received_curr_amt;
        public int curr_decimal_places;
        public int exchange_rate;
        public decimal change_amt;
        public int change_tender_id;
        public int tax_removed_code;
        public int tender_type_id;
        public int subtender_id;
        public string auth_acct_no;
        public string post_acct_no;
        public string customer_name;
        public string adtnl_info;
        public int subtender_qty;
        public decimal charges_to_date_amt;
        public decimal remaining_balance_amt;
        public bool PMS_post_flag;
        public bool sales_tippable_flag;
        public bool post_system1_flag;
        public bool post_system2_flag;
        public bool post_system3_flag;
        public bool post_system4_flag;
        public bool post_system5_flag;
        public bool post_system6_flag;
        public bool post_system7_flag;
        public bool post_system8_flag;
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
	public class GLAOrderTender : POSConnection
	{
		
		#region Constructors and Destructors

		public GLAOrderTender()
            : base(null, null)
        {
        }

        public GLAOrderTender(MySqlConnection Connection, MySqlTransaction Transaction) 
            : base(Connection, Transaction)
		{

		}

		#endregion

		#region Insert and Update

        public Int64 Insert(GLAOrderTenderDetails Details)
		{
			try  
			{
                string SQL = "INSERT INTO tblgla_order_tender (" +
                                    "identifier," +
                                    "order_hdr_id," +
                                    "tender_seq," +
                                    "tender_id," +
                                    "tender_amt," +
                                    "prorata_sales_amt_gross," +
                                    "prorata_discount_amt," +
                                    "prorata_tax_amt," +
                                    "prorata_grat_amt," +
                                    "prorata_svc_chg_amt," +
                                    "tip_amt," +
                                    "breakage_amt," +
                                    "received_curr_amt," +
                                    "curr_decimal_places," +
                                    "exchange_rate," +
                                    "change_amt," +
                                    "change_tender_id," +
                                    "tax_removed_code," +
                                    "tender_type_id," +
                                    "subtender_id," +
                                    "auth_acct_no," +
                                    "post_acct_no," +
                                    "customer_name," +
                                    "adtnl_info," +
                                    "subtender_qty," +
                                    "charges_to_date_amt," +
                                    "remaining_balance_amt," +
                                    "PMS_post_flag," +
                                    "sales_tippable_flag," +
                                    "post_system1_flag," +
                                    "post_system2_flag," +
                                    "post_system3_flag," +
                                    "post_system4_flag," +
                                    "post_system5_flag," +
                                    "post_system6_flag," +
                                    "post_system7_flag," +
                                    "DateCreated," +
                                    "CreatedBy," +
                                    "Filename," +
                                    "BatchID" +
								") VALUES (" +
                                    "@identifier," +
                                    "@order_hdr_id," +
                                    "@tender_seq," +
                                    "@tender_id," +
                                    "@tender_amt," +
                                    "@prorata_sales_amt_gross," +
                                    "@prorata_discount_amt," +
                                    "@prorata_tax_amt," +
                                    "@prorata_grat_amt," +
                                    "@prorata_svc_chg_amt," +
                                    "@tip_amt," +
                                    "@breakage_amt," +
                                    "@received_curr_amt," +
                                    "@curr_decimal_places," +
                                    "@exchange_rate," +
                                    "@change_amt," +
                                    "@change_tender_id," +
                                    "@tax_removed_code," +
                                    "@tender_type_id," +
                                    "@subtender_id," +
                                    "@auth_acct_no," +
                                    "@post_acct_no," +
                                    "@customer_name," +
                                    "@adtnl_info," +
                                    "@subtender_qty," +
                                    "@charges_to_date_amt," +
                                    "@remaining_balance_amt," +
                                    "@PMS_post_flag," +
                                    "@sales_tippable_flag," +
                                    "@post_system1_flag," +
                                    "@post_system2_flag," +
                                    "@post_system3_flag," +
                                    "@post_system4_flag," +
                                    "@post_system5_flag," +
                                    "@post_system6_flag," +
                                    "@post_system7_flag," +
                                    "@DateCreated," +
                                    "@CreatedBy," +
                                    "@Filename," +
                                    "@BatchID);";
	 			
				MySqlCommand cmd = new MySqlCommand();
				cmd.CommandType = System.Data.CommandType.Text;
				cmd.CommandText = SQL;

                cmd.Parameters.AddWithValue("@identifier", Details.identifier);
                cmd.Parameters.AddWithValue("@order_hdr_id", Details.order_hdr_id);
                cmd.Parameters.AddWithValue("@tender_seq", Details.tender_seq);
                cmd.Parameters.AddWithValue("@tender_id", Details.tender_id);
                cmd.Parameters.AddWithValue("@tender_amt", Details.tender_amt);
                cmd.Parameters.AddWithValue("@prorata_sales_amt_gross", Details.prorata_sales_amt_gross);
                cmd.Parameters.AddWithValue("@prorata_discount_amt", Details.prorata_discount_amt);
                cmd.Parameters.AddWithValue("@prorata_tax_amt", Details.prorata_tax_amt);
                cmd.Parameters.AddWithValue("@prorata_grat_amt", Details.prorata_grat_amt);
                cmd.Parameters.AddWithValue("@prorata_svc_chg_amt", Details.prorata_svc_chg_amt);
                cmd.Parameters.AddWithValue("@tip_amt", Details.tip_amt);
                cmd.Parameters.AddWithValue("@breakage_amt", Details.breakage_amt);
                cmd.Parameters.AddWithValue("@received_curr_amt", Details.received_curr_amt);
                cmd.Parameters.AddWithValue("@curr_decimal_places", Details.curr_decimal_places);
                cmd.Parameters.AddWithValue("@exchange_rate", Details.exchange_rate);
                cmd.Parameters.AddWithValue("@change_amt", Details.change_amt);
                cmd.Parameters.AddWithValue("@change_tender_id", Details.change_tender_id);
                cmd.Parameters.AddWithValue("@tax_removed_code", Details.tax_removed_code);
                cmd.Parameters.AddWithValue("@tender_type_id", Details.tender_type_id);
                cmd.Parameters.AddWithValue("@subtender_id", Details.subtender_id);
                cmd.Parameters.AddWithValue("@auth_acct_no", Details.auth_acct_no);
                cmd.Parameters.AddWithValue("@post_acct_no", Details.post_acct_no);
                cmd.Parameters.AddWithValue("@customer_name", Details.customer_name);
                cmd.Parameters.AddWithValue("@adtnl_info", Details.adtnl_info);
                cmd.Parameters.AddWithValue("@subtender_qty", Details.subtender_qty);
                cmd.Parameters.AddWithValue("@charges_to_date_amt", Details.charges_to_date_amt);
                cmd.Parameters.AddWithValue("@remaining_balance_amt", Details.remaining_balance_amt);
                cmd.Parameters.AddWithValue("@PMS_post_flag", Details.PMS_post_flag);
                cmd.Parameters.AddWithValue("@sales_tippable_flag", Details.sales_tippable_flag);
                cmd.Parameters.AddWithValue("@post_system1_flag", Details.post_system1_flag);
                cmd.Parameters.AddWithValue("@post_system2_flag", Details.post_system2_flag);
                cmd.Parameters.AddWithValue("@post_system3_flag", Details.post_system3_flag);
                cmd.Parameters.AddWithValue("@post_system4_flag", Details.post_system4_flag);
                cmd.Parameters.AddWithValue("@post_system5_flag", Details.post_system5_flag);
                cmd.Parameters.AddWithValue("@post_system6_flag", Details.post_system6_flag);
                cmd.Parameters.AddWithValue("@post_system7_flag", Details.post_system7_flag);
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

                string SQL = "DELETE FROM tblgla_order_tender WHERE TransactionDate (" + IDs + ");";

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


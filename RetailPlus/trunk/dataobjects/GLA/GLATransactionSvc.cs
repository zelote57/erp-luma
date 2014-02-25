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
    public struct GLATransactionSvcDetails
    {
        public DateTime fk_business_date;
        public int fk_location_def;
        public int fk_emp_def;
        public long fk_chk_headers;
        public int fk_svc_def;
        public int fk_auth_emp_def;
        public DateTime Transaction_Date_Time;
        public string status_flag;
        public int Round_Num;
        public int Dtl_Num;
        public int Svc_Count;
        public decimal Svc_Total;
        public string Ref_Info_1;
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
    public class GLATransactionSvc : POSConnection
    {

        #region Constructors and Destructors

        public GLATransactionSvc()
            : base(null, null)
        {
        }

        public GLATransactionSvc(MySqlConnection Connection, MySqlTransaction Discount)
            : base(Connection, Discount)
        {

        }

        #endregion

        #region Insert and Update

        public Int64 Insert(GLATransactionSvcDetails Details)
        {
            try
            {
                string SQL = "INSERT INTO tblgla_f_dtl_chk_svc (" +
                                    "fk_business_date," +
                                    "fk_location_def," +
                                    "fk_emp_def," +
                                    "fk_chk_headers," +
                                    "fk_svc_def," +
                                    "fk_auth_emp_def," +
                                    "Transaction_Date_Time," +
                                    "status_flag," +
                                    "Round_Num," +
                                    "Dtl_Num," +
                                    "Svc_Count," +
                                    "Svc_Total," +
                                    "Ref_Info_1," +
                                    "DateCreated," +
                                    "CreatedBy," +
                                    "Filename," +
                                    "BatchID" +
                                ") VALUES (" +
                                    "@fk_business_date," +
                                    "@fk_location_def," +
                                    "@fk_emp_def," +
                                    "@fk_chk_headers," +
                                    "@fk_svc_def," +
                                    "@fk_auth_emp_def," +
                                    "@Transaction_Date_Time," +
                                    "@status_flag," +
                                    "@Round_Num," +
                                    "@Dtl_Num," +
                                    "@Svc_Count," +
                                    "@Svc_Total," +
                                    "@Ref_Info_1," +
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
                cmd.Parameters.AddWithValue("@fk_chk_headers", Details.fk_chk_headers);
                cmd.Parameters.AddWithValue("@fk_svc_def", Details.fk_svc_def);
                cmd.Parameters.AddWithValue("@fk_auth_emp_def", Details.fk_auth_emp_def);
                cmd.Parameters.AddWithValue("@Transaction_Date_Time", Details.Transaction_Date_Time.ToString("yyyy-MM-dd HH:mm:ss"));
                cmd.Parameters.AddWithValue("@status_flag", Details.status_flag);
                cmd.Parameters.AddWithValue("@Round_Num", Details.Round_Num);
                cmd.Parameters.AddWithValue("@Dtl_Num", Details.Dtl_Num);
                cmd.Parameters.AddWithValue("@Svc_Count", Details.Svc_Count);
                cmd.Parameters.AddWithValue("@Svc_Total", Details.Svc_Total);
                cmd.Parameters.AddWithValue("@Ref_Info_1", Details.Ref_Info_1);
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

                string SQL = "DELETE FROM tblgla_f_dtl_chk_svc WHERE BatchID  = '" + BatchID + "';";

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


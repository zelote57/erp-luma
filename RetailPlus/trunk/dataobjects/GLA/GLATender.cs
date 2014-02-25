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
    public struct GLATenderDetails
    {
        public long Tmd_Number;
        public string Tmd_Name;
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
    public class GLATender : POSConnection
    {

        #region Constructors and Destructors

        public GLATender()
            : base(null, null)
        {
        }

        public GLATender(MySqlConnection Connection, MySqlTransaction Discount)
            : base(Connection, Discount)
        {

        }

        #endregion

        #region Insert and Update

        public Int64 Insert(GLATenderDetails Details)
        {
            try
            {
                string SQL = "INSERT INTO tblgla_d_tmd_def (" +
                                    "Tmd_Number," +
                                    "Tmd_Name," +
                                    "DateCreated," +
                                    "CreatedBy," +
                                    "Filename," +
                                    "BatchID" +
                                ") VALUES (" +
                                    "@Tmd_Number," +
                                    "@Tmd_Name," +
                                    "@DateCreated," +
                                    "@CreatedBy," +
                                    "@Filename," +
                                    "@BatchID);";

                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandText = SQL;

                cmd.Parameters.AddWithValue("@Tmd_Number", Details.Tmd_Number);
                cmd.Parameters.AddWithValue("@Tmd_Name", Details.Tmd_Name);
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

                string SQL = "DELETE FROM tblgla_d_tmd_def WHERE BatchID  = '" + BatchID + "';";

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


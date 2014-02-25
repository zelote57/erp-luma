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
    public struct GLAItemDetails
    {
        public long Rvc_Number;
        public long Mi_Number;
        public int Def_Seq;
        public string Mi_Name;
        public long Sales_Itemizer_Number;
        public string Sales_Itemizer_Name;
        public long Family_Group_Number;
        public string Family_Group_Name;
        public long Major_Group_Number;
        public string Major_Group_Name;
        public long Mi_Class_Number;
        public string Mi_Class_Name;
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
    public class GLAItem : POSConnection
    {

        #region Constructors and Destructors

        public GLAItem()
            : base(null, null)
        {
        }

        public GLAItem(MySqlConnection Connection, MySqlTransaction Discount)
            : base(Connection, Discount)
        {

        }

        #endregion

        #region Insert and Update

        public Int64 Insert(GLAItemDetails Details)
        {
            try
            {
                string SQL = "INSERT INTO tblgla_d_mi_def (" +
                                    "Rvc_Number," +
                                    "Mi_Number," +
                                    "Def_Seq," +
                                    "Mi_Name," +
                                    "Sales_Itemizer_Number," +
                                    "Sales_Itemizer_Name," +
                                    "Family_Group_Number," +
                                    "Family_Group_Name," +
                                    "Major_Group_Number," +
                                    "Major_Group_Name," +
                                    "Mi_Class_Number," +
                                    "Mi_Class_Name," +
                                    "DateCreated," +
                                    "CreatedBy," +
                                    "Filename," +
                                    "BatchID" +
                                ") VALUES (" +
                                    "@Rvc_Number," +
                                    "@Mi_Number," +
                                    "@Def_Seq," +
                                    "@Mi_Name," +
                                    "@Sales_Itemizer_Number," +
                                    "@Sales_Itemizer_Name," +
                                    "@Family_Group_Number," +
                                    "@Family_Group_Name," +
                                    "@Major_Group_Number," +
                                    "@Major_Group_Name," +
                                    "@Mi_Class_Number," +
                                    "@Mi_Class_Name," +
                                    "@DateCreated," +
                                    "@CreatedBy," +
                                    "@Filename," +
                                    "@BatchID);";

                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandText = SQL;

                cmd.Parameters.AddWithValue("@Rvc_Number", Details.Rvc_Number);
                cmd.Parameters.AddWithValue("@Mi_Number", Details.Mi_Number);
                cmd.Parameters.AddWithValue("@Def_Seq", Details.Def_Seq);
                cmd.Parameters.AddWithValue("@Mi_Name", Details.Mi_Name);
                cmd.Parameters.AddWithValue("@Sales_Itemizer_Number", Details.Sales_Itemizer_Number);
                cmd.Parameters.AddWithValue("@Sales_Itemizer_Name", Details.Sales_Itemizer_Name);
                cmd.Parameters.AddWithValue("@Family_Group_Number", Details.Family_Group_Number);
                cmd.Parameters.AddWithValue("@Family_Group_Name", Details.Family_Group_Name);
                cmd.Parameters.AddWithValue("@Major_Group_Number", Details.Major_Group_Number);
                cmd.Parameters.AddWithValue("@Major_Group_Name", Details.Major_Group_Name);
                cmd.Parameters.AddWithValue("@Mi_Class_Number", Details.Mi_Class_Number);
                cmd.Parameters.AddWithValue("@Mi_Class_Name", Details.Mi_Class_Name);
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

                string SQL = "DELETE FROM tblgla_d_mi_def WHERE BatchID  = '" + BatchID + "';";

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


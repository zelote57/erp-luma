using System;
using System.Data;
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
	#region Struct

    public struct ContactAddOnDetails
	{
        public Int64 ContactDetailID;
		public Int64 ContactID;
		public string Salutation;
		public string FirstName;
        public string MiddleName;
        public string LastName;
        public string SpouseName;
        public DateTime BirthDate;
        public DateTime SpouseBirthDate;
        public DateTime AnniversaryDate;
        public string Address1;
        public string Address2;
        public string City;
        public string State;
        public string ZipCode;
		public Int32 CountryID;
        public string CountryCode;
        public string BusinessPhoneNo;
        public string HomePhoneNo;
        public string MobileNo;
        public string FaxNo;
        public string EmailAddress;

        public DateTime CreatedOn;
        public DateTime LastModified;
	}

	#endregion

	[StrongNameIdentityPermissionAttribute(SecurityAction.LinkDemand,
		 PublicKey = "002400000480000094000000060200000024000" +
		 "052534131000400000100010053D785642F9F960B43157E0380" +
		 "F393BEE53E8DFAFBF441366C1B6F8B48D9DDF0D527B1F3B21EA" +
		 "E85D2FDB664CE81EB8A87DBE4C589D6F4202FE2B7D4B978BB69" +
		 "684874612CB9B8DB7A0339400A9C4E68277884B07817363D242" +
		 "E3696F9FACDBEA831810AE6DC9EDCA91A7B5DA12FE7BF65D113" +
		 "FF52834EAFB5A7A1FDFD5851A3")]
	public class ContactAddOns : POSConnection
	{

		#region Constructors and Destructors

		public ContactAddOns()
            : base(null, null)
        {
        }

        public ContactAddOns(MySqlConnection Connection, MySqlTransaction Transaction) 
            : base(Connection, Transaction)
		{

		}

		#endregion

		#region Insert and Update

		private Int64 Insert(ContactAddOnDetails Details)
		{
			try  
			{
                Save(Details);

                string SQL = "SELECT LAST_INSERT_ID();";
				  	 			
				MySqlCommand cmd = new MySqlCommand();
				cmd.CommandType = System.Data.CommandType.Text;
				cmd.CommandText = SQL;

                MySqlDataReader myReader = base.ExecuteReader(cmd, System.Data.CommandBehavior.SingleResult);

                Int64 iID = 0;

				while (myReader.Read()) 
				{
					iID = myReader.GetInt64(0);
				}

				myReader.Close();

				return iID;
			}

			catch (Exception ex)
			{
				throw base.ThrowException(ex);
			}	
		}
        private void Update(ContactAddOnDetails Details)
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

        //public void Save(ContactAddOnDetails Details)
        //{
        //    if (isExist(Details.ContactID) && Details.ContactID != 0)
        //    {
        //        Update(Details);
        //    }
        //    else if (Details.ContactID != 0)
        //    {
        //        Insert(Details);
        //    }
        //}

        public Int32 Save(ContactAddOnDetails Details)
        {
            try
            {
                string SQL = "CALL procSaveContactAddOn(@ContactDetailID, @ContactID, @Salutation, @FirstName, @MiddleName, @LastName," +
                                "@SpouseName, @BirthDate, @SpouseBirthDate, @AnniversaryDate, @Address1, @Address2," +
                                "@City, @State, @ZipCode, @CountryID, @BusinessphoneNo, @HomephoneNo, @MobileNo," +
                                "@FaxNo, @EmailAddress, @CreatedOn, @LastModified);";

                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandText = SQL;

                cmd.Parameters.AddWithValue("ContactDetailID", Details.ContactDetailID);
                cmd.Parameters.AddWithValue("ContactID", Details.ContactID);
                cmd.Parameters.AddWithValue("Salutation", Details.Salutation);
                cmd.Parameters.AddWithValue("FirstName", Details.FirstName);
                cmd.Parameters.AddWithValue("MiddleName", Details.MiddleName);
                cmd.Parameters.AddWithValue("LastName", Details.LastName);
                cmd.Parameters.AddWithValue("SpouseName", Details.SpouseName);
                cmd.Parameters.AddWithValue("BirthDate", Details.BirthDate);
                cmd.Parameters.AddWithValue("SpouseBirthDate", Details.SpouseBirthDate);
                cmd.Parameters.AddWithValue("AnniversaryDate", Details.AnniversaryDate);
                cmd.Parameters.AddWithValue("Address1", Details.Address1);
                cmd.Parameters.AddWithValue("Address2", Details.Address2);
                cmd.Parameters.AddWithValue("City", Details.City);
                cmd.Parameters.AddWithValue("State", Details.State);
                cmd.Parameters.AddWithValue("ZipCode", Details.ZipCode);
                cmd.Parameters.AddWithValue("CountryID", Details.CountryID);
                cmd.Parameters.AddWithValue("BusinessPhoneNo", Details.BusinessPhoneNo);
                cmd.Parameters.AddWithValue("HomePhoneNo", Details.HomePhoneNo);
                cmd.Parameters.AddWithValue("MobileNo", Details.MobileNo);
                cmd.Parameters.AddWithValue("FaxNo", Details.FaxNo);
                cmd.Parameters.AddWithValue("EmailAddress", Details.EmailAddress);
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
				string SQL=	"DELETE FROM tblContactAddOn WHERE ContactID IN (" + IDs + ");";
				  
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

        public ContactAddOnDetails Details(long ContactID)
        {
            try
            {
                System.Data.DataTable dt = ListAsDataTable(ContactID);
                ContactAddOnDetails clsContactAddOnDetails = setDetails(dt);

                return clsContactAddOnDetails;
            }

            catch (Exception ex)
            {
                throw base.ThrowException(ex);
            }
        }

        private ContactAddOnDetails setDetails(System.Data.DataTable dt)
        {
            ContactAddOnDetails Details = new ContactAddOnDetails();

            try
            {
                foreach(System.Data.DataRow dr in dt.Rows)
                {
                    Details.ContactID = Int64.Parse(dr["ContactID"].ToString());
                    Details.Salutation = "" + dr["Salutation"].ToString();
                    Details.FirstName = "" + dr["FirstName"].ToString();
                    Details.MiddleName = "" + dr["MiddleName"].ToString();
                    Details.LastName = "" + dr["LastName"].ToString();
                    Details.SpouseName = "" + dr["SpouseName"].ToString();
                    Details.BirthDate = DateTime.Parse(dr["BirthDate"].ToString());
                    Details.SpouseBirthDate = DateTime.Parse(dr["SpouseBirthDate"].ToString());
                    Details.AnniversaryDate = DateTime.Parse(dr["AnniversaryDate"].ToString());
                    Details.Address1 = "" + dr["Address1"].ToString();
                    Details.Address2 = "" + dr["Address2"].ToString();
                    Details.City = "" + dr["City"].ToString();
                    Details.State = "" + dr["State"].ToString();
                    Details.ZipCode = "" + dr["ZipCode"].ToString();
                    Details.CountryID = Int32.Parse(dr["CountryID"].ToString());
                    Details.CountryCode = "" + dr["CountryName"].ToString();
                    Details.BusinessPhoneNo = "" + dr["BusinessPhoneNo"].ToString();
                    Details.HomePhoneNo = "" + dr["HomePhoneNo"].ToString();
                    Details.MobileNo = "" + dr["MobileNo"].ToString();
                    Details.FaxNo = "" + dr["FaxNo"].ToString();
                    Details.EmailAddress = "" + dr["EmailAddress"].ToString();
                }
            }
            catch (Exception ex) { throw base.ThrowException(ex); }
            return Details;
        }

        #endregion

        #region Streams

        private bool isExist(long ContactID)
        {
            try
            {
                string SQL = "SELECT ContactID FROM tblContactAddOn WHERE ContactID = @ContactID;";

                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandText = SQL;

                cmd.Parameters.AddWithValue("@ContactID", ContactID);

                string strDataTableName = "tbl" + this.GetType().FullName.Split(new Char[] { '.' })[this.GetType().FullName.Split(new Char[] { '.' }).Length - 1]; System.Data.DataTable dt = new System.Data.DataTable(strDataTableName);
                base.MySqlDataAdapterFill(cmd, dt);

                bool boRetvalue = false;

                if (dt.Rows.Count > 0) boRetvalue = true;

                return boRetvalue;
            }

            catch (Exception ex)
            {
                throw base.ThrowException(ex);
            }
        }

        public DataTable ListAsDataTable(long ContactID = 0,string Name = "")
        {
            string SQL = "CALL procContactAddOnSelect(@ContactID, @Name)";

            MySqlCommand cmd = new MySqlCommand();
            cmd.CommandType = System.Data.CommandType.Text;
            cmd.CommandText = SQL;

            cmd.Parameters.AddWithValue("@ContactID", ContactID);
            cmd.Parameters.AddWithValue("@Name", Name);

            string strDataTableName = "tbl" + this.GetType().FullName.Split(new Char[] { '.' })[this.GetType().FullName.Split(new Char[] { '.' }).Length - 1]; System.Data.DataTable dt = new System.Data.DataTable(strDataTableName);
            base.MySqlDataAdapterFill(cmd, dt);

            return dt;
        }

        #endregion

    }
}
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

    public struct ContactDetails
	{
		public Int64 ContactID;
        public Int32 SequenceNo;
		public string ContactCode;
		public string ContactName;
		public Int32 ContactGroupID;
		public string ContactGroupName;
		public ModeOfTerms ModeOfTerms;
		public Int32 Terms;
		public string Address;
		public string BusinessName;
		public string TelephoneNo;
		public string Remarks;
		public decimal Debit;
		public decimal Credit;
		public decimal CreditLimit;
		public bool IsCreditAllowed;
		public DateTime DateCreated;
		public bool Deleted;
        public int DepartmentID;
        public string DepartmentName;
        public int PositionID;
        public string PositionName;

        // Sep 14, 2011 : Lemu for reward points details
        public ContactRewardDetails RewardDetails;

        // Nov 2, 2011 : Lemu for credit details
        public ContactCreditCardInfoDetails CreditDetails;

        // May 14, 2013
        public bool isLock;

        // Sep 15, 2013 : for additional contact info
        public ContactAddOnDetails AdditionalDetails;

        /// <summary>
        /// sep 24, 2014 : For RestoPlus to monitor how long a customer is already sitting in a table.
        /// </summary>
        public DateTime LastCheckInDate;

        public DateTime CreatedOn;
        public DateTime LastModified;

	}

    public struct ContactColumns
    {
        public bool ContactID;
        public bool SequenceNo;
        public bool ContactCode;
        public bool ContactName;
        public bool ContactGroupID;
        public bool ContactGroupName;
        public bool ModeOfTerms;
        public bool Terms;
        public bool Address;
        public bool BusinessName;
        public bool TelephoneNo;
        public bool Remarks;
        public bool Debit;
        public bool Credit;
        public bool CreditLimit;
        public bool IsCreditAllowed;
        public bool DateCreated;
        public bool Deleted;
        public bool DepartmentID;
        public bool DepartmentName;
        public bool PositionID;
        public bool PositionName;
        public bool LastCheckInDate;
        public bool RewardDetails;
        public bool CreditDetails;
    }

    public struct ContactColumnNames
    {
        public const string ContactID = "ContactID";
        public const string SequenceNo = "SequenceNo";
        public const string ContactCode = "ContactCode";
        public const string ContactName = "ContactName";
        public const string ContactGroupID = "ContactGroupID";
        public const string ContactGroupName = "ContactGroupName";
        public const string ModeOfTerms = "ModeOfTerms";
        public const string Terms = "Terms";
        public const string Address = "Address";
        public const string BusinessName = "BusinessName";
        public const string TelephoneNo = "TelephoneNo";
        public const string Remarks = "Remarks";
        public const string Debit = "Debit";
        public const string Credit = "Credit";
        public const string CreditLimit = "CreditLimit";
        public const string IsCreditAllowed = "IsCreditAllowed";
        public const string DateCreated = "DateCreated";
        public const string Deleted = "Deleted";
        public const string DepartmentID = "DepartmentID";
        public const string DepartmentName = "DepartmentName";
        public const string PositionID = "DepartmentName";
        public const string PositionName = "PositionName";
        public const string LastCheckInDate = "LastCheckInDate";
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
	public class Contacts : POSConnection
	{
		public const string DEFAULT_REMARKS_FOR_ADDED_FROM_CLIENT = "ADDED DURING SUSPEND TRANSACTION.";
		public const string DEFAULT_REMARKS_FOR_ADDED_FROM_DEPOSIT = "ADDED DURING DEPOSIT TRANSACTION.";
        public const string DEFAULT_REMARKS_FOR_QUICKLY_ADDED_FROM_FE = "QUICKLY ADDED DURING FRONT-END.";

		public const long DEFAULT_SUPPLIER_ID = 2;
        public const string DEFAULT_SUPPLIER_NAME = "RetailPlus Supplier ™";

        public ContactAddOns _ContactAddOns;

		#region Constructors and Destructors

		public Contacts()
            : base(null, null)
        {
            _ContactAddOns = new ContactAddOns(base.Connection, base.Transaction);
        }

        public Contacts(MySqlConnection Connection, MySqlTransaction Transaction) 
            : base(Connection, Transaction)
		{
            _ContactAddOns = new ContactAddOns(base.Connection, base.Transaction);
		}

		#endregion

		#region Insert and Update

		public long Insert(ContactDetails Details)
		{
			try  
			{
				string SQL="INSERT INTO tblContacts (" +
                                "SequenceNo, " + 
					            "ContactCode, " + 
					            "ContactName, " +
					            "ContactGroupID, " +
					            "ModeOfTerms, " +
					            "Terms, " +
					            "Address, " +
					            "BusinessName, " +
					            "TelephoneNo, " +
					            "Remarks, " +
					            "Debit, " +
					            "Credit, " +
					            "CreditLimit, " +
					            "IsCreditAllowed, " +
					            "DateCreated," +
                                "DepartmentID," +
                                "PositionID" +
					        ") VALUES (" +
                                "@SequenceNo, " +
					            "@ContactCode, " +
					            "@ContactName, " +
					            "@ContactGroupID, " +
					            "@ModeOfTerms, " +
					            "@Terms, " +
					            "@Address, " +
					            "@BusinessName, " +
					            "@TelephoneNo, " +
					            "@Remarks, " +
					            "@Debit, " +
					            "@Credit, " +
					            "@CrdtLimit, " +
					            "@IsCreditAllowed, " +
					            "@DateCreated," +
                                "@DepartmentID," +
                                "@PositionID" +
					        ");";
				  	 			
				MySqlCommand cmd = new MySqlCommand();

				cmd.CommandType = System.Data.CommandType.Text;
				cmd.CommandText = SQL;

                cmd.Parameters.AddWithValue("@SequenceNo", Details.SequenceNo);
                cmd.Parameters.AddWithValue("@ContactCode", Details.ContactCode);
                cmd.Parameters.AddWithValue("@ContactName", Details.ContactName);
                cmd.Parameters.AddWithValue("@ContactGroupID", Details.ContactGroupID);
                cmd.Parameters.AddWithValue("@ModeOfTerms", Details.ModeOfTerms.ToString("d"));
                cmd.Parameters.AddWithValue("@Terms", Details.Terms);
                cmd.Parameters.AddWithValue("@Address", Details.Address);
                cmd.Parameters.AddWithValue("@BusinessName", Details.BusinessName);
                cmd.Parameters.AddWithValue("@TelephoneNo", Details.TelephoneNo);
                cmd.Parameters.AddWithValue("@Remarks", Details.Remarks);
                cmd.Parameters.AddWithValue("@Debit", Details.Debit);
                cmd.Parameters.AddWithValue("@Credit", Details.Credit);
                cmd.Parameters.AddWithValue("@CrdtLimit", Details.CreditLimit);
                cmd.Parameters.AddWithValue("@IsCreditAllowed", Details.IsCreditAllowed);
                cmd.Parameters.AddWithValue("@DepartmentID", Details.DepartmentID);
                cmd.Parameters.AddWithValue("@PositionID", Details.PositionID);
                cmd.Parameters.AddWithValue("@DateCreated", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));

                base.ExecuteNonQuery(cmd);

				SQL = "SELECT LAST_INSERT_ID();";
				
				cmd.Parameters.Clear(); 
				cmd.CommandText = SQL;

                MySqlDataReader myReader = base.ExecuteReader(cmd, System.Data.CommandBehavior.SingleResult);
				
				long iID = 0;

				while (myReader.Read()) 
				{
					iID = myReader.GetInt64(0);
				}

				myReader.Close();

                //Sep 15, 2013 Include the sepecific details if there's any
                
                if (!string.IsNullOrEmpty(Details.AdditionalDetails.Salutation))
                {
                    Details.AdditionalDetails.ContactID = iID;
                    _ContactAddOns.Save(Details.AdditionalDetails);
                }
                else
                {
                    Details.AdditionalDetails.ContactID = iID;
                    Details.AdditionalDetails.Salutation = "Mr";
                    Details.AdditionalDetails.FirstName = Details.ContactCode;
                    Details.AdditionalDetails.LastName = Details.ContactName;
                    Details.AdditionalDetails.Address1 = Details.Address;
                    Details.AdditionalDetails.AnniversaryDate = DateTime.Now;
                    Details.AdditionalDetails.BirthDate = Details.DateCreated;
                    Details.AdditionalDetails.BusinessPhoneNo = Details.TelephoneNo;
                    Details.AdditionalDetails.CountryCode = "PH";
                    Details.AdditionalDetails.CountryID = 1;
                }

				return iID;
			}

			catch (Exception ex)
			{
				throw base.ThrowException(ex);
			}	
		}
		public void Update(ContactDetails Details)
		{
			try 
			{
                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandType = System.Data.CommandType.Text;
				
				string SQL=	"UPDATE tblContacts SET " +
                                "SequenceNo	    =	@SequenceNo, " +
					            "ContactCode	=	@ContactCode, " +
					            "ContactName	=	@ContactName, " +
					            "ContactGroupID =	@ContactGroupID, " +
					            "ModeOfTerms	=	@ModeOfTerms, " +
					            "Terms			=	@Terms, " +
					            "Address		=	@Address, " +
					            "BusinessName	=	@BusinessName, " +
					            "TelephoneNo	=	@TelephoneNo, " +
					            "Remarks		=	@Remarks, " +
					            "Debit			=	@Debit, " +
					            "Credit			=	@Credit, " +
					            "CreditLimit	=	@CrdtLimit, " +
					            "IsCreditAllowed =	@IsCreditAllowed, " +
                                "DepartmentID   =   @DepartmentID, " +
                                "PositionID     =   @PositionID " +
					        "WHERE ContactID = @ContactID;";


                cmd.Parameters.AddWithValue("@SequenceNo", Details.SequenceNo);
                cmd.Parameters.AddWithValue("@ContactCode", Details.ContactCode);
                cmd.Parameters.AddWithValue("@ContactName", Details.ContactName);
                cmd.Parameters.AddWithValue("@ContactGroupID", Details.ContactGroupID);
                cmd.Parameters.AddWithValue("@ModeOfTerms", Details.ModeOfTerms.ToString("d"));
                cmd.Parameters.AddWithValue("@Terms", Details.Terms);
                cmd.Parameters.AddWithValue("@Address", Details.Address);
                cmd.Parameters.AddWithValue("@BusinessName", Details.BusinessName);
                cmd.Parameters.AddWithValue("@TelephoneNo", Details.TelephoneNo);
                cmd.Parameters.AddWithValue("@Remarks", Details.Remarks);
                cmd.Parameters.AddWithValue("@Debit", Details.Debit);
                cmd.Parameters.AddWithValue("@Credit", Details.Credit);
                cmd.Parameters.AddWithValue("@CrdtLimit", Details.CreditLimit);
                cmd.Parameters.AddWithValue("@IsCreditAllowed", Details.IsCreditAllowed);
                cmd.Parameters.AddWithValue("@DepartmentID", Details.DepartmentID);
                cmd.Parameters.AddWithValue("@PositionID", Details.PositionID);
                cmd.Parameters.AddWithValue("@ContactID", Details.ContactID);

                cmd.CommandText = SQL;
				base.ExecuteNonQuery(cmd);

                //Sep 15, 2013 Include the sepecific details if there's any
                if (Details.AdditionalDetails.ContactID != 0) _ContactAddOns.Save(Details.AdditionalDetails);
			}

			catch (Exception ex)
			{
				throw base.ThrowException(ex);
			}	
		}
		public void Update(string ContactCode, string TransactionNo, long ContactID)
		{
			try 
			{
				string SQL=	"UPDATE tblContacts SET " + 
								"Deleted		=	1, " +
								"ContactCode	=	@NewContact, " +
								"ContactName	=	@NewContact " +
							"WHERE ContactID	=	@ContactID	" +
								"AND Remarks	=	@Remarks";
				  
                MySqlCommand cmd = new MySqlCommand();
			
				cmd.CommandType = System.Data.CommandType.Text;
				cmd.CommandText = SQL;
				
				MySqlParameter prmNewContact = new MySqlParameter("@NewContact",MySqlDbType.String);			
				prmNewContact.Value = ContactCode + "-" + TransactionNo;
				cmd.Parameters.Add(prmNewContact);

				MySqlParameter prmContactID = new MySqlParameter("@ContactID",MySqlDbType.Int16);			
				prmContactID.Value = ContactID;
				cmd.Parameters.Add(prmContactID);

				MySqlParameter prmRemarks = new MySqlParameter("@Remarks",MySqlDbType.String);			
				prmRemarks.Value = DEFAULT_REMARKS_FOR_ADDED_FROM_CLIENT;
				cmd.Parameters.Add(prmRemarks);

				base.ExecuteNonQuery(cmd);
			}

			catch (Exception ex)
			{
				throw base.ThrowException(ex);
			}	
		}

        public void UpdateLastCheckInDate(Int64 ContactID, DateTime CheckInDate)
        {
            try
            {
                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandType = System.Data.CommandType.Text;

                string SQL = "CALL procContactLastCheckInDateUpdate(@ContactID, @LastCheckInDate);";

                cmd.Parameters.AddWithValue("ContactID", ContactID);
                cmd.Parameters.AddWithValue("LastCheckInDate", CheckInDate.ToString("yyyy-MM-dd HH:mm:ss"));

                cmd.CommandText = SQL;
                base.ExecuteNonQuery(cmd);
            }
            catch (Exception ex)
            {
                throw base.ThrowException(ex);
            }
        }

        public Int32 Save(ContactDetails Details)
        {
            try
            {
                string SQL = "CALL procSaveContact(@ContactID, @SequenceNo, @ContactCode, @ContactName, @ContactGroupID, @ModeOfTerms," +
                                            "@Terms, @Address, @BusinessName, @TelephoneNo, @Remarks, @Debit, @Credit," +
                                            "@CreditLimit, @IsCreditAllowed, @DateCreated, @Deleted, @DepartmentID," +
                                            "@PositionID, @isLock, @CreatedOn, @LastModified);";

                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandText = SQL;

                cmd.Parameters.AddWithValue("ContactID", Details.ContactID);
                cmd.Parameters.AddWithValue("SequenceNo", Details.SequenceNo);
                cmd.Parameters.AddWithValue("ContactCode", Details.ContactCode);
                cmd.Parameters.AddWithValue("ContactName", Details.ContactName);
                cmd.Parameters.AddWithValue("ContactGroupID", Details.ContactGroupID);
                cmd.Parameters.AddWithValue("ModeOfTerms", Details.ModeOfTerms);
                cmd.Parameters.AddWithValue("Terms", Details.Terms);
                cmd.Parameters.AddWithValue("Address", Details.Address);
                cmd.Parameters.AddWithValue("BusinessName", Details.BusinessName);
                cmd.Parameters.AddWithValue("TelephoneNo", Details.TelephoneNo);
                cmd.Parameters.AddWithValue("Remarks", Details.Remarks);
                cmd.Parameters.AddWithValue("Debit", Details.Debit);
                cmd.Parameters.AddWithValue("Credit", Details.Credit);
                cmd.Parameters.AddWithValue("CreditLimit", Details.CreditLimit);
                cmd.Parameters.AddWithValue("IsCreditAllowed", Details.IsCreditAllowed);
                cmd.Parameters.AddWithValue("DateCreated", Details.DateCreated);
                cmd.Parameters.AddWithValue("Deleted", Details.Deleted);
                cmd.Parameters.AddWithValue("DepartmentID", Details.DepartmentID);
                cmd.Parameters.AddWithValue("PositionID", Details.PositionID);
                cmd.Parameters.AddWithValue("isLock", Details.isLock);
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
                //Sep 15, 2013 Include the sepecific details if there's any
                _ContactAddOns.Delete(IDs);

				string SQL=	"DELETE FROM tblContacts WHERE ContactID IN (" + IDs + ");";
				  
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
            string stSQL = "SELECT " +
                                "a.ContactID, " +
                                "SequenceNo, " +
                                "ContactCode, " +
                                "ContactName, " +
                                "a.ContactGroupID, " +
                                "b.ContactGroupName, " +
                                "a.ModeOfTerms, " +
                                "a.Terms, " +
                                "a.Address, " +
                                "a.BusinessName, " +
                                "a.TelephoneNo, " +
                                "a.Remarks, " +
                                "a.Debit, " +
                                "a.Credit, " +
                                "a.CreditLimit, " +
                                "a.IsCreditAllowed, " +
                                "a.DateCreated, " +
                                "a.Deleted, " +
                                "a.DepartmentID, " +
                                "DepartmentName, " +
                                "a.PositionID, " +
                                "PositionName, " +
                                "LastCheckInDate, " +
                                "isLock " +
                            "FROM tblContacts a " +
                            "INNER JOIN tblContactGroup b ON a.ContactGroupID = b.ContactGroupID " +
                            "INNER JOIN tblDepartments c ON a.DepartmentID = c.DepartmentID " +
                            "INNER JOIN tblPositions d ON a.PositionID = d.PositionID ";

            return stSQL;
        }
        private string SQLSelect(ContactColumns clsContactColumns)
        {
            string stSQL = "SELECT ";

            if (clsContactColumns.ContactCode) stSQL += "SequenceNo, ";        
            if (clsContactColumns.ContactCode) stSQL+= "ContactCode, ";
            if (clsContactColumns.ContactName) stSQL+= "ContactName, ";
            if (clsContactColumns.ContactGroupID) stSQL+= "tblContacts.ContactGroupID, ";
            if (clsContactColumns.ContactGroupName) stSQL += "tblContactGroup.ContactGroupName, ";
            if (clsContactColumns.ModeOfTerms) stSQL+= "tblContacts.ModeOfTerms, ";
            if (clsContactColumns.Terms) stSQL+= "tblContacts.Terms, ";
            if (clsContactColumns.Address) stSQL+= "tblContacts.Address, ";
            if (clsContactColumns.BusinessName) stSQL+= "tblContacts.BusinessName, ";
            if (clsContactColumns.TelephoneNo) stSQL+= "tblContacts.TelephoneNo, ";
            if (clsContactColumns.Remarks) stSQL+= "tblContacts.Remarks, ";
            if (clsContactColumns.Debit) stSQL+= "tblContacts.Debit, ";
            if (clsContactColumns.Credit) stSQL+= "tblContacts.Credit, ";
            if (clsContactColumns.CreditLimit) stSQL+= "tblContacts.CreditLimit, ";
            if (clsContactColumns.IsCreditAllowed) stSQL += "tblContacts.IsCreditAllowed, ";
            if (clsContactColumns.DateCreated) stSQL+= "tblContacts.DateCreated, ";
            if (clsContactColumns.Deleted) stSQL+= "tblContacts.Deleted, ";
            if (clsContactColumns.DepartmentID) stSQL+= "tblContacts.DepartmentID, ";
            if (clsContactColumns.DepartmentName) stSQL+= "tblDepartments.DepartmentName, ";
            if (clsContactColumns.PositionID) stSQL+= "tblContacts.PositionID, ";
            if (clsContactColumns.PositionName) stSQL+= "tblPositions.PositionName, ";
            if (clsContactColumns.RewardDetails)
            {
                stSQL += "tblContactRewards.RewardCardNo, " +
                        "tblContactRewards.RewardActive, " +
                        "tblContactRewards.RewardPoints, " +
                        "tblContactRewards.RewardAwardDate, " +
                        "tblContactRewards.TotalPurchases, " +
                        "tblContactRewards.RedeemedPoints, " +
                        "tblContactRewards.RewardCardStatus, " +
                        "tblContactRewards.ExpiryDate, " +
                        "tblContactRewards.BirthDate,";
            }
            if (clsContactColumns.CreditDetails)
            {
                if (!clsContactColumns.Credit) stSQL += "tblContacts.Credit, ";
                if (!clsContactColumns.CreditLimit) stSQL += "tblContacts.CreditLimit, ";
                if (!clsContactColumns.IsCreditAllowed) stSQL += "tblContacts.IsCreditAllowed, ";

                stSQL += "tblContactCreditCardInfo.GuarantorID, " +
                        "tblContactCreditCardInfo.CreditCardTypeID, " +
                        "tblContactCreditCardInfo.CreditCardNo, " +
                        "tblContactCreditCardInfo.CreditAwardDate, " +
                        "tblContactCreditCardInfo.TotalPurchases, " +
                        "tblContactCreditCardInfo.CreditPaid, " +
                        "tblContactCreditCardInfo.CreditCardStatus, " +
                        "tblContactCreditCardInfo.ExpiryDate, " +
                        "tblContactCreditCardInfo.EmbossedCardNo, " +
                        "tblContactCreditCardInfo.LastBillingDate, " +
                        "tblCardTypes.CardTypeCode, " +
                        "tblCardTypes.CardTypeName, ";
            }
            stSQL += "tblContacts.LastCheckInDate, ";
            stSQL += "tblContacts.ContactID ";
            stSQL += "FROM tblContacts ";

            if (clsContactColumns.ContactGroupName)
                stSQL += "INNER JOIN tblContactGroup ON tblContacts.ContactGroupID = tblContactGroup.ContactGroupID ";

            if (clsContactColumns.DepartmentName)
                stSQL += "INNER JOIN tblDepartments ON tblContacts.DepartmentID = tblDepartments.DepartmentID ";

            if (clsContactColumns.PositionName)
                stSQL += "INNER JOIN tblPositions ON tblContacts.PositionID = tblPositions.PositionID ";

            if (clsContactColumns.RewardDetails)
                stSQL += "INNER JOIN tblContactRewards ON tblContacts.ContactID = tblContactRewards.CustomerID ";

            if (clsContactColumns.CreditDetails)
            {
                stSQL += "INNER JOIN tblContactCreditCardInfo ON tblContacts.ContactID = tblContactCreditCardInfo.CustomerID ";
                stSQL += "INNER JOIN tblCardTypes ON tblCardTypes.CardTypeID = tblContactCreditCardInfo.CreditCardTypeID ";
            }

            return stSQL;
        }

		#region Details

		public ContactDetails Details(long ContactID)
		{
			try
			{
                System.Data.DataTable dt = ListAsDataTable(ContactGroupCategory.BOTH, ContactID);
                ContactDetails clsContactDetails = setDetails(dt);

                return clsContactDetails;
			}

			catch (Exception ex)
			{
				throw base.ThrowException(ex);
			}	
		}
		public ContactDetails Details(string ContactCode)
		{
			try
			{
                System.Data.DataTable dt = ListAsDataTable(ContactGroupCategory.BOTH, ContactCode: ContactCode);
                ContactDetails clsContactDetails = setDetails(dt);

                return clsContactDetails;
			}

			catch (Exception ex)
			{
				throw base.ThrowException(ex);
			}	
		}

        public ContactDetails DetailsByRewardCardNo(string RewardCardNo)
        {
            try
            {
                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandType = System.Data.CommandType.Text;

                string SQL = SQLSelect() + "WHERE ContactID = (SELECT IFNULL(CustomerID,0) FROM tblContactRewards WHERE RewardActive = 1 AND RewardCardNo = @RewardCardNo LIMIT 1);";

                cmd.Parameters.AddWithValue("@RewardCardNo", RewardCardNo);

                cmd.CommandText = SQL;
                string strDataTableName = "tbl" + this.GetType().FullName.Split(new Char[] { '.' })[this.GetType().FullName.Split(new Char[] { '.' }).Length - 1]; System.Data.DataTable dt = new System.Data.DataTable(strDataTableName);
                base.MySqlDataAdapterFill(cmd, dt);

                ContactDetails clsContactDetails = setDetails(dt);

                return clsContactDetails;
            }

            catch (Exception ex)
            {
                throw base.ThrowException(ex);
            }
        }

        public ContactDetails DetailsByCreditCardNo(string CreditCardNo)
        {
            try
            {
                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandType = System.Data.CommandType.Text;

                string SQL = SQLSelect() + "WHERE ContactID = (SELECT IFNULL(CustomerID,0) FROM tblContactCreditCardInfo WHERE CreditCardNo = @CreditCardNo LIMIT 1);";

                // remove the IsCreditAllowed = 1
                // need to inform the card status in FE
                //string SQL = SQLSelect() + "WHERE IsCreditAllowed = 1 AND ContactID = (SELECT IFNULL(CustomerID,0) FROM tblContactCreditCardInfo WHERE CreditCardNo = @CreditCardNo LIMIT 1);";

                cmd.Parameters.AddWithValue("@CreditCardNo", CreditCardNo);

                cmd.CommandText = SQL;
                string strDataTableName = "tbl" + this.GetType().FullName.Split(new Char[] { '.' })[this.GetType().FullName.Split(new Char[] { '.' }).Length - 1]; System.Data.DataTable dt = new System.Data.DataTable(strDataTableName);
                base.MySqlDataAdapterFill(cmd, dt);

                ContactDetails clsContactDetails = setDetails(dt);

                return clsContactDetails;
            }

            catch (Exception ex)
            {
                throw base.ThrowException(ex);
            }
        }

        private ContactDetails setDetails(System.Data.DataTable dt)
        {
            ContactDetails Details = new ContactDetails();

            try
            {
                foreach(System.Data.DataRow dr in dt.Rows)
                {
                    Details.ContactID = Int64.Parse(dr["ContactID"].ToString());
                    Details.SequenceNo = Int32.Parse(dr["SequenceNo"].ToString());
                    Details.ContactCode = "" + dr["ContactCode"].ToString();
                    Details.ContactName = "" + dr["ContactName"].ToString();
                    Details.ContactGroupID = Int32.Parse(dr["ContactGroupID"].ToString());
                    Details.ContactGroupName = "" + dr["ContactGroupName"].ToString();
                    Details.ModeOfTerms = (ModeOfTerms)Enum.Parse(typeof(ModeOfTerms), dr["ModeOfTerms"].ToString());
                    Details.Terms = Int32.Parse(dr["Terms"].ToString());
                    Details.Address = "" + dr["Address"].ToString();
                    Details.BusinessName = "" + dr["BusinessName"].ToString();
                    Details.TelephoneNo = "" + dr["TelephoneNo"].ToString();
                    Details.Remarks = "" + dr["Remarks"].ToString();
                    Details.Debit = decimal.Parse(dr["Debit"].ToString());
                    Details.Credit =decimal.Parse(dr["Credit"].ToString());
                    Details.CreditLimit = decimal.Parse(dr["CreditLimit"].ToString());
                    Details.IsCreditAllowed = bool.Parse(dr["IsCreditAllowed"].ToString());
                    Details.DateCreated = DateTime.Parse(dr["DateCreated"].ToString());
                    Details.Deleted = bool.Parse(dr["Deleted"].ToString());
                    Details.DepartmentID = Int16.Parse(dr["DepartmentID"].ToString());
                    Details.DepartmentName = "" + dr["DepartmentName"].ToString();
                    Details.PositionID = Int16.Parse(dr["PositionID"].ToString());
                    Details.PositionName = "" + dr["PositionName"].ToString();
                    Details.LastCheckInDate = DateTime.Parse(dr["LastCheckInDate"].ToString());

                    Details.isLock = bool.Parse(dr["isLock"].ToString());
                }

                // Sep 14, 2011 : Lemu - for reward points
                ContactReward clsContactReward = new ContactReward(base.Connection, base.Transaction);
                Details.RewardDetails = clsContactReward.Details(Details.ContactID);

                // Nov 2, 2011 : Lemu - for credit
                ContactCreditCardInfos clsContactCredit = new ContactCreditCardInfos(base.Connection, base.Transaction);
                Details.CreditDetails = clsContactCredit.Details(Details.ContactID);
                Details.CreditDetails.CreditLimit = Details.CreditLimit;

                if (!Details.IsCreditAllowed)
                    Details.CreditDetails.CreditActive = Details.IsCreditAllowed;
                else
                {
                    Details.CreditDetails.CreditActive = checkCreditActive(Details.CreditDetails.CreditCardStatus);
                }

                // Oct 12, 2013 : - get additional details
                Details.AdditionalDetails = new ContactAddOns(base.Connection, base.Transaction).Details(Details.ContactID);
            }
            catch (Exception ex) { throw base.ThrowException(ex); }
            return Details;
        }

		#endregion

		#region Streams

		public MySqlDataReader Search(string SearchKey, string SortField, SortOption SortOrder)
		{
			try
			{
				string SQL = SQLSelect() + "WHERE 1=1 AND deleted = '0' " +
					            "AND (ContactCode LIKE @SearchKey " +
					            "OR ContactName LIKE @SearchKey) " +
					            "ORDER BY " + SortField;

				if (SortOrder == SortOption.Ascending)
					SQL += " ASC";
				else
					SQL += " DESC";

				MySqlCommand cmd = new MySqlCommand();
				cmd.CommandType = System.Data.CommandType.Text;
				cmd.CommandText = SQL;
				
				MySqlParameter prmSearchKey = new MySqlParameter("@SearchKey",MySqlDbType.String);
				prmSearchKey.Value = "%" + SearchKey + "%";
				cmd.Parameters.Add(prmSearchKey);

				return base.ExecuteReader(cmd);
			}
			catch (Exception ex)
			{
				throw base.ThrowException(ex);
			}	
		}		
		public MySqlDataReader Customers(string SearchKey, Int32 Limit, string SortField, SortOption SortOrder)
		{
			try
			{
				string SQL = SQLSelect() + "WHERE 1=1 AND deleted = '0' " +
					            "AND (ContactCode LIKE @SearchKey " +
					            "OR ContactName LIKE @SearchKey) ";

				SQL += "AND (b.ContactGroupCategory = @CustomerCategory OR b.ContactGroupCategory = @BothCategory) ";
				SQL += "ORDER BY " + SortField;

				if (SortOrder == SortOption.Ascending)
					SQL += " ASC ";
				else
					SQL += " DESC ";

				if (Limit != 0)
					SQL += "LIMIT " + Limit;

				MySqlCommand cmd = new MySqlCommand();
				cmd.CommandType = System.Data.CommandType.Text;
				cmd.CommandText = SQL;
				
				MySqlParameter prmSearchKey = new MySqlParameter("@SearchKey",MySqlDbType.String);
				prmSearchKey.Value = SearchKey + "%";
				cmd.Parameters.Add(prmSearchKey);

				MySqlParameter prmCustomerCategory = new MySqlParameter("@CustomerCategory",MySqlDbType.Int16);
				prmCustomerCategory.Value = ContactGroupCategory.CUSTOMER.ToString("d");
				cmd.Parameters.Add(prmCustomerCategory);

				MySqlParameter prmBothCategory = new MySqlParameter("@BothCategory",MySqlDbType.Int16);
				prmBothCategory.Value = ContactGroupCategory.BOTH.ToString("d");
				cmd.Parameters.Add(prmBothCategory);

				return base.ExecuteReader(cmd);
			}
			catch (Exception ex)
			{
				throw base.ThrowException(ex);
			}	
		}		
		public MySqlDataReader Customers(string SearchKey, Int32 Limit, bool HasCreditOnly, string SortField, SortOption SortOrder)
		{
			try
			{
				string SQL = SQLSelect() + "WHERE 1=1 AND deleted = '0' " +
					            "AND (ContactCode LIKE @SearchKey " +
					            "OR ContactName LIKE @SearchKey) ";

                SQL += "AND (b.ContactGroupCategory = @CustomerCategory OR b.ContactGroupCategory = @BothCategory) ";
                SQL += "OR (ContactID IN (SELECT CustomerID FROM tblContactRewards WHERE RewardCardNo LIKE @SearchKey)) ";
				if (HasCreditOnly)
					SQL += "AND Credit > 0 ";

				SQL += "ORDER BY " + SortField;

				if (SortOrder == SortOption.Ascending)
					SQL += " ASC ";
				else
					SQL += " DESC ";

				if (Limit != 0)
					SQL += "LIMIT " + Limit;

				MySqlCommand cmd = new MySqlCommand();
				cmd.CommandType = System.Data.CommandType.Text;
				cmd.CommandText = SQL;
				
				MySqlParameter prmSearchKey = new MySqlParameter("@SearchKey",MySqlDbType.String);
				prmSearchKey.Value = "%" + SearchKey + "%";
				cmd.Parameters.Add(prmSearchKey);

				MySqlParameter prmCustomerCategory = new MySqlParameter("@CustomerCategory",MySqlDbType.Int16);
				prmCustomerCategory.Value = ContactGroupCategory.CUSTOMER.ToString("d");
				cmd.Parameters.Add(prmCustomerCategory);

				MySqlParameter prmBothCategory = new MySqlParameter("@BothCategory",MySqlDbType.Int16);
				prmBothCategory.Value = ContactGroupCategory.BOTH.ToString("d");
				cmd.Parameters.Add(prmBothCategory);

				return base.ExecuteReader(cmd);
			}
			catch (Exception ex)
			{
				throw base.ThrowException(ex);
			}	
		}		
		public MySqlDataReader Suppliers(string SearchKey, Int32 Limit, string SortField, SortOption SortOrder)
		{
			try
			{
				string SQL = SQLSelect() + "WHERE 1=1 AND deleted = '0' " +
						        "AND (ContactCode LIKE @SearchKey " +
						        "OR ContactName LIKE @SearchKey) ";

				SQL += "AND (b.ContactGroupCategory = @SupplierCategory OR b.ContactGroupCategory = @BothCategory) ";
				SQL += "ORDER BY " + SortField;

				if (SortOrder == SortOption.Ascending)
					SQL += " ASC ";
				else
					SQL += " DESC ";

				if (Limit != 0)
					SQL += "LIMIT " + Limit;

				MySqlCommand cmd = new MySqlCommand();
				cmd.CommandType = System.Data.CommandType.Text;
				cmd.CommandText = SQL;
				
				MySqlParameter prmSearchKey = new MySqlParameter("@SearchKey",MySqlDbType.String);
				prmSearchKey.Value = SearchKey + "%";
				cmd.Parameters.Add(prmSearchKey);

				MySqlParameter prmSupplierCategory = new MySqlParameter("@SupplierCategory",MySqlDbType.Int16);
				prmSupplierCategory.Value = ContactGroupCategory.SUPPLIER.ToString("d");
				cmd.Parameters.Add(prmSupplierCategory);

				MySqlParameter prmBothCategory = new MySqlParameter("@BothCategory",MySqlDbType.Int16);
				prmBothCategory.Value = ContactGroupCategory.BOTH.ToString("d");
				cmd.Parameters.Add(prmBothCategory);

				return base.ExecuteReader(cmd);
			}
			catch (Exception ex)
			{
				throw base.ThrowException(ex);
			}	
		}		
		public MySqlDataReader Search(string SearchKey, Int16 ContactGroupID, Int32 Limit, string SortField, SortOption SortOrder)
		{
			try
			{
				string SQL = SQLSelect() + "WHERE 1=1 AND deleted = '0' " +
					            "AND a.ContactGroupID = @ContactGroupID " +
					            "AND (ContactCode LIKE @SearchKey " +
					            "OR ContactName LIKE @SearchKey) " +
					            "ORDER BY " + SortField;

				if (SortOrder == SortOption.Ascending)
					SQL += " ASC ";
				else
					SQL += " DESC ";

				if (Limit != 0)
					SQL += "LIMIT " + Limit;

				

				MySqlCommand cmd = new MySqlCommand();
				cmd.CommandType = System.Data.CommandType.Text;
				cmd.CommandText = SQL;
				
				MySqlParameter prmContactGroupID = new MySqlParameter("@ContactGroupID",MySqlDbType.Int32);
				prmContactGroupID.Value = ContactGroupID;
				cmd.Parameters.Add(prmContactGroupID);

				MySqlParameter prmSearchKey = new MySqlParameter("@SearchKey",MySqlDbType.String);
				prmSearchKey.Value = SearchKey + "%";
				cmd.Parameters.Add(prmSearchKey);

				return base.ExecuteReader(cmd);
			}
			catch (Exception ex)
			{
				throw base.ThrowException(ex);
			}	
		}		
		public MySqlDataReader Search(string SearchKey, Int16 ContactGroupID, Int32 Limit, bool HasCreditOnly, string SortField, SortOption SortOrder)
		{
			try
			{
				string SQL = SQLSelect() + "WHERE 1=1 AND deleted = '0' " +
					            "AND a.ContactGroupID = @ContactGroupID " +
					            "AND (ContactCode LIKE @SearchKey " +
					            "OR ContactName LIKE @SearchKey) ";

				if (HasCreditOnly)
					SQL += "AND Credit > 0 ";

				if (SortOrder == SortOption.Ascending)
					SQL += "ORDER BY " + SortField + " ASC ";
				else
					SQL += "ORDER BY " + SortField + " DESC ";

				if (Limit != 0)
					SQL += "LIMIT " + Limit;

				MySqlCommand cmd = new MySqlCommand();
				cmd.CommandType = System.Data.CommandType.Text;
				cmd.CommandText = SQL;
				
				MySqlParameter prmContactGroupID = new MySqlParameter("@ContactGroupID",MySqlDbType.Int32);
				prmContactGroupID.Value = ContactGroupID;
				cmd.Parameters.Add(prmContactGroupID);

				MySqlParameter prmSearchKey = new MySqlParameter("@SearchKey",MySqlDbType.String);
				prmSearchKey.Value = SearchKey + "%";
				cmd.Parameters.Add(prmSearchKey);

				return base.ExecuteReader(cmd);
			}
			catch (Exception ex)
			{
				throw base.ThrowException(ex);
			}	
		}		
		public MySqlDataReader AdvanceSearch(string ContactCode, string ContactName, Int16 Deleted, Int32 ContactGroupID, bool HasCreditOnly, string SortField, SortOption SortOrder)
		{
			try
			{
				MySqlCommand cmd = new MySqlCommand();
				cmd.CommandType = System.Data.CommandType.Text;

				string SQL =  SQLSelect() + "WHERE 1=1 ";

                if (ContactCode != null && ContactCode != string.Empty && ContactCode != "" && ContactCode != "0" && ContactName != Constants.ALL)
				{
					SQL += " AND a.ContactCode = @ContactCode ";
                    cmd.Parameters.AddWithValue("@ContactCode", ContactCode);
				}
                if (ContactName != null && ContactName != string.Empty && ContactName != "" && ContactName != "0" && ContactName != Constants.ALL)
				{
					SQL += " AND a.ContactName = @ContactName ";
                    cmd.Parameters.AddWithValue("@ContactName", ContactName);
				}
				
				if (ContactGroupID != 0)
				{
					SQL += "AND a.ContactGroupID = @ContactGroupID ";
                    cmd.Parameters.AddWithValue("@ContactGroupID", ContactGroupID);
				}
				if (HasCreditOnly == true)
					SQL += "AND Credit > 0 ";

				if (Deleted != 2)
				{
					SQL += "AND a.Deleted = @Deleted ";

					MySqlParameter prmDeleted = new MySqlParameter("@Deleted",MySqlDbType.Byte);
					prmDeleted.Value = Deleted;
					cmd.Parameters.Add(prmDeleted);
				}

				SQL += "ORDER BY " + SortField;

				if (SortOrder == SortOption.Ascending)
					SQL += " ASC";
				else
					SQL += " DESC";
				
				cmd.CommandText = SQL;
				
				return base.ExecuteReader(cmd);
			}
			catch (Exception ex)
			{
				throw base.ThrowException(ex);
			}	
		}
        public System.Data.DataTable AdvanceSearchDataTable(ContactGroupCategory ContactGroupCategory, string ContactCode, string ContactName, Int16 Deleted, Int32 ContactGroupID, bool HasCreditOnly, string SortField, SortOption SortOrder)
        {
            try
            {
                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandType = System.Data.CommandType.Text;

                string SQL = SQLSelect() + "WHERE 1=1 ";

                if (ContactGroupCategory == ContactGroupCategory.CUSTOMER)
                {
                    SQL += "AND (b.ContactGroupCategory = @CustomerCategory OR b.ContactGroupCategory = @BothCategory) ";
                    //SQL += "OR (ContactID IN (SELECT CustomerID FROM tblContactRewards WHERE RewardCardNo LIKE @SearchKey)) ";
                    cmd.Parameters.AddWithValue("@CustomerCategory", ContactGroupCategory.CUSTOMER.ToString("d"));
                    cmd.Parameters.AddWithValue("@BothCategory", ContactGroupCategory.BOTH.ToString("d"));
                }

                if (ContactGroupCategory == ContactGroupCategory.SUPPLIER)
                {
                    SQL += "AND (b.ContactGroupCategory = @SupplierCategory OR b.ContactGroupCategory = @BothCategory) ";
                    cmd.Parameters.AddWithValue("@SupplierCategory", ContactGroupCategory.SUPPLIER.ToString("d"));
                    cmd.Parameters.AddWithValue("@BothCategory", ContactGroupCategory.BOTH.ToString("d"));
                }

                if (ContactCode != null && ContactCode != string.Empty && ContactCode != "" && ContactCode != "0")
                {
                    SQL += " AND a.ContactCode = @ContactCode ";
                    cmd.Parameters.AddWithValue("@ContactCode", ContactCode);
                }
                if (ContactName != null && ContactName != string.Empty && ContactName != "" && ContactName != "0")
                {
                    SQL += " AND a.ContactName = @ContactName ";
                    cmd.Parameters.AddWithValue("@ContactName", ContactName);
                }

                if (ContactGroupID != 0)
                {
                    SQL += "AND a.ContactGroupID = @ContactGroupID ";
                    cmd.Parameters.AddWithValue("@ContactGroupID", ContactGroupID);
                }
                if (HasCreditOnly == true)
                    SQL += "AND Credit > 0 ";

                if (Deleted != 2)
                {
                    SQL += "AND a.Deleted = @Deleted ";
                    cmd.Parameters.AddWithValue("@Deleted", Deleted);
                }

                SQL += "ORDER BY " + SortField;

                if (SortOrder == SortOption.Ascending)
                    SQL += " ASC";
                else
                    SQL += " DESC";

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
		
        public System.Data.DataTable CustomerAdvanceSearch(string ContactCode, string ContactName, string ContactGroupCode, bool HasCreditOnly, string SortField, SortOption SortOrder)
        {
            return ListAsDataTable(ContactGroupCategory.CUSTOMER, ContactCode: ContactCode, ContactName: ContactName, ContactGroupCode: ContactGroupCode, hasCreditOnly: HasCreditOnly, intDeleted: 0, SortField: SortField, SortOrder: SortOrder);
        }

		#endregion

		#region DataTables

        public DataTable Tables(ContactColumns clsContactColumns, long SequenceNoStart, System.Data.SqlClient.SortOrder SequenceSortOrder, ContactColumns SearchColumns, string SearchKey, Int32 limit, bool HasCreditOnly, string SortField = "ContactName", System.Data.SqlClient.SortOrder SortOrder = System.Data.SqlClient.SortOrder.Ascending)
        {
            try
            {
                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandType = System.Data.CommandType.Text;

                // enable this to include joining to table tblContactGroup
                clsContactColumns.ContactGroupName = true;

                string SQL = SQLSelect(clsContactColumns);

                SQL += "WHERE (tblContactGroup.ContactGroupCategory = @TableCategory) ";

                if (SequenceNoStart != Constants.ZERO)
                {
                    if (SequenceSortOrder == System.Data.SqlClient.SortOrder.Descending)
                        SQL += "AND tblContacts.ContactID < " + SequenceNoStart.ToString() + " ";
                    else
                        SQL += "AND tblContacts.ContactID > " + SequenceNoStart.ToString() + " ";
                }

                if (SearchKey != string.Empty)
                {
                    string SQLSearch = string.Empty;

                    if (SearchColumns.ContactCode)
                    { if (SQLSearch == string.Empty) SQLSearch += "ContactCode LIKE @SearchKey "; else SQLSearch += "OR ContactCode LIKE @SearchKey "; }

                    if (SearchColumns.ContactName)
                    { if (SQLSearch == string.Empty) SQLSearch += "ContactName LIKE @SearchKey "; else SQLSearch += "OR ContactName LIKE @SearchKey "; }

                    if (SearchColumns.RewardDetails)
                    { if (SQLSearch == string.Empty) SQLSearch += "RewardCardNo LIKE @SearchKey "; else SQLSearch += "OR RewardCardNo LIKE @SearchKey "; }

                    if (SQLSearch != string.Empty) SQL += "AND (" + SQLSearch + ") ";

                    cmd.Parameters.AddWithValue("SearchKey", "%" + SearchKey + "%");
                }

                SQL += "ORDER BY " + (!string.IsNullOrEmpty(SortField) ? SortField : "ContactName") + " ";
                SQL += SortOrder == System.Data.SqlClient.SortOrder.Ascending ? "ASC " : "DESC ";
                SQL += limit == 0 ? "" : "LIMIT " + limit.ToString() + " ";
                
                cmd.Parameters.AddWithValue("TableCategory", ContactGroupCategory.TABLES.ToString("d"));

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
        public DataTable Customers(ContactColumns clsContactColumns, long SequenceNoStart, System.Data.SqlClient.SortOrder SequenceSortOrder, ContactColumns SearchColumns, string SearchKey, Int32 Limit, bool HasCreditOnly, string SortField, System.Data.SqlClient.SortOrder SortOrder)
        {
            try
            {
                // enable this to include joining to table tblContactGroup
                clsContactColumns.ContactGroupName = true;

                string SQL = SQLSelect(clsContactColumns);

                SQL += "WHERE (tblContactGroup.ContactGroupCategory = @CustomerCategory OR tblContactGroup.ContactGroupCategory = @BothCategory) ";

                if (SequenceNoStart != Constants.ZERO)
                {
                    if (SequenceSortOrder == System.Data.SqlClient.SortOrder.Descending)
                        SQL += "AND tblContacts.ContactID < " + SequenceNoStart.ToString() + " ";
                    else
                        SQL += "AND tblContacts.ContactID > " + SequenceNoStart.ToString() + " ";
                }

                if (SearchKey != string.Empty)
                {
                    string SQLSearch = string.Empty;

                    if (SearchColumns.ContactCode)
                    { if (SQLSearch == string.Empty) SQLSearch += "ContactCode LIKE @SearchKey "; else SQLSearch += "OR ContactCode LIKE @SearchKey "; }

                    if (SearchColumns.ContactName)
                    { if (SQLSearch == string.Empty) SQLSearch += "ContactName LIKE @SearchKey "; else SQLSearch += "OR ContactName LIKE @SearchKey "; }

                    if (SearchColumns.RewardDetails)
                    { if (SQLSearch == string.Empty) SQLSearch += "RewardCardNo LIKE @SearchKey "; else SQLSearch += "OR RewardCardNo LIKE @SearchKey "; }

                    if (SQLSearch != string.Empty) SQL += "AND (" + SQLSearch + ") ";
                }

                if (SortField != string.Empty && SortField != null)
                {
                    SQL += "ORDER BY " + SortField + " ";

                    if (SortOrder != System.Data.SqlClient.SortOrder.Descending) SQL += "ASC ";
                    else SQL += "DESC ";
                }

                if (Limit != 0)
                    SQL += "LIMIT " + Limit + " ";

                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandText = SQL;

                MySqlParameter prmCustomerCategory = new MySqlParameter("@CustomerCategory",MySqlDbType.Int16);
                prmCustomerCategory.Value = ContactGroupCategory.CUSTOMER.ToString("d");
                cmd.Parameters.Add(prmCustomerCategory);

                MySqlParameter prmBothCategory = new MySqlParameter("@BothCategory",MySqlDbType.Int16);
                prmBothCategory.Value = ContactGroupCategory.BOTH.ToString("d");
                cmd.Parameters.Add(prmBothCategory);

                if (SearchKey != string.Empty)
                {
                    MySqlParameter prmSearchKey = new MySqlParameter("@SearchKey", MySqlDbType.String);
                    prmSearchKey.Value = "%" + SearchKey + "%";
                    cmd.Parameters.Add(prmSearchKey);
                }

                string strDataTableName = "tbl" + this.GetType().FullName.Split(new Char[] { '.' })[this.GetType().FullName.Split(new Char[] { '.' }).Length - 1]; System.Data.DataTable dt = new System.Data.DataTable(strDataTableName);
                base.MySqlDataAdapterFill(cmd, dt);

                return dt;
            }
            catch (Exception ex)
            {
                throw base.ThrowException(ex);
            }
        }
        public DataTable Suppliers(ContactColumns clsContactColumns, long SequenceNoStart, System.Data.SqlClient.SortOrder SequenceSortOrder, ContactColumns SearchColumns, string SearchKey, Int32 Limit, bool HasCreditOnly, string SortField, System.Data.SqlClient.SortOrder SortOrder)
        {
            try
            {
                // enable this to include joining to table tblContactGroup
                clsContactColumns.ContactGroupName = true;

                string SQL = SQLSelect(clsContactColumns);

                SQL += "WHERE (tblContactGroup.ContactGroupCategory = @SupplierCategory OR tblContactGroup.ContactGroupCategory = @BothCategory) ";

                if (SequenceNoStart != Constants.ZERO)
                {
                    if (SequenceSortOrder == System.Data.SqlClient.SortOrder.Descending)
                        SQL += "AND tblContacts.ContactID < " + SequenceNoStart.ToString() + " ";
                    else
                        SQL += "AND tblContacts.ContactID > " + SequenceNoStart.ToString() + " ";
                }

                if (SearchKey != string.Empty)
                {
                    string SQLSearch = string.Empty;

                    if (SearchColumns.ContactCode)
                    { if (SQLSearch == string.Empty) SQLSearch += "ContactCode LIKE @SearchKey "; else SQLSearch += "OR ContactCode LIKE @SearchKey "; }

                    if (SearchColumns.ContactName)
                    { if (SQLSearch == string.Empty) SQLSearch += "ContactName LIKE @SearchKey "; else SQLSearch += "OR ContactName LIKE @SearchKey "; }

                    if (SQLSearch != string.Empty) SQL += "AND (" + SQLSearch + ") ";
                }

                if (SortField != string.Empty && SortField != null)
                {
                    SQL += "ORDER BY " + SortField + " ";

                    if (SortOrder != System.Data.SqlClient.SortOrder.Descending) SQL += "ASC ";
                    else SQL += "DESC ";
                }

                if (Limit != 0)
                    SQL += "LIMIT " + Limit + " ";

                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandText = SQL;

                MySqlParameter prmSupplierCategory = new MySqlParameter("@SupplierCategory",MySqlDbType.Int16);
                prmSupplierCategory.Value = ContactGroupCategory.SUPPLIER.ToString("d");
                cmd.Parameters.Add(prmSupplierCategory);

                MySqlParameter prmBothCategory = new MySqlParameter("@BothCategory",MySqlDbType.Int16);
                prmBothCategory.Value = ContactGroupCategory.BOTH.ToString("d");
                cmd.Parameters.Add(prmBothCategory);

                string strDataTableName = "tbl" + this.GetType().FullName.Split(new Char[] { '.' })[this.GetType().FullName.Split(new Char[] { '.' }).Length - 1]; System.Data.DataTable dt = new System.Data.DataTable(strDataTableName);
                base.MySqlDataAdapterFill(cmd, dt);

                return dt;
            }
            catch (Exception ex)
            {
                throw base.ThrowException(ex);
            }
        }		
        public DataTable CustomersDataTable(string SearchKey, Int32 Limit = Constants.C_DEFAULT_LIMIT_OF_RECORD_TO_SHOW, bool HasCreditOnly= false, string SortField = "ContactName", SortOption SortOrder=SortOption.Ascending)
		{
            return ListAsDataTable(ContactGroupCategory.CUSTOMER, ContactCode: SearchKey, ContactName: SearchKey, hasCreditOnly: HasCreditOnly, intDeleted: 0, Limit: Limit, SortField: SortField, SortOrder: SortOrder);
		}
        public DataTable ListAsDataTable(string SortField, SortOption SortOrder)
        {
            return ListAsDataTable(intDeleted: 0, SortField: SortField, SortOrder: SortOrder);
        }
        public DataTable SearchAsDataTable(string SearchKey, string SortField, SortOption SortOrder)
        {
            return ListAsDataTable(ContactCode: SearchKey, ContactName: SearchKey, intDeleted: 0, SortField: SortField, SortOrder: SortOrder);
        }
        public DataTable SuppliersAsDataTable(string SearchKey = "", Int32 Limit = 0, string SortField = "ContactCode", SortOption SortOrder = SortOption.Ascending)
        {
            return ListAsDataTable(ContactGroupCategory.SUPPLIER, ContactCode: SearchKey, ContactName: SearchKey, intDeleted: 0, Limit: Limit, SortField: SortField, SortOrder: SortOrder);
        }
        public DataTable AgentsAsDataTable(string SearchKey = "", Int32 Limit = 0, string SortField = "ContactCode", SortOption SortOrder = SortOption.Ascending)
        {
            return ListAsDataTable(ContactGroupCategory.AGENT, ContactCode: SearchKey, ContactName: SearchKey, intDeleted: 0, Limit: Limit, SortField: SortField, SortOrder: SortOrder);
        }

        public DataTable CustomersWithRewards(ContactColumns clsContactColumns, long SequenceNoStart, System.Data.SqlClient.SortOrder SequenceSortOrder, Int32 limit, string CustomerCode_RewardCardNo, DateTime RewardExpiryDateFrom, DateTime RewardExpiryDateTo, Constants.DateSelectionString BirthDate = Constants.DateSelectionString.ALL, Int16 RewardCardStatus = -1, string SortField = "ContactCode", System.Data.SqlClient.SortOrder SortOrder = System.Data.SqlClient.SortOrder.Ascending)
        {
            try
            {
                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandType = System.Data.CommandType.Text;

                // enable this to include joining to table tblContactGroup
                clsContactColumns.ContactGroupName = true;
                clsContactColumns.RewardDetails = true;

                string SQL = SQLSelect(clsContactColumns);

                SQL += "WHERE (tblContactGroup.ContactGroupCategory = @CustomerCategory OR tblContactGroup.ContactGroupCategory = @BothCategory) ";
                cmd.Parameters.AddWithValue("@CustomerCategory", ContactGroupCategory.CUSTOMER.ToString("d"));
                cmd.Parameters.AddWithValue("@BothCategory", ContactGroupCategory.BOTH.ToString("d"));

                if (SequenceNoStart != Constants.ZERO)
                {
                    if (SequenceSortOrder == System.Data.SqlClient.SortOrder.Descending)
                        SQL += "AND tblContacts.ContactID < " + SequenceNoStart.ToString() + " ";
                    else
                        SQL += "AND tblContacts.ContactID > " + SequenceNoStart.ToString() + " ";
                }

                if (CustomerCode_RewardCardNo != string.Empty)
                {
                    SQL += "AND (RewardCardNo LIKE @CustomerCode_RewardCardNo OR ContactCode LIKE @CustomerCode_RewardCardNo OR ContactName LIKE @CustomerCode_RewardCardNo) ";
                    cmd.Parameters.AddWithValue("@CustomerCode_RewardCardNo", CustomerCode_RewardCardNo);
                }
                if (RewardCardStatus != -1)
                {
                    SQL += "AND RewardCardStatus = @RewardCardStatus ";
                    cmd.Parameters.AddWithValue("@RewardCardStatus", RewardCardStatus);
                }
                if (RewardExpiryDateFrom != DateTime.MinValue)
                {
                    SQL += "AND ExpiryDate >= @RewardExpiryDateFrom ";
                    cmd.Parameters.AddWithValue("@RewardExpiryDateFrom", RewardExpiryDateFrom.ToString("yyyy-MM-dd"));
                }
                if (RewardExpiryDateFrom != DateTime.MinValue)
                {
                    SQL += "AND ExpiryDate <= @RewardExpiryDateTo ";
                    cmd.Parameters.AddWithValue("@RewardExpiryDateTo", RewardExpiryDateTo.ToString("yyyy-MM-dd"));
                }
                if (BirthDate != Constants.DateSelectionString.ALL)
                {
                    
                    switch (BirthDate)
                    {
                        case Constants.DateSelectionString.Today:
                            SQL += "AND BirthDate = @BirthDate ";
                            cmd.Parameters.AddWithValue("@BirthDate", DateTime.Now.ToString("yyyy-MM-dd"));
                            break;
                        case Constants.DateSelectionString.CurrentMonth:
                            SQL += "AND MONTH(BirthDate) = @BirthDate ";
                            cmd.Parameters.AddWithValue("@BirthDate", DateTime.Now.Month);
                            break;
                        case Constants.DateSelectionString.PreviousMonth:
                            SQL += "AND MONTH(BirthDate) = @BirthDate ";
                            cmd.Parameters.AddWithValue("@BirthDate", DateTime.Now.AddMonths(-1).Month);
                            break;
                        case Constants.DateSelectionString.NextMonth:
                            SQL += "AND MONTH(BirthDate) = @BirthDate ";
                            cmd.Parameters.AddWithValue("@BirthDate", DateTime.Now.AddMonths(1).Month);
                            break;
                    }
                }

                SQL += "ORDER BY " + (!string.IsNullOrEmpty(SortField) ? SortField : "ContactCode") + " ";
                SQL += SortOrder == System.Data.SqlClient.SortOrder.Ascending ? "ASC " : "DESC ";
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

        public DataTable CustomersWithCredits(ContactColumns clsContactColumns, Int64 GuarantorID = 0, string LastNameFrom = "", string LastNameTo = "", string CustomerCode_CreditCardNo = "", DateTime? CreditCardExpiryDateFrom = null, DateTime? CreditCardExpiryDateTo = null, CreditCardStatus CreditCardStatus = CreditCardStatus.All, Int32 CreditCardTypeID = 0, bool CheckCustomersGuarantor = false, bool WithGuarantorOnly = false,  string SortField = "ContactCode", System.Data.SqlClient.SortOrder SortOrder = System.Data.SqlClient.SortOrder.Ascending, Int32 limit = 0)
        {
            try
            {
                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandType = System.Data.CommandType.Text;

                // enable this to include joining to table tblContactGroup
                clsContactColumns.ContactGroupName = true;
                clsContactColumns.CreditDetails = true;

                string SQL = SQLSelect(clsContactColumns);

                SQL += "WHERE (tblContactGroup.ContactGroupCategory = @CustomerCategory OR tblContactGroup.ContactGroupCategory = @BothCategory) ";
                cmd.Parameters.AddWithValue("@CustomerCategory", ContactGroupCategory.CUSTOMER.ToString("d"));
                cmd.Parameters.AddWithValue("@BothCategory", ContactGroupCategory.BOTH.ToString("d"));

                if (GuarantorID != Constants.ZERO)
                {
                    SQL += "AND GuarantorID = @GuarantorID ";
                    cmd.Parameters.AddWithValue("GuarantorID", GuarantorID);
                }

                if (CheckCustomersGuarantor)
                {
                    if (WithGuarantorOnly)
                        SQL += "AND GuarantorID <> 0 ";
                    else
                        SQL += "AND GuarantorID = 0 ";
                }
                
                if (!string.IsNullOrEmpty(CustomerCode_CreditCardNo))
                {
                    SQL += "AND (CreditCardNo LIKE @CustomerCode_CreditCardNo OR ContactCode LIKE @CustomerCode_CreditCardNo OR ContactName LIKE @CustomerCode_CreditCardNo) ";
                    cmd.Parameters.AddWithValue("@CustomerCode_CreditCardNo", CustomerCode_CreditCardNo + '%');
                }
                
                if (CreditCardExpiryDateFrom.GetValueOrDefault(DateTime.MinValue) != DateTime.MinValue)
                {
                    SQL += "AND ExpiryDate >= @CreditCardExpiryDateFrom ";
                    cmd.Parameters.AddWithValue("@CreditCardExpiryDateFrom", CreditCardExpiryDateFrom.GetValueOrDefault(DateTime.MinValue).ToString("yyyy-MM-dd"));
                }
                if (CreditCardExpiryDateFrom.GetValueOrDefault(DateTime.MinValue) != DateTime.MinValue)
                {
                    SQL += "AND ExpiryDate <= @CreditCardExpiryDateTo ";
                    cmd.Parameters.AddWithValue("@CreditCardExpiryDateTo", CreditCardExpiryDateTo.GetValueOrDefault(DateTime.MinValue).ToString("yyyy-MM-dd"));
                }
                if (CreditCardStatus != RetailPlus.CreditCardStatus.All)
                {
                    SQL += "AND CreditCardStatus = @CreditCardStatus ";
                    cmd.Parameters.AddWithValue("@CreditCardStatus", CreditCardStatus.ToString("d"));
                }
                if (CreditCardTypeID != 0)
                {
                    SQL += "AND CreditCardTypeID = @CreditCardTypeID ";
                    cmd.Parameters.AddWithValue("CreditCardTypeID", CreditCardTypeID);
                }

                if (!string.IsNullOrEmpty(LastNameFrom) && !string.IsNullOrEmpty(LastNameTo))
                {
                    SQL += "AND ContactID IN (SELECT ContactID FROM tblContactAddOn WHERE LastName >= @LastNameFrom AND LastName <= @LastNameTo) ";
                    cmd.Parameters.AddWithValue("LastNameFrom", LastNameFrom);
                    cmd.Parameters.AddWithValue("LastNameTo", LastNameTo);
                }
                else if (!string.IsNullOrEmpty(LastNameFrom))
                {
                    SQL += "AND ContactID IN (SELECT ContactID FROM tblContactAddOn WHERE LastName LIKE @LastNameFrom) ";
                    cmd.Parameters.AddWithValue("LastNameFrom", LastNameFrom + '%');
                }
                else if (!string.IsNullOrEmpty(LastNameTo))
                {
                    SQL += "AND ContactID IN (SELECT ContactID FROM tblContactAddOn WHERE LastName LIKE @LastNameTo) ";
                    cmd.Parameters.AddWithValue("LastNameTo", LastNameTo + '%');
                }

                SQL += "ORDER BY " + (!string.IsNullOrEmpty(SortField) ? SortField : "ContactCode") + " ";
                SQL += SortOrder == System.Data.SqlClient.SortOrder.Ascending ? "ASC " : "DESC ";
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

        public DataTable Guarantors(ContactColumns clsContactColumns, string LastNameFrom = "", string LastNameTo = "", string CustomerCode_CreditCardNo = "", DateTime? CreditCardExpiryDateFrom = null, DateTime? CreditCardExpiryDateTo = null, CreditCardStatus CreditCardStatus = CreditCardStatus.All, Int32 CreditCardTypeID = 0, string SortField = "ContactCode", System.Data.SqlClient.SortOrder SortOrder = System.Data.SqlClient.SortOrder.Ascending, Int32 limit = 0)
        {
            try
            {
                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandType = System.Data.CommandType.Text;

                // enable this to include joining to table tblContactGroup
                clsContactColumns.ContactGroupName = true;
                clsContactColumns.CreditDetails = true;

                string SQL = SQLSelect(clsContactColumns);

                if (CreditCardTypeID == 0)
                {
                    SQL += "WHERE tblContacts.ContactID = tblContactCreditCardInfo.GuarantorID ";
                }
                else
                {
                    SQL += "WHERE tblContacts.ContactID = tblContactCreditCardInfo.GuarantorID AND tblContactCreditCardInfo.CreditCardTypeID = @CreditCardTypeID ";
                    cmd.Parameters.AddWithValue("CreditCardTypeID", CreditCardTypeID);
                }
                if (!string.IsNullOrEmpty(CustomerCode_CreditCardNo))
                {
                    SQL += "AND (CreditCardNo LIKE @CustomerCode_CreditCardNo OR ContactCode LIKE @CustomerCode_CreditCardNo OR ContactName LIKE @CustomerCode_CreditCardNo) ";
                    cmd.Parameters.AddWithValue("@CustomerCode_CreditCardNo", CustomerCode_CreditCardNo + '%');
                }

                if (CreditCardExpiryDateFrom.GetValueOrDefault(DateTime.MinValue) != DateTime.MinValue)
                {
                    SQL += "AND ExpiryDate >= @CreditCardExpiryDateFrom ";
                    cmd.Parameters.AddWithValue("@CreditCardExpiryDateFrom", CreditCardExpiryDateFrom.GetValueOrDefault(DateTime.MinValue).ToString("yyyy-MM-dd"));
                }
                if (CreditCardExpiryDateFrom.GetValueOrDefault(DateTime.MinValue) != DateTime.MinValue)
                {
                    SQL += "AND ExpiryDate <= @CreditCardExpiryDateTo ";
                    cmd.Parameters.AddWithValue("@CreditCardExpiryDateTo", CreditCardExpiryDateTo.GetValueOrDefault(DateTime.MinValue).ToString("yyyy-MM-dd"));
                }
                if (CreditCardStatus != RetailPlus.CreditCardStatus.All)
                {
                    SQL += "AND CreditCardStatus = @CreditCardStatus ";
                    cmd.Parameters.AddWithValue("@CreditCardStatus", CreditCardStatus.ToString("d"));
                }

                if (!string.IsNullOrEmpty(LastNameFrom) && !string.IsNullOrEmpty(LastNameTo))
                {
                    SQL += "AND ContactID IN (SELECT ContactID FROM tblContactAddOn WHERE LastName >= @LastNameFrom AND LastName <= @LastNameTo) ";
                    cmd.Parameters.AddWithValue("LastNameFrom", LastNameFrom);
                    cmd.Parameters.AddWithValue("LastNameTo", LastNameTo);
                }
                else if (!string.IsNullOrEmpty(LastNameFrom))
                {
                    SQL += "AND ContactID IN (SELECT ContactID FROM tblContactAddOn WHERE LastName LIKE @LastNameFrom) ";
                    cmd.Parameters.AddWithValue("LastNameFrom", LastNameFrom + '%');
                }
                else if (!string.IsNullOrEmpty(LastNameTo))
                {
                    SQL += "AND ContactID IN (SELECT ContactID FROM tblContactAddOn WHERE LastName LIKE @LastNameTo) ";
                    cmd.Parameters.AddWithValue("LastNameTo", LastNameTo + '%');
                }

                SQL += "ORDER BY " + (!string.IsNullOrEmpty(SortField) ? SortField : "ContactCode") + " ";
                SQL += SortOrder == System.Data.SqlClient.SortOrder.Ascending ? "ASC " : "DESC ";
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

        public DataTable CustomersForBilling(Int16 CreditCardTypeID, long ContactID = 0, string ContactCode = "", string ContactName = "", string ContactGroupCode = "", string CreditCardNo = "", string Name = "", int Deleted = -1, int Limit = 0, string SortField = "", SortOption SortOrder = SortOption.Ascending)
        {
            try
            {
                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandType = System.Data.CommandType.Text;

                string SQL = "CALL procContactSelectForBilling(@CreditCardTypeID, @ContactID, @ContactCode, @ContactName, @ContactGroupCode, @CreditCardNo, @Name, @Deleted, @Limit, @SortField, @SortOrder)";

                cmd.Parameters.AddWithValue("@CreditCardTypeID", CreditCardTypeID);
                cmd.Parameters.AddWithValue("@ContactID", ContactID);
                cmd.Parameters.AddWithValue("@ContactCode", ContactCode);
                cmd.Parameters.AddWithValue("@ContactName", ContactName);
                cmd.Parameters.AddWithValue("@ContactGroupCode", ContactGroupCode);
                cmd.Parameters.AddWithValue("@CreditCardNo", CreditCardNo);
                cmd.Parameters.AddWithValue("@Name", Name);
                cmd.Parameters.AddWithValue("@Deleted", Deleted);
                cmd.Parameters.AddWithValue("@Limit", Limit);
                cmd.Parameters.AddWithValue("@SortField", SortField);
                cmd.Parameters.AddWithValue("@SortOrder", SortOrder == SortOption.Ascending ? "ASC" : "DESC");

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

        public DataTable ListAsDataTable(ContactGroupCategory groupCategory = ContactGroupCategory.BOTH, long ContactID = 0, string ContactCode = "", string ContactName = "", string ContactGroupCode = "", string RewardCardNo = "", string Name = "", bool hasCreditOnly = false, int intDeleted = -1, int Limit = 0, string SortField = "", SortOption SortOrder = SortOption.Ascending, string BirthDateFrom = Constants.C_DATE_MIN_VALUE_STRING, string BirthDateTo = Constants.C_DATE_MIN_VALUE_STRING, string AnniversaryDateFrom = Constants.C_DATE_MIN_VALUE_STRING, string AnniversaryDateTo = Constants.C_DATE_MIN_VALUE_STRING, int BirthMonth = 0, int AnniversaryMonth = 0)
        {
            try
            {
                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandType = System.Data.CommandType.Text;

                string SQL = "CALL procContactSelect(@ContactGroupCategory, @ContactID, @ContactCode, @ContactName, @ContactGroupCode, @RewardCardNo, @Name, @BirthMonth, @AnniversaryMonth, @BirthDateFrom, @BirthDateTo, @AnniversaryDateFrom, @AnniversaryDateTo, @hasCreditOnly, @intDeleted, @lngLimit, @SortField, @SortOrder)";

                cmd.Parameters.AddWithValue("@ContactGroupCategory", groupCategory.ToString("d"));
                cmd.Parameters.AddWithValue("@ContactID", ContactID);
                cmd.Parameters.AddWithValue("@ContactCode", ContactCode);
                cmd.Parameters.AddWithValue("@ContactName", ContactName);
                cmd.Parameters.AddWithValue("@ContactGroupCode", ContactGroupCode);
                cmd.Parameters.AddWithValue("@RewardCardNo", RewardCardNo);
                cmd.Parameters.AddWithValue("@Name", Name);
                cmd.Parameters.AddWithValue("@BirthMonth", BirthMonth);
                cmd.Parameters.AddWithValue("@AnniversaryMonth", AnniversaryMonth);
                cmd.Parameters.AddWithValue("@BirthDateFrom", BirthDateFrom);
                cmd.Parameters.AddWithValue("@BirthDateTo", BirthDateTo);
                cmd.Parameters.AddWithValue("@AnniversaryDateFrom", AnniversaryDateFrom);
                cmd.Parameters.AddWithValue("@AnniversaryDateTo", AnniversaryDateTo);
                cmd.Parameters.AddWithValue("@hasCreditOnly", hasCreditOnly);
                cmd.Parameters.AddWithValue("@intDeleted", intDeleted);
                cmd.Parameters.AddWithValue("@lngLimit", Limit);
                cmd.Parameters.AddWithValue("@SortField", SortField);
                cmd.Parameters.AddWithValue("@SortOrder", SortOrder == SortOption.Ascending ? "ASC" : "DESC");

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

        public System.Data.DataTable CreditPaymentCashDetailedAsDataTable(CreditPaymentCashDetails SearchKeys, string SortField = "CPC.CreatedOn", SortOption SortOrder = SortOption.Ascending, Int32 limit = 0)
        {
            try
            {
                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandType = System.Data.CommandType.Text;

                string SQL = "CALL procContactCreditPaymentSelectDetailed(@BranchID, @TerminalNo, @ContactID, @CreditType, @CreditCardTypeID, @PaymentDateFrom, @PaymentDateTo, " +
                                                                  "@CreditorLastNameFrom, @CreditorLastnameTo, @GuaLastNameFrom, @GuaLastNameTo, @SortField, @SortOption, @limit);";

                cmd.Parameters.AddWithValue("BranchID", SearchKeys.BranchDetails.BranchID);
                cmd.Parameters.AddWithValue("TerminalNo", SearchKeys.TerminalNo);
                cmd.Parameters.AddWithValue("ContactID", SearchKeys.ContactID);
                cmd.Parameters.AddWithValue("CreditType", SearchKeys.CreditType.ToString("d"));
                cmd.Parameters.AddWithValue("CreditCardTypeID", SearchKeys.CreditCardTypeID);
                cmd.Parameters.AddWithValue("PaymentDateFrom", SearchKeys.PaymentDateFrom);
                cmd.Parameters.AddWithValue("PaymentDateTo", SearchKeys.PaymentDateTo);
                cmd.Parameters.AddWithValue("CreditorLastNameFrom", SearchKeys.CreditorLastnameFrom);
                cmd.Parameters.AddWithValue("CreditorLastNameTo", SearchKeys.CreditorLastnameTo);
                cmd.Parameters.AddWithValue("GuaLastNameFrom", SearchKeys.GuarantorLastnameFrom);
                cmd.Parameters.AddWithValue("GuaLastNameTo", SearchKeys.GuarantorLastnameTo);
                cmd.Parameters.AddWithValue("SortField", SortField);
                cmd.Parameters.AddWithValue("SortOption", SortOrder == SortOption.Ascending ? "ASC" : "DESC");
                cmd.Parameters.AddWithValue("limit", limit);

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

        public System.Data.DataTable CreditPaymentCashAsDataTable(CreditPaymentCashDetails SearchKeys, string SortField = "trx.CreatedOn", SortOption SortOrder = SortOption.Ascending, Int32 limit = 0)
        {
            try
            {
                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandType = System.Data.CommandType.Text;

                string SQL = "CALL procContactCreditPaymentSelect(@BranchID, @TerminalNo, @ContactID, @CreditType, @CreditCardTypeID, @PaymentDateFrom, @PaymentDateTo, " +
                                                                  "@CreditorLastNameFrom, @CreditorLastnameTo, @GuaLastNameFrom, @GuaLastNameTo, @SortField, @SortOption, @limit);";

                cmd.Parameters.AddWithValue("BranchID", SearchKeys.BranchDetails.BranchID);
                cmd.Parameters.AddWithValue("TerminalNo", SearchKeys.TerminalNo);
                cmd.Parameters.AddWithValue("ContactID", SearchKeys.ContactID);
                cmd.Parameters.AddWithValue("CreditType", SearchKeys.CreditType.ToString("d"));
                cmd.Parameters.AddWithValue("CreditCardTypeID", SearchKeys.CreditCardTypeID);
                cmd.Parameters.AddWithValue("PaymentDateFrom", SearchKeys.PaymentDateFrom);
                cmd.Parameters.AddWithValue("PaymentDateTo", SearchKeys.PaymentDateTo);
                cmd.Parameters.AddWithValue("CreditorLastNameFrom", SearchKeys.CreditorLastnameFrom);
                cmd.Parameters.AddWithValue("CreditorLastNameTo", SearchKeys.CreditorLastnameTo);
                cmd.Parameters.AddWithValue("GuaLastNameFrom", SearchKeys.GuarantorLastnameFrom);
                cmd.Parameters.AddWithValue("GuaLastNameTo", SearchKeys.GuarantorLastnameTo);
                cmd.Parameters.AddWithValue("SortField", SortField);
                cmd.Parameters.AddWithValue("SortOption", SortOrder == SortOption.Ascending ? "ASC" : "DESC");
                cmd.Parameters.AddWithValue("limit", limit);

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

        public System.Data.DataTable CreditPurchasesAsDataTable(CreditPaymentDetails SearchKeys, string SortField = "CP.CreditDate", SortOption SortOrder = SortOption.Ascending, Int32 limit = 0)
        {
            try
            {
                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandType = System.Data.CommandType.Text;

                string SQL = "CALL procContactCreditPurchaseSelect(@BranchID, @TerminalNo, @ContactID, @CreditType, @CreditCardTypeID, @PurchaseDateFrom, @PurchaseDateTo, " +
                                                                  "@CreditorLastNameFrom, @CreditorLastnameTo, @GuaLastNameFrom, @GuaLastNameTo, @SortField, @SortOption, @limit);";

                cmd.Parameters.AddWithValue("BranchID", SearchKeys.BranchDetails.BranchID);
                cmd.Parameters.AddWithValue("TerminalNo", SearchKeys.TerminalNo);
                cmd.Parameters.AddWithValue("ContactID", SearchKeys.CustomerDetails.ContactID);
                cmd.Parameters.AddWithValue("CreditType", SearchKeys.CreditType.ToString("d"));
                cmd.Parameters.AddWithValue("CreditCardTypeID", SearchKeys.CreditCardTypeID);
                cmd.Parameters.AddWithValue("PurchaseDateFrom", SearchKeys.PurchaseDateFrom);
                cmd.Parameters.AddWithValue("PurchaseDateTo", SearchKeys.PurchaseDateTo);
                cmd.Parameters.AddWithValue("CreditorLastNameFrom", SearchKeys.CreditorLastnameFrom);
                cmd.Parameters.AddWithValue("CreditorLastNameTo", SearchKeys.CreditorLastnameTo);
                cmd.Parameters.AddWithValue("GuaLastNameFrom", SearchKeys.GuarantorLastnameFrom);
                cmd.Parameters.AddWithValue("GuaLastNameTo", SearchKeys.GuarantorLastnameTo);
                cmd.Parameters.AddWithValue("SortField", SortField);
                cmd.Parameters.AddWithValue("SortOption", SortOrder == SortOption.Ascending ? "ASC" : "DESC");
                cmd.Parameters.AddWithValue("limit", limit);

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

		#endregion

		#region Public Modifiers

		public void AddCredit(long ContactID, decimal Amount)
		{
			try 
			{
                string SQL = "CALL procContactAddCredit(@ContactID, @Credit);";
	 			
				MySqlCommand cmd = new MySqlCommand();
				cmd.CommandType = System.Data.CommandType.Text;
				cmd.CommandText = SQL;

                cmd.Parameters.AddWithValue("@ContactID", ContactID);
                cmd.Parameters.AddWithValue("@Credit", Amount);

				base.ExecuteNonQuery(cmd);
			}

			catch (Exception ex)
			{
				throw base.ThrowException(ex);
			}	
		}

		public void AddDebit(long ContactID, decimal Amount)
		{
			try 
			{
                string SQL = "CALL procContactAddDebit(@ContactID, @Debit);";

                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandText = SQL;

                cmd.Parameters.AddWithValue("@ContactID", ContactID);
                cmd.Parameters.AddWithValue("@Debit", Amount);

				base.ExecuteNonQuery(cmd);
			}

			catch (Exception ex)
			{
				throw base.ThrowException(ex);
			}	
		}
        public void SubtractCredit(long ContactID, decimal Amount, bool ActivateSuspendedAccount = true)
		{
			try 
			{
                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandType = System.Data.CommandType.Text;

                string SQL = "CALL procContactSubtractCredit(@ContactID, @Credit, @ActivateSuspendedAccount);";
	 			
                cmd.Parameters.AddWithValue("@ContactID", ContactID);
                cmd.Parameters.AddWithValue("@Credit", Amount);
                cmd.Parameters.AddWithValue("@ActivateSuspendedAccount", ActivateSuspendedAccount);

                cmd.CommandText = SQL;
				base.ExecuteNonQuery(cmd);
			}
			catch (Exception ex)
			{
				throw base.ThrowException(ex);
			}	
		}

        public void SubtractDebit(long ContactID, decimal Amount)
        {
            try
            {
                string SQL = "CALL procContactSubtractDebit(@ContactID, @Debit);";

                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandText = SQL;

                cmd.Parameters.AddWithValue("@ContactID", ContactID);
                cmd.Parameters.AddWithValue("@Debit", Amount);

                base.ExecuteNonQuery(cmd);
            }

            catch (Exception ex)
            {
                throw base.ThrowException(ex);
            }
        }

        public void UpdateCreditCardType(Int64 GuarantorID_ContactID, Int16 CreditCardTypeID, string UpdatedBy)
        {
            try
            {
                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandType = System.Data.CommandType.Text;

                string SQL = "CALL procContactChangeCreditCardType(@ContactID, @CreditCardTypeID, @UpdatedBy);";

                cmd.Parameters.AddWithValue("ContactID", GuarantorID_ContactID);
                cmd.Parameters.AddWithValue("CreditCardTypeID", CreditCardTypeID);
                cmd.Parameters.AddWithValue("UpdatedBy", UpdatedBy);

                cmd.CommandText = SQL;
                base.ExecuteNonQuery(cmd);
            }
            catch (Exception ex)
            {
                throw base.ThrowException(ex);
            }
        }

        public void UpdateGuarantor(Int64 ContactID, Int64 GuarantorID, string UpdatedBy)
        {
            try
            {
                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandType = System.Data.CommandType.Text;

                string SQL = "CALL procContactChangeGuarantor(@ContactID, @GuarantorID, @UpdatedBy);";

                cmd.Parameters.AddWithValue("ContactID", ContactID);
                cmd.Parameters.AddWithValue("GuarantorID", GuarantorID);
                cmd.Parameters.AddWithValue("UpdatedBy", UpdatedBy);

                cmd.CommandText = SQL;
                base.ExecuteNonQuery(cmd);
            }
            catch (Exception ex)
            {
                throw base.ThrowException(ex);
            }
        }


        /// <summary>
        /// Update remarks separately.
        /// Called after changing the credit limit/status/ in {CTRL + F12}
        /// </summary>
        /// <param name="ContactID"></param>
        /// <param name="GuarantorID"></param>
        /// <param name="UpdatedBy"></param>
        public void UpdateRemarks(Int64 ContactID, string Remarks, string UpdatedBy)
        {
            try
            {
                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandType = System.Data.CommandType.Text;

                string SQL = "CALL procContactUpdateRemarks(@ContactID, @Remarks, @UpdatedBy);";

                cmd.Parameters.AddWithValue("ContactID", ContactID);
                cmd.Parameters.AddWithValue("Remarks", Remarks);
                cmd.Parameters.AddWithValue("UpdatedBy", UpdatedBy);

                cmd.CommandText = SQL;
                base.ExecuteNonQuery(cmd);
            }
            catch (Exception ex)
            {
                throw base.ThrowException(ex);
            }
        }

        public static bool checkCreditActive(CreditCardStatus CreditCardStatus)
        {
            bool boRetValue = false;

            switch (CreditCardStatus)
            {
                case CreditCardStatus.New:
                case CreditCardStatus.Replaced_Lost:
                case CreditCardStatus.Replaced_Expired:
                case CreditCardStatus.ReNew:
                case CreditCardStatus.Reactivated_Lost:
                case CreditCardStatus.ManualActivated:
                case CreditCardStatus.SystemActivated:
                    boRetValue = true;
                    break;
                case CreditCardStatus.Lost:
                case CreditCardStatus.Expired:
                case CreditCardStatus.ManualDeactivated:
                case CreditCardStatus.SystemDeactivated:
                case CreditCardStatus.Suspended:
                case CreditCardStatus.All:
                    boRetValue = false;
                    break;
                default:
                    boRetValue = false;
                    break;
            }
            return boRetValue;
        }

        public static bool checkRewardActive(RewardCardStatus RewardCardStatus)
        {
            bool boRetValue = false;

            switch (RewardCardStatus)
            {
                case RewardCardStatus.New:
                case RewardCardStatus.Replaced_Lost:
                case RewardCardStatus.Replaced_Expired:
                case RewardCardStatus.ReNew:
                case RewardCardStatus.Reactivated_Lost:
                case RewardCardStatus.ManualActivated:
                case RewardCardStatus.SystemActivated:
                    boRetValue = true;
                    break;
                case RewardCardStatus.Lost:
                case RewardCardStatus.Expired:
                case RewardCardStatus.ManualDeactivated:
                case RewardCardStatus.SystemDeactivated:
                case RewardCardStatus.Suspended:
                case RewardCardStatus.All:
                    boRetValue = false;
                    break;
                default:
                    boRetValue = false;
                    break;
            }
            return boRetValue;
        }
        
        #endregion
	}
}
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
		public long ContactID;
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
		public Int16 IsCreditAllowed;
		public DateTime DateCreated;
		public int Deleted;
        public int DepartmentID;
        public string DepartmentName;
        public int PositionID;
        public string PositionName;

        // Sep 14, 2011 : Lemu for reward points details
        public ContactRewardDetails RewardDetails;

        // Nov 2, 2011 : Lemu for credit details
        public ContactCreditDetails CreditDetails;
	}

    public struct ContactColumns
    {
        public bool ContactID;
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
        public bool RewardDetails;
        public bool CreditDetails;
    }

    public struct ContactColumnNames
    {
        public const string ContactID = "ContactID";
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


		#region Constructors and Destructors

		public Contacts()
            : base(null, null)
        {
        }

        public Contacts(MySqlConnection Connection, MySqlTransaction Transaction) 
            : base(Connection, Transaction)
		{

		}

		#endregion

		#region Insert and Update

		public long Insert(ContactDetails Details)
		{
			try  
			{
				string SQL="INSERT INTO tblContacts (" +
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

				return iID;
			}

			catch (Exception ex)
			{
				throw ex;
			}	
		}
		public void Update(ContactDetails Details)
		{
			try 
			{
				string SQL=	"UPDATE tblContacts SET " + 
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

                MySqlCommand cmd = new MySqlCommand();

				cmd.CommandType = System.Data.CommandType.Text;
				cmd.CommandText = SQL;
				
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

				base.ExecuteNonQuery(cmd);
			}

			catch (Exception ex)
			{
				throw ex;
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
				throw ex;
			}	
		}

		#endregion

		#region Delete

		public bool Delete(string IDs)
		{
			try 
			{
				string SQL=	"DELETE FROM tblContacts WHERE ContactID IN (" + IDs + ");";
				  
				MySqlCommand cmd = new MySqlCommand();
				cmd.CommandType = System.Data.CommandType.Text;
				cmd.CommandText = SQL;

				base.ExecuteNonQuery(cmd);

				return true;
			}

			catch (Exception ex)
			{
				throw ex;
			}	
		}


		#endregion

        private string SQLSelect()
        {
            string stSQL = "SELECT " +
                                "a.ContactID, " +
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
                                "PositionName " +
                            "FROM tblContacts a " +
                            "INNER JOIN tblContactGroup b ON a.ContactGroupID = b.ContactGroupID " +
                            "INNER JOIN tblDepartments c ON a.DepartmentID = c.DepartmentID " +
                            "INNER JOIN tblPositions d ON a.PositionID = d.PositionID ";

            return stSQL;
        }
        private string SQLSelect(ContactColumns clsContactColumns)
        {
            string stSQL = "SELECT ";
                                
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

            return stSQL;
        }

		#region Details

		public ContactDetails Details(long ContactID)
		{
			try
			{
				string SQL=	SQLSelect() + "WHERE a.ContactID = @ContactID;";
				  
				MySqlCommand cmd = new MySqlCommand();
				cmd.CommandType = System.Data.CommandType.Text;
				cmd.CommandText = SQL;

                cmd.Parameters.AddWithValue("@ContactID", ContactID);
				
				MySqlDataReader myReader = base.ExecuteReader(cmd, System.Data.CommandBehavior.SingleResult);

                ContactDetails clsContactDetails = Details(myReader);

                return clsContactDetails;
			}

			catch (Exception ex)
			{
				throw ex;
			}	
		}
		public ContactDetails Details(string ContactCode)
		{
			try
			{
		        string SQL=	SQLSelect() + "WHERE ContactCode = @ContactCode;";
				  
				MySqlCommand cmd = new MySqlCommand();
				cmd.CommandType = System.Data.CommandType.Text;
				cmd.CommandText = SQL;

                cmd.Parameters.AddWithValue("@ContactCode", ContactCode);

				MySqlDataReader myReader = base.ExecuteReader(cmd, System.Data.CommandBehavior.SingleResult);

                ContactDetails clsContactDetails = Details(myReader);

                return clsContactDetails;
			}

			catch (Exception ex)
			{
				throw ex;
			}	
		}

        public ContactDetails DetailsByRewardCardNo(string RewardCardNo)
        {
            try
            {
                string SQL = SQLSelect() + "WHERE ContactID = (SELECT IFNULL(CustomerID,0) FROM tblContactRewards WHERE RewardActive = 1 AND RewardCardNo = @RewardCardNo LIMIT 1);";

                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandText = SQL;

                cmd.Parameters.AddWithValue("@RewardCardNo", RewardCardNo);

                MySqlDataReader myReader = base.ExecuteReader(cmd, System.Data.CommandBehavior.SingleResult);

                ContactDetails clsContactDetails = Details(myReader);

                return clsContactDetails;
            }

            catch (Exception ex)
            {
                throw ex;
            }
        }

        public ContactDetails DetailsByCreditCardNo(string CreditCardNo)
        {
            try
            {
                string SQL = SQLSelect() + "WHERE IsCreditAllowed = 1 AND ContactID = (SELECT IFNULL(CustomerID,0) FROM tblContactCreditCardInfo WHERE CreditCardNo = @CreditCardNo LIMIT 1);";

                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandText = SQL;

                cmd.Parameters.AddWithValue("@CreditCardNo", CreditCardNo);

                MySqlDataReader myReader = base.ExecuteReader(cmd, System.Data.CommandBehavior.SingleResult);

                ContactDetails clsContactDetails = Details(myReader);

                return clsContactDetails;
            }

            catch (Exception ex)
            {
                throw ex;
            }
        }

        private ContactDetails Details(MySqlDataReader myReader)
        {
            ContactDetails Details = new ContactDetails();

            try 
            {
                while (myReader.Read())
                {
                    Details.ContactID = myReader.GetInt64("ContactID");
                    Details.ContactCode = "" + myReader["ContactCode"].ToString();
                    Details.ContactName = "" + myReader["ContactName"].ToString();
                    Details.ContactGroupID = myReader.GetInt32("ContactGroupID");
                    Details.ContactGroupName = "" + myReader["ContactGroupName"].ToString();
                    Details.ModeOfTerms = (ModeOfTerms)Enum.Parse(typeof(ModeOfTerms), myReader.GetString("ModeOfTerms"));
                    Details.Terms = myReader.GetInt32("Terms");
                    Details.Address = "" + myReader["Address"].ToString();
                    Details.BusinessName = "" + myReader["BusinessName"].ToString();
                    Details.TelephoneNo = "" + myReader["TelephoneNo"].ToString();
                    Details.Remarks = "" + myReader["Remarks"].ToString();
                    Details.Debit = myReader.GetDecimal("Debit");
                    Details.Credit = myReader.GetDecimal("Credit");
                    Details.CreditLimit = myReader.GetDecimal("CreditLimit");
                    Details.IsCreditAllowed = myReader.GetInt16("IsCreditAllowed");
                    Details.DateCreated = myReader.GetDateTime("DateCreated");
                    Details.Deleted = myReader.GetByte("Deleted");
                    Details.DepartmentID = myReader.GetInt16("DepartmentID");
                    Details.DepartmentName = "" + myReader["DepartmentName"].ToString();
                    Details.PositionID = myReader.GetInt16("PositionID");
                    Details.PositionName = "" + myReader["PositionName"].ToString();
                }
                myReader.Close();

                // Sep 14, 2011 : Lemu - for reward points
                ContactReward clsContactReward = new ContactReward(base.Connection, base.Transaction);
                Details.RewardDetails = clsContactReward.Details(Details.ContactID);

                // Nov 2, 2011 : Lemu - for credit
                ContactCredit clsContactCredit = new ContactCredit(base.Connection, base.Transaction);
                Details.CreditDetails = clsContactCredit.Details(Details.ContactID);
                Details.CreditDetails.CreditLimit = Details.CreditLimit;
                Details.CreditDetails.CreditActive = Convert.ToBoolean(Details.IsCreditAllowed);
            }
            catch (Exception ex) { throw ex; }
            return Details;
        }
		#endregion

		#region Streams

		public MySqlDataReader List(string SortField, SortOption SortOrder)
		{
			try
			{
				string SQL = SQLSelect() + "WHERE 1=1 AND deleted = '0' ORDER BY " + SortField; 

				if (SortOrder == SortOption.Ascending)
					SQL += " ASC";
				else
					SQL += " DESC";

				MySqlCommand cmd = new MySqlCommand();
				cmd.CommandType = System.Data.CommandType.Text;
				cmd.CommandText = SQL;
				
                return base.ExecuteReader(cmd);
			}
			catch (Exception ex)
			{
				throw ex;
			}	
		}
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
				throw ex;
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
				throw ex;
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
				throw ex;
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
				throw ex;
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
				throw ex;
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
				throw ex;
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
					SQL += " AND a.ContactCode = @ContactCode";
                    cmd.Parameters.AddWithValue("@ContactCode", ContactCode);
				}
                if (ContactName != null && ContactName != string.Empty && ContactName != "" && ContactName != "0" && ContactName != Constants.ALL)
				{
					SQL += " AND a.ContactName = @ContactName";
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
				throw ex;
			}	
		}
        public MySqlDataReader AdvanceSearch(ContactGroupCategory ContactGroupCategory, string ContactCode, string ContactName, Int16 Deleted, Int32 ContactGroupID, bool HasCreditOnly, string SortField, SortOption SortOrder)
        {
            try
            {
                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandType = System.Data.CommandType.Text;

                string SQL = SQLSelect() + "WHERE 1=1 ";

                if (ContactGroupCategory == ContactGroupCategory.CUSTOMER)
                {
                    SQL += "AND (b.ContactGroupCategory = @CustomerCategory OR b.ContactGroupCategory = @BothCategory) ";
                    SQL += "OR (ContactID IN (SELECT CustomerID FROM tblContactRewards WHERE RewardCardNo LIKE @SearchKey)) ";
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
                    SQL += " AND a.ContactCode = @ContactCode";
                    cmd.Parameters.AddWithValue("@ContactCode", ContactCode);
                }
                if (ContactName != null && ContactName != string.Empty && ContactName != "" && ContactName != "0")
                {
                    SQL += " AND a.ContactName = @ContactName";
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

                return base.ExecuteReader(cmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }		
		public MySqlDataReader CustomerAdvanceSearch(string ContactCode, string ContactName, string ContactGroupCode, bool HasCreditOnly, string SortField, SortOption SortOrder)
		{
			try
			{
				MySqlCommand cmd = new MySqlCommand();
				cmd.CommandType = System.Data.CommandType.Text;

				string SQL = SQLSelect() + "WHERE 1=1 ";
				
                SQL += "AND (b.ContactGroupCategory = @CustomerCategory OR b.ContactGroupCategory = @BothCategory) ";

				MySqlParameter prmCustomerCategory = new MySqlParameter("@CustomerCategory",MySqlDbType.Int16);
				prmCustomerCategory.Value = ContactGroupCategory.CUSTOMER.ToString("d");
				cmd.Parameters.Add(prmCustomerCategory);

				MySqlParameter prmBothCategory = new MySqlParameter("@BothCategory",MySqlDbType.Int16);
				prmBothCategory.Value = ContactGroupCategory.BOTH.ToString("d");
				cmd.Parameters.Add(prmBothCategory);

				if (ContactCode != null && ContactCode != string.Empty && ContactCode != "" && ContactCode != "0")
				{
					SQL += " AND a.ContactCode = @ContactCode";

					MySqlParameter prmContactCode = new MySqlParameter("@ContactCode",MySqlDbType.String);
					prmContactCode.Value = ContactCode;
					cmd.Parameters.Add(prmContactCode);
				}
				if (ContactName != null && ContactName != string.Empty && ContactName != "" && ContactName != "0")
				{
					SQL += " AND a.ContactName = @ContactName";

					MySqlParameter prmContactName = new MySqlParameter("@ContactName",MySqlDbType.String);
					prmContactName.Value = ContactName;
					cmd.Parameters.Add(prmContactName);
				}
				
				if (HasCreditOnly == true)
					SQL += "AND Credit > 0 ";

				if (ContactGroupCode != null & ContactGroupCode != string.Empty && ContactGroupCode != "" && ContactGroupCode != "0")
				{
					SQL += "AND b.ContactGroupCode = @ContactGroupCode ";

					MySqlParameter prmContactGroupCode = new MySqlParameter("@ContactGroupCode",MySqlDbType.String);
					prmContactGroupCode.Value = ContactGroupCode;
					cmd.Parameters.Add(prmContactGroupCode);
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
				throw ex;
			}	
		}

        

		#endregion

		#region DataTables

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
                throw ex;
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
                throw ex;
            }
        }		
		public DataTable CustomersDataTable(string SearchKey, Int32 Limit, bool HasCreditOnly, string SortField, SortOption SortOrder)
		{
			try
			{
                string strDataTableName = "tbl" + this.GetType().FullName.Split(new Char[] { '.' })[this.GetType().FullName.Split(new Char[] { '.' }).Length - 1]; System.Data.DataTable dt = new System.Data.DataTable(strDataTableName);

                dt.Columns.Add("ContactID");
                dt.Columns.Add("ContactCode");
                dt.Columns.Add("ContactName");
                dt.Columns.Add("Debit");
                dt.Columns.Add("Credit");
                dt.Columns.Add("CreditLimit");
                dt.Columns.Add("IsCreditAllowed");
                dt.Columns.Add("PositionName");
                dt.Columns.Add("DepartmentName");

                MySqlDataReader myReader = Customers(SearchKey, Limit, HasCreditOnly, SortField, SortOrder);

                while (myReader.Read())
                {
                    System.Data.DataRow dr = dt.NewRow();

                    dr["ContactID"] = myReader.GetInt64("ContactID");
                    dr["ContactCode"] = "" + myReader["ContactCode"].ToString();
                    dr["ContactName"] = "" + myReader["ContactName"].ToString();
                    dr["Debit"] = myReader.GetDecimal("Debit");
                    dr["Credit"] = myReader.GetDecimal("Credit");
                    dr["CreditLimit"] = myReader.GetDecimal("CreditLimit");
                    dr["IsCreditAllowed"] = myReader.GetInt16("IsCreditAllowed");
                    dr["PositionName"] = "" + myReader["PositionName"].ToString();
                    dr["DepartmentName"] = "" + myReader["DepartmentName"].ToString();

                    dt.Rows.Add(dr);
                }
                myReader.Close();

                return dt;
			}
			catch (Exception ex)
			{
				throw ex;
			}	
		}
        public DataTable CustomersDataTable(string SearchKey, Int32 Limit = Constants.C_DEFAULT_LIMIT_OF_RECORD_TO_SHOW, string SortField = "ContactName", SortOption SortOrder=SortOption.Ascending)
		{
			try
			{
				string SQL = SQLSelect() + "WHERE deleted = '0' " +
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

				string strDataTableName = "tbl" + this.GetType().FullName.Split(new Char[] { '.' })[this.GetType().FullName.Split(new Char[] { '.' }).Length - 1]; System.Data.DataTable dt = new System.Data.DataTable(strDataTableName);
				base.MySqlDataAdapterFill(cmd, dt);
				
				return dt;
			}
			catch (Exception ex)
			{
				throw ex;
			}	
		}
        public DataTable ListAsDataTable(string SortField, SortOption SortOrder)
        {
            string SQL = SQLSelect() + "WHERE deleted = '0' ORDER BY " + SortField;

            if (SortOrder == SortOption.Ascending)
                SQL += " ASC";
            else
                SQL += " DESC";

            MySqlCommand cmd = new MySqlCommand();
            cmd.CommandType = System.Data.CommandType.Text;
            cmd.CommandText = SQL;

            string strDataTableName = "tbl" + this.GetType().FullName.Split(new Char[] { '.' })[this.GetType().FullName.Split(new Char[] { '.' }).Length - 1]; System.Data.DataTable dt = new System.Data.DataTable(strDataTableName);
            base.MySqlDataAdapterFill(cmd, dt);

            return dt;
        }
        public DataTable SearchAsDataTable(string SearchKey, string SortField, SortOption SortOrder)
        {
            string SQL = SQLSelect() + "WHERE deleted = '0' AND (ContactCode LIKE @SearchKey or ContactName LIKE @SearchKey) ";

            SQL += "ORDER BY " + SortField;

            if (SortOrder == SortOption.Ascending)
                SQL += " ASC";
            else
                SQL += " DESC";

            MySqlCommand cmd = new MySqlCommand();
            cmd.CommandType = System.Data.CommandType.Text;
            cmd.CommandText = SQL;

            cmd.Parameters.AddWithValue("@SearchKey", SearchKey + "%");

            string strDataTableName = "tbl" + this.GetType().FullName.Split(new Char[] { '.' })[this.GetType().FullName.Split(new Char[] { '.' }).Length - 1]; System.Data.DataTable dt = new System.Data.DataTable(strDataTableName);
            base.MySqlDataAdapterFill(cmd, dt);

            return dt;
        }
        public DataTable SuppliersAsDataTable(string SearchKey = "", Int32 Limit = 0, string SortField = "ContactCode", SortOption SortOrder = SortOption.Ascending)
        {
            MySqlCommand cmd = new MySqlCommand();

            string SQL = SQLSelect() + "WHERE 1=1 AND deleted = 0 ";

            if (SearchKey != string.Empty)
            {
                SQL += "AND (ContactCode LIKE @SearchKey OR ContactName LIKE @SearchKey) ";
                cmd.Parameters.AddWithValue("@SearchKey", SearchKey + "%");
            }
            SQL += "AND (b.ContactGroupCategory = @SupplierCategory OR b.ContactGroupCategory = @BothCategory) ";
            
            SQL += "ORDER BY " + SortField;
            if (SortOrder == SortOption.Ascending)
                SQL += " ASC ";
            else
                SQL += " DESC ";

            if (Limit != 0)
                SQL += "LIMIT " + Limit;

            cmd.CommandType = System.Data.CommandType.Text;
            cmd.CommandText = SQL;

            cmd.Parameters.AddWithValue("@SupplierCategory", ContactGroupCategory.SUPPLIER.ToString("d"));
            cmd.Parameters.AddWithValue("@BothCategory", ContactGroupCategory.BOTH.ToString("d"));

            string strDataTableName = "tbl" + this.GetType().FullName.Split(new Char[] { '.' })[this.GetType().FullName.Split(new Char[] { '.' }).Length - 1]; System.Data.DataTable dt = new System.Data.DataTable(strDataTableName);
            base.MySqlDataAdapterFill(cmd, dt);

            return dt;
        }
        public DataTable AgentsAsDataTable(string SearchKey, Int32 Limit, string SortField, SortOption SortOrder)
        {
            if (SearchKey == null) SearchKey = string.Empty;

            string SQL = SQLSelect() + "WHERE 1=1 AND deleted = '0' " +
                                "AND (ContactCode LIKE @SearchKey " +
                                "OR ContactName LIKE @SearchKey) ";

            SQL += "AND (b.ContactGroupCategory = @AgentCategory OR b.ContactGroupCategory = @BothCategory) ";
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

            cmd.Parameters.AddWithValue("@SearchKey", SearchKey + "%");
            cmd.Parameters.AddWithValue("@AgentCategory", ContactGroupCategory.AGENT.ToString("d"));
            cmd.Parameters.AddWithValue("@BothCategory", ContactGroupCategory.BOTH.ToString("d"));

            string strDataTableName = "tbl" + this.GetType().FullName.Split(new Char[] { '.' })[this.GetType().FullName.Split(new Char[] { '.' }).Length - 1]; System.Data.DataTable dt = new System.Data.DataTable(strDataTableName);
            base.MySqlDataAdapterFill(cmd, dt);

            return dt;
        }

        public DataTable CustomersWithRewards(ContactColumns clsContactColumns, long SequenceNoStart, System.Data.SqlClient.SortOrder SequenceSortOrder, Int32 Limit, string CustomerCode_RewardCardNo, DateTime RewardExpiryDateFrom, DateTime RewardExpiryDateTo, Constants.DateSelectionString BirthDate = Constants.DateSelectionString.ALL, Int16 RewardCardStatus = -1, string SortField = "ContactCode", System.Data.SqlClient.SortOrder SortOrder = System.Data.SqlClient.SortOrder.Ascending)
        {
            try
            {
                MySqlCommand cmd = new MySqlCommand();

                // enable this to include joining to table tblContactGroup
                clsContactColumns.ContactGroupName = true;

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

                SQL += "ORDER BY " + SortField + " ";

                if (SortOrder != System.Data.SqlClient.SortOrder.Descending) SQL += "ASC ";
                else SQL += "DESC ";

                if (Limit != 0)
                    SQL += "LIMIT " + Limit + " ";

                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandText = SQL;

                string strDataTableName = "tbl" + this.GetType().FullName.Split(new Char[] { '.' })[this.GetType().FullName.Split(new Char[] { '.' }).Length - 1]; System.Data.DataTable dt = new System.Data.DataTable(strDataTableName);
                base.MySqlDataAdapterFill(cmd, dt);

                return dt;
            }
            catch (Exception ex)
            {
                throw ex;
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
				throw ex;
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
				throw ex;
			}	
		}
		public void SubtractCredit(long ContactID, decimal Amount)
		{
			try 
			{
                string SQL = "CALL procContactSubtractCredit(@ContactID, @Credit);";
	 			
				MySqlCommand cmd = new MySqlCommand();
				cmd.CommandType = System.Data.CommandType.Text;
				cmd.CommandText = SQL;

                cmd.Parameters.AddWithValue("@ContactID", ContactID);
                cmd.Parameters.AddWithValue("@Credit", Amount);

				base.ExecuteNonQuery(cmd);
			}

			catch (Exception ex)
			{
				throw ex;
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
                throw ex;
            }
        }

		#endregion
	}
}
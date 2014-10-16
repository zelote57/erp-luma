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

	public struct ContactCreditCardInfoDetails
	{
		public Int64 CustomerID;
        public Int64 GuarantorID;
        public CardTypeDetails CardTypeDetails;
        public string CreditCardNo;
        public string EmbossedCardNo;
        public DateTime CreditAwardDate;
        public decimal TotalPurchases;
        public decimal CreditPaid;
        public CreditCardStatus CreditCardStatus;
        public DateTime ExpiryDate;
        public DateTime LastBillingDate;
        public bool CreditActive;
        public decimal CreditLimit;

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
	public class ContactCreditCardInfos : POSConnection
    {
		#region Constructors and Destructors

		public ContactCreditCardInfos()
            : base(null, null)
        {
        }

        public ContactCreditCardInfos(MySqlConnection Connection, MySqlTransaction Transaction) 
            : base(Connection, Transaction)
		{

		}

		#endregion

		#region Insert and Update

		public bool Insert(ContactCreditCardInfoDetails Details)
		{
			try  
			{
                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandType = System.Data.CommandType.Text;

                string SQL = "CALL procContactCreditModify(@CustomerID, @GuarantorID, @CreditCardTypeID, @CreditCardNo, @CreditAwardDate, @CreditCardStatus, @ExpiryDate, @CreditActive, @CreditLimit);";

                cmd.Parameters.AddWithValue("@CustomerID", Details.CustomerID);
                cmd.Parameters.AddWithValue("@GuarantorID", Details.GuarantorID);
                cmd.Parameters.AddWithValue("@CreditCardTypeID", Convert.ToInt16(Details.CardTypeDetails.CardTypeID));
                cmd.Parameters.AddWithValue("@CreditCardNo", Details.CreditCardNo);
                cmd.Parameters.AddWithValue("@CreditAwardDate", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                cmd.Parameters.AddWithValue("@CreditCardStatus", Details.CreditCardStatus.ToString("d"));
                cmd.Parameters.AddWithValue("@ExpiryDate", Details.ExpiryDate.ToString("yyyy-MM-dd"));
                cmd.Parameters.AddWithValue("@CreditActive", Convert.ToInt16(Details.CreditActive));
                cmd.Parameters.AddWithValue("@CreditLimit", Details.CreditLimit);

                cmd.CommandText = SQL;
				bool bolRetValue = false;
                if (base.ExecuteNonQuery(cmd) > 0) bolRetValue = true;
                return bolRetValue;
			}
			catch (Exception ex)
			{
				throw base.ThrowException(ex);
			}	
		}
		public bool Update(ContactCreditCardInfoDetails Details)
		{
			try 
			{
                return Insert(Details);
			}
			catch (Exception ex)
			{
				throw base.ThrowException(ex);
			}	
		}

        public Int32 Save(ContactCreditCardInfoDetails Details)
        {
            try
            {
                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandType = System.Data.CommandType.Text;

                string SQL = "CALL procSaveContactCreditCardInfo(@CustomerID, @GuarantorID, @CreditCardTypeID, @CreditCardNo, @CreditAwardDate, " +
                                                    "@TotalPurchases, @CreditPaid, @CreditCardStatus, @ExpiryDate, @EmbossedCardNo, " +
                                                    "@LastBillingDate, @CreatedOn, @LastModified);";
                
                cmd.Parameters.AddWithValue("CustomerID", Details.CustomerID);
                cmd.Parameters.AddWithValue("GuarantorID", Details.GuarantorID);
                cmd.Parameters.AddWithValue("CreditCardTypeID", Details.CardTypeDetails.CardTypeID);
                cmd.Parameters.AddWithValue("CreditCardNo", Details.CreditCardNo);
                cmd.Parameters.AddWithValue("CreditAwardDate", Details.CreditAwardDate);
                cmd.Parameters.AddWithValue("TotalPurchases", Details.TotalPurchases);
                cmd.Parameters.AddWithValue("CreditPaid", Details.CreditPaid);
                cmd.Parameters.AddWithValue("CreditCardStatus", Details.CreditCardStatus.ToString("d"));
                cmd.Parameters.AddWithValue("ExpiryDate", Details.ExpiryDate);
                cmd.Parameters.AddWithValue("EmbossedCardNo", Details.CreditCardNo);
                cmd.Parameters.AddWithValue("LastBillingDate", Details.LastBillingDate);
                cmd.Parameters.AddWithValue("CreatedOn", Details.CreatedOn == DateTime.MinValue ? Constants.C_DATE_MIN_VALUE : Details.CreatedOn);
                cmd.Parameters.AddWithValue("LastModified", Details.LastModified == DateTime.MinValue ? Constants.C_DATE_MIN_VALUE : Details.LastModified);

                cmd.CommandText = SQL;
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
                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandType = System.Data.CommandType.Text;

				string SQL=	"DELETE FROM tblContacts WHERE ContactID IN (" + IDs + ");";

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
                                "CustomerID, " +
                                "GuarantorID, " +
                                "CreditCardTypeID, " +
                                "CreditCardNo, " +
                                "CreditAwardDate, " +
                                "TotalPurchases, " +
                                "CreditPaid, " +
                                "CreditCardStatus, " +
                                "ExpiryDate " +
                            "FROM tblContactCreditCardInfo ";
            return stSQL;
        }

		#region Details

		public ContactCreditCardInfoDetails Details(Int64 ContactID)
		{
			try
			{
                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandType = System.Data.CommandType.Text;
				
				string SQL=	SQLSelect() + "WHERE CustomerID = @ContactID;";

                cmd.Parameters.AddWithValue("@ContactID", ContactID);

                cmd.CommandText = SQL;
                string strDataTableName = "tbl" + this.GetType().FullName.Split(new Char[] { '.' })[this.GetType().FullName.Split(new Char[] { '.' }).Length - 1]; System.Data.DataTable dt = new System.Data.DataTable(strDataTableName);
                base.MySqlDataAdapterFill(cmd, dt);

                return setDetails(dt);
			}
			catch (Exception ex)
			{
				throw base.ThrowException(ex);
			}	
		}
		public ContactCreditCardInfoDetails Details(string CreditCardNo)
		{
			try
			{
                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandType = System.Data.CommandType.Text;

                string SQL = SQLSelect() + "WHERE CreditCardNo = @CreditCardNo;";

                cmd.Parameters.AddWithValue("@CreditCardNo", CreditCardNo);

                cmd.CommandText = SQL;
                string strDataTableName = "tbl" + this.GetType().FullName.Split(new Char[] { '.' })[this.GetType().FullName.Split(new Char[] { '.' }).Length - 1]; System.Data.DataTable dt = new System.Data.DataTable(strDataTableName);
                base.MySqlDataAdapterFill(cmd, dt);

                return setDetails(dt);
			}
			catch (Exception ex)
			{
				throw base.ThrowException(ex);
			}	
		}

        private ContactCreditCardInfoDetails setDetails(System.Data.DataTable dt)
        {
            ContactCreditCardInfoDetails Details = new ContactCreditCardInfoDetails();
            foreach (System.Data.DataRow dr in dt.Rows)
            {
                Details.CustomerID = Int64.Parse(dr["CustomerID"].ToString());
                Details.GuarantorID = Int64.Parse(dr["GuarantorID"].ToString());
                Details.CardTypeDetails = new CardType(base.Connection, base.Transaction).Details(Int16.Parse(dr["CreditCardTypeID"].ToString()));
                Details.CreditCardNo = "" + dr["CreditCardNo"].ToString();
                Details.CreditAwardDate = DateTime.Parse(dr["CreditAwardDate"].ToString());
                Details.TotalPurchases = decimal.Parse(dr["TotalPurchases"].ToString());
                Details.CreditPaid = decimal.Parse(dr["CreditPaid"].ToString());
                Details.CreditCardStatus = (CreditCardStatus)Enum.Parse(typeof(CreditCardStatus), dr["CreditCardStatus"].ToString());
                Details.ExpiryDate = DateTime.Parse(dr["ExpiryDate"].ToString());
            }

            return Details;
        }
		#endregion

		#region Streams

		public MySqlDataReader List(string SortField, SortOption SortOrder)
		{
			try
			{
                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandType = System.Data.CommandType.Text;

				string SQL = SQLSelect() + "WHERE 1=1 ORDER BY " + SortField; 

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
				
				
				{
					
					
					
					
				}

				throw base.ThrowException(ex);
			}	
		}		

        public DataTable ListAsDataTable(string SortField, SortOption SortOrder)
        {
            MySqlCommand cmd = new MySqlCommand();
            cmd.CommandType = System.Data.CommandType.Text;

            string SQL = SQLSelect() + "WHERE ORDER BY " + SortField;

            if (SortOrder == SortOption.Ascending)
                SQL += " ASC";
            else
                SQL += " DESC";

            cmd.CommandText = SQL;
            System.Data.DataTable dt = new System.Data.DataTable("tblContactCredits");
            base.MySqlDataAdapterFill(cmd, dt);

            return dt;
        }
        public DataTable SearchAsDataTable(string SearchKey, string SortField, SortOption SortOrder)
        {
            string SQL = SQLSelect() + "WHERE (CreditCardNo LIKE @SearchKey or CreditActive LIKE @SearchKey) ";

            SQL += "ORDER BY " + SortField;

            if (SortOrder == SortOption.Ascending)
                SQL += " ASC";
            else
                SQL += " DESC";

            

            MySqlCommand cmd = new MySqlCommand();
            
            
            cmd.CommandType = System.Data.CommandType.Text;
            cmd.CommandText = SQL;

            cmd.Parameters.AddWithValue("@SearchKey", SearchKey + "%");

            System.Data.DataTable dt = new System.Data.DataTable("tblContactCredits");
            base.MySqlDataAdapterFill(cmd, dt);
            

            return dt;
        }

		#endregion

		#region Public Modifiers

        public void AddPurchase(long ContactID, decimal Amount)
        {
            try
            {
                string SQL = "CALL procContactCreditsAddPurchase(@ContactID, @Amount);";

                

                MySqlCommand cmd = new MySqlCommand();
                
                
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandText = SQL;

                cmd.Parameters.AddWithValue("@ContactID", ContactID);
                cmd.Parameters.AddWithValue("@Amount", Amount);

                base.ExecuteNonQuery(cmd);
            }

            catch (Exception ex)
            {
                
                
                {
                    
                    
                    
                    
                }

                throw base.ThrowException(ex);
            }
        }
		public void AddCredit(long ContactID, decimal Amount)
		{
			try 
			{
                Contacts clsContact = new Contacts(base.Connection, base.Transaction);
                clsContact.AddCredit(ContactID, Amount);
			}

			catch (Exception ex)
			{
				
				
				{
					
					
					
					
				}

				throw base.ThrowException(ex);
			}	
		}
        public void DeductCredit(long ContactID, decimal Amount)
		{
			try 
			{
                Contacts clsContact = new Contacts(base.Connection, base.Transaction);
                clsContact.AddCredit(ContactID, -Amount);
			}

			catch (Exception ex)
			{
				
				
				{
					
					
					
					
				}

				throw base.ThrowException(ex);
			}	
		}

        public void AddMovement(long lngCustomerID, DateTime dteCreditDate, decimal decCreditPointsBefore, decimal decCreditPointsAdjustment, decimal decCreditPointsAfter, DateTime dteCreditExpiryDate, string strCreditReason, string strTerminalNo, string strCashierName, string strTransactionNo)
        {
            try
            {
                string SQL = "CALL procContactCreditsMovementInsert(@lngCustomerID, @dteCreditDate, @decCreditPointsBefore, @decCreditPointsAdjustment, @decCreditPointsAfter, @dteCreditExpiryDate, @strCreditReason, @strTerminalNo, @strCashierName, @strTransactionNo);";

                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandText = SQL;

                cmd.Parameters.AddWithValue("@lngCustomerID", lngCustomerID);
                cmd.Parameters.AddWithValue("@dteCreditDate", dteCreditDate);
                cmd.Parameters.AddWithValue("@decCreditPointsBefore", decCreditPointsBefore);
                cmd.Parameters.AddWithValue("@decCreditPointsAdjustment", decCreditPointsAdjustment);
                cmd.Parameters.AddWithValue("@decCreditPointsAfter", decCreditPointsAfter);
                cmd.Parameters.AddWithValue("@dteCreditExpiryDate", dteCreditExpiryDate);
                cmd.Parameters.AddWithValue("@strCreditReason", strCreditReason);
                cmd.Parameters.AddWithValue("@strTerminalNo", strTerminalNo);
                cmd.Parameters.AddWithValue("@strCashierName", strCashierName);
                cmd.Parameters.AddWithValue("@strTransactionNo", strTransactionNo);

                base.ExecuteNonQuery(cmd);
            }
            catch (Exception ex)
            {
                throw base.ThrowException(ex);
            }
        }

		#endregion
	}
}
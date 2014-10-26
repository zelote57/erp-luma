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
	public struct CardTypeDetails
	{
		public Int16 CardTypeID;
		public string CardTypeCode;
		public string CardTypeName;
        public decimal CreditFinanceCharge;
        public decimal CreditLatePenaltyCharge;
        public decimal CreditMinimumAmountDue;
        public decimal CreditMinimumPercentageDue;
        public decimal CreditFinanceCharge15th;
        public decimal CreditLatePenaltyCharge15th;
        public decimal CreditMinimumAmountDue15th;
        public decimal CreditMinimumPercentageDue15th;
        public DateTime CreditPurcStartDateToProcess;
        public DateTime CreditPurcEndDateToProcess;
        /// <summary>
        /// Payment Due Date
        /// </summary>
        public DateTime CreditCutOffDate;

        public CreditCardTypes CreditCardType;
        public bool WithGuarantor;
        
        public DateTime BillingDate;
        public DateTime CreatedOn;
        public DateTime LastModified;

        public bool CheckGuarantor;

        public CardTypeDetails(CreditCardTypes CreditCardType)
        {
            this.CardTypeID = 0;
            this.CardTypeCode = string.Empty;
            this.CardTypeName = string.Empty;
            this.CreditFinanceCharge = 0;
            this.CreditLatePenaltyCharge = 0;
            this.CreditMinimumAmountDue = 0;
            this.CreditMinimumPercentageDue = 0;
            this.CreditFinanceCharge15th = 0;
            this.CreditLatePenaltyCharge15th = 0;
            this.CreditMinimumAmountDue15th = 0;
            this.CreditMinimumPercentageDue15th = 0;
            this.CreditPurcStartDateToProcess = Constants.C_DATE_MIN_VALUE;
            this.CreditPurcEndDateToProcess = Constants.C_DATE_MIN_VALUE;
            this.CreditCutOffDate = Constants.C_DATE_MIN_VALUE;
            this.CreditCardType = CreditCardType;
            this.WithGuarantor = false;
            this.BillingDate = Constants.C_DATE_MIN_VALUE;
            this.CreatedOn = DateTime.Now;
            this.LastModified = DateTime.Now;

            this.CheckGuarantor = false;
        }

        public CardTypeDetails(CreditCardTypes CreditCardType, bool WithGuarantor)
        {
            this.CardTypeID = 0;
            this.CardTypeCode = string.Empty;
            this.CardTypeName = string.Empty;
            this.CreditFinanceCharge = 0;
            this.CreditLatePenaltyCharge = 0;
            this.CreditMinimumAmountDue = 0;
            this.CreditMinimumPercentageDue = 0;
            this.CreditFinanceCharge15th = 0;
            this.CreditLatePenaltyCharge15th = 0;
            this.CreditMinimumAmountDue15th = 0;
            this.CreditMinimumPercentageDue15th = 0;
            this.CreditPurcStartDateToProcess = Constants.C_DATE_MIN_VALUE;
            this.CreditPurcEndDateToProcess = Constants.C_DATE_MIN_VALUE;
            this.CreditCutOffDate = Constants.C_DATE_MIN_VALUE;
            this.CreditCardType = CreditCardType;
            this.WithGuarantor = WithGuarantor;
            this.BillingDate = Constants.C_DATE_MIN_VALUE;
            this.CreatedOn = DateTime.Now;
            this.LastModified = DateTime.Now;

            this.CheckGuarantor = true;
        }
	}

	[StrongNameIdentityPermissionAttribute(SecurityAction.LinkDemand,
		 PublicKey = "002400000480000094000000060200000024000" +
		 "052534131000400000100010053D785642F9F960B43157E0380" +
		 "F393BEE53E8DFAFBF441366C1B6F8B48D9DDF0D527B1F3B21EA" +
		 "E85D2FDB664CE81EB8A87DBE4C589D6F4202FE2B7D4B978BB69" +
		 "684874612CB9B8DB7A0339400A9C4E68277884B07817363D242" +
		 "E3696F9FACDBEA831810AE6DC9EDCA91A7B5DA12FE7BF65D113" +
		 "FF52834EAFB5A7A1FDFD5851A3")]
	public class CardType : POSConnection
    {
		#region Constructors and Destructors

		public CardType()
            : base(null, null)
        {
        }

        public CardType(MySqlConnection Connection, MySqlTransaction Transaction) 
            : base(Connection, Transaction)
		{

		}

		#endregion
		
		#region Insert and Update

        public Int32 Insert(CardTypeDetails Details)
        {
            try
            {
                Save(Details);

                return Int16.Parse(base.getLAST_INSERT_ID(this));
            }
            catch (Exception ex)
            {
                throw base.ThrowException(ex);
            }
        }

        public void Update(CardTypeDetails Details)
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

        public Int32 Save(CardTypeDetails Details)
        {
            try
            {
                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandType = System.Data.CommandType.Text;

                string SQL = "CALL procSaveCardType(@CardTypeID, @CardTypeCode, @CardTypeName, @CreditFinanceCharge, @CreditLatePenaltyCharge, @CreditMinimumAmountDue, @CreditMinimumPercentageDue, " +
                                                   "@CreditFinanceCharge15th, @CreditLatePenaltyCharge15th, @CreditMinimumAmountDue15th, @CreditMinimumPercentageDue15th, " +
                                                   "@CreditCardType, @WithGuarantor, @BillingDate, @CreatedOn, @LastModified);";

                cmd.Parameters.AddWithValue("CardTypeID", Details.CardTypeID);
                cmd.Parameters.AddWithValue("CardTypeCode", Details.CardTypeCode);
                cmd.Parameters.AddWithValue("CardTypeName", Details.CardTypeName);
                cmd.Parameters.AddWithValue("CreditFinanceCharge", Details.CreditFinanceCharge);
                cmd.Parameters.AddWithValue("CreditLatePenaltyCharge", Details.CreditLatePenaltyCharge);
                cmd.Parameters.AddWithValue("CreditMinimumAmountDue", Details.CreditMinimumAmountDue);
                cmd.Parameters.AddWithValue("CreditMinimumPercentageDue", Details.CreditMinimumPercentageDue);
                cmd.Parameters.AddWithValue("CreditFinanceCharge15th", Details.CreditFinanceCharge15th);
                cmd.Parameters.AddWithValue("CreditLatePenaltyCharge15th", Details.CreditLatePenaltyCharge15th);
                cmd.Parameters.AddWithValue("CreditMinimumAmountDue15th", Details.CreditMinimumAmountDue15th);
                cmd.Parameters.AddWithValue("CreditMinimumPercentageDue15th", Details.CreditMinimumPercentageDue15th);
                cmd.Parameters.AddWithValue("CreditCardType", Details.CreditCardType);
                cmd.Parameters.AddWithValue("WithGuarantor", Details.WithGuarantor);
                cmd.Parameters.AddWithValue("BillingDate", Details.BillingDate == DateTime.MinValue ? Constants.C_DATE_MIN_VALUE : Details.BillingDate);
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

		#region Details

		public bool Delete(string IDs)
		{
			try 
			{
                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandType = System.Data.CommandType.Text;
				
				string SQL=	"DELETE FROM tblCardTypes WHERE CardTypeID IN (" + IDs + ");";
	 			
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
                                "CardTypeID, " +
                                "CardTypeCode, " +
                                "CardTypeName, " +
                                "CreditFinanceCharge, " +
                                "CreditLatePenaltyCharge, " +
                                "CreditMinimumAmountDue, " +
                                "CreditMinimumPercentageDue, " +
                                "CreditFinanceCharge15th, " +
                                "CreditLatePenaltyCharge15th, " +
                                "CreditMinimumAmountDue15th, " +
                                "CreditMinimumPercentageDue15th, " +
                                "CreditPurcStartDateToProcess, " +
                                "CreditPurcEndDateToProcess," +
                                "CreditCutOffDate," +
                                "CreditCardType, " +
                                "WithGuarantor, " +
                                "CreditUseLastDayCutOffDate, " +
                                "BillingDate, " +
                                "CreatedOn, " +
                                "LastModified " +
                            "FROM tblCardTypes ";

            return stSQL;
        }

		#region Details

		public CardTypeDetails Details(Int16 CardTypeID)
		{
			try
			{
                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandType = System.Data.CommandType.Text;

				string SQL=	SQLSelect() + "WHERE CardTypeID = @CardTypeID;";

                cmd.Parameters.AddWithValue("@CardTypeID", CardTypeID);

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
		public CardTypeDetails Details(string CardTypeName)
		{
			try
			{
                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandType = System.Data.CommandType.Text;

				string SQL=	SQLSelect() + "WHERE CardTypeName = @CardTypeName;";

                cmd.Parameters.AddWithValue("CardTypeName", CardTypeName);

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

        private CardTypeDetails setDetails(System.Data.DataTable dt)
        {
            CardTypeDetails Details = new CardTypeDetails();
            foreach (System.Data.DataRow dr in dt.Rows)
            {
                Details.CardTypeID = Int16.Parse(dr["CardTypeID"].ToString());
                Details.CardTypeCode = dr["CardTypeCode"].ToString();
                Details.CardTypeName = dr["CardTypeName"].ToString();

                Details.CreditFinanceCharge = decimal.Parse(dr["CreditFinanceCharge"].ToString());
                Details.CreditLatePenaltyCharge = decimal.Parse(dr["CreditLatePenaltyCharge"].ToString());
                Details.CreditMinimumAmountDue = decimal.Parse(dr["CreditMinimumAmountDue"].ToString());
                Details.CreditMinimumPercentageDue = decimal.Parse(dr["CreditMinimumPercentageDue"].ToString());
                Details.CreditFinanceCharge15th = decimal.Parse(dr["CreditFinanceCharge15th"].ToString());
                Details.CreditLatePenaltyCharge15th = decimal.Parse(dr["CreditLatePenaltyCharge15th"].ToString());
                Details.CreditMinimumAmountDue15th = decimal.Parse(dr["CreditMinimumAmountDue15th"].ToString());
                Details.CreditMinimumPercentageDue15th = decimal.Parse(dr["CreditMinimumPercentageDue15th"].ToString());
                Details.CreditPurcStartDateToProcess = DateTime.Parse(dr["CreditPurcStartDateToProcess"].ToString());
                Details.CreditPurcEndDateToProcess = DateTime.Parse(dr["CreditPurcEndDateToProcess"].ToString());
                Details.CreditCutOffDate = DateTime.Parse(dr["CreditCutOffDate"].ToString());
                Details.CreditCardType = (CreditCardTypes)Enum.Parse(typeof(CreditCardTypes), dr["CreditCardType"].ToString());
                Details.WithGuarantor = bool.Parse(dr["WithGuarantor"].ToString());
                Details.BillingDate = DateTime.Parse(dr["BillingDate"].ToString());
                Details.CreatedOn = DateTime.Parse(dr["CreatedOn"].ToString());
                Details.LastModified = DateTime.Parse(dr["LastModified"].ToString());
            }

            return Details;
        }

		#endregion

		#region Streams

		public System.Data.DataTable ListAsDataTable(CardTypeDetails SearchKeys, string SortField = "CardTypeCode", SortOption SortOrder = SortOption.Ascending, Int32 limit = 0)
		{
            MySqlCommand cmd = new MySqlCommand();
            cmd.CommandType = System.Data.CommandType.Text;

            string SQL = SQLSelect() + "WHERE 1=1 ";

            if (SearchKeys.CreditCardType != CreditCardTypes.Both)
            {
                SQL += "AND CreditCardType = @CreditCardType ";
                cmd.Parameters.AddWithValue("CreditCardType", SearchKeys.CreditCardType.ToString("d"));
            }
            if (!string.IsNullOrEmpty(SearchKeys.CardTypeCode))
            {
                SQL += "AND CardTypeCode = @CardTypeCode ";
                cmd.Parameters.AddWithValue("CardTypeCode", SearchKeys.CardTypeCode);
            }
            if (!string.IsNullOrEmpty(SearchKeys.CardTypeCode))
            {
                SQL += "AND CardTypeName = @CardTypeName ";
                cmd.Parameters.AddWithValue("CardTypeName", SearchKeys.CardTypeName);
            }
            if (SearchKeys.CheckGuarantor)
            {
                SQL += "AND WithGuarantor = @WithGuarantor ";
                cmd.Parameters.AddWithValue("WithGuarantor", SearchKeys.WithGuarantor);
            }

            SQL += "ORDER BY " + (!string.IsNullOrEmpty(SortField) ? SortField : "CardTypeCode") + " ";
            SQL += SortOrder == SortOption.Ascending ? "ASC " : "DESC ";
            SQL += limit == 0 ? "" : "LIMIT " + limit.ToString() + " ";

            cmd.CommandText = SQL;
            string strDataTableName = "tbl" + this.GetType().FullName.Split(new Char[] { '.' })[this.GetType().FullName.Split(new Char[] { '.' }).Length - 1]; System.Data.DataTable dt = new System.Data.DataTable(strDataTableName);
            base.MySqlDataAdapterFill(cmd, dt);

            return dt;
		}
		
		#endregion
	}
}


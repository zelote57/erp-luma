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
	public struct CreditBillDetails
	{
		public Int64 CreditBillID;
        public DateTime CreditPurcStartDateToProcess;
        public DateTime CreditPurcEndDateToProcess;
        public DateTime BillingDate;
        public DateTime CreditCutOffDate;
		public DateTime CreditPaymentDueDate;
        public Int16 CreditCardTypeID;
        public string CardTypeCode;
        public CreditCardTypes CreditCardType;
        public bool WithGuarantor;
        public bool CreditUseLastDayCutOffDate;
        public decimal CreditFinanceCharge;
        public decimal CreditMinimumPercentageDue;
        public decimal CreditMinimumAmountDue;
        public decimal CreditLatePenaltyCharge;
        public decimal CreditFinanceCharge15th;
        public decimal CreditMinimumPercentageDue15th;
        public decimal CreditMinimumAmountDue15th;
        public decimal CreditLatePenaltyCharge15th;
        public DateTime CreatedOn;
        public Int64 CreatedByID;
        public string CreatedByName;
	}

	[StrongNameIdentityPermissionAttribute(SecurityAction.LinkDemand,
		 PublicKey = "002400000480000094000000060200000024000" +
		 "052534131000400000100010053D785642F9F960B43157E0380" +
		 "F393BEE53E8DFAFBF441366C1B6F8B48D9DDF0D527B1F3B21EA" +
		 "E85D2FDB664CE81EB8A87DBE4C589D6F4202FE2B7D4B978BB69" +
		 "684874612CB9B8DB7A0339400A9C4E68277884B07817363D242" +
		 "E3696F9FACDBEA831810AE6DC9EDCA91A7B5DA12FE7BF65D113" +
		 "FF52834EAFB5A7A1FDFD5851A3")]
	public class CreditBills : POSConnection
    {
		#region Constructors and Destructors

		public CreditBills()
            : base(null, null)
        {
        }

        public CreditBills(MySqlConnection Connection, MySqlTransaction Transaction) 
            : base(Connection, Transaction)
		{

		}

		#endregion


        private string SQLSelect()
        {
            string stSQL = "SELECT " +
					            "CreditBillID, CreditPurcStartDateToProcess, CreditPurcEndDateToProcess, " +
                                "BillingDate, CreditCutOffDate, CreditPaymentDueDate, CreditCardTypeID, " +
                                "CardTypeCode, CreditCardType, WithGuarantor, CreditUseLastDayCutOffDate, " +
                                "CreditFinanceCharge, CreditMinimumPercentageDue, CreditMinimumAmountDue, " +
                                "CreditLatePenaltyCharge, CreditFinanceCharge15th, CreditMinimumPercentageDue15th, " +
                                "CreditMinimumAmountDue15th, CreditLatePenaltyCharge15th, CreatedOn, " +
                                "CreatedByID, CreatedByName " +
					        "FROM tblCreditBills ";

            return stSQL;
        }

		#region Details

		public CreditBillDetails Details(Int64 CreditBillID)
		{
			try
			{
                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandType = System.Data.CommandType.Text;

                string SQL = SQLSelect() + "WHERE CreditBillID = @CreditBillID;";

                cmd.Parameters.AddWithValue("@CreditBillID", CreditBillID);

                cmd.CommandText = SQL;
                string strDataTableName = "tbl" + this.GetType().FullName.Split(new Char[] { '.' })[this.GetType().FullName.Split(new Char[] { '.' }).Length - 1]; System.Data.DataTable dt = new System.Data.DataTable(strDataTableName);
                base.MySqlDataAdapterFill(cmd, dt);

                CreditBillDetails Details = setDetails(dt);

                return Details;
			}
			catch (Exception ex)
			{
				throw base.ThrowException(ex);
			}	
		}

        public CreditBillDetails Details(CreditType CreditType = CreditType.Both, DateTime? BillingDate = null, Int16 CreditCardTypeID = 0)
        {
            try
            {
                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandType = System.Data.CommandType.Text;

                string SQL = SQLSelect() + "WHERE 1=1 ";

                if (CreditType == CreditType.Group)
                { SQL += "AND WithGuarantor = 1 "; }
                else if (CreditType == CreditType.Individual)
                { SQL += "AND WithGuarantor = 0 "; }

                if (BillingDate.GetValueOrDefault(Constants.C_DATE_MIN_VALUE) == Constants.C_DATE_MIN_VALUE)
                {
                    SQL += "AND BillingDate = (SELECT MAX(BillingDate) FROM tblCreditBills) ";
                }
                else
                {
                    SQL += "AND BillingDate = @BillingDate ";
                    cmd.Parameters.AddWithValue("BillingDate", BillingDate.GetValueOrDefault(Constants.C_DATE_MIN_VALUE));
                }
                if (CreditCardTypeID != 0)
                {
                    SQL += "AND CreditCardTypeID = @CreditCardTypeID ";
                    cmd.Parameters.AddWithValue("CreditCardTypeID", CreditCardTypeID);
                }

                SQL += "ORDER BY CreditBillID ";
                SQL += "ASC ";
                SQL += "LIMIT 1 ";

                cmd.CommandText = SQL;
                string strDataTableName = "tbl" + this.GetType().FullName.Split(new Char[] { '.' })[this.GetType().FullName.Split(new Char[] { '.' }).Length - 1]; System.Data.DataTable dt = new System.Data.DataTable(strDataTableName);
                base.MySqlDataAdapterFill(cmd, dt);

                CreditBillDetails Details = setDetails(dt);

                return Details;
            }
            catch (Exception ex)
            {
                throw base.ThrowException(ex);
            }
        }

        public CreditBillDetails setDetails(System.Data.DataTable dt)
        {
            CreditBillDetails Details = new CreditBillDetails();
            foreach (System.Data.DataRow dr in dt.Rows)
            {
                //Details.CreditBillID = Int32.Parse(dr["CreditBillID"].ToString());
                //Details.CreditBillsCode = dr["CreditBillsCode"].ToString();
                //Details.CreditBillsName = dr["CreditBillsName"].ToString();
                //Details.DBIP = dr["DBIP"].ToString();
                //Details.DBPort = dr["DBPort"].ToString();
                //Details.Address = dr["Address"].ToString();
                //Details.Remarks = dr["Remarks"].ToString();
                Details.CreditBillID = Int64.Parse(dr["CreditBillID"].ToString());
                Details.CreditPurcStartDateToProcess = DateTime.Parse(dr["CreditPurcStartDateToProcess"].ToString());
                Details.CreditPurcEndDateToProcess = DateTime.Parse(dr["CreditPurcEndDateToProcess"].ToString());
                Details.BillingDate = DateTime.Parse(dr["BillingDate"].ToString());
                Details.CreditCutOffDate = DateTime.Parse(dr["CreditCutOffDate"].ToString()); 
                Details.CreditPaymentDueDate = DateTime.Parse(dr["CreditPaymentDueDate"].ToString());
                Details.CreditCardTypeID = Int16.Parse(dr["CreditCardTypeID"].ToString());
                Details.CardTypeCode = dr["CardTypeCode"].ToString();
                Details.CreditCardType  = (CreditCardTypes) Enum.Parse(typeof(CreditCardTypes), dr["CreditCardType"].ToString());
                Details.WithGuarantor = bool.Parse(dr["WithGuarantor"].ToString());
                Details.CreditUseLastDayCutOffDate = bool.Parse(dr["CreditUseLastDayCutOffDate"].ToString());
                Details.CreditFinanceCharge = Decimal.Parse(dr["CreditFinanceCharge"].ToString());
                Details.CreditMinimumPercentageDue = Decimal.Parse(dr["CreditMinimumPercentageDue"].ToString());
                Details.CreditMinimumAmountDue = Decimal.Parse(dr["CreditMinimumAmountDue"].ToString());
                Details.CreditLatePenaltyCharge = Decimal.Parse(dr["CreditLatePenaltyCharge"].ToString());
                Details.CreditFinanceCharge15th = Decimal.Parse(dr["CreditFinanceCharge15th"].ToString());
                Details.CreditMinimumPercentageDue15th = Decimal.Parse(dr["CreditMinimumPercentageDue15th"].ToString());
                Details.CreditMinimumAmountDue15th = Decimal.Parse(dr["CreditMinimumAmountDue15th"].ToString());
                Details.CreditLatePenaltyCharge15th = Decimal.Parse(dr["CreditLatePenaltyCharge15th"].ToString());
                Details.CreatedOn = DateTime.Parse(dr["CreatedOn"].ToString());
                Details.CreatedByID = Int64.Parse(dr["CreatedByID"].ToString());
                Details.CreatedByName = dr["CreatedByName"].ToString();
            }
            return Details;
        }

		#endregion

	}
}


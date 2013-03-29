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

	#region POReturnDetails

	public struct POReturnDetails
	{
		public long DebitMemoID;
		public string MemoNo;
		public DateTime MemoDate;
		public long SupplierID;
		public string SupplierCode;
		public string SupplierContact;
		public string SupplierAddress;
		public string SupplierTelephoneNo;
		public int SupplierModeOfTerms;
		public int SupplierTerms;
		public DateTime RequiredPostingDate;
		public int BranchID;
		public string BranchCode;
		public string BranchName;
		public string BranchAddress;
        public long PurchaserID;
        public string PurchaserName;
        public decimal SubTotal;
        public decimal Discount;
        public decimal DiscountApplied;
        public DiscountTypes DiscountType;
        public decimal Discount2;
        public decimal Discount2Applied;
        public DiscountTypes Discount2Type;
        public decimal Discount3;
        public decimal Discount3Applied;
        public DiscountTypes Discount3Type;
        public decimal VAT;
        public decimal VatableAmount;
        public decimal EVAT;
        public decimal EVatableAmount;
        public decimal LocalTax;
        public decimal Freight;
        public decimal Deposit;
        public decimal UnpaidAmount;
        public decimal PaidAmount;
        public decimal TotalItemDiscount;
        public POReturnStatus ReturnStatus;
        public bool IsVatInclusive;
        public string Remarks;
        public string SupplierDocNo;
        public DateTime PostingDate;
        public DateTime CancelledDate;
        public string CancelledRemarks;
        public long CancelledByID;
        public int ChartOfAccountIDAPTracking;
        public int ChartOfAccountIDAPFreight;
        public int ChartOfAccountIDAPVDeposit;
        public int ChartOfAccountIDAPContra;
        public int ChartOfAccountIDAPLatePayment;
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
	public class POReturns : POSConnection
	{
		#region Constructors and Destructors

		public POReturns()
            : base(null, null)
        {
        }

        public POReturns(MySqlConnection Connection, MySqlTransaction Transaction) 
            : base(Connection, Transaction)
		{

		}

		#endregion

		#region Insert and Update: Insert, Update, Post

		public long Insert(POReturnDetails Details)
		{
			try 
			{
                ERPConfig clsERPConfig = new ERPConfig(base.Connection, base.Transaction);
                APLinkConfigDetails clsAPLinkConfigDetails = clsERPConfig.APLinkDetails();

				string SQL = "INSERT INTO tblPODebitMemo (" +
								"MemoNo, " +
								"MemoDate, " +
								"SupplierID, " +
								"SupplierCode, " +
								"SupplierContact, " +
								"SupplierAddress, " +
								"SupplierTelephoneNo, " +
								"SupplierModeOfTerms, " +
								"SupplierTerms, " +
								"RequiredPostingDate, " +
								"BranchID, " +
								"PurchaserID, " +
                                "PurchaserName, " +
                                "POReturnStatus, " +
                                "DebitMemoStatus, " +
                                "Remarks, " +
                                "ChartOfAccountIDAPTracking, " +
                                "ChartOfAccountIDAPBills, " +
                                "ChartOfAccountIDAPFreight, " +
                                "ChartOfAccountIDAPVDeposit, " +
                                "ChartOfAccountIDAPContra, " +
                                "ChartOfAccountIDAPLatePayment" +
							") VALUES (" +
								"@MemoNo, " +
								"@MemoDate, " +
								"@SupplierID, " +
								"@SupplierCode, " +
								"@SupplierContact, " +
								"@SupplierAddress, " +
								"@SupplierTelephoneNo, " +
								"@SupplierModeOfTerms, " +
								"@SupplierTerms, " +
								"@RequiredPostingDate, " +
								"@BranchID, " +
                                "@PurchaserID, " +
                                "@PurchaserName, " +
                                "@POReturnStatus, " +
                                "@DebitMemoStatus, " +
                                "@Remarks, " +
                                "@ChartOfAccountIDAPTracking, " +
                                "@ChartOfAccountIDAPBills, " +
                                "@ChartOfAccountIDAPFreight, " +
                                "@ChartOfAccountIDAPVDeposit, " +
                                "@ChartOfAccountIDAPContra, " +
                                "@ChartOfAccountIDAPLatePayment" +
							");";
				  
				MySqlCommand cmd = new MySqlCommand();
				cmd.CommandType = System.Data.CommandType.Text;
				cmd.CommandText = SQL;
				
				MySqlParameter prmReturnNo = new MySqlParameter("@MemoNo",MySqlDbType.String);
				prmReturnNo.Value = Details.MemoNo;
				cmd.Parameters.Add(prmReturnNo);

				MySqlParameter prmReturnDate = new MySqlParameter("@MemoDate",MySqlDbType.DateTime);
				prmReturnDate.Value = Details.MemoDate.ToString("yyyy-MM-dd HH:mm:ss");
				cmd.Parameters.Add(prmReturnDate);

				MySqlParameter prmSupplierID = new MySqlParameter("@SupplierID",MySqlDbType.Int64);						
				prmSupplierID.Value = Details.SupplierID;
				cmd.Parameters.Add(prmSupplierID);
								 
				MySqlParameter prmSupplierCode = new MySqlParameter("@SupplierCode",MySqlDbType.String);
				prmSupplierCode.Value = Details.SupplierCode;
				cmd.Parameters.Add(prmSupplierCode);
		 
				MySqlParameter prmSupplierContact = new MySqlParameter("@SupplierContact",MySqlDbType.String);
				prmSupplierContact.Value = Details.SupplierContact;
				cmd.Parameters.Add(prmSupplierContact);			 
				
				MySqlParameter prmSupplierAddress = new MySqlParameter("@SupplierAddress",MySqlDbType.String);
				prmSupplierAddress.Value = Details.SupplierAddress;
				cmd.Parameters.Add(prmSupplierAddress);	
				
				MySqlParameter prmSupplierTelephoneNo = new MySqlParameter("@SupplierTelephoneNo",MySqlDbType.String);
				prmSupplierTelephoneNo.Value = Details.SupplierTelephoneNo;
				cmd.Parameters.Add(prmSupplierTelephoneNo);		 

				MySqlParameter prmSupplierModeOfTerms = new MySqlParameter("@SupplierModeOfTerms",MySqlDbType.Int16);
				prmSupplierModeOfTerms.Value = Details.SupplierModeOfTerms;
				cmd.Parameters.Add(prmSupplierModeOfTerms);	

				MySqlParameter prmSupplierTerms = new MySqlParameter("@SupplierTerms",MySqlDbType.Int16);
				prmSupplierTerms.Value = Details.SupplierTerms;
				cmd.Parameters.Add(prmSupplierTerms);	
			 
				MySqlParameter prmRequiredPostingDate = new MySqlParameter("@RequiredPostingDate",MySqlDbType.DateTime);
				prmRequiredPostingDate.Value = Details.RequiredPostingDate.ToString("yyyy-MM-dd HH:mm:ss");
				cmd.Parameters.Add(prmRequiredPostingDate);
	 
				MySqlParameter prmBranchID = new MySqlParameter("@BranchID",MySqlDbType.Int16);
				prmBranchID.Value = Details.BranchID;
				cmd.Parameters.Add(prmBranchID);				 
				
				MySqlParameter prmPurchaserID = new MySqlParameter("@PurchaserID",MySqlDbType.Int64);						
				prmPurchaserID.Value = Details.PurchaserID;
				cmd.Parameters.Add(prmPurchaserID);

                MySqlParameter prmPurchaserName = new MySqlParameter("@PurchaserName",MySqlDbType.String);
                prmPurchaserName.Value = Details.PurchaserName;
                cmd.Parameters.Add(prmPurchaserName);
								 
				MySqlParameter prmPOReturnStatus = new MySqlParameter("@POReturnStatus",MySqlDbType.Int16);			
				prmPOReturnStatus.Value = Details.ReturnStatus.ToString("d");
				cmd.Parameters.Add(prmPOReturnStatus);

				MySqlParameter prmDebitMemoStatus = new MySqlParameter("@DebitMemoStatus",MySqlDbType.Int16);			
				prmDebitMemoStatus.Value = DebitMemoStatus.Posted.ToString("d");
				cmd.Parameters.Add(prmDebitMemoStatus);

				MySqlParameter prmRemarks = new MySqlParameter("@Remarks",MySqlDbType.String);			
				prmRemarks.Value = Details.Remarks;
				cmd.Parameters.Add(prmRemarks);

                MySqlParameter prmChartOfAccountIDAPTracking = new MySqlParameter("@ChartOfAccountIDAPTracking",MySqlDbType.Int32);
                prmChartOfAccountIDAPTracking.Value = clsAPLinkConfigDetails.ChartOfAccountIDAPTracking;
                cmd.Parameters.Add(prmChartOfAccountIDAPTracking);

                MySqlParameter prmChartOfAccountIDAPBills = new MySqlParameter("@ChartOfAccountIDAPBills",MySqlDbType.Int32);
                prmChartOfAccountIDAPBills.Value = clsAPLinkConfigDetails.ChartOfAccountIDAPBills;
                cmd.Parameters.Add(prmChartOfAccountIDAPBills);

                MySqlParameter prmChartOfAccountIDAPFreight = new MySqlParameter("@ChartOfAccountIDAPFreight",MySqlDbType.Int32);
                prmChartOfAccountIDAPFreight.Value = clsAPLinkConfigDetails.ChartOfAccountIDAPFreight;
                cmd.Parameters.Add(prmChartOfAccountIDAPFreight);

                MySqlParameter prmChartOfAccountIDAPVDeposit = new MySqlParameter("@ChartOfAccountIDAPVDeposit",MySqlDbType.Int32);
                prmChartOfAccountIDAPVDeposit.Value = clsAPLinkConfigDetails.ChartOfAccountIDAPVDeposit;
                cmd.Parameters.Add(prmChartOfAccountIDAPVDeposit);

                MySqlParameter prmChartOfAccountIDAPContra = new MySqlParameter("@ChartOfAccountIDAPContra",MySqlDbType.Int32);
                prmChartOfAccountIDAPContra.Value = clsAPLinkConfigDetails.ChartOfAccountIDAPContra;
                cmd.Parameters.Add(prmChartOfAccountIDAPContra);

                MySqlParameter prmChartOfAccountIDAPLatePayment = new MySqlParameter("@ChartOfAccountIDAPLatePayment",MySqlDbType.Int32);
                prmChartOfAccountIDAPLatePayment.Value = clsAPLinkConfigDetails.ChartOfAccountIDAPLatePayment;
                cmd.Parameters.Add(prmChartOfAccountIDAPLatePayment);

				base.ExecuteNonQuery(cmd);

                SQL = "SELECT LAST_INSERT_ID();";

                cmd.Parameters.Clear();
                cmd.CommandText = SQL;

                string strDataTableName = "tbl" + this.GetType().FullName.Split(new Char[] { '.' })[this.GetType().FullName.Split(new Char[] { '.' }).Length - 1]; System.Data.DataTable dt = new System.Data.DataTable(strDataTableName);
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

		public void Update(POReturnDetails Details)
		{
			try 
			{
                ERPConfig clsERPConfig = new ERPConfig(base.Connection, base.Transaction);
                APLinkConfigDetails clsAPLinkConfigDetails = clsERPConfig.APLinkDetails();

				string SQL=	"UPDATE tblPODebitMemo SET " + 
								"MemoNo				=	@MemoNo, " +
								"MemoDate				=	@MemoDate, " +
								"SupplierID				=	@SupplierID, " +
								"SupplierCode			=	@SupplierCode, " +
								"SupplierContact		=	@SupplierContact, " +
								"SupplierAddress		=	@SupplierAddress, " +
								"SupplierTelephoneNo	=	@SupplierTelephoneNo, " +
								"SupplierModeOfTerms	=	@SupplierModeOfTerms, " +
								"SupplierTerms			=	@SupplierTerms, " +
								"RequiredPostingDate	=	@RequiredPostingDate, " +
                                "BranchID				=	@BranchID, " +
                                "PurchaserID			=	@PurchaserID, " +
                                "PurchaserName          =   @PurchaserName, " +
                                "Remarks                =   @Remarks, " +
                                "ChartOfAccountIDAPTracking     = @ChartOfAccountIDAPTracking, " +
                                "ChartOfAccountIDAPBills        = @ChartOfAccountIDAPBills, " +
                                "ChartOfAccountIDAPFreight      = @ChartOfAccountIDAPFreight, " +
                                "ChartOfAccountIDAPVDeposit     = @ChartOfAccountIDAPVDeposit, " +
                                "ChartOfAccountIDAPContra       = @ChartOfAccountIDAPContra, " +
                                "ChartOfAccountIDAPLatePayment  = @ChartOfAccountIDAPLatePayment " +
							"WHERE DebitMemoID = @DebitMemoID;";
				  
				MySqlCommand cmd = new MySqlCommand();
				cmd.CommandType = System.Data.CommandType.Text;
				cmd.CommandText = SQL;
				
				MySqlParameter prmReturnNo = new MySqlParameter("@MemoNo",MySqlDbType.String);
				prmReturnNo.Value = Details.MemoNo;
				cmd.Parameters.Add(prmReturnNo);

				MySqlParameter prmReturnDate = new MySqlParameter("@MemoDate",MySqlDbType.DateTime);
				prmReturnDate.Value = Details.MemoDate.ToString("yyyy-MM-dd HH:mm:ss");
				cmd.Parameters.Add(prmReturnDate);

				MySqlParameter prmSupplierID = new MySqlParameter("@SupplierID",MySqlDbType.Int64);						
				prmSupplierID.Value = Details.SupplierID;
				cmd.Parameters.Add(prmSupplierID);
								 
				MySqlParameter prmSupplierCode = new MySqlParameter("@SupplierCode",MySqlDbType.String);
				prmSupplierCode.Value = Details.SupplierCode;
				cmd.Parameters.Add(prmSupplierCode);
		 
				MySqlParameter prmSupplierContact = new MySqlParameter("@SupplierContact",MySqlDbType.String);
				prmSupplierContact.Value = Details.SupplierContact;
				cmd.Parameters.Add(prmSupplierContact);			 
				
				MySqlParameter prmSupplierAddress = new MySqlParameter("@SupplierAddress",MySqlDbType.String);
				prmSupplierAddress.Value = Details.SupplierAddress;
				cmd.Parameters.Add(prmSupplierAddress);	
				
				MySqlParameter prmSupplierTelephoneNo = new MySqlParameter("@SupplierTelephoneNo",MySqlDbType.String);
				prmSupplierTelephoneNo.Value = Details.SupplierTelephoneNo;
				cmd.Parameters.Add(prmSupplierTelephoneNo);	

				MySqlParameter prmSupplierModeOfTerms = new MySqlParameter("@SupplierModeOfTerms",MySqlDbType.Int16);
				prmSupplierModeOfTerms.Value = Details.SupplierModeOfTerms;
				cmd.Parameters.Add(prmSupplierModeOfTerms);	

				MySqlParameter prmSupplierTerms = new MySqlParameter("@SupplierTerms",MySqlDbType.Int16);
				prmSupplierTerms.Value = Details.SupplierTerms;
				cmd.Parameters.Add(prmSupplierTerms);			 
							 
				MySqlParameter prmRequiredPostingDate = new MySqlParameter("@RequiredPostingDate",MySqlDbType.DateTime);
				prmRequiredPostingDate.Value = Details.RequiredPostingDate.ToString("yyyy-MM-dd HH:mm:ss");
				cmd.Parameters.Add(prmRequiredPostingDate);

                MySqlParameter prmBranchID = new MySqlParameter("@BranchID",MySqlDbType.Int16);
                prmBranchID.Value = Details.BranchID;
                cmd.Parameters.Add(prmBranchID);

                MySqlParameter prmPurchaserID = new MySqlParameter("@PurchaserID",MySqlDbType.Int64);
                prmPurchaserID.Value = Details.PurchaserID;
                cmd.Parameters.Add(prmPurchaserID);

                MySqlParameter prmPurchaserName = new MySqlParameter("@PurchaserName",MySqlDbType.String);
                prmPurchaserName.Value = Details.PurchaserName;
                cmd.Parameters.Add(prmPurchaserName);

                MySqlParameter prmRemarks = new MySqlParameter("@Remarks",MySqlDbType.String);
                prmRemarks.Value = Details.Remarks;
                cmd.Parameters.Add(prmRemarks);

                MySqlParameter prmChartOfAccountIDAPTracking = new MySqlParameter("@ChartOfAccountIDAPTracking",MySqlDbType.Int32);
                prmChartOfAccountIDAPTracking.Value = clsAPLinkConfigDetails.ChartOfAccountIDAPTracking;
                cmd.Parameters.Add(prmChartOfAccountIDAPTracking);

                MySqlParameter prmChartOfAccountIDAPBills = new MySqlParameter("@ChartOfAccountIDAPBills",MySqlDbType.Int32);
                prmChartOfAccountIDAPBills.Value = clsAPLinkConfigDetails.ChartOfAccountIDAPBills;
                cmd.Parameters.Add(prmChartOfAccountIDAPBills);

                MySqlParameter prmChartOfAccountIDAPFreight = new MySqlParameter("@ChartOfAccountIDAPFreight",MySqlDbType.Int32);
                prmChartOfAccountIDAPFreight.Value = clsAPLinkConfigDetails.ChartOfAccountIDAPFreight;
                cmd.Parameters.Add(prmChartOfAccountIDAPFreight);

                MySqlParameter prmChartOfAccountIDAPVDeposit = new MySqlParameter("@ChartOfAccountIDAPVDeposit",MySqlDbType.Int32);
                prmChartOfAccountIDAPVDeposit.Value = clsAPLinkConfigDetails.ChartOfAccountIDAPVDeposit;
                cmd.Parameters.Add(prmChartOfAccountIDAPVDeposit);

                MySqlParameter prmChartOfAccountIDAPContra = new MySqlParameter("@ChartOfAccountIDAPContra",MySqlDbType.Int32);
                prmChartOfAccountIDAPContra.Value = clsAPLinkConfigDetails.ChartOfAccountIDAPContra;
                cmd.Parameters.Add(prmChartOfAccountIDAPContra);

                MySqlParameter prmChartOfAccountIDAPLatePayment = new MySqlParameter("@ChartOfAccountIDAPLatePayment",MySqlDbType.Int32);
                prmChartOfAccountIDAPLatePayment.Value = clsAPLinkConfigDetails.ChartOfAccountIDAPLatePayment;
                cmd.Parameters.Add(prmChartOfAccountIDAPLatePayment);

				MySqlParameter prmDebitMemoID = new MySqlParameter("@DebitMemoID",MySqlDbType.Int64);						
				prmDebitMemoID.Value = Details.DebitMemoID;
				cmd.Parameters.Add(prmDebitMemoID);

				base.ExecuteNonQuery(cmd);
			}

			catch (Exception ex)
			{
				throw base.ThrowException(ex);
			}	
		}
        public void UpdateIsVatInclusive(long DebitMemoID, bool IsVatInclusive)
        {
            try
            {
                string SQL = "UPDATE tblPODebitMemo SET " +
                                "IsVatInclusive          =   @IsVatInclusive " +
                            "WHERE DebitMemoID = @DebitMemoID;";

                

                MySqlCommand cmd = new MySqlCommand();
                
                
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandText = SQL;

                MySqlParameter prmIsVatInclusive = new MySqlParameter("@IsVatInclusive",MySqlDbType.Int16);
                prmIsVatInclusive.Value = Convert.ToInt16(IsVatInclusive); ;
                cmd.Parameters.Add(prmIsVatInclusive);

                MySqlParameter prmDebitMemoID = new MySqlParameter("@DebitMemoID",MySqlDbType.Int64);
                prmDebitMemoID.Value = DebitMemoID;
                cmd.Parameters.Add(prmDebitMemoID);

                base.ExecuteNonQuery(cmd);

                SynchronizeAmount(DebitMemoID);
            }

            catch (Exception ex)
            {
                throw base.ThrowException(ex);
            }
        }
        public void UpdateDiscount(long DebitMemoID, decimal DiscountApplied, DiscountTypes DiscountType, decimal Discount2Applied, DiscountTypes Discount2Type, decimal Discount3Applied, DiscountTypes Discount3Type)
        {
            try
            {
                string SQL = "UPDATE tblPODebitMemo SET " +
                                "DiscountApplied        =   @DiscountApplied, " +
                                "DiscountType           =   @DiscountType, " +
                                "Discount2Applied       =   @Discount2Applied, " +
                                "Discount2Type          =   @Discount2Type, " +
                                "Discount3Applied       =   @Discount3Applied, " +
                                "Discount3Type          =   @Discount3Type " +
                            "WHERE DebitMemoID = @DebitMemoID;";

                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandText = SQL;

                MySqlParameter prmDiscountApplied = new MySqlParameter("@DiscountApplied",MySqlDbType.Decimal);
                prmDiscountApplied.Value = DiscountApplied;
                cmd.Parameters.Add(prmDiscountApplied);

                MySqlParameter prmDiscountType = new MySqlParameter("@DiscountType",MySqlDbType.Int16);
                prmDiscountType.Value = Convert.ToInt16(DiscountType.ToString("d"));
                cmd.Parameters.Add(prmDiscountType);

                MySqlParameter prmDiscount2Applied = new MySqlParameter("@Discount2Applied",MySqlDbType.Decimal);
                prmDiscount2Applied.Value = Discount2Applied;
                cmd.Parameters.Add(prmDiscount2Applied);

                MySqlParameter prmDiscount2Type = new MySqlParameter("@Discount2Type",MySqlDbType.Int16);
                prmDiscount2Type.Value = Convert.ToInt16(Discount2Type.ToString("d"));
                cmd.Parameters.Add(prmDiscount2Type);

                MySqlParameter prmDiscount3Applied = new MySqlParameter("@Discount3Applied",MySqlDbType.Decimal);
                prmDiscount3Applied.Value = Discount3Applied;
                cmd.Parameters.Add(prmDiscount3Applied);

                MySqlParameter prmDiscount3Type = new MySqlParameter("@Discount3Type",MySqlDbType.Int16);
                prmDiscount3Type.Value = Convert.ToInt16(Discount3Type.ToString("d"));
                cmd.Parameters.Add(prmDiscount3Type);

                MySqlParameter prmDebitMemoID = new MySqlParameter("@DebitMemoID",MySqlDbType.Int64);
                prmDebitMemoID.Value = DebitMemoID;
                cmd.Parameters.Add(prmDebitMemoID);

                base.ExecuteNonQuery(cmd);
            }

            catch (Exception ex)
            {
                throw base.ThrowException(ex);
            }
        }
        public void UpdateDiscountFreightDeposit(long DebitMemoID, decimal DiscountApplied, DiscountTypes DiscountType)
        {
            try
            {
                string SQL = "UPDATE tblPODebitMemo SET " +
                                "DiscountApplied        =   @DiscountApplied, " +
                                "DiscountType           =   @DiscountType " +
                            "WHERE DebitMemoID = @DebitMemoID;";

                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandText = SQL;

                MySqlParameter prmDiscountApplied = new MySqlParameter("@DiscountApplied",MySqlDbType.Decimal);
                prmDiscountApplied.Value = DiscountApplied;
                cmd.Parameters.Add(prmDiscountApplied);

                MySqlParameter prmDiscountType = new MySqlParameter("@DiscountType",MySqlDbType.Int16);
                prmDiscountType.Value = Convert.ToInt16(DiscountType.ToString("d"));
                cmd.Parameters.Add(prmDiscountType);

                MySqlParameter prmDebitMemoID = new MySqlParameter("@DebitMemoID",MySqlDbType.Int64);
                prmDebitMemoID.Value = DebitMemoID;
                cmd.Parameters.Add(prmDebitMemoID);

                base.ExecuteNonQuery(cmd);
            }

            catch (Exception ex)
            {
                throw base.ThrowException(ex);
            }
        }
        public void UpdateFreight(long DebitMemoID, decimal Freight)
        {
            try
            {
                string SQL = "UPDATE tblPODebitMemo SET " +
                                "Freight           =   @Freight " +
                            "WHERE DebitMemoID = @DebitMemoID;";

                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandText = SQL;

                MySqlParameter prmFreight = new MySqlParameter("@Freight",MySqlDbType.Decimal);
                prmFreight.Value = Freight;
                cmd.Parameters.Add(prmFreight);

                MySqlParameter prmDebitMemoID = new MySqlParameter("@DebitMemoID",MySqlDbType.Int64);
                prmDebitMemoID.Value = DebitMemoID;
                cmd.Parameters.Add(prmDebitMemoID);

                base.ExecuteNonQuery(cmd);
            }

            catch (Exception ex)
            {
                throw base.ThrowException(ex);
            }
        }
        public void UpdateDeposit(long DebitMemoID, decimal Deposit)
        {
            try
            {
                string SQL = "UPDATE tblPODebitMemo SET " +
                                "Deposit           =   @Deposit " +
                            "WHERE DebitMemoID = @DebitMemoID;";

                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandText = SQL;

                MySqlParameter prmDeposit = new MySqlParameter("@Deposit",MySqlDbType.Decimal);
                prmDeposit.Value = Deposit;
                cmd.Parameters.Add(prmDeposit);

                MySqlParameter prmDebitMemoID = new MySqlParameter("@DebitMemoID",MySqlDbType.Int64);
                prmDebitMemoID.Value = DebitMemoID;
                cmd.Parameters.Add(prmDebitMemoID);

                base.ExecuteNonQuery(cmd);
            }

            catch (Exception ex)
            {
                throw base.ThrowException(ex);
            }
        }

		public void Post(long DebitMemoID, string SupplierDocNo, DateTime PostingDate)
		{
			try 
			{
				string SQL=	"UPDATE tblPODebitMemo SET " + 
								"SupplierDocNo		=	@SupplierDocNo, " +
								"PostingDate		=	@PostingDate, " +
								"POReturnStatus		=	@POReturnStatus, " +
								"DebitMemoStatus	=	@DebitMemoStatus " +
							"WHERE DebitMemoID = @DebitMemoID;";
				  
				MySqlCommand cmd = new MySqlCommand();
				cmd.CommandType = System.Data.CommandType.Text;
				cmd.CommandText = SQL;
				
				MySqlParameter prmSupplierDocNo = new MySqlParameter("@SupplierDocNo",MySqlDbType.String);
				prmSupplierDocNo.Value = SupplierDocNo;
				cmd.Parameters.Add(prmSupplierDocNo);

				MySqlParameter prmPostingDate = new MySqlParameter("@PostingDate",MySqlDbType.DateTime);
				prmPostingDate.Value = PostingDate.ToString("yyyy-MM-dd HH:mm:ss");
				cmd.Parameters.Add(prmPostingDate);

				MySqlParameter prmPOReturnStatus = new MySqlParameter("@POReturnStatus",MySqlDbType.Int16);
				prmPOReturnStatus.Value = POReturnStatus.Posted.ToString("d");
				cmd.Parameters.Add(prmPOReturnStatus);

				MySqlParameter prmDebitMemoStatus = new MySqlParameter("@DebitMemoStatus",MySqlDbType.Int16);
				prmDebitMemoStatus.Value = DebitMemoStatus.Posted.ToString("d");
				cmd.Parameters.Add(prmDebitMemoStatus);

				MySqlParameter prmDebitMemoID = new MySqlParameter("@DebitMemoID",MySqlDbType.Int64);						
				prmDebitMemoID.Value = DebitMemoID;
				cmd.Parameters.Add(prmDebitMemoID);

				base.ExecuteNonQuery(cmd);

				/*******************************************
				 * Update the status of items
				 * ****************************************/
				POReturnItems clsPOReturnItems = new POReturnItems(base.Connection, base.Transaction);
				clsPOReturnItems.Post(DebitMemoID);

				/*******************************************
				 * Update Vendor Account
				 * ****************************************/
				AddItemToInventory(DebitMemoID);
			}

			catch (Exception ex)
			{
				throw base.ThrowException(ex);
			}	
		}

        private void UpdateAccounts(long DebitMemoID)
        {
            try
            {
                POReturnDetails clsPOReturnDetails = Details(DebitMemoID);
                ChartOfAccount clsChartOfAccount = new ChartOfAccount(base.Connection, base.Transaction);

                // update ChartOfAccountIDAPTracking as credit
                clsChartOfAccount.UpdateCredit(clsPOReturnDetails.ChartOfAccountIDAPTracking, clsPOReturnDetails.SubTotal);

                // update Deposit & APContra
                clsChartOfAccount.UpdateDebit(clsPOReturnDetails.ChartOfAccountIDAPContra, clsPOReturnDetails.Discount);

                // update Freight & APTracking
                clsChartOfAccount.UpdateDebit(clsPOReturnDetails.ChartOfAccountIDAPTracking, clsPOReturnDetails.Freight);
                clsChartOfAccount.UpdateCredit(clsPOReturnDetails.ChartOfAccountIDAPFreight, clsPOReturnDetails.Freight);

                // update Deposit & APTracking
                clsChartOfAccount.UpdateDebit(clsPOReturnDetails.ChartOfAccountIDAPTracking, clsPOReturnDetails.Deposit);
                clsChartOfAccount.UpdateCredit(clsPOReturnDetails.ChartOfAccountIDAPVDeposit, clsPOReturnDetails.Deposit);

                POReturnItems clsPOReturnItems = new POReturnItems(base.Connection, base.Transaction);
                System.Data.DataTable dt = clsPOReturnItems.ListAsDataTable(DebitMemoID);

                foreach (System.Data.DataRow dr in dt.Rows)
                {
                    int iChartOfAccountIDPurchase = Int16.Parse(dr["ChartOfAccountIDPurchase"].ToString());
                    int iChartOfAccountIDTaxPurchase = Int16.Parse(dr["ChartOfAccountIDTaxPurchase"].ToString());

                    decimal decVAT = decimal.Parse(dr["VAT"].ToString());
                    decimal decVATABLEAmount = decimal.Parse(dr["Amount"].ToString()) - decVAT;

                    // update purchase as debit
                    clsChartOfAccount.UpdateCredit(iChartOfAccountIDPurchase, decVATABLEAmount);
                    // update tax as debit
                    clsChartOfAccount.UpdateCredit(iChartOfAccountIDTaxPurchase, decVAT);

                }

            }

            catch (Exception ex)
            {
                throw base.ThrowException(ex);
            }
        }

		private void AddItemToInventory(long DebitMemoID)
		{

			POReturnDetails clsPOReturnDetails = Details(DebitMemoID);
			ERPConfig clsERPConfig = new ERPConfig(base.Connection, base.Transaction);
			ERPConfigDetails clsERPConfigDetails = clsERPConfig.Details();

			POReturnItems clsPOReturnItems = new POReturnItems(base.Connection, base.Transaction);
			ProductUnit clsProductUnit = new ProductUnit(base.Connection, base.Transaction);
			Products clsProduct = new Products(base.Connection, base.Transaction);
			ProductVariationsMatrix clsProductVariationsMatrix = new ProductVariationsMatrix(base.Connection, base.Transaction);

			Inventory clsInventory = new Inventory(base.Connection, base.Transaction);

			//MySqlDataReader myReader = clsPOReturnItems.List(DebitMemoID, "DebitMemoItemID", SortOption.Ascending);
            System.Data.DataTable dt = clsPOReturnItems.ListAsDataTable(DebitMemoID, "DebitMemoItemID", SortOption.Ascending);

            foreach (System.Data.DataRow dr in dt.Rows)
			{
                long lngProductID = Convert.ToInt64(dr["ProductID"]); // myReader.GetInt64("ProductID");
                int intProductUnitID = Convert.ToInt16(dr["ProductUnitID"]); // myReader.GetInt16("ProductUnitID");

                decimal decItemQuantity = Convert.ToDecimal(dr["Quantity"]); // myReader.GetDecimal("Quantity");
                decimal decQuantity = clsProductUnit.GetBaseUnitValue(lngProductID, intProductUnitID, decItemQuantity);

                long lngVariationMatrixID = Convert.ToInt64(dr["VariationMatrixID"]); // myReader.GetInt64("VariationMatrixID");
                string strMatrixDescription = dr["MatrixDescription"].ToString(); //  "" + myReader["MatrixDescription"].ToString();
                string strProductCode = dr["ProductCode"].ToString(); // "" + myReader["ProductCode"].ToString();
                string strProductUnitCode = dr["ProductUnitCode"].ToString(); // "" + myReader["ProductUnitCode"].ToString();
                decimal decUnitCost = Convert.ToDecimal(dr["UnitCost"]); // myReader.GetDecimal("UnitCost");
                decimal decItemCost = Convert.ToDecimal(dr["Amount"]); // myReader.GetDecimal("Amount");
                decimal decVAT = Convert.ToDecimal(dr["VAT"]); // myReader.GetDecimal("VAT");

				/*******************************************
				 * Subtract from Inventory
				 * ****************************************/
                // clsProduct.SubtractQuantity(ProductID, Quantity);
                // if (VariationMatrixID != 0) { clsProductVariationsMatrix.SubtractQuantity(VariationMatrixID, Quantity);}
                // July 26, 2011: change the above codes to the following
                clsProduct.SubtractQuantity(clsPOReturnDetails.BranchID, lngProductID, lngVariationMatrixID, decQuantity, Products.getPRODUCT_INVENTORY_MOVEMENT_VALUE(PRODUCT_INVENTORY_MOVEMENT.DEDUCT_PURCHASE_RETURN), DateTime.Now, clsPOReturnDetails.MemoNo, clsPOReturnDetails.PurchaserName);

				/*******************************************
				 * Add to Inventory Analysis
				 * ****************************************/
				InventoryDetails clsInventoryDetails = new InventoryDetails();
				clsInventoryDetails.PostingDateFrom = clsERPConfigDetails.PostingDateFrom;
				clsInventoryDetails.PostingDateTo = clsERPConfigDetails.PostingDateTo;
				clsInventoryDetails.PostingDate = clsPOReturnDetails.PostingDate;
				clsInventoryDetails.ReferenceNo = clsPOReturnDetails.MemoNo;
				clsInventoryDetails.ContactID = clsPOReturnDetails.SupplierID;
				clsInventoryDetails.ContactCode = clsPOReturnDetails.SupplierCode;
                clsInventoryDetails.ProductID = lngProductID;
                clsInventoryDetails.ProductCode = strProductCode;
				clsInventoryDetails.VariationMatrixID = lngVariationMatrixID;
                clsInventoryDetails.MatrixDescription = strMatrixDescription;
				clsInventoryDetails.PReturnQuantity = decQuantity;
                clsInventoryDetails.PReturnCost = decItemCost - decVAT;
                clsInventoryDetails.PReturnVAT = decItemCost;	//Purchase Return with VAT

				clsInventory.Insert(clsInventoryDetails);

			}

		}

		public void Cancel(long DebitMemoID, DateTime CancelledDate, string Remarks, long CancelledByID)
		{
			try 
			{
				string SQL=	"UPDATE tblPODebitMemo SET " + 
								"CancelledDate			=	@CancelledDate, " +
								"CancelledRemarks		=	@CancelledRemarks, " +
								"CancelledByID			=	@CancelledByID, " +
								"POReturnStatus			=	@POReturnStatus " +
							"WHERE DebitMemoID = @DebitMemoID;";
				  
				MySqlCommand cmd = new MySqlCommand();
				cmd.CommandType = System.Data.CommandType.Text;
				cmd.CommandText = SQL;
				
				MySqlParameter prmCancelledDate = new MySqlParameter("@CancelledDate",MySqlDbType.DateTime);
				prmCancelledDate.Value = CancelledDate.ToString("yyyy-MM-dd HH:mm:ss");
				cmd.Parameters.Add(prmCancelledDate);

				MySqlParameter prmCancelledRemarks = new MySqlParameter("@CancelledRemarks",MySqlDbType.String);
				prmCancelledRemarks.Value = Remarks;
				cmd.Parameters.Add(prmCancelledRemarks);

				MySqlParameter prmCancelledByID = new MySqlParameter("@CancelledByID",MySqlDbType.Int64);						
				prmCancelledByID.Value = CancelledByID;
				cmd.Parameters.Add(prmCancelledByID);

				MySqlParameter prmPOReturnStatus = new MySqlParameter("@POReturnStatus",MySqlDbType.Int16);
				prmPOReturnStatus.Value = POReturnStatus.Cancelled.ToString("d");
				cmd.Parameters.Add(prmPOReturnStatus);

				MySqlParameter prmDebitMemoID = new MySqlParameter("@DebitMemoID",MySqlDbType.Int64);						
				prmDebitMemoID.Value = DebitMemoID;
				cmd.Parameters.Add(prmDebitMemoID);

				base.ExecuteNonQuery(cmd);

				/*******************************************
				 * Update the status of items
				 * ****************************************/
				POReturnItems clsPOReturnItems = new POReturnItems(base.Connection, base.Transaction);
				clsPOReturnItems.Cancel(DebitMemoID);

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
				string SQL=	"DELETE FROM tblPODebitMemo WHERE DebitMemoID IN (" + IDs + ");";
				  
				
	 			
				MySqlCommand cmd = new MySqlCommand();
				
				
				cmd.CommandType = System.Data.CommandType.Text;
				cmd.CommandText = SQL;

				base.ExecuteNonQuery(cmd);

				return true;
			}

			catch (Exception ex)
			{
				
				
				{
					
					
					
					
				}

				throw base.ThrowException(ex);
			}	
		}


		#endregion

        private string SQLSelect()
        {
            string SQL = "SELECT " +
                            "DebitMemoID, " +
                            "MemoNo, " +
                            "MemoDate, " +
                            "SupplierID, " +
                            "SupplierCode, " +
                            "SupplierContact, " +
                            "SupplierAddress, " +
                            "SupplierTelephoneNo, " +
                            "SupplierModeOfTerms, " +
                            "SupplierTerms, " +
                            "RequiredPostingDate, " +
                            "a.BranchID, " +
                            "BranchCode, " +
                            "BranchName, " +
                            "b.Address BranchAddress, " +
                            "PurchaserID, " +
                            "PurchaserName, " +
                            "SubTotal, " +
                            "Discount, " +
                            "DiscountApplied, " +
                            "DiscountType, " +
                            "Discount2, " +
                            "Discount2Applied, " +
                            "Discount2Type, " +
                            "Discount3, " +
                            "Discount3Applied, " +
                            "Discount3Type, " +
                            "VAT, " +
                            "VatableAmount, " +
                            "EVAT, " +
                            "EVatableAmount, " +
                            "LocalTax, " +
                            "Freight, " +
                            "Deposit, " +
                            "PaidAmount, " +
                            "UnpaidAmount, " +
                            "TotalItemDiscount, " +
                            "POReturnStatus, " +
                            "IsVatInclusive, " +
                            "a.Remarks, " +
                            "SupplierDocNo, " +
                            "PostingDate, " +
                            "CancelledDate, " +
                            "CancelledRemarks, " +
                            "CancelledByID, " +
                            "PaymentStatus, " +
                            "ChartOfAccountIDAPTracking, " +
                            "ChartOfAccountIDAPFreight, " +
                            "ChartOfAccountIDAPVDeposit, " +
                            "ChartOfAccountIDAPContra, " +
                            "ChartOfAccountIDAPLatePayment " +
                        "FROM tblPODebitMemo a INNER JOIN tblBranch b ON a.BranchID = b.BranchID " +
                        "WHERE LEFT(MemoNo," + Constants.PURCHASE_RETURN_CODE.Length.ToString() + ") = '" + Constants.PURCHASE_RETURN_CODE + "' ";

            return SQL;

        }

		#region Details

		public POReturnDetails Details(long DebitMemoID)
		{
			try
			{
				string SQL=	SQLSelect() + "AND DebitMemoID = @DebitMemoID;";
				  
				MySqlCommand cmd = new MySqlCommand();
				cmd.CommandType = System.Data.CommandType.Text;
				cmd.CommandText = SQL;

				MySqlParameter prmDebitMemoID = new MySqlParameter("@DebitMemoID",MySqlDbType.Int16);
				prmDebitMemoID.Value = DebitMemoID;
				cmd.Parameters.Add(prmDebitMemoID);

				MySqlDataReader myReader = base.ExecuteReader(cmd, System.Data.CommandBehavior.SingleResult);
				
				POReturnDetails Details = new POReturnDetails();

				while (myReader.Read()) 
				{
					Details.DebitMemoID = DebitMemoID;
					Details.MemoNo = "" + myReader["MemoNo"].ToString();
					Details.MemoDate = myReader.GetDateTime("MemoDate");
					Details.SupplierID = myReader.GetInt64("SupplierID");
					Details.SupplierCode = "" + myReader["SupplierCode"].ToString();
					Details.SupplierContact = "" + myReader["SupplierContact"].ToString();
					Details.SupplierAddress = "" + myReader["SupplierAddress"].ToString();
					Details.SupplierTelephoneNo = "" + myReader["SupplierTelephoneNo"].ToString();
					Details.SupplierModeOfTerms = myReader.GetInt16("SupplierModeofTerms");
					Details.SupplierTerms = myReader.GetInt16("SupplierTerms");
					Details.RequiredPostingDate = myReader.GetDateTime("RequiredPostingDate");
					Details.BranchID = myReader.GetInt16("BranchID");
					Details.BranchCode = "" + myReader["BranchCode"].ToString();
					Details.BranchName = "" + myReader["BranchName"].ToString();
					Details.BranchAddress = "" + myReader["BranchAddress"].ToString();
                    Details.PurchaserID = myReader.GetInt64("PurchaserID");
                    Details.PurchaserName = "" + myReader["PurchaserName"].ToString();
                    Details.SubTotal = myReader.GetDecimal("SubTotal");
                    Details.Discount = myReader.GetDecimal("Discount");
                    Details.DiscountApplied = myReader.GetDecimal("DiscountApplied");
                    Details.DiscountType = (DiscountTypes)Enum.Parse(typeof(DiscountTypes), myReader.GetString("DiscountType"));
                    Details.Discount2 = myReader.GetDecimal("Discount2");
                    Details.Discount2Applied = myReader.GetDecimal("Discount2Applied");
                    Details.Discount2Type = (DiscountTypes)Enum.Parse(typeof(DiscountTypes), myReader.GetString("Discount2Type"));
                    Details.Discount3 = myReader.GetDecimal("Discount3");
                    Details.Discount3Applied = myReader.GetDecimal("Discount3Applied");
                    Details.Discount3Type = (DiscountTypes)Enum.Parse(typeof(DiscountTypes), myReader.GetString("Discount3Type"));
                    Details.VAT = myReader.GetDecimal("VAT");
                    Details.VatableAmount = myReader.GetDecimal("VatableAmount");
                    Details.EVAT = myReader.GetDecimal("EVAT");
                    Details.EVatableAmount = myReader.GetDecimal("EVatableAmount");
                    Details.LocalTax = myReader.GetDecimal("LocalTax");
                    Details.Freight = myReader.GetDecimal("Freight");
                    Details.Deposit = myReader.GetDecimal("Deposit");
                    Details.PaidAmount = myReader.GetDecimal("PaidAmount");
                    Details.UnpaidAmount = myReader.GetDecimal("UnpaidAmount");
                    Details.TotalItemDiscount = myReader.GetDecimal("TotalItemDiscount");
					Details.ReturnStatus = (POReturnStatus) Enum.Parse(typeof(POReturnStatus), myReader.GetString("POReturnStatus"));
                    Details.IsVatInclusive = myReader.GetBoolean("IsVatInclusive");
					Details.Remarks = "" + myReader["Remarks"].ToString();
                    Details.SupplierDocNo = "" + myReader["SupplierDocNo"].ToString();
                    Details.PostingDate = myReader.GetDateTime("PostingDate");
                    Details.ChartOfAccountIDAPTracking = myReader.GetInt16("ChartOfAccountIDAPTracking");
                    Details.ChartOfAccountIDAPFreight = myReader.GetInt16("ChartOfAccountIDAPFreight");
                    Details.ChartOfAccountIDAPVDeposit = myReader.GetInt16("ChartOfAccountIDAPVDeposit");
                    Details.ChartOfAccountIDAPContra = myReader.GetInt16("ChartOfAccountIDAPContra");
                    Details.ChartOfAccountIDAPLatePayment = myReader.GetInt16("ChartOfAccountIDAPLatePayment");
				}

				myReader.Close();

				return Details;
			}

			catch (Exception ex)
			{
				throw base.ThrowException(ex);
			}	
		}

		#endregion

		#region Streams: ListAsDataTable, List, Search

		public System.Data.DataTable ListAsDataTable(string SortField, SortOption SortOrder)
		{
            try{
                if (SortField == string.Empty || SortField == null) SortField = "DebitMemoID";

                string SQL = SQLSelect() + "ORDER BY " + SortField;

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
            catch (Exception ex)
            {
                throw base.ThrowException(ex);
            }
		}
        public System.Data.DataTable ListAsDataTable(POReturnStatus POReturnStatus, string SortField, SortOption SortOrder)
        {
            try
            {
                if (SortField == string.Empty || SortField == null) SortField = "DebitMemoID";

                string SQL = SQLSelect() + "AND POReturnStatus = @POReturnStatus " +
                                "ORDER BY " + SortField;

                if (SortOrder == SortOption.Ascending)
                    SQL += " ASC";
                else
                    SQL += " DESC";

                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandText = SQL;

                MySqlParameter prmPOReturnStatus = new MySqlParameter("@POReturnStatus",MySqlDbType.Int16);
                prmPOReturnStatus.Value = POReturnStatus.ToString("d");
                cmd.Parameters.Add(prmPOReturnStatus);

                string strDataTableName = "tbl" + this.GetType().FullName.Split(new Char[] { '.' })[this.GetType().FullName.Split(new Char[] { '.' }).Length - 1]; System.Data.DataTable dt = new System.Data.DataTable(strDataTableName);
                base.MySqlDataAdapterFill(cmd, dt);

                return dt;
            }
            catch (Exception ex)
            {
                throw base.ThrowException(ex);
            }
        }
		public MySqlDataReader List(long DebitMemoID, string SortField, SortOption SortOrder)
		{
			try
			{
				string SQL = SQLSelect() + "AND DebitMemoID = @DebitMemoID ORDER BY " + SortField;

				if (SortOrder == SortOption.Ascending)
					SQL += " ASC";
				else
					SQL += " DESC";

				MySqlCommand cmd = new MySqlCommand();
				cmd.CommandType = System.Data.CommandType.Text;
				cmd.CommandText = SQL;
				
				MySqlParameter prmDebitMemoID = new MySqlParameter("@DebitMemoID",MySqlDbType.Int64);
				prmDebitMemoID.Value = DebitMemoID;
				cmd.Parameters.Add(prmDebitMemoID);

				MySqlDataReader myReader = base.ExecuteReader(cmd);
				
				return myReader;			
			}
			catch (Exception ex)
			{
				throw base.ThrowException(ex);
			}	
		}
		public MySqlDataReader List(string SortField, SortOption SortOrder)
		{
			try
			{
				string SQL = SQLSelect() + "ORDER BY " + SortField;

				if (SortOrder == SortOption.Ascending)
					SQL += " ASC";
				else
					SQL += " DESC";

				MySqlCommand cmd = new MySqlCommand();
				cmd.CommandType = System.Data.CommandType.Text;
				cmd.CommandText = SQL;
				
				MySqlDataReader myReader = base.ExecuteReader(cmd);
				
				return myReader;			
			}
			catch (Exception ex)
			{
				throw base.ThrowException(ex);
			}	
		}
		public MySqlDataReader List(POReturnStatus POReturnStatus, string SortField, SortOption SortOrder)
		{
			try
			{
				string SQL = SQLSelect() + "AND POReturnStatus = @POReturnStatus " +
							"ORDER BY " + SortField;

				if (SortOrder == SortOption.Ascending)
					SQL += " ASC";
				else
					SQL += " DESC";

				MySqlCommand cmd = new MySqlCommand();
				cmd.CommandType = System.Data.CommandType.Text;
				cmd.CommandText = SQL;
				
				MySqlParameter prmPOReturnStatus = new MySqlParameter("@POReturnStatus",MySqlDbType.Int16);			
				prmPOReturnStatus.Value = POReturnStatus.ToString("d");
				cmd.Parameters.Add(prmPOReturnStatus);

				MySqlDataReader myReader = base.ExecuteReader(cmd);
				
				return myReader;			
			}
			catch (Exception ex)
			{
				throw base.ThrowException(ex);
			}	
		}
		public MySqlDataReader List(POReturnStatus POReturnStatus, long SupplierID, string SortField, SortOption SortOrder)
		{
			try
			{
				string SQL = SQLSelect() + "WHERE POReturnStatus = @POReturnStatus " +
								"AND SupplierID = @SupplierID " +
							"ORDER BY " + SortField;

				if (SortOrder == SortOption.Ascending)
					SQL += " ASC";
				else
					SQL += " DESC";

				MySqlCommand cmd = new MySqlCommand();
				cmd.CommandType = System.Data.CommandType.Text;
				cmd.CommandText = SQL;
				
				MySqlParameter prmPOReturnStatus = new MySqlParameter("@POReturnStatus",MySqlDbType.Int16);			
				prmPOReturnStatus.Value = POReturnStatus.ToString("d");
				cmd.Parameters.Add(prmPOReturnStatus);

				MySqlParameter prmSupplierID = new MySqlParameter("@SupplierID",MySqlDbType.Int64);						
				prmSupplierID.Value = SupplierID;
				cmd.Parameters.Add(prmSupplierID);

				MySqlDataReader myReader = base.ExecuteReader(cmd);
				
				return myReader;			
			}
			catch (Exception ex)
			{
				throw base.ThrowException(ex);
			}	
		}
        public MySqlDataReader List(POReturnStatus POReturnStatus, DateTime StartDate, DateTime EndDate)
        {
            try
            {
                string SQL = SQLSelect() + "AND POReturnStatus = @POReturnStatus " +
                        "AND PostingDate BETWEEN @StartDate AND @EndDate " +
                    "ORDER BY DebitMemoID ASC";

                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandText = SQL;

                MySqlParameter prmPOReturnStatus = new MySqlParameter("@POReturnStatus",MySqlDbType.Int16);
                prmPOReturnStatus.Value = POReturnStatus.ToString("d");
                cmd.Parameters.Add(prmPOReturnStatus);

                MySqlParameter prmStartDate = new MySqlParameter("@StartDate",MySqlDbType.DateTime);
                prmStartDate.Value = StartDate.ToString("yyyy-MM-dd HH:mm:ss");
                cmd.Parameters.Add(prmStartDate);

                MySqlParameter prmEndDate = new MySqlParameter("@EndDate",MySqlDbType.DateTime);
                prmEndDate.Value = EndDate.ToString("yyyy-MM-dd HH:mm:ss");
                cmd.Parameters.Add(prmEndDate);

                MySqlDataReader myReader = base.ExecuteReader(cmd);

                return myReader;
            }
            catch (Exception ex)
            {
                throw base.ThrowException(ex);
            }
        }
        public MySqlDataReader List(POReturnStatus POReturnStatus, long SupplierID, DateTime StartDate, DateTime EndDate)
        {
            try
            {
                string SQL = SQLSelect() + "AND POReturnStatus = @POReturnStatus " +
                        "AND SupplierID = @SupplierID " +
                        "AND PostingDate BETWEEN @StartDate AND @EndDate " +
                    "ORDER BY DebitMemoID ASC";

                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandText = SQL;

                cmd.Parameters.AddWithValue("@POReturnStatus", POReturnStatus.ToString("d"));
                cmd.Parameters.AddWithValue("@SupplierID", SupplierID);
                cmd.Parameters.AddWithValue("@StartDate", StartDate.ToString("yyyy-MM-dd HH:mm:ss"));
                cmd.Parameters.AddWithValue("@EndDate", EndDate.ToString("yyyy-MM-dd HH:mm:ss"));

                MySqlDataReader myReader = base.ExecuteReader(cmd);

                return myReader;
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
				string SQL = SQLSelect() + "AND (MemoNo LIKE @SearchKey or MemoDate LIKE @SearchKey or SupplierCode LIKE @SearchKey " +
										"or SupplierContact LIKE @SearchKey or BranchCode LIKE @SearchKey or RequiredPostingDate LIKE @SearchKey) " +
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

				MySqlDataReader myReader = base.ExecuteReader(cmd);
				
				return myReader;			
			}
			catch (Exception ex)
			{
				throw base.ThrowException(ex);
			}	
		}		
		public MySqlDataReader Search(POReturnStatus Status, string SearchKey, string SortField, SortOption SortOrder)
		{
			try
			{
				string SQL = SQLSelect() + "AND POReturnStatus = @POReturnStatus " +
								"AND (MemoNo LIKE @SearchKey or MemoDate LIKE @SearchKey or SupplierCode LIKE @SearchKey " +
										"or SupplierContact LIKE @SearchKey or BranchCode LIKE @SearchKey or RequiredPostingDate LIKE @SearchKey) " +
							"ORDER BY " + SortField;

				if (SortOrder == SortOption.Ascending)
					SQL += " ASC";
				else
					SQL += " DESC";

				MySqlCommand cmd = new MySqlCommand();
				cmd.CommandType = System.Data.CommandType.Text;
				cmd.CommandText = SQL;
				
				MySqlParameter prmPOReturnStatus = new MySqlParameter("@POReturnStatus",MySqlDbType.Int16);			
				prmPOReturnStatus.Value = Status.ToString("d");
				cmd.Parameters.Add(prmPOReturnStatus);

				MySqlParameter prmSearchKey = new MySqlParameter("@SearchKey",MySqlDbType.String);
				prmSearchKey.Value = "%" + SearchKey + "%";
				cmd.Parameters.Add(prmSearchKey);

				MySqlDataReader myReader = base.ExecuteReader(cmd);
				
				return myReader;			
			}
			catch (Exception ex)
			{
				throw base.ThrowException(ex);
			}	
		}
        public System.Data.DataTable SearchAsDataTable(POReturnStatus Status, string SearchKey, string SortField, SortOption SortOrder)
        {
            try
            {
                string SQL = SQLSelect() + "AND POReturnStatus = @POReturnStatus " +
                                "AND (MemoNo LIKE @SearchKey or MemoDate LIKE @SearchKey or SupplierCode LIKE @SearchKey " +
                                        "or SupplierContact LIKE @SearchKey or BranchCode LIKE @SearchKey or RequiredPostingDate LIKE @SearchKey) " +
                            "ORDER BY " + SortField;

                if (SortOrder == SortOption.Ascending)
                    SQL += " ASC";
                else
                    SQL += " DESC";

                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandText = SQL;

                MySqlParameter prmPOReturnStatus = new MySqlParameter("@POReturnStatus",MySqlDbType.Int16);
                prmPOReturnStatus.Value = Status.ToString("d");
                cmd.Parameters.Add(prmPOReturnStatus);

                MySqlParameter prmSearchKey = new MySqlParameter("@SearchKey",MySqlDbType.String);
                prmSearchKey.Value = "%" + SearchKey + "%";
                cmd.Parameters.Add(prmSearchKey);

                string strDataTableName = "tbl" + this.GetType().FullName.Split(new Char[] { '.' })[this.GetType().FullName.Split(new Char[] { '.' }).Length - 1]; System.Data.DataTable dt = new System.Data.DataTable(strDataTableName);
                base.MySqlDataAdapterFill(cmd, dt);
                
                return dt;
            }
            catch (Exception ex)
            {
                throw base.ThrowException(ex);
            }
        }
        public System.Data.DataTable SearchAsDataTable(POReturnStatus status, DateTime OrderStartDate, DateTime OrderEndDate, DateTime PostingStartDate, DateTime PostingEndDate, string SearchKey, string SortField, SortOption SortOrder)
        {
            try
            {
                if (SortField == string.Empty || SortField == null) SortField = "DebitMemoID";

                string SQL = SQLSelect() + "AND POReturnStatus = @Status " +
                                "AND (MemoNo LIKE @SearchKey or MemoDate LIKE @SearchKey or SupplierCode LIKE @SearchKey " +
                                        "or SupplierContact LIKE @SearchKey or BranchCode LIKE @SearchKey or RequiredPostingDate LIKE @SearchKey) ";

                if (OrderStartDate != DateTime.MinValue) SQL += "AND MemoDate >= @OrderStartDate ";
                if (OrderEndDate != DateTime.MinValue) SQL += "AND MemoDate <= @OrderEndDate ";
                if (PostingStartDate != DateTime.MinValue) SQL += "AND PostingDate >= @PostingStartDate ";
                if (PostingEndDate != DateTime.MinValue) SQL += "AND PostingDate <= @PostingEndDate ";

                SQL += "ORDER BY " + SortField;
                
                if (SortOrder == SortOption.Ascending)
                    SQL += " ASC";
                else
                    SQL += " DESC";

                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandText = SQL;

                cmd.Parameters.AddWithValue("@Status", status.ToString("d"));
                cmd.Parameters.AddWithValue("@SearchKey", "%" + SearchKey + "%");

                if (OrderStartDate != DateTime.MinValue) cmd.Parameters.AddWithValue("@OrderStartDate", OrderStartDate.ToString("yyyy-MM-dd HH:mm:ss"));
                if (OrderEndDate != DateTime.MinValue) cmd.Parameters.AddWithValue("@OrderEndDate", OrderEndDate.ToString("yyyy-MM-dd HH:mm:ss"));
                if (PostingStartDate != DateTime.MinValue) cmd.Parameters.AddWithValue("@PostingStartDate", PostingStartDate.ToString("yyyy-MM-dd HH:mm:ss"));
                if (PostingEndDate != DateTime.MinValue) cmd.Parameters.AddWithValue("@PostingEndDate", PostingEndDate.ToString("yyyy-MM-dd HH:mm:ss"));

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

		#region Public Modifiers: LastTransactionNo, SynchronizeAmount

		public string LastTransactionNo()
		{
			try
			{
				string stRetValue = String.Empty;
				
				ERPConfig clsERPConfig = new ERPConfig(base.Connection, base.Transaction);
				stRetValue = clsERPConfig.get_LastPOReturnNo();

				return stRetValue;
			}

			catch (Exception ex)
			{
				throw base.ThrowException(ex);
			}	
		}
		public void SynchronizeAmount(long DebitMemoID)
		{
			try 
			{
                string SQL = "CALL procPODebitMemoSynchronizeAmount(@DebitMemoID);";
				  
				MySqlCommand cmd = new MySqlCommand();
				cmd.CommandType = System.Data.CommandType.Text;
				cmd.CommandText = SQL;
				
				MySqlParameter prmDebitMemoID = new MySqlParameter("@DebitMemoID",MySqlDbType.Int64);						
				prmDebitMemoID.Value = DebitMemoID;
				cmd.Parameters.Add(prmDebitMemoID);

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


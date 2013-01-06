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
	public class POReturns
	{
		MySqlConnection mConnection;
		MySqlTransaction mTransaction;
		bool IsInTransaction = false;
		bool TransactionFailed = false;

		public MySqlConnection Connection
		{
			get { return mConnection;	}
		}

		public MySqlTransaction Transaction
		{
			get { return mTransaction;	}
		}


		#region Constructors and Destructors

		public POReturns()
		{
			
		}

		public POReturns(MySqlConnection Connection, MySqlTransaction Transaction)
		{
			mConnection = Connection;
			mTransaction = Transaction;
			
		}

		public void CommitAndDispose() 
		{
			if (!TransactionFailed)
			{
				if (IsInTransaction)
				{
					mTransaction.Commit();
					mTransaction.Dispose(); 
					mConnection.Close();
					mConnection.Dispose();
				}
			}
		}

		public MySqlConnection GetConnection()
		{
			if (mConnection==null)
			{
				mConnection = new MySqlConnection(AceSoft.RetailPlus.DBConnection.ConnectionString());	
				mConnection.Open();
				
				mTransaction = (MySqlTransaction) mConnection.BeginTransaction();
			}
			
			IsInTransaction = true;
			return mConnection;
		} 


		#endregion

		#region Insert and Update: Insert, Update, Post

		public long Insert(POReturnDetails Details)
		{
			try 
			{
                ERPConfig clsERPConfig = new ERPConfig(mConnection, mTransaction);
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
				  
				MySqlConnection cn = GetConnection();
	 			
				MySqlCommand cmd = new MySqlCommand();
				cmd.Connection = cn;
				cmd.Transaction = mTransaction;
				cmd.CommandType = System.Data.CommandType.Text;
				cmd.CommandText = SQL;
				
				MySqlParameter prmReturnNo = new MySqlParameter("@MemoNo",MySqlDbType.String);
				prmReturnNo.Value = Details.MemoNo;
				cmd.Parameters.Add(prmReturnNo);

				MySqlParameter prmReturnDate = new MySqlParameter("@MemoDate",MySqlDbType.DateTime);
				prmReturnDate.Value = Details.MemoDate.ToString("yyyy-MM-dd HH:mm:ss");
				cmd.Parameters.Add(prmReturnDate);

				MySqlParameter prmSupplierID = new MySqlParameter("@SupplierID",System.Data.DbType.Int64);			
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
				
				MySqlParameter prmPurchaserID = new MySqlParameter("@PurchaserID",System.Data.DbType.Int64);			
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

				cmd.ExecuteNonQuery();

				SQL = "SELECT LAST_INSERT_ID();";
				
				cmd.Parameters.Clear(); 
				cmd.CommandText = SQL;

                System.Data.DataTable dt = new System.Data.DataTable("LAST_INSERT_ID");
                MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
                adapter.Fill(dt);

                Int64 iID = 0;
                foreach (System.Data.DataRow dr in dt.Rows)
                {
                    iID = Int64.Parse(dr[0].ToString());
                }

				return iID;
			}

			catch (Exception ex)
			{
				TransactionFailed = true;
				if (IsInTransaction)
				{
					mTransaction.Rollback();
					mTransaction.Dispose(); 
					mConnection.Close();
					mConnection.Dispose();
				}

				throw ex;
			}	
		}

		public void Update(POReturnDetails Details)
		{
			try 
			{
                ERPConfig clsERPConfig = new ERPConfig(mConnection, mTransaction);
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
				  
				MySqlConnection cn = GetConnection();
	 			
				MySqlCommand cmd = new MySqlCommand();
				cmd.Connection = cn;
				cmd.Transaction = mTransaction;
				cmd.CommandType = System.Data.CommandType.Text;
				cmd.CommandText = SQL;
				
				MySqlParameter prmReturnNo = new MySqlParameter("@MemoNo",MySqlDbType.String);
				prmReturnNo.Value = Details.MemoNo;
				cmd.Parameters.Add(prmReturnNo);

				MySqlParameter prmReturnDate = new MySqlParameter("@MemoDate",MySqlDbType.DateTime);
				prmReturnDate.Value = Details.MemoDate.ToString("yyyy-MM-dd HH:mm:ss");
				cmd.Parameters.Add(prmReturnDate);

				MySqlParameter prmSupplierID = new MySqlParameter("@SupplierID",System.Data.DbType.Int64);			
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

				MySqlParameter prmDebitMemoID = new MySqlParameter("@DebitMemoID",System.Data.DbType.Int64);			
				prmDebitMemoID.Value = Details.DebitMemoID;
				cmd.Parameters.Add(prmDebitMemoID);

				cmd.ExecuteNonQuery();
			}

			catch (Exception ex)
			{
				TransactionFailed = true;
				if (IsInTransaction)
				{
					mTransaction.Rollback();
					mTransaction.Dispose(); 
					mConnection.Close();
					mConnection.Dispose();
				}

				throw ex;
			}	
		}
        public void UpdateIsVatInclusive(long DebitMemoID, bool IsVatInclusive)
        {
            try
            {
                string SQL = "UPDATE tblPODebitMemo SET " +
                                "IsVatInclusive          =   @IsVatInclusive " +
                            "WHERE DebitMemoID = @DebitMemoID;";

                MySqlConnection cn = GetConnection();

                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = cn;
                cmd.Transaction = mTransaction;
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandText = SQL;

                MySqlParameter prmIsVatInclusive = new MySqlParameter("@IsVatInclusive",MySqlDbType.Int16);
                prmIsVatInclusive.Value = Convert.ToInt16(IsVatInclusive); ;
                cmd.Parameters.Add(prmIsVatInclusive);

                MySqlParameter prmDebitMemoID = new MySqlParameter("@DebitMemoID",MySqlDbType.Int64);
                prmDebitMemoID.Value = DebitMemoID;
                cmd.Parameters.Add(prmDebitMemoID);

                cmd.ExecuteNonQuery();

                SynchronizeAmount(DebitMemoID);
            }

            catch (Exception ex)
            {
                TransactionFailed = true;
                if (IsInTransaction)
                {
                    mTransaction.Rollback();
                    mTransaction.Dispose();
                    mConnection.Close();
                    mConnection.Dispose();
                }

                throw ex;
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

                MySqlConnection cn = GetConnection();

                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = cn;
                cmd.Transaction = mTransaction;
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandText = SQL;

                MySqlParameter prmDiscountApplied = new MySqlParameter("@DiscountApplied", System.Data.DbType.Decimal);
                prmDiscountApplied.Value = DiscountApplied;
                cmd.Parameters.Add(prmDiscountApplied);

                MySqlParameter prmDiscountType = new MySqlParameter("@DiscountType",MySqlDbType.Int16);
                prmDiscountType.Value = Convert.ToInt16(DiscountType.ToString("d"));
                cmd.Parameters.Add(prmDiscountType);

                MySqlParameter prmDiscount2Applied = new MySqlParameter("@Discount2Applied", System.Data.DbType.Decimal);
                prmDiscount2Applied.Value = Discount2Applied;
                cmd.Parameters.Add(prmDiscount2Applied);

                MySqlParameter prmDiscount2Type = new MySqlParameter("@Discount2Type",MySqlDbType.Int16);
                prmDiscount2Type.Value = Convert.ToInt16(Discount2Type.ToString("d"));
                cmd.Parameters.Add(prmDiscount2Type);

                MySqlParameter prmDiscount3Applied = new MySqlParameter("@Discount3Applied", System.Data.DbType.Decimal);
                prmDiscount3Applied.Value = Discount3Applied;
                cmd.Parameters.Add(prmDiscount3Applied);

                MySqlParameter prmDiscount3Type = new MySqlParameter("@Discount3Type",MySqlDbType.Int16);
                prmDiscount3Type.Value = Convert.ToInt16(Discount3Type.ToString("d"));
                cmd.Parameters.Add(prmDiscount3Type);

                MySqlParameter prmDebitMemoID = new MySqlParameter("@DebitMemoID",MySqlDbType.Int64);
                prmDebitMemoID.Value = DebitMemoID;
                cmd.Parameters.Add(prmDebitMemoID);

                cmd.ExecuteNonQuery();
            }

            catch (Exception ex)
            {
                TransactionFailed = true;
                if (IsInTransaction)
                {
                    mTransaction.Rollback();
                    mTransaction.Dispose();
                    mConnection.Close();
                    mConnection.Dispose();
                }

                throw ex;
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

                MySqlConnection cn = GetConnection();

                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = cn;
                cmd.Transaction = mTransaction;
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandText = SQL;

                MySqlParameter prmDiscountApplied = new MySqlParameter("@DiscountApplied", System.Data.DbType.Decimal);
                prmDiscountApplied.Value = DiscountApplied;
                cmd.Parameters.Add(prmDiscountApplied);

                MySqlParameter prmDiscountType = new MySqlParameter("@DiscountType",MySqlDbType.Int16);
                prmDiscountType.Value = Convert.ToInt16(DiscountType.ToString("d"));
                cmd.Parameters.Add(prmDiscountType);

                MySqlParameter prmDebitMemoID = new MySqlParameter("@DebitMemoID",MySqlDbType.Int64);
                prmDebitMemoID.Value = DebitMemoID;
                cmd.Parameters.Add(prmDebitMemoID);

                cmd.ExecuteNonQuery();
            }

            catch (Exception ex)
            {
                TransactionFailed = true;
                if (IsInTransaction)
                {
                    mTransaction.Rollback();
                    mTransaction.Dispose();
                    mConnection.Close();
                    mConnection.Dispose();
                }

                throw ex;
            }
        }
        public void UpdateFreight(long DebitMemoID, decimal Freight)
        {
            try
            {
                string SQL = "UPDATE tblPODebitMemo SET " +
                                "Freight           =   @Freight " +
                            "WHERE DebitMemoID = @DebitMemoID;";

                MySqlConnection cn = GetConnection();

                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = cn;
                cmd.Transaction = mTransaction;
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandText = SQL;

                MySqlParameter prmFreight = new MySqlParameter("@Freight", System.Data.DbType.Decimal);
                prmFreight.Value = Freight;
                cmd.Parameters.Add(prmFreight);

                MySqlParameter prmDebitMemoID = new MySqlParameter("@DebitMemoID",MySqlDbType.Int64);
                prmDebitMemoID.Value = DebitMemoID;
                cmd.Parameters.Add(prmDebitMemoID);

                cmd.ExecuteNonQuery();
            }

            catch (Exception ex)
            {
                TransactionFailed = true;
                if (IsInTransaction)
                {
                    mTransaction.Rollback();
                    mTransaction.Dispose();
                    mConnection.Close();
                    mConnection.Dispose();
                }

                throw ex;
            }
        }
        public void UpdateDeposit(long DebitMemoID, decimal Deposit)
        {
            try
            {
                string SQL = "UPDATE tblPODebitMemo SET " +
                                "Deposit           =   @Deposit " +
                            "WHERE DebitMemoID = @DebitMemoID;";

                MySqlConnection cn = GetConnection();

                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = cn;
                cmd.Transaction = mTransaction;
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandText = SQL;

                MySqlParameter prmDeposit = new MySqlParameter("@Deposit", System.Data.DbType.Decimal);
                prmDeposit.Value = Deposit;
                cmd.Parameters.Add(prmDeposit);

                MySqlParameter prmDebitMemoID = new MySqlParameter("@DebitMemoID",MySqlDbType.Int64);
                prmDebitMemoID.Value = DebitMemoID;
                cmd.Parameters.Add(prmDebitMemoID);

                cmd.ExecuteNonQuery();
            }

            catch (Exception ex)
            {
                TransactionFailed = true;
                if (IsInTransaction)
                {
                    mTransaction.Rollback();
                    mTransaction.Dispose();
                    mConnection.Close();
                    mConnection.Dispose();
                }

                throw ex;
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
				  
				MySqlConnection cn = GetConnection();
	 			
				MySqlCommand cmd = new MySqlCommand();
				cmd.Connection = cn;
				cmd.Transaction = mTransaction;
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

				MySqlParameter prmDebitMemoID = new MySqlParameter("@DebitMemoID",System.Data.DbType.Int64);			
				prmDebitMemoID.Value = DebitMemoID;
				cmd.Parameters.Add(prmDebitMemoID);

				cmd.ExecuteNonQuery();

				/*******************************************
				 * Update the status of items
				 * ****************************************/
				POReturnItems clsPOReturnItems = new POReturnItems(mConnection, mTransaction);
				clsPOReturnItems.Post(DebitMemoID);

				/*******************************************
				 * Update Vendor Account
				 * ****************************************/
				AddItemToInventory(DebitMemoID);
			}

			catch (Exception ex)
			{
				TransactionFailed = true;
				if (IsInTransaction)
				{
					mTransaction.Rollback();
					mTransaction.Dispose(); 
					mConnection.Close();
					mConnection.Dispose();
				}

				throw ex;
			}	
		}

        private void UpdateAccounts(long DebitMemoID)
        {
            try
            {
                POReturnDetails clsPOReturnDetails = Details(DebitMemoID);
                ChartOfAccount clsChartOfAccount = new ChartOfAccount(mConnection, mTransaction);

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

                POReturnItems clsPOReturnItems = new POReturnItems(mConnection, mTransaction);
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
                TransactionFailed = true;
                if (IsInTransaction)
                {
                    mTransaction.Rollback();
                    mTransaction.Dispose();
                    mConnection.Close();
                    mConnection.Dispose();
                }

                throw ex;
            }
        }

		private void AddItemToInventory(long DebitMemoID)
		{

			POReturnDetails clsPOReturnDetails = Details(DebitMemoID);
			ERPConfig clsERPConfig = new ERPConfig(Connection, Transaction);
			ERPConfigDetails clsERPConfigDetails = clsERPConfig.Details();

			POReturnItems clsPOReturnItems = new POReturnItems(Connection, Transaction);
			ProductUnit clsProductUnit = new ProductUnit(Connection, Transaction);
			Product clsProduct = new Product(Connection, Transaction);
			ProductVariationsMatrix clsProductVariationsMatrix = new ProductVariationsMatrix(Connection, Transaction);

			Inventory clsInventory = new Inventory(Connection, Transaction);

			MySqlDataReader myReader = clsPOReturnItems.List(DebitMemoID, "DebitMemoItemID", SortOption.Ascending);

			while (myReader.Read())
			{
                long lngProductID = myReader.GetInt64("ProductID");
				int ProductUnitID = myReader.GetInt16("ProductUnitID");

				decimal ItemQuantity = myReader.GetDecimal("Quantity");
                decimal decQuantity = clsProductUnit.GetBaseUnitValue(lngProductID, ProductUnitID, ItemQuantity);
				
				long lngVariationMatrixID = myReader.GetInt64("VariationMatrixID");
				string MatrixDescription = "" + myReader["MatrixDescription"].ToString();
				string ProductCode = "" + myReader["ProductCode"].ToString();
				decimal ItemCost = myReader.GetDecimal("Amount");
				decimal VAT = myReader.GetDecimal("VAT");

				/*******************************************
				 * Subtract from Inventory
				 * ****************************************/
                // clsProduct.SubtractQuantity(ProductID, Quantity);
                // if (VariationMatrixID != 0) { clsProductVariationsMatrix.SubtractQuantity(VariationMatrixID, Quantity);}
                // July 26, 2011: change the above codes to the following
                clsProduct.SubtractQuantity(clsPOReturnDetails.BranchID, lngProductID, lngVariationMatrixID, decQuantity, Product.getPRODUCT_INVENTORY_MOVEMENT_VALUE(PRODUCT_INVENTORY_MOVEMENT.DEDUCT_PURCHASE_RETURN), DateTime.Now, clsPOReturnDetails.MemoNo, clsPOReturnDetails.PurchaserName);

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
				clsInventoryDetails.ProductCode = ProductCode;
				clsInventoryDetails.VariationMatrixID = lngVariationMatrixID;
				clsInventoryDetails.MatrixDescription = MatrixDescription;
				clsInventoryDetails.PReturnQuantity = decQuantity;
				clsInventoryDetails.PReturnCost = ItemCost - VAT;
				clsInventoryDetails.PReturnVAT = ItemCost;	//Purchase Return with VAT

				clsInventory.Insert(clsInventoryDetails);

			}
			myReader.Close();

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
				  
				MySqlConnection cn = GetConnection();
	 			
				MySqlCommand cmd = new MySqlCommand();
				cmd.Connection = cn;
				cmd.Transaction = mTransaction;
				cmd.CommandType = System.Data.CommandType.Text;
				cmd.CommandText = SQL;
				
				MySqlParameter prmCancelledDate = new MySqlParameter("@CancelledDate",MySqlDbType.DateTime);
				prmCancelledDate.Value = CancelledDate.ToString("yyyy-MM-dd HH:mm:ss");
				cmd.Parameters.Add(prmCancelledDate);

				MySqlParameter prmCancelledRemarks = new MySqlParameter("@CancelledRemarks",MySqlDbType.String);
				prmCancelledRemarks.Value = Remarks;
				cmd.Parameters.Add(prmCancelledRemarks);

				MySqlParameter prmCancelledByID = new MySqlParameter("@CancelledByID",System.Data.DbType.Int64);			
				prmCancelledByID.Value = CancelledByID;
				cmd.Parameters.Add(prmCancelledByID);

				MySqlParameter prmPOReturnStatus = new MySqlParameter("@POReturnStatus",MySqlDbType.Int16);
				prmPOReturnStatus.Value = POReturnStatus.Cancelled.ToString("d");
				cmd.Parameters.Add(prmPOReturnStatus);

				MySqlParameter prmDebitMemoID = new MySqlParameter("@DebitMemoID",System.Data.DbType.Int64);			
				prmDebitMemoID.Value = DebitMemoID;
				cmd.Parameters.Add(prmDebitMemoID);

				cmd.ExecuteNonQuery();

				/*******************************************
				 * Update the status of items
				 * ****************************************/
				POReturnItems clsPOReturnItems = new POReturnItems(mConnection, mTransaction);
				clsPOReturnItems.Cancel(DebitMemoID);

			}

			catch (Exception ex)
			{
				TransactionFailed = true;
				if (IsInTransaction)
				{
					mTransaction.Rollback();
					mTransaction.Dispose(); 
					mConnection.Close();
					mConnection.Dispose();
				}

				throw ex;
			}	
		}

		#endregion

		#region Delete

		public bool Delete(string IDs)
		{
			try 
			{
				string SQL=	"DELETE FROM tblPODebitMemo WHERE DebitMemoID IN (" + IDs + ");";
				  
				MySqlConnection cn = GetConnection();
	 			
				MySqlCommand cmd = new MySqlCommand();
				cmd.Connection = cn;
				cmd.Transaction = mTransaction;
				cmd.CommandType = System.Data.CommandType.Text;
				cmd.CommandText = SQL;

				cmd.ExecuteNonQuery();

				return true;
			}

			catch (Exception ex)
			{
				TransactionFailed = true;
				if (IsInTransaction)
				{
					mTransaction.Rollback();
					mTransaction.Dispose(); 
					mConnection.Close();
					mConnection.Dispose();
				}

				throw ex;
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
				  
				MySqlConnection cn = GetConnection();
	 			
				MySqlCommand cmd = new MySqlCommand();
				cmd.Connection = cn;
				cmd.Transaction = mTransaction;
				cmd.CommandType = System.Data.CommandType.Text;
				cmd.CommandText = SQL;

				MySqlParameter prmDebitMemoID = new MySqlParameter("@DebitMemoID",MySqlDbType.Int16);
				prmDebitMemoID.Value = DebitMemoID;
				cmd.Parameters.Add(prmDebitMemoID);

				MySqlDataReader myReader = (MySqlDataReader) cmd.ExecuteReader(System.Data.CommandBehavior.SingleResult);
				
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
				TransactionFailed = true;
				if (IsInTransaction)
				{
					mTransaction.Rollback();
					mTransaction.Dispose(); 
					mConnection.Close();
					mConnection.Dispose();
				}

				throw ex;
			}	
		}

		#endregion

		#region Streams: ListAsDataTable, List, Search

		public System.Data.DataTable ListAsDataTable(string SortField, SortOption SortOrder)
		{
            if (SortField == string.Empty || SortField == null) SortField = "DebitMemoID";

            string SQL = SQLSelect() + "ORDER BY " + SortField;

            if (SortOrder == SortOption.Ascending)
                SQL += " ASC";
            else
                SQL += " DESC";

			MySqlConnection cn = GetConnection();

			MySqlCommand cmd = new MySqlCommand();
			cmd.Connection = cn;
			cmd.Transaction = mTransaction;
			cmd.CommandType = System.Data.CommandType.Text;
			cmd.CommandText = SQL;
				
			System.Data.DataTable dt = new System.Data.DataTable("PODebitMemo");
			MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
			adapter.Fill(dt);
				
			return dt;
		}
        public System.Data.DataTable ListAsDataTable(POReturnStatus POReturnStatus, string SortField, SortOption SortOrder)
        {
            if (SortField == string.Empty || SortField == null) SortField = "DebitMemoID";

            string SQL = SQLSelect() + "AND POReturnStatus = @POReturnStatus " +
                            "ORDER BY " + SortField;

            if (SortOrder == SortOption.Ascending)
                SQL += " ASC";
            else
                SQL += " DESC";

            MySqlConnection cn = GetConnection();

            MySqlCommand cmd = new MySqlCommand();
            cmd.Connection = cn;
            cmd.Transaction = mTransaction;
            cmd.CommandType = System.Data.CommandType.Text;
            cmd.CommandText = SQL;

            MySqlParameter prmPOReturnStatus = new MySqlParameter("@POReturnStatus",MySqlDbType.Int16);
            prmPOReturnStatus.Value = POReturnStatus.ToString("d");
            cmd.Parameters.Add(prmPOReturnStatus);

            System.Data.DataTable dt = new System.Data.DataTable("PODebitMemo");
            MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
            adapter.Fill(dt);

            return dt;
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

				MySqlConnection cn = GetConnection();

				MySqlCommand cmd = new MySqlCommand();
				cmd.Connection = cn;
				cmd.Transaction = mTransaction;
				cmd.CommandType = System.Data.CommandType.Text;
				cmd.CommandText = SQL;
				
				MySqlParameter prmDebitMemoID = new MySqlParameter("@DebitMemoID",MySqlDbType.Int64);
				prmDebitMemoID.Value = DebitMemoID;
				cmd.Parameters.Add(prmDebitMemoID);

				MySqlDataReader myReader = (MySqlDataReader) cmd.ExecuteReader();
				
				return myReader;			
			}
			catch (Exception ex)
			{
				TransactionFailed = true;
				if (IsInTransaction)
				{
					mTransaction.Rollback();
					mTransaction.Dispose(); 
					mConnection.Close();
					mConnection.Dispose();
				}

				throw ex;
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

				MySqlConnection cn = GetConnection();

				MySqlCommand cmd = new MySqlCommand();
				cmd.Connection = cn;
				cmd.Transaction = mTransaction;
				cmd.CommandType = System.Data.CommandType.Text;
				cmd.CommandText = SQL;
				
				MySqlDataReader myReader = (MySqlDataReader) cmd.ExecuteReader();
				
				return myReader;			
			}
			catch (Exception ex)
			{
				TransactionFailed = true;
				if (IsInTransaction)
				{
					mTransaction.Rollback();
					mTransaction.Dispose(); 
					mConnection.Close();
					mConnection.Dispose();
				}

				throw ex;
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

				MySqlConnection cn = GetConnection();

				MySqlCommand cmd = new MySqlCommand();
				cmd.Connection = cn;
				cmd.Transaction = mTransaction;
				cmd.CommandType = System.Data.CommandType.Text;
				cmd.CommandText = SQL;
				
				MySqlParameter prmPOReturnStatus = new MySqlParameter("@POReturnStatus",MySqlDbType.Int16);			
				prmPOReturnStatus.Value = POReturnStatus.ToString("d");
				cmd.Parameters.Add(prmPOReturnStatus);

				MySqlDataReader myReader = (MySqlDataReader) cmd.ExecuteReader();
				
				return myReader;			
			}
			catch (Exception ex)
			{
				TransactionFailed = true;
				if (IsInTransaction)
				{
					mTransaction.Rollback();
					mTransaction.Dispose(); 
					mConnection.Close();
					mConnection.Dispose();
				}

				throw ex;
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

				MySqlConnection cn = GetConnection();

				MySqlCommand cmd = new MySqlCommand();
				cmd.Connection = cn;
				cmd.Transaction = mTransaction;
				cmd.CommandType = System.Data.CommandType.Text;
				cmd.CommandText = SQL;
				
				MySqlParameter prmPOReturnStatus = new MySqlParameter("@POReturnStatus",MySqlDbType.Int16);			
				prmPOReturnStatus.Value = POReturnStatus.ToString("d");
				cmd.Parameters.Add(prmPOReturnStatus);

				MySqlParameter prmSupplierID = new MySqlParameter("@SupplierID",System.Data.DbType.Int64);			
				prmSupplierID.Value = SupplierID;
				cmd.Parameters.Add(prmSupplierID);

				MySqlDataReader myReader = (MySqlDataReader) cmd.ExecuteReader();
				
				return myReader;			
			}
			catch (Exception ex)
			{
				TransactionFailed = true;
				if (IsInTransaction)
				{
					mTransaction.Rollback();
					mTransaction.Dispose(); 
					mConnection.Close();
					mConnection.Dispose();
				}

				throw ex;
			}	
		}
        public MySqlDataReader List(POReturnStatus POReturnStatus, DateTime StartDate, DateTime EndDate)
        {
            try
            {
                string SQL = SQLSelect() + "AND POReturnStatus = @POReturnStatus " +
                        "AND PostingDate BETWEEN @StartDate AND @EndDate " +
                    "ORDER BY DebitMemoID ASC";

                MySqlConnection cn = GetConnection();

                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = cn;
                cmd.Transaction = mTransaction;
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

                MySqlDataReader myReader = (MySqlDataReader)cmd.ExecuteReader();

                return myReader;
            }
            catch (Exception ex)
            {
                TransactionFailed = true;
                if (IsInTransaction)
                {
                    mTransaction.Rollback();
                    mTransaction.Dispose();
                    mConnection.Close();
                    mConnection.Dispose();
                }

                throw ex;
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

                MySqlConnection cn = GetConnection();

                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = cn;
                cmd.Transaction = mTransaction;
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandText = SQL;

                cmd.Parameters.AddWithValue("@POReturnStatus", POReturnStatus.ToString("d"));
                cmd.Parameters.AddWithValue("@SupplierID", SupplierID);
                cmd.Parameters.AddWithValue("@StartDate", StartDate.ToString("yyyy-MM-dd HH:mm:ss"));
                cmd.Parameters.AddWithValue("@EndDate", EndDate.ToString("yyyy-MM-dd HH:mm:ss"));

                MySqlDataReader myReader = (MySqlDataReader)cmd.ExecuteReader();

                return myReader;
            }
            catch (Exception ex)
            {
                TransactionFailed = true;
                if (IsInTransaction)
                {
                    mTransaction.Rollback();
                    mTransaction.Dispose();
                    mConnection.Close();
                    mConnection.Dispose();
                }

                throw ex;
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

				MySqlConnection cn = GetConnection();

				MySqlCommand cmd = new MySqlCommand();
				cmd.Connection = cn;
				cmd.Transaction = mTransaction;
				cmd.CommandType = System.Data.CommandType.Text;
				cmd.CommandText = SQL;
				
				MySqlParameter prmSearchKey = new MySqlParameter("@SearchKey",MySqlDbType.String);
				prmSearchKey.Value = "%" + SearchKey + "%";
				cmd.Parameters.Add(prmSearchKey);

				MySqlDataReader myReader = (MySqlDataReader) cmd.ExecuteReader();
				
				return myReader;			
			}
			catch (Exception ex)
			{
				TransactionFailed = true;
				if (IsInTransaction)
				{
					mTransaction.Rollback();
					mTransaction.Dispose(); 
					mConnection.Close();
					mConnection.Dispose();
				}

				throw ex;
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

				MySqlConnection cn = GetConnection();

				MySqlCommand cmd = new MySqlCommand();
				cmd.Connection = cn;
				cmd.Transaction = mTransaction;
				cmd.CommandType = System.Data.CommandType.Text;
				cmd.CommandText = SQL;
				
				MySqlParameter prmPOReturnStatus = new MySqlParameter("@POReturnStatus",MySqlDbType.Int16);			
				prmPOReturnStatus.Value = Status.ToString("d");
				cmd.Parameters.Add(prmPOReturnStatus);

				MySqlParameter prmSearchKey = new MySqlParameter("@SearchKey",MySqlDbType.String);
				prmSearchKey.Value = "%" + SearchKey + "%";
				cmd.Parameters.Add(prmSearchKey);

				MySqlDataReader myReader = (MySqlDataReader) cmd.ExecuteReader();
				
				return myReader;			
			}
			catch (Exception ex)
			{
				TransactionFailed = true;
				if (IsInTransaction)
				{
					mTransaction.Rollback();
					mTransaction.Dispose(); 
					mConnection.Close();
					mConnection.Dispose();
				}

				throw ex;
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

                MySqlConnection cn = GetConnection();

                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = cn;
                cmd.Transaction = mTransaction;
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandText = SQL;

                MySqlParameter prmPOReturnStatus = new MySqlParameter("@POReturnStatus",MySqlDbType.Int16);
                prmPOReturnStatus.Value = Status.ToString("d");
                cmd.Parameters.Add(prmPOReturnStatus);

                MySqlParameter prmSearchKey = new MySqlParameter("@SearchKey",MySqlDbType.String);
                prmSearchKey.Value = "%" + SearchKey + "%";
                cmd.Parameters.Add(prmSearchKey);

                System.Data.DataTable dt = new System.Data.DataTable("PODebitMemo");
                MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
                adapter.Fill(dt);

                return dt;
            }
            catch (Exception ex)
            {
                TransactionFailed = true;
                if (IsInTransaction)
                {
                    mTransaction.Rollback();
                    mTransaction.Dispose();
                    mConnection.Close();
                    mConnection.Dispose();
                }

                throw ex;
            }
        }		

		#endregion

		#region Public Modifiers: LastTransactionNo, SynchronizeAmount

		public string LastTransactionNo()
		{
			try
			{
				string stRetValue = String.Empty;
				
				ERPConfig clsERPConfig = new ERPConfig(Connection, Transaction);
				stRetValue = clsERPConfig.get_LastPOReturnNo();

				return stRetValue;
			}

			catch (Exception ex)
			{
				TransactionFailed = true;
				if (IsInTransaction)
				{
					mTransaction.Rollback();
					mTransaction.Dispose(); 
					mConnection.Close();
					mConnection.Dispose();
				}

				throw ex;
			}	
		}
		public void SynchronizeAmount(long DebitMemoID)
		{
			try 
			{
                string SQL = "CALL procPODebitMemoSynchronizeAmount(@DebitMemoID);";
				  
				MySqlConnection cn = GetConnection();
	 			
				MySqlCommand cmd = new MySqlCommand();
				cmd.Connection = cn;
				cmd.Transaction = mTransaction;
				cmd.CommandType = System.Data.CommandType.Text;
				cmd.CommandText = SQL;
				
				MySqlParameter prmDebitMemoID = new MySqlParameter("@DebitMemoID",System.Data.DbType.Int64);			
				prmDebitMemoID.Value = DebitMemoID;
				cmd.Parameters.Add(prmDebitMemoID);

				cmd.ExecuteNonQuery();
			}

			catch (Exception ex)
			{
				TransactionFailed = true;
				if (IsInTransaction)
				{
					mTransaction.Rollback();
					mTransaction.Dispose(); 
					mConnection.Close();
					mConnection.Dispose();
				}

				throw ex;
			}	
		}

		#endregion

	}
}


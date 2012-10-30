//using System;
//using System.Security.Permissions;
//using MySql.Data.MySqlClient;

//namespace AceSoft.RetailPlus.Data
//{

//    [StrongNameIdentityPermissionAttribute(SecurityAction.LinkDemand,
//         PublicKey = "002400000480000094000000060200000024000" +
//         "052534131000400000100010053D785642F9F960B43157E0380" +
//         "F393BEE53E8DFAFBF441366C1B6F8B48D9DDF0D527B1F3B21EA" +
//         "E85D2FDB664CE81EB8A87DBE4C589D6F4202FE2B7D4B978BB69" +
//         "684874612CB9B8DB7A0339400A9C4E68277884B07817363D242" +
//         "E3696F9FACDBEA831810AE6DC9EDCA91A7B5DA12FE7BF65D113" +
//         "FF52834EAFB5A7A1FDFD5851A3")]

//    #region PODetails

//    public struct PODetails
//    {
//        public long POID;
//        public string PONo;
//        public DateTime PODate;
//        public long SupplierID;
//        public string SupplierCode;
//        public string SupplierContact;
//        public string SupplierAddress;
//        public string SupplierTelephoneNo;
//        public int SupplierModeOfTerms;
//        public int SupplierTerms;
//        public DateTime RequiredDeliveryDate;
//        public int BranchID;
//        public string BranchCode;
//        public string BranchName;
//        public string BranchAddress;
//        public long PurchaserID;
//        public string PurchaserName;
//        public decimal SubTotal;
//        public decimal Discount;
//        public decimal DiscountApplied;
//        public DiscountTypes DiscountType;
//        public decimal VAT;
//        public decimal VatableAmount;
//        public decimal EVAT;
//        public decimal EVatableAmount;
//        public decimal LocalTax;
//        public decimal Freight;
//        public decimal Deposit;
//        public decimal UnpaidAmount;
//        public decimal PaidAmount;
//        public decimal TotalItemDiscount;
//        public POStatus Status;
//        public string Remarks;
//        public string SupplierDRNo;
//        public DateTime DeliveryDate;
//        public DateTime CancelledDate;
//        public string CancelledRemarks;
//        public long CancelledByID;
//        public int ChartOfAccountIDAPTracking;
//        public int ChartOfAccountIDAPFreight;
//        public int ChartOfAccountIDAPVDeposit;
//        public int ChartOfAccountIDAPContra;
//        public int ChartOfAccountIDAPLatePayment;
//    }

//    #endregion

//    [StrongNameIdentityPermissionAttribute(SecurityAction.LinkDemand,
//         PublicKey = "002400000480000094000000060200000024000" +
//         "052534131000400000100010053D785642F9F960B43157E0380" +
//         "F393BEE53E8DFAFBF441366C1B6F8B48D9DDF0D527B1F3B21EA" +
//         "E85D2FDB664CE81EB8A87DBE4C589D6F4202FE2B7D4B978BB69" +
//         "684874612CB9B8DB7A0339400A9C4E68277884B07817363D242" +
//         "E3696F9FACDBEA831810AE6DC9EDCA91A7B5DA12FE7BF65D113" +
//         "FF52834EAFB5A7A1FDFD5851A3")]
//    public class PO
//    {
//        MySqlConnection mConnection;
//        MySqlTransaction mTransaction;
//        bool IsInTransaction = false;
//        bool TransactionFailed = false;

//        public MySqlConnection Connection
//        {
//            get { return mConnection;	}
//        }

//        public MySqlTransaction Transaction
//        {
//            get { return mTransaction;	}
//        }


//        #region Constructors and Destructors

//        public PO()
//        {
			
//        }

//        public PO(MySqlConnection Connection, MySqlTransaction Transaction)
//        {
//            mConnection = Connection;
//            mTransaction = Transaction;
			
//        }

//        public void CommitAndDispose() 
//        {
//            if (!TransactionFailed)
//            {
//                if (IsInTransaction)
//                {
//                    mTransaction.Commit();
//                    mTransaction.Dispose(); 
//                    mConnection.Close();
//                    mConnection.Dispose();
//                }
//            }
//        }

//        public MySqlConnection GetConnection()
//        {
//            if (mConnection==null)
//            {
//                mConnection = new MySqlConnection(AceSoft.RetailPlus.DBConnection.ConnectionString());	
//                mConnection.Open();
				
//                mTransaction = (MySqlTransaction) mConnection.BeginTransaction();
//            }
			
//            IsInTransaction = true;
//            return mConnection;
//        } 


//        #endregion

//        #region Insert and Update

//        public long Insert(PODetails Details)
//        {
//            try 
//            {
//                ERPConfig clsERPConfig = new ERPConfig(mConnection, mTransaction);
//                APLinkConfigDetails clsAPLinkConfigDetails = clsERPConfig.APLinkDetails();

//                string SQL = "INSERT INTO tblPO (" +
//                                "PONo, " +
//                                "PODate, " +
//                                "SupplierID, " +
//                                "SupplierCode, " +
//                                "SupplierContact, " +
//                                "SupplierAddress, " +
//                                "SupplierTelephoneNo, " +
//                                "SupplierModeOfTerms, " +
//                                "SupplierTerms, " +
//                                "RequiredDeliveryDate, " +
//                                "BranchID, " +
//                                "PurchaserID, " +
//                                "PurchaserName, " +
//                                "Status, " +
//                                "Remarks, " +
//                                "ChartOfAccountIDAPTracking, " +
//                                "ChartOfAccountIDAPBills, " +
//                                "ChartOfAccountIDAPFreight, " +
//                                "ChartOfAccountIDAPVDeposit, " +
//                                "ChartOfAccountIDAPContra, " +
//                                "ChartOfAccountIDAPLatePayment" +
//                            ") VALUES (" +
//                                "@PONo, " +
//                                "@PODate, " +
//                                "@SupplierID, " +
//                                "@SupplierCode, " +
//                                "@SupplierContact, " +
//                                "@SupplierAddress, " +
//                                "@SupplierTelephoneNo, " +
//                                "@SupplierModeOfTerms, " +
//                                "@SupplierTerms, " +
//                                "@RequiredDeliveryDate, " +
//                                "@BranchID, " +
//                                "@PurchaserID, " +
//                                "@PurchaserName, " +
//                                "@Status, " +
//                                "@Remarks, " +
//                                "@ChartOfAccountIDAPTracking, " +
//                                "@ChartOfAccountIDAPBills, " +
//                                "@ChartOfAccountIDAPFreight, " +
//                                "@ChartOfAccountIDAPVDeposit, " +
//                                "@ChartOfAccountIDAPContra, " +
//                                "@ChartOfAccountIDAPLatePayment" +
//                            ");";
				  
//                MySqlConnection cn = GetConnection();
	 			
//                MySqlCommand cmd = new MySqlCommand();
//                cmd.Connection = cn;
//                cmd.Transaction = mTransaction;
//                cmd.CommandType = System.Data.CommandType.Text;
//                cmd.CommandText = SQL;
				
//                MySqlParameter prmPONo = new MySqlParameter("@PONo",MySqlDbType.String);
//                prmPONo.Value = Details.PONo;
//                cmd.Parameters.Add(prmPONo);

//                MySqlParameter prmPODate = new MySqlParameter("@PODate",MySqlDbType.DateTime);
//                prmPODate.Value = Details.PODate.ToString("yyyy-MM-dd HH:mm:ss");
//                cmd.Parameters.Add(prmPODate);

//                MySqlParameter prmSupplierID = new MySqlParameter("@SupplierID",System.Data.DbType.Int64);			
//                prmSupplierID.Value = Details.SupplierID;
//                cmd.Parameters.Add(prmSupplierID);
								 
//                MySqlParameter prmSupplierCode = new MySqlParameter("@SupplierCode",MySqlDbType.String);
//                prmSupplierCode.Value = Details.SupplierCode;
//                cmd.Parameters.Add(prmSupplierCode);
		 
//                MySqlParameter prmSupplierContact = new MySqlParameter("@SupplierContact",MySqlDbType.String);
//                prmSupplierContact.Value = Details.SupplierContact;
//                cmd.Parameters.Add(prmSupplierContact);			 
				
//                MySqlParameter prmSupplierAddress = new MySqlParameter("@SupplierAddress",MySqlDbType.String);
//                prmSupplierAddress.Value = Details.SupplierAddress;
//                cmd.Parameters.Add(prmSupplierAddress);	
				
//                MySqlParameter prmSupplierTelephoneNo = new MySqlParameter("@SupplierTelephoneNo",MySqlDbType.String);
//                prmSupplierTelephoneNo.Value = Details.SupplierTelephoneNo;
//                cmd.Parameters.Add(prmSupplierTelephoneNo);	

//                MySqlParameter prmSupplierModeOfTerms = new MySqlParameter("@SupplierModeOfTerms",MySqlDbType.Int16);
//                prmSupplierModeOfTerms.Value = Details.SupplierModeOfTerms;
//                cmd.Parameters.Add(prmSupplierModeOfTerms);	

//                MySqlParameter prmSupplierTerms = new MySqlParameter("@SupplierTerms",MySqlDbType.Int16);
//                prmSupplierTerms.Value = Details.SupplierTerms;
//                cmd.Parameters.Add(prmSupplierTerms);			 
							 
//                MySqlParameter prmRequiredDeliveryDate = new MySqlParameter("@RequiredDeliveryDate",MySqlDbType.DateTime);
//                prmRequiredDeliveryDate.Value = Details.RequiredDeliveryDate.ToString("yyyy-MM-dd HH:mm:ss");
//                cmd.Parameters.Add(prmRequiredDeliveryDate);
	 
//                MySqlParameter prmBranchID = new MySqlParameter("@BranchID",MySqlDbType.Int16);
//                prmBranchID.Value = Details.BranchID;
//                cmd.Parameters.Add(prmBranchID);				 
				
//                MySqlParameter prmPurchaserID = new MySqlParameter("@PurchaserID",System.Data.DbType.Int64);			
//                prmPurchaserID.Value = Details.PurchaserID;
//                cmd.Parameters.Add(prmPurchaserID);

//                MySqlParameter prmPurchaserName = new MySqlParameter("@PurchaserName",MySqlDbType.String);
//                prmPurchaserName.Value = Details.PurchaserName;
//                cmd.Parameters.Add(prmPurchaserName);

//                MySqlParameter prmStatus = new MySqlParameter("@Status",MySqlDbType.Int16);			
//                prmStatus.Value = Details.Status.ToString("d");
//                cmd.Parameters.Add(prmStatus);

//                MySqlParameter prmRemarks = new MySqlParameter("@Remarks",MySqlDbType.String);			
//                prmRemarks.Value = Details.Remarks;
//                cmd.Parameters.Add(prmRemarks);

//                MySqlParameter prmChartOfAccountIDAPTracking = new MySqlParameter("@ChartOfAccountIDAPTracking",MySqlDbType.Int32);
//                prmChartOfAccountIDAPTracking.Value = clsAPLinkConfigDetails.ChartOfAccountIDAPTracking;
//                cmd.Parameters.Add(prmChartOfAccountIDAPTracking);

//                MySqlParameter prmChartOfAccountIDAPBills = new MySqlParameter("@ChartOfAccountIDAPBills",MySqlDbType.Int32);
//                prmChartOfAccountIDAPBills.Value = clsAPLinkConfigDetails.ChartOfAccountIDAPBills;
//                cmd.Parameters.Add(prmChartOfAccountIDAPBills);

//                MySqlParameter prmChartOfAccountIDAPFreight = new MySqlParameter("@ChartOfAccountIDAPFreight",MySqlDbType.Int32);
//                prmChartOfAccountIDAPFreight.Value = clsAPLinkConfigDetails.ChartOfAccountIDAPFreight;
//                cmd.Parameters.Add(prmChartOfAccountIDAPFreight);

//                MySqlParameter prmChartOfAccountIDAPVDeposit = new MySqlParameter("@ChartOfAccountIDAPVDeposit",MySqlDbType.Int32);
//                prmChartOfAccountIDAPVDeposit.Value = clsAPLinkConfigDetails.ChartOfAccountIDAPVDeposit;
//                cmd.Parameters.Add(prmChartOfAccountIDAPVDeposit);

//                MySqlParameter prmChartOfAccountIDAPContra = new MySqlParameter("@ChartOfAccountIDAPContra",MySqlDbType.Int32);
//                prmChartOfAccountIDAPContra.Value = clsAPLinkConfigDetails.ChartOfAccountIDAPContra;
//                cmd.Parameters.Add(prmChartOfAccountIDAPContra);

//                MySqlParameter prmChartOfAccountIDAPLatePayment = new MySqlParameter("@ChartOfAccountIDAPLatePayment",MySqlDbType.Int32);
//                prmChartOfAccountIDAPLatePayment.Value = clsAPLinkConfigDetails.ChartOfAccountIDAPLatePayment;
//                cmd.Parameters.Add(prmChartOfAccountIDAPLatePayment);

//                cmd.ExecuteNonQuery();

//                SQL = "SELECT LAST_INSERT_ID();";
				
//                cmd.Parameters.Clear(); 
//                cmd.CommandText = SQL;
				
//                MySqlDataReader myReader = (MySqlDataReader) cmd.ExecuteReader(System.Data.CommandBehavior.SingleResult);
				
//                Int64 iID = 0;

//                while (myReader.Read()) 
//                {
//                    iID = myReader.GetInt64(0);
//                }

//                myReader.Close();

//                return iID;
//            }

//            catch (Exception ex)
//            {
//                TransactionFailed = true;
//                if (IsInTransaction)
//                {
//                    mTransaction.Rollback();
//                    mTransaction.Dispose(); 
//                    mConnection.Close();
//                    mConnection.Dispose();
//                }

//                throw ex;
//            }	
//        }
//        public void Update(PODetails Details)
//        {
//            try 
//            {
//                ERPConfig clsERPConfig = new ERPConfig(mConnection, mTransaction);
//                APLinkConfigDetails clsAPLinkConfigDetails = clsERPConfig.APLinkDetails();

//                string SQL=	"UPDATE tblPO SET " +
//                                "PONo					=	@PONo, " +
//                                "PODate					=	@PODate, " +
//                                "SupplierID				=	@SupplierID, " +
//                                "SupplierCode			=	@SupplierCode, " +
//                                "SupplierContact		=	@SupplierContact, " +
//                                "SupplierAddress		=	@SupplierAddress, " +
//                                "SupplierTelephoneNo	=	@SupplierTelephoneNo, " +
//                                "SupplierModeOfTerms	=	@SupplierModeOfTerms, " +
//                                "SupplierTerms			=	@SupplierTerms, " +
//                                "RequiredDeliveryDate	=	@RequiredDeliveryDate, " +
//                                "BranchID				=	@BranchID, " +
//                                "PurchaserID			=	@PurchaserID, " +
//                                "PurchaserName          =   @PurchaserName, " +
//                                "Remarks                =   @Remarks, " +
//                                "ChartOfAccountIDAPTracking     = @ChartOfAccountIDAPTracking, " +
//                                "ChartOfAccountIDAPBills        = @ChartOfAccountIDAPBills, " +
//                                "ChartOfAccountIDAPFreight      = @ChartOfAccountIDAPFreight, " +
//                                "ChartOfAccountIDAPVDeposit     = @ChartOfAccountIDAPVDeposit, " +
//                                "ChartOfAccountIDAPContra       = @ChartOfAccountIDAPContra, " +
//                                "ChartOfAccountIDAPLatePayment  = @ChartOfAccountIDAPLatePayment " +
//                            "WHERE POID = @POID;";
				  
//                MySqlConnection cn = GetConnection();
	 			
//                MySqlCommand cmd = new MySqlCommand();
//                cmd.Connection = cn;
//                cmd.Transaction = mTransaction;
//                cmd.CommandType = System.Data.CommandType.Text;
//                cmd.CommandText = SQL;

//                MySqlParameter prmPONo = new MySqlParameter("@PONo",MySqlDbType.String);
//                prmPONo.Value = Details.PONo;
//                cmd.Parameters.Add(prmPONo);

//                MySqlParameter prmPODate = new MySqlParameter("@PODate",MySqlDbType.DateTime);
//                prmPODate.Value = Details.PODate.ToString("yyyy-MM-dd HH:mm:ss");
//                cmd.Parameters.Add(prmPODate);

//                MySqlParameter prmSupplierID = new MySqlParameter("@SupplierID",MySqlDbType.Int64);
//                prmSupplierID.Value = Details.SupplierID;
//                cmd.Parameters.Add(prmSupplierID);

//                MySqlParameter prmSupplierCode = new MySqlParameter("@SupplierCode",MySqlDbType.String);
//                prmSupplierCode.Value = Details.SupplierCode;
//                cmd.Parameters.Add(prmSupplierCode);

//                MySqlParameter prmSupplierContact = new MySqlParameter("@SupplierContact",MySqlDbType.String);
//                prmSupplierContact.Value = Details.SupplierContact;
//                cmd.Parameters.Add(prmSupplierContact);

//                MySqlParameter prmSupplierAddress = new MySqlParameter("@SupplierAddress",MySqlDbType.String);
//                prmSupplierAddress.Value = Details.SupplierAddress;
//                cmd.Parameters.Add(prmSupplierAddress);

//                MySqlParameter prmSupplierTelephoneNo = new MySqlParameter("@SupplierTelephoneNo",MySqlDbType.String);
//                prmSupplierTelephoneNo.Value = Details.SupplierTelephoneNo;
//                cmd.Parameters.Add(prmSupplierTelephoneNo);

//                MySqlParameter prmSupplierModeOfTerms = new MySqlParameter("@SupplierModeOfTerms",MySqlDbType.Int16);
//                prmSupplierModeOfTerms.Value = Details.SupplierModeOfTerms;
//                cmd.Parameters.Add(prmSupplierModeOfTerms);

//                MySqlParameter prmSupplierTerms = new MySqlParameter("@SupplierTerms",MySqlDbType.Int16);
//                prmSupplierTerms.Value = Details.SupplierTerms;
//                cmd.Parameters.Add(prmSupplierTerms);

//                MySqlParameter prmRequiredDeliveryDate = new MySqlParameter("@RequiredDeliveryDate",MySqlDbType.DateTime);
//                prmRequiredDeliveryDate.Value = Details.RequiredDeliveryDate.ToString("yyyy-MM-dd HH:mm:ss");
//                cmd.Parameters.Add(prmRequiredDeliveryDate);

//                MySqlParameter prmBranchID = new MySqlParameter("@BranchID",MySqlDbType.Int16);
//                prmBranchID.Value = Details.BranchID;
//                cmd.Parameters.Add(prmBranchID);

//                MySqlParameter prmPurchaserID = new MySqlParameter("@PurchaserID",MySqlDbType.Int64);
//                prmPurchaserID.Value = Details.PurchaserID;
//                cmd.Parameters.Add(prmPurchaserID);

//                MySqlParameter prmPurchaserName = new MySqlParameter("@PurchaserName",MySqlDbType.String);
//                prmPurchaserName.Value = Details.PurchaserName;
//                cmd.Parameters.Add(prmPurchaserName);

//                MySqlParameter prmRemarks = new MySqlParameter("@Remarks",MySqlDbType.String);
//                prmRemarks.Value = Details.Remarks;
//                cmd.Parameters.Add(prmRemarks);

//                MySqlParameter prmChartOfAccountIDAPTracking = new MySqlParameter("@ChartOfAccountIDAPTracking",MySqlDbType.Int32);
//                prmChartOfAccountIDAPTracking.Value = clsAPLinkConfigDetails.ChartOfAccountIDAPTracking;
//                cmd.Parameters.Add(prmChartOfAccountIDAPTracking);

//                MySqlParameter prmChartOfAccountIDAPBills = new MySqlParameter("@ChartOfAccountIDAPBills",MySqlDbType.Int32);
//                prmChartOfAccountIDAPBills.Value = clsAPLinkConfigDetails.ChartOfAccountIDAPBills;
//                cmd.Parameters.Add(prmChartOfAccountIDAPBills);

//                MySqlParameter prmChartOfAccountIDAPFreight = new MySqlParameter("@ChartOfAccountIDAPFreight",MySqlDbType.Int32);
//                prmChartOfAccountIDAPFreight.Value = clsAPLinkConfigDetails.ChartOfAccountIDAPFreight;
//                cmd.Parameters.Add(prmChartOfAccountIDAPFreight);

//                MySqlParameter prmChartOfAccountIDAPVDeposit = new MySqlParameter("@ChartOfAccountIDAPVDeposit",MySqlDbType.Int32);
//                prmChartOfAccountIDAPVDeposit.Value = clsAPLinkConfigDetails.ChartOfAccountIDAPVDeposit;
//                cmd.Parameters.Add(prmChartOfAccountIDAPVDeposit);

//                MySqlParameter prmChartOfAccountIDAPContra = new MySqlParameter("@ChartOfAccountIDAPContra",MySqlDbType.Int32);
//                prmChartOfAccountIDAPContra.Value = clsAPLinkConfigDetails.ChartOfAccountIDAPContra;
//                cmd.Parameters.Add(prmChartOfAccountIDAPContra);

//                MySqlParameter prmChartOfAccountIDAPLatePayment = new MySqlParameter("@ChartOfAccountIDAPLatePayment",MySqlDbType.Int32);
//                prmChartOfAccountIDAPLatePayment.Value = clsAPLinkConfigDetails.ChartOfAccountIDAPLatePayment;
//                cmd.Parameters.Add(prmChartOfAccountIDAPLatePayment);

//                MySqlParameter prmPOID = new MySqlParameter("@POID",System.Data.DbType.Int64);			
//                prmPOID.Value = Details.POID;
//                cmd.Parameters.Add(prmPOID);

//                cmd.ExecuteNonQuery();
//            }

//            catch (Exception ex)
//            {
//                TransactionFailed = true;
//                if (IsInTransaction)
//                {
//                    mTransaction.Rollback();
//                    mTransaction.Dispose(); 
//                    mConnection.Close();
//                    mConnection.Dispose();
//                }

//                throw ex;
//            }	
//        }

//        public void UpdateDiscount(long POID, decimal DiscountApplied, DiscountTypes DiscountType)
//        {
//            try
//            {
//                string SQL = "UPDATE tblPO SET " +
//                                "DiscountApplied        =   @DiscountApplied, " +
//                                "DiscountType           =   @DiscountType " +
//                            "WHERE POID = @POID;";

//                MySqlConnection cn = GetConnection();

//                MySqlCommand cmd = new MySqlCommand();
//                cmd.Connection = cn;
//                cmd.Transaction = mTransaction;
//                cmd.CommandType = System.Data.CommandType.Text;
//                cmd.CommandText = SQL;

//                MySqlParameter prmDiscountApplied = new MySqlParameter("@DiscountApplied", System.Data.DbType.Decimal);
//                prmDiscountApplied.Value = DiscountApplied;
//                cmd.Parameters.Add(prmDiscountApplied);

//                MySqlParameter prmDiscountType = new MySqlParameter("@DiscountType",MySqlDbType.Int16);
//                prmDiscountType.Value = Convert.ToInt16(DiscountType.ToString("d"));
//                cmd.Parameters.Add(prmDiscountType);

//                MySqlParameter prmPOID = new MySqlParameter("@POID",MySqlDbType.Int64);
//                prmPOID.Value = POID;
//                cmd.Parameters.Add(prmPOID);

//                cmd.ExecuteNonQuery();
//            }

//            catch (Exception ex)
//            {
//                TransactionFailed = true;
//                if (IsInTransaction)
//                {
//                    mTransaction.Rollback();
//                    mTransaction.Dispose();
//                    mConnection.Close();
//                    mConnection.Dispose();
//                }

//                throw ex;
//            }
//        }
//        public void UpdateDiscountFreightDeposit(long POID, decimal DiscountApplied, DiscountTypes DiscountType)
//        {
//            try
//            {
//                string SQL = "UPDATE tblPO SET " +
//                                "DiscountApplied        =   @DiscountApplied, " +
//                                "DiscountType           =   @DiscountType " +
//                            "WHERE POID = @POID;";

//                MySqlConnection cn = GetConnection();

//                MySqlCommand cmd = new MySqlCommand();
//                cmd.Connection = cn;
//                cmd.Transaction = mTransaction;
//                cmd.CommandType = System.Data.CommandType.Text;
//                cmd.CommandText = SQL;

//                MySqlParameter prmDiscountApplied = new MySqlParameter("@DiscountApplied", System.Data.DbType.Decimal);
//                prmDiscountApplied.Value = DiscountApplied;
//                cmd.Parameters.Add(prmDiscountApplied);

//                MySqlParameter prmDiscountType = new MySqlParameter("@DiscountType",MySqlDbType.Int16);
//                prmDiscountType.Value = Convert.ToInt16(DiscountType.ToString("d"));
//                cmd.Parameters.Add(prmDiscountType);

//                MySqlParameter prmPOID = new MySqlParameter("@POID",MySqlDbType.Int64);
//                prmPOID.Value = POID;
//                cmd.Parameters.Add(prmPOID);

//                cmd.ExecuteNonQuery();
//            }

//            catch (Exception ex)
//            {
//                TransactionFailed = true;
//                if (IsInTransaction)
//                {
//                    mTransaction.Rollback();
//                    mTransaction.Dispose();
//                    mConnection.Close();
//                    mConnection.Dispose();
//                }

//                throw ex;
//            }
//        }
//        public void UpdateFreight(long POID, decimal Freight)
//        {
//            try
//            {
//                string SQL = "UPDATE tblPO SET " +
//                                "Freight           =   @Freight " +
//                            "WHERE POID = @POID;";

//                MySqlConnection cn = GetConnection();

//                MySqlCommand cmd = new MySqlCommand();
//                cmd.Connection = cn;
//                cmd.Transaction = mTransaction;
//                cmd.CommandType = System.Data.CommandType.Text;
//                cmd.CommandText = SQL;

//                MySqlParameter prmFreight = new MySqlParameter("@Freight", System.Data.DbType.Decimal);
//                prmFreight.Value = Freight;
//                cmd.Parameters.Add(prmFreight);

//                MySqlParameter prmPOID = new MySqlParameter("@POID",MySqlDbType.Int64);
//                prmPOID.Value = POID;
//                cmd.Parameters.Add(prmPOID);

//                cmd.ExecuteNonQuery();
//            }

//            catch (Exception ex)
//            {
//                TransactionFailed = true;
//                if (IsInTransaction)
//                {
//                    mTransaction.Rollback();
//                    mTransaction.Dispose();
//                    mConnection.Close();
//                    mConnection.Dispose();
//                }

//                throw ex;
//            }
//        }
//        public void UpdateDeposit(long POID, decimal Deposit)
//        {
//            try
//            {
//                string SQL = "UPDATE tblPO SET " +
//                                "Deposit           =   @Deposit " +
//                            "WHERE POID = @POID;";

//                MySqlConnection cn = GetConnection();

//                MySqlCommand cmd = new MySqlCommand();
//                cmd.Connection = cn;
//                cmd.Transaction = mTransaction;
//                cmd.CommandType = System.Data.CommandType.Text;
//                cmd.CommandText = SQL;

//                MySqlParameter prmDeposit = new MySqlParameter("@Deposit", System.Data.DbType.Decimal);
//                prmDeposit.Value = Deposit;
//                cmd.Parameters.Add(prmDeposit);

//                MySqlParameter prmPOID = new MySqlParameter("@POID",MySqlDbType.Int64);
//                prmPOID.Value = POID;
//                cmd.Parameters.Add(prmPOID);

//                cmd.ExecuteNonQuery();
//            }

//            catch (Exception ex)
//            {
//                TransactionFailed = true;
//                if (IsInTransaction)
//                {
//                    mTransaction.Rollback();
//                    mTransaction.Dispose();
//                    mConnection.Close();
//                    mConnection.Dispose();
//                }

//                throw ex;
//            }
//        }

//        public void IssueGRN(long POID, string SupplierDRNo, DateTime DeliveryDate)
//        {
//            try 
//            {
//                string SQL=	"UPDATE tblPO SET " + 
//                                "SupplierDRNo			=	@SupplierDRNo, " +
//                                "DeliveryDate			=	@DeliveryDate, " +
//                                "Status				    =	@Status " +
//                            "WHERE POID = @POID;";
				  
//                MySqlConnection cn = GetConnection();
	 			
//                MySqlCommand cmd = new MySqlCommand();
//                cmd.Connection = cn;
//                cmd.Transaction = mTransaction;
//                cmd.CommandType = System.Data.CommandType.Text;
//                cmd.CommandText = SQL;
				
//                MySqlParameter prmSupplierDRNo = new MySqlParameter("@SupplierDRNo",MySqlDbType.String);
//                prmSupplierDRNo.Value = SupplierDRNo;
//                cmd.Parameters.Add(prmSupplierDRNo);

//                MySqlParameter prmDeliveryDate = new MySqlParameter("@DeliveryDate",MySqlDbType.DateTime);
//                prmDeliveryDate.Value = DeliveryDate.ToString("yyyy-MM-dd HH:mm:ss");
//                cmd.Parameters.Add(prmDeliveryDate);

//                MySqlParameter prmStatus = new MySqlParameter("@Status",MySqlDbType.Int16);
//                prmStatus.Value = POStatus.Posted.ToString("d");
//                cmd.Parameters.Add(prmStatus);

//                MySqlParameter prmPOID = new MySqlParameter("@POID",System.Data.DbType.Int64);			
//                prmPOID.Value = POID;
//                cmd.Parameters.Add(prmPOID);

//                cmd.ExecuteNonQuery();

//                /*******************************************
//                 * Update the status of items
//                 * ****************************************/
//                POItem clsPOItem = new POItem(mConnection, mTransaction);
//                clsPOItem.Post(POID);

//                /*******************************************
//                 * Update Vendor Account
//                 * ****************************************/
//                AddItemToInventory(POID);

//                /*******************************************
//                 * Update Account Balance
//                 * ****************************************/
//                UpdateAccounts(POID);
//            }

//            catch (Exception ex)
//            {
//                TransactionFailed = true;
//                if (IsInTransaction)
//                {
//                    mTransaction.Rollback();
//                    mTransaction.Dispose(); 
//                    mConnection.Close();
//                    mConnection.Dispose();
//                }

//                throw ex;
//            }	
//        }

//        private void UpdateAccounts(long POID)
//        {
//            try
//            {
//                PODetails clsPODetails = Details(POID);
//                ChartOfAccount clsChartOfAccount = new ChartOfAccount(mConnection, mTransaction);

//                // update ChartOfAccountIDAPTracking as credit
//                clsChartOfAccount.UpdateCredit(clsPODetails.ChartOfAccountIDAPTracking, clsPODetails.SubTotal);

//                // update Deposit & APContra
//                clsChartOfAccount.UpdateCredit(clsPODetails.ChartOfAccountIDAPContra, clsPODetails.Discount);

//                // update Freight & APTracking
//                clsChartOfAccount.UpdateCredit(clsPODetails.ChartOfAccountIDAPTracking, clsPODetails.Freight);    
//                clsChartOfAccount.UpdateDebit(clsPODetails.ChartOfAccountIDAPFreight, clsPODetails.Freight);

//                // update Deposit & APTracking
//                clsChartOfAccount.UpdateCredit(clsPODetails.ChartOfAccountIDAPTracking, clsPODetails.Deposit);
//                clsChartOfAccount.UpdateDebit(clsPODetails.ChartOfAccountIDAPVDeposit, clsPODetails.Deposit);

//                POItem clsPOItem = new POItem(mConnection, mTransaction);
//                MySqlDataReader myReader = clsPOItem.List(POID, "POItemID", SortOption.Ascending);
//                while (myReader.Read())
//                { 
//                    int iChartOfAccountIDPurchase = myReader.GetInt16("ChartOfAccountIDPurchase");
//                    int iChartOfAccountIDTaxPurchase = myReader.GetInt16("ChartOfAccountIDTaxPurchase");
                    
//                    decimal decVAT = myReader.GetDecimal("VAT");
//                    decimal decVATABLEAmount = myReader.GetDecimal("Amount") - decVAT;

//                    // update purchase as debit
//                    clsChartOfAccount.UpdateDebit(iChartOfAccountIDPurchase, decVATABLEAmount);
//                    // update tax as debit
//                    clsChartOfAccount.UpdateDebit(iChartOfAccountIDTaxPurchase, decVAT);
                    
//                }
//                myReader.Close();

//            }

//            catch (Exception ex)
//            {
//                TransactionFailed = true;
//                if (IsInTransaction)
//                {
//                    mTransaction.Rollback();
//                    mTransaction.Dispose();
//                    mConnection.Close();
//                    mConnection.Dispose();
//                }

//                throw ex;
//            }	
//        }

//        private void AddItemToInventory(long POID)
//        {

//            PODetails clsPODetails = Details(POID);
//            ERPConfig clsERPConfig = new ERPConfig(Connection, Transaction);
//            ERPConfigDetails clsERPConfigDetails = clsERPConfig.Details();

//            POItem clsPOItem = new POItem(Connection, Transaction);
//            ProductUnit clsProductUnit = new ProductUnit(Connection, Transaction);
//            Product clsProduct = new Product(Connection, Transaction);
//            ProductVariationsMatrix clsProductVariationsMatrix = new ProductVariationsMatrix(Connection, Transaction);

//            Inventory clsInventory = new Inventory(Connection, Transaction);

//            MySqlDataReader myReader = clsPOItem.List(POID, "POItemID", SortOption.Ascending);

//            while (myReader.Read())
//            {
//                long ProductID = myReader.GetInt64("ProductID");
//                int ProductUnitID = myReader.GetInt16("ProductUnitID");

//                decimal ItemQuantity = myReader.GetDecimal("Quantity");
//                decimal Quantity = clsProductUnit.GetBaseUnitValue(ProductID, ProductUnitID, ItemQuantity);
				
//                long VariationMatrixID = myReader.GetInt64("VariationMatrixID");
//                string MatrixDescription = "" + myReader["MatrixDescription"].ToString();
//                string ProductCode = "" + myReader["ProductCode"].ToString();
//                decimal UnitCost = myReader.GetDecimal("UnitCost");
//                decimal ItemCost = myReader.GetDecimal("Amount");
//                decimal VAT = myReader.GetDecimal("VAT");

//                /*******************************************
//                 * Update Purchasing Information
//                 * ****************************************/
//                clsProduct.UpdatePurchasing(ProductID, clsPODetails.SupplierID, ProductUnitID, (ItemQuantity*UnitCost)/Quantity);

//                /*******************************************
//                 * Add to Inventory
//                 * ****************************************/
//                clsProduct.AddQuantity(ProductID, Quantity);
//                if (VariationMatrixID != 0)
//                {
//                    clsProductVariationsMatrix.AddQuantity(VariationMatrixID, Quantity);
//                }

//                /*******************************************
//                 * Add to Inventory Analysis
//                 * ****************************************/
//                InventoryDetails clsInventoryDetails = new InventoryDetails();
//                clsInventoryDetails.PostingDateFrom = clsERPConfigDetails.PostingDateFrom;
//                clsInventoryDetails.PostingDateTo = clsERPConfigDetails.PostingDateTo;
//                clsInventoryDetails.PostingDate = clsPODetails.DeliveryDate;
//                clsInventoryDetails.ReferenceNo = clsPODetails.PONo;
//                clsInventoryDetails.ContactID = clsPODetails.SupplierID;
//                clsInventoryDetails.ContactCode = clsPODetails.SupplierCode;
//                clsInventoryDetails.ProductID = ProductID;
//                clsInventoryDetails.ProductCode = ProductCode;
//                clsInventoryDetails.VariationMatrixID = VariationMatrixID;
//                clsInventoryDetails.MatrixDescription = MatrixDescription;
//                clsInventoryDetails.PurchaseQuantity = Quantity;
//                clsInventoryDetails.PurchaseCost = ItemCost - VAT;
//                clsInventoryDetails.PurchaseVAT = ItemCost;	// Purchase Cost with VAT

//                clsInventory.Insert(clsInventoryDetails);

//            }
//            myReader.Close();

//        }
//        public void Cancel(long POID, DateTime CancelledDate, string Remarks, long CancelledByID)
//        {
//            try 
//            {
//                string SQL=	"UPDATE tblPO SET " + 
//                                "CancelledDate			=	@CancelledDate, " +
//                                "CancelledRemarks		=	@CancelledRemarks, " +
//                                "CancelledByID			=	@CancelledByID, " +
//                                "Status				    =	@Status " +
//                            "WHERE POID = @POID;";
				  
//                MySqlConnection cn = GetConnection();
	 			
//                MySqlCommand cmd = new MySqlCommand();
//                cmd.Connection = cn;
//                cmd.Transaction = mTransaction;
//                cmd.CommandType = System.Data.CommandType.Text;
//                cmd.CommandText = SQL;
				
//                MySqlParameter prmCancelledDate = new MySqlParameter("@CancelledDate",MySqlDbType.DateTime);
//                prmCancelledDate.Value = CancelledDate.ToString("yyyy-MM-dd HH:mm:ss");
//                cmd.Parameters.Add(prmCancelledDate);

//                MySqlParameter prmCancelledRemarks = new MySqlParameter("@CancelledRemarks",MySqlDbType.String);
//                prmCancelledRemarks.Value = Remarks;
//                cmd.Parameters.Add(prmCancelledRemarks);

//                MySqlParameter prmCancelledByID = new MySqlParameter("@CancelledByID",System.Data.DbType.Int64);			
//                prmCancelledByID.Value = CancelledByID;
//                cmd.Parameters.Add(prmCancelledByID);

//                MySqlParameter prmStatus = new MySqlParameter("@Status",MySqlDbType.Int16);
//                prmStatus.Value = POStatus.Cancelled.ToString("d");
//                cmd.Parameters.Add(prmStatus);

//                MySqlParameter prmPOID = new MySqlParameter("@POID",System.Data.DbType.Int64);			
//                prmPOID.Value = POID;
//                cmd.Parameters.Add(prmPOID);

//                cmd.ExecuteNonQuery();

//                /*******************************************
//                 * Update the status of items
//                 * ****************************************/
//                POItem clsPOItem = new POItem(mConnection, mTransaction);
//                clsPOItem.Cancel(POID);

//            }

//            catch (Exception ex)
//            {
//                TransactionFailed = true;
//                if (IsInTransaction)
//                {
//                    mTransaction.Rollback();
//                    mTransaction.Dispose(); 
//                    mConnection.Close();
//                    mConnection.Dispose();
//                }

//                throw ex;
//            }	
//        }
//        public void GenerateItemsForReorder(long POID)
//        {
//            try 
//            {
//                GetConnection();

//                Terminal clsTerminal = new Terminal(Connection, Transaction);
//                TerminalDetails clsTerminalDetails = clsTerminal.Details(Terminal.DEFAULT_TERMINAL_NO_ID);

//                PODetails clsPODetails = Details(POID);
				
//                Product clsProduct = new Product(Connection, Transaction);
//                System.Data.DataTable dt = clsProduct.ForReorder(clsPODetails.SupplierID);

//                POItem clsPOItem = new POItem(Connection, Transaction);
//                ProductVariationsMatrix clsProductVariationsMatrix = new ProductVariationsMatrix(Connection, Transaction);

//                foreach(System.Data.DataRow dr in dt.Rows)
//                {
//                    POItemDetails clsDetails = new POItemDetails();

//                    clsDetails.POID = POID;
//                    clsDetails.ProductID = Convert.ToInt64(dr["ProductID"]);
//                    clsDetails.ProductCode = dr["ProductCode"].ToString();
//                    clsDetails.BarCode = dr["BarCode"].ToString();
//                    clsDetails.Description = dr["ProductDesc"].ToString();
//                    clsDetails.ProductGroup = dr["ProductGroupCode"].ToString();
//                    clsDetails.ProductSubGroup = dr["ProductSubGroupCode"].ToString();
//                    clsDetails.ProductUnitID = Convert.ToInt32(dr["UnitID"]);
//                    clsDetails.ProductUnitCode = dr["UnitName"].ToString();
//                    clsDetails.Quantity = Convert.ToDecimal(dr["ReorderQty"]);
//                    clsDetails.UnitCost = Convert.ToDecimal(dr["Price"]);
//                    clsDetails.Discount = 0;
//                    clsDetails.DiscountApplied = 0;
//                    clsDetails.DiscountType = DiscountTypes.Percentage;
//                    clsDetails.Remarks = "added using auto generation";
				
//                    decimal amount = clsDetails.Quantity * clsDetails.UnitCost;

//                    if (Convert.ToDecimal(dr["VAT"]) > 0)
//                    {				
//                        clsDetails.VatableAmount = amount;
//                        clsDetails.EVatableAmount = amount;
//                        clsDetails.LocalTax = amount;
					
//                        clsDetails.VAT = clsDetails.VatableAmount * (clsTerminalDetails.VAT / 100);
//                        clsDetails.EVAT = clsDetails.EVatableAmount * (clsTerminalDetails.EVAT / 100);
//                        clsDetails.LocalTax = clsDetails.LocalTax * (clsTerminalDetails.LocalTax / 100);
//                        clsDetails.IsVatable = true;
//                    }
//                    else
//                    {
//                        clsDetails.VAT = 0;
//                        clsDetails.VatableAmount = 0;
//                        clsDetails.EVAT = 0;
//                        clsDetails.EVatableAmount = 0;
//                        clsDetails.LocalTax = 0;
//                        clsDetails.IsVatable = false;
//                    }
//                    clsDetails.Amount = amount + clsDetails.VAT;
					
//                    System.Data.DataTable dtmatrix = clsProductVariationsMatrix.ForReorder(clsDetails.ProductID, clsPODetails.SupplierID);
//                    if (dtmatrix.Rows.Count > 0)
//                        foreach(System.Data.DataRow drmatrix in dtmatrix.Rows)
//                        {
//                            amount = clsDetails.Quantity * clsDetails.UnitCost;

//                            clsDetails.ProductUnitID = Convert.ToInt32(drmatrix["UnitID"]);
//                            clsDetails.ProductUnitCode = drmatrix["UnitName"].ToString();
//                            clsDetails.Quantity = Convert.ToDecimal(drmatrix["ReorderQty"]);
//                            clsDetails.UnitCost = Convert.ToDecimal(drmatrix["Price"]);

//                            if (Convert.ToDecimal(drmatrix["VAT"]) > 0)
//                            {				
//                                clsDetails.VatableAmount = amount;
//                                clsDetails.EVatableAmount = amount;
//                                clsDetails.LocalTax = amount;
					
//                                clsDetails.VAT = clsDetails.VatableAmount * (clsTerminalDetails.VAT / 100);
//                                clsDetails.EVAT = clsDetails.EVatableAmount * (clsTerminalDetails.EVAT / 100);
//                                clsDetails.LocalTax = clsDetails.LocalTax * (clsTerminalDetails.LocalTax / 100);
//                                clsDetails.IsVatable = true;
//                            }
//                            else
//                            {
//                                clsDetails.VAT = 0;
//                                clsDetails.VatableAmount = 0;
//                                clsDetails.EVAT = 0;
//                                clsDetails.EVatableAmount = 0;
//                                clsDetails.LocalTax = 0;
//                                clsDetails.IsVatable = false;
//                            }
//                            clsDetails.Amount = amount + clsDetails.VAT;

//                            clsDetails.VariationMatrixID = Convert.ToInt64(drmatrix["MatrixID"]);
//                            clsDetails.MatrixDescription = drmatrix["VariationDesc"].ToString();
//                            clsPOItem.Insert(clsDetails);
//                        }
//                    else
//                    {
//                        clsDetails.VariationMatrixID = 0;
//                        clsDetails.MatrixDescription = string.Empty;
//                        clsPOItem.Insert(clsDetails);
//                    }
					
//                }
//            }

//            catch (Exception ex)
//            {
//                TransactionFailed = true;
//                if (IsInTransaction)
//                {
//                    mTransaction.Rollback();
//                    mTransaction.Dispose(); 
//                    mConnection.Close();
//                    mConnection.Dispose();
//                }

//                throw ex;
//            }	
//        }

//        /**********************************
//         * Lemuel E. Aceron
//         * July 30, 2008 17:21
//         * Added for Payment
//         **********************************/
//        public bool UpdatePaymentStatus(POPaymentStatus paymentStatus, string IDs)
//        {
//            try
//            {
//                string SQL = "UPDATE tblPO SET PaymentStatus = @PaymentStatus WHERE POID IN (" + IDs + ");";

//                MySqlConnection cn = GetConnection();

//                MySqlCommand cmd = new MySqlCommand();
//                cmd.Connection = cn;
//                cmd.Transaction = mTransaction;
//                cmd.CommandType = System.Data.CommandType.Text;
//                cmd.CommandText = SQL;

//                MySqlParameter prmPaymentStatus = new MySqlParameter("@PaymentStatus",MySqlDbType.Int16);
//                prmPaymentStatus.Value = paymentStatus.ToString("d");
//                cmd.Parameters.Add(prmPaymentStatus);

//                cmd.ExecuteNonQuery();

//                return true;
//            }

//            catch (Exception ex)
//            {
//                TransactionFailed = true;
//                if (IsInTransaction)
//                {
//                    mTransaction.Rollback();
//                    mTransaction.Dispose();
//                    mConnection.Close();
//                    mConnection.Dispose();
//                }

//                throw ex;
//            }
//        }
//        public bool UpdatePayment(long POID, decimal PaidAmount, POPaymentStatus paymentStatus)
//        {
//            try
//            {
//                string SQL = "UPDATE tblPO SET " +
//                                "PaidAmount     = PaidAmount + @PaidAmount, " +
//                                "UnpaidAmount   = UnpaidAmount - @PaidAmount, " +
//                                "PaymentStatus  = @PaymentStatus " +
//                             "WHERE POID = @POID;";

//                MySqlConnection cn = GetConnection();

//                MySqlCommand cmd = new MySqlCommand();
//                cmd.Connection = cn;
//                cmd.Transaction = mTransaction;
//                cmd.CommandType = System.Data.CommandType.Text;
//                cmd.CommandText = SQL;

//                MySqlParameter prmPaidAmount = new MySqlParameter("@PaidAmount", System.Data.DbType.Decimal);
//                prmPaidAmount.Value = PaidAmount;
//                cmd.Parameters.Add(prmPaidAmount);

//                MySqlParameter prmPaymentStatus = new MySqlParameter("@PaymentStatus",MySqlDbType.Int16);
//                prmPaymentStatus.Value = paymentStatus.ToString("d");
//                cmd.Parameters.Add(prmPaymentStatus);

//                MySqlParameter prmPOID = new MySqlParameter("@POID",MySqlDbType.Int64);
//                prmPOID.Value = POID;
//                cmd.Parameters.Add(prmPOID);

//                cmd.ExecuteNonQuery();

//                return true;
//            }

//            catch (Exception ex)
//            {
//                TransactionFailed = true;
//                if (IsInTransaction)
//                {
//                    mTransaction.Rollback();
//                    mTransaction.Dispose();
//                    mConnection.Close();
//                    mConnection.Dispose();
//                }

//                throw ex;
//            }
//        }

//        #endregion

//        #region Delete

//        public bool Delete(string IDs)
//        {
//            try 
//            {
//                string SQL=	"DELETE FROM tblPO WHERE POID IN (" + IDs + ");";
				  
//                MySqlConnection cn = GetConnection();
	 			
//                MySqlCommand cmd = new MySqlCommand();
//                cmd.Connection = cn;
//                cmd.Transaction = mTransaction;
//                cmd.CommandType = System.Data.CommandType.Text;
//                cmd.CommandText = SQL;

//                cmd.ExecuteNonQuery();

//                return true;
//            }

//            catch (Exception ex)
//            {
//                TransactionFailed = true;
//                if (IsInTransaction)
//                {
//                    mTransaction.Rollback();
//                    mTransaction.Dispose(); 
//                    mConnection.Close();
//                    mConnection.Dispose();
//                }

//                throw ex;
//            }	
//        }

//        #endregion

//        private string SQLSelect()
//        {
//            string stSQL = "SELECT " +
//                                "POID, " +
//                                "PONo, " +
//                                "PODate, " +
//                                "SupplierID, " +
//                                "SupplierCode, " +
//                                "SupplierContact, " +
//                                "SupplierAddress, " +
//                                "SupplierTelephoneNo, " +
//                                "SupplierModeOfTerms, " +
//                                "SupplierTerms, " +
//                                "RequiredDeliveryDate, " +
//                                "a.BranchID, " +
//                                "BranchCode, " +
//                                "BranchName, " +
//                                "b.Address BranchAddress, " +
//                                "PurchaserID, " +
//                                "PurchaserName, " +
//                                "SubTotal, " +
//                                "Discount, " +
//                                "DiscountApplied, " +
//                                "DiscountType, " +
//                                "VAT, " +
//                                "VatableAmount, " +
//                                "EVAT, " +
//                                "EVatableAmount, " +
//                                "LocalTax, " +
//                                "Freight, " +
//                                "Deposit, " +
//                                "PaidAmount, " +
//                                "UnpaidAmount, " +
//                                "Status, " +
//                                "a.Remarks, " +
//                                "SupplierDRNo, " +
//                                "DeliveryDate, " +
//                                "CancelledDate, " +
//                                "CancelledRemarks, " +
//                                "CancelledByID, " +
//                                "PaymentStatus, " +
//                                "ChartOfAccountIDAPTracking, " +
//                                "ChartOfAccountIDAPFreight, " +
//                                "ChartOfAccountIDAPVDeposit, " +
//                                "ChartOfAccountIDAPContra, " +
//                                "ChartOfAccountIDAPLatePayment, " +
//                                "TotalItemDiscount " +
//                            "FROM tblPO a INNER JOIN tblBranch b ON a.BranchID = b.BranchID ";
//            return stSQL;
//        }

//        #region Details

//        public PODetails Details(long POID)
//        {
//            try
//            {
//                string SQL=	SQLSelect() + "WHERE POID = @POID;";
				  
//                MySqlConnection cn = GetConnection();
	 			
//                MySqlCommand cmd = new MySqlCommand();
//                cmd.Connection = cn;
//                cmd.Transaction = mTransaction;
//                cmd.CommandType = System.Data.CommandType.Text;
//                cmd.CommandText = SQL;

//                MySqlParameter prmPOID = new MySqlParameter("@POID",MySqlDbType.Int16);
//                prmPOID.Value = POID;
//                cmd.Parameters.Add(prmPOID);

//                MySqlDataReader myReader = (MySqlDataReader) cmd.ExecuteReader(System.Data.CommandBehavior.SingleResult);
				
//                PODetails Details = new PODetails();

//                while (myReader.Read()) 
//                {
//                    Details.POID = POID;
//                    Details.PONo = "" + myReader["PONo"].ToString();
//                    Details.PODate = myReader.GetDateTime("PODate");
//                    Details.SupplierID = myReader.GetInt64("SupplierID");
//                    Details.SupplierCode = "" + myReader["SupplierCode"].ToString();
//                    Details.SupplierContact = "" + myReader["SupplierContact"].ToString();
//                    Details.SupplierAddress = "" + myReader["SupplierAddress"].ToString();
//                    Details.SupplierTelephoneNo = "" + myReader["SupplierTelephoneNo"].ToString();
//                    Details.SupplierModeOfTerms = myReader.GetInt16("SupplierModeofTerms");
//                    Details.SupplierTerms = myReader.GetInt16("SupplierTerms");
//                    Details.RequiredDeliveryDate = myReader.GetDateTime("RequiredDeliveryDate");
//                    Details.BranchID = myReader.GetInt16("BranchID");
//                    Details.BranchCode = "" + myReader["BranchCode"].ToString();
//                    Details.BranchName = "" + myReader["BranchName"].ToString();
//                    Details.BranchAddress = "" + myReader["BranchAddress"].ToString();
//                    Details.PurchaserID = myReader.GetInt64("PurchaserID");
//                    Details.PurchaserName = "" + myReader["PurchaserName"].ToString();
//                    Details.SubTotal = myReader.GetDecimal("SubTotal");
//                    Details.Discount = myReader.GetDecimal("Discount");
//                    Details.DiscountApplied = myReader.GetDecimal("DiscountApplied");
//                    Details.DiscountType = (DiscountTypes)Enum.Parse(typeof(DiscountTypes), myReader.GetString("DiscountType");
//                    Details.VAT = myReader.GetDecimal("VAT");
//                    Details.VatableAmount = myReader.GetDecimal("VatableAmount");
//                    Details.EVAT = myReader.GetDecimal("EVAT");
//                    Details.EVatableAmount = myReader.GetDecimal("EVatableAmount");
//                    Details.LocalTax = myReader.GetDecimal("LocalTax");
//                    Details.Freight = myReader.GetDecimal("Freight");
//                    Details.Deposit = myReader.GetDecimal("Deposit");
//                    Details.PaidAmount = myReader.GetDecimal("PaidAmount");
//                    Details.UnpaidAmount = myReader.GetDecimal("UnpaidAmount");
//                    Details.Status = (POStatus) Enum.Parse(typeof(POStatus), myReader.GetString("Status");
//                    Details.TotalItemDiscount = myReader.GetDecimal("TotalItemDiscount");
//                    Details.Remarks = "" + myReader["Remarks"].ToString();
//                    Details.SupplierDRNo = "" + myReader["SupplierDRNo"].ToString();
//                    Details.DeliveryDate = myReader.GetDateTime("DeliveryDate");
//                    Details.ChartOfAccountIDAPTracking = myReader.GetInt16("ChartOfAccountIDAPTracking");
//                    Details.ChartOfAccountIDAPFreight = myReader.GetInt16("ChartOfAccountIDAPFreight");
//                    Details.ChartOfAccountIDAPVDeposit = myReader.GetInt16("ChartOfAccountIDAPVDeposit");
//                    Details.ChartOfAccountIDAPContra = myReader.GetInt16("ChartOfAccountIDAPContra");
//                    Details.ChartOfAccountIDAPLatePayment = myReader.GetInt16("ChartOfAccountIDAPLatePayment");
//                }

//                myReader.Close();

//                return Details;
//            }

//            catch (Exception ex)
//            {
//                TransactionFailed = true;
//                if (IsInTransaction)
//                {
//                    mTransaction.Rollback();
//                    mTransaction.Dispose(); 
//                    mConnection.Close();
//                    mConnection.Dispose();
//                }

//                throw ex;
//            }	
//        }

//        #endregion

//        #region Streams

//        public System.Data.DataTable ListAsDataTable(POStatus postatus, string SortField, SortOption SortOrder)
//        {
//            if (SortField != string.Empty && SortField != null) SortField = "POID";

//            string SQL = SQLSelect() + "WHERE Status = @Status ORDER BY " + SortField;

//            if (SortOrder == SortOption.Ascending)
//                SQL += " ASC";
//            else
//                SQL += " DESC";

//            MySqlConnection cn = GetConnection();

//            MySqlCommand cmd = new MySqlCommand();
//            cmd.Connection = cn;
//            cmd.Transaction = mTransaction;
//            cmd.CommandType = System.Data.CommandType.Text;
//            cmd.CommandText = SQL;

//            MySqlParameter prmStatus = new MySqlParameter("@Status",MySqlDbType.Int16);
//            prmStatus.Value = postatus.ToString("d");
//            cmd.Parameters.Add(prmStatus);

//            System.Data.DataTable dt = new System.Data.DataTable("PO");
//            MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
//            adapter.Fill(dt);

//            return dt;
//        }

//        public System.Data.DataTable ListAsDataTable(string SortField, SortOption SortOrder)
//        {
//            if (SortField != string.Empty && SortField != null) SortField = "POID";

//            string SQL = SQLSelect() + "ORDER BY " + SortField;

//            if (SortOrder == SortOption.Ascending)
//                SQL += " ASC";
//            else
//                SQL += " DESC";

//            MySqlConnection cn = GetConnection();

//            MySqlCommand cmd = new MySqlCommand();
//            cmd.Connection = cn;
//            cmd.Transaction = mTransaction;
//            cmd.CommandType = System.Data.CommandType.Text;
//            cmd.CommandText = SQL;

//            System.Data.DataTable dt = new System.Data.DataTable("PO");
//            MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
//            adapter.Fill(dt);

//            return dt;
//        }

//        public MySqlDataReader List(long POID, string SortField, SortOption SortOrder)
//        {
//            try
//            {
//                if (SortField != string.Empty && SortField != null) SortField = "POID";

//                string SQL = SQLSelect() + "ORDER BY " + SortField;

//                if (SortOrder == SortOption.Ascending)
//                    SQL += " ASC";
//                else
//                    SQL += " DESC";

//                MySqlConnection cn = GetConnection();

//                MySqlCommand cmd = new MySqlCommand();
//                cmd.Connection = cn;
//                cmd.Transaction = mTransaction;
//                cmd.CommandType = System.Data.CommandType.Text;
//                cmd.CommandText = SQL;
				
//                MySqlDataReader myReader = (MySqlDataReader) cmd.ExecuteReader();
				
//                return myReader;			
//            }
//            catch (Exception ex)
//            {
//                TransactionFailed = true;
//                if (IsInTransaction)
//                {
//                    mTransaction.Rollback();
//                    mTransaction.Dispose(); 
//                    mConnection.Close();
//                    mConnection.Dispose();
//                }

//                throw ex;
//            }	
//        }

//        public MySqlDataReader List(string SortField, SortOption SortOrder)
//        {
//            try
//            {
//                if (SortField != string.Empty && SortField != null) SortField = "POID";

//                string SQL = SQLSelect() + "ORDER BY " + SortField;

//                if (SortOrder == SortOption.Ascending)
//                    SQL += " ASC";
//                else
//                    SQL += " DESC";

//                MySqlConnection cn = GetConnection();

//                MySqlCommand cmd = new MySqlCommand();
//                cmd.Connection = cn;
//                cmd.Transaction = mTransaction;
//                cmd.CommandType = System.Data.CommandType.Text;
//                cmd.CommandText = SQL;
				
//                MySqlDataReader myReader = (MySqlDataReader) cmd.ExecuteReader();
				
//                return myReader;			
//            }
//            catch (Exception ex)
//            {
//                TransactionFailed = true;
//                if (IsInTransaction)
//                {
//                    mTransaction.Rollback();
//                    mTransaction.Dispose(); 
//                    mConnection.Close();
//                    mConnection.Dispose();
//                }

//                throw ex;
//            }	
//        }

//        public MySqlDataReader List(POStatus postatus, string SortField, SortOption SortOrder)
//        {
//            try
//            {
//                if (SortField != string.Empty && SortField != null) SortField = "POID";

//                string SQL = SQLSelect() + "WHERE Status = @Status ORDER BY " + SortField;

//                if (SortOrder == SortOption.Ascending)
//                    SQL += " ASC";
//                else
//                    SQL += " DESC";

//                MySqlConnection cn = GetConnection();

//                MySqlCommand cmd = new MySqlCommand();
//                cmd.Connection = cn;
//                cmd.Transaction = mTransaction;
//                cmd.CommandType = System.Data.CommandType.Text;
//                cmd.CommandText = SQL;
				
//                MySqlParameter prmStatus = new MySqlParameter("@Status",MySqlDbType.Int16);			
//                prmStatus.Value = postatus.ToString("d");
//                cmd.Parameters.Add(prmStatus);

//                MySqlDataReader myReader = (MySqlDataReader) cmd.ExecuteReader();
				
//                return myReader;			
//            }
//            catch (Exception ex)
//            {
//                TransactionFailed = true;
//                if (IsInTransaction)
//                {
//                    mTransaction.Rollback();
//                    mTransaction.Dispose(); 
//                    mConnection.Close();
//                    mConnection.Dispose();
//                }

//                throw ex;
//            }	
//        }

//        public MySqlDataReader List(POStatus postatus, long SupplierID, string SortField, SortOption SortOrder)
//        {
//            try
//            {
//                if (SortField != string.Empty && SortField != null) SortField = "POID";

//                string SQL = SQLSelect() + "WHERE Status =@Status AND SupplierID = @SupplierID ORDER BY " + SortField;

//                if (SortOrder == SortOption.Ascending)
//                    SQL += " ASC";
//                else
//                    SQL += " DESC";

//                MySqlConnection cn = GetConnection();

//                MySqlCommand cmd = new MySqlCommand();
//                cmd.Connection = cn;
//                cmd.Transaction = mTransaction;
//                cmd.CommandType = System.Data.CommandType.Text;
//                cmd.CommandText = SQL;
				
//                MySqlParameter prmStatus = new MySqlParameter("@Status",MySqlDbType.Int16);			
//                prmStatus.Value = postatus.ToString("d");
//                cmd.Parameters.Add(prmStatus);

//                MySqlParameter prmSupplierID = new MySqlParameter("@SupplierID",System.Data.DbType.Int64);			
//                prmSupplierID.Value = SupplierID;
//                cmd.Parameters.Add(prmSupplierID);

//                MySqlDataReader myReader = (MySqlDataReader) cmd.ExecuteReader();
				
//                return myReader;			
//            }
//            catch (Exception ex)
//            {
//                TransactionFailed = true;
//                if (IsInTransaction)
//                {
//                    mTransaction.Rollback();
//                    mTransaction.Dispose(); 
//                    mConnection.Close();
//                    mConnection.Dispose();
//                }

//                throw ex;
//            }	
//        }
//        public MySqlDataReader ListForPayment(long SupplierID, string SortField, SortOption SortOrder)
//        {
//            try
//            {
//                if (SortField != string.Empty && SortField != null) SortField = "POID";

//                string SQL = SQLSelect() + "WHERE PaymentStatus <> @FullyPaidPaymentStatus AND Status =@PostedStatus AND SupplierID = @SupplierID ORDER BY " + SortField;

//                if (SortOrder == SortOption.Ascending)
//                    SQL += " ASC";
//                else
//                    SQL += " DESC";

//                MySqlConnection cn = GetConnection();

//                MySqlCommand cmd = new MySqlCommand();
//                cmd.Connection = cn;
//                cmd.Transaction = mTransaction;
//                cmd.CommandType = System.Data.CommandType.Text;
//                cmd.CommandText = SQL;

//                MySqlParameter prmFullyPaidPaymentStatus = new MySqlParameter("@FullyPaidPaymentStatus",MySqlDbType.Int16);
//                prmFullyPaidPaymentStatus.Value = POPaymentStatus.FullyPaid.ToString("d");
//                cmd.Parameters.Add(prmFullyPaidPaymentStatus);

//                MySqlParameter prmPostedStatus = new MySqlParameter("@PostedStatus",MySqlDbType.Int16);
//                prmPostedStatus.Value = POStatus.Posted.ToString("d");
//                cmd.Parameters.Add(prmPostedStatus);

//                MySqlParameter prmSupplierID = new MySqlParameter("@SupplierID",MySqlDbType.Int64);
//                prmSupplierID.Value = SupplierID;
//                cmd.Parameters.Add(prmSupplierID);

//                MySqlDataReader myReader = (MySqlDataReader)cmd.ExecuteReader();

//                return myReader;
//            }
//            catch (Exception ex)
//            {
//                TransactionFailed = true;
//                if (IsInTransaction)
//                {
//                    mTransaction.Rollback();
//                    mTransaction.Dispose();
//                    mConnection.Close();
//                    mConnection.Dispose();
//                }

//                throw ex;
//            }
//        }
//        public MySqlDataReader Search(string SearchKey, string SortField, SortOption SortOrder)
//        {
//            try
//            {
//                if (SortField != string.Empty && SortField != null) SortField = "POID";

//                string SQL = SQLSelect() + "WHERE (PONo LIKE @SearchKey or PODate LIKE @SearchKey or SupplierCode LIKE @SearchKey " +
//                                        "or SupplierContact LIKE @SearchKey or BranchCode LIKE @SearchKey or RequiredDeliveryDate LIKE @SearchKey) " +
//                                "ORDER BY " + SortField;

//                if (SortOrder == SortOption.Ascending)
//                    SQL += " ASC";
//                else
//                    SQL += " DESC";

//                MySqlConnection cn = GetConnection();

//                MySqlCommand cmd = new MySqlCommand();
//                cmd.Connection = cn;
//                cmd.Transaction = mTransaction;
//                cmd.CommandType = System.Data.CommandType.Text;
//                cmd.CommandText = SQL;
				
//                MySqlParameter prmSearchKey = new MySqlParameter("@SearchKey",MySqlDbType.String);
//                prmSearchKey.Value = "%" + SearchKey + "%";
//                cmd.Parameters.Add(prmSearchKey);

//                MySqlDataReader myReader = (MySqlDataReader) cmd.ExecuteReader();
				
//                return myReader;			
//            }
//            catch (Exception ex)
//            {
//                TransactionFailed = true;
//                if (IsInTransaction)
//                {
//                    mTransaction.Rollback();
//                    mTransaction.Dispose(); 
//                    mConnection.Close();
//                    mConnection.Dispose();
//                }

//                throw ex;
//            }	
//        }		
//        public MySqlDataReader Search(POStatus postatus, string SearchKey, string SortField, SortOption SortOrder)
//        {
//            try
//            {
//                if (SortField != string.Empty && SortField != null) SortField = "POID";

//                string SQL = SQLSelect() + "WHERE Status = @Status AND (PONo LIKE @SearchKey or PODate LIKE @SearchKey or SupplierCode LIKE @SearchKey " +
//                                        "or SupplierContact LIKE @SearchKey or BranchCode LIKE @SearchKey or RequiredDeliveryDate LIKE @SearchKey) " +
//                            "ORDER BY " + SortField;

//                if (SortOrder == SortOption.Ascending)
//                    SQL += " ASC";
//                else
//                    SQL += " DESC";

//                MySqlConnection cn = GetConnection();

//                MySqlCommand cmd = new MySqlCommand();
//                cmd.Connection = cn;
//                cmd.Transaction = mTransaction;
//                cmd.CommandType = System.Data.CommandType.Text;
//                cmd.CommandText = SQL;
				
//                MySqlParameter prmStatus = new MySqlParameter("@Status",MySqlDbType.Int16);			
//                prmStatus.Value = postatus.ToString("d");
//                cmd.Parameters.Add(prmStatus);

//                MySqlParameter prmSearchKey = new MySqlParameter("@SearchKey",MySqlDbType.String);
//                prmSearchKey.Value = "%" + SearchKey + "%";
//                cmd.Parameters.Add(prmSearchKey);

//                MySqlDataReader myReader = (MySqlDataReader) cmd.ExecuteReader();
				
//                return myReader;			
//            }
//            catch (Exception ex)
//            {
//                TransactionFailed = true;
//                if (IsInTransaction)
//                {
//                    mTransaction.Rollback();
//                    mTransaction.Dispose(); 
//                    mConnection.Close();
//                    mConnection.Dispose();
//                }

//                throw ex;
//            }	
//        }
//        public System.Data.DataTable SearchAsDataTable(POStatus postatus, string SearchKey, string SortField, SortOption SortOrder)
//        {
//            try
//            {
//                if (SortField != string.Empty && SortField != null) SortField = "POID";

//                string SQL = SQLSelect() + "WHERE Status = @Status AND (PONo LIKE @SearchKey or PODate LIKE @SearchKey or SupplierCode LIKE @SearchKey " +
//                                        "or SupplierContact LIKE @SearchKey or BranchCode LIKE @SearchKey or RequiredDeliveryDate LIKE @SearchKey) " +
//                            "ORDER BY " + SortField;

//                if (SortOrder == SortOption.Ascending)
//                    SQL += " ASC";
//                else
//                    SQL += " DESC";

//                MySqlConnection cn = GetConnection();

//                MySqlCommand cmd = new MySqlCommand();
//                cmd.Connection = cn;
//                cmd.Transaction = mTransaction;
//                cmd.CommandType = System.Data.CommandType.Text;
//                cmd.CommandText = SQL;

//                MySqlParameter prmStatus = new MySqlParameter("@Status",MySqlDbType.Int16);
//                prmStatus.Value = postatus.ToString("d");
//                cmd.Parameters.Add(prmStatus);

//                MySqlParameter prmSearchKey = new MySqlParameter("@SearchKey",MySqlDbType.String);
//                prmSearchKey.Value = "%" + SearchKey + "%";
//                cmd.Parameters.Add(prmSearchKey);

//                System.Data.DataTable dt = new System.Data.DataTable("PO");
//                MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
//                adapter.Fill(dt);

//                return dt;
//            }
//            catch (Exception ex)
//            {
//                TransactionFailed = true;
//                if (IsInTransaction)
//                {
//                    mTransaction.Rollback();
//                    mTransaction.Dispose();
//                    mConnection.Close();
//                    mConnection.Dispose();
//                }

//                throw ex;
//            }
//        }	
//        public MySqlDataReader List(POStatus postatus, DateTime StartDate, DateTime EndDate)
//        {
//            try
//            {
//                string SQL = SQLSelect() + "WHERE Status = @Status AND DeliveryDate BETWEEN @StartDate AND @EndDate ORDER BY POID ASC";

//                MySqlConnection cn = GetConnection();

//                MySqlCommand cmd = new MySqlCommand();
//                cmd.Connection = cn;
//                cmd.Transaction = mTransaction;
//                cmd.CommandType = System.Data.CommandType.Text;
//                cmd.CommandText = SQL;
				
//                MySqlParameter prmStartDate = new MySqlParameter("@StartDate",MySqlDbType.DateTime);			
//                prmStartDate.Value = StartDate.ToString("yyyy-MM-dd HH:mm:ss");
//                cmd.Parameters.Add(prmStartDate);

//                MySqlParameter prmEndDate = new MySqlParameter("@EndDate",MySqlDbType.DateTime);			
//                prmEndDate.Value = EndDate.ToString("yyyy-MM-dd HH:mm:ss");
//                cmd.Parameters.Add(prmEndDate);

//                MySqlParameter prmStatus = new MySqlParameter("@Status",MySqlDbType.Int16);			
//                prmStatus.Value = postatus.ToString("d");
//                cmd.Parameters.Add(prmStatus);

//                MySqlDataReader myReader = (MySqlDataReader) cmd.ExecuteReader();
				
//                return myReader;			
//            }
//            catch (Exception ex)
//            {
//                TransactionFailed = true;
//                if (IsInTransaction)
//                {
//                    mTransaction.Rollback();
//                    mTransaction.Dispose(); 
//                    mConnection.Close();
//                    mConnection.Dispose();
//                }

//                throw ex;
//            }	
//        }
		
//        #endregion

//        #region Public Modifiers

//        public string LastTransactionNo()
//        {
//            try
//            {
//                string stRetValue = String.Empty;
				
//                ERPConfig clsERPConfig = new ERPConfig(Connection, Transaction);
//                stRetValue = clsERPConfig.get_LastPONo();

//                return stRetValue;
//            }

//            catch (Exception ex)
//            {
//                TransactionFailed = true;
//                if (IsInTransaction)
//                {
//                    mTransaction.Rollback();
//                    mTransaction.Dispose(); 
//                    mConnection.Close();
//                    mConnection.Dispose();
//                }

//                throw ex;
//            }	
//        }
//        public void SynchronizeAmount(long POID)
//        {
//            try
//            {
//                string SQL = "CALL procPOSynchronizeAmount(@POID);";

//                MySqlConnection cn = GetConnection();

//                MySqlCommand cmd = new MySqlCommand();
//                cmd.Connection = cn;
//                cmd.Transaction = mTransaction;
//                cmd.CommandType = System.Data.CommandType.Text;
//                cmd.CommandText = SQL;

//                MySqlParameter prmPOID = new MySqlParameter("@POID",MySqlDbType.Int64);
//                prmPOID.Value = POID;
//                cmd.Parameters.Add(prmPOID);

//                cmd.ExecuteNonQuery();
//            }

//            catch (Exception ex)
//            {
//                TransactionFailed = true;
//                if (IsInTransaction)
//                {
//                    mTransaction.Rollback();
//                    mTransaction.Dispose();
//                    mConnection.Close();
//                    mConnection.Dispose();
//                }

//                throw ex;
//            }
//        }

//    #endregion

//    }
//}


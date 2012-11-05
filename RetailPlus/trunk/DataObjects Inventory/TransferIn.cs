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

    #region TransferInDetails

    public struct TransferInDetails
    {
        public long TransferInID;
        public string TransferInNo;
        public DateTime TransferInDate;
        public long SupplierID;
        public string SupplierCode;
        public string SupplierContact;
        public string SupplierAddress;
        public string SupplierTelephoneNo;
        public int SupplierModeOfTerms;
        public int SupplierTerms;
        public DateTime RequiredDeliveryDate;
        public int BranchID;
        public string BranchCode;
        public string BranchName;
        public string BranchAddress;
        public long TransferrerID;
        public string TransferrerName;
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
        public TransferInStatus Status;
        public bool IsVatInclusive;
        public string Remarks;
        public string SupplierDRNo;
        public DateTime DeliveryDate;
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
    public class TransferIn
    {
        MySqlConnection mConnection;
        MySqlTransaction mTransaction;
        bool IsInTransaction = false;
        bool TransactionFailed = false;

        public MySqlConnection Connection
        {
            get { return mConnection; }
        }

        public MySqlTransaction Transaction
        {
            get { return mTransaction; }
        }


        #region Constructors and Destructors

        public TransferIn()
        {

        }

        public TransferIn(MySqlConnection Connection, MySqlTransaction Transaction)
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
            if (mConnection == null)
            {
                mConnection = new MySqlConnection(AceSoft.RetailPlus.DBConnection.ConnectionString());
                mConnection.Open();

                mTransaction = (MySqlTransaction)mConnection.BeginTransaction();
            }

            IsInTransaction = true;
            return mConnection;
        }


        #endregion

        #region Insert and Update

        public long Insert(TransferInDetails Details)
        {
            try
            {
                ERPConfig clsERPConfig = new ERPConfig(mConnection, mTransaction);
                APLinkConfigDetails clsAPLinkConfigDetails = clsERPConfig.APLinkDetails();

                string SQL = "INSERT INTO tblTransferIn (" +
                                "TransferInNo, " +
                                "TransferInDate, " +
                                "SupplierID, " +
                                "SupplierCode, " +
                                "SupplierContact, " +
                                "SupplierAddress, " +
                                "SupplierTelephoneNo, " +
                                "SupplierModeOfTerms, " +
                                "SupplierTerms, " +
                                "RequiredDeliveryDate, " +
                                "BranchID, " +
                                "TransferrerID, " +
                                "TransferrerName, " +
                                "Status, " +
                                "Remarks, " +
                                "ChartOfAccountIDAPTracking, " +
                                "ChartOfAccountIDAPBills, " +
                                "ChartOfAccountIDAPFreight, " +
                                "ChartOfAccountIDAPVDeposit, " +
                                "ChartOfAccountIDAPContra, " +
                                "ChartOfAccountIDAPLatePayment" +
                            ") VALUES (" +
                                "@TransferInNo, " +
                                "@TransferInDate, " +
                                "@SupplierID, " +
                                "@SupplierCode, " +
                                "@SupplierContact, " +
                                "@SupplierAddress, " +
                                "@SupplierTelephoneNo, " +
                                "@SupplierModeOfTerms, " +
                                "@SupplierTerms, " +
                                "@RequiredDeliveryDate, " +
                                "@BranchID, " +
                                "@TransferrerID, " +
                                "@TransferrerName, " +
                                "@Status, " +
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

                MySqlParameter prmTransferInNo = new MySqlParameter("@TransferInNo",MySqlDbType.String);
                prmTransferInNo.Value = Details.TransferInNo;
                cmd.Parameters.Add(prmTransferInNo);

                MySqlParameter prmTransferInDate = new MySqlParameter("@TransferInDate",MySqlDbType.DateTime);
                prmTransferInDate.Value = Details.TransferInDate.ToString("yyyy-MM-dd HH:mm:ss");
                cmd.Parameters.Add(prmTransferInDate);

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

                MySqlParameter prmRequiredDeliveryDate = new MySqlParameter("@RequiredDeliveryDate",MySqlDbType.DateTime);
                prmRequiredDeliveryDate.Value = Details.RequiredDeliveryDate.ToString("yyyy-MM-dd HH:mm:ss");
                cmd.Parameters.Add(prmRequiredDeliveryDate);

                MySqlParameter prmBranchID = new MySqlParameter("@BranchID",MySqlDbType.Int16);
                prmBranchID.Value = Details.BranchID;
                cmd.Parameters.Add(prmBranchID);

                MySqlParameter prmTransferrerID = new MySqlParameter("@TransferrerID",MySqlDbType.Int64);
                prmTransferrerID.Value = Details.TransferrerID;
                cmd.Parameters.Add(prmTransferrerID);

                MySqlParameter prmTransferrerName = new MySqlParameter("@TransferrerName",MySqlDbType.String);
                prmTransferrerName.Value = Details.TransferrerName;
                cmd.Parameters.Add(prmTransferrerName);

                MySqlParameter prmStatus = new MySqlParameter("@Status",MySqlDbType.Int16);
                prmStatus.Value = Details.Status.ToString("d");
                cmd.Parameters.Add(prmStatus);

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

                MySqlDataReader myReader = (MySqlDataReader)cmd.ExecuteReader(System.Data.CommandBehavior.SingleResult);

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
        public void Update(TransferInDetails Details)
        {
            try
            {
                ERPConfig clsERPConfig = new ERPConfig(mConnection, mTransaction);
                APLinkConfigDetails clsAPLinkConfigDetails = clsERPConfig.APLinkDetails();

                string SQL = "UPDATE tblTransferIn SET " +
                                "TransferInNo					=	@TransferInNo, " +
                                "TransferInDate					=	@TransferInDate, " +
                                "SupplierID				=	@SupplierID, " +
                                "SupplierCode			=	@SupplierCode, " +
                                "SupplierContact		=	@SupplierContact, " +
                                "SupplierAddress		=	@SupplierAddress, " +
                                "SupplierTelephoneNo	=	@SupplierTelephoneNo, " +
                                "SupplierModeOfTerms	=	@SupplierModeOfTerms, " +
                                "SupplierTerms			=	@SupplierTerms, " +
                                "RequiredDeliveryDate	=	@RequiredDeliveryDate, " +
                                "BranchID				=	@BranchID, " +
                                "TransferrerID			=	@TransferrerID, " +
                                "TransferrerName          =   @TransferrerName, " +
                                "Remarks                =   @Remarks, " +
                                "ChartOfAccountIDAPTracking     = @ChartOfAccountIDAPTracking, " +
                                "ChartOfAccountIDAPBills        = @ChartOfAccountIDAPBills, " +
                                "ChartOfAccountIDAPFreight      = @ChartOfAccountIDAPFreight, " +
                                "ChartOfAccountIDAPVDeposit     = @ChartOfAccountIDAPVDeposit, " +
                                "ChartOfAccountIDAPContra       = @ChartOfAccountIDAPContra, " +
                                "ChartOfAccountIDAPLatePayment  = @ChartOfAccountIDAPLatePayment " +
                            "WHERE TransferInID = @TransferInID;";

                MySqlConnection cn = GetConnection();

                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = cn;
                cmd.Transaction = mTransaction;
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandText = SQL;

                MySqlParameter prmTransferInNo = new MySqlParameter("@TransferInNo",MySqlDbType.String);
                prmTransferInNo.Value = Details.TransferInNo;
                cmd.Parameters.Add(prmTransferInNo);

                MySqlParameter prmTransferInDate = new MySqlParameter("@TransferInDate",MySqlDbType.DateTime);
                prmTransferInDate.Value = Details.TransferInDate.ToString("yyyy-MM-dd HH:mm:ss");
                cmd.Parameters.Add(prmTransferInDate);

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

                MySqlParameter prmRequiredDeliveryDate = new MySqlParameter("@RequiredDeliveryDate",MySqlDbType.DateTime);
                prmRequiredDeliveryDate.Value = Details.RequiredDeliveryDate.ToString("yyyy-MM-dd HH:mm:ss");
                cmd.Parameters.Add(prmRequiredDeliveryDate);

                MySqlParameter prmBranchID = new MySqlParameter("@BranchID",MySqlDbType.Int16);
                prmBranchID.Value = Details.BranchID;
                cmd.Parameters.Add(prmBranchID);

                MySqlParameter prmTransferrerID = new MySqlParameter("@TransferrerID",MySqlDbType.Int64);
                prmTransferrerID.Value = Details.TransferrerID;
                cmd.Parameters.Add(prmTransferrerID);

                MySqlParameter prmTransferrerName = new MySqlParameter("@TransferrerName",MySqlDbType.String);
                prmTransferrerName.Value = Details.TransferrerName;
                cmd.Parameters.Add(prmTransferrerName);

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

                MySqlParameter prmTransferInID = new MySqlParameter("@TransferInID",MySqlDbType.Int64);
                prmTransferInID.Value = Details.TransferInID;
                cmd.Parameters.Add(prmTransferInID);

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

        public void UpdateIsVatInclusive(long TransferInID, bool IsVatInclusive)
        {
            try
            {
                string SQL = "UPDATE tblTransferIn SET " +
                                "IsVatInclusive          =   @IsVatInclusive " +
                            "WHERE TransferInID = @TransferInID;";

                MySqlConnection cn = GetConnection();

                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = cn;
                cmd.Transaction = mTransaction;
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandText = SQL;

                MySqlParameter prmIsVatInclusive = new MySqlParameter("@IsVatInclusive",MySqlDbType.Int16);
                prmIsVatInclusive.Value = Convert.ToInt16(IsVatInclusive); ;
                cmd.Parameters.Add(prmIsVatInclusive);

                MySqlParameter prmTransferInID = new MySqlParameter("@TransferInID",MySqlDbType.Int64);
                prmTransferInID.Value = TransferInID;
                cmd.Parameters.Add(prmTransferInID);

                cmd.ExecuteNonQuery();

                SynchronizeAmount(TransferInID);
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

        public void UpdateDiscount(long TransferInID, decimal DiscountApplied, DiscountTypes DiscountType, decimal Discount2Applied, DiscountTypes Discount2Type, decimal Discount3Applied, DiscountTypes Discount3Type)
        {
            try
            {
                string SQL = "UPDATE tblTransferIn SET " +
                                "DiscountApplied        =   @DiscountApplied, " +
                                "DiscountType           =   @DiscountType, " +
                                "Discount2Applied       =   @Discount2Applied, " +
                                "Discount2Type          =   @Discount2Type, " +
                                "Discount3Applied       =   @Discount3Applied, " +
                                "Discount3Type          =   @Discount3Type " +
                            "WHERE TransferInID = @TransferInID;";

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

                MySqlParameter prmTransferInID = new MySqlParameter("@TransferInID",MySqlDbType.Int64);
                prmTransferInID.Value = TransferInID;
                cmd.Parameters.Add(prmTransferInID);

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
        public void UpdateDiscountFreightDeposit(long TransferInID, decimal DiscountApplied, DiscountTypes DiscountType)
        {
            try
            {
                string SQL = "UPDATE tblTransferIn SET " +
                                "DiscountApplied        =   @DiscountApplied, " +
                                "DiscountType           =   @DiscountType " +
                            "WHERE TransferInID = @TransferInID;";

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

                MySqlParameter prmTransferInID = new MySqlParameter("@TransferInID",MySqlDbType.Int64);
                prmTransferInID.Value = TransferInID;
                cmd.Parameters.Add(prmTransferInID);

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
        public void UpdateFreight(long TransferInID, decimal Freight)
        {
            try
            {
                string SQL = "UPDATE tblTransferIn SET " +
                                "Freight           =   @Freight " +
                            "WHERE TransferInID = @TransferInID;";

                MySqlConnection cn = GetConnection();

                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = cn;
                cmd.Transaction = mTransaction;
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandText = SQL;

                MySqlParameter prmFreight = new MySqlParameter("@Freight", System.Data.DbType.Decimal);
                prmFreight.Value = Freight;
                cmd.Parameters.Add(prmFreight);

                MySqlParameter prmTransferInID = new MySqlParameter("@TransferInID",MySqlDbType.Int64);
                prmTransferInID.Value = TransferInID;
                cmd.Parameters.Add(prmTransferInID);

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
        public void UpdateDeposit(long TransferInID, decimal Deposit)
        {
            try
            {
                string SQL = "UPDATE tblTransferIn SET " +
                                "Deposit           =   @Deposit " +
                            "WHERE TransferInID = @TransferInID;";

                MySqlConnection cn = GetConnection();

                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = cn;
                cmd.Transaction = mTransaction;
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandText = SQL;

                MySqlParameter prmDeposit = new MySqlParameter("@Deposit", System.Data.DbType.Decimal);
                prmDeposit.Value = Deposit;
                cmd.Parameters.Add(prmDeposit);

                MySqlParameter prmTransferInID = new MySqlParameter("@TransferInID",MySqlDbType.Int64);
                prmTransferInID.Value = TransferInID;
                cmd.Parameters.Add(prmTransferInID);

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

        public void IssueGRN(long TransferInID, string SupplierDRNo, DateTime DeliveryDate)
        {
            try
            {
                string SQL = "UPDATE tblTransferIn SET " +
                                "SupplierDRNo			=	@SupplierDRNo, " +
                                "DeliveryDate			=	@DeliveryDate, " +
                                "Status				    =	@Status " +
                            "WHERE TransferInID = @TransferInID;";

                MySqlConnection cn = GetConnection();

                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = cn;
                cmd.Transaction = mTransaction;
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandText = SQL;

                MySqlParameter prmSupplierDRNo = new MySqlParameter("@SupplierDRNo",MySqlDbType.String);
                prmSupplierDRNo.Value = SupplierDRNo;
                cmd.Parameters.Add(prmSupplierDRNo);

                MySqlParameter prmDeliveryDate = new MySqlParameter("@DeliveryDate",MySqlDbType.DateTime);
                prmDeliveryDate.Value = DeliveryDate.ToString("yyyy-MM-dd HH:mm:ss");
                cmd.Parameters.Add(prmDeliveryDate);

                MySqlParameter prmStatus = new MySqlParameter("@Status",MySqlDbType.Int16);
                prmStatus.Value = TransferInStatus.Posted.ToString("d");
                cmd.Parameters.Add(prmStatus);

                MySqlParameter prmTransferInID = new MySqlParameter("@TransferInID",MySqlDbType.Int64);
                prmTransferInID.Value = TransferInID;
                cmd.Parameters.Add(prmTransferInID);

                cmd.ExecuteNonQuery();

                /*******************************************
                 * Update the status of items
                 * ****************************************/
                TransferInItem clsTransferInItem = new TransferInItem(mConnection, mTransaction);
                clsTransferInItem.Post(TransferInID);

                /*******************************************
                 * Update Vendor Account
                 * ****************************************/
                AddItemToInventory(TransferInID);

                /*******************************************
				 * Update Account Balance
				 * ****************************************/
                UpdateAccounts(TransferInID);
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

        private void UpdateAccounts(long TransferInID)
        {
            try
            {
                TransferInDetails clsTransferInDetails = Details(TransferInID);
                ChartOfAccount clsChartOfAccount = new ChartOfAccount(mConnection, mTransaction);

                // update ChartOfAccountIDAPTracking as credit
                clsChartOfAccount.UpdateCredit(clsTransferInDetails.ChartOfAccountIDAPTracking, clsTransferInDetails.SubTotal);

                // update Deposit & APContra
                clsChartOfAccount.UpdateCredit(clsTransferInDetails.ChartOfAccountIDAPContra, clsTransferInDetails.Discount);

                // update Freight & APTracking
                clsChartOfAccount.UpdateCredit(clsTransferInDetails.ChartOfAccountIDAPTracking, clsTransferInDetails.Freight);
                clsChartOfAccount.UpdateDebit(clsTransferInDetails.ChartOfAccountIDAPFreight, clsTransferInDetails.Freight);

                // update Deposit & APTracking
                clsChartOfAccount.UpdateCredit(clsTransferInDetails.ChartOfAccountIDAPTracking, clsTransferInDetails.Deposit);
                clsChartOfAccount.UpdateDebit(clsTransferInDetails.ChartOfAccountIDAPVDeposit, clsTransferInDetails.Deposit);

                TransferInItem clsTransferInItem = new TransferInItem(mConnection, mTransaction);
                MySqlDataReader myReader = clsTransferInItem.List(TransferInID, "TransferInItemID", SortOption.Ascending);
                while (myReader.Read())
                {
                    int iChartOfAccountIDTransferIn = myReader.GetInt16("ChartOfAccountIDTransferIn");
                    int iChartOfAccountIDTaxTransferIn = myReader.GetInt16("ChartOfAccountIDTaxTransferIn");

                    decimal decVAT = myReader.GetDecimal("VAT");
                    decimal decVATABLEAmount = myReader.GetDecimal("Amount") - decVAT;

                    // update purchase as debit
                    clsChartOfAccount.UpdateDebit(iChartOfAccountIDTransferIn, decVATABLEAmount);
                    // update tax as debit
                    clsChartOfAccount.UpdateDebit(iChartOfAccountIDTaxTransferIn, decVAT);

                }
                myReader.Close();

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

        private void AddItemToInventory(long TransferInID)
        {

            TransferInDetails clsTransferInDetails = Details(TransferInID);
            ERPConfig clsERPConfig = new ERPConfig(mConnection, mTransaction);
            ERPConfigDetails clsERPConfigDetails = clsERPConfig.Details();

            TransferInItem clsTransferInItem = new TransferInItem(Connection, Transaction);
            ProductUnit clsProductUnit = new ProductUnit(Connection, Transaction);
            Product clsProduct = new Product(mConnection, mTransaction);
            ProductVariationsMatrix clsProductVariationsMatrix = new ProductVariationsMatrix(mConnection, mTransaction);
            ProductPackage clsProductPackage = new ProductPackage(mConnection, mTransaction);
            MatrixPackage clsMatrixPackage = new MatrixPackage(mConnection, mTransaction);

            Inventory clsInventory = new Inventory(Connection, Transaction);
            InventoryDetails clsInventoryDetails;

            MatrixPackagePriceHistoryDetails clsMatrixPackagePriceHistoryDetails;
            ProductPackagePriceHistoryDetails clsProductPackagePriceHistoryDetails;

            MySqlDataReader myReader = clsTransferInItem.List(TransferInID, "TransferInItemID", SortOption.Ascending);

            while (myReader.Read())
            {
                long lngProductID = myReader.GetInt64("ProductID");
                int intProductUnitID = myReader.GetInt16("ProductUnitID");

                decimal decItemQuantity = myReader.GetDecimal("Quantity");
                decimal decQuantity = clsProductUnit.GetBaseUnitValue(lngProductID, intProductUnitID, decItemQuantity);

                long lngVariationMatrixID = myReader.GetInt64("VariationMatrixID");
                string strMatrixDescription = "" + myReader["MatrixDescription"].ToString();
                string strProductCode = "" + myReader["ProductCode"].ToString();
                decimal decUnitCost = myReader.GetDecimal("UnitCost");
                decimal decItemCost = myReader.GetDecimal("Amount");
                decimal decVAT = myReader.GetDecimal("VAT");

                /*******************************************
				 * Add in the Purchase Price History
				 * ****************************************/
                if (lngVariationMatrixID != 0)
                {
                    // Update MatrixPackagePriceHistory first to get the history
                    clsMatrixPackagePriceHistoryDetails = new MatrixPackagePriceHistoryDetails();
                    clsMatrixPackagePriceHistoryDetails.UID = clsTransferInDetails.TransferrerID;
                    clsMatrixPackagePriceHistoryDetails.PackageID = clsMatrixPackage.GetPackageID(lngVariationMatrixID, intProductUnitID);
                    clsMatrixPackagePriceHistoryDetails.ChangeDate = DateTime.Now;
                    clsMatrixPackagePriceHistoryDetails.PurchasePrice = (decItemQuantity * decUnitCost) / decQuantity;
                    clsMatrixPackagePriceHistoryDetails.Price = -1;
                    clsMatrixPackagePriceHistoryDetails.VAT = -1;
                    clsMatrixPackagePriceHistoryDetails.EVAT = -1;
                    clsMatrixPackagePriceHistoryDetails.LocalTax = -1;
                    clsMatrixPackagePriceHistoryDetails.Remarks = "Based on TransferIn #: " + clsTransferInDetails.TransferInNo;
                    MatrixPackagePriceHistory clsMatrixPackagePriceHistory = new MatrixPackagePriceHistory(mConnection, mTransaction);
                    clsMatrixPackagePriceHistory.Insert(clsMatrixPackagePriceHistoryDetails);
                }
                else
                {
                    // Update ProductPackagePriceHistory first to get the history
                    clsProductPackagePriceHistoryDetails = new ProductPackagePriceHistoryDetails();
                    clsProductPackagePriceHistoryDetails.UID = clsTransferInDetails.TransferrerID;
                    clsProductPackagePriceHistoryDetails.PackageID = clsProductPackage.GetPackageID(lngProductID, intProductUnitID);
                    clsProductPackagePriceHistoryDetails.ChangeDate = DateTime.Now;
                    clsProductPackagePriceHistoryDetails.PurchasePrice = (decItemQuantity * decUnitCost) / decQuantity;
                    clsProductPackagePriceHistoryDetails.Price = -1;
                    clsProductPackagePriceHistoryDetails.VAT = -1;
                    clsProductPackagePriceHistoryDetails.EVAT = -1;
                    clsProductPackagePriceHistoryDetails.LocalTax = -1;
                    clsProductPackagePriceHistoryDetails.Remarks = "Based on TransferIn #: " + clsTransferInDetails.TransferInNo;
                    ProductPackagePriceHistory clsProductPackagePriceHistory = new ProductPackagePriceHistory(mConnection, mTransaction);
                    clsProductPackagePriceHistory.Insert(clsProductPackagePriceHistoryDetails);
                }

                /*******************************************
                 * Add to Inventory
                 * ****************************************/
                //clsProduct.AddQuantity(lngProductID, decQuantity);
                //if (lngVariationMatrixID != 0)
                //{
                //    clsProductVariationsMatrix.AddQuantity(lngVariationMatrixID, decQuantity);
                //}
                // July 26, 2011: change the above codes to the following
                clsProduct.AddQuantity(clsTransferInDetails.BranchID, lngProductID, lngVariationMatrixID, decQuantity, Product.getPRODUCT_INVENTORY_MOVEMENT_VALUE(PRODUCT_INVENTORY_MOVEMENT.ADD_TRANSFER_IN), DateTime.Now, clsTransferInDetails.TransferInNo, clsTransferInDetails.TransferrerName);

                /*******************************************
                 * Update Purchasing Information
                 * ****************************************/
                clsProduct.UpdatePurchasing(lngProductID, clsTransferInDetails.SupplierID, intProductUnitID, (decItemQuantity * decUnitCost) / decQuantity);
                if (lngVariationMatrixID != 0)
                {
                    clsProductVariationsMatrix.UpdatePurchasing(lngVariationMatrixID, clsTransferInDetails.SupplierID, intProductUnitID, (decItemQuantity * decUnitCost) / decQuantity);
                }

                /*******************************************
                 * Add to Inventory Analysis
                 * ****************************************/
                clsInventoryDetails = new InventoryDetails();
                clsInventoryDetails.PostingDateFrom = clsERPConfigDetails.PostingDateFrom;
                clsInventoryDetails.PostingDateTo = clsERPConfigDetails.PostingDateTo;
                clsInventoryDetails.PostingDate = clsTransferInDetails.DeliveryDate;
                clsInventoryDetails.ReferenceNo = clsTransferInDetails.TransferInNo;
                clsInventoryDetails.ContactID = clsTransferInDetails.SupplierID;
                clsInventoryDetails.ContactCode = clsTransferInDetails.SupplierCode;
                clsInventoryDetails.ProductID = lngProductID;
                clsInventoryDetails.ProductCode = strProductCode;
                clsInventoryDetails.VariationMatrixID = lngVariationMatrixID;
                clsInventoryDetails.MatrixDescription = strMatrixDescription;
                clsInventoryDetails.TransferInQuantity = decQuantity;
                clsInventoryDetails.TransferInCost = decItemCost - decVAT;
                clsInventoryDetails.TransferInVAT = decItemCost;	// TransferIn Cost with VAT

                clsInventory.Insert(clsInventoryDetails);

                /*******************************************
				 * Added April 28, 2010 4:20PM
                 * Update Selling Information when TransferIn is posted
				 * ****************************************/
                clsProduct.UpdateSellingIncludingAllMatrixWithSameQuantityAndUnit(lngProductID, clsTransferInDetails.SupplierID, intProductUnitID, myReader.GetDecimal("SellingPrice"));
                //if (lngVariationMatrixID != 0)
                //{
                //    clsProductVariationsMatrix.UpdateSellingWithSameQuantityAndUnit(lngVariationMatrixID, clsPODetails.SupplierID, intProductUnitID, decimal.Parse(myReader["SellingPrice");
                //}

                /*******************************************
				 * Added April 28, 2010 4:20PM
                 * Update the purchase price history to check who has the lowest price.
				 * ****************************************/
                ProductPurchasePriceHistoryDetails clsProductPurchasePriceHistoryDetails = new ProductPurchasePriceHistoryDetails();
                clsProductPurchasePriceHistoryDetails.ProductID = lngProductID;
                clsProductPurchasePriceHistoryDetails.MatrixID = lngVariationMatrixID;
                clsProductPurchasePriceHistoryDetails.SupplierID = clsTransferInDetails.SupplierID;
                clsProductPurchasePriceHistoryDetails.PurchasePrice = decUnitCost;
                clsProductPurchasePriceHistoryDetails.PurchaseDate = clsTransferInDetails.TransferInDate;
                clsProductPurchasePriceHistoryDetails.Remarks = clsTransferInDetails.TransferInNo;
                ProductPurchasePriceHistory clsProductPurchasePriceHistory = new ProductPurchasePriceHistory(mConnection, mTransaction);
                clsProductPurchasePriceHistory.AddToList(clsProductPurchasePriceHistoryDetails);
            }
            myReader.Close();

        }
        public void Cancel(long TransferInID, DateTime CancelledDate, string Remarks, long CancelledByID)
        {
            try
            {
                string SQL = "UPDATE tblTransferIn SET " +
                                "CancelledDate			=	@CancelledDate, " +
                                "CancelledRemarks		=	@CancelledRemarks, " +
                                "CancelledByID			=	@CancelledByID, " +
                                "Status				    =	@Status " +
                            "WHERE TransferInID = @TransferInID;";

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

                MySqlParameter prmCancelledByID = new MySqlParameter("@CancelledByID",MySqlDbType.Int64);
                prmCancelledByID.Value = CancelledByID;
                cmd.Parameters.Add(prmCancelledByID);

                MySqlParameter prmStatus = new MySqlParameter("@Status",MySqlDbType.Int16);
                prmStatus.Value = TransferInStatus.Cancelled.ToString("d");
                cmd.Parameters.Add(prmStatus);

                MySqlParameter prmTransferInID = new MySqlParameter("@TransferInID",MySqlDbType.Int64);
                prmTransferInID.Value = TransferInID;
                cmd.Parameters.Add(prmTransferInID);

                cmd.ExecuteNonQuery();

                /*******************************************
                 * Update the status of items
                 * ****************************************/
                TransferInItem clsTransferInItem = new TransferInItem(mConnection, mTransaction);
                clsTransferInItem.Cancel(TransferInID);

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
        public void GenerateItemsForReorder(long TransferInID)
        {
            try
            {
                GetConnection();

                Terminal clsTerminal = new Terminal(Connection, Transaction);
                TerminalDetails clsTerminalDetails = clsTerminal.Details(Terminal.DEFAULT_TERMINAL_NO_ID);

                TransferInDetails clsTransferInDetails = Details(TransferInID);

                Product clsProduct = new Product(Connection, Transaction);
                System.Data.DataTable dt = clsProduct.ForReorder(clsTransferInDetails.SupplierID);

                TransferInItem clsTransferInItem = new TransferInItem(Connection, Transaction);
                ProductVariationsMatrix clsProductVariationsMatrix = new ProductVariationsMatrix(Connection, Transaction);

                foreach (System.Data.DataRow dr in dt.Rows)
                {
                    TransferInItemDetails clsDetails = new TransferInItemDetails();

                    clsDetails.TransferInID = TransferInID;
                    clsDetails.ProductID = Convert.ToInt64(dr["ProductID"].ToString());
                    clsDetails.ProductCode = dr["ProductCode"].ToString();
                    clsDetails.BarCode = dr["BarCode"].ToString();
                    clsDetails.Description = dr["ProductDesc"].ToString();
                    clsDetails.ProductGroup = dr["ProductGroupCode"].ToString();
                    clsDetails.ProductSubGroup = dr["ProductSubGroupCode"].ToString();
                    clsDetails.ProductUnitID = Convert.ToInt32(dr["UnitID"].ToString());
                    clsDetails.ProductUnitCode = dr["UnitName"].ToString();
                    clsDetails.Quantity = Convert.ToDecimal(dr["ReorderQty"].ToString());
                    clsDetails.UnitCost = Convert.ToDecimal(dr["Price"].ToString());
                    clsDetails.Discount = 0;
                    clsDetails.DiscountApplied = 0;
                    clsDetails.DiscountType = DiscountTypes.Percentage;
                    clsDetails.Remarks = "added using auto generation";

                    decimal amount = clsDetails.Quantity * clsDetails.UnitCost;

                    if (Convert.ToDecimal(dr["VAT"]) > 0)
                    {
                        clsDetails.VatableAmount = amount;
                        clsDetails.EVatableAmount = amount;
                        clsDetails.LocalTax = amount;

                        clsDetails.VAT = clsDetails.VatableAmount * (clsTerminalDetails.VAT / 100);
                        clsDetails.EVAT = clsDetails.EVatableAmount * (clsTerminalDetails.EVAT / 100);
                        clsDetails.LocalTax = clsDetails.LocalTax * (clsTerminalDetails.LocalTax / 100);
                        clsDetails.IsVatable = true;
                    }
                    else
                    {
                        clsDetails.VAT = 0;
                        clsDetails.VatableAmount = 0;
                        clsDetails.EVAT = 0;
                        clsDetails.EVatableAmount = 0;
                        clsDetails.LocalTax = 0;
                        clsDetails.IsVatable = false;
                    }
                    clsDetails.Amount = amount + clsDetails.VAT;

                    System.Data.DataTable dtmatrix = clsProductVariationsMatrix.ForReorder(clsDetails.ProductID, clsTransferInDetails.SupplierID);
                    if (dtmatrix.Rows.Count > 0)
                        foreach (System.Data.DataRow drmatrix in dtmatrix.Rows)
                        {
                            amount = clsDetails.Quantity * clsDetails.UnitCost;

                            clsDetails.ProductUnitID = Convert.ToInt32(drmatrix["UnitID"]);
                            clsDetails.ProductUnitCode = drmatrix["UnitName"].ToString();
                            clsDetails.Quantity = Convert.ToDecimal(drmatrix["ReorderQty"]);
                            clsDetails.UnitCost = Convert.ToDecimal(drmatrix["Price"]);

                            if (Convert.ToDecimal(drmatrix["VAT"]) > 0)
                            {
                                clsDetails.VatableAmount = amount;
                                clsDetails.EVatableAmount = amount;
                                clsDetails.LocalTax = amount;

                                clsDetails.VAT = clsDetails.VatableAmount * (clsTerminalDetails.VAT / 100);
                                clsDetails.EVAT = clsDetails.EVatableAmount * (clsTerminalDetails.EVAT / 100);
                                clsDetails.LocalTax = clsDetails.LocalTax * (clsTerminalDetails.LocalTax / 100);
                                clsDetails.IsVatable = true;
                            }
                            else
                            {
                                clsDetails.VAT = 0;
                                clsDetails.VatableAmount = 0;
                                clsDetails.EVAT = 0;
                                clsDetails.EVatableAmount = 0;
                                clsDetails.LocalTax = 0;
                                clsDetails.IsVatable = false;
                            }
                            clsDetails.Amount = amount + clsDetails.VAT;

                            clsDetails.VariationMatrixID = Convert.ToInt64(drmatrix["MatrixID"]);
                            clsDetails.MatrixDescription = drmatrix["VariationDesc"].ToString();
                            clsTransferInItem.Insert(clsDetails);
                        }
                    else
                    {
                        clsDetails.VariationMatrixID = 0;
                        clsDetails.MatrixDescription = string.Empty;
                        clsTransferInItem.Insert(clsDetails);
                    }

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

        /**********************************
         * Lemuel E. Aceron
         * July 30, 2008 17:21
         * Added for Payment
         **********************************/
        public bool UpdatePaymentStatus(TransferInPaymentStatus paymentStatus, string IDs)
        {
            try
            {
                string SQL = "UPDATE tblTransferIn SET PaymentStatus = @PaymentStatus WHERE TransferInID IN (" + IDs + ");";

                MySqlConnection cn = GetConnection();

                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = cn;
                cmd.Transaction = mTransaction;
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandText = SQL;

                MySqlParameter prmPaymentStatus = new MySqlParameter("@PaymentStatus",MySqlDbType.Int16);
                prmPaymentStatus.Value = paymentStatus.ToString("d");
                cmd.Parameters.Add(prmPaymentStatus);

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
        public bool UpdatePayment(long TransferInID, decimal PaidAmount, TransferInPaymentStatus paymentStatus)
        {
            try
            {
                string SQL = "UPDATE tblTransferIn SET " +
                                "PaidAmount     = PaidAmount + @PaidAmount, " +
                                "UnpaidAmount   = UnpaidAmount - @PaidAmount, " +
                                "PaymentStatus  = @PaymentStatus " +
                             "WHERE TransferInID = @TransferInID;";

                MySqlConnection cn = GetConnection();

                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = cn;
                cmd.Transaction = mTransaction;
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandText = SQL;

                MySqlParameter prmPaidAmount = new MySqlParameter("@PaidAmount", System.Data.DbType.Decimal);
                prmPaidAmount.Value = PaidAmount;
                cmd.Parameters.Add(prmPaidAmount);

                MySqlParameter prmPaymentStatus = new MySqlParameter("@PaymentStatus",MySqlDbType.Int16);
                prmPaymentStatus.Value = paymentStatus.ToString("d");
                cmd.Parameters.Add(prmPaymentStatus);

                MySqlParameter prmTransferInID = new MySqlParameter("@TransferInID",MySqlDbType.Int64);
                prmTransferInID.Value = TransferInID;
                cmd.Parameters.Add(prmTransferInID);

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

        #region Delete

        public bool Delete(string IDs)
        {
            try
            {
                string SQL = "DELETE FROM tblTransferIn WHERE TransferInID IN (" + IDs + ");";

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
            string stSQL = "SELECT " +
                                "TransferInID, " +
                                "TransferInNo, " +
                                "TransferInDate, " +
                                "SupplierID, " +
                                "SupplierCode, " +
                                "SupplierContact, " +
                                "SupplierAddress, " +
                                "SupplierTelephoneNo, " +
                                "SupplierModeOfTerms, " +
                                "SupplierTerms, " +
                                "RequiredDeliveryDate, " +
                                "a.BranchID, " +
                                "BranchCode, " +
                                "BranchName, " +
                                "b.Address BranchAddress, " +
                                "TransferrerID, " +
                                "TransferrerName, " +
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
                                "Status, " +
                                "IsVatInclusive, " +
                                "a.Remarks, " +
                                "SupplierDRNo, " +
                                "DeliveryDate, " +
                                "CancelledDate, " +
                                "CancelledRemarks, " +
                                "CancelledByID, " +
                                "PaymentStatus, " +
                                "ChartOfAccountIDAPTracking, " +
                                "ChartOfAccountIDAPFreight, " +
                                "ChartOfAccountIDAPVDeposit, " +
                                "ChartOfAccountIDAPContra, " +
                                "ChartOfAccountIDAPLatePayment, " +
                                "TotalItemDiscount " +
                            "FROM tblTransferIn a INNER JOIN tblBranch b ON a.BranchID = b.BranchID ";
            return stSQL;
        }

        #region Details

        public TransferInDetails Details(long TransferInID)
        {
            try
            {
                string SQL = SQLSelect() + "WHERE TransferInID = @TransferInID;";

                MySqlConnection cn = GetConnection();

                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = cn;
                cmd.Transaction = mTransaction;
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandText = SQL;

                MySqlParameter prmTransferInID = new MySqlParameter("@TransferInID",MySqlDbType.Int16);
                prmTransferInID.Value = TransferInID;
                cmd.Parameters.Add(prmTransferInID);

                MySqlDataReader myReader = (MySqlDataReader)cmd.ExecuteReader(System.Data.CommandBehavior.SingleResult);

                TransferInDetails Details = new TransferInDetails();

                while (myReader.Read())
                {
                    Details.TransferInID = TransferInID;
                    Details.TransferInNo = "" + myReader["TransferInNo"].ToString();
                    Details.TransferInDate = myReader.GetDateTime("TransferInDate");
                    Details.SupplierID = myReader.GetInt64("SupplierID");
                    Details.SupplierCode = "" + myReader["SupplierCode"].ToString();
                    Details.SupplierContact = "" + myReader["SupplierContact"].ToString();
                    Details.SupplierAddress = "" + myReader["SupplierAddress"].ToString();
                    Details.SupplierTelephoneNo = "" + myReader["SupplierTelephoneNo"].ToString();
                    Details.SupplierModeOfTerms = myReader.GetInt16("SupplierModeofTerms");
                    Details.SupplierTerms = myReader.GetInt16("SupplierTerms");
                    Details.RequiredDeliveryDate = myReader.GetDateTime("RequiredDeliveryDate");
                    Details.BranchID = myReader.GetInt16("BranchID");
                    Details.BranchCode = "" + myReader["BranchCode"].ToString();
                    Details.BranchName = "" + myReader["BranchName"].ToString();
                    Details.BranchAddress = "" + myReader["BranchAddress"].ToString();
                    Details.TransferrerID = myReader.GetInt64("TransferrerID");
                    Details.TransferrerName = "" + myReader["TransferrerName"].ToString();
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
                    Details.Status = (TransferInStatus)Enum.Parse(typeof(TransferInStatus), myReader.GetString("Status"));
                    Details.IsVatInclusive = myReader.GetBoolean("IsVatInclusive");
                    Details.TotalItemDiscount = myReader.GetDecimal("TotalItemDiscount");
                    Details.Remarks = "" + myReader["Remarks"].ToString();
                    Details.SupplierDRNo = "" + myReader["SupplierDRNo"].ToString();
                    Details.DeliveryDate = myReader.GetDateTime("DeliveryDate");
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

        #region Streams

        public System.Data.DataTable ListAsDataTable(TransferInStatus transferinstatus, string SortField, SortOption SortOrder)
        {
            if (SortField == string.Empty || SortField == null) SortField = "TransferInID";

            string SQL = SQLSelect() + "WHERE Status = @Status ORDER BY " + SortField;

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

            MySqlParameter prmStatus = new MySqlParameter("@Status",MySqlDbType.Int16);
            prmStatus.Value = transferinstatus.ToString("d");
            cmd.Parameters.Add(prmStatus);

            System.Data.DataTable dt = new System.Data.DataTable("TransferIn");
            MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
            adapter.Fill(dt);

            return dt;
        }
        public System.Data.DataTable ListAsDataTable(TransferInStatus transferinstatus, DateTime OrderStartDate, DateTime OrderEndDate, DateTime PostingStartDate, DateTime PostingEndDate, string SortField, SortOption SortOrder)
        {
            if (SortField == string.Empty || SortField == null) SortField = "TransferInID";

            string SQL = SQLSelect() + "WHERE Status = @Status ";

            if (OrderStartDate != DateTime.MinValue) SQL += "AND TransferInDate >= @OrderStartDate ";
            if (OrderEndDate != DateTime.MinValue) SQL += "AND TransferInDate <= @OrderEndDate ";
            if (PostingStartDate != DateTime.MinValue) SQL += "AND TransferInDate >= @PostingStartDate ";
            if (PostingEndDate != DateTime.MinValue) SQL += "AND TransferInDate <= @PostingEndDate ";

            SQL += "ORDER BY " + SortField;

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

            cmd.Parameters.AddWithValue("@Status", transferinstatus.ToString("d"));

            if (OrderStartDate != DateTime.MinValue) cmd.Parameters.AddWithValue("@OrderStartDate", OrderStartDate.ToString("yyyy-MM-dd HH:mm:ss"));
            if (OrderEndDate != DateTime.MinValue) cmd.Parameters.AddWithValue("@OrderEndDate", OrderEndDate.ToString("yyyy-MM-dd HH:mm:ss"));
            if (PostingStartDate != DateTime.MinValue) cmd.Parameters.AddWithValue("@PostingStartDate", PostingStartDate.ToString("yyyy-MM-dd HH:mm:ss"));
            if (PostingEndDate != DateTime.MinValue) cmd.Parameters.AddWithValue("@PostingEndDate", PostingEndDate.ToString("yyyy-MM-dd HH:mm:ss"));

            System.Data.DataTable dt = new System.Data.DataTable("TransferIn");
            MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
            adapter.Fill(dt);

            return dt;
        }

        public System.Data.DataTable ListAsDataTable(string SortField, SortOption SortOrder)
        {
            if (SortField == string.Empty || SortField == null) SortField = "TransferInID";

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

            System.Data.DataTable dt = new System.Data.DataTable("TransferIn");
            MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
            adapter.Fill(dt);

            return dt;
        }

        public MySqlDataReader List(long TransferInID, string SortField, SortOption SortOrder)
        {
            try
            {
                if (SortField == string.Empty || SortField == null) SortField = "TransferInID";

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

        public MySqlDataReader List(string SortField, SortOption SortOrder)
        {
            try
            {
                if (SortField == string.Empty || SortField == null) SortField = "TransferInID";

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

        public MySqlDataReader List(TransferInStatus transferinstatus, string SortField, SortOption SortOrder)
        {
            try
            {
                if (SortField == string.Empty || SortField == null) SortField = "TransferInID";

                string SQL = SQLSelect() + "WHERE Status = @Status ORDER BY " + SortField;

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

                MySqlParameter prmStatus = new MySqlParameter("@Status",MySqlDbType.Int16);
                prmStatus.Value = transferinstatus.ToString("d");
                cmd.Parameters.Add(prmStatus);

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

        public MySqlDataReader List(TransferInStatus transferinstatus, long SupplierID, string SortField, SortOption SortOrder)
        {
            try
            {
                if (SortField == string.Empty || SortField == null) SortField = "TransferInID";

                string SQL = SQLSelect() + "WHERE Status =@Status AND SupplierID = @SupplierID ORDER BY " + SortField;

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

                MySqlParameter prmStatus = new MySqlParameter("@Status",MySqlDbType.Int16);
                prmStatus.Value = transferinstatus.ToString("d");
                cmd.Parameters.Add(prmStatus);

                MySqlParameter prmSupplierID = new MySqlParameter("@SupplierID",MySqlDbType.Int64);
                prmSupplierID.Value = SupplierID;
                cmd.Parameters.Add(prmSupplierID);

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
        public MySqlDataReader ListForPayment(long SupplierID, string SortField, SortOption SortOrder)
        {
            try
            {
                if (SortField == string.Empty || SortField == null) SortField = "TransferInID";

                string SQL = SQLSelect() + "WHERE PaymentStatus <> @FullyPaidPaymentStatus AND Status =@PostedStatus AND SupplierID = @SupplierID ORDER BY " + SortField;

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

                MySqlParameter prmFullyPaidPaymentStatus = new MySqlParameter("@FullyPaidPaymentStatus",MySqlDbType.Int16);
                prmFullyPaidPaymentStatus.Value = TransferInPaymentStatus.FullyPaid.ToString("d");
                cmd.Parameters.Add(prmFullyPaidPaymentStatus);

                MySqlParameter prmPostedStatus = new MySqlParameter("@PostedStatus",MySqlDbType.Int16);
                prmPostedStatus.Value = TransferInStatus.Posted.ToString("d");
                cmd.Parameters.Add(prmPostedStatus);

                MySqlParameter prmSupplierID = new MySqlParameter("@SupplierID",MySqlDbType.Int64);
                prmSupplierID.Value = SupplierID;
                cmd.Parameters.Add(prmSupplierID);

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
                if (SortField == string.Empty || SortField == null) SortField = "TransferInID";

                string SQL = SQLSelect() + "WHERE (TransferInNo LIKE @SearchKey or TransferInDate LIKE @SearchKey or SupplierCode LIKE @SearchKey " +
                                        "or SupplierContact LIKE @SearchKey or BranchCode LIKE @SearchKey or RequiredDeliveryDate LIKE @SearchKey) " +
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
        public MySqlDataReader Search(TransferInStatus transferinstatus, string SearchKey, string SortField, SortOption SortOrder)
        {
            try
            {
                if (SortField == string.Empty || SortField == null) SortField = "TransferInID";

                string SQL = SQLSelect() + "WHERE Status = @Status AND (TransferInNo LIKE @SearchKey or TransferInDate LIKE @SearchKey or SupplierCode LIKE @SearchKey " +
                                        "or SupplierContact LIKE @SearchKey or BranchCode LIKE @SearchKey or RequiredDeliveryDate LIKE @SearchKey) " +
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

                MySqlParameter prmStatus = new MySqlParameter("@Status",MySqlDbType.Int16);
                prmStatus.Value = transferinstatus.ToString("d");
                cmd.Parameters.Add(prmStatus);

                MySqlParameter prmSearchKey = new MySqlParameter("@SearchKey",MySqlDbType.String);
                prmSearchKey.Value = "%" + SearchKey + "%";
                cmd.Parameters.Add(prmSearchKey);

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
        public System.Data.DataTable SearchAsDataTable(TransferInStatus transferinstatus, string SearchKey, string SortField, SortOption SortOrder)
        {
            try
            {
                if (SortField == string.Empty || SortField == null) SortField = "TransferInID";

                string SQL = SQLSelect() + "WHERE Status = @Status AND (TransferInNo LIKE @SearchKey or TransferInDate LIKE @SearchKey or SupplierCode LIKE @SearchKey " +
                                        "or SupplierContact LIKE @SearchKey or BranchCode LIKE @SearchKey or RequiredDeliveryDate LIKE @SearchKey) " +
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

                MySqlParameter prmStatus = new MySqlParameter("@Status",MySqlDbType.Int16);
                prmStatus.Value = transferinstatus.ToString("d");
                cmd.Parameters.Add(prmStatus);

                MySqlParameter prmSearchKey = new MySqlParameter("@SearchKey",MySqlDbType.String);
                prmSearchKey.Value = "%" + SearchKey + "%";
                cmd.Parameters.Add(prmSearchKey);

                System.Data.DataTable dt = new System.Data.DataTable("TransferIn");
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
        public System.Data.DataTable SearchAsDataTable(TransferInStatus transferinstatus, DateTime OrderStartDate, DateTime OrderEndDate, DateTime PostingStartDate, DateTime PostingEndDate, string SearchKey, string SortField, SortOption SortOrder)
        {
            try
            {
                if (SortField == string.Empty || SortField == null) SortField = "TransferInID";

                string SQL = SQLSelect() + "WHERE Status = @Status AND (TransferInNo LIKE @SearchKey or TransferInDate LIKE @SearchKey or SupplierCode LIKE @SearchKey " +
                                        "or SupplierContact LIKE @SearchKey or BranchCode LIKE @SearchKey or RequiredDeliveryDate LIKE @SearchKey) ";
                
                if (OrderStartDate != DateTime.MinValue) SQL += "AND TransferInDate >= @OrderStartDate ";
                if (OrderEndDate != DateTime.MinValue) SQL += "AND TransferInDate <= @OrderEndDate ";
                if (PostingStartDate != DateTime.MinValue) SQL += "AND TransferInDate >= @PostingStartDate ";
                if (PostingEndDate != DateTime.MinValue) SQL += "AND TransferInDate <= @PostingEndDate ";

                SQL += "ORDER BY " + SortField;

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

                cmd.Parameters.AddWithValue("@Status", transferinstatus.ToString("d"));
                cmd.Parameters.AddWithValue("@SearchKey", "%" + SearchKey + "%");

                if (OrderStartDate != DateTime.MinValue) cmd.Parameters.AddWithValue("@OrderStartDate", OrderStartDate.ToString("yyyy-MM-dd HH:mm:ss"));
                if (OrderEndDate != DateTime.MinValue) cmd.Parameters.AddWithValue("@OrderEndDate", OrderEndDate.ToString("yyyy-MM-dd HH:mm:ss"));
                if (PostingStartDate != DateTime.MinValue) cmd.Parameters.AddWithValue("@PostingStartDate", PostingStartDate.ToString("yyyy-MM-dd HH:mm:ss"));
                if (PostingEndDate != DateTime.MinValue) cmd.Parameters.AddWithValue("@PostingEndDate", PostingEndDate.ToString("yyyy-MM-dd HH:mm:ss"));

                System.Data.DataTable dt = new System.Data.DataTable("TransferIn");
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

        public MySqlDataReader List(TransferInStatus transferinstatus, DateTime StartDate, DateTime EndDate)
        {
            try
            {
                string SQL = SQLSelect() + "WHERE Status = @Status AND DeliveryDate BETWEEN @StartDate AND @EndDate ORDER BY TransferInID ASC";

                MySqlConnection cn = GetConnection();

                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = cn;
                cmd.Transaction = mTransaction;
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandText = SQL;

                MySqlParameter prmStartDate = new MySqlParameter("@StartDate",MySqlDbType.DateTime);
                prmStartDate.Value = StartDate.ToString("yyyy-MM-dd HH:mm:ss");
                cmd.Parameters.Add(prmStartDate);

                MySqlParameter prmEndDate = new MySqlParameter("@EndDate",MySqlDbType.DateTime);
                prmEndDate.Value = EndDate.ToString("yyyy-MM-dd HH:mm:ss");
                cmd.Parameters.Add(prmEndDate);

                MySqlParameter prmStatus = new MySqlParameter("@Status",MySqlDbType.Int16);
                prmStatus.Value = transferinstatus.ToString("d");
                cmd.Parameters.Add(prmStatus);

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

        #endregion

        #region Public Modifiers

        public string LastTransactionNo()
        {
            try
            {
                string stRetValue = String.Empty;

                ERPConfig clsERPConfig = new ERPConfig(Connection, Transaction);
                stRetValue = clsERPConfig.get_LastTransferInNo();

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
        public void SynchronizeAmount(long TransferInID)
        {
            try
            {
                string SQL = "CALL procTransferInSynchronizeAmount(@TransferInID);";

                MySqlConnection cn = GetConnection();

                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = cn;
                cmd.Transaction = mTransaction;
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandText = SQL;

                MySqlParameter prmTransferInID = new MySqlParameter("@TransferInID",MySqlDbType.Int64);
                prmTransferInID.Value = TransferInID;
                cmd.Parameters.Add(prmTransferInID);

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
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

    #region CreditMemoDetails

    public struct CreditMemoDetails
    {
        public long CreditMemoID;
        public string CNNo;
        public DateTime CNDate;
        public long CustomerID;
        public string CustomerCode;
        public string CustomerContact;
        public string CustomerAddress;
        public string CustomerTelephoneNo;
        public int CustomerModeOfTerms;
        public int CustomerTerms;
        public DateTime RequiredPostingDate;
        public int BranchID;
        public string BranchCode;
        public string BranchName;
        public string BranchAddress;
        public long SellerID;
        public string SellerName;
        public decimal SubTotal;
        public decimal Discount;
        public decimal DiscountApplied;
        public DiscountTypes DiscountType;
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
        public CreditMemoStatus CreditMemoStatus;
        public string Remarks;
        public string CustomerDocNo;
        public DateTime PostingDate;
        public DateTime CancelledDate;
        public string CancelledRemarks;
        public long CancelledByID;
        public int ChartOfAccountIDARTracking;
        public int ChartOfAccountIDARFreight;
        public int ChartOfAccountIDARVDeposit;
        public int ChartOfAccountIDARContra;
        public int ChartOfAccountIDARLatePayment;
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
    public class CreditMemos
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

        public CreditMemos()
        {

        }

        public CreditMemos(MySqlConnection Connection, MySqlTransaction Transaction)
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

        #region Insert and Update: Insert, Update, Post

        public long Insert(CreditMemoDetails Details)
        {
            try
            {
                ERPConfig clsERPConfig = new ERPConfig(mConnection, mTransaction);
                ARLinkConfigDetails clsARLinkConfigDetails = clsERPConfig.ARLinkDetails();

                string SQL = "INSERT INTO tblSOCreditMemo (" +
                                "CNNo, " +
                                "CNDate, " +
                                "CustomerID, " +
                                "CustomerCode, " +
                                "CustomerContact, " +
                                "CustomerAddress, " +
                                "CustomerTelephoneNo, " +
                                "CustomerModeOfTerms, " +
                                "CustomerTerms, " +
                                "RequiredPostingDate, " +
                                "BranchID, " +
                                "SellerID, " +
                                "SellerName, " +
                                "SOReturnStatus, " +
                                "CreditMemoStatus, " +
                                "Remarks, " +
                                "ChartOfAccountIDARTracking, " +
                                "ChartOfAccountIDARBills, " +
                                "ChartOfAccountIDARFreight, " +
                                "ChartOfAccountIDARVDeposit, " +
                                "ChartOfAccountIDARContra, " +
                                "ChartOfAccountIDARLatePayment" +
                            ") VALUES (" +
                                "@CNNo, " +
                                "@CNDate, " +
                                "@CustomerID, " +
                                "@CustomerCode, " +
                                "@CustomerContact, " +
                                "@CustomerAddress, " +
                                "@CustomerTelephoneNo, " +
                                "@CustomerModeOfTerms, " +
                                "@CustomerTerms, " +
                                "@RequiredPostingDate, " +
                                "@BranchID, " +
                                "@SellerID, " +
                                "@SellerName, " +
                                "@SOReturnStatus, " +
                                "@CreditMemoStatus, " +
                                "@Remarks, " +
                                "@ChartOfAccountIDARTracking, " +
                                "@ChartOfAccountIDARBills, " +
                                "@ChartOfAccountIDARFreight, " +
                                "@ChartOfAccountIDARVDeposit, " +
                                "@ChartOfAccountIDARContra, " +
                                "@ChartOfAccountIDARLatePayment" +
                            ");";

                MySqlConnection cn = GetConnection();

                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = cn;
                cmd.Transaction = mTransaction;
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandText = SQL;

                cmd.Parameters.AddWithValue("@CNNo", Details.CNNo);
                cmd.Parameters.AddWithValue("@CNDate", Details.CNDate.ToString("yyyy-MM-dd HH:mm:ss"));
                cmd.Parameters.AddWithValue("@CustomerID", Details.CustomerID);
                cmd.Parameters.AddWithValue("@CustomerCode", Details.CustomerCode);
                cmd.Parameters.AddWithValue("@CustomerContact", Details.CustomerContact);
                cmd.Parameters.AddWithValue("@CustomerAddress", Details.CustomerAddress);
                cmd.Parameters.AddWithValue("@CustomerTelephoneNo", Details.CustomerTelephoneNo);
                cmd.Parameters.AddWithValue("@CustomerModeOfTerms", Details.CustomerModeOfTerms);
                cmd.Parameters.AddWithValue("@CustomerTerms", Details.CustomerTerms);
                cmd.Parameters.AddWithValue("@RequiredPostingDate", Details.RequiredPostingDate.ToString("yyyy-MM-dd HH:mm:ss"));
                cmd.Parameters.AddWithValue("@BranchID", Details.BranchID);
                cmd.Parameters.AddWithValue("@SellerID", Details.SellerID);
                cmd.Parameters.AddWithValue("@SellerName", Details.SellerName);
                cmd.Parameters.AddWithValue("@SOReturnStatus", SOReturnStatus.Posted.ToString("d"));
                cmd.Parameters.AddWithValue("@CreditMemoStatus", Details.CreditMemoStatus.ToString("d"));
                cmd.Parameters.AddWithValue("@Remarks", Details.Remarks);
                cmd.Parameters.AddWithValue("@ChartOfAccountIDARTracking", clsARLinkConfigDetails.ChartOfAccountIDARTracking);
                cmd.Parameters.AddWithValue("@ChartOfAccountIDARBills", clsARLinkConfigDetails.ChartOfAccountIDARBills);
                cmd.Parameters.AddWithValue("@ChartOfAccountIDARFreight", clsARLinkConfigDetails.ChartOfAccountIDARFreight);
                cmd.Parameters.AddWithValue("@ChartOfAccountIDARVDeposit", clsARLinkConfigDetails.ChartOfAccountIDARVDeposit);
                cmd.Parameters.AddWithValue("@ChartOfAccountIDARContra", clsARLinkConfigDetails.ChartOfAccountIDARContra);
                cmd.Parameters.AddWithValue("@ChartOfAccountIDARLatePayment", clsARLinkConfigDetails.ChartOfAccountIDARLatePayment);

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

        public void Update(CreditMemoDetails Details)
        {
            try
            {
                ERPConfig clsERPConfig = new ERPConfig(mConnection, mTransaction);
                ARLinkConfigDetails clsARLinkConfigDetails = clsERPConfig.ARLinkDetails();

                string SQL = "UPDATE tblSOCreditMemo SET " +
                                "CNNo				=	@CNNo, " +
                                "CNDate				=	@CNDate, " +
                                "CustomerID				=	@CustomerID, " +
                                "CustomerCode			=	@CustomerCode, " +
                                "CustomerContact		=	@CustomerContact, " +
                                "CustomerAddress		=	@CustomerAddress, " +
                                "CustomerTelephoneNo	=	@CustomerTelephoneNo, " +
                                "CustomerModeOfTerms	=	@CustomerModeOfTerms, " +
                                "CustomerTerms			=	@CustomerTerms, " +
                                "RequiredPostingDate	=	@RequiredPostingDate, " +
                                "BranchID				=	@BranchID, " +
                                "SellerID			=	@SellerID, " +
                                "SellerName          =   @SellerName, " +
                                "Remarks                =   @Remarks, " +
                                "ChartOfAccountIDARTracking     = @ChartOfAccountIDARTracking, " +
                                "ChartOfAccountIDARBills        = @ChartOfAccountIDARBills, " +
                                "ChartOfAccountIDARFreight      = @ChartOfAccountIDARFreight, " +
                                "ChartOfAccountIDARVDeposit     = @ChartOfAccountIDARVDeposit, " +
                                "ChartOfAccountIDARContra       = @ChartOfAccountIDARContra, " +
                                "ChartOfAccountIDARLatePayment  = @ChartOfAccountIDARLatePayment " +
                            "WHERE CreditMemoID = @CreditMemoID;";

                MySqlConnection cn = GetConnection();

                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = cn;
                cmd.Transaction = mTransaction;
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandText = SQL;

                cmd.Parameters.AddWithValue("@CNNo", Details.CNNo);
                cmd.Parameters.AddWithValue("@CNDate", Details.CNDate.ToString("yyyy-MM-dd HH:mm:ss"));
                cmd.Parameters.AddWithValue("@CustomerID", Details.CustomerID);
                cmd.Parameters.AddWithValue("@CustomerCode", Details.CustomerCode);
                cmd.Parameters.AddWithValue("@CustomerContact", Details.CustomerContact);
                cmd.Parameters.AddWithValue("@CustomerAddress", Details.CustomerAddress);
                cmd.Parameters.AddWithValue("@CustomerTelephoneNo", Details.CustomerTelephoneNo);
                cmd.Parameters.AddWithValue("@CustomerModeOfTerms", Details.CustomerModeOfTerms);
                cmd.Parameters.AddWithValue("@CustomerTerms", Details.CustomerTerms);
                cmd.Parameters.AddWithValue("@RequiredPostingDate", Details.RequiredPostingDate.ToString("yyyy-MM-dd HH:mm:ss"));
                cmd.Parameters.AddWithValue("@BranchID", Details.BranchID);
                cmd.Parameters.AddWithValue("@SellerID", Details.SellerID);
                cmd.Parameters.AddWithValue("@SellerName", Details.SellerName);
                cmd.Parameters.AddWithValue("@SOReturnStatus", SOReturnStatus.Posted.ToString("d"));
                cmd.Parameters.AddWithValue("@CreditMemoStatus", Details.CreditMemoStatus.ToString("d"));
                cmd.Parameters.AddWithValue("@Remarks", Details.Remarks);
                cmd.Parameters.AddWithValue("@ChartOfAccountIDARTracking", clsARLinkConfigDetails.ChartOfAccountIDARTracking);
                cmd.Parameters.AddWithValue("@ChartOfAccountIDARBills", clsARLinkConfigDetails.ChartOfAccountIDARBills);
                cmd.Parameters.AddWithValue("@ChartOfAccountIDARFreight", clsARLinkConfigDetails.ChartOfAccountIDARFreight);
                cmd.Parameters.AddWithValue("@ChartOfAccountIDARVDeposit", clsARLinkConfigDetails.ChartOfAccountIDARVDeposit);
                cmd.Parameters.AddWithValue("@ChartOfAccountIDARContra", clsARLinkConfigDetails.ChartOfAccountIDARContra);
                cmd.Parameters.AddWithValue("@ChartOfAccountIDARLatePayment", clsARLinkConfigDetails.ChartOfAccountIDARLatePayment);
                cmd.Parameters.AddWithValue("@CreditMemoID", Details.CreditMemoID);

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

        public void UpdateDiscount(long CreditMemoID, decimal DiscountApplied, DiscountTypes DiscountType)
        {
            try
            {
                string SQL = "UPDATE tblSOCreditMemo SET " +
                                "DiscountApplied        =   @DiscountApplied, " +
                                "DiscountType           =   @DiscountType " +
                            "WHERE CreditMemoID = @CreditMemoID;";

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

                MySqlParameter prmCreditMemoID = new MySqlParameter("@CreditMemoID",MySqlDbType.Int64);
                prmCreditMemoID.Value = CreditMemoID;
                cmd.Parameters.Add(prmCreditMemoID);

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
        public void UpdateDiscountFreightDeposit(long CreditMemoID, decimal DiscountApplied, DiscountTypes DiscountType)
        {
            try
            {
                string SQL = "UPDATE tblSOCreditMemo SET " +
                                "DiscountApplied        =   @DiscountApplied, " +
                                "DiscountType           =   @DiscountType " +
                            "WHERE CreditMemoID = @CreditMemoID;";

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

                MySqlParameter prmCreditMemoID = new MySqlParameter("@CreditMemoID",MySqlDbType.Int64);
                prmCreditMemoID.Value = CreditMemoID;
                cmd.Parameters.Add(prmCreditMemoID);

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
        public void UpdateFreight(long CreditMemoID, decimal Freight)
        {
            try
            {
                string SQL = "UPDATE tblSOCreditMemo SET " +
                                "Freight           =   @Freight " +
                            "WHERE CreditMemoID = @CreditMemoID;";

                MySqlConnection cn = GetConnection();

                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = cn;
                cmd.Transaction = mTransaction;
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandText = SQL;

                MySqlParameter prmFreight = new MySqlParameter("@Freight", System.Data.DbType.Decimal);
                prmFreight.Value = Freight;
                cmd.Parameters.Add(prmFreight);

                MySqlParameter prmCreditMemoID = new MySqlParameter("@CreditMemoID",MySqlDbType.Int64);
                prmCreditMemoID.Value = CreditMemoID;
                cmd.Parameters.Add(prmCreditMemoID);

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
        public void UpdateDeposit(long CreditMemoID, decimal Deposit)
        {
            try
            {
                string SQL = "UPDATE tblSOCreditMemo SET " +
                                "Deposit           =   @Deposit " +
                            "WHERE CreditMemoID = @CreditMemoID;";

                MySqlConnection cn = GetConnection();

                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = cn;
                cmd.Transaction = mTransaction;
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandText = SQL;

                MySqlParameter prmDeposit = new MySqlParameter("@Deposit", System.Data.DbType.Decimal);
                prmDeposit.Value = Deposit;
                cmd.Parameters.Add(prmDeposit);

                MySqlParameter prmCreditMemoID = new MySqlParameter("@CreditMemoID",MySqlDbType.Int64);
                prmCreditMemoID.Value = CreditMemoID;
                cmd.Parameters.Add(prmCreditMemoID);

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

        public void Post(long CreditMemoID, string CustomerDocNo, DateTime PostingDate)
        {
            try
            {
                string SQL = "UPDATE tblSOCreditMemo SET " +
                                "CustomerDocNo		=	@CustomerDocNo, " +
                                "PostingDate		=	@PostingDate, " +
                                "SOReturnStatus		=	@SOReturnStatus, " +
                                "CreditMemoStatus	=	@CreditMemoStatus " +
                            "WHERE CreditMemoID = @CreditMemoID;";

                MySqlConnection cn = GetConnection();

                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = cn;
                cmd.Transaction = mTransaction;
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandText = SQL;

                cmd.Parameters.AddWithValue("@CustomerDocNo", CustomerDocNo);
                cmd.Parameters.AddWithValue("@PostingDate", PostingDate.ToString("yyyy-MM-dd HH:mm:ss"));
                cmd.Parameters.AddWithValue("@SOReturnStatus", SOReturnStatus.Posted.ToString("d"));
                cmd.Parameters.AddWithValue("@CreditMemoStatus", CreditMemoStatus.Posted.ToString("d"));
                cmd.Parameters.AddWithValue("@CreditMemoID", CreditMemoID);

                cmd.ExecuteNonQuery();

                /*******************************************
                 * Update the status of items
                 * ****************************************/
                CreditMemoItems clsCreditMemoItems = new CreditMemoItems(mConnection, mTransaction);
                clsCreditMemoItems.Post(CreditMemoID);

                /*******************************************
                 * Update Customer Account
                 * ****************************************/
                AddItemToInventory(CreditMemoID);
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

        private void UpdateAccounts(long CreditMemoID)
        {
            try
            {
                CreditMemoDetails clsCreditMemoDetails = Details(CreditMemoID);
                ChartOfAccount clsChartOfAccount = new ChartOfAccount(mConnection, mTransaction);

                // update ChartOfAccountIDARTracking as credit
                clsChartOfAccount.UpdateCredit(clsCreditMemoDetails.ChartOfAccountIDARTracking, clsCreditMemoDetails.SubTotal);

                // update Deposit & APContra
                clsChartOfAccount.UpdateDebit(clsCreditMemoDetails.ChartOfAccountIDARContra, clsCreditMemoDetails.Discount);

                // update Freight & APTracking
                clsChartOfAccount.UpdateDebit(clsCreditMemoDetails.ChartOfAccountIDARTracking, clsCreditMemoDetails.Freight);
                clsChartOfAccount.UpdateCredit(clsCreditMemoDetails.ChartOfAccountIDARFreight, clsCreditMemoDetails.Freight);

                // update Deposit & APTracking
                clsChartOfAccount.UpdateDebit(clsCreditMemoDetails.ChartOfAccountIDARTracking, clsCreditMemoDetails.Deposit);
                clsChartOfAccount.UpdateCredit(clsCreditMemoDetails.ChartOfAccountIDARVDeposit, clsCreditMemoDetails.Deposit);

                CreditMemoItems clsCreditMemoItems = new CreditMemoItems(mConnection, mTransaction);
                MySqlDataReader myReader = clsCreditMemoItems.List(CreditMemoID, string.Empty, SortOption.Ascending);
                while (myReader.Read())
                {
                    int iChartOfAccountIDPurchase = myReader.GetInt16("ChartOfAccountIDPurchase");
                    int iChartOfAccountIDTaxPurchase = myReader.GetInt16("ChartOfAccountIDTaxPurchase");

                    decimal decVAT = myReader.GetDecimal("VAT");
                    decimal decVATABLEAmount = myReader.GetDecimal("Amount") - decVAT;

                    // update purchase as debit
                    clsChartOfAccount.UpdateCredit(iChartOfAccountIDPurchase, decVATABLEAmount);
                    // update tax as debit
                    clsChartOfAccount.UpdateCredit(iChartOfAccountIDTaxPurchase, decVAT);

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

        private void AddItemToInventory(long CreditMemoID)
        {

            CreditMemoDetails clsCreditMemoDetails = Details(CreditMemoID);
            ERPConfig clsERPConfig = new ERPConfig(Connection, Transaction);
            ERPConfigDetails clsERPConfigDetails = clsERPConfig.Details();

            CreditMemoItems clsCreditMemoItems = new CreditMemoItems(Connection, Transaction);
            ProductUnit clsProductUnit = new ProductUnit(Connection, Transaction);
            Products clsProduct = new Products(Connection, Transaction);
            ProductVariationsMatrix clsProductVariationsMatrix = new ProductVariationsMatrix(Connection, Transaction);

            Inventory clsInventory = new Inventory(Connection, Transaction);

            MySqlDataReader myReader = clsCreditMemoItems.List(CreditMemoID, "CreditMemoItemID", SortOption.Ascending);

            while (myReader.Read())
            {
                long ProductID = myReader.GetInt64("ProductID");
                int ProductUnitID = myReader.GetInt16("ProductUnitID");

                decimal ItemQuantity = myReader.GetDecimal("Quantity");
                decimal Quantity = clsProductUnit.GetBaseUnitValue(ProductID, ProductUnitID, ItemQuantity);

                long VariationMatrixID = myReader.GetInt64("VariationMatrixID");
                string MatrixDescription = "" + myReader["MatrixDescription"].ToString();
                string ProductCode = "" + myReader["ProductCode"].ToString();
                decimal ItemCost = myReader.GetDecimal("Amount");
                decimal VAT = myReader.GetDecimal("VAT");

                /*******************************************
                 * Do not Add to Inventory coz this is a Debit Memo and no effect on inventory
                 * ****************************************/

                /*******************************************
                 * Add to Inventory Analysis
                 * ****************************************/
                InventoryDetails clsInventoryDetails = new InventoryDetails();
                clsInventoryDetails.PostingDateFrom = clsERPConfigDetails.PostingDateFrom;
                clsInventoryDetails.PostingDateTo = clsERPConfigDetails.PostingDateTo;
                clsInventoryDetails.PostingDate = clsCreditMemoDetails.PostingDate;
                clsInventoryDetails.ReferenceNo = clsCreditMemoDetails.CNNo;
                clsInventoryDetails.ContactID = clsCreditMemoDetails.CustomerID;
                clsInventoryDetails.ContactCode = clsCreditMemoDetails.CustomerCode;
                clsInventoryDetails.ProductID = ProductID;
                clsInventoryDetails.ProductCode = ProductCode;
                clsInventoryDetails.VariationMatrixID = VariationMatrixID;
                clsInventoryDetails.MatrixDescription = MatrixDescription;
                clsInventoryDetails.SCreditQuantity = Quantity;
                clsInventoryDetails.SCreditCost = ItemCost - VAT;
                clsInventoryDetails.SCreditVAT = ItemCost;	//Sales Return with VAT

                clsInventory.Insert(clsInventoryDetails);

            }
            myReader.Close();

        }

        public void Cancel(long CreditMemoID, DateTime CancelledDate, string Remarks, long CancelledByID)
        {
            try
            {
                string SQL = "UPDATE tblSOCreditMemo SET " +
                                "CancelledDate			=	@CancelledDate, " +
                                "CancelledRemarks		=	@CancelledRemarks, " +
                                "CancelledByID			=	@CancelledByID, " +
                                "CreditMemoStatus		=	@CreditMemoStatus " +
                            "WHERE CreditMemoID = @CreditMemoID;";

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

                MySqlParameter prmCreditMemoStatus = new MySqlParameter("@CreditMemoStatus",MySqlDbType.Int16);
                prmCreditMemoStatus.Value = CreditMemoStatus.Cancelled.ToString("d");
                cmd.Parameters.Add(prmCreditMemoStatus);

                MySqlParameter prmCreditMemoID = new MySqlParameter("@CreditMemoID",MySqlDbType.Int64);
                prmCreditMemoID.Value = CreditMemoID;
                cmd.Parameters.Add(prmCreditMemoID);

                cmd.ExecuteNonQuery();

                /*******************************************
                 * Update the status of items
                 * ****************************************/
                CreditMemoItems clsCreditMemoItems = new CreditMemoItems(mConnection, mTransaction);
                clsCreditMemoItems.Cancel(CreditMemoID);

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
                string SQL = "DELETE FROM tblSOCreditMemo WHERE CreditMemoID IN (" + IDs + ");";

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
                            "CreditMemoID, " +
                            "CNNo, " +
                            "CNDate, " +
                            "CustomerID, " +
                            "CustomerCode, " +
                            "CustomerContact, " +
                            "CustomerAddress, " +
                            "CustomerTelephoneNo, " +
                            "CustomerModeOfTerms, " +
                            "CustomerTerms, " +
                            "RequiredPostingDate, " +
                            "a.BranchID, " +
                            "BranchCode, " +
                            "BranchName, " +
                            "b.Address BranchAddress, " +
                            "SellerID, " +
                            "SellerName, " +
                            "SubTotal, " +
                            "Discount, " +
                            "DiscountApplied, " +
                            "DiscountType, " +
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
                            "CreditMemoStatus, " +
                            "a.Remarks, " +
                            "CustomerDocNo, " +
                            "PostingDate, " +
                            "CancelledDate, " +
                            "CancelledRemarks, " +
                            "CancelledByID, " +
                            "PaymentStatus, " +
                            "ChartOfAccountIDARTracking, " +
                            "ChartOfAccountIDARFreight, " +
                            "ChartOfAccountIDARVDeposit, " +
                            "ChartOfAccountIDARContra, " +
                            "ChartOfAccountIDARLatePayment " +
                        "FROM tblSOCreditMemo a INNER JOIN tblBranch b ON a.BranchID = b.BranchID " +
                        "WHERE 1=1 AND SOReturnStatus = " + SOReturnStatus.Posted.ToString("d") + " ";

            return SQL;

        }

        #region Details

        public CreditMemoDetails Details(long CreditMemoID)
        {
            try
            {
                string SQL = SQLSelect() + "AND CreditMemoID = @CreditMemoID;";

                MySqlConnection cn = GetConnection();

                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = cn;
                cmd.Transaction = mTransaction;
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandText = SQL;

                cmd.Parameters.AddWithValue("@CreditMemoID", CreditMemoID);

                MySqlDataReader myReader = (MySqlDataReader)cmd.ExecuteReader(System.Data.CommandBehavior.SingleResult);

                CreditMemoDetails Details = new CreditMemoDetails();

                while (myReader.Read())
                {
                    Details.CreditMemoID = CreditMemoID;
                    Details.CNNo = "" + myReader["CNNo"].ToString();
                    Details.CNDate = myReader.GetDateTime("CNDate");
                    Details.CustomerID = myReader.GetInt64("CustomerID");
                    Details.CustomerCode = "" + myReader["CustomerCode"].ToString();
                    Details.CustomerContact = "" + myReader["CustomerContact"].ToString();
                    Details.CustomerAddress = "" + myReader["CustomerAddress"].ToString();
                    Details.CustomerTelephoneNo = "" + myReader["CustomerTelephoneNo"].ToString();
                    Details.CustomerModeOfTerms = myReader.GetInt16("CustomerModeofTerms");
                    Details.CustomerTerms = myReader.GetInt16("CustomerTerms");
                    Details.RequiredPostingDate = myReader.GetDateTime("RequiredPostingDate");
                    Details.BranchID = myReader.GetInt16("BranchID");
                    Details.BranchCode = "" + myReader["BranchCode"].ToString();
                    Details.BranchName = "" + myReader["BranchName"].ToString();
                    Details.BranchAddress = "" + myReader["BranchAddress"].ToString();
                    Details.SellerID = myReader.GetInt64("SellerID");
                    Details.SellerName = "" + myReader["SellerName"].ToString();
                    Details.SubTotal = myReader.GetDecimal("SubTotal");
                    Details.Discount = myReader.GetDecimal("Discount");
                    Details.DiscountApplied = myReader.GetDecimal("DiscountApplied");
                    Details.DiscountType = (DiscountTypes)Enum.Parse(typeof(DiscountTypes), myReader.GetString("DiscountType"));
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
                    Details.CreditMemoStatus = (CreditMemoStatus)Enum.Parse(typeof(CreditMemoStatus), myReader.GetString("CreditMemoStatus"));
                    Details.Remarks = "" + myReader["Remarks"].ToString();
                    Details.CustomerDocNo = "" + myReader["CustomerDocNo"].ToString();
                    Details.PostingDate = myReader.GetDateTime("PostingDate");
                    Details.ChartOfAccountIDARTracking = myReader.GetInt16("ChartOfAccountIDARTracking");
                    Details.ChartOfAccountIDARFreight = myReader.GetInt16("ChartOfAccountIDARFreight");
                    Details.ChartOfAccountIDARVDeposit = myReader.GetInt16("ChartOfAccountIDARVDeposit");
                    Details.ChartOfAccountIDARContra = myReader.GetInt16("ChartOfAccountIDARContra");
                    Details.ChartOfAccountIDARLatePayment = myReader.GetInt16("ChartOfAccountIDARLatePayment");
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
            if (SortField == string.Empty || SortField == null) SortField = "CreditMemoID";

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

            System.Data.DataTable dt = new System.Data.DataTable("SOCreditMemo");
            MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
            adapter.Fill(dt);

            return dt;
        }
        public System.Data.DataTable ListAsDataTable(CreditMemoStatus CreditMemoStatus, string SortField, SortOption SortOrder)
        {
            if (SortField == string.Empty || SortField == null) SortField = "CreditMemoID";

            string SQL = SQLSelect() + "AND CreditMemoStatus = @CreditMemoStatus " +
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

            cmd.Parameters.AddWithValue("@CreditMemoStatus", CreditMemoStatus.ToString("d"));

            System.Data.DataTable dt = new System.Data.DataTable("SOCreditMemo");
            MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
            adapter.Fill(dt);

            return dt;
        }
        public MySqlDataReader List(long CreditMemoID, string SortField, SortOption SortOrder)
        {
            try
            {
                string SQL = SQLSelect() + "AND CreditMemoID = @CreditMemoID ORDER BY " + SortField;

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

                cmd.Parameters.AddWithValue("@CreditMemoID", CreditMemoID);

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
        public MySqlDataReader List(CreditMemoStatus CreditMemoStatus, string SortField, SortOption SortOrder)
        {
            try
            {
                string SQL = SQLSelect() + "AND CreditMemoStatus = @CreditMemoStatus " +
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

                cmd.Parameters.AddWithValue("@CreditMemoStatus", CreditMemoStatus.ToString("d"));

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
        public MySqlDataReader List(CreditMemoStatus CreditMemoStatus, long CustomerID, string SortField, SortOption SortOrder)
        {
            try
            {
                string SQL = SQLSelect() + "AND CreditMemoStatus = @CreditMemoStatus " +
                                "AND CustomerID = @CustomerID " +
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

                cmd.Parameters.AddWithValue("@CreditMemoStatus", CreditMemoStatus.ToString("d"));
                cmd.Parameters.AddWithValue("@CustomerID", CustomerID);

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
        public MySqlDataReader List(CreditMemoStatus CreditMemoStatus, DateTime StartDate, DateTime EndDate)
        {
            try
            {
                string SQL = SQLSelect() + "AND CreditMemoStatus = @CreditMemoStatus " +
                        "AND PostingDate BETWEEN @StartDate AND @EndDate " +
                    "ORDER BY CreditMemoID ASC";

                MySqlConnection cn = GetConnection();

                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = cn;
                cmd.Transaction = mTransaction;
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandText = SQL;

                cmd.Parameters.AddWithValue("@CreditMemoStatus", CreditMemoStatus.ToString("d"));
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
                string SQL = SQLSelect() + "AND (CNNo LIKE @SearchKey or CNDate LIKE @SearchKey or CustomerCode LIKE @SearchKey " +
                                        "or CustomerContact LIKE @SearchKey or BranchCode LIKE @SearchKey or RequiredPostingDate LIKE @SearchKey) " +
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

                cmd.Parameters.AddWithValue("@SearchKey", "%" + SearchKey + "%");

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
        public System.Data.DataTable SearchAsDataTable(string SearchKey, string SortField, SortOption SortOrder)
        {
            try
            {
                string SQL = SQLSelect() + "AND (CNNo LIKE @SearchKey or CNDate LIKE @SearchKey or CustomerCode LIKE @SearchKey " +
                                                "or CustomerContact LIKE @SearchKey or BranchCode LIKE @SearchKey or RequiredPostingDate LIKE @SearchKey) " +
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

                cmd.Parameters.AddWithValue("@SearchKey", "%" + SearchKey + "%");

                System.Data.DataTable dt = new System.Data.DataTable("SOCreditMemo");
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
                stRetValue = clsERPConfig.get_LastCreditMemoNo();

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
        public void SynchronizeAmount(long CreditMemoID)
        {
            try
            {
                string SQL = "CALL procSOCreditMemoSynchronizeAmount(@CreditMemoID);";

                MySqlConnection cn = GetConnection();

                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = cn;
                cmd.Transaction = mTransaction;
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandText = SQL;

                cmd.Parameters.AddWithValue("@CreditMemoID", CreditMemoID);

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
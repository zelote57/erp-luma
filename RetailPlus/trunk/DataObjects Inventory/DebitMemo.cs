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

    #region DebitMemoDetails

    public struct DebitMemoDetails
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
        public DebitMemoStatus DebitMemoStatus;
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
    public class DebitMemos : POSConnection
    {
        #region Constructors and Destructors

		public DebitMemos()
            : base(null, null)
        {
        }

        public DebitMemos(MySqlConnection Connection, MySqlTransaction Transaction) 
            : base(Connection, Transaction)
		{

		}

		#endregion

        #region Insert and Update: Insert, Update, Post

        public long Insert(DebitMemoDetails Details)
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

                cmd.Parameters.AddWithValue("@MemoNo", Details.MemoNo);
                cmd.Parameters.AddWithValue("@MemoDate", Details.MemoDate.ToString("yyyy-MM-dd HH:mm:ss"));
                cmd.Parameters.AddWithValue("@SupplierID", Details.SupplierID);
                cmd.Parameters.AddWithValue("@SupplierCode", Details.SupplierCode);
                cmd.Parameters.AddWithValue("@SupplierContact", Details.SupplierContact);
                cmd.Parameters.AddWithValue("@SupplierAddress", Details.SupplierAddress);
                cmd.Parameters.AddWithValue("@SupplierTelephoneNo", Details.SupplierTelephoneNo);
                cmd.Parameters.AddWithValue("@SupplierModeOfTerms", Details.SupplierModeOfTerms);
                cmd.Parameters.AddWithValue("@SupplierTerms", Details.SupplierTerms);
                cmd.Parameters.AddWithValue("@RequiredPostingDate", Details.RequiredPostingDate.ToString("yyyy-MM-dd HH:mm:ss"));
                cmd.Parameters.AddWithValue("@BranchID", Details.BranchID);
                cmd.Parameters.AddWithValue("@PurchaserID", Details.PurchaserID);
                cmd.Parameters.AddWithValue("@PurchaserName", Details.PurchaserName);
                cmd.Parameters.AddWithValue("@POReturnStatus", POReturnStatus.Posted.ToString("d"));
                cmd.Parameters.AddWithValue("@DebitMemoStatus", Details.DebitMemoStatus.ToString("d"));
                cmd.Parameters.AddWithValue("@Remarks", Details.Remarks);
                cmd.Parameters.AddWithValue("@ChartOfAccountIDAPTracking", clsAPLinkConfigDetails.ChartOfAccountIDAPTracking);
                cmd.Parameters.AddWithValue("@ChartOfAccountIDAPBills", clsAPLinkConfigDetails.ChartOfAccountIDAPBills);
                cmd.Parameters.AddWithValue("@ChartOfAccountIDAPFreight", clsAPLinkConfigDetails.ChartOfAccountIDAPFreight);
                cmd.Parameters.AddWithValue("@ChartOfAccountIDAPVDeposit", clsAPLinkConfigDetails.ChartOfAccountIDAPVDeposit);
                cmd.Parameters.AddWithValue("@ChartOfAccountIDAPContra", clsAPLinkConfigDetails.ChartOfAccountIDAPContra);
                cmd.Parameters.AddWithValue("@ChartOfAccountIDAPLatePayment", clsAPLinkConfigDetails.ChartOfAccountIDAPLatePayment);

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

        public void Update(DebitMemoDetails Details)
        {
            try
            {
                ERPConfig clsERPConfig = new ERPConfig(base.Connection, base.Transaction);
                APLinkConfigDetails clsAPLinkConfigDetails = clsERPConfig.APLinkDetails();

                string SQL = "UPDATE tblPODebitMemo SET " +
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

                cmd.Parameters.AddWithValue("@MemoNo", Details.MemoNo);
                cmd.Parameters.AddWithValue("@MemoDate", Details.MemoDate.ToString("yyyy-MM-dd HH:mm:ss"));
                cmd.Parameters.AddWithValue("@SupplierID", Details.SupplierID);
                cmd.Parameters.AddWithValue("@SupplierCode", Details.SupplierCode);
                cmd.Parameters.AddWithValue("@SupplierContact", Details.SupplierContact);
                cmd.Parameters.AddWithValue("@SupplierAddress", Details.SupplierAddress);
                cmd.Parameters.AddWithValue("@SupplierTelephoneNo", Details.SupplierTelephoneNo);
                cmd.Parameters.AddWithValue("@SupplierModeOfTerms", Details.SupplierModeOfTerms);
                cmd.Parameters.AddWithValue("@SupplierTerms", Details.SupplierTerms);
                cmd.Parameters.AddWithValue("@RequiredPostingDate", Details.RequiredPostingDate.ToString("yyyy-MM-dd HH:mm:ss"));
                cmd.Parameters.AddWithValue("@BranchID", Details.BranchID);
                cmd.Parameters.AddWithValue("@PurchaserID", Details.PurchaserID);
                cmd.Parameters.AddWithValue("@PurchaserName", Details.PurchaserName);
                cmd.Parameters.AddWithValue("@POReturnStatus", POReturnStatus.Posted.ToString("d"));
                cmd.Parameters.AddWithValue("@DebitMemoStatus", Details.DebitMemoStatus.ToString("d"));
                cmd.Parameters.AddWithValue("@Remarks", Details.Remarks);
                cmd.Parameters.AddWithValue("@ChartOfAccountIDAPTracking", clsAPLinkConfigDetails.ChartOfAccountIDAPTracking);
                cmd.Parameters.AddWithValue("@ChartOfAccountIDAPBills", clsAPLinkConfigDetails.ChartOfAccountIDAPBills);
                cmd.Parameters.AddWithValue("@ChartOfAccountIDAPFreight", clsAPLinkConfigDetails.ChartOfAccountIDAPFreight);
                cmd.Parameters.AddWithValue("@ChartOfAccountIDAPVDeposit", clsAPLinkConfigDetails.ChartOfAccountIDAPVDeposit);
                cmd.Parameters.AddWithValue("@ChartOfAccountIDAPContra", clsAPLinkConfigDetails.ChartOfAccountIDAPContra);
                cmd.Parameters.AddWithValue("@ChartOfAccountIDAPLatePayment", clsAPLinkConfigDetails.ChartOfAccountIDAPLatePayment);
                cmd.Parameters.AddWithValue("@DebitMemoID", Details.DebitMemoID);

                base.ExecuteNonQuery(cmd);
            }

            catch (Exception ex)
            {
                throw base.ThrowException(ex);
            }
        }

        public void UpdateDiscount(long DebitMemoID, decimal DiscountApplied, DiscountTypes DiscountType)
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

                cmd.Parameters.AddWithValue("@DiscountApplied", DiscountApplied);
                cmd.Parameters.AddWithValue("@DiscountType", Convert.ToInt16(DiscountType.ToString("d")));
                cmd.Parameters.AddWithValue("@DebitMemoID", DebitMemoID);

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

                cmd.Parameters.AddWithValue("@DiscountApplied", DiscountApplied);
                cmd.Parameters.AddWithValue("@DiscountType", Convert.ToInt16(DiscountType.ToString("d")));
                cmd.Parameters.AddWithValue("@DebitMemoID", DebitMemoID);

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

                cmd.Parameters.AddWithValue("@Freight", Freight);
                cmd.Parameters.AddWithValue("@DebitMemoID", DebitMemoID);

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

                cmd.Parameters.AddWithValue("@Deposit", Deposit);
                cmd.Parameters.AddWithValue("@DebitMemoID", DebitMemoID);

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
                string SQL = "UPDATE tblPODebitMemo SET " +
                                "SupplierDocNo		=	@SupplierDocNo, " +
                                "PostingDate		=	@PostingDate, " +
                                "DebitMemoStatus	=	@DebitMemoStatus " +
                            "WHERE DebitMemoID = @DebitMemoID;";

                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandText = SQL;

                cmd.Parameters.AddWithValue("@SupplierDocNo", SupplierDocNo);
                cmd.Parameters.AddWithValue("@PostingDate", PostingDate.ToString("yyyy-MM-dd HH:mm:ss"));
                cmd.Parameters.AddWithValue("@DebitMemoStatus", DebitMemoStatus.Posted.ToString("d"));
                cmd.Parameters.AddWithValue("@DebitMemoID", DebitMemoID);

                base.ExecuteNonQuery(cmd);

                /*******************************************
				 * Update the status of items
				 * ****************************************/
                DebitMemoItems clsDebitMemoItems = new DebitMemoItems(base.Connection, base.Transaction);
                clsDebitMemoItems.Post(DebitMemoID);

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
                DebitMemoDetails clsDebitMemoDetails = Details(DebitMemoID);
                ChartOfAccount clsChartOfAccount = new ChartOfAccount(base.Connection, base.Transaction);

                // update ChartOfAccountIDAPTracking as credit
                clsChartOfAccount.UpdateCredit(clsDebitMemoDetails.ChartOfAccountIDAPTracking, clsDebitMemoDetails.SubTotal);

                // update Deposit & APContra
                clsChartOfAccount.UpdateCredit(clsDebitMemoDetails.ChartOfAccountIDAPContra, clsDebitMemoDetails.Discount);

                // update Freight & APTracking
                clsChartOfAccount.UpdateDebit(clsDebitMemoDetails.ChartOfAccountIDAPTracking, clsDebitMemoDetails.Freight);
                clsChartOfAccount.UpdateCredit(clsDebitMemoDetails.ChartOfAccountIDAPFreight, clsDebitMemoDetails.Freight);

                // update Deposit & APTracking
                clsChartOfAccount.UpdateDebit(clsDebitMemoDetails.ChartOfAccountIDAPTracking, clsDebitMemoDetails.Deposit);
                clsChartOfAccount.UpdateCredit(clsDebitMemoDetails.ChartOfAccountIDAPVDeposit, clsDebitMemoDetails.Deposit);

                DebitMemoItems clsDebitMemoItems = new DebitMemoItems(base.Connection, base.Transaction);
                MySqlDataReader myReader = clsDebitMemoItems.List(DebitMemoID, string.Empty, SortOption.Ascending);
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
                throw base.ThrowException(ex);
            }
        }

        private void AddItemToInventory(long pvtDebitMemoID)
        {
            DebitMemoDetails clsDebitMemoDetails = Details(pvtDebitMemoID);
            ERPConfig clsERPConfig = new ERPConfig(base.Connection, base.Transaction);
            ERPConfigDetails clsERPConfigDetails = clsERPConfig.Details();

            DebitMemoItems clsDebitMemoItems = new DebitMemoItems(base.Connection, base.Transaction);
            ProductUnit clsProductUnit = new ProductUnit(base.Connection, base.Transaction);

            ProductDetails clsProductDetails = new ProductDetails();
            Products clsProduct = new Products(base.Connection, base.Transaction);
            ProductVariationsMatrix clsProductVariationsMatrix = new ProductVariationsMatrix(base.Connection, base.Transaction);
            ProductPackage clsProductPackage = new ProductPackage(base.Connection, base.Transaction);
            MatrixPackage clsMatrixPackage = new MatrixPackage(base.Connection, base.Transaction);

            Inventory clsInventory = new Inventory(base.Connection, base.Transaction);

            MatrixPackagePriceHistoryDetails clsMatrixPackagePriceHistoryDetails;
            ProductPackagePriceHistoryDetails clsProductPackagePriceHistoryDetails;
            MatrixPackagePriceHistory clsMatrixPackagePriceHistory = new MatrixPackagePriceHistory(base.Connection, base.Transaction);
            ProductPackagePriceHistory clsProductPackagePriceHistory = new ProductPackagePriceHistory(base.Connection, base.Transaction);

            MySqlDataReader myReader = clsDebitMemoItems.List(pvtDebitMemoID, "DebitMemoItemID", SortOption.Ascending);

            while (myReader.Read())
            {
                long lngProductID = myReader.GetInt64("ProductID");
                int intProductUnitID = myReader.GetInt32("ProductUnitID");

                decimal decItemQuantity = myReader.GetDecimal("Quantity");
                decimal decQuantity = clsProductUnit.GetBaseUnitValue(lngProductID, intProductUnitID, decItemQuantity);

                long lngVariationMatrixID = myReader.GetInt64("VariationMatrixID");
                string strMatrixDescription = "" + myReader["MatrixDescription"].ToString();
                string strProductCode = "" + myReader["ProductCode"].ToString();
                decimal decNewUnitCost = myReader.GetDecimal("UnitCost");
                decimal decAmount = myReader.GetDecimal("Amount");
                decimal decVAT = myReader.GetDecimal("VAT");

                clsProductDetails = clsProduct.Details(clsDebitMemoDetails.BranchID, lngProductID);
                /*******************************************
				 * Add in the Purchase Price History based on Debit Memo
				 * ****************************************/
                if (lngVariationMatrixID != 0)
                {
                    // Update MatrixPackagePriceHistory first to get the history
                    clsMatrixPackagePriceHistoryDetails = new MatrixPackagePriceHistoryDetails();
                    clsMatrixPackagePriceHistoryDetails.UID = clsDebitMemoDetails.PurchaserID;
                    clsMatrixPackagePriceHistoryDetails.PackageID = clsMatrixPackage.GetPackageID(lngVariationMatrixID, intProductUnitID);
                    clsMatrixPackagePriceHistoryDetails.ChangeDate = DateTime.Now;
                    clsMatrixPackagePriceHistoryDetails.PurchasePrice = decNewUnitCost * (decItemQuantity / decQuantity);
                    clsMatrixPackagePriceHistoryDetails.Price = -1;
                    clsMatrixPackagePriceHistoryDetails.VAT = -1;
                    clsMatrixPackagePriceHistoryDetails.EVAT = -1;
                    clsMatrixPackagePriceHistoryDetails.LocalTax = -1;
                    clsMatrixPackagePriceHistoryDetails.Remarks = "Based on DebitMemo #: " + clsDebitMemoDetails.MemoNo;
                    clsMatrixPackagePriceHistory.Insert(clsMatrixPackagePriceHistoryDetails);
                }
                else
                {
                    // Update ProductPackagePriceHistory first to get the history
                    clsProductPackagePriceHistoryDetails = new ProductPackagePriceHistoryDetails();
                    clsProductPackagePriceHistoryDetails.UID = clsDebitMemoDetails.PurchaserID;
                    clsProductPackagePriceHistoryDetails.PackageID = clsProductPackage.GetPackageID(lngProductID, intProductUnitID);
                    clsProductPackagePriceHistoryDetails.ChangeDate = DateTime.Now;
                    clsProductPackagePriceHistoryDetails.PurchasePrice = decNewUnitCost * (decItemQuantity / decQuantity);
                    clsProductPackagePriceHistoryDetails.Price = -1;
                    clsProductPackagePriceHistoryDetails.VAT = -1;
                    clsProductPackagePriceHistoryDetails.EVAT = -1;
                    clsProductPackagePriceHistoryDetails.LocalTax = -1;
                    clsProductPackagePriceHistoryDetails.Remarks = "Based on DebitMemo #: " + clsDebitMemoDetails.MemoNo;
                    clsProductPackagePriceHistory.Insert(clsProductPackagePriceHistoryDetails);
                }

                /*******************************************
                 * Subtract from Inventory : Remove this since this is a Debit Memo
                 * ****************************************/
                //clsProduct.SubtractQuantity(lngProductID, decQuantity);
                //if (lngVariationMatrixID != 0)
                //{
                //    clsProductVariationsMatrix.SubtractQuantity(lngVariationMatrixID, decQuantity);
                //}

                /*******************************************
				 * Update Purchasing Information
				 * ****************************************/
                if (intProductUnitID != clsProductDetails.BaseUnitID)
                {
                    clsProduct.UpdatePurchasing(lngProductID, clsDebitMemoDetails.SupplierID, clsProductDetails.BaseUnitID, decNewUnitCost * (decItemQuantity / decQuantity));
                }
                clsProduct.UpdatePurchasing(lngProductID, clsDebitMemoDetails.SupplierID, intProductUnitID, decNewUnitCost);

                /*******************************************
                 * Add to Inventory Analysis
                 * ****************************************/
                InventoryDetails clsInventoryDetails = new InventoryDetails();
                clsInventoryDetails.PostingDateFrom = clsERPConfigDetails.PostingDateFrom;
                clsInventoryDetails.PostingDateTo = clsERPConfigDetails.PostingDateTo;
                clsInventoryDetails.PostingDate = clsDebitMemoDetails.PostingDate;
                clsInventoryDetails.ReferenceNo = clsDebitMemoDetails.MemoNo;
                clsInventoryDetails.ContactID = clsDebitMemoDetails.SupplierID;
                clsInventoryDetails.ContactCode = clsDebitMemoDetails.SupplierCode;
                clsInventoryDetails.ProductID = lngProductID;
                clsInventoryDetails.ProductCode = strProductCode;
                clsInventoryDetails.VariationMatrixID = lngVariationMatrixID;
                clsInventoryDetails.MatrixDescription = strMatrixDescription;
                clsInventoryDetails.PDebitQuantity = decQuantity;
                clsInventoryDetails.PDebitCost = decAmount - decVAT;
                clsInventoryDetails.PDebitVAT = decAmount;

                clsInventory.Insert(clsInventoryDetails);

            }
            myReader.Close();

        }

        public void Cancel(long DebitMemoID, DateTime CancelledDate, string Remarks, long CancelledByID)
        {
            try
            {
                string SQL = "UPDATE tblPODebitMemo SET " +
                                "CancelledDate			=	@CancelledDate, " +
                                "CancelledRemarks		=	@CancelledRemarks, " +
                                "CancelledByID			=	@CancelledByID, " +
                                "DebitMemoStatus		=	@DebitMemoStatus " +
                            "WHERE DebitMemoID = @DebitMemoID;";

                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandText = SQL;

                cmd.Parameters.AddWithValue("@CancelledDate", CancelledDate.ToString("yyyy-MM-dd HH:mm:ss"));
                cmd.Parameters.AddWithValue("@CancelledRemarks", Remarks);
                cmd.Parameters.AddWithValue("@CancelledByID", CancelledByID);
                cmd.Parameters.AddWithValue("@DebitMemoStatus", DebitMemoStatus.Cancelled.ToString("d"));
                cmd.Parameters.AddWithValue("@DebitMemoID", DebitMemoID);

                base.ExecuteNonQuery(cmd);

                /*******************************************
                 * Update the status of items
                 * ****************************************/
                DebitMemoItems clsDebitMemoItems = new DebitMemoItems(base.Connection, base.Transaction);
                clsDebitMemoItems.Cancel(DebitMemoID);

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
                string SQL = "DELETE FROM tblPODebitMemo WHERE DebitMemoID IN (" + IDs + ");";

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
                            "DebitMemoStatus, " +
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
                        "WHERE 1=1 AND POReturnStatus = " + POReturnStatus.Posted.ToString("d") + " ";

            return SQL;

        }

        #region Details

        public DebitMemoDetails Details(long DebitMemoID)
        {
            try
            {
                string SQL = SQLSelect() + "AND DebitMemoID = @DebitMemoID;";

                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandText = SQL;

                MySqlParameter prmDebitMemoID = new MySqlParameter("@DebitMemoID",MySqlDbType.Int16);
                prmDebitMemoID.Value = DebitMemoID;
                cmd.Parameters.Add(prmDebitMemoID);

                MySqlDataReader myReader = base.ExecuteReader(cmd, System.Data.CommandBehavior.SingleResult);

                DebitMemoDetails Details = new DebitMemoDetails();

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
                    Details.DebitMemoStatus = (DebitMemoStatus)Enum.Parse(typeof(DebitMemoStatus), myReader.GetString("DebitMemoStatus"));
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
        public MySqlDataReader List(DebitMemoStatus DebitMemoStatus, string SortField, SortOption SortOrder)
        {
            try
            {
                string SQL = SQLSelect() + "AND DebitMemoStatus = @DebitMemoStatus " +
                            "ORDER BY " + SortField;

                if (SortOrder == SortOption.Ascending)
                    SQL += " ASC";
                else
                    SQL += " DESC";

                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandText = SQL;

                MySqlParameter prmDebitMemoStatus = new MySqlParameter("@DebitMemoStatus",MySqlDbType.Int16);
                prmDebitMemoStatus.Value = DebitMemoStatus.ToString("d");
                cmd.Parameters.Add(prmDebitMemoStatus);

                MySqlDataReader myReader = base.ExecuteReader(cmd);

                return myReader;
            }
            catch (Exception ex)
            {
                throw base.ThrowException(ex);
            }
        }
        public MySqlDataReader List(DebitMemoStatus DebitMemoStatus, long SupplierID, string SortField, SortOption SortOrder)
        {
            try
            {
                string SQL = SQLSelect() + "AND DebitMemoStatus = @DebitMemoStatus " +
                                "AND SupplierID = @SupplierID " +
                            "ORDER BY " + SortField;

                if (SortOrder == SortOption.Ascending)
                    SQL += " ASC";
                else
                    SQL += " DESC";

                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandText = SQL;

                MySqlParameter prmDebitMemoStatus = new MySqlParameter("@DebitMemoStatus",MySqlDbType.Int16);
                prmDebitMemoStatus.Value = DebitMemoStatus.ToString("d");
                cmd.Parameters.Add(prmDebitMemoStatus);

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
        public MySqlDataReader List(DebitMemoStatus DebitMemoStatus, DateTime StartDate, DateTime EndDate)
        {
            try
            {
                string SQL = SQLSelect() + "AND DebitMemoStatus = @DebitMemoStatus " +
                        "AND PostingDate BETWEEN @StartDate AND @EndDate " +
                    "ORDER BY DebitMemoID ASC";

                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandText = SQL;

                MySqlParameter prmDebitMemoStatus = new MySqlParameter("@DebitMemoStatus",MySqlDbType.Int16);
                prmDebitMemoStatus.Value = DebitMemoStatus.ToString("d");
                cmd.Parameters.Add(prmDebitMemoStatus);

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
        public MySqlDataReader List(DebitMemoStatus DebitMemoStatus, long SupplierID, DateTime StartDate, DateTime EndDate)
        {
            try
            {
                string SQL = SQLSelect() + "AND DebitMemoStatus = @DebitMemoStatus " +
                        "AND SupplierID = @SupplierID " +
                        "AND PostingDate BETWEEN @StartDate AND @EndDate " +
                    "ORDER BY DebitMemoID ASC";

                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandText = SQL;

                MySqlParameter prmDebitMemoStatus = new MySqlParameter("@DebitMemoStatus",MySqlDbType.Int16);
                prmDebitMemoStatus.Value = DebitMemoStatus.ToString("d");
                cmd.Parameters.Add(prmDebitMemoStatus);

                cmd.Parameters.AddWithValue("@DebitMemoStatus", DebitMemoStatus.ToString("d"));
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
        public MySqlDataReader Search(DebitMemoStatus Status, string SearchKey, string SortField, SortOption SortOrder)
        {
            try
            {
                string SQL = SQLSelect() + "AND DebitMemoStatus = @DebitMemoStatus " +
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

                MySqlParameter prmDebitMemoStatus = new MySqlParameter("@DebitMemoStatus",MySqlDbType.Int16);
                prmDebitMemoStatus.Value = Status.ToString("d");
                cmd.Parameters.Add(prmDebitMemoStatus);

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
        public System.Data.DataTable SearchAsDataTable(string SearchKey, string SortField, SortOption SortOrder)
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

                string strDataTableName = "tbl" + this.GetType().FullName.Split(new Char[] { '.' })[this.GetType().FullName.Split(new Char[] { '.' }).Length - 1]; System.Data.DataTable dt = new System.Data.DataTable(strDataTableName);
                base.MySqlDataAdapterFill(cmd, dt);

                return dt;
            }
            catch (Exception ex)
            {
                throw base.ThrowException(ex);
            }
        }

        public System.Data.DataTable SearchAsDataTable(DebitMemoStatus status, DateTime OrderStartDate, DateTime OrderEndDate, DateTime PostingStartDate, DateTime PostingEndDate, string SearchKey, string SortField, SortOption SortOrder)
        {
            try
            {
                if (SortField == string.Empty || SortField == null) SortField = "DebitMemoID";

                string SQL = SQLSelect() + "AND DebitMemoStatus = @Status " +
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
                stRetValue = clsERPConfig.get_LastDebitMemoNo();

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
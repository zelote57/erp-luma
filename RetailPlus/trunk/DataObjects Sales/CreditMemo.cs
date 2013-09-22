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
    public class CreditMemos : POSConnection
    {
        #region Constructors and Destructors

		public CreditMemos()
            : base(null, null)
        {
        }

        public CreditMemos(MySqlConnection Connection, MySqlTransaction Transaction) 
            : base(Connection, Transaction)
		{

		}

		#endregion

        #region Insert and Update: Insert, Update, Post

        public long Insert(CreditMemoDetails Details)
        {
            try
            {
                SysConfig clsERPConfig = new SysConfig(base.Connection, base.Transaction);
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

                

                MySqlCommand cmd = new MySqlCommand();
                
                
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

                base.ExecuteNonQuery(cmd);

                SQL = "SELECT LAST_INSERT_ID();";

                cmd.Parameters.Clear();
                cmd.CommandText = SQL;

                MySqlDataReader myReader = base.ExecuteReader(cmd, System.Data.CommandBehavior.SingleResult);

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
                
                
                {
                    
                    
                    
                    
                }

                throw base.ThrowException(ex);
            }
        }

        public void Update(CreditMemoDetails Details)
        {
            try
            {
                SysConfig clsERPConfig = new SysConfig(base.Connection, base.Transaction);
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

                

                MySqlCommand cmd = new MySqlCommand();
                
                
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

                base.ExecuteNonQuery(cmd);
            }

            catch (Exception ex)
            {
                
                
                {
                    
                    
                    
                    
                }

                throw base.ThrowException(ex);
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

                

                MySqlCommand cmd = new MySqlCommand();
                
                
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandText = SQL;

                MySqlParameter prmDiscountApplied = new MySqlParameter("@DiscountApplied",MySqlDbType.Decimal);
                prmDiscountApplied.Value = DiscountApplied;
                cmd.Parameters.Add(prmDiscountApplied);

                MySqlParameter prmDiscountType = new MySqlParameter("@DiscountType",MySqlDbType.Int16);
                prmDiscountType.Value = Convert.ToInt16(DiscountType.ToString("d"));
                cmd.Parameters.Add(prmDiscountType);

                MySqlParameter prmCreditMemoID = new MySqlParameter("@CreditMemoID",MySqlDbType.Int64);
                prmCreditMemoID.Value = CreditMemoID;
                cmd.Parameters.Add(prmCreditMemoID);

                base.ExecuteNonQuery(cmd);
            }

            catch (Exception ex)
            {
                
                
                {
                    
                    
                    
                    
                }

                throw base.ThrowException(ex);
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

                

                MySqlCommand cmd = new MySqlCommand();
                
                
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandText = SQL;

                MySqlParameter prmDiscountApplied = new MySqlParameter("@DiscountApplied",MySqlDbType.Decimal);
                prmDiscountApplied.Value = DiscountApplied;
                cmd.Parameters.Add(prmDiscountApplied);

                MySqlParameter prmDiscountType = new MySqlParameter("@DiscountType",MySqlDbType.Int16);
                prmDiscountType.Value = Convert.ToInt16(DiscountType.ToString("d"));
                cmd.Parameters.Add(prmDiscountType);

                MySqlParameter prmCreditMemoID = new MySqlParameter("@CreditMemoID",MySqlDbType.Int64);
                prmCreditMemoID.Value = CreditMemoID;
                cmd.Parameters.Add(prmCreditMemoID);

                base.ExecuteNonQuery(cmd);
            }

            catch (Exception ex)
            {
                
                
                {
                    
                    
                    
                    
                }

                throw base.ThrowException(ex);
            }
        }
        public void UpdateFreight(long CreditMemoID, decimal Freight)
        {
            try
            {
                string SQL = "UPDATE tblSOCreditMemo SET " +
                                "Freight           =   @Freight " +
                            "WHERE CreditMemoID = @CreditMemoID;";

                

                MySqlCommand cmd = new MySqlCommand();
                
                
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandText = SQL;

                MySqlParameter prmFreight = new MySqlParameter("@Freight",MySqlDbType.Decimal);
                prmFreight.Value = Freight;
                cmd.Parameters.Add(prmFreight);

                MySqlParameter prmCreditMemoID = new MySqlParameter("@CreditMemoID",MySqlDbType.Int64);
                prmCreditMemoID.Value = CreditMemoID;
                cmd.Parameters.Add(prmCreditMemoID);

                base.ExecuteNonQuery(cmd);
            }

            catch (Exception ex)
            {
                
                
                {
                    
                    
                    
                    
                }

                throw base.ThrowException(ex);
            }
        }
        public void UpdateDeposit(long CreditMemoID, decimal Deposit)
        {
            try
            {
                string SQL = "UPDATE tblSOCreditMemo SET " +
                                "Deposit           =   @Deposit " +
                            "WHERE CreditMemoID = @CreditMemoID;";

                

                MySqlCommand cmd = new MySqlCommand();
                
                
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandText = SQL;

                MySqlParameter prmDeposit = new MySqlParameter("@Deposit",MySqlDbType.Decimal);
                prmDeposit.Value = Deposit;
                cmd.Parameters.Add(prmDeposit);

                MySqlParameter prmCreditMemoID = new MySqlParameter("@CreditMemoID",MySqlDbType.Int64);
                prmCreditMemoID.Value = CreditMemoID;
                cmd.Parameters.Add(prmCreditMemoID);

                base.ExecuteNonQuery(cmd);
            }

            catch (Exception ex)
            {
                
                
                {
                    
                    
                    
                    
                }

                throw base.ThrowException(ex);
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

                

                MySqlCommand cmd = new MySqlCommand();
                
                
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandText = SQL;

                cmd.Parameters.AddWithValue("@CustomerDocNo", CustomerDocNo);
                cmd.Parameters.AddWithValue("@PostingDate", PostingDate.ToString("yyyy-MM-dd HH:mm:ss"));
                cmd.Parameters.AddWithValue("@SOReturnStatus", SOReturnStatus.Posted.ToString("d"));
                cmd.Parameters.AddWithValue("@CreditMemoStatus", CreditMemoStatus.Posted.ToString("d"));
                cmd.Parameters.AddWithValue("@CreditMemoID", CreditMemoID);

                base.ExecuteNonQuery(cmd);

                /*******************************************
                 * Update the status of items
                 * ****************************************/
                CreditMemoItems clsCreditMemoItems = new CreditMemoItems(base.Connection, base.Transaction);
                clsCreditMemoItems.Post(CreditMemoID);

                /*******************************************
                 * Update Customer Account
                 * ****************************************/
                AddItemToInventory(CreditMemoID);
            }

            catch (Exception ex)
            {
                throw base.ThrowException(ex);
            }
        }

        private void UpdateAccounts(long CreditMemoID)
        {
            try
            {
                CreditMemoDetails clsCreditMemoDetails = Details(CreditMemoID);
                ChartOfAccount clsChartOfAccount = new ChartOfAccount(base.Connection, base.Transaction);

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

                CreditMemoItems clsCreditMemoItems = new CreditMemoItems(base.Connection, base.Transaction);
                System.Data.DataTable dt = clsCreditMemoItems.ListAsDataTable(CreditMemoID);
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
                //myReader.Close();

            }

            catch (Exception ex)
            {
                throw base.ThrowException(ex);
            }
        }

        private void AddItemToInventory(long CreditMemoID)
        {

            CreditMemoDetails clsCreditMemoDetails = Details(CreditMemoID);
            SysConfig clsERPConfig = new SysConfig(base.Connection, base.Transaction);
            ERPConfigDetails clsERPConfigDetails = clsERPConfig.Details();

            CreditMemoItems clsCreditMemoItems = new CreditMemoItems(base.Connection, base.Transaction);
            ProductUnit clsProductUnit = new ProductUnit(base.Connection, base.Transaction);
            Products clsProduct = new Products(base.Connection, base.Transaction);
            ProductVariationsMatrix clsProductVariationsMatrix = new ProductVariationsMatrix(base.Connection, base.Transaction);

            Inventory clsInventory = new Inventory(base.Connection, base.Transaction);

            System.Data.DataTable dt = clsCreditMemoItems.ListAsDataTable(CreditMemoID);

            foreach (System.Data.DataRow dr in dt.Rows)
            {
                long lngProductID = long.Parse(dr["ProductID"].ToString());
                int intProductUnitID = int.Parse(dr["ProductUnitID"].ToString());

                decimal decItemQuantity = decimal.Parse(dr["Quantity"].ToString());
                decimal decQuantity = clsProductUnit.GetBaseUnitValue(lngProductID, intProductUnitID, decItemQuantity);

                long lngVariationMatrixID = long.Parse(dr["VariationMatrixID"].ToString());
                string strMatrixDescription = "" + dr["MatrixDescription"].ToString();
                string strProductCode = "" + dr["ProductCode"].ToString();
                decimal decUnitCost = decimal.Parse(dr["UnitCost"].ToString());
                decimal decItemCost = decimal.Parse(dr["Amount"].ToString());
                decimal decVAT = decimal.Parse(dr["VAT"].ToString());

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
                clsInventoryDetails.ProductID = lngProductID;
                clsInventoryDetails.ProductCode = strProductCode;
                clsInventoryDetails.VariationMatrixID = lngVariationMatrixID;
                clsInventoryDetails.MatrixDescription = strMatrixDescription;
                clsInventoryDetails.SCreditQuantity = decQuantity;
                clsInventoryDetails.SCreditCost = decItemCost - decVAT;
                clsInventoryDetails.SCreditVAT = decItemCost;	//Sales Return with VAT

                clsInventory.Insert(clsInventoryDetails);

            }
            //myReader.Close();

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

                MySqlParameter prmCreditMemoStatus = new MySqlParameter("@CreditMemoStatus",MySqlDbType.Int16);
                prmCreditMemoStatus.Value = CreditMemoStatus.Cancelled.ToString("d");
                cmd.Parameters.Add(prmCreditMemoStatus);

                MySqlParameter prmCreditMemoID = new MySqlParameter("@CreditMemoID",MySqlDbType.Int64);
                prmCreditMemoID.Value = CreditMemoID;
                cmd.Parameters.Add(prmCreditMemoID);

                base.ExecuteNonQuery(cmd);

                /*******************************************
                 * Update the status of items
                 * ****************************************/
                CreditMemoItems clsCreditMemoItems = new CreditMemoItems(base.Connection, base.Transaction);
                clsCreditMemoItems.Cancel(CreditMemoID);

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
                string SQL = "DELETE FROM tblSOCreditMemo WHERE CreditMemoID IN (" + IDs + ");";

                

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

                

                MySqlCommand cmd = new MySqlCommand();
                
                
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandText = SQL;

                cmd.Parameters.AddWithValue("@CreditMemoID", CreditMemoID);

                MySqlDataReader myReader = base.ExecuteReader(cmd, System.Data.CommandBehavior.SingleResult);

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
                
                
                {
                    
                    
                    
                    
                }

                throw base.ThrowException(ex);
            }
        }

        #endregion

        #region Streams: ListAsDataTable, List, Search

        public System.Data.DataTable ListAsDataTable(string SortField = "CreditMemoID", SortOption SortOrder = SortOption.Ascending)
        {
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
        public System.Data.DataTable ListAsDataTable(CreditMemoStatus CreditMemoStatus, string SortField = "CreditMemoID", SortOption SortOrder = SortOption.Ascending)
        {
            string SQL = SQLSelect() + "WHERE CreditMemoStatus = @CreditMemoStatus ORDER BY " + SortField;

            if (SortOrder == SortOption.Ascending)
                SQL += " ASC";
            else
                SQL += " DESC";

            MySqlCommand cmd = new MySqlCommand();
            cmd.CommandType = System.Data.CommandType.Text;
            cmd.CommandText = SQL;

            cmd.Parameters.AddWithValue("@CreditMemoStatus", CreditMemoStatus.ToString("d"));

            string strDataTableName = "tbl" + this.GetType().FullName.Split(new Char[] { '.' })[this.GetType().FullName.Split(new Char[] { '.' }).Length - 1]; System.Data.DataTable dt = new System.Data.DataTable(strDataTableName);
            base.MySqlDataAdapterFill(cmd, dt);

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

                

                MySqlCommand cmd = new MySqlCommand();
                
                
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandText = SQL;

                cmd.Parameters.AddWithValue("@CreditMemoID", CreditMemoID);

                MySqlDataReader myReader = base.ExecuteReader(cmd);

                return myReader;
            }
            catch (Exception ex)
            {
                
                
                {
                    
                    
                    
                    
                }

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
                
                
                {
                    
                    
                    
                    
                }

                throw base.ThrowException(ex);
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

                

                MySqlCommand cmd = new MySqlCommand();
                
                
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandText = SQL;

                cmd.Parameters.AddWithValue("@CreditMemoStatus", CreditMemoStatus.ToString("d"));

                MySqlDataReader myReader = base.ExecuteReader(cmd);

                return myReader;
            }
            catch (Exception ex)
            {
                
                
                {
                    
                    
                    
                    
                }

                throw base.ThrowException(ex);
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

                

                MySqlCommand cmd = new MySqlCommand();
                
                
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandText = SQL;

                cmd.Parameters.AddWithValue("@CreditMemoStatus", CreditMemoStatus.ToString("d"));
                cmd.Parameters.AddWithValue("@CustomerID", CustomerID);

                MySqlDataReader myReader = base.ExecuteReader(cmd);

                return myReader;
            }
            catch (Exception ex)
            {
                
                
                {
                    
                    
                    
                    
                }

                throw base.ThrowException(ex);
            }
        }
        public MySqlDataReader List(CreditMemoStatus CreditMemoStatus, DateTime StartDate, DateTime EndDate)
        {
            try
            {
                string SQL = SQLSelect() + "AND CreditMemoStatus = @CreditMemoStatus " +
                        "AND PostingDate BETWEEN @StartDate AND @EndDate " +
                    "ORDER BY CreditMemoID ASC";

                

                MySqlCommand cmd = new MySqlCommand();
                
                
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandText = SQL;

                cmd.Parameters.AddWithValue("@CreditMemoStatus", CreditMemoStatus.ToString("d"));
                cmd.Parameters.AddWithValue("@StartDate", StartDate.ToString("yyyy-MM-dd HH:mm:ss"));
                cmd.Parameters.AddWithValue("@EndDate", EndDate.ToString("yyyy-MM-dd HH:mm:ss"));

                MySqlDataReader myReader = base.ExecuteReader(cmd);

                return myReader;
            }
            catch (Exception ex)
            {
                
                
                {
                    
                    
                    
                    
                }

                throw base.ThrowException(ex);
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

                

                MySqlCommand cmd = new MySqlCommand();
                
                
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandText = SQL;

                cmd.Parameters.AddWithValue("@SearchKey", "%" + SearchKey + "%");

                MySqlDataReader myReader = base.ExecuteReader(cmd);

                return myReader;
            }
            catch (Exception ex)
            {
                
                
                {
                    
                    
                    
                    
                }

                throw base.ThrowException(ex);
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

                

                MySqlCommand cmd = new MySqlCommand();
                
                
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
                
                
                {
                    
                    
                    
                    
                }

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

                SysConfig clsERPConfig = new SysConfig(base.Connection, base.Transaction);
                stRetValue = clsERPConfig.get_LastCreditMemoNo();

                return stRetValue;
            }

            catch (Exception ex)
            {
                
                
                {
                    
                    
                    
                    
                }

                throw base.ThrowException(ex);
            }
        }
        public void SynchronizeAmount(long CreditMemoID)
        {
            try
            {
                string SQL = "CALL procSOCreditMemoSynchronizeAmount(@CreditMemoID);";

                

                MySqlCommand cmd = new MySqlCommand();
                
                
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandText = SQL;

                cmd.Parameters.AddWithValue("@CreditMemoID", CreditMemoID);

                base.ExecuteNonQuery(cmd);
            }

            catch (Exception ex)
            {
                
                
                {
                    
                    
                    
                    
                }

                throw base.ThrowException(ex);
            }
        }

        #endregion

    }
}
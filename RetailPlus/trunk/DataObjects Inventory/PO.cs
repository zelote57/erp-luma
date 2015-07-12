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

	#region PODetails

	public struct PODetails
	{
		public long POID;
		public string PONo;
		public DateTime PODate;
		public long SupplierID;
		public string SupplierCode;
		public string SupplierContact;
		public string SupplierAddress;
		public string SupplierTelephoneNo;
		public int SupplierModeOfTerms;
		public int SupplierTerms;
        public string SupplierTINNo;
        public string SupplierLTONo;
		public DateTime RequiredDeliveryDate;
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
		public POStatus Status;
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

        // Aug 6, 2011 RequiredInventoryDays
        public long RID;

        // 05Jun2015
        public bool IncludeIneSales;
        
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
	public class PO : POSConnection
	{
		#region Constructors and Destructors

		public PO()
            : base(null, null)
        {
        }

        public PO(MySqlConnection Connection, MySqlTransaction Transaction) 
            : base(Connection, Transaction)
		{

		}

		#endregion

		#region Insert and Update

		public long Insert(PODetails Details)
		{
			try 
			{
                ERPConfig clsERPConfig = new ERPConfig(base.Connection, base.Transaction);
                APLinkConfigDetails clsAPLinkConfigDetails = clsERPConfig.APLinkDetails();

                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandType = System.Data.CommandType.Text;

				string SQL = "INSERT INTO tblPO (" +
								"PONo, " +
								"PODate, " +
								"SupplierID, " +
								"SupplierCode, " +
								"SupplierContact, " +
								"SupplierAddress, " +
								"SupplierTelephoneNo, " +
								"SupplierModeOfTerms, " +
								"SupplierTerms, " +
                                "SupplierTINNo, " +
                                "SupplierLTONo, " +
								"RequiredDeliveryDate, " +
                                "RID, " +
								"BranchID, " +
								"PurchaserID, " +
                                "PurchaserName, " +
								"Status, " +
								"Remarks, " +
                                "ChartOfAccountIDAPTracking, " +
                                "ChartOfAccountIDAPBills, " +
                                "ChartOfAccountIDAPFreight, " +
                                "ChartOfAccountIDAPVDeposit, " +
                                "ChartOfAccountIDAPContra, " +
                                "ChartOfAccountIDAPLatePayment, " +
                                "IncludeIneSales" +
							") VALUES (" +
                                "@PONo, " +
                                "@PODate, " +
                                "@SupplierID, " +
                                "@SupplierCode, " +
                                "@SupplierContact, " +
                                "@SupplierAddress, " +
                                "@SupplierTelephoneNo, " +
                                "@SupplierModeOfTerms, " +
                                "@SupplierTerms, " +
                                "@SupplierTINNo, " +
                                "@SupplierLTONo, " +
                                "@RequiredDeliveryDate, " +
                                "@RID, " +
                                "@BranchID, " +
                                "@PurchaserID, " +
                                "@PurchaserName, " +
                                "@Status, " +
                                "@Remarks, " +
                                "@ChartOfAccountIDAPTracking, " +
                                "@ChartOfAccountIDAPBills, " +
                                "@ChartOfAccountIDAPFreight, " +
                                "@ChartOfAccountIDAPVDeposit, " +
                                "@ChartOfAccountIDAPContra, " +
                                "@ChartOfAccountIDAPLatePayment, " +
                                "@IncludeIneSales" +
							");";
				
                cmd.Parameters.AddWithValue("@PONo", Details.PONo);
				cmd.Parameters.AddWithValue("@PODate", Details.PODate.ToString("yyyy-MM-dd HH:mm:ss"));
				cmd.Parameters.AddWithValue("@SupplierID", Details.SupplierID);
				cmd.Parameters.AddWithValue("@SupplierCode", Details.SupplierCode);
				cmd.Parameters.AddWithValue("@SupplierContact", Details.SupplierContact);
                cmd.Parameters.AddWithValue("@SupplierAddress", Details.SupplierAddress);
                cmd.Parameters.AddWithValue("@SupplierTelephoneNo", Details.SupplierTelephoneNo);
                cmd.Parameters.AddWithValue("@SupplierModeOfTerms", Details.SupplierModeOfTerms);
                cmd.Parameters.AddWithValue("@SupplierTerms", Details.SupplierTerms);
                cmd.Parameters.AddWithValue("@SupplierTINNo", Details.SupplierTINNo);
                cmd.Parameters.AddWithValue("@SupplierLTONo", Details.SupplierLTONo);
                cmd.Parameters.AddWithValue("@RequiredDeliveryDate", Details.RequiredDeliveryDate.ToString("yyyy-MM-dd HH:mm:ss"));
                cmd.Parameters.AddWithValue("@RID", Details.RID);
                cmd.Parameters.AddWithValue("@BranchID", Details.BranchID);
                cmd.Parameters.AddWithValue("@PurchaserID", Details.PurchaserID);
                cmd.Parameters.AddWithValue("@PurchaserName", Details.PurchaserName);
                cmd.Parameters.AddWithValue("@Status", Details.Status.ToString("d"));
                cmd.Parameters.AddWithValue("@Remarks", Details.Remarks);
                cmd.Parameters.AddWithValue("@ChartOfAccountIDAPTracking", clsAPLinkConfigDetails.ChartOfAccountIDAPTracking);
                cmd.Parameters.AddWithValue("@ChartOfAccountIDAPBills", clsAPLinkConfigDetails.ChartOfAccountIDAPBills);
                cmd.Parameters.AddWithValue("@ChartOfAccountIDAPFreight", clsAPLinkConfigDetails.ChartOfAccountIDAPFreight);
                cmd.Parameters.AddWithValue("@ChartOfAccountIDAPVDeposit", clsAPLinkConfigDetails.ChartOfAccountIDAPVDeposit);
                cmd.Parameters.AddWithValue("@ChartOfAccountIDAPContra", clsAPLinkConfigDetails.ChartOfAccountIDAPContra);
                cmd.Parameters.AddWithValue("@ChartOfAccountIDAPLatePayment", clsAPLinkConfigDetails.ChartOfAccountIDAPLatePayment);
                cmd.Parameters.AddWithValue("@IncludeIneSales", Details.IncludeIneSales);

                cmd.CommandText = SQL;
                base.ExecuteNonQuery(cmd);

                return Int64.Parse(base.getLAST_INSERT_ID(this));
			}
			catch (Exception ex)
			{
				throw base.ThrowException(ex);
			}	
		}
		public void Update(PODetails Details)
		{
			try 
			{
                ERPConfig clsERPConfig = new ERPConfig(base.Connection, base.Transaction);
                APLinkConfigDetails clsAPLinkConfigDetails = clsERPConfig.APLinkDetails();

                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandType = System.Data.CommandType.Text;
				
                string SQL=	"UPDATE tblPO SET " +
                                "PONo					=	@PONo, " +
                                "PODate					=	@PODate, " +
                                "SupplierID				=	@SupplierID, " +
                                "SupplierCode			=	@SupplierCode, " +
                                "SupplierContact		=	@SupplierContact, " +
                                "SupplierAddress		=	@SupplierAddress, " +
                                "SupplierTelephoneNo	=	@SupplierTelephoneNo, " +
                                "SupplierModeOfTerms	=	@SupplierModeOfTerms, " +
                                "SupplierTerms			=	@SupplierTerms, " +
                                "SupplierTINNo          =	@SupplierTINNo, " +
                                "SupplierLTONo          =	@SupplierLTONo, " +
                                "RequiredDeliveryDate	=	@RequiredDeliveryDate, " +
                                "RID	                =	@RID, " +
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
							"WHERE POID = @POID;";
				  
				

                cmd.Parameters.AddWithValue("@PONo", Details.PONo);
                cmd.Parameters.AddWithValue("@PODate", Details.PODate.ToString("yyyy-MM-dd HH:mm:ss"));
                cmd.Parameters.AddWithValue("@SupplierID", Details.SupplierID);
                cmd.Parameters.AddWithValue("@SupplierCode", Details.SupplierCode);
                cmd.Parameters.AddWithValue("@SupplierContact", Details.SupplierContact);
                cmd.Parameters.AddWithValue("@SupplierAddress", Details.SupplierAddress);
                cmd.Parameters.AddWithValue("@SupplierTelephoneNo", Details.SupplierTelephoneNo);
                cmd.Parameters.AddWithValue("@SupplierModeOfTerms", Details.SupplierModeOfTerms);
                cmd.Parameters.AddWithValue("@SupplierTerms", Details.SupplierTerms);
                cmd.Parameters.AddWithValue("@SupplierTINNo", Details.SupplierTINNo);
                cmd.Parameters.AddWithValue("@SupplierLTONo", Details.SupplierLTONo);
                cmd.Parameters.AddWithValue("@RequiredDeliveryDate", Details.RequiredDeliveryDate.ToString("yyyy-MM-dd HH:mm:ss"));
                cmd.Parameters.AddWithValue("@RID", Details.RID);
                cmd.Parameters.AddWithValue("@BranchID", Details.BranchID);
                cmd.Parameters.AddWithValue("@PurchaserID", Details.PurchaserID);
                cmd.Parameters.AddWithValue("@PurchaserName", Details.PurchaserName);
                cmd.Parameters.AddWithValue("@Remarks", Details.Remarks);
                cmd.Parameters.AddWithValue("@ChartOfAccountIDAPTracking", clsAPLinkConfigDetails.ChartOfAccountIDAPTracking);
                cmd.Parameters.AddWithValue("@ChartOfAccountIDAPBills", clsAPLinkConfigDetails.ChartOfAccountIDAPBills);
                cmd.Parameters.AddWithValue("@ChartOfAccountIDAPFreight", clsAPLinkConfigDetails.ChartOfAccountIDAPFreight);
                cmd.Parameters.AddWithValue("@ChartOfAccountIDAPVDeposit", clsAPLinkConfigDetails.ChartOfAccountIDAPVDeposit);
                cmd.Parameters.AddWithValue("@ChartOfAccountIDAPContra", clsAPLinkConfigDetails.ChartOfAccountIDAPContra);
                cmd.Parameters.AddWithValue("@ChartOfAccountIDAPLatePayment", clsAPLinkConfigDetails.ChartOfAccountIDAPLatePayment);
                cmd.Parameters.AddWithValue("@POID", Details.POID);

                cmd.CommandText = SQL;
				base.ExecuteNonQuery(cmd);
			}
			catch (Exception ex)
			{
				throw base.ThrowException(ex);
			}	
		}

        public void UpdateIncludeIneSales(Int64 POID, bool IncludeIneSales)
        {
            try
            {
                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandType = System.Data.CommandType.Text;

                string SQL = "UPDATE tblPO SET " +
                                "IncludeIneSales          =   @IncludeIneSales " +
                            "WHERE POID = @POID;";

                cmd.Parameters.AddWithValue("@IncludeIneSales", IncludeIneSales);
                cmd.Parameters.AddWithValue("@POID", POID);

                cmd.CommandText = SQL;
                base.ExecuteNonQuery(cmd);
            }
            catch (Exception ex)
            {
                throw base.ThrowException(ex);
            }
        }

        public void UpdateIsVatInclusive(long POID, bool IsVatInclusive)
        {
            try
            {
                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandType = System.Data.CommandType.Text;

                string SQL = "UPDATE tblPO SET " +
                                "IsVatInclusive          =   @IsVatInclusive " +
                            "WHERE POID = @POID;";

                cmd.Parameters.AddWithValue("@IsVatInclusive", Convert.ToInt16(IsVatInclusive));
                cmd.Parameters.AddWithValue("@POID", POID);

                cmd.CommandText = SQL;
                base.ExecuteNonQuery(cmd);

                SynchronizeAmount(POID);
            }

            catch (Exception ex)
            {
                throw base.ThrowException(ex);
            }
        }
        public void UpdateDiscount(long POID, decimal DiscountApplied, DiscountTypes DiscountType, decimal Discount2Applied, DiscountTypes Discount2Type, decimal Discount3Applied, DiscountTypes Discount3Type)
        {
            try
            {
                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandType = System.Data.CommandType.Text;
                
                string SQL = "UPDATE tblPO SET " +
                                "DiscountApplied        =   @DiscountApplied, " +
                                "DiscountType           =   @DiscountType, " +
                                "Discount2Applied       =   @Discount2Applied, " +
                                "Discount2Type          =   @Discount2Type, " +
                                "Discount3Applied       =   @Discount3Applied, " +
                                "Discount3Type          =   @Discount3Type " +
                            "WHERE POID = @POID;";

                cmd.Parameters.AddWithValue("@DiscountApplied", DiscountApplied);
                cmd.Parameters.AddWithValue("@DiscountType", Convert.ToInt16(DiscountType.ToString("d")));
                cmd.Parameters.AddWithValue("@Discount2Applied", Discount2Applied);
                cmd.Parameters.AddWithValue("@Discount2Type", Convert.ToInt16(Discount2Type.ToString("d")));
                cmd.Parameters.AddWithValue("@Discount3Applied", Discount3Applied);
                cmd.Parameters.AddWithValue("@Discount3Type", Convert.ToInt16(Discount3Type.ToString("d")));
                cmd.Parameters.AddWithValue("@POID", POID);

                cmd.CommandText = SQL;
                base.ExecuteNonQuery(cmd);
            }
            catch (Exception ex)
            {
                throw base.ThrowException(ex);
            }
        }
        public void UpdateDiscountFreightDeposit(long POID, decimal DiscountApplied, DiscountTypes DiscountType)
        {
            try
            {
                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandType = System.Data.CommandType.Text;

                string SQL = "UPDATE tblPO SET " +
                                "DiscountApplied        =   @DiscountApplied, " +
                                "DiscountType           =   @DiscountType " +
                            "WHERE POID = @POID;";

                cmd.Parameters.AddWithValue("@DiscountApplied", DiscountApplied);
                cmd.Parameters.AddWithValue("@DiscountType", Convert.ToInt16(DiscountType.ToString("d")));
                cmd.Parameters.AddWithValue("@POID", POID);

                cmd.CommandText = SQL;
                base.ExecuteNonQuery(cmd);
            }
            catch (Exception ex)
            {
                throw base.ThrowException(ex);
            }
        }
        public void UpdateFreight(long POID, decimal Freight)
        {
            try
            {
                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandType = System.Data.CommandType.Text;

                string SQL = "UPDATE tblPO SET " +
                                "Freight           =   @Freight " +
                            "WHERE POID = @POID;";

                cmd.Parameters.AddWithValue("@Freight", Freight);
                cmd.Parameters.AddWithValue("@POID", POID);

                cmd.CommandText = SQL;
                base.ExecuteNonQuery(cmd);
            }
            catch (Exception ex)
            {
                throw base.ThrowException(ex);
            }
        }
        public void UpdateDeposit(long POID, decimal Deposit)
        {
            try
            {
                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandType = System.Data.CommandType.Text;
                
                string SQL = "UPDATE tblPO SET " +
                                "Deposit           =   @Deposit " +
                            "WHERE POID = @POID;";

                cmd.Parameters.AddWithValue("@Deposit", Deposit);
                cmd.Parameters.AddWithValue("@POID", POID);

                cmd.CommandText = SQL;
                base.ExecuteNonQuery(cmd);
            }
            catch (Exception ex)
            {
                throw base.ThrowException(ex);
            }
        }

		public void IssueGRN(long POID, string SupplierDRNo, DateTime DeliveryDate)
		{
			try 
			{
                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandType = System.Data.CommandType.Text;
				
				string SQL=	"UPDATE tblPO SET " + 
								"SupplierDRNo			=	@SupplierDRNo, " +
                                "DeliveryDate			=	@DeliveryDate, " +
								"Status				    =	@Status " +
							"WHERE POID = @POID;";
				  
                cmd.Parameters.AddWithValue("@SupplierDRNo", SupplierDRNo);
                cmd.Parameters.AddWithValue("@DeliveryDate", DeliveryDate.ToString("yyyy-MM-dd HH:mm:ss"));
                cmd.Parameters.AddWithValue("@Status", Convert.ToInt16(POStatus.Posted));
                cmd.Parameters.AddWithValue("@POID", POID);

                cmd.CommandText = SQL;
                base.ExecuteNonQuery(cmd);

				/*******************************************
				 * Update the status of items
				 * ****************************************/
				POItem clsPOItem = new POItem(base.Connection, base.Transaction);
				clsPOItem.Post(POID);

				/*******************************************
				 * Update Vendor Account
				 * ****************************************/
				AddItemToInventory(POID);

                /*******************************************
				 * Update Account Balance
				 * ****************************************/
                UpdateAccounts(POID);

                /*******************************************
				 * Update Required Inventory Days (RID)
				 * ****************************************/
                UpdateRID(POID);
			}
			catch (Exception ex)
			{
				throw base.ThrowException(ex);
			}	
		}

        private void UpdateAccounts(long POID)
        {
            try
            {
                PODetails clsPODetails = Details(POID);
                ChartOfAccounts clsChartOfAccount = new ChartOfAccounts(base.Connection, base.Transaction);

                // update ChartOfAccountIDAPTracking as credit
                clsChartOfAccount.UpdateCredit(clsPODetails.ChartOfAccountIDAPTracking, clsPODetails.SubTotal);

                // update Deposit & APContra
                clsChartOfAccount.UpdateCredit(clsPODetails.ChartOfAccountIDAPContra, clsPODetails.Discount);

                // update Freight & APTracking
                clsChartOfAccount.UpdateCredit(clsPODetails.ChartOfAccountIDAPTracking, clsPODetails.Freight);    
                clsChartOfAccount.UpdateDebit(clsPODetails.ChartOfAccountIDAPFreight, clsPODetails.Freight);

                // update Deposit & APTracking
                clsChartOfAccount.UpdateCredit(clsPODetails.ChartOfAccountIDAPTracking, clsPODetails.Deposit);
                clsChartOfAccount.UpdateDebit(clsPODetails.ChartOfAccountIDAPVDeposit, clsPODetails.Deposit);

                POItem clsPOItem = new POItem(base.Connection, base.Transaction);
                System.Data.DataTable dt = clsPOItem.ListAsDataTable(POID, "POItemID", SortOption.Ascending);
                foreach (System.Data.DataRow dr in dt.Rows)
                { 
                    int iChartOfAccountIDPurchase = Convert.ToInt16(dr["ChartOfAccountIDPurchase"]);
                    int iChartOfAccountIDTaxPurchase = Convert.ToInt16(dr["ChartOfAccountIDTaxPurchase"]);
                    
                    decimal decVAT = Convert.ToDecimal(dr["VAT"]);
                    decimal decVATABLEAmount = Convert.ToDecimal(dr["Amount"]) - decVAT;

                    // update purchase as debit
                    clsChartOfAccount.UpdateDebit(iChartOfAccountIDPurchase, decVATABLEAmount);
                    // update tax as debit
                    clsChartOfAccount.UpdateDebit(iChartOfAccountIDTaxPurchase, decVAT);
                }

            }

            catch (Exception ex)
            {
                throw base.ThrowException(ex);
            }	
        }

        private void UpdateRID(long POID)
        {
            try
            {
                string SQL = "CALL procProductUpdateRIDByPO(@lngPOID)";

                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandText = SQL;

                cmd.Parameters.AddWithValue("@lngPOID", POID);

                base.ExecuteNonQuery(cmd);

            }

            catch (Exception ex)
            {
                throw base.ThrowException(ex);
            }

        }
		private void AddItemToInventory(long POID)
		{

			PODetails clsPODetails = Details(POID);
            ERPConfig clsERPConfig = new ERPConfig(base.Connection, base.Transaction);
			ERPConfigDetails clsERPConfigDetails = clsERPConfig.Details();

			POItem clsPOItem = new POItem(base.Connection, base.Transaction);
            ProductUnit clsProductUnit = new ProductUnit(base.Connection, base.Transaction);
            Products clsProduct = new Products(base.Connection, base.Transaction);
            ProductVariationsMatrix clsProductVariationsMatrix = new ProductVariationsMatrix(base.Connection, base.Transaction);
            ProductPackage clsProductPackage = new ProductPackage(base.Connection, base.Transaction);

			Inventory clsInventory = new Inventory(base.Connection, base.Transaction);
            InventoryDetails clsInventoryDetails;

            ProductPackagePriceHistoryDetails clsProductPackagePriceHistoryDetails;

			//MySqlDataReader myReader = clsPOItem.List(POID, "POItemID", SortOption.Ascending);
            System.Data.DataTable dt = clsPOItem.ListAsDataTable(POID, "POItemID", SortOption.Ascending);

			//while (myReader.Read())
            foreach (System.Data.DataRow dr in dt.Rows)
			{
                long lngProductID = Convert.ToInt64(dr["ProductID"]);
                int intProductUnitID = Convert.ToInt16(dr["ProductUnitID"]);

                decimal decItemQuantity = Convert.ToDecimal(dr["Quantity"]);
                decimal decQuantity = clsProductUnit.GetBaseUnitValue(lngProductID, intProductUnitID, decItemQuantity);

                long lngVariationMatrixID = Convert.ToInt64(dr["VariationMatrixID"]);
                string strMatrixDescription = dr["MatrixDescription"].ToString();
                string strProductCode = dr["ProductCode"].ToString();
                string strProductUnitCode = dr["ProductUnitCode"].ToString();
                decimal decUnitCost = Convert.ToDecimal(dr["UnitCost"]);
                decimal decItemCost = Convert.ToDecimal(dr["Amount"]);
                decimal decSellingPrice = Convert.ToDecimal(dr["SellingPrice"]);
                decimal decVAT = Convert.ToDecimal(dr["VAT"]);
                decimal decEVAT = Convert.ToDecimal(dr["EVAT"]);
                decimal decLocalTax = Convert.ToDecimal(dr["LocalTax"]); 

                /*******************************************
				 * Add in the Price History
				 * ****************************************/
                // Update ProductPackagePriceHistory first to get the history
                clsProductPackagePriceHistoryDetails = new ProductPackagePriceHistoryDetails();
                clsProductPackagePriceHistoryDetails.UID = clsPODetails.PurchaserID;
                clsProductPackagePriceHistoryDetails.PackageID = clsProductPackage.GetPackageID(lngProductID, intProductUnitID);
                clsProductPackagePriceHistoryDetails.ChangeDate = DateTime.Now;
                clsProductPackagePriceHistoryDetails.PurchasePrice = (decItemQuantity * decUnitCost) / decQuantity;
                clsProductPackagePriceHistoryDetails.Price = decSellingPrice;
                clsProductPackagePriceHistoryDetails.VAT = decVAT;
                clsProductPackagePriceHistoryDetails.EVAT = decEVAT;
                clsProductPackagePriceHistoryDetails.LocalTax = decLocalTax;
                clsProductPackagePriceHistoryDetails.Remarks = "Based on PO #: " + clsPODetails.PONo;
                ProductPackagePriceHistory clsProductPackagePriceHistory = new ProductPackagePriceHistory(base.Connection, base.Transaction);
                clsProductPackagePriceHistory.Insert(clsProductPackagePriceHistoryDetails);


				/*******************************************
				 * Add to Inventory
				 * ****************************************/
                //clsProduct.AddQuantity(lngProductID, decQuantity);
                //if (lngVariationMatrixID != 0) { clsProductVariationsMatrix.AddQuantity(lngVariationMatrixID, decQuantity); }
                // July 26, 2011: change the above codes to the following
                clsProduct.AddQuantity(clsPODetails.BranchID, lngProductID, lngVariationMatrixID, decQuantity, Products.getPRODUCT_INVENTORY_MOVEMENT_VALUE(PRODUCT_INVENTORY_MOVEMENT.ADD_PURCHASE) + " @ " + decUnitCost.ToString("#,##0.#0") + "/" + strProductUnitCode, DateTime.Now, clsPODetails.PONo, clsPODetails.PurchaserName);

                /*******************************************
				 * Update Purchasing Information
                 * 
                 * 30May2013 Include variation in the package.
				 * ****************************************/
                int iBaseUnitID = clsProduct.get_BaseUnitID(lngProductID);
                if (iBaseUnitID != intProductUnitID)
                {
                    clsProduct.UpdatePurchasing(lngProductID, lngVariationMatrixID, clsPODetails.SupplierID, iBaseUnitID, (decItemQuantity * decUnitCost) / decQuantity);
                }
                clsProduct.UpdatePurchasing(lngProductID, lngVariationMatrixID, clsPODetails.SupplierID, intProductUnitID, decUnitCost);

				/*******************************************
				 * Add to Inventory Analysis
				 * ****************************************/
				clsInventoryDetails = new InventoryDetails();
                clsInventoryDetails.BranchID = clsPODetails.BranchID;
				clsInventoryDetails.PostingDateFrom = clsERPConfigDetails.PostingDateFrom;
				clsInventoryDetails.PostingDateTo = clsERPConfigDetails.PostingDateTo;
				clsInventoryDetails.PostingDate = clsPODetails.DeliveryDate;
				clsInventoryDetails.ReferenceNo = clsPODetails.PONo;
				clsInventoryDetails.ContactID = clsPODetails.SupplierID;
				clsInventoryDetails.ContactCode = clsPODetails.SupplierCode;
                clsInventoryDetails.ProductID = lngProductID;
                clsInventoryDetails.ProductCode = strProductCode;
                clsInventoryDetails.VariationMatrixID = lngVariationMatrixID;
                clsInventoryDetails.MatrixDescription = strMatrixDescription;
				clsInventoryDetails.PurchaseQuantity = decQuantity;
                clsInventoryDetails.PurchaseCost = decItemCost - decVAT;
                clsInventoryDetails.PurchaseVAT = decItemCost;	// Purchase Cost with VAT

				clsInventory.Insert(clsInventoryDetails);

                /*******************************************
				 * Added Jan 1, 2010 4:20PM
                 * Update Selling Information when PO is posted
				 * ****************************************/
                clsProduct.UpdateSellingPrice(lngProductID, lngVariationMatrixID, clsPODetails.SupplierID, intProductUnitID, Convert.ToDecimal(dr["SellingPrice"]), -1, -1, -1, -1, -1);

                /*******************************************
				 * Added Mar 8, 2010 4:20PM
                 * Update the purchase price history to check who has the lowest price.
				 * ****************************************/
                ProductPurchasePriceHistoryDetails clsProductPurchasePriceHistoryDetails = new ProductPurchasePriceHistoryDetails();
                clsProductPurchasePriceHistoryDetails.ProductID = lngProductID;
                clsProductPurchasePriceHistoryDetails.MatrixID = lngVariationMatrixID;
                clsProductPurchasePriceHistoryDetails.SupplierID = clsPODetails.SupplierID;
                clsProductPurchasePriceHistoryDetails.PurchasePrice = decUnitCost;
                clsProductPurchasePriceHistoryDetails.PurchaseDate = clsPODetails.PODate;
                clsProductPurchasePriceHistoryDetails.Remarks = clsPODetails.PONo;
                clsProductPurchasePriceHistoryDetails.PurchaserName = clsPODetails.PurchaserName;
                ProductPurchasePriceHistory clsProductPurchasePriceHistory = new ProductPurchasePriceHistory(base.Connection, base.Transaction);
                clsProductPurchasePriceHistory.AddToList(clsProductPurchasePriceHistoryDetails);
			}
			//myReader.Close();

		}
		public void Cancel(long POID, DateTime CancelledDate, string Remarks, long CancelledByID)
		{
			try 
			{
				string SQL=	"UPDATE tblPO SET " + 
								"CancelledDate			=	@CancelledDate, " +
								"CancelledRemarks		=	@CancelledRemarks, " +
								"CancelledByID			=	@CancelledByID, " +
								"Status				    =	@Status " +
							"WHERE POID = @POID;";
				  
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

				MySqlParameter prmStatus = new MySqlParameter("@Status",MySqlDbType.Int16);
				prmStatus.Value = POStatus.Cancelled.ToString("d");
				cmd.Parameters.Add(prmStatus);

				MySqlParameter prmPOID = new MySqlParameter("@POID",MySqlDbType.Int64);						
				prmPOID.Value = POID;
				cmd.Parameters.Add(prmPOID);

				base.ExecuteNonQuery(cmd);

				/*******************************************
				 * Update the status of items
				 * ****************************************/
				POItem clsPOItem = new POItem(base.Connection, base.Transaction);
				clsPOItem.Cancel(POID);

			}

			catch (Exception ex)
			{
				throw base.ThrowException(ex);
			}	
		}
		public void GenerateItemsForReorder(Int32 TerminalID, long POID)
		{
			try 
			{
				GetConnection();

                Terminal clsTerminal = new Terminal(base.Connection, base.Transaction);
                TerminalDetails clsTerminalDetails = clsTerminal.Details(TerminalID);

				PODetails clsPODetails = Details(POID);

                POItem clsPOItem = new POItem(base.Connection, base.Transaction);

                ProductInventories clsProductInventories = new ProductInventories(base.Connection, base.Transaction);
                System.Data.DataTable dt = clsProductInventories.ListAsDataTable(BranchID: Constants.BRANCH_ID_MAIN, SupplierID: clsPODetails.SupplierID, ForReorder: 1);

				foreach(System.Data.DataRow dr in dt.Rows)
				{
                    if (decimal.Round(Convert.ToDecimal(dr["ReorderQty"].ToString())) > 0)
                    {
                        POItemDetails clsDetails = new POItemDetails();

                        clsDetails.POID = POID;
                        clsDetails.ProductID = Convert.ToInt64(dr["ProductID"]);
                        clsDetails.ProductCode = dr["ProductCode"].ToString();
                        clsDetails.BarCode = dr["BarCode"].ToString();
                        clsDetails.Description = dr["ProductDesc"].ToString();
                        clsDetails.ProductGroup = dr["ProductGroupCode"].ToString();
                        clsDetails.ProductSubGroup = dr["ProductSubGroupCode"].ToString();
                        clsDetails.ProductUnitID = Convert.ToInt32(dr["UnitID"]);
                        clsDetails.ProductUnitCode = dr["UnitName"].ToString();
                        clsDetails.Quantity = Convert.ToDecimal(dr["ReorderQty"]);
                        clsDetails.UnitCost = Convert.ToDecimal(dr["PurchasePrice"]);
                        clsDetails.Discount = 0;
                        clsDetails.DiscountApplied = 0;
                        clsDetails.DiscountType = DiscountTypes.Percentage;
                        clsDetails.Remarks = "added using auto generation";

                        decimal amount = clsDetails.Quantity * clsDetails.UnitCost;

                        // Added Sep 27, 2010 4:20PM : for selling information
                        clsDetails.SellingPrice = decimal.Parse(dr["Price"].ToString());
                        clsDetails.SellingVAT = clsTerminalDetails.VAT;
                        clsDetails.SellingEVAT = clsTerminalDetails.EVAT;
                        clsDetails.SellingLocalTax = clsTerminalDetails.LocalTax;
                        clsDetails.OldSellingPrice = clsDetails.SellingPrice;

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
                        clsDetails.Amount = amount;

                        clsDetails.VariationMatrixID = Convert.ToInt64(dr["MatrixID"]);
                        clsDetails.MatrixDescription = dr["MatrixDescription"].ToString();
                        clsPOItem.Insert(clsDetails);
                    }
				}
			}

			catch (Exception ex)
			{
				throw base.ThrowException(ex);
			}	
		}

        public void GenerateItemsForReorderByRID(Int32 TerminalID, long POID, long RID, DateTime IDC_StartDate, DateTime IDC_EndDate)
        {
            try
            {
                GetConnection();

                Terminal clsTerminal = new Terminal(base.Connection, base.Transaction);
                TerminalDetails clsTerminalDetails = clsTerminal.Details(TerminalID);

                PODetails clsPODetails = Details(POID);

                Products clsProduct = new Products(base.Connection, base.Transaction);
                // Aug 26, 2011  :Lemu
                // Insert UpdateProductReorderOverStockPerSupplier to update the MinThreshold & MaxThreshold using RID before getting the for stocking
                clsProduct.UpdateProductReorderOverStockPerSupplier(clsPODetails.SupplierID, RID, IDC_StartDate, IDC_EndDate);
                // end

                POItem clsPOItem = new POItem(base.Connection, base.Transaction);

                ProductInventories clsProductInventories = new ProductInventories(base.Connection, base.Transaction);
                System.Data.DataTable dt = clsProductInventories.ListAsDataTable(BranchID: Constants.BRANCH_ID_MAIN, SupplierID: clsPODetails.SupplierID, ForReorder: 1);

                foreach (System.Data.DataRow dr in dt.Rows)
                {
                    if (decimal.Round(Convert.ToDecimal(dr["RIDReorderQty"].ToString())) > 0)
                    {
                        POItemDetails clsDetails = new POItemDetails();

                        clsDetails.POID = POID;
                        clsDetails.ProductID = Convert.ToInt64(dr["ProductID"].ToString());
                        clsDetails.ProductCode = dr["ProductCode"].ToString();
                        clsDetails.BarCode = dr["BarCode"].ToString();
                        clsDetails.Description = dr["ProductDesc"].ToString();
                        clsDetails.ProductGroup = dr["ProductGroupCode"].ToString();
                        clsDetails.ProductSubGroup = dr["ProductSubGroupCode"].ToString();
                        clsDetails.ProductUnitID = Convert.ToInt32(dr["UnitID"]);
                        clsDetails.ProductUnitCode = dr["UnitName"].ToString();
                        clsDetails.RID = Convert.ToInt64(dr["RID"]);
                        clsDetails.Quantity = decimal.Round(Convert.ToDecimal(dr["RIDReorderQty"]));
                        clsDetails.UnitCost = Convert.ToDecimal(dr["PurchasePrice"]);
                        clsDetails.Discount = 0;
                        clsDetails.DiscountApplied = 0;
                        clsDetails.DiscountType = DiscountTypes.Percentage;
                        clsDetails.Remarks = "added using auto generation";

                        decimal amount = clsDetails.Quantity * clsDetails.UnitCost;


                        // Added Sep 27, 2010 4:20PM : for selling information
                        clsDetails.SellingPrice = decimal.Parse(dr["Price"].ToString());
                        clsDetails.SellingVAT = clsTerminalDetails.VAT;
                        clsDetails.SellingEVAT = clsTerminalDetails.EVAT;
                        clsDetails.SellingLocalTax = clsTerminalDetails.LocalTax;
                        clsDetails.OldSellingPrice = clsDetails.SellingPrice;

                        if (Convert.ToDecimal(dr["VAT"]) > 0)
                        {
                            clsDetails.VatableAmount = amount;
                            clsDetails.EVatableAmount = amount;
                            clsDetails.LocalTax = amount;

                            clsDetails.VatableAmount = (clsDetails.VatableAmount) / (1 + (clsTerminalDetails.VAT / 100));
                            clsDetails.EVatableAmount = (clsDetails.EVatableAmount) / (1 + (clsTerminalDetails.VAT / 100));
                            clsDetails.LocalTax = (clsDetails.LocalTax) / (1 + (clsTerminalDetails.LocalTax / 100));
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
                        clsDetails.Amount = amount;

                        clsDetails.VariationMatrixID = Convert.ToInt64(dr["MatrixID"]);
                        clsDetails.MatrixDescription = dr["MatrixDescription"].ToString();
                        clsPOItem.Insert(clsDetails);
                    }
                }
            }

            catch (Exception ex)
            {
                throw base.ThrowException(ex);
            }
        }

        /**********************************
         * Lemuel E. Aceron
         * July 30, 2008 17:21
         * Added for Payment
         **********************************/
        public bool UpdatePaymentStatus(POPaymentStatus paymentStatus, string IDs)
        {
            try
            {
                string SQL = "UPDATE tblPO SET PaymentStatus = @PaymentStatus WHERE POID IN (" + IDs + ");";

                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandText = SQL;

                MySqlParameter prmPaymentStatus = new MySqlParameter("@PaymentStatus",MySqlDbType.Int16);
                prmPaymentStatus.Value = paymentStatus.ToString("d");
                cmd.Parameters.Add(prmPaymentStatus);

                base.ExecuteNonQuery(cmd);

                return true;
            }

            catch (Exception ex)
            {
                throw base.ThrowException(ex);
            }
        }
        public bool UpdatePayment(long POID, decimal PaidAmount, POPaymentStatus paymentStatus)
        {
            try
            {
                string SQL = "UPDATE tblPO SET " +
                                "PaidAmount     = PaidAmount + @PaidAmount, " +
                                "UnpaidAmount   = UnpaidAmount - @PaidAmount, " +
                                "PaymentStatus  = @PaymentStatus " +
                             "WHERE POID = @POID;";

                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandText = SQL;

                MySqlParameter prmPaidAmount = new MySqlParameter("@PaidAmount",MySqlDbType.Decimal);
                prmPaidAmount.Value = PaidAmount;
                cmd.Parameters.Add(prmPaidAmount);

                MySqlParameter prmPaymentStatus = new MySqlParameter("@PaymentStatus",MySqlDbType.Int16);
                prmPaymentStatus.Value = paymentStatus.ToString("d");
                cmd.Parameters.Add(prmPaymentStatus);

                MySqlParameter prmPOID = new MySqlParameter("@POID",MySqlDbType.Int64);
                prmPOID.Value = POID;
                cmd.Parameters.Add(prmPOID);

                base.ExecuteNonQuery(cmd);

                return true;
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
				string SQL=	"DELETE FROM tblPO WHERE POID IN (" + IDs + ");";
				  
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
            string stSQL = "SELECT " +
                                "POID, " +
                                "PONo, " +
                                "PODate, " +
                                "SupplierID, " +
                                "SupplierCode, " +
                                "SupplierContact, " +
                                "SupplierAddress, " +
                                "SupplierTelephoneNo, " +
                                "SupplierModeOfTerms, " +
                                "SupplierTerms, " +
                                "SupplierTINNo, " +
                                "SupplierLTONo, " +
                                "RequiredDeliveryDate, " +
                                "RID, " +
                                "po.BranchID, " +
                                "BranchCode, " +
                                "BranchName, " +
                                "brnch.Address BranchAddress, " +
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
                                "Status, " +
                                "IsVatInclusive, " +
                                "po.Remarks, " +
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
                                "TotalItemDiscount, " +
                                "po.IncludeIneSales " +
                            "FROM tblPO po INNER JOIN tblBranch brnch ON po.BranchID = brnch.BranchID ";
            return stSQL;
        }

		#region Details

		public PODetails Details(long POID)
		{
			try
			{
                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandType = System.Data.CommandType.Text;

				string SQL=	SQLSelect() + "WHERE POID = @POID;";
				  
				cmd.Parameters.AddWithValue("@POID", POID);

                cmd.CommandText = SQL;
                cmd.CommandText = SQL;
                string strDataTableName = "tbl" + this.GetType().FullName.Split(new Char[] { '.' })[this.GetType().FullName.Split(new Char[] { '.' }).Length - 1]; System.Data.DataTable dt = new System.Data.DataTable(strDataTableName);
                base.MySqlDataAdapterFill(cmd, dt);

                PODetails Details = new PODetails();
                foreach (System.Data.DataRow dr in dt.Rows)
                {
					Details.POID = POID;
					Details.PONo = "" + dr["PONo"].ToString();
                    Details.PODate = DateTime.Parse(dr["PODate"].ToString());
					Details.SupplierID = Int64.Parse(dr["SupplierID"].ToString());
					Details.SupplierCode = "" + dr["SupplierCode"].ToString();
					Details.SupplierContact = "" + dr["SupplierContact"].ToString();
					Details.SupplierAddress = "" + dr["SupplierAddress"].ToString();
					Details.SupplierTelephoneNo = "" + dr["SupplierTelephoneNo"].ToString();
                    Details.SupplierTINNo = "" + dr["SupplierTINNo"].ToString();
                    Details.SupplierLTONo = "" + dr["SupplierLTONo"].ToString();
					Details.SupplierModeOfTerms = Int16.Parse(dr["SupplierModeofTerms"].ToString());
					Details.SupplierTerms = Int16.Parse(dr["SupplierTerms"].ToString());
					Details.RequiredDeliveryDate = DateTime.Parse(dr["RequiredDeliveryDate"].ToString());
                    Details.RID = Int64.Parse(dr["RID"].ToString());
					Details.BranchID = Int16.Parse(dr["BranchID"].ToString());
					Details.BranchCode = "" + dr["BranchCode"].ToString();
                    Details.BranchName = "" + dr["BranchName"].ToString();
                    Details.BranchAddress = "" + dr["BranchAddress"].ToString();
					Details.PurchaserID = Int64.Parse(dr["PurchaserID"].ToString());
                    Details.PurchaserName = dr["PurchaserName"].ToString();
					Details.SubTotal = Decimal.Parse(dr["SubTotal"].ToString());
					Details.Discount = Decimal.Parse(dr["Discount"].ToString());
                    Details.DiscountApplied = Decimal.Parse(dr["DiscountApplied"].ToString());
                    Details.DiscountType = (DiscountTypes)Enum.Parse(typeof(DiscountTypes), dr["DiscountType"].ToString());
                    Details.Discount2 = Decimal.Parse(dr["Discount2"].ToString());
                    Details.Discount2Applied = Decimal.Parse(dr["Discount2Applied"].ToString());
                    Details.Discount2Type = (DiscountTypes)Enum.Parse(typeof(DiscountTypes), dr["Discount2Type"].ToString());
                    Details.Discount3 = Decimal.Parse(dr["Discount3"].ToString());
                    Details.Discount3Applied = Decimal.Parse(dr["Discount3Applied"].ToString());
                    Details.Discount3Type = (DiscountTypes)Enum.Parse(typeof(DiscountTypes), dr["Discount3Type"].ToString());
					Details.VAT = Decimal.Parse(dr["VAT"].ToString());
					Details.VatableAmount = Decimal.Parse(dr["VatableAmount"].ToString());
                    Details.EVAT = Decimal.Parse(dr["EVAT"].ToString());
                    Details.EVatableAmount = Decimal.Parse(dr["EVatableAmount"].ToString());
                    Details.LocalTax = Decimal.Parse(dr["LocalTax"].ToString());
                    Details.Freight = Decimal.Parse(dr["Freight"].ToString());
                    Details.Deposit = Decimal.Parse(dr["Deposit"].ToString());
                    Details.PaidAmount = Decimal.Parse(dr["PaidAmount"].ToString());
                    Details.UnpaidAmount = Decimal.Parse(dr["UnpaidAmount"].ToString());
                    Details.Status = (POStatus)Enum.Parse(typeof(POStatus), dr["Status"].ToString());
                    Details.IsVatInclusive = bool.Parse(dr["IsVatInclusive"].ToString());
                    Details.TotalItemDiscount = Decimal.Parse(dr["TotalItemDiscount"].ToString());
					Details.Remarks = dr["Remarks"].ToString();
					Details.SupplierDRNo = dr["SupplierDRNo"].ToString();
					Details.DeliveryDate = DateTime.Parse(dr["DeliveryDate"].ToString());
                    Details.ChartOfAccountIDAPTracking = Int16.Parse(dr["ChartOfAccountIDAPTracking"].ToString());
                    Details.ChartOfAccountIDAPFreight = Int16.Parse(dr["ChartOfAccountIDAPFreight"].ToString());
                    Details.ChartOfAccountIDAPVDeposit = Int16.Parse(dr["ChartOfAccountIDAPVDeposit"].ToString());
                    Details.ChartOfAccountIDAPContra = Int16.Parse(dr["ChartOfAccountIDAPContra"].ToString());
                    Details.ChartOfAccountIDAPLatePayment = Int16.Parse(dr["ChartOfAccountIDAPLatePayment"].ToString());
                    Details.IncludeIneSales = bool.Parse(dr["IncludeIneSales"].ToString());
				}

				return Details;
			}
			catch (Exception ex)
			{
				throw base.ThrowException(ex);
			}	
		}

		#endregion

		#region Streams

        //public System.Data.DataTable ListAsDataTable(POStatus postatus, string SortField, SortOption SortOrder)
        //{
        //    if (SortField == string.Empty || SortField == null) SortField = "POID";

        //    string SQL = SQLSelect() + "WHERE Status = @Status ";

        //    SQL += "ORDER BY " + SortField;

        //    if (SortOrder == SortOption.Ascending)
        //        SQL += " ASC";
        //    else
        //        SQL += " DESC";

        //    MySqlCommand cmd = new MySqlCommand();
        //    cmd.CommandType = System.Data.CommandType.Text;
        //    cmd.CommandText = SQL;

        //    cmd.Parameters.AddWithValue("@Status", postatus.ToString("d"));

        //    string strDataTableName = "tbl" + this.GetType().FullName.Split(new Char[] { '.' })[this.GetType().FullName.Split(new Char[] { '.' }).Length - 1]; System.Data.DataTable dt = new System.Data.DataTable(strDataTableName);
        //    base.MySqlDataAdapterFill(cmd, dt);

        //    return dt;
        //}
        public System.Data.DataTable ListAsDataTable(POStatus postatus = POStatus.All, PODetails searchKey = new PODetails(), DateTime? OrderStartDate = null, DateTime? OrderEndDate = null, DateTime? PostingStartDate = null, DateTime? PostingEndDate = null, string SortField = "POID", SortOption SortOrder = SortOption.Ascending, Int32 limit = 0, Int64 SupplierID = 0, Int64 POID = 0, eSalesFilter clseSalesFilter = new eSalesFilter())
        {
            try
            {
                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandType = System.Data.CommandType.Text;

                string SQL = SQLSelect() + "WHERE 1=1 ";

                if (postatus != POStatus.All)
                {
                    SQL += "AND Status = @Status ";
                    cmd.Parameters.AddWithValue("@Status", postatus.ToString("d"));
                }

                if (POID != 0)
                {
                    SQL += "AND POID = @POID ";
                    cmd.Parameters.AddWithValue("@POID", POID);
                }

                if (SupplierID != 0)
                {
                    SQL += "AND SupplierID >= @SupplierID ";
                    cmd.Parameters.AddWithValue("@SupplierID", SupplierID);
                }

                if ((OrderStartDate.GetValueOrDefault() == DateTime.MinValue ? Constants.C_DATE_MIN_VALUE : OrderStartDate) != Constants.C_DATE_MIN_VALUE)
                {
                    SQL += "AND PODate >= @OrderStartDate ";
                    cmd.Parameters.AddWithValue("@OrderStartDate", OrderStartDate.Value.ToString("yyyy-MM-dd HH:mm:ss"));
                }

                if ((OrderEndDate.GetValueOrDefault() == DateTime.MinValue ? Constants.C_DATE_MIN_VALUE : OrderEndDate) != Constants.C_DATE_MIN_VALUE)
                {
                    SQL += "AND PODate <= @OrderEndDate ";
                    cmd.Parameters.AddWithValue("@OrderEndDate", OrderEndDate.Value.ToString("yyyy-MM-dd HH:mm:ss"));
                }

                if ((PostingStartDate.GetValueOrDefault() == DateTime.MinValue ? Constants.C_DATE_MIN_VALUE : PostingStartDate) != Constants.C_DATE_MIN_VALUE)
                {
                    SQL += "AND PODate >= @PostingStartDate ";
                    cmd.Parameters.AddWithValue("@PostingStartDate", PostingStartDate.Value.ToString("yyyy-MM-dd HH:mm:ss"));
                }

                if ((PostingEndDate.GetValueOrDefault() == DateTime.MinValue ? Constants.C_DATE_MIN_VALUE : PostingEndDate) != Constants.C_DATE_MIN_VALUE)
                {
                    SQL += "AND PODate <= @PostingEndDate ";
                    cmd.Parameters.AddWithValue("@PostingEndDate", PostingEndDate.Value.ToString("yyyy-MM-dd HH:mm:ss"));
                }

                if (clseSalesFilter.FilterIncludeIneSales)
                {
                    SQL += "AND po.IncludeIneSales = @IncludeIneSales ";
                    cmd.Parameters.AddWithValue("@IncludeIneSales", clseSalesFilter.IncludeIneSales);
                }

                SQL += "ORDER BY " + (!string.IsNullOrEmpty(SortField) ? SortField : "POID") + " ";
                SQL += SortOrder == SortOption.Ascending ? "ASC " : "DESC ";
                SQL += limit == 0 ? "" : "LIMIT " + limit.ToString() + " ";

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

        //public System.Data.DataTable ListAsDataTable(string SortField, SortOption SortOrder)
        //{
        //    if (SortField == string.Empty || SortField == null) SortField = "POID";

        //    string SQL = SQLSelect() + "ORDER BY " + SortField;

        //    if (SortOrder == SortOption.Ascending)
        //        SQL += " ASC";
        //    else
        //        SQL += " DESC";

        //    MySqlCommand cmd = new MySqlCommand();
        //    cmd.CommandType = System.Data.CommandType.Text;
        //    cmd.CommandText = SQL;

        //    string strDataTableName = "tbl" + this.GetType().FullName.Split(new Char[] { '.' })[this.GetType().FullName.Split(new Char[] { '.' }).Length - 1]; System.Data.DataTable dt = new System.Data.DataTable(strDataTableName);
        //    base.MySqlDataAdapterFill(cmd, dt);

        //    return dt;
        //}

        public MySqlDataReader ListForPayment(long SupplierID, string SortField, SortOption SortOrder)
        {
            try
            {
                if (SortField == string.Empty || SortField == null) SortField = "POID";

                string SQL = SQLSelect() + "WHERE PaymentStatus <> @FullyPaidPaymentStatus AND Status =@PostedStatus AND SupplierID = @SupplierID ORDER BY " + SortField;

                if (SortOrder == SortOption.Ascending)
                    SQL += " ASC";
                else
                    SQL += " DESC";

                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandText = SQL;

                MySqlParameter prmFullyPaidPaymentStatus = new MySqlParameter("@FullyPaidPaymentStatus", MySqlDbType.Int16);
                prmFullyPaidPaymentStatus.Value = POPaymentStatus.FullyPaid.ToString("d");
                cmd.Parameters.Add(prmFullyPaidPaymentStatus);

                MySqlParameter prmPostedStatus = new MySqlParameter("@PostedStatus", MySqlDbType.Int16);
                prmPostedStatus.Value = POStatus.Posted.ToString("d");
                cmd.Parameters.Add(prmPostedStatus);

                MySqlParameter prmSupplierID = new MySqlParameter("@SupplierID", MySqlDbType.Int64);
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
        public System.Data.DataTable SearchAsDataTable(POStatus postatus, string SearchKey, string SortField, SortOption SortOrder)
        {
            try
            {
                if (SortField == string.Empty || SortField == null) SortField = "POID";

                string SQL = SQLSelect() + "WHERE Status = @Status AND (PONo LIKE @SearchKey or PODate LIKE @SearchKey or SupplierCode LIKE @SearchKey " +
                                        "or SupplierContact LIKE @SearchKey or BranchCode LIKE @SearchKey or RequiredDeliveryDate LIKE @SearchKey or po.Remarks LIKE @SearchKey) " +
                            "ORDER BY " + SortField;

                if (SortOrder == SortOption.Ascending)
                    SQL += " ASC";
                else
                    SQL += " DESC";

                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandText = SQL;

                MySqlParameter prmStatus = new MySqlParameter("@Status",MySqlDbType.Int16);
                prmStatus.Value = postatus.ToString("d");
                cmd.Parameters.Add(prmStatus);

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
        public System.Data.DataTable SearchAsDataTable(POStatus postatus, DateTime OrderStartDate, DateTime OrderEndDate, DateTime PostingStartDate, DateTime PostingEndDate, string SearchKey, string SortField, SortOption SortOrder, Int32 limit=0, eSalesFilter clseSalesFilter = new eSalesFilter())
        {
            try
            {
                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandType = System.Data.CommandType.Text;
                
                string SQL = SQLSelect() + "WHERE Status = @Status AND (PONo LIKE @SearchKey or PODate LIKE @SearchKey or SupplierCode LIKE @SearchKey " +
                                        "or SupplierContact LIKE @SearchKey or BranchCode LIKE @SearchKey or RequiredDeliveryDate LIKE @SearchKey or po.Remarks LIKE @SearchKey) ";

                if (OrderStartDate != DateTime.MinValue) SQL += "AND PODate >= @OrderStartDate ";
                if (OrderEndDate != DateTime.MinValue) SQL += "AND PODate <= @OrderEndDate ";
                if (PostingStartDate != DateTime.MinValue) SQL += "AND DeliveryDate >= @PostingStartDate ";
                if (PostingEndDate != DateTime.MinValue) SQL += "AND DeliveryDate <= @PostingEndDate ";

                
                if (clseSalesFilter.FilterIncludeIneSales)
                {
                    SQL += "AND po.IncludeIneSales = @IncludeIneSales ";
                    cmd.Parameters.AddWithValue("@IncludeIneSales", clseSalesFilter.IncludeIneSales);
                }

                SQL += "ORDER BY " + (!string.IsNullOrEmpty(SortField) ? SortField : "POID") + " ";
                SQL += SortOrder == SortOption.Ascending ? "ASC " : "DESC ";
                SQL += limit == 0 ? "" : "LIMIT " + limit.ToString() + " ";

                cmd.Parameters.AddWithValue("@Status", postatus.ToString("d"));
                cmd.Parameters.AddWithValue("@SearchKey", "%" + SearchKey + "%");

                if (OrderStartDate != DateTime.MinValue) cmd.Parameters.AddWithValue("@OrderStartDate", OrderStartDate.ToString("yyyy-MM-dd HH:mm:ss"));
                if (OrderEndDate != DateTime.MinValue) cmd.Parameters.AddWithValue("@OrderEndDate", OrderEndDate.ToString("yyyy-MM-dd HH:mm:ss"));
                if (PostingStartDate != DateTime.MinValue) cmd.Parameters.AddWithValue("@PostingStartDate", PostingStartDate.ToString("yyyy-MM-dd HH:mm:ss"));
                if (PostingEndDate != DateTime.MinValue) cmd.Parameters.AddWithValue("@PostingEndDate", PostingEndDate.ToString("yyyy-MM-dd HH:mm:ss"));


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
		
		#endregion

		#region Public Modifiers

		public string LastTransactionNo()
		{
			try
			{
				string stRetValue = String.Empty;
				
				ERPConfig clsERPConfig = new ERPConfig(base.Connection, base.Transaction);
				stRetValue = clsERPConfig.get_LastPONo();

				return stRetValue;
			}
			catch (Exception ex)
			{
				throw base.ThrowException(ex);
			}	
		}
        public void SynchronizeAmount(long POID)
        {
            try
            {
                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandType = System.Data.CommandType.Text;

                string SQL = "CALL procPOSynchronizeAmount(@POID);";

                cmd.Parameters.AddWithValue("@POID", POID);

                cmd.CommandText = SQL;
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


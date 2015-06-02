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

	#region WBranchTransferDetails

	public struct WBranchTransferDetails
	{
		public long WBranchTransferID;
		public string WBranchTransferNo;
		public DateTime WBranchTransferDate;
		public DateTime RequiredDeliveryDate;
		public int BranchIDFrom;
        public string BranchCodeFrom;
        public string BranchNameFrom;
        public string BranchAddressFrom;
        public int BranchIDTo;
        public string BranchCodeTo;
        public string BranchNameTo;
        public string BranchAddressTo;
		public long TransferrerID;
        public string TransferrerName;
        public string RequestedBy;
		public decimal SubTotal;
		public decimal Discount;
        public decimal DiscountApplied;
        public DiscountTypes DiscountType;
		public decimal VAT;
		public decimal VatableAmount;
        public decimal EVAT;
        public decimal EVatableAmount;
        public decimal LocalTax;
		public WBranchTransferStatus Status;
		public string Remarks;
		public string ReceivedBy;
		public DateTime DeliveryDate;
        public DateTime CancelledDate;
        public string CancelledRemarks;
        public long CancelledByID;
        public decimal UnpaidAmount;
        public decimal PaidAmount;
        public WBranchTransferPaymentStatus PaymentStatus;
        public decimal Freight;
        public decimal Deposit;
        public decimal TotalItemDiscount;

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
	public class WBranchTransfer : POSConnection
	{
		#region Constructors and Destructors

		public WBranchTransfer()
            : base(null, null)
        {
        }

        public WBranchTransfer(MySqlConnection Connection, MySqlTransaction Transaction) 
            : base(Connection, Transaction)
		{

		}

        #endregion

		#region Insert and Update

		public long Insert(WBranchTransferDetails Details)
		{
			try 
			{
                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandType = System.Data.CommandType.Text;

				string SQL = "INSERT INTO tblWBranchTransfer (" +
								            "WBranchTransferNo, " +
								            "WBranchTransferDate, " +
								            "RequiredDeliveryDate, " +
								            "BranchIDFrom, " +
                                            "BranchIDTo, " +
								            "TransferrerID, " +
                                            "TransferrerName, " +
                                            "RequestedBy, " +
								            "Status, " +
								            "Remarks" +
							            ") VALUES (" +
                                            "@WBranchTransferNo, " +
                                            "@WBranchTransferDate, " +
                                            "@RequiredDeliveryDate, " +
                                            "@BranchIDFrom, " +
                                            "@BranchIDTo, " +
                                            "@TransferrerID, " +
                                            "@TransferrerName, " +
                                            "@RequestedBy, " +
                                            "@Status, " +
                                            "@Remarks" +
							            ");";
				  
                cmd.Parameters.AddWithValue("@WBranchTransferNo", Details.WBranchTransferNo);
                cmd.Parameters.AddWithValue("@WBranchTransferDate", Details.WBranchTransferDate.ToString("yyyy-MM-dd HH:mm:ss"));
                cmd.Parameters.AddWithValue("@RequiredDeliveryDate", Details.RequiredDeliveryDate.ToString("yyyy-MM-dd HH:mm:ss"));
                cmd.Parameters.AddWithValue("@BranchIDFrom", Details.BranchIDFrom);
                cmd.Parameters.AddWithValue("@BranchIDTo", Details.BranchIDTo);
                cmd.Parameters.AddWithValue("@TransferrerID", Details.TransferrerID);
                cmd.Parameters.AddWithValue("@TransferrerName", Details.TransferrerName);
                cmd.Parameters.AddWithValue("@RequestedBy", Details.RequestedBy);
                cmd.Parameters.AddWithValue("@Status", Details.Status.ToString("d"));
                cmd.Parameters.AddWithValue("@Remarks", Details.Remarks);

                cmd.CommandText = SQL;
				base.ExecuteNonQuery(cmd);

                return Int64.Parse(base.getLAST_INSERT_ID(this));
			}
			catch (Exception ex)
			{
				throw base.ThrowException(ex);
			}	
		}
		public void Update(WBranchTransferDetails Details)
		{
			try 
			{
                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandType = System.Data.CommandType.Text;

                string SQL=	"UPDATE tblWBranchTransfer SET " +
                                "WBranchTransferNo			=	@WBranchTransferNo, " +
                                "WBranchTransferDate			=	@WBranchTransferDate, " +
                                "BranchIDFrom				=	@BranchIDFrom, " +
                                "BranchIDTo 				=	@BranchIDTo, " +
                                "TransferrerID			    =	@TransferrerID, " +
                                "TransferrerName            =   @TransferrerName, " +
                                "RequestedBy                =   @RequestedBy, " +
                                "Remarks                    =   @Remarks " +
							"WHERE WBranchTransferID = @WBranchTransferID;";

                cmd.Parameters.AddWithValue("@WBranchTransferNo", Details.WBranchTransferNo);
                cmd.Parameters.AddWithValue("@WBranchTransferDate", Details.WBranchTransferDate.ToString("yyyy-MM-dd HH:mm:ss"));
                cmd.Parameters.AddWithValue("@RequiredDeliveryDate", Details.RequiredDeliveryDate.ToString("yyyy-MM-dd HH:mm:ss"));
                cmd.Parameters.AddWithValue("@BranchIDFrom", Details.BranchIDFrom);
                cmd.Parameters.AddWithValue("@BranchIDTo", Details.BranchIDTo);
                cmd.Parameters.AddWithValue("@TransferrerID", Details.TransferrerID);
                cmd.Parameters.AddWithValue("@TransferrerName", Details.TransferrerName);
                cmd.Parameters.AddWithValue("@RequestedBy", Details.RequestedBy);
                cmd.Parameters.AddWithValue("@Remarks", Details.Remarks);
                cmd.Parameters.AddWithValue("@WBranchTransferID", Details.WBranchTransferID);

				cmd.CommandText = SQL;
				base.ExecuteNonQuery(cmd);
			}
			catch (Exception ex)
			{
				throw base.ThrowException(ex);
			}	
		}

        public void UpdateDiscount(long WBranchTransferID, decimal DiscountApplied, DiscountTypes DiscountType)
        {
            try
            {
                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandType = System.Data.CommandType.Text;

                string SQL = "UPDATE tblWBranchTransfer SET " +
                                "DiscountApplied        =   @DiscountApplied, " +
                                "DiscountType           =   @DiscountType " +
                            "WHERE WBranchTransferID = @WBranchTransferID;";

                cmd.Parameters.AddWithValue("@DiscountApplied", DiscountApplied);
                cmd.Parameters.AddWithValue("@DiscountType", DiscountType.ToString("d"));
                cmd.Parameters.AddWithValue("@WBranchTransferID", WBranchTransferID);

                cmd.CommandText = SQL;
                base.ExecuteNonQuery(cmd);
            }
            catch (Exception ex)
            {
                throw base.ThrowException(ex);
            }
        }
        public void UpdateDiscountFreightDeposit(long WBranchTransferID, decimal DiscountApplied, DiscountTypes DiscountType)
        {
            try
            {
                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandType = System.Data.CommandType.Text;

                string SQL = "UPDATE tblWBranchTransfer SET " +
                                "DiscountApplied        =   @DiscountApplied, " +
                                "DiscountType           =   @DiscountType " +
                            "WHERE WBranchTransferID = @WBranchTransferID;";

                cmd.Parameters.AddWithValue("@DiscountApplied", DiscountApplied);
                cmd.Parameters.AddWithValue("@DiscountType", DiscountType.ToString("d"));
                cmd.Parameters.AddWithValue("@WBranchTransferID", WBranchTransferID);

                cmd.CommandText = SQL;
                base.ExecuteNonQuery(cmd);
            }

            catch (Exception ex)
            {
                throw base.ThrowException(ex);
            }
        }
        public void UpdateFreight(long WBranchTransferID, decimal Freight)
        {
            try
            {
                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandType = System.Data.CommandType.Text;

                string SQL = "UPDATE tblWBranchTransfer SET " +
                                "Freight           =   @Freight " +
                            "WHERE WBranchTransferID = @WBranchTransferID;";

                cmd.Parameters.AddWithValue("@Freight", Freight);
                cmd.Parameters.AddWithValue("@WBranchTransferID", WBranchTransferID);

                cmd.CommandText = SQL;
                base.ExecuteNonQuery(cmd);
            }
            catch (Exception ex)
            {
                throw base.ThrowException(ex);
            }
        }
        public void UpdateDeposit(long WBranchTransferID, decimal Deposit)
        {
            try
            {
                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandType = System.Data.CommandType.Text;

                string SQL = "UPDATE tblWBranchTransfer SET " +
                                "Deposit           =   @Deposit " +
                            "WHERE WBranchTransferID = @WBranchTransferID;";

                cmd.Parameters.AddWithValue("@Deposit", Deposit);
                cmd.Parameters.AddWithValue("@WBranchTransferID", WBranchTransferID);

                cmd.CommandText = SQL;
                base.ExecuteNonQuery(cmd);
            }
            catch (Exception ex)
            {
                throw base.ThrowException(ex);
            }
        }

        public void IssueGRN(Int64 WBranchTransferID, string ReceivedBy, DateTime DeliveryDate)
		{
			try 
			{
                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandType = System.Data.CommandType.Text;

				string SQL=	"UPDATE tblWBranchTransfer SET " + 
								"ReceivedBy			    =	@ReceivedBy, " +
								"DeliveryDate			=	@DeliveryDate, " +
								"Status				    =	@Status " +
							"WHERE WBranchTransferID = @WBranchTransferID;";
				  
                cmd.Parameters.AddWithValue("@ReceivedBy", ReceivedBy);
                cmd.Parameters.AddWithValue("@DeliveryDate", DeliveryDate.ToString("yyyy-MM-dd HH:mm:ss"));
                cmd.Parameters.AddWithValue("@Status", WBranchTransferStatus.Posted.ToString("d"));
                cmd.Parameters.AddWithValue("@WBranchTransferID", WBranchTransferID);

                cmd.CommandText = SQL;
				base.ExecuteNonQuery(cmd);

				/*******************************************
				 * Update the status of items
				 * ****************************************/
				WBranchTransferItem clsWBranchTransferItem = new WBranchTransferItem(base.Connection, base.Transaction);
				clsWBranchTransferItem.Post(WBranchTransferID);

				/*******************************************
				 * Update Vendor Account
				 * ****************************************/
				AddItemToInventory(WBranchTransferID);

                /*******************************************
				 * Update Account Balance
				 * ****************************************/
                UpdateAccounts(WBranchTransferID);

                /*******************************************
				 * Update Required Inventory Days (RID)
				 * ****************************************/
                UpdateRID(WBranchTransferID);
			}
			catch (Exception ex)
			{
				throw base.ThrowException(ex);
			}	
		}

        public void SubmitForApproval(Int64 WBranchTransferID, string SubmittedBy)
        {
            try
            {
                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandType = System.Data.CommandType.Text;

                string SQL = "UPDATE tblWBranchTransfer SET " +
                                "SubmittedBy			=	@SubmittedBy, " +
                                "SubmissionDate			=	@SubmissionDate, " +
                                "Status				    =	@Status " +
                            "WHERE WBranchTransferID = @WBranchTransferID;";

                cmd.Parameters.AddWithValue("@SubmittedBy", SubmittedBy);
                cmd.Parameters.AddWithValue("@SubmissionDate", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                cmd.Parameters.AddWithValue("@Status", WBranchTransferStatus.ForApproval.ToString("d"));
                cmd.Parameters.AddWithValue("@WBranchTransferID", WBranchTransferID);

                cmd.CommandText = SQL;
                base.ExecuteNonQuery(cmd);
            }
            catch (Exception ex)
            {
                throw base.ThrowException(ex);
            }
        }

        private void UpdateAccounts(long WBranchTransferID)
        {
            try
            {
                //WBranchTransferDetails clsWBranchTransferDetails = Details(WBranchTransferID);
                //ChartOfAccount clsChartOfAccount = new ChartOfAccount(base.Connection, base.Transaction);

                //// update ChartOfAccountIDAPTracking as credit
                //clsChartOfAccount.UpdateCredit(clsWBranchTransferDetails.ChartOfAccountIDAPTracking, clsWBranchTransferDetails.SubTotal);

                //// update Deposit & APContra
                //clsChartOfAccount.UpdateCredit(clsWBranchTransferDetails.ChartOfAccountIDAPContra, clsWBranchTransferDetails.Discount);

                //// update Freight & APTracking
                //clsChartOfAccount.UpdateCredit(clsWBranchTransferDetails.ChartOfAccountIDAPTracking, clsWBranchTransferDetails.Freight);    
                //clsChartOfAccount.UpdateDebit(clsWBranchTransferDetails.ChartOfAccountIDAPFreight, clsWBranchTransferDetails.Freight);

                //// update Deposit & APTracking
                //clsChartOfAccount.UpdateCredit(clsWBranchTransferDetails.ChartOfAccountIDAPTracking, clsWBranchTransferDetails.Deposit);
                //clsChartOfAccount.UpdateDebit(clsWBranchTransferDetails.ChartOfAccountIDAPVDeposit, clsWBranchTransferDetails.Deposit);

                //WBranchTransferItem clsWBranchTransferItem = new WBranchTransferItem(base.Connection, base.Transaction);
                //MySqlDataReader myReader = clsWBranchTransferItem.List(WBranchTransferID, "WBranchTransferItemID", SortOption.Ascending);
                //while (myReader.Read())
                //{ 
                //    int iChartOfAccountIDPurchase = myReader.GetInt16("ChartOfAccountIDPurchase");
                //    int iChartOfAccountIDTaxPurchase = myReader.GetInt16("ChartOfAccountIDTaxPurchase");
                    
                //    decimal decVAT = myReader.GetDecimal("VAT");
                //    decimal decVATABLEAmount = myReader.GetDecimal("Amount") - decVAT;

                //    // update purchase as debit
                //    clsChartOfAccount.UpdateDebit(iChartOfAccountIDPurchase, decVATABLEAmount);
                //    // update tax as debit
                //    clsChartOfAccount.UpdateDebit(iChartOfAccountIDTaxPurchase, decVAT);
                    
                //}
                //myReader.Close();

            }
            catch (Exception ex)
            {
                throw base.ThrowException(ex);
            }	
        }

        private void UpdateRID(long WBranchTransferID)
        {
            try
            {
                //string SQL = "CALL procProductUpdateRIDByWBranchTransfer(@lngWBranchTransferID)";

                //

                //MySqlCommand cmd = new MySqlCommand();
                //
                //
                //cmd.CommandType = System.Data.CommandType.Text;
                //cmd.CommandText = SQL;

                //cmd.Parameters.AddWithValue("@lngWBranchTransferID", WBranchTransferID);

                //base.ExecuteNonQuery(cmd);

            }
            catch (Exception ex)
            {
                throw base.ThrowException(ex);
            }

        }
		private void AddItemToInventory(long WBranchTransferID)
		{

			WBranchTransferDetails clsWBranchTransferDetails = Details(WBranchTransferID);
            ERPConfig clsERPConfig = new ERPConfig(base.Connection, base.Transaction);
			ERPConfigDetails clsERPConfigDetails = clsERPConfig.Details();

			WBranchTransferItem clsWBranchTransferItem = new WBranchTransferItem(base.Connection, base.Transaction);
            ProductUnit clsProductUnit = new ProductUnit(base.Connection, base.Transaction);
            Products clsProduct = new Products(base.Connection, base.Transaction);

            Inventory clsInventory = new Inventory(base.Connection, base.Transaction);
            InventoryDetails clsInventoryDetails;

			System.Data.DataTable dt = clsWBranchTransferItem.ListAsDataTable(WBranchTransferID, "WBranchTransferItemID", SortOption.Ascending);

            foreach (System.Data.DataRow dr in dt.Rows)
			{
				long lngProductID = Int64.Parse(dr["ProductID"].ToString());
				int intProductUnitID = Int16.Parse(dr["ProductUnitID"].ToString());

				decimal decItemQuantity = decimal.Parse(dr["Quantity"].ToString());
                decimal decQuantity = clsProductUnit.GetBaseUnitValue(lngProductID, intProductUnitID, decItemQuantity);
				
				long lngVariationMatrixID = Int64.Parse(dr["VariationMatrixID"].ToString());
				string strMatrixDescription = "" + dr["MatrixDescription"].ToString();
				string strProductCode = "" + dr["ProductCode"].ToString();
                string strProductUnitCode = "" + dr["ProductUnitCode"].ToString();
				decimal decUnitCost = decimal.Parse(dr["UnitCost"].ToString());
				decimal decItemCost = decimal.Parse(dr["Amount"].ToString());
                decimal decVAT = decimal.Parse(dr["VAT"].ToString());

				/*******************************************
				 * Subtract BranchIDFrom then Add to BranchIDTo -- Inventory
				 * ****************************************/
                clsProduct.SubtractQuantity(clsWBranchTransferDetails.BranchIDFrom, lngProductID, lngVariationMatrixID, decQuantity, Products.getPRODUCT_INVENTORY_MOVEMENT_VALUE(PRODUCT_INVENTORY_MOVEMENT.DEDUCT_BRANCH_TRANSFER_FROM) + " " + clsWBranchTransferDetails.BranchCodeFrom + " @ " + decUnitCost.ToString("#,##0.#0") + "/" + strProductUnitCode, DateTime.Now, clsWBranchTransferDetails.WBranchTransferNo, clsWBranchTransferDetails.TransferrerName);
                clsProduct.AddQuantity(clsWBranchTransferDetails.BranchIDTo, lngProductID, lngVariationMatrixID, decQuantity, Products.getPRODUCT_INVENTORY_MOVEMENT_VALUE(PRODUCT_INVENTORY_MOVEMENT.ADD_BRANCH_TRANSFER_TO) + " " + clsWBranchTransferDetails.BranchCodeTo + " @ " + decUnitCost.ToString("#,##0.#0") + "/" + strProductUnitCode, DateTime.Now, clsWBranchTransferDetails.WBranchTransferNo, clsWBranchTransferDetails.TransferrerName);

				/*******************************************
				 * Add to Inventory Analysis
				 * ****************************************/
				clsInventoryDetails = new InventoryDetails();
				clsInventoryDetails.PostingDateFrom = clsERPConfigDetails.PostingDateFrom;
				clsInventoryDetails.PostingDateTo = clsERPConfigDetails.PostingDateTo;
				clsInventoryDetails.PostingDate = clsWBranchTransferDetails.DeliveryDate;
				clsInventoryDetails.ReferenceNo = clsWBranchTransferDetails.WBranchTransferNo;
				clsInventoryDetails.ContactID = Constants.C_RETAILPLUS_SUPPLIERID;
                clsInventoryDetails.ContactCode = Constants.C_RETAILPLUS_SUPPLIER;
                clsInventoryDetails.ProductID = lngProductID;
                clsInventoryDetails.ProductCode = strProductCode;
                clsInventoryDetails.VariationMatrixID = lngVariationMatrixID;
                clsInventoryDetails.MatrixDescription = strMatrixDescription;
				
                clsInventoryDetails.PurchaseCost = decItemCost - decVAT;
                clsInventoryDetails.PurchaseVAT = decItemCost;	// Purchase Cost with VAT

                // insert into branchid from with minus quantity
                clsInventoryDetails.BranchID = clsWBranchTransferDetails.BranchIDFrom;
                clsInventoryDetails.PurchaseQuantity = -decQuantity;
                clsInventory.Insert(clsInventoryDetails);

                // insert into branchid to with add quantity
                clsInventoryDetails.BranchID = clsWBranchTransferDetails.BranchIDTo;
                clsInventoryDetails.PurchaseQuantity = decQuantity;
                clsInventory.Insert(clsInventoryDetails);
			}
		}

		public void Cancel(long WBranchTransferID, DateTime CancelledDate, string Remarks, long CancelledByID)
		{
			try 
			{
                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandType = System.Data.CommandType.Text;

				string SQL=	"UPDATE tblWBranchTransfer SET " + 
								"CancelledDate			=	@CancelledDate, " +
								"CancelledRemarks		=	@CancelledRemarks, " +
								"CancelledByID			=	@CancelledByID, " +
								"Status				    =	@Status " +
							"WHERE WBranchTransferID = @WBranchTransferID;";

                cmd.Parameters.AddWithValue("@CancelledDate", CancelledDate.ToString("yyyy-MM-dd HH:mm:ss"));
                cmd.Parameters.AddWithValue("@CancelledRemarks", Remarks);
                cmd.Parameters.AddWithValue("@CancelledByID", CancelledByID);
                cmd.Parameters.AddWithValue("@Status", WBranchTransferStatus.Cancelled.ToString("d"));
                cmd.Parameters.AddWithValue("@WBranchTransferID", WBranchTransferID);

                cmd.CommandText = SQL;
				base.ExecuteNonQuery(cmd);

				/*******************************************
				 * Update the status of items
				 * ****************************************/
				WBranchTransferItem clsWBranchTransferItem = new WBranchTransferItem(base.Connection, base.Transaction);
				clsWBranchTransferItem.Cancel(WBranchTransferID);

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
        public bool UpdatePaymentStatus(WBranchTransferPaymentStatus paymentStatus, string IDs)
        {
            try
            {
                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandType = System.Data.CommandType.Text;

                string SQL = "UPDATE tblWBranchTransfer SET PaymentStatus = @PaymentStatus WHERE WBranchTransferID IN (" + IDs + ");";

                cmd.Parameters.AddWithValue("@PaymentStatus", paymentStatus.ToString("d"));

                cmd.CommandText = SQL;
                base.ExecuteNonQuery(cmd);

                return true;
            }
            catch (Exception ex)
            {
                throw base.ThrowException(ex);
            }
        }
        public bool UpdatePayment(long WBranchTransferID, decimal PaidAmount, WBranchTransferPaymentStatus paymentStatus)
        {
            try
            {
                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandType = System.Data.CommandType.Text;

                string SQL = "UPDATE tblWBranchTransfer SET " +
                                "PaidAmount     = PaidAmount + @PaidAmount, " +
                                "UnpaidAmount   = UnpaidAmount - @PaidAmount, " +
                                "PaymentStatus  = @PaymentStatus " +
                             "WHERE WBranchTransferID = @WBranchTransferID;";
                
                cmd.Parameters.AddWithValue("@PaidAmount", PaidAmount);
                cmd.Parameters.AddWithValue("@PaymentStatus", paymentStatus.ToString("d"));
                cmd.Parameters.AddWithValue("@WBranchTransferID", WBranchTransferID);

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

		#region Delete

		public bool Delete(string IDs)
		{
			try 
			{
                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandType = System.Data.CommandType.Text;

				string SQL=	"DELETE FROM tblWBranchTransfer WHERE WBranchTransferID IN (" + IDs + ");";
	 			
				
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
                                "WBranchTransferID, " +
                                "WBranchTransferNo, " +
                                "WBranchTransferDate, " +
                                "RequiredDeliveryDate, " +
                                "BranchIDFrom, " +
                                "b.BranchCode AS BranchCodeFrom, " +
                                "b.BranchName AS BranchNameFrom, " +
                                "b.Address AS BranchAddressFrom, " +
                                "BranchIDTo, " +
                                "c.BranchCode AS BranchCodeTo, " +
                                "c.BranchName AS BranchNameTo, " +
                                "c.Address AS BranchAddressTo, " +
                                "TransferrerID, " +
                                "TransferrerName, " +
                                "RequestedBy, " +
                                "SubTotal, " +
                                "Discount, " +
                                "DiscountApplied, " +
                                "DiscountType, " +
                                "VAT, " +
                                "VatableAmount, " +
                                "EVAT, " +
                                "EVatableAmount, " +
                                "LocalTax, " +
                                "Status, " +
                                "a.Remarks, " +
                                "ReceivedBy, " +
                                "DeliveryDate, " +
                                "CancelledDate, " +
                                "CancelledRemarks, " +
                                "CancelledByID, " +
                                "UnpaidAmount, " +
                                "PaidAmount, " +
                                "PaymentStatus, " +
                                "Freight, " +
                                "Deposit, " +
                                "TotalItemDiscount " +
                            "FROM tblWBranchTransfer a " + 
                            "   INNER JOIN tblBranch b ON a.BranchIDFrom = b.BranchID " +
                            "   INNER JOIN tblBranch c ON a.BranchIDTo = c.BranchID ";
            return stSQL;
        }

		#region Details

		public WBranchTransferDetails Details(long WBranchTransferID)
		{
			try
			{
				string SQL=	SQLSelect() + "WHERE WBranchTransferID = @WBranchTransferID;";
	 			
				MySqlCommand cmd = new MySqlCommand();
				cmd.CommandType = System.Data.CommandType.Text;
				cmd.CommandText = SQL;

				MySqlParameter prmWBranchTransferID = new MySqlParameter("@WBranchTransferID",MySqlDbType.Int16);
				prmWBranchTransferID.Value = WBranchTransferID;
				cmd.Parameters.Add(prmWBranchTransferID);

                string strDataTableName = "tbl" + this.GetType().FullName.Split(new Char[] { '.' })[this.GetType().FullName.Split(new Char[] { '.' }).Length - 1]; System.Data.DataTable dt = new System.Data.DataTable(strDataTableName);
                base.MySqlDataAdapterFill(cmd, dt);
				
				WBranchTransferDetails Details = new WBranchTransferDetails();

                foreach (System.Data.DataRow dr in dt.Rows)
				{
					Details.WBranchTransferID = WBranchTransferID;
					Details.WBranchTransferNo = "" + dr["WBranchTransferNo"].ToString();
					Details.WBranchTransferDate = DateTime.Parse(dr["WBranchTransferDate"].ToString());
					Details.RequiredDeliveryDate = DateTime.Parse(dr["RequiredDeliveryDate"].ToString());
                    Details.BranchIDFrom = Int16.Parse(dr["BranchIDFrom"].ToString());
                    Details.BranchCodeFrom = "" + dr["BranchCodeFrom"].ToString();
                    Details.BranchNameFrom = "" + dr["BranchNameFrom"].ToString();
                    Details.BranchAddressFrom = "" + dr["BranchAddressFrom"].ToString();
                    Details.BranchIDTo = Int16.Parse(dr["BranchIDTo"].ToString());
                    Details.BranchCodeTo = "" + dr["BranchCodeTo"].ToString();
                    Details.BranchNameTo = "" + dr["BranchNameTo"].ToString();
                    Details.BranchAddressTo = "" + dr["BranchAddressTo"].ToString();
					Details.TransferrerID = Int64.Parse(dr["TransferrerID"].ToString());
                    Details.TransferrerName = "" + dr["TransferrerName"].ToString();
                    Details.RequestedBy = "" + dr["RequestedBy"].ToString();
					Details.SubTotal = decimal.Parse(dr["SubTotal"].ToString());
					Details.Discount = decimal.Parse(dr["Discount"].ToString());
                    Details.DiscountApplied = decimal.Parse(dr["DiscountApplied"].ToString());
                    Details.DiscountType = (DiscountTypes)Enum.Parse(typeof(DiscountTypes), Convert.ToInt16(dr["DiscountType"]).ToString());
					Details.VAT = decimal.Parse(dr["VAT"].ToString());
					Details.VatableAmount = decimal.Parse(dr["VatableAmount"].ToString());
                    Details.EVAT = decimal.Parse(dr["EVAT"].ToString());
                    Details.EVatableAmount = decimal.Parse(dr["EVatableAmount"].ToString());
                    Details.LocalTax = decimal.Parse(dr["LocalTax"].ToString());
                    Details.Status = (WBranchTransferStatus)Enum.Parse(typeof(WBranchTransferStatus), Convert.ToInt16(dr["Status"]).ToString());
                    Details.Remarks = "" + dr["Remarks"].ToString();
                    Details.ReceivedBy = "" + dr["ReceivedBy"].ToString();
                    Details.DeliveryDate = DateTime.Parse(dr["DeliveryDate"].ToString());
                    Details.UnpaidAmount = decimal.Parse(dr["UnpaidAmount"].ToString());
                    Details.PaidAmount = decimal.Parse(dr["PaidAmount"].ToString());
                    Details.PaymentStatus = (WBranchTransferPaymentStatus)Enum.Parse(typeof(WBranchTransferPaymentStatus), Convert.ToInt16(dr["PaymentStatus"]).ToString());
                    Details.Freight = decimal.Parse(dr["Freight"].ToString());
                    Details.Deposit = decimal.Parse(dr["Deposit"].ToString());
                    Details.TotalItemDiscount = decimal.Parse(dr["TotalItemDiscount"].ToString());
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

        public System.Data.DataTable ListAsDataTable(Int64 WBranchTransferID = 0, Int64 SupplierID = 0, WBranchTransferDetails SearchKey = new WBranchTransferDetails(), 
                                                    WBranchTransferStatus WBranchTransferstatus = WBranchTransferStatus.All, 
                                                    DateTime? OrderStartDate = null, DateTime? OrderEndDate = null, 
                                                    DateTime? PostingStartDate = null, DateTime? PostingEndDate = null,
                                                    DateTime? DeliveryStartDate = null, DateTime? DeliveryEndDate = null, 
                                                    string SortField = "WBranchTransferID", SortOption SortOrder = SortOption.Ascending, Int32 limit = 0)
        {
            try
            {
                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandType = System.Data.CommandType.Text;

                string SQL = SQLSelect() + "WHERE 1=1 ";

                if (WBranchTransferID != 0) SQL += "AND WBranchTransferID = @WBranchTransferID ";
                if (SupplierID != 0) SQL += "AND SupplierID = @SupplierID ";
                if (WBranchTransferstatus != WBranchTransferStatus.All) SQL += "AND Status = @Status ";
                if (OrderStartDate.GetValueOrDefault() != DateTime.MinValue) SQL += "AND WBranchTransferDate >= @OrderStartDate ";
                if (OrderEndDate.GetValueOrDefault() != DateTime.MinValue) SQL += "AND WBranchTransferDate <= @OrderEndDate ";
                if (PostingStartDate.GetValueOrDefault() != DateTime.MinValue) SQL += "AND WBranchTransferDate >= @PostingStartDate ";
                if (PostingEndDate.GetValueOrDefault() != DateTime.MinValue) SQL += "AND WBranchTransferDate <= @PostingEndDate ";
                if (DeliveryStartDate.GetValueOrDefault() != DateTime.MinValue) SQL += "AND DeliveryDate >= @DeliveryStartDate ";
                if (DeliveryEndDate.GetValueOrDefault() != DateTime.MinValue) SQL += "AND DeliveryDate <= @DeliveryEndDate ";
                
                SQL += "ORDER BY " + (!string.IsNullOrEmpty(SortField) ? SortField : "WBranchTransferID") + " ";
                SQL += SortOrder == SortOption.Ascending ? "ASC " : "DESC ";
                SQL += limit == 0 ? "" : "LIMIT " + limit.ToString() + " ";

                if (WBranchTransferID != 0) cmd.Parameters.AddWithValue("@WBranchTransferID", WBranchTransferID);
                if (SupplierID != 0) cmd.Parameters.AddWithValue("@SupplierID", SupplierID);
                if (WBranchTransferstatus != WBranchTransferStatus.All) cmd.Parameters.AddWithValue("@Status", WBranchTransferstatus.ToString("d"));
                if (OrderStartDate.GetValueOrDefault() != DateTime.MinValue) cmd.Parameters.AddWithValue("@OrderStartDate", OrderStartDate.GetValueOrDefault() == DateTime.MinValue ? Constants.C_DATE_MIN_VALUE : OrderStartDate);
                if (OrderEndDate.GetValueOrDefault() != DateTime.MinValue) cmd.Parameters.AddWithValue("@OrderEndDate", OrderEndDate.GetValueOrDefault() == DateTime.MinValue ? Constants.C_DATE_MIN_VALUE : OrderEndDate);
                if (PostingStartDate.GetValueOrDefault() != DateTime.MinValue) cmd.Parameters.AddWithValue("@PostingStartDate", PostingStartDate.GetValueOrDefault() == DateTime.MinValue ? Constants.C_DATE_MIN_VALUE : PostingStartDate);
                if (PostingEndDate.GetValueOrDefault() != DateTime.MinValue) cmd.Parameters.AddWithValue("@PostingEndDate", PostingEndDate.GetValueOrDefault() == DateTime.MinValue ? Constants.C_DATE_MIN_VALUE : PostingEndDate);
                if (DeliveryStartDate.GetValueOrDefault() != DateTime.MinValue) cmd.Parameters.AddWithValue("@DeliveryStartDate", DeliveryStartDate.GetValueOrDefault() == DateTime.MinValue ? Constants.C_DATE_MIN_VALUE : DeliveryStartDate);
                if (DeliveryEndDate.GetValueOrDefault() != DateTime.MinValue) cmd.Parameters.AddWithValue("@DeliveryEndDate", DeliveryEndDate.GetValueOrDefault() == DateTime.MinValue ? Constants.C_DATE_MIN_VALUE : DeliveryEndDate);

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


        public MySqlDataReader ListForPayment(long SupplierID, string SortField, SortOption SortOrder)
        {
            try
            {
                if (SortField == string.Empty || SortField == null) SortField = "WBranchTransferID";

                string SQL = SQLSelect() + "WHERE PaymentStatus <> @FullyPaidPaymentStatus AND Status =@PostedStatus AND SupplierID = @SupplierID ORDER BY " + SortField;

                if (SortOrder == SortOption.Ascending)
                    SQL += " ASC";
                else
                    SQL += " DESC";

                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandText = SQL;

                MySqlParameter prmFullyPaidPaymentStatus = new MySqlParameter("@FullyPaidPaymentStatus",MySqlDbType.Int16);
                prmFullyPaidPaymentStatus.Value = WBranchTransferPaymentStatus.FullyPaid.ToString("d");
                cmd.Parameters.Add(prmFullyPaidPaymentStatus);

                MySqlParameter prmPostedStatus = new MySqlParameter("@PostedStatus",MySqlDbType.Int16);
                prmPostedStatus.Value = WBranchTransferStatus.Posted.ToString("d");
                cmd.Parameters.Add(prmPostedStatus);

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
		
		#endregion

		#region Public Modifiers

		public string LastTransactionNo()
		{
			try
			{
				string stRetValue = String.Empty;
				
				ERPConfig clsERPConfig = new ERPConfig(base.Connection, base.Transaction);
				stRetValue = clsERPConfig.get_LastWBranchTransferNo();

				return stRetValue;
			}

			catch (Exception ex)
			{
				throw base.ThrowException(ex);
			}	
		}
        public void SynchronizeAmount(long WBranchTransferID)
        {
            try
            {
                string SQL = "CALL procWBranchTransferSynchronizeAmount(@WBranchTransferID);";

                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandText = SQL;

                MySqlParameter prmWBranchTransferID = new MySqlParameter("@WBranchTransferID",MySqlDbType.Int64);
                prmWBranchTransferID.Value = WBranchTransferID;
                cmd.Parameters.Add(prmWBranchTransferID);

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


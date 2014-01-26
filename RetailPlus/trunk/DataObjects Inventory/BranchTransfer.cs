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

	#region BranchTransferDetails

	public struct BranchTransferDetails
	{
		public long BranchTransferID;
		public string BranchTransferNo;
		public DateTime BranchTransferDate;
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
		public BranchTransferStatus Status;
		public string Remarks;
		public string ReceivedBy;
		public DateTime DeliveryDate;
        public DateTime CancelledDate;
        public string CancelledRemarks;
        public long CancelledByID;
        public decimal UnpaidAmount;
        public decimal PaidAmount;
        public BranchTransferPaymentStatus PaymentStatus;
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
	public class BranchTransfer : POSConnection
	{
		#region Constructors and Destructors

		public BranchTransfer()
            : base(null, null)
        {
        }

        public BranchTransfer(MySqlConnection Connection, MySqlTransaction Transaction) 
            : base(Connection, Transaction)
		{

		}

        #endregion

		#region Insert and Update

		public long Insert(BranchTransferDetails Details)
		{
			try 
			{
                ERPConfig clsERPConfig = new ERPConfig(base.Connection, base.Transaction);
                APLinkConfigDetails clsAPLinkConfigDetails = clsERPConfig.APLinkDetails();

				string SQL = "INSERT INTO tblBranchTransfer (" +
								            "BranchTransferNo, " +
								            "BranchTransferDate, " +
								            "RequiredDeliveryDate, " +
								            "BranchIDFrom, " +
                                            "BranchIDTo, " +
								            "TransferrerID, " +
                                            "TransferrerName, " +
                                            "RequestedBy, " +
								            "Status, " +
								            "Remarks" +
							            ") VALUES (" +
                                            "@BranchTransferNo, " +
                                            "@BranchTransferDate, " +
                                            "@RequiredDeliveryDate, " +
                                            "@BranchIDFrom, " +
                                            "@BranchIDTo, " +
                                            "@TransferrerID, " +
                                            "@TransferrerName, " +
                                            "@RequestedBy, " +
                                            "@Status, " +
                                            "@Remarks" +
							            ");";
				  
				
				MySqlCommand cmd = new MySqlCommand();
				cmd.CommandType = System.Data.CommandType.Text;
				cmd.CommandText = SQL;
				
				MySqlParameter prmBranchTransferNo = new MySqlParameter("@BranchTransferNo",MySqlDbType.String);
				prmBranchTransferNo.Value = Details.BranchTransferNo;
				cmd.Parameters.Add(prmBranchTransferNo);

				MySqlParameter prmBranchTransferDate = new MySqlParameter("@BranchTransferDate",MySqlDbType.DateTime);
				prmBranchTransferDate.Value = Details.BranchTransferDate.ToString("yyyy-MM-dd HH:mm:ss");
				cmd.Parameters.Add(prmBranchTransferDate);

				MySqlParameter prmRequiredDeliveryDate = new MySqlParameter("@RequiredDeliveryDate",MySqlDbType.DateTime);
				prmRequiredDeliveryDate.Value = Details.RequiredDeliveryDate.ToString("yyyy-MM-dd HH:mm:ss");
				cmd.Parameters.Add(prmRequiredDeliveryDate);

                MySqlParameter prmBranchIDFrom = new MySqlParameter("@BranchIDFrom",MySqlDbType.Int16);
                prmBranchIDFrom.Value = Details.BranchIDFrom;
                cmd.Parameters.Add(prmBranchIDFrom);

                MySqlParameter prmBranchIDTo = new MySqlParameter("@BranchIDTo",MySqlDbType.Int16);
                prmBranchIDTo.Value = Details.BranchIDTo;
                cmd.Parameters.Add(prmBranchIDTo);

				MySqlParameter prmTransferrerID = new MySqlParameter("@TransferrerID",MySqlDbType.Int64);						
				prmTransferrerID.Value = Details.TransferrerID;
				cmd.Parameters.Add(prmTransferrerID);

                MySqlParameter prmTransferrerName = new MySqlParameter("@TransferrerName",MySqlDbType.String);
                prmTransferrerName.Value = Details.TransferrerName;
                cmd.Parameters.Add(prmTransferrerName);

                MySqlParameter prmRequestedBy = new MySqlParameter("@RequestedBy",MySqlDbType.String);
                prmRequestedBy.Value = Details.RequestedBy;
                cmd.Parameters.Add(prmRequestedBy);

				MySqlParameter prmStatus = new MySqlParameter("@Status",MySqlDbType.Int16);			
				prmStatus.Value = Details.Status.ToString("d");
				cmd.Parameters.Add(prmStatus);

				MySqlParameter prmRemarks = new MySqlParameter("@Remarks",MySqlDbType.String);			
				prmRemarks.Value = Details.Remarks;
                cmd.Parameters.Add(prmRemarks);

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
		public void Update(BranchTransferDetails Details)
		{
			try 
			{
                ERPConfig clsERPConfig = new ERPConfig(base.Connection, base.Transaction);
                APLinkConfigDetails clsAPLinkConfigDetails = clsERPConfig.APLinkDetails();

                string SQL=	"UPDATE tblBranchTransfer SET " +
                                "BranchTransferNo			=	@BranchTransferNo, " +
                                "BranchTransferDate			=	@BranchTransferDate, " +
                                "BranchIDFrom				=	@BranchIDFrom, " +
                                "BranchIDTo 				=	@BranchIDTo, " +
                                "TransferrerID			    =	@TransferrerID, " +
                                "TransferrerName            =   @TransferrerName, " +
                                "RequestedBy                =   @RequestedBy, " +
                                "Remarks                    =   @Remarks " +
							"WHERE BranchTransferID = @BranchTransferID;";
				  
				MySqlCommand cmd = new MySqlCommand();
				cmd.CommandType = System.Data.CommandType.Text;
				cmd.CommandText = SQL;

                MySqlParameter prmBranchTransferNo = new MySqlParameter("@BranchTransferNo",MySqlDbType.String);
                prmBranchTransferNo.Value = Details.BranchTransferNo;
                cmd.Parameters.Add(prmBranchTransferNo);

                MySqlParameter prmBranchTransferDate = new MySqlParameter("@BranchTransferDate",MySqlDbType.DateTime);
                prmBranchTransferDate.Value = Details.BranchTransferDate.ToString("yyyy-MM-dd HH:mm:ss");
                cmd.Parameters.Add(prmBranchTransferDate);

                MySqlParameter prmRequiredDeliveryDate = new MySqlParameter("@RequiredDeliveryDate",MySqlDbType.DateTime);
                prmRequiredDeliveryDate.Value = Details.RequiredDeliveryDate.ToString("yyyy-MM-dd HH:mm:ss");
                cmd.Parameters.Add(prmRequiredDeliveryDate);

                MySqlParameter prmBranchIDFrom = new MySqlParameter("@BranchIDFrom",MySqlDbType.Int16);
                prmBranchIDFrom.Value = Details.BranchIDFrom;
                cmd.Parameters.Add(prmBranchIDFrom);

                MySqlParameter prmBranchIDTo = new MySqlParameter("@BranchIDTo",MySqlDbType.Int16);
                prmBranchIDTo.Value = Details.BranchIDTo;
                cmd.Parameters.Add(prmBranchIDTo);

                MySqlParameter prmTransferrerID = new MySqlParameter("@TransferrerID",MySqlDbType.Int64);
                prmTransferrerID.Value = Details.TransferrerID;
                cmd.Parameters.Add(prmTransferrerID);

                MySqlParameter prmTransferrerName = new MySqlParameter("@TransferrerName",MySqlDbType.String);
                prmTransferrerName.Value = Details.TransferrerName;
                cmd.Parameters.Add(prmTransferrerName);

                MySqlParameter prmRequestedBy = new MySqlParameter("@RequestedBy",MySqlDbType.String);
                prmRequestedBy.Value = Details.RequestedBy;
                cmd.Parameters.Add(prmRequestedBy);
                
                MySqlParameter prmRemarks = new MySqlParameter("@Remarks",MySqlDbType.String);
                prmRemarks.Value = Details.Remarks;
                cmd.Parameters.Add(prmRemarks);

				MySqlParameter prmBranchTransferID = new MySqlParameter("@BranchTransferID",MySqlDbType.Int64);						
				prmBranchTransferID.Value = Details.BranchTransferID;
				cmd.Parameters.Add(prmBranchTransferID);

				base.ExecuteNonQuery(cmd);
			}

			catch (Exception ex)
			{
				throw base.ThrowException(ex);
			}	
		}

        public void UpdateDiscount(long BranchTransferID, decimal DiscountApplied, DiscountTypes DiscountType)
        {
            try
            {
                string SQL = "UPDATE tblBranchTransfer SET " +
                                "DiscountApplied        =   @DiscountApplied, " +
                                "DiscountType           =   @DiscountType " +
                            "WHERE BranchTransferID = @BranchTransferID;";

                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandText = SQL;

                MySqlParameter prmDiscountApplied = new MySqlParameter("@DiscountApplied",MySqlDbType.Decimal);
                prmDiscountApplied.Value = DiscountApplied;
                cmd.Parameters.Add(prmDiscountApplied);

                MySqlParameter prmDiscountType = new MySqlParameter("@DiscountType",MySqlDbType.Int16);
                prmDiscountType.Value = Convert.ToInt16(DiscountType.ToString("d"));
                cmd.Parameters.Add(prmDiscountType);

                MySqlParameter prmBranchTransferID = new MySqlParameter("@BranchTransferID",MySqlDbType.Int64);
                prmBranchTransferID.Value = BranchTransferID;
                cmd.Parameters.Add(prmBranchTransferID);

                base.ExecuteNonQuery(cmd);
            }

            catch (Exception ex)
            {
                throw base.ThrowException(ex);
            }
        }
        public void UpdateDiscountFreightDeposit(long BranchTransferID, decimal DiscountApplied, DiscountTypes DiscountType)
        {
            try
            {
                string SQL = "UPDATE tblBranchTransfer SET " +
                                "DiscountApplied        =   @DiscountApplied, " +
                                "DiscountType           =   @DiscountType " +
                            "WHERE BranchTransferID = @BranchTransferID;";

                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandText = SQL;

                MySqlParameter prmDiscountApplied = new MySqlParameter("@DiscountApplied",MySqlDbType.Decimal);
                prmDiscountApplied.Value = DiscountApplied;
                cmd.Parameters.Add(prmDiscountApplied);

                MySqlParameter prmDiscountType = new MySqlParameter("@DiscountType",MySqlDbType.Int16);
                prmDiscountType.Value = Convert.ToInt16(DiscountType.ToString("d"));
                cmd.Parameters.Add(prmDiscountType);

                MySqlParameter prmBranchTransferID = new MySqlParameter("@BranchTransferID",MySqlDbType.Int64);
                prmBranchTransferID.Value = BranchTransferID;
                cmd.Parameters.Add(prmBranchTransferID);

                base.ExecuteNonQuery(cmd);
            }

            catch (Exception ex)
            {
                throw base.ThrowException(ex);
            }
        }
        public void UpdateFreight(long BranchTransferID, decimal Freight)
        {
            try
            {
                string SQL = "UPDATE tblBranchTransfer SET " +
                                "Freight           =   @Freight " +
                            "WHERE BranchTransferID = @BranchTransferID;";

                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandText = SQL;

                MySqlParameter prmFreight = new MySqlParameter("@Freight",MySqlDbType.Decimal);
                prmFreight.Value = Freight;
                cmd.Parameters.Add(prmFreight);

                MySqlParameter prmBranchTransferID = new MySqlParameter("@BranchTransferID",MySqlDbType.Int64);
                prmBranchTransferID.Value = BranchTransferID;
                cmd.Parameters.Add(prmBranchTransferID);

                base.ExecuteNonQuery(cmd);
            }

            catch (Exception ex)
            {
                throw base.ThrowException(ex);
            }
        }
        public void UpdateDeposit(long BranchTransferID, decimal Deposit)
        {
            try
            {
                string SQL = "UPDATE tblBranchTransfer SET " +
                                "Deposit           =   @Deposit " +
                            "WHERE BranchTransferID = @BranchTransferID;";

                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandText = SQL;

                MySqlParameter prmDeposit = new MySqlParameter("@Deposit",MySqlDbType.Decimal);
                prmDeposit.Value = Deposit;
                cmd.Parameters.Add(prmDeposit);

                MySqlParameter prmBranchTransferID = new MySqlParameter("@BranchTransferID",MySqlDbType.Int64);
                prmBranchTransferID.Value = BranchTransferID;
                cmd.Parameters.Add(prmBranchTransferID);

                base.ExecuteNonQuery(cmd);
            }

            catch (Exception ex)
            {
                throw base.ThrowException(ex);
            }
        }

		public void IssueGRN(long BranchTransferID, string ReceivedBy, DateTime DeliveryDate)
		{
			try 
			{
				string SQL=	"UPDATE tblBranchTransfer SET " + 
								"ReceivedBy			    =	@ReceivedBy, " +
								"DeliveryDate			=	@DeliveryDate, " +
								"Status				    =	@Status " +
							"WHERE BranchTransferID = @BranchTransferID;";
				  
				MySqlCommand cmd = new MySqlCommand();
				cmd.CommandType = System.Data.CommandType.Text;
				cmd.CommandText = SQL;
				
				MySqlParameter prmReceivedBy = new MySqlParameter("@ReceivedBy",MySqlDbType.String);
				prmReceivedBy.Value = ReceivedBy;
				cmd.Parameters.Add(prmReceivedBy);

				MySqlParameter prmDeliveryDate = new MySqlParameter("@DeliveryDate",MySqlDbType.DateTime);
				prmDeliveryDate.Value = DeliveryDate.ToString("yyyy-MM-dd HH:mm:ss");
				cmd.Parameters.Add(prmDeliveryDate);

				MySqlParameter prmStatus = new MySqlParameter("@Status",MySqlDbType.Int16);
				prmStatus.Value = BranchTransferStatus.Posted.ToString("d");
				cmd.Parameters.Add(prmStatus);

				MySqlParameter prmBranchTransferID = new MySqlParameter("@BranchTransferID",MySqlDbType.Int64);						
				prmBranchTransferID.Value = BranchTransferID;
				cmd.Parameters.Add(prmBranchTransferID);

				base.ExecuteNonQuery(cmd);

				/*******************************************
				 * Update the status of items
				 * ****************************************/
				BranchTransferItem clsBranchTransferItem = new BranchTransferItem(base.Connection, base.Transaction);
				clsBranchTransferItem.Post(BranchTransferID);

				/*******************************************
				 * Update Vendor Account
				 * ****************************************/
				AddItemToInventory(BranchTransferID);

                /*******************************************
				 * Update Account Balance
				 * ****************************************/
                UpdateAccounts(BranchTransferID);

                /*******************************************
				 * Update Required Inventory Days (RID)
				 * ****************************************/
                UpdateRID(BranchTransferID);
			}

			catch (Exception ex)
			{
				throw base.ThrowException(ex);
			}	
		}

        private void UpdateAccounts(long BranchTransferID)
        {
            try
            {
                //BranchTransferDetails clsBranchTransferDetails = Details(BranchTransferID);
                //ChartOfAccount clsChartOfAccount = new ChartOfAccount(base.Connection, base.Transaction);

                //// update ChartOfAccountIDAPTracking as credit
                //clsChartOfAccount.UpdateCredit(clsBranchTransferDetails.ChartOfAccountIDAPTracking, clsBranchTransferDetails.SubTotal);

                //// update Deposit & APContra
                //clsChartOfAccount.UpdateCredit(clsBranchTransferDetails.ChartOfAccountIDAPContra, clsBranchTransferDetails.Discount);

                //// update Freight & APTracking
                //clsChartOfAccount.UpdateCredit(clsBranchTransferDetails.ChartOfAccountIDAPTracking, clsBranchTransferDetails.Freight);    
                //clsChartOfAccount.UpdateDebit(clsBranchTransferDetails.ChartOfAccountIDAPFreight, clsBranchTransferDetails.Freight);

                //// update Deposit & APTracking
                //clsChartOfAccount.UpdateCredit(clsBranchTransferDetails.ChartOfAccountIDAPTracking, clsBranchTransferDetails.Deposit);
                //clsChartOfAccount.UpdateDebit(clsBranchTransferDetails.ChartOfAccountIDAPVDeposit, clsBranchTransferDetails.Deposit);

                //BranchTransferItem clsBranchTransferItem = new BranchTransferItem(base.Connection, base.Transaction);
                //MySqlDataReader myReader = clsBranchTransferItem.List(BranchTransferID, "BranchTransferItemID", SortOption.Ascending);
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

        private void UpdateRID(long BranchTransferID)
        {
            try
            {
                //string SQL = "CALL procProductUpdateRIDByBranchTransfer(@lngBranchTransferID)";

                //

                //MySqlCommand cmd = new MySqlCommand();
                //
                //
                //cmd.CommandType = System.Data.CommandType.Text;
                //cmd.CommandText = SQL;

                //cmd.Parameters.AddWithValue("@lngBranchTransferID", BranchTransferID);

                //base.ExecuteNonQuery(cmd);

            }

            catch (Exception ex)
            {
                throw base.ThrowException(ex);
            }

        }
		private void AddItemToInventory(long BranchTransferID)
		{

			BranchTransferDetails clsBranchTransferDetails = Details(BranchTransferID);
            ERPConfig clsERPConfig = new ERPConfig(base.Connection, base.Transaction);
			ERPConfigDetails clsERPConfigDetails = clsERPConfig.Details();

			BranchTransferItem clsBranchTransferItem = new BranchTransferItem(base.Connection, base.Transaction);
            ProductUnit clsProductUnit = new ProductUnit(base.Connection, base.Transaction);
            Products clsProduct = new Products(base.Connection, base.Transaction);

            Inventory clsInventory = new Inventory(base.Connection, base.Transaction);
            InventoryDetails clsInventoryDetails;

			System.Data.DataTable dt = clsBranchTransferItem.ListAsDataTable(BranchTransferID, "BranchTransferItemID", SortOption.Ascending);

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
                clsProduct.SubtractQuantity(clsBranchTransferDetails.BranchIDFrom, lngProductID, lngVariationMatrixID, decQuantity, Products.getPRODUCT_INVENTORY_MOVEMENT_VALUE(PRODUCT_INVENTORY_MOVEMENT.DEDUCT_BRANCH_TRANSFER_FROM) + " " + clsBranchTransferDetails.BranchCodeFrom + " @ " + decUnitCost.ToString("#,##0.#0") + "/" + strProductUnitCode, DateTime.Now, clsBranchTransferDetails.BranchTransferNo, clsBranchTransferDetails.TransferrerName);
                clsProduct.AddQuantity(clsBranchTransferDetails.BranchIDTo, lngProductID, lngVariationMatrixID, decQuantity, Products.getPRODUCT_INVENTORY_MOVEMENT_VALUE(PRODUCT_INVENTORY_MOVEMENT.ADD_BRANCH_TRANSFER_TO) + " " + clsBranchTransferDetails.BranchCodeTo + " @ " + decUnitCost.ToString("#,##0.#0") + "/" + strProductUnitCode, DateTime.Now, clsBranchTransferDetails.BranchTransferNo, clsBranchTransferDetails.TransferrerName);

				/*******************************************
				 * Add to Inventory Analysis
				 * ****************************************/
				clsInventoryDetails = new InventoryDetails();
				clsInventoryDetails.PostingDateFrom = clsERPConfigDetails.PostingDateFrom;
				clsInventoryDetails.PostingDateTo = clsERPConfigDetails.PostingDateTo;
				clsInventoryDetails.PostingDate = clsBranchTransferDetails.DeliveryDate;
				clsInventoryDetails.ReferenceNo = clsBranchTransferDetails.BranchTransferNo;
				clsInventoryDetails.ContactID = Constants.C_RETAILPLUS_SUPPLIERID;
                clsInventoryDetails.ContactCode = Constants.C_RETAILPLUS_SUPPLIER;
                clsInventoryDetails.ProductID = lngProductID;
                clsInventoryDetails.ProductCode = strProductCode;
                clsInventoryDetails.VariationMatrixID = lngVariationMatrixID;
                clsInventoryDetails.MatrixDescription = strMatrixDescription;
				
                clsInventoryDetails.PurchaseCost = decItemCost - decVAT;
                clsInventoryDetails.PurchaseVAT = decItemCost;	// Purchase Cost with VAT

                // insert into branchid from with minus quantity
                clsInventoryDetails.BranchID = clsBranchTransferDetails.BranchIDFrom;
                clsInventoryDetails.PurchaseQuantity = -decQuantity;
                clsInventory.Insert(clsInventoryDetails);

                // insert into branchid to with add quantity
                clsInventoryDetails.BranchID = clsBranchTransferDetails.BranchIDTo;
                clsInventoryDetails.PurchaseQuantity = decQuantity;
                clsInventory.Insert(clsInventoryDetails);

			}

		}
		public void Cancel(long BranchTransferID, DateTime CancelledDate, string Remarks, long CancelledByID)
		{
			try 
			{
				string SQL=	"UPDATE tblBranchTransfer SET " + 
								"CancelledDate			=	@CancelledDate, " +
								"CancelledRemarks		=	@CancelledRemarks, " +
								"CancelledByID			=	@CancelledByID, " +
								"Status				    =	@Status " +
							"WHERE BranchTransferID = @BranchTransferID;";

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
				prmStatus.Value = BranchTransferStatus.Cancelled.ToString("d");
				cmd.Parameters.Add(prmStatus);

				MySqlParameter prmBranchTransferID = new MySqlParameter("@BranchTransferID",MySqlDbType.Int64);						
				prmBranchTransferID.Value = BranchTransferID;
				cmd.Parameters.Add(prmBranchTransferID);

				base.ExecuteNonQuery(cmd);

				/*******************************************
				 * Update the status of items
				 * ****************************************/
				BranchTransferItem clsBranchTransferItem = new BranchTransferItem(base.Connection, base.Transaction);
				clsBranchTransferItem.Cancel(BranchTransferID);

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
        public bool UpdatePaymentStatus(BranchTransferPaymentStatus paymentStatus, string IDs)
        {
            try
            {
                string SQL = "UPDATE tblBranchTransfer SET PaymentStatus = @PaymentStatus WHERE BranchTransferID IN (" + IDs + ");";

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
        public bool UpdatePayment(long BranchTransferID, decimal PaidAmount, BranchTransferPaymentStatus paymentStatus)
        {
            try
            {
                string SQL = "UPDATE tblBranchTransfer SET " +
                                "PaidAmount     = PaidAmount + @PaidAmount, " +
                                "UnpaidAmount   = UnpaidAmount - @PaidAmount, " +
                                "PaymentStatus  = @PaymentStatus " +
                             "WHERE BranchTransferID = @BranchTransferID;";

                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandText = SQL;

                MySqlParameter prmPaidAmount = new MySqlParameter("@PaidAmount",MySqlDbType.Decimal);
                prmPaidAmount.Value = PaidAmount;
                cmd.Parameters.Add(prmPaidAmount);

                MySqlParameter prmPaymentStatus = new MySqlParameter("@PaymentStatus",MySqlDbType.Int16);
                prmPaymentStatus.Value = paymentStatus.ToString("d");
                cmd.Parameters.Add(prmPaymentStatus);

                MySqlParameter prmBranchTransferID = new MySqlParameter("@BranchTransferID",MySqlDbType.Int64);
                prmBranchTransferID.Value = BranchTransferID;
                cmd.Parameters.Add(prmBranchTransferID);

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
				string SQL=	"DELETE FROM tblBranchTransfer WHERE BranchTransferID IN (" + IDs + ");";
	 			
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
                                "BranchTransferID, " +
                                "BranchTransferNo, " +
                                "BranchTransferDate, " +
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
                            "FROM tblBranchTransfer a " + 
                            "   INNER JOIN tblBranch b ON a.BranchIDFrom = b.BranchID " +
                            "   INNER JOIN tblBranch c ON a.BranchIDTo = c.BranchID ";
            return stSQL;
        }

		#region Details

		public BranchTransferDetails Details(long BranchTransferID)
		{
			try
			{
				string SQL=	SQLSelect() + "WHERE BranchTransferID = @BranchTransferID;";
	 			
				MySqlCommand cmd = new MySqlCommand();
				cmd.CommandType = System.Data.CommandType.Text;
				cmd.CommandText = SQL;

				MySqlParameter prmBranchTransferID = new MySqlParameter("@BranchTransferID",MySqlDbType.Int16);
				prmBranchTransferID.Value = BranchTransferID;
				cmd.Parameters.Add(prmBranchTransferID);

                string strDataTableName = "tbl" + this.GetType().FullName.Split(new Char[] { '.' })[this.GetType().FullName.Split(new Char[] { '.' }).Length - 1]; System.Data.DataTable dt = new System.Data.DataTable(strDataTableName);
                base.MySqlDataAdapterFill(cmd, dt);
				
				BranchTransferDetails Details = new BranchTransferDetails();

                foreach (System.Data.DataRow dr in dt.Rows)
				{
					Details.BranchTransferID = BranchTransferID;
					Details.BranchTransferNo = "" + dr["BranchTransferNo"].ToString();
					Details.BranchTransferDate = DateTime.Parse(dr["BranchTransferDate"].ToString());
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
                    Details.Status = (BranchTransferStatus)Enum.Parse(typeof(BranchTransferStatus), Convert.ToInt16(dr["Status"]).ToString());
                    Details.Remarks = "" + dr["Remarks"].ToString();
                    Details.ReceivedBy = "" + dr["ReceivedBy"].ToString();
                    Details.DeliveryDate = DateTime.Parse(dr["DeliveryDate"].ToString());
                    Details.UnpaidAmount = decimal.Parse(dr["UnpaidAmount"].ToString());
                    Details.PaidAmount = decimal.Parse(dr["PaidAmount"].ToString());
                    Details.PaymentStatus = (BranchTransferPaymentStatus)Enum.Parse(typeof(BranchTransferPaymentStatus), Convert.ToInt16(dr["PaymentStatus"]).ToString());
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

        public System.Data.DataTable ListAsDataTable(BranchTransferStatus branchtransferstatus, string SortField, SortOption SortOrder)
        {
            if (SortField == string.Empty || SortField == null) SortField = "BranchTransferID";

            string SQL = SQLSelect() + "WHERE Status = @Status ";

            SQL += "ORDER BY " + SortField;

            if (SortOrder == SortOption.Ascending)
                SQL += " ASC";
            else
                SQL += " DESC";

            MySqlCommand cmd = new MySqlCommand();
            cmd.CommandType = System.Data.CommandType.Text;
            cmd.CommandText = SQL;

            cmd.Parameters.AddWithValue("@Status", branchtransferstatus.ToString("d"));

            string strDataTableName = "tbl" + this.GetType().FullName.Split(new Char[] { '.' })[this.GetType().FullName.Split(new Char[] { '.' }).Length - 1]; System.Data.DataTable dt = new System.Data.DataTable(strDataTableName);
            base.MySqlDataAdapterFill(cmd, dt);

            return dt;
        }
        public System.Data.DataTable ListAsDataTable(BranchTransferStatus branchtransferstatus, DateTime OrderStartDate, DateTime OrderEndDate, DateTime PostingStartDate, DateTime PostingEndDate, string SortField, SortOption SortOrder)
        {
            if (SortField == string.Empty || SortField == null) SortField = "BranchTransferID";

            string SQL = SQLSelect() + "WHERE Status = @Status ";

            if (OrderStartDate != DateTime.MinValue) SQL += "AND BranchTransferDate >= @OrderStartDate ";
            if (OrderEndDate != DateTime.MinValue) SQL += "AND BranchTransferDate <= @OrderEndDate ";
            if (PostingStartDate != DateTime.MinValue) SQL += "AND BranchTransferDate >= @PostingStartDate ";
            if (PostingEndDate != DateTime.MinValue) SQL += "AND BranchTransferDate <= @PostingEndDate ";

            SQL += "ORDER BY " + SortField;

            if (SortOrder == SortOption.Ascending)
                SQL += " ASC";
            else
                SQL += " DESC";

            MySqlCommand cmd = new MySqlCommand();
            cmd.CommandType = System.Data.CommandType.Text;
            cmd.CommandText = SQL;

            cmd.Parameters.AddWithValue("@Status", branchtransferstatus.ToString("d"));

            if (OrderStartDate != DateTime.MinValue) cmd.Parameters.AddWithValue("@OrderStartDate", OrderStartDate.ToString("yyyy-MM-dd HH:mm:ss"));
            if (OrderEndDate != DateTime.MinValue) cmd.Parameters.AddWithValue("@OrderEndDate", OrderEndDate.ToString("yyyy-MM-dd HH:mm:ss"));
            if (PostingStartDate != DateTime.MinValue) cmd.Parameters.AddWithValue("@PostingStartDate", PostingStartDate.ToString("yyyy-MM-dd HH:mm:ss"));
            if (PostingEndDate != DateTime.MinValue) cmd.Parameters.AddWithValue("@PostingEndDate", PostingEndDate.ToString("yyyy-MM-dd HH:mm:ss"));

            string strDataTableName = "tbl" + this.GetType().FullName.Split(new Char[] { '.' })[this.GetType().FullName.Split(new Char[] { '.' }).Length - 1]; System.Data.DataTable dt = new System.Data.DataTable(strDataTableName);
            base.MySqlDataAdapterFill(cmd, dt);

            return dt;
        }

        public System.Data.DataTable ListAsDataTable(string SortField, SortOption SortOrder)
		{
            if (SortField == string.Empty || SortField == null) SortField = "BranchTransferID";

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

		public MySqlDataReader List(long BranchTransferID, string SortField = "BranchTransferID", SortOption SortOrder = SortOption.Ascending)
		{
			try
			{
                string SQL = SQLSelect() + "WHERE BranchTransferID = @BranchTransferID ORDER BY " + SortField;

				if (SortOrder == SortOption.Ascending)
					SQL += " ASC";
				else
					SQL += " DESC";

				MySqlCommand cmd = new MySqlCommand();
				cmd.CommandType = System.Data.CommandType.Text;
				cmd.CommandText = SQL;

                cmd.Parameters.AddWithValue("@BranchTransferID", BranchTransferID);

				MySqlDataReader myReader = base.ExecuteReader(cmd);
				
				return myReader;			
			}
			catch (Exception ex)
			{
				throw base.ThrowException(ex);
			}	
		}

		public MySqlDataReader List(string SortField = "BranchTransferID", SortOption SortOrder = SortOption.Ascending)
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

		public MySqlDataReader List(BranchTransferStatus branchtransferstatus, string SortField, SortOption SortOrder)
		{
			try
			{
                if (SortField == string.Empty || SortField == null) SortField = "BranchTransferID";

				string SQL = SQLSelect() + "WHERE Status = @Status ORDER BY " + SortField;

				if (SortOrder == SortOption.Ascending)
					SQL += " ASC";
				else
					SQL += " DESC";

				MySqlCommand cmd = new MySqlCommand();
				cmd.CommandType = System.Data.CommandType.Text;
				cmd.CommandText = SQL;
				
				MySqlParameter prmStatus = new MySqlParameter("@Status",MySqlDbType.Int16);			
				prmStatus.Value = branchtransferstatus.ToString("d");
				cmd.Parameters.Add(prmStatus);

				MySqlDataReader myReader = base.ExecuteReader(cmd);
				
				return myReader;			
			}
			catch (Exception ex)
			{
				throw base.ThrowException(ex);
			}	
		}

		public MySqlDataReader List(BranchTransferStatus branchtransferstatus, long SupplierID, string SortField, SortOption SortOrder)
		{
			try
			{
                if (SortField == string.Empty || SortField == null) SortField = "BranchTransferID";

				string SQL = SQLSelect() + "WHERE Status =@Status AND SupplierID = @SupplierID ORDER BY " + SortField;

				if (SortOrder == SortOption.Ascending)
					SQL += " ASC";
				else
					SQL += " DESC";

				MySqlCommand cmd = new MySqlCommand();
				cmd.CommandType = System.Data.CommandType.Text;
				cmd.CommandText = SQL;
				
				MySqlParameter prmStatus = new MySqlParameter("@Status",MySqlDbType.Int16);			
				prmStatus.Value = branchtransferstatus.ToString("d");
				cmd.Parameters.Add(prmStatus);

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
        public MySqlDataReader ListForPayment(long SupplierID, string SortField, SortOption SortOrder)
        {
            try
            {
                if (SortField == string.Empty || SortField == null) SortField = "BranchTransferID";

                string SQL = SQLSelect() + "WHERE PaymentStatus <> @FullyPaidPaymentStatus AND Status =@PostedStatus AND SupplierID = @SupplierID ORDER BY " + SortField;

                if (SortOrder == SortOption.Ascending)
                    SQL += " ASC";
                else
                    SQL += " DESC";

                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandText = SQL;

                MySqlParameter prmFullyPaidPaymentStatus = new MySqlParameter("@FullyPaidPaymentStatus",MySqlDbType.Int16);
                prmFullyPaidPaymentStatus.Value = BranchTransferPaymentStatus.FullyPaid.ToString("d");
                cmd.Parameters.Add(prmFullyPaidPaymentStatus);

                MySqlParameter prmPostedStatus = new MySqlParameter("@PostedStatus",MySqlDbType.Int16);
                prmPostedStatus.Value = BranchTransferStatus.Posted.ToString("d");
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
		public MySqlDataReader Search(string SearchKey, string SortField, SortOption SortOrder)
		{
			try
			{
                if (SortField == string.Empty || SortField == null) SortField = "BranchTransferID";

				string SQL = SQLSelect() + "WHERE (BranchTransferNo LIKE @SearchKey or BranchTransferDate LIKE @SearchKey or SupplierCode LIKE @SearchKey " +
										"or SupplierContact LIKE @SearchKey or BranchCode LIKE @SearchKey or RequiredDeliveryDate LIKE @SearchKey) " +
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
		public MySqlDataReader Search(BranchTransferStatus branchtransferstatus, string SearchKey, string SortField, SortOption SortOrder)
		{
			try
			{
                if (SortField == string.Empty || SortField == null) SortField = "BranchTransferID";

				string SQL = SQLSelect() + "WHERE Status = @Status AND (BranchTransferNo LIKE @SearchKey or BranchTransferDate LIKE @SearchKey or SupplierCode LIKE @SearchKey " +
                                        "or SupplierContact LIKE @SearchKey or BranchCode LIKE @SearchKey or RequiredDeliveryDate LIKE @SearchKey or a.Remarks LIKE @SearchKey) " +
							"ORDER BY " + SortField;

				if (SortOrder == SortOption.Ascending)
					SQL += " ASC";
				else
					SQL += " DESC";

				MySqlCommand cmd = new MySqlCommand();
				cmd.CommandType = System.Data.CommandType.Text;
				cmd.CommandText = SQL;
				
				MySqlParameter prmStatus = new MySqlParameter("@Status",MySqlDbType.Int16);			
				prmStatus.Value = branchtransferstatus.ToString("d");
				cmd.Parameters.Add(prmStatus);

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
        public System.Data.DataTable SearchAsDataTable(BranchTransferStatus branchtransferstatus, string SearchKey, string SortField, SortOption SortOrder)
        {
            try
            {
                if (SortField == string.Empty || SortField == null) SortField = "BranchTransferID";

                string SQL = SQLSelect() + "WHERE Status = @Status AND (BranchTransferNo LIKE @SearchKey or BranchTransferDate LIKE @SearchKey or SupplierCode LIKE @SearchKey " +
                                        "or SupplierContact LIKE @SearchKey or BranchCode LIKE @SearchKey or RequiredDeliveryDate LIKE @SearchKey or a.Remarks LIKE @SearchKey) " +
                            "ORDER BY " + SortField;

                if (SortOrder == SortOption.Ascending)
                    SQL += " ASC";
                else
                    SQL += " DESC";

                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandText = SQL;

                MySqlParameter prmStatus = new MySqlParameter("@Status",MySqlDbType.Int16);
                prmStatus.Value = branchtransferstatus.ToString("d");
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
        public System.Data.DataTable SearchAsDataTable(BranchTransferStatus branchtransferstatus, DateTime OrderStartDate, DateTime OrderEndDate, DateTime PostingStartDate, DateTime PostingEndDate, string SearchKey, string SortField, SortOption SortOrder)
        {
            try
            {
                if (SortField == string.Empty || SortField == null) SortField = "BranchTransferID";

                string SQL = SQLSelect() + "WHERE Status = @Status ";
                
                if (SearchKey != string.Empty || SearchKey != null)
                    SQL += "AND (BranchTransferNo LIKE @SearchKey " +
                                        "or b.BranchCode LIKE @SearchKey or c.BranchCode LIKE @SearchKey or a.Remarks LIKE @SearchKey) ";

                if (OrderStartDate != DateTime.MinValue) SQL += "AND BranchTransferDate >= @OrderStartDate ";
                if (OrderEndDate != DateTime.MinValue) SQL += "AND BranchTransferDate <= @OrderEndDate ";
                if (PostingStartDate != DateTime.MinValue) SQL += "AND BranchTransferDate >= @PostingStartDate ";
                if (PostingEndDate != DateTime.MinValue) SQL += "AND BranchTransferDate <= @PostingEndDate ";

                SQL += "ORDER BY " + SortField;

                if (SortOrder == SortOption.Ascending)
                    SQL += " ASC";
                else
                    SQL += " DESC";

                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandText = SQL;

                cmd.Parameters.AddWithValue("@Status", branchtransferstatus.ToString("d"));
                if (SearchKey != string.Empty || SearchKey != null) cmd.Parameters.AddWithValue("@SearchKey", "%" + SearchKey + "%");

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

        public MySqlDataReader List(BranchTransferStatus branchtransferstatus, DateTime StartDate, DateTime EndDate)
		{
			try
			{
				string SQL = SQLSelect() + "WHERE Status = @Status AND DeliveryDate BETWEEN @StartDate AND @EndDate ORDER BY BranchTransferID ASC";

				MySqlCommand cmd = new MySqlCommand();
				cmd.CommandType = System.Data.CommandType.Text;
				cmd.CommandText = SQL;
				
				MySqlParameter prmStartDate = new MySqlParameter("@StartDate",MySqlDbType.DateTime);			
				prmStartDate.Value = StartDate.ToString("yyyy-MM-dd HH:mm:ss");
				cmd.Parameters.Add(prmStartDate);

				MySqlParameter prmEndDate = new MySqlParameter("@EndDate",MySqlDbType.DateTime);			
				prmEndDate.Value = EndDate.ToString("yyyy-MM-dd HH:mm:ss");
				cmd.Parameters.Add(prmEndDate);

				MySqlParameter prmStatus = new MySqlParameter("@Status",MySqlDbType.Int16);			
				prmStatus.Value = branchtransferstatus.ToString("d");
				cmd.Parameters.Add(prmStatus);

				MySqlDataReader myReader = base.ExecuteReader(cmd);
				
				return myReader;			
			}
			catch (Exception ex)
			{
				throw base.ThrowException(ex);
			}	
		}
        public MySqlDataReader List(BranchTransferStatus branchtransferstatus, long SupplierID, DateTime StartDate, DateTime EndDate)
        {
            try
            {
                string SQL = SQLSelect() + "WHERE Status = @Status AND SupplierID = @SupplierID AND DeliveryDate BETWEEN @StartDate AND @EndDate ORDER BY BranchTransferID ASC";

                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandText = SQL;

                cmd.Parameters.AddWithValue("@Status", branchtransferstatus.ToString("d"));
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
		
		#endregion

		#region Public Modifiers

		public string LastTransactionNo()
		{
			try
			{
				string stRetValue = String.Empty;
				
				ERPConfig clsERPConfig = new ERPConfig(base.Connection, base.Transaction);
				stRetValue = clsERPConfig.get_LastBranchTransferNo();

				return stRetValue;
			}

			catch (Exception ex)
			{
				throw base.ThrowException(ex);
			}	
		}
        public void SynchronizeAmount(long BranchTransferID)
        {
            try
            {
                string SQL = "CALL procBranchTransferSynchronizeAmount(@BranchTransferID);";

                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandText = SQL;

                MySqlParameter prmBranchTransferID = new MySqlParameter("@BranchTransferID",MySqlDbType.Int64);
                prmBranchTransferID.Value = BranchTransferID;
                cmd.Parameters.Add(prmBranchTransferID);

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


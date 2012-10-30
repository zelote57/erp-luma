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
	public class BranchTransfer
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

		public BranchTransfer()
		{
			
		}

		public BranchTransfer(MySqlConnection Connection, MySqlTransaction Transaction)
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

		#region Insert and Update

		public long Insert(BranchTransferDetails Details)
		{
			try 
			{
                ERPConfig clsERPConfig = new ERPConfig(mConnection, mTransaction);
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
				  
				MySqlConnection cn = GetConnection();
	 			
				MySqlCommand cmd = new MySqlCommand();
				cmd.Connection = cn;
				cmd.Transaction = mTransaction;
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

				MySqlParameter prmTransferrerID = new MySqlParameter("@TransferrerID",System.Data.DbType.Int64);			
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

				cmd.ExecuteNonQuery();

				SQL = "SELECT LAST_INSERT_ID();";
				
				cmd.Parameters.Clear(); 
				cmd.CommandText = SQL;
				
				MySqlDataReader myReader = (MySqlDataReader) cmd.ExecuteReader(System.Data.CommandBehavior.SingleResult);
				
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
		public void Update(BranchTransferDetails Details)
		{
			try 
			{
                ERPConfig clsERPConfig = new ERPConfig(mConnection, mTransaction);
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
				  
				MySqlConnection cn = GetConnection();
	 			
				MySqlCommand cmd = new MySqlCommand();
				cmd.Connection = cn;
				cmd.Transaction = mTransaction;
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

				MySqlParameter prmBranchTransferID = new MySqlParameter("@BranchTransferID",System.Data.DbType.Int64);			
				prmBranchTransferID.Value = Details.BranchTransferID;
				cmd.Parameters.Add(prmBranchTransferID);

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

        public void UpdateDiscount(long BranchTransferID, decimal DiscountApplied, DiscountTypes DiscountType)
        {
            try
            {
                string SQL = "UPDATE tblBranchTransfer SET " +
                                "DiscountApplied        =   @DiscountApplied, " +
                                "DiscountType           =   @DiscountType " +
                            "WHERE BranchTransferID = @BranchTransferID;";

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

                MySqlParameter prmBranchTransferID = new MySqlParameter("@BranchTransferID",MySqlDbType.Int64);
                prmBranchTransferID.Value = BranchTransferID;
                cmd.Parameters.Add(prmBranchTransferID);

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
        public void UpdateDiscountFreightDeposit(long BranchTransferID, decimal DiscountApplied, DiscountTypes DiscountType)
        {
            try
            {
                string SQL = "UPDATE tblBranchTransfer SET " +
                                "DiscountApplied        =   @DiscountApplied, " +
                                "DiscountType           =   @DiscountType " +
                            "WHERE BranchTransferID = @BranchTransferID;";

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

                MySqlParameter prmBranchTransferID = new MySqlParameter("@BranchTransferID",MySqlDbType.Int64);
                prmBranchTransferID.Value = BranchTransferID;
                cmd.Parameters.Add(prmBranchTransferID);

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
        public void UpdateFreight(long BranchTransferID, decimal Freight)
        {
            try
            {
                string SQL = "UPDATE tblBranchTransfer SET " +
                                "Freight           =   @Freight " +
                            "WHERE BranchTransferID = @BranchTransferID;";

                MySqlConnection cn = GetConnection();

                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = cn;
                cmd.Transaction = mTransaction;
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandText = SQL;

                MySqlParameter prmFreight = new MySqlParameter("@Freight", System.Data.DbType.Decimal);
                prmFreight.Value = Freight;
                cmd.Parameters.Add(prmFreight);

                MySqlParameter prmBranchTransferID = new MySqlParameter("@BranchTransferID",MySqlDbType.Int64);
                prmBranchTransferID.Value = BranchTransferID;
                cmd.Parameters.Add(prmBranchTransferID);

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
        public void UpdateDeposit(long BranchTransferID, decimal Deposit)
        {
            try
            {
                string SQL = "UPDATE tblBranchTransfer SET " +
                                "Deposit           =   @Deposit " +
                            "WHERE BranchTransferID = @BranchTransferID;";

                MySqlConnection cn = GetConnection();

                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = cn;
                cmd.Transaction = mTransaction;
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandText = SQL;

                MySqlParameter prmDeposit = new MySqlParameter("@Deposit", System.Data.DbType.Decimal);
                prmDeposit.Value = Deposit;
                cmd.Parameters.Add(prmDeposit);

                MySqlParameter prmBranchTransferID = new MySqlParameter("@BranchTransferID",MySqlDbType.Int64);
                prmBranchTransferID.Value = BranchTransferID;
                cmd.Parameters.Add(prmBranchTransferID);

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

		public void IssueGRN(long BranchTransferID, string ReceivedBy, DateTime DeliveryDate)
		{
			try 
			{
				string SQL=	"UPDATE tblBranchTransfer SET " + 
								"ReceivedBy			    =	@ReceivedBy, " +
								"DeliveryDate			=	@DeliveryDate, " +
								"Status				    =	@Status " +
							"WHERE BranchTransferID = @BranchTransferID;";
				  
				MySqlConnection cn = GetConnection();
	 			
				MySqlCommand cmd = new MySqlCommand();
				cmd.Connection = cn;
				cmd.Transaction = mTransaction;
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

				MySqlParameter prmBranchTransferID = new MySqlParameter("@BranchTransferID",System.Data.DbType.Int64);			
				prmBranchTransferID.Value = BranchTransferID;
				cmd.Parameters.Add(prmBranchTransferID);

				cmd.ExecuteNonQuery();

				/*******************************************
				 * Update the status of items
				 * ****************************************/
				BranchTransferItem clsBranchTransferItem = new BranchTransferItem(mConnection, mTransaction);
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

        private void UpdateAccounts(long BranchTransferID)
        {
            try
            {
                //BranchTransferDetails clsBranchTransferDetails = Details(BranchTransferID);
                //ChartOfAccount clsChartOfAccount = new ChartOfAccount(mConnection, mTransaction);

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

                //BranchTransferItem clsBranchTransferItem = new BranchTransferItem(mConnection, mTransaction);
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

        private void UpdateRID(long BranchTransferID)
        {
            try
            {
                //string SQL = "CALL procProductUpdateRIDByBranchTransfer(@lngBranchTransferID)";

                //MySqlConnection cn = GetConnection();

                //MySqlCommand cmd = new MySqlCommand();
                //cmd.Connection = cn;
                //cmd.Transaction = mTransaction;
                //cmd.CommandType = System.Data.CommandType.Text;
                //cmd.CommandText = SQL;

                //cmd.Parameters.AddWithValue("@lngBranchTransferID", BranchTransferID);

                //cmd.ExecuteNonQuery();

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
		private void AddItemToInventory(long BranchTransferID)
		{

			BranchTransferDetails clsBranchTransferDetails = Details(BranchTransferID);
            ERPConfig clsERPConfig = new ERPConfig(mConnection, mTransaction);
			ERPConfigDetails clsERPConfigDetails = clsERPConfig.Details();

			BranchTransferItem clsBranchTransferItem = new BranchTransferItem(mConnection, mTransaction);
            ProductUnit clsProductUnit = new ProductUnit(mConnection, mTransaction);
            Product clsProduct = new Product(mConnection, mTransaction);
            ProductVariationsMatrix clsProductVariationsMatrix = new ProductVariationsMatrix(mConnection, mTransaction);
            ProductPackage clsProductPackage = new ProductPackage(mConnection, mTransaction);
            MatrixPackage clsMatrixPackage = new MatrixPackage(mConnection, mTransaction);

			Inventory clsInventory = new Inventory(mConnection, mTransaction);
            InventoryDetails clsInventoryDetails;

			MySqlDataReader myReader = clsBranchTransferItem.List(BranchTransferID, "BranchTransferItemID", SortOption.Ascending);

			while (myReader.Read())
			{
				long lngProductID = myReader.GetInt64("ProductID");
				int intProductUnitID = myReader.GetInt16("ProductUnitID");

				decimal decItemQuantity = myReader.GetDecimal("Quantity");
                decimal decQuantity = clsProductUnit.GetBaseUnitValue(lngProductID, intProductUnitID, decItemQuantity);
				
				long lngVariationMatrixID = myReader.GetInt64("VariationMatrixID");
				string strMatrixDescription = "" + myReader["MatrixDescription"].ToString();
				string strProductCode = "" + myReader["ProductCode"].ToString();
                string strProductUnitCode = "" + myReader["ProductUnitCode"].ToString();
				decimal decUnitCost = myReader.GetDecimal("UnitCost");
				decimal decItemCost = myReader.GetDecimal("Amount");
				decimal decVAT = myReader.GetDecimal("VAT");

				/*******************************************
				 * Subtract BranchIDFrom then Add to BranchIDTo -- Inventory
				 * ****************************************/
                clsProduct.SubtractQuantity(clsBranchTransferDetails.BranchIDFrom, lngProductID, lngVariationMatrixID, decQuantity, Product.getPRODUCT_INVENTORY_MOVEMENT_VALUE(PRODUCT_INVENTORY_MOVEMENT.ADD_PURCHASE) + " @ " + decUnitCost.ToString("#,##0.#0") + "/" + strProductUnitCode, DateTime.Now, clsBranchTransferDetails.BranchTransferNo, clsBranchTransferDetails.TransferrerName);
                clsProduct.AddQuantity(clsBranchTransferDetails.BranchIDTo, lngProductID, lngVariationMatrixID, decQuantity, Product.getPRODUCT_INVENTORY_MOVEMENT_VALUE(PRODUCT_INVENTORY_MOVEMENT.ADD_PURCHASE) + " @ " + decUnitCost.ToString("#,##0.#0") + "/" + strProductUnitCode, DateTime.Now, clsBranchTransferDetails.BranchTransferNo, clsBranchTransferDetails.TransferrerName);

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
			myReader.Close();

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

				MySqlParameter prmStatus = new MySqlParameter("@Status",MySqlDbType.Int16);
				prmStatus.Value = BranchTransferStatus.Cancelled.ToString("d");
				cmd.Parameters.Add(prmStatus);

				MySqlParameter prmBranchTransferID = new MySqlParameter("@BranchTransferID",System.Data.DbType.Int64);			
				prmBranchTransferID.Value = BranchTransferID;
				cmd.Parameters.Add(prmBranchTransferID);

				cmd.ExecuteNonQuery();

				/*******************************************
				 * Update the status of items
				 * ****************************************/
				BranchTransferItem clsBranchTransferItem = new BranchTransferItem(mConnection, mTransaction);
				clsBranchTransferItem.Cancel(BranchTransferID);

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
        public bool UpdatePaymentStatus(BranchTransferPaymentStatus paymentStatus, string IDs)
        {
            try
            {
                string SQL = "UPDATE tblBranchTransfer SET PaymentStatus = @PaymentStatus WHERE BranchTransferID IN (" + IDs + ");";

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
        public bool UpdatePayment(long BranchTransferID, decimal PaidAmount, BranchTransferPaymentStatus paymentStatus)
        {
            try
            {
                string SQL = "UPDATE tblBranchTransfer SET " +
                                "PaidAmount     = PaidAmount + @PaidAmount, " +
                                "UnpaidAmount   = UnpaidAmount - @PaidAmount, " +
                                "PaymentStatus  = @PaymentStatus " +
                             "WHERE BranchTransferID = @BranchTransferID;";

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

                MySqlParameter prmBranchTransferID = new MySqlParameter("@BranchTransferID",MySqlDbType.Int64);
                prmBranchTransferID.Value = BranchTransferID;
                cmd.Parameters.Add(prmBranchTransferID);

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
				string SQL=	"DELETE FROM tblBranchTransfer WHERE BranchTransferID IN (" + IDs + ");";
				  
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
				  
				MySqlConnection cn = GetConnection();
	 			
				MySqlCommand cmd = new MySqlCommand();
				cmd.Connection = cn;
				cmd.Transaction = mTransaction;
				cmd.CommandType = System.Data.CommandType.Text;
				cmd.CommandText = SQL;

				MySqlParameter prmBranchTransferID = new MySqlParameter("@BranchTransferID",MySqlDbType.Int16);
				prmBranchTransferID.Value = BranchTransferID;
				cmd.Parameters.Add(prmBranchTransferID);

				MySqlDataReader myReader = (MySqlDataReader) cmd.ExecuteReader(System.Data.CommandBehavior.SingleResult);
				
				BranchTransferDetails Details = new BranchTransferDetails();

				while (myReader.Read()) 
				{
					Details.BranchTransferID = BranchTransferID;
					Details.BranchTransferNo = "" + myReader["BranchTransferNo"].ToString();
					Details.BranchTransferDate = myReader.GetDateTime("BranchTransferDate");
					Details.RequiredDeliveryDate = myReader.GetDateTime("RequiredDeliveryDate");
					Details.BranchIDFrom = myReader.GetInt16("BranchIDFrom");
                    Details.BranchCodeFrom = "" + myReader["BranchCodeFrom"].ToString();
                    Details.BranchNameFrom = "" + myReader["BranchNameFrom"].ToString();
                    Details.BranchAddressFrom = "" + myReader["BranchAddressFrom"].ToString();
                    Details.BranchIDTo = myReader.GetInt16("BranchIDTo");
                    Details.BranchCodeTo = "" + myReader["BranchCodeTo"].ToString();
                    Details.BranchNameTo = "" + myReader["BranchNameTo"].ToString();
                    Details.BranchAddressTo = "" + myReader["BranchAddressTo"].ToString();
					Details.TransferrerID = myReader.GetInt64("TransferrerID");
                    Details.TransferrerName = "" + myReader["TransferrerName"].ToString();
                    Details.RequestedBy = "" + myReader["RequestedBy"].ToString();
					Details.SubTotal = myReader.GetDecimal("SubTotal");
					Details.Discount = myReader.GetDecimal("Discount");
                    Details.DiscountApplied = myReader.GetDecimal("DiscountApplied");
                    Details.DiscountType = (DiscountTypes)Enum.Parse(typeof(DiscountTypes), myReader.GetString("DiscountType"));
					Details.VAT = myReader.GetDecimal("VAT");
					Details.VatableAmount = myReader.GetDecimal("VatableAmount");
                    Details.EVAT = myReader.GetDecimal("EVAT");
                    Details.EVatableAmount = myReader.GetDecimal("EVatableAmount");
                    Details.LocalTax = myReader.GetDecimal("LocalTax");
                    Details.Status = (BranchTransferStatus)Enum.Parse(typeof(BranchTransferStatus), myReader.GetString("Status"));
                    Details.Remarks = "" + myReader["Remarks"].ToString();
                    Details.ReceivedBy = "" + myReader["ReceivedBy"].ToString();
                    Details.DeliveryDate = myReader.GetDateTime("DeliveryDate");
                    Details.UnpaidAmount = myReader.GetDecimal("UnpaidAmount");
                    Details.PaidAmount = myReader.GetDecimal("PaidAmount");
                    Details.PaymentStatus = (BranchTransferPaymentStatus)Enum.Parse(typeof(BranchTransferPaymentStatus), myReader.GetString("PaymentStatus"));
                    Details.Freight = myReader.GetDecimal("Freight");
                    Details.Deposit = myReader.GetDecimal("Deposit");
                    Details.TotalItemDiscount = myReader.GetDecimal("TotalItemDiscount");
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

        public System.Data.DataTable ListAsDataTable(BranchTransferStatus branchtransferstatus, string SortField, SortOption SortOrder)
        {
            if (SortField == string.Empty || SortField == null) SortField = "BranchTransferID";

            string SQL = SQLSelect() + "WHERE Status = @Status ";

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

            cmd.Parameters.AddWithValue("@Status", branchtransferstatus.ToString("d"));

            System.Data.DataTable dt = new System.Data.DataTable("BranchTransfer");
            MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
            adapter.Fill(dt);

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

            MySqlConnection cn = GetConnection();

            MySqlCommand cmd = new MySqlCommand();
            cmd.Connection = cn;
            cmd.Transaction = mTransaction;
            cmd.CommandType = System.Data.CommandType.Text;
            cmd.CommandText = SQL;

            cmd.Parameters.AddWithValue("@Status", branchtransferstatus.ToString("d"));

            if (OrderStartDate != DateTime.MinValue) cmd.Parameters.AddWithValue("@OrderStartDate", OrderStartDate.ToString("yyyy-MM-dd HH:mm:ss"));
            if (OrderEndDate != DateTime.MinValue) cmd.Parameters.AddWithValue("@OrderEndDate", OrderEndDate.ToString("yyyy-MM-dd HH:mm:ss"));
            if (PostingStartDate != DateTime.MinValue) cmd.Parameters.AddWithValue("@PostingStartDate", PostingStartDate.ToString("yyyy-MM-dd HH:mm:ss"));
            if (PostingEndDate != DateTime.MinValue) cmd.Parameters.AddWithValue("@PostingEndDate", PostingEndDate.ToString("yyyy-MM-dd HH:mm:ss"));

            System.Data.DataTable dt = new System.Data.DataTable("BranchTransfer");
            MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
            adapter.Fill(dt);

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

            MySqlConnection cn = GetConnection();

            MySqlCommand cmd = new MySqlCommand();
            cmd.Connection = cn;
            cmd.Transaction = mTransaction;
            cmd.CommandType = System.Data.CommandType.Text;
            cmd.CommandText = SQL;

            System.Data.DataTable dt = new System.Data.DataTable("BranchTransfer");
            MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
            adapter.Fill(dt);

            return dt;
		}

		public MySqlDataReader List(long BranchTransferID, string SortField, SortOption SortOrder)
		{
			try
			{
                if (SortField == string.Empty || SortField == null) SortField = "BranchTransferID";

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

		public MySqlDataReader List(string SortField, SortOption SortOrder)
		{
			try
			{
                if (SortField == string.Empty || SortField == null) SortField = "BranchTransferID";

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

				MySqlConnection cn = GetConnection();

				MySqlCommand cmd = new MySqlCommand();
				cmd.Connection = cn;
				cmd.Transaction = mTransaction;
				cmd.CommandType = System.Data.CommandType.Text;
				cmd.CommandText = SQL;
				
				MySqlParameter prmStatus = new MySqlParameter("@Status",MySqlDbType.Int16);			
				prmStatus.Value = branchtransferstatus.ToString("d");
				cmd.Parameters.Add(prmStatus);

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

				MySqlConnection cn = GetConnection();

				MySqlCommand cmd = new MySqlCommand();
				cmd.Connection = cn;
				cmd.Transaction = mTransaction;
				cmd.CommandType = System.Data.CommandType.Text;
				cmd.CommandText = SQL;
				
				MySqlParameter prmStatus = new MySqlParameter("@Status",MySqlDbType.Int16);			
				prmStatus.Value = branchtransferstatus.ToString("d");
				cmd.Parameters.Add(prmStatus);

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

                MySqlConnection cn = GetConnection();

                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = cn;
                cmd.Transaction = mTransaction;
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
                if (SortField == string.Empty || SortField == null) SortField = "BranchTransferID";

				string SQL = SQLSelect() + "WHERE (BranchTransferNo LIKE @SearchKey or BranchTransferDate LIKE @SearchKey or SupplierCode LIKE @SearchKey " +
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

				MySqlConnection cn = GetConnection();

				MySqlCommand cmd = new MySqlCommand();
				cmd.Connection = cn;
				cmd.Transaction = mTransaction;
				cmd.CommandType = System.Data.CommandType.Text;
				cmd.CommandText = SQL;
				
				MySqlParameter prmStatus = new MySqlParameter("@Status",MySqlDbType.Int16);			
				prmStatus.Value = branchtransferstatus.ToString("d");
				cmd.Parameters.Add(prmStatus);

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

                MySqlConnection cn = GetConnection();

                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = cn;
                cmd.Transaction = mTransaction;
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandText = SQL;

                MySqlParameter prmStatus = new MySqlParameter("@Status",MySqlDbType.Int16);
                prmStatus.Value = branchtransferstatus.ToString("d");
                cmd.Parameters.Add(prmStatus);

                MySqlParameter prmSearchKey = new MySqlParameter("@SearchKey",MySqlDbType.String);
                prmSearchKey.Value = "%" + SearchKey + "%";
                cmd.Parameters.Add(prmSearchKey);

                System.Data.DataTable dt = new System.Data.DataTable("BranchTransfer");
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

                MySqlConnection cn = GetConnection();

                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = cn;
                cmd.Transaction = mTransaction;
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandText = SQL;

                cmd.Parameters.AddWithValue("@Status", branchtransferstatus.ToString("d"));
                if (SearchKey != string.Empty || SearchKey != null) cmd.Parameters.AddWithValue("@SearchKey", "%" + SearchKey + "%");

                if (OrderStartDate != DateTime.MinValue) cmd.Parameters.AddWithValue("@OrderStartDate", OrderStartDate.ToString("yyyy-MM-dd HH:mm:ss"));
                if (OrderEndDate != DateTime.MinValue) cmd.Parameters.AddWithValue("@OrderEndDate", OrderEndDate.ToString("yyyy-MM-dd HH:mm:ss"));
                if (PostingStartDate != DateTime.MinValue) cmd.Parameters.AddWithValue("@PostingStartDate", PostingStartDate.ToString("yyyy-MM-dd HH:mm:ss"));
                if (PostingEndDate != DateTime.MinValue) cmd.Parameters.AddWithValue("@PostingEndDate", PostingEndDate.ToString("yyyy-MM-dd HH:mm:ss"));

                System.Data.DataTable dt = new System.Data.DataTable("BranchTransfer");
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

        public MySqlDataReader List(BranchTransferStatus branchtransferstatus, DateTime StartDate, DateTime EndDate)
		{
			try
			{
				string SQL = SQLSelect() + "WHERE Status = @Status AND DeliveryDate BETWEEN @StartDate AND @EndDate ORDER BY BranchTransferID ASC";

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
				prmStatus.Value = branchtransferstatus.ToString("d");
				cmd.Parameters.Add(prmStatus);

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
        public MySqlDataReader List(BranchTransferStatus branchtransferstatus, long SupplierID, DateTime StartDate, DateTime EndDate)
        {
            try
            {
                string SQL = SQLSelect() + "WHERE Status = @Status AND SupplierID = @SupplierID AND DeliveryDate BETWEEN @StartDate AND @EndDate ORDER BY BranchTransferID ASC";

                MySqlConnection cn = GetConnection();

                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = cn;
                cmd.Transaction = mTransaction;
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandText = SQL;

                cmd.Parameters.AddWithValue("@Status", branchtransferstatus.ToString("d"));
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
		
		#endregion

		#region Public Modifiers

		public string LastTransactionNo()
		{
			try
			{
				string stRetValue = String.Empty;
				
				ERPConfig clsERPConfig = new ERPConfig(Connection, Transaction);
				stRetValue = clsERPConfig.get_LastBranchTransferNo();

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
        public void SynchronizeAmount(long BranchTransferID)
        {
            try
            {
                string SQL = "CALL procBranchTransferSynchronizeAmount(@BranchTransferID);";

                MySqlConnection cn = GetConnection();

                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = cn;
                cmd.Transaction = mTransaction;
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandText = SQL;

                MySqlParameter prmBranchTransferID = new MySqlParameter("@BranchTransferID",MySqlDbType.Int64);
                prmBranchTransferID.Value = BranchTransferID;
                cmd.Parameters.Add(prmBranchTransferID);

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


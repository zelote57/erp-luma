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

	#region SODetails

	public struct SODetails
	{
		public long SOID;
		public string SONo;
		public DateTime SODate;
		public long CustomerID;
		public string CustomerCode;
		public string CustomerContact;
		public string CustomerAddress;
		public string CustomerTelephoneNo;
		public int CustomerModeOfTerms;
		public int CustomerTerms;
		public DateTime RequiredDeliveryDate;
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
		public SOStatus Status;
		public string Remarks;
		public string CustomerDRNo;
		public DateTime DeliveryDate;
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
	public class SO : POSConnection
	{
		#region Constructors and Destructors

		public SO()
            : base(null, null)
        {
        }

        public SO(MySqlConnection Connection, MySqlTransaction Transaction) 
            : base(Connection, Transaction)
		{

		}

		#endregion

		#region Insert and Update

		public long Insert(SODetails Details)
		{
			try
			{
                ERPConfig clsERPConfig = new ERPConfig(base.Connection, base.Transaction);
				ARLinkConfigDetails clsARLinkConfigDetails = clsERPConfig.ARLinkDetails();

				string SQL = "INSERT INTO tblSO (" +
								"SONo, " +
								"SODate, " +
								"CustomerID, " +
								"CustomerCode, " +
								"CustomerContact, " +
								"CustomerAddress, " +
								"CustomerTelephoneNo, " +
								"CustomerModeOfTerms, " +
								"CustomerTerms, " +
								"RequiredDeliveryDate, " +
								"BranchID, " +
								"SellerID, " +
								"SellerName, " +
								"Status, " +
								"Remarks, " +
								"ChartOfAccountIDARTracking, " +
								"ChartOfAccountIDARBills, " +
								"ChartOfAccountIDARFreight, " +
								"ChartOfAccountIDARVDeposit, " +
								"ChartOfAccountIDARContra, " +
								"ChartOfAccountIDARLatePayment" +
							") VALUES (" +
								"@SONo, " +
								"@SODate, " +
								"@CustomerID, " +
								"@CustomerCode, " +
								"@CustomerContact, " +
								"@CustomerAddress, " +
								"@CustomerTelephoneNo, " +
								"@CustomerModeOfTerms, " +
								"@CustomerTerms, " +
								"@RequiredDeliveryDate, " +
								"@BranchID, " +
								"@SellerID, " +
								"@SellerName, " +
								"@Status, " +
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

				MySqlParameter prmSONo = new MySqlParameter("@SONo",MySqlDbType.String);
				prmSONo.Value = Details.SONo;
				cmd.Parameters.Add(prmSONo);

				MySqlParameter prmSODate = new MySqlParameter("@SODate",MySqlDbType.DateTime);
				prmSODate.Value = Details.SODate.ToString("yyyy-MM-dd HH:mm:ss");
				cmd.Parameters.Add(prmSODate);

				MySqlParameter prmCustomerID = new MySqlParameter("@CustomerID",MySqlDbType.Int64);
				prmCustomerID.Value = Details.CustomerID;
				cmd.Parameters.Add(prmCustomerID);

				MySqlParameter prmCustomerCode = new MySqlParameter("@CustomerCode",MySqlDbType.String);
				prmCustomerCode.Value = Details.CustomerCode;
				cmd.Parameters.Add(prmCustomerCode);

				MySqlParameter prmCustomerContact = new MySqlParameter("@CustomerContact",MySqlDbType.String);
				prmCustomerContact.Value = Details.CustomerContact;
				cmd.Parameters.Add(prmCustomerContact);

				MySqlParameter prmCustomerAddress = new MySqlParameter("@CustomerAddress",MySqlDbType.String);
				prmCustomerAddress.Value = Details.CustomerAddress;
				cmd.Parameters.Add(prmCustomerAddress);

				MySqlParameter prmCustomerTelephoneNo = new MySqlParameter("@CustomerTelephoneNo",MySqlDbType.String);
				prmCustomerTelephoneNo.Value = Details.CustomerTelephoneNo;
				cmd.Parameters.Add(prmCustomerTelephoneNo);

				MySqlParameter prmCustomerModeOfTerms = new MySqlParameter("@CustomerModeOfTerms",MySqlDbType.Int16);
				prmCustomerModeOfTerms.Value = Details.CustomerModeOfTerms;
				cmd.Parameters.Add(prmCustomerModeOfTerms);

				MySqlParameter prmCustomerTerms = new MySqlParameter("@CustomerTerms",MySqlDbType.Int16);
				prmCustomerTerms.Value = Details.CustomerTerms;
				cmd.Parameters.Add(prmCustomerTerms);

				MySqlParameter prmRequiredDeliveryDate = new MySqlParameter("@RequiredDeliveryDate",MySqlDbType.DateTime);
				prmRequiredDeliveryDate.Value = Details.RequiredDeliveryDate.ToString("yyyy-MM-dd HH:mm:ss");
				cmd.Parameters.Add(prmRequiredDeliveryDate);

				MySqlParameter prmBranchID = new MySqlParameter("@BranchID",MySqlDbType.Int16);
				prmBranchID.Value = Details.BranchID;
				cmd.Parameters.Add(prmBranchID);

				MySqlParameter prmSellerID = new MySqlParameter("@SellerID",MySqlDbType.Int64);
				prmSellerID.Value = Details.SellerID;
				cmd.Parameters.Add(prmSellerID);

				MySqlParameter prmSellerName = new MySqlParameter("@SellerName",MySqlDbType.String);
				prmSellerName.Value = Details.SellerName;
				cmd.Parameters.Add(prmSellerName);

				MySqlParameter prmStatus = new MySqlParameter("@Status",MySqlDbType.Int16);
				prmStatus.Value = Details.Status.ToString("d");
				cmd.Parameters.Add(prmStatus);

				MySqlParameter prmRemarks = new MySqlParameter("@Remarks",MySqlDbType.String);
				prmRemarks.Value = Details.Remarks;
				cmd.Parameters.Add(prmRemarks);

				MySqlParameter prmChartOfAccountIDARTracking = new MySqlParameter("@ChartOfAccountIDARTracking",MySqlDbType.Int32);
				prmChartOfAccountIDARTracking.Value = clsARLinkConfigDetails.ChartOfAccountIDARTracking;
				cmd.Parameters.Add(prmChartOfAccountIDARTracking);

				MySqlParameter prmChartOfAccountIDARBills = new MySqlParameter("@ChartOfAccountIDARBills",MySqlDbType.Int32);
				prmChartOfAccountIDARBills.Value = clsARLinkConfigDetails.ChartOfAccountIDARBills;
				cmd.Parameters.Add(prmChartOfAccountIDARBills);

				MySqlParameter prmChartOfAccountIDARFreight = new MySqlParameter("@ChartOfAccountIDARFreight",MySqlDbType.Int32);
				prmChartOfAccountIDARFreight.Value = clsARLinkConfigDetails.ChartOfAccountIDARFreight;
				cmd.Parameters.Add(prmChartOfAccountIDARFreight);

				MySqlParameter prmChartOfAccountIDARVDeposit = new MySqlParameter("@ChartOfAccountIDARVDeposit",MySqlDbType.Int32);
				prmChartOfAccountIDARVDeposit.Value = clsARLinkConfigDetails.ChartOfAccountIDARVDeposit;
				cmd.Parameters.Add(prmChartOfAccountIDARVDeposit);

				MySqlParameter prmChartOfAccountIDARContra = new MySqlParameter("@ChartOfAccountIDARContra",MySqlDbType.Int32);
				prmChartOfAccountIDARContra.Value = clsARLinkConfigDetails.ChartOfAccountIDARContra;
				cmd.Parameters.Add(prmChartOfAccountIDARContra);

				MySqlParameter prmChartOfAccountIDARLatePayment = new MySqlParameter("@ChartOfAccountIDARLatePayment",MySqlDbType.Int32);
				prmChartOfAccountIDARLatePayment.Value = clsARLinkConfigDetails.ChartOfAccountIDARLatePayment;
				cmd.Parameters.Add(prmChartOfAccountIDARLatePayment);

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
		public void Update(SODetails Details)
		{
			try
			{
                ERPConfig clsERPConfig = new ERPConfig(base.Connection, base.Transaction);
				ARLinkConfigDetails clsARLinkConfigDetails = clsERPConfig.ARLinkDetails();

				string SQL = "UPDATE tblSO SET " +
								"SONo					=	@SONo, " +
								"SODate					=	@SODate, " +
								"CustomerID				=	@CustomerID, " +
								"CustomerCode			=	@CustomerCode, " +
								"CustomerContact		=	@CustomerContact, " +
								"CustomerAddress		=	@CustomerAddress, " +
								"CustomerTelephoneNo	=	@CustomerTelephoneNo, " +
								"CustomerModeOfTerms	=	@CustomerModeOfTerms, " +
								"CustomerTerms			=	@CustomerTerms, " +
								"RequiredDeliveryDate	=	@RequiredDeliveryDate, " +
								"BranchID				=	@BranchID, " +
								"SellerID			    =	@SellerID, " +
								"SellerName             =   @SellerName, " +
								"Remarks                =   @Remarks, " +
								"ChartOfAccountIDARTracking     = @ChartOfAccountIDARTracking, " +
								"ChartOfAccountIDARBills        = @ChartOfAccountIDARBills, " +
								"ChartOfAccountIDARFreight      = @ChartOfAccountIDARFreight, " +
								"ChartOfAccountIDARVDeposit     = @ChartOfAccountIDARVDeposit, " +
								"ChartOfAccountIDARContra       = @ChartOfAccountIDARContra, " +
								"ChartOfAccountIDARLatePayment  = @ChartOfAccountIDARLatePayment " +
							"WHERE SOID = @SOID;";

                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandText = SQL;

				MySqlParameter prmSONo = new MySqlParameter("@SONo",MySqlDbType.String);
				prmSONo.Value = Details.SONo;
				cmd.Parameters.Add(prmSONo);

				MySqlParameter prmSODate = new MySqlParameter("@SODate",MySqlDbType.DateTime);
				prmSODate.Value = Details.SODate.ToString("yyyy-MM-dd HH:mm:ss");
				cmd.Parameters.Add(prmSODate);

				MySqlParameter prmCustomerID = new MySqlParameter("@CustomerID",MySqlDbType.Int64);
				prmCustomerID.Value = Details.CustomerID;
				cmd.Parameters.Add(prmCustomerID);

				MySqlParameter prmCustomerCode = new MySqlParameter("@CustomerCode",MySqlDbType.String);
				prmCustomerCode.Value = Details.CustomerCode;
				cmd.Parameters.Add(prmCustomerCode);

				MySqlParameter prmCustomerContact = new MySqlParameter("@CustomerContact",MySqlDbType.String);
				prmCustomerContact.Value = Details.CustomerContact;
				cmd.Parameters.Add(prmCustomerContact);

				MySqlParameter prmCustomerAddress = new MySqlParameter("@CustomerAddress",MySqlDbType.String);
				prmCustomerAddress.Value = Details.CustomerAddress;
				cmd.Parameters.Add(prmCustomerAddress);

				MySqlParameter prmCustomerTelephoneNo = new MySqlParameter("@CustomerTelephoneNo",MySqlDbType.String);
				prmCustomerTelephoneNo.Value = Details.CustomerTelephoneNo;
				cmd.Parameters.Add(prmCustomerTelephoneNo);

				MySqlParameter prmCustomerModeOfTerms = new MySqlParameter("@CustomerModeOfTerms",MySqlDbType.Int16);
				prmCustomerModeOfTerms.Value = Details.CustomerModeOfTerms;
				cmd.Parameters.Add(prmCustomerModeOfTerms);

				MySqlParameter prmCustomerTerms = new MySqlParameter("@CustomerTerms",MySqlDbType.Int16);
				prmCustomerTerms.Value = Details.CustomerTerms;
				cmd.Parameters.Add(prmCustomerTerms);

				MySqlParameter prmRequiredDeliveryDate = new MySqlParameter("@RequiredDeliveryDate",MySqlDbType.DateTime);
				prmRequiredDeliveryDate.Value = Details.RequiredDeliveryDate.ToString("yyyy-MM-dd HH:mm:ss");
				cmd.Parameters.Add(prmRequiredDeliveryDate);

				MySqlParameter prmBranchID = new MySqlParameter("@BranchID",MySqlDbType.Int16);
				prmBranchID.Value = Details.BranchID;
				cmd.Parameters.Add(prmBranchID);

				MySqlParameter prmSellerID = new MySqlParameter("@SellerID",MySqlDbType.Int64);
				prmSellerID.Value = Details.SellerID;
				cmd.Parameters.Add(prmSellerID);

				MySqlParameter prmSellerName = new MySqlParameter("@SellerName",MySqlDbType.String);
				prmSellerName.Value = Details.SellerName;
				cmd.Parameters.Add(prmSellerName);

				MySqlParameter prmRemarks = new MySqlParameter("@Remarks",MySqlDbType.String);
				prmRemarks.Value = Details.Remarks;
				cmd.Parameters.Add(prmRemarks);

				MySqlParameter prmChartOfAccountIDARTracking = new MySqlParameter("@ChartOfAccountIDARTracking",MySqlDbType.Int32);
				prmChartOfAccountIDARTracking.Value = clsARLinkConfigDetails.ChartOfAccountIDARTracking;
				cmd.Parameters.Add(prmChartOfAccountIDARTracking);

				MySqlParameter prmChartOfAccountIDARBills = new MySqlParameter("@ChartOfAccountIDARBills",MySqlDbType.Int32);
				prmChartOfAccountIDARBills.Value = clsARLinkConfigDetails.ChartOfAccountIDARBills;
				cmd.Parameters.Add(prmChartOfAccountIDARBills);

				MySqlParameter prmChartOfAccountIDARFreight = new MySqlParameter("@ChartOfAccountIDARFreight",MySqlDbType.Int32);
				prmChartOfAccountIDARFreight.Value = clsARLinkConfigDetails.ChartOfAccountIDARFreight;
				cmd.Parameters.Add(prmChartOfAccountIDARFreight);

				MySqlParameter prmChartOfAccountIDARVDeposit = new MySqlParameter("@ChartOfAccountIDARVDeposit",MySqlDbType.Int32);
				prmChartOfAccountIDARVDeposit.Value = clsARLinkConfigDetails.ChartOfAccountIDARVDeposit;
				cmd.Parameters.Add(prmChartOfAccountIDARVDeposit);

				MySqlParameter prmChartOfAccountIDARContra = new MySqlParameter("@ChartOfAccountIDARContra",MySqlDbType.Int32);
				prmChartOfAccountIDARContra.Value = clsARLinkConfigDetails.ChartOfAccountIDARContra;
				cmd.Parameters.Add(prmChartOfAccountIDARContra);

				MySqlParameter prmChartOfAccountIDARLatePayment = new MySqlParameter("@ChartOfAccountIDARLatePayment",MySqlDbType.Int32);
				prmChartOfAccountIDARLatePayment.Value = clsARLinkConfigDetails.ChartOfAccountIDARLatePayment;
				cmd.Parameters.Add(prmChartOfAccountIDARLatePayment);

				MySqlParameter prmSOID = new MySqlParameter("@SOID",MySqlDbType.Int64);
				prmSOID.Value = Details.SOID;
				cmd.Parameters.Add(prmSOID);

                base.ExecuteNonQuery(cmd);
			}

			catch (Exception ex)
			{
                throw base.ThrowException(ex);
			}
		}

		public void UpdateDiscount(long SOID, decimal DiscountApplied, DiscountTypes DiscountType)
		{
			try
			{
				string SQL = "UPDATE tblSO SET " +
								"DiscountApplied        =   @DiscountApplied, " +
								"DiscountType           =   @DiscountType " +
							"WHERE SOID = @SOID;";

				

                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandText = SQL;

				MySqlParameter prmDiscountApplied = new MySqlParameter("@DiscountApplied",MySqlDbType.Decimal);
				prmDiscountApplied.Value = DiscountApplied;
				cmd.Parameters.Add(prmDiscountApplied);

				MySqlParameter prmDiscountType = new MySqlParameter("@DiscountType",MySqlDbType.Int16);
				prmDiscountType.Value = Convert.ToInt16(DiscountType.ToString("d"));
				cmd.Parameters.Add(prmDiscountType);

				MySqlParameter prmSOID = new MySqlParameter("@SOID",MySqlDbType.Int64);
				prmSOID.Value = SOID;
				cmd.Parameters.Add(prmSOID);

                base.ExecuteNonQuery(cmd);
			}

			catch (Exception ex)
			{
                throw base.ThrowException(ex);
			}
		}
		public void UpdateDiscountFreightDeposit(long SOID, decimal DiscountApplied, DiscountTypes DiscountType)
		{
			try
			{
				string SQL = "UPDATE tblSO SET " +
								"DiscountApplied        =   @DiscountApplied, " +
								"DiscountType           =   @DiscountType " +
							"WHERE SOID = @SOID;";

                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandText = SQL;

				MySqlParameter prmDiscountApplied = new MySqlParameter("@DiscountApplied",MySqlDbType.Decimal);
				prmDiscountApplied.Value = DiscountApplied;
				cmd.Parameters.Add(prmDiscountApplied);

				MySqlParameter prmDiscountType = new MySqlParameter("@DiscountType",MySqlDbType.Int16);
				prmDiscountType.Value = Convert.ToInt16(DiscountType.ToString("d"));
				cmd.Parameters.Add(prmDiscountType);

				MySqlParameter prmSOID = new MySqlParameter("@SOID",MySqlDbType.Int64);
				prmSOID.Value = SOID;
				cmd.Parameters.Add(prmSOID);

                base.ExecuteNonQuery(cmd);
			}

			catch (Exception ex)
			{
                throw base.ThrowException(ex);
			}
		}
		public void UpdateFreight(long SOID, decimal Freight)
		{
			try
			{
				string SQL = "CALL procSOUpdateFreight(@SOID, @Freight);";

                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandText = SQL;

				cmd.Parameters.AddWithValue("@SOID", SOID);
				cmd.Parameters.AddWithValue("@Freight", Freight);

                base.ExecuteNonQuery(cmd);
			}

			catch (Exception ex)
			{
                throw base.ThrowException(ex);
			}
		}
		public void UpdateDeposit(long SOID, decimal Deposit)
		{
			try
			{
				string SQL = "UPDATE tblSO SET " +
								"Deposit           =   @Deposit " +
							"WHERE SOID = @SOID;";

                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandText = SQL;

				MySqlParameter prmDeposit = new MySqlParameter("@Deposit",MySqlDbType.Decimal);
				prmDeposit.Value = Deposit;
				cmd.Parameters.Add(prmDeposit);

				MySqlParameter prmSOID = new MySqlParameter("@SOID",MySqlDbType.Int64);
				prmSOID.Value = SOID;
				cmd.Parameters.Add(prmSOID);

				base.ExecuteNonQuery(cmd);
			}

			catch (Exception ex)
			{
				throw base.ThrowException(ex);
			}
		}

		public void IssueGRN(long SOID, string CustomerDRNo, DateTime DeliveryDate)
		{
			try
			{
				string SQL = "UPDATE tblSO SET " +
								"CustomerDRNo			=	@CustomerDRNo, " +
								"DeliveryDate			=	@DeliveryDate, " +
								"Status				    =	@Status " +
							"WHERE SOID = @SOID;";

                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandText = SQL;

				cmd.Parameters.AddWithValue("@CustomerDRNo", CustomerDRNo);
				cmd.Parameters.AddWithValue("@DeliveryDate", DeliveryDate.ToString("yyyy-MM-dd HH:mm:ss"));
				cmd.Parameters.AddWithValue("@Status", SOStatus.Posted.ToString("d"));
				cmd.Parameters.AddWithValue("@SOID", SOID);

				base.ExecuteNonQuery(cmd);

				/*******************************************
				 * Update the status of items
				 * ****************************************/
				SOItem clsSOItem = new SOItem(base.Connection, base.Transaction);
				clsSOItem.Post(SOID);

				/*******************************************
				 * Update Vendor Account
				 * ****************************************/
				SubtractItemToInventory(SOID);

				/*******************************************
				 * Update Account Balance
				 * ****************************************/
				UpdateAccounts(SOID);

			}

			catch (Exception ex)
			{
				throw base.ThrowException(ex);
			}
		}

		private void UpdateAccounts(long SOID)
		{
			try
			{
				SODetails clsSODetails = Details(SOID);
				ChartOfAccounts clsChartOfAccount = new ChartOfAccounts(base.Connection, base.Transaction);

				// update ChartOfAccountIDARTracking as credit
				clsChartOfAccount.UpdateCredit(clsSODetails.ChartOfAccountIDARTracking, clsSODetails.SubTotal);

				// update Deposit & APContra
				clsChartOfAccount.UpdateCredit(clsSODetails.ChartOfAccountIDARContra, clsSODetails.Discount);

				// update Freight & APTracking
				clsChartOfAccount.UpdateCredit(clsSODetails.ChartOfAccountIDARTracking, clsSODetails.Freight);
				clsChartOfAccount.UpdateDebit(clsSODetails.ChartOfAccountIDARFreight, clsSODetails.Freight);

				// update Deposit & APTracking
				clsChartOfAccount.UpdateCredit(clsSODetails.ChartOfAccountIDARTracking, clsSODetails.Deposit);
				clsChartOfAccount.UpdateDebit(clsSODetails.ChartOfAccountIDARVDeposit, clsSODetails.Deposit);

				SOItem clsSOItem = new SOItem(base.Connection, base.Transaction);
                System.Data.DataTable dt = clsSOItem.ListAsDataTable(SOID, "SOItemID", SortOption.Ascending);
                foreach (System.Data.DataRow dr in dt.Rows)
				{
					int iChartOfAccountIDSold = Int16.Parse(dr["ChartOfAccountIDSold"].ToString());
                    int iChartOfAccountIDTaxSold = Int16.Parse(dr["ChartOfAccountIDTaxSold"].ToString());

					decimal decVAT = decimal.Parse(dr["VAT"].ToString());
                    decimal decVATABLEAmount = decimal.Parse(dr["Amount"].ToString()) - decVAT;

					// update purchase as debit
					clsChartOfAccount.UpdateDebit(iChartOfAccountIDSold, decVATABLEAmount);
					// update tax as debit
					clsChartOfAccount.UpdateDebit(iChartOfAccountIDTaxSold, decVAT);
				}

			}

			catch (Exception ex)
			{
				base.ThrowException(ex);
			}
		}

		private void SubtractItemToInventory(long SOID)
		{

			SODetails clsSODetails = Details(SOID);
            ERPConfig clsERPConfig = new ERPConfig(base.Connection, base.Transaction);
			ERPConfigDetails clsERPConfigDetails = clsERPConfig.Details();

            SOItem clsSOItem = new SOItem(base.Connection, base.Transaction);
            ProductUnit clsProductUnit = new ProductUnit(base.Connection, base.Transaction);
            Products clsProduct = new Products(base.Connection, base.Transaction);
            ProductVariationsMatrix clsProductVariationsMatrix = new ProductVariationsMatrix(base.Connection, base.Transaction);
            ProductPackage clsProductPackage = new ProductPackage(base.Connection, base.Transaction);
            MatrixPackage clsMatrixPackage = new MatrixPackage(base.Connection, base.Transaction);

            Inventory clsInventory = new Inventory(base.Connection, base.Transaction);
			InventoryDetails clsInventoryDetails;

            //MatrixPackagePriceHistoryDetails clsMatrixPackagePriceHistoryDetails;
            //ProductPackagePriceHistoryDetails clsProductPackagePriceHistoryDetails;

            System.Data.DataTable dt = clsSOItem.ListAsDataTable(SOID, "SOItemID", SortOption.Ascending);
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

                ///*******************************************
                // * Add in the Sales Price History
                // * ****************************************/
                //if (lngVariationMatrixID != 0)
                //{
                //    // Update MatrixPackagePriceHistory first to get the history
                //    clsMatrixPackagePriceHistoryDetails = new MatrixPackagePriceHistoryDetails();
                //    clsMatrixPackagePriceHistoryDetails.UID = clsSODetails.SellerID;
                //    clsMatrixPackagePriceHistoryDetails.PackageID = clsMatrixPackage.GetPackageID(lngVariationMatrixID, intProductUnitID);
                //    clsMatrixPackagePriceHistoryDetails.ChangeDate = DateTime.Now;
                //    clsMatrixPackagePriceHistoryDetails.Price = decUnitCost;
                //    clsMatrixPackagePriceHistoryDetails.PurchasePrice = -1; //-1 = nochange
                //    clsMatrixPackagePriceHistoryDetails.VAT = -1; //-1 = nochange
                //    clsMatrixPackagePriceHistoryDetails.EVAT = -1; //-1 = nochange
                //    clsMatrixPackagePriceHistoryDetails.LocalTax = -1; //-1 = nochange
                //    clsMatrixPackagePriceHistoryDetails.Remarks = "Based on SO #: " + clsSODetails.SONo;
                //    MatrixPackagePriceHistory clsMatrixPackagePriceHistory = new MatrixPackagePriceHistory(base.Connection, base.Transaction);
                //    clsMatrixPackagePriceHistory.Insert(clsMatrixPackagePriceHistoryDetails);
                //}
                //else
                //{
                //    // Update ProductPackagePriceHistory first to get the history
                //    clsProductPackagePriceHistoryDetails = new ProductPackagePriceHistoryDetails();
                //    clsProductPackagePriceHistoryDetails.UID = clsSODetails.SellerID;
                //    clsProductPackagePriceHistoryDetails.PackageID = clsProductPackage.GetPackageID(lngProductID, intProductUnitID);
                //    clsProductPackagePriceHistoryDetails.ChangeDate = DateTime.Now;
                //    clsProductPackagePriceHistoryDetails.Price = decUnitCost;
                //    clsProductPackagePriceHistoryDetails.PurchasePrice = -1; //-1 = nochange
                //    clsProductPackagePriceHistoryDetails.VAT = -1; //-1 = nochange
                //    clsProductPackagePriceHistoryDetails.EVAT = -1; //-1 = nochange
                //    clsProductPackagePriceHistoryDetails.LocalTax = -1; //-1 = nochange
                //    clsProductPackagePriceHistoryDetails.Remarks = "Based on SO #: " + clsSODetails.SONo;
                //    ProductPackagePriceHistory clsProductPackagePriceHistory = new ProductPackagePriceHistory(base.Connection, base.Transaction);
                //    clsProductPackagePriceHistory.Insert(clsProductPackagePriceHistoryDetails);
                //}


				/*******************************************
				 * Subtract to Inventory
				 * ****************************************/
				// clsProduct.SubtractQuantity(lngProductID, decQuantity);
				// if (lngVariationMatrixID != 0) { clsProductVariationsMatrix.SubtractQuantity(lngVariationMatrixID, decQuantity); }
				// July 28, 2011: change the above codes to the following
				clsProduct.SubtractQuantity(clsSODetails.BranchID, lngProductID, lngVariationMatrixID, decQuantity, Products.getPRODUCT_INVENTORY_MOVEMENT_VALUE(PRODUCT_INVENTORY_MOVEMENT.DEDUCT_SOLD_WHOLESALE), DateTime.Now, clsSODetails.SONo, clsSODetails.SellerName);

				/*******************************************
				 * Update Selling Information
				 * ****************************************/
                int iBaseUnitID = clsProduct.get_BaseUnitID(lngProductID);
                if (iBaseUnitID != intProductUnitID)
                {
                    clsProduct.UpdateSellingPrice(lngProductID, lngVariationMatrixID, clsSODetails.CustomerID, intProductUnitID, (decItemQuantity * decUnitCost) / decQuantity); // Price should be the sugegsted selling price
                }
                clsProduct.UpdateSellingWSPrice(lngProductID, lngVariationMatrixID, clsSODetails.CustomerID, intProductUnitID, decUnitCost); // WS Price should be the unit cost


				/*******************************************
				 * Add to Inventory Analysis
				 * ****************************************/
				clsInventoryDetails = new InventoryDetails();
				clsInventoryDetails.PostingDateFrom = clsERPConfigDetails.PostingDateFrom;
				clsInventoryDetails.PostingDateTo = clsERPConfigDetails.PostingDateTo;
				clsInventoryDetails.PostingDate = clsSODetails.DeliveryDate;
				clsInventoryDetails.ReferenceNo = clsSODetails.SONo;
				clsInventoryDetails.ContactID = clsSODetails.CustomerID;
				clsInventoryDetails.ContactCode = clsSODetails.CustomerCode;
				clsInventoryDetails.ProductID = lngProductID;
				clsInventoryDetails.ProductCode = strProductCode;
				clsInventoryDetails.VariationMatrixID = lngVariationMatrixID;
				clsInventoryDetails.MatrixDescription = strMatrixDescription;
				clsInventoryDetails.SoldQuantity = decQuantity;
				clsInventoryDetails.SoldCost = decItemCost - decVAT;
				clsInventoryDetails.SoldVAT = decItemCost;	// Sales Cost with VAT

				clsInventory.Insert(clsInventoryDetails);
			}

		}
		public void Cancel(long SOID, DateTime CancelledDate, string Remarks, long CancelledByID)
		{
			try
			{
				string SQL = "UPDATE tblSO SET " +
								"CancelledDate			=	@CancelledDate, " +
								"CancelledRemarks		=	@CancelledRemarks, " +
								"CancelledByID			=	@CancelledByID, " +
								"Status				    =	@Status " +
							"WHERE SOID = @SOID;";

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
				prmStatus.Value = SOStatus.Cancelled.ToString("d");
				cmd.Parameters.Add(prmStatus);

				MySqlParameter prmSOID = new MySqlParameter("@SOID",MySqlDbType.Int64);
				prmSOID.Value = SOID;
				cmd.Parameters.Add(prmSOID);

				base.ExecuteNonQuery(cmd);

				/*******************************************
				 * Update the status of items
				 * ****************************************/
				SOItem clsSOItem = new SOItem(base.Connection, base.Transaction);
				clsSOItem.Cancel(SOID);

			}

			catch (Exception ex)
			{
				throw base.ThrowException(ex);
			}
		}
		public void GenerateItemsForReorder(Int32 TerminalID, long SOID)
		{
			try
			{
				GetConnection();

				Terminal clsTerminal = new Terminal(base.Connection, base.Transaction);
                TerminalDetails clsTerminalDetails = clsTerminal.Details(TerminalID);

				SODetails clsSODetails = Details(SOID);

				Products clsProduct = new Products(base.Connection, base.Transaction);
				System.Data.DataTable dt = clsProduct.ForReorder(clsSODetails.CustomerID);

				SOItem clsSOItem = new SOItem(base.Connection, base.Transaction);
				ProductVariationsMatrix clsProductVariationsMatrix = new ProductVariationsMatrix(base.Connection, base.Transaction);

				foreach (System.Data.DataRow dr in dt.Rows)
				{
					SOItemDetails clsDetails = new SOItemDetails();

					clsDetails.SOID = SOID;
					clsDetails.ProductID = Convert.ToInt64(dr["ProductID"]);
					clsDetails.ProductCode = dr["ProductCode"].ToString();
					clsDetails.BarCode = dr["BarCode"].ToString();
					clsDetails.Description = dr["ProductDesc"].ToString();
					clsDetails.ProductGroup = dr["ProductGroupCode"].ToString();
					clsDetails.ProductSubGroup = dr["ProductSubGroupCode"].ToString();
					clsDetails.ProductUnitID = Convert.ToInt32(dr["UnitID"]);
					clsDetails.ProductUnitCode = dr["UnitName"].ToString();
					clsDetails.Quantity = Convert.ToDecimal(dr["ReorderQty"]);
					clsDetails.UnitCost = Convert.ToDecimal(dr["Price"]);
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

					System.Data.DataTable dtmatrix = clsProductVariationsMatrix.ForReorder(clsDetails.ProductID, clsSODetails.CustomerID);
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
							clsSOItem.Insert(clsDetails);
						}
					else
					{
						clsDetails.VariationMatrixID = 0;
						clsDetails.MatrixDescription = string.Empty;
						clsSOItem.Insert(clsDetails);
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
		public bool UpdatePaymentStatus(SOPaymentStatus paymentStatus, string IDs)
		{
			try
			{
				string SQL = "UPDATE tblSO SET PaymentStatus = @PaymentStatus WHERE SOID IN (" + IDs + ");";

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
		public bool UpdatePayment(long SOID, decimal PaidAmount, SOPaymentStatus paymentStatus)
		{
			try
			{
				string SQL = "UPDATE tblSO SET " +
								"PaidAmount     = PaidAmount + @PaidAmount, " +
								"UnpaidAmount   = UnpaidAmount - @PaidAmount, " +
								"PaymentStatus  = @PaymentStatus " +
							 "WHERE SOID = @SOID;";

				MySqlCommand cmd = new MySqlCommand();
				cmd.CommandType = System.Data.CommandType.Text;
				cmd.CommandText = SQL;

				MySqlParameter prmPaidAmount = new MySqlParameter("@PaidAmount",MySqlDbType.Decimal);
				prmPaidAmount.Value = PaidAmount;
				cmd.Parameters.Add(prmPaidAmount);

				MySqlParameter prmPaymentStatus = new MySqlParameter("@PaymentStatus",MySqlDbType.Int16);
				prmPaymentStatus.Value = paymentStatus.ToString("d");
				cmd.Parameters.Add(prmPaymentStatus);

				MySqlParameter prmSOID = new MySqlParameter("@SOID",MySqlDbType.Int64);
				prmSOID.Value = SOID;
				cmd.Parameters.Add(prmSOID);

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
				string SQL = "DELETE FROM tblSO WHERE SOID IN (" + IDs + ");";

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
								"SOID, " +
								"SONo, " +
								"SODate, " +
								"CustomerID, " +
								"CustomerCode, " +
								"CustomerContact, " +
								"CustomerAddress, " +
								"CustomerTelephoneNo, " +
								"CustomerModeOfTerms, " +
								"CustomerTerms, " +
								"RequiredDeliveryDate, " +
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
								"Status, " +
								"a.Remarks, " +
								"CustomerDRNo, " +
								"DeliveryDate, " +
								"CancelledDate, " +
								"CancelledRemarks, " +
								"CancelledByID, " +
								"PaymentStatus, " +
								"ChartOfAccountIDARTracking, " +
								"ChartOfAccountIDARFreight, " +
								"ChartOfAccountIDARVDeposit, " +
								"ChartOfAccountIDARContra, " +
								"ChartOfAccountIDARLatePayment, " +
								"TotalItemDiscount " +
							"FROM tblSO a INNER JOIN tblBranch b ON a.BranchID = b.BranchID ";
			return stSQL;
		}

		#region Details

		public SODetails Details(long SOID)
		{
			try
			{
				string SQL = SQLSelect() + "WHERE SOID = @SOID;";

				MySqlCommand cmd = new MySqlCommand();
				cmd.CommandType = System.Data.CommandType.Text;
				cmd.CommandText = SQL;

				MySqlParameter prmSOID = new MySqlParameter("@SOID",MySqlDbType.Int16);
				prmSOID.Value = SOID;
				cmd.Parameters.Add(prmSOID);

				MySqlDataReader myReader = base.ExecuteReader(cmd, System.Data.CommandBehavior.SingleResult);

				SODetails Details = new SODetails();

				while (myReader.Read())
				{
					Details.SOID = SOID;
					Details.SONo = "" + myReader["SONo"].ToString();
					Details.SODate = myReader.GetDateTime("SODate");
					Details.CustomerID = myReader.GetInt64("CustomerID");
					Details.CustomerCode = "" + myReader["CustomerCode"].ToString();
					Details.CustomerContact = "" + myReader["CustomerContact"].ToString();
					Details.CustomerAddress = "" + myReader["CustomerAddress"].ToString();
					Details.CustomerTelephoneNo = "" + myReader["CustomerTelephoneNo"].ToString();
					Details.CustomerModeOfTerms = myReader.GetInt16("CustomerModeofTerms");
					Details.CustomerTerms = myReader.GetInt16("CustomerTerms");
					Details.RequiredDeliveryDate = myReader.GetDateTime("RequiredDeliveryDate");
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
                    Details.Status = (SOStatus)Enum.Parse(typeof(SOStatus), myReader.GetString("Status"));
					Details.TotalItemDiscount = myReader.GetDecimal("TotalItemDiscount");
					Details.Remarks = "" + myReader["Remarks"].ToString();
					Details.CustomerDRNo = "" + myReader["CustomerDRNo"].ToString();
					Details.DeliveryDate = myReader.GetDateTime("DeliveryDate");
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

		#region Streams

		public System.Data.DataTable ListAsDataTable(SOStatus sostatus, string SortField = "SOID", SortOption SortOrder = SortOption.Ascending)
		{
			string SQL = SQLSelect() + "WHERE Status = @Status ORDER BY " + SortField;

			if (SortOrder == SortOption.Ascending)
				SQL += " ASC";
			else
				SQL += " DESC";

			MySqlCommand cmd = new MySqlCommand();
			cmd.CommandType = System.Data.CommandType.Text;
			cmd.CommandText = SQL;

			MySqlParameter prmStatus = new MySqlParameter("@Status",MySqlDbType.Int16);
			prmStatus.Value = sostatus.ToString("d");
			cmd.Parameters.Add(prmStatus);

            string strDataTableName = "tbl" + this.GetType().FullName.Split(new Char[] { '.' })[this.GetType().FullName.Split(new Char[] { '.' }).Length - 1]; System.Data.DataTable dt = new System.Data.DataTable(strDataTableName);
            base.MySqlDataAdapterFill(cmd, dt);

			return dt;
		}

        public System.Data.DataTable ListAsDataTable(string SortField = "SOID", SortOption SortOrder = SortOption.Ascending)
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

		public MySqlDataReader List(long SOID, string SortField, SortOption SortOrder)
		{
			try
			{
				if (SortField == string.Empty || SortField == null) SortField = "SOID";

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

		public MySqlDataReader List(string SortField, SortOption SortOrder)
		{
			try
			{
				if (SortField == string.Empty || SortField == null) SortField = "SOID";

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

		public MySqlDataReader List(SOStatus postatus, string SortField, SortOption SortOrder)
		{
			try
			{
				if (SortField == string.Empty || SortField == null) SortField = "SOID";

				string SQL = SQLSelect() + "WHERE Status = @Status ORDER BY " + SortField;

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

				MySqlDataReader myReader = base.ExecuteReader(cmd);

				return myReader;
			}
			catch (Exception ex)
			{
				throw base.ThrowException(ex);
			}
		}

		public MySqlDataReader List(SOStatus postatus, long CustomerID, string SortField, SortOption SortOrder)
		{
			try
			{
				if (SortField == string.Empty || SortField == null) SortField = "SOID";

				string SQL = SQLSelect() + "WHERE Status =@Status AND CustomerID = @CustomerID ORDER BY " + SortField;

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

				MySqlParameter prmCustomerID = new MySqlParameter("@CustomerID",MySqlDbType.Int64);
				prmCustomerID.Value = CustomerID;
				cmd.Parameters.Add(prmCustomerID);

				MySqlDataReader myReader = base.ExecuteReader(cmd);

				return myReader;
			}
			catch (Exception ex)
			{
				throw base.ThrowException(ex);
			}
		}
		public MySqlDataReader ListForPayment(long CustomerID, string SortField, SortOption SortOrder)
		{
			try
			{
				if (SortField == string.Empty || SortField == null) SortField = "SOID";

				string SQL = SQLSelect() + "WHERE PaymentStatus <> @FullyPaidPaymentStatus AND Status =@PostedStatus AND CustomerID = @CustomerID ORDER BY " + SortField;

				if (SortOrder == SortOption.Ascending)
					SQL += " ASC";
				else
					SQL += " DESC";

				MySqlCommand cmd = new MySqlCommand();
				cmd.CommandType = System.Data.CommandType.Text;
				cmd.CommandText = SQL;

				MySqlParameter prmFullyPaidPaymentStatus = new MySqlParameter("@FullyPaidPaymentStatus",MySqlDbType.Int16);
				prmFullyPaidPaymentStatus.Value = SOPaymentStatus.FullyPaid.ToString("d");
				cmd.Parameters.Add(prmFullyPaidPaymentStatus);

				MySqlParameter prmPostedStatus = new MySqlParameter("@PostedStatus",MySqlDbType.Int16);
				prmPostedStatus.Value = SOStatus.Posted.ToString("d");
				cmd.Parameters.Add(prmPostedStatus);

				MySqlParameter prmCustomerID = new MySqlParameter("@CustomerID",MySqlDbType.Int64);
				prmCustomerID.Value = CustomerID;
				cmd.Parameters.Add(prmCustomerID);

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
				if (SortField == string.Empty || SortField == null) SortField = "SOID";

				string SQL = SQLSelect() + "WHERE (SONo LIKE @SearchKey or SODate LIKE @SearchKey or CustomerCode LIKE @SearchKey " +
										"or CustomerContact LIKE @SearchKey or BranchCode LIKE @SearchKey or RequiredDeliveryDate LIKE @SearchKey) " +
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
		public MySqlDataReader Search(SOStatus postatus, string SearchKey, string SortField, SortOption SortOrder)
		{
			try
			{
				if (SortField == string.Empty || SortField == null) SortField = "SOID";

				string SQL = SQLSelect() + "WHERE Status = @Status AND (SONo LIKE @SearchKey or SODate LIKE @SearchKey or CustomerCode LIKE @SearchKey " +
										"or CustomerContact LIKE @SearchKey or BranchCode LIKE @SearchKey or RequiredDeliveryDate LIKE @SearchKey) " +
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

				MySqlDataReader myReader = base.ExecuteReader(cmd);

				return myReader;
			}
			catch (Exception ex)
			{
				throw base.ThrowException(ex);
			}
		}
		public System.Data.DataTable SearchAsDataTable(SOStatus postatus, string SearchKey, string SortField, SortOption SortOrder)
		{
			try
			{
				if (SortField == string.Empty || SortField == null) SortField = "SOID";

				string SQL = SQLSelect() + "WHERE Status = @Status AND (SONo LIKE @SearchKey or SODate LIKE @SearchKey or CustomerCode LIKE @SearchKey " +
										"or CustomerContact LIKE @SearchKey or BranchCode LIKE @SearchKey or RequiredDeliveryDate LIKE @SearchKey) " +
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

				System.Data.DataTable dt = new System.Data.DataTable("SO");
				MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
				adapter.Fill(dt);

				return dt;
			}
			catch (Exception ex)
			{
				throw base.ThrowException(ex);
			}
		}
		public MySqlDataReader List(SOStatus postatus, DateTime StartDate, DateTime EndDate)
		{
			try
			{
				string SQL = SQLSelect() + "WHERE Status = @Status AND DeliveryDate BETWEEN @StartDate AND @EndDate ORDER BY SOID ASC";

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
				prmStatus.Value = postatus.ToString("d");
				cmd.Parameters.Add(prmStatus);

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
				stRetValue = clsERPConfig.get_LastSONo();

				return stRetValue;
			}

			catch (Exception ex)
			{
				throw base.ThrowException(ex);
			}
		}
		public void SynchronizeAmount(long SOID)
		{
			try
			{
				string SQL = "CALL procSOSynchronizeAmount(@SOID);";

				MySqlCommand cmd = new MySqlCommand();
				cmd.CommandType = System.Data.CommandType.Text;
				cmd.CommandText = SQL;

				MySqlParameter prmSOID = new MySqlParameter("@SOID",MySqlDbType.Int64);
				prmSOID.Value = SOID;
				cmd.Parameters.Add(prmSOID);

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
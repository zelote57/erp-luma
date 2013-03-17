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
                                "ChartOfAccountIDAPLatePayment" +
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
                                "@ChartOfAccountIDAPLatePayment" +
							");";
				  
				MySqlCommand cmd = new MySqlCommand();
				cmd.CommandType = System.Data.CommandType.Text;
				cmd.CommandText = SQL;
				
				MySqlParameter prmPONo = new MySqlParameter("@PONo",MySqlDbType.String);
				prmPONo.Value = Details.PONo;
				cmd.Parameters.Add(prmPONo);

				MySqlParameter prmPODate = new MySqlParameter("@PODate",MySqlDbType.DateTime);
				prmPODate.Value = Details.PODate.ToString("yyyy-MM-dd HH:mm:ss");
				cmd.Parameters.Add(prmPODate);

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

                cmd.Parameters.AddWithValue("@RID", Details.RID);

				MySqlParameter prmBranchID = new MySqlParameter("@BranchID",MySqlDbType.Int16);
				prmBranchID.Value = Details.BranchID;
				cmd.Parameters.Add(prmBranchID);				 
				
				MySqlParameter prmPurchaserID = new MySqlParameter("@PurchaserID",MySqlDbType.Int64);						
				prmPurchaserID.Value = Details.PurchaserID;
				cmd.Parameters.Add(prmPurchaserID);

                MySqlParameter prmPurchaserName = new MySqlParameter("@PurchaserName",MySqlDbType.String);
                prmPurchaserName.Value = Details.PurchaserName;
                cmd.Parameters.Add(prmPurchaserName);

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
		public void Update(PODetails Details)
		{
			try 
			{
                ERPConfig clsERPConfig = new ERPConfig(base.Connection, base.Transaction);
                APLinkConfigDetails clsAPLinkConfigDetails = clsERPConfig.APLinkDetails();

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
				  
				MySqlCommand cmd = new MySqlCommand();
				cmd.CommandType = System.Data.CommandType.Text;
				cmd.CommandText = SQL;

                MySqlParameter prmPONo = new MySqlParameter("@PONo",MySqlDbType.String);
                prmPONo.Value = Details.PONo;
                cmd.Parameters.Add(prmPONo);

                MySqlParameter prmPODate = new MySqlParameter("@PODate",MySqlDbType.DateTime);
                prmPODate.Value = Details.PODate.ToString("yyyy-MM-dd HH:mm:ss");
                cmd.Parameters.Add(prmPODate);

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

                cmd.Parameters.AddWithValue("@RID", Details.RID);

                MySqlParameter prmBranchID = new MySqlParameter("@BranchID",MySqlDbType.Int16);
                prmBranchID.Value = Details.BranchID;
                cmd.Parameters.Add(prmBranchID);

                MySqlParameter prmPurchaserID = new MySqlParameter("@PurchaserID",MySqlDbType.Int64);
                prmPurchaserID.Value = Details.PurchaserID;
                cmd.Parameters.Add(prmPurchaserID);

                MySqlParameter prmPurchaserName = new MySqlParameter("@PurchaserName",MySqlDbType.String);
                prmPurchaserName.Value = Details.PurchaserName;
                cmd.Parameters.Add(prmPurchaserName);

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

				MySqlParameter prmPOID = new MySqlParameter("@POID",MySqlDbType.Int64);						
				prmPOID.Value = Details.POID;
				cmd.Parameters.Add(prmPOID);

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
                string SQL = "UPDATE tblPO SET " +
                                "IsVatInclusive          =   @IsVatInclusive " +
                            "WHERE POID = @POID;";

                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandText = SQL;

                MySqlParameter prmIsVatInclusive = new MySqlParameter("@IsVatInclusive",MySqlDbType.Int16);
                prmIsVatInclusive.Value = Convert.ToInt16(IsVatInclusive); ;
                cmd.Parameters.Add(prmIsVatInclusive);

                MySqlParameter prmPOID = new MySqlParameter("@POID",MySqlDbType.Int64);
                prmPOID.Value = POID;
                cmd.Parameters.Add(prmPOID);

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
                string SQL = "UPDATE tblPO SET " +
                                "DiscountApplied        =   @DiscountApplied, " +
                                "DiscountType           =   @DiscountType, " +
                                "Discount2Applied       =   @Discount2Applied, " +
                                "Discount2Type          =   @Discount2Type, " +
                                "Discount3Applied       =   @Discount3Applied, " +
                                "Discount3Type          =   @Discount3Type " +
                            "WHERE POID = @POID;";

                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandText = SQL;

                MySqlParameter prmDiscountApplied = new MySqlParameter("@DiscountApplied",MySqlDbType.Decimal);
                prmDiscountApplied.Value = DiscountApplied;
                cmd.Parameters.Add(prmDiscountApplied);

                MySqlParameter prmDiscountType = new MySqlParameter("@DiscountType",MySqlDbType.Int16);
                prmDiscountType.Value = Convert.ToInt16(DiscountType.ToString("d"));
                cmd.Parameters.Add(prmDiscountType);

                MySqlParameter prmDiscount2Applied = new MySqlParameter("@Discount2Applied",MySqlDbType.Decimal);
                prmDiscount2Applied.Value = Discount2Applied;
                cmd.Parameters.Add(prmDiscount2Applied);

                MySqlParameter prmDiscount2Type = new MySqlParameter("@Discount2Type",MySqlDbType.Int16);
                prmDiscount2Type.Value = Convert.ToInt16(Discount2Type.ToString("d"));
                cmd.Parameters.Add(prmDiscount2Type);

                MySqlParameter prmDiscount3Applied = new MySqlParameter("@Discount3Applied",MySqlDbType.Decimal);
                prmDiscount3Applied.Value = Discount3Applied;
                cmd.Parameters.Add(prmDiscount3Applied);

                MySqlParameter prmDiscount3Type = new MySqlParameter("@Discount3Type",MySqlDbType.Int16);
                prmDiscount3Type.Value = Convert.ToInt16(Discount3Type.ToString("d"));
                cmd.Parameters.Add(prmDiscount3Type);

                MySqlParameter prmPOID = new MySqlParameter("@POID",MySqlDbType.Int64);
                prmPOID.Value = POID;
                cmd.Parameters.Add(prmPOID);

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
                string SQL = "UPDATE tblPO SET " +
                                "DiscountApplied        =   @DiscountApplied, " +
                                "DiscountType           =   @DiscountType " +
                            "WHERE POID = @POID;";

                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandText = SQL;

                MySqlParameter prmDiscountApplied = new MySqlParameter("@DiscountApplied",MySqlDbType.Decimal);
                prmDiscountApplied.Value = DiscountApplied;
                cmd.Parameters.Add(prmDiscountApplied);

                MySqlParameter prmDiscountType = new MySqlParameter("@DiscountType",MySqlDbType.Int16);
                prmDiscountType.Value = Convert.ToInt16(DiscountType.ToString("d"));
                cmd.Parameters.Add(prmDiscountType);

                MySqlParameter prmPOID = new MySqlParameter("@POID",MySqlDbType.Int64);
                prmPOID.Value = POID;
                cmd.Parameters.Add(prmPOID);

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
                string SQL = "UPDATE tblPO SET " +
                                "Freight           =   @Freight " +
                            "WHERE POID = @POID;";

                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandText = SQL;

                MySqlParameter prmFreight = new MySqlParameter("@Freight",MySqlDbType.Decimal);
                prmFreight.Value = Freight;
                cmd.Parameters.Add(prmFreight);

                MySqlParameter prmPOID = new MySqlParameter("@POID",MySqlDbType.Int64);
                prmPOID.Value = POID;
                cmd.Parameters.Add(prmPOID);

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
                string SQL = "UPDATE tblPO SET " +
                                "Deposit           =   @Deposit " +
                            "WHERE POID = @POID;";

                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandText = SQL;

                MySqlParameter prmDeposit = new MySqlParameter("@Deposit",MySqlDbType.Decimal);
                prmDeposit.Value = Deposit;
                cmd.Parameters.Add(prmDeposit);

                MySqlParameter prmPOID = new MySqlParameter("@POID",MySqlDbType.Int64);
                prmPOID.Value = POID;
                cmd.Parameters.Add(prmPOID);

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
				string SQL=	"UPDATE tblPO SET " + 
								"SupplierDRNo			=	@SupplierDRNo, " +
                                "DeliveryDate			=	@DeliveryDate, " +
								"Status				    =	@Status " +
							"WHERE POID = @POID;";
				  
				MySqlCommand cmd = new MySqlCommand();
				cmd.CommandType = System.Data.CommandType.Text;
				cmd.CommandText = SQL;
				
				MySqlParameter prmSupplierDRNo = new MySqlParameter("@SupplierDRNo",MySqlDbType.String);
				prmSupplierDRNo.Value = SupplierDRNo;
				cmd.Parameters.Add(prmSupplierDRNo);

                MySqlParameter prmDeliveryDate = new MySqlParameter("@DeliveryDate",MySqlDbType.DateTime);
                prmDeliveryDate.Value = DeliveryDate.ToString("yyyy-MM-dd HH:mm:ss");
                cmd.Parameters.Add(prmDeliveryDate);

				MySqlParameter prmStatus = new MySqlParameter("@Status",MySqlDbType.Int16);
				prmStatus.Value = Convert.ToInt16(POStatus.Posted);
				cmd.Parameters.Add(prmStatus);

				MySqlParameter prmPOID = new MySqlParameter("@POID",MySqlDbType.Int64);						
				prmPOID.Value = POID;
				cmd.Parameters.Add(prmPOID);

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
                ChartOfAccount clsChartOfAccount = new ChartOfAccount(base.Connection, base.Transaction);

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
            MatrixPackage clsMatrixPackage = new MatrixPackage(base.Connection, base.Transaction);

			Inventory clsInventory = new Inventory(base.Connection, base.Transaction);
            InventoryDetails clsInventoryDetails;

            MatrixPackagePriceHistoryDetails clsMatrixPackagePriceHistoryDetails;
            ProductPackagePriceHistoryDetails clsProductPackagePriceHistoryDetails;

			//MySqlDataReader myReader = clsPOItem.List(POID, "POItemID", SortOption.Ascending);
            System.Data.DataTable dt = clsPOItem.ListAsDataTable(POID, "POItemID", SortOption.Ascending);

			//while (myReader.Read())
            foreach (System.Data.DataRow dr in dt.Rows)
			{
                long lngProductID = Convert.ToInt64(dr["ProductID"]); // myReader.GetInt64("ProductID");
                int intProductUnitID = Convert.ToInt16(dr["ProductUnitID"]); // myReader.GetInt16("ProductUnitID");

                decimal decItemQuantity = Convert.ToDecimal(dr["Quantity"]); // myReader.GetDecimal("Quantity");
                decimal decQuantity = clsProductUnit.GetBaseUnitValue(lngProductID, intProductUnitID, decItemQuantity);

                long lngVariationMatrixID = Convert.ToInt64(dr["ProductID"]); // myReader.GetInt64("VariationMatrixID");
                string strMatrixDescription = dr["ProductID"].ToString(); //  "" + myReader["MatrixDescription"].ToString();
                string strProductCode = dr["ProductCode"].ToString(); // "" + myReader["ProductCode"].ToString();
                string strProductUnitCode = dr["ProductUnitCode"].ToString(); // "" + myReader["ProductUnitCode"].ToString();
                decimal decUnitCost = Convert.ToDecimal(dr["UnitCost"]); // myReader.GetDecimal("UnitCost");
                decimal decItemCost = Convert.ToDecimal(dr["Amount"]); // myReader.GetDecimal("Amount");
                decimal decVAT = Convert.ToDecimal(dr["VAT"]); // myReader.GetDecimal("VAT");


                /*******************************************
				 * Add in the Purchase Price History
				 * ****************************************/
                if (lngVariationMatrixID != 0)
                {
                    // Update MatrixPackagePriceHistory first to get the history
                    clsMatrixPackagePriceHistoryDetails = new MatrixPackagePriceHistoryDetails();
                    clsMatrixPackagePriceHistoryDetails.UID = clsPODetails.PurchaserID;
                    clsMatrixPackagePriceHistoryDetails.PackageID = clsMatrixPackage.GetPackageID(lngVariationMatrixID, intProductUnitID);
                    clsMatrixPackagePriceHistoryDetails.ChangeDate = DateTime.Now;
                    clsMatrixPackagePriceHistoryDetails.PurchasePrice = (decItemQuantity * decUnitCost) / decQuantity;
                    clsMatrixPackagePriceHistoryDetails.Price = -1;
                    clsMatrixPackagePriceHistoryDetails.VAT = -1;
                    clsMatrixPackagePriceHistoryDetails.EVAT = -1;
                    clsMatrixPackagePriceHistoryDetails.LocalTax = -1;
                    clsMatrixPackagePriceHistoryDetails.Remarks = "Based on PO #: " + clsPODetails.PONo;
                    MatrixPackagePriceHistory clsMatrixPackagePriceHistory = new MatrixPackagePriceHistory(base.Connection, base.Transaction);
                    clsMatrixPackagePriceHistory.Insert(clsMatrixPackagePriceHistoryDetails);
                }
                else {
                    // Update ProductPackagePriceHistory first to get the history
                    clsProductPackagePriceHistoryDetails = new ProductPackagePriceHistoryDetails();
                    clsProductPackagePriceHistoryDetails.UID = clsPODetails.PurchaserID;
                    clsProductPackagePriceHistoryDetails.PackageID = clsProductPackage.GetPackageID(lngProductID, intProductUnitID);
                    clsProductPackagePriceHistoryDetails.ChangeDate = DateTime.Now;
                    clsProductPackagePriceHistoryDetails.PurchasePrice = (decItemQuantity * decUnitCost) / decQuantity;
                    clsProductPackagePriceHistoryDetails.Price = -1;
                    clsProductPackagePriceHistoryDetails.VAT = -1;
                    clsProductPackagePriceHistoryDetails.EVAT = -1;
                    clsProductPackagePriceHistoryDetails.LocalTax = -1;
                    clsProductPackagePriceHistoryDetails.Remarks = "Based on PO #: " + clsPODetails.PONo;
                    ProductPackagePriceHistory clsProductPackagePriceHistory = new ProductPackagePriceHistory(base.Connection, base.Transaction);
                    clsProductPackagePriceHistory.Insert(clsProductPackagePriceHistoryDetails);
                }

				/*******************************************
				 * Add to Inventory
				 * ****************************************/
                //clsProduct.AddQuantity(lngProductID, decQuantity);
                //if (lngVariationMatrixID != 0) { clsProductVariationsMatrix.AddQuantity(lngVariationMatrixID, decQuantity); }
                // July 26, 2011: change the above codes to the following
                clsProduct.AddQuantity(clsPODetails.BranchID, lngProductID, lngVariationMatrixID, decQuantity, Products.getPRODUCT_INVENTORY_MOVEMENT_VALUE(PRODUCT_INVENTORY_MOVEMENT.ADD_PURCHASE) + " @ " + decUnitCost.ToString("#,##0.#0") + "/" + strProductUnitCode, DateTime.Now, clsPODetails.PONo, clsPODetails.PurchaserName);

                /*******************************************
				 * Update Purchasing Information
				 * ****************************************/
                int iBaseUnitID = clsProduct.get_BaseUnitID(lngProductID);
                if (iBaseUnitID != intProductUnitID)
                {
                    clsProduct.UpdatePurchasing(lngProductID, clsPODetails.SupplierID, iBaseUnitID, (decItemQuantity * decUnitCost) / decQuantity);
                }
                clsProduct.UpdatePurchasing(lngProductID, clsPODetails.SupplierID, intProductUnitID, decUnitCost);

                if (lngVariationMatrixID != 0)
                {
                    clsProductVariationsMatrix.UpdatePurchasing(lngVariationMatrixID, clsPODetails.SupplierID, intProductUnitID, (decItemQuantity * decUnitCost) / decQuantity);
                }

                // update base unit purchase price
                

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
                clsProduct.UpdateSellingIncludingAllMatrixWithSameQuantityAndUnit(lngProductID, clsPODetails.SupplierID, intProductUnitID, Convert.ToDecimal(dr["SellingPrice"]));
                //if (lngVariationMatrixID != 0)
                //{
                //    clsProductVariationsMatrix.UpdateSellingWithSameQuantityAndUnit(lngVariationMatrixID, clsPODetails.SupplierID, intProductUnitID, decimal.Parse(myReader["SellingPrice");
                //}

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
		public void GenerateItemsForReorder(long POID)
		{
			try 
			{
				GetConnection();

                Terminal clsTerminal = new Terminal(base.Connection, base.Transaction);
				TerminalDetails clsTerminalDetails = clsTerminal.Details(Terminal.DEFAULT_TERMINAL_NO_ID);

				PODetails clsPODetails = Details(POID);
				
				Products clsProduct = new Products(base.Connection, base.Transaction);
				System.Data.DataTable dt = clsProduct.ForReorder(clsPODetails.SupplierID);

				POItem clsPOItem = new POItem(base.Connection, base.Transaction);
                ProductVariationsMatrix clsProductVariationsMatrix = new ProductVariationsMatrix(base.Connection, base.Transaction);

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
                        clsDetails.Amount = amount + clsDetails.VAT;

                        System.Data.DataTable dtmatrix = clsProductVariationsMatrix.ForReorder(clsDetails.ProductID, clsPODetails.SupplierID);
                        if (dtmatrix.Rows.Count > 0)
                            foreach (System.Data.DataRow drmatrix in dtmatrix.Rows)
                            {
                                // Aug 26, 2011 : Lemu
                                //clsDetails.ProductUnitID = Convert.ToInt32(drmatrix["UnitID"]);
                                //clsDetails.ProductUnitCode = drmatrix["UnitName"].ToString();
                                //clsDetails.Quantity = Convert.ToDecimal(drmatrix["ReorderQty"]);
                                clsDetails.UnitCost = Convert.ToDecimal(drmatrix["PurchasePrice"]);

                                amount = clsDetails.Quantity * clsDetails.UnitCost;

                                // Added Sep 27, 2010 4:20PM : for selling information
                                clsDetails.SellingPrice = decimal.Parse(dr["Price"].ToString());
                                clsDetails.SellingVAT = clsTerminalDetails.VAT;
                                clsDetails.SellingEVAT = clsTerminalDetails.EVAT;
                                clsDetails.SellingLocalTax = clsTerminalDetails.LocalTax;
                                clsDetails.OldSellingPrice = clsDetails.SellingPrice;

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
                                clsPOItem.Insert(clsDetails);
                            }
                        else
                        {
                            clsDetails.VariationMatrixID = 0;
                            clsDetails.MatrixDescription = string.Empty;
                            clsPOItem.Insert(clsDetails);
                        }
                    }
				}
			}

			catch (Exception ex)
			{
				throw base.ThrowException(ex);
			}	
		}

        public void GenerateItemsForReorderByRID(long POID, long RID, DateTime IDC_StartDate, DateTime IDC_EndDate)
        {
            try
            {
                GetConnection();

                Terminal clsTerminal = new Terminal(base.Connection, base.Transaction);
                TerminalDetails clsTerminalDetails = clsTerminal.Details(Terminal.DEFAULT_TERMINAL_NO_ID);

                PODetails clsPODetails = Details(POID);

                Products clsProduct = new Products(base.Connection, base.Transaction);
                // Aug 26, 2011  :Lemu
                // Insert UpdateProductReorderOverStockPerSupplier to update the MinThreshold & MaxThreshold using RID before getting the for stocking
                clsProduct.UpdateProductReorderOverStockPerSupplier(clsPODetails.SupplierID, RID, IDC_StartDate, IDC_EndDate);
                // end

                System.Data.DataTable dt = clsProduct.ForReorder(clsPODetails.SupplierID);

                POItem clsPOItem = new POItem(base.Connection, base.Transaction);
                ProductVariationsMatrix clsProductVariationsMatrix = new ProductVariationsMatrix(base.Connection, base.Transaction);

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

                        System.Data.DataTable dtmatrix = clsProductVariationsMatrix.ForReorder(clsDetails.ProductID, clsPODetails.SupplierID);
                        if (dtmatrix.Rows.Count > 0)
                            foreach (System.Data.DataRow drmatrix in dtmatrix.Rows)
                            {
                                // Aug 26, 2011 : Lemu
                                // Do not overwrite the quantity, used the computed based on RID
                                // clsDetails.ProductUnitID = Convert.ToInt32(drmatrix["UnitID"]);
                                // clsDetails.ProductUnitCode = drmatrix["UnitName"].ToString();
                                // clsDetails.Quantity = Convert.ToDecimal(drmatrix["ReorderQty"]);
                                clsDetails.UnitCost = Convert.ToDecimal(drmatrix["PurchasePrice"]);

                                amount = clsDetails.Quantity * clsDetails.UnitCost;

                                // Added Sep 27, 2010 4:20PM : for selling information
                                clsDetails.SellingPrice = decimal.Parse(dr["Price"].ToString());
                                clsDetails.SellingVAT = clsTerminalDetails.VAT;
                                clsDetails.SellingEVAT = clsTerminalDetails.EVAT;
                                clsDetails.SellingLocalTax = clsTerminalDetails.LocalTax;
                                clsDetails.OldSellingPrice = clsDetails.SellingPrice;

                                if (Convert.ToDecimal(drmatrix["VAT"]) > 0)
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

                                clsDetails.VariationMatrixID = Convert.ToInt64(drmatrix["MatrixID"]);
                                clsDetails.MatrixDescription = drmatrix["VariationDesc"].ToString();
                                clsPOItem.Insert(clsDetails);

                                break;
                            }
                        else
                        {
                            clsDetails.VariationMatrixID = 0;
                            clsDetails.MatrixDescription = string.Empty;
                            clsPOItem.Insert(clsDetails);
                        }
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
                                "RequiredDeliveryDate, " +
                                "RID, " +
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
                            "FROM tblPO a INNER JOIN tblBranch b ON a.BranchID = b.BranchID ";
            return stSQL;
        }

		#region Details

		public PODetails Details(long POID)
		{
			try
			{
				string SQL=	SQLSelect() + "WHERE POID = @POID;";
				  
				MySqlCommand cmd = new MySqlCommand();
				cmd.CommandType = System.Data.CommandType.Text;
				cmd.CommandText = SQL;

				MySqlParameter prmPOID = new MySqlParameter("@POID",MySqlDbType.Int16);
				prmPOID.Value = POID;
				cmd.Parameters.Add(prmPOID);

                MySqlDataReader myReader = base.ExecuteReader(cmd, System.Data.CommandBehavior.SingleResult);
				
				PODetails Details = new PODetails();

				while (myReader.Read()) 
				{
					Details.POID = POID;
					Details.PONo = "" + myReader["PONo"].ToString();
					Details.PODate = myReader.GetDateTime("PODate");
					Details.SupplierID = myReader.GetInt64("SupplierID");
					Details.SupplierCode = "" + myReader["SupplierCode"].ToString();
					Details.SupplierContact = "" + myReader["SupplierContact"].ToString();
					Details.SupplierAddress = "" + myReader["SupplierAddress"].ToString();
					Details.SupplierTelephoneNo = "" + myReader["SupplierTelephoneNo"].ToString();
					Details.SupplierModeOfTerms = myReader.GetInt16("SupplierModeofTerms");
					Details.SupplierTerms = myReader.GetInt16("SupplierTerms");
					Details.RequiredDeliveryDate = myReader.GetDateTime("RequiredDeliveryDate");
                    Details.RID = myReader.GetInt64("RID");
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
                    Details.Status = (POStatus)Enum.Parse(typeof(POStatus), myReader.GetString("Status"));
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
				throw base.ThrowException(ex);
			}	
		}

		#endregion

		#region Streams

        public System.Data.DataTable ListAsDataTable(POStatus postatus, string SortField, SortOption SortOrder)
        {
            if (SortField == string.Empty || SortField == null) SortField = "POID";

            string SQL = SQLSelect() + "WHERE Status = @Status ";

            SQL += "ORDER BY " + SortField;

            if (SortOrder == SortOption.Ascending)
                SQL += " ASC";
            else
                SQL += " DESC";

            MySqlCommand cmd = new MySqlCommand();
            cmd.CommandType = System.Data.CommandType.Text;
            cmd.CommandText = SQL;

            cmd.Parameters.AddWithValue("@Status", postatus.ToString("d"));

            string strDataTableName = "tbl" + this.GetType().FullName.Split(new Char[] { '.' })[this.GetType().FullName.Split(new Char[] { '.' }).Length - 1]; System.Data.DataTable dt = new System.Data.DataTable(strDataTableName);
            base.MySqlDataAdapterFill(cmd, dt);

            return dt;
        }
        public System.Data.DataTable ListAsDataTable(POStatus postatus, DateTime OrderStartDate, DateTime OrderEndDate, DateTime PostingStartDate, DateTime PostingEndDate, string SortField, SortOption SortOrder)
        {
            if (SortField == string.Empty || SortField == null) SortField = "POID";

            string SQL = SQLSelect() + "WHERE Status = @Status ";

            if (OrderStartDate != DateTime.MinValue) SQL += "AND PODate >= @OrderStartDate ";
            if (OrderEndDate != DateTime.MinValue) SQL += "AND PODate <= @OrderEndDate ";
            if (PostingStartDate != DateTime.MinValue) SQL += "AND PODate >= @PostingStartDate ";
            if (PostingEndDate != DateTime.MinValue) SQL += "AND PODate <= @PostingEndDate ";

            SQL += "ORDER BY " + SortField;

            if (SortOrder == SortOption.Ascending)
                SQL += " ASC";
            else
                SQL += " DESC";

            MySqlCommand cmd = new MySqlCommand();
            cmd.CommandType = System.Data.CommandType.Text;
            cmd.CommandText = SQL;

            cmd.Parameters.AddWithValue("@Status", postatus.ToString("d"));

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
            if (SortField == string.Empty || SortField == null) SortField = "POID";

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

		public MySqlDataReader List(long POID, string SortField, SortOption SortOrder)
		{
			try
			{
                if (SortField == string.Empty || SortField == null) SortField = "POID";

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
                if (SortField == string.Empty || SortField == null) SortField = "POID";

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

		public MySqlDataReader List(POStatus postatus, string SortField, SortOption SortOrder)
		{
			try
			{
                if (SortField == string.Empty || SortField == null) SortField = "POID";

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

		public MySqlDataReader List(POStatus postatus, long SupplierID, string SortField, SortOption SortOrder)
		{
			try
			{
                if (SortField == string.Empty || SortField == null) SortField = "POID";

				string SQL = SQLSelect() + "WHERE Status =@Status AND SupplierID = @SupplierID ORDER BY " + SortField;

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
                if (SortField == string.Empty || SortField == null) SortField = "POID";

                string SQL = SQLSelect() + "WHERE PaymentStatus <> @FullyPaidPaymentStatus AND Status =@PostedStatus AND SupplierID = @SupplierID ORDER BY " + SortField;

                if (SortOrder == SortOption.Ascending)
                    SQL += " ASC";
                else
                    SQL += " DESC";

                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandText = SQL;

                MySqlParameter prmFullyPaidPaymentStatus = new MySqlParameter("@FullyPaidPaymentStatus",MySqlDbType.Int16);
                prmFullyPaidPaymentStatus.Value = POPaymentStatus.FullyPaid.ToString("d");
                cmd.Parameters.Add(prmFullyPaidPaymentStatus);

                MySqlParameter prmPostedStatus = new MySqlParameter("@PostedStatus",MySqlDbType.Int16);
                prmPostedStatus.Value = POStatus.Posted.ToString("d");
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
                if (SortField == string.Empty || SortField == null) SortField = "POID";

				string SQL = SQLSelect() + "WHERE (PONo LIKE @SearchKey or PODate LIKE @SearchKey or SupplierCode LIKE @SearchKey " +
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
		public MySqlDataReader Search(POStatus postatus, string SearchKey, string SortField, SortOption SortOrder)
		{
			try
			{
                if (SortField == string.Empty || SortField == null) SortField = "POID";

				string SQL = SQLSelect() + "WHERE Status = @Status AND (PONo LIKE @SearchKey or PODate LIKE @SearchKey or SupplierCode LIKE @SearchKey " +
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
        public System.Data.DataTable SearchAsDataTable(POStatus postatus, string SearchKey, string SortField, SortOption SortOrder)
        {
            try
            {
                if (SortField == string.Empty || SortField == null) SortField = "POID";

                string SQL = SQLSelect() + "WHERE Status = @Status AND (PONo LIKE @SearchKey or PODate LIKE @SearchKey or SupplierCode LIKE @SearchKey " +
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
        public System.Data.DataTable SearchAsDataTable(POStatus postatus, DateTime OrderStartDate, DateTime OrderEndDate, DateTime PostingStartDate, DateTime PostingEndDate, string SearchKey, string SortField, SortOption SortOrder)
        {
            try
            {
                if (SortField == string.Empty || SortField == null) SortField = "POID";

                string SQL = SQLSelect() + "WHERE Status = @Status AND (PONo LIKE @SearchKey or PODate LIKE @SearchKey or SupplierCode LIKE @SearchKey " +
                                        "or SupplierContact LIKE @SearchKey or BranchCode LIKE @SearchKey or RequiredDeliveryDate LIKE @SearchKey or a.Remarks LIKE @SearchKey) ";

                if (OrderStartDate != DateTime.MinValue) SQL += "AND PODate >= @OrderStartDate ";
                if (OrderEndDate != DateTime.MinValue) SQL += "AND PODate <= @OrderEndDate ";
                if (PostingStartDate != DateTime.MinValue) SQL += "AND DeliveryDate >= @PostingStartDate ";
                if (PostingEndDate != DateTime.MinValue) SQL += "AND DeliveryDate <= @PostingEndDate ";

                SQL += "ORDER BY " + SortField;

                if (SortOrder == SortOption.Ascending)
                    SQL += " ASC";
                else
                    SQL += " DESC";

                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandText = SQL;

                cmd.Parameters.AddWithValue("@Status", postatus.ToString("d"));
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

        public MySqlDataReader List(POStatus postatus, DateTime StartDate, DateTime EndDate)
		{
			try
			{
				string SQL = SQLSelect() + "WHERE Status = @Status AND DeliveryDate BETWEEN @StartDate AND @EndDate ORDER BY POID ASC";

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
        public MySqlDataReader List(POStatus postatus, long SupplierID, DateTime StartDate, DateTime EndDate)
        {
            try
            {
                string SQL = SQLSelect() + "WHERE Status = @Status AND SupplierID = @SupplierID AND DeliveryDate BETWEEN @StartDate AND @EndDate ORDER BY POID ASC";

                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandText = SQL;

                cmd.Parameters.AddWithValue("@Status", postatus.ToString("d"));
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
                string SQL = "CALL procPOSynchronizeAmount(@POID);";

                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandText = SQL;

                MySqlParameter prmPOID = new MySqlParameter("@POID",MySqlDbType.Int64);
                prmPOID.Value = POID;
                cmd.Parameters.Add(prmPOID);

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


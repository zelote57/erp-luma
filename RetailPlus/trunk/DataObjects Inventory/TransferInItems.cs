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

    #region TransferInItemDetails

    public struct TransferInItemDetails
    {
        public long TransferInItemID;
        public long TransferInID;
        public long ProductID;
        public string ProductCode;
        public string BarCode;
        public string Description;
        public int ProductUnitID;
        public string ProductUnitCode;
        public decimal Quantity;
        public decimal UnitCost;
        public decimal Discount;
        public decimal DiscountApplied;
        public DiscountTypes DiscountType;
        public decimal Amount;
        public decimal VAT;
        public decimal VatableAmount;
        public decimal EVAT;
        public decimal EVatableAmount;
        public decimal LocalTax;
        public bool isVATInclusive;
        public long VariationMatrixID;
        public string MatrixDescription;
        public string ProductGroup;
        public string ProductSubGroup;
        public TransferInItemStatus TransferInItemStatus;
        public bool IsVatable;
        public string Remarks;
        public int ChartOfAccountIDTransferIn;
        public int ChartOfAccountIDTaxTransferIn;
        public int ChartOfAccountIDInventory;

        // Added April 28, 2010 6:49PM
        public decimal SellingPrice;
        public decimal SellingVAT;
        public decimal SellingEVAT;
        public decimal SellingLocalTax;
        public decimal OldSellingPrice;

        public decimal OriginalQuantity;
        public TransferInItemReceivedStatus TransferInItemReceivedStatus;
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
    public class TransferInItem
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

        public TransferInItem()
        {

        }

        public TransferInItem(MySqlConnection Connection, MySqlTransaction Transaction)
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

        public long Insert(TransferInItemDetails Details)
        {
            try
            {
                string SQL = "INSERT INTO tblTransferInItems (" +
                                "TransferInID, " +
                                "ProductID, " +
                                "ProductCode, " +
                                "BarCode, " +
                                "Description, " +
                                "ProductUnitID, " +
                                "ProductUnitCode, " +
                                "Quantity, " +
                                "OriginalQuantity, " +
                                "UnitCost, " +
                                "Discount, " +
                                "DiscountApplied, " +
                                "DiscountType, " +
                                "Amount, " +
                                "VAT, " +
                                "VatableAmount, " +
                                "EVAT, " +
                                "EVatableAmount, " +
                                "LocalTax, " +
                                "isVATInclusive, " +
                                "VariationMatrixID, " +
                                "MatrixDescription, " +
                                "ProductGroup, " +
                                "ProductSubGroup, " +
                                "TransferInItemStatus, " +
                                "IsVatable, " +
                                "Remarks, " +
                                "ChartOfAccountIDTransferIn, " +
                                "ChartOfAccountIDTaxTransferIn, " +
                                "ChartOfAccountIDInventory," +
                                "SellingPrice," +
                                "SellingVAT," +
                                "SellingEVAT," +
                                "SellingLocalTax," +
                                "OldSellingPrice" +
                            ") VALUES (" +
                                "@TransferInID, " +
                                "@ProductID, " +
                                "@ProductCode, " +
                                "@BarCode, " +
                                "@Description, " +
                                "@ProductUnitID, " +
                                "@ProductUnitCode, " +
                                "@Quantity, " +
                                "@Quantity, " +
                                "@UnitCost, " +
                                "@Discount, " +
                                "@DiscountApplied, " +
                                "@DiscountType, " +
                                "@Amount, " +
                                "@VAT, " +
                                "@VatableAmount, " +
                                "@EVAT, " +
                                "@EVatableAmount, " +
                                "@LocalTax, " +
                                "@isVATInclusive, " +
                                "@VariationMatrixID, " +
                                "@MatrixDescription, " +
                                "@ProductGroup, " +
                                "@ProductSubGroup, " +
                                "@TransferInItemStatus, " +
                                "@IsVatable, " +
                                "@Remarks, " +
                                "(SELECT ChartOfAccountIDTransferIn FROM tblProducts WHERE ProductID = @ProductID), " +
                                "(SELECT ChartOfAccountIDTaxTransferIn FROM tblProducts WHERE ProductID = @ProductID), " +
                                "(SELECT ChartOfAccountIDInventory FROM tblProducts WHERE ProductID = @ProductID)," +
                                "@SellingPrice," +
                                "@SellingVAT," +
                                "@SellingEVAT," +
                                "@SellingLocalTax," +
                                "@OldSellingPrice" +
                            ");";

                MySqlConnection cn = GetConnection();

                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = cn;
                cmd.Transaction = mTransaction;
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandText = SQL;

                MySqlParameter prmTransferInID = new MySqlParameter("@TransferInID",MySqlDbType.Int64);
                prmTransferInID.Value = Details.TransferInID;
                cmd.Parameters.Add(prmTransferInID);

                MySqlParameter prmProductID = new MySqlParameter("@ProductID",MySqlDbType.Int64);
                prmProductID.Value = Details.ProductID;
                cmd.Parameters.Add(prmProductID);

                MySqlParameter prmProductCode = new MySqlParameter("@ProductCode",MySqlDbType.String);
                prmProductCode.Value = Details.ProductCode;
                cmd.Parameters.Add(prmProductCode);

                MySqlParameter prmBarCode = new MySqlParameter("@BarCode",MySqlDbType.String);
                prmBarCode.Value = Details.BarCode;
                cmd.Parameters.Add(prmBarCode);

                MySqlParameter prmDescription = new MySqlParameter("@Description",MySqlDbType.String);
                prmDescription.Value = Details.Description;
                cmd.Parameters.Add(prmDescription);

                MySqlParameter prmProductUnitID = new MySqlParameter("@ProductUnitID",MySqlDbType.Int16);
                prmProductUnitID.Value = Details.ProductUnitID;
                cmd.Parameters.Add(prmProductUnitID);

                MySqlParameter prmProductUnitCode = new MySqlParameter("@ProductUnitCode",MySqlDbType.String);
                prmProductUnitCode.Value = Details.ProductUnitCode;
                cmd.Parameters.Add(prmProductUnitCode);

                MySqlParameter prmQuantity = new MySqlParameter("@Quantity",MySqlDbType.Decimal);
                prmQuantity.Value = Details.Quantity;
                cmd.Parameters.Add(prmQuantity);

                MySqlParameter prmUnitCost = new MySqlParameter("@UnitCost",MySqlDbType.Decimal);
                prmUnitCost.Value = Details.UnitCost;
                cmd.Parameters.Add(prmUnitCost);

                MySqlParameter prmDiscount = new MySqlParameter("@Discount",MySqlDbType.Decimal);
                prmDiscount.Value = Details.Discount;
                cmd.Parameters.Add(prmDiscount);

                MySqlParameter prmDiscountApplied = new MySqlParameter("@DiscountApplied",MySqlDbType.Decimal);
                prmDiscountApplied.Value = Details.DiscountApplied;
                cmd.Parameters.Add(prmDiscountApplied);

                MySqlParameter prmDiscountType = new MySqlParameter("@DiscountType",MySqlDbType.Int16);
                prmDiscountType.Value = (int)Details.DiscountType;
                cmd.Parameters.Add(prmDiscountType);

                MySqlParameter prmAmount = new MySqlParameter("@Amount",MySqlDbType.Decimal);
                prmAmount.Value = Details.Amount;
                cmd.Parameters.Add(prmAmount);

                MySqlParameter prmVAT = new MySqlParameter("@VAT",MySqlDbType.Decimal);
                prmVAT.Value = Details.VAT;
                cmd.Parameters.Add(prmVAT);

                MySqlParameter prmVatableAmount = new MySqlParameter("@VatableAmount",MySqlDbType.Decimal);
                prmVatableAmount.Value = Details.VatableAmount;
                cmd.Parameters.Add(prmVatableAmount);

                MySqlParameter prmEVAT = new MySqlParameter("@EVAT",MySqlDbType.Decimal);
                prmEVAT.Value = Details.EVAT;
                cmd.Parameters.Add(prmEVAT);

                MySqlParameter prmEVatableAmount = new MySqlParameter("@EVatableAmount",MySqlDbType.Decimal);
                prmEVatableAmount.Value = Details.EVatableAmount;
                cmd.Parameters.Add(prmEVatableAmount);

                MySqlParameter prmLocalTax = new MySqlParameter("@LocalTax",MySqlDbType.Decimal);
                prmLocalTax.Value = Details.LocalTax;
                cmd.Parameters.Add(prmLocalTax);

                MySqlParameter prmisVATInclusive = new MySqlParameter("@isVATInclusive",MySqlDbType.Int16);
                prmisVATInclusive.Value = Convert.ToInt16(Details.isVATInclusive);
                cmd.Parameters.Add(prmisVATInclusive);

                MySqlParameter prmVariationMatrixID = new MySqlParameter("@VariationMatrixID",MySqlDbType.Int64);
                prmVariationMatrixID.Value = Details.VariationMatrixID;
                cmd.Parameters.Add(prmVariationMatrixID);

                MySqlParameter prmMatrixDescription = new MySqlParameter("@MatrixDescription",MySqlDbType.String);
                prmMatrixDescription.Value = Details.MatrixDescription;
                cmd.Parameters.Add(prmMatrixDescription);

                MySqlParameter prmProductGroup = new MySqlParameter("@ProductGroup",MySqlDbType.String);
                prmProductGroup.Value = Details.ProductGroup;
                cmd.Parameters.Add(prmProductGroup);

                MySqlParameter prmProductSubGroup = new MySqlParameter("@ProductSubGroup",MySqlDbType.String);
                prmProductSubGroup.Value = Details.ProductSubGroup;
                cmd.Parameters.Add(prmProductSubGroup);

                MySqlParameter prmTransferInItemStatus = new MySqlParameter("@TransferInItemStatus",MySqlDbType.Int16);
                prmTransferInItemStatus.Value = Details.TransferInItemStatus.ToString("d");
                cmd.Parameters.Add(prmTransferInItemStatus);

                MySqlParameter prmIsVatable = new MySqlParameter("@IsVatable",MySqlDbType.Int16);
                prmIsVatable.Value = Convert.ToInt16(Details.IsVatable);
                cmd.Parameters.Add(prmIsVatable);

                MySqlParameter prmRemarks = new MySqlParameter("@Remarks",MySqlDbType.String);
                prmRemarks.Value = Details.Remarks;
                cmd.Parameters.Add(prmRemarks);

                cmd.Parameters.AddWithValue("@SellingPrice", Details.SellingPrice);
                cmd.Parameters.AddWithValue("@SellingVAT", Details.SellingVAT);
                cmd.Parameters.AddWithValue("@SellingEVAT", Details.SellingEVAT);
                cmd.Parameters.AddWithValue("@SellingLocalTax", Details.SellingLocalTax);
                cmd.Parameters.AddWithValue("@OldSellingPrice", Details.OldSellingPrice);

                cmd.ExecuteNonQuery();

                SQL = "SELECT LAST_INSERT_ID();";

                cmd.Parameters.Clear();
                cmd.CommandText = SQL;

                System.Data.DataTable dt = new System.Data.DataTable("TransferInItem");
                MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
                adapter.Fill(dt);

                Int64 iID = 0;
                foreach (System.Data.DataRow dr in dt.Rows)
                {
                    iID = Int64.Parse(dr[0].ToString());
                }

                TransferIn clsTransferIn = new TransferIn(Connection, Transaction);
                clsTransferIn.SynchronizeAmount(Details.TransferInID);

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

        public void Update(TransferInItemDetails Details)
        {
            try
            {
                string SQL = "UPDATE tblTransferInItems SET " +
                                "TransferInID					=	@TransferInID, " +
                                "ProductID				=	@ProductID, " +
                                "ProductCode			=	@ProductCode, " +
                                "BarCode				=	@BarCode, " +
                                "Description			=	@Description, " +
                                "ProductUnitID			=	@ProductUnitID, " +
                                "ProductUnitCode		=	@ProductUnitCode, " +
                                "Quantity				=	@Quantity, " +
                                "OriginalQuantity       =   @Quantity, " +
                                "UnitCost				=	@UnitCost, " +
                                "Discount				=	@Discount, " +
                                "DiscountApplied		=	@DiscountApplied, " +
                                "DiscountType			=	@DiscountType, " +
                                "Amount					=	@Amount, " +
                                "VAT					=	@VAT, " +
                                "VatableAmount			=	@VatableAmount, " +
                                "EVAT					=	@EVAT, " +
                                "EVatableAmount			=	@EVatableAmount, " +
                                "LocalTax				=	@LocalTax, " +
                                "isVATInclusive			=	@isVATInclusive, " +
                                "VariationMatrixID		=	@VariationMatrixID, " +
                                "MatrixDescription		=	@MatrixDescription, " +
                                "ProductGroup			=	@ProductGroup, " +
                                "ProductSubGroup		=	@ProductSubGroup, " +
                                "TransferInItemStatus	=	@TransferInItemStatus, " +
                                "IsVatable				=	@IsVatable, " +
                                "Remarks				=	@Remarks, " +
                                "ChartOfAccountIDTransferIn       = (SELECT ChartOfAccountIDTransferIn FROM tblProducts WHERE ProductID = @ProductID), " +
                                "ChartOfAccountIDTaxTransferIn    = (SELECT ChartOfAccountIDTaxTransferIn FROM tblProducts WHERE ProductID = @ProductID), " +
                                "ChartOfAccountIDInventory      = (SELECT ChartOfAccountIDInventory FROM tblProducts WHERE ProductID = @ProductID), " +
                                "SellingPrice			=	@SellingPrice, " +
                                "SellingVAT				=	@SellingVAT, " +
                                "SellingEVAT			=	@SellingEVAT, " +
                                "SellingLocalTax		=	@SellingLocalTax, " +
                                "OldSellingPrice		=	@OldSellingPrice " +
                            "WHERE TransferInItemID = @TransferInItemID;";

                MySqlConnection cn = GetConnection();

                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = cn;
                cmd.Transaction = mTransaction;
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandText = SQL;

                MySqlParameter prmTransferInID = new MySqlParameter("@TransferInID",MySqlDbType.Int64);
                prmTransferInID.Value = Details.TransferInID;
                cmd.Parameters.Add(prmTransferInID);

                MySqlParameter prmProductID = new MySqlParameter("@ProductID",MySqlDbType.Int64);
                prmProductID.Value = Details.ProductID;
                cmd.Parameters.Add(prmProductID);

                MySqlParameter prmProductCode = new MySqlParameter("@ProductCode",MySqlDbType.String);
                prmProductCode.Value = Details.ProductCode;
                cmd.Parameters.Add(prmProductCode);

                MySqlParameter prmBarCode = new MySqlParameter("@BarCode",MySqlDbType.String);
                prmBarCode.Value = Details.BarCode;
                cmd.Parameters.Add(prmBarCode);

                MySqlParameter prmDescription = new MySqlParameter("@Description",MySqlDbType.String);
                prmDescription.Value = Details.Description;
                cmd.Parameters.Add(prmDescription);

                MySqlParameter prmProductUnitID = new MySqlParameter("@ProductUnitID",MySqlDbType.Int16);
                prmProductUnitID.Value = Details.ProductUnitID;
                cmd.Parameters.Add(prmProductUnitID);

                MySqlParameter prmProductUnitCode = new MySqlParameter("@ProductUnitCode",MySqlDbType.String);
                prmProductUnitCode.Value = Details.ProductUnitCode;
                cmd.Parameters.Add(prmProductUnitCode);

                MySqlParameter prmQuantity = new MySqlParameter("@Quantity",MySqlDbType.Decimal);
                prmQuantity.Value = Details.Quantity;
                cmd.Parameters.Add(prmQuantity);

                MySqlParameter prmUnitCost = new MySqlParameter("@UnitCost",MySqlDbType.Decimal);
                prmUnitCost.Value = Details.UnitCost;
                cmd.Parameters.Add(prmUnitCost);

                MySqlParameter prmDiscount = new MySqlParameter("@Discount",MySqlDbType.Decimal);
                prmDiscount.Value = Details.Discount;
                cmd.Parameters.Add(prmDiscount);

                MySqlParameter prmDiscountApplied = new MySqlParameter("@DiscountApplied",MySqlDbType.Decimal);
                prmDiscountApplied.Value = Details.DiscountApplied;
                cmd.Parameters.Add(prmDiscountApplied);

                MySqlParameter prmDiscountType = new MySqlParameter("@DiscountType",MySqlDbType.Int16);
                prmDiscountType.Value = (int)Details.DiscountType;
                cmd.Parameters.Add(prmDiscountType);

                MySqlParameter prmAmount = new MySqlParameter("@Amount",MySqlDbType.Decimal);
                prmAmount.Value = Details.Amount;
                cmd.Parameters.Add(prmAmount);

                MySqlParameter prmVAT = new MySqlParameter("@VAT",MySqlDbType.Decimal);
                prmVAT.Value = Details.VAT;
                cmd.Parameters.Add(prmVAT);

                MySqlParameter prmVatableAmount = new MySqlParameter("@VatableAmount",MySqlDbType.Decimal);
                prmVatableAmount.Value = Details.VatableAmount;
                cmd.Parameters.Add(prmVatableAmount);

                MySqlParameter prmEVAT = new MySqlParameter("@EVAT",MySqlDbType.Decimal);
                prmEVAT.Value = Details.EVAT;
                cmd.Parameters.Add(prmEVAT);

                MySqlParameter prmEVatableAmount = new MySqlParameter("@EVatableAmount",MySqlDbType.Decimal);
                prmEVatableAmount.Value = Details.EVatableAmount;
                cmd.Parameters.Add(prmEVatableAmount);

                MySqlParameter prmLocalTax = new MySqlParameter("@LocalTax",MySqlDbType.Decimal);
                prmLocalTax.Value = Details.LocalTax;
                cmd.Parameters.Add(prmLocalTax);

                MySqlParameter prmisVATInclusive = new MySqlParameter("@isVATInclusive",MySqlDbType.Int16);
                prmisVATInclusive.Value = Convert.ToInt16(Details.isVATInclusive);
                cmd.Parameters.Add(prmisVATInclusive);

                MySqlParameter prmVariationMatrixID = new MySqlParameter("@VariationMatrixID",MySqlDbType.Int64);
                prmVariationMatrixID.Value = Details.VariationMatrixID;
                cmd.Parameters.Add(prmVariationMatrixID);

                MySqlParameter prmMatrixDescription = new MySqlParameter("@MatrixDescription",MySqlDbType.String);
                prmMatrixDescription.Value = Details.MatrixDescription;
                cmd.Parameters.Add(prmMatrixDescription);

                MySqlParameter prmProductGroup = new MySqlParameter("@ProductGroup",MySqlDbType.String);
                prmProductGroup.Value = Details.ProductGroup;
                cmd.Parameters.Add(prmProductGroup);

                cmd.Parameters.AddWithValue("@ProductSubGroup", Details.ProductSubGroup);
                cmd.Parameters.AddWithValue("@TransferInItemStatus", Details.TransferInItemStatus.ToString("d"));
                cmd.Parameters.AddWithValue("@IsVatable", Convert.ToInt16(Details.IsVatable));
                cmd.Parameters.AddWithValue("@Remarks", Details.Remarks);
                cmd.Parameters.AddWithValue("@SellingPrice", Details.SellingPrice);
                cmd.Parameters.AddWithValue("@SellingVAT", Details.SellingVAT);
                cmd.Parameters.AddWithValue("@SellingEVAT", Details.SellingEVAT);
                cmd.Parameters.AddWithValue("@SellingLocalTax", Details.SellingLocalTax);
                cmd.Parameters.AddWithValue("@OldSellingPrice", Details.OldSellingPrice);
                cmd.Parameters.AddWithValue("@TransferInItemID", Details.TransferInItemID);

                cmd.ExecuteNonQuery();

                TransferIn clsTransferIn = new TransferIn(Connection, Transaction);
                clsTransferIn.SynchronizeAmount(Details.TransferInID);
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

        public void UpdateReceiveStatus(long TransferInItemID, TransferInItemReceivedStatus TransferInItemReceivedStatus, decimal ReceivedQuantity)
        {
            try
            {
                string SQL = "UPDATE tblTransferInItems SET " +
                                "TransferInItemReceivedStatus   =   @TransferInItemReceivedStatus, " +
                                "OriginalQuantity               =   Quantity, " +
                                "Quantity                       =   @ReceivedQuantity " +
                            "WHERE TransferInItemID = @TransferInItemID;";

                MySqlConnection cn = GetConnection();

                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = cn;
                cmd.Transaction = mTransaction;
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandText = SQL;

                MySqlParameter prmTransferInItemReceivedStatus = new MySqlParameter("@TransferInItemReceivedStatus",MySqlDbType.Int16);
                prmTransferInItemReceivedStatus.Value = TransferInItemReceivedStatus.ToString("d");
                cmd.Parameters.Add(prmTransferInItemReceivedStatus);

                MySqlParameter prmReceivedQuantity = new MySqlParameter("@ReceivedQuantity",MySqlDbType.Decimal);
                prmReceivedQuantity.Value = ReceivedQuantity;
                cmd.Parameters.Add(prmReceivedQuantity);

                MySqlParameter prmTransferInItemID = new MySqlParameter("@TransferInItemID",MySqlDbType.Int64);
                prmTransferInItemID.Value = TransferInItemID;
                cmd.Parameters.Add(prmTransferInItemID);

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

        public void Post(long TransferInID)
        {
            try
            {
                string SQL = "UPDATE tblTransferInItems SET " +
                                "TransferInItemStatus			=	@TransferInItemStatus, " +
                                "TransferInItemReceivedStatus   =   @TransferInItemReceivedStatus " +
                            "WHERE TransferInID = @TransferInID;";

                MySqlConnection cn = GetConnection();

                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = cn;
                cmd.Transaction = mTransaction;
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandText = SQL;

                MySqlParameter prmTransferInItemStatus = new MySqlParameter("@TransferInItemStatus",MySqlDbType.Int16);
                prmTransferInItemStatus.Value = TransferInItemStatus.Posted.ToString("d");
                cmd.Parameters.Add(prmTransferInItemStatus);

                MySqlParameter prmTransferInItemReceivedStatus = new MySqlParameter("@TransferInItemReceivedStatus",MySqlDbType.Int16);
                prmTransferInItemReceivedStatus.Value = TransferInItemReceivedStatus.Received.ToString("d");
                cmd.Parameters.Add(prmTransferInItemReceivedStatus);

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

        public void Cancel(long TransferInID)
        {
            try
            {
                string SQL = "UPDATE tblTransferInItems SET " +
                                "TransferInItemStatus			=	@TransferInItemStatus, " +
                                "TransferInItemReceivedStatus   =   @TransferInItemReceivedStatus " +
                            "WHERE TransferInID = @TransferInID;";

                MySqlConnection cn = GetConnection();

                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = cn;
                cmd.Transaction = mTransaction;
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandText = SQL;

                MySqlParameter prmTransferInItemStatus = new MySqlParameter("@TransferInItemStatus",MySqlDbType.Int16);
                prmTransferInItemStatus.Value = TransferInItemStatus.Cancelled.ToString("d");
                cmd.Parameters.Add(prmTransferInItemStatus);

                MySqlParameter prmTransferInItemReceivedStatus = new MySqlParameter("@TransferInItemReceivedStatus",MySqlDbType.Int16);
                prmTransferInItemReceivedStatus.Value = TransferInItemReceivedStatus.NotYetReceived.ToString("d");
                cmd.Parameters.Add(prmTransferInItemReceivedStatus);

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

        #region Delete

        public bool Delete(string IDs)
        {
            try
            {
                string SQL = "DELETE FROM tblTransferInItems WHERE TransferInItemID IN (" + IDs + ");";

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
                                "TransferInItemID, " +
                                "TransferInID, " +
                                "ProductID, " +
                                "ProductCode, " +
                                "BarCode, " +
                                "Description, " +
                                "ProductUnitID, " +
                                "ProductUnitCode, " +
                                "Quantity, " +
                                "OriginalQuantity, " +
                                "UnitCost, " +
                                "Discount, " +
                                "DiscountApplied, " +
                                "DiscountType, " +
                                "Amount, " +
                                "VAT, " +
                                "VatableAmount, " +
                                "EVAT, " +
                                "EVatableAmount, " +
                                "LocalTax, " +
                                "isVATInclusive, " +
                                "VariationMatrixID, " +
                                "MatrixDescription, " +
                                "ProductGroup, " +
                                "ProductSubGroup, " +
                                "TransferInItemStatus, " +
                                "TransferInItemReceivedStatus, " +
                                "IsVatable, " +
                                "Remarks, " +
                                "ChartOfAccountIDTransferIn, " +
                                "ChartOfAccountIDTaxTransferIn, " +
                                "ChartOfAccountIDInventory, " +
                                "SellingPrice, " +
                                "SellingVAT, " +
                                "SellingEVAT, " +
                                "SellingLocalTax, " +
                                "OldSellingPrice " +
                            "FROM tblTransferInItems ";
            return stSQL;
        }

        #region Details

        public TransferInItemDetails Details(long TransferInItemID)
        {
            try
            {
                string SQL = SQLSelect() + "WHERE TransferInItemID = @TransferInItemID;";

                MySqlConnection cn = GetConnection();

                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = cn;
                cmd.Transaction = mTransaction;
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandText = SQL;

                MySqlParameter prmTransferInItemID = new MySqlParameter("@TransferInItemID",MySqlDbType.Int64);
                prmTransferInItemID.Value = TransferInItemID;
                cmd.Parameters.Add(prmTransferInItemID);

                MySqlDataReader myReader = (MySqlDataReader)cmd.ExecuteReader(System.Data.CommandBehavior.SingleResult);

                TransferInItemDetails Details = new TransferInItemDetails();

                while (myReader.Read())
                {
                    Details.TransferInItemID = TransferInItemID;
                    Details.TransferInID = myReader.GetInt64("TransferInID");
                    Details.ProductID = myReader.GetInt64("ProductID");
                    Details.ProductCode = "" + myReader["ProductCode"].ToString();
                    Details.BarCode = "" + myReader["BarCode"].ToString();
                    Details.Description = "" + myReader["Description"].ToString();
                    Details.ProductUnitID = myReader.GetInt16("ProductUnitID");
                    Details.ProductUnitCode = "" + myReader["ProductUnitCode"].ToString();
                    Details.Quantity = myReader.GetDecimal("Quantity");
                    Details.OriginalQuantity = myReader.GetDecimal("OriginalQuantity");
                    Details.UnitCost = myReader.GetDecimal("UnitCost");
                    Details.Discount = myReader.GetDecimal("Discount");
                    Details.DiscountApplied = myReader.GetDecimal("DiscountApplied");
                    Details.DiscountType = (DiscountTypes)Enum.Parse(typeof(DiscountTypes), myReader.GetString("DiscountType"));
                    Details.Amount = myReader.GetDecimal("Amount");
                    Details.VAT = myReader.GetDecimal("VAT");
                    Details.VatableAmount = myReader.GetDecimal("VatableAmount");
                    Details.EVAT = myReader.GetDecimal("EVAT");
                    Details.EVatableAmount = myReader.GetDecimal("EVatableAmount");
                    Details.LocalTax = myReader.GetDecimal("LocalTax");
                    Details.isVATInclusive = myReader.GetBoolean("isVATInclusive");
                    Details.VariationMatrixID = myReader.GetInt64("VariationMatrixID");
                    Details.MatrixDescription = "" + myReader["MatrixDescription"].ToString();
                    Details.ProductGroup = "" + myReader["ProductGroup"].ToString();
                    Details.ProductSubGroup = "" + myReader["ProductSubGroup"].ToString();
                    Details.TransferInItemStatus = (TransferInItemStatus)Enum.Parse(typeof(TransferInItemStatus), myReader.GetString("TransferInItemStatus"));
                    Details.TransferInItemReceivedStatus = (TransferInItemReceivedStatus)Enum.Parse(typeof(TransferInItemReceivedStatus), myReader.GetString("TransferInItemReceivedStatus"));
                    if (myReader["IsVatable"].ToString() == "1")
                        Details.IsVatable = true;
                    Details.Remarks = "" + myReader["Remarks"].ToString();

                    //Added April 28, 2010 4:20PM : For selling information
                    Details.SellingPrice = myReader.GetDecimal("SellingPrice");
                    Details.SellingVAT = myReader.GetDecimal("SellingVAT");
                    Details.SellingEVAT = myReader.GetDecimal("SellingEVAT");
                    Details.SellingLocalTax = myReader.GetDecimal("SellingLocalTax");
                    Details.OldSellingPrice = myReader.GetDecimal("OldSellingPrice");
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

        public System.Data.DataTable ListAsDataTable(long TransferInID = 0, string SortField = "TransferInItemID", SortOption SortOrder = SortOption.Desscending)
        {
            MySqlCommand cmd = new MySqlCommand();
            string SQL = SQLSelect();

            if (TransferInID != 0)
            {
                SQL += "WHERE TransferInID = @TransferInID ";

                MySqlParameter prmTransferInID = new MySqlParameter("@TransferInID",MySqlDbType.Int64);
                prmTransferInID.Value = TransferInID;
                cmd.Parameters.Add(prmTransferInID);
            }

            SQL += "ORDER BY " + SortField;
            if (SortOrder == SortOption.Ascending)
                SQL += " ASC";
            else
                SQL += " DESC";

            MySqlConnection cn = GetConnection();

            cmd.Connection = cn;
            cmd.Transaction = mTransaction;
            cmd.CommandType = System.Data.CommandType.Text;
            cmd.CommandText = SQL;

            System.Data.DataTable dt = new System.Data.DataTable("TransferInItems");
            MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
            adapter.Fill(dt);

            return dt;

        }

        public MySqlDataReader List(long TransferInID = 0, string SortField = "TransferInItemID", SortOption SortOrder = SortOption.Desscending)
        {
            try
            {
                MySqlCommand cmd = new MySqlCommand();
                string SQL = SQLSelect();

                if (TransferInID != 0)
                {
                    SQL += "WHERE TransferInID = @TransferInID ";

                    MySqlParameter prmTransferInID = new MySqlParameter("@TransferInID",MySqlDbType.Int64);
                    prmTransferInID.Value = TransferInID;
                    cmd.Parameters.Add(prmTransferInID);
                }

                SQL += "ORDER BY " + SortField;
                if (SortOrder == SortOption.Ascending)
                    SQL += " ASC";
                else
                    SQL += " DESC";

                MySqlConnection cn = GetConnection();

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
        public MySqlDataReader List(TransferInItemStatus TransferInItemstatus, string SortField, SortOption SortOrder)
        {
            try
            {
                if (SortField == string.Empty || SortField == null) SortField = "TransferInItemID";

                string SQL = SQLSelect() + "WHERE TransferInItemStatus = @TransferInItemStatus " +
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

                MySqlParameter prmTransferInItemStatus = new MySqlParameter("@TransferInItemStatus",MySqlDbType.Int16);
                prmTransferInItemStatus.Value = TransferInItemstatus.ToString("d");
                cmd.Parameters.Add(prmTransferInItemStatus);

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
                if (SortField == string.Empty || SortField == null) SortField = "TransferInItemID";

                string SQL = SQLSelect() + "WHERE (ProductCode LIKE @SearchKey or BarCode LIKE @SearchKey or Description LIKE @SearchKey " +
                                        "or MatrixDescription LIKE @SearchKey or ProductGroup LIKE @SearchKey or ProductSubGroup LIKE @SearchKey " +
                                        "or Remarks LIKE @SearchKey) " +
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
        public MySqlDataReader Search(TransferInItemStatus TransferInItemstatus, string SearchKey, string SortField, SortOption SortOrder)
        {
            try
            {
                if (SortField == string.Empty || SortField == null) SortField = "TransferInItemID";

                string SQL = SQLSelect() + "WHERE (ProductCode LIKE @SearchKey or BarCode LIKE @SearchKey or Description LIKE @SearchKey " +
                                    "or MatrixDescription LIKE @SearchKey or ProductGroup LIKE @SearchKey or ProductSubGroup LIKE @SearchKey " +
                                    "or Remarks LIKE @SearchKey) " +
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

                MySqlParameter prmTransferInItemStatus = new MySqlParameter("@TransferInItemStatus",MySqlDbType.Int16);
                prmTransferInItemStatus.Value = TransferInItemstatus.ToString("d");
                cmd.Parameters.Add(prmTransferInItemStatus);

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

        #endregion
    }
}


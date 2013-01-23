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

    #region TransferOutItemDetails

    public struct TransferOutItemDetails
    {
        public long TransferOutItemID;
        public long TransferOutID;
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
        public TransferOutItemStatus TransferOutItemStatus;
        public bool IsVatable;
        public string Remarks;
        public int ChartOfAccountIDTransferOut;
        public int ChartOfAccountIDTaxTransferOut;
        public int ChartOfAccountIDInventory;

        // Added April 28, 2010 6:49PM
        public decimal SellingPrice;
        public decimal SellingVAT;
        public decimal SellingEVAT;
        public decimal SellingLocalTax;
        public decimal OldSellingPrice;

        public decimal OriginalQuantity;
        public TransferOutItemReceivedStatus TransferOutItemReceivedStatus;
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
    public class TransferOutItem
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

        public TransferOutItem()
        {

        }

        public TransferOutItem(MySqlConnection Connection, MySqlTransaction Transaction)
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

        public long Insert(TransferOutItemDetails Details)
        {
            try
            {
                string SQL = "INSERT INTO tblTransferOutItems (" +
                                "TransferOutID, " +
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
                                "TransferOutItemStatus, " +
                                "IsVatable, " +
                                "Remarks, " +
                                "ChartOfAccountIDTransferOut, " +
                                "ChartOfAccountIDTaxTransferOut, " +
                                "ChartOfAccountIDInventory," +
                                "SellingPrice," +
                                "SellingVAT," +
                                "SellingEVAT," +
                                "SellingLocalTax," +
                                "OldSellingPrice" +
                            ") VALUES (" +
                                "@TransferOutID, " +
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
                                "@TransferOutItemStatus, " +
                                "@IsVatable, " +
                                "@Remarks, " +
                                "(SELECT ChartOfAccountIDTransferOut FROM tblProducts WHERE ProductID = @ProductID), " +
                                "(SELECT ChartOfAccountIDTaxTransferOut FROM tblProducts WHERE ProductID = @ProductID), " +
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

                MySqlParameter prmTransferOutID = new MySqlParameter("@TransferOutID",MySqlDbType.Int64);
                prmTransferOutID.Value = Details.TransferOutID;
                cmd.Parameters.Add(prmTransferOutID);

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

                MySqlParameter prmTransferOutItemStatus = new MySqlParameter("@TransferOutItemStatus",MySqlDbType.Int16);
                prmTransferOutItemStatus.Value = Details.TransferOutItemStatus.ToString("d");
                cmd.Parameters.Add(prmTransferOutItemStatus);

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

                System.Data.DataTable dt = new System.Data.DataTable("LAST_INSERT_ID");
                MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
                adapter.Fill(dt);

                Int64 iID = 0;
                foreach (System.Data.DataRow dr in dt.Rows)
                {
                    iID = Int64.Parse(dr[0].ToString());
                }

                TransferOut clsTransferOut = new TransferOut(Connection, Transaction);
                clsTransferOut.SynchronizeAmount(Details.TransferOutID);

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

        public void Update(TransferOutItemDetails Details)
        {
            try
            {
                string SQL = "UPDATE tblTransferOutItems SET " +
                                "TransferOutID					=	@TransferOutID, " +
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
                                "TransferOutItemStatus	=	@TransferOutItemStatus, " +
                                "IsVatable				=	@IsVatable, " +
                                "Remarks				=	@Remarks, " +
                                "ChartOfAccountIDTransferOut       = (SELECT ChartOfAccountIDTransferOut FROM tblProducts WHERE ProductID = @ProductID), " +
                                "ChartOfAccountIDTaxTransferOut    = (SELECT ChartOfAccountIDTaxTransferOut FROM tblProducts WHERE ProductID = @ProductID), " +
                                "ChartOfAccountIDInventory      = (SELECT ChartOfAccountIDInventory FROM tblProducts WHERE ProductID = @ProductID), " +
                                "SellingPrice			=	@SellingPrice, " +
                                "SellingVAT				=	@SellingVAT, " +
                                "SellingEVAT			=	@SellingEVAT, " +
                                "SellingLocalTax		=	@SellingLocalTax, " +
                                "OldSellingPrice		=	@OldSellingPrice " +
                            "WHERE TransferOutItemID = @TransferOutItemID;";

                MySqlConnection cn = GetConnection();

                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = cn;
                cmd.Transaction = mTransaction;
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandText = SQL;

                MySqlParameter prmTransferOutID = new MySqlParameter("@TransferOutID",MySqlDbType.Int64);
                prmTransferOutID.Value = Details.TransferOutID;
                cmd.Parameters.Add(prmTransferOutID);

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
                cmd.Parameters.AddWithValue("@TransferOutItemStatus", Details.TransferOutItemStatus.ToString("d"));
                cmd.Parameters.AddWithValue("@IsVatable", Convert.ToInt16(Details.IsVatable));
                cmd.Parameters.AddWithValue("@Remarks", Details.Remarks);
                cmd.Parameters.AddWithValue("@SellingPrice", Details.SellingPrice);
                cmd.Parameters.AddWithValue("@SellingVAT", Details.SellingVAT);
                cmd.Parameters.AddWithValue("@SellingEVAT", Details.SellingEVAT);
                cmd.Parameters.AddWithValue("@SellingLocalTax", Details.SellingLocalTax);
                cmd.Parameters.AddWithValue("@OldSellingPrice", Details.OldSellingPrice);
                cmd.Parameters.AddWithValue("@TransferOutItemID", Details.TransferOutItemID);

                cmd.ExecuteNonQuery();

                TransferOut clsTransferOut = new TransferOut(Connection, Transaction);
                clsTransferOut.SynchronizeAmount(Details.TransferOutID);
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

        public void UpdateReceiveStatus(long TransferOutItemID, TransferOutItemReceivedStatus TransferOutItemReceivedStatus, decimal ReceivedQuantity)
        {
            try
            {
                string SQL = "UPDATE tblTransferOutItems SET " +
                                "TransferOutItemReceivedStatus   =   @TransferOutItemReceivedStatus, " +
                                "OriginalQuantity               =   Quantity, " +
                                "Quantity                       =   @ReceivedQuantity " +
                            "WHERE TransferOutItemID = @TransferOutItemID;";

                MySqlConnection cn = GetConnection();

                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = cn;
                cmd.Transaction = mTransaction;
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandText = SQL;

                MySqlParameter prmTransferOutItemReceivedStatus = new MySqlParameter("@TransferOutItemReceivedStatus",MySqlDbType.Int16);
                prmTransferOutItemReceivedStatus.Value = TransferOutItemReceivedStatus.ToString("d");
                cmd.Parameters.Add(prmTransferOutItemReceivedStatus);

                MySqlParameter prmReceivedQuantity = new MySqlParameter("@ReceivedQuantity",MySqlDbType.Decimal);
                prmReceivedQuantity.Value = ReceivedQuantity;
                cmd.Parameters.Add(prmReceivedQuantity);

                MySqlParameter prmTransferOutItemID = new MySqlParameter("@TransferOutItemID",MySqlDbType.Int64);
                prmTransferOutItemID.Value = TransferOutItemID;
                cmd.Parameters.Add(prmTransferOutItemID);

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

        public void Post(long TransferOutID)
        {
            try
            {
                string SQL = "UPDATE tblTransferOutItems SET " +
                                "TransferOutItemStatus			=	@TransferOutItemStatus, " +
                                "TransferOutItemReceivedStatus   =   @TransferOutItemReceivedStatus " +
                            "WHERE TransferOutID = @TransferOutID;";

                MySqlConnection cn = GetConnection();

                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = cn;
                cmd.Transaction = mTransaction;
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandText = SQL;

                MySqlParameter prmTransferOutItemStatus = new MySqlParameter("@TransferOutItemStatus",MySqlDbType.Int16);
                prmTransferOutItemStatus.Value = TransferOutItemStatus.Posted.ToString("d");
                cmd.Parameters.Add(prmTransferOutItemStatus);

                MySqlParameter prmTransferOutItemReceivedStatus = new MySqlParameter("@TransferOutItemReceivedStatus",MySqlDbType.Int16);
                prmTransferOutItemReceivedStatus.Value = TransferOutItemReceivedStatus.Received.ToString("d");
                cmd.Parameters.Add(prmTransferOutItemReceivedStatus);

                MySqlParameter prmTransferOutID = new MySqlParameter("@TransferOutID",MySqlDbType.Int64);
                prmTransferOutID.Value = TransferOutID;
                cmd.Parameters.Add(prmTransferOutID);

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

        public void Cancel(long TransferOutID)
        {
            try
            {
                string SQL = "UPDATE tblTransferOutItems SET " +
                                "TransferOutItemStatus			=	@TransferOutItemStatus, " +
                                "TransferOutItemReceivedStatus   =   @TransferOutItemReceivedStatus " +
                            "WHERE TransferOutID = @TransferOutID;";

                MySqlConnection cn = GetConnection();

                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = cn;
                cmd.Transaction = mTransaction;
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandText = SQL;

                MySqlParameter prmTransferOutItemStatus = new MySqlParameter("@TransferOutItemStatus",MySqlDbType.Int16);
                prmTransferOutItemStatus.Value = TransferOutItemStatus.Cancelled.ToString("d");
                cmd.Parameters.Add(prmTransferOutItemStatus);

                MySqlParameter prmTransferOutItemReceivedStatus = new MySqlParameter("@TransferOutItemReceivedStatus",MySqlDbType.Int16);
                prmTransferOutItemReceivedStatus.Value = TransferOutItemReceivedStatus.NotYetReceived.ToString("d");
                cmd.Parameters.Add(prmTransferOutItemReceivedStatus);

                MySqlParameter prmTransferOutID = new MySqlParameter("@TransferOutID",MySqlDbType.Int64);
                prmTransferOutID.Value = TransferOutID;
                cmd.Parameters.Add(prmTransferOutID);

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
                string SQL = "DELETE FROM tblTransferOutItems WHERE TransferOutItemID IN (" + IDs + ");";

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
                                "TransferOutItemID, " +
                                "TransferOutID, " +
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
                                "TransferOutItemStatus, " +
                                "TransferOutItemReceivedStatus, " +
                                "IsVatable, " +
                                "Remarks, " +
                                "ChartOfAccountIDTransferOut, " +
                                "ChartOfAccountIDTaxTransferOut, " +
                                "ChartOfAccountIDInventory, " +
                                "SellingPrice, " +
                                "SellingVAT, " +
                                "SellingEVAT, " +
                                "SellingLocalTax, " +
                                "OldSellingPrice " +
                            "FROM tblTransferOutItems ";
            return stSQL;
        }

        #region Details

        public TransferOutItemDetails Details(long TransferOutItemID)
        {
            try
            {
                string SQL = SQLSelect() + "WHERE TransferOutItemID = @TransferOutItemID;";

                MySqlConnection cn = GetConnection();

                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = cn;
                cmd.Transaction = mTransaction;
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandText = SQL;

                MySqlParameter prmTransferOutItemID = new MySqlParameter("@TransferOutItemID",MySqlDbType.Int64);
                prmTransferOutItemID.Value = TransferOutItemID;
                cmd.Parameters.Add(prmTransferOutItemID);

                MySqlDataReader myReader = (MySqlDataReader)cmd.ExecuteReader(System.Data.CommandBehavior.SingleResult);

                TransferOutItemDetails Details = new TransferOutItemDetails();

                while (myReader.Read())
                {
                    Details.TransferOutItemID = TransferOutItemID;
                    Details.TransferOutID = myReader.GetInt64("TransferOutID");
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
                    Details.TransferOutItemStatus = (TransferOutItemStatus)Enum.Parse(typeof(TransferOutItemStatus), myReader.GetString("TransferOutItemStatus"));
                    Details.TransferOutItemReceivedStatus = (TransferOutItemReceivedStatus)Enum.Parse(typeof(TransferOutItemReceivedStatus), myReader.GetString("TransferOutItemReceivedStatus"));
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

        public System.Data.DataTable ListAsDataTable(long TransferOutID = 0, string SortField = "TransferOutItemID", SortOption SortOrder = SortOption.Desscending)
        {
            MySqlCommand cmd = new MySqlCommand();
            string SQL = SQLSelect();

            if (TransferOutID != 0)
            {
                SQL += "WHERE TransferOutID = @TransferOutID ";

                MySqlParameter prmTransferOutID = new MySqlParameter("@TransferOutID",MySqlDbType.Int64);
                prmTransferOutID.Value = TransferOutID;
                cmd.Parameters.Add(prmTransferOutID);
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

            System.Data.DataTable dt = new System.Data.DataTable("TransferOutItems");
            MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
            adapter.Fill(dt);

            return dt;

        }

        public MySqlDataReader List(long TransferOutID = 0, string SortField = "TransferOutItemID", SortOption SortOrder = SortOption.Desscending)
        {
            try
            {
                MySqlCommand cmd = new MySqlCommand();
                string SQL = SQLSelect();

                if (TransferOutID != 0)
                {
                    SQL += "WHERE TransferOutID = @TransferOutID ";

                    MySqlParameter prmTransferOutID = new MySqlParameter("@TransferOutID",MySqlDbType.Int64);
                    prmTransferOutID.Value = TransferOutID;
                    cmd.Parameters.Add(prmTransferOutID);
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
        public MySqlDataReader List(TransferOutItemStatus TransferOutItemstatus, string SortField, SortOption SortOrder)
        {
            try
            {
                if (SortField == string.Empty || SortField == null) SortField = "TransferOutItemID";

                string SQL = SQLSelect() + "WHERE TransferOutItemStatus = @TransferOutItemStatus " +
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

                MySqlParameter prmTransferOutItemStatus = new MySqlParameter("@TransferOutItemStatus",MySqlDbType.Int16);
                prmTransferOutItemStatus.Value = TransferOutItemstatus.ToString("d");
                cmd.Parameters.Add(prmTransferOutItemStatus);

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
                if (SortField == string.Empty || SortField == null) SortField = "TransferOutItemID";

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
        public MySqlDataReader Search(TransferOutItemStatus TransferOutItemstatus, string SearchKey, string SortField, SortOption SortOrder)
        {
            try
            {
                if (SortField == string.Empty || SortField == null) SortField = "TransferOutItemID";

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

                MySqlParameter prmTransferOutItemStatus = new MySqlParameter("@TransferOutItemStatus",MySqlDbType.Int16);
                prmTransferOutItemStatus.Value = TransferOutItemstatus.ToString("d");
                cmd.Parameters.Add(prmTransferOutItemStatus);

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


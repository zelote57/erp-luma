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

    #region CreditMemoItemDetails

    public struct CreditMemoItemDetails
    {
        public long CreditMemoItemID;
        public long CreditMemoID;
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
        public CreditMemoItemStatus ItemStatus;
        public bool IsVatable;
        public string Remarks;
        public int ChartOfAccountIDSold;
        public int ChartOfAccountIDTaxSold;
        public int ChartOfAccountIDInventory;
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
    public class CreditMemoItems
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

        public CreditMemoItems()
        {

        }

        public CreditMemoItems(MySqlConnection Connection, MySqlTransaction Transaction)
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

        public long Insert(CreditMemoItemDetails Details)
        {
            try
            {
                string SQL = "INSERT INTO tblSOCreditMemoItems (" +
                                "CreditMemoID, " +
                                "ProductID, " +
                                "ProductCode, " +
                                "BarCode, " +
                                "Description, " +
                                "ProductUnitID, " +
                                "ProductUnitCode, " +
                                "Quantity, " +
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
                                "ItemStatus, " +
                                "IsVatable, " +
                                "Remarks, " +
                                "ChartOfAccountIDSold, " +
                                "ChartOfAccountIDTaxSold, " +
                                "ChartOfAccountIDInventory" +
                            ") VALUES (" +
                                "@CreditMemoID, " +
                                "@ProductID, " +
                                "@ProductCode, " +
                                "@BarCode, " +
                                "@Description, " +
                                "@ProductUnitID, " +
                                "@ProductUnitCode, " +
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
                                "@ItemStatus, " +
                                "@IsVatable, " +
                                "@Remarks, " +
                                "(SELECT ChartOfAccountIDSold FROM tblProducts WHERE ProductID = @ProductID), " +
                                "(SELECT ChartOfAccountIDTaxSold FROM tblProducts WHERE ProductID = @ProductID), " +
                                "(SELECT ChartOfAccountIDInventory FROM tblProducts WHERE ProductID = @ProductID) " +
                            ");";

                MySqlConnection cn = GetConnection();

                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = cn;
                cmd.Transaction = mTransaction;
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandText = SQL;

                MySqlParameter prmReturnID = new MySqlParameter("@CreditMemoID",MySqlDbType.Int64);
                prmReturnID.Value = Details.CreditMemoID;
                cmd.Parameters.Add(prmReturnID);

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

                MySqlParameter prmQuantity = new MySqlParameter("@Quantity", System.Data.DbType.Decimal);
                prmQuantity.Value = Details.Quantity;
                cmd.Parameters.Add(prmQuantity);

                MySqlParameter prmUnitCost = new MySqlParameter("@UnitCost", System.Data.DbType.Decimal);
                prmUnitCost.Value = Details.UnitCost;
                cmd.Parameters.Add(prmUnitCost);

                MySqlParameter prmDiscount = new MySqlParameter("@Discount", System.Data.DbType.Decimal);
                prmDiscount.Value = Details.Discount;
                cmd.Parameters.Add(prmDiscount);

                MySqlParameter prmDiscountApplied = new MySqlParameter("@DiscountApplied", System.Data.DbType.Decimal);
                prmDiscountApplied.Value = Details.DiscountApplied;
                cmd.Parameters.Add(prmDiscountApplied);

                MySqlParameter prmDiscountType = new MySqlParameter("@DiscountType",MySqlDbType.Int16);
                prmDiscountType.Value = (int)Details.DiscountType;
                cmd.Parameters.Add(prmDiscountType);

                MySqlParameter prmAmount = new MySqlParameter("@Amount", System.Data.DbType.Decimal);
                prmAmount.Value = Details.Amount;
                cmd.Parameters.Add(prmAmount);

                MySqlParameter prmVAT = new MySqlParameter("@VAT", System.Data.DbType.Decimal);
                prmVAT.Value = Details.VAT;
                cmd.Parameters.Add(prmVAT);

                MySqlParameter prmVatableAmount = new MySqlParameter("@VatableAmount", System.Data.DbType.Decimal);
                prmVatableAmount.Value = Details.VatableAmount;
                cmd.Parameters.Add(prmVatableAmount);

                MySqlParameter prmEVAT = new MySqlParameter("@EVAT", System.Data.DbType.Decimal);
                prmEVAT.Value = Details.EVAT;
                cmd.Parameters.Add(prmEVAT);

                MySqlParameter prmEVatableAmount = new MySqlParameter("@EVatableAmount", System.Data.DbType.Decimal);
                prmEVatableAmount.Value = Details.EVatableAmount;
                cmd.Parameters.Add(prmEVatableAmount);

                MySqlParameter prmLocalTax = new MySqlParameter("@LocalTax", System.Data.DbType.Decimal);
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

                MySqlParameter prmCreditMemoItemStatus = new MySqlParameter("@ItemStatus",MySqlDbType.Int16);
                prmCreditMemoItemStatus.Value = Details.ItemStatus.ToString("d");
                cmd.Parameters.Add(prmCreditMemoItemStatus);

                MySqlParameter prmIsVatable = new MySqlParameter("@IsVatable",MySqlDbType.Int16);
                prmIsVatable.Value = Convert.ToInt16(Details.IsVatable);
                cmd.Parameters.Add(prmIsVatable);

                MySqlParameter prmRemarks = new MySqlParameter("@Remarks",MySqlDbType.String);
                prmRemarks.Value = Details.Remarks;
                cmd.Parameters.Add(prmRemarks);

                cmd.ExecuteNonQuery();

                SQL = "SELECT LAST_INSERT_ID();";

                cmd.Parameters.Clear();
                cmd.CommandText = SQL;

                MySqlDataReader myReader = (MySqlDataReader)cmd.ExecuteReader(System.Data.CommandBehavior.SingleResult);

                Int64 iID = 0;

                while (myReader.Read())
                {
                    iID = myReader.GetInt64(0);
                }

                myReader.Close();

                CreditMemos clsCreditMemos = new CreditMemos(Connection, Transaction);
                clsCreditMemos.SynchronizeAmount(Details.CreditMemoID);

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

        public void Update(CreditMemoItemDetails Details)
        {
            try
            {
                string SQL = "UPDATE tblSOCreditMemoItems SET " +
                                "CreditMemoID		    =	@CreditMemoID, " +
                                "ProductID				=	@ProductID, " +
                                "ProductCode			=	@ProductCode, " +
                                "BarCode				=	@BarCode, " +
                                "Description			=	@Description, " +
                                "ProductUnitID			=	@ProductUnitID, " +
                                "ProductUnitCode		=	@ProductUnitCode, " +
                                "Quantity				=	@Quantity, " +
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
                                "ItemStatus			    =	@ItemStatus, " +
                                "IsVatable				=	@IsVatable, " +
                                "Remarks				=	@Remarks, " +
                                "ChartOfAccountIDSold       = (SELECT ChartOfAccountIDSold FROM tblProducts WHERE ProductID = @ProductID), " +
                                "ChartOfAccountIDTaxSold    = (SELECT ChartOfAccountIDTaxSold FROM tblProducts WHERE ProductID = @ProductID), " +
                                "ChartOfAccountIDInventory  = (SELECT ChartOfAccountIDInventory FROM tblProducts WHERE ProductID = @ProductID) " +
                            "WHERE CreditMemoItemID = @CreditMemoItemID;";

                MySqlConnection cn = GetConnection();

                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = cn;
                cmd.Transaction = mTransaction;
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandText = SQL;

                MySqlParameter prmReturnID = new MySqlParameter("@CreditMemoID",MySqlDbType.Int64);
                prmReturnID.Value = Details.CreditMemoID;
                cmd.Parameters.Add(prmReturnID);

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

                MySqlParameter prmQuantity = new MySqlParameter("@Quantity", System.Data.DbType.Decimal);
                prmQuantity.Value = Details.Quantity;
                cmd.Parameters.Add(prmQuantity);

                MySqlParameter prmUnitCost = new MySqlParameter("@UnitCost", System.Data.DbType.Decimal);
                prmUnitCost.Value = Details.UnitCost;
                cmd.Parameters.Add(prmUnitCost);

                MySqlParameter prmDiscount = new MySqlParameter("@Discount", System.Data.DbType.Decimal);
                prmDiscount.Value = Details.Discount;
                cmd.Parameters.Add(prmDiscount);

                MySqlParameter prmDiscountApplied = new MySqlParameter("@DiscountApplied", System.Data.DbType.Decimal);
                prmDiscountApplied.Value = Details.DiscountApplied;
                cmd.Parameters.Add(prmDiscountApplied);

                MySqlParameter prmDiscountType = new MySqlParameter("@DiscountType",MySqlDbType.Int16);
                prmDiscountType.Value = (int)Details.DiscountType;
                cmd.Parameters.Add(prmDiscountType);

                MySqlParameter prmAmount = new MySqlParameter("@Amount", System.Data.DbType.Decimal);
                prmAmount.Value = Details.Amount;
                cmd.Parameters.Add(prmAmount);

                MySqlParameter prmVAT = new MySqlParameter("@VAT", System.Data.DbType.Decimal);
                prmVAT.Value = Details.VAT;
                cmd.Parameters.Add(prmVAT);

                MySqlParameter prmVatableAmount = new MySqlParameter("@VatableAmount", System.Data.DbType.Decimal);
                prmVatableAmount.Value = Details.VatableAmount;
                cmd.Parameters.Add(prmVatableAmount);

                MySqlParameter prmEVAT = new MySqlParameter("@EVAT", System.Data.DbType.Decimal);
                prmEVAT.Value = Details.EVAT;
                cmd.Parameters.Add(prmEVAT);

                MySqlParameter prmEVatableAmount = new MySqlParameter("@EVatableAmount", System.Data.DbType.Decimal);
                prmEVatableAmount.Value = Details.EVatableAmount;
                cmd.Parameters.Add(prmEVatableAmount);

                MySqlParameter prmLocalTax = new MySqlParameter("@LocalTax", System.Data.DbType.Decimal);
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

                MySqlParameter prmCreditMemoItemStatus = new MySqlParameter("@ItemStatus",MySqlDbType.Int16);
                prmCreditMemoItemStatus.Value = Details.ItemStatus.ToString("d");
                cmd.Parameters.Add(prmCreditMemoItemStatus);

                MySqlParameter prmIsVatable = new MySqlParameter("@IsVatable",MySqlDbType.Int16);
                prmIsVatable.Value = Details.IsVatable;
                cmd.Parameters.Add(prmIsVatable);

                MySqlParameter prmRemarks = new MySqlParameter("@Remarks",MySqlDbType.String);
                prmRemarks.Value = Details.Remarks;
                cmd.Parameters.Add(prmRemarks);

                MySqlParameter prmCreditMemoItemID = new MySqlParameter("@CreditMemoItemID",MySqlDbType.Int64);
                prmCreditMemoItemID.Value = Details.CreditMemoItemID;
                cmd.Parameters.Add(prmCreditMemoItemID);

                cmd.ExecuteNonQuery();

                CreditMemos clsCreditMemos = new CreditMemos(Connection, Transaction);
                clsCreditMemos.SynchronizeAmount(Details.CreditMemoID);
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

        public void Post(long CreditMemoID)
        {
            try
            {
                string SQL = "UPDATE tblSOCreditMemoItems SET " +
                                "ItemStatus			=	@ItemStatus " +
                            "WHERE CreditMemoID = @CreditMemoID;";

                MySqlConnection cn = GetConnection();

                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = cn;
                cmd.Transaction = mTransaction;
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandText = SQL;

                MySqlParameter prmItemStatus = new MySqlParameter("@ItemStatus",MySqlDbType.Int16);
                prmItemStatus.Value = CreditMemoItemStatus.Posted.ToString("d");
                cmd.Parameters.Add(prmItemStatus);

                MySqlParameter prmCreditMemoID = new MySqlParameter("@CreditMemoID",MySqlDbType.Int64);
                prmCreditMemoID.Value = CreditMemoID;
                cmd.Parameters.Add(prmCreditMemoID);

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

        public void Cancel(long CreditMemoID)
        {
            try
            {
                string SQL = "UPDATE tblSOCreditMemoItems SET " +
                                "ItemStatus			=	@ItemStatus " +
                            "WHERE CreditMemoID = @CreditMemoID;";

                MySqlConnection cn = GetConnection();

                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = cn;
                cmd.Transaction = mTransaction;
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandText = SQL;

                MySqlParameter prmItemStatus = new MySqlParameter("@ItemStatus",MySqlDbType.Int16);
                prmItemStatus.Value = CreditMemoItemStatus.Cancelled.ToString("d");
                cmd.Parameters.Add(prmItemStatus);

                MySqlParameter prmCreditMemoID = new MySqlParameter("@CreditMemoID",MySqlDbType.Int64);
                prmCreditMemoID.Value = CreditMemoID;
                cmd.Parameters.Add(prmCreditMemoID);

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
                string SQL = "DELETE FROM tblSOCreditMemoItems WHERE CreditMemoItemID IN (" + IDs + ");";

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
                                "CreditMemoItemID, " +
                                "CreditMemoID, " +
                                "ProductID, " +
                                "ProductCode, " +
                                "BarCode, " +
                                "Description, " +
                                "ProductUnitID, " +
                                "ProductUnitCode, " +
                                "Quantity, " +
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
                                "ItemStatus, " +
                                "IsVatable, " +
                                "Remarks, " +
                                "ChartOfAccountIDSold, " +
                                "ChartOfAccountIDTaxSold, " +
                                "ChartOfAccountIDInventory " +
                            "FROM tblSOCreditMemoItems ";
            return stSQL;
        }

        #region Details

        public CreditMemoItemDetails Details(long CreditMemoItemID)
        {
            try
            {
                string SQL = SQLSelect() + "WHERE CreditMemoItemID = @CreditMemoItemID;";

                MySqlConnection cn = GetConnection();

                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = cn;
                cmd.Transaction = mTransaction;
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandText = SQL;

                MySqlParameter prmCreditMemoItemID = new MySqlParameter("@CreditMemoItemID",MySqlDbType.Int64);
                prmCreditMemoItemID.Value = CreditMemoItemID;
                cmd.Parameters.Add(prmCreditMemoItemID);

                MySqlDataReader myReader = (MySqlDataReader)cmd.ExecuteReader(System.Data.CommandBehavior.SingleResult);

                CreditMemoItemDetails Details = new CreditMemoItemDetails();

                while (myReader.Read())
                {
                    Details.CreditMemoItemID = CreditMemoItemID;
                    Details.CreditMemoID = myReader.GetInt64("CreditMemoID");
                    Details.ProductID = myReader.GetInt64("ProductID");
                    Details.ProductCode = "" + myReader["ProductCode"].ToString();
                    Details.BarCode = "" + myReader["BarCode"].ToString();
                    Details.Description = "" + myReader["Description"].ToString();
                    Details.ProductUnitID = myReader.GetInt16("ProductUnitID");
                    Details.ProductUnitCode = "" + myReader["ProductUnitCode"].ToString();
                    Details.Quantity = myReader.GetDecimal("Quantity");
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
                    Details.ItemStatus = (CreditMemoItemStatus)Enum.Parse(typeof(CreditMemoItemStatus), myReader.GetString("ItemStatus"));
                    if (myReader["IsVatable"].ToString() == "1")
                        Details.IsVatable = true;
                    Details.Remarks = "" + myReader["Remarks"].ToString();
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

        public System.Data.DataTable DataList(string SortField, SortOption SortOrder)
        {
            if (SortField == string.Empty || SortField == null) SortField = "CreditMemoItemID";

            string SQL = SQLSelect() + "ORDER BY " + SortField;

            if (SortOrder == SortOption.Ascending)
                SQL += " ASC";
            else
                SQL += " DESC";

            MySqlConnection cn = GetConnection();
            System.Data.DataTable dt = new System.Data.DataTable("tblSOCreditMemoItems");
            MySqlDataAdapter adapter = new MySqlDataAdapter(SQL, cn);
            adapter.Fill(dt);

            return dt;

        }

        public MySqlDataReader List(string SortField, SortOption SortOrder)
        {
            try
            {
                if (SortField == string.Empty || SortField == null) SortField = "CreditMemoItemID";

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

        public MySqlDataReader List(long CreditMemoID, string SortField, SortOption SortOrder)
        {
            try
            {
                if (SortField == string.Empty || SortField == null) SortField = "CreditMemoItemID";

                string SQL = SQLSelect() + "WHERE CreditMemoID = @CreditMemoID ORDER BY " + SortField;

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

                MySqlParameter prmReturnID = new MySqlParameter("@CreditMemoID",MySqlDbType.Int64);
                prmReturnID.Value = CreditMemoID;
                cmd.Parameters.Add(prmReturnID);

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

        public MySqlDataReader List(CreditMemoItemStatus CreditMemoItemstatus, string SortField, SortOption SortOrder)
        {
            try
            {
                if (SortField == string.Empty || SortField == null) SortField = "CreditMemoItemID";

                string SQL = SQLSelect() + "WHERE ItemStatus = @ItemStatus " +
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

                MySqlParameter prmCreditMemoItemStatus = new MySqlParameter("@ItemStatus",MySqlDbType.Int16);
                prmCreditMemoItemStatus.Value = CreditMemoItemstatus.ToString("d");
                cmd.Parameters.Add(prmCreditMemoItemStatus);

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
                if (SortField == string.Empty || SortField == null) SortField = "CreditMemoItemID";

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

        public MySqlDataReader Search(CreditMemoItemStatus CreditMemoItemstatus, string SearchKey, string SortField, SortOption SortOrder)
        {
            try
            {
                if (SortField == string.Empty || SortField == null) SortField = "CreditMemoItemID";

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

                MySqlParameter prmCreditMemoItemStatus = new MySqlParameter("@ItemStatus",MySqlDbType.Int16);
                prmCreditMemoItemStatus.Value = CreditMemoItemstatus.ToString("d");
                cmd.Parameters.Add(prmCreditMemoItemStatus);

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


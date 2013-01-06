using System;
using System.Security.Permissions;
using System.Collections;
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
    public struct SalesTransactionItemDetails
    {
        public Int64 TransactionItemsID; //1
        public string ItemNo; //2
        public Int64 TransactionID; //3
        public Int64 ProductID; //4
        public string ProductCode; //5
        public string BarCode; //6
        public string Description; //7
        public Int32 ProductUnitID; //8
        public string ProductUnitCode; //9
        public decimal Quantity; //10
        public decimal Price; //11
        public decimal Discount; //12
        public decimal ItemDiscount; //13
        public decimal Amount; //15
        public decimal VAT; //16
        public decimal EVAT; //17
        public decimal LocalTax; //18
        public decimal PackageQuantity; //29
        public decimal PromoQuantity; //30
        public decimal PromoValue; //31
        public decimal PromoApplied; //33
        public decimal PurchasePrice; //35
        public decimal PurchaseAmount; //36
        public DiscountTypes ItemDiscountType; //14
        public Int64 VariationsMatrixID; //19
        public string MatrixDescription; //20
        public string ProductGroup; //21
        public string ProductSubGroup; //22
        public DateTime TransactionDate; //23
        public TransactionItemStatus TransactionItemStatus; //24
        public string DiscountCode; // 25
        public string DiscountRemarks; //26
        public Int64 ProductPackageID; //27
        public Int64 MatrixPackageID; //28

        public Int16 PromoInPercent; //32

        public PromoTypes PromoType; //34
        public bool IncludeInSubtotalDiscount;
        public OrderSlipPrinter OrderSlipPrinter;
        public bool OrderSlipPrinted;
        public decimal PercentageCommision;
        public decimal Commision;

        public decimal ScannedQty;
        public decimal ScannedAmt;

        public int PaxNo;
    }

    public struct SalesTransactionItemColumns
    {
        public bool TransactionItemsID; //1
        public bool ItemNo; //2
        public bool TransactionID; //3
        public bool ProductID; //4
        public bool ProductCode; //5
        public bool BarCode; //6
        public bool Description; //7
        public bool ProductUnitID; //8
        public bool ProductUnitCode; //9
        public bool Quantity; //10
        public bool Price; //11
        public bool Discount; //12
        public bool ItemDiscount; //13
        public bool Amount; //15
        public bool VAT; //16
        public bool EVAT; //17
        public bool LocalTax; //18
        public bool PackageQuantity; //29
        public bool PromoQuantity; //30
        public bool PromoValue; //31
        public bool PromoApplied; //33
        public bool PurchasePrice; //35
        public bool PurchaseAmount; //36
        public bool ItemDiscountType; //14
        public bool VariationsMatrixID; //19
        public bool MatrixDescription; //20
        public bool ProductGroup; //21
        public bool ProductSubGroup; //22
        public bool TransactionDate; //23
        public bool TransactionItemStatus; //24
        public bool DiscountCode; // 25
        public bool DiscountRemarks; //26
        public bool ProductPackageID; //27
        public bool MatrixPackageID; //28

        public bool PromoInPercent; //32

        public bool PromoType; //34
        public bool IncludeInSubtotalDiscount;
        public bool OrderSlipPrinter;
        public bool OrderSlipPrinted;
        public bool PercentageCommision;
        public bool Commision;

        public bool ScannedQty;
        public bool ScannedAmt;

        public bool PaxNo;
    }

    public struct SalesTransactionItemColumnNames
    {
        public const string TransactionItemsID = "TransactionItemsID";
        public const string ItemNo = "ItemNo";
        public const string TransactionID = "TransactionID";
        public const string ProductID = "ProductID";
        public const string ProductCode = "ProductCode";
        public const string BarCode = "BarCode";
        public const string Description = "Description";
        public const string ProductUnitID = "ProductUnitID";
        public const string ProductUnitCode = "ProductUnitCode";
        public const string Quantity = "Quantity";
        public const string Price = "Price";
        public const string Discount = "Discount";
        public const string ItemDiscount = "ItemDiscount";
        public const string Amount = "Amount";
        public const string VAT = "VAT";
        public const string EVAT = "EVAT";
        public const string LocalTax = "LocalTax";
        public const string PackageQuantity = "PackageQuantity";
        public const string PromoQuantity = "PromoQuantity";
        public const string PromoValue = "PromoValue";
        public const string PromoApplied = "PromoApplied";
        public const string PurchasePrice = "PurchasePrice";
        public const string PurchaseAmount = "PurchaseAmount";
        public const string ItemDiscountType = "ItemDiscountType";
        public const string VariationsMatrixID = "VariationsMatrixID";
        public const string MatrixDescription = "MatrixDescription";
        public const string ProductGroup = "ProductGroup";
        public const string ProductSubGroup = "ProductSubGroup";
        public const string TransactionDate = "TransactionDate";
        public const string TransactionItemStatus = "TransactionItemStatus";
        public const string DiscountCode = "DiscountCode";
        public const string DiscountRemarks = "DiscountRemarks";
        public const string ProductPackageID = "ProductPackageID";
        public const string MatrixPackageID = "MatrixPackageID";
        
        public const string PromoInPercent = "PromoInPercent";
        
        public const string PromoType = "PromoType";
        public const string IncludeInSubtotalDiscount = "IncludeInSubtotalDiscount";
        public const string OrderSlipPrinter = "OrderSlipPrinter";
        public const string OrderSlipPrinted = "OrderSlipPrinted";
        public const string PercentageCommision = "PercentageCommision";
        public const string Commision = "Commision";
        
        public const string ScannedQty = "ScannedQty";
        public const string ScannedAmt = "ScannedAmt";
        
        public const string PaxNo = "PaxNo";

    }

    [StrongNameIdentityPermissionAttribute(SecurityAction.LinkDemand,
         PublicKey = "002400000480000094000000060200000024000" +
         "052534131000400000100010053D785642F9F960B43157E0380" +
         "F393BEE53E8DFAFBF441366C1B6F8B48D9DDF0D527B1F3B21EA" +
         "E85D2FDB664CE81EB8A87DBE4C589D6F4202FE2B7D4B978BB69" +
         "684874612CB9B8DB7A0339400A9C4E68277884B07817363D242" +
         "E3696F9FACDBEA831810AE6DC9EDCA91A7B5DA12FE7BF65D113" +
         "FF52834EAFB5A7A1FDFD5851A3")]
    public class SalesTransactionItems
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

        public SalesTransactionItems()
        {

        }

        public SalesTransactionItems(MySqlConnection Connection, MySqlTransaction Transaction)
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
                    mConnection.Close();
                    mConnection.Dispose();
                }
            }
        }


        #endregion

        public MySqlConnection GetConnection()
        {
            if (mConnection == null)
            {
                mConnection = new MySqlConnection(AceSoft.RetailPlus.DBConnection.ConnectionString());
                mConnection.Open();

                mTransaction = (MySqlTransaction)mConnection.BeginTransaction();
                IsInTransaction = true;
            }

            return mConnection;
        }


        #region Insert and Update

        public Int64 Insert(SalesTransactionItemDetails Details)
        {
            try
            {
                string SQL = "INSERT INTO tblTransactionItems (" +
                                "TransactionID, " +
                                "ProductID, " +
                                "ProductCode, " +
                                "BarCode, " +
                                "Description, " +
                                "ProductUnitID, " +
                                "ProductUnitCode, " +
                                "Quantity, " +
                                "Price, " +
                                "SellingPrice, " +
                                "Discount, " +
                                "ItemDiscount, " +
                                "ItemDiscountType, " +
                                "Amount, " +
                                "VAT, " +
                                "VatableAmount, " +
                                "EVAT, " +
                                "LocalTax, " +
                                "VariationsMatrixID, " +
                                "MatrixDescription, " +
                                "ProductGroup, " +
                                "ProductSubGroup, " +
                                "TransactionItemStatus," +
                                "DiscountCode," +
                                "DiscountRemarks," +
                                "ProductPackageID," +
                                "MatrixPackageID, " +
                                "PackageQuantity," +
                                "PromoQuantity," +
                                "PromoValue," +
                                "PromoInPercent," +
                                "PromoType," +
                                "PromoApplied," +
                                "PurchasePrice," +
                                "PurchaseAmount," +
                                "IncludeInSubtotalDiscount," +
                                "OrderSlipPrinter," +
                                "OrderSlipPrinted," +
                                "PercentageCommision," +
                                "Commision" +
                            ")VALUES(" +
                                "@TransactionID, " +
                                "@ProductID, " +
                                "@ProductCode, " +
                                "@BarCode, " +
                                "@Description, " +
                                "@ProductUnitID, " +
                                "@ProductUnitCode, " +
                                "@Quantity, " +
                                "@Price, " +
                                "@Price, " +
                                "@Discount, " +
                                "@ItemDiscount, " +
                                "@ItemDiscType, " +
                                "@Amount, " +
                                "@VAT, " +
                                "@Amount/(1+(@VAT/100)), " +
                                "@EVAT, " +
                                "@LocalTax, " +
                                "@VariationsMatrixID, " +
                                "@MatrixDescription, " +
                                "@ProductGroup, " +
                                "@ProductSubGroup, " +
                                "@TransactionItemStatus," +
                                "@DiscCode, " +
                                "@DiscRemarks, " +
                                "@ProductPackageID," +
                                "@MatrixPackageID, " +
                                "@PackageQuantity," +
                                "@PromoQuantity," +
                                "@PromoValue," +
                                "@PromoInPercent," +
                                "@PromoType," +
                                "@PromoApplied," +
                                "@PurchasePrice," +
                                "@PurchaseAmount," +
                                "@IncludeInSubtotalDiscount," +
                                "@OrderSlipPrinter," +
                                "@OrderSlipPrinted," +
                                "@PercentageCommision," +
                                "@Commision);";

                MySqlConnection cn = GetConnection();

                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = cn;
                cmd.Transaction = mTransaction;
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandText = SQL;

                cmd.Parameters.AddWithValue("@TransactionID", Details.TransactionID);
                cmd.Parameters.AddWithValue("@ProductID", Details.ProductID);
                cmd.Parameters.AddWithValue("@ProductCode", Details.ProductCode);
                cmd.Parameters.AddWithValue("@BarCode", Details.BarCode);
                cmd.Parameters.AddWithValue("@Description", Details.Description);
                cmd.Parameters.AddWithValue("@ProductUnitID", Details.ProductUnitID);
                cmd.Parameters.AddWithValue("@ProductUnitCode", Details.ProductUnitCode);
                cmd.Parameters.AddWithValue("@Quantity", Details.Quantity);
                cmd.Parameters.AddWithValue("@Price", Details.Price);
                cmd.Parameters.AddWithValue("@Discount", Details.Discount);
                cmd.Parameters.AddWithValue("@ItemDiscount", Details.ItemDiscount);
                cmd.Parameters.AddWithValue("@ItemDiscType", Convert.ToInt16(Details.ItemDiscountType.ToString("d")));
                cmd.Parameters.AddWithValue("@Amount", Details.Amount);
                cmd.Parameters.AddWithValue("@VAT", Details.VAT);
                cmd.Parameters.AddWithValue("@EVAT", Details.EVAT);
                cmd.Parameters.AddWithValue("@LocalTax", Details.LocalTax);
                cmd.Parameters.AddWithValue("@VariationsMatrixID", Details.VariationsMatrixID);
                if (Details.MatrixDescription == null) Details.MatrixDescription = "";
                cmd.Parameters.AddWithValue("@MatrixDescription", Details.MatrixDescription);
                cmd.Parameters.AddWithValue("@ProductGroup", Details.ProductGroup);
                cmd.Parameters.AddWithValue("@ProductSubGroup", Details.ProductSubGroup);
                cmd.Parameters.AddWithValue("@TransactionItemStatus", Convert.ToInt16(Details.TransactionItemStatus.ToString("d")));
                if (Details.DiscountCode == null) Details.DiscountCode = "";
                cmd.Parameters.AddWithValue("@DiscCode", Details.DiscountCode);
                if (Details.DiscountRemarks == null) Details.DiscountRemarks = "";
                cmd.Parameters.AddWithValue("@DiscRemarks", Details.DiscountRemarks);
                cmd.Parameters.AddWithValue("@ProductPackageID", Details.ProductPackageID);
                cmd.Parameters.AddWithValue("@MatrixPackageID", Details.MatrixPackageID);
                cmd.Parameters.AddWithValue("@PackageQuantity", Details.PackageQuantity);
                cmd.Parameters.AddWithValue("@PromoQuantity", Details.PromoQuantity);
                cmd.Parameters.AddWithValue("@PromoValue", Details.PromoValue);
                cmd.Parameters.AddWithValue("@PromoInPercent", Details.PromoInPercent);
                cmd.Parameters.AddWithValue("@PromoType", (int)Details.PromoType);
                cmd.Parameters.AddWithValue("@PromoApplied", Details.PromoApplied);
                cmd.Parameters.AddWithValue("@PurchasePrice", Details.PurchasePrice);
                cmd.Parameters.AddWithValue("@PurchaseAmount", Details.PurchaseAmount);
                cmd.Parameters.AddWithValue("@IncludeInSubtotalDiscount", Details.IncludeInSubtotalDiscount);
                cmd.Parameters.AddWithValue("@OrderSlipPrinter", Convert.ToInt16(Details.OrderSlipPrinter));
                cmd.Parameters.AddWithValue("@OrderSlipPrinted", Convert.ToInt16(Details.OrderSlipPrinted));
                cmd.Parameters.AddWithValue("@PercentageCommision", Details.PercentageCommision);
                cmd.Parameters.AddWithValue("@Commision", Details.Commision);

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

                return iID;
            }

            catch (Exception ex)
            {
                TransactionFailed = true;
                if (IsInTransaction)
                {
                    mTransaction.Rollback();
                    mConnection.Close();
                    mConnection.Dispose();
                }

                throw ex;
            }
        }
        public void Update(SalesTransactionItemDetails Details)
        {
            try
            {
                string SQL = "UPDATE tblTransactionItems SET " +
                                "TransactionID				=	@TransactionID, " +
                                "ProductID					=	@ProductID, " +
                                "ProductCode				=	@ProductCode, " +
                                "BarCode					=	@BarCode, " +
                                "Description				=	@Description, " +
                                "ProductUnitID				=	@ProductUnitID, " +
                                "ProductUnitCode			=	@ProductUnitCode, " +
                                "Quantity					=	@Quantity, " +
                                "Price						=	@Price, " +
                                "Discount					=	@Discount, " +
                                "ItemDiscount				=	@ItemDiscount, " +
                                "ItemDiscountType			=	@ItemDiscType, " +
                                "Amount						=	@Amount, " +
                                "VAT						=	@VAT, " +
                                "VatableAmount				=	@Amount/(1+(@VAT/100)), " +
                                "EVAT						=	@EVAT, " +
                                "LocalTax					=	@LocalTax, " +
                                "VariationsMatrixID			=	@VariationsMatrixID,  " +
                                "MatrixDescription			=	@MatrixDescription, " +
                                "ProductGroup				=	@ProductGroup, " +
                                "ProductSubGroup			=	@ProductSubGroup,  " +
                                "TransactionItemStatus		=	@TransactionItemStatus, " +
                                "DiscountCode				=	@DiscCode,  " +
                                "DiscountRemarks			=	@DiscRemarks, " +
                                "ProductPackageID			=	@ProductPackageID,  " +
                                "MatrixPackageID			=	@MatrixPackageID,  " +
                                "PackageQuantity			=	@PackageQuantity, " +
                                "PromoQuantity				=	@PromoQuantity, " +
                                "PromoValue					=	@PromoValue, " +
                                "PromoInPercent				=	@PromoInPercent, " +
                                "PromoType					=	@PromoType, " +
                                "PromoApplied				=	@PromoApplied, " +
                                "PurchasePrice				=	@PurchasePrice, " +
                                "PurchaseAmount				=	@PurchaseAmount, " +
                                "IncludeInSubtotalDiscount	=	@IncludeInSubtotalDiscount, " +
                                "OrderSlipPrinter           =   @OrderSlipPrinter, " +
                                "PercentageCommision        =   @PercentageCommision, " +
                                "Commision                  =   @Commision " +
                            "WHERE TransactionItemsID		=	@TransactionItemsID; ";

                MySqlConnection cn = GetConnection();

                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = cn;
                cmd.Transaction = mTransaction;
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandText = SQL;

                cmd.Parameters.AddWithValue("@TransactionID", Details.TransactionID);
                cmd.Parameters.AddWithValue("@ProductID", Details.ProductID);
                cmd.Parameters.AddWithValue("@ProductCode", Details.ProductCode);
                cmd.Parameters.AddWithValue("@BarCode", Details.BarCode);
                cmd.Parameters.AddWithValue("@Description", Details.Description);
                cmd.Parameters.AddWithValue("@ProductUnitID", Details.ProductUnitID);
                cmd.Parameters.AddWithValue("@ProductUnitCode", Details.ProductUnitCode);
                cmd.Parameters.AddWithValue("@Quantity", Details.Quantity);
                cmd.Parameters.AddWithValue("@Price", Details.Price);
                cmd.Parameters.AddWithValue("@Discount", Details.Discount);
                cmd.Parameters.AddWithValue("@ItemDiscount", Details.ItemDiscount);
                cmd.Parameters.AddWithValue("@ItemDiscType", Convert.ToInt16(Details.ItemDiscountType.ToString("d")));
                cmd.Parameters.AddWithValue("@Amount", Details.Amount);
                cmd.Parameters.AddWithValue("@VAT", Details.VAT);
                cmd.Parameters.AddWithValue("@EVAT", Details.EVAT);
                cmd.Parameters.AddWithValue("@LocalTax", Details.LocalTax);
                cmd.Parameters.AddWithValue("@VariationsMatrixID", Details.VariationsMatrixID);
                if (Details.MatrixDescription == null) Details.MatrixDescription = "";
                cmd.Parameters.AddWithValue("@MatrixDescription", Details.MatrixDescription);
                cmd.Parameters.AddWithValue("@ProductGroup", Details.ProductGroup);
                cmd.Parameters.AddWithValue("@ProductSubGroup", Details.ProductSubGroup);
                cmd.Parameters.AddWithValue("@TransactionItemStatus", Convert.ToInt16(Details.TransactionItemStatus.ToString("d")));
                if (Details.DiscountCode == null) Details.DiscountCode = "";
                cmd.Parameters.AddWithValue("@DiscCode", Details.DiscountCode);
                if (Details.DiscountRemarks == null) Details.DiscountRemarks = "";
                cmd.Parameters.AddWithValue("@DiscRemarks", Details.DiscountRemarks);
                cmd.Parameters.AddWithValue("@ProductPackageID", Details.ProductPackageID);
                cmd.Parameters.AddWithValue("@MatrixPackageID", Details.MatrixPackageID);
                cmd.Parameters.AddWithValue("@PackageQuantity", Details.PackageQuantity);
                cmd.Parameters.AddWithValue("@PromoQuantity", Details.PromoQuantity);
                cmd.Parameters.AddWithValue("@PromoValue", Details.PromoValue);
                cmd.Parameters.AddWithValue("@PromoInPercent", Details.PromoInPercent);
                cmd.Parameters.AddWithValue("@PromoType", (int)Details.PromoType);
                cmd.Parameters.AddWithValue("@PromoApplied", Details.PromoApplied);
                cmd.Parameters.AddWithValue("@PurchasePrice", Details.PurchasePrice);
                cmd.Parameters.AddWithValue("@PurchaseAmount", Details.PurchaseAmount);
                cmd.Parameters.AddWithValue("@IncludeInSubtotalDiscount", Details.IncludeInSubtotalDiscount);
                cmd.Parameters.AddWithValue("@OrderSlipPrinter", Convert.ToInt16(Details.OrderSlipPrinter.ToString("d")));
                cmd.Parameters.AddWithValue("@OrderSlipPrinted", Convert.ToInt16(Details.OrderSlipPrinted));
                cmd.Parameters.AddWithValue("@PercentageCommision", Details.PercentageCommision);
                cmd.Parameters.AddWithValue("@Commision", Details.Commision);
                cmd.Parameters.AddWithValue("@TransactionItemsID", Details.TransactionItemsID);

                cmd.ExecuteNonQuery();
            }

            catch (Exception ex)
            {
                TransactionFailed = true;
                if (IsInTransaction)
                {
                    mTransaction.Rollback();
                    mConnection.Close();
                    mConnection.Dispose();
                }

                throw ex;
            }
        }

        public void UpdateOrderSlipPrinted(bool IsOrderSlipPrinted, long TransactionItemsID, DateTime TransactionDate)
        {
            try
            {
                string SQL = "UPDATE tblTransactionItems SET " +
                                "OrderSlipPrinted           =   @OrderSlipPrinted " +
                            "WHERE TransactionItemsID		=	@TransactionItemsID; ";

                MySqlConnection cn = GetConnection();

                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = cn;
                cmd.Transaction = mTransaction;
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandText = SQL;

                cmd.Parameters.AddWithValue("@OrderSlipPrinted", Convert.ToInt16(IsOrderSlipPrinted));
                cmd.Parameters.AddWithValue("@TransactionItemsID", TransactionItemsID);

                cmd.ExecuteNonQuery();
            }

            catch (Exception ex)
            {
                TransactionFailed = true;
                if (IsInTransaction)
                {
                    mTransaction.Rollback();
                    mConnection.Close();
                    mConnection.Dispose();
                }

                throw ex;
            }
        }
        public void UpdatePaxNo(long TransactionItemsID, DateTime TransactionDate, int PaxNo)
        {
            try
            {
                string SQL = "UPDATE tblTransactionItems SET " +
                                "PaxNo           =   @PaxNo " +
                            "WHERE TransactionItemsID		=	@TransactionItemsID; ";

                MySqlConnection cn = GetConnection();

                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = cn;
                cmd.Transaction = mTransaction;
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandText = SQL;

                cmd.Parameters.AddWithValue("@PaxNo", PaxNo);
                cmd.Parameters.AddWithValue("@TransactionItemsID", TransactionItemsID);

                cmd.ExecuteNonQuery();
            }

            catch (Exception ex)
            {
                TransactionFailed = true;
                if (IsInTransaction)
                {
                    mTransaction.Rollback();
                    mConnection.Close();
                    mConnection.Dispose();
                }

                throw ex;
            }
        }

        #endregion

        #region Details

        public SalesTransactionItemDetails[] Details(Int64 TransactionID, DateTime TransactionDate)
        {
            try
            {
                MySqlConnection cn = GetConnection();
                SalesTransactionItems clsItems = new SalesTransactionItems(mConnection, mTransaction);
                MySqlDataReader myReader = clsItems.List(TransactionID, TransactionDate, "TransactionItemsID", SortOption.Ascending);

                ArrayList items = new ArrayList();

                int itemno = 1;
                while (myReader.Read())
                {
                    Data.SalesTransactionItemDetails itemDetails = new Data.SalesTransactionItemDetails();

                    itemDetails.ItemNo = itemno.ToString();
                    itemDetails.TransactionItemsID = myReader.GetInt64("TransactionItemsID");
                    itemDetails.TransactionID = myReader.GetInt64("TransactionID");
                    itemDetails.ProductID = myReader.GetInt64("ProductID");
                    itemDetails.ProductCode = "" + myReader["ProductCode"].ToString();
                    itemDetails.BarCode = "" + myReader["BarCode"].ToString();
                    itemDetails.Description = "" + myReader["Description"].ToString();
                    itemDetails.ProductUnitID = myReader.GetInt32("ProductUnitID");
                    itemDetails.ProductUnitCode = "" + myReader["ProductUnitCode"].ToString();
                    itemDetails.Quantity = myReader.GetDecimal("Quantity");
                    itemDetails.Price = myReader.GetDecimal("Price");
                    itemDetails.Discount = myReader.GetDecimal("Discount");
                    itemDetails.ItemDiscount = myReader.GetDecimal("ItemDiscount");
                    itemDetails.ItemDiscountType = (DiscountTypes)Enum.Parse(typeof(DiscountTypes), myReader.GetString("ItemDiscountType"));
                    itemDetails.Amount = myReader.GetDecimal("Amount");
                    itemDetails.VAT = myReader.GetDecimal("VAT");
                    itemDetails.EVAT = myReader.GetDecimal("EVAT");
                    itemDetails.LocalTax = myReader.GetDecimal("LocalTax");
                    itemDetails.VariationsMatrixID = myReader.GetInt64("VariationsMatrixID");
                    itemDetails.MatrixDescription = "" + myReader["MatrixDescription"].ToString();
                    itemDetails.ProductGroup = "" + myReader["ProductGroup"].ToString();
                    itemDetails.ProductSubGroup = "" + myReader["ProductSubGroup"].ToString();
                    itemDetails.TransactionDate = TransactionDate;
                    itemDetails.TransactionItemStatus = (TransactionItemStatus)Enum.Parse(typeof(TransactionItemStatus), myReader.GetString("TransactionItemStatus"));
                    itemDetails.DiscountCode = "" + myReader["DiscountCode"].ToString();
                    itemDetails.DiscountRemarks = "" + myReader["DiscountRemarks"].ToString();
                    itemDetails.ProductPackageID = myReader.GetInt64("ProductPackageID");
                    itemDetails.MatrixPackageID = myReader.GetInt64("MatrixPackageID");
                    itemDetails.PackageQuantity = myReader.GetDecimal("PackageQuantity");
                    itemDetails.PromoQuantity = myReader.GetDecimal("PromoQuantity");
                    itemDetails.PromoValue = myReader.GetDecimal("PromoValue");
                    itemDetails.PromoInPercent = myReader.GetInt16("PromoInPercent");
                    itemDetails.PromoType = (PromoTypes)Enum.Parse(typeof(PromoTypes), myReader.GetString("PromoType"));
                    itemDetails.PromoApplied = myReader.GetDecimal("PromoApplied");
                    itemDetails.PurchasePrice = myReader.GetDecimal("PurchasePrice");
                    itemDetails.PurchaseAmount = myReader.GetDecimal("PurchaseAmount");
                    itemDetails.IncludeInSubtotalDiscount = myReader.GetBoolean("IncludeInSubtotalDiscount");
                    itemDetails.OrderSlipPrinter = (OrderSlipPrinter)Enum.Parse(typeof(OrderSlipPrinter), myReader.GetString("OrderSlipPrinter"));
                    itemDetails.OrderSlipPrinted = myReader.GetBoolean("OrderSlipPrinted");
                    itemDetails.PercentageCommision = myReader.GetDecimal("PercentageCommision");
                    itemDetails.Commision = myReader.GetDecimal("Commision");
                    itemDetails.PaxNo = myReader.GetInt32("PaxNo");

                    if (itemDetails.TransactionItemStatus == TransactionItemStatus.Return)
                    {
                        itemDetails.Amount = -itemDetails.Amount;
                        itemDetails.Commision = -itemDetails.Commision;
                    }
                    else if (itemDetails.TransactionItemStatus == TransactionItemStatus.Refund)
                    {
                        itemDetails.Amount = -itemDetails.Amount;
                        itemDetails.Commision = -itemDetails.Commision;
                    }
                    else if (itemDetails.TransactionItemStatus == TransactionItemStatus.Void)
                    {
                        itemDetails.Amount = 0;
                        itemDetails.Commision = 0;
                    }
                    //else
                    //    itemDetails.Amount				= itemDetails.Amount;

                    items.Add(itemDetails);
                    itemno++;
                }

                myReader.Close();

                SalesTransactionItemDetails[] TransactionItems = new SalesTransactionItemDetails[0];

                if (items != null)
                {
                    TransactionItems = new SalesTransactionItemDetails[items.Count];
                    items.CopyTo(TransactionItems);
                }

                return TransactionItems;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        #endregion

        #region Streams

        public MySqlDataReader SalesPerItem(string TransactionNo, string CustomerName, string CashierName, string TerminalNo,
            DateTime StartTransactionDate, DateTime EndTransactionDate, TransactionStatus Status, PaymentTypes PaymentType, SaleperItemFilterType pvtSaleperItemFilterType)
        {
            try
            {
                string SQL = "CALL procGenerateSalesPerItem(@SessionID, @TransactionNo, @CustomerName, @CashierName, @TerminalNo, @StartTransactionDate, @EndTransactionDate);";

                MySqlConnection cn = GetConnection();

                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = cn;
                cmd.Transaction = mTransaction;
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandText = SQL;

                Random clsRandom = new Random();
                MySqlParameter prmSessionID = new MySqlParameter("@SessionID",MySqlDbType.String);
                prmSessionID.Value = clsRandom.Next(1234567, 99999999);
                cmd.Parameters.Add(prmSessionID);

                MySqlParameter prmTransactionNo = new MySqlParameter("@TransactionNo",MySqlDbType.String);
                prmTransactionNo.Value = TransactionNo;
                cmd.Parameters.Add(prmTransactionNo);

                MySqlParameter prmCustomerName = new MySqlParameter("@CustomerName",MySqlDbType.String);
                prmCustomerName.Value = CustomerName;
                cmd.Parameters.Add(prmCustomerName);

                MySqlParameter prmCashierName = new MySqlParameter("@CashierName",MySqlDbType.String);
                prmCashierName.Value = CashierName;
                cmd.Parameters.Add(prmCashierName);

                MySqlParameter prmTerminalNo = new MySqlParameter("@TerminalNo",MySqlDbType.String);
                prmTerminalNo.Value = TerminalNo;
                cmd.Parameters.Add(prmTerminalNo);

                MySqlParameter prmStartTransactionDate = new MySqlParameter("@StartTransactionDate",MySqlDbType.DateTime);
                prmStartTransactionDate.Value = StartTransactionDate.ToString("yyyy-MM-dd HH:mm:ss");
                cmd.Parameters.Add(prmStartTransactionDate);

                MySqlParameter prmEndTransactionDate = new MySqlParameter("@EndTransactionDate",MySqlDbType.DateTime);
                prmEndTransactionDate.Value = EndTransactionDate.ToString("yyyy-MM-dd HH:mm:ss");
                cmd.Parameters.Add(prmEndTransactionDate);

                //MySqlParameter prmTransactionStatus = new MySqlParameter("@TransactionStatus",MySqlDbType.Int16);
                //prmTransactionStatus.Value = Status.ToString("d");
                //cmd.Parameters.Add(prmTransactionStatus);

                //MySqlParameter prmPaymentType = new MySqlParameter("@PaymentType",MySqlDbType.Int16);			
                //prmPaymentType.Value = PaymentType.ToString("d");
                //cmd.Parameters.Add(prmPaymentType);

                cmd.ExecuteNonQuery();

                SQL = "SELECT " +
                        "ProductCode," +
                        "SUM(Quantity) 'Quantity'," +
                        "SUM(Amount) 'Amount'," +
                        "SUM(PurchaseAmount) 'PurchaseAmount' " +
                    "FROM tblSalesPerItem " +
                    "WHERE SessionID = @SessionID ";

                switch (pvtSaleperItemFilterType)
                {
                    case SaleperItemFilterType.ShowPositiveOnly:
                        SQL += "AND Amount > PurchaseAmount ";
                        break;
                    case SaleperItemFilterType.ShowNegativeOnly:
                        SQL += "AND Amount < PurchaseAmount ";
                        break;
                }

                SQL += "GROUP BY ProductCode ORDER BY ProductCode;";

                cmd.CommandText = SQL;
                cmd.Parameters.Clear();
                cmd.Parameters.Add(prmSessionID);
                MySqlDataReader myReader = (MySqlDataReader)cmd.ExecuteReader();

                SQL = "DELETE FROM tblSalesPerItem WHERE SessionID = @SessionID;";
                cmd.CommandText = SQL;
                cmd.Parameters.Clear();
                cmd.Parameters.Add(prmSessionID);
                cmd.ExecuteNonQuery();

                return myReader;
            }
            catch (Exception ex)
            {
                TransactionFailed = true;
                if (IsInTransaction)
                {
                    mTransaction.Rollback();
                    mConnection.Close();
                    mConnection.Dispose();
                }
                throw ex;
            }
        }
        public MySqlDataReader SalesPerItemByGroup(string ProductGroupName, string TransactionNo, string CustomerName, string CashierName, string TerminalNo,
            DateTime StartTransactionDate, DateTime EndTransactionDate, TransactionStatus Status, PaymentTypes PaymentType, SaleperItemFilterType pvtSaleperItemFilterType)
        {
            try
            {
                string SQL = "CALL procGenerateSalesPerItemByGroup(@SessionID, @ProductGroupName, @TransactionNo, @CustomerName, @CashierName, @TerminalNo, @StartTransactionDate, @EndTransactionDate);";

                MySqlConnection cn = GetConnection();

                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = cn;
                cmd.Transaction = mTransaction;
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandText = SQL;

                Random clsRandom = new Random();
                MySqlParameter prmSessionID = new MySqlParameter("@SessionID", clsRandom.Next(1234567, 99999999));

                cmd.Parameters.Add(prmSessionID);
                cmd.Parameters.AddWithValue("@ProductGroupName", ProductGroupName);
                cmd.Parameters.AddWithValue("@TransactionNo", TransactionNo);
                cmd.Parameters.AddWithValue("@CustomerName", CustomerName);
                cmd.Parameters.AddWithValue("@CashierName", CashierName);
                cmd.Parameters.AddWithValue("@TerminalNo", TerminalNo);
                cmd.Parameters.AddWithValue("@StartTransactionDate", StartTransactionDate.ToString("yyyy-MM-dd HH:mm:ss"));
                cmd.Parameters.AddWithValue("@EndTransactionDate", EndTransactionDate.ToString("yyyy-MM-dd HH:mm:ss"));

                cmd.ExecuteNonQuery();

                SQL = "SELECT " +
                        "ProductCode," +
                        "SUM(Quantity) 'Quantity'," +
                        "SUM(Amount) 'Amount'," +
                        "SUM(PurchaseAmount) 'PurchaseAmount' " +
                    "FROM tblSalesPerItem " +
                    "WHERE SessionID = @SessionID ";

                switch (pvtSaleperItemFilterType)
                {
                    case SaleperItemFilterType.ShowPositiveOnly:
                        SQL += "AND Amount > PurchaseAmount ";
                        break;
                    case SaleperItemFilterType.ShowNegativeOnly:
                        SQL += "AND Amount < PurchaseAmount ";
                        break;
                }

                SQL += "GROUP BY ProductCode ORDER BY ProductCode;";

                cmd.CommandText = SQL;
                cmd.Parameters.Clear();
                cmd.Parameters.Add(prmSessionID);
                MySqlDataReader myReader = (MySqlDataReader)cmd.ExecuteReader();

                SQL = "DELETE FROM tblSalesPerItem WHERE SessionID = @SessionID;";

                cmd.CommandText = SQL;
                cmd.Parameters.Clear();
                cmd.Parameters.Add(prmSessionID);
                cmd.ExecuteNonQuery();

                return myReader;
            }
            catch (Exception ex)
            {
                TransactionFailed = true;
                if (IsInTransaction)
                {
                    mTransaction.Rollback();
                    mConnection.Close();
                    mConnection.Dispose();
                }
                throw ex;
            }
        }
        public MySqlDataReader List(Int64 TransactionID, DateTime TransactionDate, string SortField, SortOption SortOrder)
        {
            try
            {
                string SQL = "SELECT " +
                                "TransactionItemsID, " +
                                "TransactionID, " +
                                "ProductID, " +
                                "ProductCode, " +
                                "BarCode, " +
                                "Description, " +
                                "ProductUnitID, " +
                                "ProductUnitCode, " +
                                "ProductUnitCode AS ProductUnitName, " +
                                "Quantity, " +
                                "Price, " +
                                "Discount, " +
                                "ItemDiscount, " +
                                "ItemDiscountType, " +
                                "Amount, " +
                                "VAT, " +
                                "EVAT, " +
                                "LocalTax, " +
                                "VariationsMatrixID, " +
                                "MatrixDescription, " +
                                "ProductGroup, " +
                                "ProductSubGroup, " +
                                "@TransactionDate AS TransactionDate, " +
                                "TransactionItemStatus, " +
                                "DiscountCode, " +
                                "DiscountRemarks, " +
                                "ProductPackageID, " +
                                "MatrixPackageID, " +
                                "PackageQuantity, " +
                                "PromoQuantity, " +
                                "PromoValue, " +
                                "PromoInPercent, " +
                                "PromoType, " +
                                "PromoApplied, " +
                                "PurchasePrice, " +
                                "PurchaseAmount, " +
                                "IncludeInSubtotalDiscount, " +
                                "OrderSlipPrinter, " +
                                "OrderSlipPrinted, " +
                                "PercentageCommision, " +
                                "Commision, " +
                                "PaxNo " +
                            "FROM tblTransactionItems " +
                            "WHERE TransactionID = @TransactionID AND TransactionItemStatus <> @TransactionItemStatus ORDER BY " + SortField;

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

                MySqlParameter prmTransactionID = new MySqlParameter("@TransactionID",MySqlDbType.Int64);
                prmTransactionID.Value = TransactionID;
                cmd.Parameters.Add(prmTransactionID);

                MySqlParameter prmTransactionDate = new MySqlParameter("@TransactionDate",MySqlDbType.DateTime);
                prmTransactionDate.Value = TransactionDate.ToString("yyyy-MM-dd HH:mm:ss");
                cmd.Parameters.Add(prmTransactionDate);

                MySqlParameter prmTransactionItemStatus = new MySqlParameter("@TransactionItemStatus",MySqlDbType.Int16);
                prmTransactionItemStatus.Value = TransactionItemStatus.Trash.ToString("d");
                cmd.Parameters.Add(prmTransactionItemStatus);

                MySqlDataReader myReader = (MySqlDataReader)cmd.ExecuteReader();

                return myReader;
            }
            catch (Exception ex)
            {
                TransactionFailed = true;
                if (IsInTransaction)
                {
                    mTransaction.Rollback();
                    mConnection.Close();
                    mConnection.Dispose();
                }
                throw ex;
            }
        }

        public System.Data.DataTable MostSalableItems(DateTime StartTransactionDate, DateTime EndTransactionDate, Int32 Limit)
        {
            try
            {
                string SQL = "CALL procGenerateSalesPerItem(@SessionID, @TransactionNo, @CustomerName, @CashierName, @TerminalNo, @StartTransactionDate, @EndTransactionDate);";

                MySqlConnection cn = GetConnection();

                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = cn;
                cmd.Transaction = mTransaction;
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandText = SQL;

                Random clsRandom = new Random();
                MySqlParameter prmSessionID = new MySqlParameter("@SessionID",MySqlDbType.String);
                prmSessionID.Value = clsRandom.Next(1234567, 99999999);
                cmd.Parameters.Add(prmSessionID);
                cmd.Parameters.AddWithValue("@TransactionNo", string.Empty);
                cmd.Parameters.AddWithValue("@CustomerName", string.Empty);
                cmd.Parameters.AddWithValue("@CashierName", string.Empty);
                cmd.Parameters.AddWithValue("@TerminalNo", string.Empty);
                cmd.Parameters.AddWithValue("@StartTransactionDate", StartTransactionDate.ToString("yyyy-MM-dd HH:mm:ss"));
                cmd.Parameters.AddWithValue("@EndTransactionDate", EndTransactionDate.ToString("yyyy-MM-dd HH:mm:ss"));

                cmd.ExecuteNonQuery();

                SQL = "SELECT " +
                        "ProductGroup, " +
                        "ProductCode," +
                        "ProductUnitCode," +
                        "SUM(Quantity) 'Count' " +
                    "FROM tblSalesPerItem " +
                    "WHERE SessionID = @SessionID " +
                    "GROUP BY ProductGroup, ProductCode " +
                    "ORDER BY Quantity DESC ";
                if (Limit != 0) SQL += "LIMIT " + Limit.ToString() + ";";

                cmd.CommandText = SQL;
                cmd.Parameters.Clear();
                cmd.Parameters.Add(prmSessionID);
                cmd.ExecuteNonQuery();

                System.Data.DataTable dt = new System.Data.DataTable("MostSalableItems");
                MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
                adapter.Fill(dt);

                SQL = "DELETE FROM tblSalesPerItem WHERE SessionID = @SessionID;";
                cmd.CommandText = SQL;
                cmd.Parameters.Clear();
                cmd.Parameters.Add(prmSessionID);
                cmd.ExecuteNonQuery();

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

        public System.Data.DataTable LeastSalableItems(DateTime StartTransactionDate, DateTime EndTransactionDate, Int32 Limit)
        {
            try
            {
                string SQL = "CALL procGenerateSalesPerItemWithZeroSales(@SessionID, @TransactionNo, @CustomerName, @CashierName, @TerminalNo, @StartTransactionDate, @EndTransactionDate);";

                MySqlConnection cn = GetConnection();

                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = cn;
                cmd.Transaction = mTransaction;
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandText = SQL;

                Random clsRandom = new Random();
                MySqlParameter prmSessionID = new MySqlParameter("@SessionID",MySqlDbType.String);
                prmSessionID.Value = clsRandom.Next(1234567, 99999999);
                cmd.Parameters.Add(prmSessionID);
                cmd.Parameters.AddWithValue("@TransactionNo", string.Empty);
                cmd.Parameters.AddWithValue("@CustomerName", string.Empty);
                cmd.Parameters.AddWithValue("@CashierName", string.Empty);
                cmd.Parameters.AddWithValue("@TerminalNo", string.Empty);
                cmd.Parameters.AddWithValue("@StartTransactionDate", StartTransactionDate.ToString("yyyy-MM-dd HH:mm:ss"));
                cmd.Parameters.AddWithValue("@EndTransactionDate", EndTransactionDate.ToString("yyyy-MM-dd HH:mm:ss"));

                cmd.ExecuteNonQuery();

                SQL = "SELECT " +
                        "ProductGroup, " +
                        "ProductCode," +
                        "ProductUnitCode," +
                        "SUM(Quantity) 'Count' " +
                    "FROM tblSalesPerItem " +
                    "WHERE SessionID = @SessionID " +
                    "GROUP BY ProductGroup, ProductCode " +
                    "ORDER BY Quantity ASC ";
                if (Limit != 0) SQL += "LIMIT " + Limit.ToString() + ";";

                cmd.CommandText = SQL;
                cmd.Parameters.Clear();
                cmd.Parameters.Add(prmSessionID);
                cmd.ExecuteNonQuery();

                System.Data.DataTable dt = new System.Data.DataTable("LeastSalableItems");
                MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
                adapter.Fill(dt);

                SQL = "DELETE FROM tblSalesPerItem WHERE SessionID = @SessionID;";
                cmd.CommandText = SQL;
                cmd.Parameters.Clear();
                cmd.Parameters.Add(prmSessionID);
                cmd.ExecuteNonQuery();

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

        public MySqlDataReader ProductHistoryReport(long ProductID, DateTime StartDate, DateTime EndDate)
        {
            try
            {
                MySqlCommand cmd = new MySqlCommand();

                string SQL = "SELECT " +
                                "TransactionItemsID, " +
                                "IFNULL(NULLIF(MatrixDescription,''), ProductCode) AS MatrixDescription, " +
                                "CASE TransactionItemStatus " +
                                "	WHEN 0 THEN -Quantity " +
                                "	WHEN 1 THEN 0 " +
                                "	WHEN 2 THEN 0 " +
                                "	WHEN 3 THEN Quantity " +
                                "	WHEN 4 THEN -Quantity " +
                                "	WHEN 5 THEN 0 " +
                                "END AS Quantity, " +
                                "ProductUnitCode as UnitCode, " +
                                "CASE TransactionItemStatus " +
                                "	WHEN 0 THEN 'Sold' " +
                                "	WHEN 1 THEN 'Void' " +
                                "	WHEN 2 THEN 'Trash' " +
                                "	WHEN 3 THEN 'Return' " +
                                "	WHEN 4 THEN 'Refund' " +
                                "	WHEN 5 THEN 'OrderSlip' " +
                                "END AS Remarks, " +
                                "TransactionDate, " +
                                "TransactionNo " +
                            "FROM tblTransaction a " +
                            "INNER JOIN tblTransactionItems b ON a.TransactionID = b.TransactionID " +
                            "WHERE 1=1 ";

                SQL += "AND TransactionDate >= @StartDate ";

                MySqlParameter prmStartDate = new MySqlParameter("@StartDate",MySqlDbType.DateTime);
                prmStartDate.Value = StartDate.ToString("yyyy-MM-dd HH:mm:ss");
                cmd.Parameters.Add(prmStartDate);

                SQL += "AND TransactionDate <= @EndDate ";

                MySqlParameter prmEndDate = new MySqlParameter("@EndDate",MySqlDbType.DateTime);
                prmEndDate.Value = EndDate.ToString("yyyy-MM-dd HH:mm:ss");
                cmd.Parameters.Add(prmEndDate);

                if (ProductID != 0)
                {
                    SQL += "AND a.ProductID = @ProductID ";

                    MySqlParameter prmProductID = new MySqlParameter("@ProductID",MySqlDbType.Int64);
                    prmProductID.Value = ProductID;
                    cmd.Parameters.Add(prmProductID);
                }

                SQL += "ORDER BY TransactionDate DESC ";

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
                    mConnection.Close();
                    mConnection.Dispose();
                }
                throw ex;
            }
        }

        public System.Data.DataTable AgentsCommision(string byvalDepartment, string byvalPosition, DateTime StartTransactionDate, DateTime EndTransactionDate)
        {
            try
            {
                string SQL = "CALL procGenerateAllAgentsCommision(@SessionID, @StartTransactionDate, @EndTransactionDate);";

                MySqlConnection cn = GetConnection();

                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = cn;
                cmd.Transaction = mTransaction;
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandText = SQL;

                Random clsRandom = new Random();
                MySqlParameter prmSessionID = new MySqlParameter("@SessionID",MySqlDbType.String);
                prmSessionID.Value = clsRandom.Next(1234567, 99999999);
                cmd.Parameters.Add(prmSessionID);
                cmd.Parameters.AddWithValue("@StartTransactionDate", StartTransactionDate.ToString("yyyy-MM-dd HH:mm:ss"));
                cmd.Parameters.AddWithValue("@EndTransactionDate", EndTransactionDate.ToString("yyyy-MM-dd HH:mm:ss"));

                cmd.ExecuteNonQuery();

                SQL = "SELECT " +
                        "TransactionNo, " +
                        "TransactionDate," +
                        "Description," +
                        "Quantity," +
                        "Amount," +
                        "PercentageCommision," +
                        "Commision, " +
                        "AgentID, " +
                        "AgentName, " +
                        "DepartmentName, " +
                        "PositionName " +
                    "FROM tblAgentsCommision " +
                    "WHERE SessionID = @SessionID ";

                //if (byvalDepartment != null || byvalDepartment != string.Empty || byvalDepartment.ToUpper() != Department.DEFAULT_ALL_DEPARTMENTS.ToUpper())
                //{ SQL += "AND Department = '" + byvalDepartment + "' "; }
                //if (byvalPosition != null || byvalPosition != string.Empty || byvalPosition.ToUpper() != Position.DEFAULT_ALL_POSITIONS.ToUpper())
                //{ SQL += "AND Position = '" + byvalPosition + "' "; }

                cmd.CommandText = SQL;
                cmd.Parameters.Clear();
                cmd.Parameters.Add(prmSessionID);
                cmd.ExecuteNonQuery();

                System.Data.DataTable dt = new System.Data.DataTable("AgentsCommision");
                MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
                adapter.Fill(dt);

                SQL = "DELETE FROM tblAgentsCommision WHERE SessionID = @SessionID;";
                cmd.CommandText = SQL;
                cmd.Parameters.Clear();
                cmd.Parameters.Add(prmSessionID);
                cmd.ExecuteNonQuery();

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

        public System.Data.DataTable AgentsCommision(long AgentID, DateTime StartTransactionDate, DateTime EndTransactionDate)
        {
            try
            {
                string SQL = "CALL procGenerateAgentsCommision(@SessionID, @AgentID, @StartTransactionDate, @EndTransactionDate);";

                MySqlConnection cn = GetConnection();

                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = cn;
                cmd.Transaction = mTransaction;
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandText = SQL;

                Random clsRandom = new Random();
                MySqlParameter prmSessionID = new MySqlParameter("@SessionID",MySqlDbType.String);
                prmSessionID.Value = clsRandom.Next(1234567, 99999999);
                cmd.Parameters.Add(prmSessionID);
                cmd.Parameters.AddWithValue("@AgentID", AgentID);
                cmd.Parameters.AddWithValue("@StartTransactionDate", StartTransactionDate.ToString("yyyy-MM-dd HH:mm:ss"));
                cmd.Parameters.AddWithValue("@EndTransactionDate", EndTransactionDate.ToString("yyyy-MM-dd HH:mm:ss"));

                cmd.ExecuteNonQuery();

                SQL = "SELECT " +
                        "TransactionNo, " +
                        "TransactionDate," +
                        "Description," +
                        "Quantity," +
                        "Amount," +
                        "PercentageCommision," +
                        "Commision, " +
                        "DepartmentName, " +
                        "PositionName " +
                    "FROM tblAgentsCommision " +
                    "WHERE SessionID = @SessionID " +
                    "ORDER BY TransactionNo ASC;";

                cmd.CommandText = SQL;
                cmd.Parameters.Clear();
                cmd.Parameters.Add(prmSessionID);
                cmd.ExecuteNonQuery();

                System.Data.DataTable dt = new System.Data.DataTable("AgentsCommision");
                MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
                adapter.Fill(dt);

                SQL = "DELETE FROM tblAgentsCommision WHERE SessionID = @SessionID;";
                cmd.CommandText = SQL;
                cmd.Parameters.Clear();
                cmd.Parameters.Add(prmSessionID);
                cmd.ExecuteNonQuery();

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

        #endregion

        #region Public Modifiers

        public void Return(Int64 TransactionItemsID)
        {
            try
            {
                string SQL = "UPDATE tblTransactionItems SET " +
                                "Quantity						=	Quantity * -1, " +
                                "Price							=	Price * -1, " +
                                "Discount						=	Discount * -1, " +
                                "Amount							=	Amount * -1, " +
                                "Commision						=	Commision * -1, " +
                                "VAT							=	VAT * -1, " +
                                "EVAT							=	EVAT * -1, " +
                                "LocalTax						=	LocalTax * -1, " +
                                "TransactionItemStatus			=	@TransactionItemStatus " +
                            "WHERE TransactionItemsID		=	@TransactionItemsID;";

                MySqlConnection cn = GetConnection();

                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = cn;
                cmd.Transaction = mTransaction;
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandText = SQL;

                MySqlParameter prmTransactionItemStatus = new MySqlParameter("@TransactionItemStatus",MySqlDbType.Int16);
                prmTransactionItemStatus.Value = TransactionItemStatus.Return.ToString("d");
                cmd.Parameters.Add(prmTransactionItemStatus);

                MySqlParameter prmTransactionItemsID = new MySqlParameter("@TransactionItemsID",MySqlDbType.Int64);
                prmTransactionItemsID.Value = TransactionItemsID;
                cmd.Parameters.Add(prmTransactionItemsID);

                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                TransactionFailed = true;
                if (IsInTransaction)
                {
                    mTransaction.Rollback();
                    mConnection.Close();
                    mConnection.Dispose();
                }

                throw ex;
            }
        }
        public void Void(Int64 TransactionItemsID)
        {
            try
            {
                string SQL = "UPDATE tblTransactionItems SET " +
                                "Quantity						=	Quantity * -1, " +
                                "Price							=	Price * -1, " +
                                "Discount						=	Discount * -1, " +
                                "Amount							=	Amount * -1, " +
                                "Commision						=	0, " +
                                "VAT							=	VAT * -1, " +
                                "EVAT							=	EVAT * -1, " +
                                "LocalTax						=	LocalTax * -1, " +
                                "TransactionItemStatus			=	@TransactionItemStatus " +
                             "WHERE TransactionItemsID		=	@TransactionItemsID;";

                MySqlConnection cn = GetConnection();

                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = cn;
                cmd.Transaction = mTransaction;
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandText = SQL;

                MySqlParameter prmTransactionItemStatus = new MySqlParameter("@TransactionItemStatus",MySqlDbType.Int16);
                prmTransactionItemStatus.Value = TransactionItemStatus.Void.ToString("d");
                cmd.Parameters.Add(prmTransactionItemStatus);

                MySqlParameter prmTransactionItemsID = new MySqlParameter("@TransactionItemsID",MySqlDbType.Int64);
                prmTransactionItemsID.Value = TransactionItemsID;
                cmd.Parameters.Add(prmTransactionItemsID);

                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                TransactionFailed = true;
                if (IsInTransaction)
                {
                    mTransaction.Rollback();
                    mConnection.Close();
                    mConnection.Dispose();
                }

                throw ex;
            }
        }
        public void VoidByTransaction(Int64 TransactionID)
        {
            try
            {
                string SQL = "UPDATE tblTransactionItems SET " +
                                "Quantity						=	Quantity * -1, " +
                                "Price							=	Price * -1, " +
                                "Discount						=	Discount * -1, " +
                                "Amount							=	Amount * -1, " +
                                "Commision						=	0, " +
                                "VAT							=	VAT * -1, " +
                                "EVAT							=	EVAT * -1, " +
                                "LocalTax						=	LocalTax * -1, " +
                                "TransactionItemStatus			=	@TransactionItemStatus " +
                            "WHERE TransactionID			=	@TransactionID;";

                MySqlConnection cn = GetConnection();

                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = cn;
                cmd.Transaction = mTransaction;
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandText = SQL;

                MySqlParameter prmTransactionItemStatus = new MySqlParameter("@TransactionItemStatus",MySqlDbType.Int16);
                prmTransactionItemStatus.Value = TransactionItemStatus.Void.ToString("d");
                cmd.Parameters.Add(prmTransactionItemStatus);

                MySqlParameter prmTransactionID = new MySqlParameter("@TransactionID",MySqlDbType.Int64);
                prmTransactionID.Value = TransactionID;
                cmd.Parameters.Add(prmTransactionID);

                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                TransactionFailed = true;
                if (IsInTransaction)
                {
                    mTransaction.Rollback();
                    mConnection.Close();
                    mConnection.Dispose();
                }

                throw ex;
            }
        }
        public void RefundByTransaction(Int64 TransactionID)
        {
            try
            {
                string SQL = "UPDATE tblTransactionItems SET " +
                                "Quantity						=	Quantity * -1, " +
                                "Price							=	Price * -1, " +
                                "Discount						=	Discount * -1, " +
                                "Amount							=	Amount * -1, " +
                                "Commision						=	Commision * -1, " +
                                "VAT							=	VAT * -1, " +
                                "EVAT							=	EVAT * -1, " +
                                "LocalTax						=	LocalTax * -1, " +
                                "TransactionItemStatus			=	@TransactionItemStatus " +
                            "WHERE TransactionID			=	@TransactionID;";

                MySqlConnection cn = GetConnection();

                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = cn;
                cmd.Transaction = mTransaction;
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandText = SQL;

                MySqlParameter prmTransactionItemStatus = new MySqlParameter("@TransactionItemStatus",MySqlDbType.Int16);
                prmTransactionItemStatus.Value = TransactionItemStatus.Refund.ToString("d");
                cmd.Parameters.Add(prmTransactionItemStatus);

                MySqlParameter prmTransactionID = new MySqlParameter("@TransactionID",MySqlDbType.Int64);
                prmTransactionID.Value = TransactionID;
                cmd.Parameters.Add(prmTransactionID);

                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                TransactionFailed = true;
                if (IsInTransaction)
                {
                    mTransaction.Rollback();
                    mConnection.Close();
                    mConnection.Dispose();
                }

                throw ex;
            }
        }
        public void Trash(Int64 TransactionItemsID)
        {
            try
            {
                string SQL = "DELETE FROM tblTransactionItems " +
                            "WHERE TransactionItemsID		=	@TransactionItemsID;";

                MySqlConnection cn = GetConnection();

                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = cn;
                cmd.Transaction = mTransaction;
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandText = SQL;

                MySqlParameter prmTransactionItemsID = new MySqlParameter("@TransactionItemsID",MySqlDbType.Int64);
                prmTransactionItemsID.Value = TransactionItemsID;
                cmd.Parameters.Add(prmTransactionItemsID);

                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                TransactionFailed = true;
                if (IsInTransaction)
                {
                    mTransaction.Rollback();
                    mConnection.Close();
                    mConnection.Dispose();
                }

                throw ex;
            }
        }
        public void CloseAsOrderSlip(Int64 TransactionID)
        {
            try
            {
                string SQL = "UPDATE tblTransactionItems SET " +
                                "TransactionItemStatus			=	@TransactionItemStatus " +
                            "WHERE TransactionID			=	@TransactionID;";

                MySqlConnection cn = GetConnection();

                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = cn;
                cmd.Transaction = mTransaction;
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandText = SQL;

                MySqlParameter prmTransactionItemStatus = new MySqlParameter("@TransactionItemStatus",MySqlDbType.Int16);
                prmTransactionItemStatus.Value = TransactionItemStatus.OrderSlip.ToString("d");
                cmd.Parameters.Add(prmTransactionItemStatus);

                MySqlParameter prmTransactionID = new MySqlParameter("@TransactionID",MySqlDbType.Int64);
                prmTransactionID.Value = TransactionID;
                cmd.Parameters.Add(prmTransactionID);

                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                TransactionFailed = true;
                if (IsInTransaction)
                {
                    mTransaction.Rollback();
                    mConnection.Close();
                    mConnection.Dispose();
                }

                throw ex;
            }
        }

        #endregion
    }
}
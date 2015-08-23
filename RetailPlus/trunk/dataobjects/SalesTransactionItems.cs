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

        public bool PromoInPercent; //32

        public PromoTypes PromoType; //34
        public bool IncludeInSubtotalDiscount;
        public bool IsCreditChargeExcluded;
        public bool OrderSlipPrinter1;
        public bool OrderSlipPrinter2;
        public bool OrderSlipPrinter3;
        public bool OrderSlipPrinter4;
        public bool OrderSlipPrinter5;
        public bool OrderSlipPrinted;
        public decimal PercentageCommision;
        public decimal Commision;
        public decimal RewardPoints;

        public decimal ScannedQty;
        public decimal ScannedAmt;

        public int PaxNo;
        public decimal GrossSales;
        public decimal VatableAmount;
        public decimal NonVatableAmount; 
        public decimal VATExempt;

        public string ItemRemarks;

        // 28Jun2015 : Added to handle the return item
        public Int64 ReturnTransactionItemsID;
        public Int64 RefReturnTransactionItemsID;

        // 05Jul2015 : Added to handle supplier details
        public Int64 SupplierID;
        public string SupplierCode;
        public string SupplierName;

        public Int64 ProductGroupID; //21
        public Int64 ProductSubGroupID; //21
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
        public bool IsCreditChargeExcluded;
        public bool OrderSlipPrinted;
        public bool PercentageCommision;
        public bool Commision;
        public bool RewardPoints;
        
        public bool ScannedQty;
        public bool ScannedAmt;

        public bool PaxNo;

        public bool SupplierID;
        public bool SupplierCode;
        public bool SupplierName;
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
        public const string GrossSales = "GrossSales";
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
        public const string IsCreditChargeExcluded = "IsCreditChargeExcluded";
        public const string OrderSlipPrinted = "OrderSlipPrinted";
        public const string PercentageCommision = "PercentageCommision";
        public const string Commision = "Commision";
        public const string RewardPoints = "RewardPoints";

        public const string ScannedQty = "ScannedQty";
        public const string ScannedAmt = "ScannedAmt";
        
        public const string PaxNo = "PaxNo";

        public const string SupplierID = "SupplierID";
        public const string SupplierCode = "SupplierCode";
        public const string SupplierName = "SupplierName";
    }

    [StrongNameIdentityPermissionAttribute(SecurityAction.LinkDemand,
         PublicKey = "002400000480000094000000060200000024000" +
         "052534131000400000100010053D785642F9F960B43157E0380" +
         "F393BEE53E8DFAFBF441366C1B6F8B48D9DDF0D527B1F3B21EA" +
         "E85D2FDB664CE81EB8A87DBE4C589D6F4202FE2B7D4B978BB69" +
         "684874612CB9B8DB7A0339400A9C4E68277884B07817363D242" +
         "E3696F9FACDBEA831810AE6DC9EDCA91A7B5DA12FE7BF65D113" +
         "FF52834EAFB5A7A1FDFD5851A3")]
    public class SalesTransactionItems : POSConnection
    {

        #region Constructors and Destructors

        public SalesTransactionItems()
            : base(null, null)
        {
        }

        public SalesTransactionItems(MySqlConnection Connection, MySqlTransaction Transaction) 
            : base(Connection, Transaction)
		{

		}

        #endregion

        #region Insert and Update

        public Int64 Insert(SalesTransactionItemDetails Details)
        {
            try
            {
                string SQL = "INSERT INTO tblTransactionItems (" +
                                "ReturnTransactionItemsID, " +
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
                                "GrossSales, " +
                                "Amount, " +
                                "VAT, " +
                                "VatableAmount, " +
                                "NonVatableAmount, " +
                                "VATExempt, " +
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
                                "IsCreditChargeExcluded," +
                                "OrderSlipPrinter1," +
                                "OrderSlipPrinter2," +
                                "OrderSlipPrinter3," +
                                "OrderSlipPrinter4," +
                                "OrderSlipPrinter5," +
                                "OrderSlipPrinted," +
                                "PercentageCommision," +
                                "Commision, RewardPoints, ItemRemarks, PaxNo, " +
                                "SupplierID, SupplierCode, SupplierName, CreatedOn" +
                            ")VALUES(" +
                                "@ReturnTransactionItemsID, " +
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
                                "@GrossSales, " +
                                "@Amount, " +
                                "@VAT, " +
                                "@VatableAmount, " +
                                "@NonVatableAmount, " +
                                "@VATExempt, " +
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
                                "@IsCreditChargeExcluded," +
                                "@OrderSlipPrinter1," +
                                "@OrderSlipPrinter2," +
                                "@OrderSlipPrinter3," +
                                "@OrderSlipPrinter4," +
                                "@OrderSlipPrinter5," +
                                "@OrderSlipPrinted," +
                                "@PercentageCommision," +
                                "@Commision, @RewardPoints, @ItemRemarks, @PaxNo, " +
                                "@SupplierID, @SupplierCode, @SupplierName, NOW());";

                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandText = SQL;

                cmd.Parameters.AddWithValue("@ReturnTransactionItemsID", Details.ReturnTransactionItemsID);
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
                cmd.Parameters.AddWithValue("@GrossSales", Details.GrossSales);
                cmd.Parameters.AddWithValue("@Amount", Details.Amount);
                cmd.Parameters.AddWithValue("@VAT", Details.VAT);
                cmd.Parameters.AddWithValue("@VatableAmount", Details.VatableAmount); //GrossSales/(1+(@VAT/100))
                cmd.Parameters.AddWithValue("@NonVatableAmount", Details.NonVatableAmount);
                cmd.Parameters.AddWithValue("@VATExempt", Details.VATExempt);
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
                cmd.Parameters.AddWithValue("@IsCreditChargeExcluded", Details.IsCreditChargeExcluded);
                cmd.Parameters.AddWithValue("@OrderSlipPrinter1", Details.OrderSlipPrinter1);
                cmd.Parameters.AddWithValue("@OrderSlipPrinter2", Details.OrderSlipPrinter2);
                cmd.Parameters.AddWithValue("@OrderSlipPrinter3", Details.OrderSlipPrinter3);
                cmd.Parameters.AddWithValue("@OrderSlipPrinter4", Details.OrderSlipPrinter4);
                cmd.Parameters.AddWithValue("@OrderSlipPrinter5", Details.OrderSlipPrinter5);
                cmd.Parameters.AddWithValue("@OrderSlipPrinted", Convert.ToInt16(Details.OrderSlipPrinted));
                cmd.Parameters.AddWithValue("@PercentageCommision", Details.PercentageCommision);
                cmd.Parameters.AddWithValue("@Commision", Details.Commision);
                cmd.Parameters.AddWithValue("@RewardPoints", Details.RewardPoints);
                cmd.Parameters.AddWithValue("@ItemRemarks", Details.ItemRemarks);
                cmd.Parameters.AddWithValue("@PaxNo", Details.PaxNo);
                cmd.Parameters.AddWithValue("@SupplierID", Details.SupplierID);
                cmd.Parameters.AddWithValue("@SupplierCode", Details.SupplierCode);
                cmd.Parameters.AddWithValue("@SupplierName", Details.SupplierName);

                base.ExecuteNonQuery(cmd);

                SQL = "SELECT LAST_INSERT_ID();";

                cmd.Parameters.Clear();
                cmd.CommandText = SQL;

                Int64 iID = Int64.Parse(base.getLAST_INSERT_ID(this));

                return iID;
            }
            catch (Exception ex)
            {
                throw base.ThrowException(ex);
            }
        }
        public void Update(SalesTransactionItemDetails Details)
        {
            try
            {
                string SQL = "UPDATE tblTransactionItems SET " +
                                "ReturnTransactionItemsID   =   @ReturnTransactionItemsID, " +
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
                                "GrossSales					=	@GrossSales, " +
                                "Amount						=	@Amount, " +
                                "VAT						=	@VAT, " +
                                "VatableAmount				=	@VatableAmount, " + //GrossSales/(1+(@VAT/100))
                                "NonVatableAmount			=	@NonVatableAmount, " +
                                "VATExempt			        =	@VATExempt, " +
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
                                "IsCreditChargeExcluded	    =	@IsCreditChargeExcluded, " +
                                "OrderSlipPrinter1          =   @OrderSlipPrinter1, " +
                                "OrderSlipPrinter2          =   @OrderSlipPrinter2, " +
                                "OrderSlipPrinter3          =   @OrderSlipPrinter3, " +
                                "OrderSlipPrinter4          =   @OrderSlipPrinter4, " +
                                "OrderSlipPrinter5          =   @OrderSlipPrinter5, " +
                                "PercentageCommision        =   @PercentageCommision, " +
                                "Commision                  =   @Commision, " +
                                "RewardPoints               =   @RewardPoints, " +
                                "ItemRemarks                =   @ItemRemarks, " +
                                "LastModified		        =	NOW() " +
                            "WHERE TransactionItemsID		=	@TransactionItemsID; ";

                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandText = SQL;

                cmd.Parameters.AddWithValue("@ReturnTransactionItemsID", Details.ReturnTransactionItemsID);
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
                cmd.Parameters.AddWithValue("@GrossSales", Details.GrossSales);
                cmd.Parameters.AddWithValue("@Amount", Details.Amount);
                cmd.Parameters.AddWithValue("@VAT", Details.VAT);
                cmd.Parameters.AddWithValue("@VatableAmount", Details.VatableAmount); // GrossSales/(1+(@VAT/100))
                cmd.Parameters.AddWithValue("@NonVatableAmount", Details.NonVatableAmount);
                cmd.Parameters.AddWithValue("@VATExempt", Details.VATExempt);
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
                cmd.Parameters.AddWithValue("@IsCreditChargeExcluded", Details.IsCreditChargeExcluded);
                cmd.Parameters.AddWithValue("@OrderSlipPrinter1", Details.OrderSlipPrinter1);
                cmd.Parameters.AddWithValue("@OrderSlipPrinter2", Details.OrderSlipPrinter2);
                cmd.Parameters.AddWithValue("@OrderSlipPrinter3", Details.OrderSlipPrinter3);
                cmd.Parameters.AddWithValue("@OrderSlipPrinter4", Details.OrderSlipPrinter4);
                cmd.Parameters.AddWithValue("@OrderSlipPrinter5", Details.OrderSlipPrinter5);
                cmd.Parameters.AddWithValue("@OrderSlipPrinted", Convert.ToInt16(Details.OrderSlipPrinted));
                cmd.Parameters.AddWithValue("@PercentageCommision", Details.PercentageCommision);
                cmd.Parameters.AddWithValue("@Commision", Details.Commision);
                cmd.Parameters.AddWithValue("@RewardPoints", Details.RewardPoints);
                cmd.Parameters.AddWithValue("@ItemRemarks", Details.ItemRemarks);
                cmd.Parameters.AddWithValue("@TransactionItemsID", Details.TransactionItemsID);

                base.ExecuteNonQuery(cmd);
            }
            catch (Exception ex)
            {
                throw base.ThrowException(ex);
            }
        }

        public void UpdateOrderSlipPrinted(bool IsOrderSlipPrinted, long TransactionItemsID)
        {
            try
            {
                string SQL = "UPDATE tblTransactionItems SET " +
                                "OrderSlipPrinted           =   @OrderSlipPrinted, " +
                                "LastModified		        =	NOW() " +
                            "WHERE TransactionItemsID		=	@TransactionItemsID; ";

                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandText = SQL;

                cmd.Parameters.AddWithValue("@OrderSlipPrinted", Convert.ToInt16(IsOrderSlipPrinted));
                cmd.Parameters.AddWithValue("@TransactionItemsID", TransactionItemsID);

                base.ExecuteNonQuery(cmd);
            }
            catch (Exception ex)
            {
                throw base.ThrowException(ex);
            }
        }
        public void UpdatePaxNo(long TransactionItemsID, DateTime TransactionDate, int PaxNo)
        {
            try
            {
                string SQL = "UPDATE tblTransactionItems SET " +
                                "PaxNo           =   @PaxNo " +
                            "WHERE TransactionItemsID		=	@TransactionItemsID; ";

                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandText = SQL;

                cmd.Parameters.AddWithValue("@PaxNo", PaxNo);
                cmd.Parameters.AddWithValue("@TransactionItemsID", TransactionItemsID);

                base.ExecuteNonQuery(cmd);
            }

            catch (Exception ex)
            {
                throw base.ThrowException(ex);
            }
        }
        public void UpdateRefReturnTransactionItemsID(Int64 TransactionItemsID, Int64 ReturnTransactionItemsID)
        {
            try
            {
                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandType = System.Data.CommandType.Text;

                string SQL = "UPDATE tblTransactionItems SET " +
                                "RefReturnTransactionItemsID    =   @ReturnTransactionItemsID " +
                            "WHERE TransactionItemsID		    =	@TransactionItemsID; ";

                cmd.Parameters.AddWithValue("@ReturnTransactionItemsID", ReturnTransactionItemsID);
                cmd.Parameters.AddWithValue("@TransactionItemsID", TransactionItemsID);

                cmd.CommandText = SQL;
                base.ExecuteNonQuery(cmd);
            }
            catch (Exception ex)
            {
                throw base.ThrowException(ex);
            }
        }

        #endregion

        #region Details

        public SalesTransactionItemDetails[] Details(Int64 TransactionID, DateTime TransactionDate)
        {
            try
            {
                
                SalesTransactionItems clsItems = new SalesTransactionItems(base.Connection, base.Transaction);
                System.Data.DataTable  dt = clsItems.List(TransactionID, TransactionDate, "TransactionItemsID", SortOption.Ascending);

                ArrayList items = new ArrayList();
                
                int itemno = 1;
                foreach(System.Data.DataRow dr in dt.Rows)
                {
                    Data.SalesTransactionItemDetails itemDetails = new Data.SalesTransactionItemDetails();

                    itemDetails.ItemNo = itemno.ToString();
                    itemDetails.TransactionItemsID = Int64.Parse(dr["TransactionItemsID"].ToString());
                    itemDetails.ReturnTransactionItemsID = Int64.Parse(dr["ReturnTransactionItemsID"].ToString());
                    itemDetails.RefReturnTransactionItemsID = Int64.Parse(dr["RefReturnTransactionItemsID"].ToString());
                    itemDetails.TransactionID = Int64.Parse(dr["TransactionID"].ToString());
                    itemDetails.ProductID = Int64.Parse(dr["ProductID"].ToString());
                    itemDetails.ProductCode = "" + dr["ProductCode"].ToString();
                    itemDetails.BarCode = "" + dr["BarCode"].ToString();
                    itemDetails.Description = "" + dr["Description"].ToString();
                    itemDetails.ProductUnitID = Int32.Parse(dr["ProductUnitID"].ToString());
                    itemDetails.ProductUnitCode = "" + dr["ProductUnitCode"].ToString();
                    itemDetails.Quantity = decimal.Parse(dr["Quantity"].ToString());
                    itemDetails.Price = decimal.Parse(dr["Price"].ToString());
                    itemDetails.Discount = decimal.Parse(dr["Discount"].ToString());
                    itemDetails.ItemDiscount = decimal.Parse(dr["ItemDiscount"].ToString());
                    itemDetails.ItemDiscountType = (DiscountTypes)Enum.Parse(typeof(DiscountTypes), dr["ItemDiscountType"].ToString());
                    itemDetails.Amount = decimal.Parse(dr["Amount"].ToString());
                    itemDetails.VAT = decimal.Parse(dr["VAT"].ToString());
                    itemDetails.VatableAmount = decimal.Parse(dr["VatableAmount"].ToString());
                    itemDetails.NonVatableAmount = decimal.Parse(dr["NonVatableAmount"].ToString());
                    itemDetails.VATExempt = decimal.Parse(dr["VATExempt"].ToString());
                    itemDetails.EVAT = decimal.Parse(dr["EVAT"].ToString());
                    itemDetails.LocalTax = decimal.Parse(dr["LocalTax"].ToString());
                    itemDetails.VariationsMatrixID = Int64.Parse(dr["VariationsMatrixID"].ToString());
                    itemDetails.MatrixDescription = "" + dr["MatrixDescription"].ToString();
                    itemDetails.ProductGroup = "" + dr["ProductGroup"].ToString();
                    itemDetails.ProductSubGroup = "" + dr["ProductSubGroup"].ToString();
                    itemDetails.TransactionDate = TransactionDate;
                    itemDetails.TransactionItemStatus = (TransactionItemStatus)Enum.Parse(typeof(TransactionItemStatus), dr["TransactionItemStatus"].ToString());
                    itemDetails.DiscountCode = "" + dr["DiscountCode"].ToString();
                    itemDetails.DiscountRemarks = "" + dr["DiscountRemarks"].ToString();
                    itemDetails.ProductPackageID = Int64.Parse(dr["ProductPackageID"].ToString());
                    itemDetails.MatrixPackageID = Int64.Parse(dr["MatrixPackageID"].ToString());
                    itemDetails.PackageQuantity = decimal.Parse(dr["PackageQuantity"].ToString());
                    itemDetails.PromoQuantity = decimal.Parse(dr["PromoQuantity"].ToString());
                    itemDetails.PromoValue = decimal.Parse(dr["PromoValue"].ToString());
                    itemDetails.PromoInPercent = bool.Parse(dr["PromoInPercent"].ToString());
                    itemDetails.PromoType = (PromoTypes)Enum.Parse(typeof(PromoTypes), dr["PromoType"].ToString());
                    itemDetails.PromoApplied = decimal.Parse(dr["PromoApplied"].ToString());
                    itemDetails.PurchasePrice = decimal.Parse(dr["PurchasePrice"].ToString());
                    itemDetails.PurchaseAmount = decimal.Parse(dr["PurchaseAmount"].ToString());
                    itemDetails.IncludeInSubtotalDiscount = bool.Parse(dr["IncludeInSubtotalDiscount"].ToString());
                    itemDetails.IsCreditChargeExcluded = bool.Parse(dr["IsCreditChargeExcluded"].ToString());
                    itemDetails.OrderSlipPrinter1 = bool.Parse(dr["OrderSlipPrinter1"].ToString());
                    itemDetails.OrderSlipPrinter2 = bool.Parse(dr["OrderSlipPrinter2"].ToString());
                    itemDetails.OrderSlipPrinter3 = bool.Parse(dr["OrderSlipPrinter3"].ToString());
                    itemDetails.OrderSlipPrinter4 = bool.Parse(dr["OrderSlipPrinter4"].ToString());
                    itemDetails.OrderSlipPrinter5 = bool.Parse(dr["OrderSlipPrinter5"].ToString());
                    itemDetails.OrderSlipPrinted = Convert.ToBoolean(dr["OrderSlipPrinted"]);
                    itemDetails.PercentageCommision = decimal.Parse(dr["PercentageCommision"].ToString());
                    itemDetails.Commision = decimal.Parse(dr["Commision"].ToString());
                    itemDetails.RewardPoints = decimal.Parse(dr["RewardPoints"].ToString());
                    itemDetails.PaxNo = Int32.Parse(dr["PaxNo"].ToString());
                    itemDetails.SupplierID = Int64.Parse(dr["SupplierID"].ToString());
                    itemDetails.SupplierCode = "" + dr["SupplierCode"].ToString();
                    itemDetails.SupplierName = "" + dr["SupplierName"].ToString();
                    itemDetails.ItemRemarks = "" + dr["ItemRemarks"].ToString();

                    if (itemDetails.TransactionItemStatus == TransactionItemStatus.Return)
                    {
                        itemDetails.Amount = -itemDetails.Amount;
                        itemDetails.Commision = -itemDetails.Commision;
                        itemDetails.RewardPoints = -itemDetails.RewardPoints;
                    }
                    else if (itemDetails.TransactionItemStatus == TransactionItemStatus.Refund)
                    {
                        itemDetails.Amount = -itemDetails.Amount;
                        itemDetails.Commision = -itemDetails.Commision;
                        itemDetails.RewardPoints = -itemDetails.RewardPoints;
                    }
                    else if (itemDetails.TransactionItemStatus == TransactionItemStatus.Void)
                    {
                        itemDetails.Amount = 0;
                        itemDetails.Commision = 0;
                        itemDetails.RewardPoints = 0;
                    }
                    //else
                    //    itemDetails.Amount				= itemDetails.Amount;

                    items.Add(itemDetails);
                    itemno++;
                }

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
                throw base.ThrowException(ex);
            }
        }


        #endregion

        #region Streams

        public System.Data.DataTable SalesPerItem(string TransactionNo, string CustomerName, string CashierName, string TerminalNo,
            DateTime StartTransactionDate, DateTime EndTransactionDate, TransactionStatus Status, PaymentTypes PaymentType, SaleperItemFilterType pvtSaleperItemFilterType)
        {
            try
            {
                string SQL = "CALL procGenerateSalesPerItem(@SessionID, @TransactionNo, @CustomerName, @CashierName, @TerminalNo, @StartTransactionDate, @EndTransactionDate);";

                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandText = SQL;

                Random clsRandom = new Random();
                MySqlParameter prmSessionID = new MySqlParameter("@SessionID", clsRandom.Next(1234567, 99999999));

                cmd.Parameters.Add(prmSessionID);
                cmd.Parameters.AddWithValue("@TransactionNo", TransactionNo);
                cmd.Parameters.AddWithValue("@CustomerName", CustomerName);
                cmd.Parameters.AddWithValue("@CashierName", CashierName);
                cmd.Parameters.AddWithValue("@TerminalNo", TerminalNo);
                cmd.Parameters.AddWithValue("@StartTransactionDate", StartTransactionDate.ToString("yyyy-MM-dd HH:mm:ss"));
                cmd.Parameters.AddWithValue("@EndTransactionDate", EndTransactionDate.ToString("yyyy-MM-dd HH:mm:ss"));

                base.ExecuteNonQuery(cmd);

                SQL = "SELECT " +
                        "spi.ProductGroup," +
                        "spi.ProductID," +
                        "spi.ProductCode," +
                        "MAX(cntct.ContactCode) SupplierCode," +
                        "SUM(spi.Quantity) 'Quantity'," +
                        "SUM(spi.Amount) 'Amount'," +
                        "SUM(spi.PurchaseAmount) 'PurchaseAmount', " +
                        "SUM(spi.Discount) 'Discount', " +
                        "MIN(spi.PurchasePrice) 'PurchasePrice', " +
                        "MAX(spi.InvQuantity) 'InvQuantity' " +
                        "IFNULL(MIN(ppph.PurchasePrice),0) 'PurchasePrice2', " +
                        "IFNULL(MAX(cntct2.ContactCode),'') SupplierCode2 " +
                    "FROM tblSalesPerItem spi " +
                    "INNER JOIN tblProducts prd ON spi.ProductID = prd.ProductID " +
                    "INNER JOIN tblContacts cntct ON prd.SupplierID = cntct.ContactID " +
                    "LEFT OUTER JOIN ( " +
					"	SELECT ProductID, SupplierID, MIN(PurchasePrice) PurchasePrice " +
					"	FROM tblProductPurchasePriceHistory ppph WHERE ppph.PurchasePrice <> 0 " +
                    "                                                   AND PurchaseDate >= DATE_ADD(NOW(), INTERVAL -6 MONTH) " +
					"	GROUP BY ProductID " +
					") ppph ON prd.ProductID = ppph.ProductID " +
                    "                                                   AND ppph.SupplierID <> prd.SupplierID " +
                    "                                                   AND ppph.PurchasePrice <> spi.PurchasePrice " +
                    //"LEFT OUTER JOIN tblProductPurchasePriceHistory ppph ON prd.ProductID = ppph.ProductID " +
                    //                                                   "AND ppph.SupplierID <> prd.SupplierID " +
                    //                                                   "AND ppph.PurchasePrice <> 0 " +
                    //                                                   "AND ppph.PurchasePrice <> spi.PurchasePrice " +
                    //                                                   "AND PurchaseDate >= DATE_ADD(NOW(), INTERVAL -6 MONTH) " +
                    "LEFT OUTER JOIN tblContacts cntct2 ON ppph.SupplierID = cntct2.ContactID " +
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

                SQL += "GROUP BY spi.ProductGroup, spi.ProductCode  ORDER BY ProductCode, MIN(ppph.PurchasePrice);";

                cmd.CommandText = SQL;
                cmd.Parameters.Clear();
                cmd.Parameters.Add(prmSessionID);

                System.Data.DataTable dt = new System.Data.DataTable("SalesTransactionPerItem");
                base.MySqlDataAdapterFill(cmd, dt);

                SQL = "DELETE FROM tblSalesPerItem WHERE SessionID = @SessionID;";
                cmd.CommandText = SQL;
                cmd.Parameters.Clear();
                cmd.Parameters.Add(prmSessionID);
                base.ExecuteNonQuery(cmd);

                return dt;
            }
            catch (Exception ex)
            {
                throw base.ThrowException(ex);
            }
        }

        public System.Data.DataTable SalesPerItemByGroupProc(string ProductGroupName, string TransactionNo, string CustomerName, string CashierName, string TerminalNo,
            DateTime StartTransactionDate, DateTime EndTransactionDate, TransactionStatus Status, PaymentTypes PaymentType, SaleperItemFilterType pvtSaleperItemFilterType)
        {
            try
            {
                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                string SQL = "procGenerateSalesPerItemByGroup";

                Random clsRandom = new Random();
                MySqlParameter prmSessionID = new MySqlParameter("@strSessionID", clsRandom.Next(1234567, 99999999));

                cmd.Parameters.Add(prmSessionID);
                cmd.Parameters.AddWithValue("@strProductGroup", ProductGroupName);
                cmd.Parameters.AddWithValue("@strTransactionNo", TransactionNo);
                cmd.Parameters.AddWithValue("@strCustomerName", CustomerName);
                cmd.Parameters.AddWithValue("@strCashierName", CashierName);
                cmd.Parameters.AddWithValue("@strTerminalNo", TerminalNo);
                cmd.Parameters.AddWithValue("@dteStartTransactionDate", StartTransactionDate.ToString("yyyy-MM-dd HH:mm:ss"));
                cmd.Parameters.AddWithValue("@dteEndTransactionDate", EndTransactionDate.ToString("yyyy-MM-dd HH:mm:ss"));

                cmd.CommandText = SQL;
                base.ExecuteNonQuery(cmd);

                SQL = "SELECT " +
                        "spi.ProductGroup," +
                        "spi.ProductID," +
                        "spi.ProductCode," +
                        "MAX(cntct.ContactCode) SupplierCode," +
                        "SUM(spi.Quantity) 'Quantity'," +
                        "SUM(spi.Amount) 'Amount'," +
                        "SUM(spi.PurchaseAmount) 'PurchaseAmount', " +
                        "SUM(spi.Discount) 'Discount', " +
                        "MIN(spi.PurchasePrice) 'PurchasePrice', " +
                        "MAX(spi.InvQuantity) 'InvQuantity', " +
                        "IFNULL(MIN(ppph.PurchasePrice),0) 'PurchasePrice2', " +
                        "IFNULL(MAX(cntct2.ContactCode),'') SupplierCode2 " +
                    "FROM tblSalesPerItem spi " +
                    "INNER JOIN tblProducts prd ON spi.ProductID = prd.ProductID " +
                    "INNER JOIN tblContacts cntct ON prd.SupplierID = cntct.ContactID " +
                    "LEFT OUTER JOIN ( " +
                    "	SELECT ProductID, SupplierID, MIN(PurchasePrice) PurchasePrice " +
                    "	FROM tblProductPurchasePriceHistory ppph WHERE ppph.PurchasePrice <> 0 " +
                    "                                                   AND PurchaseDate >= DATE_ADD(NOW(), INTERVAL -6 MONTH) " +
                    "	GROUP BY ProductID " +
                    ") ppph ON prd.ProductID = ppph.ProductID " +
                    "                                                   AND ppph.SupplierID <> prd.SupplierID " +
                    "                                                   AND ppph.PurchasePrice <> spi.PurchasePrice " +
                    //"LEFT OUTER JOIN tblProductPurchasePriceHistory ppph ON prd.ProductID = ppph.ProductID " +
                    //                                                   "AND ppph.SupplierID <> prd.SupplierID " +
                    //                                                   "AND ppph.PurchasePrice <> 0 " +
                    //                                                   "AND ppph.PurchasePrice <> spi.PurchasePrice " +
                    //                                                   "AND PurchaseDate >= DATE_ADD(NOW(), INTERVAL -6 MONTH) " +
                    "LEFT OUTER JOIN tblContacts cntct2 ON ppph.SupplierID = cntct2.ContactID " +
                    "WHERE spi.SessionID = @strSessionID ";

                switch (pvtSaleperItemFilterType)
                {
                    case SaleperItemFilterType.ShowPositiveOnly:
                        SQL += "AND spi.Amount > PurchaseAmount ";
                        break;
                    case SaleperItemFilterType.ShowNegativeOnly:
                        SQL += "AND spi.Amount < PurchaseAmount ";
                        break;
                }

                SQL += "GROUP BY spi.ProductGroup, spi.ProductID, spi.ProductCode ORDER BY ProductCode, MIN(ppph.PurchasePrice);";

                cmd.CommandType = System.Data.CommandType.Text;
                cmd.Parameters.Clear();
                cmd.Parameters.Add(prmSessionID);

                cmd.CommandText = SQL;
                System.Data.DataTable dt = new System.Data.DataTable("SalesTransactionPerItem");
                base.MySqlDataAdapterFill(cmd, dt);

                SQL = "DELETE FROM tblSalesPerItem WHERE SessionID = @strSessionID;";

                cmd.CommandType = System.Data.CommandType.Text;
                cmd.Parameters.Clear();
                cmd.Parameters.Add(prmSessionID);

                cmd.CommandText = SQL;
                base.ExecuteNonQuery(cmd);

                return dt;
            }
            catch (Exception ex)
            {
                throw base.ThrowException(ex);
            }
        }

        public System.Data.DataTable SalesPerItemByGroup(string ProductGroupName, string TransactionNo, string CustomerName, string CashierName, string TerminalNo,
            DateTime StartTransactionDate, DateTime EndTransactionDate, TransactionStatus Status, PaymentTypes PaymentType, SaleperItemFilterType pvtSaleperItemFilterType)
        {
            try
            {
                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandType = System.Data.CommandType.Text;

                string SQL = "CALL procGenerateSalesPerItemByGroup(@SessionID, @ProductGroupName, @TransactionNo, @CustomerName, @CashierName, @TerminalNo, @StartTransactionDate, @EndTransactionDate);";


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

                cmd.CommandText = SQL;
                base.ExecuteNonQuery(cmd);

                SQL = "SELECT " +
                        "spi.ProductGroup," +
                        "spi.ProductID," +
                        "spi.ProductCode," +
                        "MAX(cntct.ContactCode) SupplierCode," +
                        "SUM(spi.Quantity) 'Quantity'," +
                        "SUM(spi.Amount) 'Amount'," +
                        "SUM(spi.PurchaseAmount) 'PurchaseAmount', " +
                        "SUM(spi.Discount) 'Discount', " +
                        "MIN(spi.PurchasePrice) 'PurchasePrice', " +
                        "MAX(spi.InvQuantity) 'InvQuantity', " +
                        "IFNULL(MIN(ppph.PurchasePrice),0) 'PurchasePrice2', " +
                        "IFNULL(MAX(cntct2.ContactCode),'') SupplierCode2 " +
                    "FROM tblSalesPerItem spi " +
                    "INNER JOIN tblProducts prd ON spi.ProductID = prd.ProductID " +
                    "INNER JOIN tblContacts cntct ON prd.SupplierID = cntct.ContactID " +
                    "LEFT OUTER JOIN ( " +
                    "	SELECT ProductID, SupplierID, MIN(PurchasePrice) PurchasePrice " +
                    "	FROM tblProductPurchasePriceHistory ppph WHERE ppph.PurchasePrice <> 0 " +
                    "                                                   AND PurchaseDate >= DATE_ADD(NOW(), INTERVAL -6 MONTH) " +
                    "	GROUP BY ProductID " +
                    ") ppph ON prd.ProductID = ppph.ProductID " +
                    "                                                   AND ppph.SupplierID <> prd.SupplierID " +
                    "                                                   AND ppph.PurchasePrice <> spi.PurchasePrice " +
                    //"LEFT OUTER JOIN tblProductPurchasePriceHistory ppph ON prd.ProductID = ppph.ProductID " +
                    //                                                   "AND ppph.SupplierID <> prd.SupplierID " +
                    //                                                   "AND ppph.PurchasePrice <> 0 " +
                    //                                                   "AND ppph.PurchasePrice <> spi.PurchasePrice " +
                    //                                                   "AND PurchaseDate >= DATE_ADD(NOW(), INTERVAL -6 MONTH) " +
                    "LEFT OUTER JOIN tblContacts cntct2 ON ppph.SupplierID = cntct2.ContactID " +
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

                SQL += "GROUP BY spi.ProductGroup, spi.ProductID, spi.ProductCode ORDER BY ProductCode, MIN(ppph.PurchasePrice);";

                cmd.CommandText = SQL;
                cmd.Parameters.Clear();
                cmd.Parameters.Add(prmSessionID);

                System.Data.DataTable dt = new System.Data.DataTable("SalesTransactionPerItem");
                base.MySqlDataAdapterFill(cmd, dt);

                SQL = "DELETE FROM tblSalesPerItem WHERE SessionID = @SessionID;";

                cmd.CommandText = SQL;
                cmd.Parameters.Clear();
                cmd.Parameters.Add(prmSessionID);
                base.ExecuteNonQuery(cmd);

                return dt;
            }
            catch (Exception ex)
            {
                throw base.ThrowException(ex);
            }
        }

        public System.Data.DataTable List(Int64 TransactionID, DateTime TransactionDate, string SortField, SortOption SortOrder)
        {
            try
            {
                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandType = System.Data.CommandType.Text;

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
                                "VATableAmount, " +
                                "NonVATableAmount, " +
                                "VATExempt, " +
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
                                "IsCreditChargeExcluded, " +
                                "OrderSlipPrinter1, " +
                                "OrderSlipPrinter2, " +
                                "OrderSlipPrinter3, " +
                                "OrderSlipPrinter4, " +
                                "OrderSlipPrinter5, " +
                                "OrderSlipPrinted, " +
                                "PercentageCommision, " +
                                "Commision, " +
                                "RewardPoints, " +
                                "ItemRemarks, " +
                                "PaxNo, " +
                                "SupplierID, " +
                                "SupplierCode, " +
                                "SupplierName, " +
                                "ReturnTransactionItemsID, " +
                                "RefReturnTransactionItemsID " +
                            "FROM tblTransactionItems " +
                            "WHERE TransactionID = @TransactionID AND TransactionItemStatus <> @TransactionItemStatus ORDER BY " + SortField;

                if (SortOrder == SortOption.Ascending)
                    SQL += " ASC";
                else
                    SQL += " DESC";

                cmd.Parameters.AddWithValue("@TransactionID", TransactionID);
                cmd.Parameters.AddWithValue("@TransactionDate", TransactionDate.ToString("yyyy-MM-dd HH:mm:ss"));
                cmd.Parameters.AddWithValue("@TransactionItemStatus", TransactionItemStatus.Trash.ToString("d"));

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

        public System.Data.DataTable List(string TransactionIDs)
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
                                "VATableAmount, " +
                                "NonVATableAmount, " +
                                "VATExempt, " +
                                "EVAT, " +
                                "LocalTax, " +
                                "VariationsMatrixID, " +
                                "MatrixDescription, " +
                                "ProductGroup, " +
                                "ProductSubGroup, " +
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
                                "IsCreditChargeExcluded, " +
                                "OrderSlipPrinter1, " +
                                "OrderSlipPrinter2, " +
                                "OrderSlipPrinter3, " +
                                "OrderSlipPrinter4, " +
                                "OrderSlipPrinter5, " +
                                "OrderSlipPrinted, " +
                                "PercentageCommision, " +
                                "Commision, " +
                                "RewardPoints, " +
                                "ItemRemarks, " +
                                "PaxNo, " +
                                "SupplierID, " +
                                "SupplierCode, " +
                                "SupplierName, " +
                                "ReturnTransactionItemsID, " +
                                "RefReturnTransactionItemsID " +
                            "FROM tblTransactionItems " +
                            "WHERE TransactionID IN (" + TransactionIDs + ") AND TransactionItemStatus <> @TransactionItemStatusTrash " +
                                "AND TransactionItemStatus <> @TransactionItemStatusVoid ";


                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandText = SQL;

                cmd.Parameters.AddWithValue("@TransactionItemStatusTrash", TransactionItemStatus.Trash.ToString("d"));
                cmd.Parameters.AddWithValue("@TransactionItemStatusVoid", TransactionItemStatus.Void.ToString("d"));

                string strDataTableName = "tbl" + this.GetType().FullName.Split(new Char[] { '.' })[this.GetType().FullName.Split(new Char[] { '.' }).Length - 1]; System.Data.DataTable dt = new System.Data.DataTable(strDataTableName);
                base.MySqlDataAdapterFill(cmd, dt);

                return dt;
            }
            catch (Exception ex)
            {
                throw base.ThrowException(ex);
            }
        }

        public System.Data.DataTable MostSalableItems(DateTime StartTransactionDate, DateTime EndTransactionDate, Int32 Limit)
        {
            try
            {
                string SQL = "CALL procGenerateSalesPerItem(@SessionID, @TransactionNo, @CustomerName, @CashierName, @TerminalNo, @StartTransactionDate, @EndTransactionDate);";

                MySqlCommand cmd = new MySqlCommand();
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

                base.ExecuteNonQuery(cmd);

                SQL = "SELECT " +
                        "ProductGroup, " +
                        "ProductCode," +
                        "ProductUnitCode," +
                        "SUM(Quantity) 'Count' " +
                    "FROM tblSalesPerItem " +
                    "WHERE SessionID = @SessionID " +
                    "GROUP BY ProductGroup, ProductCode " +
                    "ORDER BY SUM(Quantity) DESC ";
                if (Limit != 0) SQL += "LIMIT " + Limit.ToString() + ";";

                cmd.CommandText = SQL;
                cmd.Parameters.Clear();
                cmd.Parameters.Add(prmSessionID);
                base.ExecuteNonQuery(cmd);

                System.Data.DataTable dt = new System.Data.DataTable("MostSalableItems");
                base.MySqlDataAdapterFill(cmd, dt);


                SQL = "DELETE FROM tblSalesPerItem WHERE SessionID = @SessionID;";
                cmd.CommandText = SQL;
                cmd.Parameters.Clear();
                cmd.Parameters.Add(prmSessionID);
                base.ExecuteNonQuery(cmd);

                return dt;

            }
            catch (Exception ex)
            {
                throw base.ThrowException(ex);
            }
        }

        public System.Data.DataTable LeastSalableItems(DateTime StartTransactionDate, DateTime EndTransactionDate, Int32 Limit)
        {
            try
            {
                string SQL = "CALL procGenerateSalesPerItemWithZeroSales(@SessionID, @TransactionNo, @CustomerName, @CashierName, @TerminalNo, @StartTransactionDate, @EndTransactionDate);";

                MySqlCommand cmd = new MySqlCommand();
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

                base.ExecuteNonQuery(cmd);

                SQL = "SELECT " +
                        "ProductGroup, " +
                        "ProductCode," +
                        "ProductUnitCode," +
                        "SUM(Quantity) 'Count' " +
                    "FROM tblSalesPerItem " +
                    "WHERE SessionID = @SessionID " +
                    "GROUP BY ProductGroup, ProductCode " +
                    "ORDER BY SUM(Quantity) ASC ";
                if (Limit != 0) SQL += "LIMIT " + Limit.ToString() + ";";

                cmd.CommandText = SQL;
                cmd.Parameters.Clear();
                cmd.Parameters.Add(prmSessionID);
                base.ExecuteNonQuery(cmd);

                System.Data.DataTable dt = new System.Data.DataTable("LeastSalableItems");
                base.MySqlDataAdapterFill(cmd, dt);
                

                SQL = "DELETE FROM tblSalesPerItem WHERE SessionID = @SessionID;";
                cmd.CommandText = SQL;
                cmd.Parameters.Clear();
                cmd.Parameters.Add(prmSessionID);
                base.ExecuteNonQuery(cmd);

                return dt;
            }
            catch (Exception ex)
            {
                throw base.ThrowException(ex);
            }
        }

        public System.Data.DataTable ProductHistoryReport(long ProductID, DateTime StartDate, DateTime EndDate)
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

                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandText = SQL;

                System.Data.DataTable dt = new System.Data.DataTable("tblProductHistoryReport");
                base.MySqlDataAdapterFill(cmd, dt);

                return dt;
            }
            catch (Exception ex)
            {
                throw base.ThrowException(ex);
            }
        }

        public System.Data.DataTable AgentsCommision(string byvalDepartment, string byvalPosition, DateTime StartTransactionDate, DateTime EndTransactionDate)
        {
            try
            {
                string SQL = "CALL procGenerateAllAgentsCommision(@SessionID, @StartTransactionDate, @EndTransactionDate);";

                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandText = SQL;

                Random clsRandom = new Random();
                MySqlParameter prmSessionID = new MySqlParameter("@SessionID",MySqlDbType.String);
                prmSessionID.Value = clsRandom.Next(1234567, 99999999);
                cmd.Parameters.Add(prmSessionID);
                cmd.Parameters.AddWithValue("@StartTransactionDate", StartTransactionDate.ToString("yyyy-MM-dd HH:mm:ss"));
                cmd.Parameters.AddWithValue("@EndTransactionDate", EndTransactionDate.ToString("yyyy-MM-dd HH:mm:ss"));

                base.ExecuteNonQuery(cmd);

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
                base.ExecuteNonQuery(cmd);

                System.Data.DataTable dt = new System.Data.DataTable("AgentsCommision");
                base.MySqlDataAdapterFill(cmd, dt);
                

                SQL = "DELETE FROM tblAgentsCommision WHERE SessionID = @SessionID;";
                cmd.CommandText = SQL;
                cmd.Parameters.Clear();
                cmd.Parameters.Add(prmSessionID);
                base.ExecuteNonQuery(cmd);

                return dt;
            }
            catch (Exception ex)
            {
                throw base.ThrowException(ex);
            }
        }

        public System.Data.DataTable AgentsCommision(long AgentID, DateTime StartTransactionDate, DateTime EndTransactionDate)
        {
            try
            {
                string SQL = "CALL procGenerateAgentsCommision(@SessionID, @AgentID, @StartTransactionDate, @EndTransactionDate);";

                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandText = SQL;

                Random clsRandom = new Random();
                MySqlParameter prmSessionID = new MySqlParameter("@SessionID",MySqlDbType.String);
                prmSessionID.Value = clsRandom.Next(1234567, 99999999);
                cmd.Parameters.Add(prmSessionID);
                cmd.Parameters.AddWithValue("@AgentID", AgentID);
                cmd.Parameters.AddWithValue("@StartTransactionDate", StartTransactionDate.ToString("yyyy-MM-dd HH:mm:ss"));
                cmd.Parameters.AddWithValue("@EndTransactionDate", EndTransactionDate.ToString("yyyy-MM-dd HH:mm:ss"));

                base.ExecuteNonQuery(cmd);

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
                base.ExecuteNonQuery(cmd);

                System.Data.DataTable dt = new System.Data.DataTable("AgentsCommision");
                base.MySqlDataAdapterFill(cmd, dt);
                

                SQL = "DELETE FROM tblAgentsCommision WHERE SessionID = @SessionID;";
                cmd.CommandText = SQL;
                cmd.Parameters.Clear();
                cmd.Parameters.Add(prmSessionID);
                base.ExecuteNonQuery(cmd);

                return dt;
            }
            catch (Exception ex)
            {
                throw base.ThrowException(ex);
            }
        }

        public System.Data.DataTable ProductsInDemoReport(Int32 BranchID = 0, Int64 SupplierID = 0, string GroupCode = "" , string SubGroupCode = "", string ProductCode = "", string CustomerName = "", DateTime? TransactionDateFrom = null, DateTime? TransactionDateTo = null, DateTime? ReturnDateFrom = null, DateTime? ReturnDateTo = null)
        {
            try
            {
                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandType = System.Data.CommandType.Text;
                
                string SQL = "SELECT items.ProductCode, items.Description, trx.DateClosed, trx.CustomerName, " +
		                            "trxRet.DateClosed ReturnDate, trx.CashierName ReleasedBy, trxret.CashierName ReturnTo, items.ItemRemarks ReleaseRemarks, itemsret.ItemRemarks ReturnRemarks " +
                             "FROM tblTransactionItems items " +
                                "INNER JOIN tblTransactionItems itemsret ON items.RefReturnTransactionItemsID = itemsret.TransactionItemsID AND itemsret.TransactionItemStatus = 3 " +
                                "INNER JOIN tblTransactions trx ON items.TransactionID = trx.TransactionID " +
                                "INNER JOIN tblTransactions trxret ON itemsret.TransactionID = trxret.TransactionID " +
                                "WHERE items.TransactionItemStatus = 7 "; // Demo

                #region Search Queries

                if (BranchID != 0)
                {
                    SQL += "AND trx.BranchID = @BranchID ";
                    cmd.Parameters.AddWithValue("@BranchID", BranchID);
                }
                if (SupplierID != 0)
                {
                    SQL += "AND items.SupplierID = @SupplierID ";
                    cmd.Parameters.AddWithValue("@SupplierID", SupplierID);
                }
                if (!string.IsNullOrEmpty(GroupCode))
                {
                    SQL += "AND items.ProductGroup = @GroupCode ";
                    cmd.Parameters.AddWithValue("@GroupCode", GroupCode);
                }
                if (!string.IsNullOrEmpty(SubGroupCode))
                {
                    SQL += "AND items.ProductSubGroup = @SubGroupCode ";
                    cmd.Parameters.AddWithValue("@SubGroupCode", SubGroupCode);
                }
                if (!string.IsNullOrEmpty(ProductCode))
                {
                    SQL += "AND items.ProductCode = @ProductCode ";
                    cmd.Parameters.AddWithValue("@ProductCode", ProductCode);
                }
                if (!string.IsNullOrEmpty(CustomerName))
                {
                    SQL += "AND trx.CustomerName = @CustomerName ";
                    cmd.Parameters.AddWithValue("@CustomerName", CustomerName);
                }
                if (TransactionDateFrom.GetValueOrDefault() != DateTime.MinValue)
                {
                    SQL += "AND trx.DateClosed >= @TransactionDateFrom ";
                    cmd.Parameters.AddWithValue("@TransactionDateFrom", TransactionDateFrom.GetValueOrDefault() == DateTime.MinValue ? Constants.C_DATE_MIN_VALUE : TransactionDateFrom);
                }
                if (TransactionDateTo.GetValueOrDefault() != DateTime.MinValue)
                {
                    SQL += "AND trx.DateClosed <= @TransactionDateTo ";
                    cmd.Parameters.AddWithValue("@TransactionDateTo", TransactionDateTo.GetValueOrDefault() == DateTime.MinValue ? Constants.C_DATE_MIN_VALUE : TransactionDateTo);
                }

                if (ReturnDateFrom.GetValueOrDefault() != DateTime.MinValue)
                {
                    SQL += "AND trxret.DateClosed >= @ReturnDateFrom ";
                    cmd.Parameters.AddWithValue("@ReturnDateFrom", ReturnDateFrom.GetValueOrDefault() == DateTime.MinValue ? Constants.C_DATE_MIN_VALUE : ReturnDateFrom);
                }
                if (ReturnDateTo.GetValueOrDefault() != DateTime.MinValue)
                {
                    SQL += "AND trxret.DateClosed <= @ReturnDateTo ";
                    cmd.Parameters.AddWithValue("@ReturnDateTo", ReturnDateTo.GetValueOrDefault() == DateTime.MinValue ? Constants.C_DATE_MIN_VALUE : ReturnDateTo);
                }
                #endregion

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

        public void Return(Int64 TransactionItemsID)
        {
            try
            {
                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandType = System.Data.CommandType.Text;

                string SQL = "UPDATE tblTransactionItems SET " +
                                "Quantity						=	Quantity * -1, " +
                                "Price							=	Price * -1, " +
                                "Discount						=	Discount * -1, " +
                                "Amount							=	Amount * -1, " +
                                "Commision						=	Commision * -1, " +
                                "RewardPoints				    =	RewardPoints * -1, " +
                                "VAT							=	VAT * -1, " +
                                "EVAT							=	EVAT * -1, " +
                                "LocalTax						=	LocalTax * -1, " +
                                "TransactionItemStatus			=	@TransactionItemStatus, " +
                                "LastModified		            =	NOW() " +
                            "WHERE TransactionItemsID		=	@TransactionItemsID;";

                cmd.Parameters.AddWithValue("@TransactionItemStatus", TransactionItemStatus.Return.ToString("d"));
                cmd.Parameters.AddWithValue("@TransactionItemsID", TransactionItemsID);

                cmd.CommandText = SQL;
                base.ExecuteNonQuery(cmd);
            }
            catch (Exception ex)
            {
                throw base.ThrowException(ex);
            }
        }

        public void setItemAsDemo(Int64 TransactionItemsID)
        {
            try
            {
                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandType = System.Data.CommandType.Text;

                string SQL = "UPDATE tblTransactionItems SET " +
                                "Price							=	0, " +
                                "Discount						=	0, " +
                                "Amount							=	0, " +
                                "Commision						=	0, " +
                                "RewardPoints					=	0, " +
                                "VAT							=	0, " +
                                "EVAT							=	0, " +
                                "LocalTax						=	0, " +
                                "NonVATableAmount				=	0, " +
                                "NonEVATableAmount				=	0, " +
                                "GrossSales						=	0, " +
                                "SellingPrice					=	0, " +
                                "VatableAmount					=	0, " +
                                "EVatableAmount					=	0, " +
                                "PurchasePrice                  =   0, " +
                                "PurchaseAmount                 =   0, " +
                                "PromoApplied                   =   0, " +
                                "PercentageCommision            =   0, " +
                                "Commision                      =   0, " +
                                "RewardPoints                   =   0, " +
                                "TransactionItemStatus			=	@TransactionItemStatus, " +
                                "OrderSlipPrinted               =   0, " +
                                "LastModified		            =	NOW() " +
                             "WHERE TransactionItemsID		=	@TransactionItemsID;";

                cmd.Parameters.AddWithValue("@TransactionItemStatus", TransactionItemStatus.Demo.ToString("d"));
                cmd.Parameters.AddWithValue("@TransactionItemsID", TransactionItemsID);

                cmd.CommandText = SQL;
                base.ExecuteNonQuery(cmd);
            }
            catch (Exception ex)
            {
                throw base.ThrowException(ex);
            }
        }

        public void Void(Int64 TransactionItemsID)
        {
            try
            {
                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandType = System.Data.CommandType.Text;

                string SQL = "UPDATE tblTransactionItems SET " +
                                "ReturnTransactionItemsID       =   0, " +
                                "RefReturnTransactionItemsID    =   0, " +
                                "Quantity						=	Quantity * -1, " +
                                "Price							=	Price * -1, " +
                                "Discount						=	Discount * -1, " +
                                "Amount							=	Amount * -1, " +
                                "Commision						=	0, " +
                                "RewardPoints					=	0, " +
                                "VAT							=	VAT * -1, " +
                                "EVAT							=	EVAT * -1, " +
                                "LocalTax						=	LocalTax * -1, " +
                                "TransactionItemStatus			=	@TransactionItemStatus, " +
                                "OrderSlipPrinted               =   0, " +
                                "LastModified		            =	NOW() " +
                             "WHERE TransactionItemsID		=	@TransactionItemsID;";

                cmd.Parameters.AddWithValue("@TransactionItemStatus", TransactionItemStatus.Void.ToString("d"));
                cmd.Parameters.AddWithValue("@TransactionItemsID", TransactionItemsID);

                cmd.CommandText = SQL;
                base.ExecuteNonQuery(cmd);
            }
            catch (Exception ex)
            {
                throw base.ThrowException(ex);
            }
        }

        public void VoidByTransaction(Int64 TransactionID)
        {
            try
            {
                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandType = System.Data.CommandType.Text;

                string SQL = "UPDATE tblTransactionItems SET " +
                                "ReturnTransactionItemsID       =   0, " +
                                "RefReturnTransactionItemsID    =   0, " +
                                "Quantity						=	Quantity * -1, " +
                                "Price							=	Price * -1, " +
                                "Discount						=	Discount * -1, " +
                                "Amount							=	Amount * -1, " +
                                "Commision						=	0, " +
                                "RewardPoints					=	0, " +
                                "VAT							=	VAT * -1, " +
                                "EVAT							=	EVAT * -1, " +
                                "LocalTax						=	LocalTax * -1, " +
                                "TransactionItemStatus			=	@TransactionItemStatus, " +
                                "OrderSlipPrinted               =   0, " +
                                "LastModified		            =	NOW() " +
                            "WHERE TransactionID			=	@TransactionID;";

                cmd.Parameters.AddWithValue("@TransactionItemStatus", TransactionItemStatus.Void.ToString("d"));
                cmd.Parameters.AddWithValue("@TransactionID", TransactionID);

                cmd.CommandText = SQL;
                base.ExecuteNonQuery(cmd);
            }
            catch (Exception ex)
            {
                throw base.ThrowException(ex);
            }
        }
        public void RefundByTransaction(Int64 TransactionID)
        {
            try
            {
                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandType = System.Data.CommandType.Text;

                string SQL = "UPDATE tblTransactionItems SET " +
                                "Quantity						=	Quantity * -1, " +
                                "Price							=	Price * -1, " +
                                "Discount						=	Discount * -1, " +
                                "Amount							=	Amount * -1, " +
                                "Commision						=	Commision * -1, " +
                                "RewardPoints					=	RewardPoints * -1, " +
                                "VAT							=	VAT * -1, " +
                                "EVAT							=	EVAT * -1, " +
                                "LocalTax						=	LocalTax * -1, " +
                                "TransactionItemStatus			=	@TransactionItemStatus, " +
                                "LastModified		            =	NOW() " +
                            "WHERE TransactionID			=	@TransactionID;";

                cmd.Parameters.AddWithValue("@TransactionItemStatus", TransactionItemStatus.Refund.ToString("d"));
                cmd.Parameters.AddWithValue("@TransactionID", TransactionID);

                cmd.CommandText = SQL;
                base.ExecuteNonQuery(cmd);
            }
            catch (Exception ex)
            {
                throw base.ThrowException(ex);
            }
        }
        public void Trash(Int64 TransactionItemsID)
        {
            try
            {
                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandType = System.Data.CommandType.Text;

                string SQL = "DELETE FROM tblTransactionItems " +
                            "WHERE TransactionItemsID		=	@TransactionItemsID;";

                cmd.Parameters.AddWithValue("@TransactionItemsID", TransactionItemsID);

                cmd.CommandText = SQL;
                base.ExecuteNonQuery(cmd);
            }
            catch (Exception ex)
            {
                throw base.ThrowException(ex);
            }
        }
        public void CloseAsOrderSlip(Int64 TransactionID)
        {
            try
            {
                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandType = System.Data.CommandType.Text;

                string SQL = "UPDATE tblTransactionItems SET " +
                                "TransactionItemStatus		=	@TransactionItemStatus, " +
                                "LastModified		        =	NOW() " +
                            "WHERE TransactionID			=	@TransactionID;";

                cmd.Parameters.AddWithValue("@TransactionItemStatus", TransactionItemStatus.OrderSlip.ToString("d"));
                cmd.Parameters.AddWithValue("@TransactionID", TransactionID);

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
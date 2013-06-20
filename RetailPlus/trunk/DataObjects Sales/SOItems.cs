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

    #region SOItemDetails

    public struct SOItemDetails
    {
        public long SOItemID;
        public long SOID;
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
        public SOItemStatus SOItemStatus;
        public bool IsVatable;
        public string Remarks;
        public int ChartOfAccountIDSold;
        public int ChartOfAccountIDTaxSold;
        public int ChartOfAccountIDInventory;
        // Added July 1, 2010 4:20PM
        public decimal SellingPrice;
        public decimal SellingVAT;
        public decimal SellingEVAT;
        public decimal SellingLocalTax;
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
    public class SOItem : POSConnection
    {
        #region Constructors and Destructors

		public SOItem()
            : base(null, null)
        {
        }

        public SOItem(MySqlConnection Connection, MySqlTransaction Transaction) 
            : base(Connection, Transaction)
		{

		}

		#endregion

        #region Insert and Update

        public long Insert(SOItemDetails Details)
        {
            try
            {
                string SQL = "INSERT INTO tblSOItems (" +
                                "SOID, " +
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
                                "SOItemStatus, " +
                                "IsVatable, " +
                                "Remarks, " +
                                "ChartOfAccountIDSold, " +
                                "ChartOfAccountIDTaxSold, " +
                                "ChartOfAccountIDInventory," +
                                "SellingPrice," +
                                "SellingVAT," +
                                "SellingEVAT," +
                                "SellingLocalTax" +
                            ") VALUES (" +
                                "@SOID, " +
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
                                "@SOItemStatus, " +
                                "@IsVatable, " +
                                "@Remarks, " +
                                "(SELECT ChartOfAccountIDSold FROM tblProducts WHERE ProductID = @ProductID), " +
                                "(SELECT ChartOfAccountIDTaxSold FROM tblProducts WHERE ProductID = @ProductID), " +
                                "(SELECT ChartOfAccountIDInventory FROM tblProducts WHERE ProductID = @ProductID), " +
                                "@SellingPrice," +
                                "@SellingVAT," +
                                "@SellingEVAT," +
                                "@SellingLocalTax" +
                            ");";

                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandText = SQL;

                MySqlParameter prmSOID = new MySqlParameter("@SOID",MySqlDbType.Int64);
                prmSOID.Value = Details.SOID;
                cmd.Parameters.Add(prmSOID);

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

                MySqlParameter prmSOItemStatus = new MySqlParameter("@SOItemStatus",MySqlDbType.Int16);
                prmSOItemStatus.Value = Details.SOItemStatus.ToString("d");
                cmd.Parameters.Add(prmSOItemStatus);

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

        public void Update(SOItemDetails Details)
        {
            try
            {
                string SQL = "UPDATE tblSOItems SET " +
                                "SOID					=	@SOID, " +
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
                                "SOItemStatus			=	@SOItemStatus, " +
                                "IsVatable				=	@IsVatable, " +
                                "Remarks				=	@Remarks, " +
                                "ChartOfAccountIDSold       = (SELECT ChartOfAccountIDSold FROM tblProducts WHERE ProductID = @ProductID), " +
                                "ChartOfAccountIDTaxSold    = (SELECT ChartOfAccountIDTaxSold FROM tblProducts WHERE ProductID = @ProductID), " +
                                "ChartOfAccountIDInventory      = (SELECT ChartOfAccountIDInventory FROM tblProducts WHERE ProductID = @ProductID), " +
                                "SellingPrice			=	@SellingPrice, " +
                                "SellingVAT				=	@SellingVAT, " +
                                "SellingEVAT			=	@SellingEVAT, " +
                                "SellingLocalTax		=	@SellingLocalTax " +
                            "WHERE SOItemID = @SOItemID;";

                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandText = SQL;

                MySqlParameter prmSOID = new MySqlParameter("@SOID",MySqlDbType.Int64);
                prmSOID.Value = Details.SOID;
                cmd.Parameters.Add(prmSOID);

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

                MySqlParameter prmSOItemStatus = new MySqlParameter("@SOItemStatus",MySqlDbType.Int16);
                prmSOItemStatus.Value = Details.SOItemStatus.ToString("d");
                cmd.Parameters.Add(prmSOItemStatus);

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
                cmd.Parameters.AddWithValue("@SOItemID", Details.SOItemID);

                base.ExecuteNonQuery(cmd);

                SO clsSO = new SO(base.Connection, base.Transaction);
                clsSO.SynchronizeAmount(Details.SOID);
            }

            catch (Exception ex)
            {
                throw base.ThrowException(ex);
            }
        }

        public void Post(long SOID)
        {
            try
            {
                string SQL = "UPDATE tblSOItems SET " +
                                "SOItemStatus			=	@SOItemStatus " +
                            "WHERE SOID = @SOID;";

                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandText = SQL;

                MySqlParameter prmSOItemStatus = new MySqlParameter("@SOItemStatus",MySqlDbType.Int16);
                prmSOItemStatus.Value = SOItemStatus.Posted.ToString("d");
                cmd.Parameters.Add(prmSOItemStatus);

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

        public void Cancel(long SOID)
        {
            try
            {
                string SQL = "UPDATE tblSOItems SET " +
                                "SOItemStatus			=	@SOItemStatus " +
                            "WHERE SOID = @SOID;";

                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandText = SQL;

                MySqlParameter prmSOItemStatus = new MySqlParameter("@SOItemStatus",MySqlDbType.Int16);
                prmSOItemStatus.Value = SOItemStatus.Cancelled.ToString("d");
                cmd.Parameters.Add(prmSOItemStatus);

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

        #region Delete

        public bool Delete(string IDs)
        {
            try
            {
                string SQL = "DELETE FROM tblSOItems WHERE SOItemID IN (" + IDs + ");";

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
                                "SOItemID, " +
                                "SOID, " +
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
                                "SOItemStatus, " +
                                "IsVatable, " +
                                "Remarks, " +
                                "ChartOfAccountIDSold, " +
                                "ChartOfAccountIDTaxSold, " +
                                "ChartOfAccountIDInventory, " +
                                "SellingPrice, " +
                                "SellingVAT, " +
                                "SellingEVAT, " +
                                "SellingLocalTax " +
                            "FROM tblSOItems ";
            return stSQL;
        }

        #region Details

        public SOItemDetails Details(long SOItemID)
        {
            try
            {
                string SQL = SQLSelect() + "WHERE SOItemID = @SOItemID;";

                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandText = SQL;

                MySqlParameter prmSOItemID = new MySqlParameter("@SOItemID",MySqlDbType.Int64);
                prmSOItemID.Value = SOItemID;
                cmd.Parameters.Add(prmSOItemID);

                MySqlDataReader myReader = base.ExecuteReader(cmd, System.Data.CommandBehavior.SingleResult);

                SOItemDetails Details = new SOItemDetails();

                while (myReader.Read())
                {
                    Details.SOItemID = SOItemID;
                    Details.SOID = myReader.GetInt64("SOID");
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
                    Details.SOItemStatus = (SOItemStatus)Enum.Parse(typeof(SOItemStatus), myReader.GetString("SOItemStatus"));
                    if (myReader["IsVatable"].ToString() == "1")
                        Details.IsVatable = true;
                    Details.Remarks = "" + myReader["Remarks"].ToString();

                    //Added Jul 1, 2010 4:27PM : For selling suggested selling price information
                    Details.SellingPrice = myReader.GetDecimal("SellingPrice");
                    Details.SellingVAT = myReader.GetDecimal("SellingVAT");
                    Details.SellingEVAT = myReader.GetDecimal("SellingEVAT");
                    Details.SellingLocalTax = myReader.GetDecimal("SellingLocalTax");
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

        public System.Data.DataTable ListAsDataTable(string SortField, SortOption SortOrder)
        {
            if (SortField == string.Empty || SortField == null) SortField = "SOItemID";

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
        public MySqlDataReader List(string SortField, SortOption SortOrder)
        {
            try
            {
                if (SortField == string.Empty || SortField == null) SortField = "SOItemID";

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
        public System.Data.DataTable ListAsDataTable(long SOID = 0, string SortField = "SOItemID", SortOption SortOrder = SortOption.Ascending)
        {
            try
            {
                MySqlCommand cmd = new MySqlCommand();
                string SQL = SQLSelect();

                if (SOID != 0)
                {
                    SQL += "WHERE SOID = @SOID ";

                    MySqlParameter prmSOID = new MySqlParameter("@SOID", MySqlDbType.Int64);
                    prmSOID.Value = SOID;
                    cmd.Parameters.Add(prmSOID);
                }

                SQL += "ORDER BY " + SortField;
                if (SortOrder == SortOption.Ascending)
                    SQL += " ASC";
                else
                    SQL += " DESC";

                cmd.CommandType = System.Data.CommandType.Text;
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
        public MySqlDataReader List(SOItemStatus SOItemstatus, string SortField, SortOption SortOrder)
        {
            try
            {
                if (SortField == string.Empty || SortField == null) SortField = "SOItemID";

                string SQL = SQLSelect() + "WHERE SOItemStatus = @SOItemStatus " +
                            "ORDER BY " + SortField;

                if (SortOrder == SortOption.Ascending)
                    SQL += " ASC";
                else
                    SQL += " DESC";

                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandText = SQL;

                MySqlParameter prmSOItemStatus = new MySqlParameter("@SOItemStatus",MySqlDbType.Int16);
                prmSOItemStatus.Value = SOItemstatus.ToString("d");
                cmd.Parameters.Add(prmSOItemStatus);

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
                if (SortField == string.Empty || SortField == null) SortField = "SOItemID";

                string SQL = SQLSelect() + "WHERE (ProductCode LIKE @SearchKey or BarCode LIKE @SearchKey or Description LIKE @SearchKey " +
                                        "or MatrixDescription LIKE @SearchKey or ProductGroup LIKE @SearchKey or ProductSubGroup LIKE @SearchKey " +
                                        "or Remarks LIKE @SearchKey) " +
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
        public MySqlDataReader Search(SOItemStatus SOItemstatus, string SearchKey, string SortField, SortOption SortOrder)
        {
            try
            {
                if (SortField == string.Empty || SortField == null) SortField = "SOItemID";

                string SQL = SQLSelect() + "WHERE (ProductCode LIKE @SearchKey or BarCode LIKE @SearchKey or Description LIKE @SearchKey " +
                                    "or MatrixDescription LIKE @SearchKey or ProductGroup LIKE @SearchKey or ProductSubGroup LIKE @SearchKey " +
                                    "or Remarks LIKE @SearchKey) " +
                            "ORDER BY " + SortField;

                if (SortOrder == SortOption.Ascending)
                    SQL += " ASC";
                else
                    SQL += " DESC";

                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandText = SQL;

                MySqlParameter prmSOItemStatus = new MySqlParameter("@SOItemStatus",MySqlDbType.Int16);
                prmSOItemStatus.Value = SOItemstatus.ToString("d");
                cmd.Parameters.Add(prmSOItemStatus);

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

        #endregion
    }
}


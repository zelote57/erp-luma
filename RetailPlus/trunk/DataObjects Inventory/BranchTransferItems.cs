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

	#region BranchTransferItemDetails

	public struct BranchTransferItemDetails
	{
		public long BranchTransferItemID;
		public long BranchTransferID;
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
		public BranchTransferItemStatus BranchTransferItemStatus;
		public bool IsVatable;
		public string Remarks;
        public decimal SellingPrice;
        public decimal SellingVAT;
        public decimal SellingEVAT;
        public decimal SellingLocalTax;
        public decimal OldSellingPrice;
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
	public class BranchTransferItem : POSConnection
	{
		#region Constructors and Destructors

		public BranchTransferItem()
            : base(null, null)
        {
        }

        public BranchTransferItem(MySqlConnection Connection, MySqlTransaction Transaction) 
            : base(Connection, Transaction)
		{

		}

		#endregion

		#region Insert and Update

		public long Insert(BranchTransferItemDetails Details)
		{
			try 
			{
				string SQL = "INSERT INTO tblBranchTransferItems (" +
								"BranchTransferID, " +
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
								"BranchTransferItemStatus, " +
								"IsVatable, " +
								"Remarks, " +
                                "SellingPrice," +
                                "SellingVAT," +
                                "SellingEVAT," +
                                "SellingLocalTax," +
                                "OldSellingPrice" +
							") VALUES (" +
								"@BranchTransferID, " +
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
								"@BranchTransferItemStatus, " +
								"@IsVatable, " +
                                "@Remarks, " +
                                "@SellingPrice," +
                                "@SellingVAT," +
                                "@SellingEVAT," +
                                "@SellingLocalTax," +
                                "@OldSellingPrice" +
							");";
				  
				MySqlCommand cmd = new MySqlCommand();
				cmd.CommandType = System.Data.CommandType.Text;
				cmd.CommandText = SQL;
				
				MySqlParameter prmBranchTransferID = new MySqlParameter("@BranchTransferID",MySqlDbType.Int64);			
				prmBranchTransferID.Value = Details.BranchTransferID;
				cmd.Parameters.Add(prmBranchTransferID);

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
				prmDiscountType.Value = (int) Details.DiscountType;
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
                cmd.Parameters.AddWithValue("@BranchTransferItemStatus", Details.BranchTransferItemStatus.ToString("d"));
                cmd.Parameters.AddWithValue("@IsVatable", Convert.ToInt16(Details.IsVatable));
                cmd.Parameters.AddWithValue("@Remarks", Details.Remarks);
                cmd.Parameters.AddWithValue("@SellingPrice", Details.SellingPrice);
                cmd.Parameters.AddWithValue("@SellingVAT", Details.SellingVAT);
                cmd.Parameters.AddWithValue("@SellingEVAT", Details.SellingEVAT);
                cmd.Parameters.AddWithValue("@SellingLocalTax", Details.SellingLocalTax);
                cmd.Parameters.AddWithValue("@OldSellingPrice", Details.OldSellingPrice);

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

                BranchTransfer clsBranchTransfer = new BranchTransfer(base.Connection, base.Transaction);
                clsBranchTransfer.SynchronizeAmount(Details.BranchTransferID);

				return iID;
			}

			catch (Exception ex)
			{
				throw base.ThrowException(ex);
			}	
		}

		public void Update(BranchTransferItemDetails Details)
		{
			try 
			{
				string SQL=	"UPDATE tblBranchTransferItems SET " + 
								"BranchTransferID					=	@BranchTransferID, " +
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
								"BranchTransferItemStatus			=	@BranchTransferItemStatus, " +
								"IsVatable				=	@IsVatable, " +
								"Remarks				=	@Remarks, " +
                                "SellingPrice			=	@SellingPrice, " +
                                "SellingVAT				=	@SellingVAT, " +
                                "SellingEVAT			=	@SellingEVAT, " +
                                "SellingLocalTax		=	@SellingLocalTax, " +
                                "OldSellingPrice		=	@OldSellingPrice " +
							"WHERE BranchTransferItemID = @BranchTransferItemID;";
				  
				MySqlCommand cmd = new MySqlCommand();
				cmd.CommandType = System.Data.CommandType.Text;
				cmd.CommandText = SQL;
				
				MySqlParameter prmBranchTransferID = new MySqlParameter("@BranchTransferID",MySqlDbType.Int64);			
				prmBranchTransferID.Value = Details.BranchTransferID;
				cmd.Parameters.Add(prmBranchTransferID);

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
                cmd.Parameters.AddWithValue("@BranchTransferItemStatus", Details.BranchTransferItemStatus.ToString("d"));
                cmd.Parameters.AddWithValue("@IsVatable", Convert.ToInt16(Details.IsVatable));
                cmd.Parameters.AddWithValue("@Remarks", Details.Remarks);
                cmd.Parameters.AddWithValue("@SellingPrice", Details.SellingPrice);
                cmd.Parameters.AddWithValue("@SellingVAT", Details.SellingVAT);
                cmd.Parameters.AddWithValue("@SellingEVAT", Details.SellingEVAT);
                cmd.Parameters.AddWithValue("@SellingLocalTax", Details.SellingLocalTax);
                cmd.Parameters.AddWithValue("@OldSellingPrice", Details.OldSellingPrice);
                cmd.Parameters.AddWithValue("@BranchTransferItemID", Details.BranchTransferItemID);

				base.ExecuteNonQuery(cmd);

                BranchTransfer clsBranchTransfer = new BranchTransfer(base.Connection, base.Transaction);
                clsBranchTransfer.SynchronizeAmount(Details.BranchTransferID);
			}

			catch (Exception ex)
			{
				throw base.ThrowException(ex);
			}	
		}

		public void Post(long BranchTransferID)
		{
			try 
			{
				string SQL=	"UPDATE tblBranchTransferItems SET " + 
								"BranchTransferItemStatus			=	@BranchTransferItemStatus " +
							"WHERE BranchTransferID = @BranchTransferID;";
				  
				MySqlCommand cmd = new MySqlCommand();
				cmd.CommandType = System.Data.CommandType.Text;
				cmd.CommandText = SQL;
				
				MySqlParameter prmBranchTransferItemStatus = new MySqlParameter("@BranchTransferItemStatus",MySqlDbType.Int16);			
				prmBranchTransferItemStatus.Value = BranchTransferItemStatus.Posted.ToString("d");
				cmd.Parameters.Add(prmBranchTransferItemStatus);

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

		public void Cancel(long BranchTransferID)
		{
			try 
			{
				string SQL=	"UPDATE tblBranchTransferItems SET " + 
								"BranchTransferItemStatus			=	@BranchTransferItemStatus " +
							"WHERE BranchTransferID = @BranchTransferID;";
				  
				MySqlCommand cmd = new MySqlCommand();
				cmd.CommandType = System.Data.CommandType.Text;
				cmd.CommandText = SQL;
				
				MySqlParameter prmBranchTransferItemStatus = new MySqlParameter("@BranchTransferItemStatus",MySqlDbType.Int16);			
				prmBranchTransferItemStatus.Value = BranchTransferItemStatus.Cancelled.ToString("d");
				cmd.Parameters.Add(prmBranchTransferItemStatus);

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

		#region Delete

		public bool Delete(string IDs)
		{
			try 
			{
				string SQL=	"DELETE FROM tblBranchTransferItems WHERE BranchTransferItemID IN (" + IDs + ");";
	 			
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
								"BranchTransferItemID, " +
								"BranchTransferID, " +
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
								"BranchTransferItemStatus, " +
								"IsVatable, " +
								"Remarks, " +
                                "SellingPrice, " +
                                "SellingVAT, " +
                                "SellingEVAT, " +
                                "SellingLocalTax, " +
                                "OldSellingPrice " +
							"FROM tblBranchTransferItems ";
            return stSQL;
        }

		#region Details

		public BranchTransferItemDetails Details(long BranchTransferItemID)
		{
			try
			{
				string SQL=	SQLSelect() + "WHERE BranchTransferItemID = @BranchTransferItemID;";
	 			
				MySqlCommand cmd = new MySqlCommand();
				cmd.CommandType = System.Data.CommandType.Text;
				cmd.CommandText = SQL;

				MySqlParameter prmBranchTransferItemID = new MySqlParameter("@BranchTransferItemID",MySqlDbType.Int64);			
				prmBranchTransferItemID.Value = BranchTransferItemID;
				cmd.Parameters.Add(prmBranchTransferItemID);

				MySqlDataReader myReader = base.ExecuteReader(cmd, System.Data.CommandBehavior.SingleResult);
				
				BranchTransferItemDetails Details = new BranchTransferItemDetails();

				while (myReader.Read()) 
				{
					Details.BranchTransferItemID = BranchTransferItemID;
					Details.BranchTransferID = myReader.GetInt64("BranchTransferID");
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
                    Details.DiscountType = (DiscountTypes) Enum.Parse(typeof(DiscountTypes), myReader.GetString("DiscountType"));
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
                    Details.BranchTransferItemStatus = (BranchTransferItemStatus)Enum.Parse(typeof(BranchTransferItemStatus), myReader.GetString("BranchTransferItemStatus"));
					if (myReader["IsVatable"].ToString() == "1")
						Details.IsVatable = true;
					Details.Remarks = "" + myReader["Remarks"].ToString();
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
				throw base.ThrowException(ex);
			}	
		}

		#endregion

		#region Streams

		public System.Data.DataTable ListAsDataTable(string SortField, SortOption SortOrder)
		{
            if (SortField == string.Empty || SortField == null) SortField = "BranchTransferItemID";

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
        public System.Data.DataTable ListAsDataTable(long BranchTransferID, string SortField = "BranchTransferItemID", SortOption SortOrder = SortOption.Desscending)
        {
            if (SortField == string.Empty || SortField == null) SortField = "BranchTransferItemID";

            string SQL = SQLSelect() + "WHERE BranchTransferID = @BranchTransferID ORDER BY " + SortField;

            if (SortOrder == SortOption.Ascending)
                SQL += " ASC";
            else
                SQL += " DESC";

            MySqlCommand cmd = new MySqlCommand();
            cmd.CommandType = System.Data.CommandType.Text;
            cmd.CommandText = SQL;

            cmd.Parameters.AddWithValue("@BranchTransferID", BranchTransferID);

            string strDataTableName = "tbl" + this.GetType().FullName.Split(new Char[] { '.' })[this.GetType().FullName.Split(new Char[] { '.' }).Length - 1]; System.Data.DataTable dt = new System.Data.DataTable(strDataTableName);
            base.MySqlDataAdapterFill(cmd, dt);

            return dt;

        }
        
		public MySqlDataReader List(string SortField, SortOption SortOrder)
		{
			try
			{
                if (SortField == string.Empty || SortField == null) SortField = "BranchTransferItemID";

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
		public MySqlDataReader List(long BranchTransferID, string SortField = "BranchTransferItemID", SortOption SortOrder = SortOption.Ascending)
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
		public MySqlDataReader List(BranchTransferItemStatus BranchTransferItemstatus, string SortField, SortOption SortOrder)
		{
			try
			{
                if (SortField == string.Empty || SortField == null) SortField = "BranchTransferItemID";

				string SQL = SQLSelect() + "WHERE BranchTransferItemStatus = @BranchTransferItemStatus " +
							"ORDER BY " + SortField;

				if (SortOrder == SortOption.Ascending)
					SQL += " ASC";
				else
					SQL += " DESC";

				MySqlCommand cmd = new MySqlCommand();
				cmd.CommandType = System.Data.CommandType.Text;
				cmd.CommandText = SQL;
				
				cmd.Parameters.AddWithValue("@BranchTransferItemStatus", BranchTransferItemstatus.ToString("d"));

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
                if (SortField == string.Empty || SortField == null) SortField = "BranchTransferItemID";

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
				
				cmd.Parameters.AddWithValue("@SearchKey", "%" + SearchKey + "%");

				MySqlDataReader myReader = base.ExecuteReader(cmd);
				
				return myReader;			
			}
			catch (Exception ex)
			{
				throw base.ThrowException(ex);
			}	
		}		
		public MySqlDataReader Search(BranchTransferItemStatus BranchTransferItemstatus, string SearchKey, string SortField, SortOption SortOrder)
		{
			try
			{
                if (SortField == string.Empty || SortField == null) SortField = "BranchTransferItemID";

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
				
                cmd.Parameters.AddWithValue("@BranchTransferItemStatus", BranchTransferItemstatus.ToString("d"));
                cmd.Parameters.AddWithValue("@SearchKey", "%" + SearchKey + "%");

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


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

	#region ClosingItemDetails

	public struct ClosingItemDetails
	{
		public long ClosingItemID;
		public long ClosingID;
		public long ProductID;
		public string ProductCode;
		public string BarCode;
		public string Description;
		public int ProductUnitID;
		public string ProductUnitCode;
		public decimal Quantity;
		public decimal UnitCost;
		public decimal Discount;
		public bool InPercent;
		public decimal TotalDiscount;
		public decimal Amount;
		public decimal VAT;
		public decimal VatableAmount;
		public decimal EVAT;
		public decimal EVatableAmount;
		public decimal LocalTax;
		public long VariationMatrixID;
		public string MatrixDescription;
		public string ProductGroup;
		public string ProductSubGroup;
		public ClosingItemStatus ClosingItemStatus;
		public bool IsVatable;
		public string Remarks;
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
	public class ClosingItem
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

		public ClosingItem()
		{
			
		}

		public ClosingItem(MySqlConnection Connection, MySqlTransaction Transaction)
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

		public long Insert(ClosingItemDetails Details)
		{
			try 
			{
				string SQL = "INSERT INTO tblClosingItems (" +
								"ClosingID, " +
								"ProductID, " +
								"ProductCode, " +
								"BarCode, " +
								"Description, " +
								"ProductUnitID, " +
								"ProductUnitCode, " +
								"Quantity, " +
								"UnitCost, " +
								"Discount, " +
								"InPercent, " +
								"TotalDiscount, " +
								"Amount, " +
								"VAT, " +
								"VatableAmount, " +
								"EVAT, " +
								"EVatableAmount, " +
								"LocalTax, " +
								"VariationMatrixID, " +
								"MatrixDescription, " +
								"ProductGroup, " +
								"ProductSubGroup, " +
								"ClosingItemStatus, " +
								"IsVatable, " +
								"Remarks" +
							") VALUES (" +
								"@ClosingID, " +
								"@ProductID, " +
								"@ProductCode, " +
								"@BarCode, " +
								"@Description, " +
								"@ProductUnitID, " +
								"@ProductUnitCode, " +
								"@Quantity, " +
								"@UnitCost, " +
								"@Discount, " +
								"@InPercent, " +
								"@TotalDiscount, " +
								"@Amount, " +
								"@VAT, " +
								"@VatableAmount, " +
								"@EVAT, " +
								"@EVatableAmount, " +
								"@LocalTax, " +
								"@VariationMatrixID, " +
								"@MatrixDescription, " +
								"@ProductGroup, " +
								"@ProductSubGroup, " +
								"@ClosingItemStatus, " +
								"@IsVatable, " +
								"@Remarks" +
							");";
				  
				MySqlConnection cn = GetConnection();
	 			
				MySqlCommand cmd = new MySqlCommand();
				cmd.Connection = cn;
				cmd.Transaction = mTransaction;
				cmd.CommandType = System.Data.CommandType.Text;
				cmd.CommandText = SQL;
				
				MySqlParameter prmClosingID = new MySqlParameter("@ClosingID",System.Data.DbType.Int64);
				prmClosingID.Value = Details.ClosingID;
				cmd.Parameters.Add(prmClosingID);

				MySqlParameter prmProductID = new MySqlParameter("@ProductID",System.Data.DbType.Int64);
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

				MySqlParameter prmQuantity = new MySqlParameter("@Quantity",System.Data.DbType.Decimal);			
				prmQuantity.Value = Details.Quantity;
				cmd.Parameters.Add(prmQuantity);

				MySqlParameter prmUnitCost = new MySqlParameter("@UnitCost",System.Data.DbType.Decimal);			
				prmUnitCost.Value = Details.UnitCost;
				cmd.Parameters.Add(prmUnitCost);
					
				MySqlParameter prmDiscount = new MySqlParameter("@Discount",System.Data.DbType.Decimal);			
				prmDiscount.Value = Details.Discount;
				cmd.Parameters.Add(prmDiscount);

				MySqlParameter prmInPercent = new MySqlParameter("@InPercent",MySqlDbType.Int16);			
				prmInPercent.Value = Convert.ToInt16(Details.InPercent);
				cmd.Parameters.Add(prmInPercent);
				
				MySqlParameter prmTotalDiscount = new MySqlParameter("@TotalDiscount",System.Data.DbType.Decimal);			
				prmTotalDiscount.Value = Details.TotalDiscount;
				cmd.Parameters.Add(prmTotalDiscount);

				MySqlParameter prmAmount = new MySqlParameter("@Amount",System.Data.DbType.Decimal);			
				prmAmount.Value = Details.Amount;
				cmd.Parameters.Add(prmAmount);
								 
				MySqlParameter prmVAT = new MySqlParameter("@VAT",System.Data.DbType.Decimal);			
				prmVAT.Value = Details.VAT;
				cmd.Parameters.Add(prmVAT);			 

				MySqlParameter prmVatableAmount = new MySqlParameter("@VatableAmount",System.Data.DbType.Decimal);			
				prmVatableAmount.Value = Details.VatableAmount;
				cmd.Parameters.Add(prmVatableAmount);

				MySqlParameter prmEVAT = new MySqlParameter("@EVAT",System.Data.DbType.Decimal);			
				prmEVAT.Value = Details.EVAT;
				cmd.Parameters.Add(prmEVAT);			 

				MySqlParameter prmEVatableAmount = new MySqlParameter("@EVatableAmount",System.Data.DbType.Decimal);			
				prmEVatableAmount.Value = Details.EVatableAmount;
				cmd.Parameters.Add(prmEVatableAmount);

				MySqlParameter prmLocalTax = new MySqlParameter("@LocalTax",System.Data.DbType.Decimal);			
				prmLocalTax.Value = Details.LocalTax;
				cmd.Parameters.Add(prmLocalTax);

				MySqlParameter prmVariationMatrixID = new MySqlParameter("@VariationMatrixID",System.Data.DbType.Int64);			
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

				MySqlParameter prmClosingItemStatus = new MySqlParameter("@ClosingItemStatus",MySqlDbType.Int16);			
				prmClosingItemStatus.Value = Details.ClosingItemStatus.ToString("d");
				cmd.Parameters.Add(prmClosingItemStatus);

				MySqlParameter prmIsVatable = new MySqlParameter("@IsVatable",MySqlDbType.Int16);			
				prmIsVatable.Value = Details.IsVatable;
				cmd.Parameters.Add(prmIsVatable);

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

				Closing clsClosing = new Closing(Connection, Transaction);
				clsClosing.SynchronizeAmount(Details.ClosingID);

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

		public void Update(ClosingItemDetails Details)
		{
			try 
			{
				string SQL=	"UPDATE tblClosingItems SET " + 
								"ClosingID					=	@ClosingID, " +
								"ProductID				=	@ProductID, " +
								"ProductCode			=	@ProductCode, " +
								"BarCode				=	@BarCode, " +
								"Description			=	@Description, " +
								"ProductUnitID			=	@ProductUnitID, " +
								"ProductUnitCode		=	@ProductUnitCode, " +
								"Quantity				=	@Quantity, " +
								"UnitCost				=	@UnitCost, " +
								"Discount				=	@Discount, " +
								"InPercent				=	@InPercent, " +
								"TotalDiscount			=	@TotalDiscount, " +
								"Amount					=	@Amount, " +
								"VAT					=	@VAT, " +
								"VatableAmount			=	@VatableAmount, " +
								"EVAT					=	@EVAT, " +
								"EVatableAmount			=	@EVatableAmount, " +
								"LocalTax				=	@LocalTax, " +
								"VariationMatrixID		=	@VariationMatrixID, " +
								"MatrixDescription		=	@MatrixDescription, " +
								"ProductGroup			=	@ProductGroup, " +
								"ProductSubGroup		=	@ProductSubGroup, " +
								"ClosingItemStatus			=	@ClosingItemStatus, " +
								"IsVatable				=	@IsVatable, " +
								"Remarks				=	@Remarks " +
							"WHERE ClosingItemID = @ClosingItemID;";
				  
				MySqlConnection cn = GetConnection();
	 			
				MySqlCommand cmd = new MySqlCommand();
				cmd.Connection = cn;
				cmd.Transaction = mTransaction;
				cmd.CommandType = System.Data.CommandType.Text;
				cmd.CommandText = SQL;
				
				MySqlParameter prmClosingID = new MySqlParameter("@ClosingID",System.Data.DbType.Int64);
				prmClosingID.Value = Details.ClosingID;
				cmd.Parameters.Add(prmClosingID);

				MySqlParameter prmProductID = new MySqlParameter("@ProductID",System.Data.DbType.Int64);
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

				MySqlParameter prmQuantity = new MySqlParameter("@Quantity",System.Data.DbType.Decimal);			
				prmQuantity.Value = Details.Quantity;
				cmd.Parameters.Add(prmQuantity);

				MySqlParameter prmUnitCost = new MySqlParameter("@UnitCost",System.Data.DbType.Decimal);			
				prmUnitCost.Value = Details.UnitCost;
				cmd.Parameters.Add(prmUnitCost);
					
				MySqlParameter prmDiscount = new MySqlParameter("@Discount",System.Data.DbType.Decimal);			
				prmDiscount.Value = Details.Discount;
				cmd.Parameters.Add(prmDiscount);

				MySqlParameter prmInPercent = new MySqlParameter("@InPercent",MySqlDbType.Int16);			
				prmInPercent.Value = Convert.ToInt16(Details.InPercent);
				cmd.Parameters.Add(prmInPercent);
				
				MySqlParameter prmTotalDiscount = new MySqlParameter("@TotalDiscount",System.Data.DbType.Decimal);			
				prmTotalDiscount.Value = Details.TotalDiscount;
				cmd.Parameters.Add(prmTotalDiscount);

				MySqlParameter prmAmount = new MySqlParameter("@Amount",System.Data.DbType.Decimal);			
				prmAmount.Value = Details.Amount;
				cmd.Parameters.Add(prmAmount);
								 
				MySqlParameter prmVAT = new MySqlParameter("@VAT",System.Data.DbType.Decimal);			
				prmVAT.Value = Details.VAT;
				cmd.Parameters.Add(prmVAT);			 

				MySqlParameter prmVatableAmount = new MySqlParameter("@VatableAmount",System.Data.DbType.Decimal);			
				prmVatableAmount.Value = Details.VatableAmount;
				cmd.Parameters.Add(prmVatableAmount);

				MySqlParameter prmEVAT = new MySqlParameter("@EVAT",System.Data.DbType.Decimal);			
				prmEVAT.Value = Details.EVAT;
				cmd.Parameters.Add(prmEVAT);			 

				MySqlParameter prmEVatableAmount = new MySqlParameter("@EVatableAmount",System.Data.DbType.Decimal);			
				prmEVatableAmount.Value = Details.EVatableAmount;
				cmd.Parameters.Add(prmEVatableAmount);

				MySqlParameter prmLocalTax = new MySqlParameter("@LocalTax",System.Data.DbType.Decimal);			
				prmLocalTax.Value = Details.LocalTax;
				cmd.Parameters.Add(prmLocalTax);

				MySqlParameter prmVariationMatrixID = new MySqlParameter("@VariationMatrixID",System.Data.DbType.Int64);			
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

				MySqlParameter prmClosingItemStatus = new MySqlParameter("@ClosingItemStatus",MySqlDbType.Int16);			
				prmClosingItemStatus.Value = Details.ClosingItemStatus.ToString("d");
				cmd.Parameters.Add(prmClosingItemStatus);

				MySqlParameter prmIsVatable = new MySqlParameter("@IsVatable",MySqlDbType.Int16);			
				prmIsVatable.Value = Details.IsVatable;
				cmd.Parameters.Add(prmIsVatable);

				MySqlParameter prmRemarks = new MySqlParameter("@Remarks",MySqlDbType.String);			
				prmRemarks.Value = Details.Remarks;
				cmd.Parameters.Add(prmRemarks);	

				MySqlParameter prmClosingItemID = new MySqlParameter("@ClosingItemID",System.Data.DbType.Int64);			
				prmClosingItemID.Value = Details.ClosingItemID;
				cmd.Parameters.Add(prmClosingItemID);

				cmd.ExecuteNonQuery();

				Closing clsClosing = new Closing(Connection, Transaction);
				clsClosing.SynchronizeAmount(Details.ClosingID);
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

		public void Post(long ClosingID)
		{
			try 
			{
				string SQL=	"UPDATE tblClosingItems SET " + 
								"ClosingItemStatus			=	@ClosingItemStatus " +
							"WHERE ClosingID = @ClosingID;";
				  
				MySqlConnection cn = GetConnection();
	 			
				MySqlCommand cmd = new MySqlCommand();
				cmd.Connection = cn;
				cmd.Transaction = mTransaction;
				cmd.CommandType = System.Data.CommandType.Text;
				cmd.CommandText = SQL;
				
				MySqlParameter prmClosingItemStatus = new MySqlParameter("@ClosingItemStatus",MySqlDbType.Int16);			
				prmClosingItemStatus.Value = ClosingItemStatus.Posted.ToString("d");
				cmd.Parameters.Add(prmClosingItemStatus);

				MySqlParameter prmClosingID = new MySqlParameter("@ClosingID",System.Data.DbType.Int64);
				prmClosingID.Value = ClosingID;
				cmd.Parameters.Add(prmClosingID);

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

		public void Cancel(long ClosingID)
		{
			try 
			{
				string SQL=	"UPDATE tblClosingItems SET " + 
								"ClosingItemStatus			=	@ClosingItemStatus " +
							"WHERE ClosingID = @ClosingID;";
				  
				MySqlConnection cn = GetConnection();
	 			
				MySqlCommand cmd = new MySqlCommand();
				cmd.Connection = cn;
				cmd.Transaction = mTransaction;
				cmd.CommandType = System.Data.CommandType.Text;
				cmd.CommandText = SQL;
				
				MySqlParameter prmClosingItemStatus = new MySqlParameter("@ClosingItemStatus",MySqlDbType.Int16);			
				prmClosingItemStatus.Value = ClosingItemStatus.Cancelled.ToString("d");
				cmd.Parameters.Add(prmClosingItemStatus);

				MySqlParameter prmClosingID = new MySqlParameter("@ClosingID",System.Data.DbType.Int64);
				prmClosingID.Value = ClosingID;
				cmd.Parameters.Add(prmClosingID);

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
				string SQL=	"DELETE FROM tblClosingItems WHERE ClosingItemID IN (" + IDs + ");";
				  
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

		#region Details

		public ClosingItemDetails Details(long ClosingItemID)
		{
			try
			{
				string SQL=	"SELECT " +
								"ClosingItemID, " +
								"ClosingID, " +
								"ProductID, " +
								"ProductCode, " +
								"BarCode, " +
								"Description, " +
								"ProductUnitID, " +
								"ProductUnitCode, " +
								"Quantity, " +
								"UnitCost, " +
								"Discount, " +
								"InPercent, " +
								"TotalDiscount, " +
								"Amount, " +
								"VAT, " +
								"VatableAmount, " +
								"EVAT, " +
								"EVatableAmount, " +
								"LocalTax, " +
								"VariationMatrixID, " +
								"MatrixDescription, " +
								"ProductGroup, " +
								"ProductSubGroup, " +
								"ClosingItemStatus, " +
								"IsVatable, " +
								"Remarks " +
							"FROM tblClosingItems " +
								"WHERE ClosingItemID = @ClosingItemID;";
				  
				MySqlConnection cn = GetConnection();
	 			
				MySqlCommand cmd = new MySqlCommand();
				cmd.Connection = cn;
				cmd.Transaction = mTransaction;
				cmd.CommandType = System.Data.CommandType.Text;
				cmd.CommandText = SQL;

				MySqlParameter prmClosingItemID = new MySqlParameter("@ClosingItemID",System.Data.DbType.Int64);
				prmClosingItemID.Value = ClosingItemID;
				cmd.Parameters.Add(prmClosingItemID);

				MySqlDataReader myReader = (MySqlDataReader) cmd.ExecuteReader(System.Data.CommandBehavior.SingleResult);
				
				ClosingItemDetails Details = new ClosingItemDetails();

				while (myReader.Read()) 
				{
					Details.ClosingItemID = ClosingItemID;
					Details.ClosingID = myReader.GetInt64("ClosingID");
					Details.ProductID = myReader.GetInt64("ProductID");
					Details.ProductCode = "" + myReader["ProductCode"].ToString();
					Details.BarCode = "" + myReader["BarCode"].ToString();
					Details.Description = "" + myReader["Description"].ToString();
					Details.ProductUnitID = myReader.GetInt16("ProductUnitID");
					Details.ProductUnitCode = "" + myReader["ProductUnitCode"].ToString();
					Details.Quantity = myReader.GetDecimal("Quantity");
					Details.UnitCost = myReader.GetDecimal("UnitCost");
					Details.Discount = myReader.GetDecimal("Discount");
					if (myReader["InPercent"].ToString() == "1")
						Details.InPercent = true;
					Details.TotalDiscount = myReader.GetDecimal("TotalDiscount");
					Details.Amount = myReader.GetDecimal("Amount");
					Details.VAT = myReader.GetDecimal("VAT");
					Details.VatableAmount = myReader.GetDecimal("VatableAmount");
					Details.EVAT = myReader.GetDecimal("EVAT");
					Details.EVatableAmount = myReader.GetDecimal("EVatableAmount");
					Details.LocalTax = myReader.GetDecimal("LocalTax");
					Details.VariationMatrixID = myReader.GetInt64("VariationMatrixID");
					Details.MatrixDescription = "" + myReader["MatrixDescription"].ToString();
					Details.ProductGroup = "" + myReader["ProductGroup"].ToString();
					Details.ProductSubGroup = "" + myReader["ProductSubGroup"].ToString();
                    Details.ClosingItemStatus = (ClosingItemStatus)Enum.Parse(typeof(ClosingItemStatus), myReader.GetString("ClosingItemStatus"));
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
			MySqlDataReader myReader = List(SortField,SortOption.Ascending);
			
			System.Data.DataTable dt = new System.Data.DataTable("tblClosingItems");

			dt.Columns.Add("ClosingItemID");
			dt.Columns.Add("ClosingID");
			dt.Columns.Add("ProductID");
			dt.Columns.Add("ProductCode");
			dt.Columns.Add("BarCode");
			dt.Columns.Add("Description");
			dt.Columns.Add("ProductUnitID");
			dt.Columns.Add("ProductUnitCode");
			dt.Columns.Add("Quantity");
			dt.Columns.Add("UnitCost");
			dt.Columns.Add("Discount");
			dt.Columns.Add("InPercent");
			dt.Columns.Add("TotalDiscount");
			dt.Columns.Add("Amount");
			dt.Columns.Add("VAT");
			dt.Columns.Add("VatableAmount");
			dt.Columns.Add("EVAT");
			dt.Columns.Add("EVatableAmount");
			dt.Columns.Add("LocalTax");
			dt.Columns.Add("VariationMatrixID");
			dt.Columns.Add("MatrixDescription");
			dt.Columns.Add("ProductGroup");
			dt.Columns.Add("ProductSubGroup");
			dt.Columns.Add("TransactionItemStatus");
			dt.Columns.Add("IsVatable");
			dt.Columns.Add("Remarks");

			while (myReader.Read())
			{
				System.Data.DataRow dr = dt.NewRow();

				dr["ClosingItemID"] = myReader.GetInt64("ClosingItemID");
				dr["ClosingID"] = myReader.GetInt64("ClosingID");
				dr["ProductID"] = myReader.GetInt64("ProductID");
				dr["ProductCode"] = "" + myReader["ProductCode"].ToString();
				dr["BarCode"] = "" + myReader["BarCode"].ToString();
				dr["Description"] = "" + myReader["Description"].ToString();
				dr["ProductUnitID"] = myReader.GetInt16("ProductUnitID");
				dr["ProductUnitCode"] = "" + myReader["ProductUnitCode"].ToString();
				dr["Quantity"] = myReader.GetDecimal("Quantity");
				dr["UnitCost"] = myReader.GetDecimal("UnitCost");
				dr["Discount"] = myReader.GetDecimal("Discount");
				dr["InPercent"] = myReader.GetDecimal("InPercent");
				dr["TotalDiscount"] = myReader.GetDecimal("TotalDiscount");
				dr["Amount"] = myReader.GetDecimal("Amount");
				dr["VAT"] = myReader.GetDecimal("VAT");
				dr["VatableAmount"] = myReader.GetDecimal("VatableAmount");
				dr["EVAT"] = myReader.GetDecimal("EVAT");
				dr["EVatableAmount"] = myReader.GetDecimal("EVatableAmount");
				dr["LocalTax"] = myReader.GetDecimal("LocalTax");
				dr["VariationMatrixID"] = myReader.GetInt64("VariationMatrixID");
				dr["MatrixDescription"] = "" + myReader["MatrixDescription"].ToString();
				dr["ProductGroup"] = "" + myReader["ProductGroup"].ToString();
				dr["ProductSubGroup"] = "" + myReader["ProductSubGroup"].ToString();
                dr["ClosingItemStatus"] = (ClosingItemStatus)Enum.Parse(typeof(ClosingItemStatus), myReader.GetString("ClosingItemStatus"));
				dr["IsVatable"] = myReader.GetInt16("IsVatable");
				dr["Remarks"] = "" + myReader["Remarks"].ToString();
					
				dt.Rows.Add(dr);
			}
			
			myReader.Close();

			return dt;
		}
		
		public MySqlDataReader List(string SortField, SortOption SortOrder)
		{
			try
			{
				string SQL = "SELECT " +
								"ClosingItemID, " +
								"ClosingID, " +
								"ProductID, " +
								"ProductCode, " +
								"BarCode, " +
								"Description, " +
								"ProductUnitID, " +
								"ProductUnitCode, " +
								"Quantity, " +
								"UnitCost, " +
								"Discount, " +
								"InPercent, " +
								"TotalDiscount, " +
								"Amount, " +
								"VAT, " +
								"VatableAmount, " +
								"EVAT, " +
								"EVatableAmount, " +
								"LocalTax, " +
								"VariationMatrixID, " +
								"MatrixDescription, " +
								"ProductGroup, " +
								"ProductSubGroup, " +
								"ClosingItemStatus, " +
								"IsVatable, " +
								"Remarks " +
							"FROM tblClosingItems " +
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
		
		public MySqlDataReader List(long ClosingID, string SortField, SortOption SortOrder)
		{
			try
			{
				string SQL = "SELECT " +
								"ClosingItemID, " +
								"ClosingID, " +
								"ProductID, " +
								"ProductCode, " +
								"BarCode, " +
								"Description, " +
								"ProductUnitID, " +
								"ProductUnitCode, " +
								"Quantity, " +
								"UnitCost, " +
								"Discount, " +
								"InPercent, " +
								"TotalDiscount, " +
								"Amount, " +
								"VAT, " +
								"VatableAmount, " +
								"EVAT, " +
								"EVatableAmount, " +
								"LocalTax, " +
								"VariationMatrixID, " +
								"MatrixDescription, " +
								"ProductGroup, " +
								"ProductSubGroup, " +
								"ClosingItemStatus, " +
								"IsVatable, " +
								"Remarks " +
							"FROM tblClosingItems " +
							"WHERE ClosingID = @ClosingID " +
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
				
				MySqlParameter prmClosingID = new MySqlParameter("@ClosingID",System.Data.DbType.Int64);			
				prmClosingID.Value = ClosingID;
				cmd.Parameters.Add(prmClosingID);

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
		
		public MySqlDataReader List(ClosingItemStatus ClosingItemstatus, string SortField, SortOption SortOrder)
		{
			try
			{
				string SQL = "SELECT " +
								"ClosingItemID, " +
								"ClosingID, " +
								"ProductID, " +
								"ProductCode, " +
								"BarCode, " +
								"Description, " +
								"ProductUnitID, " +
								"ProductUnitCode, " +
								"Quantity, " +
								"UnitCost, " +
								"Discount, " +
								"InPercent, " +
								"TotalDiscount, " +
								"Amount, " +
								"VAT, " +
								"VatableAmount, " +
								"EVAT, " +
								"EVatableAmount, " +
								"LocalTax, " +
								"VariationMatrixID, " +
								"MatrixDescription, " +
								"ProductGroup, " +
								"ProductSubGroup, " +
								"ClosingItemStatus, " +
								"IsVatable, " +
								"Remarks " +
							"FROM tblClosingItems " +
							"WHERE ClosingItemStatus = @ClosingItemStatus " +
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
				
				MySqlParameter prmClosingItemStatus = new MySqlParameter("@ClosingItemStatus",MySqlDbType.Int16);			
				prmClosingItemStatus.Value = ClosingItemstatus.ToString("d");
				cmd.Parameters.Add(prmClosingItemStatus);

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
		
		public MySqlDataReader Search(string SearchKey, string SortField, SortOption SortOrder)
		{
			try
			{
				string SQL = "SELECT " +
								"ClosingItemID, " +
								"ClosingID, " +
								"ProductID, " +
								"ProductCode, " +
								"BarCode, " +
								"Description, " +
								"ProductUnitID, " +
								"ProductUnitCode, " +
								"Quantity, " +
								"UnitCost, " +
								"Discount, " +
								"InPercent, " +
								"TotalDiscount, " +
								"Amount, " +
								"VAT, " +
								"VatableAmount, " +
								"EVAT, " +
								"EVatableAmount, " +
								"LocalTax, " +
								"VariationMatrixID, " +
								"MatrixDescription, " +
								"ProductGroup, " +
								"ProductSubGroup, " +
								"ClosingItemStatus, " +
								"IsVatable, " +
								"Remarks " +
							"FROM tblClosingItems " +
								"WHERE (ProductCode LIKE @SearchKey or BarCode LIKE @SearchKey or Description LIKE @SearchKey " +
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

		public MySqlDataReader Search(ClosingItemStatus ClosingItemstatus, string SearchKey, string SortField, SortOption SortOrder)
		{
			try
			{
				string SQL = "SELECT " +
								"ClosingItemID, " +
								"ClosingID, " +
								"ProductID, " +
								"ProductCode, " +
								"BarCode, " +
								"Description, " +
								"ProductUnitID, " +
								"ProductUnitCode, " +
								"Quantity, " +
								"UnitCost, " +
								"Discount, " +
								"InPercent, " +
								"TotalDiscount, " +
								"Amount, " +
								"VAT, " +
								"VatableAmount, " +
								"EVAT, " +
								"EVatableAmount, " +
								"LocalTax, " +
								"VariationMatrixID, " +
								"MatrixDescription, " +
								"ProductGroup, " +
								"ProductSubGroup, " +
								"ClosingItemStatus, " +
								"IsVatable, " +
								"Remarks " +
							"FROM tblClosingItems " +
								"WHERE (ProductCode LIKE @SearchKey or BarCode LIKE @SearchKey or Description LIKE @SearchKey " +
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
				
				MySqlParameter prmClosingItemStatus = new MySqlParameter("@ClosingItemStatus",MySqlDbType.Int16);			
				prmClosingItemStatus.Value = ClosingItemstatus.ToString("d");
				cmd.Parameters.Add(prmClosingItemStatus);

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

		
		#endregion
	}
}


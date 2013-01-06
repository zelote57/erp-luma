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

	#region InventoryDetails

	public struct InventoryDetails
	{
        public int BranchID;
		public long InventoryID;
		public DateTime PostingDateFrom;
		public DateTime PostingDateTo;
		public DateTime PostingDate;
		public string ReferenceNo;
		public long ContactID;
		public string ContactCode;
		public long ProductID;
		public string ProductCode;
		public long VariationMatrixID;
		public string MatrixDescription;
		public decimal PurchaseQuantity;
		public decimal PurchaseCost;
		public decimal PurchaseVAT;
		public decimal PInvoiceQuantity;
		public decimal PInvoiceCost;
		public decimal PInvoiceVAT;
		public decimal PReturnQuantity;
		public decimal PReturnCost;
		public decimal PReturnVAT;
		public decimal PDebitQuantity;
		public decimal PDebitCost;
		public decimal PDebitVAT;
		public decimal TransferInQuantity;
		public decimal TransferInCost;
		public decimal TransferInVAT;
		public decimal TransferOutQuantity;
		public decimal TransferOutCost;
		public decimal TransferOutVAT;
		public decimal SoldQuantity;
		public decimal SoldCost;
		public decimal SoldVAT;
		public decimal SReturnQuantity;
		public decimal SReturnCost;
		public decimal SReturnVAT;
        public decimal SCreditQuantity;
        public decimal SCreditCost;
        public decimal SCreditVAT;
		public decimal InvAdjustmentQuantity;
		public decimal InvAdjustmentCost;
		public decimal InvAdjustmentVAT;
		public decimal ClosingQuantity;
		public decimal ClosingCost;
		public decimal ClosingVAT;
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
	public class Inventory
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

		public Inventory()
		{
			
		}

		public Inventory(MySqlConnection Connection, MySqlTransaction Transaction)
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

		public long Insert(InventoryDetails Details)
		{
			try 
			{
				string SQL = "INSERT INTO tblInventory (" +
                                "BranchID, " +
								"PostingDateFrom, " +
								"PostingDateTo, " +
								"PostingDate, " +
								"ReferenceNo, " +
								"ContactID, " +
								"ContactCode, " +
								"ProductID, " +
								"ProductCode, " +
								"VariationMatrixID, " +
								"MatrixDescription, " +
								"PurchaseQuantity, " +
								"PurchaseCost, " +
								"PurchaseVAT, " +
								"PInvoiceQuantity, " +
								"PInvoiceCost, " +
								"PInvoiceVAT, " +
								"PReturnQuantity, " +
								"PReturnCost, " +
								"PReturnVAT, " +
								"PDebitQuantity, " +
								"PDebitCost, " +
								"PDebitVAT, " +
								"TransferInQuantity, " +
								"TransferInCost, " +
								"TransferInVAT, " +
								"TransferOutQuantity, " +
								"TransferOutCost," +
								"TransferOutVAT," +
								"SoldQuantity, " +
								"SoldCost, " +
								"SoldVAT, " +
								"SReturnQuantity, " +
								"SReturnCost, " +
								"SReturnVAT, " +
                                "SCreditQuantity, " +
                                "SCreditCost, " +
                                "SCreditVAT, " +
								"InvAdjustmentQuantity, " +
								"InvAdjustmentCost, " +
								"InvAdjustmentVAT, " +
								"ClosingQuantity, " +
								"ClosingCost, " +
								"ClosingVAT" +
							") VALUES (" +
                                "@BranchID, " +
								"@PostingDateFrom, " +
								"@PostingDateTo, " +
								"@PostingDate, " +
								"@ReferenceNo, " +
								"@ContactID, " +
								"@ContactCode, " +
								"@ProductID, " +
								"@ProductCode, " +
								"@VariationMatrixID, " +
								"@MatrixDescription, " +
								"@PurchaseQuantity, " +
								"@PurchaseCost, " +
								"@PurchaseVAT, " +
								"@PInvoiceQuantity, " +
								"@PInvoiceCost, " +
								"@PInvoiceVAT, " +
								"@PReturnQuantity, " +
								"@PReturnCost, " +
								"@PReturnVAT, " +
								"@PDebitQuantity, " +
								"@PDebitCost, " +
								"@PDebitVAT, " +
								"@TransferInQuantity, " +
								"@TransferInCost, " +
								"@TransferInVAT, " +
								"@TransferOutQuantity, " +
								"@TransferOutCost," +
								"@TransferOutVAT," +
								"@SoldQuantity, " +
								"@SoldCost, " +
								"@SoldVAT, " +
								"@SReturnQuantity, " +
								"@SReturnCost, " +
								"@SReturnVAT, " +
                                "@SCreditQuantity, " +
                                "@SCreditCost, " +
                                "@SCreditVAT, " +
								"@InvAdjustmentQuantity, " +
								"@InvAdjustmentCost, " +
								"@InvAdjustmentVAT, " +
								"@ClosingQuantity, " +
								"@ClosingCost, " +
								"@ClosingVAT" +
							");";
				  
				MySqlConnection cn = GetConnection();
	 			
				MySqlCommand cmd = new MySqlCommand();
				cmd.Connection = cn;
				cmd.Transaction = mTransaction;
				cmd.CommandType = System.Data.CommandType.Text;
				cmd.CommandText = SQL;

                cmd.Parameters.AddWithValue("@BranchID", Details.BranchID);
				cmd.Parameters.AddWithValue("@PostingDateFrom", Details.PostingDateFrom.ToString("yyyy-MM-dd HH:mm:ss"));
				cmd.Parameters.AddWithValue("@PostingDateTo", Details.PostingDateTo.ToString("yyyy-MM-dd HH:mm:ss"));
				cmd.Parameters.AddWithValue("@PostingDate", Details.PostingDate.ToString("yyyy-MM-dd HH:mm:ss"));
				cmd.Parameters.AddWithValue("@ReferenceNo", Details.ReferenceNo);
				cmd.Parameters.AddWithValue("@ContactID", Details.ContactID);
				cmd.Parameters.AddWithValue("@ContactCode", Details.ContactCode);
				cmd.Parameters.AddWithValue("@ProductID", Details.ProductID);
				cmd.Parameters.AddWithValue("@ProductCode", Details.ProductCode);
				cmd.Parameters.AddWithValue("@VariationMatrixID", Details.VariationMatrixID);
				cmd.Parameters.AddWithValue("@MatrixDescription", Details.MatrixDescription);
				cmd.Parameters.AddWithValue("@PurchaseQuantity", Details.PurchaseQuantity);
				cmd.Parameters.AddWithValue("@PurchaseCost", Details.PurchaseCost);
				cmd.Parameters.AddWithValue("@PurchaseVAT", Details.PurchaseVAT);
				cmd.Parameters.AddWithValue("@PInvoiceQuantity", Details.PInvoiceQuantity);
				cmd.Parameters.AddWithValue("@PInvoiceCost", Details.PInvoiceCost);
				cmd.Parameters.AddWithValue("@PInvoiceVAT", Details.PInvoiceVAT);
				cmd.Parameters.AddWithValue("@PReturnQuantity", Details.PReturnQuantity);
				cmd.Parameters.AddWithValue("@PReturnCost", Details.PReturnCost);
				cmd.Parameters.AddWithValue("@PReturnVAT", Details.PReturnVAT);
				cmd.Parameters.AddWithValue("@PDebitQuantity", Details.PDebitQuantity);
				cmd.Parameters.AddWithValue("@PDebitCost", Details.PDebitCost);
				cmd.Parameters.AddWithValue("@PDebitVAT", Details.PDebitVAT);
				cmd.Parameters.AddWithValue("@TransferInQuantity", Details.TransferInQuantity);
				cmd.Parameters.AddWithValue("@TransferInCost", Details.TransferInCost);
				cmd.Parameters.AddWithValue("@TransferInVAT", Details.TransferInVAT);
				cmd.Parameters.AddWithValue("@TransferOutQuantity", Details.TransferOutQuantity);
				cmd.Parameters.AddWithValue("@TransferOutCost", Details.TransferOutCost);
				cmd.Parameters.AddWithValue("@TransferOutVAT", Details.TransferOutVAT);
				cmd.Parameters.AddWithValue("@SoldQuantity", Details.SoldQuantity);
				cmd.Parameters.AddWithValue("@SoldCost", Details.SoldCost);
				cmd.Parameters.AddWithValue("@SoldVAT", Details.SoldVAT);
				cmd.Parameters.AddWithValue("@SReturnQuantity", Details.SReturnQuantity);
				cmd.Parameters.AddWithValue("@SReturnCost", Details.SReturnCost);
				cmd.Parameters.AddWithValue("@SReturnVAT", Details.SReturnVAT);
                cmd.Parameters.AddWithValue("@SCreditQuantity", Details.SCreditQuantity);
                cmd.Parameters.AddWithValue("@SCreditCost", Details.SCreditCost);
                cmd.Parameters.AddWithValue("@SCreditVAT", Details.SCreditVAT);
				cmd.Parameters.AddWithValue("@InvAdjustmentQuantity", Details.InvAdjustmentQuantity);
				cmd.Parameters.AddWithValue("@InvAdjustmentCost", Details.InvAdjustmentCost);
				cmd.Parameters.AddWithValue("@InvAdjustmentVAT", Details.InvAdjustmentVAT);
				cmd.Parameters.AddWithValue("@ClosingQuantity", Details.ClosingQuantity);
				cmd.Parameters.AddWithValue("@ClosingCost", Details.ClosingCost);
				cmd.Parameters.AddWithValue("@ClosingVAT", Details.ClosingVAT);

				cmd.ExecuteNonQuery();

				SQL = "SELECT LAST_INSERT_ID();";
				
				cmd.Parameters.Clear(); 
				cmd.CommandText = SQL;

                System.Data.DataTable dt = new System.Data.DataTable("tblInventory");
                MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
                adapter.Fill(dt);

                Int64 iID = 0;
                foreach (System.Data.DataRow dr in dt.Rows)
                {
                    iID = Int64.Parse(dr[0].ToString());
                }

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

		public void Update(InventoryDetails Details)
		{
			try 
			{
				string SQL=	"UPDATE tblInventory SET " +
                                "BranchID		        =	@BranchID, " +
								"PostingDateFrom		=	@PostingDateFrom, " +
								"PostingDateTo			=	@PostingDateTo, " +
								"PostingDate			=	@PostingDate, " +
								"ReferenceNo			=	@ReferenceNo, " +
								"ContactID				=	@ContactID, " +
								"ContactCode			=	@ContactCode, " +
								"ProductID				=	@ProductID, " +
								"VariationMatrixID		=	@VariationMatrixID, " +
								"MatrixDescription		=	@MatrixDescription, " +
								"ProductCode			=	@ProductCode, " +
								"PurchaseQuantity		=	@PurchaseQuantity, " +
								"PurchaseCost			=	@PurchaseCost, " +
								"PurchaseVAT			=	@PurchaseVAT, " +
								"PInvoiceQuantity		=	@PInvoiceQuantity, " +
								"PInvoiceCost			=	@PInvoiceCost, " +
								"PInvoiceVAT			=	@PInvoiceVAT, " +
								"PReturnQuantity		=	@PReturnQuantity, " +
								"PReturnCost			=	@PReturnCost, " +
								"PReturnVAT				=	@PReturnVAT, " +
								"PDebitQuantity			=	@PDebitQuantity, " +
								"PDebitCost				=	@PDebitCost, " +
								"PDebitVAT				=	@PDebitVAT, " +
								"TransferInQuantity		=	@TransferInQuantity, " +
								"TransferInCost			=	@TransferInCost, " +
								"TransferInVAT			=	@TransferInVAT, " +
								"TransferOutQuantity	=	@TransferOutQuantity, " +
								"TransferOutCost		=	@TransferOutCost, " +
								"TransferOutVAT			=	@TransferOutVAT, " +
								"SoldQuantity			=	@SoldQuantity, " +
								"SoldCost				=	@SoldCost, " +
								"SoldVAT				=	@SoldVAT, " +
								"SReturnQuantity		=	@SReturnQuantity, " +
								"SReturnCost			=	@SReturnCost, " +
								"SReturnVAT				=	@SReturnVAT, " +
								"InvAdjustmentQuantity	=	@InvAdjustmentQuantity, " +
								"InvAdjustmentCost		=	@InvAdjustmentCost, " +
								"InvAdjustmentVAT		=	@InvAdjustmentVAT, " +
								"ClosingQuantity		=	@ClosingQuantity, " +
								"ClosingCost			=	@ClosingCost, " +
								"ClosingVAT				=	@ClosingVAT " +
							"WHERE InventoryID = @InventoryID;";
				  
				MySqlConnection cn = GetConnection();
	 			
				MySqlCommand cmd = new MySqlCommand();
				cmd.Connection = cn;
				cmd.Transaction = mTransaction;
				cmd.CommandType = System.Data.CommandType.Text;
				cmd.CommandText = SQL;

                cmd.Parameters.AddWithValue("@BranchID", Details.BranchID);
                cmd.Parameters.AddWithValue("@PostingDateFrom", Details.PostingDateFrom.ToString("yyyy-MM-dd HH:mm:ss"));
                cmd.Parameters.AddWithValue("@PostingDateTo", Details.PostingDateTo.ToString("yyyy-MM-dd HH:mm:ss"));
                cmd.Parameters.AddWithValue("@PostingDate", Details.PostingDate.ToString("yyyy-MM-dd HH:mm:ss"));
                cmd.Parameters.AddWithValue("@ReferenceNo", Details.ReferenceNo);
                cmd.Parameters.AddWithValue("@ContactID", Details.ContactID);
                cmd.Parameters.AddWithValue("@ContactCode", Details.ContactCode);
                cmd.Parameters.AddWithValue("@ProductID", Details.ProductID);
                cmd.Parameters.AddWithValue("@ProductCode", Details.ProductCode);
                cmd.Parameters.AddWithValue("@VariationMatrixID", Details.VariationMatrixID);
                cmd.Parameters.AddWithValue("@MatrixDescription", Details.MatrixDescription);
                cmd.Parameters.AddWithValue("@PurchaseQuantity", Details.PurchaseQuantity);
                cmd.Parameters.AddWithValue("@PurchaseCost", Details.PurchaseCost);
                cmd.Parameters.AddWithValue("@PurchaseVAT", Details.PurchaseVAT);
                cmd.Parameters.AddWithValue("@PInvoiceQuantity", Details.PInvoiceQuantity);
                cmd.Parameters.AddWithValue("@PInvoiceCost", Details.PInvoiceCost);
                cmd.Parameters.AddWithValue("@PInvoiceVAT", Details.PInvoiceVAT);
                cmd.Parameters.AddWithValue("@PReturnQuantity", Details.PReturnQuantity);
                cmd.Parameters.AddWithValue("@PReturnCost", Details.PReturnCost);
                cmd.Parameters.AddWithValue("@PReturnVAT", Details.PReturnVAT);
                cmd.Parameters.AddWithValue("@PDebitQuantity", Details.PDebitQuantity);
                cmd.Parameters.AddWithValue("@PDebitCost", Details.PDebitCost);
                cmd.Parameters.AddWithValue("@PDebitVAT", Details.PDebitVAT);
                cmd.Parameters.AddWithValue("@TransferInQuantity", Details.TransferInQuantity);
                cmd.Parameters.AddWithValue("@TransferInCost", Details.TransferInCost);
                cmd.Parameters.AddWithValue("@TransferInVAT", Details.TransferInVAT);
                cmd.Parameters.AddWithValue("@TransferOutQuantity", Details.TransferOutQuantity);
                cmd.Parameters.AddWithValue("@TransferOutCost", Details.TransferOutCost);
                cmd.Parameters.AddWithValue("@TransferOutVAT", Details.TransferOutVAT);
                cmd.Parameters.AddWithValue("@SoldQuantity", Details.SoldQuantity);
                cmd.Parameters.AddWithValue("@SoldCost", Details.SoldCost);
                cmd.Parameters.AddWithValue("@SoldVAT", Details.SoldVAT);
                cmd.Parameters.AddWithValue("@SReturnQuantity", Details.SReturnQuantity);
                cmd.Parameters.AddWithValue("@SReturnCost", Details.SReturnCost);
                cmd.Parameters.AddWithValue("@SReturnVAT", Details.SReturnVAT);
                cmd.Parameters.AddWithValue("@SCreditQuantity", Details.SCreditQuantity);
                cmd.Parameters.AddWithValue("@SCreditCost", Details.SCreditCost);
                cmd.Parameters.AddWithValue("@SCreditVAT", Details.SCreditVAT);
                cmd.Parameters.AddWithValue("@InvAdjustmentQuantity", Details.InvAdjustmentQuantity);
                cmd.Parameters.AddWithValue("@InvAdjustmentCost", Details.InvAdjustmentCost);
                cmd.Parameters.AddWithValue("@InvAdjustmentVAT", Details.InvAdjustmentVAT);
                cmd.Parameters.AddWithValue("@ClosingQuantity", Details.ClosingQuantity);
                cmd.Parameters.AddWithValue("@ClosingCost", Details.ClosingCost);
                cmd.Parameters.AddWithValue("@ClosingVAT", Details.ClosingVAT);
				cmd.Parameters.AddWithValue("@InventoryID", Details.InventoryID);

				cmd.ExecuteNonQuery();

                //PO clsPO = new PO(Connection, Transaction);
                //clsPO.SynchronizeAmount(Details.ReferenceNo);
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

		public void UpdateSales(InventoryDetails Details)
		{
			try 
			{
				string SQL=	"UPDATE tblInventory SET " + 
								"PostingDateFrom		=	@PostingDateFrom, " +
								"PostingDateTo			=	@PostingDateTo, " +
								"PostingDate			=	@PostingDate, " +
								"ReferenceNo			=	@ReferenceNo, " +
								"ContactID				=	@ContactID, " +
								"ContactCode			=	@ContactCode, " +
								"ProductID				=	@ProductID, " +
								"VariationMatrixID		=	@VariationMatrixID, " +
								"MatrixDescription		=	@MatrixDescription, " +
								"ProductCode			=	@ProductCode, " +
								"SoldQuantity			=	@SoldQuantity, " +
								"SoldCost				=	@SoldCost, " +
								"SoldVAT				=	@SoldVAT " +
							"WHERE InventoryID = @InventoryID;";
				  
				MySqlConnection cn = GetConnection();
	 			
				MySqlCommand cmd = new MySqlCommand();
				cmd.Connection = cn;
				cmd.Transaction = mTransaction;
				cmd.CommandType = System.Data.CommandType.Text;
				cmd.CommandText = SQL;
				
				MySqlParameter prmPostingDateFrom = new MySqlParameter("@PostingDateFrom",MySqlDbType.DateTime);
				prmPostingDateFrom.Value = Details.PostingDateFrom.ToString("yyyy-MM-dd HH:mm:ss");
				cmd.Parameters.Add(prmPostingDateFrom);

				MySqlParameter prmPostingDateTo = new MySqlParameter("@PostingDateTo",MySqlDbType.DateTime);
				prmPostingDateTo.Value = Details.PostingDateTo.ToString("yyyy-MM-dd HH:mm:ss");
				cmd.Parameters.Add(prmPostingDateTo);

				MySqlParameter prmPostingDate = new MySqlParameter("@PostingDate",MySqlDbType.DateTime);
				prmPostingDate.Value = Details.PostingDate.ToString("yyyy-MM-dd HH:mm:ss");
				cmd.Parameters.Add(prmPostingDate);

				MySqlParameter prmReferenceNo = new MySqlParameter("@ReferenceNo",MySqlDbType.String);
				prmReferenceNo.Value = Details.ReferenceNo;
				cmd.Parameters.Add(prmReferenceNo);

				MySqlParameter prmContactID = new MySqlParameter("@ContactID",System.Data.DbType.Int64);			
				prmContactID.Value = Details.ContactID;
				cmd.Parameters.Add(prmContactID);
								 
				MySqlParameter prmContactCode = new MySqlParameter("@ContactCode",MySqlDbType.String);
				prmContactCode.Value = Details.ContactCode;
				cmd.Parameters.Add(prmContactCode);

				MySqlParameter prmProductID = new MySqlParameter("@ProductID",System.Data.DbType.Int64);
				prmProductID.Value = Details.ProductID;
				cmd.Parameters.Add(prmProductID);
								 
				MySqlParameter prmProductCode = new MySqlParameter("@ProductCode",MySqlDbType.String);
				prmProductCode.Value = Details.ProductCode;
				cmd.Parameters.Add(prmProductCode);

				MySqlParameter prmVariationMatrixID = new MySqlParameter("@VariationMatrixID",System.Data.DbType.Int64);			
				prmVariationMatrixID.Value = Details.VariationMatrixID;
				cmd.Parameters.Add(prmVariationMatrixID);

				MySqlParameter prmMatrixDescription = new MySqlParameter("@MatrixDescription",MySqlDbType.String);			
				prmMatrixDescription.Value = Details.MatrixDescription;
				cmd.Parameters.Add(prmMatrixDescription);
		 
				MySqlParameter prmSoldQuantity = new MySqlParameter("@SoldQuantity",System.Data.DbType.Decimal);			
				prmSoldQuantity.Value = Details.SoldQuantity;
				cmd.Parameters.Add(prmSoldQuantity);
					
				MySqlParameter prmSoldCost = new MySqlParameter("@SoldCost",System.Data.DbType.Decimal);			
				prmSoldCost.Value = Details.SoldCost;
				cmd.Parameters.Add(prmSoldCost);

				MySqlParameter prmSoldVAT = new MySqlParameter("@SoldVAT",System.Data.DbType.Decimal);			
				prmSoldVAT.Value = Details.SoldVAT;
				cmd.Parameters.Add(prmSoldVAT);

				MySqlParameter prmInventoryID = new MySqlParameter("@InventoryID",System.Data.DbType.Int64);			
				prmInventoryID.Value = Details.InventoryID;
				cmd.Parameters.Add(prmInventoryID);

				cmd.ExecuteNonQuery();

                //PO clsPO = new PO(Connection, Transaction);
                //clsPO.SynchronizeAmount(Details.ReferenceNo);
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
				string SQL=	"DELETE FROM tblInventory WHERE InventoryID IN (" + IDs + ");";
				  
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
                                "a.BranchID, " +
                                "a.InventoryID, " +
                                "a.PostingDateFrom, " +
                                "a.PostingDateTo, " +
                                "a.PostingDate, " +
                                "a.ReferenceNo, " +
                                "a.ContactID, " +
                                "a.ContactCode, " +
                                "a.ProductID, " +
                                "a.ProductCode, " +
                                "a.VariationMatrixID, " +
                                "a.MatrixDescription, " +
                                "a.PurchaseQuantity, " +
                                "a.PurchaseCost, " +
                                "a.PurchaseVAT, " +
                                "a.PInvoiceQuantity, " +
                                "a.PInvoiceCost, " +
                                "a.PInvoiceVAT, " +
                                "a.PReturnQuantity, " +
                                "a.PReturnCost, " +
                                "a.PReturnVAT, " +
                                "a.PDebitQuantity, " +
                                "a.PDebitCost, " +
                                "a.PDebitVAT, " +
                                "a.TransferInQuantity, " +
                                "a.TransferInCost, " +
                                "a.TransferInVAT, " +
                                "a.TransferOutQuantity, " +
                                "a.TransferOutCost, " +
                                "a.TransferOutVAT, " +
                                "a.SoldQuantity, " +
                                "a.SoldCost, " +
                                "a.SoldVAT, " +
                                "a.SReturnQuantity, " +
                                "a.SReturnCost, " +
                                "a.SReturnVAT, " +
                                "a.SCreditQuantity, " +
                                "a.SCreditCost, " +
                                "a.SCreditVAT, " +
                                "a.InvAdjustmentQuantity, " +
                                "a.InvAdjustmentCost, " +
                                "a.InvAdjustmentVAT, " +
                                "a.ClosingQuantity, " +
                                "a.ClosingCost, " +
                                "a.ClosingVAT, " +
                                "a.ClosingActualQuantity, " +
                                "a.PurchasePrice " +
                            "FROM tblInventory a " +
                                "INNER JOIN tblProducts b ON a.ProductID = b.ProductID " +
                            "WHERE 1=1 ";
            return stSQL;
        }

		#region Details

		public InventoryDetails Details(long InventoryID)
		{
			try
			{
				string SQL=	SQLSelect() + "AND a.InventoryID = @InventoryID;";
				  
				MySqlConnection cn = GetConnection();
	 			
				MySqlCommand cmd = new MySqlCommand();
				cmd.Connection = cn;
				cmd.Transaction = mTransaction;
				cmd.CommandType = System.Data.CommandType.Text;
				cmd.CommandText = SQL;

				MySqlParameter prmInventoryID = new MySqlParameter("@InventoryID",System.Data.DbType.Int64);
				prmInventoryID.Value = InventoryID;
				cmd.Parameters.Add(prmInventoryID);

				MySqlDataReader myReader = (MySqlDataReader) cmd.ExecuteReader(System.Data.CommandBehavior.SingleResult);
				
				InventoryDetails Details = new InventoryDetails();

				while (myReader.Read()) 
				{
                    Details.BranchID = myReader.GetInt32("BranchID");
					Details.InventoryID = myReader.GetInt64("InventoryID");
					Details.PostingDateFrom = myReader.GetDateTime("PostingDateFrom");
					Details.PostingDateTo = myReader.GetDateTime("PostingDateTo");
					Details.PostingDate = myReader.GetDateTime("PostingDate");
					Details.ReferenceNo = "" + myReader["ReferenceNo"].ToString();
					Details.ContactID = myReader.GetInt64("ContactID");
					Details.ContactCode = "" + myReader["ContactCode"].ToString();
					Details.ProductID = myReader.GetInt64("ProductID");
					Details.ProductCode = "" + myReader["ProductCode"].ToString();
					Details.VariationMatrixID = myReader.GetInt64("VariationMatrixID");
					Details.MatrixDescription = "" + myReader["MatrixDescription"].ToString();
					Details.PurchaseQuantity = myReader.GetDecimal("PurchaseQuantity");
					Details.PurchaseCost = myReader.GetDecimal("PurchaseCost");
					Details.PurchaseVAT = myReader.GetDecimal("PurchaseVAT");
					Details.PReturnQuantity = myReader.GetDecimal("PReturnQuantity");
					Details.PReturnCost = myReader.GetDecimal("PReturnCost");
					Details.PReturnVAT = myReader.GetDecimal("PReturnVAT");
					Details.TransferInQuantity = myReader.GetDecimal("TransferInQuantity");
					Details.TransferInCost = myReader.GetDecimal("TransferInCost");
					Details.TransferInVAT = myReader.GetDecimal("TransferInVAT");
					Details.TransferOutQuantity = myReader.GetDecimal("TransferOutQuantity");
					Details.TransferOutCost = myReader.GetDecimal("TransferOutCost");
					Details.TransferOutVAT = myReader.GetDecimal("TransferOutVAT");
					Details.SoldQuantity = myReader.GetDecimal("SoldQuantity");
					Details.SoldCost = myReader.GetDecimal("SoldCost");
					Details.SoldVAT = myReader.GetDecimal("SoldVAT");
					Details.SReturnQuantity = myReader.GetDecimal("SReturnQuantity");
					Details.SReturnCost = myReader.GetDecimal("SReturnCost");
					Details.SReturnVAT = myReader.GetDecimal("SReturnVAT");
                    Details.SCreditQuantity = myReader.GetDecimal("SCreditQuantity");
                    Details.SCreditCost = myReader.GetDecimal("SCreditCost");
                    Details.SCreditVAT = myReader.GetDecimal("SCreditVAT");

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

        public System.Data.DataTable ClosingInventoryReferenceNos(DateTime PostingDateFrom, DateTime PostingDateTo)
        {
            string SQL = "SELECT ReferenceNo, PostingDate FROM tblInventory WHERE ReferenceNo LIKE @CLOSE_INVENTORY_CODE ";

            if (PostingDateFrom != DateTime.MinValue)
                SQL += "AND PostingDate >= '" + PostingDateFrom.ToString("yyyy-MM-dd") + "' ";

            if (PostingDateTo != DateTime.MinValue)
                SQL += "AND PostingDate <= '" + PostingDateTo.ToString("yyyy-MM-dd") + "' ";

            SQL += "GROUP BY ReferenceNo, PostingDate ORDER BY ReferenceNo ";

            MySqlConnection cn = GetConnection();

            MySqlCommand cmd = new MySqlCommand();
            cmd.Connection = cn;
            cmd.Transaction = mTransaction;
            cmd.CommandType = System.Data.CommandType.Text;
            cmd.CommandText = SQL;

            cmd.Parameters.AddWithValue("@CLOSE_INVENTORY_CODE", Constants.CLOSE_INVENTORY_CODE + "%");

            System.Data.DataTable dt = new System.Data.DataTable("tblInventory");
            MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
            adapter.Fill(dt);

            return dt;
        }
		public System.Data.DataTable DataList(string SortField, SortOption SortOrder)
		{
            string SQL = SQLSelect();

            if (SortField == string.Empty) SortField = "InventoryID";
            SQL += "ORDER BY " + SortField;

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

            System.Data.DataTable dt = new System.Data.DataTable("tblInventory");
            MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
            adapter.Fill(dt);

            return dt;
		}
        public System.Data.DataTable DataList(string ReferenceNo, bool IncludeShortOverProducts, long ProductGroupID, long ProductSubGroupID, string SortField, SortOption SortOrder)
        {
            string SQL = SQLSelect() + "AND ReferenceNo = @ReferenceNo ";

            if (IncludeShortOverProducts) SQL += "AND ClosingQuantity <> ClosingActualQuantity ";

            if (ProductSubGroupID != 0)
                SQL += "AND ProductSubGroupID = " + ProductSubGroupID + " ";
            else if (ProductGroupID != 0)
                SQL += "AND ProductSubGroupID IN (SELECT DISTINCT(ProductSubGroupID) FROM tblProductSubGroup WHERE ProductGroupID = " + ProductGroupID + ") ";

            if (SortField == string.Empty) SortField = "InventoryID";
            SQL += "ORDER BY " + SortField;

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

            cmd.Parameters.AddWithValue("@ReferenceNo", ReferenceNo);

            System.Data.DataTable dt = new System.Data.DataTable("tblInventory");
            MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
            adapter.Fill(dt);

            return dt;
        }
		public MySqlDataReader List(string SortField, SortOption SortOrder)
		{
			try
			{
				string SQL=	SQLSelect() ;

                if (SortField == string.Empty) SortField = "InventoryID";
                SQL += "ORDER BY " + SortField;

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
		public MySqlDataReader List(DateTime PostingDateFrom, DateTime PostingDateTo, string SortField, SortOption SortOrder)
		{
			try
			{
                string SQL = SQLSelect() + "AND PostingDateFrom = @PostingDateFrom " +
                                                "AND  PostingDateTo = @PostingDateTo ";

                if (SortField == string.Empty) SortField = "InventoryID";
                SQL += "ORDER BY " + SortField;

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
				
				MySqlParameter prmPostingDateFrom = new MySqlParameter("@PostingDateFrom",MySqlDbType.DateTime);
				prmPostingDateFrom.Value = PostingDateFrom.ToString("yyyy-MM-dd HH:mm:ss");
				cmd.Parameters.Add(prmPostingDateFrom);

				MySqlParameter prmPostingDateTo = new MySqlParameter("@PostingDateTo",MySqlDbType.DateTime);
				prmPostingDateTo.Value = PostingDateTo.ToString("yyyy-MM-dd HH:mm:ss");
				cmd.Parameters.Add(prmPostingDateTo);

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
                string SQL = SQLSelect() + "AND (a.ContactCode LIKE @SearchKey or a.ProductCode LIKE @SearchKey or MatrixDescription LIKE @SearchKey) ";

                if (SortField == string.Empty) SortField = "InventoryID";
                SQL += "ORDER BY " + SortField;

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
		public MySqlDataReader Search(DateTime PostingDateFrom, DateTime PostingDateTo, string SearchKey, string SortField, SortOption SortOrder)
		{
			try
			{
                string SQL = SQLSelect() + "AND PostingDateFrom = @PostingDateFrom " +
                                                "AND PostingDateTo = @PostingDateTo " +
                                                "AND (a.ContactCode LIKE @SearchKey or a.ProductCode LIKE @SearchKey or MatrixDescription LIKE @SearchKey) ";

                if (SortField == string.Empty) SortField = "InventoryID";
                SQL += "ORDER BY " + SortField;

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
				
				MySqlParameter prmPostingDateFrom = new MySqlParameter("@PostingDateFrom",MySqlDbType.DateTime);
				prmPostingDateFrom.Value = PostingDateFrom.ToString("yyyy-MM-dd HH:mm:ss");
				cmd.Parameters.Add(prmPostingDateFrom);

				MySqlParameter prmPostingDateTo = new MySqlParameter("@PostingDateTo",MySqlDbType.DateTime);
				prmPostingDateTo.Value = PostingDateTo.ToString("yyyy-MM-dd HH:mm:ss");
				cmd.Parameters.Add(prmPostingDateTo);

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

		#region Reports

		public System.Data.DataTable ByVendor(DateTime PostingDateFrom, DateTime PostingDateTo)
		{
			try
			{
				string SQL=	"SELECT " +
								"ContactCode AS SupplierCode, " +
								"ContactName AS SupplierName, " +
								"SUM(PurchaseCost) AS PurchaseCost, " +
								"SUM(PurchaseVAT) AS PurchaseVAT, " +
								"SUM(PInvoiceCost) AS InvoiceCost, " +
								"SUM(PInvoiceVAT) AS InvoiceVAT " +
								"SUM(PReturnCost) AS ReturnCost, " +
								"SUM(PReturnVAT) AS ReturnVAT, " +
								"SUM(PDebitCost) AS DebitCost, " +
								"SUM(PDebitVAT) AS DebitVAT " +
							"FROM tblInventory a INNER JOIN tblContacts b ON a.ContactID = b.ContactID " +
							"WHERE PostingDate BETWEEN @PostingDateFrom AND @PostingDateTo " +
							"GROUP BY ContactCode, ContactName " +
							"ORDER BY ContactCode;";

				MySqlConnection cn = GetConnection();

				MySqlCommand cmd = new MySqlCommand();
				cmd.Connection = cn;
				cmd.Transaction = mTransaction;
				cmd.CommandType = System.Data.CommandType.Text;
				cmd.CommandText = SQL;
				
				MySqlParameter prmPostingDateFrom = new MySqlParameter("@PostingDateFrom",MySqlDbType.DateTime);
				prmPostingDateFrom.Value = PostingDateFrom.ToString("yyyy-MM-dd HH:mm:ss");
				cmd.Parameters.Add(prmPostingDateFrom);

				MySqlParameter prmPostingDateTo = new MySqlParameter("@PostingDateTo",MySqlDbType.DateTime);
				prmPostingDateTo.Value = PostingDateTo.ToString("yyyy-MM-dd HH:mm:ss");
				cmd.Parameters.Add(prmPostingDateTo);

				System.Data.DataTable dt = new System.Data.DataTable("Inventory");
				MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
				adapter.Fill(dt);

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
	}
}


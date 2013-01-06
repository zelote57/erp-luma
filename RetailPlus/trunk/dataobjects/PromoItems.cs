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
	public struct PromoItemsDetails
	{
		public Int64 PromoItemsID;
		public Int64 PromoID;
		public Int64 ContactID;
		public Int64 ProductGroupID;
		public Int64 ProductSubGroupID;
		public Int64 ProductID;
		public Int64 VariationMatrixID;
		public decimal Quantity;
		public decimal PromoValue;
		public byte InPercent;
	}

	[StrongNameIdentityPermissionAttribute(SecurityAction.LinkDemand,
		 PublicKey = "002400000480000094000000060200000024000" +
		 "052534131000400000100010053D785642F9F960B43157E0380" +
		 "F393BEE53E8DFAFBF441366C1B6F8B48D9DDF0D527B1F3B21EA" +
		 "E85D2FDB664CE81EB8A87DBE4C589D6F4202FE2B7D4B978BB69" +
		 "684874612CB9B8DB7A0339400A9C4E68277884B07817363D242" +
		 "E3696F9FACDBEA831810AE6DC9EDCA91A7B5DA12FE7BF65D113" +
		 "FF52834EAFB5A7A1FDFD5851A3")]
	public class PromoItems
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

		public PromoItems()
		{
			
		}

		public PromoItems(MySqlConnection Connection, MySqlTransaction Transaction)
		{
			this.mConnection = Connection;
			this.mTransaction = Transaction;
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
				IsInTransaction = true;
			}

			return mConnection;
		} 


		#endregion

		#region Insert and Update

		public Int64 Insert(PromoItemsDetails Details)
		{
			try 
			{
				string SQL =	"INSERT INTO tblPromoItems (" + 
									"PromoID, " +
									"ContactID, " +
									"ProductGroupID, " +
									"ProductSubGroupID, " +
									"ProductID, " +
									"VariationMatrixID, " +
									"Quantity, " +
									"PromoValue, " +
									"InPercent " +
								")VALUES (" +
									"@PromoID, " +
									"@ContactID, " +
									"@ProductGroupID, " +
									"@ProductSubGroupID, " +
									"@ProductID, " +
									"@VariationMatrixID, " +
									"@Quantity, " +
									"@PromoValue, " +
									"@InPercent);";
				  
				MySqlConnection cn = GetConnection();
	 			
				MySqlCommand cmd = new MySqlCommand();
				cmd.Connection = cn;
				cmd.Transaction = mTransaction;
				cmd.CommandType = System.Data.CommandType.Text;
				cmd.CommandText = SQL;
				
				MySqlParameter prmPromoID = new MySqlParameter("@PromoID",MySqlDbType.Int64);			
				prmPromoID.Value = Details.PromoID;
				cmd.Parameters.Add(prmPromoID);

				MySqlParameter prmContactID = new MySqlParameter("@ContactID",MySqlDbType.Int64);			
				prmContactID.Value = Details.ContactID;
				cmd.Parameters.Add(prmContactID);

				MySqlParameter prmProductGroupID = new MySqlParameter("@ProductGroupID",MySqlDbType.Int64);			
				prmProductGroupID.Value = Details.ProductGroupID;
				cmd.Parameters.Add(prmProductGroupID);

				MySqlParameter prmProductSubGroupID = new MySqlParameter("@ProductSubGroupID",MySqlDbType.Int64);			
				prmProductSubGroupID.Value = Details.ProductSubGroupID;
				cmd.Parameters.Add(prmProductSubGroupID);

				MySqlParameter prmProductID = new MySqlParameter("@ProductID",MySqlDbType.Int64);			
				prmProductID.Value = Details.ProductID;
				cmd.Parameters.Add(prmProductID);

				MySqlParameter prmVariationMatrixID = new MySqlParameter("@VariationMatrixID",MySqlDbType.Int64);			
				prmVariationMatrixID.Value = Details.VariationMatrixID;
				cmd.Parameters.Add(prmVariationMatrixID);

				MySqlParameter prmQuantity = new MySqlParameter("@Quantity",MySqlDbType.Decimal);			
				prmQuantity.Value = Details.Quantity;
				cmd.Parameters.Add(prmQuantity);
				
				MySqlParameter prmPromoValue = new MySqlParameter("@PromoValue",MySqlDbType.Decimal);			
				prmPromoValue.Value = Details.PromoValue;
				cmd.Parameters.Add(prmPromoValue);

				MySqlParameter prmInPercent = new MySqlParameter("@InPercent",MySqlDbType.Int32);			
				prmInPercent.Value = Details.InPercent;
				cmd.Parameters.Add(prmInPercent);

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

		public void Update(PromoItemsDetails Details)
		{
			try 
			{
				string SQL=	"UPDATE tblPromoItems SET " + 
							"PromoID = @PromoID, " +  
							"ProductGroupID = @ProductGroupID, " +  
							"ProductSubGroupID = @ProductSubGroupID, " +  
							"ProductID = @ProductID, " +  
							"Quantity = @Quantity, " +  
							"PromoValue = @PromoValue, " +  
							"InPercent = @InPercent " +  
							"WHERE PromoItemsID = @PromoItemsID;";
							
				  
				MySqlConnection cn = GetConnection();
	 			
				MySqlCommand cmd = new MySqlCommand();
				cmd.Connection = cn;
				cmd.Transaction = mTransaction;
				cmd.CommandType = System.Data.CommandType.Text;
				cmd.CommandText = SQL;

				MySqlParameter prmPromoID = new MySqlParameter("@PromoID",MySqlDbType.Int64);			
				prmPromoID.Value = Details.PromoID;
				cmd.Parameters.Add(prmPromoID);

				MySqlParameter prmContactID = new MySqlParameter("@ContactID",MySqlDbType.Int64);			
				prmContactID.Value = Details.ContactID;
				cmd.Parameters.Add(prmContactID);

				MySqlParameter prmProductGroupID = new MySqlParameter("@ProductGroupID",MySqlDbType.Int64);			
				prmProductGroupID.Value = Details.ProductGroupID;
				cmd.Parameters.Add(prmProductGroupID);

				MySqlParameter prmProductSubGroupID = new MySqlParameter("@ProductSubGroupID",MySqlDbType.Int64);			
				prmProductSubGroupID.Value = Details.ProductSubGroupID;
				cmd.Parameters.Add(prmProductSubGroupID);

				MySqlParameter prmProductID = new MySqlParameter("@ProductID",MySqlDbType.Int32);			
				prmProductID.Value = Details.ProductID;
				cmd.Parameters.Add(prmProductID);

				MySqlParameter prmVariationMatrixID = new MySqlParameter("@VariationMatrixID",MySqlDbType.Int64);			
				prmVariationMatrixID.Value = Details.VariationMatrixID;
				cmd.Parameters.Add(prmVariationMatrixID);

				MySqlParameter prmQuantity = new MySqlParameter("@Quantity",MySqlDbType.Decimal);			
				prmQuantity.Value = Details.Quantity;
				cmd.Parameters.Add(prmQuantity);
				
				MySqlParameter prmPromoValue = new MySqlParameter("@PromoValue",MySqlDbType.Decimal);			
				prmPromoValue.Value = Details.PromoValue;
				cmd.Parameters.Add(prmPromoValue);

				MySqlParameter prmInPercent = new MySqlParameter("@InPercent",MySqlDbType.Int32);			
				prmInPercent.Value = Details.InPercent;
				cmd.Parameters.Add(prmInPercent);

				MySqlParameter prmPromoItemsID = new MySqlParameter("@PromoItemsID",MySqlDbType.Int32);	
				prmPromoItemsID.Value = Details.PromoItemsID;
				cmd.Parameters.Add(prmPromoItemsID);

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
				MySqlConnection cn = GetConnection();
				MySqlCommand cmd;

				string SQL=	"DELETE FROM tblPromoItems WHERE PromoItemsID IN (" + IDs + ");";
				cmd = new MySqlCommand();
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

		public PromoItemsDetails DetailsByPromoItemsID(Int64 PromoItemsID)
		{
			try
			{
				string SQL=	"SELECT " +
								"PromoItemsID, " +
								"PromoID, " +
								"ContactID, " +
								"ProductGroupID, " +
								"ProductSubGroupID, " +
								"ProductID, " +
								"VariationMatrixID, " +
								"Quantity, " +
								"PromoValue, " +
								"InPercent " +
							"FROM tblPromoItems " +
							"WHERE PromoItemsID = @PromoItemsID;"; 
				  
				MySqlConnection cn = GetConnection();
	 			
				MySqlCommand cmd = new MySqlCommand();
				cmd.Connection = cn;
				cmd.Transaction = mTransaction;
				cmd.CommandType = System.Data.CommandType.Text;
				cmd.CommandText = SQL;

				MySqlParameter prmPromoItemsID = new MySqlParameter("@PromoItemsID",System.Data.DbType.Int32);
				prmPromoItemsID.Value = PromoItemsID;
				cmd.Parameters.Add(prmPromoItemsID);

				MySqlDataReader myReader = (MySqlDataReader) cmd.ExecuteReader(System.Data.CommandBehavior.SingleResult);
				
				PromoItemsDetails Details = new PromoItemsDetails();

				while (myReader.Read()) 
				{
					Details.PromoItemsID = myReader.GetInt64("PromoItemsID");
					Details.PromoID = myReader.GetInt64("PromoID");
					Details.ContactID = myReader.GetInt64("ContactID");
					Details.ProductGroupID = myReader.GetInt64("ProductGroupID");
					Details.ProductSubGroupID = myReader.GetInt64("ProductSubGroupID");
					Details.ProductID = myReader.GetInt64("ProductID");
					Details.VariationMatrixID = myReader.GetInt64("VariationMatrixID");
					Details.Quantity = myReader.GetDecimal(6);
					Details.PromoValue = myReader.GetDecimal("PromoValue");
					Details.InPercent = myReader.GetByte("InPercent");
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

		public PromoItemsDetails DetailsByProductID(Int64 ProductID)
		{
			try
			{
				string SQL=	"";
				
				SQL = "SELECT " +
							"PromoItemsID, " +
							"a.PromoID, " +
							"ContactID, " +
							"ProductGroupID, " +
							"ProductSubGroupID, " +
							"ProductID, " +
							"VariationMatrixID, " +
							"Quantity, " +
							"PromoValue, " +
							"InPercent " +
						"FROM tblPromoItems a " + 
						"INNER JOIN tblPromo b ON a.PromoID = b.PromoID " +
						"WHERE ProductID = @ProductID " + 
						"AND DATE_FORMAT(StartDate, '%Y-%m-%d %H:%i') >= CURRENT_DATE " +
						"AND DATE_FORMAT(EndDate, '%Y-%m-%d %H:%i') <= CURRENT_DATE;";
				  
				MySqlConnection cn = GetConnection();
	 			
				MySqlCommand cmd = new MySqlCommand();
				cmd.Connection = cn;
				cmd.Transaction = mTransaction;
				cmd.CommandType = System.Data.CommandType.Text;
				cmd.CommandText = SQL;

				MySqlParameter prmProductID = new MySqlParameter("@ProductID",System.Data.DbType.Int64);
				prmProductID.Value = ProductID;
				cmd.Parameters.Add(prmProductID);

				MySqlDataReader myReader = (MySqlDataReader) cmd.ExecuteReader(System.Data.CommandBehavior.SingleResult);
				
				PromoItemsDetails clsDetails = new PromoItemsDetails();

				while (myReader.Read()) 
				{
					clsDetails.PromoItemsID = myReader.GetInt64("PromoItemsID");
					clsDetails.PromoID = myReader.GetInt64("PromoID");
					clsDetails.ContactID = myReader.GetInt64("ContactID");
					clsDetails.ProductGroupID = myReader.GetInt64("ProductGroupID");
					clsDetails.ProductSubGroupID = myReader.GetInt64("ProductSubGroupID");
					clsDetails.ProductID = myReader.GetInt64("ProductID");
					clsDetails.VariationMatrixID = myReader.GetInt64("VariationMatrixID");
                    clsDetails.Quantity = myReader.GetDecimal("Quantity");
					clsDetails.PromoValue = myReader.GetDecimal("PromoValue");
					clsDetails.InPercent = myReader.GetByte("InPercent");
				}
				myReader.Close();

				return clsDetails;
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

		public MySqlDataReader List(Int64 PromoID, string SortField, SortOption SortOrder)
		{
			try
			{
				string SQL =	"SELECT " +
					"PromoItemsID, " +
					"a.ContactID, " +
					"ContactName, " +
					"a.ProductGroupID, " +
					"ProductGroupName, " +
					"a.ProductSubGroupID, " +
					"ProductSubGroupName, " +
					"a.ProductID, " +
					"ProductDesc, " +
					"a.VariationMatrixID, " +
					"Description, " +
					"a.Quantity, " +
					"a.PromoValue, " +
					"a.InPercent " +
					"FROM tblPromoItems a LEFT OUTER JOIN " +
					"tblContacts b ON a.ContactID = b.ContactID LEFT OUTER JOIN " +
					"tblProductGroup c ON a.ProductGroupID = c.ProductGroupID LEFT OUTER JOIN " +
					"tblProductSubGroup d ON a.ProductSubGroupID = d.ProductSubGroupID LEFT OUTER JOIN " +
					"tblProducts e ON a.ProductID = e.ProductID LEFT OUTER JOIN " +
					"tblProductBaseVariationsMatrix f ON a.VariationMatrixID = f.MatrixID " +
					"WHERE PromoID = @PromoID " +
					"ORDER BY " + SortField; 

				if (SortOrder == SortOption.Ascending)
					SQL += " ASC;";
				else
					SQL += " DESC;";

				MySqlConnection cn = GetConnection();

				MySqlCommand cmd = new MySqlCommand();
				cmd.Connection = cn;
				cmd.Transaction = mTransaction;
				cmd.CommandType = System.Data.CommandType.Text;
				cmd.CommandText = SQL;

				MySqlParameter prmPromoID = new MySqlParameter("@PromoID",MySqlDbType.Int64);			
				prmPromoID.Value = PromoID;
				cmd.Parameters.Add(prmPromoID);

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
		
		public MySqlDataReader Search(Int64 PromoID, string SearchKey, string SortField, SortOption SortOrder)
		{
			try
			{
				string SQL =	"SELECT " +
									"PromoItemsID, " +
									"a.ContactID, " +
									"ContactName, " +
									"a.ProductGroupID, " +
									"ProductGroupName, " +
									"a.ProductSubGroupID, " +
									"ProductSubGroupName, " +
									"a.ProductID, " +
									"ProductDesc, " +
									"a.VariationMatrixID, " +
									"Description, " +
									"Quantity, " +
									"PromoValue, " +
									"InPercent " +
								"FROM tblPromoItems a LEFT OUTER JOIN " +
								"tblContacts b ON a.ContactID = b.ContactID LEFT OUTER JOIN " +
								"tblProductGroup c ON a.ProductGroupID = c.ProductGroupID LEFT OUTER JOIN " +
								"tblProductSubGroup d ON a.ProductSubGroupID = d.ProductSubGroupID LEFT OUTER JOIN " +
								"tblProducts e ON a.ProductID = e.ProductID LEFT OUTER JOIN " +
								"tblProductBaseVariationsMatrix f ON a.VariationMatrixID = f.MatrixID " +
								"WHERE PromoID = @PromoID " +
									"AND (PromoItemsCode LIKE @SearchKey " +
									"OR PromoItemsName LIKE @SearchKey " +
									"OR PromoItemsTypeCode LIKE @SearchKey " +
									"OR PromoItemsTypeName LIKE @SearchKey) " +
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

				MySqlParameter prmPromoID = new MySqlParameter("@PromoID",MySqlDbType.Int32);			
				prmPromoID.Value = PromoID;
				cmd.Parameters.Add(prmPromoID);
				
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

		#region Public Modifiers

		public bool ApplyPromoValue(Int64 ContactID, Int64 ProductID, Int64 VariationMatrixID, out PromoTypes PromoType, out decimal PromoQuantity, out decimal PromoValue, out bool InPercent)
		{
			PromoType = PromoTypes.NotApplicable;
			PromoQuantity = 0;
			PromoValue = 0;
			InPercent = false;

			bool boHasPromo = false;

			MySqlConnection cn = GetConnection();

			Data.Product clsProduct = new Data.Product(cn, mTransaction);
			Data.ProductDetails clsProductDetails = clsProduct.Details(ProductID);

			Int64 ProductSubGroupID = clsProductDetails.ProductSubGroupID;
			Int64 ProductGroupID = clsProductDetails.ProductGroupID;

			string SQL=	"SELECT " +
							"PromoID  " +
						"FROM tblPromo " +
						"WHERE 1=1 " +
							"AND Status = 1 " +
							"AND DATE_FORMAT(StartDate, '%Y-%m-%d %H:%i') <= DATE_FORMAT(NOW(), '%Y-%m-%d %H:%i') " +
							"AND DATE_FORMAT(EndDate, '%Y-%m-%d %H:%i') >= DATE_FORMAT(NOW(), '%Y-%m-%d %H:%i');";
				  
			MySqlCommand cmd = new MySqlCommand();
			cmd.Connection = cn;
			cmd.Transaction = mTransaction;
			cmd.CommandType = System.Data.CommandType.Text;
			cmd.CommandText = SQL;
			
			MySqlDataReader myReader = (MySqlDataReader) cmd.ExecuteReader(System.Data.CommandBehavior.SingleResult);
			
			while (myReader.Read())
			{
				boHasPromo = true;
			}
			myReader.Close();

			if (boHasPromo == false)	//return agad if no Promo is affected by date
				return boHasPromo;

			MySqlParameter prmContactID = new MySqlParameter("@ContactID",MySqlDbType.Int64);
			prmContactID.Value = ContactID;

			MySqlParameter prmProductGroupID = new MySqlParameter("@ProductGroupID",MySqlDbType.Int64);
			prmProductGroupID.Value = ProductGroupID;

			MySqlParameter prmProductSubGroupID = new MySqlParameter("@ProductSubGroupID",MySqlDbType.Int64);
			prmProductSubGroupID.Value = ProductSubGroupID;

			MySqlParameter prmProductID = new MySqlParameter("@ProductID",MySqlDbType.Int64);
			prmProductID.Value = ProductID;

			MySqlParameter prmVariationMatrixID = new MySqlParameter("@VariationMatrixID",MySqlDbType.Int64);
			prmVariationMatrixID.Value = VariationMatrixID;

			/*******************************Up to Contact, Group, Sub, Prod and VarM ID only*****************************/
			SQL=	"SELECT " +
						"PromoItemsID, " + 
						"a.PromoID, " +
						"PromoTypeID, " +
						"ProductGroupID, " +
						"ProductSubGroupID, " +
						"ProductID,  " +
						"VariationMatrixID, " +
						"Quantity,  " +
						"PromoValue,  " +
						"InPercent  " +
					"FROM tblPromoItems a  " +
					"INNER JOIN tblPromo b ON a.PromoID = b.PromoID " +
					"WHERE ContactID = @ContactID " +
						"AND ProductGroupID = @ProductGroupID " +
						"AND ProductSubGroupID = @ProductSubGroupID " +
						"AND ProductID = @ProductID " +
						"AND VariationMatrixID = @VariationMatrixID " +
						"AND Status = 1 " +
						"AND DATE_FORMAT(StartDate, '%Y-%m-%d %H:%i') <= DATE_FORMAT(NOW(), '%Y-%m-%d %H:%i') " +
						"AND DATE_FORMAT(EndDate, '%Y-%m-%d %H:%i') >= DATE_FORMAT(NOW(), '%Y-%m-%d %H:%i');";
				  
			cmd = new MySqlCommand();
			cmd.Connection = cn;
			cmd.Transaction = mTransaction;
			cmd.CommandType = System.Data.CommandType.Text;
			cmd.CommandText = SQL;
			
			cmd.Parameters.Add(prmContactID);
			cmd.Parameters.Add(prmProductGroupID);
			cmd.Parameters.Add(prmProductSubGroupID);
			cmd.Parameters.Add(prmProductID);
			cmd.Parameters.Add(prmVariationMatrixID);

			myReader = (MySqlDataReader) cmd.ExecuteReader(System.Data.CommandBehavior.SingleResult);
			
			while (myReader.Read())
			{
				PromoType = (PromoTypes) Enum.Parse(typeof(PromoTypes), myReader.GetString("PromoTypeID"));
				PromoQuantity = myReader.GetDecimal("Quantity");
				PromoValue = myReader.GetDecimal("PromoValue");
				InPercent = myReader.GetBoolean("InPercent");
				myReader.Close();
				return boHasPromo;
			}
			myReader.Close();

			/*******************************Up to Contact, Sub, Prod and VariationsMatrix ID only*****************************/
			SQL=	"SELECT " +
						"PromoItemsID, " + 
						"a.PromoID, " +
						"PromoTypeID, " +
						"ProductGroupID, " +
						"ProductSubGroupID, " +
						"ProductID,  " +
						"VariationMatrixID, " +
						"Quantity,  " +
						"PromoValue,  " +
						"InPercent  " +
					"FROM tblPromoItems a  " +
					"INNER JOIN tblPromo b ON a.PromoID = b.PromoID " +
					"WHERE ContactID = @ContactID " +
						"AND ProductGroupID = 0 " +
						"AND ProductSubGroupID = @ProductSubGroupID " +
						"AND ProductID = @ProductID " +
						"AND VariationMatrixID = @VariationMatrixID " +
						"AND Status = 1 " +
						"AND DATE_FORMAT(StartDate, '%Y-%m-%d %H:%i') <= DATE_FORMAT(NOW(), '%Y-%m-%d %H:%i') " +
						"AND DATE_FORMAT(EndDate, '%Y-%m-%d %H:%i') >= DATE_FORMAT(NOW(), '%Y-%m-%d %H:%i');";
				  
			cmd = new MySqlCommand();
			cmd.Connection = cn;
			cmd.Transaction = mTransaction;
			cmd.CommandType = System.Data.CommandType.Text;
			cmd.CommandText = SQL;
			
			cmd.Parameters.Add(prmContactID);
			cmd.Parameters.Add(prmProductSubGroupID);
			cmd.Parameters.Add(prmProductID);
			cmd.Parameters.Add(prmVariationMatrixID);

			myReader = (MySqlDataReader) cmd.ExecuteReader(System.Data.CommandBehavior.SingleResult);
			
			while (myReader.Read())
			{
				PromoType = (PromoTypes) Enum.Parse(typeof(PromoTypes), myReader.GetString("PromoTypeID"));
				PromoQuantity = myReader.GetDecimal("Quantity");
				PromoValue = myReader.GetDecimal("PromoValue");
				InPercent = myReader.GetBoolean("InPercent");
				myReader.Close();
				return boHasPromo;
			}
			myReader.Close();

			/*******************************Up to Contact, Prod and VariationsMatrix ID only*****************************/
			SQL=	"SELECT " +
						"PromoItemsID, " + 
						"a.PromoID, " +
						"PromoTypeID, " +
						"ProductGroupID, " +
						"ProductSubGroupID, " +
						"ProductID,  " +
						"VariationMatrixID, " +
						"Quantity,  " +
						"PromoValue,  " +
						"InPercent  " +
					"FROM tblPromoItems a  " +
					"INNER JOIN tblPromo b ON a.PromoID = b.PromoID " +
					"WHERE ContactID = @ContactID " +
						"AND ProductGroupID = 0 " +
						"AND ProductSubGroupID = 0 " +
						"AND ProductID = @ProductID " +
						"AND VariationMatrixID = @VariationMatrixID " +
						"AND Status = 1 " +
						"AND DATE_FORMAT(StartDate, '%Y-%m-%d %H:%i') <= DATE_FORMAT(NOW(), '%Y-%m-%d %H:%i') " +
						"AND DATE_FORMAT(EndDate, '%Y-%m-%d %H:%i') >= DATE_FORMAT(NOW(), '%Y-%m-%d %H:%i');";
				  
			cmd = new MySqlCommand();
			cmd.Connection = cn;
			cmd.Transaction = mTransaction;
			cmd.CommandType = System.Data.CommandType.Text;
			cmd.CommandText = SQL;
			
			cmd.Parameters.Add(prmContactID);
			cmd.Parameters.Add(prmProductID);
			cmd.Parameters.Add(prmVariationMatrixID);

			myReader = (MySqlDataReader) cmd.ExecuteReader(System.Data.CommandBehavior.SingleResult);
			
			while (myReader.Read())
			{
				PromoType = (PromoTypes) Enum.Parse(typeof(PromoTypes), myReader.GetString("PromoTypeID"));
				PromoQuantity = myReader.GetDecimal("Quantity");
				PromoValue = myReader.GetDecimal("PromoValue");
				InPercent = myReader.GetBoolean("InPercent");
				myReader.Close();
				return boHasPromo;
			}
			myReader.Close();

			/*******************************Up to Contact, VariationsMatrix ID only*****************************/
			SQL=	"SELECT " +
						"PromoItemsID, " + 
						"a.PromoID, " +
						"PromoTypeID, " +
						"ProductGroupID, " +
						"ProductSubGroupID, " +
						"ProductID,  " +
						"VariationMatrixID, " +
						"Quantity,  " +
						"PromoValue,  " +
						"InPercent  " +
					"FROM tblPromoItems a  " +
					"INNER JOIN tblPromo b ON a.PromoID = b.PromoID " +
					"WHERE ContactID = @ContactID " +
						"AND ProductGroupID = 0 " +
						"AND ProductSubGroupID = 0 " +
						"AND ProductID = 0 " +
						"AND VariationMatrixID = @VariationMatrixID " +
						"AND Status = 1 " +
						"AND DATE_FORMAT(StartDate, '%Y-%m-%d %H:%i') <= DATE_FORMAT(NOW(), '%Y-%m-%d %H:%i') " +
						"AND DATE_FORMAT(EndDate, '%Y-%m-%d %H:%i') >= DATE_FORMAT(NOW(), '%Y-%m-%d %H:%i');";
				  
			cmd = new MySqlCommand();
			cmd.Connection = cn;
			cmd.Transaction = mTransaction;
			cmd.CommandType = System.Data.CommandType.Text;
			cmd.CommandText = SQL;
			
			cmd.Parameters.Add(prmContactID);
			cmd.Parameters.Add(prmVariationMatrixID);

			myReader = (MySqlDataReader) cmd.ExecuteReader(System.Data.CommandBehavior.SingleResult);
			
			while (myReader.Read())
			{
				PromoType = (PromoTypes) Enum.Parse(typeof(PromoTypes), myReader.GetString("PromoTypeID"));
				PromoQuantity = myReader.GetDecimal("Quantity");
				PromoValue = myReader.GetDecimal("PromoValue");
				InPercent = myReader.GetBoolean("InPercent");
				myReader.Close();
				return boHasPromo;
			}
			myReader.Close();

			/*******************************Up to Contact, Group, Sub, Prod ID only*****************************/
			SQL=	"SELECT " +
						"PromoItemsID, " + 
						"a.PromoID, " +
						"PromoTypeID, " +
						"ProductGroupID, " +
						"ProductSubGroupID, " +
						"ProductID,  " +
						"VariationMatrixID, " +
						"Quantity,  " +
						"PromoValue,  " +
						"InPercent  " +
					"FROM tblPromoItems a  " +
					"INNER JOIN tblPromo b ON a.PromoID = b.PromoID " +
					"WHERE ContactID = @ContactID " +
						"AND ProductGroupID = @ProductGroupID " +
						"AND ProductSubGroupID = @ProductSubGroupID " +
						"AND ProductID = @ProductID " +
						"AND VariationMatrixID = 0 " +
						"AND Status = 1 " +
						"AND DATE_FORMAT(StartDate, '%Y-%m-%d %H:%i') <= DATE_FORMAT(NOW(), '%Y-%m-%d %H:%i') " +
						"AND DATE_FORMAT(EndDate, '%Y-%m-%d %H:%i') >= DATE_FORMAT(NOW(), '%Y-%m-%d %H:%i');";
				  
			cmd = new MySqlCommand();
			cmd.Connection = cn;
			cmd.Transaction = mTransaction;
			cmd.CommandType = System.Data.CommandType.Text;
			cmd.CommandText = SQL;
			
			cmd.Parameters.Add(prmContactID);
			cmd.Parameters.Add(prmProductGroupID);
			cmd.Parameters.Add(prmProductSubGroupID);
			cmd.Parameters.Add(prmProductID);

			myReader = (MySqlDataReader) cmd.ExecuteReader(System.Data.CommandBehavior.SingleResult);
			
			while (myReader.Read())
			{
				PromoType = (PromoTypes) Enum.Parse(typeof(PromoTypes), myReader.GetString("PromoTypeID"));
				PromoQuantity = myReader.GetDecimal("Quantity");
				PromoValue = myReader.GetDecimal("PromoValue");
				InPercent = myReader.GetBoolean("InPercent");
				myReader.Close();
				return boHasPromo;
			}
			myReader.Close();

			/*******************************Up to Contact, Sub, Prod ID only*****************************/
			SQL=	"SELECT " +
						"PromoItemsID, " + 
						"a.PromoID, " +
						"PromoTypeID, " +
						"ProductGroupID, " +
						"ProductSubGroupID, " +
						"ProductID,  " +
						"VariationMatrixID, " +
						"Quantity,  " +
						"PromoValue,  " +
						"InPercent  " +
					"FROM tblPromoItems a  " +
					"INNER JOIN tblPromo b ON a.PromoID = b.PromoID " +
					"WHERE ContactID = @ContactID " +
						"AND ProductGroupID = 0 " +
						"AND ProductSubGroupID = @ProductSubGroupID " +
						"AND ProductID = @ProductID " +
						"AND VariationMatrixID = 0 " +
						"AND Status = 1 " +
						"AND DATE_FORMAT(StartDate, '%Y-%m-%d %H:%i') <= DATE_FORMAT(NOW(), '%Y-%m-%d %H:%i') " +
						"AND DATE_FORMAT(EndDate, '%Y-%m-%d %H:%i') >= DATE_FORMAT(NOW(), '%Y-%m-%d %H:%i');";
				  
			cmd = new MySqlCommand();
			cmd.Connection = cn;
			cmd.Transaction = mTransaction;
			cmd.CommandType = System.Data.CommandType.Text;
			cmd.CommandText = SQL;
			
			cmd.Parameters.Add(prmContactID);
			cmd.Parameters.Add(prmProductSubGroupID);
			cmd.Parameters.Add(prmProductID);

			myReader = (MySqlDataReader) cmd.ExecuteReader(System.Data.CommandBehavior.SingleResult);
			
			while (myReader.Read())
			{
				PromoType = (PromoTypes) Enum.Parse(typeof(PromoTypes), myReader.GetString("PromoTypeID"));
				PromoQuantity = myReader.GetDecimal("Quantity");
				PromoValue = myReader.GetDecimal("PromoValue");
				InPercent = myReader.GetBoolean("InPercent");
				myReader.Close();
				return boHasPromo;
			}
			myReader.Close();

			/*******************************Up to Contact, Prod ID only*****************************/
			SQL=	"SELECT " +
						"PromoItemsID, " + 
						"a.PromoID, " +
						"PromoTypeID, " +
						"ProductGroupID, " +
						"ProductSubGroupID, " +
						"ProductID,  " +
						"VariationMatrixID, " +
						"Quantity,  " +
						"PromoValue,  " +
						"InPercent  " +
					"FROM tblPromoItems a  " +
					"INNER JOIN tblPromo b ON a.PromoID = b.PromoID " +
					"WHERE ContactID = @ContactID " +
						"AND ProductGroupID = 0 " +
						"AND ProductSubGroupID = 0 " +
						"AND ProductID = @ProductID " +
						"AND VariationMatrixID = 0 " +
						"AND Status = 1 " +
						"AND DATE_FORMAT(StartDate, '%Y-%m-%d %H:%i') <= DATE_FORMAT(NOW(), '%Y-%m-%d %H:%i') " +
						"AND DATE_FORMAT(EndDate, '%Y-%m-%d %H:%i') >= DATE_FORMAT(NOW(), '%Y-%m-%d %H:%i');";
				  
			cmd = new MySqlCommand();
			cmd.Connection = cn;
			cmd.Transaction = mTransaction;
			cmd.CommandType = System.Data.CommandType.Text;
			cmd.CommandText = SQL;
			
			cmd.Parameters.Add(prmContactID);
			cmd.Parameters.Add(prmProductID);

			myReader = (MySqlDataReader) cmd.ExecuteReader(System.Data.CommandBehavior.SingleResult);
			
			while (myReader.Read())
			{
				PromoType = (PromoTypes) Enum.Parse(typeof(PromoTypes), myReader.GetString("PromoTypeID"));
				PromoQuantity = myReader.GetDecimal("Quantity");
				PromoValue = myReader.GetDecimal("PromoValue");
				InPercent = myReader.GetBoolean("InPercent");
				myReader.Close();
				return boHasPromo;
			}
			myReader.Close();

			/*******************************Up to Contact, Group, Sub only*****************************/
			SQL=	"SELECT " +
						"PromoItemsID, " + 
						"a.PromoID, " +
						"PromoTypeID, " +
						"ProductGroupID, " +
						"ProductSubGroupID, " +
						"ProductID,  " +
						"VariationMatrixID, " +
						"Quantity,  " +
						"PromoValue,  " +
						"InPercent  " +
					"FROM tblPromoItems a  " +
					"INNER JOIN tblPromo b ON a.PromoID = b.PromoID " +
					"WHERE ContactID = @ContactID " +
						"AND ProductGroupID = @ProductGroupID " +
						"AND ProductSubGroupID = @ProductSubGroupID " +
						"AND ProductID = 0 " +
						"AND VariationMatrixID = 0 " +
						"AND Status = 1 " +
						"AND DATE_FORMAT(StartDate, '%Y-%m-%d %H:%i') <= DATE_FORMAT(NOW(), '%Y-%m-%d %H:%i') " +
						"AND DATE_FORMAT(EndDate, '%Y-%m-%d %H:%i') >= DATE_FORMAT(NOW(), '%Y-%m-%d %H:%i');";
				  
			cmd = new MySqlCommand();
			cmd.Connection = cn;
			cmd.Transaction = mTransaction;
			cmd.CommandType = System.Data.CommandType.Text;
			cmd.CommandText = SQL;
			
			cmd.Parameters.Add(prmContactID);
			cmd.Parameters.Add(prmProductGroupID);
			cmd.Parameters.Add(prmProductSubGroupID);

			myReader = (MySqlDataReader) cmd.ExecuteReader(System.Data.CommandBehavior.SingleResult);
			
			while (myReader.Read())
			{
				PromoType = (PromoTypes) Enum.Parse(typeof(PromoTypes), myReader.GetString("PromoTypeID"));
				PromoQuantity = myReader.GetDecimal("Quantity");
				PromoValue = myReader.GetDecimal("PromoValue");
				InPercent = myReader.GetBoolean("InPercent");
				myReader.Close();
				return boHasPromo;
			}
			myReader.Close();

			/*******************************Up to Contact, Sub only*****************************/
			SQL=	"SELECT " +
						"PromoItemsID, " + 
						"a.PromoID, " +
						"PromoTypeID, " +
						"ProductGroupID, " +
						"ProductSubGroupID, " +
						"ProductID,  " +
						"VariationMatrixID, " +
						"Quantity,  " +
						"PromoValue,  " +
						"InPercent  " +
					"FROM tblPromoItems a  " +
					"INNER JOIN tblPromo b ON a.PromoID = b.PromoID " +
					"WHERE ContactID = @ContactID " +
						"AND ProductGroupID = 0 " +
						"AND ProductSubGroupID = @ProductSubGroupID " +
						"AND ProductID = 0 " +
						"AND VariationMatrixID = 0 " +
						"AND Status = 1 " +
						"AND DATE_FORMAT(StartDate, '%Y-%m-%d %H:%i') <= DATE_FORMAT(NOW(), '%Y-%m-%d %H:%i') " +
						"AND DATE_FORMAT(EndDate, '%Y-%m-%d %H:%i') >= DATE_FORMAT(NOW(), '%Y-%m-%d %H:%i');";
				  
			cmd = new MySqlCommand();
			cmd.Connection = cn;
			cmd.Transaction = mTransaction;
			cmd.CommandType = System.Data.CommandType.Text;
			cmd.CommandText = SQL;
			
			cmd.Parameters.Add(prmContactID);
			cmd.Parameters.Add(prmProductSubGroupID);

			myReader = (MySqlDataReader) cmd.ExecuteReader(System.Data.CommandBehavior.SingleResult);
			
			while (myReader.Read())
			{
				PromoType = (PromoTypes) Enum.Parse(typeof(PromoTypes), myReader.GetString("PromoTypeID"));
				PromoQuantity = myReader.GetDecimal("Quantity");
				PromoValue = myReader.GetDecimal("PromoValue");
				InPercent = myReader.GetBoolean("InPercent");
				myReader.Close();
				return boHasPromo;
			}
			myReader.Close();

			/*******************************Up to Contact only*****************************/
			SQL=	"SELECT " +
						"PromoItemsID, " + 
						"a.PromoID, " +
						"PromoTypeID, " +
						"ProductGroupID, " +
						"ProductSubGroupID, " +
						"ProductID,  " +
						"VariationMatrixID, " +
						"Quantity,  " +
						"PromoValue,  " +
						"InPercent  " +
					"FROM tblPromoItems a  " +
					"INNER JOIN tblPromo b ON a.PromoID = b.PromoID " +
					"WHERE ContactID = @ContactID " +
						"AND ProductGroupID = 0 " +
						"AND ProductSubGroupID = 0 " +
						"AND ProductID = 0 " +
						"AND VariationMatrixID = 0 " +
						"AND Status = 1 " +
						"AND DATE_FORMAT(StartDate, '%Y-%m-%d %H:%i') <= DATE_FORMAT(NOW(), '%Y-%m-%d %H:%i') " +
						"AND DATE_FORMAT(EndDate, '%Y-%m-%d %H:%i') >= DATE_FORMAT(NOW(), '%Y-%m-%d %H:%i');";
				  
			cmd = new MySqlCommand();
			cmd.Connection = cn;
			cmd.Transaction = mTransaction;
			cmd.CommandType = System.Data.CommandType.Text;
			cmd.CommandText = SQL;
			
			cmd.Parameters.Add(prmContactID);

			myReader = (MySqlDataReader) cmd.ExecuteReader(System.Data.CommandBehavior.SingleResult);
			
			while (myReader.Read())
			{
				PromoType = (PromoTypes) Enum.Parse(typeof(PromoTypes), myReader.GetString("PromoTypeID"));
				PromoQuantity = myReader.GetDecimal("Quantity");
				PromoValue = myReader.GetDecimal("PromoValue");
				InPercent = myReader.GetBoolean("InPercent");
				myReader.Close();
				return boHasPromo;
			}
			myReader.Close();

			/*******************************Up to Group, Sub, Prod and VarM ID only*****************************/
			SQL=	"SELECT " +
						"PromoItemsID, " + 
						"a.PromoID, " +
						"PromoTypeID, " +
						"ProductGroupID, " +
						"ProductSubGroupID, " +
						"ProductID,  " +
						"VariationMatrixID, " +
						"Quantity,  " +
						"PromoValue,  " +
						"InPercent  " +
					"FROM tblPromoItems a  " +
					"INNER JOIN tblPromo b ON a.PromoID = b.PromoID " +
					"WHERE ContactID = 0 " +
						"AND ProductGroupID = @ProductGroupID " +
						"AND ProductSubGroupID = @ProductSubGroupID " +
						"AND ProductID = @ProductID " +
						"AND VariationMatrixID = @VariationMatrixID " +
						"AND Status = 1 " +
						"AND DATE_FORMAT(StartDate, '%Y-%m-%d %H:%i') <= DATE_FORMAT(NOW(), '%Y-%m-%d %H:%i') " +
						"AND DATE_FORMAT(EndDate, '%Y-%m-%d %H:%i') >= DATE_FORMAT(NOW(), '%Y-%m-%d %H:%i');";
				  
			cmd = new MySqlCommand();
			cmd.Connection = cn;
			cmd.Transaction = mTransaction;
			cmd.CommandType = System.Data.CommandType.Text;
			cmd.CommandText = SQL;
			
			cmd.Parameters.Add(prmProductGroupID);
			cmd.Parameters.Add(prmProductSubGroupID);
			cmd.Parameters.Add(prmProductID);
			cmd.Parameters.Add(prmVariationMatrixID);

			myReader = (MySqlDataReader) cmd.ExecuteReader(System.Data.CommandBehavior.SingleResult);
			
			while (myReader.Read())
			{
				PromoType = (PromoTypes) Enum.Parse(typeof(PromoTypes), myReader.GetString("PromoTypeID"));
				PromoQuantity = myReader.GetDecimal("Quantity");
				PromoValue = myReader.GetDecimal("PromoValue");
				InPercent = myReader.GetBoolean("InPercent");
				myReader.Close();
				return boHasPromo;
			}
			myReader.Close();

			/*******************************Up to Sub, Prod and VariationMatrix ID only*****************************/
			SQL=	"SELECT " +
						"PromoItemsID, " + 
						"a.PromoID, " +
						"PromoTypeID, " +
						"ProductGroupID, " +
						"ProductSubGroupID, " +
						"ProductID,  " +
						"VariationMatrixID, " +
						"Quantity,  " +
						"PromoValue,  " +
						"InPercent  " +
					"FROM tblPromoItems a  " +
					"INNER JOIN tblPromo b ON a.PromoID = b.PromoID " +
					"WHERE ContactID = 0 " +
						"AND ProductGroupID =0 " +
						"AND ProductSubGroupID = @ProductSubGroupID " +
						"AND ProductID = @ProductID " +
						"AND VariationMatrixID = @VariationMatrixID " +
						"AND Status = 1 " +
						"AND DATE_FORMAT(StartDate, '%Y-%m-%d %H:%i') <= DATE_FORMAT(NOW(), '%Y-%m-%d %H:%i') " +
						"AND DATE_FORMAT(EndDate, '%Y-%m-%d %H:%i') >= DATE_FORMAT(NOW(), '%Y-%m-%d %H:%i');";
				  
			cmd = new MySqlCommand();
			cmd.Connection = cn;
			cmd.Transaction = mTransaction;
			cmd.CommandType = System.Data.CommandType.Text;
			cmd.CommandText = SQL;
			
			cmd.Parameters.Add(prmProductSubGroupID);
			cmd.Parameters.Add(prmProductID);
			cmd.Parameters.Add(prmVariationMatrixID);

			myReader = (MySqlDataReader) cmd.ExecuteReader(System.Data.CommandBehavior.SingleResult);
			
			while (myReader.Read())
			{
				PromoType = (PromoTypes) Enum.Parse(typeof(PromoTypes), myReader.GetString("PromoTypeID"));
				PromoQuantity = myReader.GetDecimal("Quantity");
				PromoValue = myReader.GetDecimal("PromoValue");
				InPercent = myReader.GetBoolean("InPercent");
				myReader.Close();
				return boHasPromo;
			}
			myReader.Close();

			/*******************************Up to Prod and VariationMatrix ID only*****************************/
			SQL=	"SELECT " +
						"PromoItemsID, " + 
						"a.PromoID, " +
						"PromoTypeID, " +
						"ProductGroupID, " +
						"ProductSubGroupID, " +
						"ProductID,  " +
						"VariationMatrixID, " +
						"Quantity,  " +
						"PromoValue,  " +
						"InPercent  " +
					"FROM tblPromoItems a  " +
					"INNER JOIN tblPromo b ON a.PromoID = b.PromoID " +
					"WHERE ContactID = 0 " +
						"AND ProductGroupID = 0 " +
						"AND ProductSubGroupID = 0 " +
						"AND ProductID = @ProductID " +
						"AND VariationMatrixID = @VariationMatrixID " +
						"AND Status = 1 " +
						"AND DATE_FORMAT(StartDate, '%Y-%m-%d %H:%i') <= DATE_FORMAT(NOW(), '%Y-%m-%d %H:%i') " +
						"AND DATE_FORMAT(EndDate, '%Y-%m-%d %H:%i') >= DATE_FORMAT(NOW(), '%Y-%m-%d %H:%i');";
				  
			cmd = new MySqlCommand();
			cmd.Connection = cn;
			cmd.Transaction = mTransaction;
			cmd.CommandType = System.Data.CommandType.Text;
			cmd.CommandText = SQL;
			
			cmd.Parameters.Add(prmProductID);
			cmd.Parameters.Add(prmVariationMatrixID);

			myReader = (MySqlDataReader) cmd.ExecuteReader(System.Data.CommandBehavior.SingleResult);
			
			while (myReader.Read())
			{
				PromoType = (PromoTypes) Enum.Parse(typeof(PromoTypes), myReader.GetString("PromoTypeID"));
				PromoQuantity = myReader.GetDecimal("Quantity");
				PromoValue = myReader.GetDecimal("PromoValue");
				InPercent = myReader.GetBoolean("InPercent");
				myReader.Close();
				return boHasPromo;
			}
			myReader.Close();

			/*******************************Up to VariationsMatrix ID only*****************************/
			SQL=	"SELECT " +
						"PromoItemsID, " + 
						"a.PromoID, " +
						"PromoTypeID, " +
						"ProductGroupID, " +
						"ProductSubGroupID, " +
						"ProductID,  " +
						"VariationMatrixID, " +
						"Quantity,  " +
						"PromoValue,  " +
						"InPercent  " +
					"FROM tblPromoItems a  " +
					"INNER JOIN tblPromo b ON a.PromoID = b.PromoID " +
					"WHERE ContactID = 0 " +
						"AND ProductGroupID = 0 " +
						"AND ProductSubGroupID = 0 " +
						"AND ProductID = 0 " +
						"AND VariationMatrixID = @VariationMatrixID " +
						"AND Status = 1 " +
						"AND DATE_FORMAT(StartDate, '%Y-%m-%d %H:%i') <= DATE_FORMAT(NOW(), '%Y-%m-%d %H:%i') " +
						"AND DATE_FORMAT(EndDate, '%Y-%m-%d %H:%i') >= DATE_FORMAT(NOW(), '%Y-%m-%d %H:%i');";
				  
			cmd = new MySqlCommand();
			cmd.Connection = cn;
			cmd.Transaction = mTransaction;
			cmd.CommandType = System.Data.CommandType.Text;
			cmd.CommandText = SQL;
			
			cmd.Parameters.Add(prmVariationMatrixID);

			myReader = (MySqlDataReader) cmd.ExecuteReader(System.Data.CommandBehavior.SingleResult);
			
			while (myReader.Read())
			{
				PromoType = (PromoTypes) Enum.Parse(typeof(PromoTypes), myReader.GetString("PromoTypeID"));
				PromoQuantity = myReader.GetDecimal("Quantity");
				PromoValue = myReader.GetDecimal("PromoValue");
				InPercent = myReader.GetBoolean("InPercent");
				myReader.Close();
				return boHasPromo;
			}
			myReader.Close();

			/*******************************Up to group, Sub, Prod ID only*****************************/
			SQL=	"SELECT " +
						"PromoItemsID, " + 
						"a.PromoID, " +
						"PromoTypeID, " +
						"ProductGroupID, " +
						"ProductSubGroupID, " +
						"ProductID,  " +
						"VariationMatrixID, " +
						"Quantity,  " +
						"PromoValue,  " +
						"InPercent  " +
					"FROM tblPromoItems a  " +
					"INNER JOIN tblPromo b ON a.PromoID = b.PromoID " +
					"WHERE ContactID = 0 " +
						"AND ProductGroupID = @ProductGroupID " +
						"AND ProductSubGroupID = @ProductSubGroupID " +
						"AND ProductID = @ProductID " +
						"AND VariationMatrixID = 0 " +
						"AND Status = 1 " +
						"AND DATE_FORMAT(StartDate, '%Y-%m-%d %H:%i') <= DATE_FORMAT(NOW(), '%Y-%m-%d %H:%i') " +
						"AND DATE_FORMAT(EndDate, '%Y-%m-%d %H:%i') >= DATE_FORMAT(NOW(), '%Y-%m-%d %H:%i');";
				  
			cmd = new MySqlCommand();
			cmd.Connection = cn;
			cmd.Transaction = mTransaction;
			cmd.CommandType = System.Data.CommandType.Text;
			cmd.CommandText = SQL;
			
			cmd.Parameters.Add(prmProductGroupID);
			cmd.Parameters.Add(prmProductSubGroupID);
			cmd.Parameters.Add(prmProductID);

			myReader = (MySqlDataReader) cmd.ExecuteReader(System.Data.CommandBehavior.SingleResult);
			
			while (myReader.Read())
			{
				PromoType = (PromoTypes) Enum.Parse(typeof(PromoTypes), myReader.GetString("PromoTypeID"));
				PromoQuantity = myReader.GetDecimal("Quantity");
				PromoValue = myReader.GetDecimal("PromoValue");
				InPercent = myReader.GetBoolean("InPercent");
				myReader.Close();
				return boHasPromo;
			}
			myReader.Close();

			/*******************************Up to Sub, Prod ID only*****************************/
			SQL=	"SELECT " +
						"PromoItemsID, " + 
						"a.PromoID, " +
						"PromoTypeID, " +
						"ProductGroupID, " +
						"ProductSubGroupID, " +
						"ProductID,  " +
						"VariationMatrixID, " +
						"Quantity,  " +
						"PromoValue,  " +
						"InPercent  " +
					"FROM tblPromoItems a  " +
					"INNER JOIN tblPromo b ON a.PromoID = b.PromoID " +
					"WHERE ContactID = 0 " +
						"AND ProductGroupID = 0 " +
						"AND ProductSubGroupID = @ProductSubGroupID " +
						"AND ProductID = @ProductID " +
						"AND VariationMatrixID = 0 " +
						"AND Status = 1 " +
						"AND DATE_FORMAT(StartDate, '%Y-%m-%d %H:%i') <= DATE_FORMAT(NOW(), '%Y-%m-%d %H:%i') " +
						"AND DATE_FORMAT(EndDate, '%Y-%m-%d %H:%i') >= DATE_FORMAT(NOW(), '%Y-%m-%d %H:%i');";
				  
			cmd = new MySqlCommand();
			cmd.Connection = cn;
			cmd.Transaction = mTransaction;
			cmd.CommandType = System.Data.CommandType.Text;
			cmd.CommandText = SQL;
			
			cmd.Parameters.Add(prmProductSubGroupID);
			cmd.Parameters.Add(prmProductID);

			myReader = (MySqlDataReader) cmd.ExecuteReader(System.Data.CommandBehavior.SingleResult);
			
			while (myReader.Read())
			{
				PromoType = (PromoTypes) Enum.Parse(typeof(PromoTypes), myReader.GetString("PromoTypeID"));
				PromoQuantity = myReader.GetDecimal("Quantity");
				PromoValue = myReader.GetDecimal("PromoValue");
				InPercent = myReader.GetBoolean("InPercent");
				myReader.Close();
				return boHasPromo;
			}
			myReader.Close();

			/*******************************Up to group, Sub, Prod and VariationMatrix ID only*****************************/
			SQL=	"SELECT " +
						"PromoItemsID, " + 
						"a.PromoID, " +
						"PromoTypeID, " +
						"ProductGroupID, " +
						"ProductSubGroupID, " +
						"ProductID,  " +
						"VariationMatrixID, " +
						"Quantity,  " +
						"PromoValue,  " +
						"InPercent  " +
					"FROM tblPromoItems a  " +
					"INNER JOIN tblPromo b ON a.PromoID = b.PromoID " +
					"WHERE ContactID = 0 " +
						"AND ProductGroupID = 0 " +
						"AND ProductSubGroupID = 0 " +
						"AND ProductID = @ProductID " +
						"AND VariationMatrixID = 0 " +
						"AND Status = 1 " +
						"AND DATE_FORMAT(StartDate, '%Y-%m-%d %H:%i') <= DATE_FORMAT(NOW(), '%Y-%m-%d %H:%i') " +
						"AND DATE_FORMAT(EndDate, '%Y-%m-%d %H:%i') >= DATE_FORMAT(NOW(), '%Y-%m-%d %H:%i');";
				  
			cmd = new MySqlCommand();
			cmd.Connection = cn;
			cmd.Transaction = mTransaction;
			cmd.CommandType = System.Data.CommandType.Text;
			cmd.CommandText = SQL;
			
			cmd.Parameters.Add(prmProductID);

			myReader = (MySqlDataReader) cmd.ExecuteReader(System.Data.CommandBehavior.SingleResult);
			
			while (myReader.Read())
			{
				PromoType = (PromoTypes) Enum.Parse(typeof(PromoTypes), myReader.GetString("PromoTypeID"));
				PromoQuantity = myReader.GetDecimal("Quantity");
				PromoValue = myReader.GetDecimal("PromoValue");
				InPercent = myReader.GetBoolean("InPercent");
				myReader.Close();
				return boHasPromo;
			}
			myReader.Close();

			/*******************************Up to group, Sub ID only*****************************/
			SQL=	"SELECT " +
						"PromoItemsID, " + 
						"a.PromoID, " +
						"PromoTypeID, " +
						"ProductGroupID, " +
						"ProductSubGroupID, " +
						"ProductID,  " +
						"VariationMatrixID, " +
						"Quantity,  " +
						"PromoValue,  " +
						"InPercent  " +
					"FROM tblPromoItems a  " +
					"INNER JOIN tblPromo b ON a.PromoID = b.PromoID " +
					"WHERE ContactID = 0 " +
						"AND ProductGroupID = @ProductGroupID " +
						"AND ProductSubGroupID = @ProductSubGroupID " +
						"AND ProductID = 0 " +
						"AND VariationMatrixID = 0 " +
						"AND Status = 1 " +
						"AND DATE_FORMAT(StartDate, '%Y-%m-%d %H:%i') <= DATE_FORMAT(NOW(), '%Y-%m-%d %H:%i') " +
						"AND DATE_FORMAT(EndDate, '%Y-%m-%d %H:%i') >= DATE_FORMAT(NOW(), '%Y-%m-%d %H:%i');";
				  
			cmd = new MySqlCommand();
			cmd.Connection = cn;
			cmd.Transaction = mTransaction;
			cmd.CommandType = System.Data.CommandType.Text;
			cmd.CommandText = SQL;
			
			cmd.Parameters.Add(prmProductGroupID);
			cmd.Parameters.Add(prmProductSubGroupID);

			myReader = (MySqlDataReader) cmd.ExecuteReader(System.Data.CommandBehavior.SingleResult);
			
			while (myReader.Read())
			{
				PromoType = (PromoTypes) Enum.Parse(typeof(PromoTypes), myReader.GetString("PromoTypeID"));
				PromoQuantity = myReader.GetDecimal("Quantity");
				PromoValue = myReader.GetDecimal("PromoValue");
				InPercent = myReader.GetBoolean("InPercent");
				myReader.Close();
				return boHasPromo;
			}
			myReader.Close();

			/*******************************Up to Sub ID only*****************************/
			SQL=	"SELECT " +
						"PromoItemsID, " + 
						"a.PromoID, " +
						"PromoTypeID, " +
						"ProductGroupID, " +
						"ProductSubGroupID, " +
						"ProductID,  " +
						"VariationMatrixID, " +
						"Quantity,  " +
						"PromoValue,  " +
						"InPercent  " +
					"FROM tblPromoItems a  " +
					"INNER JOIN tblPromo b ON a.PromoID = b.PromoID " +
					"WHERE ContactID = 0 " +
						"AND ProductGroupID = 0 " +
						"AND ProductSubGroupID = @ProductSubGroupID " +
						"AND ProductID = 0 " +
						"AND VariationMatrixID = 0 " +
						"AND Status = 1 " +
						"AND DATE_FORMAT(StartDate, '%Y-%m-%d %H:%i') <= DATE_FORMAT(NOW(), '%Y-%m-%d %H:%i') " +
						"AND DATE_FORMAT(EndDate, '%Y-%m-%d %H:%i') >= DATE_FORMAT(NOW(), '%Y-%m-%d %H:%i');";
				  
			cmd = new MySqlCommand();
			cmd.Connection = cn;
			cmd.Transaction = mTransaction;
			cmd.CommandType = System.Data.CommandType.Text;
			cmd.CommandText = SQL;
			
			cmd.Parameters.Add(prmProductGroupID);
			cmd.Parameters.Add(prmProductSubGroupID);
			cmd.Parameters.Add(prmProductID);
			cmd.Parameters.Add(prmVariationMatrixID);

			myReader = (MySqlDataReader) cmd.ExecuteReader(System.Data.CommandBehavior.SingleResult);
			
			while (myReader.Read())
			{
				PromoType = (PromoTypes) Enum.Parse(typeof(PromoTypes), myReader.GetString("PromoTypeID"));
				PromoQuantity = myReader.GetDecimal("Quantity");
				PromoValue = myReader.GetDecimal("PromoValue");
				InPercent = myReader.GetBoolean("InPercent");
				myReader.Close();
				return boHasPromo;
			}
			myReader.Close();

			/*******************************Up to group ID only*****************************/
			SQL=	"SELECT " +
						"PromoItemsID, " + 
						"a.PromoID, " +
						"PromoTypeID, " +
						"ProductGroupID, " +
						"ProductSubGroupID, " +
						"ProductID,  " +
						"VariationMatrixID, " +
						"Quantity,  " +
						"PromoValue,  " +
						"InPercent  " +
					"FROM tblPromoItems a  " +
					"INNER JOIN tblPromo b ON a.PromoID = b.PromoID " +
					"WHERE ContactID = 0 " +
						"AND ProductGroupID = @ProductGroupID " +
						"AND ProductSubGroupID = 0 " +
						"AND ProductID = 0 " +
						"AND VariationMatrixID = 0 " +
						"AND Status = 1 " +
						"AND DATE_FORMAT(StartDate, '%Y-%m-%d %H:%i') <= DATE_FORMAT(NOW(), '%Y-%m-%d %H:%i') " +
						"AND DATE_FORMAT(EndDate, '%Y-%m-%d %H:%i') >= DATE_FORMAT(NOW(), '%Y-%m-%d %H:%i');";
				  
			cmd = new MySqlCommand();
			cmd.Connection = cn;
			cmd.Transaction = mTransaction;
			cmd.CommandType = System.Data.CommandType.Text;
			cmd.CommandText = SQL;
			
			cmd.Parameters.Add(prmProductGroupID);
			cmd.Parameters.Add(prmProductSubGroupID);
			cmd.Parameters.Add(prmProductID);
			cmd.Parameters.Add(prmVariationMatrixID);

			myReader = (MySqlDataReader) cmd.ExecuteReader(System.Data.CommandBehavior.SingleResult);
			
			while (myReader.Read())
			{
				PromoType = (PromoTypes) Enum.Parse(typeof(PromoTypes), myReader.GetString("PromoTypeID"));
				PromoQuantity = myReader.GetDecimal("Quantity");
				PromoValue = myReader.GetDecimal("PromoValue");
				InPercent = myReader.GetBoolean("InPercent");
				myReader.Close();
				return boHasPromo;
			}
			myReader.Close();

			/*******************************Up to all only*****************************/
			SQL=	"SELECT " +
						"PromoItemsID, " + 
						"a.PromoID, " +
						"PromoTypeID, " +
						"ProductGroupID, " +
						"ProductSubGroupID, " +
						"ProductID,  " +
						"VariationMatrixID, " +
						"Quantity,  " +
						"PromoValue,  " +
						"InPercent  " +
					"FROM tblPromoItems a  " +
					"INNER JOIN tblPromo b ON a.PromoID = b.PromoID " +
					"WHERE ContactID = 0 " +
						"AND ProductGroupID = 0 " +
						"AND ProductSubGroupID = 0 " +
						"AND ProductID = 0 " +
						"AND VariationMatrixID = 0 " +
						"AND Status = 1 " +
						"AND DATE_FORMAT(StartDate, '%Y-%m-%d %H:%i') <= DATE_FORMAT(NOW(), '%Y-%m-%d %H:%i') " +
						"AND DATE_FORMAT(EndDate, '%Y-%m-%d %H:%i') >= DATE_FORMAT(NOW(), '%Y-%m-%d %H:%i');";
				  
			cmd = new MySqlCommand();
			cmd.Connection = cn;
			cmd.Transaction = mTransaction;
			cmd.CommandType = System.Data.CommandType.Text;
			cmd.CommandText = SQL;
			
			cmd.Parameters.Add(prmProductGroupID);
			cmd.Parameters.Add(prmProductSubGroupID);
			cmd.Parameters.Add(prmProductID);
			cmd.Parameters.Add(prmVariationMatrixID);

			myReader = (MySqlDataReader) cmd.ExecuteReader(System.Data.CommandBehavior.SingleResult);
			
			while (myReader.Read())
			{
                PromoType = (PromoTypes)Enum.Parse(typeof(PromoTypes), myReader.GetString("PromoTypeID"));
				PromoQuantity = myReader.GetDecimal("Quantity");
				PromoValue = myReader.GetDecimal("PromoValue");
				InPercent = myReader.GetBoolean("InPercent");
				myReader.Close();
				return boHasPromo;
			}
			myReader.Close();

			return false;
		}
		
		private decimal ApplyPromoValue(PromoTypes PromoType, decimal Amount, decimal Quantity, decimal PromoQuantity, decimal PromoValue, bool InPercent, decimal AppliedQuantity, out bool IsPromoApplied)
		{
			IsPromoApplied = false;

			decimal AddedQuantity = 0;

			if (AppliedQuantity != 0)
			{
				Quantity = (Quantity - AppliedQuantity);
				if ((int)(AppliedQuantity % PromoQuantity) != 0 && Quantity < PromoQuantity)
				{
					AddedQuantity = (int) (AppliedQuantity % PromoQuantity);
					Quantity += (int) (AppliedQuantity % PromoQuantity);
				}
			}
			decimal decRetValue = Amount;
			decimal Price = Amount / (Quantity - AddedQuantity);
			decimal ApplicableQuantity = (int)(Quantity / PromoQuantity) * PromoQuantity;
			decimal UnApplicableQuantity = Quantity - ApplicableQuantity;
			if (Quantity - AddedQuantity < PromoQuantity)
				UnApplicableQuantity = 0;

			Amount = ApplicableQuantity * Price;

			switch (PromoType)
			{
				case PromoTypes.ValueOffAfterQtyReached:
					if (InPercent == false && Quantity >= PromoQuantity)
					{
						decRetValue = (UnApplicableQuantity * Price);
						decRetValue += Amount - PromoValue;
						if (AddedQuantity != 0)
							decRetValue -= Price;

						IsPromoApplied = true;
					}
					break;
				case PromoTypes.PercentOffAfterQtyReached:
					if (InPercent == true && Quantity >= PromoQuantity )
					{
						decRetValue = (UnApplicableQuantity * Price);
						decRetValue += Amount - (Amount * PromoValue / 100); 
						if (AddedQuantity != 0)
							decRetValue -= AddedQuantity * Price;

						IsPromoApplied = true;
					}
					break;
			}
			
			return decRetValue;
		}

		#endregion
	}
}


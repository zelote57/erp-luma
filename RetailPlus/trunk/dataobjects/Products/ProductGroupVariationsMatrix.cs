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
	#region Struct

	public struct ProductGroupVariationsMatrixDetails
	{
        public Int64 ProductGroupVariationsMatrixID;
		public Int64 MatrixID;
		public Int64 GroupID;
		public Int32 VariationID;
		public string VariationType;
		public string Description;

        public DateTime CreatedOn;
        public DateTime LastModified;
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
	public class ProductGroupVariationsMatrix : POSConnection
    {
		#region Constructors and Destructors

		public ProductGroupVariationsMatrix()
            : base(null, null)
        {
        }

        public ProductGroupVariationsMatrix(MySqlConnection Connection, MySqlTransaction Transaction) 
            : base(Connection, Transaction)
		{

		}

		#endregion

		#region Insert and Update

        public Int64 InsertBaseVariation(ProductGroupBaseVariationsMatrixDetails Details)
		{
			try 
			{
                ProductGroupBaseVariationsMatrix clsProductGroupBaseVariationsMatrix = new ProductGroupBaseVariationsMatrix(base.Connection, base.Transaction);
                return clsProductGroupBaseVariationsMatrix.Insert(Details);
			}
			catch (Exception ex)
			{
				throw base.ThrowException(ex);
			}	
		}

		public bool InsertVariation(ProductGroupVariationsMatrixDetails Details)
		{
			try 
			{
				string SQL = "INSERT INTO tblProductGroupVariationsMatrix (MatrixID, VariationID, Description) VALUES (@MatrixID, @VariationID, @Description);";
				  
				MySqlCommand cmd = new MySqlCommand();
				cmd.CommandType = System.Data.CommandType.Text;
				cmd.CommandText = SQL;

                cmd.Parameters.AddWithValue("@MatrixID", Details.MatrixID);
                cmd.Parameters.AddWithValue("@VariationID", Details.VariationID);
                cmd.Parameters.AddWithValue("@Description", Details.Description);

				base.ExecuteNonQuery(cmd);
				
				return true;
			}

			catch (Exception ex)
			{
				throw base.ThrowException(ex);
			}	
		}

        public bool UpdateBaseVariation(ProductGroupBaseVariationsMatrixDetails Details)
		{
			try 
			{
                ProductGroupBaseVariationsMatrix clsProductGroupBaseVariationsMatrix = new ProductGroupBaseVariationsMatrix(base.Connection, base.Transaction);
                clsProductGroupBaseVariationsMatrix.Update(Details);

                return true;
			}

			catch (Exception ex)
			{
				throw base.ThrowException(ex);
			}	
		}

        public bool UpdateVariationDesc(ProductGroupBaseVariationsMatrixDetails Details)
		{
			try 
			{
                ProductGroupBaseVariationsMatrix clsProductGroupBaseVariationsMatrix = new ProductGroupBaseVariationsMatrix(base.Connection, base.Transaction);
                clsProductGroupBaseVariationsMatrix.UpdateVariationDesc(Details);
				
				return true;
			}

			catch (Exception ex)
			{
				throw base.ThrowException(ex);
			}	
		}

		public void UpdateVariation(ProductGroupVariationsMatrixDetails Details)
		{
			try 
			{
				string SQL=	"UPDATE tblProductGroupVariationsMatrix SET " + 
								"Description = @Description " +  
							"WHERE MatrixID = @MatrixID " + 
								"AND VariationID = @VariationID;";
				  
				MySqlCommand cmd = new MySqlCommand();
				cmd.CommandType = System.Data.CommandType.Text;
				cmd.CommandText = SQL;

                cmd.Parameters.AddWithValue("@Description", Details.Description);
                cmd.Parameters.AddWithValue("@MatrixID", Details.MatrixID);
                cmd.Parameters.AddWithValue("@VariationID", Details.VariationID);

				base.ExecuteNonQuery(cmd);
			}

			catch (Exception ex)
			{
				throw base.ThrowException(ex);
			}	
		}

        public Int32 Save(ProductGroupVariationsMatrixDetails Details)
        {
            try
            {
                string SQL = "CALL procSaveProductGroupVariationsMatrix(@ProductGroupVariationsMatrixID, @MatrixID, @VariationID, @Description, @CreatedOn, @LastModified);";

                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandText = SQL;

                cmd.Parameters.AddWithValue("ProductGroupVariationsMatrixID", Details.ProductGroupVariationsMatrixID);
                cmd.Parameters.AddWithValue("MatrixID", Details.MatrixID);
                cmd.Parameters.AddWithValue("VariationID", Details.VariationID);
                cmd.Parameters.AddWithValue("Description", Details.Description);
                cmd.Parameters.AddWithValue("CreatedOn", Details.CreatedOn == DateTime.MinValue ? Constants.C_DATE_MIN_VALUE : Details.CreatedOn);
                cmd.Parameters.AddWithValue("LastModified", Details.LastModified == DateTime.MinValue ? Constants.C_DATE_MIN_VALUE : Details.LastModified);

                return base.ExecuteNonQuery(cmd);
            }

            catch (Exception ex)
            {
                throw base.ThrowException(ex);
            }
        }

        //// Dec 10, 2011 : Obsolete, change to ChangeTax
        //public void ChangeVAT(decimal OldVAT, decimal NewVAT)
        //{
        //    try 
        //    {
        //        string SQL =	"UPDATE tblProductGroupBaseVariationsMatrix SET " +
        //                            "VAT		= @NewVAT " +
        //                        "WHERE VAT		= @OldVAT;";
				  
        //        
	 			
        //        MySqlCommand cmd = new MySqlCommand();
        //        
        //        
        //        cmd.CommandType = System.Data.CommandType.Text;
        //        cmd.CommandText = SQL;
				
        //        MySqlParameter prmNewVAT = new MySqlParameter("@NewVAT",MySqlDbType.Decimal);			
        //        prmNewVAT.Value = NewVAT;
        //        cmd.Parameters.Add(prmNewVAT);

        //        MySqlParameter prmOldVAT = new MySqlParameter("@OldVAT",MySqlDbType.Decimal);			
        //        prmOldVAT.Value = OldVAT;
        //        cmd.Parameters.Add(prmOldVAT);

        //        base.ExecuteNonQuery(cmd);
        //    }

        //    catch (Exception ex)
        //    {
        //        
        //        
        //        {
        //            
        //            
        //            
        //            
        //        }

        //        throw base.ThrowException(ex);
        //    }	
        //}
        //// Dec 10, 2011 : Obsolete, change to ChangeTax
        //public void ChangeEVAT(decimal OldEVAT, decimal NewEVAT)
        //{
        //    try 
        //    {
        //        string SQL =	"UPDATE tblProductGroupBaseVariationsMatrix SET " +
        //            "EVAT		= @NewEVAT " +
        //            "WHERE EVAT		= @OldEVAT;";
				  
        //        
	 			
        //        MySqlCommand cmd = new MySqlCommand();
        //        
        //        
        //        cmd.CommandType = System.Data.CommandType.Text;
        //        cmd.CommandText = SQL;
				
        //        MySqlParameter prmNewEVAT = new MySqlParameter("@NewEVAT",MySqlDbType.Decimal);			
        //        prmNewEVAT.Value = NewEVAT;
        //        cmd.Parameters.Add(prmNewEVAT);

        //        MySqlParameter prmOldEVAT = new MySqlParameter("@OldEVAT",MySqlDbType.Decimal);			
        //        prmOldEVAT.Value = OldEVAT;
        //        cmd.Parameters.Add(prmOldEVAT);

        //        base.ExecuteNonQuery(cmd);
        //    }

        //    catch (Exception ex)
        //    {
        //        
        //        
        //        {
        //            
        //            
        //            
        //            
        //        }

        //        throw base.ThrowException(ex);
        //    }	
        //}
        //// Dec 10, 2011 : Obsolete, change to ChangeTax
        //public void ChangeLocalTax(decimal OldLocalTax, decimal NewLocalTax)
        //{
        //    try 
        //    {
        //        string SQL =	"UPDATE tblProductGroupBaseVariationsMatrix SET " +
        //                            "LocalTax		= @NewLocalTax " +
        //                        "WHERE LocalTax		= @OldLocalTax;";
				  
        //        
	 			
        //        MySqlCommand cmd = new MySqlCommand();
        //        
        //        
        //        cmd.CommandType = System.Data.CommandType.Text;
        //        cmd.CommandText = SQL;
				
        //        MySqlParameter prmNewLocalTax = new MySqlParameter("@NewLocalTax",MySqlDbType.Decimal);			
        //        prmNewLocalTax.Value = NewLocalTax;
        //        cmd.Parameters.Add(prmNewLocalTax);

        //        MySqlParameter prmOldLocalTax = new MySqlParameter("@OldLocalTax",MySqlDbType.Decimal);			
        //        prmOldLocalTax.Value = OldLocalTax;
        //        cmd.Parameters.Add(prmOldLocalTax);

        //        base.ExecuteNonQuery(cmd);
        //    }

        //    catch (Exception ex)
        //    {
        //        
        //        
        //        {
        //            
        //            
        //            
        //            
        //        }

        //        throw base.ThrowException(ex);
        //    }	
        //}

        public void ChangeTax(decimal NewVAT, decimal NewEVAT, decimal NewLocalTax)
        {
            try
            {
                string SQL = "UPDATE tblProductGroupBaseVariationsMatrix SET " +
                                    "VAT		= @NewVAT, " +
                                    "EVAT		= @NewEVAT, " +
                                    "LocalTax	= @NewLocalTax ";

                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandText = SQL;

                cmd.Parameters.AddWithValue("@NewVAT", NewVAT);
                cmd.Parameters.AddWithValue("@NewEVAT", NewEVAT);
                cmd.Parameters.AddWithValue("@NewLocalTax", NewLocalTax);

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
				string SQL=	"DELETE FROM tblProductGroupVariationsMatrix WHERE MatrixID IN (" + IDs + ");";
	 			
				MySqlCommand cmd = new MySqlCommand();

				cmd.CommandType = System.Data.CommandType.Text;
				cmd.CommandText = SQL;
				base.ExecuteNonQuery(cmd);


				SQL = "DELETE FROM tblProductGroupBaseVariationsMatrix WHERE MatrixID IN (" + IDs + ");";

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

		#region Details

		public ProductGroupVariationsMatrixDetails Details(Int64 MatrixID, Int32 VariationID)
		{
			try
			{
				string SQL=	"SELECT * FROM tblProductGroupVariationsMatrix " +
					"WHERE MatrixID = @MatrixID AND VariationID = @VariationID;";
				  
				
	 			
				MySqlCommand cmd = new MySqlCommand();
				
				
				cmd.CommandType = System.Data.CommandType.Text;
				cmd.CommandText = SQL;

				MySqlParameter prmMatrixID = new MySqlParameter("@MatrixID",MySqlDbType.Int64);			
				prmMatrixID.Value = MatrixID;
				cmd.Parameters.Add(prmMatrixID);

				MySqlParameter prmVariationID = new MySqlParameter("@VariationID",MySqlDbType.Int32);
				prmVariationID.Value = VariationID;
				cmd.Parameters.Add(prmVariationID);

				MySqlDataReader myReader = base.ExecuteReader(cmd, System.Data.CommandBehavior.SingleResult);
				
				ProductGroupVariationsMatrixDetails Details = new ProductGroupVariationsMatrixDetails();

				while (myReader.Read()) 
				{
					Details.MatrixID = MatrixID;
					Details.VariationID = VariationID;
					Details.Description = myReader.GetString(2);
				}

				myReader.Close();

				return Details;
			}

			catch (Exception ex)
			{
				
				
				{
					
					
					
					
				}

				throw base.ThrowException(ex);
			}	
		}

		public ProductGroupBaseVariationsMatrixDetails BaseDetails(Int64 MatrixID, Int64 GroupID)
		{
			try
			{
                ProductGroupBaseVariationsMatrix clsProductGroupBaseVariationsMatrix = new ProductGroupBaseVariationsMatrix(base.Connection, base.Transaction);
                return clsProductGroupBaseVariationsMatrix.BaseDetails(MatrixID, GroupID);
			}

			catch (Exception ex)
			{
				throw base.ThrowException(ex);
			}	
		}

		
		#endregion

		#region Streams

		public MySqlDataReader List(Int64 ProductGroupBaseVariationsMatrixID)
		{
			try 
			{
				string SQL=	"SELECT MatrixID, VariationID, Description FROM tblProductGroupVariationsMatrix " + 
					"WHERE MatrixID = @MatrixID ORDER BY MatrixID ASC;";

				

				MySqlCommand cmd = new MySqlCommand();
				
				
				cmd.CommandType = System.Data.CommandType.Text;
				cmd.CommandText = SQL;
				
				MySqlParameter prmMatrixID = new MySqlParameter("@MatrixID",MySqlDbType.Int64);						
				prmMatrixID.Value = ProductGroupBaseVariationsMatrixID;
				cmd.Parameters.Add(prmMatrixID);

				
				
				return base.ExecuteReader(cmd);	
			}

			catch (Exception ex)
			{
				
				
				{
					
					
					
					
				}

				throw base.ThrowException(ex);
			}	
		}

		public MySqlDataReader BaseList(Int64 GroupID, string SortField, SortOption SortOrder)
		{
			try
			{
                ProductGroupBaseVariationsMatrix clsProductGroupBaseVariationsMatrix = new ProductGroupBaseVariationsMatrix(base.Connection, base.Transaction);

                return clsProductGroupBaseVariationsMatrix.BaseList(GroupID, SortField, SortOrder);
			}
			catch (Exception ex)
			{
				throw base.ThrowException(ex);
			}	
		}

		public MySqlDataReader Search(Int64 GroupID, string SearchKey, string SortField, SortOption SortOrder)
		{
			try
			{
				string SQL = "SELECT MatrixID, " +
								"GroupID, " + 
								"Description, " + 
								"a.UnitID, " + 
								"UnitName, " + 
								"a.Price, " +
								"a.PurchasePrice, " +
								"a.IncludeInSubtotalDiscount, " +
								"a.VAT, " +
								"a.EVAT, " +
								"a.LocalTax " +
							"FROM tblProductGroupBaseVariationsMatrix a INNER JOIN " +
							"tblUnit b ON a.UnitID = b.UnitID " +	
							"WHERE GroupID = @GroupID  " + 
							"AND Description LIKE @SearchKey " + 
							"ORDER BY MatrixID, " + SortField;

				if (SortOrder == SortOption.Ascending)
					SQL += " ASC";
				else
					SQL += " DESC";

				

				MySqlCommand cmd = new MySqlCommand();
				
				
				cmd.CommandType = System.Data.CommandType.Text;
				cmd.CommandText = SQL;
				
				MySqlParameter prmGroupID = new MySqlParameter("@GroupID",MySqlDbType.Int64);			
				prmGroupID.Value = GroupID;
				cmd.Parameters.Add(prmGroupID);

				MySqlParameter prmSearchKey = new MySqlParameter("@SearchKey",MySqlDbType.String);			
				prmSearchKey.Value = "%" + SearchKey + "%";
				cmd.Parameters.Add(prmSearchKey);

				
				
				return base.ExecuteReader(cmd);
			}
			catch (Exception ex)
			{
				
				
				{
					
					
					
					
				}

				throw base.ThrowException(ex);
			}	
		}


		#endregion

		#region Public Modifiers

		public bool IsVariationExists(Int64 MatrixID, Int32 VariationID)
		{
			try 
			{
				bool boRetValue = false;
					
				string SQL=	"SELECT * FROM tblProductGroupVariationsMatrix " + 
					        "WHERE MatrixID = @MatrixID " + 
					        "AND VariationID = @VariationID;";

				MySqlCommand cmd = new MySqlCommand();
				cmd.CommandType = System.Data.CommandType.Text;
				cmd.CommandText = SQL;
				base.ExecuteNonQuery(cmd);

				MySqlParameter prmMatrixID = new MySqlParameter("@MatrixID",MySqlDbType.Int64);						
				prmMatrixID.Value = MatrixID;
				cmd.Parameters.Add(prmMatrixID);

				MySqlParameter prmVariationID = new MySqlParameter("@VariationID",MySqlDbType.Int32);			
				prmVariationID.Value = VariationID;
				cmd.Parameters.Add(prmVariationID);

				MySqlDataReader myReader = base.ExecuteReader(cmd, System.Data.CommandBehavior.SingleResult);
				
				while (myReader.Read()) 
				{
					boRetValue = true;
				}

				myReader.Close();

				return boRetValue;
			}

			catch (Exception ex)
			{
				throw base.ThrowException(ex);
			}	
		}


		#endregion
	}
}


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

    public struct ProductGroupBaseVariationsMatrixDetails
	{
		public Int64 MatrixID;
		public Int64 GroupID;
		public string Description;
		public Int32 UnitID;
		public string UnitName;
		public decimal Price;
		public decimal PurchasePrice;
		public bool IncludeInSubtotalDiscount;
		public decimal VAT;
		public decimal EVAT;
		public decimal LocalTax;

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
	public class ProductGroupBaseVariationsMatrix : POSConnection
    {
		#region Constructors and Destructors

		public ProductGroupBaseVariationsMatrix()
            : base(null, null)
        {
        }

        public ProductGroupBaseVariationsMatrix(MySqlConnection Connection, MySqlTransaction Transaction) 
            : base(Connection, Transaction)
		{

		}

		#endregion

		#region Insert and Update

        public Int64 Insert(ProductGroupBaseVariationsMatrixDetails Details)
		{
			try 
			{
                Save(Details);

                string SQL = "SELECT LAST_INSERT_ID();";
				  
				MySqlCommand cmd = new MySqlCommand();
				cmd.CommandType = System.Data.CommandType.Text;
				cmd.CommandText = SQL;

				System.Data.DataTable dt = new System.Data.DataTable("LAST_INSERT_ID");
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

        public bool Update(ProductGroupBaseVariationsMatrixDetails Details)
		{
			try 
			{
				Save(Details);
				
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
				string SQL = "UPDATE tblProductGroupBaseVariationsMatrix SET " +
								"Description = @Description " +
							"WHERE GroupID = @GroupID AND MatrixID = @MatrixID;";
				
	 			
				MySqlCommand cmd = new MySqlCommand();
				cmd.CommandType = System.Data.CommandType.Text;
				cmd.CommandText = SQL;

                cmd.Parameters.AddWithValue("@GroupID", Details.GroupID);
                cmd.Parameters.AddWithValue("@MatrixID", Details.MatrixID);
                cmd.Parameters.AddWithValue("@Description", Details.Description);

				base.ExecuteNonQuery(cmd);
				
				return true;
			}

			catch (Exception ex)
			{
				throw base.ThrowException(ex);
			}	
		}

        public Int32 Save(ProductGroupBaseVariationsMatrixDetails Details)
        {
            try
            {
                string SQL = "CALL procSaveProductGroupBaseVariationsMatrix(@MatrixID, @GroupID, @Description, @UnitID," +
                                            "@Price, @PurchasePrice, @IncludeInSubtotalDiscount," +
                                            "@VAT, @EVAT, @LocalTax, @CreatedOn, @LastModified);";

                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandText = SQL;

                cmd.Parameters.AddWithValue("MatrixID", Details.MatrixID);
                cmd.Parameters.AddWithValue("GroupID", Details.GroupID);
                cmd.Parameters.AddWithValue("Description", Details.Description);
                cmd.Parameters.AddWithValue("UnitID", Details.UnitID);
                cmd.Parameters.AddWithValue("Price", Details.Price);
                cmd.Parameters.AddWithValue("PurchasePrice", Details.PurchasePrice);
                cmd.Parameters.AddWithValue("IncludeInSubtotalDiscount", Details.IncludeInSubtotalDiscount);
                cmd.Parameters.AddWithValue("VAT", Details.VAT);
                cmd.Parameters.AddWithValue("EVAT", Details.EVAT);
                cmd.Parameters.AddWithValue("LocalTax", Details.LocalTax);
                cmd.Parameters.AddWithValue("CreatedOn", Details.CreatedOn == DateTime.MinValue ? Constants.C_DATE_MIN_VALUE : Details.CreatedOn);
                cmd.Parameters.AddWithValue("LastModified", Details.LastModified == DateTime.MinValue ? Constants.C_DATE_MIN_VALUE : Details.LastModified);

                return base.ExecuteNonQuery(cmd);
            }
            catch (Exception ex)
            {
                throw base.ThrowException(ex);
            }
        }

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

                MySqlParameter prmNewVAT = new MySqlParameter("@NewVAT",MySqlDbType.Decimal);
                prmNewVAT.Value = NewVAT;
                cmd.Parameters.Add(prmNewVAT);

                MySqlParameter prmNewEVAT = new MySqlParameter("@NewEVAT",MySqlDbType.Decimal);
                prmNewEVAT.Value = NewEVAT;
                cmd.Parameters.Add(prmNewEVAT);

                MySqlParameter prmNewLocalTax = new MySqlParameter("@NewLocalTax",MySqlDbType.Decimal);
                prmNewLocalTax.Value = NewLocalTax;
                cmd.Parameters.Add(prmNewLocalTax);

                base.ExecuteNonQuery(cmd);

            }

            catch (Exception ex)
            {
                throw base.ThrowException(ex);
            }
        }

		#endregion

		#region Delete

        /// <summary>
        /// ID = MatrixID's
        /// GroupID = GroupID's
        /// </summary>
        /// <param name="IDs"></param>
        /// <param name="GroupID"></param>
        /// <returns></returns>
		public bool Delete(string IDs, string GroupIDs = "")
		{
			try 
			{
				string SQL=	"DELETE FROM tblProductGroupVariationsMatrix WHERE MatrixID IN (" + IDs + ") ";
			    
                if (!string.IsNullOrEmpty(GroupIDs)) SQL += "AND GroupID IN (" + GroupIDs + ") ";

                SQL += ";";

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

		#region Details

		public ProductGroupBaseVariationsMatrixDetails BaseDetails(Int64 MatrixID, Int64 GroupID)
		{
			try
			{
				string SQL=	"SELECT " +
								"MatrixID, " +
								"GroupID, " +
								"description, " +
								"a.UnitID, " +
								"b.UnitName, " +
								"a.Price, " +
								"a.PurchasePrice, " +
								"a.IncludeInSubtotalDiscount, " +
								"a.VAT, " +
								"a.EVAT, " +
								"a.LocalTax " +
							"FROM tblProductGroupBaseVariationsMatrix a INNER JOIN " +
							"tblUnit b ON a.UnitID = b.UnitID " +
							"WHERE MatrixID = @MatrixID AND GroupID = @GroupID;"; 
	 			
				MySqlCommand cmd = new MySqlCommand();
				cmd.CommandType = System.Data.CommandType.Text;
				cmd.CommandText = SQL;

				MySqlParameter prmMatrixID = new MySqlParameter("@MatrixID",MySqlDbType.Int64);			
				prmMatrixID.Value = MatrixID;
				cmd.Parameters.Add(prmMatrixID);

				MySqlParameter prmGroupID = new MySqlParameter("@GroupID",MySqlDbType.Int64);			
				prmGroupID.Value = GroupID;
				cmd.Parameters.Add(prmGroupID);

				MySqlDataReader myReader = base.ExecuteReader(cmd, System.Data.CommandBehavior.SingleResult);
				
				ProductGroupBaseVariationsMatrixDetails Details = new ProductGroupBaseVariationsMatrixDetails();

				while (myReader.Read()) 
				{
					Details.MatrixID =myReader.GetInt64("MatrixID");
					Details.GroupID = myReader.GetInt64("GroupID");
					Details.Description = "" + myReader["Description"].ToString();
					Details.UnitID = myReader.GetInt32("UnitID");
					Details.UnitName = "" + myReader["UnitName"].ToString();
					Details.Price = myReader.GetDecimal("Price");
					Details.PurchasePrice = myReader.GetDecimal("PurchasePrice");
					Details.IncludeInSubtotalDiscount = myReader.GetBoolean("IncludeInSubtotalDiscount");
					Details.VAT = myReader.GetDecimal("VAT");
					Details.EVAT = myReader.GetDecimal("EVAT");
					Details.LocalTax = myReader.GetDecimal("LocalTax");
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

		public MySqlDataReader BaseList(Int64 GroupID, string SortField, SortOption SortOrder)
		{
			try
			{
				string SQL =	"SELECT MatrixID, " +
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
								"WHERE GroupID = @GroupID ORDER BY MatrixID, " + SortField;

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



		#endregion
	}
}


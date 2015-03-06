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

    public struct ProductBaseVariationsMatrixDetails
	{
        public Int64 MatrixID;
        public Int64 ProductID;
        public string Description;
        public Int32 UnitID;
        public string UnitCode;
        public string UnitName;

        public string BarCode1;
        public string BarCode2;
        public string BarCode3;
        public decimal Price;
        public decimal Price1;
        public decimal Price2;
        public decimal Price3;
        public decimal Price4;
        public decimal Price5;
        public decimal WSPrice;
        public decimal PurchasePrice;
        public bool IncludeInSubtotalDiscount;
        public decimal VAT;
        public decimal EVAT;
        public decimal LocalTax;
        public decimal Quantity;
        public decimal ActualQuantity;
        public decimal MinThreshold;
        public decimal MaxThreshold;
        public decimal RIDMinThreshold;
        public decimal RIDMaxThreshold;
        public Int64 SupplierID;
        public Int64 UpdatedBy;
        public bool Deleted;
        public DateTime UpdatedOn;
        public decimal QuantityIN;
        public decimal QuantityOUT;
        public string CreatedBy;

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
	public class ProductBaseVariationsMatrix : POSConnection
    {
		#region Constructors and Destructors

		public ProductBaseVariationsMatrix()
            : base(null, null)
        {
        }

        public ProductBaseVariationsMatrix(MySqlConnection Connection, MySqlTransaction Transaction) 
            : base(Connection, Transaction)
		{

		}

		#endregion

		#region Insert and Update

        public Int64 Insert(ProductBaseVariationsMatrixDetails Details)
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

        public bool Update(ProductBaseVariationsMatrixDetails Details)
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

        public bool UpdateVariationDesc(ProductBaseVariationsMatrixDetails Details)
		{
			try 
			{
				string SQL = "UPDATE tblProductBaseVariationsMatrix SET " +
								"Description = @Description " +
							"WHERE ProductID = @ProductID AND MatrixID = @MatrixID;";
				
	 			
				MySqlCommand cmd = new MySqlCommand();
				cmd.CommandType = System.Data.CommandType.Text;
				cmd.CommandText = SQL;

                cmd.Parameters.AddWithValue("@ProductID", Details.ProductID);
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

        public Int32 Save(ProductBaseVariationsMatrixDetails Details)
        {
            try
            {
                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandType = System.Data.CommandType.Text;

                string SQL = "CALL procSaveProductBaseVariationsMatrix(@MatrixID, @ProductID, @Description, @UnitID, " +
                                "@IncludeInSubtotalDiscount, @MinThreshold, @MaxThreshold, @SupplierID, " +
                                "@RIDMinThreshold, @RIDMaxThreshold, @Deleted, @CreatedOn, @LastModified);";

                cmd.Parameters.AddWithValue("MatrixID", Details.MatrixID);
                cmd.Parameters.AddWithValue("ProductID", Details.ProductID);
                cmd.Parameters.AddWithValue("Description", Details.Description);
                cmd.Parameters.AddWithValue("UnitID", Details.UnitID);
                cmd.Parameters.AddWithValue("IncludeInSubtotalDiscount", Details.IncludeInSubtotalDiscount);
                cmd.Parameters.AddWithValue("MinThreshold", Details.MinThreshold);
                cmd.Parameters.AddWithValue("MaxThreshold", Details.MaxThreshold);
                cmd.Parameters.AddWithValue("SupplierID", Details.SupplierID);
                cmd.Parameters.AddWithValue("RIDMinThreshold", Details.RIDMinThreshold);
                cmd.Parameters.AddWithValue("RIDMaxThreshold", Details.RIDMaxThreshold);
                cmd.Parameters.AddWithValue("Deleted", Details.Deleted);
                cmd.Parameters.AddWithValue("CreatedOn", Details.CreatedOn == DateTime.MinValue ? Constants.C_DATE_MIN_VALUE : Details.CreatedOn);
                cmd.Parameters.AddWithValue("LastModified", Details.LastModified == DateTime.MinValue ? Constants.C_DATE_MIN_VALUE : Details.LastModified);

                cmd.CommandText = SQL;
                return base.ExecuteNonQuery(cmd);
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
        /// ProductID = ProductID's
        /// </summary>
        /// <param name="IDs"></param>
        /// <param name="ProductID"></param>
        /// <returns></returns>
		public bool Delete(string IDs, string ProductIDs = "")
		{
			try 
			{
                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandType = System.Data.CommandType.Text;

				string SQL=	"DELETE FROM tblProductBaseVariationsMatrix WHERE MatrixID IN (" + IDs + ") ";
			    
                if (!string.IsNullOrEmpty(ProductIDs)) SQL += "AND ProductID IN (" + ProductIDs + ") ";

                SQL += ";";
				
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

		public ProductBaseVariationsMatrixDetails BaseDetails(Int64 MatrixID, Int64 ProductID)
		{
			try
			{
                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandType = System.Data.CommandType.Text;

				string SQL=	"SELECT " +
								"MatrixID, " +
								"ProductID, " +
								"description, " +
								"a.UnitID, " +
								"b.UnitName, " +
								"a.IncludeInSubtotalDiscount " +
							"FROM tblProductBaseVariationsMatrix a INNER JOIN " +
							"tblUnit b ON a.UnitID = b.UnitID " +
							"WHERE MatrixID = @MatrixID AND ProductID = @ProductID;"; 
	 			
				
                cmd.Parameters.AddWithValue("@MatrixID", MatrixID);
                cmd.Parameters.AddWithValue("@ProductID", ProductID);
				
                cmd.CommandText = SQL;
                string strDataTableName = "tbl" + this.GetType().FullName.Split(new Char[] { '.' })[this.GetType().FullName.Split(new Char[] { '.' }).Length - 1]; System.Data.DataTable dt = new System.Data.DataTable(strDataTableName);
                base.MySqlDataAdapterFill(cmd, dt);

                ProductBaseVariationsMatrixDetails Details = new ProductBaseVariationsMatrixDetails();
                foreach (System.Data.DataRow dr in dt.Rows)
				{
					Details.MatrixID = Int64.Parse(dr["MatrixID"].ToString());
                    Details.ProductID = Int64.Parse(dr["ProductID"].ToString());
					Details.Description = dr["Description"].ToString();
					Details.UnitID = Int32.Parse(dr["UnitID"].ToString());
					Details.UnitName = dr["UnitName"].ToString();
					Details.IncludeInSubtotalDiscount = bool.Parse(dr["IncludeInSubtotalDiscount"].ToString());
				}

				return Details;
			}
			catch (Exception ex)
			{
				throw base.ThrowException(ex);
			}	
		}

		
		#endregion

		#region Streams

		public System.Data.DataTable BaseList(Int64 ProductID, string SortField, SortOption SortOrder, Int32 limit = 0)
		{
			try
			{
                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandType = System.Data.CommandType.Text;

				string SQL =	"SELECT MatrixID, " +
									"ProductID, " + 
									"Description, " + 
									"a.UnitID, " + 
									"UnitName, " + 
									"a.IncludeInSubtotalDiscount " +
								"FROM tblProductBaseVariationsMatrix a INNER JOIN " +
								"tblUnit b ON a.UnitID = b.UnitID " +	
								"WHERE ProductID = @ProductID ORDER BY MatrixID, " + SortField;

				if (SortOrder == SortOption.Ascending)
					SQL += " ASC";
				else
					SQL += " DESC";

                SQL += "ORDER BY " + SortField + " ";
                SQL += SortOrder == SortOption.Ascending ? "ASC " : "DESC ";
                SQL += limit == 0 ? "" : "LIMIT " + limit.ToString() + " ";

                cmd.Parameters.AddWithValue("@ProductID", ProductID);

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



		#endregion
	}
}


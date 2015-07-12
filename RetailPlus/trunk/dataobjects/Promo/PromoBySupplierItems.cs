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
	public struct PromoBySupplierItemsDetails
	{
		public Int64 PromoBySupplierItemsID;
		public Int64 PromoBySupplierID;
		public Int64 ContactID;
		public Int64 ProductGroupID;
		public Int64 ProductSubGroupID;
		public Int64 ProductID;
		public Int64 VariationMatrixID;
		public decimal PromoBySupplierValue;
		public string CouponRemarks;

        public DateTime CreatedOn;
        public DateTime LastModified;
	}

	[StrongNameIdentityPermissionAttribute(SecurityAction.LinkDemand,
		 PublicKey = "002400000480000094000000060200000024000" +
		 "052534131000400000100010053D785642F9F960B43157E0380" +
		 "F393BEE53E8DFAFBF441366C1B6F8B48D9DDF0D527B1F3B21EA" +
		 "E85D2FDB664CE81EB8A87DBE4C589D6F4202FE2B7D4B978BB69" +
		 "684874612CB9B8DB7A0339400A9C4E68277884B07817363D242" +
		 "E3696F9FACDBEA831810AE6DC9EDCA91A7B5DA12FE7BF65D113" +
		 "FF52834EAFB5A7A1FDFD5851A3")]
	public class PromoBySupplierItems : POSConnection
    {
		#region Constructors and Destructors

		public PromoBySupplierItems()
            : base(null, null)
        {
        }

        public PromoBySupplierItems(MySqlConnection Connection, MySqlTransaction Transaction) 
            : base(Connection, Transaction)
		{

		}

		#endregion

		#region Insert and Update

		public Int64 Insert(PromoBySupplierItemsDetails Details)
		{
			try 
			{
                Save(Details);

                return Int64.Parse(base.getLAST_INSERT_ID(this));
			}
			catch (Exception ex)
			{
				throw base.ThrowException(ex);
			}	
		}

		public void Update(PromoBySupplierItemsDetails Details)
		{
			try 
			{
                Save(Details);
			}
			catch (Exception ex)
			{
				throw base.ThrowException(ex);
			}	
		}

        public Int32 Save(PromoBySupplierItemsDetails Details)
        {
            try
            {
                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandType = System.Data.CommandType.Text;

                string SQL = "CALL procSavePromoBySupplierItems(@PromoBySupplierItemsID, @PromoBySupplierID, @ContactID, @ProductGroupID," +
                                                "@ProductSubGroupID, @ProductID, @VariationMatrixID," +
                                                "@PromoBySupplierValue, @CouponRemarks, @CreatedOn, @LastModified);";

                cmd.Parameters.AddWithValue("PromoBySupplierItemsID", Details.PromoBySupplierItemsID);
                cmd.Parameters.AddWithValue("PromoBySupplierID", Details.PromoBySupplierID);
                cmd.Parameters.AddWithValue("ContactID", Details.ContactID);
                cmd.Parameters.AddWithValue("ProductGroupID", Details.ProductGroupID);
                cmd.Parameters.AddWithValue("ProductSubGroupID", Details.ProductSubGroupID);
                cmd.Parameters.AddWithValue("ProductID", Details.ProductID);
                cmd.Parameters.AddWithValue("VariationMatrixID", Details.VariationMatrixID);
                cmd.Parameters.AddWithValue("PromoBySupplierValue", Details.PromoBySupplierValue);
                cmd.Parameters.AddWithValue("CouponRemarks", Details.CouponRemarks);
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

		public bool Delete(string IDs)
		{
			try 
			{

                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandType = System.Data.CommandType.Text;

				string SQL=	"DELETE FROM tblPromoBySupplierItems WHERE PromoBySupplierItemsID IN (" + IDs + ");";
				
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

		public PromoBySupplierItemsDetails DetailsByPromoBySupplierItemsID(Int64 PromoBySupplierItemsID)
		{
			try
			{
                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandType = System.Data.CommandType.Text;
				
				string SQL=	"SELECT " +
								"PromoBySupplierItemsID, " +
								"PromoBySupplierID, " +
								"ContactID, " +
								"ProductGroupID, " +
								"ProductSubGroupID, " +
								"ProductID, " +
								"VariationMatrixID, " +
								"PromoBySupplierValue, " +
								"CouponRemarks " +
							"FROM tblPromoBySupplierItems " +
							"WHERE PromoBySupplierItemsID = @PromoBySupplierItemsID;";

                cmd.Parameters.AddWithValue("@PromoBySupplierItemsID", PromoBySupplierItemsID);

                cmd.CommandText = SQL;
                string strDataTableName = "tbl" + this.GetType().FullName.Split(new Char[] { '.' })[this.GetType().FullName.Split(new Char[] { '.' }).Length - 1]; System.Data.DataTable dt = new System.Data.DataTable(strDataTableName);
                base.MySqlDataAdapterFill(cmd, dt);

				return setDetails(dt);
			}
			catch (Exception ex)
			{
				throw base.ThrowException(ex);
			}	
		}

		public PromoBySupplierItemsDetails DetailsByProductID(Int64 ProductID)
		{
			try
			{
                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandType = System.Data.CommandType.Text;
				
				string SQL = "SELECT " +
							    "PromoBySupplierItemsID, " +
							    "a.PromoBySupplierID, " +
							    "ContactID, " +
							    "ProductGroupID, " +
							    "ProductSubGroupID, " +
							    "ProductID, " +
							    "VariationMatrixID, " +
							    "Quantity, " +
							    "PromoBySupplierValue, " +
							    "InPercent " +
						    "FROM tblPromoBySupplierItems a " + 
						        "INNER JOIN tblPromoBySupplier b ON a.PromoBySupplierID = b.PromoBySupplierID " +
						    "WHERE ProductID = @ProductID " + 
						        "AND DATE_FORMAT(StartDate, '%Y-%m-%d %H:%i') >= CURRENT_DATE " +
						        "AND DATE_FORMAT(EndDate, '%Y-%m-%d %H:%i') <= CURRENT_DATE;";
	 			
				cmd.Parameters.AddWithValue("@ProductID", ProductID);

                cmd.CommandText = SQL;
                string strDataTableName = "tbl" + this.GetType().FullName.Split(new Char[] { '.' })[this.GetType().FullName.Split(new Char[] { '.' }).Length - 1]; System.Data.DataTable dt = new System.Data.DataTable(strDataTableName);
                base.MySqlDataAdapterFill(cmd, dt);

                return setDetails(dt);
			}
			catch (Exception ex)
			{
				throw base.ThrowException(ex);
			}	
		}

        private PromoBySupplierItemsDetails setDetails(System.Data.DataTable dt)
        {
            PromoBySupplierItemsDetails Details = new PromoBySupplierItemsDetails();
            foreach (System.Data.DataRow dr in dt.Rows)
            {
                Details.PromoBySupplierItemsID = Int64.Parse(dr["PromoBySupplierItemsID"].ToString());
                Details.PromoBySupplierID = Int64.Parse(dr["PromoBySupplierID"].ToString());
                Details.ContactID = Int64.Parse(dr["ContactID"].ToString());
                Details.ProductGroupID = Int64.Parse(dr["ProductGroupID"].ToString());
                Details.ProductSubGroupID = Int64.Parse(dr["ProductSubGroupID"].ToString());
                Details.ProductID = Int64.Parse(dr["ProductID"].ToString());
                Details.VariationMatrixID = Int64.Parse(dr["VariationMatrixID"].ToString());
                Details.PromoBySupplierValue = Decimal.Parse(dr["PromoBySupplierValue"].ToString());
                Details.CouponRemarks = dr["CouponRemarks"].ToString();
            }
            return Details;
        }

		#endregion

		#region Streams

        public System.Data.DataTable ListAsDataTable(Int64 PromoBySupplierID, string SearchKey = null, string SortField = "PromoBySupplierItemsID", SortOption SortOrder = SortOption.Ascending, Int32 limit = 0)
		{
			try
			{
                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandType = System.Data.CommandType.Text;

                string SQL = "SELECT " +
                                    "PromoBySupplierItemsID, " +
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
                                    "a.PromoBySupplierValue, " +
                                    "a.CouponRemarks " +
                            "FROM tblPromoBySupplierItems a " +
                                "LEFT OUTER JOIN tblContacts b ON a.ContactID = b.ContactID " +
                                "LEFT OUTER JOIN tblProductGroup c ON a.ProductGroupID = c.ProductGroupID L" +
                                "EFT OUTER JOIN tblProductSubGroup d ON a.ProductSubGroupID = d.ProductSubGroupID " +
                                "LEFT OUTER JOIN tblProducts e ON a.ProductID = e.ProductID " +
                                "LEFT OUTER JOIN tblProductBaseVariationsMatrix f ON a.VariationMatrixID = f.MatrixID " +
                            "WHERE PromoBySupplierID = @PromoBySupplierID ";

                if (!string.IsNullOrEmpty(SearchKey))
                {
                    SQL += "AND (PromoBySupplierItemsCode LIKE @SearchKey " +
                                    "OR PromoBySupplierItemsName LIKE @SearchKey " +
                                    "OR PromoBySupplierItemsTypeCode LIKE @SearchKey " +
                                    "OR PromoBySupplierItemsTypeName LIKE @SearchKey) ";
                    cmd.Parameters.AddWithValue("@SearchKey", SearchKey);
                }

                SQL += "ORDER BY " + (!string.IsNullOrEmpty(SortField) ? SortField : "PromoBySupplierItemsID") + " ";
                SQL += SortOrder == SortOption.Ascending ? "ASC " : "DESC ";
                SQL += limit == 0 ? "" : "LIMIT " + limit.ToString() + " ";

                cmd.Parameters.AddWithValue("@PromoBySupplierID", PromoBySupplierID);

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


		public bool ApplyPromoBySupplierValue(ContactDetails clsContactDetails, Int64 ProductID, Int64 VariationMatrixID, out PromoTypes PromoBySupplierType, out decimal PromoBySupplierQuantity, out decimal PromoBySupplierValue, out bool InPercent, int BranchID = 0)
		{
            Int64 ContactID = clsContactDetails.ContactID;

            string strSpecialSupplierIDs = "";

            // rewardcardmember
            // make sure that it is not the default
            if (clsContactDetails.ContactID != Constants.C_RETAILPLUS_CUSTOMERID &&
                clsContactDetails.RewardDetails.RewardActive)
            {
                if (!string.IsNullOrEmpty(strSpecialSupplierIDs))
                    strSpecialSupplierIDs += "," + Constants.PLUSCARDMEMBERSID_STRING;
                else
                    strSpecialSupplierIDs = Constants.PLUSCARDMEMBERSID_STRING;
            }

            // icc card members
            // make sure that it is not the default
            if (clsContactDetails.ContactID != Constants.C_RETAILPLUS_CUSTOMERID && 
                clsContactDetails.CreditDetails.GuarantorID == Constants.ZERO &&
                clsContactDetails.CreditDetails.CreditActive)
            {
                if (!string.IsNullOrEmpty(strSpecialSupplierIDs))
                    strSpecialSupplierIDs += "," + Constants.ICCARDMEMBERSID_STRING;
                else
                    strSpecialSupplierIDs = Constants.ICCARDMEMBERSID_STRING;
            }

            // gcc card members
            // make sure that it is not the default
            if (clsContactDetails.ContactID != Constants.C_RETAILPLUS_CUSTOMERID && 
                clsContactDetails.CreditDetails.GuarantorID != Constants.ZERO &&
                clsContactDetails.CreditDetails.CreditActive)
            {
                if (!string.IsNullOrEmpty(strSpecialSupplierIDs))
                    strSpecialSupplierIDs += "," + Constants.GCCARDMEMBERSID_STRING;
                else
                    strSpecialSupplierIDs = Constants.GCCARDMEMBERSID_STRING;
            }
            

            PromoBySupplierType = PromoTypes.NotApplicable;
            PromoBySupplierQuantity = 0;
            PromoBySupplierValue = 0;
            InPercent = false;

            bool boHasPromoBySupplier = false;

            try
            {
                Data.Products clsProduct = new Data.Products(base.Connection, base.Transaction);
                Data.ProductDetails clsProductDetails = clsProduct.Details1(BranchID, ProductID);

                Int64 ProductSubGroupID = clsProductDetails.ProductSubGroupID;
                Int64 ProductGroupID = clsProductDetails.ProductGroupID;

                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandType = System.Data.CommandType.Text;

                string SQL = "SELECT " +
                                "PromoBySupplierID  " +
                            "FROM tblPromoBySupplier " +
                            "WHERE 1=1 " +
                                "AND Status = 1 " +
                                "AND DATE_FORMAT(StartDate, '%Y-%m-%d %H:%i') <= DATE_FORMAT(NOW(), '%Y-%m-%d %H:%i') " +
                                "AND DATE_FORMAT(EndDate, '%Y-%m-%d %H:%i') >= DATE_FORMAT(NOW(), '%Y-%m-%d %H:%i');";

                cmd.CommandText = SQL;
                string strDataTableName = "tbl" + this.GetType().FullName.Split(new Char[] { '.' })[this.GetType().FullName.Split(new Char[] { '.' }).Length - 1]; System.Data.DataTable dt = new System.Data.DataTable(strDataTableName);
                base.MySqlDataAdapterFill(cmd, dt);

                foreach (System.Data.DataRow dr in dt.Rows)
                {
                    boHasPromoBySupplier = true;
                    break;
                }

                if (boHasPromoBySupplier == false)	//return agad if no PromoBySupplier is affected by date
                    return boHasPromoBySupplier;

                /*******************************Up to Contact, Group, Sub, Prod and VarM ID only*****************************/
                SQL = "SELECT " +
                            "PromoBySupplierItemsID, " +
                            "a.PromoBySupplierID, " +
                            "PromoBySupplierTypeID, " +
                            "ProductGroupID, " +
                            "ProductSubGroupID, " +
                            "ProductID,  " +
                            "VariationMatrixID, " +
                            "Quantity,  " +
                            "PromoBySupplierValue,  " +
                            "InPercent  " +
                        "FROM tblPromoBySupplierItems a  " +
                        "INNER JOIN tblPromoBySupplier b ON a.PromoBySupplierID = b.PromoBySupplierID " +
                        "WHERE " + (string.IsNullOrEmpty(strSpecialSupplierIDs) ? "ContactID = @ContactID " : "ContactID IN (@ContactID, " + strSpecialSupplierIDs + ") ") +
                            "AND ProductGroupID = @ProductGroupID " +
                            "AND ProductSubGroupID = @ProductSubGroupID " +
                            "AND ProductID = @ProductID " +
                            "AND VariationMatrixID = @VariationMatrixID " +
                            "AND Status = 1 " +
                            "AND DATE_FORMAT(StartDate, '%Y-%m-%d %H:%i') <= DATE_FORMAT(NOW(), '%Y-%m-%d %H:%i') " +
                            "AND DATE_FORMAT(EndDate, '%Y-%m-%d %H:%i') >= DATE_FORMAT(NOW(), '%Y-%m-%d %H:%i') ORDER BY PromoBySupplierValue ASC LIMIT 1;";

                cmd = new MySqlCommand();
                cmd.CommandType = System.Data.CommandType.Text;

                cmd.Parameters.AddWithValue("@ContactID", ContactID);
                cmd.Parameters.AddWithValue("@ProductGroupID", ProductGroupID);
                cmd.Parameters.AddWithValue("@ProductSubGroupID", ProductSubGroupID);
                cmd.Parameters.AddWithValue("@ProductID", ProductID);
                cmd.Parameters.AddWithValue("@VariationMatrixID", VariationMatrixID);

                cmd.CommandText = SQL;
                dt = new System.Data.DataTable(strDataTableName);
                base.MySqlDataAdapterFill(cmd, dt);

                foreach (System.Data.DataRow dr in dt.Rows)
                {
                    PromoBySupplierType = (PromoTypes)Enum.Parse(typeof(PromoTypes), dr["PromoBySupplierTypeID"].ToString());
                    PromoBySupplierQuantity = decimal.Parse(dr["Quantity"].ToString());
                    PromoBySupplierValue = decimal.Parse(dr["PromoBySupplierValue"].ToString());
                    InPercent = bool.Parse(dr["InPercent"].ToString());
                    return boHasPromoBySupplier;
                }

                /*******************************Up to Contact, Sub, Prod and VariationsMatrix ID only*****************************/
                SQL = "SELECT " +
                            "PromoBySupplierItemsID, " +
                            "a.PromoBySupplierID, " +
                            "PromoBySupplierTypeID, " +
                            "ProductGroupID, " +
                            "ProductSubGroupID, " +
                            "ProductID,  " +
                            "VariationMatrixID, " +
                            "Quantity,  " +
                            "PromoBySupplierValue,  " +
                            "InPercent  " +
                        "FROM tblPromoBySupplierItems a  " +
                        "INNER JOIN tblPromoBySupplier b ON a.PromoBySupplierID = b.PromoBySupplierID " +
                        "WHERE " + (string.IsNullOrEmpty(strSpecialSupplierIDs) ? "ContactID = @ContactID " : "ContactID IN (@ContactID, " + strSpecialSupplierIDs + ") ") +
                            "AND ProductGroupID = 0 " +
                            "AND ProductSubGroupID = @ProductSubGroupID " +
                            "AND ProductID = @ProductID " +
                            "AND VariationMatrixID = @VariationMatrixID " +
                            "AND Status = 1 " +
                            "AND DATE_FORMAT(StartDate, '%Y-%m-%d %H:%i') <= DATE_FORMAT(NOW(), '%Y-%m-%d %H:%i') " +
                            "AND DATE_FORMAT(EndDate, '%Y-%m-%d %H:%i') >= DATE_FORMAT(NOW(), '%Y-%m-%d %H:%i') ORDER BY PromoBySupplierValue ASC LIMIT 1;";

                cmd = new MySqlCommand();
                cmd.CommandType = System.Data.CommandType.Text;

                cmd.Parameters.AddWithValue("@ContactID", ContactID);
                cmd.Parameters.AddWithValue("@ProductSubGroupID", ProductSubGroupID);
                cmd.Parameters.AddWithValue("@ProductID", ProductID);
                cmd.Parameters.AddWithValue("@VariationMatrixID", VariationMatrixID);

                cmd.CommandText = SQL;
                dt = new System.Data.DataTable(strDataTableName);
                base.MySqlDataAdapterFill(cmd, dt);

                foreach (System.Data.DataRow dr in dt.Rows)
                {
                    PromoBySupplierType = (PromoTypes)Enum.Parse(typeof(PromoTypes), dr["PromoBySupplierTypeID"].ToString());
                    PromoBySupplierQuantity = decimal.Parse(dr["Quantity"].ToString());
                    PromoBySupplierValue = decimal.Parse(dr["PromoBySupplierValue"].ToString());
                    InPercent = bool.Parse(dr["InPercent"].ToString());
                    return boHasPromoBySupplier;
                }

                /*******************************Up to Contact, Prod and VariationsMatrix ID only*****************************/
                SQL = "SELECT " +
                            "PromoBySupplierItemsID, " +
                            "a.PromoBySupplierID, " +
                            "PromoBySupplierTypeID, " +
                            "ProductGroupID, " +
                            "ProductSubGroupID, " +
                            "ProductID,  " +
                            "VariationMatrixID, " +
                            "Quantity,  " +
                            "PromoBySupplierValue,  " +
                            "InPercent  " +
                        "FROM tblPromoBySupplierItems a  " +
                        "INNER JOIN tblPromoBySupplier b ON a.PromoBySupplierID = b.PromoBySupplierID " +
                        "WHERE " + (string.IsNullOrEmpty(strSpecialSupplierIDs) ? "ContactID = @ContactID " : "ContactID IN (@ContactID, " + strSpecialSupplierIDs + ") ") +
                            "AND ProductGroupID = 0 " +
                            "AND ProductSubGroupID = 0 " +
                            "AND ProductID = @ProductID " +
                            "AND VariationMatrixID = @VariationMatrixID " +
                            "AND Status = 1 " +
                            "AND DATE_FORMAT(StartDate, '%Y-%m-%d %H:%i') <= DATE_FORMAT(NOW(), '%Y-%m-%d %H:%i') " +
                            "AND DATE_FORMAT(EndDate, '%Y-%m-%d %H:%i') >= DATE_FORMAT(NOW(), '%Y-%m-%d %H:%i') ORDER BY PromoBySupplierValue ASC LIMIT 1;";

                cmd = new MySqlCommand();
                cmd.CommandType = System.Data.CommandType.Text;

                cmd.Parameters.AddWithValue("@ContactID", ContactID);
                cmd.Parameters.AddWithValue("@ProductID", ProductID);
                cmd.Parameters.AddWithValue("@VariationMatrixID", VariationMatrixID);

                cmd.CommandText = SQL;
                dt = new System.Data.DataTable(strDataTableName);
                base.MySqlDataAdapterFill(cmd, dt);

                foreach (System.Data.DataRow dr in dt.Rows)
                {
                    PromoBySupplierType = (PromoTypes)Enum.Parse(typeof(PromoTypes), dr["PromoBySupplierTypeID"].ToString());
                    PromoBySupplierQuantity = decimal.Parse(dr["Quantity"].ToString());
                    PromoBySupplierValue = decimal.Parse(dr["PromoBySupplierValue"].ToString());
                    InPercent = bool.Parse(dr["InPercent"].ToString());
                    return boHasPromoBySupplier;
                }

                /*******************************Up to Contact, VariationsMatrix ID only*****************************/
                SQL = "SELECT " +
                            "PromoBySupplierItemsID, " +
                            "a.PromoBySupplierID, " +
                            "PromoBySupplierTypeID, " +
                            "ProductGroupID, " +
                            "ProductSubGroupID, " +
                            "ProductID,  " +
                            "VariationMatrixID, " +
                            "Quantity,  " +
                            "PromoBySupplierValue,  " +
                            "InPercent  " +
                        "FROM tblPromoBySupplierItems a  " +
                        "INNER JOIN tblPromoBySupplier b ON a.PromoBySupplierID = b.PromoBySupplierID " +
                        "WHERE " + (string.IsNullOrEmpty(strSpecialSupplierIDs) ? "ContactID = @ContactID " : "ContactID IN (@ContactID, " + strSpecialSupplierIDs + ") ") +
                            "AND ProductGroupID = 0 " +
                            "AND ProductSubGroupID = 0 " +
                            "AND ProductID = 0 " +
                            "AND VariationMatrixID = @VariationMatrixID " +
                            "AND Status = 1 " +
                            "AND DATE_FORMAT(StartDate, '%Y-%m-%d %H:%i') <= DATE_FORMAT(NOW(), '%Y-%m-%d %H:%i') " +
                            "AND DATE_FORMAT(EndDate, '%Y-%m-%d %H:%i') >= DATE_FORMAT(NOW(), '%Y-%m-%d %H:%i') ORDER BY PromoBySupplierValue ASC LIMIT 1;";

                cmd = new MySqlCommand();
                cmd.CommandType = System.Data.CommandType.Text;

                cmd.Parameters.AddWithValue("@ContactID", ContactID);
                cmd.Parameters.AddWithValue("@VariationMatrixID", VariationMatrixID);

                cmd.CommandText = SQL;
                dt = new System.Data.DataTable(strDataTableName);
                base.MySqlDataAdapterFill(cmd, dt);

                foreach (System.Data.DataRow dr in dt.Rows)
                {
                    PromoBySupplierType = (PromoTypes)Enum.Parse(typeof(PromoTypes), dr["PromoBySupplierTypeID"].ToString());
                    PromoBySupplierQuantity = decimal.Parse(dr["Quantity"].ToString());
                    PromoBySupplierValue = decimal.Parse(dr["PromoBySupplierValue"].ToString());
                    InPercent = bool.Parse(dr["InPercent"].ToString());
                    return boHasPromoBySupplier;
                }

                /*******************************Up to Contact, Group, Sub, Prod ID only*****************************/
                SQL = "SELECT " +
                            "PromoBySupplierItemsID, " +
                            "a.PromoBySupplierID, " +
                            "PromoBySupplierTypeID, " +
                            "ProductGroupID, " +
                            "ProductSubGroupID, " +
                            "ProductID,  " +
                            "VariationMatrixID, " +
                            "Quantity,  " +
                            "PromoBySupplierValue,  " +
                            "InPercent  " +
                        "FROM tblPromoBySupplierItems a  " +
                        "INNER JOIN tblPromoBySupplier b ON a.PromoBySupplierID = b.PromoBySupplierID " +
                        "WHERE " + (string.IsNullOrEmpty(strSpecialSupplierIDs) ? "ContactID = @ContactID " : "ContactID IN (@ContactID, " + strSpecialSupplierIDs + ") ") +
                            "AND ProductGroupID = @ProductGroupID " +
                            "AND ProductSubGroupID = @ProductSubGroupID " +
                            "AND ProductID = @ProductID " +
                            "AND VariationMatrixID = 0 " +
                            "AND Status = 1 " +
                            "AND DATE_FORMAT(StartDate, '%Y-%m-%d %H:%i') <= DATE_FORMAT(NOW(), '%Y-%m-%d %H:%i') " +
                            "AND DATE_FORMAT(EndDate, '%Y-%m-%d %H:%i') >= DATE_FORMAT(NOW(), '%Y-%m-%d %H:%i') ORDER BY PromoBySupplierValue ASC LIMIT 1;";

                cmd = new MySqlCommand();
                cmd.CommandType = System.Data.CommandType.Text;

                cmd.Parameters.AddWithValue("@ContactID", ContactID);
                cmd.Parameters.AddWithValue("@ProductGroupID", ProductGroupID);
                cmd.Parameters.AddWithValue("@ProductSubGroupID", ProductSubGroupID);
                cmd.Parameters.AddWithValue("@ProductID", ProductID);

                cmd.CommandText = SQL;
                dt = new System.Data.DataTable(strDataTableName);
                base.MySqlDataAdapterFill(cmd, dt);

                foreach (System.Data.DataRow dr in dt.Rows)
                {
                    PromoBySupplierType = (PromoTypes)Enum.Parse(typeof(PromoTypes), dr["PromoBySupplierTypeID"].ToString());
                    PromoBySupplierQuantity = decimal.Parse(dr["Quantity"].ToString());
                    PromoBySupplierValue = decimal.Parse(dr["PromoBySupplierValue"].ToString());
                    InPercent = bool.Parse(dr["InPercent"].ToString());
                    return boHasPromoBySupplier;
                }

                /*******************************Up to Contact, Sub, Prod ID only*****************************/
                SQL = "SELECT " +
                            "PromoBySupplierItemsID, " +
                            "a.PromoBySupplierID, " +
                            "PromoBySupplierTypeID, " +
                            "ProductGroupID, " +
                            "ProductSubGroupID, " +
                            "ProductID,  " +
                            "VariationMatrixID, " +
                            "Quantity,  " +
                            "PromoBySupplierValue,  " +
                            "InPercent  " +
                        "FROM tblPromoBySupplierItems a  " +
                        "INNER JOIN tblPromoBySupplier b ON a.PromoBySupplierID = b.PromoBySupplierID " +
                        "WHERE " + (string.IsNullOrEmpty(strSpecialSupplierIDs) ? "ContactID = @ContactID " : "ContactID IN (@ContactID, " + strSpecialSupplierIDs + ") ") +
                            "AND ProductGroupID = 0 " +
                            "AND ProductSubGroupID = @ProductSubGroupID " +
                            "AND ProductID = @ProductID " +
                            "AND VariationMatrixID = 0 " +
                            "AND Status = 1 " +
                            "AND DATE_FORMAT(StartDate, '%Y-%m-%d %H:%i') <= DATE_FORMAT(NOW(), '%Y-%m-%d %H:%i') " +
                            "AND DATE_FORMAT(EndDate, '%Y-%m-%d %H:%i') >= DATE_FORMAT(NOW(), '%Y-%m-%d %H:%i') ORDER BY PromoBySupplierValue ASC LIMIT 1;";

                cmd = new MySqlCommand();
                cmd.CommandType = System.Data.CommandType.Text;

                cmd.Parameters.AddWithValue("@ContactID", ContactID);
                cmd.Parameters.AddWithValue("@ProductSubGroupID", ProductSubGroupID);
                cmd.Parameters.AddWithValue("@ProductID", ProductID);

                cmd.CommandText = SQL;
                dt = new System.Data.DataTable(strDataTableName);
                base.MySqlDataAdapterFill(cmd, dt);

                foreach (System.Data.DataRow dr in dt.Rows)
                {
                    PromoBySupplierType = (PromoTypes)Enum.Parse(typeof(PromoTypes), dr["PromoBySupplierTypeID"].ToString());
                    PromoBySupplierQuantity = decimal.Parse(dr["Quantity"].ToString());
                    PromoBySupplierValue = decimal.Parse(dr["PromoBySupplierValue"].ToString());
                    InPercent = bool.Parse(dr["InPercent"].ToString());
                    return boHasPromoBySupplier;
                }

                /*******************************Up to Contact, Prod ID only*****************************/
                SQL = "SELECT " +
                            "PromoBySupplierItemsID, " +
                            "a.PromoBySupplierID, " +
                            "PromoBySupplierTypeID, " +
                            "ProductGroupID, " +
                            "ProductSubGroupID, " +
                            "ProductID,  " +
                            "VariationMatrixID, " +
                            "Quantity,  " +
                            "PromoBySupplierValue,  " +
                            "InPercent  " +
                        "FROM tblPromoBySupplierItems a  " +
                        "INNER JOIN tblPromoBySupplier b ON a.PromoBySupplierID = b.PromoBySupplierID " +
                        "WHERE " + (string.IsNullOrEmpty(strSpecialSupplierIDs) ? "ContactID = @ContactID " : "ContactID IN (@ContactID, " + strSpecialSupplierIDs + ") ") +
                            "AND ProductGroupID = 0 " +
                            "AND ProductSubGroupID = 0 " +
                            "AND ProductID = @ProductID " +
                            "AND VariationMatrixID = 0 " +
                            "AND Status = 1 " +
                            "AND DATE_FORMAT(StartDate, '%Y-%m-%d %H:%i') <= DATE_FORMAT(NOW(), '%Y-%m-%d %H:%i') " +
                            "AND DATE_FORMAT(EndDate, '%Y-%m-%d %H:%i') >= DATE_FORMAT(NOW(), '%Y-%m-%d %H:%i') ORDER BY PromoBySupplierValue ASC LIMIT 1;";

                cmd = new MySqlCommand();
                cmd.CommandType = System.Data.CommandType.Text;

                cmd.Parameters.AddWithValue("@ContactID", ContactID);
                cmd.Parameters.AddWithValue("@ProductID", ProductID);

                cmd.CommandText = SQL;
                dt = new System.Data.DataTable(strDataTableName);
                base.MySqlDataAdapterFill(cmd, dt);

                foreach (System.Data.DataRow dr in dt.Rows)
                {
                    PromoBySupplierType = (PromoTypes)Enum.Parse(typeof(PromoTypes), dr["PromoBySupplierTypeID"].ToString());
                    PromoBySupplierQuantity = decimal.Parse(dr["Quantity"].ToString());
                    PromoBySupplierValue = decimal.Parse(dr["PromoBySupplierValue"].ToString());
                    InPercent = bool.Parse(dr["InPercent"].ToString());
                    return boHasPromoBySupplier;
                }

                /*******************************Up to Contact, Group, Sub only*****************************/
                SQL = "SELECT " +
                            "PromoBySupplierItemsID, " +
                            "a.PromoBySupplierID, " +
                            "PromoBySupplierTypeID, " +
                            "ProductGroupID, " +
                            "ProductSubGroupID, " +
                            "ProductID,  " +
                            "VariationMatrixID, " +
                            "Quantity,  " +
                            "PromoBySupplierValue,  " +
                            "InPercent  " +
                        "FROM tblPromoBySupplierItems a  " +
                        "INNER JOIN tblPromoBySupplier b ON a.PromoBySupplierID = b.PromoBySupplierID " +
                        "WHERE " + (string.IsNullOrEmpty(strSpecialSupplierIDs) ? "ContactID = @ContactID " : "ContactID IN (@ContactID, " + strSpecialSupplierIDs + ") ") +
                            "AND ProductGroupID = @ProductGroupID " +
                            "AND ProductSubGroupID = @ProductSubGroupID " +
                            "AND ProductID = 0 " +
                            "AND VariationMatrixID = 0 " +
                            "AND Status = 1 " +
                            "AND DATE_FORMAT(StartDate, '%Y-%m-%d %H:%i') <= DATE_FORMAT(NOW(), '%Y-%m-%d %H:%i') " +
                            "AND DATE_FORMAT(EndDate, '%Y-%m-%d %H:%i') >= DATE_FORMAT(NOW(), '%Y-%m-%d %H:%i') ORDER BY PromoBySupplierValue ASC LIMIT 1;";

                cmd = new MySqlCommand();
                cmd.CommandType = System.Data.CommandType.Text;

                cmd.Parameters.AddWithValue("@ContactID", ContactID);
                cmd.Parameters.AddWithValue("@ProductGroupID", ProductGroupID);
                cmd.Parameters.AddWithValue("@ProductSubGroupID", ProductSubGroupID);

                cmd.CommandText = SQL;
                dt = new System.Data.DataTable(strDataTableName);
                base.MySqlDataAdapterFill(cmd, dt);

                foreach (System.Data.DataRow dr in dt.Rows)
                {
                    PromoBySupplierType = (PromoTypes)Enum.Parse(typeof(PromoTypes), dr["PromoBySupplierTypeID"].ToString());
                    PromoBySupplierQuantity = decimal.Parse(dr["Quantity"].ToString());
                    PromoBySupplierValue = decimal.Parse(dr["PromoBySupplierValue"].ToString());
                    InPercent = bool.Parse(dr["InPercent"].ToString());
                    return boHasPromoBySupplier;
                }

                /*******************************Up to Contact, Sub only*****************************/
                SQL = "SELECT " +
                            "PromoBySupplierItemsID, " +
                            "a.PromoBySupplierID, " +
                            "PromoBySupplierTypeID, " +
                            "ProductGroupID, " +
                            "ProductSubGroupID, " +
                            "ProductID,  " +
                            "VariationMatrixID, " +
                            "Quantity,  " +
                            "PromoBySupplierValue,  " +
                            "InPercent  " +
                        "FROM tblPromoBySupplierItems a  " +
                        "INNER JOIN tblPromoBySupplier b ON a.PromoBySupplierID = b.PromoBySupplierID " +
                        "WHERE " + (string.IsNullOrEmpty(strSpecialSupplierIDs) ? "ContactID = @ContactID " : "ContactID IN (@ContactID, " + strSpecialSupplierIDs + ") ") +
                            "AND ProductGroupID = 0 " +
                            "AND ProductSubGroupID = @ProductSubGroupID " +
                            "AND ProductID = 0 " +
                            "AND VariationMatrixID = 0 " +
                            "AND Status = 1 " +
                            "AND DATE_FORMAT(StartDate, '%Y-%m-%d %H:%i') <= DATE_FORMAT(NOW(), '%Y-%m-%d %H:%i') " +
                            "AND DATE_FORMAT(EndDate, '%Y-%m-%d %H:%i') >= DATE_FORMAT(NOW(), '%Y-%m-%d %H:%i') ORDER BY PromoBySupplierValue ASC LIMIT 1;";

                cmd = new MySqlCommand();
                cmd.CommandType = System.Data.CommandType.Text;

                cmd.Parameters.AddWithValue("@ContactID", ContactID);
                cmd.Parameters.AddWithValue("@ProductSubGroupID", ProductSubGroupID);

                cmd.CommandText = SQL;
                dt = new System.Data.DataTable(strDataTableName);
                base.MySqlDataAdapterFill(cmd, dt);

                foreach (System.Data.DataRow dr in dt.Rows)
                {
                    PromoBySupplierType = (PromoTypes)Enum.Parse(typeof(PromoTypes), dr["PromoBySupplierTypeID"].ToString());
                    PromoBySupplierQuantity = decimal.Parse(dr["Quantity"].ToString());
                    PromoBySupplierValue = decimal.Parse(dr["PromoBySupplierValue"].ToString());
                    InPercent = bool.Parse(dr["InPercent"].ToString());
                    return boHasPromoBySupplier;
                }

                /*******************************Up to Contact only*****************************/
                SQL = "SELECT " +
                            "PromoBySupplierItemsID, " +
                            "a.PromoBySupplierID, " +
                            "PromoBySupplierTypeID, " +
                            "ProductGroupID, " +
                            "ProductSubGroupID, " +
                            "ProductID,  " +
                            "VariationMatrixID, " +
                            "Quantity,  " +
                            "PromoBySupplierValue,  " +
                            "InPercent  " +
                        "FROM tblPromoBySupplierItems a  " +
                        "INNER JOIN tblPromoBySupplier b ON a.PromoBySupplierID = b.PromoBySupplierID " +
                        "WHERE " + (string.IsNullOrEmpty(strSpecialSupplierIDs) ? "ContactID = @ContactID " : "ContactID IN (@ContactID, " + strSpecialSupplierIDs + ") ") +
                            "AND ProductGroupID = 0 " +
                            "AND ProductSubGroupID = 0 " +
                            "AND ProductID = 0 " +
                            "AND VariationMatrixID = 0 " +
                            "AND Status = 1 " +
                            "AND DATE_FORMAT(StartDate, '%Y-%m-%d %H:%i') <= DATE_FORMAT(NOW(), '%Y-%m-%d %H:%i') " +
                            "AND DATE_FORMAT(EndDate, '%Y-%m-%d %H:%i') >= DATE_FORMAT(NOW(), '%Y-%m-%d %H:%i') ORDER BY PromoBySupplierValue ASC LIMIT 1;";

                cmd = new MySqlCommand();
                cmd.CommandType = System.Data.CommandType.Text;

                cmd.Parameters.AddWithValue("@ContactID", ContactID);

                cmd.CommandText = SQL;
                dt = new System.Data.DataTable(strDataTableName);
                base.MySqlDataAdapterFill(cmd, dt);

                foreach (System.Data.DataRow dr in dt.Rows)
                {
                    PromoBySupplierType = (PromoTypes)Enum.Parse(typeof(PromoTypes), dr["PromoBySupplierTypeID"].ToString());
                    PromoBySupplierQuantity = decimal.Parse(dr["Quantity"].ToString());
                    PromoBySupplierValue = decimal.Parse(dr["PromoBySupplierValue"].ToString());
                    InPercent = bool.Parse(dr["InPercent"].ToString());
                    return boHasPromoBySupplier;
                }

                /*******************************Up to Group, Sub, Prod and VarM ID only*****************************/
                SQL = "SELECT " +
                            "PromoBySupplierItemsID, " +
                            "a.PromoBySupplierID, " +
                            "PromoBySupplierTypeID, " +
                            "ProductGroupID, " +
                            "ProductSubGroupID, " +
                            "ProductID,  " +
                            "VariationMatrixID, " +
                            "Quantity,  " +
                            "PromoBySupplierValue,  " +
                            "InPercent  " +
                        "FROM tblPromoBySupplierItems a  " +
                        "INNER JOIN tblPromoBySupplier b ON a.PromoBySupplierID = b.PromoBySupplierID " +
                        "WHERE " + (string.IsNullOrEmpty(strSpecialSupplierIDs) ? "ContactID = 0 " : "ContactID IN (0, " + strSpecialSupplierIDs + ") ") +
                            "AND ProductGroupID = @ProductGroupID " +
                            "AND ProductSubGroupID = @ProductSubGroupID " +
                            "AND ProductID = @ProductID " +
                            "AND VariationMatrixID = @VariationMatrixID " +
                            "AND Status = 1 " +
                            "AND DATE_FORMAT(StartDate, '%Y-%m-%d %H:%i') <= DATE_FORMAT(NOW(), '%Y-%m-%d %H:%i') " +
                            "AND DATE_FORMAT(EndDate, '%Y-%m-%d %H:%i') >= DATE_FORMAT(NOW(), '%Y-%m-%d %H:%i') ORDER BY PromoBySupplierValue ASC LIMIT 1;";

                cmd = new MySqlCommand();
                cmd.CommandType = System.Data.CommandType.Text;

                cmd.Parameters.AddWithValue("@ProductGroupID", ProductGroupID);
                cmd.Parameters.AddWithValue("@ProductSubGroupID", ProductSubGroupID);
                cmd.Parameters.AddWithValue("@ProductID", ProductID);
                cmd.Parameters.AddWithValue("@VariationMatrixID", VariationMatrixID);

                cmd.CommandText = SQL;
                dt = new System.Data.DataTable(strDataTableName);
                base.MySqlDataAdapterFill(cmd, dt);

                foreach (System.Data.DataRow dr in dt.Rows)
                {
                    PromoBySupplierType = (PromoTypes)Enum.Parse(typeof(PromoTypes), dr["PromoBySupplierTypeID"].ToString());
                    PromoBySupplierQuantity = decimal.Parse(dr["Quantity"].ToString());
                    PromoBySupplierValue = decimal.Parse(dr["PromoBySupplierValue"].ToString());
                    InPercent = bool.Parse(dr["InPercent"].ToString());
                    return boHasPromoBySupplier;
                }

                /*******************************Up to Sub, Prod and VariationMatrix ID only*****************************/
                SQL = "SELECT " +
                            "PromoBySupplierItemsID, " +
                            "a.PromoBySupplierID, " +
                            "PromoBySupplierTypeID, " +
                            "ProductGroupID, " +
                            "ProductSubGroupID, " +
                            "ProductID,  " +
                            "VariationMatrixID, " +
                            "Quantity,  " +
                            "PromoBySupplierValue,  " +
                            "InPercent  " +
                        "FROM tblPromoBySupplierItems a  " +
                        "INNER JOIN tblPromoBySupplier b ON a.PromoBySupplierID = b.PromoBySupplierID " +
                        "WHERE " + (string.IsNullOrEmpty(strSpecialSupplierIDs) ? "ContactID = 0 " : "ContactID IN (0, " + strSpecialSupplierIDs + ") ") +
                            "AND ProductGroupID =0 " +
                            "AND ProductSubGroupID = @ProductSubGroupID " +
                            "AND ProductID = @ProductID " +
                            "AND VariationMatrixID = @VariationMatrixID " +
                            "AND Status = 1 " +
                            "AND DATE_FORMAT(StartDate, '%Y-%m-%d %H:%i') <= DATE_FORMAT(NOW(), '%Y-%m-%d %H:%i') " +
                            "AND DATE_FORMAT(EndDate, '%Y-%m-%d %H:%i') >= DATE_FORMAT(NOW(), '%Y-%m-%d %H:%i') ORDER BY PromoBySupplierValue ASC LIMIT 1;";

                cmd = new MySqlCommand();
                cmd.CommandType = System.Data.CommandType.Text;

                cmd.Parameters.AddWithValue("@ProductSubGroupID", ProductSubGroupID);
                cmd.Parameters.AddWithValue("@ProductID", ProductID);
                cmd.Parameters.AddWithValue("@VariationMatrixID", VariationMatrixID);

                cmd.CommandText = SQL;
                dt = new System.Data.DataTable(strDataTableName);
                base.MySqlDataAdapterFill(cmd, dt);

                foreach (System.Data.DataRow dr in dt.Rows)
                {
                    PromoBySupplierType = (PromoTypes)Enum.Parse(typeof(PromoTypes), dr["PromoBySupplierTypeID"].ToString());
                    PromoBySupplierQuantity = decimal.Parse(dr["Quantity"].ToString());
                    PromoBySupplierValue = decimal.Parse(dr["PromoBySupplierValue"].ToString());
                    InPercent = bool.Parse(dr["InPercent"].ToString());
                    return boHasPromoBySupplier;
                }

                /*******************************Up to Prod and VariationMatrix ID only*****************************/
                SQL = "SELECT " +
                            "PromoBySupplierItemsID, " +
                            "a.PromoBySupplierID, " +
                            "PromoBySupplierTypeID, " +
                            "ProductGroupID, " +
                            "ProductSubGroupID, " +
                            "ProductID,  " +
                            "VariationMatrixID, " +
                            "Quantity,  " +
                            "PromoBySupplierValue,  " +
                            "InPercent  " +
                        "FROM tblPromoBySupplierItems a  " +
                        "INNER JOIN tblPromoBySupplier b ON a.PromoBySupplierID = b.PromoBySupplierID " +
                        "WHERE " + (string.IsNullOrEmpty(strSpecialSupplierIDs) ? "ContactID = 0 " : "ContactID IN (0, " + strSpecialSupplierIDs + ") ") +
                            "AND ProductGroupID = 0 " +
                            "AND ProductSubGroupID = 0 " +
                            "AND ProductID = @ProductID " +
                            "AND VariationMatrixID = @VariationMatrixID " +
                            "AND Status = 1 " +
                            "AND DATE_FORMAT(StartDate, '%Y-%m-%d %H:%i') <= DATE_FORMAT(NOW(), '%Y-%m-%d %H:%i') " +
                            "AND DATE_FORMAT(EndDate, '%Y-%m-%d %H:%i') >= DATE_FORMAT(NOW(), '%Y-%m-%d %H:%i') ORDER BY PromoBySupplierValue ASC LIMIT 1;";

                cmd = new MySqlCommand();
                cmd.CommandType = System.Data.CommandType.Text;

                cmd.Parameters.AddWithValue("@ProductID", ProductID);
                cmd.Parameters.AddWithValue("@VariationMatrixID", VariationMatrixID);

                cmd.CommandText = SQL;
                dt = new System.Data.DataTable(strDataTableName);
                base.MySqlDataAdapterFill(cmd, dt);

                foreach (System.Data.DataRow dr in dt.Rows)
                {
                    PromoBySupplierType = (PromoTypes)Enum.Parse(typeof(PromoTypes), dr["PromoBySupplierTypeID"].ToString());
                    PromoBySupplierQuantity = decimal.Parse(dr["Quantity"].ToString());
                    PromoBySupplierValue = decimal.Parse(dr["PromoBySupplierValue"].ToString());
                    InPercent = bool.Parse(dr["InPercent"].ToString());
                    return boHasPromoBySupplier;
                }

                /*******************************Up to VariationsMatrix ID only*****************************/
                SQL = "SELECT " +
                            "PromoBySupplierItemsID, " +
                            "a.PromoBySupplierID, " +
                            "PromoBySupplierTypeID, " +
                            "ProductGroupID, " +
                            "ProductSubGroupID, " +
                            "ProductID,  " +
                            "VariationMatrixID, " +
                            "Quantity,  " +
                            "PromoBySupplierValue,  " +
                            "InPercent  " +
                        "FROM tblPromoBySupplierItems a  " +
                        "INNER JOIN tblPromoBySupplier b ON a.PromoBySupplierID = b.PromoBySupplierID " +
                        "WHERE " + (string.IsNullOrEmpty(strSpecialSupplierIDs) ? "ContactID = 0 " : "ContactID IN (0, " + strSpecialSupplierIDs + ") ") +
                            "AND ProductGroupID = 0 " +
                            "AND ProductSubGroupID = 0 " +
                            "AND ProductID = 0 " +
                            "AND VariationMatrixID = @VariationMatrixID " +
                            "AND Status = 1 " +
                            "AND DATE_FORMAT(StartDate, '%Y-%m-%d %H:%i') <= DATE_FORMAT(NOW(), '%Y-%m-%d %H:%i') " +
                            "AND DATE_FORMAT(EndDate, '%Y-%m-%d %H:%i') >= DATE_FORMAT(NOW(), '%Y-%m-%d %H:%i') ORDER BY PromoBySupplierValue ASC LIMIT 1;";

                cmd = new MySqlCommand();
                cmd.CommandType = System.Data.CommandType.Text;

                cmd.Parameters.AddWithValue("@VariationMatrixID", VariationMatrixID);

                cmd.CommandText = SQL;
                dt = new System.Data.DataTable(strDataTableName);
                base.MySqlDataAdapterFill(cmd, dt);

                foreach (System.Data.DataRow dr in dt.Rows)
                {
                    PromoBySupplierType = (PromoTypes)Enum.Parse(typeof(PromoTypes), dr["PromoBySupplierTypeID"].ToString());
                    PromoBySupplierQuantity = decimal.Parse(dr["Quantity"].ToString());
                    PromoBySupplierValue = decimal.Parse(dr["PromoBySupplierValue"].ToString());
                    InPercent = bool.Parse(dr["InPercent"].ToString());
                    return boHasPromoBySupplier;
                }

                /*******************************Up to group, Sub, Prod ID only*****************************/
                SQL = "SELECT " +
                            "PromoBySupplierItemsID, " +
                            "a.PromoBySupplierID, " +
                            "PromoBySupplierTypeID, " +
                            "ProductGroupID, " +
                            "ProductSubGroupID, " +
                            "ProductID,  " +
                            "VariationMatrixID, " +
                            "Quantity,  " +
                            "PromoBySupplierValue,  " +
                            "InPercent  " +
                        "FROM tblPromoBySupplierItems a  " +
                        "INNER JOIN tblPromoBySupplier b ON a.PromoBySupplierID = b.PromoBySupplierID " +
                        "WHERE " + (string.IsNullOrEmpty(strSpecialSupplierIDs) ? "ContactID = 0 " : "ContactID IN (0, " + strSpecialSupplierIDs + ") ") +
                            "AND ProductGroupID = @ProductGroupID " +
                            "AND ProductSubGroupID = @ProductSubGroupID " +
                            "AND ProductID = @ProductID " +
                            "AND VariationMatrixID = 0 " +
                            "AND Status = 1 " +
                            "AND DATE_FORMAT(StartDate, '%Y-%m-%d %H:%i') <= DATE_FORMAT(NOW(), '%Y-%m-%d %H:%i') " +
                            "AND DATE_FORMAT(EndDate, '%Y-%m-%d %H:%i') >= DATE_FORMAT(NOW(), '%Y-%m-%d %H:%i') ORDER BY PromoBySupplierValue ASC LIMIT 1;";

                cmd = new MySqlCommand();
                cmd.CommandType = System.Data.CommandType.Text;

                cmd.Parameters.AddWithValue("@ProductGroupID", ProductGroupID);
                cmd.Parameters.AddWithValue("@ProductSubGroupID", ProductSubGroupID);
                cmd.Parameters.AddWithValue("@ProductID", ProductID);

                cmd.CommandText = SQL;
                dt = new System.Data.DataTable(strDataTableName);
                base.MySqlDataAdapterFill(cmd, dt);

                foreach (System.Data.DataRow dr in dt.Rows)
                {
                    PromoBySupplierType = (PromoTypes)Enum.Parse(typeof(PromoTypes), dr["PromoBySupplierTypeID"].ToString());
                    PromoBySupplierQuantity = decimal.Parse(dr["Quantity"].ToString());
                    PromoBySupplierValue = decimal.Parse(dr["PromoBySupplierValue"].ToString());
                    InPercent = bool.Parse(dr["InPercent"].ToString());
                    return boHasPromoBySupplier;
                }

                /*******************************Up to Sub, Prod ID only*****************************/
                SQL = "SELECT " +
                            "PromoBySupplierItemsID, " +
                            "a.PromoBySupplierID, " +
                            "PromoBySupplierTypeID, " +
                            "ProductGroupID, " +
                            "ProductSubGroupID, " +
                            "ProductID,  " +
                            "VariationMatrixID, " +
                            "Quantity,  " +
                            "PromoBySupplierValue,  " +
                            "InPercent  " +
                        "FROM tblPromoBySupplierItems a  " +
                        "INNER JOIN tblPromoBySupplier b ON a.PromoBySupplierID = b.PromoBySupplierID " +
                        "WHERE " + (string.IsNullOrEmpty(strSpecialSupplierIDs) ? "ContactID = 0 " : "ContactID IN (0, " + strSpecialSupplierIDs + ") ") +
                            "AND ProductGroupID = 0 " +
                            "AND ProductSubGroupID = @ProductSubGroupID " +
                            "AND ProductID = @ProductID " +
                            "AND VariationMatrixID = 0 " +
                            "AND Status = 1 " +
                            "AND DATE_FORMAT(StartDate, '%Y-%m-%d %H:%i') <= DATE_FORMAT(NOW(), '%Y-%m-%d %H:%i') " +
                            "AND DATE_FORMAT(EndDate, '%Y-%m-%d %H:%i') >= DATE_FORMAT(NOW(), '%Y-%m-%d %H:%i') ORDER BY PromoBySupplierValue ASC LIMIT 1;";

                cmd = new MySqlCommand();
                cmd.CommandType = System.Data.CommandType.Text;

                cmd.Parameters.AddWithValue("@ProductSubGroupID", ProductSubGroupID);
                cmd.Parameters.AddWithValue("@ProductID", ProductID);

                cmd.CommandText = SQL;
                dt = new System.Data.DataTable(strDataTableName);
                base.MySqlDataAdapterFill(cmd, dt);

                foreach (System.Data.DataRow dr in dt.Rows)
                {
                    PromoBySupplierType = (PromoTypes)Enum.Parse(typeof(PromoTypes), dr["PromoBySupplierTypeID"].ToString());
                    PromoBySupplierQuantity = decimal.Parse(dr["Quantity"].ToString());
                    PromoBySupplierValue = decimal.Parse(dr["PromoBySupplierValue"].ToString());
                    InPercent = bool.Parse(dr["InPercent"].ToString());
                    return boHasPromoBySupplier;
                }

                /*******************************Up to group, Sub, Prod and VariationMatrix ID only*****************************/
                SQL = "SELECT " +
                            "PromoBySupplierItemsID, " +
                            "a.PromoBySupplierID, " +
                            "PromoBySupplierTypeID, " +
                            "ProductGroupID, " +
                            "ProductSubGroupID, " +
                            "ProductID,  " +
                            "VariationMatrixID, " +
                            "Quantity,  " +
                            "PromoBySupplierValue,  " +
                            "InPercent  " +
                        "FROM tblPromoBySupplierItems a  " +
                        "INNER JOIN tblPromoBySupplier b ON a.PromoBySupplierID = b.PromoBySupplierID " +
                        "WHERE " + (string.IsNullOrEmpty(strSpecialSupplierIDs) ? "ContactID = 0 " : "ContactID IN (0, " + strSpecialSupplierIDs + ") ") +
                            "AND ProductGroupID = 0 " +
                            "AND ProductSubGroupID = 0 " +
                            "AND ProductID = @ProductID " +
                            "AND VariationMatrixID = 0 " +
                            "AND Status = 1 " +
                            "AND DATE_FORMAT(StartDate, '%Y-%m-%d %H:%i') <= DATE_FORMAT(NOW(), '%Y-%m-%d %H:%i') " +
                            "AND DATE_FORMAT(EndDate, '%Y-%m-%d %H:%i') >= DATE_FORMAT(NOW(), '%Y-%m-%d %H:%i') ORDER BY PromoBySupplierValue ASC LIMIT 1;";

                cmd = new MySqlCommand();
                cmd.CommandType = System.Data.CommandType.Text;

                cmd.Parameters.AddWithValue("@ProductID", ProductID);

                cmd.CommandText = SQL;
                dt = new System.Data.DataTable(strDataTableName);
                base.MySqlDataAdapterFill(cmd, dt);

                foreach (System.Data.DataRow dr in dt.Rows)
                {
                    PromoBySupplierType = (PromoTypes)Enum.Parse(typeof(PromoTypes), dr["PromoBySupplierTypeID"].ToString());
                    PromoBySupplierQuantity = decimal.Parse(dr["Quantity"].ToString());
                    PromoBySupplierValue = decimal.Parse(dr["PromoBySupplierValue"].ToString());
                    InPercent = bool.Parse(dr["InPercent"].ToString());
                    return boHasPromoBySupplier;
                }

                /*******************************Up to group, Sub ID only*****************************/
                SQL = "SELECT " +
                            "PromoBySupplierItemsID, " +
                            "a.PromoBySupplierID, " +
                            "PromoBySupplierTypeID, " +
                            "ProductGroupID, " +
                            "ProductSubGroupID, " +
                            "ProductID,  " +
                            "VariationMatrixID, " +
                            "Quantity,  " +
                            "PromoBySupplierValue,  " +
                            "InPercent  " +
                        "FROM tblPromoBySupplierItems a  " +
                        "INNER JOIN tblPromoBySupplier b ON a.PromoBySupplierID = b.PromoBySupplierID " +
                        "WHERE " + (string.IsNullOrEmpty(strSpecialSupplierIDs) ? "ContactID = 0 " : "ContactID IN (0, " + strSpecialSupplierIDs + ") ") +
                            "AND ProductGroupID = @ProductGroupID " +
                            "AND ProductSubGroupID = @ProductSubGroupID " +
                            "AND ProductID = 0 " +
                            "AND VariationMatrixID = 0 " +
                            "AND Status = 1 " +
                            "AND DATE_FORMAT(StartDate, '%Y-%m-%d %H:%i') <= DATE_FORMAT(NOW(), '%Y-%m-%d %H:%i') " +
                            "AND DATE_FORMAT(EndDate, '%Y-%m-%d %H:%i') >= DATE_FORMAT(NOW(), '%Y-%m-%d %H:%i') ORDER BY PromoBySupplierValue ASC LIMIT 1;";

                cmd = new MySqlCommand();
                cmd.CommandType = System.Data.CommandType.Text;

                cmd.Parameters.AddWithValue("@ProductGroupID", ProductGroupID);
                cmd.Parameters.AddWithValue("@ProductSubGroupID", ProductSubGroupID);

                cmd.CommandText = SQL;
                dt = new System.Data.DataTable(strDataTableName);
                base.MySqlDataAdapterFill(cmd, dt);

                foreach (System.Data.DataRow dr in dt.Rows)
                {
                    PromoBySupplierType = (PromoTypes)Enum.Parse(typeof(PromoTypes), dr["PromoBySupplierTypeID"].ToString());
                    PromoBySupplierQuantity = decimal.Parse(dr["Quantity"].ToString());
                    PromoBySupplierValue = decimal.Parse(dr["PromoBySupplierValue"].ToString());
                    InPercent = bool.Parse(dr["InPercent"].ToString());
                    return boHasPromoBySupplier;
                }

                /*******************************Up to Sub ID only*****************************/
                SQL = "SELECT " +
                            "PromoBySupplierItemsID, " +
                            "a.PromoBySupplierID, " +
                            "PromoBySupplierTypeID, " +
                            "ProductGroupID, " +
                            "ProductSubGroupID, " +
                            "ProductID,  " +
                            "VariationMatrixID, " +
                            "Quantity,  " +
                            "PromoBySupplierValue,  " +
                            "InPercent  " +
                        "FROM tblPromoBySupplierItems a  " +
                        "INNER JOIN tblPromoBySupplier b ON a.PromoBySupplierID = b.PromoBySupplierID " +
                        "WHERE " + (string.IsNullOrEmpty(strSpecialSupplierIDs) ? "ContactID = 0 " : "ContactID IN (0, " + strSpecialSupplierIDs + ") ") +
                            "AND ProductGroupID = 0 " +
                            "AND ProductSubGroupID = @ProductSubGroupID " +
                            "AND ProductID = 0 " +
                            "AND VariationMatrixID = 0 " +
                            "AND Status = 1 " +
                            "AND DATE_FORMAT(StartDate, '%Y-%m-%d %H:%i') <= DATE_FORMAT(NOW(), '%Y-%m-%d %H:%i') " +
                            "AND DATE_FORMAT(EndDate, '%Y-%m-%d %H:%i') >= DATE_FORMAT(NOW(), '%Y-%m-%d %H:%i') ORDER BY PromoBySupplierValue ASC LIMIT 1;";

                cmd = new MySqlCommand();
                cmd.CommandType = System.Data.CommandType.Text;

                cmd.Parameters.AddWithValue("@ProductSubGroupID", ProductSubGroupID);

                cmd.CommandText = SQL;
                dt = new System.Data.DataTable(strDataTableName);
                base.MySqlDataAdapterFill(cmd, dt);

                foreach (System.Data.DataRow dr in dt.Rows)
                {
                    PromoBySupplierType = (PromoTypes)Enum.Parse(typeof(PromoTypes), dr["PromoBySupplierTypeID"].ToString());
                    PromoBySupplierQuantity = decimal.Parse(dr["Quantity"].ToString());
                    PromoBySupplierValue = decimal.Parse(dr["PromoBySupplierValue"].ToString());
                    InPercent = bool.Parse(dr["InPercent"].ToString());
                    return boHasPromoBySupplier;
                }

                /*******************************Up to group ID only*****************************/
                SQL = "SELECT " +
                            "PromoBySupplierItemsID, " +
                            "a.PromoBySupplierID, " +
                            "PromoBySupplierTypeID, " +
                            "ProductGroupID, " +
                            "ProductSubGroupID, " +
                            "ProductID,  " +
                            "VariationMatrixID, " +
                            "Quantity,  " +
                            "PromoBySupplierValue,  " +
                            "InPercent  " +
                        "FROM tblPromoBySupplierItems a  " +
                        "INNER JOIN tblPromoBySupplier b ON a.PromoBySupplierID = b.PromoBySupplierID " +
                        "WHERE " + (string.IsNullOrEmpty(strSpecialSupplierIDs) ? "ContactID = 0 " : "ContactID IN (0, " + strSpecialSupplierIDs + ") ") +
                            "AND ProductGroupID = @ProductGroupID " +
                            "AND ProductSubGroupID = 0 " +
                            "AND ProductID = 0 " +
                            "AND VariationMatrixID = 0 " +
                            "AND Status = 1 " +
                            "AND DATE_FORMAT(StartDate, '%Y-%m-%d %H:%i') <= DATE_FORMAT(NOW(), '%Y-%m-%d %H:%i') " +
                            "AND DATE_FORMAT(EndDate, '%Y-%m-%d %H:%i') >= DATE_FORMAT(NOW(), '%Y-%m-%d %H:%i') ORDER BY PromoBySupplierValue ASC LIMIT 1;";

                cmd = new MySqlCommand();
                cmd.CommandType = System.Data.CommandType.Text;

                cmd.Parameters.AddWithValue("@ProductGroupID", ProductGroupID);

                cmd.CommandText = SQL;
                dt = new System.Data.DataTable(strDataTableName);
                base.MySqlDataAdapterFill(cmd, dt);

                foreach (System.Data.DataRow dr in dt.Rows)
                {
                    PromoBySupplierType = (PromoTypes)Enum.Parse(typeof(PromoTypes), dr["PromoBySupplierTypeID"].ToString());
                    PromoBySupplierQuantity = decimal.Parse(dr["Quantity"].ToString());
                    PromoBySupplierValue = decimal.Parse(dr["PromoBySupplierValue"].ToString());
                    InPercent = bool.Parse(dr["InPercent"].ToString());
                    return boHasPromoBySupplier;
                }

                /*******************************Up to all only*****************************/
                SQL = "SELECT " +
                            "PromoBySupplierItemsID, " +
                            "a.PromoBySupplierID, " +
                            "PromoBySupplierTypeID, " +
                            "ProductGroupID, " +
                            "ProductSubGroupID, " +
                            "ProductID,  " +
                            "VariationMatrixID, " +
                            "Quantity,  " +
                            "PromoBySupplierValue,  " +
                            "InPercent  " +
                        "FROM tblPromoBySupplierItems a  " +
                        "INNER JOIN tblPromoBySupplier b ON a.PromoBySupplierID = b.PromoBySupplierID " +
                        "WHERE " + (string.IsNullOrEmpty(strSpecialSupplierIDs) ? "ContactID = 0 " : "ContactID IN (0, " + strSpecialSupplierIDs + ") ") +
                            "AND ProductGroupID = 0 " +
                            "AND ProductSubGroupID = 0 " +
                            "AND ProductID = 0 " +
                            "AND VariationMatrixID = 0 " +
                            "AND Status = 1 " +
                            "AND DATE_FORMAT(StartDate, '%Y-%m-%d %H:%i') <= DATE_FORMAT(NOW(), '%Y-%m-%d %H:%i') " +
                            "AND DATE_FORMAT(EndDate, '%Y-%m-%d %H:%i') >= DATE_FORMAT(NOW(), '%Y-%m-%d %H:%i') ORDER BY PromoBySupplierValue ASC LIMIT 1;";

                cmd = new MySqlCommand();
                cmd.CommandType = System.Data.CommandType.Text;

                cmd.CommandText = SQL;
                dt = new System.Data.DataTable(strDataTableName);
                base.MySqlDataAdapterFill(cmd, dt);

                foreach (System.Data.DataRow dr in dt.Rows)
                {
                    PromoBySupplierType = (PromoTypes)Enum.Parse(typeof(PromoTypes), dr["PromoBySupplierTypeID"].ToString());
                    PromoBySupplierQuantity = decimal.Parse(dr["Quantity"].ToString());
                    PromoBySupplierValue = decimal.Parse(dr["PromoBySupplierValue"].ToString());
                    InPercent = bool.Parse(dr["InPercent"].ToString());
                    return boHasPromoBySupplier;
                }

            }
            catch (Exception ex)
            {
                base.ThrowException(ex);
            }
			return false;
		}
		
		#endregion
	}
}


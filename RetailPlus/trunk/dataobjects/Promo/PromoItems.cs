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
		public bool InPercent;

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
	public class PromoItems : POSConnection
    {
		#region Constructors and Destructors

		public PromoItems()
            : base(null, null)
        {
        }

        public PromoItems(MySqlConnection Connection, MySqlTransaction Transaction) 
            : base(Connection, Transaction)
		{

		}

		#endregion

		#region Insert and Update

		public Int64 Insert(PromoItemsDetails Details)
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

		public void Update(PromoItemsDetails Details)
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

        public Int32 Save(PromoItemsDetails Details)
        {
            try
            {
                string SQL = "CALL procSavePromoItems(@PromoItemsID, @PromoID, @ContactID, @ProductGroupID," +
                                                "@ProductSubGroupID, @ProductID, @VariationMatrixID, @Quantity," +
                                                "@PromoValue, @InPercent, @CreatedOn, @LastModified);";

                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandText = SQL;

                cmd.Parameters.AddWithValue("PromoItemsID", Details.PromoItemsID);
                cmd.Parameters.AddWithValue("PromoID", Details.PromoID);
                cmd.Parameters.AddWithValue("ContactID", Details.ContactID);
                cmd.Parameters.AddWithValue("ProductGroupID", Details.ProductGroupID);
                cmd.Parameters.AddWithValue("ProductSubGroupID", Details.ProductSubGroupID);
                cmd.Parameters.AddWithValue("ProductID", Details.ProductID);
                cmd.Parameters.AddWithValue("VariationMatrixID", Details.VariationMatrixID);
                cmd.Parameters.AddWithValue("Quantity", Details.Quantity);
                cmd.Parameters.AddWithValue("PromoValue", Details.PromoValue);
                cmd.Parameters.AddWithValue("InPercent", Details.InPercent);
                cmd.Parameters.AddWithValue("CreatedOn", Details.CreatedOn == DateTime.MinValue ? Constants.C_DATE_MIN_VALUE : Details.CreatedOn);
                cmd.Parameters.AddWithValue("LastModified", Details.LastModified == DateTime.MinValue ? Constants.C_DATE_MIN_VALUE : Details.LastModified);

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
				
				MySqlCommand cmd;

				string SQL=	"DELETE FROM tblPromoItems WHERE PromoItemsID IN (" + IDs + ");";
				cmd = new MySqlCommand();
				
				
				cmd.CommandType = System.Data.CommandType.Text;
				cmd.CommandText = SQL;
				base.ExecuteNonQuery(cmd);

				return true;
			}

			catch (Exception ex)
			{
				
				
				{
					
					
					
					
				}

				throw base.ThrowException(ex);
			}	
		}


		#endregion

		#region Details

		public PromoItemsDetails DetailsByPromoItemsID(Int64 PromoItemsID)
		{
			try
			{
                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandType = System.Data.CommandType.Text;
				
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

                cmd.Parameters.AddWithValue("@PromoItemsID", PromoItemsID);

                cmd.CommandText = SQL;
                string strDataTableName = "tbl" + this.GetType().FullName.Split(new Char[] { '.' })[this.GetType().FullName.Split(new Char[] { '.' }).Length - 1]; System.Data.DataTable dt = new System.Data.DataTable(strDataTableName);
                base.MySqlDataAdapterFill(cmd, dt);

                PromoItemsDetails Details = new PromoItemsDetails();
                foreach (System.Data.DataRow dr in dt.Rows)
                {
                    Details.PromoItemsID = Int64.Parse(dr["PromoItemsID"].ToString());
                    Details.PromoID = Int64.Parse(dr["PromoID"].ToString());
                    Details.ContactID = Int64.Parse(dr["ContactID"].ToString());
                    Details.ProductGroupID = Int64.Parse(dr["ProductGroupID"].ToString());
                    Details.ProductSubGroupID = Int64.Parse(dr["ProductSubGroupID"].ToString());
                    Details.ProductID = Int64.Parse(dr["ProductID"].ToString());
                    Details.VariationMatrixID = Int64.Parse(dr["VariationMatrixID"].ToString());
                    Details.Quantity = Decimal.Parse(dr["Quantity"].ToString());
                    Details.PromoValue = Decimal.Parse(dr["PromoValue"].ToString());
                    Details.InPercent = bool.Parse(dr["InPercent"].ToString());
                }

				return Details;
			}
			catch (Exception ex)
			{
				throw base.ThrowException(ex);
			}	
		}

		public PromoItemsDetails DetailsByProductID(Int64 ProductID)
		{
			try
			{
                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandType = System.Data.CommandType.Text;
				
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
	 			
				cmd.Parameters.AddWithValue("@ProductID", ProductID);

                cmd.CommandText = SQL;
                string strDataTableName = "tbl" + this.GetType().FullName.Split(new Char[] { '.' })[this.GetType().FullName.Split(new Char[] { '.' }).Length - 1]; System.Data.DataTable dt = new System.Data.DataTable(strDataTableName);
                base.MySqlDataAdapterFill(cmd, dt);

                PromoItemsDetails Details = new PromoItemsDetails();
                foreach (System.Data.DataRow dr in dt.Rows)
                {
                    Details.PromoItemsID = Int64.Parse(dr["PromoItemsID"].ToString());
                    Details.PromoID = Int64.Parse(dr["PromoID"].ToString());
                    Details.ContactID = Int64.Parse(dr["ContactID"].ToString());
                    Details.ProductGroupID = Int64.Parse(dr["ProductGroupID"].ToString());
                    Details.ProductSubGroupID = Int64.Parse(dr["ProductSubGroupID"].ToString());
                    Details.ProductID = Int64.Parse(dr["ProductID"].ToString());
                    Details.VariationMatrixID = Int64.Parse(dr["VariationMatrixID"].ToString());
                    Details.Quantity = Decimal.Parse(dr["Quantity"].ToString());
                    Details.PromoValue = Decimal.Parse(dr["PromoValue"].ToString());
                    Details.InPercent = bool.Parse(dr["InPercent"].ToString());
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

        public System.Data.DataTable ListAsDataTable(Int64 PromoID, string SearchKey = null, string SortField = "PromoItemsID", SortOption SortOrder = SortOption.Ascending, Int32 limit = 0)
		{
			try
			{
                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandType = System.Data.CommandType.Text;

                string SQL = "SELECT " +
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
                            "FROM tblPromoItems a " +
                                "LEFT OUTER JOIN tblContacts b ON a.ContactID = b.ContactID " +
                                "LEFT OUTER JOIN tblProductGroup c ON a.ProductGroupID = c.ProductGroupID L" +
                                "EFT OUTER JOIN tblProductSubGroup d ON a.ProductSubGroupID = d.ProductSubGroupID " +
                                "LEFT OUTER JOIN tblProducts e ON a.ProductID = e.ProductID " +
                                "LEFT OUTER JOIN tblProductBaseVariationsMatrix f ON a.VariationMatrixID = f.MatrixID " +
                            "WHERE PromoID = @PromoID ";

                if (!string.IsNullOrEmpty(SearchKey))
                {
                    SQL += "AND (PromoItemsCode LIKE @SearchKey " +
                                    "OR PromoItemsName LIKE @SearchKey " +
                                    "OR PromoItemsTypeCode LIKE @SearchKey " +
                                    "OR PromoItemsTypeName LIKE @SearchKey) ";
                    cmd.Parameters.AddWithValue("@SearchKey", SearchKey);
                }

                SQL += "ORDER BY " + (!string.IsNullOrEmpty(SortField) ? SortField : "BranchCode") + " ";
                SQL += SortOrder == SortOption.Ascending ? "ASC " : "DESC ";
                SQL += limit == 0 ? "" : "LIMIT " + limit.ToString() + " ";

                cmd.Parameters.AddWithValue("@PromoID", PromoID);

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

        //public bool ApplyPromoValue(ContactDetails clsContactDetails, Int64 ProductID, Int64 VariationMatrixID, out PromoTypes PromoType, out decimal PromoQuantity, out decimal PromoValue, out bool InPercent, int BranchID = 0)
        //{
        //    Int64 ContactID = clsContactDetails.ContactID;

        //    string ContactIDs = "";

        //    if (clsContactDetails.ContactID != Constants.ZERO)
        //    {
        //        if (!string.IsNullOrEmpty(ContactIDs))
        //            ContactIDs = "," + clsContactDetails.ContactID.ToString();
        //        else
        //            ContactIDs = clsContactDetails.ContactID.ToString();
        //    }

        //    // rewardcardmember
        //    if (clsContactDetails.RewardDetails.RewardCardStatus == RewardCardStatus.ManualActivated)
        //    {
        //        if (!string.IsNullOrEmpty(ContactIDs))
        //            ContactIDs = "," + Constants.PLUSCARDMEMBERSID_STRING;
        //        else
        //            ContactIDs = Constants.PLUSCARDMEMBERSID_STRING;
        //    }

        //    // icc card members
        //    if (clsContactDetails.CreditDetails.GuarantorID == Constants.ZERO &&
        //        clsContactDetails.CreditDetails.CreditCardStatus == CreditCardStatus.ManualActivated)
        //    {
        //        if (!string.IsNullOrEmpty(ContactIDs))
        //            ContactIDs = "," + Constants.PLUSCARDMEMBERSID_STRING;
        //        else
        //            ContactIDs = Constants.PLUSCARDMEMBERSID_STRING;
        //    }


        //    PromoType = PromoTypes.NotApplicable;
        //    PromoQuantity = 0;
        //    PromoValue = 0;
        //    InPercent = false;

        //    bool boHasPromo = false;

        //    try
        //    {
        //        Data.Products clsProduct = new Data.Products(base.Connection, base.Transaction);
        //        Data.ProductDetails clsProductDetails = clsProduct.Details1(BranchID, ProductID);

        //        Int64 ProductSubGroupID = clsProductDetails.ProductSubGroupID;
        //        Int64 ProductGroupID = clsProductDetails.ProductGroupID;

        //        MySqlCommand cmd = new MySqlCommand();
        //        cmd.CommandType = System.Data.CommandType.Text;

        //        string SQL = "SELECT " +
        //                        "PromoID  " +
        //                    "FROM tblPromo " +
        //                    "WHERE 1=1 " +
        //                        "AND Status = 1 " +
        //                        "AND DATE_FORMAT(StartDate, '%Y-%m-%d %H:%i') <= DATE_FORMAT(NOW(), '%Y-%m-%d %H:%i') " +
        //                        "AND DATE_FORMAT(EndDate, '%Y-%m-%d %H:%i') >= DATE_FORMAT(NOW(), '%Y-%m-%d %H:%i');";

        //        cmd.CommandText = SQL;
        //        string strDataTableName = "tbl" + this.GetType().FullName.Split(new Char[] { '.' })[this.GetType().FullName.Split(new Char[] { '.' }).Length - 1]; System.Data.DataTable dt = new System.Data.DataTable(strDataTableName);
        //        base.MySqlDataAdapterFill(cmd, dt);

        //        foreach (System.Data.DataRow dr in dt.Rows)
        //        {
        //            boHasPromo = true;
        //            break;
        //        }

        //        if (boHasPromo == false)	//return agad if no Promo is affected by date
        //            return boHasPromo;

        //        /*******************************Up to Contact, Group, Sub, Prod and VarM ID only*****************************/
        //        SQL = "SELECT " +
        //                    "PromoItemsID, " +
        //                    "a.PromoID, " +
        //                    "PromoTypeID, " +
        //                    "ProductGroupID, " +
        //                    "ProductSubGroupID, " +
        //                    "ProductID,  " +
        //                    "VariationMatrixID, " +
        //                    "Quantity,  " +
        //                    "PromoValue,  " +
        //                    "InPercent  " +
        //                "FROM tblPromoItems a  " +
        //                "INNER JOIN tblPromo b ON a.PromoID = b.PromoID " +
        //                "WHERE ContactID = @ContactID " +
        //                    "AND ProductGroupID = @ProductGroupID " +
        //                    "AND ProductSubGroupID = @ProductSubGroupID " +
        //                    "AND ProductID = @ProductID " +
        //                    "AND VariationMatrixID = @VariationMatrixID " +
        //                    "AND Status = 1 " +
        //                    "AND DATE_FORMAT(StartDate, '%Y-%m-%d %H:%i') <= DATE_FORMAT(NOW(), '%Y-%m-%d %H:%i') " +
        //                    "AND DATE_FORMAT(EndDate, '%Y-%m-%d %H:%i') >= DATE_FORMAT(NOW(), '%Y-%m-%d %H:%i');";

        //        cmd = new MySqlCommand();
        //        cmd.CommandType = System.Data.CommandType.Text;

        //        cmd.Parameters.AddWithValue("@ContactID", ContactID);
        //        cmd.Parameters.AddWithValue("@ProductGroupID", ProductGroupID);
        //        cmd.Parameters.AddWithValue("@ProductSubGroupID", ProductSubGroupID);
        //        cmd.Parameters.AddWithValue("@ProductID", ProductID);
        //        cmd.Parameters.AddWithValue("@VariationMatrixID", VariationMatrixID);

        //        cmd.CommandText = SQL;
        //        dt = new System.Data.DataTable(strDataTableName);
        //        base.MySqlDataAdapterFill(cmd, dt);

        //        foreach (System.Data.DataRow dr in dt.Rows)
        //        {
        //            PromoType = (PromoTypes)Enum.Parse(typeof(PromoTypes), dr["PromoTypeID"].ToString());
        //            PromoQuantity = decimal.Parse(dr["Quantity"].ToString());
        //            PromoValue = decimal.Parse(dr["PromoValue"].ToString());
        //            InPercent = bool.Parse(dr["InPercent"].ToString());
        //            return boHasPromo;
        //        }

        //        /*******************************Up to Contact, Sub, Prod and VariationsMatrix ID only*****************************/
        //        SQL = "SELECT " +
        //                    "PromoItemsID, " +
        //                    "a.PromoID, " +
        //                    "PromoTypeID, " +
        //                    "ProductGroupID, " +
        //                    "ProductSubGroupID, " +
        //                    "ProductID,  " +
        //                    "VariationMatrixID, " +
        //                    "Quantity,  " +
        //                    "PromoValue,  " +
        //                    "InPercent  " +
        //                "FROM tblPromoItems a  " +
        //                "INNER JOIN tblPromo b ON a.PromoID = b.PromoID " +
        //                "WHERE ContactID = @ContactID " +
        //                    "AND ProductGroupID = 0 " +
        //                    "AND ProductSubGroupID = @ProductSubGroupID " +
        //                    "AND ProductID = @ProductID " +
        //                    "AND VariationMatrixID = @VariationMatrixID " +
        //                    "AND Status = 1 " +
        //                    "AND DATE_FORMAT(StartDate, '%Y-%m-%d %H:%i') <= DATE_FORMAT(NOW(), '%Y-%m-%d %H:%i') " +
        //                    "AND DATE_FORMAT(EndDate, '%Y-%m-%d %H:%i') >= DATE_FORMAT(NOW(), '%Y-%m-%d %H:%i');";

        //        cmd = new MySqlCommand();
        //        cmd.CommandType = System.Data.CommandType.Text;

        //        cmd.Parameters.AddWithValue("@ContactID", ContactID);
        //        cmd.Parameters.AddWithValue("@ProductSubGroupID", ProductSubGroupID);
        //        cmd.Parameters.AddWithValue("@ProductID", ProductID);
        //        cmd.Parameters.AddWithValue("@VariationMatrixID", VariationMatrixID);

        //        cmd.CommandText = SQL;
        //        dt = new System.Data.DataTable(strDataTableName);
        //        base.MySqlDataAdapterFill(cmd, dt);

        //        foreach (System.Data.DataRow dr in dt.Rows)
        //        {
        //            PromoType = (PromoTypes)Enum.Parse(typeof(PromoTypes), dr["PromoTypeID"].ToString());
        //            PromoQuantity = decimal.Parse(dr["Quantity"].ToString());
        //            PromoValue = decimal.Parse(dr["PromoValue"].ToString());
        //            InPercent = bool.Parse(dr["InPercent"].ToString());
        //            return boHasPromo;
        //        }

        //        /*******************************Up to Contact, Prod and VariationsMatrix ID only*****************************/
        //        SQL = "SELECT " +
        //                    "PromoItemsID, " +
        //                    "a.PromoID, " +
        //                    "PromoTypeID, " +
        //                    "ProductGroupID, " +
        //                    "ProductSubGroupID, " +
        //                    "ProductID,  " +
        //                    "VariationMatrixID, " +
        //                    "Quantity,  " +
        //                    "PromoValue,  " +
        //                    "InPercent  " +
        //                "FROM tblPromoItems a  " +
        //                "INNER JOIN tblPromo b ON a.PromoID = b.PromoID " +
        //                "WHERE ContactID = @ContactID " +
        //                    "AND ProductGroupID = 0 " +
        //                    "AND ProductSubGroupID = 0 " +
        //                    "AND ProductID = @ProductID " +
        //                    "AND VariationMatrixID = @VariationMatrixID " +
        //                    "AND Status = 1 " +
        //                    "AND DATE_FORMAT(StartDate, '%Y-%m-%d %H:%i') <= DATE_FORMAT(NOW(), '%Y-%m-%d %H:%i') " +
        //                    "AND DATE_FORMAT(EndDate, '%Y-%m-%d %H:%i') >= DATE_FORMAT(NOW(), '%Y-%m-%d %H:%i');";

        //        cmd = new MySqlCommand();
        //        cmd.CommandType = System.Data.CommandType.Text;

        //        cmd.Parameters.AddWithValue("@ContactID", ContactID);
        //        cmd.Parameters.AddWithValue("@ProductID", ProductID);
        //        cmd.Parameters.AddWithValue("@VariationMatrixID", VariationMatrixID);

        //        cmd.CommandText = SQL;
        //        dt = new System.Data.DataTable(strDataTableName);
        //        base.MySqlDataAdapterFill(cmd, dt);

        //        foreach (System.Data.DataRow dr in dt.Rows)
        //        {
        //            PromoType = (PromoTypes)Enum.Parse(typeof(PromoTypes), dr["PromoTypeID"].ToString());
        //            PromoQuantity = decimal.Parse(dr["Quantity"].ToString());
        //            PromoValue = decimal.Parse(dr["PromoValue"].ToString());
        //            InPercent = bool.Parse(dr["InPercent"].ToString());
        //            return boHasPromo;
        //        }

        //        /*******************************Up to Contact, VariationsMatrix ID only*****************************/
        //        SQL = "SELECT " +
        //                    "PromoItemsID, " +
        //                    "a.PromoID, " +
        //                    "PromoTypeID, " +
        //                    "ProductGroupID, " +
        //                    "ProductSubGroupID, " +
        //                    "ProductID,  " +
        //                    "VariationMatrixID, " +
        //                    "Quantity,  " +
        //                    "PromoValue,  " +
        //                    "InPercent  " +
        //                "FROM tblPromoItems a  " +
        //                "INNER JOIN tblPromo b ON a.PromoID = b.PromoID " +
        //                "WHERE ContactID = @ContactID " +
        //                    "AND ProductGroupID = 0 " +
        //                    "AND ProductSubGroupID = 0 " +
        //                    "AND ProductID = 0 " +
        //                    "AND VariationMatrixID = @VariationMatrixID " +
        //                    "AND Status = 1 " +
        //                    "AND DATE_FORMAT(StartDate, '%Y-%m-%d %H:%i') <= DATE_FORMAT(NOW(), '%Y-%m-%d %H:%i') " +
        //                    "AND DATE_FORMAT(EndDate, '%Y-%m-%d %H:%i') >= DATE_FORMAT(NOW(), '%Y-%m-%d %H:%i');";

        //        cmd = new MySqlCommand();
        //        cmd.CommandType = System.Data.CommandType.Text;

        //        cmd.Parameters.AddWithValue("@ContactID", ContactID);
        //        cmd.Parameters.AddWithValue("@VariationMatrixID", VariationMatrixID);

        //        cmd.CommandText = SQL;
        //        dt = new System.Data.DataTable(strDataTableName);
        //        base.MySqlDataAdapterFill(cmd, dt);

        //        foreach (System.Data.DataRow dr in dt.Rows)
        //        {
        //            PromoType = (PromoTypes)Enum.Parse(typeof(PromoTypes), dr["PromoTypeID"].ToString());
        //            PromoQuantity = decimal.Parse(dr["Quantity"].ToString());
        //            PromoValue = decimal.Parse(dr["PromoValue"].ToString());
        //            InPercent = bool.Parse(dr["InPercent"].ToString());
        //            return boHasPromo;
        //        }

        //        /*******************************Up to Contact, Group, Sub, Prod ID only*****************************/
        //        SQL = "SELECT " +
        //                    "PromoItemsID, " +
        //                    "a.PromoID, " +
        //                    "PromoTypeID, " +
        //                    "ProductGroupID, " +
        //                    "ProductSubGroupID, " +
        //                    "ProductID,  " +
        //                    "VariationMatrixID, " +
        //                    "Quantity,  " +
        //                    "PromoValue,  " +
        //                    "InPercent  " +
        //                "FROM tblPromoItems a  " +
        //                "INNER JOIN tblPromo b ON a.PromoID = b.PromoID " +
        //                "WHERE ContactID = @ContactID " +
        //                    "AND ProductGroupID = @ProductGroupID " +
        //                    "AND ProductSubGroupID = @ProductSubGroupID " +
        //                    "AND ProductID = @ProductID " +
        //                    "AND VariationMatrixID = 0 " +
        //                    "AND Status = 1 " +
        //                    "AND DATE_FORMAT(StartDate, '%Y-%m-%d %H:%i') <= DATE_FORMAT(NOW(), '%Y-%m-%d %H:%i') " +
        //                    "AND DATE_FORMAT(EndDate, '%Y-%m-%d %H:%i') >= DATE_FORMAT(NOW(), '%Y-%m-%d %H:%i');";

        //        cmd = new MySqlCommand();
        //        cmd.CommandType = System.Data.CommandType.Text;

        //        cmd.Parameters.AddWithValue("@ContactID", ContactID);
        //        cmd.Parameters.AddWithValue("@ProductGroupID", ProductGroupID);
        //        cmd.Parameters.AddWithValue("@ProductSubGroupID", ProductSubGroupID);
        //        cmd.Parameters.AddWithValue("@ProductID", ProductID);

        //        cmd.CommandText = SQL;
        //        dt = new System.Data.DataTable(strDataTableName);
        //        base.MySqlDataAdapterFill(cmd, dt);

        //        foreach (System.Data.DataRow dr in dt.Rows)
        //        {
        //            PromoType = (PromoTypes)Enum.Parse(typeof(PromoTypes), dr["PromoTypeID"].ToString());
        //            PromoQuantity = decimal.Parse(dr["Quantity"].ToString());
        //            PromoValue = decimal.Parse(dr["PromoValue"].ToString());
        //            InPercent = bool.Parse(dr["InPercent"].ToString());
        //            return boHasPromo;
        //        }

        //        /*******************************Up to Contact, Sub, Prod ID only*****************************/
        //        SQL = "SELECT " +
        //                    "PromoItemsID, " +
        //                    "a.PromoID, " +
        //                    "PromoTypeID, " +
        //                    "ProductGroupID, " +
        //                    "ProductSubGroupID, " +
        //                    "ProductID,  " +
        //                    "VariationMatrixID, " +
        //                    "Quantity,  " +
        //                    "PromoValue,  " +
        //                    "InPercent  " +
        //                "FROM tblPromoItems a  " +
        //                "INNER JOIN tblPromo b ON a.PromoID = b.PromoID " +
        //                "WHERE ContactID = @ContactID " +
        //                    "AND ProductGroupID = 0 " +
        //                    "AND ProductSubGroupID = @ProductSubGroupID " +
        //                    "AND ProductID = @ProductID " +
        //                    "AND VariationMatrixID = 0 " +
        //                    "AND Status = 1 " +
        //                    "AND DATE_FORMAT(StartDate, '%Y-%m-%d %H:%i') <= DATE_FORMAT(NOW(), '%Y-%m-%d %H:%i') " +
        //                    "AND DATE_FORMAT(EndDate, '%Y-%m-%d %H:%i') >= DATE_FORMAT(NOW(), '%Y-%m-%d %H:%i');";

        //        cmd = new MySqlCommand();
        //        cmd.CommandType = System.Data.CommandType.Text;

        //        cmd.Parameters.AddWithValue("@ContactID", ContactID);
        //        cmd.Parameters.AddWithValue("@ProductSubGroupID", ProductSubGroupID);
        //        cmd.Parameters.AddWithValue("@ProductID", ProductID);

        //        cmd.CommandText = SQL;
        //        dt = new System.Data.DataTable(strDataTableName);
        //        base.MySqlDataAdapterFill(cmd, dt);

        //        foreach (System.Data.DataRow dr in dt.Rows)
        //        {
        //            PromoType = (PromoTypes)Enum.Parse(typeof(PromoTypes), dr["PromoTypeID"].ToString());
        //            PromoQuantity = decimal.Parse(dr["Quantity"].ToString());
        //            PromoValue = decimal.Parse(dr["PromoValue"].ToString());
        //            InPercent = bool.Parse(dr["InPercent"].ToString());
        //            return boHasPromo;
        //        }

        //        /*******************************Up to Contact, Prod ID only*****************************/
        //        SQL = "SELECT " +
        //                    "PromoItemsID, " +
        //                    "a.PromoID, " +
        //                    "PromoTypeID, " +
        //                    "ProductGroupID, " +
        //                    "ProductSubGroupID, " +
        //                    "ProductID,  " +
        //                    "VariationMatrixID, " +
        //                    "Quantity,  " +
        //                    "PromoValue,  " +
        //                    "InPercent  " +
        //                "FROM tblPromoItems a  " +
        //                "INNER JOIN tblPromo b ON a.PromoID = b.PromoID " +
        //                "WHERE ContactID = @ContactID " +
        //                    "AND ProductGroupID = 0 " +
        //                    "AND ProductSubGroupID = 0 " +
        //                    "AND ProductID = @ProductID " +
        //                    "AND VariationMatrixID = 0 " +
        //                    "AND Status = 1 " +
        //                    "AND DATE_FORMAT(StartDate, '%Y-%m-%d %H:%i') <= DATE_FORMAT(NOW(), '%Y-%m-%d %H:%i') " +
        //                    "AND DATE_FORMAT(EndDate, '%Y-%m-%d %H:%i') >= DATE_FORMAT(NOW(), '%Y-%m-%d %H:%i');";

        //        cmd = new MySqlCommand();
        //        cmd.CommandType = System.Data.CommandType.Text;

        //        cmd.Parameters.AddWithValue("@ContactID", ContactID);
        //        cmd.Parameters.AddWithValue("@ProductID", ProductID);

        //        cmd.CommandText = SQL;
        //        dt = new System.Data.DataTable(strDataTableName);
        //        base.MySqlDataAdapterFill(cmd, dt);

        //        foreach (System.Data.DataRow dr in dt.Rows)
        //        {
        //            PromoType = (PromoTypes)Enum.Parse(typeof(PromoTypes), dr["PromoTypeID"].ToString());
        //            PromoQuantity = decimal.Parse(dr["Quantity"].ToString());
        //            PromoValue = decimal.Parse(dr["PromoValue"].ToString());
        //            InPercent = bool.Parse(dr["InPercent"].ToString());
        //            return boHasPromo;
        //        }

        //        /*******************************Up to Contact, Group, Sub only*****************************/
        //        SQL = "SELECT " +
        //                    "PromoItemsID, " +
        //                    "a.PromoID, " +
        //                    "PromoTypeID, " +
        //                    "ProductGroupID, " +
        //                    "ProductSubGroupID, " +
        //                    "ProductID,  " +
        //                    "VariationMatrixID, " +
        //                    "Quantity,  " +
        //                    "PromoValue,  " +
        //                    "InPercent  " +
        //                "FROM tblPromoItems a  " +
        //                "INNER JOIN tblPromo b ON a.PromoID = b.PromoID " +
        //                "WHERE ContactID = @ContactID " +
        //                    "AND ProductGroupID = @ProductGroupID " +
        //                    "AND ProductSubGroupID = @ProductSubGroupID " +
        //                    "AND ProductID = 0 " +
        //                    "AND VariationMatrixID = 0 " +
        //                    "AND Status = 1 " +
        //                    "AND DATE_FORMAT(StartDate, '%Y-%m-%d %H:%i') <= DATE_FORMAT(NOW(), '%Y-%m-%d %H:%i') " +
        //                    "AND DATE_FORMAT(EndDate, '%Y-%m-%d %H:%i') >= DATE_FORMAT(NOW(), '%Y-%m-%d %H:%i');";

        //        cmd = new MySqlCommand();
        //        cmd.CommandType = System.Data.CommandType.Text;

        //        cmd.Parameters.AddWithValue("@ContactID", ContactID);
        //        cmd.Parameters.AddWithValue("@ProductGroupID", ProductGroupID);
        //        cmd.Parameters.AddWithValue("@ProductSubGroupID", ProductSubGroupID);

        //        cmd.CommandText = SQL;
        //        dt = new System.Data.DataTable(strDataTableName);
        //        base.MySqlDataAdapterFill(cmd, dt);

        //        foreach (System.Data.DataRow dr in dt.Rows)
        //        {
        //            PromoType = (PromoTypes)Enum.Parse(typeof(PromoTypes), dr["PromoTypeID"].ToString());
        //            PromoQuantity = decimal.Parse(dr["Quantity"].ToString());
        //            PromoValue = decimal.Parse(dr["PromoValue"].ToString());
        //            InPercent = bool.Parse(dr["InPercent"].ToString());
        //            return boHasPromo;
        //        }

        //        /*******************************Up to Contact, Sub only*****************************/
        //        SQL = "SELECT " +
        //                    "PromoItemsID, " +
        //                    "a.PromoID, " +
        //                    "PromoTypeID, " +
        //                    "ProductGroupID, " +
        //                    "ProductSubGroupID, " +
        //                    "ProductID,  " +
        //                    "VariationMatrixID, " +
        //                    "Quantity,  " +
        //                    "PromoValue,  " +
        //                    "InPercent  " +
        //                "FROM tblPromoItems a  " +
        //                "INNER JOIN tblPromo b ON a.PromoID = b.PromoID " +
        //                "WHERE ContactID = @ContactID " +
        //                    "AND ProductGroupID = 0 " +
        //                    "AND ProductSubGroupID = @ProductSubGroupID " +
        //                    "AND ProductID = 0 " +
        //                    "AND VariationMatrixID = 0 " +
        //                    "AND Status = 1 " +
        //                    "AND DATE_FORMAT(StartDate, '%Y-%m-%d %H:%i') <= DATE_FORMAT(NOW(), '%Y-%m-%d %H:%i') " +
        //                    "AND DATE_FORMAT(EndDate, '%Y-%m-%d %H:%i') >= DATE_FORMAT(NOW(), '%Y-%m-%d %H:%i');";

        //        cmd = new MySqlCommand();
        //        cmd.CommandType = System.Data.CommandType.Text;

        //        cmd.Parameters.AddWithValue("@ContactID", ContactID);
        //        cmd.Parameters.AddWithValue("@ProductSubGroupID", ProductSubGroupID);

        //        cmd.CommandText = SQL;
        //        dt = new System.Data.DataTable(strDataTableName);
        //        base.MySqlDataAdapterFill(cmd, dt);

        //        foreach (System.Data.DataRow dr in dt.Rows)
        //        {
        //            PromoType = (PromoTypes)Enum.Parse(typeof(PromoTypes), dr["PromoTypeID"].ToString());
        //            PromoQuantity = decimal.Parse(dr["Quantity"].ToString());
        //            PromoValue = decimal.Parse(dr["PromoValue"].ToString());
        //            InPercent = bool.Parse(dr["InPercent"].ToString());
        //            return boHasPromo;
        //        }

        //        /*******************************Up to Contact only*****************************/
        //        SQL = "SELECT " +
        //                    "PromoItemsID, " +
        //                    "a.PromoID, " +
        //                    "PromoTypeID, " +
        //                    "ProductGroupID, " +
        //                    "ProductSubGroupID, " +
        //                    "ProductID,  " +
        //                    "VariationMatrixID, " +
        //                    "Quantity,  " +
        //                    "PromoValue,  " +
        //                    "InPercent  " +
        //                "FROM tblPromoItems a  " +
        //                "INNER JOIN tblPromo b ON a.PromoID = b.PromoID " +
        //                "WHERE ContactID = @ContactID " +
        //                    "AND ProductGroupID = 0 " +
        //                    "AND ProductSubGroupID = 0 " +
        //                    "AND ProductID = 0 " +
        //                    "AND VariationMatrixID = 0 " +
        //                    "AND Status = 1 " +
        //                    "AND DATE_FORMAT(StartDate, '%Y-%m-%d %H:%i') <= DATE_FORMAT(NOW(), '%Y-%m-%d %H:%i') " +
        //                    "AND DATE_FORMAT(EndDate, '%Y-%m-%d %H:%i') >= DATE_FORMAT(NOW(), '%Y-%m-%d %H:%i');";

        //        cmd = new MySqlCommand();
        //        cmd.CommandType = System.Data.CommandType.Text;

        //        cmd.Parameters.AddWithValue("@ContactID", ContactID);

        //        cmd.CommandText = SQL;
        //        dt = new System.Data.DataTable(strDataTableName);
        //        base.MySqlDataAdapterFill(cmd, dt);

        //        foreach (System.Data.DataRow dr in dt.Rows)
        //        {
        //            PromoType = (PromoTypes)Enum.Parse(typeof(PromoTypes), dr["PromoTypeID"].ToString());
        //            PromoQuantity = decimal.Parse(dr["Quantity"].ToString());
        //            PromoValue = decimal.Parse(dr["PromoValue"].ToString());
        //            InPercent = bool.Parse(dr["InPercent"].ToString());
        //            return boHasPromo;
        //        }

        //        /*******************************Up to Group, Sub, Prod and VarM ID only*****************************/
        //        SQL = "SELECT " +
        //                    "PromoItemsID, " +
        //                    "a.PromoID, " +
        //                    "PromoTypeID, " +
        //                    "ProductGroupID, " +
        //                    "ProductSubGroupID, " +
        //                    "ProductID,  " +
        //                    "VariationMatrixID, " +
        //                    "Quantity,  " +
        //                    "PromoValue,  " +
        //                    "InPercent  " +
        //                "FROM tblPromoItems a  " +
        //                "INNER JOIN tblPromo b ON a.PromoID = b.PromoID " +
        //                "WHERE ContactID = 0 " +
        //                    "AND ProductGroupID = @ProductGroupID " +
        //                    "AND ProductSubGroupID = @ProductSubGroupID " +
        //                    "AND ProductID = @ProductID " +
        //                    "AND VariationMatrixID = @VariationMatrixID " +
        //                    "AND Status = 1 " +
        //                    "AND DATE_FORMAT(StartDate, '%Y-%m-%d %H:%i') <= DATE_FORMAT(NOW(), '%Y-%m-%d %H:%i') " +
        //                    "AND DATE_FORMAT(EndDate, '%Y-%m-%d %H:%i') >= DATE_FORMAT(NOW(), '%Y-%m-%d %H:%i');";

        //        cmd = new MySqlCommand();
        //        cmd.CommandType = System.Data.CommandType.Text;

        //        cmd.Parameters.AddWithValue("@ProductGroupID", ProductGroupID);
        //        cmd.Parameters.AddWithValue("@ProductSubGroupID", ProductSubGroupID);
        //        cmd.Parameters.AddWithValue("@ProductID", ProductID);
        //        cmd.Parameters.AddWithValue("@VariationMatrixID", VariationMatrixID);

        //        cmd.CommandText = SQL;
        //        dt = new System.Data.DataTable(strDataTableName);
        //        base.MySqlDataAdapterFill(cmd, dt);

        //        foreach (System.Data.DataRow dr in dt.Rows)
        //        {
        //            PromoType = (PromoTypes)Enum.Parse(typeof(PromoTypes), dr["PromoTypeID"].ToString());
        //            PromoQuantity = decimal.Parse(dr["Quantity"].ToString());
        //            PromoValue = decimal.Parse(dr["PromoValue"].ToString());
        //            InPercent = bool.Parse(dr["InPercent"].ToString());
        //            return boHasPromo;
        //        }

        //        /*******************************Up to Sub, Prod and VariationMatrix ID only*****************************/
        //        SQL = "SELECT " +
        //                    "PromoItemsID, " +
        //                    "a.PromoID, " +
        //                    "PromoTypeID, " +
        //                    "ProductGroupID, " +
        //                    "ProductSubGroupID, " +
        //                    "ProductID,  " +
        //                    "VariationMatrixID, " +
        //                    "Quantity,  " +
        //                    "PromoValue,  " +
        //                    "InPercent  " +
        //                "FROM tblPromoItems a  " +
        //                "INNER JOIN tblPromo b ON a.PromoID = b.PromoID " +
        //                "WHERE ContactID = 0 " +
        //                    "AND ProductGroupID =0 " +
        //                    "AND ProductSubGroupID = @ProductSubGroupID " +
        //                    "AND ProductID = @ProductID " +
        //                    "AND VariationMatrixID = @VariationMatrixID " +
        //                    "AND Status = 1 " +
        //                    "AND DATE_FORMAT(StartDate, '%Y-%m-%d %H:%i') <= DATE_FORMAT(NOW(), '%Y-%m-%d %H:%i') " +
        //                    "AND DATE_FORMAT(EndDate, '%Y-%m-%d %H:%i') >= DATE_FORMAT(NOW(), '%Y-%m-%d %H:%i');";

        //        cmd = new MySqlCommand();
        //        cmd.CommandType = System.Data.CommandType.Text;

        //        cmd.Parameters.AddWithValue("@ProductSubGroupID", ProductSubGroupID);
        //        cmd.Parameters.AddWithValue("@ProductID", ProductID);
        //        cmd.Parameters.AddWithValue("@VariationMatrixID", VariationMatrixID);

        //        cmd.CommandText = SQL;
        //        dt = new System.Data.DataTable(strDataTableName);
        //        base.MySqlDataAdapterFill(cmd, dt);

        //        foreach (System.Data.DataRow dr in dt.Rows)
        //        {
        //            PromoType = (PromoTypes)Enum.Parse(typeof(PromoTypes), dr["PromoTypeID"].ToString());
        //            PromoQuantity = decimal.Parse(dr["Quantity"].ToString());
        //            PromoValue = decimal.Parse(dr["PromoValue"].ToString());
        //            InPercent = bool.Parse(dr["InPercent"].ToString());
        //            return boHasPromo;
        //        }

        //        /*******************************Up to Prod and VariationMatrix ID only*****************************/
        //        SQL = "SELECT " +
        //                    "PromoItemsID, " +
        //                    "a.PromoID, " +
        //                    "PromoTypeID, " +
        //                    "ProductGroupID, " +
        //                    "ProductSubGroupID, " +
        //                    "ProductID,  " +
        //                    "VariationMatrixID, " +
        //                    "Quantity,  " +
        //                    "PromoValue,  " +
        //                    "InPercent  " +
        //                "FROM tblPromoItems a  " +
        //                "INNER JOIN tblPromo b ON a.PromoID = b.PromoID " +
        //                "WHERE ContactID = 0 " +
        //                    "AND ProductGroupID = 0 " +
        //                    "AND ProductSubGroupID = 0 " +
        //                    "AND ProductID = @ProductID " +
        //                    "AND VariationMatrixID = @VariationMatrixID " +
        //                    "AND Status = 1 " +
        //                    "AND DATE_FORMAT(StartDate, '%Y-%m-%d %H:%i') <= DATE_FORMAT(NOW(), '%Y-%m-%d %H:%i') " +
        //                    "AND DATE_FORMAT(EndDate, '%Y-%m-%d %H:%i') >= DATE_FORMAT(NOW(), '%Y-%m-%d %H:%i');";

        //        cmd = new MySqlCommand();
        //        cmd.CommandType = System.Data.CommandType.Text;

        //        cmd.Parameters.AddWithValue("@ProductID", ProductID);
        //        cmd.Parameters.AddWithValue("@VariationMatrixID", VariationMatrixID);

        //        cmd.CommandText = SQL;
        //        dt = new System.Data.DataTable(strDataTableName);
        //        base.MySqlDataAdapterFill(cmd, dt);

        //        foreach (System.Data.DataRow dr in dt.Rows)
        //        {
        //            PromoType = (PromoTypes)Enum.Parse(typeof(PromoTypes), dr["PromoTypeID"].ToString());
        //            PromoQuantity = decimal.Parse(dr["Quantity"].ToString());
        //            PromoValue = decimal.Parse(dr["PromoValue"].ToString());
        //            InPercent = bool.Parse(dr["InPercent"].ToString());
        //            return boHasPromo;
        //        }

        //        /*******************************Up to VariationsMatrix ID only*****************************/
        //        SQL = "SELECT " +
        //                    "PromoItemsID, " +
        //                    "a.PromoID, " +
        //                    "PromoTypeID, " +
        //                    "ProductGroupID, " +
        //                    "ProductSubGroupID, " +
        //                    "ProductID,  " +
        //                    "VariationMatrixID, " +
        //                    "Quantity,  " +
        //                    "PromoValue,  " +
        //                    "InPercent  " +
        //                "FROM tblPromoItems a  " +
        //                "INNER JOIN tblPromo b ON a.PromoID = b.PromoID " +
        //                "WHERE ContactID = 0 " +
        //                    "AND ProductGroupID = 0 " +
        //                    "AND ProductSubGroupID = 0 " +
        //                    "AND ProductID = 0 " +
        //                    "AND VariationMatrixID = @VariationMatrixID " +
        //                    "AND Status = 1 " +
        //                    "AND DATE_FORMAT(StartDate, '%Y-%m-%d %H:%i') <= DATE_FORMAT(NOW(), '%Y-%m-%d %H:%i') " +
        //                    "AND DATE_FORMAT(EndDate, '%Y-%m-%d %H:%i') >= DATE_FORMAT(NOW(), '%Y-%m-%d %H:%i');";

        //        cmd = new MySqlCommand();
        //        cmd.CommandType = System.Data.CommandType.Text;

        //        cmd.Parameters.AddWithValue("@VariationMatrixID", VariationMatrixID);

        //        cmd.CommandText = SQL;
        //        dt = new System.Data.DataTable(strDataTableName);
        //        base.MySqlDataAdapterFill(cmd, dt);

        //        foreach (System.Data.DataRow dr in dt.Rows)
        //        {
        //            PromoType = (PromoTypes)Enum.Parse(typeof(PromoTypes), dr["PromoTypeID"].ToString());
        //            PromoQuantity = decimal.Parse(dr["Quantity"].ToString());
        //            PromoValue = decimal.Parse(dr["PromoValue"].ToString());
        //            InPercent = bool.Parse(dr["InPercent"].ToString());
        //            return boHasPromo;
        //        }

        //        /*******************************Up to group, Sub, Prod ID only*****************************/
        //        SQL = "SELECT " +
        //                    "PromoItemsID, " +
        //                    "a.PromoID, " +
        //                    "PromoTypeID, " +
        //                    "ProductGroupID, " +
        //                    "ProductSubGroupID, " +
        //                    "ProductID,  " +
        //                    "VariationMatrixID, " +
        //                    "Quantity,  " +
        //                    "PromoValue,  " +
        //                    "InPercent  " +
        //                "FROM tblPromoItems a  " +
        //                "INNER JOIN tblPromo b ON a.PromoID = b.PromoID " +
        //                "WHERE ContactID = 0 " +
        //                    "AND ProductGroupID = @ProductGroupID " +
        //                    "AND ProductSubGroupID = @ProductSubGroupID " +
        //                    "AND ProductID = @ProductID " +
        //                    "AND VariationMatrixID = 0 " +
        //                    "AND Status = 1 " +
        //                    "AND DATE_FORMAT(StartDate, '%Y-%m-%d %H:%i') <= DATE_FORMAT(NOW(), '%Y-%m-%d %H:%i') " +
        //                    "AND DATE_FORMAT(EndDate, '%Y-%m-%d %H:%i') >= DATE_FORMAT(NOW(), '%Y-%m-%d %H:%i');";

        //        cmd = new MySqlCommand();
        //        cmd.CommandType = System.Data.CommandType.Text;

        //        cmd.Parameters.AddWithValue("@ProductGroupID", ProductGroupID);
        //        cmd.Parameters.AddWithValue("@ProductSubGroupID", ProductSubGroupID);
        //        cmd.Parameters.AddWithValue("@ProductID", ProductID);

        //        cmd.CommandText = SQL;
        //        dt = new System.Data.DataTable(strDataTableName);
        //        base.MySqlDataAdapterFill(cmd, dt);

        //        foreach (System.Data.DataRow dr in dt.Rows)
        //        {
        //            PromoType = (PromoTypes)Enum.Parse(typeof(PromoTypes), dr["PromoTypeID"].ToString());
        //            PromoQuantity = decimal.Parse(dr["Quantity"].ToString());
        //            PromoValue = decimal.Parse(dr["PromoValue"].ToString());
        //            InPercent = bool.Parse(dr["InPercent"].ToString());
        //            return boHasPromo;
        //        }

        //        /*******************************Up to Sub, Prod ID only*****************************/
        //        SQL = "SELECT " +
        //                    "PromoItemsID, " +
        //                    "a.PromoID, " +
        //                    "PromoTypeID, " +
        //                    "ProductGroupID, " +
        //                    "ProductSubGroupID, " +
        //                    "ProductID,  " +
        //                    "VariationMatrixID, " +
        //                    "Quantity,  " +
        //                    "PromoValue,  " +
        //                    "InPercent  " +
        //                "FROM tblPromoItems a  " +
        //                "INNER JOIN tblPromo b ON a.PromoID = b.PromoID " +
        //                "WHERE ContactID = 0 " +
        //                    "AND ProductGroupID = 0 " +
        //                    "AND ProductSubGroupID = @ProductSubGroupID " +
        //                    "AND ProductID = @ProductID " +
        //                    "AND VariationMatrixID = 0 " +
        //                    "AND Status = 1 " +
        //                    "AND DATE_FORMAT(StartDate, '%Y-%m-%d %H:%i') <= DATE_FORMAT(NOW(), '%Y-%m-%d %H:%i') " +
        //                    "AND DATE_FORMAT(EndDate, '%Y-%m-%d %H:%i') >= DATE_FORMAT(NOW(), '%Y-%m-%d %H:%i');";

        //        cmd = new MySqlCommand();
        //        cmd.CommandType = System.Data.CommandType.Text;

        //        cmd.Parameters.AddWithValue("@ProductSubGroupID", ProductSubGroupID);
        //        cmd.Parameters.AddWithValue("@ProductID", ProductID);

        //        cmd.CommandText = SQL;
        //        dt = new System.Data.DataTable(strDataTableName);
        //        base.MySqlDataAdapterFill(cmd, dt);

        //        foreach (System.Data.DataRow dr in dt.Rows)
        //        {
        //            PromoType = (PromoTypes)Enum.Parse(typeof(PromoTypes), dr["PromoTypeID"].ToString());
        //            PromoQuantity = decimal.Parse(dr["Quantity"].ToString());
        //            PromoValue = decimal.Parse(dr["PromoValue"].ToString());
        //            InPercent = bool.Parse(dr["InPercent"].ToString());
        //            return boHasPromo;
        //        }

        //        /*******************************Up to group, Sub, Prod and VariationMatrix ID only*****************************/
        //        SQL = "SELECT " +
        //                    "PromoItemsID, " +
        //                    "a.PromoID, " +
        //                    "PromoTypeID, " +
        //                    "ProductGroupID, " +
        //                    "ProductSubGroupID, " +
        //                    "ProductID,  " +
        //                    "VariationMatrixID, " +
        //                    "Quantity,  " +
        //                    "PromoValue,  " +
        //                    "InPercent  " +
        //                "FROM tblPromoItems a  " +
        //                "INNER JOIN tblPromo b ON a.PromoID = b.PromoID " +
        //                "WHERE ContactID = 0 " +
        //                    "AND ProductGroupID = 0 " +
        //                    "AND ProductSubGroupID = 0 " +
        //                    "AND ProductID = @ProductID " +
        //                    "AND VariationMatrixID = 0 " +
        //                    "AND Status = 1 " +
        //                    "AND DATE_FORMAT(StartDate, '%Y-%m-%d %H:%i') <= DATE_FORMAT(NOW(), '%Y-%m-%d %H:%i') " +
        //                    "AND DATE_FORMAT(EndDate, '%Y-%m-%d %H:%i') >= DATE_FORMAT(NOW(), '%Y-%m-%d %H:%i');";

        //        cmd = new MySqlCommand();
        //        cmd.CommandType = System.Data.CommandType.Text;

        //        cmd.Parameters.AddWithValue("@ProductID", ProductID);

        //        cmd.CommandText = SQL;
        //        dt = new System.Data.DataTable(strDataTableName);
        //        base.MySqlDataAdapterFill(cmd, dt);

        //        foreach (System.Data.DataRow dr in dt.Rows)
        //        {
        //            PromoType = (PromoTypes)Enum.Parse(typeof(PromoTypes), dr["PromoTypeID"].ToString());
        //            PromoQuantity = decimal.Parse(dr["Quantity"].ToString());
        //            PromoValue = decimal.Parse(dr["PromoValue"].ToString());
        //            InPercent = bool.Parse(dr["InPercent"].ToString());
        //            return boHasPromo;
        //        }

        //        /*******************************Up to group, Sub ID only*****************************/
        //        SQL = "SELECT " +
        //                    "PromoItemsID, " +
        //                    "a.PromoID, " +
        //                    "PromoTypeID, " +
        //                    "ProductGroupID, " +
        //                    "ProductSubGroupID, " +
        //                    "ProductID,  " +
        //                    "VariationMatrixID, " +
        //                    "Quantity,  " +
        //                    "PromoValue,  " +
        //                    "InPercent  " +
        //                "FROM tblPromoItems a  " +
        //                "INNER JOIN tblPromo b ON a.PromoID = b.PromoID " +
        //                "WHERE ContactID = 0 " +
        //                    "AND ProductGroupID = @ProductGroupID " +
        //                    "AND ProductSubGroupID = @ProductSubGroupID " +
        //                    "AND ProductID = 0 " +
        //                    "AND VariationMatrixID = 0 " +
        //                    "AND Status = 1 " +
        //                    "AND DATE_FORMAT(StartDate, '%Y-%m-%d %H:%i') <= DATE_FORMAT(NOW(), '%Y-%m-%d %H:%i') " +
        //                    "AND DATE_FORMAT(EndDate, '%Y-%m-%d %H:%i') >= DATE_FORMAT(NOW(), '%Y-%m-%d %H:%i');";

        //        cmd = new MySqlCommand();
        //        cmd.CommandType = System.Data.CommandType.Text;

        //        cmd.Parameters.AddWithValue("@ProductGroupID", ProductGroupID);
        //        cmd.Parameters.AddWithValue("@ProductSubGroupID", ProductSubGroupID);

        //        cmd.CommandText = SQL;
        //        dt = new System.Data.DataTable(strDataTableName);
        //        base.MySqlDataAdapterFill(cmd, dt);

        //        foreach (System.Data.DataRow dr in dt.Rows)
        //        {
        //            PromoType = (PromoTypes)Enum.Parse(typeof(PromoTypes), dr["PromoTypeID"].ToString());
        //            PromoQuantity = decimal.Parse(dr["Quantity"].ToString());
        //            PromoValue = decimal.Parse(dr["PromoValue"].ToString());
        //            InPercent = bool.Parse(dr["InPercent"].ToString());
        //            return boHasPromo;
        //        }

        //        /*******************************Up to Sub ID only*****************************/
        //        SQL = "SELECT " +
        //                    "PromoItemsID, " +
        //                    "a.PromoID, " +
        //                    "PromoTypeID, " +
        //                    "ProductGroupID, " +
        //                    "ProductSubGroupID, " +
        //                    "ProductID,  " +
        //                    "VariationMatrixID, " +
        //                    "Quantity,  " +
        //                    "PromoValue,  " +
        //                    "InPercent  " +
        //                "FROM tblPromoItems a  " +
        //                "INNER JOIN tblPromo b ON a.PromoID = b.PromoID " +
        //                "WHERE ContactID = 0 " +
        //                    "AND ProductGroupID = 0 " +
        //                    "AND ProductSubGroupID = @ProductSubGroupID " +
        //                    "AND ProductID = 0 " +
        //                    "AND VariationMatrixID = 0 " +
        //                    "AND Status = 1 " +
        //                    "AND DATE_FORMAT(StartDate, '%Y-%m-%d %H:%i') <= DATE_FORMAT(NOW(), '%Y-%m-%d %H:%i') " +
        //                    "AND DATE_FORMAT(EndDate, '%Y-%m-%d %H:%i') >= DATE_FORMAT(NOW(), '%Y-%m-%d %H:%i');";

        //        cmd = new MySqlCommand();
        //        cmd.CommandType = System.Data.CommandType.Text;

        //        cmd.Parameters.AddWithValue("@ProductSubGroupID", ProductSubGroupID);

        //        cmd.CommandText = SQL;
        //        dt = new System.Data.DataTable(strDataTableName);
        //        base.MySqlDataAdapterFill(cmd, dt);

        //        foreach (System.Data.DataRow dr in dt.Rows)
        //        {
        //            PromoType = (PromoTypes)Enum.Parse(typeof(PromoTypes), dr["PromoTypeID"].ToString());
        //            PromoQuantity = decimal.Parse(dr["Quantity"].ToString());
        //            PromoValue = decimal.Parse(dr["PromoValue"].ToString());
        //            InPercent = bool.Parse(dr["InPercent"].ToString());
        //            return boHasPromo;
        //        }

        //        /*******************************Up to group ID only*****************************/
        //        SQL = "SELECT " +
        //                    "PromoItemsID, " +
        //                    "a.PromoID, " +
        //                    "PromoTypeID, " +
        //                    "ProductGroupID, " +
        //                    "ProductSubGroupID, " +
        //                    "ProductID,  " +
        //                    "VariationMatrixID, " +
        //                    "Quantity,  " +
        //                    "PromoValue,  " +
        //                    "InPercent  " +
        //                "FROM tblPromoItems a  " +
        //                "INNER JOIN tblPromo b ON a.PromoID = b.PromoID " +
        //                "WHERE ContactID = 0 " +
        //                    "AND ProductGroupID = @ProductGroupID " +
        //                    "AND ProductSubGroupID = 0 " +
        //                    "AND ProductID = 0 " +
        //                    "AND VariationMatrixID = 0 " +
        //                    "AND Status = 1 " +
        //                    "AND DATE_FORMAT(StartDate, '%Y-%m-%d %H:%i') <= DATE_FORMAT(NOW(), '%Y-%m-%d %H:%i') " +
        //                    "AND DATE_FORMAT(EndDate, '%Y-%m-%d %H:%i') >= DATE_FORMAT(NOW(), '%Y-%m-%d %H:%i');";

        //        cmd = new MySqlCommand();
        //        cmd.CommandType = System.Data.CommandType.Text;

        //        cmd.Parameters.AddWithValue("@ProductGroupID", ProductGroupID);

        //        cmd.CommandText = SQL;
        //        dt = new System.Data.DataTable(strDataTableName);
        //        base.MySqlDataAdapterFill(cmd, dt);

        //        foreach (System.Data.DataRow dr in dt.Rows)
        //        {
        //            PromoType = (PromoTypes)Enum.Parse(typeof(PromoTypes), dr["PromoTypeID"].ToString());
        //            PromoQuantity = decimal.Parse(dr["Quantity"].ToString());
        //            PromoValue = decimal.Parse(dr["PromoValue"].ToString());
        //            InPercent = bool.Parse(dr["InPercent"].ToString());
        //            return boHasPromo;
        //        }

        //        /*******************************Up to all only*****************************/
        //        SQL = "SELECT " +
        //                    "PromoItemsID, " +
        //                    "a.PromoID, " +
        //                    "PromoTypeID, " +
        //                    "ProductGroupID, " +
        //                    "ProductSubGroupID, " +
        //                    "ProductID,  " +
        //                    "VariationMatrixID, " +
        //                    "Quantity,  " +
        //                    "PromoValue,  " +
        //                    "InPercent  " +
        //                "FROM tblPromoItems a  " +
        //                "INNER JOIN tblPromo b ON a.PromoID = b.PromoID " +
        //                "WHERE ContactID = 0 " +
        //                    "AND ProductGroupID = 0 " +
        //                    "AND ProductSubGroupID = 0 " +
        //                    "AND ProductID = 0 " +
        //                    "AND VariationMatrixID = 0 " +
        //                    "AND Status = 1 " +
        //                    "AND DATE_FORMAT(StartDate, '%Y-%m-%d %H:%i') <= DATE_FORMAT(NOW(), '%Y-%m-%d %H:%i') " +
        //                    "AND DATE_FORMAT(EndDate, '%Y-%m-%d %H:%i') >= DATE_FORMAT(NOW(), '%Y-%m-%d %H:%i');";

        //        cmd = new MySqlCommand();
        //        cmd.CommandType = System.Data.CommandType.Text;

        //        cmd.CommandText = SQL;
        //        dt = new System.Data.DataTable(strDataTableName);
        //        base.MySqlDataAdapterFill(cmd, dt);

        //        foreach (System.Data.DataRow dr in dt.Rows)
        //        {
        //            PromoType = (PromoTypes)Enum.Parse(typeof(PromoTypes), dr["PromoTypeID"].ToString());
        //            PromoQuantity = decimal.Parse(dr["Quantity"].ToString());
        //            PromoValue = decimal.Parse(dr["PromoValue"].ToString());
        //            InPercent = bool.Parse(dr["InPercent"].ToString());
        //            return boHasPromo;
        //        }

        //    }
        //    catch (Exception ex)
        //    {
        //        base.ThrowException(ex);
        //    }
        //    return false;
        //}

		public bool ApplyPromoValue(ContactDetails clsContactDetails, Int64 ProductID, Int64 VariationMatrixID, out PromoTypes PromoType, out decimal PromoQuantity, out decimal PromoValue, out bool InPercent, int BranchID = 0)
		{
            Int64 ContactID = clsContactDetails.ContactID;

            string strSpecialContactIDs = "";

            // rewardcardmember
            // make sure that it is not the default
            if (clsContactDetails.ContactID != Constants.C_RETAILPLUS_CUSTOMERID &&
                clsContactDetails.RewardDetails.RewardActive)
            {
                if (!string.IsNullOrEmpty(strSpecialContactIDs))
                    strSpecialContactIDs += "," + Constants.PLUSCARDMEMBERSID_STRING;
                else
                    strSpecialContactIDs = Constants.PLUSCARDMEMBERSID_STRING;
            }

            // icc card members
            // make sure that it is not the default
            if (clsContactDetails.ContactID != Constants.C_RETAILPLUS_CUSTOMERID && 
                clsContactDetails.CreditDetails.GuarantorID == Constants.ZERO &&
                clsContactDetails.CreditDetails.CreditActive)
            {
                if (!string.IsNullOrEmpty(strSpecialContactIDs))
                    strSpecialContactIDs += "," + Constants.ICCARDMEMBERSID_STRING;
                else
                    strSpecialContactIDs = Constants.ICCARDMEMBERSID_STRING;
            }

            // gcc card members
            // make sure that it is not the default
            if (clsContactDetails.ContactID != Constants.C_RETAILPLUS_CUSTOMERID && 
                clsContactDetails.CreditDetails.GuarantorID != Constants.ZERO &&
                clsContactDetails.CreditDetails.CreditActive)
            {
                if (!string.IsNullOrEmpty(strSpecialContactIDs))
                    strSpecialContactIDs += "," + Constants.GCCARDMEMBERSID_STRING;
                else
                    strSpecialContactIDs = Constants.GCCARDMEMBERSID_STRING;
            }
            

            PromoType = PromoTypes.NotApplicable;
            PromoQuantity = 0;
            PromoValue = 0;
            InPercent = false;

            bool boHasPromo = false;

            try
            {
                Data.Products clsProduct = new Data.Products(base.Connection, base.Transaction);
                Data.ProductDetails clsProductDetails = clsProduct.Details1(BranchID, ProductID);

                Int64 ProductSubGroupID = clsProductDetails.ProductSubGroupID;
                Int64 ProductGroupID = clsProductDetails.ProductGroupID;

                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandType = System.Data.CommandType.Text;

                string SQL = "SELECT " +
                                "PromoID  " +
                            "FROM tblPromo " +
                            "WHERE 1=1 " +
                                "AND Status = 1 " +
                                "AND DATE_FORMAT(StartDate, '%Y-%m-%d %H:%i') <= DATE_FORMAT(NOW(), '%Y-%m-%d %H:%i') " +
                                "AND DATE_FORMAT(EndDate, '%Y-%m-%d %H:%i') >= DATE_FORMAT(NOW(), '%Y-%m-%d %H:%i');";

                cmd.CommandText = SQL;
                string strDataTableName = "tbl" + this.GetType().FullName.Split(new Char[] { '.' })[this.GetType().FullName.Split(new Char[] { '.' }).Length - 1]; System.Data.DataTable dt = new System.Data.DataTable(strDataTableName);
                base.MySqlDataAdapterFill(cmd, dt);

                foreach (System.Data.DataRow dr in dt.Rows)
                {
                    boHasPromo = true;
                    break;
                }

                if (boHasPromo == false)	//return agad if no Promo is affected by date
                    return boHasPromo;

                /*******************************Up to Contact, Group, Sub, Prod and VarM ID only*****************************/
                SQL = "SELECT " +
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
                        "WHERE " + (string.IsNullOrEmpty(strSpecialContactIDs) ? "ContactID = @ContactID " : "ContactID IN (@ContactID, " + strSpecialContactIDs + ") ") +
                            "AND ProductGroupID = @ProductGroupID " +
                            "AND ProductSubGroupID = @ProductSubGroupID " +
                            "AND ProductID = @ProductID " +
                            "AND VariationMatrixID = @VariationMatrixID " +
                            "AND Status = 1 " +
                            "AND DATE_FORMAT(StartDate, '%Y-%m-%d %H:%i') <= DATE_FORMAT(NOW(), '%Y-%m-%d %H:%i') " +
                            "AND DATE_FORMAT(EndDate, '%Y-%m-%d %H:%i') >= DATE_FORMAT(NOW(), '%Y-%m-%d %H:%i') ORDER BY PromoValue ASC LIMIT 1;";

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
                    PromoType = (PromoTypes)Enum.Parse(typeof(PromoTypes), dr["PromoTypeID"].ToString());
                    PromoQuantity = decimal.Parse(dr["Quantity"].ToString());
                    PromoValue = decimal.Parse(dr["PromoValue"].ToString());
                    InPercent = bool.Parse(dr["InPercent"].ToString());
                    return boHasPromo;
                }

                /*******************************Up to Contact, Sub, Prod and VariationsMatrix ID only*****************************/
                SQL = "SELECT " +
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
                        "WHERE " + (string.IsNullOrEmpty(strSpecialContactIDs) ? "ContactID = @ContactID " : "ContactID IN (@ContactID, " + strSpecialContactIDs + ") ") +
                            "AND ProductGroupID = 0 " +
                            "AND ProductSubGroupID = @ProductSubGroupID " +
                            "AND ProductID = @ProductID " +
                            "AND VariationMatrixID = @VariationMatrixID " +
                            "AND Status = 1 " +
                            "AND DATE_FORMAT(StartDate, '%Y-%m-%d %H:%i') <= DATE_FORMAT(NOW(), '%Y-%m-%d %H:%i') " +
                            "AND DATE_FORMAT(EndDate, '%Y-%m-%d %H:%i') >= DATE_FORMAT(NOW(), '%Y-%m-%d %H:%i') ORDER BY PromoValue ASC LIMIT 1;";

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
                    PromoType = (PromoTypes)Enum.Parse(typeof(PromoTypes), dr["PromoTypeID"].ToString());
                    PromoQuantity = decimal.Parse(dr["Quantity"].ToString());
                    PromoValue = decimal.Parse(dr["PromoValue"].ToString());
                    InPercent = bool.Parse(dr["InPercent"].ToString());
                    return boHasPromo;
                }

                /*******************************Up to Contact, Prod and VariationsMatrix ID only*****************************/
                SQL = "SELECT " +
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
                        "WHERE " + (string.IsNullOrEmpty(strSpecialContactIDs) ? "ContactID = @ContactID " : "ContactID IN (@ContactID, " + strSpecialContactIDs + ") ") +
                            "AND ProductGroupID = 0 " +
                            "AND ProductSubGroupID = 0 " +
                            "AND ProductID = @ProductID " +
                            "AND VariationMatrixID = @VariationMatrixID " +
                            "AND Status = 1 " +
                            "AND DATE_FORMAT(StartDate, '%Y-%m-%d %H:%i') <= DATE_FORMAT(NOW(), '%Y-%m-%d %H:%i') " +
                            "AND DATE_FORMAT(EndDate, '%Y-%m-%d %H:%i') >= DATE_FORMAT(NOW(), '%Y-%m-%d %H:%i') ORDER BY PromoValue ASC LIMIT 1;";

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
                    PromoType = (PromoTypes)Enum.Parse(typeof(PromoTypes), dr["PromoTypeID"].ToString());
                    PromoQuantity = decimal.Parse(dr["Quantity"].ToString());
                    PromoValue = decimal.Parse(dr["PromoValue"].ToString());
                    InPercent = bool.Parse(dr["InPercent"].ToString());
                    return boHasPromo;
                }

                /*******************************Up to Contact, VariationsMatrix ID only*****************************/
                SQL = "SELECT " +
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
                        "WHERE " + (string.IsNullOrEmpty(strSpecialContactIDs) ? "ContactID = @ContactID " : "ContactID IN (@ContactID, " + strSpecialContactIDs + ") ") +
                            "AND ProductGroupID = 0 " +
                            "AND ProductSubGroupID = 0 " +
                            "AND ProductID = 0 " +
                            "AND VariationMatrixID = @VariationMatrixID " +
                            "AND Status = 1 " +
                            "AND DATE_FORMAT(StartDate, '%Y-%m-%d %H:%i') <= DATE_FORMAT(NOW(), '%Y-%m-%d %H:%i') " +
                            "AND DATE_FORMAT(EndDate, '%Y-%m-%d %H:%i') >= DATE_FORMAT(NOW(), '%Y-%m-%d %H:%i') ORDER BY PromoValue ASC LIMIT 1;";

                cmd = new MySqlCommand();
                cmd.CommandType = System.Data.CommandType.Text;

                cmd.Parameters.AddWithValue("@ContactID", ContactID);
                cmd.Parameters.AddWithValue("@VariationMatrixID", VariationMatrixID);

                cmd.CommandText = SQL;
                dt = new System.Data.DataTable(strDataTableName);
                base.MySqlDataAdapterFill(cmd, dt);

                foreach (System.Data.DataRow dr in dt.Rows)
                {
                    PromoType = (PromoTypes)Enum.Parse(typeof(PromoTypes), dr["PromoTypeID"].ToString());
                    PromoQuantity = decimal.Parse(dr["Quantity"].ToString());
                    PromoValue = decimal.Parse(dr["PromoValue"].ToString());
                    InPercent = bool.Parse(dr["InPercent"].ToString());
                    return boHasPromo;
                }

                /*******************************Up to Contact, Group, Sub, Prod ID only*****************************/
                SQL = "SELECT " +
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
                        "WHERE " + (string.IsNullOrEmpty(strSpecialContactIDs) ? "ContactID = @ContactID " : "ContactID IN (@ContactID, " + strSpecialContactIDs + ") ") +
                            "AND ProductGroupID = @ProductGroupID " +
                            "AND ProductSubGroupID = @ProductSubGroupID " +
                            "AND ProductID = @ProductID " +
                            "AND VariationMatrixID = 0 " +
                            "AND Status = 1 " +
                            "AND DATE_FORMAT(StartDate, '%Y-%m-%d %H:%i') <= DATE_FORMAT(NOW(), '%Y-%m-%d %H:%i') " +
                            "AND DATE_FORMAT(EndDate, '%Y-%m-%d %H:%i') >= DATE_FORMAT(NOW(), '%Y-%m-%d %H:%i') ORDER BY PromoValue ASC LIMIT 1;";

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
                    PromoType = (PromoTypes)Enum.Parse(typeof(PromoTypes), dr["PromoTypeID"].ToString());
                    PromoQuantity = decimal.Parse(dr["Quantity"].ToString());
                    PromoValue = decimal.Parse(dr["PromoValue"].ToString());
                    InPercent = bool.Parse(dr["InPercent"].ToString());
                    return boHasPromo;
                }

                /*******************************Up to Contact, Sub, Prod ID only*****************************/
                SQL = "SELECT " +
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
                        "WHERE " + (string.IsNullOrEmpty(strSpecialContactIDs) ? "ContactID = @ContactID " : "ContactID IN (@ContactID, " + strSpecialContactIDs + ") ") +
                            "AND ProductGroupID = 0 " +
                            "AND ProductSubGroupID = @ProductSubGroupID " +
                            "AND ProductID = @ProductID " +
                            "AND VariationMatrixID = 0 " +
                            "AND Status = 1 " +
                            "AND DATE_FORMAT(StartDate, '%Y-%m-%d %H:%i') <= DATE_FORMAT(NOW(), '%Y-%m-%d %H:%i') " +
                            "AND DATE_FORMAT(EndDate, '%Y-%m-%d %H:%i') >= DATE_FORMAT(NOW(), '%Y-%m-%d %H:%i') ORDER BY PromoValue ASC LIMIT 1;";

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
                    PromoType = (PromoTypes)Enum.Parse(typeof(PromoTypes), dr["PromoTypeID"].ToString());
                    PromoQuantity = decimal.Parse(dr["Quantity"].ToString());
                    PromoValue = decimal.Parse(dr["PromoValue"].ToString());
                    InPercent = bool.Parse(dr["InPercent"].ToString());
                    return boHasPromo;
                }

                /*******************************Up to Contact, Prod ID only*****************************/
                SQL = "SELECT " +
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
                        "WHERE " + (string.IsNullOrEmpty(strSpecialContactIDs) ? "ContactID = @ContactID " : "ContactID IN (@ContactID, " + strSpecialContactIDs + ") ") +
                            "AND ProductGroupID = 0 " +
                            "AND ProductSubGroupID = 0 " +
                            "AND ProductID = @ProductID " +
                            "AND VariationMatrixID = 0 " +
                            "AND Status = 1 " +
                            "AND DATE_FORMAT(StartDate, '%Y-%m-%d %H:%i') <= DATE_FORMAT(NOW(), '%Y-%m-%d %H:%i') " +
                            "AND DATE_FORMAT(EndDate, '%Y-%m-%d %H:%i') >= DATE_FORMAT(NOW(), '%Y-%m-%d %H:%i') ORDER BY PromoValue ASC LIMIT 1;";

                cmd = new MySqlCommand();
                cmd.CommandType = System.Data.CommandType.Text;

                cmd.Parameters.AddWithValue("@ContactID", ContactID);
                cmd.Parameters.AddWithValue("@ProductID", ProductID);

                cmd.CommandText = SQL;
                dt = new System.Data.DataTable(strDataTableName);
                base.MySqlDataAdapterFill(cmd, dt);

                foreach (System.Data.DataRow dr in dt.Rows)
                {
                    PromoType = (PromoTypes)Enum.Parse(typeof(PromoTypes), dr["PromoTypeID"].ToString());
                    PromoQuantity = decimal.Parse(dr["Quantity"].ToString());
                    PromoValue = decimal.Parse(dr["PromoValue"].ToString());
                    InPercent = bool.Parse(dr["InPercent"].ToString());
                    return boHasPromo;
                }

                /*******************************Up to Contact, Group, Sub only*****************************/
                SQL = "SELECT " +
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
                        "WHERE " + (string.IsNullOrEmpty(strSpecialContactIDs) ? "ContactID = @ContactID " : "ContactID IN (@ContactID, " + strSpecialContactIDs + ") ") +
                            "AND ProductGroupID = @ProductGroupID " +
                            "AND ProductSubGroupID = @ProductSubGroupID " +
                            "AND ProductID = 0 " +
                            "AND VariationMatrixID = 0 " +
                            "AND Status = 1 " +
                            "AND DATE_FORMAT(StartDate, '%Y-%m-%d %H:%i') <= DATE_FORMAT(NOW(), '%Y-%m-%d %H:%i') " +
                            "AND DATE_FORMAT(EndDate, '%Y-%m-%d %H:%i') >= DATE_FORMAT(NOW(), '%Y-%m-%d %H:%i') ORDER BY PromoValue ASC LIMIT 1;";

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
                    PromoType = (PromoTypes)Enum.Parse(typeof(PromoTypes), dr["PromoTypeID"].ToString());
                    PromoQuantity = decimal.Parse(dr["Quantity"].ToString());
                    PromoValue = decimal.Parse(dr["PromoValue"].ToString());
                    InPercent = bool.Parse(dr["InPercent"].ToString());
                    return boHasPromo;
                }

                /*******************************Up to Contact, Sub only*****************************/
                SQL = "SELECT " +
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
                        "WHERE " + (string.IsNullOrEmpty(strSpecialContactIDs) ? "ContactID = @ContactID " : "ContactID IN (@ContactID, " + strSpecialContactIDs + ") ") +
                            "AND ProductGroupID = 0 " +
                            "AND ProductSubGroupID = @ProductSubGroupID " +
                            "AND ProductID = 0 " +
                            "AND VariationMatrixID = 0 " +
                            "AND Status = 1 " +
                            "AND DATE_FORMAT(StartDate, '%Y-%m-%d %H:%i') <= DATE_FORMAT(NOW(), '%Y-%m-%d %H:%i') " +
                            "AND DATE_FORMAT(EndDate, '%Y-%m-%d %H:%i') >= DATE_FORMAT(NOW(), '%Y-%m-%d %H:%i') ORDER BY PromoValue ASC LIMIT 1;";

                cmd = new MySqlCommand();
                cmd.CommandType = System.Data.CommandType.Text;

                cmd.Parameters.AddWithValue("@ContactID", ContactID);
                cmd.Parameters.AddWithValue("@ProductSubGroupID", ProductSubGroupID);

                cmd.CommandText = SQL;
                dt = new System.Data.DataTable(strDataTableName);
                base.MySqlDataAdapterFill(cmd, dt);

                foreach (System.Data.DataRow dr in dt.Rows)
                {
                    PromoType = (PromoTypes)Enum.Parse(typeof(PromoTypes), dr["PromoTypeID"].ToString());
                    PromoQuantity = decimal.Parse(dr["Quantity"].ToString());
                    PromoValue = decimal.Parse(dr["PromoValue"].ToString());
                    InPercent = bool.Parse(dr["InPercent"].ToString());
                    return boHasPromo;
                }

                /*******************************Up to Contact only*****************************/
                SQL = "SELECT " +
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
                        "WHERE " + (string.IsNullOrEmpty(strSpecialContactIDs) ? "ContactID = @ContactID " : "ContactID IN (@ContactID, " + strSpecialContactIDs + ") ") +
                            "AND ProductGroupID = 0 " +
                            "AND ProductSubGroupID = 0 " +
                            "AND ProductID = 0 " +
                            "AND VariationMatrixID = 0 " +
                            "AND Status = 1 " +
                            "AND DATE_FORMAT(StartDate, '%Y-%m-%d %H:%i') <= DATE_FORMAT(NOW(), '%Y-%m-%d %H:%i') " +
                            "AND DATE_FORMAT(EndDate, '%Y-%m-%d %H:%i') >= DATE_FORMAT(NOW(), '%Y-%m-%d %H:%i') ORDER BY PromoValue ASC LIMIT 1;";

                cmd = new MySqlCommand();
                cmd.CommandType = System.Data.CommandType.Text;

                cmd.Parameters.AddWithValue("@ContactID", ContactID);

                cmd.CommandText = SQL;
                dt = new System.Data.DataTable(strDataTableName);
                base.MySqlDataAdapterFill(cmd, dt);

                foreach (System.Data.DataRow dr in dt.Rows)
                {
                    PromoType = (PromoTypes)Enum.Parse(typeof(PromoTypes), dr["PromoTypeID"].ToString());
                    PromoQuantity = decimal.Parse(dr["Quantity"].ToString());
                    PromoValue = decimal.Parse(dr["PromoValue"].ToString());
                    InPercent = bool.Parse(dr["InPercent"].ToString());
                    return boHasPromo;
                }

                /*******************************Up to Group, Sub, Prod and VarM ID only*****************************/
                SQL = "SELECT " +
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
                        "WHERE " + (string.IsNullOrEmpty(strSpecialContactIDs) ? "ContactID = 0 " : "ContactID IN (0, " + strSpecialContactIDs + ") ") +
                            "AND ProductGroupID = @ProductGroupID " +
                            "AND ProductSubGroupID = @ProductSubGroupID " +
                            "AND ProductID = @ProductID " +
                            "AND VariationMatrixID = @VariationMatrixID " +
                            "AND Status = 1 " +
                            "AND DATE_FORMAT(StartDate, '%Y-%m-%d %H:%i') <= DATE_FORMAT(NOW(), '%Y-%m-%d %H:%i') " +
                            "AND DATE_FORMAT(EndDate, '%Y-%m-%d %H:%i') >= DATE_FORMAT(NOW(), '%Y-%m-%d %H:%i') ORDER BY PromoValue ASC LIMIT 1;";

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
                    PromoType = (PromoTypes)Enum.Parse(typeof(PromoTypes), dr["PromoTypeID"].ToString());
                    PromoQuantity = decimal.Parse(dr["Quantity"].ToString());
                    PromoValue = decimal.Parse(dr["PromoValue"].ToString());
                    InPercent = bool.Parse(dr["InPercent"].ToString());
                    return boHasPromo;
                }

                /*******************************Up to Sub, Prod and VariationMatrix ID only*****************************/
                SQL = "SELECT " +
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
                        "WHERE " + (string.IsNullOrEmpty(strSpecialContactIDs) ? "ContactID = 0 " : "ContactID IN (0, " + strSpecialContactIDs + ") ") +
                            "AND ProductGroupID =0 " +
                            "AND ProductSubGroupID = @ProductSubGroupID " +
                            "AND ProductID = @ProductID " +
                            "AND VariationMatrixID = @VariationMatrixID " +
                            "AND Status = 1 " +
                            "AND DATE_FORMAT(StartDate, '%Y-%m-%d %H:%i') <= DATE_FORMAT(NOW(), '%Y-%m-%d %H:%i') " +
                            "AND DATE_FORMAT(EndDate, '%Y-%m-%d %H:%i') >= DATE_FORMAT(NOW(), '%Y-%m-%d %H:%i') ORDER BY PromoValue ASC LIMIT 1;";

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
                    PromoType = (PromoTypes)Enum.Parse(typeof(PromoTypes), dr["PromoTypeID"].ToString());
                    PromoQuantity = decimal.Parse(dr["Quantity"].ToString());
                    PromoValue = decimal.Parse(dr["PromoValue"].ToString());
                    InPercent = bool.Parse(dr["InPercent"].ToString());
                    return boHasPromo;
                }

                /*******************************Up to Prod and VariationMatrix ID only*****************************/
                SQL = "SELECT " +
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
                        "WHERE " + (string.IsNullOrEmpty(strSpecialContactIDs) ? "ContactID = 0 " : "ContactID IN (0, " + strSpecialContactIDs + ") ") +
                            "AND ProductGroupID = 0 " +
                            "AND ProductSubGroupID = 0 " +
                            "AND ProductID = @ProductID " +
                            "AND VariationMatrixID = @VariationMatrixID " +
                            "AND Status = 1 " +
                            "AND DATE_FORMAT(StartDate, '%Y-%m-%d %H:%i') <= DATE_FORMAT(NOW(), '%Y-%m-%d %H:%i') " +
                            "AND DATE_FORMAT(EndDate, '%Y-%m-%d %H:%i') >= DATE_FORMAT(NOW(), '%Y-%m-%d %H:%i') ORDER BY PromoValue ASC LIMIT 1;";

                cmd = new MySqlCommand();
                cmd.CommandType = System.Data.CommandType.Text;

                cmd.Parameters.AddWithValue("@ProductID", ProductID);
                cmd.Parameters.AddWithValue("@VariationMatrixID", VariationMatrixID);

                cmd.CommandText = SQL;
                dt = new System.Data.DataTable(strDataTableName);
                base.MySqlDataAdapterFill(cmd, dt);

                foreach (System.Data.DataRow dr in dt.Rows)
                {
                    PromoType = (PromoTypes)Enum.Parse(typeof(PromoTypes), dr["PromoTypeID"].ToString());
                    PromoQuantity = decimal.Parse(dr["Quantity"].ToString());
                    PromoValue = decimal.Parse(dr["PromoValue"].ToString());
                    InPercent = bool.Parse(dr["InPercent"].ToString());
                    return boHasPromo;
                }

                /*******************************Up to VariationsMatrix ID only*****************************/
                SQL = "SELECT " +
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
                        "WHERE " + (string.IsNullOrEmpty(strSpecialContactIDs) ? "ContactID = 0 " : "ContactID IN (0, " + strSpecialContactIDs + ") ") +
                            "AND ProductGroupID = 0 " +
                            "AND ProductSubGroupID = 0 " +
                            "AND ProductID = 0 " +
                            "AND VariationMatrixID = @VariationMatrixID " +
                            "AND Status = 1 " +
                            "AND DATE_FORMAT(StartDate, '%Y-%m-%d %H:%i') <= DATE_FORMAT(NOW(), '%Y-%m-%d %H:%i') " +
                            "AND DATE_FORMAT(EndDate, '%Y-%m-%d %H:%i') >= DATE_FORMAT(NOW(), '%Y-%m-%d %H:%i') ORDER BY PromoValue ASC LIMIT 1;";

                cmd = new MySqlCommand();
                cmd.CommandType = System.Data.CommandType.Text;

                cmd.Parameters.AddWithValue("@VariationMatrixID", VariationMatrixID);

                cmd.CommandText = SQL;
                dt = new System.Data.DataTable(strDataTableName);
                base.MySqlDataAdapterFill(cmd, dt);

                foreach (System.Data.DataRow dr in dt.Rows)
                {
                    PromoType = (PromoTypes)Enum.Parse(typeof(PromoTypes), dr["PromoTypeID"].ToString());
                    PromoQuantity = decimal.Parse(dr["Quantity"].ToString());
                    PromoValue = decimal.Parse(dr["PromoValue"].ToString());
                    InPercent = bool.Parse(dr["InPercent"].ToString());
                    return boHasPromo;
                }

                /*******************************Up to group, Sub, Prod ID only*****************************/
                SQL = "SELECT " +
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
                        "WHERE " + (string.IsNullOrEmpty(strSpecialContactIDs) ? "ContactID = 0 " : "ContactID IN (0, " + strSpecialContactIDs + ") ") +
                            "AND ProductGroupID = @ProductGroupID " +
                            "AND ProductSubGroupID = @ProductSubGroupID " +
                            "AND ProductID = @ProductID " +
                            "AND VariationMatrixID = 0 " +
                            "AND Status = 1 " +
                            "AND DATE_FORMAT(StartDate, '%Y-%m-%d %H:%i') <= DATE_FORMAT(NOW(), '%Y-%m-%d %H:%i') " +
                            "AND DATE_FORMAT(EndDate, '%Y-%m-%d %H:%i') >= DATE_FORMAT(NOW(), '%Y-%m-%d %H:%i') ORDER BY PromoValue ASC LIMIT 1;";

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
                    PromoType = (PromoTypes)Enum.Parse(typeof(PromoTypes), dr["PromoTypeID"].ToString());
                    PromoQuantity = decimal.Parse(dr["Quantity"].ToString());
                    PromoValue = decimal.Parse(dr["PromoValue"].ToString());
                    InPercent = bool.Parse(dr["InPercent"].ToString());
                    return boHasPromo;
                }

                /*******************************Up to Sub, Prod ID only*****************************/
                SQL = "SELECT " +
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
                        "WHERE " + (string.IsNullOrEmpty(strSpecialContactIDs) ? "ContactID = 0 " : "ContactID IN (0, " + strSpecialContactIDs + ") ") +
                            "AND ProductGroupID = 0 " +
                            "AND ProductSubGroupID = @ProductSubGroupID " +
                            "AND ProductID = @ProductID " +
                            "AND VariationMatrixID = 0 " +
                            "AND Status = 1 " +
                            "AND DATE_FORMAT(StartDate, '%Y-%m-%d %H:%i') <= DATE_FORMAT(NOW(), '%Y-%m-%d %H:%i') " +
                            "AND DATE_FORMAT(EndDate, '%Y-%m-%d %H:%i') >= DATE_FORMAT(NOW(), '%Y-%m-%d %H:%i') ORDER BY PromoValue ASC LIMIT 1;";

                cmd = new MySqlCommand();
                cmd.CommandType = System.Data.CommandType.Text;

                cmd.Parameters.AddWithValue("@ProductSubGroupID", ProductSubGroupID);
                cmd.Parameters.AddWithValue("@ProductID", ProductID);

                cmd.CommandText = SQL;
                dt = new System.Data.DataTable(strDataTableName);
                base.MySqlDataAdapterFill(cmd, dt);

                foreach (System.Data.DataRow dr in dt.Rows)
                {
                    PromoType = (PromoTypes)Enum.Parse(typeof(PromoTypes), dr["PromoTypeID"].ToString());
                    PromoQuantity = decimal.Parse(dr["Quantity"].ToString());
                    PromoValue = decimal.Parse(dr["PromoValue"].ToString());
                    InPercent = bool.Parse(dr["InPercent"].ToString());
                    return boHasPromo;
                }

                /*******************************Up to group, Sub, Prod and VariationMatrix ID only*****************************/
                SQL = "SELECT " +
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
                        "WHERE " + (string.IsNullOrEmpty(strSpecialContactIDs) ? "ContactID = 0 " : "ContactID IN (0, " + strSpecialContactIDs + ") ") +
                            "AND ProductGroupID = 0 " +
                            "AND ProductSubGroupID = 0 " +
                            "AND ProductID = @ProductID " +
                            "AND VariationMatrixID = 0 " +
                            "AND Status = 1 " +
                            "AND DATE_FORMAT(StartDate, '%Y-%m-%d %H:%i') <= DATE_FORMAT(NOW(), '%Y-%m-%d %H:%i') " +
                            "AND DATE_FORMAT(EndDate, '%Y-%m-%d %H:%i') >= DATE_FORMAT(NOW(), '%Y-%m-%d %H:%i') ORDER BY PromoValue ASC LIMIT 1;";

                cmd = new MySqlCommand();
                cmd.CommandType = System.Data.CommandType.Text;

                cmd.Parameters.AddWithValue("@ProductID", ProductID);

                cmd.CommandText = SQL;
                dt = new System.Data.DataTable(strDataTableName);
                base.MySqlDataAdapterFill(cmd, dt);

                foreach (System.Data.DataRow dr in dt.Rows)
                {
                    PromoType = (PromoTypes)Enum.Parse(typeof(PromoTypes), dr["PromoTypeID"].ToString());
                    PromoQuantity = decimal.Parse(dr["Quantity"].ToString());
                    PromoValue = decimal.Parse(dr["PromoValue"].ToString());
                    InPercent = bool.Parse(dr["InPercent"].ToString());
                    return boHasPromo;
                }

                /*******************************Up to group, Sub ID only*****************************/
                SQL = "SELECT " +
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
                        "WHERE " + (string.IsNullOrEmpty(strSpecialContactIDs) ? "ContactID = 0 " : "ContactID IN (0, " + strSpecialContactIDs + ") ") +
                            "AND ProductGroupID = @ProductGroupID " +
                            "AND ProductSubGroupID = @ProductSubGroupID " +
                            "AND ProductID = 0 " +
                            "AND VariationMatrixID = 0 " +
                            "AND Status = 1 " +
                            "AND DATE_FORMAT(StartDate, '%Y-%m-%d %H:%i') <= DATE_FORMAT(NOW(), '%Y-%m-%d %H:%i') " +
                            "AND DATE_FORMAT(EndDate, '%Y-%m-%d %H:%i') >= DATE_FORMAT(NOW(), '%Y-%m-%d %H:%i') ORDER BY PromoValue ASC LIMIT 1;";

                cmd = new MySqlCommand();
                cmd.CommandType = System.Data.CommandType.Text;

                cmd.Parameters.AddWithValue("@ProductGroupID", ProductGroupID);
                cmd.Parameters.AddWithValue("@ProductSubGroupID", ProductSubGroupID);

                cmd.CommandText = SQL;
                dt = new System.Data.DataTable(strDataTableName);
                base.MySqlDataAdapterFill(cmd, dt);

                foreach (System.Data.DataRow dr in dt.Rows)
                {
                    PromoType = (PromoTypes)Enum.Parse(typeof(PromoTypes), dr["PromoTypeID"].ToString());
                    PromoQuantity = decimal.Parse(dr["Quantity"].ToString());
                    PromoValue = decimal.Parse(dr["PromoValue"].ToString());
                    InPercent = bool.Parse(dr["InPercent"].ToString());
                    return boHasPromo;
                }

                /*******************************Up to Sub ID only*****************************/
                SQL = "SELECT " +
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
                        "WHERE " + (string.IsNullOrEmpty(strSpecialContactIDs) ? "ContactID = 0 " : "ContactID IN (0, " + strSpecialContactIDs + ") ") +
                            "AND ProductGroupID = 0 " +
                            "AND ProductSubGroupID = @ProductSubGroupID " +
                            "AND ProductID = 0 " +
                            "AND VariationMatrixID = 0 " +
                            "AND Status = 1 " +
                            "AND DATE_FORMAT(StartDate, '%Y-%m-%d %H:%i') <= DATE_FORMAT(NOW(), '%Y-%m-%d %H:%i') " +
                            "AND DATE_FORMAT(EndDate, '%Y-%m-%d %H:%i') >= DATE_FORMAT(NOW(), '%Y-%m-%d %H:%i') ORDER BY PromoValue ASC LIMIT 1;";

                cmd = new MySqlCommand();
                cmd.CommandType = System.Data.CommandType.Text;

                cmd.Parameters.AddWithValue("@ProductSubGroupID", ProductSubGroupID);

                cmd.CommandText = SQL;
                dt = new System.Data.DataTable(strDataTableName);
                base.MySqlDataAdapterFill(cmd, dt);

                foreach (System.Data.DataRow dr in dt.Rows)
                {
                    PromoType = (PromoTypes)Enum.Parse(typeof(PromoTypes), dr["PromoTypeID"].ToString());
                    PromoQuantity = decimal.Parse(dr["Quantity"].ToString());
                    PromoValue = decimal.Parse(dr["PromoValue"].ToString());
                    InPercent = bool.Parse(dr["InPercent"].ToString());
                    return boHasPromo;
                }

                /*******************************Up to group ID only*****************************/
                SQL = "SELECT " +
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
                        "WHERE " + (string.IsNullOrEmpty(strSpecialContactIDs) ? "ContactID = 0 " : "ContactID IN (0, " + strSpecialContactIDs + ") ") +
                            "AND ProductGroupID = @ProductGroupID " +
                            "AND ProductSubGroupID = 0 " +
                            "AND ProductID = 0 " +
                            "AND VariationMatrixID = 0 " +
                            "AND Status = 1 " +
                            "AND DATE_FORMAT(StartDate, '%Y-%m-%d %H:%i') <= DATE_FORMAT(NOW(), '%Y-%m-%d %H:%i') " +
                            "AND DATE_FORMAT(EndDate, '%Y-%m-%d %H:%i') >= DATE_FORMAT(NOW(), '%Y-%m-%d %H:%i') ORDER BY PromoValue ASC LIMIT 1;";

                cmd = new MySqlCommand();
                cmd.CommandType = System.Data.CommandType.Text;

                cmd.Parameters.AddWithValue("@ProductGroupID", ProductGroupID);

                cmd.CommandText = SQL;
                dt = new System.Data.DataTable(strDataTableName);
                base.MySqlDataAdapterFill(cmd, dt);

                foreach (System.Data.DataRow dr in dt.Rows)
                {
                    PromoType = (PromoTypes)Enum.Parse(typeof(PromoTypes), dr["PromoTypeID"].ToString());
                    PromoQuantity = decimal.Parse(dr["Quantity"].ToString());
                    PromoValue = decimal.Parse(dr["PromoValue"].ToString());
                    InPercent = bool.Parse(dr["InPercent"].ToString());
                    return boHasPromo;
                }

                /*******************************Up to all only*****************************/
                SQL = "SELECT " +
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
                        "WHERE " + (string.IsNullOrEmpty(strSpecialContactIDs) ? "ContactID = 0 " : "ContactID IN (0, " + strSpecialContactIDs + ") ") +
                            "AND ProductGroupID = 0 " +
                            "AND ProductSubGroupID = 0 " +
                            "AND ProductID = 0 " +
                            "AND VariationMatrixID = 0 " +
                            "AND Status = 1 " +
                            "AND DATE_FORMAT(StartDate, '%Y-%m-%d %H:%i') <= DATE_FORMAT(NOW(), '%Y-%m-%d %H:%i') " +
                            "AND DATE_FORMAT(EndDate, '%Y-%m-%d %H:%i') >= DATE_FORMAT(NOW(), '%Y-%m-%d %H:%i') ORDER BY PromoValue ASC LIMIT 1;";

                cmd = new MySqlCommand();
                cmd.CommandType = System.Data.CommandType.Text;

                cmd.CommandText = SQL;
                dt = new System.Data.DataTable(strDataTableName);
                base.MySqlDataAdapterFill(cmd, dt);

                foreach (System.Data.DataRow dr in dt.Rows)
                {
                    PromoType = (PromoTypes)Enum.Parse(typeof(PromoTypes), dr["PromoTypeID"].ToString());
                    PromoQuantity = decimal.Parse(dr["Quantity"].ToString());
                    PromoValue = decimal.Parse(dr["PromoValue"].ToString());
                    InPercent = bool.Parse(dr["InPercent"].ToString());
                    return boHasPromo;
                }

            }
            catch (Exception ex)
            {
                base.ThrowException(ex);
            }
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


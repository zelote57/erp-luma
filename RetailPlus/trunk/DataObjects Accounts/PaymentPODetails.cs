
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
    public struct PaymentPODetailDetails
	{
		public long PaymentDetailID;
		public long PaymentID;
		public long POID;
		public decimal Amount;
        public POPaymentStatus PaymentStatus;
	}

	[StrongNameIdentityPermissionAttribute(SecurityAction.LinkDemand,
		 PublicKey = "002400000480000094000000060200000024000" +
		 "052534131000400000100010053D785642F9F960B43157E0380" +
		 "F393BEE53E8DFAFBF441366C1B6F8B48D9DDF0D527B1F3B21EA" +
		 "E85D2FDB664CE81EB8A87DBE4C589D6F4202FE2B7D4B978BB69" +
		 "684874612CB9B8DB7A0339400A9C4E68277884B07817363D242" +
		 "E3696F9FACDBEA831810AE6DC9EDCA91A7B5DA12FE7BF65D113" +
		 "FF52834EAFB5A7A1FDFD5851A3")]
	public class PaymentPODetails : POSConnection
	{
		#region Constructors and Destructors

		public PaymentPODetails()
            : base(null, null)
        {
        }

        public PaymentPODetails(MySqlConnection Connection, MySqlTransaction Transaction) 
            : base(Connection, Transaction)
		{

		}

		#endregion

		#region Insert and Update

        public long Insert(PaymentPODetailDetails Details)
		{
			try 
			{
				string SQL = "INSERT INTO tblPaymentPODetails (" +
								"PaymentID, " +
								"POID, " +
								"Amount, " +
                                "PaymentStatus" +
							") VALUES (" +
								"@PaymentID, " +
								"@POID, " +
								"@Amount, " +
                                "@PaymentStatus" +
							");";
				  
				
	 			
				MySqlCommand cmd = new MySqlCommand();
				
				
				cmd.CommandType = System.Data.CommandType.Text;
				cmd.CommandText = SQL;

				MySqlParameter prmPaymentID = new MySqlParameter("@PaymentID",MySqlDbType.Int64);			
				prmPaymentID.Value = Details.PaymentID;
				cmd.Parameters.Add(prmPaymentID);

				MySqlParameter prmPOID = new MySqlParameter("@POID",MySqlDbType.Int64);			
				prmPOID.Value = Details.POID;
				cmd.Parameters.Add(prmPOID);

				MySqlParameter prmAmount = new MySqlParameter("@Amount",MySqlDbType.Decimal);			
				prmAmount.Value = Details.Amount;
				cmd.Parameters.Add(prmAmount);

                MySqlParameter prmPaymentStatus = new MySqlParameter("@PaymentStatus",MySqlDbType.Int16);
                prmPaymentStatus.Value = Details.PaymentStatus.ToString("d");
                cmd.Parameters.Add(prmPaymentStatus);

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

		#endregion

		#region Delete

		public bool Delete(string IDs)
		{
			try 
			{
				string SQL=	"DELETE FROM tblPaymentPODetails WHERE PaymentDetailID IN (" + IDs + ");";
				  
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
        public bool Delete(long POID)
        {
            try
            {
                string SQL = "DELETE FROM tblPaymentPODetails WHERE POID = @POID;";

                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandText = SQL;

                MySqlParameter prmPOID = new MySqlParameter("@POID",MySqlDbType.Int64);
                prmPOID.Value = POID;
                cmd.Parameters.Add(prmPOID);

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
                                "PaymentDetailID, " +
                                "PaymentID, " +
                                "a.POID, " +
                                "PONo, " +
                                "PODate, " +
                                "SupplierID, " +
                                "SupplierCode, " +
                                "SupplierContact, " +
                                "SupplierModeOfTerms, " +
                                "SupplierTerms, " +
                                "SupplierDRNo, " +
                                "DeliveryDate, " +
                                "BranchCode, " +
                                "a.Amount, " +
                                "b.PaidAmount, " +
                                "b.UnpaidAmount, " +
                                "b.PaymentStatus " +
                            "FROM tblPaymentPODetails a " +
                                "INNER JOIN tblPO b ON a.POID = b.POID " +
                                "INNER JOIN tblBranch c ON b.BranchID = c.BranchID ";
            return stSQL;
        }

		#region Details

        public PaymentPODetailDetails Details(long PaymentDetailID)
		{
			try
			{
				string SQL =	SQLSelect() + "WHERE PaymentDetailID = @PaymentDetailID;";
				  
                MySqlCommand cmd = new MySqlCommand();
				cmd.CommandType = System.Data.CommandType.Text;
				cmd.CommandText = SQL;

				MySqlParameter prmPaymentDetailID = new MySqlParameter("@PaymentDetailID",MySqlDbType.Int64);			
				prmPaymentDetailID.Value = PaymentDetailID;
				cmd.Parameters.Add(prmPaymentDetailID);

				MySqlDataReader myReader = base.ExecuteReader(cmd, System.Data.CommandBehavior.SingleResult);

                PaymentPODetailDetails Details = new PaymentPODetailDetails();

				while (myReader.Read()) 
				{
					Details.PaymentDetailID = PaymentDetailID;
					Details.PaymentID = myReader.GetInt64("PaymentID");
					Details.POID = myReader.GetInt32("POID");
					Details.Amount = myReader.GetDecimal("Amount");
                    Details.PaymentStatus = (POPaymentStatus) Enum.Parse(typeof(POPaymentStatus), myReader.GetString("PaymentStatus"));
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

		public MySqlDataReader List(string SortField, SortOption SortOrder)
		{
			try
			{
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
		public MySqlDataReader List(long PaymentID, string SortField, SortOption SortOrder)
		{
			try
			{
                string SQL = SQLSelect() + "WHERE PaymentID = @PaymentID " +
							                "ORDER BY " + SortField;

				if (SortOrder == SortOption.Ascending)
					SQL += " ASC";
				else
					SQL += " DESC";

				MySqlCommand cmd = new MySqlCommand();
				cmd.CommandType = System.Data.CommandType.Text;
				cmd.CommandText = SQL;
				
				MySqlParameter prmPaymentID = new MySqlParameter("@PaymentID",MySqlDbType.Int64);			
				prmPaymentID.Value = PaymentID;
				cmd.Parameters.Add(prmPaymentID);

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
                string SQL = SQLSelect() + "WHERE PaymentDetailID = @SearchKey " +
								            "or SupplierCode LIKE @SearchKey " +
								            "or PONo LIKE @SearchKey " +
							            "ORDER BY " + SortField;

				if (SortOrder == SortOption.Ascending)
					SQL += " ASC";
				else
					SQL += " DESC";

				MySqlCommand cmd = new MySqlCommand();
				cmd.CommandType = System.Data.CommandType.Text;
				cmd.CommandText = SQL;

				MySqlParameter prmSearchKey = new MySqlParameter("@SearchKey",MySqlDbType.String);
				prmSearchKey.Value = "%" + SearchKey +"%";
				cmd.Parameters.Add(prmSearchKey);

				MySqlDataReader myReader = base.ExecuteReader(cmd);
				
				return myReader;			
			}
			catch (Exception ex)
			{
				throw base.ThrowException(ex);
			}	
		}		
		public MySqlDataReader Search(long PaymentID, string SearchKey, string SortField, SortOption SortOrder)
		{
			try
			{
				string SQL =SQLSelect() + "WHERE PaymentID = @PaymentID " +
                                             "or SupplierCode LIKE @SearchKey " +
                                            "or PONo LIKE @SearchKey " +
                                        "ORDER BY " + SortField;

				if (SortOrder == SortOption.Ascending)
					SQL += " ASC";
				else
					SQL += " DESC";

				MySqlCommand cmd = new MySqlCommand();
				cmd.CommandType = System.Data.CommandType.Text;
				cmd.CommandText = SQL;
				
				MySqlParameter prmPaymentID = new MySqlParameter("@PaymentID",MySqlDbType.Int64);			
				prmPaymentID.Value = PaymentID;
				cmd.Parameters.Add(prmPaymentID);

				MySqlParameter prmSearchKey = new MySqlParameter("@SearchKey",MySqlDbType.String);
				prmSearchKey.Value = "%" + SearchKey +"%";
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


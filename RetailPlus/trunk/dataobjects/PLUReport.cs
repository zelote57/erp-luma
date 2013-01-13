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

	public struct PLUReportDetails
	{
		public long PLUReportID;
		public string TerminalNo;
		public Int64 ProductID;
		public string ProductCode;
		public decimal Quantity;
		public decimal Amount;
        public OrderSlipPrinter OrderSlipPrinter;
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
	public class PLUReport : POSConnection
    {
		#region Constructors and Destructors

		public PLUReport()
            : base(null, null)
        {
        }

        public PLUReport(MySqlConnection Connection, MySqlTransaction Transaction) 
            : base(Connection, Transaction)
		{

		}

		#endregion

		#region Insert and Update

		public Int64 Insert(PLUReportDetails Details)
		{
			try  
			{
				string SQL =	"INSERT INTO tblPLUReport (" +
									"TerminalNo, " +
									"ProductID, " + 
									"ProductCode, " + 
									"Quantity, " +  
									"Amount, " +
                                    "OrderSlipPrinter" +
								") VALUES (" +
									"@TerminalNo, " +
									"@ProductID, " + 
									"@ProductCode, " + 
									"@Quantity, " +  
									"@Amount," +
                                    "@OrderSlipPrinter);"; 

				  
				
	 			
				MySqlCommand cmd = new MySqlCommand();
				
				
				cmd.CommandType = System.Data.CommandType.Text;
				cmd.CommandText = SQL;
				
				MySqlParameter prmTerminalNo = new MySqlParameter("@TerminalNo",MySqlDbType.String);			
				prmTerminalNo.Value = Details.TerminalNo;
				cmd.Parameters.Add(prmTerminalNo);

				MySqlParameter prmProductID = new MySqlParameter("@ProductID",MySqlDbType.Int64);			
				prmProductID.Value = Details.ProductID;
				cmd.Parameters.Add(prmProductID);

				MySqlParameter prmProductCode = new MySqlParameter("@ProductCode",MySqlDbType.String);			
				prmProductCode.Value = Details.ProductCode;
				cmd.Parameters.Add(prmProductCode);

				MySqlParameter prmQuantity = new MySqlParameter("@Quantity",MySqlDbType.Decimal);			
				prmQuantity.Value = Details.Quantity;
				cmd.Parameters.Add(prmQuantity);

				MySqlParameter prmAmount = new MySqlParameter("@Amount",MySqlDbType.Decimal);			
				prmAmount.Value = Details.Amount;
				cmd.Parameters.Add(prmAmount);

                MySqlParameter prmOrderSlipPrinter = new MySqlParameter("@OrderSlipPrinter",MySqlDbType.Int16);
                prmOrderSlipPrinter.Value = (int)Details.OrderSlipPrinter;
                cmd.Parameters.Add(prmOrderSlipPrinter);

				base.ExecuteNonQuery(cmd);

				SQL = "SELECT LAST_INSERT_ID();";
				
				cmd.Parameters.Clear(); 
				cmd.CommandText = SQL;
				
				MySqlDataReader myReader = base.ExecuteReader(cmd, System.Data.CommandBehavior.SingleResult);
				
				Int64 iID = 0;

				while (myReader.Read()) 
				{
					iID = myReader.GetInt64(0);
				}

				myReader.Close();

				return iID;
			}

			catch (Exception ex)
			{
				
				
				{
					
					
					
					
				}

				throw ex;
			}	
		}

		#endregion

		#region Delete

		public bool Delete(string TerminalNo)
		{
			try 
			{
				
				MySqlCommand cmd = new MySqlCommand();
				string SQL;

				SQL=	"DELETE FROM tblPLUReport WHERE TerminalNo = '" + TerminalNo + "';";
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

				throw ex;
			}	
		}


		#endregion

		#region Streams

		private string SQLSelect()
		{
			string SQL =	"SELECT " +
								"TerminalNo, " +
								"ProductCode, " +
								"SUM(Quantity) 'Quantity', " +
								"SUM(Amount) 'Amount' " +
							"FROM tblPLUReport " + 
							"GROUP BY TerminalNo, ProductCode ";
			return SQL;
		}

		public System.Data.DataTable dtList(string TerminalNo, string SortField, SortOption SortOrder)
		{
			try
			{
                string SQL =    "SELECT " +
                                    "TerminalNo, " +
                                    "OrderSlipPrinter, " +
                                    "ProductCode, " +
                                    "SUM(Quantity) 'Quantity', " +
                                    "SUM(Amount) 'Amount' " +
                                "FROM tblPLUReport " +
                                "WHERE TerminalNo = @TerminalNo " +
                                "GROUP BY TerminalNo, OrderSlipPrinter, ProductCode ";

				SQL = SQL + "ORDER BY " + SortField; 
				
				if (SortOrder == SortOption.Ascending)
					SQL += " ASC";
				else
					SQL += " DESC";

				

				MySqlCommand cmd = new MySqlCommand();
				
				
				cmd.CommandType = System.Data.CommandType.Text;
				cmd.CommandText = SQL;
				
				MySqlParameter prmTerminalNo = new MySqlParameter("@TerminalNo",MySqlDbType.String);			
				prmTerminalNo.Value = TerminalNo;
				cmd.Parameters.Add(prmTerminalNo);



                //System.Data.DataTable dt = new System.Data.DataTable("tblPLUReport");

                //dt.Columns.Add("TerminalNo");
                //dt.Columns.Add("OrderSlipPrinter");
                //dt.Columns.Add("ProductCode");
                //dt.Columns.Add("Quantity");
                //dt.Columns.Add("Amount");

                //while (myReader.Read())
                //{
                //    System.Data.DataRow dr = dt.NewRow();

                //    dr["TerminalNo"] = "" + myReader["TerminalNo"].ToString();
                //    dr["OrderSlipPrinter"] = myReader.GetInt16("OrderSlipPrinter").ToString();
                //    dr["ProductCode"] = "" + myReader["ProductCode"].ToString();
                //    dr["Quantity"] = myReader.GetDecimal("Quantity").ToString("#,##0.#0");
                //    dr["Amount"] = myReader.GetDecimal("Amount").ToString("#,##0.#0");
                //    dt.Rows.Add(dr);
                //}

                //myReader.Close();
                string strDataTableName = "tbl" + this.GetType().FullName.Split(new Char[] { '.' })[this.GetType().FullName.Split(new Char[] { '.' }).Length - 1]; System.Data.DataTable dt = new System.Data.DataTable(strDataTableName);
                base.MySqlDataAdapterFill(cmd, dt);

				return dt;				
			}
			catch (Exception ex)
			{
				throw ex;
			}	
		}

		
//		public void Generate(Int64 TerminalNo, string SortField, SortOption SortOrder)
//		{
//			try
//			{
//				string SQL = SQLSelect() + "WHERE TerminalNo = @TerminalNo ORDER BY " + SortField; 
//				
//				if (SortOrder == SortOption.Ascending)
//					SQL += " ASC";
//				else
//					SQL += " DESC";
//
//				
//
//				MySqlCommand cmd = new MySqlCommand();
//				
//				
//				cmd.CommandType = System.Data.CommandType.Text;
//				cmd.CommandText = SQL;
//				
//				MySqlParameter prmTerminalNo = new MySqlParameter("@TerminalNo",System.Data.DbType.Int64);			
//				prmTerminalNo.Value = TerminalNo;
//				cmd.Parameters.Add(prmTerminalNo);
//
//				
//				
//				System.Data.DataTable dt = new System.Data.DataTable("tblCompositionList");
//
//				dt.Columns.Add("TerminalNo");
//				dt.Columns.Add("ProductCode");
//				dt.Columns.Add("Quantity");
//				dt.Columns.Add("Amount");
//
//				while (myReader.Read())
//				{
//					System.Data.DataRow dr = dt.NewRow();
//
//					dr["TerminalNo"] = "" + myReader["TerminalNo"].ToString();
//					dr["ProductCode"] = "" + myReader["ProductCode"].ToString();
//					dr["Quantity"] = myReader.GetDecimal("Quantity").ToString("#,##0.#0");
//					dr["Amount"] = myReader.GetDecimal("Amount").ToString("#,##0.#0");
//					dt.Rows.Add(dr);
//				}
//
//				myReader.Close();
//
//				return dt;				
//			}
//			catch (Exception ex)
//			{
//				
//				
//				{
//					
//					
//					
//					
//				}
//
//				throw ex;
//			}	
//		}
//		
		#endregion
	}
}
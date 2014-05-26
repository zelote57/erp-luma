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
        public string ProductGroup;
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
                                    "ProductGroup, " + 
									"Quantity, " +  
									"Amount, " +
                                    "OrderSlipPrinter" +
								") VALUES (" +
									"@TerminalNo, " +
									"@ProductID, " + 
									"@ProductCode, " +
                                    "@ProductGroup, " + 
                                    "@Quantity, " +  
									"@Amount," +
                                    "@OrderSlipPrinter);"; 

				MySqlCommand cmd = new MySqlCommand();
				cmd.CommandType = System.Data.CommandType.Text;
				cmd.CommandText = SQL;

                cmd.Parameters.AddWithValue("@TerminalNo", Details.TerminalNo);
                cmd.Parameters.AddWithValue("@ProductID", Details.ProductID);
                cmd.Parameters.AddWithValue("@ProductCode", Details.ProductCode);
                cmd.Parameters.AddWithValue("@ProductGroup", Details.ProductGroup);
                cmd.Parameters.AddWithValue("@Quantity", Details.Quantity);
                cmd.Parameters.AddWithValue("@Amount", Details.Amount);
                cmd.Parameters.AddWithValue("@OrderSlipPrinter", (int) Details.OrderSlipPrinter);

				base.ExecuteNonQuery(cmd);

				SQL = "SELECT LAST_INSERT_ID();";
				
				cmd.Parameters.Clear(); 
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

				throw base.ThrowException(ex);
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

		public System.Data.DataTable dtList(string TerminalNo, string SortField, SortOption SortOrder, bool isPerGroup = false)
		{
			try
			{
                string SQL =    "SELECT " +
                                    "TerminalNo, " +
                                    "OrderSlipPrinter, " +
                                    "ProductGroup, " +
                                    "ProductCode, " +
                                    "SUM(Quantity) 'Quantity', " +
                                    "SUM(Amount) 'Amount' " +
                                "FROM tblPLUReport " +
                                "WHERE TerminalNo = @TerminalNo " +
                                "GROUP BY TerminalNo, OrderSlipPrinter, ProductGroup, ProductCode ";

				SQL = SQL + "ORDER BY " + SortField; 
				
				if (SortOrder == SortOption.Ascending)
					SQL += " ASC";
				else
					SQL += " DESC";

                if (isPerGroup)
                {
                    SQL = "SELECT " +
                                    "TerminalNo, " +
                                    "OrderSlipPrinter, " +
                                    "ProductGroup, " +
                                    "SUM(Quantity) 'Quantity', " +
                                    "SUM(Amount) 'Amount' " +
                                "FROM tblPLUReport " +
                                "WHERE TerminalNo = @TerminalNo " +
                                "GROUP BY TerminalNo, OrderSlipPrinter, ProductGroup " +
                                "ORDER BY TerminalNo, OrderSlipPrinter, ProductGroup ";
                }

				MySqlCommand cmd = new MySqlCommand();
				cmd.CommandType = System.Data.CommandType.Text;
				cmd.CommandText = SQL;

                cmd.Parameters.AddWithValue("@TerminalNo", TerminalNo);

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
	}
}
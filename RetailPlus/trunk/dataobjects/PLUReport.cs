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
        public BranchDetails BranchDetails;
        public string TerminalNo;
		public Int64 PLUReportID;
		public Int64 ProductID;
		public string ProductCode;
        public string ProductGroup;
        public decimal Quantity;
		public decimal Amount;
        public OrderSlipPrinter OrderSlipPrinter;

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

        /// <summary>
        /// This should only be called by GeneratePLUReport. 
        /// tblPLUReport is a temp table for session reporting purposes only.
        /// </summary>
        /// <param name="Details"></param>
        /// <returns></returns>
		public Int64 Insert(PLUReportDetails Details)
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

        public Int32 Save(PLUReportDetails Details)
        {
            try
            {
                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandType = System.Data.CommandType.Text;

                string SQL = "CALL procSavePLUReport(@BranchID, @TerminalNo, @PLUReportID, @ProductID, @ProductCode, @ProductGroup, @Quantity, @Amount, @OrderSlipPrinter, @CreatedOn, @LastModified);";

                cmd.Parameters.AddWithValue("BranchID", Details.BranchDetails.BranchID);
                cmd.Parameters.AddWithValue("TerminalNo", Details.TerminalNo);
                cmd.Parameters.AddWithValue("PLUReportID", Details.PLUReportID);
                cmd.Parameters.AddWithValue("ProductID", Details.ProductID);
                cmd.Parameters.AddWithValue("ProductCode", Details.ProductCode);
                cmd.Parameters.AddWithValue("ProductGroup", Details.ProductGroup);
                cmd.Parameters.AddWithValue("Quantity", Details.Quantity);
                cmd.Parameters.AddWithValue("Amount", Details.Amount);
                cmd.Parameters.AddWithValue("OrderSlipPrinter", Details.OrderSlipPrinter);
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

		public bool Delete(Int32 BranchID, string TerminalNo)
		{
			try 
			{
				MySqlCommand cmd = new MySqlCommand();
                cmd.CommandType = System.Data.CommandType.Text;

                string SQL = "DELETE FROM tblPLUReport WHERE BranchID = @BranchID AND TerminalNo = @TerminalNo ;";
				
                cmd.Parameters.AddWithValue("BranchID", BranchID);
                cmd.Parameters.AddWithValue("TerminalNo", TerminalNo);

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

        private string SQLSelectGroupByProduct()
        {
            string stSQL = "SELECT TerminalNo, OrderSlipPrinter, ProductGroup, ProductCode, " +
                                "SUM(Quantity) 'Quantity', SUM(Amount) 'Amount' " +
                            "FROM tblPLUReport ";

            return stSQL;
        }

		#region Streams

		private string SQLSelectGroupByProductGroup()
		{
            string SQL = "SELECT TerminalNo, OrderSlipPrinter, ProductGroup, '' ProductCode, " +
                                "SUM(Quantity) 'Quantity', SUM(Amount) 'Amount' " +
                            "FROM tblPLUReport ";
			return SQL;
		}

        public System.Data.DataTable ListAsDataTable(Int32 BranchID, string TerminalNo, bool isPerGroup = false, string SortField = "ProductCode", SortOption SortOrder = SortOption.Ascending, Int32 limit = 0)
		{
			try
			{
                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandType = System.Data.CommandType.Text;

                string SQL = "SELECT BranchID, TerminalNo, OrderSlipPrinter, ProductGroup, ProductCode, SUM(Quantity) 'Quantity', SUM(Amount) 'Amount' " +
                            "FROM tblPLUReport " +
                            "WHERE BranchID = @BranchID AND TerminalNo = @TerminalNo " +
                            "GROUP BY BranchID, TerminalNo, OrderSlipPrinter, ProductGroup, ProductCode " +
                            "ORDER BY TerminalNo, OrderSlipPrinter, ProductCode ";

                if (isPerGroup)
                {
                    SQL = "SELECT BranchID, TerminalNo, OrderSlipPrinter, ProductGroup, '' ProductCode, SUM(Quantity) 'Quantity', SUM(Amount) 'Amount' " +
                            "FROM tblPLUReport " +
                            "WHERE BranchID = @BranchID AND TerminalNo = @TerminalNo " +
                            "GROUP BY BranchID, TerminalNo, OrderSlipPrinter, ProductGroup " +
                            "ORDER BY TerminalNo, OrderSlipPrinter, ProductGroup ";
                }

                cmd.Parameters.AddWithValue("@BranchID", BranchID);
                cmd.Parameters.AddWithValue("@TerminalNo", TerminalNo);

                //SQL += "ORDER BY " + SortField + " "; --no need already put above
                SQL += SortOrder == SortOption.Ascending ? "ASC " : "DESC ";
                SQL += limit == 0 ? "" : "LIMIT " + limit.ToString() + " ";

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
	}
}
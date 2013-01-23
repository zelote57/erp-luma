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
	public struct PesoDenominationDetails
	{
		public int DenominationID;
		public int CashierID;
		public string CashierName;
		public string TerminalNo;
		public DateTime DenominationDate;
		public int OneThousandPesos;
		public int FiveHundredPesos;
		public int OneHundredPesos;
		public int FiftyPesos;
		public int TwentyPesos;
		public int TenPesos;
		public int FivePesos;
		public int OnePeso;
		public int TwentyFiveCents;
		public decimal Others;
	}

	[StrongNameIdentityPermissionAttribute(SecurityAction.LinkDemand,
		 PublicKey = "002400000480000094000000060200000024000" +
		 "052534131000400000100010053D785642F9F960B43157E0380" +
		 "F393BEE53E8DFAFBF441366C1B6F8B48D9DDF0D527B1F3B21EA" +
		 "E85D2FDB664CE81EB8A87DBE4C589D6F4202FE2B7D4B978BB69" +
		 "684874612CB9B8DB7A0339400A9C4E68277884B07817363D242" +
		 "E3696F9FACDBEA831810AE6DC9EDCA91A7B5DA12FE7BF65D113" +
		 "FF52834EAFB5A7A1FDFD5851A3")]
	public class PesoDenomination : POSConnection
    {
		#region Constructors and Destructors

		public PesoDenomination()
            : base(null, null)
        {
        }

        public PesoDenomination(MySqlConnection Connection, MySqlTransaction Transaction) 
            : base(Connection, Transaction)
		{

		}

		#endregion

		#region Insert and Update

		public Int32 Insert(PesoDenominationDetails Details)
		{
			try  
			{
				string SQL =	"INSERT INTO tblPesoDenominations (" +
								"CashierID, " +
								"CashierName, " +
								"TerminalNo, " + 
								"DenominationDate, " +
								"OneThousandPesos, " +
								"FiveHundredPesos, " +
								"OneHundredPesos, " +
								"FiftyPesos, " +
								"TwentyPesos, " +
								"TenPesos, " +
								"FivePesos, " +
								"OnePeso, " +
								"TwentyFiveCents, " +
								"Others " +
								") VALUES (" +
								"@CashierID, " +
								"@CashierName, " +
								"@TerminalNo, " + 
								"@DenominationDate, " +
								"@OneThousandPesos, " +
								"@FiveHundredPesos, " +
								"@OneHundredPesos, " +
								"@FiftyPesos, " +
								"@TwentyPesos, " +
								"@TenPesos, " +
								"@FivePesos, " +
								"@OnePeso, " +
								"@TwentyFiveCents, " +
								"@Others);";
				  
				
	 			
				MySqlCommand cmd = new MySqlCommand();
				
				
				cmd.CommandType = System.Data.CommandType.Text;
				cmd.CommandText = SQL;
				
//				MySqlParameter prmDenominationID = new MySqlParameter("@DenominationID",MySqlDbType.Int32);	
//				prmDenominationID.Value = Details.DenominationID;
//				cmd.Parameters.Add(prmDenominationID);
				
				MySqlParameter prmCashierID = new MySqlParameter("@CashierID",MySqlDbType.Int32);	
				prmCashierID.Value = Details.CashierID;
				cmd.Parameters.Add(prmCashierID);
				
				MySqlParameter prmCashierName = new MySqlParameter("@CashierName",MySqlDbType.String);	
				prmCashierName.Value = Details.CashierName;
				cmd.Parameters.Add(prmCashierName);

				MySqlParameter prmTerminalNo = new MySqlParameter("@TerminalNo",MySqlDbType.String);
				prmTerminalNo.Value = Details.TerminalNo;
				cmd.Parameters.Add(prmTerminalNo);
	
				MySqlParameter prmDenominationDate = new MySqlParameter("@DenominationDate",MySqlDbType.DateTime);
				prmDenominationDate.Value = Details.DenominationDate.ToString("yyyy-MM-dd HH:mm:ss");
				cmd.Parameters.Add(prmDenominationDate);

				MySqlParameter prmOneThousandPesos = new MySqlParameter("@OneThousandPesos",MySqlDbType.Int16);
				prmOneThousandPesos.Value = Details.OneThousandPesos;
				cmd.Parameters.Add(prmOneThousandPesos);

				MySqlParameter prmFiveHundredPesos = new MySqlParameter("@FiveHundredPesos",MySqlDbType.Int16);
				prmFiveHundredPesos.Value = Details.FiveHundredPesos;
				cmd.Parameters.Add(prmFiveHundredPesos);

				MySqlParameter prmOneHundredPesos = new MySqlParameter("@OneHundredPesos",MySqlDbType.Int16);
				prmOneHundredPesos.Value = Details.OneHundredPesos;
				cmd.Parameters.Add(prmOneHundredPesos);

				MySqlParameter prmFiftyPesos = new MySqlParameter("@FiftyPesos",MySqlDbType.Int16);
				prmFiftyPesos.Value = Details.FiftyPesos;
				cmd.Parameters.Add(prmFiftyPesos);

				MySqlParameter prmTwentyPesos = new MySqlParameter("@TwentyPesos",MySqlDbType.Int16);
				prmTwentyPesos.Value = Details.TwentyPesos;
				cmd.Parameters.Add(prmTwentyPesos);

				MySqlParameter prmTenPesos = new MySqlParameter("@TenPesos",MySqlDbType.Int16);
				prmTenPesos.Value = Details.TenPesos;
				cmd.Parameters.Add(prmTenPesos);

				MySqlParameter prmFivePesos = new MySqlParameter("@FivePesos",MySqlDbType.Int16);
				prmFivePesos.Value = Details.FivePesos;
				cmd.Parameters.Add(prmFivePesos);

				MySqlParameter prmOnePeso = new MySqlParameter("@OnePeso",MySqlDbType.Int16);
				prmOnePeso.Value = Details.OnePeso;
				cmd.Parameters.Add(prmOnePeso);


				MySqlParameter prmTwentyFiveCents = new MySqlParameter("@TwentyFiveCents",MySqlDbType.Int16);
				prmTwentyFiveCents.Value = Details.TwentyFiveCents;
				cmd.Parameters.Add(prmTwentyFiveCents);

				MySqlParameter prmOthers = new MySqlParameter("@Others",MySqlDbType.Decimal);
				prmOthers.Value = Details.Others;
				cmd.Parameters.Add(prmOthers);

				base.ExecuteNonQuery(cmd);

				SQL = "SELECT LAST_INSERT_ID();";
				
				cmd.Parameters.Clear(); 
				cmd.CommandText = SQL;
				
				MySqlDataReader myReader = base.ExecuteReader(cmd, System.Data.CommandBehavior.SingleResult);
				
				Int32 iID = 0;

				while (myReader.Read()) 
				{
					iID = myReader.GetInt32(0);
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

		public void Update(PesoDenominationDetails Details)
		{
			try 
			{
				string SQL =	"UPDATE tblPesoDenominations SET " +
								"CashierID = @CashierID, " +
								"CashierName = @CashierName, " +
								"TerminalNo = @TerminalNo, " + 
								"DenominationDate = @DenominationDate, " +
								"OneThousandPesos = @OneThousandPesos, " +
								"FiveHundredPesos = @FiveHundredPesos, " +
								"OneHundredPesos = @OneHundredPesos, " +
								"FiftyPesos = @FiftyPesos, " +
								"TwentyPesos = @TwentyPesos, " +
								"TenPesos = @TenPesos, " +
								"FivePesos = @FivePesos, " +
								"OnePeso = @OnePeso, " +
								"TwentyFiveCents = @TwentyFiveCents, " +
								"Others = @Others " +
								"WHERE DenominationID = @DenominationID;";
				  
				
	 			
				MySqlCommand cmd = new MySqlCommand();
				
				
				cmd.CommandType = System.Data.CommandType.Text;
				cmd.CommandText = SQL;
				
				MySqlParameter prmCashierID = new MySqlParameter("@CashierID",MySqlDbType.Int32);	
				prmCashierID.Value = Details.CashierID;
				cmd.Parameters.Add(prmCashierID);
				
				MySqlParameter prmCashierName = new MySqlParameter("@CashierName",MySqlDbType.String);	
				prmCashierName.Value = Details.CashierName;
				cmd.Parameters.Add(prmCashierName);

				MySqlParameter prmTerminalNo = new MySqlParameter("@TerminalNo",MySqlDbType.String);
				prmTerminalNo.Value = Details.TerminalNo;
				cmd.Parameters.Add(prmTerminalNo);
	
				MySqlParameter prmDenominationDate = new MySqlParameter("@DenominationDate",MySqlDbType.DateTime);
				prmDenominationDate.Value = Details.DenominationDate.ToString("yyyy-MM-dd HH:mm:ss");
				cmd.Parameters.Add(prmDenominationDate);

				MySqlParameter prmOneThousandPesos = new MySqlParameter("@OneThousandPesos",MySqlDbType.Int16);
				prmOneThousandPesos.Value = Details.OneThousandPesos;
				cmd.Parameters.Add(prmOneThousandPesos);

				MySqlParameter prmFiveHundredPesos = new MySqlParameter("@FiveHundredPesos",MySqlDbType.Int16);
				prmFiveHundredPesos.Value = Details.FiveHundredPesos;
				cmd.Parameters.Add(prmFiveHundredPesos);

				MySqlParameter prmOneHundredPesos = new MySqlParameter("@OneHundredPesos",MySqlDbType.Int16);
				prmOneHundredPesos.Value = Details.OneHundredPesos;
				cmd.Parameters.Add(prmOneHundredPesos);

				MySqlParameter prmFiftyPesos = new MySqlParameter("@FiftyPesos",MySqlDbType.Int16);
				prmFiftyPesos.Value = Details.FiftyPesos;
				cmd.Parameters.Add(prmFiftyPesos);

				MySqlParameter prmTwentyPesos = new MySqlParameter("@TwentyPesos",MySqlDbType.Int16);
				prmTwentyPesos.Value = Details.TwentyPesos;
				cmd.Parameters.Add(prmTwentyPesos);

				MySqlParameter prmTenPesos = new MySqlParameter("@TenPesos",MySqlDbType.Int16);
				prmTenPesos.Value = Details.TenPesos;
				cmd.Parameters.Add(prmTenPesos);

				MySqlParameter prmFivePesos = new MySqlParameter("@FivePesos",MySqlDbType.Int16);
				prmFivePesos.Value = Details.FivePesos;
				cmd.Parameters.Add(prmFivePesos);

				MySqlParameter prmOnePeso = new MySqlParameter("@OnePeso",MySqlDbType.Int16);
				prmOnePeso.Value = Details.OnePeso;
				cmd.Parameters.Add(prmOnePeso);

				MySqlParameter prmDenominationID = new MySqlParameter("@DenominationID",MySqlDbType.Int32);	
				prmDenominationID.Value = Details.DenominationID;
				cmd.Parameters.Add(prmDenominationID);


				MySqlParameter prmTwentyFiveCents = new MySqlParameter("@TwentyFiveCents",MySqlDbType.Int16);
				prmTwentyFiveCents.Value = Details.TwentyFiveCents;
				cmd.Parameters.Add(prmTwentyFiveCents);

				MySqlParameter prmOthers = new MySqlParameter("@Others",MySqlDbType.Decimal);
				prmOthers.Value = Details.Others;
				cmd.Parameters.Add(prmOthers);

				base.ExecuteNonQuery(cmd);
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

		public bool Delete(string IDs)
		{
			try 
			{
				string SQL=	"DELETE FROM tblPesoDenominations WHERE DenominationID IN (" + IDs + ");";
				  
				
	 			
				MySqlCommand cmd = new MySqlCommand();
				
				
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

		#region Details

		public PesoDenominationDetails Details(Int32 DenominationID)
		{
			try
			{
				string SQL =	"SELECT " +
								"DenominationID, " +
								"CashierID, " +
								"CashierName, " +
								"TerminalNo, " + 
								"DenominationDate, " +
								"OneThousandPesos, " +
								"FiveHundredPesos, " +
								"OneHundredPesos, " +
								"FiftyPesos, " +
								"TwentyPesos, " +
								"TenPesos, " +
								"FivePesos, " +
								"OnePeso, " +
								"TwentyFiveCents, " +
								"Others " +
								"FROM tblPesoDenominations " +
								"WHERE DenominationID = @DenominationID;";
				  
				
	 			
				MySqlCommand cmd = new MySqlCommand();
				
				
				cmd.CommandType = System.Data.CommandType.Text;
				cmd.CommandText = SQL;

				MySqlParameter prmDenominationID = new MySqlParameter("@DenominationID",MySqlDbType.Int32);
				prmDenominationID.Value = DenominationID;
				cmd.Parameters.Add(prmDenominationID);

				MySqlDataReader myReader = base.ExecuteReader(cmd, System.Data.CommandBehavior.SingleResult);
				
				PesoDenominationDetails Details = new PesoDenominationDetails();

				while (myReader.Read()) 
				{
					Details.DenominationID = DenominationID;
					Details.CashierID = myReader.GetInt32(1);
					Details.CashierName = myReader.GetString(2);
					Details.TerminalNo = myReader.GetString(3);
					Details.DenominationDate = myReader.GetDateTime(4);
					Details.OneThousandPesos = myReader.GetInt16(5);
					Details.FiveHundredPesos = myReader.GetInt16(6);
					Details.OneHundredPesos = myReader.GetInt16(7);
					Details.FiftyPesos = myReader.GetInt16(8);
					Details.TwentyPesos = myReader.GetInt16(9);
					Details.TenPesos = myReader.GetInt16(10);
					Details.FivePesos = myReader.GetInt16(11);
					Details.OnePeso = myReader.GetInt16(12);
					Details.TwentyFiveCents = myReader.GetInt16(13);
					Details.Others = myReader.GetDecimal(14);
				}

				myReader.Close();

				return Details;
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

		public MySqlDataReader List(string SortField, SortOption SortOrder)
		{
			try
			{
				string SQL =	"SELECT " +
					"DenominationID, " +
					"CashierID, " +
					"CashierName, " +
					"TerminalNo, " + 
					"DenominationDate, " +
					"OneThousandPesos, " +
					"FiveHundredPesos, " +
					"OneHundredPesos, " +
					"FiftyPesos, " +
					"TwentyPesos, " +
					"TenPesos, " +
					"FivePesos, " +
					"OnePeso, " +
					"TwentyFiveCents, " +
					"Others " +
					"FROM tblPesoDenominations " +
					"WHERE 1=1 ORDER BY " + SortField; 

				if (SortOrder == SortOption.Ascending)
					SQL += " ASC";
				else
					SQL += " DESC";

				

				MySqlCommand cmd = new MySqlCommand();
				
				
				cmd.CommandType = System.Data.CommandType.Text;
				cmd.CommandText = SQL;
				
				
				
				return base.ExecuteReader(cmd);			
			}
			catch (Exception ex)
			{
				
				
				{
					
					
					
					
				}

				throw ex;
			}	
		}
		
		public MySqlDataReader Search(string SearchKey, string SortField, SortOption SortOrder)
		{
			try
			{
				string SQL =	"SELECT " +
								"DenominationID, " +
								"CashierID, " +
								"CashierName, " +
								"TerminalNo, " + 
								"DenominationDate, " +
								"OneThousandPesos, " +
								"FiveHundredPesos, " +
								"OneHundredPesos, " +
								"FiftyPesos, " +
								"TwentyPesos, " +
								"TenPesos, " +
								"FivePesos, " +
								"OnePeso, " +
								"TwentyFiveCents, " +
								"Others " +
								"FROM tblPesoDenominations " +
								"WHERE 1=1 " +
								"AND CashierID LIKE '%" + SearchKey + "%' " +
								"OR CashierName LIKE '%" + SearchKey + "%' " +
								"OR TerminalNo LIKE '%" + SearchKey + "%' " +
								"OR DenominationDate LIKE '%" + SearchKey + "%' " +
								"ORDER BY " + SortField; 

				if (SortOrder == SortOption.Ascending)
					SQL += " ASC";
				else
					SQL += " DESC";

				

				MySqlCommand cmd = new MySqlCommand();
				
				
				cmd.CommandType = System.Data.CommandType.Text;
				cmd.CommandText = SQL;
				
				
				
				return base.ExecuteReader(cmd);			
			}
			catch (Exception ex)
			{
				
				
				{
					
					
					
					
				}

				throw ex;
			}	
		}		

		#endregion
	}
}


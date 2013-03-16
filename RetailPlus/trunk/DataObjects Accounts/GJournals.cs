
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
	public struct GJournalsDetails
	{
		public long GJournalID;
		public string Particulars;
		public AccountGJournalsStatus Status;
		public DateTime PostingDate;
		public DateTime CancelledDate;
		public decimal TotalDebitAmount;
		public decimal TotalCreditAmount;
	}

	[StrongNameIdentityPermissionAttribute(SecurityAction.LinkDemand,
		 PublicKey = "002400000480000094000000060200000024000" +
		 "052534131000400000100010053D785642F9F960B43157E0380" +
		 "F393BEE53E8DFAFBF441366C1B6F8B48D9DDF0D527B1F3B21EA" +
		 "E85D2FDB664CE81EB8A87DBE4C589D6F4202FE2B7D4B978BB69" +
		 "684874612CB9B8DB7A0339400A9C4E68277884B07817363D242" +
		 "E3696F9FACDBEA831810AE6DC9EDCA91A7B5DA12FE7BF65D113" +
		 "FF52834EAFB5A7A1FDFD5851A3")]
	public class GJournals : POSConnection
	{
		#region Constructors and Destructors

		public GJournals()
            : base(null, null)
        {
        }

        public GJournals(MySqlConnection Connection, MySqlTransaction Transaction) 
            : base(Connection, Transaction)
		{

		}

		#endregion

		#region Insert and Update

		public long Insert(GJournalsDetails Details)
		{
			try 
			{
				string SQL = "INSERT INTO tblGJournal (" +
								"Particulars, " +
								"Status, " +
								"TotalDebitAmount, " +
								"TotalCreditAmount" +
							") VALUES (" +
								"@Particulars, " +
								"@Status, " +
								"@TotalDebitAmount, " +
								"@TotalCreditAmount" +
							");";
				  
				
	 			
				MySqlCommand cmd = new MySqlCommand();
				
				
				cmd.CommandType = System.Data.CommandType.Text;
				cmd.CommandText = SQL;

				MySqlParameter prmParticulars = new MySqlParameter("@Particulars",MySqlDbType.String);	
				prmParticulars.Value = Details.Particulars;
				cmd.Parameters.Add(prmParticulars);

				MySqlParameter prmStatus = new MySqlParameter("@Status",MySqlDbType.Int16);			
				prmStatus.Value = Details.Status.ToString("d");
				cmd.Parameters.Add(prmStatus);

				MySqlParameter prmTotalDebitAmount = new MySqlParameter("@TotalDebitAmount",MySqlDbType.Decimal);			
				prmTotalDebitAmount.Value = Details.TotalDebitAmount;
				cmd.Parameters.Add(prmTotalDebitAmount);

				MySqlParameter prmTotalCreditAmount = new MySqlParameter("@TotalCreditAmount",MySqlDbType.Decimal);			
				prmTotalCreditAmount.Value = Details.TotalCreditAmount;
				cmd.Parameters.Add(prmTotalCreditAmount);
     
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
				
				
				{
					
					 
					
					
				}

				throw base.ThrowException(ex);
			}	
		}
		public void Update(GJournalsDetails Details)
		{
			try 
			{
				string SQL = "UPDATE tblGJournal SET " +
								"Particulars		=	@Particulars, " +
								"Status				=	@Status, " +
								"TotalDebitAmount	=	@TotalDebitAmount, " +
								"TotalCreditAmount	=	@TotalCreditAmount " +
							"WHERE GJournalID = @GJournalID;";
				  
				
	 			
				MySqlCommand cmd = new MySqlCommand();
				
				
				cmd.CommandType = System.Data.CommandType.Text;
				cmd.CommandText = SQL;

				MySqlParameter prmParticulars = new MySqlParameter("@Particulars",MySqlDbType.String);	
				prmParticulars.Value = Details.Particulars;
				cmd.Parameters.Add(prmParticulars);

				MySqlParameter prmStatus = new MySqlParameter("@Status",MySqlDbType.Int16);			
				prmStatus.Value = Details.Status.ToString("d");
				cmd.Parameters.Add(prmStatus);

				MySqlParameter prmTotalDebitAmount = new MySqlParameter("@TotalDebitAmount",MySqlDbType.Decimal);			
				prmTotalDebitAmount.Value = Details.TotalDebitAmount;
				cmd.Parameters.Add(prmTotalDebitAmount);

				MySqlParameter prmTotalCreditAmount = new MySqlParameter("@TotalCreditAmount",MySqlDbType.Decimal);			
				prmTotalCreditAmount.Value = Details.TotalCreditAmount;
				cmd.Parameters.Add(prmTotalCreditAmount);

				MySqlParameter prmGJournalID = new MySqlParameter("@GJournalID",MySqlDbType.Int64);				
				prmGJournalID.Value = Details.GJournalID;
				cmd.Parameters.Add(prmGJournalID);

				base.ExecuteNonQuery(cmd);
			}

			catch (Exception ex)
			{
				
				
				{
					
					 
					
					
				}

				throw base.ThrowException(ex);
			}	
		}

		#endregion

		#region Cancel

		public bool Cancel(string IDs)
		{
			try 
			{
                string SQL = "UPDATE tblGJournal SET Status = @CancelledStatus AND CancelledDate = @CancelledDate WHERE GJournalID IN (" + IDs + ");";
				  
				
	 			
				MySqlCommand cmd = new MySqlCommand();
				
				
				cmd.CommandType = System.Data.CommandType.Text;
				cmd.CommandText = SQL;

                MySqlParameter prmCancelledStatus = new MySqlParameter("@CancelledStatus",MySqlDbType.Int16);
                prmCancelledStatus.Value = AccountGJournalsStatus.Cancelled.ToString("d");
                cmd.Parameters.Add(prmCancelledStatus);

                MySqlParameter prmCancelledDate = new MySqlParameter("@CancelledDate",MySqlDbType.DateTime);
                prmCancelledDate.Value = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                cmd.Parameters.Add(prmCancelledDate);

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

        private string SQLSelect()
        {
            string stSQL = "SELECT " +
                                "GJournalID, " +
                                "Particulars, " +
                                "Status, " +
                                "TotalDebitAmount, " +
                                "TotalCreditAmount " +
                            "FROM tblGJournal ";
            return stSQL;
        }

		#region Details

		public GJournalsDetails Details(long GJournalID)
		{
			try
			{
				string SQL =	SQLSelect() + "WHERE GJournalID = @GJournalID;";
				  
				
	 			
				MySqlCommand cmd = new MySqlCommand();
				
				
				cmd.CommandType = System.Data.CommandType.Text;
				cmd.CommandText = SQL;

				MySqlParameter prmGJournalID = new MySqlParameter("@GJournalID",MySqlDbType.Int64);			
				prmGJournalID.Value = GJournalID;
				cmd.Parameters.Add(prmGJournalID);

				MySqlDataReader myReader = base.ExecuteReader(cmd, System.Data.CommandBehavior.SingleResult);
				
				GJournalsDetails Details = new GJournalsDetails();

				while (myReader.Read()) 
				{
					Details.GJournalID = GJournalID;
					Details.Particulars = "" + myReader["Particulars"].ToString();
                    Details.Status = (AccountGJournalsStatus)Enum.Parse(typeof(AccountGJournalsStatus), myReader.GetString("Status"));
					Details.TotalDebitAmount = myReader.GetDecimal("TotalDebitAmount");
					Details.TotalCreditAmount = myReader.GetDecimal("TotalCreditAmount");
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
				
				
				{
					
					 
					
					
				}

				throw base.ThrowException(ex);
			}	
		}
		public MySqlDataReader Search(string SearchKey, string SortField, SortOption SortOrder)
		{
			try
			{
				string SQL =SQLSelect() + "WHERE Particulars LIKE @SearchKey " +
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
				
				
				{
					
					 
					
					
				}

				throw base.ThrowException(ex);
			}	
		}		

		#endregion

		#region Public Modifiers

		public void Post(long GJournalID, DateTime PostingDate)
		{
			try 
			{
				string SQL = "UPDATE tblGJournal SET " + 
								"PostingDate		=	@PostingDate, " +
								"Status				=	@Status " +
							"WHERE GJournalID = @GJournalID;";
				  
				
	 			
				MySqlCommand cmd = new MySqlCommand();
				
				
				cmd.CommandType = System.Data.CommandType.Text;
				cmd.CommandText = SQL;

				MySqlParameter prmPostingDate = new MySqlParameter("@PostingDate",MySqlDbType.Date);
				prmPostingDate.Value = PostingDate.ToString("yyyy-MM-dd HH:mm:ss");
				cmd.Parameters.Add(prmPostingDate);

				MySqlParameter prmStatus = new MySqlParameter("@Status",MySqlDbType.Int16);			
				prmStatus.Value = AccountGJournalsStatus.Posted.ToString("d");
				cmd.Parameters.Add(prmStatus);

				MySqlParameter prmGJournalID = new MySqlParameter("@GJournalID",MySqlDbType.Int64);				
				prmGJournalID.Value = GJournalID;
				cmd.Parameters.Add(prmGJournalID);

				base.ExecuteNonQuery(cmd);


				GJournalsDebit clsGJournalsDebit = new GJournalsDebit(Connection, Transaction);
				clsGJournalsDebit.Post(GJournalID);
			
				GJournalsCredit clsGJournalsCredit = new GJournalsCredit(Connection, Transaction);
				clsGJournalsCredit.Post(GJournalID);
			}

			catch (Exception ex)
			{
				
				
				{
					
					 
					
					
				}

				throw base.ThrowException(ex);
			}	
		}

		#endregion
	}
}


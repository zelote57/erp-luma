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
	public class Database : POSConnection
    {
		#region Constructors and Destructors

		public Database()
            : base(null, null)
        {
        }

        public Database(MySqlConnection Connection, MySqlTransaction Transaction) 
            : base(Connection, Transaction)
		{

		}

		#endregion

		public bool IsAlive()
		{
			try
			{
				string SQL ="SELECT UID FROM sysAccessUsers LIMIT 1;";
				
				MySqlCommand cmd = new MySqlCommand();
				cmd.CommandType = System.Data.CommandType.Text;
				cmd.CommandText = SQL;
				
				MySqlDataReader myReader = base.ExecuteReader(cmd);
                myReader.Close();

				return true;			
			}
			catch (Exception ex)
			{
				throw ex;
			}	
		}

        public System.Data.DataTable getProcessList()
        {
            try
            {
                string SQL = "SHOW PROCESSLIST;";

                

                MySqlCommand cmd = new MySqlCommand();
                
                
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandText = SQL;

                System.Data.DataTable dt = new System.Data.DataTable("ProcessList");
                base.MySqlDataAdapterFill(cmd, dt);
                

                return dt;
            }
            catch (Exception ex)
            {
                
                
                {
                    
                    
                    
                    
                }

                throw ex;
            }
        }

        public bool killProcess(int Process)
        {
            try
            {
                bool boRetValue = false;

                string SQL = "kill " + Process + ";";

                

                MySqlCommand cmd = new MySqlCommand();
                
                
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandText = SQL;

                if (base.ExecuteNonQuery(cmd)> 0) boRetValue=true;

                return boRetValue;

            }
            catch (Exception ex)
            {
                
                
                {
                    
                    
                    
                    
                }

                throw ex;
            }
        }

        public DateTime DateLastInitialized()
        {
            try
            {
                string SQL = "SELECT DateLastInitialized FROM tblTerminalReport WHERE TerminalNo = @TerminalNo;";

                

                MySqlCommand cmd = new MySqlCommand();
                
                
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandText = SQL;

                MySqlParameter prmTerminalNo = new MySqlParameter("@TerminalNo",MySqlDbType.String);
                prmTerminalNo.Value = CompanyDetails.TerminalNo;
                cmd.Parameters.Add(prmTerminalNo);

                MySqlDataReader myReader = base.ExecuteReader(cmd, System.Data.CommandBehavior.SingleResult);

                DateTime dtDateLastInitialized = DateTime.MinValue;

                while (myReader.Read())
                {
                    dtDateLastInitialized = myReader.GetDateTime("DateLastInitialized");
                }
                myReader.Close();

                return dtDateLastInitialized;
            }
            catch (Exception ex)
            {
                
                
                {
                    
                    
                    
                    
                }

                throw ex;
            }
        }		
	}
}


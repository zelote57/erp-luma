using System;
using System.Security.Permissions;
using MySql.Data.MySqlClient;

/******************************************************************************
	**		Auth: Lemuel E. Aceron
	**		Date: Feb 13, 2008
	***************************************************************************
	**		Change History
	***************************************************************************
	**		Date:			Author:				Description:
	**		--------		--------			-------------------------------
	**      
	***************************************************************************/

namespace AceSoft.RetailPlus.Reports
{

	[StrongNameIdentityPermissionAttribute(SecurityAction.LinkDemand,
		 PublicKey = "002400000480000094000000060200000024000" +
		 "052534131000400000100010053D785642F9F960B43157E0380" +
		 "F393BEE53E8DFAFBF441366C1B6F8B48D9DDF0D527B1F3B21EA" +
		 "E85D2FDB664CE81EB8A87DBE4C589D6F4202FE2B7D4B978BB69" +
		 "684874612CB9B8DB7A0339400A9C4E68277884B07817363D242" +
		 "E3696F9FACDBEA831810AE6DC9EDCA91A7B5DA12FE7BF65D113" +
		 "FF52834EAFB5A7A1FDFD5851A3")]
	public struct ReceiptDetails
	{
		public string Module;
		public string Text;
		public string Value;
		public ReportFormatOrientation Orientation;
	}

	[StrongNameIdentityPermissionAttribute(SecurityAction.LinkDemand,
		 PublicKey = "002400000480000094000000060200000024000" +
		 "052534131000400000100010053D785642F9F960B43157E0380" +
		 "F393BEE53E8DFAFBF441366C1B6F8B48D9DDF0D527B1F3B21EA" +
		 "E85D2FDB664CE81EB8A87DBE4C589D6F4202FE2B7D4B978BB69" +
		 "684874612CB9B8DB7A0339400A9C4E68277884B07817363D242" +
		 "E3696F9FACDBEA831810AE6DC9EDCA91A7B5DA12FE7BF65D113" +
		 "FF52834EAFB5A7A1FDFD5851A3")]
	public class Receipt : POSConnection
	{

		#region Constructors and Destructors

		public Receipt()
            : base(null, null)
        {
        }

        public Receipt(MySqlConnection Connection, MySqlTransaction Transaction) 
            : base(Connection, Transaction)
		{

		}

		#endregion

		#region Insert and Update

		public void Update(ReceiptDetails Details)
		{
			try 
			{
				string SQL=	"UPDATE tblReceipt SET " +
								"Text			=	@Text," +
								"Value			=	@Value, " +
								"Orientation	=	@Orientation " +
							"WHERE " +
								"Module	=	@Module ";
	 			
				MySqlCommand cmd = new MySqlCommand();

				cmd.CommandType = System.Data.CommandType.Text;
				cmd.CommandText = SQL;

                MySqlParameter prmText = new MySqlParameter("@Text", MySqlDbType.String);
				prmText.Value = Details.Text;
				cmd.Parameters.Add(prmText);

				MySqlParameter prmValue = new MySqlParameter("@Value", MySqlDbType.String);	
				prmValue.Value = Details.Value;
				cmd.Parameters.Add(prmValue);

				MySqlParameter prmOrientation = new MySqlParameter("@Orientation", MySqlDbType.Int16);
				prmOrientation.Value = Details.Orientation.ToString("d");
				cmd.Parameters.Add(prmOrientation);

				MySqlParameter prmModule = new MySqlParameter("@Module", MySqlDbType.String);	
				prmModule.Value = Details.Module;
				cmd.Parameters.Add(prmModule);

                base.ExecuteNonQuery(cmd);
			}

            catch (Exception ex)
            {
                throw ex;
            }
		}


		#endregion

		#region Details

		public ReceiptDetails Details(string Module)
		{
			try
			{
				string SQL=	"SELECT " +
								"Module, " +
								"Text," +
								"Value, " +
								"Orientation " +
							"FROM tblReceipt WHERE Module = @Module;"; 
				  
				MySqlCommand cmd = new MySqlCommand();

				cmd.CommandType = System.Data.CommandType.Text;
				cmd.CommandText = SQL;

				MySqlParameter prmModule = new MySqlParameter("@Module", MySqlDbType.String);	
				prmModule.Value = Module;
				cmd.Parameters.Add(prmModule);

				ReceiptDetails Details = new ReceiptDetails();

                MySqlDataReader myReader = base.ExecuteReader(cmd, System.Data.CommandBehavior.SingleResult);
				while (myReader.Read()) 
				{
					Details.Module = "" + myReader["Module"].ToString();
					Details.Text = "" + myReader["Text"].ToString();
					Details.Value = "" + myReader["Value"].ToString();
					Details.Orientation = (ReportFormatOrientation) Enum.Parse(typeof(ReportFormatOrientation), myReader["Orientation"].ToString());
				}

				myReader.Close();

				return Details;
			}

			catch (Exception ex)
			{
				throw ex;
			}	
		}


		#endregion

		#region Streams

		public MySqlDataReader List()
		{
			try
			{
				string SQL=	"SELECT " +
								"Module," +
								"Text," +
								"Value, " +
								"Orientation " +
							"FROM tblReceipt;"; 
				  
				MySqlCommand cmd = new MySqlCommand();
				cmd.CommandType = System.Data.CommandType.Text;
				cmd.CommandText = SQL;

				return base.ExecuteReader(cmd);
			}

			catch (Exception ex)
			{
				throw ex;
			}	
		}


		#endregion
	}
}
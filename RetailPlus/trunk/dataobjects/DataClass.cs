using System;
using System.Collections;
using System.Security.Permissions;
using MySql.Data.MySqlClient;

namespace AceSoft
{	
	public enum SortOption
	{
		Ascending,
		Desscending,
        NotApplicable
	}

	public enum PageNavigator
	{
		CurrentPage = 0,
		FirstPage = 1,
		NextPage = 2,
		PreviousPage = 3,
		LastPage = 4
	}

	[StrongNameIdentityPermissionAttribute(SecurityAction.LinkDemand,
		 PublicKey = "002400000480000094000000060200000024000" +
		 "052534131000400000100010053D785642F9F960B43157E0380" +
		 "F393BEE53E8DFAFBF441366C1B6F8B48D9DDF0D527B1F3B21EA" +
		 "E85D2FDB664CE81EB8A87DBE4C589D6F4202FE2B7D4B978BB69" +
		 "684874612CB9B8DB7A0339400A9C4E68277884B07817363D242" +
		 "E3696F9FACDBEA831810AE6DC9EDCA91A7B5DA12FE7BF65D113" +
		 "FF52834EAFB5A7A1FDFD5851A3")]
	public class DataClass
	{
		public System.Data.DataTable DataReaderToDataTable(MySqlDataReader Reader)
		{
			System.Data.DataTable dt = new System.Data.DataTable();
			System.Data.DataColumn dc;
			System.Data.DataRow dr;
			ArrayList arr = new ArrayList();
			int i;

			for(i=0;i<Reader.FieldCount;i++)
			{
				dc = new System.Data.DataColumn();

				dc.ColumnName = Reader.GetName(i);					
				arr.Add(dc.ColumnName);

				dt.Columns.Add(dc);
			}
			
			while(Reader.Read())
			{
				dr = dt.NewRow();

				for (i=0;i<Reader.FieldCount;i++)
				{
					dr[(string)arr[i]] = Reader[i].ToString();
				}
				dt.Rows.Add(dr);
			}

			Reader.Close();
			return dt;
		}


		private System.Data.DataTable ConvertToDataTable(MySqlDataReader Reader)
		{
			int index = 0;
			string ColumnName;

			System.Data.DataTable dt = new System.Data.DataTable();

			while(Reader.Read())
			{
				ColumnName = Reader.GetName(index);
				
				dt.Columns.Add(ColumnName);     
//				dt.
				
				index++;
			}

			return dt;
		}

		System.Data.DataRow CreateRow(String Text, String Value, System.Data.DataTable dt)
		{

			// Create a DataRow using the DataTable defined in the 
			// CreateDataSource method.
			System.Data.DataRow dr = dt.NewRow();
 
			// This DataRow contains the ColorTextField and ColorValueField 
			// fields, as defined in the CreateDataSource method. Set the 
			// fields with the appropriate value. Remember that column 0 
			// is defined as ColorTextField, and column 1 is defined as 
			// ColorValueField.
			dr[0] = Text;
			dr[1] = Value;
 
			return dr;

		}

	}
}

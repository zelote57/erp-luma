using System;
using System.Data;
using System.Security.Permissions;
using AceSoft.RetailPlus.Data;

namespace AceSoft.RetailPlus.Client
{
	
	[StrongNameIdentityPermissionAttribute(SecurityAction.LinkDemand,
		 PublicKey = "002400000480000094000000060200000024000" +
		 "052534131000400000100010053D785642F9F960B43157E0380" +
		 "F393BEE53E8DFAFBF441366C1B6F8B48D9DDF0D527B1F3B21EA" +
		 "E85D2FDB664CE81EB8A87DBE4C589D6F4202FE2B7D4B978BB69" +
		 "684874612CB9B8DB7A0339400A9C4E68277884B07817363D242" +
		 "E3696F9FACDBEA831810AE6DC9EDCA91A7B5DA12FE7BF65D113" +
		 "FF52834EAFB5A7A1FDFD5851A3")]
	public class SalesTransactions
	{
		public SalesTransactions()
		{
			//
			// TODO: Add constructor logic here
			//
		}

		public SalesTransactionItemDetails[] GetItems(DataTable ItemDataTable)
		{
			System.Data.DataTable dt = ItemDataTable;
			int intIndex = 0;

			SalesTransactionItemDetails[] items = new SalesTransactionItemDetails[dt.Rows.Count];

			foreach (System.Data.DataRow dr in dt.Rows)
			{
				SalesTransactionItemDetails Details = new SalesTransactionItemDetails();
 
				Details.ProductID = Convert.ToInt32(dr["ProductID"]);
				Details.ProductCode = Convert.ToString(dr["ProductCode"]);
				Details.BarCode = Convert.ToString(dr["BarCode"]);
				Details.Description = Convert.ToString(dr["Description"]);
				Details.ProductUnitID = Convert.ToInt32(dr["ProductUnitID"]);
				Details.ProductUnitCode = Convert.ToString(dr["ProductUnitCode"]);
				Details.Quantity = Convert.ToInt32(dr["Quantity"]);
				Details.Price = Convert.ToDecimal(dr["Price"]);
				Details.Discount= Convert.ToDecimal(dr["Discount"]);
				Details.Amount = Convert.ToDecimal(dr["Amount"]);
				Details.VariationsMatrixID = Convert.ToInt32(dr["VariationsMatrixID"]);
			
				items[intIndex] = Details;
				intIndex++;
			}
						
			return items;
		}
		
		public Int64 Insert(SalesTransactionDetails Details)
		{
			Data.SalesTransactions clsSalesTransactions = new Data.SalesTransactions();
			Int64 iRetValue = clsSalesTransactions.Insert(Details);
            clsSalesTransactions.CommitAndDispose();
			return iRetValue;
		}
	}


}

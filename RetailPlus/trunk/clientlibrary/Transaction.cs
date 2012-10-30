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
	public class Transaction
	{
		public Transaction()
		{
			//
			// TODO: Add constructor logic here
			//
		}

		public SalesTransactionItemDetails[] GetItems(DataTable ItemDataTable)
		{
			int intIndex = 0;

			SalesTransactionItemDetails[] items = new SalesTransactionItemDetails[ItemDataTable.Rows.Count];

			foreach (System.Data.DataRow dr in ItemDataTable.Rows)
			{
				SalesTransactionItemDetails details = new SalesTransactionItemDetails();
 
				details.ProductID = Convert.ToInt32(dr["ProductID"]);
				details.ProductCode = Convert.ToString(dr["ProductCode"]);
				details.BarCode = Convert.ToString(dr["BarCode"]);
				details.Description = Convert.ToString(dr["Description"]);
				details.ProductUnitID = Convert.ToInt32(dr["ProductUnitID"]);
				details.ProductUnitCode = Convert.ToString(dr["ProductUnitCode"]);
				details.Quantity = Convert.ToInt32(dr["Quantity"]);
				details.Price = Convert.ToDecimal(dr["Price"]);
				details.Discount= Convert.ToDecimal(dr["Discount"]);
				details.Amount = Convert.ToDecimal(dr["Amount"]);
				details.VariationsMatrixID = Convert.ToInt32(dr["VariationsMatrixID"]);
			
				items[intIndex] = details;
				intIndex++;
			}
						
			return items;
		}
 
	}
}

using System;
using System.IO;
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
	public struct ItemFooterDetails
	{
		public decimal Quantity;
		public decimal Price;
		public decimal Discount;
		public decimal ItemDiscount;
		public decimal Amount;
	}

	[StrongNameIdentityPermissionAttribute(SecurityAction.LinkDemand,
		 PublicKey = "002400000480000094000000060200000024000" +
		 "052534131000400000100010053D785642F9F960B43157E0380" +
		 "F393BEE53E8DFAFBF441366C1B6F8B48D9DDF0D527B1F3B21EA" +
		 "E85D2FDB664CE81EB8A87DBE4C589D6F4202FE2B7D4B978BB69" +
		 "684874612CB9B8DB7A0339400A9C4E68277884B07817363D242" +
		 "E3696F9FACDBEA831810AE6DC9EDCA91A7B5DA12FE7BF65D113" +
		 "FF52834EAFB5A7A1FDFD5851A3")]
	public struct FooterDetails 
	{
		public decimal SubTotal;
		public decimal Discount;
		public decimal VAT;
		public decimal VatableAmount;
		public decimal EVAT;
		public decimal EVatableAmount;
		public decimal LocalTax;
		public decimal AmountPaid;
		public decimal BalanceAmount;
		public decimal ChangeAmount;
	}


	[StrongNameIdentityPermissionAttribute(SecurityAction.LinkDemand,
		 PublicKey = "002400000480000094000000060200000024000" +
		 "052534131000400000100010053D785642F9F960B43157E0380" +
		 "F393BEE53E8DFAFBF441366C1B6F8B48D9DDF0D527B1F3B21EA" +
		 "E85D2FDB664CE81EB8A87DBE4C589D6F4202FE2B7D4B978BB69" +
		 "684874612CB9B8DB7A0339400A9C4E68277884B07817363D242" +
		 "E3696F9FACDBEA831810AE6DC9EDCA91A7B5DA12FE7BF65D113" +
		 "FF52834EAFB5A7A1FDFD5851A3")]
	public class TransactionStream
	{
		private StreamWriter clsStreamWriter;
		bool IsInTransaction = false;
		bool TransactionFailed = false;

		#region Constructors and Destructors

		public void CommitAndDispose(DateTime TransactionDate) 
		{
			if (!TransactionFailed)
			{
				if (IsInTransaction)
				{
					InitializeWriter(TransactionDate);
					clsStreamWriter.WriteLine(Convert.ToChar(2));
					clsStreamWriter.Flush();
					clsStreamWriter.Close();
				}
			}
		}

		public TransactionStream()
		{
		}

		#endregion

		#region Public methods

		public string Create(SalesTransactionDetails Details)
		{
			try
			{
                //string stTransactionNo = GetTransactionNo();
				
                //Details.TransactionNo = stTransactionNo;
				
				IsInTransaction = true;
                return Details.TransactionNo;
			}
			catch(Exception ex)
			{
				TransactionFailed = true;
				throw ex;
			}
		}
		
		private void InitializeWriter(DateTime TransactionDate)
		{
			try
			{
				string logFile = GetRootDirectory(TransactionDate) + TransactionDate.ToString("ddMMyyyy") + ".dbg";
				clsStreamWriter = new StreamWriter(logFile, true);

				if (File.Exists(logFile)==false)
				{
					///If True: Add header
					clsStreamWriter.WriteLine("This is an auto-generated logfile for RetailPlus Transaction logs.");
					clsStreamWriter.WriteLine("Best viewed in notepad and textpad using Courier 10 as Font.");
					clsStreamWriter.WriteLine("Log Date: {0}", TransactionDate.ToString("MMMM dd, yyyy"));
					clsStreamWriter.WriteLine();
				}

				IsInTransaction = true;
			}
			catch(Exception ex)
			{
				TransactionFailed = true;
				throw ex;
			}
		}

		public void AddTransactionHeader(SalesTransactionDetails Details, DateTime TransactionDate)
		{
			InitializeWriter(TransactionDate);

			clsStreamWriter.WriteLine(Environment.NewLine);
			//this is for transaction header
			clsStreamWriter.WriteLine("TransactionNo: {0}", Details.TransactionNo);
			clsStreamWriter.WriteLine("CustomerID: {0}", Details.CustomerID.ToString());
			clsStreamWriter.WriteLine("CustomerName: {0}", Details.CustomerName);
			clsStreamWriter.WriteLine("CashierID: {0}", Details.CashierID.ToString());
			clsStreamWriter.WriteLine("CashierName: {0}", Details.CustomerName);
			clsStreamWriter.WriteLine("TerminalNo: {0}", Details.TerminalNo);
			clsStreamWriter.WriteLine("TransactionDate: {0}", Details.TransactionDate.ToString("MMM/dd/yyyy hh:mm tt"));
			clsStreamWriter.WriteLine("DateSuspended: {0}", Details.DateSuspended.ToString("MMM/dd/yyyy hh:mm tt"));
			clsStreamWriter.WriteLine("TransactionStatus: {0}", Details.TransactionStatus.ToString("g"));

			string Line = "-".PadRight(1093,'-');
			clsStreamWriter.WriteLine(Line);

			//this is for item header
			string ItemNo = "ItemNo".PadRight(8, ' ');
			string ProductID = "ProdID".PadRight(8, ' ');
			string ProductCode = "ProductCode".PadRight(20, ' ');
			string BarCode = "BarCode".PadRight(25, ' ');
			string Description = "Description".PadRight(50, ' ');
			string ProductUnitID = "UnitID".PadRight(8, ' ');
			string ProductUnitCode = "UnitCode".PadRight(30, ' ');
			string Quantity = "Quantity".PadLeft(20, ' ');
			string Price = "Price".PadLeft(20, ' ');
			string Discount = "Discount".PadLeft(20, ' ');
			string ItemDiscount = "Item Discount".PadLeft(20, ' ');
			string ItemDiscountType = "Discount Type".PadLeft(20, ' ');
			string Amount = "Amount".PadLeft(20, ' ');
			string VariationsMatrixID = "VariationsMatrixID".PadRight(19, ' ');
			string MatrixDescription = "MatrixDescription".PadRight(255, ' ');
			string ProductGroup = "ProductGroup".PadRight(50, ' ');
			string ProductSubGroup = "ProductSubGroup".PadRight(50, ' ');
			string DiscountCode = "DiscountCode".PadRight(30, ' ');
			string DiscountRemarks = "DiscountRemarks".PadRight(255, ' ');
			string TransactionItemStatus = "Status".PadRight(10, ' ');
			string PromoQuantity = "PromoQuantity".PadLeft(20, ' ');
			string PromoValue = "PromoValue".PadLeft(20, ' ');
			string PromoInPercent = "PIn%".PadLeft(5, ' ');
			string PromoType = "PromoType".PadLeft(30, ' ');
			string PromoApplied = "PromoApplied".PadLeft(20, ' ');
			string PurchasePrice = "PurchasePrice".PadLeft(20, ' ');
			string PurchaseAmount = "PurchaseAmount".PadLeft(20, ' ');
			clsStreamWriter.WriteLine("{0}{1}{2}{3}{4}{5}{6}{7}{8}{9}{10}{11} {12}{13}{14}{15}{16}{17}{18}{19}{20}{21}{22}{23}{24}{25}{26}",ItemNo, ProductID, 
				ProductCode, BarCode, Description, ProductUnitID, ProductUnitCode,
				Quantity, Price, Discount, ItemDiscount, ItemDiscountType, Amount, VariationsMatrixID, MatrixDescription, 
				ProductGroup, ProductSubGroup, DiscountCode, DiscountRemarks, TransactionItemStatus,
				PromoQuantity, PromoValue, PromoInPercent, PromoType, PromoApplied, PurchasePrice, PurchaseAmount);  
			
			clsStreamWriter.WriteLine(Line);

			clsStreamWriter.Flush();
			clsStreamWriter.Close();
		}
		
		public void AddItem(SalesTransactionItemDetails Details, DateTime TransactionDate)
		{
			InitializeWriter(TransactionDate);

			string ItemNo = Details.ItemNo.ToString().PadRight(8, ' ');
			string ProductID = Details.ProductID.ToString().PadRight(8, ' ');
			if (Details.ProductCode == null)	{	Details.ProductCode = "";	}
			string ProductCode = Details.ProductCode.PadRight(20, ' ');
			string BarCode = Details.BarCode.PadRight(25, ' ');
			if (Details.Description == null)	{	Details.Description = "";	}
			string Description = Details.Description.Replace(Environment.NewLine,"").PadRight(50, ' ');
			string ProductUnitID = Details.ProductUnitID.ToString().PadRight(8, ' ');
			if (Details.ProductUnitCode == null)	{	Details.ProductUnitCode = "";	}
			string ProductUnitCode = Details.ProductUnitCode.PadRight(30, ' ');
			string Quantity = Details.Quantity.ToString().PadLeft(20, ' ');
			string Price = Details.Price.ToString("#,##0.#0").PadLeft(20, ' ');
			string Discount = Details.Discount.ToString("#,##0.#0").PadLeft(20, ' ');
			string ItemDiscount = Details.ItemDiscount.ToString("#,##0.#0").PadLeft(20, ' ');
			string ItemDiscountType = Details.ItemDiscountType.ToString("G").PadLeft(20, ' ');
			string Amount = Details.Amount.ToString("#,##0.#0").PadLeft(20, ' ');
			string VariationsMatrixID = Details.VariationsMatrixID.ToString().PadRight(19, ' ');
			if (Details.MatrixDescription == null)	{	Details.MatrixDescription = "";	}
			string MatrixDescription = Details.MatrixDescription.PadRight(255, ' ');
			if (Details.ProductGroup == null)	{	Details.ProductGroup = "";	}
			string ProductGroup = Details.ProductGroup.PadRight(50, ' ');
			if (Details.ProductSubGroup == null)	{	Details.ProductSubGroup = "";	}
			string ProductSubGroup = Details.ProductSubGroup.PadRight(50, ' ');
			if (Details.DiscountCode == null)	{	Details.DiscountCode = "";	}
			string DiscountCode = Details.DiscountCode.PadRight(30, ' ');
			if (Details.DiscountRemarks == null)	{	Details.DiscountRemarks = "";	}
			string DiscountRemarks = Details.DiscountRemarks.PadRight(255, ' ');
			string TransactionItemStatus = Details.TransactionItemStatus.ToString("G").PadRight(10, ' ');
			string PackageQuantity = Details.PackageQuantity.ToString("#,##0.#0").PadLeft(20, ' ');
			string PromoQuantity = Details.PromoQuantity.ToString("#,##0.#0").PadLeft(20, ' ');
			string PromoValue = Details.PromoValue.ToString("#,##0.#0").PadLeft(20, ' ');
			string PromoInPercent = Details.PromoInPercent.ToString().PadLeft(5, ' ');
			string PromoType = Details.PromoType.ToString("G").PadLeft(30, ' ');
			string PromoApplied = Details.PromoApplied.ToString("#,##0.#0").PadLeft(20, ' ');
			string PurchasePrice = Details.PurchasePrice.ToString("#,##0.#0").PadLeft(20, ' ');
			string PurchaseAmount = Details.PurchaseAmount.ToString("#,##0.#0").PadLeft(20, ' ');

			clsStreamWriter.WriteLine("{0}{1}{2}{3}{4}{5}{6}{7}{8}{9}{10}{11} {12}{13}{14}{15}{16}{17}{18}{19}{20}{21}{22}{23}{24}{25}{26}",ItemNo, ProductID, 
				ProductCode, BarCode, Description, ProductUnitID, ProductUnitCode,
				Quantity, Price, Discount, ItemDiscount, ItemDiscountType, Amount, VariationsMatrixID, MatrixDescription, 
				ProductGroup, ProductSubGroup, DiscountCode, DiscountRemarks, TransactionItemStatus, 
				PromoQuantity, PromoValue, PromoInPercent, PromoType, PromoApplied, PurchasePrice, PurchaseAmount); 
			
			clsStreamWriter.Flush();
			clsStreamWriter.Close();
		}

		public void AddItemFooter(ItemFooterDetails Details, DateTime TransactionDate)
		{
			InitializeWriter(TransactionDate);

			string Line = "-".PadRight(1093,'-');
			clsStreamWriter.WriteLine(Line);

			string ItemNo = "Total".PadRight(8, ' ');
			string ProductID = "".PadRight(8, '.');
			string ProductCode = "".PadRight(20, '.');
			string BarCode = "".PadRight(25, ' ');
			string Description = "".PadRight(50, ' ');
			string ProductUnitID = "".PadRight(8, ' ');
			string ProductUnitCode = "".PadRight(30, ' ');
			string Quantity = Details.Quantity.ToString().PadLeft(20, ' ');
			string Price = Details.Price.ToString("#,##0.#0").PadLeft(20, ' ');
			string Discount = Details.Discount.ToString("#,##0.#0").PadLeft(20, ' ');
			string ItemDiscount = Details.ItemDiscount.ToString("#,##0.#0").PadLeft(20, ' ');
			string ItemDiscountType = "".PadLeft(21, ' ');
			string Amount = Details.Amount.ToString("#,##0.#0").PadLeft(20, ' ');
			
			clsStreamWriter.WriteLine("{0}{1}{2}{3}{4}{5}{6}{7}{8}{9}{10}{11}{12}",ItemNo, ProductID, ProductCode, BarCode, Description, ProductUnitID, ProductUnitCode,
				Quantity, Price, Discount, ItemDiscount, ItemDiscountType, Amount); 

			clsStreamWriter.Flush();
			clsStreamWriter.Close();
		}
		
		public void AddTransactionFooter(FooterDetails Details, DateTime TransactionDate)
		{
			InitializeWriter(TransactionDate);

			clsStreamWriter.WriteLine(Environment.NewLine);
			//this is for transaction header
			clsStreamWriter.WriteLine("SubTotal      : {0}", Details.SubTotal.ToString("#,##0.#0").PadLeft(20, ' '));
			clsStreamWriter.WriteLine("Discount      : {0}", Details.Discount.ToString("#,##0.#0").PadLeft(20, ' '));
			clsStreamWriter.WriteLine("VAT           : {0}", Details.VAT.ToString("#,##0.#0").PadLeft(20, ' '));
			clsStreamWriter.WriteLine("VatableAmount : {0}", Details.VatableAmount.ToString("#,##0.#0").PadLeft(20, ' '));
			clsStreamWriter.WriteLine("EVAT          : {0}", Details.EVAT.ToString("#,##0.#0").PadLeft(20, ' '));
			clsStreamWriter.WriteLine("EVatableAmount: {0}", Details.EVatableAmount.ToString("#,##0.#0").PadLeft(20, ' '));
			clsStreamWriter.WriteLine("LocalTax      : {0}", Details.LocalTax.ToString("#,##0.#0").PadLeft(20, ' '));
			clsStreamWriter.WriteLine("AmountPaid    : {0}", Details.AmountPaid.ToString("#,##0.#0").PadLeft(20, ' '));
			clsStreamWriter.WriteLine("BalanceAmount : {0}", Details.BalanceAmount.ToString("#,##0.#0").PadLeft(20, ' '));
			clsStreamWriter.WriteLine("ChangeAmount  : {0}", Details.ChangeAmount.ToString("#,##0.#0").PadLeft(20, ' '));

			clsStreamWriter.Flush();
			clsStreamWriter.Close();
		}
		
		public void WriteLine(string Line, DateTime TransactionDate)
		{
			InitializeWriter(TransactionDate);

			clsStreamWriter.WriteLine(Line);

			clsStreamWriter.Flush();
			clsStreamWriter.Close();
		}

		#endregion

		#region Private methods

		private string GetRootDirectory(DateTime TransactionDate)
		{
			string stPath = System.Configuration.ConfigurationManager.AppSettings["TransactionPath"].ToString();
			if (!stPath.EndsWith(@"\"))
			{
				stPath += @"\";
			}
			stPath += TransactionDate.ToString("MMM") + @"\";

			if (!Directory.Exists(stPath))
			{
				Directory.CreateDirectory(stPath);
			}
			return stPath;
		}

		private string GetTransactionNo()
		{
			AceSoft.RetailPlus.Data.Transaction clsTransaction = new AceSoft.RetailPlus.Data.Transaction();
			string stRetValue = clsTransaction.CreateTransactionNo();
			clsTransaction.CommitAndDispose();
			return stRetValue;

		}
		#endregion
	}
}

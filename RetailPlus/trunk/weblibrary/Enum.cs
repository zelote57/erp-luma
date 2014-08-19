using System;

/******************************************************************************
	**		Auth: Lemuel E. Aceron
	**		Date: March 29, 2005
	***************************************************************************
	**		Change History
	***************************************************************************
	**		Date:			Author:				Description:
	**		--------		--------			-------------------------------
	**      
	***************************************************************************/

namespace AceSoft.RetailPlus
{

	public enum SearchCategoryID
	{
		AllSources			= 0,		
		AccessGroups		= 1,
		AccessUsers			= 2,
		Contacts			= 3,
		ContactGroups		= 4,
		Countries			= 5,
		Products			= 6,
		Variations			= 7,
		ProductVariations	= 8,
		ProductGroups		= 9,
		ProductSubGroups	= 10,
		Units				= 11,
		Discounts			= 12,
		InventoryList		= 13,
		StockTypes			= 14,
		StockTrans			= 15,
		Promos				= 16,
		NotApplicable		= 17,
		ProductGroupVariations			= 18,
		ProductGroupVariationsMatrix	= 19,
		ChargeType						= 20,
		ProductGroupAdditionalCharges	= 21,
		CardTypes						= 22,
		ProductVariationsMatrix			= 23,
		Branch							= 24,
		PurchaseOrders					= 25,
		Vendors							= 26,
		PurchaseJournals				= 27,
		AccountSummary					= 28,
		AccountCategory					= 29,
		ChartOfAccounts					= 30,
		PaymentJournals					= 31,
		PurchaseReturns					= 32,
		PurchaseDebitMemo				= 33,
		Customers						= 34,
		SalesOrders						= 35,
		SalesJournals					= 36,
		SalesReturns					= 37,
		SalesCreditMemo					= 38,
		TransferIn						= 39,
		TransferOut						= 40,
		InvAdjustment					= 41,
		CloseInventory					= 42,
		GeneralJournals                 = 43,
		Positions                       = 44,
		Departments                     = 45,
        RewardMembers                   = 46,
        CustomerDetailed                = 47,
        Banks                           = 48
	}

	public enum HorizontalNavID
	{
		Login				= 0,
		Home				= 1,
		MasterFiles			= 2,
		Inventory			= 3,
		Reports				= 4,
		AdministrationFiles = 5,
		GeneralLedger		= 6,
		PurchasesAndPayables= 7,
		SalesAndReceivables	= 8,
        Rewards             = 9,
        Credits             = 10
	}
	
}

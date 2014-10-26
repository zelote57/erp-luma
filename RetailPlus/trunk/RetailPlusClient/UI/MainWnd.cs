using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Threading;
using System.Text;
using System.Management;

using AceSoft.RetailPlus.Reports;
using AceSoft.RetailPlus.Security;
using MySql.Data.MySqlClient;
using System.Runtime.InteropServices;
using System.IO;

namespace AceSoft.RetailPlus.Client.UI
{
	public class MainWnd : MainWndExtension 
	{
		#region Declarations

		private IContainer components;
		private DataGrid dgItems;
		private DataGridTableStyle dgStyle;
		private DataGridTextBoxColumn TransactionItemsID;
		private DataGridTextBoxColumn ItemNo;
		private DataGridTextBoxColumn ProductID;
		private DataGridTextBoxColumn ProductCode;
		private DataGridTextBoxColumn BarCode;
		private DataGridTextBoxColumn Description;
		private DataGridTextBoxColumn ProductUnitID;
		private DataGridTextBoxColumn ProductUnitCode;
		private DataGridTextBoxColumn Quantity;
		private DataGridTextBoxColumn Price;
		private DataGridTextBoxColumn Discount;
		private DataGridTextBoxColumn ItemDiscount;
		private DataGridTextBoxColumn ItemDiscountType;
		private DataGridTextBoxColumn Amount;
		private DataGridTextBoxColumn VAT;
		private DataGridTextBoxColumn EVAT;
		private DataGridTextBoxColumn LocalTax;
		private DataGridTextBoxColumn VariationsMatrixID;
		private DataGridTextBoxColumn MatrixDescription;
		private DataGridTextBoxColumn ProductGroup;
		private DataGridTextBoxColumn ProductSubGroup;
		private DataGridTextBoxColumn TransactionItemStat;
		private DataGridTextBoxColumn DiscountCode;
		private DataGridTextBoxColumn DiscountRemarks;
		private DataGridTextBoxColumn ProductPackageID;
		private DataGridTextBoxColumn MatrixPackageID;
		private DataGridTextBoxColumn PackageQuantity;
		private DataGridTextBoxColumn PromoQuantity;
		private DataGridTextBoxColumn PromoValue;
		private DataGridTextBoxColumn PromoInPercent;
		private DataGridTextBoxColumn PromoType;
		private DataGridTextBoxColumn PromoApplied;
		private DataGridTextBoxColumn PurchasePrice;
		private DataGridTextBoxColumn PurchaseAmount;
		private DataGridTextBoxColumn IncludeInSubtotalDiscount;
		private DataGridTextBoxColumn OrderSlipPrinter;
		private DataGridTextBoxColumn OrderSlipPrinted;
		private DataGridTextBoxColumn PercentageCommision;
		private DataGridTextBoxColumn Commision;
		private Label lblDescription;
		private Label lblCategory;
		private Label lblProperties;
		private Panel panLocked;
		private Label lblPropertiesName;
		private Label lblCategoryName;
		private Label lblDescriptionName;
		private Label lblPress;
		private Label lblThisStation;
		private PictureBox imgIcon;
		private GroupBox grpTop;
		private Label lblTransactionNoName;
		private Label lblTransNo;
		private Label lblCustomer;
		private GroupBox grpBottom;
		private Label lblCompanyName;
		private Label lblCashierName;
		private Label lblTransDate;
		private Label lblTerminalNo;
		private Label lblTerminalNoName;
		private Label lblCashier;
		private GroupBox grptxtBarcode;
		private TextBox txtBarCode;
		private GroupBox grpItems;
		private PictureBox imgCompanyLogo;
		private GroupBox grpMarquee;
		private Label lblMessage;
		private GroupBox grpRLC;
		private Button cmdRLCClose;
		private Label lblMallForwarderStatus;
        //private System.Windows.Forms.Timer tmr;
		private System.Windows.Forms.Timer tmrRLC;
        private Label lblConsignment;
		private KeyBoardHook.KeyBoardHook Hook;
		private int tempHeight = 0, tempWidth = 0;
		private Label lblAgent;
		private Label lblAgentPositionDepartment;
		private GroupBox panSubTotal;
		private Label lblSubtotalName;
		private Label lblSubTotal;
		private Label lblTransDiscount;
		private Label lblTransCharge;
		private Label lblCurrency;
		private Label lblOrderType;
		private System.Windows.Forms.Timer tmrLogo;
		private Thread MarqueeThread;

		#endregion

        #region Public Get/Set Properties

        public bool Locked
        {
            get { return mboLocked; }
            set { mboLocked = value; }
        }

        #endregion

		#region Constructors and Destructors

		public MainWnd()
		{
			InitializeComponent();
            
            //initialized loadoptions
            this.LoadOptions();

			this.Lock();
			try
			{
				MarqueeThread = new Thread(new ThreadStart(DrawMovingText));
			}
			catch { }
		}
		protected override void Dispose(bool disposing)
		{
			if (mclsTerminalDetails.FORM_Behavior == FORM_Behavior.MODAL)
			{
				TaskBarWnd taskbar = new TaskBarWnd();
				taskbar.Show();
				Cursor.Show();
				Resolution ChangeRes = new Resolution(tempWidth, tempHeight);
			}
			if (disposing)
			{
				if (components != null)
				{
					components.Dispose();
				}
			}
			base.Dispose(disposing);
		}
		#region Windows Form Designer generated code
		private void InitializeComponent()
		{
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainWnd));
            this.dgItems = new System.Windows.Forms.DataGrid();
            this.dgStyle = new System.Windows.Forms.DataGridTableStyle();
            this.TransactionItemsID = new System.Windows.Forms.DataGridTextBoxColumn();
            this.ItemNo = new System.Windows.Forms.DataGridTextBoxColumn();
            this.ProductID = new System.Windows.Forms.DataGridTextBoxColumn();
            this.ProductCode = new System.Windows.Forms.DataGridTextBoxColumn();
            this.BarCode = new System.Windows.Forms.DataGridTextBoxColumn();
            this.Description = new System.Windows.Forms.DataGridTextBoxColumn();
            this.ProductUnitID = new System.Windows.Forms.DataGridTextBoxColumn();
            this.ProductUnitCode = new System.Windows.Forms.DataGridTextBoxColumn();
            this.Quantity = new System.Windows.Forms.DataGridTextBoxColumn();
            this.Price = new System.Windows.Forms.DataGridTextBoxColumn();
            this.Discount = new System.Windows.Forms.DataGridTextBoxColumn();
            this.ItemDiscount = new System.Windows.Forms.DataGridTextBoxColumn();
            this.ItemDiscountType = new System.Windows.Forms.DataGridTextBoxColumn();
            this.Amount = new System.Windows.Forms.DataGridTextBoxColumn();
            this.VAT = new System.Windows.Forms.DataGridTextBoxColumn();
            this.EVAT = new System.Windows.Forms.DataGridTextBoxColumn();
            this.LocalTax = new System.Windows.Forms.DataGridTextBoxColumn();
            this.VariationsMatrixID = new System.Windows.Forms.DataGridTextBoxColumn();
            this.MatrixDescription = new System.Windows.Forms.DataGridTextBoxColumn();
            this.ProductGroup = new System.Windows.Forms.DataGridTextBoxColumn();
            this.ProductSubGroup = new System.Windows.Forms.DataGridTextBoxColumn();
            this.TransactionItemStat = new System.Windows.Forms.DataGridTextBoxColumn();
            this.DiscountCode = new System.Windows.Forms.DataGridTextBoxColumn();
            this.DiscountRemarks = new System.Windows.Forms.DataGridTextBoxColumn();
            this.ProductPackageID = new System.Windows.Forms.DataGridTextBoxColumn();
            this.MatrixPackageID = new System.Windows.Forms.DataGridTextBoxColumn();
            this.PackageQuantity = new System.Windows.Forms.DataGridTextBoxColumn();
            this.PromoQuantity = new System.Windows.Forms.DataGridTextBoxColumn();
            this.PromoValue = new System.Windows.Forms.DataGridTextBoxColumn();
            this.PromoInPercent = new System.Windows.Forms.DataGridTextBoxColumn();
            this.PromoType = new System.Windows.Forms.DataGridTextBoxColumn();
            this.PromoApplied = new System.Windows.Forms.DataGridTextBoxColumn();
            this.PurchasePrice = new System.Windows.Forms.DataGridTextBoxColumn();
            this.PurchaseAmount = new System.Windows.Forms.DataGridTextBoxColumn();
            this.IncludeInSubtotalDiscount = new System.Windows.Forms.DataGridTextBoxColumn();
            this.OrderSlipPrinter = new System.Windows.Forms.DataGridTextBoxColumn();
            this.OrderSlipPrinted = new System.Windows.Forms.DataGridTextBoxColumn();
            this.PercentageCommision = new System.Windows.Forms.DataGridTextBoxColumn();
            this.Commision = new System.Windows.Forms.DataGridTextBoxColumn();
            this.grpItems = new System.Windows.Forms.GroupBox();
            this.lblConsignment = new System.Windows.Forms.Label();
            this.lblProperties = new System.Windows.Forms.Label();
            this.lblCategory = new System.Windows.Forms.Label();
            this.lblPropertiesName = new System.Windows.Forms.Label();
            this.lblCategoryName = new System.Windows.Forms.Label();
            this.lblDescription = new System.Windows.Forms.Label();
            this.lblDescriptionName = new System.Windows.Forms.Label();
            this.panLocked = new System.Windows.Forms.Panel();
            this.lblPress = new System.Windows.Forms.Label();
            this.lblThisStation = new System.Windows.Forms.Label();
            this.imgIcon = new System.Windows.Forms.PictureBox();
            this.grpTop = new System.Windows.Forms.GroupBox();
            this.lblCustomer = new System.Windows.Forms.Label();
            this.lblTransNo = new System.Windows.Forms.Label();
            this.lblTransactionNoName = new System.Windows.Forms.Label();
            this.grpBottom = new System.Windows.Forms.GroupBox();
            this.lblAgent = new System.Windows.Forms.Label();
            this.lblCashier = new System.Windows.Forms.Label();
            this.lblTerminalNoName = new System.Windows.Forms.Label();
            this.lblTerminalNo = new System.Windows.Forms.Label();
            this.lblTransDate = new System.Windows.Forms.Label();
            this.lblCashierName = new System.Windows.Forms.Label();
            this.lblCompanyName = new System.Windows.Forms.Label();
            this.grptxtBarcode = new System.Windows.Forms.GroupBox();
            this.txtBarCode = new System.Windows.Forms.TextBox();
            this.grpMarquee = new System.Windows.Forms.GroupBox();
            this.lblMessage = new System.Windows.Forms.Label();
            this.lblAgentPositionDepartment = new System.Windows.Forms.Label();
            this.tmr = new System.Windows.Forms.Timer(this.components);
            this.tmrRLC = new System.Windows.Forms.Timer(this.components);
            this.grpRLC = new System.Windows.Forms.GroupBox();
            this.cmdRLCClose = new System.Windows.Forms.Button();
            this.lblMallForwarderStatus = new System.Windows.Forms.Label();
            this.imgCompanyLogo = new System.Windows.Forms.PictureBox();
            this.panSubTotal = new System.Windows.Forms.GroupBox();
            this.lblSubtotalName = new System.Windows.Forms.Label();
            this.lblSubTotal = new System.Windows.Forms.Label();
            this.lblTransDiscount = new System.Windows.Forms.Label();
            this.lblTransCharge = new System.Windows.Forms.Label();
            this.lblCurrency = new System.Windows.Forms.Label();
            this.lblOrderType = new System.Windows.Forms.Label();
            this.tmrLogo = new System.Windows.Forms.Timer(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.dgItems)).BeginInit();
            this.grpItems.SuspendLayout();
            this.panLocked.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.imgIcon)).BeginInit();
            this.grpTop.SuspendLayout();
            this.grpBottom.SuspendLayout();
            this.grptxtBarcode.SuspendLayout();
            this.grpMarquee.SuspendLayout();
            this.grpRLC.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.imgCompanyLogo)).BeginInit();
            this.panSubTotal.SuspendLayout();
            this.SuspendLayout();
            // 
            // dgItems
            // 
            this.dgItems.AlternatingBackColor = System.Drawing.Color.White;
            this.dgItems.BackColor = System.Drawing.Color.White;
            this.dgItems.BackgroundColor = System.Drawing.Color.White;
            this.dgItems.CaptionBackColor = System.Drawing.Color.White;
            this.dgItems.CaptionForeColor = System.Drawing.Color.Blue;
            this.dgItems.CaptionVisible = false;
            this.dgItems.CausesValidation = false;
            this.dgItems.DataMember = "";
            this.dgItems.FlatMode = true;
            this.dgItems.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dgItems.GridLineColor = System.Drawing.Color.Blue;
            this.dgItems.HeaderBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(81)))), ((int)(((byte)(153)))));
            this.dgItems.HeaderFont = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dgItems.HeaderForeColor = System.Drawing.Color.White;
            this.dgItems.Location = new System.Drawing.Point(0, 28);
            this.dgItems.Name = "dgItems";
            this.dgItems.ParentRowsBackColor = System.Drawing.Color.Blue;
            this.dgItems.PreferredRowHeight = 100;
            this.dgItems.ReadOnly = true;
            this.dgItems.RowHeadersVisible = false;
            this.dgItems.RowHeaderWidth = 40;
            this.dgItems.SelectionBackColor = System.Drawing.Color.RoyalBlue;
            this.dgItems.SelectionForeColor = System.Drawing.Color.White;
            this.dgItems.Size = new System.Drawing.Size(791, 511);
            this.dgItems.TabIndex = 49;
            this.dgItems.TableStyles.AddRange(new System.Windows.Forms.DataGridTableStyle[] {
            this.dgStyle});
            this.dgItems.TabStop = false;
            this.dgItems.CurrentCellChanged += new System.EventHandler(this.dgItems_CurrentCellChanged);
            this.dgItems.Click += new System.EventHandler(this.dgItems_Click);
            this.dgItems.KeyUp += new System.Windows.Forms.KeyEventHandler(this.dgItems_KeyUp);
            // 
            // dgStyle
            // 
            this.dgStyle.AllowSorting = false;
            this.dgStyle.AlternatingBackColor = System.Drawing.Color.White;
            this.dgStyle.BackColor = System.Drawing.Color.White;
            this.dgStyle.DataGrid = this.dgItems;
            this.dgStyle.GridColumnStyles.AddRange(new System.Windows.Forms.DataGridColumnStyle[] {
            this.TransactionItemsID,
            this.ItemNo,
            this.ProductID,
            this.ProductCode,
            this.BarCode,
            this.Description,
            this.ProductUnitID,
            this.ProductUnitCode,
            this.Quantity,
            this.Price,
            this.Discount,
            this.ItemDiscount,
            this.ItemDiscountType,
            this.Amount,
            this.VAT,
            this.EVAT,
            this.LocalTax,
            this.VariationsMatrixID,
            this.MatrixDescription,
            this.ProductGroup,
            this.ProductSubGroup,
            this.TransactionItemStat,
            this.DiscountCode,
            this.DiscountRemarks,
            this.ProductPackageID,
            this.MatrixPackageID,
            this.PackageQuantity,
            this.PromoQuantity,
            this.PromoValue,
            this.PromoInPercent,
            this.PromoType,
            this.PromoApplied,
            this.PurchasePrice,
            this.PurchaseAmount,
            this.IncludeInSubtotalDiscount,
            this.OrderSlipPrinter,
            this.OrderSlipPrinted,
            this.PercentageCommision,
            this.Commision});
            this.dgStyle.GridLineColor = System.Drawing.Color.Blue;
            this.dgStyle.GridLineStyle = System.Windows.Forms.DataGridLineStyle.None;
            this.dgStyle.HeaderBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(81)))), ((int)(((byte)(153)))));
            this.dgStyle.HeaderFont = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dgStyle.HeaderForeColor = System.Drawing.Color.White;
            this.dgStyle.MappingName = "tblProducts";
            this.dgStyle.PreferredColumnWidth = 0;
            this.dgStyle.PreferredRowHeight = 50;
            this.dgStyle.ReadOnly = true;
            this.dgStyle.RowHeadersVisible = false;
            this.dgStyle.SelectionBackColor = System.Drawing.Color.RoyalBlue;
            this.dgStyle.SelectionForeColor = System.Drawing.Color.White;
            // 
            // TransactionItemsID
            // 
            this.TransactionItemsID.Format = "";
            this.TransactionItemsID.FormatInfo = null;
            this.TransactionItemsID.MappingName = "TransactionItemsID";
            this.TransactionItemsID.NullText = "";
            this.TransactionItemsID.ReadOnly = true;
            this.TransactionItemsID.Width = 0;
            // 
            // ItemNo
            // 
            this.ItemNo.Alignment = System.Windows.Forms.HorizontalAlignment.Center;
            this.ItemNo.Format = "";
            this.ItemNo.FormatInfo = null;
            this.ItemNo.HeaderText = "ItemNo";
            this.ItemNo.MappingName = "ItemNo";
            this.ItemNo.NullText = "";
            this.ItemNo.ReadOnly = true;
            this.ItemNo.Width = 0;
            // 
            // ProductID
            // 
            this.ProductID.Format = "";
            this.ProductID.FormatInfo = null;
            this.ProductID.MappingName = "ProductID";
            this.ProductID.NullText = "";
            this.ProductID.ReadOnly = true;
            this.ProductID.Width = 0;
            // 
            // ProductCode
            // 
            this.ProductCode.Format = "";
            this.ProductCode.FormatInfo = null;
            this.ProductCode.MappingName = "ProductCode";
            this.ProductCode.NullText = "";
            this.ProductCode.ReadOnly = true;
            this.ProductCode.Width = 0;
            // 
            // BarCode
            // 
            this.BarCode.Format = "";
            this.BarCode.FormatInfo = null;
            this.BarCode.MappingName = "BarCode";
            this.BarCode.NullText = "";
            this.BarCode.ReadOnly = true;
            this.BarCode.Width = 0;
            // 
            // Description
            // 
            this.Description.Format = "";
            this.Description.FormatInfo = null;
            this.Description.HeaderText = "Description";
            this.Description.MappingName = "Description";
            this.Description.NullText = "";
            this.Description.ReadOnly = true;
            this.Description.Width = 0;
            // 
            // ProductUnitID
            // 
            this.ProductUnitID.Format = "";
            this.ProductUnitID.FormatInfo = null;
            this.ProductUnitID.MappingName = "ProductUnitID";
            this.ProductUnitID.NullText = "";
            this.ProductUnitID.ReadOnly = true;
            this.ProductUnitID.Width = 0;
            // 
            // ProductUnitCode
            // 
            this.ProductUnitCode.Format = "";
            this.ProductUnitCode.FormatInfo = null;
            this.ProductUnitCode.MappingName = "ProductUnitCode";
            this.ProductUnitCode.NullText = "";
            this.ProductUnitCode.ReadOnly = true;
            this.ProductUnitCode.Width = 0;
            // 
            // Quantity
            // 
            this.Quantity.Alignment = System.Windows.Forms.HorizontalAlignment.Center;
            this.Quantity.Format = "###,##0.###";
            this.Quantity.FormatInfo = null;
            this.Quantity.HeaderText = "Quantity";
            this.Quantity.MappingName = "Quantity";
            this.Quantity.NullText = "";
            this.Quantity.ReadOnly = true;
            this.Quantity.Width = 0;
            // 
            // Price
            // 
            this.Price.Alignment = System.Windows.Forms.HorizontalAlignment.Right;
            this.Price.Format = "###,##0.#0";
            this.Price.FormatInfo = null;
            this.Price.HeaderText = "Price";
            this.Price.MappingName = "Price";
            this.Price.NullText = "";
            this.Price.ReadOnly = true;
            this.Price.Width = 0;
            // 
            // Discount
            // 
            this.Discount.Alignment = System.Windows.Forms.HorizontalAlignment.Right;
            this.Discount.Format = "###,##0";
            this.Discount.FormatInfo = null;
            this.Discount.MappingName = "Discount";
            this.Discount.NullText = "";
            this.Discount.ReadOnly = true;
            this.Discount.Width = 0;
            // 
            // ItemDiscount
            // 
            this.ItemDiscount.Alignment = System.Windows.Forms.HorizontalAlignment.Right;
            this.ItemDiscount.Format = "###,##0";
            this.ItemDiscount.FormatInfo = null;
            this.ItemDiscount.MappingName = "ItemDiscount";
            this.ItemDiscount.NullText = "";
            this.ItemDiscount.ReadOnly = true;
            this.ItemDiscount.Width = 0;
            // 
            // ItemDiscountType
            // 
            this.ItemDiscountType.Alignment = System.Windows.Forms.HorizontalAlignment.Right;
            this.ItemDiscountType.Format = "";
            this.ItemDiscountType.FormatInfo = null;
            this.ItemDiscountType.MappingName = "ItemDiscountType";
            this.ItemDiscountType.NullText = "";
            this.ItemDiscountType.ReadOnly = true;
            this.ItemDiscountType.Width = 0;
            // 
            // Amount
            // 
            this.Amount.Alignment = System.Windows.Forms.HorizontalAlignment.Right;
            this.Amount.Format = "#,##0.#0";
            this.Amount.FormatInfo = null;
            this.Amount.HeaderText = "Amount";
            this.Amount.MappingName = "Amount";
            this.Amount.NullText = "";
            this.Amount.ReadOnly = true;
            this.Amount.Width = 0;
            // 
            // VAT
            // 
            this.VAT.Format = "";
            this.VAT.FormatInfo = null;
            this.VAT.MappingName = "VAT";
            this.VAT.NullText = "";
            this.VAT.ReadOnly = true;
            this.VAT.Width = 0;
            // 
            // EVAT
            // 
            this.EVAT.Format = "";
            this.EVAT.FormatInfo = null;
            this.EVAT.MappingName = "EVAT";
            this.EVAT.NullText = "";
            this.EVAT.ReadOnly = true;
            this.EVAT.Width = 0;
            // 
            // LocalTax
            // 
            this.LocalTax.Format = "";
            this.LocalTax.FormatInfo = null;
            this.LocalTax.MappingName = "LocalTax";
            this.LocalTax.NullText = "";
            this.LocalTax.ReadOnly = true;
            this.LocalTax.Width = 0;
            // 
            // VariationsMatrixID
            // 
            this.VariationsMatrixID.Format = "";
            this.VariationsMatrixID.FormatInfo = null;
            this.VariationsMatrixID.MappingName = "VariationsMatrixID";
            this.VariationsMatrixID.NullText = "";
            this.VariationsMatrixID.ReadOnly = true;
            this.VariationsMatrixID.Width = 0;
            // 
            // MatrixDescription
            // 
            this.MatrixDescription.Format = "";
            this.MatrixDescription.FormatInfo = null;
            this.MatrixDescription.MappingName = "MatrixDescription";
            this.MatrixDescription.NullText = "";
            this.MatrixDescription.ReadOnly = true;
            this.MatrixDescription.Width = 0;
            // 
            // ProductGroup
            // 
            this.ProductGroup.Format = "";
            this.ProductGroup.FormatInfo = null;
            this.ProductGroup.MappingName = "ProductGroup";
            this.ProductGroup.NullText = "";
            this.ProductGroup.ReadOnly = true;
            this.ProductGroup.Width = 0;
            // 
            // ProductSubGroup
            // 
            this.ProductSubGroup.Format = "";
            this.ProductSubGroup.FormatInfo = null;
            this.ProductSubGroup.MappingName = "ProductSubGroup";
            this.ProductSubGroup.NullText = "";
            this.ProductSubGroup.ReadOnly = true;
            this.ProductSubGroup.Width = 0;
            // 
            // TransactionItemStat
            // 
            this.TransactionItemStat.Format = "";
            this.TransactionItemStat.FormatInfo = null;
            this.TransactionItemStat.MappingName = "TransactionItemStat";
            this.TransactionItemStat.NullText = "";
            this.TransactionItemStat.ReadOnly = true;
            this.TransactionItemStat.Width = 0;
            // 
            // DiscountCode
            // 
            this.DiscountCode.Format = "";
            this.DiscountCode.FormatInfo = null;
            this.DiscountCode.MappingName = "DiscountCode";
            this.DiscountCode.NullText = "";
            this.DiscountCode.ReadOnly = true;
            this.DiscountCode.Width = 0;
            // 
            // DiscountRemarks
            // 
            this.DiscountRemarks.Format = "";
            this.DiscountRemarks.FormatInfo = null;
            this.DiscountRemarks.MappingName = "DiscountRemarks";
            this.DiscountRemarks.NullText = "";
            this.DiscountRemarks.ReadOnly = true;
            this.DiscountRemarks.Width = 0;
            // 
            // ProductPackageID
            // 
            this.ProductPackageID.Format = "";
            this.ProductPackageID.FormatInfo = null;
            this.ProductPackageID.MappingName = "ProductPackageID";
            this.ProductPackageID.NullText = "";
            this.ProductPackageID.ReadOnly = true;
            this.ProductPackageID.Width = 0;
            // 
            // MatrixPackageID
            // 
            this.MatrixPackageID.Format = "";
            this.MatrixPackageID.FormatInfo = null;
            this.MatrixPackageID.MappingName = "MatrixPackageID";
            this.MatrixPackageID.NullText = "";
            this.MatrixPackageID.ReadOnly = true;
            this.MatrixPackageID.Width = 0;
            // 
            // PackageQuantity
            // 
            this.PackageQuantity.Format = "";
            this.PackageQuantity.FormatInfo = null;
            this.PackageQuantity.MappingName = "PackageQuantity";
            this.PackageQuantity.NullText = "";
            this.PackageQuantity.ReadOnly = true;
            this.PackageQuantity.Width = 0;
            // 
            // PromoQuantity
            // 
            this.PromoQuantity.Format = "";
            this.PromoQuantity.FormatInfo = null;
            this.PromoQuantity.MappingName = "PromoQuantity";
            this.PromoQuantity.NullText = "0";
            this.PromoQuantity.ReadOnly = true;
            this.PromoQuantity.Width = 0;
            // 
            // PromoValue
            // 
            this.PromoValue.Format = "";
            this.PromoValue.FormatInfo = null;
            this.PromoValue.MappingName = "PromoValue";
            this.PromoValue.NullText = "0";
            this.PromoValue.ReadOnly = true;
            this.PromoValue.Width = 0;
            // 
            // PromoInPercent
            // 
            this.PromoInPercent.Format = "";
            this.PromoInPercent.FormatInfo = null;
            this.PromoInPercent.MappingName = "PromoInPercent";
            this.PromoInPercent.NullText = "0";
            this.PromoInPercent.ReadOnly = true;
            this.PromoInPercent.Width = 0;
            // 
            // PromoType
            // 
            this.PromoType.Format = "";
            this.PromoType.FormatInfo = null;
            this.PromoType.MappingName = "PromoType";
            this.PromoType.NullText = "";
            this.PromoType.ReadOnly = true;
            this.PromoType.Width = 0;
            // 
            // PromoApplied
            // 
            this.PromoApplied.Format = "";
            this.PromoApplied.FormatInfo = null;
            this.PromoApplied.MappingName = "PromoApplied";
            this.PromoApplied.NullText = "";
            this.PromoApplied.ReadOnly = true;
            this.PromoApplied.Width = 0;
            // 
            // PurchasePrice
            // 
            this.PurchasePrice.Format = "";
            this.PurchasePrice.FormatInfo = null;
            this.PurchasePrice.MappingName = "PurchasePrice";
            this.PurchasePrice.NullText = "";
            this.PurchasePrice.ReadOnly = true;
            this.PurchasePrice.Width = 0;
            // 
            // PurchaseAmount
            // 
            this.PurchaseAmount.Format = "";
            this.PurchaseAmount.FormatInfo = null;
            this.PurchaseAmount.MappingName = "PurchaseAmount";
            this.PurchaseAmount.NullText = "";
            this.PurchaseAmount.ReadOnly = true;
            this.PurchaseAmount.Width = 0;
            // 
            // IncludeInSubtotalDiscount
            // 
            this.IncludeInSubtotalDiscount.Format = "";
            this.IncludeInSubtotalDiscount.FormatInfo = null;
            this.IncludeInSubtotalDiscount.MappingName = "IncludeInSubtotalDiscount";
            this.IncludeInSubtotalDiscount.NullText = "";
            this.IncludeInSubtotalDiscount.ReadOnly = true;
            this.IncludeInSubtotalDiscount.Width = 0;
            // 
            // OrderSlipPrinter
            // 
            this.OrderSlipPrinter.Format = "";
            this.OrderSlipPrinter.FormatInfo = null;
            this.OrderSlipPrinter.MappingName = "OrderSlipPrinter";
            this.OrderSlipPrinter.NullText = "";
            this.OrderSlipPrinter.ReadOnly = true;
            this.OrderSlipPrinter.Width = 0;
            // 
            // OrderSlipPrinted
            // 
            this.OrderSlipPrinted.Format = "";
            this.OrderSlipPrinted.FormatInfo = null;
            this.OrderSlipPrinted.MappingName = "OrderSlipPrinted";
            this.OrderSlipPrinted.NullText = "";
            this.OrderSlipPrinted.ReadOnly = true;
            this.OrderSlipPrinted.Width = 0;
            // 
            // PercentageCommision
            // 
            this.PercentageCommision.Format = "";
            this.PercentageCommision.FormatInfo = null;
            this.PercentageCommision.MappingName = "PercentageCommision";
            this.PercentageCommision.NullText = "";
            this.PercentageCommision.ReadOnly = true;
            this.PercentageCommision.Width = 0;
            // 
            // Commision
            // 
            this.Commision.Format = "";
            this.Commision.FormatInfo = null;
            this.Commision.MappingName = "Commision";
            this.Commision.NullText = "";
            this.Commision.ReadOnly = true;
            this.Commision.Width = 0;
            // 
            // grpItems
            // 
            this.grpItems.BackColor = System.Drawing.Color.White;
            this.grpItems.Controls.Add(this.lblConsignment);
            this.grpItems.Controls.Add(this.lblProperties);
            this.grpItems.Controls.Add(this.lblCategory);
            this.grpItems.Controls.Add(this.lblPropertiesName);
            this.grpItems.Controls.Add(this.lblCategoryName);
            this.grpItems.Controls.Add(this.lblDescription);
            this.grpItems.Controls.Add(this.lblDescriptionName);
            this.grpItems.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grpItems.ForeColor = System.Drawing.Color.YellowGreen;
            this.grpItems.Location = new System.Drawing.Point(792, 252);
            this.grpItems.Name = "grpItems";
            this.grpItems.Size = new System.Drawing.Size(224, 408);
            this.grpItems.TabIndex = 52;
            this.grpItems.TabStop = false;
            this.grpItems.Text = "Item Details";
            // 
            // lblConsignment
            // 
            this.lblConsignment.BackColor = System.Drawing.Color.RoyalBlue;
            this.lblConsignment.Font = new System.Drawing.Font("Tahoma", 12F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this.lblConsignment.ForeColor = System.Drawing.Color.Maroon;
            this.lblConsignment.Location = new System.Drawing.Point(2, 323);
            this.lblConsignment.Name = "lblConsignment";
            this.lblConsignment.Size = new System.Drawing.Size(223, 85);
            this.lblConsignment.TabIndex = 6;
            this.lblConsignment.Text = "C O N S I G N M E N T";
            this.lblConsignment.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblConsignment.Visible = false;
            // 
            // lblProperties
            // 
            this.lblProperties.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblProperties.ForeColor = System.Drawing.Color.Black;
            this.lblProperties.Location = new System.Drawing.Point(25, 132);
            this.lblProperties.Name = "lblProperties";
            this.lblProperties.Size = new System.Drawing.Size(181, 138);
            this.lblProperties.TabIndex = 5;
            this.lblProperties.Text = "Properties";
            // 
            // lblCategory
            // 
            this.lblCategory.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCategory.ForeColor = System.Drawing.Color.Black;
            this.lblCategory.Location = new System.Drawing.Point(25, 92);
            this.lblCategory.Name = "lblCategory";
            this.lblCategory.Size = new System.Drawing.Size(181, 14);
            this.lblCategory.TabIndex = 4;
            this.lblCategory.Text = "Category";
            // 
            // lblPropertiesName
            // 
            this.lblPropertiesName.AutoSize = true;
            this.lblPropertiesName.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPropertiesName.Location = new System.Drawing.Point(15, 115);
            this.lblPropertiesName.Name = "lblPropertiesName";
            this.lblPropertiesName.Size = new System.Drawing.Size(66, 13);
            this.lblPropertiesName.TabIndex = 3;
            this.lblPropertiesName.Text = "Properties";
            // 
            // lblCategoryName
            // 
            this.lblCategoryName.AutoSize = true;
            this.lblCategoryName.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCategoryName.Location = new System.Drawing.Point(15, 72);
            this.lblCategoryName.Name = "lblCategoryName";
            this.lblCategoryName.Size = new System.Drawing.Size(59, 13);
            this.lblCategoryName.TabIndex = 2;
            this.lblCategoryName.Text = "Category";
            // 
            // lblDescription
            // 
            this.lblDescription.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDescription.ForeColor = System.Drawing.Color.Black;
            this.lblDescription.Location = new System.Drawing.Point(25, 48);
            this.lblDescription.Name = "lblDescription";
            this.lblDescription.Size = new System.Drawing.Size(183, 14);
            this.lblDescription.TabIndex = 1;
            this.lblDescription.Text = "Description";
            // 
            // lblDescriptionName
            // 
            this.lblDescriptionName.AutoSize = true;
            this.lblDescriptionName.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDescriptionName.Location = new System.Drawing.Point(15, 28);
            this.lblDescriptionName.Name = "lblDescriptionName";
            this.lblDescriptionName.Size = new System.Drawing.Size(71, 13);
            this.lblDescriptionName.TabIndex = 0;
            this.lblDescriptionName.Text = "Description";
            // 
            // panLocked
            // 
            this.panLocked.BackColor = System.Drawing.SystemColors.ControlDark;
            this.panLocked.Controls.Add(this.lblPress);
            this.panLocked.Controls.Add(this.lblThisStation);
            this.panLocked.Controls.Add(this.imgIcon);
            this.panLocked.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.panLocked.Location = new System.Drawing.Point(244, 251);
            this.panLocked.Name = "panLocked";
            this.panLocked.Size = new System.Drawing.Size(360, 109);
            this.panLocked.TabIndex = 65;
            this.panLocked.Click += new System.EventHandler(this.panLocked_Click);
            // 
            // lblPress
            // 
            this.lblPress.AutoSize = true;
            this.lblPress.BackColor = System.Drawing.Color.Transparent;
            this.lblPress.Font = new System.Drawing.Font("Tahoma", 6.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPress.ForeColor = System.Drawing.Color.White;
            this.lblPress.Location = new System.Drawing.Point(68, 65);
            this.lblPress.Name = "lblPress";
            this.lblPress.Size = new System.Drawing.Size(283, 11);
            this.lblPress.TabIndex = 16;
            this.lblPress.Text = "Click here or Press the Enter key to login to the system.";
            // 
            // lblThisStation
            // 
            this.lblThisStation.AutoSize = true;
            this.lblThisStation.BackColor = System.Drawing.Color.Transparent;
            this.lblThisStation.Font = new System.Drawing.Font("Tahoma", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblThisStation.ForeColor = System.Drawing.Color.White;
            this.lblThisStation.Location = new System.Drawing.Point(80, 40);
            this.lblThisStation.Name = "lblThisStation";
            this.lblThisStation.Size = new System.Drawing.Size(241, 23);
            this.lblThisStation.TabIndex = 15;
            this.lblThisStation.Text = "NEXT COUNTER PLEASE";
            this.lblThisStation.Click += new System.EventHandler(this.lblThisStation_Click);
            // 
            // imgIcon
            // 
            this.imgIcon.BackColor = System.Drawing.Color.Blue;
            this.imgIcon.Location = new System.Drawing.Point(13, 30);
            this.imgIcon.Name = "imgIcon";
            this.imgIcon.Size = new System.Drawing.Size(49, 49);
            this.imgIcon.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.imgIcon.TabIndex = 13;
            this.imgIcon.TabStop = false;
            this.imgIcon.Click += new System.EventHandler(this.imgIcon_Click);
            // 
            // grpTop
            // 
            this.grpTop.Controls.Add(this.lblCustomer);
            this.grpTop.Controls.Add(this.lblTransNo);
            this.grpTop.Controls.Add(this.lblTransactionNoName);
            this.grpTop.Location = new System.Drawing.Point(0, -7);
            this.grpTop.Name = "grpTop";
            this.grpTop.Size = new System.Drawing.Size(1017, 37);
            this.grpTop.TabIndex = 75;
            this.grpTop.TabStop = false;
            // 
            // lblCustomer
            // 
            this.lblCustomer.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblCustomer.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCustomer.Location = new System.Drawing.Point(712, 9);
            this.lblCustomer.Name = "lblCustomer";
            this.lblCustomer.Size = new System.Drawing.Size(304, 26);
            this.lblCustomer.TabIndex = 56;
            this.lblCustomer.Text = "RetailPlus Customer ™";
            this.lblCustomer.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblCustomer.Click += new System.EventHandler(this.lblCustomer_Click);
            // 
            // lblTransNo
            // 
            this.lblTransNo.BackColor = System.Drawing.SystemColors.Control;
            this.lblTransNo.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblTransNo.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTransNo.Location = new System.Drawing.Point(132, 9);
            this.lblTransNo.Name = "lblTransNo";
            this.lblTransNo.Size = new System.Drawing.Size(580, 26);
            this.lblTransNo.TabIndex = 55;
            this.lblTransNo.Text = "READY...";
            this.lblTransNo.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblTransactionNoName
            // 
            this.lblTransactionNoName.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblTransactionNoName.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTransactionNoName.Location = new System.Drawing.Point(2, 9);
            this.lblTransactionNoName.Name = "lblTransactionNoName";
            this.lblTransactionNoName.Size = new System.Drawing.Size(129, 26);
            this.lblTransactionNoName.TabIndex = 54;
            this.lblTransactionNoName.Text = "  Transaction No :";
            this.lblTransactionNoName.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // grpBottom
            // 
            this.grpBottom.Controls.Add(this.lblAgent);
            this.grpBottom.Controls.Add(this.lblCashier);
            this.grpBottom.Controls.Add(this.lblTerminalNoName);
            this.grpBottom.Controls.Add(this.lblTerminalNo);
            this.grpBottom.Controls.Add(this.lblTransDate);
            this.grpBottom.Controls.Add(this.lblCashierName);
            this.grpBottom.Controls.Add(this.lblCompanyName);
            this.grpBottom.Location = new System.Drawing.Point(-1, 681);
            this.grpBottom.Name = "grpBottom";
            this.grpBottom.Size = new System.Drawing.Size(1017, 34);
            this.grpBottom.TabIndex = 76;
            this.grpBottom.TabStop = false;
            // 
            // lblAgent
            // 
            this.lblAgent.BackColor = System.Drawing.Color.DimGray;
            this.lblAgent.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblAgent.Font = new System.Drawing.Font("Tahoma", 6.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAgent.ForeColor = System.Drawing.Color.White;
            this.lblAgent.Location = new System.Drawing.Point(530, 9);
            this.lblAgent.Name = "lblAgent";
            this.lblAgent.Size = new System.Drawing.Size(166, 22);
            this.lblAgent.TabIndex = 68;
            this.lblAgent.Text = "  Agent";
            this.lblAgent.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblAgent.Click += new System.EventHandler(this.lblAgent_Click);
            // 
            // lblCashier
            // 
            this.lblCashier.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblCashier.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCashier.Location = new System.Drawing.Point(320, 9);
            this.lblCashier.Name = "lblCashier";
            this.lblCashier.Size = new System.Drawing.Size(210, 22);
            this.lblCashier.TabIndex = 67;
            this.lblCashier.Text = "Administrator";
            this.lblCashier.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblCashier.Click += new System.EventHandler(this.lblCashier_Click);
            // 
            // lblTerminalNoName
            // 
            this.lblTerminalNoName.BackColor = System.Drawing.Color.DimGray;
            this.lblTerminalNoName.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblTerminalNoName.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTerminalNoName.ForeColor = System.Drawing.Color.White;
            this.lblTerminalNoName.Location = new System.Drawing.Point(696, 9);
            this.lblTerminalNoName.Name = "lblTerminalNoName";
            this.lblTerminalNoName.Size = new System.Drawing.Size(96, 22);
            this.lblTerminalNoName.TabIndex = 66;
            this.lblTerminalNoName.Text = "  Terminal No.:";
            this.lblTerminalNoName.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblTerminalNo
            // 
            this.lblTerminalNo.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblTerminalNo.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTerminalNo.Location = new System.Drawing.Point(792, 9);
            this.lblTerminalNo.Name = "lblTerminalNo";
            this.lblTerminalNo.Size = new System.Drawing.Size(67, 22);
            this.lblTerminalNo.TabIndex = 65;
            this.lblTerminalNo.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblTerminalNo.Click += new System.EventHandler(this.lblTerminalNo_Click);
            // 
            // lblTransDate
            // 
            this.lblTransDate.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblTransDate.Location = new System.Drawing.Point(859, 9);
            this.lblTransDate.Name = "lblTransDate";
            this.lblTransDate.Size = new System.Drawing.Size(155, 22);
            this.lblTransDate.TabIndex = 64;
            this.lblTransDate.Text = "Jan. 01, 0001 12:00:00 AM";
            this.lblTransDate.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblCashierName
            // 
            this.lblCashierName.BackColor = System.Drawing.Color.DimGray;
            this.lblCashierName.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblCashierName.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCashierName.ForeColor = System.Drawing.Color.White;
            this.lblCashierName.Location = new System.Drawing.Point(237, 9);
            this.lblCashierName.Name = "lblCashierName";
            this.lblCashierName.Size = new System.Drawing.Size(82, 22);
            this.lblCashierName.TabIndex = 63;
            this.lblCashierName.Text = "  Cashier : ";
            this.lblCashierName.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblCompanyName
            // 
            this.lblCompanyName.BackColor = System.Drawing.Color.SlateGray;
            this.lblCompanyName.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblCompanyName.Font = new System.Drawing.Font("Tahoma", 6.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCompanyName.ForeColor = System.Drawing.Color.White;
            this.lblCompanyName.Location = new System.Drawing.Point(2, 9);
            this.lblCompanyName.Name = "lblCompanyName";
            this.lblCompanyName.Size = new System.Drawing.Size(234, 22);
            this.lblCompanyName.TabIndex = 62;
            this.lblCompanyName.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblCompanyName.Click += new System.EventHandler(this.lblCompanyName_Click);
            // 
            // grptxtBarcode
            // 
            this.grptxtBarcode.BackColor = System.Drawing.Color.White;
            this.grptxtBarcode.Controls.Add(this.txtBarCode);
            this.grptxtBarcode.Location = new System.Drawing.Point(0, 532);
            this.grptxtBarcode.Name = "grptxtBarcode";
            this.grptxtBarcode.Size = new System.Drawing.Size(794, 40);
            this.grptxtBarcode.TabIndex = 77;
            this.grptxtBarcode.TabStop = false;
            // 
            // txtBarCode
            // 
            this.txtBarCode.BackColor = System.Drawing.Color.White;
            this.txtBarCode.Enabled = false;
            this.txtBarCode.Font = new System.Drawing.Font("Tahoma", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtBarCode.ForeColor = System.Drawing.Color.Black;
            this.txtBarCode.Location = new System.Drawing.Point(0, 9);
            this.txtBarCode.Name = "txtBarCode";
            this.txtBarCode.Size = new System.Drawing.Size(791, 30);
            this.txtBarCode.TabIndex = 81;
            // 
            // grpMarquee
            // 
            this.grpMarquee.Controls.Add(this.lblMessage);
            this.grpMarquee.Location = new System.Drawing.Point(-1, 653);
            this.grpMarquee.Name = "grpMarquee";
            this.grpMarquee.Size = new System.Drawing.Size(1017, 36);
            this.grpMarquee.TabIndex = 82;
            this.grpMarquee.TabStop = false;
            // 
            // lblMessage
            // 
            this.lblMessage.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblMessage.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMessage.ForeColor = System.Drawing.Color.Red;
            this.lblMessage.Location = new System.Drawing.Point(2, 10);
            this.lblMessage.Name = "lblMessage";
            this.lblMessage.Size = new System.Drawing.Size(1012, 24);
            this.lblMessage.TabIndex = 80;
            this.lblMessage.Text = " Your suggestive selling message and/or description.  Your suggestive selling mes" +
                "sage and/or    .dasdasdas";
            this.lblMessage.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblAgentPositionDepartment
            // 
            this.lblAgentPositionDepartment.AutoSize = true;
            this.lblAgentPositionDepartment.ForeColor = System.Drawing.Color.CornflowerBlue;
            this.lblAgentPositionDepartment.Location = new System.Drawing.Point(834, 203);
            this.lblAgentPositionDepartment.Name = "lblAgentPositionDepartment";
            this.lblAgentPositionDepartment.Size = new System.Drawing.Size(140, 13);
            this.lblAgentPositionDepartment.TabIndex = 69;
            this.lblAgentPositionDepartment.Text = "lblAgentPositionDepartment";
            this.lblAgentPositionDepartment.Visible = false;
            // 
            // tmr
            // 
            this.tmr.Interval = 1;
            this.tmr.Tick += new System.EventHandler(this.tmr_Tick);
            // 
            // tmrRLC
            // 
            this.tmrRLC.Interval = 1;
            this.tmrRLC.Tick += new System.EventHandler(this.tmrRLC_Tick);
            // 
            // grpRLC
            // 
            this.grpRLC.BackColor = System.Drawing.Color.White;
            this.grpRLC.Controls.Add(this.cmdRLCClose);
            this.grpRLC.Controls.Add(this.lblMallForwarderStatus);
            this.grpRLC.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grpRLC.ForeColor = System.Drawing.Color.Blue;
            this.grpRLC.Location = new System.Drawing.Point(2, 504);
            this.grpRLC.Name = "grpRLC";
            this.grpRLC.Size = new System.Drawing.Size(789, 33);
            this.grpRLC.TabIndex = 97;
            this.grpRLC.TabStop = false;
            // 
            // cmdRLCClose
            // 
            this.cmdRLCClose.AutoSize = true;
            this.cmdRLCClose.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.cmdRLCClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmdRLCClose.Font = new System.Drawing.Font("Times New Roman", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdRLCClose.ForeColor = System.Drawing.Color.White;
            this.cmdRLCClose.Image = global::AceSoft.RetailPlus.Client.Properties.Resources.close;
            this.cmdRLCClose.Location = new System.Drawing.Point(745, -5);
            this.cmdRLCClose.Name = "cmdRLCClose";
            this.cmdRLCClose.Size = new System.Drawing.Size(40, 40);
            this.cmdRLCClose.TabIndex = 92;
            this.cmdRLCClose.UseVisualStyleBackColor = true;
            this.cmdRLCClose.Click += new System.EventHandler(this.cmdRLCClose_Click);
            // 
            // lblMallForwarderStatus
            // 
            this.lblMallForwarderStatus.AutoSize = true;
            this.lblMallForwarderStatus.BackColor = System.Drawing.Color.Transparent;
            this.lblMallForwarderStatus.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMallForwarderStatus.ForeColor = System.Drawing.Color.Green;
            this.lblMallForwarderStatus.Location = new System.Drawing.Point(4, 12);
            this.lblMallForwarderStatus.Name = "lblMallForwarderStatus";
            this.lblMallForwarderStatus.Size = new System.Drawing.Size(302, 16);
            this.lblMallForwarderStatus.TabIndex = 91;
            this.lblMallForwarderStatus.Text = "RLC Notification: Trying to send unsent files...";
            // 
            // imgCompanyLogo
            // 
            this.imgCompanyLogo.BackColor = System.Drawing.Color.White;
            this.imgCompanyLogo.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.imgCompanyLogo.Location = new System.Drawing.Point(791, 28);
            this.imgCompanyLogo.Name = "imgCompanyLogo";
            this.imgCompanyLogo.Size = new System.Drawing.Size(224, 225);
            this.imgCompanyLogo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.imgCompanyLogo.TabIndex = 81;
            this.imgCompanyLogo.TabStop = false;
            // 
            // panSubTotal
            // 
            this.panSubTotal.BackColor = System.Drawing.Color.Maroon;
            this.panSubTotal.Controls.Add(this.lblSubtotalName);
            this.panSubTotal.Controls.Add(this.lblSubTotal);
            this.panSubTotal.Controls.Add(this.lblTransDiscount);
            this.panSubTotal.Controls.Add(this.lblTransCharge);
            this.panSubTotal.Controls.Add(this.lblCurrency);
            this.panSubTotal.Controls.Add(this.lblOrderType);
            this.panSubTotal.Location = new System.Drawing.Point(1, 566);
            this.panSubTotal.Name = "panSubTotal";
            this.panSubTotal.Size = new System.Drawing.Size(790, 94);
            this.panSubTotal.TabIndex = 130;
            this.panSubTotal.TabStop = false;
            // 
            // lblSubtotalName
            // 
            this.lblSubtotalName.AutoSize = true;
            this.lblSubtotalName.BackColor = System.Drawing.Color.Transparent;
            this.lblSubtotalName.Font = new System.Drawing.Font("Courier New", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSubtotalName.ForeColor = System.Drawing.Color.White;
            this.lblSubtotalName.Location = new System.Drawing.Point(106, 10);
            this.lblSubtotalName.Name = "lblSubtotalName";
            this.lblSubtotalName.Size = new System.Drawing.Size(218, 23);
            this.lblSubtotalName.TabIndex = 1;
            this.lblSubtotalName.Text = "SUBTOTAL: REFUND";
            // 
            // lblSubTotal
            // 
            this.lblSubTotal.BackColor = System.Drawing.Color.Transparent;
            this.lblSubTotal.Font = new System.Drawing.Font("Arial", 26.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSubTotal.ForeColor = System.Drawing.Color.White;
            this.lblSubTotal.Location = new System.Drawing.Point(357, 33);
            this.lblSubTotal.Name = "lblSubTotal";
            this.lblSubTotal.Size = new System.Drawing.Size(325, 38);
            this.lblSubTotal.TabIndex = 63;
            this.lblSubTotal.Text = "0.0000000000";
            this.lblSubTotal.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblTransDiscount
            // 
            this.lblTransDiscount.BackColor = System.Drawing.Color.Transparent;
            this.lblTransDiscount.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTransDiscount.ForeColor = System.Drawing.Color.RoyalBlue;
            this.lblTransDiscount.Location = new System.Drawing.Point(347, 71);
            this.lblTransDiscount.Name = "lblTransDiscount";
            this.lblTransDiscount.Size = new System.Drawing.Size(325, 15);
            this.lblTransDiscount.TabIndex = 72;
            this.lblTransDiscount.Text = "Less 0% / 0.00";
            this.lblTransDiscount.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblTransCharge
            // 
            this.lblTransCharge.BackColor = System.Drawing.Color.Transparent;
            this.lblTransCharge.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTransCharge.ForeColor = System.Drawing.Color.RoyalBlue;
            this.lblTransCharge.Location = new System.Drawing.Point(111, 71);
            this.lblTransCharge.Name = "lblTransCharge";
            this.lblTransCharge.Size = new System.Drawing.Size(229, 15);
            this.lblTransCharge.TabIndex = 3;
            this.lblTransCharge.Text = "Plus 0% / 0.00";
            this.lblTransCharge.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblCurrency
            // 
            this.lblCurrency.BackColor = System.Drawing.Color.Transparent;
            this.lblCurrency.Font = new System.Drawing.Font("Arial Narrow", 26.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCurrency.ForeColor = System.Drawing.Color.White;
            this.lblCurrency.Location = new System.Drawing.Point(104, 33);
            this.lblCurrency.Name = "lblCurrency";
            this.lblCurrency.Size = new System.Drawing.Size(117, 38);
            this.lblCurrency.TabIndex = 64;
            this.lblCurrency.Text = "PHP";
            // 
            // lblOrderType
            // 
            this.lblOrderType.Font = new System.Drawing.Font("Courier New", 15.75F, System.Drawing.FontStyle.Bold);
            this.lblOrderType.ForeColor = System.Drawing.Color.RoyalBlue;
            this.lblOrderType.Location = new System.Drawing.Point(347, 10);
            this.lblOrderType.Name = "lblOrderType";
            this.lblOrderType.Size = new System.Drawing.Size(325, 19);
            this.lblOrderType.TabIndex = 4;
            this.lblOrderType.Text = "DELIVERY";
            this.lblOrderType.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // tmrLogo
            // 
            this.tmrLogo.Enabled = true;
            this.tmrLogo.Interval = 90000;
            this.tmrLogo.Tick += new System.EventHandler(this.tmrLogo_Tick);
            // 
            // MainWnd
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 14);
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(1017, 714);
            this.Controls.Add(this.grpRLC);
            this.Controls.Add(this.grpItems);
            this.Controls.Add(this.panLocked);
            this.Controls.Add(this.lblAgentPositionDepartment);
            this.Controls.Add(this.dgItems);
            this.Controls.Add(this.grptxtBarcode);
            this.Controls.Add(this.imgCompanyLogo);
            this.Controls.Add(this.panSubTotal);
            this.Controls.Add(this.grpTop);
            this.Controls.Add(this.grpMarquee);
            this.Controls.Add(this.grpBottom);
            this.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.Name = "MainWnd";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = " RetailPlus ™";
            this.Activated += new System.EventHandler(this.MainWnd_Activated);
            this.Closing += new System.ComponentModel.CancelEventHandler(this.MainWnd_Closing);
            this.Load += new System.EventHandler(this.MainWnd_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.MainWnd_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.dgItems)).EndInit();
            this.grpItems.ResumeLayout(false);
            this.grpItems.PerformLayout();
            this.panLocked.ResumeLayout(false);
            this.panLocked.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.imgIcon)).EndInit();
            this.grpTop.ResumeLayout(false);
            this.grpBottom.ResumeLayout(false);
            this.grptxtBarcode.ResumeLayout(false);
            this.grptxtBarcode.PerformLayout();
            this.grpMarquee.ResumeLayout(false);
            this.grpRLC.ResumeLayout(false);
            this.grpRLC.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.imgCompanyLogo)).EndInit();
            this.panSubTotal.ResumeLayout(false);
            this.panSubTotal.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

		}
		#endregion

		#endregion

		#region Locking and Unlocking the window, Enable/Disable Command Controls

		public void Lock()
		{
			try
			{
				clsEvent.AddEvent("[" + lblCashier.Text + "] Locking client application.");

				this.lblTransDate.Text = DateTime.Now.ToString("MMM. dd, yyyy hh:mm:ss tt");
				this.lblCashier.Tag = "";
				this.lblCashier.Text = "";
                this.mCashierName = "";

				this.lblCustomer.Tag = Constants.C_RETAILPLUS_CUSTOMERID.ToString();
				this.lblCustomer.Text = Constants.C_RETAILPLUS_CUSTOMER;
				this.lblAgent.Tag = Constants.C_RETAILPLUS_AGENTID.ToString();
				this.lblAgent.Text = Constants.C_RETAILPLUS_AGENT;
				this.lblAgentPositionDepartment.Text = Constants.C_RETAILPLUS_AGENT_POSITIONNAME;
				this.lblAgentPositionDepartment.Tag = Constants.C_RETAILPLUS_AGENT_DEPARTMENT_NAME;
				this.grpItems.Tag = Constants.C_RETAILPLUS_WAITERID.ToString();
				this.grpItems.Text = "Served by: " + Constants.C_RETAILPLUS_WAITER;

				this.mboLocked = true;
				this.panLocked.Visible = true;
				this.txtBarCode.Text = "";
				this.txtBarCode.Enabled = false;

				this.Focus();

				InsertAuditLog(AccessTypes.LockTerminal, "Lock Terminal #: " + mclsTerminalDetails.TerminalNo + " @ Branch: " + mclsTerminalDetails.BranchDetails.BranchCode);
				clsEvent.AddEventLn("Done!");
			}
			catch (Exception ex)
            { 
                InsertErrorLogToFile(ex, "ERROR!!! Locking window."); 
            }
		}
        public void UnLock(long UserID)
		{
            
            try
            {
                AccessUser clsUser = new AccessUser(mConnection, mTransaction);
                mConnection = clsUser.Connection; mTransaction = clsUser.Transaction;

                AccessUserDetails details = clsUser.Details(UserID);

                clsEvent.AddEvent("[" + details.Name + "] UnLocking client application.");

                this.lblTransDate.Text = DateTime.Now.ToString("MMM. dd, yyyy hh:mm:ss tt");
                this.lblCashier.Tag = details.UID;
                this.lblCashier.Text = details.Name;
                this.mCashierName = details.Name;

                this.mboLocked = false;
                this.panLocked.Visible = false;
                this.txtBarCode.Text = "";
                this.txtBarCode.Enabled = true;
                this.txtBarCode.Focus();

                mclsSalesTransactionDetails.CashierID = details.UID;
                mclsSalesTransactionDetails.CashierName = details.Name;

                InsertAuditLog(AccessTypes.UnlockTerminal, "Unlock terminal #: " + mclsTerminalDetails.TerminalNo + " @ Branch: " + mclsTerminalDetails.BranchDetails.BranchCode);
                
                clsUser.CommitAndDispose();

                clsEvent.AddEventLn("Done!");
            }
            catch (Exception ex)
            {
                InsertErrorLogToFile(ex, "ERROR!!! Unlocking window.");
            }
		}

		#endregion

		#region Window Form Methods

		private void MainWnd_Activated(object sender, System.EventArgs e)
		{
			txtBarCode.Focus();
		}
		private void MainWnd_Load(object sender, System.EventArgs e)
		{
			grpRLC.Visible = false;
			try
			{ this.imgIcon.Image = new Bitmap(Application.StartupPath + "/images/MainIcon.jpg"); }
			catch { }
			try
			{ this.imgCompanyLogo.Image = new Bitmap(Application.StartupPath + "/images/CompanyLogo.jpg"); }
			catch { }
			try
			{ this.cmdRLCClose.Image = new Bitmap(Application.StartupPath + "/images/close.gif"); }
			catch { }

            Data.SysConfig clsSysConfig = new Data.SysConfig(mConnection, mTransaction);
            mConnection = clsSysConfig.Connection; mTransaction = clsSysConfig.Transaction;

            try
            {
                mclsSysConfigDetails = clsSysConfig.get_SysConfigDetails();
            }
            catch (Exception ex)
            {
                clsEvent.AddErrorEventLn(ex);
            }
            clsSysConfig.CommitAndDispose();

			this.LoadOptions();

			txtBarCode.Focus();
			lblTerminalNo.Text = mclsTerminalDetails.TerminalNo;
			lblCompanyName.Text = CompanyDetails.CompanyName;

            // Added June 30, 2013 handle if RetailPlus or Parking System
            this.Text = mclsTerminalDetails.IsParkingTerminal ? "RetailPlus™ Parking Terminal" : "RetailPlus™ POS Terminal"; 

			if (mclsTerminalDetails.FORM_Behavior == FORM_Behavior.MODAL)
			{
				TaskBarWnd taskbar = new TaskBarWnd();
				taskbar.Hide();

				Screen Srn = Screen.PrimaryScreen;
				tempHeight = Srn.Bounds.Height;
				tempWidth = Srn.Bounds.Width;

				Resolution ChangeRes = new Resolution(1024, 768);

				Hook = new KeyBoardHook.KeyBoardHook();
				Hook.KeyDown += new KeyEventHandler(MyKeyDown);

				this.FormBorderStyle = FormBorderStyle.FixedDialog;
				this.ControlBox = false;
				this.TopMost = true;
				this.Text = "";
				this.Height = 768;
				this.Width = 1024;
				this.Location = new Point(-1, -1);
				this.ShowInTaskbar = false;
				Cursor.Hide();
			}
			else
			{
				Screen Srn = Screen.PrimaryScreen;
				if (Srn.Bounds.Height <= 768)
				{
					this.FormBorderStyle = FormBorderStyle.FixedDialog;
					this.ControlBox = false;
					this.TopMost = true;
					this.Text = "";
					this.Height = 768;
					this.Width = 1024;
					this.Location = new Point(-1, -1);
					this.ShowInTaskbar = false;
				}
			}

			IsStartCutOffTimeOK();

			if (CONFIG.MallCode.ToUpper() == MallCodes.RLC)
				tmrRLC.Enabled = true;
		}
		private void MainWnd_Closing(object sender, CancelEventArgs e)
		{
			try
			{
				e.Cancel = true;
				base.OnClosing(e);
			}
			catch (Exception ex)
			{
                InsertErrorLogToFile(ex, "ERROR!!! Closing Main window. TRACE: ");
			}
		}
		private void MainWnd_KeyDown(object sender, KeyEventArgs e)
		{
			try
			{
				if (e.KeyData == Keys.F1)
				{
                    ShowHelp(); 
                }
				else if (mboLocked && e.KeyData == Keys.F2)
				{ this.PriceInquiry(); }
				else if (e.KeyData == Keys.Escape)
				{
					if (txtBarCode.Text != string.Empty)
					{ txtBarCode.Text = string.Empty; txtBarCode.Focus(); }
					else
					{ this.Exit(); }
				}
				else if (mboLocked)
				{
					if (Control.ModifierKeys == Keys.Control && e.KeyCode == Keys.L)
						LoggedOutCashier(true);
					else if (e.KeyData == Keys.Enter)
						this.DoLogin();
				}
				else if (!mboLocked)
				{
					if (txtBarCode.Text != string.Empty && e.KeyCode != Keys.Enter) return;
					if (Control.ModifierKeys == Keys.Control)
					{
						switch (e.KeyCode)
						{
							case Keys.C:
								grpRLC.Visible = false;
								break;

							case Keys.H:
								DialogResult loginresult = GetWriteAccessAndLogin(mclsSalesTransactionDetails.CashierID, AccessTypes.PrintTransactionHeader);

                                if (loginresult == DialogResult.OK)
								{
									PrintReportHeaderSection(true, DateTime.MinValue);
									MessageBox.Show("Transaction header has been printed.", "RetailPlus", MessageBoxButtons.OK, MessageBoxIcon.Information);
								}
								break;

							case Keys.E:
								ReleaseItems();
								break;

							case Keys.L:
								LoggedOutCashier(true);
								break;

							case Keys.O:
								OpenTransactionDrawer();
								break;

							case Keys.P:
								PrintCheckOutBill();
								break;

							case Keys.R:
								RefundTransaction();
								break;

							case Keys.S:
								PrintOrderSlip(false); 
								break;

                            case Keys.T:
                                UpdateIsConsignment();
                                lblConsignment.Visible = mclsSalesTransactionDetails.isConsignment;
                                break;

							case Keys.U:
								PackTransaction();
								break;

							case Keys.Insert:
								Float();
								break;

                            case Keys.F5:
                                UpdateContact();
                                break;

							case Keys.F6:
								SelectContact(AceSoft.RetailPlus.Data.ContactGroupCategory.AGENT);
								break;

							case Keys.Enter:
								if (!mboIsInTransaction)
								{
									if (MessageBox.Show("Are you sure you want to reload the default options?", "RetailPlus", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
									{
										this.LoadOptions();
										MessageBox.Show("Done!", "RetailPlus", MessageBoxButtons.OK, MessageBoxIcon.Information);
									}
								}
								break;

							case Keys.F7:
								IssueCreditCard();
								break;

							case Keys.F8:
								RenewCreditCard();
								break;

							case Keys.F9:
								CreditCardReplacement(CreditCardStatus.Replaced_Lost);
								break;

							case Keys.F10:
								CreditCardReplacement(CreditCardStatus.Replaced_Expired);
								break;

							case Keys.F11:
								CreditCardDeclareAsLost();
								break;

							case Keys.F12:
								CreditCardReactivate();
								break;
						}
					}
					else if (Control.ModifierKeys == Keys.Shift)
					{
						switch (e.KeyCode)
						{
                            case Keys.F1:
                                ShowMallForwarder();
                                break;

							case Keys.F2:
								ChangePrice();
								break;

							case Keys.F4:
								ApplyItemDiscountForAllItem();
								break;

							case Keys.F5:
								ApplyTransDiscount();
								break;

							case Keys.F6:
								SelectWaiter();
								break;

							case Keys.F10:
								PaidOut();
								break;

							case Keys.F11:
								Deposit();
								break;

							case Keys.F12:
								ReprintTransaction();
								break;

							case Keys.Enter:
                                EnterCreditItemizePayment();
								break;

                            case Keys.Delete:
                                InitializeZRead(true);
                                break;
						}
					}
					else if (Control.ModifierKeys == Keys.Alt)
					{
						switch (e.KeyCode)
						{
                            case Keys.F1:
                                string strFileName = Application.ExecutablePath.ToUpper().Replace("RETAILPLUS.EXE","") + "print.prn";
                                RawPrinterHelper.SendFileToPrinter(mclsTerminalDetails.PrinterName, strFileName, "RetailPlus " + "print.prn");
                                CutPrinterPaper();
                                MessageBox.Show("Done printing 'print.prn'", "RetailPlus", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                break;

                            case Keys.Enter:
                                VerifyCredit();
                                break;

							case Keys.S:
								/**********************
								 * December 18, 2008
								 * Added to reprint all items that are already printed
								 * *******************/
								PrintOrderSlip(true);
								break;

							case Keys.U:
								UnPackTransaction();
								break;

							case Keys.F2:
								ChangeAmount();
								break;

							case Keys.F5:
								ApplyTransCharge(); //ChargeTypes.Percentage
								break;

							case Keys.F6:
								ApplyTransCharge(); //ChargeTypes.FixedValue
								break;

							case Keys.F7:
								IssueRewardCard();
								break;

							case Keys.F8:
								RenewRewardCard();
								break;

							case Keys.F9:
								RewardCardReplacement(RewardCardStatus.Replaced_Lost);
								break;

							case Keys.F10:
								RewardCardReplacement(RewardCardStatus.Replaced_Expired);
								break;

							case Keys.F11:
								RewardCardDeclareAsLost();
								break;

							case Keys.F12:
								RewardCardReactivate();
								break;

							//case Keys.Enter:
							//    mclsTerminalDetails.TrustFund = 0;
							//    MessageBox.Show("Done!", "RetailPlus", MessageBoxButtons.OK, MessageBoxIcon.Information);
							//    break;
							
						}
					}
                    else if (Control.ModifierKeys == Keys.Control && Control.ModifierKeys == Keys.Shift)
                    {
                        switch (e.KeyCode)
                        {
                            case Keys.F6:
                                UpdateContact();
                                break;
                        }
                    }
                    else
                    {
                        switch (e.KeyData)
                        {
                            case Keys.Up:
                                MoveItemUp();
                                break;

                            case Keys.Down:
                                MoveItemDown();
                                break;

                            case Keys.Escape:
                                this.Exit();
                                break;

                            case Keys.F1:
                                ShowHelp();
                                break;

                            case Keys.F2:
                                ChangeQuantity();
                                break;

                            case Keys.F3:
                                ReturnItem();
                                break;

                            case Keys.F4:
                                ApplyItemDiscount();
                                break;

                            case Keys.F5:
                                ApplyTransDiscount();
                                break;

                            case Keys.F6:
                                SelectContact(AceSoft.RetailPlus.Data.ContactGroupCategory.CUSTOMER);
                                break;

                            case Keys.F7:
                                SuspendTransaction();
                                break;

                            case Keys.F8:
                                ResumeTransaction();
                                break;

                            case Keys.F9:
                                VoidTransaction();
                                break;

                            case Keys.F10:
                                Disburse();
                                break;

                            case Keys.F11:
                                WithHold();
                                break;

                            case Keys.F12:
                                ShowPrintWindow();
                                break;

                            case Keys.Enter:
                                if (txtBarCode.Text.Trim() != "" && txtBarCode.Text.Trim() != null)
                                    if (txtBarCode.Text.Contains(Constants.SWIPE_REWARD_CARD))
                                    {
                                        clsEvent.AddEventLn("[" + lblCashier.Text + "] Selecting customer.", true);
                                        LoadContact(Data.ContactGroupCategory.CUSTOMER, new Data.ContactDetails());
                                    }
                                    else
                                        ReadBarCode();
                                else if (lblCustomer.Text.Trim().ToUpper() == Constants.C_RETAILPLUS_ORDER_SLIP_CUSTOMER)
                                    CloseTransactionAsOrderSlip();
                                else
                                    CloseTransaction();
                                break;

                            case Keys.Back:
                                if (txtBarCode.Text.Trim() == "")
                                    VoidItem();
                                break;

                            case Keys.Insert:
                                {
                                    if (mclsTerminalDetails.CashCountBeforeReport)
                                    {
                                        if (!mboIsCashCountInitialized)
                                            CashCount();
                                        else
                                            MessageBox.Show("Sorry, cash count has been already initialized for the day. You can only initialize cash count once a day.", "RetailPlus", MessageBoxButtons.OK, MessageBoxIcon.Stop);

                                    }
                                    else { CashCount(); }
                                    break;
                                }

                            case Keys.Delete:
                                InitializeZRead(false);
                                break;

                            case Keys.PageDown:
                                SelectProduct(false);
                                break;

                            case Keys.Right:
                                SelectProduct(false); // 13May2013 LEAceron: by right, price should be visible.
                                break;

                            case Keys.PageUp:
                                SelectProduct(true);
                                break;

                            case Keys.Left:
                                PriceInquiry();
                                break;
                        }
                    }
				}
			}
			catch (Exception ex)
			{
                InsertErrorLogToFile(ex, "ERROR!!! Processing event.");
			}
		}
		public void MyKeyDown(object sender, KeyEventArgs e)
		{
			//Console.WriteLine("KeyDown 	- " + e.KeyData.ToString());
		}

		#endregion

		#region Window Control Methods

		private void txtBarcode_GotFocus(object sender, System.EventArgs e)
		{
			txtBarCode.SelectAll();
		}
		private void lblCashier_Click(object sender, EventArgs e)
		{
			LoggedOutCashier(true);
		}
		private void lblCompanyName_Click(object sender, EventArgs e)
		{
			this.Exit();
		}
		private void lblTerminalNo_Click(object sender, EventArgs e)
		{
			this.Exit();
		}
		private void lblSubtotalName_Click(object sender, EventArgs e)
		{
			KeyEventArgs enter = new KeyEventArgs(Keys.Enter);
			MainWnd_KeyDown(null, enter);
			txtBarCode.Focus();
		}
		private void lblCurrency_Click(object sender, EventArgs e)
		{
			KeyEventArgs enter = new KeyEventArgs(Keys.Enter);
			MainWnd_KeyDown(null, enter);
			txtBarCode.Focus();
		}
		private void lblSubTotal_Click(object sender, EventArgs e)
		{
			KeyEventArgs enter = new KeyEventArgs(Keys.Enter);
			MainWnd_KeyDown(null, enter);
			txtBarCode.Focus();
		}
		private void tmr_Tick(object sender, EventArgs e)
		{
			mdtCurrentDateTime.AddMilliseconds(1);
		}
		private void panLocked_Click(object sender, EventArgs e)
		{
			if (mboLocked)
				this.DoLogin();
		}
		private void imgIcon_Click(object sender, EventArgs e)
		{
			if (mboLocked)
				this.DoLogin();
		}
		private void lblThisStation_Click(object sender, EventArgs e)
		{
			if (mboLocked)
				this.DoLogin();
		}

		// [1/31] LEA Remove - redundant for keyboard based POS
		//private void cmdBackSpace_Click(object sender, EventArgs e)
		//{
		//    cmdNoClick("{BACKSPACE}");
		//}
		private void lblOrderType_Click(object sender, EventArgs e)
		{
			ChangeOrderType();
		}
		private void tmrRLC_Tick(object sender, EventArgs e)
		{
			grpRLC.Visible = true;
			tmrRLC.Interval = CONFIG.FTPAutoResendInterval;
			SendRLCDelegate delSendRLCDelegate = new SendRLCDelegate(SendRLC);
			delSendRLCDelegate.BeginInvoke(null, null);
		}
		private void cmdRLCClose_Click(object sender, EventArgs e)
		{
			grpRLC.Visible = false;
		}
		private void lblCustomer_Click(object sender, System.EventArgs e)
		{
			SelectContact(AceSoft.RetailPlus.Data.ContactGroupCategory.CUSTOMER);
		}
		private void lblAgent_Click(object sender, EventArgs e)
		{
			SelectContact(AceSoft.RetailPlus.Data.ContactGroupCategory.AGENT);
		}
		private void tmrLogo_Tick(object sender, EventArgs e)
		{
			string strCompanyTag = "CompanyLogo";
			try
			{ strCompanyTag = tmrLogo.Tag.ToString(); }
			catch { }
			if (strCompanyTag == "CompanyLogo")
			{
				try
				{ this.imgCompanyLogo.Image = new Bitmap(Application.StartupPath + "/images/CompanyLogo.jpg"); tmrLogo.Tag = "CompanyLogo"; }
				catch
				{
					try
					{ this.imgCompanyLogo.Image = new Bitmap(Application.StartupPath + "/images/CompanyLogoRBS.jpg"); tmrLogo.Tag = "CompanyLogoRBS"; }
					catch { }
				}
			}
			else
			{
				try
				{ this.imgCompanyLogo.Image = new Bitmap(Application.StartupPath + "/images/CompanyLogoRBS.jpg"); tmrLogo.Tag = "CompanyLogoRBS"; }
				catch
				{
					try
					{ this.imgCompanyLogo.Image = new Bitmap(Application.StartupPath + "/images/CompanyLogo.jpg"); tmrLogo.Tag = "CompanyLogo"; }
					catch { }
				}
			}
		}

		#endregion

		#region Overloads

		private void LoadOptions()
		{
			try
			{
                clsEvent.AddEvent("Loading transaction defaults...");

                Cursor.Current = Cursors.WaitCursor;

                this.KeyPreview = true;

                lblCurrency.Text = CompanyDetails.Currency;
				lblTransNo.Text = "READY...";
				lblCustomer.Text = Constants.C_RETAILPLUS_CUSTOMER;
				lblCustomer.Tag = Constants.C_RETAILPLUS_CUSTOMERID.ToString();
				lblAgent.Text = Constants.C_RETAILPLUS_AGENT;
				lblAgent.Tag = Constants.C_RETAILPLUS_AGENTID.ToString();
				lblAgentPositionDepartment.Text = Constants.C_RETAILPLUS_AGENT_POSITIONNAME;
				lblAgentPositionDepartment.Tag = Constants.C_RETAILPLUS_AGENT_DEPARTMENT_NAME;
				grpItems.Text = "Served by: " + Constants.C_RETAILPLUS_WAITER;
				grpItems.Tag = Constants.C_RETAILPLUS_WAITERID.ToString();
				lblDescription.Text = "Description";
				lblCategory.Text = "Category";
				lblProperties.Text = "Property";
				lblTransDate.Text = DateTime.Now.ToString("MMM. dd, yyyy hh:mm:ss tt");
				lblSubTotal.Text = "0.00";
				lblTransDiscount.Text = "Less 0% / 0.00";
				lblTransDiscount.Tag = DiscountTypes.NotApplicable.ToString("d");

				if (mclsTerminalDetails.WithRestaurantFeatures)
				{ lblSubtotalName.Text = "SUBTOTAL:"; lblOrderType.Visible = true; lblOrderType.Text = OrderTypes.DineIn.ToString("G").ToUpper();}
				else
				{ lblSubtotalName.Text = "SUBTOTAL"; lblOrderType.Visible = false; lblOrderType.Text = OrderTypes.DineIn.ToString("G").ToUpper(); }

				lblMessage.Text = " Your suggestive selling message and/or description";
				lblTransCharge.Text = lblTransCharge.Text = "Plus 0% / 0.00";
				txtBarCode.Text = "";
                lblConsignment.Visible = false;

				mboIsRefund = false;
                mboDoNotPrintTransactionDate = false;
				//mboIsDiscountAuthorized = false;

				mclsSalesTransactionDetails = new Data.SalesTransactionDetails();
				try { mclsSalesTransactionDetails.CashierID = Convert.ToInt64(lblCashier.Tag); }
				catch { }
				mclsSalesTransactionDetails.CashierName = lblCashier.Text;

                Data.Terminal clsTerminal = new Data.Terminal(mConnection, mTransaction);
                mConnection = clsTerminal.Connection; mTransaction = clsTerminal.Transaction;

				mclsTerminalDetails = clsTerminal.Details(Constants.TerminalBranchID, CompanyDetails.TerminalNo);

				Data.Contacts clsContact = new Data.Contacts(mConnection, mTransaction);
                mConnection = clsContact.Connection; mTransaction = clsContact.Transaction;

				mclsContactDetails = clsContact.Details(Constants.C_RETAILPLUS_CUSTOMERID);

				// Sep 24, 2011      Lemuel E. Aceron
				// Added order slip wherein all punch items will not change sales and inventory
				// Override the reserved and commit if order slip
				// a customer named ORDER SLIP should be defined in contacts
                //if (lblCustomer.Text.Trim().ToUpper() == Constants.C_RETAILPLUS_ORDER_SLIP_CUSTOMER)
                //{ mclsTerminalDetails.ReservedAndCommit = false; }

				// Dec 01, 2008      Lemuel E. Aceron
				// added the IsCashCountInitialized for 1 time 
				// Cash count every printing of report.
				if (mclsTerminalDetails.CashCountBeforeReport)
					mboIsCashCountInitialized = clsTerminal.IsCashCountInitialized(mclsTerminalDetails.BranchDetails.BranchID, mclsTerminalDetails.TerminalNo, mclsSalesTransactionDetails.CashierID);

				clsTerminal.CommitAndDispose();

				ItemDataTable = new System.Data.DataTable("tblProducts");
				ItemDataTable.Columns.Add("TransactionItemsID");
				ItemDataTable.Columns.Add("ItemNo");
				ItemDataTable.Columns.Add("ProductID");
				ItemDataTable.Columns.Add("ProductCode");
				ItemDataTable.Columns.Add("BarCode");
				ItemDataTable.Columns.Add("Description");
				ItemDataTable.Columns.Add("ProductUnitID");
				ItemDataTable.Columns.Add("ProductUnitCode");
				ItemDataTable.Columns.Add("Quantity");
				ItemDataTable.Columns.Add("Price");
				ItemDataTable.Columns.Add("Discount");
				ItemDataTable.Columns.Add("ItemDiscount");
				ItemDataTable.Columns.Add("ItemDiscountType");
				ItemDataTable.Columns.Add("Amount");
				ItemDataTable.Columns.Add("VAT");
				ItemDataTable.Columns.Add("EVAT");
				ItemDataTable.Columns.Add("LocalTax");
				ItemDataTable.Columns.Add("VariationsMatrixID");
				ItemDataTable.Columns.Add("MatrixDescription");
				ItemDataTable.Columns.Add("ProductGroup");
				ItemDataTable.Columns.Add("ProductSubGroup");
				ItemDataTable.Columns.Add("TransactionItemStat");
				ItemDataTable.Columns.Add("DiscountCode");
				ItemDataTable.Columns.Add("DiscountRemarks");
				ItemDataTable.Columns.Add("ProductPackageID");
				ItemDataTable.Columns.Add("MatrixPackageID");
				ItemDataTable.Columns.Add("PackageQuantity");
				ItemDataTable.Columns.Add("PromoQuantity");
				ItemDataTable.Columns.Add("PromoValue");
				ItemDataTable.Columns.Add("PromoInPercent");
				ItemDataTable.Columns.Add("PromoType");
				ItemDataTable.Columns.Add("PromoApplied");
				ItemDataTable.Columns.Add("PurchasePrice");
				ItemDataTable.Columns.Add("PurchaseAmount");
				ItemDataTable.Columns.Add("IncludeInSubtotalDiscount");
				ItemDataTable.Columns.Add("OrderSlipPrinter");
				ItemDataTable.Columns.Add("OrderSlipPrinted");
				ItemDataTable.Columns.Add("PercentageCommision");
				ItemDataTable.Columns.Add("Commision");

                this.dgStyle.MappingName = ItemDataTable.TableName;
				dgItems.DataSource = ItemDataTable;
				SetGridItemsWidth();

				mboIsInTransaction = false;
				mboIsItemHeaderPrinted = false;
				mboCreditCardSwiped = false;
				mboRewardCardSwiped = false;
                mdteOverRidingPrintDate = DateTime.MinValue;

				StartMarqueeThread();
				Cursor.Current = Cursors.Default;

                msbToPrint.Clear();
                msbToPrint = new StringBuilder();
                msbEJournalToPrint = new StringBuilder();

				clsEvent.AddEventLn("Done!");

			}
			catch (Exception ex)
            { 
                InsertErrorLogToFile(ex, "ERROR!!! Loading options."); 
            }
		}
		private void SetGridItemsWidth()
		{
			dgStyle.GridColumnStyles["ItemNo"].Width = 65;
			dgStyle.GridColumnStyles["Description"].Width = dgItems.Width - 310;
			dgStyle.GridColumnStyles["Quantity"].Width = 65;
			dgStyle.GridColumnStyles["Price"].Width = 70;
			dgStyle.GridColumnStyles["Amount"].Width = 95;
		}

		#endregion

		#region Commands

		private void MoveItemUp()
		{
			if (ItemDataTable.Rows.Count > 0 && dgItems.CurrentRowIndex != 0)
			{
				int oldindex = dgItems.CurrentRowIndex;
				dgItems.CurrentRowIndex -= 1;
				try { dgItems.UnSelect(0); }
				catch { }
				dgItems.UnSelect(oldindex);
				dgItems.Select(dgItems.CurrentRowIndex);
				SetItemDetails();
				Data.SalesTransactionItemDetails Details = getCurrentRowItemDetails();
				DisplayItemToTurretDelegate DisplayItemToTurretDel = new DisplayItemToTurretDelegate(DisplayItemToTurret);
				DisplayItemToTurretDel.BeginInvoke(Details.Description, Details.ProductUnitCode, Details.Quantity, Details.Price, Details.Discount, Details.PromoApplied, Details.Amount, Details.VAT, Details.EVAT, null, null);
			}
		}
		private void MoveItemDown()
		{
			if (dgItems.CurrentRowIndex + 1 < ItemDataTable.Rows.Count && dgItems.CurrentRowIndex + 1 != ItemDataTable.Rows.Count)
			{
				int oldindex = dgItems.CurrentRowIndex;

				dgItems.CurrentRowIndex += 1;
				try { dgItems.UnSelect(0); }
				catch { }
				dgItems.UnSelect(oldindex);
				dgItems.Select(dgItems.CurrentRowIndex);
				SetItemDetails();
				Data.SalesTransactionItemDetails Details = getCurrentRowItemDetails();
				DisplayItemToTurretDelegate DisplayItemToTurretDel = new DisplayItemToTurretDelegate(DisplayItemToTurret);
				DisplayItemToTurretDel.BeginInvoke(Details.Description, Details.ProductUnitCode, Details.Quantity, Details.Price, Details.Discount, Details.PromoApplied, Details.Amount, Details.VAT, Details.EVAT, null, null);
			}
		}
		private void Exit()
		{
			if (mboIsInTransaction)
			{
				MessageBox.Show("Cannot exit the system current transaction is active. Please SUSPEND or VOID the transaction first.", "RetailPlus", MessageBoxButtons.OK, MessageBoxIcon.Stop);
				return;
			}
			if (MessageBox.Show("Are you sure you want to exit?", "RetailPlus", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
			{
				if (this.LoggedOutCashier(false) == DialogResult.OK)
				{
					try { MarqueeThread.Abort(); }
					catch { }
					Application.Exit();
				}
			}
		}
		private void ShowHelp()
		{
			HelpWnd help = new HelpWnd();

			help.ShowDialog(this);
			help.Close();
			help.Dispose();
		}
		private void PriceInquiry()
		{
            clsEvent.AddEventLn(" Opening Price Inquiry module...", true);

            PriceInquiryWnd clsPriceInquiryWnd = new PriceInquiryWnd();
            clsPriceInquiryWnd.TerminalDetails = mclsTerminalDetails;
            clsPriceInquiryWnd.ShowDialog(this);
            DialogResult result = clsPriceInquiryWnd.Result;
            clsPriceInquiryWnd.Close();
            clsPriceInquiryWnd.Dispose();

            clsEvent.AddEventLn(" Price Inquiry module closed...", true);
		}
		private void ChangeQuantity()
		{
            
            int iOldRow = dgItems.CurrentRowIndex;
            int iRow = dgItems.CurrentRowIndex;

            if (iRow >= 0)
            {
                if (dgItems[iRow, 8].ToString() != "VOID")
                {
                    if (mclsTerminalDetails.AutoPrint == PrintingPreference.Auto)
                    {
                        MessageBox.Show("Sorry you cannot change quantity if Auto-print is ON.", "RetailPlus", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    DialogResult loginresult = GetWriteAccessAndLogin(mclsSalesTransactionDetails.CashierID, AccessTypes.ChangeQuantity);

                    if (loginresult == DialogResult.OK)
                    {
                        Data.SalesTransactionItemDetails Details = getCurrentRowItemDetails();

                        decimal oldQuantity = Details.Quantity;
                        ChangeQuantityWnd QtyWnd = new ChangeQuantityWnd();
                        QtyWnd.Details = Details;
                        QtyWnd.TerminalDetails = mclsTerminalDetails;
                        QtyWnd.ShowDialog(this);
                        DialogResult result = QtyWnd.Result;
                        Details = QtyWnd.Details;

                        QtyWnd.Close();
                        QtyWnd.Dispose();

                        if (result == DialogResult.OK && oldQuantity != Details.Quantity)
                        {
                            Data.Products clsProduct = new Data.Products(mConnection, mTransaction);
                            mConnection = clsProduct.Connection; mTransaction = clsProduct.Transaction;

                            if (mboIsRefund == false)
                            {
                                if (lblCustomer.Text.Trim().ToUpper() != Constants.C_RETAILPLUS_ORDER_SLIP_CUSTOMER)
                                {
                                    if (Details.TransactionItemStatus != TransactionItemStatus.Return)
                                    {
                                        Data.ProductDetails det = clsProduct.Details(Details.ProductID, Details.VariationsMatrixID, mclsTerminalDetails.BranchID);

                                        decimal decProductCurrentQuantity = det.Quantity - det.ReservedQuantity + oldQuantity;

                                        // sep 4, 2014 Include exception for CreditPayment
                                        if (decProductCurrentQuantity < Details.Quantity && mclsTerminalDetails.ShowItemMoreThanZeroQty == true &&
                                            Details.BarCode != Data.Products.DEFAULT_CREDIT_PAYMENT_BARCODE)
                                        {
                                            clsProduct.CommitAndDispose();
                                            MessageBox.Show("Sorry the quantity you entered is greater than the current stock. " + Environment.NewLine + "Current Stock: " + decProductCurrentQuantity.ToString("#,##0.#0"), "RetailPlus", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                            return;
                                        }
                                    }
                                }
                            }

                            if (!mboIsRefund)
                            {
                                if (Details.TransactionItemStatus != TransactionItemStatus.Return)
                                {
                                    Data.ProductUnit clsProductUnit = new Data.ProductUnit(mConnection, mTransaction);
                                    mConnection = clsProductUnit.Connection; mTransaction = clsProductUnit.Transaction;

                                    decimal decNewQuantity = clsProductUnit.GetBaseUnitValue(Details.ProductID, Details.ProductUnitID, oldQuantity);

                                    clsProduct.SubtractReservedQuantity(mclsTerminalDetails.BranchID, Details.ProductID, Details.VariationsMatrixID, decNewQuantity, Data.Products.getPRODUCT_INVENTORY_MOVEMENT_VALUE(Data.PRODUCT_INVENTORY_MOVEMENT.DEDUCT_QTY_RESERVE_AND_COMMIT_CHANGE_QTY), DateTime.Now, mclsSalesTransactionDetails.TransactionNo, mclsSalesTransactionDetails.CashierName);
                                }
                            }

                            mbodgItemRowClick = true;

                            if (mboIsRefund)
                                ApplyChangeQuantityPriceAmountDetails(iRow, Details, "Change Quantity");
                            else
                            {
                                Details = ApplyPromo(Details);

                                ApplyChangeQuantityPriceAmountDetails(iRow, Details, "Change Quantity");

                                System.Data.DataTable dt = (System.Data.DataTable)dgItems.DataSource;
                                for (int x = iRow + 1; x < dt.Rows.Count; x++)
                                {
                                    dgItems.CurrentRowIndex = x;
                                    Details = getCurrentRowItemDetails();

                                    System.Data.DataRow dr = dt.Rows[x];
                                    if (dr["Quantity"].ToString() != "VOID" && dr["Quantity"].ToString().IndexOf("RETURN") == -1 && dr["ProductID"].ToString() == Details.ProductID.ToString())
                                    {
                                        Details = ApplyPromo(Details);
                                        ApplyChangeQuantityPriceAmountDetails(x, Details, "Change Quantity");
                                    }
                                }

                                dgItems.CurrentRowIndex = iOldRow;
                                dgItems.Select(iOldRow);
                            }
                            Details = getCurrentRowItemDetails();

                            // Added May 7, 2011 to Cater Reserved and Commit functionality    
                            // Details.Quantity = -oldQuantity + Details.Quantity;
                            // Jul 26, 2011 Change the AddQuantity and SubtractQuantity
                            ReservedAndCommitItem(Details, Details.TransactionItemStatus);

                            clsProduct.CommitAndDispose();
                            mbodgItemRowClick = false;

                            DisplayItemToTurretDelegate DisplayItemToTurretDel = new DisplayItemToTurretDelegate(DisplayItemToTurret);
                            DisplayItemToTurretDel.BeginInvoke(Details.Description, Details.ProductUnitCode, Details.Quantity, Details.Price, Details.Discount, Details.PromoApplied, Details.Amount, Details.VAT, Details.EVAT, null, null);
                        }
                    }
                }
            }
            
		}
		private void ChangePrice()
		{
            
            int iOldRow = dgItems.CurrentRowIndex;
            int iRow = dgItems.CurrentRowIndex;

            if (iRow >= 0)
            {
                if (dgItems[iRow, 8].ToString() != "VOID" && dgItems[iRow, 8].ToString().IndexOf("RETURN") == -1)
                {
                    if (mclsTerminalDetails.AutoPrint == PrintingPreference.Auto)
                    {
                        MessageBox.Show("Sorry you cannot change price if Auto-print is ON.", "RetailPlus", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    DialogResult loginresult = GetWriteAccessAndLogin(mclsSalesTransactionDetails.CashierID, AccessTypes.ChangePrice);

                    if (loginresult == DialogResult.OK)
                    {
                        Data.SalesTransactionItemDetails Details = getCurrentRowItemDetails();

                        decimal oldPrice = Details.Price;
                        ChangePriceWnd clsChangePriceWnd = new ChangePriceWnd();
                        clsChangePriceWnd.Details = Details;
                        clsChangePriceWnd.TerminalDetails = mclsTerminalDetails;
                        clsChangePriceWnd.ShowDialog(this);
                        DialogResult result = clsChangePriceWnd.Result;
                        Details = clsChangePriceWnd.Details;

                        clsChangePriceWnd.Close();
                        clsChangePriceWnd.Dispose();

                        if (result == DialogResult.OK && oldPrice != Details.Price)
                        {
                            Cursor.Current = Cursors.WaitCursor;

                            mbodgItemRowClick = true;

                            if (mboIsRefund)
                                ApplyChangeQuantityPriceAmountDetails(iRow, Details, "Change Price");
                            else
                            {
                                Details = ApplyPromo(Details);

                                ApplyChangeQuantityPriceAmountDetails(iRow, Details, "Change Price");

                                System.Data.DataTable dt = (System.Data.DataTable)dgItems.DataSource;
                                for (int x = iRow + 1; x < dt.Rows.Count; x++)
                                {
                                    dgItems.CurrentRowIndex = x;
                                    Details = getCurrentRowItemDetails();

                                    System.Data.DataRow dr = dt.Rows[x];
                                    if (dr["Quantity"].ToString() != "VOID" && dr["Quantity"].ToString().IndexOf("RETURN") == -1 && dr["ProductID"].ToString() == Details.ProductID.ToString())
                                    {
                                        Details = ApplyPromo(Details);
                                        ApplyChangeQuantityPriceAmountDetails(x, Details, "Change Price");
                                    }

                                }
                                dgItems.CurrentRowIndex = iOldRow;
                                dgItems.Select(iOldRow);
                            }
                            Details = getCurrentRowItemDetails();
                            DisplayItemToTurretDelegate DisplayItemToTurretDel = new DisplayItemToTurretDelegate(DisplayItemToTurret);
                            DisplayItemToTurretDel.BeginInvoke(Details.Description, Details.ProductUnitCode, Details.Quantity, Details.Price, Details.Discount, Details.PromoApplied, Details.Amount, Details.VAT, Details.EVAT, null, null);
                            InsertAuditLog(AccessTypes.ChangePrice, "Change price for item " + Details.ProductCode + " to " + Details.Price.ToString("#,##0.#0") + " @ Branch: " + mclsTerminalDetails.BranchDetails.BranchCode);
                            mbodgItemRowClick = false;
                            Cursor.Current = Cursors.Default;
                        }
                    }
                }
            }
		}
		private void ChangeAmount()
		{
			int iOldRow = dgItems.CurrentRowIndex;
			int iRow = dgItems.CurrentRowIndex;

			if (iRow >= 0)
			{

				if (dgItems[iRow, 8].ToString() != "VOID" && dgItems[iRow, 8].ToString().IndexOf("RETURN") == -1)
				{
					if (mclsTerminalDetails.AutoPrint == PrintingPreference.Auto)
					{
						MessageBox.Show("Sorry you cannot change price if Auto-print is ON.", "RetailPlus", MessageBoxButtons.OK, MessageBoxIcon.Warning);
						return;
					}
					DialogResult loginresult = GetWriteAccessAndLogin(mclsSalesTransactionDetails.CashierID, AccessTypes.ChangePrice);

					if (loginresult == DialogResult.OK)
					{
						mbodgItemRowClick = true;

						Data.SalesTransactionItemDetails Details = getCurrentRowItemDetails();

						decimal oldAmount = Details.Amount;
                        ChangeAmountWnd clsChangeAmountWnd = new ChangeAmountWnd();
                        clsChangeAmountWnd.Details = Details;
                        clsChangeAmountWnd.TerminalDetails = mclsTerminalDetails;
                        clsChangeAmountWnd.ShowDialog(this);
                        DialogResult result = clsChangeAmountWnd.Result;
                        Details = clsChangeAmountWnd.Details;

                        clsChangeAmountWnd.Close();
                        clsChangeAmountWnd.Dispose();

						if (result == DialogResult.OK && oldAmount != Details.Amount)
						{
							Cursor.Current = Cursors.WaitCursor;
							if (mboIsRefund)
								ApplyChangeQuantityPriceAmountDetails(iRow, Details, "Change Amount");
							else
							{
								Details = ApplyPromo(Details);

								ApplyChangeQuantityPriceAmountDetails(iRow, Details, "Change Amount");

								System.Data.DataTable dt = (System.Data.DataTable)dgItems.DataSource;
								for (int x = iRow + 1; x < dt.Rows.Count; x++)
								{
									dgItems.CurrentRowIndex = x;
									Details = getCurrentRowItemDetails();

									System.Data.DataRow dr = dt.Rows[x];
									if (dr["Quantity"].ToString() != "VOID" && dr["Quantity"].ToString().IndexOf("RETURN") == -1 && dr["ProductID"].ToString() == Details.ProductID.ToString())
									{
										Details = ApplyPromo(Details);
										ApplyChangeQuantityPriceAmountDetails(x, Details, "Change Amount");
									}

								}
								dgItems.CurrentRowIndex = iOldRow;
								dgItems.Select(iOldRow);
							}
							Details = getCurrentRowItemDetails();
							DisplayItemToTurretDelegate DisplayItemToTurretDel = new DisplayItemToTurretDelegate(DisplayItemToTurret);
							DisplayItemToTurretDel.BeginInvoke(Details.Description, Details.ProductUnitCode, Details.Quantity, Details.Price, Details.Discount, Details.PromoApplied, Details.Amount, Details.VAT, Details.EVAT, null, null);
							InsertAuditLog(AccessTypes.ChangePrice, "Change amount for item " + Details.ProductCode + " to " + Details.Amount.ToString("#,##0.#0") + " @ Branch: " + mclsTerminalDetails.BranchDetails.BranchCode);
							mbodgItemRowClick = false;
							Cursor.Current = Cursors.Default;
						}
					}
				}
			}
		}
		private void ApplyChangeQuantityPriceAmountDetails(int iRow, Data.SalesTransactionItemDetails Details, string strReason = "")
		{
            if (!string.IsNullOrEmpty(strReason)) clsEvent.AddEventLn(strReason, true);
            clsEvent.AddEventLn("Updating item #".PadRight(15) + ":" + Details.ItemNo + "".PadRight(15) + ":" + Details.BarCode + " " + Details.ProductCode + " Price".PadRight(15) + ":" + Details.Price + " Quantity".PadRight(15) + ":" + Details.Quantity + " Amount".PadRight(15) + ":" + Details.Amount + ".", true);

			System.Data.DataRow dr = (System.Data.DataRow)ItemDataTable.Rows[iRow];

			dr = setCurrentRowItemDetails(dr, Details);

			ComputeSubTotal(); setTotalDetails();

            Details.TransactionID = Convert.ToInt64(lblTransNo.Tag);

			//mclsSalesTransactionDetails.SubTotal = mclsSalesTransactionDetails.SubTotal;
			/*******Added: January 18, 2008***********************
			 * update purchase amount everytime there a change in 
			 *  Quantity
			 *  Price
			 *  Amount *********************************/
			Details.PurchaseAmount = Details.Quantity * Details.PurchasePrice;
			dr["PurchaseAmount"] = Details.PurchaseAmount;

			Data.SalesTransactions clsSalesTransactions = new Data.SalesTransactions(mConnection, mTransaction);
            mConnection = clsSalesTransactions.Connection; mTransaction = clsSalesTransactions.Transaction;

            clsSalesTransactions.UpdateItem(mclsSalesTransactionDetails.TransactionID, mclsSalesTransactionDetails.ItemSold, mclsSalesTransactionDetails.QuantitySold, mclsSalesTransactionDetails.SubTotal, mclsSalesTransactionDetails.NetSales, mclsSalesTransactionDetails.ItemsDiscount, mclsSalesTransactionDetails.Discount, mclsSalesTransactionDetails.SNRDiscount, mclsSalesTransactionDetails.PWDDiscount, mclsSalesTransactionDetails.OtherDiscount, mclsSalesTransactionDetails.TransDiscount, mclsSalesTransactionDetails.TransDiscountType, mclsSalesTransactionDetails.VAT, mclsSalesTransactionDetails.VATableAmount, mclsSalesTransactionDetails.NonVATableAmount, mclsSalesTransactionDetails.VATExempt, mclsSalesTransactionDetails.EVAT, mclsSalesTransactionDetails.EVATableAmount, mclsSalesTransactionDetails.NonEVATableAmount, mclsSalesTransactionDetails.LocalTax, mclsSalesTransactionDetails.DiscountCode, mclsSalesTransactionDetails.DiscountRemarks, mclsSalesTransactionDetails.Charge, mclsSalesTransactionDetails.ChargeAmount, mclsSalesTransactionDetails.ChargeCode, mclsSalesTransactionDetails.ChargeRemarks, mclsSalesTransactionDetails.ChargeType, Details);
			clsSalesTransactions.CommitAndDispose();

            clsEvent.AddEventLn("Updating item #".PadRight(15) + ":" + Details.ItemNo + " : done", true);

		}
		private void ReturnItem()
		{
            if (mboIsRefund || mclsTerminalDetails.IsParkingTerminal)
				return;

			DialogResult loginresult = GetWriteAccessAndLogin(mclsSalesTransactionDetails.CashierID, AccessTypes.ReturnItem);

			if (loginresult == DialogResult.OK)
			{
				TransactionNoWnd clsTransactionNoWnd = new TransactionNoWnd();
				clsTransactionNoWnd.TransactionNoLength = mclsTerminalDetails.TransactionNoLength;
				clsTransactionNoWnd.TerminalNo = mclsTerminalDetails.TerminalNo;
				clsTransactionNoWnd.ShowDialog(this);
				DialogResult result = clsTransactionNoWnd.Result;
				string strTransactionNo = "";
				string strTerminalNo = mclsTerminalDetails.TerminalNo;
				if (result == DialogResult.OK)
				{
					strTransactionNo = clsTransactionNoWnd.TransactionNo;
					strTerminalNo = clsTransactionNoWnd.TerminalNo;
				}

				clsTransactionNoWnd.Close();
				clsTransactionNoWnd.Dispose();

				if (result == DialogResult.OK)
				{
					TransactionReturnItemSelectWnd ItemWnd = new TransactionReturnItemSelectWnd();

					ItemWnd.TransactionNo = strTransactionNo;
					ItemWnd.TerminalDetails = mclsTerminalDetails;
					ItemWnd.ShowDialog(this);
					if (ItemWnd.Result == DialogResult.OK)
					{
						Cursor.Current = Cursors.WaitCursor;
						if (!mboIsInTransaction)
						{
							lblTransDate.Text = DateTime.Now.ToString("MMM. dd, yyyy hh:mm:ss tt");
							if (!this.CreateTransaction()) return;
						}

						Data.SalesTransactionItemDetails details = new Data.SalesTransactionItemDetails();
						details = ItemWnd.Details;

						details.TransactionItemStatus = TransactionItemStatus.Return;

						System.Data.DataRow dr = ItemDataTable.NewRow();

						details.TransactionItemStatus = TransactionItemStatus.Return;
						details.ItemNo = Convert.ToString(ItemDataTable.Rows.Count + 1);

						dr = setCurrentRowItemDetails(dr, details);

						Data.SalesTransactions clsSalesTransactions = new Data.SalesTransactions(mConnection, mTransaction);
                        mConnection = clsSalesTransactions.Connection; mTransaction = clsSalesTransactions.Transaction;

						details.TransactionItemsID = AddItemToDB(details);
						dr["TransactionItemsID"] = details.TransactionItemsID.ToString();
						
                        
                        // Sep 14, 2013: Removed if return. Return should have no effect in Reserved and Commit
						// Added May 7, 2011 to Cater Reserved and Commit functionality    
						// ReservedAndCommitItem(details, details.TransactionItemStatus);

						ItemDataTable.Rows.Add(dr);

						dgItems.CurrentRowIndex = ItemDataTable.Rows.Count;
						dgItems.Select(dgItems.CurrentRowIndex);
						SetItemDetails();
						ComputeSubTotal(); setTotalDetails();

                        clsSalesTransactions.UpdateSubTotal(mclsSalesTransactionDetails.TransactionID, mclsSalesTransactionDetails.ItemSold, mclsSalesTransactionDetails.QuantitySold, mclsSalesTransactionDetails.SubTotal, mclsSalesTransactionDetails.NetSales, mclsSalesTransactionDetails.ItemsDiscount, mclsSalesTransactionDetails.Discount, mclsSalesTransactionDetails.SNRDiscount, mclsSalesTransactionDetails.PWDDiscount, mclsSalesTransactionDetails.OtherDiscount, mclsSalesTransactionDetails.TransDiscount, mclsSalesTransactionDetails.TransDiscountType, mclsSalesTransactionDetails.VAT, mclsSalesTransactionDetails.VATableAmount, mclsSalesTransactionDetails.NonVATableAmount, mclsSalesTransactionDetails.VATExempt, mclsSalesTransactionDetails.EVAT, mclsSalesTransactionDetails.EVATableAmount, mclsSalesTransactionDetails.NonEVATableAmount, mclsSalesTransactionDetails.LocalTax, mclsSalesTransactionDetails.DiscountCode, mclsSalesTransactionDetails.DiscountRemarks, mclsSalesTransactionDetails.Charge, mclsSalesTransactionDetails.ChargeAmount, mclsSalesTransactionDetails.ChargeCode, mclsSalesTransactionDetails.ChargeRemarks, mclsSalesTransactionDetails.ChargeType);
                        clsSalesTransactions.CommitAndDispose();

                        InsertAuditLog(AccessTypes.RefundTransaction, "Return Item " + details.ProductCode + "." + " @ Branch: " + mclsTerminalDetails.BranchDetails.BranchCode);
						
						try
						{
							DisplayItemToTurretDelegate DisplayItemToTurretDel = new DisplayItemToTurretDelegate(DisplayItemToTurret);
							DisplayItemToTurretDel.BeginInvoke("RET-" + details.ProductCode, details.ProductUnitCode, details.Quantity, details.Price, details.Discount, details.PromoApplied, details.Amount, details.VAT, details.EVAT, null, null);
						}
						catch { }
						if (mclsTerminalDetails.AutoPrint == PrintingPreference.Auto)
						{
							PrintItemDelegate PrintItemDel = new PrintItemDelegate(PrintItem);
							string strProductCode = details.ProductCode;
							if (details.MatrixDescription != string.Empty && details.MatrixDescription != null) strProductCode += "-" + details.MatrixDescription;
							PrintItemDel.BeginInvoke(details.ItemNo, strProductCode + " - RET ", details.ProductUnitCode, details.Quantity, details.Price, details.Discount, details.PromoApplied, details.Amount, details.VAT, details.EVAT, null, null);
						}

						
						Cursor.Current = Cursors.Default;
					}

					ItemWnd.Close();
					ItemWnd.Dispose();
				}
			}
		}

		private void ApplyItemDiscount()
		{
			int iRow = dgItems.CurrentRowIndex;
			if (iRow < 0) return;

			if (dgItems[iRow, 8].ToString() != "VOID")
			{

				if (dgItems[iRow, 8].ToString().IndexOf("RETURN") == -1)
				{
					if (mclsTerminalDetails.AutoPrint == PrintingPreference.Auto)
					{
						MessageBox.Show("Sorry you cannot apply a discount to an item if Auto-print is ON.", "RetailPlus", MessageBoxButtons.OK, MessageBoxIcon.Warning);
						return;
					}
					DialogResult loginresult = GetWriteAccessAndLogin(mclsSalesTransactionDetails.CashierID, AccessTypes.ApplyItemDiscount);

                    if (loginresult == DialogResult.OK)
					{
						//mboIsDiscountAuthorized = true;
						try
						{
							Data.SalesTransactionItemDetails clsItemDetails;
							clsItemDetails = getCurrentRowItemDetails();
							clsEvent.AddEvent("[" + lblCashier.Text + "] Applying item discount for item. no. [" + clsItemDetails.TransactionItemsID + "]" + clsItemDetails.ProductCode);

						Back:
							DiscountWnd discount = new DiscountWnd();
							discount.Header = "Apply Item Discount for item #" + clsItemDetails.ItemNo + " - " + clsItemDetails.ProductCode; 
							discount.BalanceAmount = clsItemDetails.Amount;
							discount.DiscountType = clsItemDetails.ItemDiscountType;
							discount.DiscountAmount = clsItemDetails.ItemDiscount;
							discount.DiscountCode = clsItemDetails.DiscountCode;
							discount.Remarks = clsItemDetails.DiscountRemarks;
							discount.IsDiscountEditable = mclsTerminalDetails.IsDiscountEditable;
							discount.ShowDialog(this);
							decimal DiscountAmount = discount.DiscountAmount;
							string ItemDiscountCode = discount.DiscountCode;
							string ItemDiscountRemarks = discount.Remarks;
							DiscountTypes itemDiscountType = discount.DiscountType;
							DialogResult result = discount.Result;
							discount.Close();
							discount.Dispose();

							if (result == DialogResult.OK)
							{
								Cursor.Current = Cursors.WaitCursor;
								clsItemDetails.ItemDiscount = DiscountAmount;
								clsItemDetails.Discount = clsItemDetails.ItemDiscount;
								clsItemDetails.ItemDiscountType = itemDiscountType;
								clsItemDetails.DiscountCode = ItemDiscountCode;
								clsItemDetails.DiscountRemarks = ItemDiscountRemarks;

								if (clsItemDetails.ItemDiscountType == DiscountTypes.Percentage)
								{
                                    //overwrite the discountable amount if its SENIORCITIZEN
                                    if (clsItemDetails.DiscountCode == mclsTerminalDetails.SeniorCitizenDiscountCode && mclsTerminalDetails.IsVATInclusive)
                                    {
                                        clsItemDetails.Amount = clsItemDetails.Amount / (1 + (mclsTerminalDetails.VAT / 100));
                                    }
                                    clsItemDetails.Discount = clsItemDetails.Amount * (clsItemDetails.ItemDiscount / 100);
								}

                                if (clsItemDetails.Discount >= clsItemDetails.Amount && clsItemDetails.DiscountCode != Constants.C_DISCOUNT_CODE_FREE)
								{
                                    MessageBox.Show("Sorry the discount is more than the transaction amount. Please select another discount.", "RetailPlus", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);
									goto Back;
								}
                                else if (clsItemDetails.Discount > clsItemDetails.Amount && clsItemDetails.DiscountCode == Constants.C_DISCOUNT_CODE_FREE)
                                {
                                    MessageBox.Show("Sorry the FREE discount cannot be more than 100%. Please set the FREE discount to 100% or less.", "RetailPlus", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);
                                    goto Back;
                                }

								clsEvent.AddEventLn("discount=" + clsItemDetails.Discount.ToString("#,###.#0"));

								clsItemDetails.Amount = clsItemDetails.Amount - clsItemDetails.Discount - clsItemDetails.PromoApplied;
								clsItemDetails.Commision = clsItemDetails.Amount * (clsItemDetails.PercentageCommision / 100);
								System.Data.DataRow dr = (System.Data.DataRow)ItemDataTable.Rows[iRow];

								dr = setCurrentRowItemDetails(dr, clsItemDetails);

								ComputeSubTotal(); setTotalDetails();

								if (mclsSalesTransactionDetails.DiscountableAmount==0)
								{
									mclsSalesTransactionDetails.TransDiscountType = DiscountTypes.NotApplicable;
									mclsSalesTransactionDetails.TransDiscount = 0;
									ComputeSubTotal(); setTotalDetails();
								}

                                Data.SalesTransactions clsSalesTransactions = new Data.SalesTransactions(mConnection, mTransaction);
                                mConnection = clsSalesTransactions.Connection; mTransaction = clsSalesTransactions.Transaction;

								/*******Added: April 12, 2010***********************
								 * update purchase amount everytime there a change in 
								 *  
								 *  discount *********************************/
								clsItemDetails.PurchaseAmount = clsItemDetails.Quantity * clsItemDetails.PurchasePrice;
								dr["PurchaseAmount"] = clsItemDetails.PurchaseAmount;

                                clsSalesTransactions.UpdateItem(mclsSalesTransactionDetails.TransactionID, mclsSalesTransactionDetails.ItemSold, mclsSalesTransactionDetails.QuantitySold, mclsSalesTransactionDetails.SubTotal, mclsSalesTransactionDetails.NetSales, mclsSalesTransactionDetails.ItemsDiscount, mclsSalesTransactionDetails.Discount, mclsSalesTransactionDetails.SNRDiscount, mclsSalesTransactionDetails.PWDDiscount, mclsSalesTransactionDetails.OtherDiscount, mclsSalesTransactionDetails.TransDiscount, mclsSalesTransactionDetails.TransDiscountType, mclsSalesTransactionDetails.VAT, mclsSalesTransactionDetails.VATableAmount, mclsSalesTransactionDetails.NonVATableAmount, mclsSalesTransactionDetails.VATExempt, mclsSalesTransactionDetails.EVAT, mclsSalesTransactionDetails.EVATableAmount, mclsSalesTransactionDetails.NonEVATableAmount, mclsSalesTransactionDetails.LocalTax, mclsSalesTransactionDetails.DiscountCode, mclsSalesTransactionDetails.DiscountRemarks, mclsSalesTransactionDetails.Charge, mclsSalesTransactionDetails.ChargeAmount, mclsSalesTransactionDetails.ChargeCode, mclsSalesTransactionDetails.ChargeRemarks, mclsSalesTransactionDetails.ChargeType, clsItemDetails);
                                clsSalesTransactions.CommitAndDispose();

                                InsertAuditLog(AccessTypes.Discounts, "Apply item discount for " + clsItemDetails.ProductCode + ". discount=" + clsItemDetails.Discount.ToString("#,###.#0") + " @ Branch: " + mclsTerminalDetails.BranchDetails.BranchCode);

								clsEvent.AddEventLn("Done applying item discount...", true);

								DisplayItemToTurretDelegate DisplayItemToTurretDel = new DisplayItemToTurretDelegate(DisplayItemToTurret);
								DisplayItemToTurretDel.BeginInvoke(clsItemDetails.Description, clsItemDetails.ProductUnitCode, clsItemDetails.Quantity, clsItemDetails.Price, clsItemDetails.Discount, clsItemDetails.PromoApplied, clsItemDetails.Amount, clsItemDetails.VAT, clsItemDetails.EVAT, null, null);

							}
							else { clsEvent.AddEventLn("Cancelled!"); }
						}
						catch (Exception ex)
						{ 
                            InsertErrorLogToFile(ex, "ERROR!!! Applying item discount."); 
                        }

						Cursor.Current = Cursors.Default;
					}
				}
			}
		}
		private void ApplyItemDiscountForAllItem()
		{
			if (ItemDataTable.Rows.Count <= 0) return;

			if (mclsTerminalDetails.AutoPrint == PrintingPreference.Auto)
			{
				MessageBox.Show("Sorry you cannot apply a discount to an item if Auto-print is ON.", "RetailPlus", MessageBoxButtons.OK, MessageBoxIcon.Warning);
				return;
			}
			DialogResult loginresult = GetWriteAccessAndLogin(mclsSalesTransactionDetails.CashierID, AccessTypes.ApplyItemDiscount);

			if (loginresult == DialogResult.OK)
			{
				//mboIsDiscountAuthorized = true;

				DiscountTypes TransDiscountType = DiscountTypes.NotApplicable;
				DiscountWnd discount = new DiscountWnd();
				discount.Header = "Apply Discounts on punched items"; 
				discount.BalanceAmount = mclsSalesTransactionDetails.SubTotal;
				discount.DiscountType = DiscountTypes.Percentage;
				discount.DiscountAmount = mclsSalesTransactionDetails.TransDiscount;
				discount.DiscountCode = mclsSalesTransactionDetails.DiscountCode;
				discount.Remarks = mclsSalesTransactionDetails.DiscountRemarks;
				discount.IsDiscountEditable = mclsTerminalDetails.IsDiscountEditable;
				discount.ShowDialog(this);
				DialogResult result = discount.Result;
				decimal DiscountAmount = discount.DiscountAmount;
				string TransDiscountCode = discount.DiscountCode;
				string TransDiscountRemarks = discount.Remarks;
				TransDiscountType = discount.DiscountType;
				discount.Close();
				discount.Dispose();

				if (result == DialogResult.OK)
				{
					Cursor.Current = Cursors.WaitCursor;

					int iCurrentSelectedRow = dgItems.CurrentRowIndex;

					for(int iRowCtr=0;iRowCtr<ItemDataTable.Rows.Count;iRowCtr++)
					{
						dgItems.Select(iRowCtr);
						dgItems.CurrentRowIndex = iRowCtr;
						if (dgItems[iRowCtr, 8].ToString() != "VOID")
						{
							if (dgItems[iRowCtr, 8].ToString().IndexOf("RETURN") == -1)
							{
								try
								{
									Data.SalesTransactionItemDetails clsItemDetails;
									clsItemDetails = getCurrentRowItemDetails();
									clsEvent.AddEvent("[" + lblCashier.Text + "] Applying item discount for item. no. [" + clsItemDetails.TransactionItemsID + "]" + clsItemDetails.ProductCode);

									clsItemDetails.ItemDiscount = DiscountAmount;
									clsItemDetails.Discount = DiscountAmount;
									clsItemDetails.ItemDiscountType = TransDiscountType;
									clsItemDetails.DiscountCode = TransDiscountCode;
									clsItemDetails.DiscountRemarks = TransDiscountRemarks;

                                    if (clsItemDetails.ItemDiscountType == DiscountTypes.Percentage)
                                    {
                                        //overwrite the discountable amount if its SENIORCITIZEN
                                        if (clsItemDetails.DiscountCode == mclsTerminalDetails.SeniorCitizenDiscountCode && mclsTerminalDetails.IsVATInclusive)
                                        {
                                            clsItemDetails.Amount = clsItemDetails.Amount / (1 + (mclsTerminalDetails.VAT / 100));
                                        }
                                        clsItemDetails.Discount = clsItemDetails.Amount * (clsItemDetails.ItemDiscount / 100);
                                    }
									//if (clsItemDetails.Discount >= clsItemDetails.Amount)
									//{
									//    MessageBox.Show("Sorry the input discount will yield a less than ZERO amount. Please type another discount.", "RetailPlus", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);
									//    goto Back;
									//}

									clsEvent.AddEventLn("discount=" + clsItemDetails.Discount.ToString("#,###.#0"));

									clsItemDetails.Amount = clsItemDetails.Amount - clsItemDetails.Discount - clsItemDetails.PromoApplied;
									clsItemDetails.Commision = clsItemDetails.Amount * (clsItemDetails.PercentageCommision / 100);
									System.Data.DataRow dr = (System.Data.DataRow)ItemDataTable.Rows[iRowCtr];

									dr = setCurrentRowItemDetails(dr, clsItemDetails);

									ComputeSubTotal(); setTotalDetails();

									if (mclsSalesTransactionDetails.DiscountableAmount == 0)
									{
										mclsSalesTransactionDetails.TransDiscountType = DiscountTypes.NotApplicable;
										mclsSalesTransactionDetails.TransDiscount = 0;
										ComputeSubTotal(); setTotalDetails();
									}

									Data.SalesTransactions clsSalesTransactions = new Data.SalesTransactions(mConnection, mTransaction);
                                    mConnection = clsSalesTransactions.Connection; mTransaction = clsSalesTransactions.Transaction;

									/*******Added: April 12, 2010***********************
									* update purchase amount everytime there a change in 
									*  
									*  discount *********************************/
									clsItemDetails.PurchaseAmount = clsItemDetails.Quantity * clsItemDetails.PurchasePrice;
									dr["PurchaseAmount"] = clsItemDetails.PurchaseAmount;

                                    clsSalesTransactions.UpdateItem(mclsSalesTransactionDetails.TransactionID, mclsSalesTransactionDetails.ItemSold, mclsSalesTransactionDetails.QuantitySold, mclsSalesTransactionDetails.SubTotal, mclsSalesTransactionDetails.NetSales, mclsSalesTransactionDetails.ItemsDiscount, mclsSalesTransactionDetails.Discount, mclsSalesTransactionDetails.SNRDiscount, mclsSalesTransactionDetails.PWDDiscount, mclsSalesTransactionDetails.OtherDiscount, mclsSalesTransactionDetails.TransDiscount, mclsSalesTransactionDetails.TransDiscountType, mclsSalesTransactionDetails.VAT, mclsSalesTransactionDetails.VATableAmount, mclsSalesTransactionDetails.NonVATableAmount, mclsSalesTransactionDetails.VATExempt, mclsSalesTransactionDetails.EVAT, mclsSalesTransactionDetails.EVATableAmount, mclsSalesTransactionDetails.NonEVATableAmount, mclsSalesTransactionDetails.LocalTax, mclsSalesTransactionDetails.DiscountCode, mclsSalesTransactionDetails.DiscountRemarks, mclsSalesTransactionDetails.Charge, mclsSalesTransactionDetails.ChargeAmount, mclsSalesTransactionDetails.ChargeCode, mclsSalesTransactionDetails.ChargeRemarks, mclsSalesTransactionDetails.ChargeType, clsItemDetails);
                                    clsSalesTransactions.CommitAndDispose();

									InsertAuditLog(AccessTypes.Discounts, "Apply item discount for " + clsItemDetails.ProductCode + ". discount=" + clsItemDetails.Discount.ToString("#,###.#0") + " @ Branch: " + mclsTerminalDetails.BranchDetails.BranchCode);

									clsEvent.AddEventLn("Done applying item discount...", true);

									DisplayItemToTurretDelegate DisplayItemToTurretDel = new DisplayItemToTurretDelegate(DisplayItemToTurret);
									DisplayItemToTurretDel.BeginInvoke(clsItemDetails.Description, clsItemDetails.ProductUnitCode, clsItemDetails.Quantity, clsItemDetails.Price, clsItemDetails.Discount, clsItemDetails.PromoApplied, clsItemDetails.Amount, clsItemDetails.VAT, clsItemDetails.EVAT, null, null);
								}
								catch (Exception ex)
								{ 
                                    InsertErrorLogToFile(ex, "ERROR!!! Applying discount for all item."); 
                                }
								Cursor.Current = Cursors.Default;
							}
						}

						dgItems.UnSelect(iRowCtr);
					}
					dgItems.Select(iCurrentSelectedRow);
					dgItems.CurrentRowIndex = iCurrentSelectedRow;
				}
				else { clsEvent.AddEventLn("Cancelled!"); }
			}
		}
		private bool SelectContact(AceSoft.RetailPlus.Data.ContactGroupCategory enumContactGroupCategory)
		{
            bool boretValue = true;

			// Sep 24, 2011      Lemuel E. Aceron
			// Added order slip wherein all punch items will not change sales and inventory
			// a customer named ORDER SLIP should be defined in contacts
			if (lblCustomer.Text.Trim().ToUpper() == Constants.C_RETAILPLUS_ORDER_SLIP_CUSTOMER && mboIsInTransaction && enumContactGroupCategory == AceSoft.RetailPlus.Data.ContactGroupCategory.CUSTOMER)
			{
				MessageBox.Show("Sorry you cannot select ORDER SLIP customer when an item is already purchased.", "RetailPlus", MessageBoxButtons.OK, MessageBoxIcon.Warning);
				return false;
			}

			if (mclsTerminalDetails.AutoPrint == PrintingPreference.Auto && mboIsInTransaction)
			{
				switch (enumContactGroupCategory)
				{ 
					case AceSoft.RetailPlus.Data.ContactGroupCategory.CUSTOMER:
						MessageBox.Show("Sorry you cannot select a customer when an item is already purchased.", "RetailPlus", MessageBoxButtons.OK, MessageBoxIcon.Warning);
						break;
					case AceSoft.RetailPlus.Data.ContactGroupCategory.AGENT:
						MessageBox.Show("Sorry you cannot select an agent when an item is already purchased.", "RetailPlus", MessageBoxButtons.OK, MessageBoxIcon.Warning);
						break;
				}

				return false;
			}

			try
			{
				switch (enumContactGroupCategory)
				{
					case AceSoft.RetailPlus.Data.ContactGroupCategory.CUSTOMER:
						clsEvent.AddEvent("[" + lblCashier.Text + "] Selecting customer.");
						if (mclsTerminalDetails.ShowCustomerSelection == false)
						{
							clsEvent.AddEventLn("Cancelled! ShowCustomerSelection is OFF, reward is ON.");
							txtBarCode.Text = Constants.SWIPE_REWARD_CARD;
							txtBarCode.Focus(); txtBarCode.SelectionStart = txtBarCode.Text.Length + 1;
							return false;
						}
						break;
					case AceSoft.RetailPlus.Data.ContactGroupCategory.AGENT:
						clsEvent.AddEvent("[" + lblCashier.Text + "] Selecting agent.");
						break;
				}

				DialogResult result; Data.ContactDetails details;
				ContactSelectWnd clsContactWnd = new ContactSelectWnd();
                clsContactWnd.TerminalDetails = mclsTerminalDetails;
				clsContactWnd.ContactGroupCategory = enumContactGroupCategory;
				clsContactWnd.ShowDialog(this);
				details = clsContactWnd.Details;
				result = clsContactWnd.Result;
				clsContactWnd.Close();
				clsContactWnd.Dispose();

				if (result == DialogResult.OK)
				{
					LoadContact(enumContactGroupCategory, details);
				}
                else { clsEvent.AddEventLn("Cancelled!"); boretValue = false; }

			}
			catch (Exception ex)
			{ 
                InsertErrorLogToFile(ex, "ERROR!!! Selecting contact.");
                boretValue = false;
            }
            return boretValue;
		}
		private void LoadContact(AceSoft.RetailPlus.Data.ContactGroupCategory enumContactGroupCategory, Data.ContactDetails pContactDetails)
		{
			try
			{
                if ((mclsTerminalDetails.ShowCustomerSelection || pContactDetails.ContactID != 0) && 
                    (enumContactGroupCategory == Data.ContactGroupCategory.CUSTOMER || enumContactGroupCategory == Data.ContactGroupCategory.AGENT))
					mclsContactDetails = pContactDetails;
				else
				{
					string strContactCardNo = txtBarCode.Text.Replace(Constants.SWIPE_REWARD_CARD, "").Trim();

					Data.Contacts clsContact = new Data.Contacts(mConnection, mTransaction);
                    mConnection = clsContact.Connection; mTransaction = clsContact.Transaction;

					// check using reward card info
					mclsContactDetails = clsContact.DetailsByRewardCardNo(strContactCardNo);
					if (mclsContactDetails.ContactID != 0)
						mboRewardCardSwiped = true;
					else if (mclsContactDetails.ContactID == 0) 
					{
						// check using credit card info
						mclsContactDetails = clsContact.DetailsByCreditCardNo(strContactCardNo);
						if (mclsContactDetails.ContactID != 0)
							mboCreditCardSwiped = true;
						else if (mclsContactDetails.ContactID == 0) 
						{
							strContactCardNo = strContactCardNo.Remove(strContactCardNo.Length - 1);
							// check using reward card info
							mclsContactDetails = clsContact.DetailsByRewardCardNo(strContactCardNo);
							if (mclsContactDetails.ContactID != 0)
								mboRewardCardSwiped = true;
							else if (mclsContactDetails.ContactID == 0)
							{
								// check using credit card info
								mclsContactDetails = clsContact.DetailsByCreditCardNo(strContactCardNo);
								if (mclsContactDetails.ContactID != 0)
									mboCreditCardSwiped = true;
								else if (mclsContactDetails.ContactID == 0)
								{ clsContact.CommitAndDispose(); SelectContact(AceSoft.RetailPlus.Data.ContactGroupCategory.CUSTOMER); return;}
							}
						}

					}
					clsContact.CommitAndDispose();
				}

				// Sep 24, 2011      Lemuel E. Aceron
				// Added order slip wherein all punch items will not change sales and inventory
				// a customer named ORDER SLIP should be defined in contacts
				if (mclsContactDetails.ContactName.Trim().ToUpper() == Constants.C_RETAILPLUS_ORDER_SLIP_CUSTOMER && mboIsInTransaction && enumContactGroupCategory == AceSoft.RetailPlus.Data.ContactGroupCategory.CUSTOMER)
				{
					MessageBox.Show("Sorry you cannot select ORDER SLIP customer when an item is already purchased.", "RetailPlus", MessageBoxButtons.OK, MessageBoxIcon.Warning);
					clsEvent.AddEventLn("Cancelled!"); return;
				}

				switch (enumContactGroupCategory)
				{
					case AceSoft.RetailPlus.Data.ContactGroupCategory.CUSTOMER:
						lblCustomer.Tag = mclsContactDetails.ContactID;
						lblCustomer.Text = mclsContactDetails.ContactName;
						
						if (!mclsTerminalDetails.ShowCustomerSelection) { txtBarCode.Text = string.Empty; txtBarCode.Focus(); }
						clsEvent.AddEventLn("Done! Selected customer: " + lblCustomer.Text);

						if (mboIsInTransaction)
						{
                            Data.SalesTransactions clsSalesTransactions = new Data.SalesTransactions(mConnection, mTransaction);
                            mConnection = clsSalesTransactions.Connection; mTransaction = clsSalesTransactions.Transaction;

							clsSalesTransactions.UpdateContact(mclsSalesTransactionDetails.TransactionID, mclsSalesTransactionDetails.TransactionDate, mclsContactDetails);
							clsSalesTransactions.CommitAndDispose();
						}
                        mclsSalesTransactionDetails.CustomerID = mclsContactDetails.ContactID;
						mclsSalesTransactionDetails.CustomerName = mclsContactDetails.ContactName;
						mclsSalesTransactionDetails.RewardCardActive = mclsContactDetails.RewardDetails.RewardActive;
						mclsSalesTransactionDetails.RewardCardNo = mclsContactDetails.RewardDetails.RewardCardNo;
						mclsSalesTransactionDetails.RewardPreviousPoints = mclsContactDetails.RewardDetails.RewardPoints;
						mclsSalesTransactionDetails.RewardCurrentPoints = mclsSalesTransactionDetails.RewardPreviousPoints;

						break;
					case AceSoft.RetailPlus.Data.ContactGroupCategory.AGENT:
                        lblAgent.Tag = pContactDetails.ContactID;
                        lblAgent.Text = pContactDetails.ContactName;
                        lblAgentPositionDepartment.Text = pContactDetails.PositionName;
                        lblAgentPositionDepartment.Tag = pContactDetails.DepartmentName;
						clsEvent.AddEventLn("Done! Selected agent: " + lblAgent.Text);

						if (mboIsInTransaction)
						{
                            Data.SalesTransactions clsSalesTransactions = new Data.SalesTransactions(mConnection, mTransaction);
                            mConnection = clsSalesTransactions.Connection; mTransaction = clsSalesTransactions.Transaction;

                            clsSalesTransactions.UpdateAgent(mclsSalesTransactionDetails.TransactionID, mclsSalesTransactionDetails.TransactionDate, pContactDetails);
							clsSalesTransactions.CommitAndDispose();
						}
                        mclsSalesTransactionDetails.AgentID = pContactDetails.ContactID;
                        mclsSalesTransactionDetails.AgentName = pContactDetails.ContactName;
						break;
				}
			}
			catch (Exception ex)
			{ 
                InsertErrorLogToFile(ex, "ERROR!!! Loading contact."); 
            }
		}
        private void UpdateContact()
        {
            try {
                if (mclsSalesTransactionDetails.CustomerID != 0 && mclsSalesTransactionDetails.CustomerID != Constants.C_RETAILPLUS_CUSTOMERID)
                {
                    DialogResult loginresult = GetWriteAccessAndLogin(mclsSalesTransactionDetails.CashierID, AccessTypes.Contacts);

                    if (loginresult == DialogResult.OK)
                    {
                        Data.Contacts clsContact = new Data.Contacts(mConnection, mTransaction);
                        mConnection = clsContact.Connection; mTransaction = clsContact.Transaction;

                        mclsContactDetails = clsContact.Details(mclsSalesTransactionDetails.CustomerID);
                        clsContact.CommitAndDispose();

                        DialogResult addresult = System.Windows.Forms.DialogResult.Cancel;
                        Data.ContactDetails details;

                        if (!mclsTerminalDetails.ShowCustomerSelection)
                        {
                            ContactAddDetWnd clsContactAddWnd = new ContactAddDetWnd();
                            clsContactAddWnd.Caption = "Update Customer [" + mclsContactDetails.ContactName + "]";
                            clsContactAddWnd.ContactDetails = mclsContactDetails;
                            clsContactAddWnd.ShowDialog(this);
                            addresult = clsContactAddWnd.Result;
                            details = clsContactAddWnd.ContactDetails;
                            clsContactAddWnd.Close();
                            clsContactAddWnd.Dispose();
                        }
                        else
                        {
                            ContactAddWnd clsContactAddWnd = new ContactAddWnd();
                            clsContactAddWnd.Caption = "Update Customer [" + mclsContactDetails.ContactName + "]";
                            clsContactAddWnd.ContactDetails = mclsContactDetails;
                            clsContactAddWnd.ShowDialog(this);
                            addresult = clsContactAddWnd.Result;
                            details = clsContactAddWnd.ContactDetails;
                            clsContactAddWnd.Close();
                            clsContactAddWnd.Dispose();
                        }
                        if (addresult == DialogResult.OK)
                        {
                            LoadContact(Data.ContactGroupCategory.CUSTOMER, details);
                            MessageBox.Show("Customer has been updated and the details has been reloaded for this transaction.", "RetailPlus", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                }
            }
            catch { }
        }

        /// <summary>
        /// Check if there is an ongoing transaction
        /// If yes, returns true if will continue
        /// If No, returns false
        /// </summary>
        /// <returns></returns>
        private bool SuspendTransactionAndContinue()
        {
            bool boRetValue = true;

            if (mboIsInTransaction)
            {
                if (MessageBox.Show("Active Transaction Found! Suspend current transaction first?", "RetailPlus", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) == DialogResult.OK)
                {
                    return SuspendTransaction(true);
                }
                else
                    boRetValue = false;
            }

            return boRetValue;

        }
        private bool SuspendTransaction(bool ShowNotificationWindow = true)
		{
			bool boRetValue = false;

			if (!mboIsInTransaction)
			{
				MessageBox.Show("Sorry you cannot suspend an empty transaction.", "RetailPlus", MessageBoxButtons.OK, MessageBoxIcon.Warning);
				return boRetValue;
			}
            //if (mboIsRefund)
            //{
            //    MessageBox.Show("Sorry you cannot suspend a REFUND transaction.", "RetailPlus", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //    return boRetValue;
            //}

			DialogResult loginresult = GetWriteAccessAndLogin(mclsSalesTransactionDetails.CashierID, AccessTypes.SuspendTransaction);

			if (loginresult == DialogResult.OK)
			{
				if (mclsSalesTransactionDetails.CustomerID == Constants.C_RETAILPLUS_CUSTOMERID)
				{
					try
					{
						clsEvent.AddEvent("[" + lblCashier.Text + "] Suspending transaction no. " + lblTransNo.Text);

						ContactAddWnd clsContactAddWnd = new ContactAddWnd();
						clsContactAddWnd.Caption = "Suspend Transaction: Please Enter Customer Name";
						clsContactAddWnd.ShowDialog(this);
						DialogResult addresult = clsContactAddWnd.Result;
						Data.ContactDetails details = clsContactAddWnd.ContactDetails;
						clsContactAddWnd.Close();
						clsContactAddWnd.Dispose();

						if (addresult == DialogResult.OK)
						{
							lblCustomer.Text = details.ContactName;
							lblCustomer.Tag = details.ContactID.ToString();

							Data.SalesTransactions clsSalesTransactions = new Data.SalesTransactions(mConnection, mTransaction);
                            mConnection = clsSalesTransactions.Connection; mTransaction = clsSalesTransactions.Transaction;

							clsSalesTransactions.Suspend(mclsSalesTransactionDetails.TransactionID, mclsSalesTransactionDetails.SubTotal, mclsSalesTransactionDetails.NetSales, details);

                            // Sep 24, 2014 : update back the LastCheckInDate to min date
                            Data.Contacts clsContact = new Data.Contacts(mConnection, mTransaction);
                            mConnection = clsContact.Connection; mTransaction = clsContact.Transaction;

                            clsContact.UpdateLastCheckInDate(mclsSalesTransactionDetails.CustomerID, mclsSalesTransactionDetails.TransactionDate);

							InsertAuditLog(AccessTypes.SuspendTransaction, "Suspend transaction #: " + lblTransNo.Text + " @ Branch: " + mclsTerminalDetails.BranchDetails.BranchCode);

							if (mclsTerminalDetails.AutoPrint == PrintingPreference.Auto)
								PrintReportFooterSection(true, TransactionStatus.Suspended, mclsSalesTransactionDetails.ItemSold, mclsSalesTransactionDetails.QuantitySold, mclsSalesTransactionDetails.SubTotal, mclsSalesTransactionDetails.Discount, mclsSalesTransactionDetails.Charge, 0, 0, 0, 0, 0, 0, 0, 0, 0, null, null, null, null);

                            clsSalesTransactions.CommitAndDispose();
							clsEvent.AddEventLn("Done!");

                            // Added Jun 30, 2013
                            if (mclsTerminalDetails.IsParkingTerminal)
                            {
                                if (MessageBox.Show("Would you like to print the Parking Ticket?", "Print Parking Ticket", MessageBoxButtons.YesNo,  MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == System.Windows.Forms.DialogResult.Yes)
                                    PrintParkingTicket();
                            }

                            this.LoadOptions();

							MessageBox.Show("Transaction has been SUSPENDED. Press OK button to continue...", "RetailPlus", MessageBoxButtons.OK);

							boRetValue = true;
						}
						else { clsEvent.AddEventLn("Cancelled!"); }
					}
					catch (Exception ex)
					{ 
                        InsertErrorLogToFile(ex, "ERROR!!! Suspending transaction."); 
                    }
				}
				else
				{
					try
					{
						clsEvent.AddEvent("[" + lblCashier.Text + "] Suspending transaction no. " + lblTransNo.Text);

						Data.SalesTransactions clsSalesTransactions = new Data.SalesTransactions(mConnection, mTransaction);
                        mConnection = clsSalesTransactions.Connection; mTransaction = clsSalesTransactions.Transaction;

                        clsSalesTransactions.Suspend(mclsSalesTransactionDetails.TransactionID, mclsSalesTransactionDetails.SubTotal, mclsSalesTransactionDetails.NetSales);

                        InsertAuditLog(AccessTypes.SuspendTransaction, "Suspend transaction #: " + lblTransNo.Text + " @ Branch: " + mclsTerminalDetails.BranchDetails.BranchCode);						

						if (mclsTerminalDetails.AutoPrint == PrintingPreference.Auto)
							PrintReportFooterSection(true, TransactionStatus.Suspended, mclsSalesTransactionDetails.ItemSold, mclsSalesTransactionDetails.QuantitySold, mclsSalesTransactionDetails.SubTotal, mclsSalesTransactionDetails.Discount, mclsSalesTransactionDetails.Charge, 0, 0, 0, 0, 0, 0, 0, 0, 0, null, null, null, null);

                        clsSalesTransactions.CommitAndDispose();
						clsEvent.AddEventLn("Done!");

                        // Added Jun 30, 2013
                        if (mclsTerminalDetails.IsParkingTerminal)
                        {
                            if (MessageBox.Show("Would you like to print the Parking Ticket?", "Print Parking Ticket", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == System.Windows.Forms.DialogResult.Yes)
                                PrintParkingTicket();
                        }

                        this.LoadOptions();

						MessageBox.Show("Transaction has been SUSPENDED. Press OK button to continue...", "RetailPlus", MessageBoxButtons.OK, MessageBoxIcon.Information);
						boRetValue = true;
					}
					catch (Exception ex)
					{ 
                        InsertErrorLogToFile(ex, "ERROR!!! Suspending transaction."); 
                    }
				}
			}
			return boRetValue;
		}
		private bool SelectWaiter()
		{
			bool retValue = false;

			if (mclsTerminalDetails.AutoPrint == PrintingPreference.Auto && mboIsInTransaction)
			{
				MessageBox.Show("Sorry you cannot select a waiter when an item is already purchased.", "RetailPlus", MessageBoxButtons.OK, MessageBoxIcon.Warning);
				return retValue;
			}

			try
			{
				clsEvent.AddEvent("[" + lblCashier.Text + "] Selecting waiter.");

				WaiterSelectWnd WaiterWnd = new WaiterSelectWnd();
				WaiterWnd.ShowDialog(this);
				long iWaiterID = WaiterWnd.getWaiterID;
				string stWaiterName = WaiterWnd.getWaiterName;
				DialogResult result = WaiterWnd.Result;
				WaiterWnd.Close();
				WaiterWnd.Dispose();

				if (result == DialogResult.OK)
				{
					grpItems.Text = "Served by: " + stWaiterName;
					grpItems.Tag = iWaiterID.ToString();
					clsEvent.AddEventLn("Done! Selected Waiter: " + stWaiterName);

					if (mboIsInTransaction)
					{
						mclsSalesTransactionDetails.WaiterID = iWaiterID;
						mclsSalesTransactionDetails.WaiterName = stWaiterName;
                        Data.SalesTransactions clsSalesTransactions = new Data.SalesTransactions(mConnection, mTransaction);
                        mConnection = clsSalesTransactions.Connection; mTransaction = clsSalesTransactions.Transaction;

						clsSalesTransactions.UpdateWaiter(mclsSalesTransactionDetails.TransactionID, mclsSalesTransactionDetails.TransactionDate, iWaiterID, stWaiterName);
						clsSalesTransactions.CommitAndDispose();

						retValue = true;
					}
				}
				else { clsEvent.AddEventLn("Cancelled!"); }
			}
			catch (Exception ex)
			{ 
                InsertErrorLogToFile(ex, "ERROR!!! Selecting waiter."); 
            }

			return retValue;
		}
		private void ResumeTransaction()
		{
            if (!SuspendTransactionAndContinue()) return;

			if (mclsTerminalDetails.ShowOneTerminalSuspendedTransactions)
			{
                Data.SalesTransactions clsSalesTransactions = new Data.SalesTransactions(mConnection, mTransaction);
                mConnection = clsSalesTransactions.Connection; mTransaction = clsSalesTransactions.Transaction;

                int count = clsSalesTransactions.CountSuspended(mclsTerminalDetails.TerminalNo, mclsSalesTransactionDetails.CashierID, mclsTerminalDetails.BranchID);
                clsSalesTransactions.CommitAndDispose();

				if (count == 0)
				{
					MessageBox.Show("No suspended transaction found for this day.", "RetailPlus", MessageBoxButtons.OK);
					return;
				}
			}

			DialogResult loginresult = GetWriteAccessAndLogin(mclsSalesTransactionDetails.CashierID, AccessTypes.ResumeTransaction);

			if (loginresult == DialogResult.OK)
			{
				try
				{
					ResumeTransactionWnd ResumeWnd = new ResumeTransactionWnd();
                    ResumeWnd.TerminalDetails = mclsTerminalDetails;
					ResumeWnd.CashierID = mclsSalesTransactionDetails.CashierID;
					ResumeWnd.ShowDialog(this);
					DialogResult result = ResumeWnd.Result;
					Data.SalesTransactionDetails details = ResumeWnd.Details;
					ResumeWnd.Close();
					ResumeWnd.Dispose();

					if (result == DialogResult.OK)
					{
						clsEvent.AddEvent("[" + lblCashier.Text + "] Resuming transaction no. " + details.TransactionNo);

						mclsSalesTransactionDetails = details;

						if (mclsSalesTransactionDetails.TransactionStatus == TransactionStatus.Refund || mclsSalesTransactionDetails.TransactionType == TransactionTypes.POSRefund)
						{
							mboIsRefund = true;
							lblSubtotalName.Text = "SUBTOTAL: REFUND";
							lblOrderType.Visible = false;
						}
						lblOrderType.Text = mclsSalesTransactionDetails.OrderType.ToString("G").ToUpper();
						lblTransNo.Text = mclsSalesTransactionDetails.TransactionNo;
						lblTransNo.Tag = mclsSalesTransactionDetails.TransactionID.ToString();
						lblCustomer.Text = mclsSalesTransactionDetails.CustomerName;
						lblCustomer.Tag = mclsSalesTransactionDetails.CustomerID.ToString();
						lblAgent.Text = mclsSalesTransactionDetails.AgentName;
						lblAgent.Tag = mclsSalesTransactionDetails.AgentID.ToString();
						lblAgentPositionDepartment.Text = mclsSalesTransactionDetails.AgentPositionName;
						lblAgentPositionDepartment.Tag = mclsSalesTransactionDetails.AgentDepartmentName;
						grpItems.Text = "Served by: " + details.WaiterName;
						grpItems.Tag = mclsSalesTransactionDetails.WaiterID.ToString();

						lblTransDate.Text = mclsSalesTransactionDetails.TransactionDate.ToString("MMM. dd, yyyy hh:mm:ss tt");
                        mdteOverRidingPrintDate = mclsSalesTransactionDetails.TransactionDate;

						lblTransDiscount.Tag = mclsSalesTransactionDetails.TransDiscountType.ToString("d");
                        lblConsignment.Visible = mclsSalesTransactionDetails.isConsignment;

						// Aug 6, 2011 : Lemu
						// Put here from CloseTransaction
						try { mclsSalesTransactionDetails.CashierID = Convert.ToInt64(lblCashier.Tag); }
						catch { }
						mclsSalesTransactionDetails.CashierName = lblCashier.Text;

						LoadResumedItems(details.TransactionItems, false);

						mboIsInTransaction = true;

						InsertAuditLog(AccessTypes.ResumeTransaction, "Resume transaction #: " + lblTransNo.Text + " @ Branch: " + mclsTerminalDetails.BranchDetails.BranchCode);
						clsEvent.AddEventLn("[" + lblCashier.Text + "] Resuming transaction no. " + details.TransactionNo + " Done.", true);
					}
					else { clsEvent.AddEventLn("Cancelled!"); }


				}
				catch (Exception ex)
				{ 
                    InsertErrorLogToFile(ex, "ERROR!!! Resuming transaction."); 
                }
			}
		}
		private void VoidTransaction()
		{
			if (!mboIsInTransaction)
			{
				MessageBox.Show("Sorry you cannot void an empty transaction.", "RetailPlus", MessageBoxButtons.OK, MessageBoxIcon.Warning);
				return;
			}

			if (MessageBox.Show("Are you sure you want to void this transaction?", "RetailPlus", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
			{
				return;
			}

			DialogResult loginresult = GetWriteAccessAndLogin(mclsSalesTransactionDetails.CashierID, AccessTypes.VoidTransaction);

			if (loginresult == DialogResult.OK)
			{
				try
				{
					clsEvent.AddEventLn("[" + lblCashier.Text + "] Voiding transaction no. " + lblTransNo.Text, true);

                    mclsSalesTransactionDetails.TransactionStatus = TransactionStatus.Void;

					if (mclsTerminalDetails.AutoPrint == PrintingPreference.AskFirst)
						if (MessageBox.Show("Would you like to print this transaction?", "RetailPlus", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
							mclsTerminalDetails.AutoPrint = PrintingPreference.Normal;

                    // Sep 14, 2014 Control printing in mclsFilePrinter.Write
                    //if (mclsTerminalDetails.AutoPrint == PrintingPreference.Normal) 
                    //{
						//if (mclsTerminalDetails.IsPrinterAutoCutter)
						//    PrintReportPageHeaderSectionChecked(true);
						//else
							PrintReportHeadersSection(true);

						mboIsItemHeaderPrinted = true;

						foreach (System.Data.DataRow dr in ItemDataTable.Rows)
						{
							string stItemNo = "" + dr["ItemNo"].ToString();
							string stProductUnitCode = "" + dr["ProductUnitCode"].ToString();
							decimal decPrice = Convert.ToDecimal(dr["Price"]);
							decimal decDiscount = Convert.ToDecimal(dr["Discount"]);
							decimal decAmount = Convert.ToDecimal(dr["Amount"]);
							decimal decVAT = Convert.ToDecimal(dr["VAT"]);
							decimal decEVAT = Convert.ToDecimal(dr["EVAT"]);
							decimal decPromoApplied = Convert.ToDecimal(dr["PromoApplied"]);
							string stProductCode = "";
							decimal decQuantity = 0;

							if (dr["Quantity"].ToString().IndexOf("RETURN") != -1)
							{
								stProductCode = "" + dr["ProductCode"].ToString() + "-RET";
								decQuantity = Convert.ToDecimal(dr["Quantity"].ToString().Replace(" - RETURN", "").Trim());
								decAmount = -decAmount;
							}
							else if (dr["Quantity"].ToString() != "VOID")
							{
								stProductCode = "" + dr["ProductCode"].ToString();
								decQuantity = Convert.ToDecimal(dr["Quantity"]);
							}
                            if (dr["Quantity"].ToString().IndexOf("VOID") != -1)
                            {
                                if (mclsTerminalDetails.WillPrintVoidItem)
                                    if (mclsTerminalDetails.ReceiptType == TerminalReceiptType.Default)
                                        PrintItem(stItemNo, stProductCode, stProductUnitCode, decQuantity, decPrice, decDiscount, decPromoApplied, decAmount, decVAT, decEVAT);
                            }
                            else
                            {
                                if (mclsTerminalDetails.ReceiptType == TerminalReceiptType.Default)
                                    PrintItem(stItemNo, stProductCode, stProductUnitCode, decQuantity, decPrice, decDiscount, decPromoApplied, decAmount, decVAT, decEVAT);
                            }
								
                        //}
                        // Sep 14, 2014 Control printing in mclsFilePrinter.Write
					}

					Cursor.Current = Cursors.WaitCursor;

					Data.SalesTransactions clsSalesTransactions = new Data.SalesTransactions(mConnection, mTransaction);
                    mConnection = clsSalesTransactions.Connection; mTransaction = clsSalesTransactions.Transaction;

					// Added May 7, 2011 to Cater Reserved and Commit functionality    
					#region Reserved And Commit
                    //if (mclsTerminalDetails.ReservedAndCommit == true)
                    //{
						System.Data.DataTable dt = (System.Data.DataTable)dgItems.DataSource;
						for (int x = 0; x < dt.Rows.Count; x++)
						{
							dgItems.CurrentRowIndex = x;
							Data.SalesTransactionItemDetails Details = getCurrentRowItemDetails();

                            if (Details.TransactionItemStatus != TransactionItemStatus.Void)
                            {
                                Details.TransactionItemStatus = TransactionItemStatus.Void;
                                ReservedAndCommitItem(Details, Details.TransactionItemStatus);
                            }
						}
                    //}
					#endregion

					//UpdateTerminalReportDelegate updateterminalDel = new UpdateTerminalReportDelegate(UpdateTerminalReport);
                    UpdateTerminalReport(TransactionStatus.Void, mclsSalesTransactionDetails.ItemSold, mclsSalesTransactionDetails.QuantitySold, mclsSalesTransactionDetails.SubTotal, mclsSalesTransactionDetails.Discount, mclsSalesTransactionDetails.ItemsDiscount, mclsSalesTransactionDetails.Charge, mclsSalesTransactionDetails.VAT, mclsSalesTransactionDetails.VATableAmount, mclsSalesTransactionDetails.NonVATableAmount, mclsSalesTransactionDetails.VATExempt, mclsSalesTransactionDetails.EVAT, mclsSalesTransactionDetails.EVATableAmount, mclsSalesTransactionDetails.NonEVATableAmount, mclsSalesTransactionDetails.LocalTax, 0, 0, 0, 0, 0, 0, 0, PaymentTypes.NotYetAssigned);

					//UpdateCashierReportDelegate updatecashierDel = new UpdateCashierReportDelegate(UpdateCashierReport);
                    UpdateCashierReport(TransactionStatus.Void, mclsSalesTransactionDetails.ItemSold, mclsSalesTransactionDetails.QuantitySold, mclsSalesTransactionDetails.SubTotal, mclsSalesTransactionDetails.Discount, mclsSalesTransactionDetails.ItemsDiscount, mclsSalesTransactionDetails.Charge, mclsSalesTransactionDetails.VAT, mclsSalesTransactionDetails.VATableAmount, mclsSalesTransactionDetails.NonVATableAmount, mclsSalesTransactionDetails.VATExempt, mclsSalesTransactionDetails.EVAT, mclsSalesTransactionDetails.EVATableAmount, mclsSalesTransactionDetails.NonEVATableAmount, mclsSalesTransactionDetails.LocalTax, 0, 0, 0, 0, 0, 0, 0, PaymentTypes.NotYetAssigned);

                    clsSalesTransactions.Void(mclsSalesTransactionDetails.TransactionID, mclsSalesTransactionDetails.ItemSold, mclsSalesTransactionDetails.QuantitySold, mclsSalesTransactionDetails.SubTotal, mclsSalesTransactionDetails.NetSales, mclsSalesTransactionDetails.ItemsDiscount, mclsSalesTransactionDetails.Discount, mclsSalesTransactionDetails.SNRDiscount, mclsSalesTransactionDetails.PWDDiscount, mclsSalesTransactionDetails.OtherDiscount, mclsSalesTransactionDetails.TransDiscount, mclsSalesTransactionDetails.TransDiscountType, mclsSalesTransactionDetails.VAT, mclsSalesTransactionDetails.VATableAmount, mclsSalesTransactionDetails.NonVATableAmount, mclsSalesTransactionDetails.VATExempt, mclsSalesTransactionDetails.EVAT, mclsSalesTransactionDetails.EVATableAmount, mclsSalesTransactionDetails.NonEVATableAmount, mclsSalesTransactionDetails.LocalTax, mclsSalesTransactionDetails.Charge, mclsSalesTransactionDetails.CashierID, mclsSalesTransactionDetails.CashierName);
					
                    clsSalesTransactions.UpdateTerminalNo(mclsSalesTransactionDetails.TransactionID, lblTerminalNo.Text);

                    // Sep 24, 2014 : update back the LastCheckInDate to min date
                    Data.Contacts clsContact = new Data.Contacts(mConnection, mTransaction);
                    mConnection = clsContact.Connection; mTransaction = clsContact.Transaction;

                    clsContact.UpdateLastCheckInDate(mclsSalesTransactionDetails.CustomerID, Constants.C_DATE_MIN_VALUE);

                    try
                    {
                        clsSalesTransactions.CommitAndDispose();
                    }
                    catch
                    {
                        mclsSalesTransactionDetails.TransactionStatus = TransactionStatus.Open;
                    }

                    InsertAuditLog(AccessTypes.VoidTransaction, "VOID transaction #".PadRight(15) + ":" + lblTransNo.Text + " @ Branch: " + mclsTerminalDetails.BranchDetails.BranchCode);

					clsEvent.AddEventLn("Done transaction no. " + lblTransNo.Text + " has been void.", true);

					if (mclsTerminalDetails.ReceiptType == TerminalReceiptType.Default)
					{
						PrintReportFooterSection(true, TransactionStatus.Void, mclsSalesTransactionDetails.ItemSold, mclsSalesTransactionDetails.QuantitySold, mclsSalesTransactionDetails.SubTotal, mclsSalesTransactionDetails.Discount, mclsSalesTransactionDetails.Charge, 0, 0, 0, 0, 0, 0, 0, 0, 0, null, null, null, null);
					}
					else if (mclsTerminalDetails.ReceiptType == TerminalReceiptType.SalesInvoice)
					{
						PrintSalesInvoice();
					}
					else if (mclsTerminalDetails.ReceiptType == TerminalReceiptType.DeliveryReceipt)
					{
						PrintDeliveryReceipt();
					}
					else if (mclsTerminalDetails.ReceiptType == TerminalReceiptType.SalesInvoiceAndDR)
					{
						PrintSalesInvoice();
						PrintDeliveryReceipt();
					}
                    //Added April 12, 2014
                    else if (mclsTerminalDetails.ReceiptType == TerminalReceiptType.OfficialReceiptAndDR)
                    {
                        PrintOfficialReceipt();
                        PrintDeliveryReceipt();
                    }
					//Added February 10, 2010
					else if (mclsTerminalDetails.ReceiptType == TerminalReceiptType.SalesInvoiceForLX300Printer)
					{
						PrintSalesInvoiceToLX(TerminalReceiptType.SalesInvoiceForLX300Printer);
					}
					//Added May 11, 2010
					else if (mclsTerminalDetails.ReceiptType == TerminalReceiptType.SalesInvoiceOrDR)
					{
						PrintDeliveryReceipt();
					}
					//Added January 17, 2011
					else if (mclsTerminalDetails.ReceiptType == TerminalReceiptType.SalesInvoiceForLX300PlusPrinter)
					{
						PrintSalesInvoiceToLX(TerminalReceiptType.SalesInvoiceForLX300PlusPrinter);
					}
					//Added February 22, 2011
					else if (mclsTerminalDetails.ReceiptType == TerminalReceiptType.SalesInvoiceForLX300PlusAmazon)
					{
						PrintSalesInvoiceToLX(TerminalReceiptType.SalesInvoiceForLX300PlusAmazon); //8.5inc x 7inch
					}

                    this.LoadOptions();

					MessageBox.Show("Transaction has been VOID. Press OK button to continue...", "RetailPlus", MessageBoxButtons.OK, MessageBoxIcon.Information);
					Cursor.Current = Cursors.Default;
				}
				catch (Exception ex)
				{ 
                    InsertErrorLogToFile(ex, "ERROR!!! Voiding transaction."); 
                }
			}
		}
		private void WithHold()
		{
            if (!SuspendTransactionAndContinue()) return;

			DialogResult loginresult = GetWriteAccessAndLogin(mclsSalesTransactionDetails.CashierID, AccessTypes.Withhold);

			if (loginresult == DialogResult.OK)
			{
				try
				{
					clsEvent.AddEvent("[" + lblCashier.Text + "] WithHolding amount.");

					WithholdWnd frmWithHoldWnd = new WithholdWnd();
                    frmWithHoldWnd.TerminalDetails = mclsTerminalDetails;
					frmWithHoldWnd.CashierID = mclsSalesTransactionDetails.CashierID;
					frmWithHoldWnd.ShowDialog(this);
					DialogResult result = frmWithHoldWnd.Result;
					Data.WithholdDetails clsWithHoldDetails = frmWithHoldWnd.WithHoldDetails;
					frmWithHoldWnd.Close();
					frmWithHoldWnd.Dispose();

					if (result == DialogResult.OK)
					{
						Cursor.Current = Cursors.WaitCursor;

                        Data.Withhold clsWithHold = new Data.Withhold(mConnection, mTransaction);
                        mConnection = clsWithHold.Connection; mTransaction = clsWithHold.Transaction;

						clsWithHold.Insert(clsWithHoldDetails);
                        clsWithHold.CommitAndDispose();

                        InsertAuditLog(AccessTypes.Withhold, "WithHold payment: type='" + clsWithHoldDetails.PaymentType.ToString("G") + "' amount='" + clsWithHoldDetails.Amount.ToString(",##0.#0") + "'" + " @ Branch: " + mclsTerminalDetails.BranchDetails.BranchCode);

						//PrintWithHoldDelegate printwithholDel = new PrintWithHoldDelegate(PrintWithHold);
						//printwithholDel.BeginInvoke(clsWithHoldDetails, null, null);
						PrintWithHold(clsWithHoldDetails);

						// Sep 28, 2011 : Lemu 
						// As per request of houseware plaze. Print a second copy
						//printwithholDel = new PrintWithHoldDelegate(PrintWithHold);
						//printwithholDel.BeginInvoke(clsWithHoldDetails, null, null);
						System.Threading.Thread.Sleep(100);
						PrintWithHold(clsWithHoldDetails);

						clsEvent.AddEventLn("Done! type=" + clsWithHoldDetails.PaymentType.ToString("G") + " amount=" + clsWithHoldDetails.Amount.ToString("#,###.#0"));

						Cursor.Current = Cursors.Default;
					}
					else { clsEvent.AddEventLn("Cancelled!"); }

				}
				catch (Exception ex)
				{ 
                    InsertErrorLogToFile(ex, "ERROR!!! Withholding amount."); 
                }
			}
		}
		private void Disburse()
		{
            if (!SuspendTransactionAndContinue()) return;

			DialogResult loginresult = GetWriteAccessAndLogin(mclsSalesTransactionDetails.CashierID, AccessTypes.Disburse);

			if (loginresult == DialogResult.OK)
			{
				try
				{
					clsEvent.AddEvent("[" + lblCashier.Text + "] Disbursing amount.");

					DisburseWnd frmDisburseWnd = new DisburseWnd();
                    frmDisburseWnd.TerminalDetails = mclsTerminalDetails;
					frmDisburseWnd.CashierID = mclsSalesTransactionDetails.CashierID;
					frmDisburseWnd.ShowDialog(this);
					DialogResult result = frmDisburseWnd.Result;
					Data.DisburseDetails clsDisburseDetails = frmDisburseWnd.DisburseDetails;
					frmDisburseWnd.Close();
					frmDisburseWnd.Dispose();

					if (result == DialogResult.OK)
					{
						Cursor.Current = Cursors.WaitCursor;
						
						Data.Disburses clsDisburse = new Data.Disburses(mConnection, mTransaction);
                        mConnection = clsDisburse.Connection; mTransaction = clsDisburse.Transaction;

						clsDisburse.Insert(clsDisburseDetails);
                        clsDisburse.CommitAndDispose();

                        InsertAuditLog(AccessTypes.Disburse, "Disburse: type='" + clsDisburseDetails.PaymentType.ToString("G") + "' amount='" + clsDisburseDetails.Amount.ToString(",##0.#0") + "'" + " @ Branch: " + mclsTerminalDetails.BranchDetails.BranchCode);

						//PrintDisbursementDelegate printdisburseDel = new PrintDisbursementDelegate(PrintDisbursement);
						//printdisburseDel.BeginInvoke(clsDisburseDetails, null, null);
						PrintDisbursement(clsDisburseDetails);

						// Sep 28, 2011 : Lemu 
						// As per request of houseware plaze. Print a second copy
						//printdisburseDel = new PrintDisbursementDelegate(PrintDisbursement);
						//printdisburseDel.BeginInvoke(clsDisburseDetails, null, null);
						System.Threading.Thread.Sleep(100);
						PrintDisbursement(clsDisburseDetails);

						clsEvent.AddEventLn("Done! type=" + clsDisburseDetails.PaymentType.ToString("G") + " amount=" + clsDisburseDetails.Amount.ToString("#,###.#0"));

						Cursor.Current = Cursors.Default;
					}
					else { clsEvent.AddEventLn("Cancelled!"); }

				}
				catch (Exception ex)
				{ 
                    InsertErrorLogToFile(ex, "ERROR!!! Disbursing amount."); 
                }
			}
		}
		private void PaidOut()
		{
            if (!SuspendTransactionAndContinue()) return;

			DialogResult loginresult = GetWriteAccessAndLogin(mclsSalesTransactionDetails.CashierID, AccessTypes.PaidOut);

			if (loginresult == DialogResult.OK)
			{
				try
				{
					clsEvent.AddEvent("[" + lblCashier.Text + "] Issuing paid-out amount.");

					PaidOutWnd frmPaidOutWnd = new PaidOutWnd();
                    frmPaidOutWnd.TerminalDetails = mclsTerminalDetails;
					frmPaidOutWnd.CashierID = mclsSalesTransactionDetails.CashierID;
					frmPaidOutWnd.ShowDialog(this);
					DialogResult result = frmPaidOutWnd.Result;
					Data.PaidOutDetails clsPaidOutDetails = frmPaidOutWnd.PaidOutDetails;
					frmPaidOutWnd.Close();
					frmPaidOutWnd.Dispose();

					if (result == DialogResult.OK)
					{
						Cursor.Current = Cursors.WaitCursor;
						
						Data.PaidOut clsPaidOut = new Data.PaidOut(mConnection, mTransaction);
                        mConnection = clsPaidOut.Connection; mTransaction = clsPaidOut.Transaction;

						clsPaidOut.Insert(clsPaidOutDetails);
                        clsPaidOut.CommitAndDispose();

                        InsertAuditLog(AccessTypes.PaidOut, "Paid-out: type='" + clsPaidOutDetails.PaymentType.ToString("G") + "' amount='" + clsPaidOutDetails.Amount.ToString(",##0.#0") + "'" + " @ Branch: " + mclsTerminalDetails.BranchDetails.BranchCode);

						//PrintPaidOutDelegate paidoutDel = new PrintPaidOutDelegate(PrintPaidOut);
						//paidoutDel.BeginInvoke(clsPaidOutDetails, null, null);
						PrintPaidOut(clsPaidOutDetails);

						// Sep 28, 2011 : Lemu 
						// As per request of houseware plaze. Print a second copy
						//paidoutDel = new PrintPaidOutDelegate(PrintPaidOut);
						//paidoutDel.BeginInvoke(clsPaidOutDetails, null, null);
						System.Threading.Thread.Sleep(100);
						PrintPaidOut(clsPaidOutDetails);

						clsEvent.AddEventLn("Done! type=" + clsPaidOutDetails.PaymentType.ToString("G") + " amount=" + clsPaidOutDetails.Amount.ToString("#,###.#0"));

						Cursor.Current = Cursors.Default;
					}
					else { clsEvent.AddEventLn("Cancelled!"); }
				}
				catch (Exception ex)
				{ 
                    InsertErrorLogToFile(ex, "ERROR!!! Issuing paid out."); 
                }
			}
		}
		private void Deposit()
		{
            if (!SuspendTransactionAndContinue()) return;

			DialogResult loginresult = GetWriteAccessAndLogin(mclsSalesTransactionDetails.CashierID, AccessTypes.Deposit);

			if (loginresult == DialogResult.OK)
			{
				try
				{
					clsEvent.AddEvent("[" + lblCashier.Text + "] Depositing amount.");

					DepositWnd frmDepositWnd = new DepositWnd();
                    frmDepositWnd.TerminalDetails = mclsTerminalDetails;
					frmDepositWnd.CashierID = mclsSalesTransactionDetails.CashierID;
					frmDepositWnd.ShowDialog(this);
					DialogResult result = frmDepositWnd.Result;
					Data.DepositDetails clsDepositDetails = frmDepositWnd.DepositDetails;
					frmDepositWnd.Close();
					frmDepositWnd.Dispose();

					if (result == DialogResult.OK)
					{
						Cursor.Current = Cursors.WaitCursor;
						
						Data.Deposits clsDeposit = new Data.Deposits(mConnection, mTransaction);
                        mConnection = clsDeposit.Connection; mTransaction = clsDeposit.Transaction;

						clsDeposit.Insert(clsDepositDetails);

						Data.Contacts clsContact = new Data.Contacts(mConnection, mTransaction);
                        mConnection = clsContact.Connection; mTransaction = clsContact.Transaction;

						clsContact.AddDebit(clsDepositDetails.ContactID, clsDepositDetails.Amount);
                        clsDeposit.CommitAndDispose();

                        InsertAuditLog(AccessTypes.Deposit, "Deposit: type='" + clsDepositDetails.PaymentType.ToString("G") + "' amount='" + clsDepositDetails.Amount.ToString(",##0.#0") + "'" + " @ Branch: " + mclsTerminalDetails.BranchDetails.BranchCode);

                        PrintDeposit(clsDepositDetails);
						//PrintDepositDelegate printdepositDel = new PrintDepositDelegate(PrintDeposit);
						//printdepositDel.BeginInvoke(clsDepositDetails, null, null);

						clsEvent.AddEventLn("Done! type=" + clsDepositDetails.PaymentType.ToString("G") + " amount=" + clsDepositDetails.Amount.ToString("#,###.#0"));

						Cursor.Current = Cursors.Default;
					}
					else { clsEvent.AddEventLn("Cancelled!"); }

				}
				catch (Exception ex)
				{ 
                    InsertErrorLogToFile(ex, "ERROR!!! Receiving customer deposit."); 
                }
			}
		}
		private void ShowPrintWindow()
		{
            if (!SuspendTransactionAndContinue()) return;

			// Dec 01, 2008      Lemuel E. Aceron
			// added the IsCashCountInitialized for 1 time 
			// Cash count every printing of report.
			if (mclsTerminalDetails.CashCountBeforeReport)
			{ if (!mboIsCashCountInitialized) { if (!CashCount()) return; } }

			ReportsWnd reports = new ReportsWnd();
			reports.CashierID = mclsSalesTransactionDetails.CashierID;
			reports.ShowDialog(this);
			DialogResult reportsresult = reports.Result;
			Keys KeyData = reports.KeyData;
			reports.Close();
			reports.Dispose();

			switch (KeyData)
			{
				case Keys.F1:
					ReprintTransaction();
					break;

				case Keys.F2:
					PrintTerminalReport();
					break;

				case Keys.F3:
					PrintCashiersReport();
					break;

				case Keys.F4:
					PrintTerminalXRead();
					break;

				case Keys.F5:
					PrintHourly();
					break;

				case Keys.F6:
					PrintGroup();
					break;

				case Keys.F7:
					PrintPLU();
					break;

				case Keys.F8:
					PrintEJournal();
					break;

				case Keys.F9:
					ReprintZRead();
					break;

				case Keys.F10:
					PrintPLUPerOrderSlipPrinter();
					break;

				case Keys.F11:
					ReprintDeliveryReceipt();
					break;

                case Keys.F12:
                    PrintPLUGroup();
                    break;
			}
		}
		private void ShowMallForwarder()
		{
            if (!SuspendTransactionAndContinue()) return;

			DialogResult loginresult = GetWriteAccessAndLogin(mclsSalesTransactionDetails.CashierID, AccessTypes.MallForwarder);

			if (loginresult == DialogResult.OK)
			{
				try
				{
					clsEvent.AddEventLn("Mall Forwarder showing window to [" + lblCashier.Text + "]...", true);

					ForwarderWnd clsForwarderWnd = new ForwarderWnd();
					clsForwarderWnd.TerminalDetails = mclsTerminalDetails;
					clsForwarderWnd.ShowDialog(this);
					DialogResult result = clsForwarderWnd.Result;
					clsForwarderWnd.Close();
					clsForwarderWnd.Dispose();

					if (result == DialogResult.OK)
					{
						ProcessMallForwarder(clsForwarderWnd.DateLastInitialized, true);
					}
					else { clsEvent.AddEventLn("Mall Forwarder: Cancelled!", true); }
				}
				catch (Exception ex)
				{ 
                    InsertErrorLogToFile(ex); 
                }
				Cursor.Current = Cursors.Default;
			}
		}
		private delegate void ProcessMallForwarderDelegate(DateTime pvtDateToProcess, bool ShowSuccessMessageBox);
		private void ProcessMallForwarder(DateTime pvtDateToProcess, bool ShowSuccessMessageBox)
		{
			try
			{
				switch (CONFIG.MallCode.ToUpper())
				{
					case MallCodes.NA:
						break;
					case MallCodes.AYALA:
						AceSoft.RetailPlus.Forwarder.AyalaDetails clsAyalaDetails = new AceSoft.RetailPlus.Forwarder.AyalaDetails();
						clsAyalaDetails.TenantCode = AYALA_CONFIG.TenantCode;
						clsAyalaDetails.TenantName = AYALA_CONFIG.TenantName;
						clsAyalaDetails.OutputDirectory = AYALA_CONFIG.OutputDirectory;
						clsAyalaDetails.FTPIPAddress = CONFIG.FTPIPAddress;
						clsAyalaDetails.FTPUsername = CONFIG.FTPUsername;
						clsAyalaDetails.FTPPassword = CONFIG.FTPPassword;
						clsAyalaDetails.FTPDirectory = CONFIG.FTPDirectory;

						AceSoft.RetailPlus.Forwarder.Ayala clsAyala = new AceSoft.RetailPlus.Forwarder.Ayala();
						clsAyala.AyalaDetails = clsAyalaDetails;
						clsAyala.CreateAndTransferFile(mclsTerminalDetails.BranchID, mclsTerminalDetails.TerminalNo, pvtDateToProcess);
						clsAyala.CommitAndDispose();
						clsEvent.AddEventLn("Mall Forwarder: Done!", true);
						if (ShowSuccessMessageBox) MessageBox.Show("Sales file successfully sent to Ayala server.", "Mall Forwarder", MessageBoxButtons.OK, MessageBoxIcon.Information);
						break;
					case MallCodes.RLC:
						AceSoft.RetailPlus.Forwarder.RLCDetails clsRLCDetails = new AceSoft.RetailPlus.Forwarder.RLCDetails();
						clsRLCDetails.TenantCode = RLC_CONFIG.TenantCode;
						clsRLCDetails.TenantName = RLC_CONFIG.TenantName;
						clsRLCDetails.OutputDirectory = RLC_CONFIG.OutputDirectory;
						clsRLCDetails.FTPIPAddress = CONFIG.FTPIPAddress;
						clsRLCDetails.FTPUsername = CONFIG.FTPUsername;
						clsRLCDetails.FTPPassword = CONFIG.FTPPassword;
						clsRLCDetails.FTPDirectory = CONFIG.FTPDirectory;

						AceSoft.RetailPlus.Forwarder.RLC clsRLC = new AceSoft.RetailPlus.Forwarder.RLC();
						clsRLC.RLCDetails = clsRLCDetails;
						clsRLC.CreateAndTransferFile(mclsTerminalDetails.BranchDetails.BranchID, mclsTerminalDetails.TerminalNo, pvtDateToProcess);
						clsEvent.AddEventLn("Mall Forwarder: Done!", true);
						if (ShowSuccessMessageBox) MessageBox.Show("Sales file successfully sent to RLC server.", "Mall Forwarder", MessageBoxButtons.OK, MessageBoxIcon.Information);
						break;
					case MallCodes.FSI:
						AceSoft.RetailPlus.Forwarder.FSIDetails clsFSIDetails = new AceSoft.RetailPlus.Forwarder.FSIDetails();
						clsFSIDetails.TenantCode = FSI_CONFIG.TenantCode;
						clsFSIDetails.TenantName = FSI_CONFIG.TenantName;
						clsFSIDetails.OutputDirectory = FSI_CONFIG.OutputDirectory;
						clsFSIDetails.SalesTypeCode = FSI_CONFIG.SalesTypeCode;
						clsFSIDetails.FTPIPAddress = CONFIG.FTPIPAddress;
						clsFSIDetails.FTPUsername = CONFIG.FTPUsername;
						clsFSIDetails.FTPPassword = CONFIG.FTPPassword;
						clsFSIDetails.FTPDirectory = CONFIG.FTPDirectory;

						AceSoft.RetailPlus.Forwarder.FSI clsFSI = new AceSoft.RetailPlus.Forwarder.FSI();
						clsFSI.FSIDetails = clsFSIDetails;
						clsFSI.CreateAndTransferFile(mclsTerminalDetails.BranchID, mclsTerminalDetails.TerminalNo, pvtDateToProcess);
						clsEvent.AddEventLn("Mall Forwarder: Done!", true);
						if (ShowSuccessMessageBox) MessageBox.Show("Sales file successfully sent to FSI server.", "Mall Forwarder", MessageBoxButtons.OK, MessageBoxIcon.Information);
						break;
					default:
						MessageBox.Show("Sorry no applicable mall code for".PadRight(15) + ":" + CONFIG.MallCode, "Mall Forwarder", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
						break;
				}
			}
			catch (Exception ex)
			{
                InsertErrorLogToFile(ex, "ERROR!!! Mall Forwarder: " + CONFIG.MallCode.ToUpper());
				MessageBox.Show(ex.Message, "Mall Forwarder", MessageBoxButtons.OK, MessageBoxIcon.Information);
			}
		}
		private void VoidItem()
		{
			Data.SalesTransactionItemDetails Details;

			int iRow = dgItems.CurrentRowIndex;

			if (iRow >= 0)
			{
				if (dgItems[iRow, 8].ToString() != "VOID")
				{
					DialogResult loginresult = GetWriteAccessAndLogin(mclsSalesTransactionDetails.CashierID, AccessTypes.VoidItem);

					if (loginresult == DialogResult.OK)
					{
						if (mclsTerminalDetails.ItemVoidConfirmation)
						{
							if (MessageBox.Show("Are you sure you want to void this item?", "RetailPlus", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
							{
								return;
							}
						}

                        Details = getCurrentRowItemDetails();
                        clsEvent.AddEvent("[" + lblCashier.Text + "] Voiding item no. " + Details.ItemNo + "".PadRight(15) + ":" + Details.Description + ".");
						try
						{
							// override the transaction item status
							TransactionItemStatus _previousTransactionItemStatus = Details.TransactionItemStatus;

							Details.TransactionItemStatus = TransactionItemStatus.Void;

                            Data.SalesTransactions clsSalesTransactions = new Data.SalesTransactions(mConnection, mTransaction);
                            mConnection = clsSalesTransactions.Connection; mTransaction = clsSalesTransactions.Transaction;

							clsSalesTransactions.VoidItem(Details.TransactionItemsID, mclsSalesTransactionDetails.TransactionDate);
                            clsEvent.AddEventLn("Voiding item #: " + Details.ItemNo + "".PadRight(15) + ":" + Details.Description + ".", true);

							// Added May 7, 2011 to Cater Reserved and Commit functionality    
							ReservedAndCommitItem(Details, _previousTransactionItemStatus);
                            clsSalesTransactions.CommitAndDispose();

                            InsertAuditLog(AccessTypes.VoidItem, "Voiding item #: " + Details.ItemNo + "".PadRight(15) + ":" + Details.Description + "." + " @ Branch: " + mclsTerminalDetails.BranchDetails.BranchCode);

							dgItems[iRow, 8] = "VOID";
							dgItems[iRow, 9] = "0.00";
							dgItems[iRow, 10] = "0.00";
							dgItems[iRow, 11] = "0.00";
							dgItems[iRow, 13] = "0.00";

							SetItemDetails();
							
							clsEvent.AddEventLn("Done!");
							
							ComputeSubTotal(); setTotalDetails();

							try
							{
								DisplayItemToTurretDelegate DisplayItemToTurretDel = new DisplayItemToTurretDelegate(DisplayItemToTurret);
								DisplayItemToTurretDel.BeginInvoke("VOID-" + Details.ProductCode, Details.ProductUnitCode, Details.Quantity, Details.Price, Details.Discount, Details.PromoApplied, Details.Amount, Details.VAT, Details.EVAT, null, null);
							}
							catch { }
							if (mclsTerminalDetails.AutoPrint == PrintingPreference.Auto)
							{
								PrintItemDelegate PrintItemDel = new PrintItemDelegate(PrintItem);
								PrintItemDel.BeginInvoke(Details.ItemNo, Details.ProductCode + " - VOID ", Details.ProductUnitCode, Details.Quantity, Details.Price, Details.Discount, Details.PromoApplied, Details.Amount, Details.VAT, Details.EVAT, null, null);
								//PrintItemDel.BeginInvoke(Details.ProductCode + " - VOID ", Details.ProductUnitCode, Details.Quantity, Details.Price, Details.Discount, Details.PromoApplied, Details.Amount, Details.VAT, Details.EVAT, null, null);
							}

						}
						catch (Exception ex)
						{
                            InsertErrorLogToFile(ex, "ERROR!!! Voiding item." + Details.ItemNo + "".PadRight(15) + ":" + Details.Description); 
                        }
					}
				}
			}
		}
		private bool CashCount()
		{
			bool boRetValue = false;

            if (!SuspendTransactionAndContinue()) return boRetValue;

			DialogResult loginresult = GetWriteAccessAndLogin(mclsSalesTransactionDetails.CashierID, AccessTypes.CashCount);

			if (loginresult == DialogResult.OK)
			{
				try
				{
					clsEvent.AddEventLn("[" + lblCashier.Text + "] Issuing cash count.", true);

					CashCountWnd frmCashCountWnd = new CashCountWnd();
                    frmCashCountWnd.TerminalDetails = mclsTerminalDetails;
					frmCashCountWnd.SalesTransactionDetails = mclsSalesTransactionDetails;
					frmCashCountWnd.ShowDialog(this);
					DialogResult result = frmCashCountWnd.Result;
					Data.CashCountDetails[] clsCashCountDetails = frmCashCountWnd.Details;
					decimal Amount = frmCashCountWnd.Amount;
					frmCashCountWnd.Close();
					frmCashCountWnd.Dispose();

					if (result == DialogResult.OK)
					{
						if (clsCashCountDetails != null)
						{
                            // Sep 3, 2014 Move it here from the CashCountWnd
                            Data.CashCounts clsCashCount = new Data.CashCounts();
                            mConnection = clsCashCount.Connection; mTransaction = clsCashCount.Transaction;

                            clsCashCount.Insert(clsCashCountDetails);

                            Data.Terminal clsTerminal = new Data.Terminal(mConnection, mTransaction);
                            clsTerminal.UpdateIsCashCountInitialized(mclsTerminalDetails.BranchDetails.BranchID, mclsTerminalDetails.TerminalNo, mclsSalesTransactionDetails.CashierID, true);

                            clsCashCount.CommitAndDispose();

							// Dec 01, 2008      Lemuel E. Aceron
							// added the IsCashCountInitialized for 1 time 
							// Cash count every printing of report.
							mboIsCashCountInitialized = true;

							//PrintCashCountDelegate printcashcountDel = new PrintCashCountDelegate(PrintCashCount);
							//printcashcountDel.BeginInvoke(clsCashCountDetails, null, null);
							PrintCashCount(clsCashCountDetails);

							// Sep 28, 2011 : Lemu 
							// As per request of houseware plaze. Print a second copy
							// printcashcountDel = new PrintCashCountDelegate(PrintCashCount);
							// printcashcountDel.BeginInvoke(clsCashCountDetails, null, null);
							System.Threading.Thread.Sleep(100);
							PrintCashCount(clsCashCountDetails);
						}
						InsertAuditLog(AccessTypes.CashCount, "Issue cash count. amount=" + Amount.ToString("#,###.#0") + " @ Branch: " + mclsTerminalDetails.BranchDetails.BranchCode);
						clsEvent.AddEventLn("Done! amount=" + Amount.ToString("#,###.#0"), true);

						boRetValue = true;

                        MessageBox.Show("Cash Count has been initialized...", "RetailPlus", MessageBoxButtons.OK, MessageBoxIcon.Information);
					}
					else { clsEvent.AddEventLn("Cancelled!", true); }
				}
				catch (Exception ex)
				{
                    InsertErrorLogToFile(ex, "ERROR!!! Issuing cash count"); 
                }
			}
			return boRetValue;
		}
		private void Float()
		{
            if (!SuspendTransactionAndContinue()) return;

			DialogResult loginresult = GetWriteAccessAndLogin(mclsSalesTransactionDetails.CashierID, AccessTypes.EnterFloat);

			if (loginresult == DialogResult.OK)
			{
				try
				{
					clsEvent.AddEventLn("[" + lblCashier.Text + "] Entering beginning balance...", true);

					BalanceWnd clsBalanceWnd = new BalanceWnd();
                    clsBalanceWnd.TerminalDetails = mclsTerminalDetails;
					clsBalanceWnd.CashierID = mclsSalesTransactionDetails.CashierID;
					clsBalanceWnd.ShowDialog(this);
					DialogResult balanceResult = clsBalanceWnd.Result;
					decimal Amount = clsBalanceWnd.Amount;
					clsBalanceWnd.Close();
					clsBalanceWnd.Dispose();

					if (balanceResult == DialogResult.OK)
					{
						OpenDrawerDelegate opendrawerDel = new OpenDrawerDelegate(OpenDrawer);
						Invoke(opendrawerDel);
						InsertAuditLog(AccessTypes.EnterFloat, "Issue beginning balance. amount=" + Amount.ToString("#,###.#0") + " @ Branch: " + mclsTerminalDetails.BranchDetails.BranchCode);
						clsEvent.AddEventLn("Entering beginning balance Done! amount=" + Amount.ToString("#,###.#0"), true);
					}
					else { clsEvent.AddEventLn("Entering beginning balance cancelled!"); }
				}
				catch (Exception ex)
				{ 
                    InsertErrorLogToFile(ex, "ERROR!!! Issuing beginning balance."); 
                }
			}
		}
        private void InitializeZRead(bool boWithOutTF)
		{
            if (!SuspendTransactionAndContinue()) return;

			DialogResult loginresult = GetWriteAccessAndLogin(mclsSalesTransactionDetails.CashierID, AccessTypes.InitializeZRead);

			if (loginresult == DialogResult.OK)
			{
				Data.SalesTransactions clsSales = new Data.SalesTransactions(mConnection, mTransaction);
                mConnection = clsSales.Connection; mTransaction = clsSales.Transaction;

				int count = clsSales.CountSuspended(mclsTerminalDetails.TerminalNo, 0, mclsTerminalDetails.BranchID);
				clsSales.CommitAndDispose();

				if (count != 0)
				{
					MessageBox.Show("Sorry there are suspended transactions for this day. Please CLOSE the transactions first...", "RetailPlus", MessageBoxButtons.OK);
					return;
				}
				if (IsDateLastInitializationOK() == false)
				{
					return;
				}
				if (MessageBox.Show("Warning!!! Z-Read will be initialized...Press OK to continue.", "RetailPlus", MessageBoxButtons.OKCancel , MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.OK)
				{
					Cursor.Current = Cursors.WaitCursor;

					try
					{
						clsEvent.AddEvent("[" + lblCashier.Text + "] Initializing Beginning balance.",true);
						PrintZRead(true);

						Data.TerminalReport clsTerminalReport = new Data.TerminalReport(mConnection, mTransaction);
                        mConnection = clsTerminalReport.Connection; mTransaction = clsTerminalReport.Transaction;

						// Dec 01, 2008      Lemuel E. Aceron
						// added the IsCashCountInitialized for
						// 1 time Cash count every printing of report.
						Data.Terminal clsTerminal = new Data.Terminal(mConnection, mTransaction);
                        mConnection = clsTerminal.Connection; mTransaction = clsTerminal.Transaction;

						clsTerminal.UpdateIsCashCountInitialized(mclsTerminalDetails.BranchID, mclsTerminalDetails.TerminalNo, mclsSalesTransactionDetails.CashierID, false);

						//initialize Z-Read
                        clsTerminalReport.InitializeZRead(mclsTerminalDetails.BranchID, mclsTerminalDetails.TerminalNo, mclsSalesTransactionDetails.CashierName, Constants.C_DATE_MIN_VALUE, boWithOutTF);

                        InsertAuditLog(AccessTypes.InitializeZRead, "Initialize Z-Read." + " @ Branch: " + mclsTerminalDetails.BranchDetails.BranchCode);

						DateTime dteMAXDateLastInitialized = DateTime.MinValue;

						// May 21, 2009      Lemuel E. Aceron
						// added the for auto FTP of file for RLC
						// get the maxdatelastinitialized
						if (CONFIG.MallCode.ToUpper() == MallCodes.RLC)
						{
							Data.TerminalReportHistory clsTerminalReportHistory = new Data.TerminalReportHistory(mConnection, mTransaction);
                            mConnection = clsTerminalReportHistory.Connection; mTransaction = clsTerminalReportHistory.Transaction;

							dteMAXDateLastInitialized = clsTerminalReportHistory.MINDateLastInitialized(mclsTerminalDetails.BranchID, mclsTerminalDetails.TerminalNo, DateTime.Now);
						}

						clsTerminalReport.CommitAndDispose();

						clsEvent.AddEventLn("Done!");

						MessageBox.Show("Z-Read has been initialized...", "RetailPlus", MessageBoxButtons.OK, MessageBoxIcon.Information);

						// May 21, 2009      Lemuel E. Aceron
						// added the for auto FTP of file for RLC
						// send the data to RLC
						if (CONFIG.MallCode.ToUpper() == MallCodes.RLC)
							ProcessMallForwarder(dteMAXDateLastInitialized, true);
						
						LoggedOutCashier(false);
					}
					catch (Exception ex)
					{ 
                        InsertErrorLogToFile(ex, "ERRROR!!! Initializing ZREAD"); 
                    }

					Cursor.Current = Cursors.Default;
				}
			}
		}
		private void ReadBarCode()
		{
			DialogResult loginresult = GetWriteAccessAndLogin(mclsSalesTransactionDetails.CashierID, AccessTypes.CreateTransaction);

			if (loginresult == DialogResult.OK)
			{
				if (!string.IsNullOrEmpty(txtBarCode.Text.Trim()))
				{
					string stBarcode = txtBarCode.Text.Trim();
					decimal decQuantity = 1;

                    // 21Jul2013 Check if parking ticket and has already an item.
                    if (mclsTerminalDetails.IsParkingTerminal && ItemDataTable.Rows.Count >= 1)
                    {
                        txtBarCode.Text = "";
                        MessageBox.Show("Sorry you can only park 1 vehicle per transaction. ", "RetailPlus", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

					if (stBarcode.EndsWith("*"))
					{
						MessageBox.Show("Sorry please scan the item after the quantity before you press the enter key.", "RetailPlus", MessageBoxButtons.OK, MessageBoxIcon.Warning);
						return;
					}
					if (stBarcode.IndexOf("*") > -1)
					{
						try
						{
							decQuantity = Convert.ToDecimal(stBarcode.Substring(0, stBarcode.IndexOf("*")).Trim());
							stBarcode = stBarcode.Substring(stBarcode.IndexOf("*") + 1, stBarcode.Length - (stBarcode.IndexOf("*") + 1));
						}
						catch
						{
							MessageBox.Show("Sorry the quantity you entered is not valid. Please enter a valid quantity to purchase.", "RetailPlus", MessageBoxButtons.OK, MessageBoxIcon.Warning);
							return;
						}
					}
                    Data.ProductPackage clsProductPackage = new Data.ProductPackage(mConnection, mTransaction);
                    mConnection = clsProductPackage.Connection; mTransaction = clsProductPackage.Transaction;

                    Data.Products clsProduct = new Data.Products(mConnection, mTransaction);
                    mConnection = clsProduct.Connection; mTransaction = clsProduct.Transaction;

                    Data.ProductPackageDetails clsProductPackageDetails = new Data.ProductPackageDetails();
                    Data.ProductDetails clsProductDetails = new Data.ProductDetails();

                    if (ProductModel.PackageID != 0) //PackageID is not zero if selection is used.
                    {
                        clsProductPackageDetails = clsProductPackage.Details(ProductModel.PackageID);
                        clsProductDetails = clsProduct.Details(clsProductPackageDetails.ProductID, clsProductPackageDetails.MatrixID, mclsTerminalDetails.BranchID);
                    }
                    else //PackageID is zero if selection is not used.
                    {
                        // check if the product exist and with quantity
                        clsProductDetails = clsProduct.Details(mclsTerminalDetails.BranchID, stBarcode, mclsTerminalDetails.ShowItemMoreThanZeroQty, decQuantity);

                        // check if the product exist and zero quantity
                        if (clsProductDetails.ProductID == 0) clsProductDetails = clsProduct.Details(stBarcode, mclsTerminalDetails.BranchID);

                        // check if the product is weighted
                        if (clsProductDetails.ProductID == 0)
                        {
                            if (stBarcode.Length == 12) stBarcode = "0" + stBarcode;
                            if (stBarcode.Length > Data.Products.DEFAULT_WEIGHTED_BARCODE_CHARACTER_COUNT + 1)
                            {
                                clsProductDetails = clsProduct.Details(mclsTerminalDetails.BranchID, stBarcode.Remove(Data.Products.DEFAULT_WEIGHTED_BARCODE_CHARACTER_COUNT));

                                if (clsProductDetails.ProductID != 0)
                                {
                                    decQuantity = (decimal.Parse(stBarcode.Remove(stBarcode.Length - 1).Remove(1, Data.Products.DEFAULT_WEIGHTED_BARCODE_CHARACTER_COUNT)) / 100) / clsProductDetails.Price;
                                }
                            }
                        }

                        // get the package details
                        if (clsProductDetails.ProductID != 0) clsProductPackageDetails = clsProductPackage.Details(clsProductDetails.PackageID);
                    }

					txtBarCode.Text = "";
                    ProductModel.Clear();
					
                    if (clsProductDetails.ProductID != 0)
                    {
                        // 21Jul2013 Include getting of rates for parking
                        if (mclsTerminalDetails.IsParkingTerminal)
                        {
                            Data.ParkingRates clsParkingRate = new Data.ParkingRates(mConnection, mTransaction);
                            mConnection = clsParkingRate.Connection; mTransaction = clsParkingRate.Transaction;

                            Data.ParkingRateDetails clsParkingRateDetails = clsParkingRate.Details(clsProductDetails.ProductID, DateTime.Now.ToString("dddd"));
                            if (clsParkingRateDetails.ParkingRateID != 0)
                            {
                                clsProductDetails.Price = clsParkingRateDetails.MinimumStayPrice;
                            }
                        }

                        if (lblCustomer.Text.Trim().ToUpper() != Constants.C_RETAILPLUS_ORDER_SLIP_CUSTOMER &&
                            !mboIsRefund && clsProductDetails.Quantity - clsProductDetails.ReservedQuantity < decQuantity && mclsTerminalDetails.ShowItemMoreThanZeroQty &&
                            clsProductDetails.BarCode != Data.Products.DEFAULT_CREDIT_PAYMENT_BARCODE)
                        {
                            clsProductPackage.CommitAndDispose();
                            MessageBox.Show("Sorry the quantity you entered is greater than the current stock. " + Environment.NewLine + "Current Stock: " + clsProductDetails.Quantity.ToString("#,##0.#0"), "RetailPlus", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }

                        if (clsProductDetails.IsLock)
                        {
                            clsProductPackage.CommitAndDispose();
                            MessageBox.Show("Sorry this product is currently LOCKED for inventory. Please advise the inventory officer if you really want to sell this product.", "RetailPlus", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }

                        Data.SalesTransactionItemDetails clsItemDetails = new Data.SalesTransactionItemDetails();

                        clsItemDetails.ProductID = clsProductDetails.ProductID;
                        clsItemDetails.ProductCode = clsProductDetails.ProductCode;
                        clsItemDetails.BarCode = clsProductDetails.BarCode;
                        clsItemDetails.Description = clsProductDetails.ProductDesc;
                        clsItemDetails.ProductGroup = clsProductDetails.ProductGroupName;
                        clsItemDetails.ProductSubGroup = clsProductDetails.ProductSubGroupName;
                        clsItemDetails.TransactionItemStatus = TransactionItemStatus.Valid;
                        clsItemDetails.ProductUnitID = clsProductDetails.BaseUnitID;
                        clsItemDetails.ProductUnitCode = clsProductDetails.BaseUnitCode;
                        clsItemDetails.Quantity = decQuantity;
                        clsItemDetails.Price = clsProductDetails.Price;
                        clsItemDetails.Discount = 0;
                        clsItemDetails.ItemDiscount = 0;
                        clsItemDetails.ItemDiscountType = DiscountTypes.NotApplicable;
                        clsItemDetails.Amount = (clsItemDetails.Quantity * clsItemDetails.Price) - (clsItemDetails.Quantity * clsItemDetails.Discount);
                        clsItemDetails.VAT = clsProductDetails.VAT;
                        clsItemDetails.EVAT = clsProductDetails.EVAT;
                        clsItemDetails.LocalTax = clsProductDetails.LocalTax;
                        clsItemDetails.TransactionItemStatus = TransactionItemStatus.Valid;
                        clsItemDetails.PurchasePrice = clsProductDetails.PurchasePrice;
                        clsItemDetails.PurchaseAmount = clsItemDetails.Quantity * clsItemDetails.PurchasePrice;
                        clsItemDetails.IncludeInSubtotalDiscount = clsProductDetails.IncludeInSubtotalDiscount;
                        clsItemDetails.OrderSlipPrinter = clsProductDetails.OrderSlipPrinter;
                        clsItemDetails.OrderSlipPrinted = false;
                        clsItemDetails.PercentageCommision = clsProductDetails.PercentageCommision;
                        clsItemDetails.Commision = clsItemDetails.Amount * (clsItemDetails.PercentageCommision / 100);

                        clsItemDetails.ProductPackageID = clsProductPackageDetails.PackageID;
                        clsItemDetails.ProductUnitID = clsProductPackageDetails.UnitID;
                        clsItemDetails.ProductUnitCode = clsProductPackageDetails.UnitCode;

                        if (!mclsTerminalDetails.IsParkingTerminal)
                        {
                            clsItemDetails.Price = clsProductPackageDetails.Price;
                            clsItemDetails.PackageQuantity = clsProductPackageDetails.Quantity;
                            clsItemDetails.Amount = (clsItemDetails.Quantity * clsItemDetails.Price) - (clsItemDetails.Quantity * clsItemDetails.Discount);
                            clsItemDetails.Commision = clsItemDetails.Amount * (clsItemDetails.PercentageCommision / 100);
                        }
                        clsItemDetails.MatrixPackageID = clsProductPackageDetails.MatrixID;
                        clsItemDetails.VariationsMatrixID = clsProductDetails.MatrixID;
                        clsItemDetails.MatrixDescription = clsProductDetails.MatrixDescription;

                        if (!mboIsInTransaction)
                        {
                            lblTransDate.Text = DateTime.Now.ToString("MMM. dd, yyyy hh:mm:ss tt");
                            if (!this.CreateTransaction()) { clsProductPackage.CommitAndDispose(); return; }
                        }

                        AddItem(clsItemDetails);
                    }
					else
					{
						MessageBox.Show("Sorry the item is not yet entered in the system. Please select the generic code for this item.", "RetailPlus", MessageBoxButtons.OK, MessageBoxIcon.Warning);
					}
					clsProductPackage.CommitAndDispose();
				}
			}
		}
		private void ShowProductPrice()
		{
			if (!string.IsNullOrEmpty(txtBarCode.Text.Trim()))
			{
				string stBarcode = txtBarCode.Text.Trim();
				decimal decQuantity = 1;

				if (stBarcode.IndexOf("*") > -1)
				{
					try
					{
						decQuantity = Convert.ToDecimal(stBarcode.Substring(0, stBarcode.IndexOf("*")).Trim());
						stBarcode = stBarcode.Substring(stBarcode.IndexOf("*") + 1, stBarcode.Length - (stBarcode.IndexOf("*") + 1));
					}
					catch
					{
						MessageBox.Show("Sorry the quantity you entered is not valid. Please enter a valid quantity to purchase.", "RetailPlus", MessageBoxButtons.OK, MessageBoxIcon.Warning);
						return;
					}
				}
				// do nothing
				txtBarCode.Text = "";
			}
		}
		private void CloseTransaction()
		{
			if (!mboIsInTransaction)
			{
				MessageBox.Show("No active transaction found.", "RetailPlus", MessageBoxButtons.OK);
				return;
			}
			if ((mclsSalesTransactionDetails.SubTotal - mclsSalesTransactionDetails.Discount) < 0)
			{
				MessageBox.Show("Sorry you cannot close a less than ZERO transaction.", "RetailPlus", MessageBoxButtons.OK, MessageBoxIcon.Warning);
				return;
			}

			// Feb 13, 2009
			// overwrite cashierid and cashiername that will close the transaction
			// Aug 6, 2011 : Lemu
			// Remove this and put in ResumeTransaction 
			//try { mclsSalesTransactionDetails.CashierID = Convert.ToInt64(lblCashier.Tag); }
			//catch { }
			//mclsSalesTransactionDetails.CashierName = lblCashier.Text;
			DialogResult loginresult = GetWriteAccessAndLogin(mclsSalesTransactionDetails.CashierID, AccessTypes.CloseTransaction);

			if (loginresult == DialogResult.OK)
			{
				if (mclsSalesTransactionDetails.SubTotal == 0)
				{
					if (MessageBox.Show("Are you sure you want to close this  ZERO amount transaction?", "RetailPlus", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.No)
						return;
				}

				try
				{
					clsEvent.AddEventLn("Closing transaction no. " + lblTransNo.Text, true);

                    clsEvent.AddEventLn("      showing payment screen", true);

                    Data.Contacts clsContact = new Data.Contacts(mConnection, mTransaction);
                    mConnection = clsContact.Connection; mTransaction = clsContact.Transaction;

                    mclsContactDetails = clsContact.Details(mclsSalesTransactionDetails.CustomerID);
                    LoadContact(Data.ContactGroupCategory.CUSTOMER, mclsContactDetails);
                    clsContact.CommitAndDispose();

					//insert payment details
					PaymentsWnd payment = new PaymentsWnd();
					payment.TerminalDetails = mclsTerminalDetails;
					payment.CustomerDetails = mclsContactDetails;
					payment.SalesTransactionDetails = mclsSalesTransactionDetails;
					payment.CreditCardSwiped = mboCreditCardSwiped;
					payment.IsRefund = mboIsRefund;

					payment.ShowDialog(this);

					DialogResult paymentResult = payment.Result;

					decimal AmountPaid = payment.AmountPaid;
					decimal CashPayment = payment.CashPayment;
					decimal ChequePayment = payment.ChequePayment;
					decimal CreditCardPayment = payment.CreditCardPayment;
					decimal CreditPayment = payment.CreditPayment;
					decimal DebitPayment = payment.DebitPayment;
					decimal CreditChargeAmount = payment.SalesTransactionDetails.CreditChargeAmount;

					decimal BalanceAmount = payment.BalanceAmount;
					decimal ChangeAmount = payment.ChangeAmount;
					PaymentTypes PaymentType = payment.PaymentType;
					ArrayList arrCashPaymentDetails = payment.CashPaymentDetails;
					ArrayList arrChequePaymentDetails = payment.ChequePaymentDetails;
					ArrayList arrCreditCardPaymentDetails = payment.CreditCardPaymentDetails;
					ArrayList arrCreditPaymentDetails = payment.CreditPaymentDetails;
					ArrayList arrDebitPaymentDetails = payment.DebitPaymentDetails;
					decimal RewardPointsPayment = payment.RewardPointsPayment;
					decimal RewardConvertedPayment = payment.RewardConvertedPayment;
                    Data.ContactDetails clsCreditorDetails = payment.CreditorDetails;
					payment.Close();
					payment.Dispose();

                    this.KeyPreview = false;
                    clsEvent.AddEventLn("      payment screen closed.", true);

                    if (paymentResult != DialogResult.OK)
                    {
                        clsEvent.AddEventLn(" cancelled.", true);
                    }
                    else
					{
                        // override the customerinformation if it's paid with in-house creditcard
                        if (clsCreditorDetails.ContactID != mclsSalesTransactionDetails.CustomerID)
                        {
                            LoadContact(Data.ContactGroupCategory.CUSTOMER, clsCreditorDetails);
                        }

                        mclsSalesTransactionDetails.AmountPaid = AmountPaid;
                        mclsSalesTransactionDetails.CashPayment = CashPayment;
                        mclsSalesTransactionDetails.ChequePayment = ChequePayment;
                        mclsSalesTransactionDetails.CreditCardPayment = CreditCardPayment;
                        mclsSalesTransactionDetails.CreditPayment = CreditPayment;

                        // include credit charge amount
                        mclsSalesTransactionDetails.CreditChargeAmount = CreditChargeAmount;
                        mclsSalesTransactionDetails.AmountDue += mclsSalesTransactionDetails.CreditChargeAmount;

                        mclsSalesTransactionDetails.DebitPayment = DebitPayment;
                        mclsSalesTransactionDetails.RewardPointsPayment = RewardConvertedPayment;

                        /**

                            * Nov 04, 2011 : for payments using reward points
                            * */
                        mclsSalesTransactionDetails.RewardPointsPayment = RewardPointsPayment;
                        mclsSalesTransactionDetails.RewardConvertedPayment = RewardConvertedPayment;

                        /**
                            * Oct 17, 2011 : Move this code here.
                            * check if will print transaction or not before opening any connection to database.
                            * */
                        mboDoNotPrintTransactionDate = false;
                        if (mclsTerminalDetails.AutoPrint == PrintingPreference.AskFirst)
                        {
                            if (MessageBox.Show("Would you like to print this transaction?", "RetailPlus", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
                            {
                                mclsTerminalDetails.AutoPrint = PrintingPreference.Normal;

                                if (mclsSysConfigDetails.WillAskDoNotPrintTransactionDate)
                                    if (MessageBox.Show("Would you like the system NOT to print the transaction date?", "RetailPlus", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
                                        mboDoNotPrintTransactionDate = true;
                            }
                        }

                        // Mar 17, 2009
                        // open drawer first before printing.
                        //OpenDrawerDelegate opendrawerDel = new OpenDrawerDelegate(OpenDrawer);
                        //Invoke(opendrawerDel);
                        OpenDrawer();

                        // start a connection for the database.
                        //update the transaction table 
                        Data.SalesTransactions clsSalesTransactions = new Data.SalesTransactions(mConnection, mTransaction);
                        mConnection = clsSalesTransactions.Connection; mTransaction = clsSalesTransactions.Transaction;

                        mclsSalesTransactionDetails.AmountPaid = AmountPaid;
                        mclsSalesTransactionDetails.ChangeAmount = ChangeAmount;

                        Cursor.Current = Cursors.WaitCursor;
                        clsEvent.AddEventLn("[" + lblCashier.Text + "]      saving payments...", true);

                        // for assignment of payments
                        mclsSalesTransactionDetails.PaymentDetails = AssignArrayListPayments(arrCashPaymentDetails, arrChequePaymentDetails, arrCreditCardPaymentDetails, arrCreditPaymentDetails, arrDebitPaymentDetails);

                        SavePayments(arrCashPaymentDetails, arrChequePaymentDetails, arrCreditCardPaymentDetails, arrCreditPaymentDetails, arrDebitPaymentDetails);

                        if (mclsSalesTransactionDetails.CreditChargeAmount != 0)
                        {
                            //Aug 30, 2014 delete need to move this from here to mainwnd
                            clsSalesTransactions.UpdateCreditChargeAmount(mclsSalesTransactionDetails.BranchID, mclsSalesTransactionDetails.TerminalNo, mclsSalesTransactionDetails.TransactionID, mclsSalesTransactionDetails.CreditChargeAmount);
                        }

                        Data.Products clsProduct = new Data.Products(mConnection, mTransaction);
                        mConnection = clsProduct.Connection; mTransaction = clsProduct.Transaction;

                        if (mboIsRefund && !mclsTerminalDetails.IsParkingTerminal)
                        {
                            #region mboIsRefund

                            clsEvent.AddEventLn("[" + lblCashier.Text + "]      updating refund terminal no...", true, mclsSysConfigDetails.WillWriteSystemLog);
                            clsSalesTransactions.UpdateTerminalNo(mclsSalesTransactionDetails.TransactionID, lblTerminalNo.Text);

                            // Sep 4, 2014 : Added to put as OR No - Void NO-OR
                            if (!mclsSalesTransactionDetails.isConsignment)
                            {
                                mclsSalesTransactionDetails.ORNo = clsSalesTransactions.CreateORNo(mclsTerminalDetails.BranchDetails.BranchID, mclsTerminalDetails.TerminalNo);
                                clsEvent.AddEventLn("      applying ORNo".PadRight(15) + ":" + mclsSalesTransactionDetails.ORNo, true, mclsSysConfigDetails.WillWriteSystemLog);
                            }

                            clsSalesTransactions.Refund(mclsSalesTransactionDetails.TransactionID, mclsSalesTransactionDetails.ORNo, -mclsSalesTransactionDetails.ItemSold, -mclsSalesTransactionDetails.QuantitySold, -mclsSalesTransactionDetails.SubTotal, -mclsSalesTransactionDetails.NetSales, -mclsSalesTransactionDetails.ItemsDiscount, -mclsSalesTransactionDetails.Discount, -mclsSalesTransactionDetails.SNRDiscount, -mclsSalesTransactionDetails.PWDDiscount, -mclsSalesTransactionDetails.OtherDiscount, mclsSalesTransactionDetails.TransDiscount, mclsSalesTransactionDetails.TransDiscountType, -mclsSalesTransactionDetails.VAT, -mclsSalesTransactionDetails.VATableAmount, -mclsSalesTransactionDetails.NonVATableAmount, -mclsSalesTransactionDetails.VATExempt, -mclsSalesTransactionDetails.EVAT, -mclsSalesTransactionDetails.EVATableAmount, -mclsSalesTransactionDetails.NonEVATableAmount, -mclsSalesTransactionDetails.LocalTax, -mclsSalesTransactionDetails.AmountPaid, -CashPayment, -ChequePayment, -CreditCardPayment, -CreditPayment, -DebitPayment, -RewardPointsPayment, -RewardConvertedPayment, -BalanceAmount, -ChangeAmount, PaymentType, mclsSalesTransactionDetails.DiscountCode, mclsSalesTransactionDetails.DiscountRemarks, -mclsSalesTransactionDetails.Charge, mclsSalesTransactionDetails.ChargeAmount, mclsSalesTransactionDetails.ChargeCode, mclsSalesTransactionDetails.ChargeRemarks, mclsSalesTransactionDetails.CashierID, lblCashier.Text);

                            //UpdateTerminalReportDelegate updateterminalDel = new UpdateTerminalReportDelegate(UpdateTerminalReport);
                            clsEvent.AddEventLn("[" + lblCashier.Text + "]      updating refund terminal report...", true, mclsSysConfigDetails.WillWriteSystemLog);
                            UpdateTerminalReport(TransactionStatus.Refund, mclsSalesTransactionDetails.ItemSold, mclsSalesTransactionDetails.QuantitySold, mclsSalesTransactionDetails.SubTotal, mclsSalesTransactionDetails.Discount, mclsSalesTransactionDetails.ItemsDiscount, mclsSalesTransactionDetails.Charge, mclsSalesTransactionDetails.VAT, mclsSalesTransactionDetails.VATableAmount, mclsSalesTransactionDetails.NonVATableAmount, mclsSalesTransactionDetails.VATExempt, mclsSalesTransactionDetails.EVAT, mclsSalesTransactionDetails.EVATableAmount, mclsSalesTransactionDetails.NonEVATableAmount, mclsSalesTransactionDetails.LocalTax, CashPayment, ChequePayment, CreditCardPayment, CreditPayment, DebitPayment, RewardPointsPayment, RewardConvertedPayment, PaymentType);

                            //UpdateCashierReportDelegate updatecashierDel = new UpdateCashierReportDelegate(UpdateCashierReport);
                            clsEvent.AddEventLn("[" + lblCashier.Text + "]      updating redunf cashier report...", true, mclsSysConfigDetails.WillWriteSystemLog);
                            UpdateCashierReport(TransactionStatus.Refund, mclsSalesTransactionDetails.ItemSold, mclsSalesTransactionDetails.QuantitySold, mclsSalesTransactionDetails.SubTotal, mclsSalesTransactionDetails.Discount, mclsSalesTransactionDetails.ItemsDiscount, mclsSalesTransactionDetails.Charge, mclsSalesTransactionDetails.VAT, mclsSalesTransactionDetails.VATableAmount, mclsSalesTransactionDetails.NonVATableAmount, mclsSalesTransactionDetails.VATExempt, mclsSalesTransactionDetails.EVAT, mclsSalesTransactionDetails.EVATableAmount, mclsSalesTransactionDetails.NonEVATableAmount, mclsSalesTransactionDetails.LocalTax, CashPayment, ChequePayment, CreditCardPayment, CreditPayment, DebitPayment, RewardPointsPayment, RewardConvertedPayment, PaymentType);

                            // Sep 24, 2014 : update back the LastCheckInDate to min date
                            clsContact = new Data.Contacts(mConnection, mTransaction);
                            mConnection = clsContact.Connection; mTransaction = clsContact.Transaction;

                            clsContact.UpdateLastCheckInDate(mclsSalesTransactionDetails.CustomerID, Constants.C_DATE_MIN_VALUE);

                            // Sep 14, 2014 Control printing in mclsFilePrinter.Write
                            //if (mclsTerminalDetails.AutoPrint == PrintingPreference.Normal)	//print items if not yet printed
                            //{
                            clsEvent.AddEventLn("[" + lblCashier.Text + "]      printing refund items...", true, mclsSysConfigDetails.WillWriteSystemLog);
                            foreach (System.Data.DataRow dr in ItemDataTable.Rows)
                            {
                                if (dr["Quantity"].ToString() != "VOID")
                                {
                                    string stItemNo = "" + dr["ItemNo"].ToString();
                                    string stProductCode = "" + dr["ProductCode"].ToString();
                                    if (dr["MatrixDescription"].ToString() != string.Empty && dr["MatrixDescription"].ToString() != null) stProductCode += "-" + dr["MatrixDescription"].ToString();
                                    string stProductUnitCode = "" + dr["ProductUnitCode"].ToString();
                                    decimal decQuantity = Convert.ToDecimal(dr["Quantity"]);
                                    decimal decPrice = Convert.ToDecimal(dr["Price"]);
                                    decimal decDiscount = Convert.ToDecimal(dr["Discount"]);
                                    decimal decAmount = Convert.ToDecimal(dr["Amount"]);
                                    decimal decVAT = Convert.ToDecimal(dr["VAT"]);
                                    decimal decEVAT = Convert.ToDecimal(dr["EVAT"]);
                                    decimal decPromoApplied = Convert.ToDecimal(dr["PromoApplied"]);

                                    if (mclsTerminalDetails.ReceiptType == TerminalReceiptType.Default)
                                        PrintItem(stItemNo, stProductCode, stProductUnitCode, decQuantity, decPrice, decDiscount, decPromoApplied, decAmount, decVAT, decEVAT);
                                }
                            }

                            if (mclsTerminalDetails.ReceiptType == TerminalReceiptType.Default)
                                PrintReportFooterSection(true, TransactionStatus.Refund, mclsSalesTransactionDetails.ItemSold, mclsSalesTransactionDetails.QuantitySold, mclsSalesTransactionDetails.SubTotal, mclsSalesTransactionDetails.Discount, mclsSalesTransactionDetails.Charge, mclsSalesTransactionDetails.AmountPaid, CashPayment, ChequePayment, CreditCardPayment, CreditPayment, DebitPayment, RewardPointsPayment, RewardConvertedPayment, ChangeAmount, arrChequePaymentDetails, arrCreditCardPaymentDetails, arrCreditPaymentDetails, arrDebitPaymentDetails);
                            //}
                            // Sep 14, 2014 Control printing in mclsFilePrinter.Write

                            // Sep 24, 2011      Lemuel E. Aceron
                            // Added order slip wherein all punch items will not change sales and inventory
                            // a customer named ORDER SLIP should be defined in contacts
                            // lblCustomer.Text.Trim().ToUpper() != Constants.C_RETAILPLUS_ORDER_SLIP_CUSTOMER

                            // Added May 7, 2011 to Cater Reserved and Commit functionality
                            // !mclsTerminalDetails.ReservedAndCommit

                            // Sep 14, 2013: Remove the reserved and commit.
                            //if (lblCustomer.Text.Trim().ToUpper() != Constants.C_RETAILPLUS_ORDER_SLIP_CUSTOMER && !mclsTerminalDetails.ReservedAndCommit && !mclsTerminalDetails.IsParkingTerminal)
                            if (lblCustomer.Text.Trim().ToUpper() != Constants.C_RETAILPLUS_ORDER_SLIP_CUSTOMER && !mclsTerminalDetails.IsParkingTerminal)
                            {
                                clsEvent.AddEventLn("[" + lblCashier.Text + "]      adding the refund items quantity to inv...", true, mclsSysConfigDetails.WillWriteSystemLog);
                                Data.ProductUnit clsProductUnit = new Data.ProductUnit(mConnection, mTransaction);
                                mConnection = clsProductUnit.Connection; mTransaction = clsProductUnit.Transaction;

                                foreach (System.Data.DataRow dr in ItemDataTable.Rows)
                                {
                                    Int64 lProductID = Convert.ToInt64(dr["ProductID"]);
                                    Int64 lVariationsMatrixID = Convert.ToInt64(dr["VariationsMatrixID"]);
                                    Int32 iProductUnitID = Convert.ToInt32(dr["ProductUnitID"]);
                                    decimal decQuantity = 0;
                                    decimal decPackageQuantity = 0;
                                    decimal decNewQuantity = 0;

                                    if (dr["Quantity"].ToString() != "VOID")
                                    {
                                        decQuantity = Convert.ToDecimal(dr["Quantity"]);
                                        decPackageQuantity = Convert.ToDecimal(dr["PackageQuantity"]);
                                        decNewQuantity = clsProductUnit.GetBaseUnitValue(lProductID, iProductUnitID, decQuantity * decPackageQuantity);

                                        clsProduct.AddQuantity(mclsTerminalDetails.BranchID, lProductID, lVariationsMatrixID, decNewQuantity, Data.Products.getPRODUCT_INVENTORY_MOVEMENT_VALUE(Data.PRODUCT_INVENTORY_MOVEMENT.ADD_REFUND_ITEM), mclsSalesTransactionDetails.TransactionDate, mclsSalesTransactionDetails.TransactionNo, mclsSalesTransactionDetails.CashierName);
                                    }
                                }
                            }
                            #endregion
                        }
                        else if (!mboIsRefund)
                        {
                            #region Normal and ParkingTerminal

                            clsEvent.AddEventLn("      closing transaction...", true, mclsSysConfigDetails.WillWriteSystemLog);
                            clsSalesTransactions.UpdateTerminalNo(mclsSalesTransactionDetails.TransactionID, lblTerminalNo.Text);

                            // Sep 4, 2014 : Added to put as OR No - Void NO-OR
                            if (!mclsSalesTransactionDetails.isConsignment)
                            {
                                mclsSalesTransactionDetails.ORNo = clsSalesTransactions.CreateORNo(mclsTerminalDetails.BranchDetails.BranchID, mclsTerminalDetails.TerminalNo);
                                clsEvent.AddEventLn("      applying ORNo".PadRight(15) + ":" + mclsSalesTransactionDetails.ORNo, true, mclsSysConfigDetails.WillWriteSystemLog);
                            }

                            clsSalesTransactions.Close(mclsSalesTransactionDetails.TransactionID, mclsSalesTransactionDetails.ORNo, mclsSalesTransactionDetails.ItemSold, mclsSalesTransactionDetails.QuantitySold, mclsSalesTransactionDetails.SubTotal, mclsSalesTransactionDetails.NetSales, mclsSalesTransactionDetails.ItemsDiscount, mclsSalesTransactionDetails.Discount, mclsSalesTransactionDetails.SNRDiscount, mclsSalesTransactionDetails.PWDDiscount, mclsSalesTransactionDetails.OtherDiscount, mclsSalesTransactionDetails.TransDiscount, mclsSalesTransactionDetails.TransDiscountType, mclsSalesTransactionDetails.VAT, mclsSalesTransactionDetails.VATableAmount, mclsSalesTransactionDetails.NonVATableAmount, mclsSalesTransactionDetails.VATExempt, mclsSalesTransactionDetails.EVAT, mclsSalesTransactionDetails.EVATableAmount, mclsSalesTransactionDetails.NonEVATableAmount, mclsSalesTransactionDetails.LocalTax, mclsSalesTransactionDetails.AmountPaid, CashPayment, ChequePayment, CreditCardPayment, CreditPayment, DebitPayment, RewardPointsPayment, RewardConvertedPayment, BalanceAmount, ChangeAmount, PaymentType, mclsSalesTransactionDetails.DiscountCode, mclsSalesTransactionDetails.DiscountRemarks, mclsSalesTransactionDetails.Charge, mclsSalesTransactionDetails.ChargeAmount, mclsSalesTransactionDetails.ChargeCode, mclsSalesTransactionDetails.ChargeRemarks, mclsSalesTransactionDetails.CashierID, mclsSalesTransactionDetails.CashierName);

                            //UpdateTerminalReportDelegate updateterminalDel = new UpdateTerminalReportDelegate(UpdateTerminalReport);
                            clsEvent.AddEventLn("      updating terminal report...", true, mclsSysConfigDetails.WillWriteSystemLog);
                            UpdateTerminalReport(TransactionStatus.Closed, mclsSalesTransactionDetails.ItemSold, mclsSalesTransactionDetails.QuantitySold, mclsSalesTransactionDetails.SubTotal, mclsSalesTransactionDetails.Discount, mclsSalesTransactionDetails.ItemsDiscount, mclsSalesTransactionDetails.Charge, mclsSalesTransactionDetails.VAT, mclsSalesTransactionDetails.VATableAmount, mclsSalesTransactionDetails.NonVATableAmount, mclsSalesTransactionDetails.VATExempt, mclsSalesTransactionDetails.EVAT, mclsSalesTransactionDetails.EVATableAmount, mclsSalesTransactionDetails.NonEVATableAmount, mclsSalesTransactionDetails.LocalTax, CashPayment, ChequePayment, CreditCardPayment, CreditPayment, DebitPayment, RewardPointsPayment, RewardConvertedPayment, PaymentType);

                            //UpdateCashierReportDelegate updatecashierDel = new UpdateCashierReportDelegate(UpdateCashierReport);
                            clsEvent.AddEventLn("      updating cashier's report...", true, mclsSysConfigDetails.WillWriteSystemLog);
                            UpdateCashierReport(TransactionStatus.Closed, mclsSalesTransactionDetails.ItemSold, mclsSalesTransactionDetails.QuantitySold, mclsSalesTransactionDetails.SubTotal, mclsSalesTransactionDetails.Discount, mclsSalesTransactionDetails.ItemsDiscount, mclsSalesTransactionDetails.Charge, mclsSalesTransactionDetails.VAT, mclsSalesTransactionDetails.VATableAmount, mclsSalesTransactionDetails.NonVATableAmount, mclsSalesTransactionDetails.VATExempt, mclsSalesTransactionDetails.EVAT, mclsSalesTransactionDetails.EVATableAmount, mclsSalesTransactionDetails.NonEVATableAmount, mclsSalesTransactionDetails.LocalTax, CashPayment, ChequePayment, CreditCardPayment, CreditPayment, DebitPayment, RewardPointsPayment, RewardConvertedPayment, PaymentType);

                            // Sep 24, 2014 : update back the LastCheckInDate to min date
                            clsContact = new Data.Contacts(mConnection, mTransaction);
                            mConnection = clsContact.Connection; mTransaction = clsContact.Transaction;

                            clsContact.UpdateLastCheckInDate(mclsSalesTransactionDetails.CustomerID, Constants.C_DATE_MIN_VALUE);

                            // Sep 24, 2011      Lemuel E. Aceron
                            // Added order slip wherein all punch items will not change sales and inventory
                            // a customer named ORDER SLIP should be defined in contacts
                            //if (lblCustomer.Text.Trim().ToUpper() != Constants.C_RETAILPLUS_ORDER_SLIP_CUSTOMER)

                            // Added May 7, 2011 to Cater Reserved and Commit functionality    
                            // !mclsTerminalDetails.ReservedAndCommit
                            if (mclsTerminalDetails.IsParkingTerminal)
                            {
                                clsEvent.AddEventLn("      adding back the parking slot to inv...", true, mclsSysConfigDetails.WillWriteSystemLog);
                                Data.ProductUnit clsProductUnit = new Data.ProductUnit(mConnection, mTransaction);
                                mConnection = clsProductUnit.Connection; mTransaction = clsProductUnit.Transaction;

                                Data.ProductVariationsMatrix clsProductVariationsMatrix = new Data.ProductVariationsMatrix(mConnection, mTransaction);

                                foreach (System.Data.DataRow dr in ItemDataTable.Rows)
                                {
                                    long lProductID = Convert.ToInt64(dr["ProductID"]);
                                    long lVariationsMatrixID = Convert.ToInt64(dr["VariationsMatrixID"]);
                                    int iProductUnitID = Convert.ToInt32(dr["ProductUnitID"]);
                                    decimal decQuantity = 0;
                                    decimal decPackageQuantity = 0;
                                    decimal decNewQuantity = 0;
                                    decimal decPrice = Convert.ToDecimal(dr["Price"]);
                                    decimal decPurchasePrice = Convert.ToDecimal(dr["PurchasePrice"]);

                                    if ((dr["Quantity"].ToString().IndexOf("RETURN") == -1) && (dr["Quantity"].ToString() != "VOID"))
                                    {
                                        decQuantity = Convert.ToDecimal(dr["Quantity"]);
                                        decPackageQuantity = Convert.ToDecimal(dr["PackageQuantity"]);
                                        decNewQuantity = clsProductUnit.GetBaseUnitValue(lProductID, iProductUnitID, decQuantity * decPackageQuantity);

                                        clsProduct.AddQuantity(mclsTerminalDetails.BranchID, lProductID, lVariationsMatrixID, decNewQuantity, Data.Products.getPRODUCT_INVENTORY_MOVEMENT_VALUE(Data.PRODUCT_INVENTORY_MOVEMENT.PARKING_OUT), mclsSalesTransactionDetails.TransactionDate, mclsSalesTransactionDetails.TransactionNo, mclsSalesTransactionDetails.CashierName);
                                    }
                                }
                            }
                            else if (lblCustomer.Text.Trim().ToUpper() != Constants.C_RETAILPLUS_ORDER_SLIP_CUSTOMER)
                            {
                                Data.ProductUnit clsProductUnit = new Data.ProductUnit(mConnection, mTransaction);
                                mConnection = clsProductUnit.Connection; mTransaction = clsProductUnit.Transaction;

                                Data.ProductVariationsMatrix clsProductVariationsMatrix = new Data.ProductVariationsMatrix(mConnection, mTransaction);

                                foreach (System.Data.DataRow dr in ItemDataTable.Rows)
                                {
                                    long lProductID = Convert.ToInt64(dr["ProductID"]);
                                    long lVariationsMatrixID = Convert.ToInt64(dr["VariationsMatrixID"]);
                                    int iProductUnitID = Convert.ToInt32(dr["ProductUnitID"]);
                                    decimal decQuantity = 0;
                                    decimal decPackageQuantity = 0;
                                    decimal decNewQuantity = 0;
                                    decimal decPrice = Convert.ToDecimal(dr["Price"]);
                                    decimal decPurchasePrice = Convert.ToDecimal(dr["PurchasePrice"]);

                                    decimal decDiscount = Convert.ToDecimal(dr["Discount"]);
                                    decimal decAmount = Convert.ToDecimal(dr["Amount"]);

                                    if (dr["Quantity"].ToString().IndexOf("RETURN") != -1)
                                    {
                                        decQuantity = Convert.ToDecimal(dr["Quantity"].ToString().Replace(" - RETURN", "").Trim());
                                        decPackageQuantity = Convert.ToDecimal(dr["PackageQuantity"]);
                                        decNewQuantity = clsProductUnit.GetBaseUnitValue(lProductID, iProductUnitID, decQuantity * decPackageQuantity);

                                        clsEvent.AddEventLn("      adding return item: prdid-" + lProductID.ToString() + " to inv: qty-" + decNewQuantity.ToString() + "...", true, mclsSysConfigDetails.WillWriteSystemLog);
                                        clsProduct.AddQuantity(mclsTerminalDetails.BranchID, lProductID, lVariationsMatrixID, decNewQuantity, Data.Products.getPRODUCT_INVENTORY_MOVEMENT_VALUE(Data.PRODUCT_INVENTORY_MOVEMENT.ADD_RETURN_ITEM), mclsSalesTransactionDetails.TransactionDate, mclsSalesTransactionDetails.TransactionNo, mclsSalesTransactionDetails.CashierName);
                                    }
                                    else if (dr["Quantity"].ToString() != "VOID")
                                    {
                                        decQuantity = Convert.ToDecimal(dr["Quantity"]);
                                        decPackageQuantity = Convert.ToDecimal(dr["PackageQuantity"]);
                                        decNewQuantity = clsProductUnit.GetBaseUnitValue(lProductID, iProductUnitID, decQuantity * decPackageQuantity);

                                        clsEvent.AddEventLn("      subtracting sold item: prdid-" + lProductID.ToString() + " from inv/rsrvd: qty-" + decNewQuantity.ToString() + "...", true, mclsSysConfigDetails.WillWriteSystemLog);
                                        clsProduct.SubtractQuantity(mclsTerminalDetails.BranchID, lProductID, lVariationsMatrixID, decNewQuantity, Data.Products.getPRODUCT_INVENTORY_MOVEMENT_VALUE(Data.PRODUCT_INVENTORY_MOVEMENT.DEDUCT_SOLD_RETAIL) + " @ " + (decAmount / decNewQuantity).ToString("#,##0.#0") + " Buying: " + decPurchasePrice.ToString("#,##0.#0") + " Orig Selling: " + decPrice.ToString("#,##0.#0") + " Discount: " + (decPrice - (decAmount / decNewQuantity)).ToString("#,##0.#0") + " to " + mclsSalesTransactionDetails.CustomerName, DateTime.Now, mclsSalesTransactionDetails.TransactionNo, mclsSalesTransactionDetails.CashierName);
                                        clsProduct.SubtractReservedQuantity(mclsTerminalDetails.BranchID, lProductID, lVariationsMatrixID, decNewQuantity, Data.Products.getPRODUCT_INVENTORY_MOVEMENT_VALUE(Data.PRODUCT_INVENTORY_MOVEMENT.DEDUCT_SOLD_RETAIL) + " @ " + (decAmount / decNewQuantity).ToString("#,##0.#0") + " Buying: " + decPurchasePrice.ToString("#,##0.#0") + " Orig Selling: " + decPrice.ToString("#,##0.#0") + " Discount: " + (decPrice - (decAmount / decNewQuantity)).ToString("#,##0.#0") + " to " + mclsSalesTransactionDetails.CustomerName, DateTime.Now, mclsSalesTransactionDetails.TransactionNo, mclsSalesTransactionDetails.CashierName);
                                    }
                                }
                            }

                            // Nov 1, 2011 : Lemu - disabled reward points if product is exempted 
                            clsEvent.AddEventLn("      checking if rewards is enabled...", true, mclsSysConfigDetails.WillWriteSystemLog);
                            if (mclsSalesTransactionDetails.RewardCardActive && mclsTerminalDetails.RewardPointsDetails.EnableRewardPoints)
                            {
                                foreach (System.Data.DataRow dr in ItemDataTable.Rows)
                                {
                                    if (dr["Barcode"].ToString() == Data.Products.DEFAULT_CREDIT_PAYMENT_BARCODE || 
                                        dr["Barcode"].ToString() == Data.Products.DEFAULT_ADVANTAGE_CARD_MEMBERSHIP_FEE_BARCODE ||
                                        dr["Barcode"].ToString() == Data.Products.DEFAULT_ADVANTAGE_CARD_RENEWAL_FEE_BARCODE ||
                                        dr["Barcode"].ToString() == Data.Products.DEFAULT_ADVANTAGE_CARD_REPLACEMENT_FEE_BARCODE ||
                                        dr["Barcode"].ToString() == Data.Products.DEFAULT_CREDIT_CARD_MEMBERSHIP_FEE_BARCODE ||
                                        dr["Barcode"].ToString() == Data.Products.DEFAULT_CREDIT_CARD_RENEWAL_FEE_BARCODE ||
                                        dr["Barcode"].ToString() == Data.Products.DEFAULT_SUPER_CARD_MEMBERSHIP_FEE_BARCODE ||
                                        dr["Barcode"].ToString() == Data.Products.DEFAULT_SUPER_CARD_RENEWAL_FEE_BARCODE ||
                                        dr["Barcode"].ToString() == Data.Products.DEFAULT_SUPER_CARD_REPLACEMENT_FEE_BARCODE)
                                    {
                                        mclsTerminalDetails.RewardPointsDetails.EnableRewardPoints = false;
                                        break;
                                    }
                                }
                            }

                            #endregion
                        }

                        // Oct 23, 2011 : Lemu - Added Reward Points
                        #region Reward Payment

                        if (mclsSalesTransactionDetails.RewardPointsPayment != 0)
                        {
                            clsEvent.AddEventLn("      deducting rewards payment...", true, mclsSysConfigDetails.WillWriteSystemLog);
                            // this should comes before earning of points otherwise this will be wrong.
                            Data.ContactReward clsContactReward = new Data.ContactReward(mConnection, mTransaction);
                            mConnection = clsContactReward.Connection; mTransaction = clsContactReward.Transaction;

                            clsContactReward.DeductPoints(mclsSalesTransactionDetails.CustomerID, mclsSalesTransactionDetails.RewardPointsPayment);
                            string strReason = "Redeemed " + mclsSalesTransactionDetails.RewardPointsPayment + " using Reward Card #: " + mclsSalesTransactionDetails.RewardCardNo;
                            clsContactReward.AddMovement(mclsSalesTransactionDetails.CustomerID, mclsSalesTransactionDetails.TransactionDate, mclsSalesTransactionDetails.RewardCurrentPoints, -mclsSalesTransactionDetails.RewardPointsPayment, mclsSalesTransactionDetails.RewardCurrentPoints - mclsSalesTransactionDetails.RewardPointsPayment, mclsSalesTransactionDetails.RewardCardExpiry, strReason, mclsTerminalDetails.TerminalNo, mclsSalesTransactionDetails.CashierName, mclsSalesTransactionDetails.TransactionNo);

                            mclsSalesTransactionDetails.RewardPreviousPoints = mclsSalesTransactionDetails.RewardCurrentPoints;
                            mclsSalesTransactionDetails.RewardCurrentPoints -= mclsSalesTransactionDetails.RewardPointsPayment;
                            mclsSalesTransactionDetails.RewardEarnedPoints = 0;

                            clsEvent.AddEventLn("      printing rewards slip...", true, mclsSysConfigDetails.WillWriteSystemLog);
                            PrintRewardsRedemptionSlip();
                        }
                        #endregion

                        #region Add reward points to customer

                        if (mboRewardCardSwiped && mclsTerminalDetails.RewardPointsDetails.EnableRewardPoints && mclsSalesTransactionDetails.RewardCardActive && mclsTerminalDetails.RewardPointsDetails.RewardPointsMinimum <= mclsSalesTransactionDetails.AmountDue)
                        {
                            decimal decRewardPoints = 0;
                            try
                            {
                                decRewardPoints = (mclsSalesTransactionDetails.AmountDue - mclsSalesTransactionDetails.RewardConvertedPayment) / mclsTerminalDetails.RewardPointsDetails.RewardPointsEvery * mclsTerminalDetails.RewardPointsDetails.RewardPoints;

                                // round down points if RoundDown is enabled
                                if (mclsTerminalDetails.RewardPointsDetails.RoundDownRewardPoints) decRewardPoints = decimal.Floor(decRewardPoints);

                                //Data.Product clsProduct
                                long lngProductID = 0;
                                foreach (System.Data.DataRow dr in ItemDataTable.Rows)
                                {
                                    lngProductID = long.Parse(dr["ProductID"].ToString());
                                    decRewardPoints += clsProduct.Details1(mclsTerminalDetails.BranchID, lngProductID).RewardPoints;
                                }
                            }
                            catch { }

                            mclsSalesTransactionDetails.RewardEarnedPoints = decRewardPoints;
                            mclsSalesTransactionDetails.RewardCurrentPoints = mclsSalesTransactionDetails.RewardPreviousPoints + mclsSalesTransactionDetails.RewardEarnedPoints;

                            Data.ContactReward clsContactReward = new Data.ContactReward(mConnection, mTransaction);
                            mConnection = clsContactReward.Connection; mTransaction = clsContactReward.Transaction;

                            clsContactReward.AddPoints(mclsSalesTransactionDetails.CustomerID, mclsSalesTransactionDetails.RewardEarnedPoints);
                            clsContactReward.AddPurchase(mclsSalesTransactionDetails.CustomerID, mclsSalesTransactionDetails.AmountDue);
                            string strReason = "Purchase " + mclsSalesTransactionDetails.AmountDue.ToString("#,##0.#0") + " using Reward Card #: " + mclsSalesTransactionDetails.RewardCardNo;
                            clsContactReward.AddMovement(mclsSalesTransactionDetails.CustomerID, mclsSalesTransactionDetails.TransactionDate, mclsSalesTransactionDetails.RewardPreviousPoints, mclsSalesTransactionDetails.RewardEarnedPoints, mclsSalesTransactionDetails.RewardCurrentPoints, mclsSalesTransactionDetails.RewardCardExpiry, strReason, mclsTerminalDetails.TerminalNo, mclsSalesTransactionDetails.CashierName, mclsSalesTransactionDetails.TransactionNo);
                        }
                        #endregion

                        // commit the transactions here.
                        // in case error s encoutered n printing. transaction is already committed.
                        clsEvent.AddEventLn("      commiting transaction to database...", true, mclsSysConfigDetails.WillWriteSystemLog);
                        clsSalesTransactions.CommitAndDispose();

                        /**
                            * print the transaction
                            * */
                        #region printing
                        try
                        {
                            if (mclsTerminalDetails.ReceiptType == TerminalReceiptType.SalesInvoice)
                            {
                                clsEvent.AddEventLn("      printing sales invoice...", true, mclsSysConfigDetails.WillWriteSystemLog);
                                PrintSalesInvoice();
                            }
                            else if (mclsTerminalDetails.ReceiptType == TerminalReceiptType.DeliveryReceipt)
                            {
                                clsEvent.AddEventLn("      printing delivery receipt...", true, mclsSysConfigDetails.WillWriteSystemLog);
                                PrintDeliveryReceipt();
                            }
                            else if (mclsTerminalDetails.ReceiptType == TerminalReceiptType.SalesInvoiceAndDR)
                            {
                                clsEvent.AddEventLn("      printing sales invoice & delivery receipt...", true, mclsSysConfigDetails.WillWriteSystemLog);

                                // Sep 24, 2014 do not print sales invoice if its a consignment
                                if (!mclsSalesTransactionDetails.isConsignment)
                                    PrintSalesInvoice();

                                PrintDeliveryReceipt();
                            }
                            //Added February 10, 2010
                            else if (mclsTerminalDetails.ReceiptType == TerminalReceiptType.SalesInvoiceForLX300Printer)
                            {
                                clsEvent.AddEventLn("      printing sales invoice for LX300...", true, mclsSysConfigDetails.WillWriteSystemLog);
                                PrintSalesInvoiceToLX(TerminalReceiptType.SalesInvoiceForLX300Printer);
                            }
                            //Added May 11, 2010
                            else if (mclsTerminalDetails.ReceiptType == TerminalReceiptType.SalesInvoiceOrDR)
                            {
                                clsEvent.AddEventLn("      printing sales invoice or OR...", true, mclsSysConfigDetails.WillWriteSystemLog);
                                if (mclsSalesTransactionDetails.CashPayment != 0 || mclsSalesTransactionDetails.CreditCardPayment != 0)
                                    PrintSalesInvoice();
                                if (mclsSalesTransactionDetails.ChequePayment != 0 || mclsSalesTransactionDetails.CreditPayment != 0)
                                    PrintDeliveryReceipt();
                            }
                            //Added January 17, 2011
                            else if (mclsTerminalDetails.ReceiptType == TerminalReceiptType.SalesInvoiceForLX300PlusPrinter)
                            {
                                clsEvent.AddEventLn("      printing sales invoice for LX300 Plus...", true, mclsSysConfigDetails.WillWriteSystemLog);
                                PrintSalesInvoiceToLX(TerminalReceiptType.SalesInvoiceForLX300PlusPrinter);
                            }
                            //Added February 22, 2011
                            else if (mclsTerminalDetails.ReceiptType == TerminalReceiptType.SalesInvoiceForLX300PlusAmazon)
                            {
                                clsEvent.AddEventLn("      printing sales invoice for LX300 Plus Amazon...", true, mclsSysConfigDetails.WillWriteSystemLog);
                                PrintSalesInvoiceToLX(TerminalReceiptType.SalesInvoiceForLX300PlusAmazon);
                            }
                            else if (!mboIsRefund) // do not print if refund coz its already printed above
                            {
                                // Sep 14, 2014 Control printing in mclsFilePrinter.Write
                                //if (mclsTerminalDetails.AutoPrint == PrintingPreference.Normal)	//print items if not yet printed
                                //{
                                clsEvent.AddEventLn("      printing items to POS printer...", true, mclsSysConfigDetails.WillWriteSystemLog);
                                foreach (System.Data.DataRow dr in ItemDataTable.Rows)
                                {
                                    string stItemNo = "" + dr["ItemNo"].ToString();
                                    string stProductUnitCode = "" + dr["ProductUnitCode"].ToString();
                                    decimal decPrice = Convert.ToDecimal(dr["Price"]);
                                    decimal decDiscount = Convert.ToDecimal(dr["Discount"]);
                                    decimal decAmount = Convert.ToDecimal(dr["Amount"]);
                                    decimal decVAT = Convert.ToDecimal(dr["VAT"]);
                                    decimal decEVAT = Convert.ToDecimal(dr["EVAT"]);
                                    decimal decPromoApplied = Convert.ToDecimal(dr["PromoApplied"]);
                                    string stProductCode = "" + dr["ProductCode"].ToString();
                                    if (dr["MatrixDescription"].ToString() != string.Empty && dr["MatrixDescription"].ToString() != null) stProductCode += "-" + dr["MatrixDescription"].ToString();
                                    decimal decQuantity = 0;

                                    if (dr["Quantity"].ToString().IndexOf("RETURN") != -1)
                                    {
                                        stProductCode = "" + dr["ProductCode"].ToString() + "-RET";
                                        decQuantity = Convert.ToDecimal(dr["Quantity"].ToString().Replace(" - RETURN", "").Trim());
                                        decAmount = -decAmount;
                                    }
                                    else if (dr["Quantity"].ToString() != "VOID")
                                    {
                                        decQuantity = Convert.ToDecimal(dr["Quantity"]);
                                    }

                                    if (dr["Quantity"].ToString().IndexOf("VOID") != -1)
                                    {
                                        if (mclsTerminalDetails.WillPrintVoidItem)
                                            if (mclsTerminalDetails.ReceiptType == TerminalReceiptType.Default)
                                                PrintItem(stItemNo, stProductCode, stProductUnitCode, decQuantity, decPrice, decDiscount, decPromoApplied, decAmount, decVAT, decEVAT);
                                    }
                                    else
                                    {
                                        if (mclsTerminalDetails.ReceiptType == TerminalReceiptType.Default)
                                            PrintItem(stItemNo, stProductCode, stProductUnitCode, decQuantity, decPrice, decDiscount, decPromoApplied, decAmount, decVAT, decEVAT);
                                    }
                                    
                                }
                                if (mclsTerminalDetails.ReceiptType == TerminalReceiptType.Default)
                                {
                                    PrintReportFooterSection(true, TransactionStatus.Closed, mclsSalesTransactionDetails.ItemSold, mclsSalesTransactionDetails.QuantitySold, mclsSalesTransactionDetails.SubTotal, mclsSalesTransactionDetails.Discount, mclsSalesTransactionDetails.Charge, mclsSalesTransactionDetails.AmountPaid, CashPayment, ChequePayment, CreditCardPayment, CreditPayment, DebitPayment, RewardPointsPayment, RewardConvertedPayment, ChangeAmount, arrChequePaymentDetails, arrCreditCardPaymentDetails, arrCreditPaymentDetails, arrDebitPaymentDetails);

                                    // print the charge slip if not refund and will print
                                    if (mclsTerminalDetails.WillPrintChargeSlip && !mboIsRefund)
                                    {
                                        clsEvent.AddEventLn("      printing charge slip...", true, mclsSysConfigDetails.WillWriteSystemLog);

                                        // Nov 05, 2011 : Print Charge Slip
                                        PrintChargeSlip(ChargeSlipType.Customer);
                                        PrintChargeSlip(ChargeSlipType.Original);
                                        if (!mclsTerminalDetails.IncludeCreditChargeAgreement) //do not print the guarantor if there is no agreement printed
                                        {
                                            PrintChargeSlip(ChargeSlipType.Guarantor);
                                        }
                                    }
                                }
                                //}
                                // Sep 14, 2014 Control printing in mclsFilePrinter.Write
                            }
                        }
                        catch (Exception ex)
                        {
                            clsEvent.AddErrorEventLn(ex);
                            clsEvent.AddEventLn("Error printing transaction no: " + lblTransNo.Text + ". Already commited in the database.", true);
                        }
                        #endregion

                        InsertAuditLog(AccessTypes.CloseTransaction, "Close transaction #: " + lblTransNo.Text + "... Subtotal: " + mclsSalesTransactionDetails.SubTotal.ToString("#,###.#0") + " Discount: " + mclsSalesTransactionDetails.Discount.ToString("#,###.#0") + " AmountPaid: " + mclsSalesTransactionDetails.AmountPaid.ToString("#,###.#0") + " CashPayment: " + CashPayment.ToString("#,###.#0") + " ChequePayment: " + ChequePayment.ToString("#,###.#0") + " CreditCardPayment: " + CreditCardPayment + " CreditPayment: " + CreditPayment.ToString("#,###.#0") + " DebitPayment: " + DebitPayment.ToString("#,###.#0") + " ChangeAmount: " + ChangeAmount.ToString("#,###.#0") + " @ Branch: " + mclsTerminalDetails.BranchDetails.BranchCode);

                        clsEvent.AddEventLn("Done! Transaction no. " + lblTransNo.Text + " has been closed. Subtotal: " + mclsSalesTransactionDetails.SubTotal.ToString("#,###.#0") + " Discount: " + mclsSalesTransactionDetails.Discount.ToString("#,###.#0") + " AmountPaid: " + mclsSalesTransactionDetails.AmountPaid.ToString("#,###.#0") + " CashPayment: " + CashPayment.ToString("#,###.#0") + " ChequePayment: " + ChequePayment.ToString("#,###.#0") + " CreditCardPayment: " + CreditCardPayment + " CreditPayment: " + CreditPayment.ToString("#,###.#0") + " DebitPayment: " + DebitPayment.ToString("#,###.#0") + " ChangeAmount: " + ChangeAmount.ToString("#,###.#0"), true);

                        clsEvent.AddEventLn(" Loading Options...", true, mclsSysConfigDetails.WillWriteSystemLog);
                        this.LoadOptions();
					}
				}
				catch (Exception ex)
				{
					InsertErrorLogToFile(ex, "ERROR!!! Closing transaction.");
				}
                this.KeyPreview = true;
			}
			Cursor.Current = Cursors.Default;
		}
		private void CloseTransactionAsOrderSlip()
		{
			if (!mboIsInTransaction)
			{
				MessageBox.Show("No active transaction found.", "RetailPlus", MessageBoxButtons.OK);
				return;
			}

			// Feb 13, 2009
			// overwrite cashierid and cashiername that will close the transaction
			// Aug 6, 2011 : Lemu
			// Remove this and put in ResumeTransaction 
			//try { mclsSalesTransactionDetails.CashierID = Convert.ToInt64(lblCashier.Tag); }
			//catch { }
			//mclsSalesTransactionDetails.CashierName = lblCashier.Text;
			DialogResult loginresult = GetWriteAccessAndLogin(mclsSalesTransactionDetails.CashierID, AccessTypes.CloseTransaction);

			if (loginresult == DialogResult.OK)
			{
				try
				{
					clsEvent.AddEventLn("[" + lblCashier.Text + "] Closing as order slip transaction no. " + lblTransNo.Text, true);


					// start a connection for the database.
					//update the transaction table 
					Data.SalesTransactions clsSalesTransactions = new Data.SalesTransactions(mConnection, mTransaction);
                    mConnection = clsSalesTransactions.Connection; mTransaction = clsSalesTransactions.Transaction;

					clsSalesTransactions.CloseAsOrderSlip(mclsSalesTransactionDetails.TransactionID);

                    // Sep 24, 2014 : update back the LastCheckInDate to min date
                    Data.Contacts clsContact = new Data.Contacts(mConnection, mTransaction);
                    mConnection = clsContact.Connection; mTransaction = clsContact.Transaction;

                    clsContact.UpdateLastCheckInDate(mclsSalesTransactionDetails.CustomerID, Constants.C_DATE_MIN_VALUE);

                    clsSalesTransactions.CommitAndDispose();

					InsertAuditLog(AccessTypes.CloseTransaction, "Close transaction #: " + lblTransNo.Text + "... Subtotal: " + mclsSalesTransactionDetails.SubTotal.ToString("#,###.#0") + " Discount: " + mclsSalesTransactionDetails.Discount.ToString("#,###.#0") + " AmountPaid: " + mclsSalesTransactionDetails.AmountPaid.ToString("#,###.#0") + " CashPayment: " + 0.ToString("#,###.#0") + " ChequePayment: " + 0.ToString("#,###.#0") + " CreditCardPayment: " + 0 + " CreditPayment: " + 0.ToString("#,###.#0") + " DebitPayment: " + 0.ToString("#,###.#0") + " ChangeAmount: " + 0.ToString("#,###.#0") + " @ Branch: " + mclsTerminalDetails.BranchDetails.BranchCode);
					
                    clsEvent.AddEventLn("Done! Transaction no. " + lblTransNo.Text + " has been closed as order slip. Subtotal: " + mclsSalesTransactionDetails.SubTotal.ToString("#,###.#0") + " Discount: " + mclsSalesTransactionDetails.Discount.ToString("#,###.#0") + " AmountPaid: " + mclsSalesTransactionDetails.AmountPaid.ToString("#,###.#0") + " CashPayment: " + 0.ToString("#,###.#0") + " ChequePayment: " + 0.ToString("#,###.#0") + " CreditCardPayment: " + 0 + " CreditPayment: " + 0.ToString("#,###.#0") + " DebitPayment: " + 0.ToString("#,###.#0") + " ChangeAmount: " + 0.ToString("#,###.#0"), true);
                    this.LoadOptions();

					MessageBox.Show("Transaction has been closed as ORDER SLIP.", "RetailPlus", MessageBoxButtons.OK);
				}
				catch (Exception ex)
				{
					InsertErrorLogToFile(ex, "ERROR!!! Closing transaction as ORDER SLIP.");
				}
			}
			Cursor.Current = Cursors.Default;
		}
		private void SelectProduct(bool isPriceInq)
		{
			Cursor.Current = Cursors.WaitCursor;
			string strSearchCode = string.Empty;
			DialogResult loginresult = GetWriteAccessAndLogin(mclsSalesTransactionDetails.CashierID, AccessTypes.CreateTransaction);

            if (loginresult == DialogResult.OK)
			{

			BackToSelectionProduct:
				try
				{
					DialogResult result = DialogResult.Cancel;
					string strSelectedBarCode = string.Empty;

                    ItemSelectWnd ItemWnd = new ItemSelectWnd();

                    ItemWnd.TerminalDetails = mclsTerminalDetails;
                    ItemWnd.SysConfigDetails = mclsSysConfigDetails;
                    ItemWnd.IsPriceInq = isPriceInq;
                    ItemWnd.SearchCode = strSearchCode;

                    if (isPriceInq == true || mboIsRefund || lblCustomer.Text.Trim().ToUpper() == Constants.C_RETAILPLUS_ORDER_SLIP_CUSTOMER)
                        ItemWnd.ShowItemMoreThanZeroQty = false;
                    else
                        ItemWnd.ShowItemMoreThanZeroQty = mclsTerminalDetails.ShowItemMoreThanZeroQty;
                    // Aug 6, 2011 : Lemu
                    // Include InAvtive Products during REFUND
                    if (mboIsRefund == true) ItemWnd.ShowInActiveProducts = true;

                    ItemWnd.ShowDialog(this);
                    result = ItemWnd.Result;
                    strSelectedBarCode = ItemWnd.SelectedBarCode;
                    strSearchCode = ItemWnd.SearchCode;
                    ItemWnd.Close();
                    ItemWnd.Dispose();

					if (result == DialogResult.OK)
					{
						txtBarCode.Text += strSelectedBarCode;
						if (!isPriceInq) ReadBarCode(); else ShowProductPrice();

						// Added June 15, 2010
						// Reload Product Selection Window if WillContinueSelectionProduct
						if (mclsTerminalDetails.WillContinueSelectionProduct == true)
						{
							goto BackToSelectionProduct;
						}
					}
				}
				catch (Exception ex)
				{
					InsertErrorLogToFile(ex, "ERROR!!! in selecting product...");
				}
			}
			Cursor.Current = Cursors.Default;
		}
		private void ReleaseItems()
		{
			if (mclsTerminalDetails.AutoPrint == PrintingPreference.Auto && mboIsInTransaction)
			{
				MessageBox.Show("Sorry you cannot release an item if Auto-print is ON and an item is already purchased.", "RetailPlus", MessageBoxButtons.OK, MessageBoxIcon.Warning);
				return;
			}
			DialogResult loginresult = GetWriteAccessAndLogin(mclsSalesTransactionDetails.CashierID, AccessTypes.ReleaseItems);

			if (loginresult == DialogResult.OK)
			{
				try
				{
					clsEvent.AddEventLn("[" + lblCashier.Text + "] Releasing items...");

					ItemReleaseWnd _ItemReleaseWnd = new ItemReleaseWnd();
					_ItemReleaseWnd.ReleaserID = long.Parse(lblCashier.Tag.ToString());
					_ItemReleaseWnd.ReleaserName = lblCashier.Text;
					_ItemReleaseWnd.TerminalDetails = mclsTerminalDetails;
					_ItemReleaseWnd.ShowDialog(this);
					_ItemReleaseWnd.Close();
					_ItemReleaseWnd.Dispose();

					clsEvent.AddEventLn("[" + lblCashier.Text + "] Done... Releasing items...");
				}
				catch (Exception ex)
				{ 
                    InsertErrorLogToFile(ex,"ERRROR!!! Releasing items."); 
                }
				Cursor.Current = Cursors.Default;
			}
		}

		private DialogResult LoggedOutCashier(bool LogOutOnly)
		{
			if (!mboLocked)
			{
                if (!SuspendTransactionAndContinue()) return System.Windows.Forms.DialogResult.Cancel;

				DialogResult loginresult = GetWriteAccessAndLogin(mclsSalesTransactionDetails.CashierID, AccessTypes.LogoutFE);

				if (loginresult == DialogResult.OK)
				{
					if (LogOutOnly)
					{
						if (MessageBox.Show("Are you sure you want to Log Out?", "RetailPlus", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.No)
						{
							return DialogResult.Cancel;
						}
					}

					Cursor.Current = Cursors.WaitCursor;
					try
					{
						clsEvent.AddEvent("[" + lblCashier.Text + "] Logging out...");

						CashierLogs clsCashierLogs = new CashierLogs(mConnection, mTransaction);
                        mConnection = clsCashierLogs.Connection; mTransaction = clsCashierLogs.Transaction;

						clsCashierLogs.Logout(Convert.ToInt64(lblCashierName.Tag));

                        InsertAuditLog(AccessTypes.LogoutFE, "User logout." + " @ Branch: " + mclsTerminalDetails.BranchDetails.BranchCode);

						clsEvent.AddEventLn("Done!");

						this.Lock();

						clsEvent.AddEventLn("System is now closed for any transaction!", true);
                        clsCashierLogs.CommitAndDispose();
					}
					catch (Exception ex)
					{
                        InsertErrorLogToFile(ex, "ERROR!!! Logging out cashier"); 
                    }
					return DialogResult.OK;
				}
				Cursor.Current = Cursors.Default;
				return DialogResult.Cancel;
			}
			return DialogResult.OK;
		}
		private void ApplyTransDiscount()
		{
			if (ItemDataTable.Rows.Count <= 0) return;

			if (mclsTerminalDetails.AutoPrint == PrintingPreference.Auto && mboIsInTransaction)
			{
				MessageBox.Show("Sorry you cannot apply a transaction discount if Auto-print is ON an item is already purchased.", "RetailPlus", MessageBoxButtons.OK, MessageBoxIcon.Warning);
				return;
			}
			DialogResult loginresult = GetWriteAccessAndLogin(mclsSalesTransactionDetails.CashierID, AccessTypes.ApplyTransDiscount);

			if (loginresult == DialogResult.OK)
			{
				//mboIsDiscountAuthorized = true;
				try
				{
					clsEvent.AddEvent("[" + lblCashier.Text + "] Applying transaction discount for trans. no. " + lblTransNo.Text);

				Back:
					DiscountTypes TransDiscountType = (DiscountTypes)Enum.Parse(typeof(DiscountTypes), lblTransDiscount.Tag.ToString());
					DiscountWnd discount = new DiscountWnd();
					discount.Header = "Apply Transaction Discount"; 
					discount.BalanceAmount = mclsSalesTransactionDetails.SubTotal;
					discount.DiscountType = TransDiscountType;
					discount.DiscountAmount = mclsSalesTransactionDetails.TransDiscount;
					discount.DiscountCode = mclsSalesTransactionDetails.DiscountCode;
					discount.Remarks = mclsSalesTransactionDetails.DiscountRemarks;
					discount.IsDiscountEditable = mclsTerminalDetails.IsDiscountEditable;
					discount.ShowDialog(this);
					DialogResult result = discount.Result;
					decimal DiscountAmount = discount.DiscountAmount;
					string TransDiscountCode = discount.DiscountCode;
					string TransDiscountRemarks = discount.Remarks;
					TransDiscountType = discount.DiscountType;
					discount.Close();
					discount.Dispose();

					if (result == DialogResult.OK)
					{
						int iCurrentSelectedRow = dgItems.CurrentRowIndex;

						Cursor.Current = Cursors.WaitCursor;
						if (!mboIsInTransaction) //false ito
						{
							this.LoadOptions();
							if (!this.CreateTransaction()) return;
						}

						decimal OldDiscount = mclsSalesTransactionDetails.TransDiscount;
						string OldTransDiscountType = lblTransDiscount.Tag.ToString();

						if (TransDiscountType == DiscountTypes.NotApplicable)
						{
							lblTransDiscount.Text = "Less 0% / 0.00";
						}
						lblTransDiscount.Tag = TransDiscountType.ToString("d");
						mclsSalesTransactionDetails.TransDiscountType = TransDiscountType;
						mclsSalesTransactionDetails.TransDiscount = DiscountAmount;
						mclsSalesTransactionDetails.DiscountCode = TransDiscountCode;
						mclsSalesTransactionDetails.DiscountRemarks = TransDiscountRemarks;

						ComputeSubTotal(); setTotalDetails();

						if (mclsSalesTransactionDetails.Discount <= mclsSalesTransactionDetails.DiscountableAmount)
						{
							Data.SalesTransactions clsSalesTransactions = new Data.SalesTransactions(mConnection, mTransaction);
                            mConnection = clsSalesTransactions.Connection; mTransaction = clsSalesTransactions.Transaction;

                            clsSalesTransactions.UpdateSubTotal(mclsSalesTransactionDetails.TransactionID, mclsSalesTransactionDetails.ItemSold, mclsSalesTransactionDetails.QuantitySold, mclsSalesTransactionDetails.SubTotal, mclsSalesTransactionDetails.NetSales, mclsSalesTransactionDetails.ItemsDiscount, mclsSalesTransactionDetails.Discount, mclsSalesTransactionDetails.SNRDiscount, mclsSalesTransactionDetails.PWDDiscount, mclsSalesTransactionDetails.OtherDiscount, mclsSalesTransactionDetails.TransDiscount, mclsSalesTransactionDetails.TransDiscountType, mclsSalesTransactionDetails.VAT, mclsSalesTransactionDetails.VATableAmount, mclsSalesTransactionDetails.NonVATableAmount, mclsSalesTransactionDetails.VATExempt, mclsSalesTransactionDetails.EVAT, mclsSalesTransactionDetails.EVATableAmount, mclsSalesTransactionDetails.NonEVATableAmount, mclsSalesTransactionDetails.LocalTax, mclsSalesTransactionDetails.DiscountCode, mclsSalesTransactionDetails.DiscountRemarks, mclsSalesTransactionDetails.Charge, mclsSalesTransactionDetails.ChargeAmount, mclsSalesTransactionDetails.ChargeCode, mclsSalesTransactionDetails.ChargeRemarks, mclsSalesTransactionDetails.ChargeType);
                            clsSalesTransactions.CommitAndDispose();

                            InsertAuditLog(AccessTypes.Discounts, "Apply transaction discount for " + mclsSalesTransactionDetails.Discount.ToString("#,###.#0") + ". Tran. #".PadRight(15) + ":" + lblTransNo.Text + " @ Branch: " + mclsTerminalDetails.BranchDetails.BranchCode);
						}
						else
						{
							MessageBox.Show("Sorry the input discount will yield a less than ZERO amount. Please type another discount.", "RetailPlus", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);
							mclsSalesTransactionDetails.TransDiscount = OldDiscount;
							lblTransDiscount.Tag = OldTransDiscountType;

							ComputeSubTotal(); setTotalDetails();
							goto Back;
						}
						dgItems.Select(iCurrentSelectedRow);
						clsEvent.AddEventLn("Done! amount=" + mclsSalesTransactionDetails.Discount.ToString("#,###.#0"));
					}
					else { clsEvent.AddEventLn("Cancelled!"); }
				}
				catch (Exception ex)
				{ 
                    InsertErrorLogToFile(ex, "ERROR!!! Applying transaction discount."); 
                }
				Cursor.Current = Cursors.Default;
			}
		}
        private void ApplyTransCharge()
        {
            int iRow = dgItems.CurrentRowIndex;
            if (iRow < 0) return;

            if (mclsTerminalDetails.AutoPrint == PrintingPreference.Auto && mboIsInTransaction)
            {
                MessageBox.Show("Sorry you cannot apply a transaction Charge if Auto-print is ON an item is already purchased.", "RetailPlus", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            DialogResult loginresult = GetWriteAccessAndLogin(mclsSalesTransactionDetails.CashierID, AccessTypes.ChargeType);

            if (loginresult == DialogResult.OK)
            {
                try
                {
                    clsEvent.AddEvent("[" + lblCashier.Text + "] Applying transaction Charge for trans. no. " + lblTransNo.Text);

                BackToCharge:
                    ChargeTypes TransChargeType = mclsSalesTransactionDetails.ChargeType;
                    ChargeWnd charge = new ChargeWnd();
                    charge.BalanceAmount = mclsSalesTransactionDetails.SubTotal;
                    charge.ChargeType = TransChargeType;
                    charge.ChargeAmount = mclsSalesTransactionDetails.ChargeAmount;
                    charge.ChargeCode = mclsSalesTransactionDetails.ChargeCode;
                    charge.Remarks = mclsSalesTransactionDetails.ChargeRemarks;
                    charge.IsChargeEditable = mclsTerminalDetails.IsChargeEditable;
                    charge.ShowDialog(this);
                    DialogResult result = charge.Result;
                    decimal ChargeAmount = charge.ChargeAmount;
                    string TransChargeCode = charge.ChargeCode;
                    string TransChargeRemarks = charge.Remarks;
                    TransChargeType = charge.ChargeType;
                    charge.Close();
                    charge.Dispose();

                    if (result == DialogResult.OK)
                    {
                        Cursor.Current = Cursors.WaitCursor;
                        if (!mboIsInTransaction) //false ito
                        {
                            this.LoadOptions();
                            if (!this.CreateTransaction()) return;
                        }

                        decimal OldCharge = mclsSalesTransactionDetails.ChargeAmount;
                        ChargeTypes OldTransChargeType = mclsSalesTransactionDetails.ChargeType;

                        mclsSalesTransactionDetails.ChargeAmount = ChargeAmount;
                        mclsSalesTransactionDetails.ChargeCode = TransChargeCode;
                        mclsSalesTransactionDetails.ChargeRemarks = TransChargeRemarks;
                        mclsSalesTransactionDetails.ChargeType = TransChargeType;

                        ComputeSubTotal(); setTotalDetails();

                        if (mclsSalesTransactionDetails.Discount <= mclsSalesTransactionDetails.DiscountableAmount)
                        {
                            Data.SalesTransactions clsSalesTransactions = new Data.SalesTransactions(mConnection, mTransaction);
                            mConnection = clsSalesTransactions.Connection; mTransaction = clsSalesTransactions.Transaction;

                            clsSalesTransactions.UpdateSubTotal(mclsSalesTransactionDetails.TransactionID, mclsSalesTransactionDetails.ItemSold, mclsSalesTransactionDetails.QuantitySold, mclsSalesTransactionDetails.SubTotal, mclsSalesTransactionDetails.NetSales, mclsSalesTransactionDetails.ItemsDiscount, mclsSalesTransactionDetails.Discount, mclsSalesTransactionDetails.SNRDiscount, mclsSalesTransactionDetails.PWDDiscount, mclsSalesTransactionDetails.OtherDiscount, mclsSalesTransactionDetails.TransDiscount, mclsSalesTransactionDetails.TransDiscountType, mclsSalesTransactionDetails.VAT, mclsSalesTransactionDetails.VATableAmount, mclsSalesTransactionDetails.NonVATableAmount, mclsSalesTransactionDetails.VATExempt, mclsSalesTransactionDetails.EVAT, mclsSalesTransactionDetails.EVATableAmount, mclsSalesTransactionDetails.NonEVATableAmount, mclsSalesTransactionDetails.LocalTax, mclsSalesTransactionDetails.ChargeCode, mclsSalesTransactionDetails.ChargeRemarks, mclsSalesTransactionDetails.Charge, mclsSalesTransactionDetails.ChargeAmount, mclsSalesTransactionDetails.ChargeCode, mclsSalesTransactionDetails.ChargeRemarks, mclsSalesTransactionDetails.ChargeType);
                            clsSalesTransactions.CommitAndDispose();

                            InsertAuditLog(AccessTypes.ChargeType, "Apply transaction Charge for " + mclsSalesTransactionDetails.Charge.ToString("#,###.#0") + ". Tran. #".PadRight(15) + ":" + lblTransNo.Text + " @ Branch: " + mclsTerminalDetails.BranchDetails.BranchCode);
                        }
                        else
                        {
                            MessageBox.Show("Sorry the input Charge will yield a less than ZERO amount. Please type another Charge.", "RetailPlus", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);
                            mclsSalesTransactionDetails.ChargeAmount = OldCharge;
                            mclsSalesTransactionDetails.ChargeType = OldTransChargeType;

                            ComputeSubTotal(); setTotalDetails();
                            goto BackToCharge;
                        }

                        clsEvent.AddEventLn("Done! amount=" + mclsSalesTransactionDetails.Charge.ToString("#,###.#0"));
                    }
                    else { clsEvent.AddEventLn("Cancelled!"); }
                }
                catch (Exception ex)
                {
                    InsertErrorLogToFile(ex, "ERROR!!! Applying transaction charge.");
                }
                Cursor.Current = Cursors.Default;
            }
        }

        private void ApplyTransDefaultCharge()
        {
            // must be called only during creation of transaction
            Cursor.Current = Cursors.WaitCursor;
            try
            {
                if (!string.IsNullOrEmpty(mclsTerminalDetails.DefaultTransactionChargeCode))
                {
                    clsEvent.AddEvent("[" + lblCashier.Text + "] Applying default transaction Charge for trans. no. " + lblTransNo.Text + ".");

                    Data.ChargeType clsChargeType = new Data.ChargeType(mConnection, mTransaction);
                    mConnection = clsChargeType.Connection; mTransaction = clsChargeType.Transaction;

                    Data.ChargeTypeDetails clsChargeTypeDetails = clsChargeType.Details(mclsTerminalDetails.DefaultTransactionChargeCode);

                    setTransCharge(clsChargeTypeDetails, "Default Transaction Charge Code: " + mclsTerminalDetails.DefaultTransactionChargeCode);

                    clsChargeType.CommitAndDispose();

                    InsertAuditLog(AccessTypes.ChargeType, "Apply transaction Charge for " + mclsSalesTransactionDetails.Charge.ToString("#,###.#0") + ". Tran. #".PadRight(15) + ":" + lblTransNo.Text + " @ Branch: " + mclsTerminalDetails.BranchDetails.BranchCode + ". DefaultTransactionCharge:" + mclsTerminalDetails.DefaultTransactionChargeCode);

                    clsEvent.AddEventLn("Done! amount=" + mclsSalesTransactionDetails.Charge.ToString("#,###.#0"));
                }
            }
            catch (Exception ex)
            {
                InsertErrorLogToFile(ex, "ERROR!!! Applying default transaction charge.");
            }
            Cursor.Current = Cursors.Default;
        }
        private void ApplyTransZeroCharge()
        {
            // must be called only during creation of transaction
            Cursor.Current = Cursors.WaitCursor;
            try
            {
                clsEvent.AddEvent("[" + mclsSalesTransactionDetails.CashierName + "] Applying zero transaction Charge for trans. no. " + mclsSalesTransactionDetails.TransactionNo + ".");

                Data.ChargeTypeDetails clsChargeTypeDetails = new Data.ChargeTypeDetails
                {
                    ChargeAmount = 0,
                    ChargeType = ChargeTypes.NotApplicable.ToString("G"),
                    ChargeTypeCode = "",
                    ChargeTypeID = 0,
                    InPercent = true,
                    CreatedOn = DateTime.Now,
                    LastModified = DateTime.Now
                };

                setTransCharge(clsChargeTypeDetails, "Zero Transaction Charge Code: " + mclsTerminalDetails.DefaultTransactionChargeCode);

                InsertAuditLog(AccessTypes.ChargeType, "Apply Zero transaction Charge for " + mclsSalesTransactionDetails.Charge.ToString("#,###.#0") + ". Tran. #".PadRight(15) + ":" + mclsSalesTransactionDetails.TransactionNo + " @ Branch: " + mclsTerminalDetails.BranchDetails.BranchCode + ". ZeroTransactionCharge");

                clsEvent.AddEventLn("Done! amount=" + mclsSalesTransactionDetails.Charge.ToString("#,###.#0"));
            }
            catch (Exception ex)
            {
                InsertErrorLogToFile(ex, "ERROR!!! Applying zero transaction charge.");
            }
            Cursor.Current = Cursors.Default;
        }
        private void setTransCharge(Data.ChargeTypeDetails ChargeTypeDetails, string Remarks)
        {
            try
            {
                mclsSalesTransactionDetails.ChargeAmount = ChargeTypeDetails.ChargeAmount;
                mclsSalesTransactionDetails.ChargeCode = ChargeTypeDetails.ChargeTypeCode;
                mclsSalesTransactionDetails.ChargeRemarks = Remarks;

                if (ChargeTypeDetails.ChargeAmount == 0)
                    mclsSalesTransactionDetails.ChargeType = ChargeTypes.NotApplicable;
                else if (ChargeTypeDetails.InPercent)
                    mclsSalesTransactionDetails.ChargeType = ChargeTypes.Percentage;
                else
                    mclsSalesTransactionDetails.ChargeType = ChargeTypes.FixedValue;

                ComputeSubTotal(); setTotalDetails();

                Data.SalesTransactions clsSalesTransactions = new Data.SalesTransactions(mConnection, mTransaction);
                mConnection = clsSalesTransactions.Connection; mTransaction = clsSalesTransactions.Transaction;

                clsSalesTransactions.UpdateSubTotal(mclsSalesTransactionDetails.TransactionID, mclsSalesTransactionDetails.ItemSold, mclsSalesTransactionDetails.QuantitySold, mclsSalesTransactionDetails.SubTotal, mclsSalesTransactionDetails.NetSales, mclsSalesTransactionDetails.ItemsDiscount, mclsSalesTransactionDetails.Discount, mclsSalesTransactionDetails.SNRDiscount, mclsSalesTransactionDetails.PWDDiscount, mclsSalesTransactionDetails.OtherDiscount, mclsSalesTransactionDetails.TransDiscount, mclsSalesTransactionDetails.TransDiscountType, mclsSalesTransactionDetails.VAT, mclsSalesTransactionDetails.VATableAmount, mclsSalesTransactionDetails.NonVATableAmount, mclsSalesTransactionDetails.VATExempt, mclsSalesTransactionDetails.EVAT, mclsSalesTransactionDetails.EVATableAmount, mclsSalesTransactionDetails.NonEVATableAmount, mclsSalesTransactionDetails.LocalTax, mclsSalesTransactionDetails.ChargeCode, mclsSalesTransactionDetails.ChargeRemarks, mclsSalesTransactionDetails.Charge, mclsSalesTransactionDetails.ChargeAmount, mclsSalesTransactionDetails.ChargeCode, mclsSalesTransactionDetails.ChargeRemarks, mclsSalesTransactionDetails.ChargeType);
                clsSalesTransactions.CommitAndDispose();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
		private void ChangeOrderType()
		{
			if (mclsTerminalDetails.AutoPrint == PrintingPreference.Auto && mboIsInTransaction)
			{
				MessageBox.Show("Sorry you cannot change Order Type if Auto-print is ON an item is already purchased.", "RetailPlus", MessageBoxButtons.OK, MessageBoxIcon.Warning);
				return;
			}
			DialogResult loginresult = GetWriteAccessAndLogin(mclsSalesTransactionDetails.CashierID, AccessTypes.CloseTransaction, "Change Order Type");

            if (loginresult == DialogResult.OK)
			{
				try
				{
					clsEvent.AddEvent("[" + lblCashier.Text + "] Changing order type of trans. no. " + lblTransNo.Text);

                    OrderTypeWnd clsOrderTypeWnd = new OrderTypeWnd();
                    clsOrderTypeWnd.ShowDialog(this);
                    DialogResult result = clsOrderTypeWnd.Result;
                    OrderTypes clsOrderType = clsOrderTypeWnd.orderType;
                    clsOrderTypeWnd.Close();
                    clsOrderTypeWnd.Dispose();

					if (result == DialogResult.OK)
					{
						Cursor.Current = Cursors.WaitCursor;
						if (!mboIsInTransaction)
						{
							this.LoadOptions();
							if (!this.CreateTransaction()) return;
						}

                        mclsSalesTransactionDetails.OrderType = clsOrderType;

                        Data.SalesTransactions clsSalesTransactions = new Data.SalesTransactions(mConnection, mTransaction);
                        mConnection = clsSalesTransactions.Connection; mTransaction = clsSalesTransactions.Transaction;

                        clsSalesTransactions.UpdateOrderType(mclsSalesTransactionDetails.TransactionID, mclsSalesTransactionDetails.TransactionDate, mclsSalesTransactionDetails.OrderType);
                        InsertAuditLog(AccessTypes.ChargeType, "Change order type to " + mclsSalesTransactionDetails.OrderType.ToString("G") + ". Tran. #".PadRight(15) + ":" + lblTransNo.Text + " @ Branch: " + mclsTerminalDetails.BranchDetails.BranchCode);
                        clsEvent.AddEventLn("Done!");

                        if (clsOrderType != OrderTypes.DineIn && mclsSalesTransactionDetails.CustomerID == Constants.C_RETAILPLUS_CUSTOMERID)
                        {
                            Int64 iOldContactID = mclsSalesTransactionDetails.CustomerID;

                            SelectContact(AceSoft.RetailPlus.Data.ContactGroupCategory.CUSTOMER);

                            if (iOldContactID != mclsSalesTransactionDetails.CustomerID)
                            {
                                Data.Contacts clsContacts = new Data.Contacts(mConnection, mTransaction);
                                mConnection = clsContacts.Connection; mTransaction = clsContacts.Transaction;
                                clsContacts.UpdateLastCheckInDate(iOldContactID, Constants.C_DATE_MIN_VALUE);
                                clsContacts.UpdateLastCheckInDate(mclsSalesTransactionDetails.CustomerID, mclsSalesTransactionDetails.TransactionDate);
                                clsContacts.CommitAndDispose();
                            }
                        }
                        if (mclsSalesTransactionDetails.CustomerID == Constants.C_RETAILPLUS_CUSTOMERID)
                        {
                            mclsSalesTransactionDetails.OrderType = OrderTypes.DineIn;

                            clsSalesTransactions = new Data.SalesTransactions(mConnection, mTransaction);
                            mConnection = clsSalesTransactions.Connection; mTransaction = clsSalesTransactions.Transaction;

                            clsSalesTransactions.UpdateOrderType(mclsSalesTransactionDetails.TransactionID, mclsSalesTransactionDetails.TransactionDate, mclsSalesTransactionDetails.OrderType);
                            InsertAuditLog(AccessTypes.ChargeType, "System override order type to " + mclsSalesTransactionDetails.OrderType.ToString("G") + ". Tran. #".PadRight(15) + ":" + lblTransNo.Text + " @ Branch: " + mclsTerminalDetails.BranchDetails.BranchCode);

                        }

                        clsSalesTransactions.CommitAndDispose();

                        lblOrderType.Text = mclsSalesTransactionDetails.OrderType.ToString("G").ToUpper();

                        // [09/24/2014] apply the default charge if changed to dine in

                        Data.ChargeType clsChargeType = new Data.ChargeType(mConnection, mTransaction);
                        mConnection = clsChargeType.Connection; mTransaction = clsChargeType.Transaction;

                        Data.ChargeTypeDetails clsChargeTypeDetails = new Data.ChargeTypeDetails();
                        if (mclsSalesTransactionDetails.OrderType == OrderTypes.DineIn && !string.IsNullOrEmpty(mclsTerminalDetails.DineInChargeCode))
                        {
                            clsChargeTypeDetails = clsChargeType.Details(mclsTerminalDetails.DineInChargeCode);
                            setTransCharge(clsChargeTypeDetails, "Change Order Type to Dine-In. Charge Code:" + mclsTerminalDetails.DineInChargeCode);
                        }
                        else if (mclsSalesTransactionDetails.OrderType == OrderTypes.TakeOut && !string.IsNullOrEmpty(mclsTerminalDetails.TakeOutChargeCode))
                        {
                            clsChargeTypeDetails = clsChargeType.Details(mclsTerminalDetails.TakeOutChargeCode);
                            setTransCharge(clsChargeTypeDetails, "Change Order Type to Take-Out. Charge Code:" + mclsTerminalDetails.TakeOutChargeCode);
                        }
                        else if (mclsSalesTransactionDetails.OrderType == OrderTypes.Delivery && !string.IsNullOrEmpty(mclsTerminalDetails.DeliveryChargeCode))
                        {
                            clsChargeTypeDetails = clsChargeType.Details(mclsTerminalDetails.DeliveryChargeCode);
                            setTransCharge(clsChargeTypeDetails, "Change Order Type to Delivery. Charge Code:" + mclsTerminalDetails.DeliveryChargeCode);
                        }
                        else
                        {
                            ApplyTransZeroCharge();
                        }

                        clsChargeType.CommitAndDispose();
                    }
                    else { clsEvent.AddEventLn("Cancelled!"); }
				}
				catch (Exception ex)
				{ 
                    InsertErrorLogToFile(ex, "ERROR!!! Changing order type."); 
                }
				Cursor.Current = Cursors.Default;
			}
		}
		private void OpenTransactionDrawer()
		{
            if (!SuspendTransactionAndContinue()) return;

			DialogResult loginresult = GetWriteAccessAndLogin(mclsSalesTransactionDetails.CashierID, AccessTypes.OpenDrawer);

			if (loginresult == DialogResult.OK)
			{
				OpenDrawerDelegate opendrawerDel = new OpenDrawerDelegate(OpenDrawer);
				Invoke(opendrawerDel);
			}
		}

        private void VerifyCredit()
        { 
            if (!SuspendTransactionAndContinue()) return;

            DialogResult loginresult = GetWriteAccessAndLogin(mclsSalesTransactionDetails.CashierID, AccessTypes.EnterCreditPayment);

            if (loginresult == DialogResult.OK)
            {
                CreditVerificationWnd clsCreditVerificationWnd = new CreditVerificationWnd();
                clsCreditVerificationWnd.ShowDialog(this);
                DialogResult result = clsCreditVerificationWnd.Result;
                Data.ContactDetails details = clsCreditVerificationWnd.CreditorDetails;
                clsCreditVerificationWnd.Close();
                clsCreditVerificationWnd.Dispose();

                if (result == DialogResult.OK)
                {
                    Cursor.Current = Cursors.WaitCursor;

                    LocalDB clsLocalDB = new LocalDB(mConnection, mTransaction);
                    mConnection = clsLocalDB.Connection; mTransaction = clsLocalDB.Transaction;

                    try
                    {
                        //print the verification slip
                        PrintCreditVerificationSlip(details);
                    }
                    catch (Exception ex)
                    {
                        InsertErrorLogToFile(ex, "ERROR!!! Credit verification procedure. Err Description: ");
                    }
                    clsLocalDB.CommitAndDispose();
                }
            }
        }
        private void EnterCreditItemizePayment()
        {
            if (!SuspendTransactionAndContinue()) return;

            DialogResult loginresult = GetWriteAccessAndLogin(mclsSalesTransactionDetails.CashierID, AccessTypes.EnterCreditPayment);

            if (loginresult == DialogResult.OK)
            {
                ContactSelectWnd ContactWnd = new ContactSelectWnd();
                ContactWnd.HasCreditOnly = true;
                ContactWnd.ShowDialog(this);
                DialogResult result = ContactWnd.Result;
                Data.ContactDetails details = ContactWnd.Details;
                ContactWnd.Close();
                ContactWnd.Dispose();

                if (result == DialogResult.OK)
                {
                    CreditsItemizeWnd creditWnd = new CreditsItemizeWnd();
                    creditWnd.TerminalDetails = mclsTerminalDetails;
                    creditWnd.CustomerDetails = details;
                    creditWnd.ShowDialog(this);

                    decimal AmountPaid = creditWnd.AmountPayment;
                    decimal CashPayment = creditWnd.CashPayment;
                    decimal ChequePayment = creditWnd.ChequePayment;
                    decimal CreditCardPayment = creditWnd.CreditCardPayment;
                    decimal DebitPayment = creditWnd.DebitPayment;
                    decimal BalanceAmount = creditWnd.BalanceAmount;
                    decimal ChangeAmount = creditWnd.ChangeAmount;
                    PaymentTypes PaymentType = creditWnd.PaymentType;
                    ArrayList arrCashPaymentDetails = creditWnd.CashPaymentDetails;
                    ArrayList arrChequePaymentDetails = creditWnd.ChequePaymentDetails;
                    ArrayList arrCreditCardPaymentDetails = creditWnd.CreditCardPaymentDetails;
                    ArrayList arrDebitPaymentDetails = creditWnd.DebitPaymentDetails;
                    result = creditWnd.Result;

                    creditWnd.Close();
                    creditWnd.Dispose();

                    if (result == DialogResult.OK)
                    {
                        Cursor.Current = Cursors.WaitCursor;

                        LocalDB clsLocalDB = new LocalDB(mConnection, mTransaction);
                        mConnection = clsLocalDB.Connection; mTransaction = clsLocalDB.Transaction;

                        try
                        {
                            Data.Products clsProducts = new Data.Products(mConnection, mTransaction);
                            mConnection = clsProducts.Connection; mTransaction = clsProducts.Transaction;

                            if (clsProducts.Details(Data.Products.DEFAULT_CREDIT_PAYMENT_BARCODE).ProductID == 0)
                            {
                                clsProducts.CREATE_CREDITPAYMENT_PRODUCT();
                                Methods.InsertAuditLog(mclsTerminalDetails, "System Administrator", AccessTypes.EnterCreditPayment, "CREDIT PAYMENT product has been created coz it's not configured");
                            }

                            /************** April 21, 2006: added transaction no. ***************/
                            lblCustomer.Tag = details.ContactID;
                            lblCustomer.Text = details.ContactName;

                            clsEvent.AddEvent("[" + lblCashier.Text + "] Creating " + Data.Products.DEFAULT_CREDIT_PAYMENT_BARCODE + " transaction for customer: ");
                            LoadContact(AceSoft.RetailPlus.Data.ContactGroupCategory.CUSTOMER, details);

                            if (!this.CreateTransaction()) return;

                            txtBarCode.Text = Data.Products.DEFAULT_CREDIT_PAYMENT_BARCODE;
                            ReadBarCode();
                            int iRow = dgItems.CurrentRowIndex;

                            Data.SalesTransactionItemDetails Details = getCurrentRowItemDetails();
                            Details.Price = AmountPaid;
                            Details.Amount = AmountPaid;

                            Data.SalesTransactions clsSalesTransactions = new Data.SalesTransactions(mConnection, mTransaction);
                            mConnection = clsSalesTransactions.Connection; mTransaction = clsSalesTransactions.Transaction;

                            ApplyChangeQuantityPriceAmountDetails(iRow, Details, "Change Quantity, Price, Amount for Credit Payment");

                            // for assignment of payments
                            mclsSalesTransactionDetails.PaymentDetails = AssignArrayListPayments(arrCashPaymentDetails, arrChequePaymentDetails, arrCreditCardPaymentDetails, null, arrDebitPaymentDetails);

                            SavePayments(arrCashPaymentDetails, arrChequePaymentDetails, arrCreditCardPaymentDetails, null, arrDebitPaymentDetails);

                            OpenDrawerDelegate opendrawerDel = new OpenDrawerDelegate(OpenDrawer);
                            Invoke(opendrawerDel);

                            //update the transaction table 
                            Int64 iTransactionID = Convert.ToInt64(lblTransNo.Tag);
                            string strORNo = ""; // no need to put an OR no for credit payment coz it's already declared before
                            clsSalesTransactions.Close(mclsSalesTransactionDetails.TransactionID, strORNo, 0, 0, AmountPaid, AmountPaid, 0, 0, 0, 0, 0, 0, DiscountTypes.NotApplicable, 0, 0, 0, 0, 0, 0, 0, 0, AmountPaid, CashPayment, ChequePayment, CreditCardPayment, 0, DebitPayment, 0, 0, 0, 0, PaymentType, null, null, 0, 0, null, null, mclsSalesTransactionDetails.CashierID, lblCashier.Text, TransactionStatus.CreditPayment);

                            //UpdateTerminalReportDelegate updateterminalDel = new UpdateTerminalReportDelegate(UpdateTerminalReport);
                            UpdateTerminalReport(TransactionStatus.CreditPayment, 0, 0, AmountPaid, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, CashPayment, ChequePayment, CreditCardPayment, 0, DebitPayment, 0, 0, PaymentType);

                            //UpdateCashierReportDelegate updatecashierDel = new UpdateCashierReportDelegate(UpdateCashierReport);
                            UpdateCashierReport(TransactionStatus.CreditPayment, 0, 0, AmountPaid, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, CashPayment, ChequePayment, CreditCardPayment, 0, DebitPayment, 0, 0, PaymentType);
                            clsSalesTransactions.CommitAndDispose();

                            InsertAuditLog(AccessTypes.CreditPayment, "Pay credit for " + details.ContactName + "." + " @ Branch: " + mclsTerminalDetails.BranchDetails.BranchCode);

                            if (mclsTerminalDetails.AutoPrint == PrintingPreference.AskFirst)
                                if (MessageBox.Show("Would you like to print this transaction?", "RetailPlus", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
                                    mclsTerminalDetails.AutoPrint = PrintingPreference.Normal;

                            // Sep 14, 2014 Control printing in mclsFilePrinter.Write
                            //if (mclsTerminalDetails.AutoPrint == PrintingPreference.Normal)	//print items if not yet printed
                            //{
                            foreach (System.Data.DataRow dr in ItemDataTable.Rows)
                            {
                                if (dr["Quantity"].ToString() != "VOID")
                                {
                                    string stItemNo = "" + dr["ItemNo"].ToString();
                                    string stProductCode = "" + dr["ProductCode"].ToString();
                                    if (dr["MatrixDescription"].ToString() != string.Empty && dr["MatrixDescription"].ToString() != null) stProductCode += "-" + dr["MatrixDescription"].ToString();
                                    string stProductUnitCode = "" + dr["ProductUnitCode"].ToString();
                                    decimal decQuantity = Convert.ToDecimal(dr["Quantity"]);
                                    decimal decPrice = Convert.ToDecimal(dr["Price"]);
                                    decimal decDiscount = Convert.ToDecimal(dr["Discount"]);
                                    decimal decAmount = Convert.ToDecimal(dr["Amount"]);
                                    decimal decVAT = Convert.ToDecimal(dr["VAT"]);
                                    decimal decEVAT = Convert.ToDecimal(dr["EVAT"]);
                                    decimal decPromoApplied = Convert.ToDecimal(dr["PromoApplied"]);

                                    PrintItem(stItemNo, stProductCode, stProductUnitCode, decQuantity, decPrice, decDiscount, decPromoApplied, decAmount, decVAT, decEVAT);
                                }
                            }
                            //}
                            // Sep 14, 2014 Control printing in mclsFilePrinter.Write

                            PrintReportFooterSection(true, TransactionStatus.CreditPayment, 0, 0, AmountPaid, 0, 0, AmountPaid, CashPayment, ChequePayment, CreditCardPayment, 0, DebitPayment, 0, 0, ChangeAmount, arrChequePaymentDetails, arrCreditCardPaymentDetails, null, arrDebitPaymentDetails);

                            this.LoadOptions();
                        }
                        catch (Exception ex)
                        {
                            InsertErrorLogToFile(ex, "ERROR!!! Credit payment procedure. Err Description: ");
                        }
                        clsLocalDB.CommitAndDispose();
                        Cursor.Current = Cursors.Default;
                    }
                }
            }
        }
		private void RefundTransaction()
		{
            if (!SuspendTransactionAndContinue()) return;

			DialogResult loginresult = GetWriteAccessAndLogin(mclsSalesTransactionDetails.CashierID, AccessTypes.RefundTransaction);

			if (loginresult == DialogResult.OK)
			{
				if (MessageBox.Show("Press OK to issue REFUND transaction or CANCEL to disregard.", "RetailPlus", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) == DialogResult.Cancel)
					return;

				Cursor.Current = Cursors.WaitCursor;

				try
				{
					clsEvent.AddEventLn("Issuing REFUND Transaction!");
					LoadOptions();

                    mboIsRefund = true;
                    if (!this.CreateTransaction()) return;
					
					// Aug 6, 2011 : Lemu
					// Include items with zero quantity for REFUND
					mclsTerminalDetails.ShowItemMoreThanZeroQty = false;

					lblSubtotalName.Text = "SUBTOTAL: REFUND";
					lblOrderType.Visible = false;
					InsertAuditLog(AccessTypes.RefundTransaction, "Initialize refund transaction defaults. Tran. #".PadRight(15) + ":" + lblTransNo.Text + " @ Branch: " + mclsTerminalDetails.BranchDetails.BranchCode);
					clsEvent.AddEventLn("Done! Tran. #" + lblTransNo.Text);
				}
				catch (Exception ex)
                { 
                    InsertErrorLogToFile(ex, "ERROR!!! Refunding transaction."); 
                }

				Cursor.Current = Cursors.Default;
			}
		}

		private void IssueRewardCard()
		{
			if (mboIsInTransaction)
			{
				MessageBox.Show("Sorry you cannot issue a Reward Card while there is an ongoing transaction. Please finish the transaction first.", "RetailPlus", MessageBoxButtons.OK, MessageBoxIcon.Warning);
				return;
			}

			DialogResult loginresult = GetWriteAccessAndLogin(mclsSalesTransactionDetails.CashierID, AccessTypes.RewardCardIssuance);

            if (loginresult == DialogResult.OK)
			{
				try
				{
					DialogResult result; Data.ContactDetails clsContactDetails;
					ContactSelectWnd clsContactWnd = new ContactSelectWnd();
                    clsContactWnd.TerminalDetails = mclsTerminalDetails;
					clsContactWnd.ContactGroupCategory = AceSoft.RetailPlus.Data.ContactGroupCategory.CUSTOMER;
                    clsContactWnd.Header = "Please select customer for reward card issuance.";
					clsContactWnd.ShowDialog(this);
					clsContactDetails = clsContactWnd.Details;
					result = clsContactWnd.Result;
					clsContactWnd.Close();
					clsContactWnd.Dispose();

					if (result != DialogResult.OK)
					{ return; }

					if (clsContactDetails.ContactID == Constants.ZERO || clsContactDetails.ContactID == Constants.C_RETAILPLUS_CUSTOMERID)
					{ return; }

					clsEvent.AddEvent("[" + lblCashier.Text + "] Issuing reward card no to " + clsContactDetails.ContactName);

					Data.Contacts clsContact = new Data.Contacts(mConnection, mTransaction);
					clsContactDetails = clsContact.Details(clsContactDetails.ContactID);
					clsContact.CommitAndDispose();

					if (clsContactDetails.RewardDetails.RewardCardNo != string.Empty && clsContactDetails.RewardDetails.RewardCardNo != null)
					{
						clsEvent.AddEventLn("Cancelled!");
						clsEvent.AddEventLn("Reward Card No: " + clsContactDetails.RewardDetails.RewardCardNo + " was already issued to " + clsContactDetails.ContactName + " on " + clsContactDetails.RewardDetails.RewardAwardDate.ToString("MMM dd, yyyy hh:mm tt"));
						MessageBox.Show("Reward Card No: " + clsContactDetails.RewardDetails.RewardCardNo + " was already issued to " + clsContactDetails.ContactName + " on " + clsContactDetails.RewardDetails.RewardAwardDate.ToString("MMM dd, yyyy hh:mm tt") + "." + 
										Environment.NewLine + " Please select another customer to issue Reward Card.", "RetailPlus", MessageBoxButtons.OK, MessageBoxIcon.Warning);
						return;
					}
					
					clsEvent.AddEvent("[" + lblCashier.Text + "] Issuing reward card no to " + clsContactDetails.ContactName);

					ContactRewardWnd clsContactRewardWnd = new ContactRewardWnd();
					clsContactRewardWnd.Caption = "Reward Card Issuance";
					clsContactRewardWnd.ContactDetails = clsContactDetails;
					clsContactRewardWnd.RewardCardStatus = RewardCardStatus.New;
					clsContactRewardWnd.ShowDialog(this);
					result = clsContactRewardWnd.Result;
					clsContactDetails = clsContactRewardWnd.ContactDetails;
					clsContactRewardWnd.Close();
					clsContactRewardWnd.Dispose();

					if (result == DialogResult.OK)
					{
                        Data.Products clsProducts = new Data.Products(mConnection, mTransaction);
                        mConnection = clsProducts.Connection; mTransaction = clsProducts.Transaction;

                        if (clsProducts.Details(Data.Products.DEFAULT_ADVANTAGE_CARD_MEMBERSHIP_FEE_BARCODE).ProductID == 0)
                        {
                            clsProducts.CREATE_ADVANTAGE_CARD_MEMBERSHIP_FEE_BARCODE_PRODUCT();
                            Methods.InsertAuditLog(mclsTerminalDetails, "System Administrator", AccessTypes.RewardCardIssuance, "ADVANTAGE_CARD_MEMBERSHIP_FEE_BARCODE product has been created coz it's not configured");
                        }
                        clsProducts.CommitAndDispose();

						MessageBox.Show("Reward Card No: " + clsContactDetails.RewardDetails.RewardCardNo + " was issued to " + clsContactDetails.ContactName + "." + 
										Environment.NewLine + "Please collect the payment then close the transaction.", "RetailPlus", MessageBoxButtons.OK, MessageBoxIcon.Warning);

						clsEvent.AddEventLn("Done!");
						clsEvent.AddEventLn("Reward Card No: " + clsContactDetails.RewardDetails.RewardCardNo + " was issued to " + clsContactDetails.ContactName + ".", true);

                        LocalDB clsLocalDB = new LocalDB(mConnection, mTransaction);
                        mConnection = clsLocalDB.Connection; mTransaction = clsLocalDB.Transaction;

						clsEvent.AddEvent("[" + lblCashier.Text + "] Creating " + Data.Products.DEFAULT_ADVANTAGE_CARD_MEMBERSHIP_FEE_BARCODE + "transaction for customer: ");
						LoadContact(AceSoft.RetailPlus.Data.ContactGroupCategory.CUSTOMER, clsContactDetails);
						if (!this.CreateTransaction()) return;

						txtBarCode.Text = Data.Products.DEFAULT_ADVANTAGE_CARD_MEMBERSHIP_FEE_BARCODE;
						ReadBarCode();
						int iRow = dgItems.CurrentRowIndex;
						
						txtBarCode.Text = "";
						CloseTransaction();

                        clsLocalDB.CommitAndDispose();
					}
					else { clsEvent.AddEventLn("Cancelled!"); }
				}
				catch (Exception ex)
                { 
                    InsertErrorLogToFile(ex, "ERROR!!! Issuing reward card."); 
                }
				Cursor.Current = Cursors.Default;
			}
			
		}
		private void RenewRewardCard()
		{
			if (mboIsInTransaction)
			{
				MessageBox.Show("Sorry you cannot renew a Reward Card while there is an ongoing transaction. Please finish the transaction first.", "RetailPlus", MessageBoxButtons.OK, MessageBoxIcon.Warning);
				return;
			}

			DialogResult loginresult = GetWriteAccessAndLogin(mclsSalesTransactionDetails.CashierID, AccessTypes.RewardCardChange);

			if (loginresult == DialogResult.OK)
			{
				try
				{
					DialogResult result; Data.ContactDetails clsContactDetails;
					ContactSelectWnd clsContactWnd = new ContactSelectWnd();
                    clsContactWnd.TerminalDetails = mclsTerminalDetails;
					clsContactWnd.ContactGroupCategory = AceSoft.RetailPlus.Data.ContactGroupCategory.CUSTOMER;
                    clsContactWnd.Header = "Please select customer for reward card renewal.";
					clsContactWnd.ShowDialog(this);
					clsContactDetails = clsContactWnd.Details;
					result = clsContactWnd.Result;
					clsContactWnd.Close();
					clsContactWnd.Dispose();

					if (result != DialogResult.OK)
					{ return; }

					if (clsContactDetails.ContactID == Constants.ZERO || clsContactDetails.ContactID == Constants.C_RETAILPLUS_CUSTOMERID)
					{ return; }

					clsEvent.AddEvent("[" + lblCashier.Text + "] Renewing reward card.");

					Data.Contacts clsContact = new Data.Contacts(mConnection, mTransaction);
                    mConnection = clsContact.Connection; mTransaction = clsContact.Transaction;

					clsContactDetails = clsContact.Details(clsContactDetails.ContactID);
					clsContact.CommitAndDispose();

					if (clsContactDetails.RewardDetails.RewardCardNo == string.Empty || clsContactDetails.RewardDetails.RewardCardNo == null)
					{
						clsEvent.AddEventLn("Cancelled!");
						clsEvent.AddEventLn(clsContactDetails.ContactName + " has no valid Reward Card yet. ");
						MessageBox.Show(clsContactDetails.ContactName + " has no valid Reward Card yet. Please select another customer.", "RetailPlus", MessageBoxButtons.OK, MessageBoxIcon.Warning);
						return;
					}
					clsEvent.AddEvent("[" + lblCashier.Text + "] Renewing reward card #: " + clsContactDetails.RewardDetails.RewardCardNo + " of " + clsContactDetails.ContactName + ".");

					ContactRewardWnd clsContactRewardWnd = new ContactRewardWnd();
					clsContactRewardWnd.Caption = "Reward Card Renewal";
					clsContactRewardWnd.ContactDetails = clsContactDetails;
					clsContactRewardWnd.RewardCardStatus = RewardCardStatus.ReNew;
					clsContactRewardWnd.ShowDialog(this);
					result = clsContactRewardWnd.Result;
					clsContactDetails = clsContactRewardWnd.ContactDetails;
					clsContactRewardWnd.Close();
					clsContactRewardWnd.Dispose();

					if (result == DialogResult.OK)
					{
                        Data.Products clsProducts = new Data.Products(mConnection, mTransaction);
                        mConnection = clsProducts.Connection; mTransaction = clsProducts.Transaction;

                        if (clsProducts.Details(Data.Products.DEFAULT_ADVANTAGE_CARD_RENEWAL_FEE_BARCODE).ProductID == 0)
                        {
                            clsProducts.CREATE_ADVANTAGE_CARD_RENEWAL_FEE_BARCODE_PRODUCT();
                            Methods.InsertAuditLog(mclsTerminalDetails, "System Administrator", AccessTypes.RewardCardChange, "ADVANTAGE_CARD_RENEWAL_FEE_BARCODE product has been created coz it's not configured");
                        }
                        clsProducts.CommitAndDispose();

						MessageBox.Show("Reward Card No: " + clsContactDetails.RewardDetails.RewardCardNo + " has been renewed with new expiry date " + clsContactDetails.RewardDetails.ExpiryDate.ToString("yyyy-MM-dd") + ".", "RetailPlus", MessageBoxButtons.OK, MessageBoxIcon.Warning);

						clsEvent.AddEventLn("Done!");
						clsEvent.AddEventLn("Reward Card No: " + clsContactDetails.RewardDetails.RewardCardNo + " has been renewed with new expiry date " + clsContactDetails.RewardDetails.ExpiryDate.ToString("yyyy-MM-dd") + ".", true);

                        LocalDB clsLocalDB = new LocalDB(mConnection, mTransaction);
                        mConnection = clsLocalDB.Connection; mTransaction = clsLocalDB.Transaction;

						clsEvent.AddEvent("[" + lblCashier.Text + "] Creating " + Data.Products.DEFAULT_ADVANTAGE_CARD_RENEWAL_FEE_BARCODE + "transaction for customer: ");
						LoadContact(AceSoft.RetailPlus.Data.ContactGroupCategory.CUSTOMER, clsContactDetails);
						if (!this.CreateTransaction()) return;

						txtBarCode.Text = Data.Products.DEFAULT_ADVANTAGE_CARD_RENEWAL_FEE_BARCODE;
						ReadBarCode();
						int iRow = dgItems.CurrentRowIndex;

						txtBarCode.Text = "";
						CloseTransaction();

                        clsLocalDB.CommitAndDispose();
					}
					else { clsEvent.AddEventLn("Cancelled!"); }
				}
				catch (Exception ex)
                { 
                    InsertErrorLogToFile(ex, "ERROR!!! Renewing reward card."); 
                }
				Cursor.Current = Cursors.Default;
			}
		}
		private void RewardCardReplacement(RewardCardStatus pvtRewardCardStatus)
		{
			if (mboIsInTransaction)
			{
				MessageBox.Show("Sorry you cannot replace a Reward Card while there is an ongoing transaction. Please finish the transaction first.", "RetailPlus", MessageBoxButtons.OK, MessageBoxIcon.Warning);
				return;
			}

			DialogResult loginresult = GetWriteAccessAndLogin(mclsSalesTransactionDetails.CashierID, AccessTypes.RewardCardChange);

			if (loginresult == DialogResult.OK)
			{
				try
				{
					DialogResult result; Data.ContactDetails clsContactDetails;
					ContactSelectWnd clsContactWnd = new ContactSelectWnd();
                    clsContactWnd.TerminalDetails = mclsTerminalDetails;
					clsContactWnd.ContactGroupCategory = AceSoft.RetailPlus.Data.ContactGroupCategory.CUSTOMER;
                    clsContactWnd.Header = "Please select customer for reward card replacement.";
					clsContactWnd.ShowDialog(this);
					clsContactDetails = clsContactWnd.Details;
					result = clsContactWnd.Result;
					clsContactWnd.Close();
					clsContactWnd.Dispose();

					if (result != DialogResult.OK)
					{ return; }

					if (clsContactDetails.ContactID == Constants.ZERO || clsContactDetails.ContactID == Constants.C_RETAILPLUS_CUSTOMERID)
					{ return; }

					clsEvent.AddEvent("[" + lblCashier.Text + "] Replacing reward card...");

					Data.Contacts clsContact = new Data.Contacts(mConnection, mTransaction);
                    mConnection = clsContact.Connection; mTransaction = clsContact.Transaction;

					clsContactDetails = clsContact.Details(clsContactDetails.ContactID);
					clsContact.CommitAndDispose();

					if (clsContactDetails.RewardDetails.RewardCardNo == string.Empty || clsContactDetails.RewardDetails.RewardCardNo == null)
					{
						clsEvent.AddEventLn("Cancelled!");
						clsEvent.AddEventLn(clsContactDetails.ContactName + " has no valid Reward Card yet. ");
						MessageBox.Show(clsContactDetails.ContactName + " has no valid Reward Card yet. Please select another customer.", "RetailPlus", MessageBoxButtons.OK, MessageBoxIcon.Warning);
						return;
					}
					clsEvent.AddEvent("[" + lblCashier.Text + "] Replacing reward card #: " + clsContactDetails.RewardDetails.RewardCardNo + " of " + clsContactDetails.ContactName + " as " + pvtRewardCardStatus.ToString("G"));

					string strOldRewardCardNo = clsContactDetails.RewardDetails.RewardCardNo;
					ContactRewardWnd clsContactRewardWnd = new ContactRewardWnd();
					if (pvtRewardCardStatus == RewardCardStatus.Replaced_Lost)
						clsContactRewardWnd.Caption = "Reward Card Replacement of LOST CARD ";
					else if (pvtRewardCardStatus == RewardCardStatus.Replaced_Expired)
						clsContactRewardWnd.Caption = "Reward Card Replacement of EXPIRED CARD ";
					clsContactRewardWnd.ContactDetails = clsContactDetails;
					clsContactRewardWnd.RewardCardStatus = pvtRewardCardStatus;
					clsContactRewardWnd.ShowDialog(this);
					result = clsContactRewardWnd.Result;
					clsContactDetails = clsContactRewardWnd.ContactDetails;
					clsContactRewardWnd.Close();
					clsContactRewardWnd.Dispose();

					if (result == DialogResult.OK)
					{
                        Data.Products clsProducts = new Data.Products(mConnection, mTransaction);
                        mConnection = clsProducts.Connection; mTransaction = clsProducts.Transaction;

                        if (clsProducts.Details(Data.Products.DEFAULT_ADVANTAGE_CARD_REPLACEMENT_FEE_BARCODE).ProductID == 0)
                        {
                            clsProducts.CREATE_ADVANTAGE_CARD_REPLACEMENT_FEE_BARCODE_PRODUCT();
                            Methods.InsertAuditLog(mclsTerminalDetails, "System Administrator", AccessTypes.RewardCardChange, "ADVANTAGE_CARD_REPLACEMENT_FEE_BARCODE product has been created coz it's not configured");
                        }
                        clsProducts.CommitAndDispose();

						MessageBox.Show("Reward Card No: " + strOldRewardCardNo + " has been replaced with new card #: " + clsContactDetails.RewardDetails.RewardCardNo + ".", "RetailPlus", MessageBoxButtons.OK, MessageBoxIcon.Warning);

						clsEvent.AddEventLn("Done!");
						clsEvent.AddEventLn("Reward Card No: " + strOldRewardCardNo + " has been replaced with new card #: " + clsContactDetails.RewardDetails.RewardCardNo + ".", true);

                        LocalDB clsLocalDB = new LocalDB(mConnection, mTransaction);
                        mConnection = clsLocalDB.Connection; mTransaction = clsLocalDB.Transaction;

						clsEvent.AddEvent("[" + lblCashier.Text + "] Creating " + Data.Products.DEFAULT_ADVANTAGE_CARD_REPLACEMENT_FEE_BARCODE + "transaction for customer: ");
						LoadContact(AceSoft.RetailPlus.Data.ContactGroupCategory.CUSTOMER, clsContactDetails);
						if (!this.CreateTransaction()) return;

						txtBarCode.Text = Data.Products.DEFAULT_ADVANTAGE_CARD_REPLACEMENT_FEE_BARCODE;
						ReadBarCode();
						int iRow = dgItems.CurrentRowIndex;

						txtBarCode.Text = "";
						CloseTransaction();

                        clsLocalDB.CommitAndDispose();
					}
					else { clsEvent.AddEventLn("Cancelled!"); }
				}
				catch (Exception ex)
				{ 
                    InsertErrorLogToFile(ex, "ERROR!!! Replacing reward card."); 
                }
				Cursor.Current = Cursors.Default;
			}
		}
		private void RewardCardReactivate()
		{
			if (mboIsInTransaction)
			{
				MessageBox.Show("Sorry you cannot reactivate a Reward Card while there is an ongoing transaction. Please finish the transaction first.", "RetailPlus", MessageBoxButtons.OK, MessageBoxIcon.Warning);
				return;
			}

			DialogResult loginresult = GetWriteAccessAndLogin(mclsSalesTransactionDetails.CashierID, AccessTypes.RewardCardChange, "LOST Reward Card Reactivation");

            if (loginresult == DialogResult.OK)
			{
				try
				{
					DialogResult result; Data.ContactDetails clsContactDetails;
					ContactSelectWnd clsContactWnd = new ContactSelectWnd();
                    clsContactWnd.TerminalDetails = mclsTerminalDetails;
					clsContactWnd.ContactGroupCategory = AceSoft.RetailPlus.Data.ContactGroupCategory.CUSTOMER;
                    clsContactWnd.Header = "Please select customer for reward card re-activation.";
					clsContactWnd.ShowDialog(this);
					clsContactDetails = clsContactWnd.Details;
					result = clsContactWnd.Result;
					clsContactWnd.Close();
					clsContactWnd.Dispose();

					if (result != DialogResult.OK)
					{ return; }

					if (clsContactDetails.ContactID == Constants.ZERO || clsContactDetails.ContactID == Constants.C_RETAILPLUS_CUSTOMERID)
					{ return; }

					clsEvent.AddEvent("[" + lblCashier.Text + "] Reactivating lost reward card...");

					Data.Contacts clsContact = new Data.Contacts(mConnection, mTransaction);
                    mConnection = clsContact.Connection; mTransaction = clsContact.Transaction;

					clsContactDetails = clsContact.Details(clsContactDetails.ContactID);
					clsContact.CommitAndDispose();

					if (clsContactDetails.RewardDetails.RewardCardNo == string.Empty || clsContactDetails.RewardDetails.RewardCardNo == null)
					{
						clsEvent.AddEventLn("Cancelled!");
						clsEvent.AddEventLn(clsContactDetails.ContactName + " has no valid Reward Card yet. ");
						MessageBox.Show(clsContactDetails.ContactName + " has no valid Reward Card yet. Please select another customer.", "RetailPlus", MessageBoxButtons.OK, MessageBoxIcon.Warning);
						return;
					}
					clsEvent.AddEvent("[" + lblCashier.Text + "] Reactivating reward card #: " + clsContactDetails.RewardDetails.RewardCardNo + " of " + clsContactDetails.ContactName + ".");

					string strOldRewardCardNo = clsContactDetails.RewardDetails.RewardCardNo;
					ContactRewardWnd clsContactRewardWnd = new ContactRewardWnd();
					clsContactRewardWnd.Caption = "OVERRIDE: Reward Card Reactivation / Change Expiry";
					clsContactRewardWnd.ContactDetails = clsContactDetails;
					clsContactRewardWnd.RewardCardStatus = RewardCardStatus.Reactivated_Lost;
					clsContactRewardWnd.ShowDialog(this);
					result = clsContactRewardWnd.Result;
					clsContactDetails = clsContactRewardWnd.ContactDetails;
					clsContactRewardWnd.Close();
					clsContactRewardWnd.Dispose();

					if (result == DialogResult.OK)
					{
						MessageBox.Show("Reward Card No: " + clsContactDetails.RewardDetails.RewardCardNo + " has been reactivated / changed expiry date / changed card no...", "RetailPlus", MessageBoxButtons.OK, MessageBoxIcon.Warning);

						clsEvent.AddEventLn("Done!");
						clsEvent.AddEventLn("Reward Card No: " + clsContactDetails.RewardDetails.RewardCardNo + " has been reactivated / changed expiry date / changed card no...", true);
						this.LoadOptions();
					}
					else { clsEvent.AddEventLn("Cancelled!"); }
				}
				catch (Exception ex)
				{ 
                    InsertErrorLogToFile(ex, "ERROR!!! Activating reward card."); 
                }
				Cursor.Current = Cursors.Default;
			}
		}
		private void RewardCardDeclareAsLost()
		{
			if (mboIsInTransaction)
			{
				MessageBox.Show("Sorry you cannot declare a Reward Card as LOST while there is an ongoing transaction. Please finish the transaction first.", "RetailPlus", MessageBoxButtons.OK, MessageBoxIcon.Warning);
				return;
			}

			DialogResult loginresult = GetWriteAccessAndLogin(mclsSalesTransactionDetails.CashierID, AccessTypes.RewardCardChange, "Reward Card Declaration as LOST");

            if (loginresult == DialogResult.OK)
			{
				try
				{
					DialogResult result; Data.ContactDetails clsContactDetails;
					ContactSelectWnd clsContactWnd = new ContactSelectWnd();
                    clsContactWnd.TerminalDetails = mclsTerminalDetails;
					clsContactWnd.ContactGroupCategory = AceSoft.RetailPlus.Data.ContactGroupCategory.CUSTOMER;
                    clsContactWnd.Header = "Please select customer for reward card declaration as lost.";
					clsContactWnd.ShowDialog(this);
					clsContactDetails = clsContactWnd.Details;
					result = clsContactWnd.Result;
					clsContactWnd.Close();
					clsContactWnd.Dispose();

					if (result != DialogResult.OK)
					{ return; }

					if (clsContactDetails.ContactID == Constants.ZERO || clsContactDetails.ContactID == Constants.C_RETAILPLUS_CUSTOMERID)
					{ return; }

					clsEvent.AddEvent("[" + lblCashier.Text + "] Declaring reward card as LOST.");

					Data.Contacts clsContact = new Data.Contacts(mConnection, mTransaction);
                    mConnection = clsContact.Connection; mTransaction = clsContact.Transaction;

					clsContactDetails = clsContact.Details(clsContactDetails.ContactID);
					clsContact.CommitAndDispose();

					if (clsContactDetails.RewardDetails.RewardCardNo == string.Empty || clsContactDetails.RewardDetails.RewardCardNo == null)
					{
						clsEvent.AddEventLn("Cancelled!");
						clsEvent.AddEventLn(clsContactDetails.ContactName + " has no valid Reward Card yet. ");
						MessageBox.Show(clsContactDetails.ContactName + " has no valid Reward Card yet. Please select another customer.", "RetailPlus", MessageBoxButtons.OK, MessageBoxIcon.Warning);
						return;
					}

					ContactRewardWnd clsContactRewardWnd = new ContactRewardWnd();
					clsContactRewardWnd.Caption = "Reward Card Declaration as LOST";
					clsContactRewardWnd.ContactDetails = clsContactDetails;
					clsContactRewardWnd.RewardCardStatus = RewardCardStatus.Lost;
					clsContactRewardWnd.ShowDialog(this);
					result = clsContactRewardWnd.Result;
					clsContactDetails = clsContactRewardWnd.ContactDetails;
					clsContactRewardWnd.Close();
					clsContactRewardWnd.Dispose();

					if (result == DialogResult.OK)
					{
						MessageBox.Show("Reward Card No: " + clsContactDetails.RewardDetails.RewardCardNo + " has been declared as LOST.", "RetailPlus", MessageBoxButtons.OK, MessageBoxIcon.Warning);

						clsEvent.AddEventLn("Done!");
						clsEvent.AddEventLn("Reward Card No: " + clsContactDetails.RewardDetails.RewardCardNo + " has been declared as LOST.", true);
						this.LoadOptions();
					}
					else { clsEvent.AddEventLn("Cancelled!"); }
				}
				catch (Exception ex)
				{ 
                    InsertErrorLogToFile(ex,"ERRROR!!! Declaring reward card as lost."); 
                }
				Cursor.Current = Cursors.Default;
			}
		}

		private void IssueCreditCard()
		{
			if (mboIsInTransaction)
			{
				MessageBox.Show("Sorry you cannot issue a Credit Card while there is an ongoing transaction. Please finish the transaction first.", "RetailPlus", MessageBoxButtons.OK, MessageBoxIcon.Warning);
				return;
			}

			DialogResult loginresult = GetWriteAccessAndLogin(mclsSalesTransactionDetails.CashierID, AccessTypes.CreditCardIssuance);

			if (loginresult == DialogResult.OK)
			{
				try
				{
					DialogResult result;
                    Data.CardTypeDetails clsCardTypeDetails = new Data.CardTypeDetails();
                    Data.ContactDetails clsGuarantorDetails = new AceSoft.RetailPlus.Data.ContactDetails();
                    ContactCreditTypeSelectWnd clsContactCreditTypeSelectWnd = new ContactCreditTypeSelectWnd();
                    clsContactCreditTypeSelectWnd.ShowDialog(this);
                    clsCardTypeDetails = clsContactCreditTypeSelectWnd.CardTypeDetails;
                    result = clsContactCreditTypeSelectWnd.Result;
                    clsContactCreditTypeSelectWnd.Close();
                    clsContactCreditTypeSelectWnd.Dispose();

                    if (result != DialogResult.OK)
                    { return; }

					Data.ContactDetails clsContactDetails;
					ContactSelectWnd clsContactWnd = new ContactSelectWnd();
                    clsContactWnd.TerminalDetails = mclsTerminalDetails;

                    if (clsCardTypeDetails.WithGuarantor)
					{
                        MessageBox.Show("Please select a GUARANTOR to issue Credit Card.", "RetailPlus", MessageBoxButtons.OK, MessageBoxIcon.Information);

						clsContactWnd.ContactGroupCategory = AceSoft.RetailPlus.Data.ContactGroupCategory.CUSTOMER;
                        clsContactWnd.Header = "Select GUARANTOR to issue Credit Card.";
						clsContactWnd.ShowDialog(this);
						clsGuarantorDetails = clsContactWnd.Details;
						result = clsContactWnd.Result;
						clsContactWnd.Close();
						clsContactWnd.Dispose();
						
						if (result != DialogResult.OK)
						{ return; }

						MessageBox.Show(clsGuarantorDetails.ContactName + " has been selected as guarantor." + Environment.NewLine + "Please select the CUSTOMER to issue Credit Card.", "RetailPlus", MessageBoxButtons.OK, MessageBoxIcon.Information);
					}
					clsContactWnd = new ContactSelectWnd();
					clsContactWnd.ContactGroupCategory = AceSoft.RetailPlus.Data.ContactGroupCategory.CUSTOMER;
                    clsContactWnd.Header = "Select CUSTOMER to issue Credit Card.";
					clsContactWnd.ShowDialog(this);
					clsContactDetails = clsContactWnd.Details;
					result = clsContactWnd.Result;
					clsContactWnd.Close();
					clsContactWnd.Dispose();

					if (result != DialogResult.OK)
					{ return; }

					if (clsContactDetails.ContactID == Constants.ZERO || clsContactDetails.ContactID == Constants.C_RETAILPLUS_CUSTOMERID)
					{ return; }

                    // if no guarantor
                    if (!clsCardTypeDetails.WithGuarantor) clsGuarantorDetails = new Data.ContactDetails();

					clsEvent.AddEvent("[" + lblCashier.Text + "] Issuing credit card no to " + clsContactDetails.ContactName);

					Data.Contacts clsContact = new Data.Contacts(mConnection, mTransaction);
                    mConnection = clsContact.Connection; mTransaction = clsContact.Transaction;

					clsContactDetails = clsContact.Details(clsContactDetails.ContactID);
					clsContact.CommitAndDispose();

					if (clsContactDetails.CreditDetails.CreditCardNo != string.Empty && clsContactDetails.CreditDetails.CreditCardNo != null)
					{
						clsEvent.AddEventLn("Cancelled!");
						clsEvent.AddEventLn("Credit Card No: " + clsContactDetails.CreditDetails.CreditCardNo + " was already issued to " + clsContactDetails.ContactName + " on " + clsContactDetails.CreditDetails.CreditAwardDate.ToString("MMM dd, yyyy hh:mm tt"));
						MessageBox.Show("Credit Card No: " + clsContactDetails.CreditDetails.CreditCardNo + " was already issued to " + clsContactDetails.ContactName + " on " + clsContactDetails.CreditDetails.CreditAwardDate.ToString("MMM dd, yyyy hh:mm tt") + "." +
                                        Environment.NewLine + " Please select another customer to issue Credit Card.", "RetailPlus", MessageBoxButtons.OK, MessageBoxIcon.Error);
						return;
					}

					clsEvent.AddEvent("[" + lblCashier.Text + "] Issuing credit card no to " + clsContactDetails.ContactName);

					ContactCreditWnd clsContactCreditWnd = new ContactCreditWnd();
					clsContactCreditWnd.Header = "Credit Card Issuance";
                    clsContactCreditWnd.CardTypeDetails = clsCardTypeDetails;
					clsContactCreditWnd.Guarantor = clsGuarantorDetails;
					clsContactCreditWnd.ContactDetails = clsContactDetails;
					clsContactCreditWnd.CreditCardStatus = CreditCardStatus.New;
					clsContactCreditWnd.ShowDialog(this);
					result = clsContactCreditWnd.Result;
					clsContactDetails = clsContactCreditWnd.ContactDetails;
					clsContactCreditWnd.Close();
					clsContactCreditWnd.Dispose();

					if (result == DialogResult.OK)
					{
                        

                        Data.Products clsProducts = new Data.Products(mConnection, mTransaction);
                        mConnection = clsProducts.Connection; mTransaction = clsProducts.Transaction;

                        string strProductBarcode = Data.Products.DEFAULT_CREDIT_CARD_MEMBERSHIP_FEE_BARCODE;
                        //override if with Guarantor
                        if (clsCardTypeDetails.WithGuarantor)
                            strProductBarcode = Data.Products.DEFAULT_SUPER_CARD_MEMBERSHIP_FEE_BARCODE;

                        if (clsProducts.Details(strProductBarcode).ProductID == 0)
                        {
                            if (!clsCardTypeDetails.WithGuarantor)
                                clsProducts.CREATE_CREDIT_CARD_MEMBERSHIP_FEE_BARCODE_PRODUCT();
                            else
                                clsProducts.CREATE_SUPER_CARD_MEMBERSHIP_FEE_BARCODE_PRODUCT();

                            Methods.InsertAuditLog(mclsTerminalDetails, "System Administrator", AccessTypes.RewardCardChange, strProductBarcode + " product has been created coz it's not configured");
                        }
                        clsProducts.CommitAndDispose();

						MessageBox.Show("Credit Card No: " + clsContactDetails.CreditDetails.CreditCardNo + " was issued to " + clsContactDetails.ContactName + "." +
										Environment.NewLine + "Please collect the payment then close the transaction.", "RetailPlus", MessageBoxButtons.OK, MessageBoxIcon.Information);

						clsEvent.AddEventLn("Done!");
						clsEvent.AddEventLn("Credit Card No: " + clsContactDetails.CreditDetails.CreditCardNo + " was issued to " + clsContactDetails.ContactName + ".", true);

                        LocalDB clsLocalDB = new LocalDB(mConnection, mTransaction);
                        mConnection = clsLocalDB.Connection; mTransaction = clsLocalDB.Transaction;

                        clsEvent.AddEvent("[" + lblCashier.Text + "] Creating " + strProductBarcode + "transaction for customer: ");
						LoadContact(AceSoft.RetailPlus.Data.ContactGroupCategory.CUSTOMER, clsContactDetails);
						if (!this.CreateTransaction()) return;

                        txtBarCode.Text = strProductBarcode;
						ReadBarCode();
						int iRow = dgItems.CurrentRowIndex;

						txtBarCode.Text = "";
						CloseTransaction();

                        clsLocalDB.CommitAndDispose();
					}
					else { clsEvent.AddEventLn("Cancelled!"); }
				}
				catch (Exception ex)
				{ 
                    InsertErrorLogToFile(ex, "ERROR!!! Issuing internal credit-card."); 
                }
				Cursor.Current = Cursors.Default;
			}

		}
		private void RenewCreditCard()
		{
			if (mboIsInTransaction)
			{
				MessageBox.Show("Sorry you cannot renew a Credit Card while there is an ongoing transaction. Please finish the transaction first.", "RetailPlus", MessageBoxButtons.OK, MessageBoxIcon.Warning);
				return;
			}

            DialogResult loginresult = GetWriteAccessAndLogin(mclsSalesTransactionDetails.CashierID, AccessTypes.CreditCardRenewal);

            if (loginresult == DialogResult.OK)
			{
				try
				{
					DialogResult result; Data.ContactDetails clsContactDetails;
					ContactSelectWnd clsContactWnd = new ContactSelectWnd();
                    clsContactWnd.TerminalDetails = mclsTerminalDetails;
					clsContactWnd.ContactGroupCategory = AceSoft.RetailPlus.Data.ContactGroupCategory.CUSTOMER;
                    clsContactWnd.Header = "Please select customer for credit card renewal.";
					clsContactWnd.ShowDialog(this);
					clsContactDetails = clsContactWnd.Details;
					result = clsContactWnd.Result;
					clsContactWnd.Close();
					clsContactWnd.Dispose();

					if (result != DialogResult.OK)
					{ return; }

					if (clsContactDetails.ContactID == Constants.ZERO || clsContactDetails.ContactID == Constants.C_RETAILPLUS_CUSTOMERID)
					{ return; }

					clsEvent.AddEvent("[" + lblCashier.Text + "] Renewing credit card.");

					Data.Contacts clsContact = new Data.Contacts(mConnection, mTransaction);
                    mConnection = clsContact.Connection; mTransaction = clsContact.Transaction;

					clsContactDetails = clsContact.Details(clsContactDetails.ContactID);
                    Data.ContactDetails clsGuarantor = clsContact.Details(clsContactDetails.CreditDetails.GuarantorID);

					clsContact.CommitAndDispose();

					if (string.IsNullOrEmpty(clsContactDetails.CreditDetails.CreditCardNo))
					{
						clsEvent.AddEventLn("Cancelled!");
						clsEvent.AddEventLn(clsContactDetails.ContactName + " has no valid Credit Card yet. ");
						MessageBox.Show(clsContactDetails.ContactName + " has no valid Credit Card yet. Please select another customer.", "RetailPlus", MessageBoxButtons.OK, MessageBoxIcon.Error);
						return;
					}
					clsEvent.AddEvent("[" + lblCashier.Text + "] Renewing credit card #: " + clsContactDetails.CreditDetails.CreditCardNo + " of " + clsContactDetails.ContactName + ".");

					ContactCreditWnd clsContactCreditWnd = new ContactCreditWnd();
					clsContactCreditWnd.Header = "Credit Card Renewal";
                    clsContactCreditWnd.CardTypeDetails = clsContactDetails.CreditDetails.CardTypeDetails;
                    clsContactCreditWnd.Guarantor = clsGuarantor;
					clsContactCreditWnd.ContactDetails = clsContactDetails;
					clsContactCreditWnd.CreditCardStatus = CreditCardStatus.ReNew;
					clsContactCreditWnd.ShowDialog(this);
					result = clsContactCreditWnd.Result;
					clsContactDetails = clsContactCreditWnd.ContactDetails;
					clsContactCreditWnd.Close();
					clsContactCreditWnd.Dispose();

					if (result == DialogResult.OK)
					{
                        Data.Products clsProducts = new Data.Products(mConnection, mTransaction);
                        mConnection = clsProducts.Connection; mTransaction = clsProducts.Transaction;

                        string strProductBarcode = Data.Products.DEFAULT_CREDIT_CARD_RENEWAL_FEE_BARCODE;
                        //override if with Guarantor
                        if (clsContactDetails.CreditDetails.CardTypeDetails.WithGuarantor)
                            strProductBarcode = Data.Products.DEFAULT_SUPER_CARD_RENEWAL_FEE_BARCODE;

                        if (clsProducts.Details(strProductBarcode).ProductID == 0)
                        {
                            if (!clsContactDetails.CreditDetails.CardTypeDetails.WithGuarantor)
                                clsProducts.CREATE_CREDIT_CARD_RENEWAL_FEE_BARCODE_PRODUCT();
                            else
                                clsProducts.CREATE_SUPER_CARD_RENEWAL_FEE_BARCODE_PRODUCT();

                            Methods.InsertAuditLog(mclsTerminalDetails, "System Administrator", AccessTypes.CreditCardRenewal, strProductBarcode + " product has been created coz it's not configured");
                        }
                        clsProducts.CommitAndDispose();

                        MessageBox.Show("Credit Card No: " + clsContactDetails.CreditDetails.CreditCardNo + " has been renewed with new expiry date " + clsContactDetails.CreditDetails.ExpiryDate.ToString("yyyy-MM-dd") + ".", "RetailPlus", MessageBoxButtons.OK, MessageBoxIcon.Information);

						clsEvent.AddEventLn("Done!");
						clsEvent.AddEventLn("Credit Card No: " + clsContactDetails.CreditDetails.CreditCardNo + " has been renewed with new expiry date " + clsContactDetails.CreditDetails.ExpiryDate.ToString("yyyy-MM-dd") + ".", true);

                        LocalDB clsLocalDB = new LocalDB(mConnection, mTransaction);
                        mConnection = clsLocalDB.Connection; mTransaction = clsLocalDB.Transaction;

						clsEvent.AddEvent("[" + lblCashier.Text + "] Creating " + strProductBarcode + "transaction for customer: ");
						LoadContact(AceSoft.RetailPlus.Data.ContactGroupCategory.CUSTOMER, clsContactDetails);
						if (!this.CreateTransaction()) return;

                        txtBarCode.Text = strProductBarcode;
						ReadBarCode();
						int iRow = dgItems.CurrentRowIndex;

						txtBarCode.Text = "";
						CloseTransaction();

                        clsLocalDB.CommitAndDispose();
					}
					else { clsEvent.AddEventLn("Cancelled!"); }
				}
				catch (Exception ex)
				{ 
                    InsertErrorLogToFile(ex, "ERROR!!! Renewing internal credit card."); 
                }
				Cursor.Current = Cursors.Default;
			}
		}
		private void CreditCardReplacement(CreditCardStatus pvtCreditCardStatus)
		{
			if (mboIsInTransaction)
			{
				MessageBox.Show("Sorry you cannot replace a Credit Card while there is an ongoing transaction. Please finish the transaction first.", "RetailPlus", MessageBoxButtons.OK, MessageBoxIcon.Warning);
				return;
			}

			DialogResult loginresult = GetWriteAccessAndLogin(mclsSalesTransactionDetails.CashierID, AccessTypes.CreditCardChange);

            if (loginresult == DialogResult.OK)
			{
				try
				{
					DialogResult result; Data.ContactDetails clsContactDetails;
					ContactSelectWnd clsContactWnd = new ContactSelectWnd();
                    clsContactWnd.TerminalDetails = mclsTerminalDetails;
					clsContactWnd.ContactGroupCategory = AceSoft.RetailPlus.Data.ContactGroupCategory.CUSTOMER;
                    clsContactWnd.Header = "Please select customer for credit card replacement.";
					clsContactWnd.ShowDialog(this);
					clsContactDetails = clsContactWnd.Details;
					result = clsContactWnd.Result;
					clsContactWnd.Close();
					clsContactWnd.Dispose();

					if (result != DialogResult.OK)
					{ return; }

					if (clsContactDetails.ContactID == Constants.ZERO || clsContactDetails.ContactID == Constants.C_RETAILPLUS_CUSTOMERID)
					{ return; }

					clsEvent.AddEvent("[" + lblCashier.Text + "] Replacing credit card...");

                    Data.Contacts clsContact = new Data.Contacts(mConnection, mTransaction);
                    mConnection = clsContact.Connection; mTransaction = clsContact.Transaction;

					clsContactDetails = clsContact.Details(clsContactDetails.ContactID);
                    Data.ContactDetails clsGuarantor = clsContact.Details(clsContactDetails.CreditDetails.GuarantorID);
					clsContact.CommitAndDispose();

					if (clsContactDetails.CreditDetails.CreditCardNo == string.Empty || clsContactDetails.CreditDetails.CreditCardNo == null)
					{
						clsEvent.AddEventLn("Cancelled!");
						clsEvent.AddEventLn(clsContactDetails.ContactName + " has no valid Credit Card yet. ");
						MessageBox.Show(clsContactDetails.ContactName + " has no valid Credit Card yet. Please select another customer.", "RetailPlus", MessageBoxButtons.OK, MessageBoxIcon.Error);
						return;
					}
					clsEvent.AddEvent("[" + lblCashier.Text + "] Replacing credit card #: " + clsContactDetails.CreditDetails.CreditCardNo + " of " + clsContactDetails.ContactName + " as " + pvtCreditCardStatus.ToString("G"));

					string strOldCreditCardNo = clsContactDetails.CreditDetails.CreditCardNo;
					ContactCreditWnd clsContactCreditWnd = new ContactCreditWnd();
					if (pvtCreditCardStatus == CreditCardStatus.Replaced_Lost)
						clsContactCreditWnd.Header = "Credit Card Replacement of LOST CARD ";
					else if (pvtCreditCardStatus == CreditCardStatus.Replaced_Expired)
						clsContactCreditWnd.Header = "Credit Card Replacement of EXPIRED CARD ";
                    clsContactCreditWnd.CardTypeDetails = clsContactDetails.CreditDetails.CardTypeDetails;
					clsContactCreditWnd.Guarantor = clsGuarantor;
					clsContactCreditWnd.ContactDetails = clsContactDetails;
					clsContactCreditWnd.CreditCardStatus = pvtCreditCardStatus;
					clsContactCreditWnd.ShowDialog(this);
					result = clsContactCreditWnd.Result;
					clsContactDetails = clsContactCreditWnd.ContactDetails;
					clsContactCreditWnd.Close();
					clsContactCreditWnd.Dispose();

					if (result == DialogResult.OK)
					{
                        Data.Products clsProducts = new Data.Products(mConnection, mTransaction);
                        mConnection = clsProducts.Connection; mTransaction = clsProducts.Transaction;

                        string strProductBarcode = Data.Products.DEFAULT_CREDIT_CARD_REPLACEMENT_FEE_BARCODE;
                        //override if with Guarantor
                        if (clsContactDetails.CreditDetails.CardTypeDetails.WithGuarantor)
                            strProductBarcode = Data.Products.DEFAULT_SUPER_CARD_REPLACEMENT_FEE_BARCODE;

                        if (clsProducts.Details(strProductBarcode).ProductID == 0)
                        {
                            if (!clsContactDetails.CreditDetails.CardTypeDetails.WithGuarantor)
                                clsProducts.CREATE_CREDIT_CARD_REPLACEMENT_FEE_BARCODE_PRODUCT();
                            else
                                clsProducts.CREATE_SUPER_CARD_REPLACEMENT_FEE_BARCODE_PRODUCT();

                            Methods.InsertAuditLog(mclsTerminalDetails, "System Administrator", AccessTypes.CreditCardChange, strProductBarcode + " product has been created coz it's not configured");
                        }
                        clsProducts.CommitAndDispose();

                        MessageBox.Show("Credit Card No: " + strOldCreditCardNo + " has been replaced with new card #: " + clsContactDetails.CreditDetails.CreditCardNo + ".", "RetailPlus", MessageBoxButtons.OK, MessageBoxIcon.Information);

						clsEvent.AddEventLn("Done!");
						clsEvent.AddEventLn("Credit Card No: " + strOldCreditCardNo + " has been replaced with new card #: " + clsContactDetails.CreditDetails.CreditCardNo + ".", true);

                        LocalDB clsLocalDB = new LocalDB(mConnection, mTransaction);
                        mConnection = clsLocalDB.Connection; mTransaction = clsLocalDB.Transaction;

						clsEvent.AddEvent("[" + lblCashier.Text + "] Creating " + strProductBarcode + "transaction for customer: ");
						LoadContact(AceSoft.RetailPlus.Data.ContactGroupCategory.CUSTOMER, clsContactDetails);
						if (!this.CreateTransaction()) return;

                        txtBarCode.Text = strProductBarcode;
						ReadBarCode();
						int iRow = dgItems.CurrentRowIndex;

						txtBarCode.Text = "";
						CloseTransaction();

                        clsLocalDB.CommitAndDispose();
					}
					else { clsEvent.AddEventLn("Cancelled!"); }
				}
				catch (Exception ex)
				{ 
                    InsertErrorLogToFile(ex, "ERROR!!! Replacing internal credit card."); 
                }
				Cursor.Current = Cursors.Default;
			}
		}
		private void CreditCardReactivate()
		{
			if (mboIsInTransaction)
			{
				MessageBox.Show("Sorry you cannot reactivate a Credit Card while there is an ongoing transaction. Please finish the transaction first.", "RetailPlus", MessageBoxButtons.OK, MessageBoxIcon.Warning);
				return;
			}

			DialogResult loginresult = GetWriteAccessAndLogin(mclsSalesTransactionDetails.CashierID, AccessTypes.CreditCardChange, "Credit Card Reactivation");

            if (loginresult == DialogResult.OK)
			{
				try
				{
					DialogResult result; Data.ContactDetails clsContactDetails;
					ContactSelectWnd clsContactWnd = new ContactSelectWnd();
                    clsContactWnd.TerminalDetails = mclsTerminalDetails;
					clsContactWnd.ContactGroupCategory = AceSoft.RetailPlus.Data.ContactGroupCategory.CUSTOMER;
                    clsContactWnd.Header = "Please select customer for credit card re-activation.";
					clsContactWnd.ShowDialog(this);
					clsContactDetails = clsContactWnd.Details;
					result = clsContactWnd.Result;
					clsContactWnd.Close();
					clsContactWnd.Dispose();

					if (result != DialogResult.OK)
					{ return; }

					if (clsContactDetails.ContactID == Constants.ZERO || clsContactDetails.ContactID == Constants.C_RETAILPLUS_CUSTOMERID)
					{ return; }

					clsEvent.AddEvent("[" + lblCashier.Text + "] Reactivating lost credit card...");

                    Data.Contacts clsContact = new Data.Contacts(mConnection, mTransaction);
                    mConnection = clsContact.Connection; mTransaction = clsContact.Transaction;

					clsContactDetails = clsContact.Details(clsContactDetails.ContactID);
                    Data.ContactDetails clsGuarantor = clsContact.Details(clsContactDetails.CreditDetails.GuarantorID);

					clsContact.CommitAndDispose();

					if (clsContactDetails.CreditDetails.CreditCardNo == string.Empty || clsContactDetails.CreditDetails.CreditCardNo == null)
					{
						clsEvent.AddEventLn("Cancelled!");
						clsEvent.AddEventLn(clsContactDetails.ContactName + " has no valid Credit Card yet. ");
						MessageBox.Show(clsContactDetails.ContactName + " has no valid Credit Card yet. Please select another customer.", "RetailPlus", MessageBoxButtons.OK, MessageBoxIcon.Error);
						return;
					}
					clsEvent.AddEvent("[" + lblCashier.Text + "] Replacing credit card #: " + clsContactDetails.CreditDetails.CreditCardNo + " of " + clsContactDetails.ContactName + ".");

					string strOldCreditCardNo = clsContactDetails.CreditDetails.CreditCardNo;
					ContactCreditWnd clsContactCreditWnd = new ContactCreditWnd();
					clsContactCreditWnd.Header = "OVERRIDE: Credit Card Reactivation / Change Credit Limit / Change Credit Card #";
                    clsContactCreditWnd.CardTypeDetails = clsContactDetails.CreditDetails.CardTypeDetails;
					clsContactCreditWnd.Guarantor = clsGuarantor;
					clsContactCreditWnd.ContactDetails = clsContactDetails;
					clsContactCreditWnd.CreditCardStatus = CreditCardStatus.Reactivated_Lost;
					clsContactCreditWnd.ShowDialog(this);
					result = clsContactCreditWnd.Result;
					clsContactDetails = clsContactCreditWnd.ContactDetails;
					clsContactCreditWnd.Close();
					clsContactCreditWnd.Dispose();

					if (result == DialogResult.OK)
					{
                        MessageBox.Show("Credit Card No: " + clsContactDetails.CreditDetails.CreditCardNo + " has been reactivated / changed credit limit / changed card no...", "RetailPlus", MessageBoxButtons.OK, MessageBoxIcon.Information);

						clsEvent.AddEventLn("Done!");
						clsEvent.AddEventLn("Credit Card No: " + clsContactDetails.CreditDetails.CreditCardNo + " has been reactivated / changed credit limit / changed card no...", true);
						this.LoadOptions();
					}
					else { clsEvent.AddEventLn("Cancelled!"); }
				}
				catch (Exception ex)
				{ 
                    InsertErrorLogToFile(ex, "ERROR!!! Reactivating internal credit card."); 
                }
				Cursor.Current = Cursors.Default;
			}
		}
		private void CreditCardDeclareAsLost()
		{
			if (mboIsInTransaction)
			{
				MessageBox.Show("Sorry you cannot deaclare a Credit Card as lost while there is an ongoing transaction. Please finish the transaction first.", "RetailPlus", MessageBoxButtons.OK, MessageBoxIcon.Warning);
				return;
			}

			DialogResult loginresult = GetWriteAccessAndLogin(mclsSalesTransactionDetails.CashierID, AccessTypes.CreditCardChange, "Credit Card Declaration as Lost");

            if (loginresult == DialogResult.OK)
			{
				try
				{
					DialogResult result; Data.ContactDetails clsContactDetails;
					ContactSelectWnd clsContactWnd = new ContactSelectWnd();
                    clsContactWnd.TerminalDetails = mclsTerminalDetails;
					clsContactWnd.ContactGroupCategory = AceSoft.RetailPlus.Data.ContactGroupCategory.CUSTOMER;
                    clsContactWnd.Header = "Please select customer for credit card lost declaration.";
					clsContactWnd.ShowDialog(this);
					clsContactDetails = clsContactWnd.Details;
					result = clsContactWnd.Result;
					clsContactWnd.Close();
					clsContactWnd.Dispose();

					if (result != DialogResult.OK)
					{ return; }

					if (clsContactDetails.ContactID == Constants.ZERO || clsContactDetails.ContactID == Constants.C_RETAILPLUS_CUSTOMERID)
					{ return; }

					clsEvent.AddEvent("[" + lblCashier.Text + "] Declaring credit card as lost.");

                    Data.Contacts clsContact = new Data.Contacts(mConnection, mTransaction);
                    mConnection = clsContact.Connection; mTransaction = clsContact.Transaction;

					clsContactDetails = clsContact.Details(clsContactDetails.ContactID);
                    Data.ContactDetails clsGuarantor = clsContact.Details(clsContactDetails.CreditDetails.GuarantorID);

					clsContact.CommitAndDispose();

					if (clsContactDetails.CreditDetails.CreditCardNo == string.Empty || clsContactDetails.CreditDetails.CreditCardNo == null)
					{
						clsEvent.AddEventLn("Cancelled!");
						clsEvent.AddEventLn(clsContactDetails.ContactName + " has no valid Credit Card yet. ");
						MessageBox.Show(clsContactDetails.ContactName + " has no valid Credit Card yet. Please select another customer.", "RetailPlus", MessageBoxButtons.OK, MessageBoxIcon.Error);
						return;
					}

					ContactCreditWnd clsContactCreditWnd = new ContactCreditWnd();
					clsContactCreditWnd.Header = "Credit Card Declaration as Lost";
                    clsContactCreditWnd.CardTypeDetails = clsContactDetails.CreditDetails.CardTypeDetails;
					clsContactCreditWnd.Guarantor = clsGuarantor;
					clsContactCreditWnd.ContactDetails = clsContactDetails;
					clsContactCreditWnd.CreditCardStatus = CreditCardStatus.Lost;
					clsContactCreditWnd.ShowDialog(this);
					result = clsContactCreditWnd.Result;
					clsContactDetails = clsContactCreditWnd.ContactDetails;
					clsContactCreditWnd.Close();
					clsContactCreditWnd.Dispose();

					if (result == DialogResult.OK)
					{
                        MessageBox.Show("Credit Card No: " + clsContactDetails.CreditDetails.CreditCardNo + " has been declared as LOST.", "RetailPlus", MessageBoxButtons.OK, MessageBoxIcon.Information);

						clsEvent.AddEventLn("Done!");
						clsEvent.AddEventLn("Credit Card No: " + clsContactDetails.CreditDetails.CreditCardNo + " has been declared as LOST.", true);
						this.LoadOptions();
					}
					else { clsEvent.AddEventLn("Cancelled!"); }
				}
				catch (Exception ex)
				{ 
                    InsertErrorLogToFile(ex, "ERROR!!! Declaring internal credit card as lost."); 
                }
				Cursor.Current = Cursors.Default;
			}
		}

		#endregion

		#region Private Modifiers

		private void InitializeTransaction(Int64 UID)
		{
			try
			{
				clsEvent.AddEvent("Checking for pending transaction.");

				Data.SalesTransactions clsSalesTransactions = new Data.SalesTransactions(mConnection, mTransaction);
                mConnection = clsSalesTransactions.Connection; mTransaction = clsSalesTransactions.Transaction;

				string stTransactionNo = null;
                bool HasPendingTransaction = clsSalesTransactions.HasPendingTransaction(UID, mclsTerminalDetails.BranchID, mclsTerminalDetails.TerminalNo, out stTransactionNo);

				if (HasPendingTransaction)
				{ clsEvent.AddEventLn(stTransactionNo + " found pending."); LoadTransaction(stTransactionNo, mclsTerminalDetails.TerminalNo); }
				else
                { clsEvent.AddEventLn("None."); this.LoadOptions(); }
                clsSalesTransactions.CommitAndDispose();
			}
			catch (Exception ex)
			{
                InsertErrorLogToFile(ex, "ERROR!!! Initializing transaction using username : " + lblCashier.Text);
			}
		}
		public void DoLogin()
		{
			try
			{
				LogInWnd login = new LogInWnd();
				login.AccessType = AccessTypes.LoginFE;
				login.Header = "Enter user name and password to login.";
                login.TerminalDetails = mclsTerminalDetails;
				login.ShowDialog(this);

				DialogResult loginResult = login.Result;
				Int64 UserID = login.UserID;

				DialogResult loginresult = login.Result;
				login.Close();
				login.Dispose();

				if (loginresult == DialogResult.OK)
				{
					clsEvent.AddEventLn("UID: [" + UserID.ToString() + "] successfully logged-in.", true);
					clsEvent.AddEventLn("Checking if beggining balance already initialized.", true);

					//	check if already initialize beginning balance.
					if (!IsBeginningBalanceInitialized(UserID))
					{
						clsEvent.AddEventLn("Not yet initialized and cancelled.");
						return;
					}
					clsEvent.AddEventLn("Done. Balance is already initialized.", true);

					Cursor.Current = Cursors.WaitCursor;

					//	ZRead and Beginning balance is already initialize 
					//	Process Transaction.
					lblPress.Tag = DateTime.Now.ToString("MMM/dd/yyyy hh:mm:ss tt");

					CashierLogsDetails clsLogDetails = new CashierLogsDetails();
					clsLogDetails.CashierID = UserID;
					clsLogDetails.LoginDate = Convert.ToDateTime(lblPress.Tag.ToString());
					clsLogDetails.BranchID = mclsTerminalDetails.BranchID;
					clsLogDetails.TerminalNo = mclsTerminalDetails.TerminalNo;
					clsLogDetails.IPAddress = System.Net.Dns.GetHostName();
					clsLogDetails.LogoutDate = clsLogDetails.LoginDate;
					clsLogDetails.Status = CashierLogStatus.LoggedIn;

                    CashierLogs clsCashierLogs = new CashierLogs(mConnection, mTransaction);
                    mConnection = clsCashierLogs.Connection; mTransaction = clsCashierLogs.Transaction;

					lblCashierName.Tag = clsCashierLogs.Insert(clsLogDetails).ToString();

					this.UnLock(UserID);
					this.InitializeTransaction(UserID);
                    clsCashierLogs.CommitAndDispose();

                    InsertAuditLog(AccessTypes.LoginFE, "System login at terminal no. " + mclsTerminalDetails.TerminalNo + " @ Branch: " + mclsTerminalDetails.BranchDetails.BranchCode);

					clsEvent.AddEventLn("System is now ready for transaction. Current user: " + lblCashier.Text, true);
					Cursor.Current = Cursors.Default;
				}
			}
			catch (Exception ex)
			{
                InsertErrorLogToFile(ex, "ERROR!!! System login: TRACE: ");
				Cursor.Current = Cursors.Default;
				throw ex;
			}
		}
		private Data.SalesTransactionItemDetails getCurrentRowItemDetails()
		{
			try
			{
				Int32 iRow = dgItems.CurrentRowIndex;

				Data.SalesTransactionItemDetails Details = new Data.SalesTransactionItemDetails();

				Details.TransactionID = Convert.ToInt64(lblTransNo.Tag);
				Details.TransactionDate = mclsSalesTransactionDetails.TransactionDate;
				Details.TransactionItemsID = Convert.ToInt64(dgItems[iRow, 0].ToString());
				Details.ItemNo = dgItems[iRow, 1].ToString();
				Details.ProductID = Convert.ToInt32(dgItems[iRow, 2].ToString());
				Details.ProductCode = dgItems[iRow, 3].ToString();
				Details.BarCode = dgItems[iRow, 4].ToString();
				Details.Description = dgItems[iRow, 5].ToString();
				if (Details.Description.IndexOf(Environment.NewLine) > -1)
				{
					Details.Description = Details.Description.Remove(Details.Description.IndexOf(Environment.NewLine), Details.Description.Length - Details.Description.IndexOf(Environment.NewLine));
				}
				Details.ProductUnitID = Convert.ToInt32(dgItems[iRow, 6].ToString());
				Details.ProductUnitCode = dgItems[iRow, 7].ToString();
				if (dgItems[iRow, 8].ToString().IndexOf("RETURN") != -1)
				{
					Details.Quantity = Convert.ToDecimal(dgItems[iRow, 8].ToString().Replace(" - RETURN", "").Trim());
				}
				else if (dgItems[iRow, 8].ToString().IndexOf("VOID") != -1)
				{
					Details.Quantity = 0;
				}
				else
				{
					Details.Quantity = Convert.ToDecimal(dgItems[iRow, 8].ToString());
				}
				Details.Price = Convert.ToDecimal(dgItems[iRow, 9].ToString());
				Details.Discount = Convert.ToDecimal(dgItems[iRow, 10].ToString());
				Details.ItemDiscount = Convert.ToDecimal(dgItems[iRow, 11].ToString());
				Details.ItemDiscountType = (DiscountTypes)Enum.Parse(typeof(DiscountTypes), dgItems[iRow, 12].ToString());
				Details.Amount = Details.Quantity * Details.Price;//Convert.ToDecimal(dgItems[iRow, 13].ToString()) ;
				Details.VAT = Convert.ToDecimal(dgItems[iRow, 14].ToString());
				Details.EVAT = Convert.ToDecimal(dgItems[iRow, 15].ToString());
				Details.LocalTax = Convert.ToDecimal(dgItems[iRow, 16].ToString());
				Details.VariationsMatrixID = Convert.ToInt64(dgItems[iRow, 17].ToString());
				Details.MatrixDescription = dgItems[iRow, 18].ToString();
				Details.ProductGroup = dgItems[iRow, 19].ToString();
				Details.ProductSubGroup = dgItems[iRow, 20].ToString();
				Details.TransactionItemStatus = (TransactionItemStatus)Enum.Parse(typeof(TransactionItemStatus), dgItems[iRow, 21].ToString());
				Details.DiscountCode = dgItems[iRow, 22].ToString();
				Details.DiscountRemarks = dgItems[iRow, 23].ToString();
				Details.ProductPackageID = Convert.ToInt64(dgItems[iRow, 24].ToString());
				Details.MatrixPackageID = Convert.ToInt64(dgItems[iRow, 25].ToString());
				Details.PackageQuantity = Convert.ToDecimal(dgItems[iRow, 26].ToString());
				Details.PromoQuantity = Convert.ToDecimal(dgItems[iRow, 27].ToString());
				Details.PromoValue = Convert.ToDecimal(dgItems[iRow, 28].ToString());
                Details.PromoInPercent = Convert.ToBoolean(dgItems[iRow, 29].ToString());
				Details.PromoType = (PromoTypes)Enum.Parse(typeof(PromoTypes), dgItems[iRow, 30].ToString());
				Details.PromoApplied = Convert.ToDecimal(dgItems[iRow, 31].ToString());
				Details.PurchasePrice = Convert.ToDecimal(dgItems[iRow, 32].ToString());
				Details.PurchaseAmount = Convert.ToDecimal(dgItems[iRow, 33].ToString());
				Details.IncludeInSubtotalDiscount = Convert.ToBoolean(dgItems[iRow, 34].ToString());
				Details.OrderSlipPrinter = (OrderSlipPrinter) Enum.Parse(typeof(OrderSlipPrinter), dgItems[iRow, 35].ToString());
				Details.OrderSlipPrinted = Convert.ToBoolean(dgItems[iRow, 36].ToString());
				Details.PercentageCommision = Convert.ToDecimal(dgItems[iRow, 37].ToString());
				Details.Commision = Convert.ToDecimal(dgItems[iRow, 38].ToString());

				return Details;
			}
			catch (Exception ex)
			{
                InsertErrorLogToFile(ex, "ERROR!!! Getting current row item details. TRACE: ");
				throw ex;
			}
		}
		private System.Data.DataRow setCurrentRowItemDetails(System.Data.DataRow dr, Data.SalesTransactionItemDetails Details)
		{
			try
			{
                //21Jul2013 Add Parking rate if parking
                if (mclsTerminalDetails.IsParkingTerminal)
                {
                    Data.ParkingRates clsParkingRate = new Data.ParkingRates(mConnection, mTransaction);
                    mConnection = clsParkingRate.Connection; mTransaction = clsParkingRate.Transaction;

                    Data.ParkingRateDetails clsParkingRateDetails = clsParkingRate.Details(Details.ProductID, mclsSalesTransactionDetails.TransactionDate.ToString("dddd"));
                    if (clsParkingRateDetails.ParkingRateID != 0)
                    {
                        //compute the parking rate
                        Int32 intTotalNoOfMinutes = mclsSalesTransactionDetails.DateResumed == DateTime.MinValue ? 0 : (Int32)(mclsSalesTransactionDetails.DateResumed - mclsSalesTransactionDetails.TransactionDate).TotalMinutes;
                        decimal decParkingPrice = Details.Price;

                        if (intTotalNoOfMinutes <= clsParkingRateDetails.MinimumStayInMin)
                        {
                            Details.Price = clsParkingRateDetails.MinimumStayPrice;
                            Details.Description += Environment.NewLine + "TotalNoOfMinutes".PadRight(15) + ":" + intTotalNoOfMinutes.ToString("#,##0") + " @ " + clsParkingRateDetails.MinimumStayPrice.ToString("#,##0.#0") + " / " + clsParkingRateDetails.MinimumStayInMin.ToString("#,##0"); 
                        }
                        else
                        {
                            decParkingPrice = clsParkingRateDetails.MinimumStayPrice + (intTotalNoOfMinutes - clsParkingRateDetails.MinimumStayInMin) / clsParkingRateDetails.NoOfUnitperMin * clsParkingRateDetails.PerUnitPrice;
                            Details.Description += Environment.NewLine + "TotalNoOfMinutes".PadRight(15) + ":" + intTotalNoOfMinutes.ToString("#,##0") + " First " + clsParkingRateDetails.MinimumStayInMin.ToString("#,##0") + " @ " + clsParkingRateDetails.MinimumStayPrice.ToString("#,##0.#0") + " SuccNoOfUnit: " + (intTotalNoOfMinutes - clsParkingRateDetails.MinimumStayInMin).ToString("#,##0") + "/" + clsParkingRateDetails.NoOfUnitperMin.ToString("#,##0") + " * " + clsParkingRateDetails.PerUnitPrice.ToString("#,##0.#0");
                        }
                        Details.Price = decParkingPrice;
                        Details.Amount = decParkingPrice;
                    }
                }

				//if (Details.ItemDiscountType == DiscountTypes.NotApplicable)
				//{	Details.Description		= Details.Description;	}
                if (Details.DiscountCode == Constants.C_DISCOUNT_CODE_FREE && Details.ItemDiscountType == DiscountTypes.FixedValue)
                { Details.Description = Details.Description + Environment.NewLine + "@ " + Details.ItemDiscount.ToString("###,##0.#0") + " FREE"; }
                else if (Details.DiscountCode == Constants.C_DISCOUNT_CODE_FREE && Details.ItemDiscountType == DiscountTypes.Percentage)
                { Details.Description = Details.Description + Environment.NewLine + "@ " + Details.ItemDiscount.ToString("###,##0.#0") + "% FREE"; }
				else if (Details.ItemDiscountType == DiscountTypes.FixedValue)
				{ Details.Description = Details.Description + Environment.NewLine + "@ " + Details.ItemDiscount.ToString("###,##0.#0") + " disc"; }
				else if (Details.ItemDiscountType == DiscountTypes.Percentage)
				{ Details.Description = Details.Description + Environment.NewLine + "@ " + Details.ItemDiscount.ToString("###,##0.#0") + " % disc"; }

				if (Details.PromoApplied != 0)
				{ Details.Description = Details.Description + Environment.NewLine + "@ " + Details.PromoApplied.ToString("###,##0.#0") + " promo"; }

				if (ItemDataTable.Rows.Count + 1 > 8)
					dgStyle.GridColumnStyles["Amount"].Width = 80;
				else
					dgStyle.GridColumnStyles["Amount"].Width = 90;

				dr["TransactionItemsID"] = Details.TransactionItemsID;
				dr["ItemNo"] = Details.ItemNo;
				dr["ProductID"] = Details.ProductID;
				dr["ProductCode"] = Details.ProductCode;
				dr["BarCode"] = Details.BarCode;
				dr["Description"] = Details.Description;
				dr["ProductUnitID"] = Details.ProductUnitID;
				dr["ProductUnitCode"] = Details.ProductUnitCode;
				dr["Quantity"] = Details.Quantity.ToString("###,##0.###");	//8
				dr["Price"] = Details.Price.ToString("###,##0.#0");	//9
				dr["Discount"] = Details.Discount.ToString("###,##0.#0"); //10
				dr["ItemDiscount"] = Details.ItemDiscount.ToString("###,##0.#0");//11
				dr["ItemDiscountType"] = Details.ItemDiscountType.ToString("d");//12
				dr["Amount"] = Details.Amount.ToString("###,##0.#0"); //13
				dr["VAT"] = Details.VAT.ToString("###,##0.#0");
				dr["EVAT"] = Details.EVAT.ToString("###,##0.#0");
				dr["LocalTax"] = Details.LocalTax.ToString("###,##0.#0");
				dr["VariationsMatrixID"] = Details.VariationsMatrixID;
				dr["MatrixDescription"] = Details.MatrixDescription;
				dr["ProductGroup"] = Details.ProductGroup;
				dr["ProductSubGroup"] = Details.ProductSubGroup;
				dr["TransactionItemStat"] = Details.TransactionItemStatus.ToString("d");
				dr["DiscountCode"] = Details.DiscountCode;
				dr["DiscountRemarks"] = Details.DiscountRemarks;
				dr["ProductPackageID"] = Details.ProductPackageID;
				dr["MatrixPackageID"] = Details.MatrixPackageID;
				dr["PackageQuantity"] = Details.PackageQuantity;
				dr["PromoQuantity"] = Details.PromoQuantity;
				dr["PromoValue"] = Details.PromoValue;
				dr["PromoInPercent"] = Details.PromoInPercent;
				dr["PromoType"] = Details.PromoType;
				dr["PromoApplied"] = Details.PromoApplied;
				dr["PurchasePrice"] = Details.PurchasePrice;
				dr["PurchaseAmount"] = Details.PurchaseAmount;
				dr["IncludeInSubtotalDiscount"] = Details.IncludeInSubtotalDiscount;
				dr["OrderSlipPrinter"] = Details.OrderSlipPrinter;
				dr["OrderSlipPrinted"] = Details.OrderSlipPrinted.ToString();
				dr["PercentageCommision"] = Details.PercentageCommision;
				dr["Commision"] = Details.Amount * (Details.PercentageCommision / 100);

				if (Details.TransactionItemStatus == TransactionItemStatus.Void)
				{
					dr["Quantity"] = "VOID";
					dr["Price"] = "0.00";
					dr["Discount"] = "0.00";
					dr["Amount"] = "0.00";
					dr["VAT"] = "0.00";
					dr["EVAT"] = "0.00";
					dr["LocalTax"] = "0.00";
					dr["PromoApplied"] = "0.00";
					dr["PercentageCommision"] = "0.00";
					dr["Commision"] = "0.00";
				}
				else if (Details.TransactionItemStatus == TransactionItemStatus.Return)
				{
					dr["Quantity"] = Details.Quantity + " - RETURN";
					if (Details.Amount < 0)
					{
						dr["Amount"] = Convert.ToDecimal(-Details.Amount).ToString("###,##0.#0");
						dr["PercentageCommision"] = -Details.PercentageCommision;
						dr["Commision"] = Convert.ToDecimal(Convert.ToDecimal(dr["Commision"]) * -1).ToString("###,##0.#0");
					}
				}

				if (ItemDataTable.Rows.Count + 1 > 8)
					dgStyle.GridColumnStyles["Amount"].Width = 80;
				else
					dgStyle.GridColumnStyles["Amount"].Width = 90;

				return dr;
			}
			catch (Exception ex)
			{
                InsertErrorLogToFile(ex, "ERROR!!! Setting current row item details. TRACE: ");
				return dr;
			}
		}
		private bool IsBeginningBalanceInitialized(long CashierID)
		{
			try
			{
				bool boRetValue = false;

				Data.CashierReports clsCashierReport = new Data.CashierReports(mConnection, mTransaction);
                mConnection = clsCashierReport.Connection; mTransaction = clsCashierReport.Transaction;

				bool IsBeginningBalanceInitialized = clsCashierReport.IsBeginningBalanceInitialized(mclsTerminalDetails.BranchDetails.BranchID, mclsTerminalDetails.TerminalNo, CashierID);
				clsCashierReport.CommitAndDispose();

				if (!IsBeginningBalanceInitialized)
				{
					BalanceWnd clsBalanceWnd = new BalanceWnd();
                    clsBalanceWnd.TerminalDetails = mclsTerminalDetails;
					clsBalanceWnd.CashierID = CashierID;
					clsBalanceWnd.ShowDialog(this);

					DialogResult balanceResult = clsBalanceWnd.Result;

					clsBalanceWnd.Close();
					clsBalanceWnd.Dispose();

					if (balanceResult == DialogResult.OK)
					{
						boRetValue = true;
						OpenDrawerDelegate opendrawerDel = new OpenDrawerDelegate(OpenDrawer);
						Invoke(opendrawerDel);
					}
				}
				else
				{
					boRetValue = true;
				}
				return boRetValue;
			}
			catch (Exception ex)
		    {
                InsertErrorLogToFile(ex, "ERROR!!! Initializing balance.");
				throw ex;
			}
		}
		private void AddItem(Data.SalesTransactionItemDetails Details)
		{
			try
			{
				Details.ItemNo = Convert.ToString(ItemDataTable.Rows.Count + 1);
				Details.TransactionDate = mclsSalesTransactionDetails.TransactionDate;

				Details = ApplyPromo(Details);

				System.Data.DataRow dr = ItemDataTable.NewRow();
				dr = setCurrentRowItemDetails(dr, Details);

				Details.TransactionItemsID = AddItemToDB(Details);
				dr["TransactionItemsID"] = Details.TransactionItemsID;

				// Added May 7, 2011 to Cater Reserved and Commit functionality    
				ReservedAndCommitItem(Details, Details.TransactionItemStatus);

				try { dgItems.UnSelect(dgItems.CurrentRowIndex); }
				catch { }

				ItemDataTable.Rows.Add(dr);

				dgItems.CurrentRowIndex = ItemDataTable.Rows.Count;
				try
				{
					dgItems.Select(dgItems.CurrentRowIndex);
					dgItems.CurrentRowIndex = dgItems.CurrentRowIndex;
				}
				catch { }

				SetItemDetails();
				ComputeSubTotal(); setTotalDetails();

				Data.SalesTransactions clsSalesTransactions = new Data.SalesTransactions(mConnection, mTransaction);
                mConnection = clsSalesTransactions.Connection; mTransaction = clsSalesTransactions.Transaction;

                clsSalesTransactions.UpdateSubTotal(mclsSalesTransactionDetails.TransactionID, mclsSalesTransactionDetails.ItemSold, mclsSalesTransactionDetails.QuantitySold, mclsSalesTransactionDetails.SubTotal, mclsSalesTransactionDetails.NetSales, mclsSalesTransactionDetails.ItemsDiscount, mclsSalesTransactionDetails.Discount, mclsSalesTransactionDetails.SNRDiscount, mclsSalesTransactionDetails.PWDDiscount, mclsSalesTransactionDetails.OtherDiscount, mclsSalesTransactionDetails.TransDiscount, mclsSalesTransactionDetails.TransDiscountType, mclsSalesTransactionDetails.VAT, mclsSalesTransactionDetails.VATableAmount, mclsSalesTransactionDetails.NonVATableAmount, mclsSalesTransactionDetails.VATExempt, mclsSalesTransactionDetails.EVAT, mclsSalesTransactionDetails.EVATableAmount, mclsSalesTransactionDetails.NonEVATableAmount, mclsSalesTransactionDetails.LocalTax, mclsSalesTransactionDetails.DiscountCode, mclsSalesTransactionDetails.DiscountRemarks, mclsSalesTransactionDetails.Charge, mclsSalesTransactionDetails.ChargeAmount, mclsSalesTransactionDetails.ChargeCode, mclsSalesTransactionDetails.ChargeRemarks, mclsSalesTransactionDetails.ChargeType);
                clsSalesTransactions.CommitAndDispose();

				try
				{
					DisplayItemToTurretDelegate DisplayItemToTurretDel = new DisplayItemToTurretDelegate(DisplayItemToTurret);
					DisplayItemToTurretDel.BeginInvoke(Details.ProductCode, Details.ProductUnitCode, Details.Quantity, Details.Price, Details.Discount, Details.PromoApplied, Details.Amount, Details.VAT, Details.EVAT, null, null);
				}
				catch { }
				if (mclsTerminalDetails.AutoPrint == PrintingPreference.Auto)
				{
					PrintItemDelegate PrintItemDel = new PrintItemDelegate(PrintItem);
					PrintItemDel.BeginInvoke(Details.ItemNo, Details.ProductCode, Details.ProductUnitCode, Details.Quantity, Details.Price, Details.Discount, Details.PromoApplied, Details.Amount, Details.VAT, Details.EVAT, null, null);
				}
			}
			
			catch (Exception ex)
			{
				InsertErrorLogToFile(ex, "ERROR!!! Adding sales item. TRACE: ");
				throw ex;
			}
		}
        private void UpdateIsConsignment()
        {
            if (!mboIsInTransaction)
            {
                MessageBox.Show("Sorry you cannot consign an empty transaction.", "RetailPlus", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (mclsSalesTransactionDetails.AgentID == Constants.C_RETAILPLUS_AGENTID)
            {
                if (MessageBox.Show("Sorry you need to select an agent before you can consign this transaction! Select now?", "RetailPlus", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
                { if (!SelectContact(Data.ContactGroupCategory.AGENT)) return; }
                else
                    return;
            }

            if (mclsSalesTransactionDetails.CustomerID == Constants.C_RETAILPLUS_CUSTOMERID)
            {
                if (MessageBox.Show("Sorry you need to select a customer before you can consign this transaction! Select now?", "RetailPlus", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
                { if (!SelectContact(Data.ContactGroupCategory.CUSTOMER)) return; }
                else
                    return;
            }

            DialogResult loginresult = GetWriteAccessAndLogin(mclsSalesTransactionDetails.CashierID, AccessTypes.CloseTransaction, "Consign Transaction Access Validation");

            if (loginresult == DialogResult.OK)
            {
                try
                {
                    Data.SalesTransactions clsSalesTransactions = new Data.SalesTransactions(mConnection, mTransaction);
                    mConnection = clsSalesTransactions.Connection; mTransaction = clsSalesTransactions.Transaction;

                    clsSalesTransactions.UpdateisConsignment(mclsSalesTransactionDetails.TransactionID, !mclsSalesTransactionDetails.isConsignment);
                    clsSalesTransactions.CommitAndDispose();

                    mclsSalesTransactionDetails.isConsignment = !mclsSalesTransactionDetails.isConsignment;

                    lblConsignment.Visible = mclsSalesTransactionDetails.isConsignment;

                    InsertAuditLog(AccessTypes.CloseTransaction, "Updating isConsignment: " + mclsSalesTransactionDetails.isConsignment.ToString() + " in the database for transactionno: " + mclsSalesTransactionDetails.TransactionNo + " done.");
                    MessageBox.Show(mclsSalesTransactionDetails.isConsignment ? "This transaction has been saved as consignment." : "This transaction has been saved as NOT consignment", "RetailPlus", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    InsertErrorLogToFile(ex, "ERROR!!! Updating isConsignment: " + mclsSalesTransactionDetails.isConsignment.ToString() + " in the database for transactionno: " + mclsSalesTransactionDetails.TransactionNo + ". TRACE: ");
                    throw ex;
                }
            }
        }
		private void PackTransaction()
		{
			if (!mboIsInTransaction)
			{
				MessageBox.Show("Sorry you cannot pack an empty transaction.", "RetailPlus", MessageBoxButtons.OK, MessageBoxIcon.Warning);
				return;
			}

			if (mclsSalesTransactionDetails.WaiterID == Constants.C_RETAILPLUS_WAITERID)
			{
				if (MessageBox.Show("Sorry you need to select waiter/packer before you can serve this transaction! \nSelect now?", "RetailPlus", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) == DialogResult.OK)
				{ if (!SelectWaiter()) return; }
				else
					return;
			}

			DialogResult loginresult = GetWriteAccessAndLogin(mclsSalesTransactionDetails.CashierID, AccessTypes.PackUnpackTransaction);

            if (loginresult == DialogResult.OK)
			{
				try
				{
                    Data.SalesTransactions clsSalesTransactions = new Data.SalesTransactions(mConnection, mTransaction);
                    mConnection = clsSalesTransactions.Connection; mTransaction = clsSalesTransactions.Transaction;

					clsSalesTransactions.Pack(mclsSalesTransactionDetails.TransactionID);
					clsSalesTransactions.CommitAndDispose();

					MessageBox.Show("Packing Done!", "RetailPlus", MessageBoxButtons.OK, MessageBoxIcon.Information);
				}
				catch (Exception ex)
				{
					InsertErrorLogToFile(ex, "ERROR!!! Packing sales transaction to database. TRACE: ");
					throw ex;
				}
			}
		}
		private void UnPackTransaction()
		{
			if (!mboIsInTransaction)
			{
				MessageBox.Show("Sorry you cannot unpack an empty transaction.", "RetailPlus", MessageBoxButtons.OK, MessageBoxIcon.Warning);
				return;
			}

			if (mclsSalesTransactionDetails.WaiterID == Constants.C_RETAILPLUS_WAITERID)
			{
				if (MessageBox.Show("Sorry you need to select waiter/packer before you can un-pack! Select now?", "RetailPlus", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) == DialogResult.OK)
				{ if (!SelectWaiter()) return; }
				else
					return;
			}

			DialogResult loginresult = GetWriteAccessAndLogin(mclsSalesTransactionDetails.CashierID, AccessTypes.PackUnpackTransaction);

            if (loginresult == DialogResult.OK)
			{
				try
				{
                    Data.SalesTransactions clsSalesTransactions = new Data.SalesTransactions(mConnection, mTransaction);
                    mConnection = clsSalesTransactions.Connection; mTransaction = clsSalesTransactions.Transaction;

					clsSalesTransactions.UnPack(mclsSalesTransactionDetails.TransactionID);
					clsSalesTransactions.CommitAndDispose();

					MessageBox.Show("UnPacking Done!", "RetailPlus", MessageBoxButtons.OK, MessageBoxIcon.Information);
				}
				catch (Exception ex)
				{
					InsertErrorLogToFile(ex, "ERROR!!! UnPacking sales transaction to database. TRACE: ");
					throw ex;
				}
			}
		}
		private void ReprintTransaction()
		{
            if (!SuspendTransactionAndContinue()) return;

			DialogResult loginresult = GetWriteAccessAndLogin(mclsSalesTransactionDetails.CashierID, AccessTypes.ReprintTransaction);

            if (loginresult == DialogResult.OK)
			{
				TransactionNoWnd clsTransactionNoWnd = new TransactionNoWnd();
				clsTransactionNoWnd.TransactionNoLength = mclsTerminalDetails.TransactionNoLength;
				clsTransactionNoWnd.TerminalNo = mclsTerminalDetails.TerminalNo;
				clsTransactionNoWnd.ShowDialog(this);
				DialogResult result = clsTransactionNoWnd.Result;
				string strTransactionNo = "";
				string strTerminalNo = mclsTerminalDetails.TerminalNo;
				if (result == DialogResult.OK)
				{
					strTransactionNo = clsTransactionNoWnd.TransactionNo;
					strTerminalNo = clsTransactionNoWnd.TerminalNo;
				}

				clsTransactionNoWnd.Close();
				clsTransactionNoWnd.Dispose();

				if (result == DialogResult.OK)
				{
                    LoadOptions();

                    clsEvent.AddEventLn("Reprinting transaction #: " + strTransactionNo, true);

					mclsTerminalDetails.AutoPrint = PrintingPreference.Normal;

					//open salestransaction data
                    Data.SalesTransactions clsSalesTransactions = new Data.SalesTransactions(mConnection, mTransaction);
                    mConnection = clsSalesTransactions.Connection; mTransaction = clsSalesTransactions.Transaction;

					LoadTransaction(strTransactionNo, strTerminalNo);

                    AddToReprintedTransaction(strTransactionNo, strTerminalNo);

                    clsSalesTransactions.CommitAndDispose();

					ArrayList arrChequePaymentDetails = null;
					ArrayList arrCreditCardPaymentDetails = null;
					ArrayList arrCreditPaymentDetails = null;
					ArrayList arrDebitPaymentDetails = null;

                    //print transactionfooter
                    //items are already printed during the loading of items.
                    if (mclsTerminalDetails.ReceiptType == TerminalReceiptType.Default)
                        PrintReportFooterSection(true, TransactionStatus.Reprinted, mclsSalesTransactionDetails.ItemSold, mclsSalesTransactionDetails.QuantitySold, mclsSalesTransactionDetails.SubTotal, mclsSalesTransactionDetails.Discount, mclsSalesTransactionDetails.Charge, mclsSalesTransactionDetails.AmountPaid, mclsSalesTransactionDetails.CashPayment, mclsSalesTransactionDetails.ChequePayment, mclsSalesTransactionDetails.CreditCardPayment, mclsSalesTransactionDetails.CreditPayment, mclsSalesTransactionDetails.DebitPayment, mclsSalesTransactionDetails.RewardPointsPayment, mclsSalesTransactionDetails.RewardConvertedPayment, mclsSalesTransactionDetails.ChangeAmount, arrChequePaymentDetails, arrCreditCardPaymentDetails, arrCreditPaymentDetails, arrDebitPaymentDetails);

                    if (mclsTerminalDetails.ReceiptType == TerminalReceiptType.SalesInvoice)
                    {
                        PrintSalesInvoice();
                    }
                    else if (mclsTerminalDetails.ReceiptType == TerminalReceiptType.DeliveryReceipt)
                    {
                        PrintDeliveryReceipt();
                    }
                    else if (mclsTerminalDetails.ReceiptType == TerminalReceiptType.SalesInvoiceAndDR)
                    {
                        PrintSalesInvoice();
                        PrintDeliveryReceipt();
                    }
                    //Added February 10, 2010
                    else if (mclsTerminalDetails.ReceiptType == TerminalReceiptType.SalesInvoiceForLX300Printer)
                    {
                        PrintSalesInvoiceToLX(TerminalReceiptType.SalesInvoiceForLX300Printer);
                    }
                    //Added May 11, 2010
                    else if (mclsTerminalDetails.ReceiptType == TerminalReceiptType.SalesInvoiceOrDR)
                    {
                        if (mclsSalesTransactionDetails.CashPayment != 0 || mclsSalesTransactionDetails.CreditCardPayment != 0)
                            PrintSalesInvoice();
                        if (mclsSalesTransactionDetails.ChequePayment != 0 || mclsSalesTransactionDetails.CreditPayment != 0)
                            PrintDeliveryReceipt();
                    }
                    //Added January 17, 2011
                    else if (mclsTerminalDetails.ReceiptType == TerminalReceiptType.SalesInvoiceForLX300PlusPrinter)
                    {
                        PrintSalesInvoiceToLX(TerminalReceiptType.SalesInvoiceForLX300PlusPrinter);
                    }
                    //Added February 22, 2011
                    else if (mclsTerminalDetails.ReceiptType == TerminalReceiptType.SalesInvoiceForLX300PlusAmazon)
                    {
                        PrintSalesInvoiceToLX(TerminalReceiptType.SalesInvoiceForLX300PlusAmazon);
                    }
                    clsEvent.AddEventLn("Done reprinting transaction #".PadRight(15) + ":" + strTransactionNo, true);


					this.LoadOptions();
				}
			}
		}
		private void ReprintDeliveryReceipt()
		{
            if (!SuspendTransactionAndContinue()) return;

			DialogResult loginresult = GetWriteAccessAndLogin(mclsSalesTransactionDetails.CashierID, AccessTypes.ReprintTransaction);

            if (loginresult == DialogResult.OK)
			{
				TransactionNoWnd clsTransactionNoWnd = new TransactionNoWnd();
				clsTransactionNoWnd.TransactionNoLength = mclsTerminalDetails.TransactionNoLength;
				clsTransactionNoWnd.TerminalNo = mclsTerminalDetails.TerminalNo;
				clsTransactionNoWnd.ShowDialog(this);
				DialogResult result = clsTransactionNoWnd.Result;
				string strTransactionNo = "";
				string strTerminalNo = mclsTerminalDetails.TerminalNo;
				if (result == DialogResult.OK)
				{
					strTransactionNo = clsTransactionNoWnd.TransactionNo;
					strTerminalNo = clsTransactionNoWnd.TerminalNo;
				}

				clsTransactionNoWnd.Close();
				clsTransactionNoWnd.Dispose();

				if (result == DialogResult.OK)
				{
					clsEvent.AddEventLn("Reprinting transaction #: " + strTransactionNo, true);

					mclsTerminalDetails.AutoPrint = PrintingPreference.Normal;

					//open salestransaction data
					LoadTransaction(strTransactionNo, strTerminalNo);

					//insert to logfile
					ArrayList arrChequePaymentDetails = null;
					ArrayList arrCreditCardPaymentDetails = null;
					ArrayList arrCreditPaymentDetails = null;
					ArrayList arrDebitPaymentDetails = null;

					//print transactionfooter
					if (mclsTerminalDetails.ReceiptType == TerminalReceiptType.Default)
						PrintReportFooterSection(true, TransactionStatus.Reprinted, mclsSalesTransactionDetails.ItemSold, mclsSalesTransactionDetails.QuantitySold, mclsSalesTransactionDetails.SubTotal, mclsSalesTransactionDetails.Discount, mclsSalesTransactionDetails.Charge, mclsSalesTransactionDetails.AmountPaid, mclsSalesTransactionDetails.CashPayment, mclsSalesTransactionDetails.ChequePayment, mclsSalesTransactionDetails.CreditCardPayment, mclsSalesTransactionDetails.CreditPayment, mclsSalesTransactionDetails.DebitPayment, mclsSalesTransactionDetails.RewardPointsPayment, mclsSalesTransactionDetails.RewardConvertedPayment, mclsSalesTransactionDetails.ChangeAmount, arrChequePaymentDetails, arrCreditCardPaymentDetails, arrCreditPaymentDetails, arrDebitPaymentDetails);

					PrintDeliveryReceipt();
					
					clsEvent.AddEventLn("Done reprinting delivery receipt transaction #".PadRight(15) + ":" + strTransactionNo, true);

					this.LoadOptions();
				}
			}
		}
        
		private void PrintTerminalZRead()
		{
            if (!SuspendTransactionAndContinue()) return;

			DialogResult loginresult = GetWriteAccessAndLogin(mclsSalesTransactionDetails.CashierID, AccessTypes.PrintZRead);

			if (loginresult == DialogResult.OK)
			{
				PrintZRead(true);
			}
		}
		private void PrintTerminalXRead()
		{
            if (!SuspendTransactionAndContinue()) return;

			DialogResult loginresult = GetWriteAccessAndLogin(mclsSalesTransactionDetails.CashierID, AccessTypes.PrintXRead);

			if (loginresult == DialogResult.OK)
			{
				PrintXRead();
			}
		}
		private void PrintHourly()
		{
            if (!SuspendTransactionAndContinue()) return;

            DialogResult loginresult = GetWriteAccessAndLogin(mclsSalesTransactionDetails.CashierID, AccessTypes.PrintHourlyReport);

			if (loginresult == DialogResult.OK)
			{
				PrintHourlyReport();
			}
		}
		private void PrintGroup()
		{
            if (!SuspendTransactionAndContinue()) return;

			DialogResult loginresult = GetWriteAccessAndLogin(mclsSalesTransactionDetails.CashierID, AccessTypes.PrintGroupReport);

			if (loginresult == DialogResult.OK)
			{
				PrintGroupReport();
			}
		}
		private void PrintPLU()
		{
            if (!SuspendTransactionAndContinue()) return;

			DialogResult loginresult = GetWriteAccessAndLogin(mclsSalesTransactionDetails.CashierID, AccessTypes.PrintPLUReport);

			if (loginresult == DialogResult.OK)
			{
				PrintPLUReport();
			}
		}
        private void PrintPLUGroup()
        {
            if (!SuspendTransactionAndContinue()) return;

            DialogResult loginresult = GetWriteAccessAndLogin(mclsSalesTransactionDetails.CashierID, AccessTypes.PrintPLUReport, "Print PLU Group Report Access Validation");

            if (loginresult == DialogResult.OK)
            {
                PrintPLUReportPerGroup();
            }
        }
		private void PrintEJournal()
		{
            if (!SuspendTransactionAndContinue()) return;

			DialogResult loginresult = GetWriteAccessAndLogin(mclsSalesTransactionDetails.CashierID, AccessTypes.PrintElectronicJournal);

			if (loginresult == DialogResult.OK)
			{
				PrintEJournalReport();
			}
		}
		private void PrintPLUPerOrderSlipPrinter()
		{
            if (!SuspendTransactionAndContinue()) return;

			DialogResult loginresult = GetWriteAccessAndLogin(mclsSalesTransactionDetails.CashierID, AccessTypes.PrintPLUReport);

			if (loginresult == DialogResult.OK)
			{
				PrintPLUReportPerOrderSlipPrinter();
			}
		}
		private bool IsDateLastInitializationOK()
		{
			try
			{
				clsEvent.AddEvent("Checking last initialization date");

				Data.Database clsDatabase = new Data.Database(mConnection, mTransaction);
                mConnection = clsDatabase.Connection; mTransaction = clsDatabase.Transaction;

				DateTime dtDateLastInitialized = clsDatabase.DateLastInitialized();

				bool boRetValue = false;

				if (DateTime.Now > dtDateLastInitialized)
				{
					clsEvent.AddEventLn("OK: Last initialization is smaller than system date. DateLastInitialized=" + dtDateLastInitialized.ToString("yyyy-MM-dd hh:mm") + " SystemDate=" + DateTime.Now.ToString("yyyy-MM-dd hh:mm"));
				}
				else
				{
					clsEvent.AddEventLn("Error: Last initialization is greater than system date. DateLastInitialized=" + dtDateLastInitialized.ToString("yyyy-MM-dd hh:mm") + " SystemDate=" + DateTime.Now.ToString("yyyy-MM-dd hh:mm"));
					clsDatabase.CommitAndDispose();
					MessageBox.Show("FATAL ERROR Level 2.!!! System date is behind ZREAD last initialization date. Please adjust SYSTEM DATE!!!", "RetailPlus", MessageBoxButtons.OK, MessageBoxIcon.Error);
					return boRetValue;
				}

				Data.TerminalReport clsTerminalReport = new Data.TerminalReport(mConnection, mTransaction);
                mConnection = clsTerminalReport.Connection; mTransaction = clsTerminalReport.Transaction;

				DateTime dteTransactionDate = Convert.ToDateTime(lblTransDate.Text);

				DateTime dteStartCutOffTime = Convert.ToDateTime(Convert.ToDateTime(lblTransDate.Text).ToString("yyyy-MM-dd") + " " + mclsTerminalDetails.StartCutOffTime);
				DateTime dteEndCutOffTime = Convert.ToDateTime(Convert.ToDateTime(lblTransDate.Text).ToString("yyyy-MM-dd") + " " + mclsTerminalDetails.EndCutOffTime);

				// if StartCutOffTime is greater than EndCutOffTime
				// this means that EndCutOffTime is in the morning.
				// Add 1 more day.
				if (dteStartCutOffTime >= dteEndCutOffTime)
					dteEndCutOffTime = dteEndCutOffTime.AddDays(1);

				DateTime dteAllowedStartDateTime; DateTime dteAllowedEndDateTime;
				if (dteTransactionDate < dteEndCutOffTime)
				{
					dteAllowedStartDateTime = Convert.ToDateTime(Convert.ToDateTime(lblTransDate.Text).ToString("yyyy-MM-dd") + " " + mclsTerminalDetails.EndCutOffTime).AddDays(-1).AddMilliseconds(1);
					dteAllowedEndDateTime = Convert.ToDateTime(Convert.ToDateTime(lblTransDate.Text).ToString("yyyy-MM-dd") + " " + mclsTerminalDetails.StartCutOffTime);
				}
				else
				{
					dteAllowedStartDateTime = Convert.ToDateTime(Convert.ToDateTime(lblTransDate.Text).ToString("yyyy-MM-dd") + " " + mclsTerminalDetails.EndCutOffTime).AddMilliseconds(1);
					dteAllowedEndDateTime = Convert.ToDateTime(Convert.ToDateTime(lblTransDate.Text).ToString("yyyy-MM-dd") + " " + mclsTerminalDetails.StartCutOffTime).AddDays(1);
				}
				if (dteTransactionDate < dteAllowedEndDateTime)
				{
					dteStartCutOffTime = dteStartCutOffTime.AddDays(1);
					dteEndCutOffTime = dteEndCutOffTime.AddDays(1);
				}

				DateTime dtePreviousStartCutOffTime = dteStartCutOffTime.AddDays(-1);
				DateTime dtePreviousEndCutOffTime = dteEndCutOffTime.AddDays(-1);
				DateTime dtePreviousAllowedStartDateTime = dteAllowedStartDateTime.AddDays(-1);
				DateTime dtePreviousAllowedEndDateTime = dteAllowedEndDateTime.AddDays(-1);

                DateTime dteMAXDateLastInitialized = clsTerminalReport.MAXDateLastInitialized(mclsTerminalDetails.BranchDetails.BranchID, mclsTerminalDetails.TerminalNo, Constants.C_DATE_MIN_VALUE);

				clsEvent.AddEventLn("Checking if MAXDateLastInitialized: " + dteMAXDateLastInitialized.ToString("MM/dd/yyyy HH:mm") + " is already initialized.", true);
				if (dteMAXDateLastInitialized >= dteAllowedStartDateTime && dteMAXDateLastInitialized <= dteAllowedEndDateTime)
				{
					clsDatabase.CommitAndDispose();
					clsEvent.AddEventLn("Transaction is not allowed, ZRead is already initialized for this date.", true);
					MessageBox.Show("Sorry initialization is not permitted this time, ZRead is already initialized for this date.", "RetailPlus", MessageBoxButtons.OK, MessageBoxIcon.Information);
					txtBarCode.Text = "";
					return boRetValue;
				}
				clsEvent.AddEventLn("OK. MAXDateLastInitialized: " + dteMAXDateLastInitialized.ToString("yyyy-MM-dd HH:mm") + " is for previous zreading.", true);

				if (dteMAXDateLastInitialized < dteAllowedStartDateTime)
				{
					if (dteMAXDateLastInitialized >= dtePreviousAllowedStartDateTime && dteMAXDateLastInitialized <= dtePreviousEndCutOffTime)
					{
						clsEvent.AddEventLn("OK: AllowedStartDateTime [" + dteAllowedStartDateTime.ToString("yyyy-MM-dd HH:mm:ss") + "] is now less than MAXDateLastInitialized [" + dteMAXDateLastInitialized.ToString("yyyy-MM-dd HH:mm:ss") + "].", true);
					}
					else if(mclsTerminalDetails.CheckCutOffTime == true)
					{
						clsEvent.AddEventLn("Transaction is not allowed, transaction date is 2-Days delayed. Please restart FE.", true);
						MessageBox.Show("Transaction is not allowed, transaction date is 2Days delayed. Please restart FE." +
							Environment.NewLine + "Sorry selling is not permitted this time, Please consult for the Selling time.", "RetailPlus", MessageBoxButtons.OK, MessageBoxIcon.Information);
						txtBarCode.Text = "";
						return false;
					}
				}
				if (dteMAXDateLastInitialized > dteTransactionDate)
				{
					clsDatabase.CommitAndDispose();
					clsEvent.AddEventLn("Transaction is not allowed, transaction date is delayed. Please restart FE.", true);
					MessageBox.Show("Transaction is not allowed, transaction date is delayed. Please restart FE.", "RetailPlus", MessageBoxButtons.OK, MessageBoxIcon.Information);
					txtBarCode.Text = "";
					return boRetValue;
				}
				clsEvent.AddEventLn("OK to initialize...", true);
                clsDatabase.CommitAndDispose();

				boRetValue = true;

				return boRetValue;
			}
			catch (Exception ex)
			{
				InsertErrorLogToFile(ex);
				return false;
			}
		}
		private bool CreateTransaction()
		{
			Boolean boRetValue = true;
			try
			{
				Data.TerminalReport clsTerminalReport = new Data.TerminalReport(mConnection, mTransaction);
                mConnection = clsTerminalReport.Connection; mTransaction = clsTerminalReport.Transaction;

				DateTime dteTransactionDate = Convert.ToDateTime(lblTransDate.Text);

				// Added checking of Cutofftime
                if (mclsTerminalDetails.CheckCutOffTime)
                {
                    DateTime dteStartCutOffTime = Convert.ToDateTime(Convert.ToDateTime(lblTransDate.Text).ToString("yyyy-MM-dd") + " " + mclsTerminalDetails.StartCutOffTime);
                    DateTime dteEndCutOffTime = Convert.ToDateTime(Convert.ToDateTime(lblTransDate.Text).ToString("yyyy-MM-dd") + " " + mclsTerminalDetails.EndCutOffTime);

                    // if StartCutOffTime is greater than EndCutOffTime
                    // this means that EndCutOffTime is in the morning.
                    // Add 1 more day.
                    if (dteStartCutOffTime >= dteEndCutOffTime)
                        dteEndCutOffTime = dteEndCutOffTime.AddDays(1);

                    DateTime dteAllowedStartDateTime; DateTime dteAllowedEndDateTime;
                    if (dteTransactionDate < dteEndCutOffTime)
                    {
                        dteAllowedStartDateTime = Convert.ToDateTime(Convert.ToDateTime(lblTransDate.Text).ToString("yyyy-MM-dd") + " " + mclsTerminalDetails.EndCutOffTime).AddDays(-1).AddMilliseconds(1);
                        dteAllowedEndDateTime = Convert.ToDateTime(Convert.ToDateTime(lblTransDate.Text).ToString("yyyy-MM-dd") + " " + mclsTerminalDetails.StartCutOffTime);
                    }
                    else
                    {
                        dteAllowedStartDateTime = Convert.ToDateTime(Convert.ToDateTime(lblTransDate.Text).ToString("yyyy-MM-dd") + " " + mclsTerminalDetails.EndCutOffTime).AddMilliseconds(1);
                        dteAllowedEndDateTime = Convert.ToDateTime(Convert.ToDateTime(lblTransDate.Text).ToString("yyyy-MM-dd") + " " + mclsTerminalDetails.StartCutOffTime).AddDays(1);
                    }
                    if (dteTransactionDate < dteAllowedEndDateTime)
                    {
                        dteStartCutOffTime = dteStartCutOffTime.AddDays(1);
                        dteEndCutOffTime = dteEndCutOffTime.AddDays(1);
                    }

                    DateTime dtePreviousStartCutOffTime = dteStartCutOffTime.AddDays(-1);
                    DateTime dtePreviousEndCutOffTime = dteEndCutOffTime.AddDays(-1);
                    DateTime dtePreviousAllowedStartDateTime = dteAllowedStartDateTime.AddDays(-1);
                    DateTime dtePreviousAllowedEndDateTime = dteAllowedEndDateTime.AddDays(-1);

                    clsEvent.AddEventLn("Checking Transactiondate: '" + dteTransactionDate.ToString("yyyy-MM-dd HH:mm") + "' before creating new transaction if not between cutoff: " + dteStartCutOffTime.ToString("MM/dd/yyyy HH:mm") + " & " + dteEndCutOffTime.ToString("MM/dd/yyyy HH:mm"), true);
                    if (dteTransactionDate >= dteStartCutOffTime && dteTransactionDate <= dteEndCutOffTime)
                    {
                        clsEvent.AddEventLn("Transaction is not allowed, transaction date is within the cutofftime.", true);
                        MessageBox.Show("Sorry selling is not permitted this time, Please consult for the Selling time.", "RetailPlus", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txtBarCode.Text = "";
                        return false;
                    }
                    clsEvent.AddEventLn("Transaction is ok, transaction date is within allowable transaction date.", true);

                    clsEvent.AddEventLn("Checking Transactiondate: '" + dteTransactionDate.ToString("yyyy-MM-dd HH:mm") + "' before creating new transaction if between selling time: " + dteAllowedStartDateTime.ToString("MM/dd/yyyy HH:mm") + " & " + dteAllowedEndDateTime.ToString("MM/dd/yyyy HH:mm"), true);
                    if (dteTransactionDate < dteAllowedStartDateTime && dteTransactionDate > dteAllowedEndDateTime)
                    {
                        clsEvent.AddEventLn("Transaction is not allowed, transaction date is not within the allowable transaction date.", true);
                        MessageBox.Show("Sorry selling is not permitted this time, Please consult for the Selling time.", "RetailPlus", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txtBarCode.Text = "";
                        return false;
                    }
                    clsEvent.AddEventLn("Transaction is ok, transaction date is within allowable transaction date.", true);

                    DateTime dteMAXDateLastInitialized = clsTerminalReport.MAXDateLastInitialized(mclsTerminalDetails.BranchDetails.BranchID, mclsTerminalDetails.TerminalNo, Constants.C_DATE_MIN_VALUE);

                    clsEvent.AddEventLn("PreviousStartCutOff       :    " + dtePreviousStartCutOffTime.ToString("yyyy-MM-dd HH:mm:ss"), true);
                    clsEvent.AddEventLn("PreviousEndCutOff         :    " + dtePreviousEndCutOffTime.ToString("yyyy-MM-dd HH:mm:ss"), true);
                    clsEvent.AddEventLn("PrevAllowedStartDateTime  :    " + dtePreviousAllowedStartDateTime.ToString("yyyy-MM-dd HH:mm:ss"), true);
                    clsEvent.AddEventLn("PrevAllowedEndDateTime    :    " + dtePreviousAllowedEndDateTime.ToString("yyyy-MM-dd HH:mm:ss"), true);

                    clsEvent.AddEventLn("StartCutOff               :    " + dteStartCutOffTime.ToString("yyyy-MM-dd HH:mm:ss"), true);
                    clsEvent.AddEventLn("EndCutOff                 :    " + dteEndCutOffTime.ToString("yyyy-MM-dd HH:mm:ss"), true);
                    clsEvent.AddEventLn("AllowedStartDateTime      :    " + dteAllowedStartDateTime.ToString("yyyy-MM-dd HH:mm:ss"), true);
                    clsEvent.AddEventLn("AllowedEndDateTime        :    " + dteAllowedEndDateTime.ToString("yyyy-MM-dd HH:mm:ss"), true);
                    clsEvent.AddEventLn("MAXDateLastInitialized    :    " + dteMAXDateLastInitialized.ToString("yyyy-MM-dd HH:mm:ss"), true);

                    clsEvent.AddEventLn("Checking if MAXDateLastInitialized: " + dteMAXDateLastInitialized.ToString("MM/dd/yyyy HH:mm") + " is already initialized.", true);
                    if (dteMAXDateLastInitialized >= dteAllowedStartDateTime && dteMAXDateLastInitialized <= dteAllowedEndDateTime)
                    {
                        clsTerminalReport.CommitAndDispose();
                        clsEvent.AddEventLn("Transaction is not allowed, ZRead is already initialized for this date.", true);
                        MessageBox.Show("Sorry selling is not permitted this time, ZRead is already initialized for this date.", "RetailPlus", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txtBarCode.Text = "";
                        return false;
                    }
                    clsEvent.AddEventLn("OK. MAXDateLastInitialized: " + dteMAXDateLastInitialized.ToString("yyyy-MM-dd HH:mm") + " is for previous zreading.", true);
                    clsEvent.AddEventLn("Checking Transactiondate: '" + dteMAXDateLastInitialized.AddDays(1).ToString("yyyy-MM-dd HH:mm") + "' < " + dteAllowedStartDateTime.ToString("MM/dd/yyyy HH:mm"), true);
                    if (dteMAXDateLastInitialized < dteAllowedStartDateTime)
                    {
                        if (dteMAXDateLastInitialized >= dtePreviousAllowedStartDateTime && dteMAXDateLastInitialized <= dtePreviousEndCutOffTime)
                        {
                            clsEvent.AddEventLn("OK: AllowedStartDateTime [" + dteAllowedStartDateTime.ToString("yyyy-MM-dd HH:mm:ss") + "] is now less than MAXDateLastInitialized [" + dteMAXDateLastInitialized.ToString("yyyy-MM-dd HH:mm:ss") + "].", true);
                        }
                        else
                        {
                            clsTerminalReport.CommitAndDispose();
                            clsEvent.AddEventLn("Transaction is not allowed, transaction date is 2-Days delayed. Please restart FE.", true);
                            MessageBox.Show("Transaction is not allowed, transaction date is 2Days delayed. Please restart FE." +
                                Environment.NewLine + "Sorry selling is not permitted this time, Please consult for the Selling time.", "RetailPlus", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            txtBarCode.Text = "";
                            return false;
                        }
                    }
                    if (dteMAXDateLastInitialized > dteTransactionDate)
                    {
                        clsTerminalReport.CommitAndDispose();
                        clsEvent.AddEventLn("Transaction is not allowed, transaction date is delayed. Please restart FE.", true);
                        MessageBox.Show("Transaction is not allowed, transaction date is delayed. Please restart FE." +
                            Environment.NewLine + "Sorry selling is not permitted this time, Please consult for the Selling time.", "RetailPlus", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txtBarCode.Text = "";
                        return false;
                    }
                    clsEvent.AddEventLn("OK to sell...", true);
                }
                else 
                {
                    // this is to check if the last maxdate is too far from current date. that means they did not zread for at least 2 days which is not allowed.
                    // or the system date has been altered.
                    DateTime dteMAXDateLastInitialized = clsTerminalReport.MAXDateLastInitialized(mclsTerminalDetails.BranchDetails.BranchID, mclsTerminalDetails.TerminalNo, Constants.C_DATE_MIN_VALUE);

                    if (dteTransactionDate > dteMAXDateLastInitialized.AddDays(2))
                    {
                        clsTerminalReport.CommitAndDispose();
                        if (MessageBox.Show("Transaction date is too far from the last ZReadDate [" + dteMAXDateLastInitialized.ToString("yyyy-MM-dd HH:mm") + "]. Do you want to continue?", "RetailPlus", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) == System.Windows.Forms.DialogResult.No)
                        {
                            clsEvent.AddEventLn("Transaction is not allowed, transaction date is too far from the last ZReadDate [" + dteMAXDateLastInitialized.ToString("yyyy-MM-dd HH:mm") + "]. Please change the date.", true);
                            MessageBox.Show("Transaction is not allowed, transaction date is too far from the last ZReadDate [" + dteMAXDateLastInitialized.ToString("yyyy-MM-dd HH:mm") + "]. Please change the date." +
                                Environment.NewLine + "Sorry selling is not permitted this time, Please consult for the Selling time.", "RetailPlus", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            txtBarCode.Text = "";
                            return false;
                        }
                        clsTerminalReport = new Data.TerminalReport(mConnection, mTransaction);
                        mConnection = clsTerminalReport.Connection; mTransaction = clsTerminalReport.Transaction;
                    }
                }

				clsEvent.AddEventLn("[" + lblCashier.Text + "] Creating new transaction.", true);

				mclsSalesTransactionDetails = new Data.SalesTransactionDetails();
				try { mclsSalesTransactionDetails.CashierID = Convert.ToInt64(lblCashier.Tag); }
				catch { }

                //mclsSalesTransactionDetails.CustomerID = Convert.ToInt64(lblCustomer.Tag);
                //mclsSalesTransactionDetails.CustomerName = lblCustomer.Text;

                mclsSalesTransactionDetails.CustomerDetails = mclsContactDetails;
                mclsSalesTransactionDetails.CustomerID = mclsContactDetails.ContactID;
                mclsSalesTransactionDetails.CustomerName = mclsContactDetails.ContactName;
                if (mclsSalesTransactionDetails.CustomerDetails.LastCheckInDate == Constants.C_DATE_MIN_VALUE) mclsSalesTransactionDetails.CustomerDetails.LastCheckInDate = dteTransactionDate;

				mclsSalesTransactionDetails.AgentID = Convert.ToInt64(lblAgent.Tag);
				mclsSalesTransactionDetails.AgentName = lblAgent.Text;
				mclsSalesTransactionDetails.AgentPositionName = lblAgentPositionDepartment.Text;
				mclsSalesTransactionDetails.AgentDepartmentName = lblAgentPositionDepartment.Tag.ToString();
				mclsSalesTransactionDetails.WaiterID = Convert.ToInt64(grpItems.Tag);
				mclsSalesTransactionDetails.WaiterName = grpItems.Text.Remove(0, 11);
				mclsSalesTransactionDetails.CreatedByID = Convert.ToInt64(lblCashier.Tag);
				mclsSalesTransactionDetails.CreatedByName = lblCashier.Text;
				mclsSalesTransactionDetails.CashierID = Convert.ToInt64(lblCashier.Tag);
				mclsSalesTransactionDetails.CashierName = lblCashier.Text;
				mclsSalesTransactionDetails.TransactionDate = dteTransactionDate;
				mclsSalesTransactionDetails.DateSuspended = DateTime.MinValue;
				mclsSalesTransactionDetails.TerminalNo = mclsTerminalDetails.TerminalNo;
				mclsSalesTransactionDetails.BranchID = mclsTerminalDetails.BranchID;
				mclsSalesTransactionDetails.BranchCode = mclsTerminalDetails.BranchDetails.BranchCode;
				mclsSalesTransactionDetails.TransactionStatus = TransactionStatus.Open;
                mclsSalesTransactionDetails.TransactionType = mboIsRefund ? TransactionTypes.POSRefund : TransactionTypes.POSNormal;

                Data.SalesTransactions clsSalesTransactions = new Data.SalesTransactions(mConnection, mTransaction);
                mConnection = clsSalesTransactions.Connection; mTransaction = clsSalesTransactions.Transaction;

                mclsSalesTransactionDetails.TransactionNo = clsSalesTransactions.CreateTransactionNo(mclsTerminalDetails.BranchDetails.BranchID, mclsTerminalDetails.TerminalNo);

				lblTransNo.Text = mclsSalesTransactionDetails.TransactionNo;

				//insert to transaction table 
				
				mclsSalesTransactionDetails.TransactionID = clsSalesTransactions.Insert(mclsSalesTransactionDetails);

				mclsSalesTransactionDetails.RewardCardActive = mclsContactDetails.RewardDetails.RewardActive;
                mclsSalesTransactionDetails.RewardCardNo = mclsContactDetails.RewardDetails.RewardCardNo;
                mclsSalesTransactionDetails.RewardCardExpiry = mclsContactDetails.RewardDetails.ExpiryDate;
                mclsSalesTransactionDetails.RewardPreviousPoints = mclsContactDetails.RewardDetails.RewardPoints;

				lblTransNo.Tag = mclsSalesTransactionDetails.TransactionID.ToString();

                // Sep 24, 2014 : update back the LastCheckInDate to transaction date
                Data.Contacts clsContact = new Data.Contacts(mConnection, mTransaction);
                mConnection = clsContact.Connection; mTransaction = clsContact.Transaction;

                clsContact.UpdateLastCheckInDate(mclsSalesTransactionDetails.CustomerID, dteTransactionDate);

				mboIsInTransaction = true;
                clsTerminalReport.CommitAndDispose();

				InsertAuditLog(AccessTypes.CreateTransaction, "Create transaction #".PadRight(15) + ":" + lblTransNo.Text + " @ Branch: " + mclsTerminalDetails.BranchDetails.BranchCode);
				clsEvent.AddEventLn("Done! Trans #: " + lblTransNo.Text + " has been created.", true);
			}
			catch (Exception ex)
			{ 
                InsertErrorLogToFile(ex); 
                boRetValue = false; 
            }

			return boRetValue;
		}
		private void LoadTransaction(string stTransactionNo, string pstrTerminalNo)
		{
			Cursor.Current = Cursors.WaitCursor;
			try
			{
				clsEvent.AddEvent("Loading transaction : " + stTransactionNo);

				Data.SalesTransactions clsSalesTransactions = new Data.SalesTransactions(mConnection, mTransaction);
                mConnection = clsSalesTransactions.Connection; mTransaction = clsSalesTransactions.Transaction;

				mclsSalesTransactionDetails = clsSalesTransactions.Details(stTransactionNo, pstrTerminalNo, mclsTerminalDetails.BranchID);
				
				Data.Contacts clsContact = new Data.Contacts(mConnection, mTransaction);
				Data.ContactDetails clsContactDetails = clsContact.Details(mclsSalesTransactionDetails.CustomerID);
				LoadContact(AceSoft.RetailPlus.Data.ContactGroupCategory.CUSTOMER, clsContactDetails);

				//mclsSalesTransactionDetails.RewardCardNo = clsContactDetails.RewardDetails.RewardCardNo;
				//mclsSalesTransactionDetails.RewardCardExpiry = clsContactDetails.RewardDetails.ExpiryDate;
				//mclsSalesTransactionDetails.RewardPreviousPoints = clsContactDetails.RewardDetails.RewardPoints;

                if (mclsSalesTransactionDetails.TransactionStatus == TransactionStatus.Refund || mclsSalesTransactionDetails.TransactionType == TransactionTypes.POSRefund)
				{
					mboIsRefund = true;
					lblSubtotalName.Text = "SUBTOTAL: REFUND";
					lblOrderType.Visible = false;
				}
				lblTransNo.Text = mclsSalesTransactionDetails.TransactionNo;
				lblTransNo.Tag = mclsSalesTransactionDetails.TransactionID.ToString();
				lblCustomer.Text = mclsSalesTransactionDetails.CustomerName;
				lblCustomer.Tag = mclsSalesTransactionDetails.CustomerID.ToString();
				lblAgent.Text = mclsSalesTransactionDetails.AgentName;
				lblAgent.Tag = mclsSalesTransactionDetails.AgentID.ToString();
				lblAgentPositionDepartment.Text = mclsSalesTransactionDetails.AgentPositionName;
				lblAgentPositionDepartment.Tag = mclsSalesTransactionDetails.AgentDepartmentName;
				grpItems.Text = "Served by: " + mclsSalesTransactionDetails.WaiterName;
				grpItems.Tag = mclsSalesTransactionDetails.WaiterID;

				lblTransDate.Text = mclsSalesTransactionDetails.TransactionDate.ToString("MMM. dd, yyyy hh:mm:ss tt");
                mdteOverRidingPrintDate = mclsSalesTransactionDetails.TransactionDate;

				lblTransDiscount.Tag = mclsSalesTransactionDetails.TransDiscountType.ToString("d");
                lblConsignment.Visible = mclsSalesTransactionDetails.isConsignment;

				Data.SalesTransactionItems clsItems = new Data.SalesTransactionItems(mConnection, mTransaction);
                mConnection = clsItems.Connection; mTransaction = clsItems.Transaction;

				Data.SalesTransactionItemDetails[] TransactionItems = clsItems.Details(mclsSalesTransactionDetails.TransactionID, mclsSalesTransactionDetails.TransactionDate);

				clsEvent.AddEventLn("Done loading transaction : " + stTransactionNo, true);

				if (mclsTerminalDetails.AutoPrint != PrintingPreference.Auto)
					LoadResumedItems(TransactionItems, true);
				else
					LoadResumedItems(TransactionItems, false);

				mboIsInTransaction = true;

                clsSalesTransactions.CommitAndDispose();
			}
			catch (Exception ex)
			{
				InsertErrorLogToFile(ex, "ERROR!!! Loading transaction. TRACE: ");
			}
			Cursor.Current = Cursors.Default;
		}
		private void LoadResumedItems(Data.SalesTransactionItemDetails[] Items, bool WillPrintItem)
		{
			Cursor.Current = Cursors.WaitCursor;
			try
			{
				clsEvent.AddEventLn("loading items...", true);
				ItemDataTable.Rows.Clear();

				foreach (Data.SalesTransactionItemDetails item in Items)
				{
					System.Data.DataRow dr = ItemDataTable.NewRow();

                    dr = setCurrentRowItemDetails(dr, item);
					dr["ItemNo"] = ItemDataTable.Rows.Count + 1;

					ItemDataTable.Rows.Add(dr);
                    clsEvent.AddEventLn("Loading item no: " + dr["ItemNo"].ToString() + " Barcode".PadRight(15) + ":" + dr["BarCode"].ToString() + " " + dr["ProductCode"].ToString() + " Price".PadRight(15) + ":" + dr["Price"].ToString(), true);

					//dgItems.CurrentRowIndex = ItemDataTable.Rows.Count;
					//dgItems.Select(dgItems.CurrentRowIndex);
					//SetItemDetails();

                    string strProductCode = item.ProductCode;
                    if (!string.IsNullOrEmpty(item.MatrixDescription)) strProductCode += "-" + item.MatrixDescription;

					if (WillPrintItem)
					{
						if (item.TransactionItemStatus == TransactionItemStatus.Return)
						{
							if (mclsTerminalDetails.ReceiptType == TerminalReceiptType.Default)
                                PrintItem(item.ItemNo, strProductCode + "-RET", item.ProductUnitCode, item.Quantity, item.Price, item.Discount, item.PromoApplied, item.Amount, item.VAT, item.EVAT);
						}
						else if (item.TransactionItemStatus != TransactionItemStatus.Void)
						{
							if (mclsTerminalDetails.ReceiptType == TerminalReceiptType.Default)
                                PrintItem(item.ItemNo, strProductCode, item.ProductUnitCode, item.Quantity, item.Price, item.Discount, item.PromoApplied, item.Amount, item.VAT, item.EVAT);
						}
					}
					else
					{
						if (mclsTerminalDetails.AutoPrint == PrintingPreference.Auto)
						{
							if (item.TransactionItemStatus == TransactionItemStatus.Return)
							{
								if (mclsTerminalDetails.ReceiptType == TerminalReceiptType.Default)
                                    PrintItem(item.ItemNo, strProductCode + "-RET", item.ProductUnitCode, item.Quantity, item.Price, item.Discount, item.PromoApplied, item.Amount, item.VAT, item.EVAT);
							}
							else if (item.TransactionItemStatus != TransactionItemStatus.Void)
							{
								if (mclsTerminalDetails.ReceiptType == TerminalReceiptType.Default)
                                    PrintItem(item.ItemNo, strProductCode, item.ProductUnitCode, item.Quantity, item.Price, item.Discount, item.PromoApplied, item.Amount, item.VAT, item.EVAT);
							}
						}
					}
				}
				if (ItemDataTable.Rows.Count != 0)
				{
					dgItems.CurrentRowIndex = 0;  //ItemDataTable.Rows.Count;
					dgItems.Select(dgItems.CurrentRowIndex);
					SetItemDetails();
				}

				clsEvent.AddEventLn("Done loading transaction items.", true);

				ComputeSubTotal(); setTotalDetails();
			}
			catch (Exception ex)
			{
				InsertErrorLogToFile(ex, "ERROR!!! Loading transaction items. TRACE: ");
			}
			Cursor.Current = Cursors.Default;
		}

        /// <summary>
        /// This should always be called to display the TotalDetails
        /// </summary>
        private void setTotalDetails()
        {
            lblSubTotal.Text = mclsSalesTransactionDetails.SubTotal.ToString("###,##0.#0");

            if (mclsSalesTransactionDetails.TransDiscountType == DiscountTypes.NotApplicable)
            {
                lblTransDiscount.Text = "Less 0% / 0.00";
            }
            else if (mclsSalesTransactionDetails.TransDiscountType == DiscountTypes.FixedValue)
            {
                lblTransDiscount.Text = "Less " + mclsSalesTransactionDetails.TransDiscount.ToString("#,##0") + " / " + mclsSalesTransactionDetails.Discount.ToString("#,##0.#0");
            }
            else if (mclsSalesTransactionDetails.DiscountCode == mclsTerminalDetails.SeniorCitizenDiscountCode)
            {
                lblTransDiscount.Text = "Less " + mclsSalesTransactionDetails.TransDiscount.ToString("#,##0") + " % / " + mclsSalesTransactionDetails.Discount.ToString("#,##0.#0");
            }
            else if (mclsSalesTransactionDetails.TransDiscountType == DiscountTypes.Percentage)
            {
                lblTransDiscount.Text = "Less " + mclsSalesTransactionDetails.TransDiscount.ToString("#,##0") + " % / " + mclsSalesTransactionDetails.Discount.ToString("#,##0.#0");
            }

            if (mclsSalesTransactionDetails.ChargeType == ChargeTypes.NotApplicable)
            {
                lblTransCharge.Text = "Plus 0% / 0.00";
            }
            else if (mclsSalesTransactionDetails.ChargeType == ChargeTypes.Percentage)
            {
                lblTransCharge.Text = "Plus " + mclsSalesTransactionDetails.ChargeAmount.ToString("#,##0") + " % / " + mclsSalesTransactionDetails.Charge.ToString("#,##0.#0");
            }
            else
            {
                lblTransCharge.Text = "Plus " + mclsSalesTransactionDetails.ChargeAmount.ToString("#,##0") + " / " + mclsSalesTransactionDetails.Charge.ToString("#,##0.#0");
            }
        }
		private void SetItemDetails()
		{
			int iRow = dgItems.CurrentRowIndex;

			if (iRow >= 0)
			{
				lblDescription.Text = dgItems[iRow, 3].ToString() + " - " + dgItems[iRow, 5].ToString();
				lblCategory.Text = dgItems[iRow, 19].ToString() + " / " + dgItems[iRow, 20].ToString();
				lblProperties.Text = dgItems[iRow, 18].ToString();
				lblProperties.Text = " " + lblProperties.Text.Replace(";", "\n");
			}
		}
		private bool IsStartCutOffTimeOK()
		{
			bool bolRetValue = false;
			try
			{
				// check if with Cutofftime
				if (mclsTerminalDetails.CheckCutOffTime)
				{
					clsEvent.AddEventLn("Checking StartCutOffTime vs MAXDateLastInitialized...", true);

					Data.TerminalReportDetails clsTerminalReportDetails;
					Data.TerminalReport clsTerminalReport = new Data.TerminalReport(mConnection, mTransaction);
                    mConnection = clsTerminalReport.Connection; mTransaction = clsTerminalReport.Transaction;

					DateTime dteTransactionDate = Convert.ToDateTime(lblTransDate.Text);

                    DateTime dteMAXDateLastInitialized = clsTerminalReport.MAXDateLastInitialized(mclsTerminalDetails.BranchDetails.BranchID, mclsTerminalDetails.TerminalNo, Constants.C_DATE_MIN_VALUE);
					DateTime dteStartCutOffTime = Convert.ToDateTime(Convert.ToDateTime(lblTransDate.Text).ToString("yyyy-MM-dd") + " " + mclsTerminalDetails.StartCutOffTime);
					DateTime dteEndCutOffTime = Convert.ToDateTime(Convert.ToDateTime(lblTransDate.Text).ToString("yyyy-MM-dd") + " " + mclsTerminalDetails.EndCutOffTime);

					// if StartCutOffTime is greater than EndCutOffTime
					// this means that EndCutOffTime is in the morning.
					// Add 1 more day.
					if (dteStartCutOffTime >= dteEndCutOffTime)
						dteEndCutOffTime = dteEndCutOffTime.AddDays(1);

					DateTime dteAllowedStartDateTime; DateTime dteAllowedEndDateTime;
					if (dteTransactionDate < dteEndCutOffTime)
					{
						dteAllowedStartDateTime = Convert.ToDateTime(Convert.ToDateTime(lblTransDate.Text).ToString("yyyy-MM-dd") + " " + mclsTerminalDetails.EndCutOffTime).AddDays(-1).AddMilliseconds(1);
						dteAllowedEndDateTime = Convert.ToDateTime(Convert.ToDateTime(lblTransDate.Text).ToString("yyyy-MM-dd") + " " + mclsTerminalDetails.StartCutOffTime);
					}
					else
					{
						dteAllowedStartDateTime = Convert.ToDateTime(Convert.ToDateTime(lblTransDate.Text).ToString("yyyy-MM-dd") + " " + mclsTerminalDetails.EndCutOffTime).AddMilliseconds(1);
						dteAllowedEndDateTime = Convert.ToDateTime(Convert.ToDateTime(lblTransDate.Text).ToString("yyyy-MM-dd") + " " + mclsTerminalDetails.StartCutOffTime).AddDays(1);
					}
					if (dteTransactionDate < dteAllowedEndDateTime)
					{
						dteStartCutOffTime = dteStartCutOffTime.AddDays(1);
						dteEndCutOffTime = dteEndCutOffTime.AddDays(1);
					}

					DateTime dtePreviousStartCutOffTime = dteStartCutOffTime.AddDays(-1);
					DateTime dtePreviousEndCutOffTime = dteEndCutOffTime.AddDays(-1);
					DateTime dtePreviousAllowedStartDateTime = dteAllowedStartDateTime.AddDays(-1);
					DateTime dtePreviousAllowedEndDateTime = dteAllowedEndDateTime.AddDays(-1);

				Back:
					clsEvent.AddEventLn("PreviousStartCutOff       :    " + dtePreviousStartCutOffTime.ToString("yyyy-MM-dd HH:mm:ss"), true);
					clsEvent.AddEventLn("PreviousEndCutOff         :    " + dtePreviousEndCutOffTime.ToString("yyyy-MM-dd HH:mm:ss"), true);
					clsEvent.AddEventLn("PrevAllowedStartDateTime  :    " + dtePreviousAllowedStartDateTime.ToString("yyyy-MM-dd HH:mm:ss"), true);
					clsEvent.AddEventLn("PrevAllowedEndDateTime    :    " + dtePreviousAllowedEndDateTime.ToString("yyyy-MM-dd HH:mm:ss"), true);

					clsEvent.AddEventLn("StartCutOff               :    " + dteStartCutOffTime.ToString("yyyy-MM-dd HH:mm:ss"), true);
					clsEvent.AddEventLn("EndCutOff                 :    " + dteEndCutOffTime.ToString("yyyy-MM-dd HH:mm:ss"), true);
					clsEvent.AddEventLn("AllowedStartDateTime      :    " + dteAllowedStartDateTime.ToString("yyyy-MM-dd HH:mm:ss"), true);
					clsEvent.AddEventLn("AllowedEndDateTime        :    " + dteAllowedEndDateTime.ToString("yyyy-MM-dd HH:mm:ss"), true);
					clsEvent.AddEventLn("MAXDateLastInitialized    :    " + dteMAXDateLastInitialized.ToString("yyyy-MM-dd HH:mm:ss"), true);
				
					if (dteMAXDateLastInitialized < dteAllowedStartDateTime)
					{
						if (dteMAXDateLastInitialized >= dtePreviousAllowedStartDateTime && dteMAXDateLastInitialized <= dtePreviousEndCutOffTime)
						{
							if (dteTransactionDate >= dteStartCutOffTime && dteTransactionDate <= dteEndCutOffTime)
							{
								MessageBox.Show("Today's EOD was not performed", "RetailPlus", MessageBoxButtons.OK, MessageBoxIcon.Information);

								clsEvent.AddEventLn("Today's EOD was not performed: System found AllowedStartDateTime [" + dteAllowedStartDateTime.ToString("yyyy-MM-dd HH:mm:ss") + "] > MAXDateLastInitialized [" + dteMAXDateLastInitialized.ToString("yyyy-MM-dd HH:mm:ss") + "].", true);
								clsEvent.AddEventLn("System is Auto-Initializing ZREAD", true);

                                clsTerminalReportDetails = clsTerminalReport.Details(mclsTerminalDetails.BranchDetails.BranchID, mclsTerminalDetails.TerminalNo);
                                mstrBeginningTransactionNo = clsTerminalReportDetails.BeginningTransactionNo;

								PrintZRead(false, clsTerminalReportDetails);

								dteMAXDateLastInitialized = Convert.ToDateTime(dteMAXDateLastInitialized.AddDays(1));
								clsTerminalReport.InitializeZRead(mclsTerminalDetails.BranchDetails.BranchID, mclsTerminalDetails.TerminalNo, "System Auto Z-Read", dteMAXDateLastInitialized, true);
								clsEvent.AddEventLn("Done.", true);
								goto Back;
							}
							else
							{
								bolRetValue = true;
								clsEvent.AddEventLn("OK: AllowedStartDateTime [" + dteAllowedStartDateTime.ToString("yyyy-MM-dd HH:mm:ss") + "] is now less than MAXDateLastInitialized [" + dteMAXDateLastInitialized.ToString("yyyy-MM-dd HH:mm:ss") + "].", true);
							}
						}
						else
						{
							MessageBox.Show("Previous' day's EOD was not performed", "RetailPlus", MessageBoxButtons.OK, MessageBoxIcon.Information);

							clsEvent.AddEventLn("Previous' day's EOD was not performed: System found AllowedStartDateTime [" + dteAllowedStartDateTime.ToString("yyyy-MM-dd HH:mm:ss") + "] > MAXDateLastInitialized [" + dteMAXDateLastInitialized.ToString("yyyy-MM-dd HH:mm:ss") + "].", true);
							clsEvent.AddEventLn("System is Auto-Initializing ZREAD", true);

                            clsTerminalReportDetails = clsTerminalReport.Details(mclsTerminalDetails.BranchDetails.BranchID, mclsTerminalDetails.TerminalNo);
                            mstrBeginningTransactionNo = clsTerminalReportDetails.BeginningTransactionNo;

							PrintZRead(false, clsTerminalReportDetails);

							dteMAXDateLastInitialized = Convert.ToDateTime(dteMAXDateLastInitialized.AddDays(1));
                            clsTerminalReport.InitializeZRead(mclsTerminalDetails.BranchDetails.BranchID, mclsTerminalDetails.TerminalNo, "System Auto Z-Read", dteMAXDateLastInitialized, true);
							clsEvent.AddEventLn("Done.",true);
							goto Back;
						}
					}
					else
					{
						bolRetValue = true;
						clsEvent.AddEventLn("OK: AllowedStartDateTime [" + dteAllowedStartDateTime.ToString("yyyy-MM-dd HH:mm:ss") + "] is now less than MAXDateLastInitialized [" + dteMAXDateLastInitialized.ToString("yyyy-MM-dd HH:mm:ss") + "].", true);
					}

					clsTerminalReport.CommitAndDispose();

				}
				else { clsEvent.AddEventLn("OK: StartCutOffTime not configured if greater than MAXDateLastInitialized", true); }

				return bolRetValue;
			}
			catch (Exception ex)
			{
				InsertErrorLogToFile(ex);
				return false;
			}
		}
		private delegate void SendRLCDelegate();
		private void SendRLC()
		{
			try
			{
				tmr.Enabled = false;

				Data.TerminalReportHistory clsTerminalReportHistory = new Data.TerminalReportHistory(mConnection, mTransaction);
                mConnection = clsTerminalReportHistory.Connection; mTransaction = clsTerminalReportHistory.Transaction;

                DateTime dteDateToProcess = clsTerminalReportHistory.getRLCDateLastInitialized(mclsTerminalDetails.BranchID, mclsTerminalDetails.TerminalNo);
				clsTerminalReportHistory.CommitAndDispose();

				if (dteDateToProcess == DateTime.MinValue)
				{
					grpRLC.Visible = false;
				}
				else
				{
					try
					{
						try { grpRLC.Visible = true; }
						catch { }
					Back:
                        SetText("RLC Notification: Trying to send unsent files...", "lblMallForwarderStatus");

						AceSoft.RetailPlus.Forwarder.RLCDetails clsRLCDetails = new AceSoft.RetailPlus.Forwarder.RLCDetails();
						clsRLCDetails.TenantCode = RLC_CONFIG.TenantCode;
						clsRLCDetails.TenantName = RLC_CONFIG.TenantName;
						clsRLCDetails.OutputDirectory = RLC_CONFIG.OutputDirectory;
						clsRLCDetails.FTPIPAddress = CONFIG.FTPIPAddress;
						clsRLCDetails.FTPUsername = CONFIG.FTPUsername;
						clsRLCDetails.FTPPassword = CONFIG.FTPPassword;
						clsRLCDetails.FTPDirectory = CONFIG.FTPDirectory;

						AceSoft.RetailPlus.Forwarder.RLC clsRLC = new AceSoft.RetailPlus.Forwarder.RLC();
						clsRLC.RLCDetails = clsRLCDetails;
						bool bolCreateAndTransferFile = clsRLC.CreateAndTransferFile(mclsTerminalDetails.BranchDetails.BranchID, mclsTerminalDetails.TerminalNo, dteDateToProcess);

						if (bolCreateAndTransferFile)
						{ tmrRLC.Enabled = false; SetText("Success in sending file(s) to RLC server. Press [Ctrl+C] to close this notification.", "lblMallForwarderStatus"); }
						else
						{ tmrRLC.Enabled = true; SetText("ERROR!!! sending file(s) to RLC server. Press [Ctrl+C] to close this notification.", "lblMallForwarderStatus"); }

						clsTerminalReportHistory = new Data.TerminalReportHistory(mConnection, mTransaction);
                        mConnection = clsTerminalReportHistory.Connection; mTransaction = clsTerminalReportHistory.Transaction;

                        dteDateToProcess = clsTerminalReportHistory.getRLCDateLastInitialized(mclsTerminalDetails.BranchID, mclsTerminalDetails.TerminalNo);
						clsTerminalReportHistory.CommitAndDispose();

						if (dteDateToProcess != DateTime.MinValue)
						{ goto Back; }
					}
					catch
                    { 
                        tmrRLC.Enabled = true;
                        SetText("ERROR!!! sending file(s) to RLC server. Press [Ctrl+C] to close this notification.", "lblMallForwarderStatus");
                    }
				}
			}
            catch (Exception ex)
            {
                InsertErrorLogToFile(ex, lblMallForwarderStatus.Text);
            }
		}

		#endregion

		#region Marquee

		private void StartMarqueeThread()
		{
			try
			{
				MarqueeThread.Abort();
				// give the thread time to die
				Thread.Sleep(100);
				Invalidate();
				MarqueeThread = new Thread(new ThreadStart(DrawMovingText));
				MarqueeThread.Start();
			}
			catch (ThreadAbortException taex)
			{
				StartMarqueeThread();
				clsEvent.AddEventLn("ERROR!!! Marquee Thread has expired. New instance of marquee thread has been started.", true);
				clsEvent.AddEventLn("Error;" + taex.Message, true);
			}
			catch { }
		}
		private void DrawMovingText()
		{
            try
            {
                string marqueemessage = mclsTerminalDetails.MarqueeMessage;

                if (marqueemessage.Length < 140) //97
                    marqueemessage = marqueemessage + " " + marqueemessage;

                // 
                // StringBuilder is used to allow for efficient manipulation of one string, 
                // rather than generating many separate strings
                //
                StringBuilder str = new StringBuilder(marqueemessage);

                int numCycles = lblMessage.Width * 3 + 1;
                for (int i = 0; i < numCycles; ++i)
                {
                    //lblMessage.Text = str.ToString();
                    this.SetText(str.ToString());

                    if (!mboIsInTransaction)
                    {
                        if (lblMessage.Text.Length <= 40)
                            SendStringToTurret(lblMessage.Text);
                        else
                            SendStringToTurret(lblMessage.Text.Substring(0, 40));
                    }
                    // relocate the first char to the end of the string
                    str.Append(str[0]);
                    str.Remove(0, 1);
                    // pause for visual effect
                    Thread.Sleep(800);
                }
            }
            catch { }
		}
        delegate void SetTextCallback(string text, string labelname = "lblMessage");
        private void SetText(string text, string labelname = "lblMessage")
		{
			// InvokeRequired required compares the thread ID of the
			// calling thread to the thread ID of the creating thread.
			// If these threads are different, it returns true.
            if (labelname == "lblMallForwarderStatus")
            {
                if (this.lblMallForwarderStatus.InvokeRequired)
                {
                    SetTextCallback d = new SetTextCallback(SetText);
                    this.Invoke(d, new object[] { text, "lblMallForwarderStatus" });
                }
                else
                {
                    this.lblMallForwarderStatus.Text = text;
                }
            }
            else
            {
                if (this.lblMessage.InvokeRequired)
                {
                    SetTextCallback d = new SetTextCallback(SetText);
                    this.Invoke(d, new object[] { text, "lblMessage" });
                }
                else
                {
                    this.lblMessage.Text = text;
                }
            }
		}

		#endregion

		#region Item Command Click

		private void dgItems_CurrentCellChanged(object sender, EventArgs e)
		{
			try
			{
				if (!mbodgItemRowClick)
				{
					int index = dgItems.CurrentRowIndex;

					try { dgItems.Select(index); }
					catch { }
					SetItemDetails();
					Data.SalesTransactionItemDetails Details = getCurrentRowItemDetails();
					DisplayItemToTurretDelegate DisplayItemToTurretDel = new DisplayItemToTurretDelegate(DisplayItemToTurret);
					DisplayItemToTurretDel.BeginInvoke(Details.Description, Details.ProductUnitCode, Details.Quantity, Details.Price, Details.Discount, Details.PromoApplied, Details.Amount, Details.VAT, Details.EVAT, null, null);

					// ShowMainMenu(03);
				}

			}
			catch { }
			finally { txtBarCode.Focus(); }
		}
		private void dgItems_KeyUp(object sender, KeyEventArgs e)
		{
			try
			{
				txtBarCode.Focus();
			}
			catch { }
		}
		private void dgItems_Click(object sender, EventArgs e)
		{
			try
			{ txtBarCode.Focus(); }
			catch { }
		}
		
		#endregion

	}
}
using System;
using System.Drawing;
using System.Collections;
using System.Windows.Forms;
using AceSoft.RetailPlus.Data;
using MySql.Data.MySqlClient;

namespace AceSoft.RetailPlus.Client.UI
{
	public class TransactionReturnItemSelectWnd : System.Windows.Forms.Form
	{
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.DataGrid dgItems;
		private System.Windows.Forms.DataGridTextBoxColumn TransactionItemsID;
		private System.Windows.Forms.DataGridTextBoxColumn ProductID;
		private System.Windows.Forms.DataGridTextBoxColumn ProductCode;
		private System.Windows.Forms.DataGridTextBoxColumn BarCode;
		private System.Windows.Forms.DataGridTextBoxColumn ProductDesc;
		private System.Windows.Forms.DataGridTextBoxColumn ProductUnitID;
		private System.Windows.Forms.DataGridTextBoxColumn ProductUnitCode;
		private System.Windows.Forms.DataGridTextBoxColumn Quantity;
		private System.Windows.Forms.DataGridTextBoxColumn Price;
		private System.Windows.Forms.DataGridTextBoxColumn Discount;
		private System.Windows.Forms.DataGridTextBoxColumn ItemDiscount;
		private System.Windows.Forms.DataGridTextBoxColumn ItemDiscountType;
		private System.Windows.Forms.DataGridTextBoxColumn Amount;
		private System.Windows.Forms.DataGridTextBoxColumn VAT;
		private System.Windows.Forms.DataGridTextBoxColumn EVAT;
		private System.Windows.Forms.DataGridTextBoxColumn LocalTax;
		private System.Windows.Forms.DataGridTextBoxColumn VariationsMatrixID;
		private System.Windows.Forms.DataGridTextBoxColumn MatrixDescription;
		private System.Windows.Forms.DataGridTextBoxColumn ProductGroup;
		private System.Windows.Forms.DataGridTextBoxColumn ProductSubGroup;
		private System.Windows.Forms.DataGridTextBoxColumn TransactionItemStatus;
		private System.Windows.Forms.DataGridTextBoxColumn DiscountCode;
		private System.Windows.Forms.DataGridTextBoxColumn DiscountRemarks;
		private System.Windows.Forms.DataGridTextBoxColumn ProductPackageID;
		private System.Windows.Forms.DataGridTextBoxColumn MatrixPackageID;
		private System.Windows.Forms.DataGridTextBoxColumn PackageQuantity;
		private System.Windows.Forms.DataGridTextBoxColumn PromoQuantity;
		private System.Windows.Forms.DataGridTextBoxColumn PromoValue;
		private System.Windows.Forms.DataGridTextBoxColumn PromoInPercent;
		private System.Windows.Forms.DataGridTextBoxColumn PromoType;
		private System.Windows.Forms.DataGridTextBoxColumn PromoApplied;
		private System.Windows.Forms.DataGridTextBoxColumn PurchasePrice;
		private System.Windows.Forms.DataGridTextBoxColumn PurchaseAmount;
        private System.Windows.Forms.DataGridTextBoxColumn SupplierID;
        private System.Windows.Forms.DataGridTextBoxColumn SupplierCode;
        private System.Windows.Forms.DataGridTextBoxColumn SupplierName;
        private System.Windows.Forms.DataGridTextBoxColumn ItemRemarks;

		private System.Windows.Forms.DataGridTableStyle dgStyle;
		private System.Windows.Forms.TextBox txtSearch;
        private System.ComponentModel.Container components = null;

		private DialogResult dialog;
		private SalesTransactionItemDetails mDetails;
		private string mstTransactionNo;
        private System.Windows.Forms.PictureBox imgIcon;

        public DialogResult Result
        {
            get
            {
                return dialog;
            }
        }

        public SalesTransactionItemDetails Details
        {
            get
            {
                return mDetails;
            }
        }

        public string TransactionNo
        {
            set
            {
                mstTransactionNo = value;
            }
        }

        public Data.TerminalDetails TerminalDetails { get; set; }

        public Data.SysConfigDetails SysConfigDetails { get; set; }

        public string TransactionTerminalNo { get; set; }

		public TransactionReturnItemSelectWnd()
		{
            //
            // Required for Windows Form Designer support
            //
            InitializeComponent();

            //
            // TODO: Add any constructor code after InitializeComponent call
            //
            try
            { this.BackgroundImage = new Bitmap(Application.StartupPath + "/images/Background.jpg"); }
            catch { }
            try
            { this.imgIcon.Image = new Bitmap(Application.StartupPath + "/images/TransactionItemSelect.jpg"); }
            catch { }

            if (TerminalDetails.MultiInstanceEnabled)
            { this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent; }
            else
            { this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen; }
            this.ShowInTaskbar = TerminalDetails.FORM_Behavior == FORM_Behavior.NON_MODAL; 
		}

		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if(components != null)
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
            this.txtSearch = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.dgItems = new System.Windows.Forms.DataGrid();
            this.dgStyle = new System.Windows.Forms.DataGridTableStyle();
            this.TransactionItemsID = new System.Windows.Forms.DataGridTextBoxColumn();
            this.ProductID = new System.Windows.Forms.DataGridTextBoxColumn();
            this.ProductCode = new System.Windows.Forms.DataGridTextBoxColumn();
            this.BarCode = new System.Windows.Forms.DataGridTextBoxColumn();
            this.ProductDesc = new System.Windows.Forms.DataGridTextBoxColumn();
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
            this.TransactionItemStatus = new System.Windows.Forms.DataGridTextBoxColumn();
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
            this.SupplierID = new System.Windows.Forms.DataGridTextBoxColumn();
            this.SupplierCode = new System.Windows.Forms.DataGridTextBoxColumn();
            this.SupplierName = new System.Windows.Forms.DataGridTextBoxColumn();
            this.ItemRemarks = new System.Windows.Forms.DataGridTextBoxColumn();
            this.imgIcon = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.dgItems)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.imgIcon)).BeginInit();
            this.SuspendLayout();
            // 
            // txtSearch
            // 
            this.txtSearch.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtSearch.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtSearch.Location = new System.Drawing.Point(67, 27);
            this.txtSearch.Name = "txtSearch";
            this.txtSearch.Size = new System.Drawing.Size(298, 23);
            this.txtSearch.TabIndex = 0;
            this.txtSearch.TextChanged += new System.EventHandler(this.txtSearch_TextChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(67, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(125, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "Enter search criteria.";
            // 
            // dgItems
            // 
            this.dgItems.AlternatingBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.dgItems.BackColor = System.Drawing.Color.White;
            this.dgItems.BackgroundColor = System.Drawing.Color.White;
            this.dgItems.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.dgItems.CaptionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.dgItems.CaptionForeColor = System.Drawing.Color.Blue;
            this.dgItems.CaptionVisible = false;
            this.dgItems.DataMember = "";
            this.dgItems.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.dgItems.FlatMode = true;
            this.dgItems.Font = new System.Drawing.Font("Tahoma", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dgItems.HeaderBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(81)))), ((int)(((byte)(153)))));
            this.dgItems.HeaderFont = new System.Drawing.Font("Tahoma", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dgItems.HeaderForeColor = System.Drawing.Color.White;
            this.dgItems.Location = new System.Drawing.Point(0, 67);
            this.dgItems.Name = "dgItems";
            this.dgItems.PreferredRowHeight = 50;
            this.dgItems.ReadOnly = true;
            this.dgItems.RowHeadersVisible = false;
            this.dgItems.RowHeaderWidth = 7;
            this.dgItems.SelectionBackColor = System.Drawing.Color.RoyalBlue;
            this.dgItems.SelectionForeColor = System.Drawing.Color.White;
            this.dgItems.Size = new System.Drawing.Size(1022, 699);
            this.dgItems.TabIndex = 5;
            this.dgItems.TableStyles.AddRange(new System.Windows.Forms.DataGridTableStyle[] {
            this.dgStyle});
            this.dgItems.TabStop = false;
            this.dgItems.MouseDown += new System.Windows.Forms.MouseEventHandler(this.dgItems_MouseDown);
            // 
            // dgStyle
            // 
            this.dgStyle.AlternatingBackColor = System.Drawing.Color.White;
            this.dgStyle.BackColor = System.Drawing.Color.White;
            this.dgStyle.DataGrid = this.dgItems;
            this.dgStyle.GridColumnStyles.AddRange(new System.Windows.Forms.DataGridColumnStyle[] {
            this.TransactionItemsID,
            this.ProductID,
            this.ProductCode,
            this.BarCode,
            this.ProductDesc,
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
            this.TransactionItemStatus,
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
            this.SupplierID,
            this.SupplierCode,
            this.SupplierName,
            this.ItemRemarks});
            this.dgStyle.HeaderBackColor = System.Drawing.Color.DarkOrange;
            this.dgStyle.HeaderFont = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dgStyle.HeaderForeColor = System.Drawing.Color.White;
            this.dgStyle.MappingName = "tblproducts";
            this.dgStyle.PreferredColumnWidth = 180;
            this.dgStyle.PreferredRowHeight = 40;
            this.dgStyle.ReadOnly = true;
            this.dgStyle.RowHeadersVisible = false;
            this.dgStyle.RowHeaderWidth = 7;
            this.dgStyle.SelectionBackColor = System.Drawing.Color.Green;
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
            // ProductID
            // 
            this.ProductID.Format = "";
            this.ProductID.FormatInfo = null;
            this.ProductID.HeaderText = "ID";
            this.ProductID.MappingName = "ProductID";
            this.ProductID.NullText = "";
            this.ProductID.ReadOnly = true;
            this.ProductID.Width = 0;
            // 
            // ProductCode
            // 
            this.ProductCode.Format = "";
            this.ProductCode.FormatInfo = null;
            this.ProductCode.HeaderText = "Product Code";
            this.ProductCode.MappingName = "ProductCode";
            this.ProductCode.NullText = "";
            this.ProductCode.ReadOnly = true;
            this.ProductCode.Width = 180;
            // 
            // BarCode
            // 
            this.BarCode.Format = "";
            this.BarCode.FormatInfo = null;
            this.BarCode.HeaderText = "Bar Code";
            this.BarCode.MappingName = "BarCode";
            this.BarCode.NullText = "";
            this.BarCode.ReadOnly = true;
            this.BarCode.Width = 180;
            // 
            // ProductDesc
            // 
            this.ProductDesc.Format = "";
            this.ProductDesc.FormatInfo = null;
            this.ProductDesc.HeaderText = "Description";
            this.ProductDesc.MappingName = "ProductDesc";
            this.ProductDesc.NullText = "";
            this.ProductDesc.ReadOnly = true;
            this.ProductDesc.Width = 276;
            // 
            // ProductUnitID
            // 
            this.ProductUnitID.Format = "";
            this.ProductUnitID.FormatInfo = null;
            this.ProductUnitID.MappingName = "ProductUnitID";
            this.ProductUnitID.NullText = "0";
            this.ProductUnitID.ReadOnly = true;
            this.ProductUnitID.Width = 0;
            // 
            // ProductUnitCode
            // 
            this.ProductUnitCode.Format = "";
            this.ProductUnitCode.FormatInfo = null;
            this.ProductUnitCode.MappingName = "ProductUnitCode";
            this.ProductUnitCode.NullText = "0";
            this.ProductUnitCode.ReadOnly = true;
            this.ProductUnitCode.Width = 0;
            // 
            // Quantity
            // 
            this.Quantity.Format = "";
            this.Quantity.FormatInfo = null;
            this.Quantity.MappingName = "Quantity";
            this.Quantity.NullText = "0";
            this.Quantity.ReadOnly = true;
            this.Quantity.Width = 0;
            // 
            // Price
            // 
            this.Price.Format = "";
            this.Price.FormatInfo = null;
            this.Price.MappingName = "Price";
            this.Price.NullText = "0";
            this.Price.ReadOnly = true;
            this.Price.Width = 0;
            // 
            // Discount
            // 
            this.Discount.Format = "";
            this.Discount.FormatInfo = null;
            this.Discount.MappingName = "Discount";
            this.Discount.NullText = "0";
            this.Discount.ReadOnly = true;
            this.Discount.Width = 0;
            // 
            // ItemDiscount
            // 
            this.ItemDiscount.Format = "";
            this.ItemDiscount.FormatInfo = null;
            this.ItemDiscount.MappingName = "ItemDiscount";
            this.ItemDiscount.NullText = "0";
            this.ItemDiscount.ReadOnly = true;
            this.ItemDiscount.Width = 0;
            // 
            // ItemDiscountType
            // 
            this.ItemDiscountType.Format = "";
            this.ItemDiscountType.FormatInfo = null;
            this.ItemDiscountType.MappingName = "ItemDiscountType";
            this.ItemDiscountType.NullText = "0";
            this.ItemDiscountType.ReadOnly = true;
            this.ItemDiscountType.Width = 0;
            // 
            // Amount
            // 
            this.Amount.Format = "";
            this.Amount.FormatInfo = null;
            this.Amount.MappingName = "Amount";
            this.Amount.NullText = "0";
            this.Amount.ReadOnly = true;
            this.Amount.Width = 0;
            // 
            // VAT
            // 
            this.VAT.Format = "";
            this.VAT.FormatInfo = null;
            this.VAT.MappingName = "VAT";
            this.VAT.NullText = "0";
            this.VAT.ReadOnly = true;
            this.VAT.Width = 0;
            // 
            // EVAT
            // 
            this.EVAT.Format = "";
            this.EVAT.FormatInfo = null;
            this.EVAT.MappingName = "EVAT";
            this.EVAT.NullText = "0";
            this.EVAT.ReadOnly = true;
            this.EVAT.Width = 0;
            // 
            // LocalTax
            // 
            this.LocalTax.Format = "";
            this.LocalTax.FormatInfo = null;
            this.LocalTax.MappingName = "LocalTax";
            this.LocalTax.NullText = "0";
            this.LocalTax.ReadOnly = true;
            this.LocalTax.Width = 0;
            // 
            // VariationsMatrixID
            // 
            this.VariationsMatrixID.Format = "";
            this.VariationsMatrixID.FormatInfo = null;
            this.VariationsMatrixID.MappingName = "VariationsMatrixID";
            this.VariationsMatrixID.NullText = "0";
            this.VariationsMatrixID.ReadOnly = true;
            this.VariationsMatrixID.Width = 0;
            // 
            // MatrixDescription
            // 
            this.MatrixDescription.Format = "";
            this.MatrixDescription.FormatInfo = null;
            this.MatrixDescription.MappingName = "MatrixDescription";
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
            this.ProductSubGroup.NullText = "0";
            this.ProductSubGroup.ReadOnly = true;
            this.ProductSubGroup.Width = 0;
            // 
            // TransactionItemStatus
            // 
            this.TransactionItemStatus.Format = "";
            this.TransactionItemStatus.FormatInfo = null;
            this.TransactionItemStatus.MappingName = "TransactionItemStatus";
            this.TransactionItemStatus.NullText = "0";
            this.TransactionItemStatus.ReadOnly = true;
            this.TransactionItemStatus.Width = 0;
            // 
            // DiscountCode
            // 
            this.DiscountCode.Format = "";
            this.DiscountCode.FormatInfo = null;
            this.DiscountCode.MappingName = "DiscountCode";
            this.DiscountCode.NullText = "0";
            this.DiscountCode.ReadOnly = true;
            this.DiscountCode.Width = 0;
            // 
            // DiscountRemarks
            // 
            this.DiscountRemarks.Format = "";
            this.DiscountRemarks.FormatInfo = null;
            this.DiscountRemarks.MappingName = "DiscountRemarks";
            this.DiscountRemarks.ReadOnly = true;
            this.DiscountRemarks.Width = 0;
            // 
            // ProductPackageID
            // 
            this.ProductPackageID.Format = "";
            this.ProductPackageID.FormatInfo = null;
            this.ProductPackageID.MappingName = "ProductPackageID";
            this.ProductPackageID.NullText = "0";
            this.ProductPackageID.ReadOnly = true;
            this.ProductPackageID.Width = 0;
            // 
            // MatrixPackageID
            // 
            this.MatrixPackageID.Format = "";
            this.MatrixPackageID.FormatInfo = null;
            this.MatrixPackageID.MappingName = "MatrixPackageID";
            this.MatrixPackageID.NullText = "0";
            this.MatrixPackageID.ReadOnly = true;
            this.MatrixPackageID.Width = 0;
            // 
            // PackageQuantity
            // 
            this.PackageQuantity.Format = "";
            this.PackageQuantity.FormatInfo = null;
            this.PackageQuantity.MappingName = "PackageQuantity";
            this.PackageQuantity.NullText = "0";
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
            this.PromoType.NullText = "0";
            this.PromoType.ReadOnly = true;
            this.PromoType.Width = 0;
            // 
            // PromoApplied
            // 
            this.PromoApplied.Format = "";
            this.PromoApplied.FormatInfo = null;
            this.PromoApplied.MappingName = "PromoApplied";
            this.PromoApplied.NullText = "0";
            this.PromoApplied.ReadOnly = true;
            this.PromoApplied.Width = 0;
            // 
            // PurchasePrice
            // 
            this.PurchasePrice.Format = "";
            this.PurchasePrice.FormatInfo = null;
            this.PurchasePrice.MappingName = "PurchasePrice";
            this.PurchasePrice.NullText = "0";
            this.PurchasePrice.ReadOnly = true;
            this.PurchasePrice.Width = 0;
            // 
            // PurchaseAmount
            // 
            this.PurchaseAmount.Format = "";
            this.PurchaseAmount.FormatInfo = null;
            this.PurchaseAmount.MappingName = "PurchaseAmount";
            this.PurchaseAmount.NullText = "0";
            this.PurchaseAmount.ReadOnly = true;
            this.PurchaseAmount.Width = 0;
            // 
            // SupplierID
            // 
            this.SupplierID.Format = "";
            this.SupplierID.FormatInfo = null;
            this.SupplierID.MappingName = "SupplierID";
            this.SupplierID.NullText = "0";
            this.SupplierID.ReadOnly = true;
            this.SupplierID.Width = 0;
            // 
            // SupplierCode
            // 
            this.SupplierCode.Format = "";
            this.SupplierCode.FormatInfo = null;
            this.SupplierCode.MappingName = "SupplierCode";
            this.SupplierCode.ReadOnly = true;
            this.SupplierCode.Width = 0;
            // 
            // SupplierName
            // 
            this.SupplierName.Format = "";
            this.SupplierName.FormatInfo = null;
            this.SupplierName.MappingName = "SupplierName";
            this.SupplierName.ReadOnly = true;
            this.SupplierName.Width = 0;
            // 
            // ItemRemarks
            // 
            this.ItemRemarks.Format = "";
            this.ItemRemarks.FormatInfo = null;
            this.ItemRemarks.HeaderText = "Remarks";
            this.ItemRemarks.MappingName = "ItemRemarks";
            this.ItemRemarks.ReadOnly = true;
            this.ItemRemarks.Width = 0;
            // 
            // imgIcon
            // 
            this.imgIcon.BackColor = System.Drawing.Color.Blue;
            this.imgIcon.Location = new System.Drawing.Point(9, 5);
            this.imgIcon.Name = "imgIcon";
            this.imgIcon.Size = new System.Drawing.Size(49, 49);
            this.imgIcon.TabIndex = 50;
            this.imgIcon.TabStop = false;
            this.imgIcon.Click += new System.EventHandler(this.imgIcon_Click);
            // 
            // TransactionReturnItemSelectWnd
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 14);
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1022, 766);
            this.ControlBox = false;
            this.Controls.Add(this.dgItems);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtSearch);
            this.Controls.Add(this.imgIcon);
            this.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.Name = "TransactionReturnItemSelectWnd";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Load += new System.EventHandler(this.TransactionReturnItemSelectWnd_Load);
            this.Resize += new System.EventHandler(this.TransactionReturnItemSelectWnd_Resize);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TransactionReturnItemSelectWnd_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.dgItems)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.imgIcon)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

		}
		#endregion

        #region Windows Form Methods

        private void TransactionReturnItemSelectWnd_Load(object sender, System.EventArgs e)
        {
            LoadOptions();
            LoadItemData();
        }

		private void TransactionReturnItemSelectWnd_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
		{
			System.Data.DataTable dt;
			int index;

			switch (e.KeyData)
			{
				case Keys.Escape:
					dialog = DialogResult.Cancel;
					this.Hide(); 
					break;

				case Keys.Enter:
                    CreateDetails(dgItems.CurrentRowIndex);
					dialog = DialogResult.OK;
					this.Hide();
					break;
				
				case Keys.Up:
					dt = (System.Data.DataTable) dgItems.DataSource;
					if (dgItems.CurrentRowIndex > 0) 
					{
						index = dgItems.CurrentRowIndex;				
						dgItems.CurrentRowIndex -= 1;
						dgItems.Select(dgItems.CurrentRowIndex);
						dgItems.UnSelect(index);
					}
					break;

				case Keys.Down:
					dt = (System.Data.DataTable) dgItems.DataSource;
					if (dgItems.CurrentRowIndex < dt.Rows.Count-1) 
					{
						index = dgItems.CurrentRowIndex;				

						dgItems.CurrentRowIndex += 1;
						dgItems.Select(dgItems.CurrentRowIndex);
						dgItems.UnSelect(index);
					}
					break;
			}
		}

        private void TransactionReturnItemSelectWnd_Resize(object sender, System.EventArgs e)
        {
            dgStyle.GridColumnStyles["ProductCode"].Width = 180;
            dgStyle.GridColumnStyles["BarCode"].Width = 180;
            dgStyle.GridColumnStyles["ProductDesc"].Width = this.Width - 370;
        }

        #endregion

        #region Windows Control Methods

        private void txtSearch_TextChanged(object sender, System.EventArgs e)
		{
			System.Data.DataTable dt = (System.Data.DataTable) dgItems.DataSource;

			for(int iRow = 0;iRow < dt.Rows.Count ;iRow++) 
			{
				try
				{
					if (dgItems[iRow, 2].ToString().Substring(0, txtSearch.Text.Length).ToLower() == txtSearch.Text.ToLower())
					{
						dgItems.UnSelect(dgItems.CurrentRowIndex); 
						dgItems.Select(iRow);
						dgItems.CurrentRowIndex=iRow;
						return;
					}
				}
				catch {}
			}
		}

        private void dgItems_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            DataGrid dgItems = (DataGrid)sender;
            System.Windows.Forms.DataGrid.HitTestInfo hti = dgItems.HitTest(e.X, e.Y);

            switch (hti.Type)
            {
                case System.Windows.Forms.DataGrid.HitTestType.Cell:
                    dgItems.Select(hti.Row);
                    CreateDetails(hti.Row);
                    dialog = DialogResult.OK;
                    this.Hide();
                    break;
            }
        }

        private void imgIcon_Click(object sender, EventArgs e)
        {
            dialog = DialogResult.Cancel;
            this.Hide();
        }

        private void keyboardSearchControl1_UserKeyPressed(object sender, AceSoft.KeyBoardHook.KeyboardEventArgs e)
        {
            txtSearch.Focus();
            SendKeys.Send(e.KeyboardKeyPressed);
        }

        #endregion

        #region Private Methods

        private void CreateDetails(int iRow)
        {
            try
            {
                mDetails = new Data.SalesTransactionItemDetails();

                mDetails.TransactionItemsID = Convert.ToInt64(dgItems[iRow, 0]);
                mDetails.ProductID = Convert.ToInt64(dgItems[iRow, 1]);
                mDetails.ProductCode = dgItems[iRow, 2].ToString();
                mDetails.BarCode = dgItems[iRow, 3].ToString();
                mDetails.Description = dgItems[iRow, 4].ToString();
                mDetails.ProductUnitID = Convert.ToInt32(dgItems[iRow, 5].ToString());
                mDetails.ProductUnitCode = dgItems[iRow, 6].ToString();
                mDetails.Quantity = Convert.ToDecimal(dgItems[iRow, 7].ToString());
                mDetails.Price = Convert.ToDecimal(dgItems[iRow, 8].ToString());
                mDetails.Discount = Convert.ToDecimal(dgItems[iRow, 9].ToString());
                mDetails.ItemDiscount = Convert.ToDecimal(dgItems[iRow, 10].ToString());
                mDetails.ItemDiscountType = (DiscountTypes)Enum.Parse(typeof(DiscountTypes), dgItems[iRow, 11].ToString());
                mDetails.Amount = Convert.ToDecimal(dgItems[iRow, 12].ToString());
                mDetails.VAT = Convert.ToDecimal(dgItems[iRow, 13].ToString());
                mDetails.EVAT = Convert.ToDecimal(dgItems[iRow, 14].ToString());
                mDetails.LocalTax = Convert.ToDecimal(dgItems[iRow, 15].ToString());
                mDetails.VariationsMatrixID = Convert.ToInt64(dgItems[iRow, 16]);
                mDetails.MatrixDescription = dgItems[iRow, 17].ToString();
                mDetails.ProductGroup = dgItems[iRow, 18].ToString();
                mDetails.ProductSubGroup = dgItems[iRow, 19].ToString();
                mDetails.TransactionItemStatus = (TransactionItemStatus)Enum.Parse(typeof(TransactionItemStatus), dgItems[iRow, 20].ToString());
                mDetails.DiscountCode = dgItems[iRow, 21].ToString();
                mDetails.DiscountRemarks = dgItems[iRow, 22].ToString();
                mDetails.ProductPackageID = Convert.ToInt64(dgItems[iRow, 23]);
                mDetails.MatrixPackageID = Convert.ToInt64(dgItems[iRow, 24]);
                mDetails.PackageQuantity = Convert.ToDecimal(dgItems[iRow, 25]);
                mDetails.PromoQuantity = Convert.ToDecimal(dgItems[iRow, 26]);
                mDetails.PromoValue = Convert.ToDecimal(dgItems[iRow, 27]);
                mDetails.PromoInPercent = Convert.ToBoolean(dgItems[iRow, 28].ToString());
                mDetails.PromoType = (PromoTypes)Enum.Parse(typeof(PromoTypes), dgItems[iRow, 29].ToString());
                mDetails.PromoApplied = Convert.ToDecimal(dgItems[iRow, 30]);
                mDetails.PurchasePrice = Convert.ToDecimal(dgItems[iRow, 31]);
                mDetails.PurchaseAmount = Convert.ToDecimal(dgItems[iRow, 32]);
                mDetails.SupplierID = Convert.ToInt64(dgItems[iRow, 33]);
                mDetails.SupplierCode = dgItems[iRow, 34].ToString();
                mDetails.SupplierName = dgItems[iRow, 35].ToString();
                mDetails.ItemRemarks = dgItems[iRow, 36].ToString();
                mDetails.ReturnTransactionItemsID = Convert.ToInt64(dgItems[iRow, 0]);
            }
            catch (Exception ex)
            {
                Event clsEvent = new Event();
                clsEvent.AddEventLn("Application error. TRACE: " + ex.Message);
                clsEvent.AddEventLn("SOURCE: " + ex.Source, false);
                clsEvent.AddEventLn("STACK TRACE: " + ex.StackTrace, false);
                throw ex;
            }
        }

        private void LoadOptions()
        {
            switch (SysConfigDetails.ItemSelectWndColumnType)
            {
                case ItemSelectWndColumnType.PcDesc:
                case ItemSelectWndColumnType.PcDescMtrx:
                case ItemSelectWndColumnType.SgDesc:
                case ItemSelectWndColumnType.SgDescMtrx:
                case ItemSelectWndColumnType.SgPcDesc:
                    dgStyle.GridColumnStyles["ProductCode"].Width = 180;
                    dgStyle.GridColumnStyles["BarCode"].Width = 0;
                    dgStyle.GridColumnStyles["ProductDesc"].Width = (this.Width - 190) / 2;
                    dgStyle.GridColumnStyles["ItemRemarks"].Width = (this.Width - 190) / 2;
                    break;
                default:
                    dgStyle.GridColumnStyles["ProductCode"].Width = 180;
                    dgStyle.GridColumnStyles["BarCode"].Width = 180;
                    dgStyle.GridColumnStyles["ProductDesc"].Width = this.Width - 370;
                    dgStyle.GridColumnStyles["ItemRemarks"].Width = 0;
                    break;
            }
        }

        private void LoadItemData()
        {
            try
            {

                System.Data.DataTable dt = new System.Data.DataTable("tblproducts");

                dt.Columns.Add("TransactionItemsID");
                dt.Columns.Add("ProductID");
                dt.Columns.Add("ProductCode");
                dt.Columns.Add("BarCode");
                dt.Columns.Add("ProductDesc");
                dt.Columns.Add("ProductUnitID");
                dt.Columns.Add("ProductUnitCode");
                dt.Columns.Add("Quantity");
                dt.Columns.Add("Price");
                dt.Columns.Add("Discount");
                dt.Columns.Add("ItemDiscount");
                dt.Columns.Add("ItemDiscountType");
                dt.Columns.Add("Amount");
                dt.Columns.Add("VAT");
                dt.Columns.Add("EVAT");
                dt.Columns.Add("LocalTax");
                dt.Columns.Add("VariationsMatrixID");
                dt.Columns.Add("MatrixDescription");
                dt.Columns.Add("ProductGroup");
                dt.Columns.Add("ProductSubGroup");
                dt.Columns.Add("TransactionItemStatus");
                dt.Columns.Add("DiscountCode");
                dt.Columns.Add("DiscountRemarks");
                dt.Columns.Add("ProductPackageID");
                dt.Columns.Add("MatrixPackageID");
                dt.Columns.Add("PackageQuantity");
                dt.Columns.Add("PromoQuantity");
                dt.Columns.Add("PromoValue");
                dt.Columns.Add("PromoInPercent");
                dt.Columns.Add("PromoType");
                dt.Columns.Add("PromoApplied");
                dt.Columns.Add("PurchasePrice");
                dt.Columns.Add("PurchaseAmount");
                dt.Columns.Add("SupplierID");
                dt.Columns.Add("SupplierCode");
                dt.Columns.Add("SupplierName");
                dt.Columns.Add("ItemRemarks");

                Data.SalesTransactions clsSalesTransactions = new Data.SalesTransactions();
                Data.SalesTransactionDetails det = clsSalesTransactions.Details(mstTransactionNo, TransactionTerminalNo, TerminalDetails.BranchID);
                clsSalesTransactions.CommitAndDispose();

                Data.SalesTransactionItems clsItems = new Data.SalesTransactionItems();
                Data.SalesTransactionItemDetails[] TransactionItems = clsItems.Details(det.TransactionID, det.TransactionDate);
                clsItems.CommitAndDispose();

                foreach (Data.SalesTransactionItemDetails item in TransactionItems)
                {
                    if ((item.TransactionItemStatus == AceSoft.RetailPlus.TransactionItemStatus.Valid ||
                         item.TransactionItemStatus == AceSoft.RetailPlus.TransactionItemStatus.Demo) && item.RefReturnTransactionItemsID == 0)
                    {
                        System.Data.DataRow dr = dt.NewRow();

                        dr["TransactionItemsID"] = item.TransactionItemsID;
                        dr["ProductID"] = item.ProductID;
                        dr["ProductCode"] = item.ProductCode;
                        dr["BarCode"] = item.BarCode;
                        if (item.TransactionItemStatus == AceSoft.RetailPlus.TransactionItemStatus.Demo)
                            dr["ProductDesc"] = "Demo-" + item.Description;
                        else
                            dr["ProductDesc"] = item.Description;

                        dr["ProductUnitID"] = item.ProductUnitID;
                        dr["ProductUnitCode"] = item.ProductUnitCode;
                        dr["Quantity"] = item.Quantity;
                        dr["Price"] = item.Price;
                        dr["Discount"] = item.Discount;
                        dr["ItemDiscount"] = item.ItemDiscount;
                        dr["ItemDiscountType"] = item.ItemDiscountType;
                        dr["Amount"] = item.Amount;
                        dr["VAT"] = item.VAT;
                        dr["EVAT"] = item.EVAT;
                        dr["LocalTax"] = item.LocalTax;
                        dr["VariationsMatrixID"] = item.VariationsMatrixID;
                        dr["MatrixDescription"] = item.MatrixDescription;
                        dr["ProductGroup"] = item.ProductGroup;
                        dr["ProductSubGroup"] = item.ProductSubGroup;
                        dr["TransactionItemStatus"] = item.TransactionItemStatus;
                        dr["DiscountCode"] = item.DiscountCode;
                        dr["DiscountRemarks"] = item.DiscountRemarks;
                        dr["ProductPackageID"] = item.ProductPackageID;
                        dr["MatrixPackageID"] = item.MatrixPackageID;
                        dr["PackageQuantity"] = item.PackageQuantity;
                        dr["PromoQuantity"] = item.PromoQuantity;
                        dr["PromoValue"] = item.PromoValue;
                        dr["PromoInPercent"] = item.PromoInPercent;
                        dr["PromoType"] = item.PromoType;
                        dr["PromoApplied"] = item.PromoApplied;
                        dr["PurchasePrice"] = item.PurchasePrice;
                        dr["PurchaseAmount"] = item.PurchaseAmount;
                        dr["SupplierID"] = item.SupplierID;
                        dr["SupplierCode"] = item.SupplierCode;
                        dr["SupplierName"] = item.SupplierName;
                        dr["ItemRemarks"] = item.ItemRemarks;

                        dt.Rows.Add(dr);
                    }
                }
                this.dgStyle.MappingName = dt.TableName;
                dgItems.DataSource = dt;

                if (dgItems.VisibleRowCount > 0)
                    dgItems.Select(0);
                dgItems.CurrentRowIndex = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "RetailPlus", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #endregion

    }
}

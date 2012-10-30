namespace AceSoft.RetailPlus.Client.UI
{
    partial class ItemReleaseWnd
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.grpItems = new System.Windows.Forms.GroupBox();
            this.dgItems = new System.Windows.Forms.DataGrid();
            this.dgStyle = new System.Windows.Forms.DataGridTableStyle();
            this.TransactionItemsID = new System.Windows.Forms.DataGridTextBoxColumn();
            this.ItemNo = new System.Windows.Forms.DataGridTextBoxColumn();
            this.ProductID = new System.Windows.Forms.DataGridTextBoxColumn();
            this.ProductCode = new System.Windows.Forms.DataGridTextBoxColumn();
            this.BarCode = new System.Windows.Forms.DataGridTextBoxColumn();
            this.Description = new System.Windows.Forms.DataGridTextBoxColumn();
            this.VariationsMatrixID = new System.Windows.Forms.DataGridTextBoxColumn();
            this.MatrixDescription = new System.Windows.Forms.DataGridTextBoxColumn();
            this.Quantity = new System.Windows.Forms.DataGridTextBoxColumn();
            this.ProductUnitID = new System.Windows.Forms.DataGridTextBoxColumn();
            this.ProductUnitCode = new System.Windows.Forms.DataGridTextBoxColumn();
            this.Price = new System.Windows.Forms.DataGridTextBoxColumn();
            this.Discount = new System.Windows.Forms.DataGridTextBoxColumn();
            this.ItemDiscount = new System.Windows.Forms.DataGridTextBoxColumn();
            this.ItemDiscountType = new System.Windows.Forms.DataGridTextBoxColumn();
            this.Amount = new System.Windows.Forms.DataGridTextBoxColumn();
            this.VAT = new System.Windows.Forms.DataGridTextBoxColumn();
            this.EVAT = new System.Windows.Forms.DataGridTextBoxColumn();
            this.LocalTax = new System.Windows.Forms.DataGridTextBoxColumn();
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
            this.ScannedQty = new System.Windows.Forms.DataGridTextBoxColumn();
            this.ScannedAmt = new System.Windows.Forms.DataGridTextBoxColumn();
            this.lblTransactionNo = new System.Windows.Forms.Label();
            this.lblHeader = new System.Windows.Forms.Label();
            this.txtScan = new System.Windows.Forms.TextBox();
            this.lblCommand = new System.Windows.Forms.Label();
            this.lblF51 = new System.Windows.Forms.Label();
            this.lblF5 = new System.Windows.Forms.Label();
            this.cmdCancel = new System.Windows.Forms.Button();
            this.imgIcon = new System.Windows.Forms.PictureBox();
            this.lblTotal = new System.Windows.Forms.Label();
            this.lblTotalAmount = new System.Windows.Forms.Label();
            this.lblTotalScannedAmt = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.grpItems.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgItems)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.imgIcon)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // grpItems
            // 
            this.grpItems.BackColor = System.Drawing.Color.White;
            this.grpItems.Controls.Add(this.dgItems);
            this.grpItems.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grpItems.ForeColor = System.Drawing.Color.Blue;
            this.grpItems.Location = new System.Drawing.Point(11, 58);
            this.grpItems.Name = "grpItems";
            this.grpItems.Size = new System.Drawing.Size(781, 471);
            this.grpItems.TabIndex = 106;
            this.grpItems.TabStop = false;
            // 
            // dgItems
            // 
            this.dgItems.AlternatingBackColor = System.Drawing.Color.White;
            this.dgItems.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
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
            this.dgItems.Location = new System.Drawing.Point(6, 20);
            this.dgItems.Name = "dgItems";
            this.dgItems.ParentRowsBackColor = System.Drawing.Color.Blue;
            this.dgItems.PreferredRowHeight = 100;
            this.dgItems.ReadOnly = true;
            this.dgItems.RowHeadersVisible = false;
            this.dgItems.RowHeaderWidth = 40;
            this.dgItems.SelectionBackColor = System.Drawing.Color.RoyalBlue;
            this.dgItems.SelectionForeColor = System.Drawing.Color.White;
            this.dgItems.Size = new System.Drawing.Size(763, 445);
            this.dgItems.TabIndex = 49;
            this.dgItems.TableStyles.AddRange(new System.Windows.Forms.DataGridTableStyle[] {
            this.dgStyle});
            this.dgItems.TabStop = false;
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
            this.VariationsMatrixID,
            this.MatrixDescription,
            this.Quantity,
            this.ProductUnitID,
            this.ProductUnitCode,
            this.Price,
            this.Discount,
            this.ItemDiscount,
            this.ItemDiscountType,
            this.Amount,
            this.VAT,
            this.EVAT,
            this.LocalTax,
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
            this.Commision,
            this.ScannedQty,
            this.ScannedAmt});
            this.dgStyle.GridLineColor = System.Drawing.Color.Blue;
            this.dgStyle.GridLineStyle = System.Windows.Forms.DataGridLineStyle.None;
            this.dgStyle.HeaderBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(81)))), ((int)(((byte)(153)))));
            this.dgStyle.HeaderFont = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dgStyle.HeaderForeColor = System.Drawing.Color.White;
            this.dgStyle.MappingName = "tblProducts";
            this.dgStyle.PreferredColumnWidth = 0;
            this.dgStyle.PreferredRowHeight = 30;
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
            this.MatrixDescription.HeaderText = "Matrix";
            this.MatrixDescription.MappingName = "MatrixDescription";
            this.MatrixDescription.NullText = "";
            this.MatrixDescription.ReadOnly = true;
            this.MatrixDescription.Width = 0;
            // 
            // Quantity
            // 
            this.Quantity.Alignment = System.Windows.Forms.HorizontalAlignment.Right;
            this.Quantity.Format = "###,##0.#0";
            this.Quantity.FormatInfo = null;
            this.Quantity.HeaderText = "Quantity ";
            this.Quantity.MappingName = "Quantity";
            this.Quantity.NullText = "";
            this.Quantity.ReadOnly = true;
            this.Quantity.Width = 0;
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
            // Price
            // 
            this.Price.Alignment = System.Windows.Forms.HorizontalAlignment.Right;
            this.Price.Format = "###,##0.#0";
            this.Price.FormatInfo = null;
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
            this.Amount.Format = "###,##0.#0";
            this.Amount.FormatInfo = null;
            this.Amount.HeaderText = "Amount";
            this.Amount.MappingName = "Amount";
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
            // ScannedQty
            // 
            this.ScannedQty.Alignment = System.Windows.Forms.HorizontalAlignment.Right;
            this.ScannedQty.Format = "###,##0.#0";
            this.ScannedQty.FormatInfo = null;
            this.ScannedQty.HeaderText = "Scanned Qty";
            this.ScannedQty.MappingName = "ScannedQty";
            this.ScannedQty.ReadOnly = true;
            this.ScannedQty.Width = 0;
            // 
            // ScannedAmt
            // 
            this.ScannedAmt.Alignment = System.Windows.Forms.HorizontalAlignment.Right;
            this.ScannedAmt.Format = "###,##0.#0";
            this.ScannedAmt.FormatInfo = null;
            this.ScannedAmt.HeaderText = "Scanned Amt";
            this.ScannedAmt.MappingName = "ScannedAmt";
            this.ScannedAmt.ReadOnly = true;
            this.ScannedAmt.Width = 0;
            // 
            // lblTransactionNo
            // 
            this.lblTransactionNo.AutoSize = true;
            this.lblTransactionNo.BackColor = System.Drawing.Color.Transparent;
            this.lblTransactionNo.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTransactionNo.ForeColor = System.Drawing.Color.White;
            this.lblTransactionNo.Location = new System.Drawing.Point(141, 26);
            this.lblTransactionNo.Name = "lblTransactionNo";
            this.lblTransactionNo.Size = new System.Drawing.Size(101, 13);
            this.lblTransactionNo.TabIndex = 15;
            this.lblTransactionNo.Text = "lblTransactionNo";
            // 
            // lblHeader
            // 
            this.lblHeader.AutoSize = true;
            this.lblHeader.BackColor = System.Drawing.Color.Transparent;
            this.lblHeader.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblHeader.ForeColor = System.Drawing.Color.White;
            this.lblHeader.Location = new System.Drawing.Point(78, 26);
            this.lblHeader.Name = "lblHeader";
            this.lblHeader.Size = new System.Drawing.Size(62, 13);
            this.lblHeader.TabIndex = 105;
            this.lblHeader.Text = "Releasing";
            // 
            // txtScan
            // 
            this.txtScan.BackColor = System.Drawing.Color.White;
            this.txtScan.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtScan.Font = new System.Drawing.Font("Tahoma", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtScan.ForeColor = System.Drawing.Color.Black;
            this.txtScan.Location = new System.Drawing.Point(215, 572);
            this.txtScan.Name = "txtScan";
            this.txtScan.Size = new System.Drawing.Size(408, 30);
            this.txtScan.TabIndex = 107;
            // 
            // lblCommand
            // 
            this.lblCommand.AutoSize = true;
            this.lblCommand.Font = new System.Drawing.Font("Tahoma", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCommand.ForeColor = System.Drawing.Color.MediumBlue;
            this.lblCommand.Location = new System.Drawing.Point(9, 576);
            this.lblCommand.Name = "lblCommand";
            this.lblCommand.Size = new System.Drawing.Size(206, 23);
            this.lblCommand.TabIndex = 109;
            this.lblCommand.Text = "Scan transaction no:";
            // 
            // lblF51
            // 
            this.lblF51.AutoSize = true;
            this.lblF51.BackColor = System.Drawing.Color.Transparent;
            this.lblF51.ForeColor = System.Drawing.Color.LightSlateGray;
            this.lblF51.Location = new System.Drawing.Point(530, 36);
            this.lblF51.Name = "lblF51";
            this.lblF51.Size = new System.Drawing.Size(262, 13);
            this.lblF51.TabIndex = 110;
            this.lblF51.Text = "Click or Press <F5> to Change transaction to release";
            this.lblF51.Click += new System.EventHandler(this.lblF51_Click);
            // 
            // lblF5
            // 
            this.lblF5.AutoSize = true;
            this.lblF5.BackColor = System.Drawing.Color.Transparent;
            this.lblF5.ForeColor = System.Drawing.Color.Red;
            this.lblF5.Location = new System.Drawing.Point(604, 35);
            this.lblF5.Name = "lblF5";
            this.lblF5.Size = new System.Drawing.Size(19, 13);
            this.lblF5.TabIndex = 111;
            this.lblF5.Text = "F5";
            this.lblF5.Click += new System.EventHandler(this.lblF5_Click);
            // 
            // cmdCancel
            // 
            this.cmdCancel.AutoSize = true;
            this.cmdCancel.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.cmdCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmdCancel.Font = new System.Drawing.Font("Times New Roman", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdCancel.ForeColor = System.Drawing.Color.White;
            this.cmdCancel.Location = new System.Drawing.Point(714, 557);
            this.cmdCancel.Name = "cmdCancel";
            this.cmdCancel.Size = new System.Drawing.Size(78, 62);
            this.cmdCancel.TabIndex = 108;
            this.cmdCancel.TabStop = false;
            this.cmdCancel.Text = "Close";
            this.cmdCancel.UseVisualStyleBackColor = true;
            this.cmdCancel.Click += new System.EventHandler(this.cmdCancel_Click);
            // 
            // imgIcon
            // 
            this.imgIcon.BackColor = System.Drawing.Color.Blue;
            this.imgIcon.Location = new System.Drawing.Point(11, 9);
            this.imgIcon.Name = "imgIcon";
            this.imgIcon.Size = new System.Drawing.Size(49, 49);
            this.imgIcon.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.imgIcon.TabIndex = 104;
            this.imgIcon.TabStop = false;
            this.imgIcon.Click += new System.EventHandler(this.imgIcon_Click);
            // 
            // lblTotal
            // 
            this.lblTotal.AutoSize = true;
            this.lblTotal.BackColor = System.Drawing.Color.Gainsboro;
            this.lblTotal.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTotal.ForeColor = System.Drawing.Color.MediumBlue;
            this.lblTotal.Location = new System.Drawing.Point(324, 4);
            this.lblTotal.Name = "lblTotal";
            this.lblTotal.Size = new System.Drawing.Size(64, 16);
            this.lblTotal.TabIndex = 112;
            this.lblTotal.Text = "TOTAL....";
            this.lblTotal.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // lblTotalAmount
            // 
            this.lblTotalAmount.AutoSize = true;
            this.lblTotalAmount.BackColor = System.Drawing.Color.Gainsboro;
            this.lblTotalAmount.Font = new System.Drawing.Font("Tahoma", 9.75F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTotalAmount.ForeColor = System.Drawing.Color.MediumBlue;
            this.lblTotalAmount.Location = new System.Drawing.Point(530, 532);
            this.lblTotalAmount.Name = "lblTotalAmount";
            this.lblTotalAmount.Size = new System.Drawing.Size(36, 16);
            this.lblTotalAmount.TabIndex = 113;
            this.lblTotalAmount.Text = "0.00";
            this.lblTotalAmount.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // lblTotalScannedAmt
            // 
            this.lblTotalScannedAmt.AutoSize = true;
            this.lblTotalScannedAmt.BackColor = System.Drawing.Color.Gainsboro;
            this.lblTotalScannedAmt.Font = new System.Drawing.Font("Tahoma", 9.75F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTotalScannedAmt.ForeColor = System.Drawing.Color.MediumBlue;
            this.lblTotalScannedAmt.Location = new System.Drawing.Point(744, 532);
            this.lblTotalScannedAmt.Name = "lblTotalScannedAmt";
            this.lblTotalScannedAmt.Size = new System.Drawing.Size(36, 16);
            this.lblTotalScannedAmt.TabIndex = 114;
            this.lblTotalScannedAmt.Text = "0.00";
            this.lblTotalScannedAmt.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Gainsboro;
            this.panel1.Controls.Add(this.lblTotal);
            this.panel1.Location = new System.Drawing.Point(11, 528);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(781, 24);
            this.panel1.TabIndex = 115;
            // 
            // ItemReleaseWnd
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(802, 620);
            this.ControlBox = false;
            this.Controls.Add(this.lblTotalScannedAmt);
            this.Controls.Add(this.lblTotalAmount);
            this.Controls.Add(this.lblF5);
            this.Controls.Add(this.lblF51);
            this.Controls.Add(this.grpItems);
            this.Controls.Add(this.lblCommand);
            this.Controls.Add(this.lblTransactionNo);
            this.Controls.Add(this.cmdCancel);
            this.Controls.Add(this.lblHeader);
            this.Controls.Add(this.imgIcon);
            this.Controls.Add(this.txtScan);
            this.Controls.Add(this.panel1);
            this.Font = new System.Drawing.Font("Tahoma", 8.25F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ItemReleaseWnd";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Load += new System.EventHandler(this.ItemReleaseWnd_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ItemReleaseWnd_KeyDown);
            this.grpItems.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgItems)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.imgIcon)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox grpItems;
        private System.Windows.Forms.Label lblTransactionNo;
        private System.Windows.Forms.Button cmdCancel;
        private System.Windows.Forms.Label lblHeader;
        private System.Windows.Forms.PictureBox imgIcon;
        private System.Windows.Forms.TextBox txtScan;
        private System.Windows.Forms.Label lblCommand;
        private System.Windows.Forms.DataGrid dgItems;
        private System.Windows.Forms.DataGridTableStyle dgStyle;
        private System.Windows.Forms.DataGridTextBoxColumn Commision;
        private System.Windows.Forms.DataGridTextBoxColumn PercentageCommision;
        private System.Windows.Forms.DataGridTextBoxColumn OrderSlipPrinted;
        private System.Windows.Forms.DataGridTextBoxColumn OrderSlipPrinter;
        private System.Windows.Forms.DataGridTextBoxColumn IncludeInSubtotalDiscount;
        private System.Windows.Forms.DataGridTextBoxColumn PurchaseAmount;
        private System.Windows.Forms.DataGridTextBoxColumn PurchasePrice;
        private System.Windows.Forms.DataGridTextBoxColumn PromoApplied;
        private System.Windows.Forms.DataGridTextBoxColumn PromoType;
        private System.Windows.Forms.DataGridTextBoxColumn PromoInPercent;
        private System.Windows.Forms.DataGridTextBoxColumn PromoValue;
        private System.Windows.Forms.DataGridTextBoxColumn PromoQuantity;
        private System.Windows.Forms.DataGridTextBoxColumn PackageQuantity;
        private System.Windows.Forms.DataGridTextBoxColumn MatrixPackageID;
        private System.Windows.Forms.DataGridTextBoxColumn ProductPackageID;
        private System.Windows.Forms.DataGridTextBoxColumn DiscountRemarks;
        private System.Windows.Forms.DataGridTextBoxColumn DiscountCode;
        private System.Windows.Forms.DataGridTextBoxColumn TransactionItemStat;
        private System.Windows.Forms.DataGridTextBoxColumn ProductSubGroup;
        private System.Windows.Forms.DataGridTextBoxColumn ProductGroup;
        private System.Windows.Forms.DataGridTextBoxColumn LocalTax;
        private System.Windows.Forms.DataGridTextBoxColumn EVAT;
        private System.Windows.Forms.DataGridTextBoxColumn VAT;
        private System.Windows.Forms.DataGridTextBoxColumn Amount;
        private System.Windows.Forms.DataGridTextBoxColumn ItemDiscountType;
        private System.Windows.Forms.DataGridTextBoxColumn ItemDiscount;
        private System.Windows.Forms.DataGridTextBoxColumn Discount;
        private System.Windows.Forms.DataGridTextBoxColumn Price;
        private System.Windows.Forms.DataGridTextBoxColumn Quantity;
        private System.Windows.Forms.DataGridTextBoxColumn MatrixDescription;
        private System.Windows.Forms.DataGridTextBoxColumn VariationsMatrixID;
        private System.Windows.Forms.DataGridTextBoxColumn ProductUnitCode;
        private System.Windows.Forms.DataGridTextBoxColumn ProductUnitID;
        private System.Windows.Forms.DataGridTextBoxColumn Description;
        private System.Windows.Forms.DataGridTextBoxColumn BarCode;
        private System.Windows.Forms.DataGridTextBoxColumn ProductCode;
        private System.Windows.Forms.DataGridTextBoxColumn ProductID;
        private System.Windows.Forms.DataGridTextBoxColumn ItemNo;
        private System.Windows.Forms.DataGridTextBoxColumn TransactionItemsID;
        private System.Windows.Forms.Label lblF51;
        private System.Windows.Forms.Label lblF5;
        private System.Windows.Forms.DataGridTextBoxColumn ScannedQty;
        private System.Windows.Forms.DataGridTextBoxColumn ScannedAmt;
        private System.Windows.Forms.Label lblTotal;
        private System.Windows.Forms.Label lblTotalAmount;
        private System.Windows.Forms.Label lblTotalScannedAmt;
        private System.Windows.Forms.Panel panel1;
    }
}
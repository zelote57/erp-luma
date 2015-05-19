using System;
using System.Drawing;
using System.Collections;
using System.Windows.Forms;
using AceSoft.RetailPlus.Data;

namespace AceSoft.RetailPlus.Client.UI
{
	public class ItemSelectWnd : System.Windows.Forms.Form
	{
		private System.Windows.Forms.Label lblHeader;
		private System.Windows.Forms.DataGrid dgItems;
        private System.Windows.Forms.DataGridTextBoxColumn PackageID;
		private System.Windows.Forms.DataGridTextBoxColumn ProductID;
        private System.Windows.Forms.DataGridTextBoxColumn MatrixID;
		private System.Windows.Forms.DataGridTextBoxColumn ProductCode;
        private System.Windows.Forms.DataGridTextBoxColumn MatrixDescription;
		private System.Windows.Forms.DataGridTextBoxColumn BarCode;
		private System.Windows.Forms.DataGridTextBoxColumn ProductDesc;
		private System.Windows.Forms.DataGridTextBoxColumn ProductGroup;
		private System.Windows.Forms.DataGridTextBoxColumn ProductSubGroup;
		private System.Windows.Forms.DataGridTextBoxColumn ProductUnitID;
		private System.Windows.Forms.DataGridTextBoxColumn ProductUnitName;
		private System.Windows.Forms.DataGridTextBoxColumn Price;
        private System.Windows.Forms.DataGridTextBoxColumn Price1;
        private System.Windows.Forms.DataGridTextBoxColumn Price2;
        private System.Windows.Forms.DataGridTextBoxColumn Price3;
        private System.Windows.Forms.DataGridTextBoxColumn Price4;
        private System.Windows.Forms.DataGridTextBoxColumn Price5;
        private System.Windows.Forms.DataGridTextBoxColumn WSPrice;
		private System.Windows.Forms.DataGridTextBoxColumn VAT;
		private System.Windows.Forms.DataGridTextBoxColumn LocalTax;
        private DataGridQuantityTextBoxColumn Quantity;
        private System.Windows.Forms.DataGridTextBoxColumn MinThreshold;
		private System.Windows.Forms.DataGridTableStyle dgStyle;
		private System.Windows.Forms.TextBox txtSearch;
		private DialogResult dialog;
		private string mstBarCode;
        private string mstSearchCode;
		private bool mboIsPriceInq;
		private bool mboShowItemMoreThanZeroQty;
        private bool mboShowInActiveProducts;
        private SysConfigDetails mclsSysConfigDetails;
        private System.Windows.Forms.PictureBox imgIcon;
        private Timer tmrSearch;
        private Label lblProductCode;
        private Label lblProductQuantity;
        private DataGridView dgvItemsPerBranch;
        private Label lblQuantityPerBranch2;
        private Label lblQuantityPerBranch3;
        private Label lblQuantityPerBranch1;
        private System.ComponentModel.IContainer components;

		public DialogResult Result
		{
			get {	return dialog;	}
		}

		public string SelectedBarCode
		{
			get	{	return mstBarCode;	}
		}

        public string SearchCode
        {
            get { return mstSearchCode; }
            set { mstSearchCode = value; }
        }

		public bool IsPriceInq
		{
			set	{	mboIsPriceInq = value;	}
		}

		public bool ShowItemMoreThanZeroQty
		{
			set	{	mboShowItemMoreThanZeroQty = value;	}
		}

        public SysConfigDetails SysConfigDetails
        {
            set { mclsSysConfigDetails = value; }
        }

        public Data.TerminalDetails TerminalDetails { get; set; }

        public ContactDetails ContactDetails { get; set; }
        
        // Aug 6, 2011  :Lemu
        // Include InActive products during refund
        public bool ShowInActiveProducts
        {
            set { mboShowInActiveProducts = value; }
        }

		#region Constructors and Destructors

		public ItemSelectWnd()
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
            { this.imgIcon.Image = new Bitmap(Application.StartupPath + "/images/ItemSelect.jpg"); }
            catch { }

            if (Common.isTerminalMultiInstanceEnabled())
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

		
		#endregion

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            this.txtSearch = new System.Windows.Forms.TextBox();
            this.lblHeader = new System.Windows.Forms.Label();
            this.dgItems = new System.Windows.Forms.DataGrid();
            this.dgStyle = new System.Windows.Forms.DataGridTableStyle();
            this.PackageID = new System.Windows.Forms.DataGridTextBoxColumn();
            this.ProductID = new System.Windows.Forms.DataGridTextBoxColumn();
            this.MatrixID = new System.Windows.Forms.DataGridTextBoxColumn();
            this.BarCode = new System.Windows.Forms.DataGridTextBoxColumn();
            this.ProductCode = new System.Windows.Forms.DataGridTextBoxColumn();
            this.ProductDesc = new System.Windows.Forms.DataGridTextBoxColumn();
            this.MatrixDescription = new System.Windows.Forms.DataGridTextBoxColumn();
            this.ProductGroup = new System.Windows.Forms.DataGridTextBoxColumn();
            this.ProductSubGroup = new System.Windows.Forms.DataGridTextBoxColumn();
            this.ProductUnitID = new System.Windows.Forms.DataGridTextBoxColumn();
            this.ProductUnitName = new System.Windows.Forms.DataGridTextBoxColumn();
            this.Price = new System.Windows.Forms.DataGridTextBoxColumn();
            this.Price1 = new System.Windows.Forms.DataGridTextBoxColumn();
            this.Price2 = new System.Windows.Forms.DataGridTextBoxColumn();
            this.Price3 = new System.Windows.Forms.DataGridTextBoxColumn();
            this.Price4 = new System.Windows.Forms.DataGridTextBoxColumn();
            this.Price5 = new System.Windows.Forms.DataGridTextBoxColumn();
            this.WSPrice = new System.Windows.Forms.DataGridTextBoxColumn();
            this.VAT = new System.Windows.Forms.DataGridTextBoxColumn();
            this.LocalTax = new System.Windows.Forms.DataGridTextBoxColumn();
            this.MinThreshold = new System.Windows.Forms.DataGridTextBoxColumn();
            this.imgIcon = new System.Windows.Forms.PictureBox();
            this.tmrSearch = new System.Windows.Forms.Timer(this.components);
            this.lblProductCode = new System.Windows.Forms.Label();
            this.lblProductQuantity = new System.Windows.Forms.Label();
            this.dgvItemsPerBranch = new System.Windows.Forms.DataGridView();
            this.lblQuantityPerBranch2 = new System.Windows.Forms.Label();
            this.lblQuantityPerBranch3 = new System.Windows.Forms.Label();
            this.lblQuantityPerBranch1 = new System.Windows.Forms.Label();
            this.Quantity = new AceSoft.RetailPlus.Client.UI.DataGridQuantityTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dgItems)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.imgIcon)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvItemsPerBranch)).BeginInit();
            this.SuspendLayout();
            // 
            // txtSearch
            // 
            this.txtSearch.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtSearch.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtSearch.Location = new System.Drawing.Point(67, 27);
            this.txtSearch.Name = "txtSearch";
            this.txtSearch.Size = new System.Drawing.Size(298, 23);
            this.txtSearch.TabIndex = 0;
            this.txtSearch.TextChanged += new System.EventHandler(this.txtSearch_TextChanged);
            // 
            // lblHeader
            // 
            this.lblHeader.AutoSize = true;
            this.lblHeader.BackColor = System.Drawing.Color.Transparent;
            this.lblHeader.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblHeader.ForeColor = System.Drawing.Color.White;
            this.lblHeader.Location = new System.Drawing.Point(67, 9);
            this.lblHeader.Name = "lblHeader";
            this.lblHeader.Size = new System.Drawing.Size(125, 13);
            this.lblHeader.TabIndex = 4;
            this.lblHeader.Text = "Enter search criteria.";
            // 
            // dgItems
            // 
            this.dgItems.AlternatingBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.dgItems.BackColor = System.Drawing.Color.White;
            this.dgItems.BackgroundColor = System.Drawing.Color.White;
            this.dgItems.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgItems.CaptionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.dgItems.CaptionFont = new System.Drawing.Font("Tahoma", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dgItems.CaptionForeColor = System.Drawing.Color.Blue;
            this.dgItems.CaptionVisible = false;
            this.dgItems.DataMember = "";
            this.dgItems.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.dgItems.FlatMode = true;
            this.dgItems.Font = new System.Drawing.Font("Tahoma", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
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
            this.PackageID,
            this.ProductID,
            this.MatrixID,
            this.BarCode,
            this.ProductCode,
            this.ProductDesc,
            this.MatrixDescription,
            this.ProductGroup,
            this.ProductSubGroup,
            this.ProductUnitID,
            this.ProductUnitName,
            this.Price,
            this.Price1,
            this.Price2,
            this.Price3,
            this.Price4,
            this.Price5,
            this.WSPrice,
            this.VAT,
            this.LocalTax,
            this.Quantity,
            this.MinThreshold});
            this.dgStyle.HeaderBackColor = System.Drawing.Color.DarkOrange;
            this.dgStyle.HeaderFont = new System.Drawing.Font("Tahoma", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dgStyle.HeaderForeColor = System.Drawing.Color.White;
            this.dgStyle.MappingName = "tblProducts";
            this.dgStyle.PreferredColumnWidth = 180;
            this.dgStyle.PreferredRowHeight = 40;
            this.dgStyle.ReadOnly = true;
            this.dgStyle.RowHeadersVisible = false;
            this.dgStyle.RowHeaderWidth = 7;
            this.dgStyle.SelectionBackColor = System.Drawing.Color.Green;
            this.dgStyle.SelectionForeColor = System.Drawing.Color.White;
            // 
            // PackageID
            // 
            this.PackageID.Format = "";
            this.PackageID.FormatInfo = null;
            this.PackageID.HeaderText = "PackageID";
            this.PackageID.MappingName = "PackageID";
            this.PackageID.NullText = "";
            this.PackageID.ReadOnly = true;
            this.PackageID.Width = 0;
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
            // MatrixID
            // 
            this.MatrixID.Format = "";
            this.MatrixID.FormatInfo = null;
            this.MatrixID.HeaderText = "MatrixID";
            this.MatrixID.MappingName = "MatrixID";
            this.MatrixID.NullText = "";
            this.MatrixID.ReadOnly = true;
            this.MatrixID.Width = 0;
            // 
            // BarCode
            // 
            this.BarCode.Format = "";
            this.BarCode.FormatInfo = null;
            this.BarCode.HeaderText = "BarCode";
            this.BarCode.MappingName = "BarCode";
            this.BarCode.NullText = "";
            this.BarCode.ReadOnly = true;
            this.BarCode.Width = 0;
            // 
            // ProductCode
            // 
            this.ProductCode.Format = "";
            this.ProductCode.FormatInfo = null;
            this.ProductCode.HeaderText = "Product Code";
            this.ProductCode.MappingName = "ProductCode";
            this.ProductCode.NullText = "";
            this.ProductCode.ReadOnly = true;
            this.ProductCode.Width = 0;
            // 
            // ProductDesc
            // 
            this.ProductDesc.Format = "";
            this.ProductDesc.FormatInfo = null;
            this.ProductDesc.HeaderText = "Description";
            this.ProductDesc.MappingName = "ProductDesc";
            this.ProductDesc.NullText = "";
            this.ProductDesc.ReadOnly = true;
            this.ProductDesc.Width = 0;
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
            this.ProductGroup.HeaderText = "Group";
            this.ProductGroup.MappingName = "ProductGroupName";
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
            // ProductUnitID
            // 
            this.ProductUnitID.Format = "";
            this.ProductUnitID.FormatInfo = null;
            this.ProductUnitID.MappingName = "ProductUnitID";
            this.ProductUnitID.NullText = "0";
            this.ProductUnitID.ReadOnly = true;
            this.ProductUnitID.Width = 0;
            // 
            // ProductUnitName
            // 
            this.ProductUnitName.Format = "";
            this.ProductUnitName.FormatInfo = null;
            this.ProductUnitName.MappingName = "ProductUnitName";
            this.ProductUnitName.NullText = "0";
            this.ProductUnitName.ReadOnly = true;
            this.ProductUnitName.Width = 0;
            // 
            // Price
            // 
            this.Price.Format = "";
            this.Price.FormatInfo = null;
            this.Price.HeaderText = "Price";
            this.Price.MappingName = "Price";
            this.Price.NullText = "0";
            this.Price.ReadOnly = true;
            this.Price.Width = 0;
            // 
            // Price1
            // 
            this.Price1.Format = "";
            this.Price1.FormatInfo = null;
            this.Price1.HeaderText = "Price1";
            this.Price1.MappingName = "Price1";
            this.Price1.NullText = "0";
            this.Price1.ReadOnly = true;
            this.Price1.Width = 0;
            // 
            // Price2
            // 
            this.Price2.Format = "";
            this.Price2.FormatInfo = null;
            this.Price2.HeaderText = "Price2";
            this.Price2.MappingName = "Price2";
            this.Price2.NullText = "0";
            this.Price2.ReadOnly = true;
            this.Price2.Width = 0;
            // 
            // Price3
            // 
            this.Price3.Format = "";
            this.Price3.FormatInfo = null;
            this.Price3.HeaderText = "Price3";
            this.Price3.MappingName = "Price3";
            this.Price3.NullText = "0";
            this.Price3.ReadOnly = true;
            this.Price3.Width = 0;
            // 
            // Price4
            // 
            this.Price4.Format = "";
            this.Price4.FormatInfo = null;
            this.Price4.HeaderText = "Price4";
            this.Price4.MappingName = "Price4";
            this.Price4.NullText = "0";
            this.Price4.ReadOnly = true;
            this.Price4.Width = 0;
            // 
            // Price5
            // 
            this.Price5.Format = "";
            this.Price5.FormatInfo = null;
            this.Price5.HeaderText = "Price5";
            this.Price5.MappingName = "Price5";
            this.Price5.NullText = "0";
            this.Price5.ReadOnly = true;
            this.Price5.Width = 0;
            // 
            // WSPrice
            // 
            this.WSPrice.Format = "";
            this.WSPrice.FormatInfo = null;
            this.WSPrice.HeaderText = "WSPrice";
            this.WSPrice.MappingName = "WSPrice";
            this.WSPrice.NullText = "0";
            this.WSPrice.ReadOnly = true;
            this.WSPrice.Width = 0;
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
            // LocalTax
            // 
            this.LocalTax.Format = "";
            this.LocalTax.FormatInfo = null;
            this.LocalTax.MappingName = "LocalTax";
            this.LocalTax.NullText = "0";
            this.LocalTax.ReadOnly = true;
            this.LocalTax.Width = 0;
            // 
            // MinThreshold
            // 
            this.MinThreshold.Format = "";
            this.MinThreshold.FormatInfo = null;
            this.MinThreshold.MappingName = "MinThreshold";
            this.MinThreshold.NullText = "0";
            this.MinThreshold.ReadOnly = true;
            this.MinThreshold.Width = 0;
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
            // tmrSearch
            // 
            this.tmrSearch.Interval = 300;
            this.tmrSearch.Tick += new System.EventHandler(this.tmrSearch_Tick);
            // 
            // lblProductCode
            // 
            this.lblProductCode.BackColor = System.Drawing.Color.Transparent;
            this.lblProductCode.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblProductCode.ForeColor = System.Drawing.Color.LightSlateGray;
            this.lblProductCode.Location = new System.Drawing.Point(371, 34);
            this.lblProductCode.Name = "lblProductCode";
            this.lblProductCode.Size = new System.Drawing.Size(486, 23);
            this.lblProductCode.TabIndex = 80;
            this.lblProductCode.Text = "Total Quantity:";
            this.lblProductCode.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // lblProductQuantity
            // 
            this.lblProductQuantity.AutoSize = true;
            this.lblProductQuantity.BackColor = System.Drawing.Color.Transparent;
            this.lblProductQuantity.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblProductQuantity.ForeColor = System.Drawing.Color.Red;
            this.lblProductQuantity.Location = new System.Drawing.Point(854, 31);
            this.lblProductQuantity.Name = "lblProductQuantity";
            this.lblProductQuantity.Size = new System.Drawing.Size(44, 19);
            this.lblProductQuantity.TabIndex = 81;
            this.lblProductQuantity.Text = "0.00";
            // 
            // dgvItemsPerBranch
            // 
            this.dgvItemsPerBranch.AllowUserToAddRows = false;
            this.dgvItemsPerBranch.AllowUserToDeleteRows = false;
            this.dgvItemsPerBranch.AllowUserToResizeColumns = false;
            this.dgvItemsPerBranch.AllowUserToResizeRows = false;
            this.dgvItemsPerBranch.BackgroundColor = System.Drawing.Color.White;
            this.dgvItemsPerBranch.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvItemsPerBranch.CausesValidation = false;
            this.dgvItemsPerBranch.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None;
            this.dgvItemsPerBranch.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(81)))), ((int)(((byte)(153)))));
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(81)))), ((int)(((byte)(153)))));
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvItemsPerBranch.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvItemsPerBranch.ColumnHeadersHeight = 24;
            this.dgvItemsPerBranch.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dgvItemsPerBranch.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.dgvItemsPerBranch.GridColor = System.Drawing.Color.White;
            this.dgvItemsPerBranch.Location = new System.Drawing.Point(469, 60);
            this.dgvItemsPerBranch.Name = "dgvItemsPerBranch";
            this.dgvItemsPerBranch.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.RoyalBlue;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvItemsPerBranch.RowHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dgvItemsPerBranch.RowHeadersVisible = false;
            this.dgvItemsPerBranch.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.RoyalBlue;
            this.dgvItemsPerBranch.RowsDefaultCellStyle = dataGridViewCellStyle3;
            this.dgvItemsPerBranch.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvItemsPerBranch.Size = new System.Drawing.Size(335, 225);
            this.dgvItemsPerBranch.StandardTab = true;
            this.dgvItemsPerBranch.TabIndex = 82;
            this.dgvItemsPerBranch.Visible = false;
            // 
            // lblQuantityPerBranch2
            // 
            this.lblQuantityPerBranch2.AutoSize = true;
            this.lblQuantityPerBranch2.BackColor = System.Drawing.Color.Transparent;
            this.lblQuantityPerBranch2.ForeColor = System.Drawing.Color.Red;
            this.lblQuantityPerBranch2.Location = new System.Drawing.Point(782, 9);
            this.lblQuantityPerBranch2.Name = "lblQuantityPerBranch2";
            this.lblQuantityPerBranch2.Size = new System.Drawing.Size(27, 13);
            this.lblQuantityPerBranch2.TabIndex = 115;
            this.lblQuantityPerBranch2.Text = "[F5]";
            // 
            // lblQuantityPerBranch3
            // 
            this.lblQuantityPerBranch3.AutoSize = true;
            this.lblQuantityPerBranch3.BackColor = System.Drawing.Color.Transparent;
            this.lblQuantityPerBranch3.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.lblQuantityPerBranch3.Location = new System.Drawing.Point(807, 9);
            this.lblQuantityPerBranch3.Name = "lblQuantityPerBranch3";
            this.lblQuantityPerBranch3.Size = new System.Drawing.Size(146, 13);
            this.lblQuantityPerBranch3.TabIndex = 114;
            this.lblQuantityPerBranch3.Text = " to show quantity per branch";
            // 
            // lblQuantityPerBranch1
            // 
            this.lblQuantityPerBranch1.AutoSize = true;
            this.lblQuantityPerBranch1.BackColor = System.Drawing.Color.Transparent;
            this.lblQuantityPerBranch1.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.lblQuantityPerBranch1.Location = new System.Drawing.Point(751, 9);
            this.lblQuantityPerBranch1.Name = "lblQuantityPerBranch1";
            this.lblQuantityPerBranch1.Size = new System.Drawing.Size(33, 13);
            this.lblQuantityPerBranch1.TabIndex = 113;
            this.lblQuantityPerBranch1.Text = "Press";
            // 
            // Quantity
            // 
            this.Quantity.Alignment = System.Windows.Forms.HorizontalAlignment.Right;
            this.Quantity.Format = "";
            this.Quantity.FormatInfo = null;
            this.Quantity.HeaderText = "Qty";
            this.Quantity.MappingName = "Quantity";
            this.Quantity.NullText = "0";
            this.Quantity.ReadOnly = true;
            this.Quantity.Width = 0;
            // 
            // ItemSelectWnd
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 14);
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1022, 766);
            this.ControlBox = false;
            this.Controls.Add(this.lblQuantityPerBranch2);
            this.Controls.Add(this.lblQuantityPerBranch3);
            this.Controls.Add(this.lblQuantityPerBranch1);
            this.Controls.Add(this.dgvItemsPerBranch);
            this.Controls.Add(this.lblProductQuantity);
            this.Controls.Add(this.lblProductCode);
            this.Controls.Add(this.dgItems);
            this.Controls.Add(this.lblHeader);
            this.Controls.Add(this.txtSearch);
            this.Controls.Add(this.imgIcon);
            this.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.Name = "ItemSelectWnd";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Load += new System.EventHandler(this.ItemSelectWnd_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ItemSelectWnd_KeyDown);
            this.Resize += new System.EventHandler(this.ItemSelectWnd_Resize);
            ((System.ComponentModel.ISupportInitialize)(this.dgItems)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.imgIcon)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvItemsPerBranch)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

		}
		#endregion
		
		#region Windows Form Methods

		private void ItemSelectWnd_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
		{
			System.Data.DataTable dt;
			int index;

			switch (e.KeyData)
			{
				case Keys.Escape:
                    if (dgvItemsPerBranch.Visible)
                    {
                        dgvItemsPerBranch.Visible = false;
                    }
                    else
                    {
                        dialog = DialogResult.Cancel;
                        this.Hide();
                    }
					break;

				case Keys.Enter:
                    if (dgItems.CurrentRowIndex < 0)
                    {
                        dialog = DialogResult.Cancel;
                    }
                    else
                    {
                        dialog = DialogResult.OK;
                        SelectVariation(dgItems.CurrentRowIndex);
                        mstSearchCode = txtSearch.Text;
                    }
                    this.Hide();
					break;

                case Keys.F5:
                    ShowProductPerBranchQuantity();
                    break;

                case Keys.Up:
                    dgvItemsPerBranch.Visible = false;
                    dt = (System.Data.DataTable)dgItems.DataSource;
                    if (dgItems.CurrentRowIndex > 0)
                    {
                        index = dgItems.CurrentRowIndex;
                        dgItems.CurrentRowIndex -= 1;
                        dgItems.Select(dgItems.CurrentRowIndex);
                        dgItems.UnSelect(index);
                        ShowProductTotalQuantity();
                    }
                    break;

                case Keys.Down:
                    dgvItemsPerBranch.Visible = false;
                    dt = (System.Data.DataTable)dgItems.DataSource;
                    if (dgItems.CurrentRowIndex < dt.Rows.Count - 1)
                    {
                        index = dgItems.CurrentRowIndex;

                        dgItems.CurrentRowIndex += 1;
                        dgItems.Select(dgItems.CurrentRowIndex);
                        dgItems.UnSelect(index);
                        ShowProductTotalQuantity();
                    }
                    break;
			}
		}
		
		private void ItemSelectWnd_Load(object sender, System.EventArgs e)
		{
			LoadOptions();
			LoadItemData();
		}

		#endregion
		
		#region Windows Control Methods

        private void ItemSelectWnd_Resize(object sender, System.EventArgs e)
        {
            //dgStyle.GridColumnStyles["ProductCode"].Width = 250;
            //dgStyle.GridColumnStyles["ProductGroup"].Width = 250;
            //dgStyle.GridColumnStyles["Price"].Width = 250;
            //dgStyle.GridColumnStyles["Quantity"].Width = this.Width - 510;
        }

		private void txtSearch_TextChanged(object sender, System.EventArgs e)
		{
            tmrSearch.Enabled = false;
            tmrSearch.Enabled = true;
//            LoadItemData();
		}

        private void dgItems_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            DataGrid dgItems = (DataGrid)sender;
            System.Windows.Forms.DataGrid.HitTestInfo hti = dgItems.HitTest(e.X, e.Y);

            switch (hti.Type)
            {
                case System.Windows.Forms.DataGrid.HitTestType.Cell:
                    dgItems.Select(hti.Row);
                    SelectVariation(hti.Row);
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

        private void tmrSearch_Tick(object sender, EventArgs e)
        {
            LoadItemData();
            tmrSearch.Enabled = false;
        }

		#endregion

		#region Private Methods

		private void SelectVariation(int iRow)
		{
			try 
			{
                mstBarCode = string.IsNullOrEmpty(dgItems[iRow, 3].ToString()) ? dgItems[iRow, 4].ToString() : dgItems[iRow, 3].ToString();

                ProductModel.Clear();
                ProductModel.PackageID = Int64.Parse(dgItems[iRow, 0].ToString());
                ProductModel.ProductID = Int64.Parse(dgItems[iRow, 1].ToString());
                ProductModel.MatrixID = Int64.Parse(dgItems[iRow, 2].ToString());
                ProductModel.BarCode = mstBarCode;
                
			}
			catch (Exception ex)
			{
				MessageBox.Show("No item has been selected. Please select at least 1 item.", "RetailPlus", MessageBoxButtons.OK, MessageBoxIcon.Error);
				Event clsEvent = new Event();
				clsEvent.AddEventLn("Item select error. TRACE: " + ex.Message.Replace(Environment.NewLine,""), true);
			}
		}

		private void LoadOptions()
		{
            if (TerminalDetails.IsParkingTerminal)
            {
                dgStyle.GridColumnStyles["BarCode"].Width = 0;
                dgStyle.GridColumnStyles["ProductCode"].HeaderText = "Parking Type";
                dgStyle.GridColumnStyles["ProductCode"].Width = this.Width - 495 + 150;
                dgStyle.GridColumnStyles["MatrixDescription"].Width = 0;

                this.Quantity.Format = "#,##0";
                dgStyle.GridColumnStyles["Quantity"].Width = 320;
                dgStyle.GridColumnStyles["Quantity"].HeaderText = "Available Slots";
                this.lblHeader.Text = "Select Parking rate or enter 'Rate Code' to search:";
                this.txtSearch.Text = mstSearchCode;
            }
			else if (mboIsPriceInq)
			{
                if (CONFIG.ShowBarcodeNotProductCodeItemSelect == true)
                {
                    dgStyle.GridColumnStyles["BarCode"].Width = 150;
                    if (!CONFIG.ShowDescriptionDuringItemSelect)
                    { 
                        dgStyle.GridColumnStyles["ProductCode"].Width = this.Width - 590;
                        dgStyle.GridColumnStyles["MatrixDescription"].Width = 200;
                    }
                    else
                    {
                        dgStyle.GridColumnStyles["ProductDesc"].Width = this.Width - 590;
                        dgStyle.GridColumnStyles["MatrixDescription"].Width = 200;
                    }
                }
                else
                {
                    dgStyle.GridColumnStyles["BarCode"].Width = 0;
                    dgStyle.GridColumnStyles["ProductCode"].Width = 200;
                    dgStyle.GridColumnStyles["ProductDesc"].Width = this.Width - 590;
                    dgStyle.GridColumnStyles["MatrixDescription"].Width = 150;
                }

                switch (ContactDetails.PriceLevel)
                {
                    case PriceLevel.SRP:
                        dgStyle.GridColumnStyles["Price"].Width = 100;
                        break;
                    case PriceLevel.One:
                        dgStyle.GridColumnStyles["Price1"].Width = 100;
                        break;
                    case PriceLevel.Two:
                        dgStyle.GridColumnStyles["Price2"].Width = 100;
                        break;
                    case PriceLevel.Three:
                        dgStyle.GridColumnStyles["Price3"].Width = 100;
                        break;
                    case PriceLevel.Four:
                        dgStyle.GridColumnStyles["Price4"].Width = 100;
                        break;
                    case PriceLevel.Five:
                        dgStyle.GridColumnStyles["Price5"].Width = 100;
                        break;
                    case PriceLevel.WSPrice:
                        dgStyle.GridColumnStyles["WSPrice"].Width = 100;
                        break;
                    default:
                        dgStyle.GridColumnStyles["Price"].Width = 100;
                        break;
                }
				
				dgStyle.GridColumnStyles["Quantity"].Width = 120;
                this.lblHeader.Text = "Price Inquiry. Enter search criteria:";
			}
			else 
			{
				this.Price.HeaderText = "";
				//dgStyle.GridColumnStyles["ProductCode"].Width = 340;
                if (CONFIG.ShowBarcodeNotProductCodeItemSelect == true)
                { 
                    dgStyle.GridColumnStyles["BarCode"].Width = 150;
                    if (!CONFIG.ShowDescriptionDuringItemSelect)
                    {
                        dgStyle.GridColumnStyles["ProductCode"].Width = this.Width - 495;
                        dgStyle.GridColumnStyles["MatrixDescription"].Width = 200; 
                    }
                    else
                    {
                        dgStyle.GridColumnStyles["ProductDesc"].Width = this.Width - 495;
                        dgStyle.GridColumnStyles["MatrixDescription"].Width = 200; 
                    }
                }
                else
                {
                    dgStyle.GridColumnStyles["BarCode"].Width = 0;
                    dgStyle.GridColumnStyles["ProductCode"].Width = 200;
                    dgStyle.GridColumnStyles["ProductDesc"].Width = this.Width - 495;
                    dgStyle.GridColumnStyles["MatrixDescription"].Width = 150;
                }
                
                //dgStyle.GridColumnStyles["ProductDesc"].Width = this.Width - 480;
                //dgStyle.GridColumnStyles["ProductGroupName"].Width = this.Width - 470;
				dgStyle.GridColumnStyles["Quantity"].Width = 120;
                this.lblHeader.Text = "Select Item. Enter search criteria:";
                this.txtSearch.Text = mstSearchCode;
			}

            lblProductCode.Text = "";
            lblProductQuantity.Text = "";

            lblQuantityPerBranch1.Visible = mclsSysConfigDetails.WillShowProductBranchQuantityInItemSelect;
            lblQuantityPerBranch2.Visible = mclsSysConfigDetails.WillShowProductBranchQuantityInItemSelect;
            lblQuantityPerBranch3.Visible = mclsSysConfigDetails.WillShowProductBranchQuantityInItemSelect;
		}

		private void LoadItemData()
		{	
			try
			{
                string strSearchKey = Constants.MaskProductSearch + txtSearch.Text;
                Products clsProduct = new Products();
                // System.Data.DataTable dt = clsProduct.ListAsDataTable(TerminalDetails.BranchID, clsSearchKeys: new ProductDetails(), mboShowInActiveProducts ? ProductListFilterType.ShowInactiveOnly : ProductListFilterType.ShowActiveOnly, Limit: 100, 
                System.Data.DataTable dt = clsProduct.ListAsDataTableFE(TerminalDetails.BranchID, strSearchKey, mboShowInActiveProducts ? ProductListFilterType.ShowActiveAndInactive : ProductListFilterType.ShowActiveOnly, 100, mboShowItemMoreThanZeroQty); 
				clsProduct.CommitAndDispose();

                this.dgStyle.MappingName = dt.TableName;
				dgItems.DataSource = dt;
				dgItems.Select(0);
				dgItems.CurrentRowIndex=0;

                dgvItemsPerBranch.Visible = false;
                ShowProductTotalQuantity();
			}
            catch (IndexOutOfRangeException) { lblProductCode.Visible = false; lblProductQuantity.Visible = false; }
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message,"RetailPlus",MessageBoxButtons.OK,MessageBoxIcon.Error); 
			}
		}

        private void TagMinThreshold()
        {

        }

        private void ShowProductTotalQuantity()
        {
            try
            {
                if (mclsSysConfigDetails.WillShowProductTotalQuantityInItemSelect)
                {
                    if (dgItems.CurrentRowIndex < 0)
                    {
                        lblProductCode.Visible = false;
                        lblProductQuantity.Visible = false;
                    }
                    else
                    {
                        ProductModel.ProductID = Int64.Parse(dgItems[dgItems.CurrentRowIndex, 1].ToString());
                        ProductModel.MatrixID = Int64.Parse(dgItems[dgItems.CurrentRowIndex, 2].ToString());

                        Data.Products clsProduct = new Data.Products();
                        Data.ProductDetails clsProductDetails = clsProduct.Details(ProductModel.ProductID, ProductModel.MatrixID);

                        lblProductCode.Text = clsProductDetails.ProductCode + " Qty:";
                        lblProductQuantity.Text = clsProductDetails.ConvertedQuantity;
                        clsProduct.CommitAndDispose();

                        lblProductCode.Visible = true;
                        lblProductQuantity.Visible = true;
                    }
                }
                else
                {
                    lblProductCode.Visible = false;
                    lblProductQuantity.Visible = false;
                }
            }
            catch { }
        }
        private void ShowProductPerBranchQuantity()
        {
            try
            {
                if (mclsSysConfigDetails.WillShowProductBranchQuantityInItemSelect)
                {
                    if (dgItems.CurrentRowIndex < 0)
                    {
                        dgvItemsPerBranch.Visible = false;
                    }
                    else
                    {
                        ProductModel.ProductID = Int64.Parse(dgItems[dgItems.CurrentRowIndex, 1].ToString());
                        ProductModel.MatrixID = Int64.Parse(dgItems[dgItems.CurrentRowIndex, 2].ToString());

                        ProductInventories clsProduct = new ProductInventories();
                        System.Data.DataTable dt = clsProduct.ListAsDataTable(ProductID: ProductModel.ProductID, MatrixID: ProductModel.MatrixID, isSummary: 0);
                        clsProduct.CommitAndDispose();

                        dgvItemsPerBranch.MultiSelect = false;
                        dgvItemsPerBranch.AutoGenerateColumns = true;
                        dgvItemsPerBranch.AutoSize = false;
                        dgvItemsPerBranch.DataSource = dt.TableName;
                        dgvItemsPerBranch.DataSource = dt;

                        foreach (DataGridViewColumn dc in dgvItemsPerBranch.Columns)
                        {
                            dc.Visible = false;
                        }

                        dgvItemsPerBranch.Columns["BranchCode"].Visible = true;
                        dgvItemsPerBranch.Columns["Quantity"].Visible = true;
                        dgvItemsPerBranch.Columns["ConvertedQuantity"].Visible = true;

                        dgvItemsPerBranch.Columns["BranchCode"].DisplayIndex = 0;
                        dgvItemsPerBranch.Columns["Quantity"].DisplayIndex = 1;
                        dgvItemsPerBranch.Columns["ConvertedQuantity"].DisplayIndex = 2;

                        dgvItemsPerBranch.Columns["BranchCode"].Width = 170;
                        dgvItemsPerBranch.Columns["Quantity"].Width = 75;
                        dgvItemsPerBranch.Columns["ConvertedQuantity"].Width = 90;

                        dgvItemsPerBranch.Columns["BranchCode"].HeaderText = "Branch";
                        dgvItemsPerBranch.Columns["Quantity"].HeaderText = "Quantity";
                        dgvItemsPerBranch.Columns["ConvertedQuantity"].HeaderText = "Converted Qty";

                        dgvItemsPerBranch.Columns["Quantity"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight;
                        dgvItemsPerBranch.Columns["ConvertedQuantity"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

                        dgvItemsPerBranch.Columns["Quantity"].DefaultCellStyle.Format = "#,##0.#0";

                        dgvItemsPerBranch.ReadOnly = true;

                        dgvItemsPerBranch.Height = dt.Rows.Count * 30;
                        dgvItemsPerBranch.Left = dgItems.Width - 480;
                        if (dgItems.GetCurrentCellBounds().Y  >= 500)
                            dgvItemsPerBranch.Top = dgItems.GetCurrentCellBounds().Y + 50 - dgvItemsPerBranch.Height;
                        else
                            dgvItemsPerBranch.Top = dgItems.GetCurrentCellBounds().Y + 105;

                        dgvItemsPerBranch.Visible = true;
                    }
                }
                else
                {
                    dgvItemsPerBranch.Visible = false;
                }
            }
            catch { }
        }

        #endregion

    }

    public class DataGridQuantityTextBoxColumn : DataGridTextBoxColumn
    {
        protected override void Paint(System.Drawing.Graphics g,
             System.Drawing.Rectangle bounds, System.Windows.Forms.CurrencyManager
             source, int rowNum, System.Drawing.Brush backBrush, System.Drawing.Brush
             foreBrush, bool alignToRight)
        {
            // the idea is to conditionally set the foreBrush and/or backbrush
            // depending upon some criteria on the cell value
            // Here, we color anything that begins with a letter higher than 'F'
            try
            {
                object objQuantity = this.GetColumnValueAtRow(source, rowNum);
                if (objQuantity != null)
                {
                    System.Data.DataRowView drView = (System.Data.DataRowView)source.Current;
                    decimal decMinThreshold = decimal.Parse(drView.Row["MinThreshold"].ToString());

                    decimal decQuantity = decimal.Parse(objQuantity.ToString());
                    if (decQuantity <= decMinThreshold)
                    {
                        // could be as simple as
                        // backBrush = new SolidBrush(Color.Pink);
                        // or something fancier...
                        //backBrush = new LinearGradientBrush(bounds,
                        //     Color.FromArgb(255, 200, 200),
                        //     Color.FromArgb(128, 20, 20),
                        //     LinearGradientMode.BackwardDiagonal);
                        backBrush = new SolidBrush(Color.Red);
                        foreBrush = new SolidBrush(Color.White);
                    }
                }
            }
            catch { /* empty catch */ }
            finally
            {
                // make sure the base class gets called to do the drawing with
                // the possibly changed brushes
                base.Paint(g, bounds, source, rowNum, backBrush, foreBrush, alignToRight);
            }
        }
    } 
}

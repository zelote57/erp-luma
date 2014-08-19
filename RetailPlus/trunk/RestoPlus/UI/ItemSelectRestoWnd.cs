using System;
using System.Drawing;
using System.Collections;
using System.Windows.Forms;
using AceSoft.RetailPlus.Data;

namespace AceSoft.RetailPlus.Client.UI
{
	public class ItemSelectRestoWnd : System.Windows.Forms.Form
	{
        private enum ProductListType
        {
            ProductGroup = 0,
            ProductList
        }
        private System.Windows.Forms.Label lblHeader;
		private DialogResult dialog;
		private string mstBarCode;
		private bool mboIsPriceInq;
		private bool mboShowItemMoreThanZeroQty;
        private bool mboShowInActiveProducts;

        private System.Windows.Forms.PictureBox imgIcon;
        private ListView lstGroup;
        private ImageList imgItems;
        private ListView lstItems;
        private ImageList imgGroup;
        private Label lblReloadGroup;
        private System.ComponentModel.IContainer components;
    
		public DialogResult Result
		{
			get {	return dialog;	}
		}

		public string SelectedBarCode
		{
			get	{	return mstBarCode;	}
		}

		public bool IsPriceInq
		{
			set	{	mboIsPriceInq = value;	}
		}

		public bool ShowItemMoreThanZeroQty
		{
			set	{	mboShowItemMoreThanZeroQty = value;	}
		}
        // Aug 6, 2011  :Lemu
        // Include InActive products during refund
        public bool ShowInActiveProducts
        {
            set { mboShowInActiveProducts = value; }
        }

		#region Constructors and Destructors

		public ItemSelectRestoWnd()
		{
			InitializeComponent();
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ItemSelectRestoWnd));
            this.lblHeader = new System.Windows.Forms.Label();
            this.lstGroup = new System.Windows.Forms.ListView();
            this.imgGroup = new System.Windows.Forms.ImageList(this.components);
            this.imgItems = new System.Windows.Forms.ImageList(this.components);
            this.imgIcon = new System.Windows.Forms.PictureBox();
            this.lstItems = new System.Windows.Forms.ListView();
            this.lblReloadGroup = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.imgIcon)).BeginInit();
            this.SuspendLayout();
            // 
            // lblHeader
            // 
            this.lblHeader.AutoSize = true;
            this.lblHeader.BackColor = System.Drawing.Color.Transparent;
            this.lblHeader.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblHeader.ForeColor = System.Drawing.Color.White;
            this.lblHeader.Location = new System.Drawing.Point(67, 23);
            this.lblHeader.Name = "lblHeader";
            this.lblHeader.Size = new System.Drawing.Size(144, 13);
            this.lblHeader.TabIndex = 4;
            this.lblHeader.Text = "Select item to purchase.";
            // 
            // lstGroup
            // 
            this.lstGroup.BackColor = System.Drawing.Color.White;
            this.lstGroup.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lstGroup.ForeColor = System.Drawing.Color.Red;
            this.lstGroup.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None;
            this.lstGroup.LabelWrap = false;
            this.lstGroup.LargeImageList = this.imgGroup;
            this.lstGroup.Location = new System.Drawing.Point(0, 64);
            this.lstGroup.Name = "lstGroup";
            this.lstGroup.Size = new System.Drawing.Size(802, 556);
            this.lstGroup.TabIndex = 92;
            this.lstGroup.UseCompatibleStateImageBehavior = false;
            this.lstGroup.SelectedIndexChanged += new System.EventHandler(this.lstGroup_SelectedIndexChanged);
            // 
            // imgGroup
            // 
            this.imgGroup.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imgGroup.ImageStream")));
            this.imgGroup.TransparentColor = System.Drawing.Color.Transparent;
            this.imgGroup.Images.SetKeyName(0, "blank_small_dark_green.jpg");
            this.imgGroup.Images.SetKeyName(1, "blank_small_dark_red.jpg");
            this.imgGroup.Images.SetKeyName(2, "blank_small_dark_yellow.jpg");
            // 
            // imgItems
            // 
            this.imgItems.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imgItems.ImageStream")));
            this.imgItems.TransparentColor = System.Drawing.Color.Transparent;
            this.imgItems.Images.SetKeyName(0, "blank_small_dark_green.jpg");
            this.imgItems.Images.SetKeyName(1, "blank_small_dark_red.jpg");
            this.imgItems.Images.SetKeyName(2, "blank_small_dark_yellow.jpg");
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
            // lstItems
            // 
            this.lstItems.BackColor = System.Drawing.Color.White;
            this.lstItems.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lstItems.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lstItems.ForeColor = System.Drawing.Color.Black;
            this.lstItems.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None;
            this.lstItems.HideSelection = false;
            this.lstItems.HoverSelection = true;
            this.lstItems.LargeImageList = this.imgItems;
            this.lstItems.Location = new System.Drawing.Point(0, 64);
            this.lstItems.MultiSelect = false;
            this.lstItems.Name = "lstItems";
            this.lstItems.ShowGroups = false;
            this.lstItems.Size = new System.Drawing.Size(802, 556);
            this.lstItems.TabIndex = 93;
            this.lstItems.UseCompatibleStateImageBehavior = false;
            this.lstItems.Click += new System.EventHandler(this.lstItems_Click);
            // 
            // lblReloadGroup
            // 
            this.lblReloadGroup.AutoSize = true;
            this.lblReloadGroup.BackColor = System.Drawing.Color.Transparent;
            this.lblReloadGroup.ForeColor = System.Drawing.Color.LightSlateGray;
            this.lblReloadGroup.Location = new System.Drawing.Point(573, 33);
            this.lblReloadGroup.Name = "lblReloadGroup";
            this.lblReloadGroup.Size = new System.Drawing.Size(222, 13);
            this.lblReloadGroup.TabIndex = 94;
            this.lblReloadGroup.Text = "Click or Press <F2> to reload product group.";
            this.lblReloadGroup.Click += new System.EventHandler(this.lblReloadGroup_Click);
            // 
            // ItemSelectRestoWnd
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 14);
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(802, 620);
            this.ControlBox = false;
            this.Controls.Add(this.lblReloadGroup);
            this.Controls.Add(this.lstGroup);
            this.Controls.Add(this.lblHeader);
            this.Controls.Add(this.imgIcon);
            this.Controls.Add(this.lstItems);
            this.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.Name = "ItemSelectRestoWnd";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Load += new System.EventHandler(this.ItemSelectRestoWnd_Load);
            this.Click += new System.EventHandler(this.ItemSelectRestoWnd_Click);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ItemSelectRestoWnd_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.imgIcon)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

		}
		#endregion
		
		#region Windows Form Methods

		private void ItemSelectRestoWnd_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
		{
			switch (e.KeyData)
			{
				case Keys.Escape:
					dialog = DialogResult.Cancel;
					this.Hide(); 
					break;

				case Keys.Enter:
                    SelectItem();
					break;
				
				case Keys.PageUp:
                    if (lstItems.Visible) ShowList(ProductListType.ProductGroup); else ShowList(ProductListType.ProductList); ;
					break;

				case Keys.PageDown:
                    if (lstItems.Visible) ShowList(ProductListType.ProductGroup); else ShowList(ProductListType.ProductList); ;
                    break;
                case Keys.F2:
                    ShowList(ProductListType.ProductGroup);
                    break;
			}
		}
		
		private void ItemSelectRestoWnd_Load(object sender, System.EventArgs e)
		{
			try
			{	this.BackgroundImage = new Bitmap(Application.StartupPath + "/images/Background.jpg");	}
			catch{}
			try
			{	this.imgIcon.Image = new Bitmap(Application.StartupPath + "/images/ItemSelect.jpg");	}
			catch{}

            imgItems.Images.Clear();
            try { imgItems.Images.Add(new Bitmap(Application.StartupPath + "/images/products/prod1.jpg")); }
            catch { }
            try { imgItems.Images.Add(new Bitmap(Application.StartupPath + "/images/products/prod2.jpg")); }
            catch { }
            try { imgItems.Images.Add(new Bitmap(Application.StartupPath + "/images/products/prod3.jpg")); }
            catch { }
            try { imgItems.Images.Add(new Bitmap(Application.StartupPath + "/images/products/prod4.jpg")); }
            catch { }
            try { imgItems.Images.Add(new Bitmap(Application.StartupPath + "/images/products/prod5.jpg")); }
            catch { }
            try { imgItems.Images.Add(new Bitmap(Application.StartupPath + "/images/products/prod6.jpg")); }
            catch { }
            try { imgItems.Images.Add(new Bitmap(Application.StartupPath + "/images/products/prod7.jpg")); }
            catch { }
            try { imgItems.Images.Add(new Bitmap(Application.StartupPath + "/images/products/prod8.jpg")); }
            catch { }

            imgGroup.Images.Clear();
            try { imgGroup.Images.Add(new Bitmap(Application.StartupPath + "/images/products/group1.jpg")); }
            catch { }
            try { imgGroup.Images.Add(new Bitmap(Application.StartupPath + "/images/products/group2.jpg")); }
            catch { }
            try { imgGroup.Images.Add(new Bitmap(Application.StartupPath + "/images/products/group3.jpg")); }
            catch { }
            try { imgGroup.Images.Add(new Bitmap(Application.StartupPath + "/images/products/group4.jpg")); }
            catch { }
            try { imgGroup.Images.Add(new Bitmap(Application.StartupPath + "/images/products/group5.jpg")); }
            catch { }
            try { imgGroup.Images.Add(new Bitmap(Application.StartupPath + "/images/products/group6.jpg")); }
            catch { }
            try { imgGroup.Images.Add(new Bitmap(Application.StartupPath + "/images/products/group7.jpg")); }
            catch { }
            try { imgGroup.Images.Add(new Bitmap(Application.StartupPath + "/images/products/group8.jpg")); }
            catch { }

			LoadOptions();
            ShowList(ProductListType.ProductGroup);
		}
        private void ItemSelectRestoWnd_Click(object sender, EventArgs e)
        {
            ShowList(ProductListType.ProductGroup);
        }

		#endregion
		
		#region Windows Control Methods

        private void imgIcon_Click(object sender, EventArgs e)
        {
            dialog = DialogResult.Cancel;
            this.Hide();
        }

        private void lstGroup_SelectedIndexChanged(object sender, EventArgs e)
        {
            ShowList(ProductListType.ProductList);
        }

        private void lstItems_Click(object sender, EventArgs e)
        {
            SelectItem();
        }

        private void lblReloadGroup_Click(object sender, EventArgs e)
        {
            ShowList(ProductListType.ProductGroup);
        }

		#endregion

		#region Private Methods

		private void SelectItem()
		{
            ListView.SelectedListViewItemCollection items = this.lstItems.SelectedItems;

            foreach (ListViewItem item in items)
            { mstBarCode = item.SubItems[0].Name; break; }

            if (mstBarCode != string.Empty)
            {
                dialog = DialogResult.OK;
                this.Hide();
            }
		}

		private void LoadOptions()
		{
            if (mboIsPriceInq)
                this.lblHeader.Text = "Item: Price Inquiry";
            else
                this.lblHeader.Text = "Select Item to purchase";

            ProductGroup clsProductGroup = new ProductGroup();
            System.Data.DataTable dtaProductGroup = clsProductGroup.ListAsDataTable(SortField:"ProductGroupCode");
            clsProductGroup.CommitAndDispose();

            lstGroup.Items.Clear(); int iImgCtr = 0;
            foreach(System.Data.DataRow dr in dtaProductGroup.Rows)
            {
                lstGroup.Items.Add(dr["ProductGroupCode"].ToString(),dr["ProductGroupCode"].ToString(),iImgCtr);
                if (iImgCtr == 7) iImgCtr = 0; else iImgCtr++;
            }
            try {   lstGroup.Items[0].Selected = true;  }catch { }
		}

		private void LoadItemData()
		{	
			try
			{
                string strGroupCode = string.Empty;
                ListView.SelectedListViewItemCollection items = this.lstGroup.SelectedItems;

                foreach (ListViewItem item in items)
                { strGroupCode = item.SubItems[0].Name; break; }

                if (strGroupCode != string.Empty)
                {
                    Products clsProduct = new Products();
                    System.Data.DataTable dt;
                    if (mboShowInActiveProducts == false)
                    { dt = clsProduct.SearchSaleableDataTableByGroup(strGroupCode, string.Empty, "ProductCode", SortOption.Ascending, 0, mboShowItemMoreThanZeroQty); }
                    else
                    { dt = clsProduct.SearchDataTableActiveInactive(ProductListFilterType.ShowInactiveOnly, strGroupCode, "ProductCode", SortOption.Ascending, 0, mboShowItemMoreThanZeroQty); }
                    clsProduct.CommitAndDispose();

                    lstItems.Items.Clear(); int iImgCtr = 0;
                    foreach (System.Data.DataRow dr in dt.Rows)
                    { 
                        if (mboIsPriceInq)
                            lstItems.Items.Add(dr["Barcode"].ToString(), "[" + dr["Price"].ToString() + "]" + Environment.NewLine + dr["ProductCode"].ToString(), iImgCtr); 
                        else
                            lstItems.Items.Add(dr["Barcode"].ToString(), dr["ProductCode"].ToString(), iImgCtr); 
                        if (iImgCtr == 7) iImgCtr = 0; else iImgCtr++; 
                    }
                    try { lstItems.Items[0].Selected = true; }
                    catch { }
                }
			}
			catch (IndexOutOfRangeException){}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message,"RetailPlus",MessageBoxButtons.OK,MessageBoxIcon.Error); 
			}
		}
		
        #endregion

        private void ShowList(ProductListType clsProductListType)
        {
            switch (clsProductListType)
            {
                case ProductListType.ProductList:
                    LoadItemData();
                    lstGroup.Visible = false;
                    lstItems.Visible = true;
                    lstItems.Focus();
                    break;
                case ProductListType.ProductGroup:
                    lstItems.Visible = false; 
                    lstGroup.Visible = true;
                    lstGroup.Focus();
                    break;
            }
        }
	}
}

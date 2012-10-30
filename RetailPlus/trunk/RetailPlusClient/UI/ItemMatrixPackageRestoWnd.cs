using System;
using System.Drawing;
using System.Collections;
using System.Windows.Forms;
using AceSoft.RetailPlus.Data;
using MySql.Data.MySqlClient;

namespace AceSoft.RetailPlus.Client.UI
{
	public class ItemMatrixPackageRestoWnd : System.Windows.Forms.Form
	{
        private Label lblHeader;
        private PictureBox imgIcon;
        private ImageList imgItems;
        private ListView lstItems;
        private System.ComponentModel.IContainer components;

        private DialogResult dialog;
        private SalesTransactionItemDetails mItemDetails;
        private bool mboIsPriceInq;

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
                return mItemDetails;
            }
            set
            {
                mItemDetails = value;
            }
        }

        public bool IsPriceInq
        {
            set { mboIsPriceInq = value; }
        }

        #region Constructors and Destructors
        
        public ItemMatrixPackageRestoWnd()
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ItemMatrixPackageRestoWnd));
            this.lblHeader = new System.Windows.Forms.Label();
            this.imgIcon = new System.Windows.Forms.PictureBox();
            this.imgItems = new System.Windows.Forms.ImageList(this.components);
            this.lstItems = new System.Windows.Forms.ListView();
            ((System.ComponentModel.ISupportInitialize)(this.imgIcon)).BeginInit();
            this.SuspendLayout();
            // 
            // lblHeader
            // 
            this.lblHeader.AutoSize = true;
            this.lblHeader.BackColor = System.Drawing.Color.Transparent;
            this.lblHeader.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblHeader.ForeColor = System.Drawing.Color.White;
            this.lblHeader.Location = new System.Drawing.Point(67, 27);
            this.lblHeader.Name = "lblHeader";
            this.lblHeader.Size = new System.Drawing.Size(164, 13);
            this.lblHeader.TabIndex = 4;
            this.lblHeader.Text = "Select Item Matrix Package";
            // 
            // imgIcon
            // 
            this.imgIcon.BackColor = System.Drawing.Color.Blue;
            this.imgIcon.Location = new System.Drawing.Point(9, 5);
            this.imgIcon.Name = "imgIcon";
            this.imgIcon.Size = new System.Drawing.Size(49, 49);
            this.imgIcon.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.imgIcon.TabIndex = 6;
            this.imgIcon.TabStop = false;
            this.imgIcon.Click += new System.EventHandler(this.imgIcon_Click);
            // 
            // imgItems
            // 
            this.imgItems.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imgItems.ImageStream")));
            this.imgItems.TransparentColor = System.Drawing.Color.Transparent;
            this.imgItems.Images.SetKeyName(0, "blank_small_dark_green.jpg");
            this.imgItems.Images.SetKeyName(1, "blank_small_dark_red.jpg");
            this.imgItems.Images.SetKeyName(2, "blank_small_dark_yellow.jpg");
            // 
            // lstItems
            // 
            this.lstItems.Activation = System.Windows.Forms.ItemActivation.OneClick;
            this.lstItems.BackColor = System.Drawing.Color.White;
            this.lstItems.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.lstItems.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lstItems.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lstItems.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lstItems.ForeColor = System.Drawing.Color.Black;
            this.lstItems.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None;
            this.lstItems.HideSelection = false;
            this.lstItems.HoverSelection = true;
            this.lstItems.LargeImageList = this.imgItems;
            this.lstItems.Location = new System.Drawing.Point(0, 60);
            this.lstItems.MultiSelect = false;
            this.lstItems.Name = "lstItems";
            this.lstItems.ShowGroups = false;
            this.lstItems.Size = new System.Drawing.Size(802, 560);
            this.lstItems.TabIndex = 95;
            this.lstItems.UseCompatibleStateImageBehavior = false;
            this.lstItems.Click += new System.EventHandler(this.lstItems_Click);
            // 
            // ItemMatrixPackageRestoWnd
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 14);
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(802, 620);
            this.ControlBox = false;
            this.Controls.Add(this.lstItems);
            this.Controls.Add(this.imgIcon);
            this.Controls.Add(this.lblHeader);
            this.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.Name = "ItemMatrixPackageRestoWnd";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Load += new System.EventHandler(this.ItemVariationsWnd_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ItemVariationsWnd_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.imgIcon)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

		}
		#endregion

        #region Windows Form Methods
        
        private void ItemVariationsWnd_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
		{
			switch (e.KeyData)
			{
				case Keys.Escape:
					dialog = DialogResult.Cancel;
					this.Hide(); 
					break;

				case Keys.Enter:
                    SelectItem();
					this.Hide(); 
					break;
			}
		}

		private void ItemVariationsWnd_Load(object sender, System.EventArgs e)
		{
			try
			{	this.BackgroundImage = new Bitmap(Application.StartupPath + "/images/Background.jpg");	}
			catch{}
			try
			{	this.imgIcon.Image = new Bitmap(Application.StartupPath + "/images/ItemMatrixPackages.jpg");	}
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

			LoadOptions();
			LoadItemData();
        }

        #endregion

        #region Windows Control Methods

        private void imgIcon_Click(object sender, EventArgs e)
        {
            dialog = DialogResult.Cancel;
            this.Hide();
        }

        private void lstItems_Click(object sender, EventArgs e)
        {
            SelectItem();
        }

        #endregion

        #region Private Methods

        private void LoadOptions()
        {
            if (mboIsPriceInq)
                this.lblHeader.Text = "Item Matrix Package: Price Inquiry";
            else
                this.lblHeader.Text = "Select Item Matrix Package";
        }

        private void LoadItemData()
        {
            try
            {
                DataClass clsData = new DataClass();
                MatrixPackage clsMatrixPackage = new MatrixPackage();
                System.Data.DataTable dt = clsData.DataReaderToDataTable(clsMatrixPackage.List(mItemDetails.VariationsMatrixID, "a.PackageID", SortOption.Ascending));
                clsMatrixPackage.CommitAndDispose();

                lstItems.Items.Clear(); int iImgCtr = 0;
                foreach (System.Data.DataRow dr in dt.Rows)
                {
                    if (mboIsPriceInq)
                        lstItems.Items.Add(dr["PackageID"].ToString(), "[" + dr["Price"].ToString() + "]" + Environment.NewLine + dr["Quantity"].ToString() + " " + dr["UnitCode"].ToString() + " - " + dr["Description"].ToString(), iImgCtr);
                    else
                        lstItems.Items.Add(dr["PackageID"].ToString(), dr["Quantity"].ToString() + " " + dr["UnitCode"].ToString() + " - " + dr["Description"].ToString(), iImgCtr);
                    if (iImgCtr == 7) iImgCtr = 0; else iImgCtr++;
                }
                try { lstItems.Items[0].Selected = true; }
                catch { }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "RetailPlus", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void SelectItem()
        {
            ListView.SelectedListViewItemCollection items = this.lstItems.SelectedItems;

            foreach (ListViewItem item in items)
            { mItemDetails.MatrixPackageID = Convert.ToInt64(item.SubItems[0].Name); break; }

            MatrixPackage clsMatrixPackage = new MatrixPackage();
            MatrixPackageDetails clsDetails = clsMatrixPackage.Details(mItemDetails.MatrixPackageID);
            clsMatrixPackage.CommitAndDispose();
            if (mItemDetails.MatrixPackageID != 0)
            {
                mItemDetails.ProductUnitID = clsDetails.UnitID;
                mItemDetails.ProductUnitCode = clsDetails.UnitCode;
                mItemDetails.Price = clsDetails.Price;
                mItemDetails.PackageQuantity = clsDetails.Quantity;
                mItemDetails.VAT = clsDetails.VAT;
                mItemDetails.LocalTax = clsDetails.LocalTax;
                mItemDetails.Amount = (mItemDetails.Quantity * mItemDetails.Price) - (mItemDetails.Quantity * mItemDetails.Discount);
                mItemDetails.PurchasePrice = clsDetails.PurchasePrice;
                mItemDetails.Amount = mItemDetails.Quantity * mItemDetails.PurchasePrice;
            }

            dialog = DialogResult.OK;
            this.Hide();
        }

        #endregion

	}
}

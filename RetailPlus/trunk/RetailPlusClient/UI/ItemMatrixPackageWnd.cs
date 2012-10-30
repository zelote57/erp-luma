using System;
using System.Drawing;
using System.Collections;
using System.Windows.Forms;
using AceSoft.RetailPlus.Data;
using MySql.Data.MySqlClient;

namespace AceSoft.RetailPlus.Client.UI
{
	public class ItemMatrixPackageWnd : System.Windows.Forms.Form
	{
		private System.Windows.Forms.Label lblHeader;
		private System.Windows.Forms.TextBox txtSearch;
		private DialogResult dialog;
		private SalesTransactionItemDetails mItemDetails;
		private System.Windows.Forms.DataGrid dgItems;
		private System.Windows.Forms.DataGridTableStyle dgStyle;
		private System.Windows.Forms.DataGridTextBoxColumn PackageID;
		private System.Windows.Forms.DataGridTextBoxColumn MatrixID;
		private System.Windows.Forms.DataGridTextBoxColumn Description;
		private System.Windows.Forms.DataGridTextBoxColumn UnitID;
		private System.Windows.Forms.DataGridTextBoxColumn UnitCode;
		private System.Windows.Forms.DataGridTextBoxColumn Price;
		private System.Windows.Forms.DataGridTextBoxColumn Quantity;
		private System.Windows.Forms.DataGridTextBoxColumn VAT;
		private System.Windows.Forms.DataGridTextBoxColumn LocalTax;
		private System.Windows.Forms.DataGridTextBoxColumn PurchasePrice;
        private System.Windows.Forms.PictureBox imgIcon;

		private System.ComponentModel.Container components = null;

		public ItemMatrixPackageWnd()
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

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
            this.txtSearch = new System.Windows.Forms.TextBox();
            this.lblHeader = new System.Windows.Forms.Label();
            this.imgIcon = new System.Windows.Forms.PictureBox();
            this.dgItems = new System.Windows.Forms.DataGrid();
            this.dgStyle = new System.Windows.Forms.DataGridTableStyle();
            this.PackageID = new System.Windows.Forms.DataGridTextBoxColumn();
            this.MatrixID = new System.Windows.Forms.DataGridTextBoxColumn();
            this.Description = new System.Windows.Forms.DataGridTextBoxColumn();
            this.UnitID = new System.Windows.Forms.DataGridTextBoxColumn();
            this.UnitCode = new System.Windows.Forms.DataGridTextBoxColumn();
            this.Price = new System.Windows.Forms.DataGridTextBoxColumn();
            this.Quantity = new System.Windows.Forms.DataGridTextBoxColumn();
            this.VAT = new System.Windows.Forms.DataGridTextBoxColumn();
            this.LocalTax = new System.Windows.Forms.DataGridTextBoxColumn();
            this.PurchasePrice = new System.Windows.Forms.DataGridTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.imgIcon)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgItems)).BeginInit();
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
            this.dgItems.RowHeaderWidth = 5;
            this.dgItems.SelectionBackColor = System.Drawing.Color.RoyalBlue;
            this.dgItems.SelectionForeColor = System.Drawing.Color.White;
            this.dgItems.Size = new System.Drawing.Size(1022, 699);
            this.dgItems.TabIndex = 7;
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
            this.MatrixID,
            this.Description,
            this.UnitID,
            this.UnitCode,
            this.Price,
            this.Quantity,
            this.VAT,
            this.LocalTax,
            this.PurchasePrice});
            this.dgStyle.HeaderBackColor = System.Drawing.Color.DarkOrange;
            this.dgStyle.HeaderFont = new System.Drawing.Font("Tahoma", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dgStyle.HeaderForeColor = System.Drawing.Color.White;
            this.dgStyle.MappingName = "tblMatrixPackage";
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
            this.PackageID.MappingName = "PackageID";
            this.PackageID.NullText = "";
            this.PackageID.ReadOnly = true;
            this.PackageID.Width = 0;
            // 
            // MatrixID
            // 
            this.MatrixID.Format = "";
            this.MatrixID.FormatInfo = null;
            this.MatrixID.MappingName = "MatrixID";
            this.MatrixID.NullText = "";
            this.MatrixID.ReadOnly = true;
            this.MatrixID.Width = 0;
            // 
            // Description
            // 
            this.Description.Format = "";
            this.Description.FormatInfo = null;
            this.Description.HeaderText = "Product";
            this.Description.MappingName = "Description";
            this.Description.NullText = "";
            this.Description.ReadOnly = true;
            this.Description.Width = 120;
            // 
            // UnitID
            // 
            this.UnitID.Format = "";
            this.UnitID.FormatInfo = null;
            this.UnitID.MappingName = "UnitID";
            this.UnitID.NullText = "";
            this.UnitID.ReadOnly = true;
            this.UnitID.Width = 0;
            // 
            // UnitCode
            // 
            this.UnitCode.Format = "";
            this.UnitCode.FormatInfo = null;
            this.UnitCode.HeaderText = "Unit";
            this.UnitCode.MappingName = "UnitCode";
            this.UnitCode.NullText = "";
            this.UnitCode.ReadOnly = true;
            this.UnitCode.Width = 80;
            // 
            // Price
            // 
            this.Price.Format = "";
            this.Price.FormatInfo = null;
            this.Price.HeaderText = "Price";
            this.Price.MappingName = "Price";
            this.Price.NullText = "";
            this.Price.ReadOnly = true;
            this.Price.Width = 80;
            // 
            // Quantity
            // 
            this.Quantity.Format = "";
            this.Quantity.FormatInfo = null;
            this.Quantity.HeaderText = "Quantity";
            this.Quantity.MappingName = "Quantity";
            this.Quantity.NullText = "";
            this.Quantity.ReadOnly = true;
            this.Quantity.Width = 80;
            // 
            // VAT
            // 
            this.VAT.Format = "";
            this.VAT.FormatInfo = null;
            this.VAT.MappingName = "VAT";
            this.VAT.NullText = "";
            this.VAT.ReadOnly = true;
            this.VAT.Width = 80;
            // 
            // LocalTax
            // 
            this.LocalTax.Format = "";
            this.LocalTax.FormatInfo = null;
            this.LocalTax.MappingName = "LocalTax";
            this.LocalTax.NullText = "";
            this.LocalTax.ReadOnly = true;
            this.LocalTax.Width = 80;
            // 
            // PurchasePrice
            // 
            this.PurchasePrice.Format = "";
            this.PurchasePrice.FormatInfo = null;
            this.PurchasePrice.MappingName = "PurchasePrice";
            this.PurchasePrice.NullText = "";
            this.PurchasePrice.ReadOnly = true;
            this.PurchasePrice.Width = 80;
            // 
            // ItemMatrixPackageWnd
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 14);
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1022, 766);
            this.ControlBox = false;
            this.Controls.Add(this.dgItems);
            this.Controls.Add(this.imgIcon);
            this.Controls.Add(this.lblHeader);
            this.Controls.Add(this.txtSearch);
            this.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.Name = "ItemMatrixPackageWnd";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Load += new System.EventHandler(this.ItemVariationsWnd_Load);
            this.Resize += new System.EventHandler(this.ItemVariationsWnd_Resize);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ItemVariationsWnd_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.imgIcon)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgItems)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

		}
		#endregion

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

		private void ItemVariationsWnd_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
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
                    if (dgItems.CurrentRowIndex < 0)
                    {
                        dialog = DialogResult.Cancel;
                    }
                    else
                    {
                        dialog = DialogResult.OK;
                        SetDetails(dgItems.CurrentRowIndex);
                    }
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

        private void SetDetails(int iRow)
		{
			mItemDetails.MatrixPackageID = Convert.ToInt64(dgItems[iRow, 0].ToString());
			mItemDetails.ProductUnitID= Convert.ToInt32(dgItems[iRow, 3].ToString());
			mItemDetails.ProductUnitCode = dgItems[iRow, 4].ToString();
			mItemDetails.Price = Convert.ToDecimal(dgItems[iRow, 5].ToString());
			mItemDetails.PackageQuantity = Convert.ToDecimal(dgItems[iRow, 6].ToString());
			mItemDetails.VAT = Convert.ToDecimal(dgItems[iRow, 7].ToString());
			mItemDetails.LocalTax = Convert.ToDecimal(dgItems[iRow, 8].ToString());
			mItemDetails.Amount = (mItemDetails.Quantity * mItemDetails.Price) - (mItemDetails.Quantity * mItemDetails.Discount);
			mItemDetails.PurchasePrice = Convert.ToDecimal(dgItems[iRow, 9].ToString());
			mItemDetails.PurchaseAmount = mItemDetails.Quantity * mItemDetails.PurchasePrice;

			dialog = DialogResult.OK;
			this.Hide();
		}

		private void ItemVariationsWnd_Load(object sender, System.EventArgs e)
		{
			try
			{	this.BackgroundImage = new Bitmap(Application.StartupPath + "/images/Background.jpg");	}
			catch{}
			try
			{	this.imgIcon.Image = new Bitmap(Application.StartupPath + "/images/ItemMatrixPackages.jpg");	}
			catch{}

			LoadOptions();
			LoadItemData();
            txtSearch.Focus();
		}

		private void LoadOptions()
		{
			SetGridItemsWidth();
            this.lblHeader.Text = "Select Variation Package. Enter search criteria:";
		}

		private void LoadItemData()
		{	
			MatrixPackage clsMatrixPackage = new MatrixPackage(); 

			try
			{
				DataClass clsData = new DataClass(); 
				System.Data.DataTable dt = 
					clsData.DataReaderToDataTable(clsMatrixPackage.List(mItemDetails.VariationsMatrixID,"a.PackageID",SortOption.Ascending));
				clsMatrixPackage.CommitAndDispose();
				
				dt.TableName = "tblMatrixPackage";
				dgItems.DataSource = dt;
				dgItems.Select(0);
				dgItems.CurrentRowIndex=0;
			}
			catch (Exception ex)
			{
				clsMatrixPackage.CommitAndDispose();
				MessageBox.Show(ex.Message,"RetailPlus",MessageBoxButtons.OK,MessageBoxIcon.Error); 
			}
		}

		private void ItemVariationsWnd_Resize(object sender, System.EventArgs e)
		{
			SetGridItemsWidth();
		}

		private void SetGridItemsWidth()
		{
			dgStyle.GridColumnStyles["PackageID"].Width = 0;
			dgStyle.GridColumnStyles["MatrixID"].Width = 0;
			dgStyle.GridColumnStyles["Description"].Width = this.Width - 250;
			dgStyle.GridColumnStyles["UnitID"].Width = 0;
			dgStyle.GridColumnStyles["UnitCode"].Width = 80;
			dgStyle.GridColumnStyles["Price"].Width = 80;
			dgStyle.GridColumnStyles["Quantity"].Width = 80;
			dgStyle.GridColumnStyles["VAT"].Width = 0;
			dgStyle.GridColumnStyles["LocalTax"].Width = 0;
			dgStyle.GridColumnStyles["PurchasePrice"].Width = 0;

		}

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
                    SetDetails(hti.Row);
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
	}
}

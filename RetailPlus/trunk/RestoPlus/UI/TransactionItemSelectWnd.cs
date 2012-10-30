using System;
using System.Drawing;
using System.Collections;
using System.Windows.Forms;
using AceSoft.RetailPlus.Data;
using MySql.Data.MySqlClient;

namespace AceSoft.RetailPlus.Client.UI
{
	public class TransactionItemSelectWnd : System.Windows.Forms.Form
	{
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.DataGrid dgItems;
		private System.Windows.Forms.DataGridTextBoxColumn ProductID;
		private System.Windows.Forms.DataGridTextBoxColumn ProductCode;
		private System.Windows.Forms.DataGridTextBoxColumn BarCode;
		private System.Windows.Forms.DataGridTextBoxColumn ProductDesc;
		private System.Windows.Forms.DataGridTextBoxColumn ProductGroup;
		private System.Windows.Forms.DataGridTextBoxColumn ProductSubGroup;
		private System.Windows.Forms.DataGridTextBoxColumn ProductUnitID;
		private System.Windows.Forms.DataGridTextBoxColumn ProductUnitCode;
		private System.Windows.Forms.DataGridTextBoxColumn Quantity;
		private System.Windows.Forms.DataGridTextBoxColumn Price;
		private System.Windows.Forms.DataGridTextBoxColumn SpecialDiscount;
		private System.Windows.Forms.DataGridTextBoxColumn VAT;
		private System.Windows.Forms.DataGridTextBoxColumn EVAT;
		private System.Windows.Forms.DataGridTextBoxColumn LocalTax;
		private System.Windows.Forms.DataGridTableStyle dgStyle;
		private System.Windows.Forms.TextBox txtSearch;

		private DialogResult dialog;
		private SalesTransactionItemDetails mDetails;
		private string mstTransactionNo;
		
		private System.Windows.Forms.PictureBox imgIcon;
		private AceSoft.KeyBoardHook.KeyboardSearchControl keyboardSearchControl1;

		private System.ComponentModel.Container components = null;

		public string TransactionNo
		{
			set { mstTransactionNo = value; }
		}
		public TransactionItemSelectWnd()
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
			this.label1 = new System.Windows.Forms.Label();
			this.dgItems = new System.Windows.Forms.DataGrid();
			this.dgStyle = new System.Windows.Forms.DataGridTableStyle();
			this.ProductID = new System.Windows.Forms.DataGridTextBoxColumn();
			this.ProductCode = new System.Windows.Forms.DataGridTextBoxColumn();
			this.BarCode = new System.Windows.Forms.DataGridTextBoxColumn();
			this.ProductDesc = new System.Windows.Forms.DataGridTextBoxColumn();
			this.ProductGroup = new System.Windows.Forms.DataGridTextBoxColumn();
			this.ProductSubGroup = new System.Windows.Forms.DataGridTextBoxColumn();
			this.ProductUnitID = new System.Windows.Forms.DataGridTextBoxColumn();
			this.ProductUnitCode = new System.Windows.Forms.DataGridTextBoxColumn();
			this.Quantity = new System.Windows.Forms.DataGridTextBoxColumn();
			this.Price = new System.Windows.Forms.DataGridTextBoxColumn();
			this.SpecialDiscount = new System.Windows.Forms.DataGridTextBoxColumn();
			this.VAT = new System.Windows.Forms.DataGridTextBoxColumn();
			this.EVAT = new System.Windows.Forms.DataGridTextBoxColumn();
			this.LocalTax = new System.Windows.Forms.DataGridTextBoxColumn();
			this.imgIcon = new System.Windows.Forms.PictureBox();
			this.keyboardSearchControl1 = new AceSoft.KeyBoardHook.KeyboardSearchControl();
			((System.ComponentModel.ISupportInitialize)(this.dgItems)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.imgIcon)).BeginInit();
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
			this.dgItems.Location = new System.Drawing.Point(0, 198);
			this.dgItems.Name = "dgItems";
			this.dgItems.PreferredRowHeight = 50;
			this.dgItems.ReadOnly = true;
			this.dgItems.RowHeadersVisible = false;
			this.dgItems.RowHeaderWidth = 7;
			this.dgItems.SelectionBackColor = System.Drawing.Color.RoyalBlue;
			this.dgItems.SelectionForeColor = System.Drawing.Color.White;
			this.dgItems.Size = new System.Drawing.Size(802, 422);
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
			this.ProductID,
			this.ProductCode,
			this.BarCode,
			this.ProductDesc,
			this.ProductGroup,
			this.ProductSubGroup,
			this.ProductUnitID,
			this.ProductUnitCode,
			this.Quantity,
			this.Price,
			this.SpecialDiscount,
			this.VAT,
			this.EVAT,
			this.LocalTax});
			this.dgStyle.HeaderBackColor = System.Drawing.Color.DarkOrange;
			this.dgStyle.HeaderFont = new System.Drawing.Font("Tahoma", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
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
			// SpecialDiscount
			// 
			this.SpecialDiscount.Format = "";
			this.SpecialDiscount.FormatInfo = null;
			this.SpecialDiscount.MappingName = "SpecialDiscount";
			this.SpecialDiscount.NullText = "0";
			this.SpecialDiscount.ReadOnly = true;
			this.SpecialDiscount.Width = 0;
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
			// keyboardSearchControl1
			// 
			this.keyboardSearchControl1.BackColor = System.Drawing.Color.White;
			this.keyboardSearchControl1.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.keyboardSearchControl1.Location = new System.Drawing.Point(0, 64);
			this.keyboardSearchControl1.Name = "keyboardSearchControl1";
			this.keyboardSearchControl1.Size = new System.Drawing.Size(802, 134);
			this.keyboardSearchControl1.TabIndex = 1;
			this.keyboardSearchControl1.TabStop = false;
			this.keyboardSearchControl1.Tag = "";
			this.keyboardSearchControl1.UserKeyPressed += new AceSoft.KeyBoardHook.KeyboardDelegate(this.keyboardSearchControl1_UserKeyPressed);
			// 
			// TransactionItemSelectWnd
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 14);
			this.BackColor = System.Drawing.Color.White;
			this.ClientSize = new System.Drawing.Size(802, 620);
			this.ControlBox = false;
			this.Controls.Add(this.keyboardSearchControl1);
			this.Controls.Add(this.dgItems);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.txtSearch);
			this.Controls.Add(this.imgIcon);
			this.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.KeyPreview = true;
			this.MaximizeBox = false;
			this.Name = "TransactionItemSelectWnd";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Load += new System.EventHandler(this.TransactionItemSelectWnd_Load);
			this.Resize += new System.EventHandler(this.TransactionItemSelectWnd_Resize);
			this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TransactionItemSelectWnd_KeyDown);
			((System.ComponentModel.ISupportInitialize)(this.dgItems)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.imgIcon)).EndInit();
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
				return mDetails;
			}
		}

		private string mstTerminalNo;
		public string TerminalNo
		{
			set { mstTerminalNo = value; }
		}

		private void TransactionItemSelectWnd_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
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

		private void CreateDetails(int iRow)
		{
			mDetails = new Data.SalesTransactionItemDetails();

			mDetails.ProductID = Convert.ToInt32(dgItems[iRow, 0]);
			mDetails.ProductCode = dgItems[iRow, 1].ToString();
			mDetails.BarCode = dgItems[iRow, 2].ToString();
			mDetails.Description = dgItems[iRow, 3].ToString();
			mDetails.ProductGroup = dgItems[iRow, 4].ToString();
			mDetails.ProductSubGroup = dgItems[iRow, 5].ToString();
			mDetails.ProductUnitID = Convert.ToInt32(dgItems[iRow, 6].ToString());
			mDetails.ProductUnitCode = dgItems[iRow, 7].ToString();
			mDetails.Quantity = Convert.ToDecimal(dgItems[iRow, 8].ToString());
			mDetails.Price = Convert.ToDecimal(dgItems[iRow, 9].ToString());
			mDetails.Discount = Convert.ToDecimal(dgItems[iRow, 10].ToString());
			mDetails.VAT = Convert.ToDecimal(dgItems[iRow, 11].ToString());
			mDetails.EVAT = Convert.ToDecimal(dgItems[iRow, 12].ToString());
			mDetails.LocalTax = Convert.ToDecimal(dgItems[iRow, 13].ToString());
			mDetails.Amount = (mDetails.Quantity * mDetails.Price) - (mDetails.Quantity * mDetails.Discount);
		}

		private void TransactionItemSelectWnd_Load(object sender, System.EventArgs e)
		{
			try
			{	this.BackgroundImage = new Bitmap(Application.StartupPath + "/images/Background.jpg");	}
			catch{}
			try
			{	this.imgIcon.Image = new Bitmap(Application.StartupPath + "/images/TransactionItemSelect.jpg");	}
			catch{}

			LoadOptions();
			LoadItemData();
			txtSearch.Focus();
		}

		private void LoadOptions()
		{
			dgStyle.GridColumnStyles["ProductCode"].Width = 180;
			dgStyle.GridColumnStyles["BarCode"].Width = 180;
			dgStyle.GridColumnStyles["ProductDesc"].Width = this.Width - 370;
		}

		private void LoadItemData()
		{	
			try
			{
				
				System.Data.DataTable dt = new System.Data.DataTable("tblproducts");

				dt.Columns.Add("ProductID");
				dt.Columns.Add("ProductCode");
				dt.Columns.Add("BarCode");
				dt.Columns.Add("ProductDesc");
				dt.Columns.Add("ProductGroup");
				dt.Columns.Add("ProductSubGroup");
				dt.Columns.Add("ProductUnitID");
				dt.Columns.Add("ProductUnitCode");
				dt.Columns.Add("Quantity");
				dt.Columns.Add("Price");
				dt.Columns.Add("SpecialDiscount");
				dt.Columns.Add("VAT");
				dt.Columns.Add("EVAT");
				dt.Columns.Add("LocalTax");
				
				Data.SalesTransactions clsSalesTransactions = new Data.SalesTransactions();
				Data.SalesTransactionDetails det = clsSalesTransactions.Details(mstTransactionNo, mstTerminalNo, Constants.TerminalBranchID);
				clsSalesTransactions.CommitAndDispose();

				Data.SalesTransactionItems clsItems = new Data.SalesTransactionItems();
				Data.SalesTransactionItemDetails[] TransactionItems = clsItems.Details(det.TransactionID, det.TransactionDate);
				clsItems.CommitAndDispose();

				foreach (Data.SalesTransactionItemDetails item in TransactionItems)
				{
					System.Data.DataRow dr = dt.NewRow();

					dr["ProductID"] = item.ProductID; 
					dr["ProductCode"] = item.ProductCode;
					dr["BarCode"] = item.BarCode;
					dr["ProductDesc"] = item.Description;
					dr["ProductGroup"] = item.ProductGroup;
					dr["ProductSubGroup"] = item.ProductSubGroup;
					dr["ProductUnitID"] = item.ProductUnitID;
					dr["ProductUnitCode"] = item.ProductUnitCode;
					dr["Quantity"] = item.Quantity;
					dr["Price"] = item.Price;
					dr["SpecialDiscount"] = item.Discount;
					dr["VAT"] = item.VAT;
					dr["EVAT"] = item.EVAT;
					dr["LocalTax"] = item.LocalTax;

					dt.Rows.Add(dr);

				}

				dgItems.DataSource = dt;
				
				if (dgItems.VisibleRowCount > 0)
					dgItems.Select(0);
					dgItems.CurrentRowIndex=0;
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message,"RetailPlus",MessageBoxButtons.OK,MessageBoxIcon.Error); 
			}
		}

		private void TransactionItemSelectWnd_Resize(object sender, System.EventArgs e)
		{
			dgStyle.GridColumnStyles["ProductCode"].Width = 180;
			dgStyle.GridColumnStyles["BarCode"].Width = 180;
			dgStyle.GridColumnStyles["ProductDesc"].Width = this.Width - 370;
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
	}
}

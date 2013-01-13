using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using AceSoft.RetailPlus.Security;

namespace AceSoft.RetailPlus.Client.UI
{
	public class PriceInquiryWnd : System.Windows.Forms.Form
	{
		
		private System.ComponentModel.Container components = null;
		private System.Windows.Forms.Label lblHeader;

		private DialogResult dialog;
		//private Int64 miCashierID;
		private GroupBox grpProducts;
		private Label lblProductDescription;
		private Label lblPriceLabel;
		private TextBox txtBarCode;
		private PictureBox imgCompany;
		private Label lblCaption;
		private Label lblPrice;
		private PictureBox imgIcon;
		private Label label1;
		private Label label2;
		private PictureBox imgRBSLogo;
		private CreditType mCreditType;

		public DialogResult Result
		{
			get {	return dialog;	}
		}

		public CreditType CreditType
		{
			get {	return mCreditType;	}
			set {	mCreditType = value;	}
		}

		#region Constructors And Desctructors
		public PriceInquiryWnd()
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PriceInquiryWnd));
			this.lblHeader = new System.Windows.Forms.Label();
			this.grpProducts = new System.Windows.Forms.GroupBox();
			this.imgRBSLogo = new System.Windows.Forms.PictureBox();
			this.imgCompany = new System.Windows.Forms.PictureBox();
			this.lblPrice = new System.Windows.Forms.Label();
			this.lblProductDescription = new System.Windows.Forms.Label();
			this.lblPriceLabel = new System.Windows.Forms.Label();
			this.txtBarCode = new System.Windows.Forms.TextBox();
			this.lblCaption = new System.Windows.Forms.Label();
			this.imgIcon = new System.Windows.Forms.PictureBox();
			this.label1 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.grpProducts.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.imgRBSLogo)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.imgCompany)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.imgIcon)).BeginInit();
			this.SuspendLayout();
			// 
			// lblHeader
			// 
			this.lblHeader.BackColor = System.Drawing.Color.Transparent;
			this.lblHeader.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lblHeader.ForeColor = System.Drawing.Color.White;
			this.lblHeader.Location = new System.Drawing.Point(67, 22);
			this.lblHeader.Name = "lblHeader";
			this.lblHeader.Size = new System.Drawing.Size(408, 16);
			this.lblHeader.TabIndex = 3;
			this.lblHeader.Text = "PRICE INQURY... REWARD POINTS ... CREDIT CARD LIMIT     INQUIRY";
			// 
			// grpProducts
			// 
			this.grpProducts.BackColor = System.Drawing.Color.White;
			this.grpProducts.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
			this.grpProducts.Controls.Add(this.imgRBSLogo);
			this.grpProducts.Controls.Add(this.imgCompany);
			this.grpProducts.Controls.Add(this.lblPrice);
			this.grpProducts.Controls.Add(this.lblProductDescription);
			this.grpProducts.Controls.Add(this.lblPriceLabel);
			this.grpProducts.Controls.Add(this.txtBarCode);
			this.grpProducts.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.grpProducts.ForeColor = System.Drawing.Color.Blue;
			this.grpProducts.Location = new System.Drawing.Point(12, 178);
			this.grpProducts.Name = "grpProducts";
			this.grpProducts.Size = new System.Drawing.Size(780, 430);
			this.grpProducts.TabIndex = 0;
			this.grpProducts.TabStop = false;
			// 
			// imgRBSLogo
			// 
			this.imgRBSLogo.BackColor = System.Drawing.Color.Transparent;
			this.imgRBSLogo.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
			this.imgRBSLogo.Location = new System.Drawing.Point(5, 242);
			this.imgRBSLogo.Name = "imgRBSLogo";
			this.imgRBSLogo.Size = new System.Drawing.Size(206, 181);
			this.imgRBSLogo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
			this.imgRBSLogo.TabIndex = 89;
			this.imgRBSLogo.TabStop = false;
			// 
			// imgCompany
			// 
			this.imgCompany.BackColor = System.Drawing.Color.Transparent;
			this.imgCompany.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
			this.imgCompany.Location = new System.Drawing.Point(570, 242);
			this.imgCompany.Name = "imgCompany";
			this.imgCompany.Size = new System.Drawing.Size(206, 181);
			this.imgCompany.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
			this.imgCompany.TabIndex = 83;
			this.imgCompany.TabStop = false;
			// 
			// lblPrice
			// 
			this.lblPrice.AutoEllipsis = true;
			this.lblPrice.BackColor = System.Drawing.Color.Transparent;
			this.lblPrice.Font = new System.Drawing.Font("Tahoma", 48F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lblPrice.ForeColor = System.Drawing.Color.Red;
			this.lblPrice.Location = new System.Drawing.Point(200, 193);
			this.lblPrice.Name = "lblPrice";
			this.lblPrice.Size = new System.Drawing.Size(398, 64);
			this.lblPrice.TabIndex = 9;
			this.lblPrice.Text = "100.00";
			this.lblPrice.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// lblProductDescription
			// 
			this.lblProductDescription.Font = new System.Drawing.Font("Tahoma", 26.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lblProductDescription.ForeColor = System.Drawing.Color.MediumBlue;
			this.lblProductDescription.Location = new System.Drawing.Point(6, 84);
			this.lblProductDescription.Name = "lblProductDescription";
			this.lblProductDescription.Size = new System.Drawing.Size(768, 51);
			this.lblProductDescription.TabIndex = 1;
			this.lblProductDescription.Text = "CHUPA CHUPS";
			this.lblProductDescription.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// lblPriceLabel
			// 
			this.lblPriceLabel.AutoSize = true;
			this.lblPriceLabel.Font = new System.Drawing.Font("Tahoma", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lblPriceLabel.ForeColor = System.Drawing.Color.MediumBlue;
			this.lblPriceLabel.Location = new System.Drawing.Point(200, 156);
			this.lblPriceLabel.Name = "lblPriceLabel";
			this.lblPriceLabel.Size = new System.Drawing.Size(77, 23);
			this.lblPriceLabel.TabIndex = 4;
			this.lblPriceLabel.Text = "PRICE:";
			// 
			// txtBarCode
			// 
			this.txtBarCode.BackColor = System.Drawing.Color.White;
			this.txtBarCode.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.txtBarCode.Font = new System.Drawing.Font("Tahoma", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.txtBarCode.ForeColor = System.Drawing.Color.Transparent;
			this.txtBarCode.Location = new System.Drawing.Point(124, 449);
			this.txtBarCode.Name = "txtBarCode";
			this.txtBarCode.Size = new System.Drawing.Size(539, 23);
			this.txtBarCode.TabIndex = 82;
			// 
			// lblCaption
			// 
			this.lblCaption.AutoSize = true;
			this.lblCaption.BackColor = System.Drawing.Color.Transparent;
			this.lblCaption.Font = new System.Drawing.Font("Tahoma", 18F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lblCaption.ForeColor = System.Drawing.Color.Red;
			this.lblCaption.Location = new System.Drawing.Point(25, 78);
			this.lblCaption.Name = "lblCaption";
			this.lblCaption.Size = new System.Drawing.Size(410, 29);
			this.lblCaption.TabIndex = 85;
			this.lblCaption.Text = "Please scan item to check the price...";
			this.lblCaption.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// imgIcon
			// 
			this.imgIcon.BackColor = System.Drawing.Color.Blue;
			this.imgIcon.Location = new System.Drawing.Point(9, 5);
			this.imgIcon.Name = "imgIcon";
			this.imgIcon.Size = new System.Drawing.Size(49, 49);
			this.imgIcon.TabIndex = 86;
			this.imgIcon.TabStop = false;
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.BackColor = System.Drawing.Color.Transparent;
			this.label1.Font = new System.Drawing.Font("Tahoma", 18F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label1.ForeColor = System.Drawing.Color.Red;
			this.label1.Location = new System.Drawing.Point(90, 107);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(471, 29);
			this.label1.TabIndex = 87;
			this.label1.Text = "or your reward card to check your points...";
			this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.BackColor = System.Drawing.Color.Transparent;
			this.label2.Font = new System.Drawing.Font("Tahoma", 18F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label2.ForeColor = System.Drawing.Color.Red;
			this.label2.Location = new System.Drawing.Point(164, 136);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(562, 29);
			this.label2.TabIndex = 88;
			this.label2.Text = "or your credit card to check your available credits...";
			this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// PriceInquiryWnd
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 14);
			this.BackColor = System.Drawing.Color.White;
			this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
			this.ClientSize = new System.Drawing.Size(802, 620);
			this.ControlBox = false;
			this.Controls.Add(this.label2);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.imgIcon);
			this.Controls.Add(this.grpProducts);
			this.Controls.Add(this.lblCaption);
			this.Controls.Add(this.lblHeader);
			this.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.KeyPreview = true;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "PriceInquiryWnd";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.TopMost = true;
			this.Load += new System.EventHandler(this.PriceInquiryWnd_Load);
			this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.PriceInquiryWnd_KeyDown);
			this.grpProducts.ResumeLayout(false);
			this.grpProducts.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.imgRBSLogo)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.imgCompany)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.imgIcon)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}
		#endregion
		#endregion

		#region Windows Form Methods

		private void PriceInquiryWnd_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
		{
			switch (e.KeyData)
			{
				case Keys.Escape:
					dialog = DialogResult.Cancel;
					this.Hide(); 
					break;

				case Keys.Enter:
					if (txtBarCode.Text.Trim() != string.Empty)
					{
						Data.Products clsProduct = new Data.Products();
						Data.ProductDetails clsProductDetails = clsProduct.Details(txtBarCode.Text.Trim());
						if (clsProductDetails.ProductID == 0)
						{
							string strContactCardNo = txtBarCode.Text.Trim();
							

							Data.Contacts clsContact = new Data.Contacts(clsProduct.Connection, clsProduct.Transaction);
							Data.ContactDetails clsContactDetails = clsContact.DetailsByRewardCardNo(strContactCardNo);
							if (clsContactDetails.ContactID == 0)
							{
								clsContactDetails = clsContact.DetailsByCreditCardNo(strContactCardNo);
								if (clsContactDetails.ContactID == 0)
								{
									strContactCardNo = strContactCardNo.Remove(strContactCardNo.Length - 1);
									clsContactDetails = clsContact.DetailsByRewardCardNo(strContactCardNo);
									if (clsContactDetails.ContactID == 0)
									{
										clsContactDetails = clsContact.DetailsByCreditCardNo(strContactCardNo);
										if (clsContactDetails.ContactID == 0)
										{
											ClearProduct();
										}
										else
										{
											decimal decAvailableCreditLimit = clsContactDetails.CreditLimit - clsContactDetails.Credit;

											lblProductDescription.Text = clsContactDetails.ContactName + " - " + clsContactDetails.CreditDetails.CreditCardNo;
											lblPrice.Text = decAvailableCreditLimit.ToString("#,##0.#0");
											lblPriceLabel.Text = "AVAILABLE CREDITS:";
										}
									}
									else
									{
										lblProductDescription.Text = clsContactDetails.ContactName + " - " + clsContactDetails.RewardDetails.RewardCardNo;
										lblPrice.Text = clsContactDetails.RewardDetails.RewardPoints.ToString("#,##0.#0");
										lblPriceLabel.Text = "AVAILABLE POINTS:";
									}
								}
								else
								{
									decimal decAvailableCreditLimit = clsContactDetails.CreditLimit - clsContactDetails.Credit;

									lblProductDescription.Text = clsContactDetails.ContactName + " - " + clsContactDetails.CreditDetails.CreditCardNo;
									lblPrice.Text = decAvailableCreditLimit.ToString("#,##0.#0");
									lblPriceLabel.Text = "AVAILABLE CREDITS:";
								}

							}
							else
							{
								lblProductDescription.Text = clsContactDetails.ContactName + " - " + clsContactDetails.RewardDetails.RewardCardNo;
								lblPrice.Text = clsContactDetails.RewardDetails.RewardPoints.ToString("#,##0.#0");
								lblPriceLabel.Text = "AVAILABLE POINTS:";
							}
						}
						else
						{
							lblProductDescription.Text = clsProductDetails.ProductDesc;
							lblPrice.Text = clsProductDetails.Price.ToString("#,##0.#0");
							lblPriceLabel.Text = "PRICE:";
						}
						clsProduct.CommitAndDispose();
						txtBarCode.Text = "";
					}
					txtBarCode.Focus();
					txtBarCode.SelectAll();
					break;
			}
		}
		private void PriceInquiryWnd_Load(object sender, System.EventArgs e)
		{
			try
			{	this.BackgroundImage = new Bitmap(Application.StartupPath + "/images/Background.jpg");	}
			catch{}
			try
			{ this.imgIcon.Image = new Bitmap(Application.StartupPath + "/images/PriceInquiry.jpg"); }
			catch { }
			try
			{	this.imgRBSLogo.Image = new Bitmap(Application.StartupPath + "/images/CompanyLogoRBS.jpg"); }
			catch{}
			try
			{ this.imgCompany.Image = new Bitmap(Application.StartupPath + "/images/CompanyLogo.jpg"); }
			catch { }

			ClearProduct();
			txtBarCode.Focus();
		}

		#endregion

		#region Windows Control Methods

		private void imgIcon_Click(object sender, EventArgs e)
		{
			dialog = DialogResult.Cancel;
			this.Hide();
		}

		#endregion

		#region Private Methods

		private void ClearProduct()
		{
			lblProductDescription.Text = string.Empty;
			lblPrice.Text = string.Empty;
			lblPriceLabel.Text = string.Empty;
		}

		#endregion

	}
}

namespace AceSoft.RetailPlus.MasterFiles._Discount
{
	using System;
	using System.Data;
	using System.Drawing;
	using System.Web;
	using System.Web.UI.WebControls;
	using System.Web.UI.HtmlControls;
	using AceSoft.RetailPlus.Data;
	
	public partial  class __Update : System.Web.UI.UserControl
	{
		
		#region Web Form Methods

		protected void Page_Load(object sender, System.EventArgs e)
		{
			if (!IsPostBack)
			{
				if (Visible)
				{
					lblReferrer.Text = Request.UrlReferrer.ToString();
					LoadOptions();	
					LoadRecord();	
				}
			}
		}

		
		#endregion

		#region Web Form Designer generated code
		override protected void OnInit(EventArgs e)
		{
			//
			// CODEGEN: This call is required by the ASP.NET Web Form Designer.
			//
			InitializeComponent();
			base.OnInit(e);
		}
		
		///		Required method for Designer support - do not modify
		///		the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.imgSave.Click += new System.Web.UI.ImageClickEventHandler(this.imgSave_Click);
			this.imgSaveBack.Click += new System.Web.UI.ImageClickEventHandler(this.imgSaveBack_Click);
			this.imgCancel.Click += new System.Web.UI.ImageClickEventHandler(this.imgCancel_Click);

		}
		#endregion

		#region Web Control Methods

		private void imgSave_Click(object sender, System.Web.UI.ImageClickEventArgs e)
		{
			SaveRecord();			
			string stParam = "?task=" + Common.Encrypt("add",Session.SessionID);
			Response.Redirect("Default.aspx" + stParam);
		}

		protected void cmdSave_Click(object sender, System.EventArgs e)
		{
			SaveRecord();
			string stParam = "?task=" + Common.Encrypt("add",Session.SessionID);
			Response.Redirect("Default.aspx" + stParam);
		}


		private void imgSaveBack_Click(object sender, System.Web.UI.ImageClickEventArgs e)
		{
			SaveRecord();
			Response.Redirect(lblReferrer.Text);
		}

		protected void cmdSaveBack_Click(object sender, System.EventArgs e)
		{
			SaveRecord();
			Response.Redirect(lblReferrer.Text);
		}


		private void imgCancel_Click(object sender, System.Web.UI.ImageClickEventArgs e)
		{
			Response.Redirect(lblReferrer.Text);
		}

		protected void cmdCancel_Click(object sender, System.EventArgs e)
		{
			Response.Redirect(lblReferrer.Text);
		}


		#endregion

		#region Private Methods

		private void LoadOptions()
		{
						
		}

		private void LoadRecord()
		{
			Int32 iID = Convert.ToInt32(Common.Decrypt(Request.QueryString["id"],Session.SessionID));
			Discount clsDiscount = new Discount();
			DiscountDetails clsDetails = clsDiscount.Details(iID);
			clsDiscount.CommitAndDispose();

			lblDiscountID.Text = Convert.ToString(clsDetails.DiscountID);
			txtDiscountCode.Text = clsDetails.DiscountCode;
			txtDiscountType.Text = clsDetails.DiscountType;
			txtDiscountPrice.Text = clsDetails.DiscountPrice.ToString();
			chkInPercent.Checked = Convert.ToBoolean(clsDetails.InPercent);
		}

		private void SaveRecord()
		{
			Discount clsDiscount = new Discount();
			DiscountDetails clsDetails = new DiscountDetails();

			clsDetails.DiscountID = Convert.ToInt16(lblDiscountID.Text);
			clsDetails.DiscountCode = txtDiscountCode.Text;
			clsDetails.DiscountType = txtDiscountType.Text;
			clsDetails.DiscountPrice = Convert.ToDecimal(txtDiscountPrice.Text);
			clsDetails.InPercent = Convert.ToByte(chkInPercent.Checked);

			clsDiscount.Update(clsDetails);
			clsDiscount.CommitAndDispose();
		}


		#endregion
	}
}

namespace AceSoft.RetailPlus.MasterFiles._Promo
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
				lblReferrer.Text = Request.UrlReferrer.ToString();
				if (Visible)
				{
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
			this.imgSaveAdd.Click += new System.Web.UI.ImageClickEventHandler(this.imgSaveAdd_Click);
			this.imgCancel.Click += new System.Web.UI.ImageClickEventHandler(this.imgCancel_Click);

		}
		#endregion

		#region Web Control Methods

		private void imgSave_Click(object sender, System.Web.UI.ImageClickEventArgs e)
		{
            if (SaveRecord() != 0)
            {
                string stParam = "?task=" + Common.Encrypt("add", Session.SessionID);
                Response.Redirect("Default.aspx" + stParam);
            }
		}

		protected void cmdSave_Click(object sender, System.EventArgs e)
		{
            if (SaveRecord() != 0)
            {
                string stParam = "?task=" + Common.Encrypt("add", Session.SessionID);
                Response.Redirect("Default.aspx" + stParam);
            }
		}


		private void imgSaveBack_Click(object sender, System.Web.UI.ImageClickEventArgs e)
		{
			if (SaveRecord() != 0)  Response.Redirect(lblReferrer.Text);
		}

		protected void cmdSaveBack_Click(object sender, System.EventArgs e)
		{
            if (SaveRecord() != 0) Response.Redirect(lblReferrer.Text);
		}


		private void imgSaveAdd_Click(object sender, System.Web.UI.ImageClickEventArgs e)
		{
			string stID = SaveRecord().ToString();
            if (stID != Constants.ZERO_STRING)
            {
                string stParam = "?task=" + Common.Encrypt("stuff", Session.SessionID) + "&id=" + Common.Encrypt(stID, Session.SessionID);
                Response.Redirect("Default.aspx" + stParam);
            }
		}

		protected void cmdSaveAdd_Click(object sender, System.EventArgs e)
		{
            string stID = SaveRecord().ToString();
            if (stID != Constants.ZERO_STRING)
            {
                string stParam = "?task=" + Common.Encrypt("stuff", Session.SessionID) + "&id=" + Common.Encrypt(stID, Session.SessionID);
                Response.Redirect("Default.aspx" + stParam);
            }
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
			PromoType clsPromoType= new PromoType();
			DataClass clsDataClass = new DataClass();
			
			cboPromoType.DataTextField = "PromoTypeCode";
			cboPromoType.DataValueField = "PromoTypeID";
			cboPromoType.DataSource = clsDataClass.DataReaderToDataTable(clsPromoType.List("PromoTypeID",SortOption.Ascending)).DefaultView;
			cboPromoType.DataBind();
			cboPromoType.SelectedIndex = cboPromoType.Items.Count - 1;	

			clsPromoType.CommitAndDispose();
		}

		private void LoadRecord()
		{
			Int64 iID = Convert.ToInt64(Common.Decrypt(Request.QueryString["id"],Session.SessionID));
			Promo clsPromo = new Promo();
			PromoDetails clsDetails = clsPromo.Details(iID);
			clsPromo.CommitAndDispose();

			lblPromoID.Text = clsDetails.PromoID.ToString();
			txtPromoCode.Text = clsDetails.PromoCode;
			txtPromoName.Text = clsDetails.PromoName;
			txtStartDate.Text = clsDetails.StartDate.ToString("yyyy-MM-dd");
            txtStartTime.Text = clsDetails.StartDate.ToString("HH:mm");
            txtEndDate.Text = clsDetails.EndDate.ToString("yyyy-MM-dd");
            txtEndTime.Text = clsDetails.EndDate.ToString("HH:mm");
			cboPromoType.SelectedIndex = cboPromoType.Items.IndexOf(cboPromoType.Items.FindByValue(clsDetails.PromoTypeID.ToString()));
		}


		private long SaveRecord()
		{
            long lngRetValue = 0;
            string stScript = string.Empty;

            DateTime dteStartDateTime = DateTime.MinValue;
            try { dteStartDateTime = Convert.ToDateTime(txtStartDate.Text + " " + txtStartTime.Text); }
            catch
            {
                stScript += "<Script>";
                stScript += "window.alert('Please enter a valid start date time of promo.')";
                stScript += "</Script>";
                Response.Write(stScript);
                return lngRetValue;
            }
            DateTime dteEndDateTime = DateTime.MinValue;
            try { dteEndDateTime = Convert.ToDateTime(txtEndDate.Text + " " + txtEndTime.Text); }
            catch
            {
                stScript += "<Script>";
                stScript += "window.alert('Please enter a valid end date time of promo.')";
                stScript += "</Script>";
                Response.Write(stScript);
                return lngRetValue;
            }

            Promo clsPromo = new Promo();
            PromoDetails clsDetails = new PromoDetails();

            clsDetails.PromoID = Convert.ToInt64(lblPromoID.Text);
            clsDetails.PromoCode = txtPromoCode.Text;
            clsDetails.PromoName = txtPromoName.Text;
            clsDetails.StartDate = dteStartDateTime;
            clsDetails.EndDate = dteEndDateTime;
            clsDetails.PromoTypeID = Convert.ToInt32(cboPromoType.SelectedItem.Value);

            clsPromo.Update(clsDetails);
            clsPromo.CommitAndDispose();

            lngRetValue = clsDetails.PromoID;
            return lngRetValue;
		}


		#endregion
		
	}
}

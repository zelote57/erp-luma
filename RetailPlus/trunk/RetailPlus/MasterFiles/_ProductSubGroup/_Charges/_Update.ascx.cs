namespace AceSoft.RetailPlus.MasterFiles._ProductSubGroup._Charges
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

		private void LoadOptions()
		{
			DataClass clsDataClass = new DataClass();
			lblProductGroupID.Text = Common.Decrypt((string)Request.QueryString["groupid"],Session.SessionID);
			lblChargeID.Text = Common.Decrypt(Request.QueryString["id"],Session.SessionID);

			ProductGroupCharge clsProductGroupCharge = new ProductGroupCharge();
			
			cboChargeType.DataTextField = "ChargeType";
			cboChargeType.DataValueField = "ChargeTypeID";
			cboChargeType.DataSource =  clsDataClass.DataReaderToDataTable(clsProductGroupCharge.AvailableCharges(Convert.ToInt32(lblProductGroupID.Text),"ChargeType",SortOption.Ascending)).DefaultView;
			cboChargeType.DataBind();
			cboChargeType.SelectedIndex = cboChargeType.Items.Count - 1;

			clsProductGroupCharge.CommitAndDispose();	
			cboChargeType_SelectedIndexChanged(null, null);
		}

		private void LoadRecord()
		{
			ProductGroupCharge clsProductGroupCharge = new ProductGroupCharge();
			ProductGroupChargeDetails clsDetails = clsProductGroupCharge.Details(Convert.ToInt64(lblChargeID.Text));
			clsProductGroupCharge.CommitAndDispose();

			cboChargeType.Items.Add(new ListItem(clsDetails.ChargeType, clsDetails.ChargeTypeID.ToString()));
			cboChargeType.SelectedIndex = cboChargeType.Items.IndexOf(cboChargeType.Items.FindByValue(clsDetails.ChargeTypeID.ToString())); 
			txtChargeAmount.Text = clsDetails.ChargeAmount.ToString("#,##0.#0");
			chkInPercent.Checked = Convert.ToBoolean(clsDetails.InPercent);
		}

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
			this.imgAdd.Click += new System.Web.UI.ImageClickEventHandler(this.imgAdd_Click);

		}
		#endregion

		private void SaveRecord()
		{
			ProductGroupCharge clsProductGroupCharge = new ProductGroupCharge();
			ProductGroupChargeDetails clsDetails = new ProductGroupChargeDetails();

			clsDetails.ChargeID = Convert.ToInt64(lblChargeID.Text);
			clsDetails.GroupID = Convert.ToInt64(lblProductGroupID.Text);
			clsDetails.ChargeTypeID = Convert.ToInt32(cboChargeType.SelectedItem.Value);
			clsDetails.ChargeType = cboChargeType.SelectedItem.Text;
			clsDetails.ChargeAmount = Convert.ToDecimal(txtChargeAmount.Text);
			clsDetails.InPercent = Convert.ToByte(chkInPercent.Checked);

			clsProductGroupCharge.Update(clsDetails);
			
			clsProductGroupCharge.CommitAndDispose();
		}

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

		private void imgAdd_Click(object sender, System.Web.UI.ImageClickEventArgs e)
		{
			string stParam = "?task=" + Common.Encrypt("add",Session.SessionID);	
			Response.Redirect("/RetailPlus/MasterFiles/_ChargeType/Default.aspx" + stParam);
		}

		protected void cboChargeType_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			if (cboChargeType.Items.Count != 0)
			{
				ChargeType clsChargeType = new ChargeType();
				ChargeTypeDetails clsDetails = clsChargeType.Details(Convert.ToInt32(cboChargeType.SelectedItem.Value));
				clsChargeType.CommitAndDispose();

				txtChargeAmount.Text = clsDetails.ChargeAmount.ToString("#,##0.#0");
				chkInPercent.Checked = Convert.ToBoolean(clsDetails.InPercent);
			}
		}

	}
}

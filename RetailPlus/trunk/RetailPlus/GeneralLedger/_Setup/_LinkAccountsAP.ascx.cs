namespace AceSoft.RetailPlus.GeneralLedger._Setup
{
	using System;
	using System.Data;
	using System.Drawing;
	using System.Web;
	using System.Web.UI.WebControls;
	using System.Web.UI.HtmlControls;
	using AceSoft.RetailPlus.Data;
	
	public partial  class __LinkAccountsAP : System.Web.UI.UserControl
	{
		
		protected void Page_Load(object sender, System.EventArgs e)
		{
			if (!IsPostBack)
			{
				if (Visible)
				{
					lblReferrer.Text = Request.UrlReferrer == null ? Constants.ROOT_DIRECTORY : Request.UrlReferrer.ToString();
					LoadOptions();	
					LoadRecord();	
				}
			}
		}

		private void LoadOptions()
		{
            ChartOfAccount clsChartOfAccount = new ChartOfAccount();
            System.Data.DataTable dtList = clsChartOfAccount.ListAsDataTable("ChartOfAccountID", SortOption.Ascending);
            clsChartOfAccount.CommitAndDispose();

            cboChartOfAccountAPTracking.DataTextField = "ChartOfAccountNameComplete";
            cboChartOfAccountAPTracking.DataValueField = "ChartOfAccountID";
            cboChartOfAccountAPTracking.DataSource = dtList.DefaultView;
            cboChartOfAccountAPTracking.DataBind();
            cboChartOfAccountAPTracking.SelectedIndex = cboChartOfAccountAPTracking.Items.Count - 1;

            cboChartOfAccountAPBills.DataTextField = "ChartOfAccountNameComplete";
            cboChartOfAccountAPBills.DataValueField = "ChartOfAccountID";
            cboChartOfAccountAPBills.DataSource = dtList.DefaultView;
            cboChartOfAccountAPBills.DataBind();
            cboChartOfAccountAPBills.SelectedIndex = cboChartOfAccountAPBills.Items.Count - 1;

            cboChartOfAccountAPFreight.DataTextField = "ChartOfAccountNameComplete";
            cboChartOfAccountAPFreight.DataValueField = "ChartOfAccountID";
            cboChartOfAccountAPFreight.DataSource = dtList.DefaultView;
            cboChartOfAccountAPFreight.DataBind();
            cboChartOfAccountAPFreight.Items.Add(new ListItem("Not Applicable","0"));
            cboChartOfAccountAPFreight.SelectedIndex = cboChartOfAccountAPFreight.Items.Count - 1;

            cboChartOfAccountAPVDeposit.DataTextField = "ChartOfAccountNameComplete";
            cboChartOfAccountAPVDeposit.DataValueField = "ChartOfAccountID";
            cboChartOfAccountAPVDeposit.DataSource = dtList.DefaultView;
            cboChartOfAccountAPVDeposit.DataBind();
            cboChartOfAccountAPVDeposit.Items.Add(new ListItem("Not Applicable", "0"));
            cboChartOfAccountAPVDeposit.SelectedIndex = cboChartOfAccountAPVDeposit.Items.Count - 1;

            cboChartOfAccountAPContra.DataTextField = "ChartOfAccountNameComplete";
            cboChartOfAccountAPContra.DataValueField = "ChartOfAccountID";
            cboChartOfAccountAPContra.DataSource = dtList.DefaultView;
            cboChartOfAccountAPContra.DataBind();
            cboChartOfAccountAPContra.Items.Add(new ListItem("Not Applicable", "0"));
            cboChartOfAccountAPContra.SelectedIndex = cboChartOfAccountAPContra.Items.Count - 1;

            cboChartOfAccountAPLatePayment.DataTextField = "ChartOfAccountNameComplete";
            cboChartOfAccountAPLatePayment.DataValueField = "ChartOfAccountID";
            cboChartOfAccountAPLatePayment.DataSource = dtList.DefaultView;
            cboChartOfAccountAPLatePayment.DataBind();
            cboChartOfAccountAPLatePayment.Items.Add(new ListItem("Not Applicable", "0"));
            cboChartOfAccountAPLatePayment.SelectedIndex = cboChartOfAccountAPLatePayment.Items.Count - 1;
		}

		private void LoadRecord()
		{
			ERPConfig clsERPConfig = new ERPConfig();
			APLinkConfigDetails clsDetails = clsERPConfig.APLinkDetails();

			clsERPConfig.CommitAndDispose();

            cboChartOfAccountAPTracking.SelectedIndex = cboChartOfAccountAPTracking.Items.IndexOf(cboChartOfAccountAPTracking.Items.FindByValue(clsDetails.ChartOfAccountIDAPTracking.ToString()));
            cboChartOfAccountAPBills.SelectedIndex = cboChartOfAccountAPBills.Items.IndexOf(cboChartOfAccountAPBills.Items.FindByValue(clsDetails.ChartOfAccountIDAPBills.ToString()));
            cboChartOfAccountAPFreight.SelectedIndex = cboChartOfAccountAPFreight.Items.IndexOf(cboChartOfAccountAPFreight.Items.FindByValue(clsDetails.ChartOfAccountIDAPFreight.ToString()));
            cboChartOfAccountAPVDeposit.SelectedIndex = cboChartOfAccountAPVDeposit.Items.IndexOf(cboChartOfAccountAPVDeposit.Items.FindByValue(clsDetails.ChartOfAccountIDAPVDeposit.ToString()));
            cboChartOfAccountAPContra.SelectedIndex = cboChartOfAccountAPContra.Items.IndexOf(cboChartOfAccountAPContra.Items.FindByValue(clsDetails.ChartOfAccountIDAPContra.ToString()));
            cboChartOfAccountAPLatePayment.SelectedIndex = cboChartOfAccountAPLatePayment.Items.IndexOf(cboChartOfAccountAPLatePayment.Items.FindByValue(clsDetails.ChartOfAccountIDAPLatePayment.ToString()));

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
			this.imgSaveBack.Click += new System.Web.UI.ImageClickEventHandler(this.imgSaveBack_Click);
			this.imgCancel.Click += new System.Web.UI.ImageClickEventHandler(this.imgCancel_Click);

		}
		#endregion

		private void SaveRecord()
		{

            APLinkConfigDetails clsDetails = new APLinkConfigDetails();

            clsDetails.ChartOfAccountIDAPTracking = Convert.ToInt32(cboChartOfAccountAPTracking.SelectedItem.Value);
            clsDetails.ChartOfAccountIDAPBills = Convert.ToInt32(cboChartOfAccountAPBills.SelectedItem.Value);
            clsDetails.ChartOfAccountIDAPFreight = Convert.ToInt32(cboChartOfAccountAPFreight.SelectedItem.Value);
            clsDetails.ChartOfAccountIDAPVDeposit = Convert.ToInt32(cboChartOfAccountAPVDeposit.SelectedItem.Value);
            clsDetails.ChartOfAccountIDAPContra = Convert.ToInt32(cboChartOfAccountAPContra.SelectedItem.Value);
            clsDetails.ChartOfAccountIDAPLatePayment = Convert.ToInt32(cboChartOfAccountAPLatePayment.SelectedItem.Value);

			ERPConfig clsERPConfig = new ERPConfig();
			clsERPConfig.UpdateAPLinkConfig(clsDetails);

			clsERPConfig.CommitAndDispose();
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

	}
}

namespace AceSoft.RetailPlus.MasterFiles._ProductGroup._UnitsMatrix
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
            if (!IsPostBack && Visible)
			{
				lblReferrer.Text = Request.UrlReferrer == null ? Constants.ROOT_DIRECTORY : Request.UrlReferrer.ToString();
				LoadOptions();
				LoadRecord();
			}
		}


		#endregion

		#region Web Form Designer generated code

		override protected void OnInit(EventArgs e)
		{
			InitializeComponent();
			base.OnInit(e);
		}
		private void InitializeComponent()
		{

		}

		#endregion

		#region Web Control Methods

        protected void imgSave_Click(object sender, System.Web.UI.ImageClickEventArgs e)
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
        protected void imgSaveBack_Click(object sender, System.Web.UI.ImageClickEventArgs e)
		{
			SaveRecord();
			Response.Redirect(lblReferrer.Text);
		}
		protected void cmdSaveBack_Click(object sender, System.EventArgs e)
		{
			SaveRecord();
			Response.Redirect(lblReferrer.Text);
		}
        protected void imgCancel_Click(object sender, System.Web.UI.ImageClickEventArgs e)
		{
			Response.Redirect(lblReferrer.Text);
		}
		protected void cmdCancel_Click(object sender, System.EventArgs e)
		{
			Response.Redirect(lblReferrer.Text);
		}
        protected void imgAdd_Click(object sender, System.Web.UI.ImageClickEventArgs e)
		{
			string stParam = "?task=" + Common.Encrypt("add",Session.SessionID);			
			Response.Redirect(Constants.ROOT_DIRECTORY + "/MasterFiles/_Unit/Default.aspx" + stParam);
		}

		#endregion

		#region Private Methods

		private void LoadOptions()
		{
			DataClass clsDataClass = new DataClass();
			Int64 iID = Convert.ToInt64(Common.Decrypt(Request.QueryString["id"],Session.SessionID));
			lblMatrixID.Text = iID.ToString();

			Int64 groupid = Convert.ToInt64(Common.Decrypt(Request.QueryString["groupid"],Session.SessionID));
			lblGroupID.Text = groupid.ToString();

			ProductGroupUnitsMatrix clsUnitMatrix = new ProductGroupUnitsMatrix();
			
			cboBottomUnit.DataTextField = "UnitName";
			cboBottomUnit.DataValueField = "UnitID";
			cboBottomUnit.DataSource = clsDataClass.DataReaderToDataTable(clsUnitMatrix.AvailableUnitsForProduct(groupid,"UnitName",SortOption.Ascending)).DefaultView;
			cboBottomUnit.DataBind();
			cboBottomUnit.SelectedIndex = cboBottomUnit.Items.Count - 1;

			clsUnitMatrix.CommitAndDispose();	
		}
		private void LoadRecord()
		{
			Int64 iID = Convert.ToInt32(Common.Decrypt(Request.QueryString["id"],Session.SessionID));
			ProductGroupUnitsMatrix clsUnitMatrix = new ProductGroupUnitsMatrix();
			ProductGroupUnitsMatrixDetails clsDetails = clsUnitMatrix.Details(iID);
			
			lblBaseUnitID.Text = clsDetails.BaseUnitID.ToString();
			txtBaseUnit.Text = Convert.ToString(clsDetails.BaseUnitName);
			txtBaseUnitValue.Text = clsDetails.BaseUnitValue.ToString();
			
			//			cboBottomUnit.Items.RemoveAt( cboBottomUnit.Items.IndexOf(cboBottomUnit.Items.FindByValue(lblBaseUnitID.Text)));
			cboBottomUnit.SelectedIndex = cboBottomUnit.Items.IndexOf(cboBottomUnit.Items.FindByValue(clsDetails.BottomUnitID.ToString()));
			txtBottomUnitValue.Text = clsDetails.BottomUnitValue.ToString();

			clsUnitMatrix.CommitAndDispose();

		}
		private void SaveRecord()
		{
			ProductGroupUnitsMatrix clsUnitMatrix = new ProductGroupUnitsMatrix();
			ProductGroupUnitsMatrixDetails clsDetails = new ProductGroupUnitsMatrixDetails();

			clsDetails.MatrixID = Convert.ToInt64(lblMatrixID.Text);
			clsDetails.BaseUnitID = Convert.ToInt32(lblBaseUnitID.Text);
			clsDetails.BaseUnitValue = Convert.ToDecimal(txtBaseUnitValue.Text);
			clsDetails.BottomUnitID = Convert.ToInt32(cboBottomUnit.SelectedItem.Value);
			clsDetails.BottomUnitValue = Convert.ToDecimal(txtBottomUnitValue.Text);
			clsUnitMatrix.Update(clsDetails);
			
			clsUnitMatrix.CommitAndDispose();
		}

		#endregion

    }
}

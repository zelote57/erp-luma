namespace AceSoft.RetailPlus.MasterFiles._ProductSubGroup._UnitsMatrix
{
	using System;
	using System.Data;
	using System.Drawing;
	using System.Web;
	using System.Web.UI.WebControls;
	using System.Web.UI.HtmlControls;
	using AceSoft.RetailPlus.Data;
	
	public partial  class __Insert : System.Web.UI.UserControl
	{

		#region Web Form Methods

		protected void Page_Load(object sender, System.EventArgs e)
		{
			if (!IsPostBack)
			{
				lblReferrer.Text = Request.UrlReferrer.ToString();
				if (Visible)
					LoadOptions();			
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
			Response.Redirect("/RetailPlus/MasterFiles/_Unit/Default.aspx" + stParam);
		}

		#endregion

		#region Private Methods

		private void LoadOptions()
		{
			DataClass clsDataClass = new DataClass();
			int subgroupid = Convert.ToInt32(Common.Decrypt(Request.QueryString["subgroupid"].ToString(),Session.SessionID));
			lblSubGroupID.Text = Convert.ToString(subgroupid);
			
			ProductSubGroupUnitsMatrix clsUnitMatrix = new ProductSubGroupUnitsMatrix();
			ProductSubGroupUnitsMatrixDetails clsUnitDetails = clsUnitMatrix.LastDetails(subgroupid);

			if (clsUnitDetails.BottomUnitName == null)
			{
				ProductSubGroup clsProductSubGroup = new ProductSubGroup();
				ProductSubGroupDetails clsDetails = clsProductSubGroup.Details(subgroupid);
				clsProductSubGroup.CommitAndDispose();

				txtBaseUnit.Text = clsDetails.BaseUnitName;
				lblBaseUnitID.Text = Convert.ToString(clsDetails.BaseUnitID);
			}
			else
			{
				txtBaseUnit.Text = clsUnitDetails.BottomUnitName;
				lblBaseUnitID.Text = Convert.ToString(clsUnitDetails.BottomUnitID);
			}

			cboBottomUnit.DataTextField = "UnitName";
			cboBottomUnit.DataValueField = "UnitID";
			cboBottomUnit.DataSource = clsDataClass.DataReaderToDataTable(clsUnitMatrix.AvailableUnitsForProduct(subgroupid,"UnitName",SortOption.Ascending)).DefaultView;
			cboBottomUnit.DataBind();
			if (cboBottomUnit.Items.Contains( new ListItem(txtBaseUnit.Text, lblBaseUnitID.Text)))
			{
				cboBottomUnit.Items.RemoveAt( cboBottomUnit.Items.IndexOf(cboBottomUnit.Items.FindByValue(lblBaseUnitID.Text)));
			}
			cboBottomUnit.SelectedIndex = cboBottomUnit.Items.Count - 1;

			clsUnitMatrix.CommitAndDispose();
		}
		private Int64 SaveRecord()
		{
			ProductSubGroupUnitsMatrix clsUnitMatrix = new ProductSubGroupUnitsMatrix();
			ProductSubGroupUnitsMatrixDetails clsDetails = new ProductSubGroupUnitsMatrixDetails();

			clsDetails.SubGroupID = Convert.ToInt32(lblSubGroupID.Text);
			clsDetails.BaseUnitID = Convert.ToInt32(lblBaseUnitID.Text);
			clsDetails.BaseUnitValue = Convert.ToDecimal(txtBaseUnitValue.Text);
			clsDetails.BottomUnitID = Convert.ToInt32(cboBottomUnit.SelectedItem.Value);
			clsDetails.BottomUnitValue = Convert.ToDecimal(txtBottomUnitValue.Text);
			Int64 id = clsUnitMatrix.Insert(clsDetails);
			
			clsUnitMatrix.CommitAndDispose();

			return id;
		}


		#endregion

}
}

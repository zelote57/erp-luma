namespace AceSoft.RetailPlus.MasterFiles._Product._UnitsMatrix
{
	using System;
	using System.Data;
	using System.Drawing;
	using System.Web;
	using System.Web.UI.WebControls;
	using System.Web.UI.HtmlControls;
	using AceSoft.RetailPlus.Data;
	
	/// <summary>
	///		Summary description for __Insert.
	/// </summary>
	public partial  class __Update : System.Web.UI.UserControl
	{
		protected System.Web.UI.WebControls.RequiredFieldValidator Requiredfieldvalidator1;

		#region Web Form Methods

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

		
		#endregion

		#region Private Methods

		private void LoadOptions()
		{
            Int64 iID = Convert.ToInt64(Common.Decrypt(Request.QueryString["id"], Session.SessionID));
			lblMatrixID.Text = iID.ToString();

			Int64 prodID = Convert.ToInt64(Common.Decrypt(Request.QueryString["prodid"],Session.SessionID));
			lblProductID.Text = prodID.ToString();

			ProductUnitsMatrix clsUnitMatrix = new ProductUnitsMatrix();
			
			cboBottomUnit.DataTextField = "UnitName";
			cboBottomUnit.DataValueField = "UnitID";
			cboBottomUnit.DataSource = clsUnitMatrix.AvailableUnitsForProduct(prodID).DefaultView;
			cboBottomUnit.DataBind();
			cboBottomUnit.SelectedIndex = cboBottomUnit.Items.Count - 1;

			clsUnitMatrix.CommitAndDispose();	
		}

		private void LoadRecord()
		{
			long iID = Convert.ToInt64(Common.Decrypt(Request.QueryString["id"],Session.SessionID));
			ProductUnitsMatrix clsUnitMatrix = new ProductUnitsMatrix();
			ProductUnitsMatrixDetails clsDetails = clsUnitMatrix.Details(iID);
			
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
			ProductUnitsMatrix clsUnitMatrix = new ProductUnitsMatrix();
			ProductUnitsMatrixDetails clsDetails = new ProductUnitsMatrixDetails();

			clsDetails.MatrixID = Convert.ToInt64(lblMatrixID.Text);
            clsDetails.ProductID = Convert.ToInt64(lblProductID.Text);
			clsDetails.BaseUnitID = Convert.ToInt16(lblBaseUnitID.Text);
			clsDetails.BaseUnitValue = Convert.ToDecimal(txtBaseUnitValue.Text);
			clsDetails.BottomUnitID = Convert.ToInt16(cboBottomUnit.SelectedItem.Value);
			clsDetails.BottomUnitValue = Convert.ToDecimal(txtBottomUnitValue.Text);
			clsUnitMatrix.Update(clsDetails);
			
			clsUnitMatrix.CommitAndDispose();
		}


		#endregion
    }
}

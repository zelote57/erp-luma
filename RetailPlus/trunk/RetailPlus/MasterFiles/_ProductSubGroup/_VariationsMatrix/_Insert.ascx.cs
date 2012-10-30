namespace AceSoft.RetailPlus.MasterFiles._ProductSubGroup._VariationsMatrix
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
			//
			// CODEGEN: This call is required by the ASP.NET Web Form Designer.
			//
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
			string stParam = "?task=" + Common.Encrypt("add",Session.SessionID) + "&subgroupid=" + Common.Encrypt(lblSubGroupID.Text,Session.SessionID);
			Response.Redirect("Default.aspx" + stParam);
		}
		protected void cmdSave_Click(object sender, System.EventArgs e)
		{
			SaveRecord();
			string stParam = "?task=" + Common.Encrypt("add",Session.SessionID) + "&subgroupid=" + Common.Encrypt(lblSubGroupID.Text,Session.SessionID);
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
        protected void lstItem_ItemDataBound(object sender, DataListItemEventArgs e)
		{
			if(e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
			{
				DataRowView dr = (DataRowView) e.Item.DataItem;				

				HtmlInputCheckBox chkList = (HtmlInputCheckBox) e.Item.FindControl("chkList");
				chkList.Value = dr["VariationID"].ToString();

                HyperLink lnkVariationType = (HyperLink)e.Item.FindControl("lnkVariationType");
                lnkVariationType.Text = dr["VariationType"].ToString(); //VariationID
                lnkVariationType.NavigateUrl = Constants.ROOT_DIRECTORY + "/MasterFiles/_Variation/Default.aspx?task=" + Common.Encrypt("details", Session.SessionID) + "&id=" + Common.Encrypt(dr["VariationID"].ToString(), Session.SessionID);

			}
		}
	
		#endregion

		#region Private Methods

		private void LoadOptions()
		{
			DataClass clsDataClass = new DataClass();

			lblSubGroupID.Text = Common.Decrypt((string)Request.QueryString["subgroupid"],Session.SessionID);

			ProductSubGroupVariation clsProductSubGroupVariation = new ProductSubGroupVariation();
			lstItem.DataSource = clsDataClass.DataReaderToDataTable(clsProductSubGroupVariation.List(Convert.ToInt32(lblSubGroupID.Text),"VariationType",SortOption.Ascending)).DefaultView;
			lstItem.DataBind();
			clsProductSubGroupVariation.CommitAndDispose();	

			UnitMeasurements clsUnit = new UnitMeasurements();
			cboUnit.DataTextField = "UnitName";
			cboUnit.DataValueField = "UnitID";
			cboUnit.DataSource = clsDataClass.DataReaderToDataTable(clsUnit.List("UnitName",SortOption.Ascending)).DefaultView;
			cboUnit.DataBind();
			cboUnit.SelectedIndex = cboUnit.Items.Count - 1;
			clsUnit.CommitAndDispose();	

			ProductSubGroup clsProductSubGroup = new ProductSubGroup();
			ProductSubGroupDetails clsProductSubGroupDetails = clsProductSubGroup.Details(Convert.ToInt32(lblSubGroupID.Text));
			clsProductSubGroup.CommitAndDispose();

			cboUnit.SelectedIndex = cboUnit.Items.IndexOf(cboUnit.Items.FindByValue(clsProductSubGroupDetails.BaseUnitID.ToString()));
			txtProductPrice.Text = clsProductSubGroupDetails.Price.ToString("#,##0.#0");
			txtProductPrice.Text = clsProductSubGroupDetails.Price.ToString("#,##0.#0");
			txtPurchasePrice.Text = clsProductSubGroupDetails.PurchasePrice.ToString("#,##0.#0");
			if (clsProductSubGroupDetails.IncludeInSubtotalDiscount == 0)
				chkIncludeInSubtotalDiscount.Checked = false;
			else
				chkIncludeInSubtotalDiscount.Checked = true;
			txtVAT.Text = clsProductSubGroupDetails.VAT.ToString("#,##0.#0");
			txtEVAT.Text = clsProductSubGroupDetails.EVAT.ToString("#,##0.#0");
			txtLocalTax.Text = clsProductSubGroupDetails.LocalTax.ToString("#,##0.#0");
			
		}
		private bool SaveRecord()
		{
			ProductSubGroupBaseMatrixDetails clsBaseDetails = new ProductSubGroupBaseMatrixDetails();
			ProductSubGroupVariationsMatrixDetails clsDetails;
			ProductSubGroupVariationsMatrix clsProductSubGroupVariationsMatrix = new ProductSubGroupVariationsMatrix();

			clsBaseDetails.SubGroupID = Convert.ToInt64(lblSubGroupID.Text);
			clsBaseDetails.Description = "";
			clsBaseDetails.UnitID = Convert.ToInt32(cboUnit.SelectedItem.Value);
			clsBaseDetails.Price = Convert.ToDecimal(txtProductPrice.Text);
			clsBaseDetails.PurchasePrice = Convert.ToDecimal(txtPurchasePrice.Text);
			clsBaseDetails.IncludeInSubtotalDiscount = Convert.ToInt16(chkIncludeInSubtotalDiscount.Checked);
			clsBaseDetails.VAT = Convert.ToDecimal(txtVAT.Text);
			clsBaseDetails.EVAT = Convert.ToDecimal(txtEVAT.Text);
			clsBaseDetails.LocalTax = Convert.ToDecimal(txtLocalTax.Text);
			clsBaseDetails.MatrixID = clsProductSubGroupVariationsMatrix.InsertBaseVariation(clsBaseDetails);
			
			string stringVariationDesc = null;

			foreach (DataListItem item in lstItem.Items)
			{
				HtmlInputCheckBox chkList = (HtmlInputCheckBox) item.FindControl("chkList");
				TextBox txtDescription = (TextBox) item.FindControl("txtDescription");

				clsDetails = new ProductSubGroupVariationsMatrixDetails();
				clsDetails.MatrixID = clsBaseDetails.MatrixID;
				clsDetails.SubGroupID = Convert.ToInt64(lblSubGroupID.Text);
				clsDetails.VariationID = Convert.ToInt32(chkList.Value);
				clsDetails.Description = txtDescription.Text;
				
				clsProductSubGroupVariationsMatrix.InsertVariation(clsDetails);

                HyperLink lnkVariationType = (HyperLink)item.FindControl("lnkVariationType");
				stringVariationDesc += txtDescription.Text + "; ";
				
			}
			
			//Insert as single description 
			clsBaseDetails.Description = stringVariationDesc;
			clsProductSubGroupVariationsMatrix.UpdateVariationDesc(clsBaseDetails);

			clsProductSubGroupVariationsMatrix.CommitAndDispose();

			return true;
		}

		#endregion

    }
}

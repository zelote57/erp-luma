namespace AceSoft.RetailPlus.MasterFiles._ProductSubGroup._VariationsMatrix
{
	using System;
	using System.Data;
	using System.Drawing;
	using System.Web;
	using System.Web.UI.WebControls;
	using System.Web.UI.HtmlControls;
	using AceSoft.RetailPlus.Data;
	
	public partial  class __Details : System.Web.UI.UserControl
	{
		protected System.Web.UI.WebControls.DropDownList cboVariationType;
		
		#region Web Form Methods

		protected void Page_Load(object sender, System.EventArgs e)
		{
			if (!IsPostBack)
			{
				lblReferrer.Text = Request.UrlReferrer == null ? Constants.ROOT_DIRECTORY : Request.UrlReferrer.ToString();
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
			InitializeComponent();
			base.OnInit(e);
		}
		private void InitializeComponent()
		{

		}
		#endregion

		#region Web Control Methods

        protected void imgBack_Click(object sender, System.Web.UI.ImageClickEventArgs e)
        {
            Response.Redirect(lblReferrer.Text);
        }
        protected void cmdBack_Click(object sender, System.EventArgs e)
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
				
				ProductSubGroupVariationsMatrix clsProductSubGroupVariationsMatrix = new ProductSubGroupVariationsMatrix();
	
				TextBox txtDescription = (TextBox) e.Item.FindControl("txtDescription");
				txtDescription.Text = clsProductSubGroupVariationsMatrix.Details(Convert.ToInt32(lblMatrixID.Text), Convert.ToInt32(chkList.Value)).Description;
				
				clsProductSubGroupVariationsMatrix.CommitAndDispose();

			}
		}

		#endregion

		#region Private Methods

		private void LoadOptions()
		{
			DataClass clsDataClass = new DataClass();

			lblSubGroupID.Text = Common.Decrypt((string)Request.QueryString["subgroupid"],Session.SessionID);
			lblMatrixID.Text = Common.Decrypt(Request.QueryString["id"],Session.SessionID);

			ProductSubGroupVariations clsProductSubGroupVariation = new ProductSubGroupVariations();
			lstItem.DataSource = clsDataClass.DataReaderToDataTable(clsProductSubGroupVariation.List(Convert.ToInt32(lblSubGroupID.Text),"VariationType",SortOption.Ascending)).DefaultView;
			lstItem.DataBind();
			clsProductSubGroupVariation.CommitAndDispose();

            Data.Unit clsUnit = new Data.Unit();
			cboUnit.DataTextField = "UnitName";
			cboUnit.DataValueField = "UnitID";
			cboUnit.DataSource = clsUnit.ListAsDataTable(SortField:"UnitName").DefaultView;
			cboUnit.DataBind();
			cboUnit.SelectedIndex = cboUnit.Items.Count - 1;
			clsUnit.CommitAndDispose();	
		}
		private void LoadRecord()
		{
			ProductSubGroupVariationsMatrix clsProductSubGroupVariationsMatrix = new ProductSubGroupVariationsMatrix();
			ProductSubGroupBaseVariationsMatrixDetails clsBaseDetails = clsProductSubGroupVariationsMatrix.BaseDetails(Convert.ToInt32(lblMatrixID.Text), Convert.ToInt32(lblSubGroupID.Text));
			clsProductSubGroupVariationsMatrix.CommitAndDispose();

			cboUnit.SelectedIndex = cboUnit.Items.IndexOf(cboUnit.Items.FindByValue(clsBaseDetails.UnitID.ToString()));
			txtProductPrice.Text = clsBaseDetails.Price.ToString("#,##0.#0");
			txtPurchasePrice.Text = clsBaseDetails.PurchasePrice.ToString("#,##0.#0");
            chkIncludeInSubtotalDiscount.Checked = clsBaseDetails.IncludeInSubtotalDiscount;
			txtVAT.Text = clsBaseDetails.VAT.ToString("#,##0.#0");
			txtEVAT.Text = clsBaseDetails.EVAT.ToString("#,##0.#0");
			txtLocalTax.Text = clsBaseDetails.LocalTax.ToString("#,##0.#0");
			
		}
		private bool SaveRecord()
		{
			ProductSubGroupVariationsMatrixDetails clsDetails;
			ProductSubGroupVariationsMatrix clsProductSubGroupVariationsMatrix = new ProductSubGroupVariationsMatrix();

			string stringVariationDesc = null;

			foreach (DataListItem item in lstItem.Items)
			{
				HtmlInputCheckBox chkList = (HtmlInputCheckBox) item.FindControl("chkList");
				TextBox txtDescription = (TextBox) item.FindControl("txtDescription");
				
				clsDetails = new ProductSubGroupVariationsMatrixDetails();
				clsDetails.MatrixID = Convert.ToInt64(lblMatrixID.Text);
				clsDetails.SubGroupID = Convert.ToInt64(lblSubGroupID.Text);
				clsDetails.VariationID = Convert.ToInt32(chkList.Value);
				clsDetails.Description = txtDescription.Text;
				
				if (clsProductSubGroupVariationsMatrix.IsVariationExists(clsDetails.MatrixID, clsDetails.VariationID) == false)
				{
					clsProductSubGroupVariationsMatrix.InsertVariation(clsDetails);
				}
				else
				{
					clsProductSubGroupVariationsMatrix.Update(clsDetails);
				}

                HyperLink lnkVariationType = (HyperLink)item.FindControl("lnkVariationType");
                stringVariationDesc += txtDescription.Text + "; ";
			}
			
			//update base variation matrix
			ProductSubGroupBaseVariationsMatrixDetails clsBaseDetails = new ProductSubGroupBaseVariationsMatrixDetails();
			clsBaseDetails.MatrixID = Convert.ToInt64(lblMatrixID.Text);
			clsBaseDetails.SubGroupID = Convert.ToInt64(lblSubGroupID.Text);
			clsBaseDetails.Description = stringVariationDesc;
			clsBaseDetails.UnitID = Convert.ToInt32(cboUnit.SelectedItem.Value);
			clsBaseDetails.Price = Convert.ToDecimal(txtProductPrice.Text);
			clsBaseDetails.PurchasePrice = Convert.ToDecimal(txtPurchasePrice.Text);
			clsBaseDetails.IncludeInSubtotalDiscount = chkIncludeInSubtotalDiscount.Checked;
			clsBaseDetails.VAT = Convert.ToDecimal(txtVAT.Text);
			clsBaseDetails.EVAT = Convert.ToDecimal(txtEVAT.Text);
			clsBaseDetails.LocalTax = Convert.ToDecimal(txtLocalTax.Text);
			clsProductSubGroupVariationsMatrix.UpdateBaseVariation(clsBaseDetails);

			clsProductSubGroupVariationsMatrix.CommitAndDispose();

			return true;
		}

		#endregion
       
}
}

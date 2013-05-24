namespace AceSoft.RetailPlus.MasterFiles._Product
{
	using System;
	using System.Data;
	using System.Drawing;
	using System.Web;
	using System.Web.UI.WebControls;
	using System.Web.UI.HtmlControls;
	using AceSoft.RetailPlus.Data;
	
	public partial  class __Compose : System.Web.UI.UserControl
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
					LoadItems();
					cmdDelete.Attributes.Add("onClick", "return confirm_delete();");
					imgDelete.Attributes.Add("onClick", "return confirm_delete();");
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
			this.cmdProductCode.Click += new System.Web.UI.ImageClickEventHandler(this.cmdProductCode_Click);
			this.cmdVariationSearch.Click += new System.Web.UI.ImageClickEventHandler(this.cmdVariationSearch_Click);
			this.imgClear.Click += new System.Web.UI.ImageClickEventHandler(this.imgClear_Click);
			this.imgSave.Click += new System.Web.UI.ImageClickEventHandler(this.imgSave_Click);
			this.imgDelete.Click += new System.Web.UI.ImageClickEventHandler(this.imgDelete_Click);
			this.imgEdit.Click += new System.Web.UI.ImageClickEventHandler(this.imgEdit_Click);
			this.imgCancel.Click += new System.Web.UI.ImageClickEventHandler(this.imgCancel_Click);
			this.lstItem.ItemDataBound += new System.Web.UI.WebControls.DataListItemEventHandler(this.lstItem_ItemDataBound);

		}
		#endregion

		#region Web Control Methods

		private void imgSave_Click(object sender, System.Web.UI.ImageClickEventArgs e)
		{
			if (cboProductCode.SelectedItem.Value.ToString() != "0" || cboProductCode.SelectedItem.Value.ToString() != null)
			{
				SaveRecord();
				LoadOptions();
				LoadItems();
			}
		}

		protected void cmdSave_Click(object sender, System.EventArgs e)
		{
			if (cboProductCode.SelectedItem.Value.ToString() != "0" || cboProductCode.SelectedItem.Value.ToString() != null)
			{
				SaveRecord();
				LoadOptions();
				LoadItems();
			}
		}

		private void imgCancel_Click(object sender, System.Web.UI.ImageClickEventArgs e)
		{
			Response.Redirect("Default.aspx?task=" + Common.Encrypt("list",Session.SessionID));
		}

		protected void cmdCancel_Click(object sender, System.EventArgs e)
		{
			Response.Redirect("Default.aspx?task=" + Common.Encrypt("list",Session.SessionID));
		}

		private void imgDelete_Click(object sender, System.Web.UI.ImageClickEventArgs e)
		{
			if (DeleteItems())
				LoadItems();
		}

		protected void cmdDelete_Click(object sender, System.EventArgs e)
		{
			if (DeleteItems())
				LoadItems();
		}

		private void imgEdit_Click(object sender, System.Web.UI.ImageClickEventArgs e)
		{
			UpdateItem();
		}

		protected void cmdEdit_Click(object sender, System.EventArgs e)
		{
			UpdateItem();
		}

		private void imgClear_Click(object sender, System.Web.UI.ImageClickEventArgs e)
		{
			LoadOptions();
		}

		protected void cmdClear_Click(object sender, System.EventArgs e)
		{
			LoadOptions();
		}

		protected void cboProductCode_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			if (cboProductCode.Items.Count ==0)
				return;

			DataClass clsDataClass = new DataClass();
			long ProductID = Convert.ToInt64(cboProductCode.SelectedItem.Value);

			ProductVariationsMatrix clsProductVariationMatrix = new ProductVariationsMatrix();
			cboVariation.DataTextField = "VariationDesc";
			cboVariation.DataValueField = "MatrixID";
			cboVariation.DataSource = clsDataClass.DataReaderToDataTable(clsProductVariationMatrix.BaseList(ProductID, "VariationDesc",SortOption.Ascending)).DefaultView;
			cboVariation.DataBind();

			if (cboVariation.Items.Count == 0)
			{
				cboVariation.Items.Add(new ListItem("No Variation", "0"));
			}
			cboVariation.SelectedIndex = cboVariation.Items.Count - 1;
			clsProductVariationMatrix.CommitAndDispose();

			ProductUnitsMatrix clsUnitMatrix = new ProductUnitsMatrix();

			cboProductUnit.DataTextField = "BottomUnitCode";
			cboProductUnit.DataValueField = "BottomUnitID";
			cboProductUnit.DataSource = clsUnitMatrix.ListAsDataTable(ProductID,"a.MatrixID",SortOption.Ascending).DefaultView;
			cboProductUnit.DataBind();
			clsUnitMatrix.CommitAndDispose();

			Products clsProduct = new Products();
			ProductDetails clsDetails = new ProductDetails();
			clsDetails = clsProduct.Details(ProductID);
			clsProduct.CommitAndDispose();
			cboProductUnit.Items.Add( new ListItem(clsDetails.BaseUnitCode, clsDetails.BaseUnitID.ToString()));

			cboProductUnit.SelectedIndex = cboProductUnit.Items.Count - 1;

			cboVariation_SelectedIndexChanged(null, null);
		}
		protected void cboVariation_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			long VariationMatrixID = Convert.ToInt64(cboVariation.SelectedItem.Value);
			if (VariationMatrixID != 0)
			{
				long ProductID = Convert.ToInt64(cboProductCode.SelectedItem.Value);

				ProductVariationsMatrix clsProductVariationMatrix = new ProductVariationsMatrix();
				ProductBaseMatrixDetails clsProductBaseMatrixDetails = clsProductVariationMatrix.BaseDetails(VariationMatrixID, ProductID);
				clsProductVariationMatrix.CommitAndDispose();
				
			}
		}
		private void cmdProductCode_Click(object sender, System.Web.UI.ImageClickEventArgs e)
		{
			DataClass clsDataClass = new DataClass();

			Data.Products clsProduct = new Data.Products();
			cboProductCode.DataTextField = "ProductCode";
			cboProductCode.DataValueField = "ProductID";

			if (txtProductCode.Text != null)
			{
				string stSearchKey = txtProductCode.Text;
				cboProductCode.DataSource = clsDataClass.DataReaderToDataTable(clsProduct.Search(stSearchKey, "ProductCode", SortOption.Ascending));
			}
			else
			{
				cboProductCode.DataSource = clsDataClass.DataReaderToDataTable(clsProduct.List("ProductCode", SortOption.Ascending)).DefaultView;
			}
			cboProductCode.DataBind();
			clsProduct.CommitAndDispose();
			
			if (cboProductCode.Items.Count == 0)
				cboProductCode.Items.Add(new ListItem("No product", "0"));

			cboProductCode.SelectedIndex = 0;

			cboProductCode_SelectedIndexChanged(null, null);
		}

		private void cmdVariationSearch_Click(object sender, System.Web.UI.ImageClickEventArgs e)
		{
			string stSearchKey = txtVariation.Text.ToString();

			if (txtVariation.Text == null) stSearchKey = "";

			DataClass clsDataClass = new DataClass();
			long ProductID = Convert.ToInt64(cboProductCode.SelectedItem.Value);

			ProductVariationsMatrix clsProductVariationMatrix = new ProductVariationsMatrix();
			cboVariation.DataTextField = "VariationDesc";
			cboVariation.DataValueField = "MatrixID";
			cboVariation.DataSource = clsDataClass.DataReaderToDataTable(clsProductVariationMatrix.Search(ProductID, stSearchKey, "VariationDesc",SortOption.Ascending)).DefaultView;
			cboVariation.DataBind();

			if (cboVariation.Items.Count == 0)
			{
				cboVariation.Items.Add(new ListItem("No Variation", "0"));
			}
			cboVariation.SelectedIndex = cboVariation.Items.Count - 1;
			clsProductVariationMatrix.CommitAndDispose();
		}				

		private void lstItem_ItemDataBound(object sender, DataListItemEventArgs e)
		{
			if(e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
			{
				DataRowView dr = (DataRowView) e.Item.DataItem;				

				HtmlInputCheckBox chkList = (HtmlInputCheckBox) e.Item.FindControl("chkList");
				chkList.Value = dr["CompositionID"].ToString();

				HyperLink lnkDescription = (HyperLink) e.Item.FindControl("lnkDescription");
				lnkDescription.Text = dr["ProductDesc"].ToString();
				lnkDescription.NavigateUrl = Constants.ROOT_DIRECTORY + "/MasterFiles/_Product/Default.aspx?task=" + Common.Encrypt("det", Session.SessionID) + "&id=" + Common.Encrypt(dr["ProductID"].ToString(), Session.SessionID);

				HyperLink lnkMatrixDescription = (HyperLink) e.Item.FindControl("lnkMatrixDescription");
				if (dr["MatrixDescription"].ToString() == string.Empty || dr["MatrixDescription"].ToString() == null)
					lnkMatrixDescription.Text = "_";
				else
				{
					lnkMatrixDescription.Text = dr["MatrixDescription"].ToString();
					lnkMatrixDescription.NavigateUrl = Constants.ROOT_DIRECTORY + "/MasterFiles/_Product/_VariationsMatrix/Default.aspx?task=" + Common.Encrypt("det", Session.SessionID) + "&prodid=" + Common.Encrypt(dr["ProductID"].ToString(), Session.SessionID) + "&id=" + Common.Encrypt(dr["VariationMatrixID"].ToString(), Session.SessionID);
				}
				
				Label lblItemQuantity = (Label) e.Item.FindControl("lblItemQuantity");
				lblItemQuantity.Text = Convert.ToDecimal(dr["Quantity"].ToString()).ToString("#,##0.#0");

				Label lblProductUnitID = (Label) e.Item.FindControl("lblProductUnitID");
				lblProductUnitID.Text = dr["UnitID"].ToString();

				Label lblProductUnitCode = (Label) e.Item.FindControl("lblProductUnitCode");
				lblProductUnitCode.Text = dr["UnitCode"].ToString();

				//For anchor
				//				HtmlGenericControl divExpCollAsst = (HtmlGenericControl) e.Item.FindControl("divExpCollAsst");
				//
				//				HtmlAnchor anchorDown = (HtmlAnchor) e.Item.FindControl("anchorDown");
				//				anchorDown.HRef = "javascript:ToggleDiv('" +  divExpCollAsst.ClientID + "')";
			}
		}


		#endregion

		#region Private Methods

		private void LoadOptions()
		{
			cboProductCode.Items.Clear();
			cboVariation.Items.Clear();
			cboProductUnit.Items.Clear();

			cboProductCode.Items.Add(new ListItem("No Product... Enter product, then search.", "0"));

			cboProductCode_SelectedIndexChanged(null, null);

			txtQuantity.Text = "1";
			lblCompositionID.Text = "0";
		}

		private void LoadRecord()
		{
			Int64 iID = Convert.ToInt64(Common.Decrypt(Request.QueryString["id"],Session.SessionID));
			Products clsProduct = new Products();
            ProductDetails clsDetails = clsProduct.Details(iID);

			Contacts clsContact = new Contacts(clsProduct.Connection, clsProduct.Transaction);
			ContactDetails clsContactDetails = clsContact.Details(clsDetails.SupplierID);

			clsProduct.CommitAndDispose();

			lblProductID.Text = clsDetails.ProductID.ToString();
			lblQuantity.Text = clsDetails.Quantity.ToString("#,##0.#0");
			lblUnitCode.Text = clsDetails.BaseUnitCode;
			lblProductCode.Text = clsDetails.ProductCode;
			lblBarcode.Text = clsDetails.BarCode;
			lblProductDesc.Text = clsDetails.ProductDesc;
			lblProductGroup.Text = clsDetails.ProductGroupCode + "/" + clsDetails.ProductGroupName;
			lblProductSubGroup.Text = clsDetails.ProductSubGroupCode + "/" + clsDetails.ProductSubGroupName;

			lblSupplierCode.Text = clsContactDetails.ContactCode.ToString();
			string stParam = "?task=" + Common.Encrypt("details",Session.SessionID) + "&id=" + Common.Encrypt(clsDetails.SupplierID.ToString(),Session.SessionID);	
			lblSupplierCode.NavigateUrl = Constants.ROOT_DIRECTORY + "/MasterFiles/_Contact/Default.aspx" + stParam;

			lblSupplierContact.Text = clsContactDetails.BusinessName;
			lblSupplierTelephoneNo.Text = clsContactDetails.TelephoneNo;
			
		}

		private void SaveRecord()
		{
			ProductCompositionDetails clsDetails = new ProductCompositionDetails();

			clsDetails.MainProductID = Convert.ToInt64(lblProductID.Text);
			clsDetails.ProductID = Convert.ToInt64(cboProductCode.SelectedItem.Value);
			clsDetails.VariationMatrixID = Convert.ToInt64(cboVariation.SelectedItem.Value);
			clsDetails.Quantity = Convert.ToDecimal(txtQuantity.Text);
			clsDetails.UnitID = Convert.ToInt32(cboProductUnit.SelectedItem.Value);

			ProductComposition clsProductComposition = new ProductComposition();
			if (lblCompositionID.Text != "0")
			{
				clsDetails.CompositionID = Convert.ToInt64(lblCompositionID.Text);
				clsProductComposition.Update(clsDetails);
			}
			else
				clsProductComposition.Insert(clsDetails);
			
			clsProductComposition.CommitAndDispose();

		}

		private void ClearAddItem()
		{
			txtQuantity.Text = "1";
		}
		private bool DeleteItems()
		{
			bool boRetValue = false;
			string stIDs = "";

			foreach(DataListItem item in lstItem.Items)
			{
				HtmlInputCheckBox chkList = (HtmlInputCheckBox) item.FindControl("chkList");
				if (chkList!=null)
				{
					if (chkList.Checked == true)
					{
						stIDs += chkList.Value + ",";		
						boRetValue = true;
					}
				}
			}
			if (boRetValue)
			{
				ProductComposition clsProductComposition = new ProductComposition();
				clsProductComposition.Delete( stIDs.Substring(0,stIDs.Length-1));

				clsProductComposition.CommitAndDispose();
			}

			return boRetValue;
		}
		private void UpdateItem()
		{
			if (isChkListSingle() == true)
			{
				string stID = GetFirstID();
				if (stID!=null)
				{
					ProductComposition clsProductComposition = new ProductComposition();
					ProductCompositionDetails clsDetails = clsProductComposition.Details(Convert.ToInt64(stID));
					clsProductComposition.CommitAndDispose();

					cboProductCode.Items.Clear();
					cboVariation.Items.Clear();
					cboProductUnit.Items.Clear();

					cboProductCode.Items.Add(new ListItem(clsDetails.ProductCode, clsDetails.ProductID.ToString()));
					cboProductCode.SelectedIndex = 0;
					if (clsDetails.VariationMatrixID == 0)
					{	cboVariation.Items.Add(new ListItem("No Variation", "0"));	}
					else
					{	cboVariation.Items.Add(new ListItem(clsDetails.MatrixDescription, clsDetails.VariationMatrixID.ToString()));	}
					cboVariation.SelectedIndex = 0;
					cboProductUnit.Items.Add(new ListItem(clsDetails.UnitCode, clsDetails.UnitID.ToString()));
					cboProductUnit.SelectedIndex = 0;
					txtQuantity.Text = clsDetails.Quantity.ToString("###0.#0");
					lblCompositionID.Text = stID;
				}
			}
			else
			{
				string stScript = "<Script>";
				stScript += "window.alert('Cannot update more than one record. Please select at least one record to update.')";
				stScript += "</Script>";
				Response.Write(stScript);	
			}
		}

		private void LoadItems()
		{
			DataClass clsDataClass = new DataClass();

			ProductComposition clsProductComposition = new ProductComposition();
			lstItem.DataSource = clsDataClass.DataReaderToDataTable(clsProductComposition.List(Convert.ToInt64(lblProductID.Text), "CompositionID",SortOption.Ascending)).DefaultView;
			lstItem.DataBind();
			clsProductComposition.CommitAndDispose();
		}

		private string GetFirstID()
		{
			foreach(DataListItem item in lstItem.Items)
			{
				HtmlInputCheckBox chkList = (HtmlInputCheckBox) item.FindControl("chkList");
				if (chkList!=null)
				{
					if (chkList.Checked == true)
					{
						return chkList.Value;
					}
				}
			}
			return null;
		}
		private bool isChkListSingle()
		{
			bool boChkListSingle = true;
			int iCount = 0;
			
			foreach(DataListItem item in lstItem.Items)
			{
				HtmlInputCheckBox chkList = (HtmlInputCheckBox) item.FindControl("chkList");
				if (chkList!=null)
				{
					if (chkList.Checked == true)
					{
						iCount += 1;
						if (iCount >= 2)
						{	return false;	}
					}
				}
			}
			return boChkListSingle;
		}


		#endregion

	}
}
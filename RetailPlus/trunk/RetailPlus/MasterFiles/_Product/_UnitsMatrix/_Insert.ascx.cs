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
	public partial  class __Insert : System.Web.UI.UserControl
	{
		protected System.Web.UI.WebControls.RequiredFieldValidator Requiredfieldvalidator1;

		#region Web Form Methods

		protected void Page_Load(object sender, System.EventArgs e)
		{
			if (!IsPostBack)
			{
				if (Visible)
				{
					lblReferrer.Text = Request.UrlReferrer.ToString();
					LoadOptions();			
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
			string stParam = "?task=" + Common.Encrypt("add",Session.SessionID) + "&prodid=" + Common.Encrypt(lblProductID.Text,Session.SessionID);
			Response.Redirect("Default.aspx" + stParam);	
		}

		protected void cmdSave_Click(object sender, System.EventArgs e)
		{
			SaveRecord();
			string stParam = "?task=" + Common.Encrypt("add",Session.SessionID) + "&prodid=" + Common.Encrypt(lblProductID.Text,Session.SessionID);
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
			DataClass clsDataClass = new DataClass();
			Int64 prodid = Convert.ToInt64(Common.Decrypt(Request.QueryString["prodid"].ToString(),Session.SessionID));
			lblProductID.Text = Convert.ToString(prodid);
			
			ProductUnitsMatrix clsUnitMatrix = new ProductUnitsMatrix();
			ProductUnitsMatrixDetails clsUnitDetails = clsUnitMatrix.LastDetails(prodid);

			if (clsUnitDetails.BottomUnitName == null)
			{
				Products clsProduct = new Products();
				ProductDetails clsDetails = new ProductDetails();
                clsDetails = clsProduct.Details(Constants.BRANCH_ID_MAIN, prodid);
				clsProduct.CommitAndDispose();

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
			cboBottomUnit.DataSource = clsDataClass.DataReaderToDataTable(clsUnitMatrix.AvailableUnitsForProduct(prodid,"UnitName",SortOption.Ascending)).DefaultView;
			cboBottomUnit.DataBind();
			if (cboBottomUnit.Items.Contains( new ListItem(txtBaseUnit.Text, lblBaseUnitID.Text)))
			{
				cboBottomUnit.Items.RemoveAt( cboBottomUnit.Items.IndexOf(cboBottomUnit.Items.FindByValue(lblBaseUnitID.Text)));
			}
			cboBottomUnit.SelectedIndex = cboBottomUnit.Items.Count - 1;

			clsUnitMatrix.CommitAndDispose();
		}

		private Int32 SaveRecord()
		{
			ProductUnitsMatrix clsUnitMatrix = new ProductUnitsMatrix();
			ProductUnitsMatrixDetails clsDetails = new ProductUnitsMatrixDetails();

			clsDetails.ProductID = Convert.ToInt64(lblProductID.Text);
			clsDetails.BaseUnitID = Convert.ToInt32(lblBaseUnitID.Text);
			clsDetails.BaseUnitValue = Convert.ToDecimal(txtBaseUnitValue.Text);
			clsDetails.BottomUnitID = Convert.ToInt32(cboBottomUnit.SelectedItem.Value);
			clsDetails.BottomUnitValue = Convert.ToDecimal(txtBottomUnitValue.Text);
			int id = clsUnitMatrix.Insert(clsDetails);

            ProductPackageDetails clsProductPackageDetails = new ProductPackageDetails();
            ProductPackage clsProductPackage = new ProductPackage(clsUnitMatrix.Connection, clsUnitMatrix.Transaction);
            clsProductPackageDetails = clsProductPackage.DetailsByProductIDAndUnitID(Convert.ToInt64(lblProductID.Text), Convert.ToInt32(cboBottomUnit.SelectedItem.Value));
            if (clsProductPackageDetails.PackageID == 0)
            {
                Products clsProduct = new Products(clsUnitMatrix.Connection, clsUnitMatrix.Transaction);
                ProductDetails clsProductDetails = clsProduct.Details(Constants.BRANCH_ID_MAIN, Convert.ToInt64(lblProductID.Text));

                Terminal clsTerminal = new Terminal(clsUnitMatrix.Connection, clsUnitMatrix.Transaction);
                TerminalDetails clsTerminalDetails = clsTerminal.Details(Constants.C_DEFAULT_TERMINAL_ID_01);

                clsProductPackageDetails.ProductID = Convert.ToInt64(lblProductID.Text);
                clsProductPackageDetails.UnitID = Convert.ToInt32(cboBottomUnit.SelectedItem.Value);
                clsProductPackageDetails.Price = clsProductDetails.Price * Convert.ToDecimal(txtBaseUnitValue.Text);
                clsProductPackageDetails.PurchasePrice = clsProductDetails.PurchasePrice * Convert.ToDecimal(txtBaseUnitValue.Text);
                clsProductPackageDetails.Quantity = 1;
                clsProductPackageDetails.VAT = clsTerminalDetails.VAT;
                clsProductPackageDetails.EVAT = clsTerminalDetails.EVAT;
                clsProductPackageDetails.LocalTax = clsTerminalDetails.LocalTax;
                clsProductPackage.Insert(clsProductPackageDetails);
            }
			clsUnitMatrix.CommitAndDispose();

			return id;
		}


		#endregion
    }
}

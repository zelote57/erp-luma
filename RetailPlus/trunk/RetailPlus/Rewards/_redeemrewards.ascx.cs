using System;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using AceSoft.RetailPlus.Data;

namespace AceSoft.RetailPlus.Rewards
{
	public partial  class __RedeemRewards : System.Web.UI.UserControl
	{

		#region Web Form Methods

		protected void Page_Load(object sender, System.EventArgs e)
		{
            if (!IsPostBack && Visible)
			{
				lblReferrer.Text = Request.UrlReferrer.ToString();
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
		
		///		Required method for Designer support - do not modify
		///		the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{

		}
		#endregion

		#region Web Control Methods

        protected void imgCancel_Click(object sender, System.Web.UI.ImageClickEventArgs e)
		{
			Response.Redirect(lblReferrer.Text);
		}
        protected void cmdCancel_Click(object sender, System.EventArgs e)
		{
			Response.Redirect(lblReferrer.Text);
		}
        protected void cmdCustomer_Click(object sender, System.Web.UI.ImageClickEventArgs e)
        {
            try
            {
                LoadMembers();
                cboCustomer_SelectedIndexChanged(null, null);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        protected void cboCustomer_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            try
            {
                if (cboCustomer.SelectedItem.Value != Constants.ZERO_STRING)
                {
                    long lngCustomerID = long.Parse(cboCustomer.SelectedItem.Value);
                    Data.ContactReward clsContactReward = new Data.ContactReward();
                    Data.ContactRewardDetails clsContactRewardDetails = clsContactReward.Details(lngCustomerID);
                    clsContactReward.CommitAndDispose();

                    txtCurrentRewardPoints.Text = clsContactRewardDetails.RewardPoints.ToString();
                    txtRedeemRewardPoints.Enabled = (DateTime.Now > clsContactRewardDetails.ExpiryDate) ? true : false;
                    txtRedeemRewardPoints.Text = (DateTime.Now > clsContactRewardDetails.ExpiryDate) ? "0" : txtRedeemRewardPoints.Text;
                    txtNewRewardPoints.Text = Convert.ToInt32(Convert.ToInt32(txtCurrentRewardPoints.Text) - Convert.ToInt32(txtRedeemRewardPoints.Text)).ToString();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
       
        protected void cmdProductCode_Click(object sender, System.Web.UI.ImageClickEventArgs e)
        {
            LoadProduct();
        }
        protected void cboProductCode_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            try
            {
                if (cboProductCode.SelectedItem.Value != Constants.ZERO_STRING)
                {
                    Product clsProduct = new Product();
                    ProductDetails clsProductDetails = clsProduct.Details(long.Parse(cboProductCode.SelectedItem.Value));
                    clsProduct.CommitAndDispose();
                    txtCurrentRewardPoints.Text = clsProductDetails.VAT.ToString("#,##0.#0");
                    txtRedeemRewardPoints.Text = clsProductDetails.EVAT.ToString("#,##0.#0");
                    txtNewRewardPoints.Text = clsProductDetails.LocalTax.ToString("#,##0.#0");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        protected void imgRedeemRewards_Click(object sender, System.Web.UI.ImageClickEventArgs e)
        {
            SaveRecord(); 
        }
        protected void cmdRedeemRewards_Click(object sender, EventArgs e)
        {
            SaveRecord(); 
        }

		#endregion

		#region Private Methods

		private void LoadOptions()
		{
            cboCustomer.Items.Add(new ListItem(Constants.ALL, Constants.ZERO_STRING));
            cboCustomer.SelectedIndex = 0;

            string strproductcode = string.Empty;
            try { strproductcode = Common.Decrypt(Request.QueryString["productcode"].ToString(), Session.SessionID); }
            catch { }

            if (strproductcode == string.Empty)
            {
                cboProductCode.Items.Clear();
                cboProductCode.Items.Add(new ListItem(Constants.ALL, Constants.ZERO_STRING));
                cboProductCode.SelectedIndex = 0;
            }
            else{
                txtProductCode.Text = strproductcode;
                cmdProductCode_Click(null, null);
            }

            lblProductGroupID.Text = Constants.ZERO_STRING;
            lblProductID.Text = Constants.ZERO_STRING;
		}
        private void LoadMembers()
        {
            Contact clsContact = new Contact();
            DataClass clsDataClass = new DataClass();
            ContactColumns clsContactColumns = new ContactColumns();
            clsContactColumns.ContactID = true;
            clsContactColumns.ContactCode = true;
            clsContactColumns.ContactName = true;
            clsContactColumns.RewardDetails = true;

            ContactColumns clsSearchColumns = new ContactColumns();
            clsSearchColumns.ContactCode = true;
            clsSearchColumns.ContactName = true;
            clsSearchColumns.RewardDetails = true;

            cboCustomer.DataTextField = "ContactID";
            cboCustomer.DataValueField = "ContactName";

            string SearchKey = "%" + txtCustomer.Text;
            cboCustomer.DataSource = clsContact.Customers(clsContactColumns, 0, System.Data.SqlClient.SortOrder.Ascending, clsSearchColumns, SearchKey, 20, false, null, System.Data.SqlClient.SortOrder.Ascending).DefaultView;
            cboCustomer.DataBind();
            clsContact.CommitAndDispose();

            if (cboCustomer.Items.Count == 0) cboCustomer.Items.Insert(0, new ListItem(Constants.ALL, Constants.ZERO_STRING));
            cboCustomer.SelectedIndex = 0;
        }
        private void LoadProduct()
        {
            Data.ProductColumns clsProductColumns = new Data.ProductColumns();
            clsProductColumns.ProductID = true;
            clsProductColumns.ProductCode = true;

            string strSearchKey = txtProductCode.Text.Trim();
            Data.ProductDetails clsSearchKeys = new Data.ProductDetails();
            clsSearchKeys.BarCode = strSearchKey;
            clsSearchKeys.BarCode2 = strSearchKey;
            clsSearchKeys.BarCode3 = strSearchKey;
            clsSearchKeys.ProductCode = strSearchKey;

            Data.Product clsProduct = new Data.Product();
            cboProductCode.DataTextField = "ProductCode";
            cboProductCode.DataValueField = "ProductID";
            cboProductCode.DataSource = clsProduct.ListAsDataTable(clsProductColumns, clsSearchKeys, ProductListFilterType.ShowActiveAndInactive, 0, System.Data.SqlClient.SortOrder.Ascending, 100, false, "ProductCode", SortOption.Ascending);
            cboProductCode.DataBind();
            clsProduct.CommitAndDispose();

            if (cboProductCode.Items.Count == 0) cboProductCode.Items.Insert(0, new ListItem(Constants.ALL, Constants.ZERO_STRING));
            cboProductCode.SelectedIndex = 0;
        }
		private void SaveRecord()
		{
            long lngCustomerID = long.Parse(cboCustomer.SelectedItem.Value);
            decimal decCurrentRewardPoints = 0; decimal decRedeemRewardPoints = 0; decimal decNewRewardPoints = 0;

            if (lngCustomerID != 0)
            {
                if (decRedeemRewardPoints < 0 || decRedeemRewardPoints < decCurrentRewardPoints || txtRedeemRewardPoints.Enabled==false)
                {
                    // Cannot be negative or less than current reward points or expired
                    return;
                }

                RetailPlus.Security.AccessUserDetails clsAccessUserDetails = (RetailPlus.Security.AccessUserDetails) Session["AccessUserDetails"];

                // this should comes before earning of points otherwise this will be wrong.
                Data.ContactReward clsContactReward = new Data.ContactReward();
                Data.ContactRewardDetails clsContactRewardDetails = clsContactReward.Details(lngCustomerID);

                clsContactReward.DeductPoints(lngCustomerID, decRedeemRewardPoints);
                string strReason = "Redeemed " + decRedeemRewardPoints + " using Reward Card #: " + clsContactRewardDetails.RewardCardNo;
                clsContactReward.AddMovement(lngCustomerID, DateTime.Now, decCurrentRewardPoints, -decRedeemRewardPoints, decCurrentRewardPoints - decRedeemRewardPoints, clsContactRewardDetails.ExpiryDate, strReason, "BACKEND", clsAccessUserDetails.Name, DateTime.Now.ToString("yyyyMMddhhmmsstt"));


                //PrintRewardsRedemptionSlip();
                clsContactReward.CommitAndDispose();


                long lngProductSubGroupID = 0;
                long lngProductID = long.Parse(cboProductCode.SelectedItem.Value);
                
                string stScript;
                try
                { decCurrentRewardPoints = decimal.Parse(txtCurrentRewardPoints.Text); }
                catch
                {
                    stScript = "<Script>";
                    stScript += "window.alert('Please enter a valid VAT.')";
                    stScript += "</Script>";
                    Response.Write(stScript);
                    return;
                }
                try
                { decRedeemRewardPoints = decimal.Parse(txtRedeemRewardPoints.Text); }
                catch
                {
                    stScript = "<Script>";
                    stScript += "window.alert('Please enter a valid EVAT.')";
                    stScript += "</Script>";
                    Response.Write(stScript);
                    return;
                }
                try
                { decNewRewardPoints = decimal.Parse(txtNewRewardPoints.Text); }
                catch
                {
                    stScript = "<Script>";
                    stScript += "window.alert('Please enter a valid LocalTax.')";
                    stScript += "</Script>";
                    Response.Write(stScript);
                    return;
                }
                if (lngProductID != 0)
                {
                    Product clsProduct = new Product();
                    clsProduct.ChangeTax(0, 0, lngProductID, decCurrentRewardPoints, decRedeemRewardPoints, decNewRewardPoints);
                    clsProduct.CommitAndDispose();
                }
                else if (lngProductSubGroupID != 0)
                {
                    ProductSubGroup clsProductSubGroup = new ProductSubGroup();
                    clsProductSubGroup.ChangeTax(0, lngProductSubGroupID, decCurrentRewardPoints, decRedeemRewardPoints, decNewRewardPoints);
                    clsProductSubGroup.CommitAndDispose();
                }
                else
                {
                    ProductGroup clsProductGroup = new ProductGroup();
                    clsProductGroup.ChangeTax(lngCustomerID, decCurrentRewardPoints, decRedeemRewardPoints, decNewRewardPoints);
                    clsProductGroup.CommitAndDispose();
                }

                txtCurrentRewardPoints.Text = decCurrentRewardPoints.ToString("#,##0.#0");
                txtCurrentRewardPoints.Text = decRedeemRewardPoints.ToString("#,##0.#0");
                txtCurrentRewardPoints.Text = decNewRewardPoints.ToString("#,##0.#0");

                stScript = "<Script>";
                stScript += "window.alert('Reward points has been updated.')";
                stScript += "</Script>";
                Response.Write(stScript);
            }
            
		}

		#endregion
        
    }
}
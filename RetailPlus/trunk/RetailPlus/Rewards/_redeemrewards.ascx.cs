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
				lblReferrer.Text = Request.UrlReferrer == null ? Constants.ROOT_DIRECTORY : Request.UrlReferrer.ToString();
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

                    txtRewardCardNo.Text = clsContactRewardDetails.RewardCardNo;
					txtCurrentRewardPoints.Text = clsContactRewardDetails.RewardPoints.ToString();
					txtRedeemRewardPoints.Enabled = (DateTime.Now > clsContactRewardDetails.ExpiryDate) ? true : false;
					txtRedeemRewardPoints.Text = (DateTime.Now > clsContactRewardDetails.ExpiryDate) ? "0" : txtRedeemRewardPoints.Text;
                    txtNewRewardPoints.Text = Convert.ToInt32(Convert.ToDecimal(txtCurrentRewardPoints.Text) - Convert.ToDecimal(txtRedeemRewardPoints.Text)).ToString();
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
					Products clsProduct = new Products();
                    ProductDetails clsProductDetails = clsProduct.Details(long.Parse(cboProductCode.SelectedItem.Value));
					clsProduct.CommitAndDispose();
					txtRedeemRewardPoints.Text = clsProductDetails.RewardPoints.ToString("#,##0.#0");
                    txtNewRewardPoints.Text = Convert.ToInt32(Convert.ToDecimal(txtCurrentRewardPoints.Text) - Convert.ToDecimal(txtRedeemRewardPoints.Text)).ToString();
				}
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}
		protected void imgRedeemRewards_Click(object sender, System.Web.UI.ImageClickEventArgs e)
		{
            if (SaveRecord())
            {
                string stParam = "?task=" + Common.Encrypt("redeemrewards", Session.SessionID);
                Response.Redirect("Default.aspx" + stParam);
            }
		}
		protected void cmdRedeemRewards_Click(object sender, EventArgs e)
		{
            if (SaveRecord())
            {
                string stParam = "?task=" + Common.Encrypt("redeemrewards", Session.SessionID);
                Response.Redirect("Default.aspx" + stParam);
            }
		}

		#endregion

		#region Private Methods

		private void LoadOptions()
		{
			cboCustomer.Items.Add(new ListItem(Constants.PLEASE_SELECT, Constants.ZERO_STRING));
			cboCustomer.SelectedIndex = 0;

			string strproductcode = string.Empty;
			try { strproductcode = Common.Decrypt(Request.QueryString["productcode"].ToString(), Session.SessionID); }
			catch { }

			if (strproductcode == string.Empty)
			{
				cboProductCode.Items.Clear();
				cboProductCode.Items.Add(new ListItem(Constants.PLEASE_SELECT, Constants.ZERO_STRING));
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
			Contacts clsContact = new Contacts();
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

            cboCustomer.DataTextField = "ContactName";
            cboCustomer.DataValueField = "ContactID";

			string SearchKey = "%" + txtCustomer.Text;
			cboCustomer.DataSource = clsContact.Customers(clsContactColumns, 0, System.Data.SqlClient.SortOrder.Ascending, clsSearchColumns, SearchKey, 20, false, "ContactName", System.Data.SqlClient.SortOrder.Ascending).DefaultView;
			cboCustomer.DataBind();
			clsContact.CommitAndDispose();

			if (cboCustomer.Items.Count == 0) cboCustomer.Items.Insert(0, new ListItem(Constants.PLEASE_SELECT, Constants.ZERO_STRING));
			cboCustomer.SelectedIndex = 0;
		}
		private void LoadProduct()
		{
			string strSearchKey = txtProductCode.Text.Trim();
			Data.ProductDetails clsSearchKeys = new Data.ProductDetails();
			clsSearchKeys.BarCode = strSearchKey;
			clsSearchKeys.BarCode2 = strSearchKey;
			clsSearchKeys.BarCode3 = strSearchKey;
			clsSearchKeys.ProductCode = strSearchKey;

			Data.Products clsProduct = new Data.Products();
			cboProductCode.DataTextField = "ProductCode";
			cboProductCode.DataValueField = "ProductID";
            cboProductCode.DataSource = clsProduct.ListAsDataTable(clsSearchKeys: clsSearchKeys, limit: 100);
			cboProductCode.DataBind();
			clsProduct.CommitAndDispose();

			if (cboProductCode.Items.Count == 0) cboProductCode.Items.Insert(0, new ListItem(Constants.PLEASE_SELECT, Constants.ZERO_STRING));
			cboProductCode.SelectedIndex = 0;
		}
		private bool SaveRecord()
		{
			long lngCustomerID = long.Parse(cboCustomer.SelectedItem.Value);
            decimal decCurrentRewardPoints = (txtCurrentRewardPoints.Text.Trim() == string.Empty) ? 0 : Decimal.Parse(txtCurrentRewardPoints.Text); 
            decimal decRedeemRewardPoints = (txtRedeemRewardPoints.Text.Trim() == string.Empty) ? 0 : Decimal.Parse(txtRedeemRewardPoints.Text);
            decimal decNewRewardPoints = decCurrentRewardPoints - decRedeemRewardPoints;

			if (lngCustomerID != 0)
			{
				if (decRedeemRewardPoints < 0 || decRedeemRewardPoints > decCurrentRewardPoints || txtRedeemRewardPoints.Enabled==false)
				{
					// Cannot be negative or less than current reward points or expired
					return false;
				}

				RetailPlus.Security.AccessUserDetails clsAccessUserDetails = (RetailPlus.Security.AccessUserDetails) Session["AccessUserDetails"];

				// this should comes before earning of points otherwise this will be wrong.
				Data.ContactReward clsContactReward = new Data.ContactReward();
				Data.ContactRewardDetails clsContactRewardDetails = clsContactReward.Details(lngCustomerID);

				string strReason = "Redeemed " + decRedeemRewardPoints + " using Reward Card #: " + clsContactRewardDetails.RewardCardNo;
                strReason += (txtRemarks.Text.Trim() == string.Empty) ? "" : " Remarks:" + txtRemarks.Text;
				clsContactReward.AddMovement(lngCustomerID, DateTime.Now, decCurrentRewardPoints, -decRedeemRewardPoints, decCurrentRewardPoints - decRedeemRewardPoints, clsContactRewardDetails.ExpiryDate, strReason, "BACKEND", clsAccessUserDetails.Name, DateTime.Now.ToString("yyyyMMddHHmmss").Trim());
                
                clsContactReward.DeductPoints(lngCustomerID, decRedeemRewardPoints);

                clsContactReward.CommitAndDispose();

				//PrintRewardsRedemptionSlip();
				string stScript;
				stScript = "<Script>";
				stScript += "window.alert('Reward points has been updated.')";
				stScript += "</Script>";
				Response.Write(stScript);

                txtCurrentRewardPoints.Text = decNewRewardPoints.ToString();
                txtRedeemRewardPoints.Text = "0";
                txtNewRewardPoints.Text = decNewRewardPoints.ToString();
                txtRemarks.Text = string.Empty;

                return true;
			}
            return false;
		}

		#endregion
		
	}
}

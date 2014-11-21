using System;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using AceSoft.RetailPlus.Data;

namespace AceSoft.RetailPlus.Credits._Guarantors
{
    public partial class __ChangeGuarantor : System.Web.UI.UserControl
	{

		#region Web Form Methods

		protected void Page_Load(object sender, System.EventArgs e)
		{
            if (!IsPostBack && Visible)
			{
				lblReferrer.Text = Request.UrlReferrer == null ? Constants.ROOT_DIRECTORY : Request.UrlReferrer.ToString();
			    LoadOptions();

                cmdSaveGuarantor.Attributes.Add("onClick", "return confirm_changeguarantor();");
                imgSaveGuarantor.Attributes.Add("onClick", "return confirm_changeguarantor();");
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
        protected void cmdContactSearch_Click(object sender, System.Web.UI.ImageClickEventArgs e)
        {
            try
            {
                Data.Contacts clsContact = new Data.Contacts();

                cboContact.DataTextField = "ContactName";
                cboContact.DataValueField = "ContactID";
                cboContact.DataSource = clsContact.CustomersWithCredits(new ContactColumns() { ContactName = true }, CustomerCode_CreditCardNo: txtSearch.Text, CheckCustomersGuarantor: true, WithGuarantorOnly: true, SortField: "ContactName").DefaultView;
                cboContact.DataBind();
                clsContact.CommitAndDispose();

                if (cboContact.Items.Count == 0) cboContact.Items.Insert(0, new ListItem(Constants.PLEASE_SELECT, Constants.ZERO_STRING));
                cboContact.SelectedIndex = 0;

                cboContact_SelectedIndexChanged(null, null);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        protected void cboContact_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            try
            {
                if (cboContact.SelectedItem.Value != Constants.ZERO_STRING)
                {
                    ContactColumns clsContactColumns = new ContactColumns();
                    clsContactColumns.ContactID = true;
                    clsContactColumns.ContactCode = true;
                    clsContactColumns.ContactName = true;
                    clsContactColumns.CreditDetails = true;

                    ContactColumns clsSearchColumns = new ContactColumns();
                    clsSearchColumns.ContactCode = true;
                    clsSearchColumns.ContactName = true;
                    clsSearchColumns.CreditDetails = true;

                    Int64 iContactID = Int64.Parse(cboContact.SelectedItem.Value);

                    Contacts clsContact = new Contacts();
                    Data.ContactDetails clsContactDetails = clsContact.Details(iContactID);
                    Data.ContactDetails clsGuarantorDetails = clsContact.Details(clsContactDetails.CreditDetails.GuarantorID);
                    clsContact.CommitAndDispose();

                    txtGuarantorName.Text = clsGuarantorDetails.ContactName.ToString();
                    txtGuarantorName.ToolTip = clsGuarantorDetails.ContactID.ToString();

                    divContactInfo.Visible = true;
                }
                else
                {
                    divContactInfo.Visible = false;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        protected void imgSaveGuarantor_Click(object sender, System.Web.UI.ImageClickEventArgs e)
        {
            SaveRecord(); 
        }
        protected void cmdSaveGuarantor_Click(object sender, EventArgs e)
        {
            SaveRecord(); 
        }

        protected void lstItemCustomer_ItemDataBound(object sender, DataListItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Header)
            {
                //LoadSortFieldOptions(e);
            }
            else if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                DataRowView dr = (DataRowView)e.Item.DataItem;
                ImageButton imgItemEdit = (ImageButton)e.Item.FindControl("imgItemEdit");

                HtmlInputCheckBox chkList = (HtmlInputCheckBox)e.Item.FindControl("chkList");
                chkList.Value = dr["ContactID"].ToString();
                if (chkList.Value == "1" || chkList.Value == "2")
                {
                    chkList.Attributes.Add("disabled", "false");
                    imgItemEdit.Enabled = false; imgItemEdit.ImageUrl = Constants.ROOT_DIRECTORY + "/_layouts/images/blank.gif";
                }
                else
                {
                    imgItemEdit.Enabled = cmdSaveGuarantor.Visible; if (!imgItemEdit.Enabled) imgItemEdit.ImageUrl = Constants.ROOT_DIRECTORY + "/_layouts/images/blank.gif";
                }

                HyperLink lnkContactName = (HyperLink)e.Item.FindControl("lnkContactName");
                lnkContactName.Text = dr["ContactName"].ToString();
                lnkContactName.NavigateUrl = "Default.aspx?task=" + Common.Encrypt("details", Session.SessionID) + "&id=" + Common.Encrypt(chkList.Value, Session.SessionID) + "&showbills=" + Common.Encrypt("false", Session.SessionID);

                Label lblCreditType = (Label)e.Item.FindControl("lblCreditType");
                lblCreditType.Text = dr["CardTypeCode"].ToString().ToString();

                Label lblCreditCardNo = (Label)e.Item.FindControl("lblCreditCardNo");
                lblCreditCardNo.Text = dr["CreditCardNo"].ToString();

                Label lblCreditCardStatus = (Label)e.Item.FindControl("lblCreditCardStatus");
                lblCreditCardStatus.Text = Enum.Parse(typeof(CreditCardStatus), dr["CreditCardStatus"].ToString()).ToString();

                Label lblCreditActive = (Label)e.Item.FindControl("lblCreditActive");
                lblCreditActive.Text = Data.Contacts.checkCreditActive((CreditCardStatus)Enum.Parse(typeof(CreditCardStatus), dr["CreditCardStatus"].ToString())) ? "Active" : "InActive";

                Label lblExpiryDate = (Label)e.Item.FindControl("lblExpiryDate");
                lblExpiryDate.Text = Convert.ToDateTime(dr["ExpiryDate"].ToString()).ToString("dd-MMM-yyyy");

                decimal decCreditLimit = Convert.ToDecimal(dr["CreditLimit"].ToString());
                decimal decCredit = Convert.ToDecimal(dr["Credit"].ToString());
                decimal decAvailableCredit = decCreditLimit - decCredit;

                Label lblCreditLimit = (Label)e.Item.FindControl("lblCreditLimit");
                lblCreditLimit.Text = decCreditLimit.ToString("#,##0.#");

                Label lblCredit = (Label)e.Item.FindControl("lblCredit");
                lblCredit.Text = decCredit.ToString("#,##0.#");

                Label lblAvailableCredit = (Label)e.Item.FindControl("lblAvailableCredit");
                lblAvailableCredit.Text = decAvailableCredit.ToString("#,##0.#");

                Label lblTotalPurchases = (Label)e.Item.FindControl("lblTotalPurchases");
                lblTotalPurchases.Text = Convert.ToDecimal(dr["TotalPurchases"].ToString()).ToString("#,##0.#");

                Label lblLastBillingDate = (Label)e.Item.FindControl("lblLastBillingDate");
                lblLastBillingDate.Text = Convert.ToDateTime(dr["LastBillingDate"].ToString()).ToString("dd-MMM-yyyy");
            }
        }

        protected void lstItemCustomer_ItemCommand(object sender, DataListCommandEventArgs e)
        {
            HtmlInputCheckBox chkList = (HtmlInputCheckBox)e.Item.FindControl("chkList");
            string stParam = string.Empty;
            switch (e.CommandName)
            {
                case "imgItemEdit":
                    stParam = "?task=" + Common.Encrypt("edit", Session.SessionID) + "&id=" + Common.Encrypt(chkList.Value, Session.SessionID) + "&showbills=" + Common.Encrypt("false", Session.SessionID);
                    Response.Redirect("Default.aspx" + stParam);
                    break;
            }
        }

		#endregion

		#region Private Methods

		private void LoadOptions()
		{
            Data.Contacts clsContact = new Data.Contacts();

            cboContact.DataTextField = "ContactName";
            cboContact.DataValueField = "ContactID";
            cboContact.DataSource = clsContact.CustomersWithCredits(new ContactColumns() { ContactName = true }, CheckCustomersGuarantor: true, WithGuarantorOnly: true, SortField: "ContactName").DefaultView;
            cboContact.DataBind();
            cboContact.Items.Insert(0, new ListItem(Constants.PLEASE_SELECT, Constants.ZERO_STRING));
            cboContact.SelectedIndex = 0;

            cboGuarantor.DataTextField = "ContactName";
            cboGuarantor.DataValueField = "ContactID";
            cboGuarantor.DataSource = clsContact.Guarantors(new ContactColumns() { ContactName = true }, SortField: "ContactName").DefaultView;
            cboGuarantor.DataBind();
            cboGuarantor.Items.Insert(0, new ListItem(Constants.PLEASE_SELECT, Constants.ZERO_STRING));
            cboGuarantor.SelectedIndex = 0;

            clsContact.CommitAndDispose();

            Int64 iContactID = 0;
            if (Request.QueryString["id"] != null)
            {
                try { iContactID = Int64.TryParse(Common.Decrypt(Request.QueryString["id"].ToString(), Session.SessionID), out iContactID) ? iContactID : 0; }
                catch { }
            }

            cboContact.ToolTip = iContactID.ToString();
            if (iContactID == 0)
            {
                divContactInfo.Visible = false;
            }
            else
            {
                cboContact.SelectedIndex = cboContact.Items.IndexOf(cboContact.Items.FindByValue(iContactID.ToString()));
                cboContact_SelectedIndexChanged(null, null);
            }
		}
		private void SaveRecord()
		{
            Int64 iContactID = Int64.Parse(cboContact.SelectedItem.Value);
            Int16 iGuarantorID = Int16.Parse(cboGuarantor.SelectedItem.Value);

            string javaScript;
            if (iContactID == 0)
            {
                javaScript = "window.alert('Please select a customer to change the guarantor.')";
                System.Web.UI.ScriptManager.RegisterClientScriptBlock(this.updSave, this.updSave.GetType(), "openwindow", javaScript, true);
                return;
            }
            if (iGuarantorID == 0)
            {
                javaScript = "window.alert('Please select a valid guarantor.')";
                System.Web.UI.ScriptManager.RegisterClientScriptBlock(this.updSave, this.updSave.GetType(), "openwindow", javaScript, true);
                return;
            }

            Security.AccessUserDetails clsAccessUserDetails = (Security.AccessUserDetails)Session["AccessUserDetails"];

            Contacts clsContacts = new Contacts();
            clsContacts.UpdateGuarantor(iContactID, iGuarantorID, clsAccessUserDetails.Name);
            clsContacts.CommitAndDispose();

            cboContact_SelectedIndexChanged(null, null);

            javaScript = "window.alert('Guarantor for " + cboContact.SelectedItem.Text + " has been updated.')";
            System.Web.UI.ScriptManager.RegisterClientScriptBlock(this.updSave, this.updSave.GetType(), "openwindow", javaScript, true);
		}

		#endregion
        
    }
}

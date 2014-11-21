using System;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using AceSoft.RetailPlus.Data;

namespace AceSoft.RetailPlus.Credits._Customers
{
    public partial class __ChangeCardType : System.Web.UI.UserControl
	{

		#region Web Form Methods

		protected void Page_Load(object sender, System.EventArgs e)
		{
            if (!IsPostBack && Visible)
			{
				lblReferrer.Text = Request.UrlReferrer == null ? Constants.ROOT_DIRECTORY : Request.UrlReferrer.ToString();
			    LoadOptions();

                imgSaveCardType.Attributes.Add("onClick", "return confirm_changecreditcardtype();");
                cmdSaveCardType.Attributes.Add("onClick", "return confirm_changecreditcardtype();");
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
                cboContact.DataSource = clsContact.CustomersWithCredits(new ContactColumns() { ContactName = true }, CustomerCode_CreditCardNo: txtSearch.Text, CheckCustomersGuarantor: true, SortField: "ContactName").DefaultView;
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

                    Int64 iCustomerID = Int64.Parse(cboContact.SelectedItem.Value);

                    Contacts clsContact = new Contacts();
                    Data.ContactDetails clsContactDetails = clsContact.Details(iCustomerID);
                    clsContact.CommitAndDispose();

                    txtCreditCardTypeCode.Text = clsContactDetails.CreditDetails.CardTypeDetails.CardTypeCode;
                    txtCreditCardTypeCode.ToolTip = clsContactDetails.CreditDetails.CardTypeDetails.CardTypeID.ToString();

                    divGuarantorInfo.Visible = true;
                }
                else
                {
                    divGuarantorInfo.Visible = false;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        protected void imgSaveCardType_Click(object sender, System.Web.UI.ImageClickEventArgs e)
        {
            SaveRecord(); 
        }
        protected void cmdSaveCardType_Click(object sender, EventArgs e)
        {
            SaveRecord(); 
        }

		#endregion

		#region Private Methods

		private void LoadOptions()
		{
            Data.Contacts clsContact = new Data.Contacts();

            cboContact.DataTextField = "ContactName";
            cboContact.DataValueField = "ContactID";
            cboContact.DataSource = clsContact.CustomersWithCredits(new ContactColumns() { ContactName = true }, CheckCustomersGuarantor:true, WithGuarantorOnly: false, SortField: "ContactName").DefaultView;
            cboContact.DataBind();
            cboContact.Items.Insert(0, new ListItem(Constants.PLEASE_SELECT, Constants.ZERO_STRING));
            cboContact.SelectedIndex = 0;

            Data.CardType clsCardType = new CardType(clsContact.Connection, clsContact.Transaction);
            cboCardType.DataTextField = "CardTypeCode";
            cboCardType.DataValueField = "CardTypeID";
            cboCardType.DataSource = clsCardType.ListAsDataTable(new CardTypeDetails() { CreditCardType = CreditCardTypes.Internal, CheckGuarantor = true, WithGuarantor = false }).DefaultView;
            cboCardType.DataBind();
            cboCardType.Items.Insert(0, new ListItem(Constants.PLEASE_SELECT, Constants.ZERO_STRING));
            cboCardType.SelectedIndex = 0;

            clsContact.CommitAndDispose();

            Int64 iCustomerID = 0;
            if (Request.QueryString["id"] != null)
            {
                try { iCustomerID = Int64.TryParse(Common.Decrypt(Request.QueryString["id"].ToString(), Session.SessionID), out iCustomerID) ? iCustomerID : 0; }
                catch { }
            }

            cboContact.ToolTip = iCustomerID.ToString();
            if (iCustomerID == 0)
            {
                divGuarantorInfo.Visible = false;
            }
            else
            {
                cboContact.SelectedIndex = cboContact.Items.IndexOf(cboContact.Items.FindByValue(iCustomerID.ToString()));
                cboContact_SelectedIndexChanged(null, null);
            }
		}
		private void SaveRecord()
		{
            Int64 iCustomerID = Int64.Parse(cboContact.SelectedItem.Value);
            Int16 iCreditCardTypeID = Int16.Parse(cboCardType.SelectedItem.Value);

            string javaScript;
            if (iCustomerID == 0)
            {
                javaScript = "window.alert('Please select a customer to change the card type.')";
                System.Web.UI.ScriptManager.RegisterClientScriptBlock(this.updSave, this.updSave.GetType(), "openwindow", javaScript, true);
                return;
            }
            if (iCreditCardTypeID == 0)
            {
                javaScript = "window.alert('Please select a valid credit card type.')";
                System.Web.UI.ScriptManager.RegisterClientScriptBlock(this.updSave, this.updSave.GetType(), "openwindow", javaScript, true);
                return;
            }

            Security.AccessUserDetails clsAccessUserDetails = (Security.AccessUserDetails)Session["AccessUserDetails"];

            Contacts clsContacts = new Contacts();
            clsContacts.UpdateCreditCardType(iCustomerID, iCreditCardTypeID, clsAccessUserDetails.Name);
            clsContacts.CommitAndDispose();

            cboContact_SelectedIndexChanged(null, null);

            javaScript = "window.alert('Card Type for " + cboContact.SelectedItem.Text + " has been updated.')";
            System.Web.UI.ScriptManager.RegisterClientScriptBlock(this.updSave, this.updSave.GetType(), "openwindow", javaScript, true);
		}

		#endregion
        
    }
}

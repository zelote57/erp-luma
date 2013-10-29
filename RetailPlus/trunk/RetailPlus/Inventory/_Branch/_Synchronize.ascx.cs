namespace AceSoft.RetailPlus.Inventory._Branch
{
    using System;
    using System.Data;
    using System.Configuration;
    using System.Collections;
    using System.Web;
    using System.Web.Security;
    using System.Web.UI;
    using System.Web.UI.WebControls;
    using System.Web.UI.WebControls.WebParts;
    using System.Web.UI.HtmlControls;
    using AceSoft.RetailPlus.Data;

	/// <summary>
	///		Summary description for __Synchronize.
	/// </summary>
    public partial class __Synchronize : System.Web.UI.UserControl
	{

		#region Web Form Methods

		protected void Page_Load(object sender, System.EventArgs e)
		{
			if (!IsPostBack)
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
            this.imgCancel.Click += new System.Web.UI.ImageClickEventHandler(this.imgCancel_Click);
            this.imgSynchronize.Click += new System.Web.UI.ImageClickEventHandler(this.imgSynchronize_Click);

		}
		#endregion

		#region Web Control Methods

		private void imgCancel_Click(object sender, System.Web.UI.ImageClickEventArgs e)
		{
			Response.Redirect(lblReferrer.Text);
		}

		protected void cmdCancel_Click(object sender, System.EventArgs e)
		{
			Response.Redirect(lblReferrer.Text);
		}

        protected void imgSynchronize_Click(object sender, ImageClickEventArgs e)
        {
            Synchronize();
        }

		protected void cmdSynchronize_Click(object sender, System.EventArgs e)
		{
			Synchronize();
		}


		#endregion

		#region Private Methods

        private void LoadOptions()
        {
            Branch clsBranch = new Branch();
            cboBranch.DataTextField = "BranchCode";
            cboBranch.DataValueField = "BranchID";
            cboBranch.DataSource = clsBranch.ListAsDataTable("BranchCode", SortOption.Ascending).DefaultView;
            cboBranch.DataBind();
            cboBranch.SelectedIndex = 0;
            clsBranch.CommitAndDispose();

            if (cboBranch.Items.Count == 0)
            {
                imgSynchronize.Visible = false;
                cmdSynchronize.Enabled = false;
                cboBranch.Items.Add(new ListItem("No Branch", "0"));
            }
        }

        private void Synchronize()
        {
            try 
            {
                lblError.Text = string.Empty;
                
                Branch clsBranch = new Branch();
                BranchDetails clsBranchDetails = clsBranch.Details(Convert.ToInt16(cboBranch.SelectedItem.Value.ToString()));
                clsBranch.CommitAndDispose();

                if (IPAddress.IsOpen(clsBranchDetails.DBIP, int.Parse(clsBranchDetails.DBPort)) == false)
                {
                    lblError.Text = "Sorry cannot connect to Branch '" + cboBranch.SelectedItem.Text + "'. Please check you connection to IP Address :" + clsBranchDetails.DBIP + ". <br /><br />";
                    lblError.Text += "HOW TO CHECK : <br /><br />";
                    lblError.Text += "  1. Open command prompt<br />";
                    lblError.Text += "  2. Type ping[space][IP Address]<br /><br />";
                    lblError.Text += "If the answer is 'Request timed out.' then contact system administrator.<br />";
                    lblError.Text += "Else if the answer is 'Reply...' Follow the next steps.<br /><br />";
                    lblError.Text += "  3. Type telnet[space][IP Address][sapce][IP Port]<br /><br />";

                    return;
                }

                Session.Timeout = 60 * 60 * 30;
                RemoteBranchInventory clsBranchInventory = new RemoteBranchInventory();
                clsBranchInventory.GetConnectionToBranch(clsBranchDetails.DBIP);

                string[] InsertStatements = clsBranchInventory.GetInsertList(clsBranchDetails.BranchID);
                clsBranchInventory.CommitAndDispose();

                clsBranchInventory = new RemoteBranchInventory();
                clsBranchInventory.Delete(clsBranchDetails.BranchID);

                foreach (string InsertStatement in InsertStatements)
                {
                    try
                    {
                        clsBranchInventory.Insert(InsertStatement);
                        lblError.Text += InsertStatement + "<br /><br />";
                    }
                    catch {
                        lblError.Text += "<div class=ms-alternating> ERROR INSERTING ITEM: " + InsertStatement + "</div><br /><br />";
                        clsBranchInventory.Insert("';");
                    }
                    
                }

                clsBranchInventory.CommitAndDispose();

                lblError.Text = "Done synchronizing Branch: " + clsBranchDetails.BranchCode + "<br /><br />" + lblError.Text;
            }
            catch (Exception ex)
            {
                lblError.Text += "ERROR WHILE CREATING INSERT STATEMENT: " + ex.Message;
                //throw ex;
            }
        }

		#endregion

        protected void cboBranch_SelectedIndexChanged(object sender, EventArgs e)
        {
            lblError.Text = string.Empty;
        }
}
}

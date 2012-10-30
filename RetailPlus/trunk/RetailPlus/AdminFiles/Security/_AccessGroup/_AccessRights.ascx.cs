using AceSoft.RetailPlus.Security;

namespace AceSoft.RetailPlus.Security._AccessGroup
{
	using System;
	using System.Data;
	using System.Drawing;
	using System.Web;
	using System.Web.UI.WebControls;
	using System.Web.UI.HtmlControls;
	
	public partial  class __AccessRights : System.Web.UI.UserControl
	{
		
		#region Web Form Methods

		protected void Page_Load(object sender, System.EventArgs e)
		{
			if (!IsPostBack)
			{
				if (Visible)
				{
					lblReferrer.Text = Request.UrlReferrer.ToString();
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
        protected void imgApply_Click(object sender, System.Web.UI.ImageClickEventArgs e)
        {
            SaveRecord();
            ApplyToUsers();
            Response.Redirect(lblReferrer.Text);
        }
        protected void cmdApply_Click(object sender, EventArgs e)
        {
            SaveRecord();
            ApplyToUsers();
            Response.Redirect(lblReferrer.Text);
        }

        protected void lstAccessCategory_ItemDataBound(object sender, DataListItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                DataRowView dr = (DataRowView)e.Item.DataItem;

                Label lblCategory = (Label)e.Item.FindControl("lblCategory");
                lblCategory.Text = dr["Category"].ToString();

                DataList lstItem = (DataList)e.Item.FindControl("lstItem");
                AccessGroupRights clsAccessGroupRights = new AccessGroupRights();
                lstItem.DataSource = clsAccessGroupRights.DataList(lblCategory.Text, int.Parse(lblGroupID.Text), "Category, SequenceNo", SortOption.Ascending).DefaultView;
                clsAccessGroupRights.CommitAndDispose();
                lstItem.DataBind();
            }
        }
        protected void lstItem_ItemDataBound(object sender, DataListItemEventArgs e)
		{
			if(e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
			{
				DataRowView dr = (DataRowView) e.Item.DataItem;				

				HtmlInputCheckBox chkList = (HtmlInputCheckBox) e.Item.FindControl("chkList");
				chkList.Value = dr["TypeID"].ToString();

				Label lblTypeName = (Label) e.Item.FindControl("lblTypeName");
				lblTypeName.Text = dr["TypeName"].ToString();

				Label lblRemarks = (Label) e.Item.FindControl("lblRemarks");
				lblRemarks.Text = dr["Remarks"].ToString();

				HtmlInputCheckBox chkRead = (HtmlInputCheckBox) e.Item.FindControl("chkRead");
				chkRead.Checked = Convert.ToBoolean(dr["Read"].ToString());

				HtmlInputCheckBox chkWrite = (HtmlInputCheckBox) e.Item.FindControl("chkWrite");
				chkWrite.Checked = Convert.ToBoolean(dr["Write"].ToString());

                if (!Convert.ToBoolean(Convert.ToInt16(dr["Enabled"].ToString())))
                {
                    chkRead.Attributes.Add("disabled", "false");
                    chkWrite.Attributes.Add("disabled", "false");
                }
			}
		}

		#endregion

		#region Private Methods

		private void LoadOptions()
		{
            
		}
		private void LoadRecord()
		{
            int intID = int.Parse(Common.Decrypt(Request.QueryString["id"], Session.SessionID));
            lblGroupID.Text = intID.ToString();

            AccessType clsAccessType = new AccessType();
            lstAccessCategory.DataSource = clsAccessType.Categories("Category, SequenceNo", SortOption.Ascending).DefaultView;
            clsAccessType.CommitAndDispose();
            lstAccessCategory.DataBind();
		}
		private int SaveRecord()
		{
			int id = int.Parse(lblGroupID.Text);

            AccessGroupRights clsAccessGroupRights = new AccessGroupRights();
            AccessGroupRightsDetails clsDetails;
            foreach (DataListItem itemAccessCategory in lstAccessCategory.Items)
            {
                DataList lstItem = (DataList)itemAccessCategory.FindControl("lstItem");
                foreach (DataListItem item in lstItem.Items)
                {
                    HtmlInputCheckBox chkList = (HtmlInputCheckBox)item.FindControl("chkList");
                    HtmlInputCheckBox chkRead = (HtmlInputCheckBox)item.FindControl("chkRead");
                    HtmlInputCheckBox chkWrite = (HtmlInputCheckBox)item.FindControl("chkWrite");

                    clsDetails = new AccessGroupRightsDetails();
                    clsDetails.GroupID = id;
                    clsDetails.TranTypeID = int.Parse(chkList.Value);
                    clsDetails.Read = chkRead.Checked;
                    clsDetails.Write = chkWrite.Checked;

                    clsAccessGroupRights.Modify(clsDetails);
                }
            }
			clsAccessGroupRights.CommitAndDispose();

			return id;
		}
        private void ApplyToUsers()
        {
            Int32 intGroupID = Convert.ToInt32(lblGroupID.Text);

            AccessRightsDetails clsDetails;
            AccessUser clsAccessUser = new AccessUser();
            DataTable dt = clsAccessUser.ListAsDataTable(AccessGroupTypes.All, string.Empty, 0, intGroupID);

            AccessRights clsAccessRights = new AccessRights(clsAccessUser.Connection, clsAccessUser.Transaction);

            foreach (DataRow dr in dt.Rows)
            {
                long lngUID = long.Parse(dr["UID"].ToString());

                foreach (DataListItem itemAccessCategory in lstAccessCategory.Items)
                {
                    DataList lstItem = (DataList)itemAccessCategory.FindControl("lstItem");
                    foreach (DataListItem item in lstItem.Items)
                    {
                        HtmlInputCheckBox chkList = (HtmlInputCheckBox)item.FindControl("chkList");
                        HtmlInputCheckBox chkRead = (HtmlInputCheckBox)item.FindControl("chkRead");
                        HtmlInputCheckBox chkWrite = (HtmlInputCheckBox)item.FindControl("chkWrite");

                        clsDetails = new AccessRightsDetails();
                        clsDetails.UID = lngUID;
                        clsDetails.TranTypeID = Convert.ToInt16(chkList.Value);
                        clsDetails.Read = chkRead.Checked;
                        clsDetails.Write = chkWrite.Checked;

                        clsAccessRights.Modify(clsDetails);
                    }
                }
            }

            clsAccessUser.CommitAndDispose();
        }

		#endregion
        
    }
}

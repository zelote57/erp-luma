using AceSoft.RetailPlus.Security;

namespace AceSoft.RetailPlus.Security._AccessUser
{
	using System;
	using System.Data;
	using System.Drawing;
	using System.Web;
	using System.Web.UI.WebControls;
	using System.Web.UI.HtmlControls;
	
	public partial  class __AccessRights : System.Web.UI.UserControl
	{
		
		protected void Page_Load(object sender, System.EventArgs e)
		{
			if (!IsPostBack)
			{
				if (Visible)
				{
					lblReferrer.Text = Request.UrlReferrer == null ? Constants.ROOT_DIRECTORY : Request.UrlReferrer.ToString();
					LoadOptions();
					LoadRecord();
				}
			}
		}

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
        protected void lstAccessCategory_ItemDataBound(object sender, DataListItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                DataRowView dr = (DataRowView)e.Item.DataItem;

                Label lblCategory = (Label)e.Item.FindControl("lblCategory");
                lblCategory.Text = dr["Category"].ToString();

                DataList lstItem = (DataList)e.Item.FindControl("lstItem");
                if (cboGroup.ToolTip == "1") //check if load from cboGroup_SelectedIndexChanged
                {
                    AccessGroupRights clsAccessGroupRights = new AccessGroupRights();
                    lstItem.DataSource = clsAccessGroupRights.DataList(lblCategory.Text, int.Parse(cboGroup.SelectedValue), "Category, SequenceNo", SortOption.Ascending).DefaultView;
                    clsAccessGroupRights.CommitAndDispose();
                }
                else
                {
                    AccessRights clsAccessRights = new AccessRights();
                    lstItem.DataSource = clsAccessRights.DataList(lblCategory.Text, long.Parse(lblUID.Text), "Category, SequenceNo", SortOption.Ascending).DefaultView;
                    clsAccessRights.CommitAndDispose();
                }
                lstItem.DataBind();
            }
        }
        protected void lstItem_ItemDataBound(object sender, DataListItemEventArgs e)
		{
			if(e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
			{
				DataRowView dr = (DataRowView) e.Item.DataItem;

                HtmlInputCheckBox chkList = (HtmlInputCheckBox)e.Item.FindControl("chkList");
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
        protected void imgReload_Click(object sender, System.Web.UI.ImageClickEventArgs e)
        {
            DataClass clsDataClass = new DataClass();
            AccessUser clsAccessUser = new AccessUser();
            int intGroupID = clsAccessUser.Details(long.Parse(lblUID.Text)).GroupID;

            AccessType clsAccessType = new AccessType(clsAccessUser.Connection, clsAccessUser.Transaction);
            lstAccessCategory.DataSource = clsAccessType.Categories("Category, SequenceNo", SortOption.Ascending).DefaultView;
            clsAccessUser.CommitAndDispose();
            lstAccessCategory.DataBind();

            cboGroup.SelectedIndex = cboGroup.Items.IndexOf(cboGroup.Items.FindByValue(intGroupID.ToString()));
        }
        protected void cboGroup_SelectedIndexChanged(object sender, EventArgs e)
        {
            cboGroup.ToolTip = "1"; //load from cboGroup_SelectedIndexChanged
            AccessType clsAccessType = new AccessType();
            lstAccessCategory.DataSource = clsAccessType.Categories("Category, SequenceNo", SortOption.Ascending).DefaultView;
            clsAccessType.CommitAndDispose();
            lstAccessCategory.DataBind();
            cboGroup.ToolTip = string.Empty;
        }

        private void LoadOptions()
        {
            long lngID = Convert.ToInt64(Common.Decrypt(Request.QueryString["id"], Session.SessionID));
            lblUID.Text = lngID.ToString();

            DataClass clsDataClass = new DataClass();
            AccessUser clsAccessUser = new AccessUser();
            int intGroupID = clsAccessUser.Details(lngID).GroupID;

            AccessGroup clsAccessGroup = new AccessGroup(clsAccessUser.Connection, clsAccessUser.Transaction);
            cboGroup.DataTextField = "GroupName";
            cboGroup.DataValueField = "GroupID";
            cboGroup.DataSource = clsDataClass.DataReaderToDataTable(clsAccessGroup.List("GroupName", SortOption.Ascending)).DefaultView;
            cboGroup.DataBind();
            cboGroup.SelectedIndex = cboGroup.Items.IndexOf(cboGroup.Items.FindByValue(intGroupID.ToString()));

            clsAccessUser.CommitAndDispose();
        }
        private void LoadRecord()
        {
            AccessType clsAccessType = new AccessType();
            lstAccessCategory.DataSource = clsAccessType.Categories("Category, SequenceNo", SortOption.Ascending).DefaultView;
            clsAccessType.CommitAndDispose();
            lstAccessCategory.DataBind();
        }
        private long SaveRecord()
        {
            long id = Convert.ToInt64(lblUID.Text);

            AccessRights clsAccessRights = new AccessRights();
            AccessRightsDetails clsDetails;
            foreach (DataListItem itemAccessCategory in lstAccessCategory.Items)
            {
                DataList lstItem = (DataList)itemAccessCategory.FindControl("lstItem");
                foreach (DataListItem item in lstItem.Items)
                {
                    HtmlInputCheckBox chkList = (HtmlInputCheckBox)item.FindControl("chkList");
                    HtmlInputCheckBox chkRead = (HtmlInputCheckBox)item.FindControl("chkRead");
                    HtmlInputCheckBox chkWrite = (HtmlInputCheckBox)item.FindControl("chkWrite");

                    clsDetails = new AccessRightsDetails();
                    clsDetails.UID = id;
                    clsDetails.TranTypeID = Convert.ToInt16(chkList.Value);
                    clsDetails.Read = chkRead.Checked;
                    clsDetails.Write = chkWrite.Checked;

                    clsAccessRights.Modify(clsDetails);
                }
            }
            clsAccessRights.CommitAndDispose();

            return id;
        }

    }
}

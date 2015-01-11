using AceSoft.RetailPlus.Security;
using AceSoft.RetailPlus.Data;

namespace AceSoft.RetailPlus.Security._Terminals
{
	using System;
	using System.Data;
	using System.Drawing;
	using System.Web;
	using System.Web.UI.WebControls;
	using System.Web.UI.HtmlControls;
	
	public partial  class __Update : System.Web.UI.UserControl
	{
		
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
			this.imgSaveBack.Click += new System.Web.UI.ImageClickEventHandler(this.imgSaveBack_Click);
			this.imgCancel.Click += new System.Web.UI.ImageClickEventHandler(this.imgCancel_Click);

		}
		#endregion

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
		
		private void imgSave_Click(object sender, System.Web.UI.ImageClickEventArgs e)
		{
			SaveRecord();
			string stParam = "?task=" + Common.Encrypt("add",Session.SessionID);
			Response.Redirect("Default.aspx" + stParam);	
		}

		private void cmdSave_Click(object sender, System.EventArgs e)
		{
			SaveRecord();
			string stParam = "?task=" + Common.Encrypt("add",Session.SessionID);
			Response.Redirect("Default.aspx" + stParam);			
		}

		private void imgSaveBack_Click(object sender, System.Web.UI.ImageClickEventArgs e)
		{
			SaveRecord();
			Response.Redirect(lblReferrer.Text);
		}

		protected void cmdSaveBack_Click(object sender, System.EventArgs e)
		{
			SaveRecord();
			Response.Redirect(lblReferrer.Text);
		}

		private void imgCancel_Click(object sender, System.Web.UI.ImageClickEventArgs e)
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
            foreach (string strEnum in Enum.GetNames(typeof(TerminalReceiptType)))
            {
                TerminalReceiptType itemTerminalReceiptType = (TerminalReceiptType)Enum.Parse(typeof(TerminalReceiptType), strEnum);
                cboReceiptType.Items.Add(new ListItem(strEnum, itemTerminalReceiptType.ToString("d")));
            }
			
            Data.Discount clsDiscount = new Data.Discount();
            cboDiscountCode.DataTextField = "DiscountType";
            cboDiscountCode.DataValueField = "DiscountCode";
            cboDiscountCode.DataSource = clsDiscount.DataList("DiscountCode", SortOption.Ascending).DefaultView;
            cboDiscountCode.DataBind();
            cboDiscountCode.SelectedIndex = cboDiscountCode.Items.Count - 1;

            cboPWDDiscountCode.DataTextField = "DiscountType";
            cboPWDDiscountCode.DataValueField = "DiscountCode";
            cboPWDDiscountCode.DataSource = clsDiscount.DataList("DiscountCode", SortOption.Ascending).DefaultView;
            cboPWDDiscountCode.DataBind();
            cboPWDDiscountCode.SelectedIndex = cboDiscountCode.Items.Count - 1;

            Data.ChargeType clsChargeType = new Data.ChargeType(clsDiscount.Connection, clsDiscount.Transaction);

            cboGroupChargeType.DataTextField = "ChargeTypeCode";
            cboGroupChargeType.DataValueField = "ChargeTypeID";
            cboGroupChargeType.DataSource = clsChargeType.ListAsDataTable().DefaultView;
            cboGroupChargeType.DataBind();
            cboGroupChargeType.Items.Insert(0, new ListItem("", "0"));
            cboGroupChargeType.SelectedIndex = 0;

            cboPersonalChargeType.DataTextField = "ChargeTypeCode";
            cboPersonalChargeType.DataValueField = "ChargeTypeID";
            cboPersonalChargeType.DataSource = clsChargeType.ListAsDataTable().DefaultView;
            cboPersonalChargeType.DataBind();
            cboPersonalChargeType.Items.Insert(0, new ListItem("", "0"));
            cboPersonalChargeType.SelectedIndex = 0;

            cboDefaultTransactionChargeCode.DataTextField = "ChargeTypeCode";
            cboDefaultTransactionChargeCode.DataValueField = "ChargeTypeCode";
            cboDefaultTransactionChargeCode.DataSource = clsChargeType.ListAsDataTable().DefaultView;
            cboDefaultTransactionChargeCode.DataBind();
            cboDefaultTransactionChargeCode.Items.Insert(0, new ListItem("", ""));
            cboDefaultTransactionChargeCode.SelectedIndex = 0;

            cboDineInChargeCode.DataTextField = "ChargeTypeCode";
            cboDineInChargeCode.DataValueField = "ChargeTypeCode";
            cboDineInChargeCode.DataSource = clsChargeType.ListAsDataTable().DefaultView;
            cboDineInChargeCode.DataBind();
            cboDineInChargeCode.Items.Insert(0, new ListItem("", ""));
            cboDineInChargeCode.SelectedIndex = 0;

            cboTakeOutChargeCode.DataTextField = "ChargeTypeCode";
            cboTakeOutChargeCode.DataValueField = "ChargeTypeCode";
            cboTakeOutChargeCode.DataSource = clsChargeType.ListAsDataTable().DefaultView;
            cboTakeOutChargeCode.DataBind();
            cboTakeOutChargeCode.Items.Insert(0, new ListItem("", ""));
            cboTakeOutChargeCode.SelectedIndex = 0;

            cboDeliveryChargeCode.DataTextField = "ChargeTypeCode";
            cboDeliveryChargeCode.DataValueField = "ChargeTypeCode";
            cboDeliveryChargeCode.DataSource = clsChargeType.ListAsDataTable().DefaultView;
            cboDeliveryChargeCode.DataBind();
            cboDeliveryChargeCode.Items.Insert(0, new ListItem("", ""));
            cboDeliveryChargeCode.SelectedIndex = 0;

            clsDiscount.CommitAndDispose();
		}

		private void LoadRecord()
		{

			Int32 TerminalID = Convert.ToInt32(Common.Decrypt(Request.QueryString["id"],Session.SessionID));
			Terminal clsTerminal = new Terminal();
            TerminalDetails clsDetails = clsTerminal.Details(TerminalID);
			clsTerminal.CommitAndDispose();

            lblBranchID.Text = clsDetails.BranchID.ToString();
			lblTerminalID.Text = clsDetails.TerminalID.ToString();
			txtTerminalNo.Text = clsDetails.TerminalNo;
			txtTerminalCode.Text = clsDetails.TerminalCode;
			txtTerminalName.Text = clsDetails.TerminalName;
			txtStatus.Text = clsDetails.Status.ToString("G");
			txtDateCreated.Text = clsDetails.DateCreated.ToString("yyyy-MM-dd HH:mm");
			txtMachineSerialNo.Text = clsDetails.MachineSerialNo;
			txtAccreditationNo.Text = clsDetails.AccreditationNo;

			chkIsPrinterAutoCutter.Checked = clsDetails.IsPrinterAutoCutter;
			cboAutoPrint.SelectedIndex = cboAutoPrint.Items.IndexOf(cboAutoPrint.Items.FindByValue(clsDetails.AutoPrint.ToString("d")));
			chkIsVATInclusive.Checked = clsDetails.IsVATInclusive;

			txtPrinterName.Text = clsDetails.PrinterName;
			txtTurretName.Text = clsDetails.TurretName;
			txtCashDrawerName.Text = clsDetails.CashDrawerName;

			chkItemVoidConfirmation.Checked = clsDetails.ItemVoidConfirmation;
			chkEnableEVAT.Checked = clsDetails.EnableEVAT;
			txtMaxReceiptWidth.Text = clsDetails.MaxReceiptWidth.ToString();

			cboFormBehaviour.SelectedIndex = cboFormBehaviour.Items.IndexOf(cboFormBehaviour.Items.FindByValue(clsDetails.FORM_Behavior));

			txtMarqueeMessage.Text = clsDetails.MarqueeMessage;
			
			if (Session["UserName"].ToString().ToLower() == "admin")
			{
				txtMachineSerialNo.ReadOnly = false;
				txtAccreditationNo.ReadOnly = false;
			}

            // Added May 6, 2009.
            txtVAT.Text = clsDetails.VAT.ToString("##.#0");
            txtEVAT.Text = clsDetails.EVAT.ToString("##.#0");
            txtLocalTax.Text = clsDetails.LocalTax.ToString("##.#0");
            chkShowItemMoreThanZeroQty.Checked = clsDetails.ShowItemMoreThanZeroQty;
            chkShowOnlyPackedTransactions.Checked = clsDetails.ShowOnlyPackedTransactions;
            chkShowOneTerminalSuspendedTransactions.Checked = clsDetails.ShowOneTerminalSuspendedTransactions;
            cboReceiptType.SelectedIndex = cboReceiptType.Items.IndexOf(cboReceiptType.Items.FindByValue(clsDetails.ReceiptType.ToString("d")));
            txtSalesInvoicePrinterName.Text = clsDetails.SalesInvoicePrinterName;
            chkCashCountBeforeReport.Checked = clsDetails.CashCountBeforeReport;
            chkPreviewTerminalReport.Checked = clsDetails.PreviewTerminalReport;

            // Added May 6, 2009.
            chkIsPrinterDotmatrix.Checked = clsDetails.IsPrinterDotMatrix;
            chkIsChargeEditable.Checked = clsDetails.IsChargeEditable;
            chkIsDiscountEditable.Checked = clsDetails.IsDiscountEditable;
            chkCheckCutOffTime.Checked = clsDetails.CheckCutOffTime;
            txtStartCutOffTime.Text = clsDetails.StartCutOffTime;
            txtEndCutOffTime.Text = clsDetails.EndCutOffTime;
            chkWithRestaurantFeatures.Checked = clsDetails.WithRestaurantFeatures;
            cboDiscountCode.SelectedIndex = cboDiscountCode.Items.IndexOf(cboDiscountCode.Items.FindByValue(clsDetails.SeniorCitizenDiscountCode));
            cboPWDDiscountCode.SelectedIndex = cboPWDDiscountCode.Items.IndexOf(cboPWDDiscountCode.Items.FindByValue(clsDetails.PWDDiscountCode));
            chkIsTouchScreen.Checked = clsDetails.IsTouchScreen;
            chkWillContinueSelectionVariation.Checked = clsDetails.WillContinueSelectionVariation;
            chkWillContinueSelectionProduct.Checked = clsDetails.WillContinueSelectionProduct;
            chkWillPrintGrandTotal.Checked = clsDetails.WillPrintGrandTotal;
            chkReservedAndCommit.Checked = clsDetails.ReservedAndCommit;

            // Added Oct 17, 2011
            chkShowCustomerSelection.Checked = clsDetails.ShowCustomerSelection;

            cboGroupChargeType.SelectedIndex = cboGroupChargeType.Items.IndexOf(cboGroupChargeType.Items.FindByValue(clsDetails.GroupChargeType.ChargeTypeID.ToString()));
            cboPersonalChargeType.SelectedIndex = cboPersonalChargeType.Items.IndexOf(cboPersonalChargeType.Items.FindByValue(clsDetails.PersonalChargeType.ChargeTypeID.ToString()));

            // Added Sep 24, 2014
            cboDefaultTransactionChargeCode.SelectedIndex = cboDefaultTransactionChargeCode.Items.IndexOf(cboDefaultTransactionChargeCode.Items.FindByValue(clsDetails.DefaultTransactionChargeCode));
            cboDineInChargeCode.SelectedIndex = cboDineInChargeCode.Items.IndexOf(cboDineInChargeCode.Items.FindByValue(clsDetails.DineInChargeCode));
            cboTakeOutChargeCode.SelectedIndex = cboTakeOutChargeCode.Items.IndexOf(cboTakeOutChargeCode.Items.FindByValue(clsDetails.TakeOutChargeCode));
            cboDeliveryChargeCode.SelectedIndex = cboDeliveryChargeCode.Items.IndexOf(cboDeliveryChargeCode.Items.FindByValue(clsDetails.DeliveryChargeCode));
		}

		private void SaveRecord()
		{
			TerminalDetails clsDetails = new TerminalDetails();

            clsDetails.BranchID = Convert.ToInt32(lblBranchID.Text);
			clsDetails.TerminalID = Convert.ToInt32(lblTerminalID.Text);
			clsDetails.TerminalNo = txtTerminalNo.Text;
			clsDetails.TerminalCode = txtTerminalCode.Text;
			clsDetails.TerminalName = txtTerminalName.Text;
			clsDetails.Status = 0; 
			clsDetails.DateCreated = Convert.ToDateTime(txtDateCreated.Text);
			clsDetails.MachineSerialNo = txtMachineSerialNo.Text;
			clsDetails.AccreditationNo = txtAccreditationNo.Text;
			clsDetails.IsPrinterAutoCutter = Convert.ToBoolean(chkIsPrinterAutoCutter.Checked);
			clsDetails.AutoPrint = (PrintingPreference) Enum.Parse(typeof(PrintingPreference), cboAutoPrint.SelectedItem.Value);
			clsDetails.IsVATInclusive = Convert.ToBoolean(chkIsVATInclusive.Checked);
			clsDetails.PrinterName = txtPrinterName.Text;
			clsDetails.TurretName = txtTurretName.Text;
			clsDetails.CashDrawerName = txtCashDrawerName.Text;
			clsDetails.ItemVoidConfirmation = Convert.ToBoolean(chkItemVoidConfirmation.Checked);
			clsDetails.EnableEVAT = Convert.ToBoolean(chkEnableEVAT.Checked);
			clsDetails.MaxReceiptWidth = Convert.ToInt16(txtMaxReceiptWidth.Text);
			clsDetails.FORM_Behavior = cboFormBehaviour.SelectedItem.Value;
			clsDetails.MarqueeMessage = txtMarqueeMessage.Text;

            // Added May 6, 2009.
            clsDetails.VAT = Convert.ToDecimal(txtVAT.Text);
            clsDetails.EVAT = Convert.ToDecimal(txtEVAT.Text);
            clsDetails.LocalTax = Convert.ToDecimal(txtLocalTax.Text);
            clsDetails.ShowItemMoreThanZeroQty = chkShowItemMoreThanZeroQty.Checked;
            clsDetails.ShowOnlyPackedTransactions = chkShowOnlyPackedTransactions.Checked;
            clsDetails.ShowOneTerminalSuspendedTransactions = chkShowOneTerminalSuspendedTransactions.Checked;
            clsDetails.ReceiptType = (TerminalReceiptType)Enum.Parse(typeof(TerminalReceiptType), cboReceiptType.SelectedItem.Value);
            clsDetails.SalesInvoicePrinterName = txtSalesInvoicePrinterName.Text;
            clsDetails.CashCountBeforeReport = chkCashCountBeforeReport.Checked;
            clsDetails.PreviewTerminalReport = chkPreviewTerminalReport.Checked;

            // Added May 6, 2009.
            clsDetails.IsPrinterDotMatrix = chkIsPrinterDotmatrix.Checked;
            clsDetails.IsChargeEditable = chkIsChargeEditable.Checked;
            clsDetails.IsDiscountEditable = chkIsDiscountEditable.Checked;
            clsDetails.CheckCutOffTime = chkCheckCutOffTime.Checked;
            clsDetails.StartCutOffTime = txtStartCutOffTime.Text;
            clsDetails.EndCutOffTime = txtEndCutOffTime.Text;
            clsDetails.WithRestaurantFeatures = chkWithRestaurantFeatures.Checked;
            
            clsDetails.SeniorCitizenDiscountCode = cboDiscountCode.SelectedItem.Value;
            clsDetails.PWDDiscountCode = cboPWDDiscountCode.SelectedItem.Value;
            // Added May 21, 2009
            clsDetails.IsTouchScreen = chkIsTouchScreen.Checked;

            // Added June 1, 2010
            clsDetails.WillContinueSelectionVariation = chkWillContinueSelectionVariation.Checked;
            
            // Added June 15, 2010
            clsDetails.WillContinueSelectionProduct = chkWillContinueSelectionProduct.Checked;

            // Added Sep 21, 2010
            clsDetails.WillPrintGrandTotal = chkWillPrintGrandTotal.Checked;

            // Added Apr 12, 2011
            clsDetails.ReservedAndCommit = chkReservedAndCommit.Checked;

            // Added Oct 17, 2011
            clsDetails.ShowCustomerSelection = chkShowCustomerSelection.Checked;

            clsDetails.GroupChargeType = new ChargeTypeDetails()
            {
                ChargeTypeID = int.Parse(cboGroupChargeType.SelectedItem.Value),
                ChargeTypeCode = cboGroupChargeType.SelectedItem.Text
            };
            clsDetails.PersonalChargeType = new ChargeTypeDetails()
            {
                ChargeTypeID = int.Parse(cboPersonalChargeType.SelectedItem.Value),
                ChargeTypeCode = cboPersonalChargeType.SelectedItem.Text
            };

            // Added Sep 24, 2014
            clsDetails.DefaultTransactionChargeCode = cboDefaultTransactionChargeCode.SelectedItem.Value;
            clsDetails.DineInChargeCode = cboDineInChargeCode.SelectedItem.Value;
            clsDetails.TakeOutChargeCode = cboTakeOutChargeCode.SelectedItem.Value;
            clsDetails.DeliveryChargeCode = cboDeliveryChargeCode.SelectedItem.Value;

			Terminal clsTerminal = new Terminal();
			clsTerminal.Update(clsDetails);
			clsTerminal.CommitAndDispose();
		}


		#endregion
	}
}


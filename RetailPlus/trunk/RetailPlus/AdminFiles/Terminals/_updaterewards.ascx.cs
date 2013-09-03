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
	
	public partial  class __UpdateRewards : System.Web.UI.UserControl
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
		}

		private void LoadRecord()
		{
			Terminal clsTerminal = new Terminal();
            RewardPointsDetails clsDetails = clsTerminal.RewardPointsDetails();
			clsTerminal.CommitAndDispose();

            chkEnableRewardPoints.Checked = clsDetails.EnableRewardPoints;
            chkRoundDownRewardPoints.Checked = clsDetails.RoundDownRewardPoints;
            txtRewardPointsMinimum.Text = clsDetails.RewardPointsMinimum.ToString("##0.#0");
            txtRewardPointsEvery.Text = clsDetails.RewardPointsEvery.ToString("##0.#0");
            txtRewardPoints.Text = clsDetails.RewardPoints.ToString("##0.#0");

            chkEnableRewardPointsAsPayment.Checked = clsDetails.EnableRewardPointsAsPayment;
            txtRewardPointsMaxPercentageForPayment.Text = clsDetails.RewardPointsMaxPercentageForPayment.ToString("##0.#0");
            txtRewardPointsPaymentValue.Text = clsDetails.RewardPointsPaymentValue.ToString("##0.#0");
            txtRewardPointsPaymentCashEquivalent.Text = clsDetails.RewardPointsPaymentCashEquivalent.ToString("##0.#0");
		}

		private void SaveRecord()
		{
            RewardPointsDetails clsDetails = new RewardPointsDetails();

            clsDetails.EnableRewardPoints = chkEnableRewardPoints.Checked;
            clsDetails.RoundDownRewardPoints = chkRoundDownRewardPoints.Checked;
            clsDetails.RewardPointsMinimum = Convert.ToDecimal(txtRewardPointsMinimum.Text);
            clsDetails.RewardPointsEvery = Convert.ToDecimal(txtRewardPointsEvery.Text);
            clsDetails.RewardPoints = Convert.ToDecimal(txtRewardPoints.Text);

            clsDetails.EnableRewardPointsAsPayment = chkEnableRewardPointsAsPayment.Checked;
            clsDetails.RewardPointsMaxPercentageForPayment = Convert.ToDecimal(txtRewardPointsMaxPercentageForPayment.Text);
            clsDetails.RewardPointsPaymentValue = Convert.ToDecimal(txtRewardPointsPaymentValue.Text);
            clsDetails.RewardPointsPaymentCashEquivalent = Convert.ToDecimal(txtRewardPointsPaymentCashEquivalent.Text);

			Terminal clsTerminal = new Terminal();
			clsTerminal.UpdateRewardPointSystem(clsDetails);
			clsTerminal.CommitAndDispose();
		}


		#endregion
	}
}


namespace AceSoft.RetailPlus.Home.GLA
{
	using System;
	using System.Data;
	using System.Drawing;
	using System.Web;
	using System.Web.UI.WebControls;
	using System.Web.UI.HtmlControls;
	using AceSoft.RetailPlus.Data;
	using System.Xml;
	using System.IO;
    using System.Collections.Generic;


	/// <summary>
	///		Summary description for __Upload.
	/// </summary>
	public partial  class __UploadSales : System.Web.UI.UserControl
	{
        
		#region Web Form Methods

		protected void Page_Load(object sender, System.EventArgs e)
		{
			if (!IsPostBack)
			{
				lblReferrer.Text = Request.UrlReferrer == null ? Constants.ROOT_DIRECTORY : Request.UrlReferrer.ToString();
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
			this.imgUpload.Click += new System.Web.UI.ImageClickEventHandler(this.imgUpload_Click);

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

		private void imgUpload_Click(object sender, System.Web.UI.ImageClickEventArgs e)
		{
			Upload();
		}

		protected void cmdUpload_Click(object sender, System.EventArgs e)
		{
			Upload();
		}


		#endregion

		#region Private Methods

		private void Upload()
		{
            GLATransaction clsTransactionGLA = new GLATransaction();

            try
            {
                string BatchID = string.Empty;

                List<HttpPostedFile> pfiles = new List<HttpPostedFile>();
                List<string> pfilesName = new List<string>();

                for (int i = 0; i < Request.Files.Count; i++)
                {
                    HttpPostedFile file = Request.Files[i];
                    if (file.ContentLength > 0)
                    {
                        pfiles.Add(file); pfilesName.Add(System.IO.Path.GetFileName(file.FileName.ToLower()));
                    }
                }

                Label1.Text = "";

                #region checking

                // Label1.Text += "Files to upload:";
                //Label1.Text += "      "
                if (pfilesName.IndexOf(Constants.GLA_file_batch_id) < 0)
                    Label1.Text += "<br /><br /><b><font class='ms-error'>Please include the batch file in the upload. Filename: " + Constants.GLA_file_batch_id + "</font></b><br />";
                if (pfilesName.IndexOf(Constants.GLA_file_d_dsc_def) < 0)
                    Label1.Text += "<br /><br /><b><font class='ms-error'>Please include the discount file in the upload. Filename: " + Constants.GLA_file_d_dsc_def + "</font></b><br />";
                if (pfilesName.IndexOf(Constants.GLA_file_d_emp_def) < 0)
                    Label1.Text += "<br /><br /><b><font class='ms-error'>Please include the employees file in the upload. Filename: " + Constants.GLA_file_d_emp_def + "</font></b><br />";
                if (pfilesName.IndexOf(Constants.GLA_file_d_location_def) < 0)
                    Label1.Text += "<br /><br /><b><font class='ms-error'>Please include the location file in the upload. Filename: " + Constants.GLA_file_d_location_def + "</font></b><br />";
                //do not check the item. it's ok not to upload the item
                //if (pfilesName.IndexOf(Constants.GLA_file_d_mi_def) < 0)
                //    Label1.Text += "<br /><br /><b><font class='ms-error'>Please include the menu items file in the upload. Filename: " + Constants.GLA_file_d_mi_def + "</font></b><br />";
                if (pfilesName.IndexOf(Constants.GLA_file_d_svc_def) < 0)
                    Label1.Text += "<br /><br /><b><font class='ms-error'>Please include the service charge file in the upload. Filename: " + Constants.GLA_file_d_svc_def + "</font></b><br />";
                if (pfilesName.IndexOf(Constants.GLA_file_d_tmd_def) < 0)
                    Label1.Text += "<br /><br /><b><font class='ms-error'>Please include the tender definitions file in the upload. Filename: " + Constants.GLA_file_d_tmd_def + "</font></b><br />";
                if (pfilesName.IndexOf(Constants.GLA_file_f_dtl_chk_dsc) < 0)
                    Label1.Text += "<br /><br /><b><font class='ms-error'>Please include the discount details file in the upload. Filename: " + Constants.GLA_file_f_dtl_chk_dsc + "</font></b><br />";
                if (pfilesName.IndexOf(Constants.GLA_file_f_dtl_chk_headers) < 0)
                    Label1.Text += "<br /><br /><b><font class='ms-error'>Please include the check information file in the upload. Filename: " + Constants.GLA_file_f_dtl_chk_headers + "</font></b><br />";
                if (pfilesName.IndexOf(Constants.GLA_file_f_dtl_chk_svc) < 0)
                    Label1.Text += "<br /><br /><b><font class='ms-error'>Please include the service charge file in the upload. Filename: " + Constants.GLA_file_f_dtl_chk_svc + "</font></b><br />";
                if (pfilesName.IndexOf(Constants.GLA_file_f_dtl_chk_tmd) < 0)
                    Label1.Text += "<br /><br /><b><font class='ms-error'>Please include the tender detail file in the upload. Filename: " + Constants.GLA_file_f_dtl_chk_tmd + "</font></b><br />";
                //if (pfilesName.IndexOf(Constants.GLA_file_otntender) < 0)
                //    Label1.Text += "<br /><br /><b><font class='ms-error'>Please include the OTN_Tender file in the upload. Filename: " + Constants.GLA_file_otntender + "</font></b><br />";
                #endregion

                if (!string.IsNullOrEmpty(Label1.Text)) return;

                string strfile = "";
                string strfolder = "/RetailPlus/temp/uploaded/gla/";
                Security.AccessUserDetails clsAccessUserDetails = (Security.AccessUserDetails)Session["AccessUserDetails"];
                DateTime DateCreated = DateTime.Now;
                int iTranCount = 0;

                #region Constants.GLA_file_batch
                if (1==1)    //process batchfile
                {
                    strfile = Server.MapPath(strfolder + Constants.GLA_file_batch_id);

                    if (System.IO.File.Exists(strfile))
                        System.IO.File.Delete(strfile);

                    pfiles[pfilesName.IndexOf(Constants.GLA_file_batch_id)].SaveAs(strfile);

                    using (var reader = new StreamReader(strfile))
                    {
                        string line;
                        while ((line = reader.ReadLine()) != null)
                        {
                            //get the firstline as the batchid
                            BatchID = line;
                            break;
                        }
                    }
                }

                if (string.IsNullOrEmpty(BatchID))
                {
                    Label1.Text += "<br /><br /><b><font class='ms-error'>Cannot get any information from batch file. Please double check the batch file: " + Constants.GLA_file_batch_id + "</font></b><br />";
                    return;
                }

                Label1.Text += "<br /><br /><b><font class='ms-error'>Uploading Batch ID: " + BatchID + "</font></b><br />";
                #endregion

                #region saveallfiles

                // delete the temporary batch file
                if (System.IO.File.Exists(strfile))
                    System.IO.File.Delete(strfile);

                // save it again in a folder
                strfolder = strfolder + BatchID + "/";

                foreach (HttpPostedFile pfile in pfiles)
                {
                    strfile = Server.MapPath(strfolder + System.IO.Path.GetFileName(pfile.FileName));

                    if (!System.IO.Directory.Exists(Server.MapPath(strfolder)))
                        System.IO.Directory.CreateDirectory(Server.MapPath(strfolder));
                    
                    if (System.IO.File.Exists(strfile))
                        System.IO.File.Delete(strfile);

                    pfile.SaveAs(strfile);
                }

                //pfiles[pfilesName.IndexOf(Constants.GLA_file_batch)].SaveAs(strfile);
                //pfiles[pfilesName.IndexOf(Constants.GLA_file_batch)].SaveAs(strfile + Constants.GLA_file_batch);

                #endregion

                #region GLA_file_d_dsc_def
                /***** upload the transaction *****/
                Label1.Text += "Processing " + Constants.GLA_file_d_dsc_def + " ...";
                strfile = Server.MapPath(strfolder + System.IO.Path.GetFileName(Constants.GLA_file_d_dsc_def));

                IList<GLADiscountDetails> lstGLADiscountDetails = new List<GLADiscountDetails>();
                GLADiscountDetails clsGLADiscountDetails = new GLADiscountDetails();
                iTranCount = 0;
                using (var reader = new StreamReader(strfile))
                {
                    string line;
                    while ((line = reader.ReadLine()) != null)
                    {
                        clsGLADiscountDetails = setDscDetails(line, DateCreated, clsAccessUserDetails.Name, Constants.GLA_file_d_dsc_def, BatchID);
                        lstGLADiscountDetails.Add(clsGLADiscountDetails);

                        iTranCount++;
                    }
                }

                GLADiscount clsGLADiscount = new GLADiscount(clsTransactionGLA.Connection, clsTransactionGLA.Transaction);
                clsGLADiscount.Delete(BatchID);
                foreach (GLADiscountDetails det in lstGLADiscountDetails)
                {
                    clsGLADiscount.Insert(det);
                }
                Label1.Text += "      <b><font class='ms-error'>" + lstGLADiscountDetails.Count.ToString() + " has been successfully uploaded...</font></b><br />";
                
                #endregion

                #region GLA_file_d_emp_def
                /***** upload the transaction *****/
                Label1.Text += "Processing " + Constants.GLA_file_d_emp_def + " ...";
                strfile = Server.MapPath(strfolder + System.IO.Path.GetFileName(Constants.GLA_file_d_emp_def));

                IList<GLAEmployeeDetails> lstGLAEmployeeDetails = new List<GLAEmployeeDetails>();
                GLAEmployeeDetails clsGLAEmployeeDetails = new GLAEmployeeDetails();
                iTranCount = 0;
                using (var reader = new StreamReader(strfile))
                {
                    string line;
                    while ((line = reader.ReadLine()) != null)
                    {
                        clsGLAEmployeeDetails = setEmpDetails(line, DateCreated, clsAccessUserDetails.Name, Constants.GLA_file_d_emp_def, BatchID);
                        lstGLAEmployeeDetails.Add(clsGLAEmployeeDetails);

                        iTranCount++;
                    }
                }

                GLAEmployee clsGLAEmployee = new GLAEmployee(clsTransactionGLA.Connection, clsTransactionGLA.Transaction);
                clsGLAEmployee.Delete(BatchID);
                foreach (GLAEmployeeDetails det in lstGLAEmployeeDetails)
                {
                    clsGLAEmployee.Insert(det);
                }
                Label1.Text += "      <b><font class='ms-error'>" + lstGLAEmployeeDetails.Count.ToString() + " has been successfully uploaded...</font></b><br />";
                /***** upload the discount end *****/
                #endregion

                #region GLA_file_d_location_def
                /***** upload the transaction *****/
                Label1.Text += "Processing " + Constants.GLA_file_d_location_def + " ...";
                strfile = Server.MapPath(strfolder + System.IO.Path.GetFileName(Constants.GLA_file_d_location_def));

                IList<GLALocationDetails> lstGLALocationDetails = new List<GLALocationDetails>();
                GLALocationDetails clsGLALocationDetails = new GLALocationDetails();
                iTranCount = 0;
                using (var reader = new StreamReader(strfile))
                {
                    string line;
                    while ((line = reader.ReadLine()) != null)
                    {
                        clsGLALocationDetails = setLocDetails(line, DateCreated, clsAccessUserDetails.Name, Constants.GLA_file_d_location_def, BatchID);
                        lstGLALocationDetails.Add(clsGLALocationDetails);

                        iTranCount++;
                    }
                }

                GLALocation clsGLALocation = new GLALocation(clsTransactionGLA.Connection, clsTransactionGLA.Transaction);
                clsGLALocation.Delete(BatchID);
                foreach (GLALocationDetails det in lstGLALocationDetails)
                {
                    clsGLALocation.Insert(det);
                }
                Label1.Text += "      <b><font class='ms-error'>" + lstGLALocationDetails.Count.ToString() + " has been successfully uploaded...</font></b><br />";
                /***** upload the discount end *****/
                #endregion

                #region GLA_file_d_mi_def
                /***** upload the transaction *****/
                if (pfilesName.IndexOf(Constants.GLA_file_d_mi_def) >= 0)
                {
                    Label1.Text += "Processing " + Constants.GLA_file_d_mi_def + " ...";
                    strfile = Server.MapPath(strfolder + System.IO.Path.GetFileName(Constants.GLA_file_d_mi_def));

                    IList<GLAItemDetails> lstGLAItemDetails = new List<GLAItemDetails>();
                    GLAItemDetails clsGLAItemDetails = new GLAItemDetails();
                    iTranCount = 0;
                    using (var reader = new StreamReader(strfile))
                    {
                        string line;
                        while ((line = reader.ReadLine()) != null)
                        {
                            clsGLAItemDetails = setItemDetails(line, DateCreated, clsAccessUserDetails.Name, Constants.GLA_file_d_mi_def, BatchID);
                            lstGLAItemDetails.Add(clsGLAItemDetails);

                            iTranCount++;
                        }
                    }

                    if (lstGLAItemDetails.Count > 0)
                    {
                        GLAItem clsGLAItem = new GLAItem(clsTransactionGLA.Connection, clsTransactionGLA.Transaction);
                        clsGLAItem.Delete(BatchID);
                        foreach (GLAItemDetails det in lstGLAItemDetails)
                        {
                            clsGLAItem.Insert(det);
                        }
                    }
                    Label1.Text += "      <b><font class='ms-error'>" + lstGLAItemDetails.Count.ToString() + " has been successfully uploaded...</font></b><br />";
                }
                /***** upload the discount end *****/
                #endregion

                #region GLA_file_d_svc_def
                /***** upload the transaction *****/
                Label1.Text += "Processing " + Constants.GLA_file_d_svc_def + " ...";
                strfile = Server.MapPath(strfolder + System.IO.Path.GetFileName(Constants.GLA_file_d_svc_def));

                IList<GLAServiceChargeDetails> lstGLAServiceChargeDetails = new List<GLAServiceChargeDetails>();
                GLAServiceChargeDetails clsGLAServiceChargeDetailss = new GLAServiceChargeDetails();
                iTranCount = 0;
                using (var reader = new StreamReader(strfile))
                {
                    string line;
                    while ((line = reader.ReadLine()) != null)
                    {
                        clsGLAServiceChargeDetailss = setSvcDetails(line, DateCreated, clsAccessUserDetails.Name, Constants.GLA_file_d_svc_def, BatchID);
                        lstGLAServiceChargeDetails.Add(clsGLAServiceChargeDetailss);

                        iTranCount++;
                    }
                }

                GLAServiceCharge clsGLAServiceCharge = new GLAServiceCharge(clsTransactionGLA.Connection, clsTransactionGLA.Transaction);
                clsGLAServiceCharge.Delete(BatchID);
                foreach (GLAServiceChargeDetails det in lstGLAServiceChargeDetails)
                {
                    clsGLAServiceCharge.Insert(det);
                }
                Label1.Text += "      <b><font class='ms-error'>" + lstGLAServiceChargeDetails.Count.ToString() + " has been successfully uploaded...</font></b><br />";
                /***** upload the discount end *****/
                #endregion

                #region GLA_file_d_tmd_def
                /***** upload the tenders *****/
                Label1.Text += "Processing " + Constants.GLA_file_d_tmd_def + " ...";
                strfile = Server.MapPath(strfolder + System.IO.Path.GetFileName(Constants.GLA_file_d_tmd_def));

                IList<GLATenderDetails> lstGLATenderDetails = new List<GLATenderDetails>();
                GLATenderDetails clsGLATenderDetails = new GLATenderDetails();
                iTranCount = 0;
                using (var reader = new StreamReader(strfile))
                {
                    string line;
                    while ((line = reader.ReadLine()) != null)
                    {
                        clsGLATenderDetails = setTenderDetails(line, DateCreated, clsAccessUserDetails.Name, Constants.GLA_file_d_tmd_def, BatchID);
                        lstGLATenderDetails.Add(clsGLATenderDetails);

                        iTranCount++;
                    }
                }

                GLATender clsGLATender = new GLATender(clsTransactionGLA.Connection, clsTransactionGLA.Transaction);
                clsGLATender.Delete(BatchID);
                foreach (GLATenderDetails det in lstGLATenderDetails)
                {
                    clsGLATender.Insert(det);
                }
                Label1.Text += "      <b><font class='ms-error'>" + lstGLATenderDetails.Count.ToString() + " has been successfully uploaded...</font></b><br />";
                
                /***** upload the discount end *****/
                #endregion

                #region GLA_file_f_dtl_chk_dsc
                /***** upload the transaction *****/
                Label1.Text += "Processing " + Constants.GLA_file_f_dtl_chk_dsc + " ...";
                strfile = Server.MapPath(strfolder + System.IO.Path.GetFileName(Constants.GLA_file_f_dtl_chk_dsc));

                IList<GLATransactionDiscountDetails> lstGLATransactionDiscountDetails = new List<GLATransactionDiscountDetails>();
                IList<GLATransactionDiscountDetails> lstGLATransactionDiscountDetailsContactCode = new List<GLATransactionDiscountDetails>();
                GLATransactionDiscountDetails clsGLATransactionDiscountDetails = new GLATransactionDiscountDetails();
                iTranCount = 0;
                using (var reader = new StreamReader(strfile))
                {
                    string line;
                    while ((line = reader.ReadLine()) != null)
                    {
                        clsGLATransactionDiscountDetails = setTranDscDetails(line, DateCreated, clsAccessUserDetails.Name, Constants.GLA_file_f_dtl_chk_dsc, BatchID);
                        lstGLATransactionDiscountDetails.Add(clsGLATransactionDiscountDetails);

                        if (!string.IsNullOrEmpty(clsGLATransactionDiscountDetails.ContactCode)) lstGLATransactionDiscountDetailsContactCode.Add(clsGLATransactionDiscountDetails);
                        iTranCount++;
                    }
                }

                GLATransactionDiscount clsGLATransactionDiscount = new GLATransactionDiscount(clsTransactionGLA.Connection, clsTransactionGLA.Transaction);
                clsGLADiscount.Delete(BatchID);
                foreach (GLATransactionDiscountDetails det in lstGLATransactionDiscountDetails)
                {
                    clsGLATransactionDiscount.Insert(det);
                }
                Label1.Text += "      <b><font class='ms-error'>" + lstGLATransactionDiscountDetails.Count.ToString() + " has been successfully uploaded...</font></b><br />";
                /***** upload the discount end *****/
                #endregion

                #region GLA_file_f_dtl_chk_headers
                /***** upload the transaction *****/
                Label1.Text += "Processing " + Constants.GLA_file_f_dtl_chk_headers + " ...";
                strfile = Server.MapPath(strfolder + System.IO.Path.GetFileName(Constants.GLA_file_f_dtl_chk_headers));

                List<GLATransactionDetails> lstGLATransactionDetails = new List<GLATransactionDetails>();
                GLATransactionDetails clsGLATransactionDetails = new GLATransactionDetails();
                iTranCount = 0;
                using (var reader = new StreamReader(strfile))
                {
                    string line;
                    while ((line = reader.ReadLine()) != null)
                    {
                        clsGLATransactionDetails = setTranHdrDetails(line, DateCreated, clsAccessUserDetails.Name, Constants.GLA_file_f_dtl_chk_headers, BatchID);
                        lstGLATransactionDetails.Add(clsGLATransactionDetails);

                        iTranCount++;
                    }
                }
                GLATransaction clsGLATransaction = new GLATransaction(clsTransactionGLA.Connection, clsTransactionGLA.Transaction);
                clsGLATransaction.Delete(BatchID);
                foreach (GLATransactionDetails det in lstGLATransactionDetails)
                {
                    clsGLATransaction.Insert(det);
                }
                Label1.Text += "      <b><font class='ms-error'>" + lstGLATransactionDetails.Count.ToString() + " has been successfully uploaded...</font></b><br />";
                /***** upload the transaction end *****/
                #endregion

                #region GLA_file_f_dtl_chk_mi
                /***** upload the transaction *****/
                Label1.Text += "Processing " + Constants.GLA_file_f_dtl_chk_mi + " ...";
                strfile = Server.MapPath(strfolder + System.IO.Path.GetFileName(Constants.GLA_file_f_dtl_chk_mi));

                IList<GLATransactionItemDetails> lstGLATransactionItemDetails = new List<GLATransactionItemDetails>();
                IList<GLATransactionItemDetails> lstGLATransactionItemDetailsContactCode = new List<GLATransactionItemDetails>();
                GLATransactionItemDetails clsGLATransactionItemDetails = new GLATransactionItemDetails();
                iTranCount = 0;
                using (var reader = new StreamReader(strfile))
                {
                    string line;
                    while ((line = reader.ReadLine()) != null)
                    {
                        clsGLATransactionItemDetails = setTranItemDetails(line, DateCreated, clsAccessUserDetails.Name, Constants.GLA_file_f_dtl_chk_mi, BatchID);
                        lstGLATransactionItemDetails.Add(clsGLATransactionItemDetails);
                        iTranCount++;
                    }
                }

                GLATransactionItem clsGLATransactionItem = new GLATransactionItem(clsTransactionGLA.Connection, clsTransactionGLA.Transaction);
                clsGLATransactionItem.Delete(BatchID);
                foreach (GLATransactionItemDetails det in lstGLATransactionItemDetails)
                {
                    clsGLATransactionItem.Insert(det);
                }
                Label1.Text += "      <b><font class='ms-error'>" + lstGLATransactionItemDetails.Count.ToString() + " has been successfully uploaded...</font></b><br />";
                /***** upload the discount end *****/
                #endregion

                #region GLA_file_f_dtl_chk_svc
                /***** upload the transaction *****/
                Label1.Text += "Processing " + Constants.GLA_file_f_dtl_chk_svc + " ...";
                strfile = Server.MapPath(strfolder + System.IO.Path.GetFileName(Constants.GLA_file_f_dtl_chk_svc));

                IList<GLATransactionSvcDetails> lstGLATransactionSvcDetails = new List<GLATransactionSvcDetails>();
                IList<GLATransactionSvcDetails> lstGLATransactionSvcDetailsContactCode = new List<GLATransactionSvcDetails>();
                GLATransactionSvcDetails clsGLATransactionSvcDetails = new GLATransactionSvcDetails();
                iTranCount = 0;
                using (var reader = new StreamReader(strfile))
                {
                    string line;
                    while ((line = reader.ReadLine()) != null)
                    {
                        clsGLATransactionSvcDetails = setTranSvcDetails(line, DateCreated, clsAccessUserDetails.Name, Constants.GLA_file_f_dtl_chk_svc, BatchID);
                        lstGLATransactionSvcDetails.Add(clsGLATransactionSvcDetails);
                        iTranCount++;
                    }
                }

                GLATransactionSvc clsGLATransactionSvc = new GLATransactionSvc(clsTransactionGLA.Connection, clsTransactionGLA.Transaction);
                clsGLATransactionSvc.Delete(BatchID);
                foreach (GLATransactionSvcDetails det in lstGLATransactionSvcDetails)
                {
                    clsGLATransactionSvc.Insert(det);
                }
                Label1.Text += "      <b><font class='ms-error'>" + lstGLATransactionSvcDetails.Count.ToString() + " has been successfully uploaded...</font></b><br />";
                /***** upload the discount end *****/
                #endregion

                #region GLA_file_f_dtl_chk_tmd
                /***** upload the transaction *****/
                Label1.Text += "Processing " + Constants.GLA_file_f_dtl_chk_tmd + " ...";
                strfile = Server.MapPath(strfolder + System.IO.Path.GetFileName(Constants.GLA_file_f_dtl_chk_tmd));

                IList<GLATransactionTenderDetails> lstGLATransactionTenderDetails = new List<GLATransactionTenderDetails>();
                IList<GLATransactionTenderDetails> lstGLATransactionTenderDetailsContactCode = new List<GLATransactionTenderDetails>();
                GLATransactionTenderDetails clsGLATransactionTenderDetails = new GLATransactionTenderDetails();
                iTranCount = 0;
                using (var reader = new StreamReader(strfile))
                {
                    string line;
                    while ((line = reader.ReadLine()) != null)
                    {
                        clsGLATransactionTenderDetails = setTranTenderDetails(line, DateCreated, clsAccessUserDetails.Name, Constants.GLA_file_f_dtl_chk_tmd, BatchID);
                        lstGLATransactionTenderDetails.Add(clsGLATransactionTenderDetails);
                        iTranCount++;
                    }
                }

                GLATransactionTender clsGLATransactionTender = new GLATransactionTender(clsTransactionGLA.Connection, clsTransactionGLA.Transaction);
                clsGLATransactionTender.Delete(BatchID);
                foreach (GLATransactionTenderDetails det in lstGLATransactionTenderDetails)
                {
                    clsGLATransactionTender.Insert(det);
                }
                Label1.Text += "      <b><font class='ms-error'>" + lstGLATransactionTenderDetails.Count.ToString() + " has been successfully uploaded...</font></b><br />";
                /***** upload the discount end *****/
                #endregion

                #region clsTransaction

                Label1.Text += "Processing transactions by members... ";

                Data.SalesTransactions clsTransaction = new SalesTransactions(clsTransactionGLA.Connection, clsTransactionGLA.Transaction);
                clsTransaction.DeleteByDataSource(BatchID);
                clsTransactionGLA.ProcessBatch(BatchID);

                Label1.Text += "      <b><font class='ms-error'>" + lstGLATransactionDiscountDetailsContactCode.Count.ToString() + " has been successfully uploaded...</font></b><br />";
                
                #endregion

                #region GLA_file_otntender
                ///***** upload the otn tender *****/
                //Label1.Text += "Processing " + Constants.GLA_file_otntender + " ...";
                //strfile = Server.MapPath(strfolder + System.IO.Path.GetFileName(Constants.GLA_file_otntender));

                //List<OrderTenderGLADetails> lstOrderTenderGLADetails = new List<OrderTenderGLADetails>();
                //OrderTenderGLADetails clsOrderTenderGLADetails = new OrderTenderGLADetails();
                //iTranCount = 0;
                //using (var reader = new StreamReader(strfile))
                //{
                //    string line;
                //    while ((line = reader.ReadLine()) != null)
                //    {
                //        clsOrderTenderGLADetails = setOrderTenderDetails(line, DateCreated, clsAccessUserDetails.Name, Constants.GLA_file_otntender, BatchID);
                //        lstOrderTenderGLADetails.Add(clsOrderTenderGLADetails);

                //        iTranCount++;
                //    }
                //}

                //OrderTenderGLA clsOrderTenderGLA = new OrderTenderGLA(clsTransactionGLA.Connection, clsTransactionGLA.Transaction);

                //Data.Contacts clsContacts = new Contacts(clsTransactionGLA.Connection, clsTransactionGLA.Transaction);
                //Data.ContactDetails clsContactDetails = new ContactDetails();

                //foreach (OrderTenderGLADetails det in lstOrderTenderGLADetails)
                //{

                //    clsOrderTenderGLA.Insert(det);
                //    clsSalesTransactionDetails = clsTransaction.Details(det.tender_seq.ToString(), Constants.C_DEFAULT_TERMINAL_01, Constants.BRANCH_ID_MAIN);
                        
                //    // update the customer information
                //    clsSalesTransactionDetails = setSalesTransactionDetails(det, clsSalesTransactionDetails);
                //    clsContactDetails = clsContacts.Details(det.auth_acct_no);

                //    clsTransaction.UpdateContact(clsSalesTransactionDetails.TransactionID, clsSalesTransactionDetails.TransactionDate, clsContactDetails);
                        
                //    //clsTransaction.Close(clsSalesTransactionDetails.TransactionID, clsSalesTransactionDetails.SubTotal, clsSalesTransactionDetails.ItemsDiscount, clsSalesTransactionDetails.Discount, clsSalesTransactionDetails.TransDiscount, clsSalesTransactionDetails.TransDiscountType, clsSalesTransactionDetails.VAT, clsSalesTransactionDetails.VatableAmount, clsSalesTransactionDetails.EVAT, clsSalesTransactionDetails.EVatableAmount, clsSalesTransactionDetails.LocalTax, clsSalesTransactionDetails.AmountPaid, clsSalesTransactionDetails.CashPayment, clsSalesTransactionDetails.ChequePayment, clsSalesTransactionDetails.CreditCardPayment, clsSalesTransactionDetails.CreditPayment, clsSalesTransactionDetails.DebitPayment, clsSalesTransactionDetails.RewardPointsPayment, clsSalesTransactionDetails.RewardConvertedPayment, clsSalesTransactionDetails.BalanceAmount, clsSalesTransactionDetails.ChangeAmount, clsSalesTransactionDetails.PaymentType, clsSalesTransactionDetails.DiscountCode, clsSalesTransactionDetails.DiscountRemarks, clsSalesTransactionDetails.Charge, clsSalesTransactionDetails.ChargeAmount, clsSalesTransactionDetails.ChargeCode, clsSalesTransactionDetails.ChargeRemarks, clsSalesTransactionDetails.CashierID, clsSalesTransactionDetails.CashierName);
                //    //clsTransaction.UpdateDateClosed(clsSalesTransactionDetails.TransactionID, clsSalesTransactionDetails.DateClosed);
                //}

                //Label1.Text += "      <b><font class='ms-error'>" + lstOrderTenderGLADetails.Count.ToString() + " has been successfully uploaded...</font></b><br />";
                #endregion

                clsTransactionGLA.CommitAndDispose();
            }
            catch (Exception ex){
                clsTransactionGLA.ThrowException(ex);
                throw ex;
            }
		}


        
        private SalesTransactionDetails setSalesTransactionDetails(GLATransactionDetails glaDetails)
        {
            SalesTransactionDetails mclsSalesTransactionDetails = new SalesTransactionDetails();

            // for insert
            mclsSalesTransactionDetails.CustomerID = glaDetails.fk_emp_def;
            mclsSalesTransactionDetails.AgentID = Constants.C_RETAILPLUS_AGENTID;
            mclsSalesTransactionDetails.AgentName = Constants.C_RETAILPLUS_AGENT;
            mclsSalesTransactionDetails.AgentPositionName = Constants.C_RETAILPLUS_AGENT_POSITIONNAME;
            mclsSalesTransactionDetails.AgentDepartmentName = Constants.C_RETAILPLUS_AGENT_DEPARTMENT_NAME;
            mclsSalesTransactionDetails.WaiterID = Constants.C_RETAILPLUS_WAITERID;
            mclsSalesTransactionDetails.WaiterName = Constants.C_RETAILPLUS_WAITER;
            mclsSalesTransactionDetails.CreatedByID = glaDetails.fk_emp_def;
            mclsSalesTransactionDetails.CreatedByName = Constants.C_RETAILPLUS_WAITER;
            mclsSalesTransactionDetails.CashierID = glaDetails.fk_emp_def;
            mclsSalesTransactionDetails.CashierName = glaDetails.Filename;
            mclsSalesTransactionDetails.CustomerID = Constants.C_RETAILPLUS_CUSTOMERID;
            mclsSalesTransactionDetails.CustomerName = Constants.C_RETAILPLUS_CUSTOMER;
            mclsSalesTransactionDetails.TransactionDate = glaDetails.Chk_Open_Date_Time;
            mclsSalesTransactionDetails.DateSuspended = Constants.C_DATE_MIN_VALUE;
            mclsSalesTransactionDetails.TerminalNo = Constants.C_DEFAULT_TERMINAL_01;
            mclsSalesTransactionDetails.BranchID = Constants.BRANCH_ID_MAIN;
            mclsSalesTransactionDetails.BranchCode = Constants.BRANCH_MAIN;
            mclsSalesTransactionDetails.TransactionStatus = TransactionStatus.Closed;
            mclsSalesTransactionDetails.TransactionType = TransactionTypes.POSNormal;
            mclsSalesTransactionDetails.TransactionNo = glaDetails.chk_headers_seq_number.ToString();

            //for update
            mclsSalesTransactionDetails.Charge = glaDetails.Auto_Svc_Ttl + glaDetails.Other_Svc_Ttl + glaDetails.Tip_ttl;
            mclsSalesTransactionDetails.Discount = -glaDetails.Dsc_Ttl;
            mclsSalesTransactionDetails.AmountDue = glaDetails.Sub_Ttl + glaDetails.Tax_Ttl + mclsSalesTransactionDetails.Charge - mclsSalesTransactionDetails.Discount;
            mclsSalesTransactionDetails.SubTotal = glaDetails.Sub_Ttl + glaDetails.Tax_Ttl + mclsSalesTransactionDetails.Charge;

            mclsSalesTransactionDetails.DiscountableAmount = mclsSalesTransactionDetails.Discount <= 0 ? 0 : glaDetails.Dsc_Ttl;
            mclsSalesTransactionDetails.ItemsDiscount = 0;
            mclsSalesTransactionDetails.VAT = glaDetails.Tax_Ttl;
            mclsSalesTransactionDetails.VatableAmount = glaDetails.Sub_Ttl + glaDetails.Tax_Ttl;
            mclsSalesTransactionDetails.NonVATableAmount = 0;
            mclsSalesTransactionDetails.EVAT = 0;
            mclsSalesTransactionDetails.EVatableAmount = 0;
            mclsSalesTransactionDetails.NonEVATableAmount = glaDetails.Sub_Ttl + glaDetails.Tax_Ttl;
            mclsSalesTransactionDetails.LocalTax = 0;
            mclsSalesTransactionDetails.TotalItemSold = glaDetails.Cov_Cnt;
            mclsSalesTransactionDetails.TotalQuantitySold = glaDetails.Num_Dtl;

            mclsSalesTransactionDetails.AmountPaid = glaDetails.Pymnt_Ttl;
            mclsSalesTransactionDetails.CashPayment = mclsSalesTransactionDetails.SubTotal;
            mclsSalesTransactionDetails.ChangeAmount = glaDetails.Pymnt_Ttl; 
            mclsSalesTransactionDetails.ChequePayment = 0;
            mclsSalesTransactionDetails.CreditCardPayment = 0;
            mclsSalesTransactionDetails.CreditPayment = 0;
            mclsSalesTransactionDetails.CreditChargeAmount = 0;
            mclsSalesTransactionDetails.DebitPayment = 0;
            mclsSalesTransactionDetails.RewardPointsPayment = 0;
            mclsSalesTransactionDetails.DateClosed = glaDetails.Chk_Closed_Date_Time;
            mclsSalesTransactionDetails.DateResumed = glaDetails.DateCreated;
            mclsSalesTransactionDetails.DataSource = glaDetails.BatchID;

            return mclsSalesTransactionDetails;
        }


        private GLADiscountDetails setDscDetails(string line, DateTime DateCreated, string CreatedBy, string filename, string BatchID)
        {
            DateTime dteRetvalue = Constants.C_DATE_MIN_VALUE;
            int intRetValue = 0;
            decimal decRetValue = 0;
            long lngRetValue = 0;

            GLADiscountDetails clsDetails = new GLADiscountDetails();

            int iCol = 0;
            foreach (string col in line.Split(','))
            {
                dteRetvalue = Constants.C_DATE_MIN_VALUE;
                intRetValue = 0;

                switch (iCol)
                {
                    case 0: clsDetails.Dsc_Number = long.TryParse(col, out lngRetValue) ? lngRetValue : 0; break;
                    case 1: clsDetails.Dsc_Name = col; break;
                    case 2: clsDetails.Is_HotelMark_Promo = Convert.ToBoolean(int.TryParse(col, out intRetValue) ? intRetValue : 0); break;
                }
                iCol++;
            }
            clsDetails.DateCreated = DateCreated;
            clsDetails.CreatedBy = CreatedBy;
            clsDetails.Filename = filename;
            clsDetails.BatchID = BatchID;

            return clsDetails;
        }
        private GLAEmployeeDetails setEmpDetails(string line, DateTime DateCreated, string CreatedBy, string filename, string BatchID)
        {
            DateTime dteRetvalue = Constants.C_DATE_MIN_VALUE;
            int intRetValue = 0;
            decimal decRetValue = 0;
            long lngRetValue = 0;

            GLAEmployeeDetails clsDetails = new GLAEmployeeDetails();

            int iCol = 0;
            foreach (string col in line.Split(','))
            {
                dteRetvalue = Constants.C_DATE_MIN_VALUE;
                intRetValue = 0;

                switch (iCol)
                {
                    case 0: clsDetails.Emp_Number = long.TryParse(col, out lngRetValue) ? lngRetValue : 0; break;
                    case 1: clsDetails.First_Name = col; break;
                    case 2: clsDetails.Last_Name = col; break;
                    case 3: clsDetails.Class_Number = long.TryParse(col, out lngRetValue) ? lngRetValue : 0; break;
                    case 4: clsDetails.Class_Name = col; break;
                }
                iCol++;
            }
            clsDetails.DateCreated = DateCreated;
            clsDetails.CreatedBy = CreatedBy;
            clsDetails.Filename = filename;
            clsDetails.BatchID = BatchID;

            return clsDetails;
        }
        private GLALocationDetails setLocDetails(string line, DateTime DateCreated, string CreatedBy, string filename, string BatchID)
        {
            DateTime dteRetvalue = Constants.C_DATE_MIN_VALUE;
            int intRetValue = 0;
            decimal decRetValue = 0;
            long lngRetValue = 0;

            GLALocationDetails clsDetails = new GLALocationDetails();

            int iCol = 0;
            foreach (string col in line.Split(','))
            {
                dteRetvalue = Constants.C_DATE_MIN_VALUE;
                intRetValue = 0;

                switch (iCol)
                {
                    case 0: clsDetails.Rvc_Number = long.TryParse(col, out lngRetValue) ? lngRetValue : 0; break;
                    case 1: clsDetails.Rvc_Name = col; break;
                    case 2: clsDetails.Sales_Itemizer1_Name = col; break;
                    case 3: clsDetails.Sales_Itemizer2_Name = col; break;
                    case 4: clsDetails.Sales_Itemizer3_Name = col; break;
                    case 5: clsDetails.Sales_Itemizer4_Name = col; break;
                    case 6: clsDetails.Sales_Itemizer5_Name = col; break;
                    case 7: clsDetails.Sales_Itemizer6_Name = col; break;
                    case 8: clsDetails.Sales_Itemizer7_Name = col; break;
                    case 9: clsDetails.Sales_Itemizer8_Name = col; break;
                    case 10: clsDetails.Sales_Itemizer9_Name = col; break;
                    case 11: clsDetails.Sales_Itemizer10_Name = col; break;
                    case 12: clsDetails.Sales_Itemizer11_Name = col; break;
                    case 13: clsDetails.Sales_Itemizer12_Name = col; break;
                    case 14: clsDetails.Sales_Itemizer13_Name = col; break;
                    case 15: clsDetails.Sales_Itemizer14_Name = col; break;
                    case 16: clsDetails.Sales_Itemizer15_Name = col; break;
                    case 17: clsDetails.Sales_Itemizer16_Name = col; break;
                }
                iCol++;
            }
            clsDetails.DateCreated = DateCreated;
            clsDetails.CreatedBy = CreatedBy;
            clsDetails.Filename = filename;
            clsDetails.BatchID = BatchID;

            return clsDetails;
        }
        private GLAItemDetails setItemDetails(string line, DateTime DateCreated, string CreatedBy, string filename, string BatchID)
        {
            DateTime dteRetvalue = Constants.C_DATE_MIN_VALUE;
            int intRetValue = 0;
            decimal decRetValue = 0;
            long lngRetValue = 0;

            GLAItemDetails clsDetails = new GLAItemDetails();

            int iCol = 0;
            foreach (string col in line.Split(','))
            {
                dteRetvalue = Constants.C_DATE_MIN_VALUE;
                intRetValue = 0;

                switch (iCol)
                {
                    case 0: clsDetails.Rvc_Number = long.TryParse(col, out lngRetValue) ? lngRetValue : 0; break;
                    case 1: clsDetails.Mi_Number = long.TryParse(col, out lngRetValue) ? lngRetValue : 0; break;
                    case 2: clsDetails.Def_Seq = int.TryParse(col, out intRetValue) ? intRetValue : 0; break;
                    case 3: clsDetails.Mi_Name = col; break;
                    case 4: clsDetails.Sales_Itemizer_Number = long.TryParse(col, out lngRetValue) ? lngRetValue : 0; break;
                    case 5: clsDetails.Sales_Itemizer_Name = col; break;
                    case 6: clsDetails.Family_Group_Number = long.TryParse(col, out lngRetValue) ? lngRetValue : 0; break;
                    case 7: clsDetails.Family_Group_Name = col; break;
                    case 8: clsDetails.Major_Group_Number = long.TryParse(col, out lngRetValue) ? lngRetValue : 0; break;
                    case 9: clsDetails.Major_Group_Name = col; break;
                    case 10: clsDetails.Mi_Class_Number = long.TryParse(col, out lngRetValue) ? lngRetValue : 0; break;
                    case 11: clsDetails.Mi_Class_Name = col; break;
                }
                iCol++;
            }
            clsDetails.DateCreated = DateCreated;
            clsDetails.CreatedBy = CreatedBy;
            clsDetails.Filename = filename;
            clsDetails.BatchID = BatchID;

            return clsDetails;
        }
        private GLAServiceChargeDetails setSvcDetails(string line, DateTime DateCreated, string CreatedBy, string filename, string BatchID)
        {
            DateTime dteRetvalue = Constants.C_DATE_MIN_VALUE;
            int intRetValue = 0;
            decimal decRetValue = 0;
            long lngRetValue = 0;

            GLAServiceChargeDetails clsDetails = new GLAServiceChargeDetails();

            int iCol = 0;
            foreach (string col in line.Split(','))
            {
                dteRetvalue = Constants.C_DATE_MIN_VALUE;
                intRetValue = 0;

                switch (iCol)
                {
                    case 0: clsDetails.Svc_Number = long.TryParse(col, out lngRetValue) ? lngRetValue : 0; break;
                    case 1: clsDetails.Svc_Name = col; break;
                }
                iCol++;
            }
            clsDetails.DateCreated = DateCreated;
            clsDetails.CreatedBy = CreatedBy;
            clsDetails.Filename = filename;
            clsDetails.BatchID = BatchID;

            return clsDetails;
        }
        private GLATenderDetails setTenderDetails(string line, DateTime DateCreated, string CreatedBy, string filename, string BatchID)
        {
            DateTime dteRetvalue = Constants.C_DATE_MIN_VALUE;
            int intRetValue = 0;
            decimal decRetValue = 0;
            long lngRetValue = 0;

            GLATenderDetails clsDetails = new GLATenderDetails();

            int iCol = 0;
            foreach (string col in line.Split(','))
            {
                dteRetvalue = Constants.C_DATE_MIN_VALUE;
                intRetValue = 0;

                switch (iCol)
                {
                    case 0: clsDetails.Tmd_Number = long.TryParse(col, out lngRetValue) ? lngRetValue : 0; break;
                    case 1: clsDetails.Tmd_Name = col; break;
                }
                iCol++;
            }
            clsDetails.DateCreated = DateCreated;
            clsDetails.CreatedBy = CreatedBy;
            clsDetails.Filename = filename;
            clsDetails.BatchID = BatchID;

            return clsDetails;
        }
        private GLATransactionDiscountDetails setTranDscDetails(string line, DateTime DateCreated, string CreatedBy, string filename, string BatchID)
        {
            DateTime dteRetvalue = Constants.C_DATE_MIN_VALUE;
            int intRetValue = 0;
            decimal decRetValue = 0;
            long lngRetValue = 0;

            GLATransactionDiscountDetails clsDetails = new GLATransactionDiscountDetails();

            int iCol = 0;
            foreach (string col in line.Split(','))
            {
                dteRetvalue = Constants.C_DATE_MIN_VALUE;
                intRetValue = 0;

                switch (iCol)
                {
                    case 0: clsDetails.fk_business_date = DateTime.TryParse(col, out dteRetvalue) ? dteRetvalue : Constants.C_DATE_MIN_VALUE; break;
                    case 1: clsDetails.fk_location_def = int.TryParse(col, out intRetValue) ? intRetValue : 0; break;
                    case 2: clsDetails.fk_emp_def = int.TryParse(col, out intRetValue) ? intRetValue : 0; break;
                    case 3: clsDetails.fk_chk_headers = long.TryParse(col, out lngRetValue) ? lngRetValue : 0; break;
                    case 4: clsDetails.fk_dsc_def = int.TryParse(col, out intRetValue) ? intRetValue : 0; break;
                    case 5: clsDetails.fk_auth_emp_def = int.TryParse(col, out intRetValue) ? intRetValue : 0; break;
                    case 6: clsDetails.Transaction_Date_Time = DateTime.TryParse(col, out dteRetvalue) ? dteRetvalue : Constants.C_DATE_MIN_VALUE; break;
                    case 7: clsDetails.status_flag = col; break;
                    case 8: clsDetails.Round_Num = int.TryParse(col, out intRetValue) ? intRetValue : 0; break;
                    case 9: clsDetails.Dtl_Num = int.TryParse(col, out intRetValue) ? intRetValue : 0; break;
                    case 10: clsDetails.Dsc_Count = int.TryParse(col, out intRetValue) ? intRetValue : 0; break;
                    case 11: clsDetails.Dsc_Total = decimal.TryParse(col, out decRetValue) ? decRetValue : 0; break;
                    case 12:
                        {
                            clsDetails.Ref_Info_1 = col;
                            try
                            {
                                if (!string.IsNullOrEmpty(col) && col.Length >= 4) 
                                    clsDetails.ContactCode = long.TryParse(col.Remove(0, 4), out lngRetValue) ? lngRetValue.ToString() : "";
                            }
                            catch { }
                            break;
                        }
                    case 13: clsDetails.Is_HotelMark_Promo = Convert.ToBoolean(int.TryParse(col, out intRetValue) ? intRetValue : 0); break;
                }
                iCol++;
            }
            clsDetails.DateCreated = DateCreated;
            clsDetails.CreatedBy = CreatedBy;
            clsDetails.Filename = filename;
            clsDetails.BatchID = BatchID;

            return clsDetails;
        }
        private GLATransactionDetails setTranHdrDetails(string line, DateTime DateCreated, string CreatedBy, string filename, string BatchID)
        {
            DateTime dteRetvalue = Constants.C_DATE_MIN_VALUE;
            int intRetValue = 0;
            bool boRetValue = false;
            decimal decRetValue = 0;
            GLATransactionDetails clsDetails = new GLATransactionDetails();

            int iCol = 0;
            foreach (string col in line.Split(','))
            {
                dteRetvalue = Constants.C_DATE_MIN_VALUE;
                intRetValue = 0;

                switch (iCol)
                {
                    case 0: clsDetails.fk_business_date = DateTime.TryParse(col, out dteRetvalue) ? dteRetvalue : Constants.C_DATE_MIN_VALUE; break;
                    case 1: clsDetails.fk_location_def = int.TryParse(col, out intRetValue) ? intRetValue : 0; break;
                    case 2: clsDetails.fk_emp_def = int.TryParse(col, out intRetValue) ? intRetValue : 0; break;
                    case 3: clsDetails.status_flag = col; break;
                    case 4: clsDetails.chk_headers_seq_number = int.TryParse(col, out intRetValue) ? intRetValue : 0; break;
                    case 5: clsDetails.chk_num = int.TryParse(col, out intRetValue) ? intRetValue : 0; break;
                    case 6: clsDetails.chk_id = col; break;
                    case 7: clsDetails.ot_number = int.TryParse(col, out intRetValue) ? intRetValue : 0; break;
                    case 8: clsDetails.Ot_Name = col; break;
                    case 9: clsDetails.Tbl_Number = int.TryParse(col, out intRetValue) ? intRetValue : 0; break;
                    case 10: clsDetails.Chk_Open_Date_Time = DateTime.TryParse(col, out dteRetvalue) ? dteRetvalue : Constants.C_DATE_MIN_VALUE; break;
                    case 11: clsDetails.Chk_Closed_Date_Time = DateTime.TryParse(col, out dteRetvalue) ? dteRetvalue : Constants.C_DATE_MIN_VALUE; break;
                    case 12: clsDetails.Uws_Number = int.TryParse(col, out intRetValue) ? intRetValue : 0; break;
                    case 13: clsDetails.Is_HotelMark_Promo = Convert.ToBoolean(int.TryParse(col, out intRetValue) ? intRetValue : 0); break;
                    case 14: clsDetails.Sub_Ttl = decimal.TryParse(col, out decRetValue) ? decRetValue : 0; break;
                    case 15: clsDetails.Tax_Ttl = decimal.TryParse(col, out decRetValue) ? decRetValue : 0; break;
                    case 16: clsDetails.Auto_Svc_Ttl = decimal.TryParse(col, out decRetValue) ? decRetValue : 0; break;
                    case 17: clsDetails.Other_Svc_Ttl = decimal.TryParse(col, out decRetValue) ? decRetValue : 0; break;
                    case 18: clsDetails.Dsc_Ttl = decimal.TryParse(col, out decRetValue) ? decRetValue : 0; break;
                    case 19: clsDetails.Pymnt_Ttl = decimal.TryParse(col, out decRetValue) ? decRetValue : 0; break;
                    case 20: clsDetails.Chk_Prntd_Cnt = int.TryParse(col, out intRetValue) ? intRetValue : 0; break;
                    case 21: clsDetails.Cov_Cnt = int.TryParse(col, out intRetValue) ? intRetValue : 0; break;
                    case 22: clsDetails.Num_Dtl = int.TryParse(col, out intRetValue) ? intRetValue : 0; break;
                    case 23: clsDetails.Itemizer1 = decimal.TryParse(col, out decRetValue) ? decRetValue : 0; break;
                    case 24: clsDetails.Itemizer2 = decimal.TryParse(col, out decRetValue) ? decRetValue : 0; break;
                    case 25: clsDetails.Itemizer3 = decimal.TryParse(col, out decRetValue) ? decRetValue : 0; break;
                    case 26: clsDetails.Itemizer4 = decimal.TryParse(col, out decRetValue) ? decRetValue : 0; break;
                    case 27: clsDetails.Itemizer5 = decimal.TryParse(col, out decRetValue) ? decRetValue : 0; break;
                    case 28: clsDetails.Itemizer6 = decimal.TryParse(col, out decRetValue) ? decRetValue : 0; break;
                    case 29: clsDetails.Itemizer7 = decimal.TryParse(col, out decRetValue) ? decRetValue : 0; break;
                    case 30: clsDetails.Itemizer8 = decimal.TryParse(col, out decRetValue) ? decRetValue : 0; break;
                    case 31: clsDetails.Itemizer9 = decimal.TryParse(col, out decRetValue) ? decRetValue : 0; break;
                    case 32: clsDetails.Itemizer10 = decimal.TryParse(col, out decRetValue) ? decRetValue : 0; break;
                    case 33: clsDetails.Itemizer11 = decimal.TryParse(col, out decRetValue) ? decRetValue : 0; break;
                    case 34: clsDetails.Itemizer12 = decimal.TryParse(col, out decRetValue) ? decRetValue : 0; break;
                    case 35: clsDetails.Itemizer13 = decimal.TryParse(col, out decRetValue) ? decRetValue : 0; break;
                    case 36: clsDetails.Itemizer14 = decimal.TryParse(col, out decRetValue) ? decRetValue : 0; break;
                    case 37: clsDetails.Itemizer15 = decimal.TryParse(col, out decRetValue) ? decRetValue : 0; break;
                    case 38: clsDetails.Itemizer16 = decimal.TryParse(col, out decRetValue) ? decRetValue : 0; break;
                    case 39: clsDetails.Tip_ttl = decimal.TryParse(col, out decRetValue) ? decRetValue : 0; break;
                }
                iCol++;
            }
            clsDetails.DateCreated = DateCreated;
            clsDetails.CreatedBy = CreatedBy;
            clsDetails.Filename = filename;
            clsDetails.BatchID = BatchID;

            return clsDetails;
        }
        private GLATransactionItemDetails setTranItemDetails(string line, DateTime DateCreated, string CreatedBy, string filename, string BatchID)
        {
            DateTime dteRetvalue = Constants.C_DATE_MIN_VALUE;
            int intRetValue = 0;
            decimal decRetValue = 0;
            long lngRetValue = 0;

            GLATransactionItemDetails clsDetails = new GLATransactionItemDetails();

            int iCol = 0;
            foreach (string col in line.Split(','))
            {
                dteRetvalue = Constants.C_DATE_MIN_VALUE;
                intRetValue = 0;

                switch (iCol)
                {
                    case 0: clsDetails.fk_business_date = DateTime.TryParse(col, out dteRetvalue) ? dteRetvalue : Constants.C_DATE_MIN_VALUE; break;
                    case 1: clsDetails.fk_location_def = int.TryParse(col, out intRetValue) ? intRetValue : 0; break;
                    case 2: clsDetails.fk_emp_def = int.TryParse(col, out intRetValue) ? intRetValue : 0; break;
                    case 3: clsDetails.fk_chk_headers = long.TryParse(col, out lngRetValue) ? lngRetValue : 0; break;
                    case 4: clsDetails.fk_mi_def = int.TryParse(col, out intRetValue) ? intRetValue : 0; break;
                    case 5: clsDetails.fk_mi_def = int.TryParse(col, out intRetValue) ? intRetValue : 0; break;
                    case 6: clsDetails.fk_auth_emp_def = int.TryParse(col, out intRetValue) ? intRetValue : 0; break;
                    case 7: clsDetails.Transaction_Date_Time = DateTime.TryParse(col, out dteRetvalue) ? dteRetvalue : Constants.C_DATE_MIN_VALUE; break;
                    case 8: clsDetails.status_flag = col; break;
                    case 9: clsDetails.Round_Num = int.TryParse(col, out intRetValue) ? intRetValue : 0; break;
                    case 10: clsDetails.Dtl_Num = int.TryParse(col, out intRetValue) ? intRetValue : 0; break;
                    case 11: clsDetails.Item_Count = int.TryParse(col, out intRetValue) ? intRetValue : 0; break;
                    case 12: clsDetails.Item_Total = decimal.TryParse(col, out decRetValue) ? decRetValue : 0; break;
                    case 13: clsDetails.Ref_Info_1 = col; break;
                }
                iCol++;
            }
            clsDetails.DateCreated = DateCreated;
            clsDetails.CreatedBy = CreatedBy;
            clsDetails.Filename = filename;
            clsDetails.BatchID = BatchID;

            return clsDetails;
        }
        private GLATransactionSvcDetails setTranSvcDetails(string line, DateTime DateCreated, string CreatedBy, string filename, string BatchID)
        {
            DateTime dteRetvalue = Constants.C_DATE_MIN_VALUE;
            int intRetValue = 0;
            decimal decRetValue = 0;
            long lngRetValue = 0;

            GLATransactionSvcDetails clsDetails = new GLATransactionSvcDetails();

            int iCol = 0;
            foreach (string col in line.Split(','))
            {
                dteRetvalue = Constants.C_DATE_MIN_VALUE;
                intRetValue = 0;

                switch (iCol)
                {
                    case 0: clsDetails.fk_business_date = DateTime.TryParse(col, out dteRetvalue) ? dteRetvalue : Constants.C_DATE_MIN_VALUE; break;
                    case 1: clsDetails.fk_location_def = int.TryParse(col, out intRetValue) ? intRetValue : 0; break;
                    case 2: clsDetails.fk_emp_def = int.TryParse(col, out intRetValue) ? intRetValue : 0; break;
                    case 3: clsDetails.fk_chk_headers = long.TryParse(col, out lngRetValue) ? lngRetValue : 0; break;
                    case 4: clsDetails.fk_svc_def = int.TryParse(col, out intRetValue) ? intRetValue : 0; break;
                    case 5: clsDetails.fk_auth_emp_def = int.TryParse(col, out intRetValue) ? intRetValue : 0; break;
                    case 6: clsDetails.Transaction_Date_Time = DateTime.TryParse(col, out dteRetvalue) ? dteRetvalue : Constants.C_DATE_MIN_VALUE; break;
                    case 7: clsDetails.status_flag = col; break;
                    case 8: clsDetails.Round_Num = int.TryParse(col, out intRetValue) ? intRetValue : 0; break;
                    case 9: clsDetails.Dtl_Num = int.TryParse(col, out intRetValue) ? intRetValue : 0; break;
                    case 10: clsDetails.Svc_Count = int.TryParse(col, out intRetValue) ? intRetValue : 0; break;
                    case 11: clsDetails.Svc_Total = decimal.TryParse(col, out decRetValue) ? decRetValue : 0; break;
                    case 12: clsDetails.Ref_Info_1 = col; break;
                }
                iCol++;
            }
            clsDetails.DateCreated = DateCreated;
            clsDetails.CreatedBy = CreatedBy;
            clsDetails.Filename = filename;
            clsDetails.BatchID = BatchID;

            return clsDetails;
        }
        private GLATransactionTenderDetails setTranTenderDetails(string line, DateTime DateCreated, string CreatedBy, string filename, string BatchID)
        {
            DateTime dteRetvalue = Constants.C_DATE_MIN_VALUE;
            int intRetValue = 0;
            decimal decRetValue = 0;
            long lngRetValue = 0;

            GLATransactionTenderDetails clsDetails = new GLATransactionTenderDetails();

            int iCol = 0;
            foreach (string col in line.Split(','))
            {
                dteRetvalue = Constants.C_DATE_MIN_VALUE;
                intRetValue = 0;

                switch (iCol)
                {
                    case 0: clsDetails.fk_business_date = DateTime.TryParse(col, out dteRetvalue) ? dteRetvalue : Constants.C_DATE_MIN_VALUE; break;
                    case 1: clsDetails.fk_location_def = int.TryParse(col, out intRetValue) ? intRetValue : 0; break;
                    case 2: clsDetails.fk_emp_def = int.TryParse(col, out intRetValue) ? intRetValue : 0; break;
                    case 3: clsDetails.fk_chk_headers = long.TryParse(col, out lngRetValue) ? lngRetValue : 0; break;
                    case 4: clsDetails.fk_tmd_def = int.TryParse(col, out intRetValue) ? intRetValue : 0; break;
                    case 5: clsDetails.fk_auth_emp_def = int.TryParse(col, out intRetValue) ? intRetValue : 0; break;
                    case 6: clsDetails.Transaction_Date_Time = DateTime.TryParse(col, out dteRetvalue) ? dteRetvalue : Constants.C_DATE_MIN_VALUE; break;
                    case 7: clsDetails.status_flag = col; break;
                    case 8: clsDetails.Round_Num = int.TryParse(col, out intRetValue) ? intRetValue : 0; break;
                    case 9: clsDetails.Dtl_Num = int.TryParse(col, out intRetValue) ? intRetValue : 0; break;
                    case 10: clsDetails.Tender_Count = int.TryParse(col, out intRetValue) ? intRetValue : 0; break;
                    case 11: clsDetails.Tender_Total = decimal.TryParse(col, out decRetValue) ? decRetValue : 0; break;
                    case 12: clsDetails.Chgd_Tip_Ttl = decimal.TryParse(col, out decRetValue) ? decRetValue : 0; break;
                    case 13: clsDetails.Ref_Info_1 = col; break;
                }
                iCol++;
            }
            clsDetails.DateCreated = DateCreated;
            clsDetails.CreatedBy = CreatedBy;
            clsDetails.Filename = filename;
            clsDetails.BatchID = BatchID;

            return clsDetails;
        }

        private GLAOrderTenderDetails setOrderTenderDetails(string line, DateTime DateCreated, string CreatedBy, string filename, string BatchID)
        {
            DateTime dteRetvalue = Constants.C_DATE_MIN_VALUE;
            long lngRetValue = 0;
            int intRetValue = 0;
            decimal decRetValue = 0;
            GLAOrderTenderDetails clsDetails = new GLAOrderTenderDetails();

            int iCol = 0;
            int iColText = 0;
            foreach (string colText in line.Split('"'))
            {
                switch (iColText)
                {
                    case 0: break;
                    case 1: clsDetails.identifier = colText; break;
                    case 2:
                        iCol = 0;
                        foreach (string col in colText.Split(','))
                        {
                            dteRetvalue = Constants.C_DATE_MIN_VALUE;
                            intRetValue = 0;

                            string col2 = col.Replace("$", "").Trim();
                            switch (iCol)
                            {
                                case 0: break;
                                case 1: clsDetails.order_hdr_id = long.TryParse(col2, out lngRetValue) ? lngRetValue : 0; break;
                                case 2: clsDetails.tender_seq = long.TryParse(col2, out lngRetValue) ? lngRetValue : 0; break;
                                case 3: clsDetails.tender_id = long.TryParse(col2, out lngRetValue) ? lngRetValue : 0; break;
                                case 4: clsDetails.tender_amt = decimal.TryParse(col2, out decRetValue) ? decRetValue : 0; break;
                                case 5: clsDetails.prorata_sales_amt_gross = decimal.TryParse(col2, out decRetValue) ? decRetValue : 0; break;
                                case 6: clsDetails.prorata_discount_amt = decimal.TryParse(col2, out decRetValue) ? decRetValue : 0; break;
                                case 7: clsDetails.prorata_tax_amt = decimal.TryParse(col2, out decRetValue) ? decRetValue : 0; break;
                                case 8: clsDetails.prorata_grat_amt = decimal.TryParse(col2, out decRetValue) ? decRetValue : 0; break;
                                case 9: clsDetails.prorata_svc_chg_amt = decimal.TryParse(col2, out decRetValue) ? decRetValue : 0; break;
                                case 10: clsDetails.tip_amt = decimal.TryParse(col2, out decRetValue) ? decRetValue : 0; break;
                                case 11: clsDetails.breakage_amt = decimal.TryParse(col2, out decRetValue) ? decRetValue : 0; break;
                                case 12: clsDetails.received_curr_amt = decimal.TryParse(col2, out decRetValue) ? decRetValue : 0; break;
                                case 13: clsDetails.curr_decimal_places = int.TryParse(col2, out intRetValue) ? intRetValue : 0; break;
                                case 14: clsDetails.exchange_rate = int.TryParse(col2, out intRetValue) ? intRetValue : 0; break;
                                case 15: clsDetails.change_amt = decimal.TryParse(col2, out decRetValue) ? decRetValue : 0; break;
                                case 16: clsDetails.change_tender_id = int.TryParse(col2, out intRetValue) ? intRetValue : 0; break;
                                case 17: clsDetails.tax_removed_code = int.TryParse(col2, out intRetValue) ? intRetValue : 0; break;
                                case 18: clsDetails.tender_type_id = int.TryParse(col2, out intRetValue) ? intRetValue : 0; break;
                                case 19: clsDetails.subtender_id = int.TryParse(col2, out intRetValue) ? intRetValue : 0; break;
                                
                            }
                            iCol++;
                        }
                        break;
                    case 3: clsDetails.auth_acct_no = colText; break;
                    case 4: break;
                    case 5: clsDetails.post_acct_no = colText; break;
                    case 6: break;
                    case 7: clsDetails.customer_name = colText; break;
                    case 8:
                        iCol = 0;
                        foreach (string col in colText.Split(','))
                        {
                            dteRetvalue = Constants.C_DATE_MIN_VALUE;
                            intRetValue = 0;

                            string col2 = col.Replace("$", "").Trim();
                            switch (iCol)
                            {
                                case 0: break;
                                case 1: break;
                                case 2: clsDetails.adtnl_info = col2; break;
                                case 3: clsDetails.subtender_qty = int.TryParse(col2, out intRetValue) ? intRetValue : 0; break;
                                case 4: clsDetails.charges_to_date_amt = decimal.TryParse(col2, out decRetValue) ? decRetValue : 0; break;
                                case 5: clsDetails.remaining_balance_amt = decimal.TryParse(col2, out decRetValue) ? decRetValue : 0; break;
                                case 6: clsDetails.PMS_post_flag = Convert.ToBoolean(int.TryParse(col2, out intRetValue) ? intRetValue : 0); break;
                                case 7: clsDetails.sales_tippable_flag = Convert.ToBoolean(int.TryParse(col2, out intRetValue) ? intRetValue : 0); break;
                                case 8: clsDetails.post_system1_flag = Convert.ToBoolean(int.TryParse(col2, out intRetValue) ? intRetValue : 0); break;
                                case 9: clsDetails.post_system2_flag = Convert.ToBoolean(int.TryParse(col2, out intRetValue) ? intRetValue : 0); break;
                                case 10: clsDetails.post_system3_flag = Convert.ToBoolean(int.TryParse(col2, out intRetValue) ? intRetValue : 0); break;
                                case 11: clsDetails.post_system4_flag = Convert.ToBoolean(int.TryParse(col2, out intRetValue) ? intRetValue : 0); break;
                                case 12: clsDetails.post_system5_flag = Convert.ToBoolean(int.TryParse(col2, out intRetValue) ? intRetValue : 0); break;
                                case 13: clsDetails.post_system6_flag = Convert.ToBoolean(int.TryParse(col2, out intRetValue) ? intRetValue : 0); break;
                                case 14: clsDetails.post_system7_flag = Convert.ToBoolean(int.TryParse(col2, out intRetValue) ? intRetValue : 0); break;
                                case 15: clsDetails.post_system8_flag = Convert.ToBoolean(int.TryParse(col2, out intRetValue) ? intRetValue : 0); break;
                            }
                            iCol++;
                        }
                        break;
                }
                iColText++;
            }

            clsDetails.DateCreated = DateCreated;
            clsDetails.CreatedBy = CreatedBy;
            clsDetails.Filename = filename;
            clsDetails.BatchID = BatchID;

            return clsDetails;
        }
        private SalesTransactionDetails setSalesTransactionDetails(GLAOrderTenderDetails glaDetails, SalesTransactionDetails mclsSalesTransactionDetails)
        {

            // for update
            
            //mclsSalesTransactionDetails.CustomerID = glaDetails.fk_emp_def;
            //mclsSalesTransactionDetails.AgentID = Constants.C_RETAILPLUS_AGENTID;
            //mclsSalesTransactionDetails.AgentName = Constants.C_RETAILPLUS_AGENT;
            //mclsSalesTransactionDetails.AgentPositionName = Constants.C_RETAILPLUS_AGENT_POSITIONNAME;
            //mclsSalesTransactionDetails.AgentDepartmentName = Constants.C_RETAILPLUS_AGENT_DEPARTMENT_NAME;
            //mclsSalesTransactionDetails.WaiterID = Constants.C_RETAILPLUS_WAITERID;
            //mclsSalesTransactionDetails.WaiterName = Constants.C_RETAILPLUS_WAITER;
            //mclsSalesTransactionDetails.CreatedByID = glaDetails.fk_emp_def;
            //mclsSalesTransactionDetails.CreatedByName = Constants.C_RETAILPLUS_WAITER;
            //mclsSalesTransactionDetails.CashierID = glaDetails.fk_emp_def;
            //mclsSalesTransactionDetails.CashierName = glaDetails.Filename;
            //mclsSalesTransactionDetails.CustomerID = Constants.C_RETAILPLUS_CUSTOMERID;
            //mclsSalesTransactionDetails.CustomerName = Constants.C_RETAILPLUS_CUSTOMER;
            //mclsSalesTransactionDetails.TransactionDate = glaDetails.Chk_Open_Date_Time;
            //mclsSalesTransactionDetails.DateSuspended = Constants.C_DATE_MIN_VALUE;
            //mclsSalesTransactionDetails.TerminalNo = Constants.C_DEFAULT_TERMINAL_01;
            //mclsSalesTransactionDetails.BranchID = Constants.BRANCH_ID_MAIN;
            //mclsSalesTransactionDetails.BranchCode = Constants.BRANCH_MAIN;
            //mclsSalesTransactionDetails.TransactionStatus = TransactionStatus.Closed;
            //mclsSalesTransactionDetails.TransactionType = TransactionTypes.POSNormal;
            //mclsSalesTransactionDetails.TransactionNo = glaDetails.chk_headers_seq_number.ToString();

            ////for update
            //mclsSalesTransactionDetails.Charge = glaDetails.Auto_Svc_Ttl + glaDetails.Other_Svc_Ttl + glaDetails.Tip_ttl;
            //mclsSalesTransactionDetails.Discount = -glaDetails.Dsc_Ttl;
            //mclsSalesTransactionDetails.AmountDue = glaDetails.Sub_Ttl + glaDetails.Tax_Ttl + mclsSalesTransactionDetails.Charge - mclsSalesTransactionDetails.Discount;
            //mclsSalesTransactionDetails.SubTotal = glaDetails.Sub_Ttl + glaDetails.Tax_Ttl + mclsSalesTransactionDetails.Charge;

            //mclsSalesTransactionDetails.DiscountableAmount = mclsSalesTransactionDetails.Discount <= 0 ? 0 : glaDetails.Dsc_Ttl;
            //mclsSalesTransactionDetails.ItemsDiscount = 0;
            //mclsSalesTransactionDetails.VAT = glaDetails.Tax_Ttl;
            //mclsSalesTransactionDetails.VatableAmount = glaDetails.Sub_Ttl + glaDetails.Tax_Ttl;
            //mclsSalesTransactionDetails.NonVATableAmount = 0;
            //mclsSalesTransactionDetails.EVAT = 0;
            //mclsSalesTransactionDetails.EVatableAmount = 0;
            //mclsSalesTransactionDetails.NonEVATableAmount = glaDetails.Sub_Ttl + glaDetails.Tax_Ttl;
            //mclsSalesTransactionDetails.LocalTax = 0;
            //mclsSalesTransactionDetails.TotalItemSold = glaDetails.Cov_Cnt;
            //mclsSalesTransactionDetails.TotalQuantitySold = glaDetails.Num_Dtl;

            //mclsSalesTransactionDetails.AmountPaid = glaDetails.Pymnt_Ttl;
            //mclsSalesTransactionDetails.CashPayment = mclsSalesTransactionDetails.SubTotal;
            //mclsSalesTransactionDetails.ChangeAmount = glaDetails.Pymnt_Ttl;
            //mclsSalesTransactionDetails.ChequePayment = 0;
            //mclsSalesTransactionDetails.CreditCardPayment = 0;
            //mclsSalesTransactionDetails.CreditPayment = 0;
            //mclsSalesTransactionDetails.CreditChargeAmount = 0;
            //mclsSalesTransactionDetails.DebitPayment = 0;
            //mclsSalesTransactionDetails.RewardPointsPayment = 0;
            //mclsSalesTransactionDetails.DateClosed = glaDetails.Chk_Closed_Date_Time;
            //mclsSalesTransactionDetails.DateResumed = glaDetails.DateCreated;
            //mclsSalesTransactionDetails.DataSource = glaDetails.BatchID;

            return mclsSalesTransactionDetails;
        }

		#endregion
	}
}

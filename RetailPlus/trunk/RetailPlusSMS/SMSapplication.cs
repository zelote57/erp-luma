/*
 * Created by: Syeda Anila Nusrat. 
 * Date: 1st August 2009
 * Time: 2:54 PM 
 */
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using System.IO;
using System.IO.Ports;
using Microsoft.Office.Interop;
using Excel = Microsoft.Office.Interop.Excel;
using System.Management;

namespace SMSapplication
{
    public partial class SMSapplication : Form
    {
        private System.ComponentModel.BackgroundWorker bgwSendSMS = new System.ComponentModel.BackgroundWorker();

        #region Constructor
        public SMSapplication()
        {
            InitializeComponent();

            bgwSendSMS.WorkerSupportsCancellation = true;
            bgwSendSMS.WorkerReportsProgress = true;
            bgwSendSMS.DoWork += new DoWorkEventHandler(bgwSendSMS_DoWork);
        }

        #endregion

        #region Private Variables
        SerialPort port = new SerialPort();
        clsSMS objclsSMS = new clsSMS();
        ShortMessageCollection objShortMessageCollection = new ShortMessageCollection();
        #endregion

        #region Private Methods

        #region Write StatusBar
        private void WriteStatusBar(string status)
        {
            try
            {
                statusBar1.Text = "Message: " + status;
            }
            catch (Exception ex)
            {
                
            }
        }
        #endregion

        private System.Data.DataTable importContacts(string _fileName)
        {
            System.Data.DataTable dtRetValue = new DataTable();
            dtRetValue.Columns.Add("MobileNo");

            if (string.IsNullOrEmpty(_fileName)) return dtRetValue;

            Excel.Application app = null;
            Excel.Workbooks workbooks = null;
            Excel.Workbook workbook = null;
            Excel.Worksheet worksheet = null;
            Excel.Range workSheet_range = null;

            worksheet = null;
            string _sheetname = "";

            try
            {
                try { app = new Excel.Application(); }
                catch (Exception ex1)
                {
                    throw new Exception("Cannot initiate excel application. \n\nDetails: " + ex1.InnerException.Message);
                }
                try
                {
                    workbooks = app.Workbooks;
                    workbook = workbooks.Open(_fileName, 0, true, 5, "", "", true, Excel.XlPlatform.xlWindows, "\t", false, false, 0, true, 1, 0);
                    //workbook = app.Workbooks.Open(_fileName, 0, true, 5, "", "", true, Excel.XlPlatform.xlWindows, "\t", false, false, 0, true, 1, 0);
                }
                catch (Exception ex1)
                {
                    throw new Exception("Cannot open the excel file. \n\nDetails: " + ex1.InnerException.Message);
                }
                foreach (Excel.Worksheet sheet in workbook.Sheets)
                {
                    if (sheet.Name.ToUpper().IndexOf("MobileNos") > -1)
                    {
                        _sheetname = sheet.Name;
                        worksheet = sheet; // (Microsoft.Office.Interop.Excel.Worksheet)workbook.Sheets[sheet.Name];
                        break;
                    }
                }
                if (worksheet == null)
                {
                    foreach (Excel.Worksheet sheet in workbook.Sheets)
                    {
                        _sheetname = sheet.Name;
                        worksheet = sheet; // (Microsoft.Office.Interop.Excel.Worksheet)workbook.Sheets[sheet.Name];
                        break;
                    }
                }
                if (worksheet == null) //sheetname is null not ""
                {
                    // return unsuccessfull value
                    throw new Exception("An invalid quotation has been detected: \n\nErr: No valid sheet in the quotation.\n\n Please upload a valid quotation output from a MACRO or rename the sheet with 'QU_' followed by a name.");
                }
                else
                {
                    for (int iRow = 1; iRow < 2000000000; iRow++)
                    {
                        
                        if (worksheet.Range["A" + iRow.ToString()].Value == null)
                            {   break;  }   // means no more mobile nos
                        else
                        {
                            string strMobileNo = worksheet.Range["A" + iRow.ToString()].Value.ToString();
                            dtRetValue.Rows.Add(strMobileNo.Replace("'", ""));
                        }
                    }
                }
            }
            catch { }
            finally 
            {
                // false means it will not wait
                workbook.Close(false);
                app.Quit();

                System.Runtime.InteropServices.Marshal.ReleaseComObject(workbook);
                System.Runtime.InteropServices.Marshal.ReleaseComObject(workbook);
                System.Runtime.InteropServices.Marshal.ReleaseComObject(workbooks);
                System.Runtime.InteropServices.Marshal.ReleaseComObject(app);

                app = null;
                worksheet = null;
            }

            return dtRetValue;
        }

        private void bgwSendSMS_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                string fileName = txtFilePath.Text;
                System.Data.DataTable dt = importContacts(fileName);

                SetText("Connection Status");

                string strMessage = this.txtMessage.Text;
                string strMobileNo = this.txtSIM.Text;

                //.............................................. Send SMS ....................................................
                try
                {
                    SetText("Sending sms to cc #: " + strMobileNo);

                    if (strMessage == "rbstest")
                    {
                        SetText("Message has sent successfully");
                    }
                    else
                    {
                        if (objclsSMS.sendMsg(this.port, strMobileNo, strMessage))
                        {
                            SetText("Message has sent successfully");
                        }
                        else
                        {
                            //MessageBox.Show("Failed to send message");
                            SetText("Failed to send message");
                            return;
                        }
                        Thread.Sleep(60);
                    }

                }
                catch (Exception ex)
                {
                    ErrorLog(ex.Message);
                    return;
                }

                Int32 iCount = dt.Rows.Count;
                Int32 iCtr = 1;

                foreach (System.Data.DataRow dr in dt.Rows)
                {
                    if (bgwSendSMS.CancellationPending)
                    { e.Cancel = true; break; }
                    else
                    {
                        strMobileNo = dr[0].ToString().Replace("'", "");

                        SetText("[" + iCtr.ToString() + "/" + iCount.ToString() + "]: sending msg to " + strMobileNo);

                        if (!isValidMobileNo(strMobileNo))
                        {
                            SetText("[" + iCtr.ToString() + "/" + iCount.ToString() + "]: sending msg to " + strMobileNo + ". Invalid Mobile no");
                        }
                        else
                        {
                            if (strMessage == "rbstest")
                            {
                                ListViewItem item = new ListViewItem(new string[] { iCtr.ToString(), DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), strMobileNo, "OK" });
                                SetSentMessage(item);
                                SetText("[" + iCtr.ToString() + "/" + iCount.ToString() + "]: msg sent to " + strMobileNo);
                            }
                            else
                            {
                                try
                                {
                                    if (objclsSMS.sendMsg(this.port, strMobileNo, strMessage))
                                    {
                                        ListViewItem item = new ListViewItem(new string[] { iCtr.ToString(), DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), strMobileNo, "OK" });
                                        SetSentMessage(item);
                                        SetText("[" + iCtr.ToString() + "/" + iCount.ToString() + "]: msg sent to " + strMobileNo);
                                    }
                                    else
                                    {
                                        ListViewItem item = new ListViewItem(new string[] { iCtr.ToString(), DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), strMobileNo, "sent" });
                                        SetSentMessage(item);
                                        SetText("[" + iCtr.ToString() + "/" + iCount.ToString() + "]: msg sent no reply from SMSC to " + strMobileNo);
                                    }
                                    Thread.Sleep(60);
                                }
                                catch
                                {
                                    ListViewItem item = new ListViewItem(new string[] { iCtr.ToString(), DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), strMobileNo, "sent" });
                                    SetSentMessage(item);
                                    SetText("[" + iCtr.ToString() + "/" + iCount.ToString() + "]: msg sent no reply from SMSC to " + strMobileNo);
                                }
                            }
                        }
                    }
                    iCtr++;

                    if (bgwSendSMS.CancellationPending)
                    { e.Cancel = true; break; }
                }

                if (bgwSendSMS.CancellationPending)
                { SetText("Sending message cancelled."); }
                else
                { SetText("Message successfully sent."); }

            }
            catch (Exception ex)
            {
                ErrorLog(ex.Message);
            }
        }

        //private void bgwSendSMS_DoWork(object sender, DoWorkEventArgs e)
        //{
        //    try
        //    {
        //        string fileName = txtFilePath.Text;

                


        //        Microsoft.Office.Interop.Excel.Application xlApp = new Microsoft.Office.Interop.Excel.Application();
        //        xlApp.Visible = false;
        //        Microsoft.Office.Interop.Excel.Workbook wb = xlApp.Workbooks.Open(fileName);

        //        Microsoft.Office.Interop.Excel._Worksheet xlWorksheet = (Microsoft.Office.Interop.Excel._Worksheet) wb.Sheets[1];

        //        Int32 iCount = xlWorksheet.UsedRange.Rows.Count;
        //        Int32 iCtr = 1;
        //        SetText("Connection Status");

        //        string strMessage = this.txtMessage.Text;
        //        string strMobileNo = this.txtSIM.Text;

        //        //.............................................. Send SMS ....................................................
        //        try
        //        {
        //            SetText("Sending sms to cc #: " + strMobileNo);

        //            if (strMessage == "rbstest")
        //            {
        //                SetText("Message has sent successfully");
        //            }
        //            else
        //            {
        //                if (objclsSMS.sendMsg(this.port, strMobileNo, strMessage))
        //                {
        //                    SetText("Message has sent successfully");
        //                }
        //                else
        //                {
        //                    //MessageBox.Show("Failed to send message");
        //                    SetText("Failed to send message");
        //                    return;
        //                }
        //            }

        //        }
        //        catch (Exception ex)
        //        {
        //            ErrorLog(ex.Message);
        //            return;
        //        }

        //        foreach (Microsoft.Office.Interop.Excel.Range row in xlWorksheet.UsedRange.Rows)
        //        {
        //            foreach (Microsoft.Office.Interop.Excel.Range cell in row.Columns)
        //            {
        //                if (bgwSendSMS.CancellationPending)
        //                { e.Cancel = true; break; }
        //                else
        //                {
        //                    strMobileNo = cell.Value.ToString().Replace("'", "");
        //                    SetText("[" + iCtr.ToString() + "/" + iCount.ToString() + "]: sending msg to " + strMobileNo);

        //                    if (strMessage == "rbstest")
        //                    {
        //                        ListViewItem item = new ListViewItem(new string[] { iCtr.ToString(), DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), strMobileNo, "OK" });
        //                        SetSentMessage(item);
        //                        SetText("[" + iCtr.ToString() + "/" + iCount.ToString() + "]: msg sent to " + strMobileNo);
        //                    }
        //                    else
        //                    {
        //                        if (objclsSMS.sendMsg(this.port, strMobileNo, strMessage))
        //                        {
        //                            ListViewItem item = new ListViewItem(new string[] { iCtr.ToString(), DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), strMobileNo, "OK" });
        //                            SetText("[" + iCtr.ToString() + "/" + iCount.ToString() + "]: msg sent to " + strMobileNo);
        //                        }
        //                        else
        //                        {
        //                            ListViewItem item = new ListViewItem(new string[] { iCtr.ToString(), DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), strMobileNo, "failed" });
        //                            SetText("[" + iCtr.ToString() + "/" + iCount.ToString() + "]: msg sending failed to " + strMobileNo);
        //                        }
        //                    }
        //                }
        //                iCtr++;
        //            }

        //            if (bgwSendSMS.CancellationPending)
        //            { e.Cancel = true; break; }
        //        }

        //        wb.Close();

        //        if (bgwSendSMS.CancellationPending)
        //        { SetText("Sending message cancelled."); }
        //        else
        //        { SetText("Message successfully sent."); }

        //    }
        //    catch (Exception ex)
        //    {
        //        ErrorLog(ex.Message);
        //    }
        //}

        delegate void SetTextCallback(string text);
        private void SetText(string text)
        {
            // InvokeRequired required compares the thread ID of the
            // calling thread to the thread ID of the creating thread.
            // If these threads are different, it returns true.
            if (this.statusBar1.InvokeRequired)
            {
                SetTextCallback d = new SetTextCallback(SetText);
                this.Invoke(d, new object[] { text });
            }
            else
            {
                this.statusBar1.Text = text;
            }
        }

        delegate void SetSentMessageCallback(ListViewItem item);
        private void SetSentMessage(ListViewItem item)
        {
            // InvokeRequired required compares the thread ID of the
            // calling thread to the thread ID of the creating thread.
            // If these threads are different, it returns true.
            if (this.lvwSentMessages.InvokeRequired)
            {
                SetSentMessageCallback d = new SetSentMessageCallback(SetSentMessage);
                this.Invoke(d, new object[] { item });
            }
            else
            {
                lvwSentMessages.Items.Add(item);
                lvwSentMessages.Items[lvwSentMessages.Items.Count - 1].Selected = true;
                lvwSentMessages.EnsureVisible(lvwSentMessages.Items.Count - 1);

                if (lvwSentMessages.Scrollable && colSentDateTime.Width >= 160)
                {
                    colSentDateTime.Width = 140;
                }
                else if (!lvwSentMessages.Scrollable && colSentDateTime.Width < 160)
                {
                    colSentDateTime.Width = 160;
                }

            }
        }

        private void export2File(ListView lv, string splitter)
        {
            string filename = "";
            SaveFileDialog sfd = new SaveFileDialog();

            sfd.Title = "SaveFileDialog Export2File";
            sfd.Filter = "Text File (.txt) | *.txt";

            if (sfd.ShowDialog() == DialogResult.OK)
            {
                filename = sfd.FileName.ToString();
                if (filename != "")
                {
                    using (StreamWriter sw = new StreamWriter(filename))
                    {
                        sw.WriteLine("{0},{1},{2},{3}", "Counter", "Sent Time", "Sent To", "Status");
                        foreach (ListViewItem item in lv.Items)
                        {
                            sw.WriteLine("{0},{1},{2},{3}", item.SubItems[0].Text, item.SubItems[1].Text, item.SubItems[2].Text, item.SubItems[3].Text);
                        }
                        sw.Close();
                    }
                    MessageBox.Show("Results has been saved to '" + filename + "'.", "RetailPlus", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        #endregion

        #region Private Events

        private void SMSapplication_Load(object sender, EventArgs e)
        {
            try
            {
                statusBar1.Text = "";

                #region Display all available COM Ports
                string[] ports = SerialPort.GetPortNames();

                // Add all port names to the combo box:
                foreach (string port in ports)
                {
                    this.cboPortName.Items.Add(port);
                    cboPortName.Text = port;
                }
                #endregion

                //Remove tab pages
                this.tabSMSapplication.TabPages.Remove(tbSendSMS);
                this.tabSMSapplication.TabPages.Remove(tbReadSMS);
                this.tabSMSapplication.TabPages.Remove(tbDeleteSMS);

                this.btnDisconnect.Enabled = false;

                Connect();
            }
            catch(Exception ex)
            {
                ErrorLog(ex.Message);
            }
        }

        private bool isModemOK()
        {
            string strCompanyCode = "";
            try
            {
                strCompanyCode = System.Configuration.ConfigurationManager.AppSettings["CompanyCode"].ToString();
            }
            catch
            {
                MessageBox.Show("Error!!! Please check the companycode configuration.", "RetailPlus", MessageBoxButtons.OK, MessageBoxIcon.Information);
                ErrorLog("Error!!! Please check the companycode configuration.");
                return false;
            }
            string strMobileNo = "09473215979";

            try
            {
                if (objclsSMS.sendMsg(this.port, strMobileNo, "SMS Application is connected @ " + strCompanyCode + "."))
                    return true;
                else
                    return false;
            }
            catch (Exception ex)
            {
                ErrorLog(ex.Message);
                return false;
            }
        }

        static List<USBDeviceInfo> GetUSBDevices()
        {
            List<USBDeviceInfo> devices = new List<USBDeviceInfo>();

            ManagementObjectCollection collection;
            using (var searcher = new ManagementObjectSearcher(@"Select * From Win32_USBHub"))
                collection = searcher.Get();

            foreach (var device in collection)
            {
                devices.Add(new USBDeviceInfo(
                (string)device.GetPropertyValue("DeviceID"),
                (string)device.GetPropertyValue("PNPDeviceID"),
                (string)device.GetPropertyValue("Description")
                ));
            }

            collection.Dispose();
            return devices;

            //var usbDevices = GetUSBDevices();

            //foreach (var usbDevice in usbDevices)
            //{
            //    Console.WriteLine("Device ID: {0}, PNP Device ID: {1}, Description: {2}",
            //        usbDevice.DeviceID, usbDevice.PnpDeviceID, usbDevice.Description);
            //}
        }

        private void Connect(bool inittab = true)
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;

                bool boPortOpen = false;
                if (chkAutoConnect.Checked)
                {
                    cboPortName.Items.Clear();
                    foreach (string portname in System.IO.Ports.SerialPort.GetPortNames())
                    {
                        cboPortName.Items.Add(portname);
                    }
                    foreach (string portname in System.IO.Ports.SerialPort.GetPortNames())
                    {
                        try
                        {
                            //Open communication port 
                            cboPortName.Text = portname;
                            try
                            {
                                objclsSMS.ClosePort(this.port);
                            }
                            catch { }
                            try
                            {
                                ErrorLog(portname + " : opening port");
                                this.port = objclsSMS.OpenPort(this.cboPortName.Text, Convert.ToInt32(this.cboBaudRate.Text), Convert.ToInt32(this.cboDataBits.Text), Convert.ToInt32(this.txtReadTimeOut.Text), Convert.ToInt32(this.txtWriteTimeOut.Text));
                                ErrorLog(portname + " : port is open");
                            }
                            catch (Exception ex)
                            {
                                ErrorLog("error opening..." + ex.Message);
                            }

                            if (isModemOK())
                            { boPortOpen = true; break; }
                            else
                            {
                                objclsSMS.ClosePort(this.port);
                            }
                        }
                        catch (Exception ex)
                        {
                            ErrorLog(ex.Message);
                        }
                    }
                }
                else 
                {
                    try
                    {
                        //Open communication port 
                        this.port = objclsSMS.OpenPort(this.cboPortName.Text, Convert.ToInt32(this.cboBaudRate.Text), Convert.ToInt32(this.cboDataBits.Text), Convert.ToInt32(this.txtReadTimeOut.Text), Convert.ToInt32(this.txtWriteTimeOut.Text));

                        if (isModemOK())
                        { boPortOpen = true; }
                        else
                        {
                            objclsSMS.ClosePort(this.port);
                        }
                    }
                    catch { }
                }

                if (!boPortOpen)
                {
                    this.statusBar1.Text = "Sorry no GSM modem detected.";
                    return;
                }

                if (this.port != null)
                {
                    this.gboPortSettings.Enabled = false;

                    //MessageBox.Show("Modem is connected at PORT " + this.cboPortName.Text);
                    this.statusBar1.Text = "Modem is connected at PORT " + this.cboPortName.Text;

                    if (inittab)
                    {
                        //Add tab pages
                        this.tabSMSapplication.TabPages.Add(tbSendSMS);
                        this.tabSMSapplication.TabPages.Add(tbReadSMS);
                        this.tabSMSapplication.TabPages.Add(tbDeleteSMS);
                    }

                    this.lblConnectionStatus.Text = "Connected at " + this.cboPortName.Text;

                    this.btnDisconnect.Enabled = true;
                }

                else
                {
                    //MessageBox.Show("Invalid port settings");
                    this.statusBar1.Text = "Invalid port settings";
                }
            }
            catch (Exception ex)
            {
                ErrorLog(ex.Message);
            }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            Connect();
            Cursor.Current = Cursors.Default;

        }
        private void btnDisconnect_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            try
            {
                this.gboPortSettings.Enabled = true;
                objclsSMS.ClosePort(this.port);

                //Remove tab pages
                this.tabSMSapplication.TabPages.Remove(tbSendSMS);
                this.tabSMSapplication.TabPages.Remove(tbReadSMS);
                this.tabSMSapplication.TabPages.Remove(tbDeleteSMS);

                this.lblConnectionStatus.Text = "Not Connected";
                this.btnDisconnect.Enabled = false;
                statusBar1.Text = "";
            }
            catch (Exception ex)
            {
                ErrorLog(ex.Message);
            }
            Cursor.Current = Cursors.Default;
        }

        private void btnSendSMS_Click(object sender, EventArgs e)
        {
            if (btnSendSMS.Text == "Cancel")
            {
                bgwSendSMS.CancelAsync();
                btnSendSMS.Text = "Send Again";
                return;
            }
            else if (btnSendSMS.Text == "Send Again")
            {
                bgwSendSMS.CancelAsync();
                btnSendSMS.Text = "Send";
                lvwSentMessages.Visible = false;
                cmdExportToFile.Visible = false;
                return;
            }

            if (string.IsNullOrEmpty(txtMessage.Text))
            {
                MessageBox.Show("Please enter the message to broadcast.", "RetailPlus", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (MessageBox.Show("Are you sure you want to broadcast this message?" + Environment.NewLine + Environment.NewLine + "Please verify the message and the recepient before confirming.", "RetailPlus", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == System.Windows.Forms.DialogResult.No)
                return;

            objclsSMS.ClosePort(this.port);

            Connect(false);

            Cursor.Current = Cursors.WaitCursor;

            lvwSentMessages.Items.Clear();
            lvwSentMessages.Visible = true;
            lvwSentMessages.Select();
            btnSendSMS.Text = "Cancel";

            this.bgwSendSMS.RunWorkerAsync();

            // Wait for the BackgroundWorker to finish the download.
            while (this.bgwSendSMS.IsBusy)
            {
                Application.DoEvents();
            }

            cmdExportToFile.Visible = true;
            btnSendSMS.Text = "Send Again";

            Cursor.Current = Cursors.Default;

            return;

        }
        private void btnReadSMS_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;

            try
            {
                //count SMS 
                int uCountSMS = objclsSMS.CountSMSmessages(this.port);
                if (uCountSMS > 0)
                {

                    #region Command
                    string strCommand = "AT+CMGL=\"ALL\"";

                    if (this.rbReadAll.Checked)
                    {
                        strCommand = "AT+CMGL=\"ALL\"";
                    }
                    else if (this.rbReadUnRead.Checked)
                    {
                        strCommand = "AT+CMGL=\"REC UNREAD\"";
                    }
                    else if (this.rbReadStoreSent.Checked)
                    {
                        strCommand = "AT+CMGL=\"STO SENT\"";
                    }
                    else if (this.rbReadStoreUnSent.Checked)
                    {
                        strCommand = "AT+CMGL=\"STO UNSENT\"";
                    }
                    #endregion

                    // If SMS exist then read SMS
                    #region Read SMS
                    //.............................................. Read all SMS ....................................................
                    objShortMessageCollection = objclsSMS.ReadSMS(this.port, strCommand);
                    foreach (ShortMessage msg in objShortMessageCollection)
                    {

                        ListViewItem item = new ListViewItem(new string[] { msg.Index, msg.Sent, msg.Sender, msg.Message });
                        item.Tag = msg;
                        lvwMessages.Items.Add(item);

                    }
                    #endregion

                }
                else
                {
                    lvwMessages.Clear();
                    //MessageBox.Show("There is no message in SIM");
                    this.statusBar1.Text = "There is no message in SIM";
                    
                }
            }
            catch (Exception ex)
            {
                ErrorLog(ex.Message);
            }
            Cursor.Current = Cursors.Default;
        }
        private void btnDeleteSMS_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            try
            {
                //Count SMS 
                int uCountSMS = objclsSMS.CountSMSmessages(this.port);
                if (uCountSMS > 0)
                {
                    DialogResult dr = MessageBox.Show("Are u sure u want to delete the SMS?", "Delete confirmation", MessageBoxButtons.YesNo);

                    if (dr.ToString() == "Yes")
                    {
                        #region Delete SMS

                        if (this.rbDeleteAllSMS.Checked)
                        {                           
                            //...............................................Delete all SMS ....................................................

                            #region Delete all SMS
                            string strCommand = "AT+CMGD=1,4";
                            if (objclsSMS.DeleteMsg(this.port, strCommand))
                            {
                                //MessageBox.Show("Messages has deleted successfuly ");
                                this.statusBar1.Text = "Messages has deleted successfuly";
                            }
                            else
                            {
                                //MessageBox.Show("Failed to delete messages ");
                                this.statusBar1.Text = "Failed to delete messages";
                            }
                            #endregion
                            
                        }
                        else if (this.rbDeleteReadSMS.Checked)
                        {                          
                            //...............................................Delete Read SMS ....................................................

                            #region Delete Read SMS
                            string strCommand = "AT+CMGD=1,3";
                            if (objclsSMS.DeleteMsg(this.port, strCommand))
                            {
                                //MessageBox.Show("Messages has deleted successfuly");
                                this.statusBar1.Text = "Messages has deleted successfuly";
                            }
                            else
                            {
                                //MessageBox.Show("Failed to delete messages ");
                                this.statusBar1.Text = "Failed to delete messages";
                            }
                            #endregion

                        }

                        #endregion
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorLog(ex.Message);
            }
            Cursor.Current = Cursors.Default;
        }
        private void btnCountSMS_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            try
            {
                //Count SMS
                int uCountSMS = objclsSMS.CountSMSmessages(this.port);
                this.txtCountSMS.Text = uCountSMS.ToString();
            }
            catch (Exception ex)
            {
                ErrorLog(ex.Message);
            }
            Cursor.Current = Cursors.Default;
        }

        private bool isValidMobileNo(string MobileNo)
        {
            try 
	        {
                Int64 iMobileNo = 0;
                bool isMobileNo = Int64.TryParse(MobileNo, out iMobileNo) ? true : false;

                if (!isMobileNo) return false;

                if (MobileNo.Length != 11) return false;

                return true;
	        }
	        catch
	        {
		        return false;
	        }
        }

        #endregion

        #region Error Log

        public void ErrorLog(string Message)
        {
            StreamWriter sw = null;

            try
            {
                WriteStatusBar(Message);

                string sLogFormat = DateTime.Now.ToShortDateString().ToString() + " " + DateTime.Now.ToLongTimeString().ToString() + " ==> ";
                //string sPathName = @"E:\";
                string sPathName = @"system_logs\";

                string sYear = DateTime.Now.Year.ToString();
                string sMonth = DateTime.Now.Month.ToString();
                string sDay = DateTime.Now.Day.ToString();

                string sErrorTime = sDay + "-" + sMonth + "-" + sYear;

                sw = new StreamWriter(sPathName + sErrorTime + ".txt", true);

                sw.WriteLine(sLogFormat + Message);
                sw.Flush();

            }
            catch (Exception ex)
            {
                //ErrorLog(ex.ToString());
            }
            finally
            {
                if (sw != null)
                {
                    sw.Dispose();
                    sw.Close();
                }
            }
            
        }

        #endregion 

        private void cmdLookFile_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Excel Files|*.xls;*.xlsx;*.xlsm";
            ofd.Multiselect = false;

            if (ofd.ShowDialog() == DialogResult.OK)
            {
                txtFilePath.Text = ofd.FileName;

            }
        }

        private void cmdExportToFIle_Click(object sender, EventArgs e)
        {
            export2File(lvwSentMessages, ",");
        }
    
    }
}
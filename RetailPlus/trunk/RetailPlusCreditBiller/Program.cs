using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AceSoft.RetailPlus.Client;
using System.Data;
using System.IO;

namespace AceSoft.RetailPlus.Monitor
{
	class MainModule
	{
		#region Application Main

		[STAThread]
		static void Main(string[] args)
		{
			try
			{
				WriteProcessToMonitor("Starting RetailPlus Credit Biller tool...");
				WriteProcessToMonitor("   ok");
			back:
				WriteProcessToMonitor("Checking connections to server.");
				if (IPAddress.IsOpen(AceSoft.RetailPlus.DBConnection.ServerIP(), DBConnection.DBPort()) == false)
				{
					WriteProcessToMonitor("   cannot connect to server please check.");
					goto exit;
				}
				WriteProcessToMonitor("   ok");
				WriteProcessToMonitor("Checking connections to database.");
				Data.Database clsDatabase = new Data.Database();
				try
				{
					bool boIsDBAlive = clsDatabase.IsAlive();
					WriteProcessToMonitor("   connected to '" + clsDatabase.Connection.ConnectionString.Split(';')[0].ToString().Replace("Data Source=", "") + "'");
				}
				catch (Exception ex)
				{
					WriteProcessToMonitor("   ERROR connecting to database. Exception: " + ex.ToString());
				}
				WriteProcessToMonitor("Checking credit billings to process...");
				WriteProcessToMonitor("   done checking...");
				
				ProcessCreditBill();

				WriteProcessToMonitor("Waiting for next process...");

				System.Threading.Thread.Sleep(20000);
				goto back;
			exit:
				WriteProcessToMonitor("Sytem terminated.");
			}
			catch (Exception ex) 
			{
				WriteProcessToMonitor("PLEASE CALL RETAILPLUS IMMEDIATELY... error:" + ex.ToString());
			}
		}

		#endregion

		#region Private Modifiers

		private static void WriteProcessToMonitor(string ProcessToWrite)
		{
			Event clsEvent = new Event();
			clsEvent.AddEventLn(ProcessToWrite);
			Console.WriteLine(ConsoleMonitor() + ProcessToWrite);
		}

		private static string ConsoleMonitor()
		{
			return DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + ": ";
		}

		private static void ProcessCreditBill()
		{
			Event clsEvent = new Event();
			clsEvent.AddEventLn("");
			Console.WriteLine(ConsoleMonitor() + "");

			Data.Billing clsBilling = new Data.Billing();

            // check billingdate
            DateTime dteBillingDate = clsBilling.getBillingDate();
            List<Data.BillingDetails> lstBillingDetails;

            if (dteBillingDate == Constants.C_DATE_MIN_VALUE)
            {
                clsBilling.CommitAndDispose();
                WriteProcessToMonitor("Will not process credit bill. There is no BillingDate set in the database. Please contact your System Administrator.");
                return;
            }
            else if (dteBillingDate >= DateTime.Now)
            {
                WriteProcessToMonitor("Will not process credit bill. Next processing date must be after BillingDate: [" + dteBillingDate.ToString("dd-MMM-yyyy") + "]. System will only process after billing date. Printing unprinted OR's instead. ");

                lstBillingDetails = clsBilling.List(BillingDate: dteBillingDate, SortField: "CurrentDueAmount", SortOrder: System.Data.SqlClient.SortOrder.Descending);
                clsBilling.CommitAndDispose();
                PrintORs(lstBillingDetails);
                return;
            }

            // Check PurchaseEndDate if lower than today then do not execute.
            DateTime dteCreditPurcEndDateToProcess = clsBilling.getCreditPurcEndDateToProcess();
            if (dteCreditPurcEndDateToProcess >= DateTime.Now)
            {
                WriteProcessToMonitor("Will not process credit bill. CreditPurcEndDateToProcess: " + dteCreditPurcEndDateToProcess.ToString("dd-MMM-yyyy") + " is lower than current date. . Printing unprinted OR's instead. ");

                lstBillingDetails = clsBilling.List(BillingDate: dteBillingDate, SortField: "CurrentDueAmount", SortOrder: System.Data.SqlClient.SortOrder.Descending);
                clsBilling.CommitAndDispose();
                PrintORs(lstBillingDetails);
                return;
            }
            WriteProcessToMonitor("Processing credit bill for BillingDate: [" + dteBillingDate.ToString("dd-MMM-yyyy") + "]. ");
			clsBilling.ProcessCurrentBill();

            // print SOA first before closing the billing date to make sure all are printed
            lstBillingDetails = clsBilling.List(BillingDate: dteBillingDate, SortField: "CurrentDueAmount", SortOrder: System.Data.SqlClient.SortOrder.Descending);
            clsBilling.CommitAndDispose();

            PrintORs(lstBillingDetails);

            WriteProcessToMonitor("Closing current billing date...");
            clsBilling.CloseCurrentBill();
            clsBilling.CommitAndDispose();

            WriteProcessToMonitor("Done.");
		}

        private static void PrintORs(List<Data.BillingDetails> lstBillingDetails)
        {
            foreach (Data.BillingDetails clsBillingDetails in lstBillingDetails)
            {
                WriteProcessToMonitor("Printing SOA of " + clsBillingDetails.CustomerDetails.ContactName + "...");
                if (string.IsNullOrEmpty(clsBillingDetails.BillingFile))
                {
                    WriteProcessToMonitor("[" + clsBillingDetails.CustomerDetails.ContactName + "] CrediLimit                : " + clsBillingDetails.CrediLimit.ToString(Constants.C_FE_DEFAULT_DECIMAL_FORMAT));
                    WriteProcessToMonitor("[" + clsBillingDetails.CustomerDetails.ContactName + "] Credit                    : " + clsBillingDetails.CurrentDueAmount.ToString(Constants.C_FE_DEFAULT_DECIMAL_FORMAT));
                    WriteProcessToMonitor("[" + clsBillingDetails.CustomerDetails.ContactName + "] CurrentDueAmount          : " + clsBillingDetails.CurrentDueAmount.ToString(Constants.C_FE_DEFAULT_DECIMAL_FORMAT));
                    WriteProcessToMonitor("[" + clsBillingDetails.CustomerDetails.ContactName + "] TotalBillCharges          : " + clsBillingDetails.TotalBillCharges.ToString(Constants.C_FE_DEFAULT_DECIMAL_FORMAT));
                    WriteProcessToMonitor("[" + clsBillingDetails.CustomerDetails.ContactName + "] CurrMonthAmountPaid       : " + clsBillingDetails.CurrMonthAmountPaid.ToString(Constants.C_FE_DEFAULT_DECIMAL_FORMAT));
                    WriteProcessToMonitor("[" + clsBillingDetails.CustomerDetails.ContactName + "] MinimumAmountDue          : " + clsBillingDetails.MinimumAmountDue.ToString(Constants.C_FE_DEFAULT_DECIMAL_FORMAT));
                    WriteProcessToMonitor("[" + clsBillingDetails.CustomerDetails.ContactName + "] Prev1MoCurrentDueAmount   : " + clsBillingDetails.Prev1MoCurrentDueAmount.ToString(Constants.C_FE_DEFAULT_DECIMAL_FORMAT));
                    WriteProcessToMonitor("[" + clsBillingDetails.CustomerDetails.ContactName + "] Prev2MoCurrentDueAmount   : " + clsBillingDetails.Prev2MoCurrentDueAmount.ToString(Constants.C_FE_DEFAULT_DECIMAL_FORMAT));

                    string strOR = PrintCreditBill(clsBillingDetails);
                    if (strOR != "")
                    {
                        WriteProcessToMonitor("[" + clsBillingDetails.CustomerDetails.ContactName + "] Bill createad @ " + strOR);
                        Data.Billing clsBilling = new Data.Billing();
                        clsBilling.SetBillingAsPrinted(clsBillingDetails.ContactID, clsBillingDetails.BillingDate, strOR);
                        clsBilling.CommitAndDispose();
                    }
                    WriteProcessToMonitor("[" + clsBillingDetails.CustomerDetails.ContactName + "] Done.");
                }
                else
                {
                    WriteProcessToMonitor("[" + clsBillingDetails.CustomerDetails.ContactName + "] Done. did not print SOA, already printed @ " + clsBillingDetails.BillingFile);
                }
            }
        }

		protected static string PrintCreditBill(Data.BillingDetails clsBillingDetails)
		{
			CreditBiller.CRSReports.Billing rpt = new CreditBiller.CRSReports.Billing();
			CreditBiller.ReportDataset rptds = new CreditBiller.ReportDataset();
			Data.Billing clsBilling = new Data.Billing();
			System.Data.DataTable dt = clsBilling.ListDetailsAsDataTable(clsBillingDetails.CreditBillHeaderID);
			clsBilling.CommitAndDispose();

			System.Data.DataRow drNew;
			foreach (System.Data.DataRow dr in dt.Rows )
			{
				drNew = rptds.CreditBillDetail.NewRow();

				foreach (System.Data.DataColumn dc in rptds.CreditBillDetail.Columns)
					drNew[dc] = dr[dc.ColumnName];

				rptds.CreditBillDetail.Rows.Add(drNew);
			}

			rpt.SetDataSource(rptds);

			CrystalDecisions.CrystalReports.Engine.ParameterFieldDefinition paramField;
			CrystalDecisions.Shared.ParameterValues currentValues;
			CrystalDecisions.Shared.ParameterDiscreteValue discreteParam;

			paramField = rpt.DataDefinition.ParameterFields["CompanyName"];
			discreteParam = new CrystalDecisions.Shared.ParameterDiscreteValue();
			discreteParam.Value = CompanyDetails.CompanyName;
			currentValues = new CrystalDecisions.Shared.ParameterValues();
			currentValues.Add(discreteParam);
			paramField.ApplyCurrentValues(currentValues);

			paramField = rpt.DataDefinition.ParameterFields["BillingDate"];
			discreteParam = new CrystalDecisions.Shared.ParameterDiscreteValue();
			discreteParam.Value = clsBillingDetails.BillingDate;
			currentValues = new CrystalDecisions.Shared.ParameterValues();
			currentValues.Add(discreteParam);
			paramField.ApplyCurrentValues(currentValues);

			paramField = rpt.DataDefinition.ParameterFields["PaymentDueDate"];
			discreteParam = new CrystalDecisions.Shared.ParameterDiscreteValue();
            discreteParam.Value = clsBillingDetails.CreditPaymentDueDate;
			currentValues = new CrystalDecisions.Shared.ParameterValues();
			currentValues.Add(discreteParam);
			paramField.ApplyCurrentValues(currentValues);

			paramField = rpt.DataDefinition.ParameterFields["CompanyAddress"];
			discreteParam = new CrystalDecisions.Shared.ParameterDiscreteValue();
			discreteParam.Value = CompanyDetails.Address1 +
									Environment.NewLine + CompanyDetails.Address2 + ", " + CompanyDetails.City + " " + CompanyDetails.Country +
									Environment.NewLine + "Tel #: " + CompanyDetails.OfficePhone + " Fax #:" + CompanyDetails.FaxPhone +
									Environment.NewLine + "TIN : " + CompanyDetails.TIN + "      VAT Reg.";
			currentValues = new CrystalDecisions.Shared.ParameterValues();
			currentValues.Add(discreteParam);
			paramField.ApplyCurrentValues(currentValues);


			paramField = rpt.DataDefinition.ParameterFields["ContactName"];
			discreteParam = new CrystalDecisions.Shared.ParameterDiscreteValue();
			discreteParam.Value = clsBillingDetails.CustomerDetails.ContactName;
			currentValues = new CrystalDecisions.Shared.ParameterValues();
			currentValues.Add(discreteParam);
			paramField.ApplyCurrentValues(currentValues);

			paramField = rpt.DataDefinition.ParameterFields["ContactAddress"];
			discreteParam = new CrystalDecisions.Shared.ParameterDiscreteValue();
			discreteParam.Value = clsBillingDetails.CustomerDetails.Address;
			currentValues = new CrystalDecisions.Shared.ParameterValues();
			currentValues.Add(discreteParam);
			paramField.ApplyCurrentValues(currentValues);

			paramField = rpt.DataDefinition.ParameterFields["CreditCardNo"];
			discreteParam = new CrystalDecisions.Shared.ParameterDiscreteValue();
			discreteParam.Value = clsBillingDetails.CustomerDetails.CreditDetails.CreditCardNo;
			currentValues = new CrystalDecisions.Shared.ParameterValues();
			currentValues.Add(discreteParam);
			paramField.ApplyCurrentValues(currentValues);

			paramField = rpt.DataDefinition.ParameterFields["CreditLimit"];
			discreteParam = new CrystalDecisions.Shared.ParameterDiscreteValue();
			discreteParam.Value = clsBillingDetails.CrediLimit;
			currentValues = new CrystalDecisions.Shared.ParameterValues();
			currentValues.Add(discreteParam);
			paramField.ApplyCurrentValues(currentValues);

			paramField = rpt.DataDefinition.ParameterFields["CurrentDueAmount"];
			discreteParam = new CrystalDecisions.Shared.ParameterDiscreteValue();
			discreteParam.Value = clsBillingDetails.CurrentDueAmount;
			currentValues = new CrystalDecisions.Shared.ParameterValues();
			currentValues.Add(discreteParam);
			paramField.ApplyCurrentValues(currentValues);

			paramField = rpt.DataDefinition.ParameterFields["MinimumAmountDue"];
			discreteParam = new CrystalDecisions.Shared.ParameterDiscreteValue();
			discreteParam.Value = clsBillingDetails.CurrentDueAmount;
			currentValues = new CrystalDecisions.Shared.ParameterValues();
			currentValues.Add(discreteParam);
			paramField.ApplyCurrentValues(currentValues);

			paramField = rpt.DataDefinition.ParameterFields["PrevMoCurrentDueAmount"];
			discreteParam = new CrystalDecisions.Shared.ParameterDiscreteValue();
            discreteParam.Value = clsBillingDetails.Prev1MoCurrentDueAmount + clsBillingDetails.Prev2MoCurrentDueAmount;
			currentValues = new CrystalDecisions.Shared.ParameterValues();
			currentValues.Add(discreteParam);
			paramField.ApplyCurrentValues(currentValues);

            paramField = rpt.DataDefinition.ParameterFields["CreditPurcStartDateToProcess"];
            discreteParam = new CrystalDecisions.Shared.ParameterDiscreteValue();
            discreteParam.Value = clsBillingDetails.CreditPurcStartDateToProcess;
            currentValues = new CrystalDecisions.Shared.ParameterValues();
            currentValues.Add(discreteParam);
            paramField.ApplyCurrentValues(currentValues);

            paramField = rpt.DataDefinition.ParameterFields["CreditPurcEndDateToProcess"];
            discreteParam = new CrystalDecisions.Shared.ParameterDiscreteValue();
            discreteParam.Value = clsBillingDetails.CreditPurcEndDateToProcess;
            currentValues = new CrystalDecisions.Shared.ParameterValues();
            currentValues.Add(discreteParam);
            paramField.ApplyCurrentValues(currentValues);

			string strRetValue = "";
			try
			{
                string logsdir = System.Configuration.ConfigurationManager.AppSettings["billingdir"].ToString();

				if (!Directory.Exists(logsdir))
				{
					Directory.CreateDirectory(logsdir);
				}
                string logFile = logsdir + "OR_" + clsBillingDetails.ContactID.ToString() + clsBillingDetails.BillingDate.ToString("yyyyMMdd") + ".pdf";

                if (File.Exists(logFile))
                {
                    MoveCreditBillToNextFile(logFile, 1);
                }

				rpt.ExportToDisk(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, logFile);

                strRetValue = "OR_" + clsBillingDetails.ContactID.ToString() + clsBillingDetails.BillingDate.ToString("yyyyMMdd") + ".pdf";
			}
			catch { }


			//rpt.PrintOptions.PrinterName = mclsTerminalDetails.SalesInvoicePrinterName;
			//rpt.PrintToPrinter(1, false, 0, 0);

			rpt.Close();
			rpt.Dispose();
			
			return strRetValue;
		}

        private static void MoveCreditBillToNextFile(string logFile, Int32 iCtr)
        {
            if (File.Exists(logFile + "_" + iCtr.ToString() + ".old"))
            {
                MoveCreditBillToNextFile(logFile, iCtr + 1 );
            }
            else
            {
                File.Move(logFile, logFile + "_" + iCtr.ToString() + ".old");
            }
        }

		#endregion

	}
}

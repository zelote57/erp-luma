using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.IO;
using System.Management;
using AceSoft.RetailPlus.Client;

namespace AceSoft.RetailPlus.Monitor
{
	class MainModule
	{
        private static MySql.Data.MySqlClient.MySqlConnection mConnection; private static MySql.Data.MySqlClient.MySqlTransaction mTransaction;

		#region Application Main

		[STAThread]
		static void Main(string[] args)
		{
            WriteProcessToMonitor("Starting RetailPlus Credit Biller tool...");
            WriteProcessToMonitor("   ok");
        back:

			try
			{
				WriteProcessToMonitor("Checking connections to server.");
				if (IPAddress.IsOpen(AceSoft.RetailPlus.DBConnection.ServerIP(), DBConnection.DBPort()) == false)
				{
					WriteProcessToMonitor("   cannot connect to server please check.");
					goto exit;
				}
				WriteProcessToMonitor("   ok");
				WriteProcessToMonitor("Checking connections to database.");
				Data.Database clsDatabase = new Data.Database();
                mConnection = clsDatabase.Connection; mTransaction = clsDatabase.Transaction;

				try
				{
					bool boIsDBAlive = clsDatabase.IsAlive();
					WriteProcessToMonitor("   connected to '" + clsDatabase.Connection.ConnectionString.Split(';')[0].ToString().Replace("Data Source=", "") + "'");
				}
				catch (Exception ex)
				{
					WriteProcessToMonitor("   ERROR connecting to database. Exception: " + ex.ToString());
                    clsDatabase.CommitAndDispose();
                    return;
				}
                WriteProcessToMonitor("Checking credit billings to process...");
				WriteProcessToMonitor("   done checking...");

                Data.CardType clsCardType = new Data.CardType(mConnection, mTransaction);
                mConnection = clsCardType.Connection; mTransaction = clsCardType.Transaction;
                System.Data.DataTable dt = clsCardType.ListAsDataTable(new Data.CardTypeDetails() { CreditCardType = CreditCardTypes.Internal, CheckGuarantor = true, WithGuarantor = false });
                clsCardType.CommitAndDispose();
                clsDatabase.CommitAndDispose();

                foreach (System.Data.DataRow dr in dt.Rows)
                {
                    WriteProcessToMonitor("Processing " + dr["CardTypeName"].ToString() + "...");
                    ProcessCreditBill(dr["CardTypeName"].ToString());
                }

                WriteProcessToMonitor("Checking credit billings with guarantor to process...");
                WriteProcessToMonitor("   done checking...");

                clsCardType = new Data.CardType(mConnection, mTransaction);
                mConnection = clsCardType.Connection; mTransaction = clsCardType.Transaction;
                dt = clsCardType.ListAsDataTable(new Data.CardTypeDetails() { CreditCardType = CreditCardTypes.Internal, CheckGuarantor = true, WithGuarantor = true });
                clsCardType.CommitAndDispose();
                clsDatabase.CommitAndDispose();

                foreach (System.Data.DataRow dr in dt.Rows)
                {
                    WriteProcessToMonitor("Processing " + dr["CardTypeName"].ToString() + "...");
                    ProcessCreditBillWG(dr["CardTypeName"].ToString());
                }

                WriteProcessToMonitor("Waiting for next process...");

				System.Threading.Thread.Sleep(20000);
				goto back;
			exit:
				WriteProcessToMonitor("Sytem terminated.");
			}
			catch (Exception ex) 
			{
				WriteProcessToMonitor("PLEASE CALL RETAILPLUS IMMEDIATELY... error:" + ex.ToString());
                goto back;
			}
		}

		#endregion


		#region CreditBills for Creditors W/out Guarantor

		private static void ProcessCreditBill(string CardTypeName)
		{
			Event clsEvent = new Event();
			clsEvent.AddEventLn("");
			Console.WriteLine(ConsoleMonitor() + "");

            LocalDB clsLocalDB = new LocalDB();
            mConnection = clsLocalDB.Connection; mTransaction = clsLocalDB.Transaction;

			Data.Billing clsBilling = new Data.Billing(mConnection, mTransaction);
            mConnection = clsBilling.Connection; mTransaction = clsBilling.Transaction;

            // check billingdate
            Data.CardType clsCardType = new Data.CardType(mConnection, mTransaction);
            mConnection = clsBilling.Connection; mTransaction = clsBilling.Transaction;

            Data.CardTypeDetails clsCreditCardTypeInfo = clsCardType.Details(CardTypeName);

            try
            {
                List<Data.BillingDetails> lstBillingDetails;

                if (clsCreditCardTypeInfo.BillingDate == Constants.C_DATE_MIN_VALUE)
                {
                    clsLocalDB.CommitAndDispose();
                    WriteProcessToMonitor("Will not process credit bill. There is no BillingDate set in the database. Please contact your System Administrator.");
                    return;
                }
                else if (clsCreditCardTypeInfo.BillingDate >= DateTime.Now)
                {
                    WriteProcessToMonitor("Will not process credit bill. Next processing date must be after BillingDate: [" + clsCreditCardTypeInfo.BillingDate.ToString("dd-MMM-yyyy") + "]. System will only process after billing date. Printing unprinted OR's instead. ");

                    lstBillingDetails = clsBilling.List(BillingDate: clsCreditCardTypeInfo.BillingDate, SortField: "CurrentDueAmount", SortOrder: System.Data.SqlClient.SortOrder.Descending);
                    clsLocalDB.CommitAndDispose();
                    PrintORs(lstBillingDetails);
                    return;
                }

                // Check PurchaseEndDate if lower than today then do not execute.
                if (clsCreditCardTypeInfo.CreditPurcEndDateToProcess >= DateTime.Now)
                {
                    WriteProcessToMonitor("Will not process credit bill. CreditPurcEndDateToProcess: " + clsCreditCardTypeInfo.CreditPurcEndDateToProcess.ToString("dd-MMM-yyyy") + " is lower than current date. . Printing unprinted OR's instead. ");

                    lstBillingDetails = clsBilling.List(BillingDate: clsCreditCardTypeInfo.BillingDate, SortField: "CurrentDueAmount", SortOrder: System.Data.SqlClient.SortOrder.Descending);
                    clsLocalDB.CommitAndDispose();
                    PrintORs(lstBillingDetails);
                    return;
                }
                WriteProcessToMonitor("Processing credit bill for BillingDate: [" + clsCreditCardTypeInfo.BillingDate.ToString("dd-MMM-yyyy") + "]. ");
                clsBilling.ProcessCurrentBill(CreditType.Individual, clsCreditCardTypeInfo.CardTypeCode);

                // print SOA first before closing the billing date to make sure all are printed
                lstBillingDetails = clsBilling.List(CreditType: CreditType.Individual, CreditCardTypeID: clsCreditCardTypeInfo.CardTypeID, BillingDate: clsCreditCardTypeInfo.BillingDate, SortField: "CurrentDueAmount", SortOrder: System.Data.SqlClient.SortOrder.Descending);
                clsBilling.CommitAndDispose();

                PrintORs(lstBillingDetails);

                WriteProcessToMonitor("Closing current billing date...");
                clsBilling = new Data.Billing(mConnection, mTransaction);
                mConnection = clsBilling.Connection; mTransaction = clsBilling.Transaction;

                clsBilling.CloseCurrentBill(CreditType.Individual, clsCreditCardTypeInfo.CardTypeCode);
                WriteProcessToMonitor("Done.");
            }
            catch (Exception ex)
            {
                WriteProcessToMonitor("PLEASE CALL RETAILPLUS IMMEDIATELY... PROCESS-CreditBillWoutGuarantor error:" + Environment.NewLine + ex.ToString());
                clsLocalDB.ThrowException(ex);
            }
            finally
            {
                clsLocalDB.CommitAndDispose();
            }
		}

        private static void PrintORs(List<Data.BillingDetails> lstBillingDetails)
        {
            Data.Billing clsBilling = new Data.Billing(mConnection, mTransaction);
            mConnection = clsBilling.Connection; mTransaction = clsBilling.Transaction;

            try
            {
                foreach (Data.BillingDetails clsBillingDetails in lstBillingDetails)
                {
                    WriteProcessToMonitor("Printing SOA of " + clsBillingDetails.CustomerDetails.ContactName + "...");
                    if (!clsBillingDetails.isBillPrinted)
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
                            clsBilling.SetBillingAsPrinted(CreditType.Individual, clsBillingDetails.ContactID, clsBillingDetails.BillingDate, strOR);
                        }
                        WriteProcessToMonitor("[" + clsBillingDetails.CustomerDetails.ContactName + "] Done.");
                    }
                    else
                    {
                        WriteProcessToMonitor("[" + clsBillingDetails.CustomerDetails.ContactName + "] Done. did not print SOA, already printed @ " + clsBillingDetails.BillingFile);
                    }
                }
            }
            catch (Exception ex)
            {
                WriteProcessToMonitor("PLEASE CALL RETAILPLUS IMMEDIATELY... PRINTING-CreditBillWoutGuarantor error:" + Environment.NewLine + ex.ToString());
                clsBilling.ThrowException(ex);
            }
            finally
            {
                clsBilling.CommitAndDispose();
            }
        }

		protected static string PrintCreditBill(Data.BillingDetails clsBillingDetails)
		{
            Data.Billing clsBilling = new Data.Billing(mConnection, mTransaction);
            mConnection = clsBilling.Connection; mTransaction = clsBilling.Transaction;
            System.Data.DataTable dt = clsBilling.ListDetailsAsDataTable(clsBillingDetails.CreditBillHeaderID);
            clsBilling.CommitAndDispose();

			CreditBiller.CRSReports.Billing rpt = new CreditBiller.CRSReports.Billing();
			CreditBiller.ReportDataset rptds = new CreditBiller.ReportDataset();

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
			discreteParam.Value = clsBillingDetails.MinimumAmountDue;
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

                logsdir += logsdir.EndsWith("/") ? "" : "/";
                if (!Directory.Exists(logsdir + "woutg"))
                {
                    Directory.CreateDirectory(logsdir + "woutg");
                }
                string logFile = logsdir + "woutg/OR_" + clsBillingDetails.ContactID.ToString() + clsBillingDetails.BillingDate.ToString("yyyyMMdd") + ".pdf";

                if (File.Exists(logFile))
                {
                    MoveCreditBillToNextFile(logFile, 1);
                }

				rpt.ExportToDisk(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, logFile);

                strRetValue = "OR_" + clsBillingDetails.ContactID.ToString() + clsBillingDetails.BillingDate.ToString("yyyyMMdd") + ".pdf";
			}
			catch { }

            try
            {
                if (isPrinterOnline("RetailPlusBilling"))
                {
                    rpt.PrintOptions.PrinterName = "RetailPlusBilling";
                    rpt.PrintToPrinter(1, false, 0, 0);
                }
                else
                {
                    WriteProcessToMonitor("will not print sales invoice. 'RetailPlusBilling' printer is offline.");
                }
            }
            catch{ }

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

        #region CreditBills for Creditors With Guarantor



        private static void ProcessCreditBillWG(string CardTypeName)
        {
            Event clsEvent = new Event();
            clsEvent.AddEventLn("");
            Console.WriteLine(ConsoleMonitor() + "");

            LocalDB clsLocalDB = new LocalDB();
            mConnection = clsLocalDB.Connection; mTransaction = clsLocalDB.Transaction;

            Data.Billing clsBilling = new Data.Billing(mConnection, mTransaction);
            mConnection = clsBilling.Connection; mTransaction = clsBilling.Transaction;

            Data.Contacts clsContacts = new Data.Contacts(mConnection, mTransaction);
            mConnection = clsContacts.Connection; mTransaction = clsContacts.Transaction;

            Data.CardType clsCardType = new Data.CardType(mConnection, mTransaction);
            mConnection = clsCardType.Connection; mTransaction = clsCardType.Transaction;

            Data.CardTypeDetails clsCreditCardTypeInfo = clsCardType.Details(CardTypeName);

            // check billingdate
            Data.ContactColumns clsContactColumns = new Data.ContactColumns() { ContactID = true, ContactCode = true, ContactName = true, CreditDetails = true };
            Data.ContactColumns clsSearchColumns = new Data.ContactColumns() { ContactCode = true, ContactName = true };
            
            System.Data.DataTable dtGuarantors;

            try
            {
                if (clsCreditCardTypeInfo.BillingDate == Constants.C_DATE_MIN_VALUE)
                {
                    clsLocalDB.CommitAndDispose();
                    WriteProcessToMonitor("Will not process group credit bill. There is no BillingDate set in the database. Please contact your System Administrator.");
                    return;
                }
                else if (clsCreditCardTypeInfo.BillingDate >= DateTime.Now)
                {
                    WriteProcessToMonitor("Will not process group credit bill. Next processing date must be after BillingDate: [" + clsCreditCardTypeInfo.BillingDate.ToString("dd-MMM-yyyy") + "]. System will only process after billing date. Printing unprinted OR's instead. ");

                    clsContacts = new Data.Contacts(mConnection, mTransaction);
                    mConnection = clsContacts.Connection; mTransaction = clsContacts.Transaction;

                    dtGuarantors = clsContacts.Guarantors(clsContactColumns, CreditCardTypeID: clsCreditCardTypeInfo.CardTypeID);
                    clsLocalDB.CommitAndDispose();
                    PrintORsWG(dtGuarantors, clsCreditCardTypeInfo);
                    return;
                }

                // Check PurchaseEndDate if lower than today then do not execute.
                if (clsCreditCardTypeInfo.CreditPurcEndDateToProcess >= DateTime.Now)
                {
                    WriteProcessToMonitor("Will not process group credit bill. CreditPurcEndDateToProcess: " + clsCreditCardTypeInfo.CreditPurcEndDateToProcess.ToString("dd-MMM-yyyy") + " is lower than current date. . Printing unprinted OR's instead. ");

                    clsContacts = new Data.Contacts(mConnection, mTransaction);
                    mConnection = clsContacts.Connection; mTransaction = clsContacts.Transaction;

                    dtGuarantors = clsContacts.Guarantors(clsContactColumns, CreditCardTypeID: clsCreditCardTypeInfo.CardTypeID);
                    clsLocalDB.CommitAndDispose();
                    PrintORsWG(dtGuarantors, clsCreditCardTypeInfo);
                    return;
                }
                WriteProcessToMonitor("Processing group credit bill for BillingDate: [" + clsCreditCardTypeInfo.BillingDate.ToString("dd-MMM-yyyy") + "]. ");
                clsBilling.ProcessCurrentBill(CreditType.Group, clsCreditCardTypeInfo.CardTypeCode);

                // print SOA first before closing the billing date to make sure all are printed
                clsContacts = new Data.Contacts(mConnection, mTransaction);
                mConnection = clsContacts.Connection; mTransaction = clsContacts.Transaction;
                dtGuarantors = clsContacts.Guarantors(clsContactColumns, CreditCardTypeID: clsCreditCardTypeInfo.CardTypeID);
                clsBilling.CommitAndDispose();
                PrintORsWG(dtGuarantors, clsCreditCardTypeInfo);

                WriteProcessToMonitor("Closing group current billing date...");
                clsBilling = new Data.Billing(mConnection, mTransaction);
                mConnection = clsBilling.Connection; mTransaction = clsBilling.Transaction;

                clsBilling.CloseCurrentBill(CreditType.Group, clsCreditCardTypeInfo.CardTypeCode);
                WriteProcessToMonitor("Done.");
            }
            catch (Exception ex)
            {
                WriteProcessToMonitor("PLEASE CALL RETAILPLUS IMMEDIATELY... PROCESS-CreditBillWithGuarantor error:" + Environment.NewLine + ex.ToString());
                clsLocalDB.ThrowException(ex);
            }
            finally
            {
                clsLocalDB.CommitAndDispose();
            }
        }

        private static void PrintORsWG(System.Data.DataTable dtGuarantors, Data.CardTypeDetails clsCreditCardTypeInfo)
        {
            Data.Billing clsBilling = new Data.Billing(mConnection, mTransaction);
            mConnection = clsBilling.Connection; mTransaction = clsBilling.Transaction;

            Data.Contacts clsContacts = new Data.Contacts(mConnection, mTransaction);
            mConnection = clsContacts.Connection; mTransaction = clsContacts.Transaction;

            Data.ContactDetails clsGuarantorDetails;
            System.Data.DataTable dtCreditors;

            try
            {
                foreach (System.Data.DataRow dr in dtGuarantors.Rows)
                {
                    clsGuarantorDetails = clsContacts.Details(Int64.Parse(dr["ContactID"].ToString()));
                    dtCreditors = clsBilling.ListAsDataTable(GuarantorID: clsGuarantorDetails.ContactID, CreditCardTypeID: clsCreditCardTypeInfo.CardTypeID, CreditType: CreditType.Group, BillingDate: clsCreditCardTypeInfo.BillingDate, SortField: "ContactName", SortOrder: System.Data.SqlClient.SortOrder.Descending);

                    if (dtCreditors.Rows.Count > 0)
                    {
                        WriteProcessToMonitor("Printing SOA of Guarantor: " + clsGuarantorDetails.ContactName + "...");
                        if (!bool.Parse(dtCreditors.Rows[0]["isBillPrinted"].ToString()))
                        {
                            WriteProcessToMonitor("[" + clsGuarantorDetails.ContactName + "] CurrentDueAmount          : " + clsGuarantorDetails.ContactGroupName);
                            WriteProcessToMonitor("[" + clsGuarantorDetails.ContactName + "] NoOfCreditors             : " + dtCreditors.Rows.Count.ToString("#,##0"));
                            WriteProcessToMonitor("[" + clsGuarantorDetails.ContactName + "] Credit Status             : " + clsGuarantorDetails.CreditDetails.CreditCardStatus.ToString("G") + " (" + (clsGuarantorDetails.CreditDetails.CreditActive ? "Active" : "InActive") + ")");

                            string strOR = PrintCreditBillWG(clsGuarantorDetails, dtCreditors, clsCreditCardTypeInfo);
                            if (strOR != "")
                            {
                                WriteProcessToMonitor("[" + clsGuarantorDetails.ContactName + "] Bill createad @ " + strOR);
                                clsBilling.SetBillingAsPrinted(CreditType.Group, clsGuarantorDetails.ContactID, clsCreditCardTypeInfo.BillingDate, strOR);
                            }
                            WriteProcessToMonitor("[" + clsGuarantorDetails.ContactName + "] Done.");
                        }
                        else
                        {
                            WriteProcessToMonitor("[" + clsGuarantorDetails.ContactName + "] Done. did not print SOA, already printed @ " + dtCreditors.Rows[0]["BillingFile"].ToString());
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                WriteProcessToMonitor("PLEASE CALL RETAILPLUS IMMEDIATELY... PRINTING-CreditBillWithGuarantor error:" + Environment.NewLine + ex.ToString());
                clsBilling.ThrowException(ex);
            }
            finally
            {
                clsBilling.CommitAndDispose();
            }
        }

        protected static string PrintCreditBillWG(Data.ContactDetails clsGuarantorDetails, System.Data.DataTable dtCreditors, Data.CardTypeDetails clsCreditCardTypeInfo)
        {
            CreditBiller.CRSReports.BillingWGuarantor rpt = new CreditBiller.CRSReports.BillingWGuarantor();
            CreditBiller.ReportDataset rptds = new CreditBiller.ReportDataset();

            System.Data.DataRow drNew;
            foreach (System.Data.DataRow dr in dtCreditors.Rows)
            {
                drNew = rptds.CreditBillHeader.NewRow();

                foreach (System.Data.DataColumn dc in rptds.CreditBillHeader.Columns)
                    drNew[dc] = dr[dc.ColumnName];

                rptds.CreditBillHeader.Rows.Add(drNew);
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
            discreteParam.Value = clsCreditCardTypeInfo.BillingDate;
            currentValues = new CrystalDecisions.Shared.ParameterValues();
            currentValues.Add(discreteParam);
            paramField.ApplyCurrentValues(currentValues);

            paramField = rpt.DataDefinition.ParameterFields["PaymentDueDate"];
            discreteParam = new CrystalDecisions.Shared.ParameterDiscreteValue();
            discreteParam.Value = clsCreditCardTypeInfo.CreditCutOffDate;
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
            discreteParam.Value = clsGuarantorDetails.ContactName;
            currentValues = new CrystalDecisions.Shared.ParameterValues();
            currentValues.Add(discreteParam);
            paramField.ApplyCurrentValues(currentValues);

            paramField = rpt.DataDefinition.ParameterFields["ContactAddress"];
            discreteParam = new CrystalDecisions.Shared.ParameterDiscreteValue();
            discreteParam.Value = clsGuarantorDetails.Address;
            currentValues = new CrystalDecisions.Shared.ParameterValues();
            currentValues.Add(discreteParam);
            paramField.ApplyCurrentValues(currentValues);

            paramField = rpt.DataDefinition.ParameterFields["CreditPurcStartDateToProcess"];
            discreteParam = new CrystalDecisions.Shared.ParameterDiscreteValue();
            discreteParam.Value = clsCreditCardTypeInfo.CreditPurcStartDateToProcess;
            currentValues = new CrystalDecisions.Shared.ParameterValues();
            currentValues.Add(discreteParam);
            paramField.ApplyCurrentValues(currentValues);

            paramField = rpt.DataDefinition.ParameterFields["CreditPurcEndDateToProcess"];
            discreteParam = new CrystalDecisions.Shared.ParameterDiscreteValue();
            discreteParam.Value = clsCreditCardTypeInfo.CreditPurcEndDateToProcess;
            currentValues = new CrystalDecisions.Shared.ParameterValues();
            currentValues.Add(discreteParam);
            paramField.ApplyCurrentValues(currentValues);

            string strRetValue = "";
            try
            {
                string logsdir = System.Configuration.ConfigurationManager.AppSettings["billingdir"].ToString();

                logsdir += logsdir.EndsWith("/") ? "" : "/";
                if (!Directory.Exists(logsdir + "withg"))
                {
                    Directory.CreateDirectory(logsdir + "withg");
                }
                string logFile = logsdir + "withg/OR_" + clsGuarantorDetails.ContactID.ToString() + clsCreditCardTypeInfo.BillingDate.ToString("yyyyMMdd") + ".pdf";

                if (File.Exists(logFile))
                {
                    MoveCreditBillToNextFile(logFile, 1);
                }

                rpt.ExportToDisk(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, logFile);

                strRetValue = "OR_" + clsGuarantorDetails.ContactID.ToString() + clsCreditCardTypeInfo.BillingDate.ToString("yyyyMMdd") + ".pdf";
            }
            catch { }

            try
            {
                if (isPrinterOnline("RetailPlusBilling"))
                {
                    rpt.PrintOptions.PrinterName = "RetailPlusBilling";
                    rpt.PrintToPrinter(1, false, 0, 0);
                }
                else
                {
                    WriteProcessToMonitor("will not print sales invoice. 'RetailPlusBilling' printer is offline.");
                }
            }
            catch { }

            rpt.Close();
            rpt.Dispose();

            return strRetValue;
        }

        private static void MoveCreditBillToNextFileWG(string logFile, Int32 iCtr)
        {
            if (File.Exists(logFile + "_" + iCtr.ToString() + ".old"))
            {
                MoveCreditBillToNextFileWG(logFile, iCtr + 1);
            }
            else
            {
                File.Move(logFile, logFile + "_" + iCtr.ToString() + ".old");
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

        public static void PrintProps(ManagementObject o, string prop)
        {
            try { Console.WriteLine(prop + "|" + o[prop]); }
            catch (Exception e) { Console.Write(e.ToString()); }
        }
        public static bool isPrinterOnline(string objPrinterName)
        {
            bool boretValue = false;
            try
            {
                ManagementObjectSearcher searcher = new ManagementObjectSearcher("SELECT * FROM Win32_Printer");

                foreach (ManagementObject printer in searcher.Get())
                {
                    string printerName = printer["Name"].ToString().ToLower();

                    if (printerName == objPrinterName.ToLower())
                    {
                        boretValue = true;
                        break;
                    }
                    //Console.WriteLine("Printer".PadRight(15) + ":" + printerName);

                    //PrintProps(printer, "Caption");
                    //PrintProps(printer, "ExtendedPrinterStatus");
                    //PrintProps(printer, "Availability");
                    //PrintProps(printer, "Default");
                    //PrintProps(printer, "DetectedErrorState");
                    //PrintProps(printer, "ExtendedDetectedErrorState");
                    //PrintProps(printer, "ExtendedPrinterStatus");
                    //PrintProps(printer, "LastErrorCode");
                    //PrintProps(printer, "PrinterState");
                    //PrintProps(printer, "PrinterStatus");
                    //PrintProps(printer, "Status");
                    //PrintProps(printer, "WorkOffline");
                    //PrintProps(printer, "Local");
                }
            }
            catch { }
            return boretValue;
        }

        #endregion

    }
}

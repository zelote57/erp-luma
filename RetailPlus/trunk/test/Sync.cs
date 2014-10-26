//using System;
//using System.Management;
//using AceSoft.RetailPlus;
//using MySql.Data.MySqlClient;
//using System.Data;
//using AceSoft.RetailPlus.Data;
//using AceSoft.RetailPlus.Reports;
//using AceSoft.RetailPlus.Security;

//namespace Test
//{
//    /******************************************************************************
//    **		Auth: Lemuel E. Aceron
//    **		Date: May 21, 2006
//    *******************************************************************************
//    **		Change History
//    *******************************************************************************
//    **		Date:		Author:				Description:
//    **		--------		--------				-------------------------------------------
//    **    
//    *******************************************************************************/

//    /// <summary>
//    /// Summary description for KeyGen.
//    /// </summary>
//    public class KeyGen
//    {
//        public static void Main(string[] args)
//        {
//            try
//            {
//                DateTime dteProcessStartDate = DateTime.Now;
//                Console.WriteLine(Environment.NewLine);
//                Console.WriteLine("[" + dteProcessStartDate.ToString("yyyy-mm-dd HH:mm:ss") + "]" + " starting process...");

//                DateTime dteStartSyncDateTime = DateTime.Now.AddDays(-1000);
//                DateTime dteEndSyncDateTime = Constants.C_DATE_MIN_VALUE;

//                AceSoft.RetailPlus.Client.MasterDB clsMasterConnection = new AceSoft.RetailPlus.Client.MasterDB();
//                clsMasterConnection.GetConnection();

//                AceSoft.RetailPlus.Client.LocalDB clsLocalConnection = new AceSoft.RetailPlus.Client.LocalDB();
//                clsLocalConnection.GetConnection();

//                AceSoft.RetailPlus.Client.DBSync clsMasterDB = new AceSoft.RetailPlus.Client.DBSync(clsMasterConnection.Connection, clsMasterConnection.Transaction);
//                AceSoft.RetailPlus.Client.DBSync clsLocalDB = new AceSoft.RetailPlus.Client.DBSync(clsLocalConnection.Connection, clsLocalConnection.Transaction);

//                clsLocalDB.SetForeignKey(false);

//                /**** Sync Configs *****/

//                // sysconfig
//                System.Data.DataTable dtMaster = new System.Data.DataTable();
//                System.Data.DataTable dtLocal = new System.Data.DataTable();
//                DateTime dteCreatedOn; DateTime dteLastModified; string strTableName; string stRetvalue;

//                #region SysConfig
//                strTableName = "SysConfig";
//                dtMaster = clsMasterDB.ListAsDataTable(strTableName, dteStartSyncDateTime, dteEndSyncDateTime);
//                dtLocal = clsLocalDB.ListAsDataTable(strTableName, dteStartSyncDateTime, dteEndSyncDateTime);
//                SysConfig clsSysConfig = new SysConfig(clsLocalConnection.Connection, clsLocalConnection.Transaction);

//                // update all records that are changed [inserted/updated]
//                foreach (DataRow dr in dtMaster.Rows)
//                {
//                    //DataRow[] drx = dtLocal.Select("Configname = '" + dr["ConfigName"].ToString() + "' ");
//                    if (!DateTime.TryParse(dr["CreatedOn"].ToString(), out dteCreatedOn)) dteCreatedOn = Constants.C_DATE_MIN_VALUE;
//                    if (!DateTime.TryParse(dr["LastModified"].ToString(), out dteLastModified)) dteLastModified = Constants.C_DATE_MIN_VALUE;

//                    clsSysConfig.Save(new SysConfigDetails
//                    {
//                        ConfigName = dr["ConfigName"].ToString(),
//                        ConfigValue = dr["ConfigValue"].ToString(),
//                        Category = dr["Category"].ToString(),
//                        CreatedOn = dteCreatedOn,
//                        LastModified = dteLastModified
//                    });
//                }

//                // delete the records in localDB that are supposed to be deleted
//                if (clsLocalDB.CountOfRecords(strTableName) > clsMasterDB.CountOfRecords(strTableName))
//                {
//                    stRetvalue = clsMasterDB.getAllIDs(strTableName, "ConfigName");
//                    clsLocalDB.Delete(strTableName, "ConfigName", stRetvalue);
//                }
//                #endregion

//                #region sysCreditConfig
//                strTableName = "sysCreditConfig";
//                dtMaster = clsMasterDB.ListAsDataTable(strTableName, dteStartSyncDateTime, dteEndSyncDateTime);
//                dtLocal = clsLocalDB.ListAsDataTable(strTableName, dteStartSyncDateTime, dteEndSyncDateTime);
//                SysCreditConfig clsSysCreditConfig = new SysCreditConfig(clsLocalConnection.Connection, clsLocalConnection.Transaction);

//                // update all records that are changed [inserted/updated]
//                foreach (DataRow dr in dtMaster.Rows)
//                {
//                    //DataRow[] drx = dtLocal.Select("Configname = '" + dr["ConfigName"].ToString() + "' ");
//                    if (!DateTime.TryParse(dr["CreatedOn"].ToString(), out dteCreatedOn)) dteCreatedOn = Constants.C_DATE_MIN_VALUE;
//                    if (!DateTime.TryParse(dr["LastModified"].ToString(), out dteLastModified)) dteLastModified = Constants.C_DATE_MIN_VALUE;

//                    clsSysCreditConfig.Save(new SysCreditConfigDetails
//                    {
//                        ConfigName = dr["ConfigName"].ToString(),
//                        ConfigValue = dr["ConfigValue"].ToString(),
//                        Remarks = dr["Remarks"].ToString(),
//                        CreatedOn = dteCreatedOn,
//                        LastModified = dteLastModified
//                    });
//                }

//                // delete the records in localDB that are supposed to be deleted
//                if (clsLocalDB.CountOfRecords(strTableName) > clsMasterDB.CountOfRecords(strTableName))
//                {
//                    stRetvalue = clsMasterDB.getAllIDs(strTableName, "ConfigName");
//                    clsLocalDB.Delete(strTableName, "ConfigName", stRetvalue);
//                }
//                #endregion

//                #region sysTerminalKey
//                strTableName = "sysTerminalKey";
//                dtMaster = clsMasterDB.ListAsDataTable(strTableName, dteStartSyncDateTime, dteEndSyncDateTime);
//                dtLocal = clsLocalDB.ListAsDataTable(strTableName, dteStartSyncDateTime, dteEndSyncDateTime);
//                SysTerminalKey clsSysTerminalKey = new SysTerminalKey(clsLocalConnection.Connection, clsLocalConnection.Transaction);

//                // update all records that are changed [inserted/updated]
//                foreach (DataRow dr in dtMaster.Rows)
//                {
//                    //DataRow[] drx = dtLocal.Select("Configname = '" + dr["ConfigName"].ToString() + "' ");
//                    if (!DateTime.TryParse(dr["CreatedOn"].ToString(), out dteCreatedOn)) dteCreatedOn = Constants.C_DATE_MIN_VALUE;
//                    if (!DateTime.TryParse(dr["LastModified"].ToString(), out dteLastModified)) dteLastModified = Constants.C_DATE_MIN_VALUE;

//                    clsSysTerminalKey.Save(new SysTerminalKeyDetails
//                    {
//                        HDSerialNo = dr["HDSerialNo"].ToString(),
//                        RegistrationKey = dr["RegistrationKey"].ToString(),
//                        CreatedOn = dteCreatedOn,
//                        LastModified = dteLastModified
//                    });
//                }

//                // delete the records in localDB that are supposed to be deleted
//                if (clsLocalDB.CountOfRecords(strTableName) > clsMasterDB.CountOfRecords(strTableName))
//                {
//                    stRetvalue = clsMasterDB.getAllIDs(strTableName, "HDSerialNo");
//                    clsLocalDB.Delete(strTableName, "HDSerialNo", stRetvalue);
//                }
//                #endregion

//                #region sysAccessTypes
//                strTableName = "sysAccessTypes";
//                dtMaster = clsMasterDB.ListAsDataTable(strTableName, dteStartSyncDateTime, dteEndSyncDateTime);
//                dtLocal = clsLocalDB.ListAsDataTable(strTableName, dteStartSyncDateTime, dteEndSyncDateTime);
//                AccessType clsAccessType = new AccessType(clsLocalConnection.Connection, clsLocalConnection.Transaction);

//                // update all records that are changed [inserted/updated]
//                foreach (DataRow dr in dtMaster.Rows)
//                {
//                    //DataRow[] drx = dtLocal.Select("Configname = '" + dr["ConfigName"].ToString() + "' ");
//                    if (!DateTime.TryParse(dr["CreatedOn"].ToString(), out dteCreatedOn)) dteCreatedOn = Constants.C_DATE_MIN_VALUE;
//                    if (!DateTime.TryParse(dr["LastModified"].ToString(), out dteLastModified)) dteLastModified = Constants.C_DATE_MIN_VALUE;

//                    clsAccessType.Save(new AccessTypeDetails
//                    {
//                        TypeID = Int32.Parse(dr["TypeID"].ToString()),
//                        TypeName = dr["TypeName"].ToString(),
//                        Remarks = dr["Remarks"].ToString(),
//                        Enabled = bool.Parse(dr["Enabled"].ToString()),
//                        SequenceNo = Int32.Parse(dr["SequenceNo"].ToString()),
//                        Category = dr["Category"].ToString(),
//                        CreatedOn = dteCreatedOn,
//                        LastModified = dteLastModified
//                    });
//                }

//                // delete the records in localDB that are supposed to be deleted
//                if (clsLocalDB.CountOfRecords(strTableName) > clsMasterDB.CountOfRecords(strTableName))
//                {
//                    stRetvalue = clsMasterDB.getAllIDs(strTableName, "TypeID", true);
//                    clsLocalDB.Delete(strTableName, "TypeID", stRetvalue);
//                }
//                #endregion

//                #region SysAccessGroups
//                strTableName = "SysAccessGroups";
//                dtMaster = clsMasterDB.ListAsDataTable(strTableName, dteStartSyncDateTime, dteEndSyncDateTime);
//                dtLocal = clsLocalDB.ListAsDataTable(strTableName, dteStartSyncDateTime, dteEndSyncDateTime);
//                AccessGroup clsAccessGroup = new AccessGroup(clsLocalConnection.Connection, clsLocalConnection.Transaction);

//                // update all records that are changed [inserted/updated]
//                foreach (DataRow dr in dtMaster.Rows)
//                {
//                    //DataRow[] drx = dtLocal.Select("Configname = '" + dr["ConfigName"].ToString() + "' ");
//                    if (!DateTime.TryParse(dr["CreatedOn"].ToString(), out dteCreatedOn)) dteCreatedOn = Constants.C_DATE_MIN_VALUE;
//                    if (!DateTime.TryParse(dr["LastModified"].ToString(), out dteLastModified)) dteLastModified = Constants.C_DATE_MIN_VALUE;

//                    clsAccessGroup.Save(new AccessGroupDetails
//                    {
//                        GroupID = Int32.Parse(dr["GroupID"].ToString()),
//                        GroupName = dr["GroupName"].ToString(),
//                        Remarks = dr["Remarks"].ToString(),
//                        CreatedOn = dteCreatedOn,
//                        LastModified = dteLastModified
//                    });
//                }

//                // delete the records in localDB that are supposed to be deleted
//                if (clsLocalDB.CountOfRecords(strTableName) > clsMasterDB.CountOfRecords(strTableName))
//                {
//                    stRetvalue = clsMasterDB.getAllIDs(strTableName, "GroupID", true);
//                    clsLocalDB.Delete(strTableName, "GroupID", stRetvalue);
//                }
//                #endregion

//                #region SysAccessGroupRights
//                strTableName = "SysAccessGroupRights";
//                dtMaster = clsMasterDB.ListAsDataTable(strTableName, dteStartSyncDateTime, dteEndSyncDateTime);
//                dtLocal = clsLocalDB.ListAsDataTable(strTableName, dteStartSyncDateTime, dteEndSyncDateTime);
//                AccessGroupRights clsAccessGroupRights = new AccessGroupRights(clsLocalConnection.Connection, clsLocalConnection.Transaction);

//                // update all records that are changed [inserted/updated]
//                foreach (DataRow dr in dtMaster.Rows)
//                {
//                    //DataRow[] drx = dtLocal.Select("Configname = '" + dr["ConfigName"].ToString() + "' ");
//                    if (!DateTime.TryParse(dr["CreatedOn"].ToString(), out dteCreatedOn)) dteCreatedOn = Constants.C_DATE_MIN_VALUE;
//                    if (!DateTime.TryParse(dr["LastModified"].ToString(), out dteLastModified)) dteLastModified = Constants.C_DATE_MIN_VALUE;

//                    clsAccessGroupRights.Save(new AccessGroupRightsDetails
//                    {
//                        GroupID = Int32.Parse(dr["GroupID"].ToString()),
//                        TranTypeID = Int32.Parse(dr["TranTypeID"].ToString()),
//                        Read = bool.Parse(dr["AllowRead"].ToString()),
//                        Write = bool.Parse(dr["AllowWrite"].ToString()),
//                        CreatedOn = dteCreatedOn,
//                        LastModified = dteLastModified
//                    });
//                }

//                //do not delete
//                //// delete the records in localDB that are supposed to be deleted
//                //if (clsLocalDB.CountOfRecords(strTableName) > clsMasterDB.CountOfRecords(strTableName))
//                //{
//                //    stRetvalue = clsMasterDB.getAllIDs(strTableName, "GroupID", true);
//                //    clsLocalDB.Delete(strTableName, "GroupID", stRetvalue);
//                //}
//                #endregion

//                #region SysAccessUsers
//                strTableName = "SysAccessUsers";
//                dtMaster = clsMasterDB.ListAsDataTable(strTableName, dteStartSyncDateTime, dteEndSyncDateTime);
//                dtLocal = clsLocalDB.ListAsDataTable(strTableName, dteStartSyncDateTime, dteEndSyncDateTime);
//                AccessUser clsAccessUser = new AccessUser(clsLocalConnection.Connection, clsLocalConnection.Transaction);

//                // update all records that are changed [inserted/updated]
//                foreach (DataRow dr in dtMaster.Rows)
//                {
//                    //DataRow[] drx = dtLocal.Select("Configname = '" + dr["ConfigName"].ToString() + "' ");
//                    if (!DateTime.TryParse(dr["CreatedOn"].ToString(), out dteCreatedOn)) dteCreatedOn = Constants.C_DATE_MIN_VALUE;
//                    if (!DateTime.TryParse(dr["LastModified"].ToString(), out dteLastModified)) dteLastModified = Constants.C_DATE_MIN_VALUE;

//                    DateTime dteDateCreated = Constants.C_DATE_MIN_VALUE;
//                    if (!DateTime.TryParse(dr["DateCreated"].ToString(), out dteDateCreated)) dteDateCreated = DateTime.MinValue;

//                    clsAccessUser.Save(new AccessUserDetails
//                    {
//                        UID = Int64.Parse(dr["UID"].ToString()),
//                        UserName = dr["UserName"].ToString(),
//                        Password = dr["Password"].ToString(),
//                        DateCreated = dteDateCreated,
//                        Deleted = bool.Parse(dr["Deleted"].ToString()),
//                        CreatedOn = dteCreatedOn,
//                        LastModified = dteLastModified
//                    });
//                }

//                // delete the records in localDB that are supposed to be deleted
//                if (clsLocalDB.CountOfRecords(strTableName) > clsMasterDB.CountOfRecords(strTableName))
//                {
//                    stRetvalue = clsMasterDB.getAllIDs(strTableName, "UID", true);
//                    clsLocalDB.Delete(strTableName, "UID", stRetvalue);
//                }
//                #endregion

//                #region SysAccessUserDetails
//                strTableName = "SysAccessUserDetails";
//                dtMaster = clsMasterDB.ListAsDataTable(strTableName, dteStartSyncDateTime, dteEndSyncDateTime);
//                dtLocal = clsLocalDB.ListAsDataTable(strTableName, dteStartSyncDateTime, dteEndSyncDateTime);
//                clsAccessUser = new AccessUser(clsLocalConnection.Connection, clsLocalConnection.Transaction);

//                // update all records that are changed [inserted/updated]
//                foreach (DataRow dr in dtMaster.Rows)
//                {
//                    //DataRow[] drx = dtLocal.Select("Configname = '" + dr["ConfigName"].ToString() + "' ");
//                    if (!DateTime.TryParse(dr["CreatedOn"].ToString(), out dteCreatedOn)) dteCreatedOn = Constants.C_DATE_MIN_VALUE;
//                    if (!DateTime.TryParse(dr["LastModified"].ToString(), out dteLastModified)) dteLastModified = Constants.C_DATE_MIN_VALUE;

//                    clsAccessUser.SaveDetails(new AccessUserDetails
//                    {
//                        UID = Int64.Parse(dr["UID"].ToString()),
//                        Name = dr["Name"].ToString(),
//                        Address1 = dr["Address1"].ToString(),
//                        Address2 = dr["Address2"].ToString(),
//                        City = dr["City"].ToString(),
//                        State = dr["State"].ToString(),
//                        Zip = dr["Zip"].ToString(),
//                        CountryID = Int32.Parse(dr["CountryID"].ToString()),
//                        OfficePhone = dr["OfficePhone"].ToString(),
//                        DirectPhone = dr["DirectPhone"].ToString(),
//                        HomePhone = dr["HomePhone"].ToString(),
//                        FaxPhone = dr["FaxPhone"].ToString(),
//                        MobilePhone = dr["MobilePhone"].ToString(),
//                        EmailAddress = dr["EmailAddress"].ToString(),
//                        GroupID = Int32.Parse(dr["GroupID"].ToString()),
//                        PageSize = Int32.Parse(dr["PageSize"].ToString()),
//                        CreatedOn = dteCreatedOn,
//                        LastModified = dteLastModified
//                    });
//                }

//                // delete the records in localDB that are supposed to be deleted
//                if (clsLocalDB.CountOfRecords(strTableName) > clsMasterDB.CountOfRecords(strTableName))
//                {
//                    stRetvalue = clsMasterDB.getAllIDs(strTableName, "UID", true);
//                    clsLocalDB.Delete(strTableName, "UID", stRetvalue);
//                }
//                #endregion

//                #region SysAccessRights
//                strTableName = "SysAccessRights";
//                dtMaster = clsMasterDB.ListAsDataTable(strTableName, dteStartSyncDateTime, dteEndSyncDateTime);
//                dtLocal = clsLocalDB.ListAsDataTable(strTableName, dteStartSyncDateTime, dteEndSyncDateTime);
//                AccessRights clsAccessRights = new AccessRights(clsLocalConnection.Connection, clsLocalConnection.Transaction);

//                // update all records that are changed [inserted/updated]
//                foreach (DataRow dr in dtMaster.Rows)
//                {
//                    //DataRow[] drx = dtLocal.Select("Configname = '" + dr["ConfigName"].ToString() + "' ");
//                    if (!DateTime.TryParse(dr["CreatedOn"].ToString(), out dteCreatedOn)) dteCreatedOn = Constants.C_DATE_MIN_VALUE;
//                    if (!DateTime.TryParse(dr["LastModified"].ToString(), out dteLastModified)) dteLastModified = Constants.C_DATE_MIN_VALUE;

//                    clsAccessRights.Save(new AccessRightsDetails
//                    {
//                        UID = Int64.Parse(dr["UID"].ToString()),
//                        TranTypeID = Int16.Parse(dr["TranTypeID"].ToString()),
//                        Read = bool.Parse(dr["AllowRead"].ToString()),
//                        Write = bool.Parse(dr["AllowWrite"].ToString()),
//                        CreatedOn = dteCreatedOn,
//                        LastModified = dteLastModified
//                    });
//                }

//                // delete the records in localDB that are supposed to be deleted
//                if (clsLocalDB.CountOfRecords(strTableName) > clsMasterDB.CountOfRecords(strTableName))
//                {
//                    stRetvalue = clsMasterDB.getAllIDs(strTableName, "UID", true);
//                    clsLocalDB.Delete(strTableName, "UID", stRetvalue);
//                }
//                #endregion

//                #region tblCaldate
//                strTableName = "tblCaldate";
//                dtMaster = clsMasterDB.ListAsDataTable(strTableName, dteStartSyncDateTime, dteEndSyncDateTime);
//                dtLocal = clsLocalDB.ListAsDataTable(strTableName, dteStartSyncDateTime, dteEndSyncDateTime);
//                CalDate clsCalDate = new CalDate(clsLocalConnection.Connection, clsLocalConnection.Transaction);

//                // update all records that are changed [inserted/updated]
//                foreach (DataRow dr in dtMaster.Rows)
//                {
//                    //DataRow[] drx = dtLocal.Select("Configname = '" + dr["ConfigName"].ToString() + "' ");
//                    if (!DateTime.TryParse(dr["CreatedOn"].ToString(), out dteCreatedOn)) dteCreatedOn = Constants.C_DATE_MIN_VALUE;
//                    if (!DateTime.TryParse(dr["LastModified"].ToString(), out dteLastModified)) dteLastModified = Constants.C_DATE_MIN_VALUE;

//                    clsCalDate.Save(new CalDateDetails
//                    {
//                        CalDateID = Int64.Parse(dr["CalDateID"].ToString()),
//                        CalDate = DateTime.Parse(dr["CalDate"].ToString()),
//                        CreatedOn = dteCreatedOn,
//                        LastModified = dteLastModified
//                    });
//                }

//                // delete the records in localDB that are supposed to be deleted
//                if (clsLocalDB.CountOfRecords(strTableName) > clsMasterDB.CountOfRecords(strTableName))
//                {
//                    stRetvalue = clsMasterDB.getAllIDs(strTableName, "CalDateID", true);
//                    clsLocalDB.Delete(strTableName, "CalDateID", stRetvalue);
//                }
//                #endregion

//                #region tblERPConfig
//                strTableName = "tblERPConfig";
//                dtMaster = clsMasterDB.ListAsDataTable(strTableName, dteStartSyncDateTime, dteEndSyncDateTime);
//                dtLocal = clsLocalDB.ListAsDataTable(strTableName, dteStartSyncDateTime, dteEndSyncDateTime);
//                ERPConfig clsERPConfig = new ERPConfig(clsLocalConnection.Connection, clsLocalConnection.Transaction);

//                // update all records that are changed [inserted/updated]
//                foreach (DataRow dr in dtMaster.Rows)
//                {
//                    //DataRow[] drx = dtLocal.Select("Configname = '" + dr["ConfigName"].ToString() + "' ");
//                    if (!DateTime.TryParse(dr["CreatedOn"].ToString(), out dteCreatedOn)) dteCreatedOn = Constants.C_DATE_MIN_VALUE;
//                    if (!DateTime.TryParse(dr["LastModified"].ToString(), out dteLastModified)) dteLastModified = Constants.C_DATE_MIN_VALUE;

//                    clsERPConfig.Save(new ERPConfigDetails
//                    {
//                        ERPConfigID = Int64.Parse(dr["ERPConfigID"].ToString()),
//                        LastPONo = dr["LastPONo"].ToString(),
//                        LastPOReturnNo = dr["LastPOReturnNo"].ToString(),
//                        LastDebitMemoNo = dr["LastDebitMemoNo"].ToString(),
//                        LastSONo = dr["LastSONo"].ToString(),
//                        LastSOReturnNo = dr["LastSOReturnNo"].ToString(),
//                        LastCreditMemoNo = dr["LastCreditMemoNo"].ToString(),
//                        LastTransferInNo = dr["LastTransferInNo"].ToString(),
//                        LastTransferOutNo = dr["LastTransferOutNo"].ToString(),
//                        LastInvAdjustmentNo = dr["LastInvAdjustmentNo"].ToString(),
//                        LastClosingNo = dr["LastClosingNo"].ToString(),
//                        PostingDateFrom = DateTime.Parse(dr["PostingDateFrom"].ToString()),
//                        PostingDateTo = DateTime.Parse(dr["PostingDateTo"].ToString()),
//                        APLinkConfigDetails = new APLinkConfigDetails
//                        {
//                            ChartOfAccountIDAPTracking = Int32.Parse(dr["ChartOfAccountIDAPTracking"].ToString()),
//                            ChartOfAccountIDAPBills = Int32.Parse(dr["ChartOfAccountIDAPBills"].ToString()),
//                            ChartOfAccountIDAPFreight = Int32.Parse(dr["ChartOfAccountIDAPFreight"].ToString()),
//                            ChartOfAccountIDAPVDeposit = Int32.Parse(dr["ChartOfAccountIDAPVDeposit"].ToString()),
//                            ChartOfAccountIDAPContra = Int32.Parse(dr["ChartOfAccountIDAPContra"].ToString()),
//                            ChartOfAccountIDAPLatePayment = Int32.Parse(dr["ChartOfAccountIDAPLatePayment"].ToString())
//                        },
//                        ARLinkConfigDetails = new ARLinkConfigDetails
//                        {
//                            ChartOfAccountIDARTracking = Int32.Parse(dr["ChartOfAccountIDARTracking"].ToString()),
//                            ChartOfAccountIDARBills = Int32.Parse(dr["ChartOfAccountIDARBills"].ToString()),
//                            ChartOfAccountIDARFreight = Int32.Parse(dr["ChartOfAccountIDARFreight"].ToString()),
//                            ChartOfAccountIDARVDeposit = Int32.Parse(dr["ChartOfAccountIDARVDeposit"].ToString()),
//                            ChartOfAccountIDARContra = Int32.Parse(dr["ChartOfAccountIDARContra"].ToString()),
//                            ChartOfAccountIDARLatePayment = Int32.Parse(dr["ChartOfAccountIDARLatePayment"].ToString())
//                        },
//                        LastCreditCardNo = dr["LastCreditCardNo"].ToString(),
//                        LastRewardCardNo = dr["LastRewardCardNo"].ToString(),
//                        DBVersion = dr["DBVersion"].ToString(),
//                        DBVersionSales = dr["DBVersionSales"].ToString(),
//                        LastBranchTransferNo = dr["LastBranchTransferNo"].ToString(),
//                        LastCustomerCode = dr["LastCustomerCode"].ToString(),
//                        CreatedOn = dteCreatedOn,
//                        LastModified = dteLastModified
//                    });
//                }

//                // delete the records in localDB that are supposed to be deleted
//                if (clsLocalDB.CountOfRecords(strTableName) > clsMasterDB.CountOfRecords(strTableName))
//                {
//                    stRetvalue = clsMasterDB.getAllIDs(strTableName, "ERPConfigID", true);
//                    clsLocalDB.Delete(strTableName, "ERPConfigID", stRetvalue);
//                }
//                #endregion

//                #region tblCountry
//                strTableName = "tblCountry";
//                dtMaster = clsMasterDB.ListAsDataTable(strTableName, dteStartSyncDateTime, dteEndSyncDateTime);
//                dtLocal = clsLocalDB.ListAsDataTable(strTableName, dteStartSyncDateTime, dteEndSyncDateTime);
//                Country clsCountry = new Country(clsLocalConnection.Connection, clsLocalConnection.Transaction);

//                // update all records that are changed [inserted/updated]
//                foreach (DataRow dr in dtMaster.Rows)
//                {
//                    //DataRow[] drx = dtLocal.Select("Configname = '" + dr["ConfigName"].ToString() + "' ");
//                    if (!DateTime.TryParse(dr["CreatedOn"].ToString(), out dteCreatedOn)) dteCreatedOn = Constants.C_DATE_MIN_VALUE;
//                    if (!DateTime.TryParse(dr["LastModified"].ToString(), out dteLastModified)) dteLastModified = Constants.C_DATE_MIN_VALUE;

//                    clsCountry.Save(new CountryDetails
//                    {
//                        CountryID = Int32.Parse(dr["CountryID"].ToString()),
//                        CountryName = dr["CountryName"].ToString(),
//                        CreatedOn = dteCreatedOn,
//                        LastModified = dteLastModified
//                    });
//                }

//                // delete the records in localDB that are supposed to be deleted
//                if (clsLocalDB.CountOfRecords(strTableName) > clsMasterDB.CountOfRecords(strTableName))
//                {
//                    stRetvalue = clsMasterDB.getAllIDs(strTableName, "CountryID", true);
//                    clsLocalDB.Delete(strTableName, "CountryID", stRetvalue);
//                }
//                #endregion

//                #region tblBranch
//                strTableName = "tblBranch";
//                dtMaster = clsMasterDB.ListAsDataTable(strTableName, dteStartSyncDateTime, dteEndSyncDateTime);
//                dtLocal = clsLocalDB.ListAsDataTable(strTableName, dteStartSyncDateTime, dteEndSyncDateTime);
//                Branch clsBranch = new Branch(clsLocalConnection.Connection, clsLocalConnection.Transaction);

//                // update all records that are changed [inserted/updated]
//                foreach (DataRow dr in dtMaster.Rows)
//                {
//                    //DataRow[] drx = dtLocal.Select("Configname = '" + dr["ConfigName"].ToString() + "' ");
//                    if (!DateTime.TryParse(dr["CreatedOn"].ToString(), out dteCreatedOn)) dteCreatedOn = Constants.C_DATE_MIN_VALUE;
//                    if (!DateTime.TryParse(dr["LastModified"].ToString(), out dteLastModified)) dteLastModified = Constants.C_DATE_MIN_VALUE;

//                    clsBranch.Save(new BranchDetails
//                    {
//                        BranchID = Int32.Parse(dr["BranchID"].ToString()),
//                        BranchCode = dr["BranchCode"].ToString(),
//                        BranchName = dr["BranchName"].ToString(),
//                        DBIP = dr["DBIP"].ToString(),
//                        DBPort = dr["DBPort"].ToString(),
//                        Address = dr["Address"].ToString(),
//                        Remarks = dr["Remarks"].ToString(),
//                        CreatedOn = dteCreatedOn,
//                        LastModified = dteLastModified
//                    });
//                }

//                // delete the records in localDB that are supposed to be deleted
//                if (clsLocalDB.CountOfRecords(strTableName) > clsMasterDB.CountOfRecords(strTableName))
//                {
//                    stRetvalue = clsMasterDB.getAllIDs(strTableName, "BranchID", true);
//                    clsLocalDB.Delete(strTableName, "BranchID", stRetvalue);
//                }
//                #endregion

//                #region tblDepartments
//                strTableName = "tblDepartments";
//                dtMaster = clsMasterDB.ListAsDataTable(strTableName, dteStartSyncDateTime, dteEndSyncDateTime);
//                dtLocal = clsLocalDB.ListAsDataTable(strTableName, dteStartSyncDateTime, dteEndSyncDateTime);
//                Department clsDepartment = new Department(clsLocalConnection.Connection, clsLocalConnection.Transaction);

//                // update all records that are changed [inserted/updated]
//                foreach (DataRow dr in dtMaster.Rows)
//                {
//                    //DataRow[] drx = dtLocal.Select("Configname = '" + dr["ConfigName"].ToString() + "' ");
//                    if (!DateTime.TryParse(dr["CreatedOn"].ToString(), out dteCreatedOn)) dteCreatedOn = Constants.C_DATE_MIN_VALUE;
//                    if (!DateTime.TryParse(dr["LastModified"].ToString(), out dteLastModified)) dteLastModified = Constants.C_DATE_MIN_VALUE;

//                    clsDepartment.Save(new DepartmentDetails
//                    {
//                        DepartmentID = Int16.Parse(dr["DepartmentID"].ToString()),
//                        DepartmentCode = dr["DepartmentCode"].ToString(),
//                        DepartmentName = dr["DepartmentName"].ToString(),
//                        CreatedOn = dteCreatedOn,
//                        LastModified = dteLastModified
//                    });
//                }

//                // delete the records in localDB that are supposed to be deleted
//                if (clsLocalDB.CountOfRecords(strTableName) > clsMasterDB.CountOfRecords(strTableName))
//                {
//                    stRetvalue = clsMasterDB.getAllIDs(strTableName, "DepartmentID", true);
//                    clsLocalDB.Delete(strTableName, "DepartmentID", stRetvalue);
//                }
//                #endregion

//                #region tblSalutations
//                strTableName = "tblSalutations";
//                dtMaster = clsMasterDB.ListAsDataTable(strTableName, dteStartSyncDateTime, dteEndSyncDateTime);
//                dtLocal = clsLocalDB.ListAsDataTable(strTableName, dteStartSyncDateTime, dteEndSyncDateTime);
//                Salutation clsSalutation = new Salutation(clsLocalConnection.Connection, clsLocalConnection.Transaction);

//                // update all records that are changed [inserted/updated]
//                foreach (DataRow dr in dtMaster.Rows)
//                {
//                    //DataRow[] drx = dtLocal.Select("Configname = '" + dr["ConfigName"].ToString() + "' ");
//                    if (!DateTime.TryParse(dr["CreatedOn"].ToString(), out dteCreatedOn)) dteCreatedOn = Constants.C_DATE_MIN_VALUE;
//                    if (!DateTime.TryParse(dr["LastModified"].ToString(), out dteLastModified)) dteLastModified = Constants.C_DATE_MIN_VALUE;

//                    clsSalutation.Save(new SalutationDetails
//                    {
//                        SalutationID = Int32.Parse(dr["SalutationID"].ToString()),
//                        SalutationCode = dr["SalutationCode"].ToString(),
//                        SalutationName = dr["SalutationName"].ToString(),
//                        CreatedOn = dteCreatedOn,
//                        LastModified = dteLastModified
//                    });
//                }

//                // delete the records in localDB that are supposed to be deleted
//                if (clsLocalDB.CountOfRecords(strTableName) > clsMasterDB.CountOfRecords(strTableName))
//                {
//                    stRetvalue = clsMasterDB.getAllIDs(strTableName, "SalutationID", true);
//                    clsLocalDB.Delete(strTableName, "SalutationID", stRetvalue);
//                }
//                #endregion

//                #region tblDenomination
//                strTableName = "tblDenomination";
//                dtMaster = clsMasterDB.ListAsDataTable(strTableName, dteStartSyncDateTime, dteEndSyncDateTime);
//                dtLocal = clsLocalDB.ListAsDataTable(strTableName, dteStartSyncDateTime, dteEndSyncDateTime);
//                Denominations clsDenominations = new Denominations(clsLocalConnection.Connection, clsLocalConnection.Transaction);

//                // update all records that are changed [inserted/updated]
//                foreach (DataRow dr in dtMaster.Rows)
//                {
//                    //DataRow[] drx = dtLocal.Select("Configname = '" + dr["ConfigName"].ToString() + "' ");
//                    if (!DateTime.TryParse(dr["CreatedOn"].ToString(), out dteCreatedOn)) dteCreatedOn = Constants.C_DATE_MIN_VALUE;
//                    if (!DateTime.TryParse(dr["LastModified"].ToString(), out dteLastModified)) dteLastModified = Constants.C_DATE_MIN_VALUE;

//                    clsDenominations.Save(new DenominationDetails
//                    {
//                        DenominationID = Int32.Parse(dr["DenominationID"].ToString()),
//                        DenominationCode = dr["DenominationCode"].ToString(),
//                        DenominationValue = decimal.Parse(dr["DenominationValue"].ToString()),
//                        ImagePath = dr["ImagePath"].ToString(),
//                        CreatedOn = dteCreatedOn,
//                        LastModified = dteLastModified
//                    });
//                }

//                // delete the records in localDB that are supposed to be deleted
//                if (clsLocalDB.CountOfRecords(strTableName) > clsMasterDB.CountOfRecords(strTableName))
//                {
//                    stRetvalue = clsMasterDB.getAllIDs(strTableName, "DenominationID", true);
//                    clsLocalDB.Delete(strTableName, "DenominationID", stRetvalue);
//                }
//                #endregion

//                #region tblCardTypes
//                strTableName = "tblCardTypes";
//                dtMaster = clsMasterDB.ListAsDataTable(strTableName, dteStartSyncDateTime, dteEndSyncDateTime);
//                dtLocal = clsLocalDB.ListAsDataTable(strTableName, dteStartSyncDateTime, dteEndSyncDateTime);
//                CardType clsCardType = new CardType(clsLocalConnection.Connection, clsLocalConnection.Transaction);

//                // update all records that are changed [inserted/updated]
//                foreach (DataRow dr in dtMaster.Rows)
//                {
//                    //DataRow[] drx = dtLocal.Select("Configname = '" + dr["ConfigName"].ToString() + "' ");
//                    if (!DateTime.TryParse(dr["CreatedOn"].ToString(), out dteCreatedOn)) dteCreatedOn = Constants.C_DATE_MIN_VALUE;
//                    if (!DateTime.TryParse(dr["LastModified"].ToString(), out dteLastModified)) dteLastModified = Constants.C_DATE_MIN_VALUE;

//                    clsCardType.Save(new CardTypeDetails
//                    {
//                        CardTypeID = Int16.Parse(dr["CardTypeID"].ToString()),
//                        CardTypeCode = dr["CardTypeCode"].ToString(),
//                        CardTypeName = dr["CardTypeName"].ToString(),
//                        CreditFinanceCharge = decimal.Parse(dr["CreditFinanceCharge"].ToString()),
//                        CreditLatePenaltyCharge = decimal.Parse(dr["CreditLatePenaltyCharge"].ToString()),
//                        CreditMinimumAmountDue = decimal.Parse(dr["CreditMinimumAmountDue"].ToString()),
//                        CreditMinimumPercentageDue = decimal.Parse(dr["CreditMinimumPercentageDue"].ToString()),
//                        CreditFinanceCharge15th = decimal.Parse(dr["CreditFinanceCharge15th"].ToString()),
//                        CreditLatePenaltyCharge15th = decimal.Parse(dr["CreditLatePenaltyCharge15th"].ToString()),
//                        CreditMinimumAmountDue15th = decimal.Parse(dr["CreditMinimumAmountDue15th"].ToString()),
//                        CreditMinimumPercentageDue15th = decimal.Parse(dr["CreditMinimumPercentageDue15th"].ToString()),
//                        CreditCardType = (CreditCardTypes)Enum.Parse(typeof(CreditCardTypes), dr["CreditCardType"].ToString()),
//                        WithGuarantor = bool.Parse(dr["WithGuarantor"].ToString()),
//                        BIRPermitNo = dr["BIRPermitNo"].ToString(),
//                        CreatedOn = dteCreatedOn,
//                        LastModified = dteLastModified
//                    });
//                }

//                // delete the records in localDB that are supposed to be deleted
//                if (clsLocalDB.CountOfRecords(strTableName) > clsMasterDB.CountOfRecords(strTableName))
//                {
//                    stRetvalue = clsMasterDB.getAllIDs(strTableName, "CardTypeID", true);
//                    clsLocalDB.Delete(strTableName, "CardTypeID", stRetvalue);
//                }
//                #endregion

//                #region tblChargeType
//                strTableName = "tblChargeType";
//                dtMaster = clsMasterDB.ListAsDataTable(strTableName, dteStartSyncDateTime, dteEndSyncDateTime);
//                dtLocal = clsLocalDB.ListAsDataTable(strTableName, dteStartSyncDateTime, dteEndSyncDateTime);
//                ChargeType clsChargeType = new ChargeType(clsLocalConnection.Connection, clsLocalConnection.Transaction);

//                // update all records that are changed [inserted/updated]
//                foreach (DataRow dr in dtMaster.Rows)
//                {
//                    //DataRow[] drx = dtLocal.Select("Configname = '" + dr["ConfigName"].ToString() + "' ");
//                    if (!DateTime.TryParse(dr["CreatedOn"].ToString(), out dteCreatedOn)) dteCreatedOn = Constants.C_DATE_MIN_VALUE;
//                    if (!DateTime.TryParse(dr["LastModified"].ToString(), out dteLastModified)) dteLastModified = Constants.C_DATE_MIN_VALUE;

//                    clsChargeType.Save(new ChargeTypeDetails
//                    {
//                        ChargeTypeID = Int32.Parse(dr["ChargeTypeID"].ToString()),
//                        ChargeTypeCode = dr["ChargeTypeCode"].ToString(),
//                        ChargeType = dr["ChargeType"].ToString(),
//                        ChargeAmount = decimal.Parse(dr["ChargeAmount"].ToString()),
//                        InPercent = bool.Parse(dr["InPercent"].ToString()),
//                        CreatedOn = dteCreatedOn,
//                        LastModified = dteLastModified
//                    });
//                }

//                // delete the records in localDB that are supposed to be deleted
//                if (clsLocalDB.CountOfRecords(strTableName) > clsMasterDB.CountOfRecords(strTableName))
//                {
//                    stRetvalue = clsMasterDB.getAllIDs(strTableName, "ChargeTypeID", true);
//                    clsLocalDB.Delete(strTableName, "ChargeTypeID", stRetvalue);
//                }
//                #endregion

//                #region tblDiscount
//                strTableName = "tblDiscount";
//                dtMaster = clsMasterDB.ListAsDataTable(strTableName, dteStartSyncDateTime, dteEndSyncDateTime);
//                dtLocal = clsLocalDB.ListAsDataTable(strTableName, dteStartSyncDateTime, dteEndSyncDateTime);
//                Discount clsDiscount = new Discount(clsLocalConnection.Connection, clsLocalConnection.Transaction);

//                // update all records that are changed [inserted/updated]
//                foreach (DataRow dr in dtMaster.Rows)
//                {
//                    //DataRow[] drx = dtLocal.Select("Configname = '" + dr["ConfigName"].ToString() + "' ");
//                    if (!DateTime.TryParse(dr["CreatedOn"].ToString(), out dteCreatedOn)) dteCreatedOn = Constants.C_DATE_MIN_VALUE;
//                    if (!DateTime.TryParse(dr["LastModified"].ToString(), out dteLastModified)) dteLastModified = Constants.C_DATE_MIN_VALUE;

//                    clsDiscount.Save(new DiscountDetails
//                    {
//                        DiscountID = Int32.Parse(dr["DiscountID"].ToString()),
//                        DiscountCode = dr["DiscountCode"].ToString(),
//                        DiscountType = dr["DiscountType"].ToString(),
//                        DiscountPrice = decimal.Parse(dr["DiscountPrice"].ToString()),
//                        InPercent = bool.Parse(dr["InPercent"].ToString()),
//                        CreatedOn = dteCreatedOn,
//                        LastModified = dteLastModified
//                    });
//                }

//                // delete the records in localDB that are supposed to be deleted
//                if (clsLocalDB.CountOfRecords(strTableName) > clsMasterDB.CountOfRecords(strTableName))
//                {
//                    stRetvalue = clsMasterDB.getAllIDs(strTableName, "DiscountID", true);
//                    clsLocalDB.Delete(strTableName, "DiscountID", stRetvalue);
//                }
//                #endregion

//                #region tblReceipt
//                strTableName = "tblReceipt";
//                dtMaster = clsMasterDB.ListAsDataTable(strTableName, dteStartSyncDateTime, dteEndSyncDateTime);
//                dtLocal = clsLocalDB.ListAsDataTable(strTableName, dteStartSyncDateTime, dteEndSyncDateTime);
//                Receipt clsReceipt = new Receipt(clsLocalConnection.Connection, clsLocalConnection.Transaction);

//                // update all records that are changed [inserted/updated]
//                foreach (DataRow dr in dtMaster.Rows)
//                {
//                    //DataRow[] drx = dtLocal.Select("Configname = '" + dr["ConfigName"].ToString() + "' ");
//                    if (!DateTime.TryParse(dr["CreatedOn"].ToString(), out dteCreatedOn)) dteCreatedOn = Constants.C_DATE_MIN_VALUE;
//                    if (!DateTime.TryParse(dr["LastModified"].ToString(), out dteLastModified)) dteLastModified = Constants.C_DATE_MIN_VALUE;

//                    clsReceipt.Save(new ReceiptDetails
//                    {
//                        ReceiptID = Int32.Parse(dr["ReceiptID"].ToString()),
//                        Module = dr["Module"].ToString(),
//                        Text = dr["Text"].ToString(),
//                        Value = dr["Value"].ToString(),
//                        Orientation = (ReportFormatOrientation)Enum.Parse(typeof(ReportFormatOrientation), dr["Orientation"].ToString()),
//                        CreatedOn = dteCreatedOn,
//                        LastModified = dteLastModified
//                    });
//                }

//                // delete the records in localDB that are supposed to be deleted
//                if (clsLocalDB.CountOfRecords(strTableName) > clsMasterDB.CountOfRecords(strTableName))
//                {
//                    stRetvalue = clsMasterDB.getAllIDs(strTableName, "ReceiptID", true);
//                    clsLocalDB.Delete(strTableName, "ReceiptID", stRetvalue);
//                }
//                #endregion

//                #region tblTerminal
//                strTableName = "tblTerminal";
//                dtMaster = clsMasterDB.ListAsDataTable(strTableName, dteStartSyncDateTime, dteEndSyncDateTime);
//                dtLocal = clsLocalDB.ListAsDataTable(strTableName, dteStartSyncDateTime, dteEndSyncDateTime);
//                Terminal clsTerminal = new Terminal(clsLocalConnection.Connection, clsLocalConnection.Transaction);

//                // update all records that are changed [inserted/updated]
//                foreach (DataRow dr in dtMaster.Rows)
//                {
//                    //DataRow[] drx = dtLocal.Select("Configname = '" + dr["ConfigName"].ToString() + "' ");
//                    if (!DateTime.TryParse(dr["CreatedOn"].ToString(), out dteCreatedOn)) dteCreatedOn = Constants.C_DATE_MIN_VALUE;
//                    if (!DateTime.TryParse(dr["LastModified"].ToString(), out dteLastModified)) dteLastModified = Constants.C_DATE_MIN_VALUE;

//                    clsTerminal.Save(new TerminalDetails
//                    {
//                        TerminalID = Int32.Parse(dr["TerminalID"].ToString()),
//                        TerminalNo = dr["TerminalNo"].ToString(),
//                        TerminalCode = dr["TerminalCode"].ToString(),
//                        TerminalName = dr["TerminalName"].ToString(),
//                        Status = (TerminalStatus)Enum.Parse(typeof(TerminalStatus), dr["Status"].ToString()),
//                        DateCreated = DateTime.Parse(dr["DateCreated"].ToString()),
//                        IsPrinterAutoCutter = bool.Parse(dr["IsPrinterAutoCutter"].ToString()),
//                        MaxReceiptWidth = Int16.Parse(dr["MaxReceiptWidth"].ToString()),
//                        TransactionNoLength = Int16.Parse(dr["TransactionNoLength"].ToString()),
//                        AutoPrint = (PrintingPreference)Enum.Parse(typeof(PrintingPreference), dr["AutoPrint"].ToString()),
//                        PrinterName = dr["PrinterName"].ToString(),
//                        TurretName = dr["TurretName"].ToString(),
//                        CashDrawerName = dr["CashDrawerName"].ToString(),
//                        MachineSerialNo = dr["MachineSerialNo"].ToString(),
//                        AccreditationNo = dr["AccreditationNo"].ToString(),
//                        ItemVoidConfirmation = bool.Parse(dr["ItemVoidConfirmation"].ToString()),
//                        EnableEVAT = bool.Parse(dr["EnableEVAT"].ToString()),
//                        FORM_Behavior = dr["FORM_Behavior"].ToString(),
//                        MarqueeMessage = dr["MarqueeMessage"].ToString(),
//                        TrustFund = decimal.Parse(dr["TrustFund"].ToString()),
//                        IsVATInclusive = bool.Parse(dr["IsVATInclusive"].ToString()),
//                        VAT = decimal.Parse(dr["VAT"].ToString()),
//                        EVAT = decimal.Parse(dr["EVAT"].ToString()),
//                        LocalTax = decimal.Parse(dr["LocalTax"].ToString()),
//                        ShowItemMoreThanZeroQty = bool.Parse(dr["ShowItemMoreThanZeroQty"].ToString()),
//                        ShowOneTerminalSuspendedTransactions = bool.Parse(dr["ShowOneTerminalSuspendedTransactions"].ToString()),
//                        ShowOnlyPackedTransactions = bool.Parse(dr["ShowOnlyPackedTransactions"].ToString()),
//                        ReceiptType = (TerminalReceiptType)Enum.Parse(typeof(TerminalReceiptType), dr["TerminalReceiptType"].ToString()),
//                        SalesInvoicePrinterName = dr["SalesInvoicePrinterName"].ToString(),
//                        CashCountBeforeReport = bool.Parse(dr["CashCountBeforeReport"].ToString()),
//                        PreviewTerminalReport = bool.Parse(dr["PreviewTerminalReport"].ToString()),
//                        OrderSlipPrinter = (OrderSlipPrinter)Enum.Parse(typeof(OrderSlipPrinter), dr["OrderSlipPrinter"].ToString()),
//                        DBVersion = dr["DBVersion"].ToString(),
//                        FEVersion = dr["FEVersion"].ToString(),
//                        BEVersion = dr["BEVersion"].ToString(),
//                        IsPrinterDotMatrix = bool.Parse(dr["IsPrinterDotmatrix"].ToString()),
//                        IsChargeEditable = bool.Parse(dr["IsChargeEditable"].ToString()),
//                        IsDiscountEditable = bool.Parse(dr["IsDiscountEditable"].ToString()),
//                        CheckCutOffTime = bool.Parse(dr["CheckCutOffTime"].ToString()),
//                        StartCutOffTime = dr["StartCutOffTime"].ToString(),
//                        EndCutOffTime = dr["EndCutOffTime"].ToString(),
//                        WithRestaurantFeatures = bool.Parse(dr["WithRestaurantFeatures"].ToString()),
//                        SeniorCitizenDiscountCode = dr["SeniorCitizenDiscountCode"].ToString(),
//                        IsTouchScreen = bool.Parse(dr["IsTouchScreen"].ToString()),
//                        WillContinueSelectionVariation = bool.Parse(dr["WillContinueSelectionVariation"].ToString()),
//                        WillContinueSelectionProduct = bool.Parse(dr["WillContinueSelectionProduct"].ToString()),
//                        RETPriceMarkUp = decimal.Parse(dr["RETPriceMarkUp"].ToString()),
//                        WSPriceMarkUp = decimal.Parse(dr["WSPriceMarkUp"].ToString()),
//                        WillPrintGrandTotal = bool.Parse(dr["WillPrintGrandTotal"].ToString()),
//                        ReservedAndCommit = bool.Parse(dr["ReservedAndCommit"].ToString()),
//                        ShowCustomerSelection = bool.Parse(dr["ShowCustomerSelection"].ToString()),
//                        AutoGenerateRewardCardNo = bool.Parse(dr["AutoGenerateRewardCardNo"].ToString()),
//                        RewardPointsDetails = new RewardPointsDetails()
//                        {
//                            EnableRewardPoints = bool.Parse(dr["EnableRewardPoints"].ToString()),
//                            RewardPointsMinimum = decimal.Parse(dr["RewardPointsMinimum"].ToString()),
//                            RewardPointsEvery = decimal.Parse(dr["RewardPointsEvery"].ToString()),
//                            RewardPoints = decimal.Parse(dr["RewardPoints"].ToString()),
//                            RoundDownRewardPoints = bool.Parse(dr["RoundDownRewardPoints"].ToString()),
//                            EnableRewardPointsAsPayment = bool.Parse(dr["EnableRewardPointsAsPayment"].ToString()),
//                            RewardPointsMaxPercentageForPayment = decimal.Parse(dr["RewardPointsMaxPercentageForPayment"].ToString()),
//                            RewardPointsPaymentValue = decimal.Parse(dr["RewardPointsPaymentValue"].ToString()),
//                            RewardPointsPaymentCashEquivalent = decimal.Parse(dr["RewardPointsPaymentCashEquivalent"].ToString()),
//                            RewardsPermitNo = dr["RewardsPermitNo"].ToString(),
//                        },

//                        IsFineDining = bool.Parse(dr["IsFineDining"].ToString()),
//                        PersonalChargeType = int.Parse(dr["PersonalChargeTypeID"].ToString()) != 0 ? clsChargeType.Details(int.Parse(dr["PersonalChargeTypeID"].ToString())) : new ChargeTypeDetails(),
//                        GroupChargeType = int.Parse(dr["GroupChargeTypeID"].ToString()) != 0 ? clsChargeType.Details(int.Parse(dr["GroupChargeTypeID"].ToString())) : new ChargeTypeDetails(),
//                        BranchID = Int32.Parse(dr["BranchID"].ToString()),
//                        ProductSearchType = (ProductSearchType)Enum.Parse(typeof(ProductSearchType), dr["ProductSearchType"].ToString()),
//                        IncludeCreditChargeAgreement = bool.Parse(dr["IncludeCreditChargeAgreement"].ToString()),
//                        IsParkingTerminal = bool.Parse(dr["IsParkingTerminal"].ToString()),
//                        WillPrintChargeSlip = bool.Parse(dr["WillPrintChargeSlip"].ToString()),
//                        IncludeTermsAndConditions = bool.Parse(dr["IncludeTermsAndConditions"].ToString()),
//                        PWDDiscountCode = dr["PWDDiscountCode"].ToString(),
//                        DefaultTransactionChargeCode = dr["DefaultTransactionChargeCode"].ToString(),
//                        DineInChargeCode = dr["DineInChargeCode"].ToString(),
//                        TakeOutChargeCode = dr["TakeOutChargeCode"].ToString(),
//                        DeliveryChargeCode = dr["DeliveryChargeCode"].ToString(),
//                        CreatedOn = dteCreatedOn,
//                        LastModified = dteLastModified
//                    });
//                }

//                // delete the records in localDB that are supposed to be deleted
//                if (clsLocalDB.CountOfRecords(strTableName) > clsMasterDB.CountOfRecords(strTableName))
//                {
//                    stRetvalue = clsMasterDB.getAllIDs(strTableName, "TerminalID", true);
//                    clsLocalDB.Delete(strTableName, "TerminalID", stRetvalue);
//                }
//                #endregion

//                #region tblStockType
//                strTableName = "tblStockType";
//                dtMaster = clsMasterDB.ListAsDataTable(strTableName, dteStartSyncDateTime, dteEndSyncDateTime);
//                dtLocal = clsLocalDB.ListAsDataTable(strTableName, dteStartSyncDateTime, dteEndSyncDateTime);
//                StockTypes clsStockTypes = new StockTypes(clsLocalConnection.Connection, clsLocalConnection.Transaction);

//                // update all records that are changed [inserted/updated]
//                foreach (DataRow dr in dtMaster.Rows)
//                {
//                    //DataRow[] drx = dtLocal.Select("Configname = '" + dr["ConfigName"].ToString() + "' ");
//                    if (!DateTime.TryParse(dr["CreatedOn"].ToString(), out dteCreatedOn)) dteCreatedOn = Constants.C_DATE_MIN_VALUE;
//                    if (!DateTime.TryParse(dr["LastModified"].ToString(), out dteLastModified)) dteLastModified = Constants.C_DATE_MIN_VALUE;

//                    clsStockTypes.Save(new StockTypesDetails
//                    {
//                        StockTypeID = Int16.Parse(dr["StockTypeID"].ToString()),
//                        StockTypeCode = dr["StockTypeCode"].ToString(),
//                        Description = dr["Description"].ToString(),
//                        StockDirection = (StockDirections)Enum.Parse(typeof(StockDirections), dr["StockDirection"].ToString()),
//                        CreatedOn = dteCreatedOn,
//                        LastModified = dteLastModified
//                    });
//                }

//                // delete the records in localDB that are supposed to be deleted
//                if (clsLocalDB.CountOfRecords(strTableName) > clsMasterDB.CountOfRecords(strTableName))
//                {
//                    stRetvalue = clsMasterDB.getAllIDs(strTableName, "StockTypeID", true);
//                    clsLocalDB.Delete(strTableName, "StockTypeID", stRetvalue);
//                }
//                #endregion

//                #region tblPositions
//                strTableName = "tblPositions";
//                dtMaster = clsMasterDB.ListAsDataTable(strTableName, dteStartSyncDateTime, dteEndSyncDateTime);
//                dtLocal = clsLocalDB.ListAsDataTable(strTableName, dteStartSyncDateTime, dteEndSyncDateTime);
//                Positions clsPositions = new Positions(clsLocalConnection.Connection, clsLocalConnection.Transaction);

//                // update all records that are changed [inserted/updated]
//                foreach (DataRow dr in dtMaster.Rows)
//                {
//                    //DataRow[] drx = dtLocal.Select("Configname = '" + dr["ConfigName"].ToString() + "' ");
//                    if (!DateTime.TryParse(dr["CreatedOn"].ToString(), out dteCreatedOn)) dteCreatedOn = Constants.C_DATE_MIN_VALUE;
//                    if (!DateTime.TryParse(dr["LastModified"].ToString(), out dteLastModified)) dteLastModified = Constants.C_DATE_MIN_VALUE;

//                    clsPositions.Save(new PositionDetails
//                    {
//                        PositionID = Int16.Parse(dr["PositionID"].ToString()),
//                        PositionCode = dr["PositionCode"].ToString(),
//                        PositionName = dr["PositionName"].ToString(),
//                        CreatedOn = dteCreatedOn,
//                        LastModified = dteLastModified
//                    });
//                }

//                // delete the records in localDB that are supposed to be deleted
//                if (clsLocalDB.CountOfRecords(strTableName) > clsMasterDB.CountOfRecords(strTableName))
//                {
//                    stRetvalue = clsMasterDB.getAllIDs(strTableName, "PositionID", true);
//                    clsLocalDB.Delete(strTableName, "PositionID", stRetvalue);
//                }
//                #endregion

//                /*****************************Contacs**************************/

//                #region tblContactGroup
//                strTableName = "tblContactGroup";
//                dtMaster = clsMasterDB.ListAsDataTable(strTableName, dteStartSyncDateTime, dteEndSyncDateTime);
//                dtLocal = clsLocalDB.ListAsDataTable(strTableName, dteStartSyncDateTime, dteEndSyncDateTime);
//                ContactGroups clsContactGroups = new ContactGroups(clsLocalConnection.Connection, clsLocalConnection.Transaction);

//                // update all records that are changed [inserted/updated]
//                foreach (DataRow dr in dtMaster.Rows)
//                {
//                    //DataRow[] drx = dtLocal.Select("Configname = '" + dr["ConfigName"].ToString() + "' ");
//                    if (!DateTime.TryParse(dr["CreatedOn"].ToString(), out dteCreatedOn)) dteCreatedOn = Constants.C_DATE_MIN_VALUE;
//                    if (!DateTime.TryParse(dr["LastModified"].ToString(), out dteLastModified)) dteLastModified = Constants.C_DATE_MIN_VALUE;

//                    clsContactGroups.Save(new ContactGroupDetails
//                    {
//                        ContactGroupID = Int16.Parse(dr["ContactGroupID"].ToString()),
//                        ContactGroupCode = dr["ContactGroupCode"].ToString(),
//                        ContactGroupName = dr["ContactGroupName"].ToString(),
//                        ContactGroupCategory = (ContactGroupCategory)Enum.Parse(typeof(ContactGroupCategory), dr["ContactGroupCategory"].ToString()),
//                        CreatedOn = dteCreatedOn,
//                        LastModified = dteLastModified
//                    });
//                }

//                // delete the records in localDB that are supposed to be deleted
//                if (clsLocalDB.CountOfRecords(strTableName) > clsMasterDB.CountOfRecords(strTableName))
//                {
//                    stRetvalue = clsMasterDB.getAllIDs(strTableName, "ContactGroupID", true);
//                    clsLocalDB.Delete(strTableName, "ContactGroupID", stRetvalue);
//                }
//                #endregion

//                #region tblContacts
//                strTableName = "tblContacts";
//                dtMaster = clsMasterDB.ListAsDataTable(strTableName, dteStartSyncDateTime, dteEndSyncDateTime);
//                dtLocal = clsLocalDB.ListAsDataTable(strTableName, dteStartSyncDateTime, dteEndSyncDateTime);
//                Contacts clsContacts = new Contacts(clsLocalConnection.Connection, clsLocalConnection.Transaction);

//                // update all records that are changed [inserted/updated]
//                foreach (DataRow dr in dtMaster.Rows)
//                {
//                    //DataRow[] drx = dtLocal.Select("Configname = '" + dr["ConfigName"].ToString() + "' ");
//                    if (!DateTime.TryParse(dr["CreatedOn"].ToString(), out dteCreatedOn)) dteCreatedOn = Constants.C_DATE_MIN_VALUE;
//                    if (!DateTime.TryParse(dr["LastModified"].ToString(), out dteLastModified)) dteLastModified = Constants.C_DATE_MIN_VALUE;

//                    clsContacts.Save(new ContactDetails
//                    {
//                        ContactID = Int64.Parse(dr["ContactID"].ToString()),
//                        ContactCode = dr["ContactCode"].ToString(),
//                        ContactName = dr["ContactName"].ToString(),
//                        ContactGroupID = Int32.Parse(dr["ContactGroupID"].ToString()),
//                        ModeOfTerms = (ModeOfTerms)Enum.Parse(typeof(ModeOfTerms), dr["ModeOfTerms"].ToString()),
//                        Terms = Int32.Parse(dr["Terms"].ToString()),
//                        Address = dr["Address"].ToString(),
//                        BusinessName = dr["BusinessName"].ToString(),
//                        TelephoneNo = dr["TelephoneNo"].ToString(),
//                        Remarks = dr["Remarks"].ToString(),
//                        Debit = decimal.Parse(dr["Debit"].ToString()),
//                        Credit = decimal.Parse(dr["Credit"].ToString()),
//                        CreditLimit = decimal.Parse(dr["CreditLimit"].ToString()),
//                        IsCreditAllowed = bool.Parse(dr["IsCreditAllowed"].ToString()),
//                        DateCreated = DateTime.Parse(dr["DateCreated"].ToString()),
//                        Deleted = bool.Parse(dr["Deleted"].ToString()),
//                        DepartmentID = Int32.Parse(dr["DepartmentID"].ToString()),
//                        PositionID = Int32.Parse(dr["PositionID"].ToString()),
//                        isLock = bool.Parse(dr["isLock"].ToString()),
//                        CreatedOn = dteCreatedOn,
//                        LastModified = dteLastModified
//                    });
//                }

//                // delete the records in localDB that are supposed to be deleted
//                if (clsLocalDB.CountOfRecords(strTableName) > clsMasterDB.CountOfRecords(strTableName))
//                {
//                    stRetvalue = clsMasterDB.getAllIDs(strTableName, "ContactID", true);
//                    clsLocalDB.Delete(strTableName, "ContactID", stRetvalue);
//                }
//                #endregion

//                #region tblContactAddOn
//                strTableName = "tblContactAddOn";
//                dtMaster = clsMasterDB.ListAsDataTable(strTableName, dteStartSyncDateTime, dteEndSyncDateTime);
//                dtLocal = clsLocalDB.ListAsDataTable(strTableName, dteStartSyncDateTime, dteEndSyncDateTime);
//                ContactAddOns clsContactAddOns = new ContactAddOns(clsLocalConnection.Connection, clsLocalConnection.Transaction);

//                // update all records that are changed [inserted/updated]
//                foreach (DataRow dr in dtMaster.Rows)
//                {
//                    //DataRow[] drx = dtLocal.Select("Configname = '" + dr["ConfigName"].ToString() + "' ");
//                    if (!DateTime.TryParse(dr["CreatedOn"].ToString(), out dteCreatedOn)) dteCreatedOn = Constants.C_DATE_MIN_VALUE;
//                    if (!DateTime.TryParse(dr["LastModified"].ToString(), out dteLastModified)) dteLastModified = Constants.C_DATE_MIN_VALUE;

//                    clsContactAddOns.Save(new ContactAddOnDetails
//                    {
//                        ContactDetailID = Int64.Parse(dr["ContactDetailID"].ToString()),
//                        ContactID = Int64.Parse(dr["ContactID"].ToString()),
//                        Salutation = dr["Salutation"].ToString(),
//                        FirstName = dr["FirstName"].ToString(),
//                        MiddleName = dr["MiddleName"].ToString(),
//                        LastName = dr["LastName"].ToString(),
//                        SpouseName = dr["SpouseName"].ToString(),
//                        BirthDate = DateTime.Parse(dr["BirthDate"].ToString()),
//                        SpouseBirthDate = DateTime.Parse(dr["SpouseBirthDate"].ToString()),
//                        AnniversaryDate = DateTime.Parse(dr["AnniversaryDate"].ToString()),
//                        Address1 = dr["Address1"].ToString(),
//                        Address2 = dr["Address2"].ToString(),
//                        City = dr["City"].ToString(),
//                        State = dr["State"].ToString(),
//                        ZipCode = dr["ZipCode"].ToString(),
//                        CountryID = Int32.Parse(dr["CountryID"].ToString()),
//                        BusinessPhoneNo = dr["BusinessphoneNo"].ToString(),
//                        HomePhoneNo = dr["HomephoneNo"].ToString(),
//                        MobileNo = dr["MobileNo"].ToString(),
//                        FaxNo = dr["FaxNo"].ToString(),
//                        EmailAddress = dr["EmailAddress"].ToString(),
//                        CreatedOn = dteCreatedOn,
//                        LastModified = dteLastModified
//                    });
//                }

//                // delete the records in localDB that are supposed to be deleted
//                if (clsLocalDB.CountOfRecords(strTableName) > clsMasterDB.CountOfRecords(strTableName))
//                {
//                    stRetvalue = clsMasterDB.getAllIDs(strTableName, "ContactDetailID", true);
//                    clsLocalDB.Delete(strTableName, "ContactDetailID", stRetvalue);
//                }
//                #endregion

//                #region tblContactCreditCardInfo
//                strTableName = "tblContactCreditCardInfo";
//                dtMaster = clsMasterDB.ListAsDataTable(strTableName, dteStartSyncDateTime, dteEndSyncDateTime);
//                dtLocal = clsLocalDB.ListAsDataTable(strTableName, dteStartSyncDateTime, dteEndSyncDateTime);
//                ContactCreditCardInfos clsContactCreditCardInfo = new ContactCreditCardInfos(clsLocalConnection.Connection, clsLocalConnection.Transaction);

//                // update all records that are changed [inserted/updated]
//                foreach (DataRow dr in dtMaster.Rows)
//                {
//                    //DataRow[] drx = dtLocal.Select("Configname = '" + dr["ConfigName"].ToString() + "' ");
//                    if (!DateTime.TryParse(dr["CreatedOn"].ToString(), out dteCreatedOn)) dteCreatedOn = Constants.C_DATE_MIN_VALUE;
//                    if (!DateTime.TryParse(dr["LastModified"].ToString(), out dteLastModified)) dteLastModified = Constants.C_DATE_MIN_VALUE;

//                    clsContactCreditCardInfo.Save(new ContactCreditCardInfoDetails
//                    {
//                        CustomerID = Int64.Parse(dr["CustomerID"].ToString()),
//                        GuarantorID = Int64.Parse(dr["GuarantorID"].ToString()),
//                        CardTypeDetails = new CardType(clsLocalDB.Connection, clsLocalDB.Transaction).Details(Int16.Parse(dr["CreditCardTypeID"].ToString())),
//                        CreditCardNo = dr["CreditCardNo"].ToString(),
//                        CreditAwardDate = DateTime.Parse(dr["CreditAwardDate"].ToString()),
//                        TotalPurchases = decimal.Parse(dr["TotalPurchases"].ToString()),
//                        CreditPaid = decimal.Parse(dr["CreditPaid"].ToString()),
//                        CreditCardStatus = (CreditCardStatus)Enum.Parse(typeof(CreditCardStatus), dr["CreditCardStatus"].ToString()),
//                        ExpiryDate = DateTime.Parse(dr["ExpiryDate"].ToString()),
//                        EmbossedCardNo = dr["EmbossedCardNo"].ToString(),
//                        LastBillingDate = DateTime.Parse(dr["LastBillingDate"].ToString()),
//                        CreditActive = bool.Parse(dr["CreditActive"].ToString()),
//                        CreatedOn = dteCreatedOn,
//                        LastModified = dteLastModified
//                    });
//                }

//                // delete the records in localDB that are supposed to be deleted
//                if (clsLocalDB.CountOfRecords(strTableName) > clsMasterDB.CountOfRecords(strTableName))
//                {
//                    stRetvalue = clsMasterDB.getAllIDs(strTableName, "CustomerID", true);
//                    clsLocalDB.Delete(strTableName, "CustomerID", stRetvalue);
//                }
//                #endregion

//                /*****************************Products Pre-requisite**************************/
//                #region tblUnit
//                strTableName = "tblUnit";
//                dtMaster = clsMasterDB.ListAsDataTable(strTableName, dteStartSyncDateTime, dteEndSyncDateTime);
//                dtLocal = clsLocalDB.ListAsDataTable(strTableName, dteStartSyncDateTime, dteEndSyncDateTime);
//                Unit clsUnit = new Unit(clsLocalConnection.Connection, clsLocalConnection.Transaction);

//                // update all records that are changed [inserted/updated]
//                foreach (DataRow dr in dtMaster.Rows)
//                {
//                    //DataRow[] drx = dtLocal.Select("Configname = '" + dr["ConfigName"].ToString() + "' ");
//                    if (!DateTime.TryParse(dr["CreatedOn"].ToString(), out dteCreatedOn)) dteCreatedOn = Constants.C_DATE_MIN_VALUE;
//                    if (!DateTime.TryParse(dr["LastModified"].ToString(), out dteLastModified)) dteLastModified = Constants.C_DATE_MIN_VALUE;

//                    clsUnit.Save(new UnitDetails
//                    {
//                        UnitID = Int32.Parse(dr["UnitID"].ToString()),
//                        UnitCode = dr["UnitCode"].ToString(),
//                        UnitName = dr["UnitName"].ToString(),
//                        CreatedOn = dteCreatedOn,
//                        LastModified = dteLastModified
//                    });
//                }

//                // delete the records in localDB that are supposed to be deleted
//                if (clsLocalDB.CountOfRecords(strTableName) > clsMasterDB.CountOfRecords(strTableName))
//                {
//                    stRetvalue = clsMasterDB.getAllIDs(strTableName, "UnitID", true);
//                    clsLocalDB.Delete(strTableName, "UnitID", stRetvalue);
//                }
//                #endregion

//                #region tblVariations
//                strTableName = "tblVariations";
//                dtMaster = clsMasterDB.ListAsDataTable(strTableName, dteStartSyncDateTime, dteEndSyncDateTime);
//                dtLocal = clsLocalDB.ListAsDataTable(strTableName, dteStartSyncDateTime, dteEndSyncDateTime);
//                Variation clsVariation = new Variation(clsLocalConnection.Connection, clsLocalConnection.Transaction);

//                // update all records that are changed [inserted/updated]
//                foreach (DataRow dr in dtMaster.Rows)
//                {
//                    //DataRow[] drx = dtLocal.Select("Configname = '" + dr["ConfigName"].ToString() + "' ");
//                    if (!DateTime.TryParse(dr["CreatedOn"].ToString(), out dteCreatedOn)) dteCreatedOn = Constants.C_DATE_MIN_VALUE;
//                    if (!DateTime.TryParse(dr["LastModified"].ToString(), out dteLastModified)) dteLastModified = Constants.C_DATE_MIN_VALUE;

//                    clsVariation.Save(new VariationDetails
//                    {
//                        VariationID = Int32.Parse(dr["VariationID"].ToString()),
//                        VariationCode = dr["VariationCode"].ToString(),
//                        VariationType = dr["VariationType"].ToString(),
//                        CreatedOn = dteCreatedOn,
//                        LastModified = dteLastModified
//                    });
//                }

//                // delete the records in localDB that are supposed to be deleted
//                if (clsLocalDB.CountOfRecords(strTableName) > clsMasterDB.CountOfRecords(strTableName))
//                {
//                    stRetvalue = clsMasterDB.getAllIDs(strTableName, "VariationID", true);
//                    clsLocalDB.Delete(strTableName, "VariationID", stRetvalue);
//                }
//                #endregion

//                #region tblPromoType
//                strTableName = "tblPromoType";
//                dtMaster = clsMasterDB.ListAsDataTable(strTableName, dteStartSyncDateTime, dteEndSyncDateTime);
//                dtLocal = clsLocalDB.ListAsDataTable(strTableName, dteStartSyncDateTime, dteEndSyncDateTime);
//                PromoType clsPromoType = new PromoType(clsLocalConnection.Connection, clsLocalConnection.Transaction);

//                // update all records that are changed [inserted/updated]
//                foreach (DataRow dr in dtMaster.Rows)
//                {
//                    //DataRow[] drx = dtLocal.Select("Configname = '" + dr["ConfigName"].ToString() + "' ");
//                    if (!DateTime.TryParse(dr["CreatedOn"].ToString(), out dteCreatedOn)) dteCreatedOn = Constants.C_DATE_MIN_VALUE;
//                    if (!DateTime.TryParse(dr["LastModified"].ToString(), out dteLastModified)) dteLastModified = Constants.C_DATE_MIN_VALUE;

//                    clsPromoType.Save(new PromoTypeDetails
//                    {
//                        PromoTypeID = Int32.Parse(dr["PromoTypeID"].ToString()),
//                        PromoTypeCode = dr["PromoTypeCode"].ToString(),
//                        PromoTypeName = dr["PromoTypeName"].ToString(),
//                        CreatedOn = dteCreatedOn,
//                        LastModified = dteLastModified
//                    });
//                }

//                // delete the records in localDB that are supposed to be deleted
//                if (clsLocalDB.CountOfRecords(strTableName) > clsMasterDB.CountOfRecords(strTableName))
//                {
//                    stRetvalue = clsMasterDB.getAllIDs(strTableName, "PromoTypeID", true);
//                    clsLocalDB.Delete(strTableName, "PromoTypeID", stRetvalue);
//                }
//                #endregion

//                #region tblPromo
//                strTableName = "tblPromo";
//                dtMaster = clsMasterDB.ListAsDataTable(strTableName, dteStartSyncDateTime, dteEndSyncDateTime);
//                dtLocal = clsLocalDB.ListAsDataTable(strTableName, dteStartSyncDateTime, dteEndSyncDateTime);
//                Promo clsPromo = new Promo(clsLocalConnection.Connection, clsLocalConnection.Transaction);

//                // update all records that are changed [inserted/updated]
//                foreach (DataRow dr in dtMaster.Rows)
//                {
//                    //DataRow[] drx = dtLocal.Select("Configname = '" + dr["ConfigName"].ToString() + "' ");
//                    if (!DateTime.TryParse(dr["CreatedOn"].ToString(), out dteCreatedOn)) dteCreatedOn = Constants.C_DATE_MIN_VALUE;
//                    if (!DateTime.TryParse(dr["LastModified"].ToString(), out dteLastModified)) dteLastModified = Constants.C_DATE_MIN_VALUE;

//                    clsPromo.Save(new PromoDetails
//                    {
//                        PromoID = Int32.Parse(dr["PromoID"].ToString()),
//                        PromoCode = dr["PromoCode"].ToString(),
//                        PromoName = dr["PromoName"].ToString(),
//                        StartDate = DateTime.Parse(dr["StartDate"].ToString()),
//                        EndDate = DateTime.Parse(dr["EndDate"].ToString()),
//                        PromoTypeID = Int32.Parse(dr["PromoTypeID"].ToString()),
//                        Status = (PromoStatus)Enum.Parse(typeof(PromoStatus), dr["Status"].ToString()),
//                        CreatedOn = dteCreatedOn,
//                        LastModified = dteLastModified
//                    });
//                }

//                // delete the records in localDB that are supposed to be deleted
//                if (clsLocalDB.CountOfRecords(strTableName) > clsMasterDB.CountOfRecords(strTableName))
//                {
//                    stRetvalue = clsMasterDB.getAllIDs(strTableName, "PromoID", true);
//                    clsLocalDB.Delete(strTableName, "PromoID", stRetvalue);
//                }
//                #endregion

//                /*****************************Product Group**************************/

//                #region tblProductGroup
//                strTableName = "tblProductGroup";
//                dtMaster = clsMasterDB.ListAsDataTable(strTableName, dteStartSyncDateTime, dteEndSyncDateTime);
//                dtLocal = clsLocalDB.ListAsDataTable(strTableName, dteStartSyncDateTime, dteEndSyncDateTime);
//                ProductGroup clsProductGroup = new ProductGroup(clsLocalConnection.Connection, clsLocalConnection.Transaction);

//                // update all records that are changed [inserted/updated]
//                foreach (DataRow dr in dtMaster.Rows)
//                {
//                    //DataRow[] drx = dtLocal.Select("Configname = '" + dr["ConfigName"].ToString() + "' ");
//                    if (!DateTime.TryParse(dr["CreatedOn"].ToString(), out dteCreatedOn)) dteCreatedOn = Constants.C_DATE_MIN_VALUE;
//                    if (!DateTime.TryParse(dr["LastModified"].ToString(), out dteLastModified)) dteLastModified = Constants.C_DATE_MIN_VALUE;

//                    clsProductGroup.Save(new ProductGroupDetails
//                    {
//                        ProductGroupID = Int64.Parse(dr["ProductGroupID"].ToString()),
//                        ProductGroupCode = dr["ProductGroupCode"].ToString(),
//                        ProductGroupName = dr["ProductGroupName"].ToString(),
//                        UnitDetails = new UnitDetails
//                        {
//                            UnitID = Int32.Parse(dr["BaseUnitID"].ToString())
//                        },
//                        Price = decimal.Parse(dr["Price"].ToString()),
//                        PurchasePrice = decimal.Parse(dr["PurchasePrice"].ToString()),
//                        IncludeInSubtotalDiscount = bool.Parse(dr["IncludeInSubtotalDiscount"].ToString()),
//                        VAT = decimal.Parse(dr["VAT"].ToString()),
//                        EVAT = decimal.Parse(dr["EVAT"].ToString()),
//                        LocalTax = decimal.Parse(dr["LocalTax"].ToString()),
//                        OrderSlipPrinter = (OrderSlipPrinter)Enum.Parse(typeof(OrderSlipPrinter), dr["OrderSlipPrinter"].ToString()),
//                        ChartOfAccountIDPurchase = Int32.Parse(dr["ChartOfAccountIDPurchase"].ToString()),
//                        ChartOfAccountIDTaxPurchase = Int32.Parse(dr["ChartOfAccountIDTaxPurchase"].ToString()),
//                        ChartOfAccountIDSold = Int32.Parse(dr["ChartOfAccountIDSold"].ToString()),
//                        ChartOfAccountIDTaxSold = Int32.Parse(dr["ChartOfAccountIDTaxSold"].ToString()),
//                        ChartOfAccountIDInventory = Int32.Parse(dr["ChartOfAccountIDInventory"].ToString()),
//                        SequenceNo = Int32.Parse(dr["SequenceNo"].ToString()),
//                        isLock = bool.Parse(dr["isLock"].ToString()),
//                        ProductGroupChartOfAccountDetails = new ProductGroupChartOfAccountDetails
//                        {
//                            ChartOfAccountIDTransferIn = Int32.Parse(dr["ChartOfAccountIDTransferIn"].ToString()),
//                            ChartOfAccountIDTaxTransferIn = Int32.Parse(dr["ChartOfAccountIDTaxTransferIn"].ToString()),
//                            ChartOfAccountIDTransferOut = Int32.Parse(dr["ChartOfAccountIDTransferOut"].ToString()),
//                            ChartOfAccountIDTaxTransferOut = Int32.Parse(dr["ChartOfAccountIDTaxTransferOut"].ToString()),
//                            ChartOfAccountIDInvAdjustment = Int32.Parse(dr["ChartOfAccountIDInvAdjustment"].ToString()),
//                            ChartOfAccountIDTaxInvAdjustment = Int32.Parse(dr["ChartOfAccountIDTaxInvAdjustment"].ToString()),
//                        },
//                        CreatedOn = dteCreatedOn,
//                        LastModified = dteLastModified
//                    });
//                }

//                // delete the records in localDB that are supposed to be deleted
//                if (clsLocalDB.CountOfRecords(strTableName) > clsMasterDB.CountOfRecords(strTableName))
//                {
//                    stRetvalue = clsMasterDB.getAllIDs(strTableName, "ProductGroupID", true);
//                    clsLocalDB.Delete(strTableName, "ProductGroupID", stRetvalue);
//                }
//                #endregion

//                #region tblProductGroupBaseVariationsMatrix
//                strTableName = "tblProductGroupBaseVariationsMatrix";
//                dtMaster = clsMasterDB.ListAsDataTable(strTableName, dteStartSyncDateTime, dteEndSyncDateTime);
//                dtLocal = clsLocalDB.ListAsDataTable(strTableName, dteStartSyncDateTime, dteEndSyncDateTime);
//                ProductGroupBaseVariationsMatrix clsProductGroupBaseVariationsMatrix = new ProductGroupBaseVariationsMatrix(clsLocalConnection.Connection, clsLocalConnection.Transaction);

//                // update all records that are changed [inserted/updated]
//                foreach (DataRow dr in dtMaster.Rows)
//                {
//                    //DataRow[] drx = dtLocal.Select("Configname = '" + dr["ConfigName"].ToString() + "' ");
//                    if (!DateTime.TryParse(dr["CreatedOn"].ToString(), out dteCreatedOn)) dteCreatedOn = Constants.C_DATE_MIN_VALUE;
//                    if (!DateTime.TryParse(dr["LastModified"].ToString(), out dteLastModified)) dteLastModified = Constants.C_DATE_MIN_VALUE;

//                    clsProductGroupBaseVariationsMatrix.Save(new ProductGroupBaseVariationsMatrixDetails
//                    {
//                        MatrixID = Int64.Parse(dr["MatrixID"].ToString()),
//                        GroupID = Int64.Parse(dr["GroupID"].ToString()),
//                        Description = dr["Description"].ToString(),
//                        UnitID = Int32.Parse(dr["UnitID"].ToString()),
//                        Price = decimal.Parse(dr["Price"].ToString()),
//                        PurchasePrice = decimal.Parse(dr["PurchasePrice"].ToString()),
//                        IncludeInSubtotalDiscount = bool.Parse(dr["IncludeInSubtotalDiscount"].ToString()),
//                        VAT = decimal.Parse(dr["VAT"].ToString()),
//                        EVAT = decimal.Parse(dr["EVAT"].ToString()),
//                        LocalTax = decimal.Parse(dr["LocalTax"].ToString()),
//                        CreatedOn = dteCreatedOn,
//                        LastModified = dteLastModified
//                    });
//                }

//                // delete the records in localDB that are supposed to be deleted
//                if (clsLocalDB.CountOfRecords(strTableName) > clsMasterDB.CountOfRecords(strTableName))
//                {
//                    stRetvalue = clsMasterDB.getAllIDs(strTableName, "MatrixID", true);
//                    clsLocalDB.Delete(strTableName, "MatrixID", stRetvalue);
//                }
//                #endregion

//                #region tblProductGroupVariations
//                strTableName = "tblProductGroupVariations";
//                dtMaster = clsMasterDB.ListAsDataTable(strTableName, dteStartSyncDateTime, dteEndSyncDateTime);
//                dtLocal = clsLocalDB.ListAsDataTable(strTableName, dteStartSyncDateTime, dteEndSyncDateTime);
//                ProductGroupVariations clsProductGroupVariations = new ProductGroupVariations(clsLocalConnection.Connection, clsLocalConnection.Transaction);

//                // update all records that are changed [inserted/updated]
//                foreach (DataRow dr in dtMaster.Rows)
//                {
//                    //DataRow[] drx = dtLocal.Select("Configname = '" + dr["ConfigName"].ToString() + "' ");
//                    if (!DateTime.TryParse(dr["CreatedOn"].ToString(), out dteCreatedOn)) dteCreatedOn = Constants.C_DATE_MIN_VALUE;
//                    if (!DateTime.TryParse(dr["LastModified"].ToString(), out dteLastModified)) dteLastModified = Constants.C_DATE_MIN_VALUE;

//                    clsProductGroupVariations.Save(new ProductGroupVariationDetails
//                    {
//                        ProductGroupVariationID = Int64.Parse(dr["ProductGroupVariationID"].ToString()),
//                        GroupID = Int64.Parse(dr["GroupID"].ToString()),
//                        VariationID = Int32.Parse(dr["VariationID"].ToString()),
//                        CreatedOn = dteCreatedOn,
//                        LastModified = dteLastModified
//                    });
//                }

//                // delete the records in localDB that are supposed to be deleted
//                if (clsLocalDB.CountOfRecords(strTableName) > clsMasterDB.CountOfRecords(strTableName))
//                {
//                    stRetvalue = clsMasterDB.getAllIDs(strTableName, "ProductGroupVariationID", true);
//                    clsLocalDB.Delete(strTableName, "ProductGroupVariationID", stRetvalue);
//                }
//                #endregion

//                #region tblProductGroupVariationsMatrix
//                strTableName = "tblProductGroupVariationsMatrix";
//                dtMaster = clsMasterDB.ListAsDataTable(strTableName, dteStartSyncDateTime, dteEndSyncDateTime);
//                dtLocal = clsLocalDB.ListAsDataTable(strTableName, dteStartSyncDateTime, dteEndSyncDateTime);
//                ProductGroupVariationsMatrix clsProductGroupVariationsMatrix = new ProductGroupVariationsMatrix(clsLocalConnection.Connection, clsLocalConnection.Transaction);

//                // update all records that are changed [inserted/updated]
//                foreach (DataRow dr in dtMaster.Rows)
//                {
//                    //DataRow[] drx = dtLocal.Select("Configname = '" + dr["ConfigName"].ToString() + "' ");
//                    if (!DateTime.TryParse(dr["CreatedOn"].ToString(), out dteCreatedOn)) dteCreatedOn = Constants.C_DATE_MIN_VALUE;
//                    if (!DateTime.TryParse(dr["LastModified"].ToString(), out dteLastModified)) dteLastModified = Constants.C_DATE_MIN_VALUE;

//                    clsProductGroupVariationsMatrix.Save(new ProductGroupVariationsMatrixDetails
//                    {
//                        ProductGroupVariationsMatrixID = Int64.Parse(dr["ProductGroupVariationsMatrixID"].ToString()),
//                        MatrixID = Int64.Parse(dr["MatrixID"].ToString()),
//                        VariationID = Int32.Parse(dr["VariationID"].ToString()),
//                        Description = dr["Description"].ToString(),
//                        CreatedOn = dteCreatedOn,
//                        LastModified = dteLastModified
//                    });
//                }

//                // delete the records in localDB that are supposed to be deleted
//                if (clsLocalDB.CountOfRecords(strTableName) > clsMasterDB.CountOfRecords(strTableName))
//                {
//                    stRetvalue = clsMasterDB.getAllIDs(strTableName, "ProductGroupVariationsMatrixID", true);
//                    clsLocalDB.Delete(strTableName, "ProductGroupVariationsMatrixID", stRetvalue);
//                }
//                #endregion

//                #region tblProductGroupUnitMatrix
//                strTableName = "tblProductGroupUnitMatrix";
//                dtMaster = clsMasterDB.ListAsDataTable(strTableName, dteStartSyncDateTime, dteEndSyncDateTime);
//                dtLocal = clsLocalDB.ListAsDataTable(strTableName, dteStartSyncDateTime, dteEndSyncDateTime);
//                ProductGroupUnitsMatrix clsProductGroupUnitsMatrix = new ProductGroupUnitsMatrix(clsLocalConnection.Connection, clsLocalConnection.Transaction);

//                // update all records that are changed [inserted/updated]
//                foreach (DataRow dr in dtMaster.Rows)
//                {
//                    //DataRow[] drx = dtLocal.Select("Configname = '" + dr["ConfigName"].ToString() + "' ");
//                    if (!DateTime.TryParse(dr["CreatedOn"].ToString(), out dteCreatedOn)) dteCreatedOn = Constants.C_DATE_MIN_VALUE;
//                    if (!DateTime.TryParse(dr["LastModified"].ToString(), out dteLastModified)) dteLastModified = Constants.C_DATE_MIN_VALUE;

//                    clsProductGroupUnitsMatrix.Save(new ProductGroupUnitsMatrixDetails
//                    {
//                        MatrixID = Int64.Parse(dr["MatrixID"].ToString()),
//                        GroupID = Int64.Parse(dr["GroupID"].ToString()),
//                        BaseUnitID = Int32.Parse(dr["BaseUnitID"].ToString()),
//                        BaseUnitValue = decimal.Parse(dr["BaseUnitValue"].ToString()),
//                        BottomUnitID = Int32.Parse(dr["BottomUnitID"].ToString()),
//                        BottomUnitValue = decimal.Parse(dr["BottomUnitValue"].ToString()),
//                        CreatedOn = dteCreatedOn,
//                        LastModified = dteLastModified
//                    });
//                }

//                // delete the records in localDB that are supposed to be deleted
//                if (clsLocalDB.CountOfRecords(strTableName) > clsMasterDB.CountOfRecords(strTableName))
//                {
//                    stRetvalue = clsMasterDB.getAllIDs(strTableName, "MatrixID", true);
//                    clsLocalDB.Delete(strTableName, "MatrixID", stRetvalue);
//                }
//                #endregion

//                #region tblProductGroupCharges
//                strTableName = "tblProductGroupCharges";
//                dtMaster = clsMasterDB.ListAsDataTable(strTableName, dteStartSyncDateTime, dteEndSyncDateTime);
//                dtLocal = clsLocalDB.ListAsDataTable(strTableName, dteStartSyncDateTime, dteEndSyncDateTime);
//                ProductGroupCharges clsProductGroupCharges = new ProductGroupCharges(clsLocalConnection.Connection, clsLocalConnection.Transaction);

//                // update all records that are changed [inserted/updated]
//                foreach (DataRow dr in dtMaster.Rows)
//                {
//                    //DataRow[] drx = dtLocal.Select("Configname = '" + dr["ConfigName"].ToString() + "' ");
//                    if (!DateTime.TryParse(dr["CreatedOn"].ToString(), out dteCreatedOn)) dteCreatedOn = Constants.C_DATE_MIN_VALUE;
//                    if (!DateTime.TryParse(dr["LastModified"].ToString(), out dteLastModified)) dteLastModified = Constants.C_DATE_MIN_VALUE;

//                    clsProductGroupCharges.Save(new ProductGroupChargeDetails
//                    {
//                        ChargeID = Int64.Parse(dr["ChargeID"].ToString()),
//                        GroupID = Int64.Parse(dr["GroupID"].ToString()),
//                        ChargeTypeID = Int32.Parse(dr["ChargeTypeID"].ToString()),
//                        ChargeAmount = decimal.Parse(dr["ChargeAmount"].ToString()),
//                        InPercent = bool.Parse(dr["InPercent"].ToString()),
//                        CreatedOn = dteCreatedOn,
//                        LastModified = dteLastModified
//                    });
//                }

//                // delete the records in localDB that are supposed to be deleted
//                if (clsLocalDB.CountOfRecords(strTableName) > clsMasterDB.CountOfRecords(strTableName))
//                {
//                    stRetvalue = clsMasterDB.getAllIDs(strTableName, "ChargeID", true);
//                    clsLocalDB.Delete(strTableName, "ChargeID", stRetvalue);
//                }
//                #endregion

//                /*****************************Product SubGroup**************************/

//                #region tblProductSubGroup
//                strTableName = "tblProductSubGroup";
//                dtMaster = clsMasterDB.ListAsDataTable(strTableName, dteStartSyncDateTime, dteEndSyncDateTime);
//                dtLocal = clsLocalDB.ListAsDataTable(strTableName, dteStartSyncDateTime, dteEndSyncDateTime);
//                ProductSubGroup clsProductSubGroup = new ProductSubGroup(clsLocalConnection.Connection, clsLocalConnection.Transaction);

//                // update all records that are changed [inserted/updated]
//                foreach (DataRow dr in dtMaster.Rows)
//                {
//                    //DataRow[] drx = dtLocal.Select("Configname = '" + dr["ConfigName"].ToString() + "' ");
//                    if (!DateTime.TryParse(dr["CreatedOn"].ToString(), out dteCreatedOn)) dteCreatedOn = Constants.C_DATE_MIN_VALUE;
//                    if (!DateTime.TryParse(dr["LastModified"].ToString(), out dteLastModified)) dteLastModified = Constants.C_DATE_MIN_VALUE;

//                    clsProductSubGroup.Save(new ProductSubGroupDetails
//                    {
//                        ProductSubGroupID = Int64.Parse(dr["ProductSubGroupID"].ToString()),
//                        ProductGroupID = Int64.Parse(dr["ProductGroupID"].ToString()),
//                        ProductSubGroupCode = dr["ProductSubGroupCode"].ToString(),
//                        ProductSubGroupName = dr["ProductSubGroupName"].ToString(),
//                        BaseUnitID = Int32.Parse(dr["BaseUnitID"].ToString()),
//                        Price = decimal.Parse(dr["Price"].ToString()),
//                        PurchasePrice = decimal.Parse(dr["PurchasePrice"].ToString()),
//                        IncludeInSubtotalDiscount = bool.Parse(dr["IncludeInSubtotalDiscount"].ToString()),
//                        VAT = decimal.Parse(dr["VAT"].ToString()),
//                        EVAT = decimal.Parse(dr["EVAT"].ToString()),
//                        LocalTax = decimal.Parse(dr["LocalTax"].ToString()),
//                        ChartOfAccountIDPurchase = Int32.Parse(dr["ChartOfAccountIDPurchase"].ToString()),
//                        ChartOfAccountIDTaxPurchase = Int32.Parse(dr["ChartOfAccountIDTaxPurchase"].ToString()),
//                        ChartOfAccountIDSold = Int32.Parse(dr["ChartOfAccountIDSold"].ToString()),
//                        ChartOfAccountIDTaxSold = Int32.Parse(dr["ChartOfAccountIDTaxSold"].ToString()),
//                        ChartOfAccountIDInventory = Int32.Parse(dr["ChartOfAccountIDInventory"].ToString()),
//                        SequenceNo = Int32.Parse(dr["SequenceNo"].ToString()),
//                        ImagePath = dr["ImagePath"].ToString(),
//                        ProductSubGroupChartOfAccountDetails = new ProductSubGroupChartOfAccountDetails
//                        {
//                            ChartOfAccountIDTransferIn = Int32.Parse(dr["ChartOfAccountIDTransferIn"].ToString()),
//                            ChartOfAccountIDTaxTransferIn = Int32.Parse(dr["ChartOfAccountIDTaxTransferIn"].ToString()),
//                            ChartOfAccountIDTransferOut = Int32.Parse(dr["ChartOfAccountIDTransferOut"].ToString()),
//                            ChartOfAccountIDTaxTransferOut = Int32.Parse(dr["ChartOfAccountIDTaxTransferOut"].ToString()),
//                            ChartOfAccountIDInvAdjustment = Int32.Parse(dr["ChartOfAccountIDInvAdjustment"].ToString()),
//                            ChartOfAccountIDTaxInvAdjustment = Int32.Parse(dr["ChartOfAccountIDTaxInvAdjustment"].ToString()),
//                        },
//                        CreatedOn = dteCreatedOn,
//                        LastModified = dteLastModified
//                    });
//                }

//                // delete the records in localDB that are supposed to be deleted
//                if (clsLocalDB.CountOfRecords(strTableName) > clsMasterDB.CountOfRecords(strTableName))
//                {
//                    stRetvalue = clsMasterDB.getAllIDs(strTableName, "ProductSubGroupID", true);
//                    clsLocalDB.Delete(strTableName, "ProductSubGroupID", stRetvalue);
//                }
//                #endregion

//                #region tblProductSubGroupBaseVariationsMatrix
//                strTableName = "tblProductSubGroupBaseVariationsMatrix";
//                dtMaster = clsMasterDB.ListAsDataTable(strTableName, dteStartSyncDateTime, dteEndSyncDateTime);
//                dtLocal = clsLocalDB.ListAsDataTable(strTableName, dteStartSyncDateTime, dteEndSyncDateTime);
//                ProductSubGroupBaseVariationsMatrix clsProductSubGroupBaseVariationsMatrix = new ProductSubGroupBaseVariationsMatrix(clsLocalConnection.Connection, clsLocalConnection.Transaction);

//                // update all records that are changed [inserted/updated]
//                foreach (DataRow dr in dtMaster.Rows)
//                {
//                    //DataRow[] drx = dtLocal.Select("Configname = '" + dr["ConfigName"].ToString() + "' ");
//                    if (!DateTime.TryParse(dr["CreatedOn"].ToString(), out dteCreatedOn)) dteCreatedOn = Constants.C_DATE_MIN_VALUE;
//                    if (!DateTime.TryParse(dr["LastModified"].ToString(), out dteLastModified)) dteLastModified = Constants.C_DATE_MIN_VALUE;

//                    clsProductSubGroupBaseVariationsMatrix.Save(new ProductSubGroupBaseVariationsMatrixDetails
//                    {
//                        MatrixID = Int64.Parse(dr["MatrixID"].ToString()),
//                        SubGroupID = Int64.Parse(dr["SubGroupID"].ToString()),
//                        Description = dr["Description"].ToString(),
//                        UnitID = Int32.Parse(dr["UnitID"].ToString()),
//                        Price = decimal.Parse(dr["Price"].ToString()),
//                        PurchasePrice = decimal.Parse(dr["PurchasePrice"].ToString()),
//                        IncludeInSubtotalDiscount = bool.Parse(dr["IncludeInSubtotalDiscount"].ToString()),
//                        VAT = decimal.Parse(dr["VAT"].ToString()),
//                        EVAT = decimal.Parse(dr["EVAT"].ToString()),
//                        LocalTax = decimal.Parse(dr["LocalTax"].ToString()),
//                        CreatedOn = dteCreatedOn,
//                        LastModified = dteLastModified
//                    });
//                }

//                // delete the records in localDB that are supposed to be deleted
//                if (clsLocalDB.CountOfRecords(strTableName) > clsMasterDB.CountOfRecords(strTableName))
//                {
//                    stRetvalue = clsMasterDB.getAllIDs(strTableName, "MatrixID", true);
//                    clsLocalDB.Delete(strTableName, "MatrixID", stRetvalue);
//                }
//                #endregion

//                #region tblProductSubGroupVariations
//                strTableName = "tblProductSubGroupVariations";
//                dtMaster = clsMasterDB.ListAsDataTable(strTableName, dteStartSyncDateTime, dteEndSyncDateTime);
//                dtLocal = clsLocalDB.ListAsDataTable(strTableName, dteStartSyncDateTime, dteEndSyncDateTime);
//                ProductSubGroupVariations clsProductSubGroupVariations = new ProductSubGroupVariations(clsLocalConnection.Connection, clsLocalConnection.Transaction);

//                // update all records that are changed [inserted/updated]
//                foreach (DataRow dr in dtMaster.Rows)
//                {
//                    //DataRow[] drx = dtLocal.Select("Configname = '" + dr["ConfigName"].ToString() + "' ");
//                    if (!DateTime.TryParse(dr["CreatedOn"].ToString(), out dteCreatedOn)) dteCreatedOn = Constants.C_DATE_MIN_VALUE;
//                    if (!DateTime.TryParse(dr["LastModified"].ToString(), out dteLastModified)) dteLastModified = Constants.C_DATE_MIN_VALUE;

//                    clsProductSubGroupVariations.Save(new ProductSubGroupVariationDetails
//                    {
//                        ProductSubGroupVariationID = Int64.Parse(dr["ProductSubGroupVariationID"].ToString()),
//                        SubGroupID = Int64.Parse(dr["SubGroupID"].ToString()),
//                        VariationID = Int32.Parse(dr["VariationID"].ToString()),
//                        CreatedOn = dteCreatedOn,
//                        LastModified = dteLastModified
//                    });
//                }

//                // delete the records in localDB that are supposed to be deleted
//                if (clsLocalDB.CountOfRecords(strTableName) > clsMasterDB.CountOfRecords(strTableName))
//                {
//                    stRetvalue = clsMasterDB.getAllIDs(strTableName, "ProductSubGroupVariationID", true);
//                    clsLocalDB.Delete(strTableName, "ProductSubGroupVariationID", stRetvalue);
//                }
//                #endregion

//                #region tblProductSubGroupVariationsMatrix
//                strTableName = "tblProductSubGroupVariationsMatrix";
//                dtMaster = clsMasterDB.ListAsDataTable(strTableName, dteStartSyncDateTime, dteEndSyncDateTime);
//                dtLocal = clsLocalDB.ListAsDataTable(strTableName, dteStartSyncDateTime, dteEndSyncDateTime);
//                ProductSubGroupVariationsMatrix clsProductSubGroupVariationsMatrix = new ProductSubGroupVariationsMatrix(clsLocalConnection.Connection, clsLocalConnection.Transaction);

//                // update all records that are changed [inserted/updated]
//                foreach (DataRow dr in dtMaster.Rows)
//                {
//                    //DataRow[] drx = dtLocal.Select("Configname = '" + dr["ConfigName"].ToString() + "' ");
//                    if (!DateTime.TryParse(dr["CreatedOn"].ToString(), out dteCreatedOn)) dteCreatedOn = Constants.C_DATE_MIN_VALUE;
//                    if (!DateTime.TryParse(dr["LastModified"].ToString(), out dteLastModified)) dteLastModified = Constants.C_DATE_MIN_VALUE;

//                    clsProductSubGroupVariationsMatrix.Save(new ProductSubGroupVariationsMatrixDetails
//                    {
//                        ProductSubGroupVariationsMatrixID = Int64.Parse(dr["ProductSubGroupVariationsMatrixID"].ToString()),
//                        MatrixID = Int64.Parse(dr["MatrixID"].ToString()),
//                        VariationID = Int32.Parse(dr["VariationID"].ToString()),
//                        Description = dr["Description"].ToString(),
//                        CreatedOn = dteCreatedOn,
//                        LastModified = dteLastModified
//                    });
//                }

//                // delete the records in localDB that are supposed to be deleted
//                if (clsLocalDB.CountOfRecords(strTableName) > clsMasterDB.CountOfRecords(strTableName))
//                {
//                    stRetvalue = clsMasterDB.getAllIDs(strTableName, "ProductSubGroupVariationsMatrixID", true);
//                    clsLocalDB.Delete(strTableName, "ProductSubGroupVariationsMatrixID", stRetvalue);
//                }
//                #endregion

//                #region tblProductSubGroupUnitMatrix
//                strTableName = "tblProductSubGroupUnitMatrix";
//                dtMaster = clsMasterDB.ListAsDataTable(strTableName, dteStartSyncDateTime, dteEndSyncDateTime);
//                dtLocal = clsLocalDB.ListAsDataTable(strTableName, dteStartSyncDateTime, dteEndSyncDateTime);
//                ProductSubGroupUnitsMatrix clsProductSubGroupUnitsMatrix = new ProductSubGroupUnitsMatrix(clsLocalConnection.Connection, clsLocalConnection.Transaction);

//                // update all records that are changed [inserted/updated]
//                foreach (DataRow dr in dtMaster.Rows)
//                {
//                    //DataRow[] drx = dtLocal.Select("Configname = '" + dr["ConfigName"].ToString() + "' ");
//                    if (!DateTime.TryParse(dr["CreatedOn"].ToString(), out dteCreatedOn)) dteCreatedOn = Constants.C_DATE_MIN_VALUE;
//                    if (!DateTime.TryParse(dr["LastModified"].ToString(), out dteLastModified)) dteLastModified = Constants.C_DATE_MIN_VALUE;

//                    clsProductSubGroupUnitsMatrix.Save(new ProductSubGroupUnitsMatrixDetails
//                    {
//                        MatrixID = Int64.Parse(dr["MatrixID"].ToString()),
//                        SubGroupID = Int64.Parse(dr["SubGroupID"].ToString()),
//                        BaseUnitID = Int32.Parse(dr["BaseUnitID"].ToString()),
//                        BaseUnitValue = decimal.Parse(dr["BaseUnitValue"].ToString()),
//                        BottomUnitID = Int32.Parse(dr["BottomUnitID"].ToString()),
//                        BottomUnitValue = decimal.Parse(dr["BottomUnitValue"].ToString()),
//                        CreatedOn = dteCreatedOn,
//                        LastModified = dteLastModified
//                    });
//                }

//                // delete the records in localDB that are supposed to be deleted
//                if (clsLocalDB.CountOfRecords(strTableName) > clsMasterDB.CountOfRecords(strTableName))
//                {
//                    stRetvalue = clsMasterDB.getAllIDs(strTableName, "MatrixID", true);
//                    clsLocalDB.Delete(strTableName, "MatrixID", stRetvalue);
//                }
//                #endregion

//                #region tblProductSubGroupCharges
//                strTableName = "tblProductSubGroupCharges";
//                dtMaster = clsMasterDB.ListAsDataTable(strTableName, dteStartSyncDateTime, dteEndSyncDateTime);
//                dtLocal = clsLocalDB.ListAsDataTable(strTableName, dteStartSyncDateTime, dteEndSyncDateTime);
//                ProductSubGroupCharges clsProductSubGroupCharges = new ProductSubGroupCharges(clsLocalConnection.Connection, clsLocalConnection.Transaction);

//                // update all records that are changed [inserted/updated]
//                foreach (DataRow dr in dtMaster.Rows)
//                {
//                    //DataRow[] drx = dtLocal.Select("Configname = '" + dr["ConfigName"].ToString() + "' ");
//                    if (!DateTime.TryParse(dr["CreatedOn"].ToString(), out dteCreatedOn)) dteCreatedOn = Constants.C_DATE_MIN_VALUE;
//                    if (!DateTime.TryParse(dr["LastModified"].ToString(), out dteLastModified)) dteLastModified = Constants.C_DATE_MIN_VALUE;

//                    clsProductSubGroupCharges.Save(new ProductSubGroupChargeDetails
//                    {
//                        ChargeID = Int64.Parse(dr["ChargeID"].ToString()),
//                        SubGroupID = Int64.Parse(dr["SubGroupID"].ToString()),
//                        ChargeTypeID = Int32.Parse(dr["ChargeTypeID"].ToString()),
//                        ChargeAmount = decimal.Parse(dr["ChargeAmount"].ToString()),
//                        InPercent = bool.Parse(dr["InPercent"].ToString()),
//                        CreatedOn = dteCreatedOn,
//                        LastModified = dteLastModified
//                    });
//                }

//                // delete the records in localDB that are supposed to be deleted
//                if (clsLocalDB.CountOfRecords(strTableName) > clsMasterDB.CountOfRecords(strTableName))
//                {
//                    stRetvalue = clsMasterDB.getAllIDs(strTableName, "ChargeID", true);
//                    clsLocalDB.Delete(strTableName, "ChargeID", stRetvalue);
//                }
//                #endregion

//                /*****************************Products**************************/

//                #region tblProducts
//                strTableName = "tblProducts";
//                dtMaster = clsMasterDB.ListAsDataTable(strTableName, dteStartSyncDateTime, dteEndSyncDateTime);
//                dtLocal = clsLocalDB.ListAsDataTable(strTableName, dteStartSyncDateTime, dteEndSyncDateTime);
//                Products clsProducts = new Products(clsLocalConnection.Connection, clsLocalConnection.Transaction);

//                // update all records that are changed [inserted/updated]
//                foreach (DataRow dr in dtMaster.Rows)
//                {
//                    //DataRow[] drx = dtLocal.Select("Configname = '" + dr["ConfigName"].ToString() + "' ");
//                    if (!DateTime.TryParse(dr["CreatedOn"].ToString(), out dteCreatedOn)) dteCreatedOn = Constants.C_DATE_MIN_VALUE;
//                    if (!DateTime.TryParse(dr["LastModified"].ToString(), out dteLastModified)) dteLastModified = Constants.C_DATE_MIN_VALUE;

//                    clsProducts.Save(new ProductDetails
//                    {
//                        ProductID = Int64.Parse(dr["ProductID"].ToString()),
//                        ProductCode = dr["ProductCode"].ToString(),
//                        ProductDesc = dr["ProductDesc"].ToString(),
//                        ProductSubGroupID = Int64.Parse(dr["ProductSubGroupID"].ToString()),
//                        BaseUnitID = Int32.Parse(dr["BaseUnitID"].ToString()),
//                        DateCreated = DateTime.Parse(dr["DateCreated"].ToString()),
//                        Deleted = bool.Parse(dr["Deleted"].ToString()),
//                        IncludeInSubtotalDiscount = bool.Parse(dr["IncludeInSubtotalDiscount"].ToString()),
//                        MinThreshold = decimal.Parse(dr["MinThreshold"].ToString()),
//                        MaxThreshold = decimal.Parse(dr["MaxThreshold"].ToString()),
//                        SupplierID = Int64.Parse(dr["SupplierID"].ToString()),
//                        ChartOfAccountIDPurchase = Int32.Parse(dr["ChartOfAccountIDPurchase"].ToString()),
//                        ChartOfAccountIDTaxPurchase = Int32.Parse(dr["ChartOfAccountIDTaxPurchase"].ToString()),
//                        ChartOfAccountIDSold = Int32.Parse(dr["ChartOfAccountIDSold"].ToString()),
//                        ChartOfAccountIDTaxSold = Int32.Parse(dr["ChartOfAccountIDTaxSold"].ToString()),
//                        ChartOfAccountIDInventory = Int32.Parse(dr["ChartOfAccountIDInventory"].ToString()),
//                        IsItemSold = bool.Parse(dr["IsItemSold"].ToString()),
//                        WillPrintProductComposition = bool.Parse(dr["WillPrintProductComposition"].ToString()),
//                        VariationCount = Int32.Parse(dr["VariationCount"].ToString()),
//                        Active = bool.Parse(dr["Active"].ToString()),
//                        PercentageCommision = decimal.Parse(dr["PercentageCommision"].ToString()),
//                        RID = Int32.Parse(dr["RID"].ToString()),
//                        RIDMinThreshold = decimal.Parse(dr["RIDMinThreshold"].ToString()),
//                        RIDMaxThreshold = decimal.Parse(dr["RIDMaxThreshold"].ToString()),
//                        RewardPoints = decimal.Parse(dr["RewardPoints"].ToString()),
//                        SequenceNo = Int32.Parse(dr["SequenceNo"].ToString()),
//                        ProductChartOfAccountDetails = new ProductChartOfAccountDetails
//                        {
//                            ChartOfAccountIDTransferIn = Int32.Parse(dr["ChartOfAccountIDTransferIn"].ToString()),
//                            ChartOfAccountIDTaxTransferIn = Int32.Parse(dr["ChartOfAccountIDTaxTransferIn"].ToString()),
//                            ChartOfAccountIDTransferOut = Int32.Parse(dr["ChartOfAccountIDTransferOut"].ToString()),
//                            ChartOfAccountIDTaxTransferOut = Int32.Parse(dr["ChartOfAccountIDTaxTransferOut"].ToString()),
//                            ChartOfAccountIDInvAdjustment = Int32.Parse(dr["ChartOfAccountIDInvAdjustment"].ToString()),
//                            ChartOfAccountIDTaxInvAdjustment = Int32.Parse(dr["ChartOfAccountIDTaxInvAdjustment"].ToString()),
//                        },
//                        CreatedOn = dteCreatedOn,
//                        LastModified = dteLastModified
//                    });
//                }

//                // delete the records in localDB that are supposed to be deleted
//                if (clsLocalDB.CountOfRecords(strTableName) > clsMasterDB.CountOfRecords(strTableName))
//                {
//                    stRetvalue = clsMasterDB.getAllIDs(strTableName, "ProductID", true);
//                    clsLocalDB.Delete(strTableName, "ProductID", stRetvalue);
//                }
//                #endregion

//                #region tblProductBaseVariationsMatrix
//                strTableName = "tblProductBaseVariationsMatrix";
//                dtMaster = clsMasterDB.ListAsDataTable(strTableName, dteStartSyncDateTime, dteEndSyncDateTime);
//                dtLocal = clsLocalDB.ListAsDataTable(strTableName, dteStartSyncDateTime, dteEndSyncDateTime);
//                ProductBaseVariationsMatrix clsProductBaseVariationsMatrix = new ProductBaseVariationsMatrix(clsLocalConnection.Connection, clsLocalConnection.Transaction);

//                // update all records that are changed [inserted/updated]
//                foreach (DataRow dr in dtMaster.Rows)
//                {
//                    //DataRow[] drx = dtLocal.Select("Configname = '" + dr["ConfigName"].ToString() + "' ");
//                    if (!DateTime.TryParse(dr["CreatedOn"].ToString(), out dteCreatedOn)) dteCreatedOn = Constants.C_DATE_MIN_VALUE;
//                    if (!DateTime.TryParse(dr["LastModified"].ToString(), out dteLastModified)) dteLastModified = Constants.C_DATE_MIN_VALUE;

//                    clsProductBaseVariationsMatrix.Save(new ProductBaseVariationsMatrixDetails
//                    {
//                        MatrixID = Int64.Parse(dr["MatrixID"].ToString()),
//                        ProductID = Int64.Parse(dr["ProductID"].ToString()),
//                        Description = dr["Description"].ToString(),
//                        UnitID = Int32.Parse(dr["UnitID"].ToString()),
//                        IncludeInSubtotalDiscount = bool.Parse(dr["IncludeInSubtotalDiscount"].ToString()),
//                        MinThreshold = decimal.Parse(dr["MinThreshold"].ToString()),
//                        MaxThreshold = decimal.Parse(dr["MaxThreshold"].ToString()),
//                        RIDMinThreshold = decimal.Parse(dr["RIDMinThreshold"].ToString()),
//                        RIDMaxThreshold = decimal.Parse(dr["RIDMaxThreshold"].ToString()),
//                        SupplierID = Int64.Parse(dr["SupplierID"].ToString()),
//                        Deleted = bool.Parse(dr["Deleted"].ToString()),
//                        CreatedOn = dteCreatedOn,
//                        LastModified = dteLastModified
//                    });
//                }

//                // delete the records in localDB that are supposed to be deleted
//                if (clsLocalDB.CountOfRecords(strTableName) > clsMasterDB.CountOfRecords(strTableName))
//                {
//                    stRetvalue = clsMasterDB.getAllIDs(strTableName, "MatrixID", true);
//                    clsLocalDB.Delete(strTableName, "MatrixID", stRetvalue);
//                }
//                #endregion

//                #region tblProductVariations
//                strTableName = "tblProductVariations";
//                dtMaster = clsMasterDB.ListAsDataTable(strTableName, dteStartSyncDateTime, dteEndSyncDateTime);
//                dtLocal = clsLocalDB.ListAsDataTable(strTableName, dteStartSyncDateTime, dteEndSyncDateTime);
//                ProductVariations clsProductVariations = new ProductVariations(clsLocalConnection.Connection, clsLocalConnection.Transaction);

//                // update all records that are changed [inserted/updated]
//                foreach (DataRow dr in dtMaster.Rows)
//                {
//                    //DataRow[] drx = dtLocal.Select("Configname = '" + dr["ConfigName"].ToString() + "' ");
//                    if (!DateTime.TryParse(dr["CreatedOn"].ToString(), out dteCreatedOn)) dteCreatedOn = Constants.C_DATE_MIN_VALUE;
//                    if (!DateTime.TryParse(dr["LastModified"].ToString(), out dteLastModified)) dteLastModified = Constants.C_DATE_MIN_VALUE;

//                    clsProductVariations.Save(new ProductVariationDetails
//                    {
//                        ProductVariationID = Int64.Parse(dr["ProductVariationID"].ToString()),
//                        ProductID = Int64.Parse(dr["ProductID"].ToString()),
//                        VariationID = Int32.Parse(dr["VariationID"].ToString()),
//                        CreatedOn = dteCreatedOn,
//                        LastModified = dteLastModified
//                    });
//                }

//                // delete the records in localDB that are supposed to be deleted
//                if (clsLocalDB.CountOfRecords(strTableName) > clsMasterDB.CountOfRecords(strTableName))
//                {
//                    stRetvalue = clsMasterDB.getAllIDs(strTableName, "ProductVariationID", true);
//                    clsLocalDB.Delete(strTableName, "ProductVariationID", stRetvalue);
//                }
//                #endregion

//                #region tblProductVariationsMatrix
//                strTableName = "tblProductVariationsMatrix";
//                dtMaster = clsMasterDB.ListAsDataTable(strTableName, dteStartSyncDateTime, dteEndSyncDateTime);
//                dtLocal = clsLocalDB.ListAsDataTable(strTableName, dteStartSyncDateTime, dteEndSyncDateTime);
//                ProductVariationsMatrix clsProductVariationsMatrix = new ProductVariationsMatrix(clsLocalConnection.Connection, clsLocalConnection.Transaction);

//                // update all records that are changed [inserted/updated]
//                foreach (DataRow dr in dtMaster.Rows)
//                {
//                    //DataRow[] drx = dtLocal.Select("Configname = '" + dr["ConfigName"].ToString() + "' ");
//                    if (!DateTime.TryParse(dr["CreatedOn"].ToString(), out dteCreatedOn)) dteCreatedOn = Constants.C_DATE_MIN_VALUE;
//                    if (!DateTime.TryParse(dr["LastModified"].ToString(), out dteLastModified)) dteLastModified = Constants.C_DATE_MIN_VALUE;

//                    clsProductVariationsMatrix.Save(new ProductVariationsMatrixDetails
//                    {
//                        ProductVariationsMatrixID = Int64.Parse(dr["ProductVariationsMatrixID"].ToString()),
//                        MatrixID = Int64.Parse(dr["MatrixID"].ToString()),
//                        VariationID = Int32.Parse(dr["VariationID"].ToString()),
//                        Description = dr["Description"].ToString(),
//                        CreatedOn = dteCreatedOn,
//                        LastModified = dteLastModified
//                    });
//                }

//                //do not delete
//                //// delete the records in localDB that are supposed to be deleted
//                //if (clsLocalDB.CountOfRecords(strTableName) > clsMasterDB.CountOfRecords(strTableName))
//                //{
//                //    stRetvalue = clsMasterDB.getAllIDs(strTableName, "ProductVariationsMatrixID", true);
//                //    clsLocalDB.Delete(strTableName, "ProductVariationsMatrixID", stRetvalue);
//                //}
//                #endregion

//                #region tblProductUnitMatrix
//                strTableName = "tblProductUnitMatrix";
//                dtMaster = clsMasterDB.ListAsDataTable(strTableName, dteStartSyncDateTime, dteEndSyncDateTime);
//                dtLocal = clsLocalDB.ListAsDataTable(strTableName, dteStartSyncDateTime, dteEndSyncDateTime);
//                ProductUnitsMatrix clsProductUnitsMatrix = new ProductUnitsMatrix(clsLocalConnection.Connection, clsLocalConnection.Transaction);

//                // update all records that are changed [inserted/updated]
//                foreach (DataRow dr in dtMaster.Rows)
//                {
//                    //DataRow[] drx = dtLocal.Select("Configname = '" + dr["ConfigName"].ToString() + "' ");
//                    if (!DateTime.TryParse(dr["CreatedOn"].ToString(), out dteCreatedOn)) dteCreatedOn = Constants.C_DATE_MIN_VALUE;
//                    if (!DateTime.TryParse(dr["LastModified"].ToString(), out dteLastModified)) dteLastModified = Constants.C_DATE_MIN_VALUE;

//                    clsProductUnitsMatrix.Save(new ProductUnitsMatrixDetails
//                    {
//                        MatrixID = Int64.Parse(dr["MatrixID"].ToString()),
//                        ProductID = Int64.Parse(dr["ProductID"].ToString()),
//                        BaseUnitID = Int32.Parse(dr["BaseUnitID"].ToString()),
//                        BaseUnitValue = decimal.Parse(dr["BaseUnitValue"].ToString()),
//                        BottomUnitID = Int32.Parse(dr["BottomUnitID"].ToString()),
//                        BottomUnitValue = decimal.Parse(dr["BottomUnitValue"].ToString()),
//                        CreatedOn = dteCreatedOn,
//                        LastModified = dteLastModified
//                    });
//                }

//                // delete the records in localDB that are supposed to be deleted
//                if (clsLocalDB.CountOfRecords(strTableName) > clsMasterDB.CountOfRecords(strTableName))
//                {
//                    stRetvalue = clsMasterDB.getAllIDs(strTableName, "MatrixID", true);
//                    clsLocalDB.Delete(strTableName, "MatrixID", stRetvalue);
//                }
//                #endregion

//                #region tblProductPackage
//                strTableName = "tblProductPackage";
//                dtMaster = clsMasterDB.ListAsDataTable(strTableName, dteStartSyncDateTime, dteEndSyncDateTime);
//                dtLocal = clsLocalDB.ListAsDataTable(strTableName, dteStartSyncDateTime, dteEndSyncDateTime);
//                ProductPackage clsProductPackage = new ProductPackage(clsLocalConnection.Connection, clsLocalConnection.Transaction);

//                // update all records that are changed [inserted/updated]
//                foreach (DataRow dr in dtMaster.Rows)
//                {
//                    //DataRow[] drx = dtLocal.Select("Configname = '" + dr["ConfigName"].ToString() + "' ");
//                    if (!DateTime.TryParse(dr["CreatedOn"].ToString(), out dteCreatedOn)) dteCreatedOn = Constants.C_DATE_MIN_VALUE;
//                    if (!DateTime.TryParse(dr["LastModified"].ToString(), out dteLastModified)) dteLastModified = Constants.C_DATE_MIN_VALUE;

//                    clsProductPackage.Save(new ProductPackageDetails
//                    {
//                        PackageID = Int64.Parse(dr["PackageID"].ToString()),
//                        ProductID = Int64.Parse(dr["ProductID"].ToString()),
//                        MatrixID = Int64.Parse(dr["MatrixID"].ToString()),
//                        UnitID = Int32.Parse(dr["UnitID"].ToString()),
//                        Price = decimal.Parse(dr["Price"].ToString()),
//                        PurchasePrice = decimal.Parse(dr["PurchasePrice"].ToString()),
//                        Quantity = decimal.Parse(dr["Quantity"].ToString()),
//                        VAT = decimal.Parse(dr["VAT"].ToString()),
//                        EVAT = decimal.Parse(dr["EVAT"].ToString()),
//                        LocalTax = decimal.Parse(dr["LocalTax"].ToString()),
//                        WSPrice = decimal.Parse(dr["WSPrice"].ToString()),
//                        BarCode1 = dr["BarCode1"].ToString(),
//                        BarCode2 = dr["BarCode2"].ToString(),
//                        BarCode3 = dr["BarCode3"].ToString(),
//                        BarCode4 = dr["BarCode4"].ToString(),
//                        CreatedOn = dteCreatedOn,
//                        LastModified = dteLastModified
//                    });
//                }

//                // delete the records in localDB that are supposed to be deleted
//                if (clsLocalDB.CountOfRecords(strTableName) > clsMasterDB.CountOfRecords(strTableName))
//                {
//                    stRetvalue = clsMasterDB.getAllIDs(strTableName, "PackageID", true);
//                    clsLocalDB.Delete(strTableName, "PackageID", stRetvalue);
//                }
//                #endregion

//                #region tblProductComposition
//                strTableName = "tblProductComposition";
//                dtMaster = clsMasterDB.ListAsDataTable(strTableName, dteStartSyncDateTime, dteEndSyncDateTime);
//                dtLocal = clsLocalDB.ListAsDataTable(strTableName, dteStartSyncDateTime, dteEndSyncDateTime);
//                ProductComposition clsProductComposition = new ProductComposition(clsLocalConnection.Connection, clsLocalConnection.Transaction);

//                // update all records that are changed [inserted/updated]
//                foreach (DataRow dr in dtMaster.Rows)
//                {
//                    //DataRow[] drx = dtLocal.Select("Configname = '" + dr["ConfigName"].ToString() + "' ");
//                    if (!DateTime.TryParse(dr["CreatedOn"].ToString(), out dteCreatedOn)) dteCreatedOn = Constants.C_DATE_MIN_VALUE;
//                    if (!DateTime.TryParse(dr["LastModified"].ToString(), out dteLastModified)) dteLastModified = Constants.C_DATE_MIN_VALUE;

//                    clsProductComposition.Save(new ProductCompositionDetails
//                    {
//                        CompositionID = Int64.Parse(dr["CompositionID"].ToString()),
//                        MainProductID = Int64.Parse(dr["MainProductID"].ToString()),
//                        ProductID = Int64.Parse(dr["ProductID"].ToString()),
//                        VariationMatrixID = Int64.Parse(dr["VariationMatrixID"].ToString()),
//                        UnitID = Int32.Parse(dr["UnitID"].ToString()),
//                        Quantity = decimal.Parse(dr["Quantity"].ToString()),
//                        CreatedOn = dteCreatedOn,
//                        LastModified = dteLastModified
//                    });
//                }

//                // delete the records in localDB that are supposed to be deleted
//                if (clsLocalDB.CountOfRecords(strTableName) > clsMasterDB.CountOfRecords(strTableName))
//                {
//                    stRetvalue = clsMasterDB.getAllIDs(strTableName, "CompositionID", true);
//                    clsLocalDB.Delete(strTableName, "CompositionID", stRetvalue);
//                }
//                #endregion

//                #region tblPromoItems
//                strTableName = "tblPromoItems";
//                dtMaster = clsMasterDB.ListAsDataTable(strTableName, dteStartSyncDateTime, dteEndSyncDateTime);
//                dtLocal = clsLocalDB.ListAsDataTable(strTableName, dteStartSyncDateTime, dteEndSyncDateTime);
//                PromoItems clsPromoItems = new PromoItems(clsLocalConnection.Connection, clsLocalConnection.Transaction);

//                // update all records that are changed [inserted/updated]
//                foreach (DataRow dr in dtMaster.Rows)
//                {
//                    //DataRow[] drx = dtLocal.Select("Configname = '" + dr["ConfigName"].ToString() + "' ");
//                    if (!DateTime.TryParse(dr["CreatedOn"].ToString(), out dteCreatedOn)) dteCreatedOn = Constants.C_DATE_MIN_VALUE;
//                    if (!DateTime.TryParse(dr["LastModified"].ToString(), out dteLastModified)) dteLastModified = Constants.C_DATE_MIN_VALUE;

//                    clsPromoItems.Save(new PromoItemsDetails
//                    {
//                        PromoItemsID = Int64.Parse(dr["PromoItemsID"].ToString()),
//                        PromoID = Int64.Parse(dr["PromoID"].ToString()),
//                        ContactID = Int64.Parse(dr["ContactID"].ToString()),
//                        ProductGroupID = Int64.Parse(dr["ProductGroupID"].ToString()),
//                        ProductSubGroupID = Int64.Parse(dr["ProductSubGroupID"].ToString()),
//                        ProductID = Int64.Parse(dr["ProductID"].ToString()),
//                        VariationMatrixID = Int64.Parse(dr["VariationMatrixID"].ToString()),
//                        Quantity = decimal.Parse(dr["Quantity"].ToString()),
//                        PromoValue = decimal.Parse(dr["PromoValue"].ToString()),
//                        InPercent = bool.Parse(dr["InPercent"].ToString()),
//                        CreatedOn = dteCreatedOn,
//                        LastModified = dteLastModified
//                    });
//                }

//                // delete the records in localDB that are supposed to be deleted
//                if (clsLocalDB.CountOfRecords(strTableName) > clsMasterDB.CountOfRecords(strTableName))
//                {
//                    stRetvalue = clsMasterDB.getAllIDs(strTableName, "PromoItemsID", true);
//                    clsLocalDB.Delete(strTableName, "PromoItemsID", stRetvalue);
//                }
//                #endregion

//                #region tblRewardItems
//                strTableName = "tblRewardItems";
//                dtMaster = clsMasterDB.ListAsDataTable(strTableName, dteStartSyncDateTime, dteEndSyncDateTime);
//                dtLocal = clsLocalDB.ListAsDataTable(strTableName, dteStartSyncDateTime, dteEndSyncDateTime);
//                RewardItems clsRewardItems = new RewardItems(clsLocalConnection.Connection, clsLocalConnection.Transaction);

//                // update all records that are changed [inserted/updated]
//                foreach (DataRow dr in dtMaster.Rows)
//                {
//                    //DataRow[] drx = dtLocal.Select("Configname = '" + dr["ConfigName"].ToString() + "' ");
//                    if (!DateTime.TryParse(dr["CreatedOn"].ToString(), out dteCreatedOn)) dteCreatedOn = Constants.C_DATE_MIN_VALUE;
//                    if (!DateTime.TryParse(dr["LastModified"].ToString(), out dteLastModified)) dteLastModified = Constants.C_DATE_MIN_VALUE;

//                    clsRewardItems.Save(new RewardItemsDetails
//                    {
//                        RewardItemsID = Int64.Parse(dr["RewardItemsID"].ToString()),
//                        ProductID = Int64.Parse(dr["ProductID"].ToString()),
//                        RewardPoints = decimal.Parse(dr["RewardPoints"].ToString()),
//                        CreatedOn = dteCreatedOn,
//                        LastModified = dteLastModified
//                    });
//                }

//                // delete the records in localDB that are supposed to be deleted
//                if (clsLocalDB.CountOfRecords(strTableName) > clsMasterDB.CountOfRecords(strTableName))
//                {
//                    stRetvalue = clsMasterDB.getAllIDs(strTableName, "RewardItemsID", true);
//                    clsLocalDB.Delete(strTableName, "RewardItemsID", stRetvalue);
//                }
//                #endregion

//                #region tblParkingRates
//                strTableName = "tblParkingRates";
//                dtMaster = clsMasterDB.ListAsDataTable(strTableName, dteStartSyncDateTime, dteEndSyncDateTime);
//                dtLocal = clsLocalDB.ListAsDataTable(strTableName, dteStartSyncDateTime, dteEndSyncDateTime);
//                ParkingRates clsParkingRates = new ParkingRates(clsLocalConnection.Connection, clsLocalConnection.Transaction);

//                // update all records that are changed [inserted/updated]
//                foreach (DataRow dr in dtMaster.Rows)
//                {
//                    //DataRow[] drx = dtLocal.Select("Configname = '" + dr["ConfigName"].ToString() + "' ");
//                    if (!DateTime.TryParse(dr["CreatedOn"].ToString(), out dteCreatedOn)) dteCreatedOn = Constants.C_DATE_MIN_VALUE;
//                    if (!DateTime.TryParse(dr["LastModified"].ToString(), out dteLastModified)) dteLastModified = Constants.C_DATE_MIN_VALUE;

//                    clsParkingRates.Save(new ParkingRateDetails
//                    {
//                        ParkingRateID = Int64.Parse(dr["ParkingRateID"].ToString()),
//                        ProductID = Int64.Parse(dr["ProductID"].ToString()),
//                        DayOfWeek = dr["DayOfWeek"].ToString(),
//                        StartTime = dr["StartTime"].ToString(),
//                        EndTime = dr["Endtime"].ToString(),
//                        NoOfUnitperMin = Int32.Parse(dr["NoOfUnitPerMin"].ToString()),
//                        PerUnitPrice = decimal.Parse(dr["PerUnitPrice"].ToString()),
//                        MinimumStayInMin = Int32.Parse(dr["MinimumStayInMin"].ToString()),
//                        MinimumStayPrice = decimal.Parse(dr["MinimumStayPrice"].ToString()),
//                        CreatedByName = dr["CreatedByName"].ToString(),
//                        LastUpdatedByName = dr["LastUpdatedByName"].ToString(),

//                        CreatedOn = dteCreatedOn,
//                        LastModified = dteLastModified
//                    });
//                }

//                // delete the records in localDB that are supposed to be deleted
//                if (clsLocalDB.CountOfRecords(strTableName) > clsMasterDB.CountOfRecords(strTableName))
//                {
//                    stRetvalue = clsMasterDB.getAllIDs(strTableName, "ParkingRateID", true);
//                    clsLocalDB.Delete(strTableName, "ParkingRateID", stRetvalue);
//                }
//                #endregion

//                /*****************************Chart Of Accounts**************************/

//                #region tblAccountClassification
//                strTableName = "tblAccountClassification";
//                dtMaster = clsMasterDB.ListAsDataTable(strTableName, dteStartSyncDateTime, dteEndSyncDateTime);
//                dtLocal = clsLocalDB.ListAsDataTable(strTableName, dteStartSyncDateTime, dteEndSyncDateTime);
//                AccountClassifications clsAccountClassifications = new AccountClassifications(clsLocalConnection.Connection, clsLocalConnection.Transaction);

//                // update all records that are changed [inserted/updated]
//                foreach (DataRow dr in dtMaster.Rows)
//                {
//                    //DataRow[] drx = dtLocal.Select("Configname = '" + dr["ConfigName"].ToString() + "' ");
//                    if (!DateTime.TryParse(dr["CreatedOn"].ToString(), out dteCreatedOn)) dteCreatedOn = Constants.C_DATE_MIN_VALUE;
//                    if (!DateTime.TryParse(dr["LastModified"].ToString(), out dteLastModified)) dteLastModified = Constants.C_DATE_MIN_VALUE;

//                    clsAccountClassifications.Save(new AccountClassificationDetails
//                    {
//                        AccountClassificationID = Int32.Parse(dr["AccountClassificationID"].ToString()),
//                        AccountClassificationCode = dr["AccountClassificationCode"].ToString(),
//                        AccountClassificationName = dr["AccountClassificationName"].ToString(),
//                        AccountClassificationType = (AccountClassificationType)Enum.Parse(typeof(AccountClassificationType), dr["AccountClassificationType"].ToString()),
//                        CreatedOn = dteCreatedOn,
//                        LastModified = dteLastModified
//                    });
//                }

//                // delete the records in localDB that are supposed to be deleted
//                if (clsLocalDB.CountOfRecords(strTableName) > clsMasterDB.CountOfRecords(strTableName))
//                {
//                    stRetvalue = clsMasterDB.getAllIDs(strTableName, "AccountClassificationID", true);
//                    clsLocalDB.Delete(strTableName, "AccountClassificationID", stRetvalue);
//                }
//                #endregion

//                #region tblAccountSummary
//                strTableName = "tblAccountSummary";
//                dtMaster = clsMasterDB.ListAsDataTable(strTableName, dteStartSyncDateTime, dteEndSyncDateTime);
//                dtLocal = clsLocalDB.ListAsDataTable(strTableName, dteStartSyncDateTime, dteEndSyncDateTime);
//                AccountSummaries clsAccountSummaries = new AccountSummaries(clsLocalConnection.Connection, clsLocalConnection.Transaction);

//                // update all records that are changed [inserted/updated]
//                foreach (DataRow dr in dtMaster.Rows)
//                {
//                    //DataRow[] drx = dtLocal.Select("Configname = '" + dr["ConfigName"].ToString() + "' ");
//                    if (!DateTime.TryParse(dr["CreatedOn"].ToString(), out dteCreatedOn)) dteCreatedOn = Constants.C_DATE_MIN_VALUE;
//                    if (!DateTime.TryParse(dr["LastModified"].ToString(), out dteLastModified)) dteLastModified = Constants.C_DATE_MIN_VALUE;

//                    clsAccountSummaries.Save(new AccountSummaryDetails
//                    {
//                        AccountSummaryID = Int32.Parse(dr["AccountSummaryID"].ToString()),
//                        AccountClassificationDetails = new AccountClassificationDetails
//                        {
//                            AccountClassificationID = Int16.Parse(dr["AccountClassificationID"].ToString())
//                        },
//                        AccountSummaryCode = dr["AccountSummaryCode"].ToString(),
//                        AccountSummaryName = dr["AccountSummaryName"].ToString(),
//                        CreatedOn = dteCreatedOn,
//                        LastModified = dteLastModified
//                    });
//                }

//                // delete the records in localDB that are supposed to be deleted
//                if (clsLocalDB.CountOfRecords(strTableName) > clsMasterDB.CountOfRecords(strTableName))
//                {
//                    stRetvalue = clsMasterDB.getAllIDs(strTableName, "AccountSummaryID", true);
//                    clsLocalDB.Delete(strTableName, "AccountSummaryID", stRetvalue);
//                }
//                #endregion

//                #region tblAccountCategory
//                strTableName = "tblAccountCategory";
//                dtMaster = clsMasterDB.ListAsDataTable(strTableName, dteStartSyncDateTime, dteEndSyncDateTime);
//                dtLocal = clsLocalDB.ListAsDataTable(strTableName, dteStartSyncDateTime, dteEndSyncDateTime);
//                AccountCategories clsAccountCategories = new AccountCategories(clsLocalConnection.Connection, clsLocalConnection.Transaction);

//                // update all records that are changed [inserted/updated]
//                foreach (DataRow dr in dtMaster.Rows)
//                {
//                    //DataRow[] drx = dtLocal.Select("Configname = '" + dr["ConfigName"].ToString() + "' ");
//                    if (!DateTime.TryParse(dr["CreatedOn"].ToString(), out dteCreatedOn)) dteCreatedOn = Constants.C_DATE_MIN_VALUE;
//                    if (!DateTime.TryParse(dr["LastModified"].ToString(), out dteLastModified)) dteLastModified = Constants.C_DATE_MIN_VALUE;

//                    clsAccountCategories.Save(new AccountCategoryDetails
//                    {
//                        AccountCategoryID = Int32.Parse(dr["AccountCategoryID"].ToString()),
//                        AccountSummaryDetails = new AccountSummaryDetails
//                        {
//                            AccountSummaryID = Int32.Parse(dr["AccountSummaryID"].ToString())
//                        },
//                        AccountCategoryCode = dr["AccountCategoryCode"].ToString(),
//                        AccountCategoryName = dr["AccountCategoryName"].ToString(),
//                        CreatedOn = dteCreatedOn,
//                        LastModified = dteLastModified
//                    });
//                }

//                // delete the records in localDB that are supposed to be deleted
//                if (clsLocalDB.CountOfRecords(strTableName) > clsMasterDB.CountOfRecords(strTableName))
//                {
//                    stRetvalue = clsMasterDB.getAllIDs(strTableName, "AccountCategoryID", true);
//                    clsLocalDB.Delete(strTableName, "AccountCategoryID", stRetvalue);
//                }
//                #endregion

//                #region tblChartOfAccount
//                strTableName = "tblChartOfAccount";
//                dtMaster = clsMasterDB.ListAsDataTable(strTableName, dteStartSyncDateTime, dteEndSyncDateTime);
//                dtLocal = clsLocalDB.ListAsDataTable(strTableName, dteStartSyncDateTime, dteEndSyncDateTime);
//                ChartOfAccounts clsChartOfAccounts = new ChartOfAccounts(clsLocalConnection.Connection, clsLocalConnection.Transaction);

//                // update all records that are changed [inserted/updated]
//                foreach (DataRow dr in dtMaster.Rows)
//                {
//                    //DataRow[] drx = dtLocal.Select("Configname = '" + dr["ConfigName"].ToString() + "' ");
//                    if (!DateTime.TryParse(dr["CreatedOn"].ToString(), out dteCreatedOn)) dteCreatedOn = Constants.C_DATE_MIN_VALUE;
//                    if (!DateTime.TryParse(dr["LastModified"].ToString(), out dteLastModified)) dteLastModified = Constants.C_DATE_MIN_VALUE;

//                    clsChartOfAccounts.Save(new ChartOfAccountDetails
//                    {
//                        ChartOfAccountID = Int32.Parse(dr["ChartOfAccountID"].ToString()),
//                        AccountCategoryDetails = new AccountCategoryDetails
//                        {
//                            AccountCategoryID = Int32.Parse(dr["AccountCategoryID"].ToString())
//                        },
//                        ChartOfAccountCode = dr["ChartOfAccountCode"].ToString(),
//                        ChartOfAccountName = dr["ChartOfAccountName"].ToString(),
//                        Debit = decimal.Parse(dr["Debit"].ToString()),
//                        Credit = decimal.Parse(dr["Credit"].ToString()),
//                        CreatedOn = dteCreatedOn,
//                        LastModified = dteLastModified
//                    });
//                }

//                // delete the records in localDB that are supposed to be deleted
//                if (clsLocalDB.CountOfRecords(strTableName) > clsMasterDB.CountOfRecords(strTableName))
//                {
//                    stRetvalue = clsMasterDB.getAllIDs(strTableName, "ChartOfAccountID", true);
//                    clsLocalDB.Delete(strTableName, "ChartOfAccountID", stRetvalue);
//                }
//                #endregion

//                #region tblBank
//                strTableName = "tblBank";
//                dtMaster = clsMasterDB.ListAsDataTable(strTableName, dteStartSyncDateTime, dteEndSyncDateTime);
//                dtLocal = clsLocalDB.ListAsDataTable(strTableName, dteStartSyncDateTime, dteEndSyncDateTime);
//                Banks clsBanks = new Banks(clsLocalConnection.Connection, clsLocalConnection.Transaction);

//                // update all records that are changed [inserted/updated]
//                foreach (DataRow dr in dtMaster.Rows)
//                {
//                    //DataRow[] drx = dtLocal.Select("Configname = '" + dr["ConfigName"].ToString() + "' ");
//                    if (!DateTime.TryParse(dr["CreatedOn"].ToString(), out dteCreatedOn)) dteCreatedOn = Constants.C_DATE_MIN_VALUE;
//                    if (!DateTime.TryParse(dr["LastModified"].ToString(), out dteLastModified)) dteLastModified = Constants.C_DATE_MIN_VALUE;

//                    clsBanks.Save(new BankDetails
//                    {
//                        BankID = Int32.Parse(dr["BankID"].ToString()),
//                        BankCode = dr["BankCode"].ToString(),
//                        BankName = dr["BankName"].ToString(),
//                        ChequeCode = dr["ChequeCode"].ToString(),
//                        ChequeCounter = dr["ChequeCounter"].ToString(),
//                        CreatedOn = dteCreatedOn,
//                        LastModified = dteLastModified
//                    });
//                }

//                // delete the records in localDB that are supposed to be deleted
//                if (clsLocalDB.CountOfRecords(strTableName) > clsMasterDB.CountOfRecords(strTableName))
//                {
//                    stRetvalue = clsMasterDB.getAllIDs(strTableName, "BankID", true);
//                    clsLocalDB.Delete(strTableName, "BankID", stRetvalue);
//                }
//                #endregion

//                /*****************************Transactions**************************/
//                // do not delete the records in MasterDB, always INSERT or update using SyncID

//                #region tblCashierLogs
//                strTableName = "tblCashierLogs";
//                dtMaster = clsMasterDB.ListAsDataTable(strTableName, dteStartSyncDateTime, dteEndSyncDateTime);
//                dtLocal = clsLocalDB.ListAsDataTable(strTableName, dteStartSyncDateTime, dteEndSyncDateTime);
//                CashierLogs clsCashierLogs = new CashierLogs(clsMasterConnection.Connection, clsMasterConnection.Transaction);

//                // update all records that are changed [inserted/updated]
//                foreach (DataRow dr in dtLocal.Rows)
//                {
//                    //DataRow[] drx = dtLocal.Select("Configname = '" + dr["ConfigName"].ToString() + "' ");
//                    if (!DateTime.TryParse(dr["CreatedOn"].ToString(), out dteCreatedOn)) dteCreatedOn = Constants.C_DATE_MIN_VALUE;
//                    if (!DateTime.TryParse(dr["LastModified"].ToString(), out dteLastModified)) dteLastModified = Constants.C_DATE_MIN_VALUE;

//                    clsCashierLogs.Save(new CashierLogsDetails
//                    {
//                        BranchID = Int32.Parse(dr["BranchID"].ToString()),
//                        TerminalNo = dr["TerminalNo"].ToString(),
//                        SyncID = Int64.Parse(dr["SyncID"].ToString()),
//                        CashierID = Int64.Parse(dr["UID"].ToString()),
//                        LoginDate = DateTime.Parse(dr["LoginDate"].ToString()),
//                        BranchCode = dr["BranchCode"].ToString(),
//                        IPAddress = dr["IPAddress"].ToString(),
//                        LogoutDate = DateTime.Parse(dr["LogoutDate"].ToString()),
//                        Status = (CashierLogStatus)Enum.Parse(typeof(CashierLogStatus), dr["Status"].ToString()),
//                        CreatedOn = dteCreatedOn,
//                        LastModified = dteLastModified
//                    });
//                }
//                #endregion

//                #region tblCashierReport
//                strTableName = "tblCashierReport";
//                dtMaster = clsMasterDB.ListAsDataTable(strTableName, dteStartSyncDateTime, dteEndSyncDateTime);
//                dtLocal = clsLocalDB.ListAsDataTable(strTableName, dteStartSyncDateTime, dteEndSyncDateTime);
//                CashierReports clsCashierReport = new CashierReports(clsMasterConnection.Connection, clsMasterConnection.Transaction);

//                // update all records that are changed [inserted/updated]
//                foreach (DataRow dr in dtLocal.Rows)
//                {
//                    //DataRow[] drx = dtLocal.Select("Configname = '" + dr["ConfigName"].ToString() + "' ");
//                    if (!DateTime.TryParse(dr["CreatedOn"].ToString(), out dteCreatedOn)) dteCreatedOn = Constants.C_DATE_MIN_VALUE;
//                    if (!DateTime.TryParse(dr["LastModified"].ToString(), out dteLastModified)) dteLastModified = Constants.C_DATE_MIN_VALUE;

//                    clsCashierReport.Save(new CashierReportDetails
//                    {
//                        BranchID = Int32.Parse(dr["BranchID"].ToString()),
//                        TerminalNo = dr["TerminalNo"].ToString(),
//                        SyncID = Int64.Parse(dr["SyncID"].ToString()),
//                        CashierReportID = Int64.Parse(dr["CashierReportID"].ToString()),
//                        CashierID = Int64.Parse(dr["CashierID"].ToString()),
//                        TerminalID = Int32.Parse(dr["TerminalID"].ToString()),
//                        NetSales = decimal.Parse(dr["NetSales"].ToString()),
//                        GrossSales = decimal.Parse(dr["GrossSales"].ToString()),
//                        TotalDiscount = decimal.Parse(dr["TotalDiscount"].ToString()),
//                        SNRDiscount = decimal.Parse(dr["SNRDiscount"].ToString()),
//                        PWDDiscount = decimal.Parse(dr["PWDDiscount"].ToString()),
//                        OtherDiscount = decimal.Parse(dr["OtherDiscount"].ToString()),
//                        DailySales = decimal.Parse(dr["DailySales"].ToString()),
//                        QuantitySold = decimal.Parse(dr["QuantitySold"].ToString()),
//                        GroupSales = decimal.Parse(dr["GroupSales"].ToString()),
//                        VAT = decimal.Parse(dr["VAT"].ToString()),
//                        EVAT = decimal.Parse(dr["EVAT"].ToString()),
//                        LocalTax = decimal.Parse(dr["LocalTax"].ToString()),
//                        CashSales = decimal.Parse(dr["CashSales"].ToString()),
//                        ChequeSales = decimal.Parse(dr["ChequeSales"].ToString()),
//                        CreditCardSales = decimal.Parse(dr["CreditCardSales"].ToString()),
//                        CreditSales = decimal.Parse(dr["CreditSales"].ToString()),
//                        CreditPayment = decimal.Parse(dr["CreditPayment"].ToString()),
//                        CashInDrawer = decimal.Parse(dr["CashInDrawer"].ToString()),
//                        TotalDisburse = decimal.Parse(dr["TotalDisburse"].ToString()),
//                        CashDisburse = decimal.Parse(dr["CashDisburse"].ToString()),
//                        ChequeDisburse = decimal.Parse(dr["ChequeDisburse"].ToString()),
//                        CreditCardDisburse = decimal.Parse(dr["CreditCardDisburse"].ToString()),
//                        TotalWithHold = decimal.Parse(dr["TotalWithhold"].ToString()),
//                        CashWithHold = decimal.Parse(dr["CashWithhold"].ToString()),
//                        ChequeWithHold = decimal.Parse(dr["ChequeWithhold"].ToString()),
//                        CreditCardWithHold = decimal.Parse(dr["CreditCardWithhold"].ToString()),
//                        TotalPaidOut = decimal.Parse(dr["TotalPaidOut"].ToString()),
//                        CashPaidOut = decimal.Parse(dr["CashPaidOut"].ToString()),
//                        ChequePaidOut = decimal.Parse(dr["ChequePaidOut"].ToString()),
//                        CreditCardPaidOut = decimal.Parse(dr["CreditCardPaidOut"].ToString()),
//                        BeginningBalance = decimal.Parse(dr["BeginningBalance"].ToString()),
//                        VoidSales = decimal.Parse(dr["VoidSales"].ToString()),
//                        RefundSales = decimal.Parse(dr["RefundSales"].ToString()),
//                        ItemsDiscount = decimal.Parse(dr["ItemsDiscount"].ToString()),
//                        SubTotalDiscount = decimal.Parse(dr["SubtotalDiscount"].ToString()),
//                        NoOfCashTransactions = Int32.Parse(dr["NoOfCashTransactions"].ToString()),
//                        NoOfChequeTransactions = Int32.Parse(dr["NoOfChequeTransactions"].ToString()),
//                        NoOfCreditCardTransactions = Int32.Parse(dr["NoOfCreditCardTransactions"].ToString()),
//                        NoOfCreditTransactions = Int32.Parse(dr["NoOfCreditTransactions"].ToString()),
//                        NoOfCombinationPaymentTransactions = Int32.Parse(dr["NoOfCombinationPaymentTransactions"].ToString()),
//                        NoOfCreditPaymentTransactions = Int32.Parse(dr["NoOfCreditPaymentTransactions"].ToString()),
//                        NoOfClosedTransactions = Int32.Parse(dr["NoOfClosedTransactions"].ToString()),
//                        NoOfRefundTransactions = Int32.Parse(dr["NoOfRefundTransactions"].ToString()),
//                        NoOfVoidTransactions = Int32.Parse(dr["NoOfVoidTransactions"].ToString()),
//                        NoOfTotalTransactions = Int32.Parse(dr["NoOfTotalTransactions"].ToString()),
//                        CashCount = decimal.Parse(dr["CashCount"].ToString()),
//                        LastLoginDate = DateTime.Parse(dr["LastLoginDate"].ToString()),
//                        TotalDeposit = decimal.Parse(dr["TotalDeposit"].ToString()),
//                        CashDeposit = decimal.Parse(dr["CashDeposit"].ToString()),
//                        ChequeDeposit = decimal.Parse(dr["ChequeDeposit"].ToString()),
//                        CreditCardDeposit = decimal.Parse(dr["CreditCardDeposit"].ToString()),
//                        DebitPayment = decimal.Parse(dr["DebitPayment"].ToString()),
//                        NoOfDebitPaymentTransactions = Int32.Parse(dr["NoOfDebitPaymentTransactions"].ToString()),
//                        TotalCharge = decimal.Parse(dr["TotalCharge"].ToString()),
//                        IsCashCountInitialized = bool.Parse(dr["IsCashCountInitialized"].ToString()),
//                        NoOfDiscountedTransactions = Int32.Parse(dr["NoOfDiscountedTransactions"].ToString()),
//                        NegativeAdjustments = decimal.Parse(dr["NegativeAdjustments"].ToString()),
//                        NoOfNegativeAdjustmentTransactions = Int32.Parse(dr["NoOfNegativeAdjustmentTransactions"].ToString()),
//                        PromotionalItems = decimal.Parse(dr["PromotionalItems"].ToString()),
//                        CreditSalesTax = decimal.Parse(dr["CreditSalesTax"].ToString()),
//                        DebitDeposit = decimal.Parse(dr["DebitDeposit"].ToString()),
//                        RewardPointsPayment = decimal.Parse(dr["RewardPointsPayment"].ToString()),
//                        RewardConvertedPayment = decimal.Parse(dr["RewardConvertedPayment"].ToString()),
//                        NoOfRewardPointsPayment = Int32.Parse(dr["NoOfRewardPointsPayment"].ToString()),
//                        CreditPaymentCash = decimal.Parse(dr["CreditPaymentCash"].ToString()),
//                        CreditPaymentCheque = decimal.Parse(dr["CreditPaymentCheque"].ToString()),
//                        CreditPaymentCreditCard = decimal.Parse(dr["CreditPaymentCreditCard"].ToString()),
//                        CreditPaymentDebit = decimal.Parse(dr["CreditPaymentDebit"].ToString()),
//                        RefundCash = decimal.Parse(dr["RefundCash"].ToString()),
//                        RefundCheque = decimal.Parse(dr["RefundCheque"].ToString()),
//                        RefundCreditCard = decimal.Parse(dr["RefundCreditCard"].ToString()),
//                        RefundCredit = decimal.Parse(dr["RefundCredit"].ToString()),
//                        RefundDebit = decimal.Parse(dr["RefundDebit"].ToString()),
//                        CreatedOn = dteCreatedOn,
//                        LastModified = dteLastModified
//                    });
//                }
//                #endregion

//                #region tblCashierReportHistory
//                strTableName = "tblCashierReportHistory";
//                dtMaster = clsMasterDB.ListAsDataTable(strTableName, dteStartSyncDateTime, dteEndSyncDateTime);
//                dtLocal = clsLocalDB.ListAsDataTable(strTableName, dteStartSyncDateTime, dteEndSyncDateTime);
//                CashierReportHistories clsCashierReportHistories = new CashierReportHistories(clsMasterConnection.Connection, clsMasterConnection.Transaction);

//                // update all records that are changed [inserted/updated]
//                foreach (DataRow dr in dtLocal.Rows)
//                {
//                    //DataRow[] drx = dtLocal.Select("Configname = '" + dr["ConfigName"].ToString() + "' ");
//                    if (!DateTime.TryParse(dr["CreatedOn"].ToString(), out dteCreatedOn)) dteCreatedOn = Constants.C_DATE_MIN_VALUE;
//                    if (!DateTime.TryParse(dr["LastModified"].ToString(), out dteLastModified)) dteLastModified = Constants.C_DATE_MIN_VALUE;

//                    clsCashierReportHistories.Save(new CashierReportDetails
//                    {
//                        BranchID = Int32.Parse(dr["BranchID"].ToString()),
//                        TerminalNo = dr["TerminalNo"].ToString(),
//                        SyncID = Int64.Parse(dr["SyncID"].ToString()),
//                        CashierReportHistoryID = Int64.Parse(dr["CashierReportHistoryID"].ToString()),
//                        CashierID = Int64.Parse(dr["CashierID"].ToString()),
//                        TerminalID = Int32.Parse(dr["TerminalID"].ToString()),
//                        NetSales = decimal.Parse(dr["NetSales"].ToString()),
//                        GrossSales = decimal.Parse(dr["GrossSales"].ToString()),
//                        TotalDiscount = decimal.Parse(dr["TotalDiscount"].ToString()),
//                        SNRDiscount = decimal.Parse(dr["SNRDiscount"].ToString()),
//                        PWDDiscount = decimal.Parse(dr["PWDDiscount"].ToString()),
//                        OtherDiscount = decimal.Parse(dr["OtherDiscount"].ToString()),
//                        DailySales = decimal.Parse(dr["DailySales"].ToString()),
//                        QuantitySold = decimal.Parse(dr["QuantitySold"].ToString()),
//                        GroupSales = decimal.Parse(dr["GroupSales"].ToString()),
//                        VAT = decimal.Parse(dr["VAT"].ToString()),
//                        EVAT = decimal.Parse(dr["EVAT"].ToString()),
//                        LocalTax = decimal.Parse(dr["LocalTax"].ToString()),
//                        CashSales = decimal.Parse(dr["CashSales"].ToString()),
//                        ChequeSales = decimal.Parse(dr["ChequeSales"].ToString()),
//                        CreditCardSales = decimal.Parse(dr["CreditCardSales"].ToString()),
//                        CreditSales = decimal.Parse(dr["CreditSales"].ToString()),
//                        CreditPayment = decimal.Parse(dr["CreditPayment"].ToString()),
//                        CashInDrawer = decimal.Parse(dr["CashInDrawer"].ToString()),
//                        TotalDisburse = decimal.Parse(dr["TotalDisburse"].ToString()),
//                        CashDisburse = decimal.Parse(dr["CashDisburse"].ToString()),
//                        ChequeDisburse = decimal.Parse(dr["ChequeDisburse"].ToString()),
//                        CreditCardDisburse = decimal.Parse(dr["CreditCardDisburse"].ToString()),
//                        TotalWithHold = decimal.Parse(dr["TotalWithhold"].ToString()),
//                        CashWithHold = decimal.Parse(dr["CashWithhold"].ToString()),
//                        ChequeWithHold = decimal.Parse(dr["ChequeWithhold"].ToString()),
//                        CreditCardWithHold = decimal.Parse(dr["CreditCardWithhold"].ToString()),
//                        TotalPaidOut = decimal.Parse(dr["TotalPaidOut"].ToString()),
//                        CashPaidOut = decimal.Parse(dr["CashPaidOut"].ToString()),
//                        ChequePaidOut = decimal.Parse(dr["ChequePaidOut"].ToString()),
//                        CreditCardPaidOut = decimal.Parse(dr["CreditCardPaidOut"].ToString()),
//                        BeginningBalance = decimal.Parse(dr["BeginningBalance"].ToString()),
//                        VoidSales = decimal.Parse(dr["VoidSales"].ToString()),
//                        RefundSales = decimal.Parse(dr["RefundSales"].ToString()),
//                        ItemsDiscount = decimal.Parse(dr["ItemsDiscount"].ToString()),
//                        SubTotalDiscount = decimal.Parse(dr["SubtotalDiscount"].ToString()),
//                        NoOfCashTransactions = Int32.Parse(dr["NoOfCashTransactions"].ToString()),
//                        NoOfChequeTransactions = Int32.Parse(dr["NoOfChequeTransactions"].ToString()),
//                        NoOfCreditCardTransactions = Int32.Parse(dr["NoOfCreditCardTransactions"].ToString()),
//                        NoOfCreditTransactions = Int32.Parse(dr["NoOfCreditTransactions"].ToString()),
//                        NoOfCombinationPaymentTransactions = Int32.Parse(dr["NoOfCombinationPaymentTransactions"].ToString()),
//                        NoOfCreditPaymentTransactions = Int32.Parse(dr["NoOfCreditPaymentTransactions"].ToString()),
//                        NoOfClosedTransactions = Int32.Parse(dr["NoOfClosedTransactions"].ToString()),
//                        NoOfRefundTransactions = Int32.Parse(dr["NoOfRefundTransactions"].ToString()),
//                        NoOfVoidTransactions = Int32.Parse(dr["NoOfVoidTransactions"].ToString()),
//                        NoOfTotalTransactions = Int32.Parse(dr["NoOfTotalTransactions"].ToString()),
//                        CashCount = decimal.Parse(dr["CashCount"].ToString()),
//                        LastLoginDate = DateTime.Parse(dr["LastLoginDate"].ToString()),
//                        TotalDeposit = decimal.Parse(dr["TotalDeposit"].ToString()),
//                        CashDeposit = decimal.Parse(dr["CashDeposit"].ToString()),
//                        ChequeDeposit = decimal.Parse(dr["ChequeDeposit"].ToString()),
//                        CreditCardDeposit = decimal.Parse(dr["CreditCardDeposit"].ToString()),
//                        DebitPayment = decimal.Parse(dr["DebitPayment"].ToString()),
//                        NoOfDebitPaymentTransactions = Int32.Parse(dr["NoOfDebitPaymentTransactions"].ToString()),
//                        TotalCharge = decimal.Parse(dr["TotalCharge"].ToString()),
//                        IsCashCountInitialized = bool.Parse(dr["IsCashCountInitialized"].ToString()),
//                        NoOfDiscountedTransactions = Int32.Parse(dr["NoOfDiscountedTransactions"].ToString()),
//                        NegativeAdjustments = decimal.Parse(dr["NegativeAdjustments"].ToString()),
//                        NoOfNegativeAdjustmentTransactions = Int32.Parse(dr["NoOfNegativeAdjustmentTransactions"].ToString()),
//                        PromotionalItems = decimal.Parse(dr["PromotionalItems"].ToString()),
//                        CreditSalesTax = decimal.Parse(dr["CreditSalesTax"].ToString()),
//                        DebitDeposit = decimal.Parse(dr["DebitDeposit"].ToString()),
//                        RewardPointsPayment = decimal.Parse(dr["RewardPointsPayment"].ToString()),
//                        RewardConvertedPayment = decimal.Parse(dr["RewardConvertedPayment"].ToString()),
//                        NoOfRewardPointsPayment = Int32.Parse(dr["NoOfRewardPointsPayment"].ToString()),
//                        CreditPaymentCash = decimal.Parse(dr["CreditPaymentCash"].ToString()),
//                        CreditPaymentCheque = decimal.Parse(dr["CreditPaymentCheque"].ToString()),
//                        CreditPaymentCreditCard = decimal.Parse(dr["CreditPaymentCreditCard"].ToString()),
//                        CreditPaymentDebit = decimal.Parse(dr["CreditPaymentDebit"].ToString()),
//                        RefundCash = decimal.Parse(dr["RefundCash"].ToString()),
//                        RefundCheque = decimal.Parse(dr["RefundCheque"].ToString()),
//                        RefundCreditCard = decimal.Parse(dr["RefundCreditCard"].ToString()),
//                        RefundCredit = decimal.Parse(dr["RefundCredit"].ToString()),
//                        RefundDebit = decimal.Parse(dr["RefundDebit"].ToString()),
//                        CreatedOn = dteCreatedOn,
//                        LastModified = dteLastModified
//                    });
//                }
//                #endregion

//                #region tblDeposit
//                strTableName = "tblDeposit";
//                dtMaster = clsMasterDB.ListAsDataTable(strTableName, dteStartSyncDateTime, dteEndSyncDateTime);
//                dtLocal = clsLocalDB.ListAsDataTable(strTableName, dteStartSyncDateTime, dteEndSyncDateTime);
//                Deposits clsDeposit = new Deposits(clsMasterConnection.Connection, clsMasterConnection.Transaction);

//                // update all records that are changed [inserted/updated]
//                foreach (DataRow dr in dtLocal.Rows)
//                {
//                    //DataRow[] drx = dtLocal.Select("Configname = '" + dr["ConfigName"].ToString() + "' ");
//                    if (!DateTime.TryParse(dr["CreatedOn"].ToString(), out dteCreatedOn)) dteCreatedOn = Constants.C_DATE_MIN_VALUE;
//                    if (!DateTime.TryParse(dr["LastModified"].ToString(), out dteLastModified)) dteLastModified = Constants.C_DATE_MIN_VALUE;

//                    clsDeposit.Save(new DepositDetails
//                    {
//                        BranchDetails = new BranchDetails
//                        {
//                            BranchID = Int32.Parse(dr["BranchID"].ToString()),
//                            BranchCode = dr["BranchCode"].ToString()
//                        },
//                        TerminalNo = dr["TerminalNo"].ToString(),
//                        SyncID = Int64.Parse(dr["SyncID"].ToString()),
//                        DepositID = Int64.Parse(dr["DepositID"].ToString()),
//                        Amount = decimal.Parse(dr["Amount"].ToString()),
//                        PaymentType = (PaymentTypes)Enum.Parse(typeof(PaymentTypes), dr["PaymentType"].ToString()),
//                        DateCreated = DateTime.Parse(dr["DateCreated"].ToString()),
//                        CashierID = Int64.Parse(dr["CashierID"].ToString()),
//                        ContactID = Int64.Parse(dr["ContactID"].ToString()),
//                        Remarks = dr["Remarks"].ToString(),
//                        CreatedOn = dteCreatedOn,
//                        LastModified = dteLastModified
//                    });
//                }
//                #endregion

//                #region tblDisburse
//                strTableName = "tblDisburse";
//                dtMaster = clsMasterDB.ListAsDataTable(strTableName, dteStartSyncDateTime, dteEndSyncDateTime);
//                dtLocal = clsLocalDB.ListAsDataTable(strTableName, dteStartSyncDateTime, dteEndSyncDateTime);
//                Disburses clsDisburse = new Disburses(clsMasterConnection.Connection, clsMasterConnection.Transaction);

//                // update all records that are changed [inserted/updated]
//                foreach (DataRow dr in dtLocal.Rows)
//                {
//                    //DataRow[] drx = dtLocal.Select("Configname = '" + dr["ConfigName"].ToString() + "' ");
//                    if (!DateTime.TryParse(dr["CreatedOn"].ToString(), out dteCreatedOn)) dteCreatedOn = Constants.C_DATE_MIN_VALUE;
//                    if (!DateTime.TryParse(dr["LastModified"].ToString(), out dteLastModified)) dteLastModified = Constants.C_DATE_MIN_VALUE;

//                    clsDisburse.Save(new DisburseDetails
//                    {
//                        BranchDetails = new BranchDetails
//                        {
//                            BranchID = Int32.Parse(dr["BranchID"].ToString()),
//                            BranchCode = dr["BranchCode"].ToString()
//                        },
//                        TerminalNo = dr["TerminalNo"].ToString(),
//                        SyncID = Int64.Parse(dr["SyncID"].ToString()),
//                        DisburseID = Int64.Parse(dr["DisburseID"].ToString()),
//                        Amount = decimal.Parse(dr["Amount"].ToString()),
//                        PaymentType = (PaymentTypes)Enum.Parse(typeof(PaymentTypes), dr["PaymentType"].ToString()),
//                        DateCreated = DateTime.Parse(dr["DateCreated"].ToString()),
//                        CashierID = Int64.Parse(dr["CashierID"].ToString()),
//                        Remarks = dr["Remarks"].ToString(),
//                        CreatedOn = dteCreatedOn,
//                        LastModified = dteLastModified
//                    });
//                }
//                #endregion

//                #region tblPaidOut
//                strTableName = "tblPaidOut";
//                dtMaster = clsMasterDB.ListAsDataTable(strTableName, dteStartSyncDateTime, dteEndSyncDateTime);
//                dtLocal = clsLocalDB.ListAsDataTable(strTableName, dteStartSyncDateTime, dteEndSyncDateTime);
//                PaidOut clsPaidOut = new PaidOut(clsMasterConnection.Connection, clsMasterConnection.Transaction);

//                // update all records that are changed [inserted/updated]
//                foreach (DataRow dr in dtLocal.Rows)
//                {
//                    //DataRow[] drx = dtLocal.Select("Configname = '" + dr["ConfigName"].ToString() + "' ");
//                    if (!DateTime.TryParse(dr["CreatedOn"].ToString(), out dteCreatedOn)) dteCreatedOn = Constants.C_DATE_MIN_VALUE;
//                    if (!DateTime.TryParse(dr["LastModified"].ToString(), out dteLastModified)) dteLastModified = Constants.C_DATE_MIN_VALUE;

//                    clsPaidOut.Save(new PaidOutDetails
//                    {
//                        BranchDetails = new BranchDetails
//                        {
//                            BranchID = Int32.Parse(dr["BranchID"].ToString()),
//                            BranchCode = dr["BranchCode"].ToString()
//                        },
//                        TerminalNo = dr["TerminalNo"].ToString(),
//                        SyncID = Int64.Parse(dr["SyncID"].ToString()),
//                        PaidOutID = Int64.Parse(dr["PaidOutID"].ToString()),
//                        Amount = decimal.Parse(dr["Amount"].ToString()),
//                        PaymentType = (PaymentTypes)Enum.Parse(typeof(PaymentTypes), dr["PaymentType"].ToString()),
//                        DateCreated = DateTime.Parse(dr["DateCreated"].ToString()),
//                        CashierID = Int64.Parse(dr["CashierID"].ToString()),
//                        Remarks = dr["Remarks"].ToString(),
//                        CreatedOn = dteCreatedOn,
//                        LastModified = dteLastModified
//                    });
//                }
//                #endregion

//                #region tblWithhold
//                strTableName = "tblWithhold";
//                dtMaster = clsMasterDB.ListAsDataTable(strTableName, dteStartSyncDateTime, dteEndSyncDateTime);
//                dtLocal = clsLocalDB.ListAsDataTable(strTableName, dteStartSyncDateTime, dteEndSyncDateTime);
//                Withhold clsWithHold = new Withhold(clsMasterConnection.Connection, clsMasterConnection.Transaction);

//                // update all records that are changed [inserted/updated]
//                foreach (DataRow dr in dtLocal.Rows)
//                {
//                    //DataRow[] drx = dtLocal.Select("Configname = '" + dr["ConfigName"].ToString() + "' ");
//                    if (!DateTime.TryParse(dr["CreatedOn"].ToString(), out dteCreatedOn)) dteCreatedOn = Constants.C_DATE_MIN_VALUE;
//                    if (!DateTime.TryParse(dr["LastModified"].ToString(), out dteLastModified)) dteLastModified = Constants.C_DATE_MIN_VALUE;

//                    clsWithHold.Save(new WithholdDetails
//                    {
//                        BranchDetails = new BranchDetails
//                        {
//                            BranchID = Int32.Parse(dr["BranchID"].ToString()),
//                            BranchCode = dr["BranchCode"].ToString()
//                        },
//                        TerminalNo = dr["TerminalNo"].ToString(),
//                        SyncID = Int64.Parse(dr["SyncID"].ToString()),
//                        WithHoldID = Int64.Parse(dr["WithHoldID"].ToString()),
//                        Amount = decimal.Parse(dr["Amount"].ToString()),
//                        PaymentType = (PaymentTypes)Enum.Parse(typeof(PaymentTypes), dr["PaymentType"].ToString()),
//                        DateCreated = DateTime.Parse(dr["DateCreated"].ToString()),
//                        CashierID = Int64.Parse(dr["CashierID"].ToString()),
//                        Remarks = dr["Remarks"].ToString(),
//                        CreatedOn = dteCreatedOn,
//                        LastModified = dteLastModified
//                    });
//                }
//                #endregion

//                #region tblCashPayment
//                strTableName = "tblCashPayment";
//                dtMaster = clsMasterDB.ListAsDataTable(strTableName, dteStartSyncDateTime, dteEndSyncDateTime);
//                dtLocal = clsLocalDB.ListAsDataTable(strTableName, dteStartSyncDateTime, dteEndSyncDateTime);
//                CashPayments clsCashPayments = new CashPayments(clsMasterConnection.Connection, clsMasterConnection.Transaction);

//                // update all records that are changed [inserted/updated]
//                foreach (DataRow dr in dtLocal.Rows)
//                {
//                    //DataRow[] drx = dtLocal.Select("Configname = '" + dr["ConfigName"].ToString() + "' ");
//                    if (!DateTime.TryParse(dr["CreatedOn"].ToString(), out dteCreatedOn)) dteCreatedOn = Constants.C_DATE_MIN_VALUE;
//                    if (!DateTime.TryParse(dr["LastModified"].ToString(), out dteLastModified)) dteLastModified = Constants.C_DATE_MIN_VALUE;

//                    clsCashPayments.Save(new CashPaymentDetails
//                    {
//                        BranchDetails = new BranchDetails
//                        {
//                            BranchID = Int32.Parse(dr["BranchID"].ToString()),
//                        },
//                        TerminalNo = dr["TerminalNo"].ToString(),
//                        SyncID = Int64.Parse(dr["SyncID"].ToString()),
//                        CashPaymentID = Int64.Parse(dr["CashPaymentID"].ToString()),
//                        TransactionID = Int64.Parse(dr["TransactionID"].ToString()),
//                        Amount = decimal.Parse(dr["Amount"].ToString()),
//                        Remarks = dr["Remarks"].ToString(),
//                        TransactionNo = dr["TransactionNo"].ToString(),
//                        CreatedOn = dteCreatedOn,
//                        LastModified = dteLastModified
//                    });
//                }
//                #endregion

//                #region tblChequePayment
//                strTableName = "tblChequePayment";
//                dtMaster = clsMasterDB.ListAsDataTable(strTableName, dteStartSyncDateTime, dteEndSyncDateTime);
//                dtLocal = clsLocalDB.ListAsDataTable(strTableName, dteStartSyncDateTime, dteEndSyncDateTime);
//                ChequePayments clsChequePayments = new ChequePayments(clsMasterConnection.Connection, clsMasterConnection.Transaction);

//                // update all records that are changed [inserted/updated]
//                foreach (DataRow dr in dtLocal.Rows)
//                {
//                    //DataRow[] drx = dtLocal.Select("Configname = '" + dr["ConfigName"].ToString() + "' ");
//                    if (!DateTime.TryParse(dr["CreatedOn"].ToString(), out dteCreatedOn)) dteCreatedOn = Constants.C_DATE_MIN_VALUE;
//                    if (!DateTime.TryParse(dr["LastModified"].ToString(), out dteLastModified)) dteLastModified = Constants.C_DATE_MIN_VALUE;

//                    clsChequePayments.Save(new ChequePaymentDetails
//                    {
//                        BranchDetails = new BranchDetails
//                        {
//                            BranchID = Int32.Parse(dr["BranchID"].ToString()),
//                        },
//                        TerminalNo = dr["TerminalNo"].ToString(),
//                        SyncID = Int64.Parse(dr["SyncID"].ToString()),
//                        ChequePaymentID = Int64.Parse(dr["ChequePaymentID"].ToString()),
//                        TransactionID = Int64.Parse(dr["TransactionID"].ToString()),
//                        ChequeNo = dr["ChequeNo"].ToString(),
//                        Amount = decimal.Parse(dr["Amount"].ToString()),
//                        ValidityDate = DateTime.Parse(dr["ValidityDate"].ToString()),
//                        Remarks = dr["Remarks"].ToString(),
//                        TransactionNo = dr["TransactionNo"].ToString(),
//                        CreatedOn = dteCreatedOn,
//                        LastModified = dteLastModified
//                    });
//                }
//                #endregion

//                #region tblCreditCardPayment
//                strTableName = "tblCreditCardPayment";
//                dtMaster = clsMasterDB.ListAsDataTable(strTableName, dteStartSyncDateTime, dteEndSyncDateTime);
//                dtLocal = clsLocalDB.ListAsDataTable(strTableName, dteStartSyncDateTime, dteEndSyncDateTime);
//                CreditCardPayments clsCreditCardPayments = new CreditCardPayments(clsMasterConnection.Connection, clsMasterConnection.Transaction);

//                // update all records that are changed [inserted/updated]
//                foreach (DataRow dr in dtLocal.Rows)
//                {
//                    //DataRow[] drx = dtLocal.Select("Configname = '" + dr["ConfigName"].ToString() + "' ");
//                    if (!DateTime.TryParse(dr["CreatedOn"].ToString(), out dteCreatedOn)) dteCreatedOn = Constants.C_DATE_MIN_VALUE;
//                    if (!DateTime.TryParse(dr["LastModified"].ToString(), out dteLastModified)) dteLastModified = Constants.C_DATE_MIN_VALUE;

//                    clsCreditCardPayments.Save(new CreditCardPaymentDetails
//                    {
//                        BranchDetails = new BranchDetails
//                        {
//                            BranchID = Int32.Parse(dr["BranchID"].ToString()),
//                        },
//                        TerminalNo = dr["TerminalNo"].ToString(),
//                        SyncID = Int64.Parse(dr["SyncID"].ToString()),
//                        CreditCardPaymentID = Int64.Parse(dr["CreditCardPaymentID"].ToString()),
//                        TransactionID = Int64.Parse(dr["TransactionID"].ToString()),
//                        Amount = decimal.Parse(dr["Amount"].ToString()),
//                        CardTypeID = Int16.Parse(dr["CardTypeID"].ToString()),
//                        CardTypeCode = dr["CardTypeCode"].ToString(),
//                        CardTypeName = dr["CardTypeName"].ToString(),
//                        CardNo = dr["CardNo"].ToString(),
//                        CardHolder = dr["CardHolder"].ToString(),
//                        ValidityDates = dr["ValidityDates"].ToString(),
//                        Remarks = dr["Remarks"].ToString(),
//                        TransactionNo = dr["TransactionNo"].ToString(),
//                        CreatedOn = dteCreatedOn,
//                        LastModified = dteLastModified
//                    });
//                }
//                #endregion

//                #region tblCashCount
//                strTableName = "tblCashCount";
//                dtMaster = clsMasterDB.ListAsDataTable(strTableName, dteStartSyncDateTime, dteEndSyncDateTime);
//                dtLocal = clsLocalDB.ListAsDataTable(strTableName, dteStartSyncDateTime, dteEndSyncDateTime);
//                CashCounts clsCashCounts = new CashCounts(clsMasterConnection.Connection, clsMasterConnection.Transaction);

//                // update all records that are changed [inserted/updated]
//                foreach (DataRow dr in dtLocal.Rows)
//                {
//                    //DataRow[] drx = dtLocal.Select("Configname = '" + dr["ConfigName"].ToString() + "' ");
//                    if (!DateTime.TryParse(dr["CreatedOn"].ToString(), out dteCreatedOn)) dteCreatedOn = Constants.C_DATE_MIN_VALUE;
//                    if (!DateTime.TryParse(dr["LastModified"].ToString(), out dteLastModified)) dteLastModified = Constants.C_DATE_MIN_VALUE;

//                    clsCashCounts.Save(new CashCountDetails
//                    {
//                        BranchDetails = new BranchDetails
//                        {
//                            BranchID = Int32.Parse(dr["BranchID"].ToString()),
//                            BranchCode = dr["BranchCode"].ToString(),
//                        },
//                        TerminalNo = dr["TerminalNo"].ToString(),
//                        SyncID = Int64.Parse(dr["SyncID"].ToString()),
//                        CashCountID = Int64.Parse(dr["CashCountID"].ToString()),
//                        CashierID = Int64.Parse(dr["CashierID"].ToString()),
//                        CashierName = dr["CashierName"].ToString(),
//                        DateCreated = DateTime.Parse(dr["DateCreated"].ToString()),
//                        DenominationDetails = new DenominationDetails {
//                            DenominationID = Int32.Parse(dr["DenominationID"].ToString()),
//                            DenominationValue = decimal.Parse(dr["DenominationValue"].ToString())
//                        },
//                        DenominationValue = decimal.Parse(dr["DenominationValue"].ToString()),
//                        DenominationCount = Int32.Parse(dr["DenominationCount"].ToString()),
//                        DenominationAmount = decimal.Parse(dr["DenominationAmount"].ToString()),
//                        CreatedOn = dteCreatedOn,
//                        LastModified = dteLastModified
//                    });
//                }
//                #endregion

//                clsLocalDB.SetForeignKey(true);

//                clsLocalConnection.CommitAndDispose();
//                clsMasterConnection.CommitAndDispose();

//                Console.WriteLine("[" + DateTime.Now.ToString("yyyy-mm-dd HH:mm:ss") + "]" + " end process... total lapse time: " + (DateTime.Now - dteProcessStartDate).ToString());
//            }
//            catch (Exception ex)
//            {
//                Console.WriteLine(ex.Message);
//                Console.WriteLine(ex.ToString());
//            }
//        }
//    }
//}

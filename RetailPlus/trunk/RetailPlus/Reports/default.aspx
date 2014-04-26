<%@ Register TagPrefix="CTRL" TagName="PageLevelError" Src="../_PageLevelError.ascx" %>
<%@ Register TagPrefix="CTRL" TagName="RightBodySectionSearch" Src="../_RightBodySectionSearch.ascx" %>
<%@ Register TagPrefix="CTRL" TagName="SiteTitle" Src="../_SiteTitle.ascx" %>
<%@ Register TagPrefix="CTRL" TagName="LargeHeading" Src="../_LargeHeading.ascx" %>
<%@ Register TagPrefix="CTRL" TagName="HorizontalNavBar" Src="../_HorizontalNavBar.ascx" %>
<%@ Register TagPrefix="CTRL" TagName="PageHeader" Src="../_PageHeader.ascx" %>
<%@ Register TagPrefix="CTRL" TagName="ctrlExpiry" Src="../_Expiry.ascx" %>
<%@ Register TagPrefix="CTRL" TagName="ctrlProcessing" Src="../_Processing.ascx" %>
<%@ Register TagPrefix="CTRL" TagName="ctrlDefault" Src="_Default.ascx" %>
<%@ Register TagPrefix="CTRL" TagName="ctrlProducts" Src="_products/_ProductsReport.ascx" %>
<%@ Register TagPrefix="CTRL" TagName="ctrlStockTransactionReport" Src="_StockTransactionReport.ascx" %>
<%@ Register TagPrefix="CTRL" TagName="ctrlTransactionReport" Src="_TransactionReport.ascx" %>
<%@ Register TagPrefix="CTRL" TagName="ctrlTerminalReport" Src="_TerminalReport.ascx" %>
<%@ Register TagPrefix="CTRL" TagName="ctrlLoginLogoutReport" Src="_LoginLogoutReport.ascx" %>
<%@ Register TagPrefix="CTRL" TagName="ctrlProductHistoryReport" Src="_productshistory/_ProductHistoryReport.ascx" %>
<%@ Page language="c#" Inherits="AceSoft.RetailPlus.Reports._Default" Codebehind="Default.aspx.cs" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register TagPrefix="CTRL" TagName="ctrlPurchasesAndPayables" Src="_PurchasesAndPayables.ascx" %>
<%@ Register TagPrefix="CTRL" TagName="ctrlCustomerCredit" Src="_customercredit/_CustomerCredit.ascx" %>
<%@ Register TagPrefix="CTRL" TagName="ctrlDatedReport" Src="_datedsalesreport/_DatedReport.ascx" %>
<%@ Register TagPrefix="CTRL" TagName="ctrlManagementReport" Src="_managementreport/_ManagementReport.ascx" %>
<%@ Register TagPrefix="CTRL" TagName="ctrlAnalyticsReport" Src="_analyticsreport/_AnalyticsReport.ascx" %>
<%@ Register TagPrefix="CTRL" TagName="ctrlProductInventoryReport" Src="_inventory/_ProductInventoryReport.ascx" %>
<%@ Register TagPrefix="CTRL" TagName="ctrlContacts" Src="_ContactsReport.ascx" %>
<%@ Register TagPrefix="CTRL" TagName="ctrlAgentsCommision" Src="_AgentsCommisionReport.ascx" %>
<%@ Register TagPrefix="CTRL" TagName="ctrlAgentsSales" Src="_AgentsSalesReport.ascx" %>
<%@ Register TagPrefix="CTRL" TagName="ctrlMenu" Src="_Menu.ascx" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>RetailPlus : Reports</title>
		<META content="Microsoft Visual Studio 7.0" name="GENERATOR">
		<META content="C#" name="CODE_LANGUAGE" />
		<META content="JavaScript" name="vs_defaultClientScript">
		<META content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../_layouts/css/menu.css" type="text/css" rel="stylesheet">
		<LINK href="../_layouts/css/ows.css" type="text/css" rel="stylesheet">
		<LINK href="../_layouts/css/sps.css" type="text/css" rel="stylesheet">
        <link href="/aspnet_client/System_Web/4_0_30319/crystalreportviewers13/css/default.css" rel="stylesheet" type="text/css" />
		<%--<link href="/aspnet_client/System_Web/2_0_50727/CrystalReportWebFormViewer3/css/default.css" rel="stylesheet" type="text/css" />--%>
		<script language="JavaScript" src="../_Scripts/DocumentScripts.js"></script>
		<script language="JavaScript" src="../_Scripts/Calendar.js"></script>
	</HEAD>
	<body id="PageBody" scroll="yes" marginheight="0" marginwidth="0">
		<form id="frmDefaultID" name="frmDefault" action="default.aspx" method="post" runat="server">
            <asp:ToolkitScriptManager id="ToolkitScriptManager1" runat="server" />
		    <CTRL:ctrlProcessing id="ctrlProcessing" runat="server" ></CTRL:ctrlProcessing>
			<table class="ms-main" height="100%" cellspacing="0" cellpadding="0" width="100%" border="0">
				<tr>
					<td width="100%" colspan="3">
						<CTRL:PageHeader id="PageHeader" runat="server"></CTRL:PageHeader>
						<div class="ms-phnav1wrapper ms-navframe">
							<CTRL:HORIZONTALNAVBAR id="HorizontalNavBar" runat="server"></CTRL:HORIZONTALNAVBAR>
							<CTRL:ctrlExpiry id="ctrlExpiry" runat="server"></CTRL:ctrlExpiry></div>
					</td>
				</tr>
				<tr>
					<td class="ms-titleareaframe" colspan="3">
						<div class="ms-titleareaframe">
							<table class="ms-titleareaframe" cellspacing="0" cellpadding="0" width="100%" border="0">
								<tr>
									<td>
										<table style="padding-left: 2px; padding-TOP: 0px" cellspacing="0" cellpadding="0" border="0" width="100%">
											<tr>
												<td style="padding-TOP: 2px" nowrap="nowrap" align="center" width="132" height="46"><img id="spsPageTitleIcon" alt="" src="../_layouts/images/reports.gif">
												</td>
												<td><img height="1" alt="" src="../_layouts/images/blank.gif" width="15">
												</td>
												<td nowrap="nowrap" width="100%">
													<table cellspacing="0" cellpadding="0">
														<tr>
															<td class="ms-titlearea" nowrap="nowrap">
																<CTRL:SITETITLE id="SiteTitle" runat="server"></CTRL:SITETITLE></td>
														</tr>
														<tr>
															<td class="ms-pagetitle" id="onetidPageTitle">
																<CTRL:LARGEHEADING id="LargeHeading" runat="server"></CTRL:LARGEHEADING></td>
														</tr>
													</table>
												</td>
												<td align="right" valign="top">
													<table cellpadding="0" cellspacing="0" height="100%">
														<tr>
															<td valign="top" nowrap="nowrap" align="right" colspan="5">
																<!-- _locID@align="align4" _locComment="{Locked=!1025,1037}{ValidString=left,right}" -->
																<CTRL:RIGHTBODYSECTIONSEARCH id="RightBodySectionSearch" runat="server"></CTRL:RIGHTBODYSECTIONSEARCH>
															</td>
														</tr>
														<tr>
															<td class="ms-vb" nowrap="nowrap" align="right" colspan="5"></td>
														</tr>
													</table>
												</td>
											</tr>
										</table>
										<table cellpadding="0" cellspacing="0" border="0" width="100%">
											<tr>
												<td height="2" colspan="5"><img src="../_layouts/images/blank.gif" width="1" height="1" alt=""></td>
											</tr>
											<tr>
												<td class="ms-titlearealine" height="1" colspan="5"><img src="../_layouts/images/blank.gif" width="1" height="1" alt=""></td>
											</tr>
										</table>
									</td>
								</tr>
							</table>
						</div>
					</td>
				</tr>
				<tr valign="top" height="100%">
					<td class="ms-nav" id="webpartpagenavbar" height="100%" widthchange="175">
						<table class="ms-navframe" id="Table7" height="100%" cellspacing="0" cellpadding="0" border="0">
							<tr valign="top">
								<td class="ms-navwatermark" id="onetidWatermark" dir="ltr"></td>
								<td style="padding-RIGHT: 2px" width="150">
									<img height="1" alt="" src="../_layouts/images/trans.gif" width="150">
									<CTRL:ctrlMenu id="ctrlMenu" runat="server"></CTRL:ctrlMenu>&nbsp;
								</td>
							</tr>
						</table>
					</td>
					<td><img height="1" alt="" src="../_layouts/images/blank.gif" width="5"></td>
					<td class="ms-bodyareaframe" valign="top" width="100%"><CTRL:PAGELEVELERROR id="PageLevelError" runat="server"></CTRL:PAGELEVELERROR>
						<table class="ms-tztable" id="ZoneTable" cellspacing="0" cellpadding="0" width="100%" border="0">
							<tr id="TopRow">
								<td class="ms-tztop" id="TopCell" valign="top" width="100%">
								    <CTRL:ctrlDefault id="ctrlDefault" runat="server" visible="False"></CTRL:ctrlDefault>
									<CTRL:ctrlContacts id="ctrlContacts" runat="server" visible="False"></CTRL:ctrlContacts>
									<CTRL:ctrlProducts id="ctrlProducts" runat="server" visible="False"></CTRL:ctrlProducts>
									<CTRL:ctrlProductInventoryReport id="ctrlProductInventoryReport" runat="server" visible="False"></CTRL:ctrlProductInventoryReport>
									<CTRL:ctrlStockTransactionReport id="ctrlStockTransactionReport" runat="server" visible="False"></CTRL:ctrlStockTransactionReport>
									<CTRL:ctrlTransactionReport id="ctrlTransactionReport" runat="server" visible="False"></CTRL:ctrlTransactionReport>
									<CTRL:ctrlDatedReport id="ctrlDatedReport" runat="server" visible="False"></CTRL:ctrlDatedReport>
                                    <CTRL:ctrlManagementReport id="ctrlManagementReport" runat="server" visible="False"></CTRL:ctrlManagementReport>
                                    <CTRL:ctrlAnalyticsReport id="ctrlAnalyticsReport" runat="server" visible="False"></CTRL:ctrlAnalyticsReport>
									<CTRL:ctrlCustomerCredit id="ctrlCustomerCredit" runat="server" visible="False"></CTRL:ctrlCustomerCredit>
									<CTRL:ctrlTerminalReport id="ctrlTerminalReport" runat="server" visible="False"></CTRL:ctrlTerminalReport>
									<CTRL:ctrlLoginLogoutReport id="ctrlLoginLogoutReport" runat="server" visible="False"></CTRL:ctrlLoginLogoutReport>
									<CTRL:ctrlPurchasesAndPayables id="ctrlPurchasesAndPayables" runat="server" visible="False"></CTRL:ctrlPurchasesAndPayables>
									<CTRL:ctrlProductHistoryReport id="ctrlProductHistoryReport" runat="server" visible="False"></CTRL:ctrlProductHistoryReport>
									<CTRL:ctrlAgentsCommision id="ctrlAgentsCommision" runat="server" visible="False"></CTRL:ctrlAgentsCommision>
									<CTRL:ctrlAgentsSales id="ctrlAgentsSales" runat="server" visible="False"></CTRL:ctrlAgentsSales>
									</td>
							</tr>
						</table>
					</td>
				</tr>
			</table>
		</form>
	</body>
</HTML>

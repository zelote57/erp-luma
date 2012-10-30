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
<%@ Register TagPrefix="CTRL" TagName="ctrlProductInventoryReport" Src="_inventory/_ProductInventoryReport.ascx" %>
<%@ Register TagPrefix="CTRL" TagName="ctrlContacts" Src="_ContactsReport.ascx" %>
<%@ Register TagPrefix="CTRL" TagName="ctrlAgentsCommision" Src="_AgentsCommisionReport.ascx" %>
<%@ Register TagPrefix="CTRL" TagName="ctrlAgentsSales" Src="_AgentsSalesReport.ascx" %>
<%@ Register TagPrefix="CTRL" TagName="ctrlMenu" Src="../Reports/_Menu.ascx" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>RetailPlus : Reports</title>
		<META content="Microsoft Visual Studio 7.0" name="GENERATOR">
		<META content="C#" name="CODE_LANGUAGE" />
		<META content="JavaScript" name="vs_defaultClientScript">
		<META content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../menu.css" type="text/css" rel="stylesheet">
		<LINK href="../ows.css" type="text/css" rel="stylesheet">
		<LINK href="../sps.css" type="text/css" rel="stylesheet">
		<link href="/aspnet_client/System_Web/2_0_50727/CrystalReportWebFormViewer3/css/default.css"
                rel="stylesheet" type="text/css" />
		<script language="JavaScript" src="../_Scripts/DocumentScripts.js"></script>
		<script language="JavaScript" src="../_Scripts/Calendar.js"></script>
	</HEAD>
	<BODY id="PageBody" scroll="yes" marginheight="0" marginwidth="0">
		<FORM id="frmDefaultID" name="frmDefault" action="default.aspx" method="post" runat="server">
            <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server" />
		    <CTRL:ctrlProcessing id="ctrlProcessing" runat="server" ></CTRL:ctrlProcessing>
			<table class="ms-main" height="100%" cellSpacing="0" cellPadding="0" width="100%" border="0">
				<TR>
					<TD width="100%" colspan="3">
						<CTRL:PageHeader id="PageHeader" runat="server"></CTRL:PageHeader>
						<DIV class="ms-phnav1wrapper ms-navframe">
							<CTRL:HORIZONTALNAVBAR id="HorizontalNavBar" runat="server"></CTRL:HORIZONTALNAVBAR>
							<CTRL:ctrlExpiry id="ctrlExpiry" runat="server"></CTRL:ctrlExpiry></DIV>
					</TD>
				</TR>
				<TR>
					<TD class="ms-titleareaframe" colspan="3">
						<DIV class="ms-titleareaframe">
							<table class="ms-titleareaframe" cellSpacing="0" cellPadding="0" width="100%" border="0">
								<TR>
									<TD>
										<table style="PADDING-LEFT: 2px; PADDING-TOP: 0px" cellSpacing="0" cellPadding="0" border="0" width="100%">
											<TR>
												<TD style="PADDING-TOP: 2px" noWrap align="center" width="132" height="46"><IMG id="spsPageTitleIcon" alt="" src="../_layouts/images/reports.gif">
												</TD>
												<TD><IMG height="1" alt="" src="../_layouts/images/blank.gif" width="15">
												</TD>
												<TD noWrap width="100%">
													<table cellSpacing="0" cellPadding="0">
														<TR>
															<TD class="ms-titlearea" noWrap>
																<CTRL:SITETITLE id="SiteTitle" runat="server"></CTRL:SITETITLE></TD>
														</TR>
														<TR>
															<TD class="ms-pagetitle" id="onetidPageTitle">
																<CTRL:LARGEHEADING id="LargeHeading" runat="server"></CTRL:LARGEHEADING></TD>
														</TR>
													</TABLE>
												</TD>
												<td align="right" valign="top">
													<table cellpadding="0" cellspacing="0" height="100%">
														<TR>
															<TD vAlign="top" noWrap align="right" colspan="5">
																<!-- _locID@align="align4" _locComment="{Locked=!1025,1037}{ValidString=left,right}" -->
																<CTRL:RIGHTBODYSECTIONSEARCH id="RightBodySectionSearch" runat="server"></CTRL:RIGHTBODYSECTIONSEARCH>
															</TD>
														</TR>
														<TR>
															<TD class="ms-vb" noWrap align="right" colspan="5"></TD>
														</TR>
													</table>
												</td>
											</TR>
										</TABLE>
										<table cellpadding="0" cellspacing="0" border="0" width="100%">
											<tr>
												<td height="2" colspan="5"><IMG SRC="../_layouts/images/blank.gif" width="1" height="1" alt=""></td>
											</tr>
											<tr>
												<td class="ms-titlearealine" height="1" colspan="5"><IMG SRC="../_layouts/images/blank.gif" width="1" height="1" alt=""></td>
											</tr>
										</table>
									</TD>
								</TR>
							</TABLE>
						</DIV>
					</TD>
				</TR>
				<TR vAlign="top" height="100%">
					<TD class="ms-nav" id="webpartpagenavbar" height="100%" widthchange="175">
						<table class="ms-navframe" id="Table7" height="100%" cellSpacing="0" cellPadding="0" border="0">
							<TR vAlign="top">
								<TD class="ms-navwatermark" id="onetidWatermark" dir="ltr"></TD>
								<TD style="PADDING-RIGHT: 2px" width="150">
									<IMG height="1" alt="" src="../_layouts/images/trans.gif" width="150">
									<CTRL:ctrlMenu id="ctrlMenu" runat="server"></CTRL:ctrlMenu>&nbsp;
								</TD>
							</TR>
						</TABLE>
					</TD>
					<TD><IMG height="1" alt="" src="../_layouts/images/blank.gif" width="5"></TD>
					<TD class="ms-bodyareaframe" vAlign="top" width="100%"><CTRL:PAGELEVELERROR id="PageLevelError" runat="server"></CTRL:PAGELEVELERROR>
						<table class="ms-tztable" id="ZoneTable" cellSpacing="0" cellPadding="0" width="100%" border="0">
							<tr id="TopRow">
								<td class="ms-tztop" id="TopCell" vAlign="top" width="100%">
								    <CTRL:ctrlDefault id="ctrlDefault" runat="server" Visible="False"></CTRL:ctrlDefault>
									<CTRL:ctrlContacts id="ctrlContacts" runat="server" Visible="False"></CTRL:ctrlContacts>
									<CTRL:ctrlProducts id="ctrlProducts" runat="server" Visible="False"></CTRL:ctrlProducts>
									<CTRL:ctrlProductInventoryReport id="ctrlProductInventoryReport" runat="server" Visible="False"></CTRL:ctrlProductInventoryReport>
									<CTRL:ctrlStockTransactionReport id="ctrlStockTransactionReport" runat="server" Visible="False"></CTRL:ctrlStockTransactionReport>
									<CTRL:ctrlTransactionReport id="ctrlTransactionReport" runat="server" Visible="False"></CTRL:ctrlTransactionReport>
									<CTRL:ctrlDatedReport id="ctrlDatedReport" runat="server" Visible="False"></CTRL:ctrlDatedReport>
									<CTRL:ctrlCustomerCredit id="ctrlCustomerCredit" runat="server" Visible="False"></CTRL:ctrlCustomerCredit>
									<CTRL:ctrlTerminalReport id="ctrlTerminalReport" runat="server" Visible="False"></CTRL:ctrlTerminalReport>
									<CTRL:ctrlLoginLogoutReport id="ctrlLoginLogoutReport" runat="server" Visible="False"></CTRL:ctrlLoginLogoutReport>
									<CTRL:ctrlPurchasesAndPayables id="ctrlPurchasesAndPayables" runat="server" Visible="False"></CTRL:ctrlPurchasesAndPayables>
									<CTRL:ctrlProductHistoryReport id="ctrlProductHistoryReport" runat="server" Visible="False"></CTRL:ctrlProductHistoryReport>
									<CTRL:ctrlAgentsCommision id="ctrlAgentsCommision" runat="server" Visible="False"></CTRL:ctrlAgentsCommision>
									<CTRL:ctrlAgentsSales id="ctrlAgentsSales" runat="server" Visible="False"></CTRL:ctrlAgentsSales>
									</td>
							</tr>
						</TABLE>
					</TD>
				</TR>
			</TABLE>
		</FORM>
	</BODY>
</HTML>

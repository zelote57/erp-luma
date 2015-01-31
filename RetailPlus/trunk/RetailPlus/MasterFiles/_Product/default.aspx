<%@ Reference Page="~/masterfiles/default.aspx" %>
<%@ Page language="c#" Inherits="AceSoft.RetailPlus.MasterFiles._Product._Default" Codebehind="Default.aspx.cs" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register TagPrefix="CTRL" TagName="ctrlMenu" Src="../_Menu.ascx" %>
<%@ Register TagPrefix="CTRL" TagName="ctrlExpiry" Src="../../_Expiry.ascx" %>
<%@ Register TagPrefix="CTRL" TagName="ctrlProcessing" Src="../../_Processing.ascx" %>
<%@ Register TagPrefix="CTRL" TagName="ctrlInsert" Src="_Insert.ascx" %>
<%@ Register TagPrefix="CTRL" TagName="ctrlDetails" Src="_Details.ascx" %>
<%@ Register TagPrefix="CTRL" TagName="ctrlChangeOSPrinter" Src="_ChangeOSPrinter.ascx" %>
<%@ Register TagPrefix="CTRL" TagName="ctrlChangeTax" Src="_ChangeTax.ascx" %>
<%@ Register TagPrefix="CTRL" TagName="ctrlChangeRewardPoints" Src="_ChangeRewardPoints.ascx" %>
<%@ Register TagPrefix="CTRL" TagName="ctrlAddProductVariation" Src="_AddProductVariation.ascx" %>
<%@ Register TagPrefix="CTRL" TagName="ctrlChangePrice" Src="_ChangePrice.ascx" %>
<%@ Register TagPrefix="CTRL" TagName="ctrlSynchronize" Src="_Synchronize.ascx" %>
<%@ Register TagPrefix="CTRL" TagName="ctrlUpdate" Src="_Update.ascx" %>
<%@ Register TagPrefix="CTRL" TagName="ctrlList" Src="_List.ascx" %>
<%@ Register TagPrefix="CTRL" TagName="ctrlListDetailed" Src="_ListDetailed.ascx" %>
<%@ Register TagPrefix="CTRL" TagName="ctrlCompose" Src="_Compose.ascx" %>
<%@ Register TagPrefix="CTRL" TagName="ctrlFinance" Src="_Finance.ascx" %>
<%@ Register TagPrefix="CTRL" TagName="PageHeader" Src="../../_PageHeader.ascx" %>
<%@ Register TagPrefix="CTRL" TagName="HorizontalNavBar" Src="../../_HorizontalNavBar.ascx" %>
<%@ Register TagPrefix="CTRL" TagName="LargeHeading" Src="../../_LargeHeading.ascx" %>
<%@ Register TagPrefix="CTRL" TagName="SiteTitle" Src="../../_SiteTitle.ascx" %>
<%@ Register TagPrefix="CTRL" TagName="RightBodySectionSearch" Src="../../_RightBodySectionSearch.ascx" %>
<%@ Register TagPrefix="CTRL" TagName="PageLevelError" Src="../../_PageLevelError.ascx" %>

<%@ Register TagPrefix="CTRL" TagName="ctrlUnitsMatrixList" Src="_UnitsMatrix/_list.ascx" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>RetailPlus : Products</title>
		<META content="Microsoft Visual Studio 7.0" name="GENERATOR">
		<META content="C#" name="CODE_LANGUAGE">
		<META content="JavaScript" name="vs_defaultClientScript">
		<META content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../../_layouts/css/menu.css" type="text/css" rel="stylesheet">
		<LINK href="../../_layouts/css/ows.css" type="text/css" rel="stylesheet">
		<LINK href="../../_layouts/css/sps.css" type="text/css" rel="stylesheet">
	</HEAD>
	<body id="PageBody" scroll="yes" marginheight="0" marginwidth="0">
		<form id="frmProducts" name="frmProducts" action="default.aspx" method="post" runat="server" enctype="multipart/form-data">
		    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server" />
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
												<td style="padding-TOP: 2px" nowrap="nowrap" align="center" width="132" height="46"><img id="spsPageTitleIcon" alt="" src="../../_layouts/images/Settings.gif">
												</td>
												<td><img height="1" alt="" src="../../_layouts/images/blank.gif" width="15" />
												</td>
												<td nowrap="nowrap" width="100%">
													<table cellspacing="0" cellpadding="0">
														<tr>
															<td class="ms-titlearea" noWrap>
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
												<td height="2" colspan="5"><img SRC="../../_layouts/images/blank.gif" width="1" height="1" alt=""></td>
											</tr>
											<tr>
												<td class="ms-titlearealine" height="1" colspan="5"><img SRC="../../_layouts/images/blank.gif" width="1" height="1" alt=""></td>
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
								<td style="padding-RIGHT: 2px" width="126">
									<img height="1" alt="" src="../../_layouts/images/trans.gif" width="126">
									<CTRL:ctrlMenu id="ctrlMenu" runat="server"></CTRL:ctrlMenu>&nbsp;
								</td>
							</tr>
						</table>
					</td>
					<td><img height="1" alt="" src="../../_layouts/images/blank.gif" width="5"></td>
					<td class="ms-bodyareaframe" valign="top" width="100%"><CTRL:PAGELEVELERROR id="PageLevelError" runat="server"></CTRL:PAGELEVELERROR>
						<table class="ms-tztable" id="ZoneTable" cellspacing="0" cellpadding="0" width="100%" border="0">
							<tr id="TopRow">
								<td class="ms-tztop" id="TopCell" valign="top" width="100%">
									<CTRL:ctrlList id="ctrlList" runat="server" Visible="False"></CTRL:ctrlList>
									<CTRL:ctrlListDetailed id="ctrlListDetailed" runat="server" Visible="False"></CTRL:ctrlListDetailed>
									<CTRL:ctrlInsert id="ctrlInsert" runat="server" Visible="False"></CTRL:ctrlInsert>
									<CTRL:ctrlUpdate id="ctrlUpdate" runat="server" Visible="False"></CTRL:ctrlUpdate>
                                    <CTRL:ctrlChangeOSPrinter id="ctrlChangeOSPrinter" runat="server" Visible="False"></CTRL:ctrlChangeOSPrinter>
									<CTRL:ctrlChangeTax id="ctrlChangeTax" runat="server" Visible="False"></CTRL:ctrlChangeTax>
									<CTRL:ctrlAddProductVariation id="ctrlAddProductVariation" runat="server" Visible="False"></CTRL:ctrlAddProductVariation>
                                    <CTRL:ctrlChangeRewardPoints id="ctrlChangeRewardPoints" runat="server" Visible="False"></CTRL:ctrlChangeRewardPoints>
									<CTRL:ctrlChangePrice id="ctrlChangePrice" runat="server" Visible="False"></CTRL:ctrlChangePrice>
									<CTRL:ctrlDetails id="ctrlDetails" runat="server" Visible="False"></CTRL:ctrlDetails>
									<CTRL:ctrlUnitsMatrixList id="ctrlUnitsMatrixList" runat="server" Visible="False"></CTRL:ctrlUnitsMatrixList>
									<CTRL:ctrlCompose id="ctrlCompose" runat="server" Visible="false"></CTRL:ctrlCompose>
									<CTRL:ctrlFinance id="ctrlFinance" runat="server" Visible="false"></CTRL:ctrlFinance>
									<CTRL:ctrlSynchronize id="ctrlSynchronize" runat="server" Visible="false"></CTRL:ctrlSynchronize>
									</td>
							</tr>
						</table>
					</td>
				</tr>
			</table>
		</form>
	</body>
</HTML>

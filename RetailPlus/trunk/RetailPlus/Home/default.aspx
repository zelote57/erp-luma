<%@ Page language="c#" Inherits="AceSoft.RetailPlus.Home._Default" Codebehind="Default.aspx.cs" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register TagPrefix="CtrL" TagName="PageLevelError" Src="../_PageLevelError.ascx" %>
<%@ Register TagPrefix="CtrL" TagName="RightbodySectionSearch" Src="../_RightbodySectionSearch.ascx" %>
<%@ Register TagPrefix="CtrL" TagName="SiteTitle" Src="../_SiteTitle.ascx" %>
<%@ Register TagPrefix="CtrL" TagName="LargeHeading" Src="../_LargeHeading.ascx" %>
<%@ Register TagPrefix="CtrL" TagName="HorizontalNavBar" Src="../_HorizontalNavBar.ascx" %>
<%@ Register TagPrefix="CtrL" TagName="PageHeader" Src="../_PageHeader.ascx" %>
<%@ Register TagPrefix="CtrL" TagName="ctrlMenu" Src="_Menu.ascx" %>
<%@ Register TagPrefix="CtrL" TagName="ctrlExpiry" Src="../_Expiry.ascx" %>
<%@ Register TagPrefix="CtrL" TagName="ctrlProcessing" Src="../_Processing.ascx" %>
<%@ Reference Page="~/adminfiles/default.aspx" %>
<!DOCTYPE HTML PUBLIC "-//W3C//Dtd HTML 4.0 transitional//EN" >
<HTML>
	<HEAD>
		<title>RetailPlus : Home</title>
		<META content="Microsoft Visual Studio 7.0" name="GENERATOR">
		<META content="C#" name="CODE_LANGUAGE">
		<META content="JavaScript" name="vs_defaultClientScript">
		<META content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../_layouts/css/menu.css" type="text/css" rel="stylesheet" />
		<LINK href="../_layouts/css/ows.css" type="text/css" rel="stylesheet" />
		<LINK href="../_layouts/css/sps.css" type="text/css" rel="stylesheet" />
		<script language="JavaScript" src="../_Scripts/DocumentScripts.js"></script>
		<script language="JavaScript" src="../_Scripts/Calendar.js"></script>
	</HEAD>
	<body id="Pagebody" scroll="yes" marginheight="0" marginwidth="0">
		<form id="frmDefaultID" name="frmDefault" action="default.aspx" method="post" runat="server">
		    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server" />
		    <CtrL:ctrlProcessing id="ctrlProcessing" runat="server" ></CtrL:ctrlProcessing>
			<table class="ms-main" height="100%" cellspacing="0" cellpadding="0" width="100%" border="0">
				<tr>
					<td width="100%" colspan="3">
						<CtrL:PageHeader id="PageHeader" runat="server"></CtrL:PageHeader>
						<div class="ms-phnav1wrapper ms-navframe">
							<CtrL:HORIZONTALNAVBAR id="HorizontalNavBar" runat="server"></CtrL:HORIZONTALNAVBAR>
							<CtrL:ctrlExpiry id="ctrlExpiry" runat="server"></CtrL:ctrlExpiry></div>
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
												<td style="padding-TOP: 2px" nowrap="nowrap" align="middle" width="132" height="46"><img id="spsPageTitleIcon" alt="" src="../_layouts/images/inventory.gif">
												</td>
												<td><img height="1" alt="" src="../_layouts/images/blank.gif" width="15">
												</td>
												<td nowrap="nowrap" width="100%">
													<table cellspacing="0" cellpadding="0">
														<tr>
															<td class="ms-titlearea" noWrap>
																<CtrL:SITETITLE id="SiteTitle" runat="server"></CtrL:SITETITLE></td>
														</tr>
														<tr>
															<td class="ms-pagetitle" id="onetidPageTitle">
																<CtrL:LARGEHEADING id="LargeHeading" runat="server"></CtrL:LARGEHEADING></td>
														</tr>
													</table>
												</td>
												<td align="right" valign="top">
													<table cellpadding="0" cellspacing="0" height="100%">
														<tr>
															<td valign="top" nowrap="nowrap" align="right" colspan="5">
																<!-- _locID@align="align4" _locComment="{Locked=!1025,1037}{ValidString=left,right}" -->
																<CtrL:RIGHTbodySECTIONSEARCH id="RightbodySectionSearch" runat="server"></CtrL:RIGHTbodySECTIONSEARCH>
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
												<td height="2" colspan="5"><img SRC="../_layouts/images/blank.gif" width="1" height="1" alt=""></td>
											</tr>
											<tr>
												<td class="ms-titlearealine" height="1" colspan="5"><img SRC="../_layouts/images/blank.gif" width="1" height="1" alt=""></td>
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
						<table class="ms-navframe" id="table7" height="100%" cellspacing="0" cellpadding="0" border="0">
							<tr valign="top">
								<td class="ms-navwatermark" id="onetidWatermark" dir="ltr"></td>
								<td style="padding-RIGHT: 2px" width="126">
									<img height="1" alt="" src="../_layouts/images/trans.gif" width="126">
									<CtrL:ctrlMenu id="ctrlMenu" runat="server"></CtrL:ctrlMenu>&nbsp;
								</td>
							</tr>
						</table>
					</td>
					<td><img height="1" alt="" src="../_layouts/images/blank.gif" width="5"></td>
					<td class="ms-bodyareaframe" valign="top" width="100%"><CtrL:PAGELEVELERROR id="PageLevelError" runat="server"></CtrL:PAGELEVELERROR>
						<table class="ms-tztable" id="Zonetable" cellspacing="0" cellpadding="0" width="100%" border="0">
							<tr id="TopRow">
								<td class="ms-tztop" id="TopCell" valign="top" width="100%" style="height: 217px">
                                    <%--put the controls here--%>
                                </td>
							</tr>
						</table>
                        
					</td>
				</tr>
			</table>
		</form>
	</body>
</HTML>

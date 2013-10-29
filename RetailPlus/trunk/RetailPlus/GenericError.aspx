<%@ Register TagPrefix="CTRL" TagName="PageLevelError" Src="_PageLevelError.ascx" %>
<%@ Register TagPrefix="CTRL" TagName="RightBodySectionSearch" Src="_RightBodySectionSearch.ascx" %>
<%@ Register TagPrefix="CTRL" TagName="SiteTitle" Src="_SiteTitle.ascx" %>
<%@ Register TagPrefix="CTRL" TagName="LargeHeading" Src="_LargeHeading.ascx" %>
<%@ Register TagPrefix="CTRL" TagName="Login" Src="_Login.ascx" %>
<%@ Page language="c#" Inherits="AceSoft.RetailPlus._GenericError" Codebehind="GenericError.aspx.cs" %>
<%@ Register TagPrefix="CTRL" TagName="HorizontalNavBar" Src="_HorizontalNavBar.ascx" %>
<%@ Register TagPrefix="CTRL" TagName="PageHeader" Src="_PageHeader.ascx" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>RetailPlus : Error Handler</title>
		<meta content="Microsoft Visual Studio 7.0" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="_layouts/css/menu.css" type="text/css" rel="stylesheet">
		<LINK href="_layouts/css/ows.css" type="text/css" rel="stylesheet">
		<LINK href="_layouts/css/sps.css" type="text/css" rel="stylesheet">
		<script src="backgo.js"></script>
	</HEAD>
	<body id="PageBody" marginwidth="0" marginheight="0" scroll="yes">
		<form name="frmDefault" method="post" action="default.aspx" id="frmDefaultID" runat="server">
			<table class="ms-main" cellpadding="0" cellspacing="0" border="0" width="100%" height="100%">
				<tr>
					<td width="100%" colspan="3">
						<CTRL:PageHeader id="PageHeader" runat="server"></CTRL:PageHeader>
						<div class="ms-phnav1wrapper ms-navframe">
							<CTRL:HORIZONTALNAVBAR id="HorizontalNavBar" runat="server"></CTRL:HORIZONTALNAVBAR>
						</div>
					</td>
				</tr>
				<tr>
					<td colspan="3" class="ms-titleareaframe">
						<div class="ms-titleareaframe">
							<table width="100%" border="0" class="ms-titleareaframe" cellpadding="0" cellspacing="0">
								<tr>
									<td>
										<table style="padding-left: 2px;padding-TOP: 0px" cellpadding="0" cellspacing="0" border="0">
											<tr>
												<td align="middle" nowrap width="132" height="46" style="padding-TOP: 2px">
													<img id="spsPageTitleIcon" src="_layouts/images/error.gif" alt="">
												</td>
												<td>
													<img SRC="_layouts/images/blank.gif" width="15" height="1" alt="">
												</td>
												<td nowrap width="100%">
													<table cellpadding="0" cellspacing="0">
														<tr>
															<td class="ms-titlearea" noWrap><CTRL:SITETITLE id="SiteTitle" runat="server"></CTRL:SITETITLE></td>
														</tr>
														<tr>
															<td class="ms-pagetitle" id="onetidPageTitle"><CTRL:LARGEHEADING id="LargeHeading" runat="server"></CTRL:LARGEHEADING></td>
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
												<td height="2" colspan="5"><img height="1" alt="" src="_layouts/images/blank.gif" width="15" /></td>
											</tr>
											<tr>
												<td class="ms-titlearealine" height="1" colspan="5"><img height="1" alt="" src="_layouts/images/blank.gif" width="15" /></td>
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
									<img height="1" alt="" src="_layouts/images/trans.gif" width="126">
								</td>
							</tr>
						</table>
					</td>
					<td><img height="1" alt="" src="_layouts/images/blank.gif" width="5"></td>
					<td class="ms-bodyareaframe" valign="top" width="100%">
						<table class="ms-tztable" id="ZoneTable" cellspacing="0" cellpadding="0" width="100%" border="0">
							<tr>
								<td class="ms-tztop" id="FirstCell" valign="top" width="100%">
									<asp:Label id="lblMessage" CssClass="ms-vh2" runat="server"></asp:Label></td>
							</tr>
							<tr>
								<td class="ms-sectionline" valign="top" width="100%" height="1"></td>
							</tr>
							<tr>
								<td class="ms-tztop" valign="top" width="100%">
									<asp:Label id="lblErrorMessage" CssClass="ms-vh2" runat="server" ForeColor="Red"></asp:Label></td>
							</tr>
							<tr>
								<td class="ms-tztop" id="SecondCell" valign="top" width="100%">
									<asp:Label id="lblSource" CssClass="ms-vh2" runat="server" ForeColor="Red"></asp:Label></td>
							</tr>
							<tr>
								<td class="ms-tztop" id="ThirdCell" valign="top" width="100%">
									<asp:Label id="lblExceptionType" CssClass="ms-vh2" runat="server" ForeColor="Red"></asp:Label></td>
							</tr>
							<tr>
								<td class="ms-tztop" id="FourthCell" valign="top" width="100%">
									<asp:Label id="lblStackTrace" CssClass="ms-vh2" runat="server" ForeColor="Red"></asp:Label></td>
							</tr>
							<tr>
								<td class="ms-sectionline" valign="top" width="100%" height="2"></td>
							</tr>
							<tr>
								<td class="ms-tztop" valign="top" width="100%">
									<INPUT class="ms-button" type="button" value="          Continue          " onclick="javascript:window.history.go(-1);return false;" id="cmdContinue" name="Button1" runat="server"></td>
							</tr>
						</table>
					</td>
				</tr>
			</table>
		</form>
	</body>
</HTML>

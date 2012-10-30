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
		<LINK href="menu.css" type="text/css" rel="stylesheet">
		<LINK href="ows.css" type="text/css" rel="stylesheet">
		<LINK href="sps.css" type="text/css" rel="stylesheet">
		<script src="backgo.js"></script>
	</HEAD>
	<body id="PageBody" marginwidth="0" marginheight="0" scroll="yes">
		<form name="frmDefault" method="post" action="default.aspx" id="frmDefaultID" runat="server">
			<table class="ms-main" cellpadding="0" cellspacing="0" border="0" width="100%" height="100%">
				<TR>
					<TD width="100%" colSpan="3">
						<CTRL:PageHeader id="PageHeader" runat="server"></CTRL:PageHeader>
						<DIV class="ms-phnav1wrapper ms-navframe">
							<CTRL:HORIZONTALNAVBAR id="HorizontalNavBar" runat="server"></CTRL:HORIZONTALNAVBAR>
						</DIV>
					</TD>
				</TR>
				<tr>
					<td colspan="3" class="ms-titleareaframe">
						<div class="ms-titleareaframe">
							<table width="100%" border="0" class="ms-titleareaframe" cellpadding="0" cellspacing="0">
								<tr>
									<td>
										<table style="PADDING-LEFT: 2px;PADDING-TOP: 0px" cellpadding="0" cellspacing="0" border="0">
											<tr>
												<td align="middle" nowrap width="132" height="46" style="PADDING-TOP: 2px">
													<img id="spsPageTitleIcon" src="_layouts/images/error.gif" alt="">
												</td>
												<td>
													<IMG SRC="_layouts/images/blank.gif" width="15" height="1" alt="">
												</td>
												<td nowrap width="100%">
													<table cellpadding="0" cellspacing="0">
														<TR>
															<TD class="ms-titlearea" noWrap><CTRL:SITETITLE id="SiteTitle" runat="server"></CTRL:SITETITLE></TD>
														</TR>
														<TR>
															<TD class="ms-pagetitle" id="onetidPageTitle"><CTRL:LARGEHEADING id="LargeHeading" runat="server"></CTRL:LARGEHEADING></TD>
														</TR>
													</table>
												</td>
												<td align="right" valign="top">
													<table cellpadding="0" cellspacing="0" height="100%">
														<TR>
															<TD vAlign="top" noWrap align="right" colSpan="5">
																<!-- _locID@align="align4" _locComment="{Locked=!1025,1037}{ValidString=left,right}" -->
																<CTRL:RIGHTBODYSECTIONSEARCH id="RightBodySectionSearch" runat="server"></CTRL:RIGHTBODYSECTIONSEARCH>
															</TD>
														</TR>
														<TR>
															<TD class="ms-vb" noWrap align="right" colSpan="5"></TD>
														</TR>
													</table>
												</td>
											</tr>
										</table>
										<table cellpadding="0" cellspacing="0" border="0" width="100%">
											<tr>
												<td height="2" colspan="5"><IMG SRC="_layouts/images/blank.gif" width="1" height="1" alt=""></td>
											</tr>
											<tr>
												<td class="ms-titlearealine" height="1" colspan="5"><IMG SRC="_layouts/images/blank.gif" width="1" height="1" alt=""></td>
											</tr>
										</table>
									</td>
								</tr>
							</table>
						</div>
					</td>
				</tr>
				<TR vAlign="top" height="100%">
					<TD class="ms-nav" id="webpartpagenavbar" height="100%" widthchange="175">
						<TABLE class="ms-navframe" id="Table7" height="100%" cellSpacing="0" cellPadding="0" border="0">
							<TR vAlign="top">
								<TD class="ms-navwatermark" id="onetidWatermark" dir="ltr"></TD>
								<TD style="PADDING-RIGHT: 2px" width="126">
									<IMG height="1" alt="" src="_layouts/images/trans.gif" width="126">
								</TD>
							</TR>
						</TABLE>
					</TD>
					<TD><IMG height="1" alt="" src="_layouts/images/blank.gif" width="5"></TD>
					<TD class="ms-bodyareaframe" vAlign="top" width="100%">
						<TABLE class="ms-tztable" id="ZoneTable" cellSpacing="0" cellPadding="0" width="100%" border="0">
							<tr>
								<td class="ms-tztop" id="FirstCell" vAlign="top" width="100%">
									<asp:Label id="lblMessage" CssClass="ms-vh2" runat="server"></asp:Label></td>
							</tr>
							<TR>
								<TD class="ms-sectionline" vAlign="top" width="100%" height="1"></TD>
							</TR>
							<TR>
								<TD class="ms-tztop" vAlign="top" width="100%">
									<asp:Label id="lblErrorMessage" CssClass="ms-vh2" runat="server" ForeColor="Red"></asp:Label></TD>
							</TR>
							<tr>
								<td class="ms-tztop" id="SecondCell" vAlign="top" width="100%">
									<asp:Label id="lblSource" CssClass="ms-vh2" runat="server" ForeColor="Red"></asp:Label></td>
							</tr>
							<tr>
								<td class="ms-tztop" id="ThirdCell" vAlign="top" width="100%">
									<asp:Label id="lblExceptionType" CssClass="ms-vh2" runat="server" ForeColor="Red"></asp:Label></td>
							</tr>
							<tr>
								<td class="ms-tztop" id="FourthCell" vAlign="top" width="100%">
									<asp:Label id="lblStackTrace" CssClass="ms-vh2" runat="server" ForeColor="Red"></asp:Label></td>
							</tr>
							<TR>
								<TD class="ms-sectionline" vAlign="top" width="100%" height="2"></TD>
							</TR>
							<TR>
								<TD class="ms-tztop" vAlign="top" width="100%">
									<INPUT class="ms-button" type="button" value="          Continue          " onclick="javascript:window.history.go(-1);return false;" id="cmdContinue" name="Button1" runat="server"></TD>
							</TR>
						</TABLE>
					</TD>
				</TR>
			</table>
		</form>
	</body>
</HTML>

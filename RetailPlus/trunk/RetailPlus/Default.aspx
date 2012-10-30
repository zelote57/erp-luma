<%@ Register TagPrefix="CTRL" TagName="PageLevelError" Src="_PageLevelError.ascx" %>
<%@ Register TagPrefix="CTRL" TagName="RightBodySectionSearch" Src="_RightBodySectionSearch.ascx" %>
<%@ Register TagPrefix="CTRL" TagName="SiteTitle" Src="_SiteTitle.ascx" %>
<%@ Register TagPrefix="CTRL" TagName="LargeHeading" Src="_LargeHeading.ascx" %>
<%@ Register TagPrefix="CTRL" TagName="Login" Src="_Login.ascx" %>
<%@ Register TagPrefix="CTRL" TagName="HorizontalNavBar" Src="_HorizontalNavBar.ascx" %>
<%@ Register TagPrefix="CTRL" TagName="PageHeader" Src="_PageHeader.ascx" %>
<%@ Page language="c#" Inherits="AceSoft.RetailPlus._Default" Codebehind="Default.aspx.cs" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>AceSoft RetailPlus System</title>
		<META content="Microsoft Visual Studio 7.0" name="GENERATOR">
		<META content="C#" name="CODE_LANGUAGE">
		<META content="JavaScript" name="vs_defaultClientScript">
		<META content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="menu.css" type="text/css" rel="stylesheet">
		<LINK href="ows.css" type="text/css" rel="stylesheet">
		<LINK href="sps.css" type="text/css" rel="stylesheet">
	</HEAD>
	<BODY id="PageBody" scroll="yes" marginheight="0" marginwidth="0">
		<FORM id="frmDefaultID" name="frmDefault" action="default.aspx" method="post" runat="server">
			<TABLE class="ms-main" height="100%" cellSpacing="0" cellPadding="0" width="100%" border="0">
				<TR>
					<TD width="100%" colSpan="3">
						<CTRL:PageHeader id="PageHeader" runat="server"></CTRL:PageHeader>
						<DIV class="ms-phnav1wrapper ms-navframe">
							<CTRL:HORIZONTALNAVBAR id="HorizontalNavBar" runat="server"></CTRL:HORIZONTALNAVBAR></DIV>
					</TD>
				</TR>
				<TR>
					<TD class="ms-titleareaframe" colSpan="3">
						<DIV class="ms-titleareaframe">
							<TABLE class="ms-titleareaframe" cellSpacing="0" cellPadding="0" width="100%" border="0">
								<TR>
									<TD>
										<table style="PADDING-LEFT: 2px; PADDING-TOP: 0px" cellSpacing="0" cellPadding="0" border="0" width="100%">
											<TR>
												<TD style="PADDING-TOP: 2px" noWrap align="middle" width="132" height="46"><IMG id="spsPageTitleIcon" alt="" src="_layouts/images/Error.gif">
												</TD>
												<TD><IMG height="1" alt="" src="_layouts/images/blank.gif" width="15">
												</TD>
												<TD noWrap width="100%">
													<TABLE cellSpacing="0" cellPadding="0">
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
											</TR>
										</TABLE>
										<table cellpadding="0" cellspacing="0" border="0" width="100%">
											<tr>
												<td height="2" colspan="5"><IMG SRC="_layouts/images/blank.gif" width="1" height="1" alt=""></td>
											</tr>
											<tr>
												<td class="ms-titlearealine" height="1" colspan="5"><IMG SRC="_layouts/images/blank.gif" width="1" height="1" alt=""></td>
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
					<TD class="ms-bodyareaframe" vAlign="top" width="100%"><CTRL:PAGELEVELERROR id="PageLevelError" runat="server"></CTRL:PAGELEVELERROR>
						<TABLE class="ms-tztable" id="ZoneTable" cellSpacing="0" cellPadding="0" width="100%" border="0">
							<tr id="TopRow">
								<td class="ms-tztop" id="TopCell" vAlign="top" width="100%"><CTRL:Login id="Login" runat="server"></CTRL:Login></td>
							</tr>
						</TABLE>
					</TD>
				</TR>
			</TABLE>
		</FORM>
	</BODY>
</HTML>

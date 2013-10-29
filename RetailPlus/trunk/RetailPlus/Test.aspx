<%@ Page language="c#" EnableViewState ="false" Inherits="AceSoft.RetailPlus._Test" CodeFile="Test.aspx.cs" %>
<%@ Register TagPrefix="CTRL" TagName="ctrlExpiry" Src="_Expiry.ascx" %>
<%@ Register TagPrefix="CTRL" TagName="ctrlMenu" Src="_Menu.ascx" %>
<%@ Register TagPrefix="CTRL" TagName="PageHeader" Src="_PageHeader.ascx" %>
<%@ Register TagPrefix="CTRL" TagName="HorizontalNavBar" Src="_HorizontalNavBar.ascx" %>
<%@ Register TagPrefix="CTRL" TagName="LargeHeading" Src="_LargeHeading.ascx" %>
<%@ Register TagPrefix="CTRL" TagName="SiteTitle" Src="_SiteTitle.ascx" %>
<%@ Register TagPrefix="CTRL" TagName="RightBodySectionSearch" Src="_RightBodySectionSearch.ascx" %>
<%@ Register TagPrefix="CTRL" TagName="PageLevelError" Src="_PageLevelError.ascx" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<script runat="server">

    protected void okbutton_Click(object sender, EventArgs e)
  {
    ClientScript.RegisterStartupScript(this.GetType(), "key", "launchModal();", true);
  }
</script>
<HTML>
	<HEAD>
		<title>AceSoft RetailPlus System</title>
		<META content="Microsoft Visual Studio 7.0" name="GENERATOR">
		<META content="C#" name="CODE_LANGUAGE">
		<META content="JavaScript" name="vs_defaultClientScript">
		<META content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="_layouts/css/menu.css" type="text/css" rel="stylesheet">
		<LINK href="_layouts/css/ows.css" type="text/css" rel="stylesheet">
		<LINK href="_layouts/css/sps.css" type="text/css" rel="stylesheet">
		<script type="text/javascript">
          var launch = false;
          function launchModal() {
            launch = true;
          }
          
          function pageLoad() {
            if (launch) {
              $find("mpe").show();
            }
          }
          </script>

        <link href="/RetailPlus/_layouts/images/rbs.ico" rel="shortcut icon" />
        <link href="/RetailPlus/_layouts/images/rbs.ico" rel="shortcut icon" />
	</HEAD>
	<body id="PageBody" scroll="yes" marginheight="0" marginwidth="0">
		<form id="frmDefaultID" name="frmDefault" action="default.aspx" method="post" runat="server">
		    <asp:ScriptManager ID="asm" runat="server" />
		    
			<table class="ms-main" height="100%" cellspacing="0" cellpadding="0" width="100%" border="0">
				<tr>
					<td width="100%" colspan="3">
						<CTRL:PageHeader id="PageHeader" runat="server"></CTRL:PageHeader>
						<div class="ms-phnav1wrapper ms-navframe">
							<CTRL:HORIZONTALNAVBAR id="HorizontalNavBar" runat="server"></CTRL:HORIZONTALNAVBAR>
							<CTRL:ctrlExpiry id="ctrlExpiry" runat="server" Visible="False"></CTRL:ctrlExpiry></div>
					</td>
				</tr>
				<tr>
					<td class="ms-titleareaframe" colspan="3">
						<div class="ms-titleareaframe">
							<table class="ms-titleareaframe" cellspacing="0" cellpadding="0" width="100%" border="0">
								<tr>
									<td>
										<table style="padding-left: 2px; padding-TOP: 0px" cellspacing="0" cellpadding="0" border="0">
											<tr>
												<td style="padding-TOP: 2px" nowrap="nowrap" align="middle" width="132" height="46"><img id="spsPageTitleIcon" alt="" src="_layouts/images/home.gif">
												</td>
												<td><img height="1" alt="" src="_layouts/images/blank.gif" width="15" />
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
								<td style="padding-RIGHT: 2px" width="180">
									<img height="1" alt="" src="_layouts/images/trans.gif" width="180">
									<CTRL:ctrlMenu id="ctrlMenu" runat="server"></CTRL:ctrlMenu>&nbsp;
								</td>
							</tr>
						</table>
					</td>
					<td><img height="1" alt="" src="_layouts/images/blank.gif" width="5"></td>
					<td class="ms-bodyareaframe" valign="top" width="100%"><CTRL:PAGELEVELERROR id="PageLevelError" runat="server"></CTRL:PAGELEVELERROR>
						<table class="ms-tztable" id="ZoneTable" cellspacing="0" cellpadding="0" width="100%" border="0">
							<tr id="TopRow">
								<td class="ms-tztop" id="TopCell" valign="top" width="100%">
								        <asp:Button ID="targetButton" runat="server" Text="targetButton" />
                                        <ajaxToolkit:ModalPopupExtender ID="ModalPopupExtender1" BehaviorID="mpe" runat="server"
                                            PopupControlID="popupPanel" CancelControlID="cancelbutton" Enabled="True" TargetControlID="targetButton">
                                        </ajaxToolkit:ModalPopupExtender>
                                    
                                    &nbsp; &nbsp;
								</td>
							</tr>
						</table>
					</td>
					<asp:Panel ID="popupPanel" runat="server" BackColor="#FFFF66"
                        Height="100%" Width="100%">
                        <br />
                        This is the popup panel!
                        <br />
                        <br />
                        <asp:Button ID="okbutton" runat="server" Text="Ok" OnClick="okbutton_Click" />
                        <asp:Button ID="cancelbutton" runat="server" Text="Cancel" />
                    </asp:Panel>
				</tr>
			</table>
			
		</form>
	</body>
</HTML>

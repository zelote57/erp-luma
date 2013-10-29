<%@ Control Language="c#" Inherits="AceSoft.RetailPlus.GeneralLedger._ChartOfAccounts.__BalanceSheet" Codebehind="_BalanceSheet.ascx.cs" %>
<%@ Register Assembly="CrystalDecisions.Web, Version=13.0.2000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304"
    Namespace="CrystalDecisions.Web" TagPrefix="CR" %>
<link href="/aspnet_client/System_Web/2_0_50727/CrystalReportWebFormViewer3/css/default.css"
    rel="stylesheet" type="text/css" />
<table cellspacing="0" cellpadding="0" width="100%" border="0">
	<tr>
		<td colspan="3"><img height="10" alt="" src="../../_layouts/images/blank.gif" width="1"/></td>
	</tr>
	<tr>
		<td><img src="../../_layouts/images/blank.gif" width="10" height="1" alt=""></td>
		<td>
			<table class="ms-toolbar" style="margin-left: 0px" cellpadding="2" cellspacing="0" border="0" width="100%">
				<tr>
					<td class="ms-toolbar" style="WIDTH: 234px">
						<table cellpadding="1" cellspacing="0" border="0">
							<tr>
								<td class="ms-toolbar" nowrap="nowrap">
									<a tabindex="2" id="idGroup" class="ms-toolbar" accesskey="N" title="Select Group"></a>
									&nbsp;<asp:imagebutton id="imgView" title="Show Balance Sheet Report" accesskey="V" tabIndex="1" height="16" width="16" border="0" ImageUrl="../../_layouts/images/tabpub.gif" runat="server" CssClass="ms-toolbar" OnClick="imgView_Click"></asp:imagebutton>
								</td>
								<td class="ms-toolbar" nowrap="nowrap"width="100">
									<asp:Label id="Label2" Runat="server" text="Select Group :">Report Options</asp:Label>
								</td>
								<td class="ms-toolbar" nowrap="nowrap">
									<asp:DropDownList id="cboReportOptions" runat="server" Width="150">
										<asp:ListItem Value="0" Selected="True">Web Format</asp:ListItem>
										<asp:ListItem Value="1">Acrobat PDF</asp:ListItem>
										<asp:ListItem Value="2">Microsoft Word</asp:ListItem>
										<asp:ListItem Value="3">Microsoft Excel</asp:ListItem>
									</asp:DropDownList>
								</td>
							</tr>
						</table>
					</td>
					<td width="99%" class="ms-toolbar" align="right" nowrap="nowrap"id="align01">
						<asp:UpdatePanel id="updPrint" runat="server">
                            <ContentTemplate>
						        <table cellspacing="0" cellpadding="0" width="100%" border="0">
							        <tr>
								        <td class="ms-toolbar" nowrap="nowrap" align="left">
									        <asp:Button id="cmdView" runat="server" Text="Go" onclick="cmdView_Click" OnClientClick="NewWindow();"></asp:Button>
								        </td>
							        </tr>
						        </table>
                                </ContentTemplate>
                        </asp:UpdatePanel>
					</td>
					<td class="ms-toolbar" align="right" nowrap="nowrap"id="align032">
						<img src="../../_layouts/images/blank.gif" width="1" height="1" alt="" />
					</td>
				</tr>
			</table>
			<asp:label id="lblReferrer" runat="server" visible="False"></asp:label>&nbsp;
		</td>
	</tr>
	<tr>
		<td><img height="1" alt="" src="../../_layouts/images/blank.gif" width="10" /></td>
		<td>
			<table cellpadding="0" cellspacing="0" border="0" width="100%">
			    <tr>
					<td class="ms-authoringcontrols" valign="top" style="PADDING-RIGHT:	10px; BORDER-TOP:	white 10px solid; PADDING-LEFT:	8px; padding-bottom:	10px" colspan="3">
						<table class="ms-authoringcontrols" style="MARGIN-BOTTOM: 5px" cellspacing="0" cellpadding="0" border="0" width="100%">
							<tr>
								<td style="padding-bottom:2px;" nowrap="nowrap">
									<label>Filter by Group</label>
								</td>
								<td>
                                    <asp:DropDownList id="cboGroup" runat="server" CssClass="ms-short">
										<asp:ListItem Value="1">Account Classification</asp:ListItem>
										<asp:ListItem Value="2">Account Summary</asp:ListItem>
										<asp:ListItem Value="3">Account Category</asp:ListItem>
										<asp:ListItem Value="4" Selected="True">Chart Of Accounts</asp:ListItem>
									</asp:DropDownList>
								</td>
								<td class="ms-separator" >&nbsp;&nbsp;|&nbsp;&nbsp;</td>
								<td style="padding-bottom:2px;" nowrap="nowrap">
									<label>
                                        <asp:CheckBox id="chkBalance" runat="server" Text="Include Balance" Checked=true /></label>
								</td>
								<td class="ms-separator" >&nbsp;&nbsp;|&nbsp;&nbsp;</td>
								<td style="padding-bottom:2px;" nowrap="nowrap">
									<label>
                                        <asp:CheckBox id="cnkInActive" runat="server" Text="Include 0 Balance" Checked=true /></label>
								</td>
								<td width="99%" id="align02" nowrap="nowrap" align="right"><img height="1" alt="" src="../../_layouts/images/blank.gif" width="1">
								</td>
							</tr>
						</table>
					</td>
				</tr>
				<tr>
					<td colspan="3" class="ms-sectionline" height="2"><img alt="" src="../../_layouts/images/empty.gif" /></td>
				</tr>
				<tr>
					<td colspan="3" height="2" align="center">
					    <CR:CrystalReportViewer id="CRViewer" runat="server" Width="350px" HasCrystalLogo="False" Height="50px" ToolPanelView="None" HasToggleGroupTreeButton="false" HasToggleParameterPanelButton="false" ></cr:crystalreportviewer>        
					</td>
				</tr>
			</table>
		</td>
		<td><img height="1" alt="" src="../../_layouts/images/blank.gif" width="10" /></td>
	</tr>
	<tr>
		<td colspan="3"><img height="10" alt="" src="../../_layouts/images/blank.gif" width="1"/></td>
	</tr>
</table>

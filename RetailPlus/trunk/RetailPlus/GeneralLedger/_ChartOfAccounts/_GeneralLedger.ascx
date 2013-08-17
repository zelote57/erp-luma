<%@ Control Language="c#" Inherits="AceSoft.RetailPlus.GeneralLedger._ChartOfAccounts.__GeneralLedger" Codebehind="_GeneralLedger.ascx.cs" %>
<table cellSpacing="0" cellPadding="0" width="100%" border="0">
	<tr>
		<td colSpan="3"><img height="10" alt="" src="../../_layouts/images/blank.gif" width="1" /></td>
	</tr>
	<TR>
		<td><IMG SRC="../../_layouts/images/blank.gif" width="10" height="1" alt=""></td>
		<TD>
			<table class="ms-toolbar" style="MARGIN-LEFT: 0px" cellpadding="2" cellspacing="0" border="0" width="100%">
				<TR>
					<td class="ms-toolbar" style="WIDTH: 234px">
						<table cellpadding="1" cellspacing="0" border="0">
							<tr>
								<td class="ms-toolbar" nowrap>
									<a tabindex="2" ID="idGroup" class="ms-toolbar" ACCESSKEY="N" title="Select Group"></a>
									&nbsp;<asp:imagebutton id="imgView" title="Show Balance Sheet Report" accessKey="V" tabIndex="1" height="16" width="16" border="0" ImageUrl="../../_layouts/images/tabpub.gif" runat="server" CssClass="ms-toolbar" OnClick="imgView_Click"></asp:imagebutton>
								</td>
								<td class="ms-toolbar" nowrap width="100">
									<asp:Label ID="Label2" Runat="server" text="Select Group :">Report Options</asp:Label>
								</td>
								<td class="ms-toolbar" nowrap>
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
					<TD width="99%" class="ms-toolbar" align="right" nowrap id="align01">
						<TABLE cellSpacing="0" cellPadding="0" width="100%" border="0">
							<TR>
								<td class="ms-toolbar" nowrap align="left">
									<asp:Button id="cmdView" runat="server" Text="Go" onclick="cmdView_Click" OnClientClick="NewWindow();"></asp:Button>
								</td>
							</TR>
						</TABLE>
					</TD>
					<td class="ms-toolbar" align="right" nowrap id="align032">
						<IMG SRC="../../_layouts/images/blank.gif" width="1" height="1" alt="">
					</td>
				</TR>
			</TABLE>
			<asp:label id="lblReferrer" runat="server" Visible="False"></asp:label>&nbsp;
		</TD>
	</TR>
	<tr>
		<td><img height="1" alt="" src="../../_layouts/images/blank.gif" width="10" /></td>
		<TD>
			<table cellpadding="0" cellspacing="0" border="0" width="100%">
			    <tr>
					<td class="ms-authoringcontrols" valign="top" style="PADDING-RIGHT:	10px; BORDER-TOP:	white 10px solid; PADDING-LEFT:	8px; PADDING-BOTTOM:	10px" colspan="3">
						<table class="ms-authoringcontrols" style="MARGIN-BOTTOM: 5px" cellSpacing="0" cellPadding="0" border="0" width="100%">
							<tr>
								<td style="PADDING-BOTTOM:2px;" nowrap>
									<label>
                                        <asp:CheckBox ID="cnkInActive" runat="server" Text="Include zero([0] Balance" Checked=true /></label>
								</td>
								<td width="99%" id="align02" noWrap align="right"><IMG height="1" alt="" src="../../_layouts/images/blank.gif" width="1">
								</td>
							</tr>
						</table>
					</td>
				</tr>
				<tr>
					<td colspan="3" class="ms-sectionline" height="2"><img alt="" src="../../_layouts/images/empty.gif"></td>
				</tr>
				<tr>
					<td colspan="3" height="2">
                        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                            <ContentTemplate>
                                <iframe runat="server" id="fraViewer" height="400" width="100%" frameborder="no"></iframe>        
                            </ContentTemplate>
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="imgView" EventName="Click" />
                                <asp:AsyncPostBackTrigger ControlID="cmdView" EventName="Click" />
                            </Triggers>
                        </asp:UpdatePanel>
						
					</td>
				</tr>
			</table>
		</TD>
		<td><img height="1" alt="" src="../../_layouts/images/blank.gif" width="10" /></td>
	</tr>
	<tr>
		<td colSpan="3"><img height="10" alt="" src="../../_layouts/images/blank.gif" width="1" /></td>
	</tr>
</table>

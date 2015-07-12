<%@ Control Language="c#" Inherits="AceSoft.RetailPlus.Reports.__ProductBranchInventoryReport" Codebehind="_ProductBranchInventoryReport.ascx.cs" %>
<table cellspacing="0" cellpadding="0" width="100%" border="0">
	<tr>
		<td colspan="3"><img height="10" alt="" src="/RetailPlus/_layouts/images/blank.gif" width="1"></td>
	</tr>
	<tr>
		<td><img src="/RetailPlus/_layouts/images/blank.gif" width="10" height="1" alt=""></td>
		<td>
			<table class="ms-toolbar" style="margin-left: 0px" cellpadding="2" cellspacing="0" border="0" width="100%">
				<tr>
					<td class="ms-toolbar" style="WIDTH: 234px">
						<table cellpadding="1" cellspacing="0" border="0">
							<tr>
								<td class="ms-toolbar" nowrap="nowrap">
									<a tabindex="2" id="idGroup" class="ms-toolbar" accesskey="N" title="Select Group"></a>
									&nbsp;<asp:imagebutton id="imgView" title="Show Rates Report" accesskey="V" tabIndex="1" height="16" width="16" border="0" ImageUrl="/RetailPlus/_layouts/images/tabpub.gif" runat="server" CssClass="ms-toolbar"></asp:imagebutton>
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
						<img src="/RetailPlus/_layouts/images/blank.gif" width="1" height="1" alt="">
					</td>
				</tr>
			</table>
			<asp:label id="lblReferrer" runat="server" visible="False"></asp:label>
		</td>
	</tr>
	<tr>
		<td><img height="1" alt="" src="/RetailPlus/_layouts/images/blank.gif" width="10"></td>
		<td>
			<table cellpadding="0" cellspacing="0" border="0" width="100%">
				<tr>
					<td class="ms-authoringcontrols" valign="top" style="PADDING-RIGHT:	10px; BORDER-TOP:	white 10px solid; PADDING-LEFT:	8px; padding-bottom:	10px" colspan="3">
						<table class="ms-authoringcontrols" style="MARGIN-BOTTOM: 5px" cellspacing="0" cellpadding="0" border="0" width="100%">
							<tr>
								<td style="padding-bottom:2px; HEIGHT:15px" nowrap="nowrap">
									<label>Filter by Group</label>
								</td>
								<td class="ms-separator" style="HEIGHT: 15px">&nbsp;&nbsp;&nbsp;</td>
								<td style="HEIGHT: 15px">
									<asp:DropDownList id="cboGroup" runat="server" CssClass="ms-short"></asp:DropDownList>
								</td>
								<td class="ms-separator" style="HEIGHT: 15px">&nbsp;&nbsp;|&nbsp;&nbsp;</td>
								<td style="padding-bottom:2px; HEIGHT:15px" nowrap="nowrap">
									<label>Filter by Sub Group</label>
								</td>
								<td style="HEIGHT: 15px">
									<asp:DropDownList id="cboSubGroup" runat="server" CssClass="ms-short"></asp:DropDownList>
								</td>
								<td width="99%" id="align02" nowrap="nowrap" align="right"><img height="1" alt="" src="/RetailPlus/_layouts/images/blank.gif" width="1">
								</td>
							</tr>
							<tr>
								<td style="padding-bottom:2px; HEIGHT:15px" nowrap="nowrap">
									<label>Product Code like</label>&nbsp;
								</td>
								<td class="ms-separator" style="HEIGHT: 15px">&nbsp;&nbsp;&nbsp;</td>
								<td colspan="4">
									<asp:TextBox id="txtProductCode" runat="server" TextMode="MultiLine" Rows="2" Width="100%"></asp:TextBox>
									<asp:Label id="Label4" CssClass="ms-error" runat="server">Enter 'Product Code' separated by semi-colon(;) to filter more than one product code.</asp:Label>
								</td>
								<td width="99%" id="align03" nowrap="nowrap" align="right" style="HEIGHT: 15px"><img height="1" alt="" src="/RetailPlus/_layouts/images/blank.gif" width="1">
								</td>
							</tr>
						</table>
					</td>
				</tr>
				<tr>
					<td colspan="3" class="ms-sectionline" height="2"><img alt="" src="/RetailPlus/_layouts/images/empty.gif"></td>
				</tr>
				<tr>
					<td colspan="3" height="2">
						<iframe runat="server" id="fraViewer" height="400" width="100%" frameborder="no"></iframe>
					</td>
				</tr>
			</table>
		</td>
		<td><img height="1" alt="" src="/RetailPlus/_layouts/images/blank.gif" width="10"></td>
	</tr>
	<tr>
		<td colspan="3"><img height="10" alt="" src="/RetailPlus/_layouts/images/blank.gif" width="1"></td>
	</tr>
</table>

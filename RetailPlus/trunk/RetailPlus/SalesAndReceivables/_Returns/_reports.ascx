<%@ Control Language="c#" Inherits="AceSoft.RetailPlus.SalesAndReceivables._Returns.__Reports" Codebehind="_Reports.ascx.cs" %>
<%@ Register Assembly="CrystalDecisions.Web, Version=13.0.2000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304"
    Namespace="CrystalDecisions.Web" TagPrefix="CR" %>
<asp:UpdatePanel ID="UpdatePanel1" runat="server">
<ContentTemplate>
<table cellSpacing="0" cellPadding="0" width="100%" border="0">
	<tr>
		<td colspan="3"><IMG height="10" alt="" src="../../_layouts/images/blank.gif" width="1"></td>
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
									<asp:imagebutton id="imgView" accessKey="V" tabIndex="1" height="16" width="16" border="0" ToolTip="View Report" ImageUrl="../../_layouts/images/tabpub.gif" runat="server" CssClass="ms-toolbar" OnClick="imgView_Click"></asp:imagebutton>
								</td>
								<td class="ms-toolbar" nowrap width="100">
									<asp:Label ID="Label2" Runat="server">Report Options</asp:Label>
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
					<TD class="ms-toolbar" align="right" nowrap id="align01">
					    <table cellSpacing="0" cellPadding="0" width="100%" border="0">
						    <TR>
							    <td class="ms-toolbar" nowrap align="left">
								    <asp:Button id="cmdView" runat="server" Text="Go" onclick="cmdView_Click"></asp:Button>
							    </td>
						    </TR>
					    </TABLE>
				    </TD>
					<TD class="ms-separator"><asp:label id="lblSeparator4" runat="server">|</asp:label></TD>
				    <td class="ms-toolbar">
					    <table cellSpacing="0" cellPadding="1" border="0">
						    <tr>
							    <td class="ms-toolbar" noWrap><asp:imagebutton id="imgBack" accessKey="B" tabIndex="3" CssClass="ms-toolbar" runat="server" ImageUrl="../../_layouts/images/impitem.gif" alt="Back to previous window" border="0" width="16" height="16" CausesValidation="False" OnClick="imgBack_Click"></asp:imagebutton></td>
							    <td noWrap><asp:linkbutton id="cmdBack" accessKey="B" tabIndex="4" CssClass="ms-toolbar" runat="server" CausesValidation="False" OnClick="cmdBack_Click">Back to previous window</asp:linkbutton></td>
						    </tr>
					    </table>
				    </td>
					<td class="ms-toolbar" align="right" nowrap id="align032" width="99%" >
						<IMG SRC="../../_layouts/images/blank.gif" width="1" height="1" alt="">
					</td>
				</TR>
			</TABLE>
			<asp:label id="lblReferrer" runat="server" Visible="False"></asp:label>
			<asp:label id="lblReturnID" runat="server" Visible="False"></asp:label>
		</TD>
	</TR>
	<tr>
		<td><IMG height="1" alt="" src="../../_layouts/images/blank.gif" width="10"></td>
		<TD>
			<table cellpadding="0" cellspacing="0" border="0" width="100%">
				<tr>
					<td colspan="3" class="ms-sectionline" height="2"><img alt="" src="../../_layouts/images/empty.gif"></td>
				</tr>
				<tr>
					<td colspan="3" height="2" align=center>
				        <cr:crystalreportviewer id="CRViewer" runat="server" hascrystallogo="False" ToolPanelView="None" HasToggleGroupTreeButton="false" HasToggleParameterPanelButton="false" ></cr:crystalreportviewer>        
				    </td>
				</tr>
			</table>
		</TD>
		<td><IMG height="1" alt="" src="../../_layouts/images/blank.gif" width="10"></td>
	</tr>
	<tr>
		<td colspan="3"><IMG height="10" alt="" src="../../_layouts/images/blank.gif" width="1"></td>
	</tr>
</table>
    </ContentTemplate>
</asp:UpdatePanel>

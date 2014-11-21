<%@ Control Language="c#" Inherits="AceSoft.RetailPlus.Credits._Customers.__Reports" Codebehind="_Reports.ascx.cs" %>
<%@ Register Assembly="CrystalDecisions.Web, Version=13.0.2000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304"
    Namespace="CrystalDecisions.Web" TagPrefix="CR" %>
<asp:UpdatePanel id="UpdatePanel1" runat="server">
<ContentTemplate>
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
									&nbsp;<asp:imagebutton id="imgView" ToolTip="Show Report" accesskey="V" tabIndex="1" height="16" width="16" border="0" ImageUrl="../../_layouts/images/tabpub.gif" runat="server" CssClass="ms-toolbar"></asp:imagebutton>
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
                    <td class="ms-toolbar" style="WIDTH: 234px">
						<table cellpadding="1" cellspacing="0" border="0">
							<tr>
								<td class="ms-toolbar" nowrap="nowrap">
									<asp:DropDownList id="cboReportType" runat="server" AutoPostBack="True" OnSelectedIndexChanged="cboReportType_SelectedIndexChanged">
										<asp:ListItem Value="0" Selected="True">Select Report Type</asp:ListItem>
									</asp:DropDownList>
								</td>
							</tr>
						</table>
					</td>
					<td class="ms-toolbar" align="right" nowrap="nowrap"id="align01">
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
					<td class="ms-separator"><asp:label id="lblSeparator4" runat="server">|</asp:label></td>
					<td class="ms-toolbar">
						<table cellspacing="0" cellpadding="1" border="0">
							<tr>
								<td class="ms-toolbar" nowrap="nowrap" style="width: 19px"><asp:imagebutton id="imgBack" accesskey="B" tabIndex="3" CssClass="ms-toolbar" runat="server" ImageUrl="../../_layouts/images/impitem.gif" alt="Back to previous window" border="0" width="16" height="16" CausesValidation="False" OnClick="imgBack_Click"></asp:imagebutton></td>
								<td nowrap="nowrap"><asp:linkbutton id="cmdBack" accesskey="B" tabIndex="4" CssClass="ms-toolbar" runat="server" CausesValidation="False" OnClick="cmdBack_Click">Back to previous window</asp:linkbutton></td>
							</tr>
						</table>
					</td>
					<td class="ms-toolbar" align="right" nowrap="nowrap"id="align032" width="99%" >
						<img src="../../_layouts/images/blank.gif" width="1" height="1" alt="">
					</td>
				</tr>
			</table>
			<asp:label id="lblReferrer" runat="server" visible="False"></asp:label> 
		    <asp:label id="lblReportType" runat="server" visible="False"></asp:label>
		</td>
	</tr>
    <tr>
		<td><img height="1" alt="" src="../../_layouts/images/blank.gif" width="10" /></td>
		<td class="ms-authoringcontrols">
		    <table class="ms-authoringcontrols" style="MARGIN-BOTTOM: 5px" cellspacing="0" cellpadding="0" border="0" width="100%">
				<tr>
					<td style="padding-bottom:2px" nowrap="nowrap">
						&nbsp;<label>Branch&nbsp;/&nbsp;Terminal #</label>&nbsp;
					</td>
					<td class="ms-separator">&nbsp;&nbsp;&nbsp;</td>
					<td nowrap="nowrap">
                        <asp:dropdownlist id="cboBranch" CssClass="ms-short" Width="125px" runat="server"></asp:dropdownlist>&nbsp:
                        <asp:TextBox id="txtTerminalNo" accessKey="S" CssClass="ms-short" runat="server" MaxLength="3" BorderStyle="Groove" Width="25px"></asp:TextBox>
					</td>
                    <td class="ms-separator">&nbsp;&nbsp;|&nbsp;&nbsp;</td>
					<td style="padding-bottom:2px" nowrap="nowrap">
						<label>Credit Type</label>
					</td>
					<td class="ms-separator">&nbsp;&nbsp;&nbsp;</td>
					<td nowrap="nowrap">
						<asp:dropdownlist id="cboCreditType" CssClass="ms-short" runat="server"></asp:dropdownlist>
					</td>
					<td width="99%" id="align05" nowrap="nowrap" align="left">&nbsp;
					</td>
				</tr>
                <div id="divDates" runat="server">
				<tr>
					<td style="padding-bottom:2px" nowrap="nowrap">
						&nbsp;<label>Creditor Last Name From</label>&nbsp;
					</td>
					<td class="ms-separator">&nbsp;&nbsp;&nbsp;</td>
					<td nowrap="nowrap">
						<asp:TextBox id="txtLastNameFrom" accessKey="S" CssClass="ms-short" runat="server" MaxLength="10" BorderStyle="Groove"></asp:TextBox>
					</td>
					<td class="ms-separator">&nbsp;&nbsp;|&nbsp;&nbsp;</td>
					<td style="padding-bottom:2px" nowrap="nowrap">
						<label>Creditor Last Name To</label>
					</td>
					<td class="ms-separator">&nbsp;&nbsp;&nbsp;</td>
					<td nowrap="nowrap">
						<asp:TextBox id="txtLastNameTo" accessKey="E" CssClass="ms-short" runat="server" BorderStyle="Groove" MaxLength="10"></asp:TextBox>
					</td>
					<td width="99%" id="Td2" nowrap="nowrap" align="left">&nbsp;
					</td>
				</tr>
                <tr>
					<td style="padding-bottom:2px" nowrap="nowrap">
						&nbsp;<asp:Label id="lblTrxStartDate" runat="server">Creditor Card Expiry Date From</asp:Label>&nbsp;
					</td>
					<td class="ms-separator">&nbsp;&nbsp;&nbsp;</td>
					<td nowrap="nowrap">
						<asp:TextBox id="txtTrxStartDate" accessKey="S" ToolTip="Double click to select date from Calendar" ondblclick="ontime(this)" CssClass="ms-short" runat="server" MaxLength="10" BorderStyle="Groove"></asp:TextBox>
					</td>
					<td class="ms-separator">&nbsp;&nbsp;|&nbsp;&nbsp;</td>
					<td style="padding-bottom:2px" nowrap="nowrap">
						<asp:Label id="lblTrxEndDate" runat="server">Creditor Card Expiry Date To</asp:Label>
					</td>
					<td class="ms-separator">&nbsp;&nbsp;&nbsp;</td>
					<td nowrap="nowrap">
						<asp:TextBox id="txtTrxEndDate" accessKey="E" ToolTip="Double click to select date from Calendar" ondblclick="ontime(this)" CssClass="ms-short" runat="server" BorderStyle="Groove" MaxLength="10"></asp:TextBox>
					</td>
					<td width="99%" id="Td1" nowrap="nowrap" align="left">&nbsp;
						<asp:Label id="Label4" CssClass="ms-error" runat="server" Font-Names="Wingdings">l</asp:Label>
						<asp:Label id="Label5" CssClass="ms-error" runat="server"> Date must be in yyyy-mm-dd format.</asp:Label>
					</td>
				</tr>
                </div>
                <div id="divBilingDate" runat="server" visible="false">
                <tr>
					<td style="padding-bottom:2px" nowrap="nowrap">
						&nbsp;<asp:Label id="Label1" runat="server">Select Billing Date</asp:Label>&nbsp;
					</td>
					<td class="ms-separator">&nbsp;&nbsp;&nbsp;</td>
					<td nowrap="nowrap">
						<asp:dropdownlist id="cboBillingDate" CssClass="ms-short" runat="server"></asp:dropdownlist>
					</td>
					<td class="ms-separator">&nbsp;&nbsp;&nbsp;&nbsp;</td>
					<td style="padding-bottom:2px" nowrap="nowrap">
						
					</td>
					<td class="ms-separator">&nbsp;&nbsp;&nbsp;</td>
					<td nowrap="nowrap">
                        
					</td>
					<td width="99%" id="Td3" nowrap="nowrap" align="left">&nbsp;
					</td>
				</tr>
                </div>
			</table>
		</td>
	</tr>
	<tr>
	    <td><img height="1" alt="" src="../../_layouts/images/blank.gif" width="10" /></td>
		<td class="ms-sectionline" height="2" style="MARGIN-BOTTOM: 5px"><img alt="" src="../../_layouts/images/empty.gif" /></td>
		<td><img height="1" alt="" src="../../_layouts/images/blank.gif" width="10" /></td>
	</tr>
	<tr>
		<td><img height="1" alt="" src="../../_layouts/images/blank.gif" width="10" /></td>
		<td>
			<table cellpadding="0" cellspacing="0" border="0" width="100%">
				<tr>
					<td colspan="3" class="ms-sectionline" height="2"><img alt="" src="../../_layouts/images/empty.gif" /></td>
				</tr>
				<tr>
					<td colspan="3" height="2" align="center">
				        <cr:crystalreportviewer id="CRViewer" runat="server" hascrystallogo="False" ToolPanelView="None" HasToggleGroupTreeButton="false" HasToggleParameterPanelButton="false" ></cr:crystalreportviewer>        
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
</ContentTemplate>
</asp:UpdatePanel>
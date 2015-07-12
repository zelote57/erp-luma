<%@ Control Language="c#" Inherits="AceSoft.RetailPlus.Reports.__ProductHistoryReport" Codebehind="_ProductHistoryReport.ascx.cs" %>
<%@ Register Assembly="CrystalDecisions.Web, Version=13.0.2000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304"
    Namespace="CrystalDecisions.Web" TagPrefix="CR" %>
<%--<link href="/aspnet_client/System_Web/2_0_50727/CrystalReportWebFormViewer3/css/default.css" rel="stylesheet" type="text/css" />--%>
<link href="/aspnet_client/System_Web/2_0_50727/crystalreportviewers13/css/default.css" rel="stylesheet" type="text/css" />
<asp:UpdatePanel id="UpdatePanel1" runat="server">
<ContentTemplate>
<table cellspacing="0" cellpadding="0" width="100%" border="0">
	<tr>
		<td colspan="3"><img height="10" alt="" src="/RetailPlus/_layouts/images/blank.gif" width="1"/></td>
	</tr>
	<tr>
		<td><img src="/RetailPlus/_layouts/images/blank.gif" width="10" height="1" alt=""></td>
		<td>
			<table class="ms-toolbar" style="margin-left: 0px" cellpadding="2" cellspacing="0" border="0" width="100%">
				<tr>
					<td class="ms-toolbar">
						<table cellpadding="1" cellspacing="0" border="0">
							<tr>
								<td class="ms-toolbar" nowrap="nowrap">
									<a tabindex="2" id="idGroup" class="ms-toolbar" accesskey="N" title="Select Group"></a>
									&nbsp;<asp:imagebutton id="imgView" title="Show Rates Report" accesskey="V" tabIndex="1" height="16" width="16" border="0" ImageUrl="/RetailPlus/_layouts/images/tabpub.gif" runat="server" CssClass="ms-toolbar"></asp:imagebutton>
								</td>
								<td class="ms-toolbar" nowrap="nowrap" width="100">
									<asp:Label id="Label2" Runat="server" text="Report Options: "></asp:Label>
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
									<asp:DropDownList id="cboReportType" runat="server" AutoPostBack="true" CausesValidation="false" OnSelectedIndexChanged="cboReportType_SelectedIndexChanged">
										<asp:ListItem Value="0" Selected="True">Select Report Type</asp:ListItem>
									</asp:DropDownList>
								</td>
							</tr>
						</table>
					</td>
					<td class="ms-toolbar" align="right" nowrap="nowrap" id="align01">
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
								<td class="ms-toolbar" nowrap="nowrap"><asp:imagebutton id="imgBack" accesskey="B" tabIndex="3" CssClass="ms-toolbar" runat="server" ImageUrl="/RetailPlus/_layouts/images/impitem.gif" alt="Back to previous window" border="0" width="16" height="16" CausesValidation="False" OnClick="imgBack_Click"></asp:imagebutton></td>
								<td nowrap="nowrap"><asp:linkbutton id="cmdBack" accesskey="B" tabIndex="4" CssClass="ms-toolbar" runat="server" CausesValidation="False" OnClick="cmdBack_Click">Back to previous window</asp:linkbutton></td>
							</tr>
						</table>
					</td>
					<td class="ms-toolbar" align="right" nowrap="nowrap" id="align032" width="99%" >
						<img src="/RetailPlus/_layouts/images/blank.gif" width="1" height="1" alt="">
					</td>
				</tr>
			</table>
			<asp:label id="lblReferrer" runat="server" visible="False"></asp:label>
		</td>
	</tr>
	<tr>
		<td></td>
		<td>
			<asp:CompareValidator id="CompareValidator1" CssClass="ms-error" runat="server" ErrorMessage="'Start Date' must be a valid date." ForeColor=" " Operator="DataTypeCheck" Type="Date" Display="Dynamic" ControlToValidate="txtStartDate"></asp:CompareValidator></td>
	</tr>
	<tr>
		<td></td>
		<td>
			<asp:CompareValidator id="CompareValidator2" CssClass="ms-error" runat="server" ErrorMessage="'End Date' must be a valid date." ForeColor=" " Operator="DataTypeCheck" Type="Date" Display="Dynamic" ControlToValidate="txtEndDate"></asp:CompareValidator></td>
	</tr>
	<tr>
		<td><img height="1" alt="" src="/RetailPlus/_layouts/images/blank.gif" width="10" /></td>
		<td>
			<table cellpadding="0" cellspacing="0" border="0" width="100%">
				<tr>
					<td class="ms-authoringcontrols" valign="top" style="PADDING-RIGHT:	10px; BORDER-TOP:	white 10px solid; PADDING-LEFT:	8px; padding-bottom:	10px" colspan="3">
						<table class="ms-authoringcontrols" style="MARGIN-BOTTOM: 5px" cellspacing="0" cellpadding="0" border="0" width="100%">
                            <asp:PlaceHolder id="holderBranch" runat="server" Visible="false">
                            <tr>
								<td style="padding-bottom:2px" nowrap="nowrap">
									<label>Branch<label>&nbsp;
								</td>
								<td class="ms-separator">&nbsp;&nbsp;&nbsp;</td>
								<td nowrap="nowrap"><asp:dropdownlist id="cboBranch" CssClass="ms-long" runat="server"></asp:dropdownlist>
								</td>
								<td class="ms-separator">&nbsp;&nbsp;&nbsp;&nbsp;</td>
								<td nowrap="nowrap">
								</td>
                                <td class="ms-separator">&nbsp;&nbsp;&nbsp;</td>
                                <td nowrap="nowrap">
                                </td>
								<td width="99%" id="Td2" nowrap="nowrap" align="right" style="HEIGHT: 15px"><img height="1" alt="" src="/RetailPlus/_layouts/images/blank.gif" width="1">
								</td>
							</tr>
                            </asp:PlaceHolder>
                            <asp:PlaceHolder id="holderProductCode" runat="server" Visible="false">
                            <tr>
								<td style="padding-bottom:2px" nowrap="nowrap">
									<label>Product Code<label>&nbsp;
								</td>
								<td class="ms-separator">&nbsp;&nbsp;&nbsp;</td>
								<td nowrap="nowrap"><asp:dropdownlist id="cboProductCode" CssClass="ms-short" Width="100%" runat="server" OnSelectedIndexChanged="cboProductCode_SelectedIndexChanged" AutoPostBack="True"></asp:dropdownlist>
								</td>
								<td class="ms-separator">&nbsp;&nbsp;|&nbsp;&nbsp;</td>
								<td nowrap="nowrap">
                                    <label>Variation</label>&nbsp;
								</td>
                                <td class="ms-separator">&nbsp;&nbsp;&nbsp;</td>
                                <td nowrap="nowrap">
                                    <asp:dropdownlist id="cboVariation" runat="server" CssClass="ms-short"></asp:dropdownlist>
                                    <asp:TextBox id="txtProductCode" accesskey="C" CssClass="ms-short" runat="server" BorderStyle="Groove" MaxLength="30"></asp:TextBox>
                                    <asp:ImageButton id="cmdProductCode" title="Search by product code" style="CURSOR: hand" accesskey="P" runat="server" ImageUrl="/RetailPlus/_layouts/images/SPSSearch2.gif" alt="Search by product code" border="0" OnClick="cmdProductCode_Click"></asp:ImageButton>
                                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                    <asp:ImageButton id="imgProductHistory" runat="server" CausesValidation="false" ImageUrl="/RetailPlus/_layouts/images/prodhist.gif"
                                        OnClick="imgProductHistory_Click" Style="cursor: hand" ToolTip="Show product inventory history report"
                                        Visible="false" />
                                    <asp:ImageButton id="imgProductPriceHistory" runat="server" CausesValidation="false"
                                        ImageUrl="/RetailPlus/_layouts/images/pricehist.gif" OnClick="imgProductPriceHistory_Click"
                                        Style="cursor: hand" ToolTip="Show product price history report" Visible="false" />
                                    <asp:ImageButton
                                            id="imgInventoryAdjustment" runat="server" CausesValidation="false" ImageUrl="/RetailPlus/_layouts/images/invadj.gif"
                                            OnClick="imgInventoryAdjustment_Click" Style="cursor: hand" ToolTip="Adjust inventory count"
                                            Visible="false" />
                                    <asp:ImageButton id="imgEditNow" runat="server" CausesValidation="false"
                                                ImageUrl="/RetailPlus/_layouts/images/edit.gif" OnClick="imgEditNow_Click" Style="cursor: hand"
                                                ToolTip="Edit this product" Visible="false" />
                                </td>
								<td width="99%" id="align02" nowrap="nowrap" align="right" style="HEIGHT: 15px"><img height="1" alt="" src="/RetailPlus/_layouts/images/blank.gif" width="1">
								</td>
							</tr>
                            </asp:PlaceHolder>
                            <asp:PlaceHolder id="holderMostSaleable" runat="server" Visible="false">
                            <tr>
                                <td style="padding-bottom:2px" nowrap="nowrap">
									<label>No of Items to Display</label>&nbsp;
								</td>
								<td class="ms-separator">&nbsp;&nbsp;&nbsp;</td>
								<td nowrap="nowrap">
									<asp:TextBox id="txtLimit" accesskey="O" CssClass="ms-short" runat="server" BorderStyle="Groove" MaxLength="10">0</asp:TextBox>
								</td>
								<td class="ms-separator">&nbsp;&nbsp;|&nbsp;&nbsp;</td>
								<td style="padding-bottom:2px" nowrap="nowrap">
									<label>Show Items By Group</label>
								</td>
								<td class="ms-separator">&nbsp;&nbsp;&nbsp;</td>
								<td nowrap="nowrap">
									<asp:CheckBox id="chkGroupItems" CssClass="ms-short" runat="server"></asp:CheckBox>
								</td>
								<td width="99%" id="Td1" nowrap="nowrap" align="left"></td>
							</tr>
                            </asp:PlaceHolder>
                            <asp:PlaceHolder id="holderHistoryDate" runat="server" Visible="true">
							<tr>
								<td style="padding-bottom:2px" nowrap="nowrap">
									<label>History Start &nbsp;Date</label>&nbsp;
								</td>
								<td class="ms-separator">&nbsp;&nbsp;&nbsp;</td>
								<td nowrap="nowrap">
									<asp:TextBox id="txtStartDate" accesskey="S" ToolTip="Double click to select date from Calendar" ondblclick="ontime(this)" CssClass="ms-short" runat="server" MaxLength="10" BorderStyle="Groove"></asp:TextBox>
									<asp:TextBox id="txtStartTime" accesskey="I" CssClass="ms-short" runat="server" BorderStyle="Groove" MaxLength="5" Width="46px">00:00</asp:TextBox>
								</td>
								<td class="ms-separator">&nbsp;&nbsp;|&nbsp;&nbsp;</td>
								<td style="padding-bottom:2px" nowrap="nowrap">
									<label>History End Date</label>
								</td>
								<td class="ms-separator">&nbsp;&nbsp;&nbsp;</td>
								<td nowrap="nowrap">
									<asp:TextBox id="txtEndDate" accesskey="E" ToolTip="Double click to select date from Calendar" ondblclick="ontime(this)" CssClass="ms-short" runat="server" BorderStyle="Groove" MaxLength="10"></asp:TextBox>
									<asp:TextBox id="txtEndTime" accesskey="M" CssClass="ms-short" runat="server" BorderStyle="Groove" MaxLength="5" Width="46px">23:59</asp:TextBox>
                                    <asp:Label id="Label3" CssClass="ms-error" runat="server" Font-Names="Wingdings">l</asp:Label>
									<asp:Label id="Label1" CssClass="ms-error" runat="server"> Date must be in yyyy-mm-dd format.</asp:Label>
								</td>
								<td width="99%" id="align05" nowrap="nowrap" align="left">&nbsp;
									
								</td>
							</tr>
                            </asp:PlaceHolder>
						</table>
					</td>
				</tr>
				<tr>
					<td colspan="3" class="ms-sectionline" height="2"><img alt="" src="/RetailPlus/_layouts/images/empty.gif" /></td>
				</tr>
				<tr>
					<td colspan="3" height="2" align="center">
					    <cr:crystalreportviewer id="CRViewer" runat="server" Width="350px" HasCrystalLogo="False" Height="50px" ToolPanelView="None" HasToggleGroupTreeButton="false" HasToggleParameterPanelButton="false" ></cr:crystalreportviewer>        
					</td>
				</tr>
			</table>
		</td>
		<td><img height="1" alt="" src="/RetailPlus/_layouts/images/blank.gif" width="10" /></td>
	</tr>
	<tr>
		<td colspan="3"><img height="10" alt="" src="/RetailPlus/_layouts/images/blank.gif" width="1"/></td>
	</tr>
</table>
</ContentTemplate>
</asp:UpdatePanel>
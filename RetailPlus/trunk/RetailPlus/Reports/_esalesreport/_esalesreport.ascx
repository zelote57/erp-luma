<%@ Control Language="c#" Inherits="AceSoft.RetailPlus.Reports.__eSalesReport" Codebehind="_esalesreport.ascx.cs" %>
<%@ Register Assembly="CrystalDecisions.Web, Version=13.0.2000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304"
    Namespace="CrystalDecisions.Web" TagPrefix="CR" %>
<link href="/aspnet_client/System_Web/2_0_50727/CrystalReportWebFormViewer3/css/default.css" rel="stylesheet" type="text/css" />
<asp:UpdatePanel id="UpdatePanel1" runat="server">
<ContentTemplate>
<table cellspacing="0" cellpadding="0" width="100%" border="0">
	<tr>
		<td colspan="3"><img height="10" alt="" src="../_layouts/images/blank.gif" width="1"/></td>
	</tr>
	<tr>
		<td><img src="../_layouts/images/blank.gif" width="10" height="1" alt=""></td>
		<td>
			<table class="ms-toolbar" style="margin-left: 0px" cellpadding="2" cellspacing="0" border="0" width="100%">
				<tr>
					<td class="ms-toolbar">
						<table cellpadding="1" cellspacing="0" border="0">
							<tr>
								<td class="ms-toolbar" nowrap="nowrap">
									<a tabindex="2" id="idGroup" class="ms-toolbar" accesskey="N" title="Select Group"></a>
									&nbsp;<asp:imagebutton id="imgView" ToolTip="Show Report" accesskey="V" tabIndex="1" height="16" width="16" border="0" alt="Show Report" ImageUrl="~/_layouts/images/tabpub.gif" runat="server" CssClass="ms-toolbar"></asp:imagebutton>
								</td>
								<td class="ms-toolbar" nowrap="nowrap">
									<asp:Label id="Label2" Runat="server" text="Report Options: "></asp:Label>
								</td>
								<td class="ms-toolbar" nowrap="nowrap">
									<asp:DropDownList id="cboReportOptions" runat="server">
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
								<td class="ms-toolbar" nowrap="nowrap" style="width: 19px"><asp:imagebutton id="imgBack" accesskey="B" tabIndex="3" CssClass="ms-toolbar" runat="server" ImageUrl="~/_layouts/images/impitem.gif" alt="Back to previous window" border="0" width="16" height="16" CausesValidation="False" OnClick="imgBack_Click"></asp:imagebutton></td>
								<td nowrap="nowrap"><asp:linkbutton id="cmdBack" accesskey="B" tabIndex="4" CssClass="ms-toolbar" runat="server" CausesValidation="False" OnClick="cmdBack_Click">Back to previous window</asp:linkbutton></td>
							</tr>
						</table>
					</td>
					<td class="ms-toolbar" align="right" nowrap="nowrap" id="align032" width="99%" >
						<img src="../_layouts/images/blank.gif" width="1" height="1" alt="">
					</td>
				</tr>
			</table>
			<asp:label id="lblReferrer" runat="server" visible="False"></asp:label>
		</td>
	</tr>
	<tr>
		<td></td>
		<td>
			<asp:CompareValidator id="CompareValidator1" CssClass="ms-error" runat="server" ErrorMessage="'Start Date' must be a valid date." ForeColor=" " Operator="DataTypeCheck" Type="Date" Display="Dynamic" ControlToValidate="txtStartTransactionDate"></asp:CompareValidator></td>
	</tr>
	<tr>
		<td></td>
		<td>
			<asp:CompareValidator id="CompareValidator2" CssClass="ms-error" runat="server" ErrorMessage="'End Date' must be a valid date." ForeColor=" " Operator="DataTypeCheck" Type="Date" Display="Dynamic" ControlToValidate="txtEndTransactionDate"></asp:CompareValidator></td>
	</tr>
	<tr>
		<td></td>
		<td></td>
	</tr>
	<tr>
		<td><img height="1" alt="" src="../_layouts/images/blank.gif" width="10" /></td>
		<td>
			<table cellpadding="0" cellspacing="0" border="0" width="100%">
				<tr>
					<td class="ms-authoringcontrols" valign="top" style="PADDING-RIGHT:	10px; BORDER-TOP:	white 10px solid; PADDING-LEFT:	8px; padding-bottom:	10px" colspan="3">
						<table class="ms-authoringcontrols" style="MARGIN-BOTTOM: 5px" cellspacing="0" cellpadding="0" border="0" width="100%">
							<%--<tr>
							    <td colspan=8>
							        
							    </td>
							</tr>--%>
                            <asp:PlaceHolder id="holderTranDate" runat="server" Visible="true">
				                <tr>
					                <td style="padding-bottom:2px" nowrap="nowrap">
						                <label>Transaction Start &nbsp;Date</label>&nbsp;
					                </td>
					                <td class="ms-separator">&nbsp;&nbsp;&nbsp;</td>
					                <td nowrap="nowrap">
						                <asp:TextBox id="txtStartTransactionDate" ondblclick="ontime(this)" accesskey="S" CssClass="ms-short" runat="server" BorderStyle="Groove" MaxLength="10" ToolTip="Double click to select date from Calendar" Width="100px"></asp:TextBox>
						                <asp:TextBox id="txtStartTime" runat="server" AccessKey="I" BorderStyle="Groove" CssClass="ms-short" MaxLength="5" Width="65px">00:00</asp:TextBox>
					                </td>
					                <td class="ms-separator">&nbsp;&nbsp;|&nbsp;&nbsp;</td>
					                <td style="padding-bottom:2px" nowrap="nowrap">
						                <label>Transaction End Date</label>
					                </td>
					                <td class="ms-separator">&nbsp;&nbsp;&nbsp;</td>
					                <td nowrap="nowrap">
						                <asp:TextBox id="txtEndTransactionDate" ondblclick="ontime(this)" accesskey="E" CssClass="ms-short" runat="server" BorderStyle="Groove" MaxLength="10" ToolTip="Double click to select date from Calendar" Width="100px"></asp:TextBox>
                                        <asp:TextBox id="txtEndTime" runat="server" AccessKey="M" BorderStyle="Groove" CssClass="ms-short" MaxLength="5" Width="65px">23:59</asp:TextBox>
                                        &nbsp;&nbsp;
                                        <asp:Label id="Label3" CssClass="ms-error" runat="server" Font-Names="Wingdings">l</asp:Label>
						                <asp:Label id="Label4" CssClass="ms-error" runat="server"> Date must be in yyyy-mm-dd format.</asp:Label>
					                </td>
					                <td width="99%" id="align05" nowrap="nowrap" align="left">&nbsp;
					                </td>
				                </tr>
					        </asp:PlaceHolder>
							<asp:PlaceHolder id="holderTransaction" runat="server" Visible="false">
				                <tr>
					                <td style="padding-bottom:2px" nowrap="nowrap">
						                <label>Filter by Transaction #</label>&nbsp;
					                </td>
					                <td class="ms-separator">&nbsp;&nbsp;&nbsp;</td>
					                <td>
						                <asp:TextBox id="txtTransactionNo" accesskey="O" CssClass="ms-short" runat="server" BorderStyle="Groove"></asp:TextBox>
					                </td>
					                <td class="ms-separator">&nbsp;&nbsp;|&nbsp;&nbsp;</td>
					                <td style="padding-bottom:2px" nowrap="nowrap">
						                <label>Filter if Consignment</label>
					                </td>
					                <td class="ms-separator">&nbsp;&nbsp;&nbsp;</td>
					                <td>
						                <asp:DropDownList id="cboConsignment" runat="server" CssClass="ms-short"></asp:DropDownList>
					                </td>
					                <td width="99%" id="align02" nowrap="nowrap" align="right"><img height="1" alt="" src="../_layouts/images/blank.gif" width="1">
					                </td>
				                </tr>
                                <tr>
					                <td style="padding-bottom:2px" nowrap="nowrap">
						                <label>Filter by&nbsp;Status</label>
					                </td>
					                <td class="ms-separator">&nbsp;&nbsp;&nbsp;</td>
					                <td>
						                <asp:DropDownList id="cboTransactionStatus" runat="server" CssClass="ms-short"></asp:DropDownList>
					                </td>
					                <td class="ms-separator">&nbsp;&nbsp;|&nbsp;&nbsp;</td>
					                <td style="padding-bottom:2px" nowrap="nowrap">
						                <label>Filter by Payment Type</label>
					                </td>
					                <td class="ms-separator">&nbsp;&nbsp;&nbsp;</td>
					                <td nowrap="nowrap">
						                <asp:DropDownList id="cboPaymentType" CssClass="ms-short" runat="server"></asp:DropDownList>
					                </td>
					                <td width="99%" id="align04" nowrap="nowrap" align="right"><img height="1" alt="" src="../_layouts/images/blank.gif" width="1">
					                </td>
				                </tr>
                                <tr>
					                <td style="padding-bottom:2px" nowrap="nowrap">
						                <label>Filter by Customer</label>
					                </td>
					                <td class="ms-separator">&nbsp;&nbsp;&nbsp;</td>
					                <td colspan="4">
                                        <asp:dropdownlist id="cboContactName" CssClass="ms-long" runat="server"></asp:dropdownlist>
                                    </td>
                                    <td>
                                        <asp:textbox id="txtContactName" accesskey="C" runat="server" CssClass="ms-short" BorderStyle="Groove" MaxLength="30" ></asp:textbox>
                                        <asp:imagebutton id="imgContactNameSearch" ToolTip="Execute search" 
                                            style="CURSOR: hand; width: 16px;" accesskey="P" 
                                            ImageUrl="~/_layouts/images/SPSSearch2.gif" runat="server" 
                                            CausesValidation="False" onclick="imgContactNameSearch_Click"></asp:imagebutton>
					                </td>
					                <td width="99%" id="Td3" nowrap="nowrap" align="right"><img height="1" alt="" src="../_layouts/images/blank.gif" width="1">
					                </td>
				                </tr>
				                <tr>
					                <td style="padding-bottom:2px" nowrap="nowrap">
						                <label>Filter by Cashier</label>
					                </td>
					                <td class="ms-separator">&nbsp;&nbsp;&nbsp;</td>
					                <td colspan="4">
						                <asp:dropdownlist id="cboCashierName" CssClass="ms-long" runat="server"></asp:dropdownlist>
                                    </td>
                                    <td>
                                        <asp:textbox id="txtCashierName" accesskey="H" runat="server" CssClass="ms-short" BorderStyle="Groove" MaxLength="30" ></asp:textbox>
                                        <asp:imagebutton id="imgCashierNameSearch" ToolTip="Execute search" 
                                            style="CURSOR: hand; width: 16px;" accesskey="P" 
                                            ImageUrl="~/_layouts/images/SPSSearch2.gif" runat="server" 
                                            CausesValidation="False" onclick="imgCashierNameSearch_Click"></asp:imagebutton>
					                </td>
					                <td width="99%" id="align03" nowrap="nowrap" align="right"><img height="1" alt="" src="../_layouts/images/blank.gif" width="1">
					                </td>
				                </tr>
                                <tr>
					                <td style="padding-bottom:2px" nowrap="nowrap">
						                <label>Filter by Agent</label>
					                </td>
					                <td class="ms-separator">&nbsp;&nbsp;&nbsp;</td>
					                <td colspan="4">
						                <asp:dropdownlist id="cboAgent" CssClass="ms-long" runat="server"></asp:dropdownlist>
                                    </td>
                                    <td>
                                        <asp:textbox id="txtAgent" accesskey="H" runat="server" CssClass="ms-short" BorderStyle="Groove" MaxLength="30" ></asp:textbox>
                                        <asp:imagebutton id="imgAgentSearch" ToolTip="Execute search" 
                                            style="CURSOR: hand; width: 16px;" accesskey="P" 
                                            ImageUrl="~/_layouts/images/SPSSearch2.gif" runat="server" 
                                            CausesValidation="False" onclick="imgAgentSearch_Click"></asp:imagebutton>
					                </td>
					                <td width="99%" id="Td7" nowrap="nowrap" align="right"><img height="1" alt="" src="../_layouts/images/blank.gif" width="1">
					                </td>
				                </tr> 
				            </asp:PlaceHolder>
                            <asp:PlaceHolder id="holderTerminaNo" runat="server" Visible="false">
                                <tr>
                                    <td style="padding-bottom:2px" nowrap="nowrap">
						                <label>Filter by Terminal No</label>
					                </td>
					                <td class="ms-separator">&nbsp;&nbsp;&nbsp;</td>
					                <td>
						                <asp:dropdownlist id="cboTerminalNo" CssClass="ms-short" runat="server"></asp:dropdownlist>
					                </td>
					                <td class="ms-separator">&nbsp;&nbsp;|&nbsp;&nbsp;</td>
					                <td style="padding-bottom:2px" nowrap="nowrap">
						                <label>Filter by Branch</label>
					                </td>
					                <td class="ms-separator">&nbsp;&nbsp;&nbsp;</td>
					                <td nowrap="nowrap">
						                <asp:DropDownList id="cboBranch" CssClass="ms-short" runat="server"></asp:DropDownList>
					                </td>
					                <td width="99%" id="Td8" nowrap="nowrap" align="right"><img height="1" alt="" src="../_layouts/images/blank.gif" width="1">
					                </td>
				                </tr>
                            </asp:PlaceHolder>
							<asp:PlaceHolder id="holderSalesperItem" runat="server" Visible="false">
					            <tr>
				                    <td style="padding-bottom:2px" nowrap="nowrap" colspan="3">
						                <label>Filter the Sales Per Item report by group</label>
					                </td>
					                <td class="ms-separator">&nbsp;&nbsp;|&nbsp;&nbsp;</td>
					                <td colspan=5>
						                <asp:DropDownList id="cboProductGroup" CssClass="ms-long" runat="server" Width="315px"></asp:DropDownList>
					                </td>
					                <td width="99%" id="Td1" nowrap="nowrap" align="right">
                                        <img height="1" alt="" src="../_layouts/images/blank.gif" width="1">
					                </td>
				                </tr>
				                <tr>
				                    <td style="padding-bottom:2px" nowrap="nowrap" colspan="7">
                                        <asp:RadioButton id="rdoShowAll" GroupName="FilterSalesPerItem" runat="server" Text="Show both positive and negative margins " Checked="true" />
                                        <asp:RadioButton id="rdoShowPositiveOnly" GroupName="FilterSalesPerItem" runat="server" Text="Show items with positive margins only " />
                                        <asp:RadioButton id="rdoShowNegativeOnly" GroupName="FilterSalesPerItem" runat="server" Text="Show items with negative margins only "/>
					                </td>
					                <td width="99%" id="Td2" nowrap="nowrap" align="right">
                                        <img height="1" alt="" src="../_layouts/images/blank.gif" width="1">
					                </td>
				                </tr>
					        </asp:PlaceHolder>
                            <asp:PlaceHolder id="holderSummarizedDailySales" runat="server" Visible="false">
				                <tr>
				                    <td style="padding-bottom:2px" nowrap="nowrap" colspan="7">
                                        &nbsp;&nbsp;&nbsp;&nbsp;<asp:RadioButton id="optActualAndEffective" runat="server" GroupName="SummarizedDailySalesReportType" Text="Use actual reporting & effective date" Checked="True" />
                                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:RadioButton id="optActual" runat="server" GroupName="SummarizedDailySalesReportType" Text="Use actual reporting date" />
                                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:RadioButton id="optEffective" runat="server" GroupName="SummarizedDailySalesReportType" Text="Use effective date" />
					                </td>
					                <td width="99%" id="Td6" nowrap="nowrap" align="right">
                                        <img height="1" alt="" src="../_layouts/images/blank.gif" width="1">
					                </td>
				                </tr>
					        </asp:PlaceHolder>
                            <asp:PlaceHolder id="holderSalesPerDay" runat="server" Visible="false">
				                <tr>
					                <td style="padding-bottom:2px" nowrap="nowrap">
						                <label>Select month</label>&nbsp;
					                </td>
					                <td class="ms-separator">&nbsp;&nbsp;&nbsp;</td>
					                <td nowrap="nowrap">
						                <asp:DropDownList id="cboMonth" runat="server" CssClass="ms-short"></asp:DropDownList>
					                </td>
					                <td class="ms-separator">&nbsp;&nbsp;|&nbsp;&nbsp;</td>
					                <td style="padding-bottom:2px" nowrap="nowrap">
						                <label>Select Year</label>
					                </td>
					                <td class="ms-separator">&nbsp;&nbsp;&nbsp;</td>
					                <td nowrap="nowrap">
						                <asp:DropDownList id="cboYear" runat="server" CssClass="ms-short"></asp:DropDownList>
					                </td>
					                <td width="99%" id="Td5" nowrap="nowrap" align="left">&nbsp;
					                </td>
				                </tr>
					        </asp:PlaceHolder>
						</table>
					</td>
				</tr>
				<tr>
					<td colspan="3" class="ms-sectionline" height="2"><img alt="" src="../_layouts/images/empty.gif"></td>
				</tr>
				<tr>
					<td colspan="3" height="2" align="center">
					    <CR:CrystalReportViewer id="CRViewer" runat="server" Width="350px" HasCrystalLogo="False" Height="50px" ToolPanelView="None" HasToggleGroupTreeButton="false" HasToggleParameterPanelButton="false" ></cr:crystalreportviewer>        
					</td>
				</tr>
			</table>
		</td>
		<td><img height="1" alt="" src="../_layouts/images/blank.gif" width="10" /></td>
	</tr>
	<tr>
		<td colspan="3"><img height="10" alt="" src="../_layouts/images/blank.gif" width="1"/></td>
	</tr>
</table>
</ContentTemplate>
<Triggers><asp:AsyncPostBackTrigger ControlID="CRViewer" /></Triggers>
</asp:UpdatePanel>

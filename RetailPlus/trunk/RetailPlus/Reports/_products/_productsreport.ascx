<%@ Control Language="c#" Inherits="AceSoft.RetailPlus.Reports.__ProductsReport" Codebehind="_ProductsReport.ascx.cs" %>
<%@ Register Assembly="CrystalDecisions.Web, Version=13.0.2000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304" Namespace="CrystalDecisions.Web" TagPrefix="CR" %>
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
									&nbsp;<asp:imagebutton id="imgView" ToolTip="Show Report" accesskey="V" tabIndex="1" height="16" width="16" border="0" ImageUrl="/RetailPlus/_layouts/images/tabpub.gif" runat="server" CssClass="ms-toolbar"></asp:imagebutton>
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
									<asp:DropDownList id="cboReportType" runat="server" AutoPostBack="true" CausesValidation="false" OnSelectedIndexChanged="cboReportType_SelectedIndexChanged">
										<asp:ListItem Value="0" Selected="True">Select Report Type</asp:ListItem>
									</asp:DropDownList>
								</td>
							</tr>
						</table>
					</td>
					<td class="ms-toolbar" nowrap="nowrap"id="align01">
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
								<td class="ms-toolbar" nowrap="nowrap"><asp:imagebutton id="imgBack" title="Back to previous window" accesskey="C" tabIndex="3" CssClass="ms-toolbar" runat="server" ImageUrl="/RetailPlus/_layouts/images/impitem.gif" alt="Back to previous window" border="0" width="16" height="16" CausesValidation="False" OnClick="imgBack_Click"></asp:imagebutton></td>
								<td nowrap="nowrap"><asp:linkbutton id="cmdBack" title="Back to previous window" accesskey="C" tabIndex="4" CssClass="ms-toolbar" runat="server" CausesValidation="False" onclick="cmdCancel_Click">Back to previous window</asp:linkbutton></td>
							</tr>
						</table>
					</td>
					<td class="ms-toolbar" align="right" nowrap="nowrap"id="align032" width="99%" >
						<img src="/RetailPlus/_layouts/images/blank.gif" width="1" height="1" alt="" />
					</td>
				</tr>
			</table>
			<asp:label id="lblReferrer" runat="server" visible="False"></asp:label>
		</td>
	</tr>
	<tr>
		<td><img height="1" alt="" src="/RetailPlus/_layouts/images/blank.gif" width="10" /></td>
		<td>
			<table cellpadding="0" cellspacing="0" border="0" width="100%">
				<tr>
					<td class="ms-authoringcontrols" valign="top" style="PADDING-RIGHT:	10px; BORDER-TOP:	white 10px solid; PADDING-LEFT:	8px; padding-bottom:	10px" colspan="3">
						<table class="ms-authoringcontrols" style="MARGIN-BOTTOM: 5px" cellspacing="0" cellpadding="0" border="0" width="100%">
                            <tr>
								<td style="padding-bottom:2px; HEIGHT:15px" nowrap="nowrap">
									<label>Filter by Branch</label>
								<img height="1" alt="" src="/RetailPlus/_layouts/images/blank.gif" width="1">
								</td>
								<td class="ms-separator" style="HEIGHT: 15px">&nbsp;&nbsp;&nbsp;</td>
								<td style="HEIGHT: 15px" colspan="3">
									<asp:dropdownlist id="cboBranch" CssClass="ms-long" runat="server"></asp:dropdownlist>
                                    
								</td>
                                <td style="padding-bottom:2px; HEIGHT:15px" nowrap="nowrap">
									<label></label>
								</td>
								<td width="99%" id="Td2" nowrap="nowrap" align="right"><img height="1" alt="" src="/RetailPlus/_layouts/images/blank.gif" width="1">
								</td>
							</tr>
							<tr>
								<td style="padding-bottom:2px; HEIGHT:15px" nowrap="nowrap">
									<label>Filter by Supplier</label>
								<img height="1" alt="" src="/RetailPlus/_layouts/images/blank.gif" width="1">
								</td>
								<td class="ms-separator" style="HEIGHT: 15px">&nbsp;&nbsp;&nbsp;</td>
								<td style="HEIGHT: 15px" colspan="3">
									<asp:dropdownlist id="cboContact" CssClass="ms-long" runat="server"></asp:dropdownlist>
                                    
								</td>
                                <td style="padding-bottom:2px; HEIGHT:15px" nowrap="nowrap">
                                    <asp:textbox id="txtContactCode" accesskey="C" runat="server" CssClass="ms-short" BorderStyle="Groove" MaxLength="30" ></asp:textbox>
                                    <asp:imagebutton id="imgContactCodeSearch" ToolTip="Execute search" 
                                        style="CURSOR: hand; width: 16px;" accesskey="P" 
                                        ImageUrl="/RetailPlus/_layouts/images/SPSSearch2.gif" runat="server" 
                                        CausesValidation="False" onclick="imgContactCodeSearch_Click"></asp:imagebutton>
									<label>Filter by Supplier</label>
								</td>
								<td width="99%" id="Td3" nowrap="nowrap" align="right"><img height="1" alt="" src="/RetailPlus/_layouts/images/blank.gif" width="1">
								</td>
							</tr>
                            <tr>
								<td style="padding-bottom:2px; HEIGHT:15px" nowrap="nowrap">
                                    <label>Filter by Group</label>
								<img height="1" alt="" src="/RetailPlus/_layouts/images/blank.gif" width="1">
								</td>
								<td class="ms-separator" style="HEIGHT: 15px">&nbsp;&nbsp;&nbsp;</td>
								<td style="HEIGHT: 15px" colspan="3">
                                    <asp:dropdownlist id="cboProductGroup" CssClass="ms-long" runat="server" AutoPostBack="True" onselectedindexchanged="cboProductGroup_SelectedIndexChanged"></asp:dropdownlist>
								</td>
                                <td style="padding-bottom:2px; HEIGHT:15px" nowrap="nowrap">
                                    <asp:textbox id="txtProductGroupCode" accesskey="G" runat="server" CssClass="ms-short" BorderStyle="Groove" MaxLength="30" ></asp:textbox>
                                    <asp:imagebutton id="imgProductGroupCodeSearch" ToolTip="Execute search" 
                                        style="CURSOR: hand" accesskey="P" 
                                        ImageUrl="/RetailPlus/_layouts/images/SPSSearch2.gif" runat="server" 
                                        CausesValidation="False" onclick="imgProductGroupCodeSearch_Click"></asp:imagebutton>
                                    <label>Filter by Group</label>
								</td>
								<td width="99%" id="Td4" nowrap="nowrap" align="right"><img height="1" alt="" src="/RetailPlus/_layouts/images/blank.gif" width="1">
								</td>
							</tr>
                            <tr>
								<td style="padding-bottom:2px; HEIGHT:15px" nowrap="nowrap">
									<label>Filter by Sub Group</label>
								<img height="1" alt="" src="/RetailPlus/_layouts/images/blank.gif" width="1">
								</td>
								<td class="ms-separator" style="HEIGHT: 15px">&nbsp;&nbsp;&nbsp;</td>
								<td style="HEIGHT: 15px" colspan="3">
									<asp:DropDownList id="cboSubGroup" runat="server" CssClass="ms-long"></asp:DropDownList>
								</td>
                                <td style="padding-bottom:2px; HEIGHT:15px" nowrap="nowrap">
                                    <asp:textbox id="txtSubGroupCode" accesskey="C" runat="server" CssClass="ms-short" BorderStyle="Groove" MaxLength="30" ></asp:textbox>
                                    <asp:imagebutton id="imgSubGroupCodeSearch" ToolTip="Execute search" 
                                        style="CURSOR: hand" accesskey="P" 
                                        ImageUrl="/RetailPlus/_layouts/images/SPSSearch2.gif" runat="server" 
                                        CausesValidation="False" onclick="imgSubGroupCodeSearch_Click"></asp:imagebutton>
									<label>Filter by Sub Group</label>
								</td>
								<td width="99%" id="align02" nowrap="nowrap" align="right"><img height="1" alt="" src="/RetailPlus/_layouts/images/blank.gif" width="1">
								</td>
							</tr>
							<asp:PlaceHolder id="holderWeighted" runat="server" Visible="false">
							<tr>
								<td style="padding-bottom:2px; HEIGHT:15px" nowrap="nowrap">
								<img height="1" alt="" src="/RetailPlus/_layouts/images/blank.gif" width="1">
								</td>
								<td class="ms-separator" style="HEIGHT: 15px">&nbsp;&nbsp;&nbsp;</td>
								<td style="HEIGHT: 15px">
									<asp:CheckBox id="chkIncludeBuying" runat=server Checked=true text="" />
                                    <label>Check if will Include Buying Price</label>
								</td>
								<td class="ms-separator" style="HEIGHT: 15px">&nbsp;&nbsp;&nbsp;&nbsp;</td>
								<td style="padding-bottom:2px; HEIGHT:15px" nowrap="nowrap">
									
								</td>
								<td style="HEIGHT: 15px">
									<asp:CheckBox id="chkIncludeMargin" runat=server Checked=true text="" />
                                    <label>Check if will Include Margin</label>
								</td>
								<td width="99%" id="Td1" nowrap="nowrap" align="right"><img height="1" alt="" src="/RetailPlus/_layouts/images/blank.gif" width="1">
								</td>
							</tr>
							</asp:PlaceHolder>
                            <asp:PlaceHolder id="holderHistoryDate" runat="server" Visible="false">
							<tr>
                                <td style="padding-bottom:2px; HEIGHT:15px" nowrap="nowrap">
									<label>Transaction Start Date</label>
								<img height="1" alt="" src="/RetailPlus/_layouts/images/blank.gif" width="1">
								</td>
								<td class="ms-separator" style="HEIGHT: 15px">&nbsp;&nbsp;&nbsp;</td>
								<td style="HEIGHT: 15px" colspan="4" nowrap="nowrap">
									<asp:TextBox id="txtStartDate" accesskey="S" ToolTip="Double click to select date from Calendar" ondblclick="ontime(this)" CssClass="ms-short" runat="server" MaxLength="10" BorderStyle="Groove"></asp:TextBox>
									<asp:TextBox id="txtStartTime" accesskey="I" CssClass="ms-short" runat="server" BorderStyle="Groove" MaxLength="5" Width="46px">00:00</asp:TextBox>
                                    &nbsp;&nbsp;To&nbsp;&nbsp;
                                    <asp:TextBox id="txtEndDate" accesskey="E" ToolTip="Double click to select date from Calendar" ondblclick="ontime(this)" CssClass="ms-short" runat="server" BorderStyle="Groove" MaxLength="10"></asp:TextBox>
									<asp:TextBox id="txtEndTime" accesskey="M" CssClass="ms-short" runat="server" BorderStyle="Groove" MaxLength="5" Width="46px">23:59</asp:TextBox>
                                    <asp:Label id="Label3" CssClass="ms-error" runat="server" Font-Names="Wingdings">l</asp:Label>
									<asp:Label id="Label1" CssClass="ms-error" runat="server"> Date must be in yyyy-mm-dd format.</asp:Label>
								</td>
								<td width="99%" id="Td5" nowrap="nowrap" align="right"><img height="1" alt="" src="/RetailPlus/_layouts/images/blank.gif" width="1">
								</td>
							</tr>
                            </asp:PlaceHolder>
							<tr>
								<td style="padding-bottom:2px; HEIGHT:15px" nowrap="nowrap">
									<label>Product Code like</label>&nbsp;
								</td>
								<td class="ms-separator" style="HEIGHT: 15px">&nbsp;&nbsp;&nbsp;</td>
								<td colspan="4">
                                    <asp:TextBox id="txtProductCode" runat="server" TextMode="MultiLine" Rows="1" Width="100%"></asp:TextBox>
									<asp:Label id="Label4" CssClass="ms-error" runat="server">Enter 'Product Code' separated by semi-colon(;) to filter more than one product code.</asp:Label>
								</td>
								<td width="99%" id="align03" nowrap="nowrap" align="right" style="HEIGHT: 15px"><img height="1" alt="" src="/RetailPlus/_layouts/images/blank.gif" width="1">
								</td>
							</tr>
						</table>
					</td>
				</tr>
				<tr>
					<td colspan="3" class="ms-sectionline" height="2"><img alt="" src="../_layouts/images/empty.gif"></td>
				</tr>
				<tr>
					<td colspan="3" height="2" align="center">
					    <cr:crystalreportviewer id="CRViewer" runat="server" Width="100%" HasCrystalLogo="False" Height="50px" ToolPanelView="None" HasToggleGroupTreeButton="false" HasToggleParameterPanelButton="false" ></cr:crystalreportviewer>        
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
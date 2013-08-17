<%@ Control Language="c#" Inherits="AceSoft.RetailPlus.Reports.__ProductInventoryReport" Codebehind="_ProductInventoryReport.ascx.cs" %>
<%@ Register Assembly="CrystalDecisions.Web, Version=13.0.2000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304"
    Namespace="CrystalDecisions.Web" TagPrefix="CR" %>
<asp:UpdatePanel id="UpdatePanel1" runat="server"><ContentTemplate>
<table cellspacing="0" cellpadding="0" width="100%" border="0">
    <tr>
		<td colspan="3"><img height="10" alt="" src="../../_layouts/images/blank.gif" width="1" /></td>
	</tr>
	<tr>
		<td><img src="../../_layouts/images/blank.gif" width="10" height="1" alt="" /></td>
		<TD>
			<table class="ms-toolbar" style="MARGIN-LEFT: 0px" cellpadding="2" cellspacing="0" border="0" width="100%">
				<tr>
					<td class="ms-toolbar">
						<table cellpadding="1" cellspacing="0" border="0">
							<tr>
								<td class="ms-toolbar" nowrap="nowrap">
									<a tabindex="2" id="idGroup" class="ms-toolbar" ACCESSKEY="N" title="Select Group"></a>
									&nbsp;<asp:imagebutton id="imgView" ToolTip="Show Report" accessKey="V" tabIndex="1" height="16" width="16" border="0" ImageUrl="../../_layouts/images/tabpub.gif" runat="server" CssClass="ms-toolbar"></asp:imagebutton>
								</td>
								<td class="ms-toolbar" nowrap="nowrap" width="100">
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
					<TD class="ms-toolbar" nowrap="nowrap" id="align01">
						<table cellspacing="0" cellpadding="0" width="100%" border="0">
							<tr>
								<td class="ms-toolbar" nowrap="nowrap" align="left">
									<asp:Button id="cmdView" runat="server" Text="Go" onclick="cmdView_Click" OnClientClick="NewWindow();"></asp:Button>
								</td>
							</tr>
						</TABLE>
					</TD>
					<TD class="ms-separator"><asp:label id="lblSeparator4" runat="server">|</asp:label></TD>
					<td class="ms-toolbar">
						<table cellspacing="0" cellpadding="1" border="0">
							<tr>
								<td class="ms-toolbar" nowrap="nowrap"><asp:imagebutton id="imgBack" title="Back to previous window" accessKey="C" tabIndex="3" CssClass="ms-toolbar" runat="server" ImageUrl="../../_layouts/images/impitem.gif" alt="Back to previous window" border="0" width="16" height="16" CausesValidation="False" OnClick="imgBack_Click"></asp:imagebutton></td>
								<td nowrap="nowrap"><asp:linkbutton id="cmdBack" title="Back to previous window" accessKey="C" tabIndex="4" CssClass="ms-toolbar" runat="server" CausesValidation="False" onclick="cmdCancel_Click">Back to previous window</asp:linkbutton></td>
							</tr>
						</table>
					</td>
					<td class="ms-toolbar" align="right" nowrap="nowrap" id="align032" width="99%" >
						<img src="../../_layouts/images/blank.gif" width="1" height="1" alt="" />
					</td>
				</tr>
			</TABLE>
			<asp:label id="lblReferrer" runat="server" Visible="False"></asp:label>
		</TD>
	</tr>
	<tr>
		<td><img height="1" alt="" src="../../_layouts/images/blank.gif" width="10" /></td>
		<TD>
			<table cellpadding="0" cellspacing="0" border="0" width="100%">
				<tr>
					<td class="ms-authoringcontrols" valign="top" style="PADDING-RIGHT:	10px; BORDER-TOP:	white 10px solid; PADDING-LEFT:	8px; PADDING-BOTTOM:	10px" colspan="3">
						<table class="ms-authoringcontrols" style="MARGIN-BOTTOM: 5px" cellspacing="0" cellpadding="0" border="0" width="100%">
				            <asp:PlaceHolder id="holderExpiry" runat="server" Visible="false">
                            <tr>
                                <td style="HEIGHT:15px" nowrap="nowrap">
						            <label>Filter by Expiration Date</label>
					            </td>
					            <td class="ms-separator" style="HEIGHT: 15px">&nbsp;&nbsp;&nbsp;</td>
					            <td style="HEIGHT: 15px" colspan="7">
						            <asp:TextBox id="txtExpiryDate" ondblclick="ontime(this)" accessKey="E" CssClass="ms-short" runat="server" ToolTip="Double click to select date from Calendar" MaxLength="10" BorderStyle="Groove"></asp:TextBox>
                                    <asp:Label id="Label1" CssClass="ms-error" runat="server"> Date must be in yyyy-mm-dd format.</asp:Label>
					            </td>
					            <td width="99%" id="Td1" nowrap="nowrap" align="right"><img height="1" alt="" src="../../_layouts/images/blank.gif" width="1" />
					            </td>
				            </tr>
                            <tr>
                                <td style="HEIGHT:15px" colspan="8">
					            </td>
					            <td width="99%" id="Td5" nowrap="nowrap" align="right"><img height="1" alt="" src="../../_layouts/images/blank.gif" width="1" />
					            </td>
				            </tr>
                            </asp:PlaceHolder>
                            <tr>
					            <td style="HEIGHT:15px" nowrap="nowrap">
						            <label>Filter by Branch</label>
					            </td>
					            <td class="ms-separator" style="HEIGHT: 15px">&nbsp;&nbsp;&nbsp;</td>
					            <td style="HEIGHT: 15px">
						            <asp:dropdownlist id="cboBranch" CssClass="ms-medium" runat="server"></asp:dropdownlist>
					            </td>
                                <td style="HEIGHT:15px" nowrap="nowrap">
                                </td>
                                <td style="HEIGHT:15px;  width:25px; text-align:center;" nowrap="nowrap">
                                </td>
                                <td style="HEIGHT:15px" nowrap="nowrap">
						            <label>Filter by Supplier</label>
					            </td>
					            <td class="ms-separator" style="HEIGHT: 15px">&nbsp;&nbsp;&nbsp;</td>
					            <td style="HEIGHT: 15px">
						            <asp:dropdownlist id="cboContact" CssClass="ms-medium" runat="server"></asp:dropdownlist>
					            </td>
                                <td style="HEIGHT:15px" nowrap="nowrap">
                                    <asp:textbox id="txtContactCode" accessKey="C" runat="server" CssClass="ms-short" BorderStyle="Groove" MaxLength="30" ></asp:textbox>
                                    <asp:imagebutton id="imgContactCodeSearch" ToolTip="Execute search" 
                                        style="CURSOR: hand; width: 16px;" accessKey="P" 
                                        ImageUrl="../../_layouts/images/SPSSearch2.gif" runat="server" 
                                        CausesValidation="False" onclick="imgContactCodeSearch_Click"></asp:imagebutton>
					            </td>
					            <td width="99%" id="Td2" nowrap="nowrap" align="right"><img height="1" alt="" src="../../_layouts/images/blank.gif" width="1" />
					            </td>
				            </tr>
                            <tr>
					            <td style="HEIGHT:15px" nowrap="nowrap">
                                    <label>Filter by Group</label>
					            </td>
					            <td class="ms-separator" style="HEIGHT: 15px">&nbsp;&nbsp;&nbsp;</td>
					            <td style="HEIGHT: 15px">
                                    <asp:dropdownlist id="cboProductGroup" CssClass="ms-medium" runat="server" AutoPostBack="True" onselectedindexchanged="cboProductGroup_SelectedIndexChanged"></asp:dropdownlist>
					            </td>
                                <td style="HEIGHT:15px" nowrap="nowrap">
                                    <asp:textbox id="txtProductGroupCode" accessKey="G" runat="server" CssClass="ms-short" BorderStyle="Groove" MaxLength="30" ></asp:textbox>
                                    <asp:imagebutton id="imgProductGroupCodeSearch" ToolTip="Execute search" 
                                        style="CURSOR: hand" accessKey="P" 
                                        ImageUrl="../../_layouts/images/SPSSearch2.gif" runat="server" 
                                        CausesValidation="False" onclick="imgProductGroupCodeSearch_Click"></asp:imagebutton>
                                </td>
                                <td style="HEIGHT:15px; width:25px; text-align:center;" nowrap="nowrap">
                                </td>
                                <td style="HEIGHT:15px" nowrap="nowrap">
						            <label>Filter by Sub Group</label>
					            </td>
					            <td class="ms-separator" style="HEIGHT: 15px">&nbsp;&nbsp;&nbsp;</td>
					            <td style="HEIGHT: 15px">
						            <asp:DropDownList id="cboSubGroup" runat="server" CssClass="ms-medium"></asp:DropDownList>
					            </td>
                                <td style="HEIGHT:15px" nowrap="nowrap">
                                    <asp:textbox id="txtSubGroupCode" accessKey="C" runat="server" CssClass="ms-short" BorderStyle="Groove" MaxLength="30" ></asp:textbox>
                                    <asp:imagebutton id="imgSubGroupCodeSearch" ToolTip="Execute search" 
                                        style="CURSOR: hand" accessKey="P" 
                                        ImageUrl="../../_layouts/images/SPSSearch2.gif" runat="server" 
                                        CausesValidation="False" onclick="imgSubGroupCodeSearch_Click"></asp:imagebutton>
                                </td>
					            <td width="99%" id="Td4" nowrap="nowrap" align="right"><img height="1" alt="" src="../../_layouts/images/blank.gif" width="1" />
					            </td>
				            </tr>
                            <tr>
					            <td style="PADDING-BOTTOM:2px; HEIGHT:15px" nowrap="nowrap">
						            <label>Product Code like</label>&nbsp;
					            </td>
					            <td class="ms-separator" style="HEIGHT: 15px">&nbsp;&nbsp;&nbsp;</td>
					            <td colspan="7">
						            <asp:TextBox id="txtProductCode" runat="server" TextMode="MultiLine" Rows="1" Width="99%"></asp:TextBox>
						            <asp:Label id="Label4" CssClass="ms-error" runat="server" Visible="false">Enter 'Product Code' separated by semi-colon(;) to filter more than one product code.</asp:Label>
					            </td>
					            <td width="99%" id="align03" nowrap="nowrap" align="right" style="HEIGHT: 15px"><img height="1" alt="" src="../../_layouts/images/blank.gif" width="1" />
					            </td>
				            </tr>
			            </table>
					</td>
				</tr>
				<tr>
					<td colspan="3" class="ms-sectionline" height="2"><img height="1" alt="" src="../../_layouts/images/empty.gif" width="1" /></td>
				</tr>
				<tr>
					<td colspan="3" height="2" align="center">
					    <cr:crystalreportviewer id="CRViewer" runat="server" Width="100%" HasCrystalLogo="False" Height="50px" ToolPanelView="None" HasToggleGroupTreeButton="false" HasToggleParameterPanelButton="false" ></cr:crystalreportviewer>        
					</td>
				</tr>
			</table>
		</TD>
		<td><img height="1" alt="" src="../../_layouts/images/blank.gif" width="10" /></td>
	</tr>
	<tr>
		<td colspan="3"><img height="10" alt="" src="../../_layouts/images/blank.gif" width="1" /></td>
	</tr>
</ContentTemplate>
</asp:UpdatePanel>